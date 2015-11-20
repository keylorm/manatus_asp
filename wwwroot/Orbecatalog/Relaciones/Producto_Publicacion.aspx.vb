Imports Orbelink.DBHandler
Imports Orbelink.Entity.Productos
Imports Orbelink.Entity.Publicaciones

Partial Class Orbecatalog_Relaciones_Producto_Publicacion
    Inherits PageBaseClass

    Const codigo_pantalla As String = "Re-06"
    Const level As Integer = 2

    Dim agregarProducto As Boolean = True

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Request.Params("id_Producto") Is Nothing Then
            agregarProducto = False
        End If

        If Not IsPostBack Then
            securityHandler.VerifyPantalla(codigo_pantalla, level)
            If Not Request.Params("id_Producto") Is Nothing Then
                cargar_AgregarRelacionPublicacion(nombreProducto(Request.Params("id_Producto")))
            Else
                If Not Request.Params("id_Publicacion") Is Nothing Then
                    cargar_AgregarRelacionProducto(nombrePublicacion(Request.Params("id_Publicacion")))
                End If
            End If
            cargarComboTipoRelacion()
        End If
    End Sub

    Protected Sub cargar_AgregarRelacionProducto(ByVal nombre As String)
        lbl_titulo.Text = "Producto para esta Publicacion"
        lbl_nombreRelacionado.Text = nombre
        btn_Agregar.ToolTip = "Ingresar nuevo Producto"
        cargarComboProducto()
        cargarProductoRelacionadas()
    End Sub

    Protected Sub cargar_AgregarRelacionPublicacion(ByVal nombre As String)
        lbl_titulo.Text = "Publicacion para este Producto"
        lbl_nombreRelacionado.Text = nombre
        btn_Agregar.ToolTip = "Ingresar nueva Publicacion"
        cargarComboPublicacion()
        cargarPublicacionRelacionados()
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


    Protected Sub cargarComboPublicacion()
        Dim dataSet As New Data.DataSet
        Dim Publicacion As New Publicacion
        Publicacion.Titulo.ToSelect = True
        Publicacion.Id_Publicacion.ToSelect = True

        Dim consulta As String = queryBuilder.SelectQuery(Publicacion)
        dataSet = connection.executeSelect(consulta)

        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables(0).Rows.Count > 0 Then
                ddl_Relacionados.DataSource = dataSet
                ddl_Relacionados.DataTextField = Publicacion.Titulo.Name
                ddl_Relacionados.DataValueField = Publicacion.Id_Publicacion.Name
                ddl_Relacionados.DataBind()
            End If
        End If
    End Sub


    Protected Sub cargarComboTipoRelacion()
        Dim dataSet As New Data.DataSet
        Dim tipoRelaciones As New TipoRelacion_Producto_Publicacion
        tipoRelaciones.Nombre_Publicacion_Producto.ToSelect = True
        tipoRelaciones.Nombre_Producto_Publicacion.ToSelect = True
        tipoRelaciones.Id_TipoRelacion.ToSelect = True
        Dim consulta As String = queryBuilder.SelectQuery(tipoRelaciones)
        dataSet = connection.executeSelect(consulta)

        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables(0).Rows.Count > 0 Then
                ddl_TipoRelacion.DataSource = dataSet
                If agregarProducto Then
                    ddl_TipoRelacion.DataTextField = tipoRelaciones.Nombre_Publicacion_Producto.Name
                Else
                    ddl_TipoRelacion.DataTextField = tipoRelaciones.Nombre_Producto_Publicacion.Name
                End If
                ddl_TipoRelacion.DataValueField = tipoRelaciones.Id_TipoRelacion.Name
                ddl_TipoRelacion.DataBind()
            End If
        End If
    End Sub

    Protected Sub cargarProductoRelacionadas()
        Dim Producto_Publicacion As New Relacion_Producto_Publicacion
        Dim tipoRelacion As New TipoRelacion_Producto_Publicacion
        Dim Producto As New Producto
        Dim dataSet As New Data.DataSet

        Producto.Nombre.ToSelect = True
        tipoRelacion.Nombre_Publicacion_Producto.ToSelect = True
        Producto.Id_Producto.ToSelect = True
        tipoRelacion.Id_TipoRelacion.ToSelect = True

        queryBuilder.Join.EqualCondition(Producto_Publicacion.id_Producto, Producto.Id_Producto)
        queryBuilder.Join.EqualCondition(Producto_Publicacion.id_TipoRelacion, tipoRelacion.Id_TipoRelacion)
        Producto_Publicacion.id_Publicacion.Where.EqualCondition(Request.Params("id_Publicacion"))

        queryBuilder.From.Add(Producto)
        queryBuilder.From.Add(tipoRelacion)
        queryBuilder.From.Add(Producto_Publicacion)

        Dim consulta As String = queryBuilder.RelationalSelectQuery()
        dataSet = connection.executeSelect(consulta)
        lst_Producto_Publicacion.Items.Clear()
        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables(0).Rows.Count > 0 Then
                Dim resul_Producto As ArrayList = ObjectBuilder.TransformDataTable(dataSet.Tables(0), Producto)
                Dim resul_tipo As ArrayList = ObjectBuilder.TransformDataTable(dataSet.Tables(0), tipoRelacion)

                For counter As Integer = 0 To dataSet.Tables(0).Rows.Count - 1
                    Dim act_Producto As Producto = resul_Producto(counter)
                    Dim act_tipo As TipoRelacion_Producto_Publicacion = resul_tipo(counter)

                    Me.lst_Producto_Publicacion.Items.Add(act_tipo.Nombre_Publicacion_Producto.Value & " " & act_Producto.Nombre.Value)
                    Me.lst_Producto_Publicacion.Items(counter).Value = act_Producto.Id_Producto.Value & "-" & act_tipo.Id_TipoRelacion.Value
                Next
            End If
        End If
    End Sub

    Protected Sub cargarPublicacionRelacionados()
        Dim Publicacion_Producto As New Relacion_Publicacion_Producto
        Dim tipoRelacion As New TipoRelacion_Producto_Publicacion
        Dim Publicacion As New Publicacion
        Dim dataSet As New Data.DataSet

        Publicacion.Titulo.ToSelect = True
        tipoRelacion.Nombre_Producto_Publicacion.ToSelect = True
        Publicacion.Id_Publicacion.ToSelect = True
        tipoRelacion.Id_TipoRelacion.ToSelect = True

        queryBuilder.Join.EqualCondition(Publicacion_Producto.id_Publicacion, Publicacion.Id_Publicacion)
        queryBuilder.Join.EqualCondition(Publicacion_Producto.id_TipoRelacion, tipoRelacion.Id_TipoRelacion)
        Publicacion_Producto.id_Producto.Where.EqualCondition(Request.Params("id_Producto"))

        queryBuilder.From.Add(Publicacion)
        queryBuilder.From.Add(tipoRelacion)
        queryBuilder.From.Add(Publicacion_Producto)

        Dim consulta As String = queryBuilder.RelationalSelectQuery()
        dataSet = connection.executeSelect(consulta)
        lst_Producto_Publicacion.Items.Clear()
        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables(0).Rows.Count > 0 Then
                Dim resul_Publicacion As ArrayList = ObjectBuilder.TransformDataTable(dataSet.Tables(0), Publicacion)
                Dim resul_tipo As ArrayList = ObjectBuilder.TransformDataTable(dataSet.Tables(0), tipoRelacion)

                For counter As Integer = 0 To dataSet.Tables(0).Rows.Count - 1
                    Dim act_Publicacion As Publicacion = resul_Publicacion(counter)
                    Dim act_tipo As TipoRelacion_Producto_Publicacion = resul_tipo(counter)

                    Me.lst_Producto_Publicacion.Items.Add(act_tipo.Nombre_Producto_Publicacion.Value & " " & act_Publicacion.Titulo.Value)
                    Me.lst_Producto_Publicacion.Items(counter).Value = act_Publicacion.Id_Publicacion.Value & "-" & act_tipo.Id_TipoRelacion.Value
                Next
            End If
        End If
    End Sub

    Protected Sub btn_Agregar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Agregar.Click

        Try
            Dim relacion As New Relacion_Producto_Publicacion
            If agregarProducto Then
                relacion.id_Producto.Value = ddl_Relacionados.SelectedValue
                relacion.id_Publicacion.Value = Request.Params("id_Publicacion")
            Else
                relacion.id_Publicacion.Value = ddl_Relacionados.SelectedValue
                relacion.id_Producto.Value = Request.Params("id_Producto")
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

        If lst_Producto_Publicacion.SelectedValue.Length > 0 Then
            Dim corte As Integer = lst_Producto_Publicacion.SelectedValue.IndexOf("-")
            Dim id_Publicacion As Integer = Request.Params("id_Publicacion")
            Dim id_Producto As Integer

            If agregarProducto Then
                id_Publicacion = Request.Params("id_Publicacion")
                id_Producto = lst_Producto_Publicacion.SelectedValue.Substring(0, corte)
            Else
                id_Producto = Request.Params("id_Producto")
                id_Publicacion = lst_Producto_Publicacion.SelectedValue.Substring(0, corte)
            End If
            Dim id_TipoRelacion As Integer = lst_Producto_Publicacion.SelectedValue.Substring(corte + 1, lst_Producto_Publicacion.SelectedValue.Length - corte - 1)


            If deletePublicacion_Producto(id_Publicacion, id_Producto, id_TipoRelacion) Then
                'Mensaje de exito
            Else
                'Mensaje de fracado
            End If
        Else
        End If
        actualizarPanel()
    End Sub

    Protected Function deletePublicacion_Producto(ByVal id_Publicacion As Integer, ByVal id_Producto As Integer, ByVal id_relacion As Integer) As Boolean
        Dim exitoso As Boolean = False
        Try
            Dim rel_Publicacion_Producto As New Relacion_Producto_Publicacion
            rel_Publicacion_Producto.id_Producto.Where.EqualCondition(id_Producto)
            rel_Publicacion_Producto.id_Publicacion.Where.EqualCondition(id_Publicacion)
            rel_Publicacion_Producto.id_TipoRelacion.Where.EqualCondition(id_relacion)

            Dim consulta As String = queryBuilder.DeleteQuery(rel_Publicacion_Producto)
            exitoso = connection.executeDelete(consulta)
        Catch ex As Exception
            exitoso = False
        End Try
        actualizarPanel()
    End Function

    Protected Sub actualizarPanel()
        If agregarProducto Then
            cargarProductoRelacionadas()
        Else
            cargarPublicacionRelacionados()
        End If
    End Sub

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

    Protected Function nombrePublicacion(ByVal id_publicacion As Integer) As String
        Dim publicacion As New Publicacion
        Dim dataSet As New Data.DataSet
        publicacion.Titulo.ToSelect = True
        publicacion.Id_Publicacion.Where.EqualCondition(id_publicacion)
        Dim consulta As String = queryBuilder.SelectQuery(publicacion)
        dataSet = connection.executeSelect(consulta)
        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables(0).Rows.Count > 0 Then
                ObjectBuilder.CreateObject(dataSet.Tables(0), 0, publicacion)
                Return publicacion.Titulo.Value
            End If
        End If
        Return ""
    End Function

End Class
