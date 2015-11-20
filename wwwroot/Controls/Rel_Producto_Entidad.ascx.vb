Imports Orbelink.DBHandler
Imports Orbelink.Entity.Entidades

Partial Class Controls_Rel_Producto_Entidad
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

    Public Sub cargarRelacionados(ByVal id_producto As Integer)
        Dim Entidad_producto As New Relacion_Entidad_Producto
        Dim tipoRelacion As New TipoRelacion_Entidad_Producto
        Dim Entidad As New Entidad
        Dim dataSet As New Data.DataSet
        If securityHandler.verificarAcceso("Re-02") Then
            Me.hyl_agregarRelProducto_Entidad.NavigateUrl = MyMaster.obtenerIframeString("../Relaciones/Entidad_Producto.aspx?id_producto=" & id_producto)
            Me.hyl_agregarRelProducto_Entidad.Visible = True
        End If
        Entidad.NombreDisplay.ToSelect = True
        tipoRelacion.Nombre_Producto_Entidad.ToSelect = True
        Entidad.Id_entidad.ToSelect = True
        tipoRelacion.Id_TipoRelacion.ToSelect = True

        queryBuilder.Join.EqualCondition(Entidad_producto.id_entidad, Entidad.Id_entidad)
        queryBuilder.Join.EqualCondition(Entidad_producto.id_TipoRelacion, tipoRelacion.Id_TipoRelacion)
        Entidad_producto.id_producto.Where.EqualCondition(id_producto)

        queryBuilder.From.Add(Entidad)
        queryBuilder.From.Add(tipoRelacion)
        queryBuilder.From.Add(Entidad_producto)

        Dim consulta As String = queryBuilder.RelationalSelectQuery()
        dataSet = connection.executeSelect(consulta)
        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables(0).Rows.Count > 0 Then

                Dim resul_Entidad As ArrayList = ObjectBuilder.TransformDataTable(dataSet.Tables(0), Entidad)
                Dim resul_tipo As ArrayList = ObjectBuilder.TransformDataTable(dataSet.Tables(0), tipoRelacion)

                Me.dl_Rel_producto_entidad.DataSource = dataSet
                Me.dl_Rel_producto_entidad.DataBind()

                For counter As Integer = 0 To dataSet.Tables(0).Rows.Count - 1
                    Dim item As DataListItem = dl_Rel_producto_entidad.Items(counter)
                    Dim act_Producto As Entidad = resul_Entidad(counter)
                    Dim act_tipo As TipoRelacion_Entidad_Producto = resul_tipo(counter)

                    Dim relacion As Label = item.FindControl("lbl_relacion")
                    relacion.Text = act_tipo.Nombre_Producto_Entidad.Value & " " & act_Producto.NombreDisplay.Value
                    lbl_vacio.Visible = False
                    dl_Rel_producto_entidad.Visible = True
                Next
            Else
                lbl_vacio.Visible = True
                dl_Rel_producto_entidad.Visible = False
            End If
        Else
            dl_Rel_producto_entidad.Visible = False
            lbl_vacio.Visible = True
        End If
        Me.udp_rel_producto_entidad.Update()
    End Sub

End Class
