Imports System.Collections.Immutable    ' We can use this in .NET 4.8, since we added the NuGet System.Collection.Immutable
Imports System.ComponentModel

Public Class FileDoubletFinder

    Private _files As Dictionary(Of Long, List(Of FileEntry))

    Public Event FileDoubletFound(sender As Object, e As FileDoubletFoundEventArgs)

    Public Sub New()
        _files = New Dictionary(Of Long, List(Of FileEntry))
    End Sub

    Public Sub AddFile(file As FileEntry)

        Dim fileDoubletList As List(Of FileEntry) = Nothing

        If _files.TryGetValue(file.Length, fileDoubletList) Then
            fileDoubletList = If(fileDoubletList, New List(Of FileEntry)())
        End If

        Dim eventArgs = New FileDoubletFoundEventArgs(file, fileDoubletList.ToImmutableList)
        RaiseEvent FileDoubletFound(Me, eventArgs)
        If Not eventArgs.Cancel Then
            fileDoubletList.Add(file)
        End If
    End Sub

End Class

Public Class FileDoubletFoundEventArgs
    Inherits CancelEventArgs

    Sub New(fileFound As FileEntry, doubletList As ImmutableList(Of FileEntry))
        Me.FileFound = fileFound
        Me.DoubletList = doubletList
    End Sub

    Public Property FileFound As FileEntry
    Public Property DoubletList As ImmutableList(Of FileEntry)
End Class
