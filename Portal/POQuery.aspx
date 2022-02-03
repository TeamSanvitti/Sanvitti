<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="POQuery.aspx.cs" Inherits="avii.POQuery" %>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="/Controls/Header.ascx" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="/Controls/Footer.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%--<%@ Register TagPrefix="PO" TagName="Detail" Src="~/Controls/PODetails.ascx" %>
--%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999 /xhtml">
<head id="Head1" runat="server">
    <title>Fulfillment Search</title>

		
        <!-- from http://encosia.com/2009/10/11/do-you-know-about-this-undocumented-google-cdn-feature/ -->
    <link href="https://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/themes/start/jquery-ui.css" type="text/css" rel="Stylesheet" />

			<%--<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.4.0/jquery.min.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/jquery-ui.min.js"></script>--%>
    
<script type="text/javascript" src="./JQuery/jquery.min.js"></script>
    <script type="text/javascript" src="./JQuery/jquery-ui.min.js"></script>
	<script type="text/javascript" src="./JQuery/jquery.blockUI.js"></script>
	
<!-- fix for 1.1em default font size in Jquery UI -->
	<style type="text/css">
		.ui-widget{font-size:11px !important;}
		.ui-state-error-text{margin-left: 10px}
	</style>
    
	<script type="text/javascript">
    
		$(document).ready(function() {

        
			$("#divFulfillmentView").dialog({
				autoOpen: false,
				modal: true,
				minHeight: 20,
				height: 550,
				width: 950,
				resizable: false,
				open: function(event, ui) {
					$(this).parent().appendTo("#divFulfillmentContainer");
				}
			});
            $("#divHistory").dialog({
				autoOpen: false,
				modal: true,
				minHeight: 20,
				height: 350,
				width: 700,
				resizable: false,
				open: function(event, ui) {
					$(this).parent().appendTo("#divFulfillmentContainer");
				}
			});
	
        
	 $("#divEditPO").dialog({
				autoOpen: false,
				modal: true,
				minHeight: 20,
				height: 450,
				width: 850,
				resizable: false,
				open: function(event, ui) {
					$(this).parent().appendTo("#divFulfillmentContainer");
				}
			});

        $("#divEditPOD").dialog({
				autoOpen: false,
				modal: true,
				minHeight: 20,
				height: 500,
				width: 600,
				resizable: false,
				open: function(event, ui) {
					$(this).parent().appendTo("#divFulfillmentContainer");
				}
			});

            $("#divDownload").dialog({
				autoOpen: false,
				modal: true,
				minHeight: 20,
				height: 200,
				width: 700,
				resizable: false,
				open: function(event, ui) {
					$(this).parent().appendTo("#divFulfillmentContainer");
				}
			});
		});
        
        
        
		function closeDialog() {
			//Could cause an infinite loop because of "on close handling"
			$("#divFulfillmentView").dialog('close');
		}
        function closeHistoryDialog() {
			//Could cause an infinite loop because of "on close handling"
			$("#divHistory").dialog('close');
		}
        
        function closeEditDialog() {
			//Could cause an infinite loop because of "on close handling"
			$("#divEditPO").dialog('close');
		}
        function closeEditPODDialog() {
			//Could cause an infinite loop because of "on close handling"
			$("#divEditPOD").dialog('close');
		}
		function closeDownloadDialog() {
			//Could cause an infinite loop because of "on close handling"
			$("#divDownload").dialog('close');
		}
        
		function openDialog(title, linkID) {
		
			var pos = $("#" + linkID).position();
			var top = pos.top;
			var left = pos.left + $("#" + linkID).width() + 10;
			//alert(top);
            //top = top - 600;
		if (top > 600)
	                top = 10;
			left = 200;
			$("#divFulfillmentView").dialog("option", "title", title);
			$("#divFulfillmentView").dialog("option", "position", [left, top]);
			
			$("#divFulfillmentView").dialog('open');
		}

        function openHistoryDialog(title, linkID) {
		
			var pos = $("#" + linkID).position();
			var top = pos.top;
			var left = pos.left + $("#" + linkID).width() + 10;
			//alert(left);
            //top = top - 600;
		if (top > 600)
	                top = 10;
			left = 300;
            //$("#divHistory").html("");
            //$('#divHistory').empty();
            //alert($('#phrHistory'));
			$("#divHistory").dialog("option", "title", title);
			$("#divHistory").dialog("option", "position", [left, top]);
			
			$("#divHistory").dialog('open');
		}

        
        function openEditDialog(title, linkID) {
		
			var pos = $("#" + linkID).position();
			var top = pos.top;
			var left = pos.left + $("#" + linkID).width() + 10;
			//alert(left);
            //top = top - 600;
		if (top > 600)
	                top = 10;
			left = 300;
            //$("#divHistory").html("");
            //$('#divHistory').empty();
            //alert($('#phrHistory'));
			$("#divEditPO").dialog("option", "title", title);
			$("#divEditPO").dialog("option", "position", [left, top]);
			
			$("#divEditPO").dialog('open');
		}
        function openEditPODDialog(title, linkID) {
		
			var pos = $("#" + linkID).position();
			var top = pos.top;
			var left = pos.left + $("#" + linkID).width() + 10;
			//alert(left);
            //top = top - 600;
		if (top > 600)
	                top = 10;
			left = 300;
            //$("#divHistory").html("");
            //$('#divHistory').empty();
            //alert($('#phrHistory'));
			$("#divEditPOD").dialog("option", "title", title);
			$("#divEditPOD").dialog("option", "position", [left, top]);
			
			$("#divEditPOD").dialog('open');
		}
        
        function openDownloadDialog(title, linkID) {
		
			var pos = $("#" + linkID).position();
			var top = pos.top;
			var left = pos.left + $("#" + linkID).width() + 10;
			//alert(left);
            top = top - 300;
			left = 275;
			$("#divDownload").dialog("option", "title", title);
			$("#divDownload").dialog("option", "position", [left, top]);
			
			$("#divDownload").dialog('open');
		}

        function openDownloadDialogAndBlock(title, linkID) {
			openDownloadDialog(title, linkID);

			//block it to clean out the data
			$("#divDownload").block({
				message: '<img src="../images/async.gif" />',
				css: { border: '0px' },
				fadeIn: 0,
				//fadeOut: 0,
				overlayCSS: { backgroundColor: '#ffffff', opacity: 1 } 
			});
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
        function openHistoryDialogAndBlock(title, linkID) {
			openHistoryDialog(title, linkID);

			//block it to clean out the data
			$("#divHistory").block({
				message: '<img src="../images/async.gif" />',
				css: { border: '0px' },
				fadeIn: 0,
				//fadeOut: 0,
				overlayCSS: { backgroundColor: '#ffffff', opacity: 1 } 
			});
		}
        function openEditDialogAndBlock(title, linkID) {
			openEditDialog(title, linkID);

			//block it to clean out the data
			$("#divEditPO").block({
				message: '<img src="../images/async.gif" />',
				css: { border: '0px' },
				fadeIn: 0,
				//fadeOut: 0,
				overlayCSS: { backgroundColor: '#ffffff', opacity: 1 } 
			});
		}
        function openEditPODDialogAndBlock(title, linkID) {
			openEditPODDialog(title, linkID);

			//block it to clean out the data
			$("#divEditPOD").block({
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
        function unblockHistoryDialog() {
			$("#divHistory").unblock();
		}
        function unblockEditDialog() {
			$("#divEditPO").unblock();
		}
        function unblockEditPODDialog() {
			$("#divEditPOD").unblock();
		}
        function unblockDownloadDialog() {
			$("#divDownload").unblock();
		}
        
		function onTest() {
			$("#divFulfillmentView").block({
				message: '<h1>Processing</h1>',
				css: { border: '3px solid #a00' },
				overlayCSS: { backgroundColor: '#ffffff', opacity: 1 } 
			});
		}
        
	</script>


   <link href="./aerostyle.css" rel="stylesheet" type="text/css"/>
		<script type="text/javascript" language="javascript" src="./avI.js"></script> 
		<link rel="stylesheet"  type="text/css" href="../fullfillment/calendar/dhtmlgoodies_calendar.css" media="screen" />
		<script type="text/javascript" src="../fullfillment/calendar/dhtmlgoodies_calendar.js"></script>  
		<link rel="stylesheet" type="text/css" href="/dhtmlxwindow/dhtmlxwindows.css"/>
	    <link rel="stylesheet" type="text/css" href="/dhtmlxwindow/skins/dhtmlxwindows_dhx_skyblue.css"/>
	    <script src="/dhtmlxwindow/dhtmlxcommon.js" type="text/javascript"></script>
	    <script src="/dhtmlxwindow/dhtmlxwindows.js" type="text/javascript"></script>
	    <script src="/dhtmlxwindow/dhtmlxcontainer.js" type="text/javascript"></script>
        <script type="text/javascript" language="javascript">
            var dhxWins, w1;
            function POSummary() {
                dhxWins = new dhtmlXWindows();
                dhxWins.enableAutoViewport(false);
                dhxWins.attachViewportTo("winVP");
                dhxWins.setImagePath("../../codebase/imgs/");

                var topPos = 100; // document.documentElement.scrollTop;


                w1 = dhxWins.createWindow("w1", 125, topPos, 725, 350);
                w1.setText("Fulfillment Summary");
                w1.attachURL("./posummary.aspx");

            }
        </script>
		<script language="javascript" type="text/javascript">

function ValidateStatus() {
	        var status = document.getElementById("<%=dpStatus.ClientID  %>");
	        if (status.selectedIndex == 0) {
	            alert('Select a status first');
	            return false;
            }
        }
		    function IsValidateDnw() {
		        var ddlDnw = document.getElementById("<%=dpDownloadDataList.ClientID %>");
		        if (ddlDnw.selectedIndex == 0) {
		            alert('Please select download option');
		            return false;
		        }
		    }
		    function alphaNumericCheck(evt) {
		        var charCodes = evt.keyCode ? evt.keyCode : evt.which ? evt.which : evt.charCode ? evt.charCode : evt.type;
		        //alert(charCodes);
		        if (charCodes == 8 || charCodes == 9 || charCodes == 46 || (105 >= charCodes && charCodes >= 96)
                || (90 >= charCodes && charCodes >= 65)
                || (57 >= charCodes && charCodes >= 48)
                 || (122 >= charCodes && charCodes >= 97)) {
		            return true;
		        }
		        else {
		            return false;
		        }
		    }
		    function alphaOnly(eventRef) {
		        var returnValue = false;
		        var keyStroke = (eventRef.which) ? eventRef.which : (window.event) ? window.event.keyCode : -1;
		        if (((keyStroke >= 65) && (keyStroke <= 90)) ||
              ((keyStroke >= 97) && (keyStroke <= 122)) ||
              ((keyStroke >= 45 && keyStroke < 58)))
		            returnValue = true;

		        if (navigator.appName.indexOf('Microsoft') != -1)
		            window.event.returnValue = returnValue;

		        return returnValue;

		    }



		    function expandcollapse(obj, row) {
		        var div = document.getElementById(obj);
		        var img = document.getElementById('img' + obj);

		        if (div.style.display == "none") {
		            div.style.display = "block";
		            if (row == 'alt') {
		                img.src = "../images/minus.gif";
		            }
		            else {
		                img.src = "../images/minus.gif";
		            }
		            img.alt = "Close to view Fulfillment";
		        }
		        else {
		            div.style.display = "none";
		            if (row == 'alt') {
		                img.src = "../images/plus.gif";
		            }
		            else {
		                img.src = "../images/plus.gif";
		            }
		            img.alt = "Expand to show Orders";
		        }
		    }





		    function mask(e, f) {
		        var len = f.value.length;
		        var key = whichKey(e);
		        if (key > 47 && key < 58) {
		            if (len == 3) f.value = f.value + '-'
		            else if (len == 7) f.value = f.value + '-'
		            else f.value = f.value;
		        }
		        else {
		            f.value = f.value.replace(/[^0-9-]/, '')
		            f.value = f.value.replace('--', '-')
		        }
		    }

		    function whichKey(e) {
		        var code;
		        if (!e) var e = window.event;
		        if (e.keyCode) code = e.keyCode;
		        else if (e.which) code = e.which;
		        return code
		        //	return String.fromCharCode(code);
		    }
 
    </script>

     <script type="text/javascript" >
         $(document).AjaxReady(function () {
             $("#[id$=txtFromDate]").focusin(function (event) {

                 $('#imgFromtDate').click();
                 event.preventDefault();

             });
             $("#[id$=txtFromDate]").keypress(function (event) {
                 $('#imgFromtDate').click();
                 event.preventDefault();

             });
             $('#txtToDate').focusin(function (event) {
                 $('#imgToDate').click();
                 event.preventDefault();

             });
             $('#txtToDate').keypress(function (event) {
                 $('#imgToDate').click();
                 event.preventDefault();

             });
             $('#txtShipFrom').focusin(function (event) {
                 $('#img1').click();
                 event.preventDefault();

             });
             $('#txtShipFrom').keypress(function (event) {
                 $('#img1').click();
                 event.preventDefault();

             });
             $('#txtShipTo').focusin(function (event) {
                 $('#img2').click();
                 event.preventDefault();

             });
             $('#txtShipTo').keypress(function (event) {
                 $('#img2').click();
                 event.preventDefault();

             });

         });
         $(document).ready(function () {
             $("#[id$=txtFromDate]").focusin(function (event) {

                 $('#imgFromtDate').click();
                 event.preventDefault();

             });
             $("#[id$=txtFromDate]").keypress(function (event) {
                 $('#imgFromtDate').click();
                 event.preventDefault();

             });
             $('#txtToDate').focusin(function (event) {
                 $('#imgToDate').click();
                 event.preventDefault();

             });
             $('#txtToDate').keypress(function (event) {
                 $('#imgToDate').click();
                 event.preventDefault();

             });
             $('#txtShipFrom').focusin(function (event) {
                 $('#img1').click();
                 event.preventDefault();

             });
             $('#txtShipFrom').keypress(function (event) {
                 $('#img1').click();
                 event.preventDefault();

             });
             $('#txtShipTo').focusin(function (event) {
                 $('#img2').click();
                 event.preventDefault();

             });
             $('#txtShipTo').keypress(function (event) {
                 $('#img2').click();
                 event.preventDefault();

             });

         });
        </script>

</head>
<body bgColor="#ffffff" leftmargin="0" rightmargin="0" topmargin="0">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    
    <table cellspacing="0" cellpadding="0"  border="0" align="center" width="100%">
	<tr>
	    <td><head:menuheader id="MenuHeader" runat="server"></head:menuheader>
		</td>
	</tr>
    </table>
      <div id="divFulfillmentContainer">	
<div id="divFulfillmentView" style="display:none">
					
				<asp:UpdatePanel ID="upnlView" runat="server">
					<ContentTemplate>
                   
						<asp:PlaceHolder ID="phrView" runat="server">
                            <asp:Label ID="lblViewPO" runat="server"  CssClass="errormessage"></asp:Label>
                            <table width="98%" align="center" cellpadding="0" cellspacing="0">
<tr valign="top">
    <td width="50%">
        <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="100%" align="center" >
           <tr bordercolor="#839abf">
                <td>
                    <table width="100%" cellSpacing="1" cellPadding="1">
                    <tr>
                        <td class="copy10grey" width="22%" align="left">
                            Fulfillment#:
                        </td>
                        <td width="1%">&nbsp;</td>
                        <td  align="left" width="32%" >
                            <asp:Label ID="lblPO" CssClass="copy10grey" runat="server" ></asp:Label>
                        </td>
                        <td class="copy10grey" width="15%" align="right">
                        AVSO#:
                        </td>
                        <td width="1%">&nbsp;</td>
                        <td  align="left" width="29%" >
                            <asp:Label ID="lblvAvso" CssClass="copy10grey" runat="server" ></asp:Label>
                        </td>
                   
                    </tr>
                    <tr>
                        <td class="copy10grey" width="22%" align="left">
                            Fulfillment Date:
                        </td>
                        <td width="1%">&nbsp;</td>
                        <td  align="left" width="32%" >
                            <asp:Label ID="lblvPODate" CssClass="copy10grey" runat="server" ></asp:Label>
                        </td>
                        <td class="copy10grey" width="15%" align="right">
                          <strong>  Status:</strong>
                        </td>
                        <td width="1%">&nbsp;</td>
                        <td  align="left" width="29%" >
                            <asp:Label ID="lblvStatus" CssClass="copy10grey" runat="server" ></asp:Label>
                        </td>
                    </tr>
                    <%--<tr>
                        
                    </tr>--%>
                    </table>
                </td>
            </tr>
        </table><br />
        <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="100%" align="center" >
           <tr bordercolor="#839abf">
                <td>
                    <table width="100%" cellSpacing="1" cellPadding="1">
                    <tr>
                        <td class="copy10grey" width="22%" align="left">
                        Shipp By:
                        </td>
                        <td width="1%">&nbsp;</td>
                        <td align="left" width="32%">
                            <asp:Label ID="lblShipBy" CssClass="copy10grey" runat="server" ></asp:Label>
                        </td>
                        <td class="copy10grey" width="15%" align="right">
                        Tracking#:
                        </td>
                        <td width="1%">&nbsp;</td>
                        <td  align="left" width="29%">
                            <asp:Label ID="lblTrackNo" CssClass="copy10grey" runat="server" ></asp:Label>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="copy10grey" width="22%" align="left">
                        Shipping Date:
                        </td>
                        <td width="1%">&nbsp;</td>
                        <td align="left" width="32%" >
                            <asp:Label ID="lblShippDate" CssClass="copy10grey" runat="server" ></asp:Label>&nbsp;
                        </td>
                        <td class="copy10grey" width="15%" align="left">
                        &nbsp;
                        </td>
                        <td width="1%">&nbsp;</td>
                        <td  align="left" width="29%">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        
                    </tr>
                    <%--<tr>
                        
                    </tr>--%>
                    </table>
                </td>
            </tr>
            </table>
    </td>
    <td width="50%">
    
        <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="99%" align="center" >
           <tr bordercolor="#839abf">
                <td>
                <table width="100%" cellSpacing="2" cellPadding="1">
                <tr>
                    
            
                    <td class="copy10grey" width="30%" align="left">
                        Customer name:
                    </td>
                    <td width="1%">&nbsp;</td>
                    <td  align="left" colspan="4">
            
                        <asp:Label ID="lblCustName" CssClass="copy10grey" runat="server" ></asp:Label>
                    </td>
                </tr>
                <tr>
                    
                    <td class="copy10grey" width="30%" align="left">
                        Contact name:
                    </td>
                    <td width="1%">&nbsp;</td>
                    <td  align="left" colspan="4">
                        <asp:Label ID="lblContactName" CssClass="copy10grey" runat="server" ></asp:Label>
                    </td>
                </tr>
                <tr>
                    
                    <td class="copy10grey" width="30%" align="left">
                        Store ID:
                    </td>
                    <td width="1%">&nbsp;</td>
                    <td align="left" colspan="4">
                        <asp:Label ID="lblvStoreID" CssClass="copy10grey" runat="server" ></asp:Label>
                    </td>
                </tr>
                <tr>
                    
                    <td class="copy10grey" width="30%" align="left">
                        Street Address:
                    </td>
                    <td width="1%">&nbsp;</td>
                    <td  align="left" colspan="4">
                        <asp:Label ID="lblAddress" CssClass="copy10grey" runat="server" ></asp:Label>
                    </td>
                </tr>
                <tr>
                    
                    <td class="copy10grey" width="30%" align="left">
                        State:
                    </td>
                    <td width="1%">&nbsp;</td>
                    <td  align="left">
                        <asp:Label ID="lblState" CssClass="copy10grey" runat="server" ></asp:Label>
                    </td>
                     <td class="copy10grey" width="30%" align="right">
                        Zip:
                    </td>
                    <td width="1%">&nbsp;</td>
                    <td  align="left">
                        <asp:Label ID="lblZip" CssClass="copy10grey" runat="server" ></asp:Label>
                    </td>
                </tr>
                <tr>
                    
                   
                </tr>
               <%-- <tr>
                    
                </tr>
               --%>   
                </table>
                </td>
            </tr>
        </table>
    </td>
</tr>

<tr>
<td>&nbsp;</td>
</tr>
<tr >
<td colspan="2">
<table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="100%" align="center" >
<tr bordercolor="#839abf">
    <td>
        <table width="100%" cellSpacing="3" cellPadding="3">
        <tr>
            <td class="copy10grey" width="10%" align="left"> 
            Comments:
            </td>
            <td width="1%">&nbsp;</td>
            <td   align="left">
                <asp:Label ID="lblComment" CssClass="copy10grey" runat="server" ></asp:Label>
            </td>
        </tr> 
        </table>
    </td>
</tr>
</table>
</td>    
</tr>
<tr>
<td>&nbsp;</td>
</tr>
<tr>
    <td colspan="2">

 <asp:GridView ID="gvPODetail"  BackColor="White" Width="100%" Visible="true"
        AutoGenerateColumns="false" Font-Names="Verdana" runat="server" DataKeyNames="PodID"
        GridLines="Both"
        BorderStyle="Double" BorderColor="#0083C1">
        <RowStyle BackColor="Gainsboro" />
        <AlternatingRowStyle BackColor="white" />
        <HeaderStyle  CssClass="button" ForeColor="white"/>
        <FooterStyle CssClass="white"  />
        <Columns>
            <asp:TemplateField HeaderText="Product Code" HeaderStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey"  ItemStyle-width="20%">
                <ItemTemplate>
                    <%# Eval("ItemCode")%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="UPC" SortExpression="UPC"  ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                <ItemTemplate><%#Eval("UPC")%></ItemTemplate>
            </asp:TemplateField>
                                                                                                                                    
                                            
            <asp:TemplateField HeaderText="Qty" SortExpression="Qty"  ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                <ItemTemplate><%#Eval("Quantity")%></ItemTemplate>
            </asp:TemplateField>
                                            
            <asp:TemplateField HeaderText="ESN" SortExpression="ESN" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                <ItemTemplate>
                    <%# Eval("ESN") %>
                </ItemTemplate>
                                                
            </asp:TemplateField>
                                            
            <asp:TemplateField HeaderText="MDN" SortExpression="MdnNumber"  ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                <ItemTemplate><%#Eval("MdnNumber")%></ItemTemplate>
                                                               
            </asp:TemplateField>
                                        
            <asp:TemplateField HeaderText="MSID" SortExpression="MsID"  ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                <ItemTemplate><%#Eval("MsID")%></ItemTemplate>
                                                                                                 
            </asp:TemplateField>
                                            

            <asp:TemplateField HeaderText="MslNumber" SortExpression="MslNumber" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                <ItemTemplate>
                    <%#Eval("MslNumber")%>
                </ItemTemplate>
                                                
            </asp:TemplateField>
                                            
            <asp:TemplateField HeaderText="Passcode" SortExpression="PassCode"  ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                <ItemTemplate><%#Eval("PassCode")%></ItemTemplate>
            </asp:TemplateField>

                                            
                <asp:TemplateField HeaderText="FM-UPC" SortExpression="FMUPC" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                <ItemTemplate>
                    <%#Eval("FMUPC")%>
                </ItemTemplate>
                                                
            </asp:TemplateField>
                                                                                                                                        
           <%-- <asp:TemplateField HeaderText="Phone Type" SortExpression="PhoneCategory"  ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                <ItemTemplate><%#Eval("PhoneCategory")%></ItemTemplate>
            </asp:TemplateField>--%>
             
            <asp:TemplateField HeaderText="Status" SortExpression="StatusID"  ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                <ItemTemplate>
                <%# Convert.ToInt32(Eval("StatusID"))==1 ? "Pending": Convert.ToInt32(Eval("StatusID"))==2 ? "Processed": Convert.ToInt32(Eval("StatusID"))==3 ? "Shipped": Convert.ToInt32(Eval("StatusID"))==4 ? "Closed": Convert.ToInt32(Eval("StatusID"))==5 ? "Return": Convert.ToInt32(Eval("StatusID"))==9 ? "Cancel": Convert.ToInt32(Eval("StatusID"))==6 ? "On Hold": Convert.ToInt32(Eval("StatusID"))==7 ? "Out of Stock": Convert.ToInt32(Eval("StatusID"))==8 ? "InProcess": "Pending"%>
                <%-- <br /> <%#Eval("PODStatus")%>--%>
                                                
                </ItemTemplate>
            </asp:TemplateField>                                                     
			                                
        </Columns>
    </asp:GridView>
    </td>
</tr>
</table>
		                </asp:PlaceHolder>
						
						
					</ContentTemplate>
							
				</asp:UpdatePanel>
           </div>
<div id="divHistory" style="display:none">
					
				<asp:UpdatePanel ID="upnlHistory" runat="server">
					<ContentTemplate>
                   
						<asp:PlaceHolder ID="phrHistory" runat="server">
                         <table bordercolor="#839abf" border="1" width="98%" align="center" cellpadding="0" cellspacing="0">
    <tr><td>

    <table border="0" width="100%" class="box" align="center" cellpadding="0" cellspacing="0">
    <tr valign="top">
        <td>
            <asp:Label ID="lblHistory" runat="server" CssClass="errormessage"></asp:Label>
                            
            <asp:Repeater ID="rptPO" runat="server">
            <HeaderTemplate>
            <table width="100%" align="center">
                <tr>
                    <td class="button">
                    &nbsp;Fulfillment#
                    </td>
                    <td class="button">
                        &nbsp;Fulfillment Date
                    </td>
                    <td class="button">
                        &nbsp;Modified Date
                    </td>
                    <td class="button">
                        &nbsp;Status
                    </td>
                    
                    <td class="button">
                        &nbsp;Modified By
                    </td>
<td class="button">
                                                        &nbsp;Source
                                                    </td>
                                    
                </tr>
                            
            </HeaderTemplate>
	            <ItemTemplate>
            <tr class="<%# Container.ItemIndex % 2 == 0 ? "alternaterow" : "" %>">
                <td class="copy10grey">
                    &nbsp;<%# Eval("PurchaseOrderNumber")%>
                </td>
                <td class="copy10grey">
                    &nbsp;
                    <%# DataBinder.Eval(Container.DataItem, "PurchaseOrderDate", "{0:MM/dd/yyyy}")%>
                </td>
                <td class="copy10grey">
                    &nbsp;<%# Eval("ModifiedDate")%>
                </td>
                
                <td class="copy10grey">
                    &nbsp;<%# Eval("PurchaseOrderStatus")%>
                </td>
                
                <td class="copy10grey">
                        &nbsp;<%# Eval("CustomerName")%>
                </td>
<td class="copy10grey">
                        &nbsp;<%# Eval("SentEsn")%>
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
    </table>
                                               </asp:PlaceHolder>
						
						
					</ContentTemplate>
							
				</asp:UpdatePanel>



            
        </div>
         <div id="divEditPO" style="display:none">
					
				<asp:UpdatePanel ID="upnlEditPO" runat="server">
					<ContentTemplate>
                   
						<asp:PlaceHolder ID="phrEditPO" runat="server">
                             <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="99%" align="center">
                <tr bordercolor="#839abf">
                <td>
                <table width="100%" align="center" cellpadding="3" cellspacing="3" border="0">
                <tr>
                    <td class="copy10grey" align="right" width="17%">
                        Fulfillment#:    
                    </td>

                    <td width="32%">
                        &nbsp; <asp:Label ID="lblPONo" runat="server" CssClass="copy10grey"></asp:Label>   
                    </td>
                    <td width="2%">
                    &nbsp;
                    </td>
                    <td class="copy10grey" align="right" width="17%">
                        AVSO#:    
                    </td>
                    <td width="32%">

                       &nbsp;  <asp:Label ID="lblAVSO" runat="server" CssClass="copy10grey"></asp:Label>   
                    </td>

                </tr>
                <tr>
                    <td class="copy10grey" align="right" width="17%">
                        Fulfillment Date:    
                    </td>
                    <td width="32%">
                       &nbsp; <asp:Label ID="lblPoDate" runat="server" CssClass="copy10grey"></asp:Label>   
                    </td>
                    <td width="2%">
                    &nbsp;
                    </td>
                    
                    <td align="right" width="17%">
                        <asp:Label ID="lblPOStatus" CssClass="copy10grey" runat="server" Text="Status:"></asp:Label>
                    </td>    
                    <td width="32%">
                     &nbsp; <asp:DropDownList ID="ddlStatus" runat="server" class="copy10grey">
                                <asp:ListItem Text="" Value=""></asp:ListItem>
                                <asp:ListItem Text="Pending" Value="1"></asp:ListItem>
<asp:ListItem Text="In-Process" Value="8"></asp:ListItem>
                                <asp:ListItem Text="Processed" Value="2"></asp:ListItem>
                                <asp:ListItem Text="Shipped" Value="3"></asp:ListItem>
                                <asp:ListItem Text="Closed" Value="4"></asp:ListItem>
                                <asp:ListItem Text="Return" Value="5"></asp:ListItem>

                                <asp:ListItem Text="Cancel" Value="9"></asp:ListItem>
<asp:ListItem Text="On Hold" Value="6"></asp:ListItem>
                                <asp:ListItem Text="Out of Stock" Value="7"></asp:ListItem>
                            </asp:DropDownList>

                            &nbsp;  <asp:Label ID="lblStatus" runat="server" CssClass="copy10grey"></asp:Label>   
                    </td>
                </tr>
                </table>
                </td>
                </tr>
                </table>
                
                <br />

                <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="99%" align="center">
                <tr bordercolor="#839abf">
                <td>
                <table width="100%" align="center" cellpadding="3" cellspacing="3" border="0" >
                <tr>
                    <td class="copy10grey" align="right" width="17%">
                        Customer:    
                    </td>
                    <td width="32%">

                       &nbsp; <asp:Label ID="lblCustomer" runat="server" CssClass="copy10grey"></asp:Label>   
                    </td>
                    <td width="2%">
                    &nbsp;
                    </td>
                    <td class="copy10grey" align="right" width="17%">
                        Store ID:    
                    </td>
                    <td width="32%">

                       &nbsp;  <asp:Label ID="lblStoreID" runat="server" CssClass="copy10grey"></asp:Label>   
                    </td>

                </tr>
                </table>
                </td>
                </tr>
                </table>
                
                <br />
                <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="99%" align="center">
                <tr bordercolor="#839abf">
                <td>
                <table width="100%" align="center" cellpadding="3" cellspacing="3" border="0" >
                
                <tr>
                    <td class="copy10grey" align="right" width="17%">
                        Ship via:    
                    </td>
                    <td width="32%">

                                          <%--<asp:DropDownList ID="dpShipBy" runat="server" Width="205" CssClass="copy10grey"></asp:DropDownList>--%>
 &nbsp; <asp:TextBox ID="txtShipBy" Width="200" MaxLength="200"  CssClass="copy10grey" runat="server"></asp:TextBox>
                
                    </td>
                    <td width="2%">
                    &nbsp;
                    </td>
                    <td class="copy10grey" align="right" width="17%">
                        Tracking#:    
                    </td>
                    <td width="32%">

                       &nbsp; <asp:TextBox ID="txtTrackingNo" Width="200" MaxLength="50" onkeypress="return alphaNumericCheck(event);" CssClass="copy10grey" runat="server"></asp:TextBox>
                
                    </td>
                    
                </tr>
                
                <tr>
                    <td class="copy10grey" align="right" width="17%">
                        Shipping Date:    
                    </td>
                    <td width="32%">

                       &nbsp;  <asp:Label ID="lblShipDate"  runat="server" CssClass="copy10grey"></asp:Label>   
                    </td>
                    <td width="2%">
                    &nbsp;
                    </td>
                    <td width="17%">
                    &nbsp;
                    </td>
                    <td width="32%">
                    &nbsp;
                    </td>
                    
                </tr>
                </table>
                </td>
                </tr>
                </table>
                
                
                <br />
                <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="99%" align="center">
                <tr bordercolor="#839abf">
                <td>
                <table width="100%" align="center" cellpadding="3" cellspacing="3" border="0">
                <tr>
                    <td class="copy10grey" align="right" width="17%">
                        Contact Name:    
                    </td>
                    <td width="32%">

                       &nbsp; <asp:TextBox ID="txtContactName" Width="200" MaxLength="50" CssClass="copy10grey" runat="server"></asp:TextBox>
                
                    </td>
                    <td width="2%">
                    &nbsp;
                    </td>
                    <td class="copy10grey" align="right" width="17%">
                        Contact Phone:    
                    </td>
                    <td width="32%">

                       &nbsp; <asp:TextBox ID="txtContactPhone"  onkeydown="mask(event,this)" onkeyup="mask(event,this)"  MaxLength="12" Width="200" CssClass="copy10grey" runat="server"></asp:TextBox>
                
                    </td>


                </tr>
                <tr>
                    <td class="copy10grey" align="right" width="17%">
                        Street Address:    
                    </td>
                    <td width="32%">

                      &nbsp;   
                      <asp:TextBox ID="txtStreetAdd" Width="200" MaxLength="100" CssClass="copy10grey" runat="server"></asp:TextBox>
                      <%--<asp:Label ID="lblStreetAdd" runat="server" CssClass="copy10grey"></asp:Label>   --%>
                    </td>
                    <td width="2%">
                    &nbsp;
                    </td>
                    <td class="copy10grey" align="right" width="17%">
                        Address2:    
                    </td>
                    <td width="32%">

                      &nbsp;   
                      <asp:TextBox ID="txtAddress2" Width="200" MaxLength="100" CssClass="copy10grey" runat="server"></asp:TextBox>
                      <%--<asp:Label ID="lblStreetAdd" runat="server" CssClass="copy10grey"></asp:Label>   --%>
                    </td>
                </tr>
                <tr>
                    
                </tr>
                <tr>
                    <td class="copy10grey" align="right" width="17%">
                        City:    
                    </td>
                    <td width="32%">

                       &nbsp;  
                       <asp:TextBox ID="txtCity" Width="200" MaxLength="50" onkeypress="return alphaNumericCheck(event);" CssClass="copy10grey" runat="server"></asp:TextBox>
                       
                    </td>
                    <td width="2%">
                    &nbsp;
                    </td>
                    <td class="copy10grey" align="right" width="17%">
                        State:    
                    </td>
                    <td width="32%" class="copy10grey" >

                       &nbsp;  
                       <%--<asp:dropdownlist id="dpState" tabIndex="6" runat="server" cssclass="copy10grey">
                       </asp:dropdownlist>--%>
                       <asp:TextBox ID="txtState" Width="30" MaxLength="2" CssClass="copy10grey" runat="server"></asp:TextBox>

                       &nbsp;  &nbsp;  &nbsp; &nbsp;Zip:&nbsp;&nbsp;&nbsp;&nbsp;  
                       <asp:TextBox ID="txtZip" Width="90" onkeypress="return alphaNumericCheck(event);"  MaxLength="6" CssClass="copy10grey" runat="server"></asp:TextBox>
                       
                       <%--<asp:Label ID="lblState" runat="server" CssClass="copy10grey"></asp:Label>   --%>
                    </td>
                </tr>
                <tr>
                    
                </tr>
                <%--<tr>
                    <td class="copy10grey" align="right" width="40%">
                        Sent ESN:    
                    </td>
                    <td>

                       &nbsp;  <asp:Label ID="lblSentESN" runat="server" CssClass="copy10grey"></asp:Label>   
                    </td>
                </tr>
                <tr>
                    <td class="copy10grey" align="right" width="40%">
                        Sent ASN:    
                    </td>
                    <td>

                       &nbsp;  <asp:Label ID="lblSentASN" runat="server" CssClass="copy10grey"></asp:Label>   
                    </td>
                </tr>--%>
                </table>
                </td>
                </tr>
                </table>

                <br />
                <table width="100%" align="center">
                <tr>
                    <td align="center">
                        <asp:Button ID="btnSubmit" runat="server"  OnClick="btnEditPO_Click" Text="Submit"  CssClass="button" />&nbsp;
                         <asp:Button ID="btnCancel2" runat="server" Text="Cancel" CssClass="button" OnClientClick="closeEditDialog()"  />
                    </td>
                </tr>
                </table>
               
                         </asp:PlaceHolder>
						
						
					</ContentTemplate>
							
				</asp:UpdatePanel>
        </div>    
        
        <div id="divEditPOD" style="display:none">
					
				<asp:UpdatePanel ID="upnlEditPOD" runat="server">
					<ContentTemplate>
                   
						<asp:PlaceHolder ID="phrEditPOD" runat="server">
                            <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="99%" align="center">
                <tr bordercolor="#839abf">
                <td>
                <table width="100%" align="center" cellpadding="3" cellspacing="3">
                <tr>
                    <td class="copy10grey" align="right" width="40%">
                        Product Code:    
                    </td>

                    <td>
                        &nbsp; <asp:Label ID="lblItemCode" runat="server" CssClass="copy10grey"></asp:Label>   
                    </td>

                </tr>
                <tr>
                    <td class="copy10grey" align="right" width="40%">
                        Qty:    
                    </td>
                    <td>
                       &nbsp; <asp:Label ID="lblQty" runat="server" CssClass="copy10grey"></asp:Label>   
                    </td>

                </tr>
               
                <tr>
                    <td class="copy10grey" align="right" width="40%">
                        ESN:    
                    </td>
                    <td>

                       &nbsp; <asp:TextBox ID="txtESNs" onkeypress="JavaScript:return alphaOnly(event);" Width="200" CssClass="copy10grey" runat="server"></asp:TextBox>
                
                    </td>
                </tr>
                <tr>
                    <td class="copy10grey" align="right" width="40%">
                        MDN:    
                    </td>
                    <td>

                       &nbsp; <asp:TextBox ID="txtMDN" Width="200" CssClass="copy10grey" runat="server"></asp:TextBox>
                
                    </td>
                </tr>
                <tr>
                    <td class="copy10grey" align="right" width="40%">
                        MSID:    
                    </td>
                    <td>

                       &nbsp; <asp:TextBox ID="txtMSID" Width="200"  CssClass="copy10grey" runat="server"></asp:TextBox>
                
                    </td>
                </tr>
                <tr>
                    <td class="copy10grey" align="right" width="40%">
                        MSL Number:    
                    </td>
                    <td>

                       &nbsp; <asp:TextBox ID="txtMSLNo" Width="200" onkeypress="JavaScript:return alphaOnly(event);" CssClass="copy10grey" runat="server"></asp:TextBox>
                
                    </td>
                </tr>
                <tr>
                    <td class="copy10grey" align="right" width="40%">
                        PassCode:    
                    </td>
                    <td>

                       &nbsp;  <asp:Label ID="lblPassCode" runat="server" CssClass="copy10grey"></asp:Label>   
                    </td>
                </tr>
                <tr>
                    <td class="copy10grey" align="right" width="40%">
                        UPC:    
                    </td>
                    <td>

                       &nbsp;  <asp:Label ID="lblUPC" runat="server" CssClass="copy10grey"></asp:Label>   
                    </td>
                </tr>
                 <tr>
                    <td align="right" width="40%">
                        <asp:Label ID="lblStatuss" CssClass="copy10grey" runat="server" Text="Status:"></asp:Label>
                    </td>    
                    <td>
                     &nbsp;    <asp:DropDownList ID="ddlPODStatus" runat="server" class="copy10grey">
                                <asp:ListItem Text="" Value=""></asp:ListItem>
                                <asp:ListItem Text="Pending" Value="1"></asp:ListItem>
<asp:ListItem Text="In-Process" Value="8"></asp:ListItem>
                                <asp:ListItem Text="Processed" Value="2"></asp:ListItem>
                                <asp:ListItem Text="Shipped" Value="3"></asp:ListItem>
                                <asp:ListItem Text="Closed" Value="4"></asp:ListItem>
                                <asp:ListItem Text="Return" Value="5"></asp:ListItem>
<asp:ListItem Text="Cancel" Value="9"></asp:ListItem>
<asp:ListItem Text="On Hold" Value="6"></asp:ListItem>
                                <asp:ListItem Text="Out of Stock" Value="7"></asp:ListItem>
                                
				
                            	</asp:DropDownList>
                            <asp:Label ID="lblPODStatus" CssClass="copy10grey" runat="server" ></asp:Label>
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
                         <asp:Button ID="btnSubmitPOD" runat="server" OnClick="btnEditPOD_Click" Text="Submit" CssClass="button" />&nbsp;
                         <asp:Button ID="btnCancelPOD" runat="server" Text="Cancel" OnClientClick="closeEditPODDialog()" CssClass="button"  />
                    </td>
                </tr>
                </table>
                
                        </asp:PlaceHolder>
						
						
					</ContentTemplate>
							
				</asp:UpdatePanel>
        </div>    

        <div id="divDownload" style="display:none">
					
				<asp:UpdatePanel ID="upnlDnl" runat="server">
					<ContentTemplate>
                   
						<asp:PlaceHolder ID="phrDnl" runat="server">
                            <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="95%" align="center">
                    <tr bordercolor="#839abf">
                        <td>
                        <table cellSpacing="5" cellPadding="5" width="100%"  >
                        <tr>
                            <td>
                                
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td class="copy10grey" width="33%" align="right">
                                Download Fulfillment Data:    
                            </td>
                            <td width="34%" align="center">
                                <asp:DropDownList ID="dpDownloadDataList" runat="server" class="copy10grey">
                                    <asp:ListItem Text="" Value=""></asp:ListItem>
                                    <asp:ListItem Text="Download Fulfillment" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Download Fulfillment for ESN" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Download Fulfillment Detail" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="Download Fulfillment for ESN(Excel)" Value="4"></asp:ListItem>
                                    <asp:ListItem Text="Download Fulfillment Header" Value="5"></asp:ListItem>
                                    <asp:ListItem Text="Download Fulfillment Tracking#" Value="6"></asp:ListItem>
                                </asp:DropDownList>   
                            </td>
                            <td width="33%" align="left">
                                <asp:Button ID="btnDownload" runat="server" OnClientClick="return IsValidateDnw();" OnClick="btnDownload_Click" Text="Download" CssClass="button" />
                                <%--<asp:Button ID="btnClose3" runat="server" Text="Cancel" CssClass="button" />--%>

                            </td>

                        </tr>
                        <tr>
                            <td>
                     <br />
                            </td>
                        </tr>
                        </table>
                       </td>
                    </tr>
                 </table>
                         </asp:PlaceHolder>
						
						
					</ContentTemplate>
							
				</asp:UpdatePanel>
        
    </div>

    <div id="winVP">
    <asp:UpdatePanel ID="UpdatePanel1"  runat="server" UpdateMode="Conditional" >
     <ContentTemplate>
     <table cellspacing="0" cellpadding="0"  border="0" align="center" width="100%">
	<tr>
	    <td align="center">
      	

    <br />
    
    <table  cellSpacing="1" cellPadding="1" width="100%" align="center">
        <tr>
			<td colSpan="6" bgcolor="#dee7f6" class="button" align="left">&nbsp;&nbsp;Fulfillment Search
			</td>
        </tr>
        <tr><td align="left"><asp:Label ID="lblMsg" runat="server" CssClass="errormessage"></asp:Label></td></tr>
    </table>
    <table  cellSpacing="1" cellPadding="1" width="100%">
                <tr><td class="copy10grey" align="left">
                - Please select your search
                  criterial to narrow down the search and record selection.<br />
                - Atleast one search criteria should be selected.<br />
                <%-- - Maximum of top 5000 records will be shown, if you want more please contact with Lan Global administrator.<br />--%>
                - Maximum of last one year records will be shown if not From and To dates are given.
                
                </td></tr>
    </table>

    <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="99%" >

           <tr bordercolor="#839abf">
                <td>
                <table cellSpacing="1" cellPadding="1" width="100%"  >
                <tr width="6">
                <td width="6">
                    &nbsp;
                </td>
                </tr>
            <tr>
                <td align="right" class="copy10grey" width="15%">
                    Fulfillment#:</td>
                <td width="1%">
                </td>
                <td align="left"  width="29%">
                    <asp:TextBox ID="txtPONum"  Width="80%" onkeypress="JavaScript:return alphaOnly(event);" MaxLength="20" runat="server" CssClass="copy10grey" ></asp:TextBox></td>
                <td align="right" class="copy10grey" width="15%">
                    Contact Name:</td>
                
                <td width="1%"></td>
                <td align="left" width="29%">
                    <asp:TextBox ID="txtCustName" MaxLength="20" runat="server" CssClass="copy10grey"  Width="80%"></asp:TextBox></td>
            </tr>
            <tr>
                <td align="right" class="copy10grey" >
                    Fulfillment Date From:</td>
                <td>
                </td>
                <td align="left">
                    <asp:TextBox ID="txtFromDate" runat="server" CssClass="copy10grey" MaxLength="15"  Width="80%"></asp:TextBox>
                    <img id="imgFromtDate" alt="" onclick="document.getElementById('<%=txtFromDate.ClientID%>').value = ''; displayCalendar(document.getElementById('<%=txtFromDate.ClientID%>'),'mm/dd/yyyy',this,true);" src="../fullfillment/calendar/sscalendar.jpg" />
                </td>
                    <td align="right" class="copy10grey">Fulfillment Date To:</td>
                <td></td>
                <td align="left">
                    <asp:TextBox ID="txtToDate" runat="server" CssClass="copy10grey" MaxLength="15"  Width="80%"></asp:TextBox>
                    <img id="imgToDate"  alt="" onclick="document.getElementById('<%=txtToDate.ClientID%>').value = ''; displayCalendar(document.getElementById('<%=txtToDate.ClientID%>'),'mm/dd/yyyy',this,true);" src="../fullfillment/calendar/sscalendar.jpg" />
                
                </td>               
            </tr>
            <tr>
                <td align="right" class="copy10grey">
                    Shipping Date From:</td>
                <td>
                </td>
                <td align="left">
                    <asp:TextBox ID="txtShipFrom" runat="server" CssClass="copy10grey" MaxLength="15"  Width="80%"></asp:TextBox>
                    <img id="img1" alt="" onclick="document.getElementById('<%=txtShipFrom.ClientID%>').value = ''; displayCalendar(document.getElementById('<%=txtShipFrom.ClientID%>'),'mm/dd/yyyy',this,true);" src="../fullfillment/calendar/sscalendar.jpg" />
                </td>
                    <td align="right" class="copy10grey">Shipping Date To:</td>
                <td></td>
                <td align="left">
                    <asp:TextBox ID="txtShipTo" runat="server" CssClass="copy10grey" MaxLength="15"  Width="80%"></asp:TextBox>
                    <img id="img2"  alt="" onclick="document.getElementById('<%=txtShipTo.ClientID%>').value = ''; displayCalendar(document.getElementById('<%=txtShipTo.ClientID%>'),'mm/dd/yyyy',this,true);" src="../fullfillment/calendar/sscalendar.jpg" />
                
                </td>               
            </tr>            
            <tr>
                <td align="right" class="copy10grey">
                    Fulfillment Status:</td>
                <td></td>
                <td align="left">
                    <asp:DropDownList ID="dpStatusList" runat="server" class="copy10grey"  Width="81%">
                                <asp:ListItem Text="" Value=""></asp:ListItem>
                                <asp:ListItem Text="Pending" Value="1"></asp:ListItem>
<asp:ListItem Text="In-Process" Value="8"></asp:ListItem>
                                <asp:ListItem Text="Processed" Value="2"></asp:ListItem>
                                <asp:ListItem Text="Shipped" Value="3"></asp:ListItem>
                                <asp:ListItem Text="Closed" Value="4"></asp:ListItem>
                                <asp:ListItem Text="Return" Value="5"></asp:ListItem>
                                <asp:ListItem Text="Cancel" Value="9"></asp:ListItem>
<asp:ListItem Text="On Hold" Value="6"></asp:ListItem>
                                <asp:ListItem Text="Out of Stock" Value="7"></asp:ListItem>

                            </asp:DropDownList>
                </td>
                <asp:Panel ID="pnlCompany" runat="server" Visible="false">
                <td align="right" class="copy10grey">
                    Customer:</td>
                <td></td>
                <td align="left">
                    <asp:DropDownList ID="dpCompany" runat="server" class="copy10grey"  Width="81%">
                                
                            </asp:DropDownList>
                </td>
                </asp:Panel>
            </tr>
            
            <tr>
                <td align="right" class="copy10grey">
                    ESN:</td>
                <td></td>
                <td align="left">
                    <asp:TextBox ID="txtEsn" Width="80%" onkeypress="JavaScript:return alphaOnly(event);" runat="server"  CssClass="copy10grey" MaxLength="20"></asp:TextBox>
                </td>
                  <td align="right" class="copy10grey">
                    AVSO#:</td>
                <td></td>
                <td align="left">
                    <asp:TextBox ID="txtAvNumber"  Width="80%" onkeypress="JavaScript:return alphaOnly(event);" runat="server"  CssClass="copy10grey" MaxLength="20"></asp:TextBox>
                </td>
            </tr> 

            <tr>
                <td align="right" class="copy10grey">
                    MSL Number:</td>
                <td></td>
                <td align="left">
                    <asp:TextBox ID="txtMslNumber" runat="server" Width="80%" onkeypress="JavaScript:return alphaOnly(event);" CssClass="copy10grey" MaxLength="20"></asp:TextBox>
                </td>
                  <%--<td align="right" class="copy10grey">
                    Phone Category:</td>
                <td></td>
                <td align="left">
                    <asp:DropDownList ID="dpPhoneCategory"  Width="81%" runat="server"  class="copy10grey">
                                <asp:ListItem Text="" Value=""></asp:ListItem>
                                <asp:ListItem Text="Hot" Value="H"></asp:ListItem>
                                <asp:ListItem Text="Cold" Value="C"></asp:ListItem>
                    </asp:DropDownList>
                </td>--%>
<td align="right" class="copy10grey">
                    Store ID:</td>
                <td></td>
                <td align="left">
                    <asp:TextBox ID="txtStoreID" runat="server" Width="80%" onkeypress="JavaScript:return alphaOnly(event);"  CssClass="copy10grey" MaxLength="20"></asp:TextBox>
                     <asp:DropDownList ID="ddlUserStores"  Width="80%" runat="server"  class="copy10grey">
                                
                    </asp:DropDownList>
                </td> 

            </tr> 
            
            <tr>
                <td align="right" class="copy10grey">
                    Product Code:</td>
                <td></td>
                <td align="left">
                    <asp:TextBox ID="txtItemCode" runat="server" Width="80%" onkeypress="JavaScript:return alphaOnly(event);" CssClass="copy10grey" MaxLength="20"></asp:TextBox>
                </td>
	
<td></td>
	<td></td>
	<td></td>
                                 
            </tr>
            <tr>
               <td align="right" class="copy10grey">
                    FM-UPC:</td>
                <td></td>
                <td align="left">
                    <asp:TextBox ID="txtFmUpc" Width="80%" runat="server" onkeypress="JavaScript:return alphaOnly(event);" CssClass="copy10grey" MaxLength="35"></asp:TextBox>
                </td>
            </tr>
            <tr>
                 <td align="right" class="copy10grey">
                    Zone:</td>
                <td></td>
                <td align="left">
                    <asp:DropDownList ID="dpZone" Width="81%" runat="server"  class="copy10grey">
                        <asp:ListItem Text="" Value=""></asp:ListItem> 
                        <asp:ListItem Text="Zone1" Value="1"></asp:ListItem> 
                        <asp:ListItem Text="Zone2" Value="2"></asp:ListItem> 
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="6" align="left"><asp:CheckBox ID="chkDownload"  runat="server" Text="Download selected records only" CssClass="copy10grey" /></td>
            </tr>
            <tr><td colspan="6"><hr /></td></tr>
            <tr><td colspan="6" align="center">
            <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Search Fulfillment" OnClick="btnSearch_Click" />&nbsp;
            &nbsp;<asp:Button ID="btnCancel" runat="server" CssClass="button" Text="Cancel Search" OnClick="btnCancel_Click" />
            &nbsp;<asp:Button ID="btnChangeStatus" runat="server" CssClass="button" Text="Change Status" OnClick="btnChangeStatus_Click" Visible="False" />
            &nbsp;<asp:Button ID="btnDownloadData"  runat="server" Visible="False" CssClass="button" Text="Download Fulfillment Data " OnClientClick="openDownloadDialog('Download Fulfillment Data','btnDownloadData')" />
            
            <%--<br />--%>
            &nbsp;<asp:Button ID="btnDownPO" runat="server" CssClass="button" OnClick= "btnDownPO_Click" Text="Download Fulfillment" Visible="False"  />&nbsp;
            
            <asp:Button ID="btnDown" runat="server" CssClass="button" OnClick= "btnDown_Click" Text="Download Fulfillment for ESN" Visible="False" />           
            <asp:Button ID="btnUpload" runat="server" CssClass="button"  Text="Upload ESN" OnClick= "btnUpload_Click" Visible="False" />
            
            <asp:Button ID="btnSendESN" runat="server" CssClass="button" Text="Send ESN to iWireless" Visible="False" OnClick="btnSendESN_Click" />
            <asp:Button ID="Button1" runat="server" CssClass="button" Text="Send ASN to iWireless" Visible="False" OnClick="btnSendESN_Click" />
       	    <asp:Button ID="btnPoDetail" runat="server" CssClass="button" Text="Download Fulfillment Details" Visible="False" CausesValidation="False" OnClick="btnPoDetail_Click" Width="253px"  />
	        <asp:Button ID="btnPoDetailTrk" runat="server" CssClass="button" Text="Download Fulfillment Detail ||" Visible="False" CausesValidation="False" OnClick="btnPoDetailTrk_Click" />
	        <br />
	        <asp:Button ID="btnEsn_Excel" runat="server" CssClass="button" OnClick= "btnEsn_Excel_Click" Text="Download Fulfillment for ESN (Excel)" Visible="False" />&nbsp;
	        <asp:Button ID="btnPoHeader" runat="server" CssClass="button" OnClick= "btnPoHeader_Click" Text="Download Fulfillment Header" Visible="False" />&nbsp;
	        
	        </td></tr>
	                
            <tr id="trUpload" runat="server" visible="false">
                <td align="center" colspan="6" style="height: 48px">
                    <table width="40%" border="2"  align="center" BackColor="Gainsboro" cellpadding="0" cellspacing="0">
                        <tr>
                            <td width="70%"><asp:FileUpload ID="flnUpload" runat="server" CssClass="txfield1" Width="100%" /></td>
                            <td align="center"><asp:Button ID="btnUpd" runat="server" Text="Upload" CssClass="button" OnClick="btn_UpdClick" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
<tr id="trStatus" runat="server" visible="false" valign="middle">
                <td align="center" colspan="6">
                <table width="40%" border="1" align="center" BackColor="Gainsboro" cellpadding="0" cellspacing="0">
                        <tr valign="middle">
                        <td valign="middle">
                    <table width="100%" border="0" align="center" BackColor="Gainsboro" cellpadding="0" cellspacing="0">
                        <tr valign="middle" >
                            <td width="70%" class="copy10grey">Status: 
                            <asp:DropDownList ID="dpStatus" runat="server">
                                <asp:ListItem Text="" Value=""></asp:ListItem>
                                <asp:ListItem Text="Pending" Value="1"></asp:ListItem>
<asp:ListItem Text="In-Process" Value="8"></asp:ListItem>
                                <asp:ListItem Text="Processed" Value="2"></asp:ListItem>
                                <asp:ListItem Text="Shipped" Value="3"></asp:ListItem>
                                <asp:ListItem Text="Closed" Value="4"></asp:ListItem>
                                <asp:ListItem Text="Return" Value="5"></asp:ListItem>
                                <asp:ListItem Text="Cancel" Value="9"></asp:ListItem>
                                <asp:ListItem Text="Deleted" Value="6"></asp:ListItem>
                                <%--<asp:ListItem Text="Out of Stock" Value="7"></asp:ListItem>--%>

                            </asp:DropDownList></td>
                            <td align="center" valign="bottom">
                                <asp:Button ID="btbSubmitStatus" runat="server" Text="Submit" CssClass="button"  OnClientClick="return ValidateStatus();" 
                                 OnClick="btn_UpdStatusClick" />
                            </td>
                        </tr>
                    </table>
                    </td>
                    </tr>
                    </table>
                </td>
            </tr>
            <tr id="trPopup" runat="server" visible="false">
                <td align="center" colspan="6">
                    
                </td>
            </tr>
            </table>
            	</td>
            </tr>
	    </table>

        <table cellSpacing="1" cellPadding="1" width="100%" >
            <tr><td style="width: 1032px" align="left" >&nbsp;
                &nbsp;&nbsp;&nbsp;
             <asp:LinkButton ID="lnkSumary"  Visible="false" CssClass="copy11link"  OnClientClick="return POSummary();" runat="server" Text="Fulfillment Summary"></asp:LinkButton>
             <%--<asp:HyperLink ID="lnkSummary" Visible="false" CssClass="copy11link" runat="server" Text="PO Summary"></asp:HyperLink>&nbsp;
            --%>
            </td></tr>
            <tr><td style="width: 1032px">
                    <table cellSpacing="1" cellPadding="1" width="80%"> 
                        <tr><td>
			                <asp:Panel ID="pnlSummary" runat="server" Width="100%" Visible="False" BackColor="#dee7f6">
			                    <table width="100%" border="1">
			                        <tr align="left">
			                            <td align="right" class="copy10grey" width="30%" >Total Fulfillment:</td>
                                        <td width="70%"><asp:Label ID="lblTotalPOs"  CssClass="copy10grey" runat="server"></asp:Label></td>
			                        </tr>
			                        <tr>
			                            <td align="right" class="copy10grey" >Pending Fulfillment:</td>
                                        <td><asp:Label ID="lblPendingPOs" CssClass="copy10grey"  runat="server"></asp:Label></td>
			                        </tr>
			                        <tr>
			                            <td align="right" class="copy10grey" >Processed Fulfillment:</td>
                                        <td><asp:Label ID="lblProcessedPOs" CssClass="copy10grey"  runat="server"></asp:Label></td>
			                        </tr>
			                        <tr>
			                            <td align="right" class="copy10grey" style="height: 2px" >Shipped Fulfillment:</td>
                                        <td style="height: 2px"><asp:Label ID="lblShippedPOs" CssClass="copy10grey"  runat="server"></asp:Label></td>
			                        </tr>			        			        
			                    </table>
			                </asp:Panel>
			            </td></tr>
			            <tr>
			                <td>
			                    <asp:Panel ID="pnlItem" runat="server">
			                    
			                    </asp:Panel>
			                </td>
			            </tr>
			        </table>
            </td></tr>
            
        <tr>
            <td align="right">
                
                <asp:Label ID="lblCount" CssClass="copy10grey" runat="server" ></asp:Label>
            </td>
        </tr>
        <tr><td>
            <asp:LinkButton ID="btnRefreshGrid" CausesValidation="false" OnClick="btnRefreshGrid_Click" style="display:none" runat="server"></asp:LinkButton>		
	
           <asp:GridView ID="gvPOQuery" OnPageIndexChanging="gridView_PageIndexChanging"    AutoGenerateColumns="false"  
                DataKeyNames="PurchaseOrderID"  Width="100%"  
            ShowFooter="false" runat="server" GridLines="Both" OnRowDataBound="GridView1_RowDataBound" 
            PageSize="20" AllowPaging="true" OnRowCommand = "GridView1_RowCommand"
            BorderStyle="Outset" OnRowDeleting = "GridView1_RowDeleting" OnRowDeleted = "GridView1_RowDeleted" 
            AllowSorting="false" > 
            <RowStyle BackColor="Gainsboro" />
            <AlternatingRowStyle BackColor="white" />
            <HeaderStyle  CssClass="button" ForeColor="white"/>
            <FooterStyle CssClass="white"  />
            <PagerStyle ForeColor="#636363" CssClass="copy10grey" HorizontalAlign="Left" />
            <Columns>
                <asp:ButtonField   ButtonType="Image"  ImageUrl ="../images/plus.gif"
                    CommandName="sel"  />
                <asp:TemplateField ItemStyle-CssClass="copy10grey"  ItemStyle-Width="3%">
                    <ItemTemplate>
                        <asp:CheckBox ID="chk"  runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>  
<asp:TemplateField HeaderText="AVSO#" SortExpression="AerovoiceSalesOrderNumber"  ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="copy10grey" ItemStyle-Width="7%">
                    <ItemTemplate>
                    <%# Eval("AerovoiceSalesOrderNumber") %>
                    </ItemTemplate>
                </asp:TemplateField>               
                
                <asp:TemplateField HeaderText="Fulfillment#" SortExpression="PurchaseOrderNumber" ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                    <ItemTemplate>
                        <asp:Label ID="lblPoNum" runat="server" Text='<%# Eval("PurchaseOrderNumber") %>'></asp:Label>                        
<asp:HiddenField ID="hdnCANo" runat="server" Value='<%# Eval("CustomerAccountNumber") %>' />                            
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Fulfillment Date"  SortExpression="PurchaseOrderDate" ItemStyle-CssClass="copy10grey"  ItemStyle-Width="5%">
                    <ItemTemplate><%# DataBinder.Eval(Container.DataItem, "PurchaseOrderDate", "{0:d}") %></ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Customer" SortExpression="CustomerName"  ItemStyle-HorizontalAlign="left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                    <ItemTemplate>
                    <%# Eval("CustomerName")%>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Contact Name" SortExpression="ContactName" ItemStyle-CssClass="copy10grey" ItemStyle-Width="15%">
                    <ItemTemplate><%# Eval("Shipping.ContactName")%></ItemTemplate>
                    
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Contact Phone" SortExpression="ContactPhone" ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                    <ItemTemplate><%# Eval("Shipping.ContactPhone") %></ItemTemplate>
                    
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="Ship Via" SortExpression="ShipToBy" ItemStyle-CssClass="copy10grey"  ItemStyle-Width="7%">
                    <ItemTemplate>
                    
<%#  Convert.ToString(Eval("Tracking.ShipToBy")).ToUpper() %>	
                    
                    </ItemTemplate>
                    
                </asp:TemplateField>
               
               

                <asp:TemplateField HeaderText="Tracking#" SortExpression="ShipToTrackingNumber" ItemStyle-CssClass="copy10grey"  ItemStyle-Width="7%">
                    <ItemTemplate><%# Eval("Tracking.ShipToTrackingNumber")%></ItemTemplate>
                    <%--<EditItemTemplate>
                        <asp:TextBox ID="txtTrack" CssClass="copy10grey"  MaxLength="200" TextMode="MultiLine" Text='<%# Eval("Tracking.ShipToTrackingNumber") %>' runat="server"></asp:TextBox>
                    </EditItemTemplate>--%>
                </asp:TemplateField>   
               
               
                <asp:TemplateField HeaderText="Shipping Date" SortExpression="ShipToDate"  ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                    <ItemTemplate>
<asp:Label ID="lblShippDate" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "Tracking.ShipToDate", "{0:d}"))=="1/1/0001"? "": DataBinder.Eval(Container.DataItem, "Tracking.ShipToDate", "{0:d}")%>'></asp:Label>                        
                    
                    </ItemTemplate>
                </asp:TemplateField>                                  
                
               
                
                <asp:TemplateField HeaderText="Store ID" SortExpression="StoreID"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                    <ItemTemplate><%#Eval("StoreID")%></ItemTemplate>
                </asp:TemplateField> 

                <asp:TemplateField HeaderText="Street Address" SortExpression="ShipTo_Address"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="15%">
                    <ItemTemplate><%#Eval("Shipping.ShipToAddress")%> <%#Eval("Shipping.ShipToAddress2")%></ItemTemplate>
                </asp:TemplateField> 
                <asp:TemplateField HeaderText="State"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                    <ItemTemplate>


<%#  Convert.ToString(Eval("Shipping.ShipToState")).ToUpper() %>

</ItemTemplate>
                </asp:TemplateField>                                              

                <asp:TemplateField HeaderText="Zip"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                    <ItemTemplate><%#Eval("Shipping.ShipToZip")%></ItemTemplate>
                </asp:TemplateField>  

                <asp:TemplateField HeaderText="Status"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                    <ItemTemplate>
<table cellpadding="3" cellspacing="3" style="width:100%; background-color:<%#Eval("StatusColor")%>; height:100%">
                    <tr>
                    <td>
                        <%#Eval("PurchaseOrderStatus")%>
                    </td>
                    </tr>
                    </table>
                    
                    <asp:HiddenField ID="hdnStatus" Value='<%#Eval("PurchaseOrderStatusID")%>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>   
                <%--
                <asp:TemplateField HeaderText="Sent ESN" SortExpression="sentesn"  ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                    <ItemTemplate><%#Eval("sentesn")%></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Sent ASN" SortExpression="sentesn"  ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                    <ItemTemplate>
                    
                    
                    <%#Eval("sentasn ")%></ItemTemplate>
                </asp:TemplateField>--%>	    
			   <%-- <asp:CommandField HeaderText="Edit" ShowEditButton="True" HeaderStyle-CssClass="button" ControlStyle-CssClass="linkgrid"  ItemStyle-HorizontalAlign="Center"/>
			    --%>
                <asp:TemplateField HeaderText=""  ItemStyle-HorizontalAlign="Center"  >
                    <ItemTemplate>
                    
                    <table>
                        <tr>
                        <td>
                            <asp:ImageButton ID="imgPO"  ToolTip="View PO" OnCommand="imgViewPO_OnCommand"  CausesValidation="false" CommandArgument='<%# Eval("PurchaseOrderID") %>' ImageUrl="~/Images/view.png"  runat="server" />
                        
                        </td>
                        <td>
                        <%--
                        <a class="copy10grey" href='<%# "/Reports/FulfillmentReport.aspx?po=" + Eval("PurchaseOrderNumber")%>&cid=<%# Eval("Companyid")%>'>
                            <img src="/Images/i_view.gif" alt="PO History" border="0" title="PO History" />
                        </a>--%>
                            <asp:ImageButton ID="imgHistory"  ToolTip="Fulfillment History" OnCommand="imgPOHistory_OnCommand"  CausesValidation="false" CommandArgument='<%# Eval("PurchaseOrderID") %>' ImageUrl="~/Images/history.png"  runat="server" />
                        
                        
                        </td>
                        <td>
                        <asp:ImageButton ToolTip="Edit PO" CausesValidation="false" OnCommand="imgEditPO_OnCommand" CommandArgument='<%# Eval("PurchaseOrderID") %>' 
                        ImageUrl="~/Images/edit.png" ID="imgEdit"  runat="server" />
                        
                        </td>
                        <td>
                        <asp:ImageButton ID="imgDelPo" runat="server"  CommandName="Delete" AlternateText="Delete PO" ImageUrl="~/images/delete.png" />
                        </td>
                        </tr>
                        </table>
                        
                        
                        
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField  >
			        <ItemTemplate>
			            <tr>
                            <td colspan="100%">
                                <div id="div<%# Eval("PurchaseOrderID") %>"  >
                                    <asp:GridView ID="GridView2"  BackColor="White" Width="100%" Visible="False"
                                        AutoGenerateColumns="false" Font-Names="Verdana" runat="server" DataKeyNames="PodID"
                                        OnRowUpdating = "GridView2_RowUpdating"
                                        OnRowCommand = "GridView2_RowCommand" OnRowEditing = "GridView2_RowEditing" GridLines="Both"
                                        OnRowUpdated = "GridView2_RowUpdated" OnRowCancelingEdit = "GridView2_CancelingEdit" OnRowDataBound = "GridView2_RowDataBound"
                                        OnRowDeleting = "GridView2_RowDeleting" OnRowDeleted = "GridView2_RowDeleted"
                                        BorderStyle="Double" BorderColor="#0083C1">
                                        <RowStyle BackColor="Gainsboro" />
                                        <AlternatingRowStyle BackColor="white" />
                                        <HeaderStyle  CssClass="button" ForeColor="white"/>
                                        <FooterStyle CssClass="white"  />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Product Code" SortExpression="Item_Code" ItemStyle-CssClass="copy10grey"  ItemStyle-Width="20%">
                                                <ItemTemplate><asp:Label ID="lblItemCode" runat="server" Text='<%# Eval("ItemCode")%>'></asp:Label></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="UPC" SortExpression="UPC"  ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                                <ItemTemplate><%#Eval("UPC")%></ItemTemplate>
                                            </asp:TemplateField>
                                                                                                                                 
                                            
                                            <asp:TemplateField HeaderText="Qty" SortExpression="Qty"  ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                                                <ItemTemplate><%#Eval("Quantity")%></ItemTemplate>
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField HeaderText="ESN" SortExpression="ESN" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEsn" CssClass="copy10grey" Text='<%# Eval("ESN") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtEsn" CssClass="copy10grey" MaxLength="35"  Text='<%# Eval("ESN") %>' runat="server"></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField HeaderText="MDN" SortExpression="MdnNumber"  ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                                <ItemTemplate><%#Eval("MdnNumber")%></ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtMdn" CssClass="copy10grey" MaxLength="30"  Text='<%# Eval("MdnNumber") %>' runat="server"></asp:TextBox>
                                                </EditItemTemplate>                                                
                                            </asp:TemplateField>
                                        
                                            <asp:TemplateField HeaderText="MSID" SortExpression="MsID"  ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                                <ItemTemplate><%#Eval("MsID")%></ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtMsid" CssClass="copy10grey" MaxLength="30"  Text='<%# Eval("MsID") %>' runat="server"></asp:TextBox>
                                                </EditItemTemplate>                                                 
                                            </asp:TemplateField>
                                            

                                            <asp:TemplateField HeaderText="MslNumber" SortExpression="MslNumber" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                    <%#Eval("MslNumber")%>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtMslNumber" CssClass="copy10grey" MaxLength="30"  Text='<%# Eval("MslNumber") %>' runat="server"></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField HeaderText="Passcode" SortExpression="PassCode"  ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                                <ItemTemplate><%#Eval("PassCode")%></ItemTemplate>
                                            </asp:TemplateField>

                                            
                                             <asp:TemplateField HeaderText="FM-UPC" SortExpression="FMUPC" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                    <%#Eval("FMUPC")%>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtFMUPC" CssClass="copy10grey" MaxLength="50"  Text='<%# Eval("FMUPC") %>' runat="server"></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                                                                                                                        
                                            <%--<asp:TemplateField HeaderText="Phone Type" SortExpression="PhoneCategory" Visible="false"  ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                                <ItemTemplate><%#Eval("PhoneCategory")%></ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <%--<asp:TemplateField  ItemStyle-Width="10%">  
                                            </asp:TemplateField> --%>   

                                            <asp:TemplateField HeaderText="Status" SortExpression="StatusID"  ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                <%# Convert.ToInt32(Eval("StatusID"))==1 ? "Pending": Convert.ToInt32(Eval("StatusID"))==2 ? "Processed": Convert.ToInt32(Eval("StatusID"))==3 ? "Shipped": Convert.ToInt32(Eval("StatusID"))==4 ? "Closed": Convert.ToInt32(Eval("StatusID"))==5 ? "Return": Convert.ToInt32(Eval("StatusID"))==9 ? "Cancel": Convert.ToInt32(Eval("StatusID"))==6 ? "On Hold": Convert.ToInt32(Eval("StatusID"))==7 ? "Out of Stock": "Pending"%>
                                              <%-- <br /> <%#Eval("PODStatus")%>--%>
                                                
                                                </ItemTemplate>
                                            </asp:TemplateField>                                                     
			                                <%-- <asp:CommandField HeaderText="Edit" ShowEditButton="True"  HeaderStyle-CssClass="button" ControlStyle-CssClass="linkgrid"  ItemStyle-HorizontalAlign="Center" />
			                                --%>
                                            <asp:TemplateField HeaderText="" HeaderStyle-CssClass="button"  ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                     <table>
                                                        <tr>
                                
                                                        <td>
                                                        <asp:ImageButton ToolTip="Edit PODetail" CausesValidation="false" OnCommand="imgEditPOD_Click" CommandArgument='<%# Eval("PodID") %>' ImageUrl="~/Images/edit.png" ID="imgEditPOD"  runat="server" />
                        
                                                        </td>
                                                        <td>
                                                        <asp:ImageButton ID="imgDelPoD" runat="server" OnClientClick="return confirm('Are you sure you want to delete?')"  CommandName="Delete" AlternateText="Delete POD" ImageUrl="~/images/delete.png" />
                                                        </td>
                                                        </tr>
                                                        </table>
                                                    <%--<asp:LinkButton ID="linkDeleteCust" CssClass="linkgrid"  CommandName="Delete" runat="server">Delete</asp:LinkButton>--%>
                                                    </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                   </asp:GridView>
                                </div>
                             </td>
                        </tr>
			        </ItemTemplate>			       
			    </asp:TemplateField>			    
			</Columns>
        </asp:GridView>          
            
            </td></tr>
             <tr><td style="width: 1032px"><br/></td></tr>
              <tr><td style="width: 1032px"><br/></td></tr>
        </table>

        

        



         <script type='text/javascript'>


             prm = Sys.WebForms.PageRequestManager.getInstance();
             prm.add_endRequest(EndRequest);
             function EndRequest(sender, args) {
                 //alert("EndRequest");
                 $(document).AjaxReady();
             }
        </script> 
        
    
        </td>
	</tr>
    </table>
        </ContentTemplate>
        <Triggers>
        <asp:PostBackTrigger ControlID="btnDownload" />
        <asp:PostBackTrigger ControlID="btnDownPO" />
        <asp:PostBackTrigger ControlID="btnDown" />
        <asp:PostBackTrigger ControlID="btnUpload" />
        
        <asp:PostBackTrigger ControlID="btnSendESN" />
        <asp:PostBackTrigger ControlID="Button1" />
        <asp:PostBackTrigger ControlID="btnPoDetailTrk" />
        <asp:PostBackTrigger ControlID="btnEsn_Excel" />
        <asp:PostBackTrigger ControlID="btnPoHeader" />
        <asp:PostBackTrigger ControlID="btnUpd" />
        </Triggers>
        </asp:UpdatePanel>
        </div>
        <asp:UpdatePanel ID="upnlJsRunner" UpdateMode="Always" runat="server">
			<ContentTemplate>
				<asp:PlaceHolder ID="phrJsRunner" runat="server"></asp:PlaceHolder>
			</ContentTemplate>
		</asp:UpdatePanel>

    <table cellspacing="0" cellpadding="0"  border="0" align="center" width="100%">
	 
	<tr>
		<td>
			&nbsp;
            <asp:UpdateProgress ID="UpdateProgress1" runat="server"
                     DynamicLayout="false">
            <ProgressTemplate>
                <img src="/Images/ajax-loaders.gif" /> Loading ...
            </ProgressTemplate>
            </asp:UpdateProgress>
		</td>
	</tr>
    
	<tr>
	    <td>
				<foot:MenuFooter id="Menuheader2" runat="server"></foot:MenuFooter>
        </td>
	</tr>
	</table>
    </form>
</body>
</html>
