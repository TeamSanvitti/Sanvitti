
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="~/dhtmlxmenu/menuControl.ascx" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>


<%@ Page language="c#"  %>
<html>
	<head>
		<title>.:: LAN Gloal Inc. - Services ::.</title>
        <link rel="stylesheet" href="aerostyle.css" />
<link rel="stylesheet" href="CSS/style.css" type="text/css" media="screen" />
  
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<style type="text/css">
<!--
.text-11 {	font-size: 35px;
	color: #939598;
	font-family:Arial;
	text-align:left;
	margin-left: 30px;
}
.text-12 {font-size: 35px;
	color: #939598;
	font-family:Arial;
	text-align:left;
	margin-left: 30px;
}
.text-31 {	font-size: 18px;
	color: #ffffff;
	font-family:Arial;
	text-align:justify;
	margin-left: 50px;
	margin-right: 50px;
}
.text-7 {	font-size: 18px;
	color:#000;
	font-family:Arial;
	text-align:justify;
	margin-left: 30px;
	margin-right: 30px;
}
-->
</style>
		
	</head>
	<body >
		<form id="Form1" method="post" runat="server">
        
<table width="100%" border="0" cellspacing="0" cellpadding="0">
  <tr>
    <td align="center">
    <table border="0" align="center">
      <tr>
        <td align="right" width="300"><a href="/index.aspx"><img src="images/logo.JPG" width="295" height="103"></a></td>
        <td align="left" width="1000">
        <table border="0" cellpadding="0" cellspacing="0"  width="100%">
            <tr>
              <td valign="top" width="100%">
      
                    <head:MenuHeader ID="header1" runat="server" />

            </td>
                    </tr>
                </table></td>
              </tr>
            </table></td>
          </tr>
        </table>


<table width="100%" border="0" cellspacing="0" cellpadding="0">
  <tr>
    <td bgcolor="#CCCCCC"><table width="1300" border="0" align="center" cellpadding="0" cellspacing="0">
      <tr>
        <td rowspan="2" align="left" valign="middle" bgcolor="#007fc1"><table width="100%" border="0" cellspacing="8" cellpadding="8">
          <tr>
            <td class="whitetx1"> LAN Gloal offers end to end supply chain management and fulfillment models for all levels and stages of customer requirements. Our current services include:</td>
          </tr>
        </table></td>
        <td height="35" align="center" valign="middle" bgcolor="#999999"><span class="whitehd">ASSEMBLY</span></td>
      </tr>
      <tr>
        <td width="300" valign="top"><img src="images/fulfillment.jpg" width="300" height="172"></td>
      </tr>
    </table></td>
  </tr>
</table>
<table width="1300" border="0" align="center" cellpadding="0" cellspacing="0">
  <tr>
    <td align="left"><table width="100%" border="0" cellspacing="8" cellpadding="8">
      <tr>
        <td class="bodytext1"><table width="100%" border="0" cellspacing="3" cellpadding="3">
            <tr>
              <td width="5%" align="left"><img src="images/bullet.png" width="19" height="19"></td>
              <td width="95%" align="left" class="bodytext1"><strong>Wireless Fulfillment</strong><strong>
                </strong></td>
              </tr>
            <tr>
              <td align="left"><img src="images/bullet.png" width="19" height="19"></td>
              <td align="left" class="bodytext1"><strong>Supply Chain Management </strong></td>
            </tr>
            <tr>
              <td align="left"><img src="images/bullet.png" width="19" height="19"></td>
              <td align="left" class="bodytext1"><strong>Inventory Management </strong></td>
            </tr>
            <tr>
              <td align="left"><img src="images/bullet.png" width="19" height="19"></td>
              <td align="left" class="bodytext1"><strong>Custom Packaging </strong></td>
            </tr>
            <tr>
              <td align="left"><img src="images/bullet.png" width="19" height="19"></td>
              <td align="left" class="bodytext1"><strong>Reverse Logistics </strong></td>
            </tr>
            <tr>
              <td align="left"><img src="images/bullet.png" width="19" height="19"></td>
              <td align="left" class="bodytext1"><strong>Software Programming</strong></td>
            </tr>
          </table>
          <p><br>
          </p>
          </td>
      </tr>
    </table></td>
    <td width="300" valign="top" bgcolor="#E5E5E5">&nbsp;</td>
  </tr>
</table>

<%--<table width="100%" border="0" align="center">
  <tr>
    <td colspan="2" bgcolor="#FF6600">&nbsp;</td>
  </tr>
</table>
--%>

    <foot:MenuFooter id="footer1" runat="server" />

		</form>
	</body>
</html>
