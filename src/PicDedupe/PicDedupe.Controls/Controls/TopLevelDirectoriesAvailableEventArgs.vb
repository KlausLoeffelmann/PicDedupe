Imports PicDedupe.Generic

Public Class TopLevelDirectoriesAvailableEventArgs
    Inherits EventArgs

    Public Sub New(rootNode As FileSystemInfoNode)
        Me.RootNode = rootNode
    End Sub

    Public ReadOnly Property RootNode As FileSystemInfoNode
End Class
