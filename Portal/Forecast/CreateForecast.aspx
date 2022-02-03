<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateForecast.aspx.cs" Inherits="avii.CreateForecast" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="/Controls/Footer.ascx" %>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="/Controls/Header.ascx" %>
<%@ Register TagPrefix="UC" TagName="Comments" Src="~/Controls/ForecastComment.ascx" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>LAN Global Inc. - Manage Forecast</title>
    <link href="../aerostyle.css" type="text/css" rel="stylesheet" />
    <link rel="stylesheet"  type="text/css" href="../fullfillment/calendar/dhtmlgoodies_calendar.css" media="screen" />
    <script type="text/javascript" src="../fullfillment/calendar/dhtmlgoodies_calendar.js"></script>
    
<!-- from http://encosia.com/2009/10/11/do-you-know-about-this-undocumented-google-cdn-feature/ -->
    <link href="https://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/themes/start/jquery-ui.css" type="text/css" rel="Stylesheet" />
	
	<script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.4.0/jquery.min.js"></script>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/jquery-ui.min.js"></script>
    <%--<script type="text/javascript" src="/JQuery/jquery.min.js"></script>
    <script type="text/javascript" src="/JQuery/jquery-ui.min.js"></script>
	--%>
	<script type="text/javascript" src="/JQuery/jquery.blockUI.js"></script>
	


<!-- fix for 1.1em default font size in Jquery UI -->
	<style type="text/css">
		.ui-widget{font-size:11px !important;}
		.ui-state-error-text{margin-left: 10px}
	</style>
    <script type="text/javascript">
        $(document).ready(function () {
            
            $("#divSKU").dialog({
                autoOpen: false,
                modal: true,
                minHeight: 290,
                height: 290,
                width: 500,
                resizable: false,
                open: function (event, ui) {
                    $(this).parent().appendTo("#divContainer");
                },
            });

            $("#divComment").dialog({
                autoOpen: false,
                modal: true,
                minHeight: 300,
                height: 400,
                width: 650,
                resizable: false,
                open: function (event, ui) {
                    $(this).parent().appendTo("#divContainer");
                },
            });

        });


        function closeDialog() {
            //Could cause an infinite loop because of "on close handling"
            $("#divComment").dialog('close');
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
            $("#divComment").dialog("option", "title", title);
            $("#divComment").dialog("option", "position", [left, top]);

            $("#divComment").dialog('open');

        }


        function openDialogAndBlock(title, linkID) {

            openDialog(title, linkID);
            //alert('2')
            //block it to clean out the data
            $("#divComment").block({
                message: '<img src="../images/async.gif" />',
                css: { border: '0px' },
                fadeIn: 0,
                //fadeOut: 0,
                overlayCSS: { backgroundColor: '#ffffff', opacity: 1 }
            });
        }

        function unblockDialog() {
            $("#divComment").unblock();
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
            //top = top - 300;
            if (top > 600)
                top = 10;

            top = 100;
            //alert(top);
            left = 450;
            $("#divSKU").dialog("option", "title", title);
            $("#divSKU").dialog("option", "position", [left, top]);

            $("#divSKU").dialog('open');

        }


        function openSKUDialogAndBlock(title, linkID) {

            openSKUDialog(title, linkID);
            //alert('2')
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


        function set_focus1() {
            var img = document.getElementById("imgFcDate");

            var st = document.getElementById("txtCommments");
            st.focus();
            img.click();
        }


	</script>
    


    
    <script type="text/javascript">

        function IsValidate() {
            var company = document.getElementById("<% =dpCompany.ClientID %>");
            //alert(company);
            if (company != 'null' && company.selectedIndex == 0) {
                alert('Customer is required!');
                return false;

            }
            var defaultDateRange = document.getElementById("<%=hdnDateRange.ClientID %>").value;
            var forecastDate = document.getElementById("<%=txtFcDate.ClientID %>").value;
            if (forecastDate == '') {
                alert('Forecast date is required!');
                return false;
            }
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
            ///alert(daydiff(parseDate(today), parseDate(forecastDate)));
            var daterange = daydiff(parseDate(today), parseDate(forecastDate));

            if (daterange > defaultDateRange) {
                alert('Forecast can  not be created before 180 days');
                return false;
            }
            if (daterange <= 0) {
                alert('Forecast can  not be created on or before today date');
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

        function isQty(obj) {

            var objPrice = document.getElementById(obj.id.replace('txtQty', 'lblPrice'));
            //alert(objPrice);
            //alert(objPrice.innerHTML);
            var objTotalPrice = document.getElementById(obj.id.replace('txtQty', 'lblTotal'));
            
            if (obj.value == '0') {
                alert('Quantity can not be zero');
                obj.value = '1';
                if (objPrice != null)
                    objTotalPrice.innerHTML = objPrice.innerHTML * obj.value;

                return false;
            }
            if (obj.value == '') {
                alert('Quantity can not be empty');
                obj.value = '1';
                if (objPrice != null)
                    objTotalPrice.innerHTML = objPrice.innerHTML * obj.value;

                return false;
            }
            if (objPrice != null)
                objTotalPrice.innerHTML = objPrice.innerHTML * obj.value;

        }
        function isNumberKey(evt) {

            var charCodes = evt.keyCode ? evt.keyCode : evt.which ? evt.which : evt.charCode;
            if (charCodes > 31 && (charCodes < 48 || charCodes > 57)) {
                charCodes = 0;
                return false;
            }
            return true;
        }
        </script>
   
</head>
<body bgColor="#ffffff" leftmargin="0" rightmargin="0" topmargin="0">
<form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

        
	<table cellspacing="0" cellpadding="0"  border="0" align="center" width="100%">
	<tr>
		<td>
			<head:menuheader id="HeadAdmin" runat="server"></head:menuheader>
		</td>
	 </tr>
     </table>
    <br />
    
    <div id="divContainer">	
			<div id="divComment" style="display:none">
					
				<asp:UpdatePanel ID="upnlComment" runat="server">
					<ContentTemplate>
                   
						<asp:PlaceHolder ID="phrComment" runat="server">
                            <table width="100%" border="0">
                                
                                <tr>
                                    <td colspan="2">
                                
                                    <asp:Panel ID="pnlComment" runat="server" Width="100%">
                                        <asp:Label ID="lblCMsg" runat="server" Width="100%" CssClass="errormessage"></asp:Label>
                                        <UC:Comments ID="c1" runat="server" ></UC:Comments>
                                    
                                    </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                                
                        </asp:PlaceHolder>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div id="divSKU" style="display:none">
					
				<asp:UpdatePanel ID="upSKU" runat="server">
					<ContentTemplate>
                   
						<asp:PlaceHolder ID="phSKU" runat="server">
                            <%--<table width="100%" border="0">
                                
                            <tr>
                                <td >--%>
                                
                                    <asp:Panel ID="pnlSKU" runat="server" Width="100%" DefaultButton="btnAdd">
                                    <asp:Label ID="lblSKU" runat="server" Width="100%" CssClass="errormessage"></asp:Label>
                                    <table bordercolor="#839abf" border="1" width="98%" align="center" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>

                                        <table border="0" width="100%" class="box" align="center" cellpadding="5" cellspacing="5">
                    
                                        <tr>
                                            <td class="copy10grey" width="25%">
                                                SKU#:
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlSKU" runat="server" Width="80%" class="copy10grey" >
                                                </asp:DropDownList>
                                        
                                            </td>
                                        </tr>
                                        <tr valign="top">
                                            <td class="copy10grey" width="25%">
                                                Quantity:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtQuantity" MaxLength="3" Width="80%" Text="1" onkeypress="return isNumberKey(event);" onchange="return isQuantity(this);"  CssClass="copy10grey" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
<tr valign="top">
                                        <td class="copy10grey" width="25%">
                                        Comments:
                                        </td>
                                        <td>
                                            
                                
                                <asp:TextBox ID="txtFcComments" CssClass="copy10grey" Width="100%" TextMode="MultiLine" Height="70"  runat="server"
                                onkeydown="checkTextAreaMaxLength(this,event,'2000');" onkeyup="checkTextAreaMaxLength(this,event,'2000');" onblur="onBlurTextCounter(this,'2000');"
                                ></asp:TextBox>
                                <br />
                                <span class="copy8grey">
                                Max. length 500 char.
                                </span>
                            
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
                                                <asp:Button ID="btnAdd" runat="server" Text="Submit"  OnClick="btnAddLineItem_Click" CssClass="button" />
                                                &nbsp;
                                                <asp:Button ID="btnAddCancel" runat="server" Text="Cancel" CssClass="button" OnClientClick="return closeSKUDialog();" />
                                        </td>
                                    </tr>
                                    </table>
                    
                                    </asp:Panel>
                                <%--</td>
                            </tr>
                            </table>--%>
                                
                        </asp:PlaceHolder>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
    </div>
<table cellspacing="0" cellpadding="0"  border="0" align="center" width="95%">
<tr>
    <td>
    <table  cellspacing="1" cellpadding="1" width="100%">
    <tr>
		<td colspan="6" bgcolor="#dee7f6" class="buttonlabel">&nbsp;&nbsp; Forecast
		</td>
    </tr>
    </table>   
    <br />
<asp:UpdatePanel ID="upnlCustomers" UpdateMode="Conditional" runat="server">
<ContentTemplate>

	<asp:LinkButton ID="btnRefreshGrid" CausesValidation="false" OnClick="btnRefreshGrid_Click" style="display:none" runat="server"></asp:LinkButton>		
    <asp:HiddenField ID="hdnDateRange" runat="server" />
    <table width="100%">
                         
        <tr><td align="left">
        <asp:Label ID="lblMsg" runat="server" CssClass="errormessage"></asp:Label></td></tr>
    </table>

    
            <table cellspacing="0" cellpadding="0" width="100%" >
            <tr valign="top">
                <td >
                    <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="100%" >
                    <tr bordercolor="#839abf">
                        <td>
                            <table width="100%" bgColor="#ffffff" leftmargin="0" rightmargin="0" topmargin="0" border="0">
                            <tr height="8">
                                <td>
                               
                                </td>
                            </tr>
                            <tr>
                                <td colspan="5">
                                <asp:Panel ID="pnlCustomer" runat="server">
                                    
                                    <table width="100%" cellpadding="0" cellspacing="0">
                                    <tr>
                                
                                        <td align="left" class="copy10grey" width="15%">Customer:</td>
                                
                                        <td align="left" width="35%">
                                            <asp:DropDownList ID="dpCompany" CssClass="copy10grey" runat="server" Width="55%"
                                            OnSelectedIndexChanged="dpCompany_SelectedIndexChanged"  
                                            AutoPostBack="true">
									        </asp:DropDownList>
                                    
                                            </td>
                                        <td align="left" class="copy10grey" width="15%">Status:</td>
                                        <td>&nbsp;</td>
                                        <td width="35%">
                                            &nbsp;<asp:DropDownList ID="ddlStatus" runat="server" Width="70%" class="copy10grey" >
                                            <asp:ListItem Text="" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Pending" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Received" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="Cancelled" Value="3"></asp:ListItem>
                                            <asp:ListItem Text="Fulfilled" Value="4"></asp:ListItem>
                                            <asp:ListItem Text="OutOfOrder" Value="5"></asp:ListItem>
                                            <asp:ListItem Text="Partial Fulfilled" Value="6"></asp:ListItem>
                                            <asp:ListItem Text="Deleted" Value="7"></asp:ListItem>
                                            </asp:DropDownList>
                                    
                                        </td>
                                    </tr>
                                    </table>
                                </asp:Panel>
                                
                                
                                </td>
                                
                            </tr>
                            
                            <tr>
                                <td align="left" class="copy10grey" width="15%">Forecast#:</td>
                                
                                <td align="left" width="35%">
                                    <asp:Label ID="lblForecastNum" MaxLength="19" runat="server" CssClass="copy10grey"></asp:Label>
                                    
                                    </td>
                                <td align="left" class="copy10grey" width="15%">Forecast Date:</td>
                                <td><FONT color="#ff0000">*</FONT></td>
                                <td width="35%">
                                    <asp:TextBox ID="txtFcDate" runat="server" onfocus="set_focus1();"  CssClass="copy10grey" MaxLength="15"  Width="70%"></asp:TextBox>
                                    <img id="imgFcDate" alt="" onclick="document.getElementById('<%=txtFcDate.ClientID%>').value = ''; displayCalendar(document.getElementById('<%=txtFcDate.ClientID%>'),'mm/dd/yyyy',this,true);" src="../fullfillment/calendar/sscalendar.jpg" />
                                     <asp:RequiredFieldValidator ID="reqPoDate" CssClass="copy10grey"
                                          ErrorMessage="Forecast Date is required"
                                           ControlToValidate="txtFcDate"
                                          EnableClientScript="false"
                                          Display="None"
                                          runat="server">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr  height="8">
                                <td  >
                                
                                </td>
                            </tr>
                            </table>
                        </td>
                    </tr>
                    </table>
                           
                </td>
                        
           </tr>

           </table>
            <br />   
            <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="100%" >
            <tr bordercolor="#839abf">
            <td>
                <table width="100%" bgColor="#ffffff" leftmargin="0" rightmargin="0" topmargin="0" border="0">
                    <tr  height="5">
                        <td  >
                                
                        </td>
                    </tr>
                    <tr valign="top" >
                        <td class="copy10grey" align="left" valign="top">
                        <table width="100%"  leftmargin="0" rightmargin="0" topmargin="0" border="0">
                        <tr valign="top">
                            <td class="copy10grey" align="left" valign="top" width="97%">
                                Comments:
                                <br />
                                <asp:TextBox ID="txtCommments" CssClass="copy10grey" Width="100%" TextMode="MultiLine" Height="65"  runat="server"
                                onkeydown="checkTextAreaMaxLength(this,event,'2000');" onkeyup="checkTextAreaMaxLength(this,event,'2000');" onblur="onBlurTextCounter(this,'2000');"
                                ></asp:TextBox>
                                <br />
                                <span class="copy8grey">
                                Max. length 2000 char.
                                </span>
                            
                            </td>
                            <td align="left">
                                <br />
                            &nbsp;<asp:ImageButton ID="imgHistory" runat="server" CssClass="button" AlternateText="Forecast Comment History" ToolTip="Forecast Comment History" OnClick="btnHistory_Click" ImageUrl="~/Images/history.png"
                                        OnClientClick="openDialogAndBlock('Forecast Comment History', 'imgHistory')" CausesValidation="false"/>
                            <%--<asp:ImageButton ID="imgHistory2"  ToolTip="RMA History" OnCommand="imgRMAHistory_Click" 
                                                  CausesValidation="false" CommandArgument='<%# Eval("rmaGUID") %>' ImageUrl="~/Images/history.png"  runat="server" />
--%>
                            </td>
                        </tr>
                        </table>
                            
                        </td>
                    </tr>
                    <tr  height="8">
                        <td  >
                                
                        </td>
                    </tr>
                </table>
                </td>
            </tr>
            </table>
                   
            <br />
            <table cellSpacing="0" cellPadding="0" width="100%" >
            <tr class="buttonlabel">
                <td class="buttonlabel" style="width:90%">
                Forecast line items
                </td>
                <td align="right" >
                    <asp:Button ID="btnSKU" Height="100%" runat="server" CssClass="buttongray" OnClick="btnNewSKU_Click"  Text="Add Forecast Line Item" OnClientClick="openSKUDialogAndBlock('Add Forecast Line Item', 'btnSKU')"/>
                </td>
            </tr>
            </table>
            <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="100%" >
            <tr bordercolor="#839abf">
                <td>

                    <asp:Repeater ID="rptItem" runat="server" OnItemDataBound="rptItem_ItemDataBound" >
                            <HeaderTemplate>
                            <table  cellSpacing="2" cellPadding="2" width="100%" align="center" >
                                 <tr >
                                <td class="buttonlabel" width="4%">
                                Delete
                                </td>
                                <td class="buttonlabel" align="left" width="20%">
                                SKU
                                </td>
                                <td class="buttonlabel" align="left" width="10%">
                                Quantity
                                </td>
                                <td class="buttonlabel" width="10%">
                                Price
                                </td>
                                <td class="buttonlabel" width="10%">
                                Total
                                </td>
                                <td class="buttonlabel" width="10%">
                                Status
                                </td>
                                <td class="buttonlabel" width="30%">
                                Comments
                                </td>
                                
                               </tr>
                               </HeaderTemplate>
                                  <ItemTemplate>
                                  <tr>

                                    <td>
                                
                                        <asp:CheckBox ID="chkDel" Visible='<%# Convert.ToString(Eval("sku"))=="" ? false : true %>' CssClass="copy10grey" runat="server" />
                                    </td>
                                    <td align="left">
                                    <%--<asp:HiddenField ID="hdnItemID" Value='<%# Eval("ItemID") %>' runat="server" />--%>
                        
                                        <asp:Label ID="lblSKU" Text='<%# Eval("SKU") %>' CssClass="copy10grey"  runat="server"></asp:Label>

                                        <%--<asp:TextBox ID="txtItemCode" AutoPostBack="true"  OnTextChanged="txtItemCode_SelectedIndexChanged" MaxLength="20" Width="40%" CssClass="copy10grey" Text='<%#Eval("itemCode")%>'  runat="server"></asp:TextBox>

                                        <asp:ImageButton ID="img" ToolTip="View ItemCode" runat="server" CausesValidation="false"  OnClientClick="return StoreItemCode(this);" OnClick="lnkCode_Click"  ImageUrl="~/images/view.png" />
                                        <asp:Label ID="lblCode" Text='<%# Eval("esn") %>' CssClass="errormessage"  runat="server"></asp:Label>
--%>
													
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtQty" MaxLength="6" Width="40%" onkeypress="return isNumberKey(event);" 
                                        onchange="return isQty(this);"  CssClass="copy10grey" Text='<%#Eval("Quantity")%>'  runat="server"></asp:TextBox>
                                    </td>
                                    <td align="right">
                                    <%#  Convert.ToInt32(Eval("price")).ToString() == "0" ? "" : "$"%>

                                    
                                        <asp:Label ID="lblPrice" Text='<%#  Convert.ToInt32(Eval("price")).ToString() == "0" ? "" : Eval("price", "{0:n}") %>' CssClass="copy10grey"  runat="server"></asp:Label>

                                    </td>
                                    <td align="right">
                                        <%#  Convert.ToInt32(Eval("totalprice")).ToString() == "0" ? "" : "$"%>
                                         <asp:Label ID="lblTotal" Text='<%#  Convert.ToInt32(Eval("totalprice")).ToString() == "0" ? "" : Eval("totalprice", "{0:n}") %>' CssClass="copy10grey"  runat="server"></asp:Label>

                                    </td>

                                   <td align="left">
                                         <asp:Label ID="lblFDStatus" Text='<%#  Eval("LineItemStatus") %>' CssClass="copy10grey"  runat="server"></asp:Label>
                                         <asp:DropDownList ID="ddlFDStatus" selectedValue='<%# Convert.ToInt32(Eval("LineItemStatus")) %>' CssClass="copy10grey"  runat="server">
                                         <asp:ListItem Text="" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Pending" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Received" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="Cancelled" Value="3"></asp:ListItem>
                                            <asp:ListItem Text="Fulfilled" Value="4"></asp:ListItem>
                                            <asp:ListItem Text="OutOfOrder" Value="5"></asp:ListItem>
                                            <asp:ListItem Text="Partial Fulfilled" Value="6"></asp:ListItem>
                                            <asp:ListItem Text="Deleted" Value="7"></asp:ListItem>
                                         </asp:DropDownList>


                                         
                                    </td>

                                    <td>
                                        <asp:Label ID="lblComments" Text='<%#  Eval("Comments") %>' CssClass="copy10grey"  runat="server"></asp:Label>
                                         
                                    
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


                <br />

                
                    <table width="100%">
                    <tr>
                        <td align="center">
                            <asp:Button ID="btnSave" runat="server" Text="Submit" CssClass="button" OnClick="btnSave_Click" OnClientClick="return IsValidate();" />&nbsp;&nbsp;
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="button" CausesValidation="false" OnClick="btnCancel_Click"  />
                        </td>
                    </tr>
                    </table>
</ContentTemplate>
</asp:UpdatePanel>

        </td>
    </tr>
    </table>		
<asp:UpdatePanel ID="upnlJsRunner" UpdateMode="Always" runat="server">
<ContentTemplate>
	<asp:PlaceHolder ID="phrJsRunner" runat="server"></asp:PlaceHolder>
</ContentTemplate>
</asp:UpdatePanel>
<asp:UpdateProgress ID="UpdateProgress1" runat="server" DynamicLayout="false">
<ProgressTemplate>
    <img src="/Images/ajax-loaders.gif" /> Loading ...
</ProgressTemplate>
</asp:UpdateProgress>


    <br />
        <table cellspacing="0" cellpadding="0" border="0" align="center" width="100%">
        <tr>
            <td >
                <foot:MenuFooter ID="footer2" runat="server"></foot:MenuFooter>
            </td>
         </tr>
         </table>
    
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.4.0/jquery.min.js"></script>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/jquery-ui.min.js"></script>
        <script type="text/javascript" src="../JQuery/jquery.blockUI.js"></script>
    </form>
</body>
</html>
