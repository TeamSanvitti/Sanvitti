<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WarehouseCode.ascx.cs" Inherits="avii.Controls.WarehouseCode" %>
<table width="100%">
                        
                        
                        <tr>
                            <td >
                                <asp:GridView runat="server" ID="gvWarehouse" AutoGenerateColumns="False" 
                                 PageSize="50" AllowPaging="false" Width="100%"  
                                 CellPadding="3" 
                                GridLines="Vertical" DataKeyNames="WarehouseCodeGUID"  >
                                <RowStyle  CssClass="copy10grey" BackColor="Gainsboro" />
                                <AlternatingRowStyle BackColor="white" />
                                <HeaderStyle  CssClass="button"   />
                                 <PagerStyle ForeColor="#636363" CssClass="copy10grey" />
                                <%-- <FooterStyle CssClass="white"  />--%>
                                <Columns>
                
                
                                    <asp:BoundField DataField="warehouseCode" HeaderText="Warehouse Code" ItemStyle-HorizontalAlign="Left"  HeaderStyle-HorizontalAlign="Left"  />
                                    <%--<asp:BoundField DataField="CompanyName" ItemStyle-Width="40%"  HeaderStyle-HorizontalAlign="Left" HeaderText="Company Name" />
                                    --%>
                                    <asp:TemplateField ItemStyle-Width="70" HeaderText="Status" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <%# Convert.ToBoolean(Eval("active")) == true? "Active": "Inactive" %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
        
                                    </Columns>
                                </asp:GridView>
                                <asp:Label ID="lblMessage" runat="server" CssClass="errormessage" ></asp:Label>
                            </td>
                        </tr>
                        </table>
            