Imports System.IO

Public Interface IFileItemEnumerator

    Function EnumerateEntriesRecursively(
        rootPath As String,
        Optional excludeAttributes As FileAttributes = Nothing) As IEnumerable(Of FileEntry)

    Function EnumerateEntries(
        rootPath As String,
        Optional excludeAttributes As FileAttributes = Nothing) As IEnumerable(Of FileEntry)

End Interface
