<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerStores.ascx.cs" Inherits="avii.Controls.CustomerStores" %>
<asp:Label ID="lblStore" runat="server" CssClass="errormessage" ></asp:Label>
                            <asp:GridView ID="gvStores"  BackColor="White" Width="100%" Visible="true" 
                            AutoGenerateColumns="false" Font-Names="Verdana" runat="server" 
                            GridLines="Both"  
                            BorderStyle="Double" BorderColor="#0083C1">
                            <RowStyle BackColor="Gainsboro" />
                            <AlternatingRowStyle BackColor="white" />
                            <HeaderStyle  CssClass="button" ForeColor="white"/>
                            <FooterStyle CssClass="white"  />
                            <Columns>
                                <asp:TemplateField HeaderText="StoreID"  HeaderStyle-HorizontalAlign="Left" 
                                ItemStyle-CssClass="copy10grey"  ItemStyle-Width="15%"  ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate><%# Eval("StoreID")%></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Store Name"  HeaderStyle-HorizontalAlign="Left" 
                                ItemStyle-CssClass="copy10grey"  ItemStyle-Width="15%"  ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate><%# Eval("Storename")%></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Address"  HeaderStyle-HorizontalAlign="Left" 
                                ItemStyle-CssClass="copy10grey"  ItemStyle-Width="30%"  ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate><%# Eval("StoreAddress.Address1")%></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="City"  HeaderStyle-HorizontalAlign="Left" 
                                ItemStyle-CssClass="copy10grey"  ItemStyle-Width="10%"  ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate><%# Eval("StoreAddress.City")%></ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="State"  HeaderStyle-HorizontalAlign="Left" 
                                ItemStyle-CssClass="copy10grey"  ItemStyle-Width="5%"  ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate><%# Eval("StoreAddress.State")%></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Zip"  HeaderStyle-HorizontalAlign="Left" 
                                ItemStyle-CssClass="copy10grey"  ItemStyle-Width="5%"  ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate><%# Eval("StoreAddress.Zip")%></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status"  HeaderStyle-HorizontalAlign="Left" 
                                ItemStyle-CssClass="copy10grey"  ItemStyle-Width="5%"  ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                    <%# Convert.ToInt32(Eval("Active")) == 1 ? "Active" : "Deleted"%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText=""  HeaderStyle-HorizontalAlign="Left" 
                                ItemStyle-CssClass="copy10grey"  ItemStyle-Width="5%"  ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                    <asp:ImageButton ID="lnkDelete" runat="server" ImageUrl="~/images/delete.png" OnCommand="DeleteStore_click" 
                                        CommandArgument='<%# Eval("CompanyAddressID") %>' OnClientClick="return ConfirmDelete(this);"
                                            CausesValidation="false"  />
                                    <%--<asp:LinkButton ID="lnkDelete"   CssClass="copy10grey" OnCommand="DeleteStore_click" CommandArgument='<%# Eval("CompanyAddressID") %>' OnClientClick="return ConfirmDelete(this);"  runat="server">Delete</asp:LinkButton>--%>
                                                         <asp:HiddenField Value='<%# Eval("StoreFlag") %>' ID="hdnStoreFlag" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                            </asp:GridView>
                        