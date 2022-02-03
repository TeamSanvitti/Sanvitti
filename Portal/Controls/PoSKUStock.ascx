<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PoSKUStock.ascx.cs" Inherits="avii.Controls.PoSKUStock" %>
<table width="100%" cellpadding="0" cellspacing="0">
<tr>
    <td align="right">
        <asp:Label ID="lblSKUCount" CssClass="copy10grey" runat="server" ></asp:Label>
    </td>
</tr>
<tr>
    <td >

<asp:GridView ID="gvSKUs" OnPageIndexChanging="gvSKUs_PageIndexChanging" AutoGenerateColumns="false"  
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
        
      <asp:TemplateField HeaderText="Category Name" SortExpression="CategoryName" HeaderStyle-CssClass="buttonundlinelabel" ItemStyle-CssClass="copy10grey"  
          ItemStyle-Width="15%"  ItemStyle-HorizontalAlign="Left">
            <ItemTemplate>
                 <%# Eval("CategoryName")%>
            </ItemTemplate>
        </asp:TemplateField>                 
          
      <asp:TemplateField HeaderText="SKU" SortExpression="SKU" HeaderStyle-CssClass="buttonundlinelabel" ItemStyle-CssClass="copy10grey"  ItemStyle-Width="30%"  ItemStyle-HorizontalAlign="Left">
            <ItemTemplate>


                 <%# Eval("SKU")%> 

            </ItemTemplate>
        </asp:TemplateField>                 
                
          
      <asp:TemplateField HeaderText="Product Name" SortExpression="ProductName" HeaderStyle-CssClass="buttonundlinelabel" ItemStyle-CssClass="copy10grey"  
          ItemStyle-Width="30%"  ItemStyle-HorizontalAlign="Left">
            <ItemTemplate>


                 <%# Eval("ProductName")%> 

            </ItemTemplate>
        </asp:TemplateField>                 
            
         <asp:TemplateField HeaderText="Stock In Hand" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                    
            <ItemTemplate>
                                                
                    <%# Eval("StockInHand")%>
            </ItemTemplate>
        </asp:TemplateField> 
      <asp:TemplateField HeaderText="Required Qty" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                    
            <ItemTemplate>
                                                
                    <%# Eval("Qty")%>
            </ItemTemplate>
        </asp:TemplateField> 
    </Columns>
</asp:GridView>

        <asp:Label ID="lblSkuMsg" runat="server" CssClass="errormessage"></asp:Label>
    </td>
</tr>

</table>

