Imports Orbelink.DBHandler
Imports Orbelink.Entity.Reservaciones

Partial Class Orbecatalog_Reservacion_TipoEstadoReservacion
    Inherits PageBaseClass

    Const codigo_pantalla As String = "RS-07"
    Const level As Integer = 2

    'Pagina
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddScripts(level)
        securityHandler.VerifyPantalla(codigo_pantalla, level)
        If Not IsPostBack Then
            Me.Title = Resources.Orbecatalog_Resources.Orbecatalog & " - " & Resources.Reservaciones_Resources.TipoEstadoReservacion_Pantalla

            dg_TipoEstadoReservacion.Attributes.Add("SelectedIndex", -1)
            dg_TipoEstadoReservacion.Attributes.Add("SelectedPage", -1)
            ClearInfo(Configuration.EstadoPantalla.NORMAL, False)
            securityHandler.VerifyActions(btn_Salvar, btn_Modificar, btn_Eliminar)

            selectTipoEstadoReservacion()
        End If
    End Sub

    Protected Sub ClearInfo(ByVal estado As Configuration.EstadoPantalla, ByVal clearInfo As Boolean)
        If clearInfo Then
            vld_Nombre.IsValid = True
            tbx_NombreTipoEstadoReservacion.Text = ""
            id_Actual = 0
            chk_Terminador.Checked = False
            chk_RepresentaCancelado.Checked = False
            Me.ClearDataGrid(dg_TipoEstadoReservacion, clearInfo)
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

    'TipoEstadoReservacion
    Protected Sub selectTipoEstadoReservacion()
        Dim TipoEstadoReservacion As New TipoEstadoReservacion

        TipoEstadoReservacion.Fields.SelectAll()
        queryBuilder.Orderby.Add(TipoEstadoReservacion.Nombre)
        Dim dataTable As Data.DataTable = connection.executeSelect_DT(queryBuilder.SelectQuery(TipoEstadoReservacion))

        If dataTable.Rows.Count > 0 Then
            dg_TipoEstadoReservacion.DataSource = dataTable
            dg_TipoEstadoReservacion.DataKeyField = TipoEstadoReservacion.Id_TipoEstadoReservacion.Name
            dg_TipoEstadoReservacion.DataBind()

            'Llena el grid
            Dim offset As Integer = dg_TipoEstadoReservacion.CurrentPageIndex * dg_TipoEstadoReservacion.PageSize
            Dim counter As Integer
            Dim result_TipoEstadoReservacion As ArrayList = ObjectBuilder.TransformDataTable(dataTable, TipoEstadoReservacion)
            For counter = 0 To dg_TipoEstadoReservacion.Items.Count - 1
                Dim act_TipoEstadoReservacion As TipoEstadoReservacion = result_TipoEstadoReservacion(offset + counter)
                Dim lbl_NombreAtributo As Label = dg_TipoEstadoReservacion.Items(counter).FindControl("lbl_Nombre")
                lbl_NombreAtributo.Text = act_TipoEstadoReservacion.Nombre.Value

                'Javascript
                dg_TipoEstadoReservacion.Items(counter).Attributes.Add("onmouseover", "setRowFocus(this, " & counter & ", false)")
                dg_TipoEstadoReservacion.Items(counter).Attributes.Add("onmouseout", "unsetRowFocus(this, " & counter & ", false)")
            Next
            lbl_ResultadoElementos.Text = contadorElementos(offset, dg_TipoEstadoReservacion.PageSize, dataTable.Rows.Count)
            dg_TipoEstadoReservacion.Visible = True
        Else
            lbl_ResultadoElementos.Text = contadorElementos(0, 0, 0)
            dg_TipoEstadoReservacion.Visible = False
        End If
    End Sub

    Protected Sub loadTipoEstadoReservacion(ByVal id_TipoEstadoReservacion As Integer)
        Dim TipoEstadoReservacion As New TipoEstadoReservacion
        Dim autoCompletar As Integer = 0
        TipoEstadoReservacion.Fields.SelectAll()
        TipoEstadoReservacion.Id_TipoEstadoReservacion.Where.EqualCondition(id_TipoEstadoReservacion)

        Dim dataTable As Data.DataTable = connection.executeSelect_DT(queryBuilder.SelectQuery(TipoEstadoReservacion))
        If dataTable.Rows.Count > 0 Then
            ObjectBuilder.CreateObject(dataTable, 0, TipoEstadoReservacion)
            tbx_NombreTipoEstadoReservacion.Text = TipoEstadoReservacion.Nombre.Value
            chk_RepresentaCancelado.Checked = TipoEstadoReservacion.RepresentaCancelado.Value
            chk_RepresentaReservado.Checked = TipoEstadoReservacion.RepresentaReservado.Value
            chk_Terminador.Checked = TipoEstadoReservacion.Terminador.Value
            chk_RepresentaInicial.Checked = TipoEstadoReservacion.Inicial.Value
            id_Actual = TipoEstadoReservacion.Id_TipoEstadoReservacion.Value
            upd_Contenido.Update()
        End If
    End Sub

    Protected Function insertTipoEstadoReservacion() As Boolean
        Dim TipoEstadoReservacion As TipoEstadoReservacion
        Try
            TipoEstadoReservacion = New TipoEstadoReservacion(tbx_NombreTipoEstadoReservacion.Text, IIf(chk_Terminador.Checked, 1, 0), IIf(chk_RepresentaCancelado.Checked, 1, 0), IIf(chk_RepresentaReservado.Checked, 1, 0), IIf(chk_RepresentaInicial.Checked, 1, 0), IIf(chk_bloqueado.Checked, 1, 0))
            connection.executeInsert(queryBuilder.InsertQuery(TipoEstadoReservacion))
            Return True
        Catch exc As Exception
            MyMaster.MostrarMensaje(exc.Message, True)

            Return False
        End Try
    End Function

    Protected Function updateTipoEstadoReservacion(ByVal id_TipoEstadoReservacion As Integer) As Boolean
        Dim TipoEstadoReservacion As TipoEstadoReservacion
        Try
            If revisaInicial() Then
                TipoEstadoReservacion = New TipoEstadoReservacion(tbx_NombreTipoEstadoReservacion.Text, IIf(chk_Terminador.Checked, 1, 0), IIf(chk_RepresentaCancelado.Checked, 1, 0), IIf(chk_RepresentaReservado.Checked, 1, 0), IIf(chk_RepresentaInicial.Checked, 1, 0), IIf(chk_bloqueado.Checked, 1, 0))
                TipoEstadoReservacion.Id_TipoEstadoReservacion.Where.EqualCondition(id_TipoEstadoReservacion)
                connection.executeUpdate(queryBuilder.UpdateQuery(TipoEstadoReservacion))
                Return True
            Else
                MyMaster.MostrarMensaje("Existe mas de un estado inicial", True)
                chk_RepresentaInicial.Checked = False
            End If
        Catch exc As Exception
            MyMaster.MostrarMensaje(exc.Message, True)

            Return False
        End Try
    End Function


    Protected Function revisaInicial() As Boolean
        Dim unico As Boolean = True
        If chk_RepresentaInicial.Checked Then
            Dim tipoestado As New TipoEstadoReservacion
            Dim dataSet As New Data.DataSet
            tipoestado.Id_TipoEstadoReservacion.ToSelect = True
            tipoestado.Inicial.Where.EqualCondition(1)
            Dim consultas As String = queryBuilder.SelectQuery(tipoestado)
            dataSet = connection.executeSelect(consultas)
            If dataSet.Tables.Count > 0 Then
                If dataSet.Tables(0).Rows.Count > 0 Then
                    unico = False
                End If
            End If
        End If
        Return unico
    End Function

    Protected Function deleteTipoEstadoReservacion(ByVal id_TipoEstadoReservacion As Integer) As Boolean
        Dim TipoEstadoReservacion As New TipoEstadoReservacion
        Try
            TipoEstadoReservacion.Id_TipoEstadoReservacion.Where.EqualCondition(id_TipoEstadoReservacion)
            connection.executeDelete(queryBuilder.DeleteQuery(TipoEstadoReservacion))
            Return True
        Catch exc As Exception
            MyMaster.MostrarMensaje(exc.Message, True)

            Return False
        End Try
    End Function

    'Eventos
    Protected Sub btn_Salvar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Salvar.Click
        If IsValid() Then
            If insertTipoEstadoReservacion() Then
                ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
                selectTipoEstadoReservacion()
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Salvar, "Tipo de Estado", False)
            Else
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Salvar, "tipo de Estado", True)
            End If
        Else
            MyMaster.MostrarMensaje(Resources.Orbecatalog_Resources.CamposRequeridos, False)
        End If
        upd_Contenido.Update()
        upd_Busqueda.Update()
    End Sub

    Protected Sub btn_Modificar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Modificar.Click

        If IsValid() Then
            If updateTipoEstadoReservacion(id_Actual) Then
                selectTipoEstadoReservacion()
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Modificar, "Tipo de Estado", False)
            Else
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Modificar, "tipo de Estado", True)
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
        Dim id_TipoEstadoReservacion As Integer = id_Actual
        If deleteTipoEstadoReservacion(id_TipoEstadoReservacion) Then
            ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
            selectTipoEstadoReservacion()
            MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Eliminar, "Tipo de Estado", False)
        Else
            MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Eliminar, "Tipo de Estado", True)
        End If
        upd_Contenido.Update()
        upd_Busqueda.Update()
    End Sub

    Protected Sub dg_TipoEstadoReservacion_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dg_TipoEstadoReservacion.EditCommand
        ClearInfo(Configuration.EstadoPantalla.CONSULTAR, True)
        Me.EditCommandDataGrid(dg_TipoEstadoReservacion, e.Item.ItemIndex)
        loadTipoEstadoReservacion(dg_TipoEstadoReservacion.DataKeys(e.Item.ItemIndex))
        upd_Contenido.Update()
        upd_Busqueda.Update()
    End Sub

    Protected Sub dg_TipoEstadoReservacion_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dg_TipoEstadoReservacion.PageIndexChanged
        dg_TipoEstadoReservacion.CurrentPageIndex = e.NewPageIndex
        selectTipoEstadoReservacion()
        Me.PageIndexChange(dg_TipoEstadoReservacion)
        upd_Busqueda.Update()
    End Sub
End Class
