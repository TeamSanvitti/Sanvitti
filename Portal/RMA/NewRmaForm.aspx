<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewRmaForm.aspx.cs" Inherits="avii.RMA.NewRmaForm" Trace="false" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="~/Controls/Header.ascx" %>
<%@ Register TagPrefix="RMA" TagName="RmaHistory" Src="~/Controls/RmaHistory.ascx" %>
<%@ Register TagPrefix="UC" TagName="Comments" Src="~/Controls/ForecastComment.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>.:: Return Merchandise Authorization (RMA) ::.</title>

    <link href="/aerostyle.css" type="text/css" rel="stylesheet"/>
<%--<link type="text/css" href="../../UIThemes/themes/base/ui.all.css" rel="stylesheet" />--%>
    <link rel="stylesheet"  type="text/css" href="../fullfillment/calendar/dhtmlgoodies_calendar.css" media="screen"></link>
	<script type="text/javascript" src="../fullfillment/calendar/dhtmlgoodies_calendar.js"></script>  
	<%--<script type="text/javascript" src="../../JSLibrary/jquery-1.3.2.min.js"></script>--%>
     <style type="text/css">
        .style1
        {
            FONT-SIZE: 10px;
            COLOR: #000000;
            FONT-FAMILY: Verdana, Arial, Helvetica, sans-serif;
            width: 14%;
        }
        .style2
        {
            width: 18%;
        }
        .style3
        {
            width: 173px;
        }
        .style4
        {
            FONT-SIZE: 10px;
            COLOR: #000000;
            FONT-FAMILY: Verdana, Arial, Helvetica, sans-serif;
            width: 173px;
        }
    </style>
   <!-- from http://encosia.com/2009/10/11/do-you-know-about-this-undocumented-google-cdn-feature/ -->
    <link href="https://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/themes/start/jquery-ui.css" type="text/css" rel="Stylesheet" />
	
	<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.4.0/jquery.min.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/jquery-ui.min.js"></script>
    <%--<script type="text/javascript" src="/JQuery/jquery.min.js"></script>
    <script type="text/javascript" src="/JQuery/jquery-ui.min.js"></script>
	--%>
	<script type="text/javascript" src="/JQuery/jquery.blockUI.js"></script>
	
<!-- fix for 1.1em default font size in Jquery UI -->
	<style type="text/css">
		.ui-widget{font-size:11px !important;}
		.ui-state-error-text{margin-left: 10px}
	</style>
    
	<script type="text/javascript">
		$(document).ready(function() {

        $("#divComments").dialog({
	            autoOpen: false,
	            modal: false,
	            minHeight: 20,
	            height: 260,
	            width: 450,
	            resizable: false,

	            open: function (event, ui) {
	                $(this).parent().appendTo("#divAddEditRMA");
	            }
	        });


			$("#divEditRMA").dialog({
				autoOpen: false,
				modal: true,
				minHeight: 20,
				height: 510,
				width: 750,
				resizable: false,
				open: function(event, ui) {
					$(this).parent().appendTo("#divAddEditRMA");
				},
			});
            $("#divHistory").dialog({
				autoOpen: false,
				modal: true,
				minHeight: 20,
				height: 350,
				width: 600,
				resizable: false,
				open: function(event, ui) {
					$(this).parent().appendTo("#divAddEditRMA");
				},
			});
            $("#divAddRMA").dialog({
				autoOpen: false,
				modal: true,
				minHeight: 20,
				height: 400,
				width: 800,
				resizable: false,
				open: function(event, ui) {
					$(this).parent().appendTo("#divAddEditRMA");
				},
			});
 $("#divRMADoc").dialog({
				autoOpen: false,
				modal: true,
				minHeight: 20,
				height: 355,
				width: 500,
				resizable: false,
				open: function(event, ui) {
					$(this).parent().appendTo("#divAddEditRMA");
				},
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


		function closeDialog() {
			//Could cause an infinite loop because of "on close handling"
			$("#divEditRMA").dialog('close');
		}
        function closeAddDialog() {
			//Could cause an infinite loop because of "on close handling"
			$("#divAddRMA").dialog('close');
		}
        function closeHistoryDialog() {
			//Could cause an infinite loop because of "on close handling"
			$("#divHistory").dialog('close');
		}


function closeRmaDocDialog() {
			//Could cause an infinite loop because of "on close handling"
			$("#divRMADoc").dialog('close');
		}
		
		
		function openDialog(title, linkID) {
		
			var pos = $("#" + linkID).position();
			var top = pos.top;
			var left = pos.left + $("#" + linkID).width() + 10;
			//alert(left);
            //top = top - 500;
if(top > 400)
	top = 10;
			left = 275;
			$("#divEditRMA").dialog("option", "title", title);
			$("#divEditRMA").dialog("option", "position", [left, top]);
			
			$("#divEditRMA").dialog('open');
		}

        
		function openAddDialog(title, linkID) {
		
			var pos = $("#" + linkID).position();
			var top = pos.top;
			var left = pos.left + $("#" + linkID).width() + 10;
			
if(top > 400)
	top = 10;

			left = 275;
			$("#divAddRMA").dialog("option", "title", title);
			$("#divAddRMA").dialog("option", "position", [left, top]);
			
			$("#divAddRMA").dialog('open');

		}

        function openHistoryDialog(title, linkID) {
		
			var pos = $("#" + linkID).position();
			var top = pos.top;
			var left = pos.left + $("#" + linkID).width() + 10;
			//alert(left);
            top = top - 300;
			left = 300;
			$("#divHistory").dialog("option", "title", title);
			$("#divHistory").dialog("option", "position", [left, top]);
			
			$("#divHistory").dialog('open');
		}
function openRmaDocDialog(title, linkID) {
		
			var pos = $("#" + linkID).position();
			var top = pos.top;
			var left = pos.left + $("#" + linkID).width() + 10;
			//alert(left);
            top = top - 300;
			left = 370;
			$("#divRMADoc").dialog("option", "title", title);
			$("#divRMADoc").dialog("option", "position", [left, top]);
			
			$("#divRMADoc").dialog('open');
		}



		function openEditDialogAndBlock(title, linkID) {
			openDialog(title, linkID);

			//block it to clean out the data
			$("#divEditRMA").block({
				message: '<img src="../images/async.gif" />',
				css: { border: '0px' },
				fadeIn: 0,
				//fadeOut: 0,
				overlayCSS: { backgroundColor: '#ffffff', opacity: 1 } 
			});
		}
        function openAddDialogAndBlock(title, linkID) {
        var userID = $('#hdnUserID').val();
            //alert(userID);
            var customerValue = 1;
            
            if(userID == 0)
            {
                //customerValue = $('#ddlCompany').get(0).selectedIndex;
            }
            if(customerValue > 0)
            {
			openAddDialog(title, linkID);
            //alert('open');
			//block it to clean out the data
			$("#divAddRMA").block({
				message: '<img src="../images/async.gif" />',
				css: { border: '0px' },
				fadeIn: 0,
				//fadeOut: 0,
				overlayCSS: { backgroundColor: '#ffffff', opacity: 1 } 
			});
            }
            else
            {
                alert('Select a Customer first');
            //$("#divAddRMA").unblock();
            }
		}
        
        function openHistoryDialogAndBlock(title, linkID) {
			openHistoryDialog(title, linkID);

			//block it to clean out the data
			$("#divHistoryRMA").block({
				message: '<img src="../images/async.gif" />',
				css: { border: '0px' },
				fadeIn: 0,
				//fadeOut: 0,
				overlayCSS: { backgroundColor: '#ffffff', opacity: 1 } 
			});
		}
function openRmaDocDialogAndBlock(title, linkID) {
			        openRmaDocDialog(title, linkID);

			        //block it to clean out the data
			        $("#divRMADoc").block({
				        message: '<img src="../images/async.gif" />',
				        css: { border: '0px' },
				        fadeIn: 0,
				        //fadeOut: 0,
				        overlayCSS: { backgroundColor: '#ffffff', opacity: 1 } 
			        });
		        }

		

		
		function unblockEditDialog() {
			$("#divEditRMA").unblock();
		}
        function unblockAddDialog() {
			$("#divAddRMA").unblock();
		}
        function unblockHistoryDialog() {
			$("#divHistory").unblock();
		}
function unblockRmaDocDialog() {
			$("#divRMADoc").unblock();
		}


		function onTest() {
			$("#divEditRMA").block({
				message: '<h1>Processing</h1>',
				css: { border: '3px solid #a00' },
				overlayCSS: { backgroundColor: '#ffffff', opacity: 1 } 
			});
		}
	</script>
<div runat="server">  
    
	
    <script type="text/javascript" language="javascript">

function checkTextAreaMaxLength(textBox, e, maxLength) {
            //if (!checkSpecialKeys(e)) 
            {
                if (textBox.value.length > maxLength) {
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



        function ReadOnly(evt) {
            var imgCall = document.getElementById("imgCal");
            imgCall.click();
            evt.keyCode = 0;
            return false;

        }
        function ReadOnlyWarranty(evt) {
            var imgW = document.getElementById("img3");
            imgW.click();
            evt.keyCode = 0;
            return false;

        }
        function ReadOnlyEditWarranty(evt) {
            var imgEW = document.getElementById("img5");
            imgEW.click();
            evt.keyCode = 0;
            return false;

        }
        function ReadOnlyRestock(evt) {
            var imgRs = document.getElementById("img2");
            imgRs.click();
            evt.keyCode = 0;
            return false;

        }
        function WarrantyAlert(ddlWarranty) {
            //var ddlWarranty = document.getElementById("<%= dpWarranty.ClientID %>");
            if (ddlWarranty.value == '1') {

                alert('Please enter the Warranty date and Notes');
                //var rmaWarranty = document.getElementById("<%= txtWarranty.ClientID %>").value;
                //if(rmaWarranty=='')
            }
        }

        function IsValidEdit() {

            var ddlWarranty = document.getElementById("<%= ddlEditWarranty.ClientID %>");
            if (ddlWarranty.value == '1') {

                var rmaWarranty = document.getElementById("<%= txtEditWarrantyDate.ClientID %>").value;
                var notes = document.getElementById("<%= txtEditNotes.ClientID %>").value;

                if (rmaWarranty == '' && notes == '') {
                    alert('Please enter the Warranty date and Notes');
                    return false;

                }
                if (rmaWarranty == '' && note != '') {
                    alert('Please enter the Warranty date');
                    return false;

                }
                else {
//                    if (rmaWarranty != '') {
//                        var arr = rmaWarranty.split('/');
//                        var months = Math.abs(arr[0] - 1);
//                        var days = arr[1];
//                        var years = arr[2];
//                        var dateRange = "120";


//                        var oneDay = 24 * 60 * 60 * 1000; // hours*minutes*seconds*milliseconds

//                        var firstDate = new Date();

//                        var secondDate = new Date(years, months, days);

////                        var diffDays = Math.abs((secondDate.getTime() - firstDate.getTime()) / (oneDay));
////                        diffDays = Math.round(diffDays);
////                        alert(diffDays);
////                        if (diffDays < 0) {
////                            var dateRangeMsg = "Invalid RMA Date! Can not be less than today date.";
////                            alert(dateRangeMsg);
////                            return false;
//                        //}
//                    }
//                    else
                        if (notes == '') {
                            alert('Please enter the Notes');
                            return false;
                        }
                }
            }


            }
            function getDateObject(dateString, dateSeperator) {
                //This function return a date object after accepting 
                //a date string ans dateseparator as arguments
                var curValue = dateString;
                var sepChar = dateSeperator;
                var curPos = 0;
                var cDate, cMonth, cYear;

                //extract day portion
                curPos = dateString.indexOf(sepChar);
                cDate = dateString.substring(0, curPos);

                //extract month portion	
                endPos = dateString.indexOf(sepChar, curPos + 1); cMonth = dateString.substring(curPos + 1, endPos);

                //extract year portion	
                curPos = endPos;
                endPos = curPos + 5;
                cYear = curValue.substring(curPos + 1, endPos);

                //Create Date Object
                dtObject = new Date(cYear, cMonth, cDate);
                return dtObject;
            }
        function IsValid() {
            var esn = document.getElementById("<%= txtESN.ClientID %>").value;
            if (esn == '') {
                alert('ESN can not be empty')
                return false;
            } else {
                var esnLength = esn.length;
                if (esnLength < 8 || esnLength > 30) {
                    alert('Esn Should be between 8 to 30 digits!');
                    return false;
                }
            }
            var ddlWarranty = document.getElementById("<%= dpWarranty.ClientID %>");
            if (ddlWarranty.value == '1') {

                var rmaWarranty = document.getElementById("<%= txtWarranty.ClientID %>").value;
                var notes = document.getElementById("<%= txtNotes.ClientID %>").value;

                if (rmaWarranty == '' && notes == '') {
                    alert('Please enter the Warranty date and Notes');
                    return false;
                
                }
                if (rmaWarranty == '' && note != '') {
                    alert('Please enter the Warranty date');
                    return false;

                }
                else {
                    if (rmaWarranty != '') {

                        var today = new Date();
                        //today.setHours(0);
                        //today.setMinutes(0);
                        //today.setSeconds(0);
                        //today.setMiliseconds(0);
                        var oneDay = 24 * 60 * 60 * 1000;

                        var warranty = new Date(rmaWarranty);
                        //var dif = warranty - today;
                       // var add = dif + oneDay; 
                    //    alert(dif + oneDay)
                        //warranty.setHours(0);
                        //warranty.setMinutes(0);
                        //warranty.setSeconds(0);
                        //warranty.setMiliseconds(0);

                       // alert(add)


                        //alert(today)
                        //if (new Date(rmaWarranty) < new Date()) {
                        //if (warranty < today) {
                        if (new Date(rmaWarranty) < new Date()) {
                            var dateRangeMsg = "Invalid RMA Date! Can not be less than today date.";
                            alert(dateRangeMsg);
                            return false;
                        }
                        var arr = rmaWarranty.split('/');
                        var months = Math.abs(arr[0] - 1);
                        var days = arr[1];
                        var years = arr[2];
                        var dateRange = "120";


                        var oneDay = 24 * 60 * 60 * 1000; // hours*minutes*seconds*milliseconds

                        var firstDate = new Date();

                        var secondDate = new Date(years, months, days);
                        if (secondDate.getTime() < firstDate.getTime()) {
                            alert('can not be less')
                            return false; }
                        //alert(firstDate)
                        //alert(secondDate)
                        //alert(firstDate.getTime())
                        //alert(secondDate.getTime())

                        //if (secondDate < firstDate)
                        //    alert('hello');
                        //var diffDays = Math.abs((firstDate.getTime() - secondDate.getTime()) / (oneDay));
                        //alert(diffDays)
                        //diffDays = Math.round(diffDays);
                        //alert(diffDays);
//                        if ((secondDate < firstDate)) {
//                            var dateRangeMsg = "Invalid RMA Date! Can not be less than today date.";
//                            alert(dateRangeMsg);
//                            return false;
//                        }
                    }
                    
                    if (notes == '') {
                        alert('Please enter the Notes');
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
        

        function IsValidate(obj) {


            var rmaDate = document.getElementById("<%= txtRMADate.ClientID %>").value;
            if (rmaDate == "") {
                alert('RmaDate cannot be empty!');
                return false;
            }
            else {
                var UserID = document.getElementById("<%=hdnUserID.ClientID %>");
                if (UserID.value == '1') {
                    var arr = rmaDate.split('/');
                    var months = Math.abs(arr[0] - 1);
                    var days = arr[1];
                    var years = arr[2];
                    var dateRange = "120";


                    var oneDay = 24 * 60 * 60 * 1000; // hours*minutes*seconds*milliseconds

                    var firstDate = new Date();

                    var secondDate = new Date(years, months, days);

                    var diffDays = Math.abs((firstDate.getTime() - secondDate.getTime()) / (oneDay));
                    diffDays = Math.round(diffDays);

                    if (diffDays > dateRange) {
                        var dateRangeMsg = "Invalid RMA Date! Can not create RMA before 120 days back.";
                        alert(dateRangeMsg);
                        return false;
                    }
                }
                else {
                     var company = document.getElementById("<%=ddlCompany.ClientID %>");
                    //alert(company)
                    if (company != null && company.selectedIndex == '0') {
                        alert("Please select a company");
                        return false;
                    }
                
                }


            }

            var rmaItemcount = document.getElementById("<%= hdnRmaItemcount.ClientID %>").value;
            if (rmaItemcount < 1) {
                alert('There is no ESN to insert!');
                return false;
            }


            var objrmaStatus = document.getElementById("<%= ddlStatus.ClientID %>");
            if (objrmaStatus != null) {
                var allStatusChk = document.getElementById("<%= chkApplyAll.ClientID %>");
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

            
        }



        function isMaxLength(obj) {
            var maxlength = obj.value;
            if (maxlength.length > 500) {
                obj.value = obj.value.substring(0, 499)
            }
            return true;
        }
function isNumberKey(evt) {

            var charCodes = evt.keyCode ? evt.keyCode : evt.which ? evt.which : evt.charCode;
            if (charCodes > 31 && (charCodes < 48 || charCodes > 57)) {
                charCodes = 0;
                return false;
            }
            return true;
        }

        function alphaNumericCheck(evt) {
            var charCodes = evt.keyCode ? evt.keyCode : evt.which ? evt.which : evt.charCode ? evt.charCode : evt.type;
            // alert(charCodes);
            if (charCodes == 8 || charCodes == 9 || (charCodes >= 35 && charCodes < 40) || charCodes == 46 || (105 >= charCodes && charCodes >= 96)
                || (90 >= charCodes && charCodes >= 65)
                || (57 >= charCodes && charCodes >= 48)
                 || (122 >= charCodes && charCodes >= 97)) {
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
   
   </div> 

</head>
<body bgcolor="#ffffff"  leftmargin="0" topmargin="0" marginwidth="0" marginheight="0">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager> 
       <table cellspacing="0" cellpadding="0" border="0"  width="100%" align="center">
        <tr>
            <td>
                <head:MenuHeader ID="HeadAdmin1" runat="server" ></head:MenuHeader>
            </td>
        </tr> 
        </table> 

        <div id="divAddEditRMA">
         <div id="divComments" style="display:none">
					
				<asp:UpdatePanel ID="upComments" runat="server">
					<ContentTemplate>
                   
						<asp:PlaceHolder ID="phComments" runat="server">
                            
                        <table bordercolor="#839abf" border="1" width="98%" align="center" cellpadding="0" cellspacing="0">
                                <tr><td>

                                <table border="0" width="100%" class="box" align="center" cellpadding="5" cellspacing="5">
                                <tr valign="top">
                                    <td>
                                    <strong> RMA#: </strong>&nbsp;&nbsp;&nbsp;<asp:Label ID="lblRNum" runat="server" CssClass="copy10grey"></asp:Label>
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
       		
			<div id="divEditRMA" style="display:none">
					
				<asp:UpdatePanel ID="upnlEditRMA" runat="server">
					<ContentTemplate>
						<asp:PlaceHolder ID="phrEditRMA" runat="server">
		                    <table width="100%">
                             <tr>
            <td>
                <div style="width:100%; border: 0px solid #839abf" >
      
                <table width="100%">
                <tr>
                    <td width="100%">
                        <table width="100%">
                        <tr>
                        <td align="left" class="copy11">
                           &nbsp;&nbsp;ESN: &nbsp;<asp:Label ID="lblEsn" runat="server" CssClass="copy12hd"></asp:Label>
                        </td>
                        <td align="right">
                            <%--<asp:Button ID="btnEditClose" runat="server" Text="Close" CssClass="button" />--%>
                        </td>
                        </tr>
                        </table>
                        
                    </td>
                </tr>
                <tr>
                    <td >
                    <table border="0" width="100%" class="box" align="center" cellpadding="0" cellspacing="0">
                    <tr valign="top">
                        <td>
                            <asp:Label ID="lblEditMsg" runat="server" CssClass="errormessage"></asp:Label>
                        </td>
                    </tr>

                    </table>
                    <table bordercolor="#839abf" border="1" width="98%" align="center" cellpadding="0" cellspacing="0">
                    <tr><td>

                    <table border="0" width="100%" class="box" align="center" cellpadding="5" cellspacing="5">
                    <tr valign="top">
                        <td align="left" class="copy10grey" style="width:15%">
                            Device State:<%--&nbsp;<span class="errormessage">*</span>--%>

                        </td>
                        <td align="left" class="copy10grey" style="width:35%">
                            <asp:DropDownList ID="ddlDeviceState" runat="server">
                            <asp:ListItem  Value="0" Text=""></asp:ListItem>
                            <asp:ListItem  Value="1" Text="Complete"></asp:ListItem>
                            <asp:ListItem  Value="2" Text="InComplete"></asp:ListItem>
                            </asp:DropDownList>
                           </td>
                        <td align="left" class="copy10grey" style="width:15%">
                            Device Designation:<%--&nbsp;<span class="errormessage">*</span>--%>

                        </td>
                        <td align="left" class="copy10grey" style="width:35%">   
                        <asp:DropDownList ID="ddlDeviceDesig" runat="server">
                              <asp:ListItem Value="0" Text=""></asp:ListItem>
                            <asp:ListItem  Value="1" Text="RTS"></asp:ListItem>
                            <asp:ListItem  Value="2" Text="RTM"></asp:ListItem>
                            <asp:ListItem  Value="3" Text="Other"></asp:ListItem>
                            </asp:DropDownList>                                     
                        </td>
                    </tr>
                    <tr valign="top">
                        <td align="left" class="copy10grey">
                            Device Condition:<%--&nbsp;<span class="errormessage">*</span>--%>

                        </td>
                        <td align="left" class="copy10grey">
                             <asp:DropDownList ID="ddlDeviceCond" runat="server"> 
                             <asp:ListItem  Value="0" Text=""></asp:ListItem>
                            <asp:ListItem  Value="1" Text="New"></asp:ListItem>
                            <asp:ListItem  Value="2" Text="Used"></asp:ListItem>
                            <asp:ListItem  Value="3" Text="Damaged"></asp:ListItem>
                            </asp:DropDownList>  
                        </td>
                        <td align="left" class="copy10grey">
                          Device Defect:<%--&nbsp;<span class="errormessage">*</span>--%>

                        </td>
                        <td align="left" class="copy10grey">
                                   
                             <asp:TextBox ID="txtDefect" CssClass="copy10grey" MaxLength="30"  runat="server" Width="80%"></asp:TextBox>                             
                        </td>
                    </tr>
                   
                    <tr valign="top">
                        <td align="left" class="copy10grey">
                            Call Time:
                        </td>
                        <td align="left" class="copy10grey">
                             <asp:TextBox ID="txtEditCallTime" Width="80%" CssClass="copy10grey" MaxLength="3" onkeypress="return isNumberKey(event);"   runat="server"></asp:TextBox>
                        </td>
                        <td align="left" class="copy10grey">
                           Reason:
                        </td>
                        <td align="left" class="copy10grey">
                        <asp:DropDownList ID="ddlEditReason"  runat="server" Width="80%"  CssClass ="copy10grey"> 
                                <asp:ListItem  Value="0" Text="" ></asp:ListItem>
                                <asp:ListItem Value="13">ActivationIssues</asp:ListItem>
 
                                                        <asp:ListItem Value="2">AudioIssues</asp:ListItem>

                                                        <asp:ListItem Value="8">BuyerRemorse</asp:ListItem>
 
                                                        <asp:ListItem Value="14">CoverageIssues</asp:ListItem>

                                                        <asp:ListItem Value="1" >DOA</asp:ListItem>
 
                                                        <asp:ListItem  Value="11">DropCalls</asp:ListItem>

                                                        <asp:ListItem  Value="17">HardwareIssues</asp:ListItem>
 
                                                        <asp:ListItem  Value="10">LiquidDamage</asp:ListItem>

                                                        <asp:ListItem  Value="15">LoanerProgram</asp:ListItem>
 
                                                        <asp:ListItem  Value="6">MissingParts</asp:ListItem>

                                                        <asp:ListItem  Value="5">Others</asp:ListItem>
 
                                                        <asp:ListItem  Value="9">PhysicalAbuse</asp:ListItem>

                                                        <asp:ListItem  Value="4">PowerIssues</asp:ListItem>
 
                                                        <asp:ListItem  Value="7">ReturnToStock</asp:ListItem>

                                                        <asp:ListItem  Value="3">ScreenIssues</asp:ListItem>
 
                                                        <asp:ListItem  Value="16">ShippingError</asp:ListItem>

                                                        <asp:ListItem  Value="12">Software</asp:ListItem>
                            </asp:DropDownList>    
                                                                 
                        </td>
                    </tr>
                    <tr>
                    <td align="left" class="copy10grey">
                    Warranty:
                    </td>
                    <td align="left" >
                        <asp:DropDownList ID="ddlEditWarranty" runat="server" Width="80%"  CssClass ="copy10grey"> 
                                                                    <asp:ListItem  Value="0" Text="" ></asp:ListItem>
                                                                     <asp:ListItem Value="1" Text="Warranty"></asp:ListItem>
                                                                    <asp:ListItem  Value="2" Text="Out of Warranty"></asp:ListItem>
                                                            </asp:DropDownList>
                    </td>
                    <td align="left" class="copy10grey">
                    Warranty expiery Date:
                    </td>
                    <td align="left" class="copy10grey">
                        <asp:TextBox ID="txtEditWarrantyDate" ContentEditable="false" onkeydown="return ReadOnlyEditWarranty(event);"  runat="server" CssClass="copy10grey"></asp:TextBox>&nbsp;
                        <img id="img5" alt=""  src="/fullfillment/calendar/sscalendar.jpg"/>
                        <cc1:CalendarExtender ID="cldExtender4" runat="server" PopupButtonID="img5" 
                        PopupPosition="BottomLeft" Format="MM/dd/yyyy"   TargetControlID="txtEditWarrantyDate">
                        </cc1:CalendarExtender>
                    </td>
                    </tr>
                    <tr><td align="left" class="copy10grey">Disposition:</td>
                        <td align="left" > <asp:DropDownList ID="dpDisposition"  runat="server" Width="100%"  CssClass ="copy10grey"> 
                                                                    <asp:ListItem  Value="0" Text="" ></asp:ListItem>
                                                                     <asp:ListItem Value="1" Text="Credit"  ></asp:ListItem>
                                                                    <asp:ListItem  Value="2" Text="Replacement"></asp:ListItem>
                                                                    <asp:ListItem  Value="3" Text="Repair"></asp:ListItem>
                                                                    
                                                            </asp:DropDownList></td>
                        <td align="left" class="copy10grey">
                        Status:
                        </td>
                        <td>
                            <asp:Label ID="Label1" Visible="false" runat="server" Text="Pending"></asp:Label>
                            <asp:DropDownList ID="ddlEditStatus"  runat="server" Class="copy10grey" 
                                Width="80%" >
                            <asp:ListItem  Value="0" >------</asp:ListItem>
                            <asp:ListItem  Value="6">Approved</asp:ListItem>
                                                        <asp:ListItem  Value="12">Back to Stock -NDF</asp:ListItem>
                                                        <asp:ListItem  Value="14">Back to Stock – Replaced by OEM</asp:ListItem>
                                                        <asp:ListItem  Value="13">Back to Stock- Credited</asp:ListItem>
                                                        <asp:ListItem Value="30" >Cancel</asp:ListItem>
                                                        <asp:ListItem  Value="10">Closed</asp:ListItem>
                                                        <asp:ListItem  Value="8">Credited</asp:ListItem>
                                                        <asp:ListItem Value="26" >Damaged</asp:ListItem>
                                                        <asp:ListItem  Value="9">Denied</asp:ListItem>
                                                        <asp:ListItem Value="31" >External ESN</asp:ListItem>
                                                        <asp:ListItem Value="25" >Incomplete</asp:ListItem>
                                                        <asp:ListItem  Value="19">NDF (No Defect Found)</asp:ListItem>
                                                        <asp:ListItem  Value="11" >Out with OEM for repair</asp:ListItem>
                                                        <asp:ListItem Value="1" >Pending</asp:ListItem>
                                                        <asp:ListItem  Value="4">Pending for Credit</asp:ListItem>
                                                        <asp:ListItem  Value="3">Pending for Repair</asp:ListItem>
                                                        <asp:ListItem  Value="5">Pending for Replacement</asp:ListItem>
                                                        <asp:ListItem Value="32" >Pending ship to OEM</asp:ListItem>
                                                        <asp:ListItem Value="34" >Pending ship to Supplier</asp:ListItem>
                                                        <asp:ListItem  Value="20">PRE-OWNED – A stock</asp:ListItem>
                                                        <asp:ListItem Value="21" >PRE-OWEND - B Stock</asp:ListItem>
                                                        <asp:ListItem Value="22" >PRE-OWEND – C Stock</asp:ListItem>
                                                        <asp:ListItem Value="27" >Preowned</asp:ListItem>
                                                        <asp:ListItem  Value="2">Received</asp:ListItem>
                                                        <asp:ListItem Value="23" >Rejected</asp:ListItem>
                                                        <asp:ListItem  Value="18">Repaired By AV</asp:ListItem>
                                                        <asp:ListItem  Value="15">Repaired by OEM</asp:ListItem>
                                                        <asp:ListItem  Value="17">Replaced BY AV</asp:ListItem>
                                                        <asp:ListItem  Value="16">Replaced BY OEM</asp:ListItem>
<asp:ListItem  Value="38">Replaced by OEM- New</asp:ListItem>
                                                        <asp:ListItem  Value="39">Replaced by OEM- Preowned</asp:ListItem>
                                                        <asp:ListItem Value="28" >Return to OEM</asp:ListItem>
                                                        <asp:ListItem  Value="7">Returned</asp:ListItem>
                                                        <asp:ListItem Value="36" >Returned from OEM</asp:ListItem>
                                                        <asp:ListItem Value="29" >Returned to Stock</asp:ListItem>
                                                        <asp:ListItem Value="24" >RTS (Return To Stock)</asp:ListItem>
                                                        <asp:ListItem Value="33" >Sent to OEM</asp:ListItem>
                                                        <asp:ListItem Value="35" >Sent to Supplier</asp:ListItem>

                                                        

                            </asp:DropDownList>  
                        </td>
                        
                    </tr>
                     <tr valign="top">
                        <td align="left" class="copy10grey">
                           Re-Stocking Fee:

                        </td>
                        <td align="left" class="copy10grey">
                              <asp:TextBox ID="txtReStockingFee" CssClass="copy10grey" MaxLength="4"  runat="server" Width="80%"></asp:TextBox>
                        
                        </td>
                        <td align="left" class="copy10grey">
                           Re-Stocking Date:

                        </td>
                        <td align="left" class="copy10grey">
                             <asp:TextBox ID="txtReStockingDate" ContentEditable="false" onkeydown="return ReadOnlyRestock(event);"  runat="server" CssClass="copy10grey"></asp:TextBox>&nbsp;
                        <img id="img2" alt=""  src="/fullfillment/calendar/sscalendar.jpg"/>
                        <cc1:CalendarExtender ID="cldExtender1" runat="server" PopupButtonID="img2" 
                        PopupPosition="BottomLeft" Format="MM/dd/yyyy"   TargetControlID="txtReStockingDate">
                        </cc1:CalendarExtender>                                     
                        </td>
                    </tr>
                    <tr valign="top">
                        <td align="left" class="copy10grey">
                           New SKU#:

                        </td>
                        <td align="left" class="copy10grey">
                             <%-- <asp:TextBox ID="txtNewSKU"  Width="80%" CssClass="copy10grey" MaxLength="20"  runat="server"></asp:TextBox>--%>
                            <asp:DropDownList ID="ddlSKU" runat="server" CssClass="copy10grey" Width="81%">
                            </asp:DropDownList>
                        </td>
                        <td align="left" class="copy10grey">
                          Location Code:

                        </td>
                        <td align="left" class="copy10grey">
                            <asp:TextBox ID="txtLocationCode" Width="80%" CssClass="copy10grey" MaxLength="30"  runat="server"></asp:TextBox>                                    
                        </td>
                    </tr>
                    <tr valign="top">
                        <td align="left" class="copy10grey">
                            Notes:
                        </td>
                        <td align="left" class="copy10grey" colspan="3">
                             <asp:TextBox ID="txtEditNotes" TextMode="MultiLine" Rows="8" CssClass="copy10grey"  Width="92%"  runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    </table>
                    
                    </td></tr>
                    </table>
                    <br />
                    <table width="100%" align="center">
                    <tr>
                        <td align="center">
                             <asp:Button ID="btnEditRMA" runat="server" Text="Submit" OnClientClick="return IsValidEdit();" CssClass="button" OnClick="btnEditLineItem_Click" />
                             &nbsp;
                             <asp:Button ID="btnEditCancel" runat="server" Text="Cancel" CssClass="button" OnClientClick="return closeDialog();" />
                        </td>
                    </tr>
                    </table>
                    </td>
                </tr>
                </table>
            
                </div>
            

            </td>
        </tr>
                            </table>

                        </asp:PlaceHolder>
						
						
					</ContentTemplate>
							
				</asp:UpdatePanel>
			
			</div>
            <div id="divAddRMA" style="display:none">
					
				<asp:UpdatePanel ID="upnlAddRMA" runat="server">
					<ContentTemplate>
						<asp:PlaceHolder ID="phrAddRMA" runat="server">
<asp:Panel ID="pnlAdd" runat="server" DefaultButton="btnAdd">
		
                                                                                            
  <table width="100%">
        <tr>
            <td>
                <%--<div style="overflow:auto; height:450px; width:100%; border: 0px solid #839abf" >
      --%>
                <table width="100%">
                <%--<tr>
                    <td class="button">
                        Add RMA Line Item
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="button" />
                    </td>
                </tr>--%>
                <tr>
                    <td >
                    <table border="0" width="100%" class="box" align="center" cellpadding="0" cellspacing="0">
                    <tr valign="top">
                        <td>
                            <asp:Label ID="lblAddMsg" runat="server" CssClass="errormessage"></asp:Label>
                        </td>
                    </tr>

                    </table>
                    <table bordercolor="#839abf" border="1" width="98%" align="center" cellpadding="0" cellspacing="0">
                    <tr><td>

                    <table border="0" width="100%" class="box" align="center" cellpadding="5" cellspacing="5">
                    <tr valign="top">
                        <td align="left" class="copy10grey" style="width:15%">
                            ESN:&nbsp;<span class="errormessage">*</span>
                        </td>
                        <td align="left" class="copy10grey" style="width:35%">
                            <asp:TextBox ID="txtESN" CssClass="copy10grey" MaxLength="30"  runat="server" Width="80%" AutoComplete="off"></asp:TextBox>
                        </td>
                        <td align="left" class="copy10grey" style="width:15%">
                            Received On:&nbsp;<span class="errormessage">*</span>
                        </td>
                        <td align="left" class="copy10grey" style="width:35%">   
                        <asp:TextBox ID="txtRecievedOn" ContentEditable="false" onkeydown="return ReadOnly(event);" runat="server" CssClass="copy10grey"></asp:TextBox>&nbsp;
                        <img id="imgCal" alt=""  src="/fullfillment/calendar/sscalendar.jpg"/>
                        <cc1:CalendarExtender ID="CalExtDate" runat="server" PopupButtonID="imgCal" 
                        PopupPosition="BottomLeft" Format="MM/dd/yyyy"   TargetControlID="txtRecievedOn">
                        </cc1:CalendarExtender>
                            <%--<asp:TextBox ID="txtRecievedOn" runat="server"  onfocus="set_focus1();" 
                            onkeypress="return doReadonly(event);" 
                            CssClass="copy10grey" MaxLength="15" Width="70%"/>
                            <img id="img2" alt=""  onclick="document.getElementById('<%=txtRecievedOn.ClientID%>').value = ''; displayCalendar(document.getElementById('<%=txtRecievedOn.ClientID%>'),'mm/dd/yyyy',this,true);" src="../../fullfillment/calendar/sscalendar.jpg" />
                            --%>                                       
                        </td>
                    </tr>
                    
                    <%--<tr valign="top">
                        <td align="left" class="copy10grey">
                            UPC:
                        </td>
                        <td align="left" class="copy10grey">
                             <asp:Label ID="lblUpc" runat="server" CssClass="copy10grey"></asp:Label>
                        </td>
                        <td align="left" class="copy10grey">
                           SKU#:
                        </td>
                        <td align="left" class="copy10grey">
                            <asp:Label ID="lblSKU" runat="server" CssClass="copy10grey"></asp:Label>                                       
                        </td>
                    </tr>
                    <tr valign="top">
                        <td align="left" class="copy10grey">
                            AVSO:
                        </td>
                        <td align="left" class="copy10grey">
                             <asp:Label ID="lblAVSO" runat="server" CssClass="copy10grey"></asp:Label>
                        </td>
                        <td align="left" class="copy10grey">
                           Fulfillment Order#:
                        </td>
                        <td align="left" class="copy10grey">
                            <asp:Label ID="lblFulfillment" runat="server" CssClass="copy10grey"></asp:Label>                                       
                        </td>
                    </tr>--%>
                    <tr valign="top">
                        <td align="left" class="copy10grey">
                            Call Time:
                        </td>
                        <td align="left" class="copy10grey">
                             <asp:TextBox ID="txtCallTime" Width="80%" CssClass="copy10grey" MaxLength="3" onkeypress="return isNumberKey(event);"  runat="server"></asp:TextBox>
                        </td>
                        <td align="left" class="copy10grey">
                           Reason:
                        </td>
                        <td align="left" class="copy10grey">
                        <asp:DropDownList ID="ddlReason"  runat="server" Width="80%"  CssClass ="copy10grey"> 
                                <asp:ListItem  Value="0" >------</asp:ListItem>
<asp:ListItem Value="13">ActivationIssues</asp:ListItem>
                                                         <asp:ListItem Value="2">AudioIssues</asp:ListItem>
                                                        <asp:ListItem Value="8">BuyerRemorse</asp:ListItem>
                                                      <asp:ListItem Value="14">CoverageIssues</asp:ListItem>
                                                        <asp:ListItem Value="1" >DOA</asp:ListItem>
                                                        <asp:ListItem  Value="11">DropCalls</asp:ListItem>
                                                        <asp:ListItem  Value="17">HardwareIssues</asp:ListItem>
                                                        <asp:ListItem  Value="10">LiquidDamage</asp:ListItem>
                                                       <asp:ListItem  Value="15">LoanerProgram</asp:ListItem>
                                                        <asp:ListItem  Value="6">MissingParts</asp:ListItem>
                                                        <asp:ListItem  Value="5">Others</asp:ListItem>
                                                        <asp:ListItem  Value="9">PhysicalAbuse</asp:ListItem>
                                                       <asp:ListItem  Value="4">PowerIssues</asp:ListItem>
                                                        <asp:ListItem  Value="7">ReturnToStock</asp:ListItem>
                                                        <asp:ListItem  Value="3">ScreenIssues</asp:ListItem>
                                                        <asp:ListItem  Value="16">ShippingError</asp:ListItem>
                                                        <asp:ListItem  Value="12">Software</asp:ListItem>
                                
                            </asp:DropDownList>    
                                                                 
                        </td>
                    </tr>
                    <tr>
                    <td align="left" class="copy10grey">
                    Warranty:
                    </td>
                    <td align="left" >
                        <asp:DropDownList ID="dpWarranty"  runat="server" Width="80%" onchange="WarrantyAlert(this);"   CssClass ="copy10grey"> 
                            <asp:ListItem  Value="0" >------</asp:ListItem>
                            <asp:ListItem Value="1" >Warranty</asp:ListItem>
                            <asp:ListItem  Value="2">Out of Warranty</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td align="left" class="copy10grey">
                    Warranty expiery Date:
                    </td>
                    <td align="left" class="copy10grey">
                        <asp:TextBox ID="txtWarranty" ContentEditable="false" onkeydown="return ReadOnlyWarranty(event);"  runat="server" CssClass="copy10grey"></asp:TextBox>&nbsp;
                        <img id="img3" alt=""  src="/fullfillment/calendar/sscalendar.jpg"/>
                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="img3" 
                        PopupPosition="BottomLeft" Format="MM/dd/yyyy"   TargetControlID="txtWarranty">
                        </cc1:CalendarExtender>
                    </td>
                    </tr>
                    <tr>
                        <td align="left" class="copy10grey">
                        Status:
                        </td>
                        <td>
                            <asp:Label ID="lblAddStatus" Visible="false" runat="server" Text="Pending"></asp:Label>
                            <asp:DropDownList ID="ddlAddStatus"  runat="server" Class="copy10grey" 
                                Width="80%" >
                            <asp:ListItem  Value="0" >------</asp:ListItem>
                            <asp:ListItem  Value="6">Approved</asp:ListItem>
                                                        <asp:ListItem  Value="12">Back to Stock -NDF</asp:ListItem>
                                                        <asp:ListItem  Value="14">Back to Stock – Replaced by OEM</asp:ListItem>
                                                        <asp:ListItem  Value="13">Back to Stock- Credited</asp:ListItem>
                                                        <asp:ListItem Value="30" >Cancel</asp:ListItem>
                                                        <asp:ListItem  Value="10">Closed</asp:ListItem>
                                                        <asp:ListItem  Value="8">Credited</asp:ListItem>
                                                        <asp:ListItem Value="26" >Damaged</asp:ListItem>
                                                        <asp:ListItem  Value="9">Denied</asp:ListItem>
                                                        <asp:ListItem Value="31" >External ESN</asp:ListItem>
                                                        <asp:ListItem Value="25" >Incomplete</asp:ListItem>
                                                        <asp:ListItem  Value="19">NDF (No Defect Found)</asp:ListItem>
                                                        <asp:ListItem  Value="11" >Out with OEM for repair</asp:ListItem>
                                                        <asp:ListItem Value="1" >Pending</asp:ListItem>
                                                        <asp:ListItem  Value="4">Pending for Credit</asp:ListItem>
                                                        <asp:ListItem  Value="3">Pending for Repair</asp:ListItem>
                                                        <asp:ListItem  Value="5">Pending for Replacement</asp:ListItem>
                                                        <asp:ListItem Value="32" >Pending ship to OEM</asp:ListItem>
                                                        <asp:ListItem Value="34" >Pending ship to Supplier</asp:ListItem>
                                                        <asp:ListItem  Value="20">PRE-OWNED – A stock</asp:ListItem>
                                                        <asp:ListItem Value="21" >PRE-OWEND - B Stock</asp:ListItem>
                                                        <asp:ListItem Value="22" >PRE-OWEND – C Stock</asp:ListItem>
                                                        <asp:ListItem Value="27" >Preowned</asp:ListItem>
                                                        <asp:ListItem  Value="2">Received</asp:ListItem>
                                                        <asp:ListItem Value="23" >Rejected</asp:ListItem>
                                                        <asp:ListItem  Value="18">Repaired By AV</asp:ListItem>
                                                        <asp:ListItem  Value="15">Repaired by OEM</asp:ListItem>
                                                        <asp:ListItem  Value="17">Replaced BY AV</asp:ListItem>
                                                        <asp:ListItem  Value="16">Replaced BY OEM</asp:ListItem>
<asp:ListItem  Value="38">Replaced by OEM- New</asp:ListItem>
                                                        <asp:ListItem  Value="39">Replaced by OEM- Preowned</asp:ListItem>
                                                        <asp:ListItem Value="28" >Return to OEM</asp:ListItem>
                                                        <asp:ListItem  Value="7">Returned</asp:ListItem>
                                                        <asp:ListItem Value="36" >Returned from OEM</asp:ListItem>
                                                        <asp:ListItem Value="29" >Returned to Stock</asp:ListItem>
                                                        <asp:ListItem Value="24" >RTS (Return To Stock)</asp:ListItem>
                                                        <asp:ListItem Value="33" >Sent to OEM</asp:ListItem>
                                                        <asp:ListItem Value="35" >Sent to Supplier</asp:ListItem>
<asp:ListItem Value="37" >ShippingError</asp:ListItem>
                                                        
                            </asp:DropDownList>  
                        </td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr valign="top">
                        <td align="left" class="copy10grey">
                            Notes:
                        </td>
                        <td align="left" class="copy10grey" colspan="3">
                             <asp:TextBox ID="txtNotes" TextMode="MultiLine" Rows="8" CssClass="copy10grey"  Width="92%"  runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    </table>
                    
                    </td></tr>
                    </table>
                    <br />
                    <table width="100%" align="center">
                    <tr>
                        <td align="center">
                             <asp:Button ID="btnAdd" runat="server" Text="Submit" OnClientClick="return IsValid();" OnClick="btnAddLineItem_Click" CssClass="button" />
                             &nbsp;
                             <asp:Button ID="btnAddCancel" runat="server" Text="Cancel" CssClass="button" OnClientClick="return closeAddDialog();" />
                        </td>
                    </tr>
                    </table>
                    </td>
                </tr>
                </table>
            
               <%-- </div>
            --%>

            </td>
        </tr>
        </table>
       </asp:Panel>
                        </asp:PlaceHolder>
						
						
					</ContentTemplate>
							
				</asp:UpdatePanel>
			
			</div>
            <div id="divHistory" style="display:none">
					
				<asp:UpdatePanel ID="upnlHistory" runat="server">
					<ContentTemplate>
						<asp:PlaceHolder ID="phrHistory" runat="server">
                        <RMA:RmaHistory ID="rmaHistory1" runat="server" />
		                     <%--<table width="100%">
                                                <tr>
                                                    <td>
                                                        <div style="overflow:auto; height:450px; width:100%; border: 0px solid #839abf" >
      
                                                        
                                                        </div>
                                                    

                                                    </td>
                                                </tr>
                                                </table>
--%>

                        </asp:PlaceHolder>
						
						
					</ContentTemplate>
							
				</asp:UpdatePanel>
			
			</div>


            
            <div id="divRMADoc" style="display:none">
					
				<asp:UpdatePanel ID="upnRmaDoc" runat="server">
					<ContentTemplate>
						<asp:PlaceHolder ID="phrRmaDoc" runat="server">
                        <table  cellSpacing="1" cellPadding="1" width="100%" align="center">
                                <tr>
			                        <td colSpan="6" bgcolor="#dee7f6" class="button" align="left">&nbsp;&nbsp;RMA document
			                        </td>
                                </tr>
                                </table>
                            
                            <table bordercolor="#839abf" border="1" width="98%" align="center" cellpadding="0" cellspacing="0">
                                <tr><td>

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
			                        <td colSpan="6" bgcolor="#dee7f6" class="button" align="left">&nbsp;&nbsp;Administration RMA document
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
                                        <td colspan="2" align="center">
                                            <hr />
                                        </td>
                                    </tr--%>
                                    <tr>
                                        <td colspan="2" align="center">
                                        
                                            <asp:Button ID="btnRmaDocUpload" runat="server" Text="Upload" CssClass="buybt" OnClick="btnRmaDocUpload_Clicck"  
                                            OnClientClick="return ValidateFile();"  />
                                            <asp:Button ID="btnUloadCancel" runat="server" Text="Cancel" CssClass="buybt" OnClientClick="return closeRmaDocDialog();" />
                                        </td>
                                    </tr>
                                    
                                    </table>

                        </asp:PlaceHolder>
						
					
					</ContentTemplate>
					<Triggers>
                        <asp:PostBackTrigger ControlID="btnRmaDocUpload" />
                    </Triggers>
				</asp:UpdatePanel>
			
			</div>            

            
            </div>
        <%-- <div id="winVP" style="z-index:1">--%>
         <asp:UpdatePanel ID="upPnlRMA"  runat="server" UpdateMode="Conditional">
            <ContentTemplate>
     <asp:LinkButton ID="btnRefreshGrid" CausesValidation="false" OnClick="btnRefreshGrid_Click" style="display:none" runat="server"></asp:LinkButton>		
		
            <table cellspacing="0" cellpadding="0" border="0"  width="98%" align="center">       
            <tr><td>&nbsp;</td></tr>                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                 
             <tr>
                  <td>
                
                <asp:HiddenField ID="hdnValidateESNs" runat="server" />
                
                
                <asp:HiddenField ID="hdncompanyname" runat="server" />
                
                <table id="rmaform" style="text-align:left; width:100%;"  align="center" class="copy10grey">
                    
                    <tr>
                        <td class="buttonlabel" align="left">Return Merchandise Authorization(RMA) Form</td>
                    </tr>
                    <tr>
                   <td>
                   <table bordercolor="#839abf" border="1" cellspacing="0" cellpadding="0" width="100%">
                            <tr bordercolor="#839abf">
                                <td>
                       <table  cellSpacing="0" cellPadding="0" width="100%">
                            <tr>
                            <td class="copy10grey" width="60%">

                                &nbsp;- Please enter your correct SHIP TO information in the space provided. ALL units will be returned to the default address on the account if this is incomplete or missing.<br />
                                &nbsp;- For all customers, please take note of the returns checklist available for download via <a href="#" target="_blank">LAN Global RMA Checklist</a>.<br />
                                &nbsp;- For Sprint dealers, please take note of the returns policies available for download via <a href="#" target="_blank">Sprint Dealers Returns Policies</a>.

                                <br />
&nbsp;- Email should not have &quot;LANGlobal.com&quot; email address.
<br />
                                &nbsp;- Upto 10 ESNs allowed per return (RMA).

                                <br />
                                &nbsp;- <span class="copy10underline" > Please note that our new online RMA process allows for ten ESNs for each entry. You may file another request separately.</span>
<br />
                                &nbsp;- Comments fields has maximum length of 5000 characters.
</td>
                            <td bgcolor="#839abf" width="1">&nbsp;</td>
                            <td  class="copy10grey"  width="40%">
                                    &nbsp;Please send ALL returns to: <br />
                                     &nbsp;<b>LAN Global</b><br />
                                     &nbsp;Attention: RMA Department AVRMA#<br />
                                     &nbsp;12031 Sherman Road North hollywood,<br />
                                     &nbsp;CA 91605<br />
                            </td>
                            </tr>
                        </table>
                         </td>
                            </tr>
                        </table>    
                   </td>
                   </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblConfirm" runat="server" CssClass="errormessage"></asp:Label>
                                   <asp:Label ID="lbl_msg" runat="server" CssClass="errormessage"></asp:Label> 
                        </td>
                    </tr>
                    <tr>
                        <td>
                          <table bordercolor="#839abf" border="1" cellspacing="0" cellpadding="0" width="100%">
                            <tr bordercolor="#839abf">
                                <td>
                                <table class="box" border="0"  align="center" width="100%">
                                    <tr>
                                        <td class="style1" align="left" Class="copy10grey">
                                             <asp:Label ID="lblCompany" runat="server" Text="Customer:"></asp:Label></td>
                                        <td class="style2">
                                
                                            <asp:HiddenField ID="hdnUserID" runat="server" />
                                            <asp:DropDownList ID="ddlCompany"  AutoPostBack="true" CssClass="copy10grey" 
                                                 runat="server" 
                                                onselectedindexchanged="ddlCompany_SelectedIndexChanged">
                                            </asp:DropDownList>
                              
                                        </td>
                                    </tr>                                     
                                     <tr >
                                        <td   class="style1"  align="left"><asp:Label ID="lblrmanumber" runat="server" Text="RMA#:" CssClass="copy10grey"></asp:Label></td>
                                                                                        <td class="style2"  >
                                                <asp:Label ID="txtRmaNum" runat="server" 
                                                    CssClass="copyblue11b" Width="80%" ReadOnly="True" 
                                                     />
                                                     <asp:HiddenField ID="hdnRmaItemcount" Value="0" runat="server" />
                                        </td>
                                        <td class="copy10grey" ><asp:Label ID="lblRMADate" runat="server" Text="RMA Date:"  CssClass="copy10grey"></asp:Label>
                                            <span Class="errormessage">*</span></td>
                                        <td class="style3">
                                            <asp:Panel ID="rmadtpanel" runat="server">
                                            &nbsp;<asp:TextBox ID="txtRMADate" runat="server"  onfocus="set_focus1();" onkeypress="return doReadonly(event);" 
                                                CssClass="copy10grey" MaxLength="15" />
                                            <img id="img1" alt=""  onclick="document.getElementById('<%=txtRMADate.ClientID%>').value = ''; displayCalendar(document.getElementById('<%=txtRMADate.ClientID%>'),'mm/dd/yyyy',this,true);" src="../../fullfillment/calendar/sscalendar.jpg" />
                                            </asp:Panel>
                                        </td> 
                                        <td class="copy10grey" width="10%"><span id="status" runat="server">Status:</span></td>
                                        <td Class="copy10grey" width="20%">
                                        <table width="100%" cellpadding="0" cellspacing="0">
                                        <tr valign="top">
                                            <td>
                                             &nbsp;<asp:Label ID="lblStatus" runat="server" Text="Pending"></asp:Label>
                                            <asp:DropDownList ID="ddlStatus"  runat="server" Class="copy10grey" 
                                                Width="166px" >
                                                        <asp:ListItem  Value="0" >------</asp:ListItem>
                                                        <asp:ListItem  Value="6">Approved</asp:ListItem>
                                                        <asp:ListItem  Value="12">Back to Stock -NDF</asp:ListItem>
                                                        <asp:ListItem  Value="14">Back to Stock – Replaced by OEM</asp:ListItem>
                                                        <asp:ListItem  Value="13">Back to Stock- Credited</asp:ListItem>
                                                        <asp:ListItem Value="30" >Cancel</asp:ListItem>
                                                        <asp:ListItem  Value="10">Closed</asp:ListItem>
                                                        <asp:ListItem  Value="8">Credited</asp:ListItem>
                                                        <asp:ListItem Value="26" >Damaged</asp:ListItem>
                                                        <asp:ListItem  Value="9">Denied</asp:ListItem>
                                                        <asp:ListItem Value="31" >External ESN</asp:ListItem>
                                                        <asp:ListItem Value="25" >Incomplete</asp:ListItem>
                                                        <asp:ListItem  Value="19">NDF (No Defect Found)</asp:ListItem>
                                                        <asp:ListItem  Value="11" >Out with OEM for repair</asp:ListItem>
                                                        <asp:ListItem Value="1" >Pending</asp:ListItem>
                                                        <asp:ListItem  Value="4">Pending for Credit</asp:ListItem>
                                                        <asp:ListItem  Value="3">Pending for Repair</asp:ListItem>
                                                        <asp:ListItem  Value="5">Pending for Replacement</asp:ListItem>
                                                        <asp:ListItem Value="32" >Pending ship to OEM</asp:ListItem>
                                                        <asp:ListItem Value="34" >Pending ship to Supplier</asp:ListItem>
                                                        <asp:ListItem  Value="20">PRE-OWNED – A stock</asp:ListItem>
                                                        <asp:ListItem Value="21" >PRE-OWEND - B Stock</asp:ListItem>
                                                        <asp:ListItem Value="22" >PRE-OWEND – C Stock</asp:ListItem>
                                                        <asp:ListItem Value="27" >Preowned</asp:ListItem>
                                                        <asp:ListItem  Value="2">Received</asp:ListItem>
                                                        <asp:ListItem Value="23" >Rejected</asp:ListItem>
                                                        <asp:ListItem  Value="18">Repaired By AV</asp:ListItem>
                                                        <asp:ListItem  Value="15">Repaired by OEM</asp:ListItem>
                                                        <asp:ListItem  Value="17">Replaced BY AV</asp:ListItem>
                                                        <asp:ListItem  Value="16">Replaced BY OEM</asp:ListItem>
<asp:ListItem  Value="38">Replaced by OEM- New</asp:ListItem>
                                                        <asp:ListItem  Value="39">Replaced by OEM- Preowned</asp:ListItem>
                                                        <asp:ListItem Value="28" >Return to OEM</asp:ListItem>
                                                        <asp:ListItem  Value="7">Returned</asp:ListItem>
                                                        <asp:ListItem Value="36" >Returned from OEM</asp:ListItem>
                                                        <asp:ListItem Value="29" >Returned to Stock</asp:ListItem>
                                                        <asp:ListItem Value="24" >RTS (Return To Stock)</asp:ListItem>
                                                        <asp:ListItem Value="33" >Sent to OEM</asp:ListItem>
                                                        <asp:ListItem Value="35" >Sent to Supplier</asp:ListItem>
                                                        
                                                        <asp:ListItem Value="37" >ShippingError</asp:ListItem>
                                            </asp:DropDownList>
                                            <br />
                                            <asp:CheckBox ID="chkApplyAll" runat="server" Text="Apply to all ESNs" />
                                            </td>
                                            <td>
                                            





                                        &nbsp;&nbsp;                                            &nbsp;
                                        <asp:Button ID="btnHistory" runat="server" CssClass="button" Text="RMA History" OnClick="btnHistory_Click" OnClientClick="openHistoryDialogAndBlock('RMA History', 'btnHistory')" CausesValidation="false"/>

                                        <%--<asp:ImageButton ID="imgRma"  ToolTip="View RMA History" OnClick="imgViewRMA_Click"  CausesValidation="false"  ImageUrl="~/Images/view.png"  runat="server" />--%>
                                         
     





                                             
                                            </td>
                                        </tr>
                                        </table>
                                           

      
                                            
                                        </td>
                                                                      
                                    </tr> 
                                    <tr>
                                        <td colspan="6"><hr size="1" align="center" style="width: 100%" /></td>
                                    </tr>
                                    <tr>
                                        <td   class="style1"  align="left">Customer Name:&nbsp;<span Class="errormessage">*</span></td>
                                        <td colspan="5">
                                            <asp:TextBox ID="txtCustName" runat="server" 
                                                CssClass="copy10grey" MaxLength="50" Width="50%" />
                                        </td>                            
                                    </tr>
                         <tr>
                            <td   class="style1"  align="left">Address:&nbsp;<span Class="errormessage">*</span></td>
                            <td colspan="5">
                                <asp:TextBox ID="txtAddress" runat="server" CssClass="copy10grey" Width="90%" MaxLength="200"/>
                            </td>                            
                        </tr>
                        <tr>
                            <td   class="style1"  align="left">City:
                                        &nbsp;<span Class="errormessage">*</span>
                            </td>
                            <td class="style2">
                                <asp:TextBox ID="txtCity" runat="server"  Width="80%"  CssClass="copy10grey" MaxLength="50"/>
                            </td>                            
                            <td class="copy10grey"  align="left"  width="10%">State:&nbsp;<span Class="errormessage">*</span></td>
                            <td class="style3" width="20%">
                                <asp:dropdownlist id="dpState"  runat="server"  Width="91%" cssclass="copy10grey">
                                </asp:dropdownlist>
                               <%-- <asp:TextBox ID="txtState" runat="server"  Width="90%"  CssClass="copy10grey" 
                                    MaxLength="30" />--%>
                             </td>
                             <td class="copy10grey"  align="left" >Zip:&nbsp;<span class="errormessage">*</span></td>    
                              <td  width="20%">  <asp:TextBox ID="txtZip" runat="server" Width="37%"  
                                      CssClass="copy10grey" MaxLength="6"/>
                            </td>  
                        </tr>
              
                                    <tr>
                            <td   class="style1"  align="left">Email:&nbsp;<span Class="errormessage">*</span>
                            </td>
                            <td class="style2">
                                <asp:TextBox ID="txtEmail" Width="90%" runat="server" CssClass="copy10grey" MaxLength="50"/>
                            </td>                            
                            <td class="copy10grey"  align="left">Phone:&nbsp;<span Class="errormessage">*</span></td>
                            <td class="style3">
                                <asp:TextBox ID="txtPhone"  Width="90%" runat="server" CssClass="copy10grey" 
                                    MaxLength="12" />
                             </td>
                             <td  class="copy10grey"  align="left">
                             
                             </td> 
                             <td  class="copy10grey"  align="left">
                                <asp:FileUpload ID="fuRMADoc" runat="server"  Visible="false"  />
                             </td>                           
                        </tr>
<tr>
                            <td class="copy10grey"  align="left">
                            Document:  
                            </td>
                                <td colspan="5">
                                <asp:ImageButton ID="imgUpload" runat="server" ToolTip="Upload RMA Document" AlternateText="Upload RMA Document" OnClick="btnRmaDoc_Click" ImageUrl="~/images/upload.png"
                                             CausesValidation="false" />
                                    <asp:Label ID="lblRmaDocs" runat="server" CssClass="copy10grey" ></asp:Label>
                                
                                </td>
                            </tr>
                        
                            <tr>
                                <td colspan="6"><hr size="1" align="center" style="width: 100%" />
                                <br />
                                </td>
                            </tr>
                                    <tr valign="top">
                            <td   class="style1" align="left">Comments:</td>
                            <td colspan="2">
                                <asp:TextBox runat="server" ID="txtRemarks" TextMode="MultiLine"
                                    Rows="3" Columns="80" CssClass="copy10grey" 
                                    
                                     Width="90%" />
                                     &nbsp;<asp:ImageButton ID="imgComments"  ImageAlign="Top" runat="server" AlternateText="RMA Customer Comments" ToolTip="RMA Customer Comments" OnClick="btnCustomerComments_Click" ImageUrl="~/Images/ccomments.png"
                                        OnClientClick="openCommentsDialogAndBlock('RMA Customer Comments', 'imgComments')" CausesValidation="false"/>
                            
                            </td>
                            <div runat="server" id="divAvc">
                                <td   class="style4" align="right">LAN Global Comments:</td>
                                <td colspan="2">
                                    <asp:TextBox runat="server" ID="txtAVComments" TextMode="MultiLine"
                                        Rows="3" Columns="80" CssClass="copy10grey" 
                                        
                                         Width="90%" />
                                         &nbsp;<asp:ImageButton ID="imgAvComments" runat="server" ImageAlign="Top" AlternateText="RMA AV Comments" ToolTip="RMA LAN Global Comments" OnClick="btnAVComments_Click" ImageUrl="~/Images/comments.png"
                                        OnClientClick="openCommentsDialogAndBlock('RMA LAN Global Comments', 'imgAvComments')" CausesValidation="false"/>
                            
                                </td>
                            </div>
                        </tr> 
                                </table>      
                                </td>
                            </tr>
                        </table>      
                        </td>          
                    </tr>
                    <tr><td>&nbsp;</td></tr>
                    <tr  >
                       <td align="left" >
                        <table width="100%" cellpadding="0" cellspacing="0">
                        <tr  class="buttonlabel">

                            <td align="left" class="buttonlabel" style="width:87%" >
                            Return Merchandise Authorization(RMA) Line Items
                            </td>
                            <td align="right" class="buttonlabel">
                            <asp:Button ID="btnNewRma" Height="100%" runat="server" CssClass="buttongray" OnClick="btnNewRma_Click"  Text="Add New RMA line Item" OnClientClick="openAddDialogAndBlock('Add New RMA line Item', 'btnNewRma')"/>
                            </td>

                        </tr>
                        </table>
                        
                        
                        </td>
                        </tr>
                        <tr>
                       <td align="left" >       
                        
                            
                        </td>
                    </tr>


                <tr>
                <td>
                    <asp:Repeater ID="rptRmaItem" runat="server" OnItemDataBound="rptRmaItem_ItemDataBound" >
                    <HeaderTemplate>
                    <table width="100%">
                        <tr>
                            <td align="left" class="buttonlabel" style="width:13%">
                            ESN
                            </td>
                           <%-- <td align="left" class="button" style="width:12%">
                            UPC
                            </td> --%>
                            <td align="left" class="buttonlabel" style="width:12%">
                            SKU#
                            </td>
<td align="left" class="buttonlabel" style="width:10%">
                            NEW SKU#
                            </td>
                           <%-- <td align="left" class="button" style="width:10%">
                            AVSO
                            </td>--%>
                            <td align="left" class="buttonlabel" style="width:13%">
                            Fulfillment#
                            </td>
                            <td align="left" class="buttonlabel" style="width:10%">
                            Call Time
                            </td>
                            <td align="left" class="buttonlabel" style="width:25%">
                            Notes
                            </td>
                            <td align="left" class="buttonlabel" style="width:5%">
                            Edit
                            </td>
                        </tr>
                    
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td align="left" class="copy10grey">
                            <%# Eval("ESN") %>
                            </td>
                            <%--<td align="left" class="copy10grey">
                            <%# Eval("UPC") %>
                            </td>--%>
                            <td align="left" class="copy10grey">
                            <%# Eval("ItemCode") %>
                            </td>
<td align="left" class="copy10grey">
                            <%# Eval("NewSKU")%>
                            </td>
                            <%--<td align="left" class="copy10grey">
                            <%# Eval("AVSalesOrderNumber")%>
                             
                            </td>--%>
                            <td align="left" class="copy10grey">
                            <%# Eval("PurchaseOrderNumber")%>
                            
                            </td>
                            <td align="left" class="copy10grey">

                            <%# Convert.ToInt32(Eval("CallTime")) == 0 ? "" : Eval("CallTime")%>
                             
                            
                            </td>
                            <td align="left" class="copy10grey">
                            <%# Eval("Notes")%>
                            
                            </td>
                            <td>
                            <asp:ImageButton ToolTip="Edit" Visible='<%# Convert.ToString(Eval("esn")) =="" ? false : true %>'  CommandArgument='<%# Eval("rmaDetGUID") %>' 
                            ImageUrl="~/Images/edit.png" ID="imgEdit" OnCommand="imgEditRMA_Commnad" runat="server" />
                                <%--<asp:Button ID="btnEdit" runat="server" Text="Edit"  OnClick="btnEdit_Click" CssClass="button" />--%>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                    </table>
                    </FooterTemplate>
                    </asp:Repeater>
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

         </ContentTemplate>
        </asp:UpdatePanel>

                                             
        <table cellspacing="0" cellpadding="0" border="0"  width="100%" align="center">

            <tr><td>
            
            <table cellspacing="0" cellpadding="0" border="0"  width="98%" align="center">       

                    <tr><td><hr /></td></tr>
                    <tr>
                        <td align="center">
                            <asp:Panel ID="btnpanel" runat="server">
                            <%--<asp:Button ID="btnValidate" CssClass="buybt" runat="server" Text="Validate ESNs" 
                                    OnClientClick="return IsValidate(0);" OnClick="btnValidate_click" />&nbsp; --%>  
                                <asp:Button ID="btnSubmitRMA"  CssClass="buybt" runat="server" Text="Submit RMA"  
                                    OnClientClick="return IsValidate(1);" OnClick="btnSubmitRMA_click" />&nbsp;       
                                <asp:Button ID="btn_Cancel" CssClass="buybt" runat="server" Text="Cancel" 
                                    CausesValidation="false" OnClick="btnCancelRMA_click" /> 
                                <asp:Button ID="btnBack" CssClass="buybt" runat="server" Text="Back to search" 
                                    CausesValidation="false" OnClick="btnBackRMAQuery_click" />                
                                </asp:Panel>
                        </td>
                    </tr>        
                </table> 
            
          

            </td></tr>
            <tr><td>&nbsp;
             <%--<asp:UpdateProgress ID="UpdateProgress1" runat="server"
                     DynamicLayout="false">
            <ProgressTemplate>
                <img src="/Images/ajax-loaders.gif" /> Loading ...
            </ProgressTemplate>
        </asp:UpdateProgress>
            --%>
            
            </td></tr>
            </table>
            
            
        <asp:UpdatePanel ID="upnlJsRunner" UpdateMode="Always" runat="server">
			<ContentTemplate>
				<asp:PlaceHolder ID="phrJsRunner" runat="server"></asp:PlaceHolder>
			</ContentTemplate>
		</asp:UpdatePanel>
            <table width="100%">
            <tr>
                <td><foot:MenuFooter id="Footer" runat="server"></foot:MenuFooter></td>
            </tr>
            </table>
        <%--</div>--%>
       <%-- <script type="text/javascript">
            var behaviour = document.getElementById("<%=ModalPopupExtender1.ClientID%>");  //$find("ModalPopupExtender1");
            alert(behaviour);
if (behaviour) 
behaviour.show(); 

alert(behaviour._backgroundElement.style.zIndex); 
alert(behaviour._foregroundElement.style.zIndex); 
behaviour._backgroundElement.style.zIndex = 2; 
behaviour._foregroundElement.style.zIndex = 3; 

behaviour._dropShadowBehavior._shadowDiv.style.zIndex = 1;
        </script>--%>
        <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.4.0/jquery.min.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/jquery-ui.min.js"></script>
        <script type="text/javascript" src="../JQuery/jquery.blockUI.js"></script>
    </form>
</body>
</html>
