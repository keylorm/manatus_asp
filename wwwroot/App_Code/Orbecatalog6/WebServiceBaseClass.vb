Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler
Imports Orbelink.Control.Security

Namespace Orbelink.Orbecatalog6
    Public Class WebServiceBaseClass
        Inherits System.Web.Services.WebService

        'Instancias de clases
        Protected connection As New SQLServer(Configuration.Config_DefaultConnectionString)
        Protected queryBuilder As QueryBuilder = New QueryBuilder()
        Protected securityHandler As SecurityHandler = New SecurityHandler(Configuration.Config_DefaultConnectionString)
    End Class
End Namespace