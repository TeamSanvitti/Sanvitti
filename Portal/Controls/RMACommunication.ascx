<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RMACommunication.ascx.cs" Inherits="avii.Controls.RMACommunication" %>

<table width="100%"  align="center" >

<tr>
    <td>
        <asp:Label ID="lblMsg" runat="server" CssClass="errormessage" ></asp:Label>
        <%--class="<%#   Convert.ToString(Eval("usertype")) != "Company" ? "alternaterows" : Container.ItemIndex % 2 == 0 ? "alternaterow" : "" %>"--%>
        <asp:Repeater ID="rptComments" runat="server">
            <HeaderTemplate>
            <table width="100%" align="center" cellpadding="1" cellspacing="1">
                <tr    >
                    <td class="button" width="1%">
                        &nbsp;S.No.
                    </td>
                    <td class="button" width="10%">
                        &nbsp;Comment By
                    </td>
                    <td class="button" width="15%">
                        &nbsp;Create Date
                    </td>
                    <td class="button" width="50%">
                        &nbsp;Comments
                    </td>
                                                    
                </tr>
                            
            </HeaderTemplate>
            <ItemTemplate>
            <tr height="18px" class="<%#   Container.ItemIndex % 2 == 0 ? "alternaterow" : "" %>">
                                                
                <td class="copy10grey">
                        &nbsp;<%#  Container.ItemIndex + 1 %>
                </td>
                                
                <td class="copy10grey">
                        &nbsp;<%# Eval("UserName")%>
                </td>                           
                <td class="copy10grey">
                        &nbsp;<%# Eval("CreateDate")%>
                </td>
                <td class="copy10grey">
                        &nbsp;<%# Eval("Comments")%>
                </td>
                <%--<td class="copy10grey">
                        &nbsp;<%# Eval("UserType")%>
                </td>--%>
            </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>    
            </FooterTemplate>
            </asp:Repeater>
                        
    </td>
</tr>
</table>
