<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewESN.ascx.cs" Inherits="avii.Controls.ViewESN" %>
<table width="100%" border="0">
<tr>
    <td class="copy10grey" align="left" width="35%">
    <b> Upload Date Range: </b>
    </td>
    <td align="left">
        <asp:Label ID="lblDate" runat="server" CssClass="copy10grey"></asp:Label>               
    </td>
</tr>
</table>


<asp:GridView ID="gvESNs" runat="server" Width="100%" GridLines="Both" AutoGenerateColumns="false"
OnPageIndexChanging="gridView_PageIndexChanging" PageSize="35" AllowPaging="true" >
    <RowStyle BackColor="Gainsboro" />
    <AlternatingRowStyle BackColor="white" />
    <HeaderStyle  CssClass="button" ForeColor="white"/>
    <FooterStyle CssClass="white"  />
    <PagerStyle ForeColor="#636363" CssClass="copy10grey" HorizontalAlign="Left" />
    <Columns>
        <asp:TemplateField HeaderText="S.No." ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="1%">
            <ItemTemplate>
            <%# Container.DataItemIndex +  1 %>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="ESN/SIM" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" >
            <ItemTemplate><%#Eval("ESN")%></ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Upload Date" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" >
            <ItemTemplate><%#Eval("UploadDate")%></ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
<asp:Label ID="lblESN" runat="server" CssClass="errormessage"></asp:Label>