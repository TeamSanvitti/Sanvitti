<%@ Control Language="c#" AutoEventWireup="false" Codebehind="cart.ascx.cs" Inherits="pndt.Controls.cart" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<TABLE id="Table1" cellSpacing="1" cellPadding="1" width="100%" border="0">
	<TR>
		<TD align="right"><IMG alt="" src="/images/shop-cart.gif"></TD>
	</TR>
	<TR>
		<TD>
			<TABLE id="Table2" cellSpacing="1" cellPadding="1" width="100%" border="0">
				<TR>
					<TD align="center">
						<asp:Label id="lblerr" CssClass="errormessage" runat="server"></asp:Label></TD>
				</TR>
				<asp:Panel ID="pnlcart" Runat="server" Visible="False">
					<TR>
						<TD>
							<asp:datagrid id="dgCart" Runat="server" OnItemDataBound="dg_DataBound" Width="100%" ShowFooter="false"
								AllowPaging="false" AutoGenerateColumns="False">
								<HeaderStyle CssClass="button"></HeaderStyle>
								<Columns>
									<asp:BoundColumn DataField="ItemName" HeaderText="Item Name" ItemStyle-CssClass="copy11">
										<ItemStyle Width="40%"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Qty">
										<ItemTemplate>
											<asp:TextBox ID="txtQty" CssClass="txfield1" onkeypress="return fnValueValidate(event,'i');" Runat =server MaxLength ="35" Width ="80%" Text ='<%#DataBinder.Eval(Container.DataItem,"Qty").ToString()%>'>
											</asp:TextBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Price">
										<ItemTemplate>
											<asp:Label ID="lblrate" CssClass="copy11" Runat=server Text ='<%#String.Format("{0:c}",Convert.ToDouble(DataBinder.Eval(Container.DataItem,"Price")))%>'>
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
									<TD class="copyblue11b">Total Amount</TD>
									<TD></TD>
									<TD>
										<asp:label id="lblGTotal" runat="server" CssClass="copyblue11b" Width="100%"></asp:label></TD>
								</TR>
								<TR>
									<TD></TD>
									<TD></TD>
									<TD></TD>
								</TR>
								<TR>
									<TD></TD>
									<TD></TD>
									<TD></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD align="center">
							<asp:Button id="btnUpd" runat="server" CssClass="button" Text="Update Qty"></asp:Button>
							<asp:Button id="btnCheckout" runat="server" CssClass="button" Text="Place Order"></asp:Button></TD>
					</TR>
				</asp:Panel>
			</TABLE>
		</TD>
	</TR>
	<tr>
		<td class="copyblue11b">&nbsp;</td>
	</tr>
	<tr>
		<td class="copyblue11b">About Shopping Cart</td>
	</tr>
	<tr>
		<td class="copy11"><LI>Items in your Shopping Cart always reflect the most recent price 
				displayed on their product pages.</LI></td>
	</tr>
</TABLE>
