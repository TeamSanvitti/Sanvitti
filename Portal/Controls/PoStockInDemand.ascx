<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PoStockInDemand.ascx.cs" Inherits="avii.Controls.PoStockInDemand" %>
<table width="100%" cellpadding="0" cellspacing="0">
<tr>
    <td align="right">
        <asp:Label ID="lblSKUCount" CssClass="copy10grey" runat="server" ></asp:Label>
    </td>
</tr>
<tr>
    <td >

    <asp:GridView ID="gvSKUs" OnPageIndexChanging="gvSKUs_PageIndexChanging"    AutoGenerateColumns="false"  
    Width="100%" ShowHeader="true"  ShowFooter="false" runat="server" GridLines="Both" 
    PageSize="50" AllowPaging="true" AllowSorting="true" OnSorting="gvSKUs_Sorting"
    >
    <RowStyle BackColor="Gainsboro" />
    <AlternatingRowStyle BackColor="white" />
    <HeaderStyle  CssClass="buttonlabel" ForeColor="white"/>
      <PagerStyle ForeColor="#636363" CssClass="copy10grey" HorizontalAlign="Left" />
      <Columns>
        <asp:TemplateField HeaderText="S.No." ItemStyle-CssClass="copy10grey"  ItemStyle-Width="1%">
            <ItemTemplate>
                 <%# Container.DataItemIndex + 1 %> 
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Category" SortExpression="CategoryName" HeaderStyle-CssClass="buttonundlinelabel" ItemStyle-CssClass="copy10grey"  
          ItemStyle-Width="11%"  ItemStyle-HorizontalAlign="Left">
            <ItemTemplate>
                 <%# Eval("CategoryName")%>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="SKU" SortExpression="SKU" HeaderStyle-CssClass="buttonundlinelabel" ItemStyle-CssClass="copy10grey"  ItemStyle-Width="22%"  ItemStyle-HorizontalAlign="Left">
            <ItemTemplate>
                 <%# Eval("SKU")%> 
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Product Name" SortExpression="ProductName" ItemStyle-CssClass="copy10grey" HeaderStyle-CssClass="buttonundlinelabel" ItemStyle-Width="45%"  ItemStyle-HorizontalAlign="Left">
            <ItemTemplate>
                 <%# Eval("ProductName")%>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Ordered Quantity" SortExpression="RequiredQunatity"  ItemStyle-Width="5%" ItemStyle-CssClass="copy10grey" ItemStyle-HorizontalAlign="Right"  HeaderStyle-CssClass="buttonundlinelabel">
            <ItemTemplate>
                <%# Eval("RequiredQunatity") %>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Current Stock" ItemStyle-Width="5%" SortExpression="CurrentStock"  ItemStyle-HorizontalAlign="Right" HeaderStyle-CssClass="buttonundlinelabel">
            <ItemTemplate>
                <asp:LinkButton ID="lnkESN" CssClass="copyblue11b" runat="server" CausesValidation="true" CommandArgument='<%# Eval("ItemCompanyGUID") + "," + Eval("CompanyID") %>' CommandName="esn" OnCommand="lnkESN_Command">
                                 
                <%# Eval("CurrentStock") %>&nbsp;
                    </asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Order Count" ItemStyle-Width="5%" SortExpression="OrderCount"  HeaderStyle-CssClass="buttonundlinelabel" ItemStyle-HorizontalAlign="Right">
            <ItemTemplate>
                <asp:LinkButton ID="lnkPO" CssClass="copyblue11b" runat="server" CausesValidation="true" CommandArgument='<%# Eval("SKU")%>' CommandName="test" OnCommand="lnkPO_Command">
               &nbsp;<%# Eval("OrderCount") %>&nbsp;

                </asp:LinkButton>

            </ItemTemplate>
        </asp:TemplateField>                        
        </Columns>
    </asp:GridView>

        <asp:Label ID="lblSkuMsg" runat="server" CssClass="errormessage"></asp:Label>
    </td>
</tr>

</table>


