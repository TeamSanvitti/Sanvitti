<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ItemList.ascx.cs" Inherits="avii.Controls.ItemList" %>
<table style="width:100%;">
    <tr>
        <td>
            &nbsp;</td>
        <td>
            <asp:GridView ID="grdItem" runat="server" AllowPaging="false" AllowSorting="false" AutoGenerateColumns="false">
                <RowStyle BackColor="Gainsboro" />
                <AlternatingRowStyle BackColor="white" />
                <HeaderStyle  CssClass="button" ForeColor="white"/>
                <FooterStyle CssClass="white"  />
                <Columns>
                    <asp:TemplateField HeaderText="Item Code" SortExpression="Item_Code" ItemStyle-CssClass="copy10grey"  ItemStyle-Width="20%">
                        <ItemTemplate>
                            <asp:HyperLink ID="hlkItemCode" NavigateUrl='<%# "/admin/itemAdd.aspx?id=" + Eval("ItemID")%>' runat="server" Text='<%# Eval("ItemCode")%>'></asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Description" SortExpression="Phone.ItemDescription"  ItemStyle-CssClass="copy10grey" ItemStyle-Width="25%">
                        <ItemTemplate><%#Eval("ItemDescription")%></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="CompanyName" SortExpression="CompanyName"  ItemStyle-CssClass="copy10grey" ItemStyle-Width="25%">
                        <ItemTemplate><%#Eval("CompanyName")%></ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
</table>
