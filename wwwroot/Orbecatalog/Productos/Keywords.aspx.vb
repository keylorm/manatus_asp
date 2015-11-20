Imports Orbelink.DBHandler
Imports Orbelink.Entity.Productos

Partial Class _Keywords
    Inherits PageBaseClass

    Const codigo_pantalla As String = "PR-06"
    Const level As Integer = 2

    'Pagina
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddScripts(level)
        securityHandler.VerifyPantalla(codigo_pantalla, level)
        If Not IsPostBack Then
            Me.Title = Resources.Orbecatalog_Resources.Orbecatalog & " - " & Resources.Productos_Resources.Keywords_Pantalla

            dg_Keywords.Attributes.Add("SelectedIndex", -1)
            dg_Keywords.Attributes.Add("SelectedPage", -1)
            ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
            securityHandler.VerifyActions(btn_Salvar, btn_Modificar, btn_Eliminar)

            selectKeywords()
        End If
    End Sub

    Protected Sub ClearInfo(ByVal estado As Configuration.EstadoPantalla, ByVal clearInfo As Boolean)
        If clearInfo Then
            vld_tbx_NombreKeyword.IsValid = True
            tbx_NombreKeyword.Text = ""
            tbx_NombreKeyword.ToolTip = ""
            chk_Visible.Checked = False
            Me.ClearDataGrid(dg_Keywords, clearInfo)
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

    'Keywords
    Protected Function selectKeywords() As Boolean
        Dim dataSet As Data.DataSet
        Dim Keywords As New Keywords

        Keywords.Fields.SelectAll()
        queryBuilder.Orderby.Add(Keywords.Nombre)
        dataSet = connection.executeSelect(queryBuilder.SelectQuery(Keywords))
        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables(0).Rows.Count > 0 Then
                dg_Keywords.DataSource = dataSet
                dg_Keywords.DataKeyField = Keywords.Id_Keyword.Name
                dg_Keywords.DataBind()

                'Llena el grid
                Dim offset As Integer = dg_Keywords.CurrentPageIndex * dg_Keywords.PageSize
                Dim counter As Integer
                Dim results As ArrayList = ObjectBuilder.TransformDataTable(dataSet.Tables(0), Keywords)
                For counter = 0 To dg_Keywords.Items.Count - 1
                    Dim act_Atributo As Keywords = results(offset + counter)
                    Dim lbl_Nombre As Label = dg_Keywords.Items(counter).FindControl("lbl_Nombre")
                    Dim lbl_Visible As Label = dg_Keywords.Items(counter).FindControl("lbl_Visible")

                    If act_Atributo.Visible.Value = "1" Then
                        lbl_Visible.Text = "SI"
                    Else
                        lbl_Visible.Text = "NO"
                    End If
                    lbl_Nombre.Text = act_Atributo.Nombre.Value

                    'Javascript
                    dg_Keywords.Items(counter).Attributes.Add("onmouseover", "setRowFocus(this, " & counter & ", false)")
                    dg_Keywords.Items(counter).Attributes.Add("onmouseout", "unsetRowFocus(this, " & counter & ", false)")
                Next
                lbl_ResultadoElementos.Text = contadorElementos(offset, dg_Keywords.PageSize, dataSet.Tables(0).Rows.Count)
                dg_Keywords.Visible = True
            Else
                lbl_ResultadoElementos.Text = contadorElementos(0, 0, 0)
                dg_Keywords.Visible = False
            End If
        Else
            dg_Keywords.Visible = False
        End If
    End Function

    Protected Sub loadKeywords(ByVal Id_Keyword As Integer)
        Dim dataSet As Data.DataSet
        Dim Keywords As New Keywords

        Keywords.Fields.SelectAll()
        Keywords.Id_Keyword.Where.EqualCondition(Id_Keyword)
        dataSet = connection.executeSelect(queryBuilder.SelectQuery(Keywords))

        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables(0).Rows.Count > 0 Then
                ObjectBuilder.CreateObject(dataSet.Tables(0), 0, Keywords)

                tbx_NombreKeyword.Text = Keywords.Nombre.Value
                tbx_NombreKeyword.ToolTip = Keywords.Id_Keyword.Value
                If Keywords.Visible.Value = "1" Then
                    chk_Visible.Checked = True
                Else
                    chk_Visible.Checked = False
                End If
            End If
        End If
    End Sub

    Protected Function updateKeyword(ByVal Id_Keyword As Integer) As Boolean
        Dim Keywords As New Keywords
        Dim esVisible As Integer = 0
        If chk_Visible.Checked Then
            esVisible = 1
        End If
        Try
            Keywords.Nombre.Value = tbx_NombreKeyword.Text
            Keywords.Visible.Value = esVisible
            Keywords.Id_Keyword.Where.EqualCondition(Id_Keyword)
            connection.executeUpdate(queryBuilder.UpdateQuery(Keywords))
            Return True
        Catch exc As Exception
            MyMaster.mostrarMensaje(exc.Message, True)

            Return False
        End Try
    End Function

    Protected Function insertKeyword() As Boolean
        Dim Keywords As Keywords
        Dim esVisible As Integer = 0

        If chk_Visible.Checked Then
            esVisible = 1
        End If
        Try
            Keywords = New Keywords(tbx_NombreKeyword.Text, esVisible)
            connection.executeInsert(queryBuilder.InsertQuery(Keywords))
            Return True
        Catch exc As Exception
            MyMaster.mostrarMensaje(exc.Message, True)

            Return False
        End Try
    End Function

    Protected Function deleteKeyword(ByVal Id_Keyword As Integer) As Boolean
        Dim Keywords As New Keywords
        Try
            Keywords.Id_Keyword.Where.EqualCondition(Id_Keyword)
            connection.executeDelete(queryBuilder.DeleteQuery(Keywords))
            Return True
        Catch exc As Exception
            MyMaster.mostrarMensaje(exc.Message, True)

            Return False
        End Try
    End Function

    'Eventos
    Protected Sub btn_Salvar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Salvar.Click

        If IsValid Then
            If insertKeyword() Then
                ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
                selectKeywords()
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Salvar, "Keyword", False)
            Else
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Salvar, "Keyword", True)
            End If
        Else
            MyMaster.mostrarMensaje("Algun campo necesario está vacio o erroneo.", False)
        End If
        upd_Contenido.Update()
        upd_Busqueda.Update()
    End Sub

    Protected Sub btn_Modificar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Modificar.Click

        If IsValid() Then
            If updateKeyword(tbx_NombreKeyword.ToolTip) Then
                'ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
                selectKeywords()
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Modificar, "Keyword", False)
            Else
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Modificar, "Keyword", True)
            End If
        Else
            MyMaster.mostrarMensaje("Algun campo necesario está vacio o erroneo.", False)
        End If
        upd_Busqueda.Update()
    End Sub

    Protected Sub btn_Cancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Cancelar.Click
        ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
        upd_Contenido.Update()
        upd_Busqueda.Update()
    End Sub

    Protected Sub btn_Eliminar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Eliminar.Click

        If deleteKeyword(tbx_NombreKeyword.ToolTip) Then
            ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
            selectKeywords()
            MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Eliminar, "Keyword", False)
        Else
            MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Eliminar, "Keyword", True)
        End If
        upd_Contenido.Update()
        upd_Busqueda.Update()
    End Sub

    Protected Sub dg_Keywords_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dg_Keywords.EditCommand
        ClearInfo(Configuration.EstadoPantalla.CONSULTAR, True)
        Me.EditCommandDataGrid(dg_Keywords, e.Item.ItemIndex)
        loadKeywords(dg_Keywords.DataKeys(e.Item.ItemIndex))
        upd_Contenido.Update()
        upd_Busqueda.Update()
    End Sub

    Protected Sub dg_Keywords_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dg_Keywords.PageIndexChanged
        dg_Keywords.CurrentPageIndex = e.NewPageIndex
        selectKeywords()
        Me.PageIndexChange(dg_Keywords)
        upd_Busqueda.Update()
    End Sub

End Class
