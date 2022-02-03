<%--<%@ Register TagPrefix="head" TagName="MenuHeader" Src="./admhead.ascx" %>--%>
<%@ Register TagPrefix="head" TagName="menuheader" Src="~/dhtmlxmenu/menuControl.ascx" %>
<%@ Page language="c#" Codebehind="Customers.aspx.cs" AutoEventWireup="false" Inherits="avii.Admin.Customers" %>
<HTML>
	<HEAD>
		<title>Customers Administration</title>
		<LINK href="../aerostyle.css" type="text/css" rel="stylesheet">
		<LINK href="../Styles.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="./../rshop.js"></script>
	</HEAD>
	<body leftmargin="0" topmargin="0" marginwidth="0" marginheight="0">
		<form id="Customers" method="post" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
				<TR>
					<TD>
						<head:menuheader id="MenuHeader" runat="server"></head:menuheader></TD>
				</TR>
			</TABLE>
			<TABLE borderColor="#ffffff" cellSpacing="0" cellPadding="0" width="880" align="left" border="1">
				<TR borderColor="white">
					<td>
						<TABLE id="Table1" cellSpacing="0" cellPadding="2" width="100%" border="0">
							<TR>
								<TD align="right" height="154"><asp:datagrid id="dgCustomer" runat="server" CssClass="text8" pagerstyle-mode="NumericPages" AllowSorting="True"
										AllowCustomPaging="False" AutoGenerateColumns="False" DataKeyField="custID" Width="100%" PageSize="10" OnItemDataBound="dg_ItemDataBound"
										OnItemCommand="dg_ItemCommand">
										<FooterStyle CssClass="label"></FooterStyle>
										<HeaderStyle Font-Bold="True" ForeColor="White" CssClass="labelTextException" BackColor="#336699"></HeaderStyle>
										<Columns>
											<asp:ButtonColumn CommandName="save" Text="Save" ItemStyle-Width="5%"></asp:ButtonColumn>
											<asp:ButtonColumn Text="Delete" HeaderText="Delete" CommandName="delete" ItemStyle-Width="5%">
												<HeaderStyle CssClass="text10WhiteBold"></HeaderStyle>
											</asp:ButtonColumn>
											<asp:BoundColumn DataField="LogonName" HeaderText="Logon" ItemStyle-Width="10%">
												<ItemStyle CssClass="label"></ItemStyle>
											</asp:BoundColumn>
											<asp:TemplateColumn HeaderText="Customer Name">
												<ItemTemplate>
													<asp:HyperLink id="lnk1" CssClass="copy11" Runat="server" NavigateUrl='<%# "../frmCust.aspx?c=" + DataBinder.Eval (Container.DataItem,"custID") %>' TEXT = '<%#DataBinder.Eval(Container.DataItem,"CustName")%>'>
													</asp:HyperLink>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:BoundColumn DataField="Address" HeaderText="Address" ItemStyle-Width="20%" Visible="False">
												<ItemStyle CssClass="label"></ItemStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="email" HeaderText="Email" ItemStyle-Width="20%">
												<ItemStyle CssClass="label"></ItemStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="OfficePhone" HeaderText="Phone" ItemStyle-Width="5%">
												<ItemStyle CssClass="label"></ItemStyle>
											</asp:BoundColumn>
											<asp:TemplateColumn HeaderText="Customer Type" ItemStyle-Width="15%">
												<HeaderStyle CssClass="text10Bold"></HeaderStyle>
												<ItemTemplate>
													<asp:DropDownList ID="dpCustType" Runat="server" Width="100%"></asp:DropDownList>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:BoundColumn DataField="CustTypeID" HeaderText="CustTypeID" Visible="False">
												<ItemStyle CssClass="label"></ItemStyle>
											</asp:BoundColumn>
											<asp:TemplateColumn HeaderText="Sales Assistance" ItemStyle-Width="15%">
												<HeaderStyle CssClass="text10Bold"></HeaderStyle>
												<ItemTemplate>
													<asp:DropDownList ID="dpSales" Runat="server" Width="100%"></asp:DropDownList>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:BoundColumn DataField="SalesID" HeaderText="SalesID" Visible="False">
												<ItemStyle CssClass="label"></ItemStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="custID" HeaderText="CustID" ItemStyle-Width="5%" Visible="False">
												<ItemStyle CssClass="label"></ItemStyle>
											</asp:BoundColumn>
										</Columns>
										<PagerStyle Mode="NumericPages"></PagerStyle>
									</asp:datagrid></TD>
							</TR>
							<tr>
								<td><asp:label id="lblException" CssClass="text8" ForeColor="red" Runat="server"></asp:label></td>
							</tr>
						</TABLE>
					</td>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
