<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="./Controls/Footer.ascx" %>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="./Controls/Header.ascx" %>
<%@ Register TagPrefix="serv" TagName="MenuSP" Src="./Controls/provider.ascx" %>
<%@ Page language="c#" Codebehind="frmForget.aspx.cs" AutoEventWireup="false" Inherits="avii.frmForget" %>
<HTML>
	<HEAD>
		<title>Forget Password</title>
		<LINK href="aerostyle.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="avI.js"></script>
	</HEAD>
	<body leftmargin="0" topmargin="0" marginwidth="0" marginheight="0">
		<form id="Form1" method="post" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" width="780" border="0" align="center">
				<TR>
					<TD><head:menuheader id="MenuHeader1" runat="server"></head:menuheader></TD>
				</TR>
				<TR>
					<TD>
						<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
							<TR>
								<TD>&nbsp;</TD>
							</TR>
							<TR>
								<TD align="center">
									<br>
									<br>
									<table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="55%">
										<tr bordercolor="#839abf">
											<td>
												<TABLE id="Table2" cellSpacing="5" cellPadding="5" width="100%" border="0">
													<TR>
														<TD class="copy10grey" align="right">Username:</TD>
														<TD></TD>
														<TD>
															<asp:TextBox id="txtUser" runat="server" CssClass="txfield1"></asp:TextBox></TD>
													</TR>
													<TR>
														<TD class="copy10grey" width="39%" align="right">Email:</TD>
														<TD width="1%"></TD>
														<TD width="60%">
															<asp:TextBox id="txtEmail" runat="server" CssClass="txfield1"></asp:TextBox></TD>
													</TR>
													<tr>
													    <td colspan="3" align=center>
                                                            <asp:CheckBox ID="chkFull" runat="server" CssClass="copy10grey" Text="Fullfillment Password" /></td>
													</tr>
													<TR>
														<TD colspan="3" align="center">
															<asp:Button id="btnSubmit" CssClass="button" runat="server" Text="Get New Password" Width="190px" OnClick="btnSubmit_Click1"></asp:Button></TD>
													</TR>
												</TABLE>
											</td>
										</tr>
									</table>
									<br>
									<br>
									<br>
								</TD>
							</TR>
							<tr>
								<td>
								<serv:MenuSP id="Menusss" runat="server"></serv:MenuSP>
								</td>
							</tr>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD vAlign="top"><foot:menufooter id="Menuheader2" runat="server"></foot:menufooter></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
