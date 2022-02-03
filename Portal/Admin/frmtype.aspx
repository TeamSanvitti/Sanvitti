<%--<%@ Register TagPrefix="head" TagName="MenuHeader" Src="./admhead.ascx" %>--%>
<%@ Register TagPrefix="head" TagName="menuheader" Src="~/dhtmlxmenu/menuControl.ascx" %>
<%@ Page language="c#" Codebehind="frmtype.aspx.cs" AutoEventWireup="false" Inherits="avii.Admin.frmtype" %>
<HTML>
	<HEAD>
		<title>Phone Type</title>
		<LINK href="../Styles.css" type="text/css" rel="stylesheet">
			<script language="javascript" src="../avI.js"></script>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
				<TR>
					<TD>
						<head:menuheader id="MenuHeader" runat="server"></head:menuheader></TD>
				</TR>
			</TABLE>
			<TABLE id="Table1" cellSpacing="1" cellPadding="1" width="95%" border="1">
				<TR>
					<TD></TD>
				</TR>
				<TR>
					<TD>
						<asp:datagrid id="dgType" Width="100%" AutoGenerateColumns="False" AllowPaging="false" Runat="server"
							ShowFooter="false" OnItemCreated="dg_ItemCreated" OnCancelCommand="dg_Cancel" OnUpdateCommand="dg_Update"
							OnEditCommand="dg_Edit" OnItemCommand="dg_ItemCommand" OnItemDataBound="dg_ItemBound">
							<HeaderStyle CssClass="button"></HeaderStyle>
							<Columns>
								<asp:EditCommandColumn ButtonType="LinkButton" UpdateText="Update" HeaderText="Select" CancelText="Cancel"
									EditText="Edit" ItemStyle-CssClass="copy11"></asp:EditCommandColumn>
								<asp:ButtonColumn CommandName="delete" Text="Delete" ItemStyle-CssClass="copy11" HeaderText="Delete"></asp:ButtonColumn>
								<asp:TemplateColumn HeaderText="Phone/Accessory">
									<ItemTemplate>
										<asp:Label CssClass=copy11 Runat =server text='<%#DataBinder.Eval(Container.DataItem,"PDesc").ToString()%>' ID="lblfea">
										</asp:Label>
									</ItemTemplate>
									<EditItemTemplate>
										<asp:TextBox ID="txtCatg" onkeypress="return fnValueValidate(event,'s');" Runat =server MaxLength ="35" Width ="80%" Text ='<%#DataBinder.Eval(Container.DataItem,"PDesc").ToString()%>'>
										</asp:TextBox>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Type">
									<ItemTemplate>
										<asp:Label CssClass=copy11 Runat =server text='<%#DataBinder.Eval(Container.DataItem,"PType").ToString()%>' ID="lblType">
										</asp:Label>
									</ItemTemplate>
									<EditItemTemplate>
										<asp:DropDownList ID="dpType" Runat="server">
											<asp:ListItem Value="P">Phone</asp:ListItem>
											<asp:ListItem Value="A">Accessories</asp:ListItem>
										</asp:DropDownList>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn ItemStyle-CssClass="copy11" DataField="ID" ReadOnly="True" Visible="False"></asp:BoundColumn>
							</Columns>
							<PagerStyle NextPageText="Next Page" PrevPageText="Previous Page&amp;nbsp;&amp;nbsp;" CssClass="text7"></PagerStyle>
						</asp:datagrid></TD>
				</TR>
				<TR>
					<TD></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
