<%@ Page language="c#"  %>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="./Controls/Header.ascx" %>
<%@ Register TagPrefix="foot" TagName="MenuHeader" Src="./Controls/Footer.ascx" %>
<%@ Register TagPrefix="serv" TagName="MenuSP" Src="./Controls/provider.ascx" %>
<HTML>
	<HEAD>
		<title>About Us</title>
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
									ABOUT US</td>
							</tr>
						</table>
						<div align="center"><br>
							<img src="images/3_pic_2.jpg" width="182" height="131" border="0">
						</div>
					</td>
					<td width="1" bgcolor="#a6bce0"><img src="images/dotblue.gif" width="1" height="1"></td>
					<td><table width="95%" border="0" align="center" cellpadding="2" cellspacing="2">
							<tr>
								<td colspan="3" class="copy10grey">&nbsp;</td>
							</tr>
							<tr>
								<td colspan="3" class="copy10grey"><div align="justify"><strong>Lan Global inc. </strong>is a 
										value added fulfillment center and wholesale distributor of wireless 
										communication products. The Company offers wireless phones and accessories from 
										leading manufacturers, featuring brand names such as Audiovox, LG Infocomm, and 
										Samsung.<br>
										<br>
										Lan Global inc. has developed a customer base of RSA and RCA carriers, network 
										operator resellers, agent dealers, and national retailers. With twenty years of 
										wireless management experience in the wholesale distribution of wireless 
										products, Lan Global inc. established in August 2000, is poised for growth and 
										success.<br>
										<br>
										Our objective is to capitalize on wireless communications opportunities in 
										markets with significant growth. The Company’s business strategy is to:<br>
										<br>
										<strong>a. </strong>Capitalize on its strategic alignments with key 
										manufacturers to provide value-added services
										<br>
										<br>
										<strong>b.</strong> Transition to a service-based company offering a broad 
										selection of fulfillment options. These services include ‘just in time’ 
										inventory capacity for its customer base.<br>
										<br>
										With direct access to products manufactured by the leading manufacturers, our 
										customers can rely on aggressive pricing and quality service to ensure their 
										profitability.
										<br>
										<br>
									</div>
								</td>
							</tr>
							<tr>
								<td colspan="3">
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
