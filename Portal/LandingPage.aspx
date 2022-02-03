<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LandingPage.aspx.cs" Inherits="avii.LandingPage" %>
<%--<%@ Register TagPrefix="RMA" TagName="RMAStatus" Src="~/Controls/RMAStatus.ascx" %>
<%@ Register TagPrefix="PO" TagName="POStatus" Src="~/Controls/POStatus.ascx" %>
<%@ Register TagPrefix="SKU" TagName="SKUAssigned" Src="~/Controls/AssignedSKUs.ascx" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="~/Controls/Header.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>--%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <%--<link href="/aerostyle.css" type="text/css" rel="stylesheet"/>
<script type="text/javascript" language="javascript">
        function ValidateSKU() {
            var sku = document.getElementById("<% =txtSearch.ClientID %>");
            if (sku.value == '') {
                alert('SKU can not be empty');
                return false;
            }
        }
    </script>--%>



</head>
<body bgcolor="#ffffff"  leftmargin="0" topmargin="0" marginwidth="0" marginheight="0" >
    <form id="form1" runat="server">
   <%-- <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    

     <table cellspacing="0" cellpadding="0" border="0"  width="100%" align="center">
        <tr>
            <td>
                <head:MenuHeader ID="HeadAdmin1" runat="server" ></head:MenuHeader>
            </td>
        </tr> 
        </table> 
    <br />

    <table  cellSpacing="1" cellPadding="1" width="100%" align="center">
        <tr>
			<td colSpan="6" bgcolor="#dee7f6" class="button" align="left">&nbsp;&nbsp;Dashboard
			</td>
        </tr>
        
    </table>
    <asp:UpdatePanel ID="UpdatePanel1"  runat="server" ChildrenAsTriggers="true"  >
     <Triggers>
     <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
     </Triggers>
     <ContentTemplate>
     
     <table width="100%"   border="0" cellpadding="1" cellspacing="1" >
    <tr valign="middle">
        <td  align="left"  class="copy10grey"  width="60%"   >
        <asp:Panel ID="pnlCust" runat="server" >
        <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td>
            Company Name:  &nbsp;&nbsp;  <asp:DropDownList ID="dpCompany" runat="server" AutoPostBack="false" 
        OnSelectedIndexChanged="dpCompany_SelectedIndexChanged" class="copy10grey"  >
                                
                            </asp:DropDownList>    
            </td>
            <td width="20%">
            <asp:Button ID="btnRefresh1" runat="server" Text="Refresh" Visible="false" CssClass="button" OnClick="btnRefresh_Click" />
            </td>
            
        </tr>
        </table></asp:Panel>
         <table  cellpadding="0" cellspacing="0">
        <tr>
            
        </tr>
        </table>
         
            
        </td>
        <td align="right" class="copy10grey" >
         Short By: &nbsp;&nbsp;   <asp:DropDownList ID="ddlDuration" AutoPostBack="false"  runat="server" Class="copy10grey" 
                                                Width="135px" OnSelectedIndexChanged="ddlDuration_SelectedIndexChanged">
                                                        <asp:ListItem  Value="30" Selected="True" >Last Month</asp:ListItem>
                                                        <asp:ListItem Value="90" >Quaterly</asp:ListItem>
                                                        <asp:ListItem  Value="180">6 Months</asp:ListItem>
                                                        <asp:ListItem  Value="365">One Year</asp:ListItem>
                                                        
                                            </asp:DropDownList>
        </td>
        <td align="right">
            <asp:Button ID="btnRefresh" runat="server" Text="Refresh" CssClass="button" OnClick="btnRefresh_Click" />&nbsp;
        <td>
    </tr>
    </table>
    <table width="100%"   border="0" cellpadding="5" cellspacing="5" >
    <tr valign="top" height="400">
        <td width="33%">
            <table width="100%" border="0">
            <tr>
                <td class="button">
                RMA Summary
                </td>
            </tr>
            <tr>
                <td>
                <table bordercolor="#839abf" border="1" cellspacing="0" cellpadding="0" width="100%" align="center">
                <tr bordercolor="#839abf">
                    <td>
                        
                        <asp:Panel ID="pnlRMA" runat="server" >
            
                        <RMA:RMAStatus ID="RMAStatus1" runat="server" />
                        </asp:Panel>
                    </td>
                </tr>
                </table>
<br/>
 <table width="100%" border="0">
            <tr>
                <td class="button">
                 Sign-in Summary
                </td>
            </tr>
            <tr>
                <td>
                <table bordercolor="#839abf" border="1" cellspacing="0" cellpadding="0" width="100%" align="center">
                <tr bordercolor="#839abf">
                    <td>
                        
                        <asp:Panel ID="pnlUser" runat="server" >
            
                        
                        </asp:Panel>
                    </td>
                </tr>
                </table>
            </td>
            </tr>
            </table>
            </td>
            </tr>
            </table>
            
        </td>
        <td width="33%">
             <table width="100%">
            <tr>
                <td class="button">
                Fulfillment Summary
                </td>
            </tr>
            <tr>
                <td>
                <table bordercolor="#839abf" border="1" cellspacing="0" cellpadding="0" width="100%" align="center">
                <tr bordercolor="#839abf">
                    <td>
                        
                    <asp:Panel ID="pnlPO" runat="server">
            
                    <PO:POStatus ID="POStatus1" runat="server" />
                    </asp:Panel>
                    </td>
                </tr>
                </table>
            </td>
            </tr>
            </table>

<br />

            <table width="100%" border="0">
            <tr>
                <td class="button">
                Fulfillment Orders Shipment Summary
                </td>
            </tr>
            <tr>
                <td>
                <table bordercolor="#839abf" border="1" cellspacing="0" cellpadding="0" width="100%" align="center">
                <tr bordercolor="#839abf">
                    <td>
                        
                        <asp:Panel ID="pnlShipby" runat="server" >
            <asp:Label ID="lblShipBy" runat="server" CssClass="errormessage"></asp:Label>
                         
                            <asp:GridView ID="gvShipby" OnPageIndexChanging="gvShipby_PageIndexChanging"    AutoGenerateColumns="false"  
                            Width="100%" ShowHeader="false"  ShowFooter="false" runat="server" GridLines="Both" 
                            PageSize="40" AllowPaging="true" AllowSorting="false" 
                            >
                            <RowStyle BackColor="Gainsboro" />
                            <AlternatingRowStyle BackColor="white" />
                            <HeaderStyle  CssClass="button" ForeColor="white"/>
                              <PagerStyle ForeColor="#636363" CssClass="copy10grey" HorizontalAlign="Left" />
                              <Columns>
                                                     
                
                                    <asp:TemplateField  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" >
                                        <ItemTemplate>
                                           
                                        <%# Eval("ShipByText")%>   
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField ItemStyle-CssClass="copy10grey"  ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                             <%# Eval("ShipByCount")%> 
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>
                        </asp:Panel>
                    </td>
                </tr>
                </table>
            </td>
            </tr>
            </table>
        </td>
        <td>
               <table width="100%">
            <tr>
                <td class="button">
Fulfillment Order Summary
                </td>
            </tr>
            <tr>
                <td>
                <table bordercolor="#839abf" border="1" cellspacing="0" cellpadding="0" width="100%" align="center">
                <tr bordercolor="#839abf">
                    <td >
                    <table cellspacing="3" cellpadding="3" width="100%" border="0" >
                    <tr valign="middle">
                        <td class="copy10grey" valign="middle" width="10%" >
                            SKU:  &nbsp;
                            </td>
                        <td class="copy10grey" valign="middle" width="55%" >
                            <asp:TextBox ID="txtSearch" Width="99%" CssClass="copy10grey" runat="server"></asp:TextBox>  &nbsp;  &nbsp;
                            </td>
                    <td class="copy10grey" valign="middle" width="40%" align="right" >
                    <asp:Button ID="btnSearch"  runat="server" Text="Search" CssClass="button" OnClick="btnSearch_Click"/>
                        </td>
                    </tr>
                    </table>
                    <table cellspacing="1" cellpadding="1" width="100%" border="0" >
                    <tr>
                        <td colspan="3">
                            <asp:Panel ID="pnlSKU" runat="server">
                    
                            <SKU:SKUAssigned ID="SKUAssigned1" runat="server" />
                            </asp:Panel>
                        </td>
                    </tr>
                    </table>
                    
                    </td>
                </tr>
                </table>
                </td>
            </tr>
            </table>  
        </td>
    </tr>
    </table>
     </ContentTemplate>
        
    </asp:UpdatePanel>
     <asp:UpdateProgress ID="UpdateProgress1" runat="server"
                     DynamicLayout="false">
            <ProgressTemplate>
                <img src="/Images/ajax-loaders.gif" /> Loading ...
            </ProgressTemplate>
        </asp:UpdateProgress>
    <table cellspacing="0" cellpadding="0" border="0"  width="100%" align="center">
        
    <tr>
        <td><foot:MenuFooter id="Footer" runat="server"></foot:MenuFooter></td>
    </tr>
    </table>  --%>  
    </form>
</body>
</html>
