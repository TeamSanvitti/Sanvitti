<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UnprovisionPoSearch.aspx.cs" Inherits="avii.UnprovisionPoSearch" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>
<%@ Register TagPrefix="menu" TagName="Menu" Src="~/Controls/Header.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Unprovisioning Request Search</title>
    <!-- fix for 1.1em default font size in Jquery UI -->
	<style type="text/css">
		.ui-widget{font-size:11px !important;}
		.ui-state-error-text{margin-left: 10px}
	</style>
    <script type="text/javascript">
        function OpenNewPage(url) {
            window.open(url);
        }

        <%--function Refresh() {
            //alert('refreshing..');
            var btnhdPrintlabel = document.getElementById("<%= btnhdDownload.ClientID %>");
            btnhdPrintlabel.click();
        }
        function RefreshESN() {
            //alert('refreshing..');
            var btnhdPrintlabel = document.getElementById("<%= btnhdESNDownload.ClientID %>");
            btnhdPrintlabel.click();
        }--%>

        

       $(document).ready(function () {
       
           $("#divRequest").dialog({
               autoOpen: false,
               modal: false,
               minHeight: 20,
               height: 550,
               width: 1150,
               resizable: false,
               open: function (event, ui) {
                   $(this).parent().appendTo("#divContainer");
               }
           });

           


       });


       function closeRequestDialog() {
           //Could cause an infinite loop because of "on close handling"
           $("#divRequest").dialog('close');
       }



       function openRequestDialog(title, linkID) {

           var pos = $("#" + linkID).position();
           var top = pos.top;
           var left = pos.left + $("#" + linkID).width() + 10;
           //alert(top);
           if (top > 600)
               top = 10;
           else
               top = 10;
           //top = top - 600;
           left = 130;
           $("#divRequest").dialog("option", "title", title);
           $("#divRequest").dialog("option", "position", [left, top]);

           $("#divRequest").dialog('open');

           unblockRequestDialog();
       }


        function openRequestDialogAndBlock(title, linkID) {
            //alert(title);
           openRequestDialog(title, linkID);

           //block it to clean out the data
           $("#divRequest").block({
               message: '<img src="../images/async.gif" />',
               css: { border: '0px' },
               fadeIn: 0,
               //fadeOut: 0,
               overlayCSS: { backgroundColor: '#ffffff', opacity: 1 }
           });
       }

        function unblockRequestDialog() {
           // alert('inlock');
           $("#divRequest").unblock();
       }


      
       

       function set_focus1() {
           var img = document.getElementById("img1");
           var st = document.getElementById("ddlModule");
           st.focus();
           img.click();
       }
       function set_focus2() {
           var img = document.getElementById("img2");
           var st = document.getElementById("ddlModule");
           st.focus();
           img.click();
       }

    </script>
	
   
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
         </table>

        <table cellspacing="0" cellpadding="0" border="0" align="center" width="95%">
        <tr valign="top">
            <td   >
                <table align="center" style="text-align:left" width="100%">
                <tr class="buttonlabel" align="left">
                <td>&nbsp;Unprovisioning Request Search</td></tr>
             </table>
          <asp:UpdatePanel ID="UPl1"  UpdateMode="Conditional"  runat="server"   >
         <ContentTemplate>
         <table  align="center" style="text-align:left" width="100%">
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
             <table width="100%" border="0" class="box" align="center" cellpadding="5" cellspacing="5">
             
                <tr valign="top" runat="server" id="trCustomer" >
                <td class="copy10grey" align="right" width="15%">
                    Customer Name:
                </td>
                <td width="35%">
                    <asp:DropDownList ID="dpCompany" CssClass="copy10grey" runat="server" Width="80%" TabIndex="1"
                      AutoPostBack="false">
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
                <tr>
                     <td class="copy10grey" align="right" width="15%">
                         Fulfillment#:
                     </td>
                     <td width="35%">
                        <asp:TextBox ID="txtPO" runat="server" CssClass="copy10grey"   Width="80%"></asp:TextBox>
                    

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
          
                <tr>                
                    <td colspan="5">
                        <hr />
                    </td>
                </tr>
                <tr>
                <td  align="center"  colspan="5">
                    <asp:Button ID="btnSearch" runat="server"  CssClass="button" Text="Search" OnClick="btnSearch_Click" ></asp:Button>
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
                <td  align="center"  >
                        <table width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td align="left">
                                
                            </td>
                            <td align="right">
                                <asp:Label ID="lblCount" CssClass="copy10grey" runat="server" ></asp:Label>

                                <%--<asp:Button ID="btnDownload"  Visible="false" CssClass="button" OnClick="btnDownload_Click"  runat="server" Text="Download"></asp:Button>--%>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                            <asp:GridView ID="gvUnprovision" AutoGenerateColumns="false"   OnRowDataBound="gvUnprovision_RowDataBound"
                            Width="100%" ShowHeader="true"  ShowFooter="false" runat="server" GridLines="Both" >                        
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
                                  <asp:TemplateField HeaderText="Fulfillment#" HeaderStyle-CssClass="buttongrid"   ItemStyle-HorizontalAlign="Left" 
                                        ItemStyle-CssClass="copy10grey" ItemStyle-Width="15%">
                                        <ItemTemplate>
                                            <%# Eval("FulfillmentNumber")%>
                                            </ItemTemplate>
                                    </asp:TemplateField>  
                                  <asp:TemplateField HeaderText="Create Date" HeaderStyle-CssClass="buttongrid"   ItemStyle-HorizontalAlign="Left" 
                                        ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <%# Eval("CreateDate")%>
                                            </ItemTemplate>
                                    </asp:TemplateField>  
                                      
                                    <asp:TemplateField HeaderText="Requested By" SortExpression="RequestedBy" HeaderStyle-CssClass="buttongrid"  ItemStyle-HorizontalAlign="Left" 
                                        ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <%# Eval("RequestedBy")%>                                    
                                        </ItemTemplate>
                                    </asp:TemplateField> 
                                    <asp:TemplateField HeaderText="Approved By" SortExpression="ApprovedBy" HeaderStyle-CssClass="buttongrid"  ItemStyle-HorizontalAlign="Left" 
                                        ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <%# Eval("ApprovedBy")%>                                    
                                        </ItemTemplate>
                                    </asp:TemplateField> 
                                    <asp:TemplateField HeaderText="Comment"  HeaderStyle-CssClass="buttongrid"  ItemStyle-HorizontalAlign="Left" 
                                        ItemStyle-CssClass="copy10grey" ItemStyle-Width="30%">
                                        <ItemTemplate>
                                            <%# Eval("CustomerComment")%>
                                            <asp:HiddenField ID="hdStatus" Value='<%# Eval("Status") %>' runat="server" />
                                            </ItemTemplate>
                                    </asp:TemplateField>  
                                    <asp:TemplateField HeaderText="Total Qty" HeaderStyle-CssClass="buttongrid"  ItemStyle-HorizontalAlign="Left" 
                                        ItemStyle-CssClass="copy10grey" ItemStyle-Width="6%">
                                        <ItemTemplate>
                                            <%# Eval("TotalQty")%>                                    
                                        </ItemTemplate>
                                    </asp:TemplateField> 
                                    <asp:TemplateField HeaderText="Status" HeaderStyle-CssClass="buttongrid"  ItemStyle-HorizontalAlign="Left" 
                                        ItemStyle-CssClass="copy10grey" ItemStyle-Width="6%">
                                        <ItemTemplate>
                                            <%# Eval("Status")%>                                    
                                        </ItemTemplate>
                                    </asp:TemplateField> 
                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass="buttongrid"  ItemStyle-HorizontalAlign="center"
                                      ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <table>
                                                <tr valign="top">
                                                    <td>
                                                        <asp:ImageButton CssClass="button" ToolTip="View PO" CausesValidation="false" Height="18" OnCommand="imgView_Command" 
                                                CommandArgument='<%# Eval("POID") %>'  
                                             ID="imgView"  runat="server" ImageUrl="~/images/view.png"  >
                                            </asp:ImageButton>      
                                            &nbsp;
                                                    </td>
                                                    <td>
                                                               <asp:ImageButton ID="imgPOA"  ToolTip="View Unprovioned ESN" OnCommand="imgPOA_Command" Height="18" CausesValidation="false" 
                                            CommandArgument='<%# Eval("POID") %>' ImageUrl="~/Images/view2.png"  runat="server" />
 
                                                    </td>
                                                </tr>
                                            </table>
                                            
                                            
                                        </ItemTemplate>
                                    </asp:TemplateField>   
                                    
                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass="buttongrid"  ItemStyle-HorizontalAlign="center"
                                      ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
                                        <ItemTemplate>
                                            <asp:LinkButton CssClass="button" Visible='<%# Eval("IsVisible") %>' ToolTip="Reject" CausesValidation="false" Height="18" OnCommand="lnkReject_Command" 
                                                CommandArgument='<%# Eval("UnprovisioningID") %>'  
                                             ID="lnkReject"  runat="server"  >Reject
                                            </asp:LinkButton>                                           
                                            <asp:LinkButton CssClass="button" Visible="false" ToolTip="Cancel" CausesValidation="false" Height="18" OnCommand="lnkCancel_Command" 
                                                CommandArgument='<%# Eval("UnprovisioningID") %>'  
                                             ID="lnkCancel"  runat="server"  >Cancel
                                            </asp:LinkButton>                                           
                                           
                                        </ItemTemplate>
                                    </asp:TemplateField>   
                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass="buttongrid"  ItemStyle-HorizontalAlign="center"
                                      ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
                                        <ItemTemplate>                                     
                                                                                        
                                            <asp:LinkButton CssClass="button" Visible='<%# Eval("IsVisible") %>' ToolTip="Approved" CausesValidation="false" Height="18" OnCommand="lnkApproved_Command" 
                                                CommandArgument='<%# Eval("UnprovisioningID") %>'  
                                             ID="lnkApproved"  runat="server"  >Approved 
                                            </asp:LinkButton>
                                          
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
             
           </asp:UpdatePanel>
                
            <asp:UpdatePanel ID="upnlJsRunner" UpdateMode="Always" runat="server">
			<ContentTemplate>
				<asp:PlaceHolder ID="phrJsRunner" runat="server"></asp:PlaceHolder>
			</ContentTemplate>
		</asp:UpdatePanel>

            <asp:UpdateProgress ID="UpdateProgress1" runat="server"
            DynamicLayout="false">
            <ProgressTemplate>
                <img src="/Images/ajax-loaders.gif" />Loading ...
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
