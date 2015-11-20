Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler

Namespace Orbelink.Helpers

    Public Module GoogleMaps

        Public Const localHostKey As String = "ABQIAAAAUhAQ_8Q4mSkNDGWm95CNpBS83PslMW-qvfWajeLw_nmEf9h63BR2XEf-jcx8DVGr6gPN4Q2SSSEhBQ"
        Dim _GMapKey As String = ""

        Public Property GMapKey() As String
            Set(ByVal value As String)
                _GMapKey = value
            End Set
            Get
                If _GMapKey.Length > 0 Then
                    Return _GMapKey
                Else
                    Return localHostKey
                End If
            End Get
        End Property

        Public Sub LoadJavaScriptFunctions(ByRef thePage As System.Web.UI.Page, ByVal pageLevel As Integer)
            Dim theScript As New HtmlGenericControl("script")

            Dim prefijoNivel As String = ""
            For counter As Integer = 0 To pageLevel - 1
                prefijoNivel &= "../"
            Next

            theScript.ID = "GoogleMaps_OrbelinkFunctions"
            theScript.Attributes.Add("type", "text/javascript")
            theScript.Attributes.Add("language", "javascript")
            theScript.Attributes.Add("src", prefijoNivel & "orbecatalog/Scripts/GoogleMaps_OrbelinkFunctions.js")

            ' Add the script to the Web page.
            thePage.Header.Controls.Add(theScript)

        End Sub

        'Private Function WriteGoogleMapsSrc(ByRef theKey As String) As String
        '    Dim script As String
        '    script = "<script src=""http://maps.google.com/maps?file=api&amp;v=2&amp;key=" & theKey & """ "
        '    script &= "type=""text/javascript""></script>"
        '    Return script
        'End Function

        'Private Function OpenGMapScript() As String
        '    Dim script As String

        '    script = "<script type=""text/javascript"">" & vbCrLf
        '    script &= "var sidebar_html = """";" & vbCrLf
        '    script &= "var gmarkers = [];" & vbCrLf
        '    script &= "var htmls = [];" & vbCrLf
        '    script &= "var i = 0;" & vbCrLf

        '    script &= "function load() {" & vbCrLf
        '    script &= "    if (GBrowserIsCompatible()) {" & vbCrLf
        '    script &= "        loadGMap();" & vbCrLf
        '    script &= "    }" & vbCrLf
        '    script &= "    document.getElementById(""" & _sideBarDivId & """).innerHTML = sidebar_html;" & vbCrLf
        '    script &= "}" & vbCrLf

        '    script &= "function createMarker(point,name,html) {" & vbCrLf
        '    script &= "  var marker = new GMarker(point);" & vbCrLf
        '    script &= "  GEvent.addListener(marker, ""click"", function() {" & vbCrLf
        '    script &= "        marker.openInfoWindowHtml(html);" & vbCrLf
        '    script &= "    });" & vbCrLf

        '    'save the info we need to use later for the sidebar
        '    script &= "  gmarkers[i] = marker;" & vbCrLf
        '    script &= "  htmls[i] = html;" & vbCrLf
        '    script &= "  sidebar_html += '<a href=""javascript:myclick(' + i + ')"">' + name + '</a><br>';" & vbCrLf
        '    script &= "  i++;" & vbCrLf
        '    script &= "  return marker;" & vbCrLf
        '    script &= "}" & vbCrLf

        '    'This function picks up the click and opens the corresponding info window
        '    script &= "function myclick(i) {" & vbCrLf
        '    script &= "  gmarkers[i].openInfoWindowHtml(htmls[i]);" & vbCrLf
        '    script &= "}" & vbCrLf

        '    Return script

        'End Function

        'Private Function CloseGMapScript() As String
        '    Return vbCrLf & "</script>" & vbCrLf & vbCrLf
        'End Function

        'Private Function LoadGMap(ByVal getCoordenates As Boolean) As String
        '    Dim script As String
        '    Dim counter As Integer

        '    script = "function loadGMap() {" & vbCrLf

        '    script &= "var map = new GMap2(document.getElementById(""" & _mapDivID & """));" & vbCrLf
        '    script &= "map.addControl(new GLargeMapControl());" & vbCrLf
        '    script &= "map.addControl(new GMapTypeControl(""S""));" & vbCrLf
        '    script &= "map.setCenter(new GLatLng( 9.909722, -85.571389), 8);" & vbCrLf

        '    'Desplegar el centro actual
        '    script &= "GEvent.addListener(map, ""moveend"", function() {" & vbCrLf
        '    script &= " var center = map.getCenter();" & vbCrLf
        '    script &= " document.getElementById(""message"").innerHTML = center.toString();" & vbCrLf
        '    script &= " });" & vbCrLf

        '    'No parece servir
        '    script &= "GEvent.addListener(map, ""rightclick"", function(marker, point) {" & vbCrLf
        '    script &= "map.openInfoWindow(point, document.createTextNode(point));  " & vbCrLf
        '    script &= "});" & vbCrLf

        '    'Muestra coordenadas del click
        '    If getCoordenates Then
        '        'script &= "GEvent.addListener(map, ""click"", function(marker, point) {" & vbCrLf
        '        'script &= "map.openInfoWindow(point, document.createTextNode(point));  " & vbCrLf
        '        'script &= "});" & vbCrLf

        '        script &= "GEvent.addListener(map, ""click"", function(marker, point) {" & vbCrLf

        '        script &= "});" & vbCrLf
        '    End If

        '    'Cargar puntos
        '    For counter = 0 To GMapMarkers.Count - 1
        '        script &= "var point = new GLatLng(" & GMapMarkers(counter).Latitude & ", " & GMapMarkers(counter).Longitude & ");" & vbCrLf
        '        script &= "var marker = createMarker(point, """ & GMapMarkers(counter).Name & """, """ & GMapMarkers(counter).Description & """);" & vbCrLf
        '        script &= "map.addOverlay(marker);" & vbCrLf
        '    Next
        '    script &= "}" & vbCrLf

        '    Return script
        'End Function

        'Public Sub WriteBody_OnLoad(ByRef thePage As System.Web.UI.Page)
        '    Dim script As String
        '    script = "<script type='text/javascript'>" & vbCrLf
        '    script &= "load();" & vbCrLf
        '    script &= "</script>"
        '    ' Add the script to the Web page.
        '    thePage.ClientScript.RegisterStartupScript(GetType(String), "ShowInfoPage", script)
        'End Sub



        'Public Sub AddGMapMarker(ByVal TheLatitude As Double, ByVal TheLongitude As Double, ByVal TheName As String, ByVal TheDescription As String)
        '    Dim themarker As New GMapMarker
        '    themarker.Latitude = TheLatitude
        '    themarker.Longitude = TheLongitude
        '    themarker.Name = TheName
        '    themarker.Description = TheDescription
        '    GMapMarkers.Add(themarker)
        'End Sub

        'Public Function GMapScript(ByVal getCoordenates As Boolean) As String
        '    Dim script As String = ""

        '    If _GMapKey.Length > 0 Then
        '        script = WriteGoogleMapsSrc(_GMapKey)
        '    Else
        '        script = WriteGoogleMapsSrc(localHostKey)
        '    End If
        '    script &= OpenGMapScript()
        '    script &= LoadGMap(getCoordenates)
        '    script &= CloseGMapScript()

        '    Return script
        'End Function

    End Module
End Namespace