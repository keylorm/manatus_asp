Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler
Imports Orbelink.Entity.Entidades
Imports Orbelink.Control.Security

Namespace Orbelink.Control.Entidades

    Public Class EntidadHandler

        Dim _connString As String

        Shared DisplayNameFormatString As String = "{0} {1} {2}"

        Sub New(ByRef theConnectionString As String)
            _connString = theConnectionString
        End Sub

        Public Structure Resultado
            Dim code As ResultCodes
            Dim id_Entidad As Integer
        End Structure

        Public Enum ResultCodes As Integer
            OK
            ErrorGeneral
            ErrorInsertar
            FaltaCampos
            CamposNoValidos
        End Enum

        'Entidades
        Function CrearEntidad(ByVal Id_tipoEntidad As Integer, ByVal NombreEntidad As String, ByVal apellido As String, ByVal apellido2 As String, _
            ByVal telefono As String, ByVal celular As String, ByVal email As String, ByVal usuario As String, ByVal password As String, ByVal id_ubicacion As Integer, ByVal Descripcion As String) As Resultado
            Dim entidad As New Entidad
            entidad.Id_tipoEntidad.Value = Id_tipoEntidad
            entidad.NombreEntidad.Value = NombreEntidad
            entidad.Apellido.Value = apellido
            entidad.Apellido2.Value = apellido2
            entidad.Telefono.Value = telefono
            entidad.Celular.Value = celular
            entidad.Id_Ubicacion.Value = id_ubicacion
            entidad.Descripcion.Value = Descripcion
            entidad.Email.Value = email
            entidad.UserName.Value = usuario
            entidad.Password.Value = password

            Return CrearEntidad(entidad)
        End Function

        Function CrearEntidad_Minimo(ByVal Id_tipoEntidad As Integer, ByVal NombreEntidad As String, ByVal id_ubicacion As Integer) As Resultado
            Dim entidad As New Entidad
            entidad.Id_tipoEntidad.Value = Id_tipoEntidad
            entidad.NombreEntidad.Value = NombreEntidad
            entidad.Id_Ubicacion.Value = id_ubicacion
            Return CrearEntidad(entidad)
        End Function

        Public Function CrearEntidad(ByVal entidad As Entidad, Optional ByVal forzarEmail As Boolean = True) As Resultado
            Dim resultado As New Resultado
            resultado.code = ResultCodes.ErrorGeneral
            resultado.id_Entidad = 0

            Try
                entidad.Fecha.ValueUtc = Date.UtcNow

                RevisarNombreDisplay(entidad)
                RevisarEmail(entidad, forzarEmail)
                RevisarUsername(entidad, 0)

                Dim elUsuario As String = ""
                If entidad.UserName.IsValid Then
                    elUsuario = entidad.UserName.Value
                End If
                Dim elPassword As String = ""
                If entidad.Password.IsValid Then
                    elPassword = entidad.Password.Value
                End If

                resultado.code = insertEntidad(entidad)
                If resultado.code = ResultCodes.OK Then
                    Dim connection As New SQLServer(_connString)
                    resultado.id_Entidad = connection.lastKey(entidad.TableName, entidad.Id_entidad.Name)

                    Dim securityHandler As New SecurityHandler(Configuration.Config_DefaultConnectionString)
                    securityHandler.CrearUsuario(resultado.id_Entidad, elUsuario, elPassword)
                End If
            Catch ex As Exception
                resultado.code = ResultCodes.ErrorGeneral
            End Try
            Return resultado
        End Function

        Public Function ActualizarEntidad(ByRef entidad As Entidad, ByVal id_entidad As Integer) As Boolean
            RevisarNombreDisplay(entidad)
            RevisarEmail(entidad, False)
            RevisarUsername(entidad, id_entidad)
            If Not entidad.UserName.IsNull Then
                entidad.Password.Value = SecurityHandler.HashPassword(entidad.Password.Value)
            End If
            Return updateEntidad(entidad, id_entidad)
        End Function

        Public Function EliminarEntidad(ByVal id_entidad As Integer) As Boolean
            Return deleteEntidad(id_entidad)
        End Function

        Public Function ExisteEntidad(ByVal id_Entidad As Integer) As Boolean
            Dim entidad As New Entidad
            entidad.Id_entidad.ToSelect = True
            entidad.Id_entidad.Where.EqualCondition(id_Entidad)
            Dim connection As New SQLServer(_connString)
            Dim query As New QueryBuilder
            Dim resultTable As Data.DataTable = connection.executeSelect_DT(query.SelectQuery(entidad))
            If resultTable.Rows.Count > 0 Then
                Return True
            End If
            Return False
        End Function

        Public Function ConsultarEntidad(ByVal id_Entidad As Integer) As Entidad
            Dim theEntidad As New Entidad
            theEntidad.Fields.SelectAll()
            theEntidad.Id_entidad.Where.EqualCondition(id_Entidad)
            Dim connection As New SQLServer(_connString)
            Dim query As New QueryBuilder
            Dim resultTable As Data.DataTable = connection.executeSelect_DT(query.SelectQuery(theEntidad))
            If resultTable.Rows.Count > 0 Then
                Return ObjectBuilder.CreateDBTable_BySelectIndex(resultTable.Rows(0), theEntidad)
            End If
            Return Nothing
        End Function

        Public Function ConsultarNombreEntidad(ByVal theId_Entidad As Integer) As String
            Dim connection As New SQLServer(_connString)
            Dim query As New QueryBuilder
            Dim entidad As New Entidad
            entidad.Id_entidad.Where.EqualCondition(theId_Entidad)
            entidad.NombreDisplay.ToSelect = True
            query.From.Add(entidad)
            Dim resultTable As Data.DataTable = connection.executeSelect_DT(query.RelationalSelectQuery)
            If resultTable.Rows.Count > 0 Then
                entidad = ObjectBuilder.CreateDBTable_BySelectIndex(resultTable.Rows(0), entidad)
            End If
            Return entidad.NombreDisplay.Value
        End Function

        'entidad logica negocio
        Private Sub RevisarNombreDisplay(ByRef entidad As Entidad)
            If entidad.Apellido.IsValid Then
                If entidad.Apellido2.IsValid Then
                    entidad.NombreDisplay.Value = String.Format(DisplayNameFormatString, entidad.NombreEntidad.Value, entidad.Apellido.Value, entidad.Apellido2.Value)
                Else
                    entidad.NombreDisplay.Value = String.Format(DisplayNameFormatString, entidad.NombreEntidad.Value, entidad.Apellido.Value, "")
                End If
            Else
                entidad.NombreDisplay.Value = String.Format(DisplayNameFormatString, entidad.NombreEntidad.Value, "", "")
            End If
            entidad.NombreDisplay.Value = entidad.NombreDisplay.Value.Trim
        End Sub

        Private Sub RevisarEmail(ByRef entidad As Entidad, ByVal forzarEmail As Boolean)
            Dim cambiarEmail As Boolean = False
            If entidad.Email.IsValid Then
                If existEmail(entidad.Email.Value, 0) Then
                    cambiarEmail = True
                End If
            Else
                cambiarEmail = True
            End If
            If cambiarEmail And forzarEmail Then
                entidad.Email.Value = entidad.NombreDisplay.Value.Replace(" ", ".") & "@" & Configuration.Config_ShortSiteName
            End If
            entidad.Email.Value = entidad.Email.Value.Trim
        End Sub

        Private Sub RevisarUsername(ByRef entidad As Entidad, ByVal id_entidad As Integer)
            If entidad.UserName.IsValid Then
                If Not existUserName(entidad.UserName.Value, id_entidad) Then
                    If entidad.Password.IsNull Then
                        entidad.UserName.SetToNull()
                    End If
                End If
            End If
        End Sub

        'Entidades select
        Public Function selectEntidades_DataGrid(ByRef theDataGrid As DataGrid, ByVal level As Integer) As Boolean
            Dim connection As New SQLServer(_connString)
            Dim query As New QueryBuilder

            Dim dataSet As Data.DataSet
            Dim entidad As New Entidad
            Dim tipoEntidad As New TipoEntidad
            Dim counter As Integer
            Dim prefijoNivel As String = ""
            Dim devolver As Boolean

            entidad.Id_entidad.ToSelect = True
            entidad.NombreDisplay.ToSelect = True
            entidad.Email.ToSelect = True

            tipoEntidad.Nombre.ToSelect = True
            query.Join.EqualCondition(tipoEntidad.Id_TipoEntidad, entidad.Id_tipoEntidad)

            query.From.Add(entidad)
            query.From.Add(tipoEntidad)
            query.Top = 5
            query.Orderby.Add(entidad.Id_entidad, False)
            dataSet = connection.executeSelect(query.RelationalSelectQuery)
            If dataSet.Tables.Count > 0 Then
                If dataSet.Tables(0).Rows.Count > 0 Then
                    theDataGrid.DataSource = dataSet
                    theDataGrid.DataKeyField = entidad.Id_entidad.Name
                    theDataGrid.DataBind()

                    'Llena el grid
                    Dim resul_Entidades As ArrayList = ObjectBuilder.TransformDataTable(dataSet.Tables(0), entidad)
                    Dim resul_TipoEntidad As ArrayList = ObjectBuilder.TransformDataTable(dataSet.Tables(0), tipoEntidad)
                    For counter = 0 To theDataGrid.Items.Count - 1
                        Dim act_Entidad As Entidad = resul_Entidades(counter)
                        Dim act_TipoEntidad As TipoEntidad = resul_TipoEntidad(counter)

                        Dim lnk_Email As HyperLink = theDataGrid.Items(counter).FindControl("lnk_Email")
                        Dim lbl_TipoEntidad As Label = theDataGrid.Items(counter).FindControl("lbl_TipoEntidad")
                        Dim lnk_Entidad As HyperLink = theDataGrid.Items(counter).FindControl("lnk_Entidad")

                        lnk_Entidad.Text = act_Entidad.NombreDisplay.Value
                        lnk_Email.Text = act_Entidad.Email.Value
                        lnk_Email.NavigateUrl = "mailto:" & act_Entidad.Email.Value
                        lbl_TipoEntidad.Text = act_TipoEntidad.Nombre.Value

                        lnk_Entidad.NavigateUrl = "Entidad.aspx?id_Entidad=" & act_Entidad.Id_entidad.Value
                    Next

                    theDataGrid.Visible = True
                    devolver = True
                Else
                    theDataGrid.Visible = False
                    devolver = False
                End If
            Else
                theDataGrid.Visible = False
                devolver = False
            End If
            Return devolver
        End Function

        Public Sub selectEntidades_DDL(ByRef theDropDownList As DropDownList, ByVal id_entidad As Integer, ByVal nivel As Integer, Optional ByVal addTodos As Boolean = False)
            Dim connection As New SQLServer(_connString)
            Dim query As New QueryBuilder

            Dim dataSet As Data.DataSet
            Dim entidad As New Entidad
            Dim tipoEntidad As New TipoEntidad

            If nivel = 0 Then
                tipoEntidad.Nivel.Where.EqualCondition(0)
            Else
                tipoEntidad.Nivel.Where.DiferentCondition(0)

                If id_entidad <> 0 Then
                    tipoEntidad.Nivel.Where.LessThanCondition(nivel)
                    entidad.Id_entidad.Where.EqualCondition(id_entidad, Orbelink.DBHandler.Where.FieldRelations.OR_)
                    query.GroupFieldsConditions(tipoEntidad.Nivel, entidad.Id_entidad)
                End If
            End If

            entidad.Id_entidad.ToSelect = True
            entidad.NombreDisplay.ToSelect = True
            query.Join.EqualCondition(entidad.Id_tipoEntidad, tipoEntidad.Id_TipoEntidad)

            query.From.Add(entidad)
            query.From.Add(tipoEntidad)
            query.Orderby.Add(entidad.NombreDisplay)
            query.Orderby.Add(entidad.Apellido)
            dataSet = connection.executeSelect(query.RelationalSelectQuery)

            theDropDownList.Items.Clear()
            If dataSet.Tables.Count > 0 Then
                If dataSet.Tables(0).Rows.Count > 0 Then
                    theDropDownList.Visible = True
                    Dim results_Entidades As ArrayList = ObjectBuilder.TransformDataTable(dataSet.Tables(0), entidad)
                    For counter As Integer = 0 To results_Entidades.Count - 1
                        Dim act_Entidad As Entidad = results_Entidades(counter)
                        Dim apellido As Boolean = False

                        theDropDownList.Items.Add(act_Entidad.NombreDisplay.Value)
                        theDropDownList.Items(counter).Value = act_Entidad.Id_entidad.Value

                        If id_entidad = act_Entidad.Id_entidad.Value Then
                            theDropDownList.SelectedIndex = counter
                        End If
                    Next
                End If
            End If

            If addTodos Then
                theDropDownList.Items.Add("-- Todos --")
                theDropDownList.SelectedIndex = theDropDownList.Items.Count - 1
            End If
        End Sub

        Public Sub selectEntidades_DDL(ByRef theDropDownList As DropDownList, ByVal id_TipoEntidad As Integer)
            Dim connection As New SQLServer(_connString)
            Dim query As New QueryBuilder
            Dim dataSet As Data.DataSet
            Dim entidad As New Entidad

            If id_TipoEntidad <> 0 Then
                entidad.Id_tipoEntidad.Where.EqualCondition(id_TipoEntidad)
            End If

            entidad.Id_entidad.ToSelect = True
            entidad.NombreDisplay.ToSelect = True

            query.From.Add(entidad)
            query.Orderby.Add(entidad.NombreDisplay)
            dataSet = connection.executeSelect(query.RelationalSelectQuery)

            theDropDownList.Items.Clear()
            If dataSet.Tables.Count > 0 Then
                If dataSet.Tables(0).Rows.Count > 0 Then
                    theDropDownList.Visible = True
                    Dim results_Entidades As ArrayList = ObjectBuilder.TransformDataTable(dataSet.Tables(0), entidad)
                    For counter As Integer = 0 To results_Entidades.Count - 1
                        Dim act_Entidad As Entidad = results_Entidades(counter)

                        theDropDownList.Items.Add(act_Entidad.NombreDisplay.Value)
                        theDropDownList.Items(counter).Value = act_Entidad.Id_entidad.Value
                    Next
                End If
            End If
        End Sub

        Public Function GetSelectedIDEntidad(ByRef theDDL As DropDownList, ByRef theLBL As Label) As String
            Dim selectedID As String = ""

            If theLBL.Visible Then
                selectedID = theLBL.ToolTip
            Else
                selectedID = theDDL.SelectedValue
            End If

            Return selectedID
        End Function

        Public Sub SetSelectedIdEntidad(ByVal id_entidad As Integer, ByRef theDDL As DropDownList, ByRef theLBL As Label)
            Dim connection As New SQLServer(_connString)
            Dim query As New QueryBuilder
            Dim dataSet As Data.DataSet
            Dim entidad As New Entidad

            If id_entidad = 0 Then
                theDDL.Visible = True
                theLBL.Visible = False
            Else
                theDDL.Visible = False
                theLBL.Visible = True

                entidad.Id_entidad.ToSelect = True
                entidad.NombreDisplay.ToSelect = True
                entidad.Id_entidad.Where.EqualCondition(id_entidad)
                dataSet = connection.executeSelect(query.SelectQuery(entidad))

                If dataSet.Tables.Count > 0 Then
                    ObjectBuilder.CreateObject(dataSet.Tables(0), 0, entidad)
                    theLBL.Text = entidad.NombreDisplay.Value
                    theLBL.ToolTip = id_entidad
                End If
            End If
        End Sub

        'Entidad privadas
        Private Function insertEntidad(ByVal entidad As Entidad) As ResultCodes
            Try
                Dim connection As New SQLServer(_connString)
                Dim query As New QueryBuilder
                If connection.executeInsert(query.InsertQuery(entidad)) = 1 Then
                    Return ResultCodes.OK
                End If
            Catch exc As DBException
                Select Case exc.MyDBExceptionType
                    Case DBException.DBExceptionTypes.FaltanCampos
                        Return ResultCodes.FaltaCampos
                    Case DBException.DBExceptionTypes.Reference_Exception
                        Return ResultCodes.CamposNoValidos
                    Case Else
                        Return ResultCodes.ErrorInsertar
                End Select
            End Try
        End Function

        Private Function updateEntidad(ByVal entidad As Entidad, ByVal id_entidad As Integer) As Boolean
            Try
                Dim connection As New SQLServer(_connString)
                Dim query As New QueryBuilder
                entidad.Id_entidad.Where.EqualCondition(id_entidad)
                If connection.executeUpdate(query.UpdateQuery(entidad)) = 1 Then
                    Return True
                End If
            Catch exc As DBException
            End Try
            Return False
        End Function

        Protected Function deleteEntidad(ByVal id_entidad As Integer) As Boolean
            Try
                Dim connection As New SQLServer(_connString)
                Dim query As New QueryBuilder
                Dim entidad As New Entidad
                entidad.Id_entidad.Where.EqualCondition(id_entidad)
                If connection.executeDelete(query.DeleteQuery(entidad)) = 1 Then
                    Return True
                End If
            Catch exc As DBException
            End Try
            Return False
        End Function

        'Entidad busquedas
        Public Function buscarEntidades(ByRef prefix As String, ByVal count As Integer, Optional ByVal contextKey As String = Nothing) As String()
            Dim connection As New SQLServer(_connString)
            Dim query As New QueryBuilder
            Dim entidad As New Entidad
            Dim tipoEntidad As New TipoEntidad
            Dim id_entidad As Integer = 0
            Dim nivel As Integer = -1

            Try
                id_entidad = contextKey
                nivel = ConsultarNivelPorEntidad(id_entidad)
            Catch ex As Exception

            End Try

            entidad.NombreDisplay.Where.LikeCondition(prefix)
            entidad.Id_entidad.ToSelect = True
            entidad.NombreDisplay.ToSelect = True
            query.Join.EqualCondition(entidad.Id_tipoEntidad, tipoEntidad.Id_TipoEntidad)

            If nivel = 0 Then
                tipoEntidad.Nivel.Where.EqualCondition(0)
            Else
                tipoEntidad.Nivel.Where.DiferentCondition(0)

                If id_entidad <> 0 Then
                    tipoEntidad.Nivel.Where.LessThanCondition(nivel)
                    entidad.Id_entidad.Where.EqualCondition(id_entidad, Orbelink.DBHandler.Where.FieldRelations.OR_)
                    query.GroupFieldsConditions(tipoEntidad.Nivel, entidad.Id_entidad)
                End If
            End If

            query.From.Add(entidad)
            query.From.Add(tipoEntidad)
            query.Orderby.Add(entidad.NombreDisplay)
            query.Top = count
            Dim resultTable As Data.DataTable = connection.executeSelect_DT(query.RelationalSelectQuery)

            Dim resultados As New System.Collections.Generic.List(Of String)
            If resultTable.Rows.Count > 0 Then
                For counter As Integer = 0 To resultTable.Rows.Count - 1
                    Dim act_entidad As Entidad = ObjectBuilder.CreateDBTable_BySelectIndex(resultTable.Rows(counter), entidad)
                    'resultados.Add(entidad.NombreDisplay.Value)
                    resultados.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(act_entidad.NombreDisplay.Value, act_entidad.Id_entidad.Value))
                Next
            End If
            Return resultados.ToArray
        End Function

        Public Function buscarEntidad_ByNombre(ByVal theName As String) As Entidad
            Dim connection As New SQLServer(_connString)
            Dim query As New QueryBuilder
            Dim entidad As New Entidad
            entidad.NombreDisplay.Where.EqualCondition(theName)
            entidad.Fields.SelectAll()
            query.From.Add(entidad)

            Dim resultTable As Data.DataTable = connection.executeSelect_DT(query.RelationalSelectQuery)
            If resultTable.Rows.Count > 0 Then
                entidad = ObjectBuilder.CreateDBTable_BySelectIndex(resultTable.Rows(0), entidad)
            End If
            Return entidad
        End Function

        'Nivel
        Public Function ConsultarNivelPorEntidad(ByVal id_entidad As Integer) As Integer
            Dim connection As New SQLServer(_connString)
            Dim query As New QueryBuilder
            Dim entidad As New Entidad
            Dim tipoEntidad As New TipoEntidad

            tipoEntidad.Nivel.ToSelect = True

            entidad.Id_entidad.Where.EqualCondition(id_entidad)

            query.Join.EqualCondition(entidad.Id_tipoEntidad, tipoEntidad.Id_TipoEntidad)

            query.From.Add(entidad)
            query.From.Add(tipoEntidad)

            Dim dataSet As Data.DataSet = connection.executeSelect(query.RelationalSelectQuery)
            If dataSet.Tables.Count > 0 Then
                If dataSet.Tables(0).Rows.Count > 0 Then
                    tipoEntidad = ObjectBuilder.CreateDBTable_BySelectIndex(dataSet.Tables(0).Rows(0), tipoEntidad)
                    Return tipoEntidad.Nivel.Value
                End If
            End If
            Return -1
        End Function

        'Verificadores
        Public Function existUserName(ByVal username As String, ByVal id_entidad As Integer) As Boolean
            Dim connection As New SQLServer(_connString)
            Dim query As New QueryBuilder
            Dim exist As Boolean = False
            Dim dataSet As Data.DataSet
            Dim entidad As New Entidad

            entidad.Id_entidad.ToSelect = True
            'If username.Length > 0 Then
            entidad.UserName.Where.EqualCondition(username)
            query.From.Add(entidad)
            dataSet = connection.executeSelect(query.RelationalSelectQuery)

            If dataSet.Tables.Count > 0 Then
                If dataSet.Tables(0).Rows.Count > 0 Then
                    ObjectBuilder.CreateObject(dataSet.Tables(0), 0, entidad)
                    If entidad.Id_entidad.Value <> id_entidad Then
                        exist = True
                    End If
                End If
            End If
            'Else
            'exist = True
            'End If

            Return exist
        End Function

        Public Function existEmail(ByVal email As String, ByVal id_entidad As Integer) As Boolean
            Dim connection As New SQLServer(_connString)
            Dim query As New QueryBuilder
            Dim exist As Boolean = False
            Dim dataSet As Data.DataSet
            Dim entidad As New Entidad

            entidad.Id_entidad.ToSelect = True
            If email.Length > 0 Then
                entidad.Email.Where.EqualCondition(email)
                query.From.Add(entidad)
                dataSet = connection.executeSelect(query.RelationalSelectQuery)

                If dataSet.Tables.Count > 0 Then
                    If dataSet.Tables(0).Rows.Count > 0 Then
                        ObjectBuilder.CreateObject(dataSet.Tables(0), 0, entidad)
                        If entidad.Id_entidad.Value <> id_entidad Then
                            exist = True
                        End If
                    End If
                End If
            Else
                exist = True
            End If

            Return exist
        End Function
    End Class

End Namespace
