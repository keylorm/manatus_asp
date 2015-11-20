Imports Orbelink.DBHandler
Imports Orbelink.Entity.Facturas

Partial Class _Facturas_TipoFactura
    Inherits PageBaseClass

    Const codigo_pantalla As String = "FC-03"
    Dim level As Integer = 2

    'Pagina

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddScripts(level)

        securityHandler.VerifyPantalla(codigo_pantalla, 1)
        If Not IsPostBack Then
            securityHandler.VerifyActions(btn_Salvar, btn_Modificar, btn_Eliminar)
            resetCampos(Configuration.EstadoPantalla.NORMAL)
            selectTipoFactura()
        End If


    End Sub

    Protected Sub resetCampos(ByVal estado As Integer)
        vld_tbx_NombreTipoFactura.IsValid = True
        tbx_NombreTipoFactura.Text = ""
        tbx_NombreTipoFactura.ToolTip = ""
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

    'Tipo Factura
    Protected Sub selectTipoFactura()
        Dim dataSet As Data.DataSet
        Dim TipoFactura As New TipoFactura
        Dim result As String
        TipoFactura.Fields.SelectAll()
        result = queryBuilder.SelectQuery(TipoFactura)
        dataSet = connection.executeSelect(result)

        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables(0).Rows.Count > 0 Then
                DG_TipoFactura.DataSource = dataSet
                DG_TipoFactura.DataKeyField = TipoFactura.Id_TipoFactura.Name
                DG_TipoFactura.DataBind()

                'Llena el grid
                Dim offset As Integer = DG_TipoFactura.CurrentPageIndex * DG_TipoFactura.PageSize
                Dim counter As Integer
                Dim resultados As ArrayList = ObjectBuilder.TransformDataTable(dataSet.Tables(0), TipoFactura)
                For counter = 0 To DG_TipoFactura.Items.Count - 1
                    Dim actual As TipoFactura = resultados(offset + counter)
                    Dim lbl_NombreAtributo As Label = DG_TipoFactura.Items(counter).FindControl("lbl_Nombre")
                    Dim lbl_Descripcion As Label = DG_TipoFactura.Items(counter).FindControl("lbl_Descripcion")
                    lbl_NombreAtributo.Text = actual.Nombre.Value
                Next
                DG_TipoFactura.Visible = True
            Else
                DG_TipoFactura.Visible = False
            End If
        End If
    End Sub

    Protected Sub cargarParaModificar(ByVal indice As Integer)
        Dim dataSet As Data.DataSet
        Dim TipoFactura As New TipoFactura
        Dim autoCompletar As Integer = 0
        TipoFactura.Fields.SelectAll()
        TipoFactura.Id_TipoFactura.Where.EqualCondition(indice)
        Dim result As String = queryBuilder.SelectQuery(TipoFactura)
        dataSet = connection.executeSelect(result)
        ObjectBuilder.CreateObject(dataSet.Tables(0), 0, TipoFactura)

        tbx_NombreTipoFactura.Text = TipoFactura.Nombre.Value
        tbx_NombreTipoFactura.ToolTip = TipoFactura.Id_TipoFactura.Value
    End Sub

    Protected Sub updateTipoFactura()
        Try
            Dim TipoFactura As New TipoFactura(tbx_NombreTipoFactura.Text, tbx_PorcentajeImpuestos.Text)
            TipoFactura.Id_TipoFactura.Where.EqualCondition(tbx_NombreTipoFactura.ToolTip)
            connection.executeUpdate(queryBuilder.UpdateQuery(TipoFactura))
            resetCampos(Configuration.EstadoPantalla.NORMAL)
        Catch exc As Exception
            MyMaster.mostrarMensaje(exc.Message, True)

        End Try
    End Sub

    Protected Sub insertarTipoFactura(ByVal theTextBox As TextBox)
        Dim sqlQuery As String
        Dim temp As TipoFactura
        Try
            temp = New TipoFactura(theTextBox.Text, tbx_PorcentajeImpuestos.Text)
            sqlQuery = queryBuilder.InsertQuery(temp)
            connection.executeInsert(sqlQuery)
            resetCampos(Configuration.EstadoPantalla.NORMAL)
        Catch exc As Exception
            MyMaster.mostrarMensaje(exc.Message, True)

        End Try
    End Sub

    Protected Sub deleteTipoFactura()
        Dim temp As New TipoFactura
        Dim sqlQuery As String
        Try
            temp.Id_TipoFactura.Where.EqualCondition(tbx_NombreTipoFactura.ToolTip)
            sqlQuery = queryBuilder.DeleteQuery(temp)
            connection.executeDelete(sqlQuery)
            resetCampos(Configuration.EstadoPantalla.NORMAL)
        Catch exc As Exception
            MyMaster.mostrarMensaje(exc.Message, True)

        End Try
    End Sub

    'Eventos
    Protected Sub btn_Modificar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Modificar.Click
        If IsValid() Then
            updateTipoFactura()
            resetCampos(Configuration.EstadoPantalla.NORMAL)
            selectTipoFactura()
        End If
    End Sub

    Protected Sub btn_Cancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Cancelar.Click
        resetCampos(Configuration.EstadoPantalla.NORMAL)
    End Sub

    Protected Sub btn_Eliminar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Eliminar.Click
        deleteTipoFactura()
        selectTipoFactura()
    End Sub

    Protected Sub btn_Salvar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Salvar.Click
        If IsValid() Then
            insertarTipoFactura(tbx_NombreTipoFactura)
            selectTipoFactura()
        End If
    End Sub

    Protected Sub DG_TipoFactura_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DG_TipoFactura.EditCommand
        Dim algo As String = e.ToString()
        Dim indiceTabla As Integer = DG_TipoFactura.DataKeys(e.Item.ItemIndex)
        resetCampos(Configuration.EstadoPantalla.CONSULTAR)
        cargarParaModificar(indiceTabla)
    End Sub

    Protected Sub DG_TipoFactura_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DG_TipoFactura.PageIndexChanged
        DG_TipoFactura.CurrentPageIndex = e.NewPageIndex
        selectTipoFactura()
    End Sub

End Class
