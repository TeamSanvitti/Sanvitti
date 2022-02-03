<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmForecastSummary.aspx.cs" Inherits="avii.frmForecastSummary" %>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="/Controls/Header.ascx" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="/Controls/Footer.ascx" %>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Lan Global Inc. - Complete Wireless</title>
		<link href="./aerostyle.css" rel="stylesheet" type="text/css"/>
		<script type="text/javascript" language="javascript" src="./avI.js"></script> 
		 <link rel="stylesheet"  type="text/css" href="../fullfillment/calendar/dhtmlgoodies_calendar.css" media="screen"></link>
		<script type="text/javascript" src="../fullfillment/calendar/dhtmlgoodies_calendar.js"></script>  	
</head>

<body bgColor="#ffffff" leftmargin="0" rightmargin="0" topmargin="0">
    <form id="form1" runat="server">
        <TABLE cellSpacing="0" cellPadding="0"  border="0" align="center" width="100%">
        <TR>
	        <TD><head:menuheader id="MenuHeader" runat="server"></head:menuheader>				    
	        </TD>
        </TR>
        <tr><td>&nbsp;</td></tr>
        <tr>
            <td>
                <table  cellSpacing="1" cellPadding="1" width="100%">
                    <tr>
	                    <td colSpan="6" bgcolor="#dee7f6" class="button">&nbsp;&nbsp;Forecast Summary
	                    </td>
                    </tr>

                    <tr><td><asp:Label ID="lblMsg" runat="server" CssClass="errormessage"></asp:Label></td></tr>
                </table>
                <table  cellSpacing="1" cellPadding="1" width="100%">
                    <tr><td class="copy10grey">
                    - Please select your search
                    criterial to narrow down the search and records selection.<br />
                    - Atleast one search criteria should be selected.</td></tr>
                </table>            
                <table bordercolor="gray" cellspacing="0" cellpadding="0" width="100%" align="center" border="1">
			        <tr bordercolor="white">
				        <td>
	                        <table cellspacing="0" cellpadding="0" width="100%" border="0" align="center">
	                        <tr><td colspan="4">&nbsp;</td></tr>	                   
	                        <tr>
	                            <td class="copy10grey" align="right">Customer:&nbsp;&nbsp;</td>
	                            <td align="left" colspan="3">
	                                <asp:DropDownList ID="ddlCustomer" runat="server" CssClass="txfield1"  ></asp:DropDownList>
	                            </td>
	                        </tr>
	                        <tr>
	                            <td class="copy10grey" align="right">Forecast Date From:&nbsp;&nbsp;</td>
	                            <td align="left">
	                                <asp:TextBox runat="server" MaxLength="10" ID="txtDateFrom" CssClass="txfield1" />
	                                <img id="imgFromtDate" alt="" onclick="document.getElementById('<%=txtDateFrom.ClientID%>').value = ''; displayCalendar(document.getElementById('<%=txtDateFrom.ClientID%>'),'mm/dd/yyyy',this,true);" src="../fullfillment/calendar/sscalendar.jpg" />
                                </td>
	                            <td class="copy10grey" align="right">Forecast Date To:&nbsp;&nbsp;</td>
	                            <td align="left">
                                <asp:TextBox runat="server" MaxLength="10" ID="txtDateTo" CssClass="txfield1"/>
	                            <img id="img1" alt="" onclick="document.getElementById('<%=txtDateTo.ClientID%>').value = ''; displayCalendar(document.getElementById('<%=txtDateTo.ClientID%>'),'mm/dd/yyyy',this,true);" src="../fullfillment/calendar/sscalendar.jpg" />
                                
                            </td>
	                    </tr>
	                        <tr>
                        	<td class="copy10grey" align="right">Brand:&nbsp;&nbsp;</td>
                            <td align="left">
                                <asp:DropDownList ID="ddlMaker"  CssClass="txfield1" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td class="copy10grey" align="right">SKU:&nbsp;&nbsp;</td>
                            <td align="left">
                            <asp:TextBox MaxLength="30" runat="server" ID="txtSKU" CssClass="txfield1"/>
                            </td>
	                    </tr>
	                        <tr>
	                        <td colspan="4">
	                            <center>
                                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="button" OnClick="btnSearch_Click" /> &nbsp; 
                                <asp:Button ID="btnSearchCancel" runat="server" Text="Cancel" CssClass="button" OnClick="btnSearchCancel_Click" />
                                </center>
	                        </td>
	                    </tr>  
	                    <tr><td>&nbsp;</td></tr>
	                    </table>
	                </td>
	              </tr>
	              <tr>
                    <td>
                        <table width="100%" border="2" cellpadding="1" cellspacing="1">   
                        <asp:DataList ID="dlForecast" runat="server" Width="100%">
                            <HeaderTemplate>
                            <tr>
                                <td class="button">Customer</td>
                                <td class="button">UPC</td>
                                <td class="button">SKU#</td>
                                <td class="button">Item Name</td>
                                <td class="button">OEM</td>
                                <td class="button">Count</td>
                            </tr>
                            </HeaderTemplate>                    
                            <itemtemplate>
                                    <tr>
                                        <td class="copy10grey"><%# DataBinder.Eval(Container.DataItem, "CompanyName")%></td>
                                        <td class="copy10grey"><%# DataBinder.Eval(Container.DataItem, "UPC")%></td>
                                        <td class="copy10grey"><%# DataBinder.Eval(Container.DataItem,"ItemCode")%></td>
                                        <td class="copy10grey"><%# DataBinder.Eval(Container.DataItem,"ItemName")%></td>
                                        <td class="copy10grey"><%# DataBinder.Eval(Container.DataItem,"MakerName")%></td>
                                        <td class="copy10grey"><%# DataBinder.Eval(Container.DataItem,"ItemCount")%></td>
                                    </tr>   
                            </itemtemplate>
                            <AlternatingItemTemplate>
                                    <tr bgcolor="Gainsboro">
                                        <td class="copy10grey"><%# DataBinder.Eval(Container.DataItem, "CompanyName")%></td>
                                        <td class="copy10grey"><%# DataBinder.Eval(Container.DataItem, "UPC")%></td>
                                        <td class="copy10grey"><%# DataBinder.Eval(Container.DataItem,"ItemCode")%></td>
                                        <td class="copy10grey"><%# DataBinder.Eval(Container.DataItem,"ItemName")%></td>
                                        <td class="copy10grey"><%# DataBinder.Eval(Container.DataItem,"MakerName")%></td>
                                        <td class="copy10grey"><%# DataBinder.Eval(Container.DataItem,"ItemCount")%></td>
                                    </tr>                             
                            </AlternatingItemTemplate>
                            
                        </asp:DataList> 
                        </table>               
                    </td>
                  </tr>
	            </table>
            </td>
        </tr>
        <tr><td>&nbsp;</td></tr>
        <tr>
            <td>
                <foot:MenuFooter id="Menuheader2" runat="server"></foot:MenuFooter></TD>            
            </td>
        </tr>
    </form>
</body>
</html>
