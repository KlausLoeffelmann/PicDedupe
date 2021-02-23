Imports System.Collections.Immutable
Imports System.ComponentModel
Imports System.Text
Imports PicDedupe.Generic

Public Class FileEntryTreeView
    Inherits TreeView

    Private _doubletNodes As New Dictionary(Of Long, FileEntryTreeViewNode)

    Friend WithEvents _doublettenContextMenu As ContextMenuStrip
    Friend WithEvents _tsmCopyFilenameToClipboard As ToolStripMenuItem
    Friend WithEvents _tsmCreateDeleteBatchInClipboard As ToolStripMenuItem
    Friend WithEvents _tsmCreateCopyBatchInClipboard As ToolStripMenuItem

    Private Sub CopyFilenameToClipboard_Click(sender As Object, e As EventArgs)
    End Sub

    Private Sub CreateMoveBatchInClipboard_Click(sender As Object, e As EventArgs)

        ' Let's get where the doublets should go.
        Dim folderDialog = New FolderBrowserDialog() With
        {
            .Description = "Pick the folder to move the doublets to:"
        }

#If NET5_0_OR_GREATER Then
        folderDialog.UseDescriptionForTitle = True
#End If

        Dim dialogResult = folderDialog.ShowDialog
        If dialogResult <> DialogResult.OK Then
            Return
        End If

        Dim files = GetDoublets()
        Dim stringBuilder = New StringBuilder
        With stringBuilder
            .AppendLine($"set ""DestPath={folderDialog.SelectedPath}""")
            .AppendLine()
            For Each fileItem In files
                .AppendLine($"move ""{fileItem.Path}"" ""%DestPath%""")
            Next
        End With
        Clipboard.SetText(stringBuilder.ToString)
    End Sub

    Protected Overrides Sub OnHandleCreated(e As EventArgs)
        MyBase.OnHandleCreated(e)
        InitializeComponents()

        AddHandler _tsmCopyFilenameToClipboard.Click, AddressOf CopyFilenameToClipboard_Click
        AddHandler _tsmCreateCopyBatchInClipboard.Click, AddressOf CreateMoveBatchInClipboard_Click

    End Sub

    Protected Overrides Sub OnDrawNode(e As DrawTreeNodeEventArgs)

        Dim node = DirectCast(e.Node, FileEntryTreeViewNode)

        With e.Graphics
            e.DrawDefault = True
        End With

    End Sub

    Public Sub AddDoublet(doublet As FileEntry, fileComparedAgainst As FileEntry)

        Dim doubletNode As FileEntryTreeViewNode = Nothing

        Dim newNode = New FileEntryTreeViewNode(doublet)

        If _doubletNodes.TryGetValue(doublet.Length, doubletNode) Then
            doubletNode.Nodes.Add(newNode)
        Else
            Dim originalFileNode = New FileEntryTreeViewNode(fileComparedAgainst)
            MyBase.Nodes.Add(originalFileNode)
            originalFileNode.Expand()
            originalFileNode.Nodes.Add(newNode)
            _doubletNodes.Add(doublet.Length, originalFileNode)
        End If
    End Sub

    Public Function GetDoublets() As ImmutableList(Of FileEntry)
        Return (From nodesItem In _doubletNodes
                Where nodesItem.Value.Nodes.Count > 0
                From fileNodeItem In nodesItem.Value.Nodes.Cast(Of FileEntryTreeViewNode)
                Select fileNodeItem.FileEntry).ToImmutableList
    End Function

    ' We're blocking complete write access to that, so that only AddDoublet and RemoveDoublet
    ' can be used to maintain the Nodes under the covers.
    Shadows ReadOnly Property Nodes() As ImmutableList(Of FileEntryTreeViewNode)
        Get
            Return MyBase.Nodes.Cast(Of FileEntryTreeViewNode).ToImmutableList
        End Get
    End Property

    Private Sub InitializeComponents()
        Me._doublettenContextMenu = New System.Windows.Forms.ContextMenuStrip()
        Me._tsmCopyFilenameToClipboard = New System.Windows.Forms.ToolStripMenuItem()
        Me._tsmCreateDeleteBatchInClipboard = New System.Windows.Forms.ToolStripMenuItem()
        Me._tsmCreateCopyBatchInClipboard = New System.Windows.Forms.ToolStripMenuItem()
        Me._doublettenContextMenu.SuspendLayout()
        Me.ContextMenuStrip = Me._doublettenContextMenu
        '
        'DoublettenContextMenu
        '
        Me._doublettenContextMenu.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me._doublettenContextMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me._tsmCopyFilenameToClipboard, Me._tsmCreateDeleteBatchInClipboard, Me._tsmCreateCopyBatchInClipboard})
        Me._doublettenContextMenu.Name = "DoublettenContextMenu"
        Me._doublettenContextMenu.Size = New System.Drawing.Size(372, 76)
        '
        'tsmCopyFilenameToClipboard
        '
        Me._tsmCopyFilenameToClipboard.Name = "tsmCopyFilenameToClipboard"
        Me._tsmCopyFilenameToClipboard.Size = New System.Drawing.Size(371, 24)
        Me._tsmCopyFilenameToClipboard.Text = "Copy Filename to Clipboard"
        '
        'tsmCreateDeleteBatchInClipboard
        '
        Me._tsmCreateDeleteBatchInClipboard.Name = "tsmCreateDeleteBatchInClipboard"
        Me._tsmCreateDeleteBatchInClipboard.Size = New System.Drawing.Size(371, 24)
        Me._tsmCreateDeleteBatchInClipboard.Text = "Create Delete Batchfile content in Clipboard"
        '
        'tsmCreateCopyBatchInClipboard
        '
        Me._tsmCreateCopyBatchInClipboard.Name = "tsmCreateCopyBatchInClipboard"
        Me._tsmCreateCopyBatchInClipboard.Size = New System.Drawing.Size(371, 24)
        Me._tsmCreateCopyBatchInClipboard.Text = "Create Move Batchfile content in Clipboard..."
        _doublettenContextMenu.ResumeLayout(False)
    End Sub
End Class
