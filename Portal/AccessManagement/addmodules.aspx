<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="addmodules.aspx.cs" Inherits="avii.AccessManagement.addmodules" ValidateRequest="false"%>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>
<%@ Register TagPrefix="menu" TagName="Menu" Src="~/Controls/Header.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
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
	
    <link href="../aerostyle.css" type="text/css" rel="stylesheet" />
    <%--<link href="../Styles.css" type="text/css" rel="stylesheet" />--%>
    <script type="text/javascript" language="javascript">
        function resetValues() 
        {
            document.getElementById('hdnCancel').value = "1";
        }
        function updateModule(obj) 
        {
           
            var objid = '';
            if (obj.id.indexOf('lnkEdit') > -1)
                objid = 'lnkEdit';
            else
                objid = 'lblModule';
            objParentNoduleGUID = document.getElementById(obj.id.replace(objid, 'hdnParentModuleGUID'));
            objddl = document.getElementById("<%=ddlparentModule.ClientID %>");

            for (i = 0; i < objddl.options.length; i++)
             {
                if (objParentNoduleGUID.value == objddl.options[i].value)
                    objddl.options[i].selected = true;
            }
            objModuleGUID = document.getElementById(obj.id.replace('lnkEdit', 'hdnModuleGUID'));
            objIsitem = document.getElementById(obj.id.replace('lnkEdit', 'hdnIsItem'));
            objUserType = document.getElementById(obj.id.replace('lnkEdit', 'hdnUserType'));
            objddluser = document.getElementById("<%=ddlType.ClientID %>");

            for (i = 0; i < objddluser.options.length; i++) 
            {
                if (objUserType.value == objddluser.options[i].value)
                    objddluser.options[i].selected = true;
            }
            
            var parentid = document.getElementsByTagName("input");
            var txturl = document.getElementById("txtUrl");
            var url = document.getElementById("url");
            txturl.style.display = "block";
            url.style.display = "block";
            for (var j = 0; j < parentid.length; j++) 
            {

                if (parentid[j].id.indexOf('hdnParentModuleGUID') > -1)
                 {
                     if (objModuleGUID.value == parentid[j].value)
                     {
                        var txturl = document.getElementById("txtUrl");
                        var url = document.getElementById("url");
                        txturl.style.display = "none";
                        url.style.display = "none";
                    }
                }
            }
            objModuleName = document.getElementById(obj.id.replace('lnkEdit', 'lblModule'));
            
            objModuleName.innerHTML = objModuleName.innerHTML.replace(/&nbsp;/g, '');
            objTitle = document.getElementById(obj.id.replace('lnkEdit', 'lblTitle'));
            objUrl = document.getElementById(obj.id.replace('lnkEdit', 'lblurl'));
            objActive = document.getElementById(obj.id.replace('lnkEdit', 'hdnActive'));
            document.getElementById('hdnModuleGUID').value = objModuleGUID.value;
            var isitem = objIsitem.value;
            document.getElementById('txtModuleName').value = objModuleName.innerHTML.replace(/&nbsp;/g, '');
            document.getElementById('txtTile').value = objTitle.innerHTML.replace(/&nbsp;/g, '');
            document.getElementById('txtUrl').value = objUrl.innerHTML;
            var vActive = document.getElementById('chkActive');
            var chkitem = document.getElementById('chkitem');
            if (objActive.value == "False")
            {
                vActive.checked = false;
            }
            else
                vActive.checked = true;
            if (isitem == "False") 
            {
                chkitem.checked = false;
            }
            else
                chkitem.checked = true;
           
          
            return false;
            
        }
        function IsValidate()
         {
            var modulename = document.getElementById("<% =txtModuleName.ClientID%>");
            if (modulename.value == "") 
            {
                alert('Module name can not be empty!');
                return false;
            }
            var txtTile = document.getElementById("<% =txtTile.ClientID%>");
            if (txtTile.value == "") {
                alert('Title can not be empty!');
                return false;
            }
            var lblMsg = document.getElementById("<%=lblMsg.ClientID%>").innerHTML;
           
            if (lblMsg == "Module already exist")
                return false;
        }
        function formatParentCatDropDown(objddl) 
        {

            for (i = 0; i < objddl.options.length; i++) 
            {
                objddl.options[i].innerHTML = objddl.options[i].innerHTML.replace(/&amp;/g, '&');
            }
        }
        function confirmation(obj) 
        {
            var check = false;
            var grid = document.getElementById('GV_Module');
            var dj= grid.getElementsByTagName('input');
            
            for(var j=0;j<dj.length;j++)
            {
                if(dj[j].id.indexOf('chkmodule') > -1)
                {
                    if(dj[j].checked)
                        check =true;
                }
            
            }

            if (!check)
             {
                if (obj == 1)
                    alert('Please select module to be deactivated!');
                else
                    alert('Please select module to be deleted!');

                return false;
            }


            if (obj == 1) 
            {
                var sure = confirm('All the selected modules will be deactivated! Are you sure to deactivate all?');

                if (sure == true) 
                {
                    return true;
                }
                else 
                {
                    return false;
                }
            }
            if (obj == 0)
             {
                var sure = confirm('All the selected modules will be deleted! Are you sure to delete all?');

                if (sure == true)
                 {
                    return true;
                 }
                 else
                 {
                    return false;
                 }
            }
        }
        
    </script>
</head>
<body bgcolor="#ffffff" leftmargin="0" rightmargin="0" topmargin="0" style="text-align: center">
    <form id="form1" runat="server">
    <table cellspacing="0" cellpadding="0" border="0" align="center" width="100%">
    <tr>
        <td>
                <menu:Menu ID="menu1" runat="server" ></menu:Menu>

        </td>
     </tr>
     <tr>
     <td>
         <asp:ScriptManager ID="ScriptManager1" runat="server">
         </asp:ScriptManager>
         <asp:UpdatePanel ID="UpdatePanel1" runat="server">
         <ContentTemplate>
            <table align="center" style="text-align:left" width="95%">
               
               <tr>
                <td>
                    &nbsp;<asp:HiddenField ID="hdnCancel" runat="server" />
                    <asp:HiddenField ID="hdnModuleGuid" runat="server" />
                    <asp:HiddenField ID="edit" Value="false" runat="server" />
                    
                </td>
               </tr>
                <tr class="button" align="left" ><td>&nbsp;Manage Modules</td></tr>
                <tr>
                    <td>
                        <asp:Label ID="lblMsg" runat="server" CssClass="errormessage"></asp:Label>
                    </td>
                </tr>
                
               <tr><td> 
        <table  align="center" bordercolor="#839abf" border="1" cellpadding="0" cellspacing="0" width="100%">
        <tr bordercolor="#839abf"><td><table class="box" width="100%" cellpadding="3" cellspacing="3">
            <tr>
                <td class="copy10grey" >
                    Module Type</td>
                <td > 
                    <asp:DropDownList ID="ddlType" runat="server"  AutoPostBack="true" CssClass="copy10grey"
                        onselectedindexchanged="ddlType_SelectedIndexChanged">
                    <asp:ListItem Text="Admin" Value="adm"></asp:ListItem>
                    <asp:ListItem Text="User" Value="usr"></asp:ListItem>
                    <asp:ListItem Text="Public" Value="pub"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="copy10grey">
                    Parent Module</td>
                <td>
                    <asp:DropDownList ID="ddlparentModule" runat="server" >
                    </asp:DropDownList>
                </td>
                
                <td class="copy10grey">
                    &nbsp;</td>
                
            </tr>
            <tr>
                <td class="copy10grey" >Name&nbsp;<span class="errormessage">*</span>
                </td>
                <td >
                    <asp:TextBox MaxLength="100" Width="200" CssClass="copy10grey" 
                        ID="txtModuleName" runat="server" AutoPostBack="false" 
                        ontextchanged="txtModuleName_TextChanged"></asp:TextBox>
                </td>
                <td class="copy10grey" >
                    Title &nbsp;<span class="errormessage">*</span>
                </td>
                <td colspan="2">
                    <asp:TextBox MaxLength="100" Width="200" CssClass="copy10grey" ID="txtTile" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="copy10grey" >
                  <span id="url">Url</span>  
                </td>
                <td width="250px">
                    <asp:TextBox MaxLength="200" Width="200" CssClass="copy10grey" ID="txtUrl" 
                        runat="server"></asp:TextBox>
                </td>
                <td class="copy10grey" > Active&nbsp;<asp:CheckBox CssClass="copy10grey" ID="chkActive" runat="server"></asp:CheckBox></td>
                <td class="copy10grey">
                    Is Item?
                    
                    <asp:CheckBox ID="chkitem" runat="server" CssClass="copy10grey" />
                </td>
                <td class="copy10grey">
                    
                    
                    
                </td>
            </tr>
           
            </table></td></tr></table>
            
                   <br />
                   
                   
          </td></tr>
          <tr>
            <td align="center">
                 <%--Visible='<% =Convert.ToBoolean(document.getElementById("edit").value) %>'--%>
                  <asp:Button ID="btnaddModule"  runat="server" Text="Submit Module" 
                      CssClass="button"  OnClientClick="return IsValidate();" 
                      onclick="btnaddModule_Click"/>
                   <asp:Button ID="btn_cancel" runat="server" OnClientClick="resetValues();" CssClass="button" Text="Cancel" onclick="btn_cancel_Click" />
            
            </td>
          </tr>
          
          </table>
        </ContentTemplate>
        </asp:UpdatePanel>
        </td>
        </tr>
         <tr>
         
            <td>
            <br /><br /><br />
            <br /><br /><br />
            <br /><br /><br />
            <br /><br /><br />
            <foot:MenuFooter id="Footer" runat="server"></foot:MenuFooter></td>
          </tr>
        </table>  
   
    <script type="text/javascript">

        function parentDropdown() {
            var obj = document.getElementById("<%=ddlparentModule.ClientID %>");
            formatParentCatDropDown(obj);
        }
        parentDropdown();
    </script>
    </form>
</body>
</html>
