<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RMAList.aspx.cs" Inherits="avii.RMA.RMAList" %>

<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>.:: Return Merchandise Authorization (RMA) Report ::.</title>
    <link href="../lanstyle.css" type="text/css" rel="stylesheet"/>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="100%" border="0">
            <tr>
                <td>
                    <span class="copy10grey">Total Count:</span>&nbsp;
                    <asp:Label ID="lblRowCount" CssClass="copyblue11b" runat="server"></asp:Label>                
                </td>
                <td align="right">
                    <asp:Button ID="btnDownload" runat="server" Text="Download" CssClass="button" 
                        onclick="btnDownload_Click" />
                </td>
            </tr>
        </table>

        
        <asp:Repeater ID="rptRMA" runat="server">
            <HeaderTemplate>
                <table width="100%" border="0" cellpadding="5" cellspacing="0">
                    <tr class="buttongrid">
                        <td width="8%">RMA#</td>
                        <td width="9%">RMA Date</td>
                        <td width="6%">Customer</td>
                        <td width="7%">Location</td>
                        <%--<td width="5%">SO#</td>--%>
                        <td width="10%">SKU#</td>
                        <td width="10%">ESN</td>
                        <td width="8%">Defect Reason</td>
                        <%--<td width="5%">Call Time</td>--%>
                        <td width="6%">Status</td>
                        <%--<td width="5%">Date Received</td>
                        <td width="10%">Inbound Tracking#</td>
                        <td width="3%">QTY Received</td>
                        <td width="5%">Date Completed</td>
                        <td width="10%">Outbound Tracking#</td>
                        <td width="10%">ESN Returned</td>--%>
                        <td width="17%">RMA Comments</td>
                        <td width="17%">Lan Global Comments</td>
                    </tr>
            </HeaderTemplate>
            <AlternatingItemTemplate>
                <tr bgcolor="Gainsboro">
                    <td class="copy10grey"><%#Eval("rmanumber")%></td>
                    <td class="copy10grey"><%#Eval("rmaDate")%></td>
                    <td class="copy10grey"><%#Eval("CompanyName")%></td>
                    <td class="copy10grey"><%#Eval("ContactCity")%></td>
                    <%--<td class="copy10grey"><%#Eval("SalesOrderNumber")%></td>--%>
                    <td class="copy10grey"><%#Eval("ItemCode")%></td>
                    <td class="copy10grey"><%#Eval("ESN")%></td>
                    <td class="copy10grey"><%#Eval("ReasonTxt")%></td>
                    <%--<td class="copy10grey"><%#Eval("CallTime")%></td>--%>
                    <td class="copy10grey"><%#Eval("RMAStatus")%></td>
                    <%--<td class="copy10grey"><%#Eval("DateReceived")%></td>
                    <td class="copy10grey"><%#Eval("InboundTrackingNumber")%></td>
                    <td class="copy10grey"><%#Eval("QTYReceived")%></td>
                    <td class="copy10grey"><%#Eval("DateCompleted")%></td>
                    <td class="copy10grey"><%#Eval("ShipmentToCustomerTrackingNumber")%></td>
                    <td class="copy10grey"><%#Eval("ESNShippedToCustomer")%></td>--%>
                    <td class="copy10grey"><%#Eval("Comment")%></td>
                    <td class="copy10grey"><%#Eval("AVComments")%></td>
                </tr>            
            </AlternatingItemTemplate>
            <ItemTemplate>
                <tr>
                    <td class="copy10grey"><%#Eval("rmanumber")%></td>
                    <td class="copy10grey"><%#Eval("rmaDate")%></td>
                    <td class="copy10grey"><%#Eval("CompanyName")%></td>
                    <td class="copy10grey"><%#Eval("ContactCity")%></td>
                    <%--<td class="copy10grey"><%#Eval("SalesOrderNumber")%></td>--%>
                    <td class="copy10grey"><%#Eval("ItemCode")%></td>
                    <td class="copy10grey"><%#Eval("ESN")%></td>
                    <td class="copy10grey"><%#Eval("ReasonTxt")%></td>
                    <%--<td class="copy10grey"><%#Eval("CallTime")%></td>--%>
                    <td class="copy10grey"><%#Eval("RMAStatus")%></td>
                    <%--<td class="copy10grey"><%#Eval("DateReceived")%></td>
                    <td class="copy10grey"><%#Eval("InboundTrackingNumber")%></td>
                    <td class="copy10grey"><%#Eval("QTYReceived")%></td>
                    <td class="copy10grey"><%#Eval("DateCompleted")%></td>
                    <td class="copy10grey"><%#Eval("ShipmentToCustomerTrackingNumber")%></td>
                    <td class="copy10grey"><%#Eval("ESNShippedToCustomer")%></td>--%>
                    <td class="copy10grey"><%#Eval("Comment")%></td>
                    <td class="copy10grey"><%#Eval("AVComments")%></td>
                </tr>
            </ItemTemplate>
           
        </asp:Repeater>
         </table>
    </div>
    </form>
</body>
</html>
