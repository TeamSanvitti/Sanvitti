<%@ Page language="c#"  %>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="./Controls/Header.ascx" %>
<%@ Register TagPrefix="foot" TagName="MenuHeader" Src="./Controls/Footer.ascx" %>
<%@ Register TagPrefix="serv" TagName="MenuSP" Src="./Controls/provider.ascx" %>
<HTML>
	<HEAD>
		<title>Accessories</title>
			<script language=javascript src="avI.js"></script>
		
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
									Accessories</td>
							</tr>
						</table>
						<div align="center"><br>
							<img src="images/5_pic_3.jpg" width="178" height="90" hspace="8" vspace="8">
						</div>
					</td>
					<td width="1" bgcolor="#a6bce0">
					<img alt="" src="images/dotblue.gif" width="1" height="1"></td>
					<td>
					<table width="95%" border="0" align="center" cellpadding="2" cellspacing="2">
							<tr>
								<td class="copy10grey">&nbsp;</td>
							</tr>
							<tr>
								<td class="copy10grey">
									<span class="copy10grey"><b>Lan Global inc.</b> sells most OEM accessories in bulk or OEM retail packaging</span>
								</td>
							</tr>	
							<tr>
							    <td align="center">
						        <table border="0" width="100%">
						            <tr><td><br /><br /></td></tr>
						            <tr>
						                <td align="center"><img alt="" src="./images/newsite/Headset.jpg"</td>
						                <td align="center"><img alt="" src="./images/newsite/Carcharger.jpg"</td>
						                <td align="center"><img alt="" src="./images/newsite/Headset1.jpg"</td>
						                <td align="center"><img alt="" src="./images/newsite/phoneclip.jpg"</td>
						                <td align="center"><img alt="" src="./images/newsite/PhoneHolder.jpg"</td>
						            </tr>
                            <tr><td><br /></td></tr>
						            
						        </table>
						        </td>
							</tr>
							<tr>
							    <td class="copy10grey"><p></p></td>
							</tr>	
							<!--<tr>
							    <td class="copy10grey">
                            <table width="95%" border="0" align="center" cellpadding="2" cellspacing="2">
							<tr>
								<td colspan="3" class="copy10grey">&nbsp;</td>
							</tr>-->
							<tr>
								<td height="200" colspan="3" class="copy10grey">
									<strong>Lan Global inc. </strong> manufactures a great line of quality aftermarket accessories for any handset in the market. <strong>Lan Global inc. </strong>along with our overseas production facilities 
									manufacture, assemble and test our products. This includes custom-made wireless 
									phone accessories for the following categories: <blockquote>
										<p>(1) Bluetooth Applications
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
												(8) Headsets 
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
									<p></p>
									<p></p>
									<br>
								</td>
							</tr>
							
						<!--</table>							    
							    </td>
							</tr>-->
							<tr><td>&nbsp;</td></tr>
							<tr><td>&nbsp;</td></tr>
					</table>
					</td>
				</tr>
				
			</table>
			<table align="center" width="95%" border="0" cellpadding="0" cellspacing="0">
				<tr>
					<td><foot:menuheader id="footer" runat="server"></foot:menuheader></td>
				</tr>
			</table>
		</form>
	</BODY>
</HTML>
