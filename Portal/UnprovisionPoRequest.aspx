<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UnprovisionPoRequest.aspx.cs" Inherits="avii.UnprovisionPoRequest" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>
<%@ Register TagPrefix="menu" TagName="Menu" Src="~/Controls/Header.ascx" %>


<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Unprovisioning Request</title>

    <script type="text/javascript">
        function OpenNewPage(url) {

            var newWin = window.open(url);

            if (!newWin || newWin.closed || typeof newWin.closed == 'undefined') {
                alert('your pop up blocker is enabled');

                //POPUP BLOCKED
            }
        }
        </script>

</head>
<body>
    <form id="form1" runat="server">
               <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <table cellspacing="0" cellpadding="0" border="0" align="center" width="100%">
        <tr>
            <td>
                <menu:Menu ID="menu1" runat="server" ></menu:Menu>
            </td>
         </tr>
         </table>

        <table cellspacing="0" cellpadding="0" border="0" align="center" width="95%">
        <tr valign="top">
        <td>
            <table align="center" style="text-align:left" width="100%">
            <tr class="buttonlabel" align="left">
            <td>&nbsp;Unprovisioning</td></tr>
            </table>

        </td>
        </tr>
        </table>
        <asp:UpdatePanel ID="UPl1"  UpdateMode="Conditional" runat="server"   >
         <ContentTemplate>
         <table  align="center" style="text-align:left" width="95%">
         <tr>
            <td>
               <asp:Label ID="lblMsg" runat="server" CssClass="errormessage"></asp:Label>
              
            
            </td>
         </tr>
         </table>
         <table bordercolor="#839abf" border="1" width="95%" align="center" cellpadding="0" cellspacing="0">
          <tr>
            <td>
             <asp:Panel  ID="pnlSearch" runat="server"  DefaultButton="btnSearch" >
             <table width="100%" border="0" class="box" align="center" cellpadding="5" cellspacing="5">
             
                <tr valign="top" id="trCustomer" runat="server" >
                <td class="copy10grey" align="right" width="35%">
                    Customer Name:
                </td>
                <td width="65%">
                    <asp:DropDownList ID="dpCompany" CssClass="copy10grey" runat="server" Width="40%" TabIndex="1">
			        </asp:DropDownList>
                
                </td>

                    
                </tr>
                 
                <tr valign="top" >
                <td class="copy10grey" align="right" width="35%">
                    Fulfillment#:
                </td>
                <td width="65%">
                    <asp:TextBox ID="txtPO" CssClass="copy10grey" runat="server" Width="40%" TabIndex="1">
			        </asp:TextBox>
                
                </td>

                    
                </tr>
                <%-- <tr valign="top">
                <td class="copy10grey" align="right" width="15%">
                    Date From:
                </td>
                <td width="35%">
                    <asp:TextBox ID="txtDateFrom" runat="server" onfocus="set_focus1();"  CssClass="copy10grey" MaxLength="12"  Width="80%"></asp:TextBox>
                    <img id="imgDateFrom" alt="" onclick="document.getElementById('<%=txtDateFrom.ClientID%>').value = ''; displayCalendar(document.getElementById('<%=txtDateFrom.ClientID%>'),'mm/dd/yyyy',this,true);" src="../fullfillment/calendar/sscalendar.jpg" />
                                        
         
                </td>
                <td  width="1%">
                    &nbsp;
                </td>
                <td class="copy10grey" align="right" width="15%">
                    Date To:
                </td>
                <td width="35%">
                   
                  <asp:TextBox ID="txtDateTo" runat="server" onfocus="set_focus2();"  CssClass="copy10grey" MaxLength="12"  Width="80%"></asp:TextBox>
                    <img id="imgDateTo" alt="" onclick="document.getElementById('<%=txtDateTo.ClientID%>').value = ''; displayCalendar(document.getElementById('<%=txtDateTo.ClientID%>'),'mm/dd/yyyy',this,true);" src="../fullfillment/calendar/sscalendar.jpg" />
                                                
        
                </td>   
                
                    
                </tr>
          --%>
                <tr>                
                    <td colspan="2">
                        <hr />
                    </td>
                </tr>
                <tr>
                <td  align="center"  colspan="2">
                    <asp:Button ID="btnSearch" runat="server"  CssClass="button" Text="Search" OnClick="btnSearch_Click" ></asp:Button>
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="button"  OnClick="btnCancel_Click" CausesValidation="false"/>
                  
        </td>
        </tr>
               
            </table>
              
            </asp:Panel>
         </td>
         </tr>
         </table>
             <br />
      <table align="center" style="text-align:left" width="100%">
      <tr>
     <tr>
                <td  align="center"  >
                        <table width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td align="left">
                                
                            </td>
                            <td align="right">
                                <asp:Label ID="lblCount" CssClass="copy10grey" runat="server" ></asp:Label>

                                <%--<asp:Button ID="btnDownload"  Visible="false" CssClass="button" OnClick="btnDownload_Click"  runat="server" Text="Download"></asp:Button>--%>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                 <asp:GridView ID="gvPOQuery" AutoGenerateColumns="false"  
                DataKeyNames="POID"  Width="100%"  
            ShowFooter="false" runat="server" GridLines="Both"          PageSize="20" AllowPaging="false" 
            BorderStyle="Outset"  > 
            <RowStyle BackColor="Gainsboro" />
            <AlternatingRowStyle BackColor="white" />
            <HeaderStyle  CssClass="buttongrid" ForeColor="white"/>
            <FooterStyle CssClass="white"  />
               <SortedAscendingHeaderStyle  Font-Underline="true" />
               <SortedDescendingHeaderStyle   Font-Underline="true" />
            <PagerStyle ForeColor="#636363" CssClass="copy10grey" HorizontalAlign="Left" />
            <Columns>
                <%--<asp:TemplateField ItemStyle-CssClass="copy10grey"  ItemStyle-Width="1%">
                    <ItemTemplate>
                        <asp:CheckBox ID="chk"  runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>  --%>
                <asp:TemplateField HeaderText="S.No." ItemStyle-CssClass="copy10grey" ItemStyle-HorizontalAlign="Right"  ItemStyle-Width="1%" HeaderStyle-CssClass="buttongrid">
                    <ItemTemplate>

                            <%# Container.DataItemIndex + 1%> &nbsp;
                  
                    </ItemTemplate>
                    </asp:TemplateField>
                
                <asp:TemplateField HeaderText="Fulfillment#" HeaderStyle-CssClass="buttonundlinelabel" SortExpression="PurchaseOrderNumber"  ItemStyle-CssClass="copy10grey" 
                    ItemStyle-Width="8%">
                    <ItemTemplate>
                        <asp:Label ID="lblPoNum" runat="server" Text='<%# Eval("FulfillmentNumber") %>'></asp:Label>   
                        
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Customer Order#" HeaderStyle-CssClass="buttonundlinelabel" SortExpression="PurchaseOrderNumber"  ItemStyle-CssClass="copy10grey" 
                    ItemStyle-Width="8%">
                    <ItemTemplate>
                        <%# Eval("CustomerOrderNumber") %>
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="Fulfillment Date" HeaderStyle-CssClass="buttonundlinelabel"  SortExpression="PurchaseOrderDate" ItemStyle-CssClass="copy10grey"  
                    ItemStyle-Width="5%">
                    <ItemTemplate><%# DataBinder.Eval(Container.DataItem, "PO_Date", "{0:d}") %></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Requested Shipping Date"   ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "requestedshipdate", "{0:d}") %>
                    
                    </ItemTemplate>
                </asp:TemplateField>
<asp:TemplateField HeaderText="Shipping Date"   ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="copy10grey" ItemStyle-Width="7%">
                    <ItemTemplate>
                        <asp:Label ID="lblShippDate" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ShipTo_Date", "{0:d}"))=="1/1/0001"? "": DataBinder.Eval(Container.DataItem, "ShipTo_Date", "{0:d}")%>'></asp:Label>                        
                    
                    </ItemTemplate>
                </asp:TemplateField>


                <asp:TemplateField HeaderText="Customer" HeaderStyle-CssClass="buttonundlinelabel"  SortExpression="CustomerName"  ItemStyle-HorizontalAlign="left" 
                    ItemStyle-CssClass="copy10grey" ItemStyle-Width="6%">
                    <ItemTemplate>
                    <%# Eval("CompanyName")%>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Contact Name"  ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                    <ItemTemplate><%# Eval("Contact_Name")%></ItemTemplate>
                    
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Contact Phone"  ItemStyle-CssClass="copy10grey" ItemStyle-Width="7%">
                    <ItemTemplate><%# Eval("Contact_Phone") %></ItemTemplate>
                    
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="Store ID" HeaderStyle-CssClass="buttonundlinelabel"  SortExpression="StoreID"  ItemStyle-HorizontalAlign="Left" 
                    ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                    <ItemTemplate>
                    
                        <%#Eval("Store_ID")%>
                        
                    
                    </ItemTemplate>
                </asp:TemplateField> 

                <asp:TemplateField HeaderText="Street Address"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="20%">
                    <ItemTemplate><%#Eval("ShipTo_Address")%> <%#Eval("ShipTo_Address2")%>
                    <%#Eval("ShipTo_City")%>
                    <%#  Convert.ToString(Eval("ShipTo_State")).ToUpper() %> 
                    <%#Eval("ShipTo_Zip")%>
                    
                    
                    
                    </ItemTemplate>
                </asp:TemplateField> 
    
                <asp:TemplateField HeaderText="Status" HeaderStyle-CssClass="buttonundlinelabel"  SortExpression="PurchaseOrderStatus"  ItemStyle-HorizontalAlign="Left" 
                    ItemStyle-CssClass="copy10grey" ItemStyle-Width="3%">
                    <ItemTemplate>
                    
                        <%#Eval("StatusText")%>
                    </ItemTemplate>
                </asp:TemplateField>   
                 <asp:TemplateField HeaderText="Source"  ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
                    <ItemTemplate><%#Eval("POSource")%></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Line Item Count"  ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                    <ItemTemplate><%#Eval("LineItemCount")%></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="PO TYPE"  ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                    <ItemTemplate><%#Eval("POType")%></ItemTemplate>
                </asp:TemplateField>
               
                <asp:TemplateField HeaderText=""  ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="12%"  ItemStyle-Width="13%">
                    <ItemTemplate>
                    
                    <table width="100%">
                        <tr>
                        <td>
                            <asp:ImageButton ID="imgPO"  ToolTip="View PO" OnCommand="imgPO_Command"  CausesValidation="false" 
                             CommandArgument='<%# Eval("POID") %>' ImageUrl="~/Images/view.png"  runat="server" />
                        
                        </td>
                        </tr>
                        </table>
                        
                        
                        
                    </ItemTemplate>
                </asp:TemplateField>
                		    
			</Columns>
        </asp:GridView>          
                     
                            </td>
                        </tr>                        
                        </table>
                        
                </td>
                </tr>
            </table>
            
            </ContentTemplate>             
           </asp:UpdatePanel>
  
    </form>
</body>
</html>
