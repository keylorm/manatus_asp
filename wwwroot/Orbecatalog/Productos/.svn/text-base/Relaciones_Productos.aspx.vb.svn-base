Imports Orbelink.DBHandler
Imports Orbelink.Entity.Productos

Partial Class _Relacion_Productos
    Inherits PageBaseClass

    Const codigo_pantalla As String = "PR-12"
    Const level As Integer = 2

    'Pagina
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddScripts(level)
        securityHandler.VerifyPantalla(codigo_pantalla, level)
        If Not IsPostBack Then
            Me.Title = Resources.Orbecatalog_Resources.Orbecatalog & " - " & Resources.Productos_Resources.Relaciones_Pantalla

            securityHandler.VerifyActions(btn_Salvar, btn_Modificar, btn_Eliminar)
            selectTiposProductos()
            selectTiposRelacion_Productos()

            Dim id_Tipo_Producto As Integer
            If ddl_Filtro_Tipo_Productos.Items.Count > 0 Then
                id_Tipo_Producto = ddl_Filtro_Tipo_Productos.SelectedValue
            End If

            Dim id_Producto As Integer
            If ddl_Filtro_Producto.Items.Count > 0 Then
                id_Producto = ddl_Filtro_Producto.SelectedValue
            End If
            selectProductos()
            selectProductos(id_Tipo_Producto)
            selectRelacion_Productos(id_Producto)

            Dim id_Relacion_Productos As String = Request.QueryString("id_Relacion_Productos")
            If Not id_Relacion_Productos Is Nothing Then
                resetCampos(Configuration.EstadoPantalla.CONSULTAR)
                cargarParaModificar(Request.QueryString("id_Relacion_Productos"))
            Else
                resetCampos(Configuration.EstadoPantalla.NORMAL)
            End If
        End If


    End Sub

    Protected Sub resetCampos(ByVal estado As Configuration.EstadoPantalla)
        tbx_ComentariosRelaciones.ToolTip = ""
        tbx_ComentariosRelaciones.Text = ""

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

        'ddl_ProductoBase.SelectedIndex = 0
        'ddl_ProductoDependiente.SelectedIndex = 0
        'ddl_TipoRelacion_Productos.SelectedIndex = 0
    End Sub

    'Relacion_Productos
    Protected Sub selectRelacion_Productos(ByVal Id_Producto As Integer)
        Dim ds1 As Data.DataSet
        Dim ds2 As Data.DataSet
        Dim Relacion_Productos As New Relacion_Productos
        Dim Producto As New Producto
        Dim TipoRelacion_Productos As New TipoRelacion_Productos
        Dim result As String

        If ddl_Filtro_TipoRelaciones.Items.Count > 0 Then
            'TipoRelacion
            TipoRelacion_Productos.Nombre.ToSelect = True
            queryBuilder.Join.EqualCondition(TipoRelacion_Productos.Id_TipoRelacion, Relacion_Productos.Id_TipoRelacion)

            'Productos
            Producto.Nombre.ToSelect = True
            Producto.Id_Producto.ToSelect = True

            'Relacion_Productos
            queryBuilder.Join.EqualCondition(Producto.Id_Producto, Relacion_Productos.Id_ProductoBase)
            Relacion_Productos.Fields.SelectAll()
            If Id_Producto <> 0 Then
                Relacion_Productos.Id_ProductoBase.Where.EqualCondition(Id_Producto, Where.FieldRelations.AND_)
                Relacion_Productos.Id_ProductoDependiente.Where.EqualCondition(Id_Producto, Where.FieldRelations.OR_)
                queryBuilder.GroupFieldsConditions(Relacion_Productos.Id_ProductoBase, Relacion_Productos.Id_ProductoDependiente)
            End If

            'Busca si hay filtro
            If Not ddl_Filtro_TipoRelaciones.SelectedIndex = ddl_Filtro_TipoRelaciones.Items.Count - 1 Then
                Relacion_Productos.Id_TipoRelacion.Where.EqualCondition(ddl_Filtro_TipoRelaciones.SelectedValue)
            End If

            'Select de Productos base
            queryBuilder.From.Add(Relacion_Productos)
            queryBuilder.From.Add(Producto)
            queryBuilder.From.Add(TipoRelacion_Productos)

            result = queryBuilder.RelationalSelectQuery
            ds1 = connection.executeSelect(result)

            If ds1.Tables.Count > 0 Then
                If ds1.Tables(0).Rows.Count > 0 Then
                    dg_Relacion_Productos.DataSource = ds1
                    dg_Relacion_Productos.DataKeyField = Relacion_Productos.Id_Relacion.Name
                    dg_Relacion_Productos.DataBind()

                    Dim results As ArrayList = ObjectBuilder.TransformDataTable(ds1.Tables(0), Relacion_Productos)

                    'Llena el grid
                    Dim limit As Integer = dg_Relacion_Productos.PageSize
                    Dim offset As Integer = dg_Relacion_Productos.CurrentPageIndex * limit
                    Dim counter As Integer
                    For counter = 0 To dg_Relacion_Productos.Items.Count - 1
                        Dim actual As Relacion_Productos = results(counter)

                        'Select de Productos dependiente
                        Producto.Fields.ClearAll()
                        Producto.Id_Producto.Where.EqualCondition(actual.Id_ProductoDependiente.Value)
                        Producto.Id_Producto.ToSelect = True
                        Producto.Nombre.ToSelect = True
                        result = queryBuilder.SelectQuery(Producto)
                        ds2 = connection.executeSelect(result)

                        Dim lbl_Relacion_Productos As Label = dg_Relacion_Productos.Items(counter).FindControl("lbl_Relacion_Productos")
                        lbl_Relacion_Productos.Text = ds2.Tables(0).Rows(0).Item(Producto.Nombre.Name)
                        lbl_Relacion_Productos.Text &= " es "
                        lbl_Relacion_Productos.Text &= ds1.Tables(0).Rows(offset + counter).Item(TipoRelacion_Productos.Nombre.Name)
                        lbl_Relacion_Productos.Text &= " de "
                        lbl_Relacion_Productos.Text &= ds1.Tables(0).Rows(offset + counter).Item(Producto.Nombre.Name)
                    Next
                    lbl_ResultadoElementos.Visible = False
                    dg_Relacion_Productos.Visible = True
                Else
                    lbl_ResultadoElementos.Visible = True
                    dg_Relacion_Productos.Visible = False
                End If
            End If
        End If
    End Sub

    Protected Function insertarRelacion_Productos() As Integer
        Dim sqlQuery As String
        Dim id_insertado As Integer
        Dim temp As Relacion_Productos
        Try
            temp = New Relacion_Productos(ddl_TipoRelacion_Productos.SelectedValue, ddl_ProductoBase.SelectedValue, _
                ddl_ProductoDependiente.SelectedValue, tbx_ComentariosRelaciones.Text)
            sqlQuery = queryBuilder.InsertQuery(temp)
            connection.executeInsert(sqlQuery)
            id_insertado = connection.lastKey(temp.TableName, temp.Id_Relacion.Name)
        Catch exc As Exception
            MyMaster.mostrarMensaje(exc.Message, True)

        End Try
        Return id_insertado
    End Function

    Protected Sub cargarParaModificar(ByVal indice As Integer)
        Dim dataSet As Data.DataSet
        Dim Relacion_Productos As New Relacion_Productos
        Dim Producto As New Producto
        Dim sqlQuery As String

        Relacion_Productos.Fields.SelectAll()
        Relacion_Productos.Id_Relacion.Where.EqualCondition(indice)

        Producto.Nombre.ToSelect = True
        Producto.Id_tipoProducto.ToSelect = True
        queryBuilder.Join.EqualCondition(Producto.Id_Producto, Relacion_Productos.Id_ProductoBase)
        queryBuilder.From.Add(Relacion_Productos)
        queryBuilder.From.Add(Producto)
        sqlQuery = queryBuilder.RelationalSelectQuery()
        dataSet = connection.executeSelect(sqlQuery)

        If dataSet.Tables.Count > 0 Then
            ObjectBuilder.CreateObject(dataSet.Tables(0), 0, Producto)
            ddl_Tipo_Producto_Base.SelectedValue = Producto.Id_tipoProducto.Value
            dataSet.Tables.Clear()
            Producto.Fields.ClearAll()

            Producto.Nombre.ToSelect = True
            Producto.Id_tipoProducto.ToSelect = True
            queryBuilder.Join.EqualCondition(Producto.Id_Producto, Relacion_Productos.Id_ProductoDependiente)
            queryBuilder.From.Add(Relacion_Productos)
            queryBuilder.From.Add(Producto)
            sqlQuery = queryBuilder.RelationalSelectQuery()
            dataSet = connection.executeSelect(sqlQuery)

            If dataSet.Tables.Count > 0 Then
                ObjectBuilder.CreateObject(dataSet.Tables(0), 0, Producto)
                ddl_Tipo_Producto_Dependiente.SelectedValue = Producto.Id_tipoProducto.Value

                selectProductos()

                ObjectBuilder.CreateObject(dataSet.Tables(0), 0, Relacion_Productos)
                tbx_ComentariosRelaciones.ToolTip = Relacion_Productos.Id_Relacion.Value
                tbx_ComentariosRelaciones.Text = Relacion_Productos.Comentarios.Value

                ddl_ProductoBase.SelectedValue = Relacion_Productos.Id_ProductoBase.Value
                ddl_ProductoDependiente.SelectedValue = Relacion_Productos.Id_ProductoDependiente.Value
                ddl_TipoRelacion_Productos.SelectedValue = Relacion_Productos.Id_TipoRelacion.Value
            End If
        End If
    End Sub

    Protected Function updateRelacion_Productos() As Integer
        Dim sqlQuery As String
        Dim id_insertado As Integer
        Dim temp As Relacion_Productos
        Try
            temp = New Relacion_Productos(ddl_TipoRelacion_Productos.SelectedValue, ddl_ProductoBase.SelectedValue, _
                ddl_ProductoDependiente.SelectedValue, tbx_ComentariosRelaciones.Text)
            temp.Id_Relacion.Where.EqualCondition(tbx_ComentariosRelaciones.ToolTip)

            sqlQuery = queryBuilder.UpdateQuery(temp)
            connection.executeUpdate(sqlQuery)
            id_insertado = connection.lastKey(temp.TableName, temp.Id_Relacion.Name)
        Catch exc As Exception
            MyMaster.mostrarMensaje(exc.Message, True)

        End Try
    End Function

    Protected Sub deleteRelacion_Productos()
        Dim temp As New Relacion_Productos
        Dim sqlQuery As String
        Try
            temp.Id_Relacion.Where.EqualCondition(tbx_ComentariosRelaciones.ToolTip)
            sqlQuery = queryBuilder.DeleteQuery(temp)
            connection.executeDelete(sqlQuery)
        Catch exc As Exception
            MyMaster.mostrarMensaje(exc.Message, True)

        End Try
    End Sub

    Protected Sub selectTiposRelacion_Productos()
        Dim ds As Data.DataSet
        Dim temp As New TipoRelacion_Productos
        Dim result As String
        Try
            temp.Fields.SelectAll()
            result = queryBuilder.SelectQuery(temp)
            ds = connection.executeSelect(result)
            If ds.Tables.Count > 0 Then
                ddl_TipoRelacion_Productos.DataSource = ds
                ddl_TipoRelacion_Productos.DataTextField = temp.Nombre.Name
                ddl_TipoRelacion_Productos.DataValueField = temp.Id_TipoRelacion.Name
                ddl_TipoRelacion_Productos.DataBind()

                ddl_Filtro_TipoRelaciones.DataSource = ds
                ddl_Filtro_TipoRelaciones.DataTextField = temp.Nombre.Name
                ddl_Filtro_TipoRelaciones.DataValueField = temp.Id_TipoRelacion.Name
                ddl_Filtro_TipoRelaciones.DataBind()
                ddl_Filtro_TipoRelaciones.Items.Add("-- Todos --")
                ddl_Filtro_TipoRelaciones.SelectedIndex = ddl_Filtro_TipoRelaciones.Items.Count - 1
            End If
        Catch exc As Exception
            MyMaster.mostrarMensaje(exc.Message, True)

        End Try
    End Sub

    'Tipo_Productos
    Protected Sub selectTiposProductos()
        Dim ds As Data.DataSet
        Dim temp As New TipoProducto
        Dim result As String
        temp.Fields.SelectAll()
        result = queryBuilder.SelectQuery(temp)
        ds = connection.executeSelect(result)
        If ds.Tables.Count > 0 Then
            If ds.Tables(0).Rows.Count > 0 Then
                ddl_Tipo_Producto_Base.DataSource = ds
                ddl_Tipo_Producto_Base.DataTextField = temp.Nombre.Name
                ddl_Tipo_Producto_Base.DataValueField = temp.Id_TipoProducto.Name
                ddl_Tipo_Producto_Base.DataBind()

                ddl_Tipo_Producto_Dependiente.DataSource = ds
                ddl_Tipo_Producto_Dependiente.DataTextField = temp.Nombre.Name
                ddl_Tipo_Producto_Dependiente.DataValueField = temp.Id_TipoProducto.Name
                ddl_Tipo_Producto_Dependiente.DataBind()

                ddl_Filtro_Tipo_Productos.DataSource = ds
                ddl_Filtro_Tipo_Productos.DataTextField = temp.Nombre.Name
                ddl_Filtro_Tipo_Productos.DataValueField = temp.Id_TipoProducto.Name
                ddl_Filtro_Tipo_Productos.DataBind()
            End If
        End If
    End Sub

    'Productos
    Protected Sub selectProductos(ByVal id_Tipo_Producto As Integer)
        Dim ds As Data.DataSet
        Dim Producto As New Producto
        Dim result As String
        Try
            Producto.Fields.SelectAll()
            If id_Tipo_Producto <> 0 Then
                Producto.Id_tipoProducto.Where.EqualCondition(id_Tipo_Producto)
            End If

            result = queryBuilder.SelectQuery(Producto)
            ds = connection.executeSelect(result)
            If ds.Tables.Count > 0 Then
                ddl_Filtro_Producto.DataSource = ds
                ddl_Filtro_Producto.DataTextField = Producto.Nombre.Name
                ddl_Filtro_Producto.DataValueField = Producto.Id_Producto.Name
                ddl_Filtro_Producto.DataBind()
                ddl_Filtro_Producto.Items.Add("-- Todos --")
                ddl_Filtro_Producto.Items(ddl_Filtro_Producto.Items.Count - 1).Value = 0
                ddl_Filtro_Producto.SelectedIndex = ddl_Filtro_Producto.Items.Count - 1
            End If
        Catch exc As Exception
            'vlr_Errores.Text = exc.Message
            'vlr_Errores.Visible = True
        End Try
    End Sub

    Protected Sub selectProductos()
        Dim ds As Data.DataSet
        Dim Producto As New Producto
        Dim sqlQuery As String
        If ddl_Tipo_Producto_Base.Items.Count > 0 Then
            Producto.Fields.SelectAll()
            Producto.Id_tipoProducto.Where.EqualCondition(ddl_Tipo_Producto_Base.SelectedValue)
            sqlQuery = queryBuilder.SelectQuery(Producto)
            ds = connection.executeSelect(sqlQuery)

            ddl_ProductoBase.DataSource = ds
            ddl_ProductoBase.DataTextField = Producto.Nombre.Name
            ddl_ProductoBase.DataValueField = Producto.Id_Producto.Name
            ddl_ProductoBase.DataBind()

            Producto.Fields.SelectAll()
            Producto.Id_tipoProducto.Where.EqualCondition(ddl_Tipo_Producto_Dependiente.SelectedValue)
            sqlQuery = queryBuilder.SelectQuery(Producto)
            ds = connection.executeSelect(sqlQuery)

            ddl_ProductoDependiente.DataSource = ds
            ddl_ProductoDependiente.DataTextField = Producto.Nombre.Name
            ddl_ProductoDependiente.DataValueField = Producto.Id_Producto.Name
            ddl_ProductoDependiente.DataBind()
        End If
    End Sub

    'Manejadores de Eventos
    Protected Sub btn_Modificar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Modificar.Click
        If IsValid() Then
            Dim id_update As Integer = updateRelacion_Productos()
            selectRelacion_Productos(ddl_Filtro_Producto.SelectedValue)
            resetCampos(Configuration.EstadoPantalla.NORMAL)
        End If
    End Sub

    Protected Sub btn_Cancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Cancelar.Click
        resetCampos(Configuration.EstadoPantalla.NORMAL)
    End Sub

    Protected Sub btn_Eliminar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Eliminar.Click
        deleteRelacion_Productos()
        selectRelacion_Productos(ddl_Filtro_Producto.SelectedValue)
        resetCampos(Configuration.EstadoPantalla.NORMAL)
    End Sub

    Protected Sub btn_Salvar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Salvar.Click
        If IsValid() Then
            insertarRelacion_Productos()
            selectRelacion_Productos(ddl_Filtro_Producto.SelectedValue)
            resetCampos(Configuration.EstadoPantalla.NORMAL)
        End If
    End Sub

    Protected Sub dg_Relacion_Productos_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dg_Relacion_Productos.EditCommand
        Dim id_Relacion_Productos As Integer = dg_Relacion_Productos.DataKeys(e.Item.ItemIndex)
        resetCampos(Configuration.EstadoPantalla.CONSULTAR)
        cargarParaModificar(id_Relacion_Productos)
    End Sub

    Protected Sub btn_AgregarTipoRelacion_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_AgregarTipoRelacion.Click
        Response.Redirect("TipoRelacion.aspx" & Orbelink.Helpers.CommonTasks.RecreateQueryString(codigo_pantalla, True))
    End Sub

    Protected Sub btn_Buscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Buscar.Click
        dg_Relacion_Productos.CurrentPageIndex = 0
        selectRelacion_Productos(ddl_Filtro_Producto.SelectedValue)
    End Sub

    Protected Sub ddl_Tipo_Producto_Base_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddl_Tipo_Producto_Base.SelectedIndexChanged
        selectProductos()
    End Sub

    Protected Sub ddl_Tipo_Producto_Dependiente_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddl_Tipo_Producto_Dependiente.SelectedIndexChanged
        selectProductos()
    End Sub

    Protected Sub dg_Relacion_Productos_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dg_Relacion_Productos.PageIndexChanged
        dg_Relacion_Productos.CurrentPageIndex = e.NewPageIndex
        selectRelacion_Productos(ddl_Filtro_Producto.SelectedValue)
    End Sub

    Protected Sub btn_AgregarTipo_Producto_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_AgregarTipo_Producto.Click
        Response.Redirect("TipoProducto.aspx" & Orbelink.Helpers.CommonTasks.RecreateQueryString(codigo_pantalla, True))
    End Sub

    Protected Sub ddl_Filtro_Tipo_Productos_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddl_Filtro_Tipo_Productos.SelectedIndexChanged
        selectProductos(ddl_Filtro_Tipo_Productos.SelectedValue)
    End Sub
End Class
