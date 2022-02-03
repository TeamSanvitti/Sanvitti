<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="avii.Dashboard" ValidateRequest="false"  %>
<%@ Register TagPrefix="RMA" TagName="RMAStatus" Src="~/Controls/RMAStatus.ascx" %>
<%@ Register TagPrefix="PO" TagName="POStatus" Src="~/Controls/POStatus.ascx" %>
<%@ Register TagPrefix="SKU" TagName="PoSkuStock" Src="~/Controls/PoSKUStock.ascx" %>
<%--<%@ Register TagPrefix="SKU" TagName="SKUAssigned" Src="~/Controls/FulfillmentOrderSummary.ascx" %>--%>
<%@ Register TagPrefix="sSKU" TagName="SKUStock" Src="~/Controls/CurrentStocks.ascx" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="~/Controls/Header.ascx" %>
<%@ Register TagPrefix="UC" TagName="UserLog" Src="~/Controls/UserLog.ascx" %>
<%@ Register TagPrefix="SOR" TagName="Widget" Src="~/Controls/SORWidget.ascx" %>
<%@ Register TagPrefix="PO" TagName="StockInDemand" Src="~/Controls/PoStockInDemand.ascx" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>.:: LAN Global Inc. -  Dashboard ::.</title>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    
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
	

    <link href="/aerostyle.css" type="text/css" rel="stylesheet"/>
    <link rel="stylesheet"  type="text/css" href="../fullfillment/calendar/dhtmlgoodies_calendar.css" media="screen" />
	<script type="text/javascript" src="../fullfillment/calendar/dhtmlgoodies_calendar.js"></script>  
    <div id="Div11" runat="server">
    <script type="text/javascript" language="javascript">
        function isAlphaNumberHiphen(e) {

            

                var regex = new RegExp("^[a-zA-Z0-9-]+$");
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
        function OpenNewPage(url) {
            window.open(url);
        }

        function ValidateSKU() {
           // var sku = document.getElementById("% =txtSearch.ClientID %>");
           // if (sku.value == '') {
           //     alert('SKU can not be empty');
            //    return false;
            //}
        }
        function ReadOnly(evt) {
		        var imgCall = document.getElementById("imgDateFrom");
		        imgCall.click();
		        evt.keyCode = 0;
		        return false;

        }
        function ReadOnly2(evt) {
		        var imgCall = document.getElementById("imgDateTo");
		        imgCall.click();
		        evt.keyCode = 0;
		        return false;

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
        </div>
       <style type="text/css">
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
<body bgcolor="#ffffff"  leftmargin="0" topmargin="0" marginwidth="0" marginheight="0" >
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    

    <%-- <table cellspacing="0" cellpadding="0" border="0"  width="100%" align="right" >
        <tr>
            <td  align="right">--%>
                <head:MenuHeader ID="HeadAdmin1" runat="server" ></head:MenuHeader>
            <%--</td>
        </tr> 
        </table> 
         --%>
     <%--<table cellspacing="0" cellpadding="0" border="0"  width="100%" align="left">
        <tr>
            <td>
                &nbsp;
            </td>
        </tr> 
        </table>  	
   --%>

    <table  cellSpacing="1" cellPadding="1" width="100%" align="center">
        <tr>
			<td colSpan="6" bgcolor="#007fc1" class="buttonlabel" align="left">&nbsp;&nbsp;Dashboard
			</td>
        </tr>
        
    </table>
        
        <table cellSpacing="0" cellPadding="0" align="center" width="100%" border="0" id="maintbl">
        <tr>
			<td>
    <asp:UpdatePanel ID="UpdatePanel1"  runat="server" ChildrenAsTriggers="true"  >
     <Triggers>
     <%--<asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />--%>
         <asp:AsyncPostBackTrigger ControlID="btnStSearch" EventName="Click" />
         
     </Triggers>
     <ContentTemplate>
     
    <table width="100%"   border="0" cellpadding="1" cellspacing="1" >
    <tr valign="middle">
        <td  align="left"  class="copy10grey"  width="60%"   >
        <asp:Panel ID="pnlCust" runat="server" >
        <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td>
            Company Name:  &nbsp;&nbsp;  <asp:DropDownList ID="dpCompany" runat="server" AutoPostBack="false" 
        OnSelectedIndexChanged="dpCompany_SelectedIndexChanged" class="copy10grey"  >
                                
                            </asp:DropDownList>    
            </td>
            <td width="20%">
            <asp:Button ID="btnRefresh1" runat="server" Text="Refresh" Visible="false" CssClass="button" OnClick="btnRefresh_Click" />
            </td>
            
        </tr>
        </table></asp:Panel>
         <table  cellpadding="0" cellspacing="0">
        <tr>
            
        </tr>
        </table>
         
            
        </td>
        <td align="right" class="copy10grey" >
            <table>
                <tr>
                    <td class="copy10grey" align="right" width="20%">
                    Date From:
                </td>
                <td width="35%">
                    <asp:TextBox ID="txtDateFrom" runat="server" onkeydown="return ReadOnly(event);" onfocus="set_focus1();"  CssClass="copy10grey" MaxLength="12"  Width="80%"></asp:TextBox>
                    <img id="imgDateFrom" alt="" onclick="document.getElementById('<%=txtDateFrom.ClientID%>').value = ''; displayCalendar(document.getElementById('<%=txtDateFrom.ClientID%>'),'mm/dd/yyyy',this,true);" src="../fullfillment/calendar/sscalendar.jpg" />
                                        
         
                </td>
                <td  width="1%">
                    &nbsp;
                </td>
                <td class="copy10grey" align="right" width="15%">
                    Date To:
                </td>
                <td width="35%">
                   
                  <asp:TextBox ID="txtDateTo" runat="server" onkeydown="return ReadOnly2(event);"  onfocus="set_focus2();"  CssClass="copy10grey" MaxLength="12"  Width="80%"></asp:TextBox>
                    <img id="imgDateTo" alt="" onclick="document.getElementById('<%=txtDateTo.ClientID%>').value = ''; displayCalendar(document.getElementById('<%=txtDateTo.ClientID%>'),'mm/dd/yyyy',this,true);" src="../fullfillment/calendar/sscalendar.jpg" />
                                                
        
                </td>   
                </tr>
            </table>
        
        </td>
        <td align="right">
            <asp:Button ID="btnRefresh" runat="server" Text="Refresh" CssClass="button" OnClick="btnRefresh_Click" OnClientClick="return ShowSendingProgress();" />&nbsp;
        <td>
    </tr>
    <tr>
        <td colspan="5">
        <hr />
            <div class="loadingcss" align="center" id="modalSending">
                        <img src="/Images/ajax-loaders.gif" alt=""  /> Loading...
                    </div> 
        </td>
    </tr>
    </table>
    <table width="100%"   border="0" cellpadding="5" cellspacing="5" >
    <tr valign="top" height="400">
        <td width="49%">
            <table width="100%"   border="0" cellpadding="0" cellspacing="0" >
            <tr valign="top">
            <td width="50%">
             <table width="100%">
            <tr>
                <td class="buttonlabel">
                Fulfillment Summary
                </td>
            </tr>
            <tr>
                <td>
                <table bordercolor="#839abf" border="1" cellspacing="0" cellpadding="0" width="100%" align="center">
                <tr bordercolor="#839abf">
                    <td>
                        
                    <asp:Panel ID="pnlPO" runat="server">
            
                    <PO:POStatus ID="POStatus1" runat="server" />
                    </asp:Panel>
                    </td>
                </tr>
                </table>
            </td>
            </tr>
            </table>
            
            </td>
            <td width="1%">
            
            </td>
            <td width="49%">
                <table width="100%" border="0">
            <tr>
                <td class="buttonlabel">
                RMA Summary
                </td>
            </tr>
            <tr>
                <td>
                <table bordercolor="#839abf" border="1" cellspacing="0" cellpadding="0" width="100%" align="center">
                <tr bordercolor="#839abf">
                    <td>
                        
                        <asp:Panel ID="pnlRMA" runat="server" >
            
                        <RMA:RMAStatus ID="RMAStatus1" runat="server" />
                        </asp:Panel>
                    </td>
                </tr>
                </table>
            </td>
            </tr>
            </table>
            
            </td>
            </tr>
            <tr valign="top">
                <td colspan="3">
                    <br />
                <table width="100%" border="0">
            <tr>
                <td class="buttonlabel">
                Ship Via Summary
                </td>
            </tr>
            <tr>
                <td>
                <table bordercolor="#839abf" border="1" cellspacing="0" cellpadding="0" width="100%" align="center">
                <tr bordercolor="#839abf">
                    <td>
                        
                        <asp:Panel ID="pnlShipby" runat="server" >
                        <asp:Label ID="lblShipBy" runat="server" CssClass="errormessage"></asp:Label>
                         
                            <asp:GridView ID="gvShipby" OnPageIndexChanging="gvShipby_PageIndexChanging"    AutoGenerateColumns="false"  
                            Width="100%" ShowHeader="true"  ShowFooter="false" runat="server" GridLines="Both" 
                            PageSize="40" AllowPaging="true" AllowSorting="false" 
                            >
                            <RowStyle BackColor="Gainsboro" />
                            <AlternatingRowStyle BackColor="white" />
                            <HeaderStyle  CssClass="buttongrid" ForeColor="white"/>
                              <PagerStyle ForeColor="#636363" CssClass="copy10grey" HorizontalAlign="Left" />
                              <Columns>                                                    
                
                                    <asp:TemplateField  HeaderText="Ship Method" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" >
                                        <ItemTemplate>
                                           
                                        <%# Eval("ShipByText")%>   
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Package"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" >
                                        <ItemTemplate>
                                           
                                        <%# Eval("ShipPackage")%>   
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Count" ItemStyle-CssClass="copy10grey"  ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                             <%# Eval("ShipByCount")%> 
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField  HeaderText="Cost" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" >
                                        <ItemTemplate>
                                           
                                        $<%# Eval("Cost")%>   
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </asp:Panel>
                    </td>
                </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="lblTCost" runat="server" CssClass="copy10grey"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
            </tr>
            </table>
                </td>
            </tr>
            <tr valign="top">
                <td colspan="3">
                    <br />
                <table width="100%">
            <tr>
                <td class="buttonlabel">
                
                    Back Order Stock
                </td>
            </tr>
            <tr>
                <td>
                <table bordercolor="#839abf" border="1" cellspacing="0" cellpadding="0" width="100%" align="center">
                <tr bordercolor="#839abf">
                    <td>
                    <table cellspacing="1" cellpadding="1" width="100%" border="0" >
                    <tr>
                        <td >
                            <asp:Panel ID="pnlPoSkuStock" runat="server">
                    
                            <SKU:PoSkuStock ID="PoSkuStock1" runat="server" />
                            </asp:Panel>
                        </td>
                    </tr>
                    </table>
                    
                    </td>
                </tr>
                </table>
                </td>
            </tr>

            </table>  
                </td>
            </tr>
            <tr valign="top">
                <td colspan="3">
                    <br />
                    <table width="100%">
                    <tr>
                        <td class="buttonlabel">
                
                            Service Order Request
                        </td>
                    </tr>
                    <tr>
                        <td>
                        <asp:Panel ID="plsSku" runat="server" DefaultButton="btnSoRSearch">
                             <table bordercolor="#839abf" border="1" cellspacing="0" cellpadding="0" width="100%" align="center">
                        <tr bordercolor="#839abf">
                            <td>
                       
                        <table cellspacing="5" cellpadding="5" width="100%" border="0" >
                        <tr valign="top">
                            <td class="copy10grey" valign="top" width="5%" >
                                SKU:  &nbsp;
                                </td>
                                <td class="copy10grey" valign="top" width="30%" >
                                    <asp:TextBox ID="txtSoRSKU" Width="70%" onkeypress="return isAlphaNumberHiphen(event);"  CssClass="copy10grey" runat="server"></asp:TextBox>  &nbsp;  &nbsp;

                                </td>
                            <td class="copy10grey" valign="top" width="5%" >
                                Status:  &nbsp;
                                </td>
                                <td class="copy10grey" valign="top" width="30%" >
                                    <asp:DropDownList ID="ddlSORStatus" Width="70%" CssClass="copy10grey" runat="server"></asp:DropDownList>  &nbsp;  &nbsp;

                                </td>
                        
                                <td class="copy10grey" valign="top" width="20%" align="right" >
                                    <asp:Button ID="btnSoRSearch" runat="server" Text="Search" CssClass="button" OnClick="btnSoRSearch_Click" OnClientClick="return ShowSendingProgress();" />
                                    </td>
                                </tr>
                        </table>
                        </asp:Panel>
                        
                                <asp:Panel ID="pnlSOR" runat="server" >
                            
                                 <SOR:Widget ID="sor1" runat="server"></SOR:Widget>
                          
                                </asp:Panel>
                            </td>
                        </tr>
                        </table>
                    </td>
            </tr>

            </table>
                </td>
            </tr>
            <tr valign="top">
                <td colspan="3">
                    <br />
            <table width="100%">
            <tr>
                <td class="buttonlabel">
                
                    Stock In Demand
                </td>
            </tr>
            <tr>
                <td>
                <asp:Panel ID="pnlSIDSearch" runat="server" DefaultButton="btnSID">
                    <table bordercolor="#839abf" border="1" cellspacing="0" cellpadding="0" width="100%" align="center">
                <tr bordercolor="#839abf">
                    <td>
                
                        <table cellspacing="5" cellpadding="5" width="100%" border="0" >
                        <tr valign="top">
                            <td class="copy10grey" valign="top" width="5%" >
                                SKU:  &nbsp;
                                </td>
                                <td class="copy10grey" valign="top" width="30%" >
                                    <asp:TextBox ID="txtSIDSKU" Width="70%" onkeypress="return isAlphaNumberHiphen(event);" CssClass="copy10grey" runat="server"></asp:TextBox>  &nbsp;  &nbsp;

                                </td>
                            
                                <td class="copy10grey" valign="top" width="20%" align="right" >
                                    <asp:Button ID="btnSID" runat="server" Text="Search" CssClass="button" OnClick="btnSIDSearch_Click" OnClientClick="return ShowSendingProgress();" />
                                    </td>
                                </tr>
                        </table>
                        </asp:Panel>
                        
                    <table cellspacing="1" cellpadding="1" width="100%" border="0" >
                    <tr>
                        <td >
                            <asp:Panel ID="pnlSID" runat="server">
                    
                            <PO:StockInDemand ID="sid1" runat="server" />
                            </asp:Panel>
                        </td>
                    </tr>
                    </table>
                    
                    </td>
                </tr>
                </table>
                </td>
            </tr>

            </table>  
                </td>
            </tr>

            <tr valign="top">
                <td colspan="3">
                <table width="100%" border="0" style="display:none">
            <tr>
                <td class="buttonlabel">
                 User SignIn and SignOut Summary
                </td>
            </tr>
            <tr valign="top">
                <td>
                <table bordercolor="#839abf" border="1" cellspacing="0" cellpadding="0" width="100%" align="center">
                <tr bordercolor="#839abf">
                    <td>
                        
                        <asp:Panel ID="pnlUser" runat="server" >
                        <UC:UserLog ID="uLog1"  runat="server" />
                        
                        </asp:Panel>
                    </td>
                </tr>
                </table>
            </td>
            </tr>
            </table>
            
                </td>
            </tr>
            </table>
            
        </td>
        <%--<td width="33%">
           
        </td>--%>
        <td>
            <table width="100%" border="0">
            <tr>
                <td class="buttonlabel">
                Stock Summary
                </td>
            </tr>
            <tr>
                <td>
                 <table bordercolor="#839abf" border="1" cellspacing="0" cellpadding="0" width="100%" align="center">
                <tr bordercolor="#839abf">
                    <td>               
                 <table cellspacing="3" cellpadding="3" width="100%" border="0" >
                    <tr valign="middle">
                        <td class="copy10grey" valign="middle" width="5%" >
                            SKU:  &nbsp;
                            </td>
                        <td class="copy10grey" valign="middle" width="30%" >
                            <asp:TextBox ID="txtStSKU" Width="99%" onkeypress="return isAlphaNumberHiphen(event);"  CssClass="copy10grey" runat="server"></asp:TextBox>  &nbsp;  &nbsp;
                            </td>
                        <td class="copy10grey" valign="middle" width="20%" >
                          Kitted SKU:  <asp:CheckBox ID="chkKitted" CssClass="copy10grey" runat="server"></asp:CheckBox>  &nbsp;  &nbsp;
                            </td>
                        <td class="copy10grey" valign="middle" width="25%" >
                          Include Disabled SKU:  <asp:CheckBox ID="chkSKU" CssClass="copy10grey" runat="server"></asp:CheckBox>  &nbsp;  &nbsp;
                            </td>
                    <td class="copy10grey" valign="middle" width="20%" align="right" >
                    <asp:Button ID="btnStSearch" runat="server" Text="Search" CssClass="button" OnClick="btnStockSKUSearch_Click" OnClientClick="return ShowSendingProgress();" />
                        </td>
                    </tr>
                    </table>
                        
                        <asp:Panel ID="pnlStock" runat="server" >
                            
                         <sSKU:SKUStock ID="stock1" runat="server"></sSKU:SKUStock>
                          
                        </asp:Panel>
                    </td>
                </tr>
                      
                </table>
            </td>
            </tr>
                <tr valign="top">
                <td align="right">
                    &nbsp;
                </td>
            </tr>
                     <tr valign="top">
                <td align="right">
                    <asp:LinkButton ID="lnkDashboard" runat="server" CssClass="buttons" PostBackUrl="~/Home.aspx">LINK TO SNAPSHOT >></asp:LinkButton> &nbsp; &nbsp;
          
                </td>
            </tr>
            </table>
            

<%--               <table width="100%">
            <tr>
                <td class="buttonlabel">
                Fulfillment Order Summary
                </td>
            </tr>
            <tr>
                <td>
                <table bordercolor="#839abf" border="1" cellspacing="0" cellpadding="0" width="100%" align="center">
                <tr bordercolor="#839abf">
                    <td >
                    <table cellspacing="3" cellpadding="3" width="100%" border="0" >
                    <tr valign="middle">
                        <td class="copy10grey" valign="middle" width="10%" >
                            SKU:  &nbsp;
                            </td>
                        <td class="copy10grey" valign="middle" width="55%" >
                            <asp:TextBox ID="txtSearch" Width="99%" CssClass="copy10grey" runat="server"></asp:TextBox>  &nbsp;  &nbsp;
                            </td>
                    <td class="copy10grey" valign="middle" width="40%" align="right" >
                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="button" OnClick="btnSearch_Click"/>
                        </td>
                    </tr>
                    </table>
                    <table cellspacing="1" cellpadding="1" width="100%" border="0" >
                    <tr>
                        <td colspan="3">
                            <asp:Panel ID="pnlSKU" runat="server">
                    
                            <SKU:SKUAssigned ID="SKUAssigned1" runat="server" />
                            </asp:Panel>
                        </td>
                    </tr>
                    </table>
                    
                    </td>
                </tr>
                </table>
                </td>
            </tr>
            </table>  --%>
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
    <table cellspacing="0" cellpadding="0" border="0"  width="100%" align="center">
        
    <tr>
        <td><foot:MenuFooter id="Footer" runat="server"></foot:MenuFooter></td>
    </tr>
    </table> 
        
        <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.4.0/jquery.min.js"></script>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/jquery-ui.min.js"></script>
        <script type="text/javascript" src="../JQuery/jquery.blockUI.js"></script>
        <script type="text/javascript" src="/JSLibrary/jquery.blockUI.js"></script>

        
        <script type="text/javascript">       

            function ShowSendingProgress() {

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
    </form>
</body>
</html>
