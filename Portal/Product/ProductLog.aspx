<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductLog.aspx.cs" Inherits="avii.Product.ProductLog" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="/Controls/Footer.ascx" %>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="/Controls/Header.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Product Log</title>
    
    <script type="text/javascript">

        function closeWindow() {

            alert('No valid data!')
            window.close();

        }
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
<body  bgColor="#ffffff" leftmargin="0" rightmargin="0" topmargin="0">
    <form id="form1" runat="server">

        <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
	<table cellSpacing="0" cellPadding="0"  border="0" align="center" width="95%">
		<tr>
			<td>
			<head:menuheader id="HeadAdmin" runat="server"></head:menuheader>
			</td>
		</tr>
     </table>
    <table  cellSpacing="1" cellPadding="1" width="95%" align="center" >
        <tr>
		    <td colSpan="6" bgcolor="#dee7f6" class="buttonlabel">&nbsp;&nbsp;Product Log
		    </td>
        </tr>
    </table> 
    <table cellSpacing="0" cellPadding="0" align="center" width="95%" border="0">
    	<tr>                    
            <td colspan="2">
                <asp:Label ID="lblMsg" runat="server" Width="100%" CssClass="errormessage"></asp:Label>
            </td>
        </tr>
    </table>
    <table  bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="95%" align="center">
    <tr  bordercolor="#839abf">
        <td>
        <table cellpadding="10" cellspacing="10" width="100%">
        <tr>
           <td class="copy10grey" align="right" width="17%">
               Category Name:
           </td>
           <td width="34%">
               <asp:Label ID="lblCategoryName" runat="server" CssClass="copy10grey"></asp:Label>
           </td>
            <td class="copy10grey" align="right" width="17%">
                Model Number:
           </td>
            <td width="32%" class="copy10grey" align="left">
                <asp:Label ID="lblModelNumber" runat="server" CssClass="copy10grey"></asp:Label>
           </td>
        </tr>
        <tr>
           <td class="copy10grey" align="right" width="17%">
               Product Name:
           </td>
           <td width="34%">
               
                <asp:Label ID="lblProductName" runat="server" CssClass="copy10grey"></asp:Label>
           </td>
            <td class="copy10grey" align="right" width="17%">

           </td>
            <td width="32%" class="copy10grey" align="right">
                
           </td>
        </tr>
        </table>
        </td>
    </tr>
    </table>
        <br />
    <table  bordercolor="#839abf" border="0" cellSpacing="0" cellPadding="0" width="95%" align="center">
    <tr  bordercolor="#839abf">
        <td>
        <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
           <td  align="center">
                                                 
            <asp:GridView ID="gvLog" runat="server" AutoGenerateColumns="false" Width="100%"  AllowPaging="false" PageSize="20"
                AllowSorting="false" >
                <RowStyle BackColor="Gainsboro" />
                <AlternatingRowStyle BackColor="white" />
                <HeaderStyle  CssClass="buttongrid" ForeColor="white"/>
                <RowStyle  CssClass="copy10grey" />
                <FooterStyle CssClass="white"  />
                <%--<PagerStyle ForeColor="#636363" CssClass="copy10grey" HorizontalAlign="Left" />--%>                
                <Columns>
                    <asp:TemplateField HeaderText="S.No." ItemStyle-CssClass="copy10grey"  ItemStyle-Width="1%" HeaderStyle-CssClass="buttongrid">
                    <ItemTemplate>
                            <%# Container.DataItemIndex + 1%>                  
                    </ItemTemplate>
                    </asp:TemplateField>
                    
                    <%-- 
                    <asp:TemplateField  ItemStyle-Width="8%" HeaderStyle-CssClass="buttongrid" HeaderStyle-HorizontalAlign="left" HeaderText="Category Name" 
                        ItemStyle-VerticalAlign="top">
                        <ItemTemplate>
                            <%# Eval("CategoryName") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField  ItemStyle-Width="15%"  HeaderStyle-CssClass="buttongrid" HeaderStyle-HorizontalAlign="left" HeaderText="Product Name" 
                        ItemStyle-VerticalAlign="top">
                        <ItemTemplate>
                            <%# Eval("ProductName") %>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                    <asp:TemplateField  ItemStyle-Width="6%"  HeaderStyle-CssClass="buttongrid" HeaderStyle-HorizontalAlign="left" HeaderText="Create Date" 
                        ItemStyle-VerticalAlign="top">
                        <ItemTemplate>
                            <%# Convert.ToDateTime(Eval("CreateDate")).ToString("MM/dd/yyyy") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField  ItemStyle-Width="10%" HeaderStyle-CssClass="buttongrid" HeaderStyle-HorizontalAlign="left" HeaderText="SKU" 
                        ItemStyle-VerticalAlign="top">
                        <ItemTemplate>
                            <%# Eval("SKU") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField  ItemStyle-Width="52%"  HeaderStyle-CssClass="buttongrid" HeaderStyle-HorizontalAlign="left" HeaderText="Request Data" 
                        ItemStyle-VerticalAlign="top">
                        <ItemTemplate>
                            <%# Eval("RequestData") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField  ItemStyle-Width="8%"  HeaderStyle-CssClass="buttongrid" HeaderStyle-HorizontalAlign="left" HeaderText="Response Data" 
                        ItemStyle-VerticalAlign="top">
                        <ItemTemplate>
                            <%# Eval("ResponseData") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField  ItemStyle-Width="3%" HeaderStyle-CssClass="buttongrid" HeaderStyle-HorizontalAlign="left" HeaderText="Status" 
                        ItemStyle-VerticalAlign="top">
                        <ItemTemplate>
                            <%# Eval("Status") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField  ItemStyle-Width="5%" HeaderStyle-CssClass="buttongrid" HeaderStyle-HorizontalAlign="left" HeaderText="User Name" 
                        ItemStyle-VerticalAlign="top">
                        <ItemTemplate>
                            <%# Eval("UserName") %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField ItemStyle-Width="4%" HeaderStyle-CssClass="buttongrid" HeaderStyle-HorizontalAlign="left" HeaderText="Action" 
                        ItemStyle-VerticalAlign="top">
                        <ItemTemplate>
                            <%# Eval("ActionName") %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    </Columns>
                    </asp:GridView>
                                        
                </td>
            </tr>
        </table>

        </td>
        </tr>
        </table>
        <table width="100%" align="center">
                <tr>
                    <td align="center">
                
                    <asp:Button ID="btnCancel" runat="server" Text="Close" CssClass="button" Visible="true" OnClientClick="return close_window();"  />
                </td>
            </tr>
        </table>


    </form>
</body>
</html>
 
