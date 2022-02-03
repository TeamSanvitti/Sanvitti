<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RmaReasonSummary.ascx.cs" Inherits="avii.Controls.RmaReasonSummary" %>
<table width="115%" cellpadding="0" cellspacing="0">
<tr>
    <td align="left">
        <asp:Label ID="lblCount" CssClass="copy10grey" runat="server" ></asp:Label>
    </td>
</tr>
<tr>
    <td >
    
<asp:GridView ID="gvRMA" OnPageIndexChanging="gridView_PageIndexChanging"    AutoGenerateColumns="false"  
Width="100%" ShowHeader="true"  ShowFooter="false" runat="server" GridLines="Both" 
PageSize="50" AllowPaging="true" AllowSorting="false"   
>
<RowStyle BackColor="Gainsboro" />
<AlternatingRowStyle BackColor="white" />
<HeaderStyle  CssClass="button" ForeColor="white"/>
  <PagerStyle ForeColor="#636363" CssClass="copy10grey" HorizontalAlign="Left" />
  <Columns>
        <asp:TemplateField HeaderText="S.No." ItemStyle-CssClass="copy10grey"    ItemStyle-Width="2%">
            <ItemTemplate>
            <div style="width:100%; background-color:<%# Convert.ToString(Eval("ProductName")) == "zzTotal"? "":"" %>; height:20px">
                 <%--<%# Container.DataItemIndex + 1 %> 
--%>
                 <%# Convert.ToString(Eval("ProductName")) == "zzTotal" ? "" : Convert.ToString(Container.DataItemIndex + 1)%>
                 </div>
            
                 <%--<%# Container.DataItemIndex + 1 %> --%>
            </ItemTemplate>
        </asp:TemplateField>                 
        
        <asp:TemplateField HeaderText="Product Name" ItemStyle-CssClass="copy10grey"  ItemStyle-Width="18%" ItemStyle-HorizontalAlign="Left">
            <ItemTemplate>
            <div style="width:100%; background-color:<%# Convert.ToString(Eval("ProductName")) == "zzTotal"? "yellow":"" %>; height:20px">
            
            <%# Convert.ToString(Eval("ProductName")) == "zzTotal" ? "Total" : Eval("ProductName")%>
                 </div>
            </ItemTemplate>
        </asp:TemplateField>                 
                
        <asp:TemplateField HeaderText="DOA" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
            <ItemTemplate>
            <div style="width:100%; background-color:<%# Convert.ToString(Eval("ProductName")) == "zzTotal"? "yellow":"" %>; height:20px">
            &nbsp;
               <%--<a class="copy10grey" href='FulfillmentsStatusReport.aspx?pos=<%# Eval("Pending")%>&t=<% =TimeInterval%>&cid=<%# Eval("CompanyID")%>'> --%>
            <%# Convert.ToString(Eval("DOA")) == "0" ? "" : Convert.ToString(Eval("DOA"))%>   
            <%--</a>--%>
            </div>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Audio Issues" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
            <ItemTemplate>
                <div style="width:100%; background-color:<%# Convert.ToString(Eval("ProductName")) == "zzTotal"? "yellow":"" %>; height:20px">
            &nbsp;
            <%--<%# Eval("InProcess")%>   --%>
            <%--<a class="copy10grey" href='FulfillmentsStatusReport.aspx?pos=<%# Eval("InProcess")%>&t=<% =TimeInterval%>&cid=<%# Eval("CompanyID")%>'> --%>
            <%# Convert.ToString(Eval("AudioIssues")) == "0" ? "" : Convert.ToString(Eval("AudioIssues"))%>
            <%--</a>--%>
            </div>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Screen Issues" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
            <ItemTemplate>
                <div style="width:100%; background-color:<%# Convert.ToString(Eval("ProductName")) == "zzTotal"? "yellow":"" %>; height:20px">
            &nbsp;
            <%--<%# Eval("Processed")%>   --%>
            <%--<a class="copy10grey" href='FulfillmentsStatusReport.aspx?pos=<%# Eval("Processed")%>&t=<% =TimeInterval%>&cid=<%# Eval("CompanyID")%>'> --%>
            <%# Convert.ToString(Eval("ScreenIssues")) == "0" ? "" : Convert.ToString(Eval("ScreenIssues"))%>
            <%--</a>   --%>
            </div>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Power Issues" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
            <ItemTemplate>
                <div style="width:100%; background-color:<%# Convert.ToString(Eval("ProductName")) == "zzTotal"? "yellow":"" %>; height:20px">
            &nbsp;
            <%--<%# Eval("Shipped")%>   --%>
            <%--<a class="copy10grey" href='FulfillmentsStatusReport.aspx?pos=<%# Eval("Shipped")%>&t=<% =TimeInterval%>&cid=<%# Eval("CompanyID")%>'> --%>
            <%# Convert.ToString(Eval("PowerIssues")) == "0" ? "" : Convert.ToString(Eval("PowerIssues"))%>   
            <%--</a>--%>
            </div>

            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Missing Parts" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
            <ItemTemplate>
                <div style="width:100%; background-color:<%# Convert.ToString(Eval("ProductName")) == "zzTotal"? "yellow":"" %>; height:20px">
            
            <%--<%# Eval("Closed")%>   --%>
            <%--<a class="copy10grey" href='FulfillmentsStatusReport.aspx?pos=<%# Eval("Closed")%>&t=<% =TimeInterval%>&cid=<%# Eval("CompanyID")%>'> --%>
            &nbsp;
            <%# Convert.ToString(Eval("MissingParts")) == "0" ? "" : Convert.ToString(Eval("MissingParts"))%>    
            <%--</a>--%>
            </div>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Return To Stock" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
            <ItemTemplate>
                <div style="width:100%; background-color:<%# Convert.ToString(Eval("ProductName")) == "zzTotal"? "yellow":"" %>; height:20px">
            
            <%--<%# Eval("Return")%>   --%>
            <%--<a class="copy10grey" href='FulfillmentsStatusReport.aspx?pos=<%# Eval("Return")%>&t=<% =TimeInterval%>&cid=<%# Eval("CompanyID")%>'> 
            --%>&nbsp;
            <%# Convert.ToString(Eval("ReturnToStock")) == "0" ? "" : Convert.ToString(Eval("ReturnToStock"))%>   
            <%--</a>--%>
            </div>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Buyer Remorse" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
            <ItemTemplate>
                <div style="width:100%; background-color:<%# Convert.ToString(Eval("ProductName")) == "zzTotal"? "yellow":"" %>; height:20px">
            
            <%--<%# Eval("OnHold")%>   --%>
            <%--<a class="copy10grey" href='FulfillmentsStatusReport.aspx?pos=<%# Eval("OnHold")%>&t=<% =TimeInterval%>&cid=<%# Eval("CompanyID")%>'> 
            --%>&nbsp;
            <%# Convert.ToString(Eval("BuyerRemorse")) == "0" ? "" : Convert.ToString(Eval("BuyerRemorse"))%>   
            <%--</a>--%>
            </div>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Physical Abuse" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
            <ItemTemplate>
                <div style="width:100%; background-color:<%# Convert.ToString(Eval("ProductName")) == "zzTotal"? "yellow":"" %>; height:20px">
            
            <%--<%# Eval("OutofStock")%> --%>  
            <%--<a class="copy10grey" href='FulfillmentsStatusReport.aspx?pos=<%# Eval("OutofStock")%>&t=<% =TimeInterval%>&cid=<%# Eval("CompanyID")%>'> --%>
            &nbsp;
            <%# Convert.ToString(Eval("PhysicalAbuse")) == "0" ? "" : Convert.ToString(Eval("PhysicalAbuse"))%>   
            <%--</a>--%>
            </div>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Liquid Damage" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
            <ItemTemplate>
                <div style="width:100%; background-color:<%# Convert.ToString(Eval("ProductName")) == "zzTotal"? "yellow":"" %>; height:20px">
            
            <%--<%# Eval("Cancel")%>   --%>
           <%-- <a class="copy10grey" href='FulfillmentsStatusReport.aspx?pos=<%# Eval("Cancel")%>&t=<% =TimeInterval%>&cid=<%# Eval("CompanyID")%>'> 
           --%> &nbsp;
            <%# Convert.ToString(Eval("LiquidDamage")) == "0" ? "" : Convert.ToString(Eval("LiquidDamage"))%>   
            <%--</a>--%>
            </div>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Drop Calls" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
            <ItemTemplate>
                <div style="width:100%; background-color:<%# Convert.ToString(Eval("ProductName")) == "zzTotal"? "yellow":"" %>; height:20px">
            
            <%--<%# Eval("PartialProcessed")%>   --%>
            <%--<a class="copy10grey" href='FulfillmentsStatusReport.aspx?pos=<%# Eval("PartialProcessed")%>&t=<% =TimeInterval%>&cid=<%# Eval("CompanyID")%>'> 
            --%>&nbsp;
            <%# Convert.ToString(Eval("DropCalls")) == "0" ? "" : Convert.ToString(Eval("DropCalls"))%>   
            <%--</a>--%>
            </div>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Activation Issues" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
            <ItemTemplate>
                <div style="width:100%; background-color:<%# Convert.ToString(Eval("ProductName")) == "zzTotal"? "yellow":"" %>; height:20px">
            
            <%--<%# Eval("Cancel")%>   --%>
           <%-- <a class="copy10grey" href='FulfillmentsStatusReport.aspx?pos=<%# Eval("Cancel")%>&t=<% =TimeInterval%>&cid=<%# Eval("CompanyID")%>'> 
           --%> &nbsp;
            <%# Convert.ToString(Eval("ActivationIssues")) == "0" ? "" : Convert.ToString(Eval("ActivationIssues"))%>   
            <%--</a>--%>
            </div>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Coverage Issues" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
            <ItemTemplate>
                <div style="width:100%; background-color:<%# Convert.ToString(Eval("ProductName")) == "zzTotal"? "yellow":"" %>; height:20px">
            
            <%--<%# Eval("PartialProcessed")%>   --%>
            <%--<a class="copy10grey" href='FulfillmentsStatusReport.aspx?pos=<%# Eval("PartialProcessed")%>&t=<% =TimeInterval%>&cid=<%# Eval("CompanyID")%>'> 
            --%>&nbsp;
            <%# Convert.ToString(Eval("CoverageIssues")) == "0" ? "" : Convert.ToString(Eval("CoverageIssues"))%>   
            <%--</a>--%>
            </div>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Software" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
            <ItemTemplate>
                <div style="width:100%; background-color:<%# Convert.ToString(Eval("ProductName")) == "zzTotal"? "yellow":"" %>; height:20px">
            
            <%--<%# Eval("Cancel")%>   --%>
           <%-- <a class="copy10grey" href='FulfillmentsStatusReport.aspx?pos=<%# Eval("Cancel")%>&t=<% =TimeInterval%>&cid=<%# Eval("CompanyID")%>'> 
           --%> &nbsp;
            <%# Convert.ToString(Eval("Software")) == "0" ? "" : Convert.ToString(Eval("Software"))%>   
            <%--</a>--%>
            </div>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Loaner Program" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
            <ItemTemplate>
                <div style="width:100%; background-color:<%# Convert.ToString(Eval("ProductName")) == "zzTotal"? "yellow":"" %>; height:20px">
            
            <%--<%# Eval("PartialProcessed")%>   --%>
            <%--<a class="copy10grey" href='FulfillmentsStatusReport.aspx?pos=<%# Eval("PartialProcessed")%>&t=<% =TimeInterval%>&cid=<%# Eval("CompanyID")%>'> 
            --%>&nbsp;
            <%# Convert.ToString(Eval("LoanerProgram")) == "0" ? "" : Convert.ToString(Eval("LoanerProgram"))%>   
            <%--</a>--%>
            </div>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Shipping Error" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
            <ItemTemplate>
                <div style="width:100%; background-color:<%# Convert.ToString(Eval("ProductName")) == "zzTotal"? "yellow":"" %>; height:20px">
            
            <%--<%# Eval("Cancel")%>   --%>
           <%-- <a class="copy10grey" href='FulfillmentsStatusReport.aspx?pos=<%# Eval("Cancel")%>&t=<% =TimeInterval%>&cid=<%# Eval("CompanyID")%>'> 
           --%> &nbsp;
            <%# Convert.ToString(Eval("ShippingError")) == "0" ? "" : Convert.ToString(Eval("ShippingError"))%>   
            <%--</a>--%>
            </div>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Hardware Issues" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
            <ItemTemplate>
                <div style="width:100%; background-color:<%# Convert.ToString(Eval("ProductName")) == "zzTotal"? "yellow":"" %>; height:20px">
            
            <%--<%# Eval("PartialProcessed")%>   --%>
            <%--<a class="copy10grey" href='FulfillmentsStatusReport.aspx?pos=<%# Eval("PartialProcessed")%>&t=<% =TimeInterval%>&cid=<%# Eval("CompanyID")%>'> 
            --%>&nbsp;
            <%# Convert.ToString(Eval("HardwareIssues")) == "0" ? "" : Convert.ToString(Eval("HardwareIssues"))%>   
            <%--</a>--%>
            </div>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Total" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ControlStyle-BackColor="Yellow" ItemStyle-Width="5%">
            <ItemTemplate>
                <div style="width:100%; background-color:yellow; height:20px">
            &nbsp;
            <%# Eval("Total")%>   
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
