<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NewMenu.ascx.cs" Inherits="avii.dhtmlxmenu.NewMenu" %>

    <link rel="stylesheet" type="text/css" href="../dhtmlxmenu/dhtmlxmenu_dhx_skyblue1.css"/>
    <script type="text/javascript" src="../dhtmlxmenu/dhtmlxcommon.js"></script>
    <script  type="text/javascript" src="../dhtmlxmenu/dhtmlxmenu.js"></script>
    <script type="text/javascript" src="../dhtmlxmenu/dhtmlxmenu_ext.js"></script>
    
<style type="text/css">
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
	background-image: url('/Images/Header_Profile.png');
}

.auto-style12 {
	color: #000000;
}
.auto-style21 {
	text-align: right;
}
.auto-style22 {
	background-image: url('/Images/footer.png');
}

.auto-style18 {
	color: #004071;
	font-family: "Century Gothic";
	font-size: x-large;
}
.auto-style16 {
	font-family: Arial, Helvetica, sans-serif;
}
.auto-style17 {
	margin: 3px 5px;
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

.auto-style25 {
	font-size: medium;
}
.auto-style26 {
	text-align: justify;
}
.auto-style27 {
	font-size: large;
}

.auto-style29 {
	text-decoration: none;
}

</style>
    <body onload="initMenu();">
    		
<table class="auto-style10" style="width: 1200px; " align="center">
	<tr>
		<td class="auto-style1" style="height: 135px" valign="top">
		<a href="/index.aspx">
		<img alt="" height="103" src="/Images/Aerovoice_logo.png" style="float: left" width="295" class="auto-style13" /></a></td>
	</tr>
				<tr>
		<td style="width: 800px; height: 54px">


        
  <table class="navbar" width="800">
    <tr>
      <td class="menuNormal"  style="text-align: center">
    
    <table width="100%" >
    <tr>
        <td width="100%" align="right">  <div id="menuObj" align="right"> </div> </td>
    </tr>
    </table>
    <asp:HiddenField ID="hdnurl" runat="server" />
  <asp:HiddenField ID="hdnmenu" runat="server" />
    </td>

      
    </tr>
  </table>

</td>
	</tr>
	</table>

    <script type="text/javascript">

        var menu;
        function initMenu() {

            menu = new dhtmlXMenuObject("menuObj");

            var menu_content = document.getElementById("<%=hdnmenu.ClientID %>").value;

            menu.loadXMLString(menu_content);

        }
     
    </script>

   </body>