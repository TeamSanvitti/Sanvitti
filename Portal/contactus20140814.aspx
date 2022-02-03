<%@ Page language="c#"  %>

<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="./Controls/Footer.ascx" %>

<%@ Register TagPrefix="head" TagName="MenuHeader" Src="./dhtmlxmenu/menuControl.ascx" %>

<HTML>
	<HEAD>
		<title>Contactus</title>
 <like rel="StyleSheet" href="/css/menu_2.css" ></like>
 
 <style type="text/css">

.auto-style34 {
	text-decoration: none;
}

.auto-style29 {
	text-decoration: none;
}	
.auto-style12 {
	color: #000000;
}
.auto-style1 {
	text-align: center;
	white-space: normal;
}

div.menuNormal
{display: none;
position: static;}

a.menuitem:link
{text-decoration: none;
color: black;
background-color: white;
display: block;}

.auto-style9 {
	text-align: center;
}

.auto-style10 {
	background-image: url('Images/Header_Contact.png');
}

.auto-style12 {
	color: #000000;
}
.auto-style22 {
	background-image: url('Images/footer.png');
	text-align: center;
}

.auto-style23 {
	background-color: #E9E9E9;
	font-family: Arial, Helvetica, sans-serif;
	text-align: center;
}

.auto-style6 {
	font-family: "Century Gothic";
	font-size: 15pt;
	text-align: center;
}
.auto-style7 {
	font-family: "Century Gothic";
	font-size: 15pt;
}

.auto-style13 {
	border-width: 0px;
}

.auto-style24 {
	text-align: justify;
	margin-top: 0;
	margin-bottom: 0;
	font-family: verdana, tahoma, arial, sans-serif;
	font-size: 10pt;
}

.auto-style32 {
	text-align: justify;
	margin-top: 0;
	margin-bottom: 0;
	font-family: "Century Gothic";
	font-size: x-large;
	color: #004071;
}

.auto-style35 {
	font-size: medium;
}
.auto-style36 {
	text-decoration: none;
}
.auto-style2 {
	text-align: left;
	font-family: "Century Gothic";
	font-size: large;
	color: #004071;
}
.auto-style3 {
	text-align: center;
	font-family: "Century Gothic";
	font-size: large;
	color: #004071;
}


</style>
	</HEAD>
	<BODY bgColor="#ffffff" leftmargin="0" rightmargin="0" topmargin="0">
		<form id="Form1" method="post" runat="server">

        
<table class="auto-style10" style="width: 1200px; " align="center">
	<tr>
		<td class="auto-style1" style="height: 135px" valign="top">
		<a href="index.aspx">
		<img alt="" height="103" src="Images/Aerovoice_logo.png" style="float: left" width="295" class="auto-style13" /></a></td>
	</tr>
				<tr>
		<td style="width: 835px; height: 54px">
        
  <table class="navbar" width="800">
    <tr>
      <td class="menuNormal" style="text-align: center">
            <head:menuheader id="MenuHeader1" runat="server"></head:menuheader>  
            </td>
            </tr>
            </table>


            
	</td>
	</tr>
	</table>



<table align="center" cellspacing="0" style="width: 1200px">
	<tr>
		<td style="height: 19px"></td>
		<td style="height: 19px">&nbsp;
		</td>
	</tr>
	<tr>
		<td class="auto-style23" style="width: 300px" valign="top">
		<img src="Images/fulfillment.jpg" style="WIDTH: 230px; HEIGHT: 160px" /><br />
		<br />
		<br />

		<span class="auto-style35">
		<!--
		<strong>Sales <br />
		</strong>Wholesale Distribution <br />
		Jesse Koehr <br />
		<//-->
        <br /> <strong>Accessory Sales <br />
        	</strong>Joanne Hsieh <br />
		<br />
		<strong>MVNO <br />
		</strong> Roger Nunez, Director MVNO
		<br />
		<br />
		<strong>RSA <br />
		</strong>Steve Jarrett, VP Carrier Sales <br /><br />
                
		</td>
        <td style="width: 10px">&nbsp;
        
        </td>
		<td valign="top">
		<p class="auto-style32"><strong>Contact Us</strong></p>
		<p class="auto-style32"><br />
		<strong><span class="auto-style35">
		<a class="auto-style36" href="http://www.Aerovoice.com">
		<span class="auto-style12">www.Aerovoice.com</span></a></span></strong></p>
		<p class="auto-style24"><span class="auto-style35"><em>General inquiries :</em> info@aerovoice.com <br />
		<em>Accounting Matters :</em> accounting@aerovoice.com <br />
		<em>Customer Support :</em> customersupport@aerovoice.com <br />
		<em>Sales inquiries :</em> <a href="mailto:sales@aerovoice.com">
		sales@aerovoice.com</a> <br />
		</span> </p>
		<p class="auto-style24">

<table cellspacing="3" cellpadding="3" style="width: 500px">
	<tr>
		<td style="width: 100px" class="auto-style2" valign="top"><strong>First Name:</strong></td>
		<td  style="width: 200px">
		
			<input name="Name" type="text" style="background-image:url('Images/box_bg.png'); height: 25px; width: 250px; border:0px; text-align:center; font-size:medium;" />
		</td>
	</tr>
	<tr>
		<td style="width: 100px" class="auto-style2" valign="top"><strong>Last Name:</strong></td>
		<td style="width: 200px">
		
			<input name="Last Name" type="text" style="background-image:url('Images/box_bg.png'); height: 25px; width: 250px; border:0px; text-align:center; font-size:medium;" />
		</td>
	</tr>
	<tr>
		<td style="width: 100px" class="auto-style2" valign="top"><strong>Email:</strong></td>
		<td style="width: 200px">
		
			<input name="Email" type="text" style="background-image:url('Images/box_bg.png'); height: 25px; width: 250px; border:0px; text-align:center; font-size:medium;" />
		</td>
	</tr>
	<tr>
		<td style="width: 100px" class="auto-style2" valign="top"><strong>Phone:</strong></td>
		<td style="width: 200px">
		
			<input name="Phone" type="text" style="background-image:url('Images/box_bg_150.png'); height: 25px; width: 150px; border:0px; text-align:center; font-size:medium;" />
		</td>
	</tr>
	<tr>
		<td style="width: 100px" class="auto-style2" valign="top"><strong>Address:</strong></td>
		<td style="width: 200px">
		
			<input name="Address" type="text" style="background-image:url('Images/ms_box_bg.png'); width: 250px; height: 50px; text-align:center; border:0px; font-size:medium;"/>
		</td>
	</tr>
	<tr>
		<td style="width: 100px" class="auto-style2" valign="top"><strong>Message</strong></td>
		<td style="width: 200px">
		
			<textarea cols="30" name="Message" rows="8" style="background-image:url('Images/ms_box_bg_150.png'); width:250px; height:150px; border:0px; text-align:justify; font-size:medium;" ></textarea>
		</td>
	</tr>
	<tr>
		<td class="auto-style3" valign="top" colspan="2">
		
			<input name="Submit1" type="submit" value="submit" style="background-image:url('Images/button_bg.png'); width:72px; height:32px; font-family:'Century Gothic'; font-weight:bold; font-size:medium; color:white; text-align:center; border:0px" />
		</td>
	</tr>
</table>

		<br />
		</p>
		</td>
	</tr>
</table>

    
<foot:MenuFooter id="footer1" runat="server" />
<p>&nbsp;</p>
			<%--
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
									contact us</td>
							</tr>
						</table>
						<div align="center"><br>
							<img src="images/5_pic_3.jpg" width="178" height="90" hspace="8" vspace="8">
						</div>
					</td>
					<td width="1" bgcolor="#a6bce0"><img src="images/dotblue.gif" width="1" height="1"></td>
					<td><table width="95%" border="0" align="center" cellpadding="2" cellspacing="2">
							<tr>
								<td colspan="3" class="copy10grey">&nbsp;</td>
							</tr>
							<tr>
								<td height="200" colspan="3" class="copy10grey">
									<span class="copy12hd"><strong>Corporate Headquarters</strong></span><span class="copy11"><strong>
										</strong></span>
									<br>
									<br>
									Aerovoice.com<br>
									2265 E. El Segundo Blvd.<br />
                                    El Segundo, CA 90245<br>
									<br>
									<strong>Tel:</strong> 866-446-CELL<br>
									<strong>Tel: </strong>310-647-9999<br>
									<strong>Fax: </strong>310-647-1677<strong><br>
									</strong>
									<br>
									<br>
									<strong>General inquiries</strong>&nbsp;:&nbsp;&nbsp;<strong><a href="mailto:info@aerovoice.com" class="copy11">info@aerovoice.com</a>
									</strong>
									<br>
									<strong>Accounting Matters</strong>&nbsp;:&nbsp;<a href="mailto:accounting@aerovoice.com" class="copy11"><strong>accounting@aerovoice.com</strong>
									</a>
									<br>
									<strong>Customer Support</strong> : <a href="mailto:customersupport@aerovoice.com" class="copy11">
										<strong>customersupport@aerovoice.com</strong> </a>
									<br>
									<strong>Sales inquiries</strong> : <a href="mailto:sales@aerovoice.com" class="copy11">
										<strong>sales@aerovoice.com</strong>&nbsp;</a><br>
									<br>
									<br>
									<a href="#"></a>
								</td>
							</tr>
							<tr><td>&nbsp;</td></tr>
							<tr>							    
							    <td class="SubTitle" colspan="2">
                                    Sales</td>							    
							</tr>
							<tr>
                                <td align="right"><img src="images/arrow_2.jpg" width="8" height="7" vspace="0"></td>
                                <td class="copy10grey">Wholesale Distribution</td>
                            </tr>
                            <tr>
                                <td></td>
                                <td class="copy10grey">
                                     <a href="mailto:jcauton@aerovoice.com" class="copy11">James Cauton, Sales Manager</a>
                                </td>
                            </tr>
                            <tr><td>&nbsp;</td></tr>
							<tr>
                                <td align="right"><img src="images/arrow_2.jpg" width="8" height="7" vspace="0"></td>
                                <td class="copy10grey">MVNO</td>
                            </tr>
                            <tr>
                                <td></td>
                                <td class="copy10grey">
                                     <a href="mailto:cparr@aerovoice.com" class="copy11">Cheri Parr, Director MVNO</a><br />
                                     <a href="mailto:bwright@aerovoice.com" class="copy11">Barry Wright, Director MVNO</a>
                                </td>
                            </tr>                            
                            <tr><td>&nbsp;</td></tr>
							<tr>
                                <td align="right"><img src="images/arrow_2.jpg" width="8" height="7" vspace="0"></td>
                                <td class="copy10grey">RSA</td>
                            </tr>
                            <tr>
                                <td></td>
                                <td class="copy10grey">
                                     <a href="mailto:sjarrett@aerovoice.com" class="copy11">Steve Jarrett, VP Carrier Sales</a>
                                </td>
                            </tr>
                            <tr><td>&nbsp;</td></tr>
							<tr>
                                <td align="right"><img src="images/arrow_2.jpg" width="8" height="7" vspace="0"></td>
                                <td class="copy10grey">Accessories</td>
                            </tr>
                            <tr>
                                <td></td>
                                <td class="copy10grey">
                                     <a href="mailto:jhsieh@aerovoice.com" class="copy11">Joanne Hsieh, Manufacturing Manager</a>
                                </td>
                            </tr> 
                            <tr><td>&nbsp;</td></tr>
					       <!-- <tr>							    
					            <td class="SubTitle" colspan="2">Marketing</td>							    
					        </tr>
					         <tr>
                                <td align="right"><img src="images/arrow_2.jpg" width="8" height="7" vspace="0"></td>
                                <td class="copy10grey">Corporate Marketing, Advertising and Events</td>
                            </tr>
                           <tr>
                                <td></td>
                                <td class="copy10grey">
                                     <a href="mailto:smcwhirter@aerovoice.com" class="copy11">Shelby McWhirter, VP Operations</a>
                                </td>
                            </tr>-->
                            <tr><td>&nbsp;</td></tr>                      
						</table>
					</td>
				</tr>
			</table>
			<table align="center" width="95%" border="0" cellpadding="0" cellspacing="0">
				<tr>
					<td colspan="2"><foot:menuheader id="footer" runat="server"></foot:menuheader></td>
				</tr>
			</table>--%>
		</form>
	</BODY>
</HTML>
