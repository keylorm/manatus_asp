Imports Orbelink.Orbecatalog6
Imports Orbelink.Entity.Productos

Partial Class ProductosPagPrinc
    Inherits PageBaseClass

    Const codigo_pantalla As String = "PR-09"
    Const level As Integer = 2

    'Pagina
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddScripts(level)
        securityHandler.VerifyPantalla(codigo_pantalla, level)

        If Not IsPostBack Then
            Me.Title = Resources.Orbecatalog_Resources.Orbecatalog & " - " & Resources.Productos_Resources.PaginaPrincipal_Pantalla

            'securityHandler.VerifyActions(btn_Salvar, btn_Modificar, btn_Eliminar)
            selectProductos_Activos()
            selectProductos_Principal()
        End If
        upd_existentes.Update()
    End Sub

    'Productos
    Protected Sub selectProductos_Activos()
        Dim dataSet As Data.DataSet
        Dim producto As New Producto

        producto.Id_Producto.ToSelect = True
        producto.Nombre.ToSelect = True
        producto.Activo.Where.EqualCondition(1)
        producto.enPrinc.Where.EqualCondition(0)

        queryBuilder.From.Add(producto)
        dataSet = connection.executeSelect(queryBuilder.RelationalSelectQuery)
        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables(0).Rows.Count > 0 Then
                lstbx_activos.DataSource = dataSet
                lstbx_activos.DataTextField = producto.Nombre.Name
                lstbx_activos.DataValueField = producto.Id_Producto.Name
                lstbx_activos.DataBind()
            End If
        End If
        upd_existentes.Update()
    End Sub

    Protected Sub selectProductos_Principal()
        Dim dataSet As Data.DataSet
        Dim producto As New Producto
        Dim tipoProducto As New TipoProducto

        producto.Id_Producto.ToSelect = True
        producto.Nombre.ToSelect = True
        producto.Activo.Where.EqualCondition(1)
        producto.enPrinc.Where.EqualCondition(1)

        queryBuilder.From.Add(producto)
        dataSet = connection.executeSelect(queryBuilder.RelationalSelectQuery)
        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables(0).Rows.Count > 0 Then
                lstbx_principal.DataSource = dataSet
                lstbx_principal.DataTextField = producto.Nombre.Name
                lstbx_principal.DataValueField = producto.Id_Producto.Name
                lstbx_principal.DataBind()
            End If
        End If
        upd_existentes.Update()
    End Sub

    Sub insertarProductosPrincipales()
        Dim producto As New Producto
        Dim item As ListItem

        If lstbx_principal.Items.Count > 0 Then
            If lstbx_principal.Items.Count <= Configuration.maxProductosPagPrincipal Then
                For Each item In lstbx_principal.Items
                    producto.Id_Producto.Where.EqualCondition(item.Value)
                    producto.enPrinc.Value = 1
                    connection.executeUpdate(queryBuilder.UpdateQuery(producto))
                Next
            Else
                MyMaster.mostrarMensaje("Solo puede(n) seleccionar " & Configuration.maxProductosPagPrincipal & "  producto(s) para la página principal", True)
            End If
        Else
            MyMaster.mostrarMensaje("No hay productos en la principal", True)
        End If
        upd_existentes.Update()
    End Sub

    Sub limpiarProductosPrincipal()
        Dim producto As New Producto
        producto.enPrinc.Value = 0
        producto.enPrinc.Where.EqualCondition(1)
        connection.executeUpdate(queryBuilder.UpdateQuery(producto))
        upd_existentes.Update()
    End Sub

    Private Sub btn_Agregar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_agregar.Click
        Dim producto_Item As ListItem = lstbx_activos.SelectedItem
        Try
            If lstbx_activos.SelectedItem.Value <> -1 Then
                If lstbx_principal.Items.Count < Configuration.maxProductosPagPrincipal Then
                    lstbx_principal.Items.Add(producto_Item)
                    lstbx_activos.Items.Remove(producto_Item)
                    producto_Item.Selected = False
                Else
                    MyMaster.MostrarMensaje("Está intentando agregar más productos de los permitidos. Esta acción no es permitida.", True)
                End If
            End If
            upd_existentes.Update()
        Catch exc As Exception
            'No hay nada seleccionado
        End Try
    End Sub

    Private Sub btn_Borrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_borrar.Click
        Dim producto_Item As ListItem = lstbx_principal.SelectedItem
        Try
            If lstbx_principal.SelectedItem.Value <> -1 Then
                lstbx_activos.Items.Add(producto_Item)
                lstbx_principal.Items.Remove(producto_Item)
                producto_Item.Selected = False
            End If
            upd_existentes.Update()
            ''MyMaster.mostrarMensaje("Borrado exitosamente.", False)
        Catch exc As Exception
            ''MyMaster.mostrarMensaje("Error al Borrar.", True)
            'No hay nada seleccionado
        End Try
    End Sub

    Protected Sub btn_Salvar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Salvar.Click
        limpiarProductosPrincipal()
        insertarProductosPrincipales()
        selectProductos_Activos()
        selectProductos_Principal()
        MyMaster.mostrarMensaje("Salvado exitosamente.", False)
        upd_existentes.Update()
    End Sub

    Protected Sub btn_Cancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Cancelar.Click
        selectProductos_Activos()
        selectProductos_Principal()
        upd_existentes.Update()
    End Sub

    Protected Sub btn_Eliminar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Eliminar.Click
        limpiarProductosPrincipal()
        selectProductos_Activos()
        selectProductos_Principal()
        upd_existentes.Update()
    End Sub
End Class
