Imports System.IO

Public Class PictureViewer

    Private _lastException As Exception
    Private _fitWindow As Boolean

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        AddHandler pictureBox.Paint,
            Sub(sender, e)
                If _lastException IsNot Nothing Then
                    Dim errorMessage = $"Sorry - while trying to show the Pic,{vbCrLf}an unexpected exception happend.{vbCrLf}The Picture Format was not recognized."

                    Dim size = e.Graphics.MeasureString(errorMessage, Font, Width \ 3 * 2)
                    Dim location = New Point(
                        Width \ 2 - CInt(size.Width) \ 2,
                        Height \ 2 - CInt(size.Height) \ 2)

                    e.Graphics.DrawString(
                    errorMessage,
                    Font,
                    Brushes.Black,
                    location)
                End If
            End Sub
    End Sub

    Public Async Function LoadImageAsync(fileInfo As FileInfo) As Task
        Dim imageAndException = Await ImageLoader.LoadImageAsync(fileInfo)
        If imageAndException.exception IsNot Nothing Then
            _lastException = imageAndException.exception
            pictureBox.Image = Nothing
            PerformLayout()
            Invalidate()
        Else
            _lastException = Nothing
            pictureBox.Image = imageAndException.image
        End If
    End Function

    Public Property FitWindow As Boolean
        Get
            Return _fitWindow
        End Get
        Set(value As Boolean)
            If Not Object.Equals(_fitWindow, value) Then
                _fitWindow = value
                PerformLayout()
            End If
        End Set
    End Property

    Protected Overrides Sub OnLayout(e As LayoutEventArgs)
        MyBase.OnLayout(e)

        If FitWindow OrElse _lastException IsNot Nothing Then
            Me.AutoScroll = False
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom
            pictureBox.Location = New Point(0, 0)
            pictureBox.Size = ClientSize
        Else
            Me.AutoScroll = True
            pictureBox.SizeMode = PictureBoxSizeMode.Normal
            If pictureBox.Image Is Nothing Then
                Return
            End If

            pictureBox.Size = pictureBox.Image.Size

            Dim x, y As Integer

            With Me.ClientRectangle
                If pictureBox.Width < .Width Then
                    x = .Width \ 2 - pictureBox.Width \ 2
                Else
                    x = 0
                End If

                If pictureBox.Height < .Height Then
                    y = .Height \ 2 - pictureBox.Height \ 2
                Else
                    y = 0
                End If
                pictureBox.Location = New Point(x, y)
            End With
        End If
    End Sub

    Public ReadOnly Property LastException As Exception
        Get
            Return _lastException
        End Get
    End Property
End Class
