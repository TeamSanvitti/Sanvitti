<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmErrorLog.aspx.cs" Inherits="avii.frmErrorLog" %>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="/Controls/Header.ascx" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="/Controls/Footer.ascx" %>


<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Lan Global Inc. - ASN Error Log</title>
	<link href="./aerostyle.css" rel="stylesheet" type="text/css"/>
	<script language="javascript" type="text/javascript" src="./avI.js"></script> 
    <link rel="stylesheet"  type="text/css" href="./fullfillment/calendar/dhtmlgoodies_calendar.css" media="screen"></link>
	<script type="text/javascript" src="./fullfillment/calendar/dhtmlgoodies_calendar.js"></script>  
		  
</head>
<body bgColor="#ffffff" leftmargin="0" rightmargin="0" topmargin="0">
    <form id="form1" runat="server">
    <div align="center" width="95%">
	<table cellSpacing="0" cellPadding="0"  border="0" align="center" width="95%">
		<tr>
			<td>
			<head:menuheader id="MenuHeader" runat="server"></head:menuheader>
			</td>
		</tr>
     </table>
    
    <br />
    <table  cellSpacing="1" cellPadding="1" width="95%">
        <tr>
		    <td colSpan="6" bgcolor="#dee7f6" class="button">&nbsp;&nbsp;Error Log
		    </td>
        </tr>
        <tr><td><asp:Label ID="lblMsg" runat="server" CssClass="errormessage"></asp:Label></td></tr>
    </table>
    <br />
        <table  cellSpacing="1" cellPadding="1" width="95%">
                <tr><td class="copy10grey">
                - Click on "Clear Log" button to clear the log table/list.<br />
                - Click on "Get Error Log" button to get all complete log, its advicable to enter atleast one search criteria.</td></tr>
    </table>
       <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="95%" >
           <tr bordercolor="#839abf">
                <td>
        <table cellSpacing="1" cellPadding="1" width="95%" border="0" >
                <tr>
                <td align="right" class="copy10grey" width="15%">
                    Purchase Order#:</td>
                <td>
                </td>
                <td>
                    <asp:TextBox ID="txtPONum" onkeypress="JavaScript:return alphaOnly(event);" MaxLength="20" runat="server" CssClass="copy10grey" ></asp:TextBox></td>              
                <td align="right" class="copy10grey">Return Error Code:</td>
                <td>
                &nbsp;<asp:TextBox ID="txtCode" runat="server" CssClass="copy10grey" MaxLength="8"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" class="copy10grey">
                    Log Date From:</td>
                <td>
                </td>
                <td>
                    &nbsp;<asp:TextBox ID="txtFromDate" runat="server" CssClass="copy10grey" MaxLength="15"></asp:TextBox>
                    <img id="imgFromtDate" alt="" onclick="document.getElementById('<%=txtFromDate.ClientID%>').value = ''; displayCalendar(document.getElementById('<%=txtFromDate.ClientID%>'),'mm/dd/yyyy',this,true);" src="../fullfillment/calendar/sscalendar.jpg" />
                </td>
                <td align="right" class="copy10grey">Log Date To:</td>
                
                <td>
                    &nbsp;<asp:TextBox ID="txtToDate" runat="server" CssClass="copy10grey" MaxLength="15"></asp:TextBox>
                    <img id="imgToDate"  alt="" onclick="document.getElementById('<%=txtToDate.ClientID%>').value = ''; displayCalendar(document.getElementById('<%=txtToDate.ClientID%>'),'mm/dd/yyyy',this,true);" src="../fullfillment/calendar/sscalendar.jpg" />
                
                </td>               
            </tr>
            </table>
            </td>
            </tr>
            </table>
    <table  cellSpacing="1" cellPadding="1" width="100%">
            <tr>
                <td colspan="6" align="Right">
                    <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Get Error Log" OnClick="btnSearch_Click" />&nbsp;
                    <asp:Button ID="btnCancel" runat="server" CssClass="button" Text="Cancel Transaction" OnClick="btnCancel_Click"/>&nbsp;
                    <asp:Button ID="btnDownload" runat="server" CssClass="button" Text="Download Error Log" Visible="false" OnClick="btnDownload_Click"/>&nbsp;
                    <asp:Button ID="btnClearLog" Visible="false" runat="server" CssClass="button"  Text="Clear Error Log List/Table" OnClick="btnClearLog_Click"/>&nbsp;&nbsp;&nbsp;
                </td>
            </tr>
            <tr><td>&nbsp;</td></tr>

            <tr>
                <td>
                    <asp:GridView ID="gvErrorLog" runat="server" Width="100%" GridLines="Both" AllowPaging="false"  AutoGenerateColumns="false">
                        <RowStyle BackColor="Gainsboro" />
                        <AlternatingRowStyle BackColor="white" />
                        <HeaderStyle  CssClass="button" ForeColor="white"/>
                        <FooterStyle CssClass="white"  />
                        <Columns>
                            <asp:TemplateField HeaderText="Module Name" SortExpression="ModuleName"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                <ItemTemplate><%#Eval("ModuleName")%></ItemTemplate>
                            </asp:TemplateField> 
                            <asp:TemplateField HeaderText="Log Date" SortExpression="LogDate"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                <ItemTemplate><%# DataBinder.Eval(Container.DataItem, "LogDate", "{0:d}")%></ItemTemplate>
                            </asp:TemplateField> 
                            <asp:TemplateField HeaderText="Purchase Order#" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                <ItemTemplate><%#Eval("PurhcaseOrdernumber")%></ItemTemplate>
                            </asp:TemplateField> 
                            <asp:TemplateField HeaderText="Description" SortExpression="Description"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="70%">
                                <ItemTemplate><%#Eval("Description")%></ItemTemplate>
                            </asp:TemplateField> 
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>

    </table>
    </div>
    </div>
    </form>
</body>
</html>
