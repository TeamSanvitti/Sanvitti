g<%@ Page language="c#" Codebehind="admusr.aspx.cs" AutoEventWireup="false" Inherits="avii.Admin.admusr" %>
<%--<%@ Register TagPrefix="head" TagName="MenuHeader" Src="./admhead.ascx" %>--%>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="~/dhtmlxmenu/menuControl.ascx" %>
<HTML>
	<HEAD>
		<title>Admin User</title>
		<LINK href="../Styles.css" type="text/css" rel="stylesheet">
			<script language="javascript" src="../avI.js"></script>
	</HEAD>
    <body leftmargin="0" topmargin="0" marginwidth="0" marginheight="0">
		<form id="Form1" method="post" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
				<TR>
					<TD>
						<head:MenuHeader id="MenuHeader" runat="server"></head:MenuHeader></TD>
				</TR>
			</TABLE>
			<TABLE cellSpacing="0" cellPadding="0" width="90%" border="1">
				<TR vAlign="top">
					<TD height="182"></TD>
					<TD align="center">
						<TABLE id="Table1" cellSpacing="1" cellPadding="1" width="100%" border="1">
							<TR>
								<TD align="center" valign="top">
									<asp:datagrid id="dgUsr" Width="100%" AutoGenerateColumns="False" AllowPaging="false" Runat="server"
										ShowFooter="false" OnItemCreated="dg_ItemCreated" OnCancelCommand="dg_Cancel" OnUpdateCommand="dg_Update"
										OnEditCommand="dg_Edit" OnItemCommand="dg_ItemCommand" OnItemDataBound="dg_ItemBound">
										<HeaderStyle CssClass="button"></HeaderStyle>
										<Columns>
											<asp:EditCommandColumn ButtonType="LinkButton" UpdateText="Update" HeaderText="Select" CancelText="Cancel" ItemStyle-Width="10%"
												EditText="Edit" ItemStyle-CssClass="copy11"></asp:EditCommandColumn>
											<asp:ButtonColumn CommandName="delete" Text="Delete" ItemStyle-CssClass="copy11" HeaderText="Delete" ItemStyle-Width="5%"></asp:ButtonColumn>
											<asp:TemplateColumn HeaderText="Username" ItemStyle-Width="10%">
												<ItemTemplate>
												    <%# Eval("username")%>
												</ItemTemplate>
												<EditItemTemplate>
													<asp:TextBox ID="txtUserName" onkeypress="return fnValueValidate(event,'ns');" Runat =server MaxLength ="35" Width ="80%" Text ='<%#DataBinder.Eval(Container.DataItem,"username").ToString()%>'>
													</asp:TextBox>
												</EditItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Password" ItemStyle-Width="10%">
												<ItemTemplate>
												    <%# Eval("pwd")%>
												</ItemTemplate>
												<EditItemTemplate>
													<INPUT type="password" runat="server" id="txtPwd" name="txtPwd" onkeypress="return fnValueValidate(event,'ns');"  Text ='<%#DataBinder.Eval(Container.DataItem,"pwd").ToString()%>'>
												</EditItemTemplate>
											</asp:TemplateColumn>
											
											<asp:TemplateColumn HeaderText="Email" ItemStyle-Width="20%">
												<ItemTemplate>
												   		 <%# Eval("Email")%>						
												</ItemTemplate>
												<EditItemTemplate>
													<asp:TextBox ID="txtEmail" onkeypress="return fnValueValidate(event,'ns');" Runat =server MaxLength ="100" Width ="100%" Text ='<%#DataBinder.Eval(Container.DataItem,"Email").ToString()%>'>
													</asp:TextBox>
												</EditItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Company" ItemStyle-Width="10%">
												<ItemTemplate>
												    <%# Eval("CompanyName")%>											
												</ItemTemplate>
												<EditItemTemplate>
													<asp:DropDownList ID="dpCompany" runat="server">
													</asp:DropDownList>
												</EditItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Company Account#" ItemStyle-Width="20%" ItemStyle-HorizontalAlign ="center">
												<ItemTemplate>
												    <%#DataBinder.Eval(Container.DataItem,"CompanyAccountNumber").ToString()%>
												</ItemTemplate>
												<EditItemTemplate>
													<asp:TextBox ID="txtAccount" onkeypress="return fnValueValidate(event,'ns');" Runat =server MaxLength ="35" Width ="80%" Text ='<%#DataBinder.Eval(Container.DataItem,"CompanyAccountNumber").ToString()%>'>
													</asp:TextBox>
												</EditItemTemplate>
											</asp:TemplateColumn>	
											<asp:TemplateColumn HeaderText="Account Status" ItemStyle-Width="10%">
												<ItemTemplate>
													<asp:Label CssClass=copy11 Runat =server text='<%#DataBinder.Eval(Container.DataItem,"AccountStatus").ToString()%>' ID="lblfea">
													</asp:Label>
												</ItemTemplate>
												<EditItemTemplate>
													<asp:DropDownList ID="dpStatus" runat="server">
													    <asp:ListItem Value="" Text=""></asp:ListItem>
													    <asp:ListItem Value="1" Text="Pending"></asp:ListItem>
													    <asp:ListItem Value="2" Text="Approved"></asp:ListItem>
													    <asp:ListItem Value="3" Text="Denied"></asp:ListItem>
													</asp:DropDownList>
												</EditItemTemplate>
											</asp:TemplateColumn>	
											
																					
											<asp:BoundColumn ItemStyle-CssClass="copy11"  DataField="USERID" ReadOnly="True" Visible="false"></asp:BoundColumn>

										</Columns>
										<PagerStyle NextPageText="Next Page" PrevPageText="Previous Page&amp;nbsp;&amp;nbsp;" CssClass="text7"></PagerStyle>
									</asp:datagrid></TD>
							<TR>
								<TD></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
