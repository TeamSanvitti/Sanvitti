<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UploadData.aspx.cs" Inherits="avii.fullfillment.UploadData" %>

<%@ Register TagPrefix="head1" TagName="MenuHeader1" Src="../admin/admhead.ascx" %>
<%@ Register TagPrefix="serv" TagName="MenuSP" Src="../Controls/provider.ascx" %>
<%@ Register TagPrefix="foot" TagName="MenuHeader" Src="../Controls/Footer.ascx" %>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="../Controls/Header.ascx" %>

<html>
<head id="Head1" runat="server">
    <title>.:: Lan Global ::.</title>
    <link href="../aerostyle.css" rel="stylesheet" type="text/css"/>
    <script language="javascript" src="../avI.js" type="text/javascript"></script>
    			<script language="javascript" type="text/javascript">
	function KeyDownHandler(btn)
    {

       if (event.keyCode == 13)
        {
            event.returnValue=false;
            event.cancel = true;
            btn.click();
        }
    }
			</script>
</head>
<body leftmargin="0" topmargin="0" marginwidth="0" marginheight="0" onkeydown="KeyDownHandler(document.all.btnUpload)">
    <form id="form1" runat="server" method="post" >
        <TABLE cellSpacing="0" cellPadding="0" align="center" border="0">
        	<tr>
				<td><head1:menuheader1 id="HeadAdmin" runat="server"></head1:menuheader1>
				<head:menuheader id="HeadUser" runat="server"></head:menuheader>
				</td>
			</tr>
			<tr><td>&nbsp;</td></tr>
			<TR>
				<TD  bgcolor="#dee7f6" class="button">&nbsp;&nbsp;Upload Fullfillment Document
				</TD>
			</TR>
            <tr><td>&nbsp;</td></tr>
            
            <tr>
                <td align="center">
                 <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="100%">
                    <tr bordercolor="#839abf">
                    <td>
					<table id="Table2" cellSpacing="5" cellPadding="5" width="100%" border="0">
						<tr>                    
                            <td colspan="2">
                                <asp:Label ID="lblMsg" runat="server" Width="100%" CssClass="errormessage"></asp:Label></td>
                            </tr>                    
                            <tr id="trUser" runat="server">
                                <td style="width: 35%" class="copy10grey" align="right"  >
                                    User</td>
                                <td style="width:65%" >
                                    <asp:DropDownList ID="ddlUsers" runat="server" Width="152px" CssClass="txfield1">
                                    </asp:DropDownList></td>
                             </tr>
                            <tr>
                                <td  class="copy10grey" align="right" >
                                    Select a file</td>
                                <td >
                                    <asp:FileUpload ID="flnUpload" runat="server" CssClass="txfield1" Width="220px" /></td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    </td>
                            </tr>
                            <tr>
                                <td  class="copy10grey" align="right" >
                                    Comments</td>
                                <td>
                                    <asp:TextBox ID="txtComments" runat="server" Height="74px" TextMode="MultiLine" Width="230px" CssClass="txfield1"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    </td>
                            </tr>
                            <tr id="trStatus" runat="server">
                                <td style="width: 35%" class="copy10grey" align="right"  >
                                    Status</td>
                                <td style="width:65%" >
                                    <asp:DropDownList ID="ddlStatus" runat="server" Width="152px" CssClass="txfield1">
                                    </asp:DropDownList></td>
                             </tr>
                             <tr><td colspan="2">&nbsp;</td></tr>                            
                             <tr>
                                <td colspan="2" align=center><asp:Button ID="btnUpload" Width="190px" CssClass="button" runat="server" OnClick="btnUpload_Click" Text="Upload" />
                                    </td>
                            </tr>
                    </table>
            </td>
            </tr>
            </table>
                    </td>
                </tr>
                <tr>
                    <td ><br /><br />
                    </td>
                </tr>

            </table>
			<table align="center" width="95%" border="0" cellpadding="0" cellspacing="0">
				<tr>
					<td colspan="2"><foot:menuheader id="footer" runat="server"></foot:menuheader></td>
				</tr>
			</table>
    </form>
</body>
</html>

