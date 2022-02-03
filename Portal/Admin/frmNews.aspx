<%@ Page language="c#" Codebehind="frmNews.aspx.cs" AutoEventWireup="false" Inherits="avii.frmNews" %>
<%--<%@ Register TagPrefix="head" TagName="MenuHeader" Src="./admhead.ascx" %>--%>
<%@ Register TagPrefix="head" TagName="menuheader" Src="~/dhtmlxmenu/menuControl.ascx" %>

<HTML>
	<HEAD>
		<title>frmNews</title>
		<link href="../aerostyle.css" rel="stylesheet" type="text/css">
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
			<asp:panel id="pnlSearch" Runat="server">
				<TABLE borderColor="gray" cellSpacing="0" cellPadding="0" width="780" align="center" border="1">
					<TR>
						<TD>
							<TABLE width="100%" align="center">
								<TR>
									<TD><FONT class="label">Title:</FONT></TD>
									<TD>
										<asp:TextBox id="txtFTitle" runat="server" MaxLength="100"></asp:TextBox></TD>
									<TD><FONT class="label">Doc Type:</FONT></TD>
									<TD>
										<asp:DropDownList id="dpDocType" runat="server">
											<asp:ListItem Value=""></asp:ListItem>
											<asp:ListItem Value="F">Form</asp:ListItem>
											<asp:ListItem Value="N">News</asp:ListItem>
										</asp:DropDownList></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
				</TABLE>
			</asp:panel>
			<table width="780" align="center">
				<TR>
					<TD align="center"><asp:button id="btnSearch" runat="server" CssClass="button" Text="Search"></asp:button>&nbsp;&nbsp;
						<asp:button id="Button1" runat="server" CssClass="button" Text="Clear"></asp:button>&nbsp;&nbsp;
						<asp:button id="btnAdd" runat="server" CssClass="button" Text="Add"></asp:button></TD>
				</TR>
				<tr>
					<td><asp:datagrid id="dgNews" runat="server" Width="100%" AutoGenerateColumns="False" OnItemCommand="dgNews_Command">
							<HeaderStyle CssClass="copyblue11b"></HeaderStyle>
							<Columns>
								<asp:TemplateColumn HeaderText="Title" ItemStyle-Width="75%">
									<ItemTemplate>
										<asp:LinkButton ID="lnkOrder" CssClass="copy10" Runat=server CommandName="select" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"Uid")%>' TEXT = '<%#DataBinder.Eval(Container.DataItem,"Title")%>'>
										</asp:LinkButton>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn ItemStyle-CssClass="copy11" ItemStyle-Width="8%" DataField="UType" HeaderText="Doc Type"
									ReadOnly="True" SortExpression="OrderDate"></asp:BoundColumn>
								<asp:BoundColumn ItemStyle-CssClass="copy11" ItemStyle-Width="11%" DataField="sDate" HeaderText="Start Date"
									ReadOnly="True" SortExpression="CustName"></asp:BoundColumn>
								<asp:BoundColumn ItemStyle-CssClass="copy11" ItemStyle-Width="6%" DataField="Active" HeaderText="Active"
									ReadOnly="True"></asp:BoundColumn>
							</Columns>
						</asp:datagrid></td>
				</tr>
			</table>
			<asp:panel id="pnlAdd" Runat="server" Visible="False">
				<TABLE cellSpacing="2" cellPadding="2" width="780" align="center" border="0">
					<TR>
						<TD>
							<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" borderColorLight="gray"
								border="1">
								<TR>
									<TD>
										<TABLE id="Table2" cellSpacing="2" cellPadding="1" width="100%" border="0">
											<TR>
												<TD align="right" width="50%">
													<asp:radiobutton id="rbForms" runat="server" Text="Forms" CssClass="copy10or" Checked="True" GroupName="Grp"></asp:radiobutton></TD>
												<TD width="50%">
													<asp:radiobutton id="rbNews" runat="server" Text="Updates" CssClass="copy10or" GroupName="Grp"></asp:radiobutton></TD>
											</TR>
										</TABLE>
									</TD>
								</TR>
								<TR borderColorLight="gray">
									<TD align="center">
										<TABLE id="Table3" cellSpacing="1" cellPadding="1" width="60%" border="0">
											<TR>
												<TD align="right"><FONT class="label">Title</FONT></TD>
												<TD></TD>
												<TD>
													<asp:textbox id="txtTitle" runat="server" CssClass="txfield1" Width="100%"></asp:textbox></TD>
											</TR>
											<TR>
												<TD class="label" align="right">Description</TD>
												<TD></TD>
												<TD>
													<asp:textbox id="txtDesc" runat="server" MaxLength="200" CssClass="txfield1" Width="100%" Rows="5"
														TextMode="MultiLine"></asp:textbox></TD>
											</TR>
											<TR>
												<TD align="right"><FONT class="label">Attachment</FONT></TD>
												<TD></TD>
												<TD><INPUT id="fAttach" type="file" size="30" name="fAttach" runat="server">
													<asp:Button id="btnAttach" runat="server" Text="Attach" CssClass="button"></asp:Button>
													<asp:HyperLink id="imgAttachment" runat="server" Target="_blank" ImageUrl="../images/attach.gif">HyperLink</asp:HyperLink>
													<DIV id="attachtxt" runat="server"></DIV>
												</TD>
											</TR>
											<TR>
												<TD class="label" align="right"></TD>
												<TD></TD>
												<TD class="labeltext"><INPUT id="fImage" style="DISPLAY: none" type="file" size="30" runat="server"></TD>
											</TR>
											<TR>
												<TD align="right"><FONT class="label">Date Start</FONT></TD>
												<TD></TD>
												<TD>
													<asp:textbox id="txtStart" runat="server" CssClass="txfield1"></asp:textbox><FONT class="label">(MM/DD/YYYY)</FONT></TD>
											</TR>
											<TR>
												<TD align="right"><FONT class="label">Link</FONT></TD>
												<TD></TD>
												<TD>
													<asp:textbox id="txtLink" runat="server" MaxLength="500" CssClass="txfield1"></asp:textbox></TD>
											</TR>
											<TR>
												<TD class="label" align="right"></TD>
												<TD></TD>
												<TD></TD>
											</TR>
											<TR>
												<TD align="center" colSpan="3">
													<asp:checkbox id="chkPublish" runat="server" Text="Publish" CssClass="label"></asp:checkbox></TD>
											</TR>
										</TABLE>
									</TD>
								</TR>
								<TR>
									<TD align="center" height="30">
										<asp:button id="btnSubmit" runat="server" Text="Submit" CssClass="button" OnClick="btnSubmit_Click1"></asp:button>&nbsp;&nbsp;
										<asp:button id="btnCancel" runat="server" Text="Cancel" CssClass="button"></asp:button></TD>
								</TR>
							</TABLE>
							<INPUT id="hdnItmImage" type="hidden" size="1" runat="server"><INPUT id="hdnID" type="hidden" size="1" name="hdnID" runat="server">
							<INPUT id="hdnItmDoc" type="hidden" size="2" name="hdnItmDoc" runat="server">
							<asp:Label id="lblError" runat="server"></asp:Label></TD>
					</TR>
				</TABLE>
			</asp:panel></form>
	</body>
</HTML>
