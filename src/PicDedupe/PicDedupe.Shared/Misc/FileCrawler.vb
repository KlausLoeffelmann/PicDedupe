Imports System.IO

Public Class FileCrawler

    Private Const AllFilesSearchPattern = "*.*"

    Private ReadOnly _startPath As DirectoryInfo
    Private ReadOnly _progressReporter As Action(Of ProgressReportInfo)
    Private ReadOnly _searchPattern As String()

    Public Sub New(
        startPath As DirectoryInfo,
        Optional searchPattern() As String = Nothing,
        Optional progressReporter As Action(Of ProgressReportInfo) = Nothing)

        If Not startPath.Exists Then
            Throw New DirectoryNotFoundException($"Directory {startPath.FullName} does not exist.")
        End If

        searchPattern = If(searchPattern, New String() {".*"})

        _startPath = startPath
        _searchPattern = searchPattern
        _progressReporter = progressReporter
    End Sub

    Public Function GetTopLevelDirectoriesAsync() As DirectoryInfo()
        Return _startPath.GetDirectories()
    End Function

    Public Function GetFilesRecursively() As FileInfo()

        Dim searchAction As Action
        Dim directoryInfoTree = New DirectoryInfoTree(_startPath)
        Dim currentNode = directoryInfoTree.RootNode
        Dim currentFileItem As FileInfo = Nothing

        If _searchPattern.Any(Function(searchPattern) searchPattern = ".*") Then
            searchAction =
                Sub()
                    currentNode.AddFile(currentFileItem)
                End Sub
        Else
            searchAction =
                Sub()
                    If _searchPattern.Any(Function(searchPattern) searchPattern = currentFileItem.Extension) Then
                        currentNode.AddFile(currentFileItem)
                    End If
                End Sub
        End If

        Dim ioDirectories = _startPath.EnumerateDirectories(AllFilesSearchPattern, SearchOption.AllDirectories)
        For Each directoryItem In ioDirectories
            currentNode = directoryInfoTree.AddDirectory(directoryItem)
            Dim files = directoryItem.EnumerateFiles(AllFilesSearchPattern)
            For Each currentFileItem In files
                searchAction()
            Next
        Next
    End Function
End Class

Public Structure ProgressReportInfo
    Public Directory As DirectoryInfo
    Public Size As MemorySize
    Public Count As Integer
End Structure
