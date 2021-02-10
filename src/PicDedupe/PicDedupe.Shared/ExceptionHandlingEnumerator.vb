Public Class ExceptionHandlingEnumerator(Of T)
    Implements IEnumerator(Of T), IEnumerable(Of T)

    Private _enumerator As IEnumerator(Of T)
    Private disposedValue As Boolean

    Sub New(enumerable As IEnumerable(Of T))
        _enumerator = enumerable.GetEnumerator
    End Sub

    Public Function GetEnumerator() As IEnumerator(Of T) Implements IEnumerable(Of T).GetEnumerator
        Return Me
    End Function

    Private Function IEnumerable_GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
        Return Me
    End Function

    Public ReadOnly Property Current As T Implements IEnumerator(Of T).Current
        Get
            Return _enumerator.Current
        End Get
    End Property

    Private ReadOnly Property IEnumerator_Current As Object Implements IEnumerator.Current
        Get
            Return Current
        End Get
    End Property

    Public Property LastException As Exception

    Public Sub Reset() Implements IEnumerator.Reset
        _enumerator.Reset()
    End Sub

    Public Function MoveNext() As Boolean Implements IEnumerator.MoveNext
        Do
            Try
                Dim IsAnotherItem = _enumerator.MoveNext()
                Return IsAnotherItem
            Catch ex As Exception
                LastException = ex
            End Try
        Loop
    End Function

    Public Sub Dispose() Implements IDisposable.Dispose
        DirectCast(_enumerator, IDisposable).Dispose()
    End Sub
End Class
