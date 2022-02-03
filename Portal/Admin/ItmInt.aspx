<%--<%@ Register TagPrefix="head" TagName="MenuHeader" Src="./admhead.ascx" %>--%>
<%@ Register TagPrefix="head" TagName="menuheader" Src="~/dhtmlxmenu/menuControl.ascx" %>

<%@ Page language="c#" Codebehind="ItmInt.aspx.cs" AutoEventWireup="false" Inherits="avii.Admin.ItmInt" %>
<HTML>
	<HEAD>
		<title>Item Integration</title>
		<LINK href="../aeroStyle.css" type="text/css" rel="stylesheet">
			<LINK href="../Styles.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>


					<form id="Form1" method="post" runat="server">
		<TABLE cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
			<TR>
				<TD>
					<head:menuheader id="MenuHeader" runat="server"></head:menuheader></TD>
			</TR>
		</TABLE>					
						<asp:label id="outError" CssClass="ErrorMessage" Runat="server"></asp:label>
						<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR vAlign="top">
								<TD></TD>
							</TR>
							<TR vAlign="top">
								<TD>
									<TABLE id="Table1" cellSpacing="1" cellPadding="1" width="100%" border="0">
										<TR>
											<TD vAlign="top" width="15%" height="22"><font class="label">Item Name:</font></TD>
											<TD width="1%" height="22"></TD>
											<TD vAlign="top" height="22"><asp:dropdownlist id="dpItem" runat="server" CssClass="label" AutoPostBack="True"></asp:dropdownlist></TD>
										</TR>
										<TR>
											<TD align="center" colSpan="3"><asp:button id="btnSubmit" runat="server" CssClass="button" Text="Submit"></asp:button>&nbsp;&nbsp;
												<asp:button id="btnCancel" runat="server" CssClass="button" Text="Cancel"></asp:button></TD>
										</TR>
										<TR>
											<TD colSpan="3"><asp:panel id="pnlItem" Runat="server" Visible="False">
													<TABLE width="100%" border="0">
														<TR>
															<TD align="center" colSpan="2">
																<HR>
															</TD>
														</TR>
														<TR>
															<TD vAlign="top"><FONT class="SubTitle">Accessories&nbsp;Items</FONT></TD>
															<TD vAlign="top"><FONT class="SubTitle">Similar Items</FONT></TD>
														</TR>
														<TR>
														<TR>
															<TD vAlign="top">
																<TABLE borderColor="gray" cellSpacing="0" cellPadding="0" width="100%" border="1">
																	<TR vAlign="top" borderColor="white">
																		<TD>
																			<asp:datagrid id="dgItemInt" Runat="server" Width="100%" AutoGenerateColumns="False" AllowPaging="false"
																				ShowFooter="false">
																				<HeaderStyle Font-Bold="True" ForeColor="White" CssClass="labelTextException" BackColor="#336699"></HeaderStyle>
																				<Columns>
																					<asp:BoundColumn Visible="False" DataField="ItemID"></asp:BoundColumn>
																					<asp:BoundColumn DataField="PhoneModel" HeaderText="Model">
																						<ItemStyle Width="50%" CssClass="label"></ItemStyle>
																					</asp:BoundColumn>
																					<asp:BoundColumn DataField="PhoneTitle" HeaderText="Phone/Accessories">
																						<ItemStyle Width="50%" CssClass="label"></ItemStyle>
																					</asp:BoundColumn>
																					<asp:TemplateColumn HeaderText="Compatible">
																						<HeaderStyle Width="70%" CssClass="labelTextException"></HeaderStyle>
																						<ItemTemplate>
																							<asp:CheckBox ID="chkSP" Runat="server" Checked='<%#Convert.ToBoolean(DataBinder.Eval(Container.DataItem,"chk").ToString())%>'>
																							</asp:CheckBox>
																						</ItemTemplate>
																					</asp:TemplateColumn>
																				</Columns>
																				<PagerStyle NextPageText="Next Page" PrevPageText="Previous Page&amp;nbsp;&amp;nbsp;" CssClass="text7"></PagerStyle>
																			</asp:datagrid></TD>
																	</TR>
																</TABLE>
															</TD>
															<TD vAlign="top">
																<TABLE borderColor="gray" cellSpacing="0" cellPadding="0" width="100%" border="1">
																	<TR vAlign="top" borderColor="white">
																		<TD>
																			<asp:datagrid id="dgItemRel" Runat="server" Width="100%" AutoGenerateColumns="False" AllowPaging="false"
																				ShowFooter="false">
																				<HeaderStyle Font-Bold="True" ForeColor="White" CssClass="labelTextException" BackColor="#336699"></HeaderStyle>
																				<Columns>
																					<asp:BoundColumn DataField="ItemID" HeaderText="" Visible="False"></asp:BoundColumn>
																					<asp:BoundColumn DataField="PhoneModel" HeaderText="Model">
																						<ItemStyle Width="50%" CssClass="label"></ItemStyle>
																					</asp:BoundColumn>
																					<asp:BoundColumn DataField="PhoneTitle" HeaderText="Phone/Accessories">
																						<ItemStyle Width="50%" CssClass="label"></ItemStyle>
																					</asp:BoundColumn>
																					<asp:TemplateColumn HeaderText="Compatible">
																						<HeaderStyle Width="70%" CssClass="labelTextException"></HeaderStyle>
																						<ItemTemplate>
																							<asp:CheckBox ID="Checkbox1" Runat="server" Checked='<%#Convert.ToBoolean(DataBinder.Eval(Container.DataItem,"chk").ToString())%>'>
																							</asp:CheckBox>
																						</ItemTemplate>
																					</asp:TemplateColumn>
																				</Columns>
																				<PagerStyle NextPageText="Next Page" PrevPageText="Previous Page&amp;nbsp;&amp;nbsp;" CssClass="text7"></PagerStyle>
																			</asp:datagrid></TD>
																	</TR>
																</TABLE>
															</TD>
														</TR>
													</TABLE>
												</asp:panel></TD>
										</TR>
										<TR>
											<TD colSpan="3"></TD>
										</TR>
									</TABLE>
								</TD>
							</TR>
						</TABLE>
					</form>
	</body>
</HTML>
