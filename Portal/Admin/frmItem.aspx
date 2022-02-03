<%@ Page language="c#" Codebehind="frmItem.aspx.cs" AutoEventWireup="false" Inherits="avii.Admin.frmItem" %>
<%--<%@ Register TagPrefix="head" TagName="MenuHeader" Src="./admhead.ascx" %>--%>
<%@ Register TagPrefix="head" TagName="menuheader" Src="~/dhtmlxmenu/menuControl.ascx" %>

<HTML>
	<HEAD>
		<title>Form Items</title>
		<LINK href="../aerostyle.css" type="text/css" rel="stylesheet">
			<LINK href="../Styles.css" type="text/css" rel="stylesheet">
				<script language="javascript" src="../avI.js"></script>
				<script language="javascript">
			function fnValidate()
			{
				var smsg = '';
				if (document.all.dpPType.selectedIndex == 0)
					smsg =  smsg + '\n- Phone Type';
				if (document.all.dpManu.selectedIndex == 0)
					smsg =  smsg + '\n- Phone Manufacturer';
				if (document.all.txtModel.value.length == 0)
					smsg = smsg +  '\n- Phone Model';
				if (document.all.txtPrice.value.length == 0)
					smsg =  smsg + '\n- Phone Price';
				if (document.all.fImage.value.length == 0)
					smsg =  smsg + '\n- Phone Image';
				if (document.all.chkNew.checked == false && document.all.chkUsed.checked == false && document.all.chkRef.checked == false)
					smsg =  smsg + '\n- Phone Condition (New/Used/Refurbished)';
				if (smsg.length > 0){
					alert('Please enter following requird values:\n' + smsg);
					return false;
				}
				else
					return true;
			}
			
				</script>
	</HEAD>
	<body bgColor="#ffffff" leftmargin="0" rightmargin="0" topmargin="0">
		<form id="Form1" method="post" encType="multipart/form-data" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
				<TR>
					<TD>
						<head:menuheader id="MenuHeader" runat="server"></head:menuheader></TD>
				</TR>
			</TABLE>
			<table width="99%" align="center" borderColorLight="gray" border="1">
				<tr borderColor="white">
					<td align="center">
						<TABLE id="Table1" cellSpacing="1" cellPadding="1" width="99%" border="0">
							<TR>
								<TD colSpan="6">
									<asp:Label id="lblError" runat="server" CssClass="errormessage"></asp:Label></TD>
							</TR>
							<TR>
								<TD width="15%">
									<P align="right"><font class="label">Phone/Acc Type</font></P>
								</TD>
								<TD width="1%"><font class="RequiredField">*</font></TD>
								<TD width="25%"><asp:dropdownlist id="dpPType" runat="server"></asp:dropdownlist></TD>
								<TD width="12%">
									<P align="right"><font class="label">Manufacturer</font></P>
								</TD>
								<TD width="1%" height="14"><font class="RequiredField">*</font></TD>
								<TD width="25%" height="14"><asp:dropdownlist id="dpManu" runat="server">
										<asp:ListItem Value=""></asp:ListItem>
										<asp:ListItem Value="1">Audiovox</asp:ListItem>
										<asp:ListItem Value="2">LG</asp:ListItem>
										<asp:ListItem Value="3">Samsung</asp:ListItem>
									</asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD>
									<P align="right"><font class="label">Phone Model</font></P>
								</TD>
								<TD><font class="RequiredField">*</font></TD>
								<TD><asp:textbox onkeypress="return fnValueValidate(event,'s');" id="txtModel" runat="server" CssClass="txfield1"
										Width="80%" MaxLength="20"></asp:textbox></TD>
								<TD></TD>
								<TD></TD>
								<TD><asp:checkbox id="chkSpecial" runat="server" CssClass="label" Text="Special"></asp:checkbox></TD>
							</TR>
							<TR>
								<TD>
									<P align="right"><font class="label">Phone Name</font></P>
								</TD>
								<TD><font class="RequiredField">*</font></TD>
								<TD colSpan="4"><asp:textbox onkeypress="return fnValueValidate(event,'s');" id="txtName" runat="server" CssClass="txfield1"
										Width="80%" MaxLength="100"></asp:textbox></TD>
							</TR>
							<TR>
								<TD vAlign="top" align="right"><font class="label">Phone Features</font>
								</TD>
								<td></td>
								<TD colSpan="4"><asp:datagrid id="dgFeatures" Width="100%" OnItemCommand="dg_ItemCommand" OnEditCommand="dg_Edit"
										OnUpdateCommand="dg_Update" OnCancelCommand="dg_Cancel" OnItemCreated="dg_ItemCreated" ShowFooter="false"
										Runat="server" AllowPaging="false" AutoGenerateColumns="False">
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
											<asp:TemplateColumn HeaderText="Phone Features">
												<HeaderStyle Width="70%" CssClass="labelTextException"></HeaderStyle>
												<ItemTemplate>
													<asp:Label CssClass=labelText Runat =server text='<%#DataBinder.Eval(Container.DataItem,"FeatureText").ToString()%>' ID="lblfea">
													</asp:Label>
												</ItemTemplate>
												<EditItemTemplate>
													<asp:TextBox ID="txtCatg" onkeypress="return fnValueValidate(event,'s');" Runat =server MaxLength ="400" Width ="80%" Text ='<%#DataBinder.Eval(Container.DataItem,"FeatureText").ToString()%>'>
													</asp:TextBox>
												</EditItemTemplate>
											</asp:TemplateColumn>
										</Columns>
										<PagerStyle NextPageText="Next Page" PrevPageText="Previous Page&amp;nbsp;&amp;nbsp;" CssClass="text7"></PagerStyle>
									</asp:datagrid></TD>
								<td rowSpan="5"><asp:image id="imgPhone" runat="server"></asp:image></td>
							</TR>
							<tr>
								<TD vAlign="top" align="right"><font class="label">Phone Description</font>
								</TD>
								<td></td>
								<TD colSpan="4"><asp:textbox onkeypress="return fnValueValidate(event,'s');" id="txtDesc" runat="server" CssClass="txfield1"
										Width="100%" TextMode="MultiLine" MaxLength="2000"></asp:textbox></TD>
							</tr>
							<tr>
								<TD>
									<P align="right"><font class="label">Phone Image</font></P>
								</TD>
								<td><font class="RequiredField"></font></td>
								<TD colSpan="4"><INPUT class="labeltext" onkeypress="return fnValueValidate(event,'s');" id="fImage" style="WIDTH: 498px; HEIGHT: 22px"
										type="file" size="63" name="fImage" runat="server"></TD>
							</tr>
							<TR>
								<TD vAlign="top" align="right"><font class="label">Phone Price</font>
								</TD>
								<td vAlign="top"><font class="RequiredField">*</font></td>
								<TD colSpan="4"><asp:datagrid id="dgPrice" Width="100%" ShowFooter="false" Runat="server" AllowPaging="false"
										AutoGenerateColumns="False">
										<HeaderStyle Font-Bold="True" ForeColor="White" CssClass="labelTextException" BackColor="#336699"></HeaderStyle>
										<Columns>
											<asp:BoundColumn DataField="CustType" HeaderText="Customer Type">
												<ItemStyle Width="50%" CssClass="label"></ItemStyle>
											</asp:BoundColumn>
											<asp:TemplateColumn HeaderText="Phone Price">
												<HeaderStyle Width="50%" CssClass="labelTextException"></HeaderStyle>
												<ItemTemplate>
													<asp:TextBox ID="txtCustPrice" CssClass="Textsmall" onkeypress="return fnValueValidate(event,'s');" Runat =server MaxLength ="35" Width ="80%" Text ='<%#DataBinder.Eval(Container.DataItem,"Price").ToString()%>'>
													</asp:TextBox>
												</ItemTemplate>
											</asp:TemplateColumn>
										</Columns>
										<PagerStyle NextPageText="Next Page" PrevPageText="Previous Page&amp;nbsp;&amp;nbsp;" CssClass="text7"></PagerStyle>
									</asp:datagrid></TD>
							</TR>
							<TR>
								<TD vAlign="top" align="right"><font class="label">Service Provider</font>
								</TD>
								<td vAlign="top"><font class="RequiredField">*</font></td>
								<TD colSpan="4"><asp:datagrid id="dgSP" Width="100%" ShowFooter="false" Runat="server" AllowPaging="false" AutoGenerateColumns="False">
										<HeaderStyle Font-Bold="True" ForeColor="White" CssClass="labelTextException" BackColor="#336699"></HeaderStyle>
										<Columns>
											<asp:BoundColumn DataField="SPID" HeaderText="Service Provider" Visible="False"></asp:BoundColumn>
											<asp:BoundColumn DataField="ServiceProvider" HeaderText="Service Provider">
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
							<tr>
								<TD>
									<P align="right"><font class="label">Phone Condition</font></P>
								</TD>
								<td><font class="RequiredField">*</font></td>
								<TD colSpan="4"><asp:checkbox id="chkNew" runat="server" CssClass="label" Text="New"></asp:checkbox>&nbsp;&nbsp;&nbsp;&nbsp;<asp:checkbox id="chkUsed" runat="server" CssClass="label" Text="Used"></asp:checkbox>
									&nbsp;&nbsp;&nbsp;&nbsp;<asp:checkbox id="chkRef" runat="server" CssClass="label" Text="Refurbished"></asp:checkbox></TD>
							</tr>
							<!--	<TR>
								<TD align="right"><font class="label">Phone Warrenty</font>
								</TD>
								<TD></TD>
								<TD colSpan="4"><asp:textbox onkeypress="return fnValueValidate(event,'s');" id="txtWarnt" runat="server" CssClass="txfield1"
										Width="300px" MaxLength="200"></asp:textbox></TD>
							</TR>
							
							<TR>
								<TD align="right">
									<font class="label">Phone Dimension</font>
								</TD>
								<TD></TD>
								<TD colSpan="4"><asp:textbox onkeypress="return fnValueValidate(event,'m');" id="txtDim" runat="server" CssClass="labeltext"
										Width="300px" MaxLength="200"></asp:textbox></TD>
							</TR>
							<TR>
								<TD align="right">
									<font class="label">Battery</font>
								</TD>
								<TD></TD>
								<TD colSpan="4"><asp:textbox onkeypress="return fnValueValidate(event,'s');" id="txtBat" runat="server" CssClass="labeltext"
										Width="300px" MaxLength="200"></asp:textbox></TD>
							</TR>
							-->
							<TR>
								<TD>
									<P align="right"><font class="label">Hide Item(Deactivate)</font></P>
								</TD>
								<TD></TD>
								<TD colSpan="4"><asp:checkbox id="chkActive" runat="server" CssClass="label"></asp:checkbox></TD>
							</TR>
							<TR>
								<TD>
									<P align="right"><font class="label">Hide Price</font></P>
								</TD>
								<TD></TD>
								<TD colSpan="4"><asp:checkbox id="chkPrice" runat="server" CssClass="label"></asp:checkbox></TD>
							</TR>
							<TR>
								<TD>
									<P align="right"><font class="label">Phone Availability</font></P>
								</TD>
								<TD></TD>
								<TD colSpan="4"><asp:dropdownlist id="dpAvail" runat="server" CssClass="txfield1">
										<asp:ListItem Value="In stock">In stock</asp:ListItem>
										<asp:ListItem Value="Limited">Limited</asp:ListItem>
										<asp:ListItem Value="Not available">Not available</asp:ListItem>
										<asp:ListItem Value="Please call">Please call</asp:ListItem>
									</asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD align="center" colSpan="6"><asp:button id="btnSubmit" runat="server" CssClass="button" Text="Submit" OnClick="btnSubmit_Click1"></asp:button>&nbsp;&nbsp;&nbsp;&nbsp;<asp:button id="btnCancel" runat="server" CssClass="button" Text="Cancel"></asp:button>
									<asp:button id="btnAddNew" runat="server" CssClass="button" Text="Add New Item"></asp:button></TD>
							</TR>
						</TABLE>
					</td>
				</tr>
			</table>
			<INPUT id="hdnItmImage" type="hidden" runat="server">
		</form>
	</body>
</HTML>
