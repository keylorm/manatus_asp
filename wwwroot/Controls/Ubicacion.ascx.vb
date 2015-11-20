Imports Orbelink.Orbecatalog6
Imports Orbelink.Entity.Entidades

Partial Class EntidadUbicacion
    Inherits System.Web.UI.UserControl

    Dim queryBuilder As Orbelink.DBHandler.QueryBuilder
    Dim connection As Orbelink.DBHandler.SQLServer
    Dim securityHandler As Orbelink.Control.Security.SecurityHandler
    Dim codigo_pantalla As String
    Dim MyMaster As Orbelink.Orbecatalog6.MasterPageBaseClass

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            lnk_Ubicacion.NavigateUrl = "javascript:toggleLayer('" & trv_Ubicaciones.ClientID & "', this)"
        End If
    End Sub

    Public Sub SetVariables(ByVal theConnection As Orbelink.DBHandler.SQLServer, _
                           ByVal theSecurityHandler As Orbelink.Control.Security.SecurityHandler, _
                           ByVal theCodigo_pantalla As String, _
                           ByVal theMaster As Orbelink.Orbecatalog6.MasterPageBaseClass)
        queryBuilder = New Orbelink.DBHandler.QueryBuilder()
        connection = theConnection
        securityHandler = theSecurityHandler
        codigo_pantalla = theCodigo_pantalla
        MyMaster = theMaster
    End Sub

    Public Sub ClearInfo(ByVal estado As Configuration.EstadoPantalla, ByVal clearInfo As Boolean)
        If clearInfo Then
            vld_Ubicacion.IsValid = True
        End If
    End Sub

    Public Sub loadUbicacion(ByVal ubicacion As Ubicacion)
        lnk_Ubicacion.ToolTip = ubicacion.Id_ubicacion.Value
        lnk_Ubicacion.Text = ubicacion.Nombre.Value
        upd_Ubicacion.Update()
    End Sub

    Public Function Ubicacion_Selected() As Integer
        If lnk_Ubicacion.ToolTip.Length > 0 Then
            Return lnk_Ubicacion.ToolTip
        Else
            Return 0
        End If
    End Function

    'Ubicaciones
    Public Sub selectUbicaciones()
        selectUbicaciones(Me.trv_Ubicaciones)
    End Sub

    Protected Sub selectUbicaciones(ByVal theTreeView As TreeView)
        Dim ubicacion As New Ubicacion
        Dim dataset As Data.DataSet
        Dim counter As Integer
        Dim primero As Boolean = True

        ubicacion.Fields.SelectAll()
        ubicacion.Id_ubicacion.Where.EqualCondition_OnSameTable(ubicacion.Id_padre)
        dataset = connection.executeSelect(queryBuilder.SelectQuery(ubicacion))
        If dataset.Tables.Count > 0 Then
            Dim result_ubicacion As ArrayList = Orbelink.DBHandler.ObjectBuilder.TransformDataTable(dataset.Tables(0), ubicacion)
            theTreeView.Nodes.Clear()
            For counter = 0 To dataset.Tables(0).Rows.Count - 1
                Dim act_ubicacion As Ubicacion = result_ubicacion(counter)
                Dim hijo As New TreeNode(act_ubicacion.Nombre.Value, act_ubicacion.Id_ubicacion.Value)
                theTreeView.Nodes.Add(hijo)
                If primero Then
                    lnk_Ubicacion.Text = act_ubicacion.Nombre.Value
                    lnk_Ubicacion.ToolTip = act_ubicacion.Id_ubicacion.Value
                    primero = False
                End If
                selectUbicacionesHijos(theTreeView.Nodes(counter))
            Next
        End If
        theTreeView.CollapseAll()
        Me.upd_Ubicacion.Update()
    End Sub

    Protected Sub selectUbicacionesHijos(ByVal elTreeNode As TreeNode)
        Dim padre_id_ubicacion As Integer = elTreeNode.Value
        Dim ubicacion As New Ubicacion
        Dim dataset As Data.DataSet
        Dim counter As Integer

        ubicacion.Fields.SelectAll()
        ubicacion.Id_padre.Where.EqualCondition(padre_id_ubicacion)
        ubicacion.Id_ubicacion.Where.DiferentCondition(padre_id_ubicacion)
        dataset = connection.executeSelect(queryBuilder.SelectQuery(ubicacion))

        For counter = 0 To dataset.Tables(0).Rows.Count - 1
            Orbelink.DBHandler.ObjectBuilder.CreateObject(dataset.Tables(0), counter, ubicacion)   'cambiar por transform
            Dim hijo As New TreeNode(ubicacion.Nombre.Value, ubicacion.Id_ubicacion.Value)
            selectUbicacionesHijos(hijo)
            elTreeNode.ChildNodes.Add(hijo)
        Next
    End Sub

    Public Function IsValid() As Boolean
        If lnk_Ubicacion.ToolTip.Length = 0 Then
            vld_Ubicacion.IsValid = False
            MyMaster.concatenarMensaje("Debe seleccionar una ubicacion.", False)
            Return False
        Else
            vld_Ubicacion.IsValid = True
            Return True
        End If
    End Function

    Protected Sub trv_Ubicaciones_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles trv_Ubicaciones.SelectedNodeChanged
        Dim id_NodoActual As Integer = trv_Ubicaciones.SelectedNode.Value
        lnk_Ubicacion.Text = trv_Ubicaciones.SelectedNode.Text
        lnk_Ubicacion.ToolTip = id_NodoActual
        upd_Ubicacion.Update()
        RaiseEvent Ubicacion_SelectedChanged(id_NodoActual)
    End Sub

    Public Sub Update()
        Me.upd_Ubicacion.Update()
    End Sub

    'Eventos
    Public Event Ubicacion_SelectedChanged(ByVal id_UbicacionSeleccionado As Integer)

    Public Event RequestExternalPage(ByVal laRuta As String, ByVal codigo As String)

    'Eventos Handler
    Protected Sub btn_AgregarUbicacion_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_AgregarUbicacion.Click
        RaiseEvent RequestExternalPage("Ubicacion.aspx", "CO-10")
    End Sub
End Class
