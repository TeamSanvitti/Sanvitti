<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FinalSKUs.aspx.cs" Inherits="avii.Product.FinalSKUs" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="/Controls/Footer.ascx" %>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="/Controls/Header.ascx" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Manage Kitted SKU</title>
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
	
    <link href="../aerostyle.css" type="text/css" rel="stylesheet" />
    	<style type="text/css">
		.ui-widget{font-size:11px !important;}
		.ui-state-error-text{margin-left: 10px}
	</style>
    
    
      <style>
.progresss {
          position: fixed !important;
          z-index: 9999 !important;
          top: 0px !important;
          left: 0px !important;
          background-color: #EEEEEE !important;
          width: 100% !important;
          height: 100% !important;
          filter: Alpha(Opacity=80) !important;
          opacity: 0.80 !important;
          -moz-cpacity: 0.80 !important;
      }
.modal
{
    position: fixed;
    top: 0;
    left: 0;
    background-color: black;
    z-index: 100000000;
    opacity: 0.8;
    filter: alpha(opacity=80);
    -moz-opacity: 0.8;
    min-height: 100%;
    width: 100%;
}
.loadingcss
{    
    font-size: 18px;
    /*border: 1px solid red;*/
    /*width: 200px;
    height: 100px;*/
    display: none;
    position: fixed;
    /*background-color: White;*/
    z-index: 100000001;
    background-color:#CF4342;
}

  </style>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#divESN").dialog({
                autoOpen: false,
                modal: false,
                minHeight: 400,
                height: 650,
                width: 1350,
                resizable: false,
                open: function (event, ui) {
                    $(this).parent().appendTo("#divContainer");
                }
            });



        });


        function closeDialog() {
            //Could cause an infinite loop because of "on close handling"
            $("#divESN").dialog('close');
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
            left = 100;
            $("#divESN").dialog("option", "title", title);
            $("#divESN").dialog("option", "position", [left, top]);
            $("#divESN").dialog('open');

        }


        function openDialogAndBlock(title, linkID) {

            openDialog(title, linkID);
            //alert('2')
            //block it to clean out the data
            $("#divESN").block({
                message: '<img src="../images/async.gif" />',
                css: { border: '0px' },
                fadeIn: 0,
                //fadeOut: 0,
                overlayCSS: { backgroundColor: '#ffffff', opacity: 1 }
            });
        }

        function unblockDialog() {
            $("#divESN").unblock();
        }


    </script>

    <script type="text/javascript">
        function ValidateDisplayName(obj) {
            //alert(obj.value);
            IsDisplayName = document.getElementById(obj.id.replace('txtName', 'hdIsDisplayName'));
            //alert(IsDisplayName.value);

            if (IsDisplayName.value == "True") {
                if (obj.value == "") {
                    alert('Display name is required!');
                }

            }
            else {
                obj.value = "";
                alert('Display name not allowed for this product');
                
            }

        }
        function alphaNumericCheck(fieldVal) {
            if ((105 >= fieldVal.keyCode && fieldVal.keyCode >= 96)
                || (90 >= fieldVal.keyCode && fieldVal.keyCode >= 65)
                || (57 >= fieldVal.keyCode && fieldVal.keyCode >= 48)
                || (122 >= fieldVal.keyCode && fieldVal.keyCode >= 97)) {
                return true;
            }
            else {
                return false;
            }
        }

        function SetQuantity(obj) {
            objQty = document.getElementById(obj.id.replace('chkSel', 'txtQty'));
            objESN = document.getElementById(obj.id.replace('chkSel', 'hdIsESNRequired'));
            //alert(objESN.value);

            var hdESN = document.getElementById('hdnIsESNRequired');
         //   var hdnIsKittedBox = document.getElementById('hdIsKittedBox');
          //  var hdIsESNRequired = document.getElementById('hdIsESNRequired');
           // alert(hdnIsKittedBox.value);
           // alert(hdIsESNRequired.value);
            //if (hdnIsKittedBox.value == '' && hdIsESNRequired.value == '' && objESN.value == 'True') {

            //}

            //if (obj.checked) {
            //   // alert(objESN.value);
            //    if (objESN.value == 'True') {
            //        if (hdESN.value == '1') {
            //            obj.checked = false;
            //            alert('Multiple ESN is not allowed!');
            //        }
            //        else {
            //            hdESN.value = '1';
            //        }
            //    }
            //}
            //else {
            //    if (objESN.value == 'True') {
            //        if (hdESN.value == '1') {
            //            hdESN.value = '';
            //        }
            //    }
            //}

            if (obj.checked) {
                if (objQty.value == '') {
                    objQty.value = 1;
                }                
            }
            else
                objQty.value = '';

            var IsDisable = true;
            var c = document.getElementsByTagName('input');
            for (var i = 0; i < c.length; i++) {
                if (c[i].type == 'checkbox') {
                    if (c[i].checked) {
                        IsDisable = false;
                       // alert(IsDisable)
                    }
                }
            }

            document.getElementById("<% =btnSubmit.ClientID %>").disabled = IsDisable
            
        }

        function Validate() {

            var company = document.getElementById("<% =dpCompany.ClientID %>");
            if (company.selectedIndex == 0) {
                alert('Customer is required!');
                return false;

            }
            var sku = document.getElementById("<% =ddlSKU.ClientID %>");
            if (company.selectedIndex > 0 && sku.selectedIndex > 0) {
                ShowSendingProgress();
            }
            

        }
        function ValidateSubmit() {

            var company = document.getElementById("<% =dpCompany.ClientID %>");
            if (company.selectedIndex == 0) {
                alert('Customer is required!');
                return false;

            }
            var IsDisable = true;
            var c = document.getElementsByTagName('input');
            for (var i = 0; i < c.length; i++) {
                if (c[i].type == 'checkbox') {
                    if (c[i].checked) {
                        IsDisable = false;
                        // alert(IsDisable)
                    }
                }
            }
            if (IsDisable) {

                alert('Select atleast one raw SKU!');
                return false;

            }

            if (!IsDisable && company.selectedIndex > 0) {

                ShowSendingProgress();
            }

        }

        function isQuantity(obj) {
                
                if (obj.value == '0') {
                    alert('Quantity can not be zero');
                    obj.value = '1';
                    return false;
                }
                //if (obj.value == '') {
                //    alert('Quantity can not be empty');
                //    obj.value = '1';
                //    return false;
                //}
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
	<table cellSpacing="0" cellPadding="0"  border="0" align="center" width="100%">
		<tr>
			<td>
			<head:menuheader id="HeadAdmin" runat="server"></head:menuheader>
			</td>
		</tr>
     </table>
    
    <table  cellSpacing="1" cellPadding="1" width="100%">
        <tr id="trAdmin" runat="server">
		    <td colSpan="6" bgcolor="#dee7f6" class="buttonlabel">&nbsp;&nbsp;Manage Kitted SKU
		    </td>
        </tr>
        <tr id="trCust" runat="server" visible="false">
		    <td colSpan="6" bgcolor="#dee7f6" class="buttonlabel">&nbsp;&nbsp;Kitted SKU
		    </td>
        </tr>

    </table>   
    

    <div id="divContainer">	
            
			<div id="divESN" style="display:none">
					
				<asp:UpdatePanel ID="upnlESN" runat="server">
					<ContentTemplate>
                   
						<asp:PlaceHolder ID="phrESN" runat="server">
                        <table width="100%" border="0">
                                
                                <tr>
                                    <td colspan="2">
                                
                                    <asp:Panel ID="pnlLog" runat="server" Width="100%">
                                        <asp:Label ID="lblLogMsg" runat="server" Width="100%" CssClass="errormessage"></asp:Label>
                                        
    <table  bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="100%" align="center">
    <tr  bordercolor="#839abf">
        <td>
        <table cellpadding="10" cellspacing="10" width="100%">
        <tr>
           <td class="copy10grey" align="right" width="17%">
               Category Name:
           </td>
           <td width="34%">
               <asp:Label ID="lblCategoryName" runat="server" CssClass="copy10grey"></asp:Label>
           </td>
            <td class="copy10grey" align="right" width="17%">
                Model Number:
           </td>
            <td width="32%" class="copy10grey" align="left">
                <asp:Label ID="lblModelNumber" runat="server" CssClass="copy10grey"></asp:Label>
           </td>
        </tr>
        <tr>
           <td class="copy10grey" align="right" width="17%">
               Product Name:
           </td>
           <td width="34%">
               
                <asp:Label ID="lblProductName" runat="server" CssClass="copy10grey"></asp:Label>
           </td>
            <td class="copy10grey" align="right" width="17%">

           </td>
            <td width="32%" class="copy10grey" align="right">
                
           </td>
        </tr>
        </table>
        </td>
    </tr>
    </table>
        <br />

                <asp:GridView ID="gvLog" runat="server" AutoGenerateColumns="false" Width="100%"  AllowPaging="false" PageSize="20"
                AllowSorting="false" >
                <RowStyle BackColor="Gainsboro" />
                <AlternatingRowStyle BackColor="white" />
                <HeaderStyle  CssClass="buttongrid" ForeColor="white"/>
                <RowStyle  CssClass="copy10grey" />
                <FooterStyle CssClass="white"  />
                <%--<PagerStyle ForeColor="#636363" CssClass="copy10grey" HorizontalAlign="Left" />--%>                
                <Columns>
                    <asp:TemplateField HeaderText="S.No." ItemStyle-CssClass="copy10grey"  ItemStyle-Width="1%" HeaderStyle-CssClass="buttongrid">
                    <ItemTemplate>
                            <%# Container.DataItemIndex + 1%>                  
                    </ItemTemplate>
                    </asp:TemplateField>                    
                     
                    <%--<asp:TemplateField  ItemStyle-Width="8%" HeaderStyle-CssClass="buttongrid" HeaderStyle-HorizontalAlign="left" HeaderText="Category Name" 
                        ItemStyle-VerticalAlign="top">
                        <ItemTemplate>
                            <%# Eval("CategoryName") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField  ItemStyle-Width="15%"  HeaderStyle-CssClass="buttongrid" HeaderStyle-HorizontalAlign="left" HeaderText="Product Name" 
                        ItemStyle-VerticalAlign="top">
                        <ItemTemplate>
                            <%# Eval("ProductName") %>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                    <asp:TemplateField  ItemStyle-Width="6%"  HeaderStyle-CssClass="buttongrid" HeaderStyle-HorizontalAlign="left" HeaderText="Create Date" 
                        ItemStyle-VerticalAlign="top">
                        <ItemTemplate>
                            <%# Convert.ToDateTime(Eval("CreateDate")).ToString("MM/dd/yyyy") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField  ItemStyle-Width="10%" HeaderStyle-CssClass="buttongrid" HeaderStyle-HorizontalAlign="left" HeaderText="SKU" 
                        ItemStyle-VerticalAlign="top">
                        <ItemTemplate>
                            <%# Eval("SKU") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField  ItemStyle-Width="51%"  HeaderStyle-CssClass="buttongrid" HeaderStyle-HorizontalAlign="left" HeaderText="Request Data" 
                        ItemStyle-VerticalAlign="top">
                        <ItemTemplate>
                            <%# Eval("RequestData") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField  ItemStyle-Width="8%"  HeaderStyle-CssClass="buttongrid" HeaderStyle-HorizontalAlign="left" HeaderText="Response Data" 
                        ItemStyle-VerticalAlign="top">
                        <ItemTemplate>
                            <%# Eval("ResponseData") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField  ItemStyle-Width="3%" HeaderStyle-CssClass="buttongrid" HeaderStyle-HorizontalAlign="left" HeaderText="Status" 
                        ItemStyle-VerticalAlign="top">
                        <ItemTemplate>
                            <%# Eval("Status") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField  ItemStyle-Width="5%" HeaderStyle-CssClass="buttongrid" HeaderStyle-HorizontalAlign="left" HeaderText="User Name" 
                        ItemStyle-VerticalAlign="top">
                        <ItemTemplate>
                            <%# Eval("UserName") %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField ItemStyle-Width="4%" HeaderStyle-CssClass="buttongrid" HeaderStyle-HorizontalAlign="left" HeaderText="Action" 
                        ItemStyle-VerticalAlign="top">
                        <ItemTemplate>
                            <%# Eval("ActionName") %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    </Columns>
                    </asp:GridView>
            
                                    
                            </asp:Panel>
                            </td>
                                </tr>
                                </table>
                                
                        </asp:PlaceHolder>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    <asp:UpdatePanel ID="UpdatePanel1"  runat="server" UpdateMode="Conditional" >
        <ContentTemplate>
    
             <table cellSpacing="0" cellPadding="0" align="center" width="95%" border="0">
    	<tr>                    
            <td colspan="2">
                <asp:Label ID="lblMsg" runat="server" Width="100%" CssClass="errormessage"></asp:Label>
            </td>
        </tr>               
        <tr>
            <td align="center">

                <asp:Panel  ID="Panel1" runat="server"  DefaultButton="btnSearch" >
                <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="90%">
                    <tr>
                    <td>
			            <table  cellSpacing="5" cellPadding="5" width="100%" border="0">
                            <tr>
                                <td  class="copy10grey" align="right" width="42%" >
                                    Customer: &nbsp;</td>
                                <td align="left" >
                                    <asp:DropDownList ID="dpCompany" CssClass="copy10grey" runat="server" Width="40%"
                                    OnSelectedIndexChanged="dpCompany_SelectedIndexChanged"  
                                    AutoPostBack="true">
									</asp:DropDownList>
                            </tr>
                            <tr runat="server" id="trSKU">
                                <td class="copy10grey"  width="42%" align="right">
                                    SKU: &nbsp;
                                </td>                                
                                <td  align="left">
                                <asp:DropDownList ID="ddlSKU" runat="server" class="copy10grey" Width="40%"
                                 OnSelectedIndexChanged="ddlSKU_SelectedIndexChanged" AutoPostBack="true"   >
                                </asp:DropDownList>                                           

                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Panel ID="pnlSearch" runat="server" Visible="true" >
                                    <table cellpadding="0" cellspacing="0" width="100%">
                                        <tr><td>
                                        <hr style="width:100%" />                            
                                        </td></tr>   
                                         <%--<tr>                    
                                            <td align="left">
                                                <asp:Label ID="lblConfirm" runat="server" Width="100%" CssClass="errorGreenMsg"></asp:Label></td>
                                        </tr>
                                        --%>
                                    
                                         <tr>
                                            <td  align="center">
                                 <div class="loadingcss" align="center" id="modalSending">
                                    <img src="/Images/ajax-loaders.gif" alt=""  /> Loading...
                                </div>
                                
                                            <asp:Button ID="btnSearch" Visible="true" Width="190px"  CssClass="button" runat="server" OnClick="btnSearch_Click" Text="Search" OnClientClick="return Validate(2);" />
                                            
                                            &nbsp;<asp:Button ID="btnClear" runat="server"  CssClass="button" Text="Clear" OnClick="btnClear_Click" />
                                                </td>
                                        </tr>
                                        <%--<tr><td>
                                        <hr style="width:100%" />
                            
                                        </td></tr>   --%>
                                    </table>

                                    </asp:Panel>
                                </td>
                            </tr>
                            </table>
                        </td>
                        </tr>
                    </table>
                    </asp:Panel>

                <asp:Panel ID="plnSKU" runat="server" Visible="false"  >  
                    
                    <table  cellSpacing="0" cellPadding="0" width="90%">
                        <tr>
                            <td align="left">

                <strong>   
                <asp:Label ID="lblCount" CssClass="copy10grey" runat="server" ></asp:Label> &nbsp;&nbsp;
                </strong>               
           
                            </td>
                                <td  align="right">
                                     &nbsp;<asp:Button ID="btnViewLog" runat="server"  CssClass="button" Text="View Log" 
                                         Visible="false" OnClientClick="openDialogAndBlock('View Log', 'btnViewLog')"
                                         OnClick="btnViewLog_Click" />
                                           
                                
                                &nbsp;<asp:Button ID="btnSubmit" Visible="false" Width="190px"  CssClass="button" runat="server" OnClick="btnSubmit_Click" Text="Submit" 
                                    OnClientClick="return ValidateSubmit();"/>
                               <%-- &nbsp;<asp:Button ID="btnViewTracking" Visible="false" Width="190px"  CssClass="button" runat="server" OnClick="btnViewAssignedPos_Click" Text="View UPDATED fulfillment" />
--%>
                                &nbsp;<asp:Button ID="btnCancel" runat="server"  CssClass="button" Text="Clear" OnClick="btnClear_Click" />
                                    </td>
                            </tr>
                    </table>
                    <table bordercolor="#839abf" border="0" cellSpacing="0" cellPadding="0" width="90%">
                    <tr>
                    <td>
			            <table  cellSpacing="0" cellPadding="0" width="100%" border="0">
               
                            <tr><td colspan="2">
                         
                        <table  bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="100%" align="center">
                        <tr  bordercolor="#839abf">
                            <td>
                            <table cellpadding="0" cellspacing="0" width="100%">
                             <%--<tr>
                                <td colspan="2" align="right" style="height:8px; vertical-align:bottom">
                                <strong>   <asp:Label ID="lblCount" CssClass="copy10grey" runat="server" ></asp:Label> &nbsp;&nbsp;</strong> 
                                </td>
                             </tr>--%>
                             <tr>
                                <td colspan="2" align="center">
                                <asp:HiddenField ID="hdnIsESNRequired"  runat="server" />
                                    <asp:HiddenField ID="hdIsKittedBox"  runat="server" />
                                    <asp:HiddenField ID="hdIsESNRequired"  runat="server" />
                                        
                            <asp:Repeater ID="rptSKUs" runat="server"  >
                            <HeaderTemplate>
                            <table  cellSpacing="2" cellPadding="2" width="100%" align="center" >
                                 <tr >
                                <td class="buttongrid" width="2%">
                                
                                </td>
                                <td class="buttongrid" align="left" width="9%">
                                Category Name
                                </td>
                                <td class="buttongrid" align="left" width="11%">
                                SKU
                                </td>
                                <td class="buttongrid" align="left" width="23%">
                                Product Name
                                </td>
                                     <td class="buttongrid" align="left" width="12%">
                                Mapped Category Name
                                </td>
                                <td class="buttongrid" align="left" width="10%">
                               Mapped SKU
                                </td>
                                <td class="buttongrid" align="left" width="20%">
                                Mapped Product Name
                                </td>
                                <td class="buttongrid" align="left" width="4%">
                                Quantity
                                </td>
                                <td class="buttongrid" align="left" width="10%">
                                Display Name
                                </td>
                                <%--<td class="buttongrid" align="left" width="4%">
                                 Name Required
                                </td>--%>
                               </tr>
                               </HeaderTemplate>
                                  <ItemTemplate>
                                  <tr class="<%# Container.ItemIndex % 2 == 0 ? "alternaterow" : "" %>">

                                    <td>
                                        
                                        <asp:HiddenField ID="hdIsESNRequired" Value='<%# Eval("IsESNRequired") %>' runat="server" />
                                        <asp:CheckBox ID="chkSel" onclick="SetQuantity(this);" Checked='<%# Convert.ToInt32(Eval("Quantity"))==0 ? false : true %>' 
                                            CssClass="copy10grey" runat="server" />
                                    </td>
                                      <td align="left" class="copy10grey">
                                    
                                       <%# Eval("CategoryName") %>

                                        
													
                                    </td>
                                    <td align="left">
                                    <asp:HiddenField ID="hdItemCompanyGUID" Value='<%# Eval("ItemCompanyGUID") %>' runat="server" />
                        
                                       
                                        <asp:Label ID="lblSKU" Text='<%# Eval("SKU") %>' CssClass="copy10grey"  runat="server"></asp:Label>

													
                                    </td>
                                      <td align="left" class="copy10grey">
                                    
                                       <%# Eval("ProductName") %>

                                        
													
                                    </td>
                                      <td align="left" class="copy10grey">
                                    
                                       <%# Eval("MappedCategoryName") %>

                                        
													
                                    </td>
                                      <td align="left" class="copy10grey">
                                    
                                       <%# Eval("MappedSKU") %>

                                        
													
                                    </td>
                                      <td align="left" class="copy10grey">
                                    
                                       <%# Eval("MappedProductName") %>

                                        
													
                                    </td>
                                    <td align="left">
                                        <%--<asp:TextBox ID="txtQty" Text='<%# Convert.ToString(Eval("Quantity"))=="0" ? "1" : Eval("Quantity")%>' MaxLength="2" Width="40%" onkeypress="return isNumberKey(event);" onchange="return isQuantity(this);"  CssClass="copy10grey"   runat="server"></asp:TextBox>--%>
                                        <asp:TextBox ID="txtQty" Text='<%# Convert.ToString(Eval("Quantity"))=="0" ? "" : Eval("Quantity")%>' MaxLength="2" Width="40%" onkeypress="return isNumberKey(event);" onchange="return isQuantity(this);"  CssClass="copy10grey"   runat="server"></asp:TextBox>
                                    </td>
                                    <td align="left">
                                        <asp:HiddenField ID="hdIsDisplayName" runat="server" Value='<%# Eval("IsDisplayName")%>' />
                                        <asp:TextBox ID="txtName" Text='<%# Eval("DisplayName")%>' MaxLength="30" Width="100%" 
                                            onchange="return ValidateDisplayName(this);" onkeypress="return alphaNumericCheck(event);" CssClass="copy10grey"   runat="server"></asp:TextBox>
                                    </td>
                                    <%--<td align="left">
                                        <asp:CheckBox ID="chkName"  CssClass="copy10grey"   runat="server"></asp:CheckBox>
                                    </td>
                                    --%>
                                
                                   
                                
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
                            
                            </td></tr>                            
                            
                            <%--<tr><td colspan="2">
                            <hr style="width:100%" />
                            
                            </td></tr>  
                                                      
                            <tr id="trHr" runat="server"><td colspan="2">
                            <hr style="width:100%" />
                            
                            </td></tr>                --%>            
                             
                       </table>
                            
                    </td>
                    </tr>
                           
                 </table>
                  </asp:Panel>
            </td>
        </tr>
            
        </table>


         </ContentTemplate>
            
    </asp:UpdatePanel>
        <asp:UpdatePanel ID="upnlJsRunner" UpdateMode="Always" runat="server">
			<ContentTemplate>
				<asp:PlaceHolder ID="phrJsRunner" runat="server"></asp:PlaceHolder>
			</ContentTemplate>
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
    
        <script type="text/javascript">
            function ShowSendingProgress() {
                var modal = $('<div  />');
                modal.addClass("modal");
                modal.attr("id", "modalSending");
                $('body').append(modal);
                var loading = $("#modalSending.loadingcss");
                //alert(loading);
                loading.show();
                var top = '300px';
                var left = '820px';
                loading.css({ top: top, left: left, color: '#ffffff' });

                var tb = $("maintbl");
                tb.addClass("progresss");
                // alert(tb);

                return true;
            }
            //background-color:#CF4342;

            function StopProgress() {

                $("div.modal").hide();

                var tb = $("maintbl");
                tb.removeClass("progresss");


                var loading = $(".loadingcss");
                loading.hide();
            }
        </script>
    </form>
</body>
</html>
