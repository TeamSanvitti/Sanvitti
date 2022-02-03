<%@ Page language="c#" Codebehind="frmRMARep.aspx.cs" AutoEventWireup="false" Inherits="avii.Admin.frmRMARep" %>
<%--<%@ Register TagPrefix="head" TagName="MenuHeader" Src="./admhead.ascx" %>--%>
<%@ Register TagPrefix="head" TagName="menuheader" Src="~/dhtmlxmenu/menuControl.ascx" %>

<HTML>
	<HEAD>
		<title></title>
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
											<td class="copyblue11b">&nbsp;RMA Report:</td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
				    <td>
				        <asp:Label ID="lblError" runat="server" CssClass="errormessage"></asp:Label>
				    </td>
				</tr>

				<tr>
					<td>
						<table borderColor="gray" cellSpacing="0" cellPadding="0" width="98%" border="1">
							<tr borderColor="white">
								<td>
									<table width="100%">
										<tr>
											<td class="copy11">RMA DATE:</td>
											<td><asp:textbox onkeypress="return fnValueValidate(event,'d');" id="txtDate" onfocus="this.blur()"
													runat="server" maxLength="10" size="10" CssClass="txfield1"></asp:textbox><A href="javascript:cal6.popup();"><IMG height="16" alt="Click Here to Pick up the date" src="../images/cal.gif" width="16"
														border="0"></A></td>
											<td class="copy11">ACCOUNT NUMBER:</td>
											<td><asp:textbox onkeypress="return fnValueValidate(event,'s');" id="txtAccNum" runat="server" CssClass="txfield1"
													MaxLength="30"></asp:textbox></td>
										</tr>
										<tr>
											<td class="copy11COMPANY NAME:</td>
											<td><asp:textbox onkeypress="return fnValueValidate(event,'s');" id="txtComp" runat="server" CssClass="txfield1"
													MaxLength="100"></asp:textbox></td>
											<td class="copy11INVOICE NUMBER:</td>
											<td><asp:textbox onkeypress="return fnValueValidate(event,'s');" id="txtInv" runat="server" CssClass="txfield1"
													MaxLength="30"></asp:textbox></td>
										</tr>
										<tr>
											<td class="copy11">STATUS:</td>
											<td colSpan="3">
											<asp:dropdownlist id="dpStatusQ" runat="server" CssClass="txfield1">
								                <asp:ListItem Text="" Value=""></asp:ListItem>
								                <asp:ListItem Text="Pending for Credit" Value="0"></asp:ListItem>
								                <asp:ListItem Text="Pending for Replacement" Value="1"></asp:ListItem>
								                <asp:ListItem Text="Pending for Repair" Value="2"></asp:ListItem>
								                <asp:ListItem Text="RMA Approve" Value="3"></asp:ListItem>
								                <asp:ListItem Text="RMA Denied" Value="4"></asp:ListItem>
								            </asp:dropdownlist></td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td align="center">
                        <asp:button id="btnSrch" runat="server" CssClass="button" 
                            Text="Search" onclick="btnSrch_Click2"></asp:button>&nbsp;&nbsp;&nbsp;
						<asp:button id="btnCancel" runat="server" CssClass="button" Text="Cancel" 
                            onclick="btnCancel_Click1" ></asp:button></td>
				</tr>
				<tr>
					<td align="right">
					    <asp:button ID="btnSubmitStatus" runat="server"  CssClass="button" Visible="false"
                            Text="Change Status for RMA" CausesValidation="False" onclick="btnSubmitStatus_Click" 
                             />
                    </td>
				</tr>
				<tr>
					<td><asp:datagrid id="dgRMA" runat="server" Width="100%" AutoGenerateColumns="False" DataKeyField="RMAID" 
					            OnDeleteCommand="dg_delete" OnItemDataBound="dgRMA_DataBound">
							<HeaderStyle CssClass="button"></HeaderStyle>
							<Columns>
								<asp:ButtonColumn ButtonType="LinkButton" CommandName="delete" Text="Delete" HeaderText="Action" ItemStyle-Width="5%"></asp:ButtonColumn>
								<asp:TemplateColumn HeaderText="RMA#" ItemStyle-Width="10%">
									<ItemTemplate>
										<asp:HyperLink id="lnk1" CssClass="copy11" Runat="server" NavigateUrl='<%# "../frmRima.aspx?rid=" + DataBinder.Eval (Container.DataItem,"RMAID") %>' TEXT = '<%#DataBinder.Eval(Container.DataItem,"RMANUM")%>'>
										</asp:HyperLink>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn ItemStyle-CssClass="copy11" DataField="RMAID" HeaderText="RMAID" Visible="False"
									ReadOnly="True" SortExpression="RMAID"></asp:BoundColumn>
								<asp:BoundColumn ItemStyle-CssClass="copy11" DataField="RMADt" HeaderText="RMA Date" ReadOnly="True"
									SortExpression="RMADt" ItemStyle-Width="10%"></asp:BoundColumn>
								<asp:BoundColumn ItemStyle-Width="15%" ItemStyle-CssClass="copy11" DataField="Company" HeaderText="Company"
									ReadOnly="True"></asp:BoundColumn>
								<asp:BoundColumn ItemStyle-Width="15%" ItemStyle-CssClass="copy11" DataField="ContactName" HeaderText="ContactName"
									ReadOnly="True"></asp:BoundColumn>
								<asp:BoundColumn ItemStyle-Width="10%" ItemStyle-CssClass="copy11" DataField="AcctNum" HeaderText="Account#"
									ReadOnly="True"></asp:BoundColumn>
								<asp:BoundColumn ItemStyle-Width="10%" ItemStyle-CssClass="copy11" DataField="InvNum" HeaderText="Invoice#"
									ReadOnly="True"></asp:BoundColumn>
								<asp:BoundColumn ItemStyle-CssClass="copy11" Visible="false" ItemStyle-Width="5%" DataField="Status" HeaderText="Status" ReadOnly="True"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Approve" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center" Visible="False">
									<ItemTemplate>
										<asp:RadioButton ID="rbApp" Runat="server" GroupName="grp"></asp:RadioButton>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Deny" ItemStyle-Width="5%" Visible="False" ItemStyle-HorizontalAlign="Center">
									<ItemTemplate>
										<asp:RadioButton ID="rbDny" Runat="server" GroupName="grp"></asp:RadioButton>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn  HeaderText="Change Status" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
								    <ItemTemplate>
								        <asp:DropDownList ID="dpStatus" runat="server">
								            <asp:ListItem Text="" Value=""></asp:ListItem>
								            <asp:ListItem Text="Pending for Credit" Value="0"></asp:ListItem>
								            <asp:ListItem Text="Pending for Replacement" Value="1"></asp:ListItem>
								            <asp:ListItem Text="Pending for Repair" Value="2"></asp:ListItem>
								            <asp:ListItem Text="RMA Approve" Value="3"></asp:ListItem>
								            <asp:ListItem Text="RMA Denied" Value="4"></asp:ListItem>
								        </asp:DropDownList>
								    </ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
						</asp:datagrid></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
