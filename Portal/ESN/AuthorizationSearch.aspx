<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AuthorizationSearch.aspx.cs" Inherits="avii.ESN.AuthorizationSearch" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>
<%@ Register TagPrefix="menu" TagName="Menu" Src="~/Controls/Header.ascx" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title> Authorization Search</title>
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
 

     
    <script type="text/javascript">
        function Refresh() {
            //alert('refreshing..');
            var btnhdPrintlabel = document.getElementById("<%= btnhdDownload.ClientID %>");
            btnhdPrintlabel.click();
        }
        function RefreshESN() {
            //alert('refreshing..');
            var btnhdPrintlabel = document.getElementById("<%= btnhdESNDownload.ClientID %>");
            btnhdPrintlabel.click();
        }

        function RefreshAuthXML() {
            //alert('refreshing..');
            var btnhdPrintlabel = document.getElementById("<%= btnhdAuth.ClientID %>");
            btnhdPrintlabel.click();
        }
        function RefreshPOS() {
            //alert('refreshing..');
            var btnhdPrintlabel = document.getElementById("<%= btnhdPOS.ClientID %>");
             btnhdPrintlabel.click();
         }

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
                <td>&nbsp;Authorization Search</td></tr>
             </table>
                 <div style="display:none">
    <asp:Button ID="btnhdDownload" runat="server"   Text="" OnClick="btnhdDownload_Click"  /> 
    <asp:Button ID="btnhdESNDownload" runat="server"   Text="" OnClick="btnhdESNDownload_Click"  /> 
    <asp:Button ID="btnhdAuth" runat="server"   Text="" OnClick="btnhdAuth_Click"  /> 
    <asp:Button ID="btnhdPOS" runat="server"   Text="" OnClick="btnhdPOS_Click"  /> 
        
    </div>
<div id="divContainer">	
            <div id="divRequest"  style="display:none">
            <asp:UpdatePanel ID="upLabel" runat="server">
				<ContentTemplate>
                    
                    <asp:Label ID="lblESNData" runat="server" CssClass="copy10grey"></asp:Label>

               </ContentTemplate>
            </asp:UpdatePanel>
        </div>
             
		
    </div>
        
        <table cellSpacing="0" cellPadding="0" align="center" width="100%" border="0" id="maintbl">
        <tr>
			<td>
        <asp:UpdatePanel ID="UPl1"  UpdateMode="Conditional" runat="server"   >
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
             
                <tr valign="top" >
                <td class="copy10grey" align="right" width="15%">
                    Customer Name:
                </td>
                <td width="35%">
                    <asp:DropDownList ID="dpCompany" CssClass="copy10grey" runat="server" Width="80%" TabIndex="1"
                    OnSelectedIndexChanged="dpCompany_SelectedIndexChanged"  AutoPostBack="true">
                    </asp:DropDownList>
                
                </td>
                <td  width="1%">
                    &nbsp;
                </td>
                <td class="copy10grey" align="right" width="15%">
                   Kitted SKU:
                </td>
                <td width="35%">                  
                          
                    <asp:DropDownList ID="ddlSKU" CssClass="copy10grey" runat="server" Width="80%">
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
                     <td class="copy10grey" align="right" width="15%">
                         Run Number:
                     </td>
                     <td width="35%">
                        <asp:TextBox ID="txtRunNumber" runat="server" CssClass="copy10grey" MaxLength="6"  Width="80%"></asp:TextBox>
                    

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
                    <td colspan="5">
                        <hr />
                    </td>
                </tr>
                <tr>
                <td  align="center"  colspan="5">
                    <asp:Button ID="btnSearch" runat="server"  CssClass="button" Text="Search" OnClick="btnSearch_Click" OnClientClick="return ShowSendingProgress();" ></asp:Button>
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="button"  OnClick="btnCancel_Click" CausesValidation="false"/>
                      <div class="loadingcss" align="center" id="modalSending">
                        <img src="/Images/ajax-loaders.gif" alt=""  /> Loading...
                    </div> 
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

                                <%--OnRowDataBound="gvAuth_RowDataBound"--%>
                            <asp:GridView ID="gvAuth" AutoGenerateColumns="false" OnSorting="gvAuth_Sorting" AllowPaging="true" 
                                OnPageIndexChanging="gvAuth_PageIndexChanging" PageSize="20"
                            Width="100%" ShowHeader="true"  ShowFooter="false" runat="server" GridLines="Both"  AllowSorting="true"
                            >                        
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
                                  <asp:TemplateField HeaderText="SKU#" SortExpression="SKU" HeaderStyle-CssClass="buttonundlinelabel"   ItemStyle-HorizontalAlign="Left" 
                                        ItemStyle-CssClass="copy10grey" ItemStyle-Width="20%">
                                        <ItemTemplate>
                                            <%# Eval("SKU")%>
                                            </ItemTemplate>
                                    </asp:TemplateField>  
                                  <asp:TemplateField HeaderText="Kitted SKU" SortExpression="KittedSKU" HeaderStyle-CssClass="buttonundlinelabel"     ItemStyle-HorizontalAlign="Left" 
                                        ItemStyle-CssClass="copy10grey" ItemStyle-Width="20%">
                                        <ItemTemplate>
                                            <%# Eval("KittedSKU")%>
                                            </ItemTemplate>
                                    </asp:TemplateField>  
                                      
                                    <asp:TemplateField HeaderText="Run Number" SortExpression="RunNumber" HeaderStyle-CssClass="buttonundlinelabel"  ItemStyle-HorizontalAlign="Left" 
                                        ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <%# Eval("RunNumber")%>                                    
                                        </ItemTemplate>
                                    </asp:TemplateField> 
                                    <asp:TemplateField HeaderText="Esn Count" SortExpression="EsnCount" HeaderStyle-CssClass="buttonundlinelabel"  ItemStyle-HorizontalAlign="Left" 
                                        ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <%# Eval("EsnCount")%>                                    
                                        </ItemTemplate>
                                    </asp:TemplateField> 
                                    <asp:TemplateField HeaderText="Create Date" SortExpression="CreateDate" HeaderStyle-CssClass="buttonundlinelabel"  ItemStyle-HorizontalAlign="Left" 
                                        ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <%# Eval("CreateDate")%>
                                            </ItemTemplate>
                                    </asp:TemplateField>  
                                    <asp:TemplateField HeaderText="Download" HeaderStyle-CssClass="buttongrid"  ItemStyle-HorizontalAlign="Left"
                                      ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                                        <ItemTemplate>                                     
                                            
<%--                                            <asp:LinkButton ToolTip="See more..." CausesValidation="false" OnCommand="lnkESNDATA_Command" CommandArgument='<%# Eval("ESNAuthorizationID") %>'  
                                             ID="lnkESNDATA"  runat="server"  ><strong>ESNData<strong>
                                            </asp:LinkButton>--%>

                                              <asp:LinkButton CssClass="button" ToolTip="ESNData" CausesValidation="false" OnCommand="ESNData_Command" 
                                                CommandArgument='<%# Eval("ESNAuthorizationID") %>'  
                                             ID="lnkESNData"  runat="server"  >ESN Data
                                            </asp:LinkButton>
                                          

                                            <%--<asp:ImageButton ID="imgView" runat="server" AlternateText="View Authorization" ToolTip="View Authorization" CommandArgument='<%# Eval("ESNAuthorizationID")%>' 
                                                OnCommand="imgView_Command" ImageUrl="~/images/view.png"  />--%>
                                        </ItemTemplate>
                                    </asp:TemplateField>   
                                    <asp:TemplateField HeaderText="Download" HeaderStyle-CssClass="buttongrid"  ItemStyle-HorizontalAlign="center"
                                      ItemStyle-CssClass="copy10grey" ItemStyle-Width="6%">
                                        <ItemTemplate>                                     
                                            
                                            <asp:LinkButton CssClass="button" ToolTip="Download file" CausesValidation="false" OnCommand="lnkDownload_Command" 
                                                CommandArgument='<%# Eval("ESNAuthorizationID") %>'  
                                             ID="lnkDownload"  runat="server"  >Authorization
                                            </asp:LinkButton>
                                            
                                           
                                        </ItemTemplate>
                                    </asp:TemplateField>   
                                    <asp:TemplateField HeaderText="Download" HeaderStyle-CssClass="buttongrid"  ItemStyle-HorizontalAlign="center"
                                      ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                                        <ItemTemplate>                                     
                                            
                                            
                                            <asp:LinkButton CssClass="button" ToolTip="Download ESN only" CausesValidation="false" OnCommand="lnkESNDownload_Command" 
                                                CommandArgument='<%# Eval("ESNAuthorizationID") %>'  
                                             ID="lnkESN"  runat="server"  > &nbsp; TXT  &nbsp; 
                                            </asp:LinkButton>
                                          
                                        </ItemTemplate>
                                    </asp:TemplateField>   
                                    <asp:TemplateField HeaderText="Download" HeaderStyle-CssClass="buttongrid"  ItemStyle-HorizontalAlign="center"
                                      ItemStyle-CssClass="copy10grey" ItemStyle-Width="7%">
                                        <ItemTemplate>                                     
                                            
                                            
                                            <asp:LinkButton CssClass="button" ToolTip="POS LABEL" CausesValidation="false" OnCommand="lnkPOSLabel_Command" 
                                                CommandArgument='<%# Eval("ESNAuthorizationID") %>'  OnClientClick="return ShowSendingProgress();" 
                                             ID="lnkPOSLabel"  runat="server"  > &nbsp; POS LABEL  &nbsp; 
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
              </td>
            </tr>
            </table>
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
