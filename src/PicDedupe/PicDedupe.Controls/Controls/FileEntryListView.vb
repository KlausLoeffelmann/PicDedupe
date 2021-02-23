Imports System.IO
Imports System.Windows.Forms

Public Class FileEntryListView
    Inherits ListView

    Private _imageList As ImageList = New ImageList

    Public Event FolderListViewItemDoubleClick(sender As Object, e As FileEntryListViewItemDoubleClickEventArgs)

    Public Sub New()
        DoubleBuffered = True
        View = View.Details
        FullRowSelect = True

        _imageList.ImageSize = New Size(24, 24)
        _imageList.Images.Add("Folder", My.Resources.folder_Closed_32xLG)
        _imageList.Images.Add("Doc", My.Resources.document_32xLG)
        SmallImageList = _imageList

        SetupHeader()
    End Sub

    Private Sub SetupHeader()
        Me.Columns.Clear()
        Me.Columns.Add("Foldername", 200, HorizontalAlignment.Left)
        Me.Columns.Add("Files", 100, HorizontalAlignment.Right)
        Me.Columns.Add("Size", 200, HorizontalAlignment.Right)
    End Sub

    Protected Overrides Sub OnMouseDoubleClick(e As MouseEventArgs)
        MyBase.OnMouseDoubleClick(e)
        Dim hitTestInfo = HitTest(e.Location)
        If hitTestInfo IsNot Nothing Then
            RaiseEvent FolderListViewItemDoubleClick(
                Me,
                New FileEntryListViewItemDoubleClickEventArgs(
                    DirectCast(hitTestInfo.Item, FileEntryListViewItem)))
        End If
    End Sub
End Class
