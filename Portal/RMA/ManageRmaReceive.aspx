<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageRmaReceive.aspx.cs" Inherits="avii.RMA.ManageRmaReceive" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>
<%@ Register TagPrefix="menu" TagName="Menu" Src="~/Controls/Header.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Manage RMA Receive</title>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>

    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.4.0/jquery.min.js"></script>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/jquery-ui.min.js"></script>
    <link href="https://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/themes/start/jquery-ui.css"
    rel="stylesheet" type="text/css" />

     <link rel="stylesheet"  type="text/css" href="../fullfillment/calendar/dhtmlgoodies_calendar.css" media="screen" />
	<script type="text/javascript" src="../fullfillment/calendar/dhtmlgoodies_calendar.js"></script>  
    
            
    <script type="text/javascript">
        function SelectAll(id) {
            // alert(document.getElementById(id).checked);
            var check = document.getElementById(id).checked;
           // alert(check);

            var elements = document.getElementsByTagName('input');
            // iterate and change status
            for (var i = elements.length; i--;) {
                if (elements[i].type == 'checkbox') {
                    elements[i].checked = check;
                }
            }
           // $(':checkbox').prop('checked', check);



        }


        function ValidateQty(obj) {
            qty = document.getElementById(obj.id.replace('txtQtyReceived', 'hdQty'));
            if (obj.value > qty.value) {
                alert('Receive quantity canot be greater than ' + qty.value)
                obj.value = qty.value;
                return false;

            }
            return true;

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
        

    </script>
</head>
<body bgcolor="#ffffff" leftmargin="0" rightmargin="0" topmargin="0">
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
            <td>&nbsp;Manage RMA Receive</td></tr>
        </table>
        <asp:UpdatePanel ID="UpdatePanel1"  runat="server" UpdateMode="Conditional" >
        <ContentTemplate>
            <div style="min-height:450px">
        <table cellSpacing="0" cellPadding="0" align="center" width="100%" border="0">
    		<tr>                    
                <td>
                    <asp:Label ID="lblMsg" runat="server" Width="100%" CssClass="errormessage"></asp:Label>
                </td>
            </tr>
            <tr id="trSearch" runat="server">
                <td align="center">
                <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="95%">
                    <tr>
                    <td>
			            <table id="Table2" cellSpacing="5" cellPadding="5" width="100%" border="0">
                        <tr>
                            <td> 
                                <asp:Panel ID="pnlSearch" runat="server" DefaultButton="btnSearch">
                                <table cellSpacing="5" cellPadding="5" width="100%" border="0">
                                <tr>
                                    <td  class="copy10grey" align="right" width="12%">
                                            Customer: &nbsp;</td>
                                    <td align="left"  width="18%">
                                        <asp:DropDownList ID="dpCompany" CssClass="copy10grey" runat="server" Width="90%">
								        </asp:DropDownList>
                                    </td>
                                    <td  class="copy10grey" align="right"  width="12%">
                                            RMA#: &nbsp;</td>
                                    <td align="left"  width="18%">
                                        <asp:TextBox    ID="txtRMA" MaxLength="20" onkeypress="return isNumberHiphen(event);" CssClass="copy10grey" runat="server" Width="90%"></asp:TextBox>
									
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
            
            <tr id="trReceived" runat="server" visible="false" >
                <td align="center">
                <br />
                    <table cellspacing="0" cellpadding="0" border="0" align="center" style="text-align:left" width="95%">
                    <tr class="buttonlabel" align="left">
                    <td>&nbsp;Received RMA</td></tr>
                    </table>
               
                    <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="95%" align="center">
                    <tr>
                    <td>
			        <table  cellSpacing="0" cellPadding="0" width="100%" border="0" align="center">
                    <tr>
                        <td>
                        <asp:Repeater ID="rptReceived" runat="server"  >
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
                                        &nbsp;Name
                                    </td>
                                    <td class="buttongrid" width="10%">
                                        &nbsp;ESN
                                    </td>                                                    
                                    <td class="buttongrid" width="5%">
                                        &nbsp;Quantity
                                    </td>
                                    <td class="buttongrid"    width="7%">
                                        &nbsp;Receive Quantity 
                                    </td>
                                    <td class="buttongrid"  width="17%" >
                                        &nbsp;Tracking#
                                    </td>
                                                    
                                    <td class="buttongrid"  width="7%" >
                                        &nbsp;Status
                                    </td>   
                                    <td class="buttongrid"  width="1%" >
                                        &nbsp;
                                    </td>   
                                    
                                </tr>                            
                            </HeaderTemplate>
                            <ItemTemplate>
                            <tr class="<%# Container.ItemIndex % 2 == 0 ? "alternaterow" : "" %>">                                                
                                <td class="copy10grey">
                                        &nbsp;<%# Container.ItemIndex + 1 %>
                                    <asp:HiddenField ID="hdID" Value='<%# Eval("RMAReceiveDetailGUID") %>' runat="server" />
                                                    
                                </td>     
                                <td class="copy10grey">
                                    &nbsp;<%# Eval("SKU")%></td>
                                <td class="copy10grey">
                                    &nbsp;<%# Eval("ItemName")%></td>
                                <td class="copy10grey">
                                    &nbsp;<%# Eval("ESNReceived") %>
                                                                                       
                                </td>
                                <td class="copy10grey" align="right">
                                    <%# Eval("Quantity")%>&nbsp;
                                                    
                                </td>
                                <td  class="copy10grey" align="right">
                                    <%# Eval("QtyReceived")%>&nbsp;
                                                             
                                </td>
                                <td class="copy10grey">
                                    &nbsp;<%# Eval("ShippingTrackingNumber") %>
                                </td>
                                <td class="copy10grey">
                                    &nbsp;<%# Eval("ReceiveStatus") %>
                                </td>                                                
                                <td class="copy10grey">
                                   <asp:ImageButton ID="imgDelReceive" runat="server"  CommandName="Delete" AlternateText="Delete RMA Receive" ToolTip="Delete RMA Receive" 
                                    ImageUrl="~/images/delete.png" CommandArgument='<%# Eval("RMAReceiveDetailGUID") %>' OnCommand="imgDelReceive_Command" OnClientClick="return confirm('Do you want to delete this RMA Receive?')" />
                        
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
                    </td>

            </tr>
            <tr id="trRMA" runat="server" visible="false" >
                <td align="center">
                    <br />
                    <table bordercolor="#839abf" border="1" width="95%" align="center" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <table border="0" width="100%" class="box" align="center" cellpadding="5" cellspacing="5">
                                <tr>
                                    <td class="copy10grey" align="right" width="12%">
                                        RMA#: 
                                    </td>
                                    <td width="38%">

                                      <asp:Label ID="lblRMA" runat="server" CssClass="copy10grey"></asp:Label>
                                        
                                    </td>
                                   
                                    <td class="copy10grey" align="right" width="20%">
                                       Customer RMA#:
                                    </td>
                                    <td width="30%" class="copy10grey" >
                                        <asp:Label ID="lblCustomerRMA" runat="server" CssClass="copy10grey"></asp:Label>
                                         </td>
                                    </tr>
                                    <tr>
                                    <td class="copy10grey" align="right" width="12%">
                                       RMA Date: 
                                    </td>
                                    <td width="38%">

                                      <asp:Label ID="lblRmaDate" runat="server" CssClass="copy10grey"></asp:Label>
                                        
                                    </td>
                                   
                                    <td class="copy10grey" align="right" width="20%">
                                       RMA Status:
                                    </td>
                                    <td width="30%" class="copy10grey" >
                                        <asp:Label ID="lblRmaStatus" runat="server" CssClass="copy10grey"></asp:Label>
                                         </td>
                                    </tr>
                                <tr>
                                   <td  class="copy10grey" align="right" width="12%">
                                            Tracking: &nbsp;</td>
                                    <td align="left"  width="38%">
                                        <asp:DropDownList ID="ddlRmaTracking" CssClass="copy10grey" runat="server" Width="40%"
                                            AutoPostBack="true" OnSelectedIndexChanged="ddlRmaTracking_SelectedIndexChanged" >
								        </asp:DropDownList>
                                    </td>
                                     <td  class="copy10grey" align="right" width="20%">
                                            <strong>Receive Status:</strong> &nbsp;</td>
                                    <td align="left"  width="30%">
                                        <asp:DropDownList ID="ddlReceiveStatus" CssClass="copy10grey" runat="server" Width="50%"
                                           AutoPostBack="true" OnSelectedIndexChanged="ddlReceiveStatus_SelectedIndexChanged" >
								        </asp:DropDownList>
                                    </td>
                                        
                               </tr>
                            </table>
                        </td>
                    </tr>
                    </table>
                    <br />
                    <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="95%">
                    <tr>
                    <td>
			            <table id="Table2" cellSpacing="5" cellPadding="5" width="100%" border="0">
                        <tr>
                            <td> 
                                <table cellSpacing="5" cellPadding="5" width="100%" border="0">
                                <tr>
                                    <td  class="copy10grey" align="right" width="12%">
                                            <strong>Approved By:</strong> &nbsp;</td>
                                    <td align="left"  width="44%">
                                        <asp:DropDownList ID="ddlUser" CssClass="copy10grey" runat="server" Width="40%">
								        </asp:DropDownList>
                                    </td>
                                     <td  class="copy10grey" align="right" width="12%">
                                            <strong>Received By:</strong> &nbsp;</td>
                                    <td align="left"  width="28%">
                                        <asp:DropDownList ID="ddlReceivedBy" CssClass="copy10grey" runat="server" Width="50%">
								        </asp:DropDownList>
                                    </td>
                                   
<%--                                    <td  class="copy10grey" align="right"  width="10%">
                                    </td>--%>
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
                                                    <td class="buttongrid" width="3%">
                                                        <asp:CheckBox ID="allchk"  runat="server" Text=" S.No."   />
                                                    </td>
                                                    <td class="buttongrid" width="15%">
                                                        &nbsp;SKU
                                                    </td>
                                                    <td class="buttongrid" width="15%">
                                                        &nbsp;Name
                                                    </td>
                                                    <td class="buttongrid" width="10%">
                                                        &nbsp;ESN
                                                    </td>                                                    
                                                    <td class="buttongrid"  width="5%">
                                                        &nbsp;Quantity
                                                    </td>
                                                    <td class="buttongrid"  width="7%">
                                                        &nbsp;Receive Quantity 
                                                    </td>
                                                    <td class="buttongrid"  width="17%" >
                                                        &nbsp;Tracking#
                                                    </td>
                                                    
                                                    <td class="buttongrid"  width="7%" >
                                                        &nbsp;Status
                                                    </td>
                                                    
                                                </tr>                            
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                            <tr class="<%# Container.ItemIndex % 2 == 0 ? "alternaterow" : "" %>">                                                
                                                <td class="copy10grey">
                                                    <asp:CheckBox ID="chkItem" Checked='<%# Convert.ToInt32(Eval("RMAReceiveDetailGUID"))==0 ? false: true %>' Enabled='<%# Convert.ToInt32(Eval("RMAReceiveDetailGUID")) > 0 && Convert.ToInt32(Eval("QtyReceived")) > 0 ? false : true %>' runat="server" />
                                                    <%--Checked='<%# Convert.ToInt32(Eval("QtyReceived"))==0 ? false: true %>'--%>
                                                        &nbsp;<%# Container.ItemIndex + 1 %><asp:HiddenField ID="hdReceiveDetGUID" Value='<%# Eval("RMAReceiveDetailGUID") %>' runat="server" />
                                                    <asp:HiddenField ID="hdRmaDelGUID" Value='<%# Eval("RMADetGUID") %>' runat="server" />
                                                </td>     
                                                <td class="copy10grey">
                                                 &nbsp;<%# Eval("SKU")%></td>
                                                <td class="copy10grey">
                                                 &nbsp;<%# Eval("ItemName")%></td>
                                                <td class="copy10grey">
                                                 &nbsp;
                                                    <asp:Label ID="lblESN" Text='<%# Eval("ESNReceived") %>' CssClass="copy10grey" runat="server" />
                                                                                       
                                                </td>
                                                <td class="copy10grey" align="right">
                                                    <%# Eval("Quantity")%>&nbsp;&nbsp;&nbsp;
                                                    
                                                </td>
                                                <td >
                                                    <asp:TextBox ID="txtQtyReceived" onchange="return ValidateQty(this);"
                                                        Enabled='<%# Convert.ToInt32(Eval("RMAReceiveDetailGUID")) > 0 ? false : true %>' 
                                                        onkeypress="return isNumberKey(event);" MaxLength="4" CssClass="copy10grey" Width="70%" 
                                                        runat="server" Text='<%# Convert.ToInt32(Eval("RMAReceiveDetailGUID")) == 0 ? Eval("Quantity") : Eval("QtyReceived") %>'></asp:TextBox>
                                                    <asp:HiddenField ID="hdQty" Value='<%# Eval("Quantity")%>' runat="server" />
                                                             
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlTracking"  runat="server" Width="100%" CssClass ="copy10grey"> 
                                                    </asp:DropDownList>
                                                    <asp:HiddenField id="hdnTracking" runat="server" Value='<%# Eval("ShippingTrackingNumber") %>'/>
                                                             
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlStatus"  runat="server" class="copy10grey" Width="95%" >                                                        
                                                    </asp:DropDownList>
                                                    <asp:HiddenField id="hdnStatusID" runat="server" Value='<%# Eval("ReceiveStatusID") %>'/>
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
                    <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="95%">
                    <tr>
                    <td>
			            <table id="Table2" cellSpacing="5" cellPadding="5" width="100%" border="0">
                        <tr>
                            <td>
                                <table cellSpacing="5" cellPadding="5" width="100%" border="0">
                                <tr>
                                    <td  class="copy10grey" align="right" width="12%">
                                            Comment: &nbsp;</td>
                                    <td align="left"  width="88%">
                                        <asp:TextBox ID="txtComment" CssClass="copy10grey" Height="70px" TextMode="MultiLine" runat="server" Width="90%">
								        </asp:TextBox>
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
                         <tr><td><hr /></td></tr>
                    <tr >
                    <td align="center">&nbsp;
                       <asp:Button ID="btnSubmit"  CssClass="button" runat="server" Text="Submit"  
                        OnClientClick="return IsValidate(1);" OnClick="btnSubmit_Click" />&nbsp;       
                       <asp:Button ID="btnCancel" CssClass="button" runat="server" Text="Close" 
                        CausesValidation="false" OnClick="btnCancel_Click" /> 
                                        

                    </td></tr>
                    </table>
                
                </td>
            </tr>
        </table>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnSearch" /> 
                
            </Triggers>
            </asp:UpdatePanel>
        

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
