<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ServiceOrderSearch.aspx.cs" Inherits="avii.ServiceOrderSearch" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>
<%@ Register TagPrefix="menu" TagName="Menu" Src="~/Controls/Header.ascx" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <title>Service Order Search</title>
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
	
    <link rel="stylesheet"  type="text/css" href="../fullfillment/calendar/dhtmlgoodies_calendar.css" media="screen" />
	<script type="text/javascript" src="../fullfillment/calendar/dhtmlgoodies_calendar.js"></script>  
    <script type="text/javascript">
        function Refresh() {
            //alert('refreshing..');
            var btnhdPrintlabel = document.getElementById("<%= btnhdPrintlabel.ClientID %>");
            btnhdPrintlabel.click();
        }
        function GoToSummary() {
            var url = "../SOSummary.aspx";
            window.open(url);
            return false;

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
        <div style="display:none">
    <asp:Button ID="btnhdPrintlabel" runat="server"   Text="" OnClick="btnhdPrintlabel_Click"  /> 
        
    </div>
    <table cellspacing="0" cellpadding="0" border="0" align="center" width="95%">
        
        <tr valign="top">
           
            <td   >
                <table align="center" style="text-align:left" width="100%">
                <tr class="buttonlabel" align="left">
                <td>&nbsp;Service Order</td></tr>
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
                <asp:DropDownList ID="dpCompany" CssClass="copy10grey" runat="server" Width="80%"
                                    >
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
                    Service Order#:
                </td>
                <td width="35%">
                <asp:TextBox ID="txtSONumber" runat="server"  onkeypress="return isNumberKey(event);" CssClass="copy10grey" MaxLength="20"  Width="80%"></asp:TextBox>
                               
                
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
                
                    
                </tr>
          <tr valign="top">
                <td class="copy10grey" align="right" width="15%">
                    Order Date From:
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
                   
                    <asp:TextBox ID="txtESN" runat="server" onkeypress="return isNumberKey(event);"   CssClass="copy10grey" MaxLength="18"  Width="80%"></asp:TextBox>
                                              
        
                </td>   
                
                    
                </tr>
               
                
                <tr>
                <td colspan="5">
                <hr />
                </td>
                </tr>
                <tr>
                <td  align="center"  colspan="5">
                    <div class="loadingcss" align="center" id="modalSending">
                        <img src="/Images/ajax-loaders.gif" alt=""  /> Loading...
                    </div>
                    <asp:Button ID="btnSearch" runat="server"  CssClass="button" Text="Search" OnClick="btnSearch_Click"  CausesValidation="false" OnClientClick="return ShowSendingProgress();"
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
                                <asp:Label ID="lblCount" CssClass="copy10grey" runat="server" ></asp:Label>
                            </td>
                            <td align="right">
                                <asp:LinkButton ID="lnkSumary"  Visible="false" CssClass="copy11link"  PostBackUrl="~/serviceorderSummary.aspx"    runat="server" Text="Fulfillment Summary"></asp:LinkButton>
                                <asp:LinkButton ID="lnkSummary"  Visible="false" CssClass="copy11link" OnClientClick="return GoToSummary();"     runat="server" Text="Summary"></asp:LinkButton>
                                
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" >
                                
                        <asp:GridView ID="gvSO" AutoGenerateColumns="false"  
                        Width="100%" ShowHeader="true"  ShowFooter="false" runat="server" GridLines="Both" 
                        OnPageIndexChanging="gvSO_PageIndexChanging" PageSize="20" AllowPaging="true" 
                        AllowSorting="true" OnSorting="gvSO_Sorting">
                        <RowStyle BackColor="Gainsboro" />
                        <AlternatingRowStyle BackColor="white" />
                        <HeaderStyle  CssClass="buttongrid" ForeColor="white"/>
                          <PagerStyle ForeColor="#636363" CssClass="copy10grey" HorizontalAlign="Left" />
                          <Columns>
                              <asp:TemplateField HeaderText="S.No." ItemStyle-CssClass="copy10grey"  ItemStyle-Width="2%">
                                <ItemTemplate>
                                      <%# Container.DataItemIndex + 1%>               
                                </ItemTemplate>
                            </asp:TemplateField>
                                <asp:TemplateField HeaderText="Service Order#" SortExpression="ServiceOrderNumber" HeaderStyle-CssClass="buttonundlinelabel"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="20%">
                                    <ItemTemplate>
                                       <asp:LinkButton ID="lnkSO" runat="server" CssClass="linkgrey" OnCommand="lnkSO_Command" 
                                            CommandArgument='<%# Eval("ServiceOrderId") +","+Eval("CategoryName") %>'>
                                           <b><%# Eval("ServiceOrderNumber")%></b> </asp:LinkButton>
                                        
                                        <%--
                                        <a class="linkgrey" href="ManageServiceOrderNEW.aspx?soid=<%# Eval("ServiceOrderId") %>">
                                        </a>--%>                                        
                                    </ItemTemplate>
                                </asp:TemplateField> 
                                <asp:TemplateField HeaderText="Customer Order#" SortExpression="CustomerOrderNumber" HeaderStyle-CssClass="buttonundlinelabel"   ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="20%">
                                    <ItemTemplate>
                                        <%# Eval("CustomerOrderNumber")%>
                                        </ItemTemplate>
                                </asp:TemplateField>  
                                <asp:TemplateField HeaderText="Order Date" SortExpression="OrderDate" HeaderStyle-CssClass="buttonundlinelabel"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="20%">
                                    <ItemTemplate>
                                        <%# Eval("OrderDate")%>
                                        </ItemTemplate>
                                </asp:TemplateField>  
                              <asp:TemplateField HeaderText="Kitted SKU#" SortExpression="SKU" HeaderStyle-CssClass="buttonundlinelabel"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="20%">
                                    <ItemTemplate>                                                
                                         <%# Eval("SKU")%>
                                    </ItemTemplate>
                                </asp:TemplateField>   
                              <asp:TemplateField HeaderText="Quantity" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="20%">
                                    <ItemTemplate>                                                
                                         <%# Eval("Quantity")%>&nbsp;
                                    </ItemTemplate>
                                </asp:TemplateField>   
                              
                                   <%-- <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="1%" HeaderStyle-CssClass="buttonlabel" HeaderText="Action">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgAuth" runat="server" ToolTip="Download ESN Authorization" AlternateText="Download ESN Authorization" 
                                                OnCommand="imgAuth_Command" CommandArgument='<%# Eval("ServiceOrderId") %>' src="../images/auth.png" />
                                                        
                                    </ItemTemplate>
                                </asp:TemplateField>    --%>
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
