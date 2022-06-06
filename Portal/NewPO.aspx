<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewPO.aspx.cs" Inherits="avii.NewPO" %>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="/Controls/Header.ascx" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="/Controls/Footer.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
        <title>Fulfillment</title>
        <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>

    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.4.0/jquery.min.js"></script>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/jquery-ui.min.js"></script>
    <link href="https://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/themes/start/jquery-ui.css" rel="stylesheet" type="text/css" />

<%--<script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/jquery-ui.js" type="text/javascript"></script>
<link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/themes/start/jquery-ui.css" rel="stylesheet" type="text/css" />--%>
	<%--<script type="text/javascript" src="../JQuery/jquery.min.js"></script>
    <script type="text/javascript" src="../JQuery/jquery-ui.min.js"></script>--%>
	<script type="text/javascript" src="../JQuery/jquery.blockUI.js"></script>

        <%--<META HTTP-EQUIV="X-UA-Compatible" CONTENT="IE=8" />--%>
		<link href="/aerostyle.css" rel="stylesheet" type="text/css"/>
		<script language="javascript" type="text/javascript" src="./avI.js"></script> 
        <link rel="stylesheet"  type="text/css" href="../fullfillment/calendar/dhtmlgoodies_calendar.css" media="screen" />
		<script type="text/javascript" src="../fullfillment/calendar/dhtmlgoodies_calendar.js"></script>  
        <style type="text/css">
            .style1
            {
                width: 235px;
            }
            .style2
            {
                FONT-SIZE: 10px;
                COLOR: #000000;
                FONT-FAMILY: Verdana, Arial, Helvetica, sans-serif;
                width: 235px;
            }
        </style>
        <script type="text/javascript">
            function LengthValidate(obj) {
                if (obj.value.length < 5 || obj.value.length > 20) {
                    alert('Length should be between 5 to 20 characters!');
                }
            }
            function isNumberHiphen(evt) {

                var charCodes = evt.keyCode ? evt.keyCode : evt.which ? evt.which : evt.charCode;
                if (((((charCodes > 31 && (charCodes < 48 || charCodes > 57) && charCodes != 45) && charCodes != 46) && charCodes != 95) && !(charCodes > 96 && charCodes < 123)) && !(charCodes > 64 && charCodes < 91)) {
                   // alert(charCodes);

                    charCodes = 0;
                    return false;
                }

                return true;
            }
            function set_focus1() {
		        var img = document.getElementById("imgDate");
		        var st = document.getElementById("dpShipBy");
		        st.focus();
		        img.click();
		    }
	    function checkTextAreaMaxLength(textBox, e, maxLength) {
	        //if (!checkSpecialKeys(e)) 
            {
	            if (textBox.value.length > maxLength) 
                {
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
            function isNumberKey(evt) {
                
                var charCodes = evt.keyCode ? evt.keyCode : evt.which ? evt.which : evt.charCode;
                if (charCodes > 31 && (charCodes < 48 || charCodes > 57)) {
                    charCodes = 0;
                    return false;
                }
                return true;
            }
            function StoreItemCode(obj) {

                var itemCodeObj = document.getElementById(obj.id.replace('img', 'txtItemCode'));

                document.getElementById("<%= hdnCode.ClientID%>").value = itemCodeObj.id;

                return true;
            }
            function SetItemCode(obj) {
                //alert(obj.innerHTML)
                //alert(codeObj.id)
                var codeObjID = document.getElementById("<%= hdnCode.ClientID%>").value;
                var codeObj = document.getElementById(codeObjID);
                //alert(codeObj.value);
                if (codeObj != null) {
                    codeObj.value = obj.innerHTML;
                    codeObj.focus();
                }
                //codeObj.value = obj.innerHTML;
                //codeObj.focus();
                //return false;
            }
            function DisplayStoreName(obj) {
                //document   ("storeName").style.display = "block";    
                //var value = obj.value;
                //var arr = value.split('!');
                //alert(arr.length);
                //if (arr.length > 1) {
                //    var storeObj = document   (" =lblStoreName.ClientID %>");
                //    storeObj.innerHTML = arr[1];

                // }
                //alert(value);
            }             
            
        </script>
    <script type="text/javascript">
        $(document).ready(function () {

            $('#txtContactName').keypress(function (e) {
                var regex = new RegExp("^[a-zA-Z ]+$");
                var str = String.fromCharCode(!e.charCode ? e.which : e.charCode);
                if (regex.test(str)) {
                    return true;
                }
                e.preventDefault();
                return false;
            });

            $('#txtAddress1').keypress(function (e) {
                var regex = new RegExp("^[a-zA-Z0-9#-():,._ ]+$");
                var str = String.fromCharCode(!e.charCode ? e.which : e.charCode);
                if (str == '&' || str == '%' || str == '$') {
                    e.preventDefault();
                    return false;
                }
                //alert(str);
                if (regex.test(str)) {
                    return true;
                }
                e.preventDefault();
                return false;
            });

            $('#txtAddress2').keypress(function (e) {
                var regex = new RegExp("^[a-zA-Z0-9#-():,._ ]+$");
                var str = String.fromCharCode(!e.charCode ? e.which : e.charCode);
                if (str == '&' || str == '%' || str == '$') {
                    e.preventDefault();
                    return false;
                }
                //alert(str);
                if (regex.test(str)) {
                    return true;
                }
                e.preventDefault();
                return false;
            });
            $('#txtPhone').keypress(function (e) {
                var regex = new RegExp("^[0-9\-\(\)\s]+$");
                var str = String.fromCharCode(!e.charCode ? e.which : e.charCode);
                //alert(str);
                if (regex.test(str)) {
                    return true;
                }
                e.preventDefault();
                return false;
            });
            $('#txtCity').keypress(function (e) {
                var regex = new RegExp("^[a-zA-Z0-9 ]+$");
                var str = String.fromCharCode(!e.charCode ? e.which : e.charCode);
                if (regex.test(str)) {
                    return true;
                }
                e.preventDefault();
                return false;
            });
            $('#txtZip').keypress(function (e) {
                var regex = new RegExp("^[0-9]+$");
                var str = String.fromCharCode(!e.charCode ? e.which : e.charCode);
                if (regex.test(str)) {
                    return true;
                }
                e.preventDefault();
                return false;
            });


        });

        function skuvalidation(e) {

            var regex = new RegExp("^[a-zA-Z0-9-]+$");
            var str = String.fromCharCode(!e.charCode ? e.which : e.charCode);
            if (regex.test(str)) {
                return true;
            }
            e.preventDefault();
            return false;
        }

    </script>
</head>
<body bgColor="#ffffff" leftmargin="0" rightmargin="0" topmargin="0">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" >
                </asp:ScriptManager>
        
			<%--<table cellSpacing="0" cellPadding="0"  border="0" align="center" width="95%">
				<tr>
					<td>--%>
					    <head:menuheader id="HeadAdmin" runat="server"></head:menuheader>
				    
					<%--</td>
				</tr>--%>
                <table cellSpacing="0" cellPadding="0"  border="0" align="center" width="95%">
            <tr>
                <td>
                
                    
                    <br />
                                   
                    <asp:UpdatePanel ID="UpdatePanel1"  runat="server" ChildrenAsTriggers="true" >
        
                    <ContentTemplate>
                        <asp:HiddenField ID="hdnCode" runat="server" />
                        <table width="100%">
                         <tr >
			     <td class="buttonlabel" align="left">&nbsp;&nbsp;Fulfillment</td>
                        </tr>
                      <tr><td align="left">
                        <asp:ValidationSummary id="ValSummary" CssClass="copy10grey" HeaderText="The following errors were found:" ShowSummary="True" EnableClientScript="true" Enabled="true" ShowMessageBox="true" DisplayMode="List" Runat="server"/>
                    
                            <asp:HyperLink ID="lnkf" CssClass="copy10grey"  runat="server" Visible="false"  Text="Return to Forecast"></asp:HyperLink>
                        <asp:Label ID="lblMsg" runat="server" CssClass="errormessage"></asp:Label></td></tr>
                    </table>

                    <table cellSpacing="0" cellPadding="0" width="100%" >
                    <tr valign="top">
                        <td width="50%">
                            <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="100%" >
                   <tr bordercolor="#839abf">
                    <td>
                        <table width="100%" bgColor="#ffffff" leftmargin="0" rightmargin="0" topmargin="0">
                            <tr height="8">
                                <td>
                               
                                </td>
                            </tr>
                            <tr>
                                <td align="left" class="copy10grey">Order For:</td>
                                <td><FONT color="#ff0000">*</FONT></td>
                                <td align="left">
                                    <asp:DropDownList CssClass="copy10grey"  ID="ddlSKUType" runat="server"   AutoPostBack="true" 
                                        OnSelectedIndexChanged="ddlSKUType_SelectedIndexChanged">
                                        <%--<asp:ListItem Value=""></asp:ListItem>--%>
                                        <asp:ListItem Value="SIM">SIM only</asp:ListItem>
                                        <asp:ListItem Value="Others">Others</asp:ListItem>
                                        </asp:DropDownList>
                                 
                                         <asp:RequiredFieldValidator  
                                    EnableClientScript="false" Display="None" 
                                     ID="reqSKU" ControlToValidate="ddlSKUType" 
                                     InitialValue="" runat="server"  ErrorMessage="Fulfillment order for is required"></asp:RequiredFieldValidator>
           
                                    <asp:TextBox ID="txtPoNum" Visible="false" MaxLength="20" runat="server" CssClass="copy10grey"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator ID="reqPoNum" CssClass="copy10grey"
                                          ErrorMessage="Order Number is required"
                                          ControlToValidate="txtPoNum"
                                          EnableClientScript="false" 
                                          Display="None"
                                          runat="server">*</asp:RequiredFieldValidator>--%>
                                    </td>
                                <td align="right" class="copy10grey">Order Date:</td>
                                <td><FONT color="#ff0000">*</FONT></td>
                                <td>
                                    <asp:TextBox ID="txtPoDate" runat="server" MaxLength="10" CssClass="copy10grey" Enabled="False"></asp:TextBox>
                                     <asp:RequiredFieldValidator ID="reqPoDate" CssClass="copy10grey"
                                          ErrorMessage="Order Date is required"
                                           ControlToValidate="txtPoDate"
                                          EnableClientScript="false"
                                          Display="None"
                                          runat="server">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                 <td align="left" class="copy10grey">Customer Order#:</td>
                                <td><FONT color="#ff0000">*</FONT></td>
                                <td align="left">
                                    <asp:TextBox ID="txtCustOrderNo" runat="server" MaxLength="20" CssClass="copy10grey" onchange="return LengthValidate(this);" onkeypress="return isNumberHiphen(event);"></asp:TextBox>
                                     <asp:RequiredFieldValidator ID="rfvCOrderNo" CssClass="copy10grey"
                                          ErrorMessage="Customer Order number is required"
                                           ControlToValidate="txtCustOrderNo"
                                          EnableClientScript="false"
                                          Display="None"
                                          runat="server">*</asp:RequiredFieldValidator>
                                </td>
                                <td align="right" class="copy10grey">Requested ship date:</td>
                                <td><%--<FONT color="#ff0000">*</FONT>--%>

                                </td>
                                <td>
                                    <asp:TextBox ID="txtRequestedshipdate" runat="server" onfocus="set_focus1();"  CssClass="copy10grey" MaxLength="15"  Width="80%"></asp:TextBox>
                                    <img id="imgDate" alt="" onclick="document.getElementById('<%=txtRequestedshipdate.ClientID%>').value = ''; displayCalendar(document.getElementById('<%=txtRequestedshipdate.ClientID%>'),'mm/dd/yyyy',this,true);" src="../fullfillment/calendar/sscalendar.jpg" />

                                   
<%--                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="copy10grey"
                                          ErrorMessage="Requested ship date is required"
                                           ControlToValidate="txtRequestedshipdate"
                                          EnableClientScript="false"
                                          Display="None"
                                          runat="server">*</asp:RequiredFieldValidator>--%>
                                </td>
                            </tr>
                            <%--<tr>
                                 <td align="left" class="copy10grey">Fact Order#:</td>
                                <td></td>
                                <td align="left">
                                    <asp:TextBox ID="txtFactOrderNumber" runat="server" MaxLength="13" CssClass="copy10grey"  onkeypress="return isNumberHiphen(event);"></asp:TextBox>
                                     
                                </td>
                                <td align="right" class="copy10grey">

                                </td>
                                <td>

                                </td>
                                <td>
                                </td>
                            </tr>--%>
                            <tr  height="8">
                                <td  >
                                
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
                            <table cellSpacing="0" cellPadding="0" width="100%" border="0">
                             <tr  height="8">
                             <td>
                                
                             </td>
                             </tr>
                             <tr>
                                <td>
                                    <asp:Panel ID="pnlCustomer" runat="server">
                                    <table width="100%" bgColor="#ffffff" leftmargin="0" rightmargin="0" topmargin="0">
                            
                                    
                                            <tr>
                                                <td class="copy10grey" width="19%">

                                                Company Name:
                                                </td>
                                                <td width="0"><FONT color="#ff0000">*</FONT>&nbsp;</td>
                                        
                                        
                                                <td width="81%" align="left">
                                                <asp:DropDownList ID="ddlCustomer" AutoPostBack="true"  CssClass="copy10grey" 
                                                    runat="server" onselectedindexchanged="ddlCustomer_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                </td>
                                            </tr>
                                    
                                    </table>
                                    </asp:Panel>
                                    <asp:Panel ID="pnlStore" runat="server">
                                    <table width="100%" border="0" bgColor="#ffffff" leftmargin="0" rightmargin="0" topmargin="0">
                                    <tr>
                                    <td class="copy10grey" width="19%">
                                    Destination StoreID:
                                    </td>
                                    <td width="0"><FONT color="#ff0000">*</FONT>&nbsp;</td>
                                    <td width="81%" align="left">
                                        <asp:DropDownList CssClass="copy10grey"  AutoPostBack="true" 
                                         ID="ddlStoreID" runat="server" OnSelectedIndexChanged="ddlStoreID_SelectedIndexChanged"  >
                                        </asp:DropDownList>
                                 
                                         <asp:RequiredFieldValidator  
                                    EnableClientScript="false" Display="None" 
                                     ID="reqStore" ControlToValidate="ddlStoreID" 
                                     InitialValue="" runat="server"  ErrorMessage="Store is required"></asp:RequiredFieldValidator>
           
                                    </td>
                                    <%--<td class="copy10grey" width="15%">
                                     <span id="storeName">
                                     Store Name:</span>
                                    </td>
                                    <td width="29%">
                                        <asp:Label ID="lblStoreName" CssClass="copy10grey" runat="server" ></asp:Label>
                                    </td>--%>
                                    </tr>
                            
                                </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr  height="8">
                                <td>
                                
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
                        <table border="0" width="100%" bgColor="#ffffff" leftmargin="0" rightmargin="0" topmargin="0">
                            <tr  height="8">
                                <td  >
                                
                                </td>
                            </tr>
                            <tr>
                                <td align="left" class="copy10grey" width="19%">Ship Via:</td>
                                <td width="0"><FONT color="#ff0000">*</FONT>&nbsp;</td>
                                <td align="left" width="81%">
                                    <asp:DropDownList ID="dpShipBy" runat="server" CssClass="copy10grey">
                                        <asp:ListItem Value="FDGE">FedEx General</asp:ListItem>
                                        <asp:ListItem Value="FED 1D PM">FedEx One day</asp:ListItem>
                                        <asp:ListItem Value="FDGE">FedEx General</asp:ListItem>
                                        <asp:ListItem Value="FED 1D PM">FedEx One day</asp:ListItem>
                                        <asp:ListItem Value="FED 2D PM">FedEx 2 days</asp:ListItem>
                                        <asp:ListItem Value="FedEx Saturday Deliver">FedEx Saturday Deliver</asp:ListItem>
                                        <asp:ListItem Value="FedEx Saver 3 day">FedEx Saver 3 day</asp:ListItem>
                                        <asp:ListItem Value="UPS Ground">UPS Ground</asp:ListItem>
                                         <asp:ListItem Value="UPS Blue">UPS Blue</asp:ListItem>
                                        <asp:ListItem Value="UPS Red">UPS Red</asp:ListItem>
                                        
                                    </asp:DropDownList>
                                    
                                </td>
                                
                            </tr>
                            <tr  height="8">
                                <td>
                                
                                </td>
                            </tr>
                        </table>   
                         
                     </td>
                 </tr>
                            </table>  
                        </td>
                        <td width="3">
                        &nbsp;
                        </td>
                        <td width="49%">
                            <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="100%" >
                           <tr bordercolor="#839abf">
                            <td>
                                <table width="100%" border="0" bgColor="#ffffff" leftmargin="0" rightmargin="0" topmargin="0" cellpadding="3" cellspacing="3">
                            <tr  height="10">
                                <td>
                                
                                </td>
                            </tr>
                            <tr>
                                <td class="copy10grey" align="left">Contact Name:</td>
                                <td><FONT color="#ff0000">*</FONT></td>
                                <td colspan="4" align="left">
                                    <asp:TextBox ID="txtContactName" runat="server" MaxLength="50" Width="80%" CssClass="copy10grey"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="reqName" CssClass="copy10grey"
                                          ErrorMessage="Contact Name is required"
                                          ControlToValidate="txtContactName"
                                          EnableClientScript="false"
                                          Display="None"
                                          runat="server">*</asp:RequiredFieldValidator>
                                </td>
                                <%--<td class="copy10grey" align="right">Customer Number:</td>
                                <td><FONT color="#ff0000"></FONT></td>
                                <td class="copy10grey">
                                    <asp:TextBox ID="txtCustNumber" runat="server" MaxLength="30" 
                                        CssClass="copy10grey"></asp:TextBox>                                
                                </td>--%> 

                            </tr>
                            
                            <tr valign="top">
                                <td align="left" class="copy10grey">Address:</td>
                                <td><FONT color="#ff0000">*</FONT></td>
                                <td class="copy10grey" colspan="4" align="left">
                                    <asp:TextBox ID="txtAddress1" runat="server" MaxLength="50" CssClass="copy10grey" Width="80%"></asp:TextBox>
                                    
                                     <asp:RequiredFieldValidator ID="reqAddress" CssClass="copy10grey"
                                          ErrorMessage="Address is required"
                                          ControlToValidate="txtAddress1"
                                          EnableClientScript="false"
                                          Display="None"
                                          runat="server">*</asp:RequiredFieldValidator>
                                </td>                        
                            </tr>
                            <tr valign="top">
                                <td align="left" class="copy10grey"></td>
                                <td></td>
                                <td class="copy10grey" colspan="4" align="left">
                                    <asp:TextBox ID="txtAddress2" runat="server" MaxLength="50"  CssClass="copy10grey" Width="80%"></asp:TextBox>
                                    
                                </td>                        
                            </tr>
                            <tr>
                             <td class="copy10grey" align="left">Customer Phone:</td>
                                <td><FONT color="#ff0000">*</FONT></td>
                                <td  >
                                    <asp:TextBox ID="txtPhone" runat="server" MaxLength="12" Width="99%" CssClass="copy10grey"></asp:TextBox>                                
                                     <asp:RequiredFieldValidator ID="rfvPhone" CssClass="copy10grey"
                                          ErrorMessage="Customer Phone is required"
                                          ControlToValidate="txtPhone"
                                          EnableClientScript="false"
                                          Display="None"
                                          runat="server">*</asp:RequiredFieldValidator>
                                </td> 
                                     <td align="left" class="copy10grey">City:</td>
                                <td><FONT color="#ff0000">*</FONT></td>
                                <td class="style2" colspan="4">
                                    <asp:TextBox ID="txtCity" runat="server" MaxLength="30" CssClass="copy10grey"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="reqCity" CssClass="copy10grey"
                                          ErrorMessage="City is required"
                                          ControlToValidate="txtCity"
                                          EnableClientScript="false"
                                          Display="None"
                                          runat="server">*</asp:RequiredFieldValidator>
                                    </td>                        
                            </tr>
                            <tr>
                                
                                <td align="left" class="copy10grey">State:</td> 
                                <td><FONT color="#ff0000">*</FONT></td>                       
                                <td class="copy10grey" align="left">
                                    <asp:dropdownlist id="dpState" tabIndex="6" runat="server" cssclass="copy10grey">
																	<%--<asp:ListItem></asp:ListItem>
																	<asp:ListItem Value="AL">AL</asp:ListItem>
																	<asp:ListItem Value="AK">AK</asp:ListItem>
																	<asp:ListItem Value="AZ">AZ</asp:ListItem>
																	<asp:ListItem Value="AR">AR</asp:ListItem>
																	<asp:ListItem Value="CA" Selected="True">CA</asp:ListItem>
																	<asp:ListItem Value="CO">CO</asp:ListItem>
																	<asp:ListItem Value="CT">CT</asp:ListItem>
																	<asp:ListItem Value="DC">DC</asp:ListItem>
																	<asp:ListItem Value="DE">DE</asp:ListItem>
																	<asp:ListItem Value="FL">FL</asp:ListItem>
																	<asp:ListItem Value="GA">GA</asp:ListItem>
																	<asp:ListItem Value="HI">HI</asp:ListItem>
																	<asp:ListItem Value="ID">ID</asp:ListItem>
																	<asp:ListItem Value="IL">IL</asp:ListItem>
																	<asp:ListItem Value="IN">IN</asp:ListItem>
																	<asp:ListItem Value="IA">IA</asp:ListItem>
																	<asp:ListItem Value="KS">KS</asp:ListItem>
																	<asp:ListItem Value="KY">KY</asp:ListItem>
																	<asp:ListItem Value="LA">LA</asp:ListItem>
																	<asp:ListItem Value="ME">ME</asp:ListItem>
																	<asp:ListItem Value="MD">MD</asp:ListItem>
																	<asp:ListItem Value="MA">MA</asp:ListItem>
																	<asp:ListItem Value="MI">MI</asp:ListItem>
																	<asp:ListItem Value="MN">MN</asp:ListItem>
																	<asp:ListItem Value="MS">MS</asp:ListItem>
																	<asp:ListItem Value="MO">MO</asp:ListItem>
																	<asp:ListItem Value="MT">MT</asp:ListItem>
																	<asp:ListItem Value="NE">NE</asp:ListItem>
																	<asp:ListItem Value="NV">NV</asp:ListItem>
																	<asp:ListItem Value="NH">NH</asp:ListItem>
																	<asp:ListItem Value="NJ">NJ</asp:ListItem>
																	<asp:ListItem Value="NM">NM</asp:ListItem>
																	<asp:ListItem Value="NY">NY</asp:ListItem>
																	<asp:ListItem Value="NC">NC</asp:ListItem>
																	<asp:ListItem Value="ND">ND</asp:ListItem>
																	<asp:ListItem Value="OH">OH</asp:ListItem>
																	<asp:ListItem Value="OK">OK</asp:ListItem>
																	<asp:ListItem Value="OR">OR</asp:ListItem>
																	<asp:ListItem Value="PA">PA</asp:ListItem>
																	<asp:ListItem Value="RI">RI</asp:ListItem>
																	<asp:ListItem Value="SC">SC</asp:ListItem>
																	<asp:ListItem Value="SD">SD</asp:ListItem>
																	<asp:ListItem Value="TN">TN</asp:ListItem>
																	<asp:ListItem Value="TX">TX</asp:ListItem>
																	<asp:ListItem Value="UT">UT</asp:ListItem>
																	<asp:ListItem Value="VT">VT</asp:ListItem>
																	<asp:ListItem Value="VA">VA</asp:ListItem>
																	<asp:ListItem Value="WA">WA</asp:ListItem>
																	<asp:ListItem Value="WV">WV</asp:ListItem>
																	<asp:ListItem Value="WI">WI</asp:ListItem>
																	<asp:ListItem Value="WY">WY</asp:ListItem>--%>
																</asp:dropdownlist>
                                    </td>
                                <td align="left" class="copy10grey">Zip:</td>                        
                                <td><FONT color="#ff0000">*</FONT></td>
                                <td class="copy10grey">
                                    <asp:TextBox ID="txtZip" MaxLength="6" runat="server" CssClass="copy10grey"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="copy10grey"
                                          ErrorMessage="Zip is required"
                                          ControlToValidate="txtZip"
                                          EnableClientScript="false"
                                          Display="None"
                                          runat="server">*</asp:RequiredFieldValidator>
                                </td>
                                    
                            </tr>
                            <tr  height="10">
                                <td>
                                
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
                        <table width="100%" bgColor="#ffffff" leftmargin="0" rightmargin="0" topmargin="0">
                            <tr  height="5">
                                <td  >
                                
                                </td>
                            </tr>
                            <tr>
                                <td class="copy10grey" align="left">
                                    Comments:
                                    <br />
                                    <asp:TextBox ID="txtCommments" CssClass="copy10grey" Width="95%" TextMode="MultiLine" Height="40"  runat="server"
onkeydown="checkTextAreaMaxLength(this,event,'5000');" onkeyup="checkTextAreaMaxLength(this,event,'5000');" onblur="onBlurTextCounter(this,'5000');"
></asp:TextBox>
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
                 
                 
             <%--   </td>
            </tr>
            <tr>
                <td>--%>

                    <table width="100%" cellSpacing="0" cellPadding="0" >
                    
                    <tr>
                            <td>

                    <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="100%">
                        <tr  bordercolor="#839abf">
                            <td>
                             <table width="100%">
                             
                             <tr>
                                <td>

                            <asp:Repeater ID="rptItem" runat="server" OnItemDataBound="rptItem_ItemDataBound" >
                            <HeaderTemplate>
                            <table  cellSpacing="2" cellPadding="2" width="100%" align="center" >
                                 <tr >
                                <td class="buttongrid" width="40">
                                Delete
                                </td>
                                <td class="buttongrid" align="left" width="45%">
                                SKU
                                </td>
                                <td class="buttongrid" align="left" width="55%">
                                Quantity
                                </td>
                                <%--<td class="buttongrid" width="30%">
                                MDN#
                                </td>--%>
                               </tr>
                               </HeaderTemplate>
                                  <ItemTemplate>
                                  <tr>

                                    <td>
                                
                                        <asp:CheckBox ID="chkDel" Visible='<%# Convert.ToString(Eval("ItemCode"))=="" ? false : true %>' CssClass="copy10grey" runat="server" />
                                    </td>
<td align="left">
                                    <asp:HiddenField ID="hdnItemID" Value='<%# Eval("ItemID") %>' runat="server" />
                        
                                       <%--<asp:DropDownList ID="dpItem" CssClass="copy10grey" runat="server"></asp:DropDownList>--%>
                                        <asp:TextBox ID="txtItemCode" AutoPostBack="true" onkeypress="skuvalidation(event);"  OnTextChanged="txtItemCode_SelectedIndexChanged" MaxLength="20" Width="40%" CssClass="copy10grey" Text='<%#Eval("itemCode")%>'  runat="server"></asp:TextBox>

                                        <%--<asp:LinkButton ID="lnkCode"  CausesValidation="false" CssClass="linkgrid" OnClientClick="return StoreItemCode(this);" OnClick="lnkCode_Click"  runat="server">Code</asp:LinkButton>--%>
                                        <asp:ImageButton ID="img" ToolTip="View ItemCode" runat="server" CausesValidation="false"  OnClientClick="return StoreItemCode(this);" OnClick="lnkCode_Click"  ImageUrl="~/images/view.png" />
                                        <asp:Label ID="lblCode" Text='<%# Eval("esn") %>' CssClass="errormessage"  runat="server"></asp:Label>

													
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtQty" MaxLength="5" Width="40%" onkeypress="return isNumberKey(event);" onchange="return isQuantity(this);"  CssClass="copy10grey" Text='<%#Eval("Quantity")%>'  runat="server"></asp:TextBox>
                                    </td>
                                   <%-- <td align="left">
                                        <asp:TextBox ID="txtMDN" MaxLength="30" Width="90%" onkeypress="return isNumberKey(event);" onchange="return isNumberKey(this);"  CssClass="copy10grey" Text='<%#Eval("MdnNumber")%>'  runat="server"></asp:TextBox>
                                    </td>--%>
                                
                                   <%-- <td >
                                        <asp:HiddenField ID="hdnPhCat" Value='<%# Eval("PhoneCategory") %>' runat="server" />
                        
                                        <asp:DropDownList ID="dpCategory" runat="server"  class="copy10grey" >
                                        <asp:ListItem Text="" Value=""></asp:ListItem>
                                        <asp:ListItem Text="Hot" Value="H"></asp:ListItem>
                                        <asp:ListItem Text="Cold" Value="C" Selected="True"></asp:ListItem>
                                        </asp:DropDownList>
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
                            </td>
                        </tr>
                        </table>
                           
                            
                            </td>
                        </tr>
                        <tr>
                        <td>
                        
                        
                    <table width="100%">
                    <tr>
                        <td align="center">
                            <asp:Button ID="btnSave" runat="server" Text="Submit" CssClass="button" OnClick="btnSave_Click" OnClientClick="return LengthValidate" />&nbsp;&nbsp;
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="button" CausesValidation="false" OnClick="btnCancel_Click" />
                        </td>
                    </tr>
                    </table>
                    
                    </td>
                    </tr>
                    <tr>
                    <td>
                    
                        <ajaxToolkit:ModalPopupExtender BackgroundCssClass="modalBackground"
                        CancelControlID="btnClose" runat="server" PopupControlID="pnlItemCode" 
                        ID="ModalPopupExtender3" TargetControlID="lnk3"></ajaxToolkit:ModalPopupExtender>
                        <asp:LinkButton ID="lnk3" runat="server" ></asp:LinkButton>

                        <asp:Panel ID="pnlItemCode" runat="server" CssClass="modalItemCode"  Style="display: none" >
                <div style="overflow:auto; height:400px;  border: 0px solid #839abf" >
      
                
                <table width="99%" align="center" cellpadding="0" cellspacing="0">
                <tr><td    align="right" ><asp:Button ID = "btnClose"  CssClass="button" runat="server" Text="Close" /></td></tr>
                <tr><td class="buttonlabel"  ><strong> Select SKU</strong></td>
                
                </tr>
                 <tr><td></td></tr>
                </table>
            <%--    <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="99%" align="center">
                <tr bordercolor="#839abf">
                <td>
                <table width="100%" align="center" cellpadding="3" cellspacing="3">
                <tr>
                    <td class="copy10grey" align="center" >
                    <RowStyle BackColor="Gainsboro" />
                <AlternatingRowStyle BackColor="white" />
                    --%>
                        
                        <asp:Repeater ID="rptItemCode" runat="server"  >
                        
                            <HeaderTemplate>
                            <table   align="center" width="99%">
                                <%--<tr>
                                <td class="button" >
                                Product Code
                                </td>
                               </tr>--%>
                            </HeaderTemplate>
                            <ItemTemplate>
                               <tr >
                                <td align="left">
                                    <asp:LinkButton ID="lnkCode" Text='<%# Eval("itemCode") %>' CausesValidation="false" CssClass="copy10link" OnClientClick="return SetItemCode(this);"  runat="server"></asp:LinkButton>
                                </td>
                               </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                               </table>  
                            </FooterTemplate>
                        </asp:Repeater> 

                    <%--</td>

                    </tr>
                </table>
                </td>
                </tr>
                </table>--%>
                </div>
                </asp:Panel>
                        

                    </td>
                        </tr>
                    </table>
                    
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnSave" />
                        <asp:PostBackTrigger ControlID="btnCancel" />
                        
                    </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
            
            <tr><td><asp:UpdateProgress ID="UpdateProgress1" runat="server" 
                     DynamicLayout="true">
                        <ProgressTemplate>
                            <img src="/Images/ajax-loaders.gif" /> Loading ...
                        </ProgressTemplate>
                    </asp:UpdateProgress></td></tr>
        	<tr>
				<td>
					
				</td>
			</tr>
        </table>
        <br />
        <br />

        <foot:MenuFooter id="Menuheader2" runat="server"></foot:MenuFooter>
        <script type="text/javascript">
            //var objStore = document.getElementById("storeName");
            //if (objStore != null)
            //    objStore.style.display = "none";

        </script>
    </form>
</body>
</html>
