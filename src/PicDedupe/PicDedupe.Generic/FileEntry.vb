Imports System.IO
Imports System.Security.Cryptography

Public Class FileEntry

    Private _fileHash As Byte()

    Public Sub New(path As String, isDirectory As Boolean,
                   Optional length As Long = 0)

        Me.Path = path
        Me.IsDirectory = isDirectory
        Me.Length = length
    End Sub

    Public ReadOnly Property Path As String

    Public Property Length As Long ' Can't be readonly, since the Length gets be accumulated while scanning folders.

    Public ReadOnly Property IsDirectory As Boolean
    Public Property LinkedTo As FileEntry
    Public Property Tag As Object

    Public Async Function GetFileHashAsync() _
        As Task(Of Byte())

#If NET5_0_OR_GREATER Then
        If _fileHash Is Nothing Then
            Dim sha256Calc = SHA256.Create()
            Using stream = File.OpenRead(Me.Path)
                _fileHash = Await sha256Calc.ComputeHashAsync(stream)
            End Using
        End If
        Return _fileHash
#Else
        If _fileHash Is Nothing Then
            Dim sha256Calc = SHA256.Create()
            Using stream = File.OpenRead(Me.Path)
                _fileHash = sha256Calc.ComputeHash(stream)
            End Using
        End If
        Return Await Task.FromResult(_fileHash)
#End If
    End Function

    Public Overrides Function ToString() As String
        'This is really useful for debugging purposes!
        Return $"{If(IsDirectory, "D", "F")}: {Path} - {CType(Length, MemorySize)}"
    End Function
End Class
