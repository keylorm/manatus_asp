
Imports Orbelink.Control.Facturas
Partial Class paypal
    Inherits Orbelink.FrontEnd6.PageBaseClass

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Orbelink.DBHandler.LanguageHandler.CurrentLanguage = Orbelink.DBHandler.LanguageHandler.Language.INGLES Then
            lbl_titulo.Text = "Steps To pay by Paypal"
            lbl_paso1.Text = "1. INSERT YOUR DATA"
            lbl_paso2.Text = "2. CHECK THE AMOUNT AND PAY"
            lbl_paso3.Text = "3. BACK TO WEB SITE"
            lbl_texto1.Text = "You will be redirected to a secure payment page with PAYPAL, where you can cancel with your credit card or with your PayPal account. (PAYPAL is the leading company in the world for processing secure electronic payments.)" & _
            "If you do not have an account with PAYPAL do not worry, you should not make any additional process to create it, simply fill out the form where they ask for your name, credit card and some other data." & _
            "<ul>" & _
            "<li>" & _
            "If you get a screen like the one below, click on ""continue to pay"" to pay with your credit card." & _
            "</li>" & _
            "<li>" & _
            "If you have an account and you want to pay with PAYPAL simply fill out the space to the right with your email and password." & _
            "</li>" & _
            "</ul>"
            lbl_texto1_1.Text = "If you are going to cancel with your credit card, when you reach a screen like this simply fill out the form that says ""Create a PayPal account."" With this option you pay directly with your credit card, and automatically keep a PayPal account that you can use to pay online for future purchases."
            lbl_texto2.Text = "Once the previous step, you will see a screen like this, where you can verify the amount to pay and click the button PAY NOW."
            lbl_texto3.Text = "Once canceled, it is important to click the button labeled ""Return to PAYGO Shopping Cart."" That way your transaction will be completed successfully and will be adopted immediately." & _
            "<br/>Note: If you do not click on the button above, the approval of the transaction will not be instant."

        End If

    End Sub



    'Protected Function ArmarQueryString() As String
    '    Dim query As String = ""
    '    query = "login.aspx?return=paypal.aspx"
    '    Return query
    'End Function

    Protected Sub btn_comprar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_comprar.Click
        'If securityHandler.Entidad > 0 Then
        Dim carrito As FacturaWebState = New FacturaWebState
        If carrito.MyCarrito > 0 Then
            Response.Redirect("~/paypal/Intermedia.aspx")
        Else
            Response.Redirect("~/ShoppingCart.aspx")
        End If
        'Else
        'Response.Redirect(ArmarQueryString())
        'End If
    End Sub
End Class
