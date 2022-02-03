<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageMslEsn.aspx.cs" Inherits="avii.Admin.ManageMslEsn" %>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="/Controls/Header.ascx" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>.:: Lan Global Inc. ::.</title>

     <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>

    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.4.0/jquery.min.js"></script>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/jquery-ui.min.js"></script>
    <link href="https://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/themes/start/jquery-ui.css" rel="stylesheet" type="text/css" />

<%--<script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/jquery-ui.js" type="text/javascript"></script>
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/themes/start/jquery-ui.css" rel="stylesheet" type="text/css" />--%>
	<%--<script type="text/javascript" src="../JQuery/jquery.min.js"></script>
    <script type="text/javascript" src="../JQuery/jquery-ui.min.js"></script>--%>
	<script type="text/javascript" src="../JQuery/jquery.blockUI.js"></script>	
    <link href="../aerostyle.css" rel="stylesheet" type="text/css"/>
    <link rel="stylesheet"  type="text/css" href="../fullfillment/calendar/dhtmlgoodies_calendar.css" media="screen" />
	<script type="text/javascript" src="../fullfillment/calendar/dhtmlgoodies_calendar.js"></script>
     <script type="text/javascript" src="https://code.jquery.com/jquery-1.11.0.min.js"></script>

     <script>
         function close_window() {
             if (confirm("Close Window?")) {
                 window.close();
                 return true;
             }
             else
                 return false
         }
         function OpenNewPage(url) {

             var newWin = window.open(url);

             if (!newWin || newWin.closed || typeof newWin.closed == 'undefined') {
                 alert('your pop up blocker is enabled');

                 //POPUP BLOCKED
             }
         }
     </script>
    <script type="text/javascript">
        function SelectAll(id) {
            var check = document.getElementById(id).checked;
            $(':checkbox').prop('checked', check);
        }
        function close_window() {
            if (confirm("Close Window?")) {
                window.close();
                return true;
            }
            else
                return false
        }
    </script>
    
    <script type="text/javascript">
        function formatParentCatDropDown(objddl) {

            for (i = 0; i < objddl.options.length; i++) {
                objddl.options[i].innerHTML = objddl.options[i].innerHTML.replace(/&amp;/g, '&');
            }
        }
       
        function ValidatePrice(evt, obj) {
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

        function isNumberKey(evt) {
            var receivedQty = document.getElementById("<% =txtShipQty.ClientID %>").value;
            //alert(receivedQty.length);
            var charCodes = evt.keyCode ? evt.keyCode : evt.which ? evt.which : evt.charCode;
            if (receivedQty.length < 1) {
                if (charCodes > 31 && (charCodes < 48 || charCodes > 57) && charCodes != 45) {
                    charCodes = 0;
                    return false;
                }
                //return true;
            }
            else
                if (charCodes > 31 && (charCodes < 48 || charCodes > 57)) {
                    charCodes = 0;
                    return false;
                }
            return true;
        }
        function Validate(flag) {
            if (flag == '1' || flag == '2') {
                var company = document.getElementById("<% =dpCompany.ClientID %>");
                if (company.selectedIndex == 0) {
                    alert('Customer is required!');
                    return false;

                }
                var category = document.getElementById("<% =ddlCategory.ClientID %>");
                if (category.selectedIndex == 0) {
                    alert('Category is required!');
                    return false;

                }
                else {
                //alert(categoryType.value)
                var categorywithproduct = category.value
                var arr = categorywithproduct.split('|');
                //alert(arr)
                //alert(arr[1])
                if (arr[1] == 0)
                {
                    alert('Please select leaf category!')
                   // alert('Product not allowed for this category!')
                   // categoryType.options[categoryType.selectedIndex].value = 0;
                    return false;
                }

            }
                if (flag == '1') {
                    var oFile = document.getElementById("flnUpload").files[0]; // <input type="file" id="fileUpload" accept=".jpg,.png,.gif,.jpeg"/>

                    if (oFile.size > 3145728) // 3 mb for bytes.
                    {
                        alert("File size must under 3 MB!");
                        return false;
                    }
                }
            }
            if (flag == '2') {
                var status = document.getElementById("<% =ddlSKU.ClientID %>");
                if (status.selectedIndex == 0) {
                    //alert('Status is required!');
                    //return false;

                }
            }


            var defaultDateRange = '1095';
            var uploadDate = new Date(); //
           // alert(uploadDate);
            if (uploadDate != null && uploadDate != '') {

                var today = uploadDate;
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
               // alert(today)
              //  alert(daydiff(parseDate(uploadDate), parseDate(today)));
                var daterange = daydiff(parseDate(uploadDate), parseDate(today));

                //if (daterange > defaultDateRange) {
                //    alert('UploadDate can not be more than  1095 days before');
                //    return false;
                //}
                //if (daterange < 0) {
                //    alert('UploadDate can not be more than today date');
                //    return false;
                //}

            }
        }

        function set_focus1() {
            var img = document.getElementById("imgOrderDate");
            var st = document.getElementById("txtShipvia");
            st.focus();
            img.click();
        }
        function set_focus2() {
            var img = document.getElementById("imgShipDate");
            var st = document.getElementById("txtShipvia");
            st.focus();
            img.click();
        }
        
        function parseDate(str) {
            var mdy = str.split('/')
            return new Date(mdy[2], mdy[0] - 1, mdy[1]);
        }

        function daydiff(first, second) {
            return (second - first) / (1000 * 60 * 60 * 24);
        }

        function deleteConfirm(obj) {
            var vFlag;



            vFlag = confirm('Delete this IMEI?');
            if (vFlag)
                return true;
            else
                return false;

        }

        function CategoryOnchange(obj) {
            var categoryType = document.getElementById('ddlCategory');
            var categoryvalue = categoryType.options[categoryType.selectedIndex].value;
            //alert(categoryvalue)
            //if (categoryvalue > 0) 
            {

                //alert(categoryType.value)
                var categorywithproduct = categoryType.value
                var arr = categorywithproduct.split('|');
                if (arr.length > 1) {  //alert(arr)
                    //alert(arr[1])
                    if (arr[1] == 0) {
                        alert('Please select leaf category!')
                       // alert('Product not allowed for this category!')
                        // categoryType.options[categoryType.selectedIndex].value = 0;
                        return false;
                    }
                }

            }
        }
        
    </script>


</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

        <table cellSpacing="0" cellPadding="0" align="center" width="100%" border="0">
        	<tr>
				<td><head:MenuHeader id="HeadAdmin" runat="server"></head:MenuHeader></td>
			</tr>
			<tr>
				<td  bgcolor="#dee7f6" class="button">&nbsp;&nbsp;Inventory Receive
				</td>
			</tr>            
        </table>
            <asp:UpdatePanel ID="UpdatePanel1"  runat="server" UpdateMode="Conditional" >
            <ContentTemplate>
    
            <table cellSpacing="0" cellPadding="0" align="center" width="100%" border="0">
    		<tr>                    
                <td colspan="2">
                    <asp:Label ID="lblMsg" runat="server" Width="100%" CssClass="errormessage"></asp:Label></td>
            </tr>               
            
            <tr>
                <td align="center">

                    <table  cellSpacing="1" cellPadding="1" width="100%">
                        <tr><td class="copy10grey" align="left">
                        <%--- Create/Update ESN(s) to Customer assigned SKU#(s).<br />&nbsp;--%>
                            <asp:Label ID="lblFilesize" runat="server" CssClass="copy10grey">&nbsp;- Upload file should be less than 3 MB. <br /></asp:Label>
	                    
                            <asp:Label ID="lblCol" runat="server" CssClass="copy10grey">&nbsp;- Bold columns are required.</asp:Label>
			            
                       <%-- - Uploaddate can not be 1095 day before  from current date <br />&nbsp;
                        - Uploaddate can not be more than current date <br />&nbsp;--%>

                        </td></tr>
                    </table>

                <table cellSpacing="0" cellPadding="0" align="center" width="100%" border="0">
                    <tr>
                        <td align="center">

                        
               
                 <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="90%">
                    <tr>
                    <td>
			            <table  cellSpacing="5" cellPadding="5" width="100%" border="0">
                            <tr>
                                <td  class="copy10grey" align="right" width="22%" >
                                    <b>Customer:</b> &nbsp;</td>
                                <td align="left"  width="28%">
                                   <asp:DropDownList ID="dpCompany" CssClass="copy10grey" runat="server" Width="80%"
                                    OnSelectedIndexChanged="dpCompany_SelectedIndexChanged"  
                                    AutoPostBack="true">
									</asp:DropDownList>

                                    </td>
                                <td class="copy10grey" align="right" width="22%" >
                                    Received As: &nbsp;
                                </td>
                                <td  align="left"  width="28%" >
                                     <asp:DropDownList ID="ddlReceivedAs" runat="server" class="copy10grey" Width="80%">
                                         <asp:ListItem Text="None" Value="None"></asp:ListItem>
                                         <asp:ListItem Text="Product without ASN" Value="Product without ASN"></asp:ListItem>
                                         <asp:ListItem Text="Product with ASN" Value="Product with ASN"></asp:ListItem>
                                         <asp:ListItem Text="Others" Value="Others"></asp:ListItem>
                                         
                                </asp:DropDownList>
                                  
                                </td>
                            </tr>
                            <tr>
                                <td class="copy10grey" align="right" width="22%" >
                                    <b>Order number:</b> &nbsp;

                                </td>
                                     
                                
                                <td  align="left" width="28%" >
                                     <asp:TextBox ID="txtOrderNumber" runat="server"   CssClass="copy10grey" MaxLength="20"  Width="80%"></asp:TextBox>
                               
                                </td>
                                <td class="copy10grey" align="right" width="22%" >
                                   Customer Order#: &nbsp;
                                </td>
                                <td  align="left"  width="28%" >
                                    <asp:TextBox ID="txtCustOrderNumber" runat="server"   CssClass="copy10grey" MaxLength="20"  Width="80%"></asp:TextBox>
                                     
                                </td>
                            </tr>
                            <tr style="display:none">
                                <td class="copy10grey" align="right" width="22%" >
                                    Order Date: &nbsp;

                                </td>

                                
                                <td  align="left" width="28%" >
                                     <asp:TextBox ID="txtOrderDate" runat="server" onfocus="set_focus1();"  CssClass="copy10grey" MaxLength="12"  Width="80%"></asp:TextBox>
                                            <img id="imgOrderDate" alt="" onclick="document.getElementById('<%=txtOrderDate.ClientID%>').value = ''; displayCalendar(document.getElementById('<%=txtOrderDate.ClientID%>'),'mm/dd/yyyy',this,true);" src="../fullfillment/calendar/sscalendar.jpg" />
                                        
                                </td>
                                <td class="copy10grey" align="right" width="22%" >
                                    Ship Date: &nbsp;
                                </td>
                                <td  align="left"  width="28%" >
                                     <asp:TextBox ID="txtShipDate" runat="server" onfocus="set_focus2();"  CssClass="copy10grey" MaxLength="12"  Width="80%"></asp:TextBox>
                                            <img id="imgShipDate" alt="" onclick="document.getElementById('<%=txtShipDate.ClientID%>').value = ''; displayCalendar(document.getElementById('<%=txtShipDate.ClientID%>'),'mm/dd/yyyy',this,true);" src="../fullfillment/calendar/sscalendar.jpg" />
                                        
                                </td>
                            </tr>
                            <tr  style="display:none">
                                <td class="copy10grey" align="right" width="22%" >
                                    Ship via: &nbsp;

                                </td>

                                
                                <td  align="left" width="28%" >
                                    <asp:TextBox ID="txtShipvia" runat="server"   CssClass="copy10grey" MaxLength="20"  Width="80%"></asp:TextBox>
                                     
                                </td>
                                <td class="copy10grey" align="right" width="22%" >
                                    Tracking#: &nbsp;
                                </td>
                                <td  align="left"  width="28%" >
                                    <asp:TextBox ID="txtTrackingNo" runat="server"   CssClass="copy10grey" MaxLength="20"  Width="80%"></asp:TextBox>
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
&nbsp;
                    </td>
                    </tr>
                     <tr>
                        <td align="center">
                        <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="90%">
                    <tr>
                    <td>
			            <table  cellSpacing="5" cellPadding="5" width="100%" border="0">
                            <tr >
                                <td class="copy10grey"  width="22%" align="right">
                                    <b>Category:</b> &nbsp;
                                </td>                                
                                <td  align="left"  width="28%">
                                <asp:DropDownList ID="ddlCategory" runat="server" CssClass="copy10grey" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged"  onchange="CategoryOnchange(this);" 
                                    Width="80%" ></asp:DropDownList>

                                <asp:Label ID="lblCategory" runat="server" Width="100%" CssClass="copy10grey"></asp:Label>

                                            

                                </td>
                                <td class="copy10grey" align="right" width="22%" >
                                    <b>SKU:</b> &nbsp;
                                </td>
                                <td  align="left"  width="28%" >
                                   <asp:DropDownList ID="ddlSKU" runat="server" CssClass="copy10grey"                                     
                                     Width="80%">
                                </asp:DropDownList>
                                                        <asp:Label ID="lblSKU" runat="server" Width="100%" CssClass="copy10grey"></asp:Label>

                                </td>
                            
                                    <asp:TextBox ID="txtUnitPrice" Visible="false" runat="server" onkeypress="return ValidatePrice(event, this);"  CssClass="copy10grey" MaxLength="20"  Width="80%"></asp:TextBox>
                                </td>
                            </tr>
                            <tr id="trQty" runat="server">
                                <td  class="copy10grey" align="right" width="22%" >
                                     <b>Received quantity:</b> &nbsp;

                                </td>
                                <td align="left"  width="28%">
                                   <asp:TextBox ID="txtShipQty" runat="server" onkeypress="return isNumberKey(event);"  CssClass="copy10grey" MaxLength="20"  Width="80%"></asp:TextBox>
                               
                                    </td>
                                <td class="copy10grey" align="right" width="22%" >
                                   <%--<b>Order quantity:</b> &nbsp;--%>
                                   <%-- Inspection Required: &nbsp;--%>
                                    
                                </td>
                                <td  align="left"  width="28%" >
                                    <asp:CheckBox ID="chkInspection" Visible="false" runat="server" CssClass="copy10grey" />
                               <%--     <asp:TextBox ID="txtOrderQty" runat="server" onkeypress="return isNumberKey(event);"  CssClass="copy10grey" MaxLength="20"  Width="80%"></asp:TextBox>
                               --%>     
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
&nbsp;
                    </td>
                    </tr>
                <tr>
                        <td align="center">
                            <asp:Panel ID="pnlESN" runat="server">
                 <table   bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="90%">
                    <tr>
                    <td>
			            <table  cellSpacing="5" cellPadding="5" width="100%" border="0">
                            <tr id="trESN1" runat="server">
                                <td colspan="2">

                                    <table width="100%" border="0" cellSpacing="0" cellPadding="0" id="uploadDT" runat="server" >
                                        <tr>
                                            <td  class="copy10grey" align="right" >
                                                Upload ESN file: &nbsp;</td>
                                            <td align="left" >
                                                <asp:FileUpload ID="flnUpload" runat="server" CssClass="txfield1" Width="55%" /></td>
                                            <td width="10%">
                                                <asp:Button ID="btnLocation" runat="server" Text="View Location" CssClass="button" OnClick="btnLocation_Click" CausesValidation="false" />
                                
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="copy10grey" align="right">
                                                File format sample: &nbsp;
                                                     </td>
                                            <td class="copy10grey" align="left">
                                              <b>BATCH,ESN</b>,MeidHex,MeidDec,<b>Location,SerialNumber,BoxID</b> <asp:LinkButton ID="lnkDownload" runat="server"   Text="Download file format" OnClick="lnkDownload_Click"></asp:LinkButton>
                                                <%--<asp:Label ID="lblUploadDate" runat="server" Text=",uploaddate"></asp:Label>--%>
                                            </td>
                                            <td></td>
                                         </tr>
                                         <%--<tr  valign="top">
                                            <td colspan="2" align="center">
                                            <asp:Panel ID="pnlDate" runat="server" Width="100%">
                                    
                                                <table cellpadding="5" cellspacing="5" width="100%" border="0">
                                                <tr  valign="top">
                                                    <td class="copy10grey" align="right"  width="42%" >
                                                        Upload Date: &nbsp;
                                                     </td>
                                                    <td class="copy10grey" align="left">
                                                        <asp:TextBox ID="txtUploadDate" runat="server" onfocus="set_focus1();"  CssClass="copy10grey" MaxLength="12"  Width="30%"></asp:TextBox>
                                                        <img id="imgFromtDate" alt="" onclick="document.getElementById('<%=txtUploadDate.ClientID%>').value = ''; displayCalendar(document.getElementById('<%=txtUploadDate.ClientID%>'),'mm/dd/yyyy',this,true);" src="../fullfillment/calendar/sscalendar.jpg" />
                                                    </td>
                                                </tr>
                                                </table>
                                            </asp:Panel>

                                            </td>
                                         </tr>--%>


                                            <tr  valign="top">
                                            <td class="copy10grey" align="right">
                                                Comment: &nbsp;
                                                     </td>
                                            <td class="copy10grey" align="left" colspan="2">
                                                <asp:TextBox ID="txtComment" runat="server" CssClass="copy10grey" Width="70%" Height="50px" TextMode="MultiLine"></asp:TextBox>

                                            </td>
                                         </tr>
 
                                    </table>
                                </td>
                            </tr>
                            

                             
                            <tr id="trESN2" runat="server">
                                <td colspan="2">
                                    <asp:Panel ID="pnlSubmit" runat="server" Visible="false">
                                    <table cellpadding="0" cellspacing="0" width="100%">
                                        <tr><td>
                                        <hr style="width:100%" />
                            
                                        </td></tr>   
                                         <tr>                    
                                            <td align="left">
                                                <asp:Label ID="lblConfirm" runat="server" Width="100%" CssClass="errorGreenMsg"></asp:Label></td>
                                        </tr>               
                                    
                                         <tr>
                                            <td  align="center">
                                
                                
                                            <asp:Button ID="btnSubmit2" Visible="false" Width="190px"  CssClass="button" runat="server" OnClick="btnSubmit_Click" Text="Submit" OnClientClick="return Validate(2);" />

                                            &nbsp;<asp:Button ID="btnCancel2" runat="server"  CssClass="button" Text="Cancel" OnClick="btnCancel_Click" />
                                                </td>
                                        </tr>
                                        <tr><td>
                                        <hr style="width:100%" />
                            
                                        </td></tr>   
                                    </table>

                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr><td colspan="2">
                            
                            <table cellpadding="0" cellspacing="0" width="95%" align="center">
                             <tr>
                                <td  align="left" style="height:8px; vertical-align:bottom">
                                <strong>   <asp:Label ID="lblCount" CssClass="copy10grey" runat="server" ></asp:Label> </strong> 
                                </td>
                                 <td  align="right">
                                     &nbsp;<asp:Button ID="btnClosew" runat="server" Text="Close Window" CssClass="button" Visible="false" OnClientClick="return close_window();"  />
                    
                                 </td>
                             </tr>
                             <tr>
                                <td colspan="2" align="center">
                                
                                <asp:GridView ID="gvMSL" runat="server" Width="100%" GridLines="Both"   AutoGenerateColumns="false"
                                    OnPageIndexChanging="gridView_PageIndexChanging" PageSize="100" AllowPaging="true" 
                                    OnRowDataBound="gvMSL_RowDataBound"  AllowSorting="true" OnSorting="gvMSL_Sorting">
                                        <RowStyle BackColor="Gainsboro" />
                                        <AlternatingRowStyle BackColor="white" />
                                        <HeaderStyle  CssClass="buttonlabel" ForeColor="white"/>
                                        <FooterStyle CssClass="white"  />
                                        <PagerStyle ForeColor="#636363" CssClass="copy10grey" HorizontalAlign="Left" />
                                        <Columns>
                                            
                                            <asp:TemplateField HeaderText="" Visible="false" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="1%">
                                                <HeaderTemplate>
                                                <asp:CheckBox ID="allchk"  runat="server"  />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hdEsn" Value='<%# Eval("Esn") %>' runat="server" />
                                                <asp:CheckBox ID="chkesn"  Visible='<%# Convert.ToBoolean(Eval("InUse")) == true ? false : true %>' runat="server" CssClass="copy10grey" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            
                                             <asp:TemplateField HeaderText="S.No." ItemStyle-CssClass="copy10grey"  ItemStyle-Width="1%" HeaderStyle-CssClass="buttonlabel">
                                                <ItemTemplate>

                                                        <%# Container.DataItemIndex + 1%>
                  
                                                </ItemTemplate>
                                            </asp:TemplateField> 

                                            <asp:TemplateField HeaderText="BATCH#" SortExpression="MSLNumber"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" 
                                                ItemStyle-Width="12%">
                                                <ItemTemplate><%#Eval("MSLNumber")%></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ESN/IMEI" HeaderStyle-CssClass="buttonundlinelabel" 
                                                SortExpression="ESN"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" 
                                                ItemStyle-Width="12%">
                                                <ItemTemplate>
                                                            <%#Eval("esn")%>
                                                    <span class="errormessage"><%#Eval("ErrorMessage")%></span>        
                                                    
                                                    
                                                </ItemTemplate>
                                            </asp:TemplateField> 
                                             
                                            
                                            <%--<asp:TemplateField HeaderText="ICC ID" Visible="false"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="30%">
                                                <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "icc_id")%>
                                                </ItemTemplate>
                                            </asp:TemplateField> 
                                            --%>

                                            <asp:TemplateField HeaderText="MeidHex" Visible="true"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="12%">
                                                <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "MeidHex")%>
                                                </ItemTemplate>
                                            </asp:TemplateField> 
                                            <asp:TemplateField HeaderText="MeidDec" Visible="true"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="12%">
                                                <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "MeidDec")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Location" Visible="true"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="12%">
                                                <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "Location")%>
                                                </ItemTemplate>
                                            </asp:TemplateField> 
                                            <asp:TemplateField HeaderText="Serial#" HeaderStyle-CssClass="buttonundlinelabel" 
                                                SortExpression="SerialNumber" Visible="true"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="12%">
                                                <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "SerialNumber")%>
                                                </ItemTemplate>
                                            </asp:TemplateField> 
                                            
                                            <%--<asp:TemplateField HeaderText="MSL" Visible="true"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "MSL")%>
                                                </ItemTemplate>
                                            </asp:TemplateField> 
                                            
                                            <asp:TemplateField HeaderText="OTKSL" Visible="true"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "OTKSL")%>
                                                </ItemTemplate>
                                            </asp:TemplateField> --%>
                                            <asp:TemplateField HeaderText="BOXID" HeaderStyle-CssClass="buttonundlinelabel" 
                                                SortExpression="BoxID" Visible="true"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="7%">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkBox" runat="server" OnCommand="lnkBox_Command" CommandArgument='<%# Eval("ESN")%>'>
                                                      &nbsp;  <%# DataBinder.Eval(Container.DataItem, "BoxID")%>&nbsp;
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField> 

                                           <asp:TemplateField ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="esnDelete" Enabled='<%# Convert.ToBoolean(Eval("InUse")) == true ? false : true %>' CommandArgument='<%# Eval("ESN") %>' OnCommand="ESNDelete_click"
                                                            OnClientClick="return deleteConfirm(this);" runat="server"  CommandName="wDelete"  AlternateText="Delete IMEI" ImageUrl="../images/delete.png" />
                                                        
                                                    </ItemTemplate>
                                                </asp:TemplateField> 

                                    
                                        </Columns>
                                    </asp:GridView>
                                    

                                </td>
                            </tr>
                            </table>
                            </td></tr>                            
                            
                            <tr><td colspan="2">
                            <hr style="width:100%" />
                            
                            </td></tr>                            
                             <tr>
                                <td colspan="2" align="center">
                                
                                <asp:Button ID="btnUpload"  Width="190px" CssClass="button" runat="server" OnClick="btnValidateUploadedFile_Click" Text="Validate Uploaded file" OnClientClick="return Validate(1);"  />

                                &nbsp;<asp:Button ID="btnSubmit" Visible="false" Width="190px"  CssClass="button" runat="server" OnClick="btnSubmit_Click" Text="Submit" OnClientClick="return Validate(2);"/>
                               <%-- &nbsp;<asp:Button ID="btnViewTracking" Visible="false" Width="190px"  CssClass="button" runat="server" OnClick="btnViewAssignedPos_Click" Text="View UPDATED fulfillment" />
--%>
                                &nbsp;<asp:Button ID="btnCancel" runat="server"  CssClass="button" Text="Cancel" OnClick="btnCancel_Click" />
                                        &nbsp;<asp:Button ID="btnDelete" Visible="false" runat="server"  CssClass="button" Text="Delete" OnClick="btnDelete_Click" />
                                        &nbsp;<asp:Button ID="btnClose" Visible="false" runat="server"  CssClass="button" Text="Close" OnClientClick="return close_window();" />
                                    </td>
                            </tr>
                       </table>
                            
                    </td>
                    </tr>
                           
                 </table>
                            </asp:Panel>
                            <asp:Panel ID="pnlHeader" runat="server" Visible="false">
                 <table  bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="90%">
                    <tr>
                    <td>
			            <table  cellSpacing="5" cellPadding="5" width="100%" border="0">
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="center">
                                
                                
                                &nbsp;<asp:Button ID="btnHeader" Visible="true" Width="190px"  CssClass="button" runat="server" OnClick="btnHeaderSubmit_Click" Text="Submit" OnClientClick="return Validate(2);"/>
                               
                                &nbsp;<asp:Button ID="Button3" Visible="true" runat="server"  CssClass="button" Text="Cancel" OnClick="btnCancel_Click" />
                                    </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
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
            
            </table>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnUpload" />
                <asp:PostBackTrigger ControlID="lnkDownload" />
            </Triggers>
            </asp:UpdatePanel>

            <asp:UpdateProgress ID="UpdateProgress1" runat="server"
                     DynamicLayout="false">
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
      
	
    </form>
    <script type="text/javascript">
        formatParentCatDropDown(document.getElementById("<%=ddlCategory.ClientID%>"));

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {
                    formatParentCatDropDown(document.getElementById("<%=ddlCategory.ClientID %>"));
                }
            });
        };
    </script>
</body>
</html>
