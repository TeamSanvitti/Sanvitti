<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ctlRmaStatusesSummary.ascx.cs" Inherits="avii.Controls.ctlRmaStatusesSummary" %>
<table bordercolor="#839abf" border="1" width="98%" align="center" cellpadding="0" cellspacing="0">
      <tr>
        <td>
<table width="100%" cellpadding="3" cellspacing="3">
<tr>
<td class="copy10grey" width="27%" align="right" >
    Product Name:
</td>
<td class="copy10grey" width="1%" align="right" >&nbsp;</td>
<td class="copy10grey"  >
<asp:Label ID="lblProduct" runat="server" CssClass="copy10grey"></asp:Label> 
</td>
</tr>
<tr>
<td class="copy10grey" align="right" >
    Duration:
</td>
<td class="copy10grey" width="1%" align="right" >&nbsp;</td>
<td class="copy10grey" >
<asp:Label ID="lblDate" runat="server" CssClass="copy10grey"></asp:Label> 
</td>
</tr>
<tr>
<td class="copy10grey" align="right" >
    <asp:Label ID="lblCust" runat="server" CssClass="copy10grey" Text="Customer Name:"></asp:Label> 
</td>
<td class="copy10grey" width="1%" align="right" >&nbsp;</td>
<td class="copy10grey" >
<asp:Label ID="lblCustName" runat="server" CssClass="copy10grey" Text="Customer Name:"></asp:Label> 
</td>
</tr></table>
<table width="100%" cellpadding="0" cellspacing="0">
<tr>
    <td colspan="3">
        
        <asp:Repeater ID="rptRmaStatus" runat="server">
        <HeaderTemplate>
        <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td align="center" class="button">S.No.</td>
            <td align="center" class="button">RMA Status</td>
            <td align="center" class="button">Count</td>
        </tr>
        
        </HeaderTemplate>
        <ItemTemplate>
            <tr class="<%# Container.ItemIndex % 2 == 0 ? "alternaterow" : "" %>">
                <td align="right" class="copy10grey" width="2%" ><%# Container.ItemIndex + 1 %> &nbsp; </td>
                <td align="left" class="copy10grey"> &nbsp; <%# Eval("StatusName")%> </td>
                <td align="right" class="copy10grey" width="5%" ><%# Eval("Count")%> </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
        </table>
        </FooterTemplate>
        </asp:Repeater>
        <asp:Label ID="lblRma" runat="server" CssClass="errormessage"></asp:Label>

    </td>
</tr>

</table>

</td>
</tr>
</table>
