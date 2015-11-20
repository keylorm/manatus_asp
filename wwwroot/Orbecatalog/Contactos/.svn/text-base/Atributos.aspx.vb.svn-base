Imports Orbelink.DBHandler
Imports Orbelink.Entity.Entidades

Partial Class _Entidades_Atributos
    Inherits PageBaseClass

    Const codigo_pantalla As String = "CO-04"
    Const level As Integer = 2

    'Pagina

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddScripts(level)

        securityHandler.VerifyPantalla(codigo_pantalla, level)
        If Not IsPostBack Then

            dg_Atributos.Attributes.Add("SelectedIndex", -1)
            dg_Atributos.Attributes.Add("SelectedPage", -1)
            ClearInfo(Configuration.EstadoPantalla.NORMAL, False)
            securityHandler.VerifyActions(btn_Salvar, btn_Modificar, btn_Eliminar)
            SetThemeProperties()
            SetControlProperties()

            selectAtributos()
        End If
    End Sub

    Protected Sub ClearInfo(ByVal estado As Configuration.EstadoPantalla, ByVal clearInfo As Boolean)
        If clearInfo Then
            vld_tbx_NombreAtributo.IsValid = True
            tbx_NombreAtributo.Text = ""
            tbx_NombreAtributo.ToolTip = ""
            chk_AutoCompletar.Checked = False
            tbx_Descripcion.Text = ""
            Me.ClearDataGrid(dg_Atributos, clearInfo)
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
        Dim atributos As New Atributos_Para_Entidades
        tbx_NombreAtributo.Attributes.Add("maxlength", atributos.Nombre.Length)
        tbx_NombreAtributo.Attributes.Add("onkeypress", "showTextProgress(this)")
        tbx_Descripcion.Attributes.Add("maxlength", atributos.Descripcion.Length)
        tbx_Descripcion.Attributes.Add("onkeypress", "showTextProgress(this)")
    End Sub

    'Atributos
    Protected Function selectAtributos() As Boolean
        Dim dataSet As Data.DataSet
        Dim Atributos_Para_Entidades As New Atributos_Para_Entidades

        Atributos_Para_Entidades.Fields.SelectAll()
        queryBuilder.Orderby.Add(Atributos_Para_Entidades.Nombre)
        dataSet = connection.executeSelect(queryBuilder.SelectQuery(Atributos_Para_Entidades))
        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables(0).Rows.Count > 0 Then
                dg_Atributos.DataSource = dataSet
                dg_Atributos.DataKeyField = Atributos_Para_Entidades.Id_atributo.Name
                dg_Atributos.DataBind()

                'Llena el grid
                Dim offset As Integer = dg_Atributos.CurrentPageIndex * dg_Atributos.PageSize
                Dim counter As Integer
                Dim results As ArrayList = ObjectBuilder.TransformDataTable(dataSet.Tables(0), Atributos_Para_Entidades)
                For counter = 0 To dg_Atributos.Items.Count - 1
                    Dim act_Atributo As Atributos_Para_Entidades = results(offset + counter)
                    Dim lbl_NombreAtributo As Label = dg_Atributos.Items(counter).FindControl("lbl_NombreAtributo")
                    Dim lbl_autoCompletar As Label = dg_Atributos.Items(counter).FindControl("lbl_AutoCompletar")

                    If act_Atributo.AutoCompletar.Value = "1" Then
                        lbl_autoCompletar.Text = "SI"
                    Else
                        lbl_autoCompletar.Text = "NO"
                    End If
                    lbl_NombreAtributo.Text = act_Atributo.Nombre.Value

                    'Javascript
                    dg_Atributos.Items(counter).Attributes.Add("onmouseover", "setRowFocus(this, " & counter & ", false)")
                    dg_Atributos.Items(counter).Attributes.Add("onmouseout", "unsetRowFocus(this, " & counter & ", false)")
                Next
                lbl_ResultadoElementos.Text = contadorElementos(offset, dg_Atributos.PageSize, dataSet.Tables(0).Rows.Count)
                dg_Atributos.Visible = True
            Else
                lbl_ResultadoElementos.Text = contadorElementos(0, 0, 0)
                dg_Atributos.Visible = False
            End If
        End If
    End Function

    Protected Function loadAtributo(ByVal id_atributo As Integer) As Boolean
        Dim dataSet As Data.DataSet
        Dim Atributos_Para_Entidades As New Atributos_Para_Entidades
        Dim loaded As Boolean = False

        Atributos_Para_Entidades.Fields.SelectAll()
        Atributos_Para_Entidades.Id_atributo.Where.EqualCondition(id_atributo)
        dataSet = connection.executeSelect(queryBuilder.SelectQuery(Atributos_Para_Entidades))

        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables(0).Rows.Count > 0 Then
                ObjectBuilder.CreateObject(dataSet.Tables(0), 0, Atributos_Para_Entidades)

                tbx_NombreAtributo.Text = Atributos_Para_Entidades.Nombre.Value
                tbx_NombreAtributo.ToolTip = Atributos_Para_Entidades.Id_atributo.Value
                tbx_Descripcion.Text = Atributos_Para_Entidades.Descripcion.Value
                If Atributos_Para_Entidades.AutoCompletar.Value = "1" Then
                    chk_AutoCompletar.Checked = True
                Else
                    chk_AutoCompletar.Checked = False
                End If
                loaded = True
            End If
        End If
        Return loaded
    End Function

    Protected Function updateAtributo(ByVal id_atributo As Integer) As Boolean
        Dim atributo As New Atributos_Para_Entidades
        Dim autoCompletar As Integer = 0

        If chk_AutoCompletar.Checked Then
            autoCompletar = 1
        End If
        Try
            atributo.Nombre.Value = tbx_NombreAtributo.Text
            atributo.AutoCompletar.Value = autoCompletar
            atributo.Descripcion.Value = tbx_Descripcion.Text
            atributo.Id_atributo.Where.EqualCondition(id_atributo)
            connection.executeUpdate(queryBuilder.UpdateQuery(atributo))
            Return True
        Catch exc As Exception
            MyMaster.mostrarMensaje(exc.Message, True)

            Return False
        End Try
    End Function

    Protected Function insertAtributo() As Boolean
        Dim atributo As Atributos_Para_Entidades
        Dim autoCom As Integer = 0
        If chk_AutoCompletar.Checked Then
            autoCom = 1
        End If
        Try
            atributo = New Atributos_Para_Entidades(tbx_NombreAtributo.Text, autoCom, tbx_Descripcion.Text)
            connection.executeInsert(queryBuilder.InsertQuery(atributo))
            Return True
        Catch exc As Exception
            MyMaster.mostrarMensaje(exc.Message, True)

            Return False
        End Try
    End Function

    Protected Function deleteAtributo(ByVal id_atributo As Integer) As Boolean
        Dim atributo As New Atributos_Para_Entidades
        Try
            atributo.Id_atributo.Where.EqualCondition(id_atributo)
            connection.executeDelete(queryBuilder.DeleteQuery(atributo))
            Return True
        Catch exc As Exception
            MyMaster.mostrarMensaje(exc.Message, True)

            Return False
        End Try
    End Function

    'Eventos
    Protected Sub btn_Salvar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Salvar.Click

        If IsValid Then
            If insertAtributo() Then
                ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
                selectAtributos()
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Salvar, "Atributo", False)
            Else
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Salvar, "atributo", True)
            End If
        Else
            MyMaster.MostrarMensaje(Resources.Orbecatalog_Resources.CamposRequeridos, False)
        End If
        upd_Contenido.Update()
        upd_Busqueda.Update()
    End Sub

    Protected Sub btn_Modificar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Modificar.Click

        If IsValid() Then
            If updateAtributo(tbx_NombreAtributo.ToolTip) Then
                'ClearInfo(Configuration.EstadoPantalla.NORMAL, False)
                selectAtributos()
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Modificar, "Atributo", False)
            Else
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Modificar, "atributo", True)
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

        If deleteAtributo(tbx_NombreAtributo.ToolTip) Then
            ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
            selectAtributos()
        End If
        upd_Contenido.Update()
        upd_Busqueda.Update()
    End Sub

    Protected Sub dg_Atributos_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dg_Atributos.EditCommand
        ClearInfo(Configuration.EstadoPantalla.CONSULTAR, True)
        Me.EditCommandDataGrid(dg_Atributos, e.Item.ItemIndex)
        loadAtributo(dg_Atributos.DataKeys(e.Item.ItemIndex))
        upd_Contenido.Update()
        upd_Busqueda.Update()
    End Sub

    Protected Sub dg_Atributos_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dg_Atributos.PageIndexChanged
        dg_Atributos.CurrentPageIndex = e.NewPageIndex
        selectAtributos()
        Me.PageIndexChange(dg_Atributos)
        upd_Busqueda.Update()
    End Sub

    Protected Sub chk_TodosGrupos_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        checkTodos(dg_Atributos, sender.checked)
    End Sub

    Protected Sub btn_BorrarSeleccionados_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_BorrarSeleccionados.Click

        Dim borrados As Integer = 0
        For counter As Integer = 0 To dg_Atributos.Items.Count - 1
            Dim chk_Control As CheckBox = dg_Atributos.Items(counter).FindControl("chk_Control")
            Dim id_Atributo As Integer = dg_Atributos.DataKeys.Item(counter)

            If chk_Control.Checked Then
                If deleteAtributo(id_Atributo) Then
                    borrados += 1
                End If
            End If
        Next
        MyMaster.mostrarMensaje("Tipos de entidad borrados: " & borrados, False)
        ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
        dg_Atributos.CurrentPageIndex = 0
        selectAtributos()
    End Sub
End Class
