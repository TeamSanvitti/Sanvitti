<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomerRoleMgmt.aspx.cs" Inherits="Sanvitti1.AccessManagement.CustomerRoleMgmt" %>
<%@ Register TagPrefix="menu" TagName="Menu" Src="~/Controls/Header.ascx" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../wsastyle.css" type="text/css" rel="stylesheet"/>
    
    
	
	<script type="text/javascript" language="javascript">
        

        
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


        function isNumberKey(evt) {

            var charCodes = evt.keyCode ? evt.keyCode : evt.which ? evt.which : evt.charCode ? evt.charCode : evt.type;
            if (charCodes > 31 && (charCodes < 48 || charCodes > 57)) {
                charCodes = 0;
                return false;
            }
            return true;
        }
    </script>
    
</head>
<body bgcolor="#ffffff" leftmargin="0" rightmargin="0" topmargin="0"  style="z-index: 1">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="360000">
                                </asp:ScriptManager>
                              
    <table cellspacing="0" cellpadding="0" border="0" align="center" width="100%">
    <tr>
        <td>
            <menu:menu ID="menu1" runat="server" ></menu:menu>
            </td>
            </tr>  
                 
            <tr>
            <td>
               
            <table align="center" width="95%" style="text-align: left;" >
            <tr><td  >&nbsp;</td></tr>   
            <tr class="button" >
                <td align="left">
                    &nbsp;Manage Role 
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
    <table class="box" border="0" width="100%" cellpadding="3" cellspacing="3">
      
        <tr>
            <td class="copy10grey" align="left">
                <table width="95%" border="0">
                <tr>
                <td class="copy10grey">
                Role:<span class="errormessage">*</span>&nbsp;
                </td>
                    <td align="left">
                    <asp:UpdatePanel ID="UpdatePanel1"  runat="server">
                    <ContentTemplate>
                        <asp:TextBox MaxLength="30" Width="150" CssClass="copy10grey" ID="txt_role" 
                        runat="server" AutoPostBack="True" ontextchanged="txt_role_TextChanged"></asp:TextBox>  
                        </ContentTemplate>
                        </asp:UpdatePanel>  
                    </td>
                    <td align="left">
                        <asp:CheckBox ID="cbk_active" CssClass="copy10grey" runat="server" Text="Active" />
                    </td>
                    <td align="left">
                           
                        
                    </td>
                </tr>
                <%--<tr>
                <td align="left" class="copy10grey">
                    Company:
                </td>
                <td align="left">
                    <asp:DropDownList ID="ddlCompany"  CssClass="copy10grey" runat="server" >
                    </asp:DropDownList>
                              
                </td>
                <td align="left" class="copy10grey">
                Priority:&nbsp;&nbsp;&nbsp;&nbsp; <asp:TextBox MaxLength="3" onkeypress="return isNumberKey(event);"  Width="50" CssClass="copy10grey" ID="txtPriority" 
                        runat="server" ></asp:TextBox>  
                </td>
                    
                <td align="left">
                        
                </td>
                </tr>--%>


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
                    <asp:GridView ID="gvCustModules" runat="server" Width="100%"  CssClass="copy10grey" AutoGenerateColumns="False" OnRowDataBound="GVCust_rowDataBound" >
                <%-- <RowStyle Font-Size="Smaller" />--%>
                    <RowStyle BackColor="Gainsboro" Font-Size="Smaller"/>
            <AlternatingRowStyle BackColor="white" />
                <Columns>
                        <asp:TemplateField  HeaderText="Module" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="50%" HeaderStyle-CssClass="button">
                            
                        <ItemTemplate>
                            
                            <asp:HiddenField ID="hdntype" Value='<%#Eval("usertype") %>' runat="server" />
                            <asp:HiddenField ID="hdnModuleparentGUID1" Value='<%#Eval("ModuleParentGUID")%>' runat="server" />
                            <asp:HiddenField ID="hdnModuleGUID1" Value='<%#Eval("ModuleGUID")%>' runat="server" />
                                
                            <asp:Label ID="lblTitle" CssClass="copy10grey" runat="server" Text='<%# Convert.ToString(DataBinder.Eval( Container.DataItem, "usertype")) == "adm" ? Convert.ToString(DataBinder.Eval( Container.DataItem, "title")) + "*" : DataBinder.Eval( Container.DataItem, "title") %>'></asp:Label>
                        </ItemTemplate>
                        
                    </asp:TemplateField>
                    <asp:TemplateField  HeaderText="Access"  ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="50%" HeaderStyle-CssClass="button">
                        <ItemTemplate>
                                
                               <asp:CheckBox ID="chkAccess" runat="server" CssClass="copy10grey"/>

                                
                              
                            </ItemTemplate>
                         </asp:TemplateField>
                </Columns>
            </asp:GridView>
                
                </td>
            </tr>
            <tr>
            <td>
            <br />
                <asp:Button ID="btn_role" runat="server" CssClass="buybt" Text="Submit Role" OnClientClick="return IsValidate();"
                    onclick="btn_role_Click"/>
                            <asp:LinkButton ID="lnkBacktosearch" runat="server" CssClass="buybt"  Height="19" Width="150"
                            PostBackUrl="~/AccessManagement/CustomerRoleQuery.aspx?search=1">Back to search</asp:LinkButton>
                <asp:Button ID="btn_cancel" runat="server" CssClass="buybt" Text="Cancel" 
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
               
        </td>
        </tr>
    
          
    </table>
     
  
    </form>
</body>
</html>
