Imports Orbelink.DBHandler
Imports Orbelink.Entity.Publicaciones

Partial Class Orbecatalog_Publicacion_Categorias
    Inherits PageBaseClass

    Const codigo_pantalla As String = "PU-04"
    Const level As Integer = 2

    'Pagina
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddScripts(level)
        securityHandler.VerifyPantalla(codigo_pantalla, level)

        If Not IsPostBack Then
            securityHandler.VerifyActions(btn_Salvar, btn_Modificar, btn_Eliminar)

            selectCategorias(trv_Categorias)
            selectCategorias(trv_Perteneciente)
            ClearInfo(Configuration.EstadoPantalla.NORMAL)
        End If
    End Sub

    Protected Sub ClearInfo(ByVal estado As Configuration.EstadoPantalla)
        vld_tbx_NombreCategorias.IsValid = True
        tbx_NombreCategorias.Text = ""
        tbx_NombreCategorias.ToolTip = ""
        Select Case estado
            Case Configuration.EstadoPantalla.NORMAL
                btn_Modificar.Visible = False

                btn_Salvar.Visible = True

                btn_Eliminar.Visible = False

                lbl_CategoriasPerteneciente.ToolTip = ""
                lbl_CategoriasPerteneciente.Text = "Sin Asignar"
            Case Configuration.EstadoPantalla.CONSULTAR
                btn_Modificar.Visible = True

                btn_Salvar.Visible = False

                btn_Eliminar.Visible = True

        End Select
    End Sub

    'Categorias
    Protected Sub selectCategorias(ByVal theTreeView As TreeView)
        Dim Categorias As New Categorias
        Dim dataset As Data.DataSet
        Dim counter As Integer

        Categorias.Fields.SelectAll()
        Categorias.Id_Categoria.Where.EqualCondition_OnSameTable(Categorias.Id_padre)
        dataset = connection.executeSelect(queryBuilder.SelectQuery(Categorias))
        If dataset.Tables.Count > 0 Then
            If dataset.Tables(0).Rows.Count > 0 Then
                Dim results_Categorias As ArrayList = ObjectBuilder.TransformDataTable(dataset.Tables(0), Categorias)
                For counter = 0 To dataset.Tables(0).Rows.Count - 1
                    Dim act_Categorias As Categorias = results_Categorias(counter)
                    Dim hijo As New TreeNode(act_Categorias.Nombre.Value, act_Categorias.Id_Categoria.Value)
                    theTreeView.Nodes.Add(hijo)
                    selectCategoriasHijos(theTreeView.Nodes(counter))
                Next
                theTreeView.CollapseAll()
            End If
        End If
    End Sub

    Protected Sub selectCategoriasHijos(ByVal elTreeNode As TreeNode)
        Dim padre_Id_Categoria As Integer = elTreeNode.Value
        Dim Categorias As New Categorias
        Dim dataset As Data.DataSet
        Dim counter As Integer

        Categorias.Fields.SelectAll()
        Categorias.Id_padre.Where.EqualCondition(padre_Id_Categoria)
        Categorias.Id_Categoria.Where.DiferentCondition(padre_Id_Categoria)
        dataset = connection.executeSelect(queryBuilder.SelectQuery(Categorias))
        If dataset.Tables.Count > 0 Then
            If dataset.Tables(0).Rows.Count > 0 Then
                Dim results_Categorias As ArrayList = ObjectBuilder.TransformDataTable(dataset.Tables(0), Categorias)
                For counter = 0 To dataset.Tables(0).Rows.Count - 1
                    Dim act_Categorias As Categorias = results_Categorias(counter)
                    Dim hijo As New TreeNode(act_Categorias.Nombre.Value, act_Categorias.Id_Categoria.Value)
                    selectCategoriasHijos(hijo)
                    elTreeNode.ChildNodes.Add(hijo)
                Next
            End If
        End If
    End Sub

    Protected Sub loadCategorias(ByVal Id_Categoria As Integer)
        Dim dataSet As Data.DataSet
        Dim Categorias As New Categorias
        Dim Categorias2 As New Categorias
        Dim id_padre As Integer

        Categorias.Fields.SelectAll()
        Categorias.Id_Categoria.Where.EqualCondition(Id_Categoria)
        dataSet = connection.executeSelect(queryBuilder.SelectQuery(Categorias))
        ObjectBuilder.CreateObject(dataSet.Tables(0), 0, Categorias)
        tbx_NombreCategorias.Text = Categorias.Nombre.Value
        tbx_NombreCategorias.ToolTip = Categorias.Id_Categoria.Value
        id_padre = Categorias.Id_padre.Value

        Categorias2.Nombre.ToSelect = True
        Categorias2.Id_Categoria.ToSelect = True
        Categorias2.Id_Categoria.Where.EqualCondition(id_padre)
        dataSet = connection.executeSelect(queryBuilder.SelectQuery(Categorias2))
        ObjectBuilder.CreateObject(dataSet.Tables(0), 0, Categorias2)
        lbl_CategoriasPerteneciente.Text = Categorias2.Nombre.Value
        lbl_CategoriasPerteneciente.ToolTip = Categorias2.Id_Categoria.Value
    End Sub

    Protected Function updateCategorias(ByVal Id_Categoria As Integer, ByVal id_father As Integer) As Boolean
        Dim Categorias As Categorias
        Try
            Categorias = New Categorias(id_father, tbx_NombreCategorias.Text)
            Categorias.Id_Categoria.Where.EqualCondition(tbx_NombreCategorias.ToolTip)
            connection.executeUpdate(queryBuilder.UpdateQuery(Categorias))
            Return True
        Catch exc As Exception
            MyMaster.mostrarMensaje(exc.Message, True)

            Return False
        End Try
    End Function

    Protected Function insertCategorias(ByVal id_father As Integer) As Boolean
        Dim Categorias As Categorias
        Try
            Categorias = New Categorias(id_father, tbx_NombreCategorias.Text)
            connection.executeInsert(queryBuilder.InsertQuery(Categorias))
            ClearInfo(Configuration.EstadoPantalla.NORMAL)
            If id_father = 0 Then
                Dim lastID As Integer = connection.lastKey(Categorias.TableName, Categorias.Id_Categoria.Name)
                Categorias.Id_Categoria.Where.EqualCondition(lastID)
                Categorias.Id_padre.Value = lastID
                connection.executeUpdate(queryBuilder.UpdateQuery(Categorias))
            End If
            Return True
        Catch exc As Exception
            MyMaster.mostrarMensaje(exc.Message, True)

            Return False
        End Try
    End Function

    Protected Function deleteCategorias(ByVal Id_Categoria As Integer) As Boolean
        Dim Categorias As New Categorias
        Try
            Categorias.Id_Categoria.Where.EqualCondition(Id_Categoria)
            connection.executeDelete(queryBuilder.DeleteQuery(Categorias))
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
            If lbl_CategoriasPerteneciente.ToolTip.Length > 0 Then
                id_father = lbl_CategoriasPerteneciente.ToolTip
            End If
            If insertCategorias(id_father) Then
                trv_Categorias.Nodes.Clear()
                selectCategorias(trv_Categorias)
                trv_Perteneciente.Nodes.Clear()
                selectCategorias(trv_Perteneciente)
                ClearInfo(Configuration.EstadoPantalla.NORMAL)
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Salvar, "Categoria", False)
            Else
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Salvar, "Categoria", True)
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
            If lbl_CategoriasPerteneciente.ToolTip.Length > 0 Then
                id_father = lbl_CategoriasPerteneciente.ToolTip
            End If
            If updateCategorias(tbx_NombreCategorias.ToolTip, id_father) Then
                trv_Categorias.Nodes.Clear()
                selectCategorias(trv_Categorias)
                trv_Perteneciente.Nodes.Clear()
                selectCategorias(trv_Perteneciente)
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Modificar, "Categoria", False)
            Else
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Modificar, "Categoria", True)
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

        If deleteCategorias(tbx_NombreCategorias.ToolTip) Then
            ClearInfo(Configuration.EstadoPantalla.NORMAL)
            trv_Categorias.Nodes.Clear()
            selectCategorias(trv_Categorias)
            trv_Perteneciente.Nodes.Clear()
            selectCategorias(trv_Perteneciente)
            MyMaster.mostrarMensaje("Categoria borrada exitosamente.", False)
        Else
            MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Eliminar, "Categoria", True)
        End If
        upd_Contenido.Update()
        upd_Busqueda.Update()
    End Sub

    Protected Sub trv_Categorias_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles trv_Categorias.SelectedNodeChanged
        Dim id_NodoActual As Integer = trv_Categorias.SelectedNode.Value
        ClearInfo(Configuration.EstadoPantalla.CONSULTAR)
        loadCategorias(id_NodoActual)
        upd_Contenido.Update()
    End Sub

    Protected Sub trv_Perteneciente_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles trv_Perteneciente.SelectedNodeChanged
        lbl_CategoriasPerteneciente.Text = trv_Perteneciente.SelectedNode.Text
        lbl_CategoriasPerteneciente.ToolTip = trv_Perteneciente.SelectedNode.Value
    End Sub
End Class
