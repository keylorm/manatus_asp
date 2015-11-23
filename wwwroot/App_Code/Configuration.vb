Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler
Imports Orbelink.Orbecatalog6
Imports Orbelink.Control.Archivos
Imports Orbelink.Helpers
Public Class Configuration
    Public Shared ReadOnly Property Config_WebsiteRoot() As String
        Get

            'Return "http://booking.manatuscostarica.com/"
            Return "http://localhost:8608/wwwroot/"

        End Get
    End Property

    Public Shared ReadOnly Property Config_WebSite_LocalhostRoot() As String
        Get
                

            'Return "http://www.orbelinksafeserver.com/manatusHotel/"
            Return "http://localhost:8608/wwwroot/"

        End Get
    End Property

    Public Shared ReadOnly Property Config_SiteName() As String
        Get
                 Return "Manatus"
        End Get
    End Property

    Public Shared ReadOnly Property Config_ShortSiteName() As String
        Get
            Return "Manatus"
        End Get
    End Property

    Public Shared ReadOnly Property Config_DefaultConnectionString() As String
        Get
            'Return "Data Source=SERGIO-PC\SQLEXPRESS;Initial Catalog=manatus;User ID=sa;Password=robot"
            'Return "Data Source=es-04;Initial Catalog=manatus;User ID=sa;Password=robot"
            'Return "Data Source=170782-1;Initial Catalog=manatus;User ID=manatus_user;Password=robot"
            Return "Data Source=192.163.216.119;Initial Catalog=manatus_keylor;User ID=manatus_keylor_user;Password=robot"
            'Return "Data Source=192.163.216.119;Initial Catalog=manatus;User ID=manatus_user;Password=robot"
            'Return "Data Source=184.173.7.137;Initial Catalog=manatusPruebas;User ID=manatus_user;Password=robot"
        End Get
    End Property

    Public Shared ReadOnly Property Config_DefaultSenderEmail() As String
        Get
            Return "mailer@manatuscostarica.com"
           'Return "info@manatuscostarica.com"
        End Get
    End Property

    Public Shared ReadOnly Property Config_DefaultIdEntidad() As Integer
        Get
            Return "1"
        End Get
    End Property

    Public Shared ReadOnly Property Config_GMapKey() As String
        Get
            'Para quitar esta exception, debe comentar esta linea y en el return escribir el valor correspondiente
            'Throw New NotImplementedException("Debe especificar el valor correspondiente.")
            Return Nothing
        End Get
    End Property

    Public Shared ReadOnly Property Config_DefaultAdminEmail() As String
        Get
            Return "info@manatuscostarica.com"
			'Return "lalonso@orbelink.com"
	    'Return "reservations@manatuscostarica.com"
            
      
        End Get
    End Property

    Public Shared ReadOnly Property SiteAdminPasswordHash() As String
        Get
            'Para quitar esta exception, debe comentar esta linea y en el return escribir el valor correspondiente
            'Throw New NotImplementedException("Debe especificar el valor correspondiente.")
            Return "BE56489BD9859CEE7A77D8569BCB5F74AAD261AB"
        End Get
    End Property

'Constantes
    Public Enum EstadoPantalla As Integer
        NORMAL = 0
        MODIFICAR = 1
        LOGED = 2
        CONSULTAR = 3
    End Enum

    Public Const maxProductosPagPrincipal As Integer = 0
    Public Const TimeOut_Minutes As Integer = 0

    'Path
    Public Shared rootPath As String
    Public Shared outPath As String
    Public Shared logsPath As String
    Public Shared uploadPath As String
    Public Shared crypto As Orbelink.Crypter.Crypto

    Public Const DefaultLang As Orbelink.DBHandler.LanguageHandler.Language = LanguageHandler.Language.ESPANOL

    Public Shared Sub InitConfig()
        rootPath = System.AppDomain.CurrentDomain.BaseDirectory
        outPath = rootPath.Substring(0, rootPath.Length - 1)
        outPath = outPath.Substring(0, outPath.LastIndexOf("\") + 1)
        uploadPath = rootPath & "\Upload\"
        If outPath.Chars(outPath.Length - 1) <> "\" Then
            outPath &= "\"
        End If

        logsPath = outPath & "logs\"
        System.IO.Directory.CreateDirectory(logsPath)

        LanguageHandler.AddUsedLanguages(LanguageHandler.Language.INGLES)
        LanguageHandler.AddUsedLanguages(LanguageHandler.Language.ESPANOL)


        Orbelink.Helpers.GoogleMaps.GMapKey = Config_GMapKey
        crypto = New Orbelink.Crypter.Crypto(Orbelink.Crypter.Crypto.CryptoProvider.DES)
        crypto.IV = "PasswordOrbelink2009uytrefghtrfgPasswordOrbelink2009uytrefghtrfg"
        crypto.Key = "Password"
    End Sub
End Class
