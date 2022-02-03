<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Esn_Asn_Queue.ascx.cs" Inherits="avii.Controls.Esn_Asn_Queue" %>

<table bordercolor="#839abf" border="4" cellSpacing="0" cellPadding="0" width="100%" >
   <tr bordercolor="#839abf">
        <td valign="top">
            <asp:Repeater ID="rptASN" runat="server" Visible="false" >  
                <HeaderTemplate>
                <table width="100%" border="0" cellpadding="0" cellspacing"0"> 
                <tr Class="button" Color="white">
                    <td colspan="2">Purchase Orders for ASN Queue</td>
                </tr>
                <tr Class="button" Color="white">
                    <td>PO#</td>
                    <td>TrackingNumber</td>
                </tr>
                </HeaderTemplate>
                <AlternatingItemTemplate>
                <tr>
                    <td Class="copy10grey" bgcolor = "gainsboro" ><%# Eval("PurchaseOrderNumber") %></td>
                    <td Class="copy10grey" bgcolor = "gainsboro"><%# Eval("TrackingNumber") %></td>
                </tr>
                </AlternatingItemTemplate>
                
                <ItemTemplate>
                <tr>
                    <td Class="copy10grey" ><%# Eval("PurchaseOrderNumber") %></td>
                    <td Class="copy10grey" ><%# Eval("TrackingNumber") %></td>
                </tr>
                </ItemTemplate>
                <FooterTemplate>
                
                    </table>
                </FooterTemplate>
            </asp:Repeater>
     </td>
     <td  valign="top">       
            <asp:Repeater ID="rptESN" runat="server" OnItemDataBound="rptESN_DataBound" Visible="false">  
                <HeaderTemplate>
                
                <table width="100%" border="0" cellpadding="0" cellspacing"0"> 
                <tr Class="button" Color="white"  colspan="5">
                    <td colspan="5">Purchase Orders for ESN Queue</td>
                </tr>
                <tr Class="button" Color="white">
                    <td>PO#</td>
                    <td>Line#</td>
                    <td>ESN</td>
                    <td>MSL#</td>
                    <td>FM-UPC</td>
                </tr>
                </HeaderTemplate>
                <AlternatingItemTemplate>
                 <tr>
                    <td Class="copy10grey">
                        <asp:Panel ID="pnlEsn" BackColor="gainsboro" runat="server"></asp:Panel>
                    </td>
                </tr>               
                </AlternatingItemTemplate>
                <ItemTemplate>
                <tr>
                    <td Class="copy10grey" >
                        <asp:Panel ID="pnlEsn" runat="server"></asp:Panel>
                    </td>
                </tr>
                </ItemTemplate>
                <FooterTemplate>                
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </td>
    </tr>
</table>