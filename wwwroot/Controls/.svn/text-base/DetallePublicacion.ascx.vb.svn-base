Imports Orbelink.DBHandler
Imports Orbelink.Entity.Archivos
Imports Orbelink.Entity.Entidades
Imports Orbelink.Entity.Publicaciones
Imports Orbelink.Control.Archivos

Partial Class Controls_DetallePublicacion
    Inherits System.Web.UI.UserControl

    Dim queryBuilder As Orbelink.DBHandler.QueryBuilder
    Dim connection As Orbelink.DBHandler.SQLServer
    Dim securityHandler As Orbelink.Control.Security.SecurityHandler
    Dim codigo_pantalla As String
    Dim MyMaster As Orbelink.Orbecatalog6.MasterPageBaseClass
    Dim scriptMan As ScriptManager
    Dim level As Integer

    Public Sub SetVariables(ByVal theQueryBuilder As Orbelink.DBHandler.QueryBuilder, _
                           ByVal theConnection As Orbelink.DBHandler.SQLServer, _
                           ByVal theSecurityHandler As Orbelink.Control.Security.SecurityHandler, _
                           ByVal theCodigo_pantalla As String, _
                           ByVal theMaster As Orbelink.Orbecatalog6.MasterPageBaseClass, _
                           ByVal theScriptMan As ScriptManager, _
                           ByVal theLevel As Integer)
        queryBuilder = theQueryBuilder
        connection = theConnection
        securityHandler = theSecurityHandler
        codigo_pantalla = theCodigo_pantalla
        MyMaster = theMaster
        scriptMan = thescriptMan
        level = theLevel
    End Sub

    Public Sub cargarInformacion(ByVal id_publicacion As Integer)
        Dim dataSet As New Data.DataSet
        Dim publicacion As New Publicacion
        Dim tipoPublicacion As New TipoPublicacion
        Dim entidad As New Entidad
        Dim categorias As New Categorias
        Dim estado As New Estado_Publicacion
        publicacion.Fields.SelectAll()
        tipoPublicacion.Nombre.ToSelect = True

        entidad.NombreDisplay.ToSelect = True

        categorias.Nombre.ToSelect = True
        estado.Nombre.ToSelect = True

        queryBuilder.Join.EqualCondition(publicacion.Id_tipoPublicacion, tipoPublicacion.Id_TipoPublicacion)
        queryBuilder.Join.EqualCondition(publicacion.id_Entidad, entidad.Id_entidad)
        queryBuilder.Join.EqualCondition(publicacion.Id_Categoria, categorias.Id_Categoria)
        queryBuilder.Join.EqualCondition(publicacion.Id_Estado_Publicacion, estado.Id_Estado_Publicacion)
        publicacion.Id_Publicacion.Where.EqualCondition(id_publicacion)

        queryBuilder.From.Add(publicacion)
        queryBuilder.From.Add(tipoPublicacion)
        queryBuilder.From.Add(entidad)
        queryBuilder.From.Add(categorias)
        queryBuilder.From.Add(estado)

        Dim consulta As String = queryBuilder.RelationalSelectQuery
        dataSet = connection.executeSelect(consulta)
        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables(0).Rows.Count > 0 Then
                ObjectBuilder.CreateObject(dataSet.Tables(0), 0, publicacion)
                ObjectBuilder.CreateObject(dataSet.Tables(0), 0, tipoPublicacion)
                ObjectBuilder.CreateObject(dataSet.Tables(0), 0, entidad)
                ObjectBuilder.CreateObject(dataSet.Tables(0), 0, categorias)
                ObjectBuilder.CreateObject(dataSet.Tables(0), 0, estado)

                Me.lbl_nombre.Text = publicacion.Titulo.Value
                'Me.lbl_nombreTitulo.Text = publicacion.Titulo.Value
                Me.lbl_tipo.Text = tipoPublicacion.Nombre.Value
                'Me.lbl_tipoTitulo.Text = tipoPublicacion.Nombre.Value
                Me.lbl_entidadid.Text = entidad.NombreDisplay.Value
                Me.lbl_descripcion.Text = publicacion.Corta.Value

                If publicacion.FechaInicio.IsValid Then
                    Me.lbl_fechaInicio.Text = publicacion.FechaInicio.ValueLocalized
                End If
                If publicacion.FechaFin.IsValid Then
                    Me.lbl_FechaFin.Text = publicacion.FechaFin.ValueLocalized
                End If
                Me.lbl_categoria.Text = categorias.Nombre.Value
                Me.lbl_estado.Text = estado.Nombre.Value
                cargarImagenes(publicacion.Id_Publicacion.Value)
                cargarAtributosExtra(publicacion.Id_Publicacion.Value)
            End If
        End If
    End Sub

    Protected Sub cargarImagenes(ByVal id_publicacion As Integer)
        Dim archivos As New Archivo
        Dim Archivo_Publicacion As New Archivo_Publicacion
        Dim dataSet As New Data.DataSet

        archivos.Fields.SelectAll()
        Archivo_Publicacion.Principal.ToSelect = True
        queryBuilder.Join.EqualCondition(archivos.Id_Archivo, Archivo_Publicacion.id_Archivo)
        Archivo_Publicacion.id_Publicacion.Where.EqualCondition(id_publicacion)
        queryBuilder.From.Add(archivos)
        queryBuilder.From.Add(Archivo_Publicacion)
        queryBuilder.Top = 3
        queryBuilder.Orderby.Add(Archivo_Publicacion.Principal, False)
        Dim consulta As String = queryBuilder.RelationalSelectQuery
        dataSet = connection.executeSelect(consulta)
        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables(0).Rows.Count > 0 Then
                Dim result_archivos As ArrayList = ObjectBuilder.TransformDataTable(dataSet.Tables(0), archivos)
                Dim result_arch_publi As ArrayList = ObjectBuilder.TransformDataTable(dataSet.Tables(0), Archivo_Publicacion)
                dl_imagenes.DataSource = dataSet
                dl_imagenes.DataBind()
                For counter As Integer = 0 To dataSet.Tables(0).Rows.Count - 1
                    Dim act_archivo As Archivo = result_archivos(counter)
                    Dim act_arch_publi As Archivo_Publicacion = result_arch_publi(counter)
                    Dim imagen As Image = dl_imagenes.Items(counter).FindControl("img_archivo")
                    ArchivoHandler.GetArchivoImage(imagen, act_archivo.FileName.Value, act_archivo.Extension.Value, 1, True)

                    Dim btn_imgNombre As LinkButton = dl_imagenes.Items(counter).FindControl("btn_imgNombre")
                    Dim peso As Label = dl_imagenes.Items(counter).FindControl("lbl_imgTamano")
                    Dim fecha As Label = dl_imagenes.Items(counter).FindControl("lbl_imgFecha")
                    Dim nombre2 As String = act_archivo.Nombre.Value
                    If act_archivo.Nombre.Value.Length > 12 Then
                        nombre2 = nombre2.Substring(0, 12)
                    End If

                    'Hace trigger en el boton para poder descargar
                    btn_imgNombre.Text = nombre2
                    btn_imgNombre.ToolTip = imagen.ToolTip
                    scriptMan.RegisterPostBackControl(btn_imgNombre)

                    peso.Text = act_archivo.Size.Value
                    fecha.Text = act_archivo.Fecha.ValueLocalized

                    If act_arch_publi.Principal.Value > 0 Then
                        'Dim imagenP As New Image
                        ArchivoHandler.GetArchivoImage(img_principal, act_archivo.FileName.Value, act_archivo.Extension.Value, 1, True)
                        'img_principal.ImageUrl = imagenP.ImageUrl
                    End If
                Next
            Else
                Me.archivos.Visible = False
            End If
        Else
            Me.archivos.Visible = False
        End If
    End Sub

    Protected Sub cargarAtributosExtra(ByVal id_publicacion As Integer)
        Dim atributos_Publicacion As New Atributos_Publicacion
        Dim atributos As New Atributos_Para_Publicaciones

        atributos.Nombre.ToSelect = True
        atributos_Publicacion.Valor.ToSelect = True
        queryBuilder.Join.EqualCondition(atributos.Id_atributo, atributos_Publicacion.Id_atributo)
        atributos_Publicacion.Id_publicacion.Where.EqualCondition(id_publicacion)

        queryBuilder.From.Add(atributos_Publicacion)
        queryBuilder.From.Add(atributos)
        Dim consulta As String = queryBuilder.RelationalSelectQuery
        Dim resultTable As Data.DataTable = connection.executeSelect_DT(consulta)

        If resultTable.Rows.Count > 0 Then
            DL_atributos.DataSource = resultTable
            DL_atributos.DataBind()

            Dim resul_atributosEntidad As ArrayList = TransformDataTable(resultTable, atributos_Publicacion)
            Dim resul_atributos As ArrayList = TransformDataTable(resultTable, atributos)

            For counter As Integer = 0 To resultTable.Rows.Count - 1
                Dim act_atributoEntidad As Atributos_Publicacion = resul_atributosEntidad(counter)
                Dim act_atributos As Atributos_Para_Publicaciones = resul_atributos(counter)

                Dim nombre As Label = DL_atributos.Items(counter).FindControl("lbl_nombreAtributo")
                nombre.Text = act_atributos.Nombre.Value & ":"
                Dim valor As Label = DL_atributos.Items(counter).FindControl("lbl_valorAtributo")
                valor.Text = act_atributoEntidad.Valor.Value
            Next
            Me.lbl_atributosExtra.Visible = False
        Else
            Me.lbl_atributosExtra.Visible = True
        End If
    End Sub

    Sub btn_imgNombre_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim btn_imgNombre As LinkButton = sender
        Dim archivo As String() = btn_imgNombre.ToolTip.Split(".")
        ArchivoHandler.DownloadArchivoFile(archivo(0), archivo(1))
    End Sub

End Class
