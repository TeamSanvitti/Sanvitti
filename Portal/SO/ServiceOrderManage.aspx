<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ServiceOrderManage.aspx.cs" Inherits="avii.SO.ServiceOrderManage" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>
<%@ Register TagPrefix="menu" TagName="Menu" Src="~/Controls/Header.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 <title>Manage Service Order</title>
    <script src="https://code.jquery.com/jquery-1.11.0.min.js"></script>
    <style type="text/css">
		.ui-widget{font-size:11px !important;}
		.ui-state-error-text{margin-left: 10px}
		.ui-state-error-text{margin-left: 10px}
	</style>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#txtCustOrderNo').keypress(function (e) {
                var regex = new RegExp("^[a-zA-Z0-9]+$");
                var str = String.fromCharCode(!e.charCode ? e.which : e.charCode);
                if (regex.test(str)) {
                    return true;
                }
                e.preventDefault();
                return false;
            });


        });

        //function SelectAll(id) {
        //    // alert(document.getElementById(id).checked);
        //    var check = document.getElementById(id).checked;
        //    $(':checkbox').prop('checked', check);
        //}
        function EmptyLabel() {
            document.getElementById('lblMsg').innerText = '';
            return true;
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
        function ValidateQuantity(obj) {
            if (obj.value > 500) {
                alert('Quantity can not be greater than 500');
                obj.value = 500;
                return false;
            }
            return true;
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
     <table cellspacing="0" cellpadding="0" border="0" align="center" width="95%">
        
        <tr valign="top">
           
            <td>
                <table align="center" style="text-align:left" width="100%">
                <tr class="buttonlabel" align="left">
                <td>&nbsp;Service Order</td></tr>
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
            <asp:Panel  ID="Panel1" runat="server"  >
        <table width="100%" border="0" class="box" align="center" cellpadding="2" cellspacing="2">
         
            <tr valign="top">
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
                    Customer Order#:
                </td>
                <td width="35%">
                                       
                  <asp:TextBox ID="txtCustOrderNo" runat="server"   CssClass="copy10grey" MaxLength="20"  Width="80%"></asp:TextBox>
                </td>   
                <td>
                     
                </td>
                
                    
                </tr>
         </table>
        </asp:Panel>
            
        </td>
     </tr>
     </table> 
         <br />
         <asp:Panel  ID="pnlSearch" runat="server" Visible="false"  >
     <table bordercolor="#839abf" border="1" width="100%" align="center" cellpadding="0" cellspacing="0">
      <tr>
        <td>
         
     <table width="100%" border="0" class="box" align="center" cellpadding="2" cellspacing="2">
         <tr style="height:1px">
         <td style="height:1px"></td>
         </tr>
     
           
                <tr valign="top">
                <td class="copy10grey" align="right" width="15%">
                    <strong>Service Order#:</strong>
                </td>
                <td width="35%">
                    <asp:TextBox ID="txtSONumber" runat="server"  onkeypress="return isNumberKey(event);" CssClass="copy10grey" MaxLength="20"  Width="80%"></asp:TextBox>
                </td>
                <td  width="1%">
                    &nbsp;
                </td>
                <td class="copy10grey" align="right" width="15%">
                    <strong>Order Date:</strong>
                </td>
                <td width="35%">
                    <asp:TextBox ID="txtOrderDate" runat="server" onfocus="set_focus1();"  CssClass="copy10grey" MaxLength="12"  Width="80%"></asp:TextBox>                                                    
        
                </td>   
                
                    
                </tr>
                <tr valign="top">
                <td class="copy10grey" align="right" width="15%">
                    <strong>Kitted SKU#:</strong>
                </td>
                <td width="35%">
                <asp:DropDownList ID="ddlKitted" CssClass="copy10grey" runat="server" Width="80%"
                                    OnSelectedIndexChanged="ddlKitted_SelectedIndexChanged"  
                                    AutoPostBack="true">
									</asp:DropDownList>
                
                </td>
                <td  width="1%">
                    &nbsp;
                </td>
                <td class="copy10grey" align="right" width="15%">
                    <strong>Number of kits:</strong>
                </td>
                <td width="35%">
                   
                    <asp:TextBox ID="txtOrderQty" runat="server"  onkeypress="return isNumberKey(event);" onchange="return ValidateQuantity(this);" CssClass="copy10grey" MaxLength="4"  Width="80%"></asp:TextBox>
                                              
        
                </td>   
                
                    
                </tr>
                <%--<tr><td>&nbsp;</td></tr>--%>
                <tr valign="top">
                    <td colspan="5" >
                        
                        <%--<table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="80%" align="center" >
                           <tr bordercolor="#839abf">
                                <td>--%>

                                <asp:Repeater ID="rptESN" runat="server">
                                <HeaderTemplate>                                    
                                   <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="100%" align="center" >
                                   <tr bordercolor="#839abf">
                                        <td>
                                   <table width="100%" border="0" cellpadding="2" cellspacing="2">                                  
                                
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr valign="top" class="<%# Container.ItemIndex % 2 == 0 ? "alternaterow" : "" %>">
                                    <td class="copy10grey" align="right" width="15%">
                                        RAW SKU#:
                                    </td>
                                    <td width="35%" class="copy10grey">
                                        <asp:HiddenField ID="hdSKUId" Value='<%# Eval("ItemcompanyGUID")%>' runat="server" />
                                        <asp:TextBox ID="txtsku" Enabled="false" runat="server" Text='<%# Eval("SKU")%>'   CssClass="copy10grey" MaxLength="30"  Width="80%"></asp:TextBox>
                                        
                                    </td>
                                    <td  width="1%">
                                        &nbsp;
                                    </td>
                                    <td class="copy10grey" align="right" width="15%">
                                        <%--ESN Starts#:--%>
                                    </td>
                                    <td width="20%">
                                        <asp:HiddenField ID="hdMappedItemCompanyGUID" Value='<%# Eval("MappedItemCompanyGUID")%>' runat="server" />
                                        
                                        <asp:TextBox ID="txtICCID" Visible="false" runat="server"  onkeypress="return isNumberKey(event);"  CssClass="copy10grey" MaxLength="18"  Width="80%"></asp:TextBox>
                                        <asp:HiddenField ID="hdIsESNRequired" Value='<%# Eval("IsESNRequired")%>' runat="server" />
                                        
                                    </td>   
                                    <td width="15%"  class="copy10grey">
                                        <asp:HiddenField ID="hdnQty" Value='<%# Eval("Quantity")%>' runat="server" />
                                        
                                        Required Qty: <%# Eval("Quantity")%>    
                                        <asp:Label ID="lblStockmsg" Value='<%# Eval("Stockmsg")%>' runat="server" />                                            
        
                                    </td>  
                    
                                    </tr>
                                <%--<tr class="<%# Container.ItemIndex % 2 == 0 ? "alternaterow" : "" %>">
                                                
                                    <td class="copy10grey">
                                            &nbsp;<%# Eval("SKU")%>
                                       
                                    </td>
                                
                                    <td class="copy10grey">
                                            &nbsp;<asp:TextBox ID="textICCID" runat="server"  CssClass="copy10grey" MaxLength="30"  Width="80%"></asp:TextBox>
                                    </td>
                                    <td width="35%">
                                    
                                            &nbsp;</td>
                                    <td class="copy10grey">
                                    
                                    </td>
                                
                                </tr>--%>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </table> 
                                    </td>
                                    </tr>
                                </table>
                                </FooterTemplate>
                                </asp:Repeater>
                               <%--</td>
                            </tr>
                        </table>--%>
                    </td>

                </tr>
                
                
                
        </table>
            
   
     </td>
     </tr>
     </table>      
         </asp:Panel>
         <br />

             
<%--     <br />--%>
      <table align="center" style="text-align:left" width="80%">
      <tr>
     <tr>
                <td  align="center"  colspan="5">
                    <%--<asp:Panel ID="pnlPO" runat="server">--%>
                        <%--<PO:Status ID="pos1" runat="server" />--%>
                        <table width="100%" cellpadding="0" cellspacing="0">
                             <tr>
                                   <td align="left" width="50%" >
                                <asp:Label ID="lblCounts" CssClass="copy10grey" runat="server" ></asp:Label>
                                       </td>
                                <td  align="right">                                
                                
                                <asp:Button ID="btnSubmit" Visible="false" Width="190px"  CssClass="button" runat="server" OnClick="btnSubmit_Click" Text="Submit" />
                               
                                &nbsp;<asp:Button ID="btnCancel" Visible="false" runat="server"  CssClass="button" Text="Cancel" OnClick="btnCancel_Click" />
                                                              

                               <%-- &nbsp;<a id="lnk_Print"  href="#" style="height:30px !important; line-height:40px !important; width:150px" class="button" Visible="false" target="_blank" runat="server"><span style="height:30px !important; line-height:40px !important; width:150px" class="button"> Print </span></a>--%>

                                    </td>
                            </tr>
                            
                        
                        </table>
                        
                </td>
                </tr>
            </table>
        </ContentTemplate>
        <%--<Triggers>
            <asp:PostBackTrigger ControlID="btnCancel" />             
            <asp:PostBackTrigger ControlID="btnSubmit"/> 
        </Triggers>--%>
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
