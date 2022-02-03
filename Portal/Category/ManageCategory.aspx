<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageCategory.aspx.cs" Inherits="avii.Category.ManageCategory" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>
<%@ Register TagPrefix="menu" TagName="Menu" Src="~/Controls/Header.ascx" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Manage Category</title>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>

    <script>
        
        $(document).ready(function () {
            $("#txtCategoryName").keypress(function (e) {
                var regex = new RegExp("^[a-zA-Z ]+$");
                var str = String.fromCharCode(!e.charCode ? e.which : e.charCode);
                if (regex.test(str)) {
                    return true;
                }
                else {
                    e.preventDefault();
                    return false;
                }
            });

            $("#txtDesc").keypress(function (e) {
                var tval = $('txtDesc').val(),
                    tlength = tval.length,
                    set = 500,
                    remain = parseInt(set - tlength);

                if (remain <= 0 && e.which !== 0 && e.charCode !== 0) {
                    $('txtDesc').val((tval).substring(0, tlength - 1));
                    return false;
                }
            });
            //$( "#txtCategory" ).keypress(function(e) {
            //    var key = e.keyCode;
            //    if (key >= 48 && key <= 57) {
            //        e.preventDefault();
            //    }
            //});
        });



        </script>
     <script type="text/javascript">
         function ValidateIsSIM() {
             var msg = 'Either "Is SIM" or "SIM Required" can be selected';
             var obj = document.getElementById("chkIsSIM")

             var IsSim = obj.checked;
             if (IsSim) {

                 var SIMRequired = document.getElementById("chkSIMRequired").checked;
                 if (SIMRequired) {
                     obj.checked = false;
                     alert(msg);
                 }
                 else {
                     var ESNRequired = document.getElementById("chkESN").checked;
                     if (!ESNRequired)
                         document.getElementById("chkESN").checked = true;
                 }
             }
         }
         function ValidateSIMRequired() {
           
             var msg = 'Either "Is SIM" or "SIM Required" can be selected';
             //alert(obj);
             var obj = document.getElementById("chkSIMRequired")
             var sim = obj.checked;

             if (sim) {

                 var IsSim = document.getElementById("chkIsSIM").checked;
                 if (IsSim) {
                     obj.checked = false;
                     alert(msg);

                 }
             }
         }
         function fileValidation() {
             var fileInput = document.getElementById('fu');
             var filePath = fileInput.value;
             var allowedExtensions = /(\.jpg|\.jpeg|\.png|\.gif)$/i;
             if (!allowedExtensions.exec(filePath)) {
                 alert('Please upload file having extensions .jpeg/.jpg/.png/.gif only.');
                 fileInput.value = '';
                 return false;
             } else {

                 //Image preview
                 //if (fileInput.files && fileInput.files[0]) {
                 //    var reader = new FileReader();
                 //    reader.onload = function (e) {
                 //        document.getElementById('imagePreview').innerHTML = '<img src="' + e.target.result + '"/>';
                 //    };
                 //    reader.readAsDataURL(fileInput.files[0]);
                 //}
             }
             return true;
         }
     </script>
</head>
<body gcolor="#ffffff" leftmargin="0" rightmargin="0" topmargin="0">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table cellspacing="0" cellpadding="0" border="0" align="center" width="100%">
    <tr>
        <td>
            <menu:Menu ID="menu1" runat="server" ></menu:Menu>
        </td>
     </tr>
     </table>
     <table cellspacing="0" cellpadding="0" border="0" align="center" width="100%">
        <tr valign="top">
            <td   >
                <table align="center" style="text-align:left" width="100%">
                <tr class="buttonlabel" align="left">
                <td>&nbsp;Manage Category</td></tr>
             </table>
                </td>
            </tr>
         </table>
     <table cellspacing="0" cellpadding="0" border="0" align="center" width="95%">
        <tr valign="top">
            <td   >
     
                <%--<asp:UpdatePanel ID="UpdatePanel1"  UpdateMode="Conditional" ChildrenAsTriggers="true" runat="server"   >
         <ContentTemplate>--%>
         <table  align="center" style="text-align:left" width="100%">
         <tr>
            <td>
               <asp:Label ID="lblMsg" runat="server" CssClass="errormessage"></asp:Label>
            
            </td>
         </tr>
         </table>
        <table bordercolor="#839abf" border="1" width="100%" align="center" cellpadding="0" cellspacing="0">
          <tr>
            <td>
                <table width="100%" border="0" class="box" align="center" cellpadding="5" cellspacing="5">
                    <tr>
                        <td class="copy10grey" align="left">
                            Parent Category:
                        </td>
                        <td class="copy10grey" align="left">
                            
                            <asp:DropDownList ID="ddlParent" CssClass="copy10grey" runat="server" Width="65%">
	                        </asp:DropDownList>              
                        </td>
                        <td>

                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="copy10grey" align="left" width="20%">
                            <strong>Category Name:</strong>
                        </td>
                        <td class="copy10grey" align="left" width="35%">
                            <asp:TextBox ID="txtCategoryName" runat="server"   CssClass="copy10grey" MaxLength="50"  Width="65%"></asp:TextBox>
                    
                        </td>
                        <td class="copy10grey" align="left" width="18%">
                            <strong>Status:</strong>
                        </td>
                        <td class="copy10grey" align="left" width="27%">

                            <asp:DropDownList ID="ddlStatus" CssClass="copy10grey" runat="server" Width="70%">
                                 <asp:ListItem Text="Active" Value="true"></asp:ListItem>
                                 <asp:ListItem Text="Inactive" Value="false"></asp:ListItem>
	                        </asp:DropDownList>              
        
                        </td>
                    </tr>
                    <tr valign="top">
                        <td class="copy10grey" align="left">
                            Description:
                            <br />
                            (Max 500 characters)
                        </td>
                        <td colspan="3" class="copy10grey" align="left">
                            <asp:TextBox ID="txtDesc" runat="server"   CssClass="copy10grey" Columns="3" Height="70" MaxLength="500" TextMode="MultiLine"  Width="90%"></asp:TextBox>                    
                        </td>
                        
                    </tr>
                    <tr>
                        <td class="copy10grey" align="left">
                            Image:
                        </td>
                        <td colspan="3" class="copy10grey" align="left">
                            <asp:FileUpload ID="fu" runat="server" onchange="return fileValidation();" />
                        </td>
                        
                    </tr>
                    
                </table>

                <br />
            </td>
         </tr>
            <tr>
                <td>
                    
        <table align="center" style="text-align:left" width="100%" border="0">
                <tr class="buttonlabel" align="left">
                <td>&nbsp;Attributes</td></tr>
             </table>
          <table bordercolor="#839abf" border="0" width="100%" align="center" cellpadding="0" cellspacing="0">
          <tr>
            <td>
                <table width="100%" border="0" class="box" align="center" cellpadding="5" cellspacing="5">
                 <tr style="vertical-align:top">
                        <td class="copy10grey"  align="left" style="vertical-align:top">
                            ESN Required &nbsp; <asp:CheckBox ID="chkESN" runat="server" Text=" "  CssClass="copy10grey" ></asp:CheckBox> 
                        </td>
                        <td class="copy10grey" align="left">
                           Kitted Box &nbsp; &nbsp;&nbsp; <asp:CheckBox ID="chkKittedBox" runat="server" Text=""  CssClass="copy10grey" ></asp:CheckBox> 
                        </td>
                        <td class="copy10grey" align="left">
                          SIM Required &nbsp;  <asp:CheckBox ID="chkSIMRequired" onclick="ValidateSIMRequired()" runat="server" Text=""  CssClass="copy10grey" ></asp:CheckBox> 
                        </td>
                        <td class="copy10grey" align="left">

                          Is SIM &nbsp; &nbsp;&nbsp;&nbsp; &nbsp; <asp:CheckBox ID="chkIsSIM" runat="server" onclick="ValidateIsSIM()"  Text="" CssClass="copy10grey" ></asp:CheckBox> 
        
                        </td>
                    </tr>
                   
                <tr>
                        <td class="copy10grey" align="left">
                          RMA Allowed &nbsp;&nbsp;  <asp:CheckBox ID="chkRMAAllowed" runat="server" Text=" "   ></asp:CheckBox>
                        </td>
                        <td class="copy10grey" align="left">
                           SKU Mapping&nbsp; <asp:CheckBox ID="chkSKUMapping" runat="server" Text=""  CssClass="copy10grey" ></asp:CheckBox>                            
                        </td>
                        <td class="copy10grey" align="left">
                           Re-Stocking &nbsp; &nbsp; <asp:CheckBox ID="chkReStocking" runat="server" Text=""  CssClass="copy10grey" ></asp:CheckBox>
                        </td>
                        <td class="copy10grey" align="left">

                          Forecasting&nbsp;  <asp:CheckBox ID="chkForecasting" runat="server"   CssClass="copy10grey" Text=" " ></asp:CheckBox>
        
                        </td>
                    </tr>
                   </table>
                </td>
              </tr>
              </table>
                    
                </td>
            </tr>
         </table>   <br />
         
          <table align="center" style="text-align:left" width="80%">
          <tr>
         <tr>
                    <td  align="center"  colspan="5">
                    
                            <table width="100%" cellpadding="0" cellspacing="0">
                                 <tr>
                                   
                                    <td  align="center">
                                
                                        <asp:Button ID="btnSubmit" CssClass="button" runat="server" OnClick="btnSubmit_Click" Text="Submit" />
                               
                                        &nbsp;<asp:Button ID="btnCancel" Visible="true" runat="server"  CssClass="button" Text="Cancel" OnClick="btnCancel_Click" />

                                        </td>
                                </tr>
                        
                            </table>
                        
                    </td>
                    </tr>
                </table>
        <%--</ContentTemplate>
             <Triggers>
                 <asp:PostBackTrigger ControlID="btnSubmit" />
             </Triggers>
        </asp:UpdatePanel>--%>
        </td>
            </tr>
         </table>
    </form>
</body>
</html>
