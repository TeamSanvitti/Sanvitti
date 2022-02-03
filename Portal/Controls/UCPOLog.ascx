<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCPOLog.ascx.cs" Inherits="avii.Controls.UCPOLog" %>
<table width="100%"  align="center">

<tr>
    <td>
        <asp:Label ID="lblMsg" runat="server" CssClass="errormessage" ></asp:Label>
                                        <asp:Repeater ID="rptLog" runat="server">
                                            <HeaderTemplate>
                                            <table width="100%" align="center">
                                                <tr >
                                                    <td class="buttonlabel" width="1%">
                                                        &nbsp;S.No.
                                                    </td>
                                                    <td class="buttonlabel" width="19%">
                                                        &nbsp;Date
                                                    </td>
                                                    <td class="buttonlabel" width="20%">
                                                        &nbsp;Log By
                                                    </td>
                                                    <td class="buttonlabel" width="20%">
                                                        &nbsp;Status
                                                    </td>
                                                    <td class="buttonlabel" width="20%">
                                                        &nbsp;Request Data
                                                    </td>
                                                    <td class="buttonlabel" width="20%">
                                                        &nbsp;Response Data
                                                    </td>
                                                    
                                                </tr>
                            
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                            <tr class="<%# Container.ItemIndex % 2 == 0 ? "alternaterow" : "" %>">
                                                
                                                <td class="copy10grey">
                                                        &nbsp;<%# Container.ItemIndex + 1 %>
                                                </td>
                                
                                                <td class="copy10grey">
                                                        &nbsp;<%# Eval("CreateDate")%>
                                                </td>
                                                <td class="copy10grey">
                                                        &nbsp;<%# Eval("UserName")%>
                                                </td>
                                                <td class="copy10grey">
                                                        &nbsp;<%# Eval("Status")%>
                                                </td>
                                                <td class="copy10grey">
                                                        &nbsp;
                                                </td>
                                                <td class="copy10grey">
                                                        &nbsp;
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

