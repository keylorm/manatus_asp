Imports Orbelink.DBHandler
Imports Orbelink.Entity.Reservaciones

Partial Class Orbecatalog_Reservacion_Temporada
    Inherits PageBaseClass

    Const codigo_pantalla As String = "RS-08"
    Const level As Integer = 2

    'Pagina
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddScripts(level)
        securityHandler.VerifyPantalla(codigo_pantalla, level)
        If Not IsPostBack Then
            Me.Title = Resources.Orbecatalog_Resources.Orbecatalog & " - " & Resources.Reservaciones_Resources.Temporada_Pantalla

            dg_Temporada.Attributes.Add("SelectedIndex", -1)
            dg_Temporada.Attributes.Add("SelectedPage", -1)
            ClearInfo(Configuration.EstadoPantalla.NORMAL, False)
            securityHandler.VerifyActions(btn_Salvar, btn_Modificar, btn_Eliminar)

            selectTemporada()
            loadRangoTemporada(0)
        End If
    End Sub

    Protected Sub ClearInfo(ByVal estado As Configuration.EstadoPantalla, ByVal clearInfo As Boolean)
        If clearInfo Then
            vld_Nombre.IsValid = True
            tbx_NombreTemporada.Text = ""
            id_Actual = 0
            Me.ClearDataGrid(dg_Temporada, clearInfo)
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

    'Temporada
    Protected Sub selectTemporada()
        Dim Temporada As New Temporada

        Temporada.Fields.SelectAll()
        queryBuilder.Orderby.Add(Temporada.Nombre)
        Dim dataTable As Data.DataTable = connection.executeSelect_DT(queryBuilder.SelectQuery(Temporada))

        If dataTable.Rows.Count > 0 Then
            dg_Temporada.DataSource = dataTable
            dg_Temporada.DataKeyField = Temporada.Id_Temporada.Name
            dg_Temporada.DataBind()

            'Llena el grid
            Dim offset As Integer = dg_Temporada.CurrentPageIndex * dg_Temporada.PageSize
            Dim counter As Integer
            Dim result_Temporada As ArrayList = ObjectBuilder.TransformDataTable(dataTable, Temporada)
            For counter = 0 To dg_Temporada.Items.Count - 1
                Dim act_Temporada As Temporada = result_Temporada(offset + counter)
                Dim lbl_NombreAtributo As Label = dg_Temporada.Items(counter).FindControl("lbl_Nombre")
                lbl_NombreAtributo.Text = act_Temporada.Nombre.Value

                'Javascript
                dg_Temporada.Items(counter).Attributes.Add("onmouseover", "setRowFocus(this, " & counter & ", false)")
                dg_Temporada.Items(counter).Attributes.Add("onmouseout", "unsetRowFocus(this, " & counter & ", false)")
            Next
            lbl_ResultadoElementos.Text = contadorElementos(offset, dg_Temporada.PageSize, dataTable.Rows.Count)
            dg_Temporada.Visible = True
            If Not IsPostBack Then
                cargarPrioridades(counter)
            End If

        Else
            lbl_ResultadoElementos.Text = contadorElementos(0, 0, 0)
            dg_Temporada.Visible = False

            If Not IsPostBack Then
                cargarPrioridades(1)
            End If

        End If
    End Sub

    Protected Sub loadTemporada(ByVal id_Temporada As Integer)
        Dim Temporada As New Temporada
        Dim autoCompletar As Integer = 0

        Temporada.Fields.SelectAll()
        Temporada.Id_Temporada.Where.EqualCondition(id_Temporada)

        Dim dataTable As Data.DataTable = connection.executeSelect_DT(queryBuilder.SelectQuery(Temporada))
        If dataTable.Rows.Count > 0 Then
            ObjectBuilder.CreateObject(dataTable, 0, Temporada)
            tbx_NombreTemporada.Text = Temporada.Nombre.Value
            ddl_Prioridad.SelectedValue = Temporada.Prioridad.Value
            id_Actual = Temporada.Id_Temporada.Value
        End If
    End Sub

    Protected Function insertTemporada() As Boolean
        Dim Temporada As Temporada
        Try
            Temporada = New Temporada(tbx_NombreTemporada.Text, ddl_Prioridad.selectedValue)
            connection.executeInsert(queryBuilder.InsertQuery(Temporada))
            Return True
        Catch exc As Exception
            MyMaster.MostrarMensaje(exc.Message, True)

            Return False
        End Try
    End Function

    Protected Function updateTemporada(ByVal id_Temporada As Integer) As Boolean
        Dim Temporada As Temporada
        Try
            Temporada = New Temporada(tbx_NombreTemporada.Text, ddl_Prioridad.selectedValue)

            Temporada.Id_Temporada.Where.EqualCondition(id_Temporada)
            connection.executeUpdate(queryBuilder.UpdateQuery(Temporada))
            Return True
        Catch exc As Exception
            MyMaster.MostrarMensaje(exc.Message, True)

            Return False
        End Try
    End Function

    Protected Function deleteTemporada(ByVal id_Temporada As Integer) As Boolean
        Dim Temporada As New Temporada
        Try
            Temporada.Id_Temporada.Where.EqualCondition(id_Temporada)
            connection.executeDelete(queryBuilder.DeleteQuery(Temporada))
            Return True
        Catch exc As Exception
            MyMaster.MostrarMensaje(exc.Message, True)

            Return False
        End Try
    End Function

    'Prioridades
    Sub cargarPrioridades(ByVal limite As Integer)
        ddl_Prioridad.Items.Clear()
        For counter = 0 To limite
            ddl_Prioridad.Items.Add(counter + 1)
        Next
        ddl_Prioridad.SelectedIndex = ddl_Prioridad.Items.Count - 1
    End Sub

    'RangoTemporada
    Protected Sub loadRangoTemporada(ByVal Id_Temporada As Integer)
        Dim RangoTemporada As New RangoTemporada

        RangoTemporada.Fields.SelectAll()
        RangoTemporada.Id_Temporada.Where.EqualCondition(Id_Temporada)
        Dim dataTable As Data.DataTable = connection.executeSelect_DT(queryBuilder.SelectQuery(RangoTemporada))
        'dg_Rangos.DataSource = dataTable
        'dg_Rangos.DataBind()

        dataTable.Rows.Add()

        If dataTable.Rows.Count > 0 Then
            Dim results_RangoTemporada As ArrayList = ObjectBuilder.TransformDataTable(dataTable, RangoTemporada)

            'dg_Rangos.DataKeyNames = New String() {RangoTemporada.Id_Temporada.Name, RangoTemporada.Ordinal.Name}
            dg_Rangos.DataKeyField = RangoTemporada.Ordinal.Name
            dg_Rangos.DataSource = dataTable
            dg_Rangos.DataBind()

            For counter As Integer = 0 To dg_Rangos.Items.Count - 1
                Dim lbl_RangoOrden As Label = dg_Rangos.Items(counter).FindControl("lbl_RangoOrden")
                Dim bdp_FechaInicio As BasicFrame.WebControls.BDPLite = dg_Rangos.Items(counter).FindControl("bdp_FechaInicio")
                Dim bdp_FechaFin As BasicFrame.WebControls.BDPLite = dg_Rangos.Items(counter).FindControl("bdp_FechaFin")

                Dim act_RangoTemporada As RangoTemporada = results_RangoTemporada(counter)

                If act_RangoTemporada.Ordinal.IsValid Then
                    lbl_RangoOrden.Attributes("InDB") = "True"
                    lbl_RangoOrden.Text = act_RangoTemporada.Ordinal.Value
                    bdp_FechaInicio.SelectedDate = act_RangoTemporada.fecha_inicio.ValueLocalized
                    bdp_FechaFin.SelectedDate = act_RangoTemporada.fecha_final.ValueLocalized
                Else
                    lbl_RangoOrden.Attributes("InDB") = "False"
                    lbl_RangoOrden.Text = "Nuevo"
                    bdp_FechaInicio.SelectedDate = Nothing
                    bdp_FechaFin.SelectedDate = Nothing
                End If
            Next
        End If
    End Sub

    Protected Function insertRangoTemporada(ByVal id_Temporada As Integer) As Boolean
        For counter As Integer = 0 To dg_Rangos.Items.Count - 1
            Dim lbl_RangoOrden As Label = dg_Rangos.Items(counter).FindControl("lbl_RangoOrden")
            Dim bdp_FechaInicio As BasicFrame.WebControls.BDPLite = dg_Rangos.Items(counter).FindControl("bdp_FechaInicio")
            Dim bdp_FechaFin As BasicFrame.WebControls.BDPLite = dg_Rangos.Items(counter).FindControl("bdp_FechaFin")

            Dim RangoTemporada As New RangoTemporada

            Try
                RangoTemporada.Id_Temporada.Value = id_Temporada
                RangoTemporada.Ordinal.Value = counter

                'Verifica de donde tomar informacion
                If bdp_FechaInicio.SelectedValue IsNot Nothing And bdp_FechaFin.SelectedValue IsNot Nothing Then
                    RangoTemporada.fecha_inicio.ValueLocalized = bdp_FechaInicio.SelectedDate
                    RangoTemporada.fecha_final.ValueLocalized = bdp_FechaFin.SelectedDate
                End If

                connection.executeInsert(queryBuilder.InsertQuery(RangoTemporada))
            Catch exc As Exception
                'MyMaster.MostrarMensaje(exc.Message, True)
                Return False
            End Try
        Next
        Return True
    End Function

    Protected Function updateRangoTemporada(ByVal id_Temporada As Integer) As Boolean
        For counter As Integer = 0 To dg_Rangos.Items.Count - 1
            Dim lbl_RangoOrden As Label = dg_Rangos.Items(counter).FindControl("lbl_RangoOrden")
            Dim bdp_FechaInicio As BasicFrame.WebControls.BDPLite = dg_Rangos.Items(counter).FindControl("bdp_FechaInicio")
            Dim bdp_FechaFin As BasicFrame.WebControls.BDPLite = dg_Rangos.Items(counter).FindControl("bdp_FechaFin")

            Dim RangoTemporada As New RangoTemporada
            Try
                'Verifica de donde tomar informacion
                If bdp_FechaInicio.SelectedValue IsNot Nothing And bdp_FechaFin.SelectedValue IsNot Nothing Then
                    RangoTemporada.fecha_inicio.ValueLocalized = bdp_FechaInicio.SelectedDate
                    RangoTemporada.fecha_final.ValueLocalized = bdp_FechaFin.SelectedDate
                End If

                If lbl_RangoOrden.Attributes("InDB") = "True" Then
                    RangoTemporada.Id_Temporada.Where.EqualCondition(id_Temporada)
                    RangoTemporada.Ordinal.Where.EqualCondition(counter)
                    connection.executeUpdate(queryBuilder.UpdateQuery(RangoTemporada))
                Else
                    RangoTemporada.Id_Temporada.Value = id_Temporada
                    RangoTemporada.Ordinal.Value = counter
                    connection.executeInsert(queryBuilder.InsertQuery(RangoTemporada))
                    lbl_RangoOrden.Attributes("InDB") = "True"
                End If
            Catch exc As Exception
                'MyMaster.MostrarMensaje(exc.Message, True)
                Return False
            End Try
        Next
        Return True
    End Function

    Protected Function deleteRangoTemporada(ByVal id_Temporada As Integer, ByVal ordinal As Integer) As Boolean
        Dim RangoTemporada As New RangoTemporada
        Try
            'Dim minimoOrdinal As Integer = dg_Rangos.Rows.Count - 1
            RangoTemporada.Id_Temporada.Where.EqualCondition(id_Temporada)
            'RangoTemporada.Ordinal.Where.GreaterThanCondition(minimoOrdinal)
            RangoTemporada.Ordinal.Where.EqualCondition(ordinal)
            Dim consulta As String = queryBuilder.DeleteQuery(RangoTemporada)
            connection.executeDelete(consulta)
            Return True
        Catch exc As Exception
            MyMaster.MostrarMensaje(exc.Message, True)
            Return False
        End Try
    End Function

    'Eventos
    Protected Sub btn_Salvar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Salvar.Click
        If IsValid() Then
            If insertTemporada() Then
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Salvar, "Temporada", False)


                Dim temporada As New Temporada
                Dim id_Temporada As Integer = connection.lastKey(temporada.TableName, temporada.Id_Temporada.Name)
                insertRangoTemporada(id_Temporada)

                selectTemporada()
       
                ClearInfo(Configuration.EstadoPantalla.CONSULTAR, False)
                loadTemporada(id_Temporada)
                loadRangoTemporada(id_Temporada)

            Else
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Salvar, "Temporada", True)
            End If
        Else
            MyMaster.MostrarMensaje(Resources.Orbecatalog_Resources.CamposRequeridos, False)
        End If
        upd_Contenido.Update()
        upd_Busqueda.Update()
    End Sub

    Protected Sub btn_Modificar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Modificar.Click
        If IsValid() Then
            If updateTemporada(id_Actual) Then
                Me.updateRangoTemporada(id_Actual)
                loadRangoTemporada(id_Actual)
                selectTemporada()
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Modificar, "Temporada", False)
            Else
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Modificar, "Temporada", True)
            End If
        Else
            MyMaster.MostrarMensaje(Resources.Orbecatalog_Resources.CamposRequeridos, False)
        End If
        upd_Busqueda.Update()
    End Sub

    Protected Sub btn_Cancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Cancelar.Click
        ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
        selectTemporada()
        loadRangoTemporada(0)
        upd_Contenido.Update()
        upd_Busqueda.Update()
    End Sub

    Protected Sub btn_Eliminar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Eliminar.Click
        Dim id_Temporada As Integer = id_Actual
        If deleteTemporada(id_Temporada) Then
            ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
            selectTemporada()
            loadRangoTemporada(0)
            MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Eliminar, "Temporada", False)
        Else
            MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Eliminar, "Temporada", True)
        End If
        upd_Contenido.Update()
        upd_Busqueda.Update()
    End Sub

    Protected Sub dg_Temporada_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dg_Temporada.EditCommand
        ClearInfo(Configuration.EstadoPantalla.CONSULTAR, True)
        Me.EditCommandDataGrid(dg_Temporada, e.Item.ItemIndex)
        loadTemporada(dg_Temporada.DataKeys(e.Item.ItemIndex))
        loadRangoTemporada(dg_Temporada.DataKeys(e.Item.ItemIndex))
        upd_Contenido.Update()
        upd_Busqueda.Update()
    End Sub

    Protected Sub dg_Temporada_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dg_Temporada.PageIndexChanged
        dg_Temporada.CurrentPageIndex = e.NewPageIndex
        selectTemporada()
        Me.PageIndexChange(dg_Temporada)
        upd_Busqueda.Update()
    End Sub

    'Protected Sub gv_Rangos_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gv_Rangos.RowCommand
    '    Dim ordinal As Integer = -1
    '    ordinal = gv_Rangos.DataKeys(e.CommandArgument)(RangoTemporada.Ordinal.Name)
    'End Sub

    Protected Sub dg_Rangos_DeleteCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dg_Rangos.DeleteCommand
        Dim RangoTemporada As New RangoTemporada
        Dim ordinal As Integer = -1
        Try
            ordinal = dg_Rangos.DataKeys(e.Item.ItemIndex)
        Catch ex As Exception

        End Try

        deleteRangoTemporada(id_Actual, ordinal)
        loadRangoTemporada(id_Actual)
        upd_Contenido.Update()
        upd_Busqueda.Update()
    End Sub
End Class
