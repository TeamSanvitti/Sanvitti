<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewRMA.ascx.cs" Inherits="avii.Controls.ViewRMA" %>
 <table width="100%">
            <tr>
                 <td>
                
                <%--<div style="overflow:auto; height:350px; width:100%; border: 0px solid #839abf" >--%>
      
<table width="98%" align="center" cellpadding="0" cellspacing="0">
<tr>
    <td>
    <asp:Label ID="lblMsgDetail" runat="server" CssClass="errormessage"></asp:Label>
        <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="100%" align="center" >
           <tr bordercolor="#839abf">
                <td>
                <table width="100%" cellSpacing="3" cellPadding="3">
                <tr>
                    <td class="copy10grey" width="25%" align="right">
                        RMA#:
                    </td>
                    <td width="1%">&nbsp;</td>
                    <td width="24%" align="left">
                        <asp:Label ID="lblRMA" CssClass="copy10grey" runat="server" EnableViewState="false" ></asp:Label>
                    </td>
                    <td class="copy10grey" width="15%" align="right">
                        RMA Date:
                    </td>
                    <td width="1%">&nbsp;</td>
                    <td width="33%" align="left">
                        <asp:Label ID="lblRMADate" CssClass="copy10grey" runat="server" EnableViewState="false"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="copy10grey" width="25%" align="right">
                        Status:
                    </td>
                    <td width="1%">&nbsp;</td>
                    <td width="24%" align="left">
                        <asp:Label ID="lblStatus" CssClass="copy10grey" runat="server" EnableViewState="false" ></asp:Label>
                    </td>
                    <td class="copy10grey" width="15%" align="right">
                        Company Name:
                    </td>
                    <td width="1%">&nbsp;</td>
                    <td width="33%" align="left">
                        <asp:Label ID="lblCompanyName" CssClass="copy10grey" runat="server" EnableViewState="false"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="copy10grey" width="25%" align="right">
                    Customer Comment:
                    </td>
                    <td width="1%">&nbsp;</td>
                    <td  align="left" colspan="5">
                        <asp:Label ID="lblComment" CssClass="copy10grey" runat="server" EnableViewState="false"></asp:Label>
                    </td>
                </tr>   
                <tr>
                    <td class="copy10grey" width="25%" align="right">
                    Admin Comment:
                    </td>
                    <td width="1%">&nbsp;</td>
                    <td  colspan="5" align="left">
                        <asp:Label ID="lblAVComment" CssClass="copy10grey" runat="server" EnableViewState="false"></asp:Label>
                    </td>
                </tr>   
                </table>
                </td>
            </tr>
        </table>
    </td>
</tr>
<tr>
<td>&nbsp;</td>
</tr>
<tr>
    <td>
        <asp:GridView ID="gvRMADetail"  BackColor="White" Width="100%" Visible="true" 
        AutoGenerateColumns="false" Font-Names="Verdana" runat="server" DataKeyNames="rmaguID"
        GridLines="Both" 
        BorderStyle="Double" BorderColor="#0083C1">
        <RowStyle BackColor="Gainsboro" />
        <AlternatingRowStyle BackColor="white" />
        <HeaderStyle  CssClass="button" ForeColor="white"/>
        <FooterStyle CssClass="white"  />
        <Columns>
            <asp:TemplateField HeaderText="ESN#" SortExpression="Item_Code" HeaderStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey"  ItemStyle-Width="20%">
                <ItemTemplate><%# Eval("ESN")%></ItemTemplate>
            </asp:TemplateField>
                                                                                                                                    
                                            
            <asp:TemplateField HeaderText="ItemCode"   ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                <ItemTemplate><%#Eval("UPC")%></ItemTemplate>
            </asp:TemplateField>
                                            
            <asp:TemplateField HeaderText="CallTime" SortExpression="ESN" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                <ItemTemplate>
                    <%# Eval("CallTime") %>
                </ItemTemplate>
                                                
            </asp:TemplateField>
                                            
            <asp:TemplateField HeaderText="Reason" SortExpression="Reason"  ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                <ItemTemplate><%--
                <asp:HiddenField ID="hdnReason" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "reason")%>' />
                                                <asp:Label ID="lblreason" runat="server" ></asp:Label>--%>
                                                
                                                <%#Eval("ReasonTxt")%>
                </ItemTemplate>
                                                               
            </asp:TemplateField>
                                        
            <asp:TemplateField HeaderText="Status" SortExpression="Status"  ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                <ItemTemplate><%#Eval("Status")%></ItemTemplate>
                                                                                                 
            </asp:TemplateField>
                                            
    <asp:TemplateField ItemStyle-Width="5%" Visible="false">
                <ItemTemplate>
                        <asp:ImageButton ToolTip="Delete RMA Item"  Visible="false"  CommandArgument='<%# Eval("rmaDetGUID") %>' 
                            ImageUrl="~/Images/delete.png" ID="imgDelDetail1" OnCommand="imgDeleteRMADetail_Commnad" runat="server" />

              </ItemTemplate>
              </asp:TemplateField>                                                
			                                
        </Columns>
    </asp:GridView>
    </td>
</tr>
</table>

                    

                    
             <%--   </div>--%>
               
                </td>
            </tr>
        </table>
        