Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler
Imports Orbelink.Control.Security

Namespace Orbelink.FrontEnd6
    Public MustInherit Class MasterBaseClass
        Inherits System.Web.UI.MasterPage

        'Instancias de clases
        Protected connection As New SQLServer(Configuration.Config_DefaultConnectionString)
        Protected queryBuilder As QueryBuilder = New QueryBuilder()
        Protected securityHandler As SecurityHandler = New SecurityHandler(Configuration.Config_DefaultConnectionString)

        Public MustOverride Sub MostrarMensaje(ByVal mensaje As String, ByVal esError As Boolean)

    End Class
End Namespace