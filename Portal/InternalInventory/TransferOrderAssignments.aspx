<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TransferOrderAssignments.aspx.cs" Inherits="avii.InternalInventory.TransferOrderAssignments" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="~/Controls/Header.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Transfer Order Assigments</title>
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
            &nbsp;&nbsp;Transfer Order Assigments
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
                 <b>Order Transfer #:</b>
                </td>
                <td width="30%" >
                       <asp:Label ID="lblOrderTransferNumber" CssClass="copy10grey" runat="server"  ></asp:Label>
                </td>
                <td  width="1%">
                       &nbsp;
                    
                </td>
                <td class="copy10grey"  align="right" width="19%" >
                 <b>Order Transfer Date: </b>    
                </td>
                <td width="30%" >
                    <asp:Label ID="lblOrderDate" CssClass="copy10grey" runat="server"  ></asp:Label>
                </td>   
                </tr>
                
                 <tr>
                <td class="copy10grey"  align="right" width="20%" >
                      <b>Requested Qty:</b>
                </td>
                <td width="30%" >
                      <asp:Label ID="lblRequestedQty" CssClass="copy10grey" runat="server"  ></asp:Label>
                </td>
                <td  width="1%">
                       &nbsp;
                    
                </td>
                <td class="copy10grey"  align="right" width="19%" >
                   <b>Tranfer Order Status:</b>
                </td>
                <td width="30%" >
                      <asp:Label ID="lblOrderStatus" CssClass="copy10grey" runat="server"  ></asp:Label>
              
                </td>   
                </tr>
                 
                 <tr>
                <td class="copy10grey"  align="right" width="20%" >
                      <b>Assignment Status:</b>
                </td>
                <td width="30%" >
                      <asp:Label ID="lblAssignmentStatus" CssClass="copy10grey" runat="server"  ></asp:Label>
              
                </td>
                <td  width="1%">
                       &nbsp;
                    
                </td>
                <td class="copy10grey"  align="right" width="19%" >
                    
                </td>
                <td width="30%" >
                      
                </td>   
                </tr>
                
                </table> 
           
                </td>
                </tr>
                    </table>
               <br />
            <table cellSpacing="0" cellPadding="0" align="center" width="100%" border="0">
        <tr>
            <td align="left">
                               
            </td>
            <td align="right">
             
              <strong> <asp:Label ID="lblCount" CssClass="copy10grey" runat="server" ></asp:Label></strong>
                        
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">

        <asp:GridView ID="gvOrders" AutoGenerateColumns="false"   
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
                <asp:TemplateField HeaderText="CategoryName" SortExpression="CategoryName"  HeaderStyle-CssClass="buttongrid"   ItemStyle-HorizontalAlign="Left" 
                    ItemStyle-CssClass="copy10grey" ItemStyle-Width="8%">
                    <ItemTemplate>
                        <%# Eval("CategoryName")%>
                        </ItemTemplate>
                </asp:TemplateField>  
                
                <asp:TemplateField HeaderText="SKU" SortExpression="SKU"  HeaderStyle-CssClass="buttongrid"   ItemStyle-HorizontalAlign="Left" 
                    ItemStyle-CssClass="copy10grey" ItemStyle-Width="12%">
                    <ItemTemplate>
                        <%# Eval("SKU")%>
                        </ItemTemplate>
                </asp:TemplateField>  
                
                <asp:TemplateField HeaderText="Product Name" SortExpression="ProductName"  HeaderStyle-CssClass="buttongrid"   ItemStyle-HorizontalAlign="Left" 
                    ItemStyle-CssClass="copy10grey" ItemStyle-Width="20%">
                    <ItemTemplate>
                        <%# Eval("ProductName")%>
                        </ItemTemplate>
                </asp:TemplateField> 

                 <asp:TemplateField HeaderText="Transfer Date" SortExpression="OrderTransferDateTime"  HeaderStyle-CssClass="buttongrid" ItemStyle-HorizontalAlign="Left" 
                    ItemStyle-CssClass="copy10grey" ItemStyle-Width="8%">
                    <ItemTemplate>
                        <%# Convert.ToDateTime( Eval("TransferedDate")).ToString("MM/dd/yyyy")%>
                        </ItemTemplate>
                </asp:TemplateField>  
                
                <asp:TemplateField HeaderText="Transfered Qty" SortExpression="TransferedQty"  HeaderStyle-CssClass="buttongrid"   ItemStyle-HorizontalAlign="Left" 
                    ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                    <ItemTemplate>
                        <%# Eval("TransferedQty")%>
                        </ItemTemplate>
                </asp:TemplateField>  
                <asp:TemplateField HeaderText="Transfered By" SortExpression="TransferedQty"  HeaderStyle-CssClass="buttongrid"   ItemStyle-HorizontalAlign="Left" 
                    ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                    <ItemTemplate>
                        <%# Eval("AssignedBy")%>
                        </ItemTemplate>
                </asp:TemplateField>  

                     
                                    
            </Columns>
        </asp:GridView>               
        </td>
    </tr> 
        
</table>
     <br />
 

                <table width="95%" align="center">
                <tr>
                    <td align="center">
                         <asp:Button ID="btnCancel" runat="server" Text="Close" CssClass="button" Visible="true" OnClientClick="return close_window();"  />
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
