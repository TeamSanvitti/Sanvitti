<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="menu.aspx.cs" Inherits="avii.Admin.menu" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="../dhtmlxmenu/dhtmlxmenu_dhx_skyblue.css">
<script type="text/javascript" src="../dhtmlxmenu/dhtmlxcommon.js"></script>
<script  type="text/javascript" src="../dhtmlxmenu/dhtmlxmenu.js"></script>
<script type="text/javascript" src="../dhtmlxmenu/dhtmlxmenu_ext.js"></script>

</head>
<body >
    <form id="form1" runat="server">
  
    <div id="menuObj"></div>
<script type="text/javascript">
    var menu = new dhtmlXMenuObject("menuObj");
   // menu.setIconsPath("../common/imgs/");
    // initing;
    menu.addNewSibling(null, "file", "File", false);
    menu.addNewChild("file", 0, "new", "New", false);
    menu.addNewSeparator("new");
    menu.addNewChild("file", 2, "open", "Open", false);
    menu.addNewChild("file", 3, "save", "Save", false);
    menu.addNewChild("file", 4, "saveAs", "Save As...", true, null);
    menu.addNewSeparator("saveAs");
    menu.addNewChild("file", 6, "print", "Print", false);
    menu.addNewChild("file", 7, "pageSetup", "Page Setup", true, null);
    menu.addNewSeparator("pageSetup");
    menu.addNewChild("file", 12, "close", "Close", false);
    menu.addNewSibling("file", "edit", "Edit", false);
    menu.addNewChild("edit", 0, "edit_undo", "Undo", false);
    menu.addNewSibling("edit_undo", "edit_redo", "Redo", false);
    menu.addNewSeparator("edit_redo", "sep_1");
    menu.addNewSibling("sep_1", "edit_select_all", "Select All", false);
    menu.addNewSeparator("edit_select_all", "sep_2");
    menu.addNewSibling("sep_2", "edit_cut", "Cut", false);
    menu.addNewSibling("edit_cut", "edit_copy", "Copy", false);
    menu.addNewSibling("edit_copy", "edit_paste", "Paste", false);
    menu.addNewSibling("edit", "help", "Help", false);
    menu.addNewChild("help", 0, "about", "About...", false);
    menu.addNewChild("help", 1, "help2", "Help", false);
    menu.addNewChild("help", 2, "bugrep", "Bug Reporting", false);
    menu.addNewChild("help", 3, "easy hai", "Easy", false);
    menu.addNewSibling("help", "new", "New", false);
    menu.addNewChild("new", 0, "new1", "New1", false);
    menu.setHref("new1", "index.aspx", "_self");
    menu.hideItem("new");
    menu.showItem("new");
    //menu.enableDynamicLoading("index.aspx");
   
</script>
 <%-- <a target=--%>
    </form>
</body>
</html>
