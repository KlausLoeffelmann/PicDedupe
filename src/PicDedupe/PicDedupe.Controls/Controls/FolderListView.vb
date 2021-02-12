Imports System.IO
Imports System.Windows.Forms
Imports PicDedupe.Generic

Public Class FolderListView
    Inherits ListView

    Public Sub New()
        DoubleBuffered = True
        View = View.Details
        FullRowSelect = True

        SetupHeader()
    End Sub

    Private Sub SetupHeader()
        Me.Columns.Clear()
        Me.Columns.Add("Foldername", 300, HorizontalAlignment.Left)
        Me.Columns.Add("Files", 150, HorizontalAlignment.Right)
        Me.Columns.Add("Size", 300, HorizontalAlignment.Right)
    End Sub

End Class

Public Class FolderListViewItem
    Inherits ListViewItem

    Private Shared s_fileCountFormatStrings As (valueFormat As String, nullFormat As String) =
        ("#,##0", "- - -")

    Private Shared ReadOnly s_fileCountFormatter As Func(Of Integer?, String) =
        Function(value) If(
            value?.ToString(s_fileCountFormatStrings.valueFormat),
            s_fileCountFormatStrings.nullFormat)

    Private ReadOnly _node As DirectoryInfoNode

    Public Sub New(node As DirectoryInfoNode)
        MyBase.New(node.Directory.Name)
        Dim subitem = SubItems.Add(s_fileCountFormatter(node.FileCount)) ' File Count
        subitem = SubItems.Add($"{node.FileSize}") ' Folder Size
        _node = node
    End Sub

    Public Sub UpdateItem()
        SubItems(1).Text = s_fileCountFormatter(Node.FileCount)
        SubItems(2).Text = $"{Node.FileSize}"
    End Sub

    Public ReadOnly Property Node As DirectoryInfoNode
        Get
            Return _node
        End Get
    End Property
End Class
