<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="esnlist.aspx.cs" Inherits="avii.Admin.esnlist" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Purchase Order ESN List</title>
		<link href="../aerostyle.css" rel="stylesheet" type="text/css">
</head>
<body>
    <form id="form1" runat="server">
        <asp:Button ID="btnDelete" runat="server" Text="Delete ESNs" CssClass="button" OnClick="btnDelete_Click" />
        <asp:GridView ID="grdView" runat="server" AllowPaging="false" AutoGenerateColumns="false" Width="60%"  DataKeyNames="esn" >
        <RowStyle BackColor="Gainsboro" />
            <AlternatingRowStyle BackColor="white" />
            <HeaderStyle  CssClass="button"/>
            <FooterStyle CssClass="white"  />
            <Columns>
                <asp:TemplateField ItemStyle-CssClass="copy10grey"  ItemStyle-Width="3%">
                    <ItemTemplate>
                        <asp:CheckBox ID="chk"  runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField ItemStyle-CssClass="copy10grey" DataField="ESN" HeaderText="ESN" />
                <asp:BoundField  ItemStyle-CssClass="copy10grey" DataField="PO_NUM" HeaderText="PurchaseOrder#" />
            </Columns>
        </asp:GridView>
    </form>
</body>
</html>
