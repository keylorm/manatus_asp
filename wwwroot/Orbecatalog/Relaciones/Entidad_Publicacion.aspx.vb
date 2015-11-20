Imports Orbelink.DBHandler
Imports Orbelink.Entity.Entidades
Imports Orbelink.Entity.Publicaciones

Partial Class Orbecatalog_Relaciones_Entidad_Publicacion
    Inherits PageBaseClass

    Const codigo_pantalla As String = "Re-04"
    Const level As Integer = 2

    Dim agregarEntidad As Boolean = True

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Request.Params("id_Entidad") Is Nothing Then
            agregarEntidad = False
        End If

        If Not IsPostBack Then
            securityHandler.VerifyPantalla(codigo_pantalla, level)
            If Not Request.Params("id_Entidad") Is Nothing Then
                cargar_AgregarRelacionPublicacion(nombreEntidad(Request.Params("id_Entidad")))
            Else
                If Not Request.Params("id_Publicacion") Is Nothing Then
                    cargar_AgregarRelacionEntidad(nombrePublicacion(Request.Params("id_Publicacion")))
                End If
            End If
            cargarComboTipoRelacion()
        End If
    End Sub

    Protected Sub cargar_AgregarRelacionEntidad(ByVal nombre As String)
        lbl_titulo.Text = "Entidad para esta Publicacion"
        lbl_nombreRelacionado.Text = nombre
        btn_Agregar.ToolTip = "Ingresar nueva Entidad"
        cargarComboEntidad()
        cargarEntidadRelacionadas()
    End Sub

    Protected Sub cargar_AgregarRelacionPublicacion(ByVal nombre As String)
        lbl_titulo.Text = "Publicacion para esta Entidad"
        lbl_nombreRelacionado.Text = nombre
        btn_Agregar.ToolTip = "Ingresar nueva Publicacion"
        cargarComboPublicacion()
        cargarPublicacionRelacionados()
    End Sub

    Protected Sub cargarComboEntidad()
        Dim dataSet As New Data.DataSet
        Dim Entidad As New Entidad
        Entidad.NombreDisplay.ToSelect = True
        Entidad.Apellido.ToSelect = True
        Entidad.Id_entidad.ToSelect = True
        Dim consulta As String = queryBuilder.SelectQuery(Entidad)
        dataSet = connection.executeSelect(consulta)

        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables(0).Rows.Count > 0 Then
                ddl_Relacionados.DataSource = dataSet
                ddl_Relacionados.DataTextField = Entidad.NombreDisplay.Name
                ddl_Relacionados.DataValueField = Entidad.Id_entidad.Name
                ddl_Relacionados.DataBind()
            End If
        End If
    End Sub


    Protected Sub cargarComboPublicacion()
        Dim dataSet As New Data.DataSet
        Dim Publicacion As New Publicacion
        Publicacion.Titulo.ToSelect = True
        Publicacion.Id_Publicacion.ToSelect = True
        Dim consulta As String = queryBuilder.SelectQuery(Publicacion)
        dataSet = connection.executeSelect(consulta)

        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables(0).Rows.Count > 0 Then
                ddl_Relacionados.DataSource = dataSet
                ddl_Relacionados.DataTextField = Publicacion.Titulo.Name
                ddl_Relacionados.DataValueField = Publicacion.Id_Publicacion.Name
                ddl_Relacionados.DataBind()
            End If
        End If
    End Sub

    Protected Sub cargarComboTipoRelacion()
        Dim dataSet As New Data.DataSet
        Dim tipoRelaciones As New TipoRelacion_Entidad_Publicacion
        tipoRelaciones.Nombre_Publicacion_Entidad.ToSelect = True
        tipoRelaciones.Nombre_Entidad_Publicacion.ToSelect = True
        tipoRelaciones.Id_TipoRelacion.ToSelect = True
        Dim consulta As String = queryBuilder.SelectQuery(tipoRelaciones)
        dataSet = connection.executeSelect(consulta)

        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables(0).Rows.Count > 0 Then
                ddl_TipoRelacion.DataSource = dataSet
                If agregarEntidad Then
                    ddl_TipoRelacion.DataTextField = tipoRelaciones.Nombre_Publicacion_Entidad.Name
                Else
                    ddl_TipoRelacion.DataTextField = tipoRelaciones.Nombre_Entidad_Publicacion.Name
                End If
                ddl_TipoRelacion.DataValueField = tipoRelaciones.Id_TipoRelacion.Name
                ddl_TipoRelacion.DataBind()
            End If
        End If
    End Sub

    Protected Sub cargarEntidadRelacionadas()
        Dim Entidad_Publicacion As New Relacion_Entidad_Publicacion
        Dim tipoRelacion As New TipoRelacion_Entidad_Publicacion
        Dim Entidad As New Entidad
        Dim dataSet As New Data.DataSet

        Entidad.NombreDisplay.ToSelect = True
        tipoRelacion.Nombre_Publicacion_Entidad.ToSelect = True
        Entidad.Id_entidad.ToSelect = True
        tipoRelacion.Id_TipoRelacion.ToSelect = True

        queryBuilder.Join.EqualCondition(Entidad_Publicacion.id_entidad, Entidad.Id_entidad)
        queryBuilder.Join.EqualCondition(Entidad_Publicacion.id_TipoRelacion, tipoRelacion.Id_TipoRelacion)
        Entidad_Publicacion.id_Publicacion.Where.EqualCondition(Request.Params("id_Publicacion"))

        queryBuilder.From.Add(Entidad)
        queryBuilder.From.Add(tipoRelacion)
        queryBuilder.From.Add(Entidad_Publicacion)

        Dim consulta As String = queryBuilder.RelationalSelectQuery()
        dataSet = connection.executeSelect(consulta)
        lst_Entidad_Publicacion.Items.Clear()
        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables(0).Rows.Count > 0 Then
                Dim resul_Entidad As ArrayList = ObjectBuilder.TransformDataTable(dataSet.Tables(0), Entidad)
                Dim resul_tipo As ArrayList = ObjectBuilder.TransformDataTable(dataSet.Tables(0), tipoRelacion)

                For counter As Integer = 0 To dataSet.Tables(0).Rows.Count - 1
                    Dim act_Entidad As Entidad = resul_Entidad(counter)
                    Dim act_tipo As TipoRelacion_Entidad_Publicacion = resul_tipo(counter)

                    Me.lst_Entidad_Publicacion.Items.Add(act_tipo.Nombre_Publicacion_Entidad.Value & " " & act_Entidad.NombreDisplay.Value)
                    Me.lst_Entidad_Publicacion.Items(counter).Value = act_Entidad.Id_entidad.Value & "-" & act_tipo.Id_TipoRelacion.Value
                Next
            End If
        End If
    End Sub

    Protected Sub cargarPublicacionRelacionados()
        Dim Publicacion_Entidad As New Relacion_Publicacion_Entidad
        Dim tipoRelacion As New TipoRelacion_Entidad_Publicacion
        Dim Publicacion As New Publicacion
        Dim dataSet As New Data.DataSet

        Publicacion.Titulo.ToSelect = True
        tipoRelacion.Nombre_Entidad_Publicacion.ToSelect = True
        Publicacion.Id_Publicacion.ToSelect = True
        tipoRelacion.Id_TipoRelacion.ToSelect = True

        queryBuilder.Join.EqualCondition(Publicacion_Entidad.id_Publicacion, Publicacion.Id_Publicacion)
        queryBuilder.Join.EqualCondition(Publicacion_Entidad.id_TipoRelacion, tipoRelacion.Id_TipoRelacion)
        Publicacion_Entidad.id_entidad.Where.EqualCondition(Request.Params("id_Entidad"))

        queryBuilder.From.Add(Publicacion)
        queryBuilder.From.Add(tipoRelacion)
        queryBuilder.From.Add(Publicacion_Entidad)

        Dim consulta As String = queryBuilder.RelationalSelectQuery()
        dataSet = connection.executeSelect(consulta)
        lst_Entidad_Publicacion.Items.Clear()
        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables(0).Rows.Count > 0 Then
                Dim resul_Publicacion As ArrayList = ObjectBuilder.TransformDataTable(dataSet.Tables(0), Publicacion)
                Dim resul_tipo As ArrayList = ObjectBuilder.TransformDataTable(dataSet.Tables(0), tipoRelacion)

                For counter As Integer = 0 To dataSet.Tables(0).Rows.Count - 1
                    Dim act_Publicacion As Publicacion = resul_Publicacion(counter)
                    Dim act_tipo As TipoRelacion_Entidad_Publicacion = resul_tipo(counter)

                    Me.lst_Entidad_Publicacion.Items.Add(act_tipo.Nombre_Entidad_Publicacion.Value & " " & act_Publicacion.Titulo.Value)
                    Me.lst_Entidad_Publicacion.Items(counter).Value = act_Publicacion.Id_Publicacion.Value & "-" & act_tipo.Id_TipoRelacion.Value
                Next
            End If
        End If
    End Sub


    Protected Sub btn_Agregar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Agregar.Click

        Try
            Dim relacion As New Relacion_Entidad_Publicacion
            If agregarEntidad Then
                relacion.id_entidad.Value = ddl_Relacionados.SelectedValue
                relacion.id_Publicacion.Value = Request.Params("id_Publicacion")
            Else
                relacion.id_Publicacion.Value = ddl_Relacionados.SelectedValue
                relacion.id_entidad.Value = Request.Params("id_Entidad")
            End If

            relacion.id_TipoRelacion.Value = ddl_TipoRelacion.SelectedValue
            Dim consulta As String = queryBuilder.InsertQuery(relacion)
            connection.executeInsert(consulta)
        Catch ex As Exception
            MyMaster.MostrarMensaje("Error al agregar", True)
        End Try
        actualizarPanel()

    End Sub

    Protected Sub btn_Eliminar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Eliminar.Click

        If lst_Entidad_Publicacion.SelectedValue.Length > 0 Then
            Dim corte As Integer = lst_Entidad_Publicacion.SelectedValue.IndexOf("-")
            Dim id_Publicacion As Integer = Request.Params("id_Publicacion")
            Dim id_Entidad As Integer

            If agregarEntidad Then
                id_Publicacion = Request.Params("id_Publicacion")
                id_Entidad = lst_Entidad_Publicacion.SelectedValue.Substring(0, corte)
            Else
                id_Entidad = Request.Params("id_Entidad")
                id_Publicacion = lst_Entidad_Publicacion.SelectedValue.Substring(0, corte)
            End If
            Dim id_TipoRelacion As Integer = lst_Entidad_Publicacion.SelectedValue.Substring(corte + 1, lst_Entidad_Publicacion.SelectedValue.Length - corte - 1)


            If deletePublicacion_Entidad(id_Publicacion, id_Entidad, id_TipoRelacion) Then
                'Mensaje de exito
            Else
                'Mensaje de fracado
            End If
        Else
        End If
        actualizarPanel()
    End Sub

    Protected Function deletePublicacion_Entidad(ByVal id_Publicacion As Integer, ByVal id_Entidad As Integer, ByVal id_relacion As Integer) As Boolean
        Dim exitoso As Boolean = False
        Try
            Dim rel_Publicacion_Entidad As New Relacion_Entidad_Publicacion
            rel_Publicacion_Entidad.id_entidad.Where.EqualCondition(id_Entidad)
            rel_Publicacion_Entidad.id_Publicacion.Where.EqualCondition(id_Publicacion)
            rel_Publicacion_Entidad.id_TipoRelacion.Where.EqualCondition(id_relacion)

            Dim consulta As String = queryBuilder.DeleteQuery(rel_Publicacion_Entidad)
            exitoso = connection.executeDelete(consulta)
        Catch ex As Exception
            exitoso = False
        End Try
        actualizarPanel()
    End Function

    Protected Sub actualizarPanel()
        If agregarEntidad Then
            cargarEntidadRelacionadas()
        Else
            cargarPublicacionRelacionados()
        End If
    End Sub

    Protected Function nombrePublicacion(ByVal id_publicacion As Integer) As String
        Dim publicacion As New Publicacion
        Dim dataSet As New Data.DataSet
        publicacion.Titulo.ToSelect = True
        publicacion.Id_Publicacion.Where.EqualCondition(id_publicacion)
        Dim consulta As String = queryBuilder.SelectQuery(publicacion)
        dataSet = connection.executeSelect(consulta)
        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables(0).Rows.Count > 0 Then
                ObjectBuilder.CreateObject(dataSet.Tables(0), 0, publicacion)
                Return publicacion.Titulo.Value
            End If
        End If
        Return ""
    End Function

    Protected Function nombreEntidad(ByVal id_entidad As Integer) As String
        Dim entidad As New Entidad
        Dim dataSet As New Data.DataSet
        entidad.NombreDisplay.ToSelect = True
        entidad.Id_entidad.Where.EqualCondition(id_entidad)
        Dim consulta As String = queryBuilder.SelectQuery(entidad)
        dataSet = connection.executeSelect(consulta)
        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables(0).Rows.Count > 0 Then
                ObjectBuilder.CreateObject(dataSet.Tables(0), 0, entidad)
                Return entidad.NombreDisplay.Value
            End If
        End If
        Return ""
    End Function

End Class
