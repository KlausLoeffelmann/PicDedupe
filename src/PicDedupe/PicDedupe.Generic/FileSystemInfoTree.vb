Imports System.IO

Public Class FileSystemInfoTree

    Private _lastNodeAdded As FileSystemInfoNode
    Private ReadOnly _nodeDirectorynameStack As New Stack(Of String)

    Public Sub New(rootDirectory As DirectoryInfo)
        RootNode = FileSystemInfoNode.CreateRootNode(rootDirectory)
    End Sub

    Public Function AddDirectory(directory As DirectoryInfo) As FileSystemInfoNode

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

            ' We don't need to look for the root name in the root name,
            ' we start the search one level below.
            _nodeDirectorynameStack.Pop()

            If currentDirectory.FullName <> RootNode.Directory.FullName AndAlso currentDirectory.Parent Is Nothing Then
                Throw New ArgumentException($"Directory does not match the root path: {directory.FullName}")
            End If

            Dim currentNode = RootNode
            Dim newCurrentNode As FileSystemInfoNode = Nothing
            Dim topLevelNode As FileSystemInfoNode = Nothing

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

    Public ReadOnly Property RootNode As FileSystemInfoNode
End Class

Public Class FileSystemInfoNode
    Private ReadOnly _fileSystemItems As New Dictionary(Of String, FileSystemInfoNode)
    Private ReadOnly _nodes As New List(Of FileSystemInfoNode)
    Private ReadOnly _item As FileSystemInfo
    Private _fileCount As Integer
    Private _fileSize As ULong
    Private _parentNode As FileSystemInfoNode
    Private _topLevelNode As FileSystemInfoNode

    Private Sub New(item As FileSystemInfo)
        _item = item
        IsDirectory = TypeOf item Is DirectoryInfo
    End Sub

    Public Shared Function CreateRootNode(directory As DirectoryInfo) As FileSystemInfoNode
        Return New FileSystemInfoNode(directory)
    End Function

    Public Function AddDirectory(directory As DirectoryInfo, toplevelNode As FileSystemInfoNode) As FileSystemInfoNode
        If directory Is Nothing Then
            Throw New ArgumentNullException(NameOf(directory))
        End If

        If directory.Parent.FullName <> _item.FullName Then
            Throw New ArgumentException($"Directory's Root does not match Parent's directory's root: {directory}")
        End If

        Dim node = New FileSystemInfoNode(directory) With
        {
            ._parentNode = Me
        }
        node.TopLevelNode = toplevelNode
        _nodes.Add(node)
        Try
            _fileSystemItems.Add(directory.Name, node)
        Catch ex As Exception

        End Try
        Return node
    End Function

    Public Sub AddFile(file As FileInfo)
        If file Is Nothing Then
            Throw New ArgumentNullException(NameOf(file))
        End If

        If file.Directory.FullName <> _item.FullName Then
            Throw New ArgumentException($"Files's path does not match nodes's directory's path: {file}")
        End If

        Dim node = New FileSystemInfoNode(file) With
        {
            ._parentNode = Me
        }

        _fileSystemItems.Add(file.Name, node)
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

    Public Function TryGetNode(directoryName As String, ByRef node As FileSystemInfoNode) As Boolean
        Return _fileSystemItems.TryGetValue(directoryName, node)
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

    Public ReadOnly Property IsDirectory As Boolean

    Public ReadOnly Property Directory As DirectoryInfo
        Get
            Return If(
                IsDirectory,
                DirectCast(_item, DirectoryInfo),
                Nothing)
        End Get
    End Property

    Public ReadOnly Property ParentNode As FileSystemInfoNode
        Get
            Return _parentNode
        End Get
    End Property

    Public ReadOnly Property Files As IReadOnlyList(Of FileInfo)
        Get
            Return _fileSystemItems.Values.OfType(Of FileInfo).ToList()
        End Get
    End Property

    Public ReadOnly Property Nodes As List(Of FileSystemInfoNode)
        Get
            Return _nodes
        End Get
    End Property

    Public Property TopLevelNode As FileSystemInfoNode
        Get
            Return _topLevelNode
        End Get
        Private Set(value As FileSystemInfoNode)
            _topLevelNode = value
        End Set
    End Property

End Class