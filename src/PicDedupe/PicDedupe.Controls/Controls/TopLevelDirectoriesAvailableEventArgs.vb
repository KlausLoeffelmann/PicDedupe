Imports PicDedupe.Generic

Public Class TopLevelDirectoriesAvailableEventArgs
    Inherits EventArgs

    Public Sub New(rootNode As FileEntryNode)
        Me.RootNode = rootNode
    End Sub

    Public ReadOnly Property RootNode As FileEntryNode
End Class
