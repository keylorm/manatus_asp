Imports System.IO
Imports Orbelink.DBHandler
Imports Orbelink.Control.Archivos
Imports Orbelink.Entity.Archivos
Imports Orbelink.Control.Entidades
Imports Orbelink.Control.Publicaciones
Imports Orbelink.Control.Productos
Imports Orbelink.Control.CRM

Partial Class _Archivos
    Inherits PageBaseClass

    Const codigo_pantalla As String = "CO-15"
    Const level As Integer = 1
    Dim id_dueno As Integer
    Dim controladora As IControladorArchivos

    'Pagina
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddScripts(level)

        'securityHandler.VerifyPantalla(codigo_pantalla, level)
        controladora = obtenerControladora()
        If controladora Is Nothing Then
            Response.Redirect("Default.aspx")
        End If

        If Not IsPostBack Then
            selectFileTypes()
            selectArchivo_Configuration()
            selectArchivos(id_dueno, cmb_CantiArchivo.SelectedValue)
        End If
    End Sub

    'FileTypes
    Protected Sub selectFileTypes()
        ddl_FileTypes.Items.Add("Imagen")
        ddl_FileTypes.Items.Add("Documento")
        ddl_FileTypes.Items.Add("Video")
        ddl_FileTypes.Items.Add("Audio")
        ddl_FileTypes.Items.Add("Flash")
        ddl_FileTypes.Items.Add("-- Todos --")
        ddl_FileTypes.SelectedIndex = ddl_FileTypes.Items.Count - 1
    End Sub

    'Archivo_dueno
    Protected Sub selectArchivos(ByVal id_dueno As Integer, ByVal cantidadRegistros As Integer)
        Dim dataSet As Data.DataSet
        Dim archivo As New Archivo
        Dim counter As Integer
        Dim resultadosPage As Integer

        dg_Archivo.PageSize = cantidadRegistros
        dataSet = controladora.generarDataSet(id_dueno, ddl_FileTypes.SelectedIndex)

        'Calcula cuantos espacios en blanco dejar
        Dim offset As Integer = dg_Archivo.CurrentPageIndex * dg_Archivo.PageSize
        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables(0).Rows.Count > offset Then
                If dataSet.Tables(0).Rows.Count - offset >= dg_Archivo.PageSize Then
                    resultadosPage = dg_Archivo.PageSize
                Else
                    resultadosPage = dataSet.Tables(0).Rows.Count Mod dg_Archivo.PageSize
                End If
            End If
        Else
            dataSet.Tables.Add()
        End If

        Dim extras As Integer = dg_Archivo.PageSize - resultadosPage
        For counter = 0 To extras - 1
            Dim tempRow As Data.DataRow = dataSet.Tables(0).NewRow
            dataSet.Tables(0).Rows.Add(tempRow)
        Next


        'Asigna el dataset al Archivo, ya con los espacios en blanco
        dg_Archivo.DataSource = dataSet
        dg_Archivo.DataBind()

        'Llena el grid
        Dim resultados_archivo As ArrayList = ObjectBuilder.TransformDataTable(dataSet.Tables(0), archivo)
        For counter = 0 To dg_Archivo.Items.Count - 1
            Dim div_Agregar As HtmlGenericControl = dg_Archivo.Items(counter).FindControl("div_Agregar")
            Dim div_Info As HtmlGenericControl = dg_Archivo.Items(counter).FindControl("div_Info")
            Dim lnk_Archivo As HyperLink = dg_Archivo.Items(counter).FindControl("lnk_Archivo")

            dg_Archivo.Items(counter).Attributes.Add("onmouseover", "setRowFocus(this, " & counter & ", false)")
            dg_Archivo.Items(counter).Attributes.Add("onmouseout", "unsetRowFocus(this, " & counter & ", false)")

            If counter <= resultadosPage - 1 Then
                Dim act_Archivo As Archivo = resultados_archivo(offset + counter)
                Dim valorPrincipal As String = dataSet.Tables(0).Rows(counter).Item("Principal")

                LoadInfoArchivo(act_Archivo, counter, valorPrincipal)
                div_Info.Visible = True
                lnk_Archivo.Visible = True
                div_Agregar.Visible = False
            ElseIf counter = resultadosPage Then
                div_Info.Visible = False
                lnk_Archivo.Visible = False
                div_Agregar.Visible = True
            Else
                div_Info.Visible = False
                lnk_Archivo.Visible = False
                div_Agregar.Visible = True
            End If
        Next

    End Sub

    Protected Sub LoadInfoArchivo(ByRef archivo As Archivo, ByVal indiceDG As Integer, ByVal valorPrincipal As String)
        Dim tbx_NombreArchivo As TextBox = dg_Archivo.Items(indiceDG).FindControl("tbx_NombreArchivo")
        Dim lbl_NombreArchivo As Label = dg_Archivo.Items(indiceDG).FindControl("lbl_NombreArchivo")
        Dim lbl_Info As Label = dg_Archivo.Items(indiceDG).FindControl("lbl_Info")
        Dim chk_Principal As CheckBox = dg_Archivo.Items(indiceDG).FindControl("chk_Principal")
        Dim lbl_comentario As Label = dg_Archivo.Items(indiceDG).FindControl("lbl_comentario")
        Dim lnk_Archivo As HyperLink = dg_Archivo.Items(indiceDG).FindControl("lnk_Archivo")
        Dim img_Archivo As Image = dg_Archivo.Items(indiceDG).FindControl("img_Archivo")
        Dim lbl_ResultadoAccion As Label = dg_Archivo.Items(indiceDG).FindControl("lbl_ResultadoAccion")
        Dim btn_Descargar As Button = dg_Archivo.Items(indiceDG).FindControl("btn_Descargar")

        tbx_NombreArchivo.Text = archivo.Nombre.Value
        tbx_NombreArchivo.ToolTip = archivo.Id_Archivo.Value
        lbl_NombreArchivo.Text = archivo.Nombre.Value

        lbl_Info.Text = archivo.Extension.Value & "(" & archivo.Size.Value & " bytes)"
        lbl_comentario.Text = archivo.Comentario.Value

        btn_Descargar.Attributes.Add(archivo.Id_Archivo.Name, archivo.Id_Archivo.Value)

        'Verifica que tipo de Archivo es
        'Eventualmente que carge el <object...
        ArchivoHandler.GetArchivoImage(img_Archivo, archivo.FileName.Value, archivo.Extension.Value, level, True)
        lnk_Archivo.NavigateUrl = ArchivoHandler.GetArchivoURL(archivo.FileName.Value, archivo.Extension.Value, level, True)

        If valorPrincipal = "1" Then
            chk_Principal.Checked = True
        End If
    End Sub

    Protected Function updateMedia_Producto(ByVal id_archivo As Integer, ByVal indiceDG As Integer) As Boolean
        Dim tbx_NombreMedia As TextBox = dg_Archivo.Items(indiceDG).FindControl("tbx_NombreMedia")
        Dim lbl_Info As Label = dg_Archivo.Items(indiceDG).FindControl("lbl_Info")
        Dim chk_Principal As CheckBox = dg_Archivo.Items(indiceDG).FindControl("chk_Principal")
        Dim tbx_Comentarios As TextBox = dg_Archivo.Items(indiceDG).FindControl("tbx_Comentarios")

        Dim checkValue As Integer = 0
        If chk_Principal.Checked Then
            checkValue = 1
        End If

        Dim resultado As Boolean = False
        resultado = controladora.ActualizarArchivoIntermedio(id_archivo, id_dueno, checkValue)
        Return resultado
    End Function

    Protected Function obtenerControladora() As IControladorArchivos
        If Request.QueryString("id_entidad") IsNot Nothing Then
            id_dueno = Request.QueryString("id_Entidad")
            Return New ControladoraArchivos_Entidad(connection)
        ElseIf Request.QueryString("id_publicacion") IsNot Nothing Then
            id_dueno = Request.QueryString("id_publicacion")
            Return New ControladoraArchivos_Publicacion(connection)
        ElseIf Request.QueryString("id_producto") IsNot Nothing Then
            id_dueno = Request.QueryString("id_producto")
            Return New ControladoraArchivos_Producto(connection)
        ElseIf Request.QueryString("id_proyecto") IsNot Nothing Then
            'id_dueno = Request.QueryString("id_proyecto")
            'Return New ControladoraArchivos_Proyecto(connection)
        ElseIf Request.QueryString("id_cuenta") IsNot Nothing Then
            id_dueno = Request.QueryString("id_cuenta")
            'Return New ControladoraArchivos_Cuenta(connection)
        End If

        Return Nothing
    End Function

    Protected Sub selectArchivo_Configuration()
        Dim dataSet As Data.DataSet
        Dim Archivo_Configuration As New Archivo_Configuration

        Archivo_Configuration.Fields.SelectAll()
        queryBuilder.Orderby.Add(Archivo_Configuration.Nombre)
        dataSet = connection.executeSelect(queryBuilder.SelectQuery(Archivo_Configuration))

        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables(0).Rows.Count > 0 Then
                ddl_Configuration.DataSource = dataSet
                ddl_Configuration.DataValueField = Archivo_Configuration.Id_Configuration.Name
                ddl_Configuration.DataTextField = Archivo_Configuration.Nombre.Name
                ddl_Configuration.DataBind()
            End If
        End If
    End Sub

    'Archivo
    Protected Sub salvarArchivoFile(ByVal id_dueno As Integer, ByVal id_Configuration As Integer)
        Dim handler As New Orbelink.Control.Archivos.ArchivoHandler(connection)

        For counter As Integer = 0 To dg_Archivo.Items.Count - 1
            Dim upl_Imagen As FileUpload = dg_Archivo.Items(counter).FindControl("upl_Imagen")
            Dim tbx_NombreArchivo As TextBox = dg_Archivo.Items(counter).FindControl("tbx_NombreArchivo")
            Dim lbl_Info As Label = dg_Archivo.Items(counter).FindControl("lbl_Info")
            Dim chk_Principal As CheckBox = dg_Archivo.Items(counter).FindControl("chk_Principal")
            Dim tbx_Comentarios As TextBox = dg_Archivo.Items(counter).FindControl("tbx_Comentarios")

            'Si ya existia
            If tbx_NombreArchivo.ToolTip.Length > 0 Then
                updateMedia_Producto(tbx_NombreArchivo.ToolTip, counter)
            Else
                'No, entonces insertar
                Dim checkValue As Integer = 0
                If chk_Principal.Checked Then
                    checkValue = 1
                End If

                Dim controlador As New ArchivoHandler(connection)
                Dim algo As IControladorArchivos = obtenerControladora()
                If upl_Imagen.HasFile Then
                    If controlador.RegistrarArchivo(algo, upl_Imagen, id_dueno, checkValue, tbx_NombreArchivo.Text, tbx_Comentarios.Text, ddl_Configuration.SelectedValue) Then
                        MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Salvar, "Archivo " & (counter + 1), False)
                    Else
                        MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Salvar, "Archivo " & (counter + 1), True)
                    End If
                End If
            End If
        Next
    End Sub

    'Verifica checks
    Protected Function VerificaChecks(ByVal maxPrincipal As Integer) As Boolean
        Dim contador As Integer
        Dim objItem As DataGridItem
        For Each objItem In dg_Archivo.Items
            Dim chk_Principal As CheckBox = objItem.FindControl("chk_Principal")
            If chk_Principal.Checked Then
                contador += 1
            End If
        Next
        If contador <= maxPrincipal Then
            Return True
        Else
            Return False
        End If
    End Function

    'Eventos
    Private Sub cmb_CantiArchivo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmb_CantiArchivo.SelectedIndexChanged
        dg_Archivo.CurrentPageIndex = 0
        selectArchivos(id_dueno, cmb_CantiArchivo.SelectedValue)
    End Sub

    Protected Sub dg_Archivo_DeleteCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dg_Archivo.DeleteCommand
        Dim tbx_NombreArchivo As TextBox = dg_Archivo.Items(e.Item.ItemIndex).FindControl("tbx_NombreArchivo")
        If tbx_NombreArchivo.ToolTip.Length > 0 Then
            Dim controlador As New ArchivoHandler(connection)
            If controlador.DeleteArchivo(tbx_NombreArchivo.ToolTip) Then
                selectArchivos(id_dueno, cmb_CantiArchivo.SelectedValue)
            Else
                MyMaster.concatenarMensaje("Error al borrar archivo Archivo.", False)
            End If
        End If
    End Sub

    Protected Sub dg_Archivo_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dg_Archivo.PageIndexChanged
        dg_Archivo.CurrentPageIndex = e.NewPageIndex
        selectArchivos(id_dueno, cmb_CantiArchivo.SelectedValue)
    End Sub

    Protected Sub ddl_FileTypes_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddl_FileTypes.SelectedIndexChanged
        dg_Archivo.CurrentPageIndex = 0
        selectArchivos(id_dueno, cmb_CantiArchivo.SelectedValue)
    End Sub

    Protected Sub btn_Descargar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim btn_Descargar As Button = sender
        Dim dataSet As Data.DataSet
        Dim archivo As New Archivo
        Dim id_Archivo As Integer = btn_Descargar.Attributes(archivo.Id_Archivo.Name)

        archivo.FileName.ToSelect = True
        archivo.Extension.ToSelect = True
        archivo.Id_Archivo.Where.EqualCondition(id_Archivo)
        queryBuilder.From.Add(archivo)
        dataSet = connection.executeSelect(queryBuilder.RelationalSelectQuery)

        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables(0).Rows.Count > 0 Then
                ObjectBuilder.CreateObject(dataSet.Tables(0), 0, archivo)
                If Not Orbelink.Control.Archivos.ArchivoHandler.DownloadArchivoFile(archivo.FileName.Value, archivo.Extension.Value) Then
                    MyMaster.MostrarMensaje("No puede descargar archivo.", True)
                End If
            End If
        End If
    End Sub

    Protected Sub btn_salvar_Click(ByVal Sender As Object, ByVal E As System.EventArgs) Handles btn_Salvar.Click
        Dim numeroPrincipales As Integer = 1
        If VerificaChecks(numeroPrincipales) Then
            If Me.ddl_Configuration.Items.Count > 0 Then
                salvarArchivoFile(id_dueno, ddl_Configuration.SelectedValue)
                selectArchivos(id_dueno, cmb_CantiArchivo.SelectedValue)
            Else
                MyMaster.MostrarMensaje("No hay configuraciones. Debe ingresar almenos una.", True)
            End If
        Else
            MyMaster.MostrarMensaje("Habia seleccionado mas principales de los permitidos (" & numeroPrincipales & "). Accion cancelada.", False)
        End If
    End Sub

    Protected Sub btn_cancelar_Click(ByVal Sender As Object, ByVal E As System.EventArgs) Handles btn_cancelar.Click
        selectArchivos(id_dueno, cmb_CantiArchivo.SelectedValue)
    End Sub
End Class
