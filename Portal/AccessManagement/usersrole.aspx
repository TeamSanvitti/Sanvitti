<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="usersrole.aspx.cs" Inherits="avii.AccessManagement.usersrole" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <LINK href="../lanstyle.css" type="text/css" rel="stylesheet">
</head>
<body bgcolor="#ffffff" >
    <form id="form1" runat="server">
    <div>
        <table width="90%">
        <tr>
            <td>
                <asp:GridView ID="gvRoles" CssClass="copy10grey" runat="server" Width="100%" AutoGenerateColumns="false">

                    <HeaderStyle CssClass="buttonlabel" />
                <Columns>
                    <asp:TemplateField HeaderText="Roles"   HeaderStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="lblRoles" CssClass="copy10grey" runat="server" Text='<%# Eval("RoleName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                </asp:GridView>
            </td>
        </tr>
        </table>
    </div>
    </form>
</body>
</html>
