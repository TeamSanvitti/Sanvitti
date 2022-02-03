<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FulfillmentDetail.aspx.cs" Inherits="avii.FulfillmentDetail" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>
<%@ Register TagPrefix="menu" TagName="Menu" Src="~/Controls/Header.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Fulfillment</title>

    <!-- fix for 1.1em default font size in Jquery UI -->
	<style type="text/css">
		.ui-widget{font-size:11px !important;}
		.ui-state-error-text{margin-left: 10px}
	</style>
    <script type="text/javascript">

        function Refresh() {
            //alert('refreshing..');
            var btnhdPrintlabel = document.getElementById("<%= btnhdPrintlabel.ClientID %>");
            btnhdPrintlabel.click();
        }
        function closeWindow() {

            alert('No valid data!')
            window.close();

        }
        function close_window() {
            if (confirm("Close Window?")) {
                window.close();
                return true;
            }
            else
                return false
        }

  
        function isQuantity(obj) {

            if (obj.value == '0') {
                alert('Quantity can not be zero');
                obj.value = '1';
                return false;
            }
            if (obj.value == '') {
                alert('Quantity can not be empty');
                obj.value = '1';
                return false;
            }
        }
        function isNumberKey(evt) {

            var charCodes = evt.keyCode ? evt.keyCode : evt.which ? evt.which : evt.charCode;
            if (charCodes > 31 && (charCodes < 48 || charCodes > 57)) {
                charCodes = 0;
                return false;
            }
            return true;
        }
        function ShowTracking(obj) {
            //setAttribute("disabled", false);
            var objTrackingNumber = document.getElementById("<%= txtTrackingNumber.ClientID%>");
            var objPrice = document.getElementById("<%= txtPrice.ClientID%>");

            if (obj.checked) {
                objTrackingNumber.disabled = false;
                objPrice.disabled = false;
                
            }
            else {
                objTrackingNumber.disabled = true;
                objPrice.disabled = true;

            }
        }
        function ShowManualTracking(obj) {
            //setAttribute("disabled", false);
            //alert(obj.value);

            var objchkTracking = document.getElementById("<%= chkTracking.ClientID%>");
            var objTrackingNumber = document.getElementById("<%= txtTrackingNumber.ClientID%>");
            var objPrice = document.getElementById("<%= txtPrice.ClientID%>");


            if (obj.value == 'TruckLoad') {
                objchkTracking.checked = true;
                objTrackingNumber.disabled = false;
                objPrice.disabled = false;

            }
            else {
                objTrackingNumber.disabled = true;
                objPrice.disabled = true;
                objchkTracking.checked = false;

            }
        }
        function ValidateWeight(evt, obj) {
            var charCodes = evt.keyCode ? evt.keyCode : evt.which ? evt.which : evt.charCode;
            //alert(charCodes);
            var priceValue = obj.value;
            //alert(priceValue);
            //alert(priceValue.indexOf('.'))
            //if ((charCodes == 46 && priceValue.indexOf('.') > -1) || (charCodes == 190 && priceValue.indexOf('.') > -1) || (charCodes < 48 && charCodes == 46 && priceValue.indexOf('.') > -1) || (charCodes > 57 && charCodes != 190)) {
            if ((charCodes == 46 && priceValue.indexOf('.') > -1) || (charCodes < 48 && charCodes != 46) || charCodes > 57) {
                //charCodes = 0;
                ///priceValue = priceValue.replace('..', '.');
                //obj.value = priceValue;

                evt.preventDefault();
                //alert('in');
                return false;
            }
            //else

            return true;


        }
        function OnKeyUp(obj) {
            qty = document.getElementById(obj.id.replace('txtQty', 'hdnQty'));
            if (obj.value > qty.value) {
                alert('Quantity can not be greater than assigned quantity!');
                obj.value = '';
                obj.value = qty.value;
            }
        }
        
        $(function () {
            $('#txtPONum').keyup(function () {
                if (this.value.match(/[^a-zA-Z0-9-,_ ]/g)) {
                    this.value = this.value.replace(/[^a-zA-Z0-9-,_ ]/g, '');
                }
            });
        });
        //	    $(function () {
        //	        $('#txtPONum').keypress(function (e) {
        //	            var code = e.keyCode ? e.keyCode : e.which; // Get the key code.
        //	            var pressedKey = String.fromCharCode(code); // Find the key pressed.
        //	            if (!pressedKey.match(/[a-zA-Z0-9]/g)) // Check if it's a alpanumeric char or not.
        //	            {
        //	                e.preventDefault(); // If it is not then prevent the event from happening.  
        //	            }

        //	        });
        //	    });
        $(function () {
            $('#txtCustName').keyup(function () {
                if (this.value.match(/[^a-zA-Z0-9-,_ ]/g)) {
                    this.value = this.value.replace(/[^a-zA-Z0-9-,_ ]/g, '');
                }
            });
        });
        //	    $(function () {
        //	        $('#txtCustName').keypress(function (e) {
        //	            var code = e.keyCode ? e.keyCode : e.which; // Get the key code.
        //	            var pressedKey = String.fromCharCode(code); // Find the key pressed.
        //	            if (!pressedKey.match(/[a-zA-Z0-9]/g)) // Check if it's a alpanumeric char or not.
        //	            {
        //	                e.preventDefault(); // If it is not then prevent the event from happening.  
        //	            }

        //	        });
        //	    });
        $(function () {
            $('#txtFromDate').keyup(function () {
                if (this.value.match(/[^a-zA-Z0-9 ]/g)) {
                    this.value = this.value.replace(/[^a-zA-Z0-9 ]/g, '');
                }
            });
        });
        //	    $(function () {
        //	        $('#txtFromDate').keypress(function (e) {
        //	            var code = e.keyCode ? e.keyCode : e.which; // Get the key code.
        //	            var pressedKey = String.fromCharCode(code); // Find the key pressed.
        //	            if (!pressedKey.match(/[a-zA-Z0-9]/g)) // Check if it's a alpanumeric char or not.
        //	            {
        //	                e.preventDefault(); // If it is not then prevent the event from happening.  
        //	            }

        //	        });
        //	    });
        $(function () {
            $('#txtToDate').keyup(function () {
                if (this.value.match(/[^a-zA-Z0-9 ]/g)) {
                    this.value = this.value.replace(/[^a-zA-Z0-9 ]/g, '');
                }
            });
        });
        //	    $(function () {
        //	        $('#txtToDate').keypress(function (e) {
        //	            var code = e.keyCode ? e.keyCode : e.which; // Get the key code.
        //	            var pressedKey = String.fromCharCode(code); // Find the key pressed.
        //	            if (!pressedKey.match(/[a-zA-Z0-9]/g)) // Check if it's a alpanumeric char or not.
        //	            {
        //	                e.preventDefault(); // If it is not then prevent the event from happening.  
        //	            }

        //	        });
        //	    });
        $(function () {
            $('#txtShipTo').keyup(function () {
                if (this.value.match(/[^a-zA-Z0-9 ]/g)) {
                    this.value = this.value.replace(/[^a-zA-Z0-9 ]/g, '');
                }
            });
        });
        //	    $(function () {
        //	        $('#txtShipTo').keypress(function (e) {
        //	            var code = e.keyCode ? e.keyCode : e.which; // Get the key code.
        //	            var pressedKey = String.fromCharCode(code); // Find the key pressed.
        //	            if (!pressedKey.match(/[a-zA-Z0-9]/g)) // Check if it's a alpanumeric char or not.
        //	            {
        //	                e.preventDefault(); // If it is not then prevent the event from happening.  
        //	            }

        //	        });
        //	    });
        $(function () {
            $('#txtShipFrom').keyup(function () {
                if (this.value.match(/[^a-zA-Z0-9 ]/g)) {
                    this.value = this.value.replace(/[^a-zA-Z0-9 ]/g, '');
                }
            });
        });
        //	    $(function () {
        //	        $('#txtShipFrom').keypress(function (e) {
        //	            var code = e.keyCode ? e.keyCode : e.which; // Get the key code.
        //	            var pressedKey = String.fromCharCode(code); // Find the key pressed.
        //	            if (!pressedKey.match(/[a-zA-Z0-9]/g)) // Check if it's a alpanumeric char or not.
        //	            {
        //	                e.preventDefault(); // If it is not then prevent the event from happening.  
        //	            }

        //	        });
        //	    });
        //	    	
        $(function () {
            $('#txtEsn').keyup(function () {
                if (this.value.match(/[^a-zA-Z0-9-,_ ]/g)) {
                    this.value = this.value.replace(/[^a-zA-Z0-9-,_ ]/g, '');
                }
            });
        });
        //	    $(function () {
        //	        $('#txtEsn').keypress(function (e) {
        //	            var code = e.keyCode ? e.keyCode : e.which; // Get the key code.
        //	            var pressedKey = String.fromCharCode(code); // Find the key pressed.
        //	            if (!pressedKey.match(/[a-zA-Z0-9]/g)) // Check if it's a alpanumeric char or not.
        //	            {
        //	                e.preventDefault(); // If it is not then prevent the event from happening.  
        //	            }

        //	        });
        //	    });
        $(function () {
            $('#txtAvNumber').keyup(function () {
                if (this.value.match(/[^a-zA-Z0-9-,_ ]/g)) {
                    this.value = this.value.replace(/[^a-zA-Z0-9-,_ ]/g, '');
                }
            });
        });
        //	    $(function () {
        //	        $('#txtAvNumber').keypress(function (e) {
        //	            var code = e.keyCode ? e.keyCode : e.which; // Get the key code.
        //	            var pressedKey = String.fromCharCode(code); // Find the key pressed.
        //	            if (!pressedKey.match(/[a-zA-Z0-9]/g)) // Check if it's a alpanumeric char or not.
        //	            {
        //	                e.preventDefault(); // If it is not then prevent the event from happening.  
        //	            }

        //	        });
        //	    });
        $(function () {
            $('#txtMslNumber').keyup(function () {
                if (this.value.match(/[^a-zA-Z0-9-,_ ]/g)) {
                    this.value = this.value.replace(/[^a-zA-Z0-9-,_ ]/g, '');
                }
            });
        });
        //	    $(function () {
        //	        $('#txtMslNumber').keypress(function (e) {
        //	            var code = e.keyCode ? e.keyCode : e.which; // Get the key code.
        //	            var pressedKey = String.fromCharCode(code); // Find the key pressed.
        //	            if (!pressedKey.match(/[a-zA-Z0-9]/g)) // Check if it's a alpanumeric char or not.
        //	            {
        //	                e.preventDefault(); // If it is not then prevent the event from happening.  
        //	            }

        //	        });
        //	    });
        $(function () {
            $('#txtStoreID').keyup(function () {
                if (this.value.match(/[^a-zA-Z0-9-,_ ]/g)) {
                    this.value = this.value.replace(/[^a-zA-Z0-9-,_ ]/g, '');
                }
            });
        });
        //	    $(function () {
        //	        $('#txtStoreID').keypress(function (e) {
        //	            var code = e.keyCode ? e.keyCode : e.which; // Get the key code.
        //	            var pressedKey = String.fromCharCode(code); // Find the key pressed.
        //	            if (!pressedKey.match(/[a-zA-Z0-9]/g)) // Check if it's a alpanumeric char or not.
        //	            {
        //	                e.preventDefault(); // If it is not then prevent the event from happening.  
        //	            }

        //	        });
        //	    });
        $(function () {
            $('#txtItemCode').keyup(function () {
                if (this.value.match(/[^a-zA-Z0-9-,_ ]/g)) {
                    this.value = this.value.replace(/[^a-zA-Z0-9-,_ ]/g, '');
                }
            });
        });
        //	    $(function () {
        //	        $('#txtItemCode').keypress(function (e) {
        //	            var code = e.keyCode ? e.keyCode : e.which; // Get the key code.
        //	            var pressedKey = String.fromCharCode(code); // Find the key pressed.
        //	            if (!pressedKey.match(/[a-zA-Z0-9]/g)) // Check if it's a alpanumeric char or not.
        //	            {
        //	                e.preventDefault(); // If it is not then prevent the event from happening.  
        //	            }

        //	        });
        //	    });
        $(function () {
            $('#txtFmUpc').keyup(function () {
                if (this.value.match(/[^a-zA-Z0-9-,_ ]/g)) {
                    this.value = this.value.replace(/[^a-zA-Z0-9-,_ ]/g, '');
                }
            });
        });
        //	    $(function () {
        //	        $('#txtFmUpc').keypress(function (e) {
        //	            var code = e.keyCode ? e.keyCode : e.which; // Get the key code.
        //	            var pressedKey = String.fromCharCode(code); // Find the key pressed.
        //	            if (!pressedKey.match(/[a-zA-Z0-9]/g)) // Check if it's a alpanumeric char or not.
        //	            {
        //	                e.preventDefault(); // If it is not then prevent the event from happening.  
        //	            }

        //	        });
        //	    });

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

        function parseDate(str) {
            var mdy = str.split('/')
            return new Date(mdy[2], mdy[0] - 1, mdy[1]);
        }

        function daydiff(first, second) {
            return (second - first) / (1000 * 60 * 60 * 24);
        }
        function ValidateShipPo() {

            var defaultDateRange = '10';
            var uploadDate = document.getElementById("<%=txtShippingDate.ClientID %>").value;
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
                var daterange = daydiff(parseDate(today), parseDate(uploadDate));
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

            var msg = '';
            //	        var ShipBy = document.getElementById("").value;
            //	        if (ShipBy == '')
            //	            msg = 'Shipvia cannot be empty!';

            var TrackingNumber = document.getElementById("<%= txtTrackingNumber.ClientID %>").value;
            var price = document.getElementById("<%= txtPrice.ClientID %>").value;
            var chkManual = document.getElementById("<%= chkTracking.ClientID %>")
            if (chkManual.checked) {


                if (price == '') {
                    alert('Price cannot be empty!');
                    return false;
                }
                if (TrackingNumber == '') {
                    alert('Tracking number cannot be empty!');
                    return false;
                }


            }
                
	        <%--var ShipVia = document.getElementById("<%= ddlShipVia.ClientID %>");
	        if (ShipVia.selectedIndex == 0 && msg == '')
	            msg = 'ShipVia cannot be empty!';
	        else
	            if (state.selectedIndex == 0 && msg != '')
	                msg = 'ShipVia, ' + msg;--%>

            if (msg == '') {
                return true;
            }
            else {
                // alert(msg);
                return false;
            }
        }
       
        
    </script>

    <script language="javascript" type="text/javascript">

        function ReadOnlys(evt) {
            var img3 = document.getElementById("img3");
            img3.click();
            evt.keyCode = 0;
            return false;

        }
		    function ReadOnly(evt) {
		        var imgCall = document.getElementById("imgCal");
		        imgCall.click();
		        evt.keyCode = 0;
		        return false;

        }
       </script> 
    
   		<link rel="stylesheet"  type="text/css" href="../fullfillment/calendar/dhtmlgoodies_calendar.css" media="screen" />
		<script type="text/javascript" src="../fullfillment/calendar/dhtmlgoodies_calendar.js"></script>  
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
                <td>&nbsp;Fulfillment Shipping</td></tr>
             </table>
                
            </td>
        </tr>
        
    <tr>
            <td>

        <asp:UpdatePanel ID="upShipItem" runat="server">
					<ContentTemplate>
                   
						<asp:PlaceHolder ID="phShipItem" runat="server">
                            <asp:Label ID="lblShipItem" runat="server"  CssClass="errormessage"></asp:Label>

                            <table width="100%" align="center" cellpadding="0" cellspacing="0" id="tblTracking" runat="server">
                            <%--<tr >
                            <td colspan="2">
                            <table width="100%" border="0" cellpadding="0" cellspacing="0" align="center">

                            <tr valign="bottom" style="height:5px">
                                <td align="left" class="buttonlabel" style="width:100%" >
                                Shipment
                                </td>

                            </tr>
                            </table>
                            </td>
                            </tr>--%>
                            <tr>
                                <td>
                                   
                                    <div style="display:none">
                                    <asp:Button ID="btnhdPrintlabel" runat="server"   Text="Printlabel" OnClick="btnhdnPrintlabel_Click" /> 
                                    </div>
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
                                        </ItemTemplate> 
                
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Shipment Type" ItemStyle-CssClass="copy10grey" ItemStyle-Width="7%">
                                        <ItemTemplate>
                                        <%# Convert.ToString(Eval("ReturnValue")) == "S" ? "Ship": Convert.ToString(Eval("ReturnValue")) == "R" ? "Return": "Ship" %>
                
                                        </ItemTemplate>
                
                                    </asp:TemplateField>
            
                                            
                                    <asp:TemplateField HeaderText="ShipBy" SortExpression="ShipBy"  ItemStyle-CssClass="copy10grey" ItemStyle-Width="3%">
                                        <ItemTemplate>
                                            <%#Eval("ShipByCode")%>
                                            <asp:HiddenField ID="hdShipVia" runat="server" Value='<%#Eval("ShipByCode")%>' />
                                        </ItemTemplate>

                                    </asp:TemplateField>
                     
                                    <asp:TemplateField HeaderText="ShipDate"   ItemStyle-CssClass="copy10grey" ItemStyle-Width="7%" >
                                        <ItemTemplate>
                
                                        <%# Convert.ToDateTime(Eval("ShipDate")).ToShortDateString() == "1/1/0001" ? "" : Eval("ShipDate")%>
                
                                        </ItemTemplate>
                
                                    </asp:TemplateField>
                        
                                    <asp:TemplateField HeaderText="Ship Package"   ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <%#Eval("ShipPackage")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>     
                                    <asp:TemplateField HeaderText="Weight" ItemStyle-HorizontalAlign="Right"   ItemStyle-CssClass="copy10grey" ItemStyle-Width="3%">
                                        <ItemTemplate>
                                            <%#Eval("ShipWeight")%>&nbsp;
                                        </ItemTemplate>
                                    </asp:TemplateField>     
                                    <asp:TemplateField HeaderText="Price" ItemStyle-HorizontalAlign="Right"  ItemStyle-CssClass="copy10grey" ItemStyle-Width="3%">
                                        <ItemTemplate>
                                            $<%# Convert.ToDecimal(Eval("ShipPrice")).ToString("0.00")%>&nbsp;
                                        </ItemTemplate>
                                    </asp:TemplateField>     
                                    <asp:TemplateField HeaderText="TrackingNumber" SortExpression="TrackingNumber" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%" >
                                        <ItemTemplate>   
                                            
                                                <%# Eval("TrackingNumber")%>                                          

                                        </ItemTemplate>
                                                
                                    </asp:TemplateField>
                                   

                                        
              <%--<asp:TemplateField HeaderText=""  ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%" ItemStyle-HorizontalAlign="Center" >
                <ItemTemplate>
                     <asp:LinkButton ID="lnkCustom" runat="server" CausesValidation="false" 
                        CommandArgument='<%# Eval("LineNumber") %>'  OnCommand="lnkCustom_Command" ToolTip="View custom declaration" AlternateText="View custom declaration" 
                        >Custom Info</asp:LinkButton>
                  
                </ItemTemplate>
            </asp:TemplateField>   
              --%>                    
            <asp:TemplateField HeaderText=""  ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:HiddenField ID="hdTN" Value='<%# Eval("TrackingNumber") %>' runat="server" />
                     <asp:ImageButton ID="imgPrint" runat="server" CausesValidation="false" 
                        CommandArgument='<%# Eval("LineNumber") %>'  OnCommand="imgPrint_Command" ToolTip="Print Label" AlternateText="View Label" 
                        ImageUrl="~/images/printer.png" />
                </ItemTemplate>
            </asp:TemplateField>   
              
              
            <asp:TemplateField HeaderText=""  ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                 <asp:ImageButton ID="imgDelTr" runat="server" OnClientClick="return confirm('Are you sure you want to delete?')"  
                    CommandArgument='<%# Eval("LineNumber") %>'  OnCommand="imgDelTracking_Command" AlternateText="Delete Tracking" ImageUrl="~/images/delete.png" />
                </ItemTemplate>
            </asp:TemplateField>                                                   
			                                
        </Columns>
    </asp:GridView>
   <br />
                                </td>
                            </tr>
                            </table>

                            <table bordercolor="#839abf" border="1" width="100%" align="center" cellpadding="0" cellspacing="0">
                                <tr><td>

                                <table border="0" width="100%" class="box" align="center" cellpadding="5" cellspacing="5">
                                <tr>
                                    <td class="copy10grey" align="right" width="17%">
                                       <strong> Fulfillment#: </strong>
                                    </td>
                                    <td width="34%">

                                     <strong> <asp:Label ID="lblShipPO" runat="server" CssClass="copy10grey"></asp:Label></strong>
                                        
                                    </td>
                                   
                                    <td class="copy10grey" align="right" width="17%">
                                       
                                    </td>
                                    <td width="32%" class="copy10grey" align="right">
                                         <asp:Button ID="btnPrintlabel" runat="server" CssClass="button"   Text="Print label" OnClick="btnhdPrintlabel_Click" /> &nbsp;
   
                                         </td>
                                    </tr>
                               
                                    <tr valign="top">
                                    <td class="copy10grey" align="right" width="17%">
                                        Requested Ship Date:
                                    </td>
                                    <td width="34%">

                                        <asp:TextBox ID="txtShippingDate" Width="91%" onkeydown="return ReadOnlys(event);" runat="server" CssClass="copy10grey"></asp:TextBox>&nbsp;
                                        <img id="img3" alt=""  onclick="document.getElementById('<%=txtShippingDate.ClientID%>').value = ''; displayCalendar(document.getElementById('<%=txtShippingDate.ClientID%>'),'mm/dd/yyyy',this,true);" src="../fullfillment/calendar/sscalendar.jpg" />
                
                                    </td>
                                   
                                    <td class="copy10grey" align="right" width="17%">
                                        Ship Via:
                                    </td>
                                    <td width="32%" class="copy10grey" >
                                         <asp:DropDownList ID="ddlShipVia" runat="server" onchange="ShowManualTracking(this);" Width="91%" CssClass="copy10grey"></asp:DropDownList>
                                       
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
                                        Add Manual Tracking: 
                                        </td>
                                        <td  width="34%">
                                           <asp:CheckBox ID="chkTracking"   runat="server" onclick="ShowTracking(this);" />
                                        </td>
                                        
                                    <td class="copy10grey" align="right" width="17%">
                                          Tracking Number:
                                    </td>
                                    <td width="32%" class="copy10grey" >
                                         <asp:TextBox ID="txtTrackingNumber" Width="91%" Enabled="false" onkeypress="return isNumberKey(event);" MaxLength="50" CssClass="copy10grey" runat="server"></asp:TextBox>
                                        
                                        <%--<asp:CheckBox ID="chkLabel" Visible="false" Checked="true" runat="server" />

                                        <asp:Button ID="btnGenLabel" runat="server" Visible="false" Text="Generate Label" OnClick="btnGenLabel_Click" CausesValidation="false" CssClass="button" />
                                            --%>
                                    </td>
                                    </tr>
                                    <tr >
                                        <td class="copy10grey" align="right" width="17%">
                                                  Price:     
                                        </td>
                                        <td  width="34%">
                                            <asp:TextBox ID="txtPrice" Enabled="false"  Width="51%"  MaxLength="10" onkeypress="return ValidateWeight(event, this);"  CssClass="copy10grey" runat="server"></asp:TextBox>
                                         
                                            
                                        </td>
                                        
                                    <td class="copy10grey" align="right" width="17%">
                                         
                                    </td>
                                    <td width="32%" class="copy10grey" >
                                         
                                    </td>
                                    </tr>
                                    
                                </table>
                                </td>
                                </tr>
                        </table>
                            <asp:Panel ID="pnlCustom" runat="server" Visible="false">
                                <br />
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
                                                        &nbsp;Custom Value
                                                    </td>                                                    
                                                </tr>                            
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                            <tr class="<%# Container.ItemIndex % 2 == 0 ? "alternaterow" : "" %>">
                                                
                                                <td class="copy10grey">
                                                        &nbsp;<%# Container.ItemIndex + 1 %></td>
                                
                                                <td class="copy10grey">
                                                        &nbsp;
                                                    <asp:Label ID="lbSKU"  CssClass="copy10grey"  Text='<%# Eval("ItemCode")%>'   runat="server"></asp:Label>
                                                    <asp:HiddenField ID="hdQty"  Value='<%# Eval("Quantity")%>'   runat="server"></asp:HiddenField>
                                                    <asp:HiddenField ID="hdPODID"   Value='<%# Eval("PodID")%>'   runat="server"></asp:HiddenField>

                                                    </td>
                                                <td class="copy10grey">
                                                        &nbsp;<%# Eval("Quantity")%>
                                                    </td>
                                                
                                                <td class="copy10grey">
                                                        &nbsp;
                                                    <asp:Label ID="lbName"  CssClass="copy10grey"  Text='<%# Eval("ProductName")%>'   runat="server"></asp:Label>
                                                    </td>
                                                <td class="copy10grey">

                                                     <asp:TextBox ID="txtValue" onkeypress="return ValidateWeight(event, this);" CssClass="copy10grey" MaxLength="7"   runat="server"></asp:TextBox>

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
                   
                            </asp:Panel>
                            <br />
                             <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="100%"  align="center">
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
                         <asp:Button ID="btnShip" runat="server" OnClick="btnAddShip_Click" Text="Submit" CssClass="button" OnClientClick="return ValidateShipPo();" />&nbsp;
                         <asp:Button ID="btnShipCancel" runat="server" Text="Close" CssClass="button" Visible="true" OnClientClick="return close_window();"  />
                    </td>
                </tr>
                </table>
	
                
                        </asp:PlaceHolder>
						
						
					</ContentTemplate>
					<Triggers>
                        <asp:PostBackTrigger ControlID="btnPrintlabel" />
                         <asp:PostBackTrigger ControlID="btnhdPrintlabel" />
                           
					</Triggers>		
				</asp:UpdatePanel>
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
