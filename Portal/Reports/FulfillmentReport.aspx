<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FulfillmentReport.aspx.cs" Inherits="avii.Reports.FulfillmentReport" ValidateRequest="false" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>
<%@ Register TagPrefix="menu" TagName="Menu" Src="~/Controls/Header.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Fullfilment Report</title>
     <link href="../aerostyle.css" type="text/css" rel="stylesheet"/>
   	 <link rel="stylesheet"  type="text/css" href="../fullfillment/calendar/dhtmlgoodies_calendar.css" media="screen"></link>
		<script type="text/javascript" src="../fullfillment/calendar/dhtmlgoodies_calendar.js"></script>  
        

        <script type="text/javascript" language="javascript">
            function ReadOnly(eventRef) {

                var keyStroke = (eventRef.which) ? eventRef.which : (window.event) ? window.event.keyCode : 0;
                keyStroke = 0;
                eventRef.keyCode = 0;
                return false;
            }
            function Validation() {



                var customerObj = document.getElementById("<%=ddlCompany.ClientID %>");
                    //alert(customerObj.selectedIndex);
                    if (customerObj.selectedIndex == 0) {
                        alert('Company required!');
                        return false;
                    }
                 
            }
    </script>
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
                <td>&nbsp;Fullfilment Report</td></tr>
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
     <div id="winVP" style="position: relative; z-index: 1;">
     
           
      <table bordercolor="#839abf" border="1" width="100%" align="center" cellpadding="0" cellspacing="0">
      <tr>
        <td>
         <asp:Panel  ID="pnlSearch" runat="server"  DefaultButton="btnSearch" >
     <table width="100%" border="0" class="box" align="center" cellpadding="2" cellspacing="2">
     <tr style="height:1px">
     <td style="height:1px"></td>
     </tr>
     <tr><td class="copy10grey" width="15%" align="right">Fulfillment#:</td>
     <td width="30%">
            <asp:TextBox ID="txtPONo" runat="server" Width="80%" CssClass="copy10grey"></asp:TextBox>
        </td>
        <td  width="10%">
            &nbsp;
        </td>
        <td class="copy10grey"  width="10%" align="right">SKU:</td>
        <td  width="35%">
        <asp:TextBox ID="txtSKU" runat="server" Width="75%" CssClass="copy10grey"></asp:TextBox>
        
        </td>
        </tr>
        
           
            <tr>
                <td class="copy10grey" align="right">
                    From Date:
                </td>
                <td>
                <asp:TextBox ID="txtFromDate" CssClass="copy10grey" runat="server" Width="80%" MaxLength="15" ></asp:TextBox>
                <img id="img1" alt=""  onclick="document.getElementById('<%=txtFromDate.ClientID%>').value = ''; displayCalendar(document.getElementById('<%=txtFromDate.ClientID%>'),'mm/dd/yyyy',this,true);" src="../fullfillment/calendar/sscalendar.jpg" />
                
                </td>
                <td  width="10%">
                    &nbsp;
                </td>
                <td class="copy10grey" align="right">
                    End Date:
                </td>
                <td>
                <asp:TextBox ID="txtEndDate" CssClass="copy10grey" runat="server" Width="75%" MaxLength="12" ></asp:TextBox>
                <img id="img2" alt=""  onclick="document.getElementById('<%=txtEndDate.ClientID%>').value = ''; displayCalendar(document.getElementById('<%=txtEndDate.ClientID%>'),'mm/dd/yyyy',this,true);" src="../fullfillment/calendar/sscalendar.jpg" />
                
                </td>   
                </tr>
                
                <tr>
                    <td class="copy10grey" align="right">
                     Company Name:
                    </td>
                    <td class="copy10grey">
                    <asp:DropDownList ID="ddlCompany"  runat="server" Width="80%" 
                        CssClass="copy10grey">
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
                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="button" OnClientClick="return Validation();" OnClick="btnSearch_Click" CausesValidation="false"/>
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
    GridLines="Vertical" DataKeyNames="PO_ID"  >
    <RowStyle  CssClass="copy10grey" BackColor="Gainsboro" />
    <AlternatingRowStyle BackColor="white" />
    <HeaderStyle  CssClass="button"   />
     <PagerStyle ForeColor="#636363" CssClass="copy10grey" />
    <%-- <FooterStyle CssClass="white"  />--%>
    <Columns>
    
        <asp:BoundField DataField="Po_Num" ItemStyle-Width="150"  HeaderStyle-HorizontalAlign="Left" HeaderText="Fulfillment#" />
        <asp:BoundField DataField="Po_Date" HeaderText="Fulfillment Date" />
        
    <%--     <asp:BoundField DataField="modelNumber" HeaderText="Model#" /> --%>
        <%--<asp:BoundField DataField="CompanyName" HeaderText="Company Name" />--%>
        <asp:BoundField DataField="username" HeaderText="Created By" />
        <%--<asp:BoundField DataField="CreatedDate" ItemStyle-Width="40" HeaderText="Created Date" />
        
        <asp:BoundField DataField="ModifiedBy" HeaderText="Modified By" />
        <asp:BoundField DataField="ModifiedDate" ItemStyle-Width="40" HeaderText="Modified Date" />
        --%>
         
        <asp:TemplateField ItemStyle-Width="40">
            <ItemTemplate>
                 <%-- Enabled='<%# Convert.ToInt32(Eval("PoStatusID"))==1 ? true: false %>'--%>   
                <asp:ImageButton ToolTip="View Report"  CausesValidation="false" CommandArgument='<%# Eval("fulfillmentLogID") %>' ImageUrl="~/Images/view.png" ID="imgEditOrder" OnCommand="imgEditOrder_Commnad" runat="server" />
                
                <asp:ImageButton ToolTip="Delete"  OnClientClick="return confirm('Are you sure delete this report?')" CommandArgument='<%# Eval("fulfillmentLogID") %>' ImageUrl="~/Images/delete.png" ID="imgDelete" OnCommand="imgDelete_Commnad" runat="server" />
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
    </tr><tr>
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
               <foot:MenuFooter ID="footer1" runat="server"></foot:MenuFooter>
    
    
    </form>
</body>
</html>
