Imports Orbelink.DBHandler
Imports Orbelink.Entity.Acciones
Imports Orbelink.Entity.Currency
Imports Orbelink.Control.Acciones
Imports Orbelink.Orbecatalog6

Partial Class Orbecatalog_Acciones_MachoteAccion
    Inherits PageBaseClass

    Const codigo_pantalla As String = "AC-03"
    Const level As Integer = 2

    'Pagina
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddScripts(level)
        securityHandler.VerifyPantalla(codigo_pantalla, level)
        If Not IsPostBack Then
            dg_MachoteAccion.Attributes.Add("SelectedIndex", -1)
            dg_MachoteAccion.Attributes.Add("SelectedPage", -1)
            ClearInfo(Configuration.EstadoPantalla.NORMAL, False)
            securityHandler.VerifyActions(btn_Salvar, btn_Modificar, btn_Eliminar)

            selectMachoteAccion()
            selectMoneda()
        End If
    End Sub

    Protected Sub ClearInfo(ByVal estado As Configuration.EstadoPantalla, ByVal clearInfo As Boolean)
        If clearInfo Then
            vld_Nombre.IsValid = True
            tbx_NombreMachoteAccion.Text = ""
            tbx_PrefijoCodigo.Text = ""
            tbx_Valor.Text = ""
            tbx_Diasvalidez.Text = ""
            tbx_FechaVencimiento.Text = ""
            id_Actual = 0
            Me.ClearDataGrid(dg_MachoteAccion, clearInfo)
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

    'MachoteAccion
    Protected Sub selectMachoteAccion()
        Dim dataSet As Data.DataSet
        Dim MachoteAccion As New MachoteAccion

        MachoteAccion.Fields.SelectAll()
        queryBuilder.Orderby.Add(MachoteAccion.Nombre)
        dataSet = connection.executeSelect(queryBuilder.SelectQuery(MachoteAccion))

        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables(0).Rows.Count > 0 Then
                dg_MachoteAccion.DataSource = dataSet
                dg_MachoteAccion.DataKeyField = MachoteAccion.Id_MachoteAccion.Name
                dg_MachoteAccion.DataBind()

                'Llena el grid
                Dim offset As Integer = dg_MachoteAccion.CurrentPageIndex * dg_MachoteAccion.PageSize
                Dim results_MachoteAccion As ArrayList = ObjectBuilder.TransformDataTable(dataSet.Tables(0), MachoteAccion)

                For counter As Integer = 0 To dg_MachoteAccion.Items.Count - 1
                    Dim act_MachoteAccion As MachoteAccion = results_MachoteAccion(offset + counter)
                    Dim lbl_NombreAtributo As Label = dg_MachoteAccion.Items(counter).FindControl("lbl_Nombre")
                    lbl_NombreAtributo.Text = act_MachoteAccion.Nombre.Value

                    'Javascript
                    dg_MachoteAccion.Items(counter).Attributes.Add("onmouseover", "setRowFocus(this, " & counter & ", false)")
                    dg_MachoteAccion.Items(counter).Attributes.Add("onmouseout", "unsetRowFocus(this, " & counter & ", false)")
                Next
                lbl_ResultadoElementos.Text = contadorElementos(offset, dg_MachoteAccion.PageSize, dataSet.Tables(0).Rows.Count)
                dg_MachoteAccion.Visible = True
            Else
                lbl_ResultadoElementos.Text = contadorElementos(0, 0, 0)
                dg_MachoteAccion.Visible = False
            End If
        End If
    End Sub

    Protected Sub loadMachoteAccion(ByVal controladora As MachoteAccionesHandler, ByVal id_MachoteAccion As Integer)
        Dim MachoteAccion As MachoteAccion = controladora.ConsultarMachoteAccion(id_MachoteAccion)
        If MachoteAccion IsNot Nothing Then
            tbx_NombreMachoteAccion.Text = MachoteAccion.Nombre.Value
            tbx_PrefijoCodigo.Text = MachoteAccion.PrefijoCodigo.Value
            tbx_Valor.Text = MachoteAccion.Valor.Value
            tbx_Diasvalidez.Text = MachoteAccion.DiasValidez.Value
            If MachoteAccion.FechaExpiracion.IsValid Then
                tbx_FechaVencimiento.Text = MachoteAccion.FechaExpiracion.ValueLocalized
            End If
            id_Actual = MachoteAccion.Id_MachoteAccion.Value
        End If
    End Sub

    Protected Function insertMachoteAccion(ByVal controladora As MachoteAccionesHandler) As Boolean
        Dim MachoteAccion As New MachoteAccion(tbx_NombreMachoteAccion.Text, tbx_PrefijoCodigo.Text, Date.UtcNow, securityHandler.Usuario, tbx_Valor.Text, ddl_Moneda.SelectedValue, tbx_Diasvalidez.Text, cln_FechaVencimiento.SelectedDate)
        Return controladora.CrearMachoteAccion(MachoteAccion)
    End Function

    Protected Function updateMachoteAccion(ByVal controladora As MachoteAccionesHandler, ByVal id_MachoteAccion As Integer) As Boolean
        Dim MachoteAccion As New MachoteAccion()
        MachoteAccion.Nombre.Value = tbx_NombreMachoteAccion.Text
        MachoteAccion.PrefijoCodigo.Value = tbx_PrefijoCodigo.Text
        MachoteAccion.Valor.Value = tbx_Valor.Text
        MachoteAccion.Moneda.Value = ddl_Moneda.SelectedValue
        MachoteAccion.DiasValidez.Value = tbx_Diasvalidez.Text
        If tbx_FechaVencimiento.Text.Length > 0 Then
            MachoteAccion.FechaExpiracion.ValueLocalized = cln_FechaVencimiento.SelectedDate
        End If
        Return controladora.ActualizarMachoteAccion(MachoteAccion, id_MachoteAccion)
    End Function

    'Moneda      
    Protected Sub selectMoneda()
        Dim Moneda As New Moneda
        Moneda.Fields.SelectAll()
        queryBuilder.Orderby.Add(Moneda.Nombre)
        Dim dataTable As Data.DataTable = connection.executeSelect_DT(queryBuilder.SelectQuery(Moneda))
        If dataTable.Rows.Count > 0 Then
            ddl_Moneda.DataSource = dataTable
            ddl_Moneda.DataTextField = Moneda.Nombre.Name
            ddl_Moneda.DataValueField = Moneda.Id_Moneda.Name
            ddl_Moneda.DataBind()
        End If
    End Sub

    'Eventos
    Protected Sub btn_Salvar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Salvar.Click
        If IsValid() Then
            Dim controladora As New MachoteAccionesHandler(Configuration.Config_DefaultConnectionString)
            If insertMachoteAccion(controladora) Then
                ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
                selectMachoteAccion()
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Salvar, "Machote Accion", False)
                upd_Contenido.Update()
                upd_Busqueda.Update()
            Else
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Salvar, "Machote Accion", True)
            End If
        Else
            MyMaster.MostrarMensaje(Resources.Orbecatalog_Resources.CamposRequeridos, False)
        End If
    End Sub

    Protected Sub btn_Modificar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Modificar.Click
        If IsValid() Then
            Dim controladora As New MachoteAccionesHandler(Configuration.Config_DefaultConnectionString)
            If updateMachoteAccion(controladora, id_Actual) Then
                selectMachoteAccion()
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Modificar, "Machote Accion", False)
            Else
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Modificar, "Machote Accion", True)
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
        Dim controladora As New MachoteAccionesHandler(Configuration.Config_DefaultConnectionString)
        If controladora.BorrarMachoteAccion(id_Actual) Then
            ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
            selectMachoteAccion()
            MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Eliminar, "Machote Accion", False)
            upd_Contenido.Update()
            upd_Busqueda.Update()
        Else
            MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Eliminar, "Machote Accion", True)
        End If
    End Sub

    Protected Sub dg_MachoteAccion_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dg_MachoteAccion.EditCommand
        ClearInfo(Configuration.EstadoPantalla.CONSULTAR, True)
        Me.EditCommandDataGrid(dg_MachoteAccion, e.Item.ItemIndex)
        Dim controladora As New MachoteAccionesHandler(Configuration.Config_DefaultConnectionString)
        loadMachoteAccion(controladora, dg_MachoteAccion.DataKeys(e.Item.ItemIndex))
        upd_Contenido.Update()
        upd_Busqueda.Update()
    End Sub

    Protected Sub dg_MachoteAccion_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dg_MachoteAccion.PageIndexChanged
        dg_MachoteAccion.CurrentPageIndex = e.NewPageIndex
        selectMachoteAccion()
        Me.PageIndexChange(dg_MachoteAccion)
        upd_Busqueda.Update()
    End Sub
End Class
