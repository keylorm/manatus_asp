Imports Orbelink.DBHandler
Imports Orbelink.Entity.Entidades
Imports Orbelink.Entity.Orbecatalog

Partial Class _Informe_Pendiente
    Inherits PageBaseClass

    Const codigo_pantalla As String = "OR-07"
    Const level As Integer = 1
    Dim id_Pendiente As Integer

    'Pagina
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddScripts(level)
        SetControlProperties()
        securityHandler.VerifyPantalla(codigo_pantalla, level)

        If Not Request.QueryString("id_Pendiente") Is Nothing Then
            id_Pendiente = Request.QueryString("id_Pendiente")
        Else
            Response.Redirect("Default.aspx")
        End If

        If Not IsPostBack Then
            ClearInfo(Configuration.EstadoPantalla.NORMAL, False)
            'securityHandler.VerifyActions(btn_Salvar, btn_Modificar, btn_Eliminar)

            If id_Pendiente <> 0 Then
                If loadPendiente(id_Pendiente) Then
                    selectInformes_Pendiente(id_Pendiente)
                Else
                    Response.Redirect("Default.aspx")
                End If
            End If
        End If
    End Sub

    Protected Sub ClearInfo(ByVal estado As Configuration.EstadoPantalla, ByVal clearInfo As Boolean)
        If clearInfo Then
            tbx_Comentario.Text = ""
        End If
        Select Case estado
            Case Configuration.EstadoPantalla.NORMAL
                btn_Salvar.Visible = True
            Case Configuration.EstadoPantalla.CONSULTAR
                btn_Salvar.Visible = False
        End Select
    End Sub

    Protected Sub SetControlProperties()
        tbx_Comentario.Attributes.Add("maxlength", 300)
        tbx_Comentario.Attributes.Add("onkeypress", "showTextProgress(this)")
    End Sub

    'Pendiente
    Protected Function loadPendiente(ByVal Id_Pendiente As Integer) As Boolean
        Dim dataSet As Data.DataSet
        Dim Pendiente As New Pendiente
        Dim entidad As New Entidad
        Dim loaded As Boolean = False

        Pendiente.Fields.SelectAll()
        Pendiente.Id_Pendiente.Where.EqualCondition(Id_Pendiente)
        entidad.NombreDisplay.ToSelect = True
        queryBuilder.Join.EqualCondition(entidad.Id_entidad, Pendiente.Id_Publicador)

        queryBuilder.From.Add(Pendiente)
        queryBuilder.From.Add(entidad)
        dataSet = connection.executeSelect(queryBuilder.RelationalSelectQuery)

        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables(0).Rows.Count > 0 Then
                ObjectBuilder.CreateObject(dataSet.Tables(0), 0, Pendiente)
                ObjectBuilder.CreateObject(dataSet.Tables(0), 0, entidad)

                lbl_NombrePendiente.Text = Pendiente.Nombre.Value
                lbl_NombrePendiente.ToolTip = Pendiente.Id_Pendiente.Value

                lbl_Autor.Text = entidad.NombreDisplay.Value
                'lbl_Prioridad.SelectedValue = Pendiente.Prioridad.Value
                lbl_Descripcion.Text = Pendiente.Descripcion.Value
                lbl_FechaProgramado.Text = Pendiente.FechaProgramado.ValueLocalized

                If Pendiente.Terminado.Value = "1" Then
                    'cln_FechaTerminado.SelectedDate = Pendientes_TareaProyecto.Fecha_Terminado.Value
                End If

                loaded = True
            End If
        End If

        Return loaded
    End Function

    Protected Function updatePendiente(ByVal Id_Pendiente As Integer, ByVal id_Entidad As Integer) As Boolean
        Dim Pendiente As New Pendiente
        Try
            'If id_Entidad <> 0 Then
            '    Pendiente.Id_Entidad.Value = id_Entidad
            'Else
            '    Pendiente.Id_Entidad.Value = Configuration.Config_DefaultIdEntidad
            'End If
            Pendiente.Terminado.Value = "1"
            Pendiente.FechaTerminado.ValueUtc = Date.UtcNow
            Pendiente.Id_Pendiente.Where.EqualCondition(Id_Pendiente)
            connection.executeUpdate(queryBuilder.UpdateQuery(Pendiente))
            Return True
        Catch exc As Exception
            MyMaster.mostrarMensaje(exc.Message, True)
            Return False
        End Try
    End Function

    'Informe_Pendiente
    Protected Sub selectInformes_Pendiente(ByVal Id_Pendiente As Integer)
        Dim dataset As Data.DataSet
        Dim entidad As New Entidad
        Dim Informes_Pendiente As New Informes_Pendiente

        Informes_Pendiente.Id_Pendiente.Where.EqualCondition(Id_Pendiente)
        Informes_Pendiente.Comentario.ToSelect = True
        Informes_Pendiente.FechaInforme.ToSelect = True
        Informes_Pendiente.Id_Informe.ToSelect = True

        entidad.NombreDisplay.ToSelect = True
        queryBuilder.Join.EqualCondition(entidad.Id_entidad, Informes_Pendiente.Id_Entidad)

        queryBuilder.From.Add(entidad)
        queryBuilder.From.Add(Informes_Pendiente)
        dataset = connection.executeSelect(queryBuilder.RelationalSelectQuery)

        If dataset.Tables.Count > 0 Then
            If dataset.Tables(0).Rows.Count > 0 Then
                dg_Pendientes_TareaProyectos.DataSource = dataset
                dg_Pendientes_TareaProyectos.DataKeyField = Informes_Pendiente.Id_Informe.Name
                dg_Pendientes_TareaProyectos.DataBind()

                'Llena el grid
                Dim offset As Integer = dg_Pendientes_TareaProyectos.CurrentPageIndex * dg_Pendientes_TareaProyectos.PageSize
                Dim result_Entidades As ArrayList = ObjectBuilder.TransformDataTable(dataset.Tables(0), entidad)
                Dim result_Informes_Pendiente As ArrayList = ObjectBuilder.TransformDataTable(dataset.Tables(0), Informes_Pendiente)
                Dim counter As Integer
                For counter = 0 To dg_Pendientes_TareaProyectos.Items.Count - 1
                    Dim act_Entidad As Entidad = result_Entidades(offset + counter)
                    Dim act_Informes_Pendiente As Informes_Pendiente = result_Informes_Pendiente(offset + counter)

                    Dim lbl_Comentario As Label = dg_Pendientes_TareaProyectos.Items(counter).FindControl("lbl_Comentario")
                    Dim lbl_Fecha As Label = dg_Pendientes_TareaProyectos.Items(counter).FindControl("lbl_Fecha")
                    Dim lbl_Entidad As Label = dg_Pendientes_TareaProyectos.Items(counter).FindControl("lbl_Entidad")

                    lbl_Comentario.Text = act_Informes_Pendiente.Comentario.Value
                    lbl_Fecha.Text = act_Informes_Pendiente.FechaInforme.ValueLocalized
                    lbl_Entidad.Text = act_Entidad.NombreDisplay.Value
                Next
                dg_Pendientes_TareaProyectos.Visible = True
            Else
                dg_Pendientes_TareaProyectos.Visible = False
            End If
        End If
    End Sub

    Protected Function insertInformes_Pendiente(ByVal Id_Pendiente As Integer, ByVal id_entidad As Integer) As Boolean
        Dim Informes_Pendiente As New Informes_Pendiente
        If id_entidad = 0 Then
            id_entidad = Configuration.Config_DefaultIdEntidad
        End If
        Try
            Informes_Pendiente.Hilo.Value = 0
            Informes_Pendiente.FechaInforme.ValueUtc = Date.UtcNow
            Informes_Pendiente.Comentario.Value = tbx_Comentario.Text
            Informes_Pendiente.Id_Pendiente.Value = Id_Pendiente
            Informes_Pendiente.Id_Entidad.Value = id_entidad
            connection.executeInsert(queryBuilder.InsertQuery(Informes_Pendiente))
            Return True
        Catch exc As Exception
            MyMaster.mostrarMensaje(exc.Message, True)

            Return False
        End Try
    End Function

    'Eventos
    Protected Sub btn_Salvar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Salvar.Click

        If IsValid() Then
            If insertInformes_Pendiente(id_Pendiente, securityHandler.Entidad) Then
                If chk_Terminar.Checked Then
                    updatePendiente(id_Pendiente, securityHandler.Entidad)
                End If
                ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
                selectInformes_Pendiente(id_Pendiente)
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Salvar, "Comentario", False)
            Else
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Salvar, "comentario", True)
            End If
        Else
            MyMaster.MostrarMensaje(Resources.Orbecatalog_Resources.CamposRequeridos, False)
        End If
        upd_Busqueda.Update()
        upd_Contenido.Update()
    End Sub

End Class
