<%@ Page language="c#" Codebehind="frmItmLst.aspx.cs" AutoEventWireup="false" Inherits="avii.Admin.frmItmLst" %>
<%--<%@ Register TagPrefix="head" TagName="MenuHeader" Src="./admhead.ascx" %>--%>
<%@ Register TagPrefix="head" TagName="menuheader" Src="~/dhtmlxmenu/menuControl.ascx" %>

<HTML>
	<HEAD>
		<title>Item Search</title>
		<LINK href="../Aerostyle.css" type="text/css" rel="stylesheet">
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
			<asp:Label ID="lblErr" Runat="server" CssClass="errormessage"></asp:Label>
			<table width="99%" align="center" borderColorLight="gray" border="1">
				<tr borderColor="white">
					<td align="center">
						<TABLE id="Table1" cellPadding="1" width="99%" border="0">
							<TR>
								<TD width="12%">
									<P align="right"><font class="label">Phone Type</font></P>
								</TD>
								<TD width="25%" height="16"><asp:dropdownlist id="dpPType" runat="server"></asp:dropdownlist></TD>
								<TD width="12%">
									<P align="right"><font class="label">Manufacturer</font></P>
								</TD>
								<TD width="25%"><asp:dropdownlist id="dpManu" runat="server">
										<asp:ListItem Value=""></asp:ListItem>
										<asp:ListItem Value="1">Audiovox</asp:ListItem>
										<asp:ListItem Value="2">LG</asp:ListItem>
										<asp:ListItem Value="3">Samsung</asp:ListItem>
									</asp:dropdownlist></TD>
								<TD>
									<P align="right"><font class="label">Phone Model</font></P>
								</TD>
								<TD><asp:textbox onkeypress="return fnValueValidate(event,'s');" id="txtModel" runat="server" CssClass="labeltext"></asp:textbox></TD>
							</TR>
							<TR>
								<TD align="right"><font class="label">Phone Condition</font>
								</TD>
								<TD colSpan="5"><asp:checkbox id="chkNew" runat="server" CssClass="label" Text="New"></asp:checkbox>&nbsp;&nbsp;&nbsp;&nbsp;<asp:checkbox id="chkUsed" runat="server" CssClass="label" Text="Used"></asp:checkbox>
									&nbsp;&nbsp;&nbsp;&nbsp;<asp:checkbox id="chkRef" runat="server" CssClass="label" Text="Refurbished"></asp:checkbox>
								</TD>
							</TR>
						</TABLE>
					</td>
				</tr>
			</table>
			<table width="99%" align="center">
				<tr>
					<td align="center"><asp:button id="btnSrch" runat="server" CssClass="button" Text="Search"></asp:button>&nbsp;&nbsp;&nbsp;<asp:button id="btnCancel" runat="server" CssClass="button" Text="Cancel"></asp:button>
						&nbsp;&nbsp;&nbsp;<asp:Button id="btnAdd" runat="server" CssClass="button" Text="Add New"></asp:Button></td>
				</tr>
				<tr>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td colSpan="6"><asp:datagrid id="dgItems" runat="server" AutoGenerateColumns="False" AllowPaging="False" Width="100%"
							OnDeleteCommand="dgDelete" OnItemDataBound="dgItemBound">
							<HeaderStyle CssClass="button"></HeaderStyle>
							<Columns>
								<asp:ButtonColumn ButtonType="LinkButton" CommandName="delete" Text="Delete" HeaderText="Action" ItemStyle-Width="7%"></asp:ButtonColumn>
								<asp:TemplateColumn HeaderText="Phone Model">
									<ItemTemplate>
										<asp:HyperLink id="lnk1" CssClass="copy11" Runat="server" NavigateUrl='<%# "./frmItem.aspx?tid=" + DataBinder.Eval (Container.DataItem,"ItemID") %>' TEXT = '<%#DataBinder.Eval(Container.DataItem,"PhoneModel")%>'>
										</asp:HyperLink>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn ItemStyle-CssClass="copy11" DataField="ItemID" HeaderText="ItemID" Visible="False"
									ReadOnly="True" SortExpression="PhoneType"></asp:BoundColumn>
								<asp:BoundColumn ItemStyle-CssClass="copy11" DataField="PhoneType" HeaderText="Phone Type" ReadOnly="True"
									SortExpression="PhoneType"></asp:BoundColumn>
								<asp:BoundColumn ItemStyle-CssClass="copy11" DataField="ManufName" HeaderText="Manufecturer" ReadOnly="True"
									SortExpression="ManufName"></asp:BoundColumn>
								<asp:BoundColumn ItemStyle-CssClass="copy11" DataField="Phonetitle" HeaderText="Phone Title" ReadOnly="True"
									SortExpression="Phonetitle"></asp:BoundColumn>
								<asp:BoundColumn ItemStyle-CssClass="copy11" DataField="Special" HeaderText="Special" ReadOnly="True"
									SortExpression="Special"></asp:BoundColumn>
							</Columns>
						</asp:datagrid></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
