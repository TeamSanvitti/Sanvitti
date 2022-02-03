<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FulfillmentEsnAssigned.aspx.cs" Inherits="avii.FulfillmentEsnAssigned" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="~/Controls/Header.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script>
        function close_window() {
            if (confirm("Close Window?")) {
                window.close();
                return true;
            }
            else
                return false
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    
    <table cellSpacing="0" cellPadding="0" align="center" width="100%" border="0">
        <tr>
			<td><head:MenuHeader id="HeadAdmin" runat="server"></head:MenuHeader></td>
		</tr>
     </table>
    <table cellspacing="0" cellpadding="0" border="0" align="center" width="95%">
		<tr>
			<td  bgcolor="#dee7f6" class="buttonlabel">
            &nbsp;&nbsp;Fulfimment Assignment
			</td>
		</tr>
    
    </table>  
    <asp:UpdatePanel ID="upPnlPOA" runat="server" UpdateMode="Conditional">
	<ContentTemplate>
                   
	<asp:PlaceHolder ID="pnrPOA" runat="server">
        <asp:Label ID="lblPOA" runat="server"  CssClass="errormessage"></asp:Label>
        <table width="95%" align="center" cellpadding="0" cellspacing="0">
            <tr>
                <td class="copy10grey" align="left" >
                     <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="100%" align="center" >
                        <tr bordercolor="#839abf">
                        <td>
                            <strong>  Fulfillment#: &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Label ID="lblAssignPO" runat="server" CssClass="copy10grey"></asp:Label></strong>
                        </td>
                        </tr>
                     </table>
                </td>
            </tr>
            
                            
            <tr>
                <td colspan="2" class="copy10grey">
                    <br />
                <asp:GridView ID="gvPOA"  BackColor="White" Width="100%" Visible="true" 
                    AutoGenerateColumns="false" Font-Names="Verdana" runat="server" 
                    GridLines="Both" 
                    BorderStyle="Double" BorderColor="#0083C1">
                    <RowStyle BackColor="Gainsboro" />
                    <AlternatingRowStyle BackColor="white" />
                    <HeaderStyle  CssClass="buttongrid" ForeColor="white"/>
                    <FooterStyle CssClass="white"  />
                    <Columns>
         	   
                        <asp:TemplateField HeaderText="SKU" SortExpression="sku" ItemStyle-CssClass="copy10grey"  HeaderStyle-Width="25%" ItemStyle-Wrap="false"  ItemStyle-width="10%">
                            <ItemTemplate>
                                <%# Eval("SKU")%>
                            </ItemTemplate>
                
                        </asp:TemplateField>
                        <%--<asp:TemplateField HeaderText="UPC" SortExpression="UPC"  ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                            <ItemTemplate><%#Eval("UPC")%></ItemTemplate>
                        </asp:TemplateField>
                        --%>                                                                                                                        
                                            
                                            
                        <asp:TemplateField HeaderText="Quantity" SortExpression="Qty" ItemStyle-CssClass="copy10grey" ItemStyle-Width="25%">
                            <ItemTemplate>
                                <%# Eval("Quantity")%>
                            </ItemTemplate>
                           <%-- <EditItemTemplate>
                                                                <asp:TextBox ID="txtEsn" CssClass="copy10grey" MaxLength="35"  Text='<%# Eval("ESN") %>' runat="server"></asp:TextBox>
                            </EditItemTemplate>--%>
                                                
                        </asp:TemplateField>
                                            

                        <asp:TemplateField HeaderText="Assigned Qty" SortExpression="AssignedQty" ItemStyle-Width="25%" ItemStyle-CssClass="copy10grey">
                            <ItemTemplate>
                                <%--<asp:LinkButton ID="lnkQty" runat="server" CssClass="linkgrey" 
                                    CommandArgument='<%# Eval("POD_ID") %>'  OnCommand="lnkESN_Command" AlternateText="View ESN"> --%>
                                 <%--  <b style="text-decoration:underline">--%>
                                       <%# Convert.ToInt32(Eval("AssignedQty")) == 0 ? "" : Eval("AssignedQty") %>

                                  <%-- </b> --%> 
                            <%--</asp:LinkButton>--%>
                    
                            </ItemTemplate>
                           <%-- <EditItemTemplate><%#Eval("AssignedQty")%>
                                <asp:TextBox ID="txtMslNumber" CssClass="copy10grey" MaxLength="30"  Text='<%# Eval("MslNumber") %>' runat="server"></asp:TextBox>
                            </EditItemTemplate>--%>
                                                
                        </asp:TemplateField>
            
            <%--<asp:TemplateField HeaderText="ICCID" SortExpression="ICCID"  ItemStyle-CssClass="copy10grey" ItemStyle-Width="25%">
                            <ItemTemplate><%#Eval("LTEICCID")%></ItemTemplate>
                        </asp:TemplateField>--%>
                              
                        <%--<asp:TemplateField HeaderText=""   ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
                            <ItemTemplate>
                    
                                <asp:CheckBox ID="chkDel" Visible='<%#Eval("IsDelete")%>' runat="server" />
                                <asp:HiddenField ID="hdPODID" runat="server" Value='<%#Eval("POD_ID")%>' />

                            </ItemTemplate>
                        </asp:TemplateField>--%>
                                            
                
                        <%--<asp:CommandField HeaderText="Edit" ShowEditButton="True" HeaderStyle-CssClass="button" ControlStyle-CssClass="linkgrid"    ItemStyle-HorizontalAlign="Center"/>
		
                        <asp:TemplateField HeaderText=""  ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
             
            
                         <asp:ImageButton ToolTip="Edit PODetail" CausesValidation="false" OnCommand="img2EditPOD_Click" CommandArgument='<%# Eval("PodID") %>' ImageUrl="~/Images/edit.png" 
                         ID="imgEditPOD"  runat="server" />
                         <asp:ImageButton ID="imgDelPoD" runat="server" OnClientClick="return confirm('Are you sure you want to delete?')"  
                         CommandName="Delete" AlternateText="Delete POD" ImageUrl="~/images/delete.png" />
                                                        
                                </ItemTemplate>
                                </asp:TemplateField>   --%>                                                
			                                
                    </Columns>
                </asp:GridView>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <br />
                    <%--<asp:Button ID="btnESNDelete" Visible="false"   CssClass="button" runat="server"  Text="Unassign ESN"  />
                     &nbsp;<a id="lnk_Print"  href="#" style="height:30px !important; line-height:40px !important; width:150px" class="button" Visible="false" target="_blank" runat="server"><span style="height:30px !important; line-height:40px !important; width:150px" class="button"> Print </span></a>--%>

                    <asp:Button ID="btnPrint" Visible="false"   CssClass="button" runat="server" OnClick="btnPrint_Click" Text="Generate Label"  />
                    &nbsp;<asp:Button ID="btnDownload"  Visible="false"  CssClass="button" runat="server" OnClick="btnDownload_Click" Text="Download"  />
                    &nbsp;<asp:Button ID="btnClosew" runat="server" Text="Close Window" CssClass="button" Visible="true" OnClientClick="return close_window();"  />
                       
                </td>
            </tr>
            <tr>
                <td colspan="2" class="copy10grey">
                    
                    <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="100%" align="center" >
                           <tr bordercolor="#839abf">
                                <td>
                                    
                                        
                            <asp:Label ID="lblSKUESN" runat="server" ></asp:Label>
                                        <asp:Repeater ID="rptSKUESN" runat="server">
                                            <HeaderTemplate>
                                            <table width="100%" align="center">
                                                <tr >
                                                    <td class="buttongrid" width="2%">
                                                        &nbsp;S.No.
                                                    </td>
                                                    <td class="buttongrid">
                                                        &nbsp;ESN
                                                    </td>
                                                    <td class="buttongrid">
                                                        &nbsp;Hex
                                                    </td>
                                                    <td class="buttongrid">
                                                        &nbsp;Dec
                                                    </td>
                                                    <td class="buttongrid">
                                                        &nbsp;Serial#
                                                    </td>
                                                    <td runat="server" visible='<%# ContainerID == "" ? false:true%>' class="buttongrid" >
                                                        &nbsp;PalletID
                                                    </td>
                                                    <td runat="server" visible='<%# ContainerID == "" ? false:true%>' class="buttongrid" >
                                                        &nbsp;ContainerID
                                                    </td>
                                                    <td class="buttongrid">
                                                        &nbsp;BoxID
                                                    </td>
                                                </tr>
                            
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                            <tr class="<%# Container.ItemIndex % 2 == 0 ? "alternaterow" : "" %>">
                                                
                                                <td class="copy10grey">
                                                        &nbsp;<%# Container.ItemIndex + 1 %></td>
                                
                                                <td class="copy10grey">
                                                        &nbsp;<%# Eval("ESN")%></td>
                                                <td class="copy10grey">
                                                        &nbsp;<%# Eval("Hex")%></td>
                                                <td class="copy10grey">
                                                        &nbsp;<%# Eval("Dec")%></td>
                                                <td class="copy10grey">
                                                        &nbsp;<%# Eval("SerialNumber")%></td>
                                                <td class="copy10grey" runat="server" visible='<%# Convert.ToString(Eval("ContainerID")) == "" ? false:true%>' >
                                                        &nbsp;<%# Eval("PalletID")%></td>
                                                <td class="copy10grey" runat="server" visible='<%# Convert.ToString(Eval("ContainerID")) == "" ? false:true%>' >
                                                        &nbsp;<%# Eval("ContainerID")%></td>
                                                <td class="copy10grey">
                                                        &nbsp;<%# Eval("BoxID")%></td>
                                
                                            </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                </table>    
                                            </FooterTemplate>
                                            </asp:Repeater>
                        
                            </td>
                                        </tr>
                                        </table>

                </td>
            </tr>
        </table>
         <br />
 

                <table width="100%" align="center">
                <tr>
                    <td align="center">
                         <asp:Button ID="btnCancel" runat="server" Text="Close" CssClass="button" Visible="true" OnClientClick="return close_window();"  />
                    </td>
                </tr>
                </table>
                
        </asp:PlaceHolder>
	</ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="btnPrint" />
        <asp:PostBackTrigger ControlID="btnDownload" />
    </Triggers>
    </asp:UpdatePanel>

          
<br /><br /> <br />
            <br /> <br />
        <table cellspacing="0" cellpadding="0" border="0" align="center" width="100%">
        <tr>
            <td >
                <foot:MenuFooter ID="footer2" runat="server"></foot:MenuFooter>
            </td>
         </tr>
         </table>
    </form>
</body>
</html>
