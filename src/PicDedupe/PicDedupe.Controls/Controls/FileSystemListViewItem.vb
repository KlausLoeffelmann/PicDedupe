Imports PicDedupe.Generic

Public Class FileSystemListViewItem
    Inherits ListViewItem

    Private Shared s_fileCountFormatStrings As (valueFormat As String, nullFormat As String) =
        ("#,##0", "- - -")

    Private Shared ReadOnly s_fileCountFormatter As Func(Of Integer?, String) =
        Function(value) If(
            value?.ToString(s_fileCountFormatStrings.valueFormat),
            s_fileCountFormatStrings.nullFormat)

    Private ReadOnly _node As FileSystemInfoNode

    Public Sub New(node As FileSystemInfoNode)
        MyBase.New(node.Directory.Name)
        Dim subitem = SubItems.Add(s_fileCountFormatter(node.FileCount)) ' File Count
        subitem = SubItems.Add($"{node.FileSize}") ' Folder Size
        _node = node
    End Sub

    Public Sub UpdateItem()
        SubItems(1).Text = s_fileCountFormatter(Node.FileCount)
        SubItems(2).Text = $"{Node.FileSize}"
    End Sub

    Public ReadOnly Property Node As FileSystemInfoNode
        Get
            Return _node
        End Get
    End Property
End Class
