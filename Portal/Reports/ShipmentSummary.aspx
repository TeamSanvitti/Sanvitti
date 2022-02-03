<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShipmentSummary.aspx.cs" Inherits="avii.Reports.ShipmentSummary" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>
<%@ Register TagPrefix="menu" TagName="Menu" Src="~/Controls/Header.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Shipment Summary</title>
      <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>

    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.4.0/jquery.min.js"></script>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/jquery-ui.min.js"></script>
    <link href="https://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/themes/start/jquery-ui.css"
    rel="stylesheet" type="text/css" />


    <link rel="stylesheet"  type="text/css" href="../fullfillment/calendar/dhtmlgoodies_calendar.css" media="screen" />
	<script type="text/javascript" src="../fullfillment/calendar/dhtmlgoodies_calendar.js"></script>  
    
    <script type="text/javascript">
        function OpenNewPage(url) {
            window.open(url);
        }
       function set_focus1() {
           var img = document.getElementById("imgDateFrom");
           var st = document.getElementById("ddlShipVia");
           st.focus();
           img.click();
       }
       function set_focus2() {
           var img = document.getElementById("imgDateTo");
           var st = document.getElementById("ddlShipVia");
           st.focus();
           img.click();
       }

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
    <table cellspacing="0" cellpadding="0" border="0" align="center" style="text-align:left" width="95%">
        <tr class="buttonlabel" align="left">
        <td>&nbsp;Shipment Summary</td></tr>
    </table>
    <div id="divContainer">
        
        <div id="divSummary" style="display:none">
		    <asp:UpdatePanel ID="upSummary" runat="server">
				<ContentTemplate>
                    <asp:PlaceHolder ID="phSummary" runat="server">   
                        
                        <table cellspacing="0" cellpadding="0" border="0" align="center" style="text-align:left" width="100%">
                        <tr class="buttonlabel" align="left">
                            <td>&nbsp;Fulfillment</td>
                        </tr>
                        </table>
                        <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="100%" align="center" >
                        <tr bordercolor="#839abf">
                        <td>                           
                            <asp:Repeater ID="rptPO" runat="server">
                                <HeaderTemplate>
                                <table width="100%" align="center" cellSpacing="5" cellPadding="5">
                                    <tr >
                                        <td class="buttonlabel" width="2%">
                                            &nbsp;S.No.
                                        </td>
                                        <td class="buttonlabel" width="63%">
                                            &nbsp;Shipment Method
                                        </td>
                                        <td class="buttonlabel"  width="20%">
                                            &nbsp;Package
                                        </td>
                                        <td class="buttonlabel"  width="15%">
                                            &nbsp;Cost
                                        </td>
                                    </tr>
                            
                                </HeaderTemplate>
                                <ItemTemplate>
                                <tr class="<%# Container.ItemIndex % 2 == 0 ? "alternaterow" : "" %>">
                                                
                                    <td class="copy10grey">
                                            &nbsp;<%# Container.ItemIndex + 1 %></td>
                                
                                    <td class="copy10grey">
                                        &nbsp;<%# Eval("ShipMethod")%></td>
                                    <td class="copy10grey" >
                                        &nbsp;<%# Eval("ShipPackage")%>
                                    </td>
                                    <td class="copy10grey" align="right">
                                        &nbsp;$<%# Eval("Cost")%>
                                    </td>
                                </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </table>    
                                </FooterTemplate>
                                </asp:Repeater>
                        </td>
                        </tr>
                        </table>
                        <br />
                        
                        <table cellspacing="0" cellpadding="0" border="0" align="center" style="text-align:left" width="100%">
                            <tr class="buttonlabel" align="left">
                            <td>&nbsp;RMA</td></tr>
                        </table>
                        <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="100%" align="center" >
                        <tr bordercolor="#839abf">
                        <td>                        
                            <asp:Repeater ID="rptRMA" runat="server">
                                            <HeaderTemplate>
                                            <table width="100%" align="center">
                                                <tr >
                                                    <td class="buttonlabel" width="2%">
                                                        &nbsp;S.No.
                                                    </td>
                                                    <td class="buttonlabel" width="63%">
                                                        &nbsp;Shipment Method
                                                    </td>
                                                    <td class="buttonlabel"  width="20%">
                                                        &nbsp;Package
                                                    </td>
                                                    <td class="buttonlabel"  width="15%">
                                                        &nbsp;Cost
                                                    </td>
                                                </tr>
                            
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                            <tr class="<%# Container.ItemIndex % 2 == 0 ? "alternaterow" : "" %>">
                                                
                                                <td class="copy10grey">
                                                        &nbsp;<%# Container.ItemIndex + 1 %></td>
                                
                                                <td class="copy10grey">
                                                 &nbsp;<%# Eval("ShipMethod")%></td>
                                                <td class="copy10grey" >
                                                 &nbsp;<%# Eval("ShipPackage")%>
                                                </td>
                                                <td class="copy10grey" align="right">
                                                 &nbsp;$<%# Eval("Cost")%>
                                                </td>
                                            </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                </table>    
                                            </FooterTemplate>
                                            </asp:Repeater>
                        
                        </td>
                        </tr>
                        </table>
                        
                            </asp:PlaceHolder>
                    </ContentTemplate>
                </asp:UpdatePanel>
        </div>
    </div>
    
    <table cellspacing="0" cellpadding="0" border="0" align="center" style="text-align:left" width="95%">
        <tr>
            <td>
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
                            <asp:DropDownList ID="dpCompany" CssClass="copy10grey" runat="server" Width="80%">
							</asp:DropDownList>
                
                        </td>
                        <td  width="1%">
                            &nbsp;
                        </td>
                        <td class="copy10grey" align="right" width="15%">
                                Label Type:
                        </td>
                        <td width="35%">
                            <asp:DropDownList ID="ddlLabelType" CssClass="copy10grey" runat="server" Width="80%">
                                <asp:ListItem Text="" Value=""></asp:ListItem>
                                <asp:ListItem Text="Fulfillment" Value="Fulfillment"></asp:ListItem>
                                <asp:ListItem Text="RMA" Value="RMA"></asp:ListItem>
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
                            Ship Via:
                   
                        </td>
                        <td width="35%">
                    
                            <asp:DropDownList ID="ddlShipVia" runat="server" Width="80%" CssClass="copy10grey"></asp:DropDownList>
                                       
                        </td>
                        <td  width="1%">
                            &nbsp;
                        </td>
                        <td class="copy10grey" align="right" width="15%">
                            Ship Package:
                        </td>
                        <td width="35%">
                   
                            <asp:DropDownList ID="ddlShape" runat="server" Width="80%" CssClass="copy10grey"></asp:DropDownList>
                                              
        
                        </td>   
                
                    
                        </tr>
               
                
                        <tr>
                        <td colspan="5">
                        <hr />
                        </td>
                        </tr>
                        <tr>
                        <td  align="center"  colspan="5">
                            <asp:Button ID="btnSearch" runat="server"  CssClass="button" Text="Search" OnClick="btnSearch_Click"></asp:Button>
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
                                
                        <asp:Button ID="btnSummary" runat="server" Text="Summary" Visible="false" OnClientClick="openSummaryDialogAndBlock('Summary', 'btnSummary')"
                            CssClass="button" OnClick="btnSummary_Click" CausesValidation="false"/>&nbsp; 
                        <asp:Button ID="btnDownload" runat="server" Text="Download"  Visible="false"  CssClass="button"  OnClick="btnDownload_Click" CausesValidation="false"/>
                  
                    </td>
                </tr>
                <tr>
                    <td colspan="2" >
    
                        <asp:GridView ID="gvShipment"   AutoGenerateColumns="false"  
                        Width="100%" ShowHeader="true"  ShowFooter="false" runat="server" GridLines="Both" 
                        OnPageIndexChanging="gvShipment_PageIndexChanging" PageSize="20" AllowPaging="true" 
                        AllowSorting="true" OnSorting="gvShipment_Sorting" >
                        <RowStyle BackColor="Gainsboro" />
                        <AlternatingRowStyle BackColor="white" />
                        <HeaderStyle  CssClass="buttonlabel" ForeColor="white"/>
                          <PagerStyle ForeColor="#636363" CssClass="copy10grey" HorizontalAlign="Left" />
                          <Columns>
                              <asp:TemplateField HeaderText="S.No." ItemStyle-CssClass="copy10grey"  ItemStyle-Width="2%">
                                <ItemTemplate>
                                      <%# Container.DataItemIndex + 1%>                  
                                </ItemTemplate>
                            </asp:TemplateField> 

                                <asp:TemplateField HeaderText="Label Generation Date" SortExpression="LabelGenerationDate" HeaderStyle-CssClass="buttonundlinelabel"  
                                    ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="13%">
                                    <ItemTemplate>
                                       <%# Eval("LabelGenerationDate")%> 
                                    </ItemTemplate>
                                </asp:TemplateField> 
                                <asp:TemplateField HeaderText="Ship Method" SortExpression="ShipMethod" HeaderStyle-CssClass="buttonundlinelabel"  ItemStyle-HorizontalAlign="Left" 
                                    ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <%# Eval("ShipMethod")%>
                                        </ItemTemplate>
                                </asp:TemplateField> 
                              <asp:TemplateField HeaderText="Package" SortExpression="ShipPackage" HeaderStyle-CssClass="buttonundlinelabel"   ItemStyle-HorizontalAlign="Left" 
                                  ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <%# Eval("ShipPackage")%>
                                        </ItemTemplate>
                                </asp:TemplateField>  
                              
                              <asp:TemplateField HeaderText="Tracking#" SortExpression="TrackingNumber" HeaderStyle-CssClass="buttonundlinelabel"  ItemStyle-HorizontalAlign="left" 
                                  ItemStyle-CssClass="copy10grey" ItemStyle-Width="15%">
                                    
                                    <ItemTemplate>
                                                
                                         <%# Eval("TrackingNumber")%>
                                    </ItemTemplate>
                                </asp:TemplateField> 
                              <asp:TemplateField HeaderText="Weight" SortExpression="ShipWeight" HeaderStyle-CssClass="buttonundlinelabel"   ItemStyle-HorizontalAlign="Right" 
                                  ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <%# Eval("ShipWeight")%>&nbsp;
                                        </ItemTemplate>
                                </asp:TemplateField>  
                              <asp:TemplateField HeaderText="Cost" SortExpression="FinalPostage" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" 
                                  ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                    
                                    <ItemTemplate>                                                
                                        $<%# Eval("FinalPostage","{0:n}")%>&nbsp;
                                    </ItemTemplate>
                                </asp:TemplateField> 

                              <asp:TemplateField HeaderText="LabelType" SortExpression="LabelType" HeaderStyle-CssClass="buttonundlinelabel"   ItemStyle-HorizontalAlign="Left" 
                                  ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <%# Eval("LabelType")%>
                                        </ItemTemplate>
                                </asp:TemplateField>  
                                <asp:TemplateField HeaderText="Assignment#" SortExpression="AssignmentNumber" HeaderStyle-CssClass="buttonundlinelabel"   
                                    ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        
                                       <%-- <%# Convert.ToString(Eval("LabelType")) == "RMA" ? "../RMA/RmaView.aspx":"../FulfillmentDetails.aspx"%>--%>
                                        
                                        <asp:LinkButton ID="lnkView" runat="server" CssClass="linkgrey" CommandArgument='<%# Eval("ID") +","+ Eval("LabelType") +","+Eval("AssignmentNumber") %>'  
                                            OnCommand="lnkView_Command" AlternateText='<%# "View " + Convert.ToString(Eval("LabelType")) %>' > 
                                            <%# Eval("AssignmentNumber")%>                                        
                                        </asp:LinkButton>


                                        </ItemTemplate>
                                </asp:TemplateField>  
                              
                                <asp:TemplateField HeaderText="Label Used" SortExpression="LabelUsedDate"  HeaderStyle-CssClass="buttonlabel"   ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                    <ItemTemplate>

                                         <%# Eval("LabelUsedDate")%>
                                        
                                        </ItemTemplate>
                                </asp:TemplateField>  
                              <asp:TemplateField Visible="false" HeaderText="" HeaderStyle-CssClass="buttonlabel"   ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                                    <ItemTemplate>
                             IsPrint
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
        <script type="text/javascript" src="../JSLibrary/jquery.blockUI.js"></script>
    </form>
</body>
</html>
