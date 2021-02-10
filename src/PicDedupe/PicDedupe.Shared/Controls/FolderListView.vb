Imports System.IO
Imports System.Windows.Forms

Public Class FolderListView
    Inherits ListView

    Public Sub New()
        DoubleBuffered = True
        View = View.Details
        FullRowSelect = True
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

    Public Sub New(node As DirectoryInfoNode, fileCount As Integer, size As MemorySize)
        MyBase.New(node.Directory.Name)
        Dim subitem = SubItems.Add(s_fileCountFormatter(fileCount)) ' File Count
        subitem = SubItems.Add($"{size}") ' Folder Size
        _node = node
    End Sub

    Public ReadOnly Property Node As DirectoryInfoNode
        Get
            Return _node
        End Get
    End Property
End Class
