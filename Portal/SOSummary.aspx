<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SOSummary.aspx.cs" Inherits="avii.SOSummary" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>
<%@ Register TagPrefix="menu" TagName="Menu" Src="~/Controls/Header.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Service Order Request Summary</title>
    <script>
        function InitializeRequest(requestID) {
            // call server side method
            PageMethods.SetSession(requestID);
            var url = "../ManageServiceOrder.aspx";
            window.open(url);
        }
        
    </script>
</head>
<body  bgcolor="#ffffff" leftmargin="0" rightmargin="0" topmargin="0">
    <form id="form1" runat="server">
                <asp:ScriptManager EnablePageMethods="true" ID="MainSM" runat="server" ScriptMode="Release" LoadScriptsBeforeUI="true"></asp:ScriptManager>
    <table cellspacing="0" cellpadding="0" border="0" align="center" width="100%">
    <tr>
        <td>
            <menu:Menu ID="menu1" runat="server" ></menu:Menu>
        </td>
     </tr>
    </table>
    <table cellspacing="0" cellpadding="0" border="0" align="center" style="text-align:left" width="95%">
        <tr class="buttonlabel" align="left">
        <td>&nbsp;Service Order Summary</td></tr>
    </table>
    <table cellspacing="0" cellpadding="0" border="0" align="center" style="text-align:left" width="95%">
        <tr>
            <td>
                <table  align="center" style="text-align:left" width="100%" cellSpacing="0" cellPadding="0">
                 <tr>
                    <td align="left">
                       <asp:Label ID="lblMsg" runat="server" CssClass="errormessage"></asp:Label>
            
                    </td>
                 </tr>
                 </table>
                <table bordercolor="#839abf" border="0" width="100%" align="center" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                    
                    <asp:Repeater ID="rptSoR" runat="server" Visible="true" OnItemDataBound="rpt_OnItemDataBound">
                    <HeaderTemplate>
                    <table bordercolor="#839abf" border="1" width="100%" cellpadding="5" cellspacing="5">
                    <tr>
                        <td class="buttongrid" width="1%" >
                            S.No.
                        </td>
                        <td class="buttongrid" width="8%">
                            Category Name
                        </td>
                                    
                        <td class="buttongrid" width="18%">
                            Product Name
                        </td>
                                    
                        <td class="buttongrid" width="12%">
                            SKU
                        </td>
                        <td class="buttongrid" width="8%">
                            Service Orders 
                        </td>
                        <td class="buttongrid" width="8%">
                            Quantity 
                        </td>
                        
                        <td class="buttongrid" width="45%">
                            Order #
                        </td>                        
                    </tr>
                    </HeaderTemplate>
                    <ItemTemplate>    
                        <tr valign="top"  class="<%# Container.ItemIndex % 2 == 0 ? "alternaterow" : "" %>">
                            <td class="copy10grey"  >
                            <%# Container.ItemIndex +  1 %>
                            </td>
                            <td valign="top" class="copy10grey"  >
                                    <%# Eval("CategoryName")%>                                 
                            </td>
                            <td valign="top" class="copy10grey"  >
                                    <%# Eval("ProductName")%>                                 
                            </td>
                            <td valign="top" class="copy10grey"  >
                                    <%# Eval("SKU")%>                                 
                            </td>
                            <td valign="top" class="copy10grey" align="right" >
                                    <%# Eval("OrderCount")%>&nbsp;                                
                            </td>
                            <td valign="top" class="copy10grey" align="right" >
                                    <%# Eval("Quantity")%>&nbsp;                                
                            </td>
                            
                            <td valign="top" class="copy10grey"  >
                                    <%# Eval("ServiceRequestIDs")%>                                 
                            </td>                                        
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        <tr>
                            <td class="copy10grey"></td>
                            <td class="copy10grey"></td>
                            <td class="copy10grey"></td>
                            <td class="copy10grey"><strong>Total</strong></td>
                            <td class="copy10grey" align="right" >
                                <strong>  <asp:Label ID="lblOrderCount" runat="server" CssClass="copy10grey" ></asp:Label></strong> 
                            </td>
                            <td class="copy10grey" align="right" >
                                <strong> <asp:Label ID="lblQty" runat="server" CssClass="copy10grey" ></asp:Label></strong> 
                            </td>
                            <td class="copy10grey"> </td>
                        </tr>
                        </table>
                    </FooterTemplate>
                    </asp:Repeater>
        
                    </td>
                  </tr>
                 </table>
            
            </td>
        </tr>
    </table>
    <br /><br /> <br />
            <br /> <br />
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
