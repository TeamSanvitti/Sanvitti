<%@ Register TagPrefix="head" TagName="MenuHeader" Src="./Controls/Header.ascx" %>
<%@ Register TagPrefix="foot" TagName="MenuHeader" Src="./Controls/Footer.ascx" %>
<%@ Register TagPrefix="serv" TagName="MenuSP" Src="./Controls/provider.ascx" %>
<%@ Page language="c#" Codebehind="frmOrder.aspx.cs" AutoEventWireup="false" Inherits="avii.frmOrder" %>
<HTML>
	<HEAD>
		<title>frmOrder</title>
		<script language="javascript" src="avI.js"></script>
		<LINK href="styles.css" type="text/css" rel="stylesheet">
			<LINK href="aerostyle.css" type="text/css" rel="stylesheet">
				<script language="javascript">
			function fnPrnMsg()
			{
				alert('THANK YOU FOR YOUR BUSINESS!\n\n Please print the Purchase Order form and fax us to place the order. (Right click on the Purchase order page and select print option)');
				window.open("prnOrderForm.aspx","Order","Height=700px;Width=400px;center=Yes;");
			}
			
			function fnSbmtMsg(smsg)
			{
				alert('We appreciate for your business and will always thrive for providing quality service to our customer(s).\n\n Your Order# ' + smsg + ' \n\nClick on Print Order button to print the purchase order. You can also check the status of your order by using Order Status link.');
			}
				</script>
	</HEAD>
	<body bgColor="#ffffff" leftMargin="0" topMargin="0" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="95%" align="center" border="0">
				<tr>
					<td vAlign="top" align="left"><head:menuheader id="MenuHeader1" runat="server"></head:menuheader></td>
				</tr>
				<TR>
					<TD><asp:label id="lblErr" runat="server" CssClass="errormessage"></asp:label></TD>
				</TR>
				<tr>
					<td vAlign="top">
						<table cellSpacing="0" cellPadding="0" width="100%" border="1">
							<tr borderColor="#839abf">
								<td>
									<TABLE id="Table1" cellSpacing="1" cellPadding="1" width="100%" border="0">
										<TR>
											<TD>&nbsp;</TD>
										</TR>
										<TR>
											<TD class="copy10grey">Thank you for provide choosing Lan Global Inc. for your 
												business. We are always thrive for providing quality service to our esteemed 
												customers. Please provide us following information to complete the order.</TD>
										</TR>
										<tr>
											<td>
												<hr>
											</td>
										</tr>
										<TR>
											<TD>
												<table width="100%">
													<TR>
														<TD class="copyblue11b" align="center" colSpan="3"><b>Shipping Address</b>
														</TD>
														<TD class="copyblue11b" align="center" colSpan="3"><b>Billing Address</b>
														</TD>
													</TR>
													<TR>
														<TD class="copy10grey" width="15%">
															<P align="right">Address Line 1</FONT><FONT color="#ff0000"></FONT></P>
														</TD>
														<td width="1%"><FONT color="#ff0000"></FONT></td>
														<TD class="cell-data-td" width="35%"><asp:label id="lblAddr1" runat="server" CssClass="copy11"></asp:label></TD>
														<TD class="copy10grey" width="15%">
															<P align="right">Address Line 1</FONT>
															</P>
														</TD>
														<td width="1%"><FONT color="#ff0000"></FONT></td>
														<TD class="cell-data-td" width="35%"><asp:label id="lblBAddr1" runat="server" CssClass="copy11"></asp:label></TD>
													</TR>
													<TR>
														<TD class="copy10grey">
															<P align="right">Address Line 2</FONT>
															</P>
														</TD>
														<TD class="cell-data-td"></TD>
														<TD class="cell-data-td"><asp:label id="lblAddr2" runat="server" CssClass="copy11"></asp:label></TD>
														<TD class="copy10grey">
															<P align="right">Address Line 2 </FONT></P>
														</TD>
														<td><FONT color="#ff0000"></FONT></td>
														<TD class="cell-data-td"><asp:label id="lblBAddr2" runat="server" CssClass="copy11"></asp:label></TD>
													</TR>
													<TR>
														<TD class="copy10grey">
															<P align="right">City<FONT color="#ff0000"></FONT></P>
														</TD>
														<td><FONT color="#ff0000"></FONT></td>
														<TD class="cell-data-td"><asp:label id="lblCity" runat="server" CssClass="copy11"></asp:label></TD>
														<TD class="copy10grey">
															<P align="right">City
															</P>
														</TD>
														<td><FONT color="#ff0000"></FONT></td>
														<TD class="cell-data-td"><asp:label id="lblBCity" runat="server" CssClass="copy11"></asp:label></TD>
													</TR>
													<TR>
														<TD class="copy10grey" height="18">
															<P align="right">State</P>
														</TD>
														<td height="18"><FONT color="#ff0000"></FONT></td>
														<TD class="cell-data-td" height="18"><asp:label id="lblState" CssClass="copy11" Runat="server"></asp:label></TD>
														<TD class="copy10grey" height="18">
															<P align="right">State
															</P>
														</TD>
														<td height="18"><FONT color="#ff0000"></FONT></td>
														<TD class="cell-data-td" height="18"><asp:label id="lblBState" CssClass="copy11" Runat="server"></asp:label></TD>
													</TR>
													<TR>
														<TD class="copy10grey">
															<P align="right">Zip<FONT color="#ff0000"></FONT></P>
														</TD>
														<td><FONT color="#ff0000"></FONT></td>
														<TD class="cell-data-td"><asp:label id="lblZip" runat="server" CssClass="copy11"></asp:label></TD>
														<TD class="copy10grey">
															<P align="right">Zip
															</P>
														</TD>
														<td><FONT color="#ff0000"></FONT></td>
														<TD class="cell-data-td"><asp:label id="lblBZip" runat="server" CssClass="copy11"></asp:label></TD>
													</TR>
												</table>
											</TD>
										</TR>
										<tr>
											<td class="copyblue11b">Comments:</td>
										</tr>
										<tr>
											<td><asp:textbox id="txtComment" runat="server" Width="100%" TextMode="MultiLine"></asp:textbox></td>
										</tr>
									</TABLE>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td>&nbsp;</td>
				</tr>
				<TR>
					<TD><asp:datagrid id="dgCart" Runat="server" Width="100%" AutoGenerateColumns="False" AllowPaging="false"
							ShowFooter="false" AlternatingItemStyle-BackColor="#E8F3F9">
							<HeaderStyle CssClass="button"></HeaderStyle>
							<Columns>
								<asp:BoundColumn DataField="ItemID" HeaderText="Item ID" Visible="False">
									<ItemStyle Width="10%" CssClass="copy11"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="ItemName" HeaderText="Item Name">
									<ItemStyle Width="40%" CssClass="copy11"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Qty" HeaderText="Qty">
									<ItemStyle Width="20%" CssClass="copy11" HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Price">
									<ItemTemplate>
										<asp:Label ID="lblrate" CssClass="copy11" style="TEXT-ALIGN: right" Runat=server Text ='<%#String.Format("{0:c}",Convert.ToDouble(DataBinder.Eval(Container.DataItem,"Price")))%>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Total">
									<ItemTemplate>
										<asp:Label ID="Label1" CssClass="copy11" Runat=server Text ='<%#String.Format("{0:C}",Convert.ToDouble(DataBinder.Eval(Container.DataItem,"Total")))%>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
						</asp:datagrid></TD>
				</TR>
				<TR>
					<TD>
						<TABLE id="Table3" cellSpacing="1" cellPadding="1" width="300" align="right" border="0">
							<TR>
								<TD class="copyblue11b" align="right">Total Amount:</TD>
								<TD></TD>
								<TD class="copyblue11b" align="right"><asp:label id="lblGTotal" runat="server" CssClass="copyblue11b" Width="100%"></asp:label></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<tr>
					<td align="center"><asp:button id="btnSubmit" runat="server" CssClass="button" Width="141px" Text="Submit Order"></asp:button>&nbsp;&nbsp;
						<asp:button id="btnPrint" runat="server" CssClass="button" Width="153px" Text="Print Order"
							Visible="False"></asp:button></td>
				</tr>
				<TR>
					<TD>&nbsp;</TD>
				</TR>
				<TR>
					<TD>&nbsp;</TD>
				</TR>
				<tr>
					<td>
						<serv:MenuSP id="Menusss" runat="server"></serv:MenuSP>
					</td>
				</tr>
				<TR>
					<TD><foot:menuheader id="Menuheader2" runat="server"></foot:menuheader></TD>
				</TR>
			</table>
		</form>
	</body>
</HTML>
