<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="customer-Form.aspx.cs" Inherits="avii.Admin.customer_Form"  ValidateRequest="false" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>
<%@ Register TagPrefix="head1" TagName="MenuHeader1" Src="~/Controls/Header.ascx" %>
<%--<%@ Register TagPrefix="menu" TagName="Menu" Src="~/Admin/admHead.ascx" %>--%>
<%@ Register TagPrefix="menu" TagName="Menu" Src="~/dhtmlxmenu/menuControl.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Customer Form</title>
     
     <link href="../../aerostyle.css" rel="stylesheet" type="text/css"/>
     <link href="../../product/ddcolortabs.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript" src="../../product/ddtabmenu.js"></script>
    
     <script language="javascript" type="text/javascript">
  
        function getShippingAddress(obj)
        {
         if(obj.checked)
         {
            document.getElementById("<%=txtShippContactName.ClientID %>").value = document.getElementById("<%=txtOfficeContactName.ClientID %>").value;
            document.getElementById("<%=txtShippAdd1.ClientID %>").value = document.getElementById("<%=txtOfficeAdd1.ClientID %>").value;
            document.getElementById("<%=txtShippAdd2.ClientID %>").value = document.getElementById("<%=txtOfficeAdd2.ClientID %>").value;
            document.getElementById("<%=txtShippOfficePhone1.ClientID %>").value = document.getElementById("<%=txtOfficePhone1.ClientID %>").value;
            document.getElementById("<%=txtShippOfficePhone2.ClientID %>").value = document.getElementById("<%=txtOfficePhone2.ClientID %>").value;
            document.getElementById("<%=txtShippCellPhone.ClientID %>").value = document.getElementById("<%=txtCellPhone.ClientID %>").value;
            document.getElementById("<%=txtShippHomePhone.ClientID %>").value = document.getElementById("<%=txtHomePhone.ClientID %>").value;
            document.getElementById("<%=txtShippCity.ClientID %>").value = document.getElementById("<%=txtOfficeCity.ClientID %>").value;
            document.getElementById("<%=txtShippState.ClientID %>").value = document.getElementById("<%=txtOfficeState.ClientID %>").value;
            document.getElementById("<%=txtShippZip.ClientID %>").value = document.getElementById("<%=txtOfficeZip.ClientID %>").value;
            document.getElementById("<%=txtShippEmail.ClientID %>").value = document.getElementById("<%=txtOfficeEmail1.ClientID %>").value;
            document.getElementById("<%=txtShippEmail2.ClientID %>").value = document.getElementById("<%=txtOfficeEmail2.ClientID %>").value;
            document.getElementById("<%=txtShipCountry.ClientID %>").value = document.getElementById("<%=txtOfficeCountry.ClientID %>").value;
        }
        else
        {
            document.getElementById("<%=txtShippContactName.ClientID %>").value = "";
            document.getElementById("<%=txtShippAdd1.ClientID %>").value = "";
            document.getElementById("<%=txtShippAdd2.ClientID %>").value = "";
            document.getElementById("<%=txtShippOfficePhone1.ClientID %>").value = "";
            document.getElementById("<%=txtShippOfficePhone2.ClientID %>").value = "";
            document.getElementById("<%=txtShippCellPhone.ClientID %>").value = "";
            document.getElementById("<%=txtShippHomePhone.ClientID %>").value = "";
            document.getElementById("<%=txtShippCity.ClientID %>").value = "";
            document.getElementById("<%=txtShippState.ClientID %>").value = "";
            document.getElementById("<%=txtShippZip.ClientID %>").value = "";
            document.getElementById("<%=txtShippEmail.ClientID %>").value = "";
            document.getElementById("<%=txtShippEmail2.ClientID %>").value = "";
            document.getElementById("<%=txtShipCountry.ClientID %>").value = "";
        }
                                
                                
                                
                                
                                
                                
                                
                                
                                
                                
                                
                                
        }
        function IsValidate()
        {
            
            var CompanyName = document.getElementById("txtCompanyName");
            var email = document.getElementById("txtEmail");
            var ShortName = document.getElementById("txtShortName");
            
            var CompanyAccount = document.getElementById("txtCompanyAccount");
            
            var CompanyType=document.getElementById("ddlCompanyType").selectedIndex;
            
            if (CompanyName.value == "") {
                alert("CompanyName can not be blank");
                CompanyName.focus();
                return false;
            }
            if (email.value == "") {
                alert("Email can not be blank");
                email.focus();
                return false;
            }
            if (ShortName.value == "") {
                alert("Short Name can not be blank");
                ShortName.focus();
                return false;
            }
            if (CompanyAccount.value == "") {
                
                alert("CompanyAccount # can not be blank");
                CompanyAccount.focus();
                return false;
            }
            
            if (CompanyType == "0") {
                alert("Please select a company type");
                CompanyType.focus();
                return false;
            }
            if(document.getElementById("<%=txtOfficeContactName.ClientID %>").value=="")
             {
             alert("Contact Name can not be blank");
             document.getElementById("<%=txtOfficeContactName.ClientID %>").focus();
                return false;
             }
             if(document.getElementById("<%=txtOfficeAdd1.ClientID %>").value=="")
             {
                alert("Address1 can not be blank")
                document.getElementById("<%=txtOfficeAdd1.ClientID %>").fucus();
                return false;
             }
             
             
             if(document.getElementById("<%=txtOfficePhone1.ClientID %>").value=="")
             {
                alert("Phone can not be blank")
                document.getElementById("<%=txtOfficePhone1.ClientID %>").fucus();
                return false;
             }
             if(document.getElementById("<%=txtOfficeCity.ClientID %>").value=="")
             {
                alert("City can not be blank")
                document.getElementById("<%=txtOfficeCity.ClientID %>").fucus();
                return false;
             }
             if(document.getElementById("<%=txtOfficeState.ClientID %>").value=="")
             {
                alert("State can not be blank")
                document.getElementById("<%=txtOfficeState.ClientID %>").fucus();
                return false;
             }
             
             if(document.getElementById("<%=txtOfficeZip.ClientID %>").value=="")
             {
                alert("Zip can not be blank")
                document.getElementById("<%=txtOfficeZip.ClientID %>").fucus();
                return false;
             }
             if(document.getElementById("<%=txtOfficeEmail1.ClientID %>").value=="")
             {
                alert("Email can not be blank")
                document.getElementById("<%=txtOfficeEmail1.ClientID %>").fucus();
                return false;
             }
             
             
            if(document.getElementById("<%=txtShippContactName.ClientID %>").value =="")
            {
                alert("Contact Name can not be blank");
                document.getElementById("<%=txtShippContactName.ClientID %>").focus();
                return false;
            }
            
             
             if(document.getElementById("<%=txtShippAdd1.ClientID %>").value=="")
             {
                alert("Address1 can not be blank")
                document.getElementById("<%=txtShippAdd1.ClientID %>").fucus();
                return false;
             }
             
             
             if(document.getElementById("<%=txtShippOfficePhone1.ClientID %>").value=="")
             {
                alert("Phone can not be blank")
                document.getElementById("<%=txtShippOfficePhone1.ClientID %>").fucus();
                return false;
             }
             
             
             if(document.getElementById("<%=txtShippCity.ClientID %>").value=="")
             {
                alert("City can not be blank")
                document.getElementById("<%=txtShippCity.ClientID %>").fucus();
                return false;
             }
             
             if(document.getElementById("<%=txtShippState.ClientID %>").value=="")
             {
                alert("State can not be blank")
                document.getElementById("<%=txtShippState.ClientID %>").fucus();
                return false;
             }
             
             if(document.getElementById("<%=txtShippZip.ClientID %>").value=="")
             {
                alert("Zip can not be blank")
                document.getElementById("<%=txtShippZip.ClientID %>").fucus();
                return false;
             }
             
             
             if(document.getElementById("<%=txtShippEmail.ClientID %>").value=="")
             {
                alert("Email can not be blank")
                document.getElementById("<%=txtShippEmail.ClientID %>").fucus();
                return false;
             }
                         
             return true;
        }
       
        
     </script>
    <style type="text/css">
        .style1
        {
            width: 326px;
        }
    </style>
</head>
<body bgColor="#ffffff" leftmargin="0" rightmargin="0" topmargin="0"  onkeydown="if (event.keyCode==13) {event.keyCode=9; return event.keyCode }">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
    <table cellSpacing="0" cellPadding="0"  border="0" align="center" width="100%">
				<tr>
					<td>
					<%--<head1:MenuHeader1 ID="menuheader" runat="server"></head1:MenuHeader1>--%>
                    <menu:Menu ID="menu1" runat="server" ></menu:Menu>
					</td>
				</tr>
			</table>
			<table   width="95%" align="center">
                <tr>
			        <td  bgcolor="#dee7f6" class="button">&nbsp;&nbsp;
			            <asp:Label ID="lblHeader" runat="server" CssClass="button" BorderWidth="0" Text="AERVOICE REGISTERED CUSTOMER" ></asp:Label>
			        </td>
                </tr>
                
               <tr><td><asp:Label ID="lblMsg" runat="server" CssClass="errormessage" ></asp:Label>
               <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" CssClass="errormessage"
                ControlToValidate="txtEmail" ErrorMessage="RegularExpressionValidator" 
                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">Invalid Email!!!</asp:RegularExpressionValidator>
               
               <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" CssClass="errormessage"
                ControlToValidate="txtGroupEmail" ErrorMessage="RegularExpressionValidator" 
                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">Invalid Email!!!</asp:RegularExpressionValidator>
               
               </td></tr>
            </table>
            
                        
            
			
            
                        
            <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="95%" align="center" >
                <tr bordercolor="#839abf">
                    <td >
                        
                        <table width="100%">
                        <tr>
                            <td class="copy10grey" width="113" >
                             <span class="errormessage">*</span>Company Name:</td>
                            <td class="style1">
                                <asp:TextBox ID="txtCompanyName"  MaxLength="50" Width="200" TabIndex="1" CssClass="copy10grey" runat="server"></asp:TextBox>
                            </td>
                            <%--<td>
                                <asp:CheckBox ID="chkEnableEmail" TabIndex="2" CssClass="copy10grey" Text="Enable Email" runat="server" />
                            </td>--%>
                            <td class="copy10grey" >
                                <span class="errormessage">*</span>Short Name:</td>
                            <td width="20%">
                                <asp:TextBox ID="txtShortName"   MaxLength="20" CssClass="copy10grey" TabIndex="2" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        
                        
                        <tr>
                            <td class="copy10grey">
                            <span class="errormessage">*</span>Company Type:</td>
                            <td class="style1">
                                <asp:DropDownList ID="ddlCompanyType" TabIndex="3" CssClass="copy10grey" runat="server">
                                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                <asp:ListItem Text="WholeSale" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Retail" Value="2"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td class="copy10grey">
                                Company A/C Status:</td>
                            <td>
                            <asp:DropDownList ID="ddlStatus" TabIndex="4"  CssClass="copy10grey" runat="server">
                            <asp:ListItem Text="Pending" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Approved" Value="2"></asp:ListItem>
                            <asp:ListItem Text="Cancelled" Value="3"></asp:ListItem>
                            <asp:ListItem Text="AllowChanges" Value="4"></asp:ListItem>
                            </asp:DropDownList>
                            </td>
                        </tr>
                         <tr>
                            <td class="copy10grey" width="130" >
                               <span class="errormessage">*</span>Company Account #:
                            </td>
                            <td class="style1">
                                <asp:TextBox ID="txtCompanyAccount"  MaxLength="50" Width="200" TabIndex="5" CssClass="copy10grey" runat="server"></asp:TextBox>
                            </td>
                            
                            <td class="copy10grey" >
                                Website:</td>
                            <td width="20%">
                                <asp:TextBox ID="txtWebsite"  Width="200" MaxLength="50" CssClass="copy10grey" TabIndex="6" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="copy10grey" width="103" >
                               Bussiness Type:</td>
                            <td class="style1">
                                <asp:TextBox ID="txtBusinessType"  MaxLength="150" Width="200" TabIndex="7" CssClass="copy10grey" runat="server"></asp:TextBox>
                            </td>
                            
                            <td class="copy10grey" >
                                
                            </td>
                            <td width="20%">
                                <asp:CheckBox ID="chkActive" Text="Active" TabIndex="8" CssClass="copy10grey"  runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="copy10grey" >
                                <span class="errormessage">*</span>Email:</td>
                            <td class="style1">
                                <asp:TextBox ID="txtEmail" Width="150"  MaxLength="50" TabIndex="9" CssClass="copy10grey" runat="server"></asp:TextBox>
                            </td>
                            
                            <td class="copy10grey" >
                                Sales Person:</td>
                            <td rowspan="2" width="30%">
                                <asp:ListBox ID="lbSalesPerson" TabIndex="10" CssClass="copy10grey" runat="server" Width="90%" SelectionMode="Multiple"></asp:ListBox>
                            
                            </td>
                        </tr>
                        <tr>
                            <td class="copy10grey" >
                                &nbsp;Group Email:</td>
                            <td class="style1">
                                <asp:TextBox ID="txtGroupEmail" Width="200"  MaxLength="50" TabIndex="11" CssClass="copy10grey" runat="server"></asp:TextBox>
                            </td>
                            
                            <td >
                                
                            </td>
                            <%--<td>
                            </td>--%>
                        </tr>
                        </table>
                    </td>
                 </tr>
             </table>  
             <br />
             <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="95%" align="center" >
                <tr bordercolor="#839abf">
                    <td >
             <table width="100%"  >
            <tr>
                <td class="copy10grey">
                    Comments:<br />
                    <asp:TextBox CssClass="copy10grey" TabIndex="37" ID="txtComment" Width="91%" TextMode="MultiLine" Height="40" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="copy10grey">
                    &nbsp;</td>
            </tr>
            </table>
            </td>
            </tr>
            </table>  
            <br />
             <table  bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0"   width="95%" align="center" >
             <tr>
                <td>
                
                
             
             <div id="ddtabs4" class="ddcolortabs">
                    <ul>
                    <li><a href="#" rel="ct1" id="imagetab"><span>Addresses</span></a></li>
                    <li><a href="#" rel="ct2"><span>Store Location</span></a></li>
                    </ul>
                    </div>
                    <div class="ddcolortabsline" >&nbsp;</div>

                    
                
                    <div id="allTabContnt"  >
                
                    <div id="ct1" class="tabcontent" style="border:0px solid #666666">
                    
                   
             
             
             <table bordercolor="#839abf" border="0" cellSpacing="0" cellPadding="0"   width="100%" align="center" >
                <tr>
                <td>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" CssClass="errormessage"
                                ControlToValidate="txtOfficeEmail1" ErrorMessage="RegularExpressionValidator" 
                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">Invalid Email!!!</asp:RegularExpressionValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" CssClass="errormessage"
                                ControlToValidate="txtOfficeEmail2" ErrorMessage="RegularExpressionValidator" 
                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">Invalid Email!!!</asp:RegularExpressionValidator>
                
                </td>
                <td>
                </td>
                <td>
                    <asp:CheckBox ID="chkSameAddess" Text ="Same as corporate office" CssClass="copy10grey" onclick="getShippingAddress(this);" runat="server" />
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" CssClass="errormessage"
                                ControlToValidate="txtShippEmail" ErrorMessage="RegularExpressionValidator" 
                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">Invalid Email!!!</asp:RegularExpressionValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" CssClass="errormessage"
                                ControlToValidate="txtShippEmail2" ErrorMessage="RegularExpressionValidator" 
                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">Invalid Email!!!</asp:RegularExpressionValidator>
                
                </td>
                </tr>
                <tr >
			        <td  width="38%" bgcolor="#dee7f6" class="button">&nbsp;&nbsp;
			        Corporate Office
			        </td>
			        <td width="4%"></td>          
			        <td width="38%" bgcolor="#dee7f6" class="button">
			        &nbsp;&nbsp;Shipping Address
			        </td>
                </tr>
                
               
            </table>    
            <table bordercolor="#839abf" border="0" cellSpacing="0" cellPadding="0" width="100%" align="center" >
                <tr bordercolor="#839abf">
                    <td >
                    <table width="100%">
                    <tr>
                        <td width="48%" >
                            <table width="100%" >
                            <tr>
                                <td class="copy10grey">
                                    <span class="errormessage">*</span>Contact Name:</td>
                                <td>
                                    <asp:TextBox ID="txtOfficeContactName"  MaxLength="50" TabIndex="13" Width="200" CssClass="copy10grey" runat="server"></asp:TextBox>
                                </td>
                            </tr>  
                            
                            <tr>
                                <td class="copy10grey">
                                <span class="errormessage">*</span>Address 1:</td>
                                <td>
                                <asp:TextBox ID="txtOfficeAdd1" Width="200"  MaxLength="50" CssClass="copy10grey" TabIndex="14" runat="server"></asp:TextBox>
                                </td>
                            </tr>    
                            <tr>
                                <td class="copy10grey">
                                Address 2:</td>
                                <td>
                                    <asp:TextBox ID="txtOfficeAdd2" Width="200"  MaxLength="50" TabIndex="15" CssClass="copy10grey" runat="server"></asp:TextBox>
                                </td>
                            </tr>    
                            <tr>
                                <td class="copy10grey">
                                <span class="errormessage">*</span>Office Phone 1:</td>
                                <td>
                                    <asp:TextBox ID="txtOfficePhone1" Width="200"  MaxLength="15" TabIndex="16" CssClass="copy10grey" runat="server"></asp:TextBox>
                                </td>
                            </tr>   
                            <tr>
                                <td class="copy10grey">
                                Office Phone 2:</td>
                                <td>
                                    <asp:TextBox ID="txtOfficePhone2" Width="200"  MaxLength="15" TabIndex="17" CssClass="copy10grey" runat="server"></asp:TextBox>
                                </td>
                            </tr>    
                            <tr>
                                <td class="copy10grey">
                                Cell Phone:</td>
                                <td>
                                    <asp:TextBox ID="txtCellPhone" Width="200"  MaxLength="15" TabIndex="18" CssClass="copy10grey" runat="server"></asp:TextBox>
                                </td>
                            </tr>   
                            <tr>
                                <td class="copy10grey">
                                Home Phone:</td>
                                <td>
                                    <asp:TextBox ID="txtHomePhone" Width="200"  MaxLength="15" TabIndex="19" CssClass="copy10grey" runat="server"></asp:TextBox>
                                </td>
                            </tr>    
                            <tr>
                                <td class="copy10grey">
                                    <span class="errormessage">*</span>City:</td>
                                <td>
                                    <asp:TextBox ID="txtOfficeCity" Width="200"  MaxLength="50" TabIndex="20" CssClass="copy10grey" runat="server"></asp:TextBox>
                                </td>
                            </tr>    
                            <tr>
                                <td class="copy10grey">
                                <span class="errormessage">*</span>State:</td>
                                <td>
                                <asp:TextBox ID="txtOfficeState" Width="200"  MaxLength="2" TabIndex="21" CssClass="copy10grey" runat="server"></asp:TextBox>
                                </td>
                            </tr>    
                            <tr>
                                <td class="copy10grey">
                                <span class="errormessage">*</span>Zip:</td>
                                <td>
                                    <asp:TextBox ID="txtOfficeZip" Width="200"  MaxLength="5" TabIndex="22" CssClass="copy10grey" runat="server"></asp:TextBox>
                                </td>
                            </tr>    
                            
                            <tr>
                                <td class="copy10grey">
                                <span class="errormessage">*</span>Email 1:</td>
                                <td>
                                <asp:TextBox ID="txtOfficeEmail1" Width="200"  MaxLength="50" TabIndex="23" CssClass="copy10grey" runat="server"></asp:TextBox>
                                 
                                </td>
                            </tr>    
                            <tr>
                                <td class="copy10grey">
                                Email 2:</td>
                                <td>
                                <asp:TextBox ID="txtOfficeEmail2" TabIndex="24" MaxLength="50"  Width="200" CssClass="copy10grey" runat="server"></asp:TextBox>
                                </td>
                            </tr> 
                            <tr>
                                <td class="copy10grey">
                                Country:</td>
                                <td>
                                <asp:TextBox ID="txtOfficeCountry" TabIndex="25" MaxLength="100"  Width="200" CssClass="copy10grey" runat="server"></asp:TextBox>
                                </td>
                            </tr>   
                            </table>
                        </td>
                        <td width="4%">
                        </td>
                        <td width="48%">
                        <table width="100%">
                            <tr>
                                <td class="copy10grey">
                                   <span class="errormessage">*</span>Contact Name:</td>
                                <td>
                                    <asp:TextBox ID="txtShippContactName"  MaxLength="50" TabIndex="26" Width="200" CssClass="copy10grey" runat="server"></asp:TextBox>
                                </td>
                            </tr>    
                            <tr>
                                <td class="copy10grey">
                                <span class="errormessage">*</span>Address 1:</td>
                                <td>
                                <asp:TextBox ID="txtShippAdd1" Width="200"  MaxLength="50" TabIndex="27" CssClass="copy10grey" runat="server"></asp:TextBox>
                                </td>
                            </tr>    
                            <tr>
                                <td class="copy10grey">
                                Address 2:</td>
                                <td>
                                    <asp:TextBox ID="txtShippAdd2" Width="200"  MaxLength="50" TabIndex="28" CssClass="copy10grey" runat="server"></asp:TextBox>
                                </td>
                            </tr>    
                            <tr>
                                <td class="copy10grey">
                                <span class="errormessage">*</span>Office Phone 1:</td>
                                <td>
                                    <asp:TextBox ID="txtShippOfficePhone1" Width="200"  MaxLength="15" TabIndex="29" CssClass="copy10grey" runat="server"></asp:TextBox>
                                </td>
                            </tr>   
                            <tr>
                                <td class="copy10grey">
                                Office Phone 2:</td>
                                <td>
                                    <asp:TextBox ID="txtShippOfficePhone2" Width="200"  MaxLength="15" TabIndex="30" CssClass="copy10grey" runat="server"></asp:TextBox>
                                </td>
                            </tr>    
                            <tr>
                                <td class="copy10grey">
                                Cell Phone:</td>
                                <td>
                                    <asp:TextBox ID="txtShippCellPhone" Width="200"  MaxLength="15" TabIndex="31" CssClass="copy10grey" runat="server"></asp:TextBox>
                                </td>
                            </tr>   
                            <tr>
                                <td class="copy10grey">
                                Home Phone:</td>
                                <td>
                                    <asp:TextBox ID="txtShippHomePhone" Width="200"  MaxLength="15" TabIndex="32" CssClass="copy10grey" runat="server"></asp:TextBox>
                                </td>
                            </tr>   
                            <tr>
                                <td class="copy10grey">
                                    <span class="errormessage">*</span>City:</td>
                                <td>
                                    <asp:TextBox ID="txtShippCity" Width="200"  MaxLength="50" TabIndex="33" CssClass="copy10grey" runat="server"></asp:TextBox>
                                </td>
                            </tr>    
                            <tr>
                                <td class="copy10grey">
                                <span class="errormessage">*</span>State:</td>
                                <td>
                                <asp:TextBox ID="txtShippState" Width="200"  MaxLength="2" TabIndex="34" CssClass="copy10grey" runat="server"></asp:TextBox>
                                </td>
                            </tr>    
                            <tr>
                                <td class="copy10grey">
                                <span class="errormessage">*</span>Zip:</td>
                                <td>
                                    <asp:TextBox ID="txtShippZip" Width="200"  MaxLength="5" TabIndex="35" CssClass="copy10grey" runat="server"></asp:TextBox>
                                </td>
                            </tr>    
                            
                            <tr>
                                <td class="copy10grey">
                                <span class="errormessage">*</span>Email 1:</td>
                                <td>
                                <asp:TextBox ID="txtShippEmail" Width="200"  MaxLength="50" TabIndex="36" CssClass="copy10grey" runat="server"></asp:TextBox>
                                </td>
                            </tr>    
                            <tr>
                                <td class="copy10grey">
                                Email 2:</td>
                                <td>
                                <asp:TextBox ID="txtShippEmail2" TabIndex="37" MaxLength="50"  Width="200" CssClass="copy10grey" runat="server"></asp:TextBox>
                                </td>
                            </tr>     
                             <tr>
                                <td class="copy10grey">
                                Country:</td>
                                <td>
                                <asp:TextBox ID="txtShipCountry" TabIndex="38" MaxLength="100"  Width="200" CssClass="copy10grey" runat="server"></asp:TextBox>
                                </td>
                            </tr>     
                            </table>
                    </td>
                </tr>
                        
            </table>  
                    </td>
               </tr>
            </table>        
            
            
            
            
             </div>
                    <div id="ct2" class="tabcontent" style="border:0px solid #666666">
                    
                    
            <table bordercolor="#839abf" border="0" cellSpacing="0" cellPadding="0" width="100%" align="center" >
                <tr bordercolor="#839abf" bgcolor="navy">
                    <td >
                        <span class="copy11whb">COMPANY STORE LOCATIONS</span>
                    </td>
                    <td align="right">
                        <asp:Button ID="btnAddStoreID"  runat="server" TabIndex="38" Text="Add New Store ID" 
                            CssClass="buttongray" onclick="btnAddStoreID_Click" CausesValidation="false" />
                    </td>
               </tr>
          </table>     
          <%--<br />   --%>        
          
            
          <asp:UpdatePanel ID="UpdatePanel2" runat="server" ChildrenAsTriggers="true">
                                        
            <ContentTemplate>
            <asp:Repeater ID="rptStore" runat="server">
    <HeaderTemplate>
    
          <table  cellSpacing="6" cellPadding="5" width="100%" align="center" >
                <tr bordercolor="#839abf">
                    <td class="button" >
                    Store ID
                    </td>
                    <td class="button">
                    Address
                    </td>
                    <td class="button">
                    City
                    </td>
                    <td class="button">
                    State
                    </td>
                    <td class="button">
                    Country
                    </td>
                    <td class="button">
                    Zip
                    </td>
                    <td class="button">
                    Active
                    </td>
               </tr>
               </HeaderTemplate>
                <ItemTemplate>
    
               <tr >
                    <td  >
                        <asp:TextBox ID="txtStoreID" MaxLength="20" CssClass="copy10grey" Text='<%# Eval("storeID") %>' runat="server"></asp:TextBox>
                    </td>
                    <td >
                        <asp:TextBox ID="txtAddress" MaxLength="50" CssClass="copy10grey" Text='<%# Eval("StoreAddress.address1") %>' runat="server"></asp:TextBox>
                    </td>
                    <td >
                        <asp:TextBox ID="txtCity" MaxLength="50" CssClass="copy10grey" Text='<%# Eval("StoreAddress.city") %>'  runat="server"></asp:TextBox>
                    </td>
                    <td >
                        <asp:TextBox ID="txtState" MaxLength="2" CssClass="copy10grey" Text='<%# Eval("StoreAddress.state") %>'  runat="server"></asp:TextBox>
                    </td>
                    <td >
                        <asp:TextBox ID="txtcountry1" MaxLength="50" CssClass="copy10grey" Text='<%# Eval("StoreAddress.country") %>'  runat="server"></asp:TextBox>
                    </td>
                    <td >
                        <asp:TextBox ID="txtZip" Width="60" MaxLength="5" CssClass="copy10grey" Text='<%# Eval("StoreAddress.zip") %>'  runat="server"></asp:TextBox>
                    </td>
                    <td >
                        <asp:CheckBox ID="chkActive1" Checked='<%# Eval("active") %>' runat="server" />
                    </td>
               </tr>
               
    </ItemTemplate>
    <FooterTemplate>
    </table>  
    </FooterTemplate>
    </asp:Repeater>         
                  
            </ContentTemplate>
            <Triggers >
            <asp:AsyncPostBackTrigger ControlID="btnAddStoreID" EventName="Click" />
            
            </Triggers>
           </asp:UpdatePanel>
           
           </div>
           </div>
             </td>
          </tr>
          </table>
          <table width="80%" align="center" >
              
                <tr>
			        <td align="center">
			        <br />
			            <asp:Button ID="btnSubmit" runat="server" CssClass="button"  OnClientClick="return IsValidate();"
                                        Text="   Submit   " onclick="btnSubmit_Click" />&nbsp;&nbsp;
                        <asp:Button ID="btnCancel" runat="server" CssClass="button" Text="   Cancel   " 
                            onclick="btnCancel_Click" />
                       <%--<asp:Button ID="btnBach" runat="server" CssClass="button" Text="Cancel" 
                            onclick="btnCancel_Click" />  --%>
			        </td>
			        
			    </tr>
			    </table>    
          <table width="100%" align="center">
        <tr><td>&nbsp;</td></tr>
				<tr>
            <td><foot:MenuFooter id="Footer" runat="server"></foot:MenuFooter></td>
          </tr>
				        
        </table>
    </form>
    <script type="text/javascript">
    //SYNTAX: ddtabmenu.definemenu("tab_menu_id", integer OR "auto")
       
    ddtabmenu.definemenu("ddtabs4", 0) //initialize Tab Menu #4 with 3rd tab selected

    
    </script>
</body>
</html>
  </tr>
			    </table>    
          <table width="100%" align="center">
        <tr><td>&nbsp;</td></tr>
				<tr>
            <td><foot