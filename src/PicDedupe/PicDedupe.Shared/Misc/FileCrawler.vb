Imports System.IO
Imports System.Runtime.CompilerServices

Public Class FileCrawler

    Public Event TopLevelDirectoriesAvailable(sender As Object, e As TopLevelDirectoriesAvailableEventArgs)
    Public Event ProgressUpdate(sender As Object, e As ProgressUpdateEventArgs)

    Private Const AllFilesSearchPattern = "*.*"

    Private ReadOnly _startPath As DirectoryInfo
    Private ReadOnly _progressReporter As Action(Of ProgressReportInfo)
    Private ReadOnly _searchPattern As String()

    Public Sub New(
        startPath As DirectoryInfo,
        Optional searchPattern() As String = Nothing,
        Optional progressReporter As Action(Of ProgressReportInfo) = Nothing)

        If Not startPath.Exists Then
            Throw New DirectoryNotFoundException($"Directory {startPath.FullName} does not exist.")
        End If

        searchPattern = If(searchPattern, New String() {".*"})

        _startPath = startPath
        _searchPattern = searchPattern
        _progressReporter = progressReporter
    End Sub

    Public Function GetTopLevelDirectoriesAsync() As DirectoryInfo()
        Return _startPath.GetDirectories()
    End Function

    Public Function GetFiles() As DirectoryInfoTree

        Dim searchAction As Action
        Dim directoryInfoTree = New DirectoryInfoTree(_startPath)
        Dim currentNode = directoryInfoTree.RootNode
        Dim currentFileItem As FileInfo = Nothing

        If _searchPattern.Any(Function(searchPattern) searchPattern = ".*") Then
            searchAction =
                Sub()
                    currentNode.AddFile(currentFileItem)
                End Sub
        Else
            searchAction =
                Sub()
                    If _searchPattern.Any(Function(searchPattern) searchPattern = currentFileItem.Extension) Then
                        currentNode.AddFile(currentFileItem)
                    End If
                End Sub
        End If

        Dim topLevelDirectories = New List(Of DirectoryInfoNode)
        Dim topLevelDirectoriesAvailableFired = False
        Dim fileCount As Integer = 0

        Dim ioDirectories = _startPath.EnumerateAllSubDirectories()

        For Each directoryItem In ioDirectories

            currentNode = directoryInfoTree.AddDirectory(directoryItem)

            If Not topLevelDirectoriesAvailableFired Then
                If directoryItem.Parent.FullName <> _startPath.FullName Then
                    RaiseEvent TopLevelDirectoriesAvailable(Me, New TopLevelDirectoriesAvailableEventArgs(topLevelDirectories))
                    topLevelDirectoriesAvailableFired = True
                Else
                    topLevelDirectories.Add(currentNode)
                End If
            End If

            Dim files As IEnumerable(Of FileInfo) = Nothing

            Try
                files = directoryItem.EnumerateFiles(AllFilesSearchPattern)
                For Each currentFileItem In files
                    searchAction()
                    fileCount += 1
                    If fileCount = 50 Then
                        fileCount = 0
                        RaiseEvent ProgressUpdate(Me, New ProgressUpdateEventArgs(currentNode))
                    End If
                Next
            Catch ex As Exception
                ' We swallow this, if EnumerateFiles causes an exception.
            End Try
        Next

        Return directoryInfoTree
    End Function

End Class

Public Module FileInfoExtension

    <Extension>
    Public Function EnumerateAllSubDirectories(directory As DirectoryInfo) As IEnumerable(Of DirectoryInfo)

        Dim queue As EnumerableQueue(Of DirectoryInfo) = Nothing

        ' This is the delegate which gets called on dequeueing each item.
        ' It practically fills up the queue with the SubItems from that item.
        ' (If there are any).
        Dim getSubFolder = New Action(Of DirectoryInfo)(
            Sub(item)
                Dim newDirectories As IEnumerable(Of DirectoryInfo) = Nothing

                Try
                    newDirectories = item.EnumerateDirectories()
                Catch ex As Exception
                    'We swallow those.
                End Try

                If newDirectories?.FirstOrDefault IsNot Nothing Then
                    queue.Queue(newDirectories)
                End If
            End Sub)

        queue = New EnumerableQueue(Of DirectoryInfo)(getSubFolder)

        ' We use that delegate here, too, to kick the operation off.
        ' We start the queue with the direct subfolders of the directory
        ' we got passed as the start path.
        getSubFolder(directory)

        Return queue.ToIEnumerable
    End Function
End Module
