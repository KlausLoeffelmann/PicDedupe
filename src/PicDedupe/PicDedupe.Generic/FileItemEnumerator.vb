Imports System.IO
Imports System.Security

' Our standard FileItem Enumerator which works based on DirectoryInfo, and
' therefore through all the Frameworks, be it classic Framework or .NET (Core).
Public Class FileItemEnumerator
    Implements IFileItemEnumerator

    Private Shared ReadOnly FileAttributeValues As FileAttributes() =
        [Enum].GetValues(GetType(FileAttributes)).Cast(Of FileAttributes).ToArray()

    Public Iterator Function EnumerateEntries(
        rootPath As String,
        Optional excludeAttributes As FileAttributes = 0) As IEnumerable(Of FileEntry) Implements IFileItemEnumerator.EnumerateEntries

        Dim rootDirectory = New DirectoryInfo(rootPath)

        ' We test each Bit of Attributes to check for again the Bits of each file.
        Dim excludeAttributeFunction = New Func(Of FileSystemInfo, Boolean)(
            Function(fileItem) (From enumValue In FileAttributeValues
                                Where excludeAttributes.HasFlag(enumValue) AndAlso
                                      fileItem.Attributes.HasFlag(enumValue)).FirstOrDefault > 0)

        'We first enumerate the directories...
        For Each fileEntry In rootDirectory.EnumerateDirectories().
            Where(Function(fileItem) Not excludeAttributeFunction(fileItem))

            Yield New FileEntry(
                        fileEntry.FullName,
                        isDirectory:=True)
        Next

        '...and then the Files in that directory.
        For Each fileEntry In rootDirectory.EnumerateFiles("*.*").
            Where(Function(fileItem) Not excludeAttributeFunction(fileItem))

            Yield New FileEntry(
                        fileEntry.FullName,
                        isDirectory:=False,
                        length:=fileEntry.Length)
        Next
    End Function

    Public Iterator Function EnumerateEntriesRecursively(
        rootPath As String,
        Optional excludeAttributes As FileAttributes = 0) As IEnumerable(Of FileEntry) Implements IFileItemEnumerator.EnumerateEntriesRecursively

        Dim queue As EnumerableQueue(Of DirectoryInfo) = Nothing

        Dim excludeAttributeFunction = New Func(Of FileSystemInfo, Boolean)(
            Function(fileItem) (From enumValue In FileAttributeValues
                                Where excludeAttributes.HasFlag(enumValue) AndAlso
                                      fileItem.Attributes.HasFlag(enumValue)).FirstOrDefault > 0)

        ' This is the delegate which gets called on dequeueing each item.
        ' It practically fills up the queue with the SubItems from that item.
        ' (If there are any).
        Dim getSubFolder = New Action(Of DirectoryInfo)(
            Sub(item)
                Dim newDirectories As IEnumerable(Of DirectoryInfo) = Nothing

                Try
                    newDirectories = New DirectoryInfo(item.FullName).
                        EnumerateDirectories().
                            Where(Function(dirItem) Not excludeAttributeFunction(dirItem))

                    'We just swallow those.
                Catch ex As SecurityException
                Catch ex As DirectoryNotFoundException
                End Try

                queue.Queue(newDirectories)
            End Sub)

        Dim topLevelDirectory = New DirectoryInfo(rootPath)

        'We're queueing each of the top level directories.

        'Note: The actual recursion is happening inside of EnumerableQueue,
        'since we're passing a delegate which gets called, when the
        'ToIEnumerable method is executed. For each element enumerated,
        'we call again `getSubFolder`, which adds the correlated subfolders
        'to the queue, which then get processed.
        'This way, calling ToEnumerable gets back the first element
        'immediately, while the crawling of the folders is still
        'happening. (Does have _nothing_ to do with parallelization, btw.)
        queue = New EnumerableQueue(Of DirectoryInfo)(getSubFolder)

        ' We need to process the files of the top directory individually,
        ' since the top directory must not become part of the queue.
        For Each fileEntry In topLevelDirectory.EnumerateFiles("*.*").
                Where(Function(fileItem) Not excludeAttributeFunction(fileItem))

            Yield New FileEntry(
                        fileEntry.FullName,
                        isDirectory:=False,
                        length:=fileEntry.Length)
        Next

        'And then getting their subfolders, which also get queued.
        getSubFolder(topLevelDirectory)

        For Each subDirectory In queue

            Yield New FileEntry(
                    subDirectory.FullName,
                    isDirectory:=True)

            For Each fileEntry In subDirectory.EnumerateFiles("*.*").
                Where(Function(fileItem) Not excludeAttributeFunction(fileItem))

                Yield New FileEntry(
                        fileEntry.FullName,
                        isDirectory:=False,
                        length:=fileEntry.Length)
            Next
        Next
    End Function
End Class
