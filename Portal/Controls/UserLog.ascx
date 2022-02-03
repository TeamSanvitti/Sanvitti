<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserLog.ascx.cs" Inherits="avii.Controls.UserLog" %>
<asp:Repeater ID="rptUser" runat="server">
<HeaderTemplate>
<table border="0" width="100%" cellpadding="1" cellspacing="1">
<tr>
    <td style="width:50%" class="button" align="left">
    SignIn
    </td>
    <td style="width:50%"  class="button" align="left">
    SignOut
    </td>
</tr>
</HeaderTemplate>
<ItemTemplate>
    
    <tr valign="bottom"  class="<%# Container.ItemIndex % 2 == 0 ? "alternaterow" : "" %>">
        <td valign="bottom" class="copy10grey" align="left"  >
        <span width="100%">
            <%# Eval("SessionStartDate")%>    </span>
        </td>
        <td align="left" class="copy10grey" valign="bottom">
           <%-- <%# Convert.ToString(Eval("SessionEndDate")) == "1/1/1900 12:00:00 AM" ? "" : Eval("SessionEndDate")%> <%# Eval("SessionEndDate")%>      
           --%> 
           
           <%# Convert.ToString(Eval("SessionEndDate")).IndexOf("1/1/1900") > -1 ? "" : Eval("SessionEndDate")%>     
        </td>
    </tr>
    
    

    
</ItemTemplate>
<FooterTemplate>
</table>
</FooterTemplate>
</asp:Repeater>
<asp:Label ID="lblUser" runat="server" CssClass="errormessage"></asp:Label>