<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="customer-Form.aspx.cs" Inherits="avii.Admin.customer_Form"  ValidateRequest="false" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="/Controls/Header.ascx" %>
<html >
<head runat="server">
    <title>.:: Company Form ::.</title>
      <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>

    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.4.0/jquery.min.js"></script>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/jquery-ui.min.js"></script>
    <link href="https://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/themes/start/jquery-ui.css"
    rel="stylesheet" type="text/css" />
<%--<script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/jquery-ui.js" type="text/javascript"></script>
<link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/themes/start/jquery-ui.css"
    rel="stylesheet" type="text/css" />--%>
	<%--<script type="text/javascript" src="../JQuery/jquery.min.js"></script>
    <script type="text/javascript" src="../JQuery/jquery-ui.min.js"></script>--%>
	<script type="text/javascript" src="../JQuery/jquery.blockUI.js"></script>
	
     <link href="/aerostyle.css" rel="stylesheet" type="text/css"/>
     <link href="/product/ddcolortabs.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript" src="/product/ddtabmenu.js"></script>
    

    <link rel="stylesheet" href="/dhtmlxwindow/style.css" type="text/css" media="screen" />
    <link rel="stylesheet" type="text/css" href="/dhtmlxwindow/dhtmlxwindows.css"/>
	<link rel="stylesheet" type="text/css" href="/dhtmlxwindow/skins/dhtmlxwindows_dhx_skyblue.css"/>
	
	<script src="/dhtmlxwindow/dhtmlxcommon.js"></script>
	<script src="/dhtmlxwindow/dhtmlxwindows.js"></script>
	<script src="/dhtmlxwindow/dhtmlxcontainer.js"></script>
    <script>
        function IsValidUrl(url) {

            var myVariable = url;
            if (/^(http|https|ftp):\/\/[a-z0-9]+([\-\.]{1}[a-z0-9]+)*\.[a-z]{2,5}(:[0-9]{1,5})?(\/.*)?$/i.test(myVariable)) {
                return 1;
            } else {
                return -1;
            }
        }

        function ValidateURL(obj) {
            if (IsValidUrl(obj.value) == -1) {
                alert("Invalid url!");
            }
        }
        $(document).ready(function () {  
            $('inputssss').keyup(function () {
                var $th = $(this);

                if (IsValidUrl($th.val()) == -1) {
                    alert("Invalid url!");
                }

            });
            
            $(".alphanumeric").keypress(function (e) {
                var regex = new RegExp("^[a-zA-Z0-9 ]+$");
                var str = String.fromCharCode(!e.charCode ? e.which : e.charCode);
                if (regex.test(str)) {
                    return true;
                }
                else {
                    e.preventDefault();
                    return false;
                }
            });
            $(".alphanumericonly").keypress(function (e) {
                var regex = new RegExp("^[a-zA-Z0-9]+$");
                var str = String.fromCharCode(!e.charCode ? e.which : e.charCode);
                if (regex.test(str)) {
                    return true;
                }
                else {
                    e.preventDefault();
                    return false;
                }
            });

            $(".numericcss").keypress(function (e) {
                var regex = new RegExp("^[0-9]+$");
                var str = String.fromCharCode(!e.charCode ? e.which : e.charCode);
                if (regex.test(str)) {
                    return true;
                }
                else {
                    e.preventDefault();
                    return false;
                }
            });

            $('.addresscss').keypress(function (e) {
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
            $('.phonecss').keypress(function (e) {
                var regex = new RegExp("^[0-9\-\(\)\s]+$");
                var str = String.fromCharCode(!e.charCode ? e.which : e.charCode);
                //alert(str);
                if (regex.test(str)) {
                    return true;
                }
                e.preventDefault();
                return false;
            });
            
        });
    </script>
     <script language="javascript" type="text/javascript">
         function fileValidation() {
             var fileInput = document.getElementById('fuLogo');
             var filePath = fileInput.value;
             var allowedExtensions = /(\.jpg|\.jpeg|\.png|\.gif)$/i;
             if (!allowedExtensions.exec(filePath)) {
                 alert('Please upload file having extensions .jpeg/.jpg/.png/.gif only.');
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
         function ValidateEmail(obj) {
         var EmaiAddress = obj.value;

             var RegExEmail = /^(?:\w+\.?)*\w+@(?:\w+\.)+\w+$/;
             if (obj.value != '') {
                 if (!RegExEmail.test(EmaiAddress)) {
                     obj.focus();
                     alert("Invalid E-mail");
                     return false;
                 }
             }
             
         } 
         function clickHandler() {
             obj = event.srcElement;

             //alert(obj);
             //alert(obj.value);
             if (obj != null) {
                 //alert(obj.id);
                 if (obj.id == 'tab1') {
                     document.getElementById("<%= hdnTabIndex.ClientID %>").value = 0;

                 }
                 else if (obj.id == 'tab2') {
                     document.getElementById("<%= hdnTabIndex.ClientID %>").value = 1;

                 }
                 else if (obj.id == 'tab3') {
                     document.getElementById("<%= hdnTabIndex.ClientID %>").value = 2;

                 }


             }
             
         }
         document.onclick = clickHandler;
         function SetTabIndex(indexValue) {


             document.getElementById("<%= hdnTabIndex.ClientID %>").value = indexValue;
             alert(document.getElementById("<%= hdnTabIndex.ClientID %>").value);
         }
        function getShippingAddress(obj)
        {
         if(obj.checked)
         {
            document.getElementById("<%=txtShippContactName.ClientID %>").value = document.getElementById("<%=txtOfficeContactName.ClientID %>").value;
            document.getElementById("<%=txtShippAdd1.ClientID %>").value = document.getElementById("<%=txtOfficeAdd1.ClientID %>").value;
            document.getElementById("<%=txtShippAdd2.ClientID %>").value = document.getElementById("<%=txtOfficeAdd2.ClientID %>").value;
            document.getElementById("<%=txtShippOfficePhone1.ClientID %>").value = document.getElementById("<%=txtOfficePhone1.ClientID %>").value;
            document.getElementById("<%=txtShippOfficePhone2.ClientID %>").value = document.getElementById("<%=txtOfficePhone2.ClientID %>").value;
            document.getElementById("<%=txtShippCellPhone.ClientID %>").value = document.getElementById("<%=txtCellPhone.ClientID %>").value;
            document.getElementById("<%=txtShippHomePhone.ClientID %>").value = document.getElementById("<%=txtHomePhone.ClientID %>").value;
            document.getElementById("<%=txtShippCity.ClientID %>").value = document.getElementById("<%=txtOfficeCity.ClientID %>").value;
document.getElementById("<%=dpShipState.ClientID %>").selectedIndex = document.getElementById("<%=dpOfficeState.ClientID %>").selectedIndex;

            document.getElementById("<%=txtShippZip.ClientID %>").value = document.getElementById("<%=txtOfficeZip.ClientID %>").value;
            document.getElementById("<%=txtShippEmail.ClientID %>").value = document.getElementById("<%=txtOfficeEmail1.ClientID %>").value;
            document.getElementById("<%=txtShippEmail2.ClientID %>").value = document.getElementById("<%=txtOfficeEmail2.ClientID %>").value;
            document.getElementById("<%=txtShipCountry.ClientID %>").value = document.getElementById("<%=txtOfficeCountry.ClientID %>").value;
        }
        else
        {
            document.getElementById("<%=txtShippContactName.ClientID %>").value = "";
            document.getElementById("<%=txtShippAdd1.ClientID %>").value = "";
            document.getElementById("<%=txtShippAdd2.ClientID %>").value = "";
            document.getElementById("<%=txtShippOfficePhone1.ClientID %>").value = "";
            document.getElementById("<%=txtShippOfficePhone2.ClientID %>").value = "";
            document.getElementById("<%=txtShippCellPhone.ClientID %>").value = "";
            document.getElementById("<%=txtShippHomePhone.ClientID %>").value = "";
            document.getElementById("<%=txtShippCity.ClientID %>").value = "";
document.getElementById("<%=dpShipState.ClientID %>").selectedIndex = 0;
            document.getElementById("<%=txtShippZip.ClientID %>").value = "";
            document.getElementById("<%=txtShippEmail.ClientID %>").value = "";
            document.getElementById("<%=txtShippEmail2.ClientID %>").value = "";
            document.getElementById("<%=txtShipCountry.ClientID %>").value = "";
        }
                                
                                
                                
                                
        }
        function IsValidate()
        {
            
            var CompanyName = document.getElementById("txtCompanyName");
            var email = document.getElementById("txtEmail");
            var ShortName = document.getElementById("txtShortName");
            
            var CompanyAccount = document.getElementById("txtCompanyAccount");
            
            
            if (CompanyName.value == "") {
                alert("CompanyName can not be blank");
                CompanyName.focus();
                return false;
            }
            if (email.value == "") {
                alert("Email can not be blank");
                email.focus();
                return false;
            }
            if (ShortName.value == "") {
                alert("Short Name can not be blank");
                ShortName.focus();
                return false;
            }
            if (CompanyAccount.value == "") {
                
                alert("Company Account# can not be blank");
                CompanyAccount.focus();
                return false;
            }
            
            
            if(document.getElementById("<%=txtOfficeContactName.ClientID %>").value=="")
             {
             alert("Contact Name can not be blank");
             document.getElementById("<%=txtOfficeContactName.ClientID %>").focus();
                return false;
             }
             if(document.getElementById("<%=txtOfficeAdd1.ClientID %>").value=="")
             {
                 alert("Address1 can not be blank");
                document.getElementById("<%=txtOfficeAdd1.ClientID %>").focus();
                return false;
             }
             
             
             if(document.getElementById("<%=txtOfficePhone1.ClientID %>").value=="")
             {
                 alert("Phone can not be blank");
                document.getElementById("<%=txtOfficePhone1.ClientID %>").focus();
                return false;
             }
             if(document.getElementById("<%=txtOfficeCity.ClientID %>").value=="")
             {
                 alert("City can not be blank");
                document.getElementById("<%=txtOfficeCity.ClientID %>").focus();
                return false;
             }
            if (document.getElementById("<%=dpOfficeState.ClientID %>").value == "")
             {
                 alert("State can not be blank");
                document.getElementById("<%=dpOfficeState.ClientID %>").focus();
                return false;
             }

             
             if(document.getElementById("<%=txtOfficeZip.ClientID %>").value=="")
             {
                alert("Zip can not be blank")
                document.getElementById("<%=txtOfficeZip.ClientID %>").focus();
                return false;
             }
             if(document.getElementById("<%=txtOfficeEmail1.ClientID %>").value=="")
             {
                alert("Email can not be blank")
                document.getElementById("<%=txtOfficeEmail1.ClientID %>").focus();
                return false;
             }
             
             
            if(document.getElementById("<%=txtShippContactName.ClientID %>").value =="")
            {
                alert("Contact Name can not be blank");
                document.getElementById("<%=txtShippContactName.ClientID %>").focus();
                return false;
            }
            
             
             if(document.getElementById("<%=txtShippAdd1.ClientID %>").value=="")
             {
                 alert("Address1 can not be blank");
                document.getElementById("<%=txtShippAdd1.ClientID %>").focus();
                return false;
             }
             
             
             if(document.getElementById("<%=txtShippOfficePhone1.ClientID %>").value=="")
             {
                 alert("Phone can not be blank");
                document.getElementById("<%=txtShippOfficePhone1.ClientID %>").focus();
                return false;
             }
             
             
             if(document.getElementById("<%=txtShippCity.ClientID %>").value=="")
             {
                 alert("City can not be blank");
                document.getElementById("<%=txtShippCity.ClientID %>").focus();
                return false;
             }

            if (document.getElementById("<%=dpShipState.ClientID %>").value == "")
             {
                 alert("State can not be blank");
                document.getElementById("<%=dpShipState.ClientID %>").focus();
                return false;
             }
             
             if(document.getElementById("<%=txtShippZip.ClientID %>").value=="")
             {
                 alert("Zip can not be blank");
                document.getElementById("<%=txtShippZip.ClientID %>").focus();
                return false;
             }
             
             
             if(document.getElementById("<%=txtShippEmail.ClientID %>").value=="")
             {
                 alert("Email can not be blank");
                document.getElementById("<%=txtShippEmail.ClientID %>").focus();
                return false;
            }

            var ctrls = document.getElementsByTagName('input');
            for (var i = 0; i < ctrls.length; i++) {
                var obj = ctrls[i];

                if (obj.type == "text") {
                    if ((obj.id.indexOf('txt_Email') > -1) || (obj.id.indexOf('txtOvrdEmail')) > -1) {
                        var EmaiAddress = obj.value;
                        var RegExEmail = /^(?:\w+\.?)*\w+@(?:\w+\.)+\w+$/;
                        if (obj.value != '') 
                        {
                            if (!RegExEmail.test(EmaiAddress)) {
                                obj.focus();
                                alert("Invalid E-mail");
                                return false;
                            }
                        }
                    }

                }
            }
                         
             return true;
        }
       
        
     </script>
    <style type="text/css">
        .style1
        {
            width: 326px;
        }
    </style>
</head>

<body bgColor="#ffffff" leftmargin="0" rightmargin="0" topmargin="0"  onkeydown="if (event.keyCode==13) {event.keyCode=9; return event.keyCode }">
    <form id="form1" runat="server">
    <table cellSpacing="0" cellPadding="0"  border="0" align="center" width="100%">
				<tr>
					<td>
					<head:MenuHeader ID="menuheader" runat="server"></head:MenuHeader>
                	</td>
				</tr>
			</table>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
    
			<table   width="95%" align="center">
                <tr>
			        <td  bgcolor="#dee7f6" class="buttonlabel">&nbsp;&nbsp;
			            <asp:Label ID="lblHeader" runat="server" CssClass="buttonlabel" BorderWidth="0" Text="MANAGE CUSTOMERS" ></asp:Label>
			        </td>
                </tr>
                
               <tr><td><asp:Label ID="lblMsg" runat="server" CssClass="errormessage" ></asp:Label>
               <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" CssClass="errormessage"
                ControlToValidate="txtEmail" ErrorMessage="RegularExpressionValidator" 
                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">Invalid Email!!!</asp:RegularExpressionValidator>
               
               <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" CssClass="errormessage"
                ControlToValidate="txtGroupEmail" ErrorMessage="RegularExpressionValidator" 
                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">Invalid Email!!!</asp:RegularExpressionValidator>
               
               </td></tr>
            </table>                      
            
			
            
                        
            <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="95%" align="center" >
                <tr bordercolor="#839abf">
                    <td >
                        
                        <table width="100%" cellSpacing="5" cellPadding="5">
                        <tr>
                            <td class="copy10grey" width="15%" >
                             Company Name: &nbsp;<span class="errormessage">*</span></td>
                            <td class="style1" width="35%">
                                <asp:TextBox ID="txtCompanyName"  MaxLength="50" Width="80%" TabIndex="1" 
                                    CssClass="copy10grey alphanumeric" runat="server"></asp:TextBox>
                            </td>
                            <td class="copy10grey"  width="15%">
                                Company Code:&nbsp;<span class="errormessage">*</span></td>
                            <td width="35%">
                                <asp:TextBox ID="txtShortName"   MaxLength="3" CssClass="copy10grey alphanumericonly" TabIndex="2" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        
                        
                        <tr>
                            <td class="copy10grey" width="130" >
                               Company Account #: &nbsp;<span class="errormessage">*</span>
                            </td>
                            <td class="style1">
                                <asp:TextBox ID="txtCompanyAccount"  MaxLength="50" Width="80%" TabIndex="3" 
                                    CssClass="copy10grey alphanumericonly" runat="server"></asp:TextBox>
                            </td>
                            <td class="copy10grey">
                                Company A/C Status:</td>
                            <td>
                            <asp:DropDownList ID="ddlStatus" TabIndex="4"  CssClass="copy10grey" runat="server">
                            <asp:ListItem Text="Pending" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Approved" Value="2"></asp:ListItem>
                            <asp:ListItem Text="Cancelled" Value="3"></asp:ListItem>
                            <asp:ListItem Text="AllowChanges" Value="4"></asp:ListItem>
                            <asp:ListItem Text="Inactive" Value="5"></asp:ListItem>
                            </asp:DropDownList>
                            </td>
                        </tr>
                         <tr style="display:none">
                            
                            <td class="copy10grey" width="130" >
                               Bussiness Type:
                            </td>
                            <td class="style1">
                                <asp:TextBox ID="txtBDAName"  MaxLength="50" Width="80%" TabIndex="5" 
                                    CssClass="copy10grey" runat="server"></asp:TextBox>
                            </td>
                            <td class="copy10grey" >
                                Website:</td>
                            <td width="20%">
                                <asp:TextBox ID="txtWebsite"  Width="80%" MaxLength="150" CssClass="copy10grey" 
                                    TabIndex="6" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <%--<tr>
                            <td class="copy10grey" width="103" >
                               Bussiness Type:</td>
                            <td class="style1">
                                <asp:TextBox ID="txtBusinessType"  MaxLength="150" Width="80%" TabIndex="7" 
                                    CssClass="copy10grey" runat="server"></asp:TextBox>
                            </td>
                            
                            <td class="copy10grey" >
                                
                            </td>
                            <td width="20%">
                                <asp:CheckBox ID="chkActive" Text="Active" TabIndex="8" CssClass="copy10grey"  runat="server" />
                            </td>
                        </tr>--%>
                        <tr>
                            <td class="copy10grey" >
                                Email: &nbsp;<span class="errormessage">*</span></td>
                            <td class="style1">
                                <asp:TextBox ID="txtEmail" Width="80%"  MaxLength="150" TabIndex="9" 
                                    CssClass="copy10grey" runat="server"></asp:TextBox>
                            </td>
                            
                            <td class="copy10grey" >
                                <%--Sales Person:--%>
                                <%--Is Email:--%>
                            </td>
                            <td  width="30%">
                                <asp:CheckBox ID="chkEmail" Visible="false" CssClass="copy10grey" runat="server" TabIndex="12"  />
                                <%--<asp:ListBox ID="lbSalesPerson" TabIndex="10" CssClass="copy10grey" runat="server" Width="90%" SelectionMode="Multiple"></asp:ListBox>
                            --%>
                            </td>
                        </tr>
                        <tr style="display:none">
                            <td class="copy10grey" >
                                Group Email:</td>
                            <td class="style1">
                                <asp:TextBox ID="txtGroupEmail" Width="80%"  MaxLength="150" TabIndex="11" 
                                    CssClass="copy10grey" runat="server"></asp:TextBox>
                            </td>
                            
                            <td >
                                
                            </td>
                            <%--<td>
                            </td>--%>
                        </tr>
                        <tr style="display:none">
                            <td class="copy10grey" >
                             
                            </td>
                            <td>
                                
                            </td>
                            <td class="copy10grey" >
                            Pricing Type:
                            </td>
                            <td>
                             <asp:DropDownList ID="ddlPriceType" TabIndex="13"  CssClass="copy10grey" runat="server">                            
                            </asp:DropDownList>
                            </td>
                        </tr>
                            <tr>
                                <td class="copy10grey" >
                                    Logo:
                                </td>
                                <td>
                                    <asp:FileUpload ID="fuLogo" runat="server" Width="80%" onchange="return fileValidation();" />
                                </td>
                                <td>

                                </td>
                                <td>

                                </td>
                            </tr>
                        </table>
                    </td>
                 </tr>
             </table>  
             <%--<br />
             <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="95%" align="center" >
                <tr bordercolor="#839abf">
                    <td >
                 <table width="100%"  cellSpacing="3" cellPadding="3">
                <tr>
                    <td class="copy10grey">
                    
                        

                    </td>
                </tr>
                </table>
            </td>
            </tr>
            </table>  
            --%>
             <br />
             <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="95%" align="center" >
                <tr bordercolor="#839abf">
                    <td >
             <table width="100%"  >
            <tr>
                <td class="copy10grey">
                    Comments:<br />
                    <asp:TextBox CssClass="copy10grey" TabIndex="37" ID="txtComment" Width="91%" TextMode="MultiLine" Height="40" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="copy10grey">
                    &nbsp;</td>
            </tr>
            </table>
            </td>
            </tr>
            </table>  
            <br />
             <table  bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0"   width="95%" align="center" >
             <tr>
                <td>
                
                <asp:HiddenField ID="hdnTabIndex" runat="server" />
             
             <div id="ddtabs4" class="ddcolortabs">
                    <ul>
                    <li id="tab1"><a href="#" rel="ct1" id="imagetab"><span id="tab1">Addresses</span></a></li>
                    <li id="tab2"><a href="#" rel="ct2"><span id="tab2">Store Location</span></a></li>
                    <li id="tab3"><a href="#" rel="ct3"><span id="tab3">Customer Email</span></a></li>
                    <li id="tab4"><a href="#" rel="ct4"><span id="tab4">Customer Warehouse Code</span></a></li>
                    <li id="tab5"><a href="#" rel="ct5"><span id="tab5">Integration</span></a></li>
                    </ul>
                    </div>
                    <div class="ddcolortabsline" >&nbsp;</div>

                    
                
                    <div id="allTabContnt"  >
                
                    <div id="ct1" class="tabcontent" style="border:0px solid #666666" >
                    
                   
             
             
             <table bordercolor="#839abf" border="0" cellSpacing="0" cellPadding="0"   width="100%" align="center" >
                <tr>
                <td>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" CssClass="errormessage"
                                ControlToValidate="txtOfficeEmail1" ErrorMessage="RegularExpressionValidator" 
                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">Invalid Email!!!</asp:RegularExpressionValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" CssClass="errormessage"
                                ControlToValidate="txtOfficeEmail2" ErrorMessage="RegularExpressionValidator" 
                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">Invalid Email!!!</asp:RegularExpressionValidator>
                
                </td>
                <td>
                </td>
                <td>
                    <asp:CheckBox ID="chkSameAddess" Text ="Same as corporate office" CssClass="copy10grey" onclick="getShippingAddress(this);" runat="server" />
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" CssClass="errormessage"
                                ControlToValidate="txtShippEmail" ErrorMessage="RegularExpressionValidator" 
                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">Invalid Email!!!</asp:RegularExpressionValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" CssClass="errormessage"
                                ControlToValidate="txtShippEmail2" ErrorMessage="RegularExpressionValidator" 
                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">Invalid Email!!!</asp:RegularExpressionValidator>
                
                </td>
                </tr>
                <tr >
			        <td  width="38%" bgcolor="#dee7f6" class="buttonlabel">&nbsp;&nbsp;
			        Corporate Office
			        </td>
			        <td width="4%"></td>          
			        <td width="38%" bgcolor="#dee7f6" class="buttonlabel">
			        &nbsp;&nbsp;Shipping Address
			        </td>
                </tr>
                
               
            </table>    
            <table bordercolor="#839abf" border="0" cellSpacing="0" cellPadding="0" width="100%" align="center" >
                <tr bordercolor="#839abf">
                    <td >
                    <table width="100%">
                    <tr>
                        <td width="48%" >
                            <table width="100%" >
                            <tr>
                                <td class="copy10grey">
                                    Contact Name:&nbsp;<span class="errormessage">*</span></td>
                                <td>
                                    <asp:TextBox ID="txtOfficeContactName"  MaxLength="50" TabIndex="13" Width="200" CssClass="copy10grey alphanumeric" runat="server"></asp:TextBox>
                                </td>
                            </tr>  
                            
                            <tr>
                                <td class="copy10grey">
                                Address 1:<span class="errormessage">*</span>
                                </td>
                                <td>
                                <asp:TextBox ID="txtOfficeAdd1" Width="200"  MaxLength="50" CssClass="copy10grey addresscss" TabIndex="14" runat="server"></asp:TextBox>
                                </td>
                            </tr>    
                            <tr>
                                <td class="copy10grey">
                                Address 2:</td>
                                <td>
                                    <asp:TextBox ID="txtOfficeAdd2" Width="200"  MaxLength="50" TabIndex="15" CssClass="copy10grey addresscss" runat="server"></asp:TextBox>
                                </td>
                            </tr>    
                            <tr>
                                <td class="copy10grey">
                                Office Phone 1:<span class="errormessage">*</span></td>
                                <td>
                                    <asp:TextBox ID="txtOfficePhone1" Width="200"  MaxLength="15" TabIndex="16" CssClass="copy10grey phonecss" runat="server"></asp:TextBox>
                                </td>
                            </tr>   
                            <tr>
                                <td class="copy10grey">
                                Office Phone 2:</td>
                                <td>
                                    <asp:TextBox ID="txtOfficePhone2" Width="200"  MaxLength="15" TabIndex="17" CssClass="copy10grey phonecss" runat="server"></asp:TextBox>
                                </td>
                            </tr>    
                            <tr>
                                <td class="copy10grey">
                                Cell Phone:</td>
                                <td>
                                    <asp:TextBox ID="txtCellPhone" Width="200"  MaxLength="15" TabIndex="18" CssClass="copy10grey phonecss" runat="server"></asp:TextBox>
                                </td>
                            </tr>   
                            <tr>
                                <td class="copy10grey">
                                Home Phone:</td>
                                <td>
                                    <asp:TextBox ID="txtHomePhone" Width="200"  MaxLength="15" TabIndex="19" CssClass="copy10grey phonecss" runat="server"></asp:TextBox>
                                </td>
                            </tr>    
                            <tr>
                                <td class="copy10grey">
                                    City: <span class="errormessage">*</span></td>
                                <td>
                                    <asp:TextBox ID="txtOfficeCity" Width="200"  MaxLength="50" TabIndex="20" CssClass="copy10grey alphanumeric" runat="server"></asp:TextBox>
                                </td>
                            </tr>    
                            <tr>
                                <td class="copy10grey">
                                State:<span class="errormessage">*</span></td>
                                <td>
                                <asp:dropdownlist id="dpOfficeState" TabIndex="21" runat="server" cssclass="copy10grey" Width="201">
																	<asp:ListItem></asp:ListItem>
                                                                    <asp:ListItem Value="AL">AL - Alabama</asp:ListItem>
																	<asp:ListItem Value="AK">AK - Alaska</asp:ListItem>
																	<asp:ListItem Value="AZ">AZ - Arizona</asp:ListItem>
																	<asp:ListItem Value="AR">AR - Arkansas</asp:ListItem>
																	<asp:ListItem Value="CA" >CA - California</asp:ListItem>
																	<asp:ListItem Value="CO">CO - Colorado</asp:ListItem>
																	<asp:ListItem Value="CT">CT - Connecticut</asp:ListItem>
																	<asp:ListItem Value="DC">DE - Delaware</asp:ListItem>
																	<asp:ListItem Value="DE">DC - District of Columbia</asp:ListItem>
																	<asp:ListItem Value="FL">FL - Florida</asp:ListItem>
																	<asp:ListItem Value="GA">GA - Georgia</asp:ListItem>
																	<asp:ListItem Value="HI">HI - Hawaii</asp:ListItem>
																	<asp:ListItem Value="ID">ID - Idaho</asp:ListItem>
																	<asp:ListItem Value="IL">IL - Illinois</asp:ListItem>
																	<asp:ListItem Value="IN">IN - Indiana</asp:ListItem>
																	<asp:ListItem Value="IA">IA - Iowa</asp:ListItem>
																	<asp:ListItem Value="KS">KS - Kansas</asp:ListItem>
																	<asp:ListItem Value="KY">KY - Kentucky</asp:ListItem>
																	<asp:ListItem Value="LA">LA - Louisiana</asp:ListItem>
																	<asp:ListItem Value="ME">ME - Maine</asp:ListItem>
																	<asp:ListItem Value="MD">MD - Maryland</asp:ListItem>
																	<asp:ListItem Value="MA">MA - Massachusetts</asp:ListItem>
																	<asp:ListItem Value="MI">MI - Michigan</asp:ListItem>
																	<asp:ListItem Value="MN">MN - Minnesota</asp:ListItem>
																	<asp:ListItem Value="MS">MS - Mississippi</asp:ListItem>
																	<asp:ListItem Value="MO">MO - Missouri</asp:ListItem>
																	<asp:ListItem Value="MT">MT - Montana</asp:ListItem>
																	<asp:ListItem Value="NE">NE - Nebraska</asp:ListItem>
																	<asp:ListItem Value="NV">NV - Nevada</asp:ListItem>
																	<asp:ListItem Value="NH">NH - New Hampshire</asp:ListItem>
																	<asp:ListItem Value="NJ">NJ - New Jersey</asp:ListItem>
																	<asp:ListItem Value="NM">NM - New Mexico</asp:ListItem>
																	<asp:ListItem Value="NY">NY - New York</asp:ListItem>
																	<asp:ListItem Value="NC">NC - North Carolina</asp:ListItem>
																	<asp:ListItem Value="ND">ND - North Dakota</asp:ListItem>
																	<asp:ListItem Value="OH">OH - Ohio</asp:ListItem>
																	<asp:ListItem Value="OK">OK - Oklahoma</asp:ListItem>
																	<asp:ListItem Value="OR">OR - Oregon</asp:ListItem>
																	<asp:ListItem Value="PA">PA - Pennsylvania</asp:ListItem>
																	<asp:ListItem Value="RI">RI - Rhode Island</asp:ListItem>
																	<asp:ListItem Value="SC">SC - South Carolina</asp:ListItem>
																	<asp:ListItem Value="SD">SD - South Dakota</asp:ListItem>
																	<asp:ListItem Value="TN">TN - Tennessee</asp:ListItem>
																	<asp:ListItem Value="TX">TX - Texas</asp:ListItem>
																	<asp:ListItem Value="UT">UT - Utah</asp:ListItem>
																	<asp:ListItem Value="VT">VT - Vermont</asp:ListItem>
																	<asp:ListItem Value="VA">VA - Virginia</asp:ListItem>
																	<asp:ListItem Value="WA">WA - Washington</asp:ListItem>
																	<asp:ListItem Value="WV">WV - West Virginia</asp:ListItem>
																	<asp:ListItem Value="WI">WI - Wisconsin</asp:ListItem>
																	<asp:ListItem Value="WY">WY - Wyoming</asp:ListItem>

                                                                    <asp:ListItem Value="AB">AB - Alberta</asp:ListItem>
																	<asp:ListItem Value="BC">BC - British Columbia</asp:ListItem>
																	<asp:ListItem Value="MB">MB - Manitoba</asp:ListItem>
																	<asp:ListItem Value="NB">NB - New Brunswick</asp:ListItem>
																	<asp:ListItem Value="NL">NL - Newfoundland and Labrador</asp:ListItem>
																	<asp:ListItem Value="NS">NS - Nova Scotia</asp:ListItem>
																	<asp:ListItem Value="NT">NT - Northwest Territories</asp:ListItem>
																	<asp:ListItem Value="NU">NU - Nunavut</asp:ListItem>
																	<asp:ListItem Value="ON">ON - Ontario</asp:ListItem>

                                                                    <asp:ListItem Value="PE">PE - Prince Edward Island</asp:ListItem>
																	<asp:ListItem Value="QC">QC - Quebec</asp:ListItem>
																	<asp:ListItem Value="SK">SK - Saskatchewan</asp:ListItem>
																	<asp:ListItem Value="YT">YT - Yukon</asp:ListItem>

																	<%--<asp:ListItem Value="AL">AL</asp:ListItem>
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
																	<asp:ListItem Value="WY">WY</asp:ListItem>

                                                                    <asp:ListItem Value="AB">AB</asp:ListItem>
																	<asp:ListItem Value="BC">BC</asp:ListItem>
																	<asp:ListItem Value="MB">MB</asp:ListItem>
																	<asp:ListItem Value="NB">NB</asp:ListItem>
																	<asp:ListItem Value="NL">NL</asp:ListItem>
																	<asp:ListItem Value="NS">NS</asp:ListItem>
																	<asp:ListItem Value="NT">NT</asp:ListItem>
																	<asp:ListItem Value="NU">NU</asp:ListItem>
																	<asp:ListItem Value="ON">ON</asp:ListItem>

                                                                    <asp:ListItem Value="PE">PE</asp:ListItem>
																	<asp:ListItem Value="QC">QC</asp:ListItem>
																	<asp:ListItem Value="SK">SK</asp:ListItem>
																	<asp:ListItem Value="YT">YT</asp:ListItem>--%>
																</asp:dropdownlist>
                                <%--<asp:dropdownlist id="dpState"  runat="server"  Width="202" cssclass="copy10grey">
                                </asp:dropdownlist>--%>
                                <%--<asp:TextBox ID="txtOfficeState" Width="200"  MaxLength="2" TabIndex="21" CssClass="copy10grey" runat="server"></asp:TextBox>--%>
                                
                                </td>
                            </tr>    
                            <tr>
                                <td class="copy10grey">
                                Zip:<span class="errormessage">*</span></td>
                                <td>
                                    <asp:TextBox ID="txtOfficeZip" Width="200"  MaxLength="5" TabIndex="22" CssClass="copy10grey numericcss" runat="server"></asp:TextBox>
                                </td>
                            </tr>    
                            
                            <tr>
                                <td class="copy10grey">
                                Email 1:<span class="errormessage">*</span></td>
                                <td>
                                <asp:TextBox ID="txtOfficeEmail1" Width="200"  MaxLength="50" TabIndex="23" CssClass="copy10grey" runat="server"></asp:TextBox>
                                 
                                </td>
                            </tr>    
                            <tr>
                                <td class="copy10grey">
                                Email 2:</td>
                                <td>
                                <asp:TextBox ID="txtOfficeEmail2" TabIndex="24" MaxLength="50"  Width="200" CssClass="copy10grey" runat="server"></asp:TextBox>
                                </td>
                            </tr> 
                            <tr>
                                <td class="copy10grey">
                                Country:</td>
                                <td>
                                <asp:TextBox ID="txtOfficeCountry" TabIndex="25" MaxLength="100"  Width="200" CssClass="copy10grey alphanumeric" runat="server"></asp:TextBox>
                                </td>
                            </tr>   
                            </table>
                        </td>
                        <td width="4%">
                        </td>
                        <td width="48%">
                        <table width="100%">
                            <tr>
                                <td class="copy10grey">
                                   Contact Name:<span class="errormessage">*</span></td>
                                <td>
                                    <asp:TextBox ID="txtShippContactName"  MaxLength="50" TabIndex="26" Width="200" CssClass="copy10grey alphanumeric" runat="server"></asp:TextBox>
                                </td>
                            </tr>    
                            <tr>
                                <td class="copy10grey">
                                Address 1:<span class="errormessage">*</span></td>
                                <td>
                                <asp:TextBox ID="txtShippAdd1" Width="200"  MaxLength="50" TabIndex="27" CssClass="copy10grey addresscss" runat="server"></asp:TextBox>
                                </td>
                            </tr>    
                            <tr>
                                <td class="copy10grey">
                                Address 2:</td>
                                <td>
                                    <asp:TextBox ID="txtShippAdd2" Width="200"  MaxLength="50" TabIndex="28" CssClass="copy10grey addresscss" runat="server"></asp:TextBox>
                                </td>
                            </tr>    
                            <tr>
                                <td class="copy10grey">
                                Office Phone 1:<span class="errormessage">*</span></td>
                                <td>
                                    <asp:TextBox ID="txtShippOfficePhone1" Width="200"  MaxLength="15" TabIndex="29" CssClass="copy10grey phonecss" runat="server"></asp:TextBox>
                                </td>
                            </tr>   
                            <tr>
                                <td class="copy10grey">
                                Office Phone 2:</td>
                                <td>
                                    <asp:TextBox ID="txtShippOfficePhone2" Width="200"  MaxLength="15" TabIndex="30" CssClass="copy10grey phonecss" runat="server"></asp:TextBox>
                                </td>
                            </tr>    
                            <tr>
                                <td class="copy10grey">
                                Cell Phone:</td>
                                <td>
                                    <asp:TextBox ID="txtShippCellPhone" Width="200"  MaxLength="15" TabIndex="31" CssClass="copy10grey phonecss" runat="server"></asp:TextBox>
                                </td>
                            </tr>   
                            <tr>
                                <td class="copy10grey">
                                Home Phone:</td>
                                <td>
                                    <asp:TextBox ID="txtShippHomePhone" Width="200"  MaxLength="15" TabIndex="32" CssClass="copy10grey phonecss" runat="server"></asp:TextBox>
                                </td>
                            </tr>   
                            <tr>
                                <td class="copy10grey">
                                    City:<span class="errormessage">*</span></td>
                                <td>
                                    <asp:TextBox ID="txtShippCity" Width="200"  MaxLength="50" TabIndex="33" CssClass="copy10grey alphanumeric" runat="server"></asp:TextBox>
                                </td>
                            </tr>    
                            <tr>
                                <td class="copy10grey">
                                State: <span class="errormessage">*</span></td>
                                <td>
                                
<asp:dropdownlist id="dpShipState" TabIndex="21" runat="server" cssclass="copy10grey" Width="201">
																	<asp:ListItem></asp:ListItem>
                                                                    <asp:ListItem Value="AL">AL - Alabama</asp:ListItem>
																	<asp:ListItem Value="AK">AK - Alaska</asp:ListItem>
																	<asp:ListItem Value="AZ">AZ - Arizona</asp:ListItem>
																	<asp:ListItem Value="AR">AR - Arkansas</asp:ListItem>
																	<asp:ListItem Value="CA" >CA - California</asp:ListItem>
																	<asp:ListItem Value="CO">CO - Colorado</asp:ListItem>
																	<asp:ListItem Value="CT">CT - Connecticut</asp:ListItem>
																	<asp:ListItem Value="DC">DC - District of Columbia</asp:ListItem>
																	<asp:ListItem Value="DE">DE - Delaware</asp:ListItem>
																	<asp:ListItem Value="FL">FL - Florida</asp:ListItem>
																	<asp:ListItem Value="GA">GA - Georgia</asp:ListItem>
																	<asp:ListItem Value="HI">HI - Hawaii</asp:ListItem>
																	<asp:ListItem Value="ID">ID - Idaho</asp:ListItem>
																	<asp:ListItem Value="IL">IL - Illinois</asp:ListItem>
																	<asp:ListItem Value="IN">IN - Indiana</asp:ListItem>
																	<asp:ListItem Value="IA">IA - Iowa</asp:ListItem>
																	<asp:ListItem Value="KS">KS - Kansas</asp:ListItem>
																	<asp:ListItem Value="KY">KY - Kentucky</asp:ListItem>
																	<asp:ListItem Value="LA">LA - Louisiana</asp:ListItem>
																	<asp:ListItem Value="ME">ME - Maine</asp:ListItem>
																	<asp:ListItem Value="MD">MD - Maryland</asp:ListItem>
																	<asp:ListItem Value="MA">MA - Massachusetts</asp:ListItem>
																	<asp:ListItem Value="MI">MI - Michigan</asp:ListItem>
																	<asp:ListItem Value="MN">MN - Minnesota</asp:ListItem>
																	<asp:ListItem Value="MS">MS - Mississippi</asp:ListItem>
																	<asp:ListItem Value="MO">MO - Missouri</asp:ListItem>
																	<asp:ListItem Value="MT">MT - Montana</asp:ListItem>
																	<asp:ListItem Value="NE">NE - Nebraska</asp:ListItem>
																	<asp:ListItem Value="NV">NV - Nevada</asp:ListItem>
																	<asp:ListItem Value="NH">NH - New Hampshire</asp:ListItem>
																	<asp:ListItem Value="NJ">NJ - New Jersey</asp:ListItem>
																	<asp:ListItem Value="NM">NM - New Mexico</asp:ListItem>
																	<asp:ListItem Value="NY">NY - New York</asp:ListItem>
																	<asp:ListItem Value="NC">NC - North Carolina</asp:ListItem>
																	<asp:ListItem Value="ND">ND - North Dakota</asp:ListItem>
																	<asp:ListItem Value="OH">OH - Ohio</asp:ListItem>
																	<asp:ListItem Value="OK">OK - Oklahoma</asp:ListItem>
																	<asp:ListItem Value="OR">OR - Oregon</asp:ListItem>
																	<asp:ListItem Value="PA">PA - Pennsylvania</asp:ListItem>
																	<asp:ListItem Value="RI">RI - Rhode Island</asp:ListItem>
																	<asp:ListItem Value="SC">SC - South Carolina</asp:ListItem>
																	<asp:ListItem Value="SD">SD - South Dakota</asp:ListItem>
																	<asp:ListItem Value="TN">TN - Tennessee</asp:ListItem>
																	<asp:ListItem Value="TX">TX - Texas</asp:ListItem>
																	<asp:ListItem Value="UT">UT - Utah</asp:ListItem>
																	<asp:ListItem Value="VT">VT - Vermont</asp:ListItem>
																	<asp:ListItem Value="VA">VA - Virginia</asp:ListItem>
																	<asp:ListItem Value="WA">WA - Washington</asp:ListItem>
																	<asp:ListItem Value="WV">WV - West Virginia</asp:ListItem>
																	<asp:ListItem Value="WI">WI - Wisconsin</asp:ListItem>
																	<asp:ListItem Value="WY">WY - Wyoming</asp:ListItem>

                                                                    <asp:ListItem Value="AB">AB - Alberta</asp:ListItem>
																	<asp:ListItem Value="BC">BC - British Columbia</asp:ListItem>
																	<asp:ListItem Value="MB">MB - Manitoba</asp:ListItem>
																	<asp:ListItem Value="NB">NB - New Brunswick</asp:ListItem>
																	<asp:ListItem Value="NL">NL - Newfoundland and Labrador</asp:ListItem>
																	<asp:ListItem Value="NS">NS - Nova Scotia</asp:ListItem>
																	<asp:ListItem Value="NT">NT - Northwest Territories</asp:ListItem>
																	<asp:ListItem Value="NU">NU - Nunavut</asp:ListItem>
																	<asp:ListItem Value="ON">ON - Ontario</asp:ListItem>

                                                                    <asp:ListItem Value="PE">PE - Prince Edward Island</asp:ListItem>
																	<asp:ListItem Value="QC">QC - Quebec</asp:ListItem>
																	<asp:ListItem Value="SK">SK - Saskatchewan</asp:ListItem>
																	<asp:ListItem Value="YT">YT - Yukon</asp:ListItem>
																</asp:dropdownlist>
                                
                                <%--<asp:dropdownlist id="dpShippState"  runat="server"  Width="202" cssclass="copy10grey">
                                </asp:dropdownlist>
                                --%>
                                <%--<asp:TextBox ID="txtShippState" Width="200"  MaxLength="2" TabIndex="34" CssClass="copy10grey" runat="server"></asp:TextBox>--%>
                                
                                </td>
                            </tr>    
                            <tr>
                                <td class="copy10grey">
                                Zip: <span class="errormessage">*</span></td>
                                <td>
                                    <asp:TextBox ID="txtShippZip" Width="200"  MaxLength="5" TabIndex="35" CssClass="copy10grey numericcss" runat="server"></asp:TextBox>
                                </td>
                            </tr>    
                            
                            <tr>
                                <td class="copy10grey">
                                Email 1: <span class="errormessage">*</span></td>
                                <td>
                                <asp:TextBox ID="txtShippEmail" Width="200"  MaxLength="50" TabIndex="36" CssClass="copy10grey" runat="server"></asp:TextBox>
                                </td>
                            </tr>    
                            <tr>
                                <td class="copy10grey">
                                Email 2:</td>
                                <td>
                                <asp:TextBox ID="txtShippEmail2" TabIndex="37" MaxLength="50"  Width="200" CssClass="copy10grey" runat="server"></asp:TextBox>
                                </td>
                            </tr>     
                             <tr>
                                <td class="copy10grey">
                                Country:</td>
                                <td>
                                <asp:TextBox ID="txtShipCountry" TabIndex="38" MaxLength="100"  Width="200" CssClass="copy10grey alphanumeric" runat="server"></asp:TextBox>
                                </td>
                            </tr>     
                            </table>
                    </td>
                </tr>
                        
            </table>  
                    </td>
               </tr>
            </table>        
            
            
            
            
             </div>
             <div id="ct2" class="tabcontent" style="border:0px solid #666666" >                                        
                <table bordercolor="#839abf" border="0" cellSpacing="0" cellPadding="0" width="100%" align="center" >
                    <tr bordercolor="#839abf" bgcolor="#00225F">
                        <td >
                            <span class="copy11whb">COMPANY STORE LOCATIONS</span>
                        </td>
                        <td align="right">
                          
                            <asp:Button ID="btnAddStoreID"  runat="server" TabIndex="38" Text="Add New Store Location" 
                                CssClass="buttongray" onclick="btnAddStoreID_Click" CausesValidation="false" />
                        </td>
                    </tr>
                </table>     
                     
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" ChildrenAsTriggers="true" >                                        
                    <ContentTemplate>
                        <asp:Repeater ID="rptStore" runat="server" >
                            <HeaderTemplate>
                    <table  cellSpacing="6" cellPadding="5" width="100%" align="center" >
                         <tr bordercolor="#839abf">
                    <td class="buttongrid" >
                    Store ID
                    </td>
                    <td class="buttongrid" >
                    Store Name
                    </td>
                    <td class="buttongrid">
                    Address
                    </td>
                    <td class="buttongrid">
                    City
                    </td>
                    <td class="buttongrid">
                    State
                    </td>
                    <td class="buttongrid">
                    Country
                    </td>
                    <td class="buttongrid">
                    Zip
                    </td>
                    <td class="buttongrid">
                    Active
                    </td>
               </tr>
               </HeaderTemplate>
                            <ItemTemplate>
  
                     <tr>
                    <td>
                    <asp:HiddenField ID="hdnAddressID" Value='<%# Eval("CompanyAddressID") %>' runat="server" />
                        
                        <asp:TextBox ID="txtStoreID" ReadOnly='<%# Convert.ToString(Eval("StoreFlag"))!="" ? true : false %>' MaxLength="15" Width="80%" CssClass="copy10grey alphanumeric" Text='<%# Eval("storeID") %>' runat="server"></asp:TextBox>
                    </td>
                    <td >
                        <asp:TextBox ID="txtStoreName" MaxLength="50" Width="80%" CssClass="copy10grey alphanumeric" Text='<%# Eval("storeName") %>' runat="server"></asp:TextBox>
                    </td>
                    <td >
                        <asp:TextBox ID="txtAddress" MaxLength="100" Width="80%" CssClass="copy10grey addresscss" Text='<%# Eval("StoreAddress.address1") %>' runat="server"></asp:TextBox>
                    </td>
                    <td >
                        <asp:TextBox ID="txtCity" MaxLength="50" Width="80%" CssClass="copy10grey alphanumeric" Text='<%# Eval("StoreAddress.city") %>'  runat="server"></asp:TextBox>
                    </td>
                    <td >
                         <asp:dropdownlist id="dpStState" runat="server" cssclass="copy10grey" Width="95%" SelectedValue='<%# Eval("StoreAddress.state") %>'>
																	<asp:ListItem></asp:ListItem>
																	<asp:ListItem Value="AL">AL - Alabama</asp:ListItem>
																	<asp:ListItem Value="AK">AK - Alaska</asp:ListItem>
																	<asp:ListItem Value="AZ">AZ - Arizona</asp:ListItem>
																	<asp:ListItem Value="AR">AR - Arkansas</asp:ListItem>
																	<asp:ListItem Value="CA" >CA - California</asp:ListItem>
																	<asp:ListItem Value="CO">CO - Colorado</asp:ListItem>
																	<asp:ListItem Value="CT">CT - Connecticut</asp:ListItem>
																	<asp:ListItem Value="DC">DC - District of Columbia</asp:ListItem>
																	<asp:ListItem Value="DE">DE - Delaware</asp:ListItem>
																	<asp:ListItem Value="FL">FL - Florida</asp:ListItem>
																	<asp:ListItem Value="GA">GA - Georgia</asp:ListItem>
																	<asp:ListItem Value="HI">HI - Hawaii</asp:ListItem>
																	<asp:ListItem Value="ID">ID - Idaho</asp:ListItem>
																	<asp:ListItem Value="IL">IL - Illinois</asp:ListItem>
																	<asp:ListItem Value="IN">IN - Indiana</asp:ListItem>
																	<asp:ListItem Value="IA">IA - Iowa</asp:ListItem>
																	<asp:ListItem Value="KS">KS - Kansas</asp:ListItem>
																	<asp:ListItem Value="KY">KY - Kentucky</asp:ListItem>
																	<asp:ListItem Value="LA">LA - Louisiana</asp:ListItem>
																	<asp:ListItem Value="ME">ME - Maine</asp:ListItem>
																	<asp:ListItem Value="MD">MD - Maryland</asp:ListItem>
																	<asp:ListItem Value="MA">MA - Massachusetts</asp:ListItem>
																	<asp:ListItem Value="MI">MI - Michigan</asp:ListItem>
																	<asp:ListItem Value="MN">MN - Minnesota</asp:ListItem>
																	<asp:ListItem Value="MS">MS - Mississippi</asp:ListItem>
																	<asp:ListItem Value="MO">MO - Missouri</asp:ListItem>
																	<asp:ListItem Value="MT">MT - Montana</asp:ListItem>
																	<asp:ListItem Value="NE">NE - Nebraska</asp:ListItem>
																	<asp:ListItem Value="NV">NV - Nevada</asp:ListItem>
																	<asp:ListItem Value="NH">NH - New Hampshire</asp:ListItem>
																	<asp:ListItem Value="NJ">NJ - New Jersey</asp:ListItem>
																	<asp:ListItem Value="NM">NM - New Mexico</asp:ListItem>
																	<asp:ListItem Value="NY">NY - New York</asp:ListItem>
																	<asp:ListItem Value="NC">NC - North Carolina</asp:ListItem>
																	<asp:ListItem Value="ND">ND - North Dakota</asp:ListItem>
																	<asp:ListItem Value="OH">OH - Ohio</asp:ListItem>
																	<asp:ListItem Value="OK">OK - Oklahoma</asp:ListItem>
																	<asp:ListItem Value="OR">OR - Oregon</asp:ListItem>
																	<asp:ListItem Value="PA">PA - Pennsylvania</asp:ListItem>
																	<asp:ListItem Value="RI">RI - Rhode Island</asp:ListItem>
																	<asp:ListItem Value="SC">SC - South Carolina</asp:ListItem>
																	<asp:ListItem Value="SD">SD - South Dakota</asp:ListItem>
																	<asp:ListItem Value="TN">TN - Tennessee</asp:ListItem>
																	<asp:ListItem Value="TX">TX - Texas</asp:ListItem>
																	<asp:ListItem Value="UT">UT - Utah</asp:ListItem>
																	<asp:ListItem Value="VT">VT - Vermont</asp:ListItem>
																	<asp:ListItem Value="VA">VA - Virginia</asp:ListItem>
																	<asp:ListItem Value="WA">WA - Washington</asp:ListItem>
																	<asp:ListItem Value="WV">WV - West Virginia</asp:ListItem>
																	<asp:ListItem Value="WI">WI - Wisconsin</asp:ListItem>
																	<asp:ListItem Value="WY">WY - Wyoming</asp:ListItem>

                                                                    <asp:ListItem Value="AB">AB - Alberta</asp:ListItem>
																	<asp:ListItem Value="BC">BC - British Columbia</asp:ListItem>
																	<asp:ListItem Value="MB">MB - Manitoba</asp:ListItem>
																	<asp:ListItem Value="NB">NB - New Brunswick</asp:ListItem>
																	<asp:ListItem Value="NL">NL - Newfoundland and Labrador</asp:ListItem>
																	<asp:ListItem Value="NS">NS - Nova Scotia</asp:ListItem>
																	<asp:ListItem Value="NT">NT - Northwest Territories</asp:ListItem>
																	<asp:ListItem Value="NU">NU - Nunavut</asp:ListItem>
																	<asp:ListItem Value="ON">ON - Ontario</asp:ListItem>

                                                                    <asp:ListItem Value="PE">PE - Prince Edward Island</asp:ListItem>
																	<asp:ListItem Value="QC">QC - Quebec</asp:ListItem>
																	<asp:ListItem Value="SK">SK - Saskatchewan</asp:ListItem>
																	<asp:ListItem Value="YT">YT - Yukon</asp:ListItem>
																</asp:dropdownlist>
                                
                        <%--<asp:DropDownList ID="ddState"  CssClass="copy10grey" Width="80%"  runat="server">
                        </asp:DropDownList>--%>
                        <%--<asp:TextBox ID="txtState"  MaxLength="2" Width="80%" CssClass="copy10grey" Text='<%# Eval("StoreAddress.state") %>'  runat="server"></asp:TextBox>--%>
                    </td>
                    <td >
                        <asp:TextBox ID="txtcountry1" MaxLength="50" Width="80%" CssClass="copy10grey alphanumeric" Text='<%# Eval("StoreAddress.country") %>'  runat="server"></asp:TextBox>
                    </td>
                    <td >
                        <asp:TextBox ID="txtZip" Width="80%"  MaxLength="5" CssClass="copy10grey numericcss" Text='<%# Eval("StoreAddress.zip") %>'  runat="server"></asp:TextBox>
                    </td>
                    <td >
                        <asp:CheckBox ID="chkActive1" Checked='<%# Eval("active") %>' runat="server" />
                    </td>
               </tr>
               
                </ItemTemplate>
                            <FooterTemplate>
                                </table>  
                            </FooterTemplate>
                        </asp:Repeater>                           
                    </ContentTemplate>
                     <%-- <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnUploadStores" EventName="Click" />            
                    </Triggers>--%>
                     <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnAddStoreID" EventName="Click" />            
                    </Triggers>
                </asp:UpdatePanel>           
           </div>
           <div id="ct3" class="tabcontent" style="border:0px solid #666666" >                                        
                <table bordercolor="#839abf" border="0" cellSpacing="0" cellPadding="0" width="100%" align="center" >
                    <tr bordercolor="#839abf" bgcolor="#00225F">
                        <td >
                            <span class="copy11whb">Customer Email</span>
                        
                        </td>
                    </tr>
                </table>

                <asp:Repeater ID="rptEmail" runat="server" >
                <HeaderTemplate>
                    <table  cellSpacing="6" cellPadding="5" width="100%" align="center" >
                    <tr bordercolor="#839abf">
                    <td class="buttongrid" width="40%">
                    Module Name
                    </td>
                    <td class="buttongrid" width="30%">
                    Email
                    </td>
                    <td class="buttongrid" width="30%">
                    Active/Inactive
                    </td>
               </tr>
               </HeaderTemplate>
                    <ItemTemplate>
                     <tr>
                    <td class="copy10grey">
                        <asp:HiddenField ID="hdnModuleGUID" Value='<%# Eval("ModuleGUID") %>' runat="server" />
                        <%# Eval("ModuleName")%>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_Email" onchange="return ValidateEmail(this);"  MaxLength="100" Width="90%" CssClass="copy10grey" Text='<%# Eval("Email") %>' runat="server"></asp:TextBox>
                        <%--<asp:RegularExpressionValidator ID="valEmail" runat="server" ErrorMessage="Enter a valid email id!"
                         ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txt_Email"></asp:RegularExpressionValidator>
--%>
                        
                      
                    </td>
                    <td>
                        <asp:CheckBox ID="chkNotification" Checked='<%# Eval("IsNotification") %>' runat="server" />

                        <%--<asp:TextBox ID="txtOvrdEmail" MaxLength="100" Width="90%" onchange="return ValidateEmail(this);"  CssClass="copy10grey" Text='<%# Eval("OverrideEmail") %>' runat="server"></asp:TextBox>
                        --%><%--<asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ErrorMessage="Enter a valid email id!"
                         ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txtOvrdEmail"></asp:RegularExpressionValidator>
--%>
                    </td>
                    
               </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table>  
                </FooterTemplate>
                </asp:Repeater>
               
               </div>   
               <div id="ct4" class="tabcontent" style="border:0px solid #666666" >
                <table bordercolor="#839abf" border="0" cellSpacing="0" cellPadding="0" width="100%" align="center" >
                    <tr bordercolor="#839abf" bgcolor="#00225F">
                        <td >
                            <span class="copy11whb">COMPANY WAREHOUSE CODE</span>
                        </td>
                        <td align="right">
                          
                            <asp:Button ID="btnWhCode"  runat="server" TabIndex="38" Text="Add New Warehouse Code" 
                                CssClass="buttongray" onclick="btnWhCode_Click" CausesValidation="false" />
                        </td>
                    </tr>
                </table>     
                     
                <asp:UpdatePanel ID="upPnl" runat="server" ChildrenAsTriggers="true" >                                        
                    <ContentTemplate>
                        <asp:Repeater ID="rptWhCode" runat="server" >
                            <HeaderTemplate>
                                <table  cellSpacing="6" cellPadding="5" width="100%" align="center" >
                                     <tr bordercolor="#839abf">
                                <td class="buttongrid" >
                                Warehouse Code
                                </td>
                                <td class="buttongrid">
                                Active
                                </td>
                           </tr>
                           </HeaderTemplate>
                                <ItemTemplate>
  
                                 <tr>
                                    <td>
                                    <asp:HiddenField ID="hdnWhID" Value='<%# Eval("warehouseCodeGUID") %>' runat="server" />
                        
                                        <asp:TextBox ID="txtWhcode"  MaxLength="4" Width="80%" CssClass="copy10grey alphanumeric" Text='<%# Eval("warehouseCode") %>' runat="server"></asp:TextBox>
                                    </td>
                   
                                    <td >
                                        <asp:CheckBox ID="chkActive2" Checked='<%# Eval("active") %>' runat="server" />
                                    </td>
                               </tr>
               
                                </ItemTemplate>
                            <FooterTemplate>
                                </table>  
                            </FooterTemplate>
                        </asp:Repeater>                           
                    </ContentTemplate>
                     <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnWhCode" EventName="Click" />            
                    </Triggers>
                </asp:UpdatePanel> 
               </div> 
                 <div id="ct5" class="tabcontent" style="border:0px solid #666666" > 
                     <table bordercolor="#839abf" border="0" cellSpacing="0" cellPadding="0" width="100%" align="center" >
                    <tr bordercolor="#839abf" bgcolor="#00225F">
                        <td >
                            <span class="copy11whb">INTEGRATION</span>
                        </td>
                        <td align="right">
                          
                            <asp:Button ID="btnIntegration"  runat="server" TabIndex="38" Text="Add New Integration" 
                                CssClass="buttongray" onclick="btnIntegration_Click" CausesValidation="false" />
                        </td>
                    </tr>
                </table>     
                     
                <asp:UpdatePanel ID="upInt" runat="server" ChildrenAsTriggers="true" >                                        
                    <ContentTemplate>
                        <asp:Repeater ID="rptInt" runat="server" OnItemDataBound="rptInt_ItemDataBound" >
                            <HeaderTemplate>
                            <table  cellSpacing="6" cellPadding="5" width="100%" align="center" >
                            <tr bordercolor="#839abf">
                                <td class="buttongrid" width="18%" >
                                API Name
                                </td>
                                <td class="buttongrid"  width="40%">
                                API Adress
                                </td>
                                <td class="buttongrid"  width="36%">
                                Connection String(Comma-delimited)
                                </td>
                                <%--<td class="buttonlabel" width="18%">
                                Password
                                </td>--%>
                                <td class="buttongrid" width="3%">
                                Active
                                </td>
                            </tr>
                            </HeaderTemplate>
                            <ItemTemplate>  
                            <tr>
                                <td>
                                    <asp:HiddenField ID="hdnIntigrationID" Value='<%# Eval("IntegrationID") %>' runat="server" />
                                    <asp:HiddenField ID="hdIntegrationModuleID" Value='<%# Eval("IntegrationModuleID") %>' runat="server" />
                                    <asp:dropdownlist id="ddlIntigration" runat="server" cssclass="copy10grey" Width="95%" >
                                    </asp:dropdownlist>                                    
                                </td>
                                <td>
                                    <asp:TextBox ID="txtAPIAddress" onchange="ValidateURL(this);" MaxLength="100" Width="95%" CssClass="copy10grey" Text='<%# Eval("APIAddress") %>' runat="server"></asp:TextBox>
                                </td>
                                <td >
                                    <asp:TextBox ID="txtUserName" MaxLength="200" Width="95%" CssClass="copy10grey alphanumeric" Text='<%# Eval("UserName") %>' runat="server"></asp:TextBox>
                                </td>
                                <%--<td>
                                   
                                    <asp:TextBox ID="txtPass" MaxLength="50" TextMode="Password"  Width="95%" CssClass="copy10grey" Text='<%# Eval("Password") %>' runat="server"></asp:TextBox>
                                </td>--%>
                                <td >
                                    <asp:CheckBox ID="chkActive2" Checked='<%# Eval("Active") %>' runat="server" />
                                </td>
                            <tr>               
                            </ItemTemplate>
                            <FooterTemplate>
                                </table>  
                            </FooterTemplate>
                        </asp:Repeater>                           
                    </ContentTemplate>
                      <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnIntegration" EventName="Click" />            
                    </Triggers>
                </asp:UpdatePanel>  
                
               
               </div>   
               
           </div>
             </td>
          </tr>
          </table>
          <table width="95%" align="center" >
                <tr>
			        <td align="center" >
			            <asp:Button ID="btnSubmit" runat="server"  CssClass="buybt" OnClientClick="return IsValidate();" 
                                        Text="   Submit   " onclick="btnSubmit_Click" />&nbsp;&nbsp;
                        <asp:Button ID="btnCancel" runat="server" CssClass="buybt" CausesValidation="false"  Text="   Cancel   " 
                            onclick="btnCancel_Click" />
			        </td>
			    </tr>
			    </table>    
          <table width="100%" align="center">
                <tr><td>&nbsp;</td></tr>
				<tr>
                    <td><foot:MenuFooter id="Footer" runat="server"></foot:MenuFooter></td>
                </tr>			        
        </table>
    </form>
    <script type="text/javascript">
    //SYNTAX: ddtabmenu.definemenu("tab_menu_id", integer OR "auto")
        var tabIndex = document.getElementById("<%= hdnTabIndex.ClientID %>").value;
        if (tabIndex == '')
            tabIndex = 0;
        //alert(tabIndex);
        ddtabmenu.definemenu("ddtabs4", tabIndex) //initialize Tab Menu #4 with 3rd tab selected

    
    </script>
</body>
</html>
