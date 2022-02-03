<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BlockEsnSearch.aspx.cs" Inherits="avii.BlockESN.BlockEsnSearch" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>
<%@ Register TagPrefix="menu" TagName="Menu" Src="~/Controls/Header.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Quarantine Search</title>
    
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
	
<!-- fix for 1.1em default font size in Jquery UI -->
	<style type="text/css">
		.ui-widget{font-size:11px !important;}
		.ui-state-error-text{margin-left: 10px}
	</style>
   <script type="text/javascript">

       $(document).ready(function () {

           $("#divESN").dialog({
               autoOpen: false,
               modal: false,
               minHeight: 20,
               height: 550,
               width: 450,
               resizable: false,
               open: function (event, ui) {
                   $(this).parent().appendTo("#divContainer");
               }
           });

          


       });


       function closeDialog() {
           //Could cause an infinite loop because of "on close handling"
           $("#divESN").dialog('close');
       }



       function openDialog(title, linkID) {

           var pos = $("#" + linkID).position();
           var top = pos.top;
           var left = pos.left + $("#" + linkID).width() + 10;
           //alert(top);
           if (top > 600)
               top = 10;
           else
               top = 10;
           //top = top - 600;
           left = 510;
           $("#divESN").dialog("option", "title", title);
           $("#divESN").dialog("option", "position", [left, top]);

           $("#divESN").dialog('open');

           unblockDialog();
       }


       function openDialogAndBlock(title, linkID) {
           openDialog(title, linkID);

           //block it to clean out the data
           $("#divESN").block({
               message: '<img src="../images/async.gif" />',
               css: { border: '0px' },
               fadeIn: 0,
               //fadeOut: 0,
               overlayCSS: { backgroundColor: '#ffffff', opacity: 1 }
           });
       }

       function unblockDialog() {
           $("#divESN").unblock();
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
    <script type="text/javascript">
        function GoToSummary() {
            var url = "../SOSummary.aspx";
            window.open(url);
            return false;

        }
            function isNumberKey(evt) {
                
                var charCodes = evt.keyCode ? evt.keyCode : evt.which ? evt.which : evt.charCode;
                if (charCodes > 31 && (charCodes < 48 || charCodes > 57)) {
                    charCodes = 0;
                    return false;
                }
                return true;
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
            <td   >
                <table align="center" style="text-align:left" width="100%">
                <tr class="buttonlabel" align="left">
                <td>&nbsp;Quarantine Search</td></tr>
             </table>


                 <div id="divContainer">	
            <div id="divESN"  style="display:none">
            <asp:UpdatePanel ID="upLabel" runat="server">
				<ContentTemplate>
                    
                    <asp:Label ID="lblMsg2" runat="server" CssClass="errormessage"></asp:Label>
                    <table bordercolor="#839abf" border="1" width="100%" align="center" cellpadding="0" cellspacing="0">
                      <tr>
                        <td>
                            <table width="100%" border="0" class="box" align="center" cellpadding="5" cellspacing="5">
                            <tr>
                                <td class="copy10grey">
                                    SKU#: 
                                </td>
                                <td>
                                    
                                    <asp:Label ID="lblSKU" runat="server" CssClass="copy10grey"></asp:Label>
                                </td>
                            </tr>
                            </table>
                        </td>
                    </tr>
                    </table>
                    <br />
                    <table align="center" style="text-align:left" width="100%">
                    <tr>
                      <td align="center">

                      <asp:Repeater ID="rptESN" runat="server"  >
                        <HeaderTemplate>
                        <table bordercolor="#839abf" border="1" width="100%" cellpadding="1" cellspacing="1">
                        <tr>
                            <td class="buttongrid"  width="1%" >
                                S.No.
                            </td>
                            <td class="buttongrid"  width="50%">
                                ESN
                            </td>
                        
                        </tr>
                        </HeaderTemplate>
                        <ItemTemplate>    
                            <tr valign="bottom"  class="<%# Container.ItemIndex % 2 == 0 ? "alternaterow" : "" %>">
                            <td class="copy10grey"  >
                            <%# Container.ItemIndex +  1 %>
                            </td>
                            <td  class="copy10grey"  >
                                <%# Eval("ESN")%>    
                                
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
                    

               </ContentTemplate>
            </asp:UpdatePanel>
        </div>
             
		
    </div>
      
         <asp:UpdatePanel ID="UPl1"  UpdateMode="Conditional" ChildrenAsTriggers="true" runat="server"   >
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
             <tr style="height:1px">
             <td style="height:1px"></td>
             </tr>
                <tr valign="top" id="trCustomer" runat="server">
                <td class="copy10grey" align="right" width="15%">
                    Customer Name:
                </td>
                <td width="35%">
                    <asp:DropDownList ID="dpCompany" CssClass="copy10grey" runat="server" Width="80%"
                                    OnSelectedIndexChanged="dpCompany_SelectedIndexChanged"  
                                    AutoPostBack="true">
									</asp:DropDownList>
                
                </td>
                <td  width="1%">
                    &nbsp;
                </td>
                <td class="copy10grey" align="right" width="15%">
                   <%--<asp:Label ID="lblStatus" runat="server" CssClass="copy10grey"></asp:Label>--%>
                </td>
                <td width="35%">
                   
                          
<%--                    <asp:DropDownList ID="ddlStatus" CssClass="copy10grey" runat="server" Width="80%">
	                </asp:DropDownList>  --%>            
        
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
                         SKU#:
                    </td>
                    <td width="35%">
                    
                    <asp:DropDownList ID="ddlSKU" CssClass="copy10grey" runat="server" Width="80%">
	                </asp:DropDownList>           
                
                    </td>
                    <td  width="1%">
                        &nbsp;
                    </td>
                    <td class="copy10grey" align="right" width="15%">
                     ESN:
                    </td>
                    <td width="35%">                                                    
                        <asp:TextBox ID="txtESN" runat="server" onkeypress="return isNumberKey(event);"  CssClass="copy10grey"   Width="80%"></asp:TextBox>
                       
                    </td>   
                
                    
                    </tr>
                    
                    <tr valign="top">
                        <td class="copy10grey" align="right" width="15%">
                            Status:
                        </td>
                        <td width="35%">
                             <asp:DropDownList ID="ddlStatus" CssClass="copy10grey" runat="server" Width="80%">
                                  <asp:ListItem Text="" Value=""></asp:ListItem>
                                 <asp:ListItem Text="Pending" Value="Pending"></asp:ListItem>
                                 <asp:ListItem Text="Approved" Value="Approved"></asp:ListItem>
                                 <asp:ListItem Text="Rejected" Value="Rejected"></asp:ListItem>
	                         </asp:DropDownList>           
                
                    
                        </td>
                        <td  width="1%">
                        &nbsp;
                    </td>
                    <td class="copy10grey" align="right" width="15%">
                     Action:
                    </td>
                    <td width="35%">                                                    
                        <asp:DropDownList ID="ddlAction" CssClass="copy10grey" runat="server" Width="80%">
                            <asp:ListItem Text="" Value=""></asp:ListItem>
                                 <asp:ListItem Text="Pending" Value="Pending"></asp:ListItem>
                                 <asp:ListItem Text="Approved" Value="Approved"></asp:ListItem>
                                 <asp:ListItem Text="Rejected" Value="Rejected"></asp:ListItem>
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
                    <asp:Button ID="btnSearch" runat="server"  CssClass="button" Text="Search" OnClick="btnSearch_Click"
                                                ></asp:Button>
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
                    <%--<asp:Panel ID="pnlPO" runat="server">--%>
                        <%--<PO:Status ID="pos1" runat="server" />--%>
                        <table width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td align="left">
                                <asp:Label ID="lblCount" CssClass="copy10grey" runat="server" ></asp:Label>
                            </td>
                            <td align="right">
                                <asp:Button ID="btnDownload"  Visible="false" CssClass="button" OnClick="btnDownload_Click"  runat="server" Text="Download"></asp:Button>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                            <asp:GridView ID="gvBlockEsn" AutoGenerateColumns="false"  
                            Width="100%" ShowHeader="true"  ShowFooter="false" runat="server" GridLines="Both" 
                            OnPageIndexChanging="gvBlockEsn_PageIndexChanging" PageSize="20" AllowPaging="true" 
                            AllowSorting="true" OnSorting="gvBlockEsn_Sorting" OnRowDataBound="gvBlockEsn_RowDataBound">                        
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
                                    <asp:TemplateField HeaderText="Category Name" SortExpression="CategoryName" HeaderStyle-CssClass="buttonundlinelabel"  ItemStyle-HorizontalAlign="Left" 
                                        ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <%# Eval("CategoryName")%>                                    
                                        </ItemTemplate>
                                    </asp:TemplateField> 
                                    <asp:TemplateField HeaderText="Product Name" SortExpression="ProductName" HeaderStyle-CssClass="buttonundlinelabel"   ItemStyle-HorizontalAlign="Left" 
                                        ItemStyle-CssClass="copy10grey" ItemStyle-Width="15%">
                                        <ItemTemplate>
                                            <%# Eval("ProductName")%>
                                            </ItemTemplate>
                                    </asp:TemplateField>  
                                    <asp:TemplateField HeaderText="SKU" SortExpression="SKU" HeaderStyle-CssClass="buttonundlinelabel"  ItemStyle-HorizontalAlign="Left" 
                                        ItemStyle-CssClass="copy10grey" ItemStyle-Width="15%">
                                        <ItemTemplate>
                                            <%# Eval("SKU")%>
                                            </ItemTemplate>
                                    </asp:TemplateField>  
                                    <asp:TemplateField HeaderText="Create Date" SortExpression="CreateDate" HeaderStyle-CssClass="buttonundlinelabel"  ItemStyle-HorizontalAlign="Left"
                                      ItemStyle-CssClass="copy10grey" ItemStyle-Width="7%">
                                        <ItemTemplate>                                                
                                             <%# Eval("CreateDate")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>   
                                    <asp:TemplateField HeaderText="Action" SortExpression="Action" HeaderStyle-CssClass="buttonundlinelabel"  ItemStyle-HorizontalAlign="Left" 
                                      ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                                        <ItemTemplate>                                                
                                             <%# Eval("Action")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>   
                                    <asp:TemplateField HeaderText="Status" SortExpression="Status" HeaderStyle-CssClass="buttonundlinelabel"  ItemStyle-HorizontalAlign="Left" 
                                      ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                                        <ItemTemplate>                                                
                                             <%# Eval("Status")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>   
                                    <asp:TemplateField HeaderText="Created By" SortExpression="CreateBy" HeaderStyle-CssClass="buttonundlinelabel"  ItemStyle-HorizontalAlign="Left" 
                                      ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                                        <ItemTemplate>                                                
                                             <%# Eval("CreateBy")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>   
                                    <asp:TemplateField HeaderText="Received By" SortExpression="ReceiveBy" HeaderStyle-CssClass="buttonundlinelabel"  ItemStyle-HorizontalAlign="Left" 
                                      ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                                        <ItemTemplate>                                                
                                             <%# Eval("ReceiveBy")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>   
                                    <asp:TemplateField HeaderText="Approved By" SortExpression="ApprovedBy" HeaderStyle-CssClass="buttonundlinelabel"  ItemStyle-HorizontalAlign="Left"
                                      ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                                        <ItemTemplate>                                                
                                             <%# Eval("ApprovedBy")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText=""  HeaderStyle-CssClass="gridlabel"  ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="copy10grey" 
                                          ItemStyle-Width="2%">
                                        <ItemTemplate>                                                
                                        
                                            <asp:ImageButton ID="imgView" runat="server" AlternateText="View ESN" ToolTip="View ESN" CommandArgument='<%# Eval("BlockID")%>' OnCommand="imgView_Command" ImageUrl="~/images/view.png"  />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText=""  HeaderStyle-CssClass="gridlabel"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="8%">
                                        <ItemTemplate>                                                
                                        
                                            &nbsp; <asp:LinkButton ID="lnkReject" Visible='<%# Convert.ToString(Eval("Status")).ToLower() == "pending" ? true : false %>' CommandArgument='<%# Eval("BlockID")%>' Text="Reject" runat="server" CssClass="button" OnCommand="lnkReject_Command"></asp:LinkButton>
                                            &nbsp; <asp:LinkButton ID="lnkApprove" Visible='<%# Convert.ToString(Eval("Status")).ToLower() == "pending" ? true : false %>'  CommandArgument='<%# Eval("BlockID")%>' Text="Approved" runat="server" CssClass="button" OnCommand="lnkApprove_Command"></asp:LinkButton>
                                            
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
    
       <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.4.0/jquery.min.js"></script>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/jquery-ui.min.js"></script>
        <script type="text/javascript" src="../JQuery/jquery.blockUI.js"></script>
        <script type="text/javascript" src="/JSLibrary/jquery.blockUI.js"></script>
	
    </form>
</body>
</html>
