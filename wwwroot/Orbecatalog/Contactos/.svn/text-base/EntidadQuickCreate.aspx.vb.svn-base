Imports Orbelink.DBHandler
Imports Orbelink.Control.Archivos
Imports Orbelink.Entity.Entidades
Imports Orbelink.Control.Entidades

Partial Class Orbecatalog_Entidades_EntidadQuickCreate
    Inherits PageBaseClass

    Const codigo_pantalla As String = "CO-06"
    Const level As Integer = 2

    'Pagina    
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddScripts(level)
        securityHandler.VerifyPantalla(codigo_pantalla, level)

        If Not IsPostBack Then
            securityHandler.VerifyActions(btn_Salvar, Nothing, Nothing)
            SetControlProperties()

            If Request.Params("nivelTipoEntidad") IsNot Nothing Then
                Try
                    selectTipoEntidad(securityHandler.ClearanceLevel, Request.Params("nivelTipoEntidad"))
                Catch ex As Exception
                    MyMaster.MostrarMensaje(ex.Message, True)
                End Try
            Else
                selectTipoEntidad(securityHandler.ClearanceLevel, -1)
            End If

            id_Actual = 0
            ClearInfo(Configuration.EstadoPantalla.NORMAL, True)

            ocwc_Ubicacion.SetVariables(connection, securityHandler, codigo_pantalla, MyMaster)
            ocwc_Ubicacion.selectUbicaciones()
        End If
    End Sub

    Protected Sub ClearInfo(ByVal estado As Configuration.EstadoPantalla, ByVal clearInfo As Boolean)
        ocwc_Ubicacion.ClearInfo(estado, clearInfo)

        If clearInfo Then
            tbx_NombreEntidad.Text = ""
            tbx_Apellido.Text = ""
            tbx_Apellido2.Text = ""
            tbx_Telefono.Text = ""
            tbx_Email.Text = ""
            tbx_Celular.Text = ""
            vld_tbx_NombreEntidad.IsValid = True
        End If

        Select Case estado
            Case Configuration.EstadoPantalla.NORMAL
                btn_Salvar.Visible = True
                div_Consultar.Visible = False
                div_Insertar.Visible = True
            Case Configuration.EstadoPantalla.CONSULTAR
                div_Consultar.Visible = True
                btn_Salvar.Visible = False
                div_Insertar.Visible = False

        End Select
    End Sub

    Public Overrides Sub PopUpOkEventHandler(ByVal param As String)
        'Cargar relacionados
        Dim id_entidad As Integer
        Dim loadSession As Boolean = False
        If id_Actual Then
            id_entidad = id_Actual
        End If

        Select Case param
            Case "CO-03"
                selectTipoEntidad(securityHandler.ClearanceLevel, False)

            Case "CO-10"
                ocwc_Ubicacion.selectUbicaciones()

        End Select
    End Sub

    Protected Sub SetControlProperties()
        Dim entidad As New Entidad
        tbx_NombreEntidad.Attributes.Add("maxlength", entidad.NombreEntidad.Length)
        tbx_NombreEntidad.Attributes.Add("onkeypress", "showTextProgress(this)")
        tbx_Apellido.Attributes.Add("maxlength", entidad.Apellido.Length)
        tbx_Apellido.Attributes.Add("onkeypress", "showTextProgress(this)")
        tbx_Apellido2.Attributes.Add("maxlength", entidad.Apellido2.Length)
        tbx_Apellido2.Attributes.Add("onkeypress", "showTextProgress(this)")
        tbx_Telefono.Attributes.Add("maxlength", entidad.Telefono.Length)
        tbx_Telefono.Attributes.Add("onkeypress", "showTextProgress(this)")
        tbx_Email.Attributes.Add("maxlength", entidad.Email.Length)
        tbx_Email.Attributes.Add("onkeypress", "showTextProgress(this)")
        tbx_Celular.Attributes.Add("maxlength", entidad.Celular.Length)
        tbx_Celular.Attributes.Add("onkeypress", "showTextProgress(this)")
    End Sub

    Protected Sub loadEntidad(ByVal id_Entidad As Integer)
        Dim entidad As New Entidad

        entidad.NombreDisplay.ToSelect = True
        entidad.Id_entidad.Where.EqualCondition(id_Entidad)

        queryBuilder.From.Add(entidad)

        Dim resultTable As Data.DataTable = connection.executeSelect_DT(queryBuilder.RelationalSelectQuery)
        'If dataSet.Tables.Count > 0 Then
        If resultTable.Rows.Count > 0 Then
            ObjectBuilder.CreateObject(resultTable, 0, entidad)
            lbl_Entidad.Text = entidad.NombreDisplay.Value
        End If
    End Sub

    Protected Function insertEntidad() As Orbelink.Control.Entidades.EntidadHandler.Resultado
        Dim entidad As New Entidad

        entidad.Id_tipoEntidad.Value = ddl_TipoEntidad.SelectedValue
        entidad.NombreEntidad.Value = tbx_NombreEntidad.Text
        entidad.Apellido2.Value = tbx_Apellido2.Text
        entidad.Telefono.Value = tbx_Telefono.Text
        entidad.Celular.Value = tbx_Celular.Text
        entidad.Apellido.Value = tbx_Apellido.Text
        entidad.Email.Value = tbx_Email.Text
        entidad.Id_Ubicacion.Value = ocwc_Ubicacion.Ubicacion_Selected()

        Dim controladoraEntidad As New EntidadHandler(Configuration.Config_DefaultConnectionString)
        Return controladoraEntidad.CrearEntidad(entidad)
    End Function

    Public Function hasMail() As Boolean
        If tbx_Email.Text.Length > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    'TipoEntidad
    Public Sub selectTipoEntidad(ByVal nivel As Integer, ByVal nivelTipoEntidad As Integer)
        Dim dataset As Data.DataSet
        Dim tipoEntidad As New TipoEntidad

        tipoEntidad.Fields.SelectAll()
        tipoEntidad.Nivel.Where.LessThanCondition(nivel)

        If nivelTipoEntidad >= 0 Then
            tipoEntidad.Nivel.Where.LessThanOrEqualCondition(nivelTipoEntidad)
        End If

        queryBuilder.Orderby.Add(tipoEntidad.Nombre)
        dataset = connection.executeSelect(queryBuilder.SelectQuery(tipoEntidad))
        If dataset.Tables.Count > 0 Then
            If dataset.Tables(0).Rows.Count > 0 Then
                ddl_TipoEntidad.DataSource = dataset
                ddl_TipoEntidad.DataTextField = tipoEntidad.Nombre.Name
                ddl_TipoEntidad.DataValueField = tipoEntidad.Id_TipoEntidad.Name
                ddl_TipoEntidad.DataBind()
            End If
        End If
    End Sub

    'Eventos
    Protected Sub btn_Salvar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Salvar.Click
        If IsValid And ocwc_Ubicacion.IsValid Then
            Dim resultado As Orbelink.Control.Entidades.EntidadHandler.Resultado = Me.insertEntidad()

            If resultado.code = EntidadHandler.ResultCodes.OK Then
                ClearInfo(Configuration.EstadoPantalla.CONSULTAR, False)
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Salvar, "Entidad", False)
                id_Actual = resultado.id_Entidad
                Session("QuickCreate_Entidad") = id_Actual
                loadEntidad(id_Actual)
                upd_Contenido.Update()
            Else
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Salvar, "Entidad", True)
                MyMaster.concatenarMensaje(Resources.Entidades_Resources.ResourceManager.GetString(resultado.code.ToString), True)
            End If
        Else
            MyMaster.MostrarMensaje(Resources.Orbecatalog_Resources.CamposRequeridos, True)
        End If
    End Sub

    Protected Sub btn_Cancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Cancelar.Click
        If id_Actual > 0 Then
            Me.ClearInfo(Configuration.EstadoPantalla.MODIFICAR, True)
        Else
            Me.ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
        End If
        upd_Contenido.Update()
    End Sub

    Protected Sub ocwc_Ubicacion_RequestExternalPage(ByVal laRuta As String, ByVal codigo As String) Handles ocwc_Ubicacion.RequestExternalPage
        MyMaster.mostrarPopUp(laRuta)
    End Sub

End Class
