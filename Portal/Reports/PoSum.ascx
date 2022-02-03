<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PoSum.ascx.cs" Inherits="avii.Controls.PoSum" %>
   <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="100%" >
           <tr bordercolor="#839abf">
                <td>

<table width="100%" cellpadding="0">
    <tr>
        <td class="button">Fulfillment Summary</td>
    </tr>
    <tr>
        <td>
            <asp:Panel ID="pnlSummary" runat="server">
             <table width="100%" border="0">
                    <tr align="left">
                        <td align="left" class="copy10grey"  colspan="2" >
                        
                        <asp:Label ID="lblPoSummaryDate"  CssClass="copy10grey" runat="server"></asp:Label></td>
                    </tr>
                    
                    <tr align="left">
                        <td align="left" class="copy10grey" width="22%" >Total Purchase Orders:</td>
                        <td width="70%" align="left"><asp:Label ID="lblTotalPOs"  CssClass="copy10grey" runat="server"></asp:Label></td>
                    </tr>
                    <%--<tr>
                        <td align="right" class="copy10grey" >Pending Purchase Orders:</td>
                        <td><asp:Label ID="lblPendingPOs" CssClass="copy10grey"  runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td align="right" class="copy10grey" >Processed Purchase Orders:</td>
                        <td><asp:Label ID="lblProcessedPOs" CssClass="copy10grey"  runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td align="right" class="copy10grey" style="height: 2px" >Shipped Purchase Orders:</td>
                        <td style="height: 2px"><asp:Label ID="lblShippedPOs" CssClass="copy10grey"  runat="server"></asp:Label></td>
                    </tr>			        			        
                    <tr>
                        <td align="right" class="copy10grey" style="height: 2px" >Closed Purchase Orders:</td>
                        <td style="height: 2px"><asp:Label ID="lblClosedPO" CssClass="copy10grey"  runat="server"></asp:Label></td>
                    </tr>	class="<%# Container.ItemIndex % 2 == 0 ? "alternaterow" : "" %>"--%>
                </table>

                <asp:Repeater ID="rptPOStatus" runat="server">
                <HeaderTemplate>
                <table width="20%" border="0">
                    
                    <tr align="left">
                        <td align="left" class="copy10grey" width="80%" ><%--Fulfillment Status--%>&nbsp;</td>
                        <td  align="right" ></td>

                    </tr>
                
                </HeaderTemplate>
                <ItemTemplate>
                    
                    <tr align="left" >
                        <td align="left" class="copy10grey" width="80%" >
                            <%# Eval("FulfillmentOrderStatus")%>
                        </td>
                        <td  class="copy10grey" align="right">
                            <%# Eval("StatusCount")%>
                        </td>

                    </tr>
                
                </ItemTemplate>
                <FooterTemplate>
                </table>
                </FooterTemplate>
                </asp:Repeater>

            </asp:Panel>
        </td>
    </tr>
    <tr><td class="button">Inventory Summary</td>
    </tr>
    <tr>
        <td class="copy10grey">
            <asp:Repeater ID="rptSummary" runat="server">
                <HeaderTemplate>
                <table width="30%" border="0">
                    <%--
                    <tr align="left">
                        <td align="left" class="copy10grey" width="80%" >&nbsp;</td>
                        <td  align="right" ></td>

                    </tr>--%>
                
                </HeaderTemplate>
                <ItemTemplate>
                    
                    <tr align="left" >
                        <td align="left" class="copy10grey" width="80%" >
                            <%# Eval("ItemCode")%>
                        </td>
                        <td  class="copy10grey" align="right">
                            <%# Eval("LineNo")%>
                        </td>

                    </tr>
                
                </ItemTemplate>
                <FooterTemplate>
                </table>
                </FooterTemplate>
                </asp:Repeater>

            <asp:Panel ID="pnlItemSummary" runat="server">
            
            </asp:Panel>
        </td>
    </tr>
</table>

</td>
</tr>
</table>
