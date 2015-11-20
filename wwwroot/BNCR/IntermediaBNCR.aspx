<%@ Page Language="VB" AutoEventWireup="false" CodeFile="IntermediaBNCR.aspx.vb" Inherits="IntermediaBNCR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Transfering to BNCR, please wait...</title>

    <script type="text/javascript" language="javascript">
    function FormSubmit(){
    document.frmSolicitudPago.submit();
    }
    </script>
</head>
<body onload="FormSubmit()">

        <asp:Literal ID="ltr_values" runat="server"></asp:Literal>    
</body>
</html>
