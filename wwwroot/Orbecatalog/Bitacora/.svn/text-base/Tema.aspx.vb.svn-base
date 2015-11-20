Imports Orbelink.DBHandler
Imports Orbelink.Entity.Bitacora
Imports Orbelink.Control.Bitacoras

Partial Class Orbecatalog_Bitacora_Tema
    Inherits PageBaseClass

    Const codigo_pantalla As String = "BT-04"
    Const level As Integer = 2
    Dim controladoraActual As Orbelink.Control.Bitacoras.IControladorBitacora

    'ViewState
    Dim _temp As Integer
    Protected Property DuenoActual() As Integer
        Get
            If _temp <= 0 Then
                If ViewState("DuenoActual") IsNot Nothing Then
                    _temp = ViewState("DuenoActual")
                Else
                    ViewState.Add("DuenoActual", 0)
                    _temp = 0
                End If
            End If
            Return _temp
        End Get
        Set(ByVal value As Integer)
            _temp = value
            ViewState("DuenoActual") = _temp
        End Set
    End Property

    'Pagina

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddScripts(level)


        controladoraActual = obtenerControladora()
        If controladoraActual Is Nothing Then
            Response.Redirect("Default.aspx")
        End If

        securityHandler.VerifyPantalla(codigo_pantalla, level)
        If Not IsPostBack Then

            dg_Tema.Attributes.Add("SelectedIndex", -1)
            dg_Tema.Attributes.Add("SelectedPage", -1)
            ClearInfo(Configuration.EstadoPantalla.NORMAL, False)
            securityHandler.VerifyActions(btn_Salvar, btn_Modificar, btn_Eliminar)

            Dim bitacoraHandler As New BitacoraHandler(connection)
            selectTema(bitacoraHandler)
        End If
    End Sub

    Protected Sub ClearInfo(ByVal estado As Configuration.EstadoPantalla, ByVal clearInfo As Boolean)
        If clearInfo Then
            vld_Nombre.IsValid = True
            tbx_NombreTema.Text = ""
            Me.id_Actual = 0
            Me.ClearDataGrid(dg_Tema, clearInfo)
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

    Protected Function obtenerControladora() As IControladorBitacora
        If Request.QueryString("id_entidad") IsNot Nothing Then
            'DuenoActual = Request.QueryString("id_Entidad")
            'Return BitacoraHandler.Intermedio.Entidad
        ElseIf Request.QueryString("id_oportunidad") IsNot Nothing Then
            DuenoActual = Request.QueryString("id_oportunidad")
            'Return New Orbelink.Control.CRM.ControladoraTema_Oportunidad(connection)
        End If

        Return Nothing
    End Function

    'Tema
    Protected Sub selectTema(ByVal bitacoraHandler As BitacoraHandler)
        Dim dataSet As Data.DataSet
        Dim Tema As New Tema

        dataSet = bitacoraHandler.selectTemas(controladoraActual, DuenoActual)

        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables(0).Rows.Count > 0 Then
                dg_Tema.DataSource = dataSet
                dg_Tema.DataKeyField = Tema.Id_Tema.Name
                dg_Tema.DataBind()

                'Llena el grid
                Dim offset As Integer = dg_Tema.CurrentPageIndex * dg_Tema.PageSize
                Dim counter As Integer
                Dim result_Tema As ArrayList = ObjectBuilder.TransformDataTable(dataSet.Tables(0), Tema)
                For counter = 0 To dg_Tema.Items.Count - 1
                    Dim act_Tema As Tema = result_Tema(offset + counter)
                    Dim lbl_NombreAtributo As Label = dg_Tema.Items(counter).FindControl("lbl_Nombre")
                    lbl_NombreAtributo.Text = act_Tema.Nombre.Value

                    'Javascript
                    dg_Tema.Items(counter).Attributes.Add("onmouseover", "setRowFocus(this, " & counter & ", false)")
                    dg_Tema.Items(counter).Attributes.Add("onmouseout", "unsetRowFocus(this, " & counter & ", false)")
                Next
                lbl_ResultadoElementos.Text = contadorElementos(offset, dg_Tema.PageSize, dataSet.Tables(0).Rows.Count)
                dg_Tema.Visible = True
            Else
                lbl_ResultadoElementos.Text = contadorElementos(0, 0, 0)
                dg_Tema.Visible = False
            End If
        End If
    End Sub

    Protected Sub loadTema(ByVal id_Tema As Integer)
        Dim dataSet As Data.DataSet
        Dim Tema As New Tema
        Dim autoCompletar As Integer = 0

        Tema.Fields.SelectAll()
        Tema.Id_Tema.Where.EqualCondition(id_Tema)

        dataSet = connection.executeSelect(queryBuilder.SelectQuery(Tema))
        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables(0).Rows.Count > 0 Then
                ObjectBuilder.CreateObject(dataSet.Tables(0), 0, Tema)
                tbx_NombreTema.Text = Tema.Nombre.Value
                Me.id_Actual = Tema.Id_Tema.Value
            End If
        End If
    End Sub

    'Eventos
    Protected Sub btn_Salvar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Salvar.Click
        If IsValid() Then
            Dim bitacoraHandler As New BitacoraHandler(connection)

            If bitacoraHandler.CrearTema(controladoraActual, tbx_NombreTema.Text, DuenoActual) Then
                ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Salvar, "Tema", False)
                selectTema(bitacoraHandler)
            Else
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Salvar, "tema", True)
            End If
        Else
            MyMaster.MostrarMensaje(Resources.Orbecatalog_Resources.CamposRequeridos, True)
        End If
        upd_Contenido.Update()
        upd_Busqueda.Update()
    End Sub

    Protected Sub btn_Modificar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Modificar.Click
        If IsValid() Then
            Dim bitacoraHandler As New BitacoraHandler(connection)
            If bitacoraHandler.updateTema(tbx_NombreTema.Text, Me.id_Actual) Then
                selectTema(bitacoraHandler)
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Modificar, "Tema", False)
            Else
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Modificar, "tema", True)
            End If
        Else
            MyMaster.MostrarMensaje(Resources.Orbecatalog_Resources.CamposRequeridos, True)
        End If
        upd_Busqueda.Update()
    End Sub

    Protected Sub btn_Cancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Cancelar.Click
        ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
        upd_Contenido.Update()
        upd_Busqueda.Update()
    End Sub

    Protected Sub btn_Eliminar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Eliminar.Click
        Dim bitacoraHandler As New BitacoraHandler(connection)
        If bitacoraHandler.deleteTema(Me.id_Actual) Then
            ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
            selectTema(bitacoraHandler)
            MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Eliminar, "Tema", False)
        Else
            MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Eliminar, "tema", True)
        End If
        upd_Contenido.Update()
        upd_Busqueda.Update()
    End Sub

    Protected Sub dg_Tema_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dg_Tema.EditCommand
        ClearInfo(Configuration.EstadoPantalla.CONSULTAR, True)
        Me.EditCommandDataGrid(dg_Tema, e.Item.ItemIndex)
        loadTema(dg_Tema.DataKeys(e.Item.ItemIndex))
        upd_Contenido.Update()
        upd_Busqueda.Update()
    End Sub

    Protected Sub dg_Tema_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dg_Tema.PageIndexChanged
        dg_Tema.CurrentPageIndex = e.NewPageIndex
        Dim bitacoraHandler As New BitacoraHandler(connection)
        selectTema(bitacoraHandler)
        Me.PageIndexChange(dg_Tema)
        upd_Busqueda.Update()
    End Sub
End Class
