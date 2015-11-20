Imports Orbelink.DBHandler
Imports Orbelink.Entity.Productos

Partial Class Orbecatalog_Productos_TipoRelacion_Grupos
    Inherits PageBaseClass

    Const codigo_pantalla As String = "PR-18"
    Const level As Integer = 2

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddScripts(level)
        securityHandler.VerifyPantalla(codigo_pantalla, level)

        If Not IsPostBack Then
            Me.Title = Resources.Orbecatalog_Resources.Orbecatalog & " - " & Resources.Productos_Resources.TipoRelacionGrupos_Pantalla

            dg_TipoRelacion_Grupos.Attributes.Add("SelectedIndex", -1)
            dg_TipoRelacion_Grupos.Attributes.Add("SelectedPage", -1)
            securityHandler.VerifyActions(btn_Salvar, btn_Modificar, btn_Eliminar)
            ClearInfo(Configuration.EstadoPantalla.NORMAL, False)
            SetThemeProperties()
            SetControlProperties()

            selectTipoRelacion_Grupos_Producto()
        End If
    End Sub

    Protected Sub ClearInfo(ByVal estado As Configuration.EstadoPantalla, ByVal clearInfo As Boolean)
        If clearInfo Then
            vld_tbx_NombreTipoRelacion_Grupos.IsValid = True
            tbx_NombreTipoRelacion_Grupos.Text = ""
            tbx_NombreTipoRelacion_Grupos.ToolTip = ""
            Me.ClearDataGrid(dg_TipoRelacion_Grupos, clearInfo)
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

    Protected Sub SetThemeProperties()




    End Sub

    Protected Sub SetControlProperties()
        tbx_NombreTipoRelacion_Grupos.Attributes.Add("maxlength", 50)
        tbx_NombreTipoRelacion_Grupos.Attributes.Add("onkeypress", "showTextProgress(this)")
    End Sub

    'TipoRelacion_Grupos_Producto
    Protected Sub selectTipoRelacion_Grupos_Producto()
        Dim dataSet As Data.DataSet
        Dim TipoRelacion_Grupos_Producto As New TipoRelacion_Grupos_Producto
        TipoRelacion_Grupos_Producto.Fields.SelectAll()
        queryBuilder.Orderby.Add(TipoRelacion_Grupos_Producto.Nombre)
        dataSet = connection.executeSelect(queryBuilder.SelectQuery(TipoRelacion_Grupos_Producto))

        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables(0).Rows.Count > 0 Then
                dg_TipoRelacion_Grupos.DataSource = dataSet
                dg_TipoRelacion_Grupos.DataKeyField = TipoRelacion_Grupos_Producto.Id_TipoRelacion.Name
                dg_TipoRelacion_Grupos.DataBind()

                'Llena el grid
                Dim offset As Integer = dg_TipoRelacion_Grupos.CurrentPageIndex * dg_TipoRelacion_Grupos.PageSize
                Dim counter As Integer
                Dim resultados As ArrayList = ObjectBuilder.TransformDataTable(dataSet.Tables(0), TipoRelacion_Grupos_Producto)
                For counter = 0 To dg_TipoRelacion_Grupos.Items.Count - 1
                    Dim act_TipoRelacion_Grupos_Producto As TipoRelacion_Grupos_Producto = resultados(offset + counter)
                    Dim lbl_NombreAtributo As Label = dg_TipoRelacion_Grupos.Items(counter).FindControl("lbl_Nombre")
                    lbl_NombreAtributo.Text = act_TipoRelacion_Grupos_Producto.Nombre.Value

                    'Javascript
                    dg_TipoRelacion_Grupos.Items(counter).Attributes.Add("onmouseover", "setRowFocus(this, " & counter & ", false)")
                    dg_TipoRelacion_Grupos.Items(counter).Attributes.Add("onmouseout", "unsetRowFocus(this, " & counter & ", false)")
                Next
                lbl_ResultadoElementos.Text = contadorElementos(offset, dg_TipoRelacion_Grupos.PageSize, dataSet.Tables(0).Rows.Count)
                dg_TipoRelacion_Grupos.Visible = True
            Else
                lbl_ResultadoElementos.Text = contadorElementos(0, 0, 0)
                dg_TipoRelacion_Grupos.Visible = False
            End If
        End If
    End Sub

    Protected Sub loadTipoRelacion_Grupos_Producto(ByVal Id_TipoRelacion As Integer)
        Dim dataSet As Data.DataSet
        Dim TipoRelacion_Grupos_Producto As New TipoRelacion_Grupos_Producto

        TipoRelacion_Grupos_Producto.Fields.SelectAll()
        TipoRelacion_Grupos_Producto.Id_TipoRelacion.Where.EqualCondition(Id_TipoRelacion)
        dataSet = connection.executeSelect(queryBuilder.SelectQuery(TipoRelacion_Grupos_Producto))
        ObjectBuilder.CreateObject(dataSet.Tables(0), 0, TipoRelacion_Grupos_Producto)

        tbx_NombreTipoRelacion_Grupos.Text = TipoRelacion_Grupos_Producto.Nombre.Value
        tbx_NombreTipoRelacion_Grupos.Attributes.Add(TipoRelacion_Grupos_Producto.Id_TipoRelacion.Name, TipoRelacion_Grupos_Producto.Id_TipoRelacion.Value)
    End Sub

    Protected Function insertTipoRelacion_Grupos_Producto() As Boolean
        Dim TipoRelacion_Grupos_Producto As TipoRelacion_Grupos_Producto
        Try
            TipoRelacion_Grupos_Producto = New TipoRelacion_Grupos_Producto(tbx_NombreTipoRelacion_Grupos.Text)
            connection.executeInsert(queryBuilder.InsertQuery(TipoRelacion_Grupos_Producto))
            Return True
        Catch exc As Exception
            MyMaster.mostrarMensaje(exc.Message, True)

            Return False
        End Try
    End Function

    Protected Function updateTipoRelacion_Grupos_Producto(ByVal Id_TipoRelacion As Integer) As Boolean
        Dim TipoRelacion_Grupos_Producto As TipoRelacion_Grupos_Producto
        Try
            TipoRelacion_Grupos_Producto = New TipoRelacion_Grupos_Producto(tbx_NombreTipoRelacion_Grupos.Text)
            TipoRelacion_Grupos_Producto.Id_TipoRelacion.Where.EqualCondition(Id_TipoRelacion)
            connection.executeUpdate(queryBuilder.UpdateQuery(TipoRelacion_Grupos_Producto))
            Return True
        Catch exc As Exception
            MyMaster.mostrarMensaje(exc.Message, True)

            Return False
        End Try
    End Function

    Protected Function deleteTipoRelacion_Grupos_Producto(ByVal Id_TipoRelacion As Integer) As Boolean
        Dim TipoRelacion_Grupos_Producto As New TipoRelacion_Grupos_Producto
        Try
            TipoRelacion_Grupos_Producto.Id_TipoRelacion.Where.EqualCondition(Id_TipoRelacion)
            connection.executeDelete(queryBuilder.DeleteQuery(TipoRelacion_Grupos_Producto))
            Return True
        Catch exc As Exception
            MyMaster.mostrarMensaje(exc.Message, True)

            Return False
        End Try
    End Function

    'Eventos
    Protected Sub btn_Salvar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Salvar.Click

        If IsValid() Then
            If insertTipoRelacion_Grupos_Producto() Then
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Salvar, "Tipo de relacion", False)
                ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
                selectTipoRelacion_Grupos_Producto()
            Else
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Salvar, "tipo de relacion", True)
            End If
        Else
            MyMaster.MostrarMensaje(Resources.Orbecatalog_Resources.CamposRequeridos, False)
        End If
        upd_Contenido.Update()
        upd_Busqueda.Update()
    End Sub

    Protected Sub btn_Modificar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Modificar.Click

        If IsValid() Then
            If updateTipoRelacion_Grupos_Producto(tbx_NombreTipoRelacion_Grupos.Attributes("Id_TipoRelacion")) Then
                'ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
                selectTipoRelacion_Grupos_Producto()
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Modificar, "Tipo de relacion", False)
            Else
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Modificar, "tipo de relacion", True)
            End If
        Else
            MyMaster.MostrarMensaje(Resources.Orbecatalog_Resources.CamposRequeridos, False)
        End If
        upd_Busqueda.Update()
    End Sub

    Protected Sub btn_Cancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Cancelar.Click
        ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
        upd_Contenido.Update()
    End Sub

    Protected Sub btn_Eliminar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Eliminar.Click

        Dim Id_TipoRelacion As Integer = tbx_NombreTipoRelacion_Grupos.Attributes("Id_TipoRelacion")
        If deleteTipoRelacion_Grupos_Producto(Id_TipoRelacion) Then
            selectTipoRelacion_Grupos_Producto()
            ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
            MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Eliminar, "Tipo de relacion", False)
        Else
            MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Eliminar, "tipo de relacion", True)
            MyMaster.concatenarMensaje("<br />No puede haber ninguna relacion de este tipo para borrarlo.", False)
        End If
        upd_Contenido.Update()
        upd_Busqueda.Update()
    End Sub

    Protected Sub dg_TipoRelacion_Grupos_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dg_TipoRelacion_Grupos.EditCommand
        ClearInfo(Configuration.EstadoPantalla.CONSULTAR, True)
        Me.EditCommandDataGrid(dg_TipoRelacion_Grupos, e.Item.ItemIndex)
        loadTipoRelacion_Grupos_Producto(dg_TipoRelacion_Grupos.DataKeys(e.Item.ItemIndex))
        upd_Contenido.Update()
        upd_Busqueda.Update()
    End Sub

    Protected Sub dg_TipoRelacion_Grupos_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dg_TipoRelacion_Grupos.PageIndexChanged
        dg_TipoRelacion_Grupos.CurrentPageIndex = e.NewPageIndex
        selectTipoRelacion_Grupos_Producto()
        Me.PageIndexChange(dg_TipoRelacion_Grupos)
        upd_Busqueda.Update()
    End Sub

    Protected Sub btn_BorrarSeleccionados_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_BorrarSeleccionados.Click

        Dim borrados As Integer = 0
        For counter As Integer = 0 To dg_TipoRelacion_Grupos.Items.Count - 1
            Dim chk_Control As CheckBox = dg_TipoRelacion_Grupos.Items(counter).FindControl("chk_Control")
            Dim id_TipoRelacion_Grupos_Producto As Integer = dg_TipoRelacion_Grupos.DataKeys.Item(counter)

            If chk_Control.Checked Then
                If deleteTipoRelacion_Grupos_Producto(id_TipoRelacion_Grupos_Producto) Then
                    borrados += 1
                End If
            End If
        Next
        MyMaster.mostrarMensaje("Tipos de relacion borrados: " & borrados, False)
        ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
        dg_TipoRelacion_Grupos.CurrentPageIndex = 0
        selectTipoRelacion_Grupos_Producto()
    End Sub

    Protected Sub chk_Todos_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        checkTodos(dg_TipoRelacion_Grupos, sender.checked)
    End Sub

End Class
