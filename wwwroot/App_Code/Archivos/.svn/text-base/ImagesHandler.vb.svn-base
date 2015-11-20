Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler
Imports System.IO
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Text.RegularExpressions
Imports System.Web
Imports System.Drawing
Imports System.Drawing.Bitmap
Imports System.Drawing.Imaging
Imports System.Drawing.Image

Namespace Orbelink.Control.Archivos

    ''' <summary>
    ''' Clase que maneja imagenes con sus tamaños y sus thumbs
    ''' </summary>
    ''' <version>1.2</version>
    ''' <remarks></remarks>
    Public Class ImagesHandler
        Implements IHandlerArchivo

        Public Structure Extension
            Dim theExtension As String
            Dim theFormat As System.Drawing.Imaging.ImageFormat
        End Structure

        Private Const _temp_prefix As String = "temp_"

        Private _SupportedExtensions() As String = {"jpg", "jpeg", "png", "gif", "tiff"}

        Public Function GetFormatFromExtension(ByVal laExtension As String) As System.Drawing.Imaging.ImageFormat
            Dim elFormato As System.Drawing.Imaging.ImageFormat = System.Drawing.Imaging.ImageFormat.Bmp
            If laExtension.Chars(0) <> "."c Then
                laExtension = "." & laExtension
            End If

            Select Case laExtension.ToLower
                Case ".jpg"
                    elFormato = System.Drawing.Imaging.ImageFormat.Jpeg

                Case ".jpeg"
                    elFormato = System.Drawing.Imaging.ImageFormat.Jpeg

                Case ".png"
                    elFormato = System.Drawing.Imaging.ImageFormat.Png

                Case ".gif"
                    elFormato = System.Drawing.Imaging.ImageFormat.Gif

                Case ".tiff"
                    elFormato = System.Drawing.Imaging.ImageFormat.Tiff
            End Select
            Return elFormato
        End Function

        ''' <summary>
        ''' Recibe una imagen le cambia el tamaño, y crea un thumb.
        ''' </summary>
        ''' <param name="fullImagePath"></param>
        ''' <param name="thumbFilePath"></param>
        ''' <param name="theImageSize"></param>
        ''' <param name="theThumbSize"></param>
        ''' <param name="elFormato"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function ResizeImageAndThumb(ByVal fullImagePath As String, ByVal thumbFilePath As String, ByRef theImageSize As Size, ByRef theThumbSize As Size, ByVal elFormato As System.Drawing.Imaging.ImageFormat) As ArchivoInfo
            'Copia el archivo al directorio del thumb
            File.Copy(fullImagePath, thumbFilePath)

            Dim imageNormal As New System.Drawing.Bitmap(fullImagePath)
            Dim imageThumb As New System.Drawing.Bitmap(thumbFilePath)

            Dim size_Normal As Size = ImagesHandler.GetFitToWindowDimensions(imageNormal.Width, imageNormal.Height, theImageSize.Width, theImageSize.Height)
            Dim size_Thumb As Size = ImagesHandler.GetFitToWindowDimensions(imageThumb.Width, imageThumb.Height, theThumbSize.Width, theThumbSize.Height)

            Dim optNormal As New System.Drawing.Bitmap(imageNormal, size_Normal)
            Dim optThumb As New System.Drawing.Bitmap(imageThumb, size_Thumb)

            imageNormal.Dispose()
            imageThumb.Dispose()

            optNormal.Save(fullImagePath, elFormato)
            optThumb.Save(thumbFilePath, elFormato)

            optNormal.Dispose()
            optThumb.Dispose()

            Dim ArchivoInfo As New ArchivoInfo(fullImagePath)
            Return ArchivoInfo
        End Function

        ''' <summary>
        ''' Metodo que obtiene las dimensiones de la imagen que se va a redimensionar
        ''' </summary>
        ''' <param name="CurrentWidth">Ancho actual de la imagen</param>
        ''' <param name="CurrentHeight">Alto actual de la imagen</param>
        ''' <param name="DesiredWidth">Ancho deseado de la imagen</param>
        ''' <param name="DesiredHeight">Alto deseado de la imagen</param>
        ''' <returns>La estructura size de la imagen</returns>
        ''' <remarks></remarks>
        Public Shared Function GetResizeDimensions(ByVal CurrentWidth As Integer, ByVal CurrentHeight As Integer, ByVal DesiredWidth As Integer, ByVal DesiredHeight As Integer) As Size
            Dim liChange As Single
            Dim newHeight As Integer
            Dim newWidth As Integer

            If CurrentHeight = CurrentWidth Then
                'image is square...
                'we can use either height or width to resize....
                'use height
                liChange = DesiredHeight / CurrentHeight
                newHeight = CurrentHeight * liChange
                newWidth = CurrentWidth * liChange

            ElseIf CurrentHeight < CurrentWidth Then
                'image is landscape...use width to resize
                liChange = DesiredWidth / CurrentWidth
                newHeight = CurrentHeight * liChange
                newWidth = DesiredWidth

            Else
                'image is portrait...
                liChange = DesiredHeight / CurrentHeight
                newHeight = DesiredHeight
                newWidth = CurrentWidth * liChange

            End If

            If CurrentHeight <= DesiredHeight And CurrentWidth <= DesiredWidth Then
                newHeight = CurrentHeight
                newWidth = CurrentWidth
            End If

            Dim sz As Size

            sz.Width = newWidth
            sz.Height = newHeight

            Return sz

        End Function

        ''' <summary>
        ''' Escala una imagen a una ventana
        ''' </summary>
        ''' <param name="CurrentWidth">Ancho actual de la imagen</param>
        ''' <param name="CurrentHeight">Alto actual de la imagen</param>
        ''' <param name="WindowWidth">Ancho deseado de la imagen</param>
        ''' <param name="WindowHeight">Alto deseado de la imagen</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetFitToWindowDimensions(ByVal CurrentWidth As Integer, ByVal CurrentHeight As Integer, ByVal WindowWidth As Integer, ByVal WindowHeight As Integer) As Size
            Dim liChange As Single
            Dim newHeight As Integer
            Dim newWidth As Integer

            If CurrentHeight = CurrentWidth Then
                'image is square...
                'we can use either height or width to resize....
                'use height
                liChange = WindowHeight / CurrentHeight
                newHeight = CurrentHeight * liChange
                newWidth = CurrentWidth * liChange

            ElseIf (WindowHeight / CurrentHeight) > (WindowWidth / CurrentWidth) Then
                liChange = WindowWidth / CurrentWidth
                newHeight = CurrentHeight * liChange
                newWidth = WindowWidth

            Else
                liChange = WindowHeight / CurrentHeight
                newHeight = WindowHeight
                newWidth = CurrentWidth * liChange

            End If

            If CurrentHeight <= WindowHeight And CurrentWidth <= WindowWidth Then
                newHeight = CurrentHeight
                newWidth = CurrentWidth
            End If

            Dim sz As Size

            sz.Width = newWidth
            sz.Height = newHeight

            Return sz
        End Function

        Public ReadOnly Property Identifier() As String Implements IHandlerArchivo.Identifier
            Get
                Return "Images"
            End Get
        End Property

        Public ReadOnly Property SupportedExtension() As String() Implements IHandlerArchivo.SupportedExtension
            Get
                Return _SupportedExtensions
            End Get
        End Property

        Public Function ApplyConfig(ByVal orignalFilePath As String, ByVal theArchivoConfiguration As Entity.Archivos.Archivo_Configuration, ByVal thumbFilePath As String) As Boolean Implements IHandlerArchivo.ApplyConfig
            Dim imageSize As Size = New Size(theArchivoConfiguration.ImgWidth.Value, theArchivoConfiguration.ImgHeight.Value)
            Dim thumbSize As Size = New Size(theArchivoConfiguration.ThumWidth.Value, theArchivoConfiguration.ThumHeight.Value)
            ResizeImageAndThumb(orignalFilePath, thumbFilePath, imageSize, thumbSize, Me.GetFormatFromExtension(Path.GetExtension(orignalFilePath)))
        End Function
    End Class

End Namespace