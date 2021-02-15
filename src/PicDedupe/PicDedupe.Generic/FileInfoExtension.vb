Imports System.IO

Public Interface IFileItemEnumerator
    Function EnumerateDirectoriesRecursively(rootPath As String, Optional excludeAttributes As FileAttributes = Nothing) As IEnumerable(Of String)
    Function EnumerateDirectoryFiles(path As String, Optional excludeAttributes As FileAttributes = Nothing) As IEnumerable(Of String)
End Interface

Public Class FileItemEnumerator
    Implements IFileItemEnumerator

    Public Iterator Function EnumerateDirectoriesRecursively(rootPath As String, Optional excludeAttributes As FileAttributes = 0) As IEnumerable(Of String) Implements IFileItemEnumerator.EnumerateDirectoriesRecursively

        'We could as well build a queue of IEnumerable just with
        'the root path as a base. But that's a visual confusing thing,
        'because in the ListView the elements get analyzed not one by one, top-->down
        'but in a cycle (top-->down, top-->down, ...).

        'When we first get and return the root elements, and then iterate
        'through the root elements one-by-one explicitly, the visual
        'experience for the user is MUCh better, although we allocate
        'somewhat more space due to the iterator.
        Dim topLevelDirectories As IEnumerable(Of String) = Nothing

        Try
            topLevelDirectories = New DirectoryInfo(rootPath).
                EnumerateDirectories().
                Where(Function(dirItem) Not dirItem.Attributes.HasFlag(excludeAttributes)).
                Select(Function(dirItem) dirItem.FullName)
        Catch ex As Exception
        End Try

        For Each item In topLevelDirectories
            Yield item
        Next

        Dim queue As EnumerableQueue(Of String) = Nothing

        ' This is the delegate which gets called on dequeueing each item.
        ' It practically fills up the queue with the SubItems from that item.
        ' (If there are any).
        Dim getSubFolder = New Action(Of String)(
            Sub(item)
                Dim newDirectories As IEnumerable(Of String) = Nothing

                Try
                    newDirectories = New DirectoryInfo(item).
                        EnumerateDirectories().
                        Where(Function(dirItem) Not dirItem.Attributes.HasFlag(excludeAttributes)).
                        Select(Function(dirItem) dirItem.FullName)

                Catch ex As Exception
                    Throw
                    'We swallow those.
                End Try

                If newDirectories?.FirstOrDefault IsNot Nothing Then
                    queue.Queue(newDirectories)
                End If
            End Sub)

        For Each item In topLevelDirectories
            queue = New EnumerableQueue(Of String)(getSubFolder)
            getSubFolder(item)
            For Each item2 In queue
                Yield (item2)
            Next
        Next
    End Function

    Public Function EnumerateDirectoryFiles(path As String, Optional excludeAttributes As FileAttributes = 0) As IEnumerable(Of String) Implements IFileItemEnumerator.EnumerateDirectoryFiles
        Throw New NotImplementedException()
    End Function
End Class
