Imports PicDedupe.Controls
Imports PicDedupe.Generic

Public Class FormMain

    Private WithEvents _fileCrawler As FileCrawler
    Private _stopWatch As Stopwatch

    Private Async Sub fileCrawlerPathPicker_PathChanged(sender As Object, e As PathChangedEventArgs) Handles fileCrawlerPathPicker.PathChanged
        Await UpdatePathView(e.Path)
    End Sub

    Private Sub FileCrawler_ProgressUpdate(sender As Object, e As ProgressUpdateEventArgs)
        Invoke(
            Sub()
                For Each item In fileCrawlerFolderListView.Items
                    DirectCast(item, FolderListViewItem).UpdateItem()
                Next
                UpdateStatusBar(e.NodeToUpdate)
            End Sub)
    End Sub

    Private Sub FileCrawler_TopLevelDirectoriesAvailable(sender As Object, e As TopLevelDirectoriesAvailableEventArgs)
        Invoke(
            Sub()
                For Each item In e.TopLevelDirectories
                    Me.fileCrawlerFolderListView.Items.Add(New FolderListViewItem(item))
                Next
                UpdateStatusBar(_fileCrawler.RootNode)
            End Sub)
    End Sub

    Private Async Function UpdatePathView(path As String) As Task

        _stopWatch = Diagnostics.Stopwatch.StartNew

        Dim directoryTree = Await Task.Run(
            Function()
                _fileCrawler = New FileCrawler(New IO.DirectoryInfo(path))

                AddHandler _fileCrawler.ProgressUpdate,
                    AddressOf FileCrawler_ProgressUpdate

                AddHandler _fileCrawler.TopLevelDirectoriesAvailable,
                    AddressOf FileCrawler_TopLevelDirectoriesAvailable

                Return _fileCrawler.GetFiles()
            End Function)

        UpdateStatusBar(directoryTree.RootNode)

    End Function

    Private Sub UpdateStatusBar(directoryNode As DirectoryInfoNode)
        TotalFileSize.Text = $"Total file size: {directoryNode.FileSize}"
        TotalFileCount.Text = $"Total file count: {directoryNode.FileCount:#,##0)}"
        ElapsedTime.Text = $"Elapsed time: {_stopWatch.Elapsed:hh\:mm\:ss}"
    End Sub
End Class
