<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginReports.aspx.cs" Inherits="avii.Reports.LoginReports" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>
<%@ Register TagPrefix="menu" TagName="Menu" Src="~/Controls/Header.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Portal Access</title>
     <link href="../aerostyle.css" type="text/css" rel="stylesheet"/>
   	 <link rel="stylesheet"  type="text/css" href="../fullfillment/calendar/dhtmlgoodies_calendar.css" media="screen"></link>
		<script type="text/javascript" src="../fullfillment/calendar/dhtmlgoodies_calendar.js"></script>  
        <script type="text/javascript" >
            function ReadOnly(eventRef) {

                var keyStroke = (eventRef.which) ? eventRef.which : (window.event) ? window.event.keyCode : 0;
                keyStroke = 0;
                eventRef.keyCode = 0;
                return false;
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
                <td>&nbsp;Portal Access</td></tr>
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
     
        <tr><td class="copy10grey" align="right"  width="10%">User Name:</td><td  width="35%">
            <asp:TextBox ID="txtUserName" runat="server" Width="80%" CssClass="copy10grey"></asp:TextBox>
        
            </td>
            <td  width="10%">
            &nbsp;
        </td>
            <td class="copy10grey" align="right" width="10%">
                Company Name:</td>
            <td  width="35%">
                <asp:DropDownList ID="ddlCompany"  runat="server" Width="80%" 
                CssClass="copy10grey">
            </asp:DropDownList>
            </td>
            </tr>
           
            <tr>
                <td class="copy10grey" align="right" width="10%">
                    From Date:
                </td>
                <td  width="35%">
                <asp:TextBox ID="txtFromDate"    CssClass="copy10grey" runat="server" Width="80%" MaxLength="15" ></asp:TextBox>
                <img id="img1" alt=""  onclick="document.getElementById('<%=txtFromDate.ClientID%>').value = ''; displayCalendar(document.getElementById('<%=txtFromDate.ClientID%>'),'mm/dd/yyyy',this,true);" src="../fullfillment/calendar/sscalendar.jpg" />
                
                </td>
                <td  width="10%">
                    &nbsp;
                </td>
                <td class="copy10grey" align="right">
                    End Date:
                </td>
                <td>
                <asp:TextBox ID="txtEndDate"   CssClass="copy10grey" runat="server" Width="80%" MaxLength="12" ></asp:TextBox>
                <img id="img2" alt=""  onclick="document.getElementById('<%=txtEndDate.ClientID%>').value = ''; displayCalendar(document.getElementById('<%=txtEndDate.ClientID%>'),'mm/dd/yyyy',this,true);" src="../fullfillment/calendar/sscalendar.jpg" />
                
                </td>   
                </tr>
               
                <tr>
                <td colspan="5">
                <hr />
                </td>
                </tr>
                <tr>
                <td  align="center"  colspan="5">
                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="button" OnClick="btnSearch_Click" CausesValidation="false"/>
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
        <td align="right">    
                <asp:Label ID="lblCount" CssClass="copy10grey" runat="server" ></asp:Label> &nbsp;&nbsp;
            
            
           </td>
    </tr>
    <tr>
        <td>
        
        
    <asp:GridView runat="server" ID="gvLoginReport" AutoGenerateColumns="False" 
     PageSize="50" AllowPaging="true" Width="100%"  OnPageIndexChanging="gridView_PageIndexChanging"   
     CellPadding="3"  AllowSorting="true" OnSorting="gvLoginReport_Sorting" 
    GridLines="Both" DataKeyNames="SignInID"  >
    <RowStyle  CssClass="copy10grey" BackColor="Gainsboro" />
    <AlternatingRowStyle BackColor="white" />
    <HeaderStyle  CssClass="buttonlabel" ForeColor="white"/>
     <PagerStyle ForeColor="#636363" CssClass="copy10grey" />
    <FooterStyle CssClass="white"  />
    <Columns>
    
        
        <asp:TemplateField HeaderText="S.No." ItemStyle-Width="1%" HeaderStyle-CssClass="buttonlabel">
            <ItemTemplate>
                <%# Container.DataItemIndex+1 %>
            </ItemTemplate>
        </asp:TemplateField>
<%--        <asp:TemplateField HeaderText="User Name" HeaderStyle-CssClass="buttonundlinelabel" SortExpression="UserName"  ItemStyle-Width="20%"  HeaderStyle-HorizontalAlign="Left">
            <ItemTemplate>
                <%# Eval("UserName") %>
            </ItemTemplate>
        </asp:TemplateField>--%>

        <asp:BoundField DataField="UserName" HeaderStyle-CssClass="buttonundlinelabel" SortExpression="UserName"  ItemStyle-Width="25%"  HeaderStyle-HorizontalAlign="Left" HeaderText="User Name" />
        <asp:BoundField DataField="CompanyName" SortExpression="CompanyName" HeaderText="Company Name" HeaderStyle-CssClass="buttonundlinelabel"/>
        <asp:BoundField DataField="SessionStartDate" SortExpression="SessionStartDate"  HeaderText="Login Date"  HeaderStyle-CssClass="buttonundlinelabel"/>
        <asp:BoundField DataField="SessionEndDate" SortExpression="SessionEndDate" HeaderText="Logout Date"  HeaderStyle-CssClass="buttonundlinelabel"/>
        
         
        <asp:TemplateField ItemStyle-Width="40" HeaderStyle-CssClass="buttonlabel" Visible="false">
            <ItemTemplate>
                <asp:ImageButton ToolTip="Delete"  OnClientClick="return confirm('Are you sure delete this report?')" CommandArgument='<%# Eval("SignInID") %>' ImageUrl="~/Images/delete.png" ID="imgDelete" OnCommand="imgDelete_Commnad" runat="server" />
            </ItemTemplate>
        </asp:TemplateField>
        
        </Columns>
        </asp:GridView>
   
        </td>
    </tr>
       
    </table>
    
         
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
    <foot:MenuFooter ID="footer1" runat="server"></foot:MenuFooter>
    
    </form>
</body>
</html>
