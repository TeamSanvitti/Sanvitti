<%@ Page language="c#" Codebehind="repSales.aspx.cs" AutoEventWireup="false" Inherits="avii.Admin.repSales" %>
<%--<%@ Register TagPrefix="head" TagName="MenuHeader" Src="./admhead.ascx" %>--%>
<%@ Register TagPrefix="head" TagName="menuheader" Src="~/dhtmlxmenu/menuControl.ascx" %>

<HTML>
	<HEAD>
		<title>Sales Report</title>
		<LINK href="../aerostyle.css" type="text/css" rel="stylesheet">
			<LINK href="../Styles.css" type="text/css" rel="stylesheet">
				<script language="javascript" src="../avI.js"></script>
	</HEAD>
	<body>
		<div align="center">
			<form id="Form1" method="post" runat="server">
				<TABLE cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
					<TR>
						<TD>
							<head:menuheader id="MenuHeader" runat="server"></head:menuheader></TD>
					</TR>
				</TABLE>			
				<TABLE id="Table1" borderColor="gray" cellSpacing="1" cellPadding="1" width="95%" border="0">
					<TR>
						<TD align="right" width="50%"><asp:radiobutton id="rdSum" runat="server" Text="Summary" GroupName="grp" CssClass="copy10or"></asp:radiobutton>&nbsp;</TD>
						<TD width="50%"><asp:radiobutton id="rdDet" runat="server" Text="Detail" GroupName="grp" CssClass="copy10or" Checked="True"></asp:radiobutton></TD>
					</TR>
				</TABLE>
				<TABLE id="Table1" borderColor="gray" cellSpacing="1" cellPadding="1" width="95%" border="1">
					<TR borderColor="white">
						<TD>
							<TABLE id="Table1" cellSpacing="1" cellPadding="1" width="100%" border="0">
								<TR>
									<TD><asp:radiobutton id="rdDt" runat="server" GroupName="grpDur" AutoPostBack="True"></asp:radiobutton></TD>
									<TD class="label" align="right">Start Date</TD>
									<TD><asp:textbox id="txtSDate" runat="server" CssClass="labeltext"></asp:textbox></TD>
									<TD class="label" align="right">End Date</TD>
									<TD><asp:textbox id="txtEDate" runat="server" CssClass="labeltext"></asp:textbox></TD>
								</TR>
								<TR>
									<TD><asp:radiobutton id="rdMnth" runat="server" GroupName="grpDur" Checked="True" AutoPostBack="True"></asp:radiobutton></TD>
									<TD class="label" align="right">Duration</TD>
									<TD colSpan="3"><asp:dropdownlist id="dpDur" CssClass="labeltext" Runat="server">
											<asp:ListItem></asp:ListItem>
											<asp:ListItem Value="d" Selected="True">Daily</asp:ListItem>
											<asp:ListItem Value="w">Weekly</asp:ListItem>
											<asp:ListItem Value="ww">Two Weeks</asp:ListItem>
											<asp:ListItem Value="m">Monthly</asp:ListItem>
											<asp:ListItem Value="mm">Two Months</asp:ListItem>
											<asp:ListItem Value="q">Quaterly</asp:ListItem>
										</asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD></TD>
									<TD class="label" align="right">Phone Type</TD>
									<TD><asp:dropdownlist id="dpType" CssClass="labeltext" Runat="server"></asp:dropdownlist></TD>
									<TD class="label" align="right">Manufacturer</TD>
									<TD><asp:dropdownlist id="dpManuf" CssClass="labeltext" Runat="server"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD></TD>
									<TD class="label" align="right">Phone</TD>
									<TD colspan="3"><asp:dropdownlist id="dpPhone" CssClass="labeltext" Runat="server"></asp:dropdownlist></TD>
								</TR>
								<tr>
									<td></td>
									<TD class="label" align="right">Service Provider</TD>
									<TD colspan="3"><asp:dropdownlist id="dpSP" CssClass="labeltext" Runat="server"></asp:dropdownlist></TD>
								</tr>
								<TR>
									<TD></TD>
									<TD class="label" align="right">Customer</TD>
									<TD colspan="3"><asp:dropdownlist id="dpCust" CssClass="labeltext" Runat="server"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD></TD>
									<TD class="label" align="right">Sales Name</TD>
									<TD colspan="3"><asp:dropdownlist id="dpSales" CssClass="labeltext" Runat="server"></asp:dropdownlist></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
				</TABLE>
				<TABLE id="Table1" borderColor="gray" cellSpacing="1" cellPadding="1" width="95%" border="0">
					<tr>
						<td align="center" colSpan="5"><asp:button id="btnSubmit" runat="server" Text="Submit" CssClass="button"></asp:button>&nbsp;
							<asp:button id="btnCancel" runat="server" Text="Cancel" CssClass="button"></asp:button></td>
					</tr>
				</TABLE>
				<TABLE id="Table1" cellSpacing="1" cellPadding="1" width="95%" border="0" bordercolor="gray">
					<tr>
						<td colspan="5" align="center">
							<asp:DataGrid id="dgOrders" runat="server" Width="100%" AutoGenerateColumns="False" AllowSorting="True"
								OnDeleteCommand="dgDelete" OnItemDataBound="dgItemBound">
								<HeaderStyle Font-Bold="True" ForeColor="White" CssClass="button" BackColor="#336699"></HeaderStyle>
								<Columns>
									<asp:ButtonColumn ButtonType="LinkButton" CommandName="delete" Text="Delete" HeaderText="Action" ItemStyle-Width="7%"></asp:ButtonColumn>
									<asp:BoundColumn ItemStyle-Width="8%" ItemStyle-CssClass="copy11" DataField="OrderID" HeaderText="Order#"></asp:BoundColumn>
									<asp:BoundColumn ItemStyle-Width="10%" ItemStyle-CssClass="copy11" DataField="OrderDate" HeaderText="Order Date"></asp:BoundColumn>
									<asp:BoundColumn ItemStyle-Width="30%" ItemStyle-CssClass="copy11" DataField="CustName" HeaderText="Customer"></asp:BoundColumn>
									<asp:BoundColumn ItemStyle-Width="15%" DataFormatString="{0:C}" ItemStyle-CssClass="copy11" DataField="Total"
										HeaderText="Order Amount"></asp:BoundColumn>
									<asp:TemplateColumn HeaderText="" ItemStyle-HorizontalAlign="Center">
										<HeaderStyle Width="5%" CssClass="labelTextException"></HeaderStyle>
										<ItemTemplate>
											<asp:HyperLink ID="imb" alt="Order Status" CssClass="copy11" Target=_blank Runat="server" ImageUrl="../images/clickhere.gif" NavigateUrl='<%# "./frmOrder.aspx?oid=" + DataBinder.Eval (Container.DataItem,"OrderID") %>'>
											</asp:HyperLink>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="" ItemStyle-HorizontalAlign="Center">
										<HeaderStyle Width="5%" CssClass="labelTextException"></HeaderStyle>
										<ItemTemplate>
											<asp:HyperLink ID="hnkOrd" alt="Purchase Order" CssClass=copy11 Target=_blank Runat="server" ImageUrl="../images/attach.gif" NavigateUrl='<%# "../prnOrderForm.aspx?oid=" + DataBinder.Eval (Container.DataItem,"OrderID") %>'>
											</asp:HyperLink>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle NextPageText="Next Page" PrevPageText="Previous Page&amp;nbsp;&amp;nbsp;" CssClass="text7"></PagerStyle>
							</asp:DataGrid>
						</td>
					</tr>
				</TABLE>
			</form>
		</div>
	</body>
</HTML>
