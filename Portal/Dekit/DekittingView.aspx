<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DekittingView.aspx.cs" Inherits="avii.Dekit.DekittingView" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>
<%@ Register TagPrefix="menu" TagName="Menu" Src="~/Controls/Header.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Dekitting View</title>
</head>

<body bgcolor="#ffffff" leftmargin="0" rightmargin="0" topmargin="0">
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
                <td>&nbsp;Dekitting View</td></tr>
             </table>
     <asp:UpdatePanel ID="UpdatePanel1"  UpdateMode="Conditional" ChildrenAsTriggers="true" runat="server"   >
     <ContentTemplate>
     <table  align="center" style="text-align:left" width="100%">
     <tr>
        <td>
           <asp:Label ID="lblMsg" runat="server" CssClass="errormessage"></asp:Label>
            
        </td>
     </tr>
     </table>
      <table bordercolor="#839abf" border="1" width="100%" align="center" cellpadding="0" cellspacing="0">
      <tr>
        <td>
           
          <table width="100%" border="0" class="box" align="center" cellpadding="5" cellspacing="5">
             <tr style="height:1px">
             <td style="height:1px"></td>
             </tr>
                <tr  >
                <td class="copy10grey" align="right" width="15%">
                   <strong>Customer Name:</strong>
                </td>
                <td width="35%">
                    <asp:Label ID="lblCustomer" runat="server" CssClass="copy10grey"></asp:Label>

                   
                </td>
                <td  width="1%">
                    &nbsp;
                </td>
                <td class="copy10grey" align="right" width="15%">
                   Status:
                </td>
                <td width="35%">
                   
                          
                   <asp:Label ID="lblStatus" runat="server" CssClass="copy10grey"></asp:Label>
                </td>   
                
                    
                </tr>
            
                    <tr >
                    <td class="copy10grey" align="right" width="15%">
                        <strong>Dekit Request#:</strong>
                    </td>
                    <td width="35%">
                         <asp:Label ID="lblDekitRequestNo" runat="server" CssClass="copy10grey"></asp:Label>
                               
                
                    </td>
                    <td  width="1%">
                        &nbsp;
                    </td>
                    <td class="copy10grey" align="right" width="15%">
                        Customer Request#: 
                    </td>
                    <td width="35%">   
                        <asp:Label ID="lblCustomerRequestNo" runat="server" CssClass="copy10grey"></asp:Label>
                        
                    </td>   
                
                    
                    </tr>
                    <tr >
                    <td class="copy10grey" align="right" width="15%">
                        <strong>Kitted SKU#:</strong>
                    </td>
                    <td width="35%">
                        <asp:Label ID="lblSKU" runat="server" CssClass="copy10grey"></asp:Label>
                     
                
                    </td>
                    <td  width="1%">
                        &nbsp;
                    </td>
                    <td class="copy10grey" align="right" width="15%">
                        <strong>Approved By: </strong>
                    </td>
                    <td width="35%">
                            <asp:Label ID="lblRequestedBy" runat="server" CssClass="copy10grey"></asp:Label>
                                             
        
                    </td>
                    </tr>
              <tr >
                <td class="copy10grey" align="right" width="15%">
                    <strong>Dekitting Quantity:</strong>
                </td>
                <td width="35%">
                    <asp:Label ID="lblQty" runat="server" CssClass="copy10grey"></asp:Label>
                    
                </td>
                <td  width="1%">
                    &nbsp;
                </td>
                <td class="copy10grey" align="right" width="15%">
                    Created By:
                </td>
                <td width="35%">
                   
                                  <asp:Label ID="lblCreatedBy" runat="server" CssClass="copy10grey"></asp:Label>            
        
                </td>   
                
                    
                </tr>
                
                    <tr>
                        <td colspan="5">
                            
                               
                        </td>
                    </tr>
                   
            </table>
           
        </td>
     </tr>
     </table> 
         <asp:Panel ID="pnlSKU" runat="server" Width="100%">
         <br />
         <table align="center" style="text-align:left" width="100%">
                <tr class="buttonlabel" align="left">
                <td>&nbsp;RAW SKU#:</td></tr>
             </table>
            <asp:Repeater ID="rptSKUs" runat="server">
                <HeaderTemplate>                                    
                    <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="100%" align="center" >
                    <tr bordercolor="#839abf">
                        <td>
                    <table width="100%" border="0" cellpadding="2" cellspacing="2">                                  
                                
                </HeaderTemplate>
                <ItemTemplate>
                    <tr valign="top" class="<%# Container.ItemIndex % 2 == 0 ? "alternaterow" : "" %>">
                    <td class="copy10grey" align="right" width="15%">
                        SKU#:
                    </td>
                    <td width="35%" class="copy10grey">
                        
                        <asp:TextBox ID="txtsku" Enabled="false" runat="server" Text='<%# Eval("SKU")%>'   CssClass="copy10grey" MaxLength="30"  Width="80%"></asp:TextBox>
                                        
                    </td>
                    <td  width="1%">
                        &nbsp;
                    </td>
                    <td class="copy10grey" align="right" width="5%">
                        <%--ESN Starts#:--%>
                    </td>
                    <td width="10%">
                                        
                        <asp:TextBox ID="txtICCID" Visible="false" runat="server"  onkeypress="return isNumberKey(event);"  CssClass="copy10grey" MaxLength="18"  Width="80%"></asp:TextBox>
                       <%-- <asp:HiddenField ID="hdIsESNRequired" Value='<%# Eval("IsESNRequired")%>' runat="server" />--%>
                                        
                    </td>   
                        <td width="20%"  class="copy10grey">
                                        
                        Location: <%# Eval("WhLocation")%>    
                                                              
        
                    </td>  
                    
                    <td width="15%"  class="copy10grey">
                        <asp:HiddenField ID="hdnQty" Value='<%# Eval("Quantity")%>' runat="server" />
                                        
                        Required Qty: <%# Eval("Quantity")%>    
                                                              
        
                    </td>  
                    
                    </tr>
                                
                </ItemTemplate>
                <FooterTemplate>
                    </table> 
                    </td>
                    </tr>
                </table>
                </FooterTemplate>
                </asp:Repeater>
         </asp:Panel>
         <br />
        
             
      <table align="center" style="text-align:left" width="100%">
      <tr>
     <tr>
                <td  align="center"  colspan="5">
                        <table width="100%" cellpadding="0" cellspacing="0">
                             <tr>
                                <td align="left" width="50%" >
                                <asp:Label ID="lblCounts" CssClass="copy10grey" runat="server" ></asp:Label>
                                </td>
                                <td  align="right">
                                    
                                    <asp:Button ID="btnSubmit" Visible="true"  CssClass="button" runat="server" OnClick="btnSubmit_Click" Text="Approve" />
                                    &nbsp;<asp:Button ID="btnReject" Visible="true" CssClass="button" runat="server" OnClick="btnReject_Click" Text="Reject" />
                                    &nbsp;<asp:Button ID="btnCancel" Visible="true" runat="server" CssClass="button" Text="Cancel" OnClick="btnCancel_Click" />
                               </td>
                            </tr>
                        <tr>
                          <td>
                             
                            </td>
                            <td align="right">
                                <%--<asp:Button ID="btnValidate" runat="server" Text="Validate" CssClass="button"  OnClick="btnValidate_Click" />--%>
                                <asp:Label ID="lblCount" CssClass="copy10grey" runat="server" ></asp:Label>
                            </td>

                        </tr>
                        <tr>
                            <td colspan="2">
    
                        <asp:GridView ID="gvEsn"   AutoGenerateColumns="false"  
                        Width="100%" ShowHeader="true"  ShowFooter="false" runat="server" GridLines="Both" 
                        PageSize="50" AllowPaging="false" AllowSorting="false"  
                        >
                        <RowStyle BackColor="Gainsboro" />
                        <AlternatingRowStyle BackColor="white" />
                        <HeaderStyle  CssClass="buttonlabel" ForeColor="white"/>
                          <PagerStyle ForeColor="#636363" CssClass="copy10grey" HorizontalAlign="Left" />
                          <Columns>
                              <asp:TemplateField HeaderText="S.No." HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
                                   <%-- <HeaderTemplate>
                                        <asp:CheckBox  runat="server" ID="chkAll" />
                                    </HeaderTemplate>--%>
                                    <ItemTemplate>
                                        <%--<asp:CheckBox runat="server"  ID="chkPrint" />--%>
                                        &nbsp; 
                                        <%# Container.DataItemIndex + 1%>
                                    </ItemTemplate>
                              </asp:TemplateField> 
                              <asp:TemplateField HeaderText="SKU" ItemStyle-CssClass="copy10grey"  ItemStyle-Width="19%">
                                <ItemTemplate>

                                      <%# Eval("SKU")%>

                                </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField HeaderText="ESN#" SortExpression="ESN"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="30%">
                                  <ItemTemplate>
                                      <%# Eval("ESN")%>
                                        <%--<asp:Label ID="hdnSKUId" Visible="false" Text='<%# Eval("ItemcompanyGUID")%>' runat="server" />
                                        <asp:Label ID="hdSKU" Visible="false" Text='<%# Eval("SKU")%>' runat="server" />
                                        <table cellpadding="3" cellspacing="3" width="100%" >
                                        <tr>
                                        <td width="70%">

                                             <asp:TextBox ID="txtESN" CssClass="copy10grey" Width="100%" onkeypress="return isNumberKey(event);"  runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ESN")%>'></asp:TextBox>
                                            
                                        </td>
                                            
                                        </tr>
                                        </table>    --%>                               
                                    </ItemTemplate>
                                </asp:TemplateField> 
                              <asp:TemplateField Visible="false" HeaderText="Mapped SKU" ItemStyle-CssClass="copy10grey"  ItemStyle-Width="19%">
                                <ItemTemplate>
                                      <%# Eval("MappedSKU")%>
                                    </ItemTemplate>
                                </asp:TemplateField> 
                              <asp:TemplateField Visible="false" HeaderText="ICCID" SortExpression="ICCID"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="30%">
                                    <ItemTemplate>
                                        <%# Eval("ICCID") %>
                                        <%--<asp:Label ID="hdnMappedItemCompanyGUID" Visible="false" Text='<%# Eval("MappedItemCompanyGUID")%>' runat="server" />
                                        <asp:Label ID="lblICCID" Visible="false" Text='<%# Eval("ICCID")%>' runat="server" />
                                        <table cellpadding="3" cellspacing="3" width="100%" >
                                        <tr>
                                        <td width="70%">
                                            
                                             <asp:TextBox ID="txtICCID" Enabled='<%# Convert.ToInt32(Eval("MappedItemCompanyGUID")) > 0 ? true : false %>' CssClass="copy10grey" Width="100%" onkeypress="return isNumberKey(event);"  runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ICCID")%>'></asp:TextBox>
                                            
                                        </td>
                                            <td>
                                                <span class="errormessage">
                                                   <%# Eval("ICCIDValidationMsg") %>
                                                </span>
                                            </td>
                                        </tr>
                                        </table>--%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                              <asp:TemplateField Visible="true" HeaderText="Location" ItemStyle-CssClass="copy10grey"  ItemStyle-Width="19%">
                                <ItemTemplate>
                                      <%# Eval("WhLocation")%>
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
    

            <asp:UpdateProgress ID="UpdateProgress1" runat="server"
                     DynamicLayout="false">
            <ProgressTemplate>
                <img src="/Images/ajax-loaders.gif" /> Loading ...
            </ProgressTemplate>
        </asp:UpdateProgress>
        </td>
      </tr>
    

      </table>
        


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
