Public Class PathPicker

    Private _dialogTitel As String
    Private _path As String

    Public Event PathChanged(sender As Object, e As PathChangedEventArgs)

    Private Sub BtnShowDialog_Click(sender As Object, e As EventArgs) Handles btnShowDialog.Click
        Dim folderBrowser = New FolderBrowserDialog() With
        {
            .Description = DialogTitel
        }

        If Not String.IsNullOrWhiteSpace(BrowserPath) Then
            folderBrowser.SelectedPath = BrowserPath
        End If

        Dim dialogResult = folderBrowser.ShowDialog()
        If dialogResult = DialogResult.OK Then
            Path = folderBrowser.SelectedPath
        End If
    End Sub

    Public Property DialogTitel As String
        Get
            Return _dialogTitel
        End Get
        Set(value As String)
            If Not Object.Equals(_dialogTitel, value) Then
                _dialogTitel = value
            End If
        End Set
    End Property

    Public Property BrowserPath As String

    Public Property Path As String
        Get
            Return _path
        End Get
        Set(value As String)
            If Not Object.Equals(_path, value) Then
                _path = value
                txtPath.Text = Path
                OnPathChanged(New PathChangedEventArgs(value))
            End If
        End Set
    End Property

    Protected Overridable Sub OnPathChanged(e As PathChangedEventArgs)
        RaiseEvent PathChanged(Me, e)
    End Sub
End Class
