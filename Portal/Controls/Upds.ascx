<%@ Control Language="c#" AutoEventWireup="false" Codebehind="Upds.ascx.cs" Inherits="avii.Controls.Upds" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<table width="100%" border="0">
	<tr>
		<td vAlign="top" align="center"><asp:datalist id="dl" OnItemDataBound="itmCmd" Width="100%" runat="server">
				<ItemTemplate>
					<table width="99%" align="center" borderColorLight="gray" border="0">
						<tr borderColor="white">
							<td align="center">
								<table border="0" cellspacing="2" cellpadding="1" width="100%">
									<tr>
										<td>
											<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
												<TR>
													<TD class="button" width="1">&nbsp;</TD>
													<!--<TD width="60">
														<div align="center">
															<img width="94" height="107" border="0" src='<%#"./content/"+DataBinder.Eval(Container.DataItem,"UImage")%>'/>
														</div>
													</TD>
													<TD class="button" width="1">&nbsp;</TD>-->
													<TD vAlign="top">
														<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
															<TR>
																<TD class="linktop" bgColor="#e8f3f9" width="97%">
																	<asp:Label ID="linktop" Runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Title")%>'>
																	</asp:Label>
																	<asp:HyperLink ID="hlinktop"  Runat=server NavigateUrl='<%#DataBinder.Eval(Container.DataItem,"ulink")%>' >
																		<%#DataBinder.Eval(Container.DataItem,"Title")%>
																	</asp:HyperLink>
																</TD>
																<td width="3%" bgColor="#e8f3f9" class="label">
																	&nbsp;
																	<asp:HyperLink id="imgAttachment" runat="server" NavigateUrl='<%#"../content/" + DataBinder.Eval(Container.DataItem,"udoc")%>' ImageUrl="../images/attach.gif" Target="_blank">
																	</asp:HyperLink>
																</td>
															</TR>
															<TR>
																<TD>
																	<asp:Label ID="Label1" CssClass="label" Runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"UDesc")%>'>
																	</asp:Label>
																</TD>
															</TR>
														</TABLE>
													</TD>
												</TR>
												<TR>
													<TD colspan="4"><IMG height="6" src="images/dot1.gif" width="100%"></TD>
												</TR>
											</TABLE>
										</td>
									</tr>
								</table>
							</td>
						</tr>
					</table>
				</ItemTemplate>
			</asp:datalist></td>
	</tr>
	<tr>
		<td align="center"><asp:label id="msg" Visible="False" Runat="server" CssClass="copyblue11b"></asp:label></td>
	</tr>
</table>
