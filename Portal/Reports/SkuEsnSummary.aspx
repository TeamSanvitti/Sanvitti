<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SkuEsnSummary.aspx.cs" Inherits="avii.Reports.SkuEsnSummary" %>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="/Controls/Header.ascx" %>
<%@ Register TagPrefix="UC" TagName="ESN" Src="/Controls/ViewESN.ascx" %>
<%@ Register TagPrefix="UC" TagName="Product" Src="/Controls/ProductsDetail.ascx" %>
<%@ Register TagPrefix="UC" TagName="SKU" Src="/Controls/ProductSKUs.ascx" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SKU ESN Summary</title>

    <link href="../aerostyle.css" rel="stylesheet" type="text/css"/>
    
    <!-- from http://encosia.com/2009/10/11/do-you-know-about-this-undocumented-google-cdn-feature/ -->
    <link href="https://ajax.googleapis.com/ajax/libs/jqueryui/1.8.11/themes/start/jquery-ui.css" type="text/css" rel="Stylesheet" />
	
	<%--<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.4.0/jquery.min.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/jquery-ui.min.js"></script>
--%>

    <script type="text/javascript" src="../JQuery/jquery.min.js"></script>
    <script type="text/javascript" src="../JQuery/jquery-ui.min.js"></script>
	
	<script type="text/javascript" src="../JQuery/jquery.blockUI.js"></script>
	
	
<!-- fix for 1.1em default font size in Jquery UI -->
	<style type="text/css">
		.ui-widget{font-size:11px !important;}
		.ui-state-error-text{margin-left: 10px}
	</style>
    
    <script type="text/javascript" src="../JQuery/jquery.easytabs.js"></script>
    <script type="text/javascript" src="../JQuery/jquery.easytabs.min.js"></script>
	<script type="text/javascript">
	    $(document).ready(function () {
	        $("#divESN").dialog({
	            autoOpen: false,
	            modal: true,
	            minHeight: 400,
	            height: 640,
	            width: 400,
	            resizable: false,
	            open: function (event, ui) {
	                $(this).parent().appendTo("#divContainer");
	            }
	        });

	        $("#divProduct").dialog({
	            autoOpen: false,
	            modal: true,
	            minHeight: 400,
	            height: 640,
	            width: 950,
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
	        left = 400;
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


	    function closeProductDialog() {
	        //Could cause an infinite loop because of "on close handling"
	        $("#divProduct").dialog('close');
	    }

	    function openProductDialog(title, linkID) {

	        var pos = $("#" + linkID).position();
	        var top = pos.top;
	        var left = pos.left + $("#" + linkID).width() + 10;
	        //alert(top);
	        //top = top - 300;
	        if (top > 600)
	            top = 10;
	        top = 100;
	        //alert(top);
	        left = 200;
	        $("#divProduct").dialog("option", "title", title);
	        $("#divProduct").dialog("option", "position", [left, top]);

	        $("#divProduct").dialog('open');

	    }


	    function openProductDialogAndBlock(title, linkID) {

	        openProductDialog(title, linkID);
	        //alert('2')
	        //block it to clean out the data
	        $("#divProduct").block({
	            message: '<img src="../images/async.gif" />',
	            css: { border: '0px' },
	            fadeIn: 0,
	            //fadeOut: 0,
	            overlayCSS: { backgroundColor: '#ffffff', opacity: 1 }
	        });
	    }


	    function unblockProductDialog() {
	        $("#divProduct").unblock();
	    }


	    $('#tab-container').easytabs();
	</script>

	
    <link href="../css/jquerytabs.css" rel="stylesheet" type="text/css" />
    <%--<link href="/Product/ddcolortabs.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript" src="/Product/ddtabmenu.js"></script>
    --%>
	
</head>
<body leftmargin="0" topmargin="0" marginwidth="0" marginheight="0"> 
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    
    <table cellSpacing="0" cellPadding="0" align="center" width="100%" border="0">
        <tr>
			<td><head:MenuHeader id="HeadAdmin" runat="server"></head:MenuHeader></td>
		</tr>
		<tr><td>&nbsp;</td></tr>
		<tr>
			<td  bgcolor="#dee7f6" class="button">&nbsp;&nbsp;SKU ESN Summary
			</td>
		</tr>
    <tr><td>&nbsp;</td></tr>
    </table>
	<div id="divContainer">	
			<div id="divESN" style="display:none">
					
				<asp:UpdatePanel ID="upnlESN" runat="server">
					<ContentTemplate>
                   
						<asp:PlaceHolder ID="phrESN" runat="server">
                        <table width="100%" border="0">
                                <tr>
                                    <td class="copy10grey" width="30%" align="left">
                                   <b> &nbsp;Product Code:</b>
                                    </td>
                                    <td align="left" >
                                     <asp:Label ID="lblProduct" runat="server" CssClass="copy10grey"></asp:Label>               
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                
                                    <asp:Panel ID="pnlESN" runat="server" Width="100%">


                                
                                        <UC:ESN ID="esn1" runat="server" />
                                    
                            </asp:Panel>
                            </td>
                                </tr>
                                </table>
                                
                        </asp:PlaceHolder>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div id="divProduct" style="display:none">
					
				<asp:UpdatePanel ID="upProduct" runat="server">
					<ContentTemplate>
                   
						<asp:PlaceHolder ID="phProduct" runat="server">
                                
                            <asp:Panel ID="pnlProduct" runat="server" Width="100%">
        
                            </asp:Panel>
                            
                            <table width="100%">
                            
                            <tr>
                            <td>
                                <div id="tab-container" class="tab-container">
  <ul class='etabs'>
    <li class='tab'><a href="#tabs1" rel="tabs1">SKU</a></li><%--
    <li class='tab'><a href="Div1" rel="Div1">Required JS</a></li>
    <li class='tab'><a href="Div2" rel="Div2">Example CSS</a></li>--%>
  </ul>
  <div id="#tabs1">
    
    <asp:Panel ID="tabPanel" runat="server">
    <UC:SKU ID="SKU1" runat="server" />
    <!-- content -->
    </asp:Panel>
  </div>
  <%--<div id="Div1">
  2nd 
  </div>
  <div id="Div2">
  3rd 
  </div>--%>
</div>
    

                            </td>
                            </tr>
                            <%--<tr >
                
                            <td id="tabTD" width="784" >


                            
                                
                                <div id="ddtabs4" class="ddcolortabs" align="center" width="100%" >
                                <ul>
                                <li><a href="#" rel="pnlSKU"><span>SKU#</span></a></li>
                                <li><a href="#" rel="ct3"><span>Pricing</span></a></li>
                                </ul>
                                </div>
                                <div class="ddcolortabsline" width="100%">&nbsp;</div>
                                <div id="allTabContnt" width="100%">
                                <div id="ct6" class="tabcontent" >
                                    
                                    
                                    
                                </div>

                                <div id="ct3" class="tabcontent" style="border:1px solid #666666">
                                testing....
                                </div>

                                </div>
                 
                 
                            </td>

                            </tr>            --%>
                            </table>
                        </asp:PlaceHolder>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
     </div>
      <asp:UpdatePanel ID="upnlCustomers" UpdateMode="Conditional" runat="server">
			<ContentTemplate>
	
    <table cellSpacing="0" cellPadding="0" align="center" width="100%" border="0">
    		<tr>                    
                <td colspan="2">
                    <asp:Label ID="lblMsg" runat="server" Width="100%" CssClass="errormessage"></asp:Label></td>
            </tr>               
    
                             <tr>
                                <td colspan="2" align="right" style="height:8px; vertical-align:bottom">
                                <strong>   
                                    <asp:Label ID="lblCount" CssClass="copy10grey" runat="server" ></asp:Label> &nbsp;&nbsp;
                                </strong> 
                                </td>
                             </tr>
                             <tr>
                                <td colspan="2" align="center">
                                    <asp:GridView ID="gvESN" runat="server" Width="100%" GridLines="Both"   AutoGenerateColumns="false"
                                    OnPageIndexChanging="gridView_PageIndexChanging" PageSize="50" AllowPaging="true" 
                                    OnRowDataBound="gvESN_RowDataBound" >
                                        <RowStyle BackColor="Gainsboro" />
                                        <AlternatingRowStyle BackColor="white" />
                                        <HeaderStyle  CssClass="button" ForeColor="white"/>
                                        <FooterStyle CssClass="white"  />
                                        <PagerStyle ForeColor="#636363" CssClass="copy10grey" HorizontalAlign="Left" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No." ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                <%# Container.DataItemIndex +  1 %>
                                                
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Product Code" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                <%--<%#Eval("ProductCode")%>--%>
                                                
                                                <asp:LinkButton ToolTip="View Product info" 
                                                    CausesValidation="false" OnCommand="lnkViewProductInfo_Click" CommandArgument='<%# Eval("ItemGUID") %>'  
                                                    ID="lnkCode"  runat="server" Text='<%#Eval("ProductCode")%>' />
                 
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Product Name" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                                <ItemTemplate><%#Eval("ItemName")%></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Category Name" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                                <ItemTemplate><%#Eval("CategoryName")%></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Carrier Name" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                                <ItemTemplate><%#Eval("CarrierName")%></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Maker Name" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                                <ItemTemplate><%#Eval("MakerName")%></ItemTemplate>
                                            </asp:TemplateField>
                            
                                             <asp:TemplateField HeaderText="Total ESN" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="7%">
                                                <ItemTemplate><%#Eval("TotalESN")%></ItemTemplate>
                                            </asp:TemplateField> 
                                            <asp:TemplateField HeaderText="Used ESN" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="8%">
                                                <ItemTemplate>
                                                <%#Eval("UsedESN")%>
                                                </ItemTemplate>
                                            </asp:TemplateField> 
                                            <asp:TemplateField HeaderText="Unused ESN"   ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="8%">
                                                <ItemTemplate>
                                                    <asp:LinkButton Visible='<%# Convert.ToInt32(Eval("ItemGUID")) ==0 ? false : true %>' ToolTip="View ESN" 
                                                    CausesValidation="false" OnCommand="imgViewESN_Click" CommandArgument='<%# Eval("ItemGUID") %>'  ID="lnkESN"  runat="server" Text='<%#Eval("UnusedESN")%>' />
                 
                                                </ItemTemplate>
                                            </asp:TemplateField> 
                                            <asp:TemplateField HeaderText="RMA" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="8%">
                                                <ItemTemplate>
                                                <%#Eval("RmaESN")%>
                                                </ItemTemplate>
                                            </asp:TemplateField> 
                                            <%--<asp:TemplateField HeaderText="AVPO" SortExpression="AVPO"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="7%">
                                                <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "AVPO")%>
                                                <%#Eval("Item_code")%>
                                                </ItemTemplate>
                                            </asp:TemplateField> 
                            --%>
                                            
                                            

                            
                                        </Columns>
                                    </asp:GridView>
                                    

                                </td>
                            </tr>
                            </table>
            </ContentTemplate>
        </asp:UpdatePanel>
		
            <asp:UpdatePanel ID="upnlJsRunner" UpdateMode="Always" runat="server">
			<ContentTemplate>
				<asp:PlaceHolder ID="phrJsRunner" runat="server"></asp:PlaceHolder>
			</ContentTemplate>
		</asp:UpdatePanel>
 
        <script type="text/javascript">
            //SYNTAX: ddtabmenu.definemenu("tab_menu_id", integer OR "auto")
            //var tabIndex = 0;
            
            //ddtabmenu.definemenu("ddtabs4", tabIndex) //initialize Tab Menu #4 with 3rd tab selected
            //var tabpanel = document.getElementById('tabTD');
            //tabpanel.style.display = 'block';
            
    
    
    </script>
    
                            
    </form>
</body>
</html>
