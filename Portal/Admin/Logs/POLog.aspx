<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="POLog.aspx.cs" Inherits="avii.Admin.Logs.POLog" %>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="/Controls/Header.ascx" %>


<html>
head id="Head1" runat="server">
    <title>.:: Lan Global ::.</title>
    <link href="/aerostyle.css" rel="stylesheet" type="text/css"/>
    <script language="javascript" src="../avI.js" type="text/javascript"></script>
	<LINK href="/Styles.css" type="text/css" rel="stylesheet"> 
    <script language="javascript" type="text/javascript">
        function KeyDownHandler(btn) {

            if (event.keyCode == 13) {
                event.returnValue = false;
                event.cancel = true;
                btn.click();
            }
        }
			</script>
</head>

<body leftmargin="0" topmargin="0" marginwidth="0" marginheight="0" onkeydown="KeyDownHandler(document.all.btnUpload)">
    <form id="form2" runat="server" method="post" >
    <div>
        <TABLE cellSpacing="0" cellPadding="0" align="center" width="100%" border="0">
        	<tr>
				<td><head:menuheader id="HeadAdmin" runat="server"></head:menuheader></td>
			</tr>
            <tr><td>&nbsp;</td></tr>
			<TR>
				<TD  bgcolor="#dee7f6" class="button">&nbsp;&nbsp;Fulfillment Log
				</TD>
			</TR>
            <tr><td>&nbsp;</td></tr>
			<tr>                    
                <td colspan="2">
                    <asp:Label ID="lblMsg" runat="server" Width="100%" CssClass="errormessage"></asp:Label></td>
            </tr>               
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                    <asp:DataList ID="dlPO" runat="server" Width="90%">
                        <HeaderTemplate>                            
                            <table width="100%">
                                <tr class="Button">
                                    <td width="10%">Fulfillment Date</td>
                                    <td width="10%">Fulfillment ID</td>
                                    <td width="10%">Status</td>
                                    <td width="40%">XML Data</td>
                                    <td width="30%">Comments</td>
                                 </tr>                           
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <%# DataBinder.Eval(Container.DataItem, "FulfillmentDate")%>                                
                                </td>
                                <td>
                                    <%# DataBinder.Eval(Container.DataItem, "PO_ID")%>
                                </td>
                                <td>
                                    <%# DataBinder.Eval(Container.DataItem, "POStatus")%>
                                </td>
                                <td>
                                    <%# DataBinder.Eval(Container.DataItem, "PoDataXML")%>
                                </td>
                                <td>
                                    <%# DataBinder.Eval(Container.DataItem, "Comments")%>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                        </table>
                        </FooterTemplate>
                    </asp:DataList>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
