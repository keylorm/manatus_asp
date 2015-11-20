Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols

<WebService(Namespace:="http://orbelink.com/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
 <System.Web.Script.Services.ScriptService()> _
Public Class Entidades
    Inherits Orbelink.Orbecatalog6.WebServiceBaseClass

    '<System.Web.Services.WebMethod()> _
    '<System.Web.Script.Services.ScriptMethod()> _
    'Public Function GetCompletionList(ByVal prefixText As String, ByVal count As Integer) As String()
    '    Dim entidadHandler As New Orbelink.Control.Entidades.EntidadHandler(connection)
    '    Return entidadHandler.buscarEntidades(prefixText, count)
    'End Function

    <System.Web.Services.WebMethod()> _
    <System.Web.Script.Services.ScriptMethod()> _
    Public Function GetCompletionList(ByVal prefixText As String, ByVal count As Integer, ByVal contextKey As String) As String()
        Dim entidadHandler As New Orbelink.Control.Entidades.EntidadHandler(Configuration.Config_DefaultConnectionString)
        Return entidadHandler.buscarEntidades(prefixText, count, contextKey)
    End Function

End Class
