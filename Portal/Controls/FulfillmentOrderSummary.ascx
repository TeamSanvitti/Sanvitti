<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FulfillmentOrderSummary.ascx.cs" Inherits="avii.Controls.FuolfillmentOrderSummary" %>
<table width="100%" cellpadding="0" cellspacing="0">
<tr>
    <td align="right">
        <asp:Label ID="lblCount" CssClass="copy10grey" runat="server" ></asp:Label>
    </td>
</tr>
<tr>
    <td >

<asp:GridView ID="gvSKU" OnPageIndexChanging="gridView_PageIndexChanging"    AutoGenerateColumns="false"  
Width="100%" ShowHeader="true"  ShowFooter="false" runat="server" GridLines="Both" 
PageSize="50" AllowPaging="true" AllowSorting="true" OnSorting="gvSKU_Sorting" 
>
<RowStyle BackColor="Gainsboro" />
<AlternatingRowStyle BackColor="white" />
<HeaderStyle  CssClass="buttonlabel" ForeColor="white"/>
  <PagerStyle ForeColor="#636363" CssClass="copy10grey" HorizontalAlign="Left" />
  <Columns>
  <asp:TemplateField HeaderText="S.No." ItemStyle-CssClass="copy10grey"  ItemStyle-Width="2%">
            <ItemTemplate>

                 <%# Container.DataItemIndex + 1 %> 
            </ItemTemplate>
        </asp:TemplateField>                 
        
        <asp:TemplateField HeaderText="SKU"  SortExpression="SKU" HeaderStyle-CssClass="buttonundlinelabel" ItemStyle-CssClass="copy10grey"  ItemStyle-Width="26%"  ItemStyle-HorizontalAlign="Left">
            <ItemTemplate>


                 <%# Eval("SKU")%> <%# Convert.ToInt32(Eval("IsAdmin")) == 0 ? "(" +  Eval("UnusedESN") + ")": "" %> 

            </ItemTemplate>
        </asp:TemplateField>                 
                
            
        <asp:TemplateField HeaderText="Pending" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
            <ItemTemplate>
               <%--<a class="copy10grey" href='FulfillmentsStatusReport.aspx?pos=<%# Eval("Pending")%>&t=<% =TimeInterval%>&cid=<%# Eval("CompanyID")%>'> --%>
            <%# Convert.ToString(Eval("Pending")) == "0" ? "" : Convert.ToString(Eval("Pending"))%>   
            <%--</a>--%>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="In Process" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
            <ItemTemplate>
                
            <%--<%# Eval("InProcess")%>   --%>
            <%--<a class="copy10grey" href='FulfillmentsStatusReport.aspx?pos=<%# Eval("InProcess")%>&t=<% =TimeInterval%>&cid=<%# Eval("CompanyID")%>'> --%>
            <%# Convert.ToString(Eval("InProcess")) == "0" ? "" : Convert.ToString(Eval("InProcess"))%>
            <%--</a>--%>

            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Processed" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
            <ItemTemplate>
                
            <%--<%# Eval("Processed")%>   --%>
            <%--<a class="copy10grey" href='FulfillmentsStatusReport.aspx?pos=<%# Eval("Processed")%>&t=<% =TimeInterval%>&cid=<%# Eval("CompanyID")%>'> --%>
            <%# Convert.ToString(Eval("Processed")) == "0" ? "" : Convert.ToString(Eval("Processed"))%>
            <%--</a>   --%>

            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Shipped" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
            <ItemTemplate>
                
            <%--<%# Eval("Shipped")%>   --%>
            <%--<a class="copy10grey" href='FulfillmentsStatusReport.aspx?pos=<%# Eval("Shipped")%>&t=<% =TimeInterval%>&cid=<%# Eval("CompanyID")%>'> --%>
            <%# Convert.ToString(Eval("Shipped")) == "0" ? "" : Convert.ToString(Eval("Shipped"))%>   
            <%--</a>--%>


            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Closed" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
            <ItemTemplate>
                
            <%--<%# Eval("Closed")%>   --%>
            <%--<a class="copy10grey" href='FulfillmentsStatusReport.aspx?pos=<%# Eval("Closed")%>&t=<% =TimeInterval%>&cid=<%# Eval("CompanyID")%>'> --%>
            
            <%# Convert.ToString(Eval("Closed")) == "0" ? "" : Convert.ToString(Eval("Closed"))%>   
            <%--</a>--%>

            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Return" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
            <ItemTemplate>
                
            <%--<%# Eval("Return")%>   --%>
            <%--<a class="copy10grey" href='FulfillmentsStatusReport.aspx?pos=<%# Eval("Return")%>&t=<% =TimeInterval%>&cid=<%# Eval("CompanyID")%>'> 
            --%>
            <%# Convert.ToString(Eval("Return")) == "0" ? "" : Convert.ToString(Eval("Return"))%>   
            <%--</a>--%>

            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="On Hold" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="8%">
            <ItemTemplate>
                
            <%--<%# Eval("OnHold")%>   --%>
            <%--<a class="copy10grey" href='FulfillmentsStatusReport.aspx?pos=<%# Eval("OnHold")%>&t=<% =TimeInterval%>&cid=<%# Eval("CompanyID")%>'> 
            --%>
            <%# Convert.ToString(Eval("OnHold")) == "0" ? "" : Convert.ToString(Eval("OnHold"))%>   
            <%--</a>--%>

            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Out of Stock" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="12%">
            <ItemTemplate>
                
            <%--<%# Eval("OutofStock")%> --%>  
            <%--<a class="copy10grey" href='FulfillmentsStatusReport.aspx?pos=<%# Eval("OutofStock")%>&t=<% =TimeInterval%>&cid=<%# Eval("CompanyID")%>'> --%>
            
            <%# Convert.ToString(Eval("OutofStock")) == "0" ? "" : Convert.ToString(Eval("OutofStock"))%>   
            <%--</a>--%>

            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Cancel" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
            <ItemTemplate>
                
            <%--<%# Eval("Cancel")%>   --%>
           <%-- <a class="copy10grey" href='FulfillmentsStatusReport.aspx?pos=<%# Eval("Cancel")%>&t=<% =TimeInterval%>&cid=<%# Eval("CompanyID")%>'> 
           --%> 
            <%# Convert.ToString(Eval("Cancel")) == "0" ? "" : Convert.ToString(Eval("Cancel"))%>   
            <%--</a>--%>

            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Partial Processed" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="12%">
            <ItemTemplate>
                
            <%--<%# Eval("PartialProcessed")%>   --%>
            <%--<a class="copy10grey" href='FulfillmentsStatusReport.aspx?pos=<%# Eval("PartialProcessed")%>&t=<% =TimeInterval%>&cid=<%# Eval("CompanyID")%>'> 
            --%>
            <%# Convert.ToString(Eval("PartialProcessed")) == "0" ? "" : Convert.ToString(Eval("PartialProcessed"))%>   
            <%--</a>--%>


            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
<asp:Label ID="lblSKU" runat="server" CssClass="errormessage"></asp:Label>


    </td>
</tr>

</table>