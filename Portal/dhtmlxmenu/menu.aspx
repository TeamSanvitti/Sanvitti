<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="menu.aspx.cs" Inherits="avii.dhtmlxmenu.menu" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>  
</head>
<body onload="initMenu();">
    <link rel="stylesheet" type="text/css" href="../dhtmlxmenu/dhtmlxmenu_dhx_skyblue.css">
    
    <script type="text/javascript" src="../dhtmlxmenu/dhtmlxcommon.js"></script>
    <script  type="text/javascript" src="../dhtmlxmenu/dhtmlxmenu.js"></script>
    <script type="text/javascript" src="../dhtmlxmenu/dhtmlxmenu_ext.js"></script>
    <div id="menuObj">
    
    </div>
   <form runat="server">
   <asp:HiddenField ID="hdnmenu" runat="server" /></form>
    <script type="text/javascript">

         var menu;
         function initMenu() 
         {
             menu = new dhtmlXMenuObject("menuObj");
          
             var menu_content = document.getElementById("hdnmenu").value;
         
            menu.loadXMLString(menu_content);
          
         }
    </script>
</body>
</html>
