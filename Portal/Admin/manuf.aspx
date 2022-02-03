<%@ Page language="c#" Codebehind="manuf.aspx.cs" AutoEventWireup="false" Inherits="avii.Admin.manuf" %>
<%--<%@ Register TagPrefix="head" TagName="MenuHeader" Src="./admhead.ascx" %>--%>
<%@ Register TagPrefix="head" TagName="menuheader" Src="~/dhtmlxmenu/menuControl.ascx" %>

<HTML>
	<HEAD>
		<title>Manufacturer List</title>
		<LINK href="../aerostyle.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
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
											<td class="copyblue11b">Record Filter:</td>
											<td><asp:radiobutton id="rdManuf" runat="server" CssClass="copy10or" Checked="True" Text="Manufecturer"
													GroupName="RecFil" AutoPostBack="True"></asp:radiobutton></td>
											<td><asp:radiobutton id="rdSP" runat="server" CssClass="copy10or" Text="Service Provider" GroupName="RecFil"
													AutoPostBack="True"></asp:radiobutton></td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td><asp:datagrid id="dgManuf" OnItemCommand="dg_ItemCommand" OnEditCommand="dg_Edit" OnUpdateCommand="dg_Update"
							OnCancelCommand="dg_Cancel" OnItemCreated="dg_ItemCreated" ShowFooter="false" Runat="server"
							AllowPaging="false" AutoGenerateColumns="False" Width="100%">
							<HeaderStyle CssClass="button"></HeaderStyle>
							<Columns>
								<asp:EditCommandColumn ButtonType="LinkButton" UpdateText="Update" HeaderText="Select" CancelText="Cancel"
									EditText="Edit" ItemStyle-CssClass="copy11"></asp:EditCommandColumn>
								<asp:ButtonColumn CommandName="delete" Text="Delete" ItemStyle-CssClass="copy11" HeaderText="Delete"></asp:ButtonColumn>
								<asp:TemplateColumn HeaderText="Manufacturer">
									<ItemTemplate>
										<asp:Label CssClass=copy11 Runat =server text='<%#DataBinder.Eval(Container.DataItem,"ManufName").ToString()%>' ID="lblfea">
										</asp:Label>
									</ItemTemplate>
									<EditItemTemplate>
										<asp:TextBox ID="txtCatg" onkeypress="return fnValueValidate(event,'s');" Runat =server MaxLength ="35" Width ="80%" Text ='<%#DataBinder.Eval(Container.DataItem,"ManufName").ToString()%>'>
										</asp:TextBox>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Image">
									<ItemTemplate>
										<asp:Image ID="img" Width="80" Height="40" Runat=server ImageUrl='<%#DataBinder.Eval(Container.DataItem,"ManufImage").ToString()%>'>
										</asp:Image>
										<asp:Label CssClass=copy11 Runat =server text='<%#DataBinder.Eval(Container.DataItem,"ManufImage").ToString()%>' ID="Label1">
										</asp:Label>
									</ItemTemplate>
									<EditItemTemplate>
										<INPUT type="file" runat="server" width="100%" ID="Filemanuf" NAME="Filemanuf">
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn ItemStyle-CssClass="copy11" DataField="ManufID" ReadOnly="True" Visible="False"></asp:BoundColumn>
							</Columns>
							<PagerStyle NextPageText="Next Page" PrevPageText="Previous Page&amp;nbsp;&amp;nbsp;" CssClass="text7"></PagerStyle>
						</asp:datagrid><asp:datagrid id="dgSP" OnItemCommand="dg_SPItemCommand" OnEditCommand="dg_SPEdit" OnUpdateCommand="dg_SPUpdate"
							OnCancelCommand="dg_SPCancel" OnItemCreated="dg_SPItemCreated" ShowFooter="false" Runat="server" AllowPaging="false"
							AutoGenerateColumns="False" Width="100%">
							<HeaderStyle CssClass="button"></HeaderStyle>
							<Columns>
								<asp:EditCommandColumn ButtonType="LinkButton" UpdateText="Update" HeaderText="Select" CancelText="Cancel"
									EditText="Edit" ItemStyle-CssClass="copy11"></asp:EditCommandColumn>
								<asp:ButtonColumn CommandName="delete" Text="Delete" ItemStyle-CssClass="copy11" HeaderText="Delete"></asp:ButtonColumn>
								<asp:TemplateColumn HeaderText="Manufacturer">
									<ItemTemplate>
										<asp:Label CssClass=copy11 Runat =server text='<%#DataBinder.Eval(Container.DataItem,"ServiceProvider").ToString()%>' ID="Label2">
										</asp:Label>
									</ItemTemplate>
									<EditItemTemplate>
										<asp:TextBox ID="Textbox1" onkeypress="return fnValueValidate(event,'s');" Runat =server MaxLength ="35" Width ="80%" Text ='<%#DataBinder.Eval(Container.DataItem,"ServiceProvider").ToString()%>'>
										</asp:TextBox>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Image">
									<ItemTemplate>
										<asp:Image ID="Image1" Width="80" Height="40" Runat=server ImageUrl='<%#DataBinder.Eval(Container.DataItem,"ServiceProviderImage").ToString()%>'>
										</asp:Image>
										<asp:Label CssClass=copy11 Runat =server text='<%#DataBinder.Eval(Container.DataItem,"ServiceProviderImage").ToString()%>' ID="Label3">
										</asp:Label>
									</ItemTemplate>
									<EditItemTemplate>
										<INPUT type="file" runat="server" width="100%" ID="File1" NAME="Filemanuf">
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn ItemStyle-CssClass="copy11" DataField="SPID" ReadOnly="True" Visible="False"></asp:BoundColumn>
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
