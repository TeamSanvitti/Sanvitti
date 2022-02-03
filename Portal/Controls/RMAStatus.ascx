<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RMAStatus.ascx.cs" Inherits="avii.Controls.RMAStatus" %>
<asp:Repeater ID="rptRMA" runat="server">
<HeaderTemplate>
<table border="0" width="100%" cellpadding="1" cellspacing="1">
</HeaderTemplate>
<ItemTemplate>
    
    <tr valign="bottom" class="<%# Container.ItemIndex % 2 == 0 ? "alternaterow" : "" %>">
    <td valign="bottom" class="copy10grey"  >
        <span width="100%" >
            <%# Eval("StatusText")%>    </span>
        </td>
        <td align="right" class="copy10grey" valign="bottom">
            <a class="copy10grey" href='/LandingPage.aspx?rma=<%# Eval("StatusID")%>&t=<% =TimeInterval%>&cid=<% =CompanyID%>'> 
            <%# Eval("StatusCount")%>    </a>
        </td>
    </tr>
    
    

    
</ItemTemplate>
<FooterTemplate>
</table>
</FooterTemplate>
</asp:Repeater>
<asp:Label ID="lblRMA" runat="server" CssClass="errormessage"></asp:Label>