Imports PicDedupe.Generic

Public Class PictureViewer

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

#If NET5_0_OR_GREATER Then
    Private Class ImageLoader
        Public Function GetSupportedImageFormats() As String()

        End Function

        Public Function LoadImage(filename As FileEntry) As Bitmap

        End Function
    End Class
#End If

End Class
