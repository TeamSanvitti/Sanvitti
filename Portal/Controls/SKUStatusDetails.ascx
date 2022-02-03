<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SKUStatusDetails.ascx.cs" Inherits="avii.Controls.SKUStatusDetails" %>

<asp:Repeater ID="rptSKU" runat="server">
<HeaderTemplate>
<table border="0" width="100%" cellpadding="1" cellspacing="1">
<tr>
    <td valign="bottom" class="button"  align="left">
        
            Company Name
        </td>
        <td valign="bottom" class="button" align="center">
            SKU
        </td>
        <td valign="bottom" class="button" align="center">
            Pending
        </td>
        <td valign="bottom" class="button" align="center">
            Processed
        </td>
        <td valign="bottom" class="button" align="center">
            Shipped
        </td>
        <td valign="bottom" class="button" align="center">
            Closed
        </td>
        <td valign="bottom" class="button" align="center">
            Cancel
        </td>
</tr>
</HeaderTemplate>
<ItemTemplate>
    
    <tr valign="bottom"  class="<%# Container.ItemIndex % 2 == 0 ? "alternaterow" : "" %>">
        <td valign="bottom" class="copy10grey"  align="left">
        
            <%# Eval("CompanyName")%>    
        </td>
        <td valign="bottom" class="copy10grey" align="left">
            <%# Eval("SKU")%> 
        </td>
        <td valign="bottom" class="copy10grey" align="right">
            <%# Eval("Pending")%> 
        </td>
        <td valign="bottom" class="copy10grey" align="right">
            <%# Eval("Processed")%> 
        </td>
        <td valign="bottom" class="copy10grey" align="right">
            <%# Eval("Shipped")%> 
        </td>
        <td valign="bottom" class="copy10grey" align="right">
            <%# Eval("Closed")%> 
        </td>
        <td valign="bottom" class="copy10grey" align="right">
            <%# Eval("Cancel")%> 
        </td>
        <%--<td valign="bottom" class="copy10grey">
            <%# Eval("OrderCount")%> 
        </td>
        <td align="right" class="copy10grey" valign="bottom">
            
            <%# Eval("OrderCount")%>    
        </td>--%>
    </tr>
    
    

    
</ItemTemplate>
<FooterTemplate>
</table>
</FooterTemplate>
</asp:Repeater>
<asp:Label ID="lblSKU" runat="server" CssClass="errormessage"></asp:Label>
