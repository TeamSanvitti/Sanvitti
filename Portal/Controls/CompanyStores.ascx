<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CompanyStores.ascx.cs" Inherits="avii.Controls.CompanyStores" %>

<table cellpadding="0" cellspacing="0" width="95%">
    <tr>
        <td>
            <asp:Label ID="lblMsg" CssClass="errormessage" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Repeater ID="rptStore" runat="server">
                <HeaderTemplate>
                    <table  cellSpacing="0" cellPadding="2" width="100%" align="center" >
                         <tr bordercolor="#839abf">
                    <td class="button" >
                    Store ID
                    </td>
                    <td class="button" >
                    Store Name
                    </td>
                    <td class="button">
                    Address
                    </td>
                    <td class="button">
                    City
                    </td>
                    <td class="button">
                    State
                    </td>
                    <td class="button">
                    Country
                    </td>
                    <td class="button">
                    Zip
                    </td>
                    <td class="button">
                    Active
                    </td>
               </tr>
               </HeaderTemplate>
                <ItemTemplate>
                        <tr bgcolor ="Gainsboro">
                            <td  >
                                <asp:Label ID="txtStoreID" MaxLength="20" CssClass="copy10grey" Text='<%# Eval("storeID") %>' runat="server"></asp:Label>
                            </td>
                            <td >
                                <asp:Label ID="txtStoreName" MaxLength="20" CssClass="copy10grey" Text='<%# Eval("storeName") %>' runat="server"></asp:Label>
                            </td>
                            <td >
                                <asp:Label ID="txtAddress" MaxLength="50" CssClass="copy10grey" Text='<%# Eval("StoreAddress.address1") %>' runat="server"></asp:Label>
                            </td>
                            <td >
                                <asp:Label ID="txtCity" MaxLength="50" CssClass="copy10grey" Text='<%# Eval("StoreAddress.city") %>'  runat="server"></asp:Label>
                            </td>
                            <td >
                                <asp:Label ID="txtState" MaxLength="2" CssClass="copy10grey" Text='<%# Eval("StoreAddress.state") %>'  runat="server"></asp:Label>
                            </td>
                            <td >
                                <asp:Label ID="txtcountry1" MaxLength="50" CssClass="copy10grey" Text='<%# Eval("StoreAddress.country") %>'  runat="server"></asp:Label>
                            </td>
                            <td >
                                <asp:Label ID="txtZip" Width="60" MaxLength="5" CssClass="copy10grey" Text='<%# Eval("StoreAddress.zip") %>'  runat="server"></asp:Label>
                            </td>
                            <td >
                                <asp:CheckBox ID="chkActive1" Enabled="false" Checked='<%# Eval("active") %>' runat="server" />
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
