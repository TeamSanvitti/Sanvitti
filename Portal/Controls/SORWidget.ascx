<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SORWidget.ascx.cs" Inherits="avii.Controls.SORWidget" %>
<table width="100%" cellpadding="0" cellspacing="0">
<%--<tr>
    <td align="right">
        <asp:Label ID="lblSKUCount" CssClass="copy10grey" runat="server" ></asp:Label>
    </td>
</tr>--%>
<tr>
    <td>

<asp:GridView ID="gvSor" OnPageIndexChanging="gridView_PageIndexChanging"    AutoGenerateColumns="false"  
Width="100%" ShowHeader="true"  ShowFooter="false" runat="server" GridLines="Both" 
PageSize="30" AllowPaging="true" AllowSorting="true" OnSorting="gvSor_Sorting"
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
        
      <asp:TemplateField HeaderText="DATE" SortExpression="SORDate" HeaderStyle-CssClass="buttonundlinelabel" ItemStyle-CssClass="copy10grey"  ItemStyle-Width="20%"  ItemStyle-HorizontalAlign="Left">
            <ItemTemplate>


                 <%# Eval("SORDate")%> 

            </ItemTemplate>
        </asp:TemplateField>                 
          
      <asp:TemplateField HeaderText="SKU" SortExpression="SKU" HeaderStyle-CssClass="buttonundlinelabel" ItemStyle-CssClass="copy10grey"  ItemStyle-Width="50%"  ItemStyle-HorizontalAlign="Left">
            <ItemTemplate>


                 <%# Eval("SKU")%> 

            </ItemTemplate>
        </asp:TemplateField>                 
                
        <asp:TemplateField HeaderText="Quantity" SortExpression="Quantity" HeaderStyle-CssClass="buttonundlinelabel" ItemStyle-CssClass="copy10grey"  
          ItemStyle-Width="15%"  ItemStyle-HorizontalAlign="Left">
            <ItemTemplate>


                 <%# Eval("Quantity")%> 

            </ItemTemplate>
        </asp:TemplateField>                 
      
    </Columns>
</asp:GridView>
<asp:Label ID="lblSoR" runat="server" CssClass="errormessage"></asp:Label>


    </td>
</tr>

</table>
