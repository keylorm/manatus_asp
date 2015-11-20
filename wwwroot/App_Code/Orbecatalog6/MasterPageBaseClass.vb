Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler
Imports Orbelink.Orbecatalog6
Imports Orbelink.Entity.Orbecatalog
Imports Orbelink.Control.Security
Imports System.IO

Namespace Orbelink.Orbecatalog6
    Public MustInherit Class MasterPageBaseClass
        Inherits System.Web.UI.MasterPage

        Delegate Sub PopUpOKButton(ByVal param As String)
        Protected elDelegado As PopUpOKButton

        Public Sub RegistrarPopUpOk(ByVal theDelegate As PopUpOKButton)
            elDelegado = theDelegate
        End Sub

        'Instancias de clases
        Protected connection As New SQLServer(Configuration.Config_DefaultConnectionString)
        Protected queryBuilder As QueryBuilder = New QueryBuilder()
        Protected securityHandler As SecurityHandler = New SecurityHandler(Configuration.Config_DefaultConnectionString)

        ''' <summary>
        ''' Limpia todos los mensajes del pop up de mensajes
        ''' </summary>
        ''' <remarks></remarks>
        Public MustOverride Sub limpiarMensajes()

        ''' <summary>
        ''' Propiedad con el ScriptManager registrado en el MasterPage
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public MustOverride ReadOnly Property TheScriptManager() As ScriptManager

        ''' <summary>
        ''' Muestra un mensaje en un popup, que puede ser de error o no.
        ''' </summary>
        ''' <param name="mensaje"></param>
        ''' <param name="esError"></param>
        ''' <remarks></remarks>
        Public MustOverride Sub MostrarMensaje(ByVal mensaje As String, ByVal esError As Boolean)

        ''' <summary>
        ''' Redirecciona de manera inteligente, es decir almacenando variables de ejecucion importantes cuando se requiera
        ''' </summary>
        ''' <param name="urlDestino"></param>
        ''' <param name="codigoOrigen"></param>
        ''' <remarks></remarks>
        Public MustOverride Sub RedirectMe(ByVal urlDestino As String, ByVal codigoOrigen As String)

        ''' <summary>
        ''' Concatena al popup de mensajes uno nuevo, que puede ser de error o no.
        ''' </summary>
        ''' <param name="mensaje"></param>
        ''' <param name="esError"></param>
        ''' <remarks></remarks>
        Public MustOverride Sub concatenarMensaje(ByVal mensaje As String, ByVal esError As Boolean)

        ''' <summary>
        ''' Retorna el codigo javascript necesario para mostrar un popUp desde el cliente
        ''' </summary>
        ''' <param name="direccion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public MustOverride Function obtenerIframeString(ByVal direccion As String) As String

        ''' <summary>
        ''' Muestra de manera dinamica un popUp con la direccion recibida
        ''' </summary>
        ''' <param name="direccion"></param>
        ''' <remarks></remarks>
        Public MustOverride Sub mostrarPopUp(ByVal direccion As String)

        'Mensajes de acciones
        Public Enum Acciones As Integer
            Salvar
            Modificar
            Eliminar
        End Enum

        Public Sub MostrarResultadoAccion(ByVal laAccion As Acciones, ByVal elemento As String, ByVal esError As Boolean)
            Dim stringMensaje As String = Nothing

            Select Case laAccion
                Case Acciones.Salvar
                    If esError Then
                        stringMensaje = Resources.Orbecatalog_Resources.MensajeSalvar_Error
                    Else
                        stringMensaje = Resources.Orbecatalog_Resources.MensajeSalvar_Exito
                    End If
                Case Acciones.Modificar
                    If esError Then
                        stringMensaje = Resources.Orbecatalog_Resources.MensajeModificar_Error
                    Else
                        stringMensaje = Resources.Orbecatalog_Resources.MensajeModificar_Exito
                    End If
                Case Acciones.Eliminar
                    If esError Then
                        stringMensaje = Resources.Orbecatalog_Resources.MensajeEliminar_Error
                    Else
                        stringMensaje = Resources.Orbecatalog_Resources.MensajeEliminar_Exito
                    End If
            End Select

            Dim mensaje As String = String.Format(stringMensaje, elemento)
            MostrarMensaje(mensaje, esError)
        End Sub


        Protected Function generarLabelDeMensaje(ByVal mensaje As String, ByVal esError As Boolean) As Label
            Dim mensajeNuevo As New Label
            If esError Then
                mensajeNuevo.ForeColor = Drawing.Color.Red
            Else
                mensajeNuevo.ForeColor = Drawing.Color.Green
            End If
            mensajeNuevo.Text = mensaje & "<br />"
            Return mensajeNuevo
        End Function

        Protected Sub Buscar(ByVal cod_pantalla As String, ByVal searchString As String)
            Dim cod_busqueda As String = cod_pantalla.Substring(0, 2) & "-SR"
            Dim dataSet As New Data.DataSet
            Dim pantallas As New Pantallas
            Dim link As String = ""
            Dim elNivel = Session("NivelPantalla")
            For counter = 0 To elNivel - 1
                link &= "../"
            Next
            pantallas.Link.ToSelect = True
            pantallas.Codigo_Pantalla.Where.EqualCondition(cod_busqueda)
            Dim consulta As String = queryBuilder.SelectQuery(pantallas)
            dataSet = connection.executeSelect(consulta)

            If dataSet.Tables.Count > 0 Then
                If dataSet.Tables(0).Rows.Count > 0 Then
                    ObjectBuilder.CreateObject(dataSet.Tables(0), 0, pantallas)
                    link &= pantallas.Link.Value
                    link &= "?search=true&general=" & searchString
                    Response.Redirect(link)
                End If
            End If
        End Sub

        Protected Sub agregarRutinaError(ByVal mensaje As String)
            Dim logDirectory As String = Configuration.outPath & "/rutina/"
            Directory.CreateDirectory(logDirectory)
            Dim filePath As String = logDirectory & "Rutina.xml"

            Dim xmldoc As New System.Xml.XmlDocument
            Dim nodoPrincipal As System.Xml.XmlElement

            If File.Exists(filePath) Then
                xmldoc.Load(filePath)
                nodoPrincipal = xmldoc.FirstChild.NextSibling
            Else
                Dim declarationNode As System.Xml.XmlNode = xmldoc.CreateNode(System.Xml.XmlNodeType.XmlDeclaration, "", "")
                xmldoc.AppendChild(declarationNode)

                'let's add the root element
                nodoPrincipal = xmldoc.CreateElement("LogRutinas")
                xmldoc.AppendChild(nodoPrincipal)
            End If

            Dim nuevoNodo As System.Xml.XmlElement = xmldoc.CreateElement("rutina")
            nuevoNodo.SetAttribute("IP", 22)
            nuevoNodo.SetAttribute("Date", System.DateTime.Now.ToLongDateString & " " & System.DateTime.Now.ToLongTimeString)

            Dim control As System.Xml.XmlElement = xmldoc.CreateElement("ErrorInfo")
            control.SetAttribute("Error", mensaje)
            Dim page As System.Xml.XmlElement = xmldoc.CreateElement("PageInfo")
            page.SetAttribute("TheFile", Me.Request.FilePath)
            nuevoNodo.AppendChild(page)
            nuevoNodo.AppendChild(control)
            nodoPrincipal.AppendChild(nuevoNodo)

            If nodoPrincipal.ChildNodes.Count > 50 Then
                nodoPrincipal.RemoveChild(nodoPrincipal.FirstChild)
            End If
            xmldoc.Save(filePath)
        End Sub
    End Class
End Namespace