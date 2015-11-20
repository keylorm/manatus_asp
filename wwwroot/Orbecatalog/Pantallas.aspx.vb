Imports Orbelink.DBHandler
Imports Orbelink.Entity.Security
Imports Orbelink.Entity.Orbecatalog

Partial Class _Pantallas
    Inherits PageBaseClass

    Const codigo_Pantalla As String = "OR-03"
    Const level As Integer = 1

    'Pagina
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddScripts(level)
        securityHandler.VerifyPantalla(codigo_Pantalla, level)
        If Not Page.IsPostBack Then
            securityHandler.VerifyActions(btn_Salvar, Nothing, Nothing)
            ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
            selectPerfil(securityHandler.ClearanceLevel)
            If ddl_Perfil.Items.Count > 0 Then
                selectPantallas()
                loadPantalla_Perfil(ddl_Perfil.SelectedValue)
            End If
        End If
    End Sub

    Protected Sub ClearInfo(ByVal estado As Configuration.EstadoPantalla, ByVal clearInfo As Boolean)
        If clearInfo Then

        End If
        Select Case estado
            Case Configuration.EstadoPantalla.NORMAL
                btn_Salvar.Visible = True

                'limpiarPantalla_Perfil(securityHandler.Perfil)
            Case Configuration.EstadoPantalla.CONSULTAR
                btn_Salvar.Visible = False
        End Select
    End Sub

    'Pantallas
    Sub selectPantallas()
        Dim dataSet As Data.DataSet
        Dim Pantallas As New Pantallas

        Pantallas.Fields.SelectAll()
        Pantallas.activo.Where.EqualCondition(1)
        'queryBuilder.OrderBy.Add(Pantallas.Orden)
        dataSet = connection.executeSelect(queryBuilder.SelectQuery(Pantallas))
        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables(0).Rows.Count > 0 Then
                dg_Pantallas.DataSource = dataSet
                dg_Pantallas.DataKeyField = Pantallas.Codigo_Pantalla.Name
                dg_Pantallas.DataBind()

                'Llena el DataGrid
                Dim counter As Integer
                Dim results As ArrayList = ObjectBuilder.TransformDataTable(dataSet.Tables(0), Pantallas)
                For counter = 0 To dataSet.Tables(0).Rows.Count - 1
                    Dim act_Pantalla As Pantallas = results(counter)

                    Dim lbl_Pantalla As Label = dg_Pantallas.Items(counter).FindControl("lbl_Pantalla")
                    Dim chk_Crear As CheckBox = dg_Pantallas.Items(counter).FindControl("chk_Crear")
                    Dim chk_Leer As CheckBox = dg_Pantallas.Items(counter).FindControl("chk_Leer")
                    Dim chk_Modificar As CheckBox = dg_Pantallas.Items(counter).FindControl("chk_Modificar")
                    Dim chk_Borrar As CheckBox = dg_Pantallas.Items(counter).FindControl("chk_Borrar")

                    If act_Pantalla.Codigo_Pantalla.Value <> act_Pantalla.codigo_Padre.Value Then
                        lbl_Pantalla.Text = "&nbsp;&nbsp;&nbsp;&nbsp; - " & act_Pantalla.ResourceKey.Value
                    Else
                        lbl_Pantalla.Text = "<h2>" & act_Pantalla.ResourceKey.Value & "</h2>"
                    End If
                    lbl_Pantalla.ToolTip = act_Pantalla.Codigo_Pantalla.Value
                    chk_Crear.Checked = False
                    chk_Leer.Checked = False
                    chk_Modificar.Checked = False
                    chk_Borrar.Checked = False

                    'Javascript
                    dg_Pantallas.Items(counter).Attributes.Add("onmouseover", "setRowFocus(this, " & counter & ", false)")
                    dg_Pantallas.Items(counter).Attributes.Add("onmouseout", "unsetRowFocus(this, " & counter & ", false)")
                Next
            Else
                dg_Pantallas.Visible = False
            End If
        End If
    End Sub

    'Perfil
    Protected Sub selectPerfil(ByVal nivel As Integer)
        Dim dataset As Data.DataSet
        Dim Perfil As New Perfil

        Perfil.Fields.SelectAll()
        Perfil.ClearanceLevel.Where.LessThanCondition(nivel)
        queryBuilder.Orderby.Add(Perfil.Nombre)
        dataset = connection.executeSelect(queryBuilder.SelectQuery(Perfil))
        If dataset.Tables.Count > 0 Then
            If dataset.Tables(0).Rows.Count > 0 Then
                ddl_Perfil.DataSource = dataset
                ddl_Perfil.DataTextField = Perfil.Nombre.Name
                ddl_Perfil.DataValueField = Perfil.Id_Perfil.Name
                ddl_Perfil.DataBind()
            End If
        End If
    End Sub

    Protected Sub loadPantalla_Perfil(ByVal id_Perfil As String)
        Dim dataSet As Data.DataSet
        Dim pantalla_Perfil As New Pantalla_Perfil
        Dim Perfil As New Perfil

        pantalla_Perfil.Id_Perfil.Where.EqualCondition(id_Perfil)
        pantalla_Perfil.Fields.SelectAll()
        dataSet = connection.executeSelect(queryBuilder.SelectQuery(pantalla_Perfil))

        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables(0).Rows.Count > 0 Then

                'Llena el DataGrid
                Dim counter As Integer
                Dim results As ArrayList = ObjectBuilder.TransformDataTable(dataSet.Tables(0), pantalla_Perfil)

                For counter = 0 To dg_Pantallas.Items.Count - 1
                    Dim codigo_Pantalla As String = CType(dg_Pantallas.Items(counter).FindControl("lbl_Pantalla"), Label).ToolTip
                    Dim counterB As Integer
                    For counterB = 0 To dataSet.Tables(0).Rows.Count - 1
                        Dim actual As Pantalla_Perfil = results(counterB)
                        If codigo_Pantalla = actual.codigo_Pantalla.Value Then
                            obtenerPantalla_Perfil(actual, counter)
                        End If
                    Next
                Next
            End If
        End If
    End Sub

    Protected Sub deletePantalla_Perfil(ByVal codigo_Pantalla As String, ByVal id_Perfil As Integer)
        Dim temp As New Pantalla_Perfil
        Dim sqlQuery As String
        Try
            temp.codigo_Pantalla.Where.EqualCondition(codigo_Pantalla)
            temp.Id_Perfil.Where.EqualCondition(id_Perfil)
            sqlQuery = queryBuilder.DeleteQuery(temp)
            connection.executeDelete(sqlQuery)
        Catch exc As Exception
            MyMaster.MostrarMensaje(exc.Message, True)
        End Try
    End Sub

    Protected Sub obtenerPantalla_Perfil(ByVal pantalla As Pantalla_Perfil, ByVal indiceDataGrid As Integer)
        Dim lbl_Pantalla As Label = dg_Pantallas.Items(indiceDataGrid).FindControl("lbl_Pantalla")
        Dim chk_Crear As CheckBox = dg_Pantallas.Items(indiceDataGrid).FindControl("chk_Crear")
        Dim chk_Leer As CheckBox = dg_Pantallas.Items(indiceDataGrid).FindControl("chk_Leer")
        Dim chk_Modificar As CheckBox = dg_Pantallas.Items(indiceDataGrid).FindControl("chk_Modificar")
        Dim chk_Borrar As CheckBox = dg_Pantallas.Items(indiceDataGrid).FindControl("chk_Borrar")

        'Dice que si habia registro
        chk_Crear.ToolTip = "Si"

        'Selecciona los que debe
        If pantalla.Crear.Value = "1" Then
            chk_Crear.Checked = True
        Else
            chk_Crear.Checked = False
        End If
        If pantalla.Leer.Value = "1" Then
            chk_Leer.Checked = True
        Else
            chk_Leer.Checked = False
        End If
        If pantalla.Modificar.Value = "1" Then
            chk_Modificar.Checked = True
        Else
            chk_Modificar.Checked = False
        End If
        If pantalla.Borrar.Value = "1" Then
            chk_Borrar.Checked = True
        Else
            chk_Borrar.Checked = False
        End If
    End Sub

    Protected Sub ejecutarPantalla_Perfil(ByVal id_Perfil As Integer, ByVal indice As Integer)
        Dim pantalla As New Pantalla_Perfil
        Dim alguno As Boolean
        Dim lbl_Pantalla As Label = dg_Pantallas.Items(indice).FindControl("lbl_Pantalla")
        Dim chk_Crear As CheckBox = dg_Pantallas.Items(indice).FindControl("chk_Crear")
        Dim chk_Leer As CheckBox = dg_Pantallas.Items(indice).FindControl("chk_Leer")
        Dim chk_Modificar As CheckBox = dg_Pantallas.Items(indice).FindControl("chk_Modificar")
        Dim chk_Borrar As CheckBox = dg_Pantallas.Items(indice).FindControl("chk_Borrar")

        'Busca cuales check estan
        If chk_Crear.Checked Then
            pantalla.Crear.Value = "1"
            pantalla.Leer.Value = "1"
            alguno = True
        Else
            pantalla.Crear.Value = "0"
        End If
        If chk_Leer.Checked Then
            pantalla.Leer.Value = "1"
            alguno = True
        Else
            pantalla.Leer.Value = "0"
        End If
        If chk_Modificar.Checked Then
            pantalla.Modificar.Value = "1"
            pantalla.Leer.Value = "1"
            alguno = True
        Else
            pantalla.Modificar.Value = "0"
        End If
        If chk_Borrar.Checked Then
            pantalla.Borrar.Value = "1"
            alguno = True
        Else
            pantalla.Borrar.Value = "0"
        End If

        'Analiza si borrar, insertar o actualizar
        If chk_Crear.ToolTip = "Si" Then
            pantalla.Id_Perfil.Where.EqualCondition(id_Perfil)
            pantalla.codigo_Pantalla.Where.EqualCondition(lbl_Pantalla.ToolTip)
            If alguno Then          'Update
                connection.executeUpdate(queryBuilder.UpdateQuery(pantalla))
            Else                    'Delete
                connection.executeDelete(queryBuilder.DeleteQuery(pantalla))
            End If
        Else
            If alguno Then          'Insert
                pantalla.Id_Perfil.Value = id_Perfil
                pantalla.codigo_Pantalla.Value = lbl_Pantalla.ToolTip
                connection.executeInsert(queryBuilder.InsertQuery(pantalla))
            End If
        End If
    End Sub

    Protected Sub limpiarPantalla_Perfil(ByVal toCheck As Boolean)
        Dim counter As Integer

        For counter = 0 To dg_Pantallas.Items.Count - 1
            Dim lbl_Pantalla As Label = dg_Pantallas.Items(counter).FindControl("lbl_Pantalla")
            Dim chk_Crear As CheckBox = dg_Pantallas.Items(counter).FindControl("chk_Crear")
            Dim chk_Leer As CheckBox = dg_Pantallas.Items(counter).FindControl("chk_Leer")
            Dim chk_Modificar As CheckBox = dg_Pantallas.Items(counter).FindControl("chk_Modificar")
            Dim chk_Borrar As CheckBox = dg_Pantallas.Items(counter).FindControl("chk_Borrar")

            chk_Crear.Checked = toCheck
            chk_Leer.Checked = toCheck
            chk_Modificar.Checked = toCheck
            chk_Borrar.Checked = toCheck

            chk_Crear.ToolTip = ""
        Next
    End Sub

    'Eventos
    Protected Sub btn_Salvar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Salvar.Click
        If IsValid() Then
            Dim counter As Integer
            For counter = 0 To dg_Pantallas.Items.Count - 1
                ejecutarPantalla_Perfil(ddl_Perfil.SelectedValue, counter)
            Next
            ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
        End If
    End Sub

    'Protected Sub btn_Modificar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Modificar.Click
    '    If IsValid() Then
    '        Dim counter As Integer
    '        For counter = 0 To dg_Pantallas.Items.Count - 1
    '            ejecutarPantalla_Perfil(ddl_Perfil.SelectedValue, counter)
    '        Next
    '        ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
    '    End If
    'End Sub

    Protected Sub btn_Cancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Cancelar.Click
        ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
        If ddl_Perfil.Items.Count > 0 Then
            loadPantalla_Perfil(ddl_Perfil.SelectedValue)
        End If
    End Sub

    'Protected Sub btn_Eliminar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Eliminar.Click
    '    Dim id_Perfil As Integer
    '    Dim counter As Integer
    '    For counter = 0 To dg_Pantallas.Items.Count - 1
    '        Dim lbl_Perfil As Label = dg_Pantallas.Items(counter).FindControl("lbl_Perfil")
    '        id_Perfil = lbl_Perfil.ToolTip
    '        deletePantalla_Perfil(codigo_Pantalla, id_Perfil)
    '    Next
    '    ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
    'End Sub

    Protected Sub btn_AgregarPerfil_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_AgregarPerfil.Click
        Response.Redirect("Contactos/Perfil.aspx" & Orbelink.Helpers.CommonTasks.RecreateQueryString(codigo_Pantalla, True))
    End Sub

    Protected Sub ddl_Perfil_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddl_Perfil.SelectedIndexChanged
        limpiarPantalla_Perfil(False)
        loadPantalla_Perfil(ddl_Perfil.SelectedValue)
    End Sub

    Protected Sub chk_TodosCrear_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim counter As Integer = 0
        Dim chk_Todos As CheckBox = sender
        For counter = 0 To dg_Pantallas.Items.Count - 1
            Dim chk_Crear As CheckBox = dg_Pantallas.Items(counter).FindControl("chk_Crear")
            If chk_Crear.Enabled Then
                chk_Crear.Checked = chk_Todos.Checked
            End If
        Next
    End Sub

    Protected Sub chk_TodosLeer_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim counter As Integer = 0
        Dim chk_Todos As CheckBox = sender
        For counter = 0 To dg_Pantallas.Items.Count - 1
            Dim chk_Leer As CheckBox = dg_Pantallas.Items(counter).FindControl("chk_Leer")
            If chk_Leer.Enabled Then
                chk_Leer.Checked = chk_Todos.Checked
            End If
        Next
    End Sub

    Protected Sub chk_TodosModificar_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim counter As Integer = 0
        Dim chk_Todos As CheckBox = sender
        For counter = 0 To dg_Pantallas.Items.Count - 1
            Dim chk_Modificar As CheckBox = dg_Pantallas.Items(counter).FindControl("chk_Modificar")
            If chk_Modificar.Enabled Then
                chk_Modificar.Checked = chk_Todos.Checked
            End If
        Next
    End Sub

    Protected Sub chk_TodosBorrar_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim counter As Integer = 0
        Dim chk_Todos As CheckBox = sender
        For counter = 0 To dg_Pantallas.Items.Count - 1
            Dim chk_Borrar As CheckBox = dg_Pantallas.Items(counter).FindControl("chk_Borrar")
            If chk_Borrar.Enabled Then
                chk_Borrar.Checked = chk_Todos.Checked
            End If
        Next
    End Sub
End Class

