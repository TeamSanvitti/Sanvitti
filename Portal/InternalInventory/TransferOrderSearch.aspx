<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TransferOrderSearch.aspx.cs" Inherits="avii.InternalInventory.TransferOrderSearch" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="~/Controls/Header.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Transfer Order Search</title>
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
    <script>
        function isNumberKey(evt) {
            var charCodes = evt.keyCode ? evt.keyCode : evt.which ? evt.which : evt.charCode;
                if (charCodes > 31 && (charCodes < 48 || charCodes > 57) && charCodes != 45) {
                    charCodes = 0;
                    return false;
                }
                //return true;
                if (charCodes > 31 && (charCodes < 48 || charCodes > 57)) {
                    charCodes = 0;
                    return false;
                }
            return true;
        }
        function ValidateQty(obj) {
            objQty = document.getElementById(obj.id.replace('txtQty', 'hdQty'));
            if (obj.value == 0) {
                alert('Order quantity cannot be 0');
                obj.value = objQty.value;
                return false;
            }
            if (obj.value > objQty.value) {
                alert('Order quantity cannot be greater than requested quantity');
                obj.value = objQty.value;
                return false;
            }
            else
                return true;

        }

        function OpenNewPage(url) {
            window.open(url);
        }

        function set_focus1() {
            var img = document.getElementById("img1");
            var st = document.getElementById("btnSearch");
            st.focus();
            img.click();
        }
        function set_focus2() {
            var img = document.getElementById("img2");
            var st = document.getElementById("btnSearch");
            st.focus();
            img.click();
        }
        function ReadOnly1(evt) {
            var img = document.getElementById("img1");
            img.click();
            evt.keyCode = 0;
            return false;

        }
        function ReadOnly2(evt) {
            var img2 = document.getElementById("img2");
            img2.click();
            evt.keyCode = 0;
            return false;

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
    
    <table cellSpacing="0" cellPadding="0" align="center" width="100%" border="0">
        <tr>
			<td><head:MenuHeader id="HeadAdmin" runat="server"></head:MenuHeader></td>
		</tr>
     </table>
    <table cellspacing="0" cellpadding="0" border="0" align="center" width="95%">
		<tr>
			<td  bgcolor="#dee7f6" class="buttonlabel">
            &nbsp;&nbsp;Transfer Order Search
			</td>
		</tr>    
    </table>
    <table cellSpacing="0" cellPadding="0" align="center" width="95%" border="0" id="maintbl">
        <tr>
	        <td>
            <asp:UpdatePanel ID="upnlCode" UpdateMode="Conditional" runat="server">
	        <ContentTemplate>	            
                <table cellSpacing="0" cellPadding="0" align="center" width="100%" border="0">
    	            <tr>                    
                        <td><asp:Label ID="lblMsg" runat="server"  CssClass="errormessage"></asp:Label></td>
                    </tr> 
                </table>
                <table bordercolor="#839abf" border="1" width="100%" align="center" cellpadding="0" cellspacing="0">
            <tr>
            <td>
             <table width="100%" border="0" class="" align="center" cellpadding="5" cellspacing="5">    
                 <tr>
                 <td class="copy10grey"  align="right" width="20%" >
                      Destination Customer:
                </td>
                <td width="30%" >
                    <asp:DropDownList ID="dpCompany" TabIndex="2" runat="server" CssClass="copy10grey" Width="60%" AutoPostBack="false">									
                    </asp:DropDownList>
                    <%--OnSelectedIndexChanged="dpCompany_SelectedIndexChanged"  --%>
                </td>
                <td  width="1%">
                       &nbsp;
                    
                </td>
                <td class="copy10grey"  align="right" width="19%" >
                      
                </td>
                <td width="30%" >

                </td>   
                </tr>
                
                 <tr>
               
                <td class="copy10grey"  align="right" width="20%" >
                 Order Transfer #:
                </td>
                <td width="30%" >
                       <asp:TextBox ID="txtOrderTransferNumber"  onkeypress="return IsAlphaNumericHiphen(event);"  
                            TabIndex="2" CssClass="copy10grey" runat="server" Width="60%" MaxLength="10" ></asp:TextBox>
                </td>
               
                     <td  width="1%">
                       &nbsp;
                    
                </td>
                <td class="copy10grey"  align="right" width="19%" >
                    SKU:
                </td>
                <td width="30%" >
                          <%--<asp:DropDownList ID="ddlSKU" runat="server" class="copy10grey"  Width="60%"   ></asp:DropDownList>--%>
                    <asp:TextBox ID="txtSKU" runat="server" class="copy10grey"  Width="60%" MaxLength="50"   ></asp:TextBox>
              
                </td>   
                </tr>
                 <tr valign="top">
                <td class="copy10grey" align="right" width="20%">
                    Date From:
                </td>
                <td width="30%">
                    <asp:TextBox ID="txtDateFrom" runat="server" onkeydown="return ReadOnly1(event);"  onfocus="set_focus1();"  CssClass="copy10grey" MaxLength="12"  Width="60%"></asp:TextBox>
                    <img id="img1" alt="" onclick="document.getElementById('<%=txtDateFrom.ClientID%>').value = ''; displayCalendar(document.getElementById('<%=txtDateFrom.ClientID%>'),'mm/dd/yyyy',this,true);" src="../fullfillment/calendar/sscalendar.jpg" />
                                        
         
                </td>
                <td  width="1%">
                    &nbsp;
                </td>
                <td class="copy10grey" align="right" width="19%">
                    Date To:
                </td>
                <td width="30%">
                   
                  <asp:TextBox ID="txtDateTo" runat="server" onkeydown="return ReadOnly2(event);"  onfocus="set_focus2();"  CssClass="copy10grey" MaxLength="12"  Width="60%"></asp:TextBox>
                    <img id="img2" alt="" onclick="document.getElementById('<%=txtDateTo.ClientID%>').value = ''; displayCalendar(document.getElementById('<%=txtDateTo.ClientID%>'),'mm/dd/yyyy',this,true);" src="../fullfillment/calendar/sscalendar.jpg" />
                                                
        
                </td>                  
                    
                </tr>
           
                <tr style="height:5px">
                     <td colspan="5" style="height:5px">
                         <hr style="height:2px" />
                     </td>
                 </tr>
                <tr>
                <td colspan="5">
                <table width="100%" align="center" >
                <tr>
			        <td align="center" >
                        <div class="loadingcss" align="center" id="modalSending">
                        <img src="/Images/ajax-loaders.gif" alt=""  /> Loading...
                    </div>
			            <asp:Button ID="btnSearch" runat="server" TabIndex="18"  CssClass="buybt" Text="   Search   " onclick="btnSearch_Click" OnClientClick="return ShowSendingProgress();" />&nbsp;&nbsp;
                        <asp:Button ID="btnCancel" runat="server" TabIndex="19" CssClass="buybt" CausesValidation="false"  Text="   Cancel   " onclick="btnCancel_Click" />
                        
			        </td>
			    </tr>
			    </table> 
           
                </td>
                </tr>
                    <//table>
                </td>
                </tr>
                 </table>
            </td>
            </tr>
            </table>   
             <br />
            <table cellSpacing="0" cellPadding="0" align="center" width="100%" border="0">
        <tr>
            <td align="left">
                               
            </td>
            <td align="right">
             
              <strong> <asp:Label ID="lblCount" CssClass="copy10grey" runat="server" ></asp:Label></strong>
                        
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">

        <asp:GridView ID="gvOrders" AutoGenerateColumns="false"   
        Width="100%" ShowHeader="true"  ShowFooter="false" runat="server" GridLines="Both"
        AllowPaging="true" OnPageIndexChanging="gvOrders_PageIndexChanging" PageSize="50" 
           AllowSorting="true" OnSorting="gvOrders_Sorting" >                        
        <RowStyle BackColor="Gainsboro" />
        <AlternatingRowStyle BackColor="white" />
        <HeaderStyle  CssClass="buttongrid" ForeColor="white"/>
            <PagerStyle ForeColor="#636363" CssClass="copy10grey" HorizontalAlign="Left" />
            <Columns>
                <asp:TemplateField HeaderText="S.No." ItemStyle-CssClass="copy10grey"  ItemStyle-Width="1%">                
                <ItemTemplate>
                        <%#  Container.DataItemIndex + 1%>               
                </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Order #" SortExpression="OrderTransferNumber"  HeaderStyle-CssClass="buttonundlinelabel"   ItemStyle-HorizontalAlign="Left" 
                    ItemStyle-CssClass="copy10grey" ItemStyle-Width="8%">
                    <ItemTemplate>
                        <%# Eval("OrderTransferNumber")%>
                        </ItemTemplate>
                </asp:TemplateField>  
                 <asp:TemplateField HeaderText="Order Date" SortExpression="OrderTransferDateTime"  HeaderStyle-CssClass="buttonundlinelabel" ItemStyle-HorizontalAlign="Left" 
                    ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                    <ItemTemplate>
                        <%# Convert.ToDateTime( Eval("OrderTransferDateTime")).ToString("MM/dd/yyyy")%>
                        </ItemTemplate>
                </asp:TemplateField>  
               
                <asp:TemplateField HeaderText="Source Customer" SortExpression="SourceCustomer"  HeaderStyle-CssClass="buttonundlinelabel"   ItemStyle-HorizontalAlign="Left" 
                    ItemStyle-CssClass="copy10grey" ItemStyle-Width="8%">
                    <ItemTemplate>
                        <%# Eval("SourceCustomer")%>
                        <%-- <asp:LinkButton  ToolTip="View location history" CausesValidation="false" Height="18" OnCommand="lnkHistory_Command" 
                                                CommandArgument='<%# Convert.ToString(Eval("WarehouseLocation")) + ","+ Convert.ToString(Eval("CompanyID")) %>'  
                                             ID="lnkHistory"  runat="server"  ><%# Eval("WarehouseLocation")%>
                                 </asp:LinkButton>                                           
                        --%>        
                        </ItemTemplate>
                </asp:TemplateField>  
                <asp:TemplateField HeaderText="SKU" SortExpression="SourceSKU"  HeaderStyle-CssClass="buttonundlinelabel"   ItemStyle-HorizontalAlign="Left" 
                    ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                    <ItemTemplate>
                        <%# Eval("SourceSKU")%>
                        </ItemTemplate>
                </asp:TemplateField>  
                <asp:TemplateField HeaderText="Product Name" SortExpression="SourceItemName"  HeaderStyle-CssClass="buttonundlinelabel"   ItemStyle-HorizontalAlign="Left" 
                    ItemStyle-CssClass="copy10grey" ItemStyle-Width="15%">
                    <ItemTemplate>
                        <%# Eval("SourceItemName")%>
                        </ItemTemplate>
                </asp:TemplateField>  
                <asp:TemplateField HeaderText="Current Stock" SortExpression="SourceStock_in_Hand"  HeaderStyle-CssClass="buttonundlinelabel"   ItemStyle-HorizontalAlign="Right" 
                    ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                    <ItemTemplate>
                        <%# Eval("SourceStock_in_Hand")%>
                        </ItemTemplate>
                </asp:TemplateField>  
                <asp:TemplateField HeaderText="Requested Qty" SortExpression="RequestedQty"  HeaderStyle-CssClass="buttonundlinelabel"   ItemStyle-HorizontalAlign="Right" 
                    ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                    <ItemTemplate>
                        <%# Eval("RequestedQty")%>
                        </ItemTemplate>
                </asp:TemplateField>  
                <asp:TemplateField HeaderText="Destination Customer" SortExpression="DestinationCompanyName"  HeaderStyle-CssClass="buttonundlinelabel"   ItemStyle-HorizontalAlign="Left" 
                    ItemStyle-CssClass="copy10grey" ItemStyle-Width="8%">
                    <ItemTemplate>
                        <%# Eval("DestinationCompanyName")%>
                        </ItemTemplate>
                </asp:TemplateField>  
                <asp:TemplateField HeaderText="Destination SKU" SortExpression="DestinationSKU"  HeaderStyle-CssClass="buttonundlinelabel"   ItemStyle-HorizontalAlign="Left" 
                    ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                    <ItemTemplate>
                        <%# Eval("DestinationSKU")%>
                        </ItemTemplate>
                </asp:TemplateField>  
                  
                <asp:TemplateField HeaderText="Destination Product" SortExpression="DestinationItemName"  HeaderStyle-CssClass="buttonundlinelabel"   ItemStyle-HorizontalAlign="Left" 
                    ItemStyle-CssClass="copy10grey" ItemStyle-Width="15%">
                    <ItemTemplate>
                        <%# Eval("DestinationItemName")%>

<%--                                 <asp:LinkButton  ToolTip="View detail" Visible='<%# Convert.ToInt32(Eval("Quantity")) > 0 ? true : false %>' CausesValidation="false" Height="18" OnCommand="lnkView_Command" 
                                                CommandArgument='<%# Convert.ToString(Eval("WarehouseLocation")) + ","+ Convert.ToString(Eval("ItemCompanyGUID")) %>'  
                                             ID="lnkView"  runat="server"  ><%# Eval("DestinationItemName")%>
                                 </asp:LinkButton>                                           
                        --%>           
                        </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Current Stock" SortExpression="DestinationStock_in_Hand"  HeaderStyle-CssClass="buttonundlinelabel"   ItemStyle-HorizontalAlign="Right" 
                    ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                    <ItemTemplate>
                        <%# Eval("DestinationStock_in_Hand")%>
                        </ItemTemplate>
                </asp:TemplateField>  
                 <asp:TemplateField HeaderText="Status" SortExpression="OrderTransferStatus"  HeaderStyle-CssClass="buttonundlinelabel"   ItemStyle-HorizontalAlign="Left" 
                    ItemStyle-CssClass="copy10grey" ItemStyle-Width="3%">
                    <ItemTemplate>
                        <%# Eval("OrderTransferStatus")%>
                        </ItemTemplate>
                </asp:TemplateField>  
               
                <asp:TemplateField HeaderText="Order Qty"   HeaderStyle-CssClass="buttongrid"   ItemStyle-HorizontalAlign="Left" 
                    ItemStyle-CssClass="copy10grey" ItemStyle-Width="6%">
                    <ItemTemplate>
                              <asp:TextBox ID="txtQty" runat="server" class="copy10grey" Text='<%# Eval("TransferQty")%>' onkeydown="return isNumberKey(event);"  Width="100%" MaxLength="5" onchange="return ValidateQty(this);"   ></asp:TextBox>
                              <asp:HiddenField ID="hdQty" runat="server"  Value='<%# Eval("TransferQty")%>'    ></asp:HiddenField>
              
                        </ItemTemplate>
                </asp:TemplateField>  
               
                <asp:TemplateField HeaderText="" HeaderStyle-CssClass="buttongrid"   ItemStyle-HorizontalAlign="Left" 
                    ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                    <ItemTemplate>
                        <table cellpadding="2">
                            <tr valign="top">
                                <td>
                                     <asp:ImageButton ID="imgTO"  ToolTip="View Assignment" OnCommand="imgTO_Command"  CausesValidation="false" 
                            CommandArgument='<%# Eval("OrderTransferID") %>' ImageUrl="~/Images/view.png"  runat="server" />
                        
                                </td>
                                <td>
                                    <asp:LinkButton  ToolTip="Accept" Visible='<%# Convert.ToString(Eval("OrderTransferStatus")) == "Pending" ? true : false %>' CausesValidation="false" Height="18" 
                                        OnCommand="lnkAccept_Command" 
                                                            CommandArgument='<%# Eval("OrderTransferID") +","+ Container.DataItemIndex +","+ Eval("IsESNRequired") %>'  
                                                         ID="lnkAccept"  runat="server" Text="Approve" >
                                    </asp:LinkButton>
                                    <asp:LinkButton  ToolTip="Receive" Visible='<%# Convert.ToString(Eval("OrderTransferStatus")) == "Approved" ? true : false %>' CausesValidation="false" Height="18" 
                                        OnCommand="lnkReceive_Command" 
                                                            CommandArgument='<%# Eval("OrderTransferID") +","+ Container.DataItemIndex +","+ Eval("IsESNRequired") %>'  
                                                         ID="lnkReceive"  runat="server" Text="Receive" >
                                    </asp:LinkButton>
                        
                                </td>
                                <td>
                                    <asp:LinkButton  ToolTip="Reject" Visible='<%# Convert.ToString(Eval("OrderTransferStatus")) == "Pending" ? true : false %>' CausesValidation="false" Height="18" 
                                        OnCommand="lnkReject_Command" CommandArgument='<%# Eval("OrderTransferID") %>'  
                                                         ID="lnkReject"  runat="server" Text="Reject"  > 
                                    </asp:LinkButton>
                        
                                </td>
                                <td>
                                    <asp:LinkButton  ToolTip="Cancel" Visible='<%# Convert.ToString(Eval("OrderTransferStatus")) == "Pending" ? true : false %>' CausesValidation="false" Height="18" 
                                        OnCommand="lnkCancel_Command" CommandArgument='<%# Eval("OrderTransferID") %>'  
                                                         ID="lnkCancel"  runat="server" Text="Cancel"  > 
                                    </asp:LinkButton>
                        
                                </td>
                                
                            </tr>
                        </table>
                        
                        </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>               
        </td>
    </tr> 
        
</table>
    
            </ContentTemplate>
            </asp:UpdatePanel>
		
            </td>
       </tr>
    </table>
        <br /> <br />
            <br /> <br /><br /> <br />
            
        <table width="100%">
        <tr>
		    <td>
			    <foot:MenuFooter id="Menuheader2" runat="server"></foot:MenuFooter>
		    </td>
	    </tr>
        </table>
        
    </form>
</body>
</html>
