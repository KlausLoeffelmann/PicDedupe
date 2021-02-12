Public Class PathChangedEventArgs
    Inherits EventArgs

    Public Sub New(path As String)
        Me.Path = path
    End Sub

    Public Property Path As String
End Class