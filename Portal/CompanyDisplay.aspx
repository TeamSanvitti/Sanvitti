<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CompanyDisplay.aspx.cs" Inherits="avii.CompanyDisplay" %>
<%@ Register TagPrefix="company" TagName="info" Src="/Controls/CompanyStores.ascx" %>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="/Controls/Header.ascx" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="/Controls/Footer.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>.:: Customer Stores - Lan Global inc. ::.</title>
    <link href="../aerostyle.css" rel="stylesheet" type="text/css"/>
    <script language="javascript" src="/avI.js" type="text/javascript"></script>
	<LINK href="/Styles.css" type="text/css" rel="stylesheet"> 

</head>
<body>
    <form id="form1" runat="server">
    <div>
	<table cellSpacing="0" cellPadding="0"  border="0" align="center" width="95%">
		<tr>
			<td>
			<head:menuheader id="MenuHeader" runat="server"></head:menuheader>
			</td>
		</tr>
     </table>
             
        <table width="95%">
            <tr>
                <td>
                    <asp:Label ID="lblMsg" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <company:info id="comp" runat="server"></company:info>                
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Button id="btnClose" CssClass="Button" Text ="Close form" runat="server" />
                </td>
            </tr>
        </table>
        <table cellSpacing="0" cellPadding="0"  border="0" align="center" width="95%">
            <tr>
			    <td>
						&nbsp;
					</td>
				</tr>
				<TR>
					<TD>
						<foot:MenuFooter id="Menuheader2" runat="server"></foot:MenuFooter></TD>
				</TR>
			</table>
    </div>
    </form>
</body>
</html>
