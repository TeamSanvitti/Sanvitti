<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="POD.ascx.cs" Inherits="avii.Controls.POD" %>
<asp:Panel ID="pnlMsg" runat="server">
<table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="100%" align="center" >
           <tr bordercolor="#839abf">
                <td>
                    <table width="100%" cellSpacing="1" cellPadding="1">
                    <tr>
                    <td>
                        <asp:Label ID="lblMsg" runat="server" CssClass="errormessage" ></asp:Label>
                    </td>
                    </tr>
                    </table>
                    </td>
                    </tr>
                    </table>
</asp:Panel>
<asp:Panel ID="pnlControl" runat="server">
<table width="98%" align="center" cellpadding="0" cellspacing="0">
<tr>
<td align="right" colspan="2">
    <asp:Label ID="lblPODCount" runat="server" CssClass="copy10greyb" ></asp:Label>
</td>
</tr>

<tr>
    <td colspan="2">
    
    <asp:GridView ID="gvPODetail"  BackColor="White" Width="100%" Visible="true"  
        AutoGenerateColumns="false" Font-Names="Verdana" runat="server" 
        GridLines="Both" 
        BorderStyle="Double" BorderColor="#0083C1" >
        <RowStyle BackColor="Gainsboro" />
        <AlternatingRowStyle BackColor="white" />
        <HeaderStyle  CssClass="button" ForeColor="white"/>
        <FooterStyle CssClass="white"  />
        <Columns>
         	<asp:TemplateField HeaderText="Line#"  ItemStyle-CssClass="copy10grey"  HeaderStyle-Width="3%" ItemStyle-Wrap="false"  ItemStyle-width="3%">
                <ItemTemplate>
                   <%# Container.DataItemIndex +  1 %>
                </ItemTemplate> 
                
            </asp:TemplateField>
            <asp:TemplateField HeaderText="SKU#" SortExpression="Item_Code" ItemStyle-CssClass="copy10grey"  HeaderStyle-Width="7%" ItemStyle-Wrap="false"  ItemStyle-width="10%">
                <ItemTemplate>
                    <%# Convert.ToString(Eval("ItemCode")).ToUpper()%>
                </ItemTemplate> 
                
            </asp:TemplateField>
                                            
            <asp:TemplateField HeaderText="Qty" SortExpression="Qty"  ItemStyle-CssClass="copy10grey" ItemStyle-Width="3%">
                <ItemTemplate><%#Eval("Quantity")%></ItemTemplate>
            </asp:TemplateField>
                                            
           <%-- <asp:TemplateField HeaderText="ESN" SortExpression="ESN" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%" >
                <ItemTemplate>
                    <%# Eval("ESN") %>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtEsn" CssClass="copy10grey" MaxLength="35"  Width="99%" Text='<%# Eval("ESN") %>' runat="server"></asp:TextBox>
                </EditItemTemplate>
                                                
            </asp:TemplateField>
                                            
            <asp:TemplateField HeaderText="MDN" SortExpression="MdnNumber"  ItemStyle-CssClass="copy10grey" ItemStyle-Width="7%" >
                <ItemTemplate><%#Eval("MdnNumber")%></ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtMdn" CssClass="copy10grey" MaxLength="30"  Text='<%# Eval("MdnNumber") %>' runat="server"></asp:TextBox>
                </EditItemTemplate>                                                                                
            </asp:TemplateField>
--%>
                                        
            
            
             
    		                                
        </Columns>
    </asp:GridView>
    
    </td>
</tr>
</table>


</asp:Panel>