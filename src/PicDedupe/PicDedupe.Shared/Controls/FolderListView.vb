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

Public Class FileListViewItem
    Inherits ListViewItem

    Private Shared s_fileCountFormatStrings As (valueFormat As String, nullFormat As String) =
        ("#,##0", "- - -")

    Private Shared s_fileCountFormatter As Func(Of Integer?, String) =
        Function(value) If(
            value?.ToString(s_fileCountFormatStrings.valueFormat),
            s_fileCountFormatStrings.nullFormat)

    Private _directory As DirectoryInfo

    Public Sub New(directory As DirectoryInfo, fileCount As Integer, size As MemorySize)
        MyBase.New(directory.Name)
        Dim subitem = SubItems.Add(s_fileCountFormatter(fileCount)) ' File Count
        subitem = SubItems.Add($"{size}") ' Folder Size
    End Sub
End Class
