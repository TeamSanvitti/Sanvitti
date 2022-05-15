<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ServiceOrderRequest.aspx.cs" Inherits="avii.ServiceOrder_Request" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>
<%@ Register TagPrefix="menu" TagName="Menu" Src="~/Controls/Header.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Manage Service Order Request</title>
    
    
        <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.4.0/jquery.min.js"></script>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/jquery-ui.min.js"></script>
        <script type="text/javascript" src="../JQuery/jquery.blockUI.js"></script>
        <script type="text/javascript" src="/JSLibrary/jquery.blockUI.js"></script>
	<%--<link rel="stylesheet"  type="text/css" href="../fullfillment/calendar/dhtmlgoodies_calendar.css" media="screen" />
	<script type="text/javascript" src="../fullfillment/calendar/dhtmlgoodies_calendar.js"></script>  --%>
	
<!-- fix for 1.1em default font size in Jquery UI -->
	<style type="text/css">
		.ui-widget{font-size:11px !important;}
		.ui-state-error-text{margin-left: 10px}
	</style>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#txtSORNumber').keypress(function (e) {
                var regex = new RegExp("^[a-zA-Z0-9]+$");
                var str = String.fromCharCode(!e.charCode ? e.which : e.charCode);
                if (regex.test(str)) {
                    return true;
                }
                e.preventDefault();
                return false;
            });


        });
        
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
        function ValidateQuantity(obj) {
            if (obj.value > 6000) {
                alert('Quantity can not be greater than 6000');
                obj.value = 6000;
                return false;
            }
            return true;
        }
        function Validate()
        {
            qty = document.getElementById("<%=txtOrderQty.ClientID%>").value;
            customer = document.getElementById("<%=hdnCustomer.ClientID%>").value;


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

    <table cellspacing="0" cellpadding="0" border="0" align="center" width="95%">
        <tr valign="top">
            <td   >
                <table align="center" style="text-align:left" width="100%">
                <tr class="buttonlabel" align="left">
                <td>&nbsp;Service Order Request</td></tr>
             </table>
         <asp:UpdatePanel ID="UpdatePanel1"  UpdateMode="Conditional" ChildrenAsTriggers="true" runat="server"   >
         <ContentTemplate>
         <table  align="center" style="text-align:left" width="100%">
         <tr>
            <td>
               <asp:Label ID="lblMsg" runat="server" CssClass="errormessage"></asp:Label>
            
            </td>
         </tr>
         </table>
         <table bordercolor="#839abf" border="1" width="100%" align="center" cellpadding="0" cellspacing="0">
          <tr>
            <td>
             <asp:HiddenField ID="hdnCustomer" runat="server" />
         <table width="100%" border="0" class="box" align="center" cellpadding="5" cellspacing="5">
             <tr style="height:1px">
             <td style="height:1px"></td>
             </tr>
                <tr valign="top" id="trCustomer" runat="server">
                <td class="copy10grey" align="right" width="15%">
                    <strong>Customer Name:</strong>
                </td>
                <td width="35%">
                    <asp:DropDownList ID="dpCompany" CssClass="copy10grey" runat="server" Width="80%"
                                    OnSelectedIndexChanged="dpCompany_SelectedIndexChanged"  
                                    AutoPostBack="true">
									</asp:DropDownList>
                
                </td>
                <td  width="1%">
                    &nbsp;
                </td>
                <td class="copy10grey" align="right" width="15%">
                   <asp:Label ID="lblStatus" runat="server" CssClass="copy10grey"></asp:Label>
                </td>
                <td width="35%">
                   
                          
                    <asp:DropDownList ID="ddlStatus" CssClass="copy10grey" runat="server" Width="80%">
	                </asp:DropDownList>              
        
                </td>   
                
                    
                </tr>
            
                    <tr valign="top">
                    <td class="copy10grey" align="right" width="15%">
                        <strong>Service Order Request#:</strong>
                    </td>
                    <td width="35%">
                    <asp:TextBox ID="txtSORNumber" runat="server"   CssClass="copy10grey" MaxLength="20"  Width="80%"></asp:TextBox>
                               
                
                    </td>
                    <td  width="1%">
                        &nbsp;
                    </td>
                    <td class="copy10grey" align="right" width="15%">
                   <strong>Requested By:</strong>
                    </td>
                    <td width="35%">                                                    
                        
                    <asp:DropDownList ID="ddlUser" CssClass="copy10grey" runat="server" Width="80%">
	                </asp:DropDownList>
                    </td>   
                
                    
                    </tr>
                    <tr valign="top">
                    <td class="copy10grey" align="right" width="15%">
                        <strong>Kitted SKU#:</strong>
                    </td>
                    <td width="35%">
                    <asp:DropDownList ID="ddlKitted" CssClass="copy10grey" runat="server" Width="80%">
	                </asp:DropDownList>
                
                    </td>
                    <td  width="1%">
                        &nbsp;
                    </td>
                    <td class="copy10grey" align="right" width="15%">
                        <strong>Kitted Quantity:</strong>
                    </td>
                    <td width="35%">
                   
                        <asp:TextBox ID="txtOrderQty" runat="server"  onkeypress="return isNumberKey(event);" onchange="return ValidateQuantity(this);" CssClass="copy10grey" MaxLength="4"  Width="80%"></asp:TextBox>
                                              
        
                    </td>   
                
                    
                    </tr>
                    <tr valign="top">
                        <td class="copy10grey" align="right" width="15%">
                            Comments:
                        </td>
                        <td colspan="4">
                             <asp:TextBox ID="txtComments" runat="server" TextMode="MultiLine"  Height="70px" CssClass="copy10grey"   Width="92%"></asp:TextBox>
                       
                        </td>
                    </tr>
               
            </table>
              
   
         </td>
         </tr>
         </table>       
         <br />
             
          <table align="center" style="text-align:left" width="80%">
          <tr>
         <tr>
                    <td  align="center"  colspan="5">
                    
                            <table width="100%" cellpadding="0" cellspacing="0">
                                 <tr>
                                   
                                    <td  align="center">
                                
                                        <asp:Button ID="btnSubmit" CssClass="button" runat="server" OnClick="btnSubmit_Click" Text="Submit" />
                               
                                        &nbsp;<asp:Button ID="btnCancel" Visible="true" runat="server"  CssClass="button" Text="Cancel" OnClick="btnCancel_Click" />

                                        </td>
                                </tr>
                        
                            </table>
                        
                    </td>
                    </tr>
                </table>
            </ContentTemplate>
        
           </asp:UpdatePanel>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server"
            DynamicLayout="false">
            <ProgressTemplate>
                <img src="/Images/ajax-loaders.gif" /> Loading ...
            </ProgressTemplate>
        </asp:UpdateProgress>
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
