<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WarehouseCodeItems.aspx.cs" Inherits="avii.WarehouseCodeItems" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../aerostyle.css" type="text/css" rel="stylesheet">
</head>
<body bgcolor="#ffffff" leftmargin="0" rightmargin="0" topmargin="0">
    <form id="form1" runat="server">
    <table align="center" width="95%" border="0" cellpadding="0" cellspacing="0">
    <tr>
        <td>
        <asp:Label ID="lblCompany" runat="server" CssClass="copy14grey" ></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
        
        
            <asp:GridView runat="server" ID="gvWarehouseItems" AutoGenerateColumns="False" 
             Width="100%"  
             CellPadding="3" 
            GridLines="Vertical"   >
            <RowStyle  CssClass="copy10grey" BackColor="Gainsboro" />
            <AlternatingRowStyle BackColor="white" />
            <HeaderStyle  CssClass="buttongrid"   />
             <PagerStyle ForeColor="#636363" CssClass="copy10grey" />
            <%-- <FooterStyle CssClass="white"  />--%>
            <Columns>
                
                <%--<asp:BoundField DataField="CompanyName" ItemStyle-Width="25%"  HeaderStyle-HorizontalAlign="Left" HeaderText="Company Name" />
                <asp:BoundField DataField="CompanyAccountNumber" HeaderText="Company A/C#" ItemStyle-HorizontalAlign="Right" />--%>
                <asp:BoundField DataField="ItemCode" HeaderText="Item Code" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"/>
                
                <asp:BoundField DataField="SKU"  HeaderStyle-HorizontalAlign="Center" HeaderText="SKU" />
                <asp:BoundField DataField="MasSKU" HeaderText="MAS SKU" ItemStyle-HorizontalAlign="Left" />
         
                
        
                </Columns>
                </asp:GridView>
   
                </td>
            </tr>
            
    <tr>
        <td>
   <asp:Panel ID="msgPanel" runat="server">
                <br />
                <br />
                 <br />
                <br />
                
    <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="45%" align="center">
    <tr bordercolor="#839abf">
    <td>
        <table align="center">
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblMsg" CssClass="errormessage" Text="No records exists!" runat="server" ></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        </table>

    </td>
    </tr>
    </table>
     <br />
                <br />
                <br />
</asp:Panel>
        </td>
    </tr>
       
            </table>    
    </form>
</body>
</html>
