<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BatchPrinting.aspx.cs" Inherits="avii.PrintLabel.BatchPrinting" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>
<%@ Register TagPrefix="menu" TagName="Menu" Src="~/Controls/Header.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Batch Printing</title>
<%--     <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>--%>
    <script src="https://code.jquery.com/jquery-1.11.0.min.js"></script>


    <!-- fix for 1.1em default font size in Jquery UI -->
	<style type="text/css">
		.ui-widget{font-size:11px !important;}
		.ui-state-error-text{margin-left: 10px}
	</style>
    <script type="text/javascript">
        function SelectAll(id) {
            var check = document.getElementById(id).checked;
            $(':checkbox').prop('checked', check);
        }
    </script>
    
    <script type="text/javascript">
        function IsDecimal(evt, obj) {
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
        function ValidateWeight(obj) {

            if (obj.value == 0) {
                obj.value = 1;
                alert('Weight cannot be 0');
            }
            if (obj.value > 1120) {
                obj.value = 1120; 
                alert('Weight cannot be greater than 1120 ounces');
            }

        }
        function set_focus1() {
            var img = document.getElementById("img1");
            var st = document.getElementById("ddlStatus");
            st.focus();
            img.click();
        }
        function set_focus2() {
            var img = document.getElementById("img2");
            var st = document.getElementById("ddlStatus");
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
   
 
    
    		<link rel="stylesheet"  type="text/css" href="../fullfillment/calendar/dhtmlgoodies_calendar.css" media="screen" />
		<script type="text/javascript" src="../fullfillment/calendar/dhtmlgoodies_calendar.js"></script>  
	
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
     </table><br />
     <table cellspacing="0" cellpadding="0" border="0" align="center" width="95%" >
        
        <tr valign="top">
           
            <td   >
    <table align="center" style="text-align:left" width="100%">
                <tr class="buttonlabel" align="left">
                <td>&nbsp;Batch Printing</td></tr>
             </table><br />
     
    <table cellSpacing="0" cellPadding="0" align="center" width="100%" border="0" id="maintbl">
        <tr>
			<td>  
<asp:UpdatePanel ID="UpdatePanel1"  runat="server"  UpdateMode="Conditional" >
     <ContentTemplate>
     <table  align="center" style="text-align:left" width="99%">
     <tr>
        <td>
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
     
           
            <tr id="trCustomer" runat="server" >
                <td class="copy10grey"  align="right">
                    Customer Name:
                </td>
                <td>
                <asp:DropDownList ID="dpCompany" CssClass="copy10grey" runat="server" Width="80%">
                                    </asp:DropDownList>
                
                </td>
                <td  width="1%">
                    &nbsp;
                </td>
                <td class="copy10grey"  align="right">
                </td>
                <td>
                
                </td>   
                </tr>
            <tr >
                <td class="copy10grey"  align="right">
                    From Date:
                </td>
                <td>
                <asp:TextBox ID="txtFromDate"  onfocus="set_focus1();" CssClass="copy10grey" runat="server" Width="80%" MaxLength="15" ></asp:TextBox>
                <img id="img1" alt=""  onclick="document.getElementById('<%=txtFromDate.ClientID%>').value = ''; displayCalendar(document.getElementById('<%=txtFromDate.ClientID%>'),'mm/dd/yyyy',this,true);" src="../fullfillment/calendar/sscalendar.jpg" />
                
                </td>
                <td  width="1%">
                    &nbsp;
                </td>
                <td class="copy10grey"  align="right">
                    End Date:
                </td>
                <td>
                <asp:TextBox ID="txtEndDate"  onfocus="set_focus2();" CssClass="copy10grey" runat="server" Width="80%" MaxLength="12" ></asp:TextBox>
                <img id="img2" alt=""  onclick="document.getElementById('<%=txtEndDate.ClientID%>').value = ''; displayCalendar(document.getElementById('<%=txtEndDate.ClientID%>'),'mm/dd/yyyy',this,true);" src="../fullfillment/calendar/sscalendar.jpg" />
                
                </td>   
                </tr>
                
            <tr>
                <td class="copy10grey"  align="right">
                    PO#:
                </td>
                <td>
                    <asp:TextBox ID="txtPONum"   CssClass="copy10grey" runat="server" Width="80%" ></asp:TextBox>
                
                </td>
                <td  width="1%">
                    &nbsp;
                </td>
                <td class="copy10grey"  align="right">
                    Status:
                </td>
                <td>
                    <asp:DropDownList ID="ddlStatus" runat="server" class="copy10grey"  Width="81%">
                                <asp:ListItem Text="" Value="0"></asp:ListItem>
                                <asp:ListItem Text="Pending" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Partial Processed" Value="10"></asp:ListItem>
                                <asp:ListItem Text="Processed" Value="2"></asp:ListItem>
                                <asp:ListItem Text="Partial Shipped" Value="11"></asp:ListItem>
                                <%--<asp:ListItem Text="Shipped" Value="3"></asp:ListItem>--%>
                                
                            </asp:DropDownList>
                
                    </td>
                </tr>
                
            <tr>
                <td class="copy10grey"  align="right" >
                    Fulfillment Type:
                </td>
                <td>
                       <asp:DropDownList ID="dpPOType" Width="81%" runat="server"  class="copy10grey">
                        <asp:ListItem Text="" Value=""></asp:ListItem> 
                        <asp:ListItem Text="B2B" Value="B2B"></asp:ListItem> 
                        <asp:ListItem Text="B2C" Value="B2C"></asp:ListItem> 
                    </asp:DropDownList>
                
                </td>
                <td  width="1%">
                    &nbsp;
                </td>
                <td class="copy10grey"  align="right">
                              ShipVia:    </td>
                <td>
                   <asp:DropDownList ID="dpShipBy" runat="server" class="copy10grey"  Width="81%"></asp:DropDownList>
                    
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
            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="button" OnClick="btnSearch_Click" CausesValidation="false" OnClientClick="return ShowSendingProgress();"/>
            &nbsp;
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="button" OnClick="btnCancel_Click" CausesValidation="false"/>
            &nbsp;
            
             
        
        </td>
        </tr>
            </table>
            </asp:Panel>
   
     </td>
     </tr>
     </table>           
                
        
                   
        <table align="center" width="100%" border="0" cellpadding="0" cellspacing="0">
    <tr>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td align="left" >
            <asp:Label ID="lblCount" CssClass="copy10grey" runat="server" ></asp:Label>
        </td>
        <td align="right">
                   <asp:Button ID="btnGenLable" runat="server" Visible="false" Text="Generate Label" CssClass="button" OnClick="btnGenLable_Click" OnClientClick="return ShowSendingProgress();" CausesValidation="false"/>
            
                    &nbsp;
            
                    <asp:Button ID="btnGeneratePDF" runat="server" Visible="false" Text="Print Label" CssClass="button" OnClick="btnGeneratePDF_Click" OnClientClick="return ShowSendingProgress();" CausesValidation="false"/>
            &nbsp;
            <asp:Button ID="btnCancel2" runat="server" Visible="false" Text="Cancel" CssClass="button" OnClick="btnCancel2_Click" CausesValidation="false"/>
            
        </td>
    </tr>
    <tr>
        <td colspan="2">
        
        
    <asp:GridView runat="server" ID="gvPO" AutoGenerateColumns="False" 
     PageSize="30" AllowPaging="true" Width="100%" OnPageIndexChanging="gvPO_PageIndexChanging"   
     CellPadding="3" OnRowDataBound="gvPO_RowDataBound" AllowSorting="true" OnSorting="gvPO_Sorting"
    GridLines="Vertical"  >
    <RowStyle  CssClass="copy10grey" BackColor="Gainsboro" />
    <AlternatingRowStyle BackColor="white" />
    <HeaderStyle  CssClass="buttongrid" ForeColor="white"/> 
    <PagerStyle ForeColor="#636363" CssClass="copy10grey" />
    <%-- <FooterStyle CssClass="white" buttonundlinelabel />--%>
    <Columns>
        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="1%">
            <HeaderTemplate>
            <asp:CheckBox ID="allchk"  runat="server"  />
        </HeaderTemplate>
        <ItemTemplate>
            <asp:CheckBox ID="chkPO" runat="server" CssClass="copy10grey" />
            </ItemTemplate>
        </asp:TemplateField>        
        <asp:TemplateField HeaderText="S.No." ItemStyle-CssClass="copy10grey"  ItemStyle-Width="1%" HeaderStyle-CssClass="buttongrid">
            <ItemTemplate>

                    <%# Container.DataItemIndex + 1%>
                  
            </ItemTemplate>
        </asp:TemplateField> 

        <asp:TemplateField HeaderText="Fulfillment#" ItemStyle-Width="10%" SortExpression="FulfillmentNumber"  HeaderStyle-CssClass="buttonundlinelabel">
            <ItemTemplate>
                <asp:HiddenField ID="hdnPOID" runat="server" Value='<%# Eval("POID") %>'></asp:HiddenField>
                
                <asp:Label ID="lblPoNum" runat="server" Text='<%# Eval("FulfillmentNumber") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="FulfillmentDate" ItemStyle-Width="5%" SortExpression="FulfillmentDate" HeaderStyle-CssClass="buttonundlinelabel">
            <ItemTemplate>
                <%# Convert.ToDateTime(Eval("FulfillmentDate")).ToString("yyyy-MM-dd") %>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="ShipDate" ItemStyle-Width="5%" SortExpression="ShipDate" HeaderStyle-CssClass="buttongrid">
            <ItemTemplate>
                  <%# Convert.ToDateTime(Eval("ShipDate")).ToString("yyyy-MM-dd")=="0001-01-01"?"":Convert.ToDateTime(Eval("ShipDate")).ToString("yyyy-MM-dd") %>
                <%--<%# Eval("ResponseTimeStamp") %>--%>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="ContactName" ItemStyle-Width="10%" SortExpression="ContactName"  HeaderStyle-CssClass="buttonundlinelabel">
            <ItemTemplate>
                <%# Eval("ContactName") %>
            </ItemTemplate>
        </asp:TemplateField>
        
        <asp:TemplateField HeaderText="ContactPhone" ItemStyle-Width="7%" SortExpression="ContactPhone"  HeaderStyle-CssClass="buttonundlinelabel">
            <ItemTemplate>
              <%# Eval("ContactPhone") %>
               
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="StoreID" ItemStyle-Width="7%" HeaderStyle-CssClass="buttongrid">
            <ItemTemplate>
                 <%# Eval("StoreID") %>
                
            </ItemTemplate>
        </asp:TemplateField>
        
        <asp:TemplateField HeaderText="StreetAddress" ItemStyle-Width="10%" HeaderStyle-CssClass="buttongrid">
            <ItemTemplate>
                <%# Eval("StreetAddress1") %>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Status" ItemStyle-Width="5%" SortExpression="Status" HeaderStyle-CssClass="buttonundlinelabel">
            <ItemTemplate>
                <%# Eval("Status") %>
            </ItemTemplate>
        </asp:TemplateField>
         <asp:TemplateField HeaderText="Ship Method" ItemStyle-Width="10%"  HeaderStyle-CssClass="buttongrid">
            <ItemTemplate>
                <asp:HiddenField ID="hdShipMethod" runat="server" Value='<%# Eval("ShipMethod") %>'></asp:HiddenField>
                
                <asp:DropDownList ID="ddlShipVia" runat="server" class="copy10grey"  Width="81%"></asp:DropDownList>
            </ItemTemplate>
        </asp:TemplateField>
         <asp:TemplateField HeaderText="Ship Package" ItemStyle-Width="10%"   HeaderStyle-CssClass="buttongrid">
            <ItemTemplate>
                <asp:HiddenField ID="hdShipPackage" runat="server" Value='<%# Eval("ShipPackage") %>'></asp:HiddenField>
                <asp:DropDownList ID="ddlShipPack" runat="server" class="copy10grey"  Width="81%"></asp:DropDownList>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Weight" ItemStyle-Width="5%" HeaderStyle-CssClass="buttongrid">
            <ItemTemplate>
                 
                <asp:TextBox ID="txtWeight" runat="server" Text='<%# Eval("ShippingWeight") %>' Width="90%" MaxLength="6" onkeypress="return IsDecimal(event, this);"  
                    onchange="ValidateWeight(this);" ></asp:TextBox>
            </ItemTemplate>
        </asp:TemplateField>
        
        <asp:TemplateField HeaderText="TrackingNumber" ItemStyle-Width="15%" HeaderStyle-CssClass="buttongrid">
            <ItemTemplate>
                 
                <asp:Label ID="lblTrackingNumber" runat="server" Text='<%# Eval("TrackingNumber") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        
        </Columns>
        </asp:GridView>
   
        </td>
    </tr>
       
    </table>
     
         

</ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="btnGeneratePDF" />
    </Triggers>
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

                 </td>
    </tr>

    </table>
    </td>
    </tr>
    </table>
    <br />
    <foot:MenuFooter ID="footer1" runat="server"></foot:MenuFooter>
     
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
