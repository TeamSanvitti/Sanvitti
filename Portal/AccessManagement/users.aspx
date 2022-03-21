<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="users.aspx.cs" Inherits="avii.users" ValidateRequest="false" %>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="~/Controls/Header.ascx" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

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
    <link href="../../aerostyle.css" type="text/css" rel="stylesheet"/>
    
    <script type="text/javascript">
        function display(obj) {
            var userType = obj.value;
            //alert(userType);
            var company = document.getElementById("<% =ddlcompany.ClientID %>");
            
            var trCustomer = document.getElementById('trCustomer');
            var lblcompany = document.getElementById('spCompany');
            
            if ("Customer" != userType) {
                company.style.display = "none";
                lblcompany.style.display = "none";
                //companyAC.style.display = "none";
                //lblcompanyAC.style.display = "none";
                trCustomer.style.display = "none";
            }
            else
                {
                company.style.display = "block";
                lblcompany.style.display = "block";
                //companyAC.style.display = "block";
                //lblcompanyAC.style.display = "block";
                trCustomer.style.display = "block";
            }
        }
        function validate() {
            
            var chkRole = document.getElementById("chkRoles");
            var txt_user = document.getElementById("txt_user").value;
            var txt_email = document.getElementById("txt_email").value;
            var txt_pwd = document.getElementById("txt_pwd").value;
            var txt_pwdcon = document.getElementById("txt_con_pwd");
            var txt_con_pwd = document.getElementById("txt_con_pwd").value;
            var dd_type = document.getElementById("dd_type").selectedIndex;
            if (dd_type < 2) {
                var dd_cmp = document.getElementById("ddlcompany").selectedIndex;
                if (dd_cmp == "0" && dd_type == "1") {
                    alert("Please select a company");
                    return false;
                }
            }
            var lbl_message = document.getElementById("lbl_message").innerHTML;
            if (txt_user == "") {
                alert("UserName can not be blank");
                return false;
            }
            if (txt_email == "") {
                alert("Email can not be blank");
                return false;
            }
            if (txt_pwd == "") {
                alert("Password can not be blank");
                return false;
            }
            if (txt_con_pwd == "") {
                alert("Confirm Password can not be blank");
                return false;
            }
            if (txt_pwd != txt_con_pwd) {
                alert("Passwords does not match");

                return false;
            }
            if (dd_type == "0") {
                alert("Please select a type");
                return false;
            }
            
            if (lbl_message == "Username already exists!") {
                return false;
            }
            var roleList = document.getElementById("<%=chkRoles.ClientID %>").childNodes[0];
            var vflag = false;

            var options = roleList.getElementsByTagName('input');
            
            for (i = 0; i < options.length; i++) {
                var opt = options[i];
                if (opt.checked) {
                    vflag = true;
                } 
            }
            if (!vflag) {
                alert('Select atleast one role!');
                return false;
            }   
            return true;
        }
        function range() {
            var txt_pwd = document.getElementById("txt_pwd").value;
          
            if (txt_pwd.length < 8) {
                alert("Password must be of minimum 8 characters!");
                return false;
            }
        }
         function isAlfaNumberKey(evt)
        {
            
            var charCodes = evt.keyCode ? evt.keyCode : evt.which ? evt.which : evt.charCode;
           if (charCodes < 48 || charCodes>57 && charCodes < 65 || charCodes>90 && charCodes < 97 || charCodes>122)
           {
               evt.keyCode = 0;
               return false;
           }
           return true;
       }
        
    </script>
</head>
<body bgcolor="#ffffff" leftmargin="0" rightmargin="0" topmargin="0" >
    <form id="form1" runat="server" autocomplete="off">
    <table cellspacing="0" cellpadding="0" border="0" align="center" width="100%">
    <tr>
        <td>
          
        </td>
     </tr>
     <tr>
        <td width="100%">
            <head:MenuHeader ID="menu1" runat="server" ></head:MenuHeader>
        </td>
     </tr>
     <tr>
     
     <td>
     <asp:HiddenField ID="hdnuserType" runat="server" />
<asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
        <table align="center" style="text-align:left" width="95%">
               <tr ><td> &nbsp;</td></tr>
                <tr class="button"><td> &nbsp;
                <asp:Label ID="lblHeader" runat="server" Text="Add New User" ></asp:Label>

                 </td></tr>
               
               <tr><td> 
        <table  align="center" bordercolor="#839abf" border="1" cellpadding="0" cellspacing="0" width="100%">
        <tr bordercolor="#839abf">    <td > 
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
     <table width="100%" >
    
        <tr>
            <td colspan="2">

                <asp:Label ID="lbl_message" runat="server" CssClass="errormessage"></asp:Label>

            </td><td ></td><td >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" CssClass="errormessage"
                ControlToValidate="txt_email" ErrorMessage="RegularExpressionValidator" 
                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">Invalid Email!!!</asp:RegularExpressionValidator>
                </td>
            <td >
            </td>
        </tr>
        <tr valign="top">
            <td class="copy10grey">Account Type:&nbsp;<span class="errormessage">*</span></td>
            <td >
            <%--onchange="display(this)"--%>
            <asp:DropDownList ID="dd_type" AutoPostBack="true" OnSelectedIndexChanged="ddType_SelectedIndexChanged"  CssClass="copy10grey"
                runat="server"  Width="85%" >
                <asp:ListItem></asp:ListItem>
            <asp:ListItem Text="Customer" Value="Company"></asp:ListItem>
            <asp:ListItem Text="Internal User" Value="LANGlobal"></asp:ListItem>
            </asp:DropDownList>
            </td>
            <td class="copy10grey">Account Status:&nbsp;</td>
            <td >
            <asp:DropDownList ID="dd_status" runat="server"  Width="85%" 
                CssClass="copy10grey">
             
            </asp:DropDownList>
            </td>
        </tr>
        <tr valign="top" id="trCustomer" runat="server">
            
            <td class="copy10grey">
                <span id="spCompany" class="copy10grey">Customer:&nbsp;</span>
            </td>
            <td >
                <asp:DropDownList ID="ddlcompany" AutoPostBack="true" runat="server" Width="85%" 
                 OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged"   CssClass="copy10grey">
                </asp:DropDownList>
            </td>
            <td class="copy10grey">
                <%--<span id="spCompanyAccount" class="copy10grey">Company Account #</span>--%>
            </td>
            <td >
                <%--<asp:TextBox ID="txtCompanyAccount" runat="server" Width="215px" class="copy10grey"></asp:TextBox>--%>
                <input style="display:none" type="text" name="fakeusernameremembered"/>
<input style="display:none" type="password" name="fakepasswordremembered"/>
            </td>
       
            
        </tr>
        <tr valign="top">
            <td class="copy10grey" width="15%">User:&nbsp;<span class="errormessage">*</span></td><td  width="35%">
             
            <asp:TextBox ID="txt_user" AutoComplete="Off" AutoCompleteType="Disabled"
                runat="server" Width="85%"  class="copy10grey" AutoPostBack="false"                  
                onkeypress="return isAlfaNumberKey(event);"></asp:TextBox>
               
            </td><td class="copy10grey"  width="15%">E-mail:&nbsp;<span class="errormessage">*</span></td><td  width="35%">
            <asp:TextBox ID="txt_email" runat="server" Width="85%" class="copy10grey" AutoComplete="Off"></asp:TextBox>
            </td>
        </tr>
        <tr valign="top">
            <td class="copy10grey">Password:&nbsp;<span class="errormessage">*</span></td><td >
            <asp:TextBox ID="txt_pwd"  MaxLength="16"
                runat="server" Width="85%" class="copy10grey" TextMode="Password" AutoComplete="Off" AutoCompleteType="Disabled"
                onchange="return range();"></asp:TextBox>
            </td>
       
            <td class="copy10grey">Confirm Password:&nbsp;<span class="errormessage">*</span></td><td >
            <asp:TextBox ID="txt_con_pwd" runat="server" MaxLength="16" Width="85%" class="copy10grey"  AutoComplete="Off"
                TextMode="Password"></asp:TextBox>
            
            </td>
        </tr>
        
        
        
        
        <tr valign="top">
            
        
            <td class="copy10grey">Roles:</td><td colspan="4" >
            <asp:CheckBoxList ID="chkRoles" CssClass="copy10grey"  runat="server" RepeatColumns="8"
            RepeatDirection="Horizontal" CellPadding="2" CellSpacing="2"
            TextAlign="Right"  BorderColor="#839abf" 
                BorderStyle="Solid" BorderWidth="1px">
            </asp:CheckBoxList> </td><td> 
                <asp:CheckBox CssClass="copy10grey" ID="chkActive" 
                    Text="Active" runat="server" Checked="True" Visible="False" />
        </td>
        </tr>
         </table>
         
         <asp:Panel ID="pnlStore" runat="server">
                    
                    
                    <table id="tblStore" width="100%" cellspacing="1" cellpadding="1">
                    <tr valign="top">
                        <td class="copy10grey" width="15%">
                        
                         <span id="lbStore">   Store Location:</span>
                        </td>
                        <td colspan="3">
                            <asp:Repeater ID="rptStores" runat="server" >
                            <HeaderTemplate>
                                <table width="600" border="0" cellpadding="2" cellspacing="2">
                                <tr align="left" valign="middle">
                                    <td width="30" class="button">
                                    
                                    </td>
                                    <td width="150" class="button"  ><strong>
                                    StoreID</strong>
                                    </td>
                                    <td width="150" class="button" ><strong>
                                    Store Name</strong>
                                    </td>
                                    <td width="300" class="button" ><strong>
                                    Address</strong>
                                    </td>
                                </tr>
                                
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr align="left" valign="middle">
                                    <td width="30" class="copy10grey">
                                        <asp:HiddenField ID="hdnAddressID" Value='<%# Eval("CompanyAddressID") %>' runat="server" />
                                        <asp:CheckBox  Checked='<%# Convert.ToInt32(Eval("UserStoreFlag"))==0 ? false : true %>' ID="chkStore" runat="server" />
                                        
                                    </td>
                                    <td width="150" class="copy10grey">
                                    <asp:Label ID="lblStoreID" runat="server" Text='<%# Eval("StoreID") %>'></asp:Label>
                                        
                                    </td>
                                    <td width="150" class="copy10grey">
                                        <asp:Label ID="lblStoreName" runat="server" Text='<%# Eval("StoreName") %>'></asp:Label>
                                    </td>
                                    <td width="150" class="copy10grey">
                                        <%# Eval("StoreAddress.Address1")%>&nbsp; <%# Eval("StoreAddress.city")%>&nbsp; <%# Eval("StoreAddress.state")%>&nbsp; <%# Eval("StoreAddress.country")%>&nbsp; <%# Eval("StoreAddress.zip")%>
                                        
                                    
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </table>
                            </FooterTemplate>
                            </asp:Repeater>
                        </td>
                    </tr>
                    </table>
                    
                    </asp:Panel>
         </ContentTemplate>
                </asp:UpdatePanel>
     </td></tr>
            
     
         
     </table>
     
     </td>
     </tr>
     <tr>
        <td>
            &nbsp;
        </td>
     </tr>  
     <tr>
        <td align="center">
            <asp:Button ID="btn_add" runat="server" Text="Submit User" CssClass="button" 
                 OnClientClick="return validate();" onclick="btn_add_Click"/>
               &nbsp;
                   <asp:Button ID="btn_cancel" runat="server" CssClass="button" 
                Text="Cancel" onclick="btn_cancel_Click" CausesValidation="false" />
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
         <script type="text/javascript">
             var usertype = document.getElementById('hdnuserType').value;
             if (usertype == 'Aerovoice') {

                 var trCustomer = document.getElementById('trCustomer');
                 var company = document.getElementById("<% =ddlcompany.ClientID %>");
                 var lblcompany = document.getElementById('spCompany');
                 var lblcompanyAC = document.getElementById('spCompanyAccount');
                 company.style.display = "none";
                 lblcompany.style.display = "none";
                 trCustomer.style.display = "none";
                              }

         </script>
      
        
    </form>
</body>
</html>
