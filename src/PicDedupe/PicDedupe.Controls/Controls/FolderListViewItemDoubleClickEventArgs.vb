Public Class FolderListViewItemDoubleClickEventArgs
    Inherits EventArgs

    Sub New(item As FileSystemListViewItem)
        Me.Item = item
    End Sub

    Public ReadOnly Property Item As FileSystemListViewItem
End Class
