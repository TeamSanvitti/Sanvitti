<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RmaESNs.ascx.cs" Inherits="avii.Controls.RmaESNs" %>
<table width="100%" cellpadding="0" cellspacing="0">
<tr>
    <td align="right">
        <asp:Label ID="lblCount" CssClass="copy10grey" runat="server" ></asp:Label>
    </td>
</tr>
<tr>
    <td >
    
<asp:GridView ID="gvRmaEsn" OnPageIndexChanging="gridView_PageIndexChanging"    AutoGenerateColumns="false"  
Width="100%" ShowHeader="true"  ShowFooter="false" runat="server" GridLines="Both" 
PageSize="50" AllowPaging="true" AllowSorting="false" 
>
<RowStyle BackColor="Gainsboro" />
<AlternatingRowStyle BackColor="white" />
<HeaderStyle  CssClass="button" ForeColor="white"/>
  <PagerStyle ForeColor="#636363" CssClass="copy10grey" HorizontalAlign="Left" />
  <Columns>
        <asp:TemplateField HeaderText="S.No." ItemStyle-CssClass="copy10grey"  ItemStyle-Width="1%">
            <ItemTemplate>
            
                  <%# Container.DataItemIndex + 1  %>
                  
            </ItemTemplate>
        </asp:TemplateField>                 
        
        <asp:TemplateField HeaderText="RMA#" ItemStyle-CssClass="copy10grey"  ItemStyle-Width="40%"  ItemStyle-HorizontalAlign="Left">
            <ItemTemplate>
                 <%# Eval("RmaNumber")%>
                 
            </ItemTemplate>
        </asp:TemplateField>                 
        
        <asp:TemplateField HeaderText="ESN" ItemStyle-CssClass="copy10grey"  ItemStyle-Width="40%"  ItemStyle-HorizontalAlign="Left">
            <ItemTemplate>
                 <%# Eval("ESN")%>
                 
            </ItemTemplate>
        </asp:TemplateField>                 
                
        
    </Columns>
</asp:GridView>
<asp:Label ID="lblRMA" runat="server" CssClass="errormessage"></asp:Label>

    </td>
</tr>
</table>
