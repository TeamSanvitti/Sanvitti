<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomError.aspx.cs" Inherits="avii.Error.CustomError" %>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="~/Controls/Header.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Lan Global</title>
    <link href="../aerostyle.css" rel="stylesheet" type="text/css"/>
</head>
<body>
    <form id="form1" runat="server">
    <table cellspacing="0" cellpadding="0" width="100%" align="center">
    <tr>
        <td>
            <head:MenuHeader ID="MenuHeader1" runat="server"/>
        </td>
    </tr>
    </table>
    <div>
    <br /><br />
    <br /><br /><br />
     <table align="center" style="text-align:left" width="60%">
    <tr class="buttonlabel" align="left">
    <td>&nbsp;<asp:Label ID="lblHeader"  runat="server"></asp:Label>
     </td></tr>
     </table>
    <table bordercolor="#839abf" border="1" cellspacing="0" cellpadding="0" width="60%" align="center">
        <tr bordercolor="#839abf">
        <td  align="center" >
        <table cellspacing="5" cellpadding="5" width="100%" align="center">
        <tr>
        <td align="left" class="copy12hd">        
        
            
                
                
                </td>
            </tr>
            <tr>
                <td class="copy10grey" align="left">
        
                    <asp:Label ID="lblMsg" CssClass="copy10grey" runat="server"></asp:Label>
                    
                    <br />
                    <%--<asp:Label ID="lblMsg" runat="server"></asp:Label>--%>
                </td>
            </tr>
            <tr>
                <td align="left">
        
        
                    <asp:LinkButton ID="lnkReturn" CssClass="copy10Underline" OnClick="lnkReturn_Click" runat="server">Return to the dashboard</asp:LinkButton>
                     <br /><br />
                </td>
            </tr>
        </table>
        </td>
    </tr>
    </table>
        
    </div>
    </form>
</body>
</html>
