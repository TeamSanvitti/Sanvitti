<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RMAReports.aspx.cs" Inherits="avii.Reports.RMAReports" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>
<%@ Register TagPrefix="menu" TagName="Menu" Src="~/Controls/Header.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ESN Report</title>
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

</head>
<body  bgcolor="#ffffff" leftmargin="0" rightmargin="0" topmargin="0">
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
                <td>&nbsp;ESN Report</td></tr>
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
     <tr><td class="copy10grey" width="15%" align="right">ESN#:</td>
     <td width="30%">
            <asp:TextBox ID="txtESN" runat="server" Width="80%" CssClass="copy10grey"></asp:TextBox>
        </td>
        <td  width="1%">
            &nbsp;
        </td>
        <td class="copy10grey"  width="14%"  align="right">Module:</td>
        <td  width="40%">

            <asp:DropDownList ID="ddlModules" runat="server" Width="70%"  CssClass="copy10grey">
            <asp:ListItem Text="" Value=""></asp:ListItem>
            <asp:ListItem Text="ESN" Value="ESN"></asp:ListItem>
            <asp:ListItem Text="PO" Value="PO"></asp:ListItem>
            <asp:ListItem Text="RMA" Value="RMA"></asp:ListItem>
            </asp:DropDownList>
        
        </td>
        </tr>
           
            <tr>
                <td class="copy10grey"  align="right">
                    From Date:
                </td>
                <td>
                <asp:TextBox ID="txtFromDate" onkeypress="return ReadOnly(event);"  CssClass="copy10grey" runat="server" Width="80%" MaxLength="15" ></asp:TextBox>
                <img id="img1" alt=""  onclick="document.getElementById('<%=txtFromDate.ClientID%>').value = ''; displayCalendar(document.getElementById('<%=txtFromDate.ClientID%>'),'mm/dd/yyyy',this,true);" src="../fullfillment/calendar/sscalendar.jpg" />
                
                </td>
                <td  width="1%">
                    &nbsp;
                </td>
                <td class="copy10grey"  align="right">
                    End Date:
                </td>
                <td>
                <asp:TextBox ID="txtEndDate" onkeypress="return ReadOnly(event);"  CssClass="copy10grey" runat="server" Width="70%" MaxLength="12" ></asp:TextBox>
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
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td>
        
        
    <asp:GridView runat="server" ID="gvRMA" AutoGenerateColumns="False" 
     PageSize="50" AllowPaging="true" Width="100%" OnPageIndexChanging="gridView_PageIndexChanging"   
     CellPadding="3" 
    GridLines="Vertical" DataKeyNames="EsnLogGUID"  >
    <RowStyle  CssClass="copy10grey" BackColor="Gainsboro" />
    <AlternatingRowStyle BackColor="white" />
    <HeaderStyle  CssClass="button"   />
    <PagerStyle ForeColor="#636363" CssClass="copy10grey" />
    <%-- <FooterStyle CssClass="white"  />--%>
    <Columns>
    
        <asp:BoundField DataField="UpdateDate" HeaderText="Update Date" ItemStyle-Width="100" />
        
    <%-- status, mslnumber only one year of records      <asp:BoundField DataField="modelNumber" HeaderText="Model#" /> --%>

        <asp:BoundField DataField="ESN" HeaderText="ESN#" />
        <asp:BoundField DataField="MSL" HeaderText="MSL Number" />
        <asp:BoundField DataField="ESNStatus" HeaderText="ESN Status" />
        <asp:BoundField DataField="Module" HeaderText="Module" />
        
         
        <asp:TemplateField ItemStyle-Width="40">
            <ItemTemplate>
                <asp:ImageButton ToolTip="Delete"  OnClientClick="return confirm('Are you sure delete this report?')" CommandArgument='<%# Eval("ESNLogGUID") %>' ImageUrl="~/Images/delete.png" ID="imgDelete" OnCommand="imgDelete_Commnad" runat="server" />
            </ItemTemplate>
        </asp:TemplateField>
        
        </Columns>
        </asp:GridView>
   
        </td>
    </tr>
       
    </table>
    
         
     
     </div>
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
    <foot:MenuFooter ID="footer1" runat="server"></foot:MenuFooter>
    
    </form>
</body>
</html>
