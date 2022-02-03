<%@ Control Language="c#" AutoEventWireup="false" Codebehind="ctlPhone.ascx.cs" Inherits="avii.Controls.ctlPhone" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<script language="javascript">
		function fnSelectSortBy()
			{	//Submit form on a change of value for the SortBy select box
				strQueryString = location.search;
				strQueryString = strQueryString.split("&");
				strQuery = "";
				for (iloop=0; iloop<strQueryString.length; iloop++) 
				{
					if (strQueryString[iloop].match('mn') || strQueryString[iloop].match('pt') || strQueryString[iloop].match('sp'))
						tmp = 1;
					else
						if (strQuery.length > 0) 
							strQuery = strQuery +  "&" + strQueryString[iloop];
						else 	
							strQuery = strQueryString[iloop];
				}
				//alert(document.all._ctl0_dpType.selectedIndex);
				if (document.all._ctl0_dpType.selectedIndex > 0)
					strQuery = strQuery + "&pt=" + document.all._ctl0_dpType.options[document.all._ctl0_dpType.selectedIndex].value;
				if (document.all._ctl0_dpManuf.selectedIndex > 0)
					strQuery = strQuery + "&mn=" + document.all._ctl0_dpManuf.options[document.all._ctl0_dpManuf.selectedIndex].value;		
				if (document.all._ctl0_dpSP.selectedIndex > 0)
					strQuery = strQuery + "&sp=" + document.all._ctl0_dpSP.options[document.all._ctl0_dpSP.selectedIndex].value;		
		
				//alert(strQuery);							
				location.search = strQuery 
				window.location.reload;	
			}
	
</script>
<table cellSpacing="0" cellPadding="0" width="95%" align="center" border="0">
	<tr>
		<td class="bgleft" vAlign="top" width="200"><br>
			<table class="copy10grey2" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="button" align="center" bgColor="#dee7f6">PRODUCTS</td>
				</tr>
			</table>
			<div align="center"><br>
				<IMG height="131" src="/images/3_pic_2.jpg" width="182" border="0">
			</div>
			<div align="center"><br>
				<table width="100%" border="0">
					<tr>
						<td class="button" bgColor="#dee7f6" colSpan="2">SORT PRODUCTS BY:</td>
					</tr>
					<tr>
						<td class="copy11" colSpan="2"><IMG height="4" src="/images/dot1.gif" width="100%"></td>
					</tr>
					<tr>
						<td class="copy11" style="HEIGHT: 17px" vAlign="top" colSpan="2"><strong><font class="txt_4_b"><asp:dropdownlist id="dpType" Width="100%" name="dpType" Runat="server" onchange="return fnSelectSortBy()"
										CssClass="txfield1"></asp:dropdownlist></font></strong></td>
					</tr>
					<tr>
						<td style="HEIGHT: 17px"><strong><font class="txt_4_b"><asp:dropdownlist id="dpManuf" Width="100%" name="dpManuf" Runat="server" onchange="return fnSelectSortBy()"
										CssClass="txfield1"></asp:dropdownlist></font></strong></td>
					</tr>
					<tr>
						<td><strong><font class="txt_4_b"><asp:dropdownlist id="dpSP" Width="100%" name="dpSP" Runat="server" onchange="return fnSelectSortBy()"
										CssClass="txfield1"></asp:dropdownlist></font></strong></FONT></td>
					</tr>
				</table>
			</div>
			<br>
			<br>
			<BR>
		<td>
		<td width="1" bgColor="#a6bce0"><IMG height="1" src="/images/dotblue.gif" width="1"></td>
		<td vAlign="top">
			<table width="100%" border="0">
				<tr>
					<td vAlign="top" align="center">
						<asp:datalist id="dl" Width="100%" runat="server">
							<ItemTemplate>
								<table width="99%" align="center" borderColorLight="gray" border="0">
									<tr borderColor="white">
										<td align="center">
											<table border="0" cellspacing="2" cellpadding="1" width="100%">
												<tr>
													<td>
														<TABLE cellSpacing="1" cellPadding="1" width="100%" border="0">
															<TR>
																<TD class="button" width="1">&nbsp;</TD>
																<TD width="94">
																	<div align="center" w>
																		<a  href='LstDist.aspx?p=<%#Request["p"]%>&pid=<%#DataBinder.Eval(Container.DataItem,"Itemid")%>' ><img  border="0" src='<%#DataBinder.Eval(Container.DataItem,"PhoneImage")%>'/>
																		</a><span class="copy12hd"></span>
																	</div>
																</TD>
																<TD class="button" width="1">&nbsp;</TD>
																<TD vAlign="top">
																	<TABLE cellSpacing="1" cellPadding="1" width="100%" border="0">
																		<TR>
																			<TD class="button" colSpan="2">
																				<asp:Label ID="Label3" Runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ManufName")%>'>
																				</asp:Label>
																				&nbsp;&nbsp;
																				<asp:Label ID="Label4" Runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"PhoneModel")%>'>
																				</asp:Label>
																				&nbsp;&nbsp;
																				<asp:Label ID="Label5" Runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Cond_New")%>'>
																				</asp:Label>
																				<asp:Label ID="Label6" Runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Cond_Old")%>'>
																				</asp:Label>
																				<asp:Label ID="Label7" Runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Cond_Ref")%>'>
																				</asp:Label>
																			</TD>
																		</TR>
																		<TR bgColor="#e8f3f9">
																			<TD class="copy10grey" colSpan="2">
																				<asp:Label ID="Label10" CssClass="copy12hd" Runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"PhoneTitle")%>'>
																				</asp:Label>
																			</TD>
																		</TR>
																		<TR><td>&nbsp;</td>
																		<!--	<TD class="copy10"><span>Price:</span> <strong>
																					<asp:Label ID="Label8" CssClass="copy12hd" Runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Price")%>'>
																					</asp:Label></strong>
																			</TD>
																			<TD align="right">&nbsp;</TD>-->
																		</TR>
																		<TR>
																			<TD class="copy10"><SPAN>Availability:</SPAN> <STRONG>
																					<asp:Label ID="Label9" CssClass="copy10or" Runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Available")%>'>
																					</asp:Label></STRONG>
																			</TD>
																			<TD align="right" class="copy10" ><a Class="copy10" href='LstDist.aspx?p=<%#Request["p"]%>&pid=<%#DataBinder.Eval(Container.DataItem,"Itemid")%>' ><!--<img border="0" src="./images/buybt.gif" />-->more...</a></a>
																			</TD>
																		</TR>
																	</TABLE>
																</TD>
															</TR>
															<TR>
																<TD colspan="4"><IMG height="6" src="/images/dot1.gif" width="100%"></TD>
															</TR>
														</TABLE>
													</td>
												</tr>
											</table>
										</td>
									</tr>
								</table>
								<!--<table width="100%" border="0" cellspacing="1" cellpadding="1">
															<tr>
																<td width="60">
																	<div align="center">
																		<a  href='LstDist.aspx?pid=<%#DataBinder.Eval(Container.DataItem,"Itemid")%>' ><img width="94" height="107" border="0" src='<%#DataBinder.Eval(Container.DataItem,"PhoneImage")%>'/>
																		</a><span class="copy12hd"></span>
																	</div>
																</td>
																<td valign="top"><table width="100%" border="0" cellspacing="1" cellpadding="1">
																		<tr>
																			<td class="copy10grey" colspan="2">
																				<asp:Label ID="lblComp" CssClass="copy12hd" Runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ManufName")%>'>
																				</asp:Label>
																				<span class="copy12hd">:</span>
																				<asp:Label ID="lblModel" CssClass="copy12hd" Runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"PhoneModel")%>'>
																				</asp:Label>
																				&nbsp;&nbsp;
																				<asp:Label ID="lblNew" CssClass="copy12hd" Runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Cond_New")%>'>
																				</asp:Label>
																				<asp:Label ID="lblOld" CssClass="copy12hd" Runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Cond_Old")%>'>
																				</asp:Label>
																				<asp:Label ID="lblRef" CssClass="copy12hd" Runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Cond_Ref")%>'>
																				</asp:Label>
																				<img src="/images/dot1.gif" width="90%" height="2"><br>
																				<asp:Label ID="Label1" CssClass="copy12hd" Runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"PhoneTitle")%>'>
																				</asp:Label>
																			</td>
																		</tr>
																		<tr>
																			<td class="copy10"><span>Price :</span> <strong>
																					<asp:Label ID="lblPrice" CssClass="copy10or" Runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Price")%>'>
																					</asp:Label></strong>
																			</td>
																			<td align="right">
																				<a  href='LstDist.aspx?pid=<%#DataBinder.Eval(Container.DataItem,"Itemid")%>' ><img border="0" src="./images/buybt.gif" /></a>
																			</td>
																		</tr>
																		<tr>
																			<td class="copy10"><span>Available :</span> <strong>
																					<asp:Label ID="Label2" CssClass="copy10or" Runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Available")%>'>
																					</asp:Label></strong>
																			</td>
																		</tr>
																	</table>
																</td>
															</tr>
														</table>
													</td>
												</tr>
											</table>
													</td>
												</tr>
											</table>-->
							</ItemTemplate>
						</asp:datalist>
					</td>
				</tr>
				<tr>
					<td>
						<asp:Panel id="pnlNot" Runat="server" Visible="False"><BR><BR>
            <TABLE borderColor=gray cellSpacing=0 cellPadding=0 width="100%" 
            border=1>
              <TR borderColor=white>
                <TD align=center>
                  <TABLE width="80%">
                    <TR>
                      <TD height=20>&nbsp;</TD></TR>
                    <TR>
                      <TD><IMG height=1 src="/images/dotgreay.gif" 
                      width="100%"></TD></TR>
                    <TR>
                      <TD><IMG height=1 src="/images/dotgreay.gif" 
                      width="100%"></TD></TR>
                    <TR>
                      <TD class=copy12hd align=center>The item you are looking 
                        for is currently not available. Please call us to get 
                        more information or come back soon for more 
                    updates.</TD></TR>
                    <TR>
                      <TD><IMG height=1 src="/images/dotgreay.gif" 
                      width="100%"></TD></TR>
                    <TR>
                      <TD><IMG height=1 src="/images/dotgreay.gif" 
                      width="100%"></TD></TR>
                    <TR>
                      <TD 
            height=20>&nbsp;</TD></TR></TABLE></TD></TR></TABLE><BR><BR>
						</asp:Panel>
					</td>
				</tr>
			</table>
		</td>
	</tr>
</table>
