Imports Orbelink.DBHandler
Imports Orbelink.Entity.Publicaciones

Partial Class Controls_Rel_Entidad_Publicacion
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
        Me.pnl_rel.Visible = securityHandler.verificarAcceso("Pu-01")
    End Sub

    Public Sub cargarRelacionados(ByVal id_entidad As Integer)
        Dim Publicacion_Entidad As New Relacion_Publicacion_Entidad
        Dim tipoRelacion As New TipoRelacion_Entidad_Publicacion
        Dim Publicacion As New Publicacion
        Dim dataSet As New Data.DataSet
        If securityHandler.verificarAcceso("Re-04") Then
            Me.hyl_agregarRelEntidad_publicacion.NavigateUrl = MyMaster.obtenerIframeString("../Relaciones/Entidad_Publicacion.aspx?id_entidad=" & id_entidad)
            Me.hyl_agregarRelEntidad_publicacion.Visible = True
        End If

        Publicacion.Titulo.ToSelect = True
        tipoRelacion.Nombre_Entidad_Publicacion.ToSelect = True
        Publicacion.Id_Publicacion.ToSelect = True
        tipoRelacion.Id_TipoRelacion.ToSelect = True

        queryBuilder.Join.EqualCondition(Publicacion_Entidad.id_Publicacion, Publicacion.Id_Publicacion)
        queryBuilder.Join.EqualCondition(Publicacion_Entidad.id_TipoRelacion, tipoRelacion.Id_TipoRelacion)
        Publicacion_Entidad.id_entidad.Where.EqualCondition(id_entidad)

        queryBuilder.From.Add(Publicacion)
        queryBuilder.From.Add(tipoRelacion)
        queryBuilder.From.Add(Publicacion_Entidad)

        Dim consulta As String = queryBuilder.RelationalSelectQuery()
        dataSet = connection.executeSelect(consulta)
        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables(0).Rows.Count > 0 Then

                Dim resul_Publicacion As ArrayList = ObjectBuilder.TransformDataTable(dataSet.Tables(0), Publicacion)
                Dim resul_tipo As ArrayList = ObjectBuilder.TransformDataTable(dataSet.Tables(0), tipoRelacion)

                Me.dl_Rel_publicacion_entidad.DataSource = dataSet
                Me.dl_Rel_publicacion_entidad.DataBind()

                For counter As Integer = 0 To dataSet.Tables(0).Rows.Count - 1
                    Dim item As DataListItem = dl_Rel_publicacion_entidad.Items(counter)
                    Dim act_Producto As Publicacion = resul_Publicacion(counter)
                    Dim act_tipo As TipoRelacion_Entidad_Publicacion = resul_tipo(counter)

                    Dim relacion As Label = item.FindControl("lbl_relacion")
                    relacion.Text = act_tipo.Nombre_Entidad_Publicacion.Value & " " & act_Producto.Titulo.Value
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
        Me.udp_rel_entidad_publicacion.Update()
    End Sub



End Class
