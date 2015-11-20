Imports Orbelink.DBHandler
Imports Orbelink.Entity.Productos

Partial Class _Estados_Productos
    Inherits PageBaseClass

    Const codigo_pantalla As String = "PR-11"
    Const level As Integer = 2

    'Pagina
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddScripts(level)
        securityHandler.VerifyPantalla(codigo_pantalla, level)
        If Not IsPostBack Then
            Me.Title = Resources.Orbecatalog_Resources.Orbecatalog & " - " & Resources.Productos_Resources.Estados_Pantalla
            dg_Estados_Productos.Attributes.Add("SelectedIndex", -1)
            dg_Estados_Productos.Attributes.Add("SelectedPage", -1)
            ClearInfo(Configuration.EstadoPantalla.NORMAL, False)
            securityHandler.VerifyActions(btn_Salvar, btn_Modificar, btn_Eliminar)

            selectEstados_Productos()
        End If
    End Sub

    Protected Sub ClearInfo(ByVal estado As Configuration.EstadoPantalla, ByVal clearInfo As Boolean)
        If clearInfo Then
            vld_tbx_NombreEstadoProducto.IsValid = True
            tbx_NombreEstadoProducto.Text = ""
            tbx_NombreEstadoProducto.ToolTip = ""
            Me.ClearDataGrid(dg_Estados_Productos, clearInfo)
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

    'Estados_Productos
    Protected Sub selectEstados_Productos()
        Dim dataSet As Data.DataSet
        Dim Estados_Productos As New Estados_Productos()
        Estados_Productos.Fields.SelectAll()
        queryBuilder.Orderby.Add(Estados_Productos.Nombre)
        dataSet = connection.executeSelect(queryBuilder.SelectQuery(Estados_Productos))
        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables(0).Rows.Count > 0 Then
                dg_Estados_Productos.DataSource = dataSet
                dg_Estados_Productos.DataKeyField = Estados_Productos.Id_Estado_Producto.Name
                dg_Estados_Productos.DataBind()

                'Llena el grid
                Dim offset As Integer = dg_Estados_Productos.CurrentPageIndex * dg_Estados_Productos.PageSize
                Dim counter As Integer
                Dim resul_Estados_Productos As ArrayList = ObjectBuilder.TransformDataTable(dataSet.Tables(0), Estados_Productos)
                For counter = 0 To dg_Estados_Productos.Items.Count - 1           'Recorre los atributos relacionados
                    Dim act_Estados_Productos As Estados_Productos = resul_Estados_Productos(counter)
                    Dim lbl_Nombre As Label = dg_Estados_Productos.Items(counter).FindControl("lbl_Nombre")
                    lbl_Nombre.Text = act_Estados_Productos.Nombre.Value

                    'Javascript
                    dg_Estados_Productos.Items(counter).Attributes.Add("onmouseover", "setRowFocus(this, " & counter & ", false)")
                    dg_Estados_Productos.Items(counter).Attributes.Add("onmouseout", "unsetRowFocus(this, " & counter & ", false)")
                Next
                lbl_ResultadoElementos.Text = contadorElementos(offset, dg_Estados_Productos.PageSize, dataSet.Tables(0).Rows.Count)
                dg_Estados_Productos.Visible = True
            Else
                lbl_ResultadoElementos.Text = contadorElementos(0, 0, 0)
                dg_Estados_Productos.Visible = False
            End If
        End If
    End Sub

    Protected Sub loadEstados_Productos(ByVal id_Estado_Producto As Integer)
        Dim dataSet As Data.DataSet
        Dim Estados_Productos As New Estados_Productos
        Estados_Productos.Fields.SelectAll()
        Estados_Productos.Id_Estado_Producto.Where.EqualCondition(id_Estado_Producto)
        dataSet = connection.executeSelect(queryBuilder.SelectQuery(Estados_Productos))
        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables(0).Rows.Count > 0 Then
                ObjectBuilder.CreateObject(dataSet.Tables(0), 0, Estados_Productos)
                tbx_NombreEstadoProducto.Text = Estados_Productos.Nombre.Value
                tbx_NombreEstadoProducto.ToolTip = Estados_Productos.Id_Estado_Producto.Value
            End If
        End If
    End Sub

    Protected Function updateEstados_Productos(ByVal id_Estado_Producto As Integer) As Boolean
        Dim Estados_Productos As Estados_Productos
        Try
            Estados_Productos = New Estados_Productos(tbx_NombreEstadoProducto.Text)
            Estados_Productos.Id_Estado_Producto.Where.EqualCondition(id_Estado_Producto)
            connection.executeUpdate(queryBuilder.UpdateQuery(Estados_Productos))
            Return True
        Catch exc As Exception
            MyMaster.mostrarMensaje(exc.Message, True)

            Return False
        End Try
    End Function

    Protected Function insertEstados_Productos(ByVal theTextBox As TextBox) As Boolean
        Dim Estados_Productos As Estados_Productos
        Try
            Estados_Productos = New Estados_Productos(theTextBox.Text)
            connection.executeInsert(queryBuilder.InsertQuery(Estados_Productos))
            Return True
        Catch exc As Exception
            MyMaster.mostrarMensaje(exc.Message, True)

            Return False
        End Try
    End Function

    Protected Function deleteEstados_Productos(ByVal id_Estado_Producto As Integer) As Boolean
        Dim Estados_Productos As New Estados_Productos
        Try
            Estados_Productos.Id_Estado_Producto.Where.EqualCondition(id_Estado_Producto)
            connection.executeDelete(queryBuilder.DeleteQuery(Estados_Productos))
        Catch exc As Exception
            MyMaster.mostrarMensaje(exc.Message, True)

        End Try
    End Function

    'Eventos
    Protected Sub btn_Salvar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Salvar.Click

        If IsValid() Then
            If insertEstados_Productos(tbx_NombreEstadoProducto) Then
                ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
                selectEstados_Productos()
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Salvar, "Estado de producto", False)
            Else
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Salvar, "Estado de producto", True)
            End If
        Else
            MyMaster.mostrarMensaje("Algun campo necesario está vacio o erroneo.", False)
        End If
        upd_Contenido.Update()
        upd_Busqueda.Update()
    End Sub

    Protected Sub btn_Modificar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Modificar.Click

        If IsValid() Then
            If updateEstados_Productos(tbx_NombreEstadoProducto.ToolTip) Then
                'ClearInfo(Configuration.EstadoPantalla.NORMAL)
                selectEstados_Productos()
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Modificar, "Estado de producto", False)
            Else
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Modificar, "Estado de producto", True)
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

        If deleteEstados_Productos(tbx_NombreEstadoProducto.ToolTip) Then
            ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
            selectEstados_Productos()
            MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Eliminar, "Estado de producto", False)
        Else
            MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Eliminar, "Estado de producto. <br /> Ningun producto debe estar con este estado seleccionado", True)
        End If
        upd_Contenido.Update()
        upd_Busqueda.Update()
    End Sub

    Protected Sub dg_Estados_Productos_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dg_Estados_Productos.EditCommand
        ClearInfo(Configuration.EstadoPantalla.CONSULTAR, True)
        Me.EditCommandDataGrid(dg_Estados_Productos, e.Item.ItemIndex)
        loadEstados_Productos(dg_Estados_Productos.DataKeys(e.Item.ItemIndex))
        upd_Contenido.Update()
        upd_Busqueda.Update()
    End Sub

    Protected Sub dg_Estados_Productos_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dg_Estados_Productos.PageIndexChanged
        dg_Estados_Productos.CurrentPageIndex = e.NewPageIndex
        selectEstados_Productos()
        Me.PageIndexChange(dg_Estados_Productos)
        upd_Busqueda.Update()
    End Sub
End Class
