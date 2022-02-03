<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FulfillmentComments.ascx.cs" Inherits="avii.Controls.FulfillmentComments" %>

<table width="100%"  align="center">

<tr>
    <td>
        <asp:Label ID="lblMsg" runat="server" CssClass="errormessage" ></asp:Label>
                                        <asp:Repeater ID="rptComments" runat="server">
                                            <HeaderTemplate>
                                            <table width="100%" align="center">
                                                <tr >
                                                    <td class="buttonlabel" width="1%">
                                                        &nbsp;S.No.
                                                    </td>
                                                    <td class="buttonlabel" width="10%">
                                                        &nbsp;Date
                                                    </td>
                                                    <td class="buttonlabel" width="89%">
                                                        &nbsp;Comments
                                                    </td>
                                                    
                                                </tr>
                            
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                            <tr class="<%# Container.ItemIndex % 2 == 0 ? "alternaterow" : "" %>">
                                                
                                                <td class="copy10grey">
                                                        &nbsp;<%# Container.ItemIndex + 1 %>
                                                </td>
                                
                                                <td class="copy10grey">
                                                        &nbsp;<%# Eval("CommentDate")%>
                                                </td>
                                                <td class="copy10grey">
                                                        &nbsp;<%# Eval("Comments")%>
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
