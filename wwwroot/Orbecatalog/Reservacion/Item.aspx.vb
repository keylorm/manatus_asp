Imports Orbelink.DBHandler
Imports Orbelink.Entity.Productos
Imports Orbelink.Entity.Currency

Partial Class Orbecatalog_Reservacion_Item
    Inherits PageBaseClass

    Const codigo_pantalla As String = "Rs-03"
    Const level As Integer = 2

    Protected Sub Page_Load1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddScripts(level)
        securityHandler.VerifyPantalla(codigo_pantalla, level)

        If Not IsPostBack Then
            ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
            securityHandler.VerifyActions(btn_Salvar, btn_Modificar, btn_Eliminar)
            cargarProductos()
            cargarItemsDerecha()
            Me.Hyl_AgregarProducto.NavigateUrl = MyMaster.obtenerIframeString("../Productos/Productos.aspx")
        End If

    End Sub

    Protected Sub cargarProductos()

        Dim Producto As New Producto
        Producto.Nombre.ToSelect = True
        Producto.Id_Producto.ToSelect = True
        Producto.Activo.Where.EqualCondition(1)
        Dim consulta As String = queryBuilder.SelectQuery(Producto)
        Dim dataTable As Data.DataTable = connection.executeSelect_DT(consulta)

        If dataTable.Rows.Count > 0 Then
            ddl_productos.DataSource = dataTable
            ddl_productosDer.DataSource = dataTable
            ddl_productos.DataTextField = Producto.Nombre.Name
            ddl_productosDer.DataTextField = Producto.Nombre.Name
            ddl_productos.DataValueField = Producto.Id_Producto.Name
            ddl_productosDer.DataValueField = Producto.Id_Producto.Name
            ddl_productos.DataBind()
            ddl_productosDer.DataBind()
        End If
    End Sub

    Protected Sub ClearInfo(ByVal estado As Configuration.EstadoPantalla, ByVal clearInfo As Boolean)
        If clearInfo Then
            Me.txb_sku.Text = ""
            Me.txb_cantidad.Text = ""
            tbx_Descripcion.Text = ""
        End If

        Select Case estado
            Case Configuration.EstadoPantalla.NORMAL
                btn_Modificar.Visible = False
                btn_Salvar.Visible = True
                btn_Eliminar.Visible = False
                txb_cantidad.Enabled = True
                ddl_productos.Enabled = True

            Case Configuration.EstadoPantalla.CONSULTAR
                btn_Modificar.Visible = True
                btn_Salvar.Visible = False
                btn_Eliminar.Visible = True
                txb_cantidad.Enabled = False
                ddl_productos.Enabled = False
        End Select
    End Sub

    Protected Sub btn_Salvar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Salvar.Click
        Dim item As New Item
        Dim cantidadItems As Integer = txb_cantidad.Text
        Dim exito As Boolean = True

        For contador As Integer = 0 To cantidadItems - 1
            item.Id_producto.Value = ddl_productos.SelectedValue
            item.ordinal.Value = nuevoOrdinal(ddl_productos.SelectedValue)
            item.SKU.Value = txb_sku.Text
            item.descripcion.Value = tbx_Descripcion.Text

            Try
                Dim consulta As String = queryBuilder.InsertQuery(item)
                connection.executeInsert(consulta)
            Catch ex As Exception
                exito = False
                MyMaster.MostrarMensaje("Error al agregar Item(s)", True)
            End Try
        Next
        If exito Then
            MyMaster.MostrarMensaje("Item(s) agregados exitosamente", False)
            cargarItemsDerecha()
        End If
        ClearInfo(Configuration.EstadoPantalla.CONSULTAR, False)
    End Sub

    Protected Function nuevoOrdinal(ByVal id_producto As Integer) As Integer
        Dim ordinal As Integer = 0

        Dim item As New Item
        item.ordinal.ToSelect = True
        item.Id_producto.Where.EqualCondition(id_producto)
        queryBuilder.Orderby.Add(item.ordinal, False)
        queryBuilder.Top = 1
        Dim consulta As String = queryBuilder.SelectQuery(item)
        Dim dataTable As Data.DataTable = connection.executeSelect_DT(consulta)
        If dataTable.Rows.Count > 0 Then
            ObjectBuilder.CreateObject(dataTable, 0, item)
            ordinal = item.ordinal.Value + 1
        End If
        Return ordinal
    End Function

    Protected Sub btn_Cancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Cancelar.Click
        ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
    End Sub

    Protected Sub btn_Eliminar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Eliminar.Click
        Try
            Dim item As New Item
            item.Id_producto.Where.EqualCondition(ddl_productos.SelectedValue)
            item.ordinal.Where.EqualCondition(hf_ordinal.Value)
            Dim consulta As String = queryBuilder.DeleteQuery(item)
            connection.executeDelete(consulta)
            MyMaster.MostrarMensaje("Item Eliminado Exitosamente", False)
            ClearInfo(Configuration.EstadoPantalla.NORMAL, False)
            cargarItemsDerecha()
        Catch ex As Exception
            MyMaster.MostrarMensaje(ex.Message, True)
        End Try
    End Sub

    Protected Sub btn_Modificar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Modificar.Click
        Try
            Dim item As New Item
            item.SKU.Value = txb_sku.Text
            item.descripcion.Value = tbx_Descripcion.Text
            item.Id_producto.Where.EqualCondition(ddl_productos.SelectedValue)
            item.ordinal.Where.EqualCondition(hf_ordinal.Value)
            Dim consulta As String = queryBuilder.UpdateQuery(item)
            connection.executeUpdate(consulta)
            MyMaster.MostrarMensaje("Item Modificado exitosamente", False)
            ClearInfo(Configuration.EstadoPantalla.CONSULTAR, False)
            cargarItemsDerecha()
        Catch ex As Exception
            MyMaster.MostrarMensaje(ex.Message, True)
        End Try
    End Sub

    Protected Sub ddl_productosDer_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddl_productosDer.SelectedIndexChanged
        cargarItemsDerecha()
    End Sub

    Protected Sub cargarItemsDerecha()
        Dim item As New Item


        item.Fields.SelectAll()
        If ddl_productosDer.SelectedValue.Length > 0 Then
            item.Id_producto.Where.EqualCondition(ddl_productosDer.SelectedValue)
        End If


        queryBuilder.From.Add(item)

        Dim consulta As String = queryBuilder.RelationalSelectQuery
        Dim dataTable As Data.DataTable = connection.executeSelect_DT(consulta)
        If dataTable.Rows.Count > 0 Then

            gv_buscarItems.DataSource = dataTable
            gv_buscarItems.DataBind()

            Dim resul_Item As ArrayList = TransformDataTable(dataTable, New Item)
            Dim resul_Unidad As ArrayList = TransformDataTable(dataTable, New Moneda)

            Dim counter As Integer = 0
            For Each unItem As GridViewRow In gv_buscarItems.Rows
                item = resul_Item(counter)

                Dim ordinal As Label = unItem.FindControl("lbl_ordinal")
                Dim descripcion As Label = unItem.FindControl("lbl_descripcion")

                ordinal.Text = item.ordinal.Value
                Dim laDescripcion As String = item.descripcion.Value
                If laDescripcion.Length > 18 And descripcion IsNot Nothing Then
                    descripcion.Text = laDescripcion.Substring(0, 18)
                Else
                    descripcion.Text = laDescripcion
                End If
                counter += 1
            Next
            gv_buscarItems.Visible = True
        Else
            gv_buscarItems.Visible = False
        End If
    End Sub

    Protected Sub gv_buscarItems_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gv_buscarItems.RowCommand
        Dim item As GridViewRow = gv_buscarItems.Rows(e.CommandArgument)
        Dim ordinal As Label = item.FindControl("lbl_ordinal")
        Dim id_producto As Integer = ddl_productosDer.SelectedValue

        Dim unItem As New Item
        unItem.Fields.SelectAll()
        unItem.Id_producto.Where.EqualCondition(id_producto)
        unItem.ordinal.Where.EqualCondition(ordinal.Text)

        Dim consulta As String = queryBuilder.SelectQuery(unItem)
        Dim dataTable As Data.DataTable = connection.executeSelect_DT(consulta)
        If dataTable.Rows.Count > 0 Then
            ObjectBuilder.CreateObject(dataTable, 0, unItem)
            ddl_productos.SelectedValue = unItem.Id_producto.Value
            hf_ordinal.Value = unItem.ordinal.Value
            Me.txb_sku.Text = unItem.SKU.Value
            Me.tbx_Descripcion.Text = unItem.descripcion.Value
            ClearInfo(Configuration.EstadoPantalla.CONSULTAR, False)
            udp_contenido.Update()
        End If
    End Sub
End Class
