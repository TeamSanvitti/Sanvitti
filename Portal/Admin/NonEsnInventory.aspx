<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NonEsnInventory.aspx.cs" Inherits="avii.Admin.NonEsnInventory" %>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="/Controls/Header.ascx" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Inventory Receive - Accessory</title>
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
            var receivedQty = document.getElementById("<% =txtPallet.ClientID %>").value;
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
        </table>
        <table cellSpacing="0" cellPadding="0" align="center" width="95%" border="0">
		
			<tr class="buttonlabel">
				<td  class="buttonlabel">&nbsp;&nbsp;Inventory Receive - Accessory
				</td>
			</tr>            
        </table>
          <asp:UpdatePanel ID="UpdatePanel1"  runat="server" UpdateMode="Conditional" >
            <ContentTemplate>
    
            <table cellSpacing="0" cellPadding="0" align="center" width="100%" border="0">
    		<tr>                    
                <td colspan="2">
                    <asp:Label ID="lblMsg" runat="server"  CssClass="errormessage"></asp:Label></td>
            </tr>  
            <tr>
                <td align="center">

                <table  cellSpacing="1" cellPadding="1" width="95%">
                    <tr><td class="copy10grey" align="left">&nbsp;
                    - Upload file should be less than 3 MB. <br />&nbsp;
			        - Bold columns are required. <br />&nbsp;
                       
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
                                         <%--<asp:ListItem Text="None" Value="None"></asp:ListItem>--%>
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
                            <tr id="trReceivedate" runat="server" visible="false">
                                <td class="copy10grey" align="right" width="22%" >
                                    Receive Date: &nbsp;

                                </td>

                                
                                <td  align="left" width="28%" >
                                     <asp:TextBox ID="txtReceiveDate" runat="server" Enabled="false" CssClass="copy10grey"   Width="80%"></asp:TextBox>
                                        
                                </td>
                                <td class="copy10grey" align="right" width="22%" >
                                    Received By: &nbsp;
                                </td>
                                <td  align="left"  width="28%" >
                                     <asp:TextBox ID="txtReceiveBy" runat="server" Enabled="false" CssClass="copy10grey"   Width="80%"></asp:TextBox>
                                            
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
                                <%--<asp:Label ID="lblCategory" runat="server" Width="100%" CssClass="copy10grey"></asp:Label>--%>
                                </td>
                                <td class="copy10grey" align="right" width="22%" >
                                    <b>SKU:</b> &nbsp;
                                </td>
                                <td  align="left"  width="28%" >
                                    <asp:DropDownList ID="ddlSKU" runat="server" class="copy10grey"                                     
                                     Width="80%">
                                    </asp:DropDownList>
                                    <%--<asp:Label ID="lblSKU" runat="server" Width="100%" CssClass="copy10grey"></asp:Label>--%>

                                </td>
                            
                            </tr>
                            <tr >
                                <td  class="copy10grey" align="right" width="22%" >
                                     Pallet: &nbsp;

                                </td>
                                <td align="left"  width="28%">
                                   <asp:TextBox ID="txtPallet" runat="server" onkeypress="return isNumberKey(event);"  CssClass="copy10grey" MaxLength="20"  Width="80%"></asp:TextBox>
                               
                                </td>
                                <td class="copy10grey" align="right" width="22%" >
                                    Cartons: &nbsp;
                                    
                                </td>
                                <td  align="left"  width="28%" >
                                    <asp:TextBox ID="txtCarton" runat="server" onkeypress="return isNumberKey(event);"  CssClass="copy10grey" MaxLength="20"  Width="80%"></asp:TextBox>
                               
                                </td>
                            </tr>
                            <tr >
                                <td  class="copy10grey" align="right" width="22%" >
                                     Pieces per Carton:&nbsp;
                                </td>
                                <td align="left"  width="28%">
                                   <asp:TextBox ID="txtBoxQty" runat="server" onkeypress="return isNumberKey(event);"  CssClass="copy10grey" MaxLength="20"  Width="80%"></asp:TextBox>
                               
                                </td>
                                <td class="copy10grey" align="right" width="22%" >
                                   <b> Total Items: </b>&nbsp;
                                    
                                </td>
                                <td  align="left"  width="28%" >
                                    <asp:TextBox ID="txtTotalQty" runat="server" onkeypress="return isNumberKey(event);"  CssClass="copy10grey" MaxLength="20"  Width="80%"></asp:TextBox>
                               
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
                 <table   bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="90%" id="tblUpload" runat="server">
                    <tr>
                    <td>
			            <table  cellSpacing="0" cellPadding="0" width="100%" border="0">
                            <tr>
                                <td colspan="2">

                                    <table width="100%" border="0" cellSpacing="5" cellPadding="5" id="uploadDT" runat="server" >
                                        <tr>
                                            <td  class="copy10grey" align="right" >
                                                Upload file: &nbsp;</td>
                                            <td align="left" >
                                                <asp:FileUpload ID="flnUpload" runat="server" CssClass="txfield1" Width="55%" /></td>
                                        </tr>
                                        <tr  >
                                            <td class="copy10grey" align="right">
                                                File format sample: &nbsp;
                                                     </td>
                                            <td class="copy10grey" align="left">
                                                 
                                              <b>Seq.No.,WareHouseLocation,BoxID,Quantity</b> <asp:LinkButton ID="lnkDownload" runat="server"   Text="Download file format" OnClick="lnkDownload_Click"></asp:LinkButton>
                                                

                                            </td>
                                         </tr>
                                            <tr  valign="top">
                                            <td class="copy10grey" align="right">
                                                Comment: &nbsp;
                                                     </td>
                                            <td class="copy10grey" align="left">
                                                <asp:TextBox ID="txtComment" runat="server" CssClass="copy10grey" Width="70%" Height="50px" TextMode="MultiLine"></asp:TextBox>

                                            </td>
                                         </tr>
 
                                    </table>
                                </td>
                            </tr>
                            <tr><td colspan="2">
                            <hr style="width:100%" />
                            
                            </td></tr>                            
                             <tr>
                                <td colspan="2" align="center">
                                
                                <asp:Button ID="btnUpload"  Width="190px" CssClass="button" runat="server" OnClick="btnUpload_Click" Text="Validate Uploaded file" OnClientClick="return Validate(1);"  />

                                &nbsp;<asp:Button ID="btnSubmit" Visible="false" Width="190px"  CssClass="button" runat="server" OnClick="btnSubmit_Click" Text="Submit" OnClientClick="return Validate(2);"/>
                               <%-- &nbsp;<asp:Button ID="btnViewTracking" Visible="false" Width="190px"  CssClass="button" runat="server" OnClick="btnViewAssignedPos_Click" Text="View UPDATED fulfillment" />
--%>
                                &nbsp;<asp:Button ID="btnCancel" runat="server"  CssClass="button" Text="Cancel" OnClick="btnCancel_Click" />
                                    </td>
                            </tr>
                            
                            <tr><td colspan="2">
                            
                            </td></tr>                            
                            
                            
                       </table>
                            
                    </td>
                    </tr>
                           
                 </table>
                    <table cellpadding="0" cellspacing="0" width="90%" align="center">
                             <tr>
                                <td colspan="2" align="right" style="height:8px; vertical-align:bottom">
                                <strong>   <asp:Label ID="lblCount" CssClass="copy10grey" runat="server" ></asp:Label> &nbsp;&nbsp;</strong> 
                                </td>
                             </tr>
                             <tr>
                                <td colspan="2" align="center">
                                
                                <asp:GridView ID="gvMSL" runat="server" Width="100%" GridLines="Both"   AutoGenerateColumns="false"
                                    PageSize="100" AllowPaging="false" 
                                     >
                                        <RowStyle BackColor="Gainsboro" />
                                        <AlternatingRowStyle BackColor="white" />
                                        <HeaderStyle  CssClass="buttonlabel" ForeColor="white"/>
                                        <FooterStyle CssClass="white"  />
                                        <PagerStyle ForeColor="#636363" CssClass="copy10grey" HorizontalAlign="Left" />
                                        <Columns>
                                             <asp:TemplateField HeaderText="S.No." ItemStyle-CssClass="copy10grey"  ItemStyle-Width="1%" HeaderStyle-CssClass="buttonlabel">
                                                <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1%>                  
                                                </ItemTemplate>
                                            </asp:TemplateField> 

                                            <asp:TemplateField HeaderText="Warehouse Location" SortExpression="MSLNumber"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" 
                                                ItemStyle-Width="12%">
                                                <ItemTemplate><%#Eval("WareHouseLocation")%></ItemTemplate>
                                            </asp:TemplateField>                                            

                                            <asp:TemplateField HeaderText="BoxID" SortExpression="ESN"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" 
                                                ItemStyle-Width="12%">
                                                <ItemTemplate>
                                                            <%#Eval("BoxID")%>

<%--                                                    <span class="errormessage"><%#Eval("ErrorMessage")%></span>   --%>
                                                    
                                                </ItemTemplate>
                                            </asp:TemplateField> 
                                             

                                            <asp:TemplateField HeaderText="Quantity" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                <%#Eval("Quantity")%>
                                                </ItemTemplate>
                                            </asp:TemplateField> 

                                            <asp:TemplateField HeaderText="Inspected"   ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkInspected" runat="server" Checked='<%#Eval("Inspected")%>' CssClass="copy10grey" />
                                                
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                           
                                    
                                        </Columns>
                                    </asp:GridView>
                                    

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
                                <td colspan="2" align="center">
                                
                                
                                &nbsp;<asp:Button ID="btnBack2Search" Visible="true" Width="190px"  CssClass="button" runat="server" OnClick="btnBack2Search_Click" Text="Back to Search" />
                               
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
