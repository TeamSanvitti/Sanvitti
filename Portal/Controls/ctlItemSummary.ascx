<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ctlItemSummary.ascx.cs" Inherits="avii.Controls.ctlItemSummary" %>
    <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="100%" >
           <tr bordercolor="#839abf">
           <td>
<table width="100%">
    <tr>
        <td align="left"><asp:CheckBox ID="chkHistory" AutoPostBack="true"
                CssClass="copy10grey" runat="server" 
                OnCheckedChanged="chkHistory_CheckedChanged" Text="Add ARCHIVED Data" 
                Visible="False" /></td>
    </tr>
    <tr>
        <td>
            <asp:GridView ID="grdSummary" runat="server" AllowPaging="false" AllowSorting="false" AutoGenerateColumns="false" OnRowDataBound = "gv_RowDataBound" Width="100%">
                <RowStyle BackColor="Gainsboro" />
                <AlternatingRowStyle BackColor="white" />
                <HeaderStyle  CssClass="button" ForeColor="white"/>
                <FooterStyle CssClass="white"  />
                <Columns>
                    <asp:TemplateField HeaderText="Phone Maker" SortExpression="Phone.PhoneMaker"  ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                        <ItemTemplate><%#Eval("Phone.PhoneMaker")%></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Item Code" SortExpression="Item_Code" ItemStyle-CssClass="copy10grey"  ItemStyle-Width="10%">
                        <ItemTemplate>
                            <asp:HyperLink ID="hlkItemCode" runat="server" Text='<%# Eval("Phone.ItemCode")%>'></asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Description" SortExpression="Phone.ItemDescription"  ItemStyle-CssClass="copy10grey" ItemStyle-Width="20%">
                        <ItemTemplate><%#Eval("Phone.ItemDescription")%></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="UPC" SortExpression="Phone.UPC"  ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                        <ItemTemplate><%#Eval("Phone.UPC")%></ItemTemplate>
                    </asp:TemplateField>                  
                    <asp:TemplateField HeaderText="SKU" SortExpression="Phone.SKU"  ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                        <ItemTemplate><%#Eval("Phone.SKU")%></ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField HeaderText="Pending" SortExpression="PendingSale"  ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                        <ItemTemplate><%#Eval("pendingSale")%></ItemTemplate>
                    </asp:TemplateField>   
                    <asp:TemplateField HeaderText="Processed" SortExpression="ProcessedSale"  ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                        <ItemTemplate><%#Eval("ProcessedSale")%></ItemTemplate>
                    </asp:TemplateField>  
                    <asp:TemplateField HeaderText="Shipped" SortExpression="ShippedSale"  ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                        <ItemTemplate>
							<asp:Label ID="lblship" CssClass="copy10grey" Text='<%# Eval("ShippedSale") %>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>                                            
                </Columns>
            </asp:GridView>        
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblMsg" runat="server" CssClass="errormessage"></asp:Label></td>
    </tr>
</table>
</td>
    </tr>
</table>