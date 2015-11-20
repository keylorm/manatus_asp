Imports Orbelink.DBHandler
Imports Orbelink.Entity.Entidades
Imports Orbelink.Entity.Productos

Partial Class Orbecatalog_Relaciones_Entidad_Producto
    Inherits PageBaseClass

    Const codigo_pantalla As String = "Re-02"
    Const level As Integer = 2

    Dim agregarEntidad As Boolean = True

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Request.Params("id_Entidad") Is Nothing Then
            agregarEntidad = False
        End If

        If Not IsPostBack Then
            securityHandler.VerifyPantalla(codigo_pantalla, level)
            If Not Request.Params("id_Entidad") Is Nothing Then
                cargar_AgregarRelacionProducto(nombreEntidad(Request.Params("id_Entidad")))
            Else
                If Not Request.Params("id_Producto") Is Nothing Then
                    cargar_AgregarRelacionEntidad(nombreProducto(Request.Params("id_Producto")))
                End If
            End If
            cargarComboTipoRelacion()
        End If
    End Sub

    Protected Sub cargar_AgregarRelacionEntidad(ByVal nombre As String)
        lbl_titulo.Text = "Entidad para este Producto"
        lbl_nombreRelacionado.Text = nombre
        btn_Agregar.ToolTip = "Ingresar nueva Entidad"
        cargarComboEntidad()
        cargarEntidadRelacionadas()
    End Sub

    Protected Sub cargar_AgregarRelacionProducto(ByVal nombre As String)
        lbl_titulo.Text = "Producto para esta Entidad"
        lbl_nombreRelacionado.Text = nombre
        btn_Agregar.ToolTip = "Ingresar nuevo Producto"
        cargarComboProducto()
        cargarProductoRelacionados()
    End Sub

    Protected Sub cargarComboEntidad()
        Dim dataSet As New Data.DataSet
        Dim Entidad As New Entidad
        Entidad.NombreDisplay.ToSelect = True
        Entidad.Id_entidad.ToSelect = True
        Dim consulta As String = queryBuilder.SelectQuery(Entidad)
        dataSet = connection.executeSelect(consulta)

        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables(0).Rows.Count > 0 Then
                ddl_Relacionados.DataSource = dataSet
                ddl_Relacionados.DataTextField = Entidad.NombreDisplay.Name
                ddl_Relacionados.DataValueField = Entidad.Id_entidad.Name
                ddl_Relacionados.DataBind()
            End If
        End If
    End Sub

    Protected Sub cargarComboProducto()
        Dim dataSet As New Data.DataSet
        Dim Producto As New Producto
        Producto.Nombre.ToSelect = True
        Producto.Id_Producto.ToSelect = True
        Producto.Activo.Where.EqualCondition(1)
        Dim consulta As String = queryBuilder.SelectQuery(Producto)
        dataSet = connection.executeSelect(consulta)

        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables(0).Rows.Count > 0 Then
                ddl_Relacionados.DataSource = dataSet
                ddl_Relacionados.DataTextField = Producto.Nombre.Name
                ddl_Relacionados.DataValueField = Producto.Id_Producto.Name
                ddl_Relacionados.DataBind()
            End If
        End If
    End Sub

    Protected Sub cargarComboTipoRelacion()
        Dim dataSet As New Data.DataSet
        Dim tipoRelaciones As New TipoRelacion_Entidad_Producto
        tipoRelaciones.Nombre_Producto_Entidad.ToSelect = True
        tipoRelaciones.Nombre_Entidad_Producto.ToSelect = True
        tipoRelaciones.Id_TipoRelacion.ToSelect = True
        Dim consulta As String = queryBuilder.SelectQuery(tipoRelaciones)
        dataSet = connection.executeSelect(consulta)

        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables(0).Rows.Count > 0 Then
                ddl_TipoRelacion.DataSource = dataSet
                If agregarEntidad Then
                    ddl_TipoRelacion.DataTextField = tipoRelaciones.Nombre_Producto_Entidad.Name
                Else
                    ddl_TipoRelacion.DataTextField = tipoRelaciones.Nombre_Entidad_Producto.Name
                End If
                ddl_TipoRelacion.DataValueField = tipoRelaciones.Id_TipoRelacion.Name
                ddl_TipoRelacion.DataBind()
            End If
        End If
    End Sub

    Protected Sub cargarEntidadRelacionadas()
        Dim Entidad_Producto As New Relacion_Entidad_Producto
        Dim tipoRelacion As New TipoRelacion_Entidad_Producto
        Dim Entidad As New Entidad
        Dim dataSet As New Data.DataSet

        Entidad.NombreDisplay.ToSelect = True
        tipoRelacion.Nombre_Producto_Entidad.ToSelect = True
        Entidad.Id_entidad.ToSelect = True
        tipoRelacion.Id_TipoRelacion.ToSelect = True

        queryBuilder.Join.EqualCondition(Entidad_Producto.id_entidad, Entidad.Id_entidad)
        queryBuilder.Join.EqualCondition(Entidad_Producto.id_TipoRelacion, tipoRelacion.Id_TipoRelacion)
        Entidad_Producto.id_producto.Where.EqualCondition(Request.Params("id_Producto"))

        queryBuilder.From.Add(Entidad)
        queryBuilder.From.Add(tipoRelacion)
        queryBuilder.From.Add(Entidad_Producto)

        Dim consulta As String = queryBuilder.RelationalSelectQuery()
        dataSet = connection.executeSelect(consulta)
        lst_Entidad_Producto.Items.Clear()
        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables(0).Rows.Count > 0 Then
                Dim resul_Entidad As ArrayList = ObjectBuilder.TransformDataTable(dataSet.Tables(0), Entidad)
                Dim resul_tipo As ArrayList = ObjectBuilder.TransformDataTable(dataSet.Tables(0), tipoRelacion)

                For counter As Integer = 0 To dataSet.Tables(0).Rows.Count - 1
                    Dim act_Entidad As Entidad = resul_Entidad(counter)
                    Dim act_tipo As TipoRelacion_Entidad_Producto = resul_tipo(counter)

                    Me.lst_Entidad_Producto.Items.Add(act_tipo.Nombre_Producto_Entidad.Value & " " & act_Entidad.NombreDisplay.Value)
                    Me.lst_Entidad_Producto.Items(counter).Value = act_Entidad.Id_entidad.Value & "-" & act_tipo.Id_TipoRelacion.Value
                Next
            End If
        End If
    End Sub

    Protected Sub cargarProductoRelacionados()
        Dim Producto_Entidad As New Relacion_Producto_Entidad
        Dim tipoRelacion As New TipoRelacion_Entidad_Producto
        Dim Producto As New Producto
        Dim dataSet As New Data.DataSet

        Producto.Nombre.ToSelect = True
        tipoRelacion.Nombre_Entidad_Producto.ToSelect = True
        Producto.Id_Producto.ToSelect = True
        tipoRelacion.Id_TipoRelacion.ToSelect = True

        queryBuilder.Join.EqualCondition(Producto_Entidad.id_producto, Producto.Id_Producto)
        queryBuilder.Join.EqualCondition(Producto_Entidad.id_TipoRelacion, tipoRelacion.Id_TipoRelacion)
        Producto_Entidad.id_entidad.Where.EqualCondition(Request.Params("id_Entidad"))

        queryBuilder.From.Add(Producto)
        queryBuilder.From.Add(tipoRelacion)
        queryBuilder.From.Add(Producto_Entidad)

        Dim consulta As String = queryBuilder.RelationalSelectQuery()
        dataSet = connection.executeSelect(consulta)
        lst_Entidad_Producto.Items.Clear()
        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables(0).Rows.Count > 0 Then
                Dim resul_Producto As ArrayList = ObjectBuilder.TransformDataTable(dataSet.Tables(0), Producto)
                Dim resul_tipo As ArrayList = ObjectBuilder.TransformDataTable(dataSet.Tables(0), tipoRelacion)

                For counter As Integer = 0 To dataSet.Tables(0).Rows.Count - 1
                    Dim act_Producto As Producto = resul_Producto(counter)
                    Dim act_tipo As TipoRelacion_Entidad_Producto = resul_tipo(counter)

                    Me.lst_Entidad_Producto.Items.Add(act_tipo.Nombre_Entidad_Producto.Value & " " & act_Producto.Nombre.Value)
                    Me.lst_Entidad_Producto.Items(counter).Value = act_Producto.Id_Producto.Value & "-" & act_tipo.Id_TipoRelacion.Value
                Next
            End If
        End If
    End Sub

    Protected Sub btn_Agregar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Agregar.Click

        Try
            Dim relacion As New Relacion_Entidad_Producto
            If agregarEntidad Then
                relacion.id_entidad.Value = ddl_Relacionados.SelectedValue
                relacion.id_producto.Value = Request.Params("id_Producto")
            Else
                relacion.id_producto.Value = ddl_Relacionados.SelectedValue
                relacion.id_entidad.Value = Request.Params("id_Entidad")
            End If

            relacion.id_TipoRelacion.Value = ddl_TipoRelacion.SelectedValue
            Dim consulta As String = queryBuilder.InsertQuery(relacion)
            connection.executeInsert(consulta)
        Catch ex As Exception
            MyMaster.MostrarMensaje("Error al agregar", True)
        End Try
        actualizarPanel()

    End Sub

    Protected Sub btn_Eliminar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Eliminar.Click

        If lst_Entidad_Producto.SelectedValue.Length > 0 Then
            Dim corte As Integer = lst_Entidad_Producto.SelectedValue.IndexOf("-")
            Dim id_Producto As Integer = Request.Params("id_Producto")
            Dim id_Entidad As Integer

            If agregarEntidad Then
                id_Producto = Request.Params("id_Producto")
                id_Entidad = lst_Entidad_Producto.SelectedValue.Substring(0, corte)
            Else
                id_Entidad = Request.Params("id_Entidad")
                id_Producto = lst_Entidad_Producto.SelectedValue.Substring(0, corte)
            End If
            Dim id_TipoRelacion As Integer = lst_Entidad_Producto.SelectedValue.Substring(corte + 1, lst_Entidad_Producto.SelectedValue.Length - corte - 1)


            If deleteProducto_Entidad(id_Producto, id_Entidad, id_TipoRelacion) Then
                'Mensaje de exito
            Else
                'Mensaje de fracado
            End If
        Else
        End If
        actualizarPanel()
    End Sub

    Protected Function deleteProducto_Entidad(ByVal id_Producto As Integer, ByVal id_Entidad As Integer, ByVal id_relacion As Integer) As Boolean
        Dim exitoso As Boolean = False
        Try
            Dim rel_Producto_Entidad As New Relacion_Entidad_Producto
            rel_Producto_Entidad.id_entidad.Where.EqualCondition(id_Entidad)
            rel_Producto_Entidad.id_producto.Where.EqualCondition(id_Producto)
            rel_Producto_Entidad.id_TipoRelacion.Where.EqualCondition(id_relacion)

            Dim consulta As String = queryBuilder.DeleteQuery(rel_Producto_Entidad)
            exitoso = connection.executeDelete(consulta)
        Catch ex As Exception
            exitoso = False
        End Try
        actualizarPanel()
    End Function

    Protected Function nombreEntidad(ByVal id_entidad As Integer) As String
        Dim entidad As New Entidad
        Dim dataSet As New Data.DataSet
        entidad.NombreDisplay.ToSelect = True
        entidad.Id_entidad.Where.EqualCondition(id_entidad)
        Dim consulta As String = queryBuilder.SelectQuery(entidad)
        dataSet = connection.executeSelect(consulta)
        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables(0).Rows.Count > 0 Then
                ObjectBuilder.CreateObject(dataSet.Tables(0), 0, entidad)
                Return entidad.NombreDisplay.Value
            End If
        End If
        Return ""
    End Function

    Protected Function nombreProducto(ByVal id_producto As Integer) As String
        Dim producto As New Producto
        Dim dataSet As New Data.DataSet
        Producto.Nombre.ToSelect = True
        Producto.Id_Producto.Where.EqualCondition(id_producto)
        Dim consulta As String = queryBuilder.SelectQuery(Producto)
        dataSet = connection.executeSelect(consulta)
        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables(0).Rows.Count > 0 Then
                ObjectBuilder.CreateObject(dataSet.Tables(0), 0, Producto)
                Return Producto.Nombre.Value
            End If
        End If
        Return ""
    End Function

    Protected Sub actualizarPanel()
        If agregarEntidad Then
            cargarEntidadRelacionadas()
        Else
            cargarProductoRelacionados()
        End If
    End Sub
End Class
