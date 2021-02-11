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

        Const EventRaiserCounterThreshold As Integer = 2000

        Dim searchAction As Action
        Dim eventRaiser As Action(Of DirectoryInfoNode)
        Dim directoryInfoTree = New DirectoryInfoTree(_startPath)
        Dim currentNode = directoryInfoTree.RootNode
        Dim currentFileItem As FileInfo = Nothing
        Dim eventRaiseCounter As Integer

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

        eventRaiser =
            Sub(nodeToUpdate)
                eventRaiseCounter += 1
                If eventRaiseCounter = EventRaiserCounterThreshold Then
                    eventRaiseCounter = 0
                    RaiseEvent ProgressUpdate(Me, ProgressUpdateEventArgs.GetDefault(nodeToUpdate))
                End If
            End Sub

        Dim topLevelDirectories = New List(Of DirectoryInfoNode)
        Dim topLevelDirectoriesAvailableFired = False
        Dim fileCount As Integer = 0

        Dim ioDirectories = _startPath.EnumerateAllSubDirectories(FileAttributes.Hidden Or FileAttributes.System)

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
                    eventRaiser(currentNode)
                Next
                eventRaiser(currentNode)
            Catch ex As Exception
                ' We swallow this, if EnumerateFiles causes an exception.
            End Try
        Next

        ' We update now unconditionally.
        eventRaiseCounter = EventRaiserCounterThreshold
        eventRaiser(currentNode)

        Return directoryInfoTree
    End Function

End Class

Public Module FileInfoExtension

    <Extension>
    Public Function EnumerateAllSubDirectories(directory As DirectoryInfo, Optional excludeAttributes As FileAttributes = Nothing) As IEnumerable(Of DirectoryInfo)

        Dim queue As EnumerableQueue(Of DirectoryInfo) = Nothing

        ' This is the delegate which gets called on dequeueing each item.
        ' It practically fills up the queue with the SubItems from that item.
        ' (If there are any).
        Dim getSubFolder = New Action(Of DirectoryInfo)(
            Sub(item)
                Dim newDirectories As IEnumerable(Of DirectoryInfo) = Nothing

                Try
                    newDirectories = item.
                        EnumerateDirectories().
                        Where(Function(dirItem) Not dirItem.Attributes.HasFlag(excludeAttributes))

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
