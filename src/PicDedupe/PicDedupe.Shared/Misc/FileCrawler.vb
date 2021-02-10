Imports System.IO

Public Class FileCrawler

    Public Event TopLevelDirectoriesAvailable(sender As Object, e As TopLevelDirectoriesAvailableEventArgs)
    Public Event ProgressUpdate(sender As Object, e As ProgressUpdateEventArgs)

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

    Public Function GetFiles() As DirectoryInfoTree

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

        Dim topLevelDirectories = New List(Of DirectoryInfoNode)
        Dim topLevelDirectoriesAvailableFired = False
        Dim fileCount As Integer = 0

        Dim ioDirectories = _startPath.EnumerateDirectories(AllFilesSearchPattern, SearchOption.AllDirectories)
        Dim ioDirectoriesEnumerator = New ExceptionHandlingEnumerator(Of DirectoryInfo)(ioDirectories)
        Try

            For Each directoryItem In ioDirectoriesEnumerator
                If directoryItem Is Nothing Then
                    If ioDirectoriesEnumerator.LastException IsNot Nothing Then
                        Continue For
                    End If
                End If
                currentNode = directoryInfoTree.AddDirectory(directoryItem)

                If Not topLevelDirectoriesAvailableFired Then
                    If directoryItem.Parent.FullName <> _startPath.FullName Then
                        RaiseEvent TopLevelDirectoriesAvailable(Me, New TopLevelDirectoriesAvailableEventArgs(topLevelDirectories))
                        topLevelDirectoriesAvailableFired = True
                    Else
                        topLevelDirectories.Add(currentNode)
                    End If
                End If

                Dim files = directoryItem.EnumerateFiles(AllFilesSearchPattern)
                For Each currentFileItem In files
                    searchAction()
                    fileCount += 1
                    If fileCount = 50 Then
                        fileCount = 0
                        RaiseEvent ProgressUpdate(Me, New ProgressUpdateEventArgs(currentNode))
                    End If
                Next
            Next
        Catch ex As Exception

        End Try
        Return directoryInfoTree
    End Function
End Class

Public Structure ProgressReportInfo
    Public Directory As DirectoryInfo
    Public Size As MemorySize
    Public Count As Integer
End Structure

Public Class TopLevelDirectoriesAvailableEventArgs
    Inherits EventArgs

    Public Sub New(topLevelDirectories As IEnumerable(Of DirectoryInfoNode))
        Me.TopLevelDirectories = topLevelDirectories
    End Sub

    Public ReadOnly Property TopLevelDirectories As IEnumerable(Of DirectoryInfoNode)
End Class

Public Class ProgressUpdateEventArgs
    Inherits EventArgs

    Public Sub New(nodeToUpdate As DirectoryInfoNode)
        Me.NodeToUpdate = nodeToUpdate
    End Sub

    Public ReadOnly Property NodeToUpdate As DirectoryInfoNode

End Class
