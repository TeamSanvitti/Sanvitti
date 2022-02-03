<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RMADetails.ascx.cs" Inherits="avii.Controls.RMADetails" %>
<table width="98%" align="center" cellpadding="0" cellspacing="0">
<tr>
    <td>
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
                        <asp:Label ID="lblRMA" CssClass="copy10grey" runat="server" ></asp:Label>
                    </td>
                    <td class="copy10grey" width="15%" align="right">
                        RMA Date:
                    </td>
                    <td width="1%">&nbsp;</td>
                    <td width="33%" align="left">
                        <asp:Label ID="lblRMADate" CssClass="copy10grey" runat="server" ></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="copy10grey" width="25%" align="right">
                        Status:
                    </td>
                    <td width="1%">&nbsp;</td>
                    <td width="24%" align="left">
                        <asp:Label ID="lblStatus" CssClass="copy10grey" runat="server" ></asp:Label>
                    </td>
                    <td class="copy10grey" width="15%" align="right">
                        Company Name:
                    </td>
                    <td width="1%">&nbsp;</td>
                    <td width="33%" align="left">
                        <asp:Label ID="lblCompanyName" CssClass="copy10grey" runat="server" ></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="copy10grey" width="25%" align="right">
                    Customer Comment:
                    </td>
                    <td width="1%">&nbsp;</td>
                    <td  align="left" colspan="5">
                        <asp:Label ID="lblComment" CssClass="copy10grey" runat="server" ></asp:Label>
                    </td>
                </tr>   
                <tr>
                    <td class="copy10grey" width="25%" align="right">
                    Admin Comment:
                    </td>
                    <td width="1%">&nbsp;</td>
                    <td  colspan="5" align="left">
                        <asp:Label ID="lblAVComment" CssClass="copy10grey" runat="server" ></asp:Label>
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
        <asp:GridView ID="gvRMA"  BackColor="White" Width="100%" Visible="true"
        AutoGenerateColumns="false" Font-Names="Verdana" runat="server" DataKeyNames="rmaguID"
        GridLines="Both" OnRowDataBound="GridView1_RowDataBound"
        BorderStyle="Double" BorderColor="#0083C1">
        <RowStyle BackColor="Gainsboro" />
        <AlternatingRowStyle BackColor="white" />
        <HeaderStyle  CssClass="buttonlabel" ForeColor="white"/>
        <FooterStyle CssClass="white"  />
        <Columns>
            <asp:TemplateField HeaderText="ESN#"  ItemStyle-CssClass="copy10grey"  ItemStyle-Width="20%">
                <ItemTemplate><%# Eval("ESN")%></ItemTemplate>
            </asp:TemplateField>
                                                                                                                                    
                                            
            <asp:TemplateField HeaderText="SKU#"   ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                <ItemTemplate><%#Eval("ItemCode")%></ItemTemplate>
            </asp:TemplateField>
<asp:TemplateField HeaderText="NEWSKU#"   ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                <ItemTemplate><%#Eval("NEWSKU")%></ItemTemplate>
            </asp:TemplateField>
            
                                            
            <asp:TemplateField HeaderText="CallTime"  ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                <ItemTemplate>
                    <%# Eval("CallTime") %>
                </ItemTemplate>
                                                
            </asp:TemplateField>
                                            
            <asp:TemplateField HeaderText="Reason"   ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                <ItemTemplate>
                <asp:HiddenField ID="hdnReason" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "reason")%>' />
                                                <asp:Label ID="lblreason" runat="server" ></asp:Label>
                </ItemTemplate>
                                                               
            </asp:TemplateField>
                                        
            <asp:TemplateField HeaderText="Status" SortExpression="Status"  ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                <ItemTemplate><%#Eval("Status")%></ItemTemplate>
                                                                                                 
            </asp:TemplateField>
                                            

                                                              
			                                
        </Columns>
    </asp:GridView>
    </td>
</tr>
</table>














