<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SKUPOStatusDetails.aspx.cs" Inherits="avii.SKUPOStatusDetails" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="~/Controls/Header.ascx" %>
<%@ Register TagPrefix="SKU" TagName="SKUStatus" Src="~/Controls/SKUStatusDetails.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/aerostyle.css" type="text/css" rel="stylesheet"/>
</head>
<body bgcolor="#ffffff"  leftmargin="0" topmargin="0" marginwidth="0" marginheight="0">
    <form id="form1" runat="server">
     <table cellspacing="0" cellpadding="0" border="0"  width="100%" align="center">
        <tr>
            <td>
                <head:MenuHeader ID="HeadAdmin1" runat="server" ></head:MenuHeader>
            </td>
        </tr> 
        </table> 
        <br />
        <table width="100%" border="0">
            <tr>
                <td class="buttonlabel">
                SKU Status Summary
                </td>
            </tr>
            <tr>
                <td>
                <br />
                <table bordercolor="#839abf" border="1" cellspacing="0" cellpadding="0" width="100%" align="center">
                <tr bordercolor="#839abf">
                    <td>
                        
                        
                        <SKU:SKUStatus ID="skuStatus1" runat="server" />
                        
                    </td>
                </tr>
                </table>
            </td>
            </tr>
            </table>

    </form>
</body>
</html>
