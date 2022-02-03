<%@ Page language="c#" Codebehind="Logon.aspx.cs" AutoEventWireup="false" Inherits="avii.Logon" ValidateRequest="false" %>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="Controls/Header.ascx" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="Controls/Footer.ascx" %>
<%@ Register TagPrefix="serv" TagName="MenuSP" Src="Controls/provider.ascx" %>
<HTML>
	<HEAD>
	<meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
        <title>LAN Global Inc.</title>
        <!-- Bootstrap CSS -->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css">
	<link href="https://maxcdn.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" rel="stylesheet">
	<link rel="stylesheet" href="css/stylenew.css">
	
	<link rel="stylesheet" href="css/animate.min.css">
		<link href="aerostyle.css" rel="stylesheet" type="text/css">
        <style>
        .alignleft
        {
            float:right !important; 
            width:600px !important; 
        }
        </style>
		<div id="Div1" runat="server">
        	<script language="javascript" src="avI.js"></script>
			<script language="javascript">
			    function KeyDownHandler(btn) {

			        if (event.keyCode == 13) {
			            event.returnValue = false;
			            event.cancel = true;
			            btn.click();
			        }
			    }
			</script>
       </div>

        <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
<script type="text/javascript" src="https://www.google.com/recaptcha/api.js?onload=onloadCallback&render=explicit" asyncdefer></script>
		<script type="text/javascript">
            var onloadCallback = function () {
                grecaptcha.render('dvCaptcha', {
                    'sitekey': '<%=ReCaptcha_Key %>',
                    'callback': function (response) {
                        $.ajax({
                            type: "POST",
                            url: "logon.aspx/VerifyCaptcha",
                            data: "{response: '" + response + "'}",
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (r) {
                                var captchaResponse = jQuery.parseJSON(r.d);
                                if (captchaResponse.success) {
                                    $("[id*=txtCaptcha]").val(captchaResponse.success);
                                    $("[id*=rfvCaptcha]").hide();
                                } else {
                                    $("[id*=txtCaptcha]").val("");
                                    $("[id*=rfvCaptcha]").show();
                                    var error = captchaResponse["error-codes"][0];
                                    $("[id*=rfvCaptcha]").html("RECaptcha error. " + error);
                                }
                            }
                        });
                    }
                });
            };
        </script>
	</HEAD>
	<body leftmargin="0" topmargin="0" marginwidth="0" marginheight="0" onkeydown="KeyDownHandler(document.all.btnlogon)">
		<form id="Form1" method="post" runat="server">
            <table cellspacing="0" cellpadding="0"  border="0"  width="100%">
	<tr>
	    <td> <head:menuheader id="MenuHeader1" runat="server"></head:menuheader>
		</td>
	</tr>
    </table>
        <div id="winVP">
			
                   
					
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
  <tr>
    <td bgcolor="#CCCCCC" valign="middle" style="text-align-last:right; font-size:36px; color:white; font-family:Arial, Helvetica, sans-serif; height:164px; background-image:url('images/bandimage.jpg')">
       
        <table width="1300" cellspacing="0" cellpadding="0">
            <tr>
                <td style="text-align:right;  font-weight:bold; font-size:24px; color:white; font-family:Arial, Helvetica, sans-serif">
                     CUSTOMER LOGIN
                </td>
                <td style="text-align-last:right; font-size:36px; color:white; font-family:Arial, Helvetica, sans-serif; width:150px">&nbsp;</td>
            </tr>

        </table>
       
        <%--<table width="1300" border="0" align="center" cellpadding="0" cellspacing="0">
      <tr>
        <td rowspan="2" align="left" valign="middle" bgcolor="#007fc1"><table width="100%" border="0" cellspacing="8" cellpadding="8">
          <tr>
            <td class="whitetx1"><p>        Sign In to the portal<br /></p>

              
            
              <p></p></td>
          </tr>

        </table></td>
        <td height="55" align="center" valign="middle" bgcolor="#999999"><span class="whitehd">Sign In</span></td>
      </tr>
      <tr>
        <td width="300" valign="top"><img src="images/fulfillment.jpg" width="300" height="172"></td>
      </tr>
    </table>--%>


    </td>
  </tr>
</table>

                <table cellSpacing="0" cellPadding="0" width="97%" border="0" align="center">
				<TR>
					<TD align="center">
                    
						<br /><br /><br />
						
						<table bordercolor="#839abf" border="0" cellSpacing="0" cellPadding="0" width="45%">
							<tr bordercolor="#839abf">
                                <td style="width:35%">
                                    <TABLE id="Table2" cellSpacing="5" cellPadding="5" width="100%" border="0">
                                        <tr>
                                            <td>
                                                <img alt="" src="images/lockicon.jpg" />
                                            </td>
                                        </tr>
                                    </TABLE>
                                </td>
                                <td style="width:10%">
                                    &nbsp;
                                </td>
								<td style="width:55%">
									<table id="Table2" cellSpacing="5" cellPadding="5" width="100%" border="0">
										<tr>
											<td class="copy10grey" align="left">Username:</td>
											<td></td>
											<td>
												<asp:TextBox id="txtUser" style="width:50%" onkeypress="return fnValueValidate(event,'ns');" runat="server" CssClass="txfield1"></asp:TextBox></td>
										</tr>
										<tr>
											<td class="copy10grey" width="29%" align="left">Password:</td>
											<td width="1%"></td>
											<td width="70%"><input style="width:50%" type="password" onkeypress="return fnValueValidate(event,'ns');" id="txtPwd" maxLength="16"
													class="txfield1" name="txtPwd" runat="server"></td>
										</tr>
										<tr>
										    <TD class="copy10grey" width="39%" align="right">
										        
										    </TD>
											<TD width="1%"></TD>
											<TD width="60%">
                                                <div id="dvCaptcha" >
                                                </div>
                                                <asp:TextBox ID="txtCaptcha" runat="server" Style="display: none" />
                                                <asp:RequiredFieldValidator ID="rfvCaptcha" ErrorMessage="Captcha validation is required." 
                                                    ControlToValidate="txtCaptcha"
                                                    runat="server" ForeColor="Red" Display="Dynamic" />

											</td>
										</tr>

										<tr>
                                                <TD class="copy10grey" width="29%" align="right">&nbsp;</TD>
											<TD width="1%"></TD>
											<TD  align="left">
												<asp:Button id="btnlogon" CssClass="button" runat="server" Text="Logon" Width="100px" OnClick="btnlogon_Click1" 
                                                OnClientClick="return validateUser();"></asp:Button></TD>
										</tr>

										<tr >
                                             <TD class="copy10grey" width="29%" align="right">&nbsp;</TD>
											<TD width="1%"></TD>
											<td height="20" class="copy10grey"  align="left"><a href="ForgotPassword.aspx" class="copy10grey">Forgot 
													password?</a> </td>
										</tr>									
										<tr >
                                             <TD class="copy10grey" width="29%" align="right">&nbsp;</TD>
											<TD width="1%"></TD>
											<td height="20" class="copy10grey"  align="left"><a href="signup.aspx" class="copy10grey">Sign 
													Up</a> </td>
										</tr>									
                                        </table>
								</td>
							</tr>
						</table>
						<br /><br /><br />
						
					</TD>
				</TR>
				<TR>
					<TD>
						</TD>
				</TR>
			</TABLE>
            <foot:MenuFooter id="Menuheader2" runat="server"></foot:MenuFooter>
            </div>

		</form>
	</body>
</HTML>
