﻿Imports System.IO
Imports PicDedupe.Generic

Public Class FileCrawler

    Public Event TopLevelDirectoriesAvailable(sender As Object, e As TopLevelDirectoriesAvailableEventArgs)
    Public Event ProgressUpdate(sender As Object, e As ProgressUpdateEventArgs)

    Private ReadOnly _startPath As String
    Private ReadOnly _searchPattern As String()

    Private _fileEntryTree As FileEntryTree
    Private _fileItemEnumerator As IFileItemEnumerator
    Private WithEvents _doubletFinder As FileDoubletFinder

    Public Sub New(
        startPath As String,
        Optional searchPattern As String() = Nothing)

        If Not New DirectoryInfo(startPath).Exists Then
            Throw New DirectoryNotFoundException($"Directory {startPath} does not exist.")
        End If

        _startPath = startPath
        _searchPattern = searchPattern
        _fileEntryTree = New FileEntryTree(_startPath)
    End Sub

    Private Function GetSearchPatternArray(searchPattern As String) As String()
        Dim eliminatedAsterisks = searchPattern.Replace("*", "")
        Return eliminatedAsterisks.Split("|"c)
    End Function

    Public Async Function GetFilesAsync() As Task(Of FileEntryTree)

        Dim entryFilter As Func(Of FileEntry, Boolean)
        Dim currentNode = _fileEntryTree.RootNode
        Dim currentFile As String = Nothing

        _fileEntryTree = New FileEntryTree(_startPath)

        entryFilter = Function(entry) True

        'This code was for filtering, if we needed it at this point.
        'But we don't. We do the filtering in the DoubletFinder.

        'If _searchPattern.Any(Function(searchPattern) searchPattern = ".*") Then
        'Else
        '    entryFilter = Function(entry) entry.IsDirectory OrElse
        '                                  _searchPattern.Any(Function(searchPattern) searchPattern = Path.GetExtension(entry.Path).ToLower)
        'End If

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
            currentNode = _fileEntryTree.AddEntry(fileEntry)
            If Not fileEntry.IsDirectory Then Await DoubletFinder.AddFileAsync(fileEntry)
        Next

        RaiseEvent TopLevelDirectoriesAvailable(Me, New TopLevelDirectoriesAvailableEventArgs(RootNode))

        For Each fileEntry In topLevelEntries

            If Not fileEntry.IsDirectory Then
                Continue For
            End If

            Dim subEntries = FileItemEnumerator.EnumerateEntriesRecursively(
                fileEntry.Path,
                FileAttributes.Hidden Or FileAttributes.System)

            For Each subEntry In subEntries
                If Not entryFilter(subEntry) Then Continue For
                _fileEntryTree.AddEntry(subEntry)

                If Not subEntry.IsDirectory Then Await DoubletFinder.AddFileAsync(subEntry)

                RaiseEvent ProgressUpdate(Me, ProgressUpdateEventArgs.GetDefault(RootNode))
            Next
        Next

        Return _fileEntryTree
    End Function

    Public ReadOnly Property RootNode As FileEntryNode
        Get
            Return _fileEntryTree?.RootNode
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

    Public Property DoubletFinder As FileDoubletFinder
        Get
            If _doubletFinder Is Nothing Then
                _doubletFinder = New FileDoubletFinder()
            End If
            Return _doubletFinder
        End Get
        Set(value As FileDoubletFinder)
            If value Is Nothing Then
                Throw New ArgumentNullException(NameOf(value))
            End If
            _doubletFinder = value
        End Set
    End Property
End Class
