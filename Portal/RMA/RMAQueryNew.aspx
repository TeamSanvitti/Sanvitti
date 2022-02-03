<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="RMAQueryNew.aspx.cs" Inherits="avii.Admin.RMA.RMAQueryNew" ValidateRequest="false"%>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="/Controls/Header.ascx" %>
<%@ Register TagPrefix="UC" TagName="Comments" Src="~/Controls/RMACommunication.ascx" %>


<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
method="post" enctype="multipart/form-data" 
--%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Return Merchandise Authorization (RMA) - Search</title>
   
    <%--<link href="/Styles.css" type="text/css" rel="stylesheet" />
--%><%--
    <script language="javaScript" src="../mm_menu.js" type="text/javascript"></script>

    <link type="text/css" href="/UIThemes/themes/base/ui.all.css" rel="stylesheet" />--%>
    
    <%--<link rel="stylesheet" type="text/css" href="/fullfillment/calendar/dhtmlgoodies_calendar.css" media="screen"></link>--%>
   <%-- <script type="text/javascript" src="/fullfillment/calendar/dhtmlgoodies_calendar.js"></script>--%>

    <!-- from http://encosia.com/2009/10/11/do-you-know-about-this-undocumented-google-cdn-feature/ -->
     <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>

    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.4.0/jquery.min.js"></script>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/jquery-ui.min.js"></script>
    <link href="https://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/themes/start/jquery-ui.css"
    rel="stylesheet" type="text/css" />
    
	<script type="text/javascript" src="/JSLibrary/jquery.blockUI.js"></script>
    <%--<script type="text/javascript" src="../JQuery/jquery.blockUI.js"></script>--%>
	
    <link rel="stylesheet"  type="text/css" href="../fullfillment/calendar/dhtmlgoodies_calendar.css" media="screen" />
	<script type="text/javascript" src="../fullfillment/calendar/dhtmlgoodies_calendar.js"></script>  
    
<!-- fix for 1.1em default font size in Jquery UI -->
	<style type="text/css">
		.ui-widget{font-size:11px !important;}
		.ui-state-error-text{margin-left: 10px}
	</style>
     <div id="Div2" runat="server"> 
	<script type="text/javascript">
        function OpenNewPage(url) {
            window.open(url);
        }
        function OpenPDF(base64data) {

            // window.open("data:application/octet-stream;charset=utf-16le;base64,"+data64);
            // window.open("data:application/pdf;base64, " + base64data);
            // window.open("data:application/pdf;base64," + encodeURI(base64data));
            var pdfWindow = window.open('', 'Print Label');
            pdfWindow.document.write("<iframe width='100%' height='100%' src='data:application/pdf;base64, " + encodeURI(base64data) + "'></iframe>")

        }
        function PrintDiv() {
            var divContents = document.getElementById("divLabelImg").innerHTML;
            var printWindow = window.open('', '', 'height=650,width=1024');
            printWindow.document.write('<html><head><title></title>');
            printWindow.document.write('</head><body >');
            printWindow.document.write(divContents);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            printWindow.print();
        }


	    $(document).ready(function () {


	        $("#divComments").dialog({
	            autoOpen: false,
	            modal: false,
	            minHeight: 20,
	            height: 400,
	            width: 850,
	            resizable: false,

	            open: function (event, ui) {
	                $(this).parent().appendTo("#divContainer");
	            }
	        });


	        $("#divTriage").dialog({
	            autoOpen: false,
	            modal: false,
	            minHeight: 20,
	            height: 300,
	            width: 850,
	            resizable: false,

	            open: function (event, ui) {
	                $(this).parent().appendTo("#divContainer");
	            }
	        });


	        $("#divRMAESN").dialog({
	            autoOpen: false,
	            modal: true,
	            minHeight: 20,
	            height: 650,
	            width: 950,
	            resizable: false,
	            open: function (event, ui) {
	                $(this).parent().appendTo("#divContainer");
	            }
	        });

	        $("#divDoc").dialog({
	            autoOpen: false,
	            modal: false,
	            minHeight: 20,
	            height: 400,
	            width: 550,
	            resizable: false,

	            open: function (event, ui) {
	                $(this).parent().appendTo("#divContainer");
	            }
	        });
	        $("#divPicture").dialog({
	            autoOpen: false,
	            modal: false,
	            minHeight: 20,
	            height: 350,
	            width: 600,
	            resizable: false,

	            open: function (event, ui) {
	                $(this).parent().appendTo("#divContainer");
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
                    $(this).parent().appendTo("#divContainer");
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
                    $(this).parent().appendTo("#divContainer");
                }
            });

            $("#divHistory").dialog({
                autoOpen: false,
                modal: true,
                minHeight: 20,
                height: 450,
                width: 800,
                resizable: false,
                open: function (event, ui) {
                    $(this).parent().appendTo("#divContainer");
                },
            });

        });




        function closeHistoryDialog() {
            //Could cause an infinite loop because of "on close handling"
            $("#divHistory").dialog('close');
        }

        function openHistoryDialog(title, linkID) {

            var pos = $("#" + linkID).position();
            var top = pos.top;
            var left = pos.left + $("#" + linkID).width() + 10;
            //alert(left);
            //top = top - 300;
            if (top > 600)
                top = 10;
            else
                top = 10;

            left = 200;
            //$("#divHistory").html("");
            //$('#divHistory').empty();
            //alert($('#phrHistory'));
            $("#divHistory").dialog("option", "title", title);
            $("#divHistory").dialog("option", "position", [left, top]);

            $("#divHistory").dialog('open');
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

        function unblockHistoryDialog() {
            $("#divHistory").unblock();
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


        function closeShipDialog() {
            //Could cause an infinite loop because of "on close handling"
            $("#divShipItems").dialog('close');
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
        function unblockShipItemsDialog() {
            $("#divShipItems").unblock();
        }


	    function closedPictureDialog() {
	        //Could cause an infinite loop because of "on close handling"
	        $("#divPicture").dialog('close');

	        return false;
	    }

	    function openPictureDialog(title, linkID) {

	        var pos = $("#" + linkID).position();
	        var top = pos.top;
	        var left = pos.left + $("#" + linkID).width() + 10;
	        //alert(top);
	        if (top > 600)
	            top = 20;
	        else
	            top = 20;
	        //top = top - 600;
	        left = 350;
	        $("#divPicture").dialog("option", "title", title);
	        $("#divPicture").dialog("option", "position", [left, top]);

	        $("#divPicture").dialog('open');

	        //unblockPictureDialog();
	    }
	    function openPictureDialogAndBlock(title, linkID) {
	        openPictureDialog(title, linkID);

	        //block it to clean out the data
	        $("#divPicture").block({
	            message: '<img src="../images/async.gif" />',
	            css: { border: '0px' },
	            fadeIn: 0,
	            //fadeOut: 0,
	            overlayCSS: { backgroundColor: '#ffffff', opacity: 1 }
	        });
	    }
	    function unblockPictureDialog() {
	        $("#divPicture").unblock();
	    }


	    function closeDocDialog() {
	        //Could cause an infinite loop because of "on close handling"
	        $("#divDoc").dialog('close');
	    }

	    function openDocDialog(title, linkID) {

	        var pos = $("#" + linkID).position();
	        var top = 0;

	        top = pos.top; var left = pos.left + $("#" + linkID).width() + 10;
	        //alert(left);
	        //top = top - 400;
	        if (top > 400)
	            top = 10;
	        left = 275;
	        $("#divDoc").dialog("option", "title", title);
	        $("#divDoc").dialog("option", "position", [left, top]);

	        $("#divDoc").dialog('open');
	    }
	    function openDocDialogAndBlock(title, linkID) {
	        openDocDialog(title, linkID);

	        //block it to clean out the data
	        $("#divDoc").block({
	            message: '<img src="../images/async.gif" />',
	            css: { border: '0px' },
	            fadeIn: 0,
	            //fadeOut: 0,
	            overlayCSS: { backgroundColor: '#ffffff', opacity: 1 }
	        });
	    }
	    function unblockDocDialog() {
	        $("#divDoc").unblock();
	    }


function closeDocDialog() {
    //Could cause an infinite loop because of "on close handling"
    $("#divDoc").dialog('close');
}

function openESNDialog(title, linkID) {

    var pos = $("#" + linkID).position();
    var top = 0;

    top = pos.top; var left = pos.left + $("#" + linkID).width() + 10;
    //alert(left);
    //top = top - 400;
    if (top > 400)
        top = 10;
     else
	    top = 10;
    left = 275;
    $("#divRMAESN").dialog("option", "title", title);
    $("#divRMAESN").dialog("option", "position", [left, top]);

    $("#divRMAESN").dialog('open');
}
function openESNDialogAndBlock(title, linkID) {
    openESNDialog(title, linkID);

    //block it to clean out the data
    $("#divRMAESN").block({
        message: '<img src="../images/async.gif" />',
        css: { border: '0px' },
        fadeIn: 0,
        //fadeOut: 0,
        overlayCSS: { backgroundColor: '#ffffff', opacity: 1 }
    });
}
function unblockESNDialog() {
    $("#divRMAESN").unblock();
}
        
	    function closedTriageDialog() {
	        //Could cause an infinite loop because of "on close handling"
	        $("#divTriage").dialog('close');

	        return false;
	    }

	    function openTriageDialog(title, linkID) {

	        var pos = $("#" + linkID).position();
	        var top = pos.top;
	        var left = pos.left + $("#" + linkID).width() + 10;
	        //alert(top);
	        if (top > 600)
	            top = 20;
	        else
	            top = 20;
	        //top = top - 600;
	        left = 300;
	        $("#divTriage").dialog("option", "title", title);
	        $("#divTriage").dialog("option", "position", [left, top]);

	        $("#divTriage").dialog('open');

	        //unblockTriageDialog();
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

        </script>

    

    <%--<script type="text/javascript" src="/JSLibrary/jquery-1.3.2.min.js"></script>

    <script type="text/javascript" src="/JSLibrary/ui/ui.core.js"></script>

    <script type="text/javascript" src="/JSLibrary/ui/ui.tabs.js"></script>--%>
    <script type="text/javascript" language="javascript">

        function AddNewRow() {

            var rownum = 1;

            var div = document.createElement("div")

            var divid = "dv" + rownum

            div.setAttribute("ID", divid)

            rownum++

            var lbl = document.createElement("label")

            lbl.setAttribute("ID", "lbl" + rownum)

            lbl.setAttribute("class", "label1")

            lbl.innerHTML = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; Upload file: "

            rownum++

            var _upload = document.createElement("input")

            _upload.setAttribute("type", "file")

            _upload.setAttribute("ID", "upload" + rownum)

            _upload.setAttribute("runat", "server")

            _upload.setAttribute("name", "uploads" + rownum)

            rownum++

            var hyp = document.createElement("a")

            hyp.setAttribute("style", "cursor:Pointer")

            hyp.setAttribute("onclick", "return RemoveDv('" + divid + "');");

            hyp.innerHTML = "Remove"

            rownum++

            var br = document.createElement("br")

            var _pdiv = document.getElementById("Parent")

            div.appendChild(br)

            div.appendChild(lbl)

            div.appendChild(_upload)

            div.appendChild(hyp)

            _pdiv.appendChild(div)

        }

function RemoveDv(obj) {

    var p = document.getElementById("Parent")

    var chld = document.getElementById(obj)

    p.removeChild(chld)

}

</script>
    <script type="text/javascript">
        function validateFileSize() {
            var uploadControl = document.getElementById('<%= fuESNPic.ClientID %>');
            var filelen = uploadControl.value;


           // alert(filelen.length)
            if (filelen.length == 0) {

                document.getElementById('dvMsg').style.display = "none";
                document.getElementById('Div1').style.display = "block";
                return false;
            }
            else {


                if (uploadControl.files[0].size > 2097152) {
                    document.getElementById('Div1').style.display = "none";
                    document.getElementById('dvMsg').style.display = "block";
                    return false;
                }
                else {
                    document.getElementById('Div1').style.display = "none";
                    document.getElementById('dvMsg').style.display = "none";
                    return true;
                }
            }
        }
        function EditRMA(url) {
            //alert(url);
            window.location = "http://wsaportal.com/" + url;
        }


        $(function () {
            $('#txtESNSearch').keyup(function () {
                if (this.value.match(/[^a-zA-Z0-9 ]/g)) {
                    this.value = this.value.replace(/[^a-zA-Z0-9 ]/g, '');
                }
            });
        });
        $(function () {
            $('#txtPONum').keyup(function () {
                if (this.value.match(/[^a-zA-Z0-9 ]/g)) {
                    this.value = this.value.replace(/[^a-zA-Z0-9 ]/g, '');
                }
            });
        });
//        $(function () {
//            $('#rmanumber').keyup(function () {
//                if (this.value.match(/[^a-zA-Z0-9 ]/g)) {
//                    this.value = this.value.replace(/[^a-zA-Z0-9 ]/g, '');
//                }
//            });
//        });
        $(function () {
            $('#txtRMADate').keyup(function () {
                if (this.value.match(/[^a-zA-Z0-9 ]/g)) {
                    this.value = this.value.replace(/[^a-zA-Z0-9 ]/g, '');
                }
            });
        });
        $(function () {
            $('#txtRMADateTo').keyup(function () {
                if (this.value.match(/[^a-zA-Z0-9 ]/g)) {
                    this.value = this.value.replace(/[^a-zA-Z0-9 ]/g, '');
                }
            });
        });
        $(function () {
            $('#txtUPC').keyup(function () {
                if (this.value.match(/[^a-zA-Z0-9 ]/g)) {
                    this.value = this.value.replace(/[^a-zA-Z0-9 ]/g, '');
                }
            });
        });
        $(function () {
            $('#txtAVSO').keyup(function () {
                if (this.value.match(/[^a-zA-Z0-9 ]/g)) {
                    this.value = this.value.replace(/[^a-zA-Z0-9 ]/g, '');
                }
            });
        });


        jQuery.fn.fadeIn = function(speed, callback) {
            return this.animate({ opacity: 'show' }, speed, function() {
                if (jQuery.browser.msie)
                    this.style.removeAttribute('filter');

                if (jQuery.isFunction(callback))
                    callback();
            });
        };

        jQuery.fn.fadeOut = function(speed, callback) {
            return this.animate({ opacity: 'hide' }, speed, function() {
                if (jQuery.browser.msie)
                    this.style.removeAttribute('filter');

                if (jQuery.isFunction(callback))
                    callback();
            });
        };

        jQuery.fn.fadeTo = function(speed, to, callback) {
            return this.animate({ opacity: to }, speed, function() {
                if (to == 1 && jQuery.browser.msie)
                    this.style.removeAttribute('filter');

                if (jQuery.isFunction(callback))
                    callback();
            });
        };


        $(function() {
            $("#tabs").tabs();
        });

        function displayrma(mode) {
            if ('s' == mode) {
                $("#rmaform").toggle(function() {
                    $("#rmaform").fadeTo('slow', 0);
                });

                $("#rmaitemlookup").toggle(function() {
                    $("#rmaitemlookup").fadeTo('slow', 1);
                });
            }
            else {
                $("#rmaitemlookup").toggle(function() {
                    $("#rmaitemlookup").fadeTo('slow', 0);
                });

                $("#rmaform").toggle(function() {
                    $("#rmaform").fadeTo('slow', 1);
                });
            }
        }


        function generateRMADetail() {
            $(".admindisplay").toggle();

        }
        function  doReadonly(evt)
        {
            
           evt.keyCode = 0;
           return false;
        }
        function set_focus() {
            var img = document.getElementById("img1");
            var st = document.getElementById("ddlStatus");
            st.focus();
            img.click();
        }
        function set_focus1() {
            var img = document.getElementById("img1");
            var st = document.getElementById("ddlStatus");
            st.focus();
            img.click();
        }

        function set_focus2() {
            var img = document.getElementById("img2");
            var st = document.getElementById("ddlStatus");
            st.focus();
            img.click();
        } 

        function selectcompany(obj) {

            var objid = "";
            if (obj.id.indexOf('lnkedit') > -1)
                objid = 'lnkedit';
            objhdCompanyID = document.getElementById(obj.id.replace(objid, 'hdnCompanyid'));

            document.getElementById("hdnCompanyId").value = objhdCompanyID.value
        }

        function displayStatus(obj)
        {
            var count = document.getElementById('hdncount').value;
            var statuspanel = document.getElementById('statuspanel');
            if (obj == '0') {
                if (statuspanel != null) {
                    statuspanel.style.display = 'none';
                    return false;
                }
            }
            else {
                if (count == '1') {
                    if (statuspanel != null) {

                        statuspanel.style.display = 'block';
                    }
                }
                else
                    alert('RMA not selected!');
                return false;
            }
        }
        function validateUser()
        {
            var status = document.getElementById('ddlchangestatus');
            if(status.selectedIndex < 1)
            {
               alert('Please select a status');
               return false;
            }   
            var confirmflag = confirm('Do you want to change the status of selected RMA?');
            if(confirmflag)
                return true;
            else
            {
                displayStatus(0);
                return false; 
            } 
               
        }
        
        function RowSelected(sender, args)
        {
            document.getElementById('hdncount').value="1";
        }
    </script>

        <link rel="stylesheet" type="text/css" href="/dhtmlxwindow/dhtmlxwindows.css"/>
	    <link rel="stylesheet" type="text/css" href="/dhtmlxwindow/skins/dhtmlxwindows_dhx_skyblue.css"/>
	    <script src="/dhtmlxwindow/dhtmlxcommon.js" type="text/javascript"></script>
	    <script src="/dhtmlxwindow/dhtmlxwindows.js" type="text/javascript"></script>
	    <script src="/dhtmlxwindow/dhtmlxcontainer.js" type="text/javascript"></script>
        <script type="text/javascript" language="javascript">
            var dhxWins, w1;
            function RmaDownload(rmaGUID) {
                dhxWins = new dhtmlXWindows();
                dhxWins.enableAutoViewport(false);
                dhxWins.attachViewportTo("winVP");
                dhxWins.setImagePath("../../codebase/imgs/");

                
                var topPos = document.body.scrollTop;  
              
                //alert(top);

                w1 = dhxWins.createWindow("w1", 225, topPos, 625, 350);
                w1.setText("RMA Document");
                w1.attachURL("rmadownload.aspx?rmaguid=" + rmaGUID);

            }
            function RmaDownload2(obj) {
                dhxWins = new dhtmlXWindows();
                dhxWins.enableAutoViewport(false);
                dhxWins.attachViewportTo("winVP");
                dhxWins.setImagePath("../../codebase/imgs/");
                var Obj2 = document.getElementById(obj.id.replace('imgDoc', 'hdnrmaGUID'));
                //var objID = obj.replace('imgDoc', 'hdnrmaGUID')
                //alert(Obj2.value);
                var topPos = document.body.scrollTop;

                //alert(top);

                w1 = dhxWins.createWindow("w1", 225, topPos, 625, 350);
                w1.setText("RMA Document");
                w1.attachURL("rmadownload.aspx?rmaguid=" + Obj2.value);
                return false;
            }
        </script>
		
    
    </div>


</head>
<body bgcolor="#ffffff" leftmargin="0" rightmargin="0" topmargin="0">
    <form id="frmRMAItemLookup" runat="server" >
    
    
    <table cellspacing="0" cellpadding="0" border="0" align="center" width="100%">
        <tr>
            <td>
                <head:MenuHeader ID="MenuHeader1" runat="server"/>
            </td>
        </tr>
    </table>
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true" ></asp:ScriptManager> 


        <div id="divContainer">
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
       	
		     <div id="divTriage" style="display:none">
					
				<asp:UpdatePanel ID="upTriage" runat="server">
					<ContentTemplate>
						<asp:PlaceHolder ID="phTriage" runat="server">
                        <asp:Label ID="lblTriage" runat="server" CssClass="errormessage"></asp:Label>
                            <asp:Panel ID="pnlTriage" runat="server">
                            

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
                                    <strong>    <asp:Label ID="lbRma" CssClass="copy10grey" runat="server"></asp:Label></strong>
                                    
                                    </td>
                                    </tr>
                                   <%-- <tr>
                                        <td class="copy10grey" style="width:100px" align="right">
                                    RMA Status: &nbsp;
                                    </td>
                                    <td  class="copy10grey" align="left">
                                        <asp:Label ID="lblRmaStatus" CssClass="copy10grey" runat="server"></asp:Label>
                                    
                                    </td>
                                    </tr>--%>

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
                                    <%--<asp:Button ID="btnTriage" runat="server" Text="Add Triage" CssClass="buybt"   OnClick="btnTriage_Click" />
                                    &nbsp;--%>
                                    <asp:Button ID="btnTriageCancel" runat="server" Text="Close" CssClass="buybt"   
                                    OnClientClick="return closedTriageDialog();"/>
                                   
                                </td>
                            </tr>
                            </table>
                            <br />
                            </td>
                            </tr>
                            </table>

                            </asp:Panel>
                            
                        </asp:PlaceHolder>
						
						
					</ContentTemplate>
							
				</asp:UpdatePanel>
             </div>

             <div id="divRMAESN" style="display:none">
					
				<asp:UpdatePanel ID="upnlViewRMA" runat="server"  >
					<ContentTemplate>
                   
						<asp:PlaceHolder ID="phrViewRMA" runat="server">
		                    
                        <table width="100%">
            <tr>
                 <td>
                
                <div style="overflow:auto; height:550px; width:100%; border: 0px solid #839abf" >
      
<table width="98%" align="center" cellpadding="0" cellspacing="0">
<tr>
    <td>
    <asp:Label ID="lblMsgDetail" runat="server" CssClass="errormessage"></asp:Label>
        <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="100%" align="center" >
           <tr bordercolor="#839abf">
                <td>
                <table width="100%" cellSpacing="3" cellPadding="3">
                <tr>
                    <td class="copy10grey" width="25%" align="right">
                        RMA#:
                    </td>
                    <td width="1%">&nbsp;</td>
                    <td width="24%" align="left">
                        <asp:Label ID="lblRMA" CssClass="copy10grey" runat="server"  ></asp:Label>
                    </td>
                    <td class="copy10grey" width="15%" align="right">
                        RMA Date:
                    </td>
                    <td width="1%">&nbsp;</td>
                    <td width="33%" align="left">
                        <asp:Label ID="lblRMADate" CssClass="copy10grey" runat="server" ></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="copy10grey" width="25%" align="right">
                        Status:
                    </td>
                    <td width="1%">&nbsp;</td>
                    <td width="24%" align="left">
                        <asp:Label ID="lblStatus" CssClass="copy10grey" runat="server"  ></asp:Label>
                    </td>
                    <td class="copy10grey" width="15%" align="right">
                        Company Name:
                    </td>
                    <td width="1%">&nbsp;</td>
                    <td width="33%" align="left">
                        <asp:Label ID="lblCompanyName" CssClass="copy10grey" runat="server" ></asp:Label>
                    </td>
                </tr>
</table>
                    </td>
               </tr>
            </table>
        <br />
                <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="100%" align="center" >
           <tr bordercolor="#839abf">
                <td>
                <table width="100%" cellSpacing="3" cellPadding="3">
        
                <tr>
                    <td class="copy10grey" width="25%" align="right">
                        Customer Name:
                    </td>
                    <td width="1%">&nbsp;</td>
                    <td width="24%" align="left">
                        <asp:Label ID="lblCustomer" CssClass="copy10grey" runat="server"  ></asp:Label>
                    </td>
                    <td class="copy10grey" width="15%" align="right">
                        StoreID:
                    </td>
                    <td width="1%">&nbsp;</td>
                    <td width="33%" align="left">
                        <asp:Label ID="lblStoreID" CssClass="copy10grey" runat="server"  ></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="copy10grey" width="25%" align="right">
                        Address:
                    </td>
                    <td width="1%">&nbsp;</td>
                    <td width="24%" align="left">
                        <asp:Label ID="lblAddress" CssClass="copy10grey" runat="server"  ></asp:Label>
                    </td>
                    <td class="copy10grey" width="15%" align="right">
                       City:
                    </td>
                    <td width="1%">&nbsp;</td>
                    <td width="33%" align="left">
                       <asp:Label ID="lblCity" CssClass="copy10grey" runat="server"  ></asp:Label>
                    </td>
                </tr>
                    <tr>
                    <td class="copy10grey" width="25%" align="right">
                        State:
                    </td>
                    <td width="1%">&nbsp;</td>
                    <td width="24%" align="left">
                        <asp:Label ID="lblState" CssClass="copy10grey" runat="server"  ></asp:Label>
                    </td>
                    <td class="copy10grey" width="15%" align="right">
                       Zip:
                    </td>
                    <td width="1%">&nbsp;</td>
                    <td width="33%" align="left">
                        <asp:Label ID="lblZip" CssClass="copy10grey" runat="server"  ></asp:Label>
                    </td>
                </tr>
    
                <tr style="display:none">
                    <td class="copy10grey" width="25%" align="right">
                    Address:
                    </td>
                    <td width="1%">&nbsp;</td>
                    <td  align="left" colspan="5">
                        <asp:Label ID="lblComment" CssClass="copy10grey" runat="server" ></asp:Label>
                    </td>
                </tr>   
                <tr  style="display:none">
                    <td class="copy10grey" width="25%" align="right">
                    State:
                    </td>
                    <td width="1%">&nbsp;</td>
                    <td  colspan="5" align="left">
                        <asp:Label ID="lblAVComment" CssClass="copy10grey" runat="server" ></asp:Label>
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
<td align="right" class="copy10grey" >

    <asp:Button ID="btnPackingSlip" runat="server"  Text="Packing Slip" 
         CssClass="button" Height="24px" 
        OnClick="btnGeneratePackingSlip_Click" />
    &nbsp;
    
    <asp:Button ID="btnPrintLabel" runat="server" Visible="false" Text="Print Label" 
         CssClass="button" Height="24px" Width="130px"
        OnClick="btnPrintLabel_Click" />
      <%--<asp:Button ID="btnPrintLabel" runat="server" Visible="false" Text="Print Label" 
        OnClientClick="openLabelDialogAndBlock('Shipping Label', 'btnPrintLabel')" CssClass="button" Height="24px" Width="130px"
        OnClick="btnPrintLabel_Click" />--%>
</td>
</tr>
<tr>
    <td class="buttonlabel" align="left">
        Shipment
    </td>
</tr>

<tr>
<td><%--runat="server" id="tblShipment" >--%>
        <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="100%" align="center"> 
           <tr bordercolor="#839abf">
                <td>
                    <table border="0" width="100%" class="box" align="center" cellpadding="5" cellspacing="5">
                        <tr valign="top">
                            <td class="buttongrid" align="left" width="1%">
                                Line#
                            </td>
                            <td class="buttongrid" align="left" width="15%">
                                Ship Via
                                    </td>
                            <td class="buttongrid" align="left" width="20%">
                                Ship Date
                                    </td>
                            <td class="buttongrid" align="left" width="14%">
                                Ship Package
                                    </td>
                            <td class="buttongrid" align="left" width="10%">
                                Weight(Oz)
                                    </td>
                            <td class="buttongrid" align="left" width="10%">
                                Price
                                    </td>
                            <td class="buttongrid" align="left" width="30%">
                                Tracking Number
                                    </td>
                                </tr>        
                        <tr valign="top">
                                    
                                    <td class="copy10grey" align="right" width="1%">
                                         <asp:Label ID="lblLineNo" CssClass="copy10grey"   runat="server"></asp:Label>
                                    </td>
                                    
                                    <td>
                                         <asp:Label ID="lblShipMethod" CssClass="copy10grey"   runat="server"></asp:Label>
                                     
                                     </td>
                                    <td  class="copy10grey" >
                                       <asp:Label ID="lblShipDate" CssClass="copy10grey"   runat="server"></asp:Label>
                                        </td>
                                    <td  class="copy10grey" >
                                        <asp:Label ID="lblPackage" CssClass="copy10grey"   runat="server"></asp:Label>
                                        </td>
                                    <td class="copy10grey" >
                                            
                                             <asp:Label ID="lblWeight" CssClass="copy10grey"   runat="server"></asp:Label>
                                        </td>
                            <td class="copy10grey" >
                                                 <asp:Label ID="lblPrice" CssClass="copy10grey"   runat="server"></asp:Label>
                                        </td>
                                        <td class="copy10grey" >
                                                 <asp:Label ID="lblTrackingNo" CssClass="copy10grey"   runat="server"></asp:Label>
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
    <td>
        <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="100%" align="center" >
           <tr bordercolor="#839abf">
                <td>
                    <table border="0" width="100%" class="box" align="center" cellpadding="5" cellspacing="5">
                    <tr valign="top">
                        <td class="copy10grey" align="right" width="15%">
                          Shipment Comments:  
                        </td>
                        <td>
                            <asp:Label ID="lblShipComment" CssClass="copy10grey"   runat="server"></asp:Label>
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
    <td class="buttonlabel" align="left">
        Line Items
    </td>
</tr>
<tr>
    <td>
        <asp:GridView ID="gvRMADetail"  BackColor="White" Width="100%" Visible="true" 
        AutoGenerateColumns="false" Font-Names="Verdana" runat="server" DataKeyNames="rmaguID"
        GridLines="Both"  OnRowDataBound="gvRmaDetail_RowDataBound" 
        BorderStyle="Double" BorderColor="#0083C1">
        <RowStyle BackColor="Gainsboro" />
        <AlternatingRowStyle BackColor="white" />
        <HeaderStyle  CssClass="buttongrid" ForeColor="white"/>
        <FooterStyle CssClass="white"  />
        <Columns>
            
            <asp:TemplateField HeaderText="Line#" HeaderStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey"  ItemStyle-Width="1%">
                <ItemTemplate><%#  Container.DataItemIndex + 1 %></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ESN#" SortExpression="ESN" HeaderStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey"  ItemStyle-Width="20%">
                <ItemTemplate><%# Eval("ESN")%></ItemTemplate>
            </asp:TemplateField>
                                                                                                                                    
                                            
           <%-- <asp:TemplateField HeaderText="UPC"   ItemStyle-CssClass="copy10grey" ItemStyle-Width="15%">
                <ItemTemplate><%#Eval("UPC")%></ItemTemplate>
            </asp:TemplateField> --%>
             <asp:TemplateField HeaderText="SKU#"   ItemStyle-CssClass="copy10grey" ItemStyle-Width="15%">
                <ItemTemplate>
                    <%# Convert.ToString(Eval("ESN")) != "" && Convert.ToString(Eval("Itemcode")) == "" ? "External Esn":Eval("Itemcode") %> 
                                                            
                    <%--<%# Eval("ItemCode")%>--%>

                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Reason"   ItemStyle-CssClass="copy10grey" ItemStyle-Width="15%">
                <ItemTemplate>
                
                 <asp:HiddenField ID="hdnReason" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "reason")%>' />
                                                <asp:Label ID="lblreason" runat="server" ></asp:Label>
                
                </ItemTemplate>
            </asp:TemplateField>
                                          
                                            
                                        
            <asp:TemplateField HeaderText="Status" SortExpression="Status"  ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                <ItemTemplate><%#Eval("Status")%></ItemTemplate>
                                                                                                 
            </asp:TemplateField>
            <%--<asp:TemplateField HeaderText="TrackingNumber" ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                <ItemTemplate>
                    <%# Eval("ShippingTrackingNumber") %>
                </ItemTemplate>
                                                
            </asp:TemplateField>
            --%>
                                            
    <asp:TemplateField ItemStyle-Width="5%">
                <ItemTemplate><%--
                        <asp:ImageButton ToolTip="Delete RMA Item"   CommandArgument='<%# Eval("rmaDetGUID") %>'  OnClientClick="return confirm('Do you want to delete?');"
                            ImageUrl="~/Images/delete.png" ID="imgDelDetail1" OnCommand="imgDeleteRMADetail_Commnad" runat="server" />--%>
                            <asp:ImageButton ID="imgTriage"  ToolTip="View RMA Triage" AlternateText="View RMA Triage" 
                            CommandArgument='<%# DataBinder.Eval(Container.DataItem, "TriageStatusID")+"~"+ Eval("TriageNotes")%>' CommandName="ss" 
                             OnCommand="imgTriage_click" CausesValidation="false"  ImageUrl="~/Images/key_t.png"  runat="server" />

                             <a href='ESNImageUpload.aspx?rmaGUID=<%# Eval("rmaGUID") %>&rmaDetGUID=<%# Eval("rmaDetGUID") %>' target="blank" >
                             <img src="/Images/upload-16.png" alt="Add/View RMA Pictire" />
                             </a>
                             <%--<asp:ImageButton ID="imgPic" Visible="false" ToolTip="View RMA Pictire" AlternateText="View RMA Pictire" 
                            CommandArgument='<%# DataBinder.Eval(Container.DataItem, "PictureID")+","+ Eval("rmaDetGUID")+","+ Eval("ESN")+","+ Eval("ESNPicture")  %>' CommandName="ss" 
                             OnCommand="imgPictire_click" CausesValidation="false"  ImageUrl="~/Images/upload-16.png"  runat="server" />
                             --%>

              </ItemTemplate>
              </asp:TemplateField>                                                
			                                
        </Columns>
    </asp:GridView>
    </td>
</tr>
</table>

                    

                    
                </div>
               
                </td>
            </tr>
        </table>
                        </asp:PlaceHolder>
						
						
					</ContentTemplate>
							<Triggers>
                                <asp:PostBackTrigger ControlID="btnPrintLabel" />
                                <asp:PostBackTrigger ControlID="btnPackingSlip" />
							</Triggers>
				</asp:UpdatePanel>

			
			</div>

            
             <div id="divDoc" style="display:none">
					
				<asp:UpdatePanel ID="upDoc" runat="server">
					<ContentTemplate>
                   
						<asp:PlaceHolder ID="phDoc" runat="server">

                        
    <table  cellSpacing="1" cellPadding="1" width="100%" align="center">
        <tr>
			<td colSpan="6" bgcolor="#dee7f6" class="topHeader" align="left">&nbsp;&nbsp;RMA document
			</td>
        </tr>
        </table>
    <table bordercolor="#839abf" border="1" width="98%" align="center" cellpadding="0" cellspacing="0">
                            <tr><td>

                            <table border="0" width="100%" class="box" align="center" cellpadding="0" cellspacing="0">
                            <tr valign="top">
                                <td>
                                    <asp:Label ID="lblDoc" runat="server" CssClass="errormessage"></asp:Label>
                            
                                    <asp:Repeater ID="rptRMADoc" runat="server">
                                    <HeaderTemplate>
                                    <table width="100%" align="center" cellpadding="1" cellspacing="1">
                                        <tr>
                                            <td class="buttongrid">
                                            &nbsp;File name 
                                            </td>
                                            <td class="buttongrid">
                                                &nbsp;Last Modified Date
                                            </td>
                                            <td class="buttongrid">
                                                &nbsp;
                                            </td>                
                                        </tr>
                            
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                    <tr>
                                        <td class="copy10grey">
                                            <asp:LinkButton ID="lnlDoc" CommandArgument='<%# Eval("RMADocument")%>'  OnCommand="DownloadRmaDoc_Click" runat="server" >
                                            &nbsp;<%# Eval("RMADocument")%>
                                            </asp:LinkButton>
                                        
                                        </td>
                                        <td class="copy10grey">
                                            &nbsp;<%# Eval("ModifiedDate")%>
                                            <%--<%# DataBinder.Eval(Container.DataItem, "ModifiedDate", "{0:MM/dd/yyyy}")%>--%>
                                        </td>
                                        <td class="copy10grey">
                                              <asp:ImageButton ID="imgDel" runat="server" OnClientClick="return confirm('Do you want to delete?');"  CommandName="Delete" AlternateText="Delete RMA Document" ToolTip="Delete RMA Document" 
                        ImageUrl="~/images/delete.png" CommandArgument='<%# Eval("RmaDocID") %>' OnCommand="imgDeleteRMADoc_OnCommand"/>
                        
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
    <br />
    
    
    <table  cellSpacing="1" cellPadding="1" width="100%" align="center">
        <tr>
			<td colSpan="6" bgcolor="#dee7f6" class="topHeader" align="left">&nbsp;&nbsp;Administration RMA document
			</td>
        </tr>
        </table>
    <table bordercolor="#839abf" border="1" width="98%" align="center" cellpadding="0" cellspacing="0">
                            <tr><td>

                            <table border="0" width="100%" class="box" align="center" cellpadding="0" cellspacing="0">
                            <tr valign="top">
                                <td>
                                    <asp:Label ID="lblMsgAdm" runat="server" CssClass="errormessage"></asp:Label>
                                
                                    <asp:Repeater ID="rptAdminRma" runat="server" OnItemDataBound="rptRmaDoc_ItemDataBound">
                                    <HeaderTemplate>
                                    <table width="100%" align="center" cellpadding="1" cellspacing="1">
                                        <tr>
                                            <td class="buttongrid">
                                            &nbsp;File name 
                                            </td>
                                            <td class="buttongrid">
                                                &nbsp;Last Modified Date
                                            </td>
                                            <td class="buttongrid">
                                                &nbsp;
                                            </td>                
                                        </tr>
                            
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                    <tr class="<%# Container.ItemIndex % 2 == 0 ? "alternaterow" : "" %>">
                                        <td class="copy10grey">
                                            <asp:LinkButton ID="lnlDoc" CommandArgument='<%# Eval("RMADocument")%>'  OnCommand="DownloadRmaDoc_Click" runat="server" >
                                            &nbsp;<%# Eval("RMADocument")%>
                                            </asp:LinkButton>
                                        
                                        </td>
                                        <td class="copy10grey">
                                            &nbsp;<%# Eval("ModifiedDate")%>
                                            <%--<%# DataBinder.Eval(Container.DataItem, "ModifiedDate", "{0:MM/dd/yyyy}")%>--%>
                                        </td>
                                        <td class="copy10grey">
                                              <asp:ImageButton ID="imgDel" runat="server" OnClientClick="return confirm('Do you want to delete?');" CommandName="Delete" AlternateText="Delete RMA Document" ToolTip="Delete RMA Document" 
                        ImageUrl="~/images/delete.png" CommandArgument='<%# Eval("RmaDocID") %>' OnCommand="imgDeleteRMADoc_OnCommand"/>
                        
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

             <div id="divPicture" style="display:none">
					
				<asp:UpdatePanel ID="upPicture" runat="server">
					<ContentTemplate>
						<asp:PlaceHolder ID="phPicture" runat="server">
                        <asp:Label ID="lblPic" runat="server" CssClass="errormessage"></asp:Label>
                            <asp:Panel ID="Panel1" runat="server">
                            

                            <table bordercolor="#839abf" border="1" cellspacing="0" cellpadding="0" width="100%">
                            <tr bordercolor="#839abf">
                                <td>
                                <br />

                                <table class="box" width="95%" align="center" cellspacing="5" cellpadding="5">
                                <tr>
                                <td colspan="2">
                                <table cellpadding="0" cellspacing="0" width="100%" border="0">

                                <tr id="Tr1" runat="Server">

                                    <td class="copy10grey" align="center">

                                    <label class="copy10grey" >

                                    Upload file: </label><asp:FileUpload ID="uploadPhoto1" runat="server" CssClass="copy10grey" /><br />

                                    <div id="Parent">

                                    </div>
                                    <br />
                                    <hr />
                                    <label class="copy10grey">

                                    &nbsp;</label>

                                    <input type="button" onclick="AddNewRow(); return false;"  value="More" class="buybt" />&nbsp;

                                    <asp:Button ID="btnAddPhoto" Text="Upload" runat="server" class="buybt"

                                    onclick="btnAddPhoto_Click1" />&nbsp;

                                    <asp:Button ID="btnCancel2" Text="cancel" runat="server" class="buybt"/>

                                    </td>
                                </tr>
                                </table>
                                </td>
                                </tr>
                                </table>
                                <div style="display:none">
                                <table >
                            <tr valign="top">
                                <td class="copy10grey" style="width:100px" align="right">
                                    ESN: &nbsp;
                                </td>
                                <td  class="copy10grey" align="left">
                                    <asp:Label ID="lblESN" CssClass="copy10grey" runat="server" ></asp:Label>
                                    
                                </td>
                            </tr>
                            <tr>
                                <td  class="copy10grey" align="right">
                                    Upload file: &nbsp;
                                </td>
                                <td class="copy10grey" align="left">
                                    <asp:FileUpload ID="fuESNPic" runat="server" />
                                    <div id="dvMsg" style="background-color:Red; color:White; width:190px; padding:3px; display:none;" >
                                    Maximum size allowed is 2 MB
                                    </div>  
                                    <div id="Div1" style="background-color:Red; color:White; width:190px; padding:3px; display:none;" >
                                    Upload file is required!
                                    </div>  
                                </td>
                                
                            </tr>
                            <tr>
                                <td colspan="2" class="copy10grey" align="center">
                                    <hr />
                                </td>
                            </tr>
                            
                            <tr>
                                <td colspan="2" align="center" class="copy10grey">
                                    <asp:Button ID="btnPicture" runat="server" Text="  Upload  " CssClass="buybt" OnClientClick="return validateFileSize();"   OnClick="btnPicture_Click" />
                                    &nbsp;
                                    <asp:Button ID="btnPicCancel" runat="server" Text=" Close " CssClass="buybt"   
                                    OnClientClick="return closedPictureDialog();"/>
                                   
                                </td>
                            </tr>
                            </table>
                            </div>
                            <br />
                            </td>
                            </tr>
                            </table>

                            </asp:Panel>
                            
                        </asp:PlaceHolder>
						
						
					</ContentTemplate>
							<Triggers>
                            <asp:PostBackTrigger ControlID="btnPicture" />
                            <asp:PostBackTrigger ControlID="btnAddPhoto" />

                            
                            </Triggers>
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
                                        Ship Date:
                                    </td>
                                    <td width="34%">

                                        <asp:TextBox ID="txtShippingDate" ContentEditable="false" Width="91%" onkeydown="return ReadOnlys(event);" runat="server" CssClass="copy10grey"></asp:TextBox>&nbsp;
                                        <img id="img3" alt=""  src="/fullfillment/calendar/sscalendar.jpg" runat="server"/>
                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="img3" 
                                        PopupPosition="BottomLeft" Format="MM/dd/yyyy"   TargetControlID="txtShippingDate">
                                        </cc1:CalendarExtender>
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
                                           Ship Package:
                                    </td>
                                    </td>
                                    <td width="32%" class="copy10grey" >
                                        <asp:DropDownList ID="ddlShape" runat="server" Width="91%" CssClass="copy10grey"></asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="copy10grey" align="right" width="17%">
                                            Tracking Number:    
                                        </td>
                                        <td  width="34%">
                                             <asp:TextBox ID="txtTrackingNumber" Width="91%" Enabled="false" onkeypress="return isNumberKey(event);" MaxLength="50" CssClass="copy10grey" runat="server"></asp:TextBox>
                                        
                                        </td>
                                        
                                    <td class="copy10grey" align="right" width="17%">
                                          <%--Generate Label:--%>
                                    </td>
                                    <td width="32%" class="copy10grey" >
                                     <%--   <asp:CheckBox ID="chkLabel" Checked="true" runat="server" />

                                        <asp:Button ID="btnGenLabel" runat="server" Visible="false" Text="Generate Label" OnClick="btnGenLabel_Click" CausesValidation="false" CssClass="button" />--%>

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
                          
                <table width="100%" align="center">
                <tr>
                    <td align="center">
                         <asp:Button ID="btnGenLabel" runat="server" OnClick="btnGenerateLabel" Text="Generate Label" CssClass="button" Height="24px" Width="130px"
                             OnClientClick="return ValidateShipPo();" />&nbsp;
                         <asp:Button ID="btnShipCancel" runat="server" Text="Cancel" CssClass="button" Height="24px" Width="130px" OnClientClick="closeShipDialog()"  />
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

             <div id="divHistory" style="display:none">
					
				<asp:UpdatePanel ID="upHistory" runat="server" >
                
					<ContentTemplate>
                    
						<asp:PlaceHolder ID="phrHistory" runat="server">
                            
                            
<table width="100%">
<tr>
    <td >
<table bordercolor="#839abf" border="1" width="98%" align="center" cellpadding="0" cellspacing="0">
            <tr><td>

            <table border="0" width="100%" class="box" align="center" cellpadding="5" cellspacing="5">
            <tr valign="top">
                <td>
                <strong> RMA#: </strong>&nbsp;&nbsp;&nbsp;<asp:Label ID="lblRMANum" runat="server" CssClass="copy10grey"></asp:Label>
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
                            
            <asp:Repeater ID="rptRma" runat="server">
            <HeaderTemplate>
            <table width="100%" align="center">
                <tr>
                    <td class="buttongrid" width="10%">
                    &nbsp;Status
                    </td>
                    <td class="buttongrid" width="18%">
                        &nbsp;Modified Date
                    </td>
                    <td class="buttongrid" width="15%">
                        &nbsp;Modified By
                    </td>

                    <td class="buttongrid" width="10%">
                        &nbsp;Source
                    </td>     
                    <td class="buttongrid" width="47%">
                        &nbsp;
                        Comments
                    </td>
                </tr>
                            
            </HeaderTemplate>
            <ItemTemplate>
            <tr>
                <td class="copy10grey">
                    <%-- <span title='<%# Eval("AVComments") %>'> &nbsp;<%# Eval("Status") %></span> --%>

<span > &nbsp;<%# Eval("Status") %></span>               
                </td>
                <td class="copy10grey">
                    &nbsp;<%# Eval("CreateDate")%>
                    <%--<%# DataBinder.Eval(Container.DataItem, "ModifiedDate", "{0:MM/dd/yyyy}")%>--%>
                </td>
                <td class="copy10grey">
                        &nbsp;<%# Eval("CreatedBy") %>
                </td>
<td class="copy10grey">
                        &nbsp;<%# Eval("Modulename")%>
                </td>
<td class="copy10grey">
                        &nbsp;<%# Eval("Comments")%>
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
    </td>
</tr>
</table>
                        </asp:PlaceHolder>
						
						
					</ContentTemplate>
							
				</asp:UpdatePanel>
			
			</div>

      
		</div>
    <%--<div id="winVP">--%>
    <asp:UpdatePanel ID="uPnl" runat="server"  UpdateMode="Conditional">
       <ContentTemplate>


    <table cellspacing="0" cellpadding="0" border="0" align="center" width="100%">
        <tr><td>
        <table width="95%" align="center" cellspacing="0" cellpadding="0"> 
        <tr>
            <td>
                <table style="text-align: left; width:100%;" align="center" class="copy10grey">
                    <tr>
                        <td>
                            &nbsp;<input type="hidden" ID="hdncount" />
                        </td>
                    </tr>
                    <tr>
                        <td class="buttonlabel" align="left">
                            &nbsp; Return Merchandise Authorization (RMA) - Search
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblMsg" runat="server" CssClass="errormessage"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table bordercolor="#839abf" border="1" cellspacing="0" cellpadding="0" width="100%" align="center">
                    <tr bordercolor="#839abf">
                        <td>
                            <table style="text-align: left; width: 100%;" cellspacing="0" cellpadding="0" align="center" class="copy10grey">
                                <tr>
                                    <td>
                                        <table class="box" width="100%" align="center" cellspacing="2" cellpadding="2">
                                            <tr>
                                                <td >
                                                    <asp:Label ID="lblComapny" CssClass="copy10grey" runat="server" Text="Company:"></asp:Label>
                                                    </td>
                                                        <asp:HiddenField ID="hdnrmaGUIDs" runat="server" />
                                                        <asp:HiddenField ID="hdncompany" runat="server" />
                                                        <asp:HiddenField ID="hdnCompanyId" runat="server" />
                                                        <asp:HiddenField ID="hdnUserID" runat="server" />
                                                <td>
                                                    <asp:Panel ID="companyPanel" runat="server">
                                                        <asp:DropDownList ID="ddlCompany" CssClass="copy10grey" AutoPostBack="true" Width="40%"
                                                            runat="server" onselectedindexchanged="ddlCompany_SelectedIndexChanged"  >
                                                        </asp:DropDownList>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="copy10grey" width="15%">
                                                    RMA#:
                                                </td>
                                                <td width="35%">
                                                    <asp:TextBox runat="server" ID="rmanumber" CssClass="copy10grey" Width="40%" />
                                                </td>
                                                 <td class="copy10grey" width="15%">
                                                    ESN:
                                                </td>
                                                <td  width="35%">
                                                    <asp:TextBox runat="server" ID="txtESNSearch" MaxLength="35" CssClass="copy10grey" Width="40%"  />
                                                </td>
                                               
                                            </tr>
                                            <tr>
                                                <td class="copy10grey">
                                                    RMA From Date:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtRMADate" runat="server" Width="40%" onfocus="set_focus1();" onkeypress="return doReadonly(event);"
                                                        CssClass="copy10grey" MaxLength="15" Text="" />
                                                    <img id="img1" alt="" onclick="document.getElementById('<%=txtRMADate.ClientID%>').value = ''; displayCalendar(document.getElementById('<%=txtRMADate.ClientID%>'),'mm/dd/yyyy',this,true);"
                                                        src="../../fullfillment/calendar/sscalendar.jpg" />
                                                    
                                               </td>
                                                <td class="copy10grey">
                                                    RMA To Date:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtRMADateTo" runat="server" Width="40%" onfocus="set_focus2();" onkeypress="return doReadonly(event);"
                                                        CssClass="copy10grey" MaxLength="15" Text="" />
                                                    <img id="img2" alt="" onclick="document.getElementById('<%=txtRMADateTo.ClientID%>').value = ''; displayCalendar(document.getElementById('<%=txtRMADateTo.ClientID%>'),'mm/dd/yyyy',this,true);"
                                                        src="../../fullfillment/calendar/sscalendar.jpg" />
                                                    
                                               </td>
                                            </tr>
                                            <tr valign="top">
                                                <td class="copy10grey">
                                                    Status:</td>
                                                <td>
                                                    <asp:DropDownList ID="ddlStatus" runat="server" Class="copy10grey" Width="40%" >
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
                                                </td>
                                                
                                                <td class="copy10grey">
                                                SKU:</td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="txtUPC" CssClass="copy10grey" Width="40%" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="copy10grey">Return Reason:</td>
                                                <td>
                                                    <asp:DropDownList ID="ddReason" CssClass="copy10grey" runat="server" Width="40%" >
                                                              </asp:DropDownList>
                                                    <%--<asp:TextBox runat="server" ID="txtAVSO"  MaxLength="30"  CssClass="copy10grey" Width="40%" />--%>

                                                </td>
                                                <td class="copy10grey">Purchase Order#:</td>
                                                <td><asp:TextBox runat="server" ID="txtPONum" MaxLength="30" CssClass="copy10grey" Width="40%" /></td>
                                            </tr>
                                             <tr>
                                                <td class="copy10grey"></td>
                                                <td>
                                                    </td>
                                                <td class="copy10grey">
                                                <%--<asp:Label ID="lblStore" CssClass="copy10grey" runat="server" Visible="false" >Store ID:</asp:Label>--%>
                                        
                                                </td>
                                                <td>
                                                   <%-- <asp:DropDownList CssClass="copy10grey" Width="41%"  Visible="false" 
                                                    ID="ddlStoreID" runat="server"   >
                                                    </asp:DropDownList>--%>
                                 
                                                </td>
                                            </tr>
                                             
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <hr />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:Button ID="Button1" runat="server" Text="Search RMA" OnClick="search_click"
                                            CssClass="button" Height="24px" Width="130px" />&nbsp;
                                        <asp:Button ID="Button2" runat="server" OnClick="Button2_Click1" Text="Cancel RMA"  Height="24px" Width="130px"
                                            CssClass="button" />
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
                <asp:Panel ID="pnlGrid" runat="server">
                
                <table width="100%" class="copy10grey" style="text-align: left">
                    <tr>
                        <td>
                        <table>
                        <tr>
                            <td>
                                <asp:Button ID="btnExport" runat="server" Visible="false" Text="Export to CSV" OnClick="btnExport_click" CssClass="button" />&nbsp;     
                                <asp:HyperLink ID="btnRMAReport" runat="server" Text="RMA Report" Visible="false" 
                                      NavigateUrl="/rma/rmalist.aspx" Target="_blank" />
                                <asp:HyperLink ID="hlkRMASummary" runat="server" Text="RMA Summary" Visible="false" 
                                      NavigateUrl="/rma/rmaSummary.aspx" Target="_blank" />
                               
                            </td>
                            <td>
                                <asp:Panel runat="server" ID="statuspanel" Visible="false" >
                                <table style="background:#969696">
                                <tr>
                                    
                                    <td class="copy10grey">
                                        Status
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlchangestatus" runat="server" Class="copy10grey" Width="165px">
                                            <asp:ListItem Value="0">------</asp:ListItem>
                                            <asp:ListItem Value="1">Pending</asp:ListItem>
                                            <asp:ListItem Value="2">Received</asp:ListItem>
                                            <asp:ListItem Value="3">Pending for Repair</asp:ListItem>
                                            <asp:ListItem Value="4">Pending for Credit</asp:ListItem>
                                            <asp:ListItem Value="5">Pending for Replacement</asp:ListItem>
                                            <asp:ListItem Value="6">Approved</asp:ListItem>
                                            <asp:ListItem Value="7">Cancelled</asp:ListItem>
                                            <asp:ListItem Value="8">Returned</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_click" OnClientClick="return validateUser();"
                                            CssClass="buybt" />&nbsp;
                                        <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Cancel" OnClientClick="return displayStatus(0);"
                                            CssClass="buybt" />
                                    </td>
                                </tr>
                                </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        </table>
                        
                        
               <table width="100%">
                        <tr>
                            <td align="right"> 
          
                    <asp:Label ID="lblCount" CssClass="copy10grey" runat="server" ></asp:Label>
           
                </td>
                </tr>


                    <tr>
                         <td> 
            
                     <asp:GridView ID="gvRma" OnPageIndexChanging="gridView_PageIndexChanging"  EnableViewState="true"  
                          AutoGenerateColumns="false"  OnRowDataBound="gvRma_RowDataBound"
                            DataKeyNames="RmaGUID"  Width="100%"  
                        ShowFooter="false" runat="server" GridLines="Both"  
                        PageSize="20" AllowPaging="true" 
                        BorderStyle="Outset"
                        AllowSorting="true" OnSorting="gvRma_Sorting"  > 
                        <RowStyle BackColor="Gainsboro" />
                        <AlternatingRowStyle BackColor="white" />
                        <HeaderStyle  CssClass="buttongrid" ForeColor="white"/>
                        <FooterStyle CssClass="white"  />
                        <PagerStyle ForeColor="#636363" CssClass="copy10grey" HorizontalAlign="Left" />
                        <Columns>
                            <%--<asp:TemplateField ItemStyle-CssClass="copy10grey"  HeaderText="" ItemStyle-Width="2%" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chk"  runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>                 
                            --%>
                            <asp:TemplateField ItemStyle-CssClass="copy10grey"  HeaderText="S.No." ItemStyle-Width="1%" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <%--<%# Eval("RMAUserCompany.CompanyName")%>--%>
                                    <%# Container.DataItemIndex + 1 %>
                                   
                                </ItemTemplate>
                            </asp:TemplateField>                 
                            
                            <asp:TemplateField ItemStyle-CssClass="copy10grey"  Visible="false" HeaderText="Customer Name" ItemStyle-Width="15%">
                                <ItemTemplate>
                                    <%# Eval("RMAUserCompany.CompanyName")%>

                                    
                                </ItemTemplate>
                            </asp:TemplateField>                 
                            <asp:TemplateField ItemStyle-CssClass="copy10grey" SortExpression="RmaNumber"  ItemStyle-Width="15%" HeaderText="RMA #" HeaderStyle-CssClass="buttonundlinelabel" >
                                <ItemTemplate>
                                    <%# Eval("RmaNumber")%>
                                    <%--<asp:Label ID="lblRMANo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "RmaNumber")%>' ></asp:Label>--%>
                                </ItemTemplate>
                            </asp:TemplateField>                 
                            <asp:TemplateField ItemStyle-CssClass="copy10grey"  SortExpression="RmaDate" ItemStyle-Width="7%" HeaderText="RMA Date" HeaderStyle-CssClass="buttonundlinelabel" >
                                <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "RmaDate", "{0:MM/dd/yyyy}")%>
                                    <%--<asp:Label ID="lblRmaDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "RmaDate", "{0:MM/dd/yyyy}")%>' ></asp:Label>--%>
                                    
                                </ItemTemplate>
                            </asp:TemplateField>              
                            
                            <asp:TemplateField ItemStyle-CssClass="copy10grey" SortExpression="RmaContactName"  ItemStyle-Width="15%" HeaderText="Customer Name" HeaderStyle-CssClass="buttonundlinelabel" >
                                <ItemTemplate>
                                    <%# Eval("RmaContactName")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="copy10grey" SortExpression="Address"  ItemStyle-Width="15%" HeaderText="Address" HeaderStyle-CssClass="buttonundlinelabel" >
                                <ItemTemplate>
                                    <%# Eval("Address")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField ItemStyle-CssClass="copy10grey" SortExpression="City"  ItemStyle-Width="10%" HeaderText="City" HeaderStyle-CssClass="buttonundlinelabel" >
                                <ItemTemplate>
                                    <%# Eval("City")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField ItemStyle-CssClass="copy10grey" SortExpression="State"  ItemStyle-Width="7%" HeaderText="State" HeaderStyle-CssClass="buttonundlinelabel" >
                                <ItemTemplate>
                                    <%# Eval("State")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField ItemStyle-CssClass="copy10grey" SortExpression="Zip"  ItemStyle-Width="7%" HeaderText="Zip" HeaderStyle-CssClass="buttonundlinelabel" >
                                <ItemTemplate>
                                    <%# Eval("Zip")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField ItemStyle-CssClass="copy10grey" SortExpression="PoNum"  ItemStyle-Width="10%" HeaderText="Fulfillment#" HeaderStyle-CssClass="buttonundlinelabel" >
                                <ItemTemplate>
                                    <%# Eval("PoNum")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <%--<asp:TemplateField ItemStyle-CssClass="copy10grey" SortExpression="TrackingNumber"  ItemStyle-Width="10%" HeaderText="Tracking#" HeaderStyle-CssClass="buttonundlinelabel" >
                                <ItemTemplate>
                                    <%# Eval("TrackingNumber")%>
                                </ItemTemplate>
                            </asp:TemplateField>           --%>      
                            <asp:TemplateField ItemStyle-CssClass="copy10grey" SortExpression="Status"  ItemStyle-Width="7%" HeaderText="Status" HeaderStyle-CssClass="buttonundlinelabel" >
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "Status") %>
                                </ItemTemplate>
                            </asp:TemplateField>                 
                            <%--<asp:TemplateField ItemStyle-CssClass="copy10grey"  ItemStyle-Width="25%" HeaderText="Customer Comments">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "Comment")%>
                                </ItemTemplate>
                            </asp:TemplateField>                 
                            <asp:TemplateField ItemStyle-CssClass="copy10grey"  ItemStyle-Width="25%" HeaderText="AV Comments">
                                <ItemTemplate>
                                    <%# Eval("AVComments")%>
                                </ItemTemplate>
                            </asp:TemplateField>                 --%>
                            <asp:TemplateField ItemStyle-CssClass="copy10grey"  ItemStyle-Width="10%" HeaderText="">
                                <ItemTemplate>
                                     <table>
                                     <tr>
                                     
                                <td>
                               <%-- <asp:UpdatePanel ID="uPnl1"   runat="server" >
                                <ContentTemplate> OnCommand="imgViewRMA_Click" 

                               --%>  
                                    <asp:ImageButton ID="imgViewRMA"  ToolTip="View RMA" OnCommand="imgViewRMA_Command"
                                     CausesValidation="false" CommandArgument='<%# Eval("rmaGUID") %>' ImageUrl="~/Images/view.png"  runat="server" />

                                    <asp:ImageButton ID="imgView"  Visible="false" ToolTip="View RMA" OnCommand="imgViewRMA_Click"
                                     CausesValidation="false" CommandArgument='<%# Eval("rmaGUID") %>' ImageUrl="~/Images/view.png"  runat="server" />
                                <%--</ContentTemplate>
                                </asp:UpdatePanel>--%>
                                </td>
                                <td>

                                <asp:ImageButton ID="imgComments" AlternateText="View RMA Commnets"  ToolTip="View RMA Commnets" OnCommand="imgCComments_OnCommand"  CausesValidation="false" 
                                        CommandArgument='<%# Eval("rmaguid") + "," + Eval("rmaNumber") %>' ImageUrl="~/Images/comments.png"  runat="server" />
                            
                                     <%--<asp:ImageButton ID="imgCComments" AlternateText="View Customer Commnets"  ToolTip="View Customer Commnets" OnCommand="imgCComments_OnCommand"  CausesValidation="false" 
                                        CommandArgument='<%# Eval("rmaguid") + "," + Eval("rmaNumber") %>' ImageUrl="~/Images/ccomments.png"  runat="server" />
                        
                                </td>
                                <td>
                                     <asp:ImageButton ID="imgAComments" AlternateText="View AV Commnets"  ToolTip="View AV Commnets" OnCommand="imgAComments_OnCommand"  CausesValidation="false" 
                                        CommandArgument='<%# Eval("rmaguid") + "," + Eval("rmaNumber") %>' ImageUrl="~/Images/comments.png"  runat="server" />
                        --%>
                                </td>
                                <td>
                                    
                                                 <asp:ImageButton ID="imgHistory"  ToolTip="RMA History" OnCommand="imgRMAHistory_Click" 
                                                  CausesValidation="false" CommandArgument='<%# Eval("rmaGUID") %>' ImageUrl="~/Images/history.png"  runat="server" />
                                </td>
                                <%--<td>
                                    <asp:ImageButton ID="imgViewDetail"  ToolTip="View RMA Detail" OnCommand="imgViewRmaDetails_Click" 
                                     CausesValidation="false" CommandArgument='<%# Eval("rmaGUID") %>' ImageUrl="~/Images/view.png"  runat="server" />
                       
                                </td>--%>
                                <td>
                                    <div title='<%# "Error: " + Eval("DOCComment") %>' style=" height:20px; background-color :<%# Convert.ToString(Eval("DOCComment")) != "" ? "Red" : "" %>"  >
                                
                                        <asp:ImageButton ID="imgDoc"  ToolTip="RMA Document" Width="16" Height="16"  
                                        CommandArgument='<%# Eval("RMAGUID") %>'
                                        OnCommand="imgRMADoc_OnCommand"
                                        Visible='<%# Convert.ToString(Eval("RmaDocument"))=="" ? false: Convert.ToString(Eval("DOCComment")) != "" ? false : true %>'
                                        CausesValidation="false"  ImageUrl="~/Images/attach2.png"  runat="server" />
                                    </div>
                               
                                
                                </td>
                                         <td>
                                             
                            <asp:ImageButton ID="imgShip"  ToolTip="Generate label" OnCommand="imgShip_OnCommand"  CausesValidation="false" 
                            CommandArgument='<%# Eval("RMAGUID") %>' ImageUrl="~/Images/ship.png"  runat="server" />
                                         </td>
                                <td>
                        <asp:ImageButton  ToolTip="Edit RMA" CausesValidation="false" OnCommand="imgEditRMA_OnCommand" Visible="true"
                         CommandArgument='<%# Eval("RMAGUID") +","+ Eval("RMAUserCompany.companyid")+","+ Eval("ByPO")+","+ Eval("RmaSource")%>' ImageUrl="~/Images/edit.png" ID="imgEditRma"  runat="server" />
                        
                        <%--<a title="Edit RMA" href='NewRMAForm.aspx?rmaGUID=<%# Eval("RMAGUID") %>&companyID='>
                         <img src="/Images/edit.png" alt="" border="0"/>
                         </a>
                        --%>
                        
                        </td>
                        <td>
                        <asp:ImageButton ID="imgDel" runat="server"  CommandName="Delete" AlternateText="Delete RMA" ToolTip="Delete RMA" 
                        ImageUrl="~/images/delete.png" CommandArgument='<%# Eval("RMAGUID") %>' OnCommand="imgDeleteRMA_OnCommand" OnClientClick="return confirm('Do you want to delete this RMA?')" />
                        </td>
                        
                                </tr></table>
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
                </asp:Panel>
            </td>
        </tr>
        </table>
        </td></tr>
        <tr><td>&nbsp;</td></tr>
        
    </table>
     </ContentTemplate>
                <Triggers>
                        <asp:PostBackTrigger ControlID="btnExport" />
                        
                      <%--  <asp:PostBackTrigger ControlID="ddlCompany" />
                        <asp:PostBackTrigger ControlID="Button1" />
                        <asp:PostBackTrigger ControlID="Button2" />--%>
                 </Triggers>
            </asp:UpdatePanel>
               
    <%--</div>--%>

    <asp:UpdatePanel ID="upnlJsRunner" UpdateMode="Always" runat="server">
			<ContentTemplate>
				<asp:PlaceHolder ID="phrJsRunner" runat="server">
                
                </asp:PlaceHolder>
			</ContentTemplate>
		</asp:UpdatePanel>


        <table cellspacing="0" cellpadding="0" border="0" align="center" width="100%">
        <tr>
            <td>
                <foot:MenuFooter ID="Foter" runat="server"></foot:MenuFooter>
            </td>
        </tr>
        </table>
    <script type="text/javascript">
        generateRMADetail();
        
        
        displayStatus(0);
        
        
    </script>

        <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.4.0/jquery.min.js"></script>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/jquery-ui.min.js"></script>
        <script type="text/javascript" src="../JQuery/jquery.blockUI.js"></script>

    </form>
</body>
</html>
