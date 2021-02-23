Imports System.IO

Public Class PictureViewer

    Private _lastException As Exception

    Public Async Function LoadImageAsync(fileInfo As FileInfo) As Task
        Dim imageAndException = Await ImageLoader.LoadImageAsync(fileInfo)
        If imageAndException.exception IsNot Nothing Then
            pictureBox.Image = Nothing
            _lastException = imageAndException.exception
        Else
            pictureBox.Image = imageAndException.image
        End If
    End Function

    Protected Overrides Sub OnLayout(e As LayoutEventArgs)
        MyBase.OnLayout(e)

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
    End Sub

    Public ReadOnly Property LastException As Exception
        Get
            Return _lastException
        End Get
    End Property
End Class
