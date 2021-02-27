Imports System.Text
Imports PicDedupe.Generic

Public Class FileEntryTreeView
    Inherits TreeView

    Private ReadOnly _doubletNodes As New Dictionary(Of Long, FileEntryTreeViewNode)

    Friend WithEvents _doublettenContextMenu As ContextMenuStrip
    Friend WithEvents _tsmCopyFilenameToClipboard As ToolStripMenuItem
    Friend WithEvents _tsmCreateDeleteBatchInClipboard As ToolStripMenuItem
    Friend WithEvents _tsmCreateCopyBatchInClipboard As ToolStripMenuItem

    Public Event RequestSetting(sender As Object, eventArgs As SettingsEventArgs)
    Public Event WriteSetting(sender As Object, eventArgs As SettingsEventArgs)

    Private Sub CopyFilenameToClipboard_Click(sender As Object, e As EventArgs)
        'TODO: Let's implement this later. (PRs welcome! :-) )
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
        Dim settingsEventArgs = New SettingsEventArgs("LastMoveDoubletsToPath")

        ' This is one of the easiest way to request a Setting inside a 
        ' Class Library from the Main app. A more sophisticated way would be, 
        ' to inject the Settings class or come up with a custom 
        ' Settings Reader/Writer. For this purpose, it suffices.
        ' Bing for: https://tinyurl.com/42bjajz7

        RaiseEvent RequestSetting(Me, settingsEventArgs)

        If Not String.IsNullOrWhiteSpace(settingsEventArgs.Value?.ToString) Then
            folderDialog.SelectedPath = settingsEventArgs.Value?.ToString
        End If

        Dim dialogResult = folderDialog.ShowDialog
        If dialogResult <> DialogResult.OK Then
            Return
        End If

        settingsEventArgs.Value = folderDialog.SelectedPath
        RaiseEvent WriteSetting(Me, settingsEventArgs)

        Dim stringBuilder = New StringBuilder

        With stringBuilder
            .AppendLine($"set ""DestPath={folderDialog.SelectedPath}""")
            .AppendLine()
            For Each node As FileEntryTreeViewNode In MyBase.Nodes
                For Each innerNode As FileEntryTreeViewNode In node.Nodes
                    .AppendLine($"move ""{innerNode.FileEntry.Path}"" ""%DestPath%""")
                Next
                .AppendLine()
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

    Public Sub AddDoublet(doublet As FileEntry)

        ' Let's get the file this is the doublet to.
        Dim parentNode = doublet.LinkedTo

        ' We have no treenode yet, so...
        If parentNode.Tag Is Nothing Then
            Dim parentTreeNode = New FileEntryTreeViewNode(parentNode)
            parentNode.Tag = parentTreeNode
            parentTreeNode.Nodes.Add(New FileEntryTreeViewNode(doublet))
            MyBase.Nodes.Add(parentTreeNode)
            Return
        Else
            ' Let's find the top of the chain...
            Do While (parentNode.LinkedTo IsNot Nothing)
                parentNode = parentNode.LinkedTo
            Loop

            '...this is the reference to our top treeview node, where we can add the doublet we just found.
            DirectCast(parentNode.Tag, FileEntryTreeViewNode).Nodes.Add(New FileEntryTreeViewNode(doublet))
        End If
    End Sub

    ' We're blocking complete write access to that, so that only AddDoublet and RemoveDoublet
    ' can be used to maintain the Nodes under the covers.
    Shadows ReadOnly Property Nodes() As List(Of FileEntryTreeViewNode)
        Get
            Return MyBase.Nodes.Cast(Of FileEntryTreeViewNode).ToList
        End Get
    End Property

    Public Sub ClearNodes()
        Nodes.Clear()
    End Sub

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
