<%@ Page language="c#" Codebehind="frmCust.aspx.cs" AutoEventWireup="false" Inherits="avii.Admin.frmCust" %>
<%--<%@ Register TagPrefix="head" TagName="MenuHeader" Src="./admhead.ascx" %>--%>
<%@ Register TagPrefix="head" TagName="menuheader" Src="~/dhtmlxmenu/menuControl.ascx" %>

<HTML>
	<HEAD>
		<title>Customer Type</title>
		<LINK href="../Styles.css" type="text/css" rel="stylesheet">
			<script language="javascript" src="../avI.js"></script>
	</HEAD>
	<BODY>
		<form id="Form1" method="post" runat="server">
						<TABLE cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
					<TR>
						<TD>
							<head:menuheader id="MenuHeader" runat="server"></head:menuheader></TD>
					</TR>
				</TABLE>
			<table width="70%" align="center">
				<tr>
					<td>
						<asp:datagrid id="dgFeatures" OnItemCommand="dg_ItemCommand" OnEditCommand="dg_Edit" OnUpdateCommand="dg_Update"
							OnCancelCommand="dg_Cancel" OnItemCreated="dg_ItemCreated" ShowFooter="false" Runat="server"
							AllowPaging="false" AutoGenerateColumns="False" Width="100%">
							<HeaderStyle ForeColor="White" CssClass="labelTextException" BackColor="#336699" Font-Bold="True"></HeaderStyle>
							<Columns>
								<asp:EditCommandColumn ButtonType="LinkButton" UpdateText="Update" HeaderText="Select" CancelText="Cancel"
									EditText="Edit" ItemStyle-CssClass="LinkSmall">
									<HeaderStyle CssClass="labelTextboldwhite"></HeaderStyle>
								</asp:EditCommandColumn>
								<asp:ButtonColumn CommandName="delete" Text="Delete" HeaderStyle-CssClass="text10WhiteBold" ItemStyle-CssClass="LinkSmall"
									HeaderText="Delete"></asp:ButtonColumn>
								<asp:TemplateColumn HeaderText="Customer Type">
									<HeaderStyle Width="70%" CssClass="labelTextException"></HeaderStyle>
									<ItemTemplate>
										<asp:Label CssClass=labelText Runat =server text='<%#DataBinder.Eval(Container.DataItem,"CustType").ToString()%>' ID="lblfea">
										</asp:Label>
									</ItemTemplate>
									<EditItemTemplate>
										<asp:TextBox ID="txtCatg" onkeypress="return fnValueValidate(event,'s');" Runat =server MaxLength ="35" Width ="80%" Text ='<%#DataBinder.Eval(Container.DataItem,"CustType").ToString()%>'>
										</asp:TextBox>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="custtypeid" ReadOnly="True" Visible="False"></asp:BoundColumn>
							</Columns>
							<PagerStyle NextPageText="Next Page" PrevPageText="Previous Page&amp;nbsp;&amp;nbsp;" CssClass="text7"></PagerStyle>
						</asp:datagrid>
					</td>
				</tr>
				<tr>
					<td>
						<font class="label">Default Customer Type:</font> &nbsp;&nbsp;<asp:DropDownList id="dpCust" runat="server" AutoPostBack="True"></asp:DropDownList>
					</td>
				</tr>
			</table>
		</form>
	</BODY>
</HTML>
