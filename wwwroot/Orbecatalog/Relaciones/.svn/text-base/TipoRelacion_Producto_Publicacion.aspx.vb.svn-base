Imports Orbelink.DBHandler
Partial Class Orbecatalog_Relaciones_TipoRelacion_Producto_Publicacion
    Inherits PageBaseClass

    Const codigo_pantalla As String = "PR-PU"
    Const level As Integer = 2

    Protected Sub Page_Load1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddScripts(level)

        If Not IsPostBack Then
            securityHandler.VerifyPantalla(codigo_pantalla, level)
            resetCampos(Configuration.EstadoPantalla.NORMAL)
            selectTipoRelacion()
        End If
    End Sub

    Protected Sub resetCampos(ByVal estado As Configuration.EstadoPantalla)
        vld_tbx_Producto_Publicacion.IsValid = True
        vld_tbx_Publicacion_Producto.IsValid = True

        tbx_NombreRelacion_Producto_Publicacion.Text = ""
        tbx_NombreRelacion_Producto_Publicacion.ToolTip = ""
        tbx_NombreRelacion_Publicacion_Producto.Text = ""
        tbx_NombreRelacion_Publicacion_Producto.ToolTip = ""
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

    Protected Sub dg_TipoRelacion_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dg_TipoRelacion.EditCommand
        Dim indiceTabla As Integer = dg_TipoRelacion.DataKeys(e.Item.ItemIndex)
        resetCampos(Configuration.EstadoPantalla.CONSULTAR)
        Me.cargarParaModificar(indiceTabla)
    End Sub

    Protected Sub selectTipoRelacion()
        Dim dataSet As Data.DataSet
        Dim relProducto_Publicacion As New TipoRelacion_Producto_Publicacion
        relProducto_Publicacion.Fields.SelectAll()
        Dim consulta As String = queryBuilder.SelectQuery(relProducto_Publicacion)
        dataSet = connection.executeSelect(consulta)

        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables(0).Rows.Count > 0 Then
                dg_TipoRelacion.DataSource = dataSet
                dg_TipoRelacion.DataKeyField = relProducto_Publicacion.Id_TipoRelacion.Name
                dg_TipoRelacion.DataBind()

                'Llena el grid
                Dim offset As Integer = dg_TipoRelacion.CurrentPageIndex * dg_TipoRelacion.PageSize
                Dim counter As Integer
                Dim resultados As ArrayList = ObjectBuilder.TransformDataTable(dataSet.Tables(0), relProducto_Publicacion)
                For counter = 0 To dg_TipoRelacion.Items.Count - 1
                    Dim actual As TipoRelacion_Producto_Publicacion = resultados(offset + counter)
                    Dim lbl_Nombre As Label = dg_TipoRelacion.Items(counter).FindControl("lbl_Nombre")
                    lbl_Nombre.Text = actual.Nombre_Producto_Publicacion.Value & " , " & actual.Nombre_Publicacion_Producto.Value
                Next
                dg_TipoRelacion.Visible = True
            Else
                dg_TipoRelacion.Visible = False
            End If
        End If
    End Sub

    Protected Sub cargarParaModificar(ByVal indice As Integer)
        Dim dataSet As Data.DataSet
        Dim Rel_Producto_Publicacion As New TipoRelacion_Producto_Publicacion
        Dim autoCompletar As Integer = 0
        Dim sqlQuery As String

        Rel_Producto_Publicacion.Fields.SelectAll()
        Rel_Producto_Publicacion.Id_TipoRelacion.Where.EqualCondition(indice)
        sqlQuery = queryBuilder.SelectQuery(Rel_Producto_Publicacion)
        dataSet = connection.executeSelect(sqlQuery)

        tbx_NombreRelacion_Producto_Publicacion.Text = dataSet.Tables(0).Rows(0).Item(Rel_Producto_Publicacion.Nombre_Producto_Publicacion.Name)
        tbx_NombreRelacion_Publicacion_Producto.Text = dataSet.Tables(0).Rows(0).Item(Rel_Producto_Publicacion.Nombre_Publicacion_Producto.Name)
        tbx_NombreRelacion_Producto_Publicacion.ToolTip = dataSet.Tables(0).Rows(0).Item(Rel_Producto_Publicacion.Id_TipoRelacion.Name)
    End Sub


    Protected Sub dg_Productoes_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dg_TipoRelacion.PageIndexChanged
        dg_TipoRelacion.CurrentPageIndex = e.NewPageIndex
        selectTipoRelacion()
    End Sub

    Protected Sub btn_Salvar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Salvar.Click
        If IsValid() Then
            insertarTipoRelacion()
            resetCampos(Configuration.EstadoPantalla.NORMAL)
            selectTipoRelacion()
            MyMaster.mostrarMensaje("Salvado", True)

        End If
    End Sub

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
        MyMaster.mostrarMensaje("Eliminado", True)
    End Sub

    Protected Sub updateTipoRelacion()
        Dim Rel_Publicacion_Producto As TipoRelacion_Producto_Publicacion
        Try
            Rel_Publicacion_Producto = New TipoRelacion_Producto_Publicacion(tbx_NombreRelacion_Producto_Publicacion.Text, tbx_NombreRelacion_Publicacion_Producto.Text)
            Rel_Publicacion_Producto.Id_TipoRelacion.Where.EqualCondition(tbx_NombreRelacion_Producto_Publicacion.ToolTip)
            Dim sqlQuery As String = queryBuilder.UpdateQuery(Rel_Publicacion_Producto)
            connection.executeUpdate(sqlQuery)
        Catch exc As Exception

        End Try
    End Sub

    Protected Sub insertarTipoRelacion()

        Dim Rel_Publicacion_Producto As TipoRelacion_Producto_Publicacion
        Try
            Rel_Publicacion_Producto = New TipoRelacion_Producto_Publicacion(tbx_NombreRelacion_Producto_Publicacion.Text, tbx_NombreRelacion_Publicacion_Producto.Text)
            Dim sqlQuery As String = queryBuilder.InsertQuery(Rel_Publicacion_Producto)
            connection.executeInsert(sqlQuery)
        Catch exc As Exception

        End Try
    End Sub


    Protected Sub deleteTipoRelacion()
        Dim Rel_Publicacion_Producto As New TipoRelacion_Producto_Publicacion
        Dim sqlQuery As String
        Try
            Rel_Publicacion_Producto.Id_TipoRelacion.Where.EqualCondition(tbx_NombreRelacion_Producto_Publicacion.ToolTip)
            sqlQuery = queryBuilder.DeleteQuery(Rel_Publicacion_Producto)
            connection.executeDelete(sqlQuery)
        Catch exc As Exception

        End Try
    End Sub
End Class
