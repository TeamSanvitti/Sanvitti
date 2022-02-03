<%@ Page language="c#" Codebehind="edi.aspx.cs" AutoEventWireup="false" Inherits="avii.edi" %>
<HTML>
	<HEAD>
		<title>EDI</title>
		<script language="javascript" src="avI.js"></script>
		<LINK href="styles.css" type="text/css" rel="stylesheet">
			<LINK href="aerostyle.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<TABLE cellSpacing="1" cellPadding="1" width="785" border="1">
				<TR>
					<TD>
						<TABLE id="Table1" cellSpacing="1" cellPadding="1" width="300" border="1">
							<TR>
								<TD class="copy10grey" align="right">Address:</TD>
								<TD></TD>
								<TD><asp:textbox id="TextBox2" runat="server" CssClass="txfield1"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="copy10grey" align="right">City:</TD>
								<TD></TD>
								<TD><asp:textbox id="TextBox3" runat="server" CssClass="txfield1"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="copy10grey" align="right">State:</TD>
								<TD></TD>
								<TD><asp:textbox id="TextBox4" runat="server" CssClass="txfield1"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="copy10grey" align="right">Phone:</TD>
								<TD></TD>
								<TD><asp:textbox id="Textbox5" runat="server" CssClass="txfield1"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="copy10grey" align="right">Fax:</TD>
								<TD></TD>
								<TD><asp:textbox id="Textbox6" runat="server" CssClass="txfield1"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="copy10grey" align="right">Vendor#:</TD>
								<TD></TD>
								<TD><asp:textbox id="Textbox7" runat="server" CssClass="txfield1"></asp:textbox></TD>
							</TR>
						</TABLE>
					</TD>
					<TD>
						<TABLE id="Table2" cellSpacing="1" cellPadding="1" width="300" border="1">
							<TR>
								<TD></TD>
								<TD></TD>
								<TD class="copy10grey" align="right"></TD>
								<TD></TD>
							</TR>
							<TR>
								<TD class="copy10grey" align="right">PO#:</TD>
								<TD><asp:textbox id="TextBox8" runat="server" CssClass="txfield1"></asp:textbox></TD>
								<TD class="copy10grey" align="right">PO Date:</TD>
								<TD><asp:textbox id="TextBox15" runat="server" CssClass="txfield1"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="copy10grey" align="right">Ref ID:</TD>
								<TD><asp:textbox id="TextBox9" runat="server" CssClass="txfield1"></asp:textbox></TD>
								<TD class="copy10grey" align="right">Description:</TD>
								<TD><asp:textbox id="TextBox16" runat="server" CssClass="txfield1"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="copy10grey" align="right">WMS Load:</TD>
								<TD><asp:textbox id="TextBox10" runat="server" CssClass="txfield1"></asp:textbox></TD>
								<TD class="copy10grey" align="right">Load#</TD>
								<TD><asp:textbox id="TextBox17" runat="server" CssClass="txfield1"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="copy10grey" align="right">BOL#:</TD>
								<TD><asp:textbox id="TextBox11" runat="server" CssClass="txfield1"></asp:textbox></TD>
								<TD class="copy10grey" align="right">SKU:</TD>
								<TD><asp:textbox id="TextBox14" runat="server" CssClass="txfield1"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="copy10grey" align="right">Pallets:</TD>
								<TD><asp:textbox id="TextBox12" runat="server" CssClass="txfield1"></asp:textbox></TD>
								<TD></TD>
							</TR>
							<TR>
								<TD class="copy10grey" align="right">Case/Skid:</TD>
								<TD><asp:textbox id="TextBox13" runat="server" CssClass="txfield1"></asp:textbox></TD>
								<TD></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<tr>
					<td>
						<TABLE id="Table5" cellSpacing="1" cellPadding="1" width="300" border="1">
							<TR>
								<TD class="copy10grey" align="right">Billing To:</TD>
								<TD></TD>
								<TD><asp:textbox id="TextBox25" runat="server" CssClass="txfield1"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="copy10grey" align="right">Address:</TD>
								<TD></TD>
								<TD><asp:textbox id="TextBox26" runat="server" CssClass="txfield1"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="copy10grey" align="right">City:</TD>
								<TD></TD>
								<TD><asp:textbox id="TextBox27" runat="server" CssClass="txfield1"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="copy10grey" align="right">State:</TD>
								<TD></TD>
								<TD><asp:textbox id="TextBox28" runat="server" CssClass="txfield1"></asp:textbox></TD>
							</TR>
						</TABLE>
					</td>
					<td>
						<TABLE id="Table4" cellSpacing="1" cellPadding="1" width="300" border="1">
							<TR>
								<TD class="copy10grey" align="right">Pallets:</TD>
								<TD><asp:textbox id="TextBox18" runat="server" CssClass="txfield1"></asp:textbox></TD>
								<TD class="copy10grey" align="right">Case/Skid:</TD>
								<TD><asp:textbox id="Textbox20" runat="server" CssClass="txfield1"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="copy10grey" align="right">Unit Cases:</TD>
								<TD><asp:textbox id="TextBox19" runat="server" CssClass="txfield1"></asp:textbox></TD>
								<TD class="copy10grey" align="right">Measurement Type:</TD>
								<TD><asp:dropdownlist id="dpMeasure" runat="server"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD></TD>
								<TD></TD>
								<TD></TD>
							</TR>
						</TABLE>
					</td>
				</tr>
				<TR>
					<TD colSpan="2">
						<TABLE id="Table3" cellSpacing="1" cellPadding="1" width="100%" border="1">
							<TR>
								<TD class="copy10grey" align="right">Customer Name:</TD>
								<TD></TD>
								<TD><asp:textbox id="TextBox1" runat="server" CssClass="txfield1"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="copy10grey" align="right" height="19">Package Type:</TD>
								<TD height="19"></TD>
								<TD height="19"><asp:dropdownlist id="dpPack" runat="server"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD class="copy10grey" align="right">Carrier:</TD>
								<TD></TD>
								<TD><asp:textbox id="TextBox21" runat="server" CssClass="txfield1"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="copy10grey" align="right">Shipping Type:</TD>
								<TD></TD>
								<TD><asp:dropdownlist id="dpShip" runat="server"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD class="copy10grey" align="right">Shipping Date:</TD>
								<TD></TD>
								<TD><asp:textbox id="TextBox22" runat="server" CssClass="txfield1"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="copy10grey" align="right">Shipping Ref:</TD>
								<TD></TD>
								<TD><asp:textbox id="TextBox23" runat="server" CssClass="txfield1"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="copy10grey" align="right">Destination:</TD>
								<TD></TD>
								<TD>
									<asp:dropdownlist id="dpDest" runat="server"></asp:dropdownlist></TD>
							</TR>
						</TABLE>
					</TD>
					<TD></TD>
				</TR>
				<TR>
					<TD colSpan="2"><asp:datagrid id="dgCart" AlternatingItemStyle-BackColor="#E8F3F9" Width="100%" OnItemDataBound="dg_DataBound"
							OnItemCreated="dg_ItemCreated" OnCancelCommand="dg_Cancel" OnUpdateCommand="dg_Update" OnEditCommand="dg_Edit"
							OnItemCommand="dg_ItemCommand" ShowFooter="false" Runat="server" AllowPaging="false" AutoGenerateColumns="False">
							<HeaderStyle CssClass="button"></HeaderStyle>
							<HeaderStyle Font-Bold="True" ForeColor="White" CssClass="labelTextException" BackColor="#336699"></HeaderStyle>
							<Columns>
								<asp:EditCommandColumn ButtonType="LinkButton" UpdateText="Update" HeaderText="Select" CancelText="Cancel"
									EditText="Edit">
									<HeaderStyle CssClass="labelTextboldwhite"></HeaderStyle>
									<ItemStyle CssClass="LinkSmall"></ItemStyle>
								</asp:EditCommandColumn>
								<asp:ButtonColumn Text="Delete" HeaderText="Delete" CommandName="delete">
									<HeaderStyle CssClass="text10WhiteBold"></HeaderStyle>
									<ItemStyle CssClass="LinkSmall"></ItemStyle>
								</asp:ButtonColumn>
								<asp:TemplateColumn HeaderText="Line">
									<HeaderStyle Width="5%" CssClass="labelTextException"></HeaderStyle>
									<ItemTemplate>
										<asp:Label CssClass=labelText Runat =server text='<%#DataBinder.Eval(Container.DataItem,"LineID").ToString()%>' ID="lblfea">
										</asp:Label>
									</ItemTemplate>
									<EditItemTemplate>
										<asp:TextBox ID="txtLine" onkeypress="return fnValueValidate(event,'s');" Runat =server MaxLength ="3" Width ="80%" Text ='<%#DataBinder.Eval(Container.DataItem,"LineID").ToString()%>'>
										</asp:TextBox>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Item# (Cingular SKU">
									<HeaderStyle Width="15%" CssClass="labelTextException"></HeaderStyle>
									<ItemTemplate>
										<asp:Label CssClass=labelText Runat =server text='<%#DataBinder.Eval(Container.DataItem,"SKU").ToString()%>' ID="Label1">
										</asp:Label>
									</ItemTemplate>
									<EditItemTemplate>
										<asp:TextBox ID="Textbox29" onkeypress="return fnValueValidate(event,'s');" Runat =server MaxLength ="3" Width ="80%" Text ='<%#DataBinder.Eval(Container.DataItem,"SKU").ToString()%>'>
										</asp:TextBox>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Model">
									<HeaderStyle Width="15%" CssClass="labelTextException"></HeaderStyle>
									<ItemTemplate>
										<asp:Label CssClass=labelText Runat =server text='<%#DataBinder.Eval(Container.DataItem,"Model").ToString()%>' ID="Label2">
										</asp:Label>
									</ItemTemplate>
									<EditItemTemplate>
										<asp:TextBox ID="Textbox30" onkeypress="return fnValueValidate(event,'s');" Runat =server MaxLength ="3" Width ="80%" Text ='<%#DataBinder.Eval(Container.DataItem,"Model").ToString()%>'>
										</asp:TextBox>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Units">
									<HeaderStyle Width="15%" CssClass="labelTextException"></HeaderStyle>
									<ItemTemplate>
										<asp:Label CssClass=labelText Runat =server text='<%#DataBinder.Eval(Container.DataItem,"Units").ToString()%>' ID="Label3">
										</asp:Label>
									</ItemTemplate>
									<EditItemTemplate>
										<asp:TextBox ID="Textbox31" onkeypress="return fnValueValidate(event,'s');" Runat =server MaxLength ="3" Width ="80%" Text ='<%#DataBinder.Eval(Container.DataItem,"Units").ToString()%>'>
										</asp:TextBox>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Units Ordered">
									<HeaderStyle Width="15%" CssClass="labelTextException"></HeaderStyle>
									<ItemTemplate>
										<asp:Label CssClass=labelText Runat =server text='<%#DataBinder.Eval(Container.DataItem,"UOrdered").ToString()%>' ID="Label4">
										</asp:Label>
									</ItemTemplate>
									<EditItemTemplate>
										<asp:TextBox ID="Textbox32" onkeypress="return fnValueValidate(event,'s');" Runat =server MaxLength ="3" Width ="80%" Text ='<%#DataBinder.Eval(Container.DataItem,"UOrdered").ToString()%>'>
										</asp:TextBox>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Case Ordered">
									<HeaderStyle Width="15%" CssClass="labelTextException"></HeaderStyle>
									<ItemTemplate>
										<asp:Label CssClass=labelText Runat =server text='<%#DataBinder.Eval(Container.DataItem,"COrdered").ToString()%>' ID="Label5">
										</asp:Label>
									</ItemTemplate>
									<EditItemTemplate>
										<asp:TextBox ID="Textbox33" onkeypress="return fnValueValidate(event,'s');" Runat =server MaxLength ="3" Width ="80%" Text ='<%#DataBinder.Eval(Container.DataItem,"COrdered").ToString()%>'>
										</asp:TextBox>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Unit Shipped">
									<HeaderStyle Width="15%" CssClass="labelTextException"></HeaderStyle>
									<ItemTemplate>
										<asp:Label CssClass=labelText Runat =server text='<%#DataBinder.Eval(Container.DataItem,"UShipped").ToString()%>' ID="Label6">
										</asp:Label>
									</ItemTemplate>
									<EditItemTemplate>
										<asp:TextBox ID="Textbox34" onkeypress="return fnValueValidate(event,'s');" Runat =server MaxLength ="3" Width ="80%" Text ='<%#DataBinder.Eval(Container.DataItem,"UShipped").ToString()%>'>
										</asp:TextBox>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Cases Shipped">
									<HeaderStyle Width="15%" CssClass="labelTextException"></HeaderStyle>
									<ItemTemplate>
										<asp:Label CssClass=labelText Runat =server text='<%#DataBinder.Eval(Container.DataItem,"CShipped").ToString()%>' ID="Label7">
										</asp:Label>
									</ItemTemplate>
									<EditItemTemplate>
										<asp:TextBox ID="Textbox35" onkeypress="return fnValueValidate(event,'s');" Runat =server MaxLength ="3" Width ="80%" Text ='<%#DataBinder.Eval(Container.DataItem,"CShipped").ToString()%>'>
										</asp:TextBox>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Cases Back Ordered ">
									<HeaderStyle Width="15%" CssClass="labelTextException"></HeaderStyle>
									<ItemTemplate>
										<asp:Label CssClass=labelText Runat =server text='<%#DataBinder.Eval(Container.DataItem,"CbackOrder").ToString()%>' ID="Label8">
										</asp:Label>
									</ItemTemplate>
									<EditItemTemplate>
										<asp:TextBox ID="Textbox36" onkeypress="return fnValueValidate(event,'s');" Runat =server MaxLength ="3" Width ="80%" Text ='<%#DataBinder.Eval(Container.DataItem,"CbackOrder").ToString()%>'>
										</asp:TextBox>
									</EditItemTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle NextPageText="Next Page" PrevPageText="Previous Page&amp;nbsp;&amp;nbsp;" CssClass="text7"></PagerStyle>
						</asp:datagrid></TD>
				</TR>
				<TR>
					<TD colSpan="2"></TD>
				</TR>
				<TR>
					<TD align="center" colSpan="2"><asp:button id="btnSubmit" runat="server" CssClass="button" Width="122px" Text="Generate EDI"></asp:button>&nbsp;&nbsp;
						<asp:button id="btnCancel" runat="server" CssClass="button" Width="140px" Text="Cancel"></asp:button></TD>
				</TR>
			</TABLE>
		</form>
		&nbsp;
	</body>
</HTML>
