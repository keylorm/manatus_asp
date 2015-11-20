Imports Orbelink.DBHandler
Imports Orbelink.Entity.Orbecatalog

Partial Class _HorarioOrdinario
    Inherits PageBaseClass

    Const codigo_pantalla As String = "OR-07"
    Const level As Integer = 2

    'Pagina
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddScripts(level)
        securityHandler.VerifyPantalla(codigo_pantalla, level)
        If Not IsPostBack Then
            dg_HorarioOrdinario.Attributes.Add("SelectedIndex", -1)
            dg_HorarioOrdinario.Attributes.Add("SelectedPage", -1)
            securityHandler.VerifyActions(btn_Salvar, btn_Modificar, btn_Eliminar)
            ClearInfo(Configuration.EstadoPantalla.NORMAL, False)

            selectHorarioOrdinario()
        End If
    End Sub

    Protected Sub ClearInfo(ByVal estado As Configuration.EstadoPantalla, ByVal clearInfo As Boolean)
        If clearInfo Then
            tbx_HoraInicio.Text = ""
            tbx_HoraFinal.Text = ""
            Me.ClearDataGrid(dg_HorarioOrdinario, clearInfo)
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

    'HorarioOrdinario
    Protected Sub selectHorarioOrdinario()
        Dim dataSet As Data.DataSet
        Dim HorarioOrdinario As New HorarioOrdinario

        HorarioOrdinario.Fields.SelectAll()
        queryBuilder.Orderby.Add(HorarioOrdinario.Dia)
        dataSet = connection.executeSelect(queryBuilder.SelectQuery(HorarioOrdinario))

        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables(0).Rows.Count > 0 Then
                dg_HorarioOrdinario.DataSource = dataSet
                dg_HorarioOrdinario.DataKeyField = HorarioOrdinario.Id_HorarioOrdinario.Name
                dg_HorarioOrdinario.DataBind()

                'Llena el grid
                Dim offset As Integer = dg_HorarioOrdinario.CurrentPageIndex * dg_HorarioOrdinario.PageSize
                Dim counter As Integer
                Dim resultados As ArrayList = ObjectBuilder.TransformDataTable(dataSet.Tables(0), HorarioOrdinario)
                For counter = 0 To dg_HorarioOrdinario.Items.Count - 1
                    Dim act_HorarioOrdinario As HorarioOrdinario = resultados(offset + counter)
                    Dim lbl_NombreDia As Label = dg_HorarioOrdinario.Items(counter).FindControl("lbl_NombreDia")
                    Dim lbl_Descripcion As Label = dg_HorarioOrdinario.Items(counter).FindControl("lbl_Descripcion")

                    lbl_NombreDia.Text = ddl_Dia.Items(act_HorarioOrdinario.Dia.Value).Text
                    lbl_Descripcion.Text = act_HorarioOrdinario.HoraInicio.Value & " - " & act_HorarioOrdinario.HoraFinal.Value

                    'Javascript
                    dg_HorarioOrdinario.Items(counter).Attributes.Add("onmouseover", "setRowFocus(this, " & counter & ", false)")
                    dg_HorarioOrdinario.Items(counter).Attributes.Add("onmouseout", "unsetRowFocus(this, " & counter & ", false)")
                Next
                lbl_ResultadoElementos.Text = contadorElementos(offset, dg_HorarioOrdinario.PageSize, dataSet.Tables(0).Rows.Count)
                dg_HorarioOrdinario.Visible = True
            Else
                lbl_ResultadoElementos.Text = contadorElementos(0, 0, 0)
                dg_HorarioOrdinario.Visible = False
            End If
        End If
    End Sub

    Protected Sub loadHorarioOrdinario(ByVal Id_HorarioOrdinario As Integer)
        Dim dataSet As Data.DataSet
        Dim HorarioOrdinario As New HorarioOrdinario

        HorarioOrdinario.Fields.SelectAll()
        HorarioOrdinario.Id_HorarioOrdinario.Where.EqualCondition(Id_HorarioOrdinario)
        dataSet = connection.executeSelect(queryBuilder.SelectQuery(HorarioOrdinario))
        ObjectBuilder.CreateObject(dataSet.Tables(0), 0, HorarioOrdinario)

        ddl_Dia.SelectedIndex = HorarioOrdinario.Dia.Value
        tbx_HoraInicio.Text = HorarioOrdinario.HoraInicio.Value
        tbx_HoraInicio.Attributes.Add(HorarioOrdinario.Id_HorarioOrdinario.Name, HorarioOrdinario.Id_HorarioOrdinario.Value)
        tbx_horaFinal.Text = HorarioOrdinario.HoraFinal.Value
    End Sub

    Protected Function insertHorarioOrdinario() As Boolean
        Dim HorarioOrdinario As HorarioOrdinario
        Try
            HorarioOrdinario = New HorarioOrdinario(ddl_Dia.SelectedIndex, tbx_HoraInicio.Text, tbx_HoraFinal.Text)
            connection.executeInsert(queryBuilder.InsertQuery(HorarioOrdinario))
            Return True
        Catch exc As Exception
            MyMaster.mostrarMensaje(exc.Message, True)
            Return False
        End Try
    End Function

    Protected Function updateHorarioOrdinario(ByVal id_HorarioOrdinario As Integer) As Boolean
        Dim HorarioOrdinario As HorarioOrdinario
        Try
            HorarioOrdinario = New HorarioOrdinario(ddl_Dia.SelectedIndex, tbx_HoraInicio.Text, tbx_HoraFinal.Text)
            HorarioOrdinario.Id_HorarioOrdinario.Where.EqualCondition(id_HorarioOrdinario)
            connection.executeUpdate(queryBuilder.UpdateQuery(HorarioOrdinario))
            Return True
        Catch exc As Exception
            MyMaster.mostrarMensaje(exc.Message, True)
            Return False
        End Try
    End Function

    Protected Function deleteHorarioOrdinario(ByVal id_HorarioOrdinario As Integer) As Boolean
        Dim HorarioOrdinario As New HorarioOrdinario
        Try
            HorarioOrdinario.Id_HorarioOrdinario.Where.EqualCondition(id_HorarioOrdinario)
            connection.executeDelete(queryBuilder.DeleteQuery(HorarioOrdinario))
            Return True
        Catch exc As Exception
            MyMaster.mostrarMensaje(exc.Message, True)
            Return False
        End Try
    End Function

    'Eventos
    Protected Sub btn_Salvar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Salvar.Click
        If IsValid() Then
            If insertHorarioOrdinario() Then
                ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
                selectHorarioOrdinario()
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Salvar, "Horario ordinario", False)
            Else
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Salvar, "horario ordinario", True)
            End If
        Else
            MyMaster.MostrarMensaje(Resources.Orbecatalog_Resources.CamposRequeridos, False)
        End If
        upd_Contenido.Update()
        upd_Busqueda.Update()
    End Sub

    Protected Sub btn_Modificar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Modificar.Click
        If IsValid() Then
            If updateHorarioOrdinario(tbx_HoraInicio.Attributes("Id_HorarioOrdinario")) Then
                'ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
                selectHorarioOrdinario()
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Modificar, "Horario ordinario", False)
            Else
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Modificar, "horario ordinario", True)
            End If
        Else
            MyMaster.MostrarMensaje(Resources.Orbecatalog_Resources.CamposRequeridos, False)
        End If
        'upd_Contenido.Update()
        upd_Busqueda.Update()
    End Sub

    Protected Sub btn_Cancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Cancelar.Click
        ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
        upd_Contenido.Update()
        upd_Busqueda.Update()
    End Sub

    Protected Sub btn_Eliminar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Eliminar.Click
        Dim id_HorarioOrdinario As Integer = tbx_HoraInicio.Attributes("Id_HorarioOrdinario")
        If deleteHorarioOrdinario(id_HorarioOrdinario) Then
            selectHorarioOrdinario()
            ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
            MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Eliminar, "Horario ordinario", False)
        Else
            MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Eliminar, "horario ordinario", True)
            MyMaster.concatenarMensaje("<br />No puede haber ninguna entidad de este tipo para borrarlo.", False)
        End If
        upd_Contenido.Update()
        upd_Busqueda.Update()
    End Sub

    Protected Sub dg_HorarioOrdinario_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dg_HorarioOrdinario.EditCommand
        ClearInfo(Configuration.EstadoPantalla.CONSULTAR, True)
        Me.EditCommandDataGrid(dg_HorarioOrdinario, e.Item.ItemIndex)
        loadHorarioOrdinario(dg_HorarioOrdinario.DataKeys(e.Item.ItemIndex))
        upd_Contenido.Update()
        upd_Busqueda.Update()
    End Sub

    Protected Sub dg_HorarioOrdinario_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dg_HorarioOrdinario.PageIndexChanged
        dg_HorarioOrdinario.CurrentPageIndex = e.NewPageIndex
        selectHorarioOrdinario()
        Me.PageIndexChange(dg_HorarioOrdinario)
        upd_Busqueda.Update()
    End Sub

End Class
