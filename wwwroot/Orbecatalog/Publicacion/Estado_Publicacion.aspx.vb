Imports Orbelink.DBHandler
Imports Orbelink.Entity.Publicaciones

Partial Class _Estado_Publicacion
    Inherits PageBaseClass

    Const codigo_pantalla As String = "PU-05"
    Const level As Integer = 2

    'Pagina
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddScripts(level)
        securityHandler.VerifyPantalla(codigo_pantalla, level)
        If Not IsPostBack Then
            dg_Estado_Publicacion.Attributes.Add("SelectedIndex", -1)
            dg_Estado_Publicacion.Attributes.Add("SelectedPage", -1)
            ClearInfo(Configuration.EstadoPantalla.NORMAL, False)
            securityHandler.VerifyActions(btn_Salvar, btn_Modificar, btn_Eliminar)

            selectEstado_Publicacion()
        End If
    End Sub

    Protected Sub ClearInfo(ByVal estado As Configuration.EstadoPantalla, ByVal clearInfo As Boolean)
        If clearInfo Then
            vld_tbx_NombreEstadoProducto.IsValid = True
            tbx_NombreEstadoProducto.Text = ""
            tbx_NombreEstadoProducto.ToolTip = ""
            Me.ClearDataGrid(dg_Estado_Publicacion, clearInfo)
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

    'Estado_Publicacion
    Protected Sub selectEstado_Publicacion()
        Dim dataSet As Data.DataSet
        Dim Estado_Publicacion As New Estado_Publicacion()
        Estado_Publicacion.Fields.SelectAll()
        queryBuilder.Orderby.Add(Estado_Publicacion.Nombre)
        dataSet = connection.executeSelect(queryBuilder.SelectQuery(Estado_Publicacion))
        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables(0).Rows.Count > 0 Then
                dg_Estado_Publicacion.DataSource = dataSet
                dg_Estado_Publicacion.DataKeyField = Estado_Publicacion.Id_Estado_Publicacion.Name
                dg_Estado_Publicacion.DataBind()

                'Llena el grid
                Dim offset As Integer = dg_Estado_Publicacion.CurrentPageIndex * dg_Estado_Publicacion.PageSize
                Dim counter As Integer
                Dim resul_Estado_Publicacion As ArrayList = ObjectBuilder.TransformDataTable(dataSet.Tables(0), Estado_Publicacion)
                For counter = 0 To dg_Estado_Publicacion.Items.Count - 1           'Recorre los atributos relacionados
                    Dim act_Estado_Publicacion As Estado_Publicacion = resul_Estado_Publicacion(counter)
                    Dim lbl_Nombre As Label = dg_Estado_Publicacion.Items(counter).FindControl("lbl_Nombre")
                    lbl_Nombre.Text = act_Estado_Publicacion.Nombre.Value

                    'Javascript
                    dg_Estado_Publicacion.Items(counter).Attributes.Add("onmouseover", "setRowFocus(this, " & counter & ", false)")
                    dg_Estado_Publicacion.Items(counter).Attributes.Add("onmouseout", "unsetRowFocus(this, " & counter & ", false)")
                Next
                lbl_ResultadoElementos.Text = contadorElementos(offset, dg_Estado_Publicacion.PageSize, dataSet.Tables(0).Rows.Count)
                dg_Estado_Publicacion.Visible = True
            Else
                lbl_ResultadoElementos.Text = contadorElementos(0, 0, 0)
                dg_Estado_Publicacion.Visible = False
            End If
        End If
    End Sub

    Protected Sub loadEstado_Publicacion(ByVal id_Estado_Publicacion As Integer)
        Dim dataSet As Data.DataSet
        Dim Estado_Publicacion As New Estado_Publicacion
        Estado_Publicacion.Fields.SelectAll()
        Estado_Publicacion.Id_Estado_Publicacion.Where.EqualCondition(id_Estado_Publicacion)
        dataSet = connection.executeSelect(queryBuilder.SelectQuery(Estado_Publicacion))
        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables(0).Rows.Count > 0 Then
                ObjectBuilder.CreateObject(dataSet.Tables(0), 0, Estado_Publicacion)
                tbx_NombreEstadoProducto.Text = Estado_Publicacion.Nombre.Value
                tbx_NombreEstadoProducto.ToolTip = Estado_Publicacion.Id_Estado_Publicacion.Value
            End If
        End If
    End Sub

    Protected Function updateEstado_Publicacion(ByVal id_Estado_Publicacion As Integer) As Boolean
        Dim Estado_Publicacion As Estado_Publicacion
        Try
            Estado_Publicacion = New Estado_Publicacion(tbx_NombreEstadoProducto.Text)
            Estado_Publicacion.Id_Estado_Publicacion.Where.EqualCondition(id_Estado_Publicacion)
            connection.executeUpdate(queryBuilder.UpdateQuery(Estado_Publicacion))
            Return True
        Catch exc As Exception
            MyMaster.mostrarMensaje(exc.Message, True)

            Return False
        End Try
    End Function

    Protected Function insertEstado_Publicacion(ByVal theTextBox As TextBox) As Boolean
        Dim Estado_Publicacion As Estado_Publicacion
        Try
            Estado_Publicacion = New Estado_Publicacion(theTextBox.Text)
            connection.executeInsert(queryBuilder.InsertQuery(Estado_Publicacion))
            Return True
        Catch exc As Exception
            MyMaster.mostrarMensaje(exc.Message, True)

            Return False
        End Try
    End Function

    Protected Function deleteEstado_Publicacion(ByVal id_Estado_Publicacion As Integer) As Boolean
        Dim Estado_Publicacion As New Estado_Publicacion
        Try
            Estado_Publicacion.Id_Estado_Publicacion.Where.EqualCondition(id_Estado_Publicacion)
            connection.executeDelete(queryBuilder.DeleteQuery(Estado_Publicacion))
        Catch exc As Exception
            MyMaster.mostrarMensaje(exc.Message, True)

        End Try
    End Function

    'Eventos
    Protected Sub btn_Salvar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Salvar.Click

        If IsValid() Then
            If insertEstado_Publicacion(tbx_NombreEstadoProducto) Then
                ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
                selectEstado_Publicacion()
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
            If updateEstado_Publicacion(tbx_NombreEstadoProducto.ToolTip) Then
                'ClearInfo(Configuration.EstadoPantalla.NORMAL)
                selectEstado_Publicacion()
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

        If deleteEstado_Publicacion(tbx_NombreEstadoProducto.ToolTip) Then
            ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
            selectEstado_Publicacion()
            MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Eliminar, "Estado de producto", False)
        Else
            MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Eliminar, "Estado de producto. <br /> Ningun producto debe estar con este estado seleccionado", True)
        End If
        upd_Contenido.Update()
        upd_Busqueda.Update()
    End Sub

    Protected Sub dg_Estado_Publicacion_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dg_Estado_Publicacion.EditCommand
        ClearInfo(Configuration.EstadoPantalla.CONSULTAR, True)
        Me.EditCommandDataGrid(dg_Estado_Publicacion, e.Item.ItemIndex)
        loadEstado_Publicacion(dg_Estado_Publicacion.DataKeys(e.Item.ItemIndex))
        upd_Contenido.Update()
        upd_Busqueda.Update()
    End Sub

    Protected Sub dg_Estado_Publicacion_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dg_Estado_Publicacion.PageIndexChanged
        dg_Estado_Publicacion.CurrentPageIndex = e.NewPageIndex
        selectEstado_Publicacion()
        Me.PageIndexChange(dg_Estado_Publicacion)
        upd_Busqueda.Update()
    End Sub
End Class
