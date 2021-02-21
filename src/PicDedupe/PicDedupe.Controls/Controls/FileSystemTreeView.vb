Imports System.Collections.Immutable
Imports PicDedupe.Generic

Public Class FileSystemTreeView
    Inherits TreeView

    Private _doubletNodes As New Dictionary(Of Long, FileSystemTreeViewNode)

    Sub New()
        MyBase.New
        'DrawMode = TreeViewDrawMode.OwnerDrawText
    End Sub

    Protected Overrides Sub OnDrawNode(e As DrawTreeNodeEventArgs)

        Dim node = DirectCast(e.Node, FileSystemTreeViewNode)

        With e.Graphics
            e.DrawDefault = True
        End With

    End Sub

    Public Sub AddDoublet(doublet As FileEntry, fileComparedAgainst As FileEntry)

        Dim doubletNode As FileSystemTreeViewNode = Nothing

        Dim newNode = New FileSystemTreeViewNode(doublet)

        If _doubletNodes.TryGetValue(doublet.Length, doubletNode) Then
            doubletNode.Nodes.Add(newNode)
        Else
            Dim originalFileNode = New FileSystemTreeViewNode(fileComparedAgainst)
            MyBase.Nodes.Add(originalFileNode)
            originalFileNode.Expand()
            originalFileNode.Nodes.Add(newNode)
            _doubletNodes.Add(doublet.Length, originalFileNode)
        End If
    End Sub

    Public Function GetDoublets() As ImmutableList(Of FileEntry)
        Return (From nodesItem In _doubletNodes
                Where nodesItem.Value.Nodes.Count > 0
                From fileNodeItem In nodesItem.Value.Nodes.Cast(Of FileSystemTreeViewNode)
                Select fileNodeItem.FileEntry).ToImmutableList
    End Function

    ' We're blocking complete write access to that, so that only AddDoublet and RemoveDoublet
    ' can be used to maintain the Nodes under the covers.
    Shadows ReadOnly Property Nodes() As ImmutableList(Of FileSystemTreeViewNode)
        Get
            Return MyBase.Nodes.Cast(Of FileSystemTreeViewNode).ToImmutableList
        End Get
    End Property

End Class

Public Class FileSystemTreeViewNode
    Inherits TreeNode

    Public Sub New(fileEntry As FileEntry)
        MyBase.New(fileEntry.Path)
        Me.FileEntry = fileEntry
    End Sub

    Public ReadOnly Property FileEntry As FileEntry

    Public ReadOnly IsHeader As Boolean

End Class
