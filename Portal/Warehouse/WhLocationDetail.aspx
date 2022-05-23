<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WhLocationDetail.aspx.cs" Inherits="avii.Warehouse.WhLocationDetail" %>
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
            &nbsp;&nbsp;Warehouse location detail
			</td>
		</tr>
    
    </table>  
    <asp:UpdatePanel ID="upPnlPOA" runat="server" UpdateMode="Conditional">
	<ContentTemplate>
        <table width="95%" align="center" cellpadding="0" cellspacing="0">

        <tr>
                         <td align="right">
                             <asp:Label ID="lblESN" CssClass="copy10grey" runat="server" ></asp:Label>
                    
                         </td>
                     </tr> 
                     
        <tr>
                <td colspan="2" class="copy10grey">
                                <asp:Label ID="lblMsg" CssClass="errormessage" runat="server" ></asp:Label>
                    
                    <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="100%" align="center" runat="server" id="tblESN" >
                        <tr bordercolor="#839abf">
                            <td>                                   
                                        
                                        <asp:Repeater ID="rptSKUESN" runat="server">
                                            <HeaderTemplate>
                                            <table width="100%" align="center">
                                                <tr >
                                                    <td class="buttongrid" width="2%">
                                                        &nbsp;S.No.
                                                    </td>
                                                    <td class="buttongrid" width="10%">
                                                        &nbsp;ESN
                                                    </td>
                                                    <td class="buttongrid" width="10%">
                                                        &nbsp;Hex
                                                    </td>
                                                    <td class="buttongrid" width="10%">
                                                        &nbsp;Dec
                                                    </td>
                                                    <td class="buttongrid" width="10%">
                                                        &nbsp;Location
                                                    </td>
                                                    <td class="buttongrid" width="10%">
                                                        &nbsp;Category Name
                                                    </td>
                                                    <td class="buttongrid" width="10%">
                                                        &nbsp;SKU
                                                    </td>
                                                    <td class="buttongrid" width="20%">
                                                        &nbsp;Product Name
                                                    </td>
                                                    <td class="buttongrid" width="5%">
                                                        &nbsp;Aisle
                                                    </td>
                                                    <td class="buttongrid" width="5%">
                                                        &nbsp;Bay
                                                    </td>
                                                    <td class="buttongrid" width="5%">
                                                        &nbsp;Row Level
                                                    </td>
                                                    
                                                    <td class="buttongrid" width="5%">
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
                                                        &nbsp;<%# Eval("MEID")%></td>
                                                <td class="copy10grey">
                                                        &nbsp;<%# Eval("Location")%></td>
                                                
                                                <td class="copy10grey">
                                                        &nbsp;<%# Eval("CategoryName")%></td>
                                                <td class="copy10grey">
                                                        &nbsp;<%# Eval("SKU")%></td>
                                                <td class="copy10grey">
                                                        &nbsp;<%# Eval("ProductName")%></td>
                                                <td class="copy10grey">
                                                        &nbsp;<%# Eval("Aisle")%></td>
                                                <td class="copy10grey">
                                                        &nbsp;<%# Eval("Bay")%></td>
                                                <td class="copy10grey">
                                                        &nbsp;<%# Eval("RowLevel")%></td>
                                                
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
                     <tr>
                         <td align="right">
                             <asp:Label ID="lblAccessory" CssClass="copy10grey" runat="server" ></asp:Label>
                    
                         </td>
                     </tr> 
                     <tr>
                         <td>
                    
                    <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="100%" align="center" runat="server" id="tblAccessory" >
                        <tr bordercolor="#839abf">
                            <td>                                   
                                 <asp:Repeater ID="rptAccessory" runat="server">
                                            <HeaderTemplate>
                                            <table width="100%" align="center">
                                                <tr >
                                                    <td class="buttongrid" width="2%">
                                                        &nbsp;S.No.
                                                    </td>
                                                    <td class="buttongrid" width="15%">
                                                        &nbsp;Warehouse location
                                                    </td>
                                                    <td class="buttongrid" width="10%">
                                                        &nbsp;Receive Date
                                                    </td>
                                                    <td class="buttongrid" width="10%">
                                                        &nbsp;Category Name
                                                    </td>
                                                    <td class="buttongrid" width="10%">
                                                        &nbsp;SKU
                                                    </td>
                                                    <td class="buttongrid" width="20%">
                                                        &nbsp;Product Name
                                                    </td>
                                                    <td class="buttongrid" width="5%">
                                                        &nbsp;Aisle
                                                    </td>
                                                    <td class="buttongrid" width="5%">
                                                        &nbsp;Bay
                                                    </td>
                                                    <td class="buttongrid" width="5%">
                                                        &nbsp;Row Level
                                                    </td>
                                                    
                                                    <td class="buttongrid" width="5%">
                                                        &nbsp;BoxID
                                                    </td>
                                                    <td class="buttongrid" width="2%">
                                                        &nbsp;Quantity
                                                    </td>
                                                   
                                                </tr>
                            
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                            <tr class="<%# Container.ItemIndex % 2 == 0 ? "alternaterow" : "" %>">
                                                
                                                <td class="copy10grey">
                                                        &nbsp;<%# Container.ItemIndex + 1 %></td>
                                
                                                <td class="copy10grey">
                                                        &nbsp;<%# Eval("WareHouseLocation")%></td>
                                                <td class="copy10grey">
                                                        &nbsp;<%# Eval("UploadDate")%></td>
                                                <td class="copy10grey">
                                                        &nbsp;<%# Eval("CategoryName")%></td>
                                                <td class="copy10grey">
                                                        &nbsp;<%# Eval("SKU")%></td>
                                                <td class="copy10grey">
                                                        &nbsp;<%# Eval("ProductName")%></td>
                                                <td class="copy10grey">
                                                        &nbsp;<%# Eval("Aisle")%></td>
                                                <td class="copy10grey">
                                                        &nbsp;<%# Eval("Bay")%></td>
                                                <td class="copy10grey">
                                                        &nbsp;<%# Eval("RowLevel")%></td>
                                                
                                                <td class="copy10grey">
                                                        &nbsp;<%# Eval("BoxID")%></td>
                                                <td class="copy10grey">
                                                        &nbsp;<%# Eval("Quantity")%></td>
                                                
                                
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
              <br /> <br />
              <br /> <br />
              <br /> <br />
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

