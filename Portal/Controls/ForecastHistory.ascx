<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ForecastHistory.ascx.cs" Inherits="avii.Controls.ForecastHistory" %>
<table width="100%"  align="center">
<tr>
    <td>
        <table bordercolor="#839abf" border="1" width="98%" align="center" cellpadding="0" cellspacing="0">
        <tr><td>

        <table border="0" width="100%" class="box" align="center" cellpadding="0" cellspacing="0">
        <tr valign="top">
            <td>
                            
            <asp:Label ID="lblFcLog" runat="server" ></asp:Label>
            <asp:Repeater ID="rptFClog" runat="server">
                <HeaderTemplate>
                <table width="100%" align="center">
                <tr >
                    
                    <td class="button">
                        &nbsp;Status
                    </td>
                    
                    <td class="button">
                        &nbsp;Last Modified Date
                    </td>
                                                    
                    <td class="button">
                        &nbsp;Last Modified By
                    </td>
                    <td class="button">
                        &nbsp;Action
                    </td>
                                                    
                    <td class="button">
                        &nbsp;Source
                    </td>
                                                    
                </tr>
                            
                            
                </HeaderTemplate>
                <ItemTemplate>
                <tr class="<%# Container.ItemIndex % 2 == 0 ? "alternaterow" : "" %>">
                    <td class="copy10grey">
                    <%--<span title='<%# Convert.ToString(Eval("SentASN")).Replace("'", "")%>'>--%>
                                                
                        &nbsp;<%# Eval("ForecastStatus")%>
                        <%--</span>--%>
                                                
                        
                    </td>
                
                    <td class="copy10grey">
                        &nbsp;<%# Eval("ModifiedDate")%>
                    </td>
                
                                                
                    <td class="copy10grey">
                            &nbsp;<%# Eval("UserName")%>
                    </td>
                                                
                    <td class="copy10grey">
                            &nbsp;<%# Eval("Comments")%>
                    </td>
                    <td class="copy10grey">
                            &nbsp;<%# Eval("ForecastSource")%>
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
