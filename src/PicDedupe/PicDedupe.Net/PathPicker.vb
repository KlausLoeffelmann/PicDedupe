Public Class PathPicker

    Private _dialogTitel As String

    Public Event PathChanged(sender As Object, e As PathChangedEventArgs)

    Private Sub BtnShowDialog_Click(sender As Object, e As EventArgs) Handles btnShowDialog.Click
        Dim folderBrowser = New FolderBrowserDialog() With
        {
            .Description = DialogTitel
        }

        Dim dialogResult = folderBrowser.ShowDialog()
        If dialogResult = DialogResult.OK Then
            RaiseEvent PathChanged(Me, New PathChangedEventArgs(folderBrowser.SelectedPath))
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
End Class

Public Class PathChangedEventArgs
    Inherits EventArgs

    Public Sub New(path As String)
        Me.Path = path
    End Sub

    Public Property Path As String

End Class