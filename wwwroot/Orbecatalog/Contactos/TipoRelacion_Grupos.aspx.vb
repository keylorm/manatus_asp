Imports Orbelink.DBHandler
Imports Orbelink.Entity.Entidades

Partial Class _TipoRelacion_Grupos
    Inherits PageBaseClass

    Const codigo_pantalla As String = "CO-12"
    Const level As Integer = 2

    'Pagina
    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        Theme = securityHandler.Theme
        If Request.QueryString("popUp") IsNot Nothing Then
            Me.MasterPageFile = "../popUp.Master"
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddScripts(level)


        securityHandler.VerifyPantalla(codigo_pantalla, level)

        If Not IsPostBack Then

            dg_TipoRelacion_Grupos.Attributes.Add("SelectedIndex", -1)
            dg_TipoRelacion_Grupos.Attributes.Add("SelectedPage", -1)
            securityHandler.VerifyActions(btn_Salvar, btn_Modificar, btn_Eliminar)
            ClearInfo(Configuration.EstadoPantalla.NORMAL, False)
            SetThemeProperties()
            SetControlProperties()

            selectTipoRelacion_Grupos()
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

    'TipoRelacion_Grupos
    Protected Sub selectTipoRelacion_Grupos()
        Dim dataSet As Data.DataSet
        Dim TipoRelacion_Grupos As New TipoRelacion_Grupos
        TipoRelacion_Grupos.Fields.SelectAll()
        queryBuilder.Orderby.Add(TipoRelacion_Grupos.Nombre)
        dataSet = connection.executeSelect(queryBuilder.SelectQuery(TipoRelacion_Grupos))

        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables(0).Rows.Count > 0 Then
                dg_TipoRelacion_Grupos.DataSource = dataSet
                dg_TipoRelacion_Grupos.DataKeyField = TipoRelacion_Grupos.Id_TipoRelacion.Name
                dg_TipoRelacion_Grupos.DataBind()

                'Llena el grid
                Dim offset As Integer = dg_TipoRelacion_Grupos.CurrentPageIndex * dg_TipoRelacion_Grupos.PageSize
                Dim counter As Integer
                Dim resultados As ArrayList = ObjectBuilder.TransformDataTable(dataSet.Tables(0), TipoRelacion_Grupos)
                For counter = 0 To dg_TipoRelacion_Grupos.Items.Count - 1
                    Dim act_TipoRelacion_Grupos As TipoRelacion_Grupos = resultados(offset + counter)
                    Dim lbl_NombreAtributo As Label = dg_TipoRelacion_Grupos.Items(counter).FindControl("lbl_Nombre")
                    lbl_NombreAtributo.Text = act_TipoRelacion_Grupos.Nombre.Value

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

    Protected Sub loadTipoRelacion_Grupos(ByVal Id_TipoRelacion As Integer)
        Dim dataSet As Data.DataSet
        Dim TipoRelacion_Grupos As New TipoRelacion_Grupos

        TipoRelacion_Grupos.Fields.SelectAll()
        TipoRelacion_Grupos.Id_TipoRelacion.Where.EqualCondition(Id_TipoRelacion)
        dataSet = connection.executeSelect(queryBuilder.SelectQuery(TipoRelacion_Grupos))
        ObjectBuilder.CreateObject(dataSet.Tables(0), 0, TipoRelacion_Grupos)

        tbx_NombreTipoRelacion_Grupos.Text = TipoRelacion_Grupos.Nombre.Value
        tbx_NombreTipoRelacion_Grupos.Attributes.Add(TipoRelacion_Grupos.Id_TipoRelacion.Name, TipoRelacion_Grupos.Id_TipoRelacion.Value)
    End Sub

    Protected Function insertTipoRelacion_Grupos() As Boolean
        Dim TipoRelacion_Grupos As TipoRelacion_Grupos
        Try
            TipoRelacion_Grupos = New TipoRelacion_Grupos(tbx_NombreTipoRelacion_Grupos.Text)
            connection.executeInsert(queryBuilder.InsertQuery(TipoRelacion_Grupos))
            Return True
        Catch exc As Exception
            MyMaster.mostrarMensaje(exc.Message, True)

            Return False
        End Try
    End Function

    Protected Function updateTipoRelacion_Grupos(ByVal Id_TipoRelacion As Integer) As Boolean
        Dim TipoRelacion_Grupos As TipoRelacion_Grupos
        Try
            TipoRelacion_Grupos = New TipoRelacion_Grupos(tbx_NombreTipoRelacion_Grupos.Text)
            TipoRelacion_Grupos.Id_TipoRelacion.Where.EqualCondition(Id_TipoRelacion)
            connection.executeUpdate(queryBuilder.UpdateQuery(TipoRelacion_Grupos))
            Return True
        Catch exc As Exception
            MyMaster.mostrarMensaje(exc.Message, True)

            Return False
        End Try
    End Function

    Protected Function deleteTipoRelacion_Grupos(ByVal Id_TipoRelacion As Integer) As Boolean
        Dim TipoRelacion_Grupos As New TipoRelacion_Grupos
        Try
            TipoRelacion_Grupos.Id_TipoRelacion.Where.EqualCondition(Id_TipoRelacion)
            connection.executeDelete(queryBuilder.DeleteQuery(TipoRelacion_Grupos))
            Return True
        Catch exc As Exception
            MyMaster.mostrarMensaje(exc.Message, True)

            Return False
        End Try
    End Function

    'Eventos
    Protected Sub btn_Salvar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Salvar.Click

        If IsValid() Then
            If insertTipoRelacion_Grupos() Then
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Salvar, "Tipo de relacion", False)
                ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
                selectTipoRelacion_Grupos()
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
            If updateTipoRelacion_Grupos(tbx_NombreTipoRelacion_Grupos.Attributes("Id_TipoRelacion")) Then
                'ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
                selectTipoRelacion_Grupos()
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
        If deleteTipoRelacion_Grupos(Id_TipoRelacion) Then
            selectTipoRelacion_Grupos()
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
        loadTipoRelacion_Grupos(dg_TipoRelacion_Grupos.DataKeys(e.Item.ItemIndex))
        upd_Contenido.Update()
        upd_Busqueda.Update()
    End Sub

    Protected Sub dg_TipoRelacion_Grupos_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dg_TipoRelacion_Grupos.PageIndexChanged
        dg_TipoRelacion_Grupos.CurrentPageIndex = e.NewPageIndex
        selectTipoRelacion_Grupos()
        Me.PageIndexChange(dg_TipoRelacion_Grupos)
        upd_Busqueda.Update()
    End Sub

    Protected Sub btn_BorrarSeleccionados_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_BorrarSeleccionados.Click

        Dim borrados As Integer = 0
        For counter As Integer = 0 To dg_TipoRelacion_Grupos.Items.Count - 1
            Dim chk_Control As CheckBox = dg_TipoRelacion_Grupos.Items(counter).FindControl("chk_Control")
            Dim id_TipoRelacion_Grupos As Integer = dg_TipoRelacion_Grupos.DataKeys.Item(counter)

            If chk_Control.Checked Then
                If deleteTipoRelacion_Grupos(id_TipoRelacion_Grupos) Then
                    borrados += 1
                End If
            End If
        Next
        MyMaster.mostrarMensaje("Tipos de relacion borrados: " & borrados, False)
        ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
        dg_TipoRelacion_Grupos.CurrentPageIndex = 0
        selectTipoRelacion_Grupos()
    End Sub

    Protected Sub chk_Todos_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        checkTodos(dg_TipoRelacion_Grupos, sender.checked)
    End Sub

End Class
