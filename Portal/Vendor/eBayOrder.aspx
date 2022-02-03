<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="eBayOrder.aspx.cs" Async="true" Inherits="avii.Vendor.eBayOrder" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="/Controls/Footer.ascx" %>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="/Controls/Header.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>eBay Order</title>

     <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>

    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.4.0/jquery.min.js"></script>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/jquery-ui.min.js"></script>
    <link href="https://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/themes/start/jquery-ui.css"
    rel="stylesheet" type="text/css" />
    <link rel="stylesheet"  type="text/css" href="../fullfillment/calendar/dhtmlgoodies_calendar.css" media="screen" />
	<script type="text/javascript" src="../fullfillment/calendar/dhtmlgoodies_calendar.js"></script>  
    <script>
        function OpenNewPage(url) {

            var newWin = window.open(url);

            if (!newWin || newWin.closed || typeof newWin.closed == 'undefined') {
                alert('your pop up blocker is enabled');

                //POPUP BLOCKED
            }
        }
        function SelectAll(id) {
            // alert(document.getElementById(id).checked);
            var check = document.getElementById(id).checked;
            // alert(check);

            var elements = document.getElementsByTagName('input');
            // iterate and change status
            for (var i = elements.length; i--;) {
                if (elements[i].type == 'checkbox') {
                    elements[i].checked = check;
                }
            }
            // $(':checkbox').prop('checked', check);



        }
        function set_focus1() {
            var img = document.getElementById("imgDateFrom");
            var st = document.getElementById("ddlStatus");
            st.focus();
            img.click();
        }
        function set_focus2() {
            var img = document.getElementById("imgDateTo");
            var st = document.getElementById("ddlStatus");
            st.focus();
            img.click();
        }
        function validate() {
            var before30Date = Date.today().add(-30).days();
           // alert(before30Date);

            var currentDate = Date.today();
            var fromDate = document.getElementById("txtDateFrom").value;
            var toDate = document.getElementById("txtDateTo").value;
           // alert(fromDate);
            
            if (fromDate < before30Date)
            {
                alert('From date cannot be before 30 days')
                return false;
            }
            if (toDate > currentDate) {
                alert('To date cannot be greater than current date')
                return false;
            }
        }

    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#divLineItem").dialog({
                autoOpen: false,
                modal: false,
                minHeight: 400,
                height: 450,
                width: 1200,
                resizable: false,
                open: function (event, ui) {
                    $(this).parent().appendTo("#divContainer");
                }
            });



        });


        function closeDialog() {
            //Could cause an infinite loop because of "on close handling"
            $("#divLineItem").dialog('close');
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
            left = 100;
            $("#divLineItem").dialog("option", "title", title);
            $("#divLineItem").dialog("option", "position", [left, top]);
            $("#divLineItem").dialog('open');

        }


        function openDialogAndBlock(title, linkID) {

            openDialog(title, linkID);
            //alert('2')
            //block it to clean out the data
            $("#divLineItem").block({
                message: '<img src="../images/async.gif" />',
                css: { border: '0px' },
                fadeIn: 0,
                //fadeOut: 0,
                overlayCSS: { backgroundColor: '#ffffff', opacity: 1 }
            });
        }

        function unblockDialog() {
            $("#divLineItem").unblock();
        }


    </script>
</head>
<body bgColor="#ffffff" leftmargin="0" rightmargin="0" topmargin="0">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
	<table cellSpacing="0" cellPadding="0"  border="0" align="center" width="100%">
		<tr>
			<td>
			<head:menuheader id="HeadAdmin" runat="server"></head:menuheader>
			</td>
		</tr>
     </table>
    <table  cellSpacing="1" cellPadding="1" width="95%" align="center" >
        <tr>
		    <td colSpan="6" bgcolor="#dee7f6" class="buttonlabel">&nbsp;&nbsp;eBay Order
		    </td>
        </tr>
    </table> 

        <div id="divContainer">	
            
			<div id="divLineItem" style="display:none">
					
				<asp:UpdatePanel ID="upnlESN" runat="server">
					<ContentTemplate>
                   
						<asp:PlaceHolder ID="phrESN" runat="server">
                        <table width="100%" border="0">
                                <tr>
                                    <td>
                                        <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="100%">
                                        <tr>
                                            <td>
			            
                                                <table cellSpacing="5" cellPadding="5" width="100%" border="0">
                                                <tr>
                                                    <td width="15%" class="copy10grey" >
                                                        Order Id:
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblOrderId" runat="server"  CssClass="copy10grey"></asp:Label>
                                                    </td>
                                                </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        </table>
                                </tr>
                                <tr>
                                    <td >
                                    
                                    <asp:Panel ID="pnLineItem" runat="server" Width="100%">
                                        <asp:Label ID="lblEsnMsg" runat="server" Width="100%" CssClass="errormessage"></asp:Label>
                                          <asp:Repeater ID="rptItems" runat="server" Visible="true" >
                                            <HeaderTemplate>
                                            <table bordercolor="#839abf" border="1" width="100%" cellpadding="1" cellspacing="1">
                                            <tr>
                                                <td class="buttonlabel"  width="1%" >
                                                    S.No.
                                                </td>
                                                <td class="buttonlabel"  width="10%">
                                                    lineItemId
                                                </td>
                                                <td class="buttonlabel"  width="20%">
                                                    SKU
                                                </td>
                                                <td class="buttonlabel"  width="20%">
                                                    Title
                                                </td>
                                                <td class="buttonlabel"  width="10%">
                                                    Quantity
                                                </td>
                                                <td class="buttonlabel"  width="10%">
                                                    Status
                                                </td>
                                                <td class="buttonlabel"  width="10%">
                                                    Error
                                                </td>
                                            </tr>
                                            </HeaderTemplate>
                                            <ItemTemplate>
    
                                                <tr valign="bottom"  class="<%# Container.ItemIndex % 2 == 0 ? "alternaterow" : "" %>">
                                                    <td class="copy10grey"  >
                                                    <%# Container.ItemIndex +  1 %>
                                                    </td>
                                                    <td valign="bottom" class="copy10grey"  >
                                                    <span width="100%">
                                                        <%# Eval("lineItemId")%>    
                                                        </span>
                                                    </td>
                                                    <td valign="bottom" class="copy10grey"  >
                                                    <span width="100%">
                                                        <%# Eval("sku")%>    
                                                        </span>
                                                    </td>
                                                    <td valign="bottom" class="copy10grey"  >
                                                    <span width="100%">
                                                        <%# Eval("Title")%>    
                                                        </span>
                                                    </td>
                                                    <td valign="bottom" class="copy10grey"  >
                                                    <span width="100%">
                                                        <%# Eval("Quantity")%>    
                                                        </span>
                                                    </td>
                                                    <td valign="bottom" class="copy10grey"  >
                                                    <span width="100%">
                                                        <%# Eval("lineItemFulfillmentStatus")%>    
                                                        </span>
                                                    </td>
                                                    <td valign="bottom" class="errormessage"  >
                                                    <span width="100%" class="errormessage">
                                                        <%# Eval("listingMarketplaceId")%>    
                                                        </span>
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

        <table cellspacing="0" cellpadding="0" border="0" align="center" width="100%">
        <tr>
            <td >
     <asp:UpdatePanel ID="UpdatePanel1"  runat="server" UpdateMode="Conditional" >
        <ContentTemplate>   
        <table cellSpacing="0" cellPadding="0" align="center" width="95%" border="0">
    	<tr>                    
            <td colspan="2">
                <asp:Label ID="lblMsg" runat="server" Width="100%" CssClass="errormessage"></asp:Label>
            </td>
        </tr>
        </table>
        <table cellSpacing="0" cellPadding="0" align="center" width="95%" border="0">    
         <tr>
                <td align="center">
                 <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="100%">
                    <tr>
                    <td>
			            <table id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
                        <tr>
                            <td colspan="5"> 
                                <asp:Panel ID="pnlSearch" runat="server" DefaultButton="btnSearch">
                        <table cellSpacing="5" cellPadding="5" width="100%" border="0">
                        
                         <tr valign="top">
                            <td class="copy10grey" align="right" width="10%">
                                <b> Date From:</b>
                            </td>
                            <td width="35%">
                                <asp:TextBox ID="txtDateFrom" runat="server" onfocus="set_focus1();"  CssClass="copy10grey" MaxLength="12"  Width="70%"></asp:TextBox>
                                <img id="imgDateFrom" alt="" onclick="document.getElementById('<%=txtDateFrom.ClientID%>').value = ''; displayCalendar(document.getElementById('<%=txtDateFrom.ClientID%>'),'mm/dd/yyyy',this,true);" src="../fullfillment/calendar/sscalendar.jpg" />
                                        
         
                            </td>
                            <td  width="1%">
                                &nbsp;
                            </td>
                            <td class="copy10grey" align="right" width="10%">
                               <b> Date To:</b>
                            </td>
                            <td >
                   
                              <asp:TextBox ID="txtDateTo" runat="server" onfocus="set_focus2();"  CssClass="copy10grey" MaxLength="12"  Width="55%"></asp:TextBox>
                                <img id="imgDateTo" alt="" onclick="document.getElementById('<%=txtDateTo.ClientID%>').value = ''; displayCalendar(document.getElementById('<%=txtDateTo.ClientID%>'),'mm/dd/yyyy',this,true);" src="../fullfillment/calendar/sscalendar.jpg" />
                                                
        
                            </td>   
                
                    
                            </tr>
                        <tr valign="top">
                             
                            <td class="copy10grey" align="right" width="10%">
                                <b> Status: </b>
                            </td>
                            <td width="35%">
                                    <asp:DropDownList ID="ddlStatus" runat="server" CssClass="copy10grey" Width="70%">
                                        <asp:ListItem Text="" Value=""></asp:ListItem>
                                        <asp:ListItem Selected="True" Text="NOT_STARTED" Value="NOT_STARTED|IN_PROGRESS"></asp:ListItem>
                                        <asp:ListItem Text="FULFILLED" Value="FULFILLED|IN_PROGRESS"></asp:ListItem>
                                    </asp:DropDownList>    
         
                            </td>
                            <td  width="1%">
                                &nbsp;
                            </td>
                                <td  class="copy10grey" align="right" width="10%">
                                    <b> Auth Token:</b> &nbsp;</td>
                                <td align="left" valign="top" >
                                     
                                    <table with="100%" border="0" cellSpacing="0" cellPadding="0" >
                                    <tr valign="top">
                                        <td align="left"  width="97%">
                                             <asp:TextBox    ID="txtToken" TextMode="MultiLine" Rows="4"  CssClass="copy10grey" runat="server" Width="98%"></asp:TextBox>
								
                                        </td>
                                        <td align="left" width="3%" valign="top">
                                             
                                        <asp:Button ID="btnGetToken" runat="server" Text="Get Token" CssClass="button" OnClick="btnGetToken_Click1" CausesValidation="false" />
                                
                                        </td>
                                    </tr>
                                </table>
                                
                                </td>
                                
                            </tr>
                            </table>
                                    </asp:Panel>
                            </td>
                        </tr>

                           <tr style="height:4px">
                <td colspan="5" cellSpacing="1" cellPadding="1" >
                <hr style="height:1px" />
                </td>
                </tr>
                <tr>
                <td  align="center"  colspan="5">
            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="button" OnClick="btnSearch_Click" OnClientClick="return validate();" CausesValidation="false"/>
            
         &nbsp;
           <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="button" OnClick="btnCancel_Click" CausesValidation="false"/>
                   
        </td>
        </tr>
                            </table>
                          </td>
                    </tr>
                           
                 </table>
                  <br />

                            
                        


                    </td>
                </tr>

            
       
    </table>
    <table cellSpacing="0" cellPadding="0" align="center" width="95%" border="0">
    <tr>
        <td align="left">
          <strong> <asp:Label ID="lblCount" CssClass="copy10grey" runat="server" ></asp:Label></strong>
                                
        </td>
        <td align="right">
            <asp:Button ID="btnValidate" runat="server" Text="Validate" Visible="false" CssClass="button" OnClick="btnValidate_Click" CausesValidation="false"/>
           &nbsp;
            <asp:Button ID="btnSubmit" runat="server" Text="Import" Visible="false" CssClass="button" OnClick="btnSubmit_Click" CausesValidation="false"/>
            
            
        </td>
    </tr>
    <tr>
        <td colspan="2" align="center">
        <asp:GridView ID="gvOrder" AutoGenerateColumns="false"   OnRowDataBound="gvOrder_RowDataBound"
        Width="100%" ShowHeader="true"  ShowFooter="false" runat="server" GridLines="Both"
        AllowPaging="true" OnPageIndexChanging="gvOrder_PageIndexChanging" PageSize="50"    >                        
        <RowStyle BackColor="Gainsboro" />
        <AlternatingRowStyle BackColor="white" />
        <HeaderStyle  CssClass="buttongrid" ForeColor="white"/>
            <PagerStyle ForeColor="#636363" CssClass="copy10grey" HorizontalAlign="Left" />
            <Columns>
                <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="1%">
                        <HeaderTemplate>
                        <asp:CheckBox ID="allchk"  runat="server"  />
                    </HeaderTemplate>
                    <ItemTemplate>
                        
                        <asp:CheckBox ID="chkPO" Enabled='<%# Convert.ToString(Eval("legacyOrderId")) == "" ? true : false %>' runat="server" CssClass="copy10grey" />
                        </ItemTemplate>
                    </asp:TemplateField>   
                <asp:TemplateField HeaderText="S.No." ItemStyle-CssClass="copy10grey"  ItemStyle-Width="1%">                
                <ItemTemplate>
                        <%#  Container.DataItemIndex + 1%>               
                </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="OrderId" HeaderStyle-CssClass="buttongrid"   ItemStyle-HorizontalAlign="Left" 
                    ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                    <ItemTemplate>
                        <%# Eval("orderId")%>
                        </ItemTemplate>
                </asp:TemplateField>  
                <asp:TemplateField HeaderText="Create Date" HeaderStyle-CssClass="buttongrid"   ItemStyle-HorizontalAlign="Left" 
                    ItemStyle-CssClass="copy10grey" ItemStyle-Width="8%">
                    <ItemTemplate>
                        <%# Convert.ToDateTime(Eval("creationDate")).ToString("MM/dd/yyyy")%>
                        </ItemTemplate>
                </asp:TemplateField>  
                                      
                <asp:TemplateField HeaderText="Min Estimated Delivery Date"  HeaderStyle-CssClass="buttongrid"  ItemStyle-HorizontalAlign="Left" 
                    ItemStyle-CssClass="copy10grey" ItemStyle-Width="8%">
                    <ItemTemplate>
                        <%# Convert.ToDateTime(Eval("fulfillmentStartInstructions[0].minEstimatedDeliveryDate")).ToString("MM/dd/yyyy")%>
                       <%-- <%# Eval("fulfillmentStartInstructions[0].minEstimatedDeliveryDate")%>   --%>                                 
                    </ItemTemplate>
                </asp:TemplateField> 
                <asp:TemplateField HeaderText="Carrier"  HeaderStyle-CssClass="buttongrid"  ItemStyle-HorizontalAlign="Left" 
                    ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                    <ItemTemplate>
                        <%# Eval("fulfillmentStartInstructions[0].shippingStep.shippingCarrierCode")%>                                    
                    </ItemTemplate>
                </asp:TemplateField> 
                <asp:TemplateField HeaderText="ServiceCode"  HeaderStyle-CssClass="buttongrid"  ItemStyle-HorizontalAlign="Left" 
                    ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                    <ItemTemplate>
                        <%# Eval("fulfillmentStartInstructions[0].shippingStep.shippingServiceCode")%>
                        
                        </ItemTemplate>
                </asp:TemplateField>  
                <asp:TemplateField HeaderText="Phone Number" HeaderStyle-CssClass="buttongrid"  ItemStyle-HorizontalAlign="Left" 
                    ItemStyle-CssClass="copy10grey" ItemStyle-Width="6%">
                    <ItemTemplate>
                       <%# Eval("fulfillmentStartInstructions[0].shippingStep.shipTo.primaryPhone.phoneNumber")%>                              
                    </ItemTemplate>
                </asp:TemplateField> 
                <%--<asp:TemplateField HeaderText="Email"  HeaderStyle-CssClass="buttongrid"  ItemStyle-HorizontalAlign="Left" 
                    ItemStyle-CssClass="copy10grey" ItemStyle-Width="8%">
                    <ItemTemplate>
                        <%# Eval("fulfillmentStartInstructions[0].shippingStep.shipTo.email")%>
                        
                        </ItemTemplate>
                </asp:TemplateField>  --%>
                <asp:TemplateField HeaderText="Contact Name"  HeaderStyle-CssClass="buttongrid"  ItemStyle-HorizontalAlign="Left" 
                    ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                    <ItemTemplate>
                        <%# Eval("fulfillmentStartInstructions[0].shippingStep.shipTo.fullName")%>
                        
                        </ItemTemplate>
                </asp:TemplateField>  
                
                
                <asp:TemplateField HeaderText="Address Line1"  HeaderStyle-CssClass="buttongrid"  ItemStyle-HorizontalAlign="Left" 
                    ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                    <ItemTemplate>
                        <%# Eval("fulfillmentStartInstructions[0].shippingStep.shipTo.contactAddress.addressLine1")%>
                        
                        </ItemTemplate>
                </asp:TemplateField>  
                
                <asp:TemplateField HeaderText="City"  HeaderStyle-CssClass="buttongrid"  ItemStyle-HorizontalAlign="Left" 
                    ItemStyle-CssClass="copy10grey" ItemStyle-Width="7%">
                    <ItemTemplate>
                        <%# Eval("fulfillmentStartInstructions[0].shippingStep.shipTo.contactAddress.city")%>
                        
                        </ItemTemplate>
                </asp:TemplateField>  
                
                <asp:TemplateField HeaderText="State"  HeaderStyle-CssClass="buttongrid"  ItemStyle-HorizontalAlign="Left" 
                    ItemStyle-CssClass="copy10grey" ItemStyle-Width="4%">
                    <ItemTemplate>
                        <%# Eval("fulfillmentStartInstructions[0].shippingStep.shipTo.contactAddress.stateOrProvince")%>
                        
                        </ItemTemplate>
                </asp:TemplateField>  
                
                <asp:TemplateField HeaderText="Zip"  HeaderStyle-CssClass="buttongrid"  ItemStyle-HorizontalAlign="Left" 
                    ItemStyle-CssClass="copy10grey" ItemStyle-Width="7%">
                    <ItemTemplate>
                        <%# Eval("fulfillmentStartInstructions[0].shippingStep.shipTo.contactAddress.postalCode")%>
                        
                        </ItemTemplate>
                </asp:TemplateField>  
                <asp:TemplateField HeaderText="Country Code"  HeaderStyle-CssClass="buttongrid"  ItemStyle-HorizontalAlign="Left" 
                    ItemStyle-CssClass="copy10grey" ItemStyle-Width="4%">
                    <ItemTemplate>
                        <%# Eval("fulfillmentStartInstructions[0].shippingStep.shipTo.contactAddress.countryCode")%>
                        
                        </ItemTemplate>
                </asp:TemplateField>  
                <asp:TemplateField HeaderText="Status" HeaderStyle-CssClass="buttongrid"  ItemStyle-HorizontalAlign="Left" 
                    ItemStyle-CssClass="copy10grey" ItemStyle-Width="4%">
                    <ItemTemplate>
                        <%# Eval("orderFulfillmentStatus")%>                                    
                    </ItemTemplate>
                </asp:TemplateField> 
                <asp:TemplateField HeaderText="Error" ItemStyle-Width="6%" HeaderStyle-CssClass="buttongrid"  ItemStyle-HorizontalAlign="Left" 
                    ItemStyle-CssClass="errormessage" >
                    <ItemTemplate>
                        <%# Eval("legacyOrderId") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Status" HeaderStyle-CssClass="buttongrid"  ItemStyle-HorizontalAlign="Left" 
                    ItemStyle-CssClass="copy10grey" ItemStyle-Width="4%"> 
                    <ItemTemplate>
                        <asp:ImageButton ID="imgView"  ToolTip="Line items" OnCommand="imgView_Command"
                                     CausesValidation="false" CommandArgument='<%# Eval("orderid") %>' ImageUrl="~/Images/view.png"  runat="server" />
                                                                  
                    </ItemTemplate>
                </asp:TemplateField> 

                     
                                    
            </Columns>
        </asp:GridView>               
        </td>
    </tr> 
        <tr>
                <td  align="center"  colspan="2">
                    <br />
            
         &nbsp;
           <asp:Button ID="btnCancel2" Visible="false" runat="server" Text="Cancel" CssClass="button" OnClick="btnCancel_Click" CausesValidation="false"/>
                   
        </td>
        </tr>
</table>
    </ContentTemplate>
         </asp:UpdatePanel>
                
    <asp:UpdatePanel ID="upnlJsRunner" UpdateMode="Always" runat="server">
			<ContentTemplate>
				<asp:PlaceHolder ID="phrJsRunner" runat="server">
                
                </asp:PlaceHolder>
			</ContentTemplate>
		</asp:UpdatePanel>
         </td>
         </tr>
         </table>
    <br />
        <table cellspacing="0" cellpadding="0" border="0" align="center" width="100%">
        <tr>
            <td >
                <foot:MenuFooter ID="footer2" runat="server"></foot:MenuFooter>
            </td>
         </tr>
         </table>

    </form>
</body>
</html>
