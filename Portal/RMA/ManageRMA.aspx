<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageRMA.aspx.cs" Inherits="avii.RMA.ManageRMA" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>
<%@ Register TagPrefix="menu" TagName="Menu" Src="~/Controls/Header.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Return Merchandise Authorization</title>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>

    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.4.0/jquery.min.js"></script>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/jquery-ui.min.js"></script>
    <link href="https://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/themes/start/jquery-ui.css"
    rel="stylesheet" type="text/css" />
    <link rel="stylesheet"  type="text/css" href="../fullfillment/calendar/dhtmlgoodies_calendar.css" media="screen" />
	<script type="text/javascript" src="../fullfillment/calendar/dhtmlgoodies_calendar.js"></script>  
    
    <script type="text/javascript">
        function ValidateQty(obj) {
            var enterQty = parseInt(obj.value);
            //alert(enterQty);
            if (enterQty > 0) {
                qty = document.getElementById(obj.id.replace('txtQty', 'hdQty'));
                var skuQty = parseInt(qty.value);

                // alert(skuQty);

                if (enterQty > skuQty) {
                    alert('Quantity cannot be greater than ' + qty.value)
                    obj.value = qty.value;
                    return false;

                }
            }
            else {
                alert('Quantity cannot be 0')
                obj.value = qty.value;
                return false;
            }
            return true;

        }
        function ValidateTriage() {
            var triageNotes = document.getElementById("<%=txtTriageNotes.ClientID %>").value;
            if (triageNotes == '') {
                alert('Triage notes is required.');
                return false;
            }
            var triageStatus = document.getElementById("<%=ddlTriage.ClientID %>");


            if (triageStatus.selectedIndex == 0) {
                alert('Triage status is required.');
                return false;
            }

        }
        function ShowDocUpload() {
            document.getElementById("tblDoc").style.display = 'block';
            return false;
        }
        function HideDocUpload() {
            document.getElementById("tblDoc").style.display = 'none';
            return false;
        }
        
        function IsAddress(e) {
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
        }
        function IsPhone(e) {
            var regex = new RegExp("^[0-9\-\(\)\s]+$");
            var str = String.fromCharCode(!e.charCode ? e.which : e.charCode);
            //alert(str);
            if (regex.test(str)) {
                return true;
            }
            e.preventDefault();
            return false;
        }
        function ValidateEmail(obj) {
            var EmailAddresses = obj.value;

            var emails = EmailAddresses.split(',');
            var EmaiAddress;
            for (var i = 0; i < emails.length; i++) {
                EmaiAddress = emails[i];
                var RegExEmail = /^(?:\w+\.?)*\w+@(?:\w+\.)+\w+$/;
                if (obj.value != '') {
                    if (!RegExEmail.test(EmaiAddress)) {
                        obj.focus();
                        alert("Invalid E-mail");
                        return false;
                    }
                }
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
        function isNumberHiphen(evt) {

            var charCodes = evt.keyCode ? evt.keyCode : evt.which ? evt.which : evt.charCode;
            if (((((charCodes > 31 && (charCodes < 48 || charCodes > 57) && charCodes != 45) && charCodes != 46) && charCodes != 95) && !(charCodes > 96 && charCodes < 123)) && !(charCodes > 64 && charCodes < 91)) {
                // alert(charCodes);

                charCodes = 0;
                return false;
            }

            return true;
        }
        function alphanumeric(e) {

            var regex = new RegExp("^[a-zA-Z0-9]+$");
            var str = String.fromCharCode(!e.charCode ? e.which : e.charCode);


            //alert(regex.test(str));
            if (regex.test(str)) {
                return true;
            }
            else {
                e.preventDefault();
                return false;
            }
        }
        function alphabetOnly(e) {

            var regex = new RegExp("^[a-zA-Z]+$");
            var str = String.fromCharCode(!e.charCode ? e.which : e.charCode);


            //alert(regex.test(str));
            if (regex.test(str)) {
                return true;
            }
            else {
                e.preventDefault();
                return false;
            }
        }
        function fileValidation() {
            var fileInput = document.getElementById('fu');
            var filePath = fileInput.value;
            var allowedExtensions = /(\.csv)$/i;
            if (!allowedExtensions.exec(filePath)) {
                alert('Please upload file having extensions .csv/ only.');
                fileInput.value = '';
                return false;
            } else {

                //Image preview
                //if (fileInput.files && fileInput.files[0]) {
                //    var reader = new FileReader();
                //    reader.onload = function (e) {
                //        document.getElementById('imagePreview').innerHTML = '<img src="' + e.target.result + '"/>';
                //    };
                //    reader.readAsDataURL(fileInput.files[0]);
                //}
            }
            return true;
        }

        function ValidateFile() {
            var flag = false;
            var fineNamec = document.getElementById("<%=fuDoc.ClientID %>");
            var FileUploadPath = fineNamec.value;
            var Extension = FileUploadPath.substring(FileUploadPath.lastIndexOf('.') + 1).toLowerCase();

            if (Extension == "doc" || Extension == "docx" || Extension == "xls" || Extension == "xlsx" || Extension == "pdf" || Extension == "jpeg" || Extension == "jpg") {
                //alert(FileUploadPath + '  is a valid file');
            }
            else {
                if (FileUploadPath != '') {
                    flag = true;
                    alert(FileUploadPath + '  is not a valid file');
                }
            }
            //alert('xcxc');

            var fineNameA1 = document.getElementById("<%=fupRmaDocA1.ClientID %>");
            var FileUploadPath1 = fineNameA1.value;
            var Extension1 = FileUploadPath1.substring(FileUploadPath1.lastIndexOf('.') + 1).toLowerCase();

            if (Extension1 == "doc" || Extension1 == "docx" || Extension1 == "xls" || Extension1 == "xlsx" || Extension1 == "pdf" || Extension1 == "jpeg" || Extension1 == "jpg") {

            }
            else {

                if (FileUploadPath1 != '') {
                    flag = true;
                    alert(FileUploadPath1 + '  is not a valid file');
                }
            }
            var fineNameA2 = document.getElementById("<%=fupRmaDocA2.ClientID %>");
            var FileUploadPath2 = fineNameA2.value;
            var Extension2 = FileUploadPath2.substring(FileUploadPath2.lastIndexOf('.') + 1).toLowerCase();

            if (Extension2 == "doc" || Extension2 == "docx" || Extension2 == "xls" || Extension2 == "xlsx" || Extension2 == "pdf" || Extension2 == "jpeg" || Extension2 == "jpg") {

            }
            else {

                if (FileUploadPath2 != '') {
                    flag = true;
                    alert(FileUploadPath2 + '  is not a valid file');
                }
            }
            var fineNameA3 = document.getElementById("<%=fupRmaDocA3.ClientID %>");
            var FileUploadPath3 = fineNameA3.value;
            var Extension3 = FileUploadPath3.substring(FileUploadPath3.lastIndexOf('.') + 1).toLowerCase();

            if (Extension3 == "doc" || Extension3 == "docx" || Extension3 == "xls" || Extension3 == "xlsx" || Extension3 == "pdf" || Extension3 == "jpeg" || Extension3 == "jpg") {

            }
            else {

                if (FileUploadPath3 != '') {
                    flag = true;
                    alert(FileUploadPath3 + '  is not a valid file');
                }
            }
            var fineNameA4 = document.getElementById("<%=fupRmaDocA4.ClientID %>");
            var FileUploadPath4 = fineNameA4.value;
            var Extension4 = FileUploadPath4.substring(FileUploadPath4.lastIndexOf('.') + 1).toLowerCase();

            if (Extension4 == "doc" || Extension4 == "docx" || Extension4 == "xls" || Extension4 == "xlsx" || Extension4 == "pdf" || Extension4 == "jpeg" || Extension4 == "jpg") {

            }
            else {
                if (FileUploadPath4 != '') {
                    flag = true;
                    alert(FileUploadPath4 + '  is not a valid file');

                }
            }

            if (flag == true)
                return false;
            else
                return true;


        }
    </script>

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

    <table cellspacing="0" cellpadding="0" border="0" align="center" style="text-align:left" width="100%">
        <tr class="buttonlabel" align="left">
            <td>&nbsp;Return Merchandise Authorization (RMA)</td>
        </tr>
    </table>
<%--    <asp:UpdatePanel ID="UpdatePanel1"  runat="server" UpdateMode="Conditional" >
        <ContentTemplate>--%>
        <div style="min-height:450px">
        <table cellSpacing="0" cellPadding="0" align="center" width="100%" border="0" >
    		<tr>                    
                <td colspan="2">
                    <asp:Label ID="lblMsg" runat="server" CssClass="errormessage"></asp:Label>
                </td>
            </tr>
            <tr id="trSearch" runat="server">
                <td align="center">
                <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="95%" >
                    <tr>
                    <td>
			            <table id="Table2" cellSpacing="5" cellPadding="5" width="100%" border="0">
                        <tr>
                            <td colspan="5"> 
                                <asp:Panel ID="pnlSearch" runat="server" DefaultButton="btnSearch">
                                <table cellSpacing="5" cellPadding="5" width="100%" border="0" >
                                <tr>
                                    <td  class="copy10grey" align="right" width="12%">
                                            Customer: &nbsp;</td>
                                    <td align="left"  width="18%">
                                        <asp:DropDownList ID="dpCompany" CssClass="copy10grey" runat="server" Width="90%">
								        </asp:DropDownList>
                                        <%--<asp:Label ID="lblCompany" runat="server" CssClass="copy10grey"></asp:Label>--%>
                                    </td>
                                    <td  class="copy10grey" align="right"  width="12%">
                                            Fulfillment#: &nbsp;</td>
                                    <td align="left"  width="18%">
                                        <asp:TextBox    ID="txtPO" MaxLength="20" onkeypress="return isNumberHiphen(event);" CssClass="copy10grey" runat="server" Width="90%"></asp:TextBox>
									
                                    </td>
                                    <td  class="copy10grey" align="right"  width="12%">
                                             Tracking#: &nbsp;</td>
                                    <td align="left"  width="18%">
                                          <asp:TextBox    ID="txtTrackingNo" MaxLength="25" onkeypress="return alphanumeric(event);"   CssClass="copy10grey" runat="server" Width="90%"></asp:TextBox>
									
                                    </td>
                                    <td  align="center"  width="40%">
                                            <asp:Button ID="btnSearch" runat="server" CssClass="button"  Text="Search" OnClick="btnSearch_Click" />
                                    </td>
                               </tr>
                               </table>
                               </asp:Panel>
                            </td>
                        </tr>
                                                     
                       </table>                            
                    </td>
                    </tr>                           
                 </table>
                </td>
            </tr>
            <tr id="trExistingRma" runat="server" visible="false" >
                <td align="center">
                    <br />
                <table cellspacing="0" cellpadding="0" border="0" align="center" style="text-align:left" width="95%">
                    <tr class="buttonlabel" align="left">
                    <td>&nbsp;RMA Issued</td></tr>
                </table>
                <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="95%">
                    <tr>
                    <td>
			            <table  cellSpacing="0" cellPadding="0" width="100%" border="0">
                        <tr>
                            <td colspan="5"> 
                                <table cellSpacing="0" cellPadding="0" width="100%" border="0">
                                <tr>
                                    <td>
                                        <asp:Repeater ID="rptExistingRma" runat="server">
                                            <HeaderTemplate>
                                            <table width="100%" align="center">
                                                <tr >
                                                    <td class="buttongrid" width="2%">
                                                        &nbsp;S.No.
                                                    </td>
                                                    <td class="buttongrid" width="18%">
                                                        &nbsp;SKU
                                                    </td>
                                                    <td class="buttongrid" width="18%">
                                                        &nbsp;Name
                                                    </td>
                                                    <td class="buttongrid"  width="20%">
                                                        &nbsp;Quantity
                                                    </td>
                                                    <td class="buttongrid"  width="20%">
                                                        &nbsp;Rma Number
                                                    </td>
                                                </tr>
                            
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                            <tr class="<%# Container.ItemIndex % 2 == 0 ? "alternaterow" : "" %>">
                                                
                                                <td class="copy10grey">
                                                        &nbsp;<%# Container.ItemIndex + 1 %></td>
                                
                                                <td class="copy10grey">
                                                 &nbsp;<%# Eval("SKU")%></td>
                                                <td class="copy10grey">
                                                 &nbsp;<%# Eval("SKU")%></td>
                                                <td class="copy10grey">
                                                 &nbsp;<%# Eval("Quantity")%></td>
                                                <td class="copy10grey">
                                                 &nbsp;<%# Eval("RmaNumber")%></td>                                                
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
                 </table>
                </td>
            </tr>
            <tr id="trRMA" runat="server" visible="false" >
                <td align="center">
                    <br />
                <table cellspacing="0" cellpadding="0" border="0" align="center" style="text-align:left" width="95%">
                    <tr class="buttonlabel" align="left">
                    <td id="tdRMA" runat="server">&nbsp;Create RMA</td></tr>
                </table>
                
                <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="95%">
                    <tr>
                    <td>
			            <table  cellSpacing="5" cellPadding="5" width="100%" border="0">
                        <tr>
                            <td colspan="5"> 
                                <table cellSpacing="5" cellPadding="5" width="100%" border="0">
                                <tr>
                                    <td class="copy10grey">RMA#:</td>
                                    <td class="copy10grey"><asp:Label ID="lblRmaNumber" runat="server" 
                                                    CssClass="copyblue11b"  
                                                     /></td>
                                    <td class="copy10grey"><strong>RMA Date:</strong></td>
                                    <td class="copy10grey">
                                          <asp:Panel ID="rmadtpanel" runat="server">
                                            &nbsp;<asp:TextBox ID="txtRMADate" runat="server"  onfocus="set_focus1();" onkeypress="return doReadonly(event);"  
                                                CssClass="copy10grey" MaxLength="15" />
                                             <img id="img1" alt="" onclick="document.getElementById('<%=txtRMADate.ClientID%>').value = ''; displayCalendar(document.getElementById('<%=txtRMADate.ClientID%>'),'mm/dd/yyyy',this,true);"
                                                        src="../../fullfillment/calendar/sscalendar.jpg" />
                                            </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="copy10grey">Customer RMA#:</td>
                                    <td class="copy10grey">
                                        <asp:TextBox ID="txtRMACustomerNumber" runat="server" onkeypress="return alphanumeric(event);"  
                                                CssClass="copy10grey" MaxLength="50" Width="90%" />
                                    </td>
                                    <td class="copy10grey">Store ID:</td>
                                    <td class="copy10grey">
                                        <asp:DropDownList CssClass="copy10grey"   Visible="true" 
                                         ID="ddlStoreID" runat="server"   >
                                        </asp:DropDownList>
                                 
                                    </td>
                                </tr>
                                <tr>
                                    <td class="copy10grey"><strong>Customer Name:</strong></td>
                                    <td class="copy10grey">
                                        <asp:TextBox ID="txtCustName" runat="server" onkeypress="return alphabetOnly(event);"  
                                                CssClass="copy10grey" MaxLength="50" Width="90%" />
                                    </td>
                                    <td class="copy10grey"><strong>Email:</strong></td>
                                    <td class="copy10grey">
                                        <asp:TextBox ID="txtEmail" onchange = "return ValidateEmail(this);" Width="90%" runat="server" CssClass="copy10grey" MaxLength="50" ViewStateMode="Disabled"/>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="copy10grey"><strong>Address:</strong></td>
                                    <td class="copy10grey">
                                        <asp:TextBox ID="txtAddress" onkeypress="return IsAddress(event);" runat="server" CssClass="copy10grey" Width="90%" MaxLength="200"/>
                                    </td>
                                    <td class="copy10grey"></td>
                                    <td class="copy10grey"></td>
                                </tr>
                                <tr>
                                    <td class="copy10grey"><strong>City:</strong></td>
                                    <td class="copy10grey">
                                        <asp:TextBox ID="txtCity" runat="server" onkeypress="return alphabetOnly(event);" Width="80%"  CssClass="copy10grey" MaxLength="50"/>
                                    </td>
                                    <td class="copy10grey"><strong>State:</strong></td>
                                    <td class="copy10grey">
                                        <asp:dropdownlist id="dpState" tabIndex="6" runat="server" cssclass="copy10grey">
                                        </asp:dropdownlist>
                                
                                    </td>
                                </tr>
                                <tr>
                                    <td class="copy10grey"><strong>Zip:</strong></td>
                                    <td class="copy10grey">
                                        <asp:TextBox ID="txtZip" runat="server" Width="37%" onkeypress="return isNumberKey(event);" 
                                      CssClass="copy10grey" MaxLength="5"/>
                                    </td>
                                    <td class="copy10grey"><strong>Contact Number:</strong></td>
                                    <td class="copy10grey">
                                        <asp:TextBox ID="txtPhone" onkeypress="return IsPhone(event);"  Width="90%" runat="server" CssClass="copy10grey"  ViewStateMode="Disabled"
                                    MaxLength="12" />
                                    </td>
                                </tr>
                                <tr id="trRmaStatus" runat="server" >
                                    <td class="copy10grey">Triage Status:</td>
                                    <td class="copy10grey">
                                        <asp:DropDownList ID="ddlTriageStatus"  runat="server" class="copy10grey" Width="166px" >
                                            <asp:ListItem Text="" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Pending" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="In-Process" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="Complete" Value="3"></asp:ListItem>
                                            <asp:ListItem Text="Not Required" Value="4"></asp:ListItem>                                    
                                        </asp:DropDownList>
                                    </td>
                                    <td class="copy10grey" id="tdReceiveRma" runat="server">Receive Status:</td>
                                    <td class="copy10grey">
                                        <asp:DropDownList ID="ddlReceive"  runat="server" class="copy10grey" Width="166px" >
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr id="trStatus" runat="server">
                                    <td class="copy10grey">
                                       RMA Status:
                                    </td>
                                    <td>
                                        <asp:Label ID="lblStatus" runat="server" Visible="false" Text="Pending"></asp:Label>
                                            <asp:DropDownList ID="ddlStatus"  runat="server" class="copy10grey" 
                                                Width="166px" ></asp:DropDownList>
                                    </td>
                                    <td class="copy10grey">
                                   <strong>    <asp:Label ID="lblCreatedBy" runat="server"  Text="Created By:"></asp:Label></strong>
                                    </td>
                                    
                                    <td>
                                         <asp:DropDownList ID="ddlUser" CssClass="copy10grey" runat="server" Width="60%">
								        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="copy10grey">Document:</strong></td>
                                    <td class="copy10grey">
                                       
                                        <asp:ImageButton ID="imgUpload" runat="server" ToolTip="Upload RMA Document" OnClientClick="return ShowDocUpload();" AlternateText="Upload RMA Document"  ImageUrl="~/images/upload.png"
                                             CausesValidation="false" /> 
                                           <asp:Label ID="lblRmaDocs" runat="server" CssClass="copy10grey" ></asp:Label>
                                
                                    </td>
                                    <td class="copy10grey"></td>
                                    <td class="copy10grey">
                                       
                                    </td><td></td>
                                </tr>
                                <tr valign="top">
                                    <td class="copy10grey">RMA Comments:</td>
                                    <td class="copy10grey">
                                        <asp:TextBox runat="server" ID="txtComments" TextMode="MultiLine" ViewStateMode="Disabled"
                                    Rows="5" Columns="80" CssClass="copy10grey" 
                                     Width="100%" />
                                    </td>
                                    <td class="copy10grey">Internal Comments:</td>
                                    <td class="copy10grey">
                                        <asp:TextBox runat="server" ID="txtLanComments" TextMode="MultiLine" ViewStateMode="Disabled"
                                        Rows="5" Columns="80" CssClass="copy10grey" 
                                         Width="99%" />
                                    </td>
                                </tr>
                                

                                
                               </table>
                               
                            </td>
                        </tr>
                        </table>                            
                    </td>
                    </tr>                           
                 </table>

                <table  cellspacing="0" cellpadding="0" border="0" align="center" style="text-align:left" width="100%">
                <tr>
                    <td>
                    
                    <table id="tblTriage" runat="server" visible="false" cellspacing="0" cellpadding="0" border="0" align="center"  width="95%">
                            <tr>
                                <td>
                                    <br />
                                    <table  cellSpacing="1" cellPadding="1" width="100%" align="center">
                                <tr>
			                        <td  class="buttonlabel" align="left">&nbsp;&nbsp;Add Triage
			                        </td>
                                </tr>
                                </table>
                            
                            <table  bordercolor="#839abf" border="1" cellspacing="0" cellpadding="0" width="99%">
                            <tr bordercolor="#839abf">
                                <td>
                              
                            <table class="box" width="100%" align="center" cellspacing="5" cellpadding="5"> 
                             <tr valign="top">
                                <td class="copy10grey" style="width:100px" align="right">
                                    SKU: &nbsp;
                                </td>
                                <td  class="copy10grey" align="left">
                                    <table width="100%" align="center" cellspacing="0" cellpadding="0"> 
                                    <tr>
                                        <td class="copy10grey" align="left" width="30%">
                                             <asp:Label ID="lblSKU" CssClass="copy10grey" runat="server"></asp:Label>                                    
                                        </td>
                                        <td class="copy10grey" align="right" width="10%">
                                            ESN: &nbsp;
                                        </td>
                                        <td class="copy10grey" align="left" width="30%">
                                             <asp:Label ID="lblESN" CssClass="copy10grey" runat="server"></asp:Label>                                    
                                        </td>
                                    </tr>
                                    </table>
                                   
                                </td>
                            </tr>
                            <tr valign="top">
                                <td class="copy10grey" style="width:100px" align="right">
                                    Triage Notes: &nbsp;
                                </td>
                                <td  class="copy10grey" align="left">
                                    <asp:TextBox ID="txtTriageNotes" TextMode="MultiLine" Height="70" Width="90%" CssClass="copy10grey" runat="server"></asp:TextBox>                                    
                                </td>
                            </tr>
                            <tr>
                                <td  class="copy10grey" align="right">
                                    Triage Status: &nbsp;
                                </td>
                                <td class="copy10grey" align="left">
                                    <asp:DropDownList ID="ddlTriage" CssClass="copy10grey" runat="server" Width="90%">
                                            <asp:ListItem Text="Select Triage Status" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Pending" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="In-Process" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="Completed" Value="3"></asp:ListItem>
                                            <asp:ListItem Text="Not Required" Value="4"></asp:ListItem>
                                    
                                    </asp:DropDownList>
                                </td>
                                
                            </tr>
                            <tr>
                                <td colspan="2" class="copy10grey" align="center">
                                    <hr />
                                </td>
                            </tr>
                            
                            <tr>
                                <td colspan="2" align="center" class="copy10grey">
                                    <asp:Button ID="btnTriage" runat="server" Text=" Submit " CssClass="button"   OnClick="btnTriage_Click" OnClientClick="return ValidateTriage();"  />
                                    &nbsp;
                                    <asp:Button ID="btnTriageCancel" runat="server" Text="Cancel" CssClass="button"   
                                    OnClick="btnTriageCancel_Click"/>
                                    <%--<asp:Button ID="btnCommCancel" runat="server" Text="Cancel" CssClass="buybt"  OnClientClick="return closeCommunicationDialog();" />--%>
                                </td>
                            </tr>
                            </table>
                            
                            </td>
                            </tr>
                            </table>
                            
                                </td>
                            </tr>
                            </table>
                    <table id="tblDoc" style="display:none;text-align:left;" cellspacing="0" cellpadding="0" border="0" align="center"  width="95%">
                    <tr>
                        <td>
                            <br />
                            <table  cellSpacing="1" cellPadding="1" width="100%" align="center">
                                <tr>
			                        <td  class="buttonlabel" align="left">&nbsp;&nbsp;RMA document
			                        </td>
                                </tr>
                                </table>
                            
                            <table bordercolor="#839abf" border="1" width="98%" align="center" cellpadding="0" cellspacing="0">
                                <tr ><td>

                                <table border="0" width="100%" class="box" align="center" cellpadding="5" cellspacing="5">
                    
                                    <tr>
                                        <td class="copy10grey" width="120">
                                            File:
                                        </td>
                                        <td>
                                            <asp:FileUpload ID="fuDoc" runat="server" />           

                                        </td>
                                    </tr>
                                    </table>
                                    </td>
                                </tr>
                                </table>
        
                             <table  cellSpacing="1" cellPadding="1" width="100%" align="center">
                                <tr>
			                        <td  class="buttonlabel"  align="left">&nbsp;&nbsp;Administration RMA document
			                        </td>                                   
                                </tr>
                                </table>
                            <table bordercolor="#839abf" border="1" width="98%" align="center" cellpadding="0" cellspacing="0">
                            <tr><td>
                                    <table border="0" width="100%" class="box" align="center" cellpadding="5" cellspacing="5">
                                    <tr >
                                        <td class="copy10grey" width="120">
                                            Admin File 1:
                                        </td>
                                        <td>
                                            <asp:FileUpload ID="fupRmaDocA1" runat="server" />  
                                        </td>
                                    </tr>
                                    <tr >
                                        <td class="copy10grey">
                                            Admin File 2:
                                        </td>
                                        <td>
                                            <asp:FileUpload ID="fupRmaDocA2" runat="server" />       
                                        </td>
                                    </tr>                                    
                                    <tr>
                                        <td class="copy10grey">
                                            Admin File 3:
                                        </td>                                        
                                        <td>
                                            <asp:FileUpload ID="fupRmaDocA3" runat="server" />  
                                        </td>
                                    </tr>                                    
                                    <tr >
                                        <td class="copy10grey">
                                            Admin File 4:
                                        </td>                                        
                                        <td>
                                            <asp:FileUpload ID="fupRmaDocA4" runat="server" /> 
                                        </td>
                                    </tr>
                                    </table>
                              <%--  <asp:Repeater ID="rptDoc" runat="server" >
                                    <HeaderTemplate>
                                    <table border="0" width="100%" class="box" align="center" cellpadding="5" cellspacing="5">
                                    </HeaderTemplate>
                                    <ItemTemplate>    
                                        <tr valign="bottom"  >
                                            <td  class="copy10grey" align="right" width="20%">
                                                Admin file <%# Container.ItemIndex + 1 %>: &nbsp;
                                            </td>
                                            <td align="left"  width="30%">
                                                <asp:FileUpload ID="fuAdmin" runat="server" CssClass="txfield1" Width="80%" />
                                            </td>
                                        </tr>                                    
                                    </ItemTemplate>
                                    <FooterTemplate>
                                    </table>
                                    </FooterTemplate>
                                </asp:Repeater>--%>
                                </td>
                            </tr>
                            </table>
                            <table border="0" width="100%"  align="center" cellpadding="5" cellspacing="5">
                            <tr>
                                <td align="center">
                                        
                                    <asp:Button ID="btnRmaDocUpload" runat="server" Text="Upload" CssClass="button" OnClick="btnRmaDocUpload_Click"   
                                        OnClientClick="return ValidateFile();"  />
                                    <asp:Button ID="btnUloadCancel" runat="server" OnClientClick="return HideDocUpload();" Text="Cancel" CssClass="button"/>
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
                    <table cellspacing="0" cellpadding="0" border="0" align="center" style="text-align:left" width="95%">
                    <tr class="buttonlabel" align="left">
                    <td>&nbsp;Return Merchandise Authorization(RMA) Line Items</td></tr>
                    </table>
               
                    <asp:Panel ID="pnlUpload" runat="server">
                        <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="95%">
                            <tr>
                            <td>
			                    <table  cellSpacing="5" cellPadding="5" width="100%" border="0">
                                <tr>
                                    <td width="50%" class="copy10grey">
                                        <asp:FileUpload ID="fu" CssClass="copy10grey" runat="server" onchange="return fileValidation();" /> File Format: <b>ESN</b>
                                    </td>
                                    <td align="left">
                                        <asp:Button ID="btnUpload" CssClass="button"  runat="server" Text="Upload" OnClick="btnUpload_Click" />
                                    </td>
                                </tr>
                                </table>
                             </td>
                            </tr>                           
                         </table>

                    </asp:Panel>
                    <asp:Panel ID="pnlESN" runat="server">
                     
                    <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="95%">
                    <tr>
                    <td>
			        <table  cellSpacing="0" cellPadding="0" width="100%" border="0">
                        <tr>
                            <td>
                                <asp:Repeater ID="rptRma" runat="server" OnItemDataBound="rptRma_ItemDataBound">
                                    <HeaderTemplate>
                                    <table width="100%" align="center">
                                        <tr>
                                            <td class="buttongrid" width="2%">
                                                &nbsp;S.No.
                                            </td>
                                            <td class="buttongrid" width="15%">
                                                &nbsp;SKU
                                            </td>
                                            <td class="buttongrid" width="15%">
                                                &nbsp;Product Name
                                            </td>
                                            <td class="buttongrid" width="10%">
                                                &nbsp;ESN
                                            </td>                                                    
                                            <td class="buttongrid"  width="5%">
                                                &nbsp;Quantity
                                            </td>
                                            <td class="buttongrid"  width="10%">
                                                &nbsp;Warranty 
                                            </td>
                                            <td class="buttongrid"  width="10%">
                                                &nbsp;Reason
                                            </td>
                                            <td class="buttongrid"  width="20%">
                                                &nbsp;Notes
                                            </td>
                                            <td  class="buttongrid" runat="server" visible='<%# RmaGUID==0 ?false:true %>' Width="5%"  id="tdDISPOSITION">
                                                &nbsp;Disposition
                                            </td>
                                            
                                            <td class="buttongrid"  width="7%" id="st" runat="server" visible='<%# RmaGUID==0 ?false:true %>'>
                                                &nbsp;Status
                                            </td>
                                            <td class="buttongrid" width="5%" id="l" runat="server" visible='<%# RmaGUID==0 ?false:true %>'>

                                            </td>
                                        </tr>                            
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                    <tr class="<%# Container.ItemIndex % 2 == 0 ? "alternaterow" : "" %>">
                                                
                                        <td class="copy10grey">
                                            <asp:CheckBox ID="chkItem" Checked='<%# Convert.ToInt32(Eval("RmaDetGUID"))==0 ? false: true %>' Enabled='<%# Convert.ToInt32(Eval("Quantity")) == 0 ? false : true %>' runat="server" />
                                                &nbsp;<%# Container.ItemIndex + 1 %>

                                                <asp:HiddenField ID="hdRmaDetGUID" Value='<%# Eval("RmaDetGUID") %>' runat="server" />                                                  
                                                    
                                                     
                                                <asp:HiddenField ID="hdPODID" Value='<%# Eval("POD_ID") %>' runat="server" />
                                                                       
                                        </td>                                                    
                                        <td class="copy10grey">
                                            &nbsp;<%# Eval("SKU")%> 
                                        </td>
                                        <td class="copy10grey">
                                            &nbsp;<%# Eval("ProductName")%> </td>
                                        <td class="copy10grey">
                                            
                                            <asp:Label ID="lblESN" runat="server" CssClass="copy10grey"  Text='<%# Eval("ESN")%>'></asp:Label>
                                            <span class="errormessage"><%# Eval("ErrorMessage")  %></span>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtQty" onchange="return ValidateQty(this);" onkeypress="return isNumberKey(event);" MaxLength="4" 
                                                Enabled='<%# Convert.ToString(Eval("ESN")) != "" ? false : true %>' CssClass="copy10grey" Width="70%" runat="server" 
                                                Text='<%# Eval("Quantity")%>'></asp:TextBox>
                                            <asp:HiddenField ID="hdQty" Value='<%# Eval("Quantity") %>' runat="server" />
                                            <asp:HiddenField ID="hdItemCompanyGUID" Value='<%# Eval("ItemCompanyGUID") %>' runat="server" />                                                    
                                                   
                                        </td>
                                        <td >
                                            <asp:DropDownList ID="dpWarranty"  runat="server" Width="100%"  CssClass ="copy10grey"> 
                                                <asp:ListItem  Value="0" >------</asp:ListItem>
                                                <asp:ListItem Value="1" >Warranty</asp:ListItem>
                                                <asp:ListItem  Value="2">Out of Warranty</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:HiddenField id="hdnWarranty" runat="server" Value='<%# Eval("Warranty") %>'/>                                                             

                                        </td>
                                        <td>
                                                <asp:DropDownList ID="ddReason"  runat="server" Width="100%" CssClass ="copy10grey"> 
                                                    <asp:ListItem  Value="0" >------</asp:ListItem>
                                                    <asp:ListItem  Value="1" >DOA</asp:ListItem>
                                                    <asp:ListItem  Value="2">AudioIssues</asp:ListItem>
                                                    <asp:ListItem  Value="3">ScreenIssues</asp:ListItem>
                                                    <asp:ListItem  Value="4">PowerIssues</asp:ListItem>
                                                    <asp:ListItem  Value="5">Others</asp:ListItem>
                                                    <asp:ListItem  Value="6">MissingParts</asp:ListItem>
                                                    <asp:ListItem  Value="7">ReturnToStock</asp:ListItem>
                                                    <asp:ListItem  Value="8">BuyerRemorse</asp:ListItem>
                                                    <asp:ListItem  Value="9">PhysicalAbuse</asp:ListItem>
                                                    <asp:ListItem  Value="10">LiquidDamage</asp:ListItem>
                                                    <asp:ListItem  Value="11">DropCalls</asp:ListItem>
                                                    <asp:ListItem  Value="12">Software</asp:ListItem>
                                                </asp:DropDownList>

                                                <asp:HiddenField id="hdnReason" runat="server" Value='<%# Eval("Reason") %>'/>
                                                             
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtNotes"  Text='<%# Eval("Notes") %>'  
                                                Width="99%" ViewStateMode="Disabled"
                                                CssClass="copy10grey" runat="server" MaxLength="1000" TextMode="MultiLine"></asp:TextBox>
                                        </td>
                                        <td id="tddp" runat="server" visible='<%# Convert.ToInt32(Eval("RmaDetGUID"))==0 ? false: true %>'>
                                                <asp:DropDownList ID="dpDisposition"  runat="server" Width="100%"  CssClass ="copy10grey"> 
                                                <asp:ListItem  Value="0" >------</asp:ListItem>
                                                <asp:ListItem Value="1" >Credit</asp:ListItem>
                                                <asp:ListItem  Value="2">Replaced</asp:ListItem>
                                                <asp:ListItem  Value="3">Repair</asp:ListItem>
                                                <asp:ListItem  Value="4">Discarded</asp:ListItem>
                                                </asp:DropDownList>
                                                    <asp:HiddenField id="hdnDisposition" runat="server" Value='<%# Eval("DispositionID") %>'/>
                                        </td>
                                        
                                        <td id="tdst" runat="server"  visible='<%# Convert.ToInt32(Eval("RmaDetGUID"))==0 ? false: true %>'>
                                            <asp:DropDownList ID="ddlStatus"  runat="server" class="copy10grey" Width="95%" ></asp:DropDownList>
                                                            <asp:HiddenField id="hdnStatus" runat="server" Value='<%# Eval("StatusID") %>'/>
                                        </td>
                                        <td id="last" runat="server"  visible='<%# Convert.ToInt32(Eval("RmaDetGUID"))==0 ? false: true %>'>                                              
                                            <asp:Button ID="btntriage" CssClass="buybt" runat="server" Visible='<%# Convert.ToInt32(Eval("rmaDetGUID"))==0 ? false : true %>' 
                                                Text="Triage" OnClick="btntriage_Click" />
                                            <asp:HiddenField ID="hdTriageStatusID" Value='<%# Eval("TriageStatusID") %>' runat="server" />
                                            <asp:HiddenField ID="hdTriageNotes" Value='<%# Eval("TriageNotes") %>' runat="server" />
                                                    
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
                     <table cellspacing="0" cellpadding="0" border="0" align="center" style="text-align:left" width="95%">
                         <tr><td><hr /></td></tr>
                    <tr >
                    <td align="center">&nbsp;
                       <asp:Button ID="btnSubmit"  CssClass="buybt" runat="server" Text="Submit RMA"  
                        OnClientClick="return IsValidate(1);" OnClick="btnSubmit_Click" />&nbsp;       
                       <asp:Button ID="btnCancel" CssClass="buybt" runat="server" Text="Cancel" 
                        CausesValidation="false" OnClick="btnCancel_Click" /> 
                                        

                    </td></tr>
                </table>
                </asp:Panel>
                </td>
            </tr>
        </table>
        </div>
       <%-- </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSearch" /> 
            <asp:PostBackTrigger ControlID="btnUpload" /> 
            <asp:PostBackTrigger ControlID="imgUpload" /> 
            <asp:PostBackTrigger ControlID="btnRmaDocUpload" />                 
        </Triggers>
    </asp:UpdatePanel>
       --%> 

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
