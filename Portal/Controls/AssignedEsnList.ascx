<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AssignedEsnList.ascx.cs" Inherits="avii.Controls.AssignedEsnList" %>
<table  border="0" cellSpacing="0" cellPadding="0" width="100%" >
    <tr >
        <td>
           <table width="100%">
            <tr>
                <td>
                    <asp:GridView Width="100%" ID="grdSummary" runat="server" AllowPaging="false"  PageSize="50"
                    AllowSorting="false" AutoGenerateColumns="false"  OnPageIndexChanging="grdSummary_PageIndexChanging">
                        <RowStyle BackColor="Gainsboro" />
                        <AlternatingRowStyle BackColor="white" />
                        <HeaderStyle  CssClass="button" ForeColor="white"/>
                        <FooterStyle CssClass="white"  />
                        <PagerStyle ForeColor="#636363" Font-Size="Small" CssClass="copy10grey" />
                        <Columns>
                            <asp:TemplateField HeaderText="Item Code" SortExpression="itemcode" HeaderStyle-HorizontalAlign="Left"  ItemStyle-CssClass="copy10grey" >
                                <ItemTemplate><%#Eval("ItemCode")%></ItemTemplate>
                            </asp:TemplateField>   
                            <asp:TemplateField HeaderText="Item Neme" SortExpression="itemName" HeaderStyle-HorizontalAlign="Left"  ItemStyle-CssClass="copy10grey" >
                                <ItemTemplate><%#Eval("ItemName")%></ItemTemplate>
                            </asp:TemplateField>   
                            <asp:TemplateField HeaderText="UPC" SortExpression="UPC"  ItemStyle-CssClass="copy10grey" >
                                <ItemTemplate><%#Eval("UPC")%></ItemTemplate>
                            </asp:TemplateField>   
                            <asp:TemplateField HeaderText="Total ESN" SortExpression="TotalESN"   ItemStyle-CssClass="copy10grey"  ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                <%# Eval("TotalESN")%>

                                

                                </ItemTemplate>
                            </asp:TemplateField>   
                            <asp:TemplateField HeaderText="Used ESN(PO)" SortExpression="UsedESN"  ItemStyle-CssClass="copy10grey"  ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                <%# Eval("UsedESN")%>
                                
                                </ItemTemplate>
                            </asp:TemplateField>   
                            <%--<asp:TemplateField HeaderText="Used ESN(RMA)" SortExpression="RmaESNCount"  ItemStyle-CssClass="copy10grey" ItemStyle-HorizontalAlign="Right" >
                                <ItemTemplate><%#Eval("RmaESNCount")%></ItemTemplate>
                            </asp:TemplateField>   --%>
                        </Columns>
                    </asp:GridView>        
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblMsg" runat="server" CssClass="errormessage"></asp:Label></td>
            </tr>
        </table>
        </td>
    </tr>
</table>