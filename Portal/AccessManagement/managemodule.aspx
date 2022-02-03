<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="managemodule.aspx.cs" Inherits="avii.AccessManagement.managemodule" ValidateRequest="false" %>

<%@ Register TagPrefix="menu" TagName="Menu" Src="~/Controls/Header.ascx" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>
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
	
    <link href="/aerostyle.css" type="text/css" rel="stylesheet" />
   
   <script type="text/javascript">
       function editmodule(obj)
       {
            var objmodule = document.getElementById(obj.id.replace('lnkEdit','hdnModuleGUID'));   
            window.location.href='addmodules.aspx?Moduleid='+objmodule.value;
       }
       function confirmation(obj) 
       {
           var check = false;
           var grid = document.getElementById('GV_Module');
           if (grid != null) 
           {
               var dj = grid.getElementsByTagName('input');

               for (var j = 0; j < dj.length; j++) 
               {
                   if (dj[j].id.indexOf('chkmodule') > -1) 
                   {
                       if (dj[j].checked)
                           check = true;
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
       }
        
   </script>

</head>
<body bgcolor="#ffffff" leftmargin="0" rightmargin="0" topmargin="0" style="text-align: center">
    <form id="form1" runat="server">
    <table cellspacing="0" cellpadding="0" border="0" align="center" width="100%">
    <tr>
        <td>
            
                <menu:menu ID="menu1" runat="server" ></menu:menu>
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
                    </td>
               </tr>
                <tr class="button" align="left" >
                    <td>
                       Module Query
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblMsg" runat="server" CssClass="errormessage"></asp:Label>
                    </td>
                </tr>
                
               <tr><td> 
               <asp:HiddenField ID="hdnEdit" runat="server" />
                        <asp:HiddenField ID="hdnDel" runat="server" />
        <table  align="center" bordercolor="#839abf" border="1" cellpadding="0" cellspacing="0" width="100%">
        <tr bordercolor="#839abf"><td><table class="box" width="100%" cellpadding="1" cellspacing="1">
            <tr>
            <td></td>
                <td class="copy10grey" width="10%" >
                    &nbsp;Type:</td>
                <td width="15%">
                    <asp:DropDownList ID="ddlType" CssClass="copy10grey" runat="server" AutoPostBack="false" 
                        onselectedindexchanged="ddlType_SelectedIndexChanged">
                        <asp:ListItem Text="Admin" Value="adm"></asp:ListItem>
                        <asp:ListItem Text="User" Value="usr"></asp:ListItem>
                        <asp:ListItem Text="Public" Value="pub"></asp:ListItem>
                    </asp:DropDownList>
                </td >
                <td class="copy10grey" width="15%">
                    
                    Module Name:
                </td>
                <td width="25%">
                    <asp:TextBox ID="txtModuleTitle" runat="server" CssClass="copy10grey" MaxLength="100" 
                        Width="200"></asp:TextBox>
                </td>
                <td width="20%"></td>
                
            </tr>
            <tr>
                <td colspan="6" align="center">
                    <hr  />    
                </td>
            </tr>
            <tr>
            <td align="center" colspan="6">
                  <asp:Button ID="btnsearchModule" runat="server" Text="Search Module" 
                      CssClass="button" onclick="btnsearchModule_Click"
                       />
                   <asp:Button ID="btn_cancel" runat="server"  CssClass="button" Text="Cancel" onclick="btn_cancel_Click" />
          
            </td>
          </tr>
           
            </table></td></tr></table>
            
                   <br />
                   
                   
          </td></tr>
          
          <tr>
            <td>
                &nbsp;
            </td>
          </tr>
          <tr>
            <td>
                <table width="100%">
                <tr>
                    <td >
                        <asp:HiddenField ID="hdnModuleGUID" runat="server" />
                       
                        <asp:GridView ID="GV_Module" runat="server" Width="100%" CssClass="copy10grey" 
                         GridLines="Both"    AutoGenerateColumns="False"  
                            >
                         <RowStyle BackColor="Gainsboro" />
                        <AlternatingRowStyle BackColor="white" />
                        <Columns>
                            <asp:TemplateField HeaderText="Module" HeaderStyle-CssClass="button" HeaderStyle-HorizontalAlign="Left" >
                                <ItemTemplate>
                                      
                                    <asp:HiddenField ID="hdnParentModuleGUID" Value='<%#Eval("moduleparentGUID")%>' runat="server" />
                                    <asp:HiddenField ID="hdnIsItem" Value='<%#Eval("isitem")%>' runat="server" />

                                    <asp:HiddenField ID="hdnModuleGUID" Value='<%#Eval("ModuleGUID")%>' runat="server" />
                                    <asp:Label ID="lblModule" CssClass="copy10grey" runat="server" Text='<%#Eval("ModuleName")%>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle CssClass="button" HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Title" HeaderStyle-CssClass="button" HeaderStyle-HorizontalAlign="Left" >
                                <ItemTemplate>
                                    <asp:Label ID="lblTitle" CssClass="copy10grey" runat="server" Text='<%#Eval("Title")%>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle CssClass="button" HorizontalAlign="Left" />
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Url" HeaderStyle-CssClass="button" HeaderStyle-HorizontalAlign="Left" >
                                <ItemTemplate>
                                    <asp:Label ID="lblurl" CssClass="copy10grey" runat="server" Text='<%#Eval("Url")%>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle CssClass="button" HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Status" HeaderStyle-CssClass="button" HeaderStyle-HorizontalAlign="Left" >
                                <ItemTemplate>
                                    <asp:HiddenField ID="hdnActive" Value='<%#Eval("active")%>' runat="server" />
                                    <asp:HiddenField ID="hdnUserType" Value='<%#Eval("UserType")%>' runat="server" />
                                    
                                    <asp:Image ID="Image1" ImageUrl='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "active"))>0 ? "../images/tick.png" : "../images/cancel.gif" %>' runat="server" />
                                    
                                </ItemTemplate>
                                <HeaderStyle CssClass="button" HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="" ItemStyle-CssClass="copy10grey" HeaderStyle-CssClass="button"  HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="70">
                                <ItemTemplate>
                                    <div id="editDiv" name="editDiv">
                                    <asp:LinkButton ID="lnkEdit" CssClass="copy10grey"  
                                        CommandArgument='<%# Eval("ModuleGUID") %>'  runat="server" 
                                        OnClientClick="return editmodule(this)" >Edit</asp:LinkButton>
                                    
                                    </div>
                                </ItemTemplate>
                                <HeaderStyle CssClass="button" HorizontalAlign="Left" Width="70px" />
                                <ItemStyle CssClass="copy10grey" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="" ItemStyle-CssClass="copy10grey" HeaderStyle-CssClass="button"  HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="70">
                                <ItemTemplate>
                                <div id="delDiv" name="delDiv">
                                <asp:CheckBox ID="chkmodule"  
                                         AutoPostBack="true" 
                                        runat="server" oncheckedchanged="chkmodule_CheckedChanged"/>
                                    </div>
                                </ItemTemplate>
                                <HeaderStyle CssClass="button" HorizontalAlign="Left" Width="70px" />
                                <ItemStyle CssClass="copy10grey" />
                                </asp:TemplateField>
                                
                            
                            
                        </Columns>
                    </asp:GridView>
                         </td>
                </tr>
                <tr><td align="right">
                    <asp:Panel ID="pnlDelete" runat="server">
                    
                <asp:Button ID="btnInactive" runat="server" Text="Deactivate" 
                            onclick="btnInactive_Click" CssClass="button"  OnClientClick="return confirmation(1)"/>
                        <asp:Button ID="btndelete"
                            runat="server" Text="Del" CssClass="button"  onclick="btndelete_Click" OnClientClick="return confirmation(0)"/>
                   </asp:Panel>
                </td></tr>
                </table>
            </td>
          </tr>
          
          </table> </ContentTemplate>
         </asp:UpdatePanel>     
          </td>
    </tr>
    <tr><td>&nbsp;</td></tr>
    <tr><td>&nbsp;</td></tr>
    <tr>
    <td><foot:MenuFooter id="Footer" runat="server"></foot:MenuFooter></td>
    </tr>
    </table>
     <script type="text/javascript" language="javascript">
         function Permission() {
             var pnlDeleteObj = document.getElementById("<%=pnlDelete.ClientID %>");
             var editPermission = document.getElementById("<%=hdnEdit.ClientID %>").value;
             
             var deletePermission = document.getElementById("<%=hdnDel.ClientID %>").value;
             //var arrObj = document.getElementsByTagName("div");
             var arrObj = document.getElementsByName("editDiv");
             var arrDelObj = document.getElementsByName("delDiv");
             for (var i = 0; i < arrObj.length; i++) {
                 if (arrObj[i].id.indexOf('editDiv') > -1) {
                     if (editPermission != "Edit")
                         arrObj[i].style.visibility = "hidden";
                     else
                         arrObj[i].style.visibility = "visible";
                 }
             }
             if (pnlDeleteObj != null) {
                 if (deletePermission != "Delete")
                     pnlDeleteObj.style.visibility = "hidden";
                 else
                     pnlDeleteObj.style.visibility = "visible";
             }
             for (var i = 0; i < arrDelObj.length; i++) {
                 if (arrDelObj[i].id.indexOf('delDiv') > -1) {
                     if (deletePermission != "Delete")
                         arrDelObj[i].style.visibility = "hidden";
                     else
                         arrDelObj[i].style.visibility = "visible";

                 }
             }
         }
         //Permission();
            </script>
    </form>
</body>
</html>
