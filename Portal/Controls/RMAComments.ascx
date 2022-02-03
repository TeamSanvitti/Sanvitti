<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RMAComments.ascx.cs" Inherits="Sanvitti1.Controls.Aarons.RMAComment" %>
<table width="100%"  align="center" >
<%--<tr>
    <td class="copy10grey" align="left">
   
  &nbsp; All the excluded comments will not be added to the e-mail.
    </td>
</tr>
--%>
<tr>
    <td>
        <asp:Label ID="lblMsg" runat="server" CssClass="errormessage" ></asp:Label>

        <asp:Repeater ID="rptComments" runat="server">
            <HeaderTemplate>
            <table width="100%" align="center" cellpadding="1" cellspacing="1">
                <tr    >
                    <td class="button" width="1%">
                        &nbsp;S.No.
                    </td>
                    <td class="button" width="15%">
                        &nbsp;User Name
                    </td>
                    <td class="button" width="24%">
                        &nbsp;Create Date
                    </td>
                    <td class="button" width="40%">
                        &nbsp;Comments
                    </td>
                    <td class="button" width="20%" align="center">
                        &nbsp;Exclude/Include
                    </td>
                                                    
                </tr>
                            
            </HeaderTemplate>
            <ItemTemplate>
            <tr height="18px" class="<%#   Convert.ToString(Eval("usertype")) != "Customer" ? "alternaterows" : Container.ItemIndex % 2 == 0 ? "alternaterow" : "" %>">
                     
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
                <td class="copy10grey"  align="center">
                        <%--&nbsp;<%# Eval("Comments")%>--%>
                    <asp:CheckBox ID="chkEx" runat="server" CssClass="copy10grey" Checked='<%# Eval("Exclude")%>' />
                    <asp:HiddenField ID="hdnCommentID" runat="server" Value='<%# Eval("CommentID")%>'  />
                    
                </td>

            </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>    
            </FooterTemplate>
            </asp:Repeater>

                        
    </td>
</tr>
<tr>
    <td>

    <hr />
    </td>
</tr>
<tr>
    <td class="copy10grey" align="center">
    <asp:Button ID="btnUpdate" runat="server" Text="Update Comments" CssClass="buybt"   OnClick="btnUpdate_Click" />
                                    
    </td>
</tr>
</table>
