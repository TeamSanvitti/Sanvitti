<%@ Register TagPrefix="head" TagName="MenuHeader" Src="./Controls/Header.ascx" %>
<%@ Register TagPrefix="foot" TagName="MenuHeader" Src="./Controls/Footer.ascx" %>
<%@ Page language="c#"  %>
<HTML>
	<HEAD>
		<title>refurbishing</title>
		<link href="aerostyle.css" rel="stylesheet" type="text/css">
	</HEAD>
	<BODY bgColor="#ffffff" leftmargin="0" rightmargin="0" topmargin="0">
		<form id="Form1" method="post" runat="server">
			<table align="center" width="775" border="0" cellpadding="0" cellspacing="0">
				<tr>
					<td valign="top" align="left"><head:menuheader id="MenuHeader1" runat="server"></head:menuheader></td>
				</tr>
			</table>
			<table width="775" border="0" align="center" cellpadding="0" cellspacing="0">
				<tr>
					<td><img src="images/dotblue.gif" width="100%" height="1"></td>
				</tr>
			</table>
			<table width="775" border="0" align="center" cellpadding="0" cellspacing="0">
				<tr>
					<td width="200" valign="top" class="bgleft"><br>
						<table width="100%" border="0" cellpadding="0" cellspacing="0" class="copy10grey2">
							<tr>
								<td align="center" bgcolor="#dee7f6" class="button">
									refurbishing</td>
							</tr>
						</table>
						<div align="center"><br>
							<img src="images/repair.jpg" width="180" height="135" hspace="8" vspace="8">
						</div>
					</td>
					<td width="1" bgcolor="#a6bce0"><img src="images/dotblue.gif" width="1" height="1"></td>
					<td><table width="95%" border="0" align="center" cellpadding="2" cellspacing="2">
							<tr>
								<td colspan="3" class="copy10grey">&nbsp;</td>
							</tr>
							<tr>
								<td height="200" colspan="3" class="copy10grey"><p align="justify"><strong>AeroVoice</strong>
										monthly receives 20,000+ used handsets from different locations and has those 
										phones refurbished to like new condition by the manufacturers authorized 
										service center.</p>
									<p align="justify">We only use original equipment manufactures parts, housings and 
										accessories to complete our refurbished handsets. This assures you of the best 
										quality and performance on refurbished products from AeroVoice.
									</p>
									<p align="justify">Lan Global Refurbished products have a 90-day warranty</p>
									<p align="justify">If the handset cannot be refurbished then we provide them to our 
										customer base in the following conditions: AS-IS or PUTCGLCD (Power Up Test 
										Call Good LCD).</p>
									<p align="justify">AS-IS products have no warranty and PUTCGLCD have a 72 hours 
										guarantee for returning if they do not work.
									</p>
									<p align="justify">A credit will be applied to your next purchase for any product 
										returned using our RMA form.
									</p>
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
			<table align="center" width="775" border="0" cellpadding="0" cellspacing="0">
				<tr>
					<td colspan="2"><foot:menuheader id="footer" runat="server"></foot:menuheader></td>
				</tr>
			</table>
		</form>
	</BODY>
</HTML>
