<%@ Page language="c#" AutoEventWireup="true"%>
<%@ Register TagPrefix="foot" TagName="MenuHeader" Src="./Controls/Footer.ascx" %>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="./Controls/Header.ascx" %>
<HTML>
	<HEAD>
		<title>Careers</title>
			<script language=javascript src="avI.js"></script>
		<link href="aerostyle.css" rel="stylesheet" type="text/css">
		
	</HEAD>
	<BODY bgColor="#ffffff" leftmargin="0" rightmargin="0" topmargin="0">
		<form id="Form1" method="post" runat="server">
			<table align="center" width="95%" border="0" cellpadding="0" cellspacing="0">
				<tr>
					<td valign="top" align="left"><head:menuheader id="MenuHeader1" runat="server"></head:menuheader></td>
				</tr>
				<tr>
					<td valign="top">
						<table width="95%" border="0" align="center" cellpadding="0" cellspacing="0">
							<tr>
								<td width="200" valign="top" class="bgleft"><br>
									<table width="100%" border="0" cellpadding="0" cellspacing="0" class="copy10grey2">
										<tr>
											<td align="center" bgcolor="#dee7f6" class="button">
												Careers</td>
										</tr>
									</table>
									<div align="center"><br>
										<img src="images/5_pic_3.jpg"  border="0">
									</div>
								</td>
								<td width="1" bgcolor="#a6bce0"><img src="images/dotblue.gif" width="1" height="1"></td>
								<td>
								    <table width="95%" border="0" align="center" cellpadding="2" cellspacing="2">
										<tr>
											<td class="copy10grey">&nbsp;</td>
										</tr>
										<tr>
											<td class="subtitle" align="center">
											        <table width="100%" border="0">
											            <tr><td>&nbsp;<br /></td></tr>
											            <tr>
											                <td class="subtitle" style="height: 18px" align="center"><u>
                                                                
                                                                No career opportunity is available at this time.</u></td>
											            </tr>
											        </table>
											        
											        <br />

											</td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colspan="2"><foot:menuheader id="footer" runat="server"></foot:menuheader></td>
				</tr>
			</table>
		</form>
	</BODY>
</HTML>
