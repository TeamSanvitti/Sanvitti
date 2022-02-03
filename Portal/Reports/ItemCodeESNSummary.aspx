<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ItemCodeESNSummary.aspx.cs" Inherits="avii.Reports.ItemCodeESNSummary" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>
<%@ Register TagPrefix="menu" TagName="Menu" Src="~/Controls/Header.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>ESN Repository
</title>
     <link href="../aerostyle.css" type="text/css" rel="stylesheet"/>
   	 
</head>
<body  bgcolor="#ffffff" leftmargin="0" rightmargin="0" topmargin="0">
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
                <td>&nbsp;ESN Repository</td></tr>
             </table>
     
     
     
     <table  align="center" style="text-align:left" width="99%">
     <tr>
        <td>
           <asp:Label ID="lblMsg" runat="server" CssClass="errormessage"></asp:Label>
            
        </td>
     </tr>
     <tr>
        <td class="copy10grey">
            <%--Data is for past one year<br />--%>
            - Following ESN Repository report is between <strong> <asp:Label ID="lblFromDate" runat="server" CssClass="copy10grey"></asp:Label></strong> 
            and <strong><asp:Label ID="lblToDate" runat="server" CssClass="copy10grey"></asp:Label></strong>

        </td>
     </tr>
     </table>
     <br />
           
               
                
        
                   
        <table align="center" width="100%" border="0" cellpadding="0" cellspacing="0">
    <%--<tr>
        <td>&nbsp;</td>
    </tr>--%>
    <tr>
        <td>
        
        
    <asp:GridView runat="server" ID="gvESN" AutoGenerateColumns="False" 
     PageSize="50" AllowPaging="true" Width="100%" OnPageIndexChanging="gridView_PageIndexChanging"   
     CellPadding="3" 
    GridLines="Vertical"   >
    <RowStyle  CssClass="copy10grey" BackColor="Gainsboro" />
    <AlternatingRowStyle BackColor="white" />
    <HeaderStyle  CssClass="button"   />
    <PagerStyle ForeColor="#636363" CssClass="copy10grey" />
    <%-- <FooterStyle CssClass="white"  />--%>
    <Columns>
    
        <asp:BoundField DataField="ItemCode" HeaderText="Item Code" ItemStyle-Width="250" HeaderStyle-HorizontalAlign="Left" />
        
    <%-- status, mslnumber only one year of records      <asp:BoundField DataField="modelNumber" HeaderText="Model#" /> --%>

        <asp:BoundField DataField="Active" HeaderText="Active" ItemStyle-HorizontalAlign="Right" />
        <asp:BoundField DataField="Used" HeaderText="Used" ItemStyle-HorizontalAlign="Right"/>
        <asp:BoundField DataField="RMA" HeaderText="RMA" ItemStyle-HorizontalAlign="Right"/>
        
        
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
