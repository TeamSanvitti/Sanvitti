<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomDeclarations.aspx.cs" Inherits="avii.CustomDeclarations" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>
<%@ Register TagPrefix="menu" TagName="Menu" Src="~/Controls/Header.ascx" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Custom Declaration</title>
    <script>
        
        function close_window() {
            if (confirm("Close Window?")) {
                window.close();
                return true;
            }
            else
                return false
        }

    </script>
</head>
<body bgcolor="#ffffff" leftmargin="0" rightmargin="0" topmargin="0">
    <form id="form1" runat="server">
        <table cellspacing="0" cellpadding="0" border="0" align="center" width="100%">
    <tr>
        <td>
            <menu:Menu ID="menu1" runat="server" ></menu:Menu>
        </td>
     </tr>
     </table>
    
            <table align="center" style="text-align:left" width="95%">
                <tr class="buttonlabel" align="left">
                <td>&nbsp;Custom Declaration</td></tr>
             </table>
                <br />
                   <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="95%"  align="center">
                   <tr bordercolor="#839abf">
                    <td>
                        <table width="100%" bgColor="#ffffff" cellpadding="3" cellspacing="3"  align="center">                            
                            <tr>
                                <td class="copy10grey" align="left">
                                    <asp:Repeater ID="rptCustom" runat="server">
                                        <HeaderTemplate>
                                            <table width="100%" align="center">
                                                <tr>
                                                    <td class="buttongrid" width="2%">
                                                        &nbsp;S.No.
                                                    </td>
                                                    <td class="buttongrid">
                                                        &nbsp;SKU#
                                                    </td>
                                                    <td class="buttongrid">
                                                        &nbsp;ProductName
                                                    </td>
                                                    <td class="buttongrid">
                                                        &nbsp;Custom Value
                                                    </td>                                                    
                                                </tr>                            
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                            <tr class="<%# Container.ItemIndex % 2 == 0 ? "alternaterow" : "" %>">                                                
                                                <td class="copy10grey">
                                                        &nbsp;<%# Container.ItemIndex + 1 %></td>                                
                                                <td class="copy10grey">
                                                        &nbsp;<%# Eval("SKU")%>

                                                   <%-- <asp:Label ID="lbSKU"  CssClass="copy10grey"  Text='<%# Eval("SKU")%>'   runat="server"></asp:Label>
                                                   --%>
                                                    </td>
                                                <td class="copy10grey">
                                                        &nbsp;<%# Eval("SKU")%>
                                                    </td>
                                                <td class="copy10grey">
                                                    &nbsp;<%# Eval("CustomValue")%>
                                                     

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
                        </td>
                    </tr>
                    </table>

        <br />
        <table width="100%" align="center">
                <tr>
                    <td align="center">
                         <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="button" Visible="true" OnClientClick="return close_window();"  />
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
