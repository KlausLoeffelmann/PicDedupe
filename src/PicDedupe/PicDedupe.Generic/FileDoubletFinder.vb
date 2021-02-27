Imports System.IO

Public Class FileDoubletFinder

    'Private Const DefaultFilesSearchPattern = "*.cs|*.vb|*.csproj|*.vbproj|*.sln|*.ico|*.bmp|*.png|*.jpg|*.gif|*.resx|*.xml"
    Private Const DefaultFilesSearchPattern = "*.bmp|*.png|*.jpg|*.jpeg|*.gif|*.ico|*.tif|*.tiff|*.raw|*.arw|*.heic|*.nef|*.cr2"

    Private ReadOnly _files As Dictionary(Of Long, List(Of FileEntry))
    Private ReadOnly _entryFilter As Func(Of FileEntry, Boolean)
    Private ReadOnly _searchPatternArray As String()

    Public Event FileDoubletFound(sender As Object, e As FileDoubletFoundEventArgs)

    Public Sub New()
        _files = New Dictionary(Of Long, List(Of FileEntry))
        Dim eliminatedAsterisks = DefaultFilesSearchPattern.Replace("*", "")
        _searchPatternArray = eliminatedAsterisks.Split("|"c)

        _entryFilter = Function(entry) _searchPatternArray.Any(
            Function(searchPattern) searchPattern = Path.GetExtension(entry.Path).ToLower)

    End Sub

    Public Async Function AddFileAsync(file As FileEntry) As Task

        ' This algorithm is way too trivial for professional purposes.
        ' But to find Double pics and for demo purposes, it does the trick!
        Try
            Dim eventArgs As FileDoubletFoundEventArgs
            Dim fileDoubletList As List(Of FileEntry) = Nothing

            If Not _entryFilter(file) Then
                Return
            End If

            If file.Length < IgnoreFilesSmallerThan Then
                Return
            End If

            If Not _files.TryGetValue(file.Length, fileDoubletList) Then
                fileDoubletList = If(fileDoubletList, New List(Of FileEntry)())
                fileDoubletList.Add(file)
                _files.Add(file.Length, fileDoubletList)
                Return
            End If

            For Each fileToCompare In fileDoubletList
                If TakeFilenameIntoAccount Then
                    Dim fileToComparePath = Path.GetFileNameWithoutExtension(fileToCompare.Path)
                    Dim thisFilePath = Path.GetFileNameWithoutExtension(file.Path)

                    ' Let's test if the shorter filename matches the longer at one point:
                    ' (like 'firstphoto' and 'firstphoto(2)').
                    If fileToComparePath.Length < thisFilePath.Length Then
                        Dim temp = fileToComparePath
                        fileToComparePath = thisFilePath
                        thisFilePath = Path.GetFileNameWithoutExtension(fileDoubletList(0).Path)
                        fileToComparePath = temp
                    End If

                    'if it doesn't, we don't consider it a doublet, and return.
                    If fileToComparePath.Contains(thisFilePath) Then
                        Continue For
                    End If
                End If

                If file.Length < CompleteFileHashThreshold Then
                    Dim firstFileFoundHashTask = fileToCompare.GetFileHashAsync
                    Dim currentFileFoundHashTask = file.GetFileHashAsync

                    Await Task.WhenAll(firstFileFoundHashTask, currentFileFoundHashTask)

                    If Not QuickCompareHashes(
                        firstFileFoundHashTask.Result,
                        currentFileFoundHashTask.Result) Then
                        Continue For
                    End If
                End If

                file.LinkedTo = fileToCompare
                eventArgs = New FileDoubletFoundEventArgs(file)
                RaiseEvent FileDoubletFound(Me, eventArgs)
                Exit For
            Next
            fileDoubletList.Add(file)
        Catch ex As Exception
            'Stop
        End Try
    End Function

    Public Shared Function QuickCompareHashes(
        firstBytes As Byte(),
        secondBytes As Byte()) As Boolean

        Dim firstBytesSpan = firstBytes
        Dim secondBytesSpan = secondBytes

        Return firstBytesSpan.SequenceEqual(secondBytesSpan)
    End Function

    Public Property CompleteFileHashThreshold As Long = Long.MaxValue
    Public Property IgnoreFilesSmallerThan As Long = 65535
    Public Property TakeFilenameIntoAccount As Boolean = True

End Class
