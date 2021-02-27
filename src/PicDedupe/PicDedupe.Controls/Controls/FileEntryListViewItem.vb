Imports PicDedupe.Generic

Public Class FileEntryListViewItem
    Inherits ListViewItem

    Private Shared s_fileCountFormatStrings As (valueFormat As String, nullFormat As String) =
        ("#,##0", "- - -")

    Private Shared ReadOnly s_fileCountFormatter As Func(Of Integer?, String) =
        Function(value) If(
            value?.ToString(s_fileCountFormatStrings.valueFormat),
            s_fileCountFormatStrings.nullFormat)

    Private ReadOnly _node As FileEntryNode

    Public Sub New(node As FileEntryNode)
        MyBase.New(node.Name, If(node.IsDirectory, "Folder", "Doc"))

        Dim subitem = SubItems.Add(s_fileCountFormatter(node.FileCount)) ' File Count
        subitem = SubItems.Add($"{CType(node.Length, MemorySize)}") ' Folder Size
        _node = node
    End Sub

    Public Sub UpdateItem()
        SubItems(1).Text = s_fileCountFormatter(Node.FileCount)
        SubItems(2).Text = $"{CType(Node.Length, MemorySize)}"
    End Sub

    Public ReadOnly Property Node As FileEntryNode
        Get
            Return _node
        End Get
    End Property
End Class
