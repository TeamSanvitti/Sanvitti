<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FulfillmentStatus.ascx.cs" Inherits="avii.Controls.FulfillmentStatuses" %>
<table width="100%" cellpadding="0" cellspacing="0">
<tr>
    <td align="right">
        <asp:Label ID="lblCount" CssClass="copy10grey" runat="server" ></asp:Label>
    </td>
</tr>
<tr>
    <td >
    
<asp:GridView ID="gvPO" OnPageIndexChanging="gridView_PageIndexChanging"    AutoGenerateColumns="false"  
Width="100%" ShowHeader="true"  ShowFooter="false" runat="server" GridLines="Both" 
PageSize="50" AllowPaging="true" AllowSorting="false" 
>
<RowStyle BackColor="Gainsboro" />
<AlternatingRowStyle BackColor="white" />
<HeaderStyle  CssClass="button" ForeColor="white"/>
  <PagerStyle ForeColor="#636363" CssClass="copy10grey" HorizontalAlign="Left" />
  <Columns>
        <asp:TemplateField HeaderText="S.No." ItemStyle-CssClass="copy10grey"  ItemStyle-Width="2%">
            <ItemTemplate>

                  <%# Convert.ToString(Eval("CustomerName")) == "zzTotal" ? "" : Convert.ToString(Container.DataItemIndex + 1)%>
            </ItemTemplate>
        </asp:TemplateField>                 
        
        <asp:TemplateField HeaderText="Customer Name" ItemStyle-CssClass="copy10grey"  ItemStyle-Width="25%" ItemStyle-HorizontalAlign="Left" >
            <ItemTemplate>
             <div style="width:100%;  font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
            
            <%# Convert.ToString(Eval("CustomerName")) == "zzTotal" ? "Total" : Eval("CustomerName")%>
                 </div>
           
                 <%--<%# Eval("CustomerName")%>--%> 
            </ItemTemplate>
        </asp:TemplateField>                 
                
        <asp:TemplateField HeaderText="Pending" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
            <ItemTemplate>
             <div style="width:100%;  font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
            &nbsp;

               <%--<a class="copy10grey" href='FulfillmentsStatusReport.aspx?pos=<%# Eval("Pending")%>&t=<% =TimeInterval%>&cid=<%# Eval("CompanyID")%>'> --%>
            <%# Convert.ToString(Eval("Pending")) == "0" ? "" : Convert.ToString(Eval("Pending"))%>   
            <%--</a>--%>
            
            </div>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="In Process" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
            <ItemTemplate>
             <div style="width:100%;  font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
                &nbsp;

            <%--<%# Eval("InProcess")%>   --%>
            <%--<a class="copy10grey" href='FulfillmentsStatusReport.aspx?pos=<%# Eval("InProcess")%>&t=<% =TimeInterval%>&cid=<%# Eval("CompanyID")%>'> --%>
            <%# Convert.ToString(Eval("InProcess")) == "0" ? "" : Convert.ToString(Eval("InProcess"))%>
            <%--</a>--%>
            
            </div>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Processed" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
            <ItemTemplate>
             <div style="width:100%;  font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
            &nbsp;    
            <%--<%# Eval("Processed")%>   --%>
            <%--<a class="copy10grey" href='FulfillmentsStatusReport.aspx?pos=<%# Eval("Processed")%>&t=<% =TimeInterval%>&cid=<%# Eval("CompanyID")%>'> --%>
            <%# Convert.ToString(Eval("Processed")) == "0" ? "" : Convert.ToString(Eval("Processed"))%>
            <%--</a>   --%>
            
            </div>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Shipped" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
            <ItemTemplate>
             <div style="width:100%;  font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
            &nbsp;   
            <%--<%# Eval("Shipped")%>   --%>
            <%--<a class="copy10grey" href='FulfillmentsStatusReport.aspx?pos=<%# Eval("Shipped")%>&t=<% =TimeInterval%>&cid=<%# Eval("CompanyID")%>'> --%>
            <%# Convert.ToString(Eval("Shipped")) == "0" ? "" : Convert.ToString(Eval("Shipped"))%>   
            <%--</a>--%>
            
            </div>

            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Closed" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
            <ItemTemplate>
                <div style="width:100%;  font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
            &nbsp;
            <%--<%# Eval("Closed")%>   --%>
            <%--<a class="copy10grey" href='FulfillmentsStatusReport.aspx?pos=<%# Eval("Closed")%>&t=<% =TimeInterval%>&cid=<%# Eval("CompanyID")%>'> --%>
            
            <%# Convert.ToString(Eval("Closed")) == "0" ? "" : Convert.ToString(Eval("Closed"))%>   
            <%--</a>--%>
            
            </div>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Return" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
            <ItemTemplate>
                <div style="width:100%;  font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
            &nbsp;
            <%--<%# Eval("Return")%>   --%>
            <%--<a class="copy10grey" href='FulfillmentsStatusReport.aspx?pos=<%# Eval("Return")%>&t=<% =TimeInterval%>&cid=<%# Eval("CompanyID")%>'> 
            --%>
            <%# Convert.ToString(Eval("Return")) == "0" ? "" : Convert.ToString(Eval("Return"))%>   
            <%--</a>--%>
            
            </div>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="On Hold" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="8%">
            <ItemTemplate>
                <div style="width:100%;  font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
            &nbsp;
            <%--<%# Eval("OnHold")%>   --%>
            <%--<a class="copy10grey" href='FulfillmentsStatusReport.aspx?pos=<%# Eval("OnHold")%>&t=<% =TimeInterval%>&cid=<%# Eval("CompanyID")%>'> 
            --%>
            <%# Convert.ToString(Eval("OnHold")) == "0" ? "" : Convert.ToString(Eval("OnHold"))%>   
            <%--</a>--%>
            
            </div>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Out of Stock" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="12%">
            <ItemTemplate>
                <div style="width:100%;  font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
            &nbsp;
            <%--<%# Eval("OutofStock")%> --%>  
            <%--<a class="copy10grey" href='FulfillmentsStatusReport.aspx?pos=<%# Eval("OutofStock")%>&t=<% =TimeInterval%>&cid=<%# Eval("CompanyID")%>'> --%>
            
            <%# Convert.ToString(Eval("OutofStock")) == "0" ? "" : Convert.ToString(Eval("OutofStock"))%>   
            <%--</a>--%>
            
            </div>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Cancel" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
            <ItemTemplate>
                <div style="width:100%;  font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
            &nbsp;
            <%--<%# Eval("Cancel")%>   --%>
           <%-- <a class="copy10grey" href='FulfillmentsStatusReport.aspx?pos=<%# Eval("Cancel")%>&t=<% =TimeInterval%>&cid=<%# Eval("CompanyID")%>'> 
           --%> 
            <%# Convert.ToString(Eval("Cancel")) == "0" ? "" : Convert.ToString(Eval("Cancel"))%>   
            <%--</a>--%>
            
            </div>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Partial Processed" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="13%">
            <ItemTemplate>
                <div style="width:100%;  font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
            &nbsp;
            <%--<%# Eval("PartialProcessed")%>   --%>
            <%--<a class="copy10grey" href='FulfillmentsStatusReport.aspx?pos=<%# Eval("PartialProcessed")%>&t=<% =TimeInterval%>&cid=<%# Eval("CompanyID")%>'> 
            --%>
            <%# Convert.ToString(Eval("PartialProcessed")) == "0" ? "" : Convert.ToString(Eval("PartialProcessed"))%>   
            <%--</a>--%>
            </div>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Total" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ControlStyle-BackColor="Yellow" ItemStyle-Width="5%">
            <ItemTemplate>
                <div style="width:100%;  font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:yellow; height:20px">
            &nbsp;
           <b> <%# Eval("Total")%>   </b>
            <%--<a class="copy10grey" href='FulfillmentsStatusReport.aspx?pos=<%# Eval("PartialProcessed")%>&t=<% =TimeInterval%>&cid=<%# Eval("CompanyID")%>'> 
            --%>
            <%--<%# Convert.ToString(Eval("HardwareIssues")) == "0" ? "" : Convert.ToString(Eval("HardwareIssues"))%>   --%>
            <%--</a>--%>
            </div>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
<asp:Label ID="lblPO" runat="server" CssClass="errormessage"></asp:Label>

    </td>
</tr>

</table>