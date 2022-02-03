<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RmaDownload.aspx.cs" Inherits="avii.RMA.RmaDownload" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/aerostyle.css" type="text/css" rel="stylesheet" />    
</head>
<body bgcolor="#ffffff" leftmargin="0" rightmargin="0" topmargin="0">
    <form id="form1" runat="server">
    
    <table  cellSpacing="1" cellPadding="1" width="100%" align="center">
        <tr>
			<td colSpan="6" bgcolor="#dee7f6" class="topHeader" style="ORDER-RIGHT: #ffffff 1px solid; BORDER-TOP: #ffffff 1px solid; FONT-WEIGHT: bold; 
	FONT-SIZE: 10px; TEXT-TRANSFORM: uppercase; BORDER-LEFT: #ffffff 1px solid; COLOR: navy; LINE-HEIGHT: 16px; BORDER-BOTTOM: #ffffff 1px solid; FONT-FAMILY: Verdana, Arial, Helvetica, sans-serif; BACKGROUND-COLOR:#ffffff" align="left">&nbsp;&nbsp;RMA document
			</td>
        </tr>
     </table>
    <table bordercolor="#839abf" border="1" width="98%" align="center" cellpadding="0" cellspacing="0">
                            <tr><td>

                            <table border="0" width="100%" class="box" align="center" cellpadding="0" cellspacing="0">
                            <tr valign="top">
                                <td>
                                    <asp:Label ID="lblDoc" runat="server" CssClass="errormessage"></asp:Label>
                            
                                    <asp:Repeater ID="rptRMADoc" runat="server">
                                    <HeaderTemplate>
                                    <table width="100%" align="center" cellpadding="1" cellspacing="1">
                                        <tr>
                                            <td class="buttongrid">
                                            &nbsp;File name 
                                            </td>
                                            <td class="buttongrid">
                                                &nbsp;Last Modified Date
                                            </td>
                                            <td class="buttongrid">
                                                &nbsp;
                                            </td>                
                                        </tr>
                            
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                    <tr>
                                        <td class="copy10grey">
                                            <asp:LinkButton ID="lnlDoc" CommandArgument='<%# Eval("RMADocument")%>'  OnCommand="DownloadRmaDoc_Click" runat="server" >
                                            &nbsp;<%# Eval("RMADocument")%>
                                            </asp:LinkButton>
                                        
                                        </td>
                                        <td class="copy10grey">
                                            &nbsp;<%# Eval("ModifiedDate")%>
                                            <%--<%# DataBinder.Eval(Container.DataItem, "ModifiedDate", "{0:MM/dd/yyyy}")%>--%>
                                        </td>
                                        <td class="copy10grey">
                                              <asp:ImageButton ID="imgDel" runat="server" OnClientClick="return confirm('Do you want to delete?');"  CommandName="Delete" AlternateText="Delete RMA Document" ToolTip="Delete RMA Document" 
                        ImageUrl="~/images/delete.png" CommandArgument='<%# Eval("RmaDocID") %>' OnCommand="imgDeleteRMADoc_OnCommand"/>
                        
                                        </td>
                                
                                    </tr>
                                    
                                    
                                    </ItemTemplate>
                                    <FooterTemplate>                                        </table>    
                                    </FooterTemplate>
                                    </asp:Repeater>
                        
                                </td>
                            </tr>
                            </table>
                            </td></tr>
                            </table>
    <br />
    
    
    <table  cellSpacing="1" cellPadding="1" width="100%" align="center">
        <tr>
			<td colSpan="6" bgcolor="#dee7f6" class="topHeader" style="ORDER-RIGHT: #ffffff 1px solid; BORDER-TOP: #ffffff 1px solid; FONT-WEIGHT: bold; 
	FONT-SIZE: 10px; TEXT-TRANSFORM: uppercase; BORDER-LEFT: #ffffff 1px solid; COLOR: navy; LINE-HEIGHT: 16px; BORDER-BOTTOM: #ffffff 1px solid; FONT-FAMILY: Verdana, Arial, Helvetica, sans-serif; BACKGROUND-COLOR:#ffffff" align="left">&nbsp;&nbsp;Administration RMA document
			</td>
        </tr>
        </table>
    <table bordercolor="#839abf" border="1" width="98%" align="center" cellpadding="0" cellspacing="0">
                            <tr><td>

                            <table border="0" width="100%" class="box" align="center" cellpadding="0" cellspacing="0">
                            <tr valign="top">
                                <td>
                                    <asp:Label ID="lblMsgAdm" runat="server" CssClass="errormessage"></asp:Label>
                                
                                    <asp:Repeater ID="rptAdminRma" runat="server">
                                    <HeaderTemplate>
                                    <table width="100%" align="center" cellpadding="1" cellspacing="1">
                                        <tr>
                                            <td class="buttongrid">
                                            &nbsp;File name 
                                            </td>
                                            <td class="buttongrid">
                                                &nbsp;Last Modified Date
                                            </td>
                                            <td class="buttongrid">
                                                &nbsp;
                                            </td>                
                                        </tr>
                            
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                    <tr>
                                        <td class="copy10grey">
                                            <asp:LinkButton ID="lnlDoc" CommandArgument='<%# Eval("RMADocument")%>'  OnCommand="DownloadRmaDoc_Click" runat="server" >
                                            &nbsp;<%# Eval("RMADocument")%>
                                            </asp:LinkButton>
                                        
                                        </td>
                                        <td class="copy10grey">
                                            &nbsp;<%# Eval("ModifiedDate")%>
                                            <%--<%# DataBinder.Eval(Container.DataItem, "ModifiedDate", "{0:MM/dd/yyyy}")%>--%>
                                        </td>
                                        <td class="copy10grey">
                                              <asp:ImageButton ID="imgDel" runat="server" OnClientClick="return confirm('Do you want to delete?');" CommandName="Delete" AlternateText="Delete RMA Document" ToolTip="Delete RMA Document" 
                        ImageUrl="~/images/delete.png" CommandArgument='<%# Eval("RmaDocID") %>' OnCommand="imgDeleteRMADoc_OnCommand"/>
                        
                                        </td>
                                
                                    </tr>
                                    
                                    
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </table>    
                                    </FooterTemplate>
                                    </asp:Repeater>
                        
                                </td>
                            </tr>
                            </table>
                            </td></tr>
                            </table>
                            
    </form>
</body>
</html>
