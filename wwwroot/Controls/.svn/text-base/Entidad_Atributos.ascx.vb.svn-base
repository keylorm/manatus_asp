Imports Orbelink.Entity.Entidades

Partial Class Controls_Entidad_Atributos
    Inherits System.Web.UI.UserControl

    Dim queryBuilder As Orbelink.DBHandler.QueryBuilder
    Dim connection As Orbelink.DBHandler.SQLServer
    Dim securityHandler As Orbelink.Control.Security.SecurityHandler
    Dim codigo_pantalla As String
    Dim MyMaster As Orbelink.Orbecatalog6.MasterPageBaseClass

    Public Sub ClearInfo(ByVal estado As Configuration.EstadoPantalla, ByVal clearInfo As Boolean)
        If clearInfo Then
            For counter As Integer = 0 To dg_Atributos.Items.Count - 1
                Dim dg_valores As DataGrid = dg_Atributos.Items(counter).FindControl("dg_Valores")
                For counter2 As Integer = 0 To dg_valores.Items.Count - 1
                    Dim tbx_Atributos_Valor As TextBox = dg_valores.Items(counter2).FindControl("tbx_Atributos_Valor")
                    Dim chk_Atributo_Visible As CheckBox = dg_valores.Items(counter2).FindControl("chk_Atributo_Visible")

                    chk_Atributo_Visible.Checked = False
                    tbx_Atributos_Valor.Text = ""
                Next
            Next
        End If
    End Sub

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

    'Atributos_Entidad
    Public Sub selectAtributos_Para_Entidades(ByVal id_TipoEntidad As Integer)
        Dim dataSet As Data.DataSet
        Dim Atributos_Para_Entidades As New Atributos_Para_Entidades
        Dim Atributos_TipoEntidad As New Atributos_TipoEntidad

        Atributos_Para_Entidades.Fields.SelectAll()
        queryBuilder.Join.EqualCondition(Atributos_Para_Entidades.Id_atributo, Atributos_TipoEntidad.id_atributo)

        Atributos_TipoEntidad.id_tipoEntidad.Where.EqualCondition(id_TipoEntidad)

        queryBuilder.Orderby.Add(Atributos_TipoEntidad.orden)
        queryBuilder.From.Add(Atributos_Para_Entidades)
        queryBuilder.From.Add(Atributos_TipoEntidad)
        dataSet = connection.executeSelect(queryBuilder.RelationalSelectQuery())

        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables(0).Rows.Count > 0 Then
                dg_Atributos.DataSource = dataSet
                dg_Atributos.DataKeyField = Atributos_Para_Entidades.Id_atributo.Name
                dg_Atributos.DataBind()
                dg_Atributos.Visible = True

                'Llena el DataGrid
                Dim counter As Integer
                Dim result_Atributos_Para_Entidades As ArrayList = Orbelink.DBHandler.ObjectBuilder.TransformDataTable(dataSet.Tables(0), Atributos_Para_Entidades)
                For counter = 0 To dataSet.Tables(0).Rows.Count - 1
                    Dim act_Atributos_Para_Entidades As Atributos_Para_Entidades = result_Atributos_Para_Entidades(counter)

                    Dim dg_Valores As DataGrid = dg_Atributos.Items(counter).FindControl("dg_Valores")
                    Dim lbl_Atributo As Label = dg_Atributos.Items(counter).FindControl("lbl_Atributo")
                    Dim img_Info As UI.WebControls.Image = dg_Atributos.Items(counter).FindControl("img_Info")
                    Dim lnk_OtroAtributo As HyperLink = dg_Atributos.Items(counter).FindControl("lnk_OtroAtributo")

                    lbl_Atributo.Text = act_Atributos_Para_Entidades.Nombre.Value
                    lbl_Atributo.Attributes.Add(act_Atributos_Para_Entidades.Id_atributo.Name, act_Atributos_Para_Entidades.Id_atributo.Value)

                    If act_Atributos_Para_Entidades.Descripcion.IsValid Then
                        img_Info.AlternateText = act_Atributos_Para_Entidades.Descripcion.Value
                        img_Info.ToolTip = act_Atributos_Para_Entidades.Descripcion.Value
                        img_Info.Visible = True
                    Else
                        img_Info.Visible = False
                    End If

                    Dim autoCompletar As Boolean = False
                    If act_Atributos_Para_Entidades.AutoCompletar.Value = 1 Then
                        lbl_Atributo.Attributes.Add("autoCompletar", "true")
                        autoCompletar = True
                    Else
                        lbl_Atributo.Attributes.Add("autoCompletar", "False")
                    End If

                    crearCamposValores(dg_Valores, lnk_OtroAtributo, act_Atributos_Para_Entidades.Id_atributo.Value, id_TipoEntidad, 0, autoCompletar)
                    Me.upd_Atributos.Update()
                Next
            Else
                dg_Atributos.Visible = False
                Me.upd_Atributos.Update()
            End If
        Else
            dg_Atributos.Visible = False
            Me.upd_Atributos.Update()
        End If
    End Sub

    Protected Sub crearCamposValores(ByRef theDataGrid As DataGrid, ByRef theAddLink As HyperLink, ByVal id_Atributo As Integer, ByVal id_TipoEntidad As Integer, ByVal cantidadRows As Integer, ByVal autoCompletar As Boolean)
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
                llenarDDL(ddl_Atributos_Valores, id_TipoEntidad, id_Atributo)
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

    Protected Sub llenarDDL(ByVal theDDL As DropDownList, ByVal id_TipoEntidad As Integer, ByVal id_Atributo As Integer)
        Dim Atributos_Entidad As New Atributos_Entidad
        Dim entidad As New Entidad
        Dim dataSet As Data.DataSet

        Atributos_Entidad.Valor.ToSelect = True
        Atributos_Entidad.Id_atributo.Where.EqualCondition(id_Atributo)
        queryBuilder.Join.EqualCondition(Atributos_Entidad.Id_entidad, entidad.Id_entidad)
        entidad.Id_tipoEntidad.Where.EqualCondition(id_TipoEntidad)

        queryBuilder.From.Add(Atributos_Entidad)
        queryBuilder.From.Add(entidad)
        queryBuilder.Distinct = True
        dataSet = connection.executeSelect(queryBuilder.RelationalSelectQuery())

        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables(0).Rows.Count > 0 Then
                theDDL.DataSource = dataSet
                theDDL.DataTextField = Atributos_Entidad.Valor.Name
                theDDL.DataValueField = Atributos_Entidad.Valor.Name
                theDDL.DataBind()
            End If
            theDDL.Items.Add(" - ")
            theDDL.SelectedIndex = theDDL.Items.Count - 1
        End If
    End Sub

    Public Sub loadAtributos_Entidad(ByVal id_Entidad As Integer, ByVal id_TipoEntidad As Integer)
        Dim Atributos_Entidad As New Atributos_Entidad
        Dim dataset As Data.DataSet
        Dim counter As Integer

        Atributos_Entidad.Id_atributo.ToSelect = True
        Atributos_Entidad.Id_entidad.Where.EqualCondition(id_Entidad)
        dataset = connection.executeSelect(queryBuilder.SelectQuery(Atributos_Entidad))

        If dataset.Tables.Count > 0 Then
            If dataset.Tables(0).Rows.Count > 0 Then
                Dim results_Atributos_Entidad As ArrayList = Orbelink.DBHandler.ObjectBuilder.TransformDataTable(dataset.Tables(0), Atributos_Entidad)
                For counter = 0 To dg_Atributos.Items.Count - 1
                    Dim lbl_Atributo As Label = dg_Atributos.Items(counter).FindControl("lbl_Atributo")
                    Dim dg_Valores As DataGrid = dg_Atributos.Items(counter).FindControl("dg_Valores")
                    Dim lnk_OtroAtributo As HyperLink = dg_Atributos.Items(counter).FindControl("lnk_OtroAtributo")

                    For counterB As Integer = 0 To results_Atributos_Entidad.Count - 1
                        Dim act_Atributos_Entidad As Atributos_Entidad = results_Atributos_Entidad(counterB)

                        'Es el atributo igual
                        If lbl_Atributo.Attributes("id_Atributo") = act_Atributos_Entidad.Id_atributo.Value Then
                            Dim autoCompletar As Boolean = lbl_Atributo.Attributes("autoCompletar")
                            loadValoresAtributos(dg_Valores, lnk_OtroAtributo, id_Entidad, id_TipoEntidad, act_Atributos_Entidad.Id_atributo.Value, autoCompletar)
                        End If
                    Next
                Next
                Me.upd_Atributos.Update()
            End If
        End If
    End Sub

    Public Sub loadValoresAtributos(ByVal theDataGrid As DataGrid, ByRef theAddLink As HyperLink, ByVal id_Entidad As Integer, ByVal id_TipoEntidad As Integer, ByVal id_Atributo As Integer, ByVal autoCompletar As Boolean)
        Dim Atributos_Entidad As New Atributos_Entidad
        Dim dataset As Data.DataSet
        Dim lastOrdinal As Integer = 0

        Atributos_Entidad.Id_atributo.Where.EqualCondition(id_Atributo)
        Atributos_Entidad.Id_entidad.Where.EqualCondition(id_Entidad)
        Atributos_Entidad.Ordinal.ToSelect = True
        Atributos_Entidad.Valor.ToSelect = True
        Atributos_Entidad.visible.ToSelect = True
        queryBuilder.Orderby.Add(Atributos_Entidad.Ordinal)
        dataset = connection.executeSelect(queryBuilder.SelectQuery(Atributos_Entidad))

        If dataset.Tables.Count > 0 Then
            If dataset.Tables(0).Rows.Count > 0 Then
                crearCamposValores(theDataGrid, theAddLink, id_Atributo, id_TipoEntidad, dataset.Tables(0).Rows.Count, autoCompletar)

                Dim results_Atributos_Entidad As ArrayList = Orbelink.DBHandler.ObjectBuilder.TransformDataTable(dataset.Tables(0), Atributos_Entidad)
                For counter As Integer = 0 To theDataGrid.Items.Count - 1
                    Dim ddl_Atributos_Valores As DropDownList = theDataGrid.Items(counter).FindControl("ddl_Atributos_Valores")
                    Dim tbx_Atributos_Valor As TextBox = theDataGrid.Items(counter).FindControl("tbx_Atributos_Valor")
                    Dim chk_Atributo_Visible As CheckBox = theDataGrid.Items(counter).FindControl("chk_Atributo_Visible")

                    If counter <= results_Atributos_Entidad.Count - 1 Then
                        Dim act_Atributos_Entidad As Atributos_Entidad = results_Atributos_Entidad(counter)

                        If autoCompletar Then
                            ddl_Atributos_Valores.SelectedValue = act_Atributos_Entidad.Valor.Value
                        End If
                        tbx_Atributos_Valor.Text = act_Atributos_Entidad.Valor.Value

                        If act_Atributos_Entidad.visible.Value = "1" Then
                            chk_Atributo_Visible.Checked = True
                        End If
                        lastOrdinal = act_Atributos_Entidad.Ordinal.Value
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

    Public Function executeAtributos_Entidad(ByVal id_Entidad As Integer) As Boolean
        Dim resultado As Boolean = True
        For counter As Integer = 0 To dg_Atributos.Items.Count - 1
            Dim lbl_Atributo As Label = dg_Atributos.Items(counter).FindControl("lbl_Atributo")
            Dim dg_Valores As DataGrid = dg_Atributos.Items(counter).FindControl("dg_Valores")
            Dim lnk_OtroAtributo As HyperLink = dg_Atributos.Items(counter).FindControl("lnk_OtroAtributo")

            If Not executeValoresAtributos(dg_Valores, id_Entidad, lbl_Atributo.Attributes("Id_atributo")) Then
                resultado = False
            End If
        Next
        Return resultado
    End Function

    Public Function executeValoresAtributos(ByRef theDataGrid As DataGrid, ByVal id_Entidad As Integer, ByVal id_Atributo As Integer) As Boolean
        Dim resultado As Boolean = True

        'Todos los valores para un atributo
        For counter As Integer = 0 To theDataGrid.Items.Count - 1
            Dim Atributos_Entidad As New Atributos_Entidad
            Dim posibleDelete As Boolean = False

            Dim ddl_Atributos_Valores As DropDownList = theDataGrid.Items(counter).FindControl("ddl_Atributos_Valores")
            Dim tbx_Atributos_Valor As TextBox = theDataGrid.Items(counter).FindControl("tbx_Atributos_Valor")
            Dim chk_Atributo_Visible As CheckBox = theDataGrid.Items(counter).FindControl("chk_Atributo_Visible")

            Try
                'Por si debe delete o update
                Atributos_Entidad.Id_entidad.Where.EqualCondition(id_Entidad)
                Atributos_Entidad.Id_atributo.Where.EqualCondition(id_Atributo)
                Atributos_Entidad.Ordinal.Where.EqualCondition(chk_Atributo_Visible.Attributes("ordinal"))

                'Por si debe insert
                Atributos_Entidad.Id_entidad.Value = id_Entidad
                Atributos_Entidad.Id_atributo.Value = id_Atributo
                Atributos_Entidad.Ordinal.Value = chk_Atributo_Visible.Attributes("ordinal")

                'Verifica de donde tomar informacion
                If tbx_Atributos_Valor.Visible Then
                    'Verifica si debe borrar
                    If tbx_Atributos_Valor.Text.Length > 0 Then
                        Atributos_Entidad.Valor.Value = tbx_Atributos_Valor.Text
                    Else
                        posibleDelete = True
                    End If
                Else
                    'Verifica si debe borrar
                    If ddl_Atributos_Valores.SelectedIndex < ddl_Atributos_Valores.Items.Count - 1 Then
                        Atributos_Entidad.Valor.Value = ddl_Atributos_Valores.SelectedValue
                    Else
                        posibleDelete = True
                    End If
                End If

                If chk_Atributo_Visible.Checked Then
                    Atributos_Entidad.visible.Value = "1"
                Else
                    Atributos_Entidad.visible.Value = "0"
                End If

                'Verifica que accion tomar...
                If chk_Atributo_Visible.Attributes("InDB") = "True" Then
                    If posibleDelete Then
                        connection.executeDelete(queryBuilder.DeleteQuery(Atributos_Entidad))
                    Else
                        connection.executeUpdate(queryBuilder.UpdateQuery(Atributos_Entidad))
                    End If
                Else
                    If Atributos_Entidad.Valor.IsValid Then
                        connection.executeInsert(queryBuilder.InsertQuery(Atributos_Entidad))
                        chk_Atributo_Visible.Attributes("InDB") = "True"
                    End If
                End If

            Catch exc As Exception
                resultado = False
            End Try
        Next
        Return resultado
    End Function

    Protected Function deleteAtributos_Entidad(ByVal id_Entidad As Integer) As Boolean
        Dim Atributos_Entidad As New Atributos_Entidad
        Try
            Atributos_Entidad.Id_entidad.Where.EqualCondition(id_Entidad)
            connection.executeDelete(queryBuilder.DeleteQuery(Atributos_Entidad))
            Return True
        Catch exc As Exception
            MyMaster.MostrarMensaje(exc.Message, True)

            Return False
        End Try
    End Function

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

    'Eventos
    Public Event RequestExternalPage(ByVal laRuta As String, ByVal codigo As String)

    Protected Sub btn_AgregarAtributo_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_OtrosAtributos.Click
        RaiseEvent RequestExternalPage("AtribTipoEntidad.aspx", "CO-04")
    End Sub
End Class
