Imports Orbelink.DBHandler
Imports Orbelink.Entity.Productos

Partial Class _Origenes
    Inherits PageBaseClass

    Const codigo_pantalla As String = "PR-08"
    Const level As Integer = 2

    'Pagina
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddScripts(level)
        securityHandler.VerifyPantalla(codigo_pantalla, level)
        If Not IsPostBack Then
            Me.Title = Resources.Orbecatalog_Resources.Orbecatalog & " - " & Resources.Productos_Resources.Origenes_Pantalla

            ClearInfo(Configuration.EstadoPantalla.NORMAL, False)
            securityHandler.VerifyActions(btn_Salvar, btn_Modificar, btn_Eliminar)

            selectOrigenes(trv_Origenes)
            selectOrigenes(trv_Perteneciente)
            trv_Perteneciente.Visible = False
        End If
    End Sub

    Protected Sub ClearInfo(ByVal estado As Configuration.EstadoPantalla, ByVal clearInfo As Boolean)
        If clearInfo Then
            vld_tbx_NombreOrigenes.IsValid = True
            tbx_NombreOrigenes.Text = ""
            tbx_NombreOrigenes.ToolTip = ""
        End If
        Select Case estado
            Case Configuration.EstadoPantalla.NORMAL
                btn_Modificar.Visible = False

                btn_Salvar.Visible = True

                btn_Eliminar.Visible = False


                lbl_OrigenesPerteneciente.ToolTip = ""
                lbl_OrigenesPerteneciente.Text = "Sin Asignar"
                pnl_Enlaces.Visible = False

            Case Configuration.EstadoPantalla.CONSULTAR
                btn_Modificar.Visible = True

                btn_Salvar.Visible = False

                btn_Eliminar.Visible = True


                pnl_Enlaces.Visible = True
        End Select
    End Sub

    'Origenes
    Protected Sub selectOrigenes(ByVal theTreeView As TreeView)
        Dim Origenes As New Origenes
        Dim dataset As Data.DataSet
        Dim counter As Integer
        Origenes.Fields.SelectAll()
        Origenes.Id_Origen.Where.EqualCondition_OnSameTable(Origenes.Id_padre)
        queryBuilder.Orderby.Add(Origenes.Nombre)
        dataset = connection.executeSelect(queryBuilder.SelectQuery(Origenes))
        If dataset.Tables.Count > 0 Then
            For counter = 0 To dataset.Tables(0).Rows.Count - 1
                ObjectBuilder.CreateObject(dataset.Tables(0), counter, Origenes)   'cambiar por transform
                Dim hijo As New TreeNode(Origenes.Nombre.Value, Origenes.Id_Origen.Value)
                theTreeView.Nodes.Add(hijo)
                selectOrigenesesHijos(theTreeView.Nodes(counter))
            Next
            theTreeView.CollapseAll()
        End If
    End Sub

    Protected Sub selectOrigenesesHijos(ByVal elTreeNode As TreeNode)
        Dim padre_Id_Origen As Integer = elTreeNode.Value
        Dim Origenes As New Origenes
        Dim dataset As Data.DataSet
        Dim counter As Integer

        Origenes.Fields.SelectAll()
        Origenes.Id_padre.Where.EqualCondition(padre_Id_Origen)
        Origenes.Id_Origen.Where.DiferentCondition(padre_Id_Origen)
        queryBuilder.Orderby.Add(Origenes.Nombre)
        dataset = connection.executeSelect(queryBuilder.SelectQuery(Origenes))

        Dim results As ArrayList = ObjectBuilder.TransformDataTable(dataset.Tables(0), Origenes)
        For counter = 0 To dataset.Tables(0).Rows.Count - 1
            Dim act_Origenes As Origenes = results(counter)
            Dim hijo As New TreeNode(act_Origenes.Nombre.Value, act_Origenes.Id_Origen.Value)
            selectOrigenesesHijos(hijo)
            elTreeNode.ChildNodes.Add(hijo)
        Next
    End Sub

    Protected Sub loadOrigenes(ByVal id_Origen As Integer)
        Dim dataSet As Data.DataSet
        Dim Origenes As New Origenes
        Dim Origenes2 As New Origenes
        Dim id_padre As Integer

        Origenes.Fields.SelectAll()
        Origenes.Id_Origen.Where.EqualCondition(id_Origen)
        dataSet = connection.executeSelect(queryBuilder.SelectQuery(Origenes))
        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables(0).Rows.Count > 0 Then
                ObjectBuilder.CreateObject(dataSet.Tables(0), 0, Origenes)
                tbx_NombreOrigenes.Text = Origenes.Nombre.Value
                tbx_NombreOrigenes.ToolTip = Origenes.Id_Origen.Value
                id_padre = Origenes.Id_padre.Value

                Origenes2.Nombre.ToSelect = True
                Origenes2.Id_Origen.ToSelect = True
                Origenes2.Id_Origen.Where.EqualCondition(id_padre)
                dataSet = connection.executeSelect(queryBuilder.SelectQuery(Origenes2))
                ObjectBuilder.CreateObject(dataSet.Tables(0), 0, Origenes2)
                lbl_OrigenesPerteneciente.Text = Origenes2.Nombre.Value
                lbl_OrigenesPerteneciente.ToolTip = Origenes2.Id_Origen.Value
            End If
        End If
    End Sub

    Protected Function insertOrigenes(ByVal id_father As Integer) As Boolean
        Dim Origenes As Origenes
        Try
            Origenes = New Origenes(id_father, tbx_NombreOrigenes.Text)
            connection.executeInsert(queryBuilder.InsertQuery(Origenes))

            Dim lastID As Integer = connection.lastKey(Origenes.TableName, Origenes.Id_Origen.Name)
            If id_father = 0 Then
                Origenes.Id_padre.Value = lastID
                Origenes.Id_Origen.Where.EqualCondition(lastID)
                connection.executeUpdate(queryBuilder.UpdateQuery(Origenes))
            End If
            Return True
        Catch exc As Exception
            MyMaster.mostrarMensaje(exc.Message, True)

            Return False
        End Try
    End Function

    Protected Function updateOrigenes(ByVal id_padre As Integer) As Boolean
        Dim Origenes As Origenes
        Try
            Origenes = New Origenes(id_padre, tbx_NombreOrigenes.Text)
            Origenes.Id_Origen.Where.EqualCondition(tbx_NombreOrigenes.ToolTip)
            connection.executeUpdate(queryBuilder.UpdateQuery(Origenes))
            Return True
        Catch exc As Exception
            MyMaster.mostrarMensaje(exc.Message, True)

            Return False
        End Try
    End Function

    Protected Function deleteOrigenes(ByVal id_Origen As Integer) As Boolean
        Dim Origenes As New Origenes
        Try
            Origenes.Id_Origen.Where.EqualCondition(id_Origen)
            connection.executeDelete(queryBuilder.DeleteQuery(Origenes))
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
            If lbl_OrigenesPerteneciente.ToolTip.Length > 0 Then
                id_father = lbl_OrigenesPerteneciente.ToolTip
            End If
            If insertOrigenes(id_father) Then
                trv_Origenes.Nodes.Clear()
                selectOrigenes(trv_Origenes)
                trv_Perteneciente.Nodes.Clear()
                selectOrigenes(trv_Perteneciente)
                ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Salvar, "Origen", False)
            Else
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Salvar, "origen", True)
            End If
        Else
            MyMaster.MostrarMensaje(Resources.Orbecatalog_Resources.CamposRequeridos, False)
        End If
    End Sub

    Protected Sub btn_Modificar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Modificar.Click

        If IsValid() Then
            Dim id_father As Integer
            If lbl_OrigenesPerteneciente.ToolTip.Length > 0 Then
                id_father = lbl_OrigenesPerteneciente.ToolTip
            End If
            If updateOrigenes(id_father) Then
                ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
                trv_Origenes.Nodes.Clear()
                selectOrigenes(trv_Origenes)
                trv_Perteneciente.Nodes.Clear()
                selectOrigenes(trv_Perteneciente)
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Modificar, "Origen", False)
            Else
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Modificar, "origen", True)
            End If
        Else
            MyMaster.MostrarMensaje(Resources.Orbecatalog_Resources.CamposRequeridos, False)
        End If
    End Sub

    Protected Sub btn_Cancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Cancelar.Click
        ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
    End Sub

    Protected Sub btn_Eliminar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Eliminar.Click

        If deleteOrigenes(tbx_NombreOrigenes.ToolTip) Then
            ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
            trv_Origenes.Nodes.Clear()
            selectOrigenes(trv_Origenes)
            trv_Perteneciente.Nodes.Clear()
            selectOrigenes(trv_Perteneciente)
            MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Eliminar, "Origen", False)
        Else
            MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Eliminar, "origen", True)
        End If
    End Sub

    Protected Sub trv_Origenes_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles trv_Origenes.SelectedNodeChanged
        Dim id_Origen As Integer = trv_Origenes.SelectedNode.Value
        ClearInfo(Configuration.EstadoPantalla.CONSULTAR, True)
        loadOrigenes(id_Origen)
        lnk_Archivo.NavigateUrl = "Archivo_Origen.aspx?id_Origen=" & id_Origen
    End Sub

    Protected Sub trv_Perteneciente_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles trv_Perteneciente.SelectedNodeChanged
        lbl_OrigenesPerteneciente.Text = trv_Perteneciente.SelectedNode.Text
        lbl_OrigenesPerteneciente.ToolTip = trv_Perteneciente.SelectedNode.Value
        trv_Perteneciente.Visible = False
    End Sub

    Protected Sub lbl_OrigenesPerteneciente_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbl_OrigenesPerteneciente.Click
        trv_Perteneciente.Visible = True
    End Sub

End Class
