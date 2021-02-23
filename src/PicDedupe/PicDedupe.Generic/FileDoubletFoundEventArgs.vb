Imports System.ComponentModel

Public Class FileDoubletFoundEventArgs
    Inherits CancelEventArgs

    Sub New(fileFound As FileEntry)
        Me.FileFound = fileFound
    End Sub

    Public Property FileFound As FileEntry
End Class
