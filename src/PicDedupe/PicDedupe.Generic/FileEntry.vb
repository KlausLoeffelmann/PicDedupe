Public Class FileEntry
    Public Sub New(path As String, isDirectory As Boolean,
                   Optional length As Long = 0)

        Me.Path = path
        Me.IsDirectory = isDirectory
        Me.Length = length
    End Sub

    Public ReadOnly Property Path As String

    Public Property Length As Long ' Can't be readonly, since the Length gets be accumulated while scanning folders.

    Public ReadOnly Property IsDirectory As Boolean

    Public Overrides Function ToString() As String
        'This is really useful for debugging purposes!
        Return $"{If(IsDirectory, "D", "F")}: {Path} - {CType(Length, MemorySize)}"
    End Function
End Class
