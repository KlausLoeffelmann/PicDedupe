Public Class FormMain

    Private _withoutEvents As Integer = 5
    Private WithEvents _fileCrawler As FileCrawler

    Private Async Sub PathPicker1_PathChanged(sender As Object, e As PathChangedEventArgs) Handles PathPicker1.PathChanged
        Await UpdatePathView(e.Path)
    End Sub

    Private Async Function UpdatePathView(path As String) As Task
        Dim files = Await Task.Run(
            Function()
                _fileCrawler = New FileCrawler(New IO.DirectoryInfo(path))
                Return _fileCrawler.GetFilesRecursively()
            End Function)

    End Function

    Private Sub PathPicker1_Load(sender As Object, e As EventArgs) Handles PathPicker1.Load
        _withoutEvents += 10
        Debug.Print(_withoutEvents.ToString)
    End Sub
End Class
