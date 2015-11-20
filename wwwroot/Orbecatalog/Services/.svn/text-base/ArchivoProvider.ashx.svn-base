<%@ WebHandler Language="VB" Class="ArchivoProvider" %>

Imports System
Imports System.Web

Public Class ArchivoProvider : Implements IHttpHandler
    
    Dim orbeConfig As Configuration
    
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        orbeConfig = New Configuration
        context.Response.ContentType = "text/xml"
        context.Response.ContentEncoding = System.Text.Encoding.UTF8
        context.Response.Cache.SetExpires(Date.UtcNow.AddSeconds(600))
        context.Response.Cache.SetCacheability(HttpCacheability.Public)
        
        Dim elXML As New System.Xml.XmlTextWriter(context.Response.OutputStream, Nothing)
        elXML.WriteStartDocument()
        elXML.WriteStartElement("itemlist")

        'Carga xml segun paramatros
        Dim id_Publicacion As Integer = 0
        Dim absoluteUrl As Boolean = True
        Dim level As Integer = 0
        Dim ArchivoType As Orbelink.Control.Archivos.ArchivoHandler.ArchivoType
        
        If Not context.Request.Params("id_TipoPublicacion") Is Nothing Then
            id_Publicacion = selectPublicacion_FromTipoPublicacion(context.Request.Params("id_TipoPublicacion"))
        End If

        If Not context.Request.Params("id_Publicacion") Is Nothing Then
            id_Publicacion = context.Request.Params("id_Publicacion")
        End If

        If Not context.Request.Params("absoluteUrl") Is Nothing Then
            absoluteUrl = context.Request.Params("absoluteUrl")
        End If

        If Not context.Request.Params("ArchivoType") Is Nothing Then
            ArchivoType = context.Request.Params("ArchivoType")
        End If
        
        If Not context.Request.Params("level") Is Nothing Then
            level = context.Request.Params("level")
        End If

        If id_Publicacion > 0 Then
            selectArchivo_Publicacion(id_Publicacion, level, absoluteUrl, ArchivoType)
        End If

        'Responde el xml
        elXML.WriteEndElement()
        elXML.WriteEndDocument()
        elXML.Close()
    End Sub
 
    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return True
        End Get
    End Property

    Protected Sub selectArchivo_Publicacion(ByVal id_Publicacion As Integer, ByVal level As Integer, ByVal AbsoluteURL As Boolean, _
        ByVal theArchivoType As Orbelink.Control.Archivos.ArchivoHandler.ArchivoType)
        'Dim dataSet As Data.DataSet
        'Dim Archivo_Publicacion As New Archivo_Publicacion
        'Dim publicacion As New Publicacion

        'Archivo_Publicacion.Fields.SelectAll()
        'Archivo_Publicacion.Id_Publicacion.Where.EqualCondition(id_Publicacion)
        'Archivo_Publicacion.FileType.Where.EqualCondition(theArchivoType)

        'queryBuilder.From.Add(Archivo_Publicacion)
        'dataSet = connection.executeSelect(queryBuilder.RelationalSelectQuery)

        'If dataSet.Tables.Count > 0 Then
        '    If dataSet.Tables(0).Rows.Count > 0 Then
        '        Dim resultados As ArrayList = objectBuilder.TransformDataTable(dataSet.Tables(0), Archivo_Publicacion)

        '        'Llena el grid
        '        For counter As Integer = 0 To resultados.Count - 1
        '            Dim act_Archivo_Publicacion As Archivo_Publicacion = resultados(counter)

        '            Dim ArchivoURL As String = Orbelink.Control.Archivos.ArchivoHandler.GetArchivoURL(Configuration.ArchivoProperties, Configuration.publicacion_ArchivoId, act_Archivo_Publicacion.FileName.Value, act_Archivo_Publicacion.Extension.Value, level, AbsoluteURL)

        '            elXML.WriteStartElement("item")
        '            elXML.WriteAttributeString("name", act_Archivo_Publicacion.Nombre.Value)
        '            elXML.WriteAttributeString("ArchivoURL", ArchivoURL)
        '            elXML.WriteAttributeString("comment", act_Archivo_Publicacion.Comentario.Value)
        '            elXML.WriteAttributeString("extension", act_Archivo_Publicacion.Extension.Value)
        '            elXML.WriteString(act_Archivo_Publicacion.Comentario.Value)
        '            elXML.WriteEndElement()
        '        Next
        '    End If
        'End If
    End Sub

    Protected Function selectPublicacion_FromTipoPublicacion(ByVal id_TipoPublicacion As Integer) As Integer
        'Dim dataSet As Data.DataSet
        'Dim Archivo_Publicacion As New Archivo_Publicacion
        'Dim publicacion As New Publicacion
        Dim id_Publicacion As Integer

        'publicacion.Id_Publicacion.ToSelect = True
        'publicacion.Id_tipoPublicacion.Where.EqualCondition(id_TipoPublicacion)
        'publicacion.Visible.Where.EqualCondition(1)

        'Archivo_Publicacion.Id_Publicacion.Where.EqualCondition(publicacion.Id_Publicacion)
        ''Archivo_Publicacion.FileType.Where.EqualCondition(ArchivoHandler.ArchivoType.ImageType)

        'queryBuilder.From.Add(Archivo_Publicacion)
        'queryBuilder.From.Add(publicacion)
        'queryBuilder.OrderBy.Add(publicacion.Id_Publicacion, False)
        'dataSet = connection.executeSelect(queryBuilder.RelationalSelectQuery)

        'If dataSet.Tables.Count > 0 Then
        '    If dataSet.Tables(0).Rows.Count > 0 Then
        '        objectBuilder.CreateObject(dataSet.Tables(0), 0, publicacion)
        '        id_Publicacion = publicacion.Id_Publicacion.Value
        '    End If
        'End If
        Return id_Publicacion
    End Function
 
End Class