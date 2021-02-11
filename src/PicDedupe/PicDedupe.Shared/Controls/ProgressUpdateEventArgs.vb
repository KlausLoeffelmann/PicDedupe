Public Class ProgressUpdateEventArgs
    Inherits EventArgs

    Public Sub New(nodeToUpdate As DirectoryInfoNode)
        Me.NodeToUpdate = nodeToUpdate
    End Sub

    Public ReadOnly Property NodeToUpdate As DirectoryInfoNode

End Class
