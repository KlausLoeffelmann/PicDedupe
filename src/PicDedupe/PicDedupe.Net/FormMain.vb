Imports PicDedupe.Controls
Imports PicDedupe.Generic
Imports PicDedupe.Net.My

Public Class FormMain

    Private Const UpdateInterval = 100

    Private ReadOnly _itemsPerSecondCalculator As New ItemsPerSecondCalculator(200)

    Private WithEvents _fileCrawler As FileCrawler
    Private WithEvents _doubletFinder As FileDoubletFinder
    Private WithEvents _timer As Timer

    Private _stopWatch As Stopwatch
    Private _lastUpdate As Long
    Private _lastItemCount As Integer
    Private _lastUpdateTime As TimeSpan

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        _timer = New Timer() With
        {
            .Interval = 1000,
            .Enabled = True
        }

        _doubletFinder = New FileDoubletFinder

        AddHandler _timer.Tick, Sub() CurrentTime.Text = $"Time: {Now.ToLongTimeString}"
        AddHandler _doubletFinder.FileDoubletFound, AddressOf DoubletFinder_FileDoubletFound
        AddHandler doubletsTreeView.RequestSetting, Sub(sender, e) e.Value = MySettings.Default(e.Key)
        AddHandler doubletsTreeView.WriteSetting, Sub(sender, e)
                                                      MySettings.Default(e.Key) = e.Value
                                                      My.Settings.Save()
                                                  End Sub
    End Sub

    Protected Overrides Sub OnLoad(e As EventArgs)
        MyBase.OnLoad(e)

        'Get the Last search Path from the Settings and assign it to the PathPicker.
        If Not String.IsNullOrWhiteSpace(My.Settings.LastSearchPath) Then
            fileCrawlerPathPicker.BrowserPath = My.Settings.LastSearchPath
        End If
    End Sub

    Private Async Sub FileCrawlerPathPicker_PathChanged(sender As Object, e As PathChangedEventArgs) Handles fileCrawlerPathPicker.PathChanged
        My.Settings.LastSearchPath = e.Path
        My.Settings.Save()

        Await UpdatePathView(e.Path)
    End Sub

    Private Sub DoubletFinder_FileDoubletFound(sender As Object, e As FileDoubletFoundEventArgs)
        Invoke(Sub() doubletsTreeView.AddDoublet(e.FileFound))
    End Sub

    Private Sub FileCrawler_ProgressUpdate(sender As Object, e As ProgressUpdateEventArgs)

        'we'll visually update only every 100 ms.
        If _lastUpdate + UpdateInterval < _stopWatch.ElapsedMilliseconds Then
            _lastUpdate = _stopWatch.ElapsedMilliseconds

            Invoke(
            Sub()
                UpdateListView()
                UpdateStatusBar(e.NodeToUpdate)
            End Sub)
        End If
    End Sub

    Private Sub UpdateListView()
        For Each item In fileCrawlerFolderListView.Items
            DirectCast(item, FileEntryListViewItem).UpdateItem()
        Next
    End Sub

    Private Sub FileCrawler_TopLevelDirectoriesAvailable(sender As Object, e As TopLevelDirectoriesAvailableEventArgs)
        Invoke(
            Sub()
                For Each item In e.RootNode.Nodes
                    Me.fileCrawlerFolderListView.Items.Add(New FileEntryListViewItem(item.Value))
                Next
                UpdateStatusBar(_fileCrawler.RootNode)
            End Sub)
    End Sub

    Private Async Function UpdatePathView(path As String) As Task

        _stopWatch = Diagnostics.Stopwatch.StartNew
        _lastUpdate = _stopWatch.ElapsedMilliseconds

        _fileCrawler = New FileCrawler(path)
        _fileCrawler.DoubletFinder = _doubletFinder

        fileCrawlerFolderListView.Items.Clear()
        doubletsTreeView.ClearNodes()

        '' Enable, for using the LIGHtNING-FAST .NET 5 File-Crawler-Version!
        'If chkUseNetEnumerator.Checked Then
        '    _fileCrawler.FileItemEnumerator = New LightningFastFileItemEnumerator()
        'End If

        Dim directoryTree = Await Task.Run(
            Async Function()

                AddHandler _fileCrawler.ProgressUpdate,
                    AddressOf FileCrawler_ProgressUpdate

                AddHandler _fileCrawler.TopLevelDirectoriesAvailable,
                    AddressOf FileCrawler_TopLevelDirectoriesAvailable

                Return Await _fileCrawler.GetFilesAsync()
            End Function)

        UpdateListView()
        UpdateStatusBar(directoryTree.RootNode, isDone:=True)

    End Function

    Private Sub UpdateStatusBar(directoryNode As FileEntryNode, Optional isDone As Boolean = False)
        TotalFileSize.Text = $"Total file size: {CType(directoryNode.Length, MemorySize)}"
        TotalFileCount.Text = $"Total file count: {directoryNode.FileCount:#,##0}"
        ElapsedTime.Text = $"{If(isDone, "Done after: ", "Elapsed time: ")}{_stopWatch.Elapsed:hh\:mm\:ss}"

        If _stopWatch.Elapsed - _lastUpdateTime > New TimeSpan(0, 0, 0, 0, 200) Then
            Dim itemsPerSecond = 5 * (directoryNode.FileCount - _lastItemCount)

            _lastUpdateTime = _stopWatch.Elapsed
            _itemsPerSecondCalculator.AddElement(itemsPerSecond)
            ItemsPerSecondProcessed.Text = $"Items per Second: {_itemsPerSecondCalculator.Avergage:#,##0}"
            _lastItemCount = directoryNode.FileCount
        End If
    End Sub

    Private Sub doubletsTreeView_NodeMouseDoubleClick(sender As Object, e As TreeNodeMouseClickEventArgs) Handles doubletsTreeView.NodeMouseDoubleClick
        PictureViewerForm.ShowPicture(New IO.FileInfo(DirectCast(e.Node, FileEntryTreeViewNode).FileEntry.Path))
    End Sub
End Class
