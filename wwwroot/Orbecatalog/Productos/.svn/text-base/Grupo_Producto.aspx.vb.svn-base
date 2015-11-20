Imports Orbelink.DBHandler
Imports Orbelink.Entity.Productos

Partial Class _Grupo
    Inherits PageBaseClass

    Const Descripcion_pantalla As String = "PR-19"
    Const level As Integer = 2

    'Pagina

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddScripts(level)
        securityHandler.VerifyPantalla(Descripcion_pantalla, level)
        If Not IsPostBack Then
            Me.Title = Resources.Orbecatalog_Resources.Orbecatalog & " - " & Resources.Productos_Resources.Grupos_Pantalla

            securityHandler.VerifyActions(btn_Salvar, btn_Modificar, btn_Eliminar)
            ClearInfo(Configuration.EstadoPantalla.NORMAL, False)
            SetThemeProperties()
            SetControlProperties()

            selectGrupos_Productos(trv_Grupos_Productos)
            selectGrupos_Productos(trv_Perteneciente)
            'trv_Perteneciente.Visible = False
        End If
    End Sub

    Protected Sub ClearInfo(ByVal estado As Configuration.EstadoPantalla, ByVal clearInfo As Boolean)
        If clearInfo Then
            vld_tbx_NombreGrupo.IsValid = True
            tbx_NombreGrupo.Text = ""
            tbx_NombreGrupo.ToolTip = ""
            tbx_Descripcion.Text = ""
        End If

        Select Case estado
            Case Configuration.EstadoPantalla.NORMAL
                btn_Modificar.Visible = False

                btn_Salvar.Visible = True

                btn_Eliminar.Visible = False

                lbl_GrupoPerteneciente.ToolTip = ""
                lbl_GrupoPerteneciente.Text = "Sin Asignar"

            Case Configuration.EstadoPantalla.CONSULTAR
                btn_Modificar.Visible = True

                btn_Salvar.Visible = False

                btn_Eliminar.Visible = True


        End Select
    End Sub

    Protected Sub SetThemeProperties()




    End Sub

    Protected Sub SetControlProperties()
        tbx_NombreGrupo.Attributes.Add("maxlength", 50)
        tbx_NombreGrupo.Attributes.Add("onkeypress", "showTextProgress(this)")
        tbx_Descripcion.Attributes.Add("maxlength", 200)
        tbx_Descripcion.Attributes.Add("onkeypress", "showTextProgress(this)")
    End Sub

    'Grupos_Productos
    Protected Sub selectGrupos_Productos(ByVal theTreeView As TreeView)
        Dim Grupos_Productos As New Grupos_Productos
        Dim dataset As Data.DataSet
        Dim counter As Integer

        Grupos_Productos.Fields.SelectAll()
        Grupos_Productos.Id_Grupo.Where.EqualCondition_OnSameTable(Grupos_Productos.Id_padre)
        queryBuilder.Orderby.Add(Grupos_Productos.Nombre)

        dataset = connection.executeSelect(queryBuilder.SelectQuery(Grupos_Productos))
        If dataset.Tables.Count > 0 Then
            For counter = 0 To dataset.Tables(0).Rows.Count - 1
                ObjectBuilder.CreateObject(dataset.Tables(0), counter, Grupos_Productos)   'cambiar por transform
                Dim hijo As New TreeNode(Grupos_Productos.Nombre.Value, Grupos_Productos.Id_Grupo.Value)
                theTreeView.Nodes.Add(hijo)
                selectGrupos_ProductosHijos(theTreeView.Nodes(counter))
            Next
            theTreeView.CollapseAll()
        Else
            theTreeView.Visible = False
        End If
    End Sub

    Protected Function loadGrupo(ByVal id_Grupo As Integer) As Boolean
        Dim dataSet As Data.DataSet
        Dim Grupos_Productos As New Grupos_Productos
        Dim id_padre As Integer
        Dim loaded As Boolean = False

        Grupos_Productos.Fields.SelectAll()
        Grupos_Productos.Id_Grupo.Where.EqualCondition(id_Grupo)
        dataSet = connection.executeSelect(queryBuilder.SelectQuery(Grupos_Productos))
        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables(0).Rows.Count > 0 Then
                ObjectBuilder.CreateObject(dataSet.Tables(0), 0, Grupos_Productos)

                tbx_NombreGrupo.Text = Grupos_Productos.Nombre.Value
                tbx_NombreGrupo.ToolTip = Grupos_Productos.Id_Grupo.Value
                tbx_Descripcion.Text = Grupos_Productos.Descripcion.Value

                id_padre = Grupos_Productos.Id_padre.Value

                Grupos_Productos.Fields.ClearAll()
                Grupos_Productos.Nombre.ToSelect = True
                Grupos_Productos.Id_Grupo.ToSelect = True
                Grupos_Productos.Id_Grupo.Where.EqualCondition(id_padre)
                dataSet = connection.executeSelect(queryBuilder.SelectQuery(Grupos_Productos))
                ObjectBuilder.CreateObject(dataSet.Tables(0), 0, Grupos_Productos)
                lbl_GrupoPerteneciente.Text = Grupos_Productos.Nombre.Value
                lbl_GrupoPerteneciente.ToolTip = Grupos_Productos.Id_Grupo.Value

                loaded = True
            End If
        End If
        Return loaded
    End Function

    Protected Function insertGrupo(ByVal id_father As Integer) As Boolean
        Dim Grupos_Productos As Grupos_Productos
        Try
            Grupos_Productos = New Grupos_Productos(id_father, tbx_NombreGrupo.Text, tbx_Descripcion.Text)
            connection.executeInsert(queryBuilder.InsertQuery(Grupos_Productos))
            If id_father = 0 Then
                Dim lastID As Integer = connection.lastKey(Grupos_Productos.TableName, Grupos_Productos.Id_Grupo.Name)
                Grupos_Productos.Id_Grupo.Where.EqualCondition(lastID)
                Grupos_Productos.Id_padre.Value = lastID
                connection.executeUpdate(queryBuilder.UpdateQuery(Grupos_Productos))
            End If
            Return True
        Catch exc As Exception
            MyMaster.mostrarMensaje(exc.Message, True)

            Return False
        End Try
    End Function

    Protected Function updateGrupo(ByVal id_father As Integer) As Boolean
        Dim Grupos_Productos As Grupos_Productos

        Try
            Grupos_Productos = New Grupos_Productos(id_father, tbx_NombreGrupo.Text, tbx_Descripcion.Text)
            Grupos_Productos.Id_Grupo.Where.EqualCondition(tbx_NombreGrupo.ToolTip)
            connection.executeUpdate(queryBuilder.UpdateQuery(Grupos_Productos))
            Return True
        Catch exc As Exception
            MyMaster.mostrarMensaje(exc.Message, True)

            Return False
        End Try
    End Function

    Protected Function deleteGrupo() As Boolean
        Dim Grupos_Productos As New Grupos_Productos
        Try
            Grupos_Productos.Id_Grupo.Where.EqualCondition(tbx_NombreGrupo.ToolTip)
            connection.executeDelete(queryBuilder.DeleteQuery(Grupos_Productos))
            Return True
        Catch exc As Exception
            MyMaster.mostrarMensaje(exc.Message, True)

            Return False
        End Try
    End Function

    'Grupos_Productos Pertenecientes
    Protected Sub selectGrupos_ProductosHijos(ByVal elTreeNode As TreeNode)
        Dim padre_Id_Grupo_Producto As Integer = elTreeNode.Value
        Dim Grupos_Productos As New Grupos_Productos
        Dim dataset As Data.DataSet

        Grupos_Productos.Fields.SelectAll()
        Grupos_Productos.Id_padre.Where.EqualCondition(padre_Id_Grupo_Producto)
        Grupos_Productos.Id_Grupo.Where.DiferentCondition(padre_Id_Grupo_Producto)
        queryBuilder.OrderBy.Add(Grupos_Productos.Nombre)
        dataset = connection.executeSelect(queryBuilder.SelectQuery(Grupos_Productos))

        Dim results As ArrayList = objectBuilder.TransformDataTable(dataset.Tables(0), Grupos_Productos)
        For counter As Integer = 0 To dataset.Tables(0).Rows.Count - 1
            Dim act_Grupo As Grupos_Productos = results(counter)
            Dim hijo As New TreeNode(act_Grupo.Nombre.Value, act_Grupo.Id_Grupo.Value)
            selectGrupos_ProductosHijos(hijo)
            elTreeNode.ChildNodes.Add(hijo)
        Next
    End Sub

    'Eventos
    Protected Sub btn_Salvar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Salvar.Click

        If IsValid() Then
            Dim id_perteneciente As Integer
            If lbl_GrupoPerteneciente.ToolTip.Length > 0 Then
                id_perteneciente = lbl_GrupoPerteneciente.ToolTip
            End If
            If insertGrupo(id_perteneciente) Then
                ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
                trv_Grupos_Productos.Nodes.Clear()
                selectGrupos_Productos(trv_Grupos_Productos)
                trv_Perteneciente.Nodes.Clear()
                selectGrupos_Productos(trv_Perteneciente)
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Salvar, "Grupo", False)
            Else
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Salvar, "Grupos_Productos", True)
            End If
        Else
            MyMaster.MostrarMensaje(Resources.Orbecatalog_Resources.CamposRequeridos, False)
        End If
        upd_Contenido.Update()
        upd_Busqueda.Update()
    End Sub

    Protected Sub btn_Modificar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Modificar.Click

        If IsValid() Then
            Dim id_perteneciente As Integer
            If lbl_GrupoPerteneciente.ToolTip.Length > 0 Then
                id_perteneciente = lbl_GrupoPerteneciente.ToolTip
            End If
            If updateGrupo(id_perteneciente) Then
                'ClearInfo(Configuration.EstadoPantalla.NORMAL)
                trv_Grupos_Productos.Nodes.Clear()
                selectGrupos_Productos(trv_Grupos_Productos)
                trv_Perteneciente.Nodes.Clear()
                selectGrupos_Productos(trv_Perteneciente)
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Modificar, "Grupo", False)
            Else
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Modificar, "Grupos_Productos", True)
            End If
        Else
            MyMaster.MostrarMensaje(Resources.Orbecatalog_Resources.CamposRequeridos, False)
        End If
        upd_Busqueda.Update()
    End Sub

    Protected Sub btn_Cancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Cancelar.Click
        ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
        upd_Contenido.Update()
    End Sub

    Protected Sub btn_Eliminar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Eliminar.Click
        If deleteGrupo() Then
            ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
            trv_Grupos_Productos.Nodes.Clear()
            selectGrupos_Productos(trv_Grupos_Productos)
            trv_Perteneciente.Nodes.Clear()
            selectGrupos_Productos(trv_Perteneciente)
        End If
        upd_Contenido.Update()
        upd_Busqueda.Update()
    End Sub

    Protected Sub trv_Grupos_Productos_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles trv_Grupos_Productos.SelectedNodeChanged
        Dim id_NodoActual As Integer = trv_Grupos_Productos.SelectedNode.Value
        ClearInfo(Configuration.EstadoPantalla.CONSULTAR, True)
        loadGrupo(id_NodoActual)
        upd_Contenido.Update()
    End Sub

    Protected Sub trv_Perteneciente_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles trv_Perteneciente.SelectedNodeChanged
        lbl_GrupoPerteneciente.Text = trv_Perteneciente.SelectedNode.Text
        lbl_GrupoPerteneciente.ToolTip = trv_Perteneciente.SelectedNode.Value
        'trv_Perteneciente.Visible = False
        upd_Contenido.Update()
    End Sub

End Class
