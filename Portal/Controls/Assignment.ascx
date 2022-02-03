<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Assignment.ascx.cs" Inherits="avii.Controls.Assignment" %>
<table width="100%"  align="center">
<tr>
    <td>
        <asp:Label ID="lblESN" runat="server" ></asp:Label>
                                        <asp:Repeater ID="rptESN" runat="server">
                                            <HeaderTemplate>
                                            <table width="100%" align="center">
                                                <tr >
                                                    <td class="button" width="2%">
                                                        &nbsp;S.No.
                                                    </td>
                                                    <td class="button">
                                                        &nbsp;Assignment
                                                    </td>
                                                    
                                                </tr>
                            
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                            <tr class="<%# Container.ItemIndex % 2 == 0 ? "alternaterow" : "" %>">
                                                
                                                <td class="copy10grey">
                                                        &nbsp;<%# Container.ItemIndex + 1 %>
                                                </td>
                                
                                                <td class="copy10grey">
                                                        &nbsp;<%# Eval("ESN")%>
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