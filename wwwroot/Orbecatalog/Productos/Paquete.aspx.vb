Imports Orbelink.DBHandler
Imports Orbelink.Entity.Productos
Imports Orbelink.Entity.Entidades
Imports Orbelink.Entity.Currency
Imports Orbelink.Entity.Orbecatalog

Partial Class _Paquete_Paquetes
    Inherits PageBaseClass

    Const codigo_pantalla As String = "PR-17"
    Const level As Integer = 2

    'Pagina
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddScripts(level)
        securityHandler.VerifyPantalla(codigo_pantalla, level)
        If Not IsPostBack Then
            Me.Title = Resources.Orbecatalog_Resources.Orbecatalog & " - " & Resources.Productos_Resources.Paquetes_Pantalla

            ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
            securityHandler.VerifyActions(btn_Salvar, btn_Modificar, btn_Eliminar)
            SetThemeProperties()
            SetControlProperties()

            'Datos Extra
            selectPaquetes()
            selectEntidad(securityHandler.Entidad)
            selectUnidades()
            selectProductos()
            AgregaAtributosBotones()

            ToLoadPage()
        End If
    End Sub

    Protected Sub okbuttonevento(ByVal param As String)
        'alñgodfasd





    End Sub



    Protected Sub ToLoadPage()
        'Obtiene id_Paquete
        Dim PaqueteExist As Boolean = False
        Dim id_Paquete As Integer
        Dim loadSession As Boolean = False
        If Not Request.QueryString("id_Paquete") Is Nothing Then
            id_Paquete = Request.QueryString("id_Paquete")
        End If
        If Not Request.QueryString("back") Is Nothing Then
            Dim id_PaqueteTemp As Integer = LoadSessionWorkingID()
            If id_PaqueteTemp > 0 Then
                id_Paquete = id_PaqueteTemp
            End If
            loadSession = True
        End If

        'Carga detalle
        PaqueteExist = loadPaquete(id_Paquete)

        If loadSession Then
            LoadSessionValues()
        End If

        If PaqueteExist Then
            ClearInfo(Configuration.EstadoPantalla.CONSULTAR, False)
            selectProductos_Paquete(id_Paquete)
        End If
    End Sub

    Protected Sub ClearInfo(ByVal estado As Configuration.EstadoPantalla, ByVal clearInfo As Boolean)
        If clearInfo Then
            tbx_NombrePaquete.Text = ""
            tbx_NombrePaquete.ToolTip = ""
            tbx_Descripcion.Text = ""
            tbx_PrecioUnitario.Text = ""
            Chk_Activo.Checked = False
            vld_Paquete.IsValid = True
        End If

        Select Case estado
            Case Configuration.EstadoPantalla.NORMAL
                btn_Salvar.Visible = True

                btn_Modificar.Visible = False

                btn_Eliminar.Visible = False


                pnl_Grupos.Style("display") = "none"
            Case Configuration.EstadoPantalla.CONSULTAR
                btn_Salvar.Visible = False

                btn_Modificar.Visible = True

                btn_Eliminar.Visible = True


                pnl_Grupos.Style("display") = "block"
        End Select

    End Sub

    Protected Sub SetThemeProperties()




    End Sub

    Protected Sub SetControlProperties()
        Dim Paquete As New Paquete
        tbx_NombrePaquete.Attributes.Add("maxlength", Paquete.Nombre.Length)
        tbx_NombrePaquete.Attributes.Add("onkeypress", "showTextProgress(this)")
        tbx_Descripcion.Attributes.Add("maxlength", Paquete.Descripcion.Length)
        tbx_Descripcion.Attributes.Add("onkeypress", "showTextProgress(this)")
    End Sub

    'Valores_Session
    Protected Sub SaveSessionValues()
        Dim Valores_Session As Valores_Session
        Try
            'WorkingID
            If tbx_NombrePaquete.ToolTip.Length > 0 Then
                Valores_Session = New Valores_Session(securityHandler.Entidad, codigo_pantalla, "WorkingID", tbx_NombrePaquete.ToolTip)
                connection.executeInsert(queryBuilder.InsertQuery(Valores_Session))
            End If

            'Otros
            Valores_Session = New Valores_Session(securityHandler.Entidad, codigo_pantalla, ddl_Unidades.ID, ddl_Unidades.SelectedIndex)
            connection.executeInsert(queryBuilder.InsertQuery(Valores_Session))

            If tbx_NombrePaquete.Text.Length > 0 Then
                Valores_Session = New Valores_Session(securityHandler.Entidad, codigo_pantalla, tbx_NombrePaquete.ID, tbx_NombrePaquete.Text)
                connection.executeInsert(queryBuilder.InsertQuery(Valores_Session))
            End If

            If tbx_PrecioUnitario.Text.Length > 0 Then
                Valores_Session = New Valores_Session(securityHandler.Entidad, codigo_pantalla, tbx_PrecioUnitario.ID, tbx_PrecioUnitario.Text)
                connection.executeInsert(queryBuilder.InsertQuery(Valores_Session))
            End If

            If tbx_Descripcion.Text.Length > 0 Then
                Valores_Session = New Valores_Session(securityHandler.Entidad, codigo_pantalla, tbx_Descripcion.ID, tbx_Descripcion.Text)
                connection.executeInsert(queryBuilder.InsertQuery(Valores_Session))
            End If

        Catch exc As Exception

        End Try
    End Sub

    Protected Function LoadSessionValues() As Boolean
        Dim Valores_Session As New Valores_Session
        Dim dataset As Data.DataSet
        Dim counter As Integer
        Try
            Valores_Session.Fields.SelectAll()
            Valores_Session.Id_Entidad.Where.EqualCondition(securityHandler.Entidad)
            Valores_Session.Pantalla.Where.EqualCondition(codigo_pantalla)
            dataset = connection.executeSelect(queryBuilder.SelectQuery(Valores_Session))

            If dataset.Tables.Count > 0 Then
                If dataset.Tables(0).Rows.Count > 0 Then
                    Dim results_Valores_Session As ArrayList = ObjectBuilder.TransformDataTable(dataset.Tables(0), Valores_Session)
                    For counter = 0 To results_Valores_Session.Count - 1
                        Dim act_Valores_Session As Valores_Session = results_Valores_Session(counter)

                        If act_Valores_Session.ControlID.Value = ddl_Unidades.ID Then
                            ddl_Unidades.SelectedIndex = act_Valores_Session.Valor.Value

                        ElseIf act_Valores_Session.ControlID.Value = tbx_NombrePaquete.ID Then
                            tbx_NombrePaquete.Text = act_Valores_Session.Valor.Value

                        ElseIf act_Valores_Session.ControlID.Value = tbx_PrecioUnitario.ID Then
                            tbx_PrecioUnitario.Text = act_Valores_Session.Valor.Value

                        ElseIf act_Valores_Session.ControlID.Value = tbx_Descripcion.ID Then
                            tbx_Descripcion.Text = act_Valores_Session.Valor.Value

                        Else
                            'Otros varas
                        End If
                    Next
                End If
            End If

            'Borra estos registros
            Valores_Session.Id_Entidad.Where.EqualCondition(securityHandler.Entidad)
            Valores_Session.Pantalla.Where.EqualCondition(codigo_pantalla)
            connection.executeDelete(queryBuilder.DeleteQuery(Valores_Session))
            Return True
        Catch exc As Exception
            MyMaster.mostrarMensaje(exc.Message, True)

            Return False
        End Try
    End Function

    Protected Function LoadSessionWorkingID() As Integer
        Dim Valores_Session As New Valores_Session
        Dim dataset As Data.DataSet
        Dim workingId As Integer = 0

        Valores_Session.Fields.SelectAll()
        Valores_Session.Id_Entidad.Where.EqualCondition(securityHandler.Entidad)
        Valores_Session.Pantalla.Where.EqualCondition(codigo_pantalla)
        Valores_Session.ControlID.Where.EqualCondition("WorkingID")
        dataset = connection.executeSelect(queryBuilder.SelectQuery(Valores_Session))

        If dataset.Tables.Count > 0 Then
            If dataset.Tables(0).Rows.Count > 0 Then
                ObjectBuilder.CreateObject(dataset.Tables(0), 0, Valores_Session)
                workingId = Valores_Session.Valor.Value
            End If
        End If

        Return workingId
    End Function

    'Paquete
    Protected Sub selectPaquetes()
        Dim dataSet As Data.DataSet
        Dim Paquete As New Paquete
        Paquete.Fields.SelectAll()
        queryBuilder.Orderby.Add(Paquete.Nombre)
        dataSet = connection.executeSelect(queryBuilder.SelectQuery(Paquete))
        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables(0).Rows.Count > 0 Then
                dg_Paquetes.DataSource = dataSet
                dg_Paquetes.DataKeyField = Paquete.Id_Moneda.Name
                dg_Paquetes.DataBind()

                'Llena el grid
                Dim offset As Integer = dg_Paquetes.CurrentPageIndex * dg_Paquetes.PageSize
                Dim counter As Integer
                Dim resultados As ArrayList = ObjectBuilder.TransformDataTable(dataSet.Tables(0), Paquete)
                For counter = 0 To dg_Paquetes.Items.Count - 1
                    Dim act_Paquete As Paquete = resultados(offset + counter)
                    Dim lbl_Nombre As Label = dg_Paquetes.Items(counter).FindControl("lbl_Nombre")
                    lbl_Nombre.Text = act_Paquete.Nombre.Value

                    'Javascript
                    dg_Paquetes.Items(counter).Attributes.Add("onmouseover", "setRowFocus(this, " & counter & ", false)")
                    dg_Paquetes.Items(counter).Attributes.Add("onmouseout", "unsetRowFocus(this, " & counter & ", false)")
                Next
                lbl_ResultadoElementos.Text = contadorElementos(offset, dg_Paquetes.PageSize, dataSet.Tables(0).Rows.Count)
                dg_Paquetes.Visible = True
            Else
                lbl_ResultadoElementos.Text = contadorElementos(0, 0, 0)
                dg_Paquetes.Visible = False
            End If
        End If
    End Sub

    Protected Function loadPaquete(ByVal id_Paquete As Integer) As Boolean
        Dim dataSet As Data.DataSet
        Dim Paquete As New Paquete

        Paquete.Fields.SelectAll()
        Paquete.Id_Paquete.Where.EqualCondition(id_Paquete)

        queryBuilder.From.Add(Paquete)
        dataSet = connection.executeSelect(queryBuilder.RelationalSelectQuery)

        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables(0).Rows.Count > 0 Then
                ObjectBuilder.CreateObject(dataSet.Tables(0), 0, Paquete)

                'Carga la informacion
                tbx_NombrePaquete.Text = Paquete.Nombre.Value
                tbx_NombrePaquete.ToolTip = Paquete.Id_Paquete.Value
                tbx_Descripcion.Text = Paquete.Descripcion.Value
                tbx_PrecioUnitario.Text = Paquete.Precio.Value
                ddl_Unidades.SelectedValue = Paquete.Id_Moneda.Value

                If Paquete.Activo.Value = "1" Then
                    Chk_Activo.Checked = True
                End If

                selectEntidad(Paquete.id_Entidad.Value)
                Return True
            End If
        End If
        Return False
    End Function

    Protected Function insertPaquete() As Boolean
        Dim Paquete As Paquete
        Dim activo As Integer
        If Chk_Activo.Checked Then
            activo = 1
        End If
        Dim id_entidad As Integer
        If lbl_Entidad.Visible Then
            id_entidad = lbl_Entidad.ToolTip
        Else
            id_entidad = ddl_Entidad.SelectedValue
        End If

        Try

            Paquete = New Paquete(tbx_NombrePaquete.Text, tbx_Descripcion.Text, tbx_PrecioUnitario.Text, Date.UtcNow, _
                activo, id_entidad, ddl_Unidades.SelectedValue)
            connection.executeInsert(queryBuilder.InsertQuery(Paquete))
            Return True
        Catch exc As Exception
            MyMaster.mostrarMensaje(exc.Message, True)

            Return False
        End Try
    End Function

    Protected Function updatePaquete(ByVal id_Paquete As Integer) As Boolean
        Dim activo As Integer
        If Chk_Activo.Checked Then
            activo = 1
        End If
        Dim id_entidad As Integer
        If lbl_Entidad.Visible Then
            id_entidad = lbl_Entidad.ToolTip
        Else
            id_entidad = ddl_Entidad.SelectedValue
        End If

        Try
            Dim Paquete As New Paquete(tbx_NombrePaquete.Text, tbx_Descripcion.Text, tbx_PrecioUnitario.Text, Date.UtcNow, _
                activo, id_entidad, ddl_Unidades.SelectedValue)
            Paquete.Fecha.ToUpdate = False
            Paquete.Id_Paquete.Where.EqualCondition(id_Paquete)
            If connection.executeUpdate(queryBuilder.UpdateQuery(Paquete)) >= 1 Then
                Return True
            End If
        Catch exc As Exception
            MyMaster.mostrarMensaje(exc.Message, True)

            Return False
        End Try
    End Function

    Protected Function deletePaquete(ByVal id_Paquete As Integer) As Boolean
        Dim Paquete As New Paquete
        Try
            Paquete.Id_Paquete.Where.EqualCondition(id_Paquete)
            connection.executeDelete(queryBuilder.DeleteQuery(Paquete))
            Return True
        Catch exc As Exception
            MyMaster.mostrarMensaje(exc.Message, True)

            Return False
        End Try
    End Function

    'Productos
    Protected Sub selectProductos()
        Dim ds As New Data.DataSet
        Dim productos As New Producto
        productos.Nombre.ToSelect = True
        productos.Id_Producto.ToSelect = True

        queryBuilder.From.Add(productos)
        queryBuilder.Orderby.Add(productos.Nombre)
        ds = connection.executeSelect(queryBuilder.RelationalSelectQuery)
        If ds.Tables.Count > 0 Then
            If ds.Tables(0).Rows.Count > 0 Then
                ddl_Productos.DataSource = ds
                ddl_Productos.DataValueField = productos.Id_Producto.Name
                ddl_Productos.DataTextField = productos.Nombre.Name
                ddl_Productos.DataBind()
            End If
        End If
    End Sub

    'Moneda      
    Protected Sub selectUnidades()
        Dim dataset As Data.DataSet
        Dim Moneda As New Moneda
        Moneda.Fields.SelectAll()
        queryBuilder.Orderby.Add(Moneda.Nombre)
        dataset = connection.executeSelect(queryBuilder.SelectQuery(Moneda))
        If dataset.Tables.Count > 0 Then
            If dataset.Tables(0).Rows.Count > 0 Then
                ddl_Unidades.DataSource = dataset
                ddl_Unidades.DataTextField = Moneda.Nombre.Name
                ddl_Unidades.DataValueField = Moneda.Id_Moneda.Name
                ddl_Unidades.DataBind()
            End If
        End If
    End Sub

    'JavaScript
    Private Sub AgregaAtributosBotones()
        btn_Eliminar.Attributes.Add("onclick", "return confirm('Está seguro de que desea eliminar este Paquete?\n Esto también eliminará las imagenes asociadas a el!');")
        btn_Modificar.Attributes.Add("onclick", "return confirm('Está seguro de que desea modificar la información de este Paquete?');")
        btn_Cancelar.Attributes.Add("onclick", "return confirm('Esto limpiará todos los campos. Continuar?');")
    End Sub

    'Entidades
    Protected Sub selectEntidad(ByVal id_entidad As Integer)
        Dim dataSet As Data.DataSet
        Dim entidad As New Entidad

        If id_entidad <> 0 Then
            entidad.Id_entidad.ToSelect = True
            entidad.NombreDisplay.ToSelect = True
            entidad.Apellido.ToSelect = True
            entidad.Id_entidad.Where.EqualCondition(id_entidad)

            dataSet = connection.executeSelect(queryBuilder.SelectQuery(entidad))

            If dataSet.Tables.Count > 0 Then
                ObjectBuilder.CreateObject(dataSet.Tables(0), 0, entidad)
                lbl_Entidad.Visible = True
                lbl_Entidad.Text = entidad.NombreDisplay.Value
                lbl_Entidad.ToolTip = id_entidad
            End If

            ddl_Entidad.Visible = False
            lbl_Entidad.Visible = True
        Else
            Dim entidadHandler As New Orbelink.Control.Entidades.EntidadHandler(Configuration.Config_DefaultConnectionString)
            entidadHandler.selectEntidades_DDL(ddl_Entidad, securityHandler.Entidad, securityHandler.ClearanceLevel)
            ddl_Entidad.Visible = True
            lbl_Entidad.Visible = False
        End If
    End Sub

    'Productos_Paquete
    Protected Sub selectProductos_Paquete(ByVal Id_Paquete As Integer)
        Dim dataset As New Data.DataSet
        Dim Productos_Paquete As New Productos_Paquete
        Dim Producto As New Producto

        Productos_Paquete.Id_Paquete.Where.EqualCondition(Id_Paquete)
        Productos_Paquete.Cantidad.ToSelect = True
        Productos_Paquete.Id_Producto.ToSelect = True

        Producto.Nombre.ToSelect = True
        queryBuilder.Join.EqualCondition(Producto.Id_Producto, Productos_Paquete.Id_Producto)

        queryBuilder.From.Add(Productos_Paquete)
        queryBuilder.From.Add(Producto)
        queryBuilder.Orderby.Add(Producto.Nombre)
        dataset = connection.executeSelect(queryBuilder.RelationalSelectQuery())

        lst_Grupos.Items.Clear()
        If dataset.Tables(0).Rows.Count > 0 Then
            Dim results_Productos_Paquete As ArrayList = ObjectBuilder.TransformDataTable(dataset.Tables(0), Productos_Paquete)
            Dim results_Producto As ArrayList = ObjectBuilder.TransformDataTable(dataset.Tables(0), Producto)
            For counter As Integer = 0 To dataset.Tables(0).Rows.Count - 1
                Dim act_Productos_Paquete As Productos_Paquete = results_Productos_Paquete(counter)
                Dim act_Producto As Producto = results_Producto(counter)

                lst_Grupos.Items.Add(act_Producto.Nombre.Value & ", con " & act_Productos_Paquete.Cantidad.Value)
                lst_Grupos.Items(counter).Value = act_Productos_Paquete.Id_Producto.Value
            Next
        End If
    End Sub

    Protected Function insertProductos_Paquete(ByVal id_Paquete As Integer, ByVal Id_Producto As Integer, ByVal Cantidad As Integer) As Boolean
        Dim Productos_Paquete As New Productos_Paquete
        Try
            Productos_Paquete.Id_Paquete.Value = id_Paquete
            Productos_Paquete.Id_Producto.Value = Id_Producto
            Productos_Paquete.Cantidad.Value = Cantidad
            connection.executeInsert(queryBuilder.InsertQuery(Productos_Paquete))
            Return True
        Catch exc As Exception
            MyMaster.mostrarMensaje(exc.Message, True)

            Return False
        End Try
    End Function

    Protected Function deleteProductos_Paquete(ByVal id_Paquete As Integer, ByVal Id_Producto As Integer) As Boolean
        Dim Productos_Paquete As New Productos_Paquete
        Try
            Productos_Paquete.Id_Producto.Where.EqualCondition(Id_Producto)
            Productos_Paquete.Id_Paquete.Where.EqualCondition(id_Paquete)
            connection.executeDelete(queryBuilder.DeleteQuery(Productos_Paquete))
            Return True
        Catch exc As Exception
            MyMaster.mostrarMensaje(exc.Message, True)

            Return False
        End Try
    End Function

    'Eventos
    Private Sub btn_Salvar_Click(ByVal Sender As Object, ByVal E As System.EventArgs) Handles btn_Salvar.Click

        If IsValid Then
            If insertPaquete() Then
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Salvar, "Paquete", False)
                Dim Paquete As New Paquete
                Dim id_Insertado As Integer = connection.lastKey(Paquete.TableName, Paquete.Id_Paquete.Name)
                tbx_NombrePaquete.ToolTip = id_Insertado

                ClearInfo(Configuration.EstadoPantalla.CONSULTAR, False)
                selectPaquetes()

                upd_Botones.Update()
                upd_ProductosPaquete.Update()
                upd_DGPaquetes.Update()
            Else
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Salvar, "Paquete", True)
            End If
        Else
            MyMaster.MostrarMensaje("Algun campo necesario está vacio o erroneo.", False)
        End If
    End Sub

    Private Sub btn_Modificar_Click(ByVal Sender As Object, ByVal E As System.EventArgs) Handles btn_Modificar.Click
        If IsValid Then
            Dim id_Paquete As Integer = tbx_NombrePaquete.ToolTip
            If updatePaquete(id_Paquete) Then
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Modificar, "Paquete", False)

                selectPaquetes()
                upd_DGPaquetes.Update()
            Else
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Modificar, "Paquete", True)
            End If
        Else
            MyMaster.MostrarMensaje("Algun campo necesario está vacio o erroneo.", False)
        End If
    End Sub

    Private Sub btn_Cancelar_Click(ByVal Sender As Object, ByVal E As System.EventArgs) Handles btn_Cancelar.Click
        ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
        upd_DatosBasicos.Update()
        upd_Descripcion.Update()
        upd_ProductosPaquete.Update()
        upd_Botones.Update()
    End Sub

    Private Sub btn_Eliminar_Click(ByVal Sender As Object, ByVal E As System.EventArgs) Handles btn_Eliminar.Click
        If deletePaquete(tbx_NombrePaquete.ToolTip) Then
            MyMaster.MostrarMensaje("Paquete eliminiado exitosamente.", False)
            ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
            selectPaquetes()

            upd_DGPaquetes.Update()
            upd_DatosBasicos.Update()
            upd_Descripcion.Update()
            upd_ProductosPaquete.Update()
            upd_Botones.Update()
        Else
            MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Eliminar, "Paquete", True)
        End If
    End Sub

    Protected Sub btn_AgregarProductoAPaquete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_AgregarProductoAPaquete.Click

        Dim id_Paquete As Integer = tbx_NombrePaquete.ToolTip
        If insertProductos_Paquete(id_Paquete, ddl_Productos.SelectedValue, tbx_CantidadProducto.Text) Then
            selectProductos_Paquete(id_Paquete)
            MyMaster.MostrarMensaje("Paquete agregada al Grupo exitosamente.", False)
        Else
            MyMaster.MostrarMensaje("Error al agregar Paquete al Grupo.", False)
        End If
    End Sub

    Protected Sub btn_QuitarDeGrupo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_QuitarProductoDePaquete.Click

        Dim id_Paquete As Integer = tbx_NombrePaquete.ToolTip
        If lst_Grupos.SelectedValue.Length > 0 Then
            Dim id_Producto As Integer = lst_Grupos.SelectedValue
            If deleteProductos_Paquete(id_Paquete, id_Producto) Then
                selectProductos_Paquete(id_Paquete)
                MyMaster.MostrarMensaje("Producto eliminado del paquete exitosamente.", False)
                upd_ProductosPaquete.Update()
            Else
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Eliminar, "producto del paquete", True)
            End If
        Else
            MyMaster.MostrarMensaje("Debe seleccionar un Producto.", False)
        End If
    End Sub

    Protected Sub dg_Paquetes_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dg_Paquetes.EditCommand
        ClearInfo(Configuration.EstadoPantalla.CONSULTAR, True)
        Me.EditCommandDataGrid(dg_Paquetes, e.Item.ItemIndex)
        loadPaquete(dg_Paquetes.DataKeys(e.Item.ItemIndex))
        selectProductos_Paquete(dg_Paquetes.DataKeys(e.Item.ItemIndex))
        upd_DGPaquetes.Update()
        upd_DatosBasicos.Update()
        upd_Descripcion.Update()
        upd_ProductosPaquete.Update()
        upd_Botones.Update()
    End Sub

    Protected Sub dg_Paquetes_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dg_Paquetes.PageIndexChanged
        dg_Paquetes.CurrentPageIndex = e.NewPageIndex
        selectPaquetes()
        Me.PageIndexChange(dg_Paquetes)
        upd_DGPaquetes.Update()
    End Sub

    'Agregar
    Protected Sub btn_AgregarUnidad_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_agregarunidad.Click
        SaveSessionValues()
        Response.Redirect("Moneda.aspx" & Orbelink.Helpers.CommonTasks.RecreateQueryString(codigo_pantalla, True))
    End Sub

    Protected Sub btn_AgregarProductos_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_AgregarProductos.Click
        SaveSessionValues()
        Response.Redirect("Productos.aspx" & Orbelink.Helpers.CommonTasks.RecreateQueryString(codigo_pantalla, True))
    End Sub
End Class
