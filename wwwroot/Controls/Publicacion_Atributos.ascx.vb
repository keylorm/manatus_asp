Imports Orbelink.DBHandler
Imports Orbelink.Orbecatalog6
Imports Orbelink.Entity.Publicaciones
Imports Orbelink.Control.Security

Partial Class Controls_Publicacion_Atributos
    Inherits System.Web.UI.UserControl

    Dim queryBuilder As QueryBuilder
    Dim connection As SQLServer
    Dim securityHandler As SecurityHandler
    Dim codigo_pantalla As String
    Dim MyMaster As MasterPageBaseClass

    Public Sub SetVariables(ByVal theQueryBuilder As QueryBuilder, _
                           ByVal theConnection As SQLServer, _
                           ByVal theSecurityHandler As SecurityHandler, _
                           ByVal theCodigo_pantalla As String, _
                           ByVal theMaster As MasterPageBaseClass)
        queryBuilder = theQueryBuilder
        connection = theConnection
        securityHandler = theSecurityHandler
        codigo_pantalla = theCodigo_pantalla
        MyMaster = theMaster
    End Sub

    Public Sub ClearInfo(ByVal estado As Configuration.EstadoPantalla, ByVal clearInfo As Boolean)
        For counter As Integer = 0 To dg_Atributos.Items.Count - 1
            Dim dg_valores As DataGrid = dg_Atributos.Items(counter).FindControl("dg_Valores")
            For counter2 As Integer = 0 To dg_valores.Items.Count - 1
                Dim tbx_Atributos_Valor As TextBox = dg_valores.Items(counter).FindControl("tbx_Atributos_Valor")
                Dim chk_Atributo_Visible As CheckBox = dg_valores.Items(counter).FindControl("chk_Atributo_Visible")

                chk_Atributo_Visible.Checked = False
                tbx_Atributos_Valor.Text = ""
            Next
        Next
    End Sub

    'Atributos_Publicacion
    Public Sub selectAtributos_Para_Publicaciones(ByVal id_TipoPublicacion As Integer)
        Dim dataSet As Data.DataSet
        Dim Atributos_Para_Publicaciones As New Atributos_Para_Publicaciones
        Dim Atributos_TipoPublicacion As New Atributos_TipoPublicacion

        Atributos_Para_Publicaciones.Fields.SelectAll()
        queryBuilder.Join.EqualCondition(Atributos_Para_Publicaciones.Id_atributo, Atributos_TipoPublicacion.id_atributo)
        Atributos_TipoPublicacion.id_tipoPublicacion.Where.EqualCondition(id_TipoPublicacion)

        queryBuilder.Orderby.Add(Atributos_TipoPublicacion.orden)
        queryBuilder.From.Add(Atributos_Para_Publicaciones)
        queryBuilder.From.Add(Atributos_TipoPublicacion)
        dataSet = connection.executeSelect(queryBuilder.RelationalSelectQuery())

        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables(0).Rows.Count > 0 Then
                dg_Atributos.DataSource = dataSet
                dg_Atributos.DataKeyField = Atributos_Para_Publicaciones.Id_atributo.Name
                dg_Atributos.DataBind()
                dg_Atributos.Visible = True

                'Llena el DataGrid
                Dim counter As Integer
                Dim result_Atributos_Para_Publicaciones As ArrayList = ObjectBuilder.TransformDataTable(dataSet.Tables(0), Atributos_Para_Publicaciones)
                For counter = 0 To dataSet.Tables(0).Rows.Count - 1
                    Dim act_Atributos_Para_Publicaciones As Atributos_Para_Publicaciones = result_Atributos_Para_Publicaciones(counter)

                    Dim dg_Valores As DataGrid = dg_Atributos.Items(counter).FindControl("dg_Valores")
                    Dim lbl_Atributo As Label = dg_Atributos.Items(counter).FindControl("lbl_Atributo")
                    Dim img_Info As UI.WebControls.Image = dg_Atributos.Items(counter).FindControl("img_Info")
                    Dim lnk_OtroAtributo As HyperLink = dg_Atributos.Items(counter).FindControl("lnk_OtroAtributo")

                    lbl_Atributo.Text = act_Atributos_Para_Publicaciones.Nombre.Value
                    lbl_Atributo.Attributes.Add(act_Atributos_Para_Publicaciones.Id_atributo.Name, act_Atributos_Para_Publicaciones.Id_atributo.Value)

                    If act_Atributos_Para_Publicaciones.Descripcion.IsValid Then
                        img_Info.AlternateText = act_Atributos_Para_Publicaciones.Descripcion.Value
                        img_Info.ToolTip = act_Atributos_Para_Publicaciones.Descripcion.Value
                        img_Info.Visible = True
                    Else
                        img_Info.Visible = False
                    End If

                    Dim autoCompletar As Boolean = False
                    If act_Atributos_Para_Publicaciones.AutoCompletar.Value = 1 Then
                        lbl_Atributo.Attributes.Add("autoCompletar", "true")
                        autoCompletar = True
                    Else
                        lbl_Atributo.Attributes.Add("autoCompletar", "False")
                    End If

                    crearCamposValores(dg_Valores, lnk_OtroAtributo, act_Atributos_Para_Publicaciones.Id_atributo.Value, id_TipoPublicacion, 0, autoCompletar)
                Next
                dg_Atributos.Visible = True
            Else
                dg_Atributos.Visible = False
            End If
        Else
            dg_Atributos.Visible = False
        End If
        upd_Atributos.Update()
    End Sub

    Protected Sub crearCamposValores(ByRef theDataGrid As DataGrid, ByRef theAddLink As HyperLink, ByVal id_Atributo As Integer, ByVal id_TipoPublicacion As Integer, ByVal cantidadRows As Integer, ByVal autoCompletar As Boolean)
        Dim dataset As New Data.DataSet
        Dim lastOrdinal As Integer = 0
        dataset.Tables.Add()

        If cantidadRows = 0 Then
            cantidadRows = 1
        End If

        For counter As Integer = 0 To cantidadRows
            Dim tempRow As Data.DataRow = dataset.Tables(0).NewRow
            dataset.Tables(0).Rows.Add(tempRow)
        Next

        theDataGrid.DataSource = dataset
        theDataGrid.DataBind()

        For counter As Integer = 0 To theDataGrid.Items.Count - 1
            Dim ddl_Atributos_Valores As DropDownList = theDataGrid.Items(counter).FindControl("ddl_Atributos_Valores")
            Dim tbx_Atributos_Valor As TextBox = theDataGrid.Items(counter).FindControl("tbx_Atributos_Valor")
            Dim chk_Atributo_Visible As CheckBox = theDataGrid.Items(counter).FindControl("chk_Atributo_Visible")

            tbx_Atributos_Valor.Attributes.Add("maxlength", 100)
            tbx_Atributos_Valor.Attributes.Add("onkeypress", "showTextProgress(this)")
            chk_Atributo_Visible.ToolTip = "Visible"

            If autoCompletar Then
                tbx_Atributos_Valor.Visible = False
                ddl_Atributos_Valores.Visible = True
                llenarDDL(ddl_Atributos_Valores, id_TipoPublicacion, id_Atributo)
            Else
                tbx_Atributos_Valor.Visible = True
                ddl_Atributos_Valores.Visible = False
            End If

            If counter = theDataGrid.Items.Count - 1 Then
                tbx_Atributos_Valor.Visible = True
                ddl_Atributos_Valores.Visible = False
                theDataGrid.Items(counter).Style("display") = "none"
                theAddLink.NavigateUrl = "javascript:toggleLayer('" & theDataGrid.Items(counter).ClientID & "', this)"
            End If

            chk_Atributo_Visible.Attributes("InDB") = "False"
            chk_Atributo_Visible.Attributes.Add("ordinal", counter)
        Next
    End Sub

    Protected Sub llenarDDL(ByVal theDDL As DropDownList, ByVal id_TipoPublicacion As Integer, ByVal id_Atributo As Integer)
        Dim Atributos_Publicacion As New Atributos_Publicacion
        Dim Publicacion As New Publicacion
        Dim dataSet As Data.DataSet

        Atributos_Publicacion.Valor.ToSelect = True
        Atributos_Publicacion.Id_atributo.Where.EqualCondition(id_Atributo)
        queryBuilder.Join.EqualCondition(Atributos_Publicacion.Id_publicacion, Publicacion.Id_Publicacion)
        Publicacion.Id_tipoPublicacion.Where.EqualCondition(id_TipoPublicacion)

        queryBuilder.From.Add(Atributos_Publicacion)
        queryBuilder.From.Add(Publicacion)
        queryBuilder.Distinct = True
        dataSet = connection.executeSelect(queryBuilder.RelationalSelectQuery())

        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables(0).Rows.Count > 0 Then
                theDDL.DataSource = dataSet
                theDDL.DataTextField = Atributos_Publicacion.Valor.Name
                theDDL.DataValueField = Atributos_Publicacion.Valor.Name
                theDDL.DataBind()
            End If
            theDDL.Items.Add(" - ")
            theDDL.SelectedIndex = theDDL.Items.Count - 1
        End If
    End Sub

    Public Sub loadAtributos_Publicacion(ByVal id_Publicacion As Integer, ByVal id_TipoPublicacion As Integer)
        Dim Atributos_Publicacion As New Atributos_Publicacion
        Dim dataset As Data.DataSet
        Dim counter As Integer

        Atributos_Publicacion.Id_atributo.ToSelect = True
        Atributos_Publicacion.Id_publicacion.Where.EqualCondition(id_Publicacion)
        dataset = connection.executeSelect(queryBuilder.SelectQuery(Atributos_Publicacion))

        If dataset.Tables.Count > 0 Then
            If dataset.Tables(0).Rows.Count > 0 Then
                Dim results_Atributos_Publicacion As ArrayList = ObjectBuilder.TransformDataTable(dataset.Tables(0), Atributos_Publicacion)
                For counter = 0 To dg_Atributos.Items.Count - 1
                    Dim lbl_Atributo As Label = dg_Atributos.Items(counter).FindControl("lbl_Atributo")
                    Dim dg_Valores As DataGrid = dg_Atributos.Items(counter).FindControl("dg_Valores")
                    Dim lnk_OtroAtributo As HyperLink = dg_Atributos.Items(counter).FindControl("lnk_OtroAtributo")

                    For counterB As Integer = 0 To results_Atributos_Publicacion.Count - 1
                        Dim act_Atributos_Publicacion As Atributos_Publicacion = results_Atributos_Publicacion(counterB)

                        'Es el atributo igual
                        If lbl_Atributo.Attributes("id_Atributo") = act_Atributos_Publicacion.Id_atributo.Value Then

                            Dim autoCompletar As Boolean = lbl_Atributo.Attributes("autoCompletar")
                            loadValoresAtributos(dg_Valores, lnk_OtroAtributo, id_Publicacion, id_TipoPublicacion, act_Atributos_Publicacion.Id_atributo.Value, autoCompletar)
                        End If
                    Next
                Next
            End If
        End If
    End Sub

    Protected Sub loadValoresAtributos(ByVal theDataGrid As DataGrid, ByRef theAddLink As HyperLink, ByVal id_Publicacion As Integer, ByVal id_TipoPublicacion As Integer, ByVal id_Atributo As Integer, ByVal autoCompletar As Boolean)
        Dim Atributos_Publicacion As New Atributos_Publicacion
        Dim dataset As Data.DataSet
        Dim lastOrdinal As Integer = 0

        Atributos_Publicacion.Id_atributo.Where.EqualCondition(id_Atributo)
        Atributos_Publicacion.Id_publicacion.Where.EqualCondition(id_Publicacion)
        Atributos_Publicacion.Ordinal.ToSelect = True
        Atributos_Publicacion.Valor.ToSelect = True
        Atributos_Publicacion.visible.ToSelect = True
        queryBuilder.Orderby.Add(Atributos_Publicacion.Ordinal)
        dataset = connection.executeSelect(queryBuilder.SelectQuery(Atributos_Publicacion))

        If dataset.Tables.Count > 0 Then
            If dataset.Tables(0).Rows.Count > 0 Then
                crearCamposValores(theDataGrid, theAddLink, id_Atributo, id_TipoPublicacion, dataset.Tables(0).Rows.Count, autoCompletar)

                Dim results_Atributos_Publicacion As ArrayList = ObjectBuilder.TransformDataTable(dataset.Tables(0), Atributos_Publicacion)
                For counter As Integer = 0 To theDataGrid.Items.Count - 1
                    Dim ddl_Atributos_Valores As DropDownList = theDataGrid.Items(counter).FindControl("ddl_Atributos_Valores")
                    Dim tbx_Atributos_Valor As TextBox = theDataGrid.Items(counter).FindControl("tbx_Atributos_Valor")
                    Dim chk_Atributo_Visible As CheckBox = theDataGrid.Items(counter).FindControl("chk_Atributo_Visible")

                    If counter <= results_Atributos_Publicacion.Count - 1 Then
                        Dim act_Atributos_Publicacion As Atributos_Publicacion = results_Atributos_Publicacion(counter)

                        If autoCompletar Then
                            ddl_Atributos_Valores.SelectedValue = act_Atributos_Publicacion.Valor.Value
                        End If
                        tbx_Atributos_Valor.Text = act_Atributos_Publicacion.Valor.Value

                        If act_Atributos_Publicacion.visible.Value = "1" Then
                            chk_Atributo_Visible.Checked = True
                        End If
                        lastOrdinal = act_Atributos_Publicacion.Ordinal.Value
                        chk_Atributo_Visible.Attributes("InDB") = "True"
                    Else
                        chk_Atributo_Visible.Attributes("InDB") = "False"
                        lastOrdinal += 1
                    End If
                    chk_Atributo_Visible.Attributes.Add("ordinal", lastOrdinal)
                Next
            End If
        End If
    End Sub

    Protected Function executeAtributos_Publicacion(ByVal id_Publicacion As Integer) As Boolean
        Dim resultado As Boolean = True
        For counter As Integer = 0 To dg_Atributos.Items.Count - 1
            Dim lbl_Atributo As Label = dg_Atributos.Items(counter).FindControl("lbl_Atributo")
            Dim dg_Valores As DataGrid = dg_Atributos.Items(counter).FindControl("dg_Valores")
            Dim lnk_OtroAtributo As HyperLink = dg_Atributos.Items(counter).FindControl("lnk_OtroAtributo")

            If Not executeValoresAtributos(dg_Valores, id_Publicacion, lbl_Atributo.Attributes("Id_atributo")) Then
                resultado = False
            End If
        Next
        Return resultado
    End Function

    Protected Function executeValoresAtributos(ByRef theDataGrid As DataGrid, ByVal id_Publicacion As Integer, ByVal id_Atributo As Integer) As Boolean
        Dim resultado As Boolean = True

        'Todos los valores para un atributo
        For counter As Integer = 0 To theDataGrid.Items.Count - 1
            Dim Atributos_Publicacion As New Atributos_Publicacion
            Dim posibleDelete As Boolean = False

            Dim ddl_Atributos_Valores As DropDownList = theDataGrid.Items(counter).FindControl("ddl_Atributos_Valores")
            Dim tbx_Atributos_Valor As TextBox = theDataGrid.Items(counter).FindControl("tbx_Atributos_Valor")
            Dim chk_Atributo_Visible As CheckBox = theDataGrid.Items(counter).FindControl("chk_Atributo_Visible")

            Try
                'Por si debe delete o update
                Atributos_Publicacion.Id_publicacion.Where.EqualCondition(id_Publicacion)
                Atributos_Publicacion.Id_atributo.Where.EqualCondition(id_Atributo)
                Atributos_Publicacion.Ordinal.Where.EqualCondition(chk_Atributo_Visible.Attributes("ordinal"))

                'Por si debe insert
                Atributos_Publicacion.Id_publicacion.Value = id_Publicacion
                Atributos_Publicacion.Id_atributo.Value = id_Atributo
                Atributos_Publicacion.Ordinal.Value = chk_Atributo_Visible.Attributes("ordinal")

                'Verifica de donde tomar informacion
                If tbx_Atributos_Valor.Visible Then
                    'Verifica si debe borrar
                    If tbx_Atributos_Valor.Text.Length > 0 Then
                        Atributos_Publicacion.Valor.Value = tbx_Atributos_Valor.Text
                    Else
                        posibleDelete = True
                    End If
                Else
                    'Verifica si debe borrar
                    If ddl_Atributos_Valores.SelectedIndex < ddl_Atributos_Valores.Items.Count - 1 Then
                        Atributos_Publicacion.Valor.Value = ddl_Atributos_Valores.SelectedValue
                    Else
                        posibleDelete = True
                    End If
                End If

                If chk_Atributo_Visible.Checked Then
                    Atributos_Publicacion.visible.Value = "1"
                Else
                    Atributos_Publicacion.visible.Value = "0"
                End If

                'Verifica que accion tomar...
                If chk_Atributo_Visible.Attributes("InDB") = "True" Then
                    If posibleDelete Then
                        connection.executeDelete(queryBuilder.DeleteQuery(Atributos_Publicacion))
                    Else
                        connection.executeUpdate(queryBuilder.UpdateQuery(Atributos_Publicacion))
                    End If
                Else
                    If Atributos_Publicacion.Valor.IsValid Then
                        connection.executeInsert(queryBuilder.InsertQuery(Atributos_Publicacion))
                        chk_Atributo_Visible.Attributes("InDB") = "True"
                    End If
                End If

            Catch exc As Exception
                resultado = False
            End Try
        Next
        Return resultado
    End Function

    Protected Function deleteAtributos_Publicacion(ByVal id_Publicacion As Integer) As Boolean
        Dim Atributos_Publicacion As New Atributos_Publicacion
        Try
            Atributos_Publicacion.Id_publicacion.Where.EqualCondition(id_Publicacion)
            connection.executeDelete(queryBuilder.DeleteQuery(Atributos_Publicacion))
            Return True
        Catch exc As Exception
            MyMaster.MostrarMensaje(exc.Message, True)
            Return False
        End Try
    End Function


    Public Sub verificarAtributos(ByVal id_publicacion As Integer, ByVal id_tipoPublicacion As Integer)
        If Not executeAtributos_Publicacion(id_publicacion) Then
            MyMaster.concatenarMensaje("Pero error al actualizar atributos.", False)
        Else
            loadAtributos_Publicacion(id_publicacion, id_tipoPublicacion)
        End If
    End Sub


    Protected Sub dg_Atributos_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dg_Atributos.EditCommand
        Dim idAtributo As Integer = dg_Atributos.DataKeys(e.Item.ItemIndex)
        Dim ddl_Atributos_Valores As DropDownList = dg_Atributos.Items(e.Item.ItemIndex).FindControl("ddl_Atributos_Valores")
        Dim tbx_Atributos_Valor As TextBox = dg_Atributos.Items(e.Item.ItemIndex).FindControl("tbx_Atributos_Valor")
        If ddl_Atributos_Valores.Visible Then
            tbx_Atributos_Valor.Visible = True
            ddl_Atributos_Valores.Visible = False
        Else
            tbx_Atributos_Valor.Visible = False
            ddl_Atributos_Valores.Visible = True
        End If
        upd_Atributos.Update()
    End Sub
End Class
