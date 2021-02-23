Imports System.Collections.Immutable    ' We can use this in .NET 4.8, since we added the NuGet System.Collection.Immutable
Imports System.ComponentModel
Imports System.IO

Public Class FileDoubletFinder

    Private ReadOnly _files As Dictionary(Of Long, List(Of FileEntry))
    Public Event FileDoubletFound(sender As Object, e As FileDoubletFoundEventArgs)

    Public Sub New()
        _files = New Dictionary(Of Long, List(Of FileEntry))
    End Sub

    Public Async Function AddFileAsync(file As FileEntry) As Task
        Try

            Dim eventArgs As FileDoubletFoundEventArgs
            Dim fileDoubletList As List(Of FileEntry) = Nothing

            If file.Length < IgnoreFilesSmallerThan Then
                Return
            End If

            If Not _files.TryGetValue(file.Length, fileDoubletList) Then
                fileDoubletList = If(fileDoubletList, New List(Of FileEntry)())
                fileDoubletList.Add(file)
                _files.Add(file.Length, fileDoubletList)
                Return
            End If

            If TakeFilenameIntoAccount Then
                Dim firstFileFound = Path.GetFileNameWithoutExtension(fileDoubletList(0).Path)
                Dim thisFile = Path.GetFileNameWithoutExtension(file.Path)

                ' Let's test if the shorter filename matches the longer at one point:
                ' (like 'firstphoto' and 'firstphoto(2)').
                If firstFileFound.Length < thisFile.Length Then
                    firstFileFound = thisFile
                    thisFile = Path.GetFileNameWithoutExtension(fileDoubletList(0).Path)
                End If

                'if it doesn't, we don't consider it a doublet, and return.
                If firstFileFound.IndexOf(thisFile) = -1 Then
                    Return
                End If
            End If

            If file.Length < CompleteFileHashThreshold Then
                Dim firstFileFoundHashTask = fileDoubletList(0).GetFileHashAsync
                Dim currentFileFoundHashTask = file.GetFileHashAsync

                Await Task.WhenAll(firstFileFoundHashTask, currentFileFoundHashTask)

                If Not QuickCompareHashes(
                    firstFileFoundHashTask.Result,
                    currentFileFoundHashTask.Result) Then
                    Return
                End If

            End If

            eventArgs = New FileDoubletFoundEventArgs(file, fileDoubletList.ToImmutableList)
            RaiseEvent FileDoubletFound(Me, eventArgs)

            If Not eventArgs.Cancel Then
                fileDoubletList.Add(file)
            End If
        Catch ex As Exception
            Stop
        End Try
    End Function

    Public Function QuickCompareHashes(
        firstBytes As Byte(),
        secondBytes As Byte()) As Boolean

        Dim firstBytesSpan = firstBytes.AsSpan
        Dim secondBytesSpan = secondBytes.AsSpan

        Return firstBytesSpan.SequenceEqual(secondBytesSpan)
    End Function

    Public Property CompleteFileHashThreshold As Long = Long.MaxValue
    Public Property IgnoreFilesSmallerThan As Long = 65535
    Public Property TakeFilenameIntoAccount As Boolean = True

End Class

Public Class FileDoubletFoundEventArgs
    Inherits CancelEventArgs

    Sub New(fileFound As FileEntry, doubletList As ImmutableList(Of FileEntry))
        Me.FileFound = fileFound
        Me.DoubletList = doubletList
    End Sub

    Public Property FileFound As FileEntry
    Public Property DoubletList As ImmutableList(Of FileEntry)
End Class
