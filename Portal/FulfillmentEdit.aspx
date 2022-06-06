<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FulfillmentEdit.aspx.cs" Inherits="avii.FulfillmentEdit" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>
<%@ Register TagPrefix="menu" TagName="Menu" Src="~/Controls/Header.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Fulfillment Edit</title>
     <link rel="stylesheet"  type="text/css" href="../fullfillment/calendar/dhtmlgoodies_calendar.css" media="screen" />
	<script type="text/javascript" src="../fullfillment/calendar/dhtmlgoodies_calendar.js"></script>  
	
    <div id="Div1" runat="server"> 
	
    <script>
        function isNumberHiphen(evt) {

            var charCodes = evt.keyCode ? evt.keyCode : evt.which ? evt.which : evt.charCode;
            if (((((charCodes > 31 && (charCodes < 48 || charCodes > 57) && charCodes != 45) && charCodes != 46) && charCodes != 95) && !(charCodes > 96 && charCodes < 123)) && !(charCodes > 64 && charCodes < 91)) {
                //alert(charCodes);

                charCodes = 0;
                return false;
            }

            return true;
        }
        function set_focus1() {
            var img = document.getElementById("img1");
            var st = document.getElementById("txtContactName");
           // st.focus();
            img.click();
        }

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
        function onBlurTextCounter(textBox, maxLength) {
            if (textBox.value.length > maxLength)
                textBox.value = textBox.value.substr(0, maxLength);
        }
        function ReadOnlyReqShipDate(evt) {
            var imgCall = document.getElementById("img1");
            imgCall.click();
            evt.keyCode = 0;
            return false;

        }
        function ReadOnly(evt) {
            var imgCall = document.getElementById("img1");
            imgCall.click();
            evt.keyCode = 0;
            return false;

        }
        function ValidateEditPo() {

            var defaultDateRange = '1095';
            <%--var uploadDate = document.getElementById("<%=txtPODate.ClientID %>").value;
            //alert(uploadDate);
            if (uploadDate != null && uploadDate != '') {

                var today = new Date();
                var dd = today.getDate();
                var mm = today.getMonth() + 1; //January is 0!
                var yyyy = today.getFullYear();

                if (dd < 10) {
                    dd = '0' + dd
                }

                if (mm < 10) {
                    mm = '0' + mm
                }

                today = mm + '/' + dd + '/' + yyyy;
                //alert(daydiff(parseDate(uploadDate), parseDate(today)));
                var daterange = daydiff(parseDate(uploadDate), parseDate(today));

                if (daterange > defaultDateRange) {
                    alert('UploadDate can not be more than  1095 days before');
                    return false;
                }
                if (daterange < 0) {
                    alert('UploadDate can not be more than today date');
                    return false;
                }

            }--%>

            var ShippingDate = document.getElementById("<%=txtReqShipDate.ClientID %>").value;
          //  alert(ShippingDate);
            if (ShippingDate != null && ShippingDate != '') {

                var today = new Date();
                var dd = today.getDate();
                var mm = today.getMonth() + 1; //January is 0!
                var yyyy = today.getFullYear();

                if (dd < 10) {
                    dd = '0' + dd
                }

                if (mm < 10) {
                    mm = '0' + mm
                }

                today = mm + '/' + dd + '/' + yyyy;
               //    alert(today);
                //alert(ShippingDate);
                //alert(daydiff(parseDate(uploadDate), parseDate(today)));
                var daterange = daydiff(parseDate(today), parseDate(ShippingDate));
                //alert(daterange)
                if (daterange < 0) {
                    alert('Requested ship date can not be less than today date');
                    return false;
                }
                if (daterange > 10) {
                    alert('Requested ship date can not be more than 10 days from today date');
                    return false;
                    //can not be less than 7 days from Today's date
                }

            }
            else {
                alert('Requested ship date can not be empty');
            }
            var msg = '';
            //	        var ShipBy = document.getElementById("").value;
            //	        if (ShipBy == '')
            //	            msg = 'Shipvia cannot be empty!';

            var contactName = document.getElementById("<%= txtContactName.ClientID %>").value;
            if (contactName == '' && msg == '')
                msg = 'Contact Name cannot be empty!';
            //	        else
            //	            if (contactName == '' && msg != '')
            //	                msg = 'Shipvia and Contact Name cannot be empty!';


            var address = document.getElementById("<%= txtStreetAdd.ClientID %>").value;
            if (address == '' && msg == '')
                msg = 'Address cannot be empty!';
            else
                if (address == '' && msg != '')
                    msg = 'Address, ' + msg;

            var City = document.getElementById("<%= txtCity.ClientID %>").value;
            if (City == '' && msg == '')
                msg = 'City cannot be empty!';
            else
                if (City == '' && msg != '')
                    msg = 'City, ' + msg;

            var state = document.getElementById("<%= dpState.ClientID %>");
            if (state.selectedIndex == 0 && msg == '')
                msg = 'State cannot be empty!';
            else
                if (state.selectedIndex == 0 && msg != '')
                    msg = 'State, ' + msg;


            var zip = document.getElementById("<%= txtZip.ClientID %>").value;
            if (zip == '' && msg == '')
                msg = 'Zip cannot be empty!';
            else
                if (zip == '' && msg != '')
                    msg = 'Zip, ' + msg;



            if (msg == '') {
                return true;
            }
            else {
                alert(msg);
                return false;
            }



        }
        function parseDate(str) {
            var mdy = str.split('/')
            return new Date(mdy[2], mdy[0] - 1, mdy[1]);
        }

        function daydiff(first, second) {
            return (second - first) / (1000 * 60 * 60 * 24);
        }
        function isNumberKey(evt) {

            var charCodes = evt.keyCode ? evt.keyCode : evt.which ? evt.which : evt.charCode;
            if (charCodes > 31 && (charCodes < 48 || charCodes > 57)) {
                charCodes = 0;
                return false;
            }
            return true;
        }

        function mask(e, f) {
            var len = f.value.length;
            var key = whichKey(e);
            if (key > 47 && key < 58) {
                if (len == 3) f.value = f.value + '-'
                else if (len == 7) f.value = f.value + '-'
                else f.value = f.value;
            }
            else {
                f.value = f.value.replace(/[^0-9-]/, '')
                f.value = f.value.replace('--', '-')
            }
        }
        function alphaNumericCheck(evt) {
            var charCodes = evt.keyCode ? evt.keyCode : evt.which ? evt.which : evt.charCode ? evt.charCode : evt.type;
            //alert(charCodes);
            if (charCodes == 8 || charCodes == 9 || charCodes == 46 || (105 >= charCodes && charCodes >= 96)
                || (90 >= charCodes && charCodes >= 65)
                || (57 >= charCodes && charCodes >= 48)
                || (122 >= charCodes && charCodes >= 97)) {
                return true;
            }
            else {
                return false;
            }
        }

    </script>
        </div>
</head>
<body  bgcolor="#ffffff" leftmargin="0" rightmargin="0" topmargin="0">
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
        <table cellspacing="0" cellpadding="0" border="0" align="center" width="95%">        
        <tr valign="top">           
            <td>
                <table align="center" style="text-align:left" width="100%">
                <tr class="buttonlabel" align="left">
                <td>&nbsp;Fulfillment Edit</td></tr>
             </table>
    
				<asp:UpdatePanel ID="upnlEditPO" runat="server">
					<ContentTemplate>
                    <table  align="center" style="text-align:left" width="100%" cellSpacing="0" cellPadding="0">
                     <tr>
                        <td align="left">
                           <asp:Label ID="lblMsg" runat="server" CssClass="errormessage"></asp:Label>
            
                        </td>
                     </tr>
                     </table>
    
                            <%--<asp:Label ID="lblEditPO" runat="server" CssClass="errormessage"></asp:Label>--%>
                             <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="99%" align="center">
                            <tr bordercolor="#839abf">
                            <td>
                            <table width="100%" align="center" cellpadding="3" cellspacing="3" border="0">
                            <tr>
                                <td class="copy10grey" align="right" width="17%">
                                    <b>Fulfillment#:</b>    
                                </td>

                                <td width="32%">
                                    &nbsp; <asp:Label ID="lblPONo" runat="server" CssClass="copy10grey"></asp:Label>   
                                </td>
                                <td width="2%">
                                &nbsp;
                                </td>
                                <td class="copy10grey" align="right" width="17%">
                                    <b> Customer Order#:</b>
                                    
                                </td>
                                <td width="32%">

                                   &nbsp;  
                                    <asp:TextBox ID="txtCustomerOrderNumber" runat="server" MaxLength="20" Width="200" CssClass="copy10grey"></asp:TextBox>   
                                    <%--<asp:Label ID="lblCustomerOrderNumber" runat="server" CssClass="copy10grey"></asp:Label>   
                                    --%>
                                </td>

                            </tr>
                            <tr>
                                <td class="copy10grey" align="right" width="17%">
                                    <b>Fulfillment Date:</b>    
                                </td>
                                <td width="32%">

                                &nbsp;<asp:TextBox ID="txtPODate" ContentEditable="false" onkeydown="return ReadOnly(event);" runat="server" CssClass="copy10grey"></asp:TextBox>&nbsp;
                                    <%--<img id="imgCal" alt=""  src="/fullfillment/calendar/sscalendar.jpg" runat="server"/>
                                    <ajaxToolkit:CalendarExtender ID="CalExtDate" runat="server" PopupButtonID="imgCal" 
                                    PopupPosition="BottomLeft" Format="MM/dd/yyyy"   TargetControlID="txtPODate">
                                    </ajaxToolkit:CalendarExtender>
                        --%>
                                   &nbsp; <%--<asp:Label ID="lblPoDate" runat="server" CssClass="copy10grey"></asp:Label>   --%>
                                </td>
                                <td width="2%">
                                &nbsp;
                                </td>
                    
                                <td align="right" width="17%" class="copy10grey">
                                    <b>Fulfillment Type:</b>
                                    
                                   
                                </td>    
                                <td width="32%">
                                 &nbsp;<asp:Label ID="lblPoTye" runat="server" CssClass="copy10grey"></asp:Label>
                                    
                                    
                                </td>
                            </tr>
                            <tr>
                                <td class="copy10grey" align="right" width="17%">
                                <b>Default Shipvia:</b>
                            
                                </td>

                                <td width="32%">
                                    &nbsp;<asp:DropDownList ID="dpShipVia" runat="server" Width="91%" CssClass="copy10grey"></asp:DropDownList>
                                    <asp:Label ID="lblDShipvia" runat="server" CssClass="copy10grey" ></asp:Label>   
                                </td>
                                <td width="2%">
                                &nbsp;
                                </td>
                                <td class="copy10grey" align="right" width="17%">
                                    <b><asp:Label ID="lblPOStatus" CssClass="copy10grey" runat="server" Text="Status:"></asp:Label></b>
                                       
                                </td>
                                <td width="32%">

                                   &nbsp;<asp:DropDownList ID="ddlStatus" runat="server" class="copy10grey">
                                            <asp:ListItem Text="" Value=""></asp:ListItem>
                                            <asp:ListItem Text="Pending" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="In Process" Value="8"></asp:ListItem>
                                            <asp:ListItem Text="Partial Processed" Value="10"></asp:ListItem>
                                            <asp:ListItem Text="Partial Shipped" Value="11"></asp:ListItem>
                                            <asp:ListItem Text="Processed" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="Shipped" Value="3"></asp:ListItem>
                                            <asp:ListItem Text="Closed" Value="4"></asp:ListItem>
                                            <asp:ListItem Text="Return" Value="5"></asp:ListItem>

                                            <asp:ListItem Text="Cancel" Value="9"></asp:ListItem>
                                        <asp:ListItem Text="On Hold" Value="6"></asp:ListItem>
                                            <asp:ListItem Text="Out of Stock" Value="7"></asp:ListItem>
                                        </asp:DropDownList>

                                        &nbsp;  <asp:Label ID="lblStatus" runat="server" CssClass="copy10grey"></asp:Label>   
                                    
                                </td>

                        
                            </tr>
                            <tr>
                                <td class="copy10grey" align="right" width="17%">
                                    <b>Requested Ship Date: </b>
                                </td>
                                <td  width="32%">
                                    &nbsp;
                                      <asp:TextBox ID="txtReqShipDate" runat="server" onkeydown="return ReadOnly(event);" onfocus="set_focus1();"  CssClass="copy10grey" MaxLength="12"  Width="80%"></asp:TextBox>
                                            <img id="img1" alt="" onclick="document.getElementById('<%=txtReqShipDate.ClientID%>').value = ''; displayCalendar(document.getElementById('<%=txtReqShipDate.ClientID%>'),'mm/dd/yyyy',this,true);" src="../fullfillment/calendar/sscalendar.jpg" />
                                   
<%--                                    <asp:TextBox ID="txtReqShipDate" ContentEditable="false" onkeydown="return ReadOnlyReqShipDate(event);" runat="server" CssClass="copy10grey"></asp:TextBox>&nbsp;
                                    <img id="imgReq" alt=""  src="/fullfillment/calendar/sscalendar.jpg" runat="server"/>
                                    <ajaxToolkit:CalendarExtender ID="cextId" runat="server" PopupButtonID="imgReq" 
                                    PopupPosition="BottomLeft" Format="MM/dd/yyyy"   TargetControlID="txtReqShipDate">
                                    </ajaxToolkit:CalendarExtender>
                    --%>
                                </td>
                                <td width="2%">
                                &nbsp;
                                </td>
                                <td class="copy10grey" align="right" width="17%">
                                   Shipment required:
                                </td>
                                <td width="32%">
                                    <asp:CheckBox ID="chkShipRequired" runat="server" />   
                                    <asp:TextBox ID="txtFactOrderNumber" Visible="false" runat="server" MaxLength="13" CssClass="copy10grey"  onkeypress="return isNumberHiphen(event);"></asp:TextBox>
                                    
                                </td>

                            </tr>
                            </table>
                            </td>
                            </tr>
                            </table>
                
                <br />

                <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="99%" align="center">
                <tr bordercolor="#839abf">
                <td>
                <table width="100%" align="center" cellpadding="3" cellspacing="3" border="0" >
                <tr>
                    <td class="copy10grey" align="right" width="17%">
                        Customer:    
                    </td>
                    <td width="32%">

                       &nbsp; <asp:Label ID="lblCustomer" runat="server" CssClass="copy10grey"></asp:Label>   
                    </td>
                    <td width="2%">
                    &nbsp;
                    </td>
                    <td class="copy10grey" align="right" width="17%">
                       Destination StoreID:    
                    </td>
                    <td width="32%">

                       &nbsp;  <asp:Label ID="lblStoreID" runat="server" CssClass="copy10grey"></asp:Label>   
                    </td>

                </tr>
                </table>
                </td>
                </tr>
                </table>
                
                
                <br />
                <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="99%" align="center">
                <tr bordercolor="#839abf">
                <td>
                <table width="100%" align="center" cellpadding="3" cellspacing="3" border="0">
                <tr>
                    <td class="copy10grey" align="right" width="17%">
                        <b>Contact Name:</b>    
                    </td>
                    <td width="32%">

                       &nbsp; <asp:TextBox ID="txtContactName" Width="200" MaxLength="50" CssClass="copy10grey" runat="server"></asp:TextBox>
                
                    </td>
                    <td width="2%">
                    &nbsp;
                    </td>
                    <td class="copy10grey" align="right" width="17%">
                        <b>Contact Phone:</b>    
                    </td>
                    <td width="32%">

                       &nbsp; <asp:TextBox ID="txtContactPhone"  onkeydown="mask(event,this)" onkeyup="mask(event,this)"  MaxLength="12" Width="200" CssClass="copy10grey" runat="server"></asp:TextBox>
                
                    </td>


                </tr>
                <tr>
                    <td class="copy10grey" align="right" width="17%">
                        <b>Street Address:  </b>  
                    </td>
                    <td width="32%">

                      &nbsp;   
                      <asp:TextBox ID="txtStreetAdd" Width="200" MaxLength="100" CssClass="copy10grey" runat="server"></asp:TextBox>
                      <%--<asp:Label ID="lblStreetAdd" runat="server" CssClass="copy10grey"></asp:Label>   --%>
                    </td>
                    <td width="2%">
                    &nbsp;
                    </td>
                    <td class="copy10grey" align="right" width="17%">
                        Address2:    
                    </td>
                    <td width="32%">

                      &nbsp;   
                      <asp:TextBox ID="txtAddress2" Width="200" MaxLength="100" CssClass="copy10grey" runat="server"></asp:TextBox>
                      <%--<asp:Label ID="lblStreetAdd" runat="server" CssClass="copy10grey"></asp:Label>   --%>
                    </td>
                </tr>
                <tr>
                    
                </tr>
                <tr>
                    <td class="copy10grey" align="right" width="17%">
                        <b>City: </b>   
                    </td>
                    <td width="32%">

                       &nbsp;  
                       <asp:TextBox ID="txtCity" Width="200" MaxLength="50" onkeypress="return alphaNumericCheck(event);" CssClass="copy10grey" runat="server"></asp:TextBox>
                       
                    </td>
                    <td width="2%">
                    &nbsp;
                    </td>
                    <td class="copy10grey" align="right" width="17%">
                        <b>State:</b>    
                    </td>
                    <td width="32%" class="copy10grey" >

                       &nbsp;  
                       <asp:dropdownlist id="dpState" tabIndex="6" runat="server" cssclass="copy10grey">
                       </asp:dropdownlist>

                       <%--<asp:TextBox ID="txtState" Width="30" MaxLength="2" CssClass="copy10grey" runat="server"></asp:TextBox>--%>

                       &nbsp;  &nbsp;  &nbsp; &nbsp;<b>Zip:</b>&nbsp;&nbsp;&nbsp;&nbsp;  
                       <asp:TextBox ID="txtZip" Width="90" onkeypress="return alphaNumericCheck(event);"  MaxLength="6" CssClass="copy10grey" runat="server"></asp:TextBox>
                       
                       <%--<asp:Label ID="lblState" runat="server" CssClass="copy10grey"></asp:Label>   --%>
                    </td>
                </tr>
                <tr>
                    <td>

                    </td>
                </tr>
                <%--<tr>
                    <td class="copy10grey" align="right" width="40%">
                        Sent ESN:    
                    </td>
                    <td>

                       &nbsp;  <asp:Label ID="lblSentESN" runat="server" CssClass="copy10grey"></asp:Label>   
                    </td>
                </tr>
                <tr>
                    <td class="copy10grey" align="right" width="40%">
                        Sent ASN:    
                    </td>
                    <td>

                       &nbsp;  <asp:Label ID="lblSentASN" runat="server" CssClass="copy10grey"></asp:Label>   
                    </td>
                </tr>--%>
                </table>
                </td>
                </tr>
                </table>
                 <br />   
                   <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="99%"  align="center">
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
                                    &nbsp;<asp:TextBox ID="txtCommments" CssClass="copy10grey" Width="95%" TextMode="MultiLine" Height="40"  runat="server"
                                    onkeydown="checkTextAreaMaxLength(this,event,'5000');" onkeyup="checkTextAreaMaxLength(this,event,'5000');" onblur="onBlurTextCounter(this,'5000');"
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
                   <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="100%"  align="center">
                   <tr>
                    <td align="left" class="buttonlabel"  >
                        Line items
                    </td>
    
                    </tr>
                    <tr>
                        <td colspan="1">

                        <asp:GridView ID="gvPODetail"  BackColor="White" Width="100%" Visible="true"  
                            AutoGenerateColumns="false" Font-Names="Verdana" runat="server" DataKeyNames="PodID"
                            GridLines="Both" 
                            BorderStyle="Double" BorderColor="#0083C1" >
                            <RowStyle BackColor="Gainsboro" />
                            <AlternatingRowStyle BackColor="white" />
                            <HeaderStyle  CssClass="buttongrid" ForeColor="white"/>
                            <Columns>
         	                    <asp:TemplateField HeaderText="Line#"  ItemStyle-CssClass="copy10grey"  HeaderStyle-Width="3%" ItemStyle-Wrap="false"  ItemStyle-width="3%">
                                    <ItemTemplate>
                                       <%--<%# Container.DataItemIndex +  1 %> , --%>
                                       <%#Eval("LineNo")%>
                                        <asp:HiddenField ID="hdpodid" runat="server" Value='<%#Eval("PodID")%>' />
                                    </ItemTemplate> 
                
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="SKU#"  ItemStyle-CssClass="copy10grey"  HeaderStyle-Width="7%" ItemStyle-Wrap="false"  ItemStyle-width="25%">
                                    <ItemTemplate>
                                        <%# Convert.ToString(Eval("ItemCode")).ToUpper()%>
                                    </ItemTemplate> 
                
                                </asp:TemplateField>
                                                                                                                                  
                                            
                                <asp:TemplateField HeaderText="Qty"  ItemStyle-CssClass="copy10grey" ItemStyle-Width="15%">
                                    <ItemTemplate><%#Eval("Quantity")%></ItemTemplate>
                                     <EditItemTemplate>
                                        <asp:TextBox ID="txtQty" CssClass="copy10grey" MaxLength="5" onkeypress="return isNumberKey(event);" onchange="return isQuantity(this);" 
                                        Enabled='<%# Convert.ToInt32(Eval("StatusID")) == 1?true:false %>' Width="99%" Text='<%# Eval("Quantity") %>' runat="server"></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                 
                                                                                                                                        
           
                                <asp:TemplateField HeaderText="Status"   ItemStyle-CssClass="copy10grey" ItemStyle-Width="15%">
                                    <ItemTemplate>
                                    <%# Convert.ToInt32(Eval("StatusID")) == 1 ? "Pending" : Convert.ToInt32(Eval("StatusID")) == 2 ? "Processed" : Convert.ToInt32(Eval("StatusID")) == 3 ? "Shipped" : Convert.ToInt32(Eval("StatusID")) == 4 ? "Closed" : Convert.ToInt32(Eval("StatusID")) == 5 ? "Return" : Convert.ToInt32(Eval("StatusID")) == 9 ? "Cancel" : Convert.ToInt32(Eval("StatusID")) == 6 ? "On Hold" : Convert.ToInt32(Eval("StatusID")) == 7 ? "Out of Stock" : Convert.ToInt32(Eval("StatusID")) == 8 ? "In Process" : Convert.ToInt32(Eval("StatusID")) == 10 ? "Partial Processed" : Convert.ToInt32(Eval("StatusID")) == 11 ? "Partial Shipped" : "Pending"%>
                                    <%-- <br /> <%#Eval("PODStatus")%>--%>
                                        <asp:HiddenField ID="hdnStatus" Value='<%# Eval("StatusID") %>' runat="server" />
                   
                                                
                                    </ItemTemplate>
                                </asp:TemplateField> 
                                <asp:TemplateField HeaderText="Items/Container"  ItemStyle-CssClass="copy10grey" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtItemsPerContainer" Text='<%#Eval("ItemsPerContainer")%>'  onkeypress="return isNumberKey(event);" Width="100" MaxLength="5" CssClass="copy10grey" runat="server"></asp:TextBox>
                
                                    </ItemTemplate>
                                </asp:TemplateField>                                                   
			
                                <asp:TemplateField HeaderText="Containers/Pallet"  ItemStyle-CssClass="copy10grey" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                    
                                    <asp:TextBox ID="txtContainersPerPallet" Text='<%#Eval("ContainersPerPallet")%>' onkeypress="return isNumberKey(event);"  MaxLength="5" Width="100" CssClass="copy10grey" runat="server"></asp:TextBox>
                
                                    </ItemTemplate>
                                </asp:TemplateField>             
                            </Columns>
                        </asp:GridView>
                        </td>
                    </tr>
                    </table>

                <br />
                <table width="100%" align="center">
                <tr>
                    <td align="center"> 
                      
                        <asp:Button ID="btnSubmit" Visible="false" runat="server"  OnClick="btnEditPO_Click" Text="Submit" OnClientClick="return ValidateEditPo();"  CssClass="button" />&nbsp;
                         <asp:Button ID="btnCancel2" runat="server" Text="Cancel" CssClass="button" OnClientClick="closeEditDialog()"  />
                    </td>
                </tr>
                </table>
               
						
						
					</ContentTemplate>
							
				</asp:UpdatePanel>

                
          <asp:UpdateProgress ID="UpdateProgress091" runat="server" >
            <ProgressTemplate>
                <div id="overlay">
                    <div id="modelprogress">
                        <div id="theprogress">
                            <img src="/Images/ajax-loaders.gif" /> 
                        </div>
                    </div>
                </div>
                
            </ProgressTemplate>
        </asp:UpdateProgress>
   
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

    </form>
</body>
</html>
