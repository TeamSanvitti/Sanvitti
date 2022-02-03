<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SKUStockStatus.aspx.cs" Inherits="avii.SKUStockStatus" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>
<%@ Register TagPrefix="menu" TagName="Menu" Src="~/Controls/Header.ascx" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>STOCK</title>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>

    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.4.0/jquery.min.js"></script>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/jquery-ui.min.js"></script>
    <link href="https://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/themes/start/jquery-ui.css"  rel="stylesheet" type="text/css" />

    
    <link rel="stylesheet"  type="text/css" href="../fullfillment/calendar/dhtmlgoodies_calendar.css" media="screen" />
	<script type="text/javascript" src="../fullfillment/calendar/dhtmlgoodies_calendar.js"></script>  
    <script type="text/javascript">
        function isNumberHiphen(evt) {

            var charCodes = evt.keyCode ? evt.keyCode : evt.which ? evt.which : evt.charCode;
            if (((((charCodes > 31 && (charCodes < 48 || charCodes > 57) && charCodes != 45) && charCodes != 46) && charCodes != 95) && !(charCodes > 96 && charCodes < 123)) && !(charCodes > 64 && charCodes < 91)) {
                // alert(charCodes);

                charCodes = 0;
                return false;
            }

            return true;
        }
        function ReadOnly(evt) {
		        var imgCall = document.getElementById("imgStockDate");
		        imgCall.click();
		        evt.keyCode = 0;
		        return false;

        }
        function ReadOnly2(evt) {
		        var imgCall = document.getElementById("imgStocktoDate");
		        imgCall.click();
		        evt.keyCode = 0;
		        return false;

        }
        
        function set_focus1() {
		        var img = document.getElementById("imgStockDate");
		        
		        img.click();
        }

        function set_focus2() {
		        var img = document.getElementById("imgStocktoDate");
		        
		        img.click();
		    }
		   
        
</script>
    <script type="text/javascript">
        $(document).ready(function () {


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
        });

        

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
    
<%--    <style type="text/css">
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
</style>--%>

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
    </table><br />

    <table cellspacing="0" cellpadding="0" border="0" align="center" width="95%">        
        <tr valign="top">           
            <td>
                <table align="center" style="text-align:left" width="100%">
                <tr class="buttonlabel" align="left">
                <td>&nbsp;Stock  Receive</td></tr>
             </table><br />
        
             <div id="divContainer">
                    
        <div id="divSummary" style="display:none">
				<asp:UpdatePanel ID="upSummary" runat="server">
					<ContentTemplate>
                        <asp:PlaceHolder ID="phSummary" runat="server">
           
                                        <table width="100%" align="center">
                                        <tr >
                                            <td align="right" class="copy10grey" >
                                                <asp:Button ID="btnSummaryDownload" runat="server" Text="Download" CssClass="button"  Visible="false" CausesValidation="false"/>
                  
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
                                                        &nbsp;Received Quantity
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
                                                 &nbsp;<%# Eval("ReceivedQty")%>
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
     
           <tr>
                <td colspan="3" width="50%">
                <asp:Panel  ID="pnlCust" Width="100%" runat="server"   >
                <table width="100%">
                    <tr valign="top">
                    <td class="copy10grey" align="right" width="30%">
                        Customer Name:
                    </td>
                        <td width="1%">&nbsp;&nbsp;</td>
                    <td width="70%">
                    <asp:DropDownList ID="dpCompany" CssClass="copy10grey" runat="server" Width="50%"
                                        >
									    </asp:DropDownList>
                
                    </td>
                    </tr>
                </table>
                    </asp:Panel>
                </td>
                <td width="1%">&nbsp;</td>
                <td class="copy10grey" align="right" width="15%">
                    SKU#:
                </td>
                    <td width="1%">&nbsp;</td>
                <td width="35%">
                   
                    <asp:TextBox ID="txtSKU" runat="server" onkeypress="return isNumberHiphen(event);"   CssClass="copy10grey" MaxLength="30"  Width="50%"></asp:TextBox>
                                        
        
                </td> 

                </tr>
                <tr>
                <td class="copy10grey" align="right" width="15%">
                    Stock From Date:
                </td>
                    <td width="1%">&nbsp;</td>
                <td width="35%">
                   
                    <asp:TextBox ID="txtStockDate" runat="server" onkeydown="return ReadOnly(event);" onfocus="set_focus1();"  CssClass="copy10grey" MaxLength="12"  Width="50%"></asp:TextBox>
                    <img id="imgStockDate" alt="" onclick="document.getElementById('<%=txtStockDate.ClientID%>').value = ''; displayCalendar(document.getElementById('<%=txtStockDate.ClientID%>'),'mm/dd/yyyy',this,true);" src="../fullfillment/calendar/sscalendar.jpg" />
                                        
        
                </td>   
                 <td width="1%">&nbsp;</td>
                <td class="copy10grey" align="right" width="15%">
                    Stock To Date:
                </td>
                    <td width="1%">&nbsp;</td>
                <td width="35%">
                   
                    <asp:TextBox ID="txtToDate" runat="server" onkeydown="return ReadOnly2(event);"  onfocus="set_focus2();"  CssClass="copy10grey" MaxLength="12"  Width="50%"></asp:TextBox>
                    <img id="imgStocktoDate" alt="" onclick="document.getElementById('<%=txtToDate.ClientID%>').value = ''; displayCalendar(document.getElementById('<%=txtToDate.ClientID%>'),'mm/dd/yyyy',this,true);" src="../fullfillment/calendar/sscalendar.jpg" />
                                        
        
                </td> 
                    
                </tr>
                
                <tr>
                <td colspan="7">
                <hr />
                </td>
                </tr>
                <tr>
                <td  align="center"  colspan="7">
                    <asp:Button ID="btnSearch" runat="server"  CssClass="button" Text="Search" OnClick="btnSearch_Click"></asp:Button>
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
                   
                        <table width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td align="left">
                                <asp:Label ID="lblCount" CssClass="copy10grey" runat="server" ></asp:Label>
                            </td>
                            <td align="right">
                                
                        <asp:Button ID="btnSummary" runat="server" Text="Summary" Visible="false" OnClientClick="openSummaryDialogAndBlock('Summary', 'btnSummary')"
                            CssClass="button" OnClick="btnSummary_Click" CausesValidation="false"/>&nbsp; 
                        <asp:Button ID="btnDownload" runat="server" Text="Download"  Visible="false"  CssClass="button"  OnClick="btnDownload_Click" CausesValidation="false"/>
                  
                    </td>
                        </tr>
                        <tr>
                            <td colspan="2" >
    
                        <asp:GridView ID="gvStocks"   AutoGenerateColumns="false"  
                        Width="100%" ShowHeader="true"  ShowFooter="false" runat="server" GridLines="Both" 
                        OnPageIndexChanging="gvStocks_PageIndexChanging" PageSize="20" AllowPaging="true" 
                        AllowSorting="true" OnSorting="gvStocks_Sorting" >
                        <RowStyle BackColor="Gainsboro" />
                        <AlternatingRowStyle BackColor="white" />
                        <HeaderStyle  CssClass="buttongrid" ForeColor="white"/>
                          <PagerStyle ForeColor="#636363" CssClass="copy10grey" HorizontalAlign="Left" />
                          <Columns>                              
                              <asp:TemplateField HeaderText="S.No."  HeaderStyle-CssClass="buttonundlinelabel" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="1%">
                                    <ItemTemplate>
                                         <%# Container.DataItemIndex + 1%>                                        
                                    </ItemTemplate>
                                </asp:TemplateField> 
                              <asp:TemplateField HeaderText="Received Date" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="9%">
                                    <ItemTemplate>
                                         <%# Eval("OpeningBalanceDate")%>
                                    </ItemTemplate>
                                </asp:TemplateField> 
  
                                
                              <asp:TemplateField HeaderText="Category Name" SortExpression="CategoryName" HeaderStyle-CssClass="buttonundlinelabel" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <%# Eval("CategoryName")%>
                                        
                                    </ItemTemplate>
                                </asp:TemplateField> 
                                
                                <asp:TemplateField HeaderText="SKU#" SortExpression="SKU" HeaderStyle-CssClass="buttonundlinelabel" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="25%">
                                    <ItemTemplate>
                                        <%# Eval("SKU")%>
                                        
                                    </ItemTemplate>
                                </asp:TemplateField> 
                                <asp:TemplateField HeaderText="Item Name" SortExpression="ItemName" HeaderStyle-CssClass="buttonundlinelabel" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="25%">
                                    <ItemTemplate>
                                        <%# Eval("ItemName")%>
                                        </ItemTemplate>
                                </asp:TemplateField>  
                                <%--<asp:TemplateField HeaderText="Model Number" SortExpression="ModelNumber" HeaderStyle-CssClass="buttonundlinelabel" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <%# Eval("ModelNumber")%>
                                        </ItemTemplate>
                                </asp:TemplateField>  --%>
                              <asp:TemplateField HeaderText="Opening Balance" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="copy10grey" ItemStyle-Width="9%">
                                    
                                    <ItemTemplate>
                                                
                                         <%# Eval("OpeningBalance")%>
                                    </ItemTemplate>
                                </asp:TemplateField> 
                                <asp:TemplateField HeaderText="Stock Received" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="copy10grey" ItemStyle-Width="8%">
                                    
                                    <ItemTemplate>
                                                
                                         <%# Eval("StockReceived")%>
                                    </ItemTemplate>
                                </asp:TemplateField> 
                             <%-- <asp:TemplateField HeaderText="Stock Assignment" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="copy10grey" ItemStyle-Width="9%">
                                    
                                    <ItemTemplate>
                                                
                                         <%# Eval("StockAssignment")%>
                                    </ItemTemplate>
                                </asp:TemplateField> 
                             
                              <asp:TemplateField HeaderText="Reassignment" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="copy10grey" ItemStyle-Width="8%">
                                    
                                    <ItemTemplate>
                                                
                                         <%# Eval("Reassignment")%>
                                    </ItemTemplate>
                                </asp:TemplateField> 
                              
                              
                              <asp:TemplateField HeaderText="Defective ESN" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="copy10grey" ItemStyle-Width="9%">
                                    
                                    <ItemTemplate>
                                                
                                         <%# Eval("DefectiveESN")%>
                                    </ItemTemplate>
                                </asp:TemplateField> 
                               <asp:TemplateField HeaderText="Stock In Hand" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="copy10grey" ItemStyle-Width="9%">
                                    
                                    <ItemTemplate>
                                                
                                         <%# Eval("StockInHand")%>
                                    </ItemTemplate>
                                </asp:TemplateField> 
                              <asp:TemplateField HeaderText="Pending Assignment" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                    
                                    <ItemTemplate>
                                                
                                         <%# Eval("PendingAssignment")%>
                                    </ItemTemplate>
                                </asp:TemplateField>--%> 

                               <asp:TemplateField HeaderText="Closing Balance*" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                         <%# Eval("ClosingBalance")%>
                                    </ItemTemplate>
                                </asp:TemplateField>  
                              
                            </Columns>
                        </asp:GridView>
                        
                            </td>
                        </tr>
                            <tr>
                            <td align="left">
                                <asp:Label ID="lblNote" Visible="false" CssClass="copy10grey" runat="server" ><strong>*</strong> Closing Balance = (Opening Balance + Stock Received)</asp:Label>
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

          <asp:UpdateProgress ID="UpdateProgress091" runat="server" >
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

    </form>
</body>
</html>
