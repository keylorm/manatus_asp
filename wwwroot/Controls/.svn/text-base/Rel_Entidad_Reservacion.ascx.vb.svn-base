Imports Orbelink.DBHandler
Imports Orbelink.Entity.Entidades

Partial Class Controls_Rel_Entidad_Reservacion
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
        Me.pnl_rel.Visible = securityHandler.verificarAcceso("Rs-01")
    End Sub

    Public Sub cargarRelacionados(ByVal id_Entidad As Integer)
        Dim dataSet As New Data.DataSet
        Dim entidad As New Entidad
        Dim reservacion As New Orbelink.Entity.Reservaciones.Reservacion
        Dim estado As New Orbelink.Entity.Reservaciones.TipoEstadoReservacion
        reservacion.fecha_inicioProgramado.ToSelect = True
        reservacion.Id_Reservacion.ToSelect = True
        estado.Nombre.ToSelect = True
        entidad.Id_entidad.Where.EqualCondition(id_Entidad)
        queryBuilder.Join.EqualCondition(entidad.Id_entidad, reservacion.id_Cliente)
        queryBuilder.Join.EqualCondition(reservacion.Id_TipoEstado, estado.Id_TipoEstadoReservacion)
        queryBuilder.Orderby.Add(reservacion.fecha_inicioProgramado)
        queryBuilder.From.Add(entidad)
        queryBuilder.From.Add(reservacion)
        queryBuilder.From.Add(estado)

        Dim consulta As String = queryBuilder.RelationalSelectQuery()
        dataSet = connection.executeSelect(consulta)
        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables(0).Rows.Count > 0 Then

                Dim resul_reservacion As ArrayList = ObjectBuilder.TransformDataTable(dataSet.Tables(0), reservacion)
                Dim resul_estado As ArrayList = ObjectBuilder.TransformDataTable(dataSet.Tables(0), estado)
                Me.dl_Rel_entidad_reservacion.DataSource = dataSet
                Me.dl_Rel_entidad_reservacion.DataBind()

                For counter As Integer = 0 To dataSet.Tables(0).Rows.Count - 1
                    Dim item As DataListItem = dl_Rel_entidad_reservacion.Items(counter)
                    Dim act_reservacion As Orbelink.Entity.Reservaciones.Reservacion = resul_reservacion(counter)
                    Dim act_estado As Orbelink.Entity.Reservaciones.TipoEstadoReservacion = resul_estado(counter)

                    Dim descripcion As HyperLink = item.FindControl("hyl_descripcion")
                    descripcion.Text = act_reservacion.fecha_inicioProgramado.ValueLocalized & " " & act_estado.Nombre.Value
                    descripcion.NavigateUrl = "~/Orbecatalog/Reservacion/Reservacion.aspx?id_reservacion=" & act_reservacion.Id_Reservacion.Value
                    lbl_vacio.Visible = False
                    dl_Rel_entidad_reservacion.Visible = True
                Next
            Else
                lbl_vacio.Visible = True
                dl_Rel_entidad_reservacion.Visible = False
            End If
        Else
            dl_Rel_entidad_reservacion.Visible = False
            lbl_vacio.Visible = True
        End If
        Me.udp_rel_entidad_reservacion.Update()
    End Sub

End Class
