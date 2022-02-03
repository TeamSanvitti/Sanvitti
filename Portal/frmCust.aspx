<%@ Page language="c#" Codebehind="frmCust.aspx.cs" AutoEventWireup="false" Inherits="avii.frmCust" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="./Controls/Footer.ascx" %>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="./Controls/Header.ascx" %>
<%@ Register TagPrefix="serv" TagName="MenuSP" Src="./Controls/provider.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<title>Registration</title>
		<LINK href="aerostyle.css" type="text/css" rel="stylesheet">
			<script language="javascript" src="avI.js" type="text/javascript"></script>
			<script language="javascript"  type="text/javascript">
			function fnTha()
			{
				alert('Thank you for registering with Aerovoice.com !!\n\n You can login to website to view cellphones and accessories.');
				//location.search = './logon.aspx' ;
				window.navigate('./logon.aspx');
			}
			function fnTha1()
			{
				alert('Your registration information has been changed !!');
			}
			function fnValidate()
			{
				var smsg = '';
				if (document.all.txtUser) {
					if (document.all.txtUser.value.length == 0)
						smsg = smsg +  '\n - Username';
					if (document.all.txtPwd.value.length == 0)
						smsg = smsg +  '\n - Password';
					if (document.all.txtCPwd.value.length == 0)
						smsg = smsg +  '\n - Confirm Password';
				}
				if (document.all.txtFirstName.value.length == 0)
					smsg = smsg +  '\n - First Name';
				if (document.all.txtBillFirstName.value.length == 0)
					smsg = smsg +  '\n - Bill First Name';
				if (document.all.txtLastName.value.length == 0)
					smsg = smsg +  '\n - Last Name';
				if (document.all.txtBillLastName.value.length == 0)
					smsg = smsg +  '\n - Bill Last Name';
				//if (document.all.txtMiddleName.value.length == 0)
				//	smsg = smsg +  '\n - Middle Name';
				//if (document.all.txtBillMiddleName.value.length == 0)
				//	smsg = smsg +  '\n - Bill Middle Name';
				if (document.all.txtAddress1.value.length == 0)
					smsg = smsg +  '\n - Address1';
				if (document.all.txtBillAddress1.value.length == 0)
					smsg = smsg +  '\n - Bill Address1';
				if (document.all.txtCity.value.length == 0)
					smsg = smsg +  '\n - City';
				if (document.all.txtBillCity.value.length == 0)
					smsg = smsg +  '\n - Bill City';
					
				if (document.all.dpState.selectedIndex == 0)
					smsg = smsg +  '\n - State';
				if (document.all.dpBillState.selectedIndex == 0)
					smsg = smsg +  '\n - Bill State';
				if (document.all.txtZip.value.length == 0)
					smsg = smsg +  '\n - Zip';
				if (document.all.txtBillZip.value.length == 0)
					smsg = smsg +  '\n - Bill Zip';
				if ((document.all.txtOfficePhone.value.length > 0) )
				{
					if (!isPhoneNumber(document.all.txtOfficePhone.value))
						smsg = smsg + '\n- Invalid Office Number format: 999-999-9999';
				}
				else
					smsg = smsg +  '\n - Office phone';
				
				if (document.all.txtEmail.value.length == 0)
					smsg = smsg +  '\n - Email';					
					else
					{
					if (!isEmail(document.all.txtEmail.value))
						smsg = smsg + '\n- Invalid Email format';
					}					
			
				if (document.all.txtStores.value.length == 0)
					smsg = smsg +  '\n - Stores';
				if (document.all.dpSP.selectedIndex == 0)
					smsg = smsg +  '\n - Service Provider';
				if (smsg.length > 0)
				 {
					alert('Please enter following values:' + smsg);
					return false;
				 }
				else
				{
					if (document.all.txtPwd.value != document.all.txtCPwd.value)
						{alert ('Password and Confirm Password should be same');
						return false;
						}
					else
						return true;			
				}

			}
			</script>
	</head>
	<body leftMargin="0" topMargin="0" rightMargin="0">
		<form id="cust" method="post" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" align="center" border="0">
				<TR>
					<TD vAlign="top"><head:menuheader id="MenuHeader1" runat="server"></head:menuheader></TD>
				</TR>
				<tr>
					<td vAlign="top">
						<table cellSpacing="1" cellPadding="1" width="100%" border="0">
							<TR>
								<TD    colSpan="6">&nbsp;&nbsp;
								</TD>
							</TR>
							<TR>
								<TD colSpan="6" bgcolor="#dee7f6" class="button">&nbsp;&nbsp;Registration
								</TD>
							</TR>
							<TR>
								<TD   colSpan="6">&nbsp;&nbsp;
								</TD>
							</TR>
							<TR>
								<TD class="linkgrid" colSpan="6">&nbsp;&nbsp;ADDRESS(S)</TD>
							</TR>
							<tr>
								<td colSpan="6">
									<table cellSpacing="0" cellPadding="0" width="99%" align="center" borderColorLight="#839abf"
										border="1">
										<tr borderColor="#839abf">
											<td align="center">
												<table width="100%">
													<TBODY>
														<TR>
															<TD class="linkgrid" align="center" colSpan="3"><b>Shipping Address</b></TD>
															<TD class="linkgrid" align="center" colSpan="3"><b>Billing Address</b></TD>
														</TR>
														<TR>
															<TD class="copy10grey" width="15%">FirstName</FONT></TD>
															<td><FONT color="#ff0000">*</FONT></td>
															<TD class="cell-data-td" width="35%"><asp:textbox onkeypress="return fnValueValidate(event,'s');" id="txtFirstName" runat="server"
																	cssclass="txfield1" MaxLength="100"></asp:textbox></TD>
															<TD class="copy10grey" width="15%">FirstName</FONT></TD>
															<td><FONT color="#ff0000" width="1%">*</FONT></td>
															<TD class="cell-data-td" width="35%"><asp:textbox onkeypress="return fnValueValidate(event,'s');" id="txtBillFirstName" tabIndex="9"
																	runat="server" cssclass="txfield1" MaxLength="100"></asp:textbox></TD>
														</TR>
														<TR>
															<TD class="copy10grey">Last Name</FONT><FONT color="#ff0000"></FONT></TD>
															<td><FONT color="#ff0000">*</FONT></td>
															<TD class="cell-data-td"><asp:textbox onkeypress="return fnValueValidate(event,'s');" id="txtLastName" tabIndex="1" runat="server"
																	cssclass="txfield1" MaxLength="100"></asp:textbox></TD>
															<TD class="copy10grey">Last Name </FONT></TD>
															<td><FONT color="#ff0000">*</FONT></td>
															<TD class="cell-data-td"><asp:textbox onkeypress="return fnValueValidate(event,'s');" id="txtBillLastName" tabIndex="10"
																	runat="server" cssclass="txfield1" MaxLength="100"></asp:textbox></TD>
														</TR>
														<TR>
															<TD class="copy10grey">MI</FONT></TD>
															<TD class="cell-data-td"></TD>
															<TD class="cell-data-td"><asp:textbox onkeypress="return fnValueValidate(event,'s');" id="txtMiddleName" tabIndex="2"
																	runat="server" cssclass="txfield1" MaxLength="100"></asp:textbox></TD>
															<TD class="copy10grey">MI</FONT></TD>
															<TD class="cell-data-td"></TD>
															<TD class="cell-data-td"><asp:textbox id="txtBillMiddleName" tabIndex="11" runat="server" cssclass="txfield1" MaxLength="100"></asp:textbox></TD>
														</TR>
														<TR>
															<TD class="copy10grey">Address Line 1</FONT><FONT color="#ff0000"></FONT></TD>
															<td><FONT color="#ff0000">*</FONT></td>
															<TD class="cell-data-td"><asp:textbox onkeypress="return fnValueValidate(event,'s');" id="txtAddress1" tabIndex="3" runat="server"
																	cssclass="txfield1" MaxLength="500"></asp:textbox></TD>
															<TD class="copy10grey">Address Line 1</FONT>
															</TD>
															<td><FONT color="#ff0000">*</FONT></td>
															<TD class="cell-data-td"><asp:textbox onkeypress="return fnValueValidate(event,'s');" id="txtBillAddress1" tabIndex="12"
																	runat="server" cssclass="txfield1" MaxLength="500"></asp:textbox></TD>
														</TR>
														<TR>
															<TD class="copy10grey">Address Line 2</FONT>
															</TD>
															<TD class="cell-data-td"></TD>
															<TD class="cell-data-td"><asp:textbox onkeypress="return fnValueValidate(event,'s');" id="txtAddress2" tabIndex="4" runat="server"
																	cssclass="txfield1" MaxLength="500"></asp:textbox></TD>
															<TD class="copy10grey">Address Line 2 </FONT></TD>
															<td><FONT color="#ff0000"></FONT></td>
															<TD class="cell-data-td"><asp:textbox onkeypress="return fnValueValidate(event,'s');" id="txtBillAddress2" tabIndex="13"
																	runat="server" cssclass="txfield1" MaxLength="500"></asp:textbox></TD>
														</TR>
														<TR>
															<TD class="copy10grey">City<FONT color="#ff0000"></FONT></TD>
															<td><FONT color="#ff0000">*</FONT></td>
															<TD class="cell-data-td"><asp:textbox onkeypress="return fnValueValidate(event,'s');" id="txtCity" tabIndex="5" runat="server"
																	cssclass="txfield1" MaxLength="100"></asp:textbox></TD>
															<TD class="copy10grey">City
															</TD>
															<td><FONT color="#ff0000">*</FONT></td>
															<TD class="cell-data-td"><asp:textbox onkeypress="return fnValueValidate(event,'s');" id="txtBillCity" tabIndex="14" runat="server"
																	cssclass="txfield1" MaxLength="100"></asp:textbox></TD>
														</TR>
														<TR>
															<TD class="copy10grey" height="18">State</TD>
															<td height="18"><FONT color="#ff0000">*</FONT></td>
															<TD class="cell-data-td" height="18"><asp:dropdownlist id="dpState" tabIndex="6" runat="server" cssclass="txfield1">
																	<asp:ListItem></asp:ListItem>
																	<asp:ListItem Value="AL">AL</asp:ListItem>
																	<asp:ListItem Value="AK">AK</asp:ListItem>
																	<asp:ListItem Value="AZ">AZ</asp:ListItem>
																	<asp:ListItem Value="AR">AR</asp:ListItem>
																	<asp:ListItem Value="CA" Selected="True">CA</asp:ListItem>
																	<asp:ListItem Value="CO">CO</asp:ListItem>
																	<asp:ListItem Value="CT">CT</asp:ListItem>
																	<asp:ListItem Value="DC">DC</asp:ListItem>
																	<asp:ListItem Value="DE">DE</asp:ListItem>
																	<asp:ListItem Value="FL">FL</asp:ListItem>
																	<asp:ListItem Value="GA">GA</asp:ListItem>
																	<asp:ListItem Value="HI">HI</asp:ListItem>
																	<asp:ListItem Value="ID">ID</asp:ListItem>
																	<asp:ListItem Value="IL">IL</asp:ListItem>
																	<asp:ListItem Value="IN">IN</asp:ListItem>
																	<asp:ListItem Value="IA">IA</asp:ListItem>
																	<asp:ListItem Value="KS">KS</asp:ListItem>
																	<asp:ListItem Value="KY">KY</asp:ListItem>
																	<asp:ListItem Value="LA">LA</asp:ListItem>
																	<asp:ListItem Value="ME">ME</asp:ListItem>
																	<asp:ListItem Value="MD">MD</asp:ListItem>
																	<asp:ListItem Value="MA">MA</asp:ListItem>
																	<asp:ListItem Value="MI">MI</asp:ListItem>
																	<asp:ListItem Value="MN">MN</asp:ListItem>
																	<asp:ListItem Value="MS">MS</asp:ListItem>
																	<asp:ListItem Value="MO">MO</asp:ListItem>
																	<asp:ListItem Value="MT">MT</asp:ListItem>
																	<asp:ListItem Value="NE">NE</asp:ListItem>
																	<asp:ListItem Value="NV">NV</asp:ListItem>
																	<asp:ListItem Value="NH">NH</asp:ListItem>
																	<asp:ListItem Value="NJ">NJ</asp:ListItem>
																	<asp:ListItem Value="NM">NM</asp:ListItem>
																	<asp:ListItem Value="NY">NY</asp:ListItem>
																	<asp:ListItem Value="NC">NC</asp:ListItem>
																	<asp:ListItem Value="ND">ND</asp:ListItem>
																	<asp:ListItem Value="OH">OH</asp:ListItem>
																	<asp:ListItem Value="OK">OK</asp:ListItem>
																	<asp:ListItem Value="OR">OR</asp:ListItem>
																	<asp:ListItem Value="PA">PA</asp:ListItem>
																	<asp:ListItem Value="RI">RI</asp:ListItem>
																	<asp:ListItem Value="SC">SC</asp:ListItem>
																	<asp:ListItem Value="SD">SD</asp:ListItem>
																	<asp:ListItem Value="TN">TN</asp:ListItem>
																	<asp:ListItem Value="TX">TX</asp:ListItem>
																	<asp:ListItem Value="UT">UT</asp:ListItem>
																	<asp:ListItem Value="VT">VT</asp:ListItem>
																	<asp:ListItem Value="VA">VA</asp:ListItem>
																	<asp:ListItem Value="WA">WA</asp:ListItem>
																	<asp:ListItem Value="WV">WV</asp:ListItem>
																	<asp:ListItem Value="WI">WI</asp:ListItem>
																	<asp:ListItem Value="WY">WY</asp:ListItem>
																</asp:dropdownlist></TD>
															<TD class="copy10grey" height="18">State
															</TD>
															<td height="18"><FONT color="#ff0000">*</FONT></td>
															<TD class="cell-data-td" height="18"><asp:dropdownlist id="dpBillState" tabIndex="15" runat="server" cssclass="txfield1">
																	<asp:ListItem></asp:ListItem>
																	<asp:ListItem Value="AL">AL</asp:ListItem>
																	<asp:ListItem Value="AK">AK</asp:ListItem>
																	<asp:ListItem Value="AZ">AZ</asp:ListItem>
																	<asp:ListItem Value="AR">AR</asp:ListItem>
																	<asp:ListItem Value="CA" Selected="True">CA</asp:ListItem>
																	<asp:ListItem Value="CO">CO</asp:ListItem>
																	<asp:ListItem Value="CT">CT</asp:ListItem>
																	<asp:ListItem Value="DC">DC</asp:ListItem>
																	<asp:ListItem Value="DE">DE</asp:ListItem>
																	<asp:ListItem Value="FL">FL</asp:ListItem>
																	<asp:ListItem Value="GA">GA</asp:ListItem>
																	<asp:ListItem Value="HI">HI</asp:ListItem>
																	<asp:ListItem Value="ID">ID</asp:ListItem>
																	<asp:ListItem Value="IL">IL</asp:ListItem>
																	<asp:ListItem Value="IN">IN</asp:ListItem>
																	<asp:ListItem Value="IA">IA</asp:ListItem>
																	<asp:ListItem Value="KS">KS</asp:ListItem>
																	<asp:ListItem Value="KY">KY</asp:ListItem>
																	<asp:ListItem Value="LA">LA</asp:ListItem>
																	<asp:ListItem Value="ME">ME</asp:ListItem>
																	<asp:ListItem Value="MD">MD</asp:ListItem>
																	<asp:ListItem Value="MA">MA</asp:ListItem>
																	<asp:ListItem Value="MI">MI</asp:ListItem>
																	<asp:ListItem Value="MN">MN</asp:ListItem>
																	<asp:ListItem Value="MS">MS</asp:ListItem>
																	<asp:ListItem Value="MO">MO</asp:ListItem>
																	<asp:ListItem Value="MT">MT</asp:ListItem>
																	<asp:ListItem Value="NE">NE</asp:ListItem>
																	<asp:ListItem Value="NV">NV</asp:ListItem>
																	<asp:ListItem Value="NH">NH</asp:ListItem>
																	<asp:ListItem Value="NJ">NJ</asp:ListItem>
																	<asp:ListItem Value="NM">NM</asp:ListItem>
																	<asp:ListItem Value="NY">NY</asp:ListItem>
																	<asp:ListItem Value="NC">NC</asp:ListItem>
																	<asp:ListItem Value="ND">ND</asp:ListItem>
																	<asp:ListItem Value="OH">OH</asp:ListItem>
																	<asp:ListItem Value="OK">OK</asp:ListItem>
																	<asp:ListItem Value="OR">OR</asp:ListItem>
																	<asp:ListItem Value="PA">PA</asp:ListItem>
																	<asp:ListItem Value="RI">RI</asp:ListItem>
																	<asp:ListItem Value="SC">SC</asp:ListItem>
																	<asp:ListItem Value="SD">SD</asp:ListItem>
																	<asp:ListItem Value="TN">TN</asp:ListItem>
																	<asp:ListItem Value="TX">TX</asp:ListItem>
																	<asp:ListItem Value="UT">UT</asp:ListItem>
																	<asp:ListItem Value="VT">VT</asp:ListItem>
																	<asp:ListItem Value="VA">VA</asp:ListItem>
																	<asp:ListItem Value="WA">WA</asp:ListItem>
																	<asp:ListItem Value="WV">WV</asp:ListItem>
																	<asp:ListItem Value="WI">WI</asp:ListItem>
																	<asp:ListItem Value="WY">WY</asp:ListItem>
																</asp:dropdownlist></TD>
														</TR>
														<TR>
															<TD class="copy10grey">Zip<FONT color="#ff0000"></FONT></TD>
															<td><FONT color="#ff0000">*</FONT></td>
															<TD class="cell-data-td"><asp:textbox onkeypress="return fnValueValidate(event,'n');" id="txtZip" tabIndex="7" runat="server"
																	MaxLength="5" cssclass="txfield1"></asp:textbox></TD>
															<TD class="copy10grey">Zip
															</TD>
															<td><FONT color="#ff0000">*</FONT></td>
															<TD class="cell-data-td"><asp:textbox onkeypress="return fnValueValidate(event,'n');" id="txtBillZip" tabIndex="16" runat="server"
																	MaxLength="5" cssclass="txfield1"></asp:textbox></TD>
														</TR>
														<TR>
															<td class="copy10grey">Billing Address</td>
															<td><FONT color="#ff0000"></FONT></td>
															<TD class="cell-data-td"><asp:checkbox id="chkBillingAddress" tabIndex="8" runat="server" AutoPostBack="True" Width="144px"></asp:checkbox></TD>
														</TR>
														<TR>
															<TD colSpan="6"><font class="text10Bold"></font></TD>
														</TR>
													</TBODY></table>
											</td>
										</tr>
									</table>
								</td>
							</tr>
							<TR>
								<TD class="linkgrid" colSpan="4">&nbsp;&nbsp;Contact Information</TD>
							</TR>
							<tr>
								<td colSpan="6">
									<table cellSpacing="0" cellPadding="0" width="99%" align="center" borderColorLight="#839abf"
										border="1">
										<tr borderColor="#839abf">
											<td align="center">
												<table width="100%" border="0">
													<TR>
														<TD class="copy10grey" width="15%">Office Phone</TD>
														<td><FONT color="#ff0000" width="1%">*</FONT></td>
														<TD width="35%"><asp:textbox onkeypress="return fnValueValidate(event,'p');" id="txtOfficePhone" tabIndex="17"
																runat="server" cssclass="txfield1" MaxLength="12"></asp:textbox></TD>
														<TD class="copy10grey" width="15%">
															Mobile</TD>
														<td width="1%"></td>
														<TD class="cell-data-td" width="35%"><asp:textbox onkeypress="return fnValueValidate(event,'p');" id="txtCellPhone" tabIndex="18"
																runat="server" cssclass="txfield1" MaxLength="12"></asp:textbox></TD>
													</TR>
													<TR>
														<TD class="copy10grey">Email Address<FONT color="#ff0000"></FONT></TD>
														<td><FONT color="#ff0000">*</FONT></td>
														<TD class="cell-data-td" colspan="4"><asp:textbox onkeypress="return fnValueValidate(event,'s');" id="txtEmail" tabIndex="19" runat="server"
																cssclass="txfield1" MaxLength="50" Width="70%"></asp:textbox></TD>
													</TR>
													<TR>
														<TD class="copy10grey">No(s). of Store</TD>
														<TD><FONT color="#ff0000">*</FONT></TD>
														<TD class="cell-data-td"><asp:textbox onkeypress="return fnValueValidate(event,'i');" id="txtStores" tabIndex="20" runat="server"
																cssclass="txfield1" MaxLength="4"></asp:textbox></TD>
														<TD class="copy10grey" height="6">Carrier</FONT></TD>
														<td width="1%" height="6"><FONT color="#ff0000">*</FONT></td>
														<TD class="cell-data-td" height="6"><asp:dropdownlist id="dpSP" runat="server" CssClass="txfield1" tabIndex="21"></asp:dropdownlist></TD>
													</TR>
												</table>
											</td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td colSpan="6">
									<table width="100%">

										<asp:panel id="pnlUser" Runat="server">
											<TR>
											    <TD class="linkgrid" colSpan="6">&nbsp;&nbsp;Logon Information
											    </TD>
										    </TR>
											<TR>
												<TD align="center">
													<TABLE cellSpacing="0" cellPadding="0" width="99%" align="center" borderColorLight="#839abf"
														border="1">
														<TR borderColor="#839abf">
															<TD align="center">
																<TABLE width="100%">
																	<TR>
																		<TD class="copy10grey" width="15%">User Name:</FONT>
																		</TD>
																		<TD width="1%"><FONT color="#ff0000">*</FONT></TD>
																		<TD class="cell-data-td" width="84%">
																			<asp:TextBox onkeypress="return fnValueValidate(event,'s');" id="txtUser" tabIndex="22" runat="server"
																				MaxLength="16" CssClass="txfield1"></asp:TextBox></TD>
																	</TR>
																	<TR>
																		<TD class="copy10grey">Password: </FONT></TD>
																		<TD><FONT color="#ff0000">*</FONT></TD>
																		<TD colSpan="2"><INPUT class="txfield1" onkeypress="return fnValueValidate(event,'s');" id="txtPwd" type="password"
																				maxLength="16" name="txtPwd" runat="server">
																		</TD>
																	</TR>
																	<TR>
																		<TD class="copy10grey">Confirm Password: </FONT></TD>
																		<TD><FONT color="#ff0000">*</FONT></TD>
																		<TD colSpan="2"><INPUT class="txfield1" onkeypress="return fnValueValidate(event,'s');" id="txtCPwd" type="password"
																				maxLength="16" name="txtCPwd" runat="server">
																		</TD>
																	</TR>
																</TABLE>
															</TD>
														</TR>
													</TABLE>
												</TD>
											</TR>
										</asp:panel></table>
								</td>
							</tr>
							<TR>
								<TD align="center" colSpan="6"><asp:button id="btnSave" tabIndex="24" runat="server" cssclass="button" Width="216px" Text="Save"></asp:button></TD>
							</TR>
							<tr>
								<td colSpan="4"><asp:label id="lblException" runat="server" CssClass="text8Exception" ForeColor="Red"></asp:label></td>
							</tr>
							<tr>
								<td colSpan="4">
									<INPUT id="hdnCustTypeID" type="hidden" name="hdnCustTypeID" runat="server"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td>
						<serv:MenuSP id="Menusss" runat="server"></serv:MenuSP>
					</td>
				</tr>
				<TR>
					<TD vAlign="top"><foot:menufooter id="Menuheader2" runat="server"></foot:menufooter></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</html>
