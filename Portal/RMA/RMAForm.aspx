<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RMAForm.aspx.cs" Inherits="avii.Admin.RMAForm" ValidateRequest="false"  %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="~/Controls/Header.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>.:: Return Merchandise Authorization (RMA) ::.</title>
    <link href="/aerostyle.css" type="text/css" rel="stylesheet"/>

    <link rel="stylesheet"  type="text/css" href="../fullfillment/calendar/dhtmlgoodies_calendar.css" media="screen"></link>
	<script type="text/javascript" src="../fullfillment/calendar/dhtmlgoodies_calendar.js"></script>  
	
    <link rel="icon" href="../common/favicon.ico" type="image/x-icon" />
	<link rel="shortcut icon" href="../common/favicon.ico" type="image/x-icon" />
	
	
    <script type="text/javascript" language="javascript">
        
        
        

        function getQuerystring(key, default_) {
            if (default_ == null) default_ = "";
            key = key.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
            var regex = new RegExp("[\\?&]" + key + "=([^&#]*)");
            var qs = regex.exec(window.location.href);
            if (qs == null)
                return default_;
            else
                return qs[1];
        }

        function textchanged(obj) {

            var esn = obj.value;
            var esn_detail = RMAForm.getesnDetails(esn);
            var esndetail = esn_detail.value;
            var esnerr = esndetail.split(',');
            var fundqnr = document.getElementById(uniqueID.id.replace('txtAnswer', 'hdnfundquestionnaireID'));

        }



        function isMaxLength(obj) {
            var maxlength = obj.value;
            if (maxlength.length > 500) {
                obj.value = obj.value.substring(0, 499)
            }
            return true;
        }

        function IsValidate(obj) {


            var rmaDate = document.getElementById("<%= txtRMADate.ClientID %>").value;
            if (rmaDate == "") {
                alert('RmaDate cannot be empty!');
                return false;
            }
            else 
            {
                var UserID = document.getElementById("<%=hdnUserID.ClientID %>");
                if (UserID.value == '1') 
                {
                    var arr = rmaDate.split('/');
                    var months = Math.abs(arr[0] - 1);
                    var days = arr[1];
                    var years = arr[2];
                    var dateRange = "120";


                    var oneDay = 24 * 60 * 60 * 1000; // hours*minutes*seconds*milliseconds

                    var firstDate = new Date();

                    var secondDate = new Date(years, months, days);

                    var diffDays = Math.abs((firstDate.getTime() - secondDate.getTime()) / (oneDay));
                    diffDays = Math.round(diffDays);

                    if (diffDays > dateRange) {
                        var dateRangeMsg = "Invalid RMA Date! Can not create RMA before 120 days back.";
                        alert(dateRangeMsg);
                        return false;
                    }
                }


            }

            var rmaItemcount = document.getElementById("<%= hdnRmaItemcount.ClientID %>").value;
            if (rmaItemcount < 2) {
                alert('There is no ESN to insert!');
                return false;
            }


            var objrmaStatus = document.getElementById("<%= ddlStatus.ClientID %>");
            if (objrmaStatus != null) {
                var allStatusChk = document.getElementById("<%= chkApplyAll.ClientID %>");
                if (allStatusChk != null && !allStatusChk.checked) {
                    var rmaStatus = objrmaStatus.options[objrmaStatus.selectedIndex].text;
                    var arrtxt = document.getElementsByTagName("select");
                    var arrchk = document.getElementsByTagName("input");
                    for (var k = 0; k < arrtxt.length - 1; k++) {
                        if (arrtxt[k].id.indexOf("ddl_Status") > -1) {
                            var esnchk = document.getElementById(arrtxt[k].id.replace('ddl_Status', 'chkESN'));
                            var objstatus = arrtxt[k].options[arrtxt[k].selectedIndex].text;
                            if (esnchk.checked) {
                                if ("Approved" == rmaStatus) {
                                    if ("Approved" == objstatus || "Returned" == objstatus || "Cancelled" == objstatus) { }
                                    else {
                                        var msg = "Item is " + objstatus + " so can not approve the RMA";
                                        alert(msg);
                                        return false;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            if (obj == 1) {
                var objValidateEsn = document.getElementById("<%= hdnValidateESNs.ClientID %>").value;

                if (objValidateEsn != '1') {
                    var validflag = confirm('Do you want to VALIDATE the RMA before submit Yes/No');
                    if (validflag) {
                        document.getElementById("<%= hdnValidateESNs.ClientID %>").value = "2";
                        //document.getElementById("<%= btnValidate.ClientID %>").click();
                        //return false;

                    }
                    else {
                        document.getElementById("<%= hdnValidateESNs.ClientID %>").value = "0";
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

        function alphaNumericCheck(evt) {
            var charCodes = evt.keyCode ? evt.keyCode : evt.which ? evt.which : evt.charCode ? evt.charCode : evt.type;
           // alert(charCodes);
            if (charCodes == 8 || charCodes == 9 || (charCodes >= 35 && charCodes < 40) || charCodes == 46 || (105 >= charCodes && charCodes >= 96)
                || (90 >= charCodes && charCodes >= 65)
                || (57 >= charCodes && charCodes >= 48)
                 || (122 >= charCodes && charCodes >= 97)) {
                return true;
            }
            else {
                return false;
            }
        }

        function doReadonly(evt) {

            evt.keyCode = 0;
            return false;
        }

        function set_focus() {
            var img = document.getElementById("img2");
            var st = document.getElementById("txtRemarks");
            st.focus();
            img.click();
        }
        function set_focus1() {
            var img = document.getElementById("img1");
            var st = document.getElementById("ddlStatus");
            st.focus();
            img.click();
        }

        function hideEsn(val) {

            var mode = getQuerystring('mode');
            var rmaGUID = getQuerystring('rmaGUID');

            if ('esn' == mode || rmaGUID != '' || val == 1) {

                var rmaItemcount = document.getElementById("<%= hdnRmaItemcount.ClientID %>").value;

                if (rmaItemcount > 1) {
                    var submitbtn = document.getElementById("<%= btnSubmitRMA.ClientID %>");
                    var btnValidate = document.getElementById("<%= btnValidate.ClientID %>");

                    submitbtn.disabled = false;
                    btnValidate.disabled = false;
                }
            }
            var comapnyName = document.getElementById('hdncompanyname').value;
            var calltime = document.getElementById('tdCallTime');
            var reason = document.getElementById('tdReason');
            var tdNotes = document.getElementById('tdNotes');
            if (calltime != null && reason != null && tdNotes != null) {

                if (comapnyName == 'iWireless') {

                    calltime.style.display = "none";
                    reason.style.display = "none";
                    tdNotes.style.display = "none";
                }
                else {



                    calltime.style.display = "block";
                    reason.style.display = "block";
                    tdNotes.style.display = "block";
                }
            }
            var hdn_msg = document.getElementById("hdnmsg");
            if (hdn_msg != null) {
                if (hdn_msg.value == 'ESN already added to the RMA!' || hdn_msg.value == 'Esn Should be between 8 to 30 digits!')
                    alert(hdn_msg.value);
                hdn_msg.value = "";
            }

        }
        function checkcompany() {

            var UserID = document.getElementById("<%=hdnUserID.ClientID %>");
            if (UserID.value == '0') {
                var company = document.getElementById("<%=ddlCompany.ClientID %>");
                if (company.selectedIndex == 0) {
                    alert('Company not selected!');
                    return false;
                }
            }
            return true;
        }
        function confirmclear() {
            var lblConfirm = document.getElementById("lblConfirm");
            lblConfirm.value = "";
        }
    </script>
   
    
    <style type="text/css">
        .style1
        {
            FONT-SIZE: 10px;
            COLOR: #000000;
            FONT-FAMILY: Verdana, Arial, Helvetica, sans-serif;
            width: 14%;
        }
        .style2
        {
            width: 18%;
        }
        .style3
        {
            width: 173px;
        }
        .style4
        {
            FONT-SIZE: 10px;
            COLOR: #000000;
            FONT-FAMILY: Verdana, Arial, Helvetica, sans-serif;
            width: 173px;
        }
    </style>


   
    
</head>
<body bgcolor="#ffffff"  leftmargin="0" topmargin="0" marginwidth="0" marginheight="0"  onkeydown="if (event.keyCode==13) {event.keyCode=9; return event.keyCode }">
    <form id="frmRMAItemLookup" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager> 
       <table cellspacing="0" cellpadding="0" border="0"  width="100%" align="center">
        <tr>
            <td>
                <head:MenuHeader ID="HeadAdmin1" runat="server" ></head:MenuHeader>
            </td>
        </tr> 
        </table> 
        <div id="winVP" style="z-index:1">

            <table cellspacing="0" cellpadding="0" border="0"  width="98%" align="center">       
            <tr><td>&nbsp;</td></tr>                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                 
             <tr>
                  <td>
                
                <asp:HiddenField ID="hdnValidateESNs" runat="server" />
                
                
                <asp:HiddenField ID="hdncompanyname" runat="server" />
                
                <table id="rmaform" style="text-align:left; width:100%;"  align="center" class="copy10grey">
                    
                    <tr>
                        <td class="button" align="left">Return Merchandise Authorization(RMA) Form</td>
                    </tr>
                    <tr>
                   <td>
                   <table bordercolor="#839abf" border="1" cellspacing="0" cellpadding="0" width="100%">
                            <tr bordercolor="#839abf">
                                <td>
                       <table  cellSpacing="0" cellPadding="0" width="100%">
                            <tr>
                            <td class="copy10grey" width="60%">
                                &nbsp;- Please VALIDATE the RMA before submitting. System will give a warning message if RMA is not validated through <b>"Validate ESNs"</b><br />
                                &nbsp;- Maximum of 10 ESNs are allowed in one RMA request.<br />
                                &nbsp;- Please enter your correct SHIP TO information in the space provided. ALL units will be returned to the default address on the account if this is incomplete or missing.<br />
                                &nbsp;- For all customers, please take note of the returns checklist available for download via <a href="#" target="_blank">Lan Global inc. RMA Checklist</a>.<br />
                                &nbsp;- For Sprint dealers, please take note of the returns policies available for download via <a href="#" target="_blank">Sprint Dealers Returns Policies</a>.

                                <br />
&nbsp;- Email should not have &quot;Lan Global inc..com&quot; email address.
<br />
                                &nbsp;- Upto 10 ESNs allowed per return (RMA).
                                <br />
                                &nbsp;- Remove the RMA Line Item (ESN) by de-selecting the checkbox.
                                <br />
                                &nbsp;- <span class="copy10underline" > Please note that our new online RMA process allows for ten ESNs for each entry. You may file another request separately.</span>
</td>
                            <td bgcolor="#839abf" width="1">&nbsp;</td>
                            <td  class="copy10grey"  width="40%">
                                    &nbsp;Please send ALL returns to: <br />
                                     &nbsp;<b>Lan Global inc.</b><br />
                                     &nbsp;Attention: RMA Department AVRMA#<br />
                                     &nbsp;2265 E. El Segundo Blvd.,<br />
                                     &nbsp;El Segundo, CA 90245<br />
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
                            <asp:Label ID="lblConfirm" runat="server" CssClass="errormessage"></asp:Label>
                                   <asp:Label ID="lbl_msg" runat="server" CssClass="errormessage"></asp:Label> 
                        </td>
                    </tr>
                    <tr>
                        <td>
                          <table bordercolor="#839abf" border="1" cellspacing="0" cellpadding="0" width="100%">
                            <tr bordercolor="#839abf">
                                <td>
                                <table class="box" border="0"  align="center" width="100%">
                                    <tr>
                                        <td class="style1" align="left" Class="copy10grey">
                                             <asp:Label ID="lblCompany" runat="server" Text="Company:"></asp:Label></td>
                                        <td class="style2">
                                
                                            <asp:HiddenField ID="hdnUserID" runat="server" />
                                            <asp:DropDownList ID="ddlCompany"  AutoPostBack="true" CssClass="copy10grey" 
                                                 runat="server" 
                                                onselectedindexchanged="ddlCompany_SelectedIndexChanged">
                                            </asp:DropDownList>
                              
                                        </td>
                                    </tr>                                     
                                     <tr >
                                        <td   class="style1"  align="left"><asp:Label ID="lblrmanumber" runat="server" Text="RMA#:" CssClass="copy10grey"></asp:Label></td>
                                                                                        <td class="style2"  >
                                                <asp:Label ID="txtRmaNum" runat="server" 
                                                    CssClass="copyblue11b" Width="80%" ReadOnly="True" 
                                                     />
                                        </td>
                                        <td class="copy10grey" ><asp:Label ID="lblRMADate" runat="server" Text="RMA Date:"  CssClass="copy10grey"></asp:Label>
                                            <span Class="errormessage">*</span></td>
                                        <td class="style3">
                                            <asp:Panel ID="rmadtpanel" runat="server">
                                            &nbsp;<asp:TextBox ID="txtRMADate" runat="server"  onfocus="set_focus1();" onkeypress="return doReadonly(event);" 
                                                CssClass="copy10grey" MaxLength="15" />
                                            <img id="img1" alt=""  onclick="document.getElementById('<%=txtRMADate.ClientID%>').value = ''; displayCalendar(document.getElementById('<%=txtRMADate.ClientID%>'),'mm/dd/yyyy',this,true);" src="../../fullfillment/calendar/sscalendar.jpg" />
                                            </asp:Panel>
                                        </td> 
                                        <td class="copy10grey" width="10%"><span id="status" runat="server">Status:</span></td>
                                        <td Class="copy10grey" width="20%">
                                        <table width="100%" cellpadding="0" cellspacing="0">
                                        <tr valign="top">
                                            <td>
                                             &nbsp;<asp:Label ID="lblStatus" runat="server" Text="Pending"></asp:Label>
                                            <asp:DropDownList ID="ddlStatus"  runat="server" Class="copy10grey" 
                                                Width="166px" onselectedindexchanged="ddlStatus_SelectedIndexChanged">
                                                        <asp:ListItem  Value="0" >------</asp:ListItem>
                                                        <asp:ListItem Value="1" >Pending</asp:ListItem>
                                                        <asp:ListItem  Value="2">Received</asp:ListItem>
                                                        <asp:ListItem  Value="3">Pending for Repair</asp:ListItem>
                                                        <asp:ListItem  Value="4">Pending for Credit</asp:ListItem>
                                                        <asp:ListItem  Value="5">Pending for Replacement</asp:ListItem>
                                                        <asp:ListItem  Value="6">Approved</asp:ListItem>
                                                        <asp:ListItem  Value="7">Returned</asp:ListItem>
                                                        <asp:ListItem  Value="8">Credited</asp:ListItem>
                                                        <asp:ListItem  Value="9">Denied</asp:ListItem>
                                                        <asp:ListItem  Value="10">Closed</asp:ListItem>

                                                        <asp:ListItem  Value="11" >Out with OEM for repair</asp:ListItem>
                                                        <asp:ListItem  Value="12">Back to Stock -NDF</asp:ListItem>
                                                        <asp:ListItem  Value="13">Back to Stock- Credited</asp:ListItem>
                                                        <asp:ListItem  Value="14">Back to Stock – Replaced by OEM</asp:ListItem>
                                                        <asp:ListItem  Value="15">Repaired by OEM</asp:ListItem>
                                                        <asp:ListItem  Value="16">Replaced BY OEM</asp:ListItem>
                                                        <asp:ListItem  Value="17">Replaced BY AV</asp:ListItem>
                                                        <asp:ListItem  Value="18">Repaired By AV</asp:ListItem>
                                                        <asp:ListItem  Value="19">NDF (No Defect Found)</asp:ListItem>
                                                        <asp:ListItem  Value="20">PRE-OWNED – A stock</asp:ListItem>
                                                        <asp:ListItem Value="21" >PRE-OWEND - B Stock</asp:ListItem>
                                                        <asp:ListItem Value="22" >PRE-OWEND – C Stock</asp:ListItem>
                                                        <asp:ListItem Value="23" >Rejected</asp:ListItem>
                                                        <asp:ListItem Value="24" >RTS (Return To Stock)</asp:ListItem>
                                                        <asp:ListItem Value="25" >Incomplete</asp:ListItem>
<asp:ListItem Value="26" >Damaged</asp:ListItem>
<asp:ListItem Value="27" >Preowned</asp:ListItem>
<asp:ListItem Value="28" >Return to OEM</asp:ListItem>
<asp:ListItem Value="29" >Returned to Stock</asp:ListItem>
                                                        
                                            </asp:DropDownList>
                                            <br />
                                            <asp:CheckBox ID="chkApplyAll" runat="server" Text="Apply to all ESNs" />
                                            </td>
                                            <td>
                                            <asp:UpdatePanel ID="UpdatePanel1"  runat="server" >
     <ContentTemplate>
     





&nbsp;&nbsp;                                            &nbsp;<asp:ImageButton ID="imgRma"  ToolTip="View RMA Report" OnClick="imgViewRMA_Click"  CausesValidation="false"  ImageUrl="~/Images/view.png"  runat="server" />
  <table>
        <tr>
            <td>
            <cc1:ModalPopupExtender BackgroundCssClass="modal5Background"
            CancelControlID="btnClose2"  runat="server" PopupControlID="pnlRmap" 
            ID="mdlPopup2" TargetControlID="lnk2"
             />
            <asp:LinkButton ID="lnk2" runat="server" ></asp:LinkButton>
            <asp:Panel ID="pnlRmap" runat="server" CssClass="modal5Popup" >
                <div style="overflow:auto; height:450px; width:100%; border: 0px solid #839abf" >
      
                <table width="100%">
                <tr>
                    <td class="button">
                        RMA Report
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Button ID="btnClose2" runat="server" Text="Close" CssClass="button" />
                    </td>
                </tr>
                <tr>
                    <td >
                    <table bordercolor="#839abf" border="1" width="98%" align="center" cellpadding="0" cellspacing="0">
                    <tr><td>

                    <table border="0" width="100%" class="box" align="center" cellpadding="0" cellspacing="0">
                    <tr valign="top">
                        <td>
                            <asp:Label ID="lblHistory" runat="server" CssClass="errormessage"></asp:Label>
                            
                            <asp:Repeater ID="rptRma" runat="server">
                            <HeaderTemplate>
                            <table width="100%" align="center">
                                <tr>
                                    <td class="button">
                                    &nbsp;Status
                                    </td>
                                    <td class="button">
                                        &nbsp;Last Modified Date
                                    </td>
                                    <td class="button">
                                        &nbsp;Modified By
                                    </td>
                                    
                                </tr>
                            
                            </HeaderTemplate>
                            <ItemTemplate>
                            <tr>
                                <td class="copy10grey">
                                    &nbsp;<%# Eval("Status") %>
                                </td>
                                <td class="copy10grey">
                                    &nbsp;<%# DataBinder.Eval(Container.DataItem, "ModifiedDate", "{0:MM/dd/yyyy}")%>
                                </td>
                                <td class="copy10grey">
                                     &nbsp;<%# Eval("RmaContactName") %>
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
                    </td></tr>
                    </table>
                    </td>
                </tr>
                </table>
            
                </div>
            </asp:Panel>


            </td>
        </tr>
        </table>

       <%-- <cc1:ModalPopupExtender BackgroundCssClass="modal5Background"
            CancelControlID="btnClose1"  runat="server" PopupControlID="pnlRmap2" 
            ID="mdlRmaXml" TargetControlID="lnk1"
             />
            <asp:LinkButton ID="lnk1" runat="server" ></asp:LinkButton>
            <asp:Panel ID="pnlRmap2" runat="server" CssClass="modal5Popup" >
                <div style="overflow:auto; height:450px; width:100%; border: 0px solid #839abf" >
      
                <table width="100%">
                <tr>
                    <td class="button">
                        RMA Report
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Button ID="btnClose1" runat="server" Text="Close" CssClass="button" />
                    </td>
                </tr>
                <tr>
                    <td >
                    <table bordercolor="#839abf" border="1" width="98%" align="center" cellpadding="0" cellspacing="0">
                    <tr><td>

                    <table border="0" width="100%" class="box" align="center" cellpadding="0" cellspacing="0">
                    <tr valign="top">
                        <td>
                        
                            <asp:Label ID="lblRmaXml" runat="server" CssClass="copy10grey" ></asp:Label>   
                            
                        
                        </td>
                    </tr>
                    </table>
                    </td></tr>
                    </table>
                    </td>
                </tr>
                </table>
            
                </div>
            </asp:Panel>--%>




     </ContentTemplate>
     </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                        </table>
                                           

      
                                            
                                        </td>
                                                                      
                                    </tr> 
                                    <tr>
                                        <td colspan="6"><hr size="1" align="center" style="width: 100%" /></td>
                                    </tr>
                                    <tr>
                                        <td   class="style1"  align="left">Customer Name:&nbsp;<span Class="errormessage">*</span></td>
                                        <td colspan="5">
                                            <asp:TextBox ID="txtCustName" runat="server" 
                                                CssClass="copy10grey" MaxLength="50" Width="50%" />
                                        </td>                            
                                    </tr>
                         <tr>
                            <td   class="style1"  align="left">Address:&nbsp;<span Class="errormessage">*</span></td>
                            <td colspan="5">
                                <asp:TextBox ID="txtAddress" runat="server" CssClass="copy10grey" Width="90%" MaxLength="200"/>
                            </td>                            
                        </tr>
                        <tr>
                            <td   class="style1"  align="left">City:
                                        &nbsp;<span Class="errormessage">*</span>
                            </td>
                            <td class="style2">
                                <asp:TextBox ID="txtCity" runat="server"  Width="80%"  CssClass="copy10grey" MaxLength="50"/>
                            </td>                            
                            <td class="copy10grey"  align="left"  width="10%">State:&nbsp;<span Class="errormessage">*</span></td>
                            <td class="style3" width="20%">
                                <asp:dropdownlist id="dpState"  runat="server"  Width="91%" cssclass="copy10grey">
                                </asp:dropdownlist>
                               <%-- <asp:TextBox ID="txtState" runat="server"  Width="90%"  CssClass="copy10grey" 
                                    MaxLength="30" />--%>
                             </td>
                             <td class="copy10grey"  align="left" >Zip:&nbsp;<span Class="errormessage">*</span></td>    
                              <td  width="20%">  <asp:TextBox ID="txtZip" runat="server" Width="37%"  
                                      CssClass="copy10grey" MaxLength="6"/>
                            </td>  
                        </tr>
              
                                    <tr>
                            <td   class="style1"  align="left">Email:&nbsp;<span Class="errormessage">*</span>
                            </td>
                            <td class="style2">
                                <asp:TextBox ID="txtEmail" Width="90%" runat="server" CssClass="copy10grey" MaxLength="50"/>
                            </td>                            
                            <td class="copy10grey"  align="left">Phone:&nbsp;<span Class="errormessage">*</span></td>
                            <td class="style3">
                                <asp:TextBox ID="txtPhone"  Width="90%" runat="server" CssClass="copy10grey" 
                                    MaxLength="12" />
                             </td>                            
                        </tr>
                                    <tr valign="top">
                            <td   class="style1" align="left">Comments:</td>
                            <td colspan="2">
                                <asp:TextBox runat="server" ID="txtRemarks" TextMode="MultiLine"
                                    Rows="3" Columns="80" CssClass="copy10grey" 
                                     Width="90%" />
                            </td>
                            <div runat="server" id="divAvc">
                                <td   class="style4" align="right">Lan Global inc. Comments:</td>
                                <td colspan="2">
                                    <asp:TextBox runat="server" ID="txtAVComments" TextMode="MultiLine"
                                        Rows="3" Columns="80" CssClass="copy10grey" 
                                         Width="90%" />
                                </td>
                            </div>
                        </tr> 
                                </table>      
                                </td>
                            </tr>
                        </table>      
                        </td>          
                    </tr>
                    <tr><td>&nbsp;</td></tr>
                    <tr>
            <td align="Left" class="button">
                    Return Merchandise Authorization(RMA) Line Items
             </td>
          </tr>
    
         
                    <tr>
            <td align="center"> 
            
                <table id="rmadetail"  cellspacing="0" cellpadding="0" width="100%" style="text-align: center" >                
                    <tr>                 
                        <td align="left">
                        <asp:Panel ID="POpanel" runat="server"  >
                        <%-- <table id="Table1"  bordercolor="#839abf" border="1" cellspacing="0" cellpadding="0" width="100%">                
                            <tr>                 
                            <td align="Center" width="80%" >
                            <table width="60%" style="display:none; margin-right:50px;" id="rmabypo" cellpadding="0" cellspacing="0">
                                <tr valign="middle" height="36">
                                    <td align="left">
                                        <asp:TextBox runat="server" ID="txtPo_num" MaxLength="20" CssClass="copy10grey" onkeypress="return isNumberKey(event);"  />                                       
                                    </td>
                                    <td>
                                        
                                        <asp:Button ID="RMA_po_button" Text="Generate RMA Detail from PO" runat="server" OnClientClick="return checkcompany();" CssClass="buybt" OnClick="RMA_po_button_click" CausesValidation="false" />                                        
                                    </td>
                                </tr>
                            </table>
                            </td>
                            </tr>
                            </table>--%>
                            </asp:Panel>
                            </td>
                            </tr>
                         </table>
                        </td>
                    </tr>
                    
                    <tr>
                        <td align="center">
                            <asp:Panel ID="pnluploaddetail" runat="server">
                            <table align="left">

                            <tr>
                                <td align="left">
                                    RMA Uploaded:
                                </td>
                                <td align="left">
                                    <asp:Label ID="lblRMA" runat="server" CssClass="errormessage"></asp:Label> 
                                </td>
                            </tr>
                            <tr>
                                <td  align="left">
                                    External Ens:
                                </td>
                                <td align="left">
                                    <asp:Label ID="lblExternalEsn" runat="server" CssClass="errormessage"></asp:Label> 
                                </td>
                            </tr>
                            <tr>
                                <td  align="left">
                                    Invalid Esn:
                                </td>
                                <td align="left">
                                    <asp:Label ID="lblInvalid_esn" runat="server" CssClass="errormessage"></asp:Label>  
                                </td>
                            </tr>
                            </table>
                                
                                
                                
                                  
                            </asp:Panel>
                             
            
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" ChildrenAsTriggers="true">
                                        
                                        <ContentTemplate>
                                        <%--<script type="text/javascript">
                                        Sys.Application.add_load(hideEsn);
                                        </script>--%>
                            <asp:Panel ID="esnPanel" runat="server" align="center">
                            <table width="100%" cellspacing="0" cellpadding="0" border="0"  align="center" >
                                <tr>
                                    <td id="td_esn" align="left">
                                       
                                        
                                        <asp:HiddenField ID="hdnmsg" runat="server" />
                                        <asp:HiddenField ID="hdnRmaItemcount" Value="1" runat="server" />
                                        <table  cellpadding="1" cellspacing="1" border="3"  width="100%">
                                        <asp:DataList ID="Dl_rma_detail" runat="server" 
                                            OnItemDataBound="DataList1_ItemDataBound" Width="100%">
                                            <HeaderTemplate >
                                            
                                            <tr style="height: 40px" valign="top">
                                                <td class="button" Width="1%">&nbsp;</td>
                                                <td class="button" Width="15%" align="left">ESN</td>
                                                <td class="button" Width="5%"  align="left">UPC</td>
                                                <td class="button" Width="5%"  align="left">SKU#</td>
                                                <td class="button" Width="5%"  align="left">AVSO#</td>
                                                <td class="button" Width="5%"  align="left">Purchase Order#</td>
                                                <td class="button" Width="5%"  align="left" id="tdCallTime">Call Time</td>
                                                <td class="button" Width="10%"  align="left" id="tdReason">Reason</td>                                             
                                                <td class="button" Width="20%"  align="left" id="tdnotes">Notes</td>
                                                <td  Width="10%"  bgcolor="navy"   class ='<%# ShowRMADetailStatus() %>'> 
                                                    <asp:Label ID="lblStatusheader" Height="100%" Width="100%" runat="server" Text=" Status  " ForeColor="white" Font-Bold="true"></asp:Label>
                                                 </td>
                                                
                                            </tr>
                                           <%-- </table>--%>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                
                                               <%-- <table  border="2" cellspacing="2" cellpadding="2" width="100%" align="center">--%>
                                                    <tr id="trCallTimerow" valign="bottom">
                                                        <td >
                                                        

                                                        

                                                            <asp:CheckBox ID="chkESN" Checked="true" Enabled='<%# Convert.ToInt32(Eval("StatusID")) > 1 ? false : true%>' runat="server" />
                                                        </td>
                                                        <td  >
                                                            <asp:HiddenField ID="hdnRMADetGUID" Value='<%# Eval("rmaDetGUID") %>' runat="server" />
                                                            
                                                         <asp:TextBox ID="txt_ESN" ReadOnly='<%# Convert.ToInt32(Eval("StatusID")) > 1 ? true : false%>' Text = '<%# Eval("esn") %>'  runat="server"  Width="95%" onkeypress="return alphaNumericCheck(event);"  
                                                                      AutoPostBack="true" ontextchanged="ESN_TextChanged" MaxLength="30" CssClass="esntext" ></asp:TextBox>
                                                                
                                                         </td>

                                                        <td  >
                                                            <%--<asp:HiddenField ID="hdnAllowRMA" Value='<%# Eval("AllowRMA") %>' runat="server" />
                                                            <asp:HiddenField ID="hdnDuplicate" Value='<%# Eval("AllowDuplicate") %>' runat="server" />
                                                            --%>
                                                            <asp:Label ID="lblinvalidESN" CssClass ="errormessage" runat="server" ></asp:Label>
                                                            
                                                            <asp:Label ID="lblItemCode" Text = '<%# Eval("UPC") %>'  CssClass="copy10grey" Width="100%" runat="server"></asp:Label>
                                                        </td>
                                                        <td Class="copy10grey">
                                                            <%# Eval("Itemcode") %>                                                          
                                                        </td>
                                                        <td Class="copy10grey">
                                                            <%# Eval("AVSalesOrderNumber") %>                                                            
                                                        </td>  
                                                        <td Class="copy10grey">
                                                            <%# Eval("PurchaseOrderNumber") %>                                                           
                                                        </td>                                                         <td >
                                                            <asp:TextBox ID="txtCallTime"  ReadOnly='<%# Convert.ToInt32(Eval("StatusID")) > 1 ? true : false%>' Width="80%" Text='<%# Eval("CallTime") %>' ontextchanged="CallTime_TextChanged" onkeypress="return isNumberKey(event);"  CssClass="copy10grey" runat="server" MaxLength="3"></asp:TextBox>
                                                            
                                                          
                                                        </td>
                                                        
                                                          <td >

                                                              <asp:DropDownList ID="ddReason"  runat="server" Width="100%"  CssClass ="copy10grey"> 
                                                                    <asp:ListItem  Value="0" >------</asp:ListItem>
                                                                     <asp:ListItem Value="1" >DOA</asp:ListItem>
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
                                                                    <asp:ListItem  Value="13">ActivationIssues</asp:ListItem>
                                                                    <asp:ListItem  Value="14">CoverageIssues</asp:ListItem>
                                                                    <asp:ListItem  Value="15">LoanerProgram</asp:ListItem>
                                                              </asp:DropDownList>
                                                              <asp:HiddenField id="hdnReason" runat="server" Value='<%# Eval("Reason") %>'/>
                                                            </td>
                                                            
                                                            <td >
                                                                <asp:TextBox ID="txtNotes"  ontextchanged="Notes_TextChanged"  Text='<%# Eval("Notes") %>'  Width="95%" 
                                                                CssClass="copy10grey" runat="server" MaxLength="1000" TextMode="MultiLine"></asp:TextBox>
                                                           </td>

                                                           <td  class ='<%# ShowRMADetailStatus() %>'>
                                                                 <asp:DropDownList ID="ddl_Status"    runat="server" Class="copy10grey" Width="95%">
                                                                    <asp:ListItem  Value="0" >------</asp:ListItem>
                                                                 

                                                                     <asp:ListItem Value="1" >Pending</asp:ListItem>
                                                                    <asp:ListItem  Value="2">Received</asp:ListItem>
                                                                    <asp:ListItem  Value="3">Pending for Repair</asp:ListItem>
                                                                    <asp:ListItem  Value="4">Pending for Credit</asp:ListItem>
                                                                    <asp:ListItem  Value="5">Pending for Replacement</asp:ListItem>
                                                                    <asp:ListItem  Value="6">Approved</asp:ListItem>
                                                                    <asp:ListItem  Value="7">Returned</asp:ListItem>
                                                                    <asp:ListItem  Value="8">Credited</asp:ListItem>
                                                                    <asp:ListItem  Value="9">Denied</asp:ListItem>
                                                                    <asp:ListItem  Value="10">Closed</asp:ListItem>
                                                                    <asp:ListItem  Value="11" >Out with OEM for repair</asp:ListItem>
                                                                    <asp:ListItem  Value="12">Back to Stock -NDF</asp:ListItem>
                                                                    <asp:ListItem  Value="13">Back to Stock- Credited</asp:ListItem>
                                                                    <asp:ListItem  Value="14">Back to Stock – Replaced by OEM</asp:ListItem>
                                                                    <asp:ListItem  Value="15">Repaired by OEM</asp:ListItem>
                                                                    <asp:ListItem  Value="16">Replaced BY OEM</asp:ListItem>
                                                                    <asp:ListItem  Value="17">Replaced BY AV</asp:ListItem>
                                                                    <asp:ListItem  Value="18">Repaired By AV</asp:ListItem>
                                                                    <asp:ListItem  Value="19">NDF (No Defect Found)</asp:ListItem>
                                                                    <asp:ListItem  Value="20">PRE-OWNED – A stock</asp:ListItem>
                                                                    <asp:ListItem Value="21" >PRE-OWEND - B Stock</asp:ListItem>
                                                                    <asp:ListItem Value="22" >PRE-OWEND – C Stock</asp:ListItem>
                                                                    <asp:ListItem Value="23" >Rejected</asp:ListItem>
                                                                    <asp:ListItem Value="24" >RTS (Return To Stock)</asp:ListItem>
                                                                    <asp:ListItem Value="25" >Incomplete</asp:ListItem>
<asp:ListItem Value="26" >Damaged</asp:ListItem>
<asp:ListItem Value="27" >Preowned</asp:ListItem>
<asp:ListItem Value="28" >Return to OEM</asp:ListItem>
<asp:ListItem Value="29" >Returned to Stock</asp:ListItem>

                                                                </asp:DropDownList>
                                                                <asp:HiddenField id="hdnStatus" runat="server" Value='<%# Eval("StatusID") %>'/>
                                                           </td>
  
                                                    </tr>
                                                   
                                                   
                                            </ItemTemplate>  
                                            <FooterTemplate>

                                            </FooterTemplate>                  
                                            </asp:DataList>
                                            </table>
                                      
                                    </td>
                                </tr>
                            </table>
                            </asp:Panel>
                                  
                                        </ContentTemplate>
                                        </asp:UpdatePanel>
                        </td>                    
                    </tr>

                    <tr><td><hr /></td></tr>
                    <tr>
                        <td align="center">
                            <asp:Panel ID="btnpanel" runat="server">
                            <asp:Button ID="btnValidate" CssClass="buybt" runat="server" Text="Validate ESNs" 
                                    OnClientClick="return IsValidate(0);" OnClick="btnValidate_click" />&nbsp;   
                                <asp:Button ID="btnSubmitRMA"  CssClass="buybt" runat="server" Text="Submit RMA"  
                                    OnClientClick="return IsValidate(1);" OnClick="btnSubmitRMA_click" />&nbsp;       
                                <asp:Button ID="btn_Cancel" CssClass="buybt" runat="server" Text="Cancel" 
                                    CausesValidation="false" OnClick="btnCancelRMA_click" /> 
                                <asp:Button ID="btnBack" CssClass="buybt" runat="server" Text="Back to search" 
                                    CausesValidation="false" OnClick="btnBackRMAQuery_click" />                
                                </asp:Panel>
                        </td>
                    </tr>        
                </table> 
                
                </td> 
              </tr>                 

            
            <tr><td>&nbsp;
            
          

            </td></tr>
            <tr><td>&nbsp;
            
            
            </td></tr>
            <tr>
                <td><foot:MenuFooter id="Footer" runat="server"></foot:MenuFooter></td>
            </tr>
            </table>
        </div>
    </form>
</body>
</html>
