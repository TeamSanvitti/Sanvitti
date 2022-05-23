<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UnProvisionEsns.aspx.cs" Inherits="avii.UnProvisionEsns" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="~/Controls/Header.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
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
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    
    <table cellSpacing="0" cellPadding="0" align="center" width="100%" border="0">
        <tr>
			<td><head:MenuHeader id="HeadAdmin" runat="server"></head:MenuHeader></td>
		</tr>
     </table>
    <table cellspacing="0" cellpadding="0" border="0" align="center" width="95%">
		<tr>
			<td  bgcolor="#dee7f6" class="buttonlabel">
            &nbsp;&nbsp;Unprovisioned ESN
			</td>
		</tr>
    
    </table>  
    <asp:UpdatePanel ID="upPnlPOA" runat="server" UpdateMode="Conditional">
	<ContentTemplate>
        <table width="95%" align="center" cellpadding="0" cellspacing="0">
        <tr>
                <td colspan="2" class="copy10grey">
                    
                    <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="100%" align="center" >
                           <tr bordercolor="#839abf">
                                <td>
                                    
                                        
                            <asp:Label ID="lblMsg" CssClass="errormessage" runat="server" ></asp:Label>
                                        <asp:Repeater ID="rptSKUESN" runat="server">
                                            <HeaderTemplate>
                                            <table width="100%" align="center">
                                                <tr >
                                                    <td class="buttongrid" width="2%">
                                                        &nbsp;S.No.
                                                    </td>
                                                    <td class="buttongrid">
                                                        &nbsp;ESN
                                                    </td>
                                                    <td class="buttongrid">
                                                        &nbsp;Hex
                                                    </td>
                                                    <td class="buttongrid">
                                                        &nbsp;Dec
                                                    </td>
                                                    <td class="buttongrid">
                                                        &nbsp;Location
                                                    </td>
                                                    <%--<td runat="server" visible='<%# ContainerID == "" ? false:true%>' class="buttongrid" >
                                                        &nbsp;PalletID
                                                    </td>
                                                    <td runat="server" visible='<%# ContainerID == "" ? false:true%>' class="buttongrid" >
                                                        &nbsp;ContainerID
                                                    </td>--%>
                                                    <td class="buttongrid">
                                                        &nbsp;BoxID
                                                    </td>
                                                </tr>
                            
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                            <tr class="<%# Container.ItemIndex % 2 == 0 ? "alternaterow" : "" %>">
                                                
                                                <td class="copy10grey">
                                                        &nbsp;<%# Container.ItemIndex + 1 %></td>
                                
                                                <td class="copy10grey">
                                                        &nbsp;<%# Eval("ESN")%></td>
                                                <td class="copy10grey">
                                                        &nbsp;<%# Eval("Hex")%></td>
                                                <td class="copy10grey">
                                                        &nbsp;<%# Eval("Dec")%></td>
                                                <td class="copy10grey">
                                                        &nbsp;<%# Eval("Location")%></td>
                                                
                                                <%--<td class="copy10grey" runat="server" visible='<%# Convert.ToString(Eval("ContainerID")) == "" ? false:true%>' >
                                                        &nbsp;<%# Eval("PalletID")%></td>
                                                <td class="copy10grey" runat="server" visible='<%# Convert.ToString(Eval("ContainerID")) == "" ? false:true%>' >
                                                        &nbsp;<%# Eval("ContainerID")%></td>
                                                --%>
                                                <td class="copy10grey">
                                                        &nbsp;<%# Eval("BoxID")%></td>
                                
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
                         <asp:Button ID="btnCancel" runat="server" Text="Close" CssClass="button" Visible="true" OnClientClick="return close_window();"  />
                    </td>
                </tr>
                </table>
        
    </ContentTemplate>
    </asp:UpdatePanel>

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
