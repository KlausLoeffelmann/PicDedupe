Public Class TopLevelDirectoriesAvailableEventArgs
    Inherits EventArgs

    Public Sub New(topLevelDirectories As IEnumerable(Of DirectoryInfoNode))
        Me.TopLevelDirectories = topLevelDirectories
    End Sub

    Public ReadOnly Property TopLevelDirectories As IEnumerable(Of DirectoryInfoNode)
End Class
