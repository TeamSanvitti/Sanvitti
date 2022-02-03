<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RmaUpload.aspx.cs" Inherits="avii.RMA.RmaUpload" %>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="/Controls/Header.ascx" %>
<%@ Register TagPrefix="Rma" TagName="Reasons" Src="/Controls/RmaReasons.ascx" %>
<%@ Register TagPrefix="WC" TagName="State" Src="/Controls/States.ascx" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 <title>.:: Lan Global inc. ::.</title>
    <link href="/aerostyle.css" type="text/css" rel="stylesheet"/>

<!-- from http://encosia.com/2009/10/11/do-you-know-about-this-undocumented-google-cdn-feature/ -->
    <link href="https://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/themes/start/jquery-ui.css" type="text/css" rel="Stylesheet" />
	<%--
	<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.4.0/jquery.min.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/jquery-ui.min.js"></script>--%>
    <script type="text/javascript" src="/JQuery/jquery.min.js"></script>
    <script type="text/javascript" src="/JQuery/jquery-ui.min.js"></script>
	
	<script type="text/javascript" src="/JQuery/jquery.blockUI.js"></script>

	
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
	            width: 750,
	            resizable: false,
	            open: function (event, ui) {
	                $(this).parent().appendTo("#divFulfillmentContainer");
	            }
	        });
	        $("#divReason").dialog({
	            autoOpen: false,
	            modal: true,
	            minHeight: 20,
	            height: 340,
	            width: 450,
	            resizable: false,
	            open: function (event, ui) {
	                $(this).parent().appendTo("#divFulfillmentContainer");
	            }
	        });
	        $("#divState").dialog({
	            autoOpen: false,
	            modal: true,
	            minHeight: 20,
	            height: 540,
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
	        top = top - 100;
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

	    function closedReasonDialog() {
	        //Could cause an infinite loop because of "on close handling"
	        $("#divReason").dialog('close');
	    }
	    function openReasonDialog(title, linkID) {

	        var pos = $("#" + linkID).position();
	        var top = pos.top;
	        var left = pos.left + $("#" + linkID).width() + 10;
	        //alert(left);
	        top = top - 300;
	        left = 350;
	        $("#divReason").dialog("option", "title", title);
	        $("#divReason").dialog("option", "position", [left, top]);

	        $("#divReason").dialog('open');
	    }
	    function openReasonDialogAndBlock(title, linkID) {
	        openReasonDialog(title, linkID);

	        //block it to clean out the data
	        $("#divReason").block({
	            message: '<img src="../images/async.gif" />',
	            css: { border: '0px' },
	            fadeIn: 0,
	            //fadeOut: 0,
	            overlayCSS: { backgroundColor: '#ffffff', opacity: 1 }
	        });
	    }
	    function unblockReasonDialog() {
	        $("#divReason").unblock();
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
				<td  bgcolor="#dee7f6" class="button">&nbsp;&nbsp;RMA Upload
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
                        <%--
	                    - Upload file should be less than 2 MB. <br />
                        - Upload file should not contain more than 20 RMAs. <br />
                        - Maximum of 10 ESNs are allowed in one RMA request.<br />
                        - RMA date should not be 90 days before from today date.<br />
                        - Email should not have &quot;Aerovoice.com&quot; email address.<br />--%>
                        <%--- Maximum of 20 RMAs are allowed in one time upload request.<br />--%>

                        
	                    - The upload File should not contain more than 20 RMAs and should have less than 500 ESNs.<br />
                        - RMA date should not be 90 days before from today'a date.<br />
                        - Email should not have "Aerovoice.com" email address.<br />
                        - Click on "State" link to get the list of State codes.<br />
                        - Use backslash(\) with comma(\,) to add , in the text.<br />
                        &nbsp;
                        </td></tr>
                    </table>

                 <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="70%">
                    <tr>
                    <td>
			            <table id="Table2" cellSpacing="5" cellPadding="5" width="100%" border="0">
                            <tr>
                                <td  class="copy10grey" align="right" width="22%" >
                                    Customer: &nbsp;</td>
                                <td align="left" >
                                    <asp:DropDownList ID="dpCompany" CssClass="copy10grey" runat="server"  AutoPostBack="false">
									</asp:DropDownList>
                            </tr>
                            
                            <tr>
                                <td  class="copy10grey" align="right" >
                                    Upload RMA file: &nbsp;</td>
                                <td align="left" >
                                    <asp:FileUpload ID="flnUpload" runat="server" CssClass="txfield1" Width="50%" /></td>
                            </tr>
                            <tr id="trShipFile" >
                                <td class="copy10grey" align="right">
                                    File format sample: &nbsp;
                                         </td>
                                <td class="copy10grey" align="left">
                                   <b>RMANumber, RMADate, CustomerName, Address, City, 
                                   <asp:LinkButton ID="lnkState" runat="server" CssClass="copy10grey" Text="State" ToolTip="State list" OnClick="btnState_Click" 
                                        OnClientClick="openStateDialogAndBlock('State List', 'lnkState')" CausesValidation="false"/>, Zip, Email, Phone, ESN, ReceivedOn, 
                                   <asp:LinkButton ID="lnkReason" runat="server" CssClass="copy10grey" Text="Reason" ToolTip="Rma Reason list" OnClick="btnReason_Click" 
                                        OnClientClick="openReasonDialogAndBlock('RMA Reason', 'lnkReason')" CausesValidation="false"/>

                                   </b>, Comments, CallTime, warranty, WarrantExpiryDate, Notes
                                   
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
                                
                                            &nbsp;<asp:Button ID="btnValidate2" Visible="false" Width="190px"  CssClass="button" runat="server" OnClick="btnValidates_Click" Text="Validate" />
                                
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
                                <td align="right" style="height:8px; vertical-align:bottom">
                                <strong>   <asp:Label ID="lblCount" CssClass="copy10grey" runat="server" ></asp:Label> &nbsp;&nbsp;</strong> 
                                </td>
                             </tr>
                             <tr>
                                <td align="center">
                                        
    
                                
                                <asp:Repeater ID="rptRMA" runat="server" Visible="true"  OnItemDataBound="rpt_OnItemDataBound">
                                <HeaderTemplate>
                                <table border="0" width="98%" cellpadding="1" cellspacing="1">
                                <tr>
                                    <td class="button" >
                                        S.No.
                                    </td>
                                    <td class="button" >
                                        rmaNumber
                                    </td>
                                    <td class="button" >
                                        TemprmaNumber
                                    </td>
                                    
                                    <td class="button">
                                        rma Date
                                    </td>
                                    <td class="button">
                                        ContactName 
                                    </td>
                                    <td class="button">
                                        Address
                                    </td>
                                    <td class="button">
                                        City
                                    </td>
                                    <td class="button">
                                        state
                                    </td>
                                    <td class="button">
                                        zip
                                    </td>
                                    <td class="button">
                                        email
                                    </td>
                                    <td class="button">
                                        phone  
                                    </td>
                                    <%--<td class="button">
                                        esn  
                                    </td>
                                    <td class="button">
                                        received on
                                    </td>
                                    <td class="button">
                                        reason  
                                    </td>--%>
                                    <td class="button">
                                        comments  
                                    </td>
                                    <%--<td class="button">
                                        CallTime  
                                    </td>
                                    
                                    <td class="button">
                                        warrantry
                                    </td>
                                    <td class="button">
                                        warranty Date
                                    </td>
                                    <td class="button">
                                        notes
                                    </td>--%>
                                    <%--<td class="button">
                                    Status
                                    </td>--%>
                                </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
    
                                    <tr valign="bottom"  class="<%# Container.ItemIndex % 2 == 0 ? "alternaterow" : "" %>">
                                        <td class="copy10grey"  >
                                        <%# Container.ItemIndex +  1 %>
                                        </td>
                                        <td valign="bottom" class="copy10grey"  >
                                        <span width="100%" >
                                            <asp:LinkButton ID="lnkRmaNum" CommandArgument='<%# Eval("TemprmaNumber") +","+Eval("RmaNumber") %>' OnCommand="lnkRmaNum_OnCommand" runat="server"><%# Eval("TemprmaNumber")%></asp:LinkButton>
                                                

                                            <%--<%# Convert.ToString(Eval("FulfillmentNumber")) == "" ? "red" : ""%>  
                                             
                                             
                                             style="background-color:<%# Convert.ToString(Eval("SalesOrderNumber")) == "" ? "red" : ""%>"  
                                            --%>
                                            </span>
                                        </td>
                                        <td valign="bottom" class="copy10grey"  style="background-color:<%# Convert.ToString(Eval("rmanumber")) == "" ? "red" : ""%>">
                                        <span width="100%">
                                            <%# Eval("rmanumber")%> 
                                            </span>
                                        </td>
                                        <td valign="bottom" class="copy10grey"  style="background-color:<%# Convert.ToDateTime( Eval("rmaDate")).ToShortDateString() == "" ? "red" : ""%>">
                                        <span width="100%">
                                            <%# Convert.ToDateTime(Eval("rmaDate")).ToShortDateString() %>    
                                            </span>
                                        </td>
                                        <td valign="bottom" class="copy10grey"  style="background-color:<%# Convert.ToString(Eval("RmaContactName")) == "" ? "red" : ""%>">
                                        <span width="100%">
                                            <%# Eval("RmaContactName")%> 
                                            </span>
                                        </td>
                                        <td valign="bottom" class="copy10grey"  style="background-color:<%# Convert.ToString(Eval("address")) == "" ? "red" : ""%>">
                                        <span width="100%">
                                            <%# Eval("address")%> 
                                            </span>
                                        </td>
                                        
                                        <td valign="bottom" class="copy10grey"  style="background-color:<%# Convert.ToString(Eval("City")) == "" ? "red" : ""%>">
                                        <span width="100%">
                                            <%# Eval("city")%>    
                                            </span>
                                        </td>
                                        <td valign="bottom" class="copy10grey"  style="background-color:<%# Convert.ToString(Eval("State")) == "" ? "red" : ""%>">
                                        <span width="100%">
                                            <%# Eval("State")%>    
                                            </span>
                                        </td>
                                        <td valign="bottom" class="copy10grey"  style="background-color:<%# Convert.ToString(Eval("zip")) == "" ? "" : ""%>">
                                        <span width="100%">
                                            <%# Eval("Zip")%>    
                                            </span>
                                        </td>
                                        <td valign="bottom" class="copy10grey"  style="background-color:<%# Convert.ToString(Eval("email")) == "" ? "red" : ""%>">
                                        <span width="100%">
                                            <%# Eval("email")%>    
                                            </span>
                                        </td>
                                        <td valign="bottom" class="copy10grey"  style="background-color:<%# Convert.ToString(Eval("phone")) == "" ? "red" : ""%>">
                                        <span width="100%">
                                            <%# Eval("phone")%>    
                                            </span>
                                        </td>
                                        <%--<td valign="bottom" class="copy10grey"  style="background-color:<%# Convert.ToString(Eval("RmaDetails.esn")) == "" ? "red" : ""%>">
                                        <span width="100%">
                                            <%# Eval("RmaDetails.esn")%>    
                                            </span>
                                        </td>
                                        <td valign="bottom" class="copy10grey"  style="background-color:<%# Convert.ToDateTime( Eval("RmaDetails.RecievedOn")).ToShortDateString() == "" ? "red" : ""%>">
                                        <span width="100%">
                                            <%# Convert.ToDateTime(Eval("RmaDetails.RecievedOn")).ToShortDateString()%>    
                                            </span>
                                        </td>
                                        <td valign="bottom" class="copy10grey"  style="background-color:<%# Convert.ToString(Eval("RmaDetails.Reason")) == "" ? "red" : ""%>">
                                        <span width="100%">
                                            <%# Eval("RmaDetails.Reason")%>    
                                            </span>
                                        </td>--%>
                                        
                                        <td valign="bottom" class="copy10grey"  >
                                        <span width="100%">
                                            <%# Eval("Comment")%>    
                                            </span>
                                        
                                        </td>
                                        <%--<td valign="bottom" class="copy10grey"  >
                                        <span width="100%">
                                            <%# Eval("CallTime")%>    
                                            </span>
                                        
                                        </td>
                                        <td valign="bottom" class="copy10grey"  >
                                        <span width="100%">
                                            <%# Convert.ToString(Eval("Warranty")) == "1" ? "Warranty" : Convert.ToString(Eval("Warranty")) == "2" ? "Out of Warranty": "" %>    
                                            </span>
                                        
                                        </td>
                                        
                                        <td valign="bottom" class="copy10grey"  >
                                        <span width="100%">
                                            <%# Convert.ToDateTime(Eval("WarrantyExpieryDate")).ToShortDateString()%>    
                                            </span>
                                        </td>
                                        
                                        <td valign="bottom" class="copy10grey"  >
                                        <span width="100%">
                                            <%# Eval("Comments")%>    
                                            </span>
                                        
                                        </td>--%>
                                        
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
                                &nbsp;<asp:Button ID="btnValidate" Visible="false" Width="190px"  CssClass="button" runat="server" OnClick="btnValidates_Click" Text="Validate" />
                                
                                &nbsp;<asp:Button ID="btnSubmit" Visible="false" Width="190px"  CssClass="button" runat="server" OnClick="btnSubmit_Click" Text="Submit" />
                                &nbsp;<asp:Button ID="btnView" Visible="false" Width="190px"  CssClass="button" runat="server" OnClick="btnViewAssignedRmas_Click" Text="View uploaded RMA" />
                                
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
            </Triggers>
            </asp:UpdatePanel>
    
    <div id="divFulfillmentContainer">
			<div id="divFulfillmentView" style="display:none">
					
				<asp:UpdatePanel ID="upnlView" runat="server">
					<ContentTemplate>
                   
						<asp:PlaceHolder ID="phrView" runat="server">
                            <%--<asp:Label ID="lblViewPO" runat="server"  CssClass="errormessage"></asp:Label>
    --%>
                            <asp:Panel ID="pnlRMA" runat="server">
                            

                            </asp:Panel>


                        </asp:PlaceHolder>
						
						
					</ContentTemplate>
							
				</asp:UpdatePanel>
           </div>
           <div id="divReason" style="display:none">
					
				<asp:UpdatePanel ID="upReason" runat="server">
					<ContentTemplate>
                   
						<asp:PlaceHolder ID="phReason" runat="server">
                            
                            <asp:Panel ID="pnlReason" runat="server">
                            
                                <Rma:Reasons ID="reason1" runat="server" />
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
                            <WC:State ID="state1" runat="server" />
                               
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
