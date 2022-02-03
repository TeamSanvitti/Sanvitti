<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucForecastDetail.ascx.cs" Inherits="avii.Controls.ucForecastDetail" %>
<table width="100%"  align="center">
<tr>
    <td>
        <table bordercolor="#839abf" border="1" width="98%" align="center" cellpadding="0" cellspacing="0">
        <tr><td>

        <table border="0" width="100%" class="box" align="center" cellpadding="0" cellspacing="0">
        <tr valign="top">
            <td>
                            
            <asp:Label ID="lblMsg" runat="server" ></asp:Label>

            <asp:Repeater ID="rptItem" runat="server" >
                            <HeaderTemplate>
                            <table  cellSpacing="2" cellPadding="2" width="100%" align="center" >
                                 <tr >
                                <td class="button" align="left" width="20%">
                                SKU#
                                </td>
                                <td class="button" align="left" width="10%">
                                Quantity
                                </td>
                                <td class="button" width="10%">
                                Price
                                </td>
                                <td class="button" width="10%">
                                Total
                                </td>
                                <td class="button" width="10%">
                                Status
                                </td>
                                <td class="button" width="30%">
                                Comments
                                </td>
                               </tr>
                               </HeaderTemplate>
                                  <ItemTemplate>
                                  <tr>

                                    <td align="left">
                                    
                                        <%# Eval("SKU") %>

                                    
													
                                    </td>
                                    <td align="left">
                                        <%#Eval("Quantity")%>
                                    </td>
                                    <td align="right">
                                        <%# "$" + Eval("price", "{0:n}") %>

                                    </td>
                                    <td align="right">
                                        <%# "$" + Eval("totalprice", "{0:n}") %>

                                    </td>
                                    <td align="left">
                                        <%#  Eval("LineItemStatus") %>
                                    </td>
                                    <td align="left">
                                    <%#  Eval("Comments") %>
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

