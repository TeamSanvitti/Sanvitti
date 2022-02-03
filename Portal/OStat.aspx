<%@ Register TagPrefix="foot" TagName="MenuHeader" Src="./Controls/Footer.ascx" %>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="./Controls/Header.ascx" %>
<%@ Page language="c#" Codebehind="OStat.aspx.cs" AutoEventWireup="false" Inherits="avii.OStat" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Order Status</title>
		<LINK href="aerostyle.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="avI.js"></script>
	</HEAD>
	<body bgColor="#ffffff" leftMargin="0" topMargin="0" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="95%" align="center" border="0">
				<tr>
					<td vAlign="top" align="left"><head:menuheader id="MenuHeader1" runat="server"></head:menuheader></td>
				</tr>
				<TR>
					<TD><asp:label id="lblErr" runat="server" CssClass="errormessage"></asp:label></TD>
				</TR>
				<tr>
					<td vAlign="top">&nbsp;</td>
				</tr>
				<tr>
					<td vAlign="top">&nbsp;</td>
				</tr>
				<TR>
					<TD align="center">
						<table borderColor="gray" cellSpacing="0" cellPadding="0" width="60%" border="1">
							<tr borderColor="white">
								<td>
									<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
										<TR>
											<TD class="button" colSpan="3">Order Status</TD>
										</TR>
										<TR>
											<TD colSpan="3">&nbsp;</TD>
										</TR>
										<TR>
											<TD class="copy11" align="right" width="20%">Order#:</TD>
											<TD width="1%"></TD>
											<TD width="79%"><asp:textbox onkeypress="return fnValueValidate(event,'i');" id="txtONum" runat="server" cssclass="txfield1" Width="80%"></asp:textbox></TD>
										</TR>
										<TR>
											<TD colSpan="3">&nbsp;</TD>
										</TR>
									</TABLE>
								</td>
							</tr>
						</table>
					</TD>
				</TR>
				<TR>
					<TD align="center" colSpan="3" height="10"></TD>
				</TR>
				<TR>
					<TD align="center" colSpan="3"><asp:button id="btnSubmit" runat="server" CssClass="button" Text="Submit"></asp:button>&nbsp;&nbsp;<asp:button id="btnCancel" runat="server" CssClass="button" Text="Cancel"></asp:button></TD>
				</TR>
				<TR>
					<TD align="center" colSpan="3">&nbsp;</TD>
				</TR>
				<TR>
					<TD align="center"><asp:datagrid id="dgOrder" runat="server" Width="60%" AutoGenerateColumns="False">
							<HeaderStyle CssClass="button"></HeaderStyle>
							<Columns>
								<asp:BoundColumn DataField="OrderDate" ItemStyle-Width="20%" DataFormatString="{0:d}" HeaderText="Order Date"
									ItemStyle-CssClass="copy11"></asp:BoundColumn>
								<asp:BoundColumn DataField="Comments" ItemStyle-Width="80%" HeaderText="Description" ItemStyle-CssClass="copy11"></asp:BoundColumn>
							</Columns>
						</asp:datagrid></TD>
				</TR>
				<TR>
					<TD align="center" colSpan="3">&nbsp;</TD>
				</TR>
				<TR>
					<TD align="center" colSpan="3">&nbsp;</TD>
				</TR>
				<TR>
					<TD><foot:menuheader id="Menuheader2" runat="server"></foot:menuheader></TD>
				</TR>
			</table>
		</form>
	</body>
</HTML>
