Imports Orbelink.DBHandler
Imports Orbelink.Entity.Archivos
Imports Orbelink.Entity.Entidades
Imports Orbelink.Control.Archivos
Imports Orbelink.Control.Comentarios
Imports Orbelink.Control.Entidades

Partial Class Controls_DetalleEntidad
    Inherits System.Web.UI.UserControl

    Dim queryBuilder As Orbelink.DBHandler.QueryBuilder
    Dim connection As Orbelink.DBHandler.SQLServer
    Dim securityHandler As Orbelink.Control.Security.SecurityHandler
    Dim codigo_pantalla As String
    Dim MyMaster As Orbelink.Orbecatalog6.MasterPageBaseClass
    Dim level As Integer

    Dim _tempEntidadActual As Integer
    Protected Property EntidadActual() As Integer
        Get
            If _tempEntidadActual <= 0 Then
                If ViewState("vs_EntidadActual") IsNot Nothing Then
                    _tempEntidadActual = ViewState("vs_EntidadActual")
                Else
                    ViewState.Add("vs_EntidadActual", 0)
                    _tempEntidadActual = 0
                End If
            End If
            Return _tempEntidadActual
        End Get
        Set(ByVal value As Integer)
            _tempEntidadActual = value
            ViewState("vs_EntidadActual") = _tempEntidadActual
        End Set
    End Property

    Public Sub SetVariables(ByVal theQueryBuilder As Orbelink.DBHandler.QueryBuilder, _
                           ByVal theConnection As Orbelink.DBHandler.SQLServer, _
                           ByVal theSecurityHandler As Orbelink.Control.Security.SecurityHandler, _
                           ByVal theCodigo_pantalla As String, _
                           ByVal theMaster As Orbelink.Orbecatalog6.MasterPageBaseClass, _
                           ByVal theLevel As Integer)
        queryBuilder = theQueryBuilder
        connection = theConnection
        securityHandler = theSecurityHandler
        codigo_pantalla = theCodigo_pantalla
        MyMaster = theMaster
        level = theLevel
    End Sub

    Public Function LoadEntidad(ByVal id_entidad As Integer) As Boolean
        If asignarInformacion(id_entidad) Then
            selectComentarios(id_entidad)
            cargarArchivos(id_entidad)
            Return True
        Else
            Return False
        End If
    End Function

    Protected Function asignarInformacion(ByVal id_entidad As Integer) As Boolean
        Dim dataSet As New Data.DataSet
        Dim entidad As New Entidad
        Dim ubicacion As New Ubicacion
        Dim tipo As New TipoEntidad

        ubicacion.Fields.SelectAll()
        entidad.Fields.SelectAll()
        tipo.Fields.SelectAll()

        entidad.Id_entidad.Where.EqualCondition(id_entidad)

        queryBuilder.Join.EqualCondition(entidad.Id_Ubicacion, ubicacion.Id_ubicacion)
        queryBuilder.Join.EqualCondition(entidad.Id_tipoEntidad, tipo.Id_TipoEntidad)
        queryBuilder.From.Add(entidad)
        queryBuilder.From.Add(ubicacion)
        queryBuilder.From.Add(tipo)
        Dim consulta As String = queryBuilder.RelationalSelectQuery
        dataSet = connection.executeSelect(consulta)
        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables(0).Rows.Count > 0 Then
                ObjectBuilder.CreateObject(dataSet.Tables(0), 0, entidad)
                ObjectBuilder.CreateObject(dataSet.Tables(0), 0, ubicacion)
                ObjectBuilder.CreateObject(dataSet.Tables(0), 0, tipo)

                Me.lbl_nombre.Text = entidad.NombreDisplay.Value
                Me.lbl_telefono.Text = entidad.Telefono.Value
                Me.lbl_mail.Text = entidad.Email.Value
                Me.lbl_Celular.Text = entidad.Celular.Value
                Me.lbl_ubicacion.Text = ubicacion.Nombre.Value
                Me.lbl_tipo.Text = tipo.Nombre.Value
                cargarAtributosExtra(entidad.Id_entidad.Value, entidad.Id_tipoEntidad.Value)
                Return True
            End If
        End If
        Return False
    End Function

    'Archivos
    Protected Sub cargarArchivos(ByVal id_entidad As Integer)
        Dim archivos As New Archivo
        Dim Archivo_Entidad As New Archivo_Entidad
        Dim dataSet As New Data.DataSet

        archivos.Fields.SelectAll()
        Archivo_Entidad.Principal.ToSelect = True
        queryBuilder.Join.EqualCondition(archivos.Id_Archivo, Archivo_Entidad.id_Archivo)
        Archivo_Entidad.id_Entidad.Where.EqualCondition(id_entidad)
        queryBuilder.From.Add(archivos)
        queryBuilder.From.Add(Archivo_Entidad)
        queryBuilder.Top = 3
        queryBuilder.Orderby.Add(Archivo_Entidad.Principal, False)

        Dim consulta As String = queryBuilder.RelationalSelectQuery
        dataSet = connection.executeSelect(consulta)

        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables(0).Rows.Count > 0 Then
                Dim result_archivos As ArrayList = ObjectBuilder.TransformDataTable(dataSet.Tables(0), archivos)
                Dim result_arch_entidad As ArrayList = ObjectBuilder.TransformDataTable(dataSet.Tables(0), Archivo_Entidad)
                dl_imagenes.DataSource = dataSet
                dl_imagenes.DataBind()
                For counter As Integer = 0 To dataSet.Tables(0).Rows.Count - 1
                    Dim act_archivo As Archivo = result_archivos(counter)
                    Dim act_arch_entidad As Archivo_Entidad = result_arch_entidad(counter)
                    Dim imagen As Image = dl_imagenes.Items(counter).FindControl("img_archivo")
                    ArchivoHandler.GetArchivoImage(imagen, act_archivo.FileName.Value, act_archivo.Extension.Value, level, True)

                    Dim nombre As Label = dl_imagenes.Items(counter).FindControl("lbl_imgNombre")
                    Dim peso As Label = dl_imagenes.Items(counter).FindControl("lbl_imgTamano")
                    Dim fecha As Label = dl_imagenes.Items(counter).FindControl("lbl_imgFecha")
                    Dim nombre2 As String = act_archivo.Nombre.Value
                    If act_archivo.Nombre.Value.Length > 12 Then
                        nombre2 = nombre2.Substring(0, 12)
                    End If
                    nombre.Text = nombre2
                    peso.Text = act_archivo.Size.Value
                    fecha.Text = act_archivo.Fecha.ValueLocalized

                    If act_arch_entidad.Principal.Value > 0 Then
                        Dim imagenP As New Image
                        ArchivoHandler.GetArchivoImage(imagenP, act_archivo.FileName.Value, act_archivo.Extension.Value, level, True)
                        img_principal.ImageUrl = imagenP.ImageUrl
                    End If
                Next
            Else
                Me.archivos.Visible = False
            End If
        Else
            Me.archivos.Visible = False
        End If
    End Sub

    'Atributos
    Protected Sub cargarAtributosExtra(ByVal id_entidad As Integer, ByVal id_tipoEntidad As Integer)
        Dim atributos_Entidad As New Atributos_Entidad
        Dim atributos As New Atributos_Para_Entidades
        Dim dataSet As New Data.DataSet
        atributos.Nombre.ToSelect = True
        atributos_Entidad.Valor.ToSelect = True
        queryBuilder.Join.EqualCondition(atributos.Id_atributo, atributos_Entidad.Id_atributo)
        atributos_Entidad.Id_entidad.Where.EqualCondition(id_entidad)

        queryBuilder.From.Add(atributos_Entidad)
        queryBuilder.From.Add(atributos)
        Dim consulta As String = queryBuilder.RelationalSelectQuery
        dataSet = connection.executeSelect(consulta)

        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables(0).Rows.Count > 0 Then
                DL_atributos.DataSource = dataSet
                DL_atributos.DataBind()

                Dim resul_atributosEntidad As ArrayList = TransformDataTable(dataSet.Tables(0), atributos_Entidad)
                Dim resul_atributos As ArrayList = TransformDataTable(dataSet.Tables(0), atributos)

                For counter As Integer = 0 To dataSet.Tables(0).Rows.Count - 1
                    Dim act_atributoEntidad As Atributos_Entidad = resul_atributosEntidad(counter)
                    Dim act_atributos As Atributos_Para_Entidades = resul_atributos(counter)

                    Dim nombre As Label = DL_atributos.Items(counter).FindControl("lbl_nombreAtributo")
                    nombre.Text = act_atributos.Nombre.Value & ":"
                    Dim valor As Label = DL_atributos.Items(counter).FindControl("lbl_valorAtributo")
                    valor.Text = act_atributoEntidad.Valor.Value
                Next
            Else
                Me.lbl_atributosExtra.Visible = True
            End If
        End If
    End Sub

    'comentarios
    Sub selectComentarios(ByVal id_entidad As Integer)
        Dim comentHandler As New ComentarioHandler(connection.connectionString)
        Dim entiCommentHandler As New ControladoraComentario_Entidad(connection.connectionString)
        comentHandler.selectComentarios_ForNotas(entiCommentHandler, id_entidad, rep_Notas, AddressOf tbx_Nota_TextChanged)
    End Sub

    Protected Sub tbx_Nota_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim theTextBox As TextBox = sender
        Dim comentHandler As New ComentarioHandler(connection.connectionString)
        Dim entiCommentHandler As New ControladoraComentario_Entidad(connection.connectionString)

        Dim comentarioInsertado As Integer = comentHandler.CrearComentario(entiCommentHandler, securityHandler.Usuario, theTextBox.Text, theTextBox.Text, 0, EntidadActual)
        If comentarioInsertado <= 0 Then
            MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Salvar, Resources.Comentarios_Resources.Comentario, True)
        End If
    End Sub
End Class
