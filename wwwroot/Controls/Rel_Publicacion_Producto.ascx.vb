Imports Orbelink.DBHandler
Imports Orbelink.Entity.Productos

Partial Class Controls_Rel_Publicacion_Producto
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
        Me.pnl_rel.Visible = securityHandler.verificarAcceso("PR-01")
    End Sub

    Public Sub cargarRelacionados(ByVal id_publicacion As Integer)
        Dim producto_publicacion As New Relacion_Producto_Publicacion
        Dim tipoRelacion As New TipoRelacion_Producto_Publicacion
        Dim producto As New Producto
        Dim dataSet As New Data.DataSet
        If securityHandler.verificarAcceso("Re-06") Then
            Me.hyl_agregarRelPublicacion_producto.NavigateUrl = MyMaster.obtenerIframeString("../Relaciones/producto_publicacion.aspx?id_publicacion=" & id_publicacion)
            Me.hyl_agregarRelPublicacion_producto.Visible = True
        End If
        producto.Nombre.ToSelect = True
        tipoRelacion.Nombre_publicacion_producto.ToSelect = True
        producto.Id_Producto.ToSelect = True
        tipoRelacion.Id_TipoRelacion.ToSelect = True

        queryBuilder.Join.EqualCondition(producto_publicacion.id_Producto, producto.Id_Producto)
        queryBuilder.Join.EqualCondition(producto_publicacion.id_TipoRelacion, tipoRelacion.Id_TipoRelacion)
        producto_publicacion.id_Publicacion.Where.EqualCondition(id_publicacion)

        queryBuilder.From.Add(producto)
        queryBuilder.From.Add(tipoRelacion)
        queryBuilder.From.Add(producto_publicacion)

        Dim consulta As String = queryBuilder.RelationalSelectQuery()
        dataSet = connection.executeSelect(consulta)
        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables(0).Rows.Count > 0 Then

                Dim resul_producto As ArrayList = ObjectBuilder.TransformDataTable(dataSet.Tables(0), producto)
                Dim resul_tipo As ArrayList = ObjectBuilder.TransformDataTable(dataSet.Tables(0), tipoRelacion)

                Me.dl_Rel_publicacion_producto.DataSource = dataSet
                Me.dl_Rel_publicacion_producto.DataBind()

                For counter As Integer = 0 To dataSet.Tables(0).Rows.Count - 1
                    Dim item As DataListItem = dl_Rel_publicacion_producto.Items(counter)
                    Dim act_Producto As Producto = resul_producto(counter)
                    Dim act_tipo As TipoRelacion_Producto_Publicacion = resul_tipo(counter)

                    Dim relacion As Label = item.FindControl("lbl_relacion")
                    relacion.Text = act_Producto.Nombre.Value & ", como " & act_tipo.Nombre_publicacion_producto.Value
                    lbl_vacio.Visible = False
                    dl_Rel_publicacion_producto.Visible = True
                Next
            Else
                lbl_vacio.Visible = True
                dl_Rel_publicacion_producto.Visible = False
            End If
        Else
            dl_Rel_publicacion_producto.Visible = False
            lbl_vacio.Visible = True
        End If
        Me.udp_rel_publicacion_producto.Update()
    End Sub















End Class
