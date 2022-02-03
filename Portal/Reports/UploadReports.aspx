<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UploadReports.aspx.cs" Inherits="avii.Reports.UploadReports" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>
<%@ Register TagPrefix="menu" TagName="Menu" Src="~/Controls/Header.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Upload Data Report</title>
     <link href="../aerostyle.css" type="text/css" rel="stylesheet"/>
   	 <link rel="stylesheet"  type="text/css" href="../fullfillment/calendar/dhtmlgoodies_calendar.css" media="screen"></link>
		<script type="text/javascript" src="../fullfillment/calendar/dhtmlgoodies_calendar.js"></script>  
        
        <script type="text/javascript" src="../JQuery/jquery-latest.js"></script>
        <script type="text/javascript" >

            $(document).ready(function () {
                $('#txtFromDate').focusin(function (event) {
                    $('#img1').click();
                    event.preventDefault();

                });
                $('#txtFromDate').keypress(function (event) {
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
            $(document).AjaxReady(function () {
                $('#txtFromDate').focusin(function (event) {
                    $('#img1').click();
                    event.preventDefault();

                });
                $('#txtFromDate').keypress(function (event) {
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
     </table>
    
    
     <table cellspacing="0" cellpadding="0" border="0" align="center" width="95%">
        
        <tr valign="top">
           
            <td   ><br />
    <table align="center" style="text-align:left" width="100%">
                <tr class="button" align="left">
                <td>&nbsp;Upload Data Report</td></tr>
             </table>
     
     
     
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
     <tr><td class="copy10grey">
                - Please select your search
                criterial to narrow down the search and record selection.<br />
                - Atleast one search criteria should be selected.
                
                </td></tr>
     </table>
     <div id="winVP" style="position: relative; z-index: 1;">
     
           
      <table bordercolor="#839abf" border="1" width="100%" align="center" cellpadding="0" cellspacing="0">
      <tr>
        <td>
         <asp:Panel  ID="pnlSearch" runat="server"  DefaultButton="btnSearch" >
            <table width="100%" border="0" class="box" align="center" cellpadding="2" cellspacing="2">
     <tr style="height:6px">
     <td style="height:6px">
     
     </td>
     </tr>
  
        
            
            <tr>
                <td class="copy10grey" align="right" width="15%">
                    From Date:
                </td>
                <td width="35%">
                <asp:TextBox ID="txtFromDate" CssClass="copy10grey" runat="server" Width="80%" MaxLength="15" ></asp:TextBox>
                <img id="img1" alt=""  onclick="document.getElementById('<%=txtFromDate.ClientID%>').value = ''; displayCalendar(document.getElementById('<%=txtFromDate.ClientID%>'),'mm/dd/yyyy',this,true);" src="../fullfillment/calendar/sscalendar.jpg" />
                
                </td>
                <td  width="1%">
                    &nbsp;
                </td>
                <td class="copy10grey" align="right" width="15%">
                    End Date:
                </td>
                <td width="35%"> 
                <asp:TextBox ID="txtEndDate"   CssClass="copy10grey" runat="server" Width="80%" MaxLength="12" ></asp:TextBox>
                <img id="img2" alt=""  onclick="document.getElementById('<%=txtEndDate.ClientID%>').value = ''; displayCalendar(document.getElementById('<%=txtEndDate.ClientID%>'),'mm/dd/yyyy',this,true);" src="../fullfillment/calendar/sscalendar.jpg" />
                
                </td>   
                </tr>
                
            <tr>
                <td class="copy10grey" align="right" width="15%">
                    Module Name:
                </td>
                <td width="35%">
                    <asp:DropDownList ID="ddlModules" CssClass="copy10grey" Width="81%" runat="server">
                    <asp:ListItem Text="" Value=""></asp:ListItem>
                    <asp:ListItem Text="Fulfillment" Value="Fulfillment"></asp:ListItem>
                    <asp:ListItem Text="Provisioning" Value="Provisioning"></asp:ListItem>
                    <asp:ListItem Text="ESN" Value="ESN"></asp:ListItem>
                    <asp:ListItem Text="ASN" Value="ASN"></asp:ListItem>
                    <asp:ListItem Text="RMA" Value="RMA"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td  width="1%">
                    &nbsp;
                </td>
                <td class="copy10grey" align="right" width="15%">
                    Status:
                </td>
                <td width="35%"> 
                 <asp:DropDownList ID="ddlStatus" CssClass="copy10grey" Width="81%" runat="server">
                    <asp:ListItem Text="" Value="0"></asp:ListItem>
                    <asp:ListItem Text="Pending" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Received" Value="2"></asp:ListItem>
                    <asp:ListItem Text="Shipped" Value="3"></asp:ListItem>
                    
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
        
        
        <asp:GridView runat="server" ID="gvOrders" AutoGenerateColumns="False" 
     PageSize="50" AllowPaging="true" Width="100%"  OnPageIndexChanging="gridView_PageIndexChanging"   
     CellPadding="3" 
    GridLines="Vertical" DataKeyNames="UploadID"  >
    <RowStyle  CssClass="copy10grey" BackColor="Gainsboro" />
    <AlternatingRowStyle BackColor="white" />
    <HeaderStyle  CssClass="button"   />
     <PagerStyle ForeColor="#636363" CssClass="copy10grey" />
    <%-- <FooterStyle CssClass="white"  />--%>
    <Columns>
    
        <asp:BoundField DataField="UploadDate"   HeaderStyle-HorizontalAlign="Left" HeaderText="Upload Date" />
        <asp:BoundField DataField="modulename" HeaderText="Module Name" />
        <asp:BoundField DataField="Status" HeaderText="Status" />

         
        <asp:TemplateField ItemStyle-Width="40">
            <ItemTemplate>
                 <%-- Enabled='<%# Convert.ToInt32(Eval("PoStatusID"))==1 ? true: false %>'--%>   
                <asp:ImageButton ToolTip="View Report"  CausesValidation="false" CommandArgument='<%# Eval("UploadID") %>' ImageUrl="~/Images/view.png" ID="imgEditOrder" OnCommand="imgEditOrder_Commnad" runat="server" />
                
                <asp:ImageButton ToolTip="Delete"  OnClientClick="return confirm('Are you sure delete this report?')" CommandArgument='<%# Eval("UploadID") %>' ImageUrl="~/Images/delete.png" ID="imgDelete" OnCommand="imgDelete_Commnad" runat="server" />
            </ItemTemplate>
        </asp:TemplateField>
        
        </Columns>
        </asp:GridView>
   
        </td>
    </tr>
       
    </table>
    
        <table>
        <tr>
            <td>
        <ajaxToolkit:ModalPopupExtender BackgroundCssClass="modalBackground" 
        CancelControlID="btnClose" runat="server" PopupControlID="pnlModelPopup" 
        ID="ModalPopupExtender1" TargetControlID="lnk"
         />
        <asp:LinkButton ID="lnk" runat="server" ></asp:LinkButton>
        <asp:Panel  ID="pnlModelPopup" runat="server" CssClass="modalPopup"   >
      
      
        <div style="overflow:auto; height:400px; width:100%; border: 0px solid #839abf" >
      
        <table align="center" border="0"  width="80%">
        <tr>
        <td>
        
        
            <table align="left" border="0" width="800" >
      <tr>
        <td align="left" class="button">
       <strong> View Fulfillment Report </strong>
        </td>
      
        <td align="center" width="40">
            <asp:Button ID="btnClose" CssClass="button" Height="28" runat="server" Text="Close" CausesValidation="false"  />
        
         
        </td>
      </tr>
      </table>
        </td>
        </tr>
        <tr>
        <td align="left">
        
        
      <table align="left" border="0" width="80%"> 
      <tr>
      <td align="left" >
          <asp:Label ID="lblPoXML" Width="80%"  runat="server" CssClass="copy10grey" ></asp:Label>
      </td>
      </tr>
      </table>
      </td>
      </tr>
        </div>
        </asp:Panel>
        </td>
        </tr>
        </table>
     
     </div>


    
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
     
    </td>
        </tr>
        <tr>
        <td>
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
