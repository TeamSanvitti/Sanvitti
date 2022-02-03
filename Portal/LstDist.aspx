<%@ Register TagPrefix="serv" TagName="MenuSP" Src="./Controls/provider.ascx" %>
<%@ Register TagPrefix="foot" TagName="MenuHeader" Src="./Controls/Footer.ascx" %>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="./Controls/Header.ascx" %>
<%@ Page language="c#" Codebehind="LstDist.aspx.cs" AutoEventWireup="false" Inherits="avii.LstDist" %>
<HTML>
	<HEAD>
		<title></title>
		<LINK href="aerostyle.css" type="text/css" rel="stylesheet">
			<script language="javascript" src="avI.js"></script>
	</HEAD>
	<BODY bgColor="#ffffff" leftMargin="0" topMargin="0" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="95%" align="center" border="0">
				<tr>
					<td vAlign="top" align="left"><head:menuheader id="MenuHeader1" runat="server"></head:menuheader></td>
				</tr>
			</table>
			<TABLE cellSpacing="0" cellPadding="0" width="95%" align="center" border="0">
				<TBODY>
					<TR>
						<TD vAlign="top" width="79%">
							<TABLE cellSpacing="1" cellPadding="2" width="100%" align="center" borderColorLight="gray"
								border="0">
								<TBODY>
									<TR borderColor="white">
										<TD vAlign="top" align="center">
											<DIV align="justify">
												<TABLE id="Table1" cellSpacing="1" cellPadding="1" width="100%" border="0">
													<TBODY>
														<TR>
															<TD class="button" width="1" rowSpan="2">&nbsp;</TD>
															<TD vAlign="top" width="12%">
																<div align="center">
																	<p><IMG id="imgManuf" style="DISPLAY: none" alt="" src="" name="imgManuf" runat="server">
																		<IMG id="imgPhone" alt="" src="" name="imgPhone" runat="server">
																	</p>
																</div>
															</TD>
															<TD class="button" width="1" rowSpan="2">&nbsp;</TD>
															<TD vAlign="top" rowSpan="3">
																<TABLE id="Table2" height="100%" cellSpacing="1" cellPadding="2" width="100%" border="0">
																	<TBODY>
																		<TR>
																			<TD class="button" colSpan="3"><asp:label id="lblPhoneTitle" runat="server"></asp:label></TD>
																		</TR>
																		<TR>
																			<TD class="copy11" width="15%"><span class="copyblue11b"><strong>Model:</strong></span><span class="linktop"><strong>&nbsp;</strong></span></TD>
																			<TD class="copy11"><asp:label id="lblModel" runat="server" CssClass="label"></asp:label></TD>
																			<TD class="copy11"><asp:label id="lblPhoneType" runat="server" CssClass="label"></asp:label></TD>
																		</TR>
																		<TR>
																			<TD class="copy11" colSpan="3"></TD>
																		</TR>
																		<TR>
																			<!--		<TD class="copy11"><span class="copyblue11b"><font class="copyblue11b"><strong>Price</strong>:</font></span></TD>
																			<TD class="copy11"><asp:label id="lblPrice" runat="server" CssClass="copyblue11b"></asp:label></TD>
																			<TD class="copy11">
																				<div align="right"><span class="copyblue11b"><FONT class="LabelBold">Quantity</FONT> <FONT class="LabelBold">
																						</FONT>
																					</span><FONT class="LabelBold"><asp:textbox onkeypress="return fnValueValidate(event,'i');" id="txtQty" runat="server" CssClass="txfield1"
																							Width="30px" MaxLength="3"></asp:textbox><asp:button id="Button2" runat="server" CssClass="button" Text="Order"></asp:button></FONT></div>
																			</TD>
																		--></TR>
																		<TR>
																			<TD colSpan="3"></TD>
																		</TR>
																		<TR>
																			<TD class="copyblue11b" colSpan="3">Description:</TD>
																		</TR>
																		<TR>
																			<TD class="copy10" colSpan="3"><asp:label id="lblDesc" runat="server" CssClass="Label"></asp:label></TD>
																		</TR>
																		<TR>
																			<TD align="center" colSpan="3">
																				<TABLE cellSpacing="1" cellPadding="2" width="100%" border="0">
																					<TR>
																						<TD class="copyblue11b" vAlign="top" width="10%" colSpan="3"><span class="copyblue11b">Feature(s):</span></TD>
																					</TR>
																					<tr>
																						<TD><asp:datagrid id="dgFeature" runat="server" Width="100%" AutoGenerateColumns="False" ShowHeader="False"
																								ShowFooter="False" BorderStyle="None" BorderColor="white">
																								<HeaderStyle CssClass="button"></HeaderStyle>
																								<Columns>
																									<asp:TemplateColumn ItemStyle-Width="2%" ItemStyle-HorizontalAlign="Right">
																										<ItemTemplate>
																											<asp:Literal ID="cc" Runat="server" Text="<li></li>"></asp:Literal>
																										</ItemTemplate>
																									</asp:TemplateColumn>
																									<asp:BoundColumn DataField="FeatureText" ItemStyle-CssClass="copy11" ItemStyle-BackColor="Gainsboro"
																										HeaderText="Feature"></asp:BoundColumn>
																								</Columns>
																							</asp:datagrid></TD>
																					</tr>
																				</TABLE>
																			</TD>
																		</TR>
																		<TR>
																			<TD class="copyblue11b" colSpan="3">Service Provider(s):</TD>
																		</TR>
																		<tr>
																			<td colSpan="3"><asp:datagrid id="dgSP" runat="server" Width="100%" AutoGenerateColumns="False" ShowHeader="False"
																					ShowFooter="False" BorderStyle="None" BorderColor="white">
																					<HeaderStyle CssClass="button"></HeaderStyle>
																					<Columns>
																						<asp:TemplateColumn ItemStyle-Width="2%" ItemStyle-HorizontalAlign="Right">
																							<ItemTemplate>
																								<asp:Literal ID="Literal1" Runat="server" Text="<li></li>"></asp:Literal>
																							</ItemTemplate>
																						</asp:TemplateColumn>
																						<asp:BoundColumn DataField="ServiceProvider" ItemStyle-CssClass="copy11" ItemStyle-BackColor="Gainsboro"
																							HeaderText="Feature"></asp:BoundColumn>
																					</Columns>
																				</asp:datagrid></td>
																		</tr>
																		<TR>
																			<TD class="copyblue11b" colSpan="3">Availability:</TD>
																		</TR>
																		<TR>
																			<TD class="copy10" colSpan="3"><asp:label id="lblAvail" runat="server" CssClass="Label"></asp:label></TD>
																		</TR>
																		<TR>
																			<TD class="copyblue11b"></TD>
																			<TD class="copy10" colSpan="2"><asp:label id="lblWarnt" runat="server" CssClass="Label"></asp:label></TD>
																		</TR>
																		<!--<TR>
																			<TD class="copyblue11b">Dimension:</TD>
																			<TD class="copy10" colSpan="2"><asp:label id="lblDim" runat="server" CssClass="Label"></asp:label></TD>
																		</TR>
																		
																		<TR>
																			<TD class="copyblue11b">Battery:</TD>
																			<TD class="copy10" colSpan="2"><asp:label id="lblBat" runat="server" CssClass="Label"></asp:label></TD>
																		</TR>-->
																		<TR>
																			<TD class="copy10" colSpan="3"><IMG height="1" src="LstDist_files/dotblue.gif" width="100%"></TD>
																		</TR>
																		<TR>
																			<TD class="copy10" colSpan="3"><IMG height="1" src="LstDist_files/dotblue.gif" width="100%"></TD>
																		</TR>
																		<tr>
																			<TD colSpan="3" align="right">
																				<asp:LinkButton id="LinkButton1" CssClass="copyblue11b" Runat="server">Continue</asp:LinkButton></TD>
																		</tr> <!--<TR align="right">
																			<TD colSpan="3" designtimesp=31917>
																				<DIV align="right" DESIGNTIMESP=31918><A href="frmCart.aspx" DESIGNTIMEURL="frmCart.aspx" DESIGNTIMESP=31919><IMG style="BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BORDER-BOTTOM-STYLE: none"
																							src="./images/cart.gif" DESIGNTIMEURL="./images/cart.gif" DESIGNTIMESP=31920></A></DIV>
																			</TD>
																		</TR>-->
																		<tr>
																			<td colSpan="3"><serv:menusp id="Menusp1" runat="server"></serv:menusp></td>
																		</tr>
																	</TBODY></TABLE>
															</TD>
														</TR>
														<TR>
															<TD>
																<TABLE cellSpacing="0" cellPadding="0" border="0">
																	<TR>
																		<TD vAlign="top"><asp:datagrid id="dgSP1" runat="server" Width="100%" AutoGenerateColumns="False" ShowHeader="False"
																				ShowFooter="False" BorderStyle="None" Visible="False">
																				<Columns>
																					<asp:TemplateColumn>
																						<ItemTemplate>
																							<asp:Image ID="spImage" Runat=server ImageUrl='<%#DataBinder.Eval (Container.DataItem,"SPImage") %>'>
																							</asp:Image>
																						</ItemTemplate>
																					</asp:TemplateColumn>
																				</Columns>
																			</asp:datagrid></TD>
																	</TR>
																</TABLE>
															</TD>
														</TR>
													</TBODY></TABLE>
											</DIV>
										</TD>
									</TR>
									<tr>
										<td><asp:panel id="pnlSim" Visible="false" Runat="server">
												<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
													<TR bgColor="#e8f3f9">
														<TD class="copyblue11b">FOUND Similar&nbsp;Items</TD>
													</TR>
													<TR>
														<TD>
															<TABLE cellSpacing="0" cellPadding="0" width="100%">
																<TR>
																	<TD>
																		<asp:datagrid id="dgItems" runat="server" AutoGenerateColumns="False" Width="100%" AllowPaging="False">
																			<HeaderStyle ForeColor="White" CssClass="button"></HeaderStyle>
																			<AlternatingItemStyle BackColor="Gainsboro"></AlternatingItemStyle>
																			<Columns>
																				<asp:BoundColumn ItemStyle-Width="20%" DataField="ManufName" ItemStyle-CssClass="copy11" HeaderText="Brand"
																					ReadOnly="True" SortExpression="ManufName"></asp:BoundColumn>
																				<asp:BoundColumn ItemStyle-Width="20%" DataField="PhoneType" ItemStyle-CssClass="copy11" HeaderText="Type"
																					ReadOnly="True" SortExpression="PhoneType"></asp:BoundColumn>
																				<asp:TemplateColumn HeaderText="Model" ItemStyle-Width="20%">
																					<ItemTemplate>
																						<asp:HyperLink id="lnk1" CssClass="copy11" Runat="server" Target=_blank NavigateUrl='<%# "./LstDist.aspx?pid=" + DataBinder.Eval (Container.DataItem,"ItemID") %>' TEXT = '<%#DataBinder.Eval(Container.DataItem,"PhoneModel")%>'>
																						</asp:HyperLink>
																					</ItemTemplate>
																				</asp:TemplateColumn>
																				<asp:BoundColumn ItemStyle-Width="30%" DataField="Phonetitle" ItemStyle-CssClass="copy11" HeaderText="Description"
																					ReadOnly="True" SortExpression="Phonetitle"></asp:BoundColumn>
																				<asp:BoundColumn ItemStyle-Width="5%" DataField="Special" ItemStyle-CssClass="copy11" HeaderText="Special"
																					ItemStyle-HorizontalAlign="Center" ReadOnly="True" SortExpression="Special" Visible="False"></asp:BoundColumn>
																			</Columns>
																		</asp:datagrid></TD>
																</TR>
															</TABLE>
														</TD>
													</TR>
												</TABLE>
											</asp:panel></td>
									</tr>
								</TBODY></TABLE>
						</TD>
					</TR>
				</TBODY></TABLE>
			<table cellSpacing="0" cellPadding="0" width="95%" align="center" border="0">
				<tr>
					<td colSpan="2"><foot:menuheader id="footer" runat="server"></foot:menuheader></td>
				</tr>
			</table>
		</form>
	</BODY>
</HTML>
