<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FulfillmentUpload.aspx.cs" Inherits="avii.Admin.FulfillmentUpload" %>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="/Controls/Header.ascx" %>
<%--<%@ Register TagPrefix="Po" TagName="ShipVia" Src="~/Controls/PoShipVia.ascx" %>
<%@ Register TagPrefix="WC" TagName="State" Src="/Controls/States.ascx" %>--%>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>.:: Lan Global inc. ::.</title>
    <link href="../aerostyle.css" rel="stylesheet" type="text/css"/>

    <!-- from http://encosia.com/2009/10/11/do-you-know-about-this-undocumented-google-cdn-feature/ -->
    <link href="https://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/themes/start/jquery-ui.css" type="text/css" rel="Stylesheet" />
	<%--
	<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.4.0/jquery.min.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/jquery-ui.min.js"></script>--%>
    
	<script type="text/javascript" src="../JQuery/jquery.min.js"></script>
    <script type="text/javascript" src="../JQuery/jquery-ui.min.js"></script>
	<script type="text/javascript" src="../JQuery/jquery.blockUI.js"></script
	
<!-- fix for 1.1em default font size in Jquery UI -->
	<style type="text/css">
		.ui-widget{font-size:11px !important;}
		.ui-state-error-text{margin-left: 10px}
	</style>
    
	<script type="text/javascript">

	    $(document).ready(function () {


	        $("#divFulfillmentView").dialog({
	            autoOpen: false,
	            modal: true,
	            minHeight: 20,
	            height: 440,
	            width: 700,
	            resizable: false,
	            open: function (event, ui) {
	                $(this).parent().appendTo("#divFulfillmentContainer");
	            }
	        });
            $("#divShipVia").dialog({
				autoOpen: false,
				modal: true,
				minHeight: 20,
				height: 500,
				width: 600,
				resizable: false,
				open: function(event, ui) {
					$(this).parent().appendTo("#divFulfillmentContainer");
				},
			});
            $("#divState").dialog({
	            autoOpen: false,
	            modal: true,
	            minHeight: 20,
	            height: 500,
	            width: 450,
	            resizable: false,
	            open: function (event, ui) {
	                $(this).parent().appendTo("#divFulfillmentContainer");
	            }
	        });

	    });


        
	    function closedStateDialog() {
	        //Could cause an infinite loop because of "on close handling"
	        $("#divState").dialog('close');
	    }
	    function openStateDialog(title, linkID) {

	        var pos = $("#" + linkID).position();
	        var top = pos.top;
	        var left = pos.left + $("#" + linkID).width() + 10;
	        //alert(left);
	        top = top - 300;
	        left = 350;
	        $("#divState").dialog("option", "title", title);
	        $("#divState").dialog("option", "position", [left, top]);

	        $("#divState").dialog('open');
	    }
	    function openStateDialogAndBlock(title, linkID) {
	        openStateDialog(title, linkID);

	        //block it to clean out the data
	        $("#divState").block({
	            message: '<img src="../images/async.gif" />',
	            css: { border: '0px' },
	            fadeIn: 0,
	            //fadeOut: 0,
	            overlayCSS: { backgroundColor: '#ffffff', opacity: 1 }
	        });
	    }
	    function unblockStateDialog() {
	        $("#divState").unblock();
	    }

        function closeShipViaDialog() {
			//Could cause an infinite loop because of "on close handling"
			$("#divShipVia").dialog('close');
		}
        function openShipViaDialog(title, linkID) {
		
			var pos = $("#" + linkID).position();
			var top = pos.top;
			var left = pos.left + $("#" + linkID).width() + 10;
			//alert(left);
            top = top - 300;
			left = 300;
			$("#divShipVia").dialog("option", "title", title);
			$("#divShipVia").dialog("option", "position", [left, top]);
			
			$("#divShipVia").dialog('open');
		}
        function openShipViaDialogAndBlock(title, linkID) {
			openShipViaDialog(title, linkID);

			//block it to clean out the data
			$("#divShipVia").block({
				message: '<img src="../images/async.gif" />',
				css: { border: '0px' },
				fadeIn: 0,
				//fadeOut: 0,
				overlayCSS: { backgroundColor: '#ffffff', opacity: 1 } 
			});
		}
        function unblockShipViaDialog() {
			$("#divShipVia").unblock();
		}
	    function closeDialog() {
	        //Could cause an infinite loop because of "on close handling"
	        $("#divFulfillmentView").dialog('close');
	    }
	    function openDialog(title, linkID) {

	        var pos = $("#" + linkID).position();
	        var top = pos.top;
	        var left = pos.left + $("#" + linkID).width() + 10;
	        //alert(top);
	        if (top > 500)
	            top = 10;
	        //top = top - 600;
	        left = 300;
	        $("#divFulfillmentView").dialog("option", "title", title);
	        $("#divFulfillmentView").dialog("option", "position", [left, top]);

	        $("#divFulfillmentView").dialog('open');

	    }
	    function openDialogAndBlock(title, linkID) {
	        openDialog(title, linkID);

	        //block it to clean out the data
	        $("#divFulfillmentView").block({
	            message: '<img src="../images/async.gif" />',
	            css: { border: '0px' },
	            fadeIn: 0,
	            //fadeOut: 0,
	            overlayCSS: { backgroundColor: '#ffffff', opacity: 1 }
	        });
	    }

	    function unblockDialog() {
	        $("#divFulfillmentView").unblock();
	    }

	    </script>
    <script language="javascript" type="text/javascript">



        function KeyDownHandler(btn) {

            if (event.keyCode == 13) {
                event.returnValue = false;
                event.cancel = true;
                btn.click();
            }
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
			<tr><td>&nbsp;</td></tr>
			<tr>
				<td  bgcolor="#dee7f6" class="button">&nbsp;&nbsp;Fulfillment Orders Upload
				</td>
			</tr>
            <tr><td>&nbsp;</td></tr>
            </table>
	
    <asp:UpdatePanel ID="UpdatePanel1"  runat="server" UpdateMode="Conditional" >
            <ContentTemplate>
            <table cellSpacing="0" cellPadding="0" align="center" width="100%" border="0">
    		<tr>                    
                <td colspan="2">
                    <asp:Label ID="lblMsg" runat="server" Width="100%" CssClass="errormessage"></asp:Label></td>
            </tr>               
            
            <tr>
                <td align="center">

                    <table  cellSpacing="1" cellPadding="1" width="100%">
                        <tr><td class="copy10grey" align="left">
                        
	                    - Upload file should be less than 3 MB. <br />
                        <%--- Fulfillment order date should not be 90 days before or 30 days after from today date.<br />
                        - Click on "Ship Via" link to get the list of Ship Via codes.<br />
                        - Click on "State" link to get the list of State codes.<br />
                        - Use backslash(\) with comma(\,) to add , in the text.--%>
                        <br />&nbsp;
                        </td></tr>
                    </table>

                 <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="70%">
                    <tr>
                    <td>
			            <table id="Table2" cellSpacing="5" cellPadding="5" width="100%" border="0">
                            <tr id="trCustomer" runat="server">
                                <td  class="copy10grey" align="right" >
                                    Customer: &nbsp;</td>
                                <td align="left" >
                                    <asp:DropDownList ID="dpCompany" CssClass="copy10grey" runat="server"  AutoPostBack="true" Width="45%"
                                         onselectedindexchanged="ddlCustomer_SelectedIndexChanged">
									</asp:DropDownList>
                                </td>
                            </tr>
                            <%--<tr id="trStore" runat="server">
                                <td  class="copy10grey" align="right" >
                                    Store ID: &nbsp;</td>
                                <td align="left" >
                                    <asp:DropDownList ID="ddlStoreID" CssClass="copy10grey" runat="server" Width="50%" >
									</asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td  class="copy10grey" align="right"  >
                                    Ship Via: &nbsp;</td>
                                <td align="left" >
                                    <asp:DropDownList ID="dpShipBy" CssClass="copy10grey" runat="server" Width="50%" >
									</asp:DropDownList>
                                </td>
                            </tr>--%>
                            <tr>
                                <td  class="copy10grey" align="right" >
                                    Upload Fulfillment file: &nbsp;</td>
                                <td align="left" >
                                    <asp:FileUpload ID="flnUpload" runat="server" CssClass="txfield1" Width="45%" /></td>
                           </tr>
                           <tr valign="top" id="trShipFile" >
                                <td class="copy10grey" align="right">
                                    File format sample: &nbsp;
                                         </td>
                                <td class="copy10grey" align="left">
                                   <b>FulfillmentOrderType,Fulfillment#,SKU#,Quantity</b>,
                                    StoreID,
                                    <b>ShipToName,ShipToAddress1</b>,
                                    ShipToAddress2,
                                    <b>ShipToCity,ShipToState,ShipToZip,ShipToPhone,ShipmentThrough,RequestedShipDate</b>
                                    <br />
                                    <asp:LinkButton ID="lnkDownload" runat="server"   Text="Download file format" OnClick="lnkDownload_Click"></asp:LinkButton>
                                </td>
                            </tr>
                            <tr  valign="top">
                                <td class="copy10grey" align="right">
                                    Comment: &nbsp;
                                         </td>
                                <td class="copy10grey" align="left">
                                    <asp:TextBox ID="txtComment" runat="server" CssClass="copy10grey" Width="70%" Height="50px" TextMode="MultiLine"></asp:TextBox>

                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Panel ID="pnlSubmit" runat="server" Visible="false">
                                    <table cellpadding="0" cellspacing="0" width="100%">
                                        <tr><td>
                                        <hr style="width:100%" />
                            
                                        </td></tr>   
                                         <tr>                    
                                            <td >
                                                <asp:Label ID="lblConfirm" runat="server" Width="100%" CssClass="errorGreenMsg"></asp:Label></td>
                                        </tr>               
                                    
                                         <tr>
                                            <td  align="center">
                                
                                &nbsp;<asp:Button ID="btnValidate2" Visible="false" Width="190px"  CssClass="button" runat="server" OnClick="btnValidatePOs_Click" Text="Validate" />
                                
                                            <asp:Button ID="btnSubmit2" Visible="false" Width="190px"  CssClass="button" runat="server" OnClick="btnSubmit_Click" Text="Submit" />

                                            &nbsp;<asp:Button ID="btnCancel2" runat="server"  CssClass="button" Text="Cancel" OnClick="btnCancel_Click" />
                                                </td>
                                        </tr>
                                        <tr><td>
                                        <hr style="width:100%" />
                            
                                        </td></tr>
                                    </table>

                                    </asp:Panel>
                                </td>
                            </tr>
                             <tr><td colspan="2">
                            
                            <table cellpadding="0" cellspacing="0" width="100%">
                             <tr>
                                <td colspan="2" align="right" style="height:8px; vertical-align:bottom">
                                <strong>   <asp:Label ID="lblCount" CssClass="copy10grey" runat="server" ></asp:Label> &nbsp;&nbsp;</strong> 
                                </td>
                             </tr>
                             <tr>
                                <td colspan="2" align="center">
                                        
    
                                
                                <asp:Repeater ID="rptPO" runat="server" Visible="true"  OnItemDataBound="rpt_OnItemDataBound">
                                <HeaderTemplate>
                                <table border="0" width="98%" cellpadding="1" cellspacing="1">
                                <tr>
                                    <td class="button" >
                                        S.No.
                                    </td>
                                    <td class="button" >
                                        PO Type
                                    </td>
                                    
                                    <td class="button" >
                                        Fulfillment#
                                    </td>
                                    
                                    <td class="button">
                                        PO Date &nbsp;&nbsp; &nbsp;&nbsp;
                                    </td>
                                    <td class="button">
                                        Store ID 
                                    </td>
                                    <td class="button">
                                        Ship Via
                                    </td>
                                    <td class="button">
                                        Contact Name
                                    </td>
                                    <td class="button">
                                        Address1  
                                    </td>
                                    <td class="button">
                                        Address2  
                                    </td>
                                    <td class="button">
                                        City
                                    </td>
                                    <td class="button">
                                        State  
                                    </td>
                                    <td class="button">
                                        zip
                                    </td>
                                    <td class="button">
                                        Phone#
                                    </td>
                                    <td class="button">
                                        Requested Ship Date
                                    </td>
                                    <td class="button">
                                    Status
                                    </td>
                                </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
    
                                    <tr valign="bottom"  class="<%# Container.ItemIndex % 2 == 0 ? "alternaterow" : "" %>">
                                        <td class="copy10grey"  >
                                        <%# Container.ItemIndex +  1 %>
                                        </td>
                                        <td valign="bottom" class="copy10grey"  style="background-color:<%# Convert.ToString(Eval("POType")) == "" ? "red" : ""%>">
                                            <span width="100%">
                                                <%# Eval("POType")%> 
                                                </span>
                                        </td>
                                        <td valign="bottom" class="copy10grey"  style="background-color:<%# Convert.ToString(Eval("PurchaseOrderNumber")) == "" ? "red" : ""%>">
                                        <span width="100%" >
                                            <asp:LinkButton ID="lnkPoNum" CommandArgument='<%# Eval("PurchaseOrderNumber") %>' OnCommand="lnkPoNum_OnCommand" runat="server"><%# Eval("PurchaseOrderNumber")%></asp:LinkButton>
                                                

                                            <%--<%# Convert.ToString(Eval("FulfillmentNumber")) == "" ? "red" : ""%>  
                                             
                                             
                                             style="background-color:<%# Convert.ToString(Eval("SalesOrderNumber")) == "" ? "red" : ""%>"  
                                            --%>
                                            </span>
                                        </td>
                                        
                                        <td valign="bottom" class="copy10grey"  style="background-color:<%# Convert.ToDateTime( Eval("PurchaseOrderDate")).ToShortDateString() == "" ? "red" : ""%>">
                                        <span width="100%">
                                            <%# Convert.ToDateTime(Eval("PurchaseOrderDate")).ToShortDateString() %>    
                                            </span>
                                        </td>
                                        <td valign="bottom" class="copy10grey"  style="background-color:<%# Convert.ToString(Eval("POType")) == "B2B" && Convert.ToString(Eval("StoreID")) == "" ? "red" : ""%>">
                                        <span width="100%">
                                            <%# Eval("StoreID")%> 
                                            </span>
                                        </td>
                                        <td valign="bottom" class="copy10grey"  style="background-color:<%# Convert.ToString(Eval("ShipThrough")) == "" ? "red" : ""%>">
                                        <span width="100%">
                                            <%# Eval("ShipThrough")%> 
                                            </span>
                                        </td>
                                        
                                        <td valign="bottom" class="copy10grey"  style="background-color:<%# Convert.ToString(Eval("Shipping.ContactName")) == "" ? "red" : ""%>">
                                        <span width="100%">
                                            <%# Eval("Shipping.ContactName")%>    
                                            </span>
                                        </td>
                                        <td valign="bottom" class="copy10grey"  style="background-color:<%# Convert.ToString(Eval("Shipping.ShipToAddress")) == "" ? "red" : ""%>">
                                        <span width="100%">
                                            <%# Eval("Shipping.ShipToAddress")%>    
                                            </span>
                                        </td>
                                        <td valign="bottom" class="copy10grey"  style="background-color:<%# Convert.ToString(Eval("Shipping.ShipToAddress2")) == "" ? "" : ""%>">
                                        <span width="100%">
                                            <%# Eval("Shipping.ShipToAddress2")%>    
                                            </span>
                                        </td>
                                        <td valign="bottom" class="copy10grey"  style="background-color:<%# Convert.ToString(Eval("Shipping.ShipToCity")) == "" ? "red" : ""%>">
                                        <span width="100%">
                                            <%# Eval("Shipping.ShipToCity")%>    
                                            </span>
                                        </td>
                                        <td valign="bottom" class="copy10grey"  style="background-color:<%# Convert.ToString(Eval("Shipping.ShipToState")) == "" ? "red" : ""%>">
                                        <span width="100%">
                                            <%# Eval("Shipping.ShipToState")%>    
                                            </span>
                                        </td>
                                        <td valign="bottom" class="copy10grey"  style="background-color:<%# Convert.ToString(Eval("Shipping.ShipToZip")) == "" ? "red" : ""%>">
                                        <span width="100%">
                                            <%# Eval("Shipping.ShipToZip")%>    
                                            </span>
                                        </td>
                                        <td valign="bottom" class="copy10grey"  style="background-color:<%# Convert.ToString(Eval("Shipping.ContactPhone")) == "" ? "red" : ""%>">
                                        <span width="100%">
                                            <%# Eval("Shipping.ContactPhone")%>    
                                            </span>
                                        
                                        </td>
                                        <td valign="bottom" class="copy10grey"  style="background-color:<%# Convert.ToDateTime(Eval("RequestedShipDate")).ToShortDateString() == "" ? "red" : ""%>">
                                        <span width="100%">

                                            <%# Convert.ToDateTime(Eval("RequestedShipDate")).ToShortDateString() %>    
                                            </span>
                                        
                                        </td>
                                        <td valign="bottom" class="copy10grey"  >
                                        <span width="100%">
                                            <%# Eval("Comments")%>    
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
                            </td></tr>                            
                            
                            <tr><td colspan="2">
                            <hr style="width:100%" />
                            
                            </td></tr>                            
                             <tr>
                                <td colspan="2" align="center">
                                
                                <asp:Button ID="btnUpload"  Width="190px" CssClass="button" runat="server" OnClick="btnValidateUploadedFile_Click" Text="Validate Uploaded file" />
                                &nbsp;<asp:Button ID="btnValidate" Visible="false" Width="190px"  CssClass="button" runat="server" OnClick="btnValidatePOs_Click" Text="Validate" />
                                
                                &nbsp;<asp:Button ID="btnSubmit" Visible="false" Width="190px"  CssClass="button" runat="server" OnClick="btnSubmit_Click" Text="Submit" />
                                &nbsp;<asp:Button ID="btnViewPO" Visible="false" Width="190px"  CssClass="button" runat="server" OnClick="btnViewAssignedPos_Click" Text="View uploaded fulfillment" />
                                
                                &nbsp;<asp:Button ID="btnCancel" runat="server"  CssClass="button" Text="Cancel" OnClick="btnCancel_Click" />
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
            <Triggers>
                <asp:PostBackTrigger ControlID="btnUpload" />
                <asp:PostBackTrigger ControlID="btnCancel" />
                <asp:PostBackTrigger ControlID="btnCancel2" />
                <asp:PostBackTrigger ControlID="btnSubmit2" />
                <asp:PostBackTrigger ControlID="btnValidate" />
                <asp:PostBackTrigger ControlID="btnValidate2" />
                <asp:PostBackTrigger ControlID="lnkDownload" />

            </Triggers>
            </asp:UpdatePanel>
    
    <div id="divFulfillmentContainer">
			<div id="divFulfillmentView" style="display:none">
					
				<asp:UpdatePanel ID="upnlView" runat="server">
					<ContentTemplate>
                   
						<asp:PlaceHolder ID="phrView" runat="server">
                            <%--<asp:Label ID="lblViewPO" runat="server"  CssClass="errormessage"></asp:Label>
    --%>
                            <asp:Panel ID="pnlPO" runat="server">
                            

                            </asp:Panel>


                        </asp:PlaceHolder>
						
						
					</ContentTemplate>
							
				</asp:UpdatePanel>
           </div>
           <div id="divShipVia" style="display:none">
					
				<asp:UpdatePanel ID="upShipvia" runat="server">
					<ContentTemplate>
                   
						<asp:PlaceHolder ID="phShipvia" runat="server">
                            <%--<asp:Label ID="lblViewPO" runat="server"  CssClass="errormessage"></asp:Label>
    --%>
                            <asp:Panel ID="pnlShipvia" runat="server">
                            
                               <%-- <Po:ShipVia ID="shipvia1" runat="server" />--%>
                            </asp:Panel>


                        </asp:PlaceHolder>
						
						
					</ContentTemplate>
							
				</asp:UpdatePanel>
           </div>
           <div id="divState" style="display:none">
					
				<asp:UpdatePanel ID="upState" runat="server">
					<ContentTemplate>
                   
						<asp:PlaceHolder ID="phState" runat="server">
                            
                            <asp:Panel ID="pnlState" runat="server">
                           <%-- <WC:State ID="state1" runat="server" />--%>
                               
                            </asp:Panel>


                        </asp:PlaceHolder>
						
						
					</ContentTemplate>
							
				</asp:UpdatePanel>
           </div>
           
           </div>
           <asp:UpdatePanel ID="upnlJsRunner" UpdateMode="Always" runat="server">
			<ContentTemplate>
				<asp:PlaceHolder ID="phrJsRunner" runat="server"></asp:PlaceHolder>
			</ContentTemplate>
		</asp:UpdatePanel>

    </form>
</body>
</html>
