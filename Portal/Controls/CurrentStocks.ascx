<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CurrentStocks.ascx.cs" Inherits="avii.Controls.CurrentStocks" %>
<table width="100%" cellpadding="0" cellspacing="0">
<tr>
    <td align="right">
        <asp:Label ID="lblSKUCount" CssClass="copy10grey" runat="server" ></asp:Label>
    </td>
</tr>
<tr>
    <td >

<asp:GridView ID="gvcSKU" OnPageIndexChanging="gridView_PageIndexChanging"    AutoGenerateColumns="false"  
Width="100%" ShowHeader="true"  ShowFooter="false" runat="server" GridLines="Both" 
PageSize="30" AllowPaging="true" AllowSorting="true" OnSorting="gvcSKU_Sorting" OnRowDataBound="gvcSKU_RowDataBound"
>
<%--<RowStyle BackColor="Gainsboro" />
<AlternatingRowStyle BackColor="white"  />--%>

<HeaderStyle  CssClass="buttonlabel" ForeColor="white"/>
  <PagerStyle ForeColor="#636363" CssClass="copy10grey" HorizontalAlign="Left" />
  <Columns>
  <asp:TemplateField HeaderText="S.No." ItemStyle-CssClass="copy10grey"  ItemStyle-Width="1%">
            <ItemTemplate>

                 <%# Container.DataItemIndex + 1 %> 
            </ItemTemplate>
        </asp:TemplateField>                 
        
      <asp:TemplateField HeaderText="Category Name" SortExpression="CategoryName" HeaderStyle-CssClass="buttonundlinelabel" ItemStyle-CssClass="copy10grey"  ItemStyle-Width="14%"  ItemStyle-HorizontalAlign="Left">
            <ItemTemplate>
                <asp:HiddenField ID="hdIsDisable" Value='<%# Eval("IsDisable")%>' runat="server" />

                 <%# Eval("CategoryName")%> 

            </ItemTemplate>
        </asp:TemplateField>                 
          
      <asp:TemplateField HeaderText="SKU" SortExpression="SKU" HeaderStyle-CssClass="buttonundlinelabel" ItemStyle-CssClass="copy10grey"  ItemStyle-Width="27%"  ItemStyle-HorizontalAlign="Left">
            <ItemTemplate>


                 <%# Eval("SKU")%> 

            </ItemTemplate>
        </asp:TemplateField>                 
                
            <asp:TemplateField HeaderText="Product Name" SortExpression="ProductName" HeaderStyle-CssClass="buttonundlinelabel" ItemStyle-CssClass="copy10grey"  
          ItemStyle-Width="38%"  ItemStyle-HorizontalAlign="Left">
            <ItemTemplate>


                 <%# Eval("ProductName")%> 

            </ItemTemplate>
        </asp:TemplateField>                 
      
         <asp:TemplateField HeaderText="Stock In Hand" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                    
            <ItemTemplate>
                                                
                    <%# Eval("StockInHand")%>
            </ItemTemplate>
        </asp:TemplateField> 
    </Columns>
</asp:GridView>
<asp:Label ID="lblStock" runat="server" CssClass="errormessage"></asp:Label>


    </td>
</tr>

</table>
