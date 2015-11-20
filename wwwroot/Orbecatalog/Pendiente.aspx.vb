Imports Orbelink.DBHandler
Imports Orbelink.Entity.Entidades
Imports Orbelink.Entity.Orbecatalog
Imports Orbelink.DateAndTime

Partial Class _Pendiente
    Inherits PageBaseClass

    Const codigo_pantalla As String = "OR-04"
    Const level As Integer = 1

    'Pagina
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddScripts(level)
        securityHandler.VerifyPantalla(codigo_pantalla, level)

        If Not IsPostBack Then
            dg_Pendientes.Attributes.Add("SelectedIndex", -1)
            dg_Pendientes.Attributes.Add("SelectedPage", -1)
            dg_Pendientes.Attributes.Add("SelectedValue", -1)
            ClearInfo(Configuration.EstadoPantalla.NORMAL, False)
            securityHandler.VerifyActions(btn_Salvar, btn_Modificar, btn_Eliminar)

            selectEntidades(securityHandler.Entidad)
            If Not Request.QueryString("id_Pendiente") Is Nothing Then
                ClearInfo(Configuration.EstadoPantalla.CONSULTAR, False)
                dg_Pendientes.Attributes("SelectedValue") = Request.QueryString("id_Pendiente")
                loadPendiente(Request.QueryString("id_Pendiente"))
                selectPendientes(True)
            Else
                selectPendientes(securityHandler.Entidad)
            End If

        End If
    End Sub

    Protected Sub ClearInfo(ByVal estado As Configuration.EstadoPantalla, ByVal clearInfo As Boolean)
        If clearInfo Then
            tbx_NombrePendiente.Text = ""
            tbx_NombrePendiente.ToolTip = ""
            tbx_Descripcion.Text = ""

            tbx_FechaProgramado.Enabled = True
            tbx_FechaProgramado.SelectedDate = DateHandler.ToLocalizedDateFromUtc(Date.UtcNow).Date
            Me.ClearDataGrid(dg_Pendientes, clearInfo)
        End If
        Select Case estado
            Case Configuration.EstadoPantalla.NORMAL
                btn_Modificar.Visible = False
                btn_Salvar.Visible = True
                btn_Eliminar.Visible = False

                lbl_Inicio.Text = Date.UtcNow
            Case Configuration.EstadoPantalla.CONSULTAR
                btn_Modificar.Visible = True
                btn_Salvar.Visible = False
                btn_Eliminar.Visible = True
        End Select
    End Sub

    'Pendientes
    Protected Sub selectPendientes(ByVal id_Publicador As Integer, Optional ByVal fromValue As Boolean = False)
        Dim dataset As Data.DataSet
        Dim entidad As New Entidad
        Dim Pendiente As New Pendiente

        Pendiente.Id_Pendiente.ToSelect = True
        Pendiente.Nombre.ToSelect = True
        Pendiente.Prioridad.ToSelect = True
        Pendiente.Terminado.ToSelect = True
        entidad.NombreDisplay.ToSelect = True

        If id_Publicador > 0 Then
            Pendiente.Id_Publicador.Where.EqualCondition(id_Publicador)
        End If

        queryBuilder.Join.EqualCondition(entidad.Id_entidad, Pendiente.Id_Responsable)

        'Busca si hay filtro
        If ddl_Filtro_Estados.SelectedIndex < ddl_Filtro_Estados.Items.Count - 1 Then
            Pendiente.Prioridad.Where.EqualCondition(ddl_Filtro_Estados.SelectedIndex)
        End If

        queryBuilder.From.Add(entidad)
        queryBuilder.From.Add(Pendiente)
        queryBuilder.Orderby.Add(Pendiente.Terminado)
        queryBuilder.Orderby.Add(Pendiente.Prioridad, False)
        dataset = connection.executeSelect(queryBuilder.RelationalSelectQuery)

        If dataset.Tables.Count > 0 Then
            If dataset.Tables(0).Rows.Count > 0 Then
                dg_Pendientes.DataSource = dataset
                dg_Pendientes.DataKeyField = Pendiente.Id_Pendiente.Name
                If fromValue Then
                    SelectFromValue(dg_Pendientes, dataset.Tables(0), Pendiente.Id_Pendiente.Name, dg_Pendientes.Attributes("SelectedValue"))
                Else
                    dg_Pendientes.DataBind()
                End If

                'Llena el grid
                Dim offset As Integer = dg_Pendientes.CurrentPageIndex * dg_Pendientes.PageSize
                Dim result_Entidades As ArrayList = ObjectBuilder.TransformDataTable(dataset.Tables(0), entidad)
                Dim result_Pendientes As ArrayList = ObjectBuilder.TransformDataTable(dataset.Tables(0), Pendiente)
                Dim counter As Integer
                For counter = 0 To dg_Pendientes.Items.Count - 1
                    Dim act_Entidad As Entidad = result_Entidades(offset + counter)
                    Dim act_Pendientes As Pendiente = result_Pendientes(offset + counter)

                    Dim lbl_Nombre As Label = dg_Pendientes.Items(counter).FindControl("lbl_Nombre")
                    Dim lbl_Estado As Label = dg_Pendientes.Items(counter).FindControl("lbl_Estado")
                    Dim lbl_Entidad As Label = dg_Pendientes.Items(counter).FindControl("lbl_Entidad")
                    Dim img_Estado As Image = dg_Pendientes.Items(counter).FindControl("img_Estado")

                    lbl_Nombre.Text = act_Pendientes.Nombre.Value
                    lbl_Estado.Text = ddl_Prioridad.Items(act_Pendientes.Prioridad.Value).Text
                    lbl_Entidad.Text = act_Entidad.NombreDisplay.Value

                    If act_Pendientes.Terminado.Value = 1 Then
                        img_Estado.ImageUrl = "images/icons/check.gif"
                        img_Estado.Visible = True
                    Else
                        img_Estado.Visible = False
                    End If

                    'Javascript
                    dg_Pendientes.Items(counter).Attributes.Add("onmouseover", "setRowFocus(this, " & counter & ", false)")
                    dg_Pendientes.Items(counter).Attributes.Add("onmouseout", "unsetRowFocus(this, " & counter & ", false)")
                Next
                lbl_ResultadoElementos.Text = contadorElementos(offset, dg_Pendientes.PageSize, dataset.Tables(0).Rows.Count)
                dg_Pendientes.Visible = True
            Else
                lbl_ResultadoElementos.Text = contadorElementos(0, 0, 0)
                dg_Pendientes.Visible = False
            End If
        End If
    End Sub

    Protected Function insertPendiente() As Boolean
        Dim Pendiente As New Pendiente
        Try
            Pendiente.Nombre.Value = tbx_NombrePendiente.Text
            Pendiente.FechaInicio.ValueUtc = Date.UtcNow
            Pendiente.Descripcion.Value = tbx_Descripcion.Text
            Pendiente.Prioridad.Value = ddl_Prioridad.SelectedValue
            Pendiente.FechaProgramado.ValueLocalized = tbx_FechaProgramado.SelectedDate
            Pendiente.Id_Publicador.Value = ddl_Publicador.SelectedValue
            Pendiente.Id_Responsable.Value = ddl_Responsable.SelectedValue
            Pendiente.Terminado.Value = 0
            connection.executeInsert(queryBuilder.InsertQuery(Pendiente))
            Return True
        Catch exc As Exception
            MyMaster.MostrarMensaje(exc.Message, True)

            Return False
        End Try
    End Function

    Protected Sub loadPendiente(ByVal Id_Pendiente As Integer)
        Dim dataSet As Data.DataSet
        Dim Pendiente As New Pendiente

        Pendiente.Fields.SelectAll()
        Pendiente.Id_Pendiente.Where.EqualCondition(Id_Pendiente)
        queryBuilder.From.Add(Pendiente)
        dataSet = connection.executeSelect(queryBuilder.RelationalSelectQuery)

        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables(0).Rows.Count > 0 Then
                ObjectBuilder.CreateObject(dataSet.Tables(0), 0, Pendiente)

                tbx_NombrePendiente.Text = Pendiente.Nombre.Value
                tbx_NombrePendiente.ToolTip = Pendiente.Id_Pendiente.Value

                ddl_Publicador.SelectedValue = Pendiente.Id_Publicador.Value
                ddl_Responsable.SelectedValue = Pendiente.Id_Responsable.Value

                ddl_Prioridad.SelectedValue = Pendiente.Prioridad.Value
                tbx_Descripcion.Text = Pendiente.Descripcion.Value
                lbl_Inicio.Text = Pendiente.FechaInicio.ValueLocalized
                tbx_FechaProgramado.SelectedDate = Pendiente.FechaProgramado.ValueLocalized

                If Pendiente.Terminado.Value = "1" Then
                    tbx_FechaProgramado.Enabled = False
                    'tbx_FechaTerminado.Enabled = False
                    'tbx_FechaTerminado.SelectedDate = Pendiente.FechaTerminado.Value
                Else
                    tbx_FechaProgramado.Enabled = True
                    'tbx_FechaTerminado.Enabled = True
                    'tbx_FechaTerminado.SelectedDate = Nothing
                End If
            End If
        End If
    End Sub

    Protected Function updatePendiente(ByVal Id_Pendiente As Integer) As Integer
        Dim Pendiente As New Pendiente
        Try
            Pendiente.Id_Publicador.Value = ddl_Publicador.SelectedValue
            Pendiente.Id_Responsable.Value = ddl_Responsable.SelectedValue
            Pendiente.Nombre.Value = tbx_NombrePendiente.Text
            Pendiente.Descripcion.Value = tbx_Descripcion.Text
            Pendiente.Prioridad.Value = ddl_Prioridad.SelectedValue
            Pendiente.FechaProgramado.ValueLocalized = tbx_FechaProgramado.SelectedDate
            Pendiente.Id_Pendiente.Where.EqualCondition(Id_Pendiente)
            connection.executeUpdate(queryBuilder.UpdateQuery(Pendiente))
            Return True
        Catch exc As Exception
            MyMaster.MostrarMensaje(exc.Message, True)

            Return False
        End Try
    End Function

    Protected Function deletePendiente(ByVal Id_Pendiente As Integer) As Integer
        Dim Pendiente As New Pendiente
        Try
            Pendiente.Id_Pendiente.Where.EqualCondition(Id_Pendiente)
            connection.executeDelete(queryBuilder.DeleteQuery(Pendiente))
            Return True
        Catch exc As Exception
            MyMaster.MostrarMensaje(exc.Message, True)

            Return False
        End Try
    End Function

    'Entidades
    Protected Sub selectEntidades(ByVal id_entidad As Integer)
        Dim dataSet As Data.DataSet
        Dim entidad As New Entidad

        entidad.Id_entidad.ToSelect = True
        entidad.NombreDisplay.ToSelect = True
        entidad.Apellido.ToSelect = True
        dataSet = connection.executeSelect(queryBuilder.SelectQuery(entidad))

        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables.Count > 0 Then
                ddl_Publicador.DataSource = dataSet
                ddl_Publicador.DataTextField = entidad.NombreDisplay.Name
                ddl_Publicador.DataValueField = entidad.Id_entidad.Name
                ddl_Publicador.DataBind()
                ddl_Publicador.Visible = True
                If id_entidad <> 0 Then
                    ddl_Publicador.SelectedValue = id_entidad
                End If

                ddl_Responsable.DataSource = dataSet
                ddl_Responsable.DataTextField = entidad.NombreDisplay.Name
                ddl_Responsable.DataValueField = entidad.Id_entidad.Name
                ddl_Responsable.DataBind()
                ddl_Responsable.Visible = True
                If id_entidad <> 0 Then
                    ddl_Responsable.SelectedValue = id_entidad
                End If
            End If
        End If
    End Sub

    'Eventos
    Protected Sub btn_Salvar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Salvar.Click

        If IsValid() Then
            If insertPendiente() Then
                ClearInfo(Configuration.EstadoPantalla.NORMAL, False)
                selectPendientes(securityHandler.Entidad)
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Salvar, "Pendiente", False)
            Else
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Salvar, "pendiente", True)
            End If
        Else
            MyMaster.MostrarMensaje(Resources.Orbecatalog_Resources.CamposRequeridos, False)
        End If
        upd_Contenido.Update()
        upd_Busqueda.Update()
    End Sub

    Protected Sub btn_Modificar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Modificar.Click

        If IsValid() Then
            If updatePendiente(tbx_NombrePendiente.ToolTip) Then
                'ClearInfo(Configuration.EstadoPantalla.NORMAL, False)
                selectPendientes(securityHandler.Entidad)
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Modificar, "Pendiente", False)
            Else
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Modificar, "pendiente", True)
            End If
        Else
            MyMaster.MostrarMensaje(Resources.Orbecatalog_Resources.CamposRequeridos, False)
        End If
        upd_Busqueda.Update()
    End Sub

    Protected Sub btn_Cancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Cancelar.Click
        ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
        upd_Contenido.Update()
        upd_Busqueda.Update()
    End Sub

    Protected Sub btn_Eliminar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Eliminar.Click

        If deletePendiente(tbx_NombrePendiente.ToolTip) Then
            ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
            MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Eliminar, "Pendiente", False)
        Else
            MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Eliminar, "pendiente", True)
        End If
        upd_Contenido.Update()
        upd_Busqueda.Update()
    End Sub

    Protected Sub dg_Pendientes_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dg_Pendientes.EditCommand
        ClearInfo(Configuration.EstadoPantalla.CONSULTAR, True)
        Me.EditCommandDataGrid(dg_Pendientes, e.Item.ItemIndex)
        loadPendiente(dg_Pendientes.DataKeys(e.Item.ItemIndex))
        upd_Contenido.Update()
        upd_Busqueda.Update()
    End Sub

    Protected Sub dg_Pendientes_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dg_Pendientes.PageIndexChanged
        dg_Pendientes.CurrentPageIndex = e.NewPageIndex
        selectPendientes(securityHandler.Entidad)
        Me.PageIndexChange(dg_Pendientes)
        upd_Busqueda.Update()
    End Sub

    Protected Sub ddl_Filtro_Estados_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddl_Filtro_Estados.SelectedIndexChanged
        dg_Pendientes.CurrentPageIndex = 0
        selectPendientes(securityHandler.Entidad)
    End Sub
End Class
