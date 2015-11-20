Imports Orbelink.DBHandler
Imports Orbelink.Entity.Entidades

Partial Class _TipoRelacion_Entidades
    Inherits PageBaseClass

    Const codigo_pantalla As String = "CO-09"
    Const level As Integer = 2

    'Pagina

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddScripts(level)


        securityHandler.VerifyPantalla(codigo_pantalla, level)



        If Not IsPostBack Then

            dg_TipoRelacion_Entidades.Attributes.Add("SelectedIndex", -1)
            dg_TipoRelacion_Entidades.Attributes.Add("SelectedPage", -1)
            securityHandler.VerifyActions(btn_Salvar, btn_Modificar, btn_Eliminar)
            ClearInfo(Configuration.EstadoPantalla.NORMAL, False)

            selectTipoRelacion_Entidades()
        End If
    End Sub

    Protected Sub ClearInfo(ByVal estado As Configuration.EstadoPantalla, ByVal clearInfo As Boolean)
        If clearInfo Then
            vld_tbx_NombreBaseDependeiente.IsValid = True
            vld_tbx_NombreDependienteBase.IsValid = True
            tbx_NombreBaseDependeiente.Text = ""
            tbx_NombreDependienteBase.Text = ""
            tbx_NombreBaseDependeiente.ToolTip = ""
            Me.ClearDataGrid(dg_TipoRelacion_Entidades, clearInfo)
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

    'TipoRelacion_Entidades
    Protected Sub selectTipoRelacion_Entidades()
        Dim dataSet As Data.DataSet
        Dim TipoRelacion_Entidades As New TipoRelacion_Entidades
        TipoRelacion_Entidades.Fields.SelectAll()
        queryBuilder.Orderby.Add(TipoRelacion_Entidades.NombreBaseDependiente)
        dataSet = connection.executeSelect(queryBuilder.SelectQuery(TipoRelacion_Entidades))

        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables(0).Rows.Count > 0 Then
                dg_TipoRelacion_Entidades.DataSource = dataSet
                dg_TipoRelacion_Entidades.DataKeyField = TipoRelacion_Entidades.Id_TipoRelacion.Name
                dg_TipoRelacion_Entidades.DataBind()

                'Llena el grid
                Dim offset As Integer = dg_TipoRelacion_Entidades.CurrentPageIndex * dg_TipoRelacion_Entidades.PageSize
                Dim counter As Integer
                Dim resultados As ArrayList = ObjectBuilder.TransformDataTable(dataSet.Tables(0), TipoRelacion_Entidades)
                For counter = 0 To dg_TipoRelacion_Entidades.Items.Count - 1
                    Dim act_TipoRelacion_Entidades As TipoRelacion_Entidades = resultados(offset + counter)
                    Dim lbl_NombreAtributo As Label = dg_TipoRelacion_Entidades.Items(counter).FindControl("lbl_Nombre")
                    lbl_NombreAtributo.Text = act_TipoRelacion_Entidades.NombreBaseDependiente.Value

                    'Javascript
                    dg_TipoRelacion_Entidades.Items(counter).Attributes.Add("onmouseover", "setRowFocus(this, " & counter & ", false)")
                    dg_TipoRelacion_Entidades.Items(counter).Attributes.Add("onmouseout", "unsetRowFocus(this, " & counter & ", false)")
                Next
                lbl_ResultadoElementos.Text = contadorElementos(offset, dg_TipoRelacion_Entidades.PageSize, dataSet.Tables(0).Rows.Count)
                dg_TipoRelacion_Entidades.Visible = True
            Else
                lbl_ResultadoElementos.Text = contadorElementos(0, 0, 0)
                dg_TipoRelacion_Entidades.Visible = False
            End If
        End If
    End Sub

    Protected Function loadTipoRelacion_Entidades(ByVal Id_TipoRelacion As Integer) As Boolean
        Dim dataSet As Data.DataSet
        Dim TipoRelacion_Entidades As New TipoRelacion_Entidades

        TipoRelacion_Entidades.Fields.SelectAll()
        TipoRelacion_Entidades.Id_TipoRelacion.Where.EqualCondition(Id_TipoRelacion)
        dataSet = connection.executeSelect(queryBuilder.SelectQuery(TipoRelacion_Entidades))
        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables(0).Rows.Count > 0 Then
                ObjectBuilder.CreateObject(dataSet.Tables(0), 0, TipoRelacion_Entidades)

                tbx_NombreBaseDependeiente.Text = TipoRelacion_Entidades.NombreBaseDependiente.Value
                tbx_NombreDependienteBase.Text = TipoRelacion_Entidades.NombreDependienteBase.Value
                tbx_NombreBaseDependeiente.Attributes.Add(TipoRelacion_Entidades.Id_TipoRelacion.Name, TipoRelacion_Entidades.Id_TipoRelacion.Value)
                Return True
            End If
        End If
        Return False
    End Function

    Protected Function insertTipoRelacion_Entidades() As Boolean
        Dim TipoRelacion_Entidades As TipoRelacion_Entidades
        Try
            TipoRelacion_Entidades = New TipoRelacion_Entidades(tbx_NombreBaseDependeiente.Text, tbx_NombreDependienteBase.Text)
            connection.executeInsert(queryBuilder.InsertQuery(TipoRelacion_Entidades))
            Return True
        Catch exc As Exception
            MyMaster.mostrarMensaje(exc.Message, True)

            Return False
        End Try
    End Function

    Protected Function updateTipoRelacion_Entidades(ByVal Id_TipoRelacion As Integer) As Boolean
        Dim TipoRelacion_Entidades As TipoRelacion_Entidades
        Try
            TipoRelacion_Entidades = New TipoRelacion_Entidades(tbx_NombreBaseDependeiente.Text, tbx_NombreDependienteBase.Text)
            TipoRelacion_Entidades.Id_TipoRelacion.Where.EqualCondition(Id_TipoRelacion)
            connection.executeUpdate(queryBuilder.UpdateQuery(TipoRelacion_Entidades))
            Return True
        Catch exc As Exception
            MyMaster.mostrarMensaje(exc.Message, True)

            Return False
        End Try
    End Function

    Protected Function deleteTipoRelacion_Entidades(ByVal Id_TipoRelacion As Integer) As Boolean
        Dim TipoRelacion_Entidades As New TipoRelacion_Entidades
        Try
            TipoRelacion_Entidades.Id_TipoRelacion.Where.EqualCondition(Id_TipoRelacion)
            connection.executeDelete(queryBuilder.DeleteQuery(TipoRelacion_Entidades))
            Return True
        Catch exc As Exception
            MyMaster.mostrarMensaje(exc.Message, True)

            Return False
        End Try
    End Function

    'Eventos
    Protected Sub btn_Salvar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Salvar.Click

        If IsValid() Then
            If insertTipoRelacion_Entidades() Then
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Salvar, "Tipo de relacion", False)
                ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
                selectTipoRelacion_Entidades()
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
            If updateTipoRelacion_Entidades(tbx_NombreBaseDependeiente.Attributes("Id_TipoRelacion")) Then
                'ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
                selectTipoRelacion_Entidades()
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

        Dim Id_TipoRelacion As Integer = tbx_NombreBaseDependeiente.Attributes("Id_TipoRelacion")
        If deleteTipoRelacion_Entidades(Id_TipoRelacion) Then
            selectTipoRelacion_Entidades()
            ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
            MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Eliminar, "Tipo de relacion", False)
        Else
            MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Eliminar, "tipo de relacion", True)
            MyMaster.concatenarMensaje("<br />No puede haber ninguna relacion de este tipo para borrarlo.", False)
        End If
        upd_Contenido.Update()
        upd_Busqueda.Update()
    End Sub

    Protected Sub dg_TipoRelacion_Entidades_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dg_TipoRelacion_Entidades.EditCommand
        ClearInfo(Configuration.EstadoPantalla.CONSULTAR, True)
        Me.EditCommandDataGrid(dg_TipoRelacion_Entidades, e.Item.ItemIndex)
        loadTipoRelacion_Entidades(dg_TipoRelacion_Entidades.DataKeys(e.Item.ItemIndex))
        upd_Contenido.Update()
        upd_Busqueda.Update()
    End Sub

    Protected Sub dg_TipoRelacion_Entidades_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dg_TipoRelacion_Entidades.PageIndexChanged
        dg_TipoRelacion_Entidades.CurrentPageIndex = e.NewPageIndex
        selectTipoRelacion_Entidades()
        Me.PageIndexChange(dg_TipoRelacion_Entidades)
        upd_Busqueda.Update()
    End Sub

    Protected Sub btn_BorrarSeleccionados_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_BorrarSeleccionados.Click

        Dim borrados As Integer = 0
        For counter As Integer = 0 To dg_TipoRelacion_Entidades.Items.Count - 1
            Dim chk_Control As CheckBox = dg_TipoRelacion_Entidades.Items(counter).FindControl("chk_Control")
            Dim id_TipoRelacion_Entidades As Integer = dg_TipoRelacion_Entidades.DataKeys.Item(counter)

            If chk_Control.Checked Then
                If deleteTipoRelacion_Entidades(id_TipoRelacion_Entidades) Then
                    borrados += 1
                End If
            End If
        Next
        MyMaster.mostrarMensaje("Tipos de relacion borrados: " & borrados, False)
        ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
        dg_TipoRelacion_Entidades.CurrentPageIndex = 0
        Me.selectTipoRelacion_Entidades()
    End Sub

    Protected Sub chk_Todos_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        checkTodos(dg_TipoRelacion_Entidades, sender.checked)
    End Sub
End Class
