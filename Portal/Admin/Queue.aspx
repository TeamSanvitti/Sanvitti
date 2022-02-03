<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Queue.aspx.cs" Inherits="avii.Admin.Queue" %>
<%--<%@ Register TagPrefix="head" TagName="MenuHeader" Src="./admhead.ascx" %>--%>
<%@ Register TagPrefix="head" TagName="menuheader" Src="~/dhtmlxmenu/menuControl.ascx" %>

<%@ Register TagPrefix="esnasn" TagName="ctrl" Src="../Controls/Esn_Asn_Queue.ascx" %>
<%@ Register TagPrefix="ctrl" TagName="polist" Src="../Controls/POList.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Lan Global inc. Inc. - Esn/ASN Queue</title>
		<LINK href="../aerostyle.css" type="text/css" rel="stylesheet">
		<LINK href="../Styles.css" type="text/css" rel="stylesheet">
</head>
<body bgColor="#ffffff" leftmargin="0" rightmargin="0" topmargin="0">
    <form id="form1" runat="server">
        <table cellSpacing="0" cellPadding="0"  border="0" align="center" width="100%">
	        <tr>
		        <td>
		        <head1:menuheader1 id="HeadAdmin" runat="server"></head1:menuheader1>
		        </td>
	        </tr>
     </table>
     <table  cellSpacing="1" cellPadding="1" width="100%">
        <tr>
		    <td colSpan="6" bgcolor="#dee7f6" class="button">&nbsp;&nbsp;ESN/ASN Queue
		    </td>
        </tr>
        <tr><td><asp:Label ID="lblMsg" runat="server" CssClass="errormessage"></asp:Label></td></tr>
    </table>
    <br />
    <table  cellSpacing="1" cellPadding="1" width="100%">
                <tr><td class="copy10grey">
                - Following list contains ESN and ASN purchase orders in the queue for Quolution.<br />
                - Search the specific purchase order by entering Customer from dropdown list.</td></tr>
    </table>  
    <br />  
    <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="100%" >
        <tr bordercolor="#839abf">
            <td>
                <table cellSpacing="1" cellPadding="1" width="100%"  >
                <tr>
                <td>
                    &nbsp;
                </td></tr>
                <tr>
                <!--<td align="right" class="copy10grey" width="15%">
                    Purchase Order#:</td>
                <td>
                </td>
                <td>
                    <asp:TextBox ID="txtPONum" onkeypress="JavaScript:return alphaOnly(event);" MaxLength="20" runat="server" CssClass="copy10grey" ></asp:TextBox></td>
                <td align="right" class="copy10grey" width="15%">
                    </td>
                -->
                <td align="right" class="copy10grey">
                    Customer :</td>
                <td>
                </td>
                <td>
                    <asp:DropDownList ID="dpCompany" CssClass="copy10grey" runat="server">
                            <asp:ListItem Value =""></asp:ListItem>                            
                        </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="2"></td>
                <td>
                    <asp:CheckBox ID="chkESN" runat="server" CssClass="copy10grey"  Text="PO with missing ESN"/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:CheckBox ID="chkMSL" runat="server" CssClass="copy10grey"  Text="PO with missing MSL"/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:CheckBox ID="chkTracking" runat="server" CssClass="copy10grey"  Text="PO with Missing Tracking#s"/>
                
                </td>
                          
            </tr>
             <tr>
                    <td colspan="8" align="center">
                        <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Get Queue Data"  
                            OnClick="btnSearch_Click"
                            />&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="button" OnClick="btnCancel_Click" 
                            />
                    </td>
                </tr>
            </table>
            </td>
        </tr>
    </table>     
    
    <br />
	<table cellSpacing="1" cellPadding="5" border="0" width="100%" > 
	<tr>
		<td valign="top">
		    <asp:Panel ID="pnlEsn" width="100%" runat="server"></asp:Panel>			
		</td>
		<td valign="top">
		    <asp:Panel ID="pnlAsn" width="100%" runat="server"></asp:Panel>			
		</td>
			
		
	</tr>
	<tr><td>&nbsp;</td></tr>
	 <ctrl:polist ID="poList" runat="server" />
           
</table>
    
    </form>
</body>
</html>
