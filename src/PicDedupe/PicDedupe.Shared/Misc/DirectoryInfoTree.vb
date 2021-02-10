Imports System.IO

Public Class DirectoryInfoTree

    Private _lastNodeAdded As DirectoryInfoNode
    Private ReadOnly _nodeDirectorynameStack As New Stack(Of String)

    Public Sub New(rootDirectory As DirectoryInfo)
        RootNode = DirectoryInfoNode.CreateRootNode(rootDirectory)
    End Sub

    Public Function AddDirectory(directory As DirectoryInfo) As DirectoryInfoNode

        If _lastNodeAdded Is Nothing Then
            If RootNode.Directory.FullName = directory.Parent.FullName Then
                _lastNodeAdded = RootNode.AddDirectory(directory, Nothing)
                Return _lastNodeAdded
            End If
        End If

        If _lastNodeAdded.ParentNode.Directory.FullName = directory.Parent.FullName Then
            _lastNodeAdded = _lastNodeAdded.ParentNode.AddDirectory(directory, _lastNodeAdded.TopLevelNode)
            Return _lastNodeAdded
        Else
            ' Find Root and store the Nodes up the Hirachy on the way
            Dim currentDirectory = directory
            Do While currentDirectory.Parent IsNot Nothing AndAlso currentDirectory.FullName <> RootNode.Directory.FullName
                currentDirectory = currentDirectory.Parent
                _nodeDirectorynameStack.Push(currentDirectory.Name)
            Loop

            If currentDirectory.Parent Is Nothing Then
                Throw New ArgumentException($"Directory does not match the root path: {directory.FullName}")
            End If

            Dim currentNode = RootNode
            Dim newCurrentNode As DirectoryInfoNode = Nothing
            Dim topLevelNode As DirectoryInfoNode = Nothing

            Dim pathPart As String
            Do While _nodeDirectorynameStack.Count > 0
                pathPart = _nodeDirectorynameStack.Pop
                If currentNode.TryGetNode(pathPart, newCurrentNode) Then
                    currentNode = newCurrentNode
                    If topLevelNode Is Nothing Then
                        topLevelNode = currentNode
                    End If
                Else
                    Throw New ArgumentException($"Directory does not match the root path: {directory.FullName}")
                End If
            Loop

            Return currentNode.AddDirectory(directory, topLevelNode)
        End If
    End Function

    Public ReadOnly Property RootNode As DirectoryInfoNode
End Class

Public Class DirectoryInfoNode
    Private ReadOnly _directories As New Dictionary(Of String, DirectoryInfoNode)
    Private ReadOnly _files As New Dictionary(Of String, FileInfo)
    Private ReadOnly _nodes As New List(Of DirectoryInfoNode)
    Private ReadOnly _directory As DirectoryInfo
    Private _fileCount As Integer
    Private _fileSize As ULong
    Private _parentNode As DirectoryInfoNode
    Private _topLevelNode As DirectoryInfoNode

    Private Sub New(directory As DirectoryInfo)
        _directory = directory
        _directories.Add(directory.Name, Me)
    End Sub

    Public Shared Function CreateRootNode(directory As DirectoryInfo) As DirectoryInfoNode
        Return New DirectoryInfoNode(directory)
    End Function

    Public Function AddDirectory(directory As DirectoryInfo, toplevelNode As DirectoryInfoNode) As DirectoryInfoNode
        If directory Is Nothing Then
            Throw New ArgumentNullException(NameOf(directory))
        End If

        If directory.Parent.FullName <> _directory.FullName Then
            Throw New ArgumentException($"Directory's Root does not match Parent's directory's root: {directory}")
        End If

        Dim node = New DirectoryInfoNode(directory) With {
            ._parentNode = Me
        }
        node.TopLevelNode = toplevelNode
        _nodes.Add(node)
        _directories.Add(directory.Name, node)
        Return node
    End Function

    Public Sub AddFile(file As FileInfo)
        If file Is Nothing Then
            Throw New ArgumentNullException(NameOf(file))
        End If

        If file.Directory.FullName <> _directory.FullName Then
            Throw New ArgumentException($"Files's path does not match nodes's directory's path: {file}")
        End If

        _files.Add(file.Name, file)
        UpdateCount()
        UpdateSize(CULng(file.Length))
    End Sub

    Private Sub UpdateCount()
        _fileCount += 1
        ParentNode?.UpdateCount()
    End Sub

    Private Sub UpdateSize(size As ULong)
        _fileSize += size
        ParentNode?.UpdateSize(size)
    End Sub

    Friend Function GetFolderListViewItems() As IEnumerable(Of FolderListViewItem)

        Dim listViewItems = New List(Of FolderListViewItem)

        For Each nodeItem In Nodes
            listViewItems.Add(New FolderListViewItem(nodeItem, nodeItem.FileCount, nodeItem.FileSize))
        Next

        Return listViewItems
    End Function

    Public Function TryGetNode(directoryName As String, ByRef node As DirectoryInfoNode) As Boolean
        Return _directories.TryGetValue(directoryName, node)
    End Function

    Public ReadOnly Property FileCount As Integer
        Get
            Return _fileCount
        End Get
    End Property

    Public ReadOnly Property FileSize As MemorySize
        Get
            Return _fileSize
        End Get
    End Property

    Public ReadOnly Property Directory As DirectoryInfo
        Get
            Return _directory
        End Get
    End Property

    Public ReadOnly Property ParentNode As DirectoryInfoNode
        Get
            Return _parentNode
        End Get
    End Property

    Public ReadOnly Property Files As IReadOnlyList(Of FileInfo)
        Get
            Return _files.Values.ToList()
        End Get
    End Property

    Public ReadOnly Property Nodes As List(Of DirectoryInfoNode)
        Get
            Return _nodes
        End Get
    End Property

    Public Property TopLevelNode As DirectoryInfoNode
        Get
            Return _topLevelNode
        End Get
        Private Set(value As DirectoryInfoNode)
            _topLevelNode = value
        End Set
    End Property

End Class