<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" Inherits="avii.ForgotPassword" %>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="Controls/Header.ascx" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="Controls/Footer.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Lan Global inc. Inc. - Complete Wireless</title>
    <link rel="stylesheet" href="css/stylenew.css">
	
	<link rel="stylesheet" href="css/animate.min.css">
	
		<link href="aerostyle.css" rel="stylesheet" type="text/css" />
        <script language="javascript" src="avI.js" type="text/javascript"></script>
     <style>
        .alignleft
        {
            float:right !important; 
            width:600px !important; 
        }
        </style>
		
    <script type="text/javascript">
        function isNumberKey(evt) {

            var charCodes = evt.keyCode ? evt.keyCode : evt.which ? evt.which : evt.charCode;
            if (charCodes > 31 && (charCodes < 48 || charCodes > 57)) {
                charCodes = 0;
                return false;
            }
            return true;
        }
        function ValidatePassword(e) {
            var regex = new RegExp("^[a-zA-Z0-9-*._]+$");
            var str = String.fromCharCode(!e.charCode ? e.which : e.charCode);
            if (str == '&' || str == '%' || str == '$') {
                e.preventDefault();
                return false;
            }
            //alert(str);
            if (regex.test(str)) {
                return true;
            }

            e.preventDefault();
            return false;
        }
        function ValidateEmail(obj) {
            var EmailAddresses = obj.value;

            var emails = EmailAddresses.split(',');
            var EmaiAddress;
            for (var i = 0; i < emails.length; i++) {
                EmaiAddress = emails[i];
                var RegExEmail = /^(?:\w+\.?)*\w+@(?:\w+\.)+\w+$/;
                if (obj.value != '') {
                    if (!RegExEmail.test(EmaiAddress)) {
                        obj.focus();
                        alert("Invalid E-mail");
                        return false;
                    }
                }
            }
        }
        function alphanumeric(e) {

            var regex = new RegExp("^[a-zA-Z0-9_]+$");
            var str = String.fromCharCode(!e.charCode ? e.which : e.charCode);


            //alert(regex.test(str));
            if (regex.test(str)) {
                return true;
            }
            else {
                e.preventDefault();
                return false;
            }
        }
    </script>
</head>
<body leftmargin="0" topmargin="0" marginwidth="0" marginheight="0" >
    <form id="form1" runat="server">
    <table cellspacing="0" cellpadding="0"  border="0"  width="100%">
	<tr>
	    <td>
    <head:menuheader id="MenuHeader1" runat="server"></head:menuheader>
        			</td>
	</tr>
    </table>
  <table width="100%" border="0" cellspacing="0" cellpadding="0">
  <tr>
    <td bgcolor="#CCCCCC" valign="middle" style="text-align-last:right; font-size:36px; color:white; font-family:Arial, Helvetica, sans-serif; height:164px; background-image:url('images/bandimage.jpg')">
       
        <table width="1300" cellspacing="0" cellpadding="0">
            <tr>
                <td style="text-align:right;  font-weight:bold; font-size:24px; color:white; font-family:Arial, Helvetica, sans-serif">
                   <span ID="lblHeader"   runat="server" style="text-align:right;  font-weight:bold; font-size:24px; color:white; font-family:Arial, Helvetica, sans-serif">
                       </span>
                    
                </td>
                <td style="text-align-last:right; font-size:36px; color:white; font-family:Arial, Helvetica, sans-serif; width:150px">&nbsp;</td>
            </tr>

        </table>
       
      

    </td>
  </tr>
</table>
    	
              <%--  <table width="100%" border="0" cellspacing="0" cellpadding="0">
  <tr>
    <td bgcolor="#CCCCCC"><table width="1300" border="0" align="center" cellpadding="0" cellspacing="0">
      <tr>
        <td rowspan="2" align="left" valign="middle" bgcolor="#007fc1"><table width="100%" border="0" cellspacing="8" cellpadding="8">
          <tr>
            <td class="whitetx1"><p>        Change password<br /></p>

              
            
              <p></p></td>
          </tr>

        </table></td>
        <td height="55" align="center" valign="middle" bgcolor="#999999"><span class="whitehd">Forgot Password</span></td>
      </tr>
      <tr>
        <td width="300" valign="top"><img src="images/fulfillment.jpg" width="300" height="172"></td>
      </tr>
    </table></td>
  </tr>
</table>--%>

   <table cellSpacing="0" cellPadding="0" width="55%" border="0" align="center">
				<tr>
					<td>
					</td>
				</tr>
				<tr>
					<td align="center"  valign="top">
						  <br /><br /><br /><br />
						<br />
						<br />
                        
						
                        <table width="100%">
                        <tr>
                            <td align="left">
                                <asp:Label ID="lblMsg" CssClass="errormessage" runat="server"></asp:Label>
                        
                            </td>
                        </tr>
                        
                        
                        </table>
                       
                        <asp:Panel ID="pnLogin" Visible="true" runat="server" DefaultButton="btnLogin">
                        <table width="100%">
                        <tr>
                            <td align="center">
                            
                        <%--<table cellspacing="0" cellpadding="0" border="0" align="center" style="text-align:left" width="100%">
                            <tr class="buttonlabel" align="left">
                            <td >&nbsp;Login</td></tr>
                        </table>--%>
                     
						<table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="100%">
							<tr bordercolor="#839abf">
								<td>
									<table id="Table3" cellSpacing="5" cellPadding="5" width="100%" border="0">
									    <tr>
                                        <td align="left" >
                                            <asp:Label ID="lblLogin" CssClass="errormessage" runat="server"></asp:Label>
                        
                                        </td>
                                    </tr>
                                         <tr>
                            <td align="center">
                            <br />
                            <asp:Button ID="btnLogin" runat="server" OnClick="btnLogin_Click" CausesValidation="false" CssClass="button" Text="  Login  " />&nbsp; &nbsp; 
                        <br /><br />
                                    </td>
                                </tr>
                            </table>
                                    </td>
                            </tr>
                            </table>
                            </td>
                        </tr>
                            </table>             
                            </asp:Panel>
                        
                        <asp:Panel ID="pnlLogin" runat="server" DefaultButton="btnVerify">
                        <table width="100%">
                        <tr>
                            <td align="center">
                            
<%--                        <table cellspacing="0" cellpadding="0" border="0" align="center" style="text-align:left" width="100%">
                            <tr class="buttonlabel" align="left">
                            <td >&nbsp;Verify Security Code</td></tr>
                        </table>--%>
                    
						<table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="100%">
							<tr bordercolor="#839abf">
								<td>
                                    <br />
									<table id="Table3" cellSpacing="5" cellPadding="5" width="100%" border="0">
									<tr>
                                        <%--<td align="left">
                                            &nbsp;
                                             </td>
                                        </tr>
                                        --%>
                                        <tr>
                                        <td align="center" colspan="3">
                                            
                                            <asp:Label ID="lblMessage" CssClass="copy10grey" runat="server"></asp:Label>
                                            
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="copy10grey" align="right" width="45%"><strong> Security Code:</strong></td>
											<td></td>
											
                                        <td align="left">
                                            <input type="text" onkeypress="return isNumberKey(event);" id="txtSecurityCode" maxlength="6"
													class="txfield1"  runat="server" />
                                        </td>
                                    </tr>
                                        <tr>
                                            <td></td>
											
                                            <td></td>
                                        <td align="left" >
                                             
                                            <asp:Button ID="btnVerify" runat="server" OnClick="btnVerify_Click" CausesValidation="false" CssClass="button" Text=" Verify " />&nbsp; &nbsp; 
                                            <asp:Button ID="btnLogin2" runat="server" OnClick="btnLogin_Click" CausesValidation="false" CssClass="button" Text="  Login  " />
                        <br /><br />
                                        </td>
                                    </tr>
                                    </table>
                                    </td>
                            </tr>
                            </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                          
                            </td>
                        </tr>
                            </table>             
                            </asp:Panel>
                            
                        <asp:Panel ID="pnlChangePwd" runat="server" DefaultButton="btnFgtPassword">
                        <%--<table cellspacing="0" cellpadding="0" border="0" align="center" style="text-align:left" width="100%">
                            <tr class="buttonlabel" align="left">
                            <td >&nbsp;Change Password</td></tr>
                        </table>                    --%>
						<table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="100%">
							<tr bordercolor="#839abf">
								<td>
                                    <br />
									<table id="Table2" cellSpacing="5" cellPadding="5" width="100%" border="0">
										<tr>
											<td class="copy10grey" align="right" ><strong>New Password:</strong></td>
											<td></td>
											<td>
												<input type="password" style="width:200px"  onkeypress="return ValidatePassword(event);" id="txtNewPassword" maxlength="16"
													class="txfield1" name="txtNewPassword" runat="server" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="Password" 
                                                 Text="." ForeColor="white" Display="None" SetFocusOnError="true" ControlToValidate="txtNewPassword" runat="server"  ErrorMessage="Enter new password"></asp:RequiredFieldValidator>
            <%--<asp:RangeValidator ID="rv1" ValidationGroup="Password" MinimumValue="8"  MaximumValue="16"
                                                 Text="." ForeColor="white" Display="None" SetFocusOnError="true" ControlToValidate="txtNewPassword" runat="server"  ErrorMessage="Password must be 8-16 characters"></asp:RangeValidator>
            --%>
                                            </td>
										</tr>
										<tr>
											<td class="copy10grey" width="42%" align="right"><strong>Confirm Password:</strong></td>
											<td width="1%"></td>
											<td ><input type="password" style="width:200px"  onkeypress="return ValidatePassword(event);" id="txtConfirmPwd" maxlength="16"
													class="txfield1" name="txtConfirmPwd" runat="server" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="Password" 
                                                 Text="." ForeColor="white" Display="None" SetFocusOnError="true" ControlToValidate="txtConfirmPwd" runat="server"  ErrorMessage="Enter confirm password"></asp:RequiredFieldValidator>
    
                                                <asp:CompareValidator ID="CompareValidator1" runat="server" 
                                                Text="." ForeColor="white" Display="None" SetFocusOnError="true"
                                                ControlToCompare="txtNewPassword" ControlToValidate="txtConfirmPwd" ValidationGroup="Password" ErrorMessage="Password must match"></asp:CompareValidator>
                                                    </td>
										</tr>
										
										<tr>
                                            <td>

                                            </td>
                                            <td></td>
											<td  align="left">
												<asp:Button id="btnFgtPassword" CssClass="button" runat="server" Text="Submit" ValidationGroup="Password" Width="120px" OnClick="btnFgtPassword_Click"></asp:Button>
                                                &nbsp;<asp:Button ID="btnPwdCancel" CssClass="button" runat="server" Text="Cancel" OnClick="btnPwdCancel_Click" />
                                                 <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="Password"
                                                DisplayMode="List" ShowMessageBox="true" ShowSummary="false" />
                                               <br />
                                                <br />
                                                </td>
										</tr>
										
									</table>
								</td>
							</tr>
						</table>
                        </asp:Panel>
                        <asp:Panel ID="pnlRequestPwd" runat="server" DefaultButton="btnRequestPwd">
                        <%--    <table cellspacing="0" cellpadding="0" border="0" align="center" style="text-align:left" width="100%">
                            <tr class="buttonlabel" align="left">
                            <td >&nbsp;Forgot Password</td></tr>
                        </table>
                    --%>
                            <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="100%">
							<tr bordercolor="#839abf">
								<td>
									<table id="Table1" cellSpacing="5" cellPadding="5" width="100%" border="0">
                                        <tr>
                                            <td colspan="3">
                                                &nbsp;
                                            </td>
                                        </tr>
										<tr>
											<td class="copy10grey" align="right" width="42%"><strong>User Name:</strong></td>
											<td></td>
											<td align="left">
												<asp:TextBox id="txtUserName" Width="200" onkeypress="return alphanumeric(event);" runat="server" MaxLength="20" CssClass="txfield1"></asp:TextBox> 
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="RequestPassword" 
                                                 Text="." ForeColor="white" Display="None" SetFocusOnError="true" ControlToValidate="txtUserName" runat="server"  ErrorMessage="Enter username"></asp:RequiredFieldValidator>
            
                                                 
                                                </td>
										</tr>

                                        <tr>
											<td class="copy10grey" align="right"><strong>Registered email:</strong></td>
											<td></td>
											<td align="left">
												<asp:TextBox id="txtEmail" Width="200" runat="server" CssClass="txfield1" MaxLength="100" onchange = "return ValidateEmail(this);"></asp:TextBox> 
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ValidationGroup="RequestPassword" 
                                                 Text="." ForeColor="white" Display="None" SetFocusOnError="true" ControlToValidate="txtEmail" runat="server"  ErrorMessage="Enter email address"></asp:RequiredFieldValidator>
            
                                                 <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                                                        Text="." ForeColor="white" Display="None" SetFocusOnError="true" ValidationGroup="RequestPassword" ErrorMessage="Invalid email ID!" 
                                                        ControlToValidate="txtEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"  >
                                                        </asp:RegularExpressionValidator>
                                                </td>
										</tr>
										
										
										<tr><td class="copy10grey" align="right"></td>
											<td></td>
											<td align="left">
												<asp:Button id="btnRequestPwd" CssClass="button" runat="server" ValidationGroup="RequestPassword" Text="Submit" Width="120px" OnClick="btnRequestPwd_Click"></asp:Button>
                                                &nbsp;<asp:Button ID="btnCancel" CssClass="button" runat="server" Text="Cancel" OnClick="btnCancel_Click"/>
                                                
                                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="RequestPassword"
                                                DisplayMode="List" ShowMessageBox="true" ShowSummary="false" />
                                                <br /><br />
                                                </td>
										</tr>
                                       <%-- <tr>
                                            <td colspan="3">
                                                &nbsp;
                                            </td>
                                        </tr>
										--%>
										
									</table>
								</td>
							</tr>
						</table>
                        </asp:Panel>
						<br />
                        <br />
						<br />
                        <br />
						<br />
                        
					</td>
				</tr>
				<tr>
					<td>
						</td>
				</tr>
			</table>
            <foot:MenuFooter id="Menuheader2" runat="server"></foot:MenuFooter>
    </form>
</body>
</html>
