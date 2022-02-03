<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ForecastCtrl.ascx.cs" Inherits="avii.Controls.ForecastCtrl" %>
<style type="text/css">
    .style1
    {
        width: 239px;
    }
    .style2
    {
        width: 129px;
    }
</style>
<table style="width: 100%;" cellpadding="0" cellspacing="0" bgcolor="Gainsboro">
    <tr bgcolor="Gainsboro" >
        <td  class="copyblue11b">
            Forecast Date:</td>
        <td>
            &nbsp;
        </td>
        <td class="style1">
            <asp:Label BorderStyle="None" ID="lblDate" CssClass="txfield1" runat="server"></asp:Label>
        </td>
        <td  class="copyblue11b">
            Forecast Status:</td>
        <td>
            &nbsp;
        </td>
        <td>
            <asp:Label ID="lblStatus" BorderStyle="None" CssClass="txfield1" runat="server"></asp:Label>
        </td>
    </tr>
    <tr bgcolor="Gainsboro" >
        <td class="copyblue11b">
            Qty:</td>
        <td>
            &nbsp;
        </td>
        <td class="copyblue11b">
            <asp:TextBox ID="txtQty" CssClass="txfield1" runat="server" MaxLength="4"></asp:TextBox>
        </td>
    </tr>
</table>
