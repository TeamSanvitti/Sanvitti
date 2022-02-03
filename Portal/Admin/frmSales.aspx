<%@ Page language="c#" Codebehind="frmSales.aspx.cs" AutoEventWireup="false" Inherits="avii.Admin.frmSales" %>
<%--<%@ Register TagPrefix="head" TagName="MenuHeader" Src="./admhead.ascx" %>--%>
<%@ Register TagPrefix="head" TagName="menuheader" Src="~/dhtmlxmenu/menuControl.ascx" %>

<HTML>
	<HEAD>
		<title>Sales Employee List</title>
		<LINK href="../aerostyle.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<form id="Customers" method="post" runat="server">
				<TABLE cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
					<TR>
						<TD>
							<head:menuheader id="MenuHeader" runat="server"></head:menuheader></TD>
					</TR>
				</TABLE>		
			<table width="100%" align="center">
				<tr>
					<td>
						<table borderColor="gray" cellSpacing="0" cellPadding="0" width="100%" align="center" border="1">
							<tr borderColor="white">
								<td>
									<table width="100%">
										<tr>
											<td class="copyblue11b">SaleS EMPLOYEE LIST:</td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td>
						<asp:datagrid id="dgEmp" OnItemCommand="dg_ItemCommand" OnEditCommand="dg_Edit" OnUpdateCommand="dg_Update"
							OnCancelCommand="dg_Cancel" OnItemCreated="dg_ItemCreated" ShowFooter="false" Runat="server"
							AllowPaging="false" AutoGenerateColumns="False" Width="100%">
							<HeaderStyle CssClass="button"></HeaderStyle>
							<Columns>
								<asp:EditCommandColumn ButtonType="LinkButton" ItemStyle-Width="10%" UpdateText="Update" HeaderText="Select"
									CancelText="Cancel" EditText="Edit" ItemStyle-CssClass="copy11"></asp:EditCommandColumn>
								<asp:ButtonColumn CommandName="delete" Text="Delete" ItemStyle-Width="10%" ItemStyle-CssClass="copy11"
									HeaderText="Delete"></asp:ButtonColumn>
								<asp:TemplateColumn HeaderText="Sales Employee" ItemStyle-Width="40%">
									<ItemTemplate>
										<asp:Label CssClass=copy11 Runat =server text='<%#DataBinder.Eval(Container.DataItem,"Employee").ToString()%>' ID="lblfea">
										</asp:Label>
									</ItemTemplate>
									<EditItemTemplate>
										<asp:TextBox ID="txtCatg" onkeypress="return fnValueValidate(event,'s');" Runat =server MaxLength ="35" Width ="80%" Text ='<%#DataBinder.Eval(Container.DataItem,"Employee").ToString()%>'>
										</asp:TextBox>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Active" ItemStyle-Width="5%">
									<ItemTemplate>
										<asp:Label CssClass=copy11 Runat =server text='<%#DataBinder.Eval(Container.DataItem,"Active").ToString()%>' ID="lblActive">
										</asp:Label>
									</ItemTemplate>
									<EditItemTemplate>
										<asp:CheckBox ID="chkActive" Runat="server" Checked='<%#Convert.ToBoolean(DataBinder.Eval(Container.DataItem,"Active").ToString())%>'>
										</asp:CheckBox>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn ItemStyle-CssClass="copy11" DataField="SalesID" ReadOnly="True" Visible="False"></asp:BoundColumn>
							</Columns>
							<PagerStyle NextPageText="Next Page" PrevPageText="Previous Page&amp;nbsp;&amp;nbsp;" CssClass="text7"></PagerStyle>
						</asp:datagrid></td>
				</tr>
				<tr>
					<td></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
