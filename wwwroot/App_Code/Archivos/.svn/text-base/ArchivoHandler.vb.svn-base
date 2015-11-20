Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler
Imports System.IO
Imports System.Web.HttpContext
Imports Orbelink.Entity.Archivos

Namespace Orbelink.Control.Archivos

    ''' <summary>
    ''' Controla lo referente a archivos Archivo
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ArchivoHandler

        Public Const classIdentifier As String = "Orbelink.Control.Archivos.ArchivoHandler Error: "

        Public Enum whatToDo As Integer
            DoNothing = 0
            ForceIt = 1
            OverrideIt = 2
        End Enum

        Shared ReadOnly Property BasePath() As String
            Get
                Return System.AppDomain.CurrentDomain.BaseDirectory
            End Get
        End Property

        Public Enum ArchivoType As Integer
            ImageType = 0
            DocumentType = 1
            VideoType = 2
            AudioType = 3
            FlashType = 4
        End Enum

        Dim connection As SQLServer
        Dim queryBuilder As QueryBuilder

        Sub New(ByRef theConnection As SQLServer)
            connection = theConnection
            queryBuilder = New QueryBuilder
        End Sub

        'Functions
        ''' <summary>
        ''' Salva un archivo desde un FileUpload en la ruta correspondiente según su tipo. (Tiene opción de sobreescribir)
        ''' </summary>
        ''' <param name="fileUpload">El FileUpload donde se encuentra el archivo</param>
        ''' <param name="newfileName">Nuevo nombre del archivo. Si viene vacio usa el original.</param>
        ''' <param name="what">Que hacer: nada, forzarlo o sobreescribir</param>
        ''' <returns>Si fue salvado</returns>
        ''' <remarks></remarks>
        Private Function SaveFile(ByRef fileUpload As FileUpload, ByVal newfileName As String, ByVal what As whatToDo) As ArchivoInfo
            Dim exists As Boolean
            Dim counter As Integer
            Dim variation As String = ""
            Dim savedFileName As String = ""
            Dim savedFilePath As String = ""

            If fileUpload.HasFile Then
                'Dim fileInfo As New FileInfo(fileUpload.FileName)
                Dim realFileName As String = fileUpload.FileName.Substring(0, fileUpload.FileName.LastIndexOf("."))
                Dim fileExtension As String = fileUpload.FileName.Substring(fileUpload.FileName.LastIndexOf("."))
                Dim archivoTypeActual As ArchivoType = GetArchivoType(fileExtension)
                Dim directoryPath As String = GetFolder(archivoTypeActual)
                Directory.CreateDirectory(directoryPath)

                If newfileName.Length = 0 Then
                    newfileName = realFileName
                End If
                exists = File.Exists(savedFilePath)

                If what = whatToDo.ForceIt Then
                    Do
                        savedFileName = newfileName & variation & fileExtension
                        savedFilePath = directoryPath & savedFileName
                        exists = File.Exists(savedFilePath)
                        counter += 1
                        variation = "-" & counter
                    Loop While exists
                    fileUpload.SaveAs(savedFilePath)
                End If

                If ((what = whatToDo.DoNothing) And Not exists) Or what = whatToDo.OverrideIt Then
                    savedFileName = newfileName & fileExtension
                    savedFilePath = directoryPath & savedFileName
                    fileUpload.SaveAs(savedFilePath)
                End If
                Return New ArchivoInfo(savedFilePath)
            End If
            Return Nothing
        End Function

        ''' <summary>
        ''' Elimina el archivo de disco. Infiere la ruta segun el tipo
        ''' </summary>
        ''' <param name="fileName"></param>
        ''' <param name="extension"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Shared Function DeleteFile(ByVal fileName As String, ByVal extension As String) As Boolean
            Dim theArchivoType As Integer = GetArchivoType(extension)
            Try
                If File.Exists(GetFolder(theArchivoType) & fileName & "." & extension) Then
                    File.Delete(GetFolder(theArchivoType) & fileName & "." & extension)
                    If File.Exists(GetFolder(theArchivoType, True) & fileName & "." & extension) Then
                        File.Delete(GetFolder(theArchivoType, True) & fileName & "." & extension)
                    End If
                End If
                Return True
            Catch exc As Exception
                Return False
            End Try
        End Function

        'Private Function CopyArchivoFile(ByVal fileName As String, ByVal newfileName As String, ByVal fileExtension As String, ByVal what As whatToDo) As ArchivoInfo
        '    Dim ArchivoInfo As ArchivoInfo = Nothing
        '    If newfileName.Length = 0 Then
        '        newfileName = fileName
        '    End If
        '    newfileName = newfileName.Replace(" ", "_")

        '    Dim theArchivoType As ArchivoType = GetArchivoType(fileExtension)
        '    Dim theHandler As IHandlerArchivo = GetIArchivoHandler(theArchivoType)

        '    If theArchivoType = ArchivoType.ImageType Then
        '        ArchivoInfo = CopyFile(GetFolder(theArchivoType, True), fileName, newfileName, fileExtension, what)
        '    End If
        '    ArchivoInfo = CopyFile(GetFolder(theArchivoType), fileName, newfileName, fileExtension, what)

        '    Return ArchivoInfo
        'End Function

        'Function RenameFile_Forced(ByVal directoryPath As String, ByVal fileName As String, ByVal newfileName As String) As ArchivoInfo
        '    Dim excID As String = classIdentifier & "RenameFile_forced: "
        '    Dim savedFileName As String = ""

        '    If Not directoryPath.Chars(directoryPath.Length - 1) = "\" Then
        '        directoryPath &= "\"
        '    End If

        '    Dim actualFilePath As String = directoryPath & fileName

        '    If File.Exists(actualFilePath) Then
        '        Dim exists As Boolean
        '        Dim counter As Integer
        '        Dim variation As String = ""
        '        Dim fileExtension As String = fileName.Substring(fileName.LastIndexOf("."))

        '        If newfileName.Length = 0 Then
        '            Throw New Exception(excID & "No new file name.")
        '        End If

        '        Do
        '            savedFileName = newfileName & variation
        '            exists = File.Exists(directoryPath & savedFileName)
        '            counter += 1
        '            variation = counter
        '        Loop While exists

        '        File.Move(actualFilePath, directoryPath & savedFileName)
        '    End If

        '    Dim savedArchivoInfo As New ArchivoInfo(directoryPath & savedFileName)
        '    savedArchivoInfo.Saved = True
        '    savedArchivoInfo.Forced = True

        '    Return savedArchivoInfo
        'End Function

        'Function CopyFile(ByVal directoryPath As String, ByVal fileName As String, ByVal newfileName As String, ByVal fileExtension As String, ByVal what As whatToDo) As ArchivoInfo
        '    Dim excID As String = classIdentifier & "RenameFile_forced: "
        '    Dim savedFileName As String = ""
        '    Dim savedArchivoInfo As ArchivoInfo = Nothing

        '    If Not directoryPath.Chars(directoryPath.Length - 1) = "\" Then
        '        directoryPath &= "\"
        '    End If
        '    Directory.CreateDirectory(directoryPath)

        '    Dim actualFilePath As String = directoryPath & fileName & "." & fileExtension

        '    If File.Exists(actualFilePath) Then
        '        Dim exists As Boolean
        '        Dim counter As Integer
        '        Dim variation As String = ""
        '        Dim savedFilePath As String

        '        If newfileName.Length = 0 Then
        '            Throw New Exception(excID & "No new file name.")
        '        End If

        '        If what = whatToDo.ForceIt Then
        '            Do
        '                savedFileName = newfileName & variation & "." & fileExtension
        '                savedFilePath = directoryPath & savedFileName
        '                exists = File.Exists(savedFilePath)
        '                counter += 1
        '                variation = "-" & counter
        '            Loop While exists
        '            File.Copy(actualFilePath, savedFilePath)

        '            savedArchivoInfo = New ArchivoInfo(directoryPath & savedFileName)
        '            savedArchivoInfo.Forced = True
        '            savedArchivoInfo.Saved = True
        '        End If

        '        If ((what = whatToDo.DoNothing) And Not exists) Then
        '            savedFileName = newfileName & "." & fileExtension
        '            savedFilePath = directoryPath & savedFileName
        '            File.Copy(actualFilePath, savedFilePath)

        '            savedArchivoInfo = New ArchivoInfo(directoryPath & savedFileName)
        '            savedArchivoInfo.Forced = True
        '            savedArchivoInfo.Saved = True
        '        End If

        '    End If
        '    Return savedArchivoInfo
        'End Function

        'Publicas 
        Public Function RegistrarArchivo(ByVal controladora As IControladorArchivos, ByRef fileUpload As FileUpload, ByVal id_dueno As Integer, ByVal esPrincipal As Boolean, ByVal nombreArchivo As String, ByVal comentario As String, ByVal IdArchivoConfiguracion As Integer) As Boolean
            Dim resultado As Boolean = False

            If fileUpload.HasFile Then
                Dim archivoInfo As ArchivoInfo = SaveFile(fileUpload, nombreArchivo, whatToDo.ForceIt)
                If archivoInfo IsNot Nothing Then
                    Dim theArchivoConfig As Archivo_Configuration = loadArchivo_Configuration(IdArchivoConfiguracion)
                    Dim handler As IHandlerArchivo = GetIArchivoHandler(archivoInfo.FileType)
                    Dim id_archivo As Integer = 0
                    If theArchivoConfig IsNot Nothing Then
                        handler.ApplyConfig(GetFolder(archivoInfo.FileType) & archivoInfo.FileName & archivoInfo.Extension, theArchivoConfig, GetFolder(archivoInfo.FileType, True) & archivoInfo.FileName & archivoInfo.Extension)
                        id_archivo = insertarArchivo(nombreArchivo, archivoInfo.FileName, comentario, archivoInfo.Extension, archivoInfo.FileSize, archivoInfo.FileType, IdArchivoConfiguracion)
                        If id_archivo > 0 Then

                            If controladora.insertarArchivoIntermedio(id_archivo, id_dueno, IIf(esPrincipal, 1, 0), 1) Then
                                resultado = True
                            End If
                        Else
                            DeleteFile(archivoInfo.FileName, archivoInfo.Extension)
                        End If
                    End If
                End If

                If Not resultado And archivoInfo IsNot Nothing Then
                    DeleteFile(archivoInfo.FileName, archivoInfo.Extension)
                End If

            End If
            Return resultado
        End Function

        'Utils
        ''' <summary>
        ''' Asigna a una imagen la ruta segun su tipo de archivo
        ''' </summary>
        ''' <param name="theImage"></param>
        ''' <param name="fileName"></param>
        ''' <param name="extension"></param>
        ''' <param name="level"></param>
        ''' <param name="thumb"></param>
        ''' <remarks></remarks>
        Public Shared Sub GetArchivoImage(ByRef theImage As System.Web.UI.WebControls.Image, ByVal fileName As String, ByVal extension As String, ByVal level As Integer, ByVal thumb As Boolean)
            Dim theArchivoType As ArchivoType = GetArchivoType(extension)

            If extension.Chars(0) <> "."c Then
                extension = "." & extension
            End If

            If thumb Then
                theImage.ImageUrl = GetDirectory(theArchivoType, level, True) & fileName & extension
            Else
                If theArchivoType = ArchivoType.ImageType Then
                    theImage.ImageUrl = GetDirectory(theArchivoType, level) & fileName & extension
                Else
                    Dim theHandler As IHandlerArchivo = GetIArchivoHandler(theArchivoType)
                    theImage.ImageUrl = "~/orbeCatalog/images/icons/" & theHandler.Identifier & ".png"
                End If
            End If
            theImage.AlternateText = fileName & extension
            theImage.ToolTip = fileName & extension
            theImage.Visible = True
        End Sub

        ''' <summary>
        ''' Retorna la ruta donde se encuentra el archivo segun su tipo
        ''' </summary>
        ''' <param name="theArchivoType"></param>
        ''' <param name="thumb"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetFolder(ByVal theArchivoType As ArchivoType, Optional ByVal thumb As Boolean = False) As String
            Dim theHandler As IHandlerArchivo = GetIArchivoHandler(theArchivoType)
            Dim thefolder As String
            If thumb Then
                thefolder = BasePath & "Files\" & theHandler.Identifier & "\thumbs\"
            Else
                thefolder = BasePath & "Files\" & theHandler.Identifier & "\"
            End If
            Directory.CreateDirectory(thefolder)
            Return thefolder
        End Function

        Public Shared Function GetDirectory(ByVal theArchivoType As ArchivoType, ByVal level As Integer, Optional ByVal thumb As Boolean = False, Optional ByVal AbsoluteURL As Boolean = False) As String
            Dim prefix As String = ""
            If AbsoluteURL Then
                prefix = Configuration.Config_WebsiteRoot
            Else
                For counter As Integer = 0 To level - 1
                    prefix &= "../"
                Next
            End If

            Dim theHandler As IHandlerArchivo = GetIArchivoHandler(theArchivoType)
            If thumb Then
                Return prefix & "Files/" & theHandler.Identifier & "/thumbs/"
            Else
                Return prefix & "Files/" & theHandler.Identifier & "/"
            End If
        End Function

        ''' <summary>
        ''' Retorna el URL del archivo
        ''' </summary>
        ''' <param name="fileName"></param>
        ''' <param name="extension"></param>
        ''' <param name="level"></param>
        ''' <param name="AbsoluteURL"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetArchivoURL(ByVal fileName As String, ByVal extension As String, ByVal level As Integer, ByVal AbsoluteURL As Boolean) As String
            Dim theArchivoType As Integer = GetArchivoType(extension)
            If extension.Chars(0) <> "."c Then
                extension = "." & extension
            End If
            Return GetDirectory(theArchivoType, level, False, AbsoluteURL) & fileName & extension
        End Function

        ''' <summary>
        ''' Retorna el valor de ArchivoType segun la extension
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetIArchivoHandler(ByVal theArchivoType As ArchivoType) As IHandlerArchivo
            Select Case theArchivoType
                Case ArchivoType.ImageType
                    Return New ImagesHandler
                Case ArchivoType.DocumentType
                    Return New DocumentHandler
                Case ArchivoType.VideoType
                    Return New VideoHandler
                Case ArchivoType.AudioType
                    Return New AudioHandler
                Case ArchivoType.FlashType
                    Return New FlashHandler
            End Select
            Return Nothing
        End Function

        ''' <summary>
        ''' Retorna el valor de ArchivoType segun la extension
        ''' </summary>
        ''' <param name="theExtension"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetArchivoType(ByVal theExtension As String) As ArchivoType
            If theExtension.Length > 0 Then
                If theExtension.Chars(0) = "."c Then
                    theExtension = theExtension.Substring(1)
                End If
            End If

            If IsSupportedExtension(New ImagesHandler(), theExtension) Then
                Return ArchivoType.ImageType
            ElseIf IsSupportedExtension(New VideoHandler(), theExtension) Then
                Return ArchivoType.VideoType
            ElseIf IsSupportedExtension(New AudioHandler, theExtension) Then
                Return ArchivoType.AudioType
            ElseIf IsSupportedExtension(New FlashHandler, theExtension) Then
                Return ArchivoType.FlashType
            ElseIf IsSupportedExtension(New DocumentHandler, theExtension) Then
                Return ArchivoType.DocumentType
            Else
                Throw New Exception("No supported extension.")
            End If
        End Function

        'Tools
        ''' <summary>
        ''' Permite descargar un archivo en el cliente. Solo funciona con un Postback y no con ajax
        ''' </summary>
        ''' <param name="fileName"></param>
        ''' <param name="extension"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function DownloadArchivoFile(ByVal fileName As String, ByVal extension As String) As Boolean
            Dim theArchivoType As Integer = GetArchivoType(extension)
            Return DownloadFile(GetFolder(theArchivoType), fileName & extension)
        End Function

        Public Shared Function DownloadFile(ByVal directoryPath As String, ByVal fileName As String) As Boolean
            Dim exists As Boolean
            Dim filePath As String = directoryPath & fileName
            If File.Exists(filePath) Then
                Current.Response.Clear()
                Current.Response.ContentType = "application/octet-stream"
                Current.Response.AddHeader("Content-Disposition", _
                  "attachment; filename=""" & fileName & """")
                Current.Response.Flush()
                Current.Response.WriteFile(filePath)
                Current.Response.End()
                exists = True
            Else
                exists = False
            End If
            Return exists
        End Function

        Public Shared Function AllFilesInFolder(ByVal thePath As String, ByVal extension As String) As ArrayList
            Dim directory As DirectoryInfo = New DirectoryInfo(thePath)
            Dim enumerator As IEnumerator = CType(directory.GetFiles("*." & extension), IEnumerable).GetEnumerator
            Dim files As New ArrayList

            Do While enumerator.MoveNext
                Dim currentName As String
                Dim currentFile As FileInfo = CType(enumerator.Current, FileInfo)
                currentName = currentFile.Name
                files.Add(currentName)
            Loop
            Return files
        End Function

        Public Shared Function IsSupportedExtension(ByVal theHandler As IHandlerArchivo, ByVal theExtension As String) As Boolean
            Dim supported As Boolean = False
            For counter As Integer = 0 To theHandler.SupportedExtension.Length - 1
                If theExtension.ToLower = theHandler.SupportedExtension(counter) Then
                    supported = True
                    Exit For
                End If
            Next
            Return supported
        End Function

        '''' <summary>
        '''' Obtiene todas las fotos del upload folder las coloca en la carpeta de imagenes
        '''' </summary>
        '''' <param name="genericName">Nombre generico para las imagenes</param>
        '''' <param name="extension">Extension de las imagenes buscadas</param>
        '''' <param name="start">Desde donde empieza a contar</param>
        '''' <returns>Cantidad de imagenes encontradas</returns>
        '''' <remarks></remarks>
        'Public Function UploadImages(ByRef theImageSize As Size, ByRef theThumbSize As Size, ByVal genericName As String, ByVal extension As String, Optional ByVal start As Integer = 0) As Integer
        '    Dim directory As DirectoryInfo = New DirectoryInfo(theImageProperties.UploadPath)
        '    Dim enumerator As IEnumerator = CType(directory.GetFiles("*." & extension), IEnumerable).GetEnumerator
        '    Dim currentFile As FileInfo
        '    Dim image_Name As String
        '    Dim file_Name As String
        '    Dim counter As Integer

        '    Do While enumerator.MoveNext
        '        image_Name = ""
        '        Dim numeroTemp As Integer = start + counter
        '        currentFile = CType(enumerator.Current, FileInfo)
        '        If genericName.Length > 0 Then
        '            image_Name = genericName & " "
        '        End If
        '        image_Name &= numeroTemp
        '        file_Name = image_Name & "." & extension
        '        currentFile.CopyTo(theImageProperties.ImagesPath & file_Name)
        '        ResizeImageAndThumb(theImageProperties, theImageSize, theThumbSize, file_Name, GetFormatFromExtension(extension))
        '        File.Delete(theImageProperties.UploadPath & currentFile.Name)
        '        counter = counter + 1
        '    Loop
        '    Return counter
        'End Function

        'BD
        Private Function insertarArchivo(ByVal nombreArchivo As String, ByVal fileName As String, ByVal comentario As String, ByVal extension As String, ByVal sizeInBytes As Integer, ByVal theFileType As Integer, ByVal IdArchivoConfiguracion As Integer) As Integer
            Try
                Dim archivo As New Archivo
                If nombreArchivo.Length > 0 Then
                    archivo.Nombre.Value = nombreArchivo
                Else
                    archivo.Nombre.Value = fileName
                End If
                archivo.Fecha.ValueUtc = Date.UtcNow
                archivo.Comentario.Value = comentario
                archivo.FileName.Value = fileName
                archivo.Extension.Value = extension
                archivo.Size.Value = sizeInBytes
                archivo.FileType.Value = theFileType
                archivo.Id_ArchivoConfiguration.Value = IdArchivoConfiguracion

                connection.executeInsert(queryBuilder.InsertQuery(archivo))
                Return connection.lastKey(archivo.TableName, archivo.Id_Archivo.Name)
            Catch exc As Exception
                Return 0
            End Try
        End Function

        Private Function updateArchivo(ByVal id_Archivo As Integer, ByVal nombreArchivo As String, ByVal fileName As String, ByVal comentario As String, ByVal extension As String, ByVal sizeInBytes As Integer, ByVal theFileType As Integer, ByVal IdArchivoConfiguracion As Integer) As Boolean
            Dim Archivo As New Archivo
            Try
                Archivo.Id_Archivo.Where.EqualCondition(id_Archivo)
                Archivo.Comentario.Value = comentario

                If fileName.Length > 0 And extension.Length > 0 Then
                    Archivo.Nombre.Value = nombreArchivo
                    Archivo.FileName.Value = fileName
                    Archivo.Extension.Value = extension
                    Archivo.Size.Value = sizeInBytes
                    Archivo.FileType.Value = theFileType
                    Archivo.Id_ArchivoConfiguration.Value = IdArchivoConfiguracion
                End If

                connection.executeUpdate(queryBuilder.UpdateQuery(Archivo))
                Return True
            Catch exc As Exception
                Return False
            End Try
        End Function

        ''' <summary>
        ''' Borra el archivo a nivel de tablas
        ''' </summary>
        ''' <param name="id_archivo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function DeleteArchivo(ByVal id_archivo As Integer) As Boolean
            Dim dataSet As Data.DataSet
            Dim archivo As New Archivo
            Try
                Dim borrado As Boolean = False
                archivo.FileName.ToSelect = True
                archivo.Extension.ToSelect = True
                archivo.Id_Archivo.Where.EqualCondition(id_archivo)
                queryBuilder.From.Add(archivo)
                dataSet = connection.executeSelect(queryBuilder.RelationalSelectQuery)
                If dataSet.Tables.Count > 0 Then
                    If dataSet.Tables(0).Rows.Count > 0 Then
                        ObjectBuilder.CreateObject(dataSet.Tables(0), 0, archivo)
                        If DeleteFile(archivo.FileName.Value, archivo.Extension.Value) Then
                            archivo.Id_Archivo.Where.EqualCondition(id_archivo)
                            connection.executeDelete(queryBuilder.DeleteQuery(archivo))
                            Return True
                        End If
                    End If
                End If
            Catch exc As Exception

            End Try
            Return False
        End Function

        Public Function loadArchivo_Configuration(ByVal Id_Configuration As Integer) As Archivo_Configuration
            Dim dataSet As Data.DataSet
            Dim archivo As New Archivo_Configuration
            Try
                archivo.Fields.SelectAll()
                archivo.Id_Configuration.Where.EqualCondition(Id_Configuration)
                queryBuilder.From.Add(archivo)
                dataSet = connection.executeSelect(queryBuilder.RelationalSelectQuery)
                If dataSet.Tables.Count > 0 Then
                    If dataSet.Tables(0).Rows.Count > 0 Then
                        ObjectBuilder.CreateObject(dataSet.Tables(0), 0, archivo)
                        Return archivo
                    End If
                End If
            Catch exc As Exception
            End Try
            Return Nothing
        End Function

    End Class

End Namespace