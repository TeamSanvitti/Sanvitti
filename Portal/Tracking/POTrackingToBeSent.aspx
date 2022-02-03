<%@ Page Language="C#" AutoEventWireup="true" Async="true" CodeBehind="POTrackingToBeSent.aspx.cs" Inherits="avii.Tracking.POTrackingToBeSent" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="~/Controls/Header.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Shipment Notification</title>
<script src="https://code.jquery.com/jquery-1.11.0.min.js"></script>

<%--<script src="http://code.jquery.com/ui/1.10.4/jquery-ui.min.js"></script>--%>
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
<body>
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
            &nbsp;&nbsp;Shipment Notification
			</td>
		</tr>
    
    </table>
        <table cellSpacing="0" cellPadding="0" align="center" width="95%" border="0" id="maintbl">
        <tr>
			<td>
    <asp:UpdatePanel ID="upnlCustomers" UpdateMode="Conditional" runat="server">
	<ContentTemplate>
	
    <table cellSpacing="0" cellPadding="0" align="center" width="100%" border="0">
    		<tr>                    
                <td >
                    <asp:Label ID="lblMsg" runat="server" Width="100%" CssClass="errormessage"></asp:Label></td>
            </tr> 
     </table> 
        
        
      <table bordercolor="#839abf" border="1" width="100%" align="center" cellpadding="0" cellspacing="0">
      <tr>
        <td>
         <asp:Panel  ID="pnlSearch" runat="server"  DefaultButton="btnSearch" >
     <table width="100%" border="0" class="box" align="center" cellpadding="2" cellspacing="2">
     <%--<tr style="height:1px">
     <td style="height:1px"></td>
     </tr>
     --%>
            <tr id="trCustomer" runat="server">
                <td class="copy10grey"  align="right">
                   Customer:
                </td>
                <td>
                <asp:DropDownList ID="dpCompany" runat="server" class="copy10grey"  Width="81%">
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
           
            <tr>
                <td class="copy10grey"  align="right">
                   Shipping Date From:
                </td>
                <td>
                <asp:TextBox ID="txtFromDate"  onfocus="set_focus1();" CssClass="copy10grey" runat="server" Width="80%" MaxLength="15" ></asp:TextBox>
                <img id="img1" alt=""  onclick="document.getElementById('<%=txtFromDate.ClientID%>').value = ''; displayCalendar(document.getElementById('<%=txtFromDate.ClientID%>'),'mm/dd/yyyy',this,true);" src="../fullfillment/calendar/sscalendar.jpg" />
                
                </td>
                <td  width="1%">
                    &nbsp;
                </td>
                <td class="copy10grey"  align="right">
                   Shipping Date To:
                </td>
                <td>
                <asp:TextBox ID="txtEndDate"  onfocus="set_focus2();" CssClass="copy10grey" runat="server" Width="80%" MaxLength="12" ></asp:TextBox>
                <img id="img2" alt=""  onclick="document.getElementById('<%=txtEndDate.ClientID%>').value = ''; displayCalendar(document.getElementById('<%=txtEndDate.ClientID%>'),'mm/dd/yyyy',this,true);" src="../fullfillment/calendar/sscalendar.jpg" />
                
                </td>   
                </tr>
                
            <tr>
                <td class="copy10grey"  align="right">
                    Fulfillment#:
                </td>
                <td>
                    <asp:TextBox ID="txtPONum" MaxLength="20"   CssClass="copy10grey" runat="server" Width="80%" ></asp:TextBox>
                
                </td>
                <td  width="1%">
                    &nbsp;
                </td>
                <td class="copy10grey"  align="right">
                    Tracking#:
                </td>
                <td>
                     <asp:TextBox ID="txtTrackingNo" MaxLength="25"   CssClass="copy10grey" runat="server" Width="80%" ></asp:TextBox>
                
             
                    </td>   
                </tr>
         <tr>
                <td class="copy10grey"  align="right">
                    Show Shipment Tracking Sent records
                </td>
                <td>
                           <asp:CheckBox ID="chkShipment"   CssClass="copy10grey" runat="server" ></asp:CheckBox>
                
                </td>
                <td  width="1%">
                    &nbsp;
                </td>
                <td class="copy10grey"  align="right">
                   
                </td>
                <td>
                   
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

            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="button" OnClick="btnSearch_Click" CausesValidation="false" OnClientClick="return ShowSendingProgress(2);"    />
            
         &nbsp;
           <asp:Button ID="btnCancel2" runat="server" Text="Cancel" Visible="true" CssClass="button" OnClick="btnCancel_Click" CausesValidation="false"/>
                   
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
            <td  align="left" style="height:8px; vertical-align:bottom">                                    
                                
            <strong>   
                <asp:Label ID="lblCount" CssClass="copy10grey" runat="server" ></asp:Label> &nbsp;&nbsp;
            </strong>               
            </td>
            <b></b>

            <td  align="right" style="height:8px; vertical-align:bottom">
                     <asp:Button ID="btnSubmit" runat="server" Text="Submit" Visible="false" CssClass="button"  OnClientClick="return ShowSendingProgress(1);"    OnClick="btnSubmit_Click" CausesValidation="false"/>
                     &nbsp;
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" Visible="false" CssClass="button" OnClick="btnCancel_Click" CausesValidation="false"/>
                  
                    <%--  &nbsp;
                    <asp:Button ID="btnRefresh" runat="server" Text="Refresh" Visible="false" CssClass="button" OnClick="btnRefresh_Click" CausesValidation="false"/>
                   --%>
            </td>
        </tr>

        <tr>
            <td colspan="2" align="center">
            <asp:GridView ID="gvPOTracking" runat="server" AutoGenerateColumns="false" 
                 PageSize="50" AllowPaging="true" Width="100%" OnPageIndexChanging="gvPOTracking_PageIndexChanging"    GridLines="Both"
                DataKeyNames="PO_ID" AllowSorting="true" OnSorting="gvPOTracking_Sorting" OnRowDataBound="gvPOTracking_RowDataBound" >
                <RowStyle BackColor="Gainsboro" />
                <AlternatingRowStyle BackColor="white" />
                <HeaderStyle  CssClass="buttongrid" ForeColor="white"/>
                <FooterStyle CssClass="white"  />
                <PagerStyle ForeColor="#636363" CssClass="copy10grey" HorizontalAlign="Left" />
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
                    <asp:TemplateField HeaderText="Fulfillment Number" HeaderStyle-CssClass="buttonundlinelabel" SortExpression="PO_NUM"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                        <ItemTemplate>
                            <asp:HiddenField ID="hdnPOID" runat="server" Value='<%#Eval("PO_ID")%>' />
                            <asp:HiddenField ID="hdnTrackingNumber" runat="server" Value='<%#Eval("TrackingNumber")%>' />
                            
                            <%#Eval("PO_NUM")%></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Customer Order#" HeaderStyle-CssClass="buttonundlinelabel" SortExpression="CustomerOrderNumber"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                        <ItemTemplate>
                            <%#Eval("CustomerOrderNumber")%></ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="PurchaseOrder Date" HeaderStyle-CssClass="buttonundlinelabel" SortExpression="PO_Date"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                        <ItemTemplate><%# Convert.ToString(Eval("PO_Date")) == "1/1/0001"?"":Eval("PO_Date") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Ship Method" HeaderStyle-CssClass="buttonundlinelabel" SortExpression="Ship_Via" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                        <ItemTemplate><%#Eval("Ship_Via")%></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="PO_Status" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                        <ItemTemplate><%#Eval("PO_Status")%></ItemTemplate>
                    </asp:TemplateField>
                    
                    <%--<asp:TemplateField HeaderText="Line_no" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                        <ItemTemplate><%#Eval("Line_no")%></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Qty" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                        <ItemTemplate><%#Eval("Qty")%></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="SKU" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                        <ItemTemplate><%#Eval("SKU")%></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ESN" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                        <ItemTemplate><%#Eval("ESN")%></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ICCID" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                        <ItemTemplate><%#Eval("ICC_ID")%></ItemTemplate>
                    </asp:TemplateField>
                        <asp:TemplateField HeaderText="BatchNumber" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                        <ItemTemplate><%#Eval("BatchNumber")%></ItemTemplate>
                    </asp:TemplateField>
                        --%>
                    <%--<asp:TemplateField HeaderText="ContainerID" HeaderStyle-CssClass="buttonundlinelabel" SortExpression="TrackingNumber"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                        <ItemTemplate><%#Eval("ContainerID")%></ItemTemplate>
                    </asp:TemplateField>
                    --%>
                    <asp:TemplateField HeaderText="TrackingNumber" HeaderStyle-CssClass="buttonundlinelabel" SortExpression="TrackingNumber"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                        <ItemTemplate><%#Eval("TrackingNumber")%></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ShipDate" HeaderStyle-CssClass="buttonundlinelabel" SortExpression="ShipDate"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                        <ItemTemplate><%# Convert.ToString(Eval("ShipDate")) == "1/1/0001"?"":Eval("ShipDate") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Acknowledgement" HeaderStyle-CssClass="buttongrid" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                        <ItemTemplate>
                            <%# Convert.ToString(Eval("AcknowledgmentSent"))=="0001-01-01" ? "" : Eval("AcknowledgmentSent")%>
                            
                            <asp:Label ID="lblErrorMsg" runat="server" CssClass="errormessage"></asp:Label>

                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    
                </Columns>
            </asp:GridView>
<%--</td>--%>

            <%--<tr>
            <td colspan="2">
                <hr />
                </td>
                </tr>
            --%>   
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
                <img src="/Images/ajax-loaders.gif" alt="" /> Loading ...
            </ProgressTemplate>
        </asp:UpdateProgress>
                </td>
    </tr>

    </table>
        <br /> <br />
            <br /> <br />
        <table width="100%">
        <tr>
		    <td>
			    <foot:MenuFooter id="Menuheader2" runat="server"></foot:MenuFooter>
		    </td>
	    </tr>
        </table>
        
    <script type="text/javascript">
        <%--$(function () {
            $("#<%=btnSubmit.ClientID %>").click(function () {

                ShowSendingProgress();
            });
        });--%>

        function ShowSendingProgress(flag) {
            if (flag == 2) {
                var chkBox = document.getElementById("<%=chkShipment.ClientID%>");
                var poNum = document.getElementById("<%=txtPONum.ClientID%>");
                var trackingNo = document.getElementById("<%=txtTrackingNo.ClientID%>");
                var fromDate = document.getElementById("<%=txtFromDate.ClientID%>");
                var endDate = document.getElementById("<%=txtEndDate.ClientID%>");
                if (chkBox.checked) {

                    if (poNum.value == "" && trackingNo.value == "" && fromDate.value == "" && endDate.value == "") {

                        alert('Please select second search criteria!');
                        return false;
                    }
                }
            }

            var modal = $('<div  />');
            modal.addClass("modal");
            modal.attr("id", "modalSending");
            $('body').append(modal);
            var loading = $("#modalSending.loadingcss");
            loading.show();
            var top = '300px';
            var left = '820px';
            loading.css({ top: top, left: left, color: '#ffffff' });

            var tb = $("maintbl");
            tb.addClass("progresss");


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
         <script type="text/javascript">

             function SelectAll(id) {
                 var sCheck = document.getElementById("<%= chkShipment.ClientID %>").checked;

            // alert(document.getElementById(id).checked);
            var check = document.getElementById(id).checked;
            $(':checkbox').prop('checked', check);

            $('#chkShipment').prop('checked', sCheck);
            //document.getElementById("<%= chkShipment.ClientID %>").checked = sCheck;



             }

             function set_focus1() {
                 var img = document.getElementById("img1");
                 var st = document.getElementById("txtPONum");
                 st.focus();
                 img.click();
             }
             function set_focus2() {
                 var img = document.getElementById("img2");
                 var st = document.getElementById("txtPONum");
                 st.focus();
                 img.click();
             }

        </script>
	
    </form>
</body>
</html>
