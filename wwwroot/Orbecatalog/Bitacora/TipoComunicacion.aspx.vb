Imports Orbelink.DBHandler
Imports Orbelink.Entity.Bitacora

Partial Class Orbecatalog_Bitacora_TipoComunicacion
    Inherits PageBaseClass

    Const codigo_pantalla As String = "BT-03"
    Const level As Integer = 2

    'Pagina

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddScripts(level)


        securityHandler.VerifyPantalla(codigo_pantalla, level)
        If Not IsPostBack Then

            dg_TipoComunicacion.Attributes.Add("SelectedIndex", -1)
            dg_TipoComunicacion.Attributes.Add("SelectedPage", -1)
            ClearInfo(Configuration.EstadoPantalla.NORMAL, False)
            securityHandler.VerifyActions(btn_Salvar, btn_Modificar, btn_Eliminar)

            selectTipoComunicacion()
        End If
    End Sub

    Protected Sub ClearInfo(ByVal estado As Configuration.EstadoPantalla, ByVal clearInfo As Boolean)
        If clearInfo Then
            vld_Nombre.IsValid = True
            tbx_NombreTipoComunicacion.Text = ""
            tbx_NombreTipoComunicacion.ToolTip = ""
            Me.ClearDataGrid(dg_TipoComunicacion, clearInfo)
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

    'TipoComunicacion
    Protected Sub selectTipoComunicacion()
        Dim dataSet As Data.DataSet
        Dim TipoComunicacion As New TipoComunicacion

        TipoComunicacion.Fields.SelectAll()
        queryBuilder.Orderby.Add(TipoComunicacion.Nombre)
        dataSet = connection.executeSelect(queryBuilder.SelectQuery(TipoComunicacion))

        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables(0).Rows.Count > 0 Then
                dg_TipoComunicacion.DataSource = dataSet
                dg_TipoComunicacion.DataKeyField = TipoComunicacion.Id_TipoComunicacion.Name
                dg_TipoComunicacion.DataBind()

                'Llena el grid
                Dim offset As Integer = dg_TipoComunicacion.CurrentPageIndex * dg_TipoComunicacion.PageSize
                Dim counter As Integer
                Dim result_TipoComunicacion As ArrayList = ObjectBuilder.TransformDataTable(dataSet.Tables(0), TipoComunicacion)
                For counter = 0 To dg_TipoComunicacion.Items.Count - 1
                    Dim act_TipoComunicacion As TipoComunicacion = result_TipoComunicacion(offset + counter)
                    Dim lbl_NombreAtributo As Label = dg_TipoComunicacion.Items(counter).FindControl("lbl_Nombre")
                    lbl_NombreAtributo.Text = act_TipoComunicacion.Nombre.Value

                    'Javascript
                    dg_TipoComunicacion.Items(counter).Attributes.Add("onmouseover", "setRowFocus(this, " & counter & ", false)")
                    dg_TipoComunicacion.Items(counter).Attributes.Add("onmouseout", "unsetRowFocus(this, " & counter & ", false)")
                Next
                lbl_ResultadoElementos.Text = contadorElementos(offset, dg_TipoComunicacion.PageSize, dataSet.Tables(0).Rows.Count)
                dg_TipoComunicacion.Visible = True
            Else
                lbl_ResultadoElementos.Text = contadorElementos(0, 0, 0)
                dg_TipoComunicacion.Visible = False
            End If
        End If
    End Sub

    Protected Sub loadTipoComunicacion(ByVal id_TipoComunicacion As Integer)
        Dim dataSet As Data.DataSet
        Dim TipoComunicacion As New TipoComunicacion
        Dim autoCompletar As Integer = 0

        TipoComunicacion.Fields.SelectAll()
        TipoComunicacion.Id_TipoComunicacion.Where.EqualCondition(id_TipoComunicacion)

        dataSet = connection.executeSelect(queryBuilder.SelectQuery(TipoComunicacion))
        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables(0).Rows.Count > 0 Then
                ObjectBuilder.CreateObject(dataSet.Tables(0), 0, TipoComunicacion)
                tbx_NombreTipoComunicacion.Text = TipoComunicacion.Nombre.Value
                tbx_NombreTipoComunicacion.ToolTip = TipoComunicacion.Id_TipoComunicacion.Value
            End If
        End If
    End Sub

    Protected Function insertTipoComunicacion() As Boolean
        Dim TipoComunicacion As TipoComunicacion
        Try
            TipoComunicacion = New TipoComunicacion(tbx_NombreTipoComunicacion.Text)
            connection.executeInsert(queryBuilder.InsertQuery(TipoComunicacion))
            Return True
        Catch exc As Exception
            MyMaster.mostrarMensaje(exc.Message, True)

            Return False
        End Try
    End Function

    Protected Function updateTipoComunicacion(ByVal id_TipoComunicacion As Integer) As Boolean
        Dim TipoComunicacion As TipoComunicacion
        Try
            TipoComunicacion = New TipoComunicacion(tbx_NombreTipoComunicacion.Text)
            TipoComunicacion.Id_TipoComunicacion.Where.EqualCondition(id_TipoComunicacion)
            connection.executeUpdate(queryBuilder.UpdateQuery(TipoComunicacion))
            Return True
        Catch exc As Exception
            MyMaster.mostrarMensaje(exc.Message, True)

            Return False
        End Try
    End Function

    Protected Function deleteTipoComunicacion(ByVal id_TipoComunicacion As Integer) As Boolean
        Dim TipoComunicacion As New TipoComunicacion
        Try
            TipoComunicacion.Id_TipoComunicacion.Where.EqualCondition(id_TipoComunicacion)
            connection.executeDelete(queryBuilder.DeleteQuery(TipoComunicacion))
            Return True
        Catch exc As Exception
            MyMaster.mostrarMensaje(exc.Message, True)

            Return False
        End Try
    End Function

    'Eventos
    Protected Sub btn_Salvar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Salvar.Click
        If IsValid() Then
            If insertTipoComunicacion() Then
                ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
                selectTipoComunicacion()
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Salvar, "Tipo de Lead", False)
            Else
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Salvar, "tipo de Lead", True)
            End If
        Else
            MyMaster.MostrarMensaje(Resources.Orbecatalog_Resources.CamposRequeridos, False)
        End If
        upd_Contenido.Update()
        upd_Busqueda.Update()
    End Sub

    Protected Sub btn_Modificar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Modificar.Click

        If IsValid() Then
            If updateTipoComunicacion(tbx_NombreTipoComunicacion.ToolTip) Then
                selectTipoComunicacion()
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Modificar, "Tipo de Lead", False)
            Else
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Modificar, "tipo de Lead", True)
            End If
        Else
            MyMaster.MostrarMensaje(Resources.Orbecatalog_Resources.CamposRequeridos, False)
        End If
        upd_Busqueda.Update()
    End Sub

    Protected Sub btn_Cancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Cancelar.Click
        ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
        upd_Contenido.Update()
        upd_Busqueda.Update()
    End Sub

    Protected Sub btn_Eliminar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Eliminar.Click
        Dim id_TipoComunicacion As Integer = tbx_NombreTipoComunicacion.ToolTip
        If deleteTipoComunicacion(id_TipoComunicacion) Then
            ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
            selectTipoComunicacion()
            MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Eliminar, "Tipo de Lead", False)
        Else
            MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Eliminar, "Tipo de Lead", True)
        End If
        upd_Contenido.Update()
        upd_Busqueda.Update()
    End Sub

    Protected Sub dg_TipoComunicacion_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dg_TipoComunicacion.EditCommand
        ClearInfo(Configuration.EstadoPantalla.CONSULTAR, True)
        Me.EditCommandDataGrid(dg_TipoComunicacion, e.Item.ItemIndex)
        loadTipoComunicacion(dg_TipoComunicacion.DataKeys(e.Item.ItemIndex))
        upd_Contenido.Update()
        upd_Busqueda.Update()
    End Sub

    Protected Sub dg_TipoComunicacion_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dg_TipoComunicacion.PageIndexChanged
        dg_TipoComunicacion.CurrentPageIndex = e.NewPageIndex
        selectTipoComunicacion()
        Me.PageIndexChange(dg_TipoComunicacion)
        upd_Busqueda.Update()
    End Sub
End Class
