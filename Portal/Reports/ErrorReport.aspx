<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ErrorReport.aspx.cs" Inherits="avii.Reports.ErrorReport" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>
<%@ Register TagPrefix="menu" TagName="Menu" Src="~/Controls/Header.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
<title>.:: Lan Global ::.</title>
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
                <td>&nbsp;Error Log Report</td></tr>
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
     
     
           
      <table bordercolor="#839abf" border="1" width="100%" align="center" cellpadding="0" cellspacing="0">
      <tr>
        <td>
         <asp:Panel  ID="pnlSearch" runat="server"  DefaultButton="btnSearch" >
            <table width="100%" border="0" class="box" align="center" cellpadding="3" cellspacing="3">
             <tr style="height:1px">
             <td style="height:1px"></td>
             </tr>
             <tr>
                <td class="copy10grey" align="right">
                    From Date:
                </td>
                <td>
                <asp:TextBox ID="txtFromDate" CssClass="copy10grey" runat="server" Width="80%" MaxLength="15" ></asp:TextBox>
                <img id="img1" alt=""  onclick="document.getElementById('<%=txtFromDate.ClientID%>').value = ''; displayCalendar(document.getElementById('<%=txtFromDate.ClientID%>'),'mm/dd/yyyy',this,true);" src="../fullfillment/calendar/sscalendar.jpg" />
                
                </td>
                <td  width="2%">
                    &nbsp;
                </td>
                <td class="copy10grey" align="right">
                    End Date:
                </td>
                <td>
                <asp:TextBox ID="txtEndDate"   CssClass="copy10grey" runat="server" Width="75%" MaxLength="12" ></asp:TextBox>
                <img id="img2" alt=""  onclick="document.getElementById('<%=txtEndDate.ClientID%>').value = ''; displayCalendar(document.getElementById('<%=txtEndDate.ClientID%>'),'mm/dd/yyyy',this,true);" src="../fullfillment/calendar/sscalendar.jpg" />
                
                </td> 
                
                </tr>
                
                
                
                <tr>
                    <td class="copy10grey" align="right">
                       Search Text:
                    </td>
                    <td>
                        <asp:TextBox ID="txtSearch"   CssClass="copy10grey" runat="server" Width="75%"  ></asp:TextBox>
                    </td>  
                    <td  width="2%">
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
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
     <br />
    <table align="center" width="100%" border="0" cellpadding="0" cellspacing="0">
    <tr>
        <td align="right">
        <asp:Label ID="lblCount" runat="server" CssClass="copy10grey" ></asp:Label>   
        &nbsp;</td>
    </tr>
    <tr>
        <td>
            <asp:GridView ID="gvError" OnPageIndexChanging="gridView_PageIndexChanging"    AutoGenerateColumns="false"  
                DataKeyNames="ErrorLogID"  Width="100%"  
            ShowFooter="false" runat="server" GridLines="Both" 
            PageSize="25" AllowPaging="true" 
            BorderStyle="Outset" 
            AllowSorting="false" > 
            <RowStyle BackColor="Gainsboro" />
            <AlternatingRowStyle BackColor="white" />
            <HeaderStyle  CssClass="button" ForeColor="white"/>
            <FooterStyle CssClass="white"  />
            <PagerStyle ForeColor="#636363" CssClass="copy10grey" HorizontalAlign="Left" />
            <Columns>
                         
                
                <asp:TemplateField HeaderText="User Name" SortExpression="Username" ItemStyle-VerticalAlign="Top" HeaderStyle-HorizontalAlign="Left"  ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                    <ItemTemplate>
                        <%# Eval("Username")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Error Date"  SortExpression="ErrorDate" ItemStyle-VerticalAlign="Top" ItemStyle-CssClass="copy10grey"  ItemStyle-Width="10%">
                    <ItemTemplate>
                    
                    <%# Eval("ErrorDate")%>
                    <%--<%# DataBinder.Eval(Container.DataItem, "OrderDate", "{0:d}")%>--%>
                    
                    </ItemTemplate>
                </asp:TemplateField>

              

                <asp:TemplateField HeaderText="Error Message" SortExpression="ErrorMessage" ItemStyle-VerticalAlign="Top" ItemStyle-CssClass="copy10grey" ItemStyle-Width="70%">
                    <ItemTemplate><%# Eval("ErrorMessage")%></ItemTemplate>
                    
                </asp:TemplateField>
                
                <asp:TemplateField Visible="false" HeaderText="Source" SortExpression="Source" ItemStyle-VerticalAlign="Top" ItemStyle-CssClass="copy10grey"  ItemStyle-Width="7%">
                    <ItemTemplate><%# Eval("Source")%></ItemTemplate>
                </asp:TemplateField>
               
                

                <asp:TemplateField Visible="false" HeaderText="Url" SortExpression="Url" ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="25%">
                    <ItemTemplate><%#Eval("Url")%></ItemTemplate>
                </asp:TemplateField> 
                
                
                	    
			   <%--<asp:templatefield headertext=""  itemstyle-horizontalalign="center"  >
                    <itemtemplate>
                        <asp:imagebutton id="imgdelpo" runat="server"  commandname="delete" alternatetext="delete po" imageurl="~/images/delete.png" />
                    </itemtemplate>
                </asp:templatefield>--%>
                
                		       
			    		    
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

