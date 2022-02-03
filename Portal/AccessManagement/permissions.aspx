<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="permissions.aspx.cs" Inherits="avii.AccessManagement.permissions" ValidateRequest="false"%>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>
<%@ Register TagPrefix="menu" TagName="Menu" Src="~/Controls/Header.ascx" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
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
    <script type="text/javascript" language="javascript">
        function updatePermission(obj) 
        {
            objPermissionGUID = document.getElementById(obj.id.replace('lnkEdit', 'hdnPermissionGUID0'));
            objPermissionName = document.getElementById(obj.id.replace('lnkEdit', 'lblPermission'));
            
            document.getElementById('hdnPermissionGUID').value = objPermissionGUID.value;
            document.getElementById('txtPermissionName').value = objPermissionName.innerHTML;
            return false;
            
        }
        function IsValidate() 
        {
            var Permissionname = document.getElementById("<% =txtPermissionName.ClientID%>");
            if (Permissionname.value == "") 
            {
                alert('Permission name can not be empty!');
                return false;
            }
            var lblMsg = document.getElementById("lblMsg").innerHTML;
            if (lblMsg == "Permission already exists")
                return false;
        }
        function cancel()
         {
            var Permissionname = document.getElementById("<% =txtPermissionName.ClientID%>");
            Permissionname.value = "";
            var hdnPermissionGUID = document.getElementById("<% =hdnPermissionGUID.ClientID%>");
            hdnPermissionGUID.value = "";
        }
      
    </script>
</head>
<body bgcolor="#ffffff" leftmargin="0" rightmargin="0" topmargin="0" style="text-align: left">
    <form id="form1" runat="server">
    <div>
     <table cellspacing="0" cellpadding="0" border="0"  width="100%">
    <tr>
        <td>           
                <menu:menu ID="menu1" runat="server" ></menu:menu>
        </td>
     </tr>
     <tr>
     
     <td>
        <table align="center" style="text-align:left" width="95%">
               
               <tr>
                <td>
                    &nbsp;
                </td>
               </tr>
                <tr class="button" align="left" ><td>&nbsp;Add/Edit Permission</td></tr>
                <tr>
                    <td>
                        <asp:Label ID="lblMsg" runat="server" CssClass="errormessage"></asp:Label>
                    </td>
                </tr>
                
               <tr><td> 
        <table  align="center" bordercolor="#839abf" border="1" cellpadding="0" cellspacing="0" width="100%">
        <tr bordercolor="#839abf"><td><table class="box" width="100%" cellpadding="3" cellspacing="3">
      
            <tr>
                <td class="copy10grey" >Name<span class="errormessage">*</span>
                </td>
                <td >
                    <asp:TextBox MaxLength="50" Width="100" CssClass="copy10grey" 
                        ID="txtPermissionName" runat="server" AutoPostBack="True" 
                        ontextchanged="txtPermissionName_TextChanged"></asp:TextBox>
                </td>
               
                
               
                
            </tr>
            <tr>
               <td>
                   
                </td>
                <td>
                    
                </td>
                <td></td>
                <td></td>
            </tr>
            </table></td></tr></table>
            
                   <br />
                   
                   
          </td></tr>
          <tr>
            <td align="center">
                  <asp:Button ID="btnaddpermission" runat="server" Text="Submit Permission" 
                      CssClass="button"  OnClientClick="return IsValidate();"
                       onclick="btn_Permission_Click"/>
                   <asp:Button ID="btn_cancel" runat="server" CssClass="button" Text="Cancel" 
                      onclientclick="return cancel();" onclick="btn_cancel_Click" />
          
            </td>
          </tr>
          <tr>
            <td>
                &nbsp;
            </td>
          </tr>
          <tr>
            <td>
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                <table width="100%">
                <tr>
                    <td>
                        <asp:HiddenField ID="hdnPermissionGUID" runat="server" />
                        <asp:GridView ID="GV_Permission" runat="server" Width="100%" 
                            CssClass="copy10grey" AutoGenerateColumns="False" 
                            >
                        
                        <Columns>
                            <asp:TemplateField HeaderText="Permission" HeaderStyle-CssClass="button" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="40%">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hdnPermissionGUID0" Value='<%#Eval("PermissionGUID")%>' 
                                        runat="server" />
                                    <asp:Label ID="lblPermission" CssClass="copy10grey" runat="server" Text='<%#Eval("PermissionName")%>'></asp:Label>
                                </ItemTemplate>

<HeaderStyle HorizontalAlign="Left" CssClass="button" Width="75%"></HeaderStyle>
                            </asp:TemplateField>
                           
                          
                            <asp:TemplateField HeaderText="" ItemStyle-CssClass="copy10grey" HeaderStyle-CssClass="button"  HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="70">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkEdit" CssClass="copy10grey" CommandArgument='<%# Eval("PermissionGUID") %>' OnClientClick="return updatePermission(this)"  runat="server">Edit</asp:LinkButton>
                                    &nbsp;
                                    <asp:LinkButton ID="lnkDelete" CssClass="copy10grey" OnCommand="Delete_click" CommandArgument='<%# Eval("PermissionGUID") %>' OnClientClick="return confirm('Delete this Permission?');"  runat="server">Delete</asp:LinkButton>
                                </ItemTemplate>

<HeaderStyle HorizontalAlign="Left" CssClass="button" Width="70px"></HeaderStyle>

<ItemStyle CssClass="copy10grey"></ItemStyle>
                            </asp:TemplateField>

                            
                        </Columns>
                    </asp:GridView>
                    </td>
                </tr>
                </table></ContentTemplate>
                </asp:UpdatePanel>
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
            
            <foot:MenuFooter id="Footer" runat="server"></foot:MenuFooter></td>
          </tr>
          </table>
          
    </div>
    </form>
</body>
</html>
