<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ItemSummary.aspx.cs" Inherits="avii.ItemSummary" %>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="/Controls/Header.ascx" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="/Controls/Footer.ascx" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Inventory Summary</title>
    <link href="./aerostyle.css" rel="stylesheet" type="text/css">
    <script language="javascript" src="./avI.js"></script> 
    <link rel="stylesheet" href="./fullfillment/calendar/dhtmlgoodies_calendar.css" media="screen"></link>
    <script type="text/javascript" src="./fullfillment/calendar/dhtmlgoodies_calendar.js"></script>  
    <script type="text/javascript" language="javascript">
        function Validation() {

            var isAdmin = document.getElementById("<%=hdnAdmin.ClientID %>").value;
            
            if (isAdmin == "admin") {
                var customerObj = document.getElementById("<%=dpCompany.ClientID %>");
                //alert(customerObj.selectedIndex);
                if (customerObj.selectedIndex == 0) {
                    alert('Company required!');
                    return false;

                }
            }
        }
    </script>

</head>
<body bgColor="#ffffff" leftmargin="0" rightmargin="0" topmargin="0">
    <form id="form1" runat="server">
    <div align="center">
   	<TABLE cellSpacing="0" cellPadding="0"  border="0" align="center" width="95%">
				<TR>
					<TD>
                        <head:menuheader id="MenuHeader" runat="server"></head:menuheader>
					</TD>
				</TR>
			<TR>
				<TD align="center">
                <br />
                <table  cellSpacing="1" cellPadding="1" width="100%">
                    <tr>
			            <td colSpan="6" bgcolor="#dee7f6" class="buttonlabel">&nbsp;&nbsp;Inventory Summary
			            </td>
                    </tr>
                    <tr><td><asp:Label ID="lblMsg" runat="server" CssClass="errormessage"></asp:Label></td></tr>
                    
                </table>
                <table  cellSpacing="1" cellPadding="1" width="100%">
                <tr><td class="copy10grey" align="left">
                    - Please select your search
                    criterial to narrow down the search and record selection.<br />
                    - Atleast one search criteria should be selected.<br />
                    - By default it will search one year back records.
                    </td></tr>
                    
                </table>

                <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="99%" >
                       <tr bordercolor="#839abf">
                            <td>
                                <table cellSpacing="3" cellPadding="3" width="100%"  >
                              <tr>
                            <td align="right" class="copy10grey" Width="10%">
                                Inventory From:</td>
                            <td>
                            </td>
                            <td Width="38%">
                                <asp:HiddenField ID="hdnAdmin" runat="server" />
                                <asp:TextBox ID="txtFromDate" Width="70%" onkeypress="return false;" runat="server" CssClass="copy10grey" MaxLength="15"></asp:TextBox>
                                <img id="imgFromtDate" alt="" onclick="document.getElementById('<%=txtFromDate.ClientID%>').value = ''; displayCalendar(document.getElementById('<%=txtFromDate.ClientID%>'),'mm/dd/yyyy',this,true);" src="../fullfillment/calendar/sscalendar.jpg" />
                            </td>
                            <td align="right" class="copy10grey" Width="10%">Inventory To:</td>
                            <td></td>
                            <td Width="42%">
                                <asp:TextBox ID="txtToDate" Width="66%" onkeypress="return false;" runat="server" CssClass="copy10grey" MaxLength="15"></asp:TextBox>
                                <img id="imgToDate"  alt="" onclick="document.getElementById('<%=txtToDate.ClientID%>').value = ''; displayCalendar(document.getElementById('<%=txtToDate.ClientID%>'),'mm/dd/yyyy',this,true);" src="../fullfillment/calendar/sscalendar.jpg" />
                            
                            </td>               
                        </tr>
                        <tr>
                            <td align="right" class="copy10grey" width="15%">
                                Sku#:</td>
                            <td></td>
                            <td>
                               <asp:TextBox ID="txtSku" runat="server" Width="70%" CssClass="copy10grey"></asp:TextBox></td>    
                            
                       </tr>
                       <tr>
                            <asp:Panel ID="pnlCompany" runat="server" Visible="false">
                            <td align="right" class="copy10grey">
                                Company:</td>
                            <td></td>
                            <td>
                                <asp:DropDownList ID="dpCompany" runat="server" class="copy10grey" Width="70%">
                                            <%--<asp:ListItem Text="" Value=""></asp:ListItem>
                                            <asp:ListItem Text="iWireless" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="telSpace" Value="1"></asp:ListItem>--%>
                                        </asp:DropDownList>
                            </td>
                            </asp:Panel>
                        </tr>
                        <tr><td colspan="6"><hr />
                        </td></tr>
                        <tr>
                            <td colspan="6" align="center">
                                <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Get Summary" OnClick="btnSearch_Click" />&nbsp;
                                <asp:Button ID="btnEsnList" runat="server" CssClass="button" Text="Get EsnList" OnClientClick="return Validation();" OnClick="btnEsnList_Click" />&nbsp;
                                <asp:Button ID="btnCancel" runat="server" CssClass="button" Text="Cancel" OnClick="btnCancel_Click" />
                            </td>
                        </tr> 
                    </table>
                            </td>
                        </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td> <br />
                            <asp:Panel id="pnlItems" Width="100%" runat="server"></asp:Panel>
                        </td>
                    </tr>
                </table>
                </TD>
            </TR>
        </TABLE>
        </div>
        <br />
        <foot:MenuFooter ID="footer1" runat="server" />
    </form>
</body>
</html>
