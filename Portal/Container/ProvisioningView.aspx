<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProvisioningView.aspx.cs" Inherits="avii.Container.ProvisioningView" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="~/Controls/Header.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
      <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>

    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.4.0/jquery.min.js"></script>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/jquery-ui.min.js"></script>
    <link href="https://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/themes/start/jquery-ui.css"
    rel="stylesheet" type="text/css" />
<%--<script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/jquery-ui.js" type="text/javascript"></script>
<link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/themes/start/jquery-ui.css"
    rel="stylesheet" type="text/css" />--%>
	<%--<script type="text/javascript" src="../JQuery/jquery.min.js"></script>
    <script type="text/javascript" src="../JQuery/jquery-ui.min.js"></script>--%>
	<script type="text/javascript" src="../JQuery/jquery.blockUI.js"></script>
    <!-- fix for 1.1em default font size in Jquery UI -->
	<style type="text/css">
		.ui-widget{font-size:11px !important;}
		.ui-state-error-text{margin-left: 10px}
	</style>
<script type="text/javascript">
    $(document).ready(function () {
        $("#divESN").dialog({
            autoOpen: false,
            modal: false,
            minHeight: 300,
            height: 350,
            width: 600,
            resizable: false,
            open: function (event, ui) {
                $(this).parent().appendTo("#divContainer");
            }
        });



    });


    function closeDialog() {
        //Could cause an infinite loop because of "on close handling"
        $("#divESN").dialog('close');
    }

    function openDialog(title, linkID) {
        var pos = $("#" + linkID).position();
        var top = pos.top;
        var left = pos.left + $("#" + linkID).width() + 10;
        //alert(top);
        //top = top - 300;
        if (top > 600)
            top = 10;
        top = 100;
        //alert(top);
        left = 300;
        $("#divESN").dialog("option", "title", title);
        $("#divESN").dialog("option", "position", [left, top]);
        $("#divESN").dialog('open');

    }


    function openDialogAndBlock(title, linkID) {

        openDialog(title, linkID);
        //alert('2')
        //block it to clean out the data
        $("#divESN").block({
            message: '<img src="../images/async.gif" />',
            css: { border: '0px' },
            fadeIn: 0,
            //fadeOut: 0,
            overlayCSS: { backgroundColor: '#ffffff', opacity: 1 }
        });
    }

    function unblockDialog() {
        $("#divESN").unblock();
    }


</script>
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
<body leftmargin="0" topmargin="0" marginwidth="0" marginheight="0">
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
            &nbsp;&nbsp;Provisioning - View
			</td>
		</tr>
    
    </table>
    <div id="divContainer">	
            
			<div id="divESN" style="display:none">
					
				<asp:UpdatePanel ID="upnlESN" runat="server">
					<ContentTemplate>
                   
						<asp:PlaceHolder ID="phrESN" runat="server">
                        <table width="100%" border="0">
                                
                                <tr>
                                    <td colspan="2">
                                
                                    <asp:Panel ID="pnlESN" runat="server" Width="100%">
                                        <asp:Label ID="lblEsnMsg" runat="server" Width="100%" CssClass="errormessage"></asp:Label>
                                          <asp:Repeater ID="rptEsnLog" runat="server" Visible="true" >
                                            <HeaderTemplate>
                                            <table bordercolor="#839abf" border="1" width="100%" cellpadding="1" cellspacing="1">
                                            <tr>
                                                <td class="buttongrid"  width="24%" >
                                                   ESN
                                                </td>
                                                <td class="buttongrid"  width="22%">
                                                   Update Date
                                                </td>
                                                <td class="buttongrid"  width="22%">
                                                   Status
                                                </td>
                                                <td class="buttongrid"  width="22%">
                                                   Module
                                                </td>
                                            </tr>
                                            </HeaderTemplate>
                                            <ItemTemplate>
    
                                                <tr valign="bottom"  class="<%# Container.ItemIndex % 2 == 0 ? "alternaterow" : "" %>">
                                                    <td class="copy10grey"  >
                                                        <%# Eval("ESN")%>
                                                    <%--<%# Container.ItemIndex +  1 %>--%>
                                                    </td>
                                                    <td valign="bottom" class="copy10grey"  >
                                                    <span width="100%">
                                                        <%# Convert.ToDateTime(Eval("UpdateDate")).ToString("MM/dd/yyyy") %>  
                                                        </span>
                                                    </td>
                                                    <td valign="bottom" class="copy10grey"  >
                                                    <span width="100%">
                                                        <%# Eval("Status")%>  
                                                        </span>
                                                    </td>
                                                    <td valign="bottom" class="copy10grey"  >
                                                    <span width="100%">
                                                        <%# Eval("ModuleName")%>  
                                                        </span>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                            </table>
                                            </FooterTemplate>
                                            </asp:Repeater>
                               
                                    
                            </asp:Panel>
                            </td>
                                </tr>
                                </table>
                                
                        </asp:PlaceHolder>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>


    <table cellSpacing="0" cellPadding="0" align="center" width="95%" border="0">
        <tr>
			<td>
    <asp:UpdatePanel ID="upnlCustomers" UpdateMode="Conditional" runat="server">
	<ContentTemplate>
	
    <table cellSpacing="0" cellPadding="0" align="center" width="100%" border="0">
    		<tr>                    
                <td >
                    <asp:Label ID="lblMsg" runat="server" Width="100%" CssClass="errormessage"></asp:Label></td>
            </tr> 
              
     </table> 
        
        
      <table bordercolor="#839abf" border="1" width="100%" align="center" cellpadding="0" cellspacing="0">
      <tr>
        <td>
        
     <table width="100%" border="0" class="box" align="center" cellpadding="2" cellspacing="2">
     <%--<tr style="height:1px">
     <td style="height:1px"></td>
     </tr>
     --%>
            <tr>
                <td  class="copy10grey" align="right" width="25%">
                        <strong>Fulfillment#:</strong> &nbsp;</td>
                <td align="left"  width="25%">
                        <asp:Label ID="lblPONum"  CssClass="copy10grey" runat="server"  ></asp:Label>
                
                </td>
                <td  width="1%">
                    &nbsp;
                </td>
                <td class="copy10grey"  align="right" width="25%">
                   <strong>Fulfillment Status:</strong> &nbsp;
                </td>
                <td width="25%">
                   <asp:Label ID="lblPoStatus"  CssClass="copy10grey" runat="server"  ></asp:Label>
                </td>
            </tr>
             <tr >
                <td class="copy10grey" align="right" width="25%">
                   <strong> Fulfillment Date:</strong> &nbsp;
                </td>
                <td width="25%">
                    <asp:Label ID="lblPoDate"  CssClass="copy10grey" runat="server"  ></asp:Label>                  
         
                </td>
                <td  width="1%">
                    &nbsp;
                </td>
                <td class="copy10grey" align="right" width="25%">
                    <strong>Shipping Date:</strong> &nbsp;
                </td>
                <td width="25%">
                   
                 <asp:Label ID="lblShipDate"  CssClass="copy10grey" runat="server"  ></asp:Label>                               
        
                </td>   
                
                    
                </tr>
                
                
               
            </table>
   
     </td>
     </tr>
     </table>  
          <br />
        <table align="center" style="text-align:left" width="100%">
        <%--<tr>
            
            <td colspan="3"  align="right" style="height:8px; vertical-align:bottom">
                        
            </td>
        </tr>--%>
        <tr>
            <td colspan="3" align="left">
                <asp:Label ID="lblsku" runat="server" Width="100%" CssClass="buttonlabel">Line items</asp:Label>

                                <asp:Repeater ID="rptSKU" runat="server" Visible="true" >
                                <HeaderTemplate>
                                <table bordercolor="#839abf" border="1" width="100%" cellpadding="1" cellspacing="1">
                                <tr>
                                    <td class="buttongrid"  width="2%" >
                                        S.No.
                                    </td>
                                    <td class="buttongrid"  width="10%">
                                        Category Name
                                    </td>
                                    <td class="buttongrid"  width="25%">
                                        Product Name
                                    </td>
                                    <td class="buttongrid" width="23%">
                                        SKU
                                    </td>
                                    <td class="buttongrid" width="15%">
                                        Qty 
                                    </td>
                                    
                                    
                                </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
    
                                    <tr valign="bottom"  class="<%# Container.ItemIndex % 2 == 0 ? "alternaterow" : "" %>">
                                        <td class="copy10grey"  >
                                        <%# Container.ItemIndex +  1 %>
                                        </td>
                                        <td valign="bottom" class="copy10grey"  >
                                        <span width="100%">
                                            <%# Eval("CategoryName")%>    
                                            </span>
                                        </td>
                                        <td valign="bottom" class="copy10grey"  >
                                        <span width="100%">
                                            <%# Eval("ProductName")%>    
                                            </span>
                                        </td>
                                        <td valign="bottom" class="copy10grey"  >
                                        <span width="100%">
                                            <%# Eval("SKU")%>    
                                            </span>
                                        </td>
                                         <td valign="bottom" class="copy10grey"  >
                                        <span width="100%">
                                            <%# Eval("Quantity")%>    
                                            </span>
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
        <br />
        <table align="center" style="text-align:left" width="100%">
      
        <tr>
            <td  align="left">
                  <asp:Label ID="lblPO" runat="server" Width="100%" CssClass="buttonlabel">ESN Assignment</asp:Label>

            <asp:GridView ID="gvESNs" runat="server" AutoGenerateColumns="false"  OnRowDataBound="gvESNs_RowDataBound"
                  Width="100%" GridLines="Both" OnPageIndexChanging="gvESNs_PageIndexChanging" AllowPaging="true" PageSize="20"
                AllowSorting="true" OnSorting="gvESNs_Sorting">
                <RowStyle BackColor="Gainsboro" />
                <AlternatingRowStyle BackColor="white" />
                <HeaderStyle  CssClass="buttongrid" ForeColor="white"/>
                <FooterStyle CssClass="white"  />
                <PagerStyle ForeColor="#636363" CssClass="copy10grey" HorizontalAlign="Left" />
                <Columns>
                     
                    <asp:TemplateField HeaderText="S.No." ItemStyle-CssClass="copy10grey"  ItemStyle-Width="1%" HeaderStyle-CssClass="buttongrid">
                    <ItemTemplate>

                            <%# Container.DataItemIndex + 1%>
                  
                    </ItemTemplate>
                </asp:TemplateField> 
                    <asp:TemplateField HeaderText="ContainerID" HeaderStyle-CssClass="buttonundlinelabel" SortExpression="ContainerID" ItemStyle-HorizontalAlign="Left" 
                        ItemStyle-CssClass="copy10grey" ItemStyle-Width="20%">
                        <ItemTemplate><%#Eval("ContainerID")%></ItemTemplate>
                    </asp:TemplateField>
                    
                    
                    <asp:TemplateField HeaderText="ESN" HeaderStyle-CssClass="buttonundlinelabel" SortExpression="ESN" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="15%">
                        <ItemTemplate><%#Eval("ESN")%></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="HEX" HeaderStyle-CssClass="buttonundlinelabel" SortExpression="HEX"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="15%">
                        <ItemTemplate><%# Eval("HEX") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="DEC" HeaderStyle-CssClass="buttonundlinelabel" SortExpression="DEC"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="15%">
                        <ItemTemplate><%# Eval("DEC") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="LOCATION" HeaderStyle-CssClass="buttonundlinelabel" SortExpression="Location"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="15%">
                        <ItemTemplate><%# Eval("LOCATION") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="BOXID" HeaderStyle-CssClass="buttonundlinelabel" SortExpression="BoxID"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="15%">
                        <ItemTemplate><%# Eval("BOXID") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Action" HeaderStyle-CssClass="buttongrid" ItemStyle-HorizontalAlign="Center" 
                        ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
                        <ItemTemplate>
                            <asp:ImageButton ID="imgESN"  ToolTip="View ESN log" OnCommand="imgESN_Command"  CausesValidation="false" 
                            CommandArgument='<%# Eval("ESN") %>' ImageUrl="~/Images/view.png"  runat="server" />
                        

                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    
                </Columns>
            </asp:GridView>
  
                </td>
            </tr>
            
            </table>
        <br />
        <table width="100%" align="center"  >            
        <tr>
            <td  align="center" >
            <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="button" Visible="true" OnClientClick="return close_window();"  />
                   
            </td>
        </tr>
        </table>
        </ContentTemplate>
        <%--<Triggers>
            <asp:PostBackTrigger ControlID="btnSearch" />
            <asp:PostBackTrigger ControlID="btnCancel" />
            
        </Triggers>--%>
        </asp:UpdatePanel>
	
        <asp:UpdatePanel ID="upnlJsRunner" UpdateMode="Always" runat="server">
			<ContentTemplate>
				<asp:PlaceHolder ID="phrJsRunner" runat="server"></asp:PlaceHolder>
			</ContentTemplate>
		</asp:UpdatePanel>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server"
                     DynamicLayout="false">
            <ProgressTemplate>
                <img src="/Images/ajax-loaders.gif" /> Loading ...
            </ProgressTemplate>
        </asp:UpdateProgress>
                </td>
    </tr>

    </table>
        <br /> <br />
            <br /> <br />
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
