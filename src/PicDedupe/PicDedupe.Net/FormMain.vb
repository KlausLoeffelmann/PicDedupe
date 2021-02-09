Public Class FormMain
    Private Sub PathPicker1_PathChanged(sender As Object, e As PathChangedEventArgs) Handles PathPicker1.PathChanged
        Dim fileCrawler As New FileCrawler(New IO.DirectoryInfo(e.Path))
        Dim allFiles = fileCrawler.GetFilesRecursively()
    End Sub

    Private Sub PathPicker1_Load(sender As Object, e As EventArgs) Handles PathPicker1.Load

    End Sub
End Class
