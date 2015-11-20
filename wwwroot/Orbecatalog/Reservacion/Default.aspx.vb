Imports Orbelink.DBHandler
Imports Orbelink.Entity.Reservaciones
Imports Orbelink.Control.Reservaciones

Partial Class Orbecatalog_Reservacion_Default
    Inherits PageBaseClass

    Const codigo_pantalla As String = "Rs-01"
    Const level As Integer = 2

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddScripts(level)

        securityHandler.VerifyPantalla(codigo_pantalla, level)

        If Not IsPostBack Then
            Me.Title = CType(GetGlobalResourceObject("Orbecatalog_Resources", "Orbecatalog"), String) & " - " & Resources.Reservaciones_Resources.Modulo_Reservaciones

            securityHandler.VerifyAveliableScreens(trv_Reservaciones, level, "Rs", False)
            selectReservaciones()
        End If
    End Sub

    'Reservacions 
    Protected Sub selectReservaciones()
        Dim controladora As New ControladorReservaciones(connection, Resources.Reservaciones_Resources.ResourceManager)

        Dim Reservacion As New Reservacion
        Dim consultarText As String = Resources.Orbecatalog_Resources.Consultar
        Dim resultado As Data.DataTable = controladora.SelectReservaciones(10)

        If resultado IsNot Nothing Then
            gv_Reservacions.AutoGenerateColumns = False

            Dim linkField As New BoundField
            linkField.DataField = Reservacion.Id_Reservacion.Name
            linkField.DataFormatString = "<a href=""Reservacion.aspx?id_Reservacion={0}"" >" & consultarText & "</a>"
            linkField.HtmlEncodeFormatString = False
            linkField.HeaderText = consultarText
            gv_Reservacions.Columns.Add(linkField)

            For Each columna As Data.DataColumn In resultado.Columns
                If columna.ColumnMapping <> Data.MappingType.Hidden Then



                    Dim fieldTemp As New BoundField
                    fieldTemp.DataField = columna.ColumnName
                    fieldTemp.HeaderText = CType(GetGlobalResourceObject("Reservaciones_Resources", columna.ColumnName), String)
                    If columna.DataType.Equals(Date.UtcNow.GetType) Then
                        fieldTemp.DataFormatString = "{0:" & CurrentCulture.DateTimeFormat.ShortDatePattern & "}"
                    End If
                    gv_Reservacions.Columns.Add(fieldTemp)
                End If
            Next
            gv_Reservacions.DataSource = resultado
            gv_Reservacions.DataBind()
            gv_Reservacions.Visible = True

        Else
            gv_Reservacions.Visible = False
        End If
    End Sub

End Class
