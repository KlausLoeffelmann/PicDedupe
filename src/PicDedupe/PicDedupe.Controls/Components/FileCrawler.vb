Imports System.IO
Imports PicDedupe.Generic

Public Class FileCrawler

    Public Event TopLevelDirectoriesAvailable(sender As Object, e As TopLevelDirectoriesAvailableEventArgs)
    Public Event ProgressUpdate(sender As Object, e As ProgressUpdateEventArgs)

    Private Const AllFilesSearchPattern = "*.*"

    Private ReadOnly _startPath As String
    Private ReadOnly _searchPattern As String()

    Private _fileEntryTreeTree As FileEntryTree
    Private _fileItemEnumerator As IFileItemEnumerator

    Public Sub New(
        startPath As String,
        Optional searchPattern() As String = Nothing)

        If Not New DirectoryInfo(startPath).Exists Then
            Throw New DirectoryNotFoundException($"Directory {startPath} does not exist.")
        End If

        searchPattern = If(searchPattern, New String() {".*"})

        _startPath = startPath
        _searchPattern = searchPattern
        _fileEntryTreeTree = New FileEntryTree(_startPath)
    End Sub

    Public Function GetFiles() As FileEntryTree

        Dim entryFilter As Func(Of FileEntry, Boolean)
        Dim currentNode = _fileEntryTreeTree.RootNode
        Dim currentFile As String = Nothing

        _fileEntryTreeTree = New FileEntryTree(_startPath)

        If _searchPattern.Any(Function(searchPattern) searchPattern = ".*") Then
            entryFilter = Function(entry) True
        Else
            entryFilter = Function(entry) _searchPattern.Any(Function(searchPattern) searchPattern = Path.GetExtension(entry.Path))
        End If

        Dim topLevelDirectoriesAvailableFired = False
        Dim fileCount As Integer = 0

        'We first get the first-level subdirectories of our start path.

        'We could as well get all the subfolders of the start path 
        'recursively right away. But that's a visualy confusing thing,
        'because in the ListView the element's progress would not be shown one by one, 
        'from top to bottom, but in a permanent cycle (top-->down, top-->down, ...).

        'When we first get and process the root elements, and then iterate
        'through the root elements one-by-one explicitly, the visual
        'experience for the user is MUCH better.

        Dim topLevelEntries = FileItemEnumerator.EnumerateEntries(
            _startPath,
            FileAttributes.Hidden Or FileAttributes.System)

        For Each fileEntry In topLevelEntries
            If Not entryFilter(fileEntry) Then Continue For
            currentNode = _fileEntryTreeTree.AddEntry(fileEntry)
        Next

        RaiseEvent TopLevelDirectoriesAvailable(Me, New TopLevelDirectoriesAvailableEventArgs(RootNode))

        For Each fileEntry In topLevelEntries
            Dim subEntries = FileItemEnumerator.EnumerateEntriesRecursively(
                _startPath,
                FileAttributes.Hidden Or FileAttributes.System)

            For Each subEntry In subEntries
                If Not entryFilter(fileEntry) Then Continue For
                currentNode = _fileEntryTreeTree.AddEntry(subEntry)
                RaiseEvent ProgressUpdate(Me, ProgressUpdateEventArgs.GetDefault(RootNode))
            Next
        Next

        Return _fileEntryTreeTree
    End Function

    Public ReadOnly Property RootNode As FileEntryNode
        Get
            Return _fileEntryTreeTree?.RootNode
        End Get
    End Property

    Public Property FileItemEnumerator As IFileItemEnumerator
        Get
            If _fileItemEnumerator Is Nothing Then
                _fileItemEnumerator = New FileItemEnumerator()
            End If
            Return _fileItemEnumerator
        End Get

        Set(value As IFileItemEnumerator)
            If value Is Nothing Then
                Throw New ArgumentNullException(NameOf(value))
            End If
            _fileItemEnumerator = value
        End Set
    End Property
End Class
