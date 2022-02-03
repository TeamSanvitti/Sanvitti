<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="avii.Home" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="~/Controls/Header.ascx" %>
<%@ Register TagPrefix="PO" TagName="POStatus" Src="~/Controls/POStatus.ascx" %>
<%@ Register TagPrefix="SKU" TagName="PoSkuStock" Src="~/Controls/PoSKUStock.ascx" %>
<%@ Register TagPrefix="PO" TagName="StockInDemand" Src="~/Controls/PoStockInDemand.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>.:: LAN Global Inc. -  SNAPSHOT ::.</title>
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
<body  bgcolor="#ffffff"  leftmargin="0" topmargin="0" marginwidth="0" marginheight="0" >
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <table cellspacing="0" cellpadding="0" border="0"  width="100%" align="right" >
        <tr>
            <td  align="right">
                <head:MenuHeader ID="HeadAdmin1" runat="server" ></head:MenuHeader>
            </td>
        </tr> 
        </table>
        <table  cellSpacing="1" cellPadding="1" width="100%" align="center">
        <tr>
			<td colSpan="6" bgcolor="#007fc1" class="buttonlabel" align="left" style="font-size:14px">&nbsp;&nbsp;SNAPSHOT
			</td>
        </tr>        
        </table>
          
        <table cellSpacing="0" cellPadding="0" align="center" width="100%" border="0" id="maintbl">
        <tr>
			<td>
    <asp:UpdatePanel ID="UpdatePanel1"  runat="server" ChildrenAsTriggers="true"  >
     <%--<Triggers>
         <asp:AsyncPostBackTrigger ControlID="btnStSearch" EventName="Click" />
         
     </Triggers>--%>
     <ContentTemplate>
     
    <table width="100%"   border="0" cellpadding="1" cellspacing="1" >
    <tr valign="middle">
        <td  align="left"  class="copy10grey"  width="60%"   >
        <asp:Panel ID="pnlCust" runat="server" >
        <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td>
            &nbsp;&nbsp;Company Name:  &nbsp;&nbsp;  <asp:DropDownList ID="dpCompany" runat="server" AutoPostBack="false" 
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
        <td width="100%">
            <table width="100%"   border="0" cellpadding="0" cellspacing="0" >
            <tr valign="top">
            <td width="24%">
            <table width="100%">
            <tr>
                <td class="buttonlabel" style="font-size:14px; height:18px">
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
            <td width="1px">
            
            </td>
            <td width="35%">
            <table width="100%" border="0">
            <tr>
                <td class="buttonlabel" style="font-size:14px; height:18px">
                Back Order Stock
                </td>
            </tr>
            <tr>
                <td>
                <table bordercolor="#839abf" border="1" cellspacing="0" cellpadding="0" width="100%" align="center">
                <tr bordercolor="#839abf">
                    <td>
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
                <td width="1px">
            
            </td>
                <td  width="40%">
                      <table width="100%">
            <tr>
                <td class="buttonlabel" style="font-size:14px; height:18px">
                
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
                    <br />
          
                </td>
            </tr>
          <tr valign="top">
                <td colspan="3">
                    <br />
          
                </td>
            </tr>
  <tr valign="top">
                <td colspan="3">
                    <br />
          
                </td>
            </tr>
  <tr valign="top">
                <td colspan="5" align="right">
                    <asp:LinkButton ID="lnkDashboard" runat="server" CssClass="buttons" OnClick="lnkDashboard_Click">LINK TO DASHBOARD >></asp:LinkButton> &nbsp; &nbsp;
          
                </td>
            </tr>

            </table>  
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
