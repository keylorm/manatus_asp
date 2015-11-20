Imports Orbelink.DBHandler
Imports Orbelink.Entity.Orbecatalog
Imports Orbelink.DateAndTime

Partial Class _HorarioExcepciones
    Inherits PageBaseClass

    Const codigo_pantalla As String = "OR-08"
    Const level As Integer = 2

    'Pagina

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddScripts(level)
        securityHandler.VerifyPantalla(codigo_pantalla, level)
        If Not IsPostBack Then
            dg_HorarioExcepciones.Attributes.Add("SelectedIndex", -1)
            dg_HorarioExcepciones.Attributes.Add("SelectedPage", -1)
            ClearInfo(Configuration.EstadoPantalla.NORMAL, False)
            securityHandler.VerifyActions(btn_Salvar, btn_Modificar, btn_Eliminar)

            selectHorarioExcepciones()
        End If
    End Sub

    Protected Sub ClearInfo(ByVal estado As Configuration.EstadoPantalla, ByVal clearInfo As Boolean)
        If clearInfo Then
            'tbx_HoraInicio.Text = ""
            'tbx_HoraFinal.Text = ""
            Me.ClearDataGrid(dg_HorarioExcepciones, clearInfo)
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

    'HorarioExcepciones
    Protected Sub selectHorarioExcepciones()
        Dim dataSet As Data.DataSet
        Dim HorarioExcepciones As New HorarioExcepciones

        HorarioExcepciones.Fields.SelectAll()
        queryBuilder.Orderby.Add(HorarioExcepciones.Fecha)
        dataSet = connection.executeSelect(queryBuilder.SelectQuery(HorarioExcepciones))

        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables(0).Rows.Count > 0 Then
                dg_HorarioExcepciones.DataSource = dataSet
                dg_HorarioExcepciones.DataKeyField = HorarioExcepciones.Fecha.Name
                dg_HorarioExcepciones.DataBind()

                'Llena el grid
                Dim offset As Integer = dg_HorarioExcepciones.CurrentPageIndex * dg_HorarioExcepciones.PageSize
                Dim counter As Integer
                Dim resultados As ArrayList = ObjectBuilder.TransformDataTable(dataSet.Tables(0), HorarioExcepciones)
                For counter = 0 To dg_HorarioExcepciones.Items.Count - 1
                    Dim act_HorarioExcepciones As HorarioExcepciones = resultados(offset + counter)
                    Dim lbl_Fecha As Label = dg_HorarioExcepciones.Items(counter).FindControl("lbl_Fecha")

                    lbl_Fecha.Text = act_HorarioExcepciones.Fecha.ValueLocalized
                    cld_Fecha.SelectedDates.Add(act_HorarioExcepciones.Fecha.ValueLocalized)

                    'Javascript
                    dg_HorarioExcepciones.Items(counter).Attributes.Add("onmouseover", "setRowFocus(this, " & counter & ", false)")
                    dg_HorarioExcepciones.Items(counter).Attributes.Add("onmouseout", "unsetRowFocus(this, " & counter & ", false)")
                Next
                lbl_ResultadoElementos.Text = contadorElementos(offset, dg_HorarioExcepciones.PageSize, dataSet.Tables(0).Rows.Count)
                dg_HorarioExcepciones.Visible = True
            Else
                lbl_ResultadoElementos.Text = contadorElementos(0, 0, 0)
                dg_HorarioExcepciones.Visible = False
            End If
        End If
    End Sub

    Protected Sub loadHorarioExcepciones(ByVal fecha As Date)
        cld_Fecha.SelectedDate = fecha

        'Dim dataSet As Data.DataSet
        'Dim HorarioExcepciones As New HorarioExcepciones

        'HorarioExcepciones.Fields.SelectAll()
        'dataSet = connection.executeSelect(queryBuilder.SelectQuery(HorarioExcepciones))
        'objectBuilder.createObject(dataSet.Tables(0), 0, HorarioExcepciones)

        'ddl_Dia.SelectedIndex = HorarioExcepciones.Dia.Value
        'tbx_HoraInicio.Text = HorarioExcepciones.HoraInicio.Value
        'tbx_HoraInicio.Attributes.Add(HorarioExcepciones.Id_HorarioExcepciones.Name, HorarioExcepciones.Id_HorarioExcepciones.Value)
        'tbx_horaFinal.Text = HorarioExcepciones.HoraFinal.Value
    End Sub

    Protected Function insertHorarioExcepciones() As Boolean
        Dim HorarioExcepciones As HorarioExcepciones
        Try
            HorarioExcepciones = New HorarioExcepciones(DateHandler.ToUtcFromLocalizedDate(cld_Fecha.SelectedDate))
            connection.executeInsert(queryBuilder.InsertQuery(HorarioExcepciones))
            Return True
        Catch exc As Exception
            MyMaster.MostrarMensaje(exc.Message, True)

            Return False
        End Try
    End Function

    Protected Function deleteHorarioExcepciones(ByVal fecha As Date) As Boolean
        Dim HorarioExcepciones As New HorarioExcepciones
        Try
            HorarioExcepciones.Fecha.Where.EqualCondition(fecha)
            connection.executeDelete(queryBuilder.DeleteQuery(HorarioExcepciones))

            cld_Fecha.SelectedDates.Remove(fecha)
            Return True
        Catch exc As Exception
            MyMaster.MostrarMensaje(exc.Message, True)

            Return False
        End Try
    End Function

    'Eventos
    Protected Sub btn_Salvar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Salvar.Click

        If IsValid() Then
            If insertHorarioExcepciones() Then
                ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
                selectHorarioExcepciones()
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

    Protected Sub btn_Cancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Cancelar.Click
        ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
        upd_Contenido.Update()
        upd_Busqueda.Update()
    End Sub

    Protected Sub btn_Eliminar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Eliminar.Click

        If deleteHorarioExcepciones(cld_Fecha.SelectedDate) Then
            selectHorarioExcepciones()
            ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
            MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Eliminar, "Horario excepcion", False)
        Else
            MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Eliminar, "horario excepcion", True)
        End If
        upd_Contenido.Update()
        upd_Busqueda.Update()
    End Sub

    Protected Sub dg_HorarioExcepciones_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dg_HorarioExcepciones.EditCommand
        ClearInfo(Configuration.EstadoPantalla.CONSULTAR, True)
        Me.EditCommandDataGrid(dg_HorarioExcepciones, e.Item.ItemIndex)
        loadHorarioExcepciones(dg_HorarioExcepciones.DataKeys(e.Item.ItemIndex))
        upd_Contenido.Update()
        upd_Busqueda.Update()
    End Sub

    Protected Sub dg_HorarioExcepciones_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dg_HorarioExcepciones.PageIndexChanged
        dg_HorarioExcepciones.CurrentPageIndex = e.NewPageIndex
        selectHorarioExcepciones()
        Me.PageIndexChange(dg_HorarioExcepciones)
        upd_Busqueda.Update()
    End Sub

End Class
