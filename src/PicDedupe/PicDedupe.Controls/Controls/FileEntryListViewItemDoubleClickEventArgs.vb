Public Class FileEntryListViewItemDoubleClickEventArgs
    Inherits EventArgs

    Sub New(item As FileEntryListViewItem)
        Me.Item = item
    End Sub

    Public ReadOnly Property Item As FileEntryListViewItem
End Class
