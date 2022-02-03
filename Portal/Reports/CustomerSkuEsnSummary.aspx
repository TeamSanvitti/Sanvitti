<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomerSkuEsnSummary.aspx.cs" Inherits="avii.Reports.CustomerSkuEsnSummary" %>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="/Controls/Header.ascx" %>
<%@ Register TagPrefix="UC" TagName="ESN" Src="/Controls/ViewESN.ascx" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="/Controls/Footer.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>.:: Lan Global Inc. - ESN/SIM Inventory(SKU Wise) ::.</title>

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
    
	<script type="text/javascript">
	    $(document).ready(function () {
	        $("#divESN").dialog({
	            autoOpen: false,
	            modal: true,
	            minHeight: 400,
	            height: 660,
	            width: 400,
	            resizable: false,
	            open: function (event, ui) {
	                $(this).parent().appendTo("#divContainer");
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
	                $(this).parent().appendTo("#divContainer");
	            }
	        });

	        $("#divProcessedESN").dialog({
	            autoOpen: false,
	            modal: true,
	            minHeight: 400,
	            height: 660,
	            width: 400,
	            resizable: false,
	            open: function (event, ui) {
	                $(this).parent().appendTo("#divContainer");
	            }
	        });

	        $("#divShipped").dialog({
	            autoOpen: false,
	            modal: true,
	            minHeight: 400,
	            height: 660,
	            width: 400,
	            resizable: false,
	            open: function (event, ui) {
	                $(this).parent().appendTo("#divContainer");
	            }
	        });

	        $("#divRMA").dialog({
	            autoOpen: false,
	            modal: true,
	            minHeight: 400,
	            height: 660,
	            width: 400,
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
	    function closeDialogP() {
	        //Could cause an infinite loop because of "on close handling"
	        $("#divProcessedESN").dialog('close');
	    }
	    function closeDialogS() {
	        //Could cause an infinite loop because of "on close handling"
	        $("#divShipped").dialog('close');
	    }
	    function closeDialogR() {
	        //Could cause an infinite loop because of "on close handling"
	        $("#divRMA").dialog('close');
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
	    function openDialogP(title, linkID) {

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
	        $("#divProcessedESN").dialog("option", "title", title);
	        $("#divProcessedESN").dialog("option", "position", [left, top]);

	        $("#divProcessedESN").dialog('open');

	    }
	    function openDialogS(title, linkID) {

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
	        $("#divShipped").dialog("option", "title", title);
	        $("#divShipped").dialog("option", "position", [left, top]);

	        $("#divShipped").dialog('open');

	    }

	    function openDialogR(title, linkID) {

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
	        $("#divRMA").dialog("option", "title", title);
	        $("#divRMA").dialog("option", "position", [left, top]);

	        $("#divRMA").dialog('open');

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
	    function openDialogAndBlockP(title, linkID) {

	        openDialogP(title, linkID);
	        //alert('2')
	        //block it to clean out the data
	        $("#divProcessedESN").block({
	            message: '<img src="../images/async.gif" />',
	            css: { border: '0px' },
	            fadeIn: 0,
	            //fadeOut: 0,
	            overlayCSS: { backgroundColor: '#ffffff', opacity: 1 }
	        });
	    }
	    function openDialogAndBlockS(title, linkID) {

	        openDialogS(title, linkID);
	        //alert('2')
	        //block it to clean out the data
	        $("#divShipped").block({
	            message: '<img src="../images/async.gif" />',
	            css: { border: '0px' },
	            fadeIn: 0,
	            //fadeOut: 0,
	            overlayCSS: { backgroundColor: '#ffffff', opacity: 1 }
	        });
	    }
	    function openDialogAndBlockR(title, linkID) {

	        openDialogR(title, linkID);
	        //alert('2')
	        //block it to clean out the data
	        $("#divRMA").block({
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
	    function unblockDialogP() {
	        $("#divProcessedESN").unblock();
	    }

	    function unblockDialogS() {
	        $("#divShipped").unblock();
	    }
	    function unblockDialogR() {
	        $("#divRMA").unblock();
	    }

	    
	    

	    function closeDownloadDialog() {
	        //Could cause an infinite loop because of "on close handling"
	        $("#divDownload").dialog('close');
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
	    function unblockDownloadDialog() {
	        $("#divDownload").unblock();
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

	    function IsValidateDnw() {
	        var ddlDnw = document.getElementById("<%=dllDownload.ClientID %>");
	        if (ddlDnw.selectedIndex == 0) {
	            alert('Please select download option');
	            return false;
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
			<tr>
			<td  bgcolor="#dee7f6" class="buttonlabel">
            &nbsp;&nbsp;ESN/SIM Inventory(SKU Wise) 
			</td>
		</tr>
    </table>
	<div id="divContainer">	
			<div id="divESN" style="display:none">
					
				<asp:UpdatePanel ID="upnlESN" runat="server">
					<ContentTemplate>
                   
						<asp:PlaceHolder ID="phrESN" runat="server">
                        <table width="100%" border="0">
                                <tr>
                                    <td class="copy10grey" width="35%" align="left">
                                        <b> &nbsp;SKU#:</b>
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

            <div id="divProcessedESN" style="display:none">
					
				<asp:UpdatePanel ID="upProcessed" runat="server">
					<ContentTemplate>
                   
						<asp:PlaceHolder ID="phProcessed" runat="server">
                        <table width="100%" border="0">
                                <tr>
                                    <td class="copy10grey" width="35%" align="left">
                                        <b> &nbsp;SKU#:</b>
                                    </td>
                                    <td align="left" >
                                     <asp:Label ID="lblSku2" runat="server" CssClass="copy10grey"></asp:Label>               
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                
                                    <asp:Panel ID="pnlProcessed" runat="server" Width="100%">

                                        <UC:ESN ID="esn2" runat="server" />


                                    </asp:Panel>
                            </td>
                                </tr>
                                </table>
                                
                        </asp:PlaceHolder>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>

            <div id="divShipped" style="display:none">
					
				<asp:UpdatePanel ID="upShipped" runat="server">
					<ContentTemplate>
                   
						<asp:PlaceHolder ID="phShipped" runat="server">
                        <table width="100%" border="0">
                                <tr>
                                    <td class="copy10grey" width="35%" align="left">
                                        <b> &nbsp;SKU#:</b>
                                    </td>
                                    <td align="left" >
                                     <asp:Label ID="lblSku3" runat="server" CssClass="copy10grey"></asp:Label>               
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                
                                    <asp:Panel ID="pnlShipped" runat="server" Width="100%">

                                        <UC:ESN ID="esn3" runat="server" />


                                    </asp:Panel>
                            </td>
                                </tr>
                                </table>
                                
                        </asp:PlaceHolder>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div id="divRMA" style="display:none">
					
				<asp:UpdatePanel ID="upRMA" runat="server">
					<ContentTemplate>
                   
						<asp:PlaceHolder ID="phRMA" runat="server">
                        <table width="100%" border="0">
                                <tr>
                                    <td class="copy10grey" width="35%" align="left">
                                        <b> &nbsp;SKU#:</b>
                                    </td>
                                    <td align="left" >
                                     <asp:Label ID="lblSKU4" runat="server" CssClass="copy10grey"></asp:Label>               
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                
                                    <asp:Panel ID="pnlRMA" runat="server" Width="100%">

                                        <UC:ESN ID="esn4" runat="server" />


                                    </asp:Panel>
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
                                        Download ESN:    
                                    </td>
                                    <td width="34%" align="center">
                                        <asp:DropDownList ID="dllDownload" runat="server" class="copy10grey">
                                            <asp:ListItem Text="" Value=""></asp:ListItem>
                                            <asp:ListItem Text="Download Unused ESN/SIM" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Download Used ESN/SIM" Value="2"></asp:ListItem>
                                        </asp:DropDownList>   
                                    </td>
                                    <td width="33%" align="left">
                                        <asp:Button ID="btnDwnlESN" runat="server" OnClientClick="return IsValidateDnw();" OnClick="btnDownload_Click" Text="Download" CssClass="button" />
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
            
     </div>


      <asp:UpdatePanel ID="upnlCustomers" UpdateMode="Conditional" runat="server">
			<ContentTemplate>
	
    <table cellSpacing="0" cellPadding="0" align="center" width="100%" border="0">
    		<tr>                    
                <td >
                    <asp:Label ID="lblMsg" runat="server" Width="100%" CssClass="errormessage"></asp:Label></td>
            </tr> 
            <tr><td class="copy10grey" align="left">&nbsp;
                        - ESN/SIM of inprocess RMA will be reflected fullfillment shipped count & RMA count.<br />&nbsp;
	                    - Unused ESN/SIM count equal to new plus approved RMA ESN. <br />&nbsp;
                        </td></tr>  
            </table>  
                      
      <table bordercolor="#839abf" border="1" width="100%" align="center" cellpadding="0" cellspacing="0">
      <tr>
        <td>
         <asp:Panel  ID="pnlSearch" runat="server"  DefaultButton="btnSearch" >
      <table width="100%" border="0" class="box" align="center" cellpadding="2" cellspacing="2">
         <tr style="height:1px">
         <td style="height:1px"></td>
         </tr>
         <tr valign="top">
                <td class="copy10grey" align="right" width="15%">
                    ESN/SIM:
                </td>
                <td width="35%" align="left">
                &nbsp;<asp:DropDownList ID="ddlOption" runat="server"  Width="80%" 
                AutoPostBack="true" OnSelectedIndexChanged="ddlOption_SelectedIndexChanged"  
                 class="copy10grey"  >
                    <asp:ListItem Text="ESN" Value="ESN"></asp:ListItem>
                    <asp:ListItem Text="SIM" Value="SIM"></asp:ListItem>
                </asp:DropDownList>  
                
                </td>
                <td  width="1%">
                    &nbsp;
                </td>
                <td class="copy10grey" align="right" width="10%">
                    
                
                </td>
                <td class="copy10grey" align="left" width="40%">
                    
                </td>

            </tr>    
            
            <tr valign="top">
                <td class="copy10grey" align="right" width="15%">
                    Customer Name:
                </td>
                <td width="35%" align="left">
                &nbsp;<asp:DropDownList ID="dpCompany" runat="server" AutoPostBack="true" OnSelectedIndexChanged="dpCompany_SelectedIndexChanged"  Width="80%"
                 class="copy10grey"  >
                </asp:DropDownList>  
                <asp:Label ID="lblCompany" runat="server" CssClass="copy10grey" ></asp:Label>
                </td>
                <td  width="1%">
                    &nbsp;
                </td>
                <td class="copy10grey" align="right" width="10%">
                    <asp:Label ID="lblSKU" runat="server" CssClass="copy10grey" Text="SKU:" ></asp:Label>
                
                </td>
                <td class="copy10grey" align="left" width="40%">
                    &nbsp;<asp:DropDownList ID="ddlSKU" runat="server" class="copy10grey">
                    </asp:DropDownList>
          
                </td>

            </tr>    
            <tr>
                <td class="copy10grey" align="right" width="15%">
                    From Date:
                
                
                </td>
                <td align="left" width="35%">
                    &nbsp;<asp:TextBox ID="txtFromDate" runat="server" onfocus="set_focus1();"  CssClass="copy10grey" MaxLength="15"  Width="80%"></asp:TextBox>
                    <img id="imgFromtDate" alt="" onclick="document.getElementById('<%=txtFromDate.ClientID%>').value = ''; displayCalendar(document.getElementById('<%=txtFromDate.ClientID%>'),'mm/dd/yyyy',this,true);" src="../fullfillment/calendar/sscalendar.jpg" />
                </td>
                <td  width="1%">
                    &nbsp;
                </td>
                    <td class="copy10grey" align="right" width="10%">Date To:</td>
                
                <td align="left" width="40%">
                    &nbsp;<asp:TextBox ID="txtToDate" runat="server" onfocus="set_focus2();"  CssClass="copy10grey" MaxLength="15"  Width="45%"></asp:TextBox>
                    <img id="imgToDate"  alt="" onclick="document.getElementById('<%=txtToDate.ClientID%>').value = ''; displayCalendar(document.getElementById('<%=txtToDate.ClientID%>'),'mm/dd/yyyy',this,true);" src="../fullfillment/calendar/sscalendar.jpg" />
                
                </td>               
                </tr>
            <tr>
                <td class="copy10grey" align="right" width="15%">
                    <%--Show All Unused ESN:--%>
                
                
                </td>
                <td align="left" width="35%">
                    <asp:CheckBox ID="chkESN" Visible="false" Checked="true" CssClass="copy10grey" runat="server" />
                </td>
                <td  width="1%">
                    
                </td>
                    <td class="copy10grey" align="right" width="10%">
                    </td>
                
                <td align="left" width="40%">
                    
                </td>               
                </tr>
                
            <%--<tr>
                <td class="copy10grey" align="left" width="10%">
                    Duration with in:
                </td>
                <td width="90%" align="left">
                   <asp:DropDownList ID="ddlDuration" AutoPostBack="false"  runat="server" Class="copy10grey" 
                                                Width="135px" >
                                                        <asp:ListItem  Value="1"  >Today</asp:ListItem>
                                <asp:ListItem  Value="7"  >One week</asp:ListItem>
                                <asp:ListItem  Value="15"  >Two week</asp:ListItem>
                                <asp:ListItem  Value="30" Selected="True" >Last Month</asp:ListItem>
                                <asp:ListItem Value="90" >Quaterly</asp:ListItem>
                                <asp:ListItem  Value="180">6 Months</asp:ListItem>
                                <asp:ListItem  Value="365">One Year</asp:ListItem>
                                <asp:ListItem  Value="730">Past One Year</asp:ListItem>
                    </asp:DropDownList>
                   
        
                </td>   
                
                
            </tr>--%>
            <tr>
            <td colspan="5">
                <hr />
                </td>
                </tr>
                <tr>
                <td  align="center"  colspan="5">
                    
                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="button" OnClientClick="return Validate();"   OnClick="btnSearch_Click" CausesValidation="false"/>
                     &nbsp;
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="button" OnClick="btnCancel_Click" CausesValidation="false"/>
                    &nbsp;
                    <asp:Button ID="btnDownload" runat="server" Text=" Download " CssClass="button" OnClientClick="openDownloadDialog('Download ESN','btnDownload')"  CausesValidation="false"/>  &nbsp;
                          &nbsp;
                    <%--<asp:Button ID="btnDwnlUsedESN" runat="server" Text=" Download Used ESN" CssClass="button" OnClick="btnDwnlUsedESN_Click" CausesValidation="false" />  
--%>
            
        
        </td>
        </tr>
        </table>
        </asp:Panel>
        </td>
        </tr>
        </table>
   

            <table align="center" style="text-align:left" width="100%">
                             <tr>
                                <td  align="left" style="height:8px; vertical-align:bottom">
                                    
                                    <asp:Label ID="lblDate" CssClass="copy10grey" runat="server" ></asp:Label> 
                                    <%--<asp:Label ID="lblRmaDate" CssClass="copy10grey" runat="server" ></asp:Label> &nbsp;&nbsp;    --%>
                                </td>
                                <b></b>

                                <td  align="right" style="height:8px; vertical-align:bottom">
                                
                                <strong>   
                                    <asp:Label ID="lblCount" CssClass="copy10grey" runat="server" ></asp:Label> &nbsp;&nbsp;
                                </strong> 
                                </td>
                             </tr>

                             <tr>
                                <td colspan="2" align="center">
                                    <asp:GridView ID="gvESN" runat="server" Width="100%" GridLines="Both"   AutoGenerateColumns="false"
                                    OnPageIndexChanging="gridView_PageIndexChanging" PageSize="50" AllowPaging="false" 
                                    OnRowDataBound="gvESN_RowDataBound" ShowFooter="true" >
                                        <RowStyle BackColor="Gainsboro" />
                                        <AlternatingRowStyle BackColor="white" />
                                        <HeaderStyle  CssClass="buttongrid" ForeColor="white"/>
                                        <FooterStyle CssClass="white"  />
                                        <PagerStyle ForeColor="#636363" CssClass="copy10grey" HorizontalAlign="Left" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No." ItemStyle-HorizontalAlign="Left"  ItemStyle-CssClass="copy10grey" ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                <%# Container.DataItemIndex +  1 %>
                                                
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                  <strong> <span class="copy10grey"> Total</span></strong>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="SKU#" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                                <ItemTemplate><%#Eval("SKU")%></ItemTemplate>
                                            </asp:TemplateField>
                                            
                                            <%--<asp:TemplateField HeaderText="Product Code" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                
                                                <%#Eval("ProductCode")%>
                                                
                                                
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="Product Name" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                                <ItemTemplate><%#Eval("ItemName")%></ItemTemplate>
                                            </asp:TemplateField>
                                            
                                             <%--<asp:TemplateField HeaderText="Total ESN" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="7%">
                                                <ItemTemplate><%#Eval("TotalESN")%></ItemTemplate>
                                            </asp:TemplateField> --%>

                                            <asp:TemplateField HeaderText="Opening Balance(1)" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right"  ItemStyle-CssClass="copy10grey" FooterStyle-CssClass="copy10grey" ItemStyle-Width="8%">
                                                <ItemTemplate>
                                                <%#Eval("OpeningBalance")%>
                                                </ItemTemplate>
                                                
                                            </asp:TemplateField> 
                                            
                                            <asp:TemplateField HeaderText="New ESN/SIM(2)" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right"  ItemStyle-CssClass="copy10grey" FooterStyle-CssClass="copy10grey" ItemStyle-Width="8%">
                                                <ItemTemplate>
                                                <%#Eval("NewESN")%>
                                                </ItemTemplate>
                                                
                                            </asp:TemplateField> 
                                            
                                            <asp:TemplateField HeaderText="Available ESN/SIM(1+2)" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right"  ItemStyle-CssClass="copy10grey" FooterStyle-CssClass="copy10grey" ItemStyle-Width="8%">
                                                <ItemTemplate>
                                                <%#Eval("AvailableESN")%>
                                                    <%--<asp:Label ID="lblAE" runat="server" CssClass="copy10grey" Text='<%#Eval("AvailableESN")%>'></asp:Label>--%>
                                                </ItemTemplate>
                                                
                                            </asp:TemplateField> 

                                            <asp:TemplateField HeaderText="ESN/SIM Processed(3)" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right"  ItemStyle-CssClass="copy10grey" FooterStyle-CssClass="copy10grey" ItemStyle-Width="8%">
                                                <ItemTemplate>
                                                <asp:LinkButton Visible='<%# Convert.ToInt32(Eval("ItemCompanyGUID")) ==0 ? false : true %>' ToolTip="View Processed ESN" 
                                                        CausesValidation="false" OnCommand="imgViewProcessedESN_Click" CommandArgument='<%# Eval("ItemCompanyGUID") %>'  
                                                        ID="lnkPESN"  runat="server" Text='<%#Eval("UsedEsnProcessed")%>' />
                                                <%--<%#Eval("UsedEsnProcessed")%>--%>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <strong><asp:Literal ID="ltProcessed" runat="server" /></strong>
                                                </FooterTemplate>
                                            </asp:TemplateField> 
                                            <asp:TemplateField HeaderText="ESN/SIM Shipped(4)" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right"  ItemStyle-CssClass="copy10grey" FooterStyle-CssClass="copy10grey" ItemStyle-Width="8%">
                                                <ItemTemplate>
                                                <asp:LinkButton Visible='<%# Convert.ToInt32(Eval("ItemCompanyGUID")) ==0 ? false : true %>' ToolTip="View Shipped ESN" 
                                                        CausesValidation="false" OnCommand="imgViewShippedESN_Click" CommandArgument='<%# Eval("ItemCompanyGUID") %>'  
                                                        ID="lnkSESN"  runat="server" Text='<%#Eval("UsedEsnShipped")%>' />
                                                <%--<%#Eval("UsedEsnShipped")%>--%>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                  <strong> <asp:Literal ID="ltShipped" runat="server" /></strong>
                                                </FooterTemplate>
                                            </asp:TemplateField> 
                                            <asp:TemplateField HeaderText="Closing Balance (1+2)-(3+4)" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right"  ItemStyle-CssClass="copy10grey" FooterStyle-CssClass="copy10grey" ItemStyle-Width="8%">
                                                <ItemTemplate>
                                                <%#  Convert.ToInt32(Eval("AvailableESN")) - (Convert.ToInt32(Eval("UsedEsnProcessed")) + Convert.ToInt32(Eval("UsedEsnShipped")))%>
                                                </ItemTemplate>
                                                <%--<FooterTemplate>
                                                   
                                                   <strong>  <asp:Literal ID="ltClosedBalance" runat="server" /></strong>
                                                </FooterTemplate>
                                                --%>
                                            </asp:TemplateField> 
                                            
                                            <asp:TemplateField HeaderText="Unused ESN/SIM" FooterStyle-CssClass="copy10grey"  Visible="false" FooterStyle-HorizontalAlign="Right"  ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="8%">
                                                <ItemTemplate>
                                                    <asp:LinkButton Visible='<%# Convert.ToInt32(Eval("ItemCompanyGUID")) ==0 ? false : true %>' ToolTip="View ESN" 
                                                        CausesValidation="false" OnCommand="imgViewESN_Click" CommandArgument='<%# Eval("ItemCompanyGUID") %>'  ID="lnkESN"  runat="server" Text='<%#Eval("UnusedESN")%>' />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                   
                                                   <strong>  <asp:Literal ID="ltUnused" runat="server" /></strong>
                                                </FooterTemplate>
                                            </asp:TemplateField> 

                                            <asp:TemplateField HeaderText="RMA" FooterStyle-CssClass="copy10grey" FooterStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="8%">
                                                <ItemTemplate>
                                                <asp:LinkButton Visible='<%# Convert.ToInt32(Eval("ItemCompanyGUID")) ==0 ? false : true %>' ToolTip="View RMA ESN" 
                                                        CausesValidation="false" OnCommand="imgViewRMAESN_Click" CommandArgument='<%# Eval("ItemCompanyGUID") %>'  ID="lnkRMA"  runat="server" Text='<%#Eval("RmaESN")%>' />
                                                <%--<%#Eval("RmaESN")%>--%>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <strong><asp:Literal ID="ltRMA" runat="server" /></strong>
                                                </FooterTemplate>
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
            <Triggers>
                <asp:PostBackTrigger ControlID="btnDwnlESN" />
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
