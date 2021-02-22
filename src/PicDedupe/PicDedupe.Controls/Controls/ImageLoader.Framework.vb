#If NET461_OR_GREATER Then
Imports PicDedupe.Generic

Friend Class ImageLoader
    Public Shared Async Function LoadImageAsync(imageFile As FileEntry) As Task(Of (Image, Exception))
        Try
            Dim imageToReturn = Await Task.FromResult(Image.FromFile(imageFile.Path))
            Return (imageToReturn, Nothing)
        Catch ex As Exception
            Return (Nothing, ex)
        End Try
    End Function
End Class
#End If
