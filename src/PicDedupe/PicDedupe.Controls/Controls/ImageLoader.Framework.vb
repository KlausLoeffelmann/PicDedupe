'#If NET461_OR_GREATER Then
Imports System.IO
Imports PicDedupe.Generic

Friend Class ImageLoader
    Public Shared Async Function LoadImageAsync(imageFile As FileInfo) As Task(Of (image as Image, exception as Exception))
        Try
            Dim imageToReturn = Await Task.FromResult(Image.FromFile(imageFile.FullName))
            Return (imageToReturn, Nothing)
        Catch ex As Exception
            Return (Nothing, ex)
        End Try
    End Function
End Class
'#End If
