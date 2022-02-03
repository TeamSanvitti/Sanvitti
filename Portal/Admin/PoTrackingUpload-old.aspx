<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PoTrackingUpload.aspx.cs" Inherits="avii.Admin.PoTrackingUpload" %>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="/Controls/Header.ascx" %>
<%@ Register TagPrefix="PO" TagName="PoDetail" Src="~/Controls/PODetails.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>.:: Aerovoice ::.</title>
    <link href="../aerostyle.css" rel="stylesheet" type="text/css"/>
    
    <!-- from http://encosia.com/2009/10/11/do-you-know-about-this-undocumented-google-cdn-feature/ -->
    <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/themes/start/jquery-ui.css" type="text/css" rel="Stylesheet" />
	
	<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.4.0/jquery.min.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/jquery-ui.min.js"></script>
    <%--<script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js" type="text/javascript"></script>
<script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js" type="text/javascript"></script>--%>
	<%--<script type="text/javascript" src="JQuery/jquery.min.js"></script>
    <script type="text/javascript" src="JQuery/jquery-ui.min.js"></script>
	--%><script type="text/javascript" src="../JQuery/jquery.blockUI.js"></script>
	
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
	            height: 640,
	            width: 1124,
	            resizable: false,
	            open: function (event, ui) {
	                $(this).parent().appendTo("#divFulfillmentContainer");
	            }
	        });
	    });

	    function closeDialog() {
	        //Could cause an infinite loop because of "on close handling"
	        $("#divFulfillmentView").dialog('close');
	    }
	    function openDialog(title, linkID) {

	        var pos = $("#" + linkID).position();
	        var top = pos.top;
	        var left = pos.left + $("#" + linkID).width() + 10;
	        //alert(top);
	        if (top > 600)
	            top = 10;
	        //top = top - 600;
	        left = 150;
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

        function Validate() {
            //if (flag == '1' || flag == '2') {
            var company = document.getElementById("<% =dpCompany.ClientID %>");
            //alert(company);
            if (company != 'null' && company.selectedIndex == 0) {
                alert('Customer is required!');
                return false;

            }
            //}
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
				<td  bgcolor="#dee7f6" class="button">&nbsp;&nbsp;Assign Tracking to Fulfillment Orders
				</td>
			</tr>
            <tr><td>&nbsp;</td></tr>
            </table>
	
            <asp:UpdatePanel ID="UpdatePanel1"  runat="server" UpdateMode="Conditional" >
            <ContentTemplate>
    
            <table cellSpacing="0" cellPadding="0" align="center" width="100%" border="0">
    		<tr>                    
                <td>
                    <asp:Label ID="lblMsg" runat="server" Width="100%" CssClass="errormessage"></asp:Label></td>
            </tr>               
            
            <tr>
                <td align="center">

                    <table  cellSpacing="1" cellPadding="1" width="100%">
                        <tr><td class="copy10grey" align="left">
                        
	                    - Upload file should be less than 2 MB. <br />&nbsp;
                        - Valid values for Shipment Type column is "S" = Shipment (Default), "R" = Return Label<br />&nbsp;
                        </td></tr>
                    </table>

                 <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="70%">
                    <tr>
                    <td>
			            <table id="Table2" cellSpacing="5" cellPadding="5" width="100%" border="0">
                            <tr>
                                <td  class="copy10grey" align="right" width="35%" >
                                   <b> Customer:</b> &nbsp;</td>
                                <td align="left" >
                                    <asp:DropDownList ID="dpCompany" CssClass="copy10grey" runat="server" Width="51%"  AutoPostBack="false">
									</asp:DropDownList>
                            </tr>
                            <%--<tr>
                                <td  class="copy10grey" align="right" width="35%" >
                                   <b> Ship by: </b>&nbsp;</td>
                                <td align="left" >
                                    <asp:DropDownList ID="ddlShipby" CssClass="copy10grey" runat="server" Width="51%"  AutoPostBack="false">
									</asp:DropDownList>
                            </tr>
                            <tr>
                                <td  class="copy10grey" align="right" width="35%" >
                                    AvOrderNumber: &nbsp;</td>
                                <td align="left" >
                                    <asp:TextBox ID="txtAvOrderNumber" CssClass="copy10grey" runat="server" Width="50%" ></asp:TextBox>
									
                            </tr>--%>
                            
                            
                            <tr>
                                <td  class="copy10grey" align="right" >
                                    Upload Tracking file: &nbsp;</td>
                                <td align="left" >
                                    <asp:FileUpload ID="flnUpload" runat="server" CssClass="txfield1" Width="50%" /></td>
                            </tr>
                            <tr id="trShipFile" >
                                <td class="copy10grey" align="right">
                                    File format sample: &nbsp;
                                         </td>
                                <td class="copy10grey" align="left">
                                    
                                   <b>PoNum,TrackingNumber</b>,AVOrderNumber,ShipmentType,ShippingVia,Comments
                                </td>
                             </tr>

                             <%--<tr valign="top">
                                <td  class="copy10grey" align="right" width="35%" >
                                    Comments: &nbsp;</td>
                                <td align="left" >
                                    <asp:TextBox ID="txtComments" TextMode="MultiLine" Rows="5" CssClass="copy10grey" runat="server" Width="80%" ></asp:TextBox>
									
                            </tr>
                            --%>
                             
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
                                
                                
                                            <asp:Button ID="btnSubmit2" Visible="false" Width="190px"  CssClass="button" runat="server" OnClick="btnSubmit_Click" Text="Submit" OnClientClick="return IsValidateDnw();" />

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
                                <td align="right" style="height:8px; vertical-align:bottom">
                                <strong>   <asp:Label ID="lblCount" CssClass="copy10grey" runat="server" ></asp:Label> &nbsp;&nbsp;</strong> 
                                </td>
                             </tr>
                             <tr>
                                <td align="center">
                                
                                <asp:Repeater ID="rptTracking" runat="server" Visible="true"  OnItemDataBound="rpt_OnItemDataBound">
                                <HeaderTemplate>
                                <table border="0" width="98%" cellpadding="1" cellspacing="1">
                                <tr>
                                    <td class="button" >
                                        S.No.
                                    </td>
                                    <td class="button" >
                                        FulfillmentNumber
                                    </td>
                                    
                                    <td class="button">
                                        TrackingNumber
                                    </td>
                                    <td class="button">
                                        AvOrderNumber
                                    </td>
                                    <td class="button">
                                        ShipmentType 
                                    </td>
                                    <td class="button">
                                        shippingVia 
                                    </td>
                                    
                                    <td class="button">
                                        Comments 
                                    </td>
                                </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
    
                                    <tr valign="bottom"  class="<%# Container.ItemIndex % 2 == 0 ? "alternaterow" : "" %>">
                                        <td class="copy10grey"  >
                                        <%# Container.ItemIndex +  1 %>
                                        </td>
                                        <td valign="bottom" class="copy10grey"  style="background-color:<%# Convert.ToString(Eval("FulfillmentNumber")) == "" ? "red" : ""%>">
                                        <span width="100%" >
                                            <asp:LinkButton ID="lnkPoNum" CommandArgument='<%# Eval("FulfillmentNumber") %>' OnCommand="lnkPoNum_OnCommand" runat="server"><%# Eval("FulfillmentNumber")%></asp:LinkButton>
                                            </span>
                                        </td>
                                        <td valign="bottom" class="copy10grey"  style="background-color:<%# Convert.ToString(Eval("Tracking")) == "" ? "red" : ""%>">
                                        <span width="100%">
                                            <%# Eval("Tracking")%>    
                                            </span>
                                        </td>
                                        <td valign="bottom" class="copy10grey"  >
                                        <span width="100%">
                                            <%# Eval("avOrderNumber")%>    
                                            </span>
                                        </td>
                                        <td valign="bottom" class="copy10grey"  >
                                        <span width="100%">
                                            <%# Eval("ShipmentType")%>    
                                            </span>
                                        </td>
                                        <td valign="bottom" class="copy10grey"  >
                                        <span width="100%">
                                            <%# Eval("shippingVia")%>    
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

                                &nbsp;<asp:Button ID="btnSubmit" Visible="false" Width="190px"  CssClass="button" runat="server" OnClick="btnSubmit_Click" Text="Submit" OnClientClick="return IsValidateDnw();" />
                                <%--&nbsp;<asp:Button ID="btnViewTracking" Visible="false" Width="190px"  CssClass="button" runat="server" OnClick="btnViewAssignedPos_Click" Text="View Assigned Tracking" />
--%>
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
                            <PO:PoDetail ID="poDetail1" runat="server" />

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
