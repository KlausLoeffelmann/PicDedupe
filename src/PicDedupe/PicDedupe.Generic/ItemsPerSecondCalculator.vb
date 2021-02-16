Public Class ItemsPerSecondCalculator
    Private _elementQueue As New Queue(Of Integer)
    Private _bufferCount As Integer
    Private _overAllSum As Long
    Private _overAllElementCount As Integer

    Sub New(bufferCount As Integer)
        _bufferCount = bufferCount
    End Sub

    Sub AddElement(element As Integer)
        _elementQueue.Enqueue(element)
        If _elementQueue.Count > _bufferCount Then
            _elementQueue.Dequeue()
        End If
        _overAllElementCount += 1
        _overAllSum += element
    End Sub

    Public ReadOnly Property OverAllAverage As Integer
        Get
            Return CInt(_overAllSum \ _overAllElementCount)
        End Get
    End Property

    Public ReadOnly Property Avergage As Integer
        Get
            Return _elementQueue.Sum() \ _elementQueue.Count
        End Get
    End Property
End Class
