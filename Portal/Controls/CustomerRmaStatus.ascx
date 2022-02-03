<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerRmaStatus.ascx.cs" Inherits="avii.Controls.CustomerRmaStatus" %>
<table width="125%" cellpadding="0" cellspacing="0">
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
        <asp:TemplateField HeaderText="S.No." ItemStyle-CssClass="copy10grey"  ItemStyle-Width="1%">
            <ItemTemplate>

                  <%# Convert.ToString(Eval("CustomerName")) == "zzTotal" ? "" : Convert.ToString(Container.DataItemIndex + 1)%>
                  
            </ItemTemplate>
        </asp:TemplateField>                 
        
        <asp:TemplateField HeaderText="Customer Name" ItemStyle-CssClass="copy10grey"  ItemStyle-Width="10%"  ItemStyle-HorizontalAlign="Left">
            <ItemTemplate>
            <div style="width:100%; font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
            &nbsp;
                 <%# Convert.ToString(Eval("CustomerName")) == "zzTotal" ? "Total" : Eval("CustomerName")%>
                 
            </div>
            </ItemTemplate>
        </asp:TemplateField>                 
                
        <asp:TemplateField HeaderText="Pending" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
            <ItemTemplate>
            <div style="width:100%; font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
            &nbsp;
               <%--<a class="copy10grey" href='FulfillmentsStatusReport.aspx?pos=<%# Eval("Pending")%>&t=<% =TimeInterval%>&cid=<%# Eval("CompanyID")%>'> --%>
            <%# Convert.ToString(Eval("Pending")) == "0" ? "" : Convert.ToString(Eval("Pending"))%>   
            <%--</a>--%>
            
            </div>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Received" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
            <ItemTemplate>
                <div style="width:100%;  font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
            &nbsp;
            <%--<%# Eval("InProcess")%>   --%>
            <%--<a class="copy10grey" href='FulfillmentsStatusReport.aspx?pos=<%# Eval("InProcess")%>&t=<% =TimeInterval%>&cid=<%# Eval("CompanyID")%>'> --%>
            <%# Convert.ToString(Eval("Received")) == "0" ? "" : Convert.ToString(Eval("Received"))%>
            <%--</a>--%>
            
            </div>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Pending for Repair" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
            <ItemTemplate>
                <div style="width:100%;  font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
            &nbsp;
            <%--<%# Eval("Processed")%>   --%>
            <%--<a class="copy10grey" href='FulfillmentsStatusReport.aspx?pos=<%# Eval("Processed")%>&t=<% =TimeInterval%>&cid=<%# Eval("CompanyID")%>'> --%>
            <%# Convert.ToString(Eval("PendingforRepair")) == "0" ? "" : Convert.ToString(Eval("PendingforRepair"))%>
            <%--</a>   --%>
            
            </div>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Pending for Credit" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
            <ItemTemplate>
                <div style="width:100%;  font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
            &nbsp;
            <%--<%# Eval("Shipped")%>   --%>
            <%--<a class="copy10grey" href='FulfillmentsStatusReport.aspx?pos=<%# Eval("Shipped")%>&t=<% =TimeInterval%>&cid=<%# Eval("CompanyID")%>'> --%>
            <%# Convert.ToString(Eval("PendingforCredit")) == "0" ? "" : Convert.ToString(Eval("PendingforCredit"))%>   
            <%--</a>--%>

            
            </div>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Pending for Replacement" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
            <ItemTemplate>
                <div style="width:100%;  font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
            &nbsp;
            <%--<%# Eval("Closed")%>   --%>
            <%--<a class="copy10grey" href='FulfillmentsStatusReport.aspx?pos=<%# Eval("Closed")%>&t=<% =TimeInterval%>&cid=<%# Eval("CompanyID")%>'> --%>
            
            <%# Convert.ToString(Eval("PendingforReplacement")) == "0" ? "" : Convert.ToString(Eval("PendingforReplacement"))%>   
            <%--</a>--%>
            
            </div>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Approved" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
            <ItemTemplate>
                <div style="width:100%;  font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
            &nbsp;
            <%--<%# Eval("Return")%>   --%>
            <%--<a class="copy10grey" href='FulfillmentsStatusReport.aspx?pos=<%# Eval("Return")%>&t=<% =TimeInterval%>&cid=<%# Eval("CompanyID")%>'> 
            --%>
            <%# Convert.ToString(Eval("Approved")) == "0" ? "" : Convert.ToString(Eval("Approved"))%>   
            <%--</a>--%>
            
            </div>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Returned" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
            <ItemTemplate>
                <div style="width:100%;  font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
            &nbsp;
            <%--<%# Eval("OnHold")%>   --%>
            <%--<a class="copy10grey" href='FulfillmentsStatusReport.aspx?pos=<%# Eval("OnHold")%>&t=<% =TimeInterval%>&cid=<%# Eval("CompanyID")%>'> 
            --%>
            <%# Convert.ToString(Eval("Returned")) == "0" ? "" : Convert.ToString(Eval("Returned"))%>   
            <%--</a>--%>
            
            </div>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Credited" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
            <ItemTemplate>
                <div style="width:100%; font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>;  background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
            &nbsp;
            <%--<%# Eval("OutofStock")%> --%>  
            <%--<a class="copy10grey" href='FulfillmentsStatusReport.aspx?pos=<%# Eval("OutofStock")%>&t=<% =TimeInterval%>&cid=<%# Eval("CompanyID")%>'> --%>
            
            <%# Convert.ToString(Eval("Credited")) == "0" ? "" : Convert.ToString(Eval("Credited"))%>   
            <%--</a>--%>
            
            </div>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Denied" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
            <ItemTemplate>
                <div style="width:100%; font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>;  background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
            &nbsp;
            <%--<%# Eval("Cancel")%>   --%>
           <%-- <a class="copy10grey" href='FulfillmentsStatusReport.aspx?pos=<%# Eval("Cancel")%>&t=<% =TimeInterval%>&cid=<%# Eval("CompanyID")%>'> 
           --%> 
            <%# Convert.ToString(Eval("Denied")) == "0" ? "" : Convert.ToString(Eval("Denied"))%>   
            <%--</a>--%>
            
            </div>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Closed" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
            <ItemTemplate>
                <div style="width:100%; font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>;  background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
            &nbsp;
            <%--<%# Eval("PartialProcessed")%>   --%>
            <%--<a class="copy10grey" href='FulfillmentsStatusReport.aspx?pos=<%# Eval("PartialProcessed")%>&t=<% =TimeInterval%>&cid=<%# Eval("CompanyID")%>'> 
            --%>
            <%# Convert.ToString(Eval("Closed")) == "0" ? "" : Convert.ToString(Eval("Closed"))%>   
            <%--</a>--%>
            
            </div>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Out with OEM for repair" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
            <ItemTemplate>
                <div style="width:100%; font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>;  background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
            &nbsp;
            <%# Convert.ToString(Eval("OutwithOEMforrepair")) == "0" ? "" : Convert.ToString(Eval("Out with OEM for repair"))%>   
            
            
            </div>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Back to Stock -NDF" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
            <ItemTemplate>
                <div style="width:100%;  font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
            &nbsp;
            <%# Convert.ToString(Eval("BacktoStockNDF")) == "0" ? "" : Convert.ToString(Eval("BacktoStockNDF"))%>   
            
            </div>

            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Back to Stock- Credited" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
            <ItemTemplate>
                <div style="width:100%;  font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
            &nbsp;
            <%# Convert.ToString(Eval("BacktoStockCredited")) == "0" ? "" : Convert.ToString(Eval("BacktoStockCredited"))%>   
            
            </div>

            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Back to Stock – Replaced by OEM" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
            <ItemTemplate>
                <div style="width:100%;  font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
            &nbsp;
            <%# Convert.ToString(Eval("BacktoStockReplacedbyOEM")) == "0" ? "" : Convert.ToString(Eval("BacktoStockReplacedbyOEM"))%>   
            
            </div>

            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Repaired by OEM" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
            <ItemTemplate>
                <div style="width:100%;  font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
            &nbsp;
            <%# Convert.ToString(Eval("RepairedbyOEM")) == "0" ? "" : Convert.ToString(Eval("RepairedbyOEM"))%>   
            
            
            </div>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Replaced BY OEM" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
            <ItemTemplate>
                <div style="width:100%;  font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
            &nbsp;
            <%# Convert.ToString(Eval("ReplacedBYOEM")) == "0" ? "" : Convert.ToString(Eval("ReplacedBYOEM"))%>   
            
            </div>

            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Replaced BY AV" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
            <ItemTemplate>
                <div style="width:100%;  font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
            &nbsp;
            <%# Convert.ToString(Eval("ReplacedBYAV")) == "0" ? "" : Convert.ToString(Eval("ReplacedBYAV"))%>   
            
            
            </div>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Repaired By AV" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
            <ItemTemplate>
                <div style="width:100%;  font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
            &nbsp;
            <%# Convert.ToString(Eval("RepairedByAV")) == "0" ? "" : Convert.ToString(Eval("RepairedByAV"))%>   
            
            
            </div>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="NDF (No Defect Found)" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
            <ItemTemplate>
                <div style="width:100%; font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>;  background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
            &nbsp;
            <%# Convert.ToString(Eval("NDFNoDefectFound")) == "0" ? "" : Convert.ToString(Eval("NDFNoDefectFound"))%>   
            
            </div>

            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="PRE-OWNED – A stock" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
            <ItemTemplate>
                <div style="width:100%;  font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
            &nbsp;
            <%# Convert.ToString(Eval("PREOWNEDAstock")) == "0" ? "" : Convert.ToString(Eval("PREOWNEDAstock"))%>   
            
            </div>

            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="PRE-OWEND - B Stock" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
            <ItemTemplate>
                <div style="width:100%;  font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
            &nbsp;
            <%# Convert.ToString(Eval("PREOWENDBStock")) == "0" ? "" : Convert.ToString(Eval("PREOWENDBStock"))%>   
            
            </div>

            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="PRE-OWEND – C Stock" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
            <ItemTemplate>
                <div style="width:100%;  font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
            &nbsp;
            <%# Convert.ToString(Eval("PREOWENDCStock")) == "0" ? "" : Convert.ToString(Eval("PREOWENDCStock"))%>   
            
            
            </div>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Rejected" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
            <ItemTemplate>
                <div style="width:100%; font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>;  background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
            &nbsp;
            <%# Convert.ToString(Eval("Rejected")) == "0" ? "" : Convert.ToString(Eval("Rejected"))%>   
            
            
            </div>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="RTS (Return To Stock)" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
            <ItemTemplate>
                <div style="width:100%; font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>;  background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
            &nbsp;
            <%# Convert.ToString(Eval("RTSReturnToStock")) == "0" ? "" : Convert.ToString(Eval("RTSReturnToStock"))%>   
            
            </div>

            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Incomplete" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
            <ItemTemplate>
                <div style="width:100%; font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>;  background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
            &nbsp;
            <%# Convert.ToString(Eval("Incomplete")) == "0" ? "" : Convert.ToString(Eval("Incomplete"))%>   
            
            </div>

            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Damaged" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
            <ItemTemplate>
                <div style="width:100%; font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>;  background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
            &nbsp;
            <%# Convert.ToString(Eval("Damaged")) == "0" ? "" : Convert.ToString(Eval("Damaged"))%>   
            
            </div>

            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Preowned" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
            <ItemTemplate>
                <div style="width:100%;  font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
            &nbsp;
            <%# Convert.ToString(Eval("Preowned")) == "0" ? "" : Convert.ToString(Eval("Preowned"))%>   
            
            </div>

            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Return to OEM" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
            <ItemTemplate>
                <div style="width:100%;  font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
            &nbsp;
            <%# Convert.ToString(Eval("ReturntoOEM")) == "0" ? "" : Convert.ToString(Eval("ReturntoOEM"))%>   
            
            </div>

            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Returned to Stock" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
            <ItemTemplate>
                <div style="width:100%; font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>;  background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
            &nbsp;
            <%# Convert.ToString(Eval("ReturnedtoStock")) == "0" ? "" : Convert.ToString(Eval("ReturnedtoStock"))%>   
            
            
            </div>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Cancel" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
            <ItemTemplate>
                <div style="width:100%; font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>;  background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
            &nbsp;
            <%# Convert.ToString(Eval("Cancel")) == "0" ? "" : Convert.ToString(Eval("Cancel"))%>   
            
            </div>

            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="External ESN" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
            <ItemTemplate>
                <div style="width:100%; font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>;  background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
            &nbsp;
            <%# Convert.ToString(Eval("ExternalESN")) == "0" ? "" : Convert.ToString(Eval("ExternalESN"))%>   
            
            
            </div>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Pending ship to OEM" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
            <ItemTemplate>
                <div style="width:100%;  font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
            &nbsp;
            <%# Convert.ToString(Eval("PendingshiptoOEM")) == "0" ? "" : Convert.ToString(Eval("PendingshiptoOEM"))%>   
            
            
            </div>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Sent to OEM" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
            <ItemTemplate>
                <div style="width:100%; font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>;  background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
            &nbsp;
            <%# Convert.ToString(Eval("SenttoOEM")) == "0" ? "" : Convert.ToString(Eval("SenttoOEM"))%>   
            
            
            </div>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Pending ship to Supplier" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
            <ItemTemplate>
                <div style="width:100%;  font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
            &nbsp;
            <%# Convert.ToString(Eval("PendingshiptoSupplier")) == "0" ? "" : Convert.ToString(Eval("PendingshiptoSupplier"))%>   
            
            
            </div>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Sent to Supplier" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
            <ItemTemplate>
                <div style="width:100%; font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>;  background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
            &nbsp;
            <%# Convert.ToString(Eval("SenttoSupplier")) == "0" ? "" : Convert.ToString(Eval("SenttoSupplier"))%>   
            
            
            </div>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Returned from OEM" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
            <ItemTemplate>
                <div style="width:100%;  font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
            &nbsp;
            <%# Convert.ToString(Eval("ReturnedfromOEM")) == "0" ? "" : Convert.ToString(Eval("ReturnedfromOEM"))%>   
            
            </div>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Total" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ControlStyle-BackColor="Yellow" ItemStyle-Width="2%">
            <ItemTemplate>
                <div style="width:100%;  font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>;  background-color:yellow; height:20px">
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
<asp:Label ID="lblRMA" runat="server" CssClass="errormessage"></asp:Label>

    </td>
</tr>

</table>