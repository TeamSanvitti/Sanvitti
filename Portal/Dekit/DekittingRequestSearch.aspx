<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DekittingRequestSearch.aspx.cs" Inherits="avii.Dekit.DekittingRequestSearch" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>
<%@ Register TagPrefix="menu" TagName="Menu" Src="~/Controls/Header.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Dekitting Request Search</title>
    <script type="text/javascript" src="../JQuery/jquery.blockUI.js"></script>
	
    <link rel="stylesheet"  type="text/css" href="../fullfillment/calendar/dhtmlgoodies_calendar.css" media="screen" />
	<script type="text/javascript" src="../fullfillment/calendar/dhtmlgoodies_calendar.js"></script>  
    <script type="text/javascript">
        function OpenNewPage(url) {
                window.open(url);
        }
    
    
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

            $('#txtKittedSKU').keypress(function (e) {
                var regex = new RegExp("^[a-zA-Z0-9-]+$");
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

        function set_focus1() {
		        var img = document.getElementById("imgDateFrom");
		        var st = document.getElementById("txtKittedSKU");
		        st.focus();
		        img.click();
		    }
		    function set_focus2() {
		        var img = document.getElementById("imgDateTo");
		        var st = document.getElementById("txtKittedSKU");
		        st.focus();
		        img.click();
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
                <td>&nbsp;Dekitting Request</td></tr>
             </table>
        
    <asp:UpdatePanel ID="UpdatePanel1"  runat="server"   >
    <ContentTemplate>
    <table  align="center" style="text-align:left" width="100%" cellSpacing="0" cellPadding="0">
     <tr>
        <td align="left">
           <asp:Label ID="lblMsg" runat="server" CssClass="errormessage"></asp:Label>
            
        </td>
     </tr>
     </table>
        
     <table bordercolor="#839abf" border="1" width="100%" align="center" cellpadding="0" cellspacing="0">
      <tr>
        <td>
         <asp:Panel  ID="pnlSearch" runat="server"  DefaultButton="btnSearch" >
     <table width="100%" border="0" class="box" align="center" cellpadding="2" cellspacing="2">
         <tr style="height:1px">
         <td style="height:1px"></td>
         </tr>
     
           
            <tr valign="top" id="trCustomer" runat="server">
                <td class="copy10grey" align="right" width="15%">
                    Customer Name:
                </td>
                <td width="35%">
                    <asp:DropDownList ID="dpCompany" CssClass="copy10grey" runat="server" Width="80%">
				    </asp:DropDownList>
                
                </td>
                <td  width="1%">
                    &nbsp;
                </td>
                <td class="copy10grey" align="right" width="15%">
                    
                </td>
                <td width="35%">
                   
                   
                </td>
                </tr>
                <tr valign="top">
                <td class="copy10grey" align="right" width="15%">
                    Dekit Request#:
                </td>
                <td width="35%">
                    <asp:TextBox ID="txtDekitRequestNo" runat="server"  onkeypress="return isNumberKey(event);" CssClass="copy10grey" MaxLength="20"  Width="80%"></asp:TextBox>
                </td>
                <td  width="1%">
                    &nbsp;
                </td>
                <td class="copy10grey" align="right" width="15%">
                    Customer Request#:
                </td>
                <td width="35%">                   
                  <asp:TextBox ID="txtCustomerRequestNo" runat="server"   CssClass="copy10grey" MaxLength="20"  Width="80%"></asp:TextBox>
                </td>
                </tr>
          <tr valign="top">
                <td class="copy10grey" align="right" width="15%">
                    Request Date From:
                </td>
                <td width="35%">
                    <asp:TextBox ID="txtDateFrom" runat="server" onfocus="set_focus1();"  CssClass="copy10grey" MaxLength="12"  Width="80%"></asp:TextBox>
                    <img id="imgDateFrom" alt="" onclick="document.getElementById('<%=txtDateFrom.ClientID%>').value = ''; displayCalendar(document.getElementById('<%=txtDateFrom.ClientID%>'),'mm/dd/yyyy',this,true);" src="../fullfillment/calendar/sscalendar.jpg" />
                                        
         
                </td>
                <td  width="1%">
                    &nbsp;
                </td>
                <td class="copy10grey" align="right" width="15%">
                    Date To:
                </td>
                <td width="35%">
                   
                  <asp:TextBox ID="txtDateTo" runat="server" onfocus="set_focus2();"  CssClass="copy10grey" MaxLength="12"  Width="80%"></asp:TextBox>
                    <img id="imgDateTo" alt="" onclick="document.getElementById('<%=txtDateTo.ClientID%>').value = ''; displayCalendar(document.getElementById('<%=txtDateTo.ClientID%>'),'mm/dd/yyyy',this,true);" src="../fullfillment/calendar/sscalendar.jpg" />
                                                
        
                </td>   
                
                    
                </tr>
                <tr valign="top">
                <td class="copy10grey" align="right" width="15%">
                    Kitted SKU#:
                </td>
                <td width="35%">
                <asp:TextBox ID="txtKittedSKU" runat="server"   CssClass="copy10grey" MaxLength="20"  Width="80%"></asp:TextBox>
                
                </td>
                <td  width="1%">
                    &nbsp;
                </td>
                <td class="copy10grey" align="right" width="15%">
                    ESN/ICCID:
                </td>
                <td width="35%">
                   
                    <asp:TextBox ID="txtESN" runat="server" onkeypress="return isNumberKey(event);"   CssClass="copy10grey" MaxLength="20"  Width="80%"></asp:TextBox>                                              
        
                </td>   
                
                    
                </tr>
               
                <tr valign="top">
                <td class="copy10grey" align="right" width="15%">
                    Dekitting Status:
                </td>
                <td width="35%">
                     <asp:DropDownList ID="ddlStatus" CssClass="copy10grey" runat="server" Width="80%">
				    </asp:DropDownList>
                
                </td>
                <td  width="1%">
                    &nbsp;
                </td>
                <td class="copy10grey" align="right" width="15%">
                  
                </td>
                <td width="35%">
                   
                    
                </td>   
                
                    
                </tr>
               
                <tr>
                <td colspan="5">
                <hr />
                </td>
                </tr>
                <tr>
                <td  align="center"  colspan="5">
                    <asp:Button ID="btnSearch" runat="server"  CssClass="button" Text="Search" OnClick="btnSearch_Click"
                                                ></asp:Button>
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="button"  OnClick="btnCancel_Click" CausesValidation="false"/>
                  
        </td>
        </tr>
        </table>
            </asp:Panel>
            </td>
          </tr>
         </table>
            <br />
      <table align="center" style="text-align:left" width="100%">
      <tr>
     <tr>
                <td  align="center"  colspan="5">
                    <%--<asp:Panel ID="pnlPO" runat="server">--%>
                        <%--<PO:Status ID="pos1" runat="server" />--%>
                        <table width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td align="left">
                                
                            </td>
                            <td align="right">
                                <asp:Label ID="lblCount" CssClass="copy10grey" runat="server" ></asp:Label>

                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" >
                                
                        <asp:GridView ID="gvDekit" AutoGenerateColumns="false"  
                        Width="100%" ShowHeader="true"  ShowFooter="false" runat="server" GridLines="Both" 
                        OnPageIndexChanging="gvDekit_PageIndexChanging" PageSize="20" AllowPaging="true" 
                        AllowSorting="true" OnSorting="gvDekit_Sorting">
                        <RowStyle BackColor="Gainsboro" />
                        <AlternatingRowStyle BackColor="white" />
                        <HeaderStyle  CssClass="buttonlabel" ForeColor="white"/>
                          <PagerStyle ForeColor="#636363" CssClass="copy10grey" HorizontalAlign="Left" />
                          <Columns>
                              <asp:TemplateField HeaderText="S.No." ItemStyle-CssClass="copy10grey"  ItemStyle-Width="2%">
                                <ItemTemplate>
                                      <%# Container.DataItemIndex + 1%>               
                                </ItemTemplate>
                            </asp:TemplateField>
                                <asp:TemplateField HeaderText="Dekit Request#" SortExpression="ServiceOrderNumber" HeaderStyle-CssClass="buttonundlinelabel"  ItemStyle-HorizontalAlign="Left" 
                                    ItemStyle-CssClass="copy10grey" ItemStyle-Width="8%">
                                    <ItemTemplate>
                                        <%--<asp:HiddenField ID="hdnSKUId" Value='<%# Eval("ItemcompanyGUID")%>' runat="server" />--%>
                                        <%--<a class="linkgrey" href="ManageServiceOrderNew.aspx?soid=<%# Eval("DekitRequestNumber") %>">
                                        <b><%# Eval("DekitRequestNumber")%></b></a>
                                        --%>
                                        <asp:LinkButton ID="lnkDekit" runat="server" ToolTip="View Dekitting" OnCommand="lnkDekit_Command" CommandArgument='<%# Eval("DeKittingID")%>' 
                                            CssClass="linkgrey"><b><%# Eval("DekitRequestNumber")%></b></asp:LinkButton>
                                        <%--<%# Eval("DekitRequestNumber")%>--%>
                                    </ItemTemplate>
                                </asp:TemplateField> 
                                <asp:TemplateField HeaderText="Customer Dekit Request#" SortExpression="CustomerOrderNumber" HeaderStyle-CssClass="buttonundlinelabel"   ItemStyle-HorizontalAlign="Left" 
                                    ItemStyle-CssClass="copy10grey" ItemStyle-Width="13%">
                                    <ItemTemplate>
                                        <%# Eval("CustomerRequestNumber")%>
                                        </ItemTemplate>
                                </asp:TemplateField> 
                              <asp:TemplateField HeaderText="Category Name" SortExpression="CategoryName" HeaderStyle-CssClass="buttonundlinelabel"  ItemStyle-HorizontalAlign="Left" 
                                  ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                    <ItemTemplate>                                                
                                         <%# Eval("CategoryName")%>
                                    </ItemTemplate>
                                </asp:TemplateField>   
                              <asp:TemplateField HeaderText="Kitted SKU#" SortExpression="SKU" HeaderStyle-CssClass="buttonundlinelabel"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" 
                                  ItemStyle-Width="13%">
                                    <ItemTemplate>                                                
                                         <%# Eval("SKU")%>
                                    </ItemTemplate>
                                </asp:TemplateField>   
                              <asp:TemplateField HeaderText="ProductName" SortExpression="ProductName" HeaderStyle-CssClass="buttonundlinelabel"  ItemStyle-HorizontalAlign="Left" 
                                  ItemStyle-CssClass="copy10grey" ItemStyle-Width="22%">
                                    <ItemTemplate>                                                
                                         <%# Eval("ProductName")%>
                                    </ItemTemplate>
                                </asp:TemplateField>   
                              <asp:TemplateField HeaderText="Quantity" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey"
                                  ItemStyle-Width="5%">
                                    <ItemTemplate>                                                
                                         <%# Eval("Quantity")%>&nbsp;
                                    </ItemTemplate>
                                </asp:TemplateField>
                              <asp:TemplateField HeaderText="Created By" SortExpression="CreatedBy" HeaderStyle-CssClass="buttonundlinelabel"  ItemStyle-HorizontalAlign="Left" 
                                  ItemStyle-CssClass="copy10grey" ItemStyle-Width="7%">
                                    <ItemTemplate>                                                
                                         <%# Eval("CreatedBy")%>
                                    </ItemTemplate>
                                </asp:TemplateField>   
                              <asp:TemplateField HeaderText="Approved By" SortExpression="ApprovedBy" HeaderStyle-CssClass="buttonundlinelabel"  ItemStyle-HorizontalAlign="Left" 
                                  ItemStyle-CssClass="copy10grey" ItemStyle-Width="7%">
                                    <ItemTemplate>                                                
                                         <%# Eval("ApprovedBy")%>
                                    </ItemTemplate>
                                </asp:TemplateField>   
                              
                                <asp:TemplateField HeaderText="Create Date" SortExpression="DeKitDate" HeaderStyle-CssClass="buttonundlinelabel"  ItemStyle-HorizontalAlign="Left" 
                                    ItemStyle-CssClass="copy10grey" ItemStyle-Width="6%">
                                    <ItemTemplate>
                                        <%# Convert.ToDateTime(Eval("DeKitDate")).ToString("MM-dd-yyyy") %>
                                        </ItemTemplate>
                                </asp:TemplateField>  
                              <asp:TemplateField HeaderText="Status" SortExpression="DeKitStatus" HeaderStyle-CssClass="buttonundlinelabel"  ItemStyle-HorizontalAlign="Left" 
                                  ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                                    <ItemTemplate>                                                
                                         <%# Eval("DeKitStatus")%>
                                    </ItemTemplate>
                                </asp:TemplateField>   
                              
                              <asp:TemplateField>
                                    <ItemTemplate>
                                        
                                         <asp:ImageButton ID="imgDel" runat="server" Visible='<%# Convert.ToString(Eval("DeKitStatus")).ToLower()=="completed" ? false : true %>' OnClientClick="return confirm('Are you sure you want to delete?')"  
                                            CommandArgument='<%# Eval("DeKittingID") %>'  OnCommand="imgDel_Command" AlternateText="Delete Dekitting" ImageUrl="~/images/delete.png" />
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
