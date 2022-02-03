<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AssignedSKUs.ascx.cs" Inherits="avii.Controls.AssignedSKUs" %>
<asp:GridView ID="gvSKU" OnPageIndexChanging="gridView_PageIndexChanging"    AutoGenerateColumns="false"  
Width="100%" ShowHeader="false"  ShowFooter="false" runat="server" GridLines="Both" 
PageSize="50" AllowPaging="true" AllowSorting="false" 
>
<RowStyle BackColor="Gainsboro" />
<AlternatingRowStyle BackColor="white" />
<HeaderStyle  CssClass="button" ForeColor="white"/>
  <PagerStyle ForeColor="#636363" CssClass="copy10grey" HorizontalAlign="Left" />
  <Columns>
        <asp:TemplateField ItemStyle-CssClass="copy10grey"  ItemStyle-Width="85%">
            <ItemTemplate>
                 <%# Eval("SKU")%> 
            </ItemTemplate>
        </asp:TemplateField>                 
                
        <asp:TemplateField  ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="15%">
            <ItemTemplate>
                <%--<a class="copy10grey" href='/LandingPage.aspx?sku=<%# Eval("sku")%>&t=<% =TimeInterval%>'> --%>
            <%# Eval("OrderCount")%>   <%--</a> --%>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
<%--
<asp:Repeater ID="rptSKU" runat="server">
<HeaderTemplate>
<table border="0" width="100%" cellpadding="1" cellspacing="1">
</HeaderTemplate>
<ItemTemplate>
    
    <tr valign="bottom"  class="<%# Container.ItemIndex % 2 == 0 ? "alternaterow" : "" %>">
        <td valign="bottom" class="copy10grey"  >
        <span width="100%">

            <%# Eval("SKU")%>    </span>
        </td>
        <td align="right" class="copy10grey" valign="bottom">
            <a class="copy10grey" href='/LandingPage.aspx?sku=<%# Eval("sku")%>&t=<% =TimeInterval%>'> 
            <%# Eval("OrderCount")%>   </a> 
        </td>
    </tr>
    
    

    
</ItemTemplate>
<FooterTemplate>
</table>
</FooterTemplate>
</asp:Repeater>--%>
<asp:Label ID="lblSKU" runat="server" CssClass="errormessage"></asp:Label>