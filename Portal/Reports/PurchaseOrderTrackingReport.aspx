<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PurchaseOrderTrackingReport.aspx.cs" Inherits="avii.Reports.PurchaseOrderTrackingReport" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="~/Controls/Header.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>.:: Fulfillment Shipment ::.</title>

    <link href="../aerostyle.css" rel="stylesheet" type="text/css"/>
    
    <!-- from http://encosia.com/2009/10/11/do-you-know-about-this-undocumented-google-cdn-feature/ -->
    <link href="https://ajax.googleapis.com/ajax/libs/jqueryui/1.8.11/themes/start/jquery-ui.css" type="text/css" rel="Stylesheet" />
	<%--<link href="../CSS/jquery-ui.css" rel="stylesheet" type="text/css"/>--%>
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
    
    <%--<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>--%>

    <script language="javascript" type="text/javascript">

        function Refresh() {
            //alert('refreshing..');
            var btnhdPrintlabel = document.getElementById("<%= btnhdPrintlabel.ClientID %>");
            btnhdPrintlabel.click();
        }

        function OpenPDF(base64data) {
            let pdfWindow = window.open("")
            pdfWindow.document.write("<iframe width='100%' height='100%' src='data:application/pdf;base64, " + encodeURI(base64data)+"'></iframe>")
        }

        function divexpandcollapse(divname) {
           // alert(divname);
            
            var div = document.getElementById(divname);
            var img = document.getElementById('img' + divname);

            if (div.style.display == "none") {

                if ($('#hdnchild').val() == "")
                    $('#hdnchild').val(divname);
                else
                    $('#hdnchild').val($('#hdnchild').val() + ',' + divname);

                div.style.display = "inline";
                img.src = "../Images/minus.gif";

            } else {

                var id = $('#hdnchild').val().replace(divname+ ',', '').replace(divname+ ',', '').replace(divname+ ',', '').replace(divname, '').replace(divname, '').replace(divname, '');
      //   //   alert(id)
            $('#hdnchild').val(id);

                div.style.display = "none";
                img.src = "../Images/plus.gif";

            }

        } 

        function divexpandcollapseChild(divname) {

            var div1 = document.getElementById(divname);

            var img = document.getElementById('img' + divname);

            if (div1.style.display == "none") {

                div1.style.display = "inline";

                img.src = "../Images/minus.gif";

            } else {

                div1.style.display = "none";

                img.src = "../Images/plus.gif"; ;

            }

        }

    </script>
    <script type="text/javascript">
        function PlusMinusClick(obj)
            {
            if ($('#hdnchild').val() != "") {
                //alert('u123');
                var childs = $('#hdnchild').val().split(',');
                //alert(childs);

                if (childs.length > 0) {
                    for (var i = 0; i < childs.length; i++) {
                        //$("#gvPO").find('div#gvPO_pnlTracking_' + childs[i]).siblings('img').click();    
                        //$("#" + childs[i]).click(); 
                        $("#" + childs[i]).attr("src", "../images/plus.png");
                        $("#" + childs[i]).closest("tr").next().remove();
                    }
                }
            }
            $('#hdnchild').val('');

            $(obj).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(obj).next().html() + "</td></tr>")
            $(obj).attr("src", "../images/minus.png");
            if ($('#hdnchild').val() == "")
                $('#hdnchild').val(obj.id);
            else
                $('#hdnchild').val($('#hdnchild').val() + ',' + obj.id);
        }
      //  $("[src*=plus]").live("click", function () {
      //      // alert('plus');
      //      //alert(this.id)
      //      //if ($('#hdnchild').val() != "") {
      //      //    //alert('u123');
      //      //    var childs = $('#hdnchild').val().split(',');
      //      //    alert(childs);
      //      //    if (childs.length > 0) {
      //      //        for (var i = 0; i < childs.length; i++) {
      //      //            //$("#gvPO").find('div#gvPO_pnlTracking_' + childs[i]).siblings('img').click();    
      //      //            //$("#" + childs[i]).click(); 
      //      //            $("#" + childs[i]).attr("src", "../images/plus.png");
      //      //            $("#" + childs[i]).closest("tr").next().remove();
      //      //        }
      //      //    }
      //      //}
      //      //$('#hdnchild').val('');

      //      $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>")
      //      $(this).attr("src", "../images/minus.png");
      //      if ($('#hdnchild').val() == "")
      //          $('#hdnchild').val(this.id);
      //      else
      //          $('#hdnchild').val($('#hdnchild').val() + ',' + this.id);
      //      //var row = $(this).siblings("div").attr('id').split('_');
      //    //  alert(row);
      //  // $('#hdnchild').val($('#hdnchild').val().replace(row[row.length - 1] + ',', ''));    
      //   //   $('#hdnchild').val($('#hdnchild').val() + row[row.length - 1] + ',');
      //      //alert($('#hdnchild').val());
      //  });
      //  $("[src*=minus]").live("click", function () {
      //    //  alert($('#hdnchild').val());
      //      $(this).attr("src", "../images/plus.png");
      //    //  var id = $('#hdnchild').val().replace(this.id+ ',', '').replace(this.id+ ',', '').replace(this.id+ ',', '').replace(this.id, '').replace(this.id, '').replace(this.id, '');
      //   //   alert(id)
      //  //    $('#hdnchild').val(id);
      //      //var row = $(this).siblings("div").attr('id').split('_');    
      ////   $('#hdnchild').val($('#hdnchild').val().replace(row[row.length - 1] + ',', '')); 
      //      $(this).closest("tr").next().remove();
      //  });

        $(document).ready(function () {    
            if ($('#hdnchild').val() != "") { 
             //alert('u1');
             var childs = $('#hdnchild').val().split(',');    
             if (childs.length > 0) {    
                 for (var i = 0; i < childs.length; i++) {    
                     //$("#gvPO").find('div#gvPO_pnlTracking_' + childs[i]).siblings('img').click();    
                     $("#"+childs[i]).click(); 
                 }    
             }    
         }    
     });    
    </script>
	<script type="text/javascript">

	    $(document).ready(function () {
	        $("#divESN").dialog({
	            autoOpen: false,
	            modal: true,
	            minHeight: 400,
	            height: 450,
	            width: 800,
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

            $("#divSKU").dialog({
                autoOpen: false,
                modal: false,
                minHeight: 20,
                height: 450,
                width: 950,
                resizable: false,
                open: function (event, ui) {
                    $(this).parent().appendTo("#divContainer");
                }
            });

            $("#divShip").dialog({
                autoOpen: false,
                modal: false,
                minHeight: 20,
                height: 450,
                width: 850,
                resizable: false,
                open: function (event, ui) {
                    $(this).parent().appendTo("#divContainer");
                }
            });


	    });

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

	        unblockLabelDialog();
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

        function closeSKUDialog() {
            //Could cause an infinite loop because of "on close handling"
            $("#divSKU").dialog('close');
        }

        function openSKUDialog(title, linkID) {

            var pos = $("#" + linkID).position();
            var top = pos.top;
            var left = pos.left + $("#" + linkID).width() + 10;
            //alert(top);
            if (top > 600)
                top = 10;
            else
                top = 10;
            //top = top - 600;
            left = 280;
            $("#divSKU").dialog("option", "title", title);
            $("#divSKU").dialog("option", "position", [left, top]);

            $("#divSKU").dialog('open');

            unblockSKUDialog();
        }


        function openSKUDialogAndBlock(title, linkID) {
            openSKUDialog(title, linkID);

            //block it to clean out the data
            $("#divSKU").block({
                message: '<img src="../images/async.gif" />',
                css: { border: '0px' },
                fadeIn: 0,
                //fadeOut: 0,
                overlayCSS: { backgroundColor: '#ffffff', opacity: 1 }
            });
        }

        function unblockSKUDialog() {
            $("#divSKU").unblock();
        }

        function closeShipDialog() {
            //Could cause an infinite loop because of "on close handling"
            $("#divShip").dialog('close');
        }

        function openShipDialog(title, linkID) {

            var pos = $("#" + linkID).position();
            var top = pos.top;
            var left = pos.left + $("#" + linkID).width() + 10;
            //alert(top);
            if (top > 600)
                top = 10;
            else
                top = 10;
            //top = top - 600;
            left = 330;
            $("#divShip").dialog("option", "title", title);
            $("#divShip").dialog("option", "position", [left, top]);

            $("#divShip").dialog('open');

            unblockShipDialog();
        }


        function openShipDialogAndBlock(title, linkID) {
            openShipDialog(title, linkID);

            //block it to clean out the data
            $("#divShip").block({
                message: '<img src="../images/async.gif" />',
                css: { border: '0px' },
                fadeIn: 0,
                //fadeOut: 0,
                overlayCSS: { backgroundColor: '#ffffff', opacity: 1 }
            });
        }

        function unblockShipDialog() {
            $("#divShip").unblock();
        }

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
	    

	    function set_focus1() {
	        var img = document.getElementById("imgFromtDate");
	        var st = document.getElementById("btnSearch");
	        st.focus();
	        img.click();
	    }
	    function set_focus2() {
	        var img = document.getElementById("imgToDate");
	        var st = document.getElementById("btnSearch");
	        st.focus();
	        img.click();
	    }

	    
	    function Validate() {
	        //if (flag == '1' || flag == '2') {
	        var company = document.getElementById("<% =dpCompany.ClientID %>");
	        //alert(company);
	        if (company != 'null' && company.selectedIndex == 0) {
	            alert('Customer is required!');
	            return false;

            }

            var fromDate = document.getElementById("<% =txtFromDate.ClientID %>").value;
            var toDate = document.getElementById("<% =txtToDate.ClientID %>").value;
            var poNum = document.getElementById("<% =txtPoNum.ClientID %>").value;
            if (fromDate == "" && toDate == "" && poNum == "") {
                if (company != 'null' && company.selectedIndex > 0)
                    alert('Please select second search criteria!');
                else
                    alert('Please select search criteria!');

                return false;
            }
            return true;

	        //}
        }
        </script>
		<div id="Div1" runat="server"> 
	<script type="text/javascript">    
        function PrintDiv() {
            var divContents = document.getElementById("divLabelImg").innerHTML;
            var printWindow = window.open('', '', 'height=450,width=800');
            printWindow.document.write('<html><head><title></title>');
            printWindow.document.write('</head><body >');
            printWindow.document.write(divContents);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            printWindow.print();
        }
	</script>
    </div>
        <link rel="stylesheet"  type="text/css" href="../fullfillment/calendar/dhtmlgoodies_calendar.css" media="screen" />
		<script type="text/javascript" src="../fullfillment/calendar/dhtmlgoodies_calendar.js"></script>  
		
</head>
<body leftmargin="0" topmargin="0" marginwidth="0" marginheight="0">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    
    <table cellSpacing="0" cellPadding="0" align="center" width="100%" border="0">
        <tr>
			<td><head:MenuHeader id="HeadAdmin" runat="server"></head:MenuHeader></td>
		</tr>
		<%--<tr><td>&nbsp;</td></tr>--%>
		<tr>
			<td  bgcolor="#dee7f6" class="buttonlabel">
            &nbsp;&nbsp;Fulfillment Shipment
			</td>
		</tr>
    
    </table>
    <div id="divContainer">	
            <div id="divShip"  style="display:none">
            <asp:UpdatePanel ID="upShip" runat="server">
				<ContentTemplate>
                    <table width="100%">
                        <tr>
                            <td align="right">
                                <asp:Button ID="btnShipClose" runat="server" Text=" Close " CssClass="button" OnClientClick="closeShipDialog()" 
                                CausesValidation="false" />  
                                <asp:Button ID="btnShipDownload" runat="server" Text=" Download " CssClass="button" OnClick="btnShipDownload_Click" 
                                CausesValidation="false" />  
                    
                            </td>
                        </tr>
                        <tr>
                            <td>

                    <asp:Repeater ID="rptShip" runat="server" OnItemDataBound="rptShip_ItemDataBound">
                        <HeaderTemplate>
                        <table width="100%" align="center">
                            <tr>
                                <td class="buttonlabel" width="2%">
                                    &nbsp;S.No.
                                </td>                                
                                <td class="buttonlabel" width="40%">
                                    &nbsp;Ship Method
                                </td>
                                <td class="buttonlabel" width="45%">
                                    &nbsp;Package Type
                                </td>
                                <td class="buttonlabel" width="10%">
                                    &nbsp;Cost($)
                                </td>
                            </tr>                            
                        </HeaderTemplate>
                        <ItemTemplate>
                        <tr class="<%# Container.ItemIndex % 2 == 0 ? "alternaterow" : "" %>">
                                                
                            <td class="copy10grey">
                                    &nbsp;<%# Container.ItemIndex + 1 %>
                            </td>
                                
                            <td class="copy10grey">
                                    &nbsp;<%# Eval("ShipMethod")%>
                            </td>
                            <td class="copy10grey">
                                    &nbsp;<%# Eval("ShipPackage")%>
                            </td>
                            <td class="copy10grey" align="right" >
                                $<asp:Label ID="lblCost" runat="server" Text='<%# Eval("Cost", "{0:#,##0.00}")%>' CssClass="copy10grey"></asp:Label>    
                                &nbsp;
                            </td>
                                
                        </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            <tr >
                                                
                            <td class="copy10grey">
                                    
                            </td>
                                
                            <td class="copy10grey">
                                    
                            </td>
                            <td class="copy10grey" align="right">
                                    <strong>TOTAL COST($): </strong>
                            </td>
                            <td class="copy10grey" align="right" >
                                $<asp:Label ID="lblTotalCost" runat="server" CssClass="copy10grey"></asp:Label>
                                &nbsp;
                                    <%--$<%# Eval("Cost")%>&nbsp;--%>
                            </td>
                                
                        </tr>
                        
                        </table>    
                        </FooterTemplate>
                        </asp:Repeater>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
            </div>
            <div id="divSKU"  style="display:none">
            <asp:UpdatePanel ID="upSKU" runat="server">
				<ContentTemplate>
                    <table width="100%">
                        <tr>
                            <td align="right">
                                <asp:Button ID="btnSKUClose" runat="server" Text=" Close " CssClass="button" OnClientClick="closeSKUDialog()" 
                                CausesValidation="false" />  
                                <asp:Button ID="btnSKUDownload" runat="server" Text=" Download " CssClass="button" OnClick="btnSKUDownload_Click" 
                                CausesValidation="false" />  
                    
                            </td>
                        </tr>
                        <tr>
                            <td>

                                <asp:Repeater ID="rptSKU" runat="server">
                                <HeaderTemplate>
                                <table width="100%" align="center">
                                    <tr>
                                        <td class="buttonlabel" width="2%">
                                            &nbsp;S.No.
                                        </td>
                                        <td class="buttonlabel" width="17%">
                                            &nbsp;Category Name
                                        </td>
                                        <td class="buttonlabel" width="35%">
                                            &nbsp;SKU
                                        </td>
                                        <td class="buttonlabel" width="38%">
                                            &nbsp;Product Name
                                        </td>
                                        <td class="buttonlabel" width="8%">
                                            &nbsp;Count
                                        </td>
                                    </tr>                            
                                </HeaderTemplate>
                                <ItemTemplate>
                                <tr class="<%# Container.ItemIndex % 2 == 0 ? "alternaterow" : "" %>">
                                                
                                    <td class="copy10grey">
                                            &nbsp;<%# Container.ItemIndex + 1 %>
                                    </td>
                                
                                    <td class="copy10grey">
                                            &nbsp;<%# Eval("CategoryName")%>
                                    </td>
                                    <td class="copy10grey">
                                            &nbsp;<%# Eval("SKU")%>
                                    </td>
                                    <td class="copy10grey">
                                            &nbsp;<%# Eval("ProductName")%>
                                    </td>
                                    <td class="copy10grey" align="right" >
                                            <%# Eval("Count")%>&nbsp;
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
                        <asp:Image ID="imgLabel" ImageUrl="~/warning.gif" Height="100%"  runat="server" />
                    </div>
                    </ContentTemplate>
            </asp:UpdatePanel>
            </div>
			<div id="divESN" style="display:none">
					
				<asp:UpdatePanel ID="upnlESN" runat="server">
					<ContentTemplate>
                   
						<asp:PlaceHolder ID="phrESN" runat="server">
                        <table width="100%" border="0">
                                <tr>
                                    <td class="copy10grey" width="20%" align="left">
                                        <b> &nbsp;Fulfillment#:</b>
                                    </td>
                                    <td align="left" >
                                     <asp:Label ID="lblPO" runat="server" CssClass="copy10grey"></asp:Label>               
                                    </td>
                                </tr>
                                
                                <tr>
                                    <td class="copy10grey" width="20%" align="left">
                                        <b> &nbsp;Tracking#:</b>
                                    </td>
                                    <td align="left" >
                                     <asp:Label ID="lblTracking" runat="server" CssClass="copy10grey"></asp:Label>               
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                
                                    <asp:Panel ID="pnlESN" runat="server" Width="100%">
                                        <asp:Label ID="lblEsnMsg" runat="server" Width="100%" CssClass="errormessage"></asp:Label>
                                         <asp:Repeater ID="rptESN" runat="server">
                                            <HeaderTemplate>
                                            <table width="100%" align="center">
                                                <tr >
                                                    <td class="buttonlabel" width="1%">
                                                        &nbsp;S.No.
                                                    </td>
                                                    <td class="buttonlabel" width="22%">
                                                        &nbsp;SKU
                                                    </td>
                                                    <td class="buttonlabel" width="1%">
                                                        &nbsp;Qty
                                                    </td>
                                                    <td class="buttonlabel" width="20%">
                                                        &nbsp;ESN
                                                    </td>
                                                    <td class="buttonlabel" width="16%">
                                                        &nbsp;Batch Number
                                                    </td>
                                                    <td class="buttonlabel" width="20%">
                                                        &nbsp;ICC_ID
                                                    </td>
                                                    <td runat="server" visible='<%# ContainerID == "" ? false:true%>' class="buttonlabel" width="20%">
                                                        &nbsp;ContainerID
                                                    </td>
                                                </tr>
                            
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                            <tr class="<%# Container.ItemIndex % 2 == 0 ? "alternaterow" : "" %>">
                                                
                                                <td class="copy10grey">
                                                        &nbsp;<%# Container.ItemIndex + 1 %>
                                                </td>
                                
                                                <td class="copy10grey">
                                                        &nbsp;<%# Eval("SKU")%>
                                                </td>
                                                <td class="copy10grey">
                                                        &nbsp;<%# Convert.ToString(Eval("ESN"))==""? Eval("Qty") : 1 %>
                                                </td>
                                                <td class="copy10grey">
                                                        &nbsp;<%# Eval("ESN")%>
                                                </td>
                                                <td class="copy10grey">
                                                        &nbsp;<%# Eval("BatchNumber")%>
                                                </td>
                                                <td class="copy10grey">
                                                        &nbsp;<%# Eval("ICC_ID")%>
                                                </td>
                                                <td class="copy10grey" runat="server" visible='<%# Convert.ToString(Eval("ContainerID")) == "" ? false:true%>' >
                                                        &nbsp;<%# Eval("ContainerID")%>
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
    <asp:UpdatePanel ID="upnlCustomers" UpdateMode="Conditional" runat="server">
	<ContentTemplate>
	<asp:HiddenField ID="hdnchild" runat="server" />
    <table cellSpacing="0" cellPadding="0" align="center" width="95%" border="0">
    		<tr>                    
                <td >
                    <asp:Label ID="lblMsg" runat="server" Width="100%" CssClass="errormessage"></asp:Label></td>
            </tr> 
            <tr><td class="copy10grey" align="left">&nbsp;
                        <%--- ESN of inprocess RMA will be reflected fullfillment shipped count & RMA count.<br />&nbsp;
	                    - Unused ESN count equal to new plus approved RMA ESN. <br />&nbsp;--%>
                        </td></tr>  
     </table>  
                      
     <table bordercolor="#839abf" border="1" width="95%" align="center" cellpadding="0" cellspacing="0">
      <tr>
        <td>
         <asp:Panel  ID="pnlSearch" runat="server"  DefaultButton="btnSearch" >
      <table width="100%" border="0" class="box" align="center" cellpadding="2" cellspacing="2">
         
            <tr valign="top">
                <td class="copy10grey" align="right" width="15%">
                    Customer Name:
                </td>
                <td width="35%" align="left">
                &nbsp;<asp:DropDownList ID="dpCompany" runat="server" AutoPostBack="false"   Width="60%"
                 class="copy10grey"  >
                </asp:DropDownList>  
                <asp:Label ID="lblCompany" runat="server" CssClass="copy10grey" ></asp:Label>
                </td>
                <td  width="1%">
                    &nbsp;
                </td>
                <td class="copy10grey" align="right" width="15%">
                    Fulfillment#:
                
                </td>
                <td class="copy10grey" align="left" width="35%">
                    &nbsp;<asp:TextBox ID="txtPoNum" CssClass="copy10grey" Width="60%" runat="server"></asp:TextBox>
          
                </td>

            </tr>    
            <tr>
                <td class="copy10grey" align="right" width="15%">
                   Shipment From Date:
                
                
                </td>
                <td align="left" width="35%">
                    &nbsp;<asp:TextBox ID="txtFromDate" runat="server" onfocus="set_focus1();"  CssClass="copy10grey" MaxLength="15"  Width="60%"></asp:TextBox>
                    <img id="imgFromtDate" alt="" onclick="document.getElementById('<%=txtFromDate.ClientID%>').value = ''; displayCalendar(document.getElementById('<%=txtFromDate.ClientID%>'),'mm/dd/yyyy',this,true);" src="../fullfillment/calendar/sscalendar.jpg" />
                </td>
                <td  width="1%">
                    &nbsp;
                </td>
                    <td class="copy10grey" align="right" width="15%">Shipment To Date:</td>
                
                <td align="left" width="35%">
                    &nbsp;<asp:TextBox ID="txtToDate" runat="server" onfocus="set_focus2();"  CssClass="copy10grey" MaxLength="15"  Width="60%"></asp:TextBox>
                    <img id="imgToDate"  alt="" onclick="document.getElementById('<%=txtToDate.ClientID%>').value = ''; displayCalendar(document.getElementById('<%=txtToDate.ClientID%>'),'mm/dd/yyyy',this,true);" src="../fullfillment/calendar/sscalendar.jpg" />
                
                </td>               
                </tr>
           
                
            
            <tr>
            <td colspan="5">
                <hr />
                </td>
                </tr>
                <tr>
                <td  align="center"  colspan="5">
                    
                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="button"    OnClick="btnSearch_Click" OnClientClick="return Validate();" CausesValidation="false"/>
                     &nbsp;
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="button" OnClick="btnCancel_Click" CausesValidation="false"/>
                    &nbsp;
                    <asp:Button ID="btnDownload" runat="server" Text=" Download " CssClass="button" OnClick="btnDownload_Click" 
                    CausesValidation="false" />  
                 <%--   &nbsp;<asp:Button ID="btnSKU"  runat="server" Visible="False" CssClass="button" Text="SKUs Shipped" OnClick="btnSKU_Click" OnClientClick="openSKUDialogAndBlock('SKUs Shipped','btnSKU')" />
                     &nbsp;<asp:Button ID="btnShipMethods"  runat="server" Visible="False" CssClass="button" Text="Ship Methods" 
                         OnClick="btnShipMethods_Click" OnClientClick="openShipDialogAndBlock('Ship Methods','btnShipMethods')" />--%>

        </td>
        </tr>
        </table>
        </asp:Panel>
        </td>
        </tr>
        </table>
   

            <table align="center" style="text-align:left" width="95%">
                             <tr>
                                <td  align="left" style="height:8px; vertical-align:bottom">
                                    <asp:LinkButton ID="lnkSKUSumary"  Visible="false" CssClass="copy11link"  OnClick="btnSKU_Click" 
                                        OnClientClick="openSKUDialogAndBlock('SKUs Shipped','lnkSKUSumary')" runat="server" Text="SKUs Shipped"></asp:LinkButton>
             &nbsp;
                                    <asp:LinkButton ID="lnkShipSummary"  Visible="false" CssClass="copy11link"  OnClick="btnShipMethods_Click" 
                                        OnClientClick="openShipDialogAndBlock('Ship Methods','lnkShipSummary')" runat="server" Text="Ship Methods"></asp:LinkButton>
                                    
                                </td>
                                

                                <td  align="right" style="height:8px; vertical-align:bottom">
                                
                                <strong>   
                                    <asp:Label ID="lblCount" CssClass="copy10grey" runat="server" ></asp:Label> &nbsp;&nbsp;
                                </strong> 
                                </td>
                             </tr>

                             <tr>
                                <td colspan="2" align="center">
                                    <div style="display:none">
                                    <asp:Button ID="btnhdPrintlabel" runat="server"   Text="Printlabel" OnClick="btnhdPrintlabel_Click" /> 
                                    </div>
    
                                    <asp:GridView ID="gvPO" runat="server" Width="100%" GridLines="Both"   AutoGenerateColumns="false"
                                    OnPageIndexChanging="gridView_PageIndexChanging" PageSize="50" AllowPaging="true" 
                                     DataKeyNames="PO_ID"  OnRowDataBound="OnRowDataBound" AllowSorting="true" OnSorting="gvPO_Sorting">
                                        <RowStyle BackColor="Gainsboro" />
                                        <AlternatingRowStyle BackColor="white" />
                                        <HeaderStyle  CssClass="buttonlabel" ForeColor="white"/>
                                        <FooterStyle CssClass="white"  />
                                        <PagerStyle ForeColor="#636363" CssClass="copy10grey" HorizontalAlign="Left" />
                                        <Columns>
                                            <%--<asp:TemplateField HeaderText="S.No." ItemStyle-HorizontalAlign="Right"  ItemStyle-CssClass="copy10grey" ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                <%# Container.DataItemIndex +  1 %>
                                                
                                                </ItemTemplate>
                                                
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField ItemStyle-Width="1%">
                                            <ItemTemplate>
                                                <a href="JavaScript:divexpandcollapse('div<%# Container.DataItemIndex +  1 %>');">
                                                    <img id="imgdiv<%# Container.DataItemIndex +  1 %>" width="9px" border="0" 
                                                                                                 src="../Images/plus.png" alt="" /></a>                       
                                            </ItemTemplate>
                                            <ItemStyle Width="20px" VerticalAlign="Middle"></ItemStyle>
                                        </asp:TemplateField>
<%--                                            <asp:TemplateField ItemStyle-Width="1%" >
                                            <ItemTemplate>
                                                    <img alt = "" style="cursor: pointer" onclick="PlusMinusClick(this)" src="../images/plus.png" id="img<%# Container.DataItemIndex +  1 %>" />
                                                    <asp:Panel ID="pnlTracking" runat="server" Style="display: none">
                                                    <asp:GridView ID="gvTracking" runat="server" AutoGenerateColumns="false" 
                                                        DataKeyNames="PO_ID"  Width="100%" OnRowDataBound="gvTracking_RowDataBound">
                                                        <RowStyle BackColor="Gainsboro" />
                                                        <AlternatingRowStyle BackColor="white" />
                                                        <HeaderStyle  CssClass="buttonlabel" ForeColor="white"/>
                                                        <FooterStyle CssClass="white"  />
                                                        <PagerStyle ForeColor="#636363" CssClass="copy10grey" HorizontalAlign="Left" />
                                                        <Columns>
                                                          
                                                            <asp:TemplateField HeaderText="Tracking Number" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                                                <ItemTemplate><%#Eval("TrackingNumber")%></ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Ship Date" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                                                <ItemTemplate><%# Convert.ToString(Eval("ShipDate")) == "1/1/0001"?"":Eval("ShipDate") %></ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Shipment Type" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                                                <ItemTemplate><%#Eval("ShipmentType")%></ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Acknowledgment Sent Date" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                                                <ItemTemplate><%#Eval("AcknowledgmentSent")%></ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="3%">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ToolTip="View ESN" 
                                                        CausesValidation="false" OnCommand="imgViewESN_Click" CommandArgument='<%# Eval("PO_ID") + "," + Eval("TrackingNumber") %>'  
                                                        ID="lnkESN"  runat="server"  >
                                                        SKU 
                                                        
                                                        </asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="3%">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="imgPrint" runat="server" CausesValidation="false" 
                                                                    CommandArgument='<%# Eval("LineNumber") %>'  OnCommand="imgPrint_Command" ToolTip="Print Label" AlternateText="View Label" 
                                                                    ImageUrl="~/images/printer.png" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="3%">
                                                                <ItemTemplate>
                                                                   <asp:LinkButton ToolTip="Generate Label" 
                                                        CausesValidation="false" OnCommand="lnkGenerateLabel_Click" CommandArgument='<%# Eval("PO_ID") %>'  
                                                        ID="lnkLabel"  runat="server"  >
                                                        Generate Label 
                                                        
                                                        </asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </asp:Panel>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                            <%--<asp:BoundField ItemStyle-Width="150px" DataField="PO_NUM" HeaderText="Fulfillment#" />
                                            <asp:BoundField ItemStyle-Width="150px" DataField="PO_Date" HeaderText="Fulfillment Date" />
                                            <asp:BoundField ItemStyle-Width="150px" DataField="Ship_Via" HeaderText="Ship Method" />--%>
                                            <asp:TemplateField HeaderText="S.No." ItemStyle-CssClass="copy10grey"  ItemStyle-Width="1%" HeaderStyle-CssClass="buttonlabel">
                                                <ItemTemplate>

                                                        <%# Container.DataItemIndex + 1%>
                  
                                                </ItemTemplate>
                                            </asp:TemplateField> 
                                            <asp:TemplateField HeaderText="Fulfillment Type" ItemStyle-CssClass="copy10grey"  ItemStyle-Width="12%" HeaderStyle-CssClass="buttonlabel">
                                                <ItemTemplate>
                                                        <%# Eval("PoType") %>
                                                </ItemTemplate>
                                            </asp:TemplateField> 

                                            <asp:TemplateField HeaderText="Fulfillment#" SortExpression="PO_NUM" HeaderStyle-CssClass="buttonundlinelabel" ItemStyle-HorizontalAlign="Left"  ItemStyle-CssClass="copy10grey" ItemStyle-Width="25%">
                                                <ItemTemplate><%#Eval("PO_NUM")%></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Fulfillment Date" SortExpression="PO_Date" HeaderStyle-CssClass="buttonundlinelabel"  ItemStyle-HorizontalAlign="Left" FooterStyle-HorizontalAlign="Right"  ItemStyle-CssClass="copy10grey" FooterStyle-CssClass="copy10grey" ItemStyle-Width="25%">
                                                <ItemTemplate>
                                                    <%# Convert.ToDateTime(Eval("PO_Date")).ToShortDateString() %>
                                                </ItemTemplate>
                                            </asp:TemplateField> 
                                            
                                            <asp:TemplateField HeaderText="Contact Name" SortExpression="ContactName" HeaderStyle-CssClass="buttonundlinelabel"  ItemStyle-HorizontalAlign="Left" FooterStyle-HorizontalAlign="Right"  ItemStyle-CssClass="copy10grey" FooterStyle-CssClass="copy10grey" ItemStyle-Width="25%">
                                                <ItemTemplate>
                                                <%#Eval("ContactName")%>
                                                </ItemTemplate>
                                                
                                            </asp:TemplateField> 
                                            
                                            <asp:TemplateField HeaderText="Ship Method" SortExpression="Ship_Via" HeaderStyle-CssClass="buttonundlinelabel"  ItemStyle-HorizontalAlign="Left" FooterStyle-HorizontalAlign="Right"  ItemStyle-CssClass="copy10grey" FooterStyle-CssClass="copy10grey" ItemStyle-Width="12%">
                                                <ItemTemplate>
                                                <%#Eval("Ship_Via")%>
                                                </ItemTemplate>
                                                
                                            </asp:TemplateField> 
                                             
                                            <asp:TemplateField>
                                            <ItemTemplate>
                                            <tr>
                                                <td colspan="100%">
                                                <div id="div<%# Container.DataItemIndex +  1 %>"  style="overflow:auto; display:none;
                                                                    position: relative; left: 15px; overflow: auto">
                                                    <asp:GridView ID="gvTracking" runat="server" AutoGenerateColumns="false" 
                                                        DataKeyNames="PO_ID"  Width="98%" OnRowDataBound="gvTracking_RowDataBound">
                                                        <RowStyle BackColor="Gainsboro" />
                                                        <AlternatingRowStyle BackColor="white" />
                                                        <HeaderStyle  CssClass="buttonlabel" ForeColor="white"/>
                                                        <FooterStyle CssClass="white"  />
                                                        <PagerStyle ForeColor="#636363" CssClass="copy10grey" HorizontalAlign="Left" />
                                                        <Columns>
                                                            <%--<asp:BoundField ItemStyle-Width="150px" DataField="TrackingNumber" HeaderText="TrackingNumber" />
                                                            <asp:BoundField ItemStyle-Width="150px" DataField="ShipDate" HeaderText="ShipDate" />
                                                            <asp:BoundField ItemStyle-Width="150px" DataField="ShipmentType" HeaderText="ShipmentType" />
                                                            <asp:BoundField ItemStyle-Width="150px" DataField="AcknowledgmentSent" HeaderText="AcknowledgmentSent" />--%>

                                                            <asp:TemplateField HeaderText="Tracking Number" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                                                <ItemTemplate><%#Eval("TrackingNumber")%></ItemTemplate>
                                                            </asp:TemplateField>
                                                            
                                                            <asp:TemplateField HeaderText="Ship Date" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                                                <ItemTemplate><%# Convert.ToString(Eval("ShipDate")) == "1/1/0001"?"":Eval("ShipDate") %></ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Shipment Type" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                                                <ItemTemplate><%#Eval("ShipmentType")%></ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Acknowledgment Date" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                                                <ItemTemplate><%#Eval("AcknowledgmentSent")%></ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Ship Method" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                                                <ItemTemplate><%#Eval("ShipMethod")%></ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Ship Package" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                                                <ItemTemplate><%#Eval("ShipPackage")%></ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Weight" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                                                <ItemTemplate><%#Eval("ShipWeight")%></ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Price" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                                                <ItemTemplate>$<%#Eval("ShipPrice")%></ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Line Items" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="copy10grey" ItemStyle-Width="3%">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ToolTip="View ESN" 
                                                                    CausesValidation="false" OnCommand="imgViewESN_Click" CommandArgument='<%# Eval("PO_ID") + "," + Eval("TrackingNumber") %>'  
                                                                    ID="lnkESN"  runat="server"  >
                                                                    Line Items 
                                                        
                                                                    </asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Shipment Label" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="copy10grey" ItemStyle-Width="3%">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="imgPrint" runat="server" CausesValidation="false" 
                                                                    CommandArgument='<%# Eval("LineNumber") %>'  OnCommand="imgPrint_Command" ToolTip="Print Label" AlternateText="View Label" 
                                                                    ImageUrl="~/images/printer.png" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ESN Label" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="copy10grey" ItemStyle-Width="3%">
                                                                <ItemTemplate>
                                                                    <a title="" target="_blank" href="../downloadlabel.aspx?tr=<%#Eval("TrackingNumber")%>">ESN Label</a>
                                                                   <%--<asp:LinkButton ToolTip="Generate Label" 
                                                        CausesValidation="false" OnCommand="lnkGenerateLabel_Click" CommandArgument='<%# Eval("PO_ID") %>'  
                                                        ID="lnkLabel"  runat="server"  >
                                                        Generate Label 
                                                        
                                                        </asp:LinkButton>--%>
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
                                    
                                    

                                </td>
                            </tr>
                            </table>
            </ContentTemplate>
            <Triggers>
                
                <asp:PostBackTrigger ControlID="btnSKUDownload" />
                <asp:PostBackTrigger ControlID="btnShipDownload" />
                <asp:PostBackTrigger ControlID="btnDownload" />
                <asp:PostBackTrigger ControlID="btnhdPrintlabel" />
            </Triggers>
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
        <br /> <br />
            <br /> <br />
        <table width="100%">
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
        
        var prm = Sys.WebForms.PageRequestManager.getInstance();
            if (prm != null) {
                prm.add_endRequest(function (sender, e) {
                    if (sender._postBackSettings.panelsToUpdate != null) {
                        
                        if ($('#hdnchild').val() != "") {
                            //alert($('#hdnchild').val());
                            var childs = $('#hdnchild').val().split(',');
                          // alert(childs);
                            //alert(childs.length)
                            if (childs.length > 0) {
                                for (var i = 0; i < childs.length; i++) {
                                    if (childs[i] != '')
                                        var div = document.getElementById(childs[i]);
                                        var img = document.getElementById('img' + childs[i]);
                                        div.style.display = "inline";
                                        img.src = "../Images/minus.gif";

            
                                        //  $("#"+childs[i]).click(); 
                                        //$("#gvPO").find('div#gvPO_pnlTracking_' + childs[i]).siblings('img').click();
                                }
                            }
                        }
                    }
                });
            };
    </script>
    </form>
</body>
</html>
