Imports System.IO
Imports PicDedupe.Generic

Public Class PictureViewerForm

    Private _imageFile As FileInfo

    Friend Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        PictureViewer1.FitWindow = True
    End Sub

    ' So, we want to show a picture in a Form that we pass,
    ' and the Form is supposed to load and show the picture
    ' in one go.

    ' That's a bit tricky, since we need to do things from 
    ' this ShowPicture on asynchronously, while ShowPicture isn't.

    ' So, we are creating an instance of the Viewer Form in this
    ' Shared (so static) method, and show it. So far, so good. Now...
    Public Shared Sub ShowPicture(imageFile As FileInfo)
        Dim formInstance = New PictureViewerForm(imageFile)
        formInstance.Text = imageFile.FullName
        formInstance.Show()
    End Sub

    ' ...we are calling this second constructor, which makes sure,
    ' we have a file. It instanciates the Form, which we then show...
    Friend Sub New(imageFile As FileInfo)
        Me.New()
        _imageFile = imageFile
    End Sub

    ' ...and this at one point leads to the creation of the Form's handle.
    ' Now, this is what we need, to Invoke. Invoke needs a created Windows handle,
    ' so CreateHandle is the earliest point in time, where es know, all the necessary
    ' infrastructure for the Form and for Invoking is in place.
    ' Usually Invoking a method is used, to delegate it to the UI thread, and 
    ' this is done by queing the call in the Windows' (Form's) MessageQueue. 
    ' Well, we do right this, not to delegate to the UI thread, but then 
    ' the MessageQueue Call-back becomes Event character,...
    Protected Overrides Sub CreateHandle()
        MyBase.CreateHandle()

        ' ... from which we can kick-off all asynchronous calls! Voila!
        Invoke(
            Async Sub()
                Try
                    Await Me.PictureViewer1.LoadImageAsync(_imageFile)
                Catch ex As Exception
                End Try
            End Sub)
    End Sub

    Private Sub TsbFitPage_Click(sender As Object, e As EventArgs) Handles tsbFitPage.Click
        PictureViewer1.FitWindow = True
    End Sub

    Private Sub TsbOriginalSize_Click(sender As Object, e As EventArgs) Handles tsbOriginalSize.Click
        PictureViewer1.FitWindow = False
    End Sub
End Class
