<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RmaHistory.ascx.cs" Inherits="avii.Controls.RmaHistory" %>
<asp:UpdatePanel ID="upnHistory" runat="server" >
                
					<ContentTemplate>
                
<table width="100%">
<tr>
    <td >
<table bordercolor="#839abf" border="1" width="98%" align="center" cellpadding="0" cellspacing="0">
            <tr><td>

            <table border="0" width="100%" class="box" align="center" cellpadding="5" cellspacing="5">
            <tr valign="top">
                <td>
                <strong> RMA#: </strong>&nbsp;&nbsp;&nbsp;<asp:Label ID="lblRMANum" runat="server" CssClass="copy10grey"></asp:Label>
                </td>
            </tr>
            </table>
            </td>
            </tr>
    </table>
    <br />
    
    <table bordercolor="#839abf" border="1" width="98%" align="center" cellpadding="0" cellspacing="0">
    <tr><td>

    <table border="0" width="100%" class="box" align="center" cellpadding="0" cellspacing="0">
    <tr valign="top">
        <td>
            <asp:Label ID="lblHistory" runat="server" CssClass="errormessage"></asp:Label>
                            
            <asp:Repeater ID="rptRma" runat="server">
            <HeaderTemplate>
            <table width="100%" align="center">
                <tr>
                    <td class="button">
                    &nbsp;Status
                    </td>
                    <td class="button">
                        &nbsp;Last Modified Date
                    </td>
                    <td class="button">
                        &nbsp;Modified By
                    </td>
<td class="button">
                        &nbsp;Source
                    </td>                                    
	
                                    
                </tr>
                            
            </HeaderTemplate>
            <ItemTemplate>
            <tr>
                <td class="copy10grey">
                    &nbsp;<%# Eval("Status") %>
                </td>
                <td class="copy10grey">
                    &nbsp;<%# DataBinder.Eval(Container.DataItem, "ModifiedDate", "{0:MM/dd/yyyy}")%>
                </td>
                <td class="copy10grey">
                        &nbsp;<%# Eval("RmaContactName") %>
                </td>

<td class="copy10grey">
                        &nbsp;<%# Eval("PoNum")%>
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
    </td>
</tr>
</table>
</ContentTemplate>
</asp:UpdatePanel>
            