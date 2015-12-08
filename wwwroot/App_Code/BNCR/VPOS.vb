Imports Microsoft.VisualBasic

Imports System.IO
Imports VPOS20_PLUGIN
Imports System.Collections.Specialized
Imports Orbelink.DBHandler
Imports System
Imports System.Net
Imports System.Text
Imports Orbelink.Entity.Productos
Imports Orbelink.Entity.Facturas
Public Class VPOS

    Dim LlavePrivadaFirmaRSA As String = "C:\domains\booking.manatuscostarica.com\wwwroot\App_Code\BNCR\LlaveFirmaComercioPrivada.txt"
    Dim LlavePublicaCifradoRSA As String = "C:\domains\booking.manatuscostarica.com\wwwroot\App_Code\BNCR\LLAVE.VPOS.CRYPTO.1024.txt"
    Dim LlavePrivadaCifrado As String = "C:\domains\booking.manatuscostarica.com\wwwroot\App_Code\BNCR\LlaveCifradoComercioPrivada.txt"
    Dim LlavePublicaVerificacionFirma As String = "C:\domains\booking.manatuscostarica.com\wwwroot\App_Code\BNCR\LLAVE.VPOS.FIRMA.1024.txt"

    'Dim LlavePrivadaFirmaRSA As String = "C:\Disco D\Repositorios\manatus_asp\wwwroot\App_Code\BNCR\LlaveFirmaComercioPrivada.txt"
    'Dim LlavePublicaCifradoRSA As String = "C:\Disco D\Repositorios\manatus_asp\wwwroot\App_Code\BNCR\LLAVE.VPOS.CRYPTO.1024.txt"
    'Dim LlavePrivadaCifrado As String = "C:\Disco D\Repositorios\manatus_asp\wwwroot\App_Code\BNCR\LlaveCifradoComercioPrivada.txt"
    'Dim LlavePublicaVerificacionFirma As String = "C:\Disco D\Repositorios\manatus_asp\wwwroot\App_Code\BNCR\LLAVE.VPOS.FIRMA.1024.txt"

    'Const VectorInicializacion As String = "A0B5DD830ED25B29"
    Const VectorInicializacion As String = "A0B5DD830EDC8753"
    Const IDACQUIRER As String = "12"
    Const IDCOMMERCE As String = "2659"
    Const IDMALL As String = "0"
    Const terminalCode As String = "00000000"
    Const purchaseCurrencyCode As String = "840"

    'Const URL As String = "https://servicios.alignet.com/VPOS/MM/transactionStart20.do"
    'Const URL As String = "https://vpayment.verifika.com/VPOS/MM/transactionStart20.do"
    Const URL As String = "https://vpayment.verifika.com/VPOS/MM/transactionStart20.do"

    Public Function EnviarInfo(ByVal datasetItems As Data.DataTable, ByVal purchaseOperationNumber As Integer, ByVal purchaseAmount As Double, ByVal billingFirstName As String, ByVal billingLastName As String, ByVal billingEMail As String, ByVal billingAddress As String, ByVal billingZIP As String, ByVal billingCity As String, ByVal billingState As String, ByVal billingCountry As String, ByVal billingPhone As String, Optional ByVal shippingFirstName As String = "", Optional ByVal shippingLastName As String = "", Optional ByVal shippingEmail As String = "", Optional ByVal shippingAddress As String = "", Optional ByVal shippingZIP As String = "", Optional ByVal shippingCity As String = "", Optional ByVal shippingState As String = "", Optional ByVal shippingCountry As String = "", Optional ByVal shippingPhone As String = "", Optional ByVal HTTPSessionId As String = "", Optional ByVal additionalObservations As String = "", Optional ByVal terminalCodeLocal As String = "") As String

        Dim srVPOSLlaveCifradoPublica As StreamReader = New StreamReader(LlavePublicaCifradoRSA)
        Dim srComercioLlaveFirmaPrivada As StreamReader = New StreamReader(LlavePrivadaFirmaRSA)
        Dim oVPOSBean As VPOSBean = New VPOSBean()
        If datasetItems.Rows.Count > 0 Then
            Dim Producto As New Producto
            Dim DetalleFactura As New DetalleFactura
            For i As Integer = 0 To datasetItems.Rows.Count - 1
                ObjectBuilder.CreateObject(datasetItems, i, DetalleFactura)
                ObjectBuilder.CreateObject(datasetItems, i, Producto)
                Dim precio As Decimal = DetalleFactura.Precio_Unitario.Value
                oVPOSBean.addProduct(DetalleFactura.Detalle.Value, Producto.Nombre.Value, DetalleFactura.Precio_Unitario.Value, DetalleFactura.Cantidad.Value)
            Next

        End If



        'Datos brindados por el banco nacional
        oVPOSBean.acquirerId = IDACQUIRER
        oVPOSBean.commerceId = IDCOMMERCE
        oVPOSBean.commerceMallId = IDMALL
        Dim idioma As String = "SP"
        If Orbelink.DBHandler.LanguageHandler.CurrentLanguage = Orbelink.DBHandler.LanguageHandler.Language.INGLES Then
            idioma = "EN"
        End If
        oVPOSBean.language = idioma
        If terminalCodeLocal.Length > 0 Then
            oVPOSBean.terminalCode = terminalCodeLocal
        Else
            oVPOSBean.terminalCode = terminalCode
        End If

        oVPOSBean.purchaseOperationNumber = purchaseOperationNumber
        Dim amount As String = FormatNumber(purchaseAmount, 2, TriState.UseDefault, TriState.False, TriState.False).ToString.Replace(".", "")
        amount = amount.ToString.Replace(",", "")
        oVPOSBean.purchaseAmount = amount
        oVPOSBean.purchaseCurrencyCode = purchaseCurrencyCode

        'No requeridos
        'oVPOSBean.purchaseIPAddress = "Dirección IP del comprador"
        'oVPOSBean.language = "Idioma usado, tiene dos posibles valores: Español (SP), Inglés (si no se envía valor alguno el idioma por defecto será el español)"
        'oVPOSBean.tipAmount = "Valor de propina. El formato será igual al del campo purchaseAmount"
        'oVPOSBean.cardType = "VISA,MC,AMEX, etc.) "

        If billingFirstName.Length > 30 Then
            billingFirstName = billingFirstName.Remove(30)
        End If
        If billingLastName.Length > 50 Then
            billingLastName = billingLastName.Remove(50)
        End If
        If billingEMail.Length > 50 Then
            billingEMail = billingEMail.Remove(50)
        End If
        If billingAddress.Length > 50 Then
            billingAddress = billingAddress.Remove(50)
        End If
        If billingZIP.Length > 10 Then
            billingZIP = billingZIP.Remove(10)
        End If
        If billingCity.Length > 50 Then
            billingCity = billingCity.Remove(50)
        End If
        If billingState.Length > 15 Then
            billingState = billingState.Remove(15)
        End If
        If billingCountry.Length > 2 Then
            billingCountry = billingCountry.Remove(2)
        End If
        If billingPhone.Length > 15 Then
            billingPhone = billingPhone.Remove(15)
        End If

        oVPOSBean.billingFirstName = billingFirstName
        oVPOSBean.billingLastName = billingLastName
        oVPOSBean.billingEMail = billingEMail
        oVPOSBean.billingAddress = billingAddress
        oVPOSBean.billingZIP = billingZIP
        oVPOSBean.billingCity = billingCity
        oVPOSBean.billingState = billingState
        oVPOSBean.billingCountry = billingCountry
        oVPOSBean.billingPhone = billingPhone

        If shippingFirstName.Length > 0 Then

            If shippingFirstName.Length > 30 Then
                shippingFirstName = shippingFirstName.Remove(30)
            End If
            If shippingLastName.Length > 50 Then
                shippingLastName = shippingLastName.Remove(50)
            End If
            If shippingEmail.Length > 30 Then
                shippingEmail = shippingEmail.Remove(30)
            End If
            If shippingAddress.Length > 50 Then
                shippingAddress = shippingAddress.Remove(50)
            End If
            If shippingZIP.Length > 10 Then
                shippingZIP = shippingZIP.Remove(10)
            End If
            If shippingCity.Length > 50 Then
                shippingCity = shippingCity.Remove(50)
            End If
            If shippingState.Length > 15 Then
                shippingState = shippingState.Remove(15)
            End If
            If shippingCountry.Length > 2 Then
                shippingCountry = shippingCountry.Remove(2)
            End If
            If shippingPhone.Length > 15 Then
                shippingPhone = shippingPhone.Remove(15)
            End If

            oVPOSBean.shippingFirstName = shippingFirstName
            oVPOSBean.shippingLastName = shippingLastName
            oVPOSBean.shippingEMail = shippingEmail
            oVPOSBean.shippingAddress = shippingAddress
            oVPOSBean.shippingZIP = shippingZIP
            oVPOSBean.shippingCity = shippingCity
            oVPOSBean.shippingState = shippingState
            oVPOSBean.shippingCountry = shippingCountry
            oVPOSBean.shippingPhone = shippingPhone
        End If

        If HTTPSessionId.Length > 0 Then
            oVPOSBean.HTTPSessionId = HTTPSessionId
        End If

        If additionalObservations.Length > 0 Then
            oVPOSBean.additionalObservations = additionalObservations
        End If

        Dim oVPOSSend As VPOSSend = New VPOSSend(srVPOSLlaveCifradoPublica, srComercioLlaveFirmaPrivada, VectorInicializacion)
        oVPOSSend.execute(oVPOSBean)

        'Recuperando datos cifrados
        Dim sCipheredSessionKey As String = oVPOSBean.cipheredSessionKey
        Dim sCipheredXML As String = oVPOSBean.cipheredXML
        Dim sCipheredSignature As String = oVPOSBean.cipheredSignature

        Dim result As String = "<form name='frmSolicitudPago' method='post' action='" & URL & "'>" & _
        "<input type='hidden' name='IDACQUIRER' value='" & IDACQUIRER & "'>" & _
        "<input type='hidden' name='IDCOMMERCE' value='" & IDCOMMERCE & "'>" & _
        "<input type='hidden' name='XMLREQ' value='" & sCipheredXML & "'>" & _
        "<input type='hidden' name='DIGITALSIGN' value='" & sCipheredSignature & "'>" & _
        "<input type='hidden' name='SESSIONKEY' value='" & sCipheredSessionKey & "'>" & _
        "</form>"
        Return result

    End Function

    Public Function RecibirInfo(ByRef coll As NameValueCollection) As TransactionResult

        Dim TransactionResult As New TransactionResult
        Dim sIDACQUIRER As String = coll.Get("IDACQUIRER")
        Dim sIDCOMMERCE As String = coll.Get("IDCOMMERCE")
        Dim sXMLRES As String = coll.Get("XMLRES")
        Dim sSESSIONKEY As String = coll.Get("SESSIONKEY")
        Dim sDIGITALSIGN As String = coll.Get("DIGITALSIGN")

        Dim oVPOSBean As VPOSBean = New VPOSBean()

        oVPOSBean.cipheredXML = sXMLRES
        oVPOSBean.cipheredSessionKey = sSESSIONKEY
        oVPOSBean.cipheredSignature = sDIGITALSIGN

        Dim srVPOSLlaveFirmaPublica As StreamReader = New StreamReader(LlavePublicaVerificacionFirma)
        Dim srComercioLlaveCifradoPrivada As StreamReader = New StreamReader(LlavePrivadaCifrado)

        Dim oVPOSReceive As VPOSReceive = New VPOSReceive(srVPOSLlaveFirmaPublica, srComercioLlaveCifradoPrivada, VectorInicializacion)
        oVPOSReceive.execute(oVPOSBean)
        TransactionResult.result = False
        If oVPOSBean.validSign = True Then
            'El descifrado fue correcto y la firma digital es correcta
            TransactionResult.authorizationResult = oVPOSBean.authorizationResult
            TransactionResult.authorizationCode = oVPOSBean.authorizationCode
            TransactionResult.purchaseOperationNumber = oVPOSBean.purchaseOperationNumber
            TransactionResult.errorCode = oVPOSBean.errorCode
            TransactionResult.errorMessage = oVPOSBean.errorMessage
            TransactionResult.language = oVPOSBean.language

            If oVPOSBean.authorizationResult = "00" Then 'Transaccion Autorizada
                TransactionResult.result = True
                'TransactionResult.authorizationCode = oVPOSBean.authorizationCode
                TransactionResult.message = "Transaction Successfull"
            Else
                TransactionResult.result = False
                TransactionResult.message = "Transaction Unsuccessfull"
                'Dim errorCode As String = oVPOSBean.errorCode
                'Dim errorMessage As String = oVPOSBean.errorMessage
                If oVPOSBean.authorizationResult = "01" Then 'Operación Denegada por BNCR
                End If
                If oVPOSBean.authorizationResult = "05" Then 'Operación Rechazada por VPOS
                End If
            End If

        Else
            TransactionResult.result = False
            TransactionResult.message = "Firma Digital Invalida"
        End If

        Return TransactionResult
    End Function

    Structure TransactionResult
        Dim result As Boolean
        Dim message As String
        'Datos pedidos por VPOS
        Dim authorizationResult As String
        Dim authorizationCode As String
        Dim purchaseOperationNumber As Integer
        Dim errorCode As String
        Dim errorMessage As String
        Dim language As String
    End Structure


End Class
