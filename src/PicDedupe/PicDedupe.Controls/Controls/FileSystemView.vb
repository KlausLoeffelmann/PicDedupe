Imports System.IO
Imports System.Windows.Forms

Public Class FileSystemView
    Inherits ListView

    Public Event FolderListViewItemDoubleClick(sender As Object, e As FolderListViewItemDoubleClickEventArgs)

    Public Sub New()
        DoubleBuffered = True
        View = View.Details
        FullRowSelect = True

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
                New FolderListViewItemDoubleClickEventArgs(
                    DirectCast(hitTestInfo.Item, FileSystemListViewItem)))
        End If
    End Sub
End Class
