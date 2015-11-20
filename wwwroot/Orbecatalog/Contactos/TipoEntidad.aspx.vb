Imports Orbelink.DBHandler
Imports Orbelink.Entity.Entidades

Partial Class _TipoEntidad
    Inherits PageBaseClass

    Const codigo_pantalla As String = "CO-03"
    Const level As Integer = 2

    'Pagina
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddScripts(level)
        securityHandler.VerifyPantalla(codigo_pantalla, level)

        If Not IsPostBack Then
            dg_TipoEntidad.Attributes.Add("SelectedIndex", -1)
            dg_TipoEntidad.Attributes.Add("SelectedPage", -1)
            ClearInfo(Configuration.EstadoPantalla.NORMAL, False)
            securityHandler.VerifyActions(btn_Salvar, btn_Modificar, btn_Eliminar)
            SetThemeProperties()
            SetControlProperties()

            selectTipoEntidad(securityHandler.ClearanceLevel)
        End If
    End Sub

    Protected Sub ClearInfo(ByVal estado As Configuration.EstadoPantalla, ByVal clearInfo As Boolean)
        If clearInfo Then
            vld_tbx_NombreTipoEntidad.IsValid = True
            tbx_NombreTipoEntidad.Text = ""
            tbx_DescripcionTipoEntidad.Text = ""
            tbx_NombreTipoEntidad.ToolTip = ""
            Me.ClearDataGrid(dg_TipoEntidad, clearInfo)
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
        tbx_NombreTipoEntidad.Attributes.Add("maxlength", 50)
        tbx_NombreTipoEntidad.Attributes.Add("onkeypress", "showTextProgress(this)")
        tbx_DescripcionTipoEntidad.Attributes.Add("maxlength", 100)
        tbx_DescripcionTipoEntidad.Attributes.Add("onkeypress", "showTextProgress(this)")
    End Sub

    'TipoEntidad
    Protected Sub selectTipoEntidad(ByVal nivel As Integer)
        Dim dataset As Data.DataSet
        Dim tipoEntidad As New TipoEntidad

        tipoEntidad.Fields.SelectAll()
        tipoEntidad.Nivel.Where.LessThanCondition(nivel)
        queryBuilder.Orderby.Add(tipoEntidad.Nombre)
        dataset = connection.executeSelect(queryBuilder.SelectQuery(tipoEntidad))

        If dataset.Tables.Count > 0 Then
            If dataset.Tables(0).Rows.Count > 0 Then
                dg_TipoEntidad.DataSource = dataset
                dg_TipoEntidad.DataKeyField = tipoEntidad.Id_TipoEntidad.Name
                dg_TipoEntidad.DataBind()

                'Llena el grid
                Dim offset As Integer = dg_TipoEntidad.CurrentPageIndex * dg_TipoEntidad.PageSize
                Dim counter As Integer
                Dim result_TipoEntidad As ArrayList = ObjectBuilder.TransformDataTable(dataset.Tables(0), tipoEntidad)
                For counter = 0 To dg_TipoEntidad.Items.Count - 1
                    Dim act_TipoEntidad As TipoEntidad = result_TipoEntidad(offset + counter)
                    Dim lbl_NombreAtributo As Label = dg_TipoEntidad.Items(counter).FindControl("lbl_Nombre")
                    Dim lbl_Descripcion As Label = dg_TipoEntidad.Items(counter).FindControl("lbl_Descripcion")
                    'Dim btn_EditarTipoEntidad As ImageButton = dg_TipoEntidad.Items(counter).FindControl("btn_EditarTipoEntidad")

                    lbl_NombreAtributo.Text = act_TipoEntidad.Nombre.Value
                    lbl_Descripcion.Text = act_TipoEntidad.Descripcion.Value

                    'btn_EditarTipoEntidad.Attributes.Add(act_TipoEntidad.Id_TipoEntidad.Name, act_TipoEntidad.Id_TipoEntidad.Value)
                    'btn_EditarTipoEntidad.Attributes.Add("indice", counter)

                    'Javascript
                    dg_TipoEntidad.Items(counter).Attributes.Add("onmouseover", "setRowFocus(this, " & counter & ", false)")
                    dg_TipoEntidad.Items(counter).Attributes.Add("onmouseout", "unsetRowFocus(this, " & counter & ", false)")
                Next
                lbl_ResultadoElementos.Text = contadorElementos(offset, dg_TipoEntidad.PageSize, dataset.Tables(0).Rows.Count)
                dg_TipoEntidad.Visible = True
                btn_BorrarSeleccionados.Visible = True
            Else
                lbl_ResultadoElementos.Text = contadorElementos(0, 0, 0)
                dg_TipoEntidad.Visible = False
                btn_BorrarSeleccionados.Visible = False
            End If
        End If
    End Sub

    Protected Function loadTipoEntidad(ByVal Id_TipoEntidad As Integer) As Boolean
        Dim dataSet As Data.DataSet
        Dim tipoEntidad As New TipoEntidad

        tipoEntidad.Fields.SelectAll()
        tipoEntidad.Id_TipoEntidad.Where.EqualCondition(Id_TipoEntidad)
        dataSet = connection.executeSelect(queryBuilder.SelectQuery(tipoEntidad))
        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables(0).Rows.Count > 0 Then
                ObjectBuilder.CreateObject(dataSet.Tables(0), 0, tipoEntidad)

                tbx_NombreTipoEntidad.Text = tipoEntidad.Nombre.Value
                tbx_NombreTipoEntidad.Attributes.Add(tipoEntidad.Id_TipoEntidad.Name, tipoEntidad.Id_TipoEntidad.Value)
                tbx_DescripcionTipoEntidad.Text = tipoEntidad.Descripcion.Value
                ddl_Nivel.SelectedIndex = tipoEntidad.Nivel.Value
                Return True
            End If
        End If
        Return False
    End Function

    Protected Function insertTipoEntidad() As Boolean
        Dim tipoEntidad As TipoEntidad
        Try
            tipoEntidad = New TipoEntidad(tbx_NombreTipoEntidad.Text, tbx_DescripcionTipoEntidad.Text, ddl_Nivel.SelectedIndex)
            connection.executeInsert(queryBuilder.InsertQuery(tipoEntidad))
            Return True
        Catch exc As Exception
            Me.MyMaster.MostrarMensaje(exc.Message, True)
            Return False
        End Try
    End Function

    Protected Function updateTipoEntidad(ByVal id_tipoEntidad As Integer) As Boolean
        Dim tipoEntidad As TipoEntidad
        Try
            tipoEntidad = New TipoEntidad(tbx_NombreTipoEntidad.Text, tbx_DescripcionTipoEntidad.Text, ddl_Nivel.SelectedIndex)
            tipoEntidad.Id_TipoEntidad.Where.EqualCondition(id_tipoEntidad)
            connection.executeUpdate(queryBuilder.UpdateQuery(tipoEntidad))
            Return True
        Catch exc As Exception
            Me.MyMaster.MostrarMensaje(exc.Message, True)
            Return False
        End Try
    End Function

    Protected Function deleteTipoEntidad(ByVal id_TipoEntidad As Integer) As Boolean
        Dim tipoEntidad As New TipoEntidad
        Try
            tipoEntidad.Id_TipoEntidad.Where.EqualCondition(id_TipoEntidad)
            connection.executeDelete(queryBuilder.DeleteQuery(tipoEntidad))
            Return True
        Catch exc As Exception
            Me.MyMaster.MostrarMensaje(exc.Message, True)
            Return False
        End Try
    End Function

    'Atributos_TipoEntidad
    Protected Function deleteAtributos_TipoEntidad(ByVal id_TipoEntidad As Integer) As Boolean
        Dim Atributos_TipoEntidad As New Atributos_TipoEntidad
        Try
            Atributos_TipoEntidad.id_tipoEntidad.Where.EqualCondition(id_TipoEntidad)
            connection.executeDelete(queryBuilder.DeleteQuery(Atributos_TipoEntidad))
            Return True
        Catch exc As Exception
            Me.MyMaster.MostrarMensaje(exc.Message, True)
            Return False
        End Try
    End Function

    'Ejecucion
    Public Sub executeDelete(ByVal id_TipoEntidad As Integer)

        'If deletePublicacion_TipoEntidad(id_TipoEntidad) Then
        If deleteAtributos_TipoEntidad(id_TipoEntidad) Then
            If deleteTipoEntidad(id_TipoEntidad) Then
                selectTipoEntidad(securityHandler.ClearanceLevel)
                ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
                Me.MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Eliminar, "Tipo de entidad", False)
            Else
                Me.MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Eliminar, "Tipo de entidad", True)
            End If
        Else
            Me.MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Eliminar, "atributos por tipo de entidad", True)
        End If
        'Else
        'Me.MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Eliminar, "publicaciones por tipo de entidad", True)
        'End If

        upd_Contenido.Update()
        upd_Busqueda.Update()
    End Sub

    'Eventos
    Protected Sub btn_Salvar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Salvar.Click
        If IsValid() Then
            If insertTipoEntidad() Then
                ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
                selectTipoEntidad(securityHandler.ClearanceLevel)
                Me.MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Salvar, "Tipo de entidad", False)
            Else
                Me.MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Salvar, "tipo de entidad", True)
            End If
        Else
            Me.MyMaster.MostrarMensaje(Resources.Orbecatalog_Resources.CamposRequeridos, True)
        End If
        upd_Contenido.Update()
        upd_Busqueda.Update()
    End Sub

    Protected Sub btn_Modificar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Modificar.Click
        If IsValid() Then
            If updateTipoEntidad(tbx_NombreTipoEntidad.Attributes("Id_TipoEntidad")) Then
                'ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
                selectTipoEntidad(securityHandler.ClearanceLevel)
                Me.MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Modificar, "Tipo de entidad", False)
            Else
                Me.MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Modificar, "tipo de entidad", True)
            End If
        Else
            Me.MyMaster.MostrarMensaje(Resources.Orbecatalog_Resources.CamposRequeridos, True)
        End If
        upd_Busqueda.Update()
    End Sub

    Protected Sub btn_Cancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Cancelar.Click
        ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
        upd_Contenido.Update()
        upd_Busqueda.Update()
    End Sub

    Protected Sub btn_Eliminar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Eliminar.Click
        executeDelete(tbx_NombreTipoEntidad.Attributes("Id_TipoEntidad"))
    End Sub

    Protected Sub dg_TipoEntidad_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dg_TipoEntidad.EditCommand
        ClearInfo(Configuration.EstadoPantalla.CONSULTAR, True)
        Me.EditCommandDataGrid(dg_TipoEntidad, e.Item.ItemIndex)
        loadTipoEntidad(dg_TipoEntidad.DataKeys(e.Item.ItemIndex))
        upd_Contenido.Update()
        upd_Busqueda.Update()
    End Sub

    Protected Sub dg_TipoEntidad_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dg_TipoEntidad.PageIndexChanged
        dg_TipoEntidad.CurrentPageIndex = e.NewPageIndex
        selectTipoEntidad(securityHandler.ClearanceLevel)
        Me.PageIndexChange(dg_TipoEntidad)
        upd_Busqueda.Update()
    End Sub

    Protected Sub btn_BorrarSeleccionados_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_BorrarSeleccionados.Click
        Dim borrados As Integer = 0
        For counter As Integer = 0 To dg_TipoEntidad.Items.Count - 1
            Dim chk_Control As CheckBox = dg_TipoEntidad.Items(counter).FindControl("chk_Control")
            Dim id_TipoEntidad As Integer = dg_TipoEntidad.DataKeys.Item(counter)

            If chk_Control.Checked Then
                If deleteTipoEntidad(id_TipoEntidad) Then
                    borrados += 1
                End If
            End If
        Next
        Me.MyMaster.MostrarMensaje("Tipos de entidad borrados: " & borrados, IIf(borrados = 0, True, False))
        ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
        dg_TipoEntidad.CurrentPageIndex = 0
        selectTipoEntidad(securityHandler.ClearanceLevel)
    End Sub

    Protected Sub chk_TodosGrupos_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        checkTodos(dg_TipoEntidad, sender.checked)
    End Sub
End Class
