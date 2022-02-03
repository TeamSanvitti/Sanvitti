<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UploadPOData.aspx.cs" Inherits="avii.Admin.UploadPOData" %>
<%@ Register TagPrefix="head1" TagName="MenuHeader1" Src="../admin/admhead.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>.:: Aerovoice ::.</title>
    <link href="../aerostyle.css" rel="stylesheet" type="text/css"/>
    <script language="javascript" src="../avI.js" type="text/javascript"></script>
		<LINK href="../Styles.css" type="text/css" rel="stylesheet"> 
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
        <TABLE cellSpacing="0" cellPadding="0" align="center" width="100%" border="0">
        	<tr>
				<td><head1:menuheader1 id="HeadAdmin" runat="server"></head1:menuheader1></td>
			</tr>
			<tr><td>&nbsp;</td></tr>
			<TR>
				<TD  bgcolor="#dee7f6" class="button">&nbsp;&nbsp;Upload Purchase Order Data
				</TD>
			</TR>
            <tr><td>&nbsp;</td></tr>
            
            <tr>
                <td align="center">
                 <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="70%">
                    <tr bordercolor="#839abf">
                    <td>
					<table id="Table2" cellSpacing="5" cellPadding="5" width="100%" border="0">
						<tr>                    
                            <td colspan="2">
                                <asp:Label ID="lblMsg" runat="server" Width="100%" CssClass="errormessage"></asp:Label></td>
                            </tr>                    
                            <tr id="trUser" runat="server">
                                <td style="width: 35%" class="copy10grey" align="right"  >
                                    Purchase Order Data Type:</td>
                                <td style="width:65%" align="left" >
                                    <asp:DropDownList ID="ddlData" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlData_SelectedIndexChanged">
                                     <asp:ListItem></asp:ListItem>
                                       
                                        <asp:ListItem Text="Shipping Information" Value="ship"></asp:ListItem>
                                    </asp:DropDownList>

                             </tr>
                             <tr id="trItems" runat="server" visible="false">
                                  <td  class="copy10grey" align="right" >Inventory Item</td>
                                  <td align="left">
                                    <asp:DropDownList ID="dpItems" runat="server" AutoPostBack="True" OnSelectedIndexChanged="dpItems_SelectedIndexChanged"></asp:DropDownList>
                                    &nbsp;<asp:HyperLink ID="hnk" runat="server" Visible=false></asp:HyperLink>
                                  </td>
                             </tr>
                             
                            <tr>
                                <td  class="copy10grey" align="right" >
                                    Select a file</td>
                                <td align="left" >
                                    <asp:FileUpload ID="flnUpload" runat="server" CssClass="txfield1" Width="60%" /></td>
                            </tr>
                             <tr id="trItemFile" runat="server" visible="false">
                                <td class="copy10grey" align="left" colspan="2">
                                    Select the Phone Code from the dropdown and upload the file with list of ESNs. The CSV file will have only one ESN column.
                                </td>
                                
                             </tr>
                             <tr id="trShipFile" runat="server" visible="false">
                                <td class="copy10grey" align="right">
                                    File format sample:
                                </td>
                                <td class="copy10grey" align="left">
                                    poNum,trackingnumber,AvOrderNumber
                                </td>
                             </tr>
                            <tr>
                                <td colspan="4">
                                    </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    </td>
                            </tr>
                            <tr id="trStatus" runat="server">
                                <td style="width: 35%" class="copy10grey" align="right"  >
                                    </td>
                                <td style="width:65%" >
                                    </td>
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

    </form>
</body>
</html>