<%@ Register TagPrefix="foot" TagName="MenuHeader" Src="./Controls/Footer.ascx" %>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="./Controls/Header.ascx" %>
<%@ Page language="c#"  %>
<HTML>
	<HEAD>
		<title>Manufacturing</title>
		<link href="aerostyle.css" rel="stylesheet" type="text/css">
			<script language=javascript src="avI.js"></script>
		
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
									manufacturing</td>
							</tr>
						</table>
						<div align="center"><br>
							<img src="images/manufaturing.jpg" width="180" height="135" hspace="8" vspace="8">
						</div>
					</td>
					<td width="1" bgcolor="#a6bce0"><img src="images/dotblue.gif" width="1" height="1"></td>
					<td><table width="95%" border="0" align="center" cellpadding="2" cellspacing="2">
							<tr>
								<td colspan="3" class="copy10grey">&nbsp;</td>
							</tr>
							<tr>
								<td height="200" colspan="3" class="copy10grey">
									<strong>Lan Global inc. </strong>along with our overseas production facilities 
									manufacture, assemble and test our products. This includes custom-made wireless 
									phone accessories for the following categories: <blockquote>
										<p><strong>(1) Bluetooth Applications
												<br>
												(2) Premium Leather Cases
												<br>
												(3) Innovative Phone Cases
												<br>
												(4) Car Chargers<br>
												(5) Travel Chargers
												<br>
												(6) Batteries<br>
												(7) Holsters and Belt Clips<br>
												(8) Headsets </strong>
										</p>
									</blockquote>
									<p>We determine product specifications and production practices based on customer 
										requirements and ensure that they go beyond this. We pride ourselves in meeting <strong>
											OEM standards</strong> and <strong>consumer expectations.</strong> <strong>Regular 
											Q.A. (Quality assurance)</strong> inspections occur in all factories 
										producing components, materials and finished goods.
									</p>
									<p class="copy12hd"><strong>Our accessories are covered by:</strong></p>
									<p>(1) Lifetime warranty
										<br>
										(2) 1-800 Customer Support Availability</p>
									<p>Please click on the following link for more information on our accessory 
										business.</p>
									<p><a href="http://www.allurewireless.com" target="_blank" class="copy10">www.allurewireless.com</a></p>
									<p></p>
									<p></p>
									<br>
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
