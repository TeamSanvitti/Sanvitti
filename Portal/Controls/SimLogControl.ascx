<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SimLogControl.ascx.cs" Inherits="avii.Controls.SimLogControl" %>
<table width="100%" border="0">
<tr>
    <td>
        <asp:Label ID="lblLog" runat="server"  CssClass="errormessage"></asp:Label>
        <asp:Repeater ID="rptSimLog" runat="server"   >
        <HeaderTemplate>
        <table border="0" width="98%" cellpadding="1" cellspacing="1">
        <tr>
            <%--<td class="button" >
                S.No.
            </td>--%>
            <td class="button" >
                Status
            </td>
            
            <td class="button">
                Last Modified Date                 
            </td>
            
            <td class="button">
               Last Modified By               
            </td>
            
            <td class="button">
                Action         
            </td>
            
            <td class="button">
                Source                  
            </td>
        </tr>
        </HeaderTemplate>
        <ItemTemplate>
    
            <tr valign="bottom"  class="<%# Container.ItemIndex % 2 == 0 ? "alternaterow" : "" %>">
                <%--<td class="copy10grey"  >
                <%# Container.ItemIndex +  1 %>
                </td>--%>
                <td valign="bottom" class="copy10grey"  >
                <span width="100%">
                    <%# Eval("Status")%>    
                    </span>
                                        
                </td>
                <td valign="bottom" class="copy10grey"  >
                <span width="100%">
                    <%# Eval("SimLogDate")%>    
                    </span>
                                        
                </td>
                <td valign="bottom" class="copy10grey"  >
                <span width="100%">
                    <%# Eval("ModifiedBy")%>    
                    </span>
                                        
                </td>
                <td valign="bottom" class="copy10grey"  >
                <span width="100%">
                    <%# Eval("Action")%>    
                    </span>
                                        
                </td>
                <td valign="bottom" class="copy10grey"  >
                <span width="100%">
                    <%# Eval("Source")%>    
                    </span>
                                        
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


