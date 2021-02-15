Public Class FileEntry
    Public Sub New(path As String, isDirectory As Boolean,
                   Optional length As Long = 0)

        Me.Path = path
        Me.IsDirectory = isDirectory
        Me.Length = length
    End Sub

    Public ReadOnly Property Path As String

    Public Length As Long ' Can't be readonly, since the Length gets be accumulated while scanning folders.

    Public ReadOnly Property IsDirectory As Boolean
End Class
