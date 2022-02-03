<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="POStatus.ascx.cs" Inherits="avii.Controls.POStatus" %>
<asp:Repeater ID="rptPO" runat="server">
<HeaderTemplate>
<table border="0" width="100%" cellpadding="1" cellspacing="1">
    <tr>
        <td class="buttonlabel" width="15%">
            Order Type
        </td>
        <td class="buttonlabel" width="40%">
            Fulfillment Status
        </td>
        <td class="buttonlabel" width="20%">
           Order Count
        </td>
        <td class="buttonlabel" width="25%">
           Line Items Count
        </td>
    </tr>
</HeaderTemplate>
<ItemTemplate>
    
    <tr valign="bottom"  class="<%# Container.ItemIndex % 2 == 0 ? "alternaterow" : "" %>">
        <td valign="bottom" class="copy10grey"  >
        <span width="100%">
            <%# Eval("POType")%>    </span>
        </td>
        <td valign="bottom" class="copy10grey"  >
        <span width="100%">
            <%# Eval("StatusText")%>    </span>
        </td>
        <td align="right" class="copy10grey" valign="bottom">
            <a class="linkgrey" href='/dashboard.aspx?pos=<%# Eval("StatusID")%>&t=<% =TimeInterval%>&cid=<% =CompanyID%>&type=<%# Eval("POType")%>'> 
         <b>   <%# Eval("StatusCount")%>&nbsp; </b>   </a>
            
        </td>
        <td class="copy10grey" align="right">
            <b>   <%# Eval("SKUQty")%>&nbsp; </b>
            
        </td>
    </tr>
    
    

    
</ItemTemplate>
<FooterTemplate>
</table>
</FooterTemplate>
</asp:Repeater>
<asp:Label ID="lblPO" runat="server" CssClass="errormessage"></asp:Label>