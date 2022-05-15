<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FulfillmentDetails.aspx.cs" Inherits="avii.FulfillmentDetails" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>
<%@ Register TagPrefix="menu" TagName="Menu" Src="~/Controls/Header.ascx" %>
<%@ Register TagPrefix="UC" TagName="Comments" Src="~/Controls/FulfillmentComments.ascx" %>
<%--<%@ Register TagPrefix="UC" TagName="PoLog" Src="~/Controls/UCPOLog.ascx" %>--%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Fulfillment</title>
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
	<style>
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
   
    <script type="text/javascript">        
        function ShowLoading() {

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

        
       $(document).ready(function () {

           $("#divRequest").dialog({
               autoOpen: false,
               modal: false,
               minHeight: 20,
               height: 550,
               width: 1150,
               resizable: false,
               open: function (event, ui) {
                   $(this).parent().appendTo("#divContainer");
               }
           });

           $("#divResponse").dialog({
               autoOpen: false,
               modal: false,
               minHeight: 20,
               height: 550,
               width: 1150,
               resizable: false,
               open: function (event, ui) {
                   $(this).parent().appendTo("#divContainer");
               }
           });
       });


       function closeRequestDialog() {
           //Could cause an infinite loop because of "on close handling"
           $("#divRequest").dialog('close');
       }



       function openRequestDialog(title, linkID) {

           var pos = $("#" + linkID).position();
           var top = pos.top;
           var left = pos.left + $("#" + linkID).width() + 10;
           //alert(top);
           if (top > 600)
               top = 10;
           else
               top = 10;
           //top = top - 600;
           left = 130;
           $("#divRequest").dialog("option", "title", title);
           $("#divRequest").dialog("option", "position", [left, top]);

           $("#divRequest").dialog('open');

           unblockRequestDialog();
       }


       function openRequestDialogAndBlock(title, linkID) {
           openRequestDialog(title, linkID);

           //block it to clean out the data
           $("#divRequest").block({
               message: '<img src="../images/async.gif" />',
               css: { border: '0px' },
               fadeIn: 0,
               //fadeOut: 0,
               overlayCSS: { backgroundColor: '#ffffff', opacity: 1 }
           });
       }

       function unblockRequestDialog() {
           $("#divRequest").unblock();
       }


       function closeResponseDialog() {
           //Could cause an infinite loop because of "on close handling"
           $("#divResponse").dialog('close');
       }



       function openResponseDialog(title, linkID) {

           var pos = $("#" + linkID).position();
           var top = pos.top;
           var left = pos.left + $("#" + linkID).width() + 10;
           //alert(top);
           if (top > 600)
               top = 10;
           else
               top = 10;
           //top = top - 600;
           left = 130;
           $("#divResponse").dialog("option", "title", title);
           $("#divResponse").dialog("option", "position", [left, top]);

           $("#divResponse").dialog('open');

           unblockResponseDialog();
       }


       function openResponseDialogAndBlock(title, linkID) {
           openResponseDialog(title, linkID);

           //block it to clean out the data
           $("#divResponse").block({
               message: '<img src="../images/async.gif" />',
               css: { border: '0px' },
               fadeIn: 0,
               //fadeOut: 0,
               overlayCSS: { backgroundColor: '#ffffff', opacity: 1 }
           });
       }

       function unblockResponseDialog() {
           $("#divResponse").unblock();
       }



       
    </script>
	
    <!-- fix for 1.1em default font size in Jquery UI -->
	<style type="text/css">
		.ui-widget{font-size:11px !important;}
		.ui-state-error-text{margin-left: 10px}
	</style>

    <script>
        function OpenNewPage(url) {
            window.open(url);
        }

        function Refresh() {
            //alert('refreshing..');
            var btnhdPrintlabel = document.getElementById("<%= btnhdPrintlabel.ClientID %>");
            btnhdPrintlabel.click();
        }
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
<body bgcolor="#ffffff" leftmargin="0" rightmargin="0" topmargin="0" onclick="StopProgress();">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table cellspacing="0" cellpadding="0" border="0" align="center" width="100%">
    <tr>
        <td>
            <menu:Menu ID="menu1" runat="server" ></menu:Menu>
        </td>
     </tr>
     </table>
        
    <table cellspacing="0" cellpadding="0" border="0" align="center" width="95%" >
        <tr valign="top">
            <td>
                <table align="center" style="text-align:left" width="100%" border="0">
                <tr align="left" style="vertical-align:baseline; height:20px" >
                <td >
                    <asp:Label ID="lblHeader" runat="server" Width="100%" Height="20px" CssClass="buttonlabel" Text="Fulfillment Detail"></asp:Label>
                    </td></tr>
             </table>
            </td>
        </tr>
        
        <tr>
            <td>
                  <div id="divContainer">	
            <div id="divRequest"  style="display:none">
            <asp:UpdatePanel ID="upLabel" runat="server">
				<ContentTemplate>
                    
                    <asp:Label ID="lblRequestData" runat="server" CssClass="copy10grey"></asp:Label>

               </ContentTemplate>
            </asp:UpdatePanel>
        </div>
             <div id="divResponse"  style="display:none">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
				<ContentTemplate>
                    
                    <asp:Label ID="lblResponseData" runat="server" CssClass="copy10grey"></asp:Label>

               </ContentTemplate>
            </asp:UpdatePanel>
        </div>
		
    </div>
                 
                
        <table cellSpacing="0" cellPadding="0" align="center" width="100%" border="0" id="maintbl">
        <tr>
			<td>

                            <asp:Label ID="lblViewPO" runat="server"  CssClass="errormessage"></asp:Label>
                 <asp:Label ID="lblpoid" runat="server" Visible="false" ></asp:Label>
<table width="100%" align="center" cellpadding="0" cellspacing="0">
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
                        <td  align="left" width="32%" class="copy10grey" >
                            <asp:Label ID="lblTpye" CssClass="copy10grey" runat="server" ></asp:Label> - <asp:Label ID="lblPO" CssClass="copy10grey" runat="server" ></asp:Label>(<asp:Label ID="lblvStatus" CssClass="copy10grey" runat="server" ></asp:Label>)
                        </td>
                        <td class="copy10grey" width="15%" align="right">
                       <%--<strong>  Status:</strong>--%>
                            Fulfillment Date:
                        </td>
                        <td width="1%">&nbsp;</td>
                        <td  align="left" width="29%" >
                              <asp:Label ID="lblvPODate" CssClass="copy10grey" runat="server" ></asp:Label>
                            
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
                        <td class="copy10grey" width="22%" align="right">
                          Store ID:
                        </td>
                        <td width="1%">&nbsp;</td>
                        <td  align="left" width="22%" >
                           <asp:Label ID="lblvStoreID" CssClass="copy10grey" runat="server" ></asp:Label>
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
                        <td class="copy10grey" width="22%" align="right" >
                           Requested Shipping Date:
                        </td>
                        <td width="1%" >&nbsp;</td>
                        <td  align="left" width="22%" >
                            <asp:Label ID="lblReqShipDate" CssClass="copy10grey" runat="server" ></asp:Label>
                        </td>
                        
                    </tr>
                    <tr>
                        <td class="copy10grey" width="22%" align="left">
                            Total Containers:
                        </td>
                        <td width="1%">&nbsp;</td>
                        <td  align="left" width="32%" >
                           <asp:Label ID="lblItemsPerContainer"  CssClass="copy10grey" runat="server"></asp:Label>
                
                        </td>
                        <td class="copy10grey" width="22%" align="right">
                            Total Pallets:
                        </td>
                        <td width="1%" >&nbsp;</td>
                        <td  align="left" width="22%" >
                            <asp:Label ID="lblContainersPerPallet"  CssClass="copy10grey" runat="server"></asp:Label>
                
                        </td>
                        
                    </tr>
                    </table>
                </td>
            </tr>
        </table>
       
    </td>
    <td>
        &nbsp;
    </td>
    <td width="50%" >
    
        <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="100%" align="center" >
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
<td align="right" colspan="3">
   <div class="loadingcss" align="center" id="modalSending" >
       <img src="/Images/ajax-loaders.gif" alt=""  /> Loading...
   </div> 
   &nbsp;
    <asp:Button ID="btnBox" Visible="false" runat="server" CssClass="button" Text="BOX LABEL" OnClick="btnBox_Click"  /> 
    
     &nbsp;
    <asp:Button ID="btnESN" Visible="false" runat="server" CssClass="button"  Text="ESN Text" OnClick="btnESN_Click"  /> 
   
    &nbsp;
    <asp:Button ID="btnTXT" Visible="false" runat="server" CssClass="button" BackColor="Red" ForeColor="White" Text="Dish Text" OnClick="btnTXT_Click"  /> 
    &nbsp;
    <asp:Button ID="btnMapping" Visible="false" runat="server" CssClass="button" Text="Pallets Mapping" OnClick="btnMapping_Click" OnClientClick="return ShowLoading();"/> 
    
    &nbsp;
    <asp:Button ID="btnPOSLabel" Visible="false" runat="server" CssClass="button" Text="POS KIT" OnClick="btnPOSLabel_Click" OnClientClick="return ShowLoading();"/> 
    &nbsp;
    <asp:Button ID="btnPallet" Visible="false" runat="server" CssClass="button" Text="Pallet Slip" OnClick="btnPallet_Click" OnClientClick="return ShowLoading();" /> 
    &nbsp;
    <asp:Button ID="btnUedf" Visible="false" runat="server" CssClass="button" Text="UEDF XML" OnClick="btnUedf_Click" OnClientClick="return ShowLoading();"/> 
    &nbsp;    
    <asp:Button ID="btnContainerSlip" Visible="true" runat="server" CssClass="button" Text="Container Slip" OnClick="btnContainerSlip_Click"   OnClientClick="return ShowLoading();"/> 
    &nbsp;
    <asp:Button ID="btnPckSlip" Visible="true" runat="server" CssClass="button" Text="Packing Slip" OnClick="btnPckSlip_Click" /> 
    
</td>
</tr>
</table>

<table width="100%" align="center" cellpadding="0" cellspacing="0">
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
    <asp:Button ID="btnhdPrintlabel" runat="server"   Text="Printlabel" OnClick="btnhdPrintlabel_Click"  /> 
        
    </div>
    <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="100%" align="center" >
<tr bordercolor="#839abf">
    <td>
        <table width="100%" cellSpacing="3" cellPadding="3" border="0" >
        <tr width="100%">
        <td colspan="3">
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
            <asp:TemplateField HeaderText="Shipment Type" ItemStyle-CssClass="copy10grey" ItemStyle-Width="11%">
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
            
                                            
            <asp:TemplateField HeaderText="ShipBy" SortExpression="ShipBy"  ItemStyle-CssClass="copy10grey" ItemStyle-Width="13%">
                <ItemTemplate>
                    <%#Eval("ShipByCode")%>
                    <asp:HiddenField ID="hdShipVia" runat="server" Value='<%#Eval("ShipByCode")%>' />
                </ItemTemplate>

            </asp:TemplateField>
                     
            <asp:TemplateField HeaderText="ShipDate"   ItemStyle-CssClass="copy10grey" ItemStyle-Width="14%" >
                <ItemTemplate>
                
                <%# Convert.ToDateTime(Eval("ShipDate")).ToShortDateString() == "1/1/0001" ? "" : Eval("ShipDate")%>
                
                </ItemTemplate>
                <%--<EditItemTemplate>
                    <asp:TextBox ID="txtMdn" CssClass="copy10grey" MaxLength="30"  Text='<%# Eval("MdnNumber") %>' runat="server"></asp:TextBox>
                </EditItemTemplate>                                                                                --%>
            </asp:TemplateField>
                        
            <asp:TemplateField HeaderText="Ship Package"   ItemStyle-CssClass="copy10grey" ItemStyle-Width="12%">
                <ItemTemplate>
                    <%#Eval("ShipPackage")%>
                </ItemTemplate>
            </asp:TemplateField>     
            <asp:TemplateField HeaderText="Weight"  ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="12%">
                <ItemTemplate>
                    <%#Eval("ShipWeight")%>&nbsp;
                </ItemTemplate>
            </asp:TemplateField>     
            <asp:TemplateField HeaderText="Price"  ItemStyle-HorizontalAlign="Right"   ItemStyle-CssClass="copy10grey" ItemStyle-Width="12%">
                <ItemTemplate>
                    $<%# Convert.ToDecimal(Eval("ShipPrice")).ToString("0.00")%>&nbsp;
                </ItemTemplate>
            </asp:TemplateField>     
            <asp:TemplateField HeaderText="TrackingNumber" SortExpression="TrackingNumber" ItemStyle-CssClass="copy10grey" ItemStyle-Width="20%" >
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


<%--            <asp:TemplateField HeaderText=""  ItemStyle-CssClass="copy10grey" ItemStyle-HorizontalAlign="Center" >
                <ItemTemplate>
                     <asp:LinkButton ID="lnkCustom" runat="server" CausesValidation="false" 
                        CommandArgument='<%# Eval("LineNumber") %>'  OnCommand="lnkCustom_Command" ToolTip="View custom declaration" AlternateText="View custom declaration" 
                        >Custom Info</asp:LinkButton>
                  
                </ItemTemplate>
            </asp:TemplateField>  --%> 
            
            <asp:TemplateField HeaderText="" Visible="false"  ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:HiddenField ID="hdTN" Value='<%# Eval("TrackingNumber") %>' runat="server" />
                     <asp:ImageButton ID="imgPrint" runat="server" CausesValidation="false" 
                        CommandArgument='<%# Eval("LineNumber") %>'  OnCommand="imgPrint_Command" ToolTip="Print Label" AlternateText="View Label" 
                        ImageUrl="~/images/printer.png" />
                   <%-- <asp:ImageButton ID="imgLabl" runat="server" 
                        CommandArgument='<%# Eval("ReturnValue") %>'  Visible="false" OnCommand="imgGenerateLabel_Command" ToolTip="Generate Label" AlternateText="Generate Label" 
                        ImageUrl="~/images/doc1.png" />--%>
                </ItemTemplate>
            </asp:TemplateField>   
              
            <%--<asp:CommandField  AccessibleHeaderText="EditTracking"  HeaderText="Edit" ItemStyle-Width="2%" ShowEditButton="True" HeaderStyle-CssClass="button" ControlStyle-CssClass="linkgrid"    ItemStyle-HorizontalAlign="Center"/>--%>
		    <%--<asp:TemplateField HeaderText="" Visible="false"  ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                     <asp:ImageButton ID="imgEditTr" runat="server" 
                        CommandArgument='<%# Eval("LineNumber") %>'  OnCommand="imgEditTracking_Command" ToolTip="Edit Tracking" AlternateText="Edit Tracking" 
                        ImageUrl="~/images/edit.png" />
                </ItemTemplate>
            </asp:TemplateField>                                                   
			--%>  

            <asp:TemplateField HeaderText=""  ItemStyle-CssClass="copy10grey"  ItemStyle-Width="2%" ItemStyle-HorizontalAlign="Center">
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
<%--<tr>
<td>&nbsp;


</td>
</tr>

<tr >
<td colspan="2">
    
</td>    
</tr>

<tr>
<td>&nbsp;</td>
</tr>--%>



<tr id="trCustom" runat="server" visible="false">
    <td colspan="2">
        <table>
<tr>
<td>&nbsp;</td>
</tr></table>
            <table align="center" style="text-align:left" width="100%">
                <tr class="buttonlabel" align="left">
                <td>&nbsp;Custom Declaration</td></tr>
             </table>
                
        <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="100%"  align="center">
                   <tr bordercolor="#839abf">
                    <td>
                        <table width="100%" bgColor="#ffffff" cellpadding="3" cellspacing="3"  align="center">                            
                            <tr>
                                <td class="copy10grey" align="left">
                                    <asp:Repeater ID="rptCustom" runat="server">
                                        <HeaderTemplate>
                                            <table width="100%" align="center">
                                                <tr>
                                                    <td class="buttongrid" width="2%">
                                                        &nbsp;S.No.
                                                    </td>
                                                    <td class="buttongrid">
                                                        &nbsp;SKU#
                                                    </td>
                                                    <td class="buttongrid">
                                                        &nbsp;Quantity
                                                    </td>
                                                    <td class="buttongrid">
                                                        &nbsp;ProductName
                                                    </td>
                                                    <td class="buttongrid">
                                                        &nbsp;TrackingNumber
                                                    </td>
                                                    <td class="buttongrid">
                                                        &nbsp;Custom Value
                                                    </td>                                                    
                                                </tr>                            
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                            <tr class="<%# Container.ItemIndex % 2 == 0 ? "alternaterow" : "" %>">                                                
                                                <td class="copy10grey">
                                                        &nbsp;<%# Container.ItemIndex + 1 %></td>                                
                                                <td class="copy10grey">
                                                        &nbsp;<%# Eval("SKU")%>

                                                    </td>
                                                <td class="copy10grey" align="right">
                                                        <%# Eval("Quantity")%>&nbsp;
                                                    </td>
                                                
                                                <td class="copy10grey">
                                                        &nbsp;<%# Eval("ProductName")%>
                                                    </td>
                                                
                                                <td class="copy10grey">
                                                        &nbsp;<%# Eval("TrackingNumber")%>
                                                    </td>
                                                <td class="copy10grey" align="right">
                                                    &nbsp;$<%# Eval("CustomValue")%>&nbsp;
                                                     

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
            <asp:TemplateField HeaderText="SKU#"  ItemStyle-CssClass="copy10grey"  HeaderStyle-Width="7%" ItemStyle-Wrap="false"  ItemStyle-width="20%">
                <ItemTemplate>
                    <%# Convert.ToString(Eval("ItemCode")).ToUpper()%>
                </ItemTemplate> 
                
            </asp:TemplateField>
                                                                                                                                  
                                            
            <asp:TemplateField HeaderText="Qty"  ItemStyle-CssClass="copy10grey" ItemStyle-Width="3%">
                <ItemTemplate><%#Eval("Quantity")%></ItemTemplate>
                 <EditItemTemplate>
                    <asp:TextBox ID="txtQty" CssClass="copy10grey" MaxLength="5" onkeypress="return isNumberKey(event);" onchange="return isQuantity(this);" 
                    Enabled='<%# Convert.ToInt32(Eval("StatusID")) == 1?true:false %>' Width="99%" Text='<%# Eval("Quantity") %>' runat="server"></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>                 
            <asp:TemplateField HeaderText="Items/Container"  ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <%# Convert.ToInt32(Eval("ItemsPerContainer")) == 0 ? "Not assigned" : Eval("ItemsPerContainer")%>
                </ItemTemplate>
            </asp:TemplateField> 
            <asp:TemplateField HeaderText="Containers/Pallet"  ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate> 
                    <%# Convert.ToInt32(Eval("ContainersPerPallet")) == 0 ? "Not assigned" : Eval("ContainersPerPallet")%>
                    <%--<%#Eval("ContainersPerPallet")%>--%>
                </ItemTemplate>
            </asp:TemplateField>                                                                                                                             
           
            <asp:TemplateField HeaderText="Status"   ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                <ItemTemplate>
                <%# Convert.ToInt32(Eval("StatusID")) == 1 ? "Pending" : Convert.ToInt32(Eval("StatusID")) == 2 ? "Processed" : Convert.ToInt32(Eval("StatusID")) == 3 ? "Shipped" : Convert.ToInt32(Eval("StatusID")) == 4 ? "Closed" : Convert.ToInt32(Eval("StatusID")) == 5 ? "Return" : Convert.ToInt32(Eval("StatusID")) == 9 ? "Cancel" : Convert.ToInt32(Eval("StatusID")) == 6 ? "On Hold" : Convert.ToInt32(Eval("StatusID")) == 7 ? "Out of Stock" : Convert.ToInt32(Eval("StatusID")) == 8 ? "In Process" : Convert.ToInt32(Eval("StatusID")) == 10 ? "Partial Processed" : Convert.ToInt32(Eval("StatusID")) == 11 ? "Partial Shipped" : "Pending"%>
                <%-- <br /> <%#Eval("PODStatus")%>--%>
                    <asp:HiddenField ID="hdnStatus" Value='<%# Eval("StatusID") %>' runat="server" />                   
                                                
                </ItemTemplate>
            </asp:TemplateField> 
            
             
              
            <asp:CommandField  AccessibleHeaderText="EditPOD" Visible="false"  HeaderText="Edit" ItemStyle-Width="5%" ShowEditButton="false" HeaderStyle-CssClass="buttongrid" ControlStyle-CssClass="linkgrid"    ItemStyle-HorizontalAlign="Center"/>
		
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
<br />
 <table align="center" style="text-align:left" width="100%">
                <tr class="buttonlabel" align="left">
                <td>&nbsp;Fulfillment Order Comments</td></tr>
             </table>

<table width="100%" border="0" cellpadding="0" cellspacing="0" align="center">
    <tr>
        <td>
             <asp:Panel ID="pnlComments" runat="server">
                    <asp:Label ID="lblCMsg" CssClass="errormessage" runat="server" ></asp:Label>
                    <UC:Comments ID="c1" runat="server" />
                </asp:Panel>
        </td>
    </tr>

</table>
                <br />

                <asp:PlaceHolder ID="phDoc" runat="server">
                <table align="center" style="text-align:left" width="100%">
                                <tr class="buttonlabel" align="left">
                                <td>&nbsp;Fulfillment Documents</td></tr>
                             </table>
                   <table bordercolor="#839abf" border="0" width="100%" align="center" cellpadding="0" cellspacing="0">
                    <tr>
                    <td>
                        <asp:Label ID="lblDoc" runat="server" CssClass="errormessage" ></asp:Label>
                        <asp:Repeater ID="rptDoc" runat="server" >
                            <HeaderTemplate>
                            <table width="100%" align="center" cellpadding="2" cellspacing="2">
                                <tr >
                                    <td class="buttongrid" width="1%">
                                        &nbsp;S.No.
                                    </td>
                                    <td class="buttongrid" width="30%">
                                        &nbsp;Fulfillment Document
                                    </td>
                                    <td class="buttongrid" width="60%">
                                        &nbsp;Document Description
                                    </td>
                                                    
                                </tr>
                            
                            </HeaderTemplate>
                            <ItemTemplate>
                            <tr class="<%# Container.ItemIndex % 2 == 0 ? "alternaterow" : "" %>">
                                                
                                <td class="copy10grey">
                                        &nbsp;<%# Container.ItemIndex + 1 %>
                                </td>
                                
                                <td class="copy10grey">

                                        &nbsp;
                                        <asp:LinkButton ID="lnkDoc" CssClass="copy10grey" ForeColor="#4092D1" Font-Underline="true"  Font-Size="12px" 
                                            OnClick="lnkDoc_Click" Text='<%# Eval("FileName")%>' runat="server"></asp:LinkButton>  
                        
                                                    
                                </td>
                                <td class="copy10grey">
                                        &nbsp;<%# Eval("FileDescription")%>
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


		                </asp:PlaceHolder>
						
<br />
                
                <asp:UpdatePanel ID="upnlView" runat="server" ChildrenAsTriggers="true">
					<ContentTemplate>
                   
						<asp:PlaceHolder ID="phrView" runat="server">
<table align="center" style="text-align:left" width="100%">
                <tr class="buttonlabel" align="left">
                <td>&nbsp;Fulfillment Log</td></tr>
             </table>
  <table width="100%"  align="center">

<tr>
    <td>
        <asp:Label ID="lblMsg2" runat="server" CssClass="errormessage" ></asp:Label>
                                        <asp:Repeater ID="rptLog" runat="server" OnItemDataBound="rptLog_ItemDataBound">
                                            <HeaderTemplate>
                                            <table width="100%" align="center">
                                                <tr >
                                                    <td class="buttongrid" width="1%">
                                                        &nbsp;S.No.
                                                    </td>
                                                    <td class="buttongrid" width="10%">
                                                        &nbsp;Date
                                                    </td>
                                                    <td class="buttongrid" width="10%">
                                                        &nbsp;Log By
                                                    </td>
                                                    <td class="buttongrid" width="10%">
                                                        &nbsp;Status
                                                    </td>
                                                    <td class="buttongrid" width="20%">
                                                        &nbsp;Request Data
                                                    </td>
                                                    <td class="buttongrid" width="20%">
                                                        &nbsp;Response Data
                                                    </td>
                                                    <td class="buttongrid" width="20%">
                                                        &nbsp;Action
                                                    </td>
                                                    
                                                </tr>
                            
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                            <tr class="<%# Container.ItemIndex % 2 == 0 ? "alternaterow" : "" %>">
                                                
                                                <td class="copy10grey">
                                                        &nbsp;<%# Container.ItemIndex + 1 %>
                                                </td>
                                
                                                <td class="copy10grey">
                                                        &nbsp;<%# Eval("CreateDate")%>
                                                </td>
                                                <td class="copy10grey">
                                                        &nbsp;<%# Eval("UserName")%>
                                                </td>
                                                <td class="copy10grey">
                                                        &nbsp;<%# Eval("Status")%>
                                                </td>
                                                <td class="copy10grey">
                                                        &nbsp; <asp:LinkButton ToolTip="See more..." CausesValidation="false" OnCommand="lnkRequest_Click" CommandArgument='<%# Eval("POLogID") %>'  
                                                                ID="lnkRequest"  runat="server"  >RequestData</asp:LinkButton>
               
                                                </td>
                                                <td class="copy10grey">
                                                        &nbsp;  
                                                     <asp:LinkButton ToolTip="See more..." CausesValidation="false" OnCommand="lnkResponse_Click" CommandArgument='<%# Eval("POLogID") %>'  
                                                     ID="lnkResponse"  runat="server"  >ResponseData</asp:LinkButton>
                                                </td>
                                                <td class="copy10grey">
                                                        &nbsp;<%# Eval("ActionName")%>
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


		                </asp:PlaceHolder>
						
						
					</ContentTemplate>
						<%--<Triggers>
                            <asp:PostBackTrigger ControlID="btnhdPrintlabel" />
                            <asp:PostBackTrigger ControlID="btnPckSlip" />
                            <asp:PostBackTrigger ControlID="btnContainerSlip" />
                            <asp:PostBackTrigger ControlID="btnUedf" />
                            <asp:PostBackTrigger ControlID="btnPOSLabel" />
                            <asp:PostBackTrigger ControlID="btnPallet" />
                            <asp:PostBackTrigger ControlID="btnMapping" />
                            <asp:PostBackTrigger ControlID="btnTXT" />

						</Triggers>	--%>
				</asp:UpdatePanel>

                </td>
            </tr>
            </table>
                 <asp:UpdatePanel ID="upnlJsRunner" UpdateMode="Always" runat="server">
			<ContentTemplate>
				<asp:PlaceHolder ID="phrJsRunner" runat="server"></asp:PlaceHolder>
			</ContentTemplate>
		</asp:UpdatePanel>
           

            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr valign="top">
            <td>
                           </td>
        </tr>
        
        <tr>
            <td>
               
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr valign="top">
            <td>
                
            </td>
        </tr>
        
        <tr>
            <td>
              
                        <%--<asp:Panel ID="pnlLog" runat="server" >                            
                         <UC:PoLog ID="poLog" runat="server"></UC:PoLog>                          
                        </asp:Panel>--%>
            </td>
        </tr>
    
    </table>
        <div style="display:block">
        <asp:Panel ID="pnlUnprovision" runat="server" Visible="false">
            <%--<table align="center" style="text-align:left" width="95%">
                <tr class="buttonlabel" align="left">
                <td>&nbsp;Comments</td>
                </tr>
            </table>--%>
            <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="95%" align="center" >
           <tr bordercolor="#839abf">
                <td>
                    <table width="100%" cellSpacing="2" cellPadding="2">
                    <tr>
                        <td class="copy10grey" width="22%" align="left">
                          <strong>  Requested By#:</strong>
                        </td>
                        <td width="1%">&nbsp;</td>
                        <td  align="left" width="32%" >
                            <asp:DropDownList ID="ddlRequestedBy" CssClass="copy10grey" runat="server" Width="70%" ></asp:DropDownList>
                        </td>
                        <td class="copy10grey" width="15%" align="right">
                       <%--<strong>  Approved By:</strong>--%>
                        </td>
                        <td width="1%">&nbsp;</td>
                        <td  align="left" width="29%" >
                             <%--<asp:DropDownList ID="ddlApprovedBy" CssClass="copy10grey" runat="server" ></asp:DropDownList>--%>
                        </td>
                   
                    </tr>
                    <tr>
                        <td class="copy10grey" width="22%" align="left">
                            Comment:
                        </td>
                        <td width="1%">&nbsp;</td>
                        <td  align="left" width="32%" colspan="4" >
                            <asp:TextBox ID="txtUnComment" runat="server" Width="90%"  Height="70px" TextMode="MultiLine" Rows="9" CssClass="copy10grey"></asp:TextBox>
                        </td>
                    </tr>
                    </table>
                </td>
            </tr>
        </table>
       

        </asp:Panel>
            </div>
        <br />
        <table width="100%" align="center">
                <tr>
                    <td align="center">
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="button" Visible="false" OnClick="btnSubmit_Click"  /> &nbsp; 
                        <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="button" Visible="true" OnClientClick="return close_window();"  />
                    </td>
                </tr>
                </table>
	
        
<br /><br /> <br />
            <br /> <br />
        <table cellspacing="0" cellpadding="0" border="0" align="center" width="100%">
        <tr>
            <td >
                <foot:MenuFooter ID="footer2" runat="server"></foot:MenuFooter>
            </td>
         </tr>
         </table>
        <script type="text/javascript">
            function StopProgress() {

                // $("div.modal").hide();
                
                var delayInMilliseconds = 13000; //7 second

                setTimeout(function () {

                   // alert('yo');

                    var tb = $("maintbl");
                    tb.removeClass("progresss");


                    var loading = $("#modalSending.loadingcss");
                    loading.hide();

                    //your code to be executed after 1 second
                }, delayInMilliseconds);

                // alert(loading);

            }

            //StopProgress();
        </script>
 
    </form>
</body>
</html>
