Imports Orbelink.DBHandler
Imports Orbelink.Entity.Archivos
Imports Orbelink.Orbecatalog6

Partial Class _Archivo_Configuration
    Inherits PageBaseClass

    Const codigo_pantalla As String = "OR-05"
    Const level As Integer = 1

    'Pagina
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddScripts(level)
        securityHandler.VerifyPantalla(codigo_pantalla, level)

        If Not IsPostBack Then
            dg_Archivo_Configuration.Attributes.Add("SelectedIndex", -1)
            dg_Archivo_Configuration.Attributes.Add("SelectedPage", -1)
            ClearInfo(Configuration.EstadoPantalla.NORMAL, False)
            securityHandler.VerifyActions(btn_Salvar, btn_Modificar, btn_Eliminar)

            selectArchivo_Configuration()
        End If
    End Sub

    Protected Sub ClearInfo(ByVal estado As Configuration.EstadoPantalla, ByVal clearInfo As Boolean)
        If clearInfo Then
            vld_Nombre.IsValid = True
            tbx_NombreArchivo_Configuration.Text = ""
            tbx_NombreArchivo_Configuration.ToolTip = ""
            tbx_anchoI.Text = ""
            tbx_altoI.Text = ""
            tbx_anchoT.Text = ""
            tbx_AltoT.Text = ""
            tbx_Compresion.Text = ""
            Me.ClearDataGrid(dg_Archivo_Configuration, clearInfo)
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

    'Archivo_Configuration
    Protected Sub selectArchivo_Configuration()
        Dim dataSet As Data.DataSet
        Dim Archivo_Configuration As New Archivo_Configuration

        Archivo_Configuration.Fields.SelectAll()
        queryBuilder.Orderby.Add(Archivo_Configuration.Nombre)
        dataSet = connection.executeSelect(queryBuilder.SelectQuery(Archivo_Configuration))

        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables(0).Rows.Count > 0 Then
                dg_Archivo_Configuration.DataSource = dataSet
                dg_Archivo_Configuration.DataKeyField = Archivo_Configuration.Id_Configuration.Name
                dg_Archivo_Configuration.DataBind()

                'Llena el grid
                Dim offset As Integer = dg_Archivo_Configuration.CurrentPageIndex * dg_Archivo_Configuration.PageSize
                Dim counter As Integer
                Dim result_Archivo_Configuration As ArrayList = ObjectBuilder.TransformDataTable(dataSet.Tables(0), Archivo_Configuration)
                For counter = 0 To dg_Archivo_Configuration.Items.Count - 1
                    Dim act_Archivo_Configuration As Archivo_Configuration = result_Archivo_Configuration(offset + counter)

                    Dim lbl_NombreAtributo As Label = dg_Archivo_Configuration.Items(counter).FindControl("lbl_Nombre")
                    lbl_NombreAtributo.Text = act_Archivo_Configuration.Nombre.Value

                    'Javascript
                    dg_Archivo_Configuration.Items(counter).Attributes.Add("onmouseover", "setRowFocus(this, " & counter & ", false)")
                    dg_Archivo_Configuration.Items(counter).Attributes.Add("onmouseout", "unsetRowFocus(this, " & counter & ", false)")
                Next
                lbl_ResultadoElementos.Text = contadorElementos(offset, dg_Archivo_Configuration.PageSize, dataSet.Tables(0).Rows.Count)
                dg_Archivo_Configuration.Visible = True
            Else
                lbl_ResultadoElementos.Text = contadorElementos(0, 0, 0)
                dg_Archivo_Configuration.Visible = False
            End If
        End If
    End Sub

    Protected Sub loadArchivo_Configuration(ByVal Id_Configuration As Integer)
        Dim dataSet As Data.DataSet
        Dim Archivo_Configuration As New Archivo_Configuration
        Dim autoCompletar As Integer = 0

        Archivo_Configuration.Fields.SelectAll()
        Archivo_Configuration.Id_Configuration.Where.EqualCondition(Id_Configuration)

        dataSet = connection.executeSelect(queryBuilder.SelectQuery(Archivo_Configuration))
        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables(0).Rows.Count > 0 Then
                ObjectBuilder.CreateObject(dataSet.Tables(0), 0, Archivo_Configuration)
                tbx_NombreArchivo_Configuration.Text = Archivo_Configuration.Nombre.Value
                tbx_NombreArchivo_Configuration.ToolTip = Archivo_Configuration.Id_Configuration.Value
                tbx_anchoI.Text = Archivo_Configuration.ImgWidth.Value
                tbx_altoI.Text = Archivo_Configuration.ImgHeight.Value
                tbx_anchoT.Text = Archivo_Configuration.ThumWidth.Value
                tbx_AltoT.Text = Archivo_Configuration.ThumHeight.Value
                tbx_Compresion.Text = Archivo_Configuration.Compresion.Value
            End If
        End If
    End Sub

    Protected Function insertArchivo_Configuration() As Boolean
        Dim Archivo_Configuration As Archivo_Configuration
        Try
            Dim autoPlay As String = "0"
            Archivo_Configuration = New Archivo_Configuration(tbx_NombreArchivo_Configuration.Text, tbx_anchoI.Text, tbx_altoI.Text, tbx_anchoT.Text, tbx_AltoT.Text, tbx_Compresion.Text, autoPlay, "", 10, 0)
            connection.executeInsert(queryBuilder.InsertQuery(Archivo_Configuration))
            Return True
        Catch exc As Exception
            MyMaster.mostrarMensaje(exc.Message, True)

            Return False
        End Try
    End Function

    Protected Function updateArchivo_Configuration(ByVal Id_Configuration As Integer) As Boolean
        Dim Archivo_Configuration As Archivo_Configuration
        Try
            Dim autoPlay As String = "0"

            Archivo_Configuration = New Archivo_Configuration(tbx_NombreArchivo_Configuration.Text, tbx_anchoI.Text, tbx_altoI.Text, tbx_anchoT.Text, tbx_AltoT.Text, tbx_Compresion.Text, autoPlay, "", 10, 0)
            Archivo_Configuration.Id_Configuration.Where.EqualCondition(Id_Configuration)
            connection.executeUpdate(queryBuilder.UpdateQuery(Archivo_Configuration))
            Return True
        Catch exc As Exception
            MyMaster.mostrarMensaje(exc.Message, True)

            Return False
        End Try
    End Function

    Protected Function deleteArchivo_Configuration(ByVal Id_Configuration As Integer) As Boolean
        Dim Archivo_Configuration As New Archivo_Configuration
        Try
            Archivo_Configuration.Id_Configuration.Where.EqualCondition(Id_Configuration)
            connection.executeDelete(queryBuilder.DeleteQuery(Archivo_Configuration))
            Return True
        Catch exc As Exception
            MyMaster.mostrarMensaje(exc.Message, True)

            Return False
        End Try
    End Function

    'Eventos
    Protected Sub btn_Salvar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Salvar.Click

        If IsValid() Then
            If insertArchivo_Configuration() Then
                ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
                selectArchivo_Configuration()
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Salvar, "Configuracion de archivo", False)
            Else
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Salvar, "Configuracion de archivo.", True)
            End If
        Else
            MyMaster.MostrarMensaje(Resources.Orbecatalog_Resources.CamposRequeridos, False)
        End If
        upd_Contenido.Update()
        upd_Busqueda.Update()
    End Sub

    Protected Sub btn_Modificar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Modificar.Click

        If IsValid() Then
            If updateArchivo_Configuration(tbx_NombreArchivo_Configuration.ToolTip) Then
                selectArchivo_Configuration()
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Modificar, "Configuracion de archivo", False)
            Else
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Modificar, "Configuracion de archivo", True)
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

        Dim Id_Configuration As Integer = tbx_NombreArchivo_Configuration.ToolTip
        If deleteArchivo_Configuration(Id_Configuration) Then
            ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
            selectArchivo_Configuration()
            MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Eliminar, "Configuracion de archivo", False)
        Else
            MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Eliminar, "Configuracion de archivo", True)
            MyMaster.concatenarMensaje("<br />No puede haber ninguna Configuracion de archivo de este tipo para borrarlo.", False)
        End If
        upd_Contenido.Update()
        upd_Busqueda.Update()
    End Sub

    Protected Sub dg_Archivo_Configuration_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dg_Archivo_Configuration.EditCommand
        ClearInfo(Configuration.EstadoPantalla.CONSULTAR, True)
        Me.EditCommandDataGrid(dg_Archivo_Configuration, e.Item.ItemIndex)
        loadArchivo_Configuration(dg_Archivo_Configuration.DataKeys(e.Item.ItemIndex))
        upd_Contenido.Update()
        upd_Busqueda.Update()
    End Sub

    Protected Sub dg_Archivo_Configuration_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dg_Archivo_Configuration.PageIndexChanged
        dg_Archivo_Configuration.CurrentPageIndex = e.NewPageIndex
        selectArchivo_Configuration()
        Me.PageIndexChange(dg_Archivo_Configuration)
        upd_Busqueda.Update()
    End Sub
End Class
