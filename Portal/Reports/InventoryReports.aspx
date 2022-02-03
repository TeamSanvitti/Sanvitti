<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InventoryReports.aspx.cs" Inherits="avii.Reports.InventoryReports" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>
<%@ Register TagPrefix="menu" TagName="Menu" Src="~/Controls/Header.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Inventory Stock Report</title>
    <link href="../aerostyle.css" type="text/css" rel="stylesheet"/>
   	
</head>
<body bgcolor="#ffffff" leftmargin="0" rightmargin="0" topmargin="0">
    <form id="form1" runat="server" defaultbutton="btnSearch">
    <table cellspacing="0" cellpadding="0" border="0" align="center" width="100%">
    <tr>
        <td>
            <menu:Menu ID="menu1" runat="server" ></menu:Menu>
        </td>
     </tr>
     </table>
     <table cellspacing="0" cellpadding="0" border="0" align="center" width="95%">
        
        <tr valign="top">
           
            <td   ><br />
            <table align="center" style="text-align:left" width="100%">
                <tr class="button" align="left">
                <td>&nbsp;Inventory Stock Report</td></tr>
             </table>

             <table  align="center" style="text-align:left" width="99%">
             <tr>
                <td>
                   <asp:Label ID="lblMsg" runat="server" CssClass="errormessage"></asp:Label>
            
                </td>
             </tr>
             <tr><td class="copy10grey">
                        - Please select your search
                        criterial to narrow down the search and record selection.<br />
                        <%--- Atleast one search criteria should be selected.<br />--%>
                        - Click on 'Search' button to view all Products.
                
                        </td></tr>
             </table>
             <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="99%" >

             <tr bordercolor="#839abf">
             <td>
             <table  width="100%" align="center" cellpadding="1" cellspacing="1">
              <tr>
              <td>
                &nbsp;
              </td>
              </tr>
              <tr style="display:none">
                <td align="right" class="copy10grey">
                Customer:&nbsp;
                </td>
                <td width="65%">
                    <asp:DropDownList ID="ddlCompany"  runat="server" Width="80%" 
                        CssClass="copy10grey">
                    </asp:DropDownList>
                </td>
             </tr>
             <tr>
                <td align="right" class="copy10grey">
                Item Code:&nbsp;
                </td>
                <td width="65%" align="left">
                    <asp:TextBox ID="txtItemcode"  runat="server" Width="70%" 
                        CssClass="copy10grey">
                    </asp:TextBox>
                </td>
             </tr>
             <tr>
              <td colspan="2">
              <hr />
              </td>
              </tr>
             <tr>
                <td  align="center"  colspan="2">
                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="button" OnClick="btnSearch_Click" CausesValidation="false"/>
                     &nbsp;
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="button" OnClick="btnCancel_Click" CausesValidation="false"/>
            
        
                </td>
                </tr>
             </table>  
             </td>
             </tr>
             </table>
             <br />
             
             <table width="100%">
             <tr>
                <td align="right">
                    <asp:Label ID="lblCount" runat="server" CssClass="copy10grey" ></asp:Label>
                </td>
             </tr>
             <tr>
                <td>
                    <asp:GridView runat="server" ID="gvInventory" AutoGenerateColumns="False" 
                     PageSize="50" AllowPaging="true" Width="100%"  OnPageIndexChanging="gridView_PageIndexChanging"   
                     CellPadding="3" 
                    GridLines="Vertical"   >
                    <RowStyle  CssClass="copy10grey" BackColor="Gainsboro" />
                    <AlternatingRowStyle BackColor="white" />
                    <HeaderStyle  CssClass="button"   />
                     <PagerStyle ForeColor="#636363" CssClass="copy10grey" />
                    <%-- <FooterStyle CssClass="white"  />--%>
                    <Columns>
                         <asp:BoundField DataField="Category1" HeaderText="Account#" HeaderStyle-HorizontalAlign="Left"/>
        
                        <asp:BoundField DataField="ItemNumber" ItemStyle-Width="150"   HeaderText="Item#" />
                        <asp:BoundField DataField="ItemDescription" HeaderText="Item Description" />
        
                    <%--     <asp:BoundField DataField="modelNumber" HeaderText="Model#" /> --%>
                        <%--<asp:BoundField DataField="CompanyName" HeaderText="Company Name" />--%>
                        <asp:BoundField DataField="TotalQtyOnHand" HeaderText="Total Qty On Hand" ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField DataField="ReturnsAllowed" HeaderText="Returns Allowed" />
                        <asp:BoundField DataField="RestockingMethod" HeaderText="Restocking Method" />
        
                        <asp:BoundField DataField="BuyerCode" HeaderText="Buyer Code" />
        
         
                        </Columns>
                        </asp:GridView>
   
                </td>
             </tr>
             </table>   

        </td>
        </tr>
        </table>

        <br />

        <table cellspacing="0" cellpadding="0" border="0" align="center" width="100%">
        <tr>
            <td >
                <foot:MenuFooter ID="footer2" runat="server"></foot:MenuFooter>
            </td>
         </tr>
         </table>
    
    </form>
</body>
</html>
