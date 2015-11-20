Imports Orbelink.DBHandler
Imports Orbelink.Entity.Productos
Imports Orbelink.Entity.Currency

Partial Class _Unidades
    Inherits PageBaseClass

    Const codigo_pantalla As String = "OR-09"
    Const level As Integer = 1

    'Pagina

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddScripts(level)

        securityHandler.VerifyPantalla(codigo_pantalla, level)

        If Not IsPostBack Then
            dg_Unidades.Attributes.Add("SelectedIndex", -1)
            dg_Unidades.Attributes.Add("SelectedPage", -1)
            ClearInfo(Configuration.EstadoPantalla.NORMAL, False)
            securityHandler.VerifyActions(btn_Salvar, btn_Modificar, btn_Eliminar)

            selectUnidades()
        End If
    End Sub

    Protected Sub ClearInfo(ByVal estado As Configuration.EstadoPantalla, ByVal clearInfo As Boolean)
        If clearInfo Then
            vld_tbx_NombreUnidades.IsValid = True
            tbx_NombreUnidades.Text = ""
            tbx_NombreUnidades.ToolTip = ""
            tbx_simbolo.Text = ""
            Me.ClearDataGrid(dg_Unidades, clearInfo)
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

    'Moneda
    Protected Sub selectUnidades()
        Dim dataSet As Data.DataSet
        Dim Moneda As New Moneda
        Moneda.Fields.SelectAll()
        queryBuilder.Orderby.Add(Moneda.Nombre)
        dataSet = connection.executeSelect(queryBuilder.SelectQuery(Moneda))
        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables(0).Rows.Count > 0 Then
                dg_Unidades.DataSource = dataSet
                dg_Unidades.DataKeyField = Moneda.Id_Moneda.Name
                dg_Unidades.DataBind()

                'Llena el grid
                Dim offset As Integer = dg_Unidades.CurrentPageIndex * dg_Unidades.PageSize
                Dim counter As Integer
                Dim resultados As ArrayList = ObjectBuilder.TransformDataTable(dataSet.Tables(0), Moneda)
                For counter = 0 To dg_Unidades.Items.Count - 1
                    Dim act_unidades As Moneda = resultados(offset + counter)
                    Dim lbl_Nombre As Label = dg_Unidades.Items(counter).FindControl("lbl_Nombre")
                    lbl_Nombre.Text = act_unidades.Nombre.Value

                    'Javascript
                    dg_Unidades.Items(counter).Attributes.Add("onmouseover", "setRowFocus(this, " & counter & ", false)")
                    dg_Unidades.Items(counter).Attributes.Add("onmouseout", "unsetRowFocus(this, " & counter & ", false)")
                Next
                lbl_ResultadoElementos.Text = contadorElementos(offset, dg_Unidades.PageSize, dataSet.Tables(0).Rows.Count)
                dg_Unidades.Visible = True
            Else
                lbl_ResultadoElementos.Text = contadorElementos(0, 0, 0)
                dg_Unidades.Visible = False
            End If
        End If
    End Sub

    Protected Sub loadUnidades(ByVal Id_Moneda As Integer)
        Dim dataSet As Data.DataSet
        Dim Moneda As New Moneda

        Moneda.Fields.SelectAll()
        Moneda.Id_Moneda.Where.EqualCondition(Id_Moneda)
        dataSet = connection.executeSelect(queryBuilder.SelectQuery(Moneda))
        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables(0).Rows.Count > 0 Then
                ObjectBuilder.CreateObject(dataSet.Tables(0), 0, Moneda)

                tbx_NombreUnidades.Text = Moneda.Nombre.Value
                tbx_NombreUnidades.ToolTip = Moneda.Id_Moneda.Value

                tbx_simbolo.Text = Moneda.Simbolo.Value
            End If
        End If
    End Sub

    Protected Function updateUnidades(ByVal Id_Moneda As Integer) As Boolean
        Dim Moneda As Moneda
        Try
            Moneda = New Moneda(tbx_NombreUnidades.Text, tbx_simbolo.Text)
            Moneda.Id_Moneda.Where.EqualCondition(Id_Moneda)
            connection.executeUpdate(queryBuilder.UpdateQuery(Moneda))
            Return True
        Catch exc As Exception
            MyMaster.mostrarMensaje(exc.Message, True)

            Return False
        End Try
    End Function

    Protected Function insertarUnidades() As Boolean
        Dim Moneda As Moneda
        Try
            Moneda = New Moneda(tbx_NombreUnidades.Text, tbx_simbolo.Text)
            connection.executeInsert(queryBuilder.InsertQuery(Moneda))
            Return True
        Catch exc As Exception
            MyMaster.mostrarMensaje(exc.Message, True)

            Return False
        End Try
    End Function

    Protected Function deleteUnidades(ByVal Id_Moneda As Integer) As Boolean
        Dim Moneda As New Moneda
        Try
            Moneda.Id_Moneda.Where.EqualCondition(Id_Moneda)
            connection.executeDelete(queryBuilder.DeleteQuery(Moneda))
            Return True
        Catch exc As Exception
            MyMaster.mostrarMensaje(exc.Message, True)

            Return False
        End Try
    End Function

    'Eventos
    Protected Sub btn_Salvar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Salvar.Click

        If IsValid() Then
            If insertarUnidades() Then
                ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
                selectUnidades()
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Salvar, "Unidad", False)
            Else
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Salvar, "Unidad", True)
            End If
        Else
            MyMaster.mostrarMensaje("Algun campo necesario está vacio o erroneo.", False)
        End If
        upd_Contenido.Update()
        upd_Busqueda.Update()
    End Sub

    Protected Sub btn_Modificar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Modificar.Click

        If IsValid() Then
            If updateUnidades(tbx_NombreUnidades.ToolTip) Then
                'ClearInfo(Configuration.EstadoPantalla.NORMAL)
                selectUnidades()
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Modificar, "Unidad", False)
            Else
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Modificar, "Unidad", True)
            End If
        Else
            MyMaster.mostrarMensaje("Algun campo necesario está vacio o erroneo.", False)
        End If
        upd_Busqueda.Update()
    End Sub

    Protected Sub btn_Cancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Cancelar.Click
        ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
        upd_Contenido.Update()
        upd_Busqueda.Update()
    End Sub

    Protected Sub btn_Eliminar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Eliminar.Click

        If deleteUnidades(tbx_NombreUnidades.ToolTip) Then
            ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
            selectUnidades()
            MyMaster.mostrarMensaje("Unidad borrada exitosamente.", False)
        Else
            MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Eliminar, "Unidad", True)
        End If
        upd_Contenido.Update()
        upd_Busqueda.Update()
    End Sub

    Protected Sub dg_Unidades_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dg_Unidades.EditCommand
        ClearInfo(Configuration.EstadoPantalla.CONSULTAR, True)
        Me.EditCommandDataGrid(dg_Unidades, e.Item.ItemIndex)
        loadUnidades(dg_Unidades.DataKeys(e.Item.ItemIndex))
        upd_Contenido.Update()
        upd_Busqueda.Update()
    End Sub

    Protected Sub dg_Unidades_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dg_Unidades.PageIndexChanged
        dg_Unidades.CurrentPageIndex = e.NewPageIndex
        selectUnidades()
        Me.PageIndexChange(dg_Unidades)
        upd_Busqueda.Update()
    End Sub

End Class
