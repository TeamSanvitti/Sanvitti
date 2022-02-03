<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewWarehouseLocation.aspx.cs" Inherits="avii.Warehouse.ViewWarehouseLocation" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="~/Controls/Header.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>View Warehouse location</title>
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
            &nbsp;&nbsp;Warehouse Location
			</td>
		</tr>    
    </table>
    <table cellSpacing="0" cellPadding="0" align="center" width="95%" border="0" id="maintbl">
        <tr>
	        <td>
            <asp:UpdatePanel ID="upnlCode" UpdateMode="Conditional" runat="server">
	        <ContentTemplate>	            
                <table cellSpacing="0" cellPadding="0" align="center" width="100%" border="0">
    	            <tr>                    
                        <td><asp:Label ID="lblMsg" runat="server"  CssClass="errormessage"></asp:Label></td>
                    </tr> 
                </table>
                <table bordercolor="#839abf" border="1" width="100%" align="center" cellpadding="0" cellspacing="0">
            <tr>
            <td>
             <table width="100%" border="0" class="" align="center" cellpadding="5" cellspacing="5">                     
                 <tr>
                <td class="copy10grey"  align="right" width="20%" >
                 <b>Warehouse city</b>:
                </td>
                <td width="30%" >
                    <asp:Label ID="lblWarehouseCity"  runat="server" CssClass="copy10grey">                         
                    </asp:Label>

                </td>
                <td  width="1%">
                       &nbsp;
                    
                </td>
                <td class="copy10grey"  align="right" width="19%" >
                      <b>Customer</b>:
                </td>
                <td width="30%" >
                    <asp:Label ID="lblCustomer"  runat="server" CssClass="copy10grey">                         
                    </asp:Label>

                </td>   
                </tr>
                <tr>
                     <td colspan="5" >
                         <hr />
                     </td>
                 </tr>
                <tr>
                <td colspan="5">
                    <table width="100%" border="0" class="" align="center" cellpadding="5" cellspacing="5">               
                    <tr>
                        <td class="copy10grey"  align="right" width="15%" >
                        <b>Warehouse Location</b>:
                    </td>
                    <td width="15%" >                   
                    <asp:Label ID="lblLocation"  runat="server" CssClass="copy10grey">                         
                    </asp:Label>

                    </td>
                    <td class="copy10grey"  align="right" width="8%" >
                   <b>Aisle</b>:
                </td>
                <td width="15%" >
                    
                <asp:Label ID="lblAisle"  runat="server" CssClass="copy10grey">                         
                    </asp:Label>

                </td>
                <td class="copy10grey"  align="right" width="8%" >
                    <b>Bay</b>:
                </td>
                <td width="15%" >
                <asp:Label ID="lblBay"  runat="server" CssClass="copy10grey">                         
                    </asp:Label>

                </td>   
                    <td class="copy10grey"  align="right" width="8%" >
                       <b>Level</b>: 
                    </td>
                    <td width="15%" >
                             <asp:Label ID="lblLevel"  runat="server" CssClass="copy10grey">                         
                    </asp:Label>

                    </td>   
                
                
                    </tr>
                    </table>
                </td>
                </tr>
                 </table>
            </td>
            </tr>
            </table>   
             <br />
            <table cellSpacing="0" cellPadding="0" align="center" width="100%" border="0">
                <tr>
			        <td  bgcolor="#dee7f6" class="buttonlabel">
                    &nbsp;Location Allowcation
			        </td>
		        </tr>    
                <tr>
			        <td align="center" >
			        <asp:GridView ID="gvWHLocation" AutoGenerateColumns="false"   
                    Width="100%" ShowHeader="true"  ShowFooter="false" runat="server" GridLines="Both">                        
                    <RowStyle BackColor="Gainsboro" />
                    <AlternatingRowStyle BackColor="white" />
                    <HeaderStyle  CssClass="buttongrid" ForeColor="white"/>
                        <PagerStyle ForeColor="#636363" CssClass="copy10grey" HorizontalAlign="Left" />
                        <Columns>
                            <asp:TemplateField HeaderText="S.No." ItemStyle-CssClass="copy10grey"  ItemStyle-Width="1%">                
                            <ItemTemplate>
                                    <%#  Container.DataItemIndex + 1%>               
                            </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Customer Name" HeaderStyle-CssClass="buttongrid"   ItemStyle-HorizontalAlign="Left" 
                                ItemStyle-CssClass="copy10grey" ItemStyle-Width="15%">
                                <ItemTemplate>
                                    <%# Eval("CustomerName")%>
                                    </ItemTemplate>
                            </asp:TemplateField>  
                            
                            <asp:TemplateField HeaderText="SKU" HeaderStyle-CssClass="buttongrid"   ItemStyle-HorizontalAlign="Left" 
                                ItemStyle-CssClass="copy10grey" ItemStyle-Width="20%">
                                <ItemTemplate>
                                    <%# Eval("SKU")%>
                                    </ItemTemplate>
                            </asp:TemplateField>  
                            <asp:TemplateField HeaderText="ESN Count" HeaderStyle-CssClass="buttongrid"   ItemStyle-HorizontalAlign="Left" 
                                ItemStyle-CssClass="copy10grey" ItemStyle-Width="15%">
                                <ItemTemplate>
                                    <%# Eval("ESNCount")%>
                                    </ItemTemplate>
                            </asp:TemplateField>  
                            
                            <asp:TemplateField HeaderText="Received Date" HeaderStyle-CssClass="buttongrid"   ItemStyle-HorizontalAlign="Left" 
                                ItemStyle-CssClass="copy10grey" ItemStyle-Width="15%">
                                <ItemTemplate>
                                    <%# Convert.ToDateTime(Eval("ReceivedDate")).ToString("MM/dd/yyyy") %>
                                    </ItemTemplate>
                            </asp:TemplateField>  
                            <asp:TemplateField HeaderText="Received By" HeaderStyle-CssClass="buttongrid"   ItemStyle-HorizontalAlign="Left" 
                                ItemStyle-CssClass="copy10grey" ItemStyle-Width="15%">
                                <ItemTemplate>
                                    <%# Eval("ReceivedBy")%>
                                    </ItemTemplate>
                            </asp:TemplateField>  

                            <%--<asp:TemplateField HeaderText="ESN" HeaderStyle-CssClass="buttongrid"   ItemStyle-HorizontalAlign="Left" 
                                ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                <ItemTemplate>
                                    <%# Eval("ESN")%>
                                    </ItemTemplate>
                            </asp:TemplateField>  
                            <asp:TemplateField HeaderText="DEC" HeaderStyle-CssClass="buttongrid"   ItemStyle-HorizontalAlign="Left" 
                                ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                <ItemTemplate>
                                    <%# Eval("DEC")%>
                                    </ItemTemplate>
                            </asp:TemplateField>  
                            <asp:TemplateField HeaderText="HEX" HeaderStyle-CssClass="buttongrid"   ItemStyle-HorizontalAlign="Left" 
                                ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                <ItemTemplate>
                                    <%# Eval("HEX")%>
                                    </ItemTemplate>
                            </asp:TemplateField>  
                            <asp:TemplateField HeaderText="BOXID" HeaderStyle-CssClass="buttongrid"   ItemStyle-HorizontalAlign="Left" 
                                ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                <ItemTemplate>
                                    <%# Eval("BOXID")%>
                                    </ItemTemplate>
                            </asp:TemplateField>  
                            <asp:TemplateField HeaderText="SerialNumber" HeaderStyle-CssClass="buttongrid"   ItemStyle-HorizontalAlign="Left" 
                                ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                <ItemTemplate>
                                    <%# Eval("SerialNumber")%>
                                    </ItemTemplate>
                            </asp:TemplateField>  
                            <asp:TemplateField HeaderText="Fulfillment#" HeaderStyle-CssClass="buttongrid"   ItemStyle-HorizontalAlign="Left" 
                                ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                <ItemTemplate>
                                    <%# Eval("FulfillmentNumber")%>
                                    </ItemTemplate>
                            </asp:TemplateField>  
                            <asp:TemplateField HeaderText="RmaNumber" HeaderStyle-CssClass="buttongrid"   ItemStyle-HorizontalAlign="Left" 
                                ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                <ItemTemplate>
                                    <%# Eval("RmaNumber")%>
                                    </ItemTemplate>
                            </asp:TemplateField>  --%>
                        </Columns>
                    </asp:GridView>               
                    <table bordercolor="#839abf" border="1" width="100%" align="center" cellpadding="0" cellspacing="0" visible="false" runat="server" id="tblMsg">
                    <tr>
                        <td align="left">
                            <asp:Label ID="lblLocationMsg" runat="server"  Text="No location found!" CssClass="errormessage"></asp:Label>
                        </td>
                    </tr>
                    </table>
			        
                    
                    </td>
			    </tr>
			    </table> 
            </ContentTemplate>
            </asp:UpdatePanel>
		
            </td>
       </tr>
    </table>
        <br /> <br />
            <br /> <br /><br /> <br />
            
        <table width="100%">
        <tr>
		    <td>
			    <foot:MenuFooter id="Menuheader2" runat="server"></foot:MenuFooter>
		    </td>
	    </tr>
        </table>
    
    </form>
</body>
</html>
