Imports Orbelink.DBHandler
Imports Orbelink.Entity.Entidades

Partial Class Controls_Rel_Publicacion_Entidad
    Inherits System.Web.UI.UserControl

    Dim queryBuilder As Orbelink.DBHandler.QueryBuilder
    Dim connection As Orbelink.DBHandler.SQLServer
    Dim securityHandler As Orbelink.Control.Security.SecurityHandler
    Dim codigo_pantalla As String
    Dim MyMaster As Orbelink.Orbecatalog6.MasterPageBaseClass

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
        Me.pnl_rel.Visible = securityHandler.verificarAcceso("CO-01")
    End Sub

    Public Sub cargarRelacionados(ByVal id_publicacion As Integer)
        Dim Entidad_publicacion As New Relacion_Entidad_Publicacion
        Dim tipoRelacion As New TipoRelacion_Entidad_Publicacion
        Dim Entidad As New Entidad
        Dim dataSet As New Data.DataSet
        If securityHandler.verificarAcceso("Re-04") Then
            Me.hyl_agregarRelPublicacion_Entidad.NavigateUrl = MyMaster.obtenerIframeString("../Relaciones/Entidad_publicacion.aspx?id_publicacion=" & id_publicacion)
            Me.hyl_agregarRelPublicacion_Entidad.Visible = True
        End If
        Entidad.NombreDisplay.ToSelect = True
        tipoRelacion.Nombre_Publicacion_Entidad.ToSelect = True
        Entidad.Id_entidad.ToSelect = True
        tipoRelacion.Id_TipoRelacion.ToSelect = True

        queryBuilder.Join.EqualCondition(Entidad_publicacion.id_entidad, Entidad.Id_entidad)
        queryBuilder.Join.EqualCondition(Entidad_publicacion.id_TipoRelacion, tipoRelacion.Id_TipoRelacion)
        Entidad_publicacion.id_Publicacion.Where.EqualCondition(id_publicacion)

        queryBuilder.From.Add(Entidad)
        queryBuilder.From.Add(tipoRelacion)
        queryBuilder.From.Add(Entidad_publicacion)

        Dim consulta As String = queryBuilder.RelationalSelectQuery()
        dataSet = connection.executeSelect(consulta)
        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables(0).Rows.Count > 0 Then

                Dim resul_Entidad As ArrayList = ObjectBuilder.TransformDataTable(dataSet.Tables(0), Entidad)
                Dim resul_tipo As ArrayList = ObjectBuilder.TransformDataTable(dataSet.Tables(0), tipoRelacion)

                Me.dl_Rel_publicacion_entidad.DataSource = dataSet
                Me.dl_Rel_publicacion_entidad.DataBind()

                For counter As Integer = 0 To dataSet.Tables(0).Rows.Count - 1
                    Dim item As DataListItem = dl_Rel_publicacion_entidad.Items(counter)
                    Dim act_Entidad As Entidad = resul_Entidad(counter)
                    Dim act_tipo As TipoRelacion_Entidad_Publicacion = resul_tipo(counter)

                    Dim relacion As Label = item.FindControl("lbl_relacion")
                    relacion.Text = act_Entidad.NombreDisplay.Value & ", como " & act_tipo.Nombre_Publicacion_Entidad.Value
                    lbl_vacio.Visible = False
                    dl_Rel_publicacion_entidad.Visible = True
                Next
            Else
                lbl_vacio.Visible = True
                dl_Rel_publicacion_entidad.Visible = False
            End If
        Else
            dl_Rel_publicacion_entidad.Visible = False
            lbl_vacio.Visible = True
        End If
        Me.udp_rel_publicacion_entidad.Update()
    End Sub












End Class
