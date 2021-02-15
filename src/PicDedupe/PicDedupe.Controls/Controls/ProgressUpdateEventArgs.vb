Imports PicDedupe.Generic

Public Class ProgressUpdateEventArgs
    Inherits EventArgs

    Private Shared s_defaultEventArgs As ProgressUpdateEventArgs

    Public Sub New(nodeToUpdate As FileEntryNode)
        Me.NodeToUpdate = nodeToUpdate
    End Sub

    Public Shared Function GetDefault(nodeToUpdate As FileEntryNode) As ProgressUpdateEventArgs
        If s_defaultEventArgs Is Nothing Then
            s_defaultEventArgs = New ProgressUpdateEventArgs(nodeToUpdate)
        Else
            'Uhhhhh...In case you're asking where this comes from:
            'This is the compiler generated backing field
            'from the Auto Property.
            s_defaultEventArgs._NodeToUpdate = nodeToUpdate
        End If
        Return s_defaultEventArgs
    End Function

    Public ReadOnly Property NodeToUpdate As FileEntryNode

End Class
