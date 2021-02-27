Public Class EnumerableQueue(Of T)
    Implements IEnumerable(Of T)

    Private ReadOnly _queue As New Queue(Of T)
    Private ReadOnly _queueProcessPredicate As Action(Of T)

    Sub New(queueProcessPredicate As Action(Of T))
        _queueProcessPredicate = queueProcessPredicate
    End Sub

    Public Sub Queue(queue As IEnumerable(Of T))

        If queue Is Nothing Then Return

        For Each item In queue
            _queue.Enqueue(item)
        Next
    End Sub

    Public Iterator Function ToIEnumerable() As IEnumerable(Of T)
        Do While _queue.Count > 0
            Dim element = _queue.Dequeue
            _queueProcessPredicate(element)
            Yield element
        Loop
    End Function

    Public Function GetEnumerator() As IEnumerator(Of T) Implements IEnumerable(Of T).GetEnumerator
        Return ToIEnumerable.GetEnumerator()
    End Function

    Private Function IEnumerable_GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
        Return GetEnumerator()
    End Function
End Class
