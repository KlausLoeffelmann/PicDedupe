Imports System.IO
Imports IoPath = System.IO.Path

Public Class FileSystemInfoTree

    Private _lastNodeAdded As FileSystemInfoNode

    Public Sub New(rootPath As String)
        RootNode = FileSystemInfoNode.CreateRootNode(rootPath)
    End Sub

    Public Function AddDirectory(path As String) As FileSystemInfoNode
        If Not path.StartsWith(RootNode.Path) Then
            Throw New ArgumentException($"Root of 'path' does not match the root node's path: {path}")
        Else
            Dim remainingPathName = path.Substring(RootNode.Path.Length + 1)
            Dim remainingPaths = remainingPathName.Split(IoPath.DirectorySeparatorChar)
            Dim searchNode = RootNode
            For i = 0 To remainingPaths.Length - 2
                If (Not searchNode.TryGetNode(remainingPaths(i), searchNode)) Then
                    Throw New ArgumentException($"Couldn't find one of the ancestor nodes in the node structore: {path}")
                End If
            Next
            Return searchNode.AddDirectory(path)
        End If
    End Function

    Public ReadOnly Property RootNode As FileSystemInfoNode
End Class

Public Class FileSystemInfoNode
    Private ReadOnly _nodes As New Dictionary(Of String, FileSystemInfoNode)
    Private _fileCount As Integer
    Private _fileSize As ULong
    Private _parentNode As FileSystemInfoNode
    Private _topLevelNode As FileSystemInfoNode
    Private _path As String

    Private Sub New(fullpath As String, isDirectory As Boolean, Optional isTopLevelNode As Boolean = False)
        _path = fullpath
        Me.IsDirectory = isDirectory

        If isTopLevelNode Then
            TopLevelNode = Me
        End If
    End Sub

    Public Shared Function CreateRootNode(directoryPath As String) As FileSystemInfoNode
        Return New FileSystemInfoNode(directoryPath, True, True)
    End Function

    Public Function AddDirectory(path As String) As FileSystemInfoNode
        If path Is Nothing Then
            Throw New ArgumentNullException(NameOf(path))
        End If

        If IoPath.GetDirectoryName(path) <> _path Then
            Throw New ArgumentException($"Directory's Root does not match Parent's directory's root: {path}")
        End If

        Dim node = New FileSystemInfoNode(path, True) With
        {
            ._parentNode = Me
        }

        node.TopLevelNode = TopLevelNode
        ' We want just the name of the Directory, not the full path.
        ' So using GetFileName for this is just fine.
        _nodes.Add(IoPath.GetFileName(path), node)

        Return node
    End Function

    Public Sub AddFile(path As String, fileSize As Long)
        If path Is Nothing Then
            Throw New ArgumentNullException(NameOf(path))
        End If

        If IO.Path.GetDirectoryName(path) <> _path Then
            Throw New ArgumentException($"Files's path does not match nodes's directory's path: {path}")
        End If

        Dim node = New FileSystemInfoNode(path, False) With
        {
            ._parentNode = Me
        }

        _nodes.Add(IO.Path.GetFileName(path), node)
        UpdateCount()
        UpdateSize(CULng(fileSize))
    End Sub

    Private Sub UpdateCount()
        _fileCount += 1
        ParentNode?.UpdateCount()
    End Sub

    Private Sub UpdateSize(size As ULong)
        _fileSize += size
        ParentNode?.UpdateSize(size)
    End Sub

    Public Function TryGetNode(path As String, ByRef node As FileSystemInfoNode) As Boolean
        Return _nodes.TryGetValue(path, node)
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

    Public ReadOnly Property ParentNode As FileSystemInfoNode
        Get
            Return _parentNode
        End Get
    End Property

    Public ReadOnly Property Path As String
        Get
            Return _path
        End Get
    End Property

    Public ReadOnly Property Name As String
        Get
            Return IoPath.GetFileName(Path)
        End Get
    End Property

    Public ReadOnly Property Nodes As Dictionary(Of String, FileSystemInfoNode)
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