<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddNewItem.aspx.cs" Inherits="avii.AddNewItem" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>
<%@ Register TagPrefix="menu" TagName="Menu" Src="~/Controls/Header.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Add New Item</title>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>

    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.4.0/jquery.min.js"></script>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/jquery-ui.min.js"></script>
    <link href="https://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/themes/start/jquery-ui.css" rel="stylesheet" type="text/css" />

<%--<script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/jquery-ui.js" type="text/javascript"></script>
<link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/themes/start/jquery-ui.css" rel="stylesheet" type="text/css" />--%>
	<%--<script type="text/javascript" src="../JQuery/jquery.min.js"></script>
    <script type="text/javascript" src="../JQuery/jquery-ui.min.js"></script>--%>
	<script type="text/javascript" src="../JQuery/jquery.blockUI.js"></script>
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
            function skuvalidation(e) {

                var regex = new RegExp("^[a-zA-Z0-9-]+$");
                var str = String.fromCharCode(!e.charCode ? e.which : e.charCode);
                if (regex.test(str)) {
                    return true;
                }
                e.preventDefault();
                return false;
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
</head>
<body bgcolor="#ffffff" leftmargin="0" rightmargin="0" topmargin="0" >
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
    <table cellspacing="0" cellpadding="0" border="0" align="center" width="95%" >
        <tr valign="top">
            <td>
                <table align="center" style="text-align:left" width="100%">
                <tr class="buttonlabel" align="left">
                <td>&nbsp;Add Line Items</td></tr>
             </table>
            </td>
        </tr>
        <tr>
        <td>
            <asp:UpdatePanel ID="UpdatePanel1"  runat="server" ChildrenAsTriggers="true" >
            <ContentTemplate>
                <asp:HiddenField ID="hdnCode" runat="server" />
                <asp:Label ID="lblMsg" runat="server" CssClass="errormessage"></asp:Label>

                
        <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="100%" align="center" >
           <tr bordercolor="#839abf">
                <td>
                    <table width="100%" cellSpacing="2" cellPadding="2">
                    <tr>
                        <td class="copy10grey" width="22%" align="left">
                            Fulfillment#:
                        </td>
                        <td width="1%">&nbsp;</td>
                        <td  align="left" width="32%" >
                            <asp:Label ID="lblPO" CssClass="copy10grey" runat="server" ></asp:Label>
                        </td>
                        <td class="copy10grey" width="15%" align="right">
                       <strong>  Status:</strong>
                        </td>
                        <td width="1%">&nbsp;</td>
                        <td  align="left" width="29%" >
                             <asp:Label ID="lblvStatus" CssClass="copy10grey" runat="server" ></asp:Label>
                            <%--<asp:Label ID="lblvAvso" CssClass="copy10grey" runat="server" ></asp:Label>--%>
                        </td>
                   
                    </tr>
                    </table>
                </td>
            </tr>
        </table>
                <br />

                <table width="100%" cellSpacing="0" cellPadding="0" >
                            
                <tr>
                <td align="left" class="buttonlabel" style="width:90%" >
                    Line items
                </td>
    
                <td align="right" style="width:10%">

                    <asp:Label ID="lblPODCount" runat="server" CssClass="copy10greyb" ></asp:Label>&nbsp;
                </td>
                </tr>
                <tr>
                    <td colspan="2">

                    <asp:GridView ID="gvPODetail"  BackColor="White" Width="100%" Visible="true"  AllowPaging="true"
                    OnPageIndexChanging="gvPODetail_PageIndexChanging"    PageSize="20"
                        AutoGenerateColumns="false" Font-Names="Verdana" runat="server" DataKeyNames="PodID"
                        GridLines="Both" OnRowDeleting = "GridView2_RowDeleting" OnRowDeleted = "GridView2_RowDeleted"
                         AllowSorting="false" OnRowEditing="gvPODetail_RowEditing"  OnRowUpdating = "gvPODetail_RowUpdating" 
                            OnRowUpdated = "gvPODetail_RowUpdated" OnRowCancelingEdit="gvPODetail_RowCancelingEdit"
                        BorderStyle="Double" BorderColor="#0083C1" OnRowDataBound="gvPODetail_RowDataBound">
                        <RowStyle BackColor="Gainsboro" />
                        <AlternatingRowStyle BackColor="white" />
                        <HeaderStyle  CssClass="buttongrid" ForeColor="white"/>
                        <FooterStyle CssClass="white"  />
                        <Columns>
         	                <asp:TemplateField HeaderText="Line#"  ItemStyle-CssClass="copy10grey"  HeaderStyle-Width="3%" ItemStyle-Wrap="false"  ItemStyle-width="3%">
                                <ItemTemplate>
                                   <%--<%# Container.DataItemIndex +  1 %> , --%>
                                   <%#Eval("LineNo")%>
                                </ItemTemplate> 
                
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="SKU#"  ItemStyle-CssClass="copy10grey"  HeaderStyle-Width="7%" ItemStyle-Wrap="false"  ItemStyle-width="10%">
                                <ItemTemplate>
                                    <%# Convert.ToString(Eval("ItemCode")).ToUpper()%>
                                </ItemTemplate> 
                
                            </asp:TemplateField>
                                                                                                                                  
                                            
                            <asp:TemplateField HeaderText="Qty"  ItemStyle-CssClass="copy10grey" ItemStyle-Width="3%">
                                <ItemTemplate><%#Eval("Quantity")%></ItemTemplate>
                                 <EditItemTemplate>
                                    <asp:TextBox ID="txtQty" CssClass="copy10grey" MaxLength="5" onkeypress="return isNumberKey(event);" onchange="return isQuantity(this);" 
                                    Enabled='<%# Convert.ToInt32(Eval("StatusID")) == 1?true:false %>' Width="99%" Text='<%# Eval("Quantity") %>' runat="server"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                 
                                                                                                                                        
           
                            <asp:TemplateField HeaderText="Status"   ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                                <ItemTemplate>
                                <%# Convert.ToInt32(Eval("StatusID")) == 1 ? "Pending" : Convert.ToInt32(Eval("StatusID")) == 2 ? "Processed" : Convert.ToInt32(Eval("StatusID")) == 3 ? "Shipped" : Convert.ToInt32(Eval("StatusID")) == 4 ? "Closed" : Convert.ToInt32(Eval("StatusID")) == 5 ? "Return" : Convert.ToInt32(Eval("StatusID")) == 9 ? "Cancel" : Convert.ToInt32(Eval("StatusID")) == 6 ? "On Hold" : Convert.ToInt32(Eval("StatusID")) == 7 ? "Out of Stock" : Convert.ToInt32(Eval("StatusID")) == 8 ? "In Process" : Convert.ToInt32(Eval("StatusID")) == 10 ? "Partial Processed" : Convert.ToInt32(Eval("StatusID")) == 11 ? "Partial Shipped" : "Pending"%>
                                <%-- <br /> <%#Eval("PODStatus")%>--%>
                                    <asp:HiddenField ID="hdnStatus" Value='<%# Eval("StatusID") %>' runat="server" />
                   
                                                
                                </ItemTemplate>
                            </asp:TemplateField> 
            
             
              
                            <asp:CommandField  AccessibleHeaderText="EditPOD" Visible="false"  HeaderText="Edit" ItemStyle-Width="5%" ShowEditButton="false" HeaderStyle-CssClass="buttongrid" ControlStyle-CssClass="linkgrid"    ItemStyle-HorizontalAlign="Center"/>
		
                            <asp:TemplateField HeaderText=""  ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                     <asp:ImageButton ID="imgDelPoD" runat="server" OnClientClick="return confirm('Are you sure you want to delete?')"  
                                     CommandName="Delete" AlternateText="Delete POD" ImageUrl="~/images/delete.png" />
                                </ItemTemplate>
                            </asp:TemplateField>                                                   
			                                
                        </Columns>
                    </asp:GridView>
                    </td>
                </tr>
                </table>
                <br />
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
                            <%--<asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="button" CausesValidation="false"  />--%>
                            <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="button" Visible="true" OnClientClick="return close_window();"  />
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
                <tr><td    align="right" ><asp:Button ID = "Button1"  CssClass="button" runat="server" Text="Close" /></td></tr>
                <tr><td class="buttonlabel"  ><strong> Select SKU</strong></td>
                
                </tr>
                 <tr><td></td></tr>
                </table>
                        
                        <asp:Repeater ID="rptItemCode" runat="server"  >
                        
                            <HeaderTemplate>
                            <table   align="center" width="99%">
                                
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
        	
        
    </table>
        <br />
        <table width="100%" align="center">
                <tr>
                    <td align="center">
                         
                    </td>
                </tr>
                </table>
	
        
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
