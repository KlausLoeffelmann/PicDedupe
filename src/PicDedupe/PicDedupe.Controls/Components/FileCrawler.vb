Imports System.IO
Imports PicDedupe.Generic

Public Class FileCrawler

    Public Event TopLevelDirectoriesAvailable(sender As Object, e As TopLevelDirectoriesAvailableEventArgs)
    Public Event ProgressUpdate(sender As Object, e As ProgressUpdateEventArgs)

    Private Const AllFilesSearchPattern = "*.*"

    Private ReadOnly _startPath As String
    Private ReadOnly _searchPattern As String()

    Private _directoryInfoTree As FileSystemInfoTree

    Public Sub New(
        startPath As String,
        Optional searchPattern() As String = Nothing)

        If Not New DirectoryInfo(startPath).Exists Then
            Throw New DirectoryNotFoundException($"Directory {startPath} does not exist.")
        End If

        searchPattern = If(searchPattern, New String() {".*"})

        _startPath = startPath
        _searchPattern = searchPattern
        _directoryInfoTree = New FileSystemInfoTree(_startPath)
    End Sub

    Public Function GetFiles() As FileSystemInfoTree

        Const EventRaiserCounterThreshold As Integer = 10

        Dim searchAction As Action(Of String, Long)
        Dim currentNode = _directoryInfoTree.RootNode
        Dim currentFile As String = Nothing
        Dim eventRaiseCounter As Integer

        _directoryInfoTree = New FileSystemInfoTree(_startPath)

        If _searchPattern.Any(Function(searchPattern) searchPattern = ".*") Then
            searchAction =
                Sub(file, size)
                    currentNode.AddFile(file, size)
                End Sub
        Else
            searchAction =
                Sub(file, size)
                    Dim fileExtension = Path.GetExtension(file)
                    If _searchPattern.Any(Function(searchPattern) searchPattern = fileExtension) Then
                        currentNode.AddFile(file, size)
                    End If
                End Sub
        End If

        Dim topLevelDirectoriesAvailableFired = False
        Dim fileCount As Integer = 0

        Dim ioDirectories = New DirectoryInfo(_startPath).EnumerateAllSubDirectories(FileAttributes.Hidden Or FileAttributes.System)

        For Each directoryItem In ioDirectories

            currentNode = _directoryInfoTree.AddDirectory(directoryItem)

            Dim files As IEnumerable(Of FileInfo) = Nothing

            Try
                files = New DirectoryInfo(directoryItem).EnumerateFiles(AllFilesSearchPattern)
                For Each currentFileItem In files
                    searchAction(currentFileItem.FullName, currentFileItem.Length)
                    RaiseEvent ProgressUpdate(Me, ProgressUpdateEventArgs.GetDefault(RootNode))
                Next
                RaiseEvent ProgressUpdate(Me, ProgressUpdateEventArgs.GetDefault(RootNode))
            Catch ex As Exception
                ' We swallow this, if EnumerateFiles causes an exception.
            End Try

            If Not topLevelDirectoriesAvailableFired Then
                If Path.GetDirectoryName(directoryItem) <> _startPath Then
                    RaiseEvent TopLevelDirectoriesAvailable(Me, New TopLevelDirectoriesAvailableEventArgs(RootNode))
                    topLevelDirectoriesAvailableFired = True
                End If
            End If

        Next

        ' We update now unconditionally.
        eventRaiseCounter = EventRaiserCounterThreshold
        RaiseEvent ProgressUpdate(Me, ProgressUpdateEventArgs.GetDefault(RootNode))

        Return _directoryInfoTree
    End Function

    Public ReadOnly Property RootNode As FileSystemInfoNode
        Get
            Return _directoryInfoTree?.RootNode
        End Get
    End Property

End Class
