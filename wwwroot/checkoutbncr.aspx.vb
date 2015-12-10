
Partial Class checkoutbncr
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Redirect("http://booking.manatuscostarica.com/BNCR/IntermediaBNCR.aspx", False)
        'Response.Redirect("http://manatus.orbelink.com/BNCR/IntermediaBNCR.aspx", False)
    End Sub
End Class
