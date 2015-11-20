Imports Orbelink.DBHandler
Imports Orbelink.Entity.Publicaciones

Partial Public Class _Publicaciones_AtribTipoPublicacion
    Inherits PageBaseClass

    Const codigo_pantalla As String = "PU-09"
    Const level As Integer = 2

    'Pagina
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddScripts(level)
        securityHandler.VerifyPantalla(codigo_pantalla, level)
        If Not IsPostBack Then
            ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
            securityHandler.VerifyActions(btn_Salvar, btn_Modificar, btn_Eliminar)

            selectTipoPublicacion()
            If ddl_TipoPublicaciones.Items.Count > 0 Then
                selectMaestroAtributos_Para_Publicaciones()
                selectAtributos_TipoPublicacion(ddl_TipoPublicaciones.SelectedValue)
                loadMaestroAtributos_Para_Publicaciones(ddl_TipoPublicaciones.SelectedValue)
            End If
        End If

    End Sub

    Protected Sub ClearInfo(ByVal estado As Configuration.EstadoPantalla, ByVal clearInfo As Boolean)
        If clearInfo Then
            'tbx_NombrePublicacion.Text = ""
        End If

        Select Case estado
            Case Configuration.EstadoPantalla.NORMAL
                btn_Modificar.Visible = False
                btn_Salvar.Visible = True
                btn_Eliminar.Visible = False

            Case Configuration.EstadoPantalla.CONSULTAR
                btn_Modificar.Visible = True
                btn_Salvar.Visible = False
                btn_Eliminar.Visible = True

        End Select
    End Sub

    'Maestro_Atributos_Para_Publicaciones
    Protected Sub selectMaestroAtributos_Para_Publicaciones()
        Dim dataSet As Data.DataSet
        Dim Atributos_Para_Publicaciones As New Atributos_Para_Publicaciones

        Atributos_Para_Publicaciones.Fields.SelectAll()

        queryBuilder.From.Add(Atributos_Para_Publicaciones)
        queryBuilder.OrderBy.Add(Atributos_Para_Publicaciones.Nombre)
        dataSet = connection.executeSelect(queryBuilder.RelationalSelectQuery)
        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables.Count > 0 Then
                dg_MaestroAtributos_Para_Publicaciones.DataSource = dataSet
                dg_MaestroAtributos_Para_Publicaciones.DataBind()

                Dim results As ArrayList = ObjectBuilder.TransformDataTable(dataSet.Tables(0), Atributos_Para_Publicaciones)
                For counter As Integer = 0 To dataSet.Tables(0).Rows.Count - 1
                    Dim act_Atributos_Para_Publicaciones As Atributos_Para_Publicaciones = results(counter)
                    Dim chk_Seleccionado As CheckBox = dg_MaestroAtributos_Para_Publicaciones.Items(counter).FindControl("chk_Seleccionado")
                    Dim lbl_NombreCheck As Label = dg_MaestroAtributos_Para_Publicaciones.Items(counter).FindControl("lbl_NombreCheck")
                    Dim img_Info As UI.WebControls.Image = dg_MaestroAtributos_Para_Publicaciones.Items(counter).FindControl("img_Info")

                    lbl_NombreCheck.Text = act_Atributos_Para_Publicaciones.Nombre.Value
                    lbl_NombreCheck.Attributes.Add(act_Atributos_Para_Publicaciones.Id_atributo.Name, act_Atributos_Para_Publicaciones.Id_atributo.Value)
                    chk_Seleccionado.ToolTip = act_Atributos_Para_Publicaciones.Id_atributo.Value

                    If act_Atributos_Para_Publicaciones.Descripcion.IsValid Then
                        img_Info.AlternateText = act_Atributos_Para_Publicaciones.Descripcion.Value
                        img_Info.ToolTip = act_Atributos_Para_Publicaciones.Descripcion.Value
                        img_Info.Visible = True
                    Else
                        img_Info.Visible = False
                    End If
                Next
                dg_MaestroAtributos_Para_Publicaciones.Visible = True
            Else
                dg_MaestroAtributos_Para_Publicaciones.Visible = False
            End If
        End If
    End Sub

    Protected Sub loadMaestroAtributos_Para_Publicaciones(ByVal id_tipoPublicacion As Integer)
        Dim dataSet As Data.DataSet
        Dim Atributos_TipoPublicacion As New Atributos_TipoPublicacion

        If id_tipoPublicacion <> 0 Then
            Atributos_TipoPublicacion.Fields.SelectAll()
            Atributos_TipoPublicacion.id_tipoPublicacion.Where.EqualCondition(id_tipoPublicacion)
            queryBuilder.From.Add(Atributos_TipoPublicacion)
            dataSet = connection.executeSelect(queryBuilder.RelationalSelectQuery())

            If dataSet.Tables.Count > 0 Then
                If dataSet.Tables(0).Rows.Count > 0 Then
                    Dim resul_Atributos_TipoPublicacion As ArrayList = ObjectBuilder.TransformDataTable(dataSet.Tables(0), Atributos_TipoPublicacion)
                    For counterA As Integer = 0 To resul_Atributos_TipoPublicacion.Count - 1            'Recorre los Atributos_TipoPublicacion
                        Dim act_Atributos_TipoPublicacion As Atributos_TipoPublicacion = resul_Atributos_TipoPublicacion(counterA)
                        For counterB As Integer = 0 To dg_MaestroAtributos_Para_Publicaciones.Items.Count - 1                  'Recorre todos los Atributos_Para_Publicaciones
                            Dim lbl_NombreCheck As Label = dg_MaestroAtributos_Para_Publicaciones.Items(counterB).FindControl("lbl_NombreCheck")
                            Dim chk_Seleccionado As CheckBox = dg_MaestroAtributos_Para_Publicaciones.Items(counterB).FindControl("chk_Seleccionado")

                            If lbl_NombreCheck.Attributes("Id_Atributo") = act_Atributos_TipoPublicacion.id_atributo.Value Then
                                chk_Seleccionado.Checked = True
                                lbl_NombreCheck.Attributes.Add("EnDB", "Si")
                                Exit For
                            End If
                        Next
                    Next
                End If
            End If
        End If
    End Sub

    Protected Sub clearAtributos_Para_Publicaciones()
        For counter As Integer = 0 To dg_MaestroAtributos_Para_Publicaciones.Items.Count - 1                  'Recorre todos los Atributos_Para_Publicaciones
            Dim chk_Seleccionado As CheckBox = dg_MaestroAtributos_Para_Publicaciones.Items(counter).FindControl("chk_Seleccionado")
            Dim lbl_NombreCheck As Label = dg_MaestroAtributos_Para_Publicaciones.Items(counter).FindControl("lbl_NombreCheck")

            chk_Seleccionado.Checked = False
            lbl_NombreCheck.Attributes("EnDB") = "No"
        Next
    End Sub

    Protected Function checkearMaestroAtributos_Para_Publicaciones(ByVal id_tipoPublicacion As Integer, ByVal offsetOrden As Integer) As Boolean
        Dim Atributos_TipoPublicacion As New Atributos_TipoPublicacion

        For counter As Integer = 0 To dg_MaestroAtributos_Para_Publicaciones.Items.Count - 1      'Recorre Atributos_Para_Publicaciones
            Dim lbl_NombreCheck As Label = dg_MaestroAtributos_Para_Publicaciones.Items(counter).FindControl("lbl_NombreCheck")
            Dim chk_Seleccionado As CheckBox = dg_MaestroAtributos_Para_Publicaciones.Items(counter).FindControl("chk_Seleccionado")
            Dim Id_Atributo As Integer = lbl_NombreCheck.Attributes("Id_Atributo")

            If lbl_NombreCheck.Attributes("EnDB") = "Si" Then
                If Not chk_Seleccionado.Checked Then       'Borra
                    Atributos_TipoPublicacion.id_tipoPublicacion.Where.EqualCondition(id_tipoPublicacion)
                    Atributos_TipoPublicacion.id_atributo.Where.EqualCondition(Id_Atributo)
                    connection.executeDelete(queryBuilder.DeleteQuery(Atributos_TipoPublicacion))
                End If
            Else                                            'Inserta
                If chk_Seleccionado.Checked Then
                    offsetOrden += 1
                    Atributos_TipoPublicacion.id_tipoPublicacion.Value = id_tipoPublicacion
                    Atributos_TipoPublicacion.id_atributo.Value = Id_Atributo
                    Atributos_TipoPublicacion.Orden.Value = offsetOrden
                    Atributos_TipoPublicacion.Buscable.Value = 0
                    connection.executeInsert(queryBuilder.InsertQuery(Atributos_TipoPublicacion))
                End If
            End If
        Next

        Return True
    End Function

    'Atributos_TipoPublicacion
    Protected Sub selectAtributos_TipoPublicacion(ByVal id_tipoPublicacion As Integer)
        Dim dataSet As Data.DataSet
        Dim Atributos_TipoPublicacion As New Atributos_TipoPublicacion
        Dim Atributos_Para_Publicaciones As New Atributos_Para_Publicaciones

        If id_tipoPublicacion <> 0 Then
            Atributos_TipoPublicacion.Fields.SelectAll()
            Atributos_TipoPublicacion.id_tipoPublicacion.Where.EqualCondition(id_tipoPublicacion)
            Atributos_Para_Publicaciones.Fields.SelectAll()
            queryBuilder.Join.EqualCondition(Atributos_Para_Publicaciones.Id_atributo, Atributos_TipoPublicacion.id_atributo)

            queryBuilder.From.Add(Atributos_Para_Publicaciones)
            queryBuilder.From.Add(Atributos_TipoPublicacion)
            queryBuilder.OrderBy.Add(Atributos_TipoPublicacion.Orden)
            dataSet = connection.executeSelect(queryBuilder.RelationalSelectQuery())

            If dataSet.Tables.Count > 0 Then
                If dataSet.Tables(0).Rows.Count > 0 Then
                    dg_Atributos_Para_Publicaciones.DataSource = dataSet
                    dg_Atributos_Para_Publicaciones.DataBind()

                    Dim results As ArrayList = ObjectBuilder.TransformDataTable(dataSet.Tables(0), Atributos_Para_Publicaciones)
                    Dim resul_Atributos_TipoPublicacion As ArrayList = ObjectBuilder.TransformDataTable(dataSet.Tables(0), Atributos_TipoPublicacion)
                    For counter As Integer = 0 To dataSet.Tables(0).Rows.Count - 1      'Recorre todos
                        Dim act_Atributos_Para_Publicaciones As Atributos_Para_Publicaciones = results(counter)
                        Dim act_Atributos_TipoPublicacion As Atributos_TipoPublicacion = resul_Atributos_TipoPublicacion(counter)

                        Dim lbl_NombreCheck As Label = dg_Atributos_Para_Publicaciones.Items(counter).FindControl("lbl_NombreCheck")
                        Dim chk_Buscable As CheckBox = dg_Atributos_Para_Publicaciones.Items(counter).FindControl("chk_Buscable")
                        Dim btn_Subir As ImageButton = dg_Atributos_Para_Publicaciones.Items(counter).FindControl("btn_Subir")
                        Dim btn_Bajar As ImageButton = dg_Atributos_Para_Publicaciones.Items(counter).FindControl("btn_Bajar")

                        lbl_NombreCheck.Text = act_Atributos_Para_Publicaciones.Nombre.Value
                        lbl_NombreCheck.Attributes.Add(Atributos_TipoPublicacion.id_tipoPublicacion.Name, act_Atributos_TipoPublicacion.id_tipoPublicacion.Value)
                        lbl_NombreCheck.Attributes.Add(Atributos_TipoPublicacion.id_atributo.Name, act_Atributos_TipoPublicacion.id_atributo.Value)
                        lbl_NombreCheck.Attributes.Add(Atributos_TipoPublicacion.Orden.Name, act_Atributos_TipoPublicacion.Orden.Value)

                        If act_Atributos_TipoPublicacion.Buscable.Value = 1 Then
                            chk_Buscable.Checked = True
                        Else
                            chk_Buscable.Checked = False
                        End If
                        btn_Subir.Attributes.Add("indice", counter)
                        btn_Bajar.Attributes.Add("indice", counter)

                        dg_Atributos_Para_Publicaciones.Items(counter).Attributes.Add("onmouseover", "setRowFocus(this, " & counter & ", false)")
                        dg_Atributos_Para_Publicaciones.Items(counter).Attributes.Add("onmouseout", "unsetRowFocus(this, " & counter & ", false)")
                    Next
                    dg_Atributos_Para_Publicaciones.Visible = True
                Else
                    dg_Atributos_Para_Publicaciones.Visible = False
                End If
            End If
        End If
    End Sub

    Protected Sub updateAtributos_TipoPublicacion()
        Dim Atributos_TipoPublicacion As New Atributos_TipoPublicacion

        For counter As Integer = 0 To dg_Atributos_Para_Publicaciones.Items.Count - 1      'Recorre Atributos_Para_Publicaciones
            Dim lbl_NombreCheck As Label = dg_Atributos_Para_Publicaciones.Items(counter).FindControl("lbl_NombreCheck")
            Dim chk_Buscable As CheckBox = dg_Atributos_Para_Publicaciones.Items(counter).FindControl("chk_Buscable")

            Atributos_TipoPublicacion.Orden.Value = lbl_NombreCheck.Attributes(Atributos_TipoPublicacion.Orden.Name)
            Atributos_TipoPublicacion.id_tipoPublicacion.Where.EqualCondition(lbl_NombreCheck.Attributes(Atributos_TipoPublicacion.id_tipoPublicacion.Name))
            Atributos_TipoPublicacion.id_atributo.Where.EqualCondition(lbl_NombreCheck.Attributes(Atributos_TipoPublicacion.id_atributo.Name))
            If chk_Buscable.Checked Then
                Atributos_TipoPublicacion.Buscable.Value = 1
            Else
                Atributos_TipoPublicacion.Buscable.Value = 0
            End If

            connection.executeUpdate(queryBuilder.UpdateQuery(Atributos_TipoPublicacion))
        Next
    End Sub

    'TipoPublicacion
    Protected Sub selectTipoPublicacion()
        Dim dataset As Data.DataSet
        Dim tipoPublicacion As New TipoPublicacion

        tipoPublicacion.Fields.SelectAll()
        queryBuilder.OrderBy.Add(tipoPublicacion.Nombre)
        dataset = connection.executeSelect(queryBuilder.SelectQuery(tipoPublicacion))
        If dataset.Tables.Count > 0 Then
            If dataset.Tables(0).Rows.Count > 0 Then
                ddl_TipoPublicaciones.DataSource = dataset
                ddl_TipoPublicaciones.DataTextField = tipoPublicacion.Nombre.Name
                ddl_TipoPublicaciones.DataValueField = tipoPublicacion.Id_TipoPublicacion.Name
                ddl_TipoPublicaciones.DataBind()
            End If
        End If
    End Sub

    'Ordenes
    Protected Sub cambiarInfo(ByVal indiceA As Integer, ByVal indiceB As Integer)
        Dim temp As String
        Dim Atributos_TipoPublicacion As New Atributos_TipoPublicacion

        Dim lbl_NombreCheckA As Label = dg_Atributos_Para_Publicaciones.Items(indiceA).FindControl("lbl_NombreCheck")

        Dim lbl_NombreCheckB As Label = dg_Atributos_Para_Publicaciones.Items(indiceB).FindControl("lbl_NombreCheck")

        temp = lbl_NombreCheckA.Text
        lbl_NombreCheckA.Text = lbl_NombreCheckB.Text
        lbl_NombreCheckB.Text = temp

        temp = lbl_NombreCheckA.ToolTip
        lbl_NombreCheckA.ToolTip = lbl_NombreCheckB.ToolTip
        lbl_NombreCheckB.ToolTip = temp

        temp = lbl_NombreCheckA.Attributes(Atributos_TipoPublicacion.id_atributo.Name)
        lbl_NombreCheckA.Attributes(Atributos_TipoPublicacion.id_atributo.Name) = lbl_NombreCheckB.Attributes(Atributos_TipoPublicacion.id_atributo.Name)
        lbl_NombreCheckB.Attributes(Atributos_TipoPublicacion.id_atributo.Name) = temp
    End Sub

    'Eventos
    Protected Sub btn_Salvar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Salvar.Click
        If ddl_TipoPublicaciones.Items.Count > 0 Then
            updateAtributos_TipoPublicacion()
            ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
            selectAtributos_TipoPublicacion(ddl_TipoPublicaciones.SelectedValue)
        End If
        upd_Contenido.Update()
        upd_Busqueda.Update()
    End Sub

    Protected Sub btn_Cancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Cancelar.Click
        If ddl_TipoPublicaciones.Items.Count > 0 Then
            ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
            selectAtributos_TipoPublicacion(ddl_TipoPublicaciones.SelectedValue)
        End If
        upd_Contenido.Update()
    End Sub

    Protected Sub btn_Actualizar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Actualizar.Click
        If ddl_TipoPublicaciones.Items.Count > 0 Then
            Dim offsetOrden As Integer = 0
            If dg_Atributos_Para_Publicaciones.Items.Count > 0 Then
                Dim lbl_NombreCheck As Label = dg_Atributos_Para_Publicaciones.Items(dg_Atributos_Para_Publicaciones.Items.Count - 1).FindControl("lbl_NombreCheck")
                offsetOrden = lbl_NombreCheck.Attributes("Orden")
            End If
            checkearMaestroAtributos_Para_Publicaciones(ddl_TipoPublicaciones.SelectedValue, offsetOrden)
            loadMaestroAtributos_Para_Publicaciones(ddl_TipoPublicaciones.SelectedValue)
            selectAtributos_TipoPublicacion(ddl_TipoPublicaciones.SelectedValue)
        End If
        upd_Contenido.Update()
        upd_Busqueda.Update()
    End Sub

    Protected Sub ddl_TipoPublicaciones_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddl_TipoPublicaciones.SelectedIndexChanged
        clearAtributos_Para_Publicaciones()
        loadMaestroAtributos_Para_Publicaciones(ddl_TipoPublicaciones.SelectedValue)
        selectAtributos_TipoPublicacion(ddl_TipoPublicaciones.SelectedValue)
        upd_Contenido.Update()
        upd_Busqueda.Update()
    End Sub

    Protected Sub btn_Subir_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim btn_Subir As ImageButton = sender
        Dim indice As Integer = btn_Subir.Attributes("indice")

        If indice > 0 Then
            Dim otroIndice As Integer = indice - 1
            cambiarInfo(indice, otroIndice)
        End If
        upd_Contenido.Update()
    End Sub

    Protected Sub btn_Bajar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim btn_Bajar As ImageButton = sender
        Dim indice As Integer = btn_Bajar.Attributes("indice")

        If indice < dg_Atributos_Para_Publicaciones.Items.Count - 1 Then
            Dim otroIndice As Integer = indice + 1
            cambiarInfo(indice, otroIndice)
        End If
        upd_Contenido.Update()
    End Sub

    Protected Sub btn_AgregarTipoPublicacion_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_AgregarTipoPublicacion.Click
        Response.Redirect("TipoPublicacion.aspx" & Orbelink.Helpers.CommonTasks.RecreateQueryString(codigo_pantalla, True))
    End Sub

    Protected Sub btn_AgregarAtributo_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_AgregarAtributo.Click
        Response.Redirect("Atributos.aspx" & Orbelink.Helpers.CommonTasks.RecreateQueryString(codigo_pantalla, True))
    End Sub

End Class
