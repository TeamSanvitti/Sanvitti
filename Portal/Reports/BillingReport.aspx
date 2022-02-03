<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BillingReport.aspx.cs" Inherits="avii.Reports.BillingReport" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>
<%@ Register TagPrefix="menu" TagName="Menu" Src="~/Controls/Header.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Billing Report</title>
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
    function ReadOnly3(evt) {
        var imgCall = document.getElementById("imgShipDate");
        imgCall.click();
        evt.keyCode = 0;
        return false;

    }
    function ReadOnly4(evt) {
        var imgCall = document.getElementById("imgShiptoDate");
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
    function set_focus3() {
        var img = document.getElementById("imgShipDate");

        img.click();
    }

    function set_focus4() {
        var img = document.getElementById("imgShiptoDate");

        img.click();
    }

        
</script>
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
        
    <table cellspacing="0" cellpadding="0" border="0" align="center" width="95%">        
        <tr valign="top">           
            <td>
                <table align="center" style="text-align:left" width="100%">
                <tr class="buttonlabel" align="left">
                <td>&nbsp;Billing Report</td></tr>
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
                    Fulfillment#:
                </td>
                    <td width="1%">&nbsp;</td>
                <td width="35%">
                                        
                    <asp:TextBox ID="txtFulfillmentNo" runat="server" CssClass="copy10grey" MaxLength="20"  Width="50%"></asp:TextBox>
                    
                </td> 

                </tr>
                <tr>
                <td class="copy10grey" align="right" width="15%">
                    Fulfillment Date From:
                </td>
                    <td width="1%">&nbsp;</td>
                <td width="35%">
                   
                    <asp:TextBox ID="txtDateFrom" runat="server" onkeydown="return ReadOnly(event);" onfocus="set_focus1();"  CssClass="copy10grey" MaxLength="12"  Width="50%"></asp:TextBox>
                    <img id="imgStockDate" alt="" onclick="document.getElementById('<%=txtDateFrom.ClientID%>').value = ''; displayCalendar(document.getElementById('<%=txtDateFrom.ClientID%>'),'mm/dd/yyyy',this,true);" src="../fullfillment/calendar/sscalendar.jpg" />
                                        
        
                </td>   
                 <td width="1%">&nbsp;</td>
                <td class="copy10grey" align="right" width="15%">
                   Fulfillment Date To:
                </td>
                    <td width="1%">&nbsp;</td>
                <td width="35%">
                   
                    <asp:TextBox ID="txtToDate" runat="server" onkeydown="return ReadOnly2(event);"  onfocus="set_focus2();"  CssClass="copy10grey" MaxLength="12"  Width="50%"></asp:TextBox>
                    <img id="imgStocktoDate" alt="" onclick="document.getElementById('<%=txtToDate.ClientID%>').value = ''; displayCalendar(document.getElementById('<%=txtToDate.ClientID%>'),'mm/dd/yyyy',this,true);" src="../fullfillment/calendar/sscalendar.jpg" />
                                        
        
                </td> 
                    
                </tr>
                <tr>
                <td class="copy10grey" align="right" width="15%">
                    Ship Date From:
                </td>
                    <td width="1%">&nbsp;</td>
                <td width="35%">
                   
                    <asp:TextBox ID="txtShipFromDate" runat="server" onkeydown="return ReadOnly3(event);" onfocus="set_focus3();"  CssClass="copy10grey" MaxLength="12"  Width="50%"></asp:TextBox>
                    <img id="imgShipDate" alt="" onclick="document.getElementById('<%=txtShipFromDate.ClientID%>').value = ''; displayCalendar(document.getElementById('<%=txtShipFromDate.ClientID%>'),'mm/dd/yyyy',this,true);" src="../fullfillment/calendar/sscalendar.jpg" />
                                        
        
                </td>   
                 <td width="1%">&nbsp;</td>
                <td class="copy10grey" align="right" width="15%">
                    Ship Date To:
                </td>
                    <td width="1%">&nbsp;</td>
                <td width="35%">
                   
                    <asp:TextBox ID="txtShipDateTo" runat="server" onkeydown="return ReadOnly4(event);"  onfocus="set_focus4();"  CssClass="copy10grey" MaxLength="12"  Width="50%"></asp:TextBox>
                    <img id="imgShiptoDate" alt="" onclick="document.getElementById('<%=txtShipDateTo.ClientID%>').value = ''; displayCalendar(document.getElementById('<%=txtShipDateTo.ClientID%>'),'mm/dd/yyyy',this,true);" src="../fullfillment/calendar/sscalendar.jpg" />
                                        
        
                </td> 
                    
                </tr>
                <tr>
                <td class="copy10grey" align="right" width="15%">
                    Fulfillment Type:
                </td>
                    <td width="1%">&nbsp;</td>
                <td width="35%">
                   
                   <asp:DropDownList ID="ddlPoType" CssClass="copy10grey" runat="server" Width="50%">
                       <asp:ListItem Text="" Value=""> </asp:ListItem>
                       <asp:ListItem Text="B2C"  Value="B2C"> </asp:ListItem>
                       <asp:ListItem Text="B2B" Value="B2B"> </asp:ListItem>
			       </asp:DropDownList>
                                 
        
                </td>   
                 <td width="1%">&nbsp;</td>
                <td class="copy10grey" align="right" width="15%">
                   Contact Name:
                </td>
                    <td width="1%">&nbsp;</td>
                <td width="35%">
                   
                   <asp:TextBox ID="txtContactName" runat="server" CssClass="copy10grey" MaxLength="20"  Width="50%"></asp:TextBox>
                                    
        
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
    
                        <asp:GridView ID="gvPO"   AutoGenerateColumns="false"  
                        Width="100%" ShowHeader="true"  ShowFooter="false" runat="server" GridLines="Both" 
                        OnPageIndexChanging="gvPO_PageIndexChanging" PageSize="50" AllowPaging="true" 
                        AllowSorting="true" OnSorting="gvPO_Sorting" >
                        <RowStyle BackColor="Gainsboro" />
                        <AlternatingRowStyle BackColor="white" />
                        <HeaderStyle  CssClass="buttonlabel" ForeColor="white"/>
                          <PagerStyle ForeColor="#636363" CssClass="copy10grey" HorizontalAlign="Left" />
                          <Columns>                              
                              <asp:TemplateField HeaderText="S.No."  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="1%">
                                    <ItemTemplate>
                                         <%# Container.DataItemIndex + 1%>                                        
                                    </ItemTemplate>
                                </asp:TemplateField> 
                              <asp:TemplateField HeaderText="Fulfillment#" SortExpression="FulfillmentNumber"  HeaderStyle-CssClass="buttonundlinelabel"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="8%">
                                    <ItemTemplate>
                                        <%# Eval("FulfillmentNumber")%>
                                         
                                    </ItemTemplate>
                                </asp:TemplateField> 
                              <asp:TemplateField HeaderText="Fulfillment Date" SortExpression="FulfillmentDate" HeaderStyle-CssClass="buttonundlinelabel" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="copy10grey" ItemStyle-Width="6%">
                                    <ItemTemplate>
                                        <%# Convert.ToDateTime(Eval("FulfillmentDate")).ToString("MM/dd/yyyy") %>
                                        
                                    </ItemTemplate>
                                </asp:TemplateField>                                 
                                <asp:TemplateField HeaderText="Ship_Via" SortExpression="ShipVia" HeaderStyle-CssClass="buttonundlinelabel" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="6%">
                                    <ItemTemplate>
                                        <%# Eval("ShipVia")%>                                        
                                    </ItemTemplate>
                                </asp:TemplateField> 
                                <asp:TemplateField HeaderText="Tracking Number" SortExpression="TrackingNumber" HeaderStyle-CssClass="buttonundlinelabel" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <%# Eval("TrackingNumber")%>
                                        </ItemTemplate>
                                </asp:TemplateField>  
                                <asp:TemplateField HeaderText="Ship Date" SortExpression="ShipDate"  ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="copy10grey" ItemStyle-Width="6%">
                                    <ItemTemplate>                                                
                                         <%# Convert.ToDateTime(Eval("ShipDate")).ToString("MM/dd/yyyy") %>
                                    </ItemTemplate>
                                </asp:TemplateField> 
                                <asp:TemplateField HeaderText="Shipment Type" SortExpression="ShipmentType" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="copy10grey" ItemStyle-Width="6%">
                                    <ItemTemplate>                                                
                                         <%# Eval("ShipmentType")%>
                                    </ItemTemplate>
                                </asp:TemplateField> 
                                <asp:TemplateField HeaderText="ESN" SortExpression="ESN" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                    <ItemTemplate>                                                
                                         <%# Eval("ESN")%>
                                    </ItemTemplate>
                                </asp:TemplateField>                              
                              <asp:TemplateField HeaderText="ICC_ID" SortExpression="ICC_ID" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" >
                                    <ItemTemplate>                                                
                                         <%# Eval("ICC_ID")%>
                                    </ItemTemplate>
                                </asp:TemplateField> 
                               <asp:TemplateField HeaderText="Batch Number" SortExpression="BatchNumber" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="7%">
                                    <ItemTemplate>
                                         <%# Eval("BatchNumber")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                              <asp:TemplateField HeaderText="Ship Package" SortExpression="ShipPackage" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="6%">
                                    <ItemTemplate>
                                         <%# Eval("ShipPackage")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                              <asp:TemplateField HeaderText="Weight" SortExpression="Weight" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                         <%# Eval("Weight")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                              <asp:TemplateField HeaderText="Price" SortExpression="Price" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                         <%# Eval("Price")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                              <asp:TemplateField HeaderText="ContainerID" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="6%">
                                    <ItemTemplate>
                                         <%# Eval("ContainerID")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                              <asp:TemplateField HeaderText="ContactName" SortExpression="ContactName" HeaderStyle-CssClass="buttonundlinelabel"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                         <%# Eval("ContactName")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                              <asp:TemplateField HeaderText="Fulfillment Type" SortExpression="FulfillmentType" HeaderStyle-CssClass="buttonundlinelabel" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                         <%# Eval("FulfillmentType")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                              
                            </Columns>
                        </asp:GridView>
                        
                            </td>
                        
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
        
       
    </form>
</body>
</html>
