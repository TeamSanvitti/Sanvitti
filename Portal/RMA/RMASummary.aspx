<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RMASummary.aspx.cs" Inherits="avii.RMA.RMASummary" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html>
<head>
    <title>.:: Return Merchandise Authorization (RMA) Summary ::.</title>
    <link href="../lanstyle.css" type="text/css" rel="stylesheet"/>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table width="100%" border="0">
            <tr>
                <td>
                    <span class="copy10grey">Total RMA Count:</span>&nbsp;
                    <asp:Label ID="lblRowCount" CssClass="copyblue11b" runat="server"></asp:Label>                
                </td>
                <td align="right">
                    
                </td>
            </tr>
        </table>
        <table style="width:100%;">
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
            <asp:Repeater ID="rptRMA" runat="server">
            <HeaderTemplate>
                <table width="100%" border="0" cellpadding="5" cellspacing="0">
                    <tr class="buttongrid"> 
                        <td width="20%">CompanyName</td>
                        <td width="10%">ItemCode</td>
                        <td width="10%">UPC</td>
                        <td width="10%">RmaCount</td>                        
                    </tr>
            </HeaderTemplate>
            <AlternatingItemTemplate>
                <tr bgcolor="Gainsboro">
                    <td class="copy10grey"><%#Eval("CompanyName")%></td>
                    <td class="copy10grey"><%#Eval("ItemCode")%></td>
                    <td class="copy10grey"><%#Eval("UPC")%></td>
                    <td class="copy10grey"><%#Eval("RmaCount")%></td>
                </tr>            
            </AlternatingItemTemplate>
            <ItemTemplate>
                <tr>
                    <td class="copy10grey"><%#Eval("CompanyName")%></td>
                    <td class="copy10grey"><%#Eval("ItemCode")%></td>
                    <td class="copy10grey"><%#Eval("UPC")%></td>
                    <td class="copy10grey"><%#Eval("RmaCount")%></td>
                </tr>
            </ItemTemplate>
           
        </asp:Repeater>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
