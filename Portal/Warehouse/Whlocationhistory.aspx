<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Whlocationhistory.aspx.cs" Inherits="avii.Warehouse.Whlocationhistory" %>
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
            &nbsp;&nbsp;Warehouse location History
			</td>
		</tr>
    
    </table>  
    <asp:UpdatePanel ID="upPnlPOA" runat="server" UpdateMode="Conditional">
	<ContentTemplate>
        <table width="95%" align="center" cellpadding="0" cellspacing="0">

        <tr>
                         <td align="right">
                             <asp:Label ID="lblCount" CssClass="copy10grey" runat="server" ></asp:Label>
                    
                         </td>
                     </tr> 
                     
        <tr>
            <td colspan="2" class="copy10grey">
                                <asp:Label ID="lblMsg" CssClass="errormessage" runat="server" ></asp:Label>
                    
            <asp:GridView ID="gvWhHistory" AutoGenerateColumns="false"   
            Width="100%" ShowHeader="true"  ShowFooter="false" runat="server" GridLines="Both"
            AllowPaging="true" OnPageIndexChanging="gvWhHistory_PageIndexChanging" PageSize="50" 
               AllowSorting="true" OnSorting="gvWhHistory_Sorting" >                        
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
                <asp:TemplateField HeaderText="Warehouse Location" SortExpression="WarehouseLocation"  HeaderStyle-CssClass="buttonundlinelabel"   ItemStyle-HorizontalAlign="Left" 
                    ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                    <ItemTemplate>
                        <%# Eval("WarehouseLocation")%>
                        </ItemTemplate>
                </asp:TemplateField>  
                 <asp:TemplateField HeaderText="Category Name" SortExpression="CategoryName"  HeaderStyle-CssClass="buttonundlinelabel" ItemStyle-HorizontalAlign="Left" 
                    ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                    <ItemTemplate>
                        <%# Eval("CategoryName")%>
                        </ItemTemplate>
                </asp:TemplateField>  
               
                <asp:TemplateField HeaderText="SKU" SortExpression="SKU"  HeaderStyle-CssClass="buttonundlinelabel"   ItemStyle-HorizontalAlign="Left" 
                    ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                    <ItemTemplate>
                        <%# Eval("SKU")%>
                        </ItemTemplate>
                </asp:TemplateField>  
                <asp:TemplateField HeaderText="Product Name" SortExpression="ItemName"  HeaderStyle-CssClass="buttonundlinelabel"   ItemStyle-HorizontalAlign="Left" 
                    ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                    <ItemTemplate>
                        <%# Eval("ItemName")%>
                        </ItemTemplate>
                </asp:TemplateField>  
                
                <asp:TemplateField HeaderText="Aisle" SortExpression="Aisle"  HeaderStyle-CssClass="buttonundlinelabel"   ItemStyle-HorizontalAlign="Left" 
                    ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                    <ItemTemplate>
                        <%# Eval("Aisle")%>
                        </ItemTemplate>
                </asp:TemplateField>  
                <asp:TemplateField HeaderText="Bay" SortExpression="Bay"  HeaderStyle-CssClass="buttonundlinelabel"   ItemStyle-HorizontalAlign="Left" 
                    ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                    <ItemTemplate>
                        <%# Eval("Bay")%>
                        </ItemTemplate>
                </asp:TemplateField>  
                <asp:TemplateField HeaderText="Level" SortExpression="RowLevel"  HeaderStyle-CssClass="buttonundlinelabel"   ItemStyle-HorizontalAlign="Left" 
                    ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                    <ItemTemplate>
                        <%# Eval("RowLevel")%>
                        </ItemTemplate>
                </asp:TemplateField>  
                  
                <asp:TemplateField HeaderText="Quantity" SortExpression="Quantity"  HeaderStyle-CssClass="buttonundlinelabel"   ItemStyle-HorizontalAlign="Right" 
                    ItemStyle-CssClass="copy10grey" ItemStyle-Width="4%">
                    <ItemTemplate>
                        <%# Eval("Quantity")%>
                                 <%--<asp:LinkButton  ToolTip="View detail" Visible='<%# Convert.ToInt32(Eval("Quantity")) > 0 ? true : false %>' CausesValidation="false" Height="18" OnCommand="lnkView_Command" 
                                                CommandArgument='<%# Convert.ToString(Eval("WarehouseLocation")) + ","+ Convert.ToString(Eval("ItemCompanyGUID")) %>'  
                                             ID="lnkView"  runat="server"  ><%# Eval("Quantity")%>
                                            </asp:LinkButton>                                           
                                 --%>  
                        </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Create Date" SortExpression="LastReceivedDate"  HeaderStyle-CssClass="buttonundlinelabel"   ItemStyle-HorizontalAlign="Left" 
                    ItemStyle-CssClass="copy10grey" ItemStyle-Width="6%">
                    <ItemTemplate>
                        <%# Convert.ToDateTime( Eval("LastReceivedDate")).ToString("MM/dd/yyyy")%>
                        </ItemTemplate>
                </asp:TemplateField>  
               
                 <asp:TemplateField HeaderText="Source" SortExpression="LogSource"  HeaderStyle-CssClass="buttonundlinelabel"   ItemStyle-HorizontalAlign="Left" 
                    ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                    <ItemTemplate>
                        <%# Eval("LogSource")%>
                        </ItemTemplate>
                </asp:TemplateField>  
               
                     
                                    
            </Columns>
            </asp:GridView>               
       
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

