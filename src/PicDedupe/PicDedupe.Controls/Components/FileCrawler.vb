Imports System.IO
Imports PicDedupe.Generic

Public Class FileCrawler

    Public Event TopLevelDirectoriesAvailable(sender As Object, e As TopLevelDirectoriesAvailableEventArgs)
    Public Event ProgressUpdate(sender As Object, e As ProgressUpdateEventArgs)

    Private Const AllFilesSearchPattern = "*.*"

    Private ReadOnly _startPath As DirectoryInfo
    Private ReadOnly _progressReporter As Action(Of ProgressReportInfo)
    Private ReadOnly _searchPattern As String()

    Private _directoryInfoTree As DirectoryInfoTree

    Public Sub New(
        startPath As DirectoryInfo,
        Optional searchPattern() As String = Nothing,
        Optional progressReporter As Action(Of ProgressReportInfo) = Nothing)

        If Not startPath.Exists Then
            Throw New DirectoryNotFoundException($"Directory {startPath.FullName} does not exist.")
        End If

        _directoryInfoTree = New DirectoryInfoTree(_startPath)

        searchPattern = If(searchPattern, New String() {".*"})

        _startPath = startPath
        _searchPattern = searchPattern
        _progressReporter = progressReporter
    End Sub

    Public Function GetTopLevelDirectoriesAsync() As DirectoryInfo()
        Return _startPath.GetDirectories()
    End Function

    Public Function GetFiles() As DirectoryInfoTree

        Const EventRaiserCounterThreshold As Integer = 500

        Dim searchAction As Action
        Dim eventRaiser As Action(Of DirectoryInfoNode)
        Dim currentNode = _directoryInfoTree.RootNode
        Dim currentFileItem As FileInfo = Nothing
        Dim eventRaiseCounter As Integer

        _directoryInfoTree = New DirectoryInfoTree(_startPath)

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

        eventRaiser =
            Sub(nodeToUpdate)
                eventRaiseCounter += 1
                If eventRaiseCounter = EventRaiserCounterThreshold Then
                    eventRaiseCounter = 0
                    RaiseEvent ProgressUpdate(Me, ProgressUpdateEventArgs.GetDefault(nodeToUpdate))
                End If
            End Sub

        Dim topLevelDirectories = New List(Of DirectoryInfoNode)
        Dim topLevelDirectoriesAvailableFired = False
        Dim fileCount As Integer = 0

        Dim ioDirectories = _startPath.EnumerateAllSubDirectories(FileAttributes.Hidden Or FileAttributes.System)

        For Each directoryItem In ioDirectories

            currentNode = _directoryInfoTree.AddDirectory(directoryItem)

            If Not topLevelDirectoriesAvailableFired Then
                If directoryItem.Parent.FullName <> _startPath.FullName Then
                    RaiseEvent TopLevelDirectoriesAvailable(Me, New TopLevelDirectoriesAvailableEventArgs(topLevelDirectories))
                    topLevelDirectoriesAvailableFired = True
                Else
                    topLevelDirectories.Add(currentNode)
                End If
            End If

            Dim files As IEnumerable(Of FileInfo) = Nothing

            Try
                files = directoryItem.EnumerateFiles(AllFilesSearchPattern)
                For Each currentFileItem In files
                    searchAction()
                    eventRaiser(RootNode)
                Next
                eventRaiser(RootNode)
            Catch ex As Exception
                ' We swallow this, if EnumerateFiles causes an exception.
            End Try
        Next

        ' We update now unconditionally.
        eventRaiseCounter = EventRaiserCounterThreshold
        eventRaiser(RootNode)

        Return _directoryInfoTree
    End Function

    Public ReadOnly Property RootNode As DirectoryInfoNode
        Get
            Return _directoryInfoTree?.RootNode
        End Get
    End Property

End Class
