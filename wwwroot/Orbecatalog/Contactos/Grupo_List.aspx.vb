Imports Orbelink.Control.Archivos
Imports Orbelink.DBHandler
Imports Orbelink.Entity.Entidades

Partial Class _Grupo_View
    Inherits PageBaseClass

    Const codigo_pantalla As String = "CO-14"
    Const level As Integer = 0

    'Pagina
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddScripts(level)
        'securityHandler.VerifyPantalla(codigo_pantalla, level)
        If Not Page.IsPostBack Then
            Dim id_Grupo As Integer = 0
            If Not Request.QueryString("id_Grupo") Is Nothing Then
                ClearInfo(Configuration.EstadoPantalla.CONSULTAR, True)
                id_Grupo = Request.QueryString("id_Grupo")
                If selectGrupo(id_Grupo, securityHandler.Entidad) Then
                    selectTipoRelacion_Grupos(id_Grupo)
                Else
                    Response.Redirect("Default.aspx")
                End If
            Else
                Response.Redirect("Default.aspx")
            End If
        End If
    End Sub

    Protected Sub ClearInfo(ByVal estado As Configuration.EstadoPantalla, ByVal clearInfo As Boolean)
        If clearInfo Then
            'lbl_Titulo.Text = ""
            'lbl_Corta.Text = ""
            'lbl_Texto.Text = ""
            'lbl_Link.Text = ""
            'lbl_Titulo.ToolTip = ""
        End If

        Select Case estado
            Case Configuration.EstadoPantalla.NORMAL
                'lbl_Entidad.ToolTip = securityHandler.Entidad

            Case Configuration.EstadoPantalla.CONSULTAR

        End Select
    End Sub

    'Grupo
    Protected Function selectGrupo(ByVal id_Grupo As Integer, ByVal id_Entidad As Integer) As Boolean
        Dim dataSet As Data.DataSet
        Dim Entidades_Grupo As New Entidades_Grupo
        Dim Grupos_Entidades As New Grupos_Entidades

        If id_Entidad <> 0 Then
            Entidades_Grupo.Id_Entidad.Where.EqualCondition(id_Entidad)
        End If

        Entidades_Grupo.Id_Grupo.Where.EqualCondition(id_Grupo)
        Entidades_Grupo.Id_TipoRelacion.ToSelect = True

        Grupos_Entidades.Nombre.ToSelect = True
        Grupos_Entidades.Descripcion.ToSelect = True
        Grupos_Entidades.Id_Grupo.ToSelect = True
        queryBuilder.Join.EqualCondition(Grupos_Entidades.Id_Grupo, Entidades_Grupo.Id_Grupo)

        queryBuilder.From.Add(Entidades_Grupo)
        queryBuilder.From.Add(Grupos_Entidades)
        dataSet = connection.executeSelect(queryBuilder.RelationalSelectQuery())

        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables(0).Rows.Count > 0 Then
                dtl_Relaciones.DataSource = dataSet
                dtl_Relaciones.DataKeyField = Grupos_Entidades.Id_Grupo.Name
                dtl_Relaciones.DataBind()

                'Grupo
                ObjectBuilder.createObject(dataSet.Tables(0), 0, Grupos_Entidades)
                ObjectBuilder.createObject(dataSet.Tables(0), 0, Entidades_Grupo)

                lbl_Titulo.Text = Grupos_Entidades.Nombre.Value
                lbl_Descripcion.Text = Grupos_Entidades.Descripcion.Value
                Return True
            End If
        End If
        Return False
    End Function

    Protected Sub selectTipoRelacion_Grupos(ByVal id_Grupo As Integer)
        Dim dataSet As Data.DataSet
        Dim Entidades_Grupo As New Entidades_Grupo
        Dim TipoRelacion_Grupos As New TipoRelacion_Grupos

        Entidades_Grupo.Id_Grupo.Where.EqualCondition(id_Grupo)

        TipoRelacion_Grupos.Nombre.ToSelect = True
        TipoRelacion_Grupos.Id_TipoRelacion.ToSelect = True
        queryBuilder.Join.EqualCondition(TipoRelacion_Grupos.Id_TipoRelacion, Entidades_Grupo.Id_TipoRelacion)

        queryBuilder.From.Add(Entidades_Grupo)
        queryBuilder.From.Add(TipoRelacion_Grupos)
        queryBuilder.Distinct = True
        dataSet = connection.executeSelect(queryBuilder.RelationalSelectQuery())

        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables(0).Rows.Count > 0 Then
                dtl_Relaciones.DataSource = dataSet
                dtl_Relaciones.DataKeyField = TipoRelacion_Grupos.Id_TipoRelacion.Name
                dtl_Relaciones.DataBind()

                'Llena el grid
                Dim results_TipoRelacion_Grupos As ArrayList = ObjectBuilder.TransformDataTable(dataSet.Tables(0), TipoRelacion_Grupos)
                For counter As Integer = 0 To dtl_Relaciones.Items.Count - 1
                    Dim act_TipoRelacion_Grupos As TipoRelacion_Grupos = results_TipoRelacion_Grupos(counter)

                    Dim lbl_TipoRelacion As Label = dtl_Relaciones.Items(counter).FindControl("lbl_TipoRelacion")
                    Dim dtl_Entidades As DataList = dtl_Relaciones.Items(counter).FindControl("dtl_Entidades")

                    lbl_TipoRelacion.Text = act_TipoRelacion_Grupos.Nombre.Value
                    selectEntidades_Grupo(dtl_Entidades, id_Grupo, act_TipoRelacion_Grupos.Id_TipoRelacion.Value)
                Next

            End If
        End If
    End Sub

    Protected Sub selectEntidades_Grupo(ByRef theDataList As DataList, ByVal id_grupo As Integer, ByVal id_Relacion As Integer)
        Dim dataSet As Data.DataSet
        Dim Entidades_Grupo As New Entidades_Grupo
        Dim entidad As New Entidad

        entidad.NombreDisplay.ToSelect = True
        entidad.Id_entidad.ToSelect = True
        queryBuilder.Join.EqualCondition(entidad.Id_entidad, Entidades_Grupo.Id_Entidad)

        If id_grupo <> 0 Then
            Entidades_Grupo.Id_Grupo.Where.EqualCondition(id_grupo)
        End If

        If id_Relacion <> 0 Then
            Entidades_Grupo.Id_TipoRelacion.Where.EqualCondition(id_Relacion)
        End If

        queryBuilder.From.Add(Entidades_Grupo)
        queryBuilder.From.Add(entidad)
        queryBuilder.Orderby.Add(entidad.NombreDisplay)

        dataSet = connection.executeSelect(queryBuilder.RelationalSelectQuery)
        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables(0).Rows.Count > 0 Then
                theDataList.DataSource = dataSet
                theDataList.DataKeyField = entidad.Id_entidad.Name
                theDataList.DataBind()

                'Llena el grid
                Dim results_Entidades As ArrayList = ObjectBuilder.TransformDataTable(dataSet.Tables(0), entidad)
                For counter As Integer = 0 To theDataList.Items.Count - 1
                    Dim act_Entidad As Entidad = results_Entidades(counter)

                    Dim lnk_Entidad As HyperLink = theDataList.Items(counter).FindControl("lnk_Entidad")

                    lnk_Entidad.Text = act_Entidad.NombreDisplay.Value
                    lnk_Entidad.NavigateUrl = "Entidad.aspx?id_Entidad=" & act_Entidad.Id_entidad.Value
                Next
            End If
        End If
    End Sub

End Class

