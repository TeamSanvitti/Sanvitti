<%@ Register TagPrefix="head" TagName="MenuHeader" Src="./Controls/Header.ascx" %>
<%@ Register TagPrefix="foot" TagName="MenuHeader" Src="./Controls/Footer.ascx" %>
<%@ Register TagPrefix="serv" TagName="MenuSP" Src="./Controls/provider.ascx" %>
<%@ Page language="c#" Codebehind="form.aspx.cs" AutoEventWireup="false" Inherits="avii.form" %>
<HTML>
	<HEAD>
		<title>Forms</title>
			<script language=javascript src="avI.js"></script>
		
		<LINK href="aerostyle.css" type="text/css" rel="stylesheet">
	</HEAD>
	<BODY bgColor="#ffffff" leftMargin="0" topMargin="0" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="95%" align="center" border="0">
				<tr>
					<td vAlign="top" align="left"><head:menuheader id="MenuHeader1" runat="server"></head:menuheader></td>
				</tr>
			</table>
			<table cellSpacing="0" cellPadding="0" width="95%" align="center" border="0">
				<tr>
					<td class="bgleft" vAlign="top" width="200"><br>
						<table class="copy10grey2" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td class="button" align="center" bgColor="#dee7f6">forms</td>
							</tr>
						</table>
						<div align="center"><br>
							<IMG height="96" hspace="8" src="images/2_pic_1.jpg" width="145" vspace="8">
						</div>
					</td>
					<td width="1" bgColor="#a6bce0"><IMG height="1" src="images/dotblue.gif" width="1"></td>
					<td>
						<table cellSpacing="2" cellPadding="2" width="100%" align="center" border="0">
							<tr>
								<td class="copy10grey" vAlign="top" colSpan="3" height="200">
									<div align="center" height="200"><asp:panel id="pnlForm" Runat="server"></asp:panel></div>
								</td>
							</tr>
                            <tr><td><br /><br /></td></tr>
						</table>
					</td>
				</tr>
			</table>
			<table cellSpacing="0" cellPadding="0" width="95%" align="center" border="0">
				<tr>
					<td colSpan="2"><foot:menuheader id="footer" runat="server"></foot:menuheader></td>
				</tr>
			</table>
		</form>
	</BODY>
</HTML>
