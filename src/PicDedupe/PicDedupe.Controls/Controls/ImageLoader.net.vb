'#If NET5_0_OR_GREATER Then
'Imports System.Drawing.Imaging
'Imports System.IO
'Imports System.Runtime.InteropServices
'Imports PicDedupe.Generic

'Imports Windows.Graphics.Imaging
'Imports Windows.Storage
'Imports Windows.Storage.FileProperties
'Imports Windows.Storage.Streams

'' More about using Desktop Bridge/projected UWP APIs:
'' https://docs.microsoft.com/en-us/windows/apps/desktop/modernize/desktop-to-uwp-enhance

'Friend Class ImageLoader

'    Private Shared ReadOnly s_parallelOptions As ParallelOptions

'    Public Shared Async Function LoadThumbnailAsync(ByVal imageFile As FileEntry) As Task(Of Bitmap)
'        Dim storageItemThumbnailStream As IRandomAccessStream
'        Dim file As StorageFile = Await StorageFile.GetFileFromPathAsync(imageFile.Path)

'        Try
'            Dim storageItemThumbnail = Await file.GetThumbnailAsync(ThumbnailMode.ListView, 128)

'            storageItemThumbnailStream = If(
'                storageItemThumbnail Is Nothing,
'                Await file.OpenAsync(FileAccessMode.Read),
'                storageItemThumbnail.CloneStream())
'        Catch e1 As Exception
'            Throw
'        End Try

'        Dim bitmap As Bitmap = Await GetWinFormsBitmapAsync(storageItemThumbnailStream, New Size(128, 128))
'        Return bitmap
'    End Function

'    Public Shared Async Function LoadImageAsync(
'        ByVal imageFile As FileInfo,
'        Optional ByVal scaleAndCropSize? As Size = Nothing) As Task(Of (image As Image, exception As Exception))

'        Dim file As StorageFile = Await StorageFile.GetFileFromPathAsync(imageFile.FullName)
'        'var imageProperties = await file.Properties.GetImagePropertiesAsync();

'        Using stream = Await file.OpenAsync(FileAccessMode.Read)
'            Try
'                Dim bitmap As Bitmap = Await GetWinFormsBitmapAsync(stream, scaleAndCropSize)
'                Return (bitmap, Nothing)
'            Catch ex As Exception
'                Return (Nothing, ex)
'            End Try
'        End Using

'    End Function

'    Private Shared Async Function GetWinFormsBitmapAsync(ByVal stream As IRandomAccessStream, Optional ByVal scaleAndCropSize? As Size = Nothing) As Task(Of Bitmap)
'        Dim decoder As BitmapDecoder = Await BitmapDecoder.CreateAsync(stream)

'        Dim bitmapTransform = New BitmapTransform()

'        Dim scaleWidth As Double = decoder.OrientedPixelWidth
'        Dim scaleHeight As Double = decoder.OrientedPixelHeight

'        If scaleAndCropSize IsNot Nothing Then
'            Dim scale As Double

'            Dim destRatio = scaleAndCropSize.Value.Width \ scaleAndCropSize.Value.Height
'            scale = CDbl(scaleAndCropSize.Value.Width) / decoder.OrientedPixelWidth

'            scaleWidth = decoder.OrientedPixelWidth * scale
'            scaleHeight = decoder.OrientedPixelHeight * scale
'            bitmapTransform.ScaledWidth = CUInt(scaleWidth)
'            bitmapTransform.ScaledHeight = CUInt(scaleHeight)
'        End If

'        If decoder.OrientedPixelWidth <> decoder.PixelWidth Then
'            bitmapTransform.Rotation = BitmapRotation.Clockwise270Degrees
'            Dim scaleHeightTemp = bitmapTransform.ScaledHeight
'            bitmapTransform.ScaledHeight = bitmapTransform.ScaledWidth
'            bitmapTransform.ScaledWidth = scaleHeightTemp
'        End If

'        Dim bitmap = New Bitmap(CInt(Fix(scaleWidth)), CInt(Fix(scaleHeight)), System.Drawing.Imaging.PixelFormat.Format32bppArgb)

'        Dim bmpdata = bitmap.LockBits(New Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, bitmap.PixelFormat)
'        Dim numbytes As Integer = bmpdata.Stride * bitmap.Height
'        bitmap.UnlockBits(bmpdata)

'        Dim pixelData = Await decoder.GetPixelDataAsync(
'            BitmapPixelFormat.Rgba8,
'            BitmapAlphaMode.Straight,
'            bitmapTransform,
'            ExifOrientationMode.IgnoreExifOrientation,
'            ColorManagementMode.DoNotColorManage)

'        Dim bitmapBytes = pixelData.DetachPixelData()
'        Dim stride = (((CInt(Fix(scaleWidth)) * 32) + 31) And (Not 31)) >> 3

'        Debug.Print($"Allocated: {bitmapBytes.Length / 1024:#,###} KiloBytes for Graphic calculation.")

'        Dim bitmapData As BitmapData = bitmap.LockBits(
'            New Rectangle(0, 0, bitmap.Width, bitmap.Height),
'            ImageLockMode.ReadWrite, bitmap.PixelFormat)

'        ' We need to reorgenize the Byte-Pattern.            
'        Parallel.For(0, bitmapBytes.Length \ 4, s_parallelOptions,
'                     Sub(i) ' * 4
'                         Dim parallelForIndex = i << &B10
'                         Dim byteTemp = bitmapBytes(parallelForIndex + 0)
'                         bitmapBytes(parallelForIndex + 0) = bitmapBytes(parallelForIndex + 2)
'                         bitmapBytes(parallelForIndex + 2) = byteTemp
'                     End Sub)

'        Marshal.Copy(bitmapBytes, 0, bitmapData.Scan0, bitmapBytes.Length)
'        bitmap.UnlockBits(bitmapData)
'        Return bitmap
'    End Function

'    Shared Sub New()

'        Dim maxDegreeOfParallelism As Integer

'        Select Case Environment.ProcessorCount
'            Case 1, 2
'                maxDegreeOfParallelism = 1
'            Case 3, 4
'                maxDegreeOfParallelism = 2
'            Case 4 To 8
'                maxDegreeOfParallelism = Environment.ProcessorCount \ 2
'            Case 8 To 32
'                maxDegreeOfParallelism = Environment.ProcessorCount \ 4
'            Case > 32
'                maxDegreeOfParallelism = 10
'            Case Else
'                Throw New Exception("This machine doesn't seem to have a processor.")
'                Exit Select
'        End Select

'        s_parallelOptions = New ParallelOptions() With
'        {
'            .MaxDegreeOfParallelism = maxDegreeOfParallelism
'        }
'    End Sub

'End Class
'#End If
