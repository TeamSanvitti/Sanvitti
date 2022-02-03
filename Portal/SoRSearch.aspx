<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SoRSearch.aspx.cs" Inherits="avii.SoRSearch" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>
<%@ Register TagPrefix="menu" TagName="Menu" Src="~/Controls/Header.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Service Order Request Search</title>
    
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
    <script type="text/javascript">
        function GoToSummary() {
            var url = "../SoRSummary.aspx";
            window.open(url);
            return false;

        }
       function set_focus1() {
           var img = document.getElementById("imgDateFrom");
           var st = document.getElementById("ddlUser");
           st.focus();
           img.click();
       }
       function set_focus2() {
           var img = document.getElementById("imgDateTo");
           var st = document.getElementById("ddlUser");
           st.focus();
           img.click();
       }

       $(document).ready(function () {
           $("#divSOR").dialog({
               autoOpen: false,
               modal: false,
               minHeight: 20,
               height: 450,
               width: 800,
               resizable: false,
               open: function (event, ui) {
                   $(this).parent().appendTo("#divContainer");
               }
           });

           $("#divSummary").dialog({
               autoOpen: false,
               modal: false,
               minHeight: 20,
               height: 450,
               width: 600,
               resizable: false,

               open: function (event, ui) {
                   $(this).parent().appendTo("#divContainer");
               }
           });

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

       function closeSORDialog() {
           //Could cause an infinite loop because of "on close handling"
           $("#divSOR").dialog('close');
       }


       function openSORDialog(title, linkID) {

           var pos = $("#" + linkID).position();
           var top = pos.top;
           var left = pos.left + $("#" + linkID).width() + 10;
           //alert(top);
           if (top > 600)
               top = 10;
           else
               top = 10;
           //top = top - 600;
           left = 300;
           $("#divSOR").dialog("option", "title", title);
           $("#divSOR").dialog("option", "position", [left, top]);

           $("#divSOR").dialog('open');

           unblockSORDialog();
       }
       function openSORDialogAndBlock(title, linkID) {
           openSORDialog(title, linkID);

           //block it to clean out the data
           $("#divSOR").block({
               message: '<img src="../images/async.gif" />',
               css: { border: '0px' },
               fadeIn: 0,
               //fadeOut: 0,
               overlayCSS: { backgroundColor: '#ffffff', opacity: 1 }
           });
       }
       function unblockSORDialog() {
           $("#divSOR").unblock();
       }


       function closeSummaryDialog() {
           //Could cause an infinite loop because of "on close handling"
           $("#divSummary").dialog('close');
       }


       function openSummaryDialog(title, linkID) {

           var pos = $("#" + linkID).position();
           var top = pos.top;
           var left = pos.left + $("#" + linkID).width() + 10;
           //alert(top);
           if (top > 600)
               top = 10;
           else
               top = 10;
           //top = top - 600;
           left = 450;
           $("#divSummary").dialog("option", "title", title);
           $("#divSummary").dialog("option", "position", [left, top]);

           $("#divSummary").dialog('open');

           unblockSummaryDialog();
       }
       function openSummaryDialogAndBlock(title, linkID) {
           openSummaryDialog(title, linkID);

           //block it to clean out the data
           $("#divSummary").block({
               message: '<img src="../images/async.gif" />',
               css: { border: '0px' },
               fadeIn: 0,
               //fadeOut: 0,
               overlayCSS: { backgroundColor: '#ffffff', opacity: 1 }
           });
       }
       function unblockSummaryDialog() {
           $("#divSummary").unblock();
       }

       

    </script>  

    <style type="text/css">
        html, body{
            background:none !important;
        }
        #overlay{
 position: fixed;
 z-index:99;
 top:0px;
 left:0px;
 background-color:#FFFFFF;
 width:100%;
height: 100%;
filter: Alpha(Opacity=80);
opacity: 0.80;
-moz-cpacity:0.80;
}
#theprogress{
background-color: blue;
width: 110px;
height: 24px;
text-align: center;
filter: Alpha(Opacity=100);
opacity: 1;
-moz-cpacity:1;
}
#modelprogress{
position: absolute;
top: 50%;
left: 50%;
margin: -11px 0 0 -55px; 
color:white;
}
body>#modelprogress
{
    position:fixed;
}
</style>
     
</head>
<body  leftmargin="0" rightmargin="0" topmargin="0">
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
    <div id="divContainer">
        <div id="divSOR" style="display:none">
					
				<asp:UpdatePanel ID="uplLog" runat="server">
					<ContentTemplate>
                        <asp:PlaceHolder ID="phLog" runat="server">
           
                           <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="100%" align="center" >
                           <tr bordercolor="#839abf">
                                <td>
                                    <table width="100%" cellSpacing="15" cellPadding="15">
                                    <tr>
                                        <td class="copy10grey" width="25%" align="right">
                                            <strong>Service Request#:</strong>
                                        </td>
                                        <td align="left" class="copy10grey" >
                                            <asp:Label ID="lblSORNumber"  CssClass="copy10grey" runat="server" ></asp:Label>
                                        </td>
                                        <td class="copy10grey" width="25%" align="right">
                                            <strong>SKU:</strong>
                                        </td>
                                        <td  align="left>
                                           <asp:Label ID="lblSKU" CssClass="copy10grey" runat="server" ></asp:Label>
                                        </td>
                                        </tr>

                                        </table>
                                        
                                        </td>
                                        </tr>
                                        </table>
                            <br />
                                        <asp:Label ID="lblSOR" runat="server" ></asp:Label>
                                            <asp:Repeater ID="rptSORLog" runat="server">
                                            <HeaderTemplate>
                                            <table width="100%" align="center">
                                                <tr >
                                                    <td class="buttongrid" width="2%">
                                                        &nbsp;S.No.
                                                    </td>
                                                    <td class="buttongrid" width="18%">
                                                        &nbsp;Date
                                                    </td>
                                                    <td class="buttongrid"  width="20%">
                                                        &nbsp;Status
                                                    </td>
                                                    <td class="buttongrid"  width="20%">
                                                        &nbsp;Requested By
                                                    </td>
                                                    <td class="buttongrid"  width="20%">
                                                        &nbsp;Created By
                                                    </td>
                                                    <td class="buttongrid"  width="20%">
                                                        &nbsp;Quantity
                                                    </td>
                                                   <%-- <td class="buttonlabel">
                                                        &nbsp;Request Data
                                                    </td>--%>

                                                    
                                                    
                                                    
                                                    
                                                    
                                                </tr>
                            
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                            <tr class="<%# Container.ItemIndex % 2 == 0 ? "alternaterow" : "" %>">
                                                
                                                <td class="copy10grey">
                                                        &nbsp;<%# Container.ItemIndex + 1 %></td>
                                
                                                <td class="copy10grey">
                                                 &nbsp;<%# Eval("SORDate")%></td>
                                                <td class="copy10grey">
                                                 &nbsp;<%# Eval("Status")%></td>
                                                <td class="copy10grey">
                                                 &nbsp;<%# Eval("RequestedUserBy")%></td>
                                                <td class="copy10grey">
                                                 &nbsp;<%# Eval("CreatedUserBy")%></td>
                                                <td class="copy10grey">
                                                 &nbsp;<%# Eval("Quantity")%></td>
                                                <%--<td class="copy10grey">
                                                 &nbsp;<%# Eval("RequestData")%></td>
                                --%>
                                            </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                </table>    
                                            </FooterTemplate>
                                            </asp:Repeater>                       


                            </asp:PlaceHolder>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                
            </div>

        <div id="divSummary" style="display:none">
				<asp:UpdatePanel ID="upSummary" runat="server">
					<ContentTemplate>
                        <asp:PlaceHolder ID="phSummary" runat="server">
           
                           <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="100%" align="center" >
                           <tr bordercolor="#839abf">
                                <td>
                                    <table width="100%" cellSpacing="5" cellPadding="5">
                                    <tr>
                                        <td class="copy10grey" width="25%" align="right">
                                            Date From:
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lblFromDate"  CssClass="copy10grey" runat="server" ></asp:Label>
                                        </td>
                                        <td class="copy10grey" width="25%" align="right">
                                            Date To:
                                        </td>
                                        <td  align="left>
                                            <asp:Label ID="lblToDate" CssClass="copy10grey" runat="server" ></asp:Label>
                                        </td>
                                        </tr>

                                        </table>
                                        </td>
                                        </tr>
                                        </table>
                                        <table width="100%" align="center">
                                        <tr >
                                            <td align="right" class="copy10grey" >
                                                <asp:Button ID="btnSummaryDownload" runat="server" Text="Download" CssClass="button"  OnClick="btnSummaryDownload_Click" CausesValidation="false"/>
                  
                                            </td>
                                        </tr>
                                        </table>
                                        <asp:Repeater ID="rptSummary" runat="server">
                                            <HeaderTemplate>
                                            <table width="100%" align="center">
                                                <tr >
                                                    <td class="buttongrid" width="2%">
                                                        &nbsp;S.No.
                                                    </td>
                                                    <td class="buttongrid" width="70%">
                                                        &nbsp;SKU
                                                    </td>
                                                    <td class="buttongrid"  width="20%">
                                                        &nbsp;Quantity
                                                    </td>
                                                </tr>
                            
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                            <tr class="<%# Container.ItemIndex % 2 == 0 ? "alternaterow" : "" %>">
                                                
                                                <td class="copy10grey">
                                                        &nbsp;<%# Container.ItemIndex + 1 %></td>
                                
                                                <td class="copy10grey">
                                                 &nbsp;<%# Eval("SKU")%></td>
                                                <td class="copy10grey" align="right">
                                                 &nbsp;<%# Eval("Quantity")%>
                                                </td>
                                            </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                </table>    
                                            </FooterTemplate>
                                            </asp:Repeater>
                        
                        </asp:PlaceHolder>
                    </ContentTemplate>
                </asp:UpdatePanel>
        </div>
    </div>
        
    <table cellspacing="0" cellpadding="0" border="0" align="center" width="95%">
    <tr valign="top">
        <td>
            <table align="center" style="text-align:left" width="100%">
                <tr class="buttonlabel" align="left">
                    <td>&nbsp;Service Order Request</td>
                </tr>
             </table>
        
     <asp:UpdatePanel ID="UpdatePanel1"  runat="server"  UpdateMode="Conditional" >
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
                     AutoPostBack="true" OnSelectedIndexChanged="dpCompany_SelectedIndexChanged"               >
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
                    Service Request#:
                </td>
                <td width="35%">
                <asp:TextBox ID="txtSORNumber" runat="server"   CssClass="copy10grey" MaxLength="20"  Width="80%"></asp:TextBox>
                               
                
                </td>
                <td  width="1%">
                    &nbsp;
                </td>
                <td class="copy10grey" align="right" width="15%">
                    Kitted SKU#:
                
                </td>
                <td width="35%">
                  <%-- <asp:TextBox ID="txtKittedSKU" runat="server"   CssClass="copy10grey" MaxLength="20"  Width="80%"></asp:TextBox>
                  --%>                             
                    <asp:DropDownList ID="ddlKitted" CssClass="copy10grey" runat="server" Width="80%">
	                </asp:DropDownList>
                
                </td>   
                
                    
                </tr>
          <tr valign="top">
                <td class="copy10grey" align="right" width="15%">
                    Date From:
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
                    Requested By:
                   
                </td>
                <td width="35%">
                    
                    <asp:DropDownList ID="ddlUser" CssClass="copy10grey" runat="server" Width="80%">
	                </asp:DropDownList>
                </td>
                <td  width="1%">
                    &nbsp;
                </td>
                <td class="copy10grey" align="right" width="15%">
                    Status:
                </td>
                <td width="35%">
                   
                    <asp:DropDownList ID="ddlStatus" CssClass="copy10grey" runat="server" Width="80%">
	                </asp:DropDownList>       
        
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
                    <asp:Button ID="btnSearch" runat="server"  CssClass="button" Text="Search" OnClick="btnSearch_Click"  CausesValidation="false" OnClientClick="return ShowSendingProgress();"></asp:Button>
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="button"  OnClick="btnCancel_Click" CausesValidation="false"/>
                  
        </td>
        </tr>
        </table>
            </asp:Panel>
            </td>
          </tr>
         </table>
            
      <table align="center" style="text-align:left" width="100%">
        <tr>
            <td  align="center"  colspan="5">
                <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td align="left">
                        <asp:Label ID="lblCount" CssClass="copy10grey" runat="server" ></asp:Label>                                
                    </td>
                    <td align="right">
                        
                        <asp:Button ID="btnSoRSummary" runat="server" Text="Summary" Visible="false" CssClass="button" OnClientClick="return GoToSummary();" OnClick="btnSoRSummary_Click" CausesValidation="false"/>&nbsp; 
                        
                        <asp:Button ID="btnSummary" runat="server" Text="Summary" Visible="false" OnClientClick="openSummaryDialogAndBlock('Summary', 'btnSummary')"
                            CssClass="button" OnClick="btnSummary_Click" CausesValidation="false"/>&nbsp; 
                        <asp:Button ID="btnDownload" runat="server" Text="Download"  Visible="false"  CssClass="button"  OnClick="btnDownload_Click" CausesValidation="false"/>
                  
                    </td>
                </tr>
                <tr>
                    <td colspan="2" >
    
                        <asp:GridView ID="gvSO"   AutoGenerateColumns="false"  
                        Width="100%" ShowHeader="true"  ShowFooter="false" runat="server" GridLines="Both" 
                        OnPageIndexChanging="gvSO_PageIndexChanging" PageSize="20" AllowPaging="true" 
                        AllowSorting="true" OnSorting="gvSO_Sorting" OnRowDataBound="GridView1_RowDataBound">
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

                                <asp:TemplateField HeaderText="Service Request#" SortExpression="SORNumber" HeaderStyle-CssClass="buttonundlinelabel"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <%--<asp:HiddenField ID="hdnSKUId" Value='<%# Eval("ItemcompanyGUID")%>' runat="server" />--%>
                                        <%--<a class="linkgrey" href="ServiceOrderRequest.aspx?sorid=<%# Eval("ServiceRequestID") %>"></a>--%>
                                            <asp:LinkButton ID="lnkSOID" runat="server" ToolTip="Edit/View" OnCommand="lnkSOID_Command" CommandArgument='<%# Eval("ServiceRequestID") %>'><b><%# Eval("SORNumber")%></b></asp:LinkButton>
                                        
                                        
                                    </ItemTemplate>
                                </asp:TemplateField> 
                                <asp:TemplateField HeaderText="Date" SortExpression="CreateDate" HeaderStyle-CssClass="buttonundlinelabel"  ItemStyle-HorizontalAlign="Left" 
                                    ItemStyle-CssClass="copy10grey" ItemStyle-Width="6%">
                                    <ItemTemplate>
                                        <%# Eval("SORDate")%>
                                        </ItemTemplate>
                                </asp:TemplateField> 
                              <asp:TemplateField HeaderText="Category Name" SortExpression="CategoryName" HeaderStyle-CssClass="buttonundlinelabel"   ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <%# Eval("CategoryName")%>
                                        </ItemTemplate>
                                </asp:TemplateField>  
                              
                              <asp:TemplateField HeaderText="Kitted SKU#" SortExpression="SKU" HeaderStyle-CssClass="buttonundlinelabel"  ItemStyle-HorizontalAlign="left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="20%">
                                    
                                    <ItemTemplate>
                                                
                                         <%# Eval("SKU")%>
                                    </ItemTemplate>
                                </asp:TemplateField> 
                              <asp:TemplateField HeaderText="Product Name" SortExpression="ProductName" HeaderStyle-CssClass="buttonundlinelabel"   ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="20%">
                                    <ItemTemplate>
                                        <%# Eval("ProductName")%>
                                        </ItemTemplate>
                                </asp:TemplateField>  
                              <asp:TemplateField HeaderText="Quantity" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="6%">
                                    
                                    <ItemTemplate>
                                                
                                         <%# Eval("Quantity")%>
                                    </ItemTemplate>
                                </asp:TemplateField> 

                              <asp:TemplateField HeaderText="User Name" SortExpression="RequestedUserBy" HeaderStyle-CssClass="buttonundlinelabel"   ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="8%">
                                    <ItemTemplate>
                                        <%# Eval("RequestedUserBy")%>
                                        </ItemTemplate>
                                </asp:TemplateField>  
                                <asp:TemplateField HeaderText="Status" SortExpression="Status" HeaderStyle-CssClass="buttonundlinelabel"   ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="6%">
                                    <ItemTemplate>
                                        <%# Eval("Status")%>
                                        </ItemTemplate>
                                </asp:TemplateField>  
                              
                                <asp:TemplateField HeaderText="Service Order"  HeaderStyle-CssClass="buttongrid"   ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="copy10grey" ItemStyle-Width="8%">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkSO" runat="server" CssClass="buybt" OnCommand="lnkSO_Command" Visible='<%# Convert.ToString(Eval("Status")) == "Received" ? true : false %>'
                                            CommandArgument='<%# Eval("SORNumber") +","+Eval("CompanyID") +","+Eval("CategoryName") %>'> &nbsp;Create&nbsp; </asp:LinkButton>
                                        
                                        </ItemTemplate>
                                </asp:TemplateField>  
                              <asp:TemplateField HeaderText="" HeaderStyle-CssClass="buttongrid"   ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                         <asp:ImageButton ID="imgLOG" AlternateText="View LOG" ToolTip="View LOG" OnCommand="imgLOG_Command"  CausesValidation="false" 
                                            CommandArgument='<%# Eval("ServiceRequestID") %>' ImageUrl="~/Images/log2-24.png"  runat="server" />
                        
                                        <%--<%# Convert.ToString(Eval("Status")) == "Received" ? true : false %>--%>
                                         <asp:ImageButton ID="imgDel" runat="server"  Visible='<%# Convert.ToString(Eval("Status")) == "Received" ? true : false %>' AlternateText="Delete SOR" ImageUrl="~/images/delete.png" CommandArgument='<%# Eval("ServiceRequestID") %>'
                                             OnCommand="imgDel_Command" />
                       
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
             <asp:PostBackTrigger ControlID="btnDownload" />
             <asp:PostBackTrigger ControlID="btnSummaryDownload" />
         </Triggers>
        </asp:UpdatePanel>
                <asp:UpdatePanel ID="upnlJsRunner" UpdateMode="Always" runat="server">
			<ContentTemplate>
				<asp:PlaceHolder ID="phrJsRunner" runat="server"></asp:PlaceHolder>
			</ContentTemplate>
		</asp:UpdatePanel>

          <asp:UpdateProgress ID="UpdateProgress091" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
                 DisplayAfter="0"    >
            <ProgressTemplate>
                <div id="overlay">
                    <div id="modelprogress">
                        <div id="theprogress">
                            <img src="/Images/ajax-loaders.gif" /> 
                        </div>
                    </div>
                </div>
                
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
