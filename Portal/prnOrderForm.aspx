<%@ Page language="c#" Codebehind="prnOrderForm.aspx.cs" AutoEventWireup="false" Inherits="avii.prnOrderForm" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Order Form</title>
		<LINK href="aerostyle.css" type="text/css" rel="stylesheet">
			<script language=javascript src="avI.js"></script>
		
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="95%" border="0">
				<TR>
					<TD colSpan="3"><IMG alt="" src="./images/logo.gif"></TD>
				</TR>
				<TR>
					<TD class="copy10" colSpan="3">8635 Aviation Blvd.,</TD>
				<tr>
				<TR>
					<TD class="copy10" colSpan="3">Inglewood, CA 90301</TD>
				</TR>
				<tr>
					<TD class="copy10" colSpan="3">Phone (310) 649-6490 Fax (310) 649-7288</TD>
				</tr>
				<tr>
					<td>&nbsp;</td>
				</tr>
				<TR>
					<TD width="50%">
						<table borderColor="#839abf" cellSpacing="0" cellPadding="0" width="100%" border="1">
							<tr borderColor="#839abf">
								<td>
									<table borderColor="#839abf" cellSpacing="0" cellPadding="0" width="100%" border="0">
										<tr borderColor="#839abf">
											<td>
												<TABLE id="Table2" cellSpacing="1" cellPadding="1" width="100%" border="0">
													<TR>
														<TD class="label" height="17">
															<P align="right">Date:
															</P>
														</TD>
														<TD height="17"><asp:label id="lblBillDate" runat="server" CssClass="label"></asp:label></TD>
													</TR>
													<TR>
														<TD><FONT face="Arial" size="2"><STRONG>Bill To:</STRONG></FONT></TD>
													</TR>
													<TR>
														<TD colSpan="2"><asp:label id="lblAddr" runat="server" CssClass="label"></asp:label></TD>
													</TR>
													<TR>
														<TD colSpan="2"><asp:label id="lblCity" runat="server" CssClass="label"></asp:label><asp:label id="lblState" runat="server" CssClass="label"></asp:label><asp:label id="lblZip" runat="server" CssClass="label"></asp:label></TD>
													</TR>
												</TABLE>
											</td>
											<TD width="1%"></TD>
											<TD width="49%">
												<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
													<TR>
														<TD colSpan="2">&nbsp;</TD>
													</TR>
													<TR>
														<TD colSpan="2"><STRONG><FONT face="Arial" size="2">Ship To:</FONT></STRONG></TD>
													</TR>
													<TR>
														<TD colSpan="2"><asp:label id="lblBAddr" runat="server" CssClass="label"></asp:label></TD>
													</TR>
													<TR>
														<TD colSpan="2"><asp:label id="lblBCity" runat="server" CssClass="label"></asp:label>&nbsp;
															<asp:label id="lblBState" runat="server" CssClass="label"></asp:label>&nbsp;
															<asp:label id="lblBZip" runat="server" CssClass="label"></asp:label></TD>
													</TR>
												</TABLE>
											</TD>
										</tr>
									</table>
								</td>
							</tr>
						</table>
					</TD>
				</TR>
				<tr>
					<td colSpan="3">&nbsp;</td>
				</tr>
				<TR>
					<TD colSpan="3"><asp:datagrid id="dgCart" Runat="server" AllowPaging="false" ShowFooter="false" AutoGenerateColumns="False"
							Width="100%">
							<HeaderStyle Font-Bold="True" ForeColor="White" CssClass="labelTextException" BackColor="#336699"></HeaderStyle>
							<Columns>
								<asp:BoundColumn DataField="ItemID" HeaderText="Item ID" Visible="False">
									<ItemStyle Width="10%" CssClass="copy11"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="ItemName" HeaderText="Item Name">
									<ItemStyle Width="40%" CssClass="copy11"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Qty" HeaderText="Qty">
									<ItemStyle Width="20%" CssClass="copy11"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Price" HeaderText="Price" DataFormatString="{0:c}">
									<ItemStyle Width="20%" CssClass="copy11"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Total" HeaderText="Total" DataFormatString="{0:c}">
									<ItemStyle Width="20%" CssClass="copy11"></ItemStyle>
								</asp:BoundColumn>
							</Columns>
						</asp:datagrid></TD>
				</TR>
				<TR>
					<TD colSpan="3">
						<TABLE id="Table3" cellSpacing="1" cellPadding="1" width="300" align="right" border="0">
							<TR>
								<TD width="141" height="24"><FONT face="Arial" size="2"><STRONG><EM>Total Amount</EM></STRONG></FONT></TD>
								<TD width="1" height="24"></TD>
								<TD height="24"><asp:label id="lblGTotal" runat="server" CssClass="labelBold" Width="100%"></asp:label></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD colSpan="3"><FONT face="Arial" size="2"><STRONG>Comments or Special Instructions:</STRONG></FONT>
					</TD>
				</TR>
				<TR>
					<TD colSpan="3"><asp:label id="lblComments" runat="server" CssClass="label"></asp:label></TD>
				</TR>
				<TR>
					<TD colSpan="3">&nbsp;</TD>
				</TR>
				<TR>
					<TD colSpan="3">
						<P align="center"><STRONG><FONT face="Arial">THANK YOU FOR YOUR BUSINESS!</FONT></STRONG></P>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
