<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RmaShipLabel.aspx.cs" Inherits="avii.RMA.RmaShipLabel" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>
<%@ Register TagPrefix="menu" TagName="Menu" Src="~/Controls/Header.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>RMA Shipping Label</title>
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
            var objCost = document.getElementById("<%= txtCost.ClientID%>");

            if (obj.checked) {
                objTrackingNumber.disabled = false;
                objCost.disabled = false;
            }
            else {
                objTrackingNumber.disabled = true;
                objCost.disabled = true;
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
            var cost = document.getElementById("<%= txtCost.ClientID %>").value;

            var chkManual = document.getElementById("<%= chkTracking.ClientID %>")
            if (chkManual.checked) {
                if (TrackingNumber == '') {
                    alert('Tracking number cannot be empty!');
                    return false;
                }
                if (cost == '') {
                    alert('Cost is required!');
                    return false;
                }
                if (cost == 0) {
                    alert('Cost cannot be 0!');
                    return false;
                }


            }

            var prepaid = document.getElementById("<%= ddlPrepaid.ClientID %>")
            if (prepaid.selectedIndex == 0) {
                alert('Prepaid is required!');
                return false;
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
<body bgcolor="#ffffff" leftmargin="0" rightmargin="0" topmargin="0">
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
     <table cellspacing="0" cellpadding="0" border="0" align="center" style="text-align:left" width="95%">
        <tr class="buttonlabel" align="left">
        <td>&nbsp;RMA Shipping Label</td></tr>
    </table>
        <asp:UpdatePanel ID="upShipItem" runat="server">
		    <ContentTemplate>                   
			    <asp:PlaceHolder ID="phShipItem" runat="server">
                <asp:Label ID="lblShipItem" runat="server"  CssClass="errormessage"></asp:Label>
                
                <div style="display:none">
                <asp:Button ID="btnhdPrintlabel" runat="server"   Text="Printlabel" OnClick="btnhdnPrintlabel_Click" /> 
                </div>
                <table  cellSpacing="0" cellPadding="0" width="95%" align="center" runat="server" id="trShip">
                <tr>
                <td>

                <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="100%" align="center">
                <tr>
                <td>
			    <table  cellSpacing="0" cellPadding="0" width="100%" border="0" > 
                <tr>
                    <td>

                        <asp:Repeater ID="rptTracking" runat="server" >
                        <HeaderTemplate>
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
                                <td class="buttongrid" align="left" width="22%">
                                    Tracking Number
                                        </td>
                                <td class="buttongrid" align="left" width="8%">
                                    Prepaid
                                        </td>
                                <td class="buttongrid" align="left" width="1%">
                                    Print
                                        </td>

                            </tr>        
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr class="<%# Container.ItemIndex % 2 == 0 ? "alternaterow" : "" %>">
                                    
                                    <td class="copy10grey" align="right" width="1%">
                                        <%# Container.ItemIndex + 1 %>
                                         
                                    </td>
                                    
                                    <td  class="copy10grey" >
                                        <%# Eval("ShipVia") %>
                                       
                                     </td>
                                    <td  class="copy10grey" >
                                        <%# Eval("ShipDate") %>
                                       
                                        </td>
                                    <td  class="copy10grey" >
                                        <%# Eval("Package") %>
                                        </td>
                                    <td class="copy10grey" >
                                            <%# Eval("Weight") %>
                                             
                                        </td>
                                    <td class="copy10grey" >
                                        $<%# Convert.ToDecimal(Eval("FinalPostage")).ToString("0.00") %>
                                                 
                                        </td>
                                        <td class="copy10grey" >
                                            <%# Eval("TrackingNumber") %>
                                                 
                                        </td>
                                <td class="copy10grey" >
                                            <%# Eval("Prepaid") %>
                                                 
                                        </td>
                                <td>
                                     <asp:ImageButton ID="imgPrint" runat="server" CausesValidation="false" 
                        CommandArgument='<%# Eval("TrackingId") %>'  OnCommand="imgPrint_Command" ToolTip="Print Label" AlternateText="Print Label" 
                        ImageUrl="~/images/printer.png" />
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
                            <br />
                                </td>
                            </tr>
                            </table>
                            <table bordercolor="#839abf" border="1" width="95%" align="center" cellpadding="0" cellspacing="0">
                                <tr><td>

                                <table border="0" width="100%" class="box" align="center" cellpadding="5" cellspacing="5">
                                <tr>
                                    <td class="copy10grey" align="right" width="17%">
                                       <strong> RMA#: </strong>
                                    </td>
                                    <td width="34%">

                                      <asp:Label ID="lblRMA" runat="server" CssClass="copy10grey"></asp:Label>
                                        
                                    </td>
                                   
                                    <td class="copy10grey" align="right" width="17%">
                                       Customer RMA#:
                                    </td>
                                    <td width="32%" class="copy10grey" >
                                        <asp:Label ID="lblCustomerRMA" runat="server" CssClass="copy10grey"></asp:Label>
                                         </td>
                                    </tr>
                                    <tr>
                                    <td class="copy10grey" align="right" width="17%">
                                       <strong> RMA Date: </strong>
                                    </td>
                                    <td width="34%">

                                     <asp:Label ID="lblRmaDate" runat="server" CssClass="copy10grey"></asp:Label>
                                        
                                    </td>
                                   
                                    <td class="copy10grey" align="right" width="17%">
                                       RMA Status:
                                    </td>
                                    <td width="32%" class="copy10grey" >
                                        <asp:Label ID="lblRmaStatus" runat="server" CssClass="copy10grey"></asp:Label>
                                         </td>
                                    </tr>
                                </table>
                                </td>
                                </tr>
                        </table>
                        <br />
                            <table bordercolor="#839abf" border="1" width="95%" align="center" cellpadding="0" cellspacing="0">
                                <tr><td>

                                <table border="0" width="100%" class="box" align="center" cellpadding="5" cellspacing="5">
                                
                               
                                    <tr valign="top">
                                    <td class="copy10grey" align="right" width="17%">
                                        <strong>Ship Date:</strong>
                                    </td>
                                    <td width="34%">

                                        <asp:TextBox ID="txtShippingDate" Width="51%" onkeydown="return ReadOnlys(event);" runat="server" CssClass="copy10grey"></asp:TextBox>&nbsp;
                                        <img id="img3" alt=""  onclick="document.getElementById('<%=txtShippingDate.ClientID%>').value = ''; displayCalendar(document.getElementById('<%=txtShippingDate.ClientID%>'),'mm/dd/yyyy',this,true);" src="../fullfillment/calendar/sscalendar.jpg" />
                
                                    </td>
                                   
                                    <td class="copy10grey" align="right" width="17%">
                                        <strong>Ship Via:</strong>
                                    </td>
                                    <td width="32%" class="copy10grey" >
                                         <asp:DropDownList ID="ddlShipVia" runat="server" Width="81%" CssClass="copy10grey"></asp:DropDownList>
                                       
                                         </td>
                                </tr>
                                    <tr>
                                        <td class="copy10grey" align="right" width="17%">
                                            <strong>Shipping Weight(Oz):</strong>
                                        </td>
                                        <td  width="34%">
                                            
                                             <asp:TextBox ID="txtWeight" Width="51%" Text="1" MaxLength="6" onkeypress="return ValidateWeight(event, this);" CssClass="copy10grey" runat="server"></asp:TextBox>
                                        </td>
                                        
                                    <td class="copy10grey" align="right" width="17%">
                                           <strong>Ship Package</strong>
                                    </td>
                                    </td>
                                    <td width="32%" class="copy10grey" >
                                        <asp:DropDownList ID="ddlShape" runat="server" Width="81%" CssClass="copy10grey"></asp:DropDownList>
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
                                         <asp:TextBox ID="txtTrackingNumber" Width="81%" Enabled="false" onkeypress="return isNumberKey(event);" MaxLength="50" CssClass="copy10grey" runat="server"></asp:TextBox>
                                        
                                    </td>
                                    </tr>
                                    <tr>
                                        <td class="copy10grey" align="right" width="17%">
                                            Prepaid:    
                                        </td>
                                        <td  width="34%">
                                            <asp:DropDownList ID="ddlPrepaid"  runat="server" class="copy10grey" Width="51%" >
                                            <asp:ListItem Text="" Value=""></asp:ListItem>
                                            <asp:ListItem Text="Customer" Value="Customer"></asp:ListItem>
                                            <asp:ListItem Text="Internal" Value="Internal"></asp:ListItem>                                            
                                        </asp:DropDownList>
                                        </td>
                                        
                                    <td class="copy10grey" align="right" width="17%">
                                          Cost:
                                    </td>
                                    <td width="32%" class="copy10grey" >
                                        <asp:TextBox ID="txtCost" Width="81%" Enabled="false" onkeypress="return ValidateWeight(event, this);"  MaxLength="6" CssClass="copy10grey" runat="server"></asp:TextBox>
                                         
                                        <%--<asp:Label ID="lblCost" CssClass="copy10grey" runat="server"></asp:Label>--%>
                                        
                                    </td>
                                    </tr>
                                    
                                </table>
                                
                            <br />
                             <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="95%"  align="center">
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
                         <asp:Button ID="btnShipCancel" runat="server" Text="Close" CssClass="button"  OnClientClick="return close_window();"  />
                    </td>
                </tr>
                </table>
	
                
                        </asp:PlaceHolder>
			</ContentTemplate>							
		</asp:UpdatePanel>
        

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
