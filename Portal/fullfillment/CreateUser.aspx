<%@ Page Language="C#" AutoEventWireup="true" Inherits="Admin_CreateUser" Codebehind="CreateUser.aspx.cs" %>

<%@ Register TagPrefix="head" TagName="MenuHeader" Src="/admin/admhead.ascx" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <link href="../aerostyle.css" rel="stylesheet" type="text/css"></link>
    <script language="javascript" src="../avI.js" type="text/javascript"></script>
    			<script language="javascript">
	function KeyDownHandler(btn)
    {

       if (event.keyCode == 13)
        {
            event.returnValue=false;
            event.cancel = true;
            btn.click();
        }
    }
			</script>    
</head>
<body leftmargin="0" topmargin="0" marginwidth="0" marginheight="0" onkeydown="KeyDownHandler(document.all.btnLogin)">
    <form id="form1" runat="server" method="post" >
        <table border="0">
        	<tr>
				<td><head:menuheader id="MenuHeader1" runat="server"></head:menuheader></td>
			</tr>
			<tr><td>&nbsp;</td></tr>
			<TR>
				<TD class="button">&nbsp;&nbsp;Registration
				</TD>
			</TR>
            <tr><td>&nbsp;</td></tr>			
            <tr>
                <td align="center"  valign=top>
                        <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="55%">
                         <tr bordercolor="#839abf">
		                    <td>
									<TABLE id="Table2" cellSpacing="5" cellPadding="5" width="100%" border="0">
							
                            <tr>
                                <td colspan="4">
                                    <asp:Label ID="lblMsg" runat="server" CssClass="errormessage"></asp:Label></td>
                             </tr>                        
		                    <tr bordercolor="#839abf">
                                <td class="copy10grey" align="right">Username</td>
                                <td class="errormessage">*</td>
                                <td>
                                    <asp:TextBox ID="txtUsername" runat="server" OnTextChanged="txtUsername_TextChanged" CssClass="txfield1"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Enter Username"
                                        ControlToValidate="txtUsername" CssClass="errormessage" ForeColor=""></asp:RequiredFieldValidator></td>
                            </tr>
                            <tr id="Tr5" runat="server" style="color: #000000">
                                <td class="copy10grey" align="right" >
                                    Password</td>
                                <td class="errormessage">*</td>    
                                <td>
                                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" Width="149px" CssClass="txfield1"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Enter Password"
                                        ControlToValidate="txtPassword" CssClass="errormessage" ForeColor=""></asp:RequiredFieldValidator></td>
                            </tr>
                            <tr id="Tr6" runat="server" style="color: #000000">
                                <td class="copy10grey"  align="right">
                                    Confirm Password</td>
                                <td class="errormessage">*</td>
                                <td>
                                    <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" Width="149px" CssClass="txfield1"></asp:TextBox></td>
                                <td>
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtPassword"
                                        ControlToValidate="txtConfirmPassword" ErrorMessage="Passwords do not match" CssClass="errormessage" ForeColor=""></asp:CompareValidator></td>
                            </tr>
                            <tr id="Tr7" runat="server">
                                <td class="copy10grey"  align="right">
                                    Email Address</td>
<td class="errormessage">*</td>                                    
                                <td>
                                    <asp:TextBox ID="txtEmail" runat="server" Width="149px" CssClass="txfield1"></asp:TextBox></td>
                                <td>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtEmail"
                                        ErrorMessage="Enter Email" CssClass="errormessage" ForeColor=""></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmail"
                                        ErrorMessage="Invalid Email" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" CssClass="errormessage" ForeColor=""></asp:RegularExpressionValidator></td>
                            </tr>
                            <tr id="Tr8" runat="server">
                                <td class="copy10grey" align="right">
                                    Company Name</td>
                                <td class="errormessage">*</td>
                                <td>
                                    <asp:TextBox ID="txtCompanyName" runat="server" Width="149px" CssClass="txfield1"></asp:TextBox></td>
                                <td>
                                </td>
                            </tr>
                              <tr>
                                <td colspan="4">
                                    &nbsp;</td>
                             </tr>   
                            <tr>
                                <td colspan="4" align="center">
                                    <asp:Button ID="btnLogin" Width="190px" runat="server" CssClass="button" Text="Create User" OnClick="btnLogin_Click" />
                                </td>
                            </tr>
                    </table>
                    </td>
                    </tr>
                    </table>
                    </td>
                </tr>                
            </table>
    </form>
</body>
</html>
