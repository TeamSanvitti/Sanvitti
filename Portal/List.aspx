<%@ Register TagPrefix="foot" TagName="MenuHeader" Src="./Controls/Footer.ascx" %>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="./Controls/Header.ascx" %>
<%@ Register TagPrefix="serv" TagName="MenuSP" Src="./Controls/provider.ascx" %>
<%@ Page language="c#" Codebehind="List.aspx.cs" AutoEventWireup="false" Inherits="avii.List" %>
<HTML>
	<HEAD>
		<title>List</title>
			<script language=javascript src="avI.js"></script>
			<link href="aerostyle.css" rel="stylesheet" type="text/css">
	</HEAD>
	<BODY bgColor="#ffffff" leftmargin="0" rightmargin="0" topmargin="0" >
		<form id="Form1" method="post" runat="server">
			<table align="center" width="95%" border="0" cellpadding="0" cellspacing="0">
				<tr>
					<td valign="top" align="left"><head:menuheader id="MenuHeader1" runat="server"></head:menuheader></td>
				</tr>
				<tr>
					<td valign="top">
						<asp:Panel ID="pnlItem" Runat="server"></asp:Panel>
					</td>
				</tr>
									<tr>
										<td>
										<serv:MenuSP id="Menusss" runat="server"></serv:MenuSP>
										</td>
									</tr>				
				<tr>
					<td colspan="2"><foot:menuheader id="footer" runat="server"></foot:menuheader></td>
				</tr>
			</table>
		</form>
	</BODY>
</HTML>
