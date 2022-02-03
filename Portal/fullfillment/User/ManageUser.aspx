<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageUser.aspx.cs" Inherits="avii.fullfillment.User.ManageUser" %>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="/admin/admhead.ascx" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="./../../Controls/Footer.ascx" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>.:: Lan Global ::.</title>

    <link href="./../../aerostyle.css" rel="stylesheet" type="text/css">
    <script language="javascript" src="./../../avI.js" type="text/javascript"></script>
</head>
<body leftmargin="0" topmargin="0" marginwidth="0" marginheight="0" onkeydown="KeyDownHandler(document.all.btnlogon)">
    <form id="form1" runat="server" method="post" >
        <TABLE cellSpacing="0" cellPadding="0" align="center" border="0">
        	<tr>
				<td><head:menuheader id="MenuHeader1" runat="server"></head:menuheader></td>
			</tr>
			<tr><td>&nbsp;</td></tr>
			<TR>
				<TD  bgcolor="#dee7f6" class="button">Manage Users</TD>
			</TR>
            <tr><td>&nbsp;</td></tr>			
			<tr><td>
                        
                        <table style="width: 100%">
                            <tr>
                                <td>
                                    <asp:Label ID="lblMsg" runat="server" CssClass="errormessage" ></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:GridView ID="gvUserComments" runat="server" AutoGenerateColumns="False" BorderWidth="1px"
                                        CssClass="textblack" Width="100%" BorderColor="#989898" CellPadding="4" DataKeyNames="UserID"
                                        OnRowDataBound="gvUserComments_RowDataBound" OnRowCommand="gvUserComments_RowCommand">
                                        <Columns>
                                            <asp:BoundField DataField="Username" ItemStyle-CssClass="copy11"   HeaderText="Username" />
                                            <asp:BoundField DataField="Email" ItemStyle-CssClass="copy11"   HeaderText="Email" />
                                            <asp:BoundField DataField="CompanyName" ItemStyle-CssClass="copy11"   HeaderText="Company Name" />
                                            <asp:TemplateField ShowHeader="False"  >
                                                <ItemTemplate>
                                                    <asp:HyperLink runat="server" CssClass="linktop"  ID="hlnkEdit" Text="Edit" NavigateUrl='<%#DataBinder.Eval(Container.DataItem, "UserID", "../CreateUser.aspx?UserID={0}") %>'></asp:HyperLink>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ShowHeader="False">
                                                <ItemTemplate>
                                                    <asp:LinkButton CssClass="linktop"  OnClientClick="return confirm('Are you sure you want to delete this user?');" 
                                                    runat="server" ID="lbtnDelete" Text="Delete" CommandArgument='<%#DataBinder.Eval(Container.DataItem, "UserID") %>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField Visible="false">
                                                <ItemTemplate>
                                                    <asp:HyperLink runat="server" ID="hlnkFile" CssClass="linktop" Text="View Files" NavigateUrl='<%#DataBinder.Eval(Container.DataItem, "UserID", "Manageuploads.aspx?UserID={0}") %>'></asp:HyperLink>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <RowStyle HorizontalAlign="Center" />
                                        <HeaderStyle BackColor="#989898" CssClass="copy11whb" />
                                        <AlternatingRowStyle BackColor="#EAEAEA" />
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">&nbsp;
                                </td>
                            </tr>

                            <tr>
                                <td colspan="2" align=center ><asp:Button ID="btnCreate" runat="server" CssClass="button" Text="Create New User" OnClick="btnCreate_Click" />
                                </td>
                            </tr>
                            
                            <tr>
                                <td colspan="2">&nbsp;
                                </td>
                            </tr>
                        </table>
                </td>
            </tr>
				<tr>
					<td>
                        &nbsp;</td>
				</tr>
				<tr>
					<td>
						</td>
				</tr>
        </table>
    </form>
</body>
</html>

