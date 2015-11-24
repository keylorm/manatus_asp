Imports Orbelink.Entity.Entidades
Imports Orbelink.Entity.Productos
Imports Orbelink.Entity.Reservaciones
Imports Orbelink.DBHandler
Imports Orbelink.Control.Reservaciones
Imports Orbelink.DateAndTime.DateHandler
Imports Orbelink.DateAndTime

Partial Class reservacion_en_paso2
    Inherits Orbelink.FrontEnd6.PageBaseClass
    Private Const id_tipoEntidadUSUARIO As Integer = 2 'Usuario

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            selectPaises()
        End If
    End Sub
    'Protected Function InsertarModificar(ByVal id_tipoEntidad As Integer) As Boolean
    '    Dim resul As Boolean = True
    '    Dim entidad As New Entidad
    '    Dim telefonoConArea As String = "(" & txt_codArea.Text & ")" & txt_tel.Text

    '    If Not existEmail(txt_email.Text) And Not existUserName(txt_email.Text, 0) Then
    '        If insertEntidad(id_tipoEntidad, ddl_ubicacion.SelectedValue, txt_nombre.Text, "", txt_codPostal.Text, telefonoConArea, txt_email.Text, txt_direccion.Text) Then
    '            Dim id_insertado As Integer = connection.lastKey(entidad.TableName, entidad.Id_entidad.Name)
    '            txt_nombre.ToolTip = id_insertado
    '            txt_email.ToolTip = txt_email.Text
    '        Else
    '            resul = False
    '            If Orbelink.DBHandler.LanguageHandler.CurrentLanguage = Orbelink.DBHandler.LanguageHandler.Language.INGLES Then
    '                lbl_ResultadoReservacion.Text = "Sorry, an error occurred when registering the reservation. Please try again."
    '            Else
    '                lbl_ResultadoReservacion.Text = "Ocurrió un error al ingresar los datos"
    '            End If

    '        End If
    '    Else
    '        Dim id_entidad As Integer = txt_nombre.ToolTip

    '        If Not updateEntidad(id_entidad, ddl_ubicacion.SelectedValue, txt_nombre.Text, "", txt_codPostal.Text, telefonoConArea, txt_email.Text, txt_direccion.Text) Then
    '            resul = False
    '            If Orbelink.DBHandler.LanguageHandler.CurrentLanguage = Orbelink.DBHandler.LanguageHandler.Language.INGLES Then
    '                lbl_ResultadoReservacion.Text = "Sorry, an error occurred when updating your information. Please try again."
    '            Else
    '                lbl_ResultadoReservacion.Text = "Ocurrió un error al modificar los datos"
    '            End If
    '        End If
    '    End If
    '    Return resul
    'End Function

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
        MsgBox("test")
    End Sub
End Class
