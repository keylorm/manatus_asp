Imports Orbelink.DBHandler
Imports Orbelink.Entity.Productos

Partial Class _TipoRelacion_Productos
    Inherits PageBaseClass

    Const codigo_pantalla As String = "PR-13"
    Const level As Integer = 2

    'Pagina
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddScripts(level)
        securityHandler.VerifyPantalla(codigo_pantalla, level)
        If Not IsPostBack Then
            Me.Title = Resources.Orbecatalog_Resources.Orbecatalog & " - " & Resources.Productos_Resources.TiposRelacion_Pantalla

            securityHandler.VerifyActions(btn_Salvar, btn_Modificar, btn_Eliminar)
            resetCampos(Configuration.EstadoPantalla.NORMAL)
            selectTipoRelacion()
        End If
    End Sub

    Protected Sub resetCampos(ByVal estado As Configuration.EstadoPantalla)
        vld_tbx_NombreTipoRelacion_Productos.IsValid = True
        tbx_NombreTipoRelacion_Productos.Text = ""
        tbx_NombreTipoRelacion_Productos.ToolTip = ""
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

    'TipoRelacion
    Protected Sub selectTipoRelacion()
        Dim dataSet As Data.DataSet
        Dim TipoRelacion_Productos As New TipoRelacion_Productos
        Dim result As String
        TipoRelacion_Productos.Fields.SelectAll()
        result = queryBuilder.SelectQuery(TipoRelacion_Productos)
        dataSet = connection.executeSelect(result)

        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables(0).Rows.Count > 0 Then
                dg_TipoRelacion.DataSource = dataSet
                dg_TipoRelacion.DataKeyField = TipoRelacion_Productos.Id_TipoRelacion.Name
                dg_TipoRelacion.DataBind()

                'Llena el grid
                Dim offset As Integer = dg_TipoRelacion.CurrentPageIndex * dg_TipoRelacion.PageSize
                Dim counter As Integer
                Dim resultados As ArrayList = ObjectBuilder.TransformDataTable(dataSet.Tables(0), TipoRelacion_Productos)
                For counter = 0 To dg_TipoRelacion.Items.Count - 1
                    Dim actual As TipoRelacion_Productos = resultados(offset + counter)
                    Dim lbl_Nombre As Label = dg_TipoRelacion.Items(counter).FindControl("lbl_Nombre")
                    lbl_Nombre.Text = actual.Nombre.Value
                Next
                dg_TipoRelacion.Visible = True
            Else
                dg_TipoRelacion.Visible = False
            End If
        End If
    End Sub

    Protected Sub cargarParaModificar(ByVal indice As Integer)
        Dim dataSet As Data.DataSet
        Dim TipoRelacion_Productos As New TipoRelacion_Productos
        Dim autoCompletar As Integer = 0
        Dim sqlQuery As String

        TipoRelacion_Productos.Fields.SelectAll()
        TipoRelacion_Productos.Id_TipoRelacion.Where.EqualCondition(indice)
        sqlQuery = queryBuilder.SelectQuery(TipoRelacion_Productos)
        dataSet = connection.executeSelect(sqlQuery)

        tbx_NombreTipoRelacion_Productos.Text = dataSet.Tables(0).Rows(0).Item(TipoRelacion_Productos.Nombre.Name)
        tbx_NombreTipoRelacion_Productos.ToolTip = dataSet.Tables(0).Rows(0).Item(TipoRelacion_Productos.Id_TipoRelacion.Name)
    End Sub

    Protected Sub updateTipoRelacion()
        Dim TipoRelacion_Productos As TipoRelacion_Productos
        Dim sqlQuery As String
        Try
            TipoRelacion_Productos = New TipoRelacion_Productos(tbx_NombreTipoRelacion_Productos.Text)
            TipoRelacion_Productos.Id_TipoRelacion.Where.EqualCondition(tbx_NombreTipoRelacion_Productos.ToolTip)
            sqlQuery = queryBuilder.UpdateQuery(TipoRelacion_Productos)
            connection.executeUpdate(sqlQuery)
        Catch exc As Exception
            MyMaster.mostrarMensaje(exc.Message, True)

        End Try
    End Sub

    Protected Sub insertarTipoRelacion(ByVal theTextBox As TextBox)
        Dim sqlQuery As String
        Dim TipoRelacion_Productos As TipoRelacion_Productos
        Try
            TipoRelacion_Productos = New TipoRelacion_Productos(theTextBox.Text)
            sqlQuery = queryBuilder.InsertQuery(TipoRelacion_Productos)
            connection.executeInsert(sqlQuery)
        Catch exc As Exception
            MyMaster.mostrarMensaje(exc.Message, True)

        End Try
    End Sub

    Protected Sub deleteTipoRelacion()
        Dim TipoRelacion_Productos As New TipoRelacion_Productos
        Dim sqlQuery As String
        Try
            TipoRelacion_Productos.Id_TipoRelacion.Where.EqualCondition(tbx_NombreTipoRelacion_Productos.ToolTip)
            sqlQuery = queryBuilder.DeleteQuery(TipoRelacion_Productos)
            connection.executeDelete(sqlQuery)
        Catch exc As Exception
            MyMaster.mostrarMensaje(exc.Message, True)

        End Try
    End Sub

    'Eventos
    Protected Sub btn_Modificar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Modificar.Click
        If IsValid() Then
            updateTipoRelacion()
            resetCampos(Configuration.EstadoPantalla.NORMAL)
            selectTipoRelacion()
        End If
    End Sub

    Protected Sub btn_Cancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Cancelar.Click
        resetCampos(Configuration.EstadoPantalla.NORMAL)
    End Sub

    Protected Sub btn_Eliminar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Eliminar.Click
        deleteTipoRelacion()
        resetCampos(Configuration.EstadoPantalla.NORMAL)
        selectTipoRelacion()
    End Sub

    Protected Sub btn_Salvar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Salvar.Click
        If IsValid() Then
            insertarTipoRelacion(tbx_NombreTipoRelacion_Productos)
            resetCampos(Configuration.EstadoPantalla.NORMAL)
            selectTipoRelacion()
        End If
    End Sub

    Protected Sub dg_TipoRelacion_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dg_TipoRelacion.EditCommand
        Dim indiceTabla As Integer = dg_TipoRelacion.DataKeys(e.Item.ItemIndex)
        resetCampos(Configuration.EstadoPantalla.CONSULTAR)
        Me.cargarParaModificar(indiceTabla)
    End Sub

    Protected Sub dg_Entidades_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dg_TipoRelacion.PageIndexChanged
        dg_TipoRelacion.CurrentPageIndex = e.NewPageIndex
        selectTipoRelacion()
    End Sub
End Class
