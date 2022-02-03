<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmailReports.aspx.cs" Inherits="avii.Reports.EmailReports" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>
<%@ Register TagPrefix="menu" TagName="Menu" Src="~/Controls/Header.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Email Report</title>
     <link href="../aerostyle.css" type="text/css" rel="stylesheet"/>
   	 <link rel="stylesheet"  type="text/css" href="../fullfillment/calendar/dhtmlgoodies_calendar.css" media="screen"></link>
		<script type="text/javascript" src="../fullfillment/calendar/dhtmlgoodies_calendar.js"></script> 
        <script type="text/javascript" src="../JQuery/jquery-latest.js"></script>
        <script type="text/javascript" >
            $(document).AjaxReady(function () {
                $("#[id$=txtFromDate]").focusin(function (event) {

                    $('#img1').click();
                    event.preventDefault();

                });
                $("#[id$=txtFromDate]").keypress(function (event) {
                    $('#img1').click();
                    event.preventDefault();

                });
                $('#txtEndDate').focusin(function (event) {
                    $('#img2').click();
                    event.preventDefault();

                });
                $('#txtEndDate').keypress(function (event) {
                    $('#img2').click();
                    event.preventDefault();

                });
                

            });
            $(document).ready(function () {
                $("#[id$=txtFromDate]").focusin(function (event) {

                    $('#img1').click();
                    event.preventDefault();

                });
                $("#[id$=txtFromDate]").keypress(function (event) {
                    $('#img1').click();
                    event.preventDefault();

                });
                $('#txtEndDate').focusin(function (event) {
                    $('#img2').click();
                    event.preventDefault();

                });
                $('#txtEndDate').keypress(function (event) {
                    $('#img2').click();
                    event.preventDefault();

                });
               

            });
        </script>
        </head>
<body bgcolor="#ffffff" leftmargin="0" rightmargin="0" topmargin="0">
    <form id="form1" runat="server">
    <table cellspacing="0" cellpadding="0" border="0" align="center" width="100%">
    <tr>
        <td>
            <menu:Menu ID="menu1" runat="server" ></menu:Menu>
        </td>
     </tr>
     </table><br />
     <table cellspacing="0" cellpadding="0" border="0" align="center" width="95%">
        
        <tr valign="top">
           
            <td   >
    <table align="center" style="text-align:left" width="100%">
                <tr class="button" align="left">
                <td>&nbsp;Email Report</td></tr>
             </table><br />
     
     
     
      <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
      <asp:UpdatePanel ID="UpdatePanel1"  runat="server" >
     <ContentTemplate>
     <table  align="center" style="text-align:left" width="99%">
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
     <table width="100%" border="0" class="box" align="center" cellpadding="2" cellspacing="2">
         <tr style="height:1px">
         <td style="height:1px"></td>
         </tr>
     
           
            <tr>
                <td class="copy10grey" align="right" width="15%">
                    From Date:
                </td>
                <td width="35%">
                <asp:TextBox ID="txtFromDate" CssClass="copy10grey" runat="server" Width="80%" MaxLength="12" ></asp:TextBox>
                <img id="img1" alt=""  onclick="document.getElementById('<%=txtFromDate.ClientID%>').value = ''; displayCalendar(document.getElementById('<%=txtFromDate.ClientID%>'),'mm/dd/yyyy',this,true);" src="../fullfillment/calendar/sscalendar.jpg" />
                
                </td>
                <td  width="1%">
                    &nbsp;
                </td>
                <td class="copy10grey" align="right" width="10%">
                    End Date:
                </td>
                <td width="44%">
                <asp:TextBox ID="txtEndDate" CssClass="copy10grey" runat="server" Width="75%" MaxLength="12" ></asp:TextBox>
                <img id="img2" alt=""  onclick="document.getElementById('<%=txtEndDate.ClientID%>').value = ''; displayCalendar(document.getElementById('<%=txtEndDate.ClientID%>'),'mm/dd/yyyy',this,true);" src="../fullfillment/calendar/sscalendar.jpg" />
                
                </td>   
                </tr>
                
                <tr>
                    <td class="copy10grey" align="right">
                     Module Name:
                    </td>
                    <td class="copy10grey">
                    <asp:DropDownList ID="ddlModule"  runat="server" Width="80%" 
                        CssClass="copy10grey">
                        <asp:ListItem Text="" Value=""></asp:ListItem> 
                        <asp:ListItem Text="Forgot password" Value="Forgot password"></asp:ListItem> 
                        <asp:ListItem Text="Fulfillment" Value="Fulfillment"></asp:ListItem> 
                        <asp:ListItem Text="RMA" Value="RMA"></asp:ListItem> 
                    </asp:DropDownList>
                
                    </td>
                    <td  width="10%">
                        &nbsp;
                    </td>
                    <td class="copy10grey">
                    
                    </td>
                    <td class="copy10grey">
                    
                    </td>
                </tr>
                <tr>
                <td colspan="5">
                <hr />
                </td>
                </tr>
                <tr>
                <td  align="center"  colspan="5">
                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="button"  OnClick="btnSearch_Click" CausesValidation="false"/>
                     &nbsp;
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="button" OnClick="btnCancel_Click" CausesValidation="false"/>
            
        
        </td>
        </tr>
            </table>
            </asp:Panel>
   
     </td>
     </tr>
     </table>       
     <table align="center" width="100%" border="0" cellpadding="0" cellspacing="0">
    <tr>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td>
        
        
    <asp:GridView runat="server" ID="gvEmails" AutoGenerateColumns="False" 
     PageSize="50" AllowPaging="true" Width="100%"
     CellPadding="3"  OnPageIndexChanging="gridView_PageIndexChanging"
     GridLines="Vertical" DataKeyNames="EmailLogID"  >
    <RowStyle  CssClass="copy10grey" BackColor="Gainsboro" />
    <AlternatingRowStyle BackColor="white" />
    <HeaderStyle  CssClass="button"   />
     <PagerStyle ForeColor="#636363" CssClass="copy10grey" />
    <Columns>
        <asp:BoundField DataField="ModuleName" HeaderStyle-HorizontalAlign="Left" HeaderText="Module Name" ItemStyle-Width="116" />
        
        <asp:BoundField DataField="EmailSentDate" HeaderStyle-HorizontalAlign="Left" HeaderText="EmailSent Date" ItemStyle-Width="116" />
    <%--     <asp:BoundField DataField="modelNumber" HeaderText="Model#" /> --%>
        <asp:BoundField DataField="username" HeaderText="Sent By" />
        <asp:BoundField DataField="emailTo" HeaderStyle-HorizontalAlign="Left" HeaderText="Sent To"  />
        <asp:BoundField DataField="emailCC" HeaderStyle-HorizontalAlign="Left" HeaderText="Sent CC"  />
    <%--    <asp:BoundField DataField="CompanyName" HeaderText="Company Name" /> --%>
        
        
        <asp:TemplateField ItemStyle-Width="70">
            <ItemTemplate>
                <asp:ImageButton ToolTip="Resend email" OnClientClick="return confirm('Do you want to resend email?')"  CausesValidation="false" CommandArgument='<%# Eval("EmailLogID") %>' ImageUrl="~/Images/mail.png" ID="ImageButton1" OnCommand="imgResendEmail_Commnad" runat="server" />
                
                 <%-- Enabled='<%# Convert.ToInt32(Eval("PoStatusID"))==1 ? true: false %>'--%>   
                <asp:ImageButton ToolTip="View Report"  CausesValidation="false" CommandArgument='<%# Eval("EmailLogID") %>' ImageUrl="~/Images/view.png" ID="imgEditOrder" OnCommand="imgEditOrder_Commnad" runat="server" />
                
                <asp:ImageButton ToolTip="Delete"  OnClientClick="return confirm('Are you sure delete this report?')" CommandArgument='<%# Eval("EmailLogID") %>' ImageUrl="~/Images/delete.png" ID="imgDelete" OnCommand="imgDelete_Commnad" runat="server" />
            </ItemTemplate>
        </asp:TemplateField>
        
        </Columns>
        </asp:GridView>
   
        </td>
    </tr>
       
    </table>
    <ajaxToolkit:ModalPopupExtender BackgroundCssClass="modalBackground" 
        CancelControlID="btnClose" runat="server" PopupControlID="pnlModelPoupp" 
        ID="ModalPopupExtender1" TargetControlID="lnk"
         />
        <asp:LinkButton ID="lnk" runat="server" ></asp:LinkButton>
        <asp:Panel  ID="pnlModelPoupp" runat="server" CssClass="modalPopup" >
       <div style="overflow:auto; height:400px;border: 0px solid #969696" >
      
      <table align="left" border="0"  width="100%">
      <tr>
        <td align="left" class="button">
       <strong> View Email Report </strong>
        </td>
      
        <td align="center" width="40">
        <asp:Button ID="btnClose" CssClass="button" Height="28" runat="server" Text="Close" CausesValidation="false"  />
         
        </td>
      </tr>
      <tr>
        <td>
        &nbsp;
        </td>


      </tr>
      <tr>
      <td >
          <asp:Label ID="lblPoXML" runat="server" CssClass="copy11" ></asp:Label>
      </td>
      </tr>
      </table>
      
      
      </div>
      </asp:Panel>    
       <script type='text/javascript'>


           prm = Sys.WebForms.PageRequestManager.getInstance();
           prm.add_endRequest(EndRequest);
           function EndRequest(sender, args) {
               //alert("EndRequest");
               $(document).AjaxReady();
           }
        </script>
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
        <br />

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
