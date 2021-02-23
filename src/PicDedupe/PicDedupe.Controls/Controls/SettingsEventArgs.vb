Public Class SettingsEventArgs
    Inherits EventArgs

    Sub New(key As String)
        Me.Key = key
    End Sub

    Sub New(key As String, value As Object)
        Me.Key = key
        Me.Value = value
    End Sub

    Public ReadOnly Property Key As String
    Public Property Value As Object

End Class
