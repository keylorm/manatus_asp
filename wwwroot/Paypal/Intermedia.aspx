<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Intermedia.aspx.vb" Inherits="Intermedia" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Reservaciones Hotel Manatus, reservaciones on-line, Mejor Hotel Tortuguero, hospedaje,
        alojamiento en Tortuguero</title>
<%--  <link href="estilo.css" rel="stylesheet" type="text/css" />--%>
    <script type="text/javascript" language="javascript">
    function FormSubmit(){
    document.frm_paypal.submit();
    }
    </script>



</head>

<body onload="FormSubmit()">
    <form id="frm_paypal" name="frm_paypal" action="https://www.paypal.com/cgi-bin/webscr">
        <input type="hidden" name="cmd" value="_cart" />
        <input type="hidden" name="upload" value="1" />
          <input type="hidden" name="business" value="sales@orbelink.com" />
    <input type="hidden" name="rm" value="2" />
        <asp:Literal ID="ltr_values" runat="server"></asp:Literal>        
      
    </form>
<script type="text/javascript">
_uacct = "UA-2144377-5";
urchinTracker();
</script>
</body>
</html>
