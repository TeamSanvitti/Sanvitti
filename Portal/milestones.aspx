<%@ Page language="c#"  %>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="./Controls/Header.ascx" %>
<%@ Register TagPrefix="foot" TagName="MenuHeader" Src="./Controls/Footer.ascx" %>
<HTML>
	<HEAD>
		<title>Team</title>
		<link href="aerostyle.css" rel="stylesheet" type="text/css">
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
									milestones</td>
							</tr>
						</table>
						<div align="center"><br>
							<img src="images/3_pic_2.jpg" width="182" height="131" hspace="0" vspace="0" class="border">
						</div>
					</td>
					<td width="1" bgcolor="#a6bce0"><img src="images/dotblue.gif" width="1" height="1"></td>
					<td><table width="95%" border="0" align="center" cellpadding="2" cellspacing="2">
							<tr>
								<td colspan="3" class="copy10grey">&nbsp;</td>
							</tr>
							<tr>
								<td colspan="3" class="copy10grey"><div align="justify">
										<p align="center" class="copy12hd"><strong>NO DATA AVAILABLE</strong></p>
										<br>
										<br>
										<br>
										<br>
										<br>
										<br>
									</div>
								</td>
							</tr>
							<tr>
								<td><div align="center"><img src="images/samlogo.jpg" width="85" height="38"></div>
								</td>
								<td><div align="center"><img src="images/ultralogo.jpg" width="197" height="37"></div>
								</td>
								<td><div align="center"><img src="images/lglogo.jpg" width="72" height="38"></div>
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
