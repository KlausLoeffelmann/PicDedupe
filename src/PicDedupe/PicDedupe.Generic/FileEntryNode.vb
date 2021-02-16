Imports IoPath = System.IO.Path

Public Class FileEntryNode
    Inherits FileEntry

    ' The name alone can't work as key, because a folder and a file 
    ' can have the exact same name any given directory.
    ' So, we have a Tuple of (name, isDirecotry) as the unique key.
    Private ReadOnly _nodes As New Dictionary(Of (name As String, isDirectory As Boolean), FileEntryNode)
    Private _fileCount As Integer
    Private _parentNode As FileEntryNode
    Private _topLevelNode As FileEntryNode

    Private Sub New(path As String, isDirectory As Boolean,
                    Optional length As Long = 0, Optional isTopLevelNode As Boolean = False)
        MyBase.New(path, isDirectory, length)

        If isTopLevelNode Then
            TopLevelNode = Me
        End If
    End Sub

    Public Shared Function CreateRootNode(directoryPath As String) As FileEntryNode

        Return New FileEntryNode(
            directoryPath,
            isDirectory:=True,
            isTopLevelNode:=True)
    End Function

    Public Function AddDirectory(path As String) As FileEntryNode
        If path Is Nothing Then
            Throw New ArgumentNullException(NameOf(path))
        End If

        If IoPath.GetDirectoryName(path) <> Me.Path Then
            Throw New ArgumentException($"Directory's Root does not match Parent's directory's root: {path}")
        End If

        Dim node = New FileEntryNode(
            path,
            isDirectory:=True) With {._parentNode = Me}

        node.TopLevelNode = TopLevelNode

        ' We want just the name of the Directory, not the full path.
        ' So using GetFileName for this is just fine.
        _nodes.Add((IoPath.GetFileName(path), True), node)

        Return node
    End Function

    Public Function AddFile(path As String, length As Long) As FileEntryNode
        If path Is Nothing Then
            Throw New ArgumentNullException(NameOf(path))
        End If

        If IO.Path.GetDirectoryName(path) <> Me.Path Then
            Throw New ArgumentException($"Files's path does not match nodes's directory's path: {path}")
        End If

        Dim node = New FileEntryNode(
            path,
            isDirectory:=False) With {._parentNode = Me}

        _nodes.Add((IO.Path.GetFileName(path), False), node)
        UpdateCount()
        UpdateSize(length)

        Return node
    End Function

    Private Sub UpdateCount()
        _fileCount += 1
        ParentNode?.UpdateCount()
    End Sub

    Private Sub UpdateSize(size As Long)
        Length += size
        ParentNode?.UpdateSize(size)
    End Sub

    Public Function TryGetNode(path As String, ByRef node As FileEntryNode) As Boolean
        Return If(
            Not _nodes.TryGetValue((path, True), node), ' If we can't find the folder,
            _nodes.TryGetValue((path, False), node),    ' maybe we can find a file with the same name,
            True)                                       ' otherwise, we found it in the first attempt, and return true.
    End Function

    Public ReadOnly Property FileCount As Integer
        Get
            Return _fileCount
        End Get
    End Property

    Public ReadOnly Property ParentNode As FileEntryNode
        Get
            Return _parentNode
        End Get
    End Property

    Public ReadOnly Property Name As String
        Get
            Return IoPath.GetFileName(Path)
        End Get
    End Property

    Public ReadOnly Property Nodes As Dictionary(Of (name As String, isDirectory As Boolean), FileEntryNode)
        Get
            Return _nodes
        End Get
    End Property

    Public Property TopLevelNode As FileEntryNode
        Get
            Return _topLevelNode
        End Get

        Private Set(value As FileEntryNode)
            _topLevelNode = value
        End Set
    End Property
End Class
