<%@ Control Language="c#" AutoEventWireup="false" Codebehind="admHead.ascx.cs" Inherits="avii.Admin.admHead" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<asp:panel id="pnlmenu" Runat="server"  visible="false">
	<TABLE cellSpacing="0" cellPadding="4" width="100%" border="0">
		<TR bgColor="gainsboro">
			<TD class="label"><A href="/admin/admusr.aspx">Admin User</A></TD>
			<TD class="label"><A href="/admin/company/CustomerQuery.aspx">Customer Query</A></TD>
			<TD class="label"><A href="/admin/company/customerForm.aspx">Customer Form</A></TD>
			<TD class="label"><A href="/admin/forecastadmin.aspx">Forecast</A></TD>
			<TD class="label"><A href="/RMA/RMAQuery.aspx">RMA Query</A></TD>
			<TD class="label"><A href="/RMA/RMAForm.aspx?mode=esn">RMA Add</A></TD>
			<TD class="label"><A href="/admin/itemAdd.aspx">Inventory Maintenance</A></TD>
			<TD class="label"><A href="/admin/frmMSL.aspx">ESN Search</A></TD>
			<TD class="label"><A href="/ItemSummary.aspx">Inventory Report</A></TD>
			<TD class="label"><A href="/frmPOASN.aspx">Fulfillment Workflow</A></TD>
			<TD class="label"><A href="/admin/queue.aspx">ESN/ASN Queue</A></TD>
			<TD class="label"><A href="/ESN/EsnManagement.aspx">ESN Management</A></TD>
			<TD class="label"><A href="/PurchaseOrderQuery.aspx">Purchase Order Search</A></TD>
			<TD class="label"><A href="/admin/UploadPOData.aspx">Forms Uploads</A></TD>
			<TD class="label"><A href="/frmErrorLog.aspx">Error Log</A></TD>
			<TD class="label"><A href="/Index.aspx">Public Site</A></TD>
			<TD>
				<asp:Button ID="btnClear" runat="server" runat="server"   visible="false"
                    onclick="btnClear_Click" Text=" " /></TD>
			<TD class="label">
				<asp:Button id="btnlog" runat="server" Width="100%" Text="Logout" CausesValidation="False"></asp:Button></TD>
		</TR>
	</TABLE>
</asp:panel>
<asp:panel id="pnlAvCustomer" Runat="server" visible="false">
	<TABLE cellSpacing="0" cellPadding="4" width="100%" border="0">
		<TR bgColor="gainsboro">
			<TD class="label"><A href="/admin/forecastadmin.aspx">Forecast Admin</A></TD>
			<TD class="label"><A href="/admin/frmMSL.aspx">MSL Search</A></TD>
			<TD class="label">
				<asp:Button id="btnlog2" runat="server" Width="100%" Text="Logout" CausesValidation="False"></asp:Button></TD>
		</TR>
		<TR>
			<TD>&nbsp;</TD>
		</TR>
	</TABLE>
</asp:panel>