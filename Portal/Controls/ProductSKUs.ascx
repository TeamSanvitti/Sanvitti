<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProductSKUs.ascx.cs" Inherits="avii.Controls.ProductSKUs" %>
<asp:Label ID="lblSKU" runat="server" Width="100%" CssClass="errormessage"></asp:Label>
 <asp:GridView ID="gvSKU" Width="100%" AllowPaging="false" PageSize="30" 
    
runat="server" AutoGenerateColumns="false">
<RowStyle BackColor="Gainsboro" />
    <AlternatingRowStyle BackColor="white" />
    <HeaderStyle  CssClass="button" ForeColor="white"/>
    <FooterStyle CssClass="white"  />
    <PagerStyle ForeColor="#636363" CssClass="copy10grey" HorizontalAlign="Left" />
    <Columns>
        <asp:TemplateField HeaderText="Company Name" HeaderStyle-CssClass="buybt" HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="Left">
            <ItemTemplate>
            <%# Eval("CompanyName")%>
                <%--<asp:Label ID="lblcompanyName" CssClass="copy10grey" runat="server"  Text='<%# Eval("CompanyName") %>'></asp:Label>
                OnPageIndexChanging="grvItem_PageIndexChanging" 
                --%>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="SKU#" HeaderStyle-CssClass="buybt" HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="Left">
            <ItemTemplate>
            
            <%# Eval("sku") %>
                <%--<asp:Label ID="lblSKU" CssClass="copy10grey" runat="server"  Text='<%# Eval("sku") %>'></asp:Label>--%>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="MAS SKU#" HeaderStyle-CssClass="buybt" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
            <ItemTemplate>
            <%# Eval("MASSKU")%>
                <%--<asp:Label ID="lblMASSKU" CssClass="copy10grey" runat="server"  Text='<%# Eval("MASSKU") %>'></asp:Label>--%>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Warehouse Code" HeaderStyle-CssClass="buybt" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
            <ItemTemplate>
            <%# Eval("WarehouseCode")%>
                <%--<asp:Label ID="lblWhCode" CssClass="copy10grey" runat="server"  Text='<%# Eval("WarehouseCode") %>'></asp:Label>--%>
            </ItemTemplate>
        </asp:TemplateField>
                                            
    </Columns>
</asp:GridView>
