<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomerUserQuery.aspx.cs" Inherits="avii.CustomerUserQuery" %>
<%@ Register TagPrefix="menu" TagName="Menu" Src="~/Controls/Header.ascx" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>

<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>User Query</title>
     <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>

    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.4.0/jquery.min.js"></script>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/jquery-ui.min.js"></script>
    <link href="https://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/themes/start/jquery-ui.css"
    rel="stylesheet" type="text/css" />
<%--<script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/jquery-ui.js" type="text/javascript"></script>
<link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/themes/start/jquery-ui.css"
    rel="stylesheet" type="text/css" />--%>
	<%--<script type="text/javascript" src="../JQuery/jquery.min.js"></script>
    <script type="text/javascript" src="../JQuery/jquery-ui.min.js"></script>--%>
	<script type="text/javascript" src="../JQuery/jquery.blockUI.js"></script>
	
 <link href="../wsastyle.css" type="text/css" rel="stylesheet"/>
    <%--<link href="../Styles.css" type="text/css" rel="stylesheet" />--%>
    <link href="../dhtmlwindow.css" type="text/css" rel="stylesheet" />
	<link rel="stylesheet" href="../dhtmlxwindow/style.css" type="text/css" media="screen" />
    <link rel="stylesheet" type="text/css" href="../dhtmlxwindow/dhtmlxwindows.css"/>
	<link rel="stylesheet" type="text/css" href="../dhtmlxwindow/skins/dhtmlxwindows_dhx_skyblue.css"/>
	
	<script src="../dhtmlxwindow/dhtmlxcommon.js"></script>
	<script src="../dhtmlxwindow/dhtmlxwindows.js"></script>
	
	
	<script src="../dhtmlxwindow/dhtmlxcontainer.js"></script>
    <script type="text/javascript" language="javascript">
        var dhxWins, w1;
        function openpopup(userid) {
            //alert(userid);
            dhxWins = new dhtmlXWindows();
            dhxWins.enableAutoViewport(false);
            dhxWins.attachViewportTo("winVP");
            dhxWins.setImagePath("../../codebase/imgs/");
            w1 = dhxWins.createWindow("w1", 320, 125, 350, 250);

            w1.setText("Roles");
            //alert(userid);
            w1.attachURL("usersrole.aspx?userid=" + userid);
        }
	</script>
</head>
<body bgcolor="#ffffff" leftmargin="0" rightmargin="0" topmargin="0">
    <form id="form1" runat="server">
    <div id="winVP" style="z-index:1">
    <table cellspacing="0" cellpadding="0" border="0" align="center" width="100%">
    <tr>
        <td>
            
                <menu:menu ID="menu1" runat="server" ></menu:menu>
        </td>
     </tr>
     <tr>
     
     <td>
     
        <table align="center" style="text-align:left" width="85%">
               <tr ><td> &nbsp;</td></tr>
              <tr class="buttonlabel"><td class="style19">&nbsp;User Query</td></tr>
              <tr><td colspan=4>
            <asp:Label ID="lbl_message" runat="server" style="text-align: left" 
                    CssClass="errormessage"></asp:Label>
     </td></tr>
               <tr><td> 
                   <asp:Panel ID="pnlUser" DefaultButton="btn_searchuser" runat="server">
                   
        <table  align="center" bordercolor="#839abf" border="1" cellpadding="0" cellspacing="0" 
                        width= "100%">
        <tr><td >
        
    <table width="100%" align="center" cellpadding="5" cellspacing="5" >
    <tr><td class="copy10grey" width="15%">User:</td><td width="35%">
            <asp:TextBox ID="txt_user" runat="server" Width="80%" CssClass="copy10grey"></asp:TextBox>
        </td>
        <td class="copy10grey"  width="15%">Role:</td>
        <td  width="35%">
        <asp:DropDownList ID="ddlRole" runat="server" Width="80%" 
                CssClass="copy10grey">
            </asp:DropDownList>
        
        </td>
        </tr>
        
           
            <tr>
                <td colspan="4">
                    <hr  />    
                </td>
            </tr>
            
                <tr>
        <td colspan="4" align="center">
            <asp:Button ID="btn_searchuser" runat="server" Text="User Search" cssClass="buybt" 
                onclick="Button1_Click"/>
            <asp:Button ID="btn_cancel" runat="server" CssClass="buybt" 
            onclick="btn_cancel_Click" Text="Cancel" />
        
        </td>
        </tr>
            </table>
   
              
                
    </td></tr>
    
    
    </table>
            </asp:Panel>         
    <table align="center" width="100%" border="0" cellpadding="0" cellspacing="0">
    <tr>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td>
        
        
            <asp:GridView ID="GV_User" PageSize="20" AllowPaging="true" runat="server" 
                Width="100%" CssClass="copy10grey" AutoGenerateColumns="False"  OnPageIndexChanging="GV_User_PageIndexChanging"
                >
                <PagerStyle ForeColor="Black" />
                <Columns>
                    <asp:TemplateField HeaderText="User name" HeaderStyle-CssClass="buttongrid" HeaderStyle-HorizontalAlign="Left" >
                        <ItemTemplate>
                            
                            
                            <asp:Label ID="lblUsername" CssClass="copy10grey" runat="server" Text='<%#Eval("username")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                   <%-- <asp:TemplateField HeaderText="Password" HeaderStyle-CssClass="button" HeaderStyle-HorizontalAlign="Left" >
                        <ItemTemplate>
                            <asp:Label ID="lblPassord" CssClass="copy10grey" runat="server" Text='<%#Eval("password")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                   --%> 
                    <asp:TemplateField HeaderText="Email" HeaderStyle-CssClass="buttongrid" HeaderStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            
                            <asp:Label ID="lblEmail" CssClass="copy10grey" runat="server" Text='<%#Eval("email")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Company" HeaderStyle-CssClass="buttongrid" HeaderStyle-HorizontalAlign="Left" >
                        <ItemTemplate>
                            
                            <asp:Label ID="lblCompany" CssClass="copy10grey" runat="server" Text='<%#Eval("companyname")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Co. A/C #" HeaderStyle-CssClass="buttongrid" HeaderStyle-HorizontalAlign="Left" >
                        <ItemTemplate>
                            <asp:Label ID="lblACStatus" CssClass="copy10grey" runat="server" Text='<%#Eval("companyAccno")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="A/C Status" HeaderStyle-CssClass="buttongrid" HeaderStyle-HorizontalAlign="Left" >
                        <ItemTemplate>
                            
                            <asp:Label ID="lblType" CssClass="copy10grey" runat="server" Text='<%#Eval("AccStatus")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                   
                    <asp:TemplateField HeaderText="Role" HeaderStyle-CssClass="buttongrid" HeaderStyle-HorizontalAlign="Left" >
                        <ItemTemplate>
                            <img src="../images/question-list.gif" alt="Role List"  onclick="openpopup(<%# Eval("userID") %>);"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="" ItemStyle-CssClass="copy10grey" HeaderStyle-CssClass="buttongrid"  HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="70">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkEdit" CssClass="copy10grey" OnCommand="Edit_click" CommandArgument='<%# Eval("userID") %>'  runat="server">Edit</asp:LinkButton>
                            &nbsp;
                            <asp:LinkButton ID="lnkDelete" CssClass="copy10grey" OnCommand="Delete_click" CommandArgument='<%# Eval("userID") %>' OnClientClick="return confirm('Delete this User?');"  runat="server">Delete</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </td>
    </tr>
       
    </table>
    </td>
    </tr>
    </table>
        
    </td>
    </tr>
    
    
        
        <tr><td>&nbsp;</td></tr>
          <tr><td>&nbsp;</td></tr>
          <tr>
            <td><foot:MenuFooter id="Footer1" runat="server"></foot:MenuFooter></td>
          </tr>
    </table>
    </div>
    </form>
</body>
</html>

