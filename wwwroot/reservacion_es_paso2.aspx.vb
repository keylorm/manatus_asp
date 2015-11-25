Imports Orbelink.Entity.Entidades
Imports Orbelink.Entity.Productos
Imports Orbelink.Entity.Reservaciones
Imports Orbelink.DBHandler
Imports Orbelink.Control.Reservaciones
Imports Orbelink.DateAndTime.DateHandler
Imports Orbelink.DateAndTime

Partial Class reservacion_es_paso2
    Inherits Orbelink.FrontEnd6.PageBaseClass
    Private Const id_tipoEntidadUSUARIO As Integer = 2 'Usuario

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            selectPaises()
        End If
    End Sub
    Protected Function insertEntidad(ByVal id_tipoEntidad As Integer, ByVal id_ubicacion As Integer, ByVal nombre As String, ByVal apellido As String, ByVal codigoPostal As String, ByVal tel As String, ByVal email As String, ByVal direccion As String) As Boolean
        Dim entidad As Entidad
        Try
            entidad = New Entidad()
            entidad.Id_tipoEntidad.Value = id_tipoEntidad
            entidad.NombreEntidad.Value = nombre
            entidad.NombreDisplay.Value = nombre & " " & apellido
            entidad.Apellido.Value = apellido
            entidad.Apellido2.Value = codigoPostal ' Codigo postal en apellido 2
            entidad.Telefono.Value = tel
            entidad.Id_Ubicacion.Value = id_ubicacion
            entidad.UserName.Value = email
            entidad.Email.Value = email
            entidad.Password.Value = "12345"
            entidad.Descripcion.Value = direccion 'Direccion en descripcion
            entidad.Fecha.ValueLocalized = DateHandler.ToLocalizedDateFromUtc(Date.UtcNow)
            Dim a As String = queryBuilder.InsertQuery(entidad)
            connection.executeInsert(a)
            Return True
        Catch exc As Exception
            'lbl_Errores.Text = exc.Message
            'lbl_Errores.Visible = True
            Return False
        End Try
    End Function
    Protected Function updateEntidad(ByVal id_entidad As Integer, ByVal id_ubicacion As Integer, ByVal nombre As String, ByVal apellido As String, ByVal codigoPostal As String, ByVal tel As String, ByVal email As String, ByVal direccion As String) As Boolean
        Dim result As Boolean = True
        Dim entidad As New Entidad
        Try
            entidad.Fields.UpdateAll()
            entidad.Id_tipoEntidad.ToUpdate = False
            entidad.Id_Ubicacion.Value = id_ubicacion
            entidad.NombreEntidad.Value = nombre
            entidad.NombreDisplay.Value = nombre & " " & apellido
            entidad.Apellido.Value = apellido
            entidad.Apellido2.Value = codigoPostal ' Codigo postal en apellido 2
            If txt_email.ToolTip = email Then
                entidad.UserName.ToUpdate = False
            Else
                entidad.UserName.Value = email
            End If
            If txt_email.ToolTip = email Then
                entidad.Email.ToUpdate = False
            Else
                entidad.Email.Value = email
            End If

            entidad.Password.ToUpdate = False
            entidad.Telefono.Value = tel
            entidad.Descripcion.Value = direccion 'Direccion en descripcion
            entidad.Id_entidad.Where.EqualCondition(id_entidad)
            Dim res As String = queryBuilder.UpdateQuery(entidad)
            If connection.executeUpdate(res) >= 1 Then
                result = True
            Else
                result = False
            End If

            Return result
        Catch exc As Exception
            Return False
        End Try
    End Function
    Protected Function existEmail(ByVal email As String) As Boolean
        Dim exist As Boolean = False
        Dim dataSet As Data.DataSet
        Dim entidad As New Entidad

        entidad.Id_entidad.ToSelect = True
        If email.Length > 0 Then
            entidad.Email.Where.EqualCondition(email)
            queryBuilder.From.Add(entidad)
            dataSet = connection.executeSelect(queryBuilder.RelationalSelectQuery)

            If dataSet.Tables.Count > 0 Then
                If dataSet.Tables(0).Rows.Count > 0 Then
                    ObjectBuilder.CreateObject(dataSet.Tables(0), 0, entidad)
                    txt_nombre.ToolTip = entidad.Id_entidad.Value
                    exist = True

                End If
            End If
        End If

        Return exist
    End Function
    Protected Function existUserName(ByVal username As String, ByVal id_entidad As String) As Boolean
        Dim exist As Boolean = False
        Dim dataSet As Data.DataSet
        Dim entidad As New Entidad

        entidad.Id_entidad.ToSelect = True
        If username.Length > 0 Then
            entidad.UserName.Where.EqualCondition(username)
            queryBuilder.From.Add(entidad)
            dataSet = connection.executeSelect(queryBuilder.RelationalSelectQuery)

            If dataSet.Tables.Count > 0 Then
                If dataSet.Tables(0).Rows.Count > 0 Then
                    ObjectBuilder.CreateObject(dataSet.Tables(0), 0, entidad)
                    If id_entidad.Length > 0 Then   'Si esta update
                        If entidad.Id_entidad.Value <> id_entidad Then
                            exist = True
                        End If
                    Else                            'Esta insert
                        exist = True
                    End If
                End If
            End If
        End If

        Return exist
    End Function
    Protected Function InsertarModificar(ByVal id_tipoEntidad As Integer) As Boolean
        Dim resul As Boolean = True
        Dim entidad As New Entidad
        Dim telefonoConArea As String = "(" & txt_codArea.Text & ")" & txt_tel.Text

        If Not existEmail(txt_email.Text) And Not existUserName(txt_email.Text, 0) Then
            If insertEntidad(id_tipoEntidad, ddl_ubicacion.SelectedValue, txt_nombre.Text, "", txt_codPostal.Text, telefonoConArea, txt_email.Text, txt_direccion.Text) Then
                Dim id_insertado As Integer = connection.lastKey(entidad.TableName, entidad.Id_entidad.Name)
                txt_nombre.ToolTip = id_insertado
                txt_email.ToolTip = txt_email.Text
            Else
                resul = False
                If Orbelink.DBHandler.LanguageHandler.CurrentLanguage = Orbelink.DBHandler.LanguageHandler.Language.INGLES Then
                    lbl_ResultadoReservacion.Text = "Sorry, an error occurred when registering the reservation. Please try again."
                Else
                    lbl_ResultadoReservacion.Text = "Ocurrió un error al ingresar los datos"
                End If

            End If
        Else
            Dim id_entidad As Integer = txt_nombre.ToolTip

            If Not updateEntidad(id_entidad, ddl_ubicacion.SelectedValue, txt_nombre.Text, "", txt_codPostal.Text, telefonoConArea, txt_email.Text, txt_direccion.Text) Then
                resul = False
                If Orbelink.DBHandler.LanguageHandler.CurrentLanguage = Orbelink.DBHandler.LanguageHandler.Language.INGLES Then
                    lbl_ResultadoReservacion.Text = "Sorry, an error occurred when updating your information. Please try again."
                Else
                    lbl_ResultadoReservacion.Text = "Ocurrió un error al modificar los datos"
                End If
            End If
        End If
        Return resul
    End Function
    'Devuelve listado de los paises
    Protected Sub selectPaises()
        Dim dataset As Data.DataSet
        Dim Ubicacion As New Ubicacion
        Dim res As String
        Ubicacion.Id_ubicacion.ToSelect = True
        Ubicacion.Nombre.ToSelect = True

        queryBuilder.Orderby.Add(Ubicacion.Nombre)
        queryBuilder.From.Add(Ubicacion)
        res = queryBuilder.RelationalSelectQuery()

        dataset = connection.executeSelect(res)
        If dataset.Tables.Count > 0 Then
            If dataset.Tables(0).Rows.Count > 0 Then
                ddl_ubicacion.DataSource = dataset
                ddl_ubicacion.DataValueField = Ubicacion.Id_ubicacion.Name
                ddl_ubicacion.DataTextField = Ubicacion.Nombre.Name
                ddl_ubicacion.DataBind()
            End If
        End If
        ddl_ubicacion.SelectedValue = 1
    End Sub
    Protected Sub btn_aPaso3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_aPaso3.Click
        'MsgBox("test")
        Dim id_entidad As Integer = 0
        If InsertarModificar(id_tipoEntidadUSUARIO) Then
            If txt_nombre.ToolTip <> "" Then
                id_entidad = txt_nombre.ToolTip
            End If
        End If
    End Sub
End Class
