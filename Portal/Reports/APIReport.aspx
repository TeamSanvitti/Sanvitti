<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="APIReport.aspx.cs" Inherits="avii.Reports.APIReport" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>
<%@ Register TagPrefix="menu" TagName="Menu" Src="~/Controls/Header.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>API Log</title>
     
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
	</style>
   <script type="text/javascript">

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

       

       function set_focus1() {
           var img = document.getElementById("img1");
           var st = document.getElementById("ddlModule");
           st.focus();
           img.click();
       }
       function set_focus2() {
           var img = document.getElementById("img2");
           var st = document.getElementById("ddlModule");
           st.focus();
           img.click();
       }

        </script>
	
   		<link rel="stylesheet"  type="text/css" href="../fullfillment/calendar/dhtmlgoodies_calendar.css" media="screen" />
		<script type="text/javascript" src="../fullfillment/calendar/dhtmlgoodies_calendar.js"></script>  
	    
</head>
<body bgcolor="#ffffff" leftmargin="0" rightmargin="0" topmargin="0">
    <form id="form1" runat="server">
        <table cellspacing="0" cellpadding="0" border="0" align="center" width="100%">
    <tr>
        <td>
            <menu:Menu ID="menu1" runat="server" ></menu:Menu>
        </td>
     </tr>
     </table>
     <table cellspacing="0" cellpadding="0" border="0" align="center" width="95%">
        
        <tr valign="top">
           
            <td   >
    <table align="center" style="text-align:left" width="100%">
                <tr class="buttonlabel" align="left">
                <td>&nbsp;API Log Report</td></tr>
             </table>
     
      <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
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
      <asp:UpdatePanel ID="UpdatePanel1"  runat="server"  UpdateMode="Conditional" >
     <ContentTemplate>
     <table  align="center" style="text-align:left" width="99%">
     <tr>
        <td>
           <asp:Label ID="lblMsg" runat="server" CssClass="errormessage"></asp:Label>
            
        </td>
     </tr>
     </table>
     
           
      <table bordercolor="#839abf" border="1" width="100%" align="center" cellpadding="0" cellspacing="0">
      <tr>
        <td>
         <asp:Panel  ID="pnlSearch" runat="server"  DefaultButton="btnSearch" >
     <table width="100%" border="0" class="box" align="center" cellpadding="2" cellspacing="2">
     <tr style="height:1px">
     <td style="height:1px"></td>
     </tr>
     
           <tr id="trCustomer" runat="server">
                <td class="copy10grey"  align="right">
                    Customer Name:
                </td>
                <td>
                <asp:DropDownList ID="dpCompany" CssClass="copy10grey" runat="server" Width="80%" >
									</asp:DropDownList>
                
                </td>
                <td  width="1%">
                    &nbsp;
                </td>
                <td class="copy10grey"  align="right">
                </td>
                <td>
                
                </td>   
                </tr>
            
            <tr>
                <td class="copy10grey"  align="right">
                    From Date:
                </td>
                <td>
                <asp:TextBox ID="txtFromDate"  onfocus="set_focus1();" CssClass="copy10grey" runat="server" Width="80%" MaxLength="15" ></asp:TextBox>
                <img id="img1" alt=""  onclick="document.getElementById('<%=txtFromDate.ClientID%>').value = ''; displayCalendar(document.getElementById('<%=txtFromDate.ClientID%>'),'mm/dd/yyyy',this,true);" src="../fullfillment/calendar/sscalendar.jpg" />
                
                </td>
                <td  width="1%">
                    &nbsp;
                </td>
                <td class="copy10grey"  align="right">
                    End Date:
                </td>
                <td>
                <asp:TextBox ID="txtEndDate"  onfocus="set_focus2();" CssClass="copy10grey" runat="server" Width="70%" MaxLength="12" ></asp:TextBox>
                <img id="img2" alt=""  onclick="document.getElementById('<%=txtEndDate.ClientID%>').value = ''; displayCalendar(document.getElementById('<%=txtEndDate.ClientID%>'),'mm/dd/yyyy',this,true);" src="../fullfillment/calendar/sscalendar.jpg" />
                
                </td>   
                </tr>
                
            <tr>
                <td class="copy10grey"  align="right">
                    Request Data:
                </td>
                <td>
                    <asp:TextBox ID="txtRequest"   CssClass="copy10grey" runat="server" Width="80%" ></asp:TextBox>
                
                </td>
                <td  width="1%">
                    &nbsp;
                </td>
                <td class="copy10grey"  align="right">
                    Response Data:
                </td>
                <td>
                    <asp:TextBox ID="txtResponse"   CssClass="copy10grey" runat="server" Width="70%" ></asp:TextBox>
                    </td>   
                </tr>
                
                <tr>
                <td class="copy10grey"  align="right">
                    Module Name:
                </td>
                <td>
                    <asp:DropDownList ID="ddlModule"   CssClass="copy10grey" runat="server" Width="80%" >
                        <asp:ListItem Text="" Value=""></asp:ListItem>
                        <asp:ListItem Text="addPurchaseOrder" Value="addPurchaseOrder"></asp:ListItem>
                        <asp:ListItem Text="CancelFulfillment" Value="CancelFulfillment"></asp:ListItem>
                        <asp:ListItem Text="CancelRMA" Value="CancelRMA"></asp:ListItem>
                        <asp:ListItem Text="GetAssignedUsers" Value="GetAssignedUsers"></asp:ListItem>
                        <asp:ListItem Text="GetCompanyStores" Value="GetCompanyStores"></asp:ListItem>
                        <asp:ListItem Text="GetEsnRepositoryList" Value="GetEsnRepositoryList"></asp:ListItem>
                        <asp:ListItem Text="GetInventorySKU" Value="GetInventorySKU"></asp:ListItem>
                        <asp:ListItem Text="GetInventoryStockCurrent" Value="GetInventoryStockCurrent"></asp:ListItem>
                        <asp:ListItem Text="GetInventoryStockFlow" Value="GetInventoryStockFlow"></asp:ListItem>
                        <asp:ListItem Text="GetPurchaseOrder" Value="GetPurchaseOrder"></asp:ListItem>
                        <asp:ListItem Text="GetPurchaseOrderProvisioning" Value="GetPurchaseOrderProvisioning"></asp:ListItem>
                        <asp:ListItem Text="GetPurchaseOrderShipment" Value="GetPurchaseOrderShipment"></asp:ListItem>
                        <asp:ListItem Text="GetPurchaseOrderShipmentToBeSent" Value="GetPurchaseOrderShipmentToBeSent"></asp:ListItem>
                        <asp:ListItem Text="GetRMA" Value="GetRMA"></asp:ListItem>
                        <asp:ListItem Text="GetRmaEsnDetail" Value="GetRmaEsnDetail"></asp:ListItem>
                        <asp:ListItem Text="GetRmaEsnListing" Value="GetRmaEsnListing"></asp:ListItem>
                        <asp:ListItem Text="GetShippingCodes" Value="GetShippingCodes"></asp:ListItem>
                        <asp:ListItem Text="SetPurchaseOrderShipment" Value="SetPurchaseOrderShipment"></asp:ListItem>
                        <asp:ListItem Text="SetRMA" Value="SetRMA"></asp:ListItem>
                        <asp:ListItem Text="ShipmentTracking" Value="ShipmentTracking"></asp:ListItem>
                         
                        <asp:ListItem Text="UpdatePurchaseOrder" Value="UpdatePurchaseOrder"></asp:ListItem>
                    </asp:DropDownList>
                
                </td>
                <td  width="1%">
                    &nbsp;
                </td>
                <td class="copy10grey"  align="right">
                    Status:
                </td>
                <td>
                    <asp:DropDownList ID="ddlStatus"   CssClass="copy10grey" runat="server" Width="70%" >
                        <asp:ListItem Text="" Value="-1"></asp:ListItem>
                        <asp:ListItem Text="Exception" Value="Exception"></asp:ListItem>
                        <asp:ListItem Text="Success" Value="Success"></asp:ListItem>
                    </asp:DropDownList>
                
                </td>   
                </tr>
                
                <tr>
                <td colspan="5">
                <hr />
                </td>
                </tr>
                <tr>
                <td  align="center"  colspan="5">
            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="button" OnClick="btnSearch_Click" CausesValidation="false"/>
            &nbsp;
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="button" OnClick="btnCancel_Click" CausesValidation="false"/>
            
        
        </td>
        </tr>
            </table>
            </asp:Panel>
   
     </td>
     </tr>
     </table>           
                
        
                   
        <table align="center" width="100%" border="0" cellpadding="0" cellspacing="0">
    <tr>
        <td align="right">&nbsp;    <strong>   
                <asp:Label ID="lblCount" CssClass="copy10grey" runat="server" ></asp:Label> &nbsp;&nbsp;
            </strong>               
         </td>
    </tr>
    <tr>
        <td>
        
        
    <asp:GridView runat="server" ID="gvLog" AutoGenerateColumns="False" 
     PageSize="50" AllowPaging="true" Width="100%" OnPageIndexChanging="gvLog_PageIndexChanging"   
     CellPadding="3" OnRowDataBound="gvLog_RowDataBound" AllowSorting="true" OnSorting="gvLog_Sorting"
    GridLines="Vertical" DataKeyNames="LogID"  >
    <RowStyle  CssClass="copy10grey" BackColor="Gainsboro" />
    <AlternatingRowStyle BackColor="white" />
    <HeaderStyle  CssClass="buttongrid" ForeColor="white"/>
    <PagerStyle ForeColor="#636363" CssClass="copy10grey" />
    <%-- <FooterStyle CssClass="white"  />--%>
    <Columns>
        <asp:TemplateField HeaderText="S.No." ItemStyle-CssClass="copy10grey"  ItemStyle-Width="1%" HeaderStyle-CssClass="buttongrid">
            <ItemTemplate>

                    <%# Container.DataItemIndex + 1%>
                  
            </ItemTemplate>
        </asp:TemplateField> 

        <asp:TemplateField HeaderText="Module Name" ItemStyle-Width="10%" SortExpression="ModuleName"  HeaderStyle-CssClass="buttonundlinelabel">
            <ItemTemplate>
                <%# Eval("ModuleName") %>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Request TimeStamp" ItemStyle-Width="10%" SortExpression="RequestTimeStamp" HeaderStyle-CssClass="buttonundlinelabel">
            <ItemTemplate>
                <%# Convert.ToDateTime(Eval("RequestTimeStamp")).ToString("yyyy-MM-dd HH:mm:ss.fff") %>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Response TimeStamp" ItemStyle-Width="10%" SortExpression="ResponseTimeStamp" HeaderStyle-CssClass="buttonundlinelabel">
            <ItemTemplate>
                  <%# Convert.ToDateTime(Eval("ResponseTimeStamp")).ToString("yyyy-MM-dd HH:mm:ss.fff") %>
                <%--<%# Eval("ResponseTimeStamp") %>--%>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Execution Time(ms)" ItemStyle-Width="10%" HeaderStyle-CssClass="buttongrid">
            <ItemTemplate>
                <%# Eval("TimeDifference") %>
            </ItemTemplate>
        </asp:TemplateField>
        
        <asp:TemplateField HeaderText="Request Data" ItemStyle-Width="10%" HeaderStyle-CssClass="buttongrid">
            <ItemTemplate>
              
                <asp:LinkButton ToolTip="See more..." CausesValidation="false" OnCommand="lnkRequest_Click" CommandArgument='<%# Eval("LogID") %>'  
                 ID="lnkRequest"  runat="server"  >RequestData
                </asp:LinkButton>
                
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Response Data" ItemStyle-Width="10%" HeaderStyle-CssClass="buttongrid">
            <ItemTemplate>
                 
                 <asp:LinkButton ToolTip="See more..." CausesValidation="false" OnCommand="lnkResponse_Click" CommandArgument='<%# Eval("LogID") %>'  
                 ID="lnkResponse"  runat="server"  >
                     ResponseData
                 </asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
        
        <asp:TemplateField HeaderText="Return Message" ItemStyle-Width="10%" SortExpression="ReturnMessage" HeaderStyle-CssClass="buttonundlinelabel">
            <ItemTemplate>
                <%# Eval("ReturnMessage") %>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Status" ItemStyle-Width="5%" SortExpression="ExceptionOccured" HeaderStyle-CssClass="buttonundlinelabel">
            <ItemTemplate>
                <%# Convert.ToBoolean(Eval("ExceptionOccured")) == true ? "Exception" : "Success" %>
            </ItemTemplate>
        </asp:TemplateField>
         
        
        </Columns>
        </asp:GridView>
   
        </td>
    </tr>
       
    </table>
    
         
     
     </div>

<%--     <script type='text/javascript'>


         prm = Sys.WebForms.PageRequestManager.getInstance();
         prm.add_endRequest(EndRequest);
         function EndRequest(sender, args) {
             //alert("EndRequest");
             $(document).AjaxReady();
         }
        </script>--%>
     </ContentTemplate>
     </asp:UpdatePanel>
     
    </td>
    </tr>
    <tr>
        <td>
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
        </td>
    </tr>
    </table>
    <br />
    <foot:MenuFooter ID="footer1" runat="server"></foot:MenuFooter>
      <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.4.0/jquery.min.js"></script>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/jquery-ui.min.js"></script>
        <script type="text/javascript" src="../JQuery/jquery.blockUI.js"></script>
        <script type="text/javascript" src="/JSLibrary/jquery.blockUI.js"></script>
		
    </form>
</body>
</html>
