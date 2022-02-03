<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="roleMgmt.aspx.cs" Inherits="avii.AccessManagement.roleMgmt" ValidateRequest="false"%>
<%@ Register TagPrefix="menu" TagName="Menu" Src="~/Controls/Header.ascx" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

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
    
    <link href="../product/ddcolortabs.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript" src="../product/ddtabmenu.js"></script>
    
    <link href="../dhtmlwindow.css" type="text/css" rel="stylesheet" />
   
	
<%--	<link rel="stylesheet" href="../dhtmlxwindow/style.css" type="text/css" media="screen" />--%>
    <link rel="stylesheet" type="text/css" href="../dhtmlxwindow/dhtmlxwindows.css"/>
	<link rel="stylesheet" type="text/css" href="../dhtmlxwindow/skins/dhtmlxwindows_dhx_skyblue.css"/>
	
	<script src="../dhtmlxwindow/dhtmlxcommon.js"></script>
	<script src="../dhtmlxwindow/dhtmlxwindows.js"></script>
	<script src="../dhtmlxwindow/dhtmlxcontainer.js"></script>
    <script type="text/javascript" language="javascript">
    var dhxWins, w1;
    function doOnLoad() {
		dhxWins = new dhtmlXWindows();
		dhxWins.enableAutoViewport(false);
		dhxWins.attachViewportTo("winVP");
		dhxWins.setImagePath("../../codebase/imgs/");
		w1 = dhxWins.createWindow("w1", 320, 100, 465, 350);
		w1.setText("Add/Edit Permissions");
		
		w1.attachURL("permissions.aspx?flag=1");
		
	}
	
        function openPop() {
       
            url = "permissions.aspx?flag=1";
            var googlewin = dhtmlwindow.open("googlebox", "iframe", url, "Add/Edit Permission", "width=465px,height=350px,resize=0,scrolling=0,center=1", "recal")
        }
        function IsValidate() {

            var txt_role = document.getElementById("txt_role").value;
            var lblMsg = document.getElementById("lblMsg").innerHTML;
            if (txt_role == "") {
                alert("Role Name can't be blank");
                return false;
            }
            if (lblMsg == "Role Name already exists") {
                return false;
            }

        }
        function reset() {
            var txt_role = document.getElementById("txt_role");
            var cbk_active = document.getElementById("cbk_active");
            txt_role.value = "";
            cbk_active.checked = false;
            
            var permissionarr = document.getElementsByTagName('input');
           
            for (var i = 0; i < permissionarr.length; i++) {
              
                if (permissionarr[i].id.indexOf('cbk_permissions') > -1) {
              
                    permissionarr[i].checked = false;
                    permissionarr[i].disabled = false;
                }
            }
        }
        function confirmmessage(obj) {
            var lblMsg = document.getElementById("lblMsg");
            if (obj == '1') {
                lblMsg.innerHTML = "Role Name already exists.";
            } 
            else
                lblMsg.innerHTML = "";
        }
    </script>
    
    
    
</head>
<body  bgcolor="#ffffff" leftmargin="0" rightmargin="0" topmargin="0"  style="z-index: 1">
 
    <form id="form1" runat="server"><asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="360000">
                                </asp:ScriptManager>
                              
    <table cellspacing="0" cellpadding="0" border="0"  width="100%">
        <tr>
            <td>
                <menu:menu ID="menu1" runat="server" ></menu:menu>
                </td>
                </tr>  
                 
                <tr>
                <td>
                
             <div id="winVP" style="z-index:1">
             <br />
                
               
               <table align="center" width="95%" style="text-align: left;" >
               
                <tr class="button" >
                    <td align="left">
                        &nbsp;Add/Edit Role 
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblMsg" runat="server" CssClass="errormessage"></asp:Label>
                    </td>
                </tr>
                <tr>
                   <td> 
        <table  align="center" bordercolor="#839abf" border="1" cellpadding="0" cellspacing="0" width="100%">
        <tr>
        <td>
        <table class="box" border="0" width="100%">
      
            <tr>
                <td class="copy10grey" align="left">
                    <table width="95%" border="0">
                    <tr>
                    <td class="copy10grey">
                    Role<span class="errormessage">*</span>&nbsp;
                    </td>
                        <td align="left">
                        <asp:UpdatePanel  runat="server">
                        <ContentTemplate>
                            <asp:TextBox MaxLength="30" Width="150" CssClass="copy10grey" ID="txt_role" 
                            runat="server" AutoPostBack="True" ontextchanged="txt_role_TextChanged"></asp:TextBox>  
                            </ContentTemplate>
                            </asp:UpdatePanel>  
                        </td>
                        <td class="copy10grey">
                    Role Type<span class="errormessage">*</span>&nbsp;</td>
                        <td>
                            <asp:DropDownList ID="ddlRoleType" CssClass="copy10grey"
                                runat="server"  >                               
                            </asp:DropDownList>
            
                        </td>
                        <td align="left">
                            <asp:CheckBox ID="cbk_active" CssClass="copy10grey" runat="server" Text="Active" />
                        </td>
                        <td align="left">
                           
                            <img id="img1" onclick="doOnLoad();" alt="Add Permission" src="../images/personal-ref.png" />
                        </td>
                    </tr>
                    </table>
                    </td>
            </tr>
            <tr>
                <td   align="center" id="tabTD">
                <asp:HiddenField ID="hdnTabindex" runat="server" />
                    <br />
                    
                        <table width="100%">
                        <tr>
                            <td>
                            
                            
                    <asp:Button ID="btnadmin" runat="server" CssClass="buybt" 
                         Text="Admin" onclick="btnadmin_Click" />
                 <asp:Button ID="btnCust" runat="server" CssClass="buybt" 
                         Text="Customer" onclick="btnCust_Click" />
                        <asp:Button ID="btnPublic" runat="server" CssClass="buybt" 
                        Text="Public" onclick="btnPublic_Click" />

                        
                    <asp:GridView ID="gvAdminModules" runat="server" Width="100%"  CssClass="copy10grey" AutoGenerateColumns="False" OnRowDataBound="GV_Module_rowDataBound">
                    <RowStyle Font-Size="Smaller" />
                    <Columns>
                         <asp:TemplateField   ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100%" >
                           <HeaderTemplate>
                           <table ><tr class="button"><td>Module</td><td>
                           <asp:DataList ID="dl_permissionheader" runat="server" RepeatDirection="Horizontal">
                                <ItemTemplate>
                                <table width="100%" cellspacing="2"><tr><td width="90px" align="left">
                                          <asp:Label ID="lbl_head" runat="server" Text='<%#Eval("Permissionname") %>'></asp:Label>
                                </td></tr></table>
                                </ItemTemplate>
                                </asp:DataList></td></tr>
                           </HeaderTemplate>
                            
                            <ItemTemplate>
                                <tr><td>
                                <asp:HiddenField ID="hdntype" Value='<%#Eval("usertype") %>' runat="server" />
                               <asp:HiddenField ID="hdnModuleparentGUID" Value='<%#Eval("ModuleParentGUID")%>' runat="server" />
                                <asp:HiddenField ID="hdnModuleGUID" Value='<%#Eval("ModuleGUID")%>' runat="server" />
                                
                                <asp:Label ID="lblTitle" CssClass="copy10grey" runat="server" Text='<%# Convert.ToString(DataBinder.Eval( Container.DataItem, "usertype")) == "adm" ? Convert.ToString(DataBinder.Eval( Container.DataItem, "title")) + "*" : DataBinder.Eval( Container.DataItem, "title") %>'></asp:Label>
                            </td>
                            <td>
                               <asp:UpdatePanel ID="UPOuter" runat="server">
                        <ContentTemplate>
                                <asp:CheckBoxList ID="cbk_permissions" AutoPostBack="true" CellSpacing="0" CellPadding="0"  runat="server" 
                                OnSelectedIndexChanged="cbk_permissions_SelectedIndexChanged"  RepeatDirection="Horizontal" CssClass="copy10grey">
                               
                             
                               </asp:CheckBoxList>
                                
                              </ContentTemplate></asp:UpdatePanel>
                                
                            </td></tr> 
                            </ItemTemplate>
                            <FooterTemplate>
                           </table>
                            </FooterTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:GridView ID="gvCustModules" runat="server" Width="100%"  CssClass="copy10grey" AutoGenerateColumns="False" OnRowDataBound="GVCust_rowDataBound" Visible="false">
                    <RowStyle Font-Size="Smaller" />
                    <Columns>
                         <asp:TemplateField   ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100%" >
                           <HeaderTemplate>
                           <table ><tr class="button"><td>Module</td><td>
                           <asp:DataList ID="dl_permissionheadercust" runat="server" RepeatDirection="Horizontal">
                                <ItemTemplate>
                                <table width="100%" cellspacing="2"><tr><td width="90px" align="left">
                                          <asp:Label ID="lbl_head" runat="server" Text='<%#Eval("Permissionname") %>'></asp:Label>
                                </td></tr></table>
                                </ItemTemplate>
                                </asp:DataList></td></tr>
                           </HeaderTemplate>
                            
                            <ItemTemplate>
                                <tr><td>
                                <asp:HiddenField ID="hdntype" Value='<%#Eval("usertype") %>' runat="server" />
                               <asp:HiddenField ID="hdnModuleparentGUID1" Value='<%#Eval("ModuleParentGUID")%>' runat="server" />
                                <asp:HiddenField ID="hdnModuleGUID1" Value='<%#Eval("ModuleGUID")%>' runat="server" />
                                
                                <asp:Label ID="lblTitle" CssClass="copy10grey" runat="server" Text='<%# Convert.ToString(DataBinder.Eval( Container.DataItem, "usertype")) == "adm" ? Convert.ToString(DataBinder.Eval( Container.DataItem, "title")) + "*" : DataBinder.Eval( Container.DataItem, "title") %>'></asp:Label>
                            </td>
                            <td>
                               <asp:UpdatePanel ID="UPOuter" runat="server">
                        <ContentTemplate>
                                <asp:CheckBoxList ID="cbk_permissionscust" AutoPostBack="true" CellSpacing="0" CellPadding="0"  runat="server" 
                                OnSelectedIndexChanged="cbk_permissionscust_SelectedIndexChanged"  RepeatDirection="Horizontal" CssClass="copy10grey">
                                </asp:CheckBoxList>
                                </ContentTemplate></asp:UpdatePanel>
                              
                                
                                
                            </td></tr> 
                            </ItemTemplate>
                            <FooterTemplate>
                           </table>
                            </FooterTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:GridView ID="gvPublicModules" runat="server" Width="100%"  CssClass="copy10grey" AutoGenerateColumns="False" OnRowDataBound="GVPublic_rowDataBound" Visible="false">
                    <RowStyle Font-Size="Smaller" />
                    <Columns>
                         <asp:TemplateField   ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100%" >
                           <HeaderTemplate>
                           <table ><tr class="button"><td>Module</td><td>
                           <asp:DataList ID="dl_permissionheaderpublic" runat="server" RepeatDirection="Horizontal">
                                <ItemTemplate>
                                <table width="100%" cellspacing="2"><tr><td width="90px" align="left">
                                          <asp:Label ID="lbl_head" runat="server" Text='<%#Eval("Permissionname") %>'></asp:Label>
                                </td></tr></table>
                                </ItemTemplate>
                                </asp:DataList></td></tr>
                           </HeaderTemplate>
                            
                            <ItemTemplate>
                                <tr><td>
                                <asp:HiddenField ID="hdntype" Value='<%#Eval("usertype") %>' runat="server" />
                               <asp:HiddenField ID="hdnModuleparentGUID2" Value='<%#Eval("ModuleParentGUID")%>' runat="server" />
                                <asp:HiddenField ID="hdnModuleGUID2" Value='<%#Eval("ModuleGUID")%>' runat="server" />
                                
                                <asp:Label ID="lblTitle" CssClass="copy10grey" runat="server" Text='<%# Convert.ToString(DataBinder.Eval( Container.DataItem, "usertype")) == "adm" ? Convert.ToString(DataBinder.Eval( Container.DataItem, "title")) + "*" : DataBinder.Eval( Container.DataItem, "title") %>'></asp:Label>
                            </td>
                            <td>
                                <asp:UpdatePanel ID="UPOuter" runat="server">
                        <ContentTemplate>
                                <asp:CheckBoxList ID="cbk_permissionspublic" AutoPostBack="true" CellSpacing="0" CellPadding="0"  runat="server" 
                                OnSelectedIndexChanged="cbk_permissionspublic_SelectedIndexChanged"  RepeatDirection="Horizontal" CssClass="copy10grey">
                                </asp:CheckBoxList>
                                </ContentTemplate>
                             </asp:UpdatePanel>
                                
                            </td></tr> 
                            </ItemTemplate>
                            <FooterTemplate>
                           </table>
                            </FooterTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                
                </td>
                        </tr>
                        <tr>
                            <td>
                             <br />
                  <asp:Button ID="btn_role" runat="server" CssClass="buttonlink" Text="Submit Role" OnClientClick="return IsValidate();"
                       onclick="btn_role_Click"/>
                                <asp:LinkButton ID="lnkBacktosearch" runat="server" CssClass="buttonlink"  Height="19" Width="150"
                                PostBackUrl="~/AccessManagement/rolequery.aspx?search=1">Back to search</asp:LinkButton>
                  <asp:Button ID="btn_cancel" runat="server" CssClass="buttonlink" Text="Cancel" 
                       onclick="btn_cancel_Click"/> 
                            </td>
                        </tr>
                        </table>
                
          </td></tr>
        
          
          
          
          </table>
         </td>
         </tr>
         </table>
         </table>
         
         </div>
               
          </td>
          </tr>
    
          
       </table>
     
  
    
    </form>
</body>
</html>
