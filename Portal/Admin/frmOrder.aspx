<%@ Page language="c#" Codebehind="frmOrder.aspx.cs" AutoEventWireup="false" Inherits="avii.Admin.frmOrder" %>
<%--<%@ Register TagPrefix="head" TagName="MenuHeader" Src="./admhead.ascx" %>--%>
<%@ Register TagPrefix="head" TagName="menuheader" Src="~/dhtmlxmenu/menuControl.ascx" %>

<HTML>
	<HEAD>
		<title>Order Status</title>
		<LINK href="../aerostyle.css" type="text/css" rel="stylesheet">
			<LINK href="../style.css" type="text/css" rel="stylesheet">
				<script language="javascript" src="avI.js"></script>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
				<TABLE cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
					<TR>
						<TD>
							<head:menuheader id="MenuHeader" runat="server"></head:menuheader></TD>
					</TR>
				</TABLE>		
			<TABLE cellSpacing="1" cellPadding="1" width="780" border="0">
			
				<asp:panel id="pnlSearch" Runat="server">
					<TBODY>
						<TR>
							<TD class="SubTitle">
								<P>Order Search</P>
							</TD>
						</TR>
						<TR>
							<TD>
								<TABLE borderColor="gray" cellSpacing="0" cellPadding="0" width="100%" border="1">
									<TR>
										<TD>
											<TABLE id="Table2" cellSpacing="1" cellPadding="1" width="100%" border="0">
												<TR>
													<TD class="copy10grey">Order#:</TD>
													<TD></TD>
													<TD>
														<asp:TextBox id="txtOrderNo" runat="server" CssClass="txfield1"></asp:TextBox></TD>
													<TD class="copy10grey">Order Date:</TD>
													<TD></TD>
													<TD>
														<asp:TextBox id="txtOrderDt" runat="server" CssClass="txfield1"></asp:TextBox></TD>
												</TR>
												<TR>
													<TD class="copy10grey">Customer Last Name</TD>
													<TD></TD>
													<TD>
														<asp:TextBox id="txtCustLName" runat="server" CssClass="txfield1"></asp:TextBox></TD>
													<TD class="copy10grey">Customer First Name</TD>
													<TD></TD>
													<TD>
														<asp:TextBox id="txtCustFName" runat="server" CssClass="txfield1"></asp:TextBox></TD>
												</TR>
											</TABLE>
										</TD>
									</TR>
								</TABLE>
							</TD>
						</TR>
				</asp:panel>
				<TR>
					<TD align="center" colSpan="6"><asp:button id="btnSearch" runat="server" CssClass="button" Text="Search"></asp:button>&nbsp;&nbsp;
						<asp:button id="btnCancel" runat="server" CssClass="button" Text="Clear"></asp:button></TD>
				</TR>
				<TR>
					<TD>
						<DIV id="scroller">
							<asp:datagrid id="dgOrders" runat="server" OnItemCommand="dgOrditemcommand" Width="100%" AutoGenerateColumns="False"
								AllowSorting="True">
								<HeaderStyle CssClass="copyblue11b"></HeaderStyle>
								<Columns>
									<asp:TemplateColumn HeaderText="Order#" ItemStyle-Width="10%">
										<ItemTemplate>
											<asp:LinkButton ID="lnkOrder" CssClass="linktop" Runat=server CommandName="select" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"OrderID")%>' TEXT = '<%#DataBinder.Eval(Container.DataItem,"OrderID")%>'>
											</asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn ItemStyle-CssClass="copy11" ItemStyle-Width="11%" DataField="OrderDate" HeaderText="Order Date"
										ReadOnly="True" SortExpression="OrderDate"></asp:BoundColumn>
									<asp:BoundColumn ItemStyle-CssClass="copy11" ItemStyle-Width="19%" DataField="CustName" HeaderText="Customer Name"
										ReadOnly="True" SortExpression="CustName"></asp:BoundColumn>
									<asp:BoundColumn ItemStyle-CssClass="copy11" ItemStyle-Width="40%" DataField="Comments" HeaderText="Comments"
										ReadOnly="True"></asp:BoundColumn>
								</Columns>
							</asp:datagrid>
						</DIV>
					</TD>
				</TR>
				<asp:panel id="pnlAdd" Runat="server" Visible="False">
					<TR>
						<TD class="SubTitle">&nbsp;
						</TD>
					</TR>
					<TR>
						<TD class="SubTitle">
							<P>Order Status</P>
						</TD>
					</TR>
					<TR>
						<TD>
							<TABLE borderColor="gray" cellSpacing="0" cellPadding="0" width="100%" border="1">
								<TR>
									<TD>
										<TABLE id="Table3" cellSpacing="1" cellPadding="1" width="100%" border="0">
											<TR>
												<TD class="copy10grey">Order#:</TD>
												<TD></TD>
												<TD><asp:label id="txtOrderNum" runat="server" CssClass="copy11"></asp:label></TD>
												<TD class="copy10grey">
													<P align="right">Order Date:</P>
												</TD>
												<TD></TD>
												<TD><asp:label id="lblOrderDt" runat="server" CssClass="copy11"></asp:label></TD>
											</TR>
											<TR>
												<TD class="copy10grey">Customer Name:</TD>
												<TD></TD>
												<TD colSpan="4"><asp:label id="lblCustName" runat="server" CssClass="copy11"></asp:label></TD>
											</TR>
											<TR>
												<TD class="copy10grey">Address:</TD>
												<TD></TD>
												<TD colSpan="4"><asp:label id="lblAddr" runat="server" CssClass="copy11"></asp:label></TD>
											</TR>
											<TR>
												<TD class="copy10grey">City:</TD>
												<TD></TD>
												<TD><asp:label id="lblCity" runat="server" CssClass="copy11"></asp:label></TD>
												<TD class="copy10grey">
													<P align="right">Zip:</P>
												</TD>
												<TD></TD>
												<TD><asp:label id="lblZip" runat="server" CssClass="copy11"></asp:label></TD>
											</TR>
											<TR>
												<TD class="SubTitle" colSpan="6">Order Previous Status(s)</TD>
											</TR>
											<TR>
												<TD colSpan="6"><asp:datagrid id="dgStatus" Runat="server" OnItemCommand="dg_ItemCommand" Width="100%" AutoGenerateColumns="False"
														OnEditCommand="dg_Edit" OnUpdateCommand="dg_Update" OnCancelCommand="dg_Cancel" OnItemCreated="dg_ItemCreated"
														ShowFooter="false" AllowPaging="false">
														<HeaderStyle CssClass="button"></HeaderStyle>
														<Columns>
															<asp:EditCommandColumn ButtonType="LinkButton" UpdateText="Update" HeaderText="Select" CancelText="Cancel"
																EditText="Edit" ItemStyle-CssClass="copy11" ItemStyle-Width="20%"></asp:EditCommandColumn>
															<asp:ButtonColumn ItemStyle-Width="10%" CommandName="delete" Text="Delete" ItemStyle-CssClass="copy11"
																HeaderText="Delete"></asp:ButtonColumn>
															<asp:TemplateColumn HeaderText="Order Date" ItemStyle-Width="20%">
																<ItemTemplate>
																	<asp:Label CssClass=copy11 Runat =server text='<%#DataBinder.Eval(Container.DataItem,"OrderDate").ToString()%>' ID="lblDt">
																	</asp:Label>
																</ItemTemplate>
															</asp:TemplateColumn>
															<asp:TemplateColumn HeaderText="Comments" ItemStyle-Width="60%">
																<ItemTemplate>
																	<asp:Label CssClass=copy11 Runat =server text='<%#DataBinder.Eval(Container.DataItem,"Comments").ToString()%>' ID="lblComment">
																	</asp:Label>
																</ItemTemplate>
																<EditItemTemplate>
																	<asp:TextBox ID="txtComment" TextMode=MultiLine CssClass="txfield1" onkeypress="return fnValueValidate(event,'s');" Runat =server MaxLength ="500" Width ="80%" Text ='<%#DataBinder.Eval(Container.DataItem,"Comments").ToString()%>'>
																	</asp:TextBox>
																</EditItemTemplate>
															</asp:TemplateColumn>
															<asp:BoundColumn ItemStyle-CssClass="copy11" DataField="Ordernum" ReadOnly="true" Visible="true"></asp:BoundColumn>
														</Columns>
														<PagerStyle NextPageText="Next Page" PrevPageText="Previous Page&amp;nbsp;&amp;nbsp;" CssClass="text7"></PagerStyle>
													</asp:datagrid></TD>
											</TR>
										</TABLE>
									</TD>
								</TR>
							</TABLE>
					<TR>
						<TD align="center" colSpan="6"><asp:button id="btnClear" runat="server" CssClass="button" Text="Cancel"></asp:button></TD>
					</TR></TBODY></TABLE>
			</TD></TR></asp:panel></TBODY></TABLE></form>
	</body>
</HTML>
