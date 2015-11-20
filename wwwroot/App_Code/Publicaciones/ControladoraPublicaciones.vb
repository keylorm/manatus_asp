Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler
Imports Orbelink.Service
Imports Orbelink.Control.Archivos
Imports System.Data
Imports System.Data.SqlClient
Imports Orbelink.Entity.Archivos
Imports Orbelink.Entity.Publicaciones
Imports Orbelink.Entity.Entidades

Namespace Orbelink.Control.Publicaciones

    Public Class ControladoraPublicaciones

        Dim _connString As String

        Sub New(ByRef theConnectionString As String)
            _connString = theConnectionString
        End Sub


        Public Shared ReadOnly Property Config_PublicationViewer() As String
            Get
                'Para quitar esta exception, debe comentar esta linea y en el return escribir el valor correspondiente
                Throw New NotImplementedException("Debe especificar el valor correspondiente.")
                Return "Orbecatalog/Publicacion/Publicacion.aspx"
            End Get
        End Property

        Public Shared ReadOnly Property Config_PublicationItemIdentifier() As String
            Get
                'Para quitar esta exception, debe comentar esta linea y en el return escribir el valor correspondiente
                Throw New NotImplementedException("Debe especificar el valor correspondiente.")
                Return "id_Publicacion"
            End Get
        End Property

        'Publicacion
        Public Function ConsultarPublicacion(ByVal id_Publicacion As Integer) As Publicacion
            Dim dataSet As Data.DataSet
            Dim Publicacion As New Publicacion
            Dim connection As New SQLServer(_connString)
            Dim query As New QueryBuilder

            Publicacion.Fields.SelectAll()
            Publicacion.Id_Publicacion.Where.EqualCondition(id_Publicacion)
            query.From.Add(Publicacion)

            dataSet = connection.executeSelect(query.RelationalSelectQuery)

            If dataSet.Tables.Count > 0 Then
                If dataSet.Tables(0).Rows.Count > 0 Then
                    ObjectBuilder.CreateObject(dataSet.Tables(0), 0, Publicacion)
                    Return Publicacion
                End If
            End If
            Return Nothing
        End Function

        Public Function EliminarPublicacion(ByVal id_Publicacion As Integer) As Boolean
            If deleteArchivo_Publicacion(id_Publicacion) Then
                Return deletePublicacion(id_Publicacion)
            End If
            Return False
        End Function

        'Publicacion privadas
        Private Function deletePublicacion(ByVal id_Publicacion As Integer) As Boolean
            Dim publicacion As New Publicacion
            Try
                Dim connection As New SQLServer(_connString)
                Dim query As New QueryBuilder
                publicacion.Id_Publicacion.Where.EqualCondition(id_Publicacion)
                If connection.executeDelete(query.DeleteQuery(publicacion)) = 1 Then
                    Return True
                End If
            Catch exc As Exception
            End Try
            Return False
        End Function

        'Publicaciones select
        Public Function selectPublicaciones_DataList(ByRef theDataList As DataList, ByVal level As Integer, ByVal publicationPage As String, _
             ByVal id_tipoEntidada As Integer, ByVal id_entidad As Integer, ByVal id_tipoPublicacion As Integer, ByVal top As Integer, _
             ByVal orderByFechaImportante As Boolean, ByVal specificDate As String, ByVal showThumbs As Boolean) As Boolean
            Dim dataSet As Data.DataSet
            Dim publicacion As New Publicacion
            'Dim permisos As New Publicacion_TipoEntidad
            'Dim Publicacion_Grupo As New Publicacion_Grupo
            'Dim Entidades_Grupo As New Entidades_Grupo
            Dim prefijoNivel As String = ""
            Dim devolver As Boolean
            Dim byDate As Boolean = False
            Dim Archivo_Publicacion As New Archivo_Publicacion
            Dim archivos As New Archivo

            Dim connection As New SQLServer(_connString)
            Dim query As New QueryBuilder

            For counter As Integer = 0 To level - 1
                prefijoNivel &= "../"
            Next

            publicacion.Id_Publicacion.ToSelect = True
            publicacion.Titulo.ToSelect = True
            publicacion.Fecha.ToSelect = True
            publicacion.Corta.ToSelect = True
            publicacion.Aprobada.Where.EqualCondition(1)
            publicacion.Visible.Where.EqualCondition(1)
            publicacion.FechaInicio.ToSelect = True

            query.From.Add(publicacion)

            If Not specificDate Is Nothing Then
                If specificDate.Length > 0 Then
                    Try
                        Dim theDate As Date = specificDate
                        publicacion.FechaInicio.Where.GreaterThanOrEqualCondition(theDate)
                        publicacion.FechaInicio.Where.LessThanCondition(theDate.AddDays(1))
                        byDate = True
                    Catch ex As Exception
                    End Try
                End If
            End If

            'If id_entidad <> 0 Then
            'query.Join.EqualCondition(Publicacion_Grupo.Id_TipoRelacion, Entidades_Grupo.Id_TipoRelacion, Join.JoinTypes.RIGHT_OUTER_JOIN)
            'query.Join.EqualCondition(Publicacion_Grupo.Id_Grupo, Entidades_Grupo.Id_Grupo, Join.JoinTypes.RIGHT_OUTER_JOIN)

            'Entidades_Grupo.Id_Entidad.Where.EqualCondition(id_entidad)

            'query.From.Add(Entidades_Grupo)
            'query.From.Add(Publicacion_Grupo)
            'End If

            'If id_tipoEntidad <> 0 Then
            'permisos.id_TipoEntidad.Where.EqualCondition(id_tipoEntidad)
            'query.From.Add(permisos)
            'End If

            'Agrupa las condiciones de la publicacion
            'If id_entidad <> 0 Then
            '    query.Join.EqualCondition(publicacion.Id_Publicacion, Publicacion_Grupo.Id_Publicacion, Join.JoinTypes.RIGHT_OUTER_JOIN)

            '    'publicacion.Id_Publicacion.Where.EqualCondition(Publicacion_Grupo.Id_Publicacion)
            '    If id_tipoEntidad <> 0 Then
            '        query.Join.EqualCondition(publicacion.Id_Publicacion, permisos.id_Publicacion, Join.JoinTypes.RIGHT_OUTER_JOIN)
            '        'publicacion.Id_Publicacion.Where.EqualCondition(permisos.id_Publicacion, Orbelink.DBHandler.Where.FieldRelations.OR_)
            '    End If
            'Else
            '    If id_tipoEntidad <> 0 Then
            '        query.Join.EqualCondition(publicacion.Id_Publicacion, permisos.id_Publicacion)
            '    End If
            'End If

            If id_tipoPublicacion <> 0 Then
                publicacion.Id_tipoPublicacion.Where.EqualCondition(id_tipoPublicacion)
                If id_entidad <> 0 Then
                    'publicacion.id_Entidad.Where.EqualCondition(id_entidad, Where.FieldRelations.OR_)
                    'queryBuilder.GroupFieldsConditions(publicacion.id_Entidad, publicacion.Id_tipoPublicacion)
                End If
            End If


            If orderByFechaImportante Then
                query.Orderby.Add(publicacion.FechaInicio, False)
            Else
                query.Orderby.Add(publicacion.Fecha, False)
            End If

            'Archivos
            archivos.FileName.ToSelect = True
            archivos.Extension.ToSelect = True
            query.Join.EqualCondition(Archivo_Publicacion.id_Publicacion, publicacion.Id_Publicacion, Join.JoinTypes.RIGHT_OUTER_JOIN)
            Archivo_Publicacion.Principal.Where.EqualCondition(1)
            Archivo_Publicacion.Principal.Where.IsNullCondition(Where.FieldRelations.OR_)
            query.Join.EqualCondition(Archivo_Publicacion.id_Archivo, archivos.Id_Archivo, Join.JoinTypes.LEFT_OUTER_JOIN)

            query.From.Add(Archivo_Publicacion)
            query.From.Add(archivos)

            'General
            query.Distinct = True
            query.Top = top

            Dim elquery As String = query.RelationalSelectQuery
            dataSet = connection.executeSelect(elquery)
            If dataSet.Tables.Count > 0 Then
                If dataSet.Tables(0).Rows.Count > 0 Then
                    theDataList.DataSource = dataSet
                    theDataList.DataKeyField = publicacion.Id_Publicacion.Name
                    theDataList.DataBind()

                    'Llena el grid
                    Dim results_Publicaciones As ArrayList = ObjectBuilder.TransformDataTable(dataSet.Tables(0), publicacion)
                    Dim results_Archivos As ArrayList = ObjectBuilder.TransformDataTable(dataSet.Tables(0), archivos)

                    For counter As Integer = 0 To theDataList.Items.Count - 1
                        Dim act_Publicacion As Publicacion = results_Publicaciones(counter)
                        Dim act_Archivo As Archivo = results_Archivos(counter)

                        Dim lnk_Titulo As HyperLink = theDataList.Items(counter).FindControl("lnk_Titulo")
                        Dim lbl_Corta As Label = theDataList.Items(counter).FindControl("lbl_Corta")
                        Dim lbl_Fecha As Label = theDataList.Items(counter).FindControl("lbl_Fecha")
                        Dim lbl_FechaImportante As Label = theDataList.Items(counter).FindControl("lbl_FechaImportante")
                        Dim img_Publicacion As UI.WebControls.Image = theDataList.Items(counter).FindControl("img_Publicacion")

                        If Not img_Publicacion Is Nothing Then
                            If act_Archivo.FileName.IsValid Then
                                Orbelink.Control.Archivos.ArchivoHandler.GetArchivoImage(img_Publicacion, act_Archivo.FileName.Value, act_Archivo.Extension.Value, level, True)
                            Else
                                img_Publicacion.Visible = False
                            End If
                        End If

                        If Not lbl_FechaImportante Is Nothing Then
                            If orderByFechaImportante Then
                                If byDate Then
                                    lbl_FechaImportante.Text = act_Publicacion.FechaInicio.ValueLocalized
                                    'lbl_Corta.Visible = False
                                Else
                                    lbl_FechaImportante.Text = "Para " & act_Publicacion.FechaInicio.ValueLocalized & ", desde "
                                End If
                            Else
                                lbl_FechaImportante.Visible = False
                            End If
                        End If
                        lnk_Titulo.Text = act_Publicacion.Titulo.Value
                        lnk_Titulo.NavigateUrl = prefijoNivel & publicationPage & "?id_Publicacion=" & act_Publicacion.Id_Publicacion.Value
                        lbl_Corta.Text = act_Publicacion.Corta.Value
                        lbl_Fecha.Text = act_Publicacion.Fecha.ValueLocalized
                    Next
                    theDataList.Visible = True
                    devolver = True
                Else
                    theDataList.Visible = False
                    devolver = False
                End If
            Else
                theDataList.Visible = False
                devolver = False
            End If
            Return devolver
        End Function

        'Publicacion RSS 
        Function publicarRSS(ByVal id_tipoPublicacion As Integer) As Boolean
            Dim dataset As Data.DataSet
            Dim publicacion As New Publicacion
            Dim tipoPublicacion As New TipoPublicacion
            Dim publicado As Boolean
            Dim rssPublisher As New Orbelink.Helpers.RSSPublisher(Configuration.Config_WebsiteRoot, Configuration.rootPath, "rss")
            Dim connection As New SQLServer(_connString)
            Dim query As New QueryBuilder

            tipoPublicacion.Fields.SelectAll()
            tipoPublicacion.Id_TipoPublicacion.Where.EqualCondition(id_tipoPublicacion)
            dataset = connection.executeSelect(query.SelectQuery(tipoPublicacion))

            If dataset.Tables.Count > 0 Then
                If dataset.Tables(0).Rows.Count > 0 Then
                    ObjectBuilder.CreateObject(dataset.Tables(0), 0, tipoPublicacion)
                    rssPublisher.OpenRssFile(tipoPublicacion.Nombre.Value, tipoPublicacion.Nombre.Value, Config_PublicationViewer, Config_PublicationItemIdentifier, tipoPublicacion.Descripcion.Value, Date.Now)
                    dataset.Tables.Clear()

                    publicacion.Fields.SelectAll()
                    publicacion.Id_tipoPublicacion.Where.EqualCondition(id_tipoPublicacion)
                    publicacion.IncluirRSS.Where.EqualCondition(1)
                    publicacion.Visible.Where.EqualCondition(1)

                    query.Orderby.Add(publicacion.Fecha, False)
                    dataset = connection.executeSelect(query.SelectQuery(publicacion))

                    If dataset.Tables.Count > 0 Then
                        If dataset.Tables(0).Rows.Count > 0 Then
                            Dim results As ArrayList = ObjectBuilder.TransformDataTable(dataset.Tables(0), publicacion)
                            Dim counter As Integer
                            For counter = 0 To results.Count - 1
                                Dim actual As Publicacion = results(counter)
                                rssPublisher.WriteItem(actual.Id_Publicacion.Value, actual.Titulo.Value, actual.Corta.Value, actual.Fecha.ValueUtc, actual.Id_Categoria.Value)
                            Next
                            publicado = True
                            rssPublisher.CloseRssFile()
                        Else
                            publicado = False
                            rssPublisher.CloseRssFile()
                            rssPublisher.DeleteRssFile(tipoPublicacion.Nombre.Value)
                        End If
                    End If
                End If
            End If
            Return publicado
        End Function




        Public Function selectPublicacion(ByVal id_Publicacion As Integer, Optional ByVal incluirCategoria As Boolean = False, Optional ByVal incluirTipoPublicacion As Boolean = False, Optional ByVal incluirEntidad As Boolean = False, Optional ByVal incluirEstadoPublicacion As Boolean = False) As Data.DataSet
            Dim dataSet As Data.DataSet
            Dim Publicacion As New Publicacion
            Dim connection As New SQLServer(_connString)
            Dim query As New QueryBuilder

            Publicacion.Fields.SelectAll()
            Publicacion.Id_Publicacion.Where.EqualCondition(id_Publicacion)
            query.From.Add(Publicacion)

            If incluirEstadoPublicacion Then
                Dim Estado_Publicacion As New Estado_Publicacion
                Estado_Publicacion.Nombre.ToSelect = True
                query.From.Add(Estado_Publicacion)
            End If
            If incluirCategoria Then
                Dim Categorias As New Categorias
                Categorias.Nombre.ToSelect = True
                query.From.Add(Categorias)
            End If
            If incluirTipoPublicacion Then
                Dim TipoPublicacion As New TipoPublicacion
                TipoPublicacion.Nombre.ToSelect = True
                query.From.Add(TipoPublicacion)
            End If
            If incluirEntidad Then
                Dim Entidad As New Entidad
                Entidad.NombreDisplay.ToSelect = True
                query.From.Add(Entidad)
            End If
            dataSet = connection.executeSelect(query.RelationalSelectQuery)

            'If dataSet.Tables.Count > 0 Then
            '    If dataSet.Tables(0).Rows.Count > 0 Then
            '        ObjectBuilder.CreateObject(dataSet.Tables(0), 0, Publicacion)
            '        Return Publicacion
            '    End If
            'End If
            Return dataSet
        End Function

        Friend Sub cargarEntidades(ByRef dropdownlist As DropDownList)
            Dim dataSet As New Data.DataSet
            Dim Entidad As New Entidad
            Entidad.NombreDisplay.ToSelect = True
            Entidad.Id_entidad.ToSelect = True

            Dim connection As New SQLServer(_connString)
            Dim query As New QueryBuilder
            Dim consulta As String = Query.SelectQuery(Entidad)
            dataSet = connection.executeSelect(consulta)

            If dataSet.Tables.Count > 0 Then
                If dataSet.Tables(0).Rows.Count > 0 Then
                    dropdownlist.DataSource = dataSet
                    dropdownlist.DataTextField = Entidad.NombreDisplay.Name
                    dropdownlist.DataValueField = Entidad.Id_entidad.Name
                    dropdownlist.DataBind()
                End If
            End If

        End Sub
        Friend Sub cargarEstados(ByRef dropdownlist As DropDownList)
            Dim dataSet As New Data.DataSet
            Dim Estados_Publicacion As New Estado_Publicacion
            Estados_Publicacion.Nombre.ToSelect = True
            Estados_Publicacion.Id_Estado_Publicacion.ToSelect = True

            Dim connection As New SQLServer(_connString)
            Dim query As New QueryBuilder
            Dim consulta As String = query.SelectQuery(Estados_Publicacion)
            dataSet = connection.executeSelect(consulta)

            If dataSet.Tables.Count > 0 Then
                If dataSet.Tables(0).Rows.Count > 0 Then
                    dropdownlist.DataSource = dataSet
                    dropdownlist.DataTextField = Estados_Publicacion.Nombre.Name
                    dropdownlist.DataValueField = Estados_Publicacion.Id_Estado_Publicacion.Name
                    dropdownlist.DataBind()
                End If
            End If

        End Sub
        Friend Sub cargarCategorias(ByRef dropdownlist As DropDownList, Optional ByVal id_Padre As Integer = 0)
            Dim dataSet As New Data.DataSet
            Dim Categorias As New Categorias
            Categorias.Nombre.ToSelect = True
            Categorias.Id_Categoria.ToSelect = True
            If id_Padre = 0 Then
                Categorias.Id_padre.Where.EqualCondition_OnSameTable(Categorias.Id_Categoria)
            Else
                Categorias.Id_padre.Where.EqualCondition(id_Padre)
                Categorias.Id_Categoria.Where.DiferentCondition(id_Padre)
            End If

            Dim connection As New SQLServer(_connString)
            Dim query As New QueryBuilder
            Dim consulta As String = query.SelectQuery(Categorias)
            dataSet = connection.executeSelect(consulta)
            If dataSet.Tables.Count > 0 Then
                If dataSet.Tables(0).Rows.Count > 0 Then
                    dropdownlist.DataSource = dataSet
                    dropdownlist.DataTextField = Categorias.Nombre.Name
                    dropdownlist.DataValueField = Categorias.Id_Categoria.Name
                    dropdownlist.DataBind()
                End If
            End If
        End Sub
        Friend Sub cargarTipoPublicacion(ByRef dropdownlist As DropDownList)
            Dim dataSet As New Data.DataSet
            Dim TipoPublicacion As New TipoPublicacion
            TipoPublicacion.Nombre.ToSelect = True
            TipoPublicacion.Id_TipoPublicacion.ToSelect = True

            Dim connection As New SQLServer(_connString)
            Dim query As New QueryBuilder
            Dim consulta As String = query.SelectQuery(TipoPublicacion)
            dataSet = connection.executeSelect(consulta)
            If dataSet.Tables.Count > 0 Then
                If dataSet.Tables(0).Rows.Count > 0 Then
                    dropdownlist.DataSource = dataSet
                    dropdownlist.DataTextField = TipoPublicacion.Nombre.Name
                    dropdownlist.DataValueField = TipoPublicacion.Id_TipoPublicacion.Name
                    dropdownlist.DataBind()
                End If
            End If

        End Sub




        ''' <summary>
        ''' Agrega todos los atributos que esten relacionados con algun producto
        ''' </summary>
        ''' <param name="dataset"></param>
        ''' <param name="querystringPublicaciones"></param>
        ''' <remarks></remarks>
        Public Sub AgregarAtributosPublicaciones(ByRef dataset As Data.DataSet, ByVal querystringPublicaciones As String)
            Dim Publicacion As New Publicacion
            If querystringPublicaciones.Length > 1 Then
                querystringPublicaciones = querystringPublicaciones.Remove(0, querystringPublicaciones.IndexOf("FROM"))
                querystringPublicaciones = querystringPublicaciones.Insert(0, "Select  " & Publicacion.TableName & "." & Publicacion.Id_Publicacion.Name & " ")
                querystringPublicaciones = querystringPublicaciones.Remove(querystringPublicaciones.LastIndexOf("ORDER"))
            End If
            Dim Atributos_Publicacion As New Atributos_Publicacion
            Dim Atributos_Para_Publicaciones As New Atributos_Para_Publicaciones
            Dim listaAtrubutos As Data.DataTable = selectAtributosParaPublicaciones()
            If listaAtrubutos IsNot Nothing Then
                If listaAtrubutos.Rows.Count > 0 Then
                    For countAtributos As Integer = 0 To listaAtrubutos.Rows.Count - 1
                        ObjectBuilder.CreateObject(listaAtrubutos, countAtributos, Atributos_Para_Publicaciones)
                        Dim datasetAtributos As Data.DataTable = selectAtributosPublicaciones(querystringPublicaciones, Atributos_Para_Publicaciones.Id_atributo.Value)
                        If datasetAtributos IsNot Nothing Then
                            If datasetAtributos.Rows.Count > 0 Then
                                Dim columName As String = Atributos_Publicacion.Id_atributo.Name & Atributos_Para_Publicaciones.Id_atributo.Value
                                Dim X As New Data.DataColumn(columName, Type.GetType("System.String"))
                                dataset.Tables(0).Columns.Add(X)
                                For countAtributosPublicaciones As Integer = 0 To datasetAtributos.Rows.Count - 1
                                    For countProductos As Integer = 0 To dataset.Tables(0).Rows.Count - 1
                                        If datasetAtributos.Rows(countAtributosPublicaciones).Item(Atributos_Publicacion.Id_publicacion.Name) = dataset.Tables(0).Rows(countProductos).Item(Publicacion.Id_Publicacion.Name) Then
                                            dataset.Tables(0).Rows(countProductos).Item(columName) = datasetAtributos.Rows(countAtributosPublicaciones).Item(Atributos_Publicacion.Valor.Name)
                                            Exit For
                                        End If
                                    Next
                                Next
                            End If
                        End If
                    Next
                End If
            End If

        End Sub
        ''' <summary>
        '''   Agrega un atributos especifico como una columna mas y se le puede cambiar ese nombre cambiando el parametro Nombre_Atributo
        ''' </summary>
        ''' <param name="dataset"></param>
        ''' <param name="querystringPublicaciones"></param>
        ''' <param name="idAtributo"></param>
        ''' <param name="Nombre_Atributo"></param>
        ''' <remarks></remarks>
        Public Sub AgregarAtributosPublicaciones(ByRef dataset As Data.DataSet, ByVal querystringPublicaciones As String, ByVal idAtributo As Integer, Optional ByVal Nombre_Atributo As String = "id_atributo & Atributos_Producto.Id_atributo.value")
            Dim Publicacion As New Publicacion
            querystringPublicaciones = querystringPublicaciones.Remove(0, querystringPublicaciones.IndexOf("FROM"))
            querystringPublicaciones = querystringPublicaciones.Insert(0, "Select  " & Publicacion.TableName & "." & Publicacion.Id_Publicacion.Name & " ")
            querystringPublicaciones = querystringPublicaciones.Remove(querystringPublicaciones.LastIndexOf("ORDER"))
            Dim Atributos_Publicacion As New Atributos_Publicacion
            Dim Atributos_Para_Publicaciones As New Atributos_Para_Publicaciones

            Dim datasetAtributos As Data.DataTable = selectAtributosPublicaciones(querystringPublicaciones, idAtributo)
            If datasetAtributos IsNot Nothing Then
                If datasetAtributos.Rows.Count > 0 Then
                    Dim columName As String = ""
                    If Nombre_Atributo.Length > 0 Then
                        columName = Nombre_Atributo
                    Else
                        columName = Atributos_Publicacion.Id_atributo.Name & idAtributo
                    End If
                    If dataset.Tables(0).Columns(columName) IsNot Nothing Then
                        Throw New Exception("Error la columna " & columName & " ya existe")
                    Else
                        Dim X As New Data.DataColumn(columName, Type.GetType("System.String"))
                        dataset.Tables(0).Columns.Add(X)
                        For countAtributos As Integer = 0 To datasetAtributos.Rows.Count - 1
                            For countProductos As Integer = 0 To dataset.Tables(0).Rows.Count - 1
                                If datasetAtributos.Rows(countAtributos).Item(Atributos_Publicacion.Id_publicacion.Name) = dataset.Tables(0).Rows(countProductos).Item(Publicacion.Id_Publicacion.Name) Then
                                    dataset.Tables(0).Rows(countProductos).Item(columName) = datasetAtributos.Rows(countAtributos).Item(Atributos_Publicacion.Valor.Name)
                                    Exit For
                                End If
                            Next
                        Next
                    End If
                End If
            End If
        End Sub
        Public Function selectAtributosPublicaciones(ByVal querystringPublicacions As String, Optional ByVal Id_atributo As Integer = 0) As Data.DataTable
            Dim connection As New SQLServer(_connString)
            Dim query As New QueryBuilder
            Dim dataset As Data.DataSet

            Dim Atributos_Publicacion As New Atributos_Publicacion
            Dim Atributos_Para_Publicacions As New Atributos_Para_Publicaciones

            If Id_atributo <> 0 Then
                Atributos_Para_Publicacions.Id_atributo.Where.EqualCondition(Id_atributo)
            End If
            Atributos_Publicacion.Id_publicacion.Where.InCondition(querystringPublicacions)
            Atributos_Publicacion.Valor.ToSelect = True
            Atributos_Publicacion.Id_publicacion.ToSelect = True
            Query.Join.EqualCondition(Atributos_Publicacion.Id_atributo, Atributos_Para_Publicacions.Id_atributo)

            Query.From.Add(Atributos_Publicacion)
            Query.From.Add(Atributos_Para_Publicacions)
            Dim result As String = query.RelationalSelectQuery
            dataset = connection.executeSelect(result)

            If dataset.Tables.Count > 0 Then
                If dataset.Tables(0).Rows.Count > 0 Then
                    Return dataset.Tables(0)
                Else
                    Return Nothing
                End If
            Else
                Return Nothing
            End If
        End Function
        Public Function selectAtributosParaPublicaciones() As Data.DataTable

            Dim dataset As Data.DataSet
            Dim Atributos_Para_Publicacions As New Atributos_Para_Publicaciones
            Atributos_Para_Publicacions.Id_atributo.ToSelect = True

            Dim connection As New SQLServer(_connString)
            Dim query As New QueryBuilder
            Query.From.Add(Atributos_Para_Publicacions)
            Dim result As String = query.RelationalSelectQuery
            dataset = connection.executeSelect(result)

            If dataset.Tables.Count > 0 Then
                If dataset.Tables(0).Rows.Count > 0 Then
                    Return dataset.Tables(0)
                Else
                    Return Nothing
                End If
            Else
                Return Nothing
            End If
        End Function
      
        'Archivo_Publicacion
        Private Function deleteArchivo_Publicacion(ByVal id_Publicacion As Integer) As Boolean
            Dim Archivo_Publicacion As New Archivo_Publicacion
            Dim counter As Integer
            Dim deleted As Boolean = True
            Try
                Dim connection As New SQLServer(_connString)
                Dim query As New QueryBuilder
                Archivo_Publicacion.id_Publicacion.Where.EqualCondition(id_Publicacion)
                Archivo_Publicacion.id_Archivo.ToSelect = True

                query.From.Add(Archivo_Publicacion)
                Dim dataTable As Data.DataTable = connection.executeSelect_DT(query.RelationalSelectQuery)

                Dim archivos As New ArchivoHandler(connection)

                If dataTable.Rows.Count > 0 Then
                    Dim results_Archivos As ArrayList = ObjectBuilder.TransformDataTable(dataTable, Archivo_Publicacion)
                    For counter = 0 To dataTable.Rows.Count - 1
                        Dim act_Archivos As Archivo_Publicacion = results_Archivos(counter)

                        If archivos.DeleteArchivo(act_Archivos.id_Archivo.Value) Then
                            'Borrar registro 
                            Archivo_Publicacion.id_Archivo.Where.EqualCondition(act_Archivos.id_Archivo.Value)
                            connection.executeDelete(query.DeleteQuery(Archivo_Publicacion))
                        End If
                    Next
                End If
            Catch exc As Exception
                deleted = False
            End Try
            Return deleted
        End Function
    End Class
End Namespace