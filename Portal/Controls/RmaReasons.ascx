<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RmaReasons.ascx.cs" Inherits="avii.Controls.RmaReasons" %>

<table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="100%" align="center" >
           <tr bordercolor="#839abf">
                <td>
                    <table width="100%" cellSpacing="1" cellPadding="1">
                    <tr>
                    <td>
                    
                        <asp:Repeater ID="rptReason" runat="server"   >
                                <HeaderTemplate>
                                <table border="0" width="98%" cellpadding="1" cellspacing="1">
                                <tr>
                                    <td class="button" >
                                        S.No.
                                    </td>
                                    <td class="button" >
                                        Reason
                                    </td>
                                    <%--
                                    <td class="button">
                                        
                                    </td>--%>
                                </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
    
                                    <tr valign="bottom"  class="<%# Container.ItemIndex % 2 == 0 ? "alternaterow" : "" %>">
                                        <td class="copy10grey"  >
                                        <%# Container.ItemIndex +  1 %>
                                        </td>
                                        <td valign="bottom" class="copy10grey"  >
                                        <span width="100%">
                                            <%# Eval("Reason")%>    
                                            </span>
                                        
                                        </td>
                                        <%--<td valign="bottom" class="copy10grey"  >
                                        <span width="100%">
                                            <%# Eval("Reason")%>    
                                            </span>
                                        
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
                    </td>
                    </tr>
                    </table>

