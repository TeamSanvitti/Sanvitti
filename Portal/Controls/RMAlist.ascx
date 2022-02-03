<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RMAlist.ascx.cs" Inherits="avii.Controls.RMAlist" %>
<table width="100%">
<tr>
    <td align="right">
        <asp:Label ID="lblCount" runat="server" CssClass="copy10grey" ></asp:Label>
    </td>
</tr>
<tr>
    <td>
        <asp:GridView runat="server" ID="gvRMA" AutoGenerateColumns="False" 
        PageSize="50" AllowPaging="true" Width="100%"  OnPageIndexChanging="gridView_PageIndexChanging"   
        CellPadding="3"  OnRowCommand="GridView1_RowCommand"
        GridLines="Vertical"   DataKeyNames="rmaGUID">
        <RowStyle  CssClass="copy10grey" BackColor="Gainsboro" />
        <AlternatingRowStyle BackColor="white" />
        <HeaderStyle  CssClass="button"   />
            <PagerStyle ForeColor="#636363" CssClass="copy10grey" />
        <%-- <FooterStyle CssClass="white"  />--%>
        <Columns>
            <asp:ButtonField   ButtonType="Image"  ImageUrl ="../images/plus.gif"
                    CommandName="sel"  />
            <%--<asp:BoundField DataField="CompanyName" HeaderText="CompanyName" HeaderStyle-HorizontalAlign="Left"/>
        --%>
            <asp:BoundField DataField="RmaNumber" ItemStyle-Width="150"   HeaderText="RMA#" />
            <asp:BoundField DataField="RmaDate" HeaderText="RmaDate" />
            <asp:TemplateField HeaderText="Status" >
            <ItemTemplate>
                <%# Eval("Status") %>
            </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Comment" HeaderText="Customer Comments"  />
            <asp:BoundField DataField="AVComments" HeaderText="AV Comments" />
        
            <asp:TemplateField  >
			        <ItemTemplate>
			            <tr>
                            <td colspan="100%">
                                <div id="div<%# Eval("rmaGUID") %>"  >
                                    <asp:GridView ID="GridView2"  BackColor="White" Width="100%" Visible="False"
                                        AutoGenerateColumns="false" Font-Names="Verdana" runat="server" DataKeyNames="rmaDetGUID"
                                         GridLines="Both"
                                        
                                        BorderStyle="Double" BorderColor="#0083C1">
                                        <RowStyle BackColor="Gainsboro" />
                                        <AlternatingRowStyle BackColor="white" />
                                        <HeaderStyle  CssClass="button" ForeColor="white"/>
                                        <FooterStyle CssClass="white"  />
                                        <Columns>
                                            <asp:TemplateField HeaderText="ESN" SortExpression="ESN" ItemStyle-CssClass="copy10grey"  ItemStyle-Width="20%">
                                                <ItemTemplate><%# "#" + Eval("ESN")%></ItemTemplate>
                                            </asp:TemplateField>
                                                                                                                                    
                                            
                                            <asp:TemplateField HeaderText="ItemCode" SortExpression="upc"  ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                                                <ItemTemplate><%#Eval("UPC")%></ItemTemplate>
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField HeaderText="CallTime" SortExpression="CallTime" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                   <%# Eval("CallTime")%>
                                                </ItemTemplate>
                                                
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField HeaderText="Reason" SortExpression="Reason"  ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                                <ItemTemplate><%#Eval("Reason")%></ItemTemplate>
                                                                                                
                                            </asp:TemplateField>
                                        
                                            <asp:TemplateField HeaderText="Status" SortExpression="Status"  ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                                <ItemTemplate><%#Eval("Status")%></ItemTemplate>
                                                                                               
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
    </td>
</tr>
</table>