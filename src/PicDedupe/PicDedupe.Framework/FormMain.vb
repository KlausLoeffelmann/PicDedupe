Imports PicDedupe.Controls
Imports PicDedupe.Generic

Public Class FormMain

    Private WithEvents _fileCrawler As FileCrawler
    Private _stopWatch As Stopwatch
    Private _lastUpdate As Long
    Private _lastItemCount As Integer
    Private WithEvents _timer As Timer
    Private _itemsPerSecondCalculator As New ItemsPerSecondCalculator(200)
    Private _lastUpdateTime As TimeSpan

    Private Const UpdateInterval As Integer = 50 ' Update Intervall in ms.

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        _timer = New Timer() With
        {
            .Interval = 1000,
            .Enabled = True
        }

    End Sub

    Private Async Sub fileCrawlerPathPicker_PathChanged(sender As Object, e As PathChangedEventArgs) Handles fileCrawlerPathPicker.PathChanged
        Await UpdatePathView(e.Path)
    End Sub

    Private Sub _timer_Tick(sender As Object, e As EventArgs) Handles _timer.Tick
        CurrentTime.Text = Date.Now.ToShortTimeString
    End Sub

    Private Sub FileCrawler_ProgressUpdate(sender As Object, e As ProgressUpdateEventArgs)

        'we'll visually update only every 50 ms.
        If _lastUpdate + UpdateInterval < _stopWatch.ElapsedMilliseconds Then
            _lastUpdate = _stopWatch.ElapsedMilliseconds

            Invoke(
                Sub()
                    For Each item In fileCrawlerFolderListView.Items
                        DirectCast(item, FileSystemListViewItem).UpdateItem()
                    Next
                    UpdateStatusBar(e.NodeToUpdate)
                End Sub)
        End If
    End Sub

    Private Sub FileCrawler_TopLevelDirectoriesAvailable(sender As Object, e As TopLevelDirectoriesAvailableEventArgs)
        Invoke(
            Sub()
                For Each item In e.RootNode.Nodes
                    Me.fileCrawlerFolderListView.Items.Add(New FileSystemListViewItem(item.Value))
                Next
                UpdateStatusBar(_fileCrawler.RootNode)
            End Sub)
    End Sub

    Private Async Function UpdatePathView(path As String) As Task

        _stopWatch = Diagnostics.Stopwatch.StartNew

        Dim directoryTree = Await Task.Run(
            Function()
                _fileCrawler = New FileCrawler(path)

                AddHandler _fileCrawler.ProgressUpdate,
                    AddressOf FileCrawler_ProgressUpdate

                AddHandler _fileCrawler.TopLevelDirectoriesAvailable,
                    AddressOf FileCrawler_TopLevelDirectoriesAvailable

                Return _fileCrawler.GetFiles()
            End Function)

        UpdateStatusBar(directoryTree.RootNode)

    End Function

    Private Sub UpdateStatusBar(directoryNode As FileEntryNode)
        TotalFileSize.Text = $"Total file size: {CType(directoryNode.Length, MemorySize)}"
        TotalFileCount.Text = $"Total file count: {directoryNode.FileCount:#,##0)}"
        ElapsedTime.Text = $"Elapsed time: {_stopWatch.Elapsed:hh\:mm\:ss}"

        If _stopWatch.Elapsed - _lastUpdateTime > New TimeSpan(0, 0, 0, 0, 200) Then
            _lastUpdateTime = _stopWatch.Elapsed
            Dim itemsPerSecond = 5 * (directoryNode.FileCount - _lastItemCount)
            _itemsPerSecondCalculator.AddElement(itemsPerSecond)
            ItemsPerSecondProcessed.Text = $"Items per Second: {_itemsPerSecondCalculator.Avergage:#,##0}"
            _lastItemCount = directoryNode.FileCount
        End If
    End Sub
End Class
