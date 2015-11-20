Imports Orbelink.DBHandler
Imports Orbelink.Entity.Publicaciones

Partial Class Controls_Rel_Producto_Publicacion
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
        Me.pnl_rel.Visible = securityHandler.verificarAcceso("PU-01")
    End Sub

    Public Sub cargarRelacionados(ByVal id_producto As Integer)
        Dim publicacion_producto As New Relacion_Publicacion_Producto
        Dim tipoRelacion As New TipoRelacion_Producto_Publicacion
        Dim publicacion As New Publicacion
        Dim dataSet As New Data.DataSet
        If securityHandler.verificarAcceso("Re-06") Then
            Me.hyl_agregarRelProducto_publicacion.NavigateUrl = MyMaster.obtenerIframeString("../Relaciones/producto_publicacion.aspx?id_producto=" & id_producto)
            Me.hyl_agregarRelProducto_publicacion.Visible = True
        End If
        publicacion.Titulo.ToSelect = True
        tipoRelacion.Nombre_Producto_Publicacion.ToSelect = True
        publicacion.Id_Publicacion.ToSelect = True
        tipoRelacion.Id_TipoRelacion.ToSelect = True

        queryBuilder.Join.EqualCondition(publicacion_producto.id_Publicacion, publicacion.Id_Publicacion)
        queryBuilder.Join.EqualCondition(publicacion_producto.id_TipoRelacion, tipoRelacion.Id_TipoRelacion)
        publicacion_producto.id_Producto.Where.EqualCondition(id_producto)

        queryBuilder.From.Add(publicacion)
        queryBuilder.From.Add(tipoRelacion)
        queryBuilder.From.Add(publicacion_producto)

        Dim consulta As String = queryBuilder.RelationalSelectQuery()
        dataSet = connection.executeSelect(consulta)
        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables(0).Rows.Count > 0 Then

                Dim resul_publicacion As ArrayList = ObjectBuilder.TransformDataTable(dataSet.Tables(0), publicacion)
                Dim resul_tipo As ArrayList = ObjectBuilder.TransformDataTable(dataSet.Tables(0), tipoRelacion)

                Me.dl_Rel_producto_publicacion.DataSource = dataSet
                Me.dl_Rel_producto_publicacion.DataBind()

                For counter As Integer = 0 To dataSet.Tables(0).Rows.Count - 1
                    Dim item As DataListItem = dl_Rel_producto_publicacion.Items(counter)
                    Dim act_Producto As Publicacion = resul_publicacion(counter)
                    Dim act_tipo As TipoRelacion_Producto_Publicacion = resul_tipo(counter)

                    Dim relacion As Label = item.FindControl("lbl_relacion")
                    relacion.Text = act_tipo.Nombre_Producto_Publicacion.Value & " " & act_Producto.Titulo.Value
                    lbl_vacio.Visible = False
                    dl_Rel_producto_publicacion.Visible = True
                Next
            Else
                lbl_vacio.Visible = True
                dl_Rel_producto_publicacion.Visible = False
            End If
        Else
            dl_Rel_producto_publicacion.Visible = False
            lbl_vacio.Visible = True
        End If
        Me.udp_rel_producto_publicacion.Update()
    End Sub













End Class
