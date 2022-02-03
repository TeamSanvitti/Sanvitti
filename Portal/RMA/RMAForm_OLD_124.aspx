<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RMAForm.aspx.cs" Inherits="avii.Admin.RMAForm" ValidateRequest="false"  %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="~/Controls/Header.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>.:: Return Merchandise Authorization (RMA) ::.</title>
    <link href="/aerostyle.css" type="text/css" rel="stylesheet"/>
<link type="text/css" href="../../UIThemes/themes/base/ui.all.css" rel="stylesheet" />
    <link rel="stylesheet"  type="text/css" href="../fullfillment/calendar/dhtmlgoodies_calendar.css" media="screen"></link>
	<script type="text/javascript" src="../fullfillment/calendar/dhtmlgoodies_calendar.js"></script>  
	<script type="text/javascript" src="../../JSLibrary/jquery-1.3.2.min.js"></script>
	<script type="text/javascript" src="../../JSLibrary/ui/ui.core.js"></script>
	<script type="text/javascript" src="../../JSLibrary/ui/ui.tabs.js"></script>
	<link href="../dhtmlwindow.css" type="text/css" rel="stylesheet" />
    <link rel="icon" href="../common/favicon.ico" type="image/x-icon" />
	<link rel="shortcut icon" href="../common/favicon.ico" type="image/x-icon" />
	
	<link rel="stylesheet" href="../dhtmlxwindow/style.css" type="text/css" media="screen" />
    <link rel="stylesheet" type="text/css" href="../dhtmlxwindow/dhtmlxwindows.css">
	<link rel="stylesheet" type="text/css" href="../dhtmlxwindow/skins/dhtmlxwindows_dhx_skyblue.css">
	
	<script src="../dhtmlxwindow/dhtmlxcommon.js"></script>
	<script src="../dhtmlxwindow/dhtmlxwindows.js"></script>
	<script src="../dhtmlxwindow/dhtmlxcontainer.js"></script>
    <script type="text/javascript" language="javascript">
    var dhxWins, w1;
    function doOnLoad(qstring) {
        var comapnyName = document.getElementById('hdncompanyname').value;
        var calltime = document.getElementById('tdCallTime');
        var reason = document.getElementById('tdReason');
        var tdNotes = document.getElementById('tdNotes');      
		dhxWins = new dhtmlXWindows();
		dhxWins.enableAutoViewport(false);
		dhxWins.attachViewportTo("winVP");
		dhxWins.setImagePath("../../codebase/imgs/");
		w1 = dhxWins.createWindow("w1", 220, 60, 365, 250);
		w1.setText("Choose ESN");
		
		w1.attachURL("esnpopup.aspx?qstring="+qstring);
		
	}

	function getid(obj) {
	    
	    var esnid = 'txt_ESN';
	    var itemid = document.getElementById(obj.id.replace(esnid, 'lblItemCode'));

	    var hdnitemid = document.getElementById(obj.id.replace(esnid, 'hdnlblItemCode'));

	    var podid = document.getElementById(obj.id.replace(esnid, 'hdnPod_id'));
	    var hdnitemcode = document.getElementById("hdnitemcode");


	    var hdnitemcodelbl = document.getElementById("hdnlblitemcode");
	    hdnitemcodelbl.value = hdnitemid.id;

	    var hdnpodid = document.getElementById("hdnpodid");
	    hdnitemcode.value = itemid.id;
	    hdnpodid.value = podid.id;
	}
   
        jQuery.fn.fadeIn = function(speed, callback) {
            return this.animate({ opacity: 'show' }, speed, function() {
                if (jQuery.browser.msie)
                    this.style.removeAttribute('filter');

                if (jQuery.isFunction(callback))
                    callback();
            });
        };

        jQuery.fn.fadeOut = function(speed, callback) {
            return this.animate({ opacity: 'hide' }, speed, function() {
                if (jQuery.browser.msie)
                    this.style.removeAttribute('filter');

                if (jQuery.isFunction(callback))
                    callback();
            });
        };

        jQuery.fn.fadeTo = function(speed, to, callback) {
            return this.animate({ opacity: to }, speed, function() {
                if (to == 1 && jQuery.browser.msie)
                    this.style.removeAttribute('filter');

                if (jQuery.isFunction(callback))
                    callback();
            });
        }; 


       
       

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

        function generateRMADetail() {
            var mode = getQuerystring('mode');
            if ('po' == mode) {
                $("#rmabypo").css('display', 'block');
                $(".chk").css('display', 'block');
            }
            else {
                $("#rmabypo").css('display', 'none');
                $(".chk").css('display', 'none');
            }
            
            
            
            
            $(".admindisplay").toggle();
        }
        function textchanged(obj) {

            var esn = obj.value;
            var esn_detail = RMAForm.getesnDetails(esn);
            var esndetail = esn_detail.value;
            var esnerr = esndetail.split(',');
            var fundqnr = document.getElementById(uniqueID.id.replace('txtAnswer', 'hdnfundquestionnaireID'));

        }
        

       
       function isMaxLength(obj) 
       {
           var maxlength = obj.value;
           if (maxlength.length > 500) 
           {
               obj.value = obj.value.substring(0, 499)
           }
           return true;
       }
       
       function IsValidate(obj)
       {
       
         
           var rmaDate = document.getElementById("<%= txtRMADate.ClientID %>").value;
           if(rmaDate == "")
           {
                alert('RmaDate cannot be empty!');
                return false;
           } 
           else
           {
               var arr = rmaDate.split('/');
               var months = Math.abs(arr[0]-1);
               var days = arr[1];
               var years = arr[2];
               var dateRange = "90";
               
               
               var oneDay = 24*60*60*1000;	// hours*minutes*seconds*milliseconds
               
               var firstDate = new Date();
               
               var secondDate = new Date(years,months,days);
               
               var diffDays = Math.abs((firstDate.getTime() - secondDate.getTime())/(oneDay));
               diffDays = Math.round(diffDays);
               
               if(diffDays > dateRange)
               {
                   var dateRangeMsg = "Invalid RMA Date! Can not create RMA before 90 days back.";
                    alert(dateRangeMsg);
                    return false;
               }
               
               
           }
           
           var rmaItemcount = document.getElementById("<%= hdnRmaItemcount.ClientID %>").value;
           if(rmaItemcount < 2)
           {
               alert('There is no ESN to insert!');
               return false;
           }
           
           
           var objrmaStatus = document.getElementById("<%= ddlStatus.ClientID %>");
           if (objrmaStatus != null) {
               var rmaStatus = objrmaStatus.options[objrmaStatus.selectedIndex].text;
               var arrtxt = document.getElementsByTagName("select");
               var arrchk = document.getElementsByTagName("input");
               for (var k = 0; k < arrtxt.length-1; k++) {
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
           
           if(obj==1)
           {
              var objValidateEsn = document.getElementById("<%= hdnValidateESNs.ClientID %>").value;
         
              if(objValidateEsn != '1')
              {
                var validflag = confirm('Do you want to VALIDATE the RMA before submit Yes/No');
                if (validflag) 
                {
                    document.getElementById("<%= hdnValidateESNs.ClientID %>").value = "2";
                    //document.getElementById("<%= btnValidate.ClientID %>").click();
                    //return false;

                }
                else
                    document.getElementById("<%= hdnValidateESNs.ClientID %>").value = "1";
             }
           
         }    
       }
       function isNumberKey(evt)
        {
            
            var charCodes = evt.keyCode ? evt.keyCode : evt.which ? evt.which : evt.charCode;
           if (charCodes > 31 && (charCodes < 48 || charCodes > 57))
           {
               charCodes = 0;
               return false;
           }
           return true;
        }
        
       function alphaNumericCheck(fieldVal)
       {
        if((105>=fieldVal.keyCode && fieldVal.keyCode>=96)  
                || (90>=fieldVal.keyCode && fieldVal.keyCode>=65) 
                ||(57>=fieldVal.keyCode && fieldVal.keyCode>=48)
                 ||(122>=fieldVal.keyCode && fieldVal.keyCode>=97))
            { 
              return true; 
            } 
            else 
            { 
                return false; 
            } 
        }
        
        function  doReadonly(evt)
        {
            
           evt.keyCode = 0;
           return false;
        }
        
        function set_focus() 
        {
            var img = document.getElementById("img2");
            var st = document.getElementById("txtRemarks");
            st.focus();
            img.click();
        }
        function set_focus1() 
        {
            var img = document.getElementById("img1");
            var st = document.getElementById("txtInvoicenum");
            st.focus();
            img.click();
        }

        function hideEsn(val) {
        
            var mode = getQuerystring('mode');
            var rmaGUID = getQuerystring('rmaGUID');
            
            if ('esn' == mode || rmaGUID != ''|| val==1) {
          
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
     
     function displayUpload()
     {
        var mode = getQuerystring('mode');
        if ('import' == mode) {
            var rmaupload = document.getElementById("<%= rmaupload.ClientID %>");
            var uploadDiv = document.getElementById('btnUploadRMA');
            
                if (rmaupload.value != '')
                    uploadDiv.style.display = "block";
            
        }
    }
    function emptyRmafile() {
        var rmaupload = document.getElementById("<%= rmaupload.ClientID %>");
        rmaupload.value = "";
        var lbl_msg = document.getElementById("<%= lbl_msg.ClientID %>");
        var lblRMA = document.getElementById("<%= lblRMA.ClientID %>");
        var lblExternalEsn = document.getElementById("<%= lblExternalEsn.ClientID %>");
        var lblInvalid_esn = document.getElementById("<%= lblInvalid_esn.ClientID %>");
        lbl_msg.innerHTML="";
        lblRMA.innerHTML="";
        lblExternalEsn.innerHTML="";
        lblInvalid_esn.innerHTML="";
        
        return true;
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
   
    
</head>
<body bgcolor="#ffffff"  leftmargin="0" topmargin="0" marginwidth="0" marginheight="0"  onkeydown="if (event.keyCode==13) {event.keyCode=9; return event.keyCode }">
    <form id="frmRMAItemLookup" runat="server">
    
       <table cellspacing="0" cellpadding="0" border="0"  width="98%" align="center">
        <tr>
            <td>
                <head:MenuHeader ID="HeadAdmin1" runat="server" ></head:MenuHeader>
            </td>
        </tr> 
        </table> 
        <div id="winVP" style="z-index:1">
            <table cellspacing="0" cellpadding="0" border="0"  width="98%" align="center">                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        
             <tr>
                  <td>
                
                <asp:HiddenField ID="hdnValidateESNs" runat="server" />
                <asp:HiddenField ID="hdnitemcode" runat="server" />
                <asp:HiddenField ID="hdnlblitemcode" runat="server" />
                <asp:HiddenField ID="hdnpodid" runat="server" />
               
                <table id="rmaform" style="text-align:left; width:100%;"  align="center" class="copy10grey">
                    <tr><td>&nbsp;</td></tr>
                    <tr>
                        <asp:HiddenField ID="hdncompanyname" runat="server" />
                        <td class="button" align="left">Return Merchandise Authorization(RMA) Form</td>
                    </tr>
                    <tr>
                   <td>
                       <table  cellSpacing="1" cellPadding="1" width="100%">
                            <tr><td class="copy10grey">
                                - Please VALIDATE the RMA before submitting. System will give a warning message if RMA is not validated through <b>"Validate ESNs"</b><br />
                                - Atleast one ESN is assigned to RMA before Submission.<br />
                                - Please enter/validate your email and phone number for all correspondence.
                            </td></tr>
                        </table>
                   </td>
                   </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblConfirm" runat="server" CssClass="errormessage"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                          <table bordercolor="#839abf" border="1" cellspacing="0" cellpadding="0" width="100%">
                            <tr bordercolor="#839abf">
                                <td>
                                <table class="box" border="0"  align="center" width="100%">
                                    <tr>
                            <td class="copy10grey" align="left">
                            <asp:Label ID="lblCompany" runat="server" CssClass="copy10grey" Text="Company:" ></asp:Label> </td>
                            <td>
                                
                                <asp:HiddenField ID="hdnUserID" runat="server" />
                                <asp:DropDownList ID="ddlCompany"  AutoPostBack="true" CssClass="copy10grey" 
                                    Width="164px" runat="server" 
                                    onselectedindexchanged="ddlCompany_SelectedIndexChanged">
                                </asp:DropDownList>
                               
                            </td>
                        </tr>                                     
                                     <tr >
                                        <td   class="copy10grey"  align="left"><asp:Label ID="lblrmanumber" runat="server" Text="RMA#:" CssClass="copy10grey"></asp:Label></td>
                                                                                        <td  >
                                <asp:TextBox ID="txtRmaNum" runat="server" 
                                    CssClass="copy10grey" MaxLength="15" Width="166px" ReadOnly="True" 
                                    Enabled="false" />
                            </td>
                                        <td class="copy10grey" ><asp:Label ID="lblRMADate" runat="server" Text="RMA Date:"  CssClass="copy10grey"></asp:Label>
                                            <span Class="errormessage">*</span></td>
                                        <td width="166px">
                                            <asp:Panel ID="rmadtpanel" runat="server">
                                            &nbsp;<asp:TextBox ID="txtRMADate" runat="server"  onfocus="set_focus1();" onkeypress="return doReadonly(event);" 
                                                CssClass="copy10grey" MaxLength="15" />
                                            <img id="img1" alt=""  onclick="document.getElementById('<%=txtRMADate.ClientID%>').value = ''; displayCalendar(document.getElementById('<%=txtRMADate.ClientID%>'),'mm/dd/yyyy',this,true);" src="../../fullfillment/calendar/sscalendar.jpg" />
                                            </asp:Panel>
                                        </td> 
                                        <td class="copy10grey"><span id="status" runat="server">Status:</span></td>
                                        <td Class="copy10grey" Width="166px">
                                            &nbsp;<asp:Label ID="lblStatus" runat="server" Text="Pending" Width="166px"></asp:Label>
                                            <asp:DropDownList ID="ddlStatus"  runat="server" Class="copy10grey" 
                                                Width="166px" onselectedindexchanged="ddlStatus_SelectedIndexChanged">
                                                        <asp:ListItem  Value="0" >------</asp:ListItem>
                                                        <asp:ListItem Value="1" >Pending</asp:ListItem>
                                                        <asp:ListItem  Value="2">Received</asp:ListItem>
                                                        <asp:ListItem  Value="3">Pending for Repair</asp:ListItem>
                                                        <asp:ListItem  Value="4">Pending for Credit</asp:ListItem>
                                                        <asp:ListItem  Value="5">Pending for Replacement</asp:ListItem>
                                                        <asp:ListItem  Value="6">Approved</asp:ListItem>
                                                        <asp:ListItem  Value="7">Cancelled</asp:ListItem>
                                                        <asp:ListItem  Value="8">Returned & Credited</asp:ListItem>
                                                        <asp:ListItem  Value="9">Returned</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>                              
                                    </tr> 
                                    <tr>
                                        <td colspan="6"><hr size="1" width="98%" align="center" /></td>
                                    </tr>
                                    <tr>
                            <td   class="copy10grey"  align="left">Customer Name:&nbsp;<span Class="errormessage">*</span></td>
                            <td colspan="5">
                                <asp:TextBox ID="txtCustName" runat="server" 
                                    CssClass="copy10grey" MaxLength="50" Width="90%" />
                            </td>                            
                        </tr>
                                    <tr>
                            <td   class="copy10grey"  align="left">Address:&nbsp;<span Class="errormessage">*</span></td>
                            <td colspan="5">
                                <asp:TextBox ID="txtAddress" runat="server" CssClass="copy10grey" Width="90%" MaxLength="200"/>
                            </td>                            
                        </tr>
                                    <tr>
                            <td   class="copy10grey"  align="left" width="10%">City:
                            &nbsp;<span Class="errormessage">*</span>
                            </td>
                            <td  width="20%">
                                <asp:TextBox ID="txtCity" runat="server"  Width="80%"  CssClass="copy10grey" MaxLength="50"/>
                            </td>                            
                            <td class="copy10grey"  align="left"  width="10%">State:&nbsp;<span Class="errormessage">*</span></td>
                            <td  width="20%">
                                <asp:TextBox ID="txtState" runat="server"  Width="80%"  CssClass="copy10grey" MaxLength="30" />
                             </td>
                             <td class="copy10grey"  align="left"  width="10%">Zip:&nbsp;<span Class="errormessage">*</span></td>    
                              <td  width="20%">  <asp:TextBox ID="txtZip" runat="server" Width="80%"  CssClass="copy10grey" MaxLength="5"/>
                            </td>  
                        </tr>
              
                                    <tr>
                            <td   class="copy10grey"  align="left">Email:&nbsp;<span Class="errormessage">*</span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtEmail" Width="80%" runat="server" CssClass="copy10grey" MaxLength="50"/>
                            </td>                            
                            <td class="copy10grey"  align="left">Phone:&nbsp;<span Class="errormessage">*</span></td>
                            <td>
                                <asp:TextBox ID="txtPhone"  Width="80%" runat="server" CssClass="copy10grey" MaxLength="12" />
                             </td>                            
                        </tr>
                                    <tr valign="top">
                            <td   class="copy10grey" align="left">Comments:</td>
                            <td colspan="2">
                                <asp:TextBox runat="server" ID="txtRemarks" TextMode="MultiLine"
                                    Rows="3" Columns="50" CssClass="copy10grey" 
                                    onkeypress="return isMaxLength(this);"/>
                            </td>
                            <div runat="server" id="divAvc">
                                <td   class="copy10grey" align="left">Aerovoice Comments:</td>
                                <td colspan="2">
                                    <asp:TextBox runat="server" ID="txtAVComments" TextMode="MultiLine"
                                        Rows="3" Columns="50" CssClass="copy10grey" 
                                        onkeypress="return isMaxLength(this);" Width="90%" />
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
          
                    <tr><td class="copy10grey" align="left">
         <asp:Panel ID="UploadPanel" runat="server">
            <table border="0" width="80%">
            <tr>
                <td class="copy10grey" align="left">
                Please upload an excel format file(with extension .xls) having column with heading ESN:<br />
                 Bulk Upload of RMA:
                </td>
            </tr>
            <tr>
                <td class="copy10grey" align="left">
                <table cellspacing="0" cellpadding="0" border="0"  width="95%" align="center">
                <tr>
                    <td>
                        <asp:FileUpload ID="rmaupload" runat="server" onclick="return checkcompany();" onchange="displayUpload();" Width="275px" CssClass="copy10grey" />
                    </td>
                    <td>
                        <asp:Button ID="btnUploadRMA" Text="Upload" runat="server" CssClass="buybt" 
                            OnClick="btnUploadRMA_click"  CausesValidation="false" /> 
                    </td>
                    <td>
                        <asp:Button ID="btnCancelUpload" Text="Cancel" runat="server" CssClass="buybt" 
                            OnClientClick="emptyRmafile();" OnClick="btnCancelUpload_click"  
                            CausesValidation="false" />
                    </td>
                    
                </tr>
                </table>
                                              
                
                </td>
                
            </tr>
            
            </table>
               <br />
                
                
                
                 </asp:Panel>                  
            </td>
         </tr>
         
                    <tr>
            <td align="center"> 
            
                <table id="rmadetail"  cellspacing="0" cellpadding="0" width="100%" style="text-align: center" >                
                    <tr>                 
                        <td align="left">
                        <asp:Panel ID="POpanel" runat="server"  >
                         <table id="Table1"  bordercolor="#839abf" border="1" cellspacing="0" cellpadding="0" width="100%">                
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
                            </table>
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
                                <td colspan="2" align="left">
                                    <asp:Label ID="lbl_msg" runat="server" CssClass="errormessage"></asp:Label> 
                                </td>
                            </tr>
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
                             <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager> 
            
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
                                            
                                            <tr valign="top">
                                                <td class="button" Width="2%">&nbsp;</td>
                                                <td class="button" Width="15%" align="left">ESN</td>
                                                <td class="button" Width="15%"  align="left">UPC</td>
                                                <td class="button" Width="15%"  align="left">AVSO#</td>
                                                <td class="button" Width="8%"  align="left" id="tdCallTime">Call Time</td>
                                                <td class="button" Width="10%"  align="left" id="tdReason">Reason</td>                                             
                                                <td class="button" Width="20%"  align="left" id="tdnotes">Notes</td>
                                                <td  Width="20%" class="button"> 
                                                    <asp:Label ID="lblStatusheader" runat="server" Text="Status"></asp:Label>
                                                 </td>
                                                
                                            </tr>
                                           <%-- </table>--%>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                
                                               <%-- <table  border="2" cellspacing="2" cellpadding="2" width="100%" align="center">--%>
                                                    <tr id="trCallTimerow" valign="bottom">
                                                        <td >
                                                            <asp:CheckBox ID="chkESN" Enabled="false" runat="server" />
                                                        </td>
                                                        <td  >
                                                            <asp:HiddenField ID="hdnRMADetGUID" Value='<%# Eval("rmaDetGUID") %>' runat="server" />
                                                            <asp:HiddenField ID="hdnPOID" Value='<%#Eval("po_id")%>' runat="server" />
                                                         <asp:TextBox ID="txt_ESN" Text = '<%# Eval("esn") %>'  runat="server"  Width="95%" onkeypress="return alphaNumericCheck(event);"  
                                                                      AutoPostBack="true" ontextchanged="ESN_TextChanged" MaxLength="30" CssClass="esntext" ></asp:TextBox>
                                                                
                                                         </td>

                                                        <td  >
                                                            <asp:Label ID="lblinvalidESN" CssClass ="errormessage" runat="server" ></asp:Label>
                                                            <asp:Label ID="lblpod_ID" Text = '<%# Eval("pod_id") %>'  Visible="false" runat="server"></asp:Label>
                                                            <asp:Label ID="lblItemCode" Text = '<%# Eval("UPC") %>'  CssClass="copy10grey" Width="100%" runat="server"></asp:Label>
                                                        </td>
                                                        <td  >
                                                            <asp:Label ID="lblAvso" Text = '<%# Eval("AVSalesOrderNumber") %>'  CssClass="copy10grey" Width="100%" runat="server"></asp:Label>                                                            
                                                        </td>  
                                                        <td >
                                                            <asp:TextBox ID="txtCallTime"  AutoPostBack="true" Width="80%" Text='<%# Eval("CallTime") %>' ontextchanged="CallTime_TextChanged" onkeypress="return isNumberKey(event);"  CssClass="copy10grey" runat="server" MaxLength="3"></asp:TextBox>
                                                            <asp:HiddenField id="hdnPod_id" runat="server" Value='<%# Eval("pod_id") %>'/>
                                                          
                                                        </td>
                                                        
                                                          <td >
                                                              <asp:DropDownList ID="ddReason" CssClass="copy10grey" runat="server" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="Reason_OnChanged" >
                                                              </asp:DropDownList>
                                                              <asp:HiddenField id="hdnReason" runat="server" Value='<%# Eval("Reason") %>'/>
                                                            </td>
                                                            
                                                            <td >
                                                                <asp:TextBox ID="txtNotes" AutoPostBack="true"  ontextchanged="Notes_TextChanged"  onkeypress="return isMaxLength(this);"  Text='<%# Eval("Notes") %>'  Width="95%" CssClass="copy10grey" runat="server" MaxLength="1000" TextMode="MultiLine"></asp:TextBox>
                                                           </td>
                                                           <td >
                                                                 <asp:DropDownList ID="ddl_Status" OnSelectedIndexChanged="Status_OnChanged" runat="server" Class="copy10grey">
                                                                    <asp:ListItem  Value="0" >------</asp:ListItem>
                                                                    <asp:ListItem Value="1" Selected="True" >Pending</asp:ListItem>
                                                                    <asp:ListItem  Value="2">Pending for Repair</asp:ListItem>
                                                                    <asp:ListItem  Value="3">Pending for Credit</asp:ListItem>
                                                                    <asp:ListItem  Value="4">Pending for Replacement</asp:ListItem>
                                                                    <asp:ListItem  Value="5">Approved</asp:ListItem>
                                                                    <asp:ListItem  Value="6">Cancelled</asp:ListItem>
                                                                    <asp:ListItem  Value="7">Returned</asp:ListItem>
                                                                    <asp:ListItem  Value="8">Returned & Credited</asp:ListItem>
                                                                    <asp:ListItem  Value="9">Returned</asp:ListItem>
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

            
            <tr><td>&nbsp;</td></tr>
            <tr><td>&nbsp;</td></tr>
            <tr>
                <td><foot:MenuFooter id="Footer" runat="server"></foot:MenuFooter></td>
            </tr>
            </table>
        </div>
 
    <script type="text/javascript">
        generateRMADetail();
        
        var userid = document.getElementById('hdnUserID').value;
        

        var mode = getQuerystring('mode');
        if ('import' != mode)
            hideEsn();
        
        if ('import' == mode) {

            var UploadRMA = document.getElementById("<%= btnUploadRMA.ClientID %>");
            var UploadRMADiv = document.getElementById('btnUploadRMA');

            UploadRMADiv.style.display = 'none';

        }

    </script>
    </form>
</body>
</html>
