<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StockReport.aspx.cs" Inherits="avii.Reports.StockReport" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>
<%@ Register TagPrefix="menu" TagName="Menu" Src="~/Controls/Header.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>.:: Lan Global Inc. -  Stock In Hand Report ::.</title>
     <link href="../aerostyle.css" type="text/css" rel="stylesheet"/>
   	
</head>
<body bgcolor="#ffffff" leftmargin="0" rightmargin="0" topmargin="0">
    <form id="form1" runat="server">
    <table cellspacing="0" cellpadding="0" border="0" align="center" width="100%">
    <tr>
        <td>
            <menu:Menu ID="menu1" runat="server" ></menu:Menu>
        </td>
     </tr>
     </table><br />
    <table cellspacing="0" cellpadding="0" border="0" align="center" width="95%">
        
        <tr valign="top">
           
            <td   >
    <table align="center" style="text-align:left" width="100%">
                <tr class="button" align="left">
                <td>&nbsp;Stock In Hand Report</td></tr>
             </table><br />
             <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
      <asp:UpdatePanel ID="UpdatePanel1"  runat="server" >
     <ContentTemplate>
     

      <table  align="center" style="text-align:left" width="99%">
     <tr>
        <td>
           <asp:Label ID="lblMsg" runat="server" CssClass="errormessage"></asp:Label>
            


        </td>
     </tr>
     </table>
     <asp:Panel  ID="pnlSearch" runat="server"  DefaultButton="btnSearch" >
     <table bordercolor="#839abf" border="1" width="100%" align="center" cellpadding="0" cellspacing="0">
      <tr>
        <td>
         
     <table width="100%" border="0" class="box" align="center" cellpadding="2" cellspacing="2">
     <tr style="height:1px">
     <td style="height:1px"></td>
     </tr>
     <tr>
            <td class="copy10grey" align="center" >
                Customer Name:&nbsp; <asp:DropDownList ID="ddlCompany"  runat="server" 
                CssClass="copy10grey">
            </asp:DropDownList></td>
            </tr>
           
               
                <tr>
                <td >
                <hr />
                </td>
                </tr>
                <tr>
                <td  align="center"  >
                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="button" OnClick="btnSearch_Click" CausesValidation="false"/>
                    &nbsp;
                    <%--<asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="button" OnClick="btnCancel_Click" CausesValidation="false"/>
            --%>
        
        </td>
        </tr>
      </table>
           
   
     </td>
     </tr>
     </table> 
      </asp:Panel>          
      
     <table align="center" width="100%" border="0" cellpadding="0" cellspacing="0">
    <tr>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td>
        
          
    <asp:GridView runat="server" ID="gvStockReport" AutoGenerateColumns="False" 
     PageSize="50" AllowPaging="true" Width="100%"  OnPageIndexChanging="gridView_PageIndexChanging"   
     CellPadding="3" 
    GridLines="Vertical"   >
    <RowStyle  CssClass="copy10grey" BackColor="Gainsboro" />
    <AlternatingRowStyle BackColor="white" />
    <HeaderStyle  CssClass="button"   />
     <PagerStyle ForeColor="#636363" CssClass="copy10grey" />
    <%-- <FooterStyle CssClass="white"  />--%>
    <Columns>
    

        <asp:BoundField DataField="ItemCode"  HeaderStyle-HorizontalAlign="Left" HeaderText="Lan Global ProductCode" />
        <asp:BoundField DataField="ItemDescription" HeaderText="Product Description" />
        <asp:BoundField DataField="WarehouseCode" HeaderText="Warehouse Code"  ItemStyle-HorizontalAlign="Right"/>
        <asp:BoundField DataField="TotalQtyInHand" HeaderText="Total Qty In Hand" ItemStyle-HorizontalAlign="Right"/>
        <asp:BoundField DataField="QtyOnPurchaseOrder" HeaderText="Qty On PurchaseOrder"  ItemStyle-HorizontalAlign="Right"/>
        <asp:BoundField DataField="QtyOnBackOrder" HeaderText="Qty On BackOrder" ItemStyle-HorizontalAlign="Right"/>
        <asp:BoundField DataField="QtyOnSalesOrder" HeaderText="Qty On SalesOrder"  ItemStyle-HorizontalAlign="Right"/>
        
         
        
        </Columns>
        </asp:GridView>
   
        </td>
    </tr>
       
    </table>
    
      </ContentTemplate>
     </asp:UpdatePanel>
     
    </td>
    </tr>
    <tr>
        <td>
            <asp:UpdateProgress ID="UpdateProgress1" runat="server"
                     DynamicLayout="false">
            <ProgressTemplate>
                <img src="/Images/ajax-loaders.gif" /> Loading ...
            </ProgressTemplate>
        </asp:UpdateProgress>
        </td>
    </tr>
    </table>
    <br />
    <foot:MenuFooter ID="footer1" runat="server"></foot:MenuFooter>
    

    
    </form>
</body>
</html>
