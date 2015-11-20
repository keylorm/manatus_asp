Imports Orbelink.DBHandler
Imports Subgurim.Controles
Imports Orbelink.Entity.Productos

Partial Class _GoogleMaps
    Inherits PageBaseClass

    Const codigo_pantalla As String = "PR-15"
    Const level As Integer = 2

    'Pagina
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddScripts(level)
        securityHandler.VerifyPantalla(codigo_pantalla, level)
        If Not IsPostBack Then
            Me.Title = Resources.Orbecatalog_Resources.Orbecatalog & " - " & Resources.Productos_Resources.Mapa_Pantalla

            If Not Request.Params.Item("id_producto") Is Nothing Then
                id_Actual = Request.Params("id_producto")
                cargarPuntos(GMap1, id_Actual)
                loadGMap()
            Else
                Response.Redirect("Default.aspx")
            End If
        End If
    End Sub

    Sub loadGMap()
        GMap1.Key = Orbelink.Helpers.GoogleMaps.GMapKey
        GMap1.Height = 250
        GMap1.Width = -100
        GMap1.setCenter(New GLatLng(9.9296240961483413, -84.0728759765625), 7)
        GMap1.mapType = GMapType.GTypes.Satellite

        Dim control As New GControl(GControl.preBuilt.GOverviewMapControl)
        GMap1.addControl(control)
        GMap1.addControl(New GControl(GControl.preBuilt.SmallMapControl, New GControlPosition(GControlPosition.position.Top_Right)))

        '**********
        'Agregar este codigo a la pagina de googleMaps
        Dim sb As New System.Text.StringBuilder()

        sb.Append("function(marker, point) {")
        sb.Append(" setLatLng('" & tbx_Latitud.ClientID & "', '" & tbx_Longitud.ClientID & "', point.lat(), point.lng());")
        sb.Append("}")

        Dim onClick As New GListener(GMap1.GMap_Id, GListener.Event.click, sb.ToString)
        GMap1.addListener(onClick)

        'Hasta aqui
        '**********

        'Agrega el style de behavior necesario
        Dim behaviorScript As New Literal
        behaviorScript.ID = "behaviorScript"
        behaviorScript.Text = "<style type=""text/css""> v\:* { behavior:url(#default#VML); } </style>"
        Header.Controls.Add(behaviorScript)
    End Sub

    'Puntos_Producto
    Protected Function insertPuntos_Producto(ByVal id_Producto As Integer, ByVal nombre As String, _
      ByVal latitud As String, ByVal longitud As String, ByVal Descripcion As String) As Boolean
        Dim Puntos_Producto As New Puntos_Producto
        Try
            Puntos_Producto.Id_Producto.Value = id_Producto
            Puntos_Producto.Nombre.Value = nombre
            Puntos_Producto.Lat.Value = latitud
            Puntos_Producto.Lon.Value = longitud
            Puntos_Producto.Descr_Punto.Value = Descripcion
            connection.executeInsert(queryBuilder.InsertQuery(Puntos_Producto))
            Return True
        Catch exc As Exception
            MyMaster.mostrarMensaje(exc.Message, True)

            Return False
        End Try
    End Function

    Sub cargarPuntos(ByRef theMap As Subgurim.Controles.GMap, ByVal id_Producto As Integer)
        Dim dataSet As Data.DataSet
        Dim counter As Integer
        Dim Puntos_Producto As New Puntos_Producto

        Puntos_Producto.Fields.SelectAll()
        Puntos_Producto.Id_Producto.Where.EqualCondition(id_Producto)
        queryBuilder.From.Add(Puntos_Producto)
        dataSet = connection.executeSelect(queryBuilder.RelationalSelectQuery)

        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables(0).Rows.Count > 0 Then
                Dim resul_Puntos As ArrayList = objectBuilder.TransformDataTable(dataSet.Tables(0), Puntos_Producto)
                For counter = 0 To resul_Puntos.Count - 1
                    Dim act_Punto_Producto As Puntos_Producto = resul_Puntos(counter)

                    Dim marker As New GMarker(New GLatLng(act_Punto_Producto.Lat.Value, act_Punto_Producto.Lon.Value))
                    Dim infoWindow As New GInfoWindow(marker, act_Punto_Producto.Descr_Punto.Value, False)

                    theMap.addGMarker(marker)
                    theMap.addInfoWindow(infoWindow)
                Next
            End If
        End If
    End Sub

    Protected Sub btn_Agregar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Agregar.Click
        If id_Actual > 0 Then
            If insertPuntos_Producto(id_Actual, tbx_N_Punto.Text, tbx_Latitud.Text, tbx_Longitud.Text, tbx_Descripcion.Text) Then
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Salvar, "Punto", False)
                MyMaster.concatenarMensaje("Ya puede cerrar esta ventana con el boton ""ok""", False)
                div_AgregarDireccion.Visible = False
                upd_Contenido.Update()
            Else
                MyMaster.MostrarMensaje("Error al insertar punto.", False)
            End If
        End If
    End Sub
End Class
