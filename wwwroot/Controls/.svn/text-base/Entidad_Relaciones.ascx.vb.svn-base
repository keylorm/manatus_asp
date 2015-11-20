Imports Orbelink.DBHandler
Imports Orbelink.Entity.Entidades

Partial Class Controls_Entidad_Relaciones
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
    End Sub

    'Relacion_Entidades
    Public Sub selectRelaciones_Entidades(ByVal Id_entidad As Integer)
        Dim dataset As Data.DataSet
        Dim Relacion_Entidades As New Relacion_Entidades
        Dim entidad As New Entidad
        Dim entidadB As New Entidad
        Dim TipoRelacion_Entidades As New TipoRelacion_Entidades

        'TipoRelacion
        TipoRelacion_Entidades.NombreBaseDependiente.ToSelect = True
        queryBuilder.Join.EqualCondition(TipoRelacion_Entidades.Id_TipoRelacion, Relacion_Entidades.Id_TipoRelacion)

        'entidades
        entidad.NombreDisplay.ToSelect = True
        entidad.Id_entidad.ToSelect = True

        'entidades
        entidadB.NombreDisplay.ToSelect = True
        entidadB.Id_entidad.ToSelect = True
        entidadB.AsName = "EntidadB"

        'Relacion_Entidades
        queryBuilder.Join.EqualCondition(entidad.Id_entidad, Relacion_Entidades.Id_EntidadBase)
        queryBuilder.Join.EqualCondition(entidadB.Id_entidad, Relacion_Entidades.Id_EntidadDependiente)

        Relacion_Entidades.Fields.SelectAll()
        If Id_entidad <> 0 Then
            Relacion_Entidades.Id_EntidadBase.Where.EqualCondition(Id_entidad, Where.FieldRelations.AND_)
            Relacion_Entidades.Id_EntidadDependiente.Where.EqualCondition(Id_entidad, Where.FieldRelations.OR_)
            queryBuilder.GroupFieldsConditions(Relacion_Entidades.Id_EntidadBase, Relacion_Entidades.Id_EntidadDependiente)
        End If

        'Select de entidades base
        queryBuilder.From.Add(Relacion_Entidades)
        queryBuilder.From.Add(entidad)
        queryBuilder.From.Add(entidadB)
        queryBuilder.From.Add(TipoRelacion_Entidades)
        Dim consulta As String = queryBuilder.RelationalSelectQuery
        dataset = connection.executeSelect(consulta)

        If dataset.Tables.Count > 0 Then
            If dataset.Tables(0).Rows.Count > 0 Then
                dg_Relaciones.DataSource = dataset
                dg_Relaciones.DataBind()

                Dim results As ArrayList = ObjectBuilder.TransformDataTable(dataset.Tables(0), Relacion_Entidades)
                Dim results_EntidadA As ArrayList = ObjectBuilder.TransformDataTable(dataset.Tables(0), entidad)
                Dim results_EntidadB As ArrayList = ObjectBuilder.TransformDataTable(dataset.Tables(0), entidadB)

                'Llena el grid
                Dim offset As Integer = dg_Relaciones.CurrentPageIndex * dg_Relaciones.PageSize
                For counter As Integer = 0 To dg_Relaciones.Items.Count - 1
                    Dim act_Relacion_Entidades As Relacion_Entidades = results(offset + counter)
                    Dim act_EntidadA As Entidad = results_EntidadA(offset + counter)
                    Dim act_entidadB As Entidad = results_EntidadB(offset + counter)

                    Dim lbl_Relacion_Entidades As Label = dg_Relaciones.Items(counter).FindControl("lbl_Relacion_Entidades")
                    Dim lnk_Entidad_Dependiente As HyperLink = dg_Relaciones.Items(counter).FindControl("lnk_Entidad_Dependiente")
                    Dim lnk_Entidad_Base As HyperLink = dg_Relaciones.Items(counter).FindControl("lnk_Entidad_Base")
                    Dim lnk_Relacion As HyperLink = dg_Relaciones.Items(counter).FindControl("lnk_Relacion")

                    lnk_Relacion.NavigateUrl = "~/Orbecatalog/Contactos/Relaciones_Entidades.aspx?id_relacion=" & act_Relacion_Entidades.Id_Relacion.Value & Orbelink.Helpers.CommonTasks.RecreateQueryString(codigo_pantalla)

                    lnk_Entidad_Dependiente.Text = act_entidadB.NombreDisplay.Value
                    lnk_Entidad_Dependiente.NavigateUrl = "~/Orbecatalog/Contactos/Entidad.aspx?id_entidad=" & act_entidadB.Id_entidad.Value
                    lbl_Relacion_Entidades.Text = " es "
                    lbl_Relacion_Entidades.Text &= dataset.Tables(0).Rows(offset + counter).Item(TipoRelacion_Entidades.NombreBaseDependiente.Name)
                    lbl_Relacion_Entidades.Text &= " de "
                    lnk_Entidad_Base.Text = act_EntidadA.NombreDisplay.Value
                    lnk_Entidad_Base.NavigateUrl = "~/Orbecatalog/Contactos/Entidad.aspx?id_entidad=" & act_EntidadA.Id_entidad.Value
                Next
                dg_Relaciones.Visible = True

            Else
                dg_Relaciones.Visible = False
            End If
        End If
        Me.upd_relaciones.Update()
    End Sub

    Public Function deleteRelacion_Entidades(ByVal id_entidad As Integer) As Boolean
        Dim relacion As New Relacion_Entidades
        Try
            relacion.Fields.SelectAll()
            relacion.Id_EntidadBase.Where.EqualCondition(id_entidad)
            relacion.Id_EntidadDependiente.Where.EqualCondition(id_entidad, Where.FieldRelations.OR_)
            connection.executeDelete(queryBuilder.DeleteQuery(relacion))
            Return True
        Catch exc As Exception
            MyMaster.mostrarMensaje(exc.Message, True)

            Return False
        End Try
    End Function

    Protected Sub dg_Relaciones_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dg_Relaciones.PageIndexChanged
        dg_Relaciones.CurrentPageIndex = e.NewPageIndex
        'Dim id_Entidad As Integer = hdn_Entidad.Value
        'selectRelaciones_Entidades(id_Entidad)
    End Sub

    Public Event RequestExternalPage(ByVal laRuta As String, ByVal codigo As String)

    Protected Sub btn_Relaciones_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_Relaciones.Click
        RaiseEvent RequestExternalPage("~/Orbecatalog/Contactos/Relaciones_Entidades.aspx", "CO-08")
    End Sub
End Class
