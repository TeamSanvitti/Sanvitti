<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="POQueryNew.aspx.cs" Inherits="avii.POQueryNew" %>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="/Controls/Header.ascx" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="/Controls/Footer.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="UC" TagName="Comments" Src="~/Controls/FulfillmentComments.ascx" %>


<%--<%@ Register TagPrefix="PO" TagName="Detail" Src="~/Controls/PODetails.ascx" %>
--%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Fulfillment Search</title>
		
		

    <!-- from http://encosia.com/2009/10/11/do-you-know-about-this-undocumented-google-cdn-feature/ -->
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

        .hide{
            display:none;
        }
        .show{
            display:none;
        }
	</style>
    <div id="Div1" runat="server"> 
	<script type="text/javascript">
        function OpenNewPage(url) {

            var newWin = window.open(url);
            
            if (!newWin || newWin.closed || typeof newWin.closed == 'undefined') {
                alert('your pop up blocker is enabled');

                //POPUP BLOCKED
            }
        }
        function Refresh() {
            //alert('refreshing..');
            var btnhdPrintlabel = document.getElementById("<%= btnhdPrintlabel.ClientID %>");
            btnhdPrintlabel.click();
        }

        function OpenPDF(base64data) {

           // window.open("data:application/octet-stream;charset=utf-16le;base64,"+data64);
           // window.open("data:application/pdf;base64, " + base64data);
           // window.open("data:application/pdf;base64," + encodeURI(base64data));
            var pdfWindow = window.open('','Print Label');
            pdfWindow.document.write("<iframe width='100%' height='100%' src='data:application/pdf;base64, " + encodeURI(base64data) + "'></iframe>")

        }
        function PrintDiv() {
            var divContents = document.getElementById("divLabelImg").innerHTML;
            var printWindow = window.open('', '', 'height=650,width=1024');
            printWindow.document.write('<html><head><title></title>');
            printWindow.document.write('</head><body leftmargin="0" rightmargin="0">');
            printWindow.document.write(divContents);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            printWindow.print();
        }
    
        function isQuantity(obj) {
                
                if (obj.value == '0') {
                    alert('Quantity can not be zero');
                    obj.value = '1';
                    return false;
                }
                if (obj.value == '') {
                    alert('Quantity can not be empty');
                    obj.value = '1';
                    return false;
                }
            }
            function isNumberKey(evt) {
                
                var charCodes = evt.keyCode ? evt.keyCode : evt.which ? evt.which : evt.charCode;
                if (charCodes > 31 && (charCodes < 48 || charCodes > 57)) {
                    charCodes = 0;
                    return false;
                }
                return true;
        }
        function ShowTracking(obj) {
            //setAttribute("disabled", false);
            var objTrackingNumber = document.getElementById("<%= txtTrackingNumber.ClientID%>");
            if (obj.checked) {
                objTrackingNumber.disabled = false;
            }
            else
                objTrackingNumber.disabled = true;
        }
        function ValidateWeight(evt, obj) {
            var charCodes = evt.keyCode ? evt.keyCode : evt.which ? evt.which : evt.charCode;
            //alert(charCodes);
            var priceValue = obj.value;
            //alert(priceValue);
            //alert(priceValue.indexOf('.'))
            //if ((charCodes == 46 && priceValue.indexOf('.') > -1) || (charCodes == 190 && priceValue.indexOf('.') > -1) || (charCodes < 48 && charCodes == 46 && priceValue.indexOf('.') > -1) || (charCodes > 57 && charCodes != 190)) {
            if ((charCodes == 46 && priceValue.indexOf('.') > -1) || (charCodes < 48 && charCodes != 46) || charCodes > 57) {
                //charCodes = 0;
                ///priceValue = priceValue.replace('..', '.');
                //obj.value = priceValue;

                evt.preventDefault();
                //alert('in');
                return false;
            }
            //else

            return true;


        }
        function OnKeyUp(obj) {
            qty = document.getElementById(obj.id.replace('txtQty', 'hdnQty'));
            if (obj.value > qty.value) {
                alert('Quantity can not be greater than assigned quantity!');
                obj.value = '';
                obj.value = qty.value;
            }
        }
	    $(document).ready(function () {

            $("#divSKUESN").dialog({
	            autoOpen: false,
	            modal: false,
	            minHeight: 20,
	            height: 450,
	            width: 600,
	            resizable: false,

	            open: function (event, ui) {
	                $(this).parent().appendTo("#divFulfillmentContainer");
	            }
	        });

	        $("#divComments").dialog({
	            autoOpen: false,
	            modal: false,
	            minHeight: 20,
	            height: 360,
	            width: 650,
	            resizable: false,

	            open: function (event, ui) {
	                $(this).parent().appendTo("#divFulfillmentContainer");
	            }
	        });


	        $("#divAddTracking").dialog({
	            autoOpen: false,
	            modal: false,
	            minHeight: 20,
	            height: 260,
	            width: 450,
	            resizable: false,

	            open: function (event, ui) {
	                $(this).parent().appendTo("#divFulfillmentContainer");
	            }
	        });
$("#divESN").dialog({
	            autoOpen: false,
	            modal: false,
	            minHeight: 20,
	            height: 450,
	            width: 300,
	            resizable: false,

	            open: function (event, ui) {
	                $(this).parent().appendTo("#divFulfillmentContainer");
	            }
	        });


	        $("#divFulfillmentView").dialog({
	            autoOpen: false,
	            modal: true,
	            minHeight: 20,
	            height: 640,
	            width: 1200,
	            resizable: false,
	            open: function (event, ui) {
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
	            open: function (event, ui) {
	                $(this).parent().appendTo("#divFulfillmentContainer");
	            }
	        });


	        $("#divEditPO").dialog({
	            autoOpen: false,
	            modal: true,
	            minHeight: 20,
	            height: 500,
	            width: 850,
	            resizable: false,
	            open: function (event, ui) {
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
	            open: function (event, ui) {
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
	            open: function (event, ui) {
	                $(this).parent().appendTo("#divFulfillmentContainer");
	            }
	        });

            $("#divPOA").dialog({
                autoOpen: false,
                modal: true,
	            minHeight: 20,
	            height: 450,
	            width: 900,
	            resizable: false,

	            open: function (event, ui) {
	                $(this).parent().appendTo("#divFulfillmentContainer");
	            }
            });
            $("#divShipItems").dialog({
	            autoOpen: false,
	            modal: false,
	            minHeight: 20,
	            height: 450,
	            width: 1100,
	            resizable: false,

	            open: function (event, ui) {
	                $(this).parent().appendTo("#divFulfillmentContainer");
	            }
	        });
	        $("#divStore").dialog({
	            autoOpen: false,
	            modal: true,
	            minHeight: 20,
	            height: 225,
	            width: 500,
	            resizable: false,
	            open: function (event, ui) {
	                $(this).parent().appendTo("#divFulfillmentContainer");
	            }
            });

            $("#divLabel").dialog({
	            autoOpen: false,
	            modal: false,
	            minHeight: 20,
	            height: 650,
	            width: 1250,
	            resizable: false,
	            open: function (event, ui) {
	                $(this).parent().appendTo("#divFulfillmentContainer");
	            }
	        });

	    });


	    function closeCommentsDialog() {
	        //Could cause an infinite loop because of "on close handling"
	        $("#divComments").dialog('close');
	    }


	    function openCommentsDialog(title, linkID) {

	        var pos = $("#" + linkID).position();
	        var top = pos.top;
	        var left = pos.left + $("#" + linkID).width() + 10;
	        //alert(top);
	        if (top > 600)
	            top = 10;
	        else
	            top = 10;
	        //top = top - 600;
	        left = 425;
	        $("#divComments").dialog("option", "title", title);
	        $("#divComments").dialog("option", "position", [left, top]);

	        $("#divComments").dialog('open');

	        unblockCommentsDialog();
	    }
	    function openCommentsDialogAndBlock(title, linkID) {
	        openCommentsDialog(title, linkID);

	        //block it to clean out the data
	        $("#divComments").block({
	            message: '<img src="../images/async.gif" />',
	            css: { border: '0px' },
	            fadeIn: 0,
	            //fadeOut: 0,
	            overlayCSS: { backgroundColor: '#ffffff', opacity: 1 }
	        });
	    }
	    function unblockCommentsDialog() {
	        $("#divComments").unblock();
	    }


	    function closeAddESNDialog() {
	        //Could cause an infinite loop because of "on close handling"
	        $("#divESN").dialog('close');
	    }


	    function openESNDialog(title, linkID) {

	        var pos = $("#" + linkID).position();
	        var top = pos.top;
	        var left = pos.left + $("#" + linkID).width() + 10;
	        //alert(top);
	        if (top > 600)
	            top = 10;
		else
	            top = 10;
	        //top = top - 600;
	        left = 425;
	        $("#divESN").dialog("option", "title", title);
	        $("#divESN").dialog("option", "position", [left, top]);

	        $("#divESN").dialog('open');

	        unblockESNDialog();
	    }
	    function openESNDialogAndBlock(title, linkID) {
	        openESNDialog(title, linkID);

	        //block it to clean out the data
	        $("#divESN").block({
	            message: '<img src="../images/async.gif" />',
	            css: { border: '0px' },
	            fadeIn: 0,
	            //fadeOut: 0,
	            overlayCSS: { backgroundColor: '#ffffff', opacity: 1 }
	        });
	    }
	    function unblockESNDialog() {
	        $("#divESN").unblock();
	    }


        function closeAddSKUESNDialog() {
	        //Could cause an infinite loop because of "on close handling"
	        $("#divSKUESN").dialog('close');
	    }


	    function openSKUESNDialog(title, linkID) {

	        var pos = $("#" + linkID).position();
	        var top = pos.top;
	        var left = pos.left + $("#" + linkID).width() + 10;
	        //alert(top);
	        if (top > 600)
	            top = 10;
		else
	            top = 10;
	        //top = top - 600;
	        left = 425;
	        $("#divSKUESN").dialog("option", "title", title);
	        $("#divSKUESN").dialog("option", "position", [left, top]);

	        $("#divSKUESN").dialog('open');

	        unblockSKUESNDialog();
	    }
	    function openSKUESNDialogAndBlock(title, linkID) {
	        openSKUESNDialog(title, linkID);

	        //block it to clean out the data
	        $("#divESN").block({
	            message: '<img src="../images/async.gif" />',
	            css: { border: '0px' },
	            fadeIn: 0,
	            //fadeOut: 0,
	            overlayCSS: { backgroundColor: '#ffffff', opacity: 1 }
	        });
	    }
	    function unblockSKUESNDialog() {
	        $("#divSKUESN").unblock();
	    }

	    function closeAddTrackingDialog() {
	        //Could cause an infinite loop because of "on close handling"
	        $("#divAddTracking").dialog('close');
	    }



	    function openAddTrackingDialog(title, linkID) {

	        var pos = $("#" + linkID).position();
	        var top = pos.top;
	        var left = pos.left + $("#" + linkID).width() + 10;
	        //alert(top);
	        if (top > 600)
	            top = 10;
	        //top = top - 600;
	        left = 425;
	        $("#divAddTracking").dialog("option", "title", title);
	        $("#divAddTracking").dialog("option", "position", [left, top]);

	        $("#divAddTracking").dialog('open');

	       // unblockAddTrackingDialog();
	    }


	    function openAddTrackingDialogAndBlock(title, linkID) {
	        openAddTrackingDialog(title, linkID);

	        //block it to clean out the data
	        $("#divAddTracking").block({
	            message: '<img src="../images/async.gif" />',
	            css: { border: '0px' },
	            fadeIn: 0,
	            //fadeOut: 0,
	            overlayCSS: { backgroundColor: '#ffffff', opacity: 1 }
	        });
	    }

	    function unblockAddTrackingDialog() {
	        $("#divAddTracking").unblock();
	    }


        function closeLabelDialog() {
	        //Could cause an infinite loop because of "on close handling"
	        $("#divLabel").dialog('close');
	    }



	    function openLabelDialog(title, linkID) {

	        var pos = $("#" + linkID).position();
	        var top = pos.top;
	        var left = pos.left + $("#" + linkID).width() + 10;
	        //alert(top);
            if (top > 600)
                top = 10;
            else
                top = 10;
	        //top = top - 600;
	        left = 30;
	        $("#divLabel").dialog("option", "title", title);
	        $("#divLabel").dialog("option", "position", [left, top]);

	        $("#divLabel").dialog('open');

	        //unblockLabelDialog();
	    }


	    function openLabelDialogAndBlock(title, linkID) {
	        openLabelDialog(title, linkID);

	        //block it to clean out the data
	        $("#divLabel").block({
	            message: '<img src="../images/async.gif" />',
	            css: { border: '0px' },
	            fadeIn: 0,
	            //fadeOut: 0,
	            overlayCSS: { backgroundColor: '#ffffff', opacity: 1 }
	        });
	    }

	    function unblockLabelDialog() {
	        $("#divLabel").unblock();
	    }


	    function closeStoreDialog() {
	        //Could cause an infinite loop because of "on close handling"
	        $("#divStore").dialog('close');
	    }

	    function closeDialog() {
	        //Could cause an infinite loop because of "on close handling"
	        $("#divFulfillmentView").dialog('close');
	    }
	    function closeHistoryDialog() {
	        //Could cause an infinite loop because of "on close handling"
	        $("#divHistory").dialog('close');
	    }

        function closeShipDialog() {
	        //Could cause an infinite loop because of "on close handling"
	        $("#divShipItems").dialog('close');
	    }
	    function closeEditDialog() {
	        //Could cause an infinite loop because of "on close handling"
	        $("#divEditPO").dialog('close');
	    }
	    function closeEditPODDialog() {
	        //Could cause an infinite loop because of "on close handling"
	        $("#divEditPO").dialog('close');
	    }
	    function closeDownloadDialog() {
	        //Could cause an infinite loop because of "on close handling"
	        $("#divDownload").dialog('close');
	    }
        function closePOA() {
	        //Could cause an infinite loop because of "on close handling"
	        $("#divPOA").dialog('close');
	    }
	    function openDialog(title, linkID) {

	        var pos = $("#" + linkID).position();
	        var top = pos.top;
	        var left = pos.left + $("#" + linkID).width() + 10;
	        //alert(top);
	        if (top > 600)
	            top = 10;
	        //top = top - 600;
	        left = 55;
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
	        $("#chkRecieved").attr("checked", false);
	        var selectedRecords = false;
	        var poflag = true;
	        if ($('#chkDownload').is(":checked")) {

	            $('#<%=gvPOQuery.ClientID %>').find('input[Id*="chk"]:checkbox').each(function () {
	                if (this.checked) {
	                    selectedRecords = true;
	                }

	            });

	            poflag = selectedRecords;

	        }

	        if (poflag) {
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
	        else
	            alert('No records selected to download');

	        //else {
	        //  alert('Select download selected records only check then fulfillment from search result');
	        //}
	    }
        function openPOADialog(title, linkID) {

	        var pos = $("#" + linkID).position();
	        var top = pos.top;
	        var left = pos.left + $("#" + linkID).width() + 10;
	        //alert(top);
	        top = top - 600;
	        left = 275;
	        $("#divPOA").dialog("option", "title", title);
	        $("#divPOA").dialog("option", "position", [left, top]);

	        $("#divPOA").dialog('open');
        }


        function openPOADialogAndBlock(title, linkID) {
	        openPOADialog(title, linkID);

	        //block it to clean out the data
	        $("#divPOA").block({
	            message: '<img src="../images/async.gif" />',
	            css: { border: '0px' },
	            fadeIn: 0,
	            //fadeOut: 0,
	            overlayCSS: { backgroundColor: '#ffffff', opacity: 1 }
	        });
	    }
        function openShipDialog(title, linkID) {

	        var pos = $("#" + linkID).position();
	        var top = pos.top;
	        var left = pos.left + $("#" + linkID).width() + 10;
	        //alert(top);
	        top = top - 600;
	        left = 150;
	        $("#divShipItems").dialog("option", "title", title);
	        $("#divShipItems").dialog("option", "position", [left, top]);

	        $("#divShipItems").dialog('open');
        }


        function openShipDialogAndBlock(title, linkID) {
	        openShipDialog(title, linkID);

	        //block it to clean out the data
	        $("#divShipItems").block({
	            message: '<img src="../images/async.gif" />',
	            css: { border: '0px' },
	            fadeIn: 0,
	            //fadeOut: 0,
	            overlayCSS: { backgroundColor: '#ffffff', opacity: 1 }
	        });
	    }
	    function openDownloadDialogAndBlock(title, linkID) {

	        //var chkDownloadObj =
	        //	        var grid = document.getElementById('<%= gvPOQuery.ClientID %>');

	        //            if (grid.rows.length > 0) {
	        //                alert('dd');
	        //            }
	        if ($('#chkDownload').is(":checked")) {

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
	        else {
	            alert('Select download selected records only check then fulfillment from search result');
	        }
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

	    function openStoreDialog(title, linkID) {

	        var pos = $("#" + linkID).position();
	        var top = pos.top;
	        var left = pos.left + $("#" + linkID).width() + 10;
	        //alert(top);
	        top = top - 600;
	        left = 275;
	        $("#divStore").dialog("option", "title", title);
	        $("#divStore").dialog("option", "position", [left, top]);

	        $("#divStore").dialog('open');
	    }

	    function openStoreDialogAndBlock(title, linkID) {
	        openStoreDialog(title, linkID);

	        //block it to clean out the data
	        $("#divStore").block({
	            message: '<img src="../images/async.gif" />',
	            css: { border: '0px' },
	            fadeIn: 0,
	            //fadeOut: 0,
	            overlayCSS: { backgroundColor: '#ffffff', opacity: 1 }
	        });
	    }

	    function unblockStoreDialog() {
	        $("#divStore").unblock();
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
         function unblockPOADialog() {
	        $("#divPOA").unblock();
        }
        function unblockShipItemsDialog() {
	        $("#divShipItems").unblock();
        }
        
	    function onTest() {
	        $("#divFulfillmentView").block({
	            message: '<h1>Processing</h1>',
	            css: { border: '3px solid #a00' },
	            overlayCSS: { backgroundColor: '#ffffff', opacity: 1 }
	        });
	    }


	    $(function () {
	        $('#txtPONum').keyup(function () {
	            if (this.value.match(/[^a-zA-Z0-9-,_ ]/g)) {
	                this.value = this.value.replace(/[^a-zA-Z0-9-,_ ]/g, '');
	            }
	        });
	    });
	    //	    $(function () {
	    //	        $('#txtPONum').keypress(function (e) {
	    //	            var code = e.keyCode ? e.keyCode : e.which; // Get the key code.
	    //	            var pressedKey = String.fromCharCode(code); // Find the key pressed.
	    //	            if (!pressedKey.match(/[a-zA-Z0-9]/g)) // Check if it's a alpanumeric char or not.
	    //	            {
	    //	                e.preventDefault(); // If it is not then prevent the event from happening.  
	    //	            }

	    //	        });
	    //	    });
	    $(function () {
	        $('#txtCustName').keyup(function () {
	            if (this.value.match(/[^a-zA-Z0-9-,_ ]/g)) {
	                this.value = this.value.replace(/[^a-zA-Z0-9-,_ ]/g, '');
	            }
	        });
	    });
	    //	    $(function () {
	    //	        $('#txtCustName').keypress(function (e) {
	    //	            var code = e.keyCode ? e.keyCode : e.which; // Get the key code.
	    //	            var pressedKey = String.fromCharCode(code); // Find the key pressed.
	    //	            if (!pressedKey.match(/[a-zA-Z0-9]/g)) // Check if it's a alpanumeric char or not.
	    //	            {
	    //	                e.preventDefault(); // If it is not then prevent the event from happening.  
	    //	            }

	    //	        });
	    //	    });
	    $(function () {
	        $('#txtFromDate').keyup(function () {
	            if (this.value.match(/[^a-zA-Z0-9 ]/g)) {
	                this.value = this.value.replace(/[^a-zA-Z0-9 ]/g, '');
	            }
	        });
	    });
	    //	    $(function () {
	    //	        $('#txtFromDate').keypress(function (e) {
	    //	            var code = e.keyCode ? e.keyCode : e.which; // Get the key code.
	    //	            var pressedKey = String.fromCharCode(code); // Find the key pressed.
	    //	            if (!pressedKey.match(/[a-zA-Z0-9]/g)) // Check if it's a alpanumeric char or not.
	    //	            {
	    //	                e.preventDefault(); // If it is not then prevent the event from happening.  
	    //	            }

	    //	        });
	    //	    });
	    $(function () {
	        $('#txtToDate').keyup(function () {
	            if (this.value.match(/[^a-zA-Z0-9 ]/g)) {
	                this.value = this.value.replace(/[^a-zA-Z0-9 ]/g, '');
	            }
	        });
	    });
	    //	    $(function () {
	    //	        $('#txtToDate').keypress(function (e) {
	    //	            var code = e.keyCode ? e.keyCode : e.which; // Get the key code.
	    //	            var pressedKey = String.fromCharCode(code); // Find the key pressed.
	    //	            if (!pressedKey.match(/[a-zA-Z0-9]/g)) // Check if it's a alpanumeric char or not.
	    //	            {
	    //	                e.preventDefault(); // If it is not then prevent the event from happening.  
	    //	            }

	    //	        });
	    //	    });
	    $(function () {
	        $('#txtShipTo').keyup(function () {
	            if (this.value.match(/[^a-zA-Z0-9 ]/g)) {
	                this.value = this.value.replace(/[^a-zA-Z0-9 ]/g, '');
	            }
	        });
	    });
	    //	    $(function () {
	    //	        $('#txtShipTo').keypress(function (e) {
	    //	            var code = e.keyCode ? e.keyCode : e.which; // Get the key code.
	    //	            var pressedKey = String.fromCharCode(code); // Find the key pressed.
	    //	            if (!pressedKey.match(/[a-zA-Z0-9]/g)) // Check if it's a alpanumeric char or not.
	    //	            {
	    //	                e.preventDefault(); // If it is not then prevent the event from happening.  
	    //	            }

	    //	        });
	    //	    });
	    $(function () {
	        $('#txtShipFrom').keyup(function () {
	            if (this.value.match(/[^a-zA-Z0-9 ]/g)) {
	                this.value = this.value.replace(/[^a-zA-Z0-9 ]/g, '');
	            }
	        });
	    });
	    //	    $(function () {
	    //	        $('#txtShipFrom').keypress(function (e) {
	    //	            var code = e.keyCode ? e.keyCode : e.which; // Get the key code.
	    //	            var pressedKey = String.fromCharCode(code); // Find the key pressed.
	    //	            if (!pressedKey.match(/[a-zA-Z0-9]/g)) // Check if it's a alpanumeric char or not.
	    //	            {
	    //	                e.preventDefault(); // If it is not then prevent the event from happening.  
	    //	            }

	    //	        });
	    //	    });
	    //	    	
	    $(function () {
	        $('#txtEsn').keyup(function () {
	            if (this.value.match(/[^a-zA-Z0-9-,_ ]/g)) {
	                this.value = this.value.replace(/[^a-zA-Z0-9-,_ ]/g, '');
	            }
	        });
	    });
	    //	    $(function () {
	    //	        $('#txtEsn').keypress(function (e) {
	    //	            var code = e.keyCode ? e.keyCode : e.which; // Get the key code.
	    //	            var pressedKey = String.fromCharCode(code); // Find the key pressed.
	    //	            if (!pressedKey.match(/[a-zA-Z0-9]/g)) // Check if it's a alpanumeric char or not.
	    //	            {
	    //	                e.preventDefault(); // If it is not then prevent the event from happening.  
	    //	            }

	    //	        });
	    //	    });
	    $(function () {
	        $('#txtAvNumber').keyup(function () {
	            if (this.value.match(/[^a-zA-Z0-9-,_ ]/g)) {
	                this.value = this.value.replace(/[^a-zA-Z0-9-,_ ]/g, '');
	            }
	        });
	    });
	    //	    $(function () {
	    //	        $('#txtAvNumber').keypress(function (e) {
	    //	            var code = e.keyCode ? e.keyCode : e.which; // Get the key code.
	    //	            var pressedKey = String.fromCharCode(code); // Find the key pressed.
	    //	            if (!pressedKey.match(/[a-zA-Z0-9]/g)) // Check if it's a alpanumeric char or not.
	    //	            {
	    //	                e.preventDefault(); // If it is not then prevent the event from happening.  
	    //	            }

	    //	        });
	    //	    });
	    $(function () {
	        $('#txtMslNumber').keyup(function () {
	            if (this.value.match(/[^a-zA-Z0-9-,_ ]/g)) {
	                this.value = this.value.replace(/[^a-zA-Z0-9-,_ ]/g, '');
	            }
	        });
	    });
	    //	    $(function () {
	    //	        $('#txtMslNumber').keypress(function (e) {
	    //	            var code = e.keyCode ? e.keyCode : e.which; // Get the key code.
	    //	            var pressedKey = String.fromCharCode(code); // Find the key pressed.
	    //	            if (!pressedKey.match(/[a-zA-Z0-9]/g)) // Check if it's a alpanumeric char or not.
	    //	            {
	    //	                e.preventDefault(); // If it is not then prevent the event from happening.  
	    //	            }

	    //	        });
	    //	    });
	    $(function () {
	        $('#txtStoreID').keyup(function () {
	            if (this.value.match(/[^a-zA-Z0-9-,_ ]/g)) {
	                this.value = this.value.replace(/[^a-zA-Z0-9-,_ ]/g, '');
	            }
	        });
	    });
	    //	    $(function () {
	    //	        $('#txtStoreID').keypress(function (e) {
	    //	            var code = e.keyCode ? e.keyCode : e.which; // Get the key code.
	    //	            var pressedKey = String.fromCharCode(code); // Find the key pressed.
	    //	            if (!pressedKey.match(/[a-zA-Z0-9]/g)) // Check if it's a alpanumeric char or not.
	    //	            {
	    //	                e.preventDefault(); // If it is not then prevent the event from happening.  
	    //	            }

	    //	        });
	    //	    });
	    $(function () {
	        $('#txtItemCode').keyup(function () {
	            if (this.value.match(/[^a-zA-Z0-9-,_ ]/g)) {
	                this.value = this.value.replace(/[^a-zA-Z0-9-,_ ]/g, '');
	            }
	        });
	    });
	    //	    $(function () {
	    //	        $('#txtItemCode').keypress(function (e) {
	    //	            var code = e.keyCode ? e.keyCode : e.which; // Get the key code.
	    //	            var pressedKey = String.fromCharCode(code); // Find the key pressed.
	    //	            if (!pressedKey.match(/[a-zA-Z0-9]/g)) // Check if it's a alpanumeric char or not.
	    //	            {
	    //	                e.preventDefault(); // If it is not then prevent the event from happening.  
	    //	            }

	    //	        });
	    //	    });
	    $(function () {
	        $('#txtFmUpc').keyup(function () {
	            if (this.value.match(/[^a-zA-Z0-9-,_ ]/g)) {
	                this.value = this.value.replace(/[^a-zA-Z0-9-,_ ]/g, '');
	            }
	        });
	    });
	    //	    $(function () {
	    //	        $('#txtFmUpc').keypress(function (e) {
	    //	            var code = e.keyCode ? e.keyCode : e.which; // Get the key code.
	    //	            var pressedKey = String.fromCharCode(code); // Find the key pressed.
	    //	            if (!pressedKey.match(/[a-zA-Z0-9]/g)) // Check if it's a alpanumeric char or not.
	    //	            {
	    //	                e.preventDefault(); // If it is not then prevent the event from happening.  
	    //	            }

	    //	        });
	    //	    });

   function checkTextAreaMaxLength(textBox, e, maxLength) {
	        //if (!checkSpecialKeys(e)) 
            {
	            if (textBox.value.length > maxLength) 
                {
                    if (window.event)//IE
                    {
                        alert('Comment length exceeds the allowed max length.')
                        onBlurTextCounter(textBox, maxLength);

                        //e.returnValue = false;
                    }
                    else//Firefox
                    {
                        alert('Comment length exceeds the allowed max length.')
                        onBlurTextCounter(textBox, maxLength);

                        //e.preventDefault();
                    }
	            }
	        }
	        
	    }

	    function checkSpecialKeys(e) {
	        if (e.keyCode != 8 && e.keyCode != 46 && e.keyCode != 37 && e.keyCode != 38 && e.keyCode != 39 && e.keyCode != 40)
	            return false;
	        else
	            return true;
	    }

	    function onBlurTextCounter(textBox, maxLength) {
	        if (textBox.value.length > maxLength)
	            textBox.value = textBox.value.substr(0, maxLength);
	    }

	    function parseDate(str) {
	        var mdy = str.split('/')
	        return new Date(mdy[2], mdy[0] - 1, mdy[1]);
	    }

	    function daydiff(first, second) {
	        return (second - first) / (1000 * 60 * 60 * 24);
	    }
        function ValidateShipPo() {

	        var defaultDateRange = '10';
	        var uploadDate = document.getElementById("<%=txtShippingDate.ClientID %>").value;
	        //alert(uploadDate);
	        if (uploadDate != null && uploadDate != '') {

	            var today = new Date();
	            var dd = today.getDate();
	            var mm = today.getMonth() + 1; //January is 0!
	            var yyyy = today.getFullYear();

	            if (dd < 10) {
	                dd = '0' + dd
	            }

	            if (mm < 10) {
	                mm = '0' + mm
	            }

	            today = mm + '/' + dd + '/' + yyyy;
	            //alert(daydiff(parseDate(uploadDate), parseDate(today)));
                var daterange = daydiff(parseDate(today), parseDate(uploadDate));
                //alert(daterange)
	            if (daterange < 0) {
	                alert('Requested ship date can not be less than today date');
	                return false;
	            }
	            if (daterange > 10) {
                    alert('Requested ship date can not be more than 10 days from today date');
                    return false;
                    //can not be less than 7 days from Today's date
	            }

	        }
	        var msg = '';
	        //	        var ShipBy = document.getElementById("").value;
	        //	        if (ShipBy == '')
	        //	            msg = 'Shipvia cannot be empty!';

            var TrackingNumber = document.getElementById("<%= txtTrackingNumber.ClientID %>").value;
            var chkManual = document.getElementById("<%= chkTracking.ClientID %>")
            if (chkManual.checked)
                if (TrackingNumber == '') {
                    alert('Tracking number cannot be empty!');
                    return false;
                }
	        <%--var ShipVia = document.getElementById("<%= ddlShipVia.ClientID %>");
	        if (ShipVia.selectedIndex == 0 && msg == '')
	            msg = 'ShipVia cannot be empty!';
	        else
	            if (state.selectedIndex == 0 && msg != '')
	                msg = 'ShipVia, ' + msg;--%>

            if (msg == '') {
                return true;
	        }
	        else {
	           // alert(msg);
	            return false;
	        }
	    }
	    function ValidateEditPo() {

	        var defaultDateRange = '1095';
	        var uploadDate = document.getElementById("<%=txtPODate.ClientID %>").value;
	        //alert(uploadDate);
	        if (uploadDate != null && uploadDate != '') {

	            var today = new Date();
	            var dd = today.getDate();
	            var mm = today.getMonth() + 1; //January is 0!
	            var yyyy = today.getFullYear();

	            if (dd < 10) {
	                dd = '0' + dd
	            }

	            if (mm < 10) {
	                mm = '0' + mm
	            }

	            today = mm + '/' + dd + '/' + yyyy;
	            //alert(daydiff(parseDate(uploadDate), parseDate(today)));
	            var daterange = daydiff(parseDate(uploadDate), parseDate(today));

	            if (daterange > defaultDateRange) {
	                alert('UploadDate can not be more than  1095 days before');
	                return false;
	            }
	            if (daterange < 0) {
	                alert('UploadDate can not be more than today date');
	                return false;
	            }

            }

            var ShippingDate = document.getElementById("<%=txtReqShipDate.ClientID %>").value;
            //alert(uploadDate);
            if (ShippingDate != null && ShippingDate != '') {

                var today = new Date();
                var dd = today.getDate();
                var mm = today.getMonth() + 1; //January is 0!
                var yyyy = today.getFullYear();

                if (dd < 10) {
                    dd = '0' + dd
                }

                if (mm < 10) {
                    mm = '0' + mm
                }

                today = mm + '/' + dd + '/' + yyyy;
                //alert(daydiff(parseDate(uploadDate), parseDate(today)));
                var daterange = daydiff(parseDate(today), parseDate(ShippingDate));
                //alert(daterange)
                if (daterange < 0) {
                    alert('Requested ship date can not be less than today date');
                    return false;
                }
                if (daterange > 10) {
                    alert('Requested ship date can not be more than 10 days from today date');
                    return false;
                    //can not be less than 7 days from Today's date
                }

            }
            else {
                alert('Requested ship date can not be empty');
            }
	        var msg = '';
	        //	        var ShipBy = document.getElementById("").value;
	        //	        if (ShipBy == '')
	        //	            msg = 'Shipvia cannot be empty!';

	        var contactName = document.getElementById("<%= txtContactName.ClientID %>").value;
	        if (contactName == '' && msg == '')
	            msg = 'Contact Name cannot be empty!';
	        //	        else
	        //	            if (contactName == '' && msg != '')
	        //	                msg = 'Shipvia and Contact Name cannot be empty!';


	        var address = document.getElementById("<%= txtStreetAdd.ClientID %>").value;
	        if (address == '' && msg == '')
	            msg = 'Address cannot be empty!';
	        else
	            if (address == '' && msg != '')
	                msg = 'Address, ' + msg;

	        var City = document.getElementById("<%= txtCity.ClientID %>").value;
	        if (City == '' && msg == '')
	            msg = 'City cannot be empty!';
	        else
	            if (City == '' && msg != '')
	                msg = 'City, ' + msg;

	        var state = document.getElementById("<%= dpState.ClientID %>");
	        if (state.selectedIndex == 0 && msg == '')
	            msg = 'State cannot be empty!';
	        else
	            if (state.selectedIndex == 0 && msg != '')
	                msg = 'State, ' + msg;


	        var zip = document.getElementById("<%= txtZip.ClientID %>").value;
	        if (zip == '' && msg == '')
	            msg = 'Zip cannot be empty!';
	        else
	            if (zip == '' && msg != '')
	                msg = 'Zip, ' + msg;



	        if (msg == '') {
	            return true;
	        }
	        else {
	            alert(msg);
	            return false;
	        }



	    }

	    function ValidateTracking() {
	        var returnObj = '';
	        //alert(returnObj)
	        //	        if (returnObj.selectedIndex == 0) {
	        //	            alert('Shipment type is required!');
	        //	            return false;
	        //	        }
	        var trackingNo = document.getElementById("<%= txtTrackings.ClientID %>").value;
	       // if (trackingNo == '') {
	       ///     alert('Tracking number is required!');
	       //     return false;
	       // }
	        //alert(trackingNo);
	        //	        if (returnObj.selectedIndex > 1) {
	        //	            if (trackingNo == '') {
	        //	                alert('Tracking number is required!');
	        //	                return false;
	        //	            }
	        //	            else {
	        //	                if (returnObj.selectedIndex < 2) {
	        //	                    alert('Shipment type must be shipped or retuned!');
	        //	                    return;
	        //	                }
	        //                }

	        //}
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
            function ReadOnlys(evt) {
		        var img3 = document.getElementById("img3");
		        img3.click();
		        evt.keyCode = 0;
		        return false;

		    }
		    function ReadOnly(evt) {
		        var imgCall = document.getElementById("imgCal");
		        imgCall.click();
		        evt.keyCode = 0;
		        return false;

            }
            function ReadOnlyReqShipDate(evt) {
                var imgCall = document.getElementById("imgReq");
                imgCall.click();
                evt.keyCode = 0;
                return false;

            }
function ValidateStatus() {
	        var status = document.getElementById("<%=dpStatus.ClientID  %>");
	        if (status.selectedIndex == 0) {
	            alert('Select a status first');
	            return false;
            }
        }

function ValidateRecievedStatus(obj) {
		        if (obj.checked) {
		            var flag = confirm('Do you want to mark pending order to In-Process order?');
		            if (!flag) {
		                var chkReceive = document.getElementById("<%=chkRecieved.ClientID %>");
		                chkReceive.checked = false;
                    }
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
		        if (((keyStroke >= 65) && (keyStroke <= 90)) || keyStroke ==95 ||
              ((keyStroke >= 97) && (keyStroke <= 122)) ||
              ((keyStroke >= 44 && keyStroke < 58)))
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

		    function set_focus1() {
		        var img = document.getElementById("imgFromtDate");
		        var st = document.getElementById("dpStatusList");
		        st.focus();
		        img.click();
		    }
		    function set_focus2() {
		        var img = document.getElementById("imgToDate");
		        var st = document.getElementById("dpStatusList");
		        st.focus();
		        img.click();
		    }

		    function set_focus3() {
		        var img = document.getElementById("img1");
                var st = document.getElementById("txtComment"); //flnUpload
		        st.focus();
		        img.click();
		    }
		    function set_focus4() {
		        var img = document.getElementById("img2");
		        var st = document.getElementById("dpStatusList");
		        st.focus();
		        img.click();
		    }

 
        </script>

     <script type="text/javascript" >
         
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
        </div>

      <style type="text/css">
.progresss {
          position: fixed !important;
          z-index: 9999 !important;
          top: 0px !important;
          left: 0px !important;
          background-color: #EEEEEE !important;
          width: 100% !important;
          height: 100% !important;
          filter: Alpha(Opacity=80) !important;
          opacity: 0.80 !important;
          -moz-cpacity: 0.80 !important;
      }
.modal
{
    position: fixed;
    top: 0;
    left: 0;
    background-color: black;
    z-index: 100000000;
    opacity: 0.8;
    filter: alpha(opacity=80);
    -moz-opacity: 0.8;
    min-height: 100%;
    width: 100%;
}
.loadingcss
{    
    font-size: 18px;
    /*border: 1px solid red;*/
    /*width: 200px;
    height: 100px;*/
    display: none;
    position: fixed;
    /*background-color: White;*/
    z-index: 100000001;
    background-color:#CF4342;
}

  </style>
 

</head>

<body bgColor="#ffffff" leftmargin="0" rightmargin="0" topmargin="0">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    
    <table cellspacing="0" cellpadding="0"  border="0"  width="100%">
	<tr>
	    <td><head:menuheader id="MenuHeader" runat="server"></head:menuheader>
		</td>
	</tr>
    </table>
    <div id="divFulfillmentContainer">
			<div id="divFulfillmentView" style="display:none">
					
				<asp:UpdatePanel ID="upnlView" runat="server" ChildrenAsTriggers="true">
					<ContentTemplate>
                   
						<asp:PlaceHolder ID="phrView" runat="server">

                            <asp:Label ID="lblViewPO" runat="server"  CssClass="errormessage"></asp:Label>
<table width="98%" align="center" cellpadding="0" cellspacing="0">
<tr valign="top">
    <td width="50%">
        <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="100%" align="center" >
           <tr bordercolor="#839abf">
                <td>
                    <table width="100%" cellSpacing="2" cellPadding="2">
                    <tr>
                        <td class="copy10grey" width="22%" align="left">
                            Fulfillment#:
                        </td>
                        <td width="1%">&nbsp;</td>
                        <td  align="left" width="32%" >
                            <asp:Label ID="lblPO" CssClass="copy10grey" runat="server" ></asp:Label>
                        </td>
                        <td class="copy10grey" width="15%" align="right">
                       <strong>  Status:</strong>
                        </td>
                        <td width="1%">&nbsp;</td>
                        <td  align="left" width="29%" >
                             <asp:Label ID="lblvStatus" CssClass="copy10grey" runat="server" ></asp:Label>
                            <%--<asp:Label ID="lblvAvso" CssClass="copy10grey" runat="server" ></asp:Label>--%>
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
                          Store ID:
                        </td>
                        <td width="1%">&nbsp;</td>
                        <td  align="left" width="29%" >
                           <asp:Label ID="lblvStoreID" CssClass="copy10grey" runat="server" ></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="copy10grey" width="22%" align="left">
                            Customer name:
                        </td>
                        <td width="1%">&nbsp;</td>
                        <td  align="left" width="32%" >
                            <asp:Label ID="lblCustName" CssClass="copy10grey" runat="server" ></asp:Label>
                        
                        </td>
                        <td class="copy10grey" width="15%" align="right">
                           Requested Shipping Date:
                        </td>
                        <td width="1%">&nbsp;</td>
                        <td  align="left" width="29%" >
                            <asp:Label ID="lblReqShipDate" CssClass="copy10grey" runat="server" ></asp:Label>
                        </td>
                        
                    </tr>
                    <tr>
                        <td class="copy10grey" width="22%" align="left">
                            Default ShipVia:
                        </td>
                        <td width="1%">&nbsp;</td>
                        <td  align="left" width="32%" >
                            <asp:Label ID="lblShipViaCode" CssClass="copy10grey" runat="server" ></asp:Label>
                        
                        </td>
                        <td class="copy10grey" width="15%" align="right">
                            
                        </td>
                        <td width="1%">&nbsp;</td>
                        <td  align="left" width="29%" >
                            
                        </td>
                        
                    </tr>
                    </table>
                </td>
            </tr>
        </table>
       
    </td>
    <td width="50%">
    
        <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="99%" align="center" >
           <tr bordercolor="#839abf">
                <td>
                <table width="100%" cellSpacing="2" cellPadding="2">
                
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
                    <td class="copy10grey" width="30%" align="left">
                    
                        Shipment Sent:
                    </td>
                    <td width="1%">&nbsp;</td>
                    <td  align="left">
                        <asp:Label ID="lblShipmentAck" CssClass="copy10grey" runat="server" Text="PENDING" ></asp:Label>
                    </td>
                     <td class="copy10grey" width="30%" align="right">
                        
                    </td>
                    <td width="1%">&nbsp;</td>
                    <td  align="left">
                        
                    </td>
                    
                </tr>
                   
                </table>
                </td>
            </tr>
        </table>
    </td>
</tr>
<tr>
<td align="right" colspan="2">&nbsp;

    <asp:Button ID="btnContainerSlip" Visible="true" runat="server" CssClass="button" Text="Container Slip" OnClick="btnContainerSlip_Click" /> 
    &nbsp;
    <asp:Button ID="btnPckSlip" Visible="true" runat="server" CssClass="button" Text="Packing Slip" OnClick="btnPckSlip_Click" /> 
    
</td>
</tr>
</table>

<table width="98%" align="center" cellpadding="0" cellspacing="0">
<tr >
<td colspan="2">
<table width="100%" border="0" cellpadding="0" cellspacing="0" align="center">

<tr valign="bottom" style="height:5px">
    <td align="left" class="buttonlabel" style="width:100%" >
    Shipment
    <%--</td>
                            
    <td align="right" style="height:5px" class="buttonlabel">
    --%>
    <asp:Button ID="btnTracking" Visible="false" runat="server" CssClass="buttongray" Text="Add Return Label" OnClick="btnTracking_Click" 
    OnClientClick="openAddTrackingDialogAndBlock('Add Tracking', 'btnTracking')"/> 
    </td>

</tr>
<%--<tr valign="top" style="display:none">
    <td>
<asp:Repeater ID="rptTracking" runat="server">
<HeaderTemplate>
<table border="0" width="100%" cellpadding="1" cellspacing="1">
<tr valign="top">
    <td align="left" class="button" width="2%">
        Line#
    </td>
    <td align="center" class="button" width="20%">
        Ship By
    </td>
    <td align="center" class="button" width="20%">
        Ship Date
    </td>
    <td align="center" class="button" width="20%">
        TrackingNumber
    </td>
    <td align="center" class="button" width="20%">
        Tracking Status
    </td>
    <td align="center" class="button" width="8%">
        
    </td>
</tr>
</HeaderTemplate>
<ItemTemplate>
    <tr valign="top" class="<%# Container.ItemIndex % 2 == 0 ? "alternaterow" : "" %>">
    <td align="left" class="copy10grey" width="2%">
        <%#Eval("LineNumber")%>
        <asp:HiddenField ID="hdnLineNo" runat="server" Value='<%#Eval("LineNumber")%>' />
    </td>
    <td align="center" class="copy10grey" width="20%">
    
        
        <asp:DropDownList ID="ddlShipBy" runat="server" SelectedValue='<%#Eval("ShipByID")%>'>
        <asp:ListItem Text="" Value=""></asp:ListItem>
        <asp:ListItem Text="FedEx General" Value="1"></asp:ListItem>
        <asp:ListItem Text="FedEx One day" Value="2"></asp:ListItem>
        <asp:ListItem Text="FedEx 2 days" Value="3"></asp:ListItem>
        <asp:ListItem Text="FedEx Saturday Deliver" Value="4"></asp:ListItem>
        <asp:ListItem Text="FedEx Saver 3 day" Value="5"></asp:ListItem>
        <asp:ListItem Text="UPS Ground" Value="6"></asp:ListItem>
        <asp:ListItem Text="UPS Blue" Value="7"></asp:ListItem>
        <asp:ListItem Text="UPS Red" Value="8"></asp:ListItem>
        <asp:ListItem Text="Ground delivery signature required" Value="9"></asp:ListItem>
        <asp:ListItem Text="Ground adult signature required" Value="10"></asp:ListItem>
        <asp:ListItem Text="Residential no signature required" Value="11"></asp:ListItem>
        <asp:ListItem Text="Residential delivery signature required" Value="12"></asp:ListItem>
        <asp:ListItem Text="Residential adult signature required" Value="13"></asp:ListItem>
        <asp:ListItem Text="3D Express no signature required" Value="14"></asp:ListItem>
        <asp:ListItem Text="3D Express delivery signature required" Value="15"></asp:ListItem>
        <asp:ListItem Text="3D Express adult signature required" Value="16"></asp:ListItem>
        <asp:ListItem Text="2D Express no signature required" Value="17"></asp:ListItem>
        <asp:ListItem Text="2D Express delivery signature required" Value="18"></asp:ListItem>
        <asp:ListItem Text="2D Express adult signature required" Value="19"></asp:ListItem>
        <asp:ListItem Text="Standard Overnight no signature required" Value="20"></asp:ListItem>
        <asp:ListItem Text="Standard Overnight delivery signature required" Value="21"></asp:ListItem>
        <asp:ListItem Text="Standard Overnight adult signature required" Value="22"></asp:ListItem>
        <asp:ListItem Text="Priority Overnight no signature required" Value="23"></asp:ListItem>
        <asp:ListItem Text="Priority Overnight delivery signature required" Value="24"></asp:ListItem>
        <asp:ListItem Text="Priority Overnight adult signature required" Value="25"></asp:ListItem>
        <asp:ListItem Text="Saturday Delivery no signature required" Value="26"></asp:ListItem>
        <asp:ListItem Text="Saturday Delivery delivery signature required" Value="27"></asp:ListItem>
        <asp:ListItem Text="Saturday Delivery adult signature required" Value="28"></asp:ListItem>
        <asp:ListItem Text="Ground cash-on-delivery" Value="29"></asp:ListItem>
        <asp:ListItem Text="Express cash-on-delivery" Value="30"></asp:ListItem>
        <asp:ListItem Text="2D Express cash-on-delivery" Value="31"></asp:ListItem>
        <asp:ListItem Text="Saturday Delivery Express cash-on-delivery" Value="32"></asp:ListItem>
        <asp:ListItem Text="Priority Overnight Express cash-on-delivery" Value="33"></asp:ListItem>
        <asp:ListItem Text="International Priority" Value="34"></asp:ListItem>
        <asp:ListItem Text="International Economy" Value="35"></asp:ListItem>
        <asp:ListItem Text="Indirect signature" Value="36"></asp:ListItem>
        <asp:ListItem Text="Priority overnight" Value="37"></asp:ListItem>
        <asp:ListItem Text="Home Delivery 3D Express no signature required" Value="38"></asp:ListItem>
        <asp:ListItem Text="Home Delivery 3D Express delivery signature required" Value="39"></asp:ListItem>
        <asp:ListItem Text="Home Delivery 3D Express adult ignature required" Value="40"></asp:ListItem>
        <asp:ListItem Text="Home Delivery 2D Express no signature required" Value="41"></asp:ListItem>
        <asp:ListItem Text="Home Delivery 2D Express delivery signature required" Value="42"></asp:ListItem>
        <asp:ListItem Text="Home Delivery 2D Express adult signature required" Value="43"></asp:ListItem>
        <asp:ListItem Text="Home Delivery standard overnight no signature required" Value="44"></asp:ListItem>
        <asp:ListItem Text="Home Delivery standard overnight delivery signature required" Value="45"></asp:ListItem>
        <asp:ListItem Text="Home Delivery standard overnight adult signature required" Value="46"></asp:ListItem>
        <asp:ListItem Text="Home Delivery priority overnight no signature required" Value="47"></asp:ListItem>
        <asp:ListItem Text="Home Delivery priority overnight delivery signature required" Value="48"></asp:ListItem>
        <asp:ListItem Text="Home Delivery priority overnight adult signature required" Value="49"></asp:ListItem>
        <asp:ListItem Text="Home Delivery saturday no signature required" Value="50"></asp:ListItem>
        <asp:ListItem Text="Home Delivery saturday delivery signature required" Value="51"></asp:ListItem>
        <asp:ListItem Text="Home Delivery saturday adult signature required" Value="52"></asp:ListItem>
        <asp:ListItem Text="Fedex 1 Day" Value="53"></asp:ListItem>
        <asp:ListItem Text="Fedex 2 Days" Value="54"></asp:ListItem>
        <asp:ListItem Text="FedEx Saver 3 day" Value="55"></asp:ListItem>
        </asp:DropDownList>

    </td>
    <td align="center" class="copy10grey" width="20%">
        <%# Convert.ToDateTime(Eval("ShipDate")).ToShortDateString() == "1/1/0001" ? "" : Eval("ShipDate")%>
    </td>
    <td align="center" class="copy10grey" width="20%">
        <asp:TextBox ID="txtTracking" CssClass="copy10grey" MaxLength="25"  Width="99%" Text='<%# Eval("TrackingNumber") %>' runat="server"></asp:TextBox>
    </td>
    <td align="center" class="copy10grey" width="20%">
        <asp:DropDownList ID="ddlReturn" runat="server" SelectedValue='<%#Eval("ReturnValue")%>' >
        <asp:ListItem Text="" Value=""></asp:ListItem>
        <asp:ListItem Text="Shipped" Value="Shipped"></asp:ListItem>
        <asp:ListItem Text="Returned" Value="Returned"></asp:ListItem>
        </asp:DropDownList>
    </td>
    <td align="center" class="copy10grey" width="8%">
        <asp:ImageButton ID="imgDelTr" runat="server" OnClientClick="return confirm('Are you sure you want to delete?')"  
        CommandArgument='<%# Eval("LineNumber") %>'  OnCommand="imgDelTracking_Command" AlternateText="Delete Tracking" ImageUrl="~/images/delete.png" />

    </td>
</tr>
</ItemTemplate>
<FooterTemplate>
</table>
</FooterTemplate>
</asp:Repeater>
 </td>
</tr>--%>

<%--<tr valign="bottom" style="height:5px;display:none">
    
    <td align="right" style="height:5px">
    <asp:Button ID="btnUpdateTracking" runat="server" CssClass="button" Text="Update Tracking" OnClick="btnUpdateTracking_Click" /> 
    </td>
</tr>
--%>
</table>
<%--
OnRowDeleting = "gvTracking_RowDeleting" OnRowDeleted = "gvTracking_RowDeleted"
         OnRowEditing="gvTracking_RowEditing"  OnRowUpdating = "gvTracking_RowUpdating" 
            OnRowUpdated = "gvTracking_RowUpdated" OnRowCancelingEdit="gvTracking_RowCancelingEdit"--%>
    <div style="display:none">
    <asp:Button ID="btnhdPrintlabel" runat="server"   Text="Printlabel" OnClick="btnhdPrintlabel_Click" /> 
    </div>
    <asp:GridView ID="gvTracking"  Visible="true" BackColor="White" Width="100%"   AllowPaging="false"  
        AutoGenerateColumns="false" Font-Names="Verdana" runat="server"  DataKeyNames="LineNumber" 
        GridLines="Both" 
            
            AllowSorting="false" 
        BorderStyle="Double" BorderColor="#0083C1" OnRowDataBound="gvTracking_RowDataBound">
        <RowStyle BackColor="Gainsboro" />
        <AlternatingRowStyle BackColor="white" />
        <HeaderStyle  CssClass="buttongrid" ForeColor="white"/>
        <FooterStyle CssClass="white"  />
        <Columns>

         	<asp:TemplateField HeaderText="Line#"  ItemStyle-CssClass="copy10grey"  HeaderStyle-Width="1%" ItemStyle-Wrap="false"  ItemStyle-width="1%">
                <ItemTemplate>
                   <%# Container.DataItemIndex +  1 %> 
                   <%--<%#Eval("LineNumber")%>--%>
                </ItemTemplate> 
                
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Shipment Type" ItemStyle-CssClass="copy10grey" ItemStyle-Width="7%">
                <ItemTemplate>
                <%# Convert.ToString(Eval("ReturnValue")) == "S" ? "Ship": Convert.ToString(Eval("ReturnValue")) == "R" ? "Return": "Ship" %>
                
                </ItemTemplate>
                <%--<EditItemTemplate>
                    <asp:DropDownList ID="ddlReturn" runat="server" DataTextField='<%#Eval("ReturnValue")%>'>
                    <asp:ListItem Text="Shipped" Value="S"></asp:ListItem>
                    <asp:ListItem Text="Returned" Value="R"></asp:ListItem>
                    </asp:DropDownList>
                   
                </EditItemTemplate>                                                                                                                                  --%>
            </asp:TemplateField>
            
                                            
            <asp:TemplateField HeaderText="ShipBy" SortExpression="ShipBy"  ItemStyle-CssClass="copy10grey" ItemStyle-Width="3%">
                <ItemTemplate>
                    <%#Eval("ShipByCode")%>
                    <asp:HiddenField ID="hdShipVia" runat="server" Value='<%#Eval("ShipByCode")%>' />
                </ItemTemplate>

            </asp:TemplateField>
                     
            <asp:TemplateField HeaderText="ShipDate"   ItemStyle-CssClass="copy10grey" ItemStyle-Width="7%" >
                <ItemTemplate>
                
                <%# Convert.ToDateTime(Eval("ShipDate")).ToShortDateString() == "1/1/0001" ? "" : Eval("ShipDate")%>
                
                </ItemTemplate>
                <%--<EditItemTemplate>
                    <asp:TextBox ID="txtMdn" CssClass="copy10grey" MaxLength="30"  Text='<%# Eval("MdnNumber") %>' runat="server"></asp:TextBox>
                </EditItemTemplate>                                                                                --%>
            </asp:TemplateField>
                        
            <asp:TemplateField HeaderText="Ship Package"   ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                <ItemTemplate>
                    <%#Eval("ShipPackage")%>
                </ItemTemplate>
            </asp:TemplateField>     
            <asp:TemplateField HeaderText="Weight"   ItemStyle-CssClass="copy10grey" ItemStyle-Width="3%">
                <ItemTemplate>
                    <%#Eval("ShipWeight")%>
                </ItemTemplate>
            </asp:TemplateField>     
            <asp:TemplateField HeaderText="Price"   ItemStyle-CssClass="copy10grey" ItemStyle-Width="3%">
                <ItemTemplate>
                    $<%#Eval("ShipPrice")%>
                </ItemTemplate>
            </asp:TemplateField>     
            <asp:TemplateField HeaderText="TrackingNumber" SortExpression="TrackingNumber" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%" >
                <ItemTemplate>
    <%--<%# Eval("TrackingNumber")%>--%>
                    <asp:LinkButton ID="lnkESN" runat="server" 
                        CommandArgument='<%# Eval("TrackingNumber") %>'  OnCommand="lnkTracking_Command" AlternateText="View ESN"> 
                        <%# Convert.ToInt32(Eval("EsnCount")) == 0 ? Eval("TrackingNumber") : Eval("TrackingNumber") + "(" + Eval("EsnCount") + ")"%>  
                </asp:LinkButton>

                </ItemTemplate>
                <%--<EditItemTemplate>
                    <asp:TextBox ID="txtTracking" CssClass="copy10grey" MaxLength="25"  Width="99%" Text='<%# Eval("TrackingNumber") %>' runat="server"></asp:TextBox>
                </EditItemTemplate>
                --%>                                
            </asp:TemplateField>
                                   

                                        
                                            

            <%--<asp:TemplateField HeaderText="Comments"  ItemStyle-CssClass="copy10grey" ItemStyle-Width="7%">
                <ItemTemplate>
                    <%#Eval("Comments")%>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtComments" CssClass="copy10grey" MaxLength="30"  Text='<%# Eval("Comments") %>' runat="server"></asp:TextBox>
                </EditItemTemplate>
                                                
            </asp:TemplateField>
            --%>                                
            
             <%--<asp:TemplateField HeaderText="Label" SortExpression="LineNumber" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%" >
                <ItemTemplate>
    
                    <asp:LinkButton ID="lnkLabel" runat="server" 
                        CommandArgument='<%# Eval("LineNumber") %>'  OnCommand="lnkPrint_Command" AlternateText="View Label"> 
                        View Label

                </asp:LinkButton>

                </ItemTemplate>
                                 
            </asp:TemplateField>--%>
            <asp:TemplateField HeaderText=""  ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:HiddenField ID="hdTN" Value='<%# Eval("TrackingNumber") %>' runat="server" />
                     <asp:ImageButton ID="imgPrint" runat="server" CausesValidation="false" 
                        CommandArgument='<%# Eval("LineNumber") %>'  OnCommand="imgPrint_Command" ToolTip="Print Label" AlternateText="View Label" 
                        ImageUrl="~/images/printer.png" />
                    <asp:ImageButton ID="imgLabl" runat="server" 
                        CommandArgument='<%# Eval("ReturnValue") %>'  Visible="false" OnCommand="imgGenerateLabel_Command" ToolTip="Generate Label" AlternateText="Generate Label" 
                        ImageUrl="~/images/doc1.png" />
                </ItemTemplate>
            </asp:TemplateField>   
              
            <%--<asp:CommandField  AccessibleHeaderText="EditTracking"  HeaderText="Edit" ItemStyle-Width="2%" ShowEditButton="True" HeaderStyle-CssClass="button" ControlStyle-CssClass="linkgrid"    ItemStyle-HorizontalAlign="Center"/>--%>
		    <asp:TemplateField HeaderText="" Visible="false"  ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                     <asp:ImageButton ID="imgEditTr" runat="server" 
                        CommandArgument='<%# Eval("LineNumber") %>'  OnCommand="imgEditTracking_Command" ToolTip="Edit Tracking" AlternateText="Edit Tracking" 
                        ImageUrl="~/images/edit.png" />
                </ItemTemplate>
            </asp:TemplateField>                                                   
			  
            <asp:TemplateField HeaderText=""  ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                 <asp:ImageButton ID="imgDelTr" runat="server" OnClientClick="return confirm('Are you sure you want to delete?')"  
                    CommandArgument='<%# Eval("LineNumber") %>'  OnCommand="imgDelTracking_Command" AlternateText="Delete Tracking" ImageUrl="~/images/delete.png" />
<%--
                     <asp:ImageButton ID="imgDelTr" runat="server" OnClientClick="return confirm('Are you sure you want to delete?')"  
                     CommandName="Delete" AlternateText="Delete POD" ImageUrl="~/images/delete.png" />--%>
                </ItemTemplate>
            </asp:TemplateField>                                                   
			                                
        </Columns>
    </asp:GridView>
                                
            

</td>
</tr>
<tr>
<td>&nbsp;


</td>
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
<td align="left" class="buttonlabel" style="width:90%" >
    Line items
</td>
    
<td align="right" style="width:10%">

    <asp:Label ID="lblPODCount" runat="server" CssClass="copy10greyb" ></asp:Label>&nbsp;
</td>
</tr>
<tr>
    <td colspan="2">

    <asp:GridView ID="gvPODetail"  BackColor="White" Width="100%" Visible="true"  AllowPaging="true"
    OnPageIndexChanging="gvPODetail_PageIndexChanging"    PageSize="20"
        AutoGenerateColumns="false" Font-Names="Verdana" runat="server" DataKeyNames="PodID"
        GridLines="Both" OnRowDeleting = "GridView2_RowDeleting" OnRowDeleted = "GridView2_RowDeleted"
        OnRowCommand = "GridView2_RowCommand" AllowSorting="false" OnRowEditing="gvPODetail_RowEditing"  OnRowUpdating = "gvPODetail_RowUpdating" 
            OnRowUpdated = "gvPODetail_RowUpdated" OnRowCancelingEdit="gvPODetail_RowCancelingEdit"
        BorderStyle="Double" BorderColor="#0083C1" OnRowDataBound="gvPODetail_RowDataBound">
        <RowStyle BackColor="Gainsboro" />
        <AlternatingRowStyle BackColor="white" />
        <HeaderStyle  CssClass="buttongrid" ForeColor="white"/>
        <FooterStyle CssClass="white"  />
        <Columns>
         	<asp:TemplateField HeaderText="Line#"  ItemStyle-CssClass="copy10grey"  HeaderStyle-Width="3%" ItemStyle-Wrap="false"  ItemStyle-width="3%">
                <ItemTemplate>
                   <%--<%# Container.DataItemIndex +  1 %> , --%>
                   <%#Eval("LineNo")%>
                </ItemTemplate> 
                
            </asp:TemplateField>
            <asp:TemplateField HeaderText="SKU#"  ItemStyle-CssClass="copy10grey"  HeaderStyle-Width="7%" ItemStyle-Wrap="false"  ItemStyle-width="10%">
                <ItemTemplate>
                    <%# Convert.ToString(Eval("ItemCode")).ToUpper()%>
                </ItemTemplate> 
                
            </asp:TemplateField>
           <%-- <asp:TemplateField HeaderText="UPC" SortExpression="UPC"  ItemStyle-CssClass="copy10grey" ItemStyle-Width="7%">
                <ItemTemplate><%#Eval("UPC")%></ItemTemplate>
            </asp:TemplateField>
           --%>                                                                                                                         
                                            
            <asp:TemplateField HeaderText="Qty"  ItemStyle-CssClass="copy10grey" ItemStyle-Width="3%">
                <ItemTemplate><%#Eval("Quantity")%></ItemTemplate>
                 <EditItemTemplate>
                    <asp:TextBox ID="txtQty" CssClass="copy10grey" MaxLength="5" onkeypress="return isNumberKey(event);" onchange="return isQuantity(this);" 
                    Enabled='<%# Convert.ToInt32(Eval("StatusID")) == 1?true:false %>' Width="99%" Text='<%# Eval("Quantity") %>' runat="server"></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>
                 <%--                           
            <asp:TemplateField HeaderText="ESN"  ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%" >
                <ItemTemplate>
                    <%# Eval("ESN") %>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtEsn" CssClass="copy10grey" MaxLength="35"  Width="99%" Text='<%# Eval("ESN") %>' runat="server"></asp:TextBox>
                </EditItemTemplate>
                                                
            </asp:TemplateField>
                                            
            <asp:TemplateField HeaderText="MDN" ItemStyle-CssClass="copy10grey" ItemStyle-Width="7%" >
                <ItemTemplate><%#Eval("MdnNumber")%></ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtMdn" CssClass="copy10grey" MaxLength="30"  Text='<%# Eval("MdnNumber") %>' runat="server"></asp:TextBox>
                </EditItemTemplate>                                                                                
            </asp:TemplateField>--%>

                                        
          <%--  <asp:TemplateField HeaderText="MSID"   ItemStyle-CssClass="copy10grey" ItemStyle-Width="7%">
                <ItemTemplate><%#Eval("MsID")%></ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtMsid" CssClass="copy10grey" MaxLength="30"  Text='<%# Eval("MsID") %>' runat="server"></asp:TextBox>
                </EditItemTemplate>                                                                                                                                  
            </asp:TemplateField>--%>
                                  <%--          

            <asp:TemplateField HeaderText="MslNumber"  ItemStyle-CssClass="copy10grey" ItemStyle-Width="7%">
                <ItemTemplate>
                    <%#Eval("MslNumber")%>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtMslNumber" CssClass="copy10grey" MaxLength="30"  Text='<%# Eval("MslNumber") %>' runat="server"></asp:TextBox>
                </EditItemTemplate>
                                                
            </asp:TemplateField>--%>
                                            
           <%-- <asp:TemplateField HeaderText="Passcode" SortExpression="PassCode"  ItemStyle-CssClass="copy10grey" ItemStyle-Width="7%">
                <ItemTemplate><%#Eval("PassCode")%></ItemTemplate>
            </asp:TemplateField>
--%>
                                <%--            
                <asp:TemplateField HeaderText="FM-UPC" SortExpression="FMUPC" ItemStyle-CssClass="copy10grey" ItemStyle-Width="7%">
                <ItemTemplate>
                    <%#Eval("FMUPC")%>
                </ItemTemplate>
                <EditItemTemplate>
                                                    <asp:TextBox ID="txtFMUPC" CssClass="copy10grey" MaxLength="50"  Text='<%# Eval("FMUPC") %>' runat="server"></asp:TextBox>
                                                </EditItemTemplate>
                                                
            </asp:TemplateField>--%>
                                                                                                                                        
           <%-- <asp:TemplateField HeaderText="Phone Type" SortExpression="PhoneCategory"  ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                <ItemTemplate><%#Eval("PhoneCategory")%></ItemTemplate>
            </asp:TemplateField>--%>
             
             
<%--            <asp:TemplateField HeaderText="LTEICCID" ItemStyle-CssClass="copy10grey" ItemStyle-Width="7%">
                <ItemTemplate>
                    <%#Eval("LTEICCID")%>   
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtLTEICCID" Visible='<%# Convert.ToBoolean(Eval("CustomAttribute")) == true? true: false %>' CssClass="copy10grey" MaxLength="30"  Text='<%# Eval("LTEICCID") %>' runat="server"></asp:TextBox>
                </EditItemTemplate>
                                                
            </asp:TemplateField>--%>
            
            <%--<asp:TemplateField HeaderText="LTE IMSI"  ItemStyle-CssClass="copy10grey" ItemStyle-Width="7%">
                <ItemTemplate>
                    <%#Eval("LTEIMSI")%>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtLTEIMSI" Visible='<%# Convert.ToBoolean(Eval("CustomAttribute")) == true? true: false %>'  CssClass="copy10grey" MaxLength="30"  Text='<%# Eval("LTEIMSI") %>' runat="server"></asp:TextBox>
                </EditItemTemplate>
                                                
            </asp:TemplateField>
             <asp:TemplateField HeaderText="AKEY"  ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                <ItemTemplate>
                    <%#Eval("akey")%>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtAkey" Visible='<%# Convert.ToBoolean(Eval("CustomAttribute")) == true? true: false %>'  CssClass="copy10grey" MaxLength="30"  Text='<%# Eval("AKEY") %>' runat="server"></asp:TextBox>
                </EditItemTemplate>
                
             </asp:TemplateField>
             
            <asp:TemplateField HeaderText="OTKSL"  ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                <ItemTemplate>
                    <%#Eval("OTKSaL")%>   
                </ItemTemplate>
                 <EditItemTemplate>
                    <asp:TextBox ID="txtOTKSL" Visible='<%# Convert.ToBoolean(Eval("CustomAttribute")) == true? true: false %>'  CssClass="copy10grey" MaxLength="30"  Text='<%# Eval("OTKSaL") %>' runat="server"></asp:TextBox>
                </EditItemTemplate>
             </asp:TemplateField>
            <asp:TemplateField HeaderText="Sim"  ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                <ItemTemplate>
                    <%#Eval("SimNumber")%>   
                </ItemTemplate>
                 <EditItemTemplate>
                    <asp:TextBox ID="txtSim" Visible='<%# Convert.ToBoolean(Eval("IsSim")) == true? true: false %>'  CssClass="copy10grey" MaxLength="30"  Text='<%# Eval("SimNumber") %>' runat="server"></asp:TextBox>
                </EditItemTemplate>
             </asp:TemplateField>
           <asp:TemplateField HeaderText="Tracking#"  ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                <ItemTemplate>
                    <%#Eval("TrackingNumber")%> 
                
                </ItemTemplate>
                 
             </asp:TemplateField>--%>
           
            <asp:TemplateField HeaderText="Status"   ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                <ItemTemplate>
                <%# Convert.ToInt32(Eval("StatusID")) == 1 ? "Pending" : Convert.ToInt32(Eval("StatusID")) == 2 ? "Processed" : Convert.ToInt32(Eval("StatusID")) == 3 ? "Shipped" : Convert.ToInt32(Eval("StatusID")) == 4 ? "Closed" : Convert.ToInt32(Eval("StatusID")) == 5 ? "Return" : Convert.ToInt32(Eval("StatusID")) == 9 ? "Cancel" : Convert.ToInt32(Eval("StatusID")) == 6 ? "On Hold" : Convert.ToInt32(Eval("StatusID")) == 7 ? "Out of Stock" : Convert.ToInt32(Eval("StatusID")) == 8 ? "In Process" : Convert.ToInt32(Eval("StatusID")) == 10 ? "Partial Processed" : Convert.ToInt32(Eval("StatusID")) == 11 ? "Partial Shipped" : "Pending"%>
                <%-- <br /> <%#Eval("PODStatus")%>--%>
                    <asp:HiddenField ID="hdnStatus" Value='<%# Eval("StatusID") %>' runat="server" />
                   
                                                
                </ItemTemplate>
            </asp:TemplateField> 
            
             
              
            <asp:CommandField  AccessibleHeaderText="EditPOD" Visible="false"  HeaderText="Edit" ItemStyle-Width="5%" ShowEditButton="false" HeaderStyle-CssClass="buttonlabel" ControlStyle-CssClass="linkgrid"    ItemStyle-HorizontalAlign="Center"/>
		
            <asp:TemplateField HeaderText=""  ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                     <asp:ImageButton ID="imgDelPoD" runat="server" OnClientClick="return confirm('Are you sure you want to delete?')"  
                     CommandName="Delete" AlternateText="Delete POD" ImageUrl="~/images/delete.png" />
                </ItemTemplate>
            </asp:TemplateField>                                                   
			                                
        </Columns>
    </asp:GridView>
    </td>
</tr>
</table>

		                </asp:PlaceHolder>
						
						
					</ContentTemplate>
						<Triggers>
                            <asp:PostBackTrigger ControlID="btnhdPrintlabel" />
						</Triggers>	
				</asp:UpdatePanel>
           </div>
<div id="divESN" style="display:none">
					
				<asp:UpdatePanel ID="uplESN" runat="server">
					<ContentTemplate>
                        <asp:PlaceHolder ID="phESN" runat="server">
           
                            <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="100%" align="center" >
                           <tr bordercolor="#839abf">
                                <td>
                                    <table width="100%" cellSpacing="2" cellPadding="2">
                                    <tr>
                                        <td class="copy10grey" width="25%" align="left">
                                            <b>Tracking#:</b>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblTracking" runat="server" Text="Label"></asp:Label>
                                        </td>
                                        </tr>
                                        </table>
                                        </td>
                                        </tr>
                                        </table>
                            <asp:Label ID="lblESN" runat="server" Text="Label"></asp:Label>
                                        <asp:Repeater ID="rptESN" runat="server">
                                            <HeaderTemplate>
                                            <table width="100%" align="center">
                                                <tr >
                                                    <td class="buttongrid" width="2%">
                                                        &nbsp;S.No.
                                                    </td>
                                                    <td class="buttongrid">
                                                        &nbsp;ESN
                                                    </td>
                                                    
                                                </tr>
                            
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                            <tr class="<%# Container.ItemIndex % 2 == 0 ? "alternaterow" : "" %>">
                                                
                                                <td class="copy10grey">
                                                        &nbsp;<%# Container.ItemIndex + 1 %></td>
                                
                                                <td class="copy10grey">
                                                        &nbsp;<%# Eval("ESN")%></td>
                                
                                            </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                </table>    
                                            </FooterTemplate>
                                            </asp:Repeater>
                        


                            </asp:PlaceHolder>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                
            </div>

           <div id="divAddTracking" style="display:none">
					
				<asp:UpdatePanel ID="upTracking" runat="server">
					<ContentTemplate>
                        <asp:PlaceHolder ID="phTracking" runat="server">
                           <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="100%" align="center" >
                           <tr bordercolor="#839abf">
                                <td>
                                    <table width="100%" cellSpacing="2" cellPadding="2">
                                    <tr>
                                        <td class="copy10grey" width="25%" align="left">
                                            <b>Shipment Type:</b>
                                        </td>
                                        <td>
                                            <%--<asp:DropDownList ID="ddlReturn" runat="server" Width="91%" CssClass="copy10grey">
                                            <asp:ListItem Text="" Value=""></asp:ListItem>
                                            
                                            <asp:ListItem Text="Shipped" Value="S"></asp:ListItem>
                                            <asp:ListItem Text="Returned" Value="R"></asp:ListItem>
                                            </asp:DropDownList>--%>
                                            <asp:Label ID="lblReturn" runat="server" CssClass="copy10grey"></asp:Label>

                                        </td>
                                    </tr>
                                    
                                    <tr>
                                        <td class="copy10grey" width="25%" align="left">
                                            <b>Ship Via:</b>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="dpShipBy" runat="server" Width="91%" CssClass="copy10grey"></asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="copy10grey" width="25%" align="left">
                                         <b>   Tracking#:</b>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtTrackings" MaxLength="25"  Enabled="false" runat="server" Width="90%"  CssClass="copy10grey"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr valign="top">
                                        <td class="copy10grey" width="25%" align="left">
                                            Comment:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtTrComment" Width="90%" TextMode="MultiLine" Rows="5" runat="server" CssClass="copy10grey"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="center">
                                            <asp:Button ID="btnAddTrackings" runat="server"  OnClick="btnAddTrackings_Click" Text="Submit" CssClass="button"
                                             OnClientClick="return ValidateTracking();" />&nbsp;
                                             <asp:Button ID="btnCancelTr" runat="server" Text="Cancel" CssClass="button" OnClientClick="closeAddTrackingDialog()"  />
                                        
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

            <div id="divHistory" style="display:none">
					
				<asp:UpdatePanel ID="upnlHistory" runat="server">
					<ContentTemplate>
                   
						<asp:PlaceHolder ID="phrHistory" runat="server">

                            <table bordercolor="#839abf" border="1" width="98%" align="center" cellpadding="0" cellspacing="0">
                                    <tr><td>

                                    <table border="0" width="100%" class="box" align="center" cellpadding="5" cellspacing="5">
                                    <tr valign="top">
                                        <td>
                                       <strong> Fulfillment#: </strong>&nbsp;&nbsp;&nbsp;<asp:Label ID="lblPoNum" runat="server" CssClass="copy10grey"></asp:Label>
                                        </td>
                                    </tr>
                                    </table>
                                    </td>
                                    </tr>
                            </table>
                            <br />
                            <table bordercolor="#839abf" border="1" width="98%" align="center" cellpadding="0" cellspacing="0">
                                    <tr><td>

                                    <table border="0" width="100%" class="box" align="center" cellpadding="0" cellspacing="0">
                                    <tr valign="top">
                                        <td>
                                            <asp:Label ID="lblHistory" runat="server" CssClass="errormessage"></asp:Label>
                            
                                            <asp:Repeater ID="rptPO" runat="server">
                                            <HeaderTemplate>
                                            <table width="100%" align="center">
                                                <tr >
                                                    <%--<td class="button">
                                                     &nbsp;Fulfillment#
                                                    </td>
                                                    <td class="button">
                                                        &nbsp;Fulfillment Date
                                                    </td>--%>
                                                    <td class="buttongrid">
                                                        &nbsp;Status
                                                    </td>
                    
                                                    <td class="buttongrid">
                                                        &nbsp;Last Modified Date
                                                    </td>
                                                    
                                                    <td class="buttongrid">
                                                        &nbsp;Last Modified By
                                                    </td>
                                                    <td class="buttongrid">
                                                        &nbsp;Action
                                                    </td>
                                                    
                                                    <td class="buttongrid">
                                                        &nbsp;Source
                                                    </td>
                                                    
                                                </tr>
                            
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                            <tr class="<%# Container.ItemIndex % 2 == 0 ? "alternaterow" : "" %>">
                                                <%--<td class="copy10grey">
                                                <span title='<%# Eval("Comments")%>'>
                                                
                                                    &nbsp;<%# Eval("PurchaseOrderNumber")%>
                                                    </span>
                                                
                                                </td>
                                                <td class="copy10grey">
                                                    &nbsp;
                                                    <%# DataBinder.Eval(Container.DataItem, "PurchaseOrderDate", "{0:MM/dd/yyyy}")%>
                                                </td>--%>
                                                <td class="copy10grey">
                                                <span title='<%# Convert.ToString(Eval("SentASN")).Replace("'", "")%>'>
                                                
                                                    &nbsp;<%# Eval("PurchaseOrderStatus")%></span><%--&nbsp;<%# Eval("PurchaseOrderStatus")%>--%></td>
                
                                                <td class="copy10grey">
                                                    &nbsp;<%# Eval("ModifiedDate")%></td>
                
                                                
                                                <td class="copy10grey">
                                                        &nbsp;<%# Eval("CustomerName")%></td>
                                                
                                                <td class="copy10grey">
                                                        &nbsp;<%# Eval("Comments")%></td>
                                                <td class="copy10grey">
                                                        &nbsp;<%# Eval("SentESN")%></td>
                                
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
                            <asp:Label ID="lblEditPO" runat="server" CssClass="errormessage"></asp:Label>
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
                           
                    </td>
                    <td width="32%">

                       &nbsp;  <%--<asp:Label ID="lblAVSO" runat="server" CssClass="copy10grey"></asp:Label> --%>  
                    </td>

                </tr>
                <tr>
                    <td class="copy10grey" align="right" width="17%">
                        Fulfillment Date:    
                    </td>
                    <td width="32%">

                    &nbsp;<asp:TextBox ID="txtPODate" ContentEditable="false" onkeydown="return ReadOnly(event);" runat="server" CssClass="copy10grey"></asp:TextBox>&nbsp;
                        <img id="imgCal" alt=""  src="/fullfillment/calendar/sscalendar.jpg" runat="server"/>
                        <ajaxToolkit:CalendarExtender ID="CalExtDate" runat="server" PopupButtonID="imgCal" 
                        PopupPosition="BottomLeft" Format="MM/dd/yyyy"   TargetControlID="txtPODate">
                        </ajaxToolkit:CalendarExtender>
                        
                       &nbsp; <%--<asp:Label ID="lblPoDate" runat="server" CssClass="copy10grey"></asp:Label>   --%>
                    </td>
                    <td width="2%">
                    &nbsp;
                    </td>
                    
                    <td align="right" width="17%">
                        <asp:Label ID="lblPOStatus" CssClass="copy10grey" runat="server" Text="Status:"></asp:Label>
                    </td>    
                    <td width="32%">
                     &nbsp;<asp:DropDownList ID="ddlStatus" runat="server" class="copy10grey">
                                <asp:ListItem Text="" Value=""></asp:ListItem>
                                <asp:ListItem Text="Pending" Value="1"></asp:ListItem>
                                <asp:ListItem Text="In Process" Value="8"></asp:ListItem>
                                <asp:ListItem Text="Partial Processed" Value="10"></asp:ListItem>
                                <asp:ListItem Text="Partial Shipped" Value="11"></asp:ListItem>
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
                <tr>
                    <td class="copy10grey" align="right" width="17%">
                    Default Shipvia:
                            
                    </td>

                    <td width="32%">
                        &nbsp;<asp:DropDownList ID="dpShipVia" runat="server" Width="91%" CssClass="copy10grey"></asp:DropDownList>
                        <asp:Label ID="lblDShipvia" runat="server" CssClass="copy10grey" ></asp:Label>   
                    </td>
                    <td width="2%">
                    &nbsp;
                    </td>
                    <td class="copy10grey" align="right" width="17%">
                           Shipment required:
                    </td>
                    <td width="32%">

                       &nbsp;<asp:CheckBox ID="chkShipRequired" runat="server" />   
                    </td>

                        
                </tr>
                <tr>
                    <td class="copy10grey" align="right" width="17%">
                        Requested Ship Date: 
                    </td>
                    <td  width="32%">
                        &nbsp;<asp:TextBox ID="txtReqShipDate" ContentEditable="false" onkeydown="return ReadOnlyReqShipDate(event);" runat="server" CssClass="copy10grey"></asp:TextBox>&nbsp;
                        <img id="imgReq" alt=""  src="/fullfillment/calendar/sscalendar.jpg" runat="server"/>
                        <ajaxToolkit:CalendarExtender ID="cextId" runat="server" PopupButtonID="imgReq" 
                        PopupPosition="BottomLeft" Format="MM/dd/yyyy"   TargetControlID="txtReqShipDate">
                        </ajaxToolkit:CalendarExtender>
                    
                    </td>
                    <td width="2%">
                    &nbsp;
                    </td>
                    <td class="copy10grey" align="right" width="17%">
                    </td>
                    <td width="32%">

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
                       Destination StoreID:    
                    </td>
                    <td width="32%">

                       &nbsp;  <asp:Label ID="lblStoreID" runat="server" CssClass="copy10grey"></asp:Label>   
                    </td>

                </tr>
                </table>
                </td>
                </tr>
                </table>
                
                <%--<br />
                <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="99%" align="center">
                <tr bordercolor="#839abf">
                <td>
                <table width="100%" align="center" cellpadding="3" cellspacing="3" border="0" >
                
                <tr>
                    <td class="copy10grey" align="right" width="17%">
                        Ship By:    
                    </td>
                    <td width="32%">

                    
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
                --%>
                
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
                       <asp:dropdownlist id="dpState" tabIndex="6" runat="server" cssclass="copy10grey">
                       </asp:dropdownlist>

                       <%--<asp:TextBox ID="txtState" Width="30" MaxLength="2" CssClass="copy10grey" runat="server"></asp:TextBox>--%>

                       &nbsp;  &nbsp;  &nbsp; &nbsp;Zip:&nbsp;&nbsp;&nbsp;&nbsp;  
                       <asp:TextBox ID="txtZip" Width="90" onkeypress="return alphaNumericCheck(event);"  MaxLength="6" CssClass="copy10grey" runat="server"></asp:TextBox>
                       
                       <%--<asp:Label ID="lblState" runat="server" CssClass="copy10grey"></asp:Label>   --%>
                    </td>
                </tr>
                <tr>
                    <td>

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
                        Items/Container:    
                    </td>
                    <td width="32%">

                       &nbsp; <asp:TextBox ID="txtItemsPerContainer" onkeypress="return isNumberKey(event);" Width="200" MaxLength="5" CssClass="copy10grey" runat="server"></asp:TextBox>
                
                    </td>
                    <td width="2%">
                    &nbsp;
                    </td>
                    <td class="copy10grey" align="right" width="17%">
                        Container/Pallets:    
                    </td>
                    <td width="32%">

                       &nbsp; <asp:TextBox ID="txtContainersPerPallet"  onkeypress="return isNumberKey(event);"  MaxLength="5" Width="200" CssClass="copy10grey" runat="server"></asp:TextBox>
                
                    </td>


                </tr>
               </table>
                    </td>
                    </tr>
                    </table>
                 <br />   
                   <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="99%"  align="center">
                   <tr bordercolor="#839abf">
                    <td>
                        <table width="100%" bgColor="#ffffff" cellpadding="3" cellspacing="3"  align="center">
                            <tr  height="5">
                                <td  >
                                
                                </td>
                            </tr>
                            <tr >
                                <td class="copy10grey" align="left">
                                    &nbsp;Comments:
                                    <br />
                                    &nbsp;<asp:TextBox ID="txtCommments" CssClass="copy10grey" Width="95%" TextMode="MultiLine" Height="40"  runat="server"
onkeydown="checkTextAreaMaxLength(this,event,'5000');" onkeyup="checkTextAreaMaxLength(this,event,'5000');" onblur="onBlurTextCounter(this,'5000');"
></asp:TextBox>
                                </td>
                            </tr>
                            <tr  height="5">
                                <td  >
                                
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
                      
                        <asp:Button ID="btnSubmit" Visible="false" runat="server"  OnClick="btnEditPO_Click" Text="Submit" OnClientClick="return ValidateEditPo();"  CssClass="button" />&nbsp;
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
                        Item Code:    
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
                <tr style="display:none">
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
                                <asp:ListItem Text="In Process" Value="8"></asp:ListItem>
                                <asp:ListItem Text="Partial Processed" Value="10"></asp:ListItem>
                                <asp:ListItem Text="Partial Shipped" Value="11"></asp:ListItem>
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
                         <asp:Button ID="btnCancelPOD" runat="server" Text="Cancel" CssClass="button"  />
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
                                    <asp:ListItem Text="Download Fulfillment for ESN(NEW)" Value="7"></asp:ListItem>
<asp:ListItem Text="Download Tracking - Assignment" Value="8"></asp:ListItem>
                                </asp:DropDownList>   
                            </td>
                            <td width="33%" align="left">
                                <asp:Button ID="btnDownload" runat="server" OnClientClick="return IsValidateDnw();" OnClick="btnDownload_Click" Text="Download" CssClass="button" />
                                <%--<asp:Button ID="btnClose3" runat="server" Text="Cancel" CssClass="button" />--%>

                            </td>

                        </tr>
                        <tr>
                            <td class="copy10grey"  align="left" colspan="2">
                             &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;   
                                <asp:Label ID="lblRecieve" CssClass="copy10grey" runat="server" Text="Assign RECEIVED status to Pending order:"></asp:Label> &nbsp;
                                <asp:CheckBox ID="chkRecieved" CssClass="copy10grey" onclick="ValidateRecievedStatus(this);" runat="server" />                            </td>
                            <td>
                     
                            </td>
                            <%--<td>
                            
                            </td>--%>
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
        <div id="divLabel"  style="display:none">
            <asp:UpdatePanel ID="upLabel" runat="server">
				<ContentTemplate>
                    <input type="button" onclick="closeLabelDialog();" value="Close" class="buybt" style="float:right" />
                     &nbsp; &nbsp;
                    <input type="button" onclick="PrintDiv();" value="Print" class="buybt" style="float:right" />
                    
                    

                    <div id="divLabelImg">
                        <asp:Image ID="imgLabel" ImageUrl="~/warning.gif"  runat="server" />
                    </div>
                    </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div id="divStore"  style="display:none">
            <asp:UpdatePanel ID="upPnlStore" runat="server">
				<ContentTemplate>
                   
					<asp:PlaceHolder ID="phrStore" runat="server">
                
                <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="99%" align="center">
                <tr bordercolor="#839abf">
                <td>

                <table width="100%" align="center" cellpadding="1" cellspacing="1">
                <tr>
                    <td colspan="2">
                        <asp:Label ID="lblStMsg" runat="server" CssClass="errormessage"></asp:Label>   
                    </td>
                </tr>
                <tr>
                    <td class="copy10grey" align="right" width="40%">
                        Store ID:    
                    </td>

                    <td>
                        &nbsp; <asp:Label ID="lblStID" runat="server" CssClass="copy10grey"></asp:Label>   
                    </td>

                </tr>
                
                <tr>
                    <td class="copy10grey" align="right" width="40%">
                        Store Name:    
                    </td>

                    <td>
                        &nbsp; <asp:Label ID="lblStName" runat="server" CssClass="copy10grey"></asp:Label>   
                    </td>

                </tr>
                
                <tr>
                    <td class="copy10grey" align="right" width="40%">
                        Contact Name:    
                    </td>

                    <td>
                        &nbsp; <asp:Label ID="lblStCName" runat="server" CssClass="copy10grey"></asp:Label>   
                    </td>

                </tr>
                <tr>
                    <td class="copy10grey" align="right" width="40%">
                       Street Address:    
                    </td>
                    <td>
                       &nbsp; <asp:Label ID="lblStAdd" runat="server" CssClass="copy10grey"></asp:Label>   
                    </td>

                </tr>
                <tr>
                    <td class="copy10grey" align="right" width="40%">
                        City:    
                    </td>
                    <td>

                       &nbsp; <asp:Label ID="lblStCity" runat="server" CssClass="copy10grey"></asp:Label>   
                    </td>

                </tr>
                <tr>
                    <td class="copy10grey" align="right" width="40%">
                        State:    
                    </td>
                    <td>

                       &nbsp; <asp:Label ID="lblStState" runat="server" CssClass="copy10grey"></asp:Label>   
                       
                    </td>
                </tr>
                <tr>
                    <td class="copy10grey" align="right" width="40%">
                        Zip:    
                    </td>
                    <td>

                       &nbsp; <asp:Label ID="lblStZip" runat="server" CssClass="copy10grey"></asp:Label>   
                       
                       
                      
                    </td>
                </tr>
                <tr>
                    <td class="copy10grey" align="right" width="40%">
                        Country:    
                    </td>
                    <td>

                       &nbsp; <asp:Label ID="lblStCountry" runat="server" CssClass="copy10grey"></asp:Label>   
                       
                       
                      
                    </td>
                </tr>
                
                <tr>
                    <td class="copy10grey" align="right" width="40%">
                        Contact Phone:    
                    </td>
                    <td>

                       &nbsp; <asp:Label ID="lblStPhone" runat="server" CssClass="copy10grey"></asp:Label>   
                       
                       
                      
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
       
          <div id="divComments" style="display:none">
					
				<asp:UpdatePanel ID="upComments" runat="server">
					<ContentTemplate>
                   
						<asp:PlaceHolder ID="phComments" runat="server">
                            
                        <table bordercolor="#839abf" border="1" width="98%" align="center" cellpadding="0" cellspacing="0">
                                <tr><td>

                                <table border="0" width="100%" class="box" align="center" cellpadding="5" cellspacing="5">
                                <tr valign="top">
                                    <td>
                                    <strong> Fulfillment#: &nbsp;&nbsp;&nbsp;<asp:Label ID="lblFNum" runat="server" CssClass="copy10grey"></asp:Label></strong>
                                    </td>
                                </tr>
                                </table>
                                </td>
                                </tr>
                        </table>
                        <br />
                        <asp:Panel ID="pnlComments" runat="server">
                        
                            <%--<table bordercolor="#839abf" border="1" width="98%" align="center" cellpadding="0" cellspacing="0">
                                    <tr><td>

                                    <table border="0" width="100%" class="box" align="center" cellpadding="5" cellspacing="5">
                                    <tr valign="top">
                                        <td>--%>
                                            <UC:Comments ID="c1" runat="server" />
                                        <%--</td>
                                    </tr>
                                    </table>
                                    </td>
                                    </tr>
                            </table>--%>
                            </asp:Panel>
                    </asp:PlaceHolder>
						
						
					</ContentTemplate>
							
				</asp:UpdatePanel>
        </div>
         <div id="divPOA" style="display:none">
					
			<asp:UpdatePanel ID="upPnlPOA" runat="server">
					<ContentTemplate>
                   
						<asp:PlaceHolder ID="pnrPOA" runat="server">
                            <asp:Label ID="lblPOA" runat="server"  CssClass="errormessage"></asp:Label>
                            <table width="98%" align="center" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td class="copy10grey" align="left" >
                                      <strong>  Fulfillment#: &nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="lblAssignPO" runat="server" CssClass="copy10grey"></asp:Label></strong>
                                        
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                         <%--OnClick="btnESNDelete_Click"--%>
                                        <asp:Button ID="btnESNDelete" Visible="false"   CssClass="button" runat="server" Text="Unassign ESN"  />

                                        <asp:Button ID="btnPrint" Visible="false"   CssClass="button" runat="server" OnClick="btnPrint_Click" Text="Generate Label"  />

                                         &nbsp;<a id="lnk_Print"  href="#" style="height:30px !important; line-height:40px !important; width:150px" class="button" Visible="false" target="_blank" runat="server"><span style="height:30px !important; line-height:40px !important; width:150px" class="button"> Print </span></a>

                                    </td>
                                </tr>
                            
<tr>
    <td colspan="2" class="copy10grey">

    <asp:GridView ID="gvPOA"  BackColor="White" Width="100%" Visible="true" 
        AutoGenerateColumns="false" Font-Names="Verdana" runat="server" 
        GridLines="Both" OnRowDataBound="gvPOA_RowDataBound"
        BorderStyle="Double" BorderColor="#0083C1">
        <RowStyle BackColor="Gainsboro" />
        <AlternatingRowStyle BackColor="white" />
        <HeaderStyle  CssClass="buttongrid" ForeColor="white"/>
        <FooterStyle CssClass="white"  />
        <Columns>
         	   
            <asp:TemplateField HeaderText="SKU" SortExpression="sku" ItemStyle-CssClass="copy10grey"  HeaderStyle-Width="25%" ItemStyle-Wrap="false"  ItemStyle-width="10%">
                <ItemTemplate>
                    <%# Eval("SKU")%>
                </ItemTemplate>
                
            </asp:TemplateField>
            <%--<asp:TemplateField HeaderText="UPC" SortExpression="UPC"  ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                <ItemTemplate><%#Eval("UPC")%></ItemTemplate>
            </asp:TemplateField>
            --%>                                                                                                                        
                                            
                                            
            <asp:TemplateField HeaderText="Quantity" SortExpression="Qty" ItemStyle-CssClass="copy10grey" ItemStyle-Width="25%">
                <ItemTemplate>
                    <%# Eval("Quantity")%>
                </ItemTemplate>
               <%-- <EditItemTemplate>
                                                    <asp:TextBox ID="txtEsn" CssClass="copy10grey" MaxLength="35"  Text='<%# Eval("ESN") %>' runat="server"></asp:TextBox>
                </EditItemTemplate>--%>
                                                
            </asp:TemplateField>
                                            

            <asp:TemplateField HeaderText="Assigned Qty" SortExpression="AssignedQty" ItemStyle-Width="25%">
                <ItemTemplate>
                    <%--<asp:LinkButton ID="lnkQty" runat="server" CssClass="linkgrey" 
                        CommandArgument='<%# Eval("POD_ID") %>'  OnCommand="lnkESN_Command" AlternateText="View ESN"> --%>

                       <b style="text-decoration:underline"><%# Convert.ToInt32(Eval("AssignedQty")) == 0 ? "" : Eval("AssignedQty") %></b>  
                <%--</asp:LinkButton>--%>
                    
                </ItemTemplate>
               <%-- <EditItemTemplate><%#Eval("AssignedQty")%>
                    <asp:TextBox ID="txtMslNumber" CssClass="copy10grey" MaxLength="30"  Text='<%# Eval("MslNumber") %>' runat="server"></asp:TextBox>
                </EditItemTemplate>--%>
                                                
            </asp:TemplateField>
            
<%--<asp:TemplateField HeaderText="ICCID" SortExpression="ICCID"  ItemStyle-CssClass="copy10grey" ItemStyle-Width="25%">
                <ItemTemplate><%#Eval("LTEICCID")%></ItemTemplate>
            </asp:TemplateField>--%>
                              
            <asp:TemplateField HeaderText=""   ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
                <ItemTemplate>
                    
                    <asp:CheckBox ID="chkDel" Visible='<%#Eval("IsDelete")%>' runat="server" />
                    <asp:HiddenField ID="hdPODID" runat="server" Value='<%#Eval("POD_ID")%>' />

                </ItemTemplate>
            </asp:TemplateField>
                                            
                
            <%--<asp:CommandField HeaderText="Edit" ShowEditButton="True" HeaderStyle-CssClass="button" ControlStyle-CssClass="linkgrid"    ItemStyle-HorizontalAlign="Center"/>
		
            <asp:TemplateField HeaderText=""  ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
             
            
             <asp:ImageButton ToolTip="Edit PODetail" CausesValidation="false" OnCommand="img2EditPOD_Click" CommandArgument='<%# Eval("PodID") %>' ImageUrl="~/Images/edit.png" 
             ID="imgEditPOD"  runat="server" />
             <asp:ImageButton ID="imgDelPoD" runat="server" OnClientClick="return confirm('Are you sure you want to delete?')"  
             CommandName="Delete" AlternateText="Delete POD" ImageUrl="~/images/delete.png" />
                                                        
                    </ItemTemplate>
                    </asp:TemplateField>   --%>                                                
			                                
        </Columns>
    </asp:GridView>
    </td>
</tr>
</table>
                
                        </asp:PlaceHolder>
					</ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnPrint" />
                </Triggers>
				</asp:UpdatePanel>
    </div>  
        
        <div id="divSKUESN" style="display:none">
					
				<asp:UpdatePanel ID="upnlSKUESN" runat="server">
					<ContentTemplate>
                        <asp:PlaceHolder ID="phSKUESN" runat="server">
           
                            <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="100%" align="center" >
                           <tr bordercolor="#839abf">
                                <td>
                                    
                                        
                            <asp:Label ID="lblSKUESN" runat="server" ></asp:Label>
                                        <asp:Repeater ID="rptSKUESN" runat="server">
                                            <HeaderTemplate>
                                            <table width="100%" align="center">
                                                <tr >
                                                    <td class="buttongrid" width="2%">
                                                        &nbsp;S.No.
                                                    </td>
                                                    <td class="buttongrid">
                                                        &nbsp;ESN
                                                    </td>
                                                    <td class="buttongrid">
                                                        &nbsp;ICCID
                                                    </td>
                                                    <td runat="server" visible='<%# ContainerID == "" ? false:true%>' class="buttongrid" >
                                                        &nbsp;ContainerID
                                                    </td>
                                                </tr>
                            
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                            <tr class="<%# Container.ItemIndex % 2 == 0 ? "alternaterow" : "" %>">
                                                
                                                <td class="copy10grey">
                                                        &nbsp;<%# Container.ItemIndex + 1 %></td>
                                
                                                <td class="copy10grey">
                                                        &nbsp;<%# Eval("ESN")%></td>
                                                <td class="copy10grey">
                                                        &nbsp;<%# Eval("ICCID")%></td>
                                                <td class="copy10grey" runat="server" visible='<%# Convert.ToString(Eval("ContainerID")) == "" ? false:true%>' >
                                                        &nbsp;<%# Eval("ContainerID")%></td>
                                
                                            </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                </table>    
                                            </FooterTemplate>
                                            </asp:Repeater>
                        
                            </td>
                                        </tr>
                                        </table>

                            </asp:PlaceHolder>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                
            </div>
      <div id="divShipItems" style="display:none">
					
			<asp:UpdatePanel ID="upShipItem" runat="server">
					<ContentTemplate>
                   
						<asp:PlaceHolder ID="phShipItem" runat="server">
                            <asp:Label ID="lblShipItem" runat="server"  CssClass="errormessage"></asp:Label>
                            <table bordercolor="#839abf" border="1" width="98%" align="center" cellpadding="0" cellspacing="0">
                                <tr><td>

                                <table border="0" width="100%" class="box" align="center" cellpadding="5" cellspacing="5">
                                <tr valign="top">
                                    <td class="copy10grey" align="right" width="17%">
                                       <strong> Fulfillment#: </strong>
                                    </td>
                                    <td width="34%">

                                     <strong>   <asp:Label ID="lblShipPO" runat="server" CssClass="copy10grey"></asp:Label></strong>
                                        
                                    </td>
                                   
                                    <td class="copy10grey" align="right" width="17%">
                                       
                                    </td>
                                    <td width="32%" class="copy10grey" >
                                        
                                         </td>
                                    </tr>
                               
                                    <tr valign="top">
                                    <td class="copy10grey" align="right" width="17%">
                                        Requested Ship Date:
                                    </td>
                                    <td width="34%">

                                        <asp:TextBox ID="txtShippingDate" ContentEditable="false" Width="91%" onkeydown="return ReadOnlys(event);" runat="server" CssClass="copy10grey"></asp:TextBox>&nbsp;
                                        <img id="img3" alt=""  src="/fullfillment/calendar/sscalendar.jpg" runat="server"/>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="img3" 
                                        PopupPosition="BottomLeft" Format="MM/dd/yyyy"   TargetControlID="txtShippingDate">
                                        </ajaxToolkit:CalendarExtender>
                                    </td>
                                   
                                    <td class="copy10grey" align="right" width="17%">
                                        Ship Via:
                                    </td>
                                    <td width="32%" class="copy10grey" >
                                         <asp:DropDownList ID="ddlShipVia" runat="server" Width="91%" CssClass="copy10grey"></asp:DropDownList>
                                       
                                         </td>
                                </tr>
                                    <tr>
                                        <td class="copy10grey" align="right" width="17%">
                                            Shipping Weight(Oz):
                                        </td>
                                        <td  width="34%">
                                            
                                             <asp:TextBox ID="txtWeight" Width="51%" Text="1" MaxLength="6" onkeypress="return ValidateWeight(event, this);" CssClass="copy10grey" runat="server"></asp:TextBox>
                                        </td>
                                        
                                    <td class="copy10grey" align="right" width="17%">
                                           Ship Package
                                    </td>
                                    </td>
                                    <td width="32%" class="copy10grey" >
                                        <asp:DropDownList ID="ddlShape" runat="server" Width="91%" CssClass="copy10grey"></asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="copy10grey" align="right" width="17%">
                                            Add Manual Tracking:    
                                        </td>
                                        <td  width="34%">
                                            <asp:CheckBox ID="chkTracking"   runat="server" onclick="ShowTracking(this);" />
                                        </td>
                                        
                                    <td class="copy10grey" align="right" width="17%">
                                          Tracking Number:
                                    </td>
                                    <td width="32%" class="copy10grey" >
                                         <asp:TextBox ID="txtTrackingNumber" Width="91%" Enabled="false" onkeypress="return isNumberKey(event);" MaxLength="50" CssClass="copy10grey" runat="server"></asp:TextBox>
                                        
                                        <%--<asp:CheckBox ID="chkLabel" Visible="false" Checked="true" runat="server" />

                                        <asp:Button ID="btnGenLabel" runat="server" Visible="false" Text="Generate Label" OnClick="btnGenLabel_Click" CausesValidation="false" CssClass="button" />
                                            --%>
                                    </td>
                                    </tr>
                                    
                                </table>
                                </td>
                                </tr>
                        </table>
                            <br />
                             <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="98%"  align="center">
                   <tr bordercolor="#839abf">
                    <td>
                        <table width="100%" bgColor="#ffffff" cellpadding="3" cellspacing="3"  align="center">
                            <tr  height="5">
                                <td  >
                                
                                </td>
                            </tr>
                            <tr >
                                <td class="copy10grey" align="left">
                                    &nbsp;Comments:
                                    <br />
                                    &nbsp;<asp:TextBox ID="txtShipComments" CssClass="copy10grey" Width="95%" TextMode="MultiLine" Height="40"  runat="server"
                                    onkeydown="checkTextAreaMaxLength(this,event,'500');" onkeyup="checkTextAreaMaxLength(this,event,'500');" onblur="onBlurTextCounter(this,'500');"
                                    ></asp:TextBox>
                                </td>
                            </tr>
                            <tr  height="5">
                                <td  >
                                
                                </td>
                            </tr>
                        </table>
                        </td>
                    </tr>
                    </table>
                   
                <br />
                            <br />
 <table width="98%" align="center" cellpadding="0" cellspacing="0" style="display:none">
                            
<tr>
    <td colspan="2" class="copy10grey" >

    <asp:Repeater ID="rptShipItems" runat="server">
<HeaderTemplate>
<table border="0" width="100%" cellpadding="1" cellspacing="1">
<tr valign="top">
    <td align="left" class="buttongrid" width="2%">
        S.No.
    </td>
    <td align="center" class="buttongrid" width="40%">
       SKU
    </td>
    <td align="center" class="buttongrid" width="20%">
        Quantity
    </td>
    <%--<td width="10%"  align="center" class="button" >
        Generate Label
    </td>--%>
</tr>
</HeaderTemplate>
<ItemTemplate>
    <tr valign="top" class="<%# Container.ItemIndex % 2 == 0 ? "alternaterow" : "" %>">
    <td align="left" class="copy10grey" width="2%">
        <asp:CheckBox ID="chkSKU" runat="server" Checked='<%# Convert.ToInt32(Eval("Quantity")) == 0 ? false : true %>' CssClass="copy10grey" Enabled='<%# Convert.ToInt32(Eval("Quantity")) == 0 ? false : true %>' />
        <asp:HiddenField ID="hdnPODID" runat="server" Value='<%#Eval("PODID")%>' />
    </td>
    <td align="left" class="copy10grey" width="40%">
    
       <%#Eval("SKU")%>

    </td>
    <td align="left" class="copy10grey" width="20%">
         <asp:HiddenField ID="hdnQty" runat="server" Value='<%#Eval("Quantity")%>' />
        <asp:TextBox ID="txtQty" CssClass="copy10grey" MaxLength="5" onkeyup="OnKeyUp(this)"  Width="69%" Text='<%# Eval("Quantity") %>' runat="server"></asp:TextBox>
       
    </td>
    <%--<td width="10%" align="center" >
        <asp:ImageButton ID="imgGenLabel" runat="server" 
                        CommandArgument='S'  OnCommand="imgGeneratShipLabel_Command" ToolTip="Generate Label" AlternateText="Generate Label" 
                        ImageUrl="~/images/doc1.png" />
    </td>--%>
</tr>
</ItemTemplate>
<FooterTemplate>
</table>
</FooterTemplate>
</asp:Repeater>
        <br />
    </td>
</tr>
</table>

                <table width="100%" align="center">
                <tr>
                    <td align="center">
                         <asp:Button ID="btnShip" runat="server" OnClick="btnAddShip_Click" Text="Submit" CssClass="button" OnClientClick="return ValidateShipPo();" />&nbsp;
                         <asp:Button ID="btnShipCancel" runat="server" Text="Cancel" CssClass="button" OnClientClick="closeShipDialog()"  />
                    </td>
                </tr>
                </table>
	
                
                        </asp:PlaceHolder>
						
						
					</ContentTemplate>
							
				</asp:UpdatePanel>
    </div>  

    </div>

        <table cellSpacing="0" cellPadding="0" align="center" width="100%" border="0" id="maintbl">
        <tr>
			<td>
    <div id="winVP">
    <asp:UpdatePanel ID="UpdatePanel1"  runat="server" UpdateMode="Conditional" >
     <ContentTemplate>
     <table cellspacing="0" cellpadding="0"  border="0" align="center" width="100%">
	<tr>
	    <td align="center">
      	


    
    <table  cellSpacing="1" cellPadding="1" width="100%" align="center">
        <tr>
			<td colSpan="6" bgcolor="#dee7f6" class="buttonlabel" align="left">&nbsp;&nbsp;Fulfillment Search
			</td>
        </tr>
        <tr><td align="left"><asp:Label ID="lblMsg" runat="server" CssClass="errormessage"></asp:Label></td></tr>
    </table>

    <table  cellSpacing="1" cellPadding="1" width="100%">
                <tr><td class="copy10grey" align="left">&nbsp;
                - Please select your search
                  criterial to narrow down the search and record selection.<br />&nbsp;
                - Atleast one search criteria should be selected.<br />&nbsp;
                <%-- - Maximum of top 5000 records will be shown, if you want more please contact with Lan Global administrator.<br />--%>
                - Maximum of last one year records will be shown if not From and To dates are given.<br />&nbsp;
                - Fulfillment date can not be 1095 day before  from current date <br />&nbsp;
                - Fulfillment date can not be more than current date <br />&nbsp;
                
                </td></tr>
    </table>

    <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="99%" >

           <tr bordercolor="#839abf">
                <td>
                <asp:Panel  ID="pnlSearch" runat="server"  DefaultButton="btnSearch" >
                <table cellSpacing="1" cellPadding="1" width="100%"  >
                <tr width="6">
                <td width="6">
                    &nbsp;
                </td>
                </tr>
                   
                    <tr>
                <td align="right" class="copy10grey" width="15%">
                    </td>
                <td width="1%">
                </td>
                <td align="left"  width="29%">
                    </td>
                <asp:Panel ID="pnlCompany" runat="server" Visible="false">
                <td align="right" class="copy10grey">
                    Company:</td>
                <td></td>
                <td align="left">
                    <asp:DropDownList ID="dpCompany" runat="server" class="copy10grey"  Width="81%">
                                
                            </asp:DropDownList>
                </td>
                </asp:Panel>
            </tr>            
            <tr>
                <td align="right" class="copy10grey" width="15%">
                    Fulfillment#:</td>
                <td width="1%">
                </td>
                <td align="left"  width="29%">
                    <asp:TextBox ID="txtPONum"  Width="80%" onkeypress="JavaScript:return alphaOnly(event);" MaxLength="20" runat="server" CssClass="copy10grey" ></asp:TextBox></td>
                <td align="right" class="copy10grey">
                   Customer Order#: </td>
                <td></td>
                <td align="left">
                    <asp:TextBox ID="txtCustOrderNo"  Width="80%" onkeypress="JavaScript:return alphaOnly(event);" MaxLength="20" runat="server" CssClass="copy10grey" ></asp:TextBox>
                
                </td>
                
                
            </tr>
            <tr>
                <td align="right" class="copy10grey" >
                    Fulfillment Date From:</td>
                <td>
                </td>
                <td align="left">
                    <asp:TextBox ID="txtFromDate" runat="server" onfocus="set_focus1();"  CssClass="copy10grey" MaxLength="15"  Width="80%"></asp:TextBox>
                    <img id="imgFromtDate" alt="" onclick="document.getElementById('<%=txtFromDate.ClientID%>').value = ''; displayCalendar(document.getElementById('<%=txtFromDate.ClientID%>'),'mm/dd/yyyy',this,true);" src="../fullfillment/calendar/sscalendar.jpg" />
                </td>
                    <td align="right" class="copy10grey">Fulfillment Date To:</td>
                <td></td>
                <td align="left">
                    <asp:TextBox ID="txtToDate" runat="server" onfocus="set_focus2();"  CssClass="copy10grey" MaxLength="15"  Width="80%"></asp:TextBox>
                    <img id="imgToDate"  alt="" onclick="document.getElementById('<%=txtToDate.ClientID%>').value = ''; displayCalendar(document.getElementById('<%=txtToDate.ClientID%>'),'mm/dd/yyyy',this,true);" src="../fullfillment/calendar/sscalendar.jpg" />
                
                </td>               
            </tr>
            <tr>
                <td align="right" class="copy10grey">
                    Shipping Date From:</td>
                <td>
                </td>
                <td align="left">
                    <asp:TextBox ID="txtShipFrom" runat="server" onfocus="set_focus3();" CssClass="copy10grey" MaxLength="15"  Width="80%"></asp:TextBox>
                    <img id="img1" alt="" onclick="document.getElementById('<%=txtShipFrom.ClientID%>').value = ''; displayCalendar(document.getElementById('<%=txtShipFrom.ClientID%>'),'mm/dd/yyyy',this,true);" src="../fullfillment/calendar/sscalendar.jpg" />
                </td>
                    <td align="right" class="copy10grey">Shipping Date To:</td>
                <td></td>
                <td align="left">
                    <asp:TextBox ID="txtShipTo" runat="server" onfocus="set_focus4();"  CssClass="copy10grey" MaxLength="15"  Width="80%"></asp:TextBox>
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
                                <asp:ListItem Text="In Process" Value="8"></asp:ListItem>
                                <asp:ListItem Text="Partial Processed" Value="10"></asp:ListItem>
                                <asp:ListItem Text="Partial Shipped" Value="11"></asp:ListItem>
                                <asp:ListItem Text="Processed" Value="2"></asp:ListItem>
                                <asp:ListItem Text="Shipped" Value="3"></asp:ListItem>
                                <asp:ListItem Text="Closed" Value="4"></asp:ListItem>
                                <asp:ListItem Text="Return" Value="5"></asp:ListItem>
                                <asp:ListItem Text="Cancel" Value="9"></asp:ListItem>
<asp:ListItem Text="On Hold" Value="6"></asp:ListItem>
                                <asp:ListItem Text="Out of Stock" Value="7"></asp:ListItem>
                                
                            </asp:DropDownList>
                </td>                
                <td align="right" class="copy10grey">
                      Store ID:
                   </td>
                <td></td>
                <td align="left">
                     <asp:TextBox ID="txtStoreID" runat="server" Width="80%" onkeypress="JavaScript:return alphaOnly(event);"  CssClass="copy10grey" MaxLength="20"></asp:TextBox>
                     <asp:DropDownList ID="ddlUserStores"  Width="80%" runat="server"  class="copy10grey">                                
                    </asp:DropDownList>
                    <%--<asp:TextBox ID="txtAvNumber"  Width="80%" onkeypress="JavaScript:return alphaOnly(event);" runat="server"  CssClass="copy10grey" MaxLength="20"></asp:TextBox>--%>
                </td>
            </tr>
            
            <tr>
                <td align="right" class="copy10grey">
                    ESN:</td>
                <td></td>
                <td align="left">
                    <asp:TextBox ID="txtEsn" Width="80%" onkeypress="JavaScript:return alphaOnly(event);" runat="server"  CssClass="copy10grey" MaxLength="20"></asp:TextBox>
                </td>
                  
                <td align="right" class="copy10grey">
                    Tracking#:
                    </td>
                <td></td>
                <td align="left">
                   <asp:TextBox ID="txtSTrackingNo" Width="80%" runat="server"  CssClass="copy10grey" MaxLength="30"></asp:TextBox>
                </td> 

            </tr> 

            <tr>
                <td align="right" class="copy10grey">
                    Fulfillment Type:</td>
                <td></td>
                <td align="left">
                      <asp:DropDownList ID="dpPOType" Width="81%" runat="server"  class="copy10grey">
                        <asp:ListItem Text="" Value=""></asp:ListItem> 
                        <asp:ListItem Text="B2B" Value="B2B"></asp:ListItem> 
                        <asp:ListItem Text="B2C" Value="B2C"></asp:ListItem> 
                    </asp:DropDownList>
                </td>
                  
                <td align="right" class="copy10grey">
                    Contact Name:</td>
                <td></td>
                <td align="left">
                   <asp:TextBox ID="txtSContactName" runat="server" Width="80%" onkeypress="JavaScript:return alphaOnly(event);" CssClass="copy10grey" MaxLength="50"></asp:TextBox>
                </td> 

            </tr> 
            
            <%--<tr>
                <td align="right" class="copy10grey">
                    Item Code:</td>
                <td></td>
                <td align="left">
                    <asp:TextBox ID="txtItemCode" runat="server" Width="80%" onkeypress="JavaScript:return alphaOnly(event);" CssClass="copy10grey" MaxLength="20"></asp:TextBox>
                </td>
	
<td></td>
	<td></td>
	<td></td>
                                 
            </tr>--%>
           <%-- <tr>
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
            </tr>--%>
            <tr>
                <td colspan="3" align="left">
                    <asp:CheckBox ID="chkDownload"  runat="server" Text="Download selected records only" CssClass="copy10grey" />

                </td>
                <td align="right" class="copy10grey">
                    SKU:</td>
                <td></td>
                <td align="left">
                   <asp:TextBox ID="txtSKU" runat="server" Width="80%" onkeypress="JavaScript:return alphaOnly(event);" CssClass="copy10grey" MaxLength="25"></asp:TextBox>
                </td> 

            </tr>
            <tr><td colspan="6"><hr /></td></tr>
            <tr><td colspan="6" align="center">
                <div class="loadingcss" align="center" id="modalSending">
                        <img src="/Images/ajax-loaders.gif" alt=""  /> Loading...
                    </div> 
            <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Search Fulfillment" OnClick="btnSearch_Click" OnClientClick="return ShowSendingProgress();" />&nbsp;
            &nbsp;<asp:Button ID="btnCancel" runat="server" CssClass="button" Text="Cancel Search" OnClick="btnCancel_Click" />
            &nbsp;<asp:Button ID="btnChangeStatus" runat="server" CssClass="button" Text="Change Status" OnClick="btnChangeStatus_Click" Visible="False" />
            &nbsp;<asp:Button ID="btnDownloadData"  runat="server" Visible="False" CssClass="button" Text="Download Fulfillment Data " OnClientClick="openDownloadDialog('Download Fulfillment Data','btnDownloadData')" />
            &nbsp;<asp:Button ID="btnPackSlipAll" runat="server" Visible="false" CssClass="button" Text="Packing Slip" OnClick="btnPackSlipAll_Click" />
            &nbsp;<asp:Button ID="btnASN" runat="server" Visible="false" CssClass="button" Text="ASN-1 File" OnClick="btnASN_Click" />
            
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
                                <asp:ListItem Text="In Process" Value="8"></asp:ListItem>
                                <%--<asp:ListItem Text="Partial Processed" Value="10"></asp:ListItem>
                                <asp:ListItem Text="Partial Shipped" Value="11"></asp:ListItem>
                                <asp:ListItem Text="Processed" Value="2"></asp:ListItem>
                                <asp:ListItem Text="Shipped" Value="3"></asp:ListItem>--%>
                                <asp:ListItem Text="Closed" Value="4"></asp:ListItem>
                                <asp:ListItem Text="Return" Value="5"></asp:ListItem>
                                <asp:ListItem Text="Cancel" Value="9"></asp:ListItem>
                                <asp:ListItem Text="On Hold" Value="6"></asp:ListItem>
                                <asp:ListItem Text="Out of Stock" Value="7"></asp:ListItem>
                                
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
                    </asp:Panel>
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
            <td align="right" >
                
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
            AllowSorting="true" OnSorting="gvPOQuery_Sorting" > 
            <RowStyle BackColor="Gainsboro" />
            <AlternatingRowStyle BackColor="white" />
            <HeaderStyle  CssClass="buttongrid" ForeColor="white"/>
            <FooterStyle CssClass="white"  />
               <SortedAscendingHeaderStyle  Font-Underline="true" />
               <SortedDescendingHeaderStyle   Font-Underline="true" />
            <PagerStyle ForeColor="#636363" CssClass="copy10grey" HorizontalAlign="Left" />
            <Columns>
               <%-- <asp:ButtonField   ButtonType="Image"  ImageUrl ="../images/plus.gif"
                    CommandName="sel"  />--%>
                <asp:TemplateField ItemStyle-CssClass="copy10grey"  ItemStyle-Width="1%">
                    <ItemTemplate>
                        <asp:CheckBox ID="chk"  runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>  
                <asp:TemplateField HeaderText="S.No." ItemStyle-CssClass="copy10grey" ItemStyle-HorizontalAlign="Right"  ItemStyle-Width="1%" HeaderStyle-CssClass="buttongrid">
                    <ItemTemplate>

                            <%# Container.DataItemIndex + 1%> &nbsp;
                  
                    </ItemTemplate>
                    </asp:TemplateField>          
                <asp:TemplateField HeaderText="Type"  ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
                    <ItemTemplate><%#Eval("POType")%></ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="Fulfillment#" HeaderStyle-CssClass="buttonundlinelabel" SortExpression="PurchaseOrderNumber"  ItemStyle-CssClass="copy10grey" 
                    ItemStyle-Width="8%">
                    <ItemTemplate>
                        <asp:Label ID="lblPoNum" runat="server" Text='<%# Eval("PurchaseOrderNumber") %>'></asp:Label>   
                        <asp:HiddenField ID="hdnCANo" runat="server" Value='<%# Eval("CustomerAccountNumber") %>' />                     
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Customer Order#" HeaderStyle-CssClass="buttonundlinelabel" SortExpression="PurchaseOrderNumber"  ItemStyle-CssClass="copy10grey" 
                    ItemStyle-Width="8%">
                    <ItemTemplate>
                        <%# Eval("CustomerOrderNumber") %>
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="Fulfillment Date" HeaderStyle-CssClass="buttonundlinelabel"  SortExpression="PurchaseOrderDate" ItemStyle-CssClass="copy10grey"  
                    ItemStyle-Width="5%">
                    <ItemTemplate><%# DataBinder.Eval(Container.DataItem, "PurchaseOrderDate", "{0:d}") %></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Requested Shipping Date"   ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "RequestedShipDate", "{0:d}") %>
                    
                    </ItemTemplate>
                </asp:TemplateField>
<asp:TemplateField HeaderText="Shipping Date"   ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="copy10grey" ItemStyle-Width="7%">
                    <ItemTemplate>
                        <asp:Label ID="lblShippDate" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "Tracking.ShipToDate", "{0:d}"))=="1/1/0001"? "": DataBinder.Eval(Container.DataItem, "Tracking.ShipToDate", "{0:d}")%>'></asp:Label>                        
                    
                    </ItemTemplate>
                </asp:TemplateField>


                <asp:TemplateField HeaderText="Customer" HeaderStyle-CssClass="buttonundlinelabel"  SortExpression="CustomerName"  ItemStyle-HorizontalAlign="left" 
                    ItemStyle-CssClass="copy10grey" ItemStyle-Width="6%">
                    <ItemTemplate>
                    <%# Eval("CustomerName")%>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Contact Name"  ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                    <ItemTemplate><%# Eval("Shipping.ContactName")%></ItemTemplate>
                    
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Contact Phone"  ItemStyle-CssClass="copy10grey" ItemStyle-Width="7%">
                    <ItemTemplate><%# Eval("Shipping.ContactPhone") %></ItemTemplate>
                    
                </asp:TemplateField>
                <%--
                <asp:TemplateField HeaderText="Ship By" SortExpression="ShipToBy" ItemStyle-CssClass="copy10grey"  ItemStyle-Width="7%">
                    <ItemTemplate>
                    <%# Convert.ToString(Eval("Tracking.ShipToBy")).ToUpper() %>
                    
                    
                    </ItemTemplate>
                    
                </asp:TemplateField>
               
               

                <asp:TemplateField HeaderText="Tracking#" SortExpression="ShipToTrackingNumber" ItemStyle-CssClass="copy10grey"  ItemStyle-Width="7%">
                    <ItemTemplate><%# Eval("Tracking.ShipToTrackingNumber")%></ItemTemplate>
                    
                </asp:TemplateField>   
               
               
                <asp:TemplateField HeaderText="Shipping Date" SortExpression="ShipToDate"  ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                    <ItemTemplate>
<asp:Label ID="lblShippDate" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "Tracking.ShipToDate", "{0:d}"))=="1/1/0001"? "": DataBinder.Eval(Container.DataItem, "Tracking.ShipToDate", "{0:d}")%>'></asp:Label>                        
                    
                    </ItemTemplate>
                </asp:TemplateField>                                  --%>
                
<%--               <asp:TemplateField HeaderText="Requested Shipping Date"   ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "RequestedShipDate", "{0:d}") %>
                    
                    </ItemTemplate>
                </asp:TemplateField>
                --%>
                <asp:TemplateField HeaderText="Store ID" HeaderStyle-CssClass="buttonundlinelabel"  SortExpression="StoreID"  ItemStyle-HorizontalAlign="Left" 
                    ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                    <ItemTemplate>
                    <asp:LinkButton ID="lnkStore" runat="server" CssClass="copy10underline" 
                        CausesValidation="false" Text='<%#Eval("StoreID")%>' OnCommand="lnkStore_OnCommand" CommandArgument='<%# Eval("StoreID") +","+Eval("CompanyID") %>' >
                        
                        <%#Eval("StoreID")%></asp:LinkButton>
                        
                    
                    </ItemTemplate>
                </asp:TemplateField> 

                <asp:TemplateField HeaderText="Street Address"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="20%">
                    <ItemTemplate><%#Eval("Shipping.ShipToAddress")%> <%#Eval("Shipping.ShipToAddress2")%>
                    <%#Eval("Shipping.ShipToCity")%>
                    <%#  Convert.ToString(Eval("Shipping.ShipToState")).ToUpper() %> 
                    <%#Eval("Shipping.ShipToZip")%>
                    
                    
                    
                    </ItemTemplate>
                </asp:TemplateField> 
                <%--<asp:TemplateField HeaderText="State"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                    <ItemTemplate>
                    <%#  Convert.ToString(Eval("Shipping.ShipToState")).ToUpper() %>
                    </ItemTemplate>
                </asp:TemplateField>                                              

                <asp:TemplateField HeaderText="Zip"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                    <ItemTemplate>
                    <%#Eval("Shipping.ShipToZip")%>
                    
                    </ItemTemplate>
                </asp:TemplateField>  
--%>
                <asp:TemplateField HeaderText="Status" HeaderStyle-CssClass="buttonundlinelabel"  SortExpression="PurchaseOrderStatus"  ItemStyle-HorizontalAlign="Left" 
                    ItemStyle-CssClass="copy10grey" ItemStyle-Width="3%">
                    <ItemTemplate>                    
                    <table cellpadding="3" cellspacing="3" style="width:100%; background-color:<%#Eval("StatusColor")%>; height:100%">
                    <tr>
                    <td>
                        <%#Eval("PurchaseOrderStatus")%>
                    </td>
                    </tr>
                    </table>
                     <asp:HiddenField ID="hdOrderSent" Value='<%# Eval("OrderSent") %>' runat="server" />
                    <asp:HiddenField ID="hdnStatus" Value='<%#Eval("PurchaseOrderStatusID")%>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>   
                
                <asp:TemplateField HeaderText="Line Item Count"  ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                    <ItemTemplate><%#Eval("LineItemCount")%></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Source"  ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
                    <ItemTemplate><%#Eval("POSource")%></ItemTemplate>
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
                <asp:TemplateField HeaderText=""  ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="12%"  ItemStyle-Width="13%">
                    <ItemTemplate>
                    
                    <table width="100%">
                        <tr>
                        <td>
                            <asp:ImageButton ID="imgPO"  ToolTip="View PO" OnCommand="imgViewPO_OnCommand"  CausesValidation="false" 
                            CommandArgument='<%# Eval("PurchaseOrderID") %>' ImageUrl="~/Images/view.png"  runat="server" />
                        
                        </td>                        
                        <td>
                        
                            <asp:ImageButton ID="imgAdd" Visible='<%# Convert.ToInt32(Eval("PurchaseOrderStatusID")) == 1 ? true : false %>' ToolTip="Add line items" OnCommand="imgAddLineItem_Command"  CausesValidation="false" 
                            CommandArgument='<%# Eval("PurchaseOrderID") %>' ImageUrl="~/Images/plus.png"  runat="server" />
 
                            </td>
                        <td>    
                        <asp:ImageButton ToolTip="Edit PO" CausesValidation="false" OnCommand="imgEditPO_OnCommand" CommandArgument='<%# Eval("PurchaseOrderID") %>' 
                        ImageUrl="~/Images/edit.png" ID="imgEdit"  runat="server" />
                        
                        </td>
                             <td>
                                <asp:ImageButton ID="imgShip"  ToolTip="Add Shiping" OnCommand="imgShip_OnCommand"  CausesValidation="false" 
                            CommandArgument='<%# Eval("PurchaseOrderID") %>' ImageUrl="~/Images/ship.png"  runat="server" />
 
                            </td>
                            <td>
                                <asp:ImageButton ID="imgPOA"  ToolTip="Assign ESN" OnCommand="imgProvisioning_OnCommand"  CausesValidation="false" 
                            CommandArgument='<%# Eval("PurchaseOrderID") %>' ImageUrl="~/Images/view2.png"  runat="server" />
 
                            </td>
                            <td>
                                <asp:ImageButton ID="imgDoc"  ToolTip="View Doc" OnCommand="imgDoc_Command"  CausesValidation="false" 
                            CommandArgument='<%# Eval("CompanyID")+","+Eval("PurchaseOrderNumber") %>' ImageUrl="~/Images/doc1.png"  runat="server" />
 
                            </td>
                        <td>
                        <asp:ImageButton ID="imgDelPo" runat="server"  CommandName="Delete" AlternateText="Delete PO" ImageUrl="~/images/delete.png" />
                        </td>
                        </tr>
                        </table>
                        
                        
                        
                    </ItemTemplate>
                </asp:TemplateField>
                		    
			</Columns>
        </asp:GridView>          
            
            </td></tr>
             <tr><td style="width: 1032px"><br/></td></tr>
              <tr><td style="width: 1032px"><br/></td></tr>
        </table>

        

        



         <%--<script type='text/javascript'>


             prm = Sys.WebForms.PageRequestManager.getInstance();
             prm.add_endRequest(EndRequest);
             function EndRequest(sender, args) {
                 //alert("EndRequest");
                 $(document).AjaxReady();
             }
        </script>
        <asp:PostBackTrigger ControlID="btnChangeStatus" />
        --%>
    
        </td>
	</tr>
    </table>
        </ContentTemplate>
        <Triggers>
            
        <asp:PostBackTrigger ControlID="btnASN" />
        <asp:PostBackTrigger ControlID="btnPackSlipAll" />
            
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
            <asp:PostBackTrigger ControlID="btnPckSlip" />
            <asp:PostBackTrigger ControlID="btnContainerSlip" />

            
            
        </Triggers>
        </asp:UpdatePanel>
        </div>
    <asp:UpdatePanel ID="upnlJsRunner" UpdateMode="Always" runat="server">
		<ContentTemplate>
			<asp:PlaceHolder ID="phrJsRunner" runat="server"></asp:PlaceHolder>
		</ContentTemplate>
    </asp:UpdatePanel>
</td>
            </tr>
            </table>
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
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.4.0/jquery.min.js"></script>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/jquery-ui.min.js"></script>
        <script type="text/javascript" src="../JQuery/jquery.blockUI.js"></script>
        <script type="text/javascript" src="/JSLibrary/jquery.blockUI.js"></script>

        <script type="text/javascript">       

            function ShowSendingProgress() {
                
                var modal = $('<div  />');
                modal.addClass("modal");
                modal.attr("id", "modalSending");
                $('body').append(modal);
                var loading = $("#modalSending.loadingcss");
                loading.show();
                var top = '300px';
                var left = '820px';
                loading.css({ top: top, left: left, color: '#ffffff' });

                var tb = $("maintbl");
                tb.addClass("progresss");


                return true;
            }
            //background-color:#CF4342;

            function StopProgress() {

                $("div.modal").hide();

                var tb = $("maintbl");
                tb.removeClass("progresss");


                var loading = $(".loadingcss");
                loading.hide();
            }
        </script>
    
       
	
    </form>
</body>
</html>
