Imports Orbelink.DBHandler
Imports Orbelink.Entity.Entidades

Partial Public Class _Entidades_AtribTipoEntidad
    Inherits PageBaseClass

    Const codigo_pantalla As String = "CO-05"
    Const level As Integer = 2

    'Pagina

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddScripts(level)
        securityHandler.VerifyPantalla(codigo_pantalla, level)
        If Not IsPostBack Then
            ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
            securityHandler.VerifyActions(btn_Salvar, btn_Modificar, btn_Eliminar)

            selectTipoEntidad(securityHandler.ClearanceLevel)
            If ddl_TipoEntidades.Items.Count > 0 Then
                selectMaestroAtributos_Para_Entidades()
                selectAtributos_TipoEntidad(ddl_TipoEntidades.SelectedValue)
                loadMaestroAtributos_Para_Entidades(ddl_TipoEntidades.SelectedValue)
            End If
        End If

    End Sub

    Protected Sub ClearInfo(ByVal estado As Configuration.EstadoPantalla, ByVal clearInfo As Boolean)
        If clearInfo Then
            'tbx_NombreEntidad.Text = ""
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

    'Maestro_Atributos_Para_Entidades
    Protected Sub selectMaestroAtributos_Para_Entidades()
        Dim dataSet As Data.DataSet
        Dim Atributos_Para_Entidades As New Atributos_Para_Entidades

        Atributos_Para_Entidades.Fields.SelectAll()

        queryBuilder.From.Add(Atributos_Para_Entidades)
        queryBuilder.OrderBy.Add(Atributos_Para_Entidades.Nombre)
        dataSet = connection.executeSelect(queryBuilder.RelationalSelectQuery)
        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables.Count > 0 Then
                dg_MaestroAtributos_Para_Entidades.DataSource = dataSet
                dg_MaestroAtributos_Para_Entidades.DataBind()

                Dim results As ArrayList = ObjectBuilder.TransformDataTable(dataSet.Tables(0), Atributos_Para_Entidades)
                For counter As Integer = 0 To dataSet.Tables(0).Rows.Count - 1
                    Dim act_Atributos_Para_Entidades As Atributos_Para_Entidades = results(counter)
                    Dim chk_Seleccionado As CheckBox = dg_MaestroAtributos_Para_Entidades.Items(counter).FindControl("chk_Seleccionado")
                    Dim lbl_NombreCheck As Label = dg_MaestroAtributos_Para_Entidades.Items(counter).FindControl("lbl_NombreCheck")
                    Dim img_Info As UI.WebControls.Image = dg_MaestroAtributos_Para_Entidades.Items(counter).FindControl("img_Info")

                    lbl_NombreCheck.Text = act_Atributos_Para_Entidades.Nombre.Value
                    lbl_NombreCheck.Attributes.Add(act_Atributos_Para_Entidades.Id_atributo.Name, act_Atributos_Para_Entidades.Id_atributo.Value)
                    chk_Seleccionado.ToolTip = act_Atributos_Para_Entidades.Id_atributo.Value

                    If act_Atributos_Para_Entidades.Descripcion.IsValid Then
                        img_Info.AlternateText = act_Atributos_Para_Entidades.Descripcion.Value
                        img_Info.ToolTip = act_Atributos_Para_Entidades.Descripcion.Value
                        img_Info.Visible = True
                    Else
                        img_Info.Visible = False
                    End If
                Next
                dg_MaestroAtributos_Para_Entidades.Visible = True
            Else
                dg_MaestroAtributos_Para_Entidades.Visible = False
            End If
        End If
    End Sub

    Protected Sub loadMaestroAtributos_Para_Entidades(ByVal id_tipoEntidad As Integer)
        Dim dataSet As Data.DataSet
        Dim Atributos_TipoEntidad As New Atributos_TipoEntidad

        If id_tipoEntidad <> 0 Then
            Atributos_TipoEntidad.Fields.SelectAll()
            Atributos_TipoEntidad.id_tipoEntidad.Where.EqualCondition(id_tipoEntidad)
            queryBuilder.From.Add(Atributos_TipoEntidad)
            dataSet = connection.executeSelect(queryBuilder.RelationalSelectQuery())

            If dataSet.Tables.Count > 0 Then
                If dataSet.Tables(0).Rows.Count > 0 Then
                    Dim resul_Atributos_TipoEntidad As ArrayList = ObjectBuilder.TransformDataTable(dataSet.Tables(0), Atributos_TipoEntidad)
                    For counterA As Integer = 0 To resul_Atributos_TipoEntidad.Count - 1            'Recorre los Atributos_TipoEntidad
                        Dim act_Atributos_TipoEntidad As Atributos_TipoEntidad = resul_Atributos_TipoEntidad(counterA)
                        For counterB As Integer = 0 To dg_MaestroAtributos_Para_Entidades.Items.Count - 1                  'Recorre todos los Atributos_Para_Entidades
                            Dim lbl_NombreCheck As Label = dg_MaestroAtributos_Para_Entidades.Items(counterB).FindControl("lbl_NombreCheck")
                            Dim chk_Seleccionado As CheckBox = dg_MaestroAtributos_Para_Entidades.Items(counterB).FindControl("chk_Seleccionado")

                            If lbl_NombreCheck.Attributes("Id_Atributo") = act_Atributos_TipoEntidad.id_atributo.Value Then
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

    Protected Sub clearAtributos_Para_Entidades()
        For counter As Integer = 0 To dg_MaestroAtributos_Para_Entidades.Items.Count - 1                  'Recorre todos los Atributos_Para_Entidades
            Dim chk_Seleccionado As CheckBox = dg_MaestroAtributos_Para_Entidades.Items(counter).FindControl("chk_Seleccionado")
            Dim lbl_NombreCheck As Label = dg_MaestroAtributos_Para_Entidades.Items(counter).FindControl("lbl_NombreCheck")

            chk_Seleccionado.Checked = False
            lbl_NombreCheck.Attributes("EnDB") = "No"
        Next
    End Sub

    Protected Function checkearMaestroAtributos_Para_Entidades(ByVal id_tipoEntidad As Integer, ByVal offsetOrden As Integer) As Boolean
        Dim Atributos_TipoEntidad As New Atributos_TipoEntidad

        For counter As Integer = 0 To dg_MaestroAtributos_Para_Entidades.Items.Count - 1      'Recorre Atributos_Para_Entidades
            Dim lbl_NombreCheck As Label = dg_MaestroAtributos_Para_Entidades.Items(counter).FindControl("lbl_NombreCheck")
            Dim chk_Seleccionado As CheckBox = dg_MaestroAtributos_Para_Entidades.Items(counter).FindControl("chk_Seleccionado")
            Dim Id_Atributo As Integer = lbl_NombreCheck.Attributes("Id_Atributo")

            If lbl_NombreCheck.Attributes("EnDB") = "Si" Then
                If Not chk_Seleccionado.Checked Then       'Borra
                    Atributos_TipoEntidad.id_tipoEntidad.Where.EqualCondition(id_tipoEntidad)
                    Atributos_TipoEntidad.id_atributo.Where.EqualCondition(Id_Atributo)
                    connection.executeDelete(queryBuilder.DeleteQuery(Atributos_TipoEntidad))
                End If
            Else                                            'Inserta
                If chk_Seleccionado.Checked Then
                    offsetOrden += 1
                    Atributos_TipoEntidad.id_tipoEntidad.Value = id_tipoEntidad
                    Atributos_TipoEntidad.id_atributo.Value = Id_Atributo
                    Atributos_TipoEntidad.Orden.Value = offsetOrden
                    Atributos_TipoEntidad.Buscable.Value = 0
                    connection.executeInsert(queryBuilder.InsertQuery(Atributos_TipoEntidad))
                End If
            End If
        Next

        Return True
    End Function

    'Atributos_TipoEntidad
    Protected Sub selectAtributos_TipoEntidad(ByVal id_tipoEntidad As Integer)
        Dim dataSet As Data.DataSet
        Dim Atributos_TipoEntidad As New Atributos_TipoEntidad
        Dim Atributos_Para_Entidades As New Atributos_Para_Entidades

        If id_tipoEntidad <> 0 Then
            Atributos_TipoEntidad.Fields.SelectAll()
            Atributos_TipoEntidad.id_tipoEntidad.Where.EqualCondition(id_tipoEntidad)
            Atributos_Para_Entidades.Fields.SelectAll()
            queryBuilder.Join.EqualCondition(Atributos_Para_Entidades.Id_atributo, Atributos_TipoEntidad.id_atributo)

            queryBuilder.From.Add(Atributos_Para_Entidades)
            queryBuilder.From.Add(Atributos_TipoEntidad)
            queryBuilder.OrderBy.Add(Atributos_TipoEntidad.Orden)
            dataSet = connection.executeSelect(queryBuilder.RelationalSelectQuery())

            If dataSet.Tables.Count > 0 Then
                If dataSet.Tables(0).Rows.Count > 0 Then
                    dg_Atributos_Para_Entidades.DataSource = dataSet
                    dg_Atributos_Para_Entidades.DataBind()

                    Dim results As ArrayList = ObjectBuilder.TransformDataTable(dataSet.Tables(0), Atributos_Para_Entidades)
                    Dim resul_Atributos_TipoEntidad As ArrayList = ObjectBuilder.TransformDataTable(dataSet.Tables(0), Atributos_TipoEntidad)
                    For counter As Integer = 0 To dataSet.Tables(0).Rows.Count - 1      'Recorre todos
                        Dim act_Atributos_Para_Entidades As Atributos_Para_Entidades = results(counter)
                        Dim act_Atributos_TipoEntidad As Atributos_TipoEntidad = resul_Atributos_TipoEntidad(counter)

                        Dim lbl_NombreCheck As Label = dg_Atributos_Para_Entidades.Items(counter).FindControl("lbl_NombreCheck")
                        Dim chk_Buscable As CheckBox = dg_Atributos_Para_Entidades.Items(counter).FindControl("chk_Buscable")
                        Dim btn_Subir As ImageButton = dg_Atributos_Para_Entidades.Items(counter).FindControl("btn_Subir")
                        Dim btn_Bajar As ImageButton = dg_Atributos_Para_Entidades.Items(counter).FindControl("btn_Bajar")

                        lbl_NombreCheck.Text = act_Atributos_Para_Entidades.Nombre.Value
                        lbl_NombreCheck.Attributes.Add(Atributos_TipoEntidad.id_tipoEntidad.Name, act_Atributos_TipoEntidad.id_tipoEntidad.Value)
                        lbl_NombreCheck.Attributes.Add(Atributos_TipoEntidad.id_atributo.Name, act_Atributos_TipoEntidad.id_atributo.Value)
                        lbl_NombreCheck.Attributes.Add(Atributos_TipoEntidad.Orden.Name, act_Atributos_TipoEntidad.Orden.Value)

                        If act_Atributos_TipoEntidad.Buscable.Value = 1 Then
                            chk_Buscable.Checked = True
                        Else
                            chk_Buscable.Checked = False
                        End If
                        btn_Subir.Attributes.Add("indice", counter)
                        btn_Bajar.Attributes.Add("indice", counter)

                        dg_Atributos_Para_Entidades.Items(counter).Attributes.Add("onmouseover", "setRowFocus(this, " & counter & ", false)")
                        dg_Atributos_Para_Entidades.Items(counter).Attributes.Add("onmouseout", "unsetRowFocus(this, " & counter & ", false)")
                    Next
                    dg_Atributos_Para_Entidades.Visible = True
                Else
                    dg_Atributos_Para_Entidades.Visible = False
                End If
            End If
        End If
    End Sub

    Protected Sub updateAtributos_TipoEntidad()
        Dim Atributos_TipoEntidad As New Atributos_TipoEntidad

        For counter As Integer = 0 To dg_Atributos_Para_Entidades.Items.Count - 1      'Recorre Atributos_Para_Entidades
            Dim lbl_NombreCheck As Label = dg_Atributos_Para_Entidades.Items(counter).FindControl("lbl_NombreCheck")
            Dim chk_Buscable As CheckBox = dg_Atributos_Para_Entidades.Items(counter).FindControl("chk_Buscable")

            Atributos_TipoEntidad.Orden.Value = lbl_NombreCheck.Attributes(Atributos_TipoEntidad.Orden.Name)
            Atributos_TipoEntidad.id_tipoEntidad.Where.EqualCondition(lbl_NombreCheck.Attributes(Atributos_TipoEntidad.id_tipoEntidad.Name))
            Atributos_TipoEntidad.id_atributo.Where.EqualCondition(lbl_NombreCheck.Attributes(Atributos_TipoEntidad.id_atributo.Name))
            If chk_Buscable.Checked Then
                Atributos_TipoEntidad.Buscable.Value = 1
            Else
                Atributos_TipoEntidad.Buscable.Value = 0
            End If

            connection.executeUpdate(queryBuilder.UpdateQuery(Atributos_TipoEntidad))
        Next
    End Sub

    'TipoEntidad
    Protected Sub selectTipoEntidad(ByVal nivel As Integer)
        Dim dataset As Data.DataSet
        Dim tipoEntidad As New TipoEntidad

        tipoEntidad.Fields.SelectAll()
        tipoEntidad.Nivel.Where.LessThanCondition(nivel)
        queryBuilder.OrderBy.Add(tipoEntidad.Nombre)
        dataset = connection.executeSelect(queryBuilder.SelectQuery(tipoEntidad))
        If dataset.Tables.Count > 0 Then
            If dataset.Tables(0).Rows.Count > 0 Then
                ddl_TipoEntidades.DataSource = dataset
                ddl_TipoEntidades.DataTextField = tipoEntidad.Nombre.Name
                ddl_TipoEntidades.DataValueField = tipoEntidad.Id_TipoEntidad.Name
                ddl_TipoEntidades.DataBind()
            End If
        End If
    End Sub

    'Ordenes
    Protected Sub cambiarInfo(ByVal indiceA As Integer, ByVal indiceB As Integer)
        Dim temp As String
        Dim Atributos_TipoEntidad As New Atributos_TipoEntidad

        Dim lbl_NombreCheckA As Label = dg_Atributos_Para_Entidades.Items(indiceA).FindControl("lbl_NombreCheck")

        Dim lbl_NombreCheckB As Label = dg_Atributos_Para_Entidades.Items(indiceB).FindControl("lbl_NombreCheck")

        temp = lbl_NombreCheckA.Text
        lbl_NombreCheckA.Text = lbl_NombreCheckB.Text
        lbl_NombreCheckB.Text = temp

        temp = lbl_NombreCheckA.ToolTip
        lbl_NombreCheckA.ToolTip = lbl_NombreCheckB.ToolTip
        lbl_NombreCheckB.ToolTip = temp

        temp = lbl_NombreCheckA.Attributes(Atributos_TipoEntidad.id_atributo.Name)
        lbl_NombreCheckA.Attributes(Atributos_TipoEntidad.id_atributo.Name) = lbl_NombreCheckB.Attributes(Atributos_TipoEntidad.id_atributo.Name)
        lbl_NombreCheckB.Attributes(Atributos_TipoEntidad.id_atributo.Name) = temp
    End Sub

    'Eventos
    Protected Sub btn_Salvar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Salvar.Click
        If ddl_TipoEntidades.Items.Count > 0 Then
            updateAtributos_TipoEntidad()
            ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
            selectAtributos_TipoEntidad(ddl_TipoEntidades.SelectedValue)
        End If
        upd_Contenido.Update()
        upd_Busqueda.Update()
    End Sub

    Protected Sub btn_Cancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Cancelar.Click
        If ddl_TipoEntidades.Items.Count > 0 Then
            ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
            selectAtributos_TipoEntidad(ddl_TipoEntidades.SelectedValue)
        End If
        upd_Contenido.Update()
    End Sub

    Protected Sub btn_Actualizar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Actualizar.Click
        If ddl_TipoEntidades.Items.Count > 0 Then
            Dim offsetOrden As Integer = 0
            If dg_Atributos_Para_Entidades.Items.Count > 0 Then
                Dim lbl_NombreCheck As Label = dg_Atributos_Para_Entidades.Items(dg_Atributos_Para_Entidades.Items.Count - 1).FindControl("lbl_NombreCheck")
                offsetOrden = lbl_NombreCheck.Attributes("Orden")
            End If
            checkearMaestroAtributos_Para_Entidades(ddl_TipoEntidades.SelectedValue, offsetOrden)
            loadMaestroAtributos_Para_Entidades(ddl_TipoEntidades.SelectedValue)
            selectAtributos_TipoEntidad(ddl_TipoEntidades.SelectedValue)
        End If
        upd_Contenido.Update()
        upd_Busqueda.Update()
    End Sub

    Protected Sub ddl_TipoEntidades_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddl_TipoEntidades.SelectedIndexChanged
        clearAtributos_Para_Entidades()
        loadMaestroAtributos_Para_Entidades(ddl_TipoEntidades.SelectedValue)
        selectAtributos_TipoEntidad(ddl_TipoEntidades.SelectedValue)
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

        If indice < dg_Atributos_Para_Entidades.Items.Count - 1 Then
            Dim otroIndice As Integer = indice + 1
            cambiarInfo(indice, otroIndice)
        End If
        upd_Contenido.Update()
    End Sub

    Protected Sub btn_AgregarTipoEntidad_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_AgregarTipoEntidad.Click
        Response.Redirect("TipoEntidad.aspx" & Orbelink.Helpers.CommonTasks.RecreateQueryString(codigo_pantalla, True))
    End Sub

    Protected Sub btn_AgregarAtributo_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_AgregarAtributo.Click
        Response.Redirect("Atributos.aspx" & Orbelink.Helpers.CommonTasks.RecreateQueryString(codigo_pantalla, True))
    End Sub

End Class
