<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AssignTracking.aspx.cs" Inherits="avii.Admin.AssignTracking" %>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="/Controls/Header.ascx" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="/Controls/Footer.ascx" %>
<%@ Register TagPrefix="Po" TagName="ShipVia" Src="~/Controls/PoShipVia.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>.:: Lan Global inc. Inc. - Assign Tracking ::.</title>

    <link href="../aerostyle.css" rel="stylesheet" type="text/css"/>
    
    <!-- from http://encosia.com/2009/10/11/do-you-know-about-this-undocumented-google-cdn-feature/ -->
    <link href="https://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/themes/start/jquery-ui.css" type="text/css" rel="Stylesheet" />
	
	<!-- from http://encosia.com/2009/10/11/do-you-know-about-this-undocumented-google-cdn-feature/ -->
    <link href="https://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/themes/start/jquery-ui.css" type="text/css" rel="Stylesheet" />
	<%--
	<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.4.0/jquery.min.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/jquery-ui.min.js"></script>--%>
    
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


	        $("#divShipVia").dialog({
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
	    });


	    function closeShipViaDialog() {
	        //Could cause an infinite loop because of "on close handling"
	        $("#divShipVia").dialog('close');
	    }
	    function openShipViaDialog(title, linkID) {

	        var pos = $("#" + linkID).position();
	        var top = pos.top;
	        var left = pos.left + $("#" + linkID).width() + 10;
	        //alert(left);
	        top = top - 300;
	        left = 300;
	        $("#divShipVia").dialog("option", "title", title);
	        $("#divShipVia").dialog("option", "position", [left, top]);

	        $("#divShipVia").dialog('open');
	    }
	    function openShipViaDialogAndBlock(title, linkID) {
	        openShipViaDialog(title, linkID);

	        //block it to clean out the data
	        $("#divShipVia").block({
	            message: '<img src="../images/async.gif" />',
	            css: { border: '0px' },
	            fadeIn: 0,
	            //fadeOut: 0,
	            overlayCSS: { backgroundColor: '#ffffff', opacity: 1 }
	        });
	    }
	    function unblockShipViaDialog() {
	        $("#divShipVia").unblock();
	    }
	    



        </script>


     <link rel="stylesheet"  type="text/css" href="../fullfillment/calendar/dhtmlgoodies_calendar.css" media="screen" />
		<script type="text/javascript" src="../fullfillment/calendar/dhtmlgoodies_calendar.js"></script>  
		

    <script language="javascript" type="text/javascript">
        function set_focus2() {
            var img = document.getElementById("imgShipTo");
            var st = document.getElementById("dpShipBy");
           
            img.click();
             st.focus();
        }
        function Validate() {
            
            var company = document.getElementById("<% =dpCompany.ClientID %>");
            //alert(company);
            if (company != 'null' && company.selectedIndex == 0) {
                alert('Customer is required!');
                return false;

            }

        }
        function IsValidate(obj) {
            if (obj.checked) {
                var isTrue = confirm('This will remove shipment label from fulfillment order. Do you want to continue?');
                obj.checked = isTrue;
            }
        }
		
    </script>
    
</head>
<body leftmargin="0" topmargin="0" marginwidth="0" marginheight="0">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    
    <table cellSpacing="0" cellPadding="0" align="center" width="100%" border="0">
        <tr>
			<td>
                <head:MenuHeader id="HeadAdmin" runat="server"></head:MenuHeader>
            </td>
		</tr>
		<tr><td>&nbsp;</td></tr>
		<tr>
			<td  bgcolor="#dee7f6" class="button">
            &nbsp;&nbsp;Assign Tacking
			</td>
		</tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
    </table>
<asp:UpdatePanel ID="UpdatePanel1"  runat="server" UpdateMode="Conditional" >
            <ContentTemplate>
    
	
    <table cellSpacing="0" cellPadding="0" align="center" width="100%" border="0">
    <tr>                    
        <td >
            <asp:Label ID="lblMsg" runat="server" Width="100%" CssClass="errormessage"></asp:Label>
        </td>
    
    </tr> 
    <tr><td class="copy10grey" align="left">&nbsp;
	                    - Upload file should be less than 2 MB. <br />&nbsp;
                        - Bold columns are required. <br />&nbsp;
                        - Shipdate can not be 3 day before  from current date <br />&nbsp;
                        - Shipdate can not be more than current date <br />&nbsp;
        </td>
    </tr>  
    </table>  

    <table bordercolor="#839abf" border="1" width="100%" align="center" cellpadding="0" cellspacing="0">
    <tr>
        <td>
         
      <table width="100%" border="0" class="box" align="center" cellpadding="2" cellspacing="2">
         <tr style="height:1px">
         <td style="height:1px"></td>
         </tr>
            <tr valign="top">
                <td class="copy10grey" align="right" width="35%">
                    Customer Name: &nbsp;
                </td>
                <td width="65%" align="left">
                &nbsp;<asp:DropDownList ID="dpCompany" runat="server" AutoPostBack="false"  
                Width="50%"
                 class="copy10grey"  >
                </asp:DropDownList>  
                
                </td>
                
            </tr> 
            <tr>
                <td  class="copy10grey" align="right" >
                    Ship Date: &nbsp;</td>
                <td align="left" >
                    <asp:TextBox ID="txtShipDate" runat="server" onfocus="set_focus2();"  CssClass="copy10grey" MaxLength="12"  Width="50%"></asp:TextBox>
                    <img id="imgShipTo" alt="" onclick="document.getElementById('<%=txtShipDate.ClientID%>').value = ''; displayCalendar(document.getElementById('<%=txtShipDate.ClientID%>'),'mm/dd/yyyy',this,true);" src="../fullfillment/calendar/sscalendar.jpg" />
                                        
                </td>
            </tr>
            <tr >
                <td  class="copy10grey" align="right"  >
                    Ship Via: &nbsp;</td>
                <td align="left" >
                    <asp:DropDownList ID="dpShipBy" CssClass="copy10grey" runat="server" Width="50%" >
					</asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td  class="copy10grey" align="right" >
                    Upload Tracking file: &nbsp;</td>
                <td align="left" >
                    &nbsp;<asp:FileUpload ID="flnUpload" runat="server" CssClass="txfield1" Width="50%" />

                </td>
            </tr>
            <tr  >
                <td class="copy10grey" align="right">
                    File format sample: &nbsp;
                            </td>
                <td class="copy10grey" align="left">
                    &nbsp;<b>FulfillmentNumber,TrackingNumber</b>
                    

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
 
                <tr  >
                <td class="copy10grey" align="right">
                     &nbsp;
                            </td>
                <td class="copy10grey" align="left">
                    <asp:CheckBox ID="chkDelete" CssClass="copy10grey" Text="Remove shipment label from fulfillment order" runat="server" onclick="return IsValidate(this);" />

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
                                <td align="left">
                                    <asp:Label ID="lblConfirm" runat="server" Width="100%" CssClass="errorGreenMsg"></asp:Label></td>
                            </tr>               
                                    
                                <tr>
                                <td  align="center">
                                
                                
                                <asp:Button ID="btnSubmit2" Visible="false" Width="190px"  CssClass="button" runat="server" OnClick="btnSubmit_Click" Text="Submit" OnClientClick="return Validate(2);" />

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
                <tr>
                    <td colspan="2">
                            
                    <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                        <td colspan="2" align="right" style="height:8px; vertical-align:bottom">
                        <strong>   <asp:Label ID="lblCount" CssClass="copy10grey" runat="server" ></asp:Label> &nbsp;&nbsp;</strong> 
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="center">
                                <asp:GridView ID="rptTracking" runat="server" Width="100%" GridLines="Both"   AutoGenerateColumns="false"
                                    AllowPaging="false" 
                                     ShowFooter="false" >
                                        <RowStyle BackColor="Gainsboro" />
                                        <AlternatingRowStyle BackColor="white" />
                                        <HeaderStyle  CssClass="button" ForeColor="white"/>
                                        <FooterStyle CssClass="white"  />
                                        <PagerStyle ForeColor="#636363" CssClass="copy10grey" HorizontalAlign="Left" />
                                        <Columns>
                                            <%--<asp:TemplateField ItemStyle-CssClass="copy10grey"  ItemStyle-Width="3%">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk"  runat="server" />
                                            </ItemTemplate>
                                            </asp:TemplateField>  --%>
                                            <asp:TemplateField HeaderText="S.No." ItemStyle-HorizontalAlign="Left"  ItemStyle-CssClass="copy10grey" ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex +  1 %>
                                                </ItemTemplate>
                                                
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Fulfillment#" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                    <%#Eval("FulfillmentNumber")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            
                                            <%--<asp:TemplateField HeaderText="ESN" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                    <%#Eval("ESN")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            
                                            <asp:TemplateField HeaderText="Tracking#" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                    <%#Eval("Tracking")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <%--<asp:TemplateField HeaderText="SKU#" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                    <%#Eval("SKU")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                           <%-- <asp:TemplateField HeaderText="ShipViaCode" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                    <%#Eval("ShipViaCode")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            

                                            
                                            <asp:TemplateField HeaderText="AVSONUMBER" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                    <%#Eval("AVSONUMBER")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            --%>
                                            
                                            

                            
                                        </Columns>
                                    </asp:GridView>
                                    

                    </td>
                        </tr>         
                    </table>
                    </td>
                </tr>
                <tr><td colspan="2">
                            <hr style="width:100%" />
                            
                            </td></tr>                            
                             <tr>
                                <td colspan="2" align="center">
                                
                                <asp:Button ID="btnUpload"  Width="190px" CssClass="button" runat="server" OnClick="btnValidateUploadedFile_Click" Text="Validate Uploaded file" OnClientClick="return Validate(1);"  />

                                &nbsp;<asp:Button ID="btnSubmit" Visible="false" Width="190px"  CssClass="button" runat="server" OnClick="btnSubmit_Click" Text="Submit" OnClientClick="return Validate(2);"/>
                               <%-- &nbsp;<asp:Button ID="btnViewTracking" Visible="false" Width="190px"  CssClass="button" runat="server" OnClick="btnViewAssignedPos_Click" Text="View UPDATED fulfillment" />
--%>
                                &nbsp;<asp:Button ID="btnCancel" runat="server"  CssClass="button" Text="Cancel" OnClick="btnCancel_Click" />
                                    </td>
                            </tr>
        </table>
        </td>
    </tr>
    </table>
   
        
           

           </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnUpload" />
                
            </Triggers>
            </asp:UpdatePanel>

            <div id="divFulfillmentContainer">
	        <div id="divShipVia" style="display:none">
					
				<asp:UpdatePanel ID="upShipvia" runat="server">
					<ContentTemplate>
                   
						<asp:PlaceHolder ID="phShipvia" runat="server">
                            <%--<asp:Label ID="lblViewPO" runat="server"  CssClass="errormessage"></asp:Label>
    --%>
                            <asp:Panel ID="pnlShipvia" runat="server">
                            
                                <Po:ShipVia ID="shipvia1" runat="server" />
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

        
            <asp:UpdateProgress ID="UpdateProgress1" runat="server"
                     DynamicLayout="false">
            <ProgressTemplate>
                <img src="/Images/ajax-loaders.gif" /> Loading ...
            </ProgressTemplate>
            </asp:UpdateProgress>

    
    </form>
</body>
</html>
