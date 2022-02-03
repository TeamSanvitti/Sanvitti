<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EsnLogDetails.ascx.cs" Inherits="avii.Controls.EsnLogDetails" %>
 <table width="100%">
        
        <tr>
            <td>
                <asp:Label ID="lblEsn" runat="server" CssClass="errormessage" ></asp:Label>

                <asp:GridView ID="gvEsn" runat="server" AutoGenerateColumns="false" Width="100%"  
                ShowFooter="false"  GridLines="Both" 
                AllowPaging="true" PageSize="50" 
                OnPageIndexChanging="gvEsn_PageIndexChanging" > 
                <PagerStyle ForeColor="black" Font-Size="XX-Small"/>
                <RowStyle BackColor="Gainsboro" />
                <AlternatingRowStyle BackColor="white" />
                <HeaderStyle  CssClass="button" ForeColor="white"/>
                <FooterStyle CssClass="white"  />
                <Columns>  
                                                      
                    <asp:TemplateField HeaderText="ESN" SortExpression="esn" HeaderStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="25%">
                        <ItemTemplate>
                            <%# Eval("ESN")%>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Update Date" SortExpression="UpdateDate" ItemStyle-CssClass="copy10grey" ItemStyle-Width="25%">
                        <ItemTemplate>
                            <%# Eval("UpdateDate")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                                 
                    <asp:TemplateField HeaderText="Status" SortExpression="status" ItemStyle-CssClass="copy10grey" ItemStyle-Width="25%">
                        <ItemTemplate>
                            <%# Eval("Status")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Module" SortExpression="Module" ItemStyle-CssClass="copy10grey" ItemStyle-Width="25%">
                        <ItemTemplate>
                            <%# Eval("ModuleName")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                </asp:GridView>
            
            </td>
        </tr>
        </table>
