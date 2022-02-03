<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PODetails.ascx.cs" Inherits="avii.Controls.PODetails" %>
<asp:Panel ID="pnlMsg" runat="server">
<table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="100%" align="center" >
           <tr bordercolor="#839abf">
                <td>
                    <table width="100%" cellSpacing="1" cellPadding="1">
                    <tr>
                    <td>
                        <asp:Label ID="lblMsg" runat="server" CssClass="errormessage" ></asp:Label>
                    </td>
                    </tr>
                    </table>
                    </td>
                    </tr>
                    </table>
</asp:Panel>
<asp:Panel ID="pnlControl" runat="server">
<table width="98%" align="center" cellpadding="0" cellspacing="0">
<tr valign="top">
    <td width="50%">

        <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="100%" align="center" >
           <tr bordercolor="#839abf">
                <td>
                    <table width="100%" cellSpacing="1" cellPadding="1">
                    <tr>
                        <td class="copy10grey" width="30%" align="left">
                            Fulfillment#:
                        </td>
                        <td width="1%">&nbsp;</td>
                        <td  align="left">
                            <asp:Label ID="lblPO" CssClass="copy10grey" runat="server" ></asp:Label>
                        </td>
            
                   
                    </tr>
                    <tr>
                        <td class="copy10grey" width="30%" align="left">
                            Fulfillment Date:
                        </td>
                        <td width="1%">&nbsp;</td>
                        <td  align="left">
                            <asp:Label ID="lblPODate" CssClass="copy10grey" runat="server" ></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="copy10grey" width="30%" align="left">
                          <strong>  Status:</strong>
                        </td>
                        <td width="1%">&nbsp;</td>
                        <td  align="left" >
                            <asp:Label ID="lblStatus" CssClass="copy10grey" runat="server" ></asp:Label>
                        </td>
                    </tr>
                    </table>
                </td>
            </tr>
        </table><br />
        <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="100%" align="center" >
           <tr bordercolor="#839abf">
                <td>
                    <table width="100%" cellSpacing="1" cellPadding="1">
                    <tr>
                        <td class="copy10grey" width="30%" align="left">
                        Shipp By:
                        </td>
                        <td width="1%">&nbsp;</td>
                        <td align="left">
                            <asp:Label ID="lblShipBy" CssClass="copy10grey" runat="server" ></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="copy10grey" width="30%" align="left">
                        Shipping Date:
                        </td>
                        <td width="1%">&nbsp;</td>
                        <td align="left">
                            <asp:Label ID="lblShippDate" CssClass="copy10grey" runat="server" ></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="copy10grey" width="30%" align="left">
                        Tracking#:
                        </td>
                        <td width="1%">&nbsp;</td>
                        <td  align="left">
                            <asp:Label ID="lblTrackNo" CssClass="copy10grey" runat="server" ></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="copy10grey" width="30%" align="left">
                        AVSO#:
                        </td>
                        <td width="1%">&nbsp;</td>
                        <td  align="left">
                            <asp:Label ID="lblAVSO" CssClass="copy10grey" runat="server" ></asp:Label>
                        </td>
                    </tr>
                    </table>
                </td>
            </tr>
            </table>
    </td>
    <td width="50%">
    
        <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="99%" align="center" >
           <tr bordercolor="#839abf">
                <td>
                <table width="100%" cellSpacing="1" cellPadding="1">
                <tr>
                    
            
                    <td class="copy10grey" width="30%" align="left">
                        Customer name:
                    </td>
                    <td width="1%">&nbsp;</td>
                    <td  align="left">
            
                        <asp:Label ID="lblCustName" CssClass="copy10grey" runat="server" ></asp:Label>
                    </td>
                </tr>
                <tr>
                    
                    <td class="copy10grey" width="30%" align="left">
                        Contact name:
                    </td>
                    <td width="1%">&nbsp;</td>
                    <td  align="left">
                        <asp:Label ID="lblContactName" CssClass="copy10grey" runat="server" ></asp:Label>
                    </td>
                </tr>
                <tr>
                    
                    <td class="copy10grey" width="30%" align="left">
                        Store ID:
                    </td>
                    <td width="1%">&nbsp;</td>
                    <td align="left">
                        <asp:Label ID="lblStoreID" CssClass="copy10grey" runat="server" ></asp:Label>
                    </td>
                </tr>
                <tr>
                    
                    <td class="copy10grey" width="30%" align="left">
                        Street Address:
                    </td>
                    <td width="1%">&nbsp;</td>
                    <td  align="left">
                        <asp:Label ID="lblAddress" CssClass="copy10grey" runat="server" ></asp:Label>
                    </td>
                </tr>
                <tr>
                    
                    <td class="copy10grey" width="30%" align="left">
                        State:
                    </td>
                    <td width="1%">&nbsp;</td>
                    <td  align="left">
                        <asp:Label ID="lblState" CssClass="copy10grey" runat="server" ></asp:Label>
                    </td>
                </tr>
                <tr>
                    
                    <td class="copy10grey" width="30%" align="left">
                        Zip:
                    </td>
                    <td width="1%">&nbsp;</td>
                    <td  align="left">
                        <asp:Label ID="lblZip" CssClass="copy10grey" runat="server" ></asp:Label>
                    </td>
                </tr>
                <tr>
                    
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
<tr >
<td colspan="2">
<table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="100%" align="center" >
<tr bordercolor="#839abf">
    <td>
        <table width="100%" cellSpacing="3" cellPadding="3">
        <tr>
            <td class="copy10grey" width="10%" align="left"> 
            Comments:
            </td>
            <td width="1%">&nbsp;</td>
            <td   align="left">
                <asp:Label ID="lblComment" CssClass="copy10grey" runat="server" ></asp:Label>
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
<td align="right" colspan="2">
    <asp:Label ID="lblPODCount" runat="server" CssClass="copy10greyb" ></asp:Label>
</td>
</tr>

<tr>
    <td colspan="2">
    
    <asp:GridView ID="gvPODetail"  BackColor="White" Width="100%" Visible="true"  
        AutoGenerateColumns="false" Font-Names="Verdana" runat="server" DataKeyNames="PodID"
        GridLines="Both" 
        BorderStyle="Double" BorderColor="#0083C1" >
        <RowStyle BackColor="Gainsboro" />
        <AlternatingRowStyle BackColor="white" />
        <HeaderStyle  CssClass="buttonlabel" ForeColor="white"/>
        <FooterStyle CssClass="white"  />
        <Columns>
         	<asp:TemplateField HeaderText="Line#"  ItemStyle-CssClass="copy10grey"  HeaderStyle-Width="3%" ItemStyle-Wrap="false"  ItemStyle-width="3%">
                <ItemTemplate>
                   <%# Container.DataItemIndex +  1 %>
                </ItemTemplate> 
                
            </asp:TemplateField>
            <asp:TemplateField HeaderText="SKU#" SortExpression="Item_Code" ItemStyle-CssClass="copy10grey"  HeaderStyle-Width="7%" ItemStyle-Wrap="false"  ItemStyle-width="10%">
                <ItemTemplate>
                    <%# Convert.ToString(Eval("ItemCode")).ToUpper()%>
                </ItemTemplate> 
                
            </asp:TemplateField>
                                            
            <asp:TemplateField HeaderText="Qty" SortExpression="Qty"  ItemStyle-CssClass="copy10grey" ItemStyle-Width="3%">
                <ItemTemplate><%#Eval("Quantity")%></ItemTemplate>
            </asp:TemplateField>
                                            
            <asp:TemplateField HeaderText="ESN" SortExpression="ESN" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%" >
                <ItemTemplate>
                    <%# Eval("ESN") %>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtEsn" CssClass="copy10grey" MaxLength="35"  Width="99%" Text='<%# Eval("ESN") %>' runat="server"></asp:TextBox>
                </EditItemTemplate>
                                                
            </asp:TemplateField>
                                            
            <%--<asp:TemplateField HeaderText="MDN" SortExpression="MdnNumber"  ItemStyle-CssClass="copy10grey" ItemStyle-Width="7%" >
                <ItemTemplate><%#Eval("MdnNumber")%></ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtMdn" CssClass="copy10grey" MaxLength="30"  Text='<%# Eval("MdnNumber") %>' runat="server"></asp:TextBox>
                </EditItemTemplate>                                                                                
            </asp:TemplateField>

                                        
            <asp:TemplateField HeaderText="MSID" SortExpression="MsID"  ItemStyle-CssClass="copy10grey" ItemStyle-Width="7%">
                <ItemTemplate><%#Eval("MsID")%></ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtMsid" CssClass="copy10grey" MaxLength="30"  Text='<%# Eval("MsID") %>' runat="server"></asp:TextBox>
                </EditItemTemplate>                                                                                                                                  
            </asp:TemplateField>
                                            
            <asp:TemplateField HeaderText="FM-UPC" SortExpression="FMUPC" ItemStyle-CssClass="copy10grey" ItemStyle-Width="7%">
                <ItemTemplate>
                    <%#Eval("FMUPC")%>
                </ItemTemplate>
                
                                                
            </asp:TemplateField>--%>
                
            <asp:TemplateField HeaderText="MslNumber" SortExpression="MslNumber" ItemStyle-CssClass="copy10grey" ItemStyle-Width="7%">
                <ItemTemplate>
                    <%#Eval("MslNumber")%>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtMslNumber" CssClass="copy10grey" MaxLength="30"  Text='<%# Eval("MslNumber") %>' runat="server"></asp:TextBox>
                </EditItemTemplate>
                                                
            </asp:TemplateField>
                <%--<asp:TemplateField HeaderText="AKEY" SortExpression="akey" ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                <ItemTemplate>
                    <%#Eval("akey")%>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtAkey" Visible='<%# Convert.ToBoolean(Eval("CustomAttribute")) == true? true: false %>'  CssClass="copy10grey" MaxLength="30"  Text='<%# Eval("AKEY") %>' runat="server"></asp:TextBox>
                </EditItemTemplate>
                
             </asp:TemplateField>
             
            <asp:TemplateField HeaderText="OTKSL" SortExpression="OTKSL" ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                <ItemTemplate>
                    <%#Eval("OTKSaL")%>   
                </ItemTemplate>
                 <EditItemTemplate>
                    <asp:TextBox ID="txtOTKSL" Visible='<%# Convert.ToBoolean(Eval("CustomAttribute")) == true? true: false %>'  CssClass="copy10grey" MaxLength="30"  Text='<%# Eval("OTKSaL") %>' runat="server"></asp:TextBox>
                </EditItemTemplate>
             </asp:TemplateField>--%>
    
                                    
             
             
            <asp:TemplateField HeaderText="ICCID" SortExpression="LTEICCID" ItemStyle-CssClass="copy10grey" ItemStyle-Width="7%">
                <ItemTemplate>
                    <%#Eval("LTEICCID")%>   
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtLTEICCID" Visible='<%# Convert.ToBoolean(Eval("CustomAttribute")) == true? true: false %>' CssClass="copy10grey" MaxLength="30"  Text='<%# Eval("LTEICCID") %>' runat="server"></asp:TextBox>
                </EditItemTemplate>
                                                
            </asp:TemplateField>
            
            <%--<asp:TemplateField HeaderText="LTE IMSI" SortExpression="LTEIMSI" ItemStyle-CssClass="copy10grey" ItemStyle-Width="7%">
                <ItemTemplate>
                    <%#Eval("LTEIMSI")%>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtLTEIMSI" Visible='<%# Convert.ToBoolean(Eval("CustomAttribute")) == true? true: false %>'  CssClass="copy10grey" MaxLength="30"  Text='<%# Eval("LTEIMSI") %>' runat="server"></asp:TextBox>
                </EditItemTemplate>
                                                
            </asp:TemplateField>--%>
           
            <asp:TemplateField HeaderText="Status" SortExpression="StatusID"  ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                <ItemTemplate>
                <%# Convert.ToInt32(Eval("StatusID")) == 1 ? "Pending" : Convert.ToInt32(Eval("StatusID")) == 2 ? "Processed" : Convert.ToInt32(Eval("StatusID")) == 3 ? "Shipped" : Convert.ToInt32(Eval("StatusID")) == 4 ? "Closed" : Convert.ToInt32(Eval("StatusID")) == 5 ? "Return" : Convert.ToInt32(Eval("StatusID")) == 9 ? "Cancel" : Convert.ToInt32(Eval("StatusID")) == 6 ? "On Hold" : Convert.ToInt32(Eval("StatusID")) == 7 ? "Out of Stock" : Convert.ToInt32(Eval("StatusID")) == 8 ? "Recieved" : Convert.ToInt32(Eval("StatusID")) == 10 ? "Partial Processed" : "Pending"%>
                
                                                
                </ItemTemplate>
            </asp:TemplateField> 
            
             
    		                                
        </Columns>
    </asp:GridView>
    
    </td>
</tr>
</table>

</asp:Panel>














