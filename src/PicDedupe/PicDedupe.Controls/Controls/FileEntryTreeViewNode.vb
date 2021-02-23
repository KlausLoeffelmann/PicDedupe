Imports PicDedupe.Generic

Public Class FileEntryTreeViewNode
    Inherits TreeNode

    Public Sub New(fileEntry As FileEntry)
        MyBase.New(fileEntry.Path)
        Me.FileEntry = fileEntry
    End Sub

    Public ReadOnly Property FileEntry As FileEntry

    Public ReadOnly IsHeader As Boolean

End Class
