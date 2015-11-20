Imports Orbelink.Entity.Productos

Partial Class Controls_DireccionesProducto
    Inherits System.Web.UI.UserControl

    Dim queryBuilder As Orbelink.DBHandler.QueryBuilder
    Dim connection As Orbelink.DBHandler.SQLServer
    Dim securityHandler As Orbelink.Control.Security.SecurityHandler
    Dim codigo_pantalla As String
    Dim MyMaster As Orbelink.Orbecatalog6.MasterPageBaseClass

    Dim _tempMyIdProducto As Integer
    Protected Property MyIdProducto() As Integer
        Get
            If _tempMyIdProducto <= 0 Then
                If ViewState("vs_MyIdProducto") IsNot Nothing Then
                    _tempMyIdProducto = ViewState("vs_MyIdProducto")
                Else
                    ViewState.Add("vs_MyIdProducto", 0)
                    _tempMyIdProducto = 0
                End If
            End If
            Return _tempMyIdProducto
        End Get
        Set(ByVal value As Integer)
            _tempMyIdProducto = value
            ViewState("vs_MyIdProducto") = _tempMyIdProducto
        End Set
    End Property

    Public Sub SetVariables(ByVal theQueryBuilder As Orbelink.DBHandler.QueryBuilder, _
                           ByVal theConnection As Orbelink.DBHandler.SQLServer, _
                           ByVal theSecurityHandler As Orbelink.Control.Security.SecurityHandler, _
                           ByVal theCodigo_pantalla As String, _
                           ByVal theMaster As Orbelink.Orbecatalog6.MasterPageBaseClass)
        queryBuilder = theQueryBuilder
        connection = theConnection
        securityHandler = theSecurityHandler
        codigo_pantalla = theCodigo_pantalla
        MyMaster = theMaster
    End Sub

    Public Sub SetIdProducto(ByVal theId_Producto As Integer)
        MyIdProducto = theId_Producto
    End Sub

    'Puntos_Producto
    Public Sub selectPuntos_Producto(ByVal id_Producto As Integer)
        Dim Puntos_Producto As New Puntos_Producto

        Puntos_Producto.Fields.SelectAll()
        Puntos_Producto.Id_Producto.Where.EqualCondition(id_Producto)
        queryBuilder.From.Add(Puntos_Producto)
        Dim resultTable As Data.DataTable = connection.executeSelect_DT(queryBuilder.RelationalSelectQuery)

        dg_Puntos.DataSource = resultTable
        dg_Puntos.DataKeyField = Puntos_Producto.Id_Producto.Name
        dg_Puntos.DataBind()

        For counter As Integer = 0 To resultTable.Rows.Count - 1
            Dim act_Puntos_Producto As Puntos_Producto = Orbelink.DBHandler.ObjectBuilder.CreateDBTable_BySelectIndex(resultTable.Rows(counter), Puntos_Producto)

            Dim tbx_Descripcion As TextBox = dg_Puntos.Items(counter).FindControl("tbx_Descripcion")
            Dim lbl_Latitud As Label = dg_Puntos.Items(counter).FindControl("lbl_Latitud")
            Dim lbl_Longitud As Label = dg_Puntos.Items(counter).FindControl("lbl_Longitud")
            Dim tbx_Nombre As TextBox = dg_Puntos.Items(counter).FindControl("tbx_Nombre")

            tbx_Descripcion.Text = act_Puntos_Producto.Descr_Punto.Value
            lbl_Latitud.Text = act_Puntos_Producto.Lat.Value
            lbl_Longitud.Text = act_Puntos_Producto.Lon.Value
            tbx_Nombre.Text = act_Puntos_Producto.Nombre.Value
            tbx_Nombre.ToolTip = act_Puntos_Producto.Id_Punto.Value

            'Javascript
            dg_Puntos.Items(counter).Attributes.Add("onmouseover", "setRowFocus(this, " & counter & ", false)")
            dg_Puntos.Items(counter).Attributes.Add("onmouseout", "unsetRowFocus(this, " & counter & ", false)")
        Next
    End Sub

    Private Function deletePuntos_Producto(ByVal Id_Punto As Integer) As Boolean
        Dim Puntos_Producto As New Puntos_Producto
        Try
            Puntos_Producto.Id_Punto.Where.EqualCondition(Id_Punto)
            connection.executeDelete(queryBuilder.DeleteQuery(Puntos_Producto))
            'Orbelink.FileHandler.ImagesHandler.DeleteImageAndThumb(Configuration.prod_Images, Id_Punto & ".jpg")
            Return True
        Catch exc As Exception
            MyMaster.MostrarMensaje(exc.Message, True)
            Return False
        End Try
    End Function

    Private Function updatePuntos_Producto(ByVal id_Punto As Integer, ByVal id_Producto As Integer, ByVal nombre As String, ByVal Descripcion As String) As Boolean
        Dim Puntos_Producto As New Puntos_Producto
        Try
            Puntos_Producto.Id_Producto.Value = id_Producto
            Puntos_Producto.Nombre.Value = nombre
            Puntos_Producto.Descr_Punto.Value = Descripcion
            Puntos_Producto.Id_Punto.Where.EqualCondition(id_Punto)
            connection.executeUpdate(queryBuilder.UpdateQuery(Puntos_Producto))
            Return True
        Catch exc As Exception
            MyMaster.MostrarMensaje(exc.Message, True)
            Return False
        End Try
    End Function

    'Eventos
    Protected Sub btn_Salvar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Salvar.Click
        If MyIdProducto > 0 Then
            'Dim Puntos_Producto As New Puntos_Producto
            For counter As Integer = 0 To dg_Puntos.Items.Count - 1
                Dim tbx_Descripcion As TextBox = dg_Puntos.Items(counter).FindControl("tbx_Descripcion")
                Dim tbx_Nombre As TextBox = dg_Puntos.Items(counter).FindControl("tbx_Nombre")

                If tbx_Nombre.ToolTip.Length > 0 Then
                    If Not updatePuntos_Producto(tbx_Nombre.ToolTip, MyIdProducto, tbx_Nombre.Text, tbx_Descripcion.Text) Then
                        MyMaster.MostrarMensaje("Error al actualizar la " & counter + 1 & "fila.", False)
                        Exit For
                    End If
                End If
            Next
            MyMaster.MostrarMensaje("Salvado exitoso.", False)
            selectPuntos_Producto(MyIdProducto)
            upd_Direcciones.Update()
        End If
    End Sub

    Protected Sub dg_Puntos_DeleteCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dg_Puntos.DeleteCommand
        Dim tbx_Nombre As TextBox = dg_Puntos.Items(e.Item.ItemIndex).FindControl("tbx_Nombre")
        If tbx_Nombre.ToolTip.Length > 0 Then
            If deletePuntos_Producto(tbx_Nombre.ToolTip) Then
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Eliminar, "Punto", False)
            Else
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Eliminar, "el punto", True)
            End If
            selectPuntos_Producto(MyIdProducto)
            upd_Direcciones.Update()
        End If
    End Sub
End Class
