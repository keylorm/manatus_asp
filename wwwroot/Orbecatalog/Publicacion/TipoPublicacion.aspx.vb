Imports Orbelink.DBHandler
Imports Orbelink.Entity.Publicaciones

Partial Class _TipoPublicacion
    Inherits PageBaseClass

    Const codigo_pantalla As String = "PU-03"
    Const level As Integer = 2

    'Pagina

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddScripts(level)


        securityHandler.VerifyPantalla(codigo_pantalla, level)
        If Not IsPostBack Then

            dg_TipoPublicacion.Attributes.Add("SelectedIndex", -1)
            dg_TipoPublicacion.Attributes.Add("SelectedPage", -1)
            ClearInfo(Configuration.EstadoPantalla.NORMAL, False)
            securityHandler.VerifyActions(btn_Salvar, btn_Modificar, btn_Eliminar)

            selectTipoPublicacion()
        End If
    End Sub

    Protected Sub ClearInfo(ByVal estado As Configuration.EstadoPantalla, ByVal clearInfo As Boolean)
        If clearInfo Then
            vld_Nombre.IsValid = True
            tbx_NombreTipoPublicacion.Text = ""
            tbx_Descripcion.Text = ""
            tbx_NombreTipoPublicacion.ToolTip = ""
            Me.ClearDataGrid(dg_TipoPublicacion, clearInfo)
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

    'TipoPublicacion
    Protected Sub selectTipoPublicacion()
        Dim dataSet As Data.DataSet
        Dim TipoPublicacion As New TipoPublicacion

        TipoPublicacion.Fields.SelectAll()
        queryBuilder.Orderby.Add(TipoPublicacion.Nombre)
        dataSet = connection.executeSelect(queryBuilder.SelectQuery(TipoPublicacion))

        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables(0).Rows.Count > 0 Then
                dg_TipoPublicacion.DataSource = dataSet
                dg_TipoPublicacion.DataKeyField = TipoPublicacion.Id_TipoPublicacion.Name
                dg_TipoPublicacion.DataBind()

                'Llena el grid
                Dim offset As Integer = dg_TipoPublicacion.CurrentPageIndex * dg_TipoPublicacion.PageSize
                Dim counter As Integer
                Dim result_TipoPublicacion As ArrayList = ObjectBuilder.TransformDataTable(dataSet.Tables(0), TipoPublicacion)
                For counter = 0 To dg_TipoPublicacion.Items.Count - 1
                    Dim act_TipoPublicacion As TipoPublicacion = result_TipoPublicacion(offset + counter)
                    Dim lbl_NombreAtributo As Label = dg_TipoPublicacion.Items(counter).FindControl("lbl_Nombre")
                    Dim lbl_Descripcion As Label = dg_TipoPublicacion.Items(counter).FindControl("lbl_Descripcion")
                    lbl_NombreAtributo.Text = act_TipoPublicacion.Nombre.Value
                    lbl_Descripcion.Text = act_TipoPublicacion.Descripcion.Value

                    'Javascript
                    dg_TipoPublicacion.Items(counter).Attributes.Add("onmouseover", "setRowFocus(this, " & counter & ", false)")
                    dg_TipoPublicacion.Items(counter).Attributes.Add("onmouseout", "unsetRowFocus(this, " & counter & ", false)")
                Next
                lbl_ResultadoElementos.Text = contadorElementos(offset, dg_TipoPublicacion.PageSize, dataSet.Tables(0).Rows.Count)
                dg_TipoPublicacion.Visible = True
            Else
                lbl_ResultadoElementos.Text = contadorElementos(0, 0, 0)
                dg_TipoPublicacion.Visible = False
            End If
        End If
    End Sub

    Protected Sub loadTipoPublicacion(ByVal id_TipoPublicacion As Integer)
        Dim dataSet As Data.DataSet
        Dim TipoPublicacion As New TipoPublicacion
        Dim autoCompletar As Integer = 0

        TipoPublicacion.Fields.SelectAll()
        TipoPublicacion.Id_TipoPublicacion.Where.EqualCondition(id_TipoPublicacion)

        dataSet = connection.executeSelect(queryBuilder.SelectQuery(TipoPublicacion))
        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables(0).Rows.Count > 0 Then
                ObjectBuilder.CreateObject(dataSet.Tables(0), 0, TipoPublicacion)
                tbx_NombreTipoPublicacion.Text = TipoPublicacion.Nombre.Value
                tbx_NombreTipoPublicacion.ToolTip = TipoPublicacion.Id_TipoPublicacion.Value
                tbx_Descripcion.Text = TipoPublicacion.Descripcion.Value
            End If
        End If
    End Sub

    Protected Function insertTipoPublicacion() As Boolean
        Dim TipoPublicacion As TipoPublicacion
        Try
            TipoPublicacion = New TipoPublicacion(tbx_NombreTipoPublicacion.Text, tbx_Descripcion.Text)
            connection.executeInsert(queryBuilder.InsertQuery(TipoPublicacion))
            Return True
        Catch exc As Exception
            MyMaster.mostrarMensaje(exc.Message, True)

            Return False
        End Try
    End Function

    Protected Function updateTipoPublicacion(ByVal id_tipoPublicacion As Integer) As Boolean
        Dim TipoPublicacion As TipoPublicacion
        Try
            TipoPublicacion = New TipoPublicacion(tbx_NombreTipoPublicacion.Text, tbx_Descripcion.Text)
            TipoPublicacion.Id_TipoPublicacion.Where.EqualCondition(id_tipoPublicacion)
            connection.executeUpdate(queryBuilder.UpdateQuery(TipoPublicacion))
            Return True
        Catch exc As Exception
            MyMaster.mostrarMensaje(exc.Message, True)

            Return False
        End Try
    End Function

    Protected Function deleteTipoPublicacion(ByVal id_TipoPublicacion As Integer) As Boolean
        Dim TipoPublicacion As New TipoPublicacion
        Try
            TipoPublicacion.Id_TipoPublicacion.Where.EqualCondition(id_TipoPublicacion)
            connection.executeDelete(queryBuilder.DeleteQuery(TipoPublicacion))
            Return True
        Catch exc As Exception
            MyMaster.mostrarMensaje(exc.Message, True)

            Return False
        End Try
    End Function

    'Aprobacion_TipoPublicacion
    Protected Function deleteAprobacion_TipoPublicacion(ByVal id_TipoPublicacion As Integer) As Boolean
        Dim Aprobacion_TipoPublicacion As New Aprobacion_TipoPublicacion
        Try
            Aprobacion_TipoPublicacion.Id_TipoPublicacion.Where.EqualCondition(id_TipoPublicacion)
            connection.executeDelete(queryBuilder.DeleteQuery(Aprobacion_TipoPublicacion))
            Return True
        Catch exc As Exception
            MyMaster.mostrarMensaje(exc.Message, True)

            Return False
        End Try
    End Function

    'Eventos
    Protected Sub btn_Salvar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Salvar.Click

        If IsValid() Then
            If insertTipoPublicacion() Then
                ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
                selectTipoPublicacion()
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Salvar, "Tipo de publicacion", False)
            Else
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Salvar, "tipo de publicacion", True)
            End If
        Else
            MyMaster.MostrarMensaje(Resources.Orbecatalog_Resources.CamposRequeridos, False)
        End If
        upd_Contenido.Update()
        upd_Busqueda.Update()
    End Sub

    Protected Sub btn_Modificar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Modificar.Click

        If IsValid() Then
            If updateTipoPublicacion(tbx_NombreTipoPublicacion.ToolTip) Then
                selectTipoPublicacion()
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Modificar, "Tipo de publicacion", False)
            Else
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Modificar, "tipo de publicacion", True)
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

        Dim id_tipoPublicacion As Integer = tbx_NombreTipoPublicacion.ToolTip
        If deleteAprobacion_TipoPublicacion(id_tipoPublicacion) Then
            If deleteTipoPublicacion(id_tipoPublicacion) Then
                ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
                selectTipoPublicacion()
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Eliminar, "Tipo de publicacion", False)
            Else
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Eliminar, "Tipo de publicacion", True)
                MyMaster.concatenarMensaje("<br />No puede haber ninguna publicacion de este tipo para borrarlo.", False)
            End If
        Else
            MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Eliminar, "aprobacion por tipo de publicacion", True)
        End If
        upd_Contenido.Update()
        upd_Busqueda.Update()
    End Sub

    Protected Sub dg_TipoPublicacion_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dg_TipoPublicacion.EditCommand
        ClearInfo(Configuration.EstadoPantalla.CONSULTAR, True)
        Me.EditCommandDataGrid(dg_TipoPublicacion, e.Item.ItemIndex)
        loadTipoPublicacion(dg_TipoPublicacion.DataKeys(e.Item.ItemIndex))
        upd_Contenido.Update()
        upd_Busqueda.Update()
    End Sub

    Protected Sub dg_TipoPublicacion_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dg_TipoPublicacion.PageIndexChanged
        dg_TipoPublicacion.CurrentPageIndex = e.NewPageIndex
        selectTipoPublicacion()
        Me.PageIndexChange(dg_TipoPublicacion)
        upd_Busqueda.Update()
    End Sub
End Class
