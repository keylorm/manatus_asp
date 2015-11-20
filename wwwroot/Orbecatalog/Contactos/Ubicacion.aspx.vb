Imports Orbelink.DBHandler
Imports Orbelink.Entity.Entidades

Partial Class _Ubicacion
    Inherits PageBaseClass

    Const codigo_pantalla As String = "CO-10"
    Const level As Integer = 2

    'Pagina
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddScripts(level)
        securityHandler.VerifyPantalla(codigo_pantalla, level)
        If Not IsPostBack Then
            securityHandler.VerifyActions(btn_Salvar, btn_Modificar, btn_Eliminar)
            SetThemeProperties()
            SetControlProperties()

            selectUbicaciones(trv_Ubicaciones)
            selectUbicaciones(trv_Perteneciente)
            ClearInfo(Configuration.EstadoPantalla.NORMAL)
        End If
    End Sub

    Protected Sub ClearInfo(ByVal estado As Configuration.EstadoPantalla)
        vld_tbx_NombreUbicacion.IsValid = True
        tbx_NombreUbicacion.Text = ""
        tbx_NombreUbicacion.ToolTip = ""
        Select Case estado
            Case Configuration.EstadoPantalla.NORMAL
                btn_Modificar.Visible = False
                btn_Salvar.Visible = True
                btn_Eliminar.Visible = False

                lbl_UbicacionPerteneciente.ToolTip = ""
                lbl_UbicacionPerteneciente.Text = "Sin Asignar"
            Case Configuration.EstadoPantalla.CONSULTAR
                btn_Modificar.Visible = True
                btn_Salvar.Visible = False
                btn_Eliminar.Visible = True

        End Select
    End Sub

    Protected Sub SetThemeProperties()




    End Sub

    Protected Sub SetControlProperties()
        tbx_NombreUbicacion.Attributes.Add("maxlength", 50)
        tbx_NombreUbicacion.Attributes.Add("onkeypress", "showTextProgress(this)")
    End Sub

    'Ubicacion
    Protected Sub selectUbicaciones(ByVal theTreeView As TreeView)
        Dim ubicacion As New Ubicacion
        Dim dataset As Data.DataSet
        Dim counter As Integer

        ubicacion.Fields.SelectAll()
        ubicacion.Id_ubicacion.Where.EqualCondition_OnSameTable(ubicacion.Id_padre)
        dataset = connection.executeSelect(queryBuilder.SelectQuery(ubicacion))
        If dataset.Tables.Count > 0 Then
            If dataset.Tables(0).Rows.Count > 0 Then
                Dim results_Ubicacion As ArrayList = ObjectBuilder.TransformDataTable(dataset.Tables(0), ubicacion)
                For counter = 0 To dataset.Tables(0).Rows.Count - 1
                    Dim act_Ubicacion As Ubicacion = results_Ubicacion(counter)
                    Dim hijo As New TreeNode(act_Ubicacion.Nombre.Value, act_Ubicacion.Id_ubicacion.Value)
                    theTreeView.Nodes.Add(hijo)
                    selectUbicacionesHijos(theTreeView.Nodes(counter))
                Next
                theTreeView.CollapseAll()
            End If
        End If
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
        If dataset.Tables.Count > 0 Then
            If dataset.Tables(0).Rows.Count > 0 Then
                Dim results_Ubicacion As ArrayList = ObjectBuilder.TransformDataTable(dataset.Tables(0), ubicacion)
                For counter = 0 To dataset.Tables(0).Rows.Count - 1
                    Dim act_Ubicacion As Ubicacion = results_Ubicacion(counter)
                    Dim hijo As New TreeNode(act_Ubicacion.Nombre.Value, act_Ubicacion.Id_ubicacion.Value)
                    selectUbicacionesHijos(hijo)
                    elTreeNode.ChildNodes.Add(hijo)
                Next
            End If
        End If
    End Sub

    Protected Sub loadUbicacion(ByVal id_Ubicacion As Integer)
        Dim dataSet As Data.DataSet
        Dim Ubicacion As New Ubicacion
        Dim ubicacion2 As New Ubicacion
        Dim id_padre As Integer

        Ubicacion.Fields.SelectAll()
        Ubicacion.Id_ubicacion.Where.EqualCondition(id_Ubicacion)
        dataSet = connection.executeSelect(queryBuilder.SelectQuery(Ubicacion))
        ObjectBuilder.CreateObject(dataSet.Tables(0), 0, Ubicacion)
        tbx_NombreUbicacion.Text = Ubicacion.Nombre.Value
        tbx_NombreUbicacion.ToolTip = Ubicacion.Id_ubicacion.Value
        id_padre = Ubicacion.Id_padre.Value

        ubicacion2.Nombre.ToSelect = True
        ubicacion2.Id_ubicacion.ToSelect = True
        ubicacion2.Id_ubicacion.Where.EqualCondition(id_padre)
        dataSet = connection.executeSelect(queryBuilder.SelectQuery(ubicacion2))
        ObjectBuilder.CreateObject(dataSet.Tables(0), 0, ubicacion2)
        lbl_UbicacionPerteneciente.Text = ubicacion2.Nombre.Value
        lbl_UbicacionPerteneciente.ToolTip = ubicacion2.Id_ubicacion.Value
    End Sub

    Protected Function updateUbicacion(ByVal Id_ubicacion As Integer, ByVal id_father As Integer) As Boolean
        Dim Ubicacion As Ubicacion
        Try
            Ubicacion = New Ubicacion(id_father, tbx_NombreUbicacion.Text)
            Ubicacion.Id_ubicacion.Where.EqualCondition(tbx_NombreUbicacion.ToolTip)
            connection.executeUpdate(queryBuilder.UpdateQuery(Ubicacion))
            Return True
        Catch exc As Exception
            MyMaster.mostrarMensaje(exc.Message, True)

            Return False
        End Try
    End Function

    Protected Function insertUbicacion(ByVal id_father As Integer) As Boolean
        Dim ubicacion As Ubicacion
        Try
            ubicacion = New Ubicacion(id_father, tbx_NombreUbicacion.Text)
            connection.executeInsert(queryBuilder.InsertQuery(ubicacion))
            ClearInfo(Configuration.EstadoPantalla.NORMAL)
            If id_father = 0 Then
                Dim lastID As Integer = connection.lastKey(ubicacion.TableName, ubicacion.Id_ubicacion.Name)
                ubicacion.Id_ubicacion.Where.EqualCondition(lastID)
                ubicacion.Id_padre.Value = lastID
                connection.executeUpdate(queryBuilder.UpdateQuery(ubicacion))
            End If
            Return True
        Catch exc As Exception
            MyMaster.mostrarMensaje(exc.Message, True)

            Return False
        End Try
    End Function

    Protected Function deleteUbicacion(ByVal id_Ubicacion As Integer) As Boolean
        Dim Ubicacion As New Ubicacion
        Try
            Ubicacion.Id_ubicacion.Where.EqualCondition(id_Ubicacion)
            connection.executeDelete(queryBuilder.DeleteQuery(Ubicacion))
            Return True
        Catch exc As Exception
            MyMaster.mostrarMensaje(exc.Message, True)

            Return False
        End Try
    End Function

    'Eventos
    Protected Sub btn_Salvar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Salvar.Click

        If IsValid() Then
            Dim id_father As Integer
            If lbl_UbicacionPerteneciente.ToolTip.Length > 0 Then
                id_father = lbl_UbicacionPerteneciente.ToolTip
            End If
            If insertUbicacion(id_father) Then
                trv_Ubicaciones.Nodes.Clear()
                selectUbicaciones(trv_Ubicaciones)
                trv_Perteneciente.Nodes.Clear()
                selectUbicaciones(trv_Perteneciente)
                ClearInfo(Configuration.EstadoPantalla.NORMAL)
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Salvar, "Ubicacion", False)
            Else
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Salvar, "ubicacion", True)
            End If
        Else
            MyMaster.MostrarMensaje(Resources.Orbecatalog_Resources.CamposRequeridos, False)
        End If
        upd_Contenido.Update()
        upd_Busqueda.Update()
    End Sub

    Protected Sub btn_Modificar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Modificar.Click

        If IsValid() Then
            Dim id_father As Integer
            If lbl_UbicacionPerteneciente.ToolTip.Length > 0 Then
                id_father = lbl_UbicacionPerteneciente.ToolTip
            End If
            If updateUbicacion(tbx_NombreUbicacion.ToolTip, id_father) Then
                trv_Ubicaciones.Nodes.Clear()
                selectUbicaciones(trv_Ubicaciones)
                trv_Perteneciente.Nodes.Clear()
                selectUbicaciones(trv_Perteneciente)

                'ClearInfo(Configuration.EstadoPantalla.NORMAL)
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Modificar, "Ubicacion", False)
            Else
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Modificar, "ubicacion", True)
            End If
        Else
            MyMaster.MostrarMensaje(Resources.Orbecatalog_Resources.CamposRequeridos, False)
        End If
        upd_Busqueda.Update()
    End Sub

    Protected Sub btn_Cancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Cancelar.Click
        ClearInfo(Configuration.EstadoPantalla.NORMAL)
        upd_Contenido.Update()
        upd_Busqueda.Update()
    End Sub

    Protected Sub btn_Eliminar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Eliminar.Click

        If deleteUbicacion(tbx_NombreUbicacion.ToolTip) Then
            ClearInfo(Configuration.EstadoPantalla.NORMAL)
            trv_Ubicaciones.Nodes.Clear()
            selectUbicaciones(trv_Ubicaciones)
            trv_Perteneciente.Nodes.Clear()
            selectUbicaciones(trv_Perteneciente)
            MyMaster.MostrarMensaje("Ubicacion borrada exitosamente.", False)
        Else
            MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Eliminar, "ubicacion", True)
        End If
        upd_Contenido.Update()
        upd_Busqueda.Update()
    End Sub

    Protected Sub trv_Ubicaciones_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles trv_Ubicaciones.SelectedNodeChanged
        Dim id_NodoActual As Integer = trv_Ubicaciones.SelectedNode.Value
        ClearInfo(Configuration.EstadoPantalla.CONSULTAR)
        loadUbicacion(id_NodoActual)
        upd_Contenido.Update()
    End Sub

    Protected Sub trv_Perteneciente_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles trv_Perteneciente.SelectedNodeChanged
        lbl_UbicacionPerteneciente.Text = trv_Perteneciente.SelectedNode.Text
        lbl_UbicacionPerteneciente.ToolTip = trv_Perteneciente.SelectedNode.Value
    End Sub
End Class
