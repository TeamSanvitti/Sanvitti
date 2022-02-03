<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MDNProvisioning.aspx.cs" Inherits="avii.Admin.MDNProvisioning" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <LINK href="../aerostyle.css" type="text/css" rel="stylesheet">
	<script type="text/javascript">
	    function CallPrint() {
	        var prtContent = document.getElementById('divPrint');

	        var WinPrint = window.open('', '', 'letf=10,top=0,width=900px,height=400px,toolbar=0,scrollbars=0,status=0');
	        WinPrint.document.write(prtContent.innerHTML);

	        WinPrint.document.close();
	        WinPrint.focus();
	        WinPrint.print();
	        WinPrint.close();

	    }
    </script>
</head>
<body bgColor="#ffffff" leftmargin="0" rightmargin="0" topmargin="0">
    <form id="form1" runat="server">
    <table width="100%" align="center" border="0">
        <tr>
            <td colspan="2" align="right">
                <input onclick="CallPrint()" class="button" type="button" value=" Print " />
                <%--<a onclick="CallPrint()" class="button"> &nbsp; Print &nbsp;</a>--%>&nbsp;
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        </table>
    <div id="divPrint">
        
        <table width="100%" align="center">
        <tr>
            <td class="copy10grey" align="right" width="40%">
            SKU:
            </td>
            <td align="left">
                &nbsp;<asp:Label ID="lblSKU" CssClass="copy10grey" runat="server" ></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="copy10grey"  align="right">
            ESN:
            </td>
            <td align="left">
                &nbsp;<asp:Label ID="lblESN" CssClass="copy10grey" runat="server" ></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="copy10grey"  align="right">
            MDN:
            </td>
            <td align="left">
                &nbsp;<asp:Label ID="lblMDN" CssClass="copy10grey" runat="server" ></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="copy10grey"  align="right">
            PO:
            </td>
            <td align="left">
                &nbsp;<asp:Label ID="lblPO" CssClass="copy10grey" runat="server" ></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="copy10grey"  align="right">
            UPC:
            </td>
            <td  align="left">
                &nbsp;<asp:Label ID="lblUPC" CssClass="copy10grey" runat="server" ></asp:Label>
            </td>
        </tr>
        </table>    
    </div>
    </form>
</body>
</html>
