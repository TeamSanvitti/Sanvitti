<%@ Page Language="C#" AutoEventWireup="true" Trace="false" CodeBehind="RMA-Form.aspx.cs" Inherits="avii.RMA_Form" %>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="/Controls/Header.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="ctl" TagName="RMACommunication" Src="~/Controls/RMACommunication.ascx" %>

<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>.:: Return Merchandise Authorization (RMA) ::.</title>
    
    <link rel="stylesheet"  type="text/css" href="../fullfillment/calendar/dhtmlgoodies_calendar.css" media="screen" />
		<script type="text/javascript" src="../fullfillment/calendar/dhtmlgoodies_calendar.js"></script>  
		
        <!-- from http://encosia.com/2009/10/11/do-you-know-about-this-undocumented-google-cdn-feature/ -->
    <link href="https://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/themes/start/jquery-ui.css" type="text/css" rel="Stylesheet" />
	
	<script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.4.0/jquery.min.js"></script>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/jquery-ui.min.js"></script>
    
	<script type="text/javascript" src="/JSLibrary/jquery.blockUI.js"></script>

	
<!-- fix for 1.1em default font size in Jquery UI -->
	<style type="text/css">
		.ui-widget{font-size:11px !important;}
		.ui-state-error-text{margin-left: 10px}
	</style>
   
   
	<script type="text/javascript">
	    $(document).ready(function () {
               
                
	        $("#divAddEditRMA").dialog({
	            autoOpen: false,
	            modal: false,
	            minHeight: 20,
	            height: 360,
	            width: 650,
	            resizable: false,

	            open: function (event, ui) {
	                $(this).parent().appendTo("#divAddEditContainer");
	            }
	        });
	        $("#divTracking").dialog({
	            autoOpen: false,
	            modal: false,
	            minHeight: 20,
	            height: 220,
	            width: 450,
	            resizable: false,

	            open: function (event, ui) {
	                $(this).parent().appendTo("#divAddEditContainer");
	            }
	        });

	        $("#divComments").dialog({
	            autoOpen: false,
	            modal: false,
	            minHeight: 20,
	            height: 400,
	            width: 850,
	            resizable: false,

	            open: function (event, ui) {
	                $(this).parent().appendTo("#divAddEditContainer");
	            }
	        });

	        //unblockCommentsDialog

	        $("#divTriage").dialog({
	            autoOpen: false,
	            modal: false,
	            minHeight: 20,
	            height: 300,
	            width: 850,
	            resizable: false,

	            open: function (event, ui) {
	                $(this).parent().appendTo("#divAddEditContainer");
	            }
	        });


	        $("#divRMAReceive").dialog({
	            autoOpen: false,
	            modal: false,
	            minHeight: 20,
	            height: 300,
	            width: 650,
	            resizable: false,

	            open: function (event, ui) {
	                $(this).parent().appendTo("#divAddEditContainer");
	            }
	        });



	    });

	    function Toggle() {
	        $("#d1").hide();
	        $("#d2").show();
	    }
	    function Toggle2() {
	        $("#d2").hide();
	        $("#d1").show();
	    }



	    function closedRMAReceiveDialog() {
	        //Could cause an infinite loop because of "on close handling"
	        $("#divRMAReceive").dialog('close');
	    }

	    function openRMAReceiveDialog(title, linkID) {

	        var pos = $("#" + linkID).position();
	        var top = pos.top;
	        var left = pos.left + $("#" + linkID).width() + 10;
	        //alert(top);
	        if (top > 600)
	            top = 10;
	        else
	            top = 10;
	        //top = top - 600;
	        left = 400;
	        $("#divRMAReceive").dialog("option", "title", title);
	        $("#divRMAReceive").dialog("option", "position", [left, top]);

	        $("#divRMAReceive").dialog('open');

	        //unblockTrackingDialog();
	    }
	    function openRMAReceiveDialogAndBlock(title, linkID) {
	        openRMAReceiveDialog(title, linkID);

	        //block it to clean out the data
	        $("#divRMAReceive").block({
	            message: '<img src="../images/async.gif" />',
	            css: { border: '0px' },
	            fadeIn: 0,
	            //fadeOut: 0,
	            overlayCSS: { backgroundColor: '#ffffff', opacity: 1 }
	        });
	    }
	    function unblockRMAReceiveDialog() {
	        $("#divRMAReceive").unblock();
	    }



	    function closedTriageDialog() {
	        //Could cause an infinite loop because of "on close handling"
	        $("#divTriage").dialog('close');
	    }

	    function openTriageDialog(title, linkID) {

	        var pos = $("#" + linkID).position();
	        var top = pos.top;
	        var left = pos.left + $("#" + linkID).width() + 10;
	        //alert(top);
	        if (top > 600)
	            top = 10;
	        else
	            top = 10;
	        //top = top - 600;
	        left = 400;
	        $("#divTriage").dialog("option", "title", title);
	        $("#divTriage").dialog("option", "position", [left, top]);

	        $("#divTriage").dialog('open');

	        //unblockTrackingDialog();
	    }
	    function openTriageDialogAndBlock(title, linkID) {
	        openTriageDialog(title, linkID);

	        //block it to clean out the data
	        $("#divTriage").block({
	            message: '<img src="../images/async.gif" />',
	            css: { border: '0px' },
	            fadeIn: 0,
	            //fadeOut: 0,
	            overlayCSS: { backgroundColor: '#ffffff', opacity: 1 }
	        });
	    }
	    function unblockTriageDialog() {
	        $("#divTriage").unblock();
	    }

	    function closedCommentsDialog() {
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
	        left = 400;
	        $("#divComments").dialog("option", "title", title);
	        $("#divComments").dialog("option", "position", [left, top]);

	        $("#divComments").dialog('open');

	        //unblockTrackingDialog();
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

</script>

    <script type="text/javascript"  src="/JSLibrary/jquery-latest2.js"></script> 
		
    <script type="text/javascript">
        $(document).AjaxReady(function () {

            $("#[id$=dpWarranty]").change(function (evt) {
                //alert($(this).val());
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    //data: "{ Warranty: " + $(this).val() +  ", itemIndex: " + $(this).next().val() + "}",
                    data: "{ warrantyInfo: '" + $(this).val() + "," + $(this).next().val() + "'}",
                    url: "RMA-Form.aspx/UpdateEsnWarranty",
                    dataType: "json",//<a href="RMA-Form.aspx">RMA-Form.aspx</a>
                    success: function (data) {
                        //alert(data.d);
                    }
                });
                evt.preventDefault();
            });

            $("#[id$=dpDisposition]").change(function (evt) {
                //alert($(this).val());
                //alert($(this).next().val());
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    //data: "{ Warranty: " + $(this).val() +  ", itemIndex: " + $(this).next().val() + "}",
                    data: "{ dispositionInfo: '" + $(this).val() + "," + $(this).next().val() + "'}",
                    url: "RMA-Form.aspx/UpdateEsnDisposition",
                    dataType: "json",
                    success: function (data) {
                        //alert(data.d);
                    }
                });
                evt.preventDefault();
            });
            $("#[id$=ddReason]").change(function (evt) {
                //alert($(this).val());
                //alert($(this).next().val());
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    //data: "{ Warranty: " + $(this).val() +  ", itemIndex: " + $(this).next().val() + "}",
                    data: "{ reasonInfo: '" + $(this).val() + "," + $(this).next().val() + "'}",
                    url: "RMA-Form.aspx/UpdateEsnReason",
                    dataType: "json",
                    success: function (data) {
                        //alert(data.d);
                    }
                });
                evt.preventDefault();
            });
            $("#[id$=ddl_Status]").change(function (evt) {
                //alert($(this).val());
                //alert($(this).next().val());
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    //data: "{ Warranty: " + $(this).val() +  ", itemIndex: " + $(this).next().val() + "}",
                    data: "{ statusInfo: '" + $(this).val() + "," + $(this).next().val() + "'}",
                    url: "RMA-Form.aspx/UpdateEsnStatus",
                    dataType: "json",
                    success: function (data) {
                        //alert(data.d);
                    }
                });
                evt.preventDefault();
            });



        });

        $(document).ready(function () {
            //    $('#btnVarify').click(function(evt) {
            $("#[id$=dpWarranty]").change(function (evt) {
                //alert($(this).val());
                //alert($(this).next().val());
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    //data: "{ Warranty: " + $(this).val() +  ", itemIndex: " + $(this).next().val() + "}",
                    data: "{ warrantyInfo: '" + $(this).val() + "," + $(this).next().val() + "'}",
                    url: "RMA-Form.aspx/UpdateEsnWarranty",
                    dataType: "json",
                    success: function (data) {
                        //alert(data.d);
                    }
                });
                evt.preventDefault();
            });

            $("#[id$=dpDisposition]").change(function (evt) {
                //alert($(this).val());
                //alert($(this).next().val());
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    //data: "{ Warranty: " + $(this).val() +  ", itemIndex: " + $(this).next().val() + "}",
                    data: "{ dispositionInfo: '" + $(this).val() + "," + $(this).next().val() + "'}",
                    url: "RMA-Form.aspx/UpdateEsnDisposition",
                    dataType: "json",
                    success: function (data) {
                        //alert(data.d);
                    }
                });
                evt.preventDefault();
            });
            $("#[id$=ddReason]").change(function (evt) {
                //alert($(this).val());
                //alert($(this).next().val());
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    //data: "{ Warranty: " + $(this).val() +  ", itemIndex: " + $(this).next().val() + "}",
                    data: "{ reasonInfo: '" + $(this).val() + "," + $(this).next().val() + "'}",
                    url: "RMA-Form.aspx/UpdateEsnReason",
                    dataType: "json",
                    success: function (data) {
                        //alert(data.d);
                    }
                });
                evt.preventDefault();
            });
            $("#[id$=ddl_Status]").change(function (evt) {
                //alert($(this).val());
                //alert($(this).next().val());
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    //data: "{ Warranty: " + $(this).val() +  ", itemIndex: " + $(this).next().val() + "}",
                    data: "{ statusInfo: '" + $(this).val() + "," + $(this).next().val() + "'}",
                    url: "RMA-Form.aspx/UpdateEsnStatus",
                    dataType: "json",
                    success: function (data) {
                        //alert(data.d);
                    }
                });
                evt.preventDefault();
            });

        });

       

    
    </script>
    <script type="text/javascript">

        function ValidateReceiveRMA() {
            //var trackingNumber = document.getElementById("<%=txtShipTracking.ClientID %>").value;
            var newESN = document.getElementById("<%=txtRESN.ClientID %>").value;
            var oldESN = document.getElementById("<%=hdnRESN.ClientID %>").value;


            if (newESN == '') {
                alert('ESN is required.');
                return false;
            }

            var newSKU = document.getElementById("<%=txtRSKU.ClientID %>");
            if (newSKU != null && newSKU.value == '') {
                alert('SKU is required.');
                return false;
            }

            //if (trackingNumber == '') {
            //    alert('Shipping tracking number is required.');
            //    return false;
            //}


            if (newESN != '') {
                if (oldESN != newESN) {

                    var flag = confirm('The entered ESN is different from the one on the RMA. Are you sure you want to replace the ESN Number?');
                    if (!flag) {

                        return false;
                    }

                }
            }



        }

        function ValidateTriage() {
            var triageNotes = document.getElementById("<%=txtTriageNotes.ClientID %>").value;
            if (triageNotes == '') {
                alert('Triage notes is required.');
                return false;
            }
            var triageStatus = document.getElementById("<%=ddlTriage.ClientID %>");


            if (triageStatus.selectedIndex == 0) {
                alert('Triage status is required.');
                return false;
            }

        }
        function ValidateEmail(obj) {
            var EmailAddresses = obj.value;

            var emails = EmailAddresses.split(',');
            var EmaiAddress;
            for (var i = 0; i < emails.length; i++) {
                EmaiAddress = emails[i];
                var RegExEmail = /^(?:\w+\.?)*\w+@(?:\w+\.)+\w+$/;
                if (obj.value != '') {
                    if (!RegExEmail.test(EmaiAddress)) {
                        obj.focus();
                        alert("Invalid E-mail");
                        return false;
                    }
                }
            }
        }

        function ValidateFile() {
            var flag = false;
            var fineNamec = document.getElementById("<%=fupRmaDocc.ClientID %>");
            var FileUploadPath = fineNamec.value;
            var Extension = FileUploadPath.substring(FileUploadPath.lastIndexOf('.') + 1).toLowerCase();

            if (Extension == "doc" || Extension == "docx" || Extension == "xls" || Extension == "xlsx" || Extension == "pdf" || Extension == "jpeg" || Extension == "jpg") {
                //alert(FileUploadPath + '  is a valid file');
            }
            else {
                if (FileUploadPath != '') {
                    flag = true;
                    alert(FileUploadPath + '  is not a valid file');
                }
            }
            //alert('xcxc');

            var fineNameA1 = document.getElementById("<%=fupRmaDocA1.ClientID %>");
            var FileUploadPath1 = fineNameA1.value;
            var Extension1 = FileUploadPath1.substring(FileUploadPath1.lastIndexOf('.') + 1).toLowerCase();

            if (Extension1 == "doc" || Extension1 == "docx" || Extension1 == "xls" || Extension1 == "xlsx" || Extension1 == "pdf" || Extension1 == "jpeg" || Extension1 == "jpg") {

            }
            else {

                if (FileUploadPath1 != '') {
                    flag = true;
                    alert(FileUploadPath1 + '  is not a valid file');
                }
            }
            var fineNameA2 = document.getElementById("<%=fupRmaDocA2.ClientID %>");
            var FileUploadPath2 = fineNameA2.value;
            var Extension2 = FileUploadPath2.substring(FileUploadPath2.lastIndexOf('.') + 1).toLowerCase();

            if (Extension2 == "doc" || Extension2 == "docx" || Extension2 == "xls" || Extension2 == "xlsx" || Extension2 == "pdf" || Extension2 == "jpeg" || Extension2 == "jpg") {

            }
            else {

                if (FileUploadPath2 != '') {
                    flag = true;
                    alert(FileUploadPath2 + '  is not a valid file');
                }
            }
            var fineNameA3 = document.getElementById("<%=fupRmaDocA3.ClientID %>");
            var FileUploadPath3 = fineNameA3.value;
            var Extension3 = FileUploadPath3.substring(FileUploadPath3.lastIndexOf('.') + 1).toLowerCase();

            if (Extension3 == "doc" || Extension3 == "docx" || Extension3 == "xls" || Extension3 == "xlsx" || Extension3 == "pdf" || Extension3 == "jpeg" || Extension3 == "jpg") {

            }
            else {

                if (FileUploadPath3 != '') {
                    flag = true;
                    alert(FileUploadPath3 + '  is not a valid file');
                }
            }
            var fineNameA4 = document.getElementById("<%=fupRmaDocA4.ClientID %>");
            var FileUploadPath4 = fineNameA4.value;
            var Extension4 = FileUploadPath4.substring(FileUploadPath4.lastIndexOf('.') + 1).toLowerCase();

            if (Extension4 == "doc" || Extension4 == "docx" || Extension4 == "xls" || Extension4 == "xlsx" || Extension4 == "pdf" || Extension4 == "jpeg" || Extension4 == "jpg") {

            }
            else {
                if (FileUploadPath4 != '') {
                    flag = true;
                    alert(FileUploadPath4 + '  is not a valid file');

                }
            }

            if (flag == true)
                return false;
            else
                return true;


        }
    </script>

</head>
<body bgcolor="#ffffff"  leftmargin="0" topmargin="0" marginwidth="0" marginheight="0"  onkeydown="if (event.keyCode==13) {event.keyCode=9; return event.keyCode }">
    <form id="form1" runat="server">
    <table cellspacing="0" cellpadding="0" border="0" align="center" width="100%">
        <tr>
            <td>
                <head:MenuHeader ID="MenuHeader1" runat="server"/>
            </td>
        </tr>
        </table>
    <table cellspacing="0" cellpadding="0" border="0"  width="98%" align="center">
    <tr>
        <td>
            &nbsp;
        </td>
    </tr>                                                       
    <tr>
        <td>
                <asp:HiddenField ID="hdnValidateESNs" runat="server" />
                <asp:HiddenField ID="hdnShipDate" runat="server" />
                <asp:HiddenField ID="hdncompanyname" runat="server" />
                <table id="rmaform" style="text-align:left; width:100%;"  align="center" class="copy10grey">
                    
                    <tr>
                        <td class="buttonlabel" align="left">Return Merchandise Authorization(RMA) Form</td>
                    </tr>
                    
                </table>
                 <table bordercolor="#839abf" border="1" cellspacing="0" cellpadding="0" width="100%">
                            <tr bordercolor="#839abf">
                                <td>
                      <table  cellSpacing="0" cellPadding="0" width="100%">
                                <tr>
                                <td class="copy10grey" width="60%">
                                    &nbsp;- Please VALIDATE the RMA before submitting. System will give a warning message if RMA is not validated through <b>"Validate ESNs"</b><br />
                                    &nbsp;- Please enter your correct SHIP TO information in the space provided. ALL units will be returned to the default address on the account if this is incomplete or missing.<br />
                                    &nbsp;- For all customers, please take note of the returns checklist available for download via <a href="#" target="_blank">Lan Global RMA Checklist</a>.<br />
                                    
                                    &nbsp;- Upto 10 ESNs allowed per return (RMA).
                                
                                    <br />
                                     &nbsp;- Email should not have &quot;langlobal.com&quot; email address.</td>
                                <td bgcolor="#839abf" width="1">&nbsp;</td>
                                <td  class="copy10grey"  width="40%">
                                        &nbsp;Please send ALL returns to: <br />
                                         &nbsp;<b>Lan Global Inc. </b><br />
                                         &nbsp;Attention: RMA Department <br />
                                         &nbsp;12031 Sherman Road North hollywood,<br />
                                         &nbsp;CA 91605<br />
                                </td>
                                </tr>
                            </table>
                         </td>
                            </tr>
                        </table>
                        <table>
                         <tr>
                             <td>
                                <asp:Label ID="lblConfirm" runat="server" CssClass="errormessage"></asp:Label>
                                <asp:Label ID="lbl_msg" runat="server" CssClass="errormessage"></asp:Label> 
                             </td>
                        </tr>
                        </table>
                          <table bordercolor="#839abf" border="1" cellspacing="5" cellpadding="5" width="100%">
                            <tr bordercolor="#839abf">
                                <td>
                                <table class="box" border="0"  align="center" width="100%">
                                    <tr>
                                        <td class="copy10grey" align="left" class="copy10grey">
                                          <strong>   <asp:Label ID="lblCompany" runat="server" Text="Company:"></asp:Label></strong></td>
                                        <td colspan="7" class="copy10grey">
                                
                                            <asp:HiddenField ID="hdnUserID" runat="server" />
                                            <asp:DropDownList ID="ddlCompany"  AutoPostBack="true" CssClass="copy10grey" 
                                                 runat="server" 
                                                onselectedindexchanged="ddlCompany_SelectedIndexChanged">
                                            </asp:DropDownList>
                              
                                        </td>
                                    </tr>                                     
                                     <tr valign="top" >
                                        <td width="10%"  class="copy10grey"  align="left"><asp:Label ID="lblrmanumber" runat="server" Text="RMA#:" CssClass="copy10grey"></asp:Label></td>
                                        <td width="16%" class="copy10grey"  >
                                                <asp:Label ID="txtRmaNum" runat="server" 
                                                    CssClass="copyblue11b" Width="98%" ReadOnly="True" 
                                                     />
                                        </td>
                                        <td class="copy10grey" ><strong><asp:Label ID="lblRMADate" runat="server" Text="RMA Date:"  CssClass="copy10grey"></asp:Label>
                                            <span class="errormessage">*</span></strong></td>
                                        <td class="copy10grey">
                                            <asp:Panel ID="rmadtpanel" runat="server">
                                            &nbsp;<asp:TextBox ID="txtRMADate" runat="server"  onfocus="set_focus1();" onkeypress="return doReadonly(event);"  
                                                CssClass="copy10grey" MaxLength="15" />
                                             <img id="img1" alt="" onclick="document.getElementById('<%=txtRMADate.ClientID%>').value = ''; displayCalendar(document.getElementById('<%=txtRMADate.ClientID%>'),'mm/dd/yyyy',this,true);"
                                                        src="../../fullfillment/calendar/sscalendar.jpg" />
                                            </asp:Panel>
                                        </td> 
                                        <td class="copy10grey" width="10%"><span id="status" runat="server">Status:</span></td>
                                        <td class="copy10grey" width="20%">
                                            &nbsp;<asp:Label ID="lblStatus" runat="server" Text="Pending"></asp:Label>
                                            <asp:DropDownList ID="ddlStatus"  runat="server" class="copy10grey" 
                                                Width="166px" >
                                                       <%-- <asp:ListItem  Value="0" >------</asp:ListItem>
                                                        <asp:ListItem Value="1" >Pending</asp:ListItem>
                                                                    <asp:ListItem  Value="2">Received</asp:ListItem>
                                                                    <asp:ListItem  Value="3">Pending for Repair</asp:ListItem>
                                                                    <asp:ListItem  Value="4">Pending for Credit</asp:ListItem>
                                                                    <asp:ListItem  Value="5">Pending for Replacement</asp:ListItem>
                                                                    <asp:ListItem  Value="6">RMA Authorized</asp:ListItem>
                                                                    <asp:ListItem  Value="7">Returned</asp:ListItem>
                                                                    <asp:ListItem  Value="8">Credited</asp:ListItem>
                                                                    <asp:ListItem  Value="9">Denied</asp:ListItem>
                                                                    <asp:ListItem  Value="10">Canceled</asp:ListItem>
                                                                    <asp:ListItem  Value="11">Estimate Pending Approval</asp:ListItem>
                                                                    <asp:ListItem  Value="12">Repair Approved</asp:ListItem>
                                                                    <asp:ListItem  Value="13">Repair Rejected</asp:ListItem>
                                                                    <asp:ListItem  Value="14">Repair Finished</asp:ListItem>
                                                                    <asp:ListItem  Value="15">Pending Disposition Instruction</asp:ListItem>
                                                                    <asp:ListItem  Value="16">Closed</asp:ListItem>
                                                                    <asp:ListItem  Value="17">Disposition Instruction Provided</asp:ListItem>
--%>

                                            </asp:DropDownList>
                                            <br />
                                            &nbsp;
                                            
                                            <asp:CheckBox ID="chkAll" runat="server" Text="Apply to all ESNs" />
                                            
                                            
                                        </td>
                                        <%--<td class="copy10grey" >
                                        Location Code:
                                        </td>                           
                                        <td>
                                        <asp:TextBox ID="txtLocationCode"  runat="server" 
                                                CssClass="copy10grey" MaxLength="20" Width="90%" />
                                        </td>--%>                              
                                    </tr> 


                                    <tr>
                                        <td class="copy10grey"  align="left">
                                            
                                            <asp:Label ID="lblRMACustNo" CssClass="copy10grey" runat="server" Text="RMA Customer#:"  ></asp:Label>
                                        
                                        </td>
                                        <td width="16%" class="copy10grey"  >
                                            <asp:TextBox ID="txtRMACustomerNumber" runat="server" 
                                                CssClass="copy10grey" MaxLength="50" Width="90%" />
                                        </td>
                                        <td class="copy10grey"  align="left">
                                            <asp:Label ID="lblStore" CssClass="copy10grey" runat="server"  >Store ID:</asp:Label>
                                        
                                        </td> 
                                        <td colspan ="3">
                                        <asp:DropDownList CssClass="copy10grey"   Visible="true" 
                                         ID="ddlStoreID" runat="server"   >
                                        </asp:DropDownList>
                                 
                                        </td>                          
                                   
                                    </tr>
                                    <tr>
                                        <td colspan="6"><hr size="1" align="center" style="width: 100%" /></td>
                                    </tr>
                                    <tr>
                                        <td   class="copy10grey"  align="left"><strong>Customer Name:&nbsp;<span class="errormessage">*</span></strong></td>
                                        <td colspan="5">
                                            <asp:TextBox ID="txtCustName" runat="server" 
                                                CssClass="copy10grey" MaxLength="50" Width="90%" />
                                        </td> 
                                        
                                    </tr>
                         <tr>
                            <td   class="copy10grey"  align="left"><strong>Address:&nbsp;<span class="errormessage">*</span></strong></td>
                            <td colspan="5">
                                <asp:TextBox ID="txtAddress" runat="server" CssClass="copy10grey" Width="90%" MaxLength="200"/>
                            </td>                            
                        </tr>
                        <tr>
                            <td   class="copy10grey"  align="left"><strong>City:
                                        &nbsp;<span class="errormessage">*</span></strong>
                            </td>
                            <td class="copy10grey">
                                <asp:TextBox ID="txtCity" runat="server"  Width="80%"  CssClass="copy10grey" MaxLength="50"/>
                            </td>                            
                            <td class="copy10grey"  align="left"  width="10%"><strong>State:&nbsp;<span class="errormessage">*</span></strong></td>
                            <td class="copy10grey" width="20%">
                                <asp:dropdownlist id="dpState" tabIndex="6" runat="server" cssclass="copy10grey">

                                </asp:dropdownlist>
                                <%--<asp:TextBox ID="txtState" runat="server"  Width="90%"  CssClass="copy10grey"  ViewStateMode="Disabled"
                                    MaxLength="30" />--%>
                             </td>
                             <td class="copy10grey"  align="left" ><strong>Zip:&nbsp;<span class="errormessage">*</span></strong></td>    
                              <td  width="20%">  <asp:TextBox ID="txtZip" runat="server" Width="37%"  
                                      CssClass="copy10grey" MaxLength="5"/>
                            </td>  
                        </tr>
              
                                    <tr>
                            <td   class="copy10grey"  align="left"><strong>Email:&nbsp;<span class="errormessage">*</span></strong>
                            </td>
                            <td class="copy10grey">
                                <asp:TextBox ID="txtEmail" Width="90%" runat="server" CssClass="copy10grey" MaxLength="50" ViewStateMode="Disabled"/>
                            </td>                            
                            <td class="copy10grey"  align="left"><strong>Phone:&nbsp;<span class="errormessage">*</span></strong></td>
                            <td class="copy10grey">
                                <asp:TextBox ID="txtPhone"  Width="90%" runat="server" CssClass="copy10grey"  ViewStateMode="Disabled"
                                    MaxLength="12" />
                             </td> 
                             <td class="copy10grey"  align="left">
                                 <%--Allow Shipping Label:&nbsp;--%>

                             </td>
                            <td class="copy10grey">
                                <%--<asp:CheckBox ID="chkShipLabel" CssClass="copy10grey" runat="server" />--%>
                             </td> 
                             
                             <%--<td colspan="2"></td>   --%>                      
                        </tr>
                        <tr>
                            <td class="copy10grey">Document:</td>
                            <td >
                            <asp:ImageButton ID="imgUpload" runat="server" ToolTip="Upload RMA Document" AlternateText="Upload RMA Document"  ImageUrl="~/images/upload.png"
                                             CausesValidation="false" />


                                    <asp:Label ID="lblRmaDocs" runat="server" CssClass="copy10grey" ></asp:Label>
                                

                            <table>
        <tr>
            <td>
                <ajaxToolkit:ModalPopupExtender BackgroundCssClass="modalBackground" 
        CancelControlID="btnUloadCancel"  runat="server" PopupControlID="pnlModelPoupp" 
        ID="ModalPopupExtender1" TargetControlID="imgUpload"
         />
        <asp:LinkButton ID="lnk" runat="server" ></asp:LinkButton>
        <asp:Panel ID="pnlModelPoupp" runat="server" CssClass="modal1Popup" Visible="true" Style="display: none" >
        <%--
        <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="button" />--%>
        <table  cellSpacing="1" cellPadding="1" width="100%" align="center">
                                <tr>
			                        <td bgcolor="#dee7f6" class="buttonlabel" align="left">&nbsp;&nbsp;RMA document
			                        </td>
                                </tr>
                                </table>
                            
                            <table bordercolor="#839abf" border="1" width="98%" align="center" cellpadding="0" cellspacing="0">
                                <tr ><td>

                                <table border="0" width="100%" class="box" align="center" cellpadding="5" cellspacing="5">
                    
                                    <tr>
                                        <td class="copy10grey" width="120">
                                            File:
                                        </td>
                                        <td>
                                            <asp:FileUpload ID="fupRmaDocc" runat="server" />           

                                        </td>
                                    </tr>
                                    </table>
                                    </td>
                                </tr>
                                </table>
                                
                                
                                    <asp:Panel ID="pnlDoc" runat="server">
                                    <br />
                                    <table  cellSpacing="1" cellPadding="1" width="100%" align="center">
                                <tr>
			                        <td bgcolor="#dee7f6" class="buttonlabel" align="left">&nbsp;&nbsp;Administration RMA document
			                        </td>
                                </tr>
                                </table>
                            
                                    <table bordercolor="#839abf" border="1" width="98%" align="center" cellpadding="0" cellspacing="0">
                                    <tr><td>

                                    <table border="0" width="100%" class="box" align="center" cellpadding="5" cellspacing="5">
                    
                                    <tr >
                                        <td class="copy10grey" width="120">
                                            Admin File 1:
                                        </td>
                                        <td>
                                            <asp:FileUpload ID="fupRmaDocA1" runat="server" />           

                                        </td>
                                    </tr>
                                    
                                    <tr >
                                        <td class="copy10grey">
                                            Admin File 2:
                                        </td>
                                        
                                        <td>
                                            <asp:FileUpload ID="fupRmaDocA2" runat="server" />           

                                        </td>
                                    </tr>
                                    
                                    <tr>
                                        <td class="copy10grey">
                                            Admin File 3:
                                        </td>
                                        
                                        <td>
                                            <asp:FileUpload ID="fupRmaDocA3" runat="server" />           

                                        </td>
                                    </tr>
                                    
                                    <tr >
                                        <td class="copy10grey">
                                            Admin File 4:
                                        </td>
                                        
                                        <td>
                                            <asp:FileUpload ID="fupRmaDocA4" runat="server" />           

                                        </td>
                                    </tr>
                                    

                                    </table>
                                    </td>
                                    </tr>
                                    </table>
                                    </asp:Panel>

                                    <table border="0" width="100%"  align="center" cellpadding="5" cellspacing="5">
                    
                                    <%--<tr>
                                        <td colspan="2" align="center">OnClick="btnRmaDocUpload_Clicck"  

                                            <hr />
                                        </td>
                                    </tr--%>
                                    <tr>
                                        <td align="center">
                                        
                                            <asp:Button ID="btnRmaDocUpload" runat="server" Text="Upload" CssClass="buybt" OnClick="btnRmaDocUpload_Clicck"   OnClientClick="return ValidateFile();"  />
                                            <asp:Button ID="btnUloadCancel" runat="server" Text="Cancel" CssClass="buybt"/>
                                        </td>
                                    </tr>
                                    
                                    </table>

        </asp:Panel>
            </td>
        </tr>
        </table>
                            </td>
                            <td class="copy10grey">
                            
                            <%--<asp:Label ID="lblPrint" CssClass="copy10grey" Text="Re-Print:" runat="server" ></asp:Label>--%>
                            </td>
                            <td colspan="5">
                               <%-- <asp:CheckBox ID="chkPrint" CssClass="copy10grey" runat="server" />
                               --%> <asp:Label ID="lblDocError" CssClass="errormessage" runat="server" ></asp:Label>
                            </td>

                        </tr>
                        <tr>
                                        <td colspan="8"><hr size="1" align="center" style="width: 100%" /></td>
                                    </tr>
                                    
                                    <tr valign="top">
                            <td   class="copy10grey" align="left">RMA Comments:</td>
                            <td colspan="2">
                                <asp:TextBox runat="server" ID="txtRemarks" TextMode="MultiLine" ViewStateMode="Disabled"
                                    Rows="5" Columns="80" CssClass="copy10grey" 
                                     Width="100%" />
                            </td>
                            <div runat="server" id="divAvc">
                                <td   class="copy10grey" align="Right">LAN Global Comments:</td>
                                <td colspan="4">
                                    <asp:TextBox runat="server" ID="txtAVComments" TextMode="MultiLine" ViewStateMode="Disabled"
                                        Rows="5" Columns="80" CssClass="copy10grey" 
                                         Width="99%" />
                                </td>
                            </div>
                        </tr> 
                                </table>      
                                </td>
                            </tr>
                        </table> 
                        <table>
                        <tr><td>&nbsp;</td></tr>
                        <%--<tr>
                            <td align="left" class="buttonlink">
                                    Return Merchandise Authorization(RMA) Line Items
                             </td>
                        </tr>--%>
                        </table>   
                        <table width="100%">
                        <tr>
                        <td>
                        
                        
                        <asp:ScriptManager ID="sMgr" runat="server"></asp:ScriptManager> 
            
                        <asp:UpdatePanel ID="uPnl" runat="server" ChildrenAsTriggers="true"  UpdateMode="Conditional">
                        <ContentTemplate>
                            <table width="100%">
                            <tr>
                                <td>
                                <asp:LinkButton ID="btnRefreshGrid" CausesValidation="false" OnClick="btnRefreshGrid_Click" style="display:none" runat="server"></asp:LinkButton>		
	
                                
                            <asp:Panel ID="esnPanel" runat="server" align="center">
                            <table width="100%" cellspacing="0" cellpadding="0" border="0"  align="center" >
                            <tr>
                                <td class="copy10grey" align="left">
                                <table width="100%" cellspacing="0" cellpadding="0" border="0"  align="center" >
                                <tr>
                                    <td align="left" class="buttonlink">
                                    Return Merchandise Authorization(RMA) Line Items
                                    </td>
                                    <td class="copy10grey" align="right">
                                    <asp:Button ID="btnComments" runat="server" CssClass="button" Text="View Communication" OnClick="btnComments_Click" OnClientClick="openCommentsDialogAndBlock('RMA Communication', 'btnComments')" CausesValidation="false"/>
                           &nbsp;&nbsp;
                                        &nbsp;
                                    </td>
                                </tr>
                                </table>
                                    

                                </td>
                                </tr>
                                <tr>
                                    <td id="td_esn" align="left">
                                       
                                        
                                        <asp:HiddenField ID="hdnmsg" runat="server" />
                                        <asp:HiddenField ID="hdnRmaItemcount" Value="1" runat="server" />
                                        <table  cellpadding="1" cellspacing="1" border="3"  width="100%">
                                        <asp:DataList ID="DlRma" runat="server" 
                                            OnItemDataBound="DataList1_ItemDataBound" Width="100%">
                                            <HeaderTemplate >
                                            
                                            <tr style="height: 40px" valign="top" >
                                                <td style="height: 40px" class="buttongrid" Width="1%">&nbsp;</td>
                                                <td style="height: 40px" class="buttongrid" Width="15%" align="left">ESN/MEID/IMEI</td>
                                                <%--<td class="button" Width="5%"  align="left">UPC</td>--%>
                                                <td  style="height: 40px"class="buttongrid" Width="10%"  align="left">Fulfillment#</td>
                                                <td  style="height: 40px"class="buttongrid" Width="10%"  align="left">SKU#</td>
                                               <%-- <td class="button" Width="5%"  align="left">AVSO#</td>
                                                <td class="button" Width="5%"  align="left">Fulfillment#</td>
                                               --%> <%--<td class="button" Width="5%"  align="left" id="tdCallTime">Call Time</td>--%>
                                               <td style="height: 40px" class="buttongrid" Width="10%"  align="left" id="tdType">Type</td>
                                                <td  style="height: 40px; display:none"   align="left" Width="5%"  id="tdDISPOSITION">
                                                    <span style="height:100%; width:100%; border:none" class="buttongrid">
                                                        DISPOSITION
                                                        <%--class ='<%# HideShowDisposition() %>'--%>
                                                    </span>
                                                    

                                                </td>
                                                
                                                <td style="height: 40px" class="buttongrid" Width="10%"  align="left" id="tdReason">Reason</td>                                             
                                                <td style="height: 40px" class="buttongrid" Width="30%"  align="left" id="tdnotes">Notes</td>
                                                <td  style="height: 40px"   class ='<%# ShowRMADetailStatus() %>' Width="10%"> 
                                                    <asp:Label ID="lblHeader" Width="100%" runat="server" Height="100%" BorderWidth="0" Text=" Status       " CssClass="buttongrid"></asp:Label>&nbsp;
                                                 </td>
                                                <td  class="buttongrid" style="height: 40px; width:1%" >
                                                    &nbsp;
                                                    </td>
                                                <td class="buttongrid" style="height: 40px; width:1%; display:none">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                           <%-- </table>--%>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                
                                               <%-- <table  border="2" cellspacing="2" cellpadding="2" width="100%" align="center">--%>
                                                    <tr  valign="top" class="<%# Container.ItemIndex % 2 == 0 ? "alternaterow" : "" %>">
                                                        <td >
                                                            <asp:CheckBox ID="chkESN" Checked="true" Enabled='<%# Convert.ToInt32(Eval("StatusID")) > 1 ? false : true%>'  runat="server" />
                                                        </td>
                                                        <td  >
                                                            <asp:HiddenField ID="hdnRDOID" Value='<%# Eval("rmaDetGUID") %>' runat="server" />
                                                            
                                                            <asp:TextBox ID="txt_ESN" ReadOnly='<%# Convert.ToInt32(Eval("StatusID")) > 1 ? true : false%>' Text = '<%# Eval("esn") %>'  runat="server"  Width="95%" onkeypress="return alphaNumericCheck(event);" 
                                                                      AutoPostBack="true" ontextchanged="ESN_TextChanged" MaxLength="30" CssClass="esntext" ></asp:TextBox>
                                                                
                                                         </td>
<%--
                                                        <td  >
                                                            
                                                        </td>--%>
                                                        <td class="copy10grey">
                                                            <%# Convert.ToString(Eval("ESN")) != "" && Convert.ToString(Eval("PurchaseOrderNumber")) == "" ? "External Esn":Eval("PurchaseOrderNumber") %> 
                                                            
                                                            <asp:Label ID="lblPONum" Visible="false" runat="server" ViewStateMode="Enabled" Text='<%# Eval("PurchaseOrderNumber") %>' ></asp:Label>
                                                            
                                                      </td>
                                                         <td class="copy10grey">
                                                            <%# Convert.ToString(Eval("ESN")) != "" && Convert.ToString(Eval("Itemcode")) == "" ? "External Esn":Eval("Itemcode") %> 
                                                            <asp:Label ID="lblCode" Visible="false" runat="server" ViewStateMode="Enabled" Text='<%# Eval("Itemcode") %>' ></asp:Label>
                                                                                                                     
                                                            <asp:Label ID="lblinvalidESN" CssClass ="errormessage" runat="server" ViewStateMode="Disabled" ></asp:Label>
                                                            
                                                            <asp:Label ID="lblUPC" Text = '<%# Eval("UPC") %>' CssClass="copy10grey" Width="100%" ViewStateMode="Disabled" runat="server"></asp:Label>
                                                        </td>
                                                       
                                                        <%--<td >
                                                            <asp:TextBox ID="txtCTime" ReadOnly='<%# Convert.ToInt32(Eval("StatusID")) > 1 ? true : false%>' Width="80%" Text='<%# Eval("CallTime") %>' ViewStateMode="Disabled" ontextchanged="CallTime_TextChanged" onkeypress="return isNumberKey(event);"  CssClass="copy10grey" runat="server" MaxLength="3"></asp:TextBox>
                                                            
                                                          
                                                        </td>--%>
                                                         <td>
                                                            <asp:DropDownList ID="dpWarranty"  runat="server" Width="100%"  CssClass ="copy10grey"> 
                                                                    <asp:ListItem  Value="0" >------</asp:ListItem>
                                                                    <asp:ListItem Value="1" >Warranty</asp:ListItem>
                                                                    <asp:ListItem  Value="2">Out of Warranty</asp:ListItem>
                                                            </asp:DropDownList>

                                                            <input type="hidden" id="hdnIndex" value='<%# Container.ItemIndex %>' />
                                                            <%--<asp:HiddenField id="hdnIndex" runat="server" Value='<%# Container.ItemIndex %>'/>--%>                                                           
                                                            <asp:HiddenField id="hdnWarranty" runat="server" Value='<%# Eval("Warranty") %>'/>
                                                              
                                                        </td>
                                                        <td style="display:none" >
                                                            <%--class ='<%# HideShowDisposition() %>'--%>
                                                            <asp:DropDownList ID="dpDisposition"  runat="server" Width="100%"  CssClass ="copy10grey"> 
                                                                    <asp:ListItem  Value="0" >------</asp:ListItem>
                                                                   <asp:ListItem Value="1" >Credit</asp:ListItem>
                                                                    <asp:ListItem  Value="2">Replaced</asp:ListItem>
                                                                    <asp:ListItem  Value="3">Repair</asp:ListItem>
                                                                    <asp:ListItem  Value="4">Discarded</asp:ListItem>
                                                 
                                                            </asp:DropDownList>
                                                            <input type="hidden" id="h1" value='<%# Container.ItemIndex %>' />
                                                            
                                                              <asp:HiddenField id="hdnDisposition" runat="server" Value='<%# Eval("Disposition") %>'/>
                                                        </td>
                                                          <td >
                                                              <asp:DropDownList ID="ddReason"  runat="server" Width="100%" 
                                                              CssClass ="copy10grey"> 
                                                                    <asp:ListItem  Value="0" >------</asp:ListItem>
                                                                     <asp:ListItem Value="1" >DOA</asp:ListItem>
                                                                    <asp:ListItem  Value="2">AudioIssues</asp:ListItem>
                                                                    <asp:ListItem  Value="3">ScreenIssues</asp:ListItem>
                                                                    <asp:ListItem  Value="4">PowerIssues</asp:ListItem>
                                                                    <asp:ListItem  Value="5">Others</asp:ListItem>
                                                                    <asp:ListItem  Value="6">MissingParts</asp:ListItem>
                                                                    <asp:ListItem  Value="7">ReturnToStock</asp:ListItem>
                                                                    <asp:ListItem  Value="8">BuyerRemorse</asp:ListItem>
                                                                    <asp:ListItem  Value="9">PhysicalAbuse</asp:ListItem>
                                                                    <asp:ListItem  Value="10">LiquidDamage</asp:ListItem>
                                                                    <asp:ListItem  Value="11">DropCalls</asp:ListItem>
                                                                    <asp:ListItem  Value="12">Software</asp:ListItem>
                                                              </asp:DropDownList>
                                                              <input type="hidden" id="h2" value='<%# Container.ItemIndex %>' />
                                                            
                                                              <asp:HiddenField id="hdnReason" runat="server" Value='<%# Eval("Reason") %>'/>
                                                              
                                                            </td>
                                                            
                                                            <td >
                                                                <asp:TextBox ID="txtNotes"  ontextchanged="Notes_TextChanged"  Text='<%# Eval("Notes") %>'  
                                                                Width="95%" ViewStateMode="Disabled"
                                                                CssClass="copy10grey" runat="server" MaxLength="1000" TextMode="MultiLine"></asp:TextBox>
                                                           </td>
                                                           <td  class ='<%# ShowRMADetailStatus() %>'>
                                                                 <asp:DropDownList ID="ddl_Status"   OnSelectedIndexChanged="Status_OnChanged" runat="server" 
                                                                 class="copy10grey"
                                                                  Width="95%" >
                                                                    <%--<asp:ListItem  Value="0" >------</asp:ListItem>
                                                                    <asp:ListItem Value="1" >Pending</asp:ListItem>
                                                                    <asp:ListItem  Value="2">Received</asp:ListItem>
                                                                    <asp:ListItem  Value="3">Pending for Repair</asp:ListItem>
                                                                    <asp:ListItem  Value="4">Pending for Credit</asp:ListItem>
                                                                    <asp:ListItem  Value="5">Pending for Replacement</asp:ListItem>
                                                                    <asp:ListItem  Value="6">RMA Authorized</asp:ListItem>
                                                                    <asp:ListItem  Value="7">Returned</asp:ListItem>
                                                                    <asp:ListItem  Value="8">Credited</asp:ListItem>
                                                                    <asp:ListItem  Value="9">Denied</asp:ListItem>
                                                                    <asp:ListItem  Value="10">Canceled</asp:ListItem>
                                                                    <asp:ListItem  Value="11">Estimate Pending Approval</asp:ListItem>
                                                                    <asp:ListItem  Value="12">Repair Approved</asp:ListItem>
                                                                    <asp:ListItem  Value="13">Repair Rejected</asp:ListItem>
                                                                    <asp:ListItem  Value="14">Repair Finished</asp:ListItem>
                                                                    <asp:ListItem  Value="15">Pending Disposition Instruction</asp:ListItem>
                                                                    <asp:ListItem  Value="16">Closed</asp:ListItem>
                                                                    <asp:ListItem  Value="17">Disposition Instruction Provided</asp:ListItem>
--%>
                                                                </asp:DropDownList>
                                                                <input type="hidden" id="h3" value='<%# Container.ItemIndex %>' />
                                                            
                                                                <asp:HiddenField id="hdnStatus" runat="server" Value='<%# Eval("StatusID") %>'/>
                                                           </td>
                                                           <td >
                                                           
                                                              <asp:HiddenField ID="hdTriagNotes" Value='<%# Eval("TriageNotes") %>' runat="server" />
                                                              <asp:HiddenField ID="hdTriagID" Value='<%# Eval("TriageStatusID") %>' runat="server" />
                                                           
                                                               <asp:Button ID="btntriage" CssClass="buybt" runat="server" Visible='<%# Convert.ToInt32(Eval("rmaDetGUID"))==0 ? false : true %>' 
                                                               Text="Triage" OnClick="btnAddtriage_click" />

                                                           </td>
                                                           <td style="display:none">
                                                           <asp:HiddenField ID="hdnNewSKU" Value='<%# Eval("newsku") %>' runat="server" />
                                                            <asp:HiddenField ID="hdnTracking" Value='<%# Eval("ShippingTrackingNumber") %>' runat="server" />
                                                           
                                                               <asp:Button ID="btnRMAReceive" CssClass="buybt" runat="server" 
                                                               Visible='<%# Convert.ToInt32(Eval("rmaDetGUID"))==0 ? false : true %>' 
                                                               Text='<%# Convert.ToInt32(Eval("rmaDetGUID")) == 0 ? "Receive Device" : Convert.ToInt32(Eval("rmaDetGUID")) > 0 && Convert.ToDateTime(Eval("CreateDate")).ToShortDateString() != "1/1/0001" ? "SHOW RECEIPT" : "Receive Device"%>' OnClick="btnAddRMAReceive_click" />

                                                           </td>
  
  
  
  
                                                    </tr>
                                                   
                                                   
                                            </ItemTemplate>  
                                            <FooterTemplate>

                                            </FooterTemplate>                  
                                            </asp:DataList>
                                            </table>
                                      
                                    </td>
                                </tr>
                            </table>
                            </asp:Panel>    
                            </td>
                            </tr>
                            
                            </table>       
                            <script type='text/javascript'>


                                prm = Sys.WebForms.PageRequestManager.getInstance();
                                prm.add_endRequest(EndRequest);
                                function EndRequest(sender, args) {
                                    //alert("EndRequest");
                                    $(document).AjaxReady();
                                }
        </script>  
                        </ContentTemplate>
                        <Triggers>
                        <asp:PostBackTrigger ControlID="btnBack" />
                        <asp:PostBackTrigger ControlID="btn_Cancel" />
                        <%--<asp:PostBackTrigger ControlID="btnValidate" />--%>
                        </Triggers>
                        </asp:UpdatePanel>
                        </td>
                        </tr>
                         <tr><td><hr /></td></tr>
                            <tr>
                                <td align="center">
                                    <asp:Panel ID="btnpanel" runat="server">
                                    <asp:Button ID="btnNewRMA" CssClass="buybt" runat="server" Text="Enter new RMA" 
                                             OnClick="btnNewRMA_click" />&nbsp;   
                                    
                                    <asp:Button ID="btnValidate" CssClass="buybt" runat="server" Text="Validate ESNs" Visible="false" 
                                            OnClientClick="return IsValidate(0);" OnClick="btnValidate_click" />&nbsp;   
                                        <asp:Button ID="btnSubmitRMA"  CssClass="buybt" runat="server" Text="Submit RMA"  
                                            OnClientClick="return IsValidate(1);" OnClick="btnSubmitRMA_click" />&nbsp;       
                                        <asp:Button ID="btn_Cancel" CssClass="buybt" runat="server" Text="Cancel" 
                                            CausesValidation="false" OnClick="btnCancelRMA_click" /> 
                                        <asp:Button ID="btnBack" CssClass="buybt" runat="server" Text="Back to search" 
                                            CausesValidation="false" OnClick="btnBackRMAQuery_click" />                
                                        </asp:Panel>
                                </td>
                            </tr>
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
                        </table>

        </td>    
    </tr>
    </table>
    
    <div id="divAddEditContainer">
          <div id="divComments" style="display:none">
					
				<asp:UpdatePanel ID="UpdatePanel2" runat="server">
					<ContentTemplate>
						<asp:PlaceHolder ID="PlaceHolder2" runat="server">
                        <asp:Label ID="lblCommMsg" runat="server" CssClass="errormessage"></asp:Label>
                        <div id="d1">
                            
                        <table width="95%" align="center">
                            <tr style="display:none">
                                <td class="copy11link"  align="right">
                                    <a class="copy11link" style="cursor: pointer;"  onclick="Toggle();" >Send Comments E-mail</a>
                                </td>
                            </tr>
                            <tr>
                            <td>
                        
                            <ctl:RMACommunication ID="rmaComment1" runat="server" />
                            </td>
                            </tr>
                            </table>
                            
                            </div>
                            <div id="d2" style="display: none;">
                            
                            <table width="100%" align="center">
                            <tr>
                                <td colspan="2" class="copy10grey" align="right">
                                <input type="button" title="Back" value="  Back  " class="button" onclick="Toggle2();"  />
                                </td>
                            </tr>
                            </table>


                            <table bordercolor="#839abf" border="1" cellspacing="0" cellpadding="0" width="100%">
                            <tr bordercolor="#839abf">
                                <td>
                                <br />

                                <table class="box" width="95%" align="center">
                            <tr>
                                <td class="copy10grey" style="width:100px" align="right">
                                    Customer Emails: &nbsp;
                                </td>
                                <td  class="copy10grey" align="left">
                                    <asp:Label ID="lblEmails" runat="server" CssClass="copy10grey"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td  class="copy10grey" align="right">
                                    CC: &nbsp;
                                </td>
                                <td class="copy10grey" align="left">
                                    <asp:TextBox ID="txtEmails" Width="90%" runat="server" onchange = "return ValidateEmail(this);"  CssClass="copy10grey"></asp:TextBox>
                                    <br />(Add multiple emails ,(comma) delimited)
                                </td>
                                
                            </tr>
                            <tr>
                                <td colspan="2" align="center" class="copy10grey">
                                    <asp:Button ID="btnSend" runat="server" Text="Send Email" CssClass="buybt"   OnClick="btnSendEmail_Click" />
                                    &nbsp;
                                    <asp:Button ID="btnCCancel" runat="server" Text="Cancel" CssClass="buybt"   OnClientClick="return closedCommentsDialog();"/>
                                    <%--<asp:Button ID="btnCommCancel" runat="server" Text="Cancel" CssClass="buybt"  OnClientClick="return closeCommunicationDialog();" />--%>
                                </td>
                            </tr>
                            </table>

                            <br />
                            </td>
                            </tr>
                            </table>

                            </div>
                        </asp:PlaceHolder>
						
						
					</ContentTemplate>
							
				</asp:UpdatePanel>
                </div>

                 <div id="divTriage" style="display:none">
					
				<asp:UpdatePanel ID="upTriage" runat="server">
					<ContentTemplate>
						<asp:PlaceHolder ID="phTriage" runat="server">
                        <asp:Label ID="lblTriage" runat="server" CssClass="errormessage"></asp:Label>
                            


                            <table bordercolor="#839abf" border="1" cellspacing="0" cellpadding="0" width="100%">
                            <tr bordercolor="#839abf">
                                <td>
                                <br />

                                <table class="box" width="95%" align="center" cellspacing="5" cellpadding="5">
                                    <tr>
                                        <td class="copy10grey" style="width:100px" align="right">
                                    RMA#: &nbsp;
                                    </td>
                                    <td  class="copy10grey" align="left">
                                    <strong>    <asp:Label ID="lblRMA" CssClass="copy10grey" runat="server"></asp:Label></strong>
                                    
                                    </td>
                                    </tr>
                            <tr valign="top">
                                <td class="copy10grey" style="width:100px" align="right">
                                    Triage Notes: &nbsp;
                                </td>
                                <td  class="copy10grey" align="left">
                                    <asp:TextBox ID="txtTriageNotes" TextMode="MultiLine" Height="70" Width="90%" CssClass="copy10grey" runat="server"></asp:TextBox>
                                    
                                </td>
                            </tr>
                            <tr>
                                <td  class="copy10grey" align="right">
                                    Triage Status: &nbsp;
                                </td>
                                <td class="copy10grey" align="left">
                                    <asp:DropDownList ID="ddlTriage" CssClass="copy10grey" runat="server" Width="90%">
                                    <asp:ListItem Text="Select Triage Status" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="In-Process" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Accepted" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Rejected" Value="3"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                
                            </tr>
                            <tr>
                                <td colspan="2" class="copy10grey" align="center">
                                    <hr />
                                </td>
                            </tr>
                            
                            <tr>
                                <td colspan="2" align="center" class="copy10grey">
                                    <asp:Button ID="btnTriage" runat="server" Text=" Submit " CssClass="buybt"   OnClick="btnTriage_Click" OnClientClick="return ValidateTriage();"  />
                                    &nbsp;
                                    <asp:Button ID="btnTriageCancel" runat="server" Text="Cancel" CssClass="buybt"   
                                    OnClientClick="return closedTriageDialog();"/>
                                    <%--<asp:Button ID="btnCommCancel" runat="server" Text="Cancel" CssClass="buybt"  OnClientClick="return closeCommunicationDialog();" />--%>
                                </td>
                            </tr>
                            </table>
                            <br />
                            </td>
                            </tr>
                            </table>

                            
                        </asp:PlaceHolder>
						
						
					</ContentTemplate>
							
				</asp:UpdatePanel>
                </div>
                
		 <div id="divRMAReceive" style="display:none">
					
				<asp:UpdatePanel ID="upRRMA" runat="server">
					<ContentTemplate>
						<asp:PlaceHolder ID="phRRMA" runat="server">
                        <asp:Label ID="lblReceiveRMA" runat="server" CssClass="errormessage"></asp:Label>
                            


                            <table bordercolor="#839abf" border="1" cellspacing="0" cellpadding="0" width="100%">
                            <tr bordercolor="#839abf">
                                <td>
                                <br />

                                <table class="box" width="95%" align="center" cellspacing="5" cellpadding="5">
                            <tr valign="top">
                                <td class="copy10grey" style="width:150px" align="right">
                                    ESN: &nbsp;
                                </td>
                                <td  class="copy10grey" align="left">
                                    <asp:TextBox ID="txtRESN" MaxLength="50" Width="90%" CssClass="copy10grey" runat="server" 
                                    AutoPostBack="true" OnTextChanged="txtRESN_TextChanged" ></asp:TextBox>
                                    <asp:HiddenField ID="hdnRESN" runat="server" />
                                    
                                </td>
                            </tr>
                            <tr valign="top">
                                <td class="copy10grey" style="width:150px" align="right">
                                    SKU: &nbsp;
                                </td>
                                <td  class="copy10grey" align="left">
                                    <asp:TextBox ID="txtRSKU" MaxLength="50" Width="90%" CssClass="copy10grey" runat="server"></asp:TextBox>
                                    
                                </td>
                            </tr>
                            <tr valign="top" style="display:none">
                                <td class="copy10grey" style="width:150px" align="right">
                                    Shipment Tracking Number: &nbsp;
                                </td>
                                <td  class="copy10grey" align="left">
                                    <asp:TextBox ID="txtShipTracking" MaxLength="50" Width="90%" CssClass="copy10grey" runat="server"></asp:TextBox>
                                    
                                </td>
                            </tr>

                            <tr valign="top">
                                <td class="copy10grey" style="width:150px" align="right">
                                    <asp:Label ID="lblDate" MaxLength="50" Width="90%" CssClass="copy10grey" runat="server" Text="Create Date: " ></asp:Label> 
                                </td>
                                <td  class="copy10grey" align="left">
                                    <asp:Label ID="lblCreateDate" MaxLength="50" Width="90%" CssClass="copy10grey" runat="server" ></asp:Label>
                                    
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" class="copy10grey" align="center">
                                    <hr />
                                </td>
                            </tr>
                            
                            <tr>
                                <td colspan="2" align="center" class="copy10grey">
                                    <asp:Button ID="btnRRMA" runat="server" Text="Save" CssClass="buybt"  OnClientClick="return ValidateReceiveRMA();"  OnClick="btnRMAReceive_click" />
                                    &nbsp;
                                    <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="buybt"   
                                    OnClientClick="return confirm('Do you want to delete?');"  OnClick="btnRMAReceiveDelete_click"/>
                                    
                                    &nbsp;
                                    <asp:Button ID="btnRRMACancel" runat="server" Text="Cancel" CssClass="buybt"   
                                    OnClientClick="return closedRMAReceiveDialog();"/>
                                    <%--<asp:Button ID="btnCommCancel" runat="server" Text="Cancel" CssClass="buybt"  OnClientClick="return closeCommunicationDialog();" />--%>
                                </td>
                            </tr>
                            </table>
                            <br />
                            </td>
                            </tr>
                            </table>

                            
                        </asp:PlaceHolder>
						
						
					</ContentTemplate>
							
				</asp:UpdatePanel>
                </div>

		

		
		
    </div>
    
    <asp:UpdatePanel ID="upnlJsRunner" UpdateMode="Always" runat="server">
		<ContentTemplate>
			<asp:PlaceHolder ID="phrJsRunner" runat="server">
                
            </asp:PlaceHolder>
		</ContentTemplate>
	</asp:UpdatePanel>


    </form>
    <script type="text/javascript" src="/fullfillment/calendar/dhtmlgoodies_calendar.js"></script>
    <script type="text/javascript" language="javascript">

    function getQuerystring(key, default_) {
            if (default_ == null) default_ = "";
            key = key.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
            var regex = new RegExp("[\\?&]" + key + "=([^&#]*)");
            var qs = regex.exec(window.location.href);
            if (qs == null)
                return default_;
            else
                return qs[1];
        }






        function isMaxLength(obj) {
            var maxlength = obj.value;
            if (maxlength.length > 500) {
                obj.value = obj.value.substring(0, 499)
            }
            return true;
        }

        function IsValidate(obj) {


            var rmaDate = document.getElementById("<%= txtRMADate.ClientID %>").value;
            if (rmaDate == "") {
                alert('RmaDate cannot be empty!');
                return false;
            }
            else {
                var arr = rmaDate.split('/');
                var months = Math.abs(arr[0] - 1);
                var days = arr[1];
                var years = arr[2];
                var dateRange = "365";


                var oneDay = 24 * 60 * 60 * 1000; // hours*minutes*seconds*milliseconds

                var firstDate = new Date();

                var secondDate = new Date(years, months, days);

                var diffDays = Math.abs((firstDate.getTime() - secondDate.getTime()) / (oneDay));
                diffDays = Math.round(diffDays);

                if (diffDays > dateRange) {
                    var dateRangeMsg = "Invalid RMA Date! Can not create RMA before 365 days back.";
                    alert(dateRangeMsg);
                    return false;
                }


            }

            var rmaItemcount = document.getElementById("<%= hdnRmaItemcount.ClientID %>").value;
            if (rmaItemcount < 2) {
                alert('There is no ESN to insert!');
                return false;
            }


            var objrmaStatus = document.getElementById("<%= ddlStatus.ClientID %>");
            if (objrmaStatus != null) {
                var allStatusChk = document.getElementById("<%= chkAll.ClientID %>");
                if (allStatusChk != null && !allStatusChk.checked) {
                    var rmaStatus = objrmaStatus.options[objrmaStatus.selectedIndex].text;
                    var arrtxt = document.getElementsByTagName("select");
                    var arrchk = document.getElementsByTagName("input");
                    for (var k = 0; k < arrtxt.length - 1; k++) {
                        if (arrtxt[k].id.indexOf("ddl_Status") > -1) {
                            var esnchk = document.getElementById(arrtxt[k].id.replace('ddl_Status', 'chkESN'));
                            var objstatus = arrtxt[k].options[arrtxt[k].selectedIndex].text;
                            if (esnchk.checked) {
                                if ("Approved" == rmaStatus) {
                                    if ("Approved" == objstatus || "Returned" == objstatus || "Cancelled" == objstatus) { }
                                    else {
                                        var msg = "Item is " + objstatus + " so can not approve the RMA";
                                        alert(msg);
                                        return false;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            if (obj == 1) {
                
                var objValidateEsn = document.getElementById("<%= hdnValidateESNs.ClientID %>").value;
                document.getElementById("<%= hdnValidateESNs.ClientID %>").value = "2";
//                if (objValidateEsn != '1') {
//                    var validflag = confirm('Do you want to VALIDATE the RMA before submit Yes/No');
//                    if (validflag) {
//                        document.getElementById("<%= hdnValidateESNs.ClientID %>").value = "2";
//                        //document.getElementById("<%= btnValidate.ClientID %>").click();
//                        //return false;

//                    }
//                    else {
//                        document.getElementById("<%= hdnValidateESNs.ClientID %>").value = "0";
//                        return false;
//                    }
//                }

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

        function alphaNumericCheck(fieldVal) {
            if ((105 >= fieldVal.keyCode && fieldVal.keyCode >= 96)
                || (90 >= fieldVal.keyCode && fieldVal.keyCode >= 65)
                || (57 >= fieldVal.keyCode && fieldVal.keyCode >= 48)
                 || (122 >= fieldVal.keyCode && fieldVal.keyCode >= 97)) {
                return true;
            }
            else {
                return false;
            }
        }

        function doReadonly(evt) {

            evt.keyCode = 0;
            return false;
        }

        function set_focus() {
            var img = document.getElementById("img2");
            var st = document.getElementById("txtRemarks");
            st.focus();
            img.click();
        }
        function set_focus1() {
            var img = document.getElementById("img1");
            var st = document.getElementById("ddlStatus");
            st.focus();
            img.click();
        }

        function hideEsn(val) {

            var mode = getQuerystring('mode');
            var rmaGUID = getQuerystring('rmaGUID');

            if ('esn' == mode || rmaGUID != '' || val == 1) {

                var rmaItemcount = document.getElementById("<%= hdnRmaItemcount.ClientID %>").value;

                if (rmaItemcount > 1) {
                    var submitbtn = document.getElementById("<%= btnSubmitRMA.ClientID %>");
                    //var btnValidate = document.getElementById("<%= btnValidate.ClientID %>");

                    submitbtn.disabled = false;
                    //btnValidate.disabled = false;
                }
            }
            var comapnyName = document.getElementById('hdncompanyname').value;
            var calltime = document.getElementById('tdCallTime');
            var reason = document.getElementById('tdReason');
            var tdNotes = document.getElementById('tdNotes');
            if (calltime != null && reason != null && tdNotes != null) {

                if (comapnyName == 'iWireless') {

                    calltime.style.display = "none";
                    reason.style.display = "none";
                    tdNotes.style.display = "none";
                }
                else {



                    calltime.style.display = "block";
                    reason.style.display = "block";
                    tdNotes.style.display = "block";
                }
            }
            var hdn_msg = document.getElementById("hdnmsg");
            if (hdn_msg != null) {
                if (hdn_msg.value == 'ESN already added to the RMA!' || hdn_msg.value == 'Esn Should be between 8 to 30 digits!')
                    alert(hdn_msg.value);
                hdn_msg.value = "";
            }

        } 
        function checkcompany() {

            var UserID = document.getElementById("<%=hdnUserID.ClientID %>");
            if (UserID.value == '0') {
                var company = document.getElementById("<%=ddlCompany.ClientID %>");
                if (company.selectedIndex == 0) {
                    alert('Company not selected!');
                    return false;
                }
            }
            return true;
        }
        function confirmclear() {
            var lblConfirm = document.getElementById("lblConfirm");
            lblConfirm.value = "";
        }
    </script>
    
<%--        <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.4.0/jquery.min.js"></script>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/jquery-ui.min.js"></script>
        <script type="text/javascript" src="../JQuery/jquery.blockUI.js"></script>
        <script type="text/javascript" src="/JSLibrary/jquery.blockUI.js"></script>--%>
	
</body>
</html>
