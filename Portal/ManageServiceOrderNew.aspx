<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageServiceOrderNew.aspx.cs" Inherits="avii.ManageServiceOrderNew" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>
<%@ Register TagPrefix="menu" TagName="Menu" Src="~/Controls/Header.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     
    <title>Manage Service Order</title>
    <script src="https://code.jquery.com/jquery-1.11.0.min.js"></script>

	<%--<link rel="stylesheet"  type="text/css" href="../fullfillment/calendar/dhtmlgoodies_calendar.css" media="screen" />
	<script type="text/javascript" src="../fullfillment/calendar/dhtmlgoodies_calendar.js"></script>  --%>	
<!-- fix for 1.1em default font size in Jquery UI -->
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

        function SelectAll(id) {
            // alert(document.getElementById(id).checked);
            var check = document.getElementById(id).checked;
            $(':checkbox').prop('checked', check);
        }
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

    <script type="text/javascript">
        $(document).ready(function () {
            var increementIndex = <%=IncreementIndex%>;

             //alert('1')
            $('input:text:first').focus();
             //alert('2')
            $('input:text').bind('keypress', function (e) {
               // alert('3')
                if (e.keyCode == 13) {
                   // alert('4')

                      e.preventDefault();
                    var nextIndex = $('input:text').index(this) + increementIndex;
                    var maxIndex = $('input:text').length;
                  //  alert(maxIndex);
                    if (nextIndex < maxIndex) {
                      //  alert('5')
                        $('input:text:eq(' + nextIndex + ')').focus();
                        //e.preventDefault();
                    }

                }

            });
        });
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
           
            <asp:Panel  ID="Panel1" runat="server"  DefaultButton="btnSearch" >
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
                     <asp:Button ID="btnSearch"  runat="server" Text=" Search " CssClass="button" OnClick="btnSearch_Click" />       
        
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
                <tr valign="top">
                <td class="copy10grey" align="right" width="15%">
                    <strong>Select:</strong>
                </td>
                <td width="35%">
                <asp:RadioButton ID="rdScan" Text=" Scanning" Checked="true"  CssClass="copy10grey" GroupName="SO" runat="server" OnCheckedChanged="rdScan_CheckedChanged"  AutoPostBack="true"/>&nbsp;&nbsp;
                    <asp:RadioButton ID="rdUpload" Text=" Upload" CssClass="copy10grey" GroupName="SO" runat="server" OnCheckedChanged="rdUpload_CheckedChanged" AutoPostBack="true" /> 
                    
                </td>
                <td  width="1%">
                    &nbsp;
                </td>
                <td class="copy10grey" align="right" width="15%">
                    
                </td>
                <td width="35%">
                   
                                              
        
                </td>   
                
                    
                </tr>
                <tr id="trHr" runat="server">
                <td colspan="5">
                <hr />
                </td>
                </tr>
                <tr id="trScan" runat="server" visible="false">
                <td  align="center"  colspan="5">
                    <asp:Button ID="btnAdd" runat="server"  CssClass="button" Text="Generate ESN" OnClick="btnGenerateEsn_Click"></asp:Button>
                    
                    <asp:Button ID="btValidate" Visible="false" runat="server" Text="Validate" CssClass="button" OnClick="btnValidate_Click" />
                  
        </td>

                </tr>
            
        </table>
            
   
     </td>
     </tr>
     </table>      
         </asp:Panel>
         <br />

     <table bordercolor="#839abf" border="1" width="100%" align="center" cellpadding="0" cellspacing="0" id="trUpload" runat="server" visible="false">
      <tr>
        <td align="center">
    
                                <table width="65%" cellpadding="5" cellspacing="5">
                        <tr>
                            <td class="copy10grey" align="right" width="40%">
                
                                    Upload ESN file: &nbsp;</td>
                                <td align="left" width="60%">
                                    <asp:FileUpload ID="flnUpload" runat="server" CssClass="txfield1" Width="50%" />

                                </td>
                                
                                
                                
                        </tr>
                        <tr>
                            <td class="copy10grey" align="right" width="40%">
                                    File format sample: &nbsp;
                                          </td>
                                <td class="copy10grey" align="left" width="60%">
                                   <b>SKU,ESN</b>,ICCID <asp:LinkButton ID="lnkButton" runat="server"   Text="Download file format" OnClick="btnCSV_Click"></asp:LinkButton>
                    
                                </td>
                           
                        </tr>
                        <tr>
                            <td class="copy10grey" align="right" width="40%">
                                     &nbsp;
                                         </td>
                                <td class="copy10grey" align="left" width="60%">

                                    
                                    <asp:Button ID="btnUploadValidate"  runat="server" Text="Validate Uploaded File" CssClass="button" OnClick="btnUploadValidate_Click" />       
       
                                    
                            </td>
                        </tr>
                    </table>

        </td>
      </tr>
    </table>
             
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

                                &nbsp;<asp:Button ID="btnPrint"  Visible="false" OnClientClick="return EmptyLabel();"  CssClass="button" runat="server" OnClick="btnPrint_Click" Text="Generate Label"  />

                                    <asp:Button ID="btnDownload" Visible="false" runat="server" Text=" Download " CssClass="button" OnClick="btnDownload_Click" 
                    CausesValidation="false" />  


                               <%-- &nbsp;<a id="lnk_Print"  href="#" style="height:30px !important; line-height:40px !important; width:150px" class="button" Visible="false" target="_blank" runat="server"><span style="height:30px !important; line-height:40px !important; width:150px" class="button"> Print </span></a>--%>

                                    </td>
                            </tr>
                            <%--<tr><td colspan="2">
                            <hr style="width:100%" />
                            
                            </td></tr>                            
                        --%>
                        <tr>
                          <td>
                              <asp:Label ID="lblCount" CssClass="copy10grey" runat="server" ></asp:Label>
                            </td>
                            <td align="right">
                                <asp:Button ID="btnValidate" runat="server" Text="Validate" CssClass="button"  OnClick="btnValidate_Click" />
                    
                            </td>

                        </tr>
                        <tr>
                            <td colspan="2">
    
                        <asp:GridView ID="gvSOEsn"   AutoGenerateColumns="false"  
                        Width="100%" ShowHeader="true"  ShowFooter="false" runat="server" GridLines="Both" 
                        PageSize="50" AllowPaging="false" AllowSorting="false"  OnRowDataBound="gvSOEsn_RowDataBound"
                        >
                        <RowStyle BackColor="Gainsboro" />
                        <AlternatingRowStyle BackColor="white" />
                        <HeaderStyle  CssClass="buttongrid" ForeColor="white"/>
                          <PagerStyle ForeColor="#636363" CssClass="copy10grey" HorizontalAlign="Left" />
                          <Columns>
                              <asp:TemplateField HeaderText="S.No." ItemStyle-CssClass="copy10grey"  ItemStyle-Width="1%" HeaderStyle-CssClass="buttongrid">
                                <ItemTemplate>

                                        <%# Container.DataItemIndex + 1%>
                                        <asp:HiddenField ID="hdRowNumber" Value='<%# Eval("RowNumber")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField> 
                              <asp:TemplateField HeaderText="SKU" ItemStyle-CssClass="copy10grey"  ItemStyle-Width="19%">
                                <ItemTemplate>

                                      <%# Eval("SKU")%>
                  
                                </ItemTemplate>
                            </asp:TemplateField> 


                                <asp:TemplateField HeaderText="ESN#" SortExpression="ESN"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="30%">
                                    <ItemTemplate>
                                        <asp:Label ID="hdnSKUId" Visible="false" Text='<%# Eval("ItemcompanyGUID")%>' runat="server" />
                                        <asp:Label ID="hdUPC" Visible="false" Text='<%# Eval("UPC")%>' runat="server" />
                                        
                                        <asp:Label ID="hdSKU" Visible="false" Text='<%# Eval("SKU")%>' runat="server" />
                                        <table cellpadding="3" cellspacing="3" width="100%" >
                                        <tr>
                                        <td width="70%">
                                             <asp:TextBox ID="txtESN" CssClass="copy10grey" Width="100%" onkeypress="return isNumberKey(event);"  runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ESN")%>'></asp:TextBox>
                                            
                                        </td>
                                            <td>
                                                <span class="errormessage">
                                                   <%# Eval("ValidationMsg") %>
                                                </span>
                                            </td>
                                        </tr>
                                        </table>                                               
                                       
                                    </ItemTemplate>
                                </asp:TemplateField> 
                              <asp:TemplateField HeaderText="Mapped SKU" ItemStyle-CssClass="copy10grey"  ItemStyle-Width="19%">
                                <ItemTemplate>

                                      <%# Eval("MappedSKU")%>
                  
                                    </ItemTemplate>
                                </asp:TemplateField> 

                              <asp:TemplateField HeaderText="ICCID" SortExpression="ICCID"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="30%">
                                    <ItemTemplate>
                                        <asp:Label ID="hdnMappedItemCompanyGUID" Visible="false" Text='<%# Eval("MappedItemCompanyGUID")%>' runat="server" />
                                        <%--<asp:Label ID="hdUPC" Visible="false" Text='<%# Eval("UPC")%>' runat="server" />
                                        --%>
                                        <asp:Label ID="lblICCID" Visible="false" Text='<%# Eval("ICCID")%>' runat="server" />
                                        <table cellpadding="3" cellspacing="3" width="100%" >
                                        <tr>
                                        <td width="70%">
                                            
                                             <asp:TextBox ID="txtICCID" Enabled='<%# Convert.ToInt32(Eval("MappedItemCompanyGUID")) > 0 ? true : false %>' CssClass="copy10grey" Width="100%" onkeypress="return isNumberKey(event);"  runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ICCID")%>'></asp:TextBox>
                                            
                                        </td>
                                            <td>
                                                <span class="errormessage">
                                                   <%# Eval("ICCIDValidationMsg") %>
                                                </span>
                                            </td>
                                        </tr>
                                        </table>                                              
                                       
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                              <asp:TemplateField HeaderText="KitID" ItemStyle-CssClass="copy10grey"  ItemStyle-Width="2%">
                                <ItemTemplate>
                                        
                                      <%# Eval("KitID")%>
                  
                                </ItemTemplate>
                            </asp:TemplateField> 


<%--                                <asp:TemplateField HeaderText="ICCID" SortExpression="ICCID"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="24%">
                                    <ItemTemplate>
                                        <%#Eval("ICCID")%>
                                        <asp:Label ID="txtICCID" Visible="false" runat="server" Text='<%#Eval("ICCID")%>'></asp:Label>
                                        </ItemTemplate>
                                </asp:TemplateField>  --%>
                                <asp:TemplateField HeaderText="Print" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="copy10grey" ItemStyle-Width="1%">
                                    <HeaderTemplate>
                                       &nbsp;Print <br />
                                       &nbsp;&nbsp; <asp:CheckBox  runat="server" ID="chkAll" />
                                        
                                        <%--<input type="checkbox" id="ckAll" onclick="SelectAll('ckAll')" <%# Convert.ToBoolean(Eval("IsPrint")) == true ? "checked":"" %> name="Print"   value="Print" title="Print" >Print
                                        --%><%--Print--%>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                                
                                        <asp:CheckBox runat="server"  ID="chkPrint" />
                                    </ItemTemplate>
                                </asp:TemplateField> 

                                
                            </Columns>
                        </asp:GridView>
                        
                            </td>
                        </tr>
                        
                        </table>
                        
                </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnUploadValidate" /> 
            <asp:PostBackTrigger ControlID="btnDownload" />
            <asp:PostBackTrigger ControlID="rdUpload" />
            <asp:PostBackTrigger ControlID="rdScan" />
            <asp:PostBackTrigger ControlID="ddlKitted" />
            <asp:PostBackTrigger ControlID="btnCancel" /> 
            <asp:PostBackTrigger ControlID="btnPrint"   /> 
            <asp:PostBackTrigger ControlID="lnkButton"   /> 
            
            <asp:PostBackTrigger ControlID="btnSubmit"/> 
            <asp:AsyncPostBackTrigger ControlID="btValidate" EventName="Click"/> 
            <asp:AsyncPostBackTrigger ControlID="btnValidate" EventName="Click"/> 
            <asp:AsyncPostBackTrigger ControlID="btnAdd" EventName="Click"/> 
            
        </Triggers>
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
