<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StockSummary.aspx.cs" Inherits="avii.Stock.StockSummary" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>
<%@ Register TagPrefix="menu" TagName="Menu" Src="~/Controls/Header.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Running Stock</title>
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
            <td>
                <table align="center" style="text-align:left" width="100%">
                <tr class="buttonlabel" align="left">
                <td>&nbsp;Running Stock</td></tr>
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
                    
                </td>
                    <td width="1%">&nbsp;</td>
                <td width="35%">
                                        
        
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
                <td class="copy10grey" align="right" width="15%">
                    SKU#:
                </td>
                <td width="1%">&nbsp;</td>
                <td width="35%">

                    <asp:TextBox ID="txtSKU" runat="server" onkeypress="return isNumberHiphen(event);"   CssClass="copy10grey" MaxLength="35"  Width="50%"></asp:TextBox>
                                
                </td>   
                <td width="1%">&nbsp;</td>
                <td class="copy10grey" align="right" width="15%">
                    Include Disabled SKU:
                </td>
                <td width="1%">&nbsp;</td>
                <td width="35%">
                   
                    <asp:CheckBox ID="chkActive" CssClass="copy10grey" runat="server" ></asp:CheckBox>
                
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
                                 
                        <asp:Button ID="btnDownload" runat="server" Text="Download"  Visible="false"  CssClass="button"  OnClick="btnDownload_Click" CausesValidation="false"/>
                    </td>
                        </tr>
                        <tr>
                            <td colspan="2" >
    
                        <asp:GridView ID="gvStocks"   AutoGenerateColumns="false"  
                        Width="100%" ShowHeader="true"  ShowFooter="false" runat="server" GridLines="Both" 
                        OnPageIndexChanging="gvStocks_PageIndexChanging" PageSize="50" AllowPaging="true" 
                        AllowSorting="true" OnSorting="gvStocks_Sorting" >
                        <RowStyle BackColor="Gainsboro" />
                        <AlternatingRowStyle BackColor="white" />
                        <HeaderStyle  CssClass="buttongrid" ForeColor="white"/>
                          <PagerStyle ForeColor="#636363" CssClass="copy10grey" HorizontalAlign="Left" />
                          <Columns>                              
                              <asp:TemplateField HeaderText="S.No."  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="1%">
                                    <ItemTemplate>
                                         <%# Container.DataItemIndex + 1%>                                        
                                    </ItemTemplate>
                                </asp:TemplateField> 
                              <asp:TemplateField HeaderText="Received Date" SortExpression="StockDate"  HeaderStyle-CssClass="buttonundlinelabel"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="7%">
                                    <ItemTemplate>
                                         <%# Convert.ToDateTime(Eval("StockDate")).ToString("MM/dd/yyyy") %>
                                    </ItemTemplate>
                                </asp:TemplateField> 
                              <asp:TemplateField HeaderText="Category Name" SortExpression="CategoryName" HeaderStyle-CssClass="buttonundlinelabel" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="9%">
                                    <ItemTemplate>
                                        <%# Eval("CategoryName")%>
                                    </ItemTemplate>
                                </asp:TemplateField>                                 
                                <asp:TemplateField HeaderText="SKU#" SortExpression="SKU" HeaderStyle-CssClass="buttonundlinelabel" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="12%">
                                    <ItemTemplate>
                                        <%# Eval("SKU")%>                                        
                                    </ItemTemplate>
                                </asp:TemplateField> 
                                <asp:TemplateField HeaderText="Product Name" SortExpression="ItemName" HeaderStyle-CssClass="buttonundlinelabel" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="21%">
                                    <ItemTemplate>
                                        <%# Eval("ItemName")%>
                                        </ItemTemplate>
                                </asp:TemplateField>  
                                <asp:TemplateField HeaderText="Opening Balance" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                                    <ItemTemplate>                                                
                                         <%# Eval("OpeningBalance")%>
                                    </ItemTemplate>
                                </asp:TemplateField> 
                                <asp:TemplateField HeaderText="Stock Received" SortExpression="StockReceived" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                                    <ItemTemplate>                                                
                                         <%# Eval("StockReceived")%>
                                    </ItemTemplate>
                                </asp:TemplateField> 
                                <asp:TemplateField HeaderText="Stock Assigned" SortExpression="StockAssigned" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                                    <ItemTemplate>                                                
                                         <%# Eval("StockAssigned")%>
                                    </ItemTemplate>
                                </asp:TemplateField>                              
                              <asp:TemplateField HeaderText="Reassignment" SortExpression="StockReassignment" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="7%">
                                    <ItemTemplate>                                                
                                         <%# Eval("StockReassignment")%>
                                    </ItemTemplate>
                                </asp:TemplateField> 
                              <asp:TemplateField HeaderText="Dekit Count" SortExpression="DekitCount" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="4%">
                                    <ItemTemplate>                                                
                                         <%# Eval("DekitCount")%>
                                    </ItemTemplate>
                                </asp:TemplateField> 
                              <asp:TemplateField HeaderText="UnProvisioning Count" SortExpression="UnProvisioningCount" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="6%">
                                    <ItemTemplate>                                                
                                         <%# Eval("UnProvisioningCount")%>
                                    </ItemTemplate>
                                </asp:TemplateField> 
                              <asp:TemplateField HeaderText="Discarded SKU" SortExpression="DiscardedSKU" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="6%">
                                    <ItemTemplate>                                                
                                         <%# Eval("DiscardedSKU")%>
                                    </ItemTemplate>
                                </asp:TemplateField> 
                              
                               <asp:TemplateField HeaderText="Closing Balance*" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                         <%# Eval("ClosingBalance")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                              <asp:TemplateField HeaderText="Last Refresh Date*" SortExpression="RefreshDate" HeaderStyle-CssClass="buttonundlinelabel" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                         <%--<%# Eval("RefreshDate")%>--%>
                                        <%# Convert.ToDateTime(Eval("RefreshDate")).ToString("MM/dd/yyyy H:mm:ss") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                              
                            </Columns>
                        </asp:GridView>
                        
                            </td>
                        </tr>
                            <tr>
                            <td align="left">
                                <asp:Label ID="lblNote" Visible="false" CssClass="copy10grey" runat="server" ><strong>*</strong> Closing Balance = (Opening Balance + Stock Received - Stock Assigned - Stock Reaassignment)</asp:Label>
                            </td>
                        </tr>
                        
                        </table>
                        
                </td>
                </tr>
            </table>
            
         </ContentTemplate>
             <Triggers>
             <asp:PostBackTrigger ControlID="btnDownload" />
             
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
