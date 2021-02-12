Imports System.IO
Imports System.Runtime.CompilerServices
Imports PicDedupe.Generic

Public Module FileInfoExtension

    <Extension>
    Public Iterator Function EnumerateAllSubDirectories(directory As DirectoryInfo, Optional excludeAttributes As FileAttributes = Nothing) As IEnumerable(Of DirectoryInfo)


        'We could as well build a queue of IEnurables just with
        'the root path as a base. But that's a visual confusing thing,
        'because in the ListView the elements get analyzed not one by one, top-->down
        'but in a cycle (top-->down, top-->down, ...).

        'When we first get and return the root elements, and then iterate
        'through the root elements one-by-one explicitly, the visual
        'experience for the user is MUCh better, although we allocate
        'somewhat more space due to the iterator.
        Dim topLevelDirectories As IEnumerable(Of DirectoryInfo) = Nothing

        Try
            topLevelDirectories = directory.
                EnumerateDirectories().
                Where(Function(dirItem) Not dirItem.Attributes.HasFlag(excludeAttributes))
        Catch ex As Exception
        End Try

        For Each item In topLevelDirectories
            Yield item
        Next

        Dim queue As EnumerableQueue(Of DirectoryInfo) = Nothing

        ' This is the delegate which gets called on dequeueing each item.
        ' It practically fills up the queue with the SubItems from that item.
        ' (If there are any).
        Dim getSubFolder = New Action(Of DirectoryInfo)(
            Sub(item)
                Dim newDirectories As IEnumerable(Of DirectoryInfo) = Nothing

                Try
                    newDirectories = item.
                        EnumerateDirectories().
                        Where(Function(dirItem) Not dirItem.Attributes.HasFlag(excludeAttributes))

                Catch ex As Exception
                    'We swallow those.
                End Try

                If newDirectories?.FirstOrDefault IsNot Nothing Then
                    queue.Queue(newDirectories)
                End If
            End Sub)

        For Each item In topLevelDirectories
            queue = New EnumerableQueue(Of DirectoryInfo)(getSubFolder)
            getSubFolder(item)
            For Each item2 In queue
                Yield (item2)
            Next
        Next
    End Function
End Module
