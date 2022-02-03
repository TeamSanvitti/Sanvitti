<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="POList.ascx.cs" Inherits="avii.Controls.POList" %>

<table>
<tr><td Class="copy10grey"><asp:Label ID="lbCount" CssClass="copyblue11b" runat="server"></asp:Label></td></tr>
    <tr>
        <td>
      <asp:GridView ID="grdPO"   AutoGenerateColumns="false"  OnRowCommand = "GridView1_RowCommand" 
                DataKeyNames="PurchaseOrderID"  Width="100%" ShowFooter="false" runat="server" GridLines="Both"  PageSize="50" AllowPaging="false"
            AllowSorting="false"  > 
            <RowStyle BackColor="Gainsboro" />
            <AlternatingRowStyle BackColor="white" />
            <HeaderStyle  CssClass="button" ForeColor="white"/>
            <FooterStyle CssClass="white"  />
            <Columns>
             <asp:ButtonField   ButtonType="Image"  ImageUrl ="../images/plus.gif"
                    CommandName="sel"  />              

                <asp:TemplateField HeaderText="Lan Global Order#" SortExpression="AerovoiceSalesOrderNumber"  ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                    <ItemTemplate><%#Eval("AerovoiceSalesOrderNumber")%></ItemTemplate>
                </asp:TemplateField>   
                                
                <asp:TemplateField HeaderText="PO#" SortExpression="PurchaseOrderNumber" ItemStyle-CssClass="copy10grey" ItemStyle-Width="6%">
                    <ItemTemplate>
                        <asp:Label ID="lblPoNum" runat="server" Text='<%# Eval("PurchaseOrderNumber") %>'></asp:Label>                        
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="PO Date"  SortExpression="PurchaseOrderDate" ItemStyle-CssClass="copy10grey"  ItemStyle-Width="11%">
                    <ItemTemplate><%# DataBinder.Eval(Container.DataItem, "PurchaseOrderDate", "{0:d}") %></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Contact Name" SortExpression="ContactName" ItemStyle-CssClass="copy10grey">
                    <ItemTemplate><%# Eval("Shipping.ContactName")%></ItemTemplate>

                </asp:TemplateField>
                <asp:TemplateField HeaderText="Ship Attn" SortExpression="ShipToAttn" ItemStyle-CssClass="copy10grey">
                    <ItemTemplate><%# Eval("Shipping.ShipToAttn") %></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Shippping" SortExpression="ShipToBy" ItemStyle-CssClass="copy10grey">
                    <ItemTemplate><%# Eval("Tracking.ShipToBy")%></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Tracking#" SortExpression="ShipToTrackingNumber" ItemStyle-CssClass="copy10grey">
                    <ItemTemplate><%# Eval("Tracking.ShipToTrackingNumber")%></ItemTemplate>
                </asp:TemplateField>   
                                
                <asp:TemplateField HeaderText="Store ID" SortExpression="StoreID"  ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                    <ItemTemplate><%#Eval("StoreID")%></ItemTemplate>
                </asp:TemplateField> 

                <asp:TemplateField HeaderText="Street Address" SortExpression="ShipTo_Address"  ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                    <ItemTemplate><%#Eval("Shipping.ShipToAddress")%></ItemTemplate>
                </asp:TemplateField> 
                <asp:TemplateField HeaderText="State"  ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                    <ItemTemplate><%#Eval("Shipping.ShipToState")%></ItemTemplate>
                </asp:TemplateField>                                              
                <asp:TemplateField HeaderText="Zip Code"  ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                    <ItemTemplate><%#Eval("Shipping.ShipToZip")%></ItemTemplate>
                </asp:TemplateField>                                              


                <asp:TemplateField HeaderText="Status"  ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                    <ItemTemplate><%#Eval("PurchaseOrderStatus")%></ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="Sent ESN" SortExpression="sentesn"  ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                    <ItemTemplate><%#Eval("sentesn")%></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Sent ASN" SortExpression="sentesn"  ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                    <ItemTemplate><%#Eval("sentasn ")%></ItemTemplate>
                </asp:TemplateField>	                    
                <asp:TemplateField  >
			        <ItemTemplate>
			            <tr>
                            <td colspan="100%">
                                <div id="div<%# Eval("PurchaseOrderID") %>"  >
                                    <asp:GridView ID="GridView2"  BackColor="White" Width="100%" Visible="False"
                                        AutoGenerateColumns="false" Font-Names="Verdana" runat="server" DataKeyNames="PodID"
                                         GridLines="Both"
                                        BorderStyle="Double" BorderColor="#0083C1">
                                        <RowStyle BackColor="Gainsboro" />
                                        <AlternatingRowStyle BackColor="white" />
                                        <HeaderStyle  CssClass="button" ForeColor="white"/>
                                        <FooterStyle CssClass="white"  />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Item Code" SortExpression="Item_Code" ItemStyle-CssClass="copy10grey"  ItemStyle-Width="20%">
                                                <ItemTemplate><asp:Label ID="lblItemCode" runat="server" Text='<%# Eval("ItemCode")%>'></asp:Label></ItemTemplate>
                                            </asp:TemplateField>
                                                                                                                                    
                                            
                                            <asp:TemplateField HeaderText="Qty" SortExpression="Qty"  ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                                                <ItemTemplate><%#Eval("Quantity")%></ItemTemplate>
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField HeaderText="ESN" SortExpression="ESN" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEsn" CssClass="copy10grey" Text='<%# Eval("ESN") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtEsn" CssClass="copy10grey" MaxLength="50"  Text='<%# Eval("ESN") %>' runat="server"></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField HeaderText="MDN" SortExpression="MdnNumber"  ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                                <ItemTemplate><%#Eval("MdnNumber")%></ItemTemplate>
                                            </asp:TemplateField>
                                        
                                            <asp:TemplateField HeaderText="MSID" SortExpression="MsID"  ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                                <ItemTemplate><%#Eval("MsID")%></ItemTemplate>
                                            </asp:TemplateField>
                                            

                                            <asp:TemplateField HeaderText="MslNumber" SortExpression="MslNumber" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMslNumber" CssClass="copy10grey" Text='<%# Eval("MslNumber") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtMslNumber" CssClass="copy10grey" MaxLength="30"  Text='<%# Eval("MslNumber") %>' runat="server"></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField HeaderText="Passcode" SortExpression="PassCode"  ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                                <ItemTemplate><%#Eval("PassCode")%></ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="UPC" SortExpression="UPC"  ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                                <ItemTemplate><%#Eval("UPC")%></ItemTemplate>
                                            </asp:TemplateField>
                                            
                                             <asp:TemplateField HeaderText="FM-UPC" SortExpression="FMUPC" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFMUPC" CssClass="copy10grey" Text='<%# Eval("FMUPC") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtFMUPC" CssClass="copy10grey" MaxLength="50"  Text='<%# Eval("FMUPC") %>' runat="server"></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                                                                                                                        
                                            <asp:TemplateField HeaderText="Phone Type" SortExpression="PhoneCategory"  ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                                <ItemTemplate><%#Eval("PhoneCategory")%></ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                   </asp:GridView>
                                </div>
                             </td>
                        </tr>
			        </ItemTemplate>			       
			    </asp:TemplateField>			    
			</Columns>
        </asp:GridView>     
<asp:GridView ID="GridView1" runat="server">
</asp:GridView>     
</td>
    </tr>
</table>