<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rolequery.aspx.cs" Inherits="avii.AccessManagement.rolequery" ValidateRequest="false" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>
<%@ Register TagPrefix="menu" TagName="Menu" Src="~/Controls/Header.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Roles - Search</title>
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
	
     <link href="../aerostyle.css" type="text/css" rel="stylesheet"/>
     <%--<link href="../Styles.css" type="text/css" rel="stylesheet" />--%>
    
</head>
<body bgcolor="#ffffff" leftmargin="0" rightmargin="0" topmargin="0" style="text-align: left">
 <form id="frmrolequer" runat="server">
    <table cellspacing="0" cellpadding="0" border="0" width="100%">
    <tr>
        <td>
             <menu:Menu ID="menu1" runat="server" ></menu:Menu>
        </td>
     </tr>
     <tr>
     
     <td>
     <table  width="95%" align="center" >
            <tr><td  >&nbsp;</td></tr>
            <tr><td class="button" align="left" >&nbsp;Roles - Search</td></tr>
            <tr><td>
                    <asp:Label ID="lblMsg" runat="server" CssClass="errormessage"></asp:Label>
                </td>
            </tr>
            
               <tr><td> 
        <table  align="center" bordercolor="#839abf" border="1" cellpadding="0" cellspacing="0" 
                       width="100%" >
        <tr bordercolor="#839abf">
            <td >
                <table align="center" width="100%">
                <tr valign="top"><td class="copy10grey" align="left" >Role Name</td>
                    <td align="left">
                        <asp:TextBox ID="txt_role" CssClass="copy10grey" Width="170" runat="server"></asp:TextBox>
                        </td>
                        <td class="copy10grey" align="left">
                            Module
                        </td>
                        <td align="left">
                            <asp:ListBox ID="lbModulelist"  SelectionMode="Multiple" CssClass="copy10grey" Height="85" runat="server"></asp:ListBox>
                            
                        </td>
                </tr>
                 <tr>
                    <td colspan="4">
                        <hr />
                    </td>
                </tr>
                <tr>
                    <td colspan="4" style="text-align: center">
                        <asp:Button ID="btn_Search" runat="server" Text="Search Role" OnClick="btn_Search_click" CssClass="button" />
                        <asp:Button ID="btn_cancel" runat="server" Text="Cancel" CssClass="button" OnClick="btn_cancel_click" />
                        
                    </td>
                </tr>
            </table>
     
            
     
   
    </td>
    </tr>
    </table>
        <table border="0" cellpadding="0" cellspacing="0" 
                width="100%"  >
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>
                        
                        <asp:GridView ID="rolesGV" AutoGenerateColumns="false" runat="server" 
                            Width="100%" style="text-align: left" 
>
<RowStyle BackColor="Gainsboro" />
        <AlternatingRowStyle BackColor="white" />
        
                        <Columns>
                            <asp:TemplateField HeaderText="Roles Title" HeaderStyle-CssClass="button" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="85%">
                                <ItemTemplate>
                                    
                                    <asp:Label ID="lblRole" runat="server" CssClass="copy10grey"  Text='<%# Eval("rolename") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Status" HeaderStyle-CssClass="button" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Image ID="Image1" ImageUrl='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "active"))>0 ? "../images/tick.png" : "../images/cancel.gif" %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="" HeaderStyle-Width="70" HeaderStyle-CssClass="button" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkEdit" CssClass="copy10grey" OnCommand="Edit_click" CommandArgument='<%# Eval("roleID") %>'  runat="server">Edit</asp:LinkButton>
                                    &nbsp;
                                    <asp:LinkButton ID="lnkDelete" CssClass="copy10grey" OnCommand="Delete_click" CommandArgument='<%# Eval("roleID") %>' OnClientClick="return confirm('Delete this Role?');"  runat="server">Delete</asp:LinkButton>
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
            <td>
            <br /><br /><br />
            <br /><br /><br />
            <br /><br /><br />
            <br /><br /><br />
            
            <foot:MenuFooter id="Footer" runat="server"></foot:MenuFooter></td>
        </tr>
    </table>
           
   
    </form>
</body>
</html>
