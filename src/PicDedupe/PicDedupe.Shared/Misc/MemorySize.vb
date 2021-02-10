Imports System.Globalization

Public Structure MemorySize

    Private _size As ULong ' An ULong can represent a maximum of 16 Exabytes.

    Private Shared ReadOnly s_units As String() =
        {"Bytes", "Kilobytes", "MegaBytes", "Gigabytes", "Terabytes", "Petabytes", "Exabytes"}

    Private Shared ReadOnly s_decimalPlacecFormatStrings As String() =
        {"#,##0", "#,##0.0", "#,##0.00", "#,##0.000", "#,##0.0000", "#,##0.00000"}

    Private Shared s_defaultDecimalPlaces As Byte = 3

    Private Sub New(value As Long)
        _size = CULng(value)
    End Sub

    Private Sub New(value As Integer)
        _size = CULng(value)
    End Sub

    Private Sub New(value As ULong)
        _size = value
    End Sub

    Private Sub New(value As UInteger)
        _size = value
    End Sub

    Public Shared Property DefaultDecimalPlaces As Byte
        Get
            Return s_defaultDecimalPlaces
        End Get
        Set(value As Byte)
            If value > s_decimalPlacecFormatStrings.Length Then
                Throw New ArgumentException($"Formatting is limitted to {s_decimalPlacecFormatStrings.Length} decimal places.")
            End If
            s_defaultDecimalPlaces = value
        End Set
    End Property

    Public ReadOnly Property Value As ULong
        Get
            Return _size
        End Get
    End Property

    Public Overrides Function ToString() As String
        Dim floatSize As Double = _size
        Dim previousSize As Double

        For i = 1 To s_units.Length
            previousSize = floatSize
            floatSize /= 1024
            If floatSize < 1 Then
                Return $"{previousSize.ToString(s_decimalPlacecFormatStrings(s_defaultDecimalPlaces))} {s_units(i - 1)}"
            End If
        Next

        Throw New ArgumentOutOfRangeException("Couldn't convert Memorysize to String.")
    End Function

    Public Shared Widening Operator CType(longValue As Long) As MemorySize
        Return New MemorySize(longValue)
    End Operator

    Public Shared Widening Operator CType(intValue As Integer) As MemorySize
        Return New MemorySize(intValue)
    End Operator
    Public Shared Widening Operator CType(ulongValue As ULong) As MemorySize
        Return New MemorySize(ulongValue)
    End Operator
    Public Shared Widening Operator CType(uintValue As UInteger) As MemorySize
        Return New MemorySize(uintValue)
    End Operator
End Structure
