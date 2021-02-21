#If NET5_0_OR_GREATER Then
Imports System.IO
Imports System.IO.Enumeration

Public Class LightningFastFileItemEnumerator
    Implements IFileItemEnumerator

    Public Function EnumerateEntriesRecursively(
        rootPath As String,
        Optional excludeAttributes As FileAttributes = Nothing) As IEnumerable(Of FileEntry) Implements IFileItemEnumerator.EnumerateEntriesRecursively

        Dim enumOptions = New EnumerationOptions() With {.RecurseSubdirectories = True}

        Return EnumerateEntriesInternal(rootPath, enumOptions, excludeAttributes)
    End Function

    Public Function EnumerateEntries(
        rootPath As String,
        Optional excludeAttributes As FileAttributes = 0) As IEnumerable(Of FileEntry) Implements IFileItemEnumerator.EnumerateEntries

        Dim enumOptions = New EnumerationOptions() With {.RecurseSubdirectories = False}

        Return EnumerateEntriesInternal(rootPath, enumOptions, excludeAttributes).
               OrderByDescending(Function(item) item.IsDirectory) ' It looks weird, if files appear in between folders in the listview.
    End Function

    Private Iterator Function EnumerateEntriesInternal(rootPath As String, enumOptions As EnumerationOptions, excludeAttributes As FileAttributes) As IEnumerable(Of FileEntry)

        Dim enumeration = New FileSystemEnumerable(Of FileEntry)(
            rootPath,
            Function(ByRef entry) New FileEntry(entry.ToFullPath, entry.IsDirectory, entry.Length),
            enumOptions) With
            {
                .ShouldIncludePredicate = Function(ByRef entry) Not (entry.Attributes.HasFlag(excludeAttributes))
            }

        For Each fileEntry In enumeration
            Yield fileEntry
        Next

    End Function
End Class
#End If
