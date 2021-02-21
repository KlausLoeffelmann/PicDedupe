Imports System.IO
Imports IoPath = System.IO.Path

Public Class FileEntryTree

    Private ReadOnly _lastNodeAddedTo As FileEntryNode
    Private _lastNodeAdded As FileEntryNode

    Public Sub New(rootPath As String)
        RootNode = FileEntryNode.CreateRootNode(rootPath)
        _lastNodeAddedTo = RootNode
    End Sub

    Public Function AddEntry(fileEntry As FileEntry) As FileEntryNode
        If fileEntry.IsDirectory Then
            Return AddDirectory(fileEntry.Path)
        Else
            Return AddFile(fileEntry.Path, fileEntry.Length)
        End If
    End Function

    Public Function AddDirectory(path As String) As FileEntryNode
        If Not path.StartsWith(RootNode.Path) Then
            Throw New ArgumentException($"Root of 'path' does not match the root node's path: {path}")
        Else
            Dim parentNode = FindParentNode(path)
            _lastNodeAdded = parentNode.AddDirectory(path)

            Return _lastNodeAdded
        End If
    End Function

    Public Function AddFile(path As String, length As Long) As FileEntryNode
        If Not path.StartsWith(RootNode.Path) Then
            Throw New ArgumentException($"Root of 'path' does not match the root node's path: {path}")
        Else
            Dim parentNode = FindParentNode(path)
            _lastNodeAdded = parentNode.AddFile(path, length)

            Return _lastNodeAdded
        End If
    End Function

    Private Function FindParentNode(path As String) As FileEntryNode
        If Not path.StartsWith(RootNode.Path) Then
            Throw New ArgumentException($"Root of 'path' does not match the root node's path: {path}")
        End If

        ' Special case root folder is drive (C:\).
        Dim directoryName = If(path.Length = 3, path, IoPath.GetDirectoryName(path))

        If directoryName = _lastNodeAddedTo.Path Then
            Return _lastNodeAddedTo
        End If

        If directoryName = _lastNodeAdded?.Path Then
            Return _lastNodeAdded
        End If

        ' Again, put special case root folder is drive into account.
        Dim rootNodePathLength = RootNode.Path.Length
        Dim remainingPathName = path.Substring(rootNodePathLength + If(rootNodePathLength = 3, 0, 1))
        Dim remainingPaths = remainingPathName.Split(IoPath.DirectorySeparatorChar)
        Dim searchNode = RootNode
        For i = 0 To remainingPaths.Length - 2
            If Not searchNode.TryGetNode(remainingPaths(i), searchNode) Then
                Throw New ArgumentException($"Couldn't find one of the ancestor nodes in the node structore: {path}")
            End If
        Next

        Return searchNode
    End Function

    Public ReadOnly Property RootNode As FileEntryNode
End Class
