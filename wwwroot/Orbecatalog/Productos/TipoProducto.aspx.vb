Imports Orbelink.DBHandler
Imports Orbelink.Entity.Productos

Partial Class _TipoProducto
    Inherits PageBaseClass

    Const codigo_pantalla As String = "PR-03"
    Const level As Integer = 2

    'ViewState
    Dim _tempPerteneciente As Integer
    Protected Property Perteneciente() As Integer
        Get
            If _tempPerteneciente <= 0 Then
                If ViewState("vs_Perteneciente") IsNot Nothing Then
                    _tempPerteneciente = ViewState("vs_Perteneciente")
                Else
                    ViewState.Add("vs_Perteneciente", 0)
                    _tempPerteneciente = 0
                End If
            End If
            Return _tempPerteneciente
        End Get
        Set(ByVal value As Integer)
            _tempPerteneciente = value
            ViewState("vs_Perteneciente") = _tempPerteneciente
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddScripts(level)
        securityHandler.VerifyPantalla(codigo_pantalla, level)

        If Not IsPostBack Then
            Me.Title = Resources.Orbecatalog_Resources.Orbecatalog & " - " & Resources.Productos_Resources.TipoProducto_Pantalla

            ClearInfo(Configuration.EstadoPantalla.NORMAL, False)
            securityHandler.VerifyActions(btn_Salvar, btn_Modificar, btn_Eliminar)

            selectTipoProducto(trv_TipoProducto)
            selectTipoProducto(trv_Perteneciente)

            lnk_Perteneciente.NavigateUrl = "javascript:toggleLayer('" & trv_Perteneciente.ClientID & "', this)"
        End If
    End Sub

    Protected Sub ClearInfo(ByVal estado As Configuration.EstadoPantalla, ByVal clearInfo As Boolean)
        If clearInfo Then
            vld_tbx_NombreTipoProducto.IsValid = True
            tbx_NombreTipoProducto.Text = ""
            tbx_NombreTipoProducto.ToolTip = ""
            tbx_Codigo.Text = ""
        End If
        Select Case estado
            Case Configuration.EstadoPantalla.NORMAL
                btn_Modificar.Visible = False

                btn_Salvar.Visible = True

                btn_Eliminar.Visible = False

                Perteneciente = 0
                lnk_Perteneciente.Text = "(No perteneciente, primer nivel)"

                pnl_Enlaces.Visible = False
            Case Configuration.EstadoPantalla.CONSULTAR
                btn_Modificar.Visible = True

                btn_Salvar.Visible = False

                btn_Eliminar.Visible = True


                pnl_Enlaces.Visible = True
        End Select
    End Sub

    'TipoProducto
    Protected Sub selectTipoProducto(ByVal theTreeView As TreeView)
        Dim TipoProducto As New TipoProducto
        Dim dataset As Data.DataSet
        Dim counter As Integer
        TipoProducto.Fields.SelectAll()
        TipoProducto.Id_TipoProducto.Where.EqualCondition_OnSameTable(TipoProducto.Id_padre)
        queryBuilder.Orderby.Add(TipoProducto.Nombre)
        dataset = connection.executeSelect(queryBuilder.SelectQuery(TipoProducto))
        If dataset.Tables.Count > 0 Then
            For counter = 0 To dataset.Tables(0).Rows.Count - 1
                ObjectBuilder.CreateObject(dataset.Tables(0), counter, TipoProducto)   'cambiar por transform
                Dim hijo As New TreeNode(TipoProducto.Nombre.Value, TipoProducto.Id_TipoProducto.Value)
                theTreeView.Nodes.Add(hijo)
                selectTipoProductoesHijos(theTreeView.Nodes(counter))
            Next
            theTreeView.CollapseAll()
        End If
    End Sub

    Protected Sub selectTipoProductoesHijos(ByVal elTreeNode As TreeNode)
        Dim padre_Id_TipoProducto As Integer = elTreeNode.Value
        Dim TipoProducto As New TipoProducto
        Dim dataset As Data.DataSet
        Dim counter As Integer

        TipoProducto.Fields.SelectAll()
        TipoProducto.Id_padre.Where.EqualCondition(padre_Id_TipoProducto)
        TipoProducto.Id_TipoProducto.Where.DiferentCondition(padre_Id_TipoProducto)
        queryBuilder.Orderby.Add(TipoProducto.Nombre)
        dataset = connection.executeSelect(queryBuilder.SelectQuery(TipoProducto))

        Dim results As ArrayList = ObjectBuilder.TransformDataTable(dataset.Tables(0), TipoProducto)
        For counter = 0 To dataset.Tables(0).Rows.Count - 1
            Dim act_TipoProducto As TipoProducto = results(counter)
            Dim hijo As New TreeNode(act_TipoProducto.Nombre.Value, act_TipoProducto.Id_TipoProducto.Value)
            selectTipoProductoesHijos(hijo)
            elTreeNode.ChildNodes.Add(hijo)
        Next
    End Sub

    Protected Sub loadTipoProducto(ByVal Id_TipoProducto As Integer)
        Dim dataSet As Data.DataSet
        Dim TipoProducto As New TipoProducto
        Dim TipoProducto2 As New TipoProducto
        Dim id_padre As Integer

        TipoProducto.Fields.SelectAll()
        TipoProducto.Id_TipoProducto.Where.EqualCondition(Id_TipoProducto)
        dataSet = connection.executeSelect(queryBuilder.SelectQuery(TipoProducto))
        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables(0).Rows.Count > 0 Then
                ObjectBuilder.CreateObject(dataSet.Tables(0), 0, TipoProducto)

                tbx_NombreTipoProducto.Text = TipoProducto.Nombre.Value
                tbx_NombreTipoProducto.ToolTip = TipoProducto.Id_TipoProducto.Value
                tbx_Codigo.Text = TipoProducto.Codigo.Value
                id_padre = TipoProducto.Id_padre.Value

                TipoProducto2.Nombre.ToSelect = True
                TipoProducto2.Id_TipoProducto.ToSelect = True
                TipoProducto2.Id_TipoProducto.Where.EqualCondition(id_padre)
                dataSet = connection.executeSelect(queryBuilder.SelectQuery(TipoProducto2))
                ObjectBuilder.CreateObject(dataSet.Tables(0), 0, TipoProducto2)
                lnk_Perteneciente.Text = TipoProducto2.Nombre.Value
                Perteneciente = TipoProducto2.Id_TipoProducto.Value
            End If
        End If
    End Sub

    Protected Function insertTipoProducto(ByVal id_father As Integer) As Boolean
        Dim TipoProducto As TipoProducto
        Try
            TipoProducto = New TipoProducto(id_father, tbx_NombreTipoProducto.Text, tbx_Codigo.Text)
            connection.executeInsert(queryBuilder.InsertQuery(TipoProducto))

            Dim lastID As Integer = connection.lastKey(TipoProducto.TableName, TipoProducto.Id_TipoProducto.Name)
            If id_father = 0 Then
                TipoProducto.Id_padre.Value = lastID
                TipoProducto.Id_TipoProducto.Where.EqualCondition(lastID)
                connection.executeUpdate(queryBuilder.UpdateQuery(TipoProducto))
            End If
            Return True
        Catch exc As Exception
            MyMaster.mostrarMensaje(exc.Message, True)

            Return False
        End Try
    End Function

    Protected Function updateTipoProducto(ByVal id_padre As Integer) As Boolean
        Dim TipoProducto As TipoProducto
        Try
            TipoProducto = New TipoProducto(id_padre, tbx_NombreTipoProducto.Text, tbx_Codigo.Text)
            TipoProducto.Id_TipoProducto.Where.EqualCondition(tbx_NombreTipoProducto.ToolTip)
            connection.executeUpdate(queryBuilder.UpdateQuery(TipoProducto))
            Return True
        Catch exc As Exception
            MyMaster.mostrarMensaje(exc.Message, True)

            Return False
        End Try
    End Function

    Protected Function deleteTipoProducto(ByVal Id_TipoProducto As Integer) As Boolean
        Dim TipoProducto As New TipoProducto
        Try
            TipoProducto.Id_TipoProducto.Where.EqualCondition(Id_TipoProducto)
            connection.executeDelete(queryBuilder.DeleteQuery(TipoProducto))
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
            If Perteneciente > 0 Then
                id_father = Perteneciente
            End If
            If insertTipoProducto(id_father) Then
                trv_TipoProducto.Nodes.Clear()
                selectTipoProducto(trv_TipoProducto)
                trv_Perteneciente.Nodes.Clear()
                selectTipoProducto(trv_Perteneciente)
                ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Salvar, "TipoProducto", False)
            Else
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Salvar, "TipoProducto", True)
            End If
        Else
            MyMaster.MostrarMensaje(Resources.Orbecatalog_Resources.CamposRequeridos, False)
        End If
    End Sub

    Protected Sub btn_Modificar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Modificar.Click
        If IsValid() Then
            Dim id_father As Integer
            If Perteneciente > 0 Then
                id_father = Perteneciente
            End If
            If updateTipoProducto(id_father) Then
                ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
                trv_TipoProducto.Nodes.Clear()
                selectTipoProducto(trv_TipoProducto)
                trv_Perteneciente.Nodes.Clear()
                selectTipoProducto(trv_Perteneciente)
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Modificar, "TipoProducto", False)
            Else
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Modificar, "TipoProducto", True)
            End If
        Else
            MyMaster.MostrarMensaje(Resources.Orbecatalog_Resources.CamposRequeridos, False)
        End If
    End Sub

    Protected Sub btn_Cancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Cancelar.Click
        ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
    End Sub

    Protected Sub btn_Eliminar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Eliminar.Click

        If deleteTipoProducto(tbx_NombreTipoProducto.ToolTip) Then
            ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
            trv_TipoProducto.Nodes.Clear()
            selectTipoProducto(trv_TipoProducto)
            trv_Perteneciente.Nodes.Clear()
            selectTipoProducto(trv_Perteneciente)
            MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Eliminar, "TipoProducto", False)
        Else
            MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Eliminar, "TipoProducto", True)
        End If
    End Sub

    Protected Sub trv_TipoProducto_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles trv_TipoProducto.SelectedNodeChanged
        Dim id_NodoActual As Integer = trv_TipoProducto.SelectedNode.Value
        ClearInfo(Configuration.EstadoPantalla.CONSULTAR, True)
        loadTipoProducto(id_NodoActual)
        Me.lnk_Archivo.NavigateUrl = "Archivo_TipoProducto.aspx?id_tipoProducto=" & id_NodoActual
    End Sub

    Protected Sub trv_Perteneciente_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles trv_Perteneciente.SelectedNodeChanged
        lnk_Perteneciente.Text = trv_Perteneciente.SelectedNode.Text
        Perteneciente = trv_Perteneciente.SelectedNode.Value
        trv_Perteneciente.Style("display") = "none"
    End Sub


End Class
