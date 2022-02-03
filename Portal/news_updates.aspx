<%@ Register TagPrefix="serv" TagName="MenuSP" Src="./Controls/provider.ascx" %>
<%@ Register TagPrefix="foot" TagName="MenuHeader" Src="./Controls/Footer.ascx" %>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="./Controls/Header.ascx" %>
<%@ Page language="c#" Codebehind="news_updates.aspx.cs" AutoEventWireup="false" Inherits="avii.news_updates" %>
<HTML>
	<HEAD>
		<title>Updates</title>
		<link href="aerostyle.css" rel="stylesheet" type="text/css"/>
		<script language="javascript" src="avI.js"></script>
	</HEAD>
	<BODY bgColor="#ffffff" leftmargin="0" rightmargin="0" topmargin="0">
		<form id="Form1" method="post" runat="server">
			<table align="center" width="95%" border="0" cellpadding="0" cellspacing="0">
				<tr>
					<td valign="top" align="left"><head:menuheader id="MenuHeader1" runat="server"></head:menuheader></td>
				</tr>
			</table>
			<table width="95%" border="0" align="center" cellpadding="0" cellspacing="0">
				<tr>
					<td width="200" valign="top" class="bgleft"><br>
						<table width="100%" border="0" cellpadding="0" cellspacing="0" class="copy10grey2">
							<tr>
								<td align="center" bgcolor="#dee7f6" class="button">
									news &amp; updates</td>
							</tr>
						</table>
						<div align="center"><br>
							<img src="images/2_pic_2.jpg" width="181" height="120" hspace="8" vspace="8">
						</div>
					</td>
					<td width="1" bgcolor="#a6bce0"><img src="images/dotblue.gif" width="1" height="1"></td>
					<td><table width="95%" border="0" align="center" cellpadding="2" cellspacing="2">
							<tr>
								<td colspan="3" class="copy10grey">&nbsp;</td>
							</tr>
							<tr>
								<td class="copy10grey" vAlign="top" colSpan="3" height="200">
									<div align="center" height="200"><asp:panel id="pnlForm" Runat="server"></asp:panel></div>
								</td>
							</tr>
							<tr>
								<td>
									<serv:MenuSP id="Menusss" runat="server"></serv:MenuSP>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<table align="center" width="95%" border="0" cellpadding="0" cellspacing="0">
				<tr>
					<td colspan="2"><foot:menuheader id="footer" runat="server"></foot:menuheader></td>
				</tr>
			</table>
		</form>
	</BODY>
</HTML>
