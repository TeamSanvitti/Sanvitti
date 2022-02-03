<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ESNSearch.aspx.cs" Inherits="avii.ESN.ESNSearch" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>
<%@ Register TagPrefix="menu" TagName="Menu" Src="~/Controls/Header.ascx" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ESN Replacement</title>
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
            <td   >
                <table align="center" style="text-align:left" width="100%">
                <tr class="buttonlabel" align="left">
                <td>&nbsp;Quarantine Search</td></tr>
             </table>
 <asp:UpdatePanel ID="UPl1"  UpdateMode="Conditional" ChildrenAsTriggers="true" runat="server"   >
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
             <asp:Panel  ID="pnlSearch" runat="server"  DefaultButton="btnSearch" >
         <table width="100%" border="0" class="box" align="center" cellpadding="5" cellspacing="5">
             <tr style="height:1px">
             <td style="height:1px"></td>
             </tr>
                <tr valign="top" id="trCustomer" runat="server">
                <td class="copy10grey" align="right" width="15%">
                    Customer Name:
                </td>
                <td width="35%">
                    <asp:DropDownList ID="dpCompany" CssClass="copy10grey" runat="server" Width="80%" TabIndex="1"
                                    OnSelectedIndexChanged="dpCompany_SelectedIndexChanged"  
                                    AutoPostBack="true">
									</asp:DropDownList>
                
                </td>
                <td  width="1%">
                    &nbsp;
                </td>
                <td class="copy10grey" align="right" width="15%">
                   <%--<asp:Label ID="lblStatus" runat="server" CssClass="copy10grey"></asp:Label>--%>
                </td>
                <td width="35%">
                   
                          
<%--                    <asp:DropDownList ID="ddlStatus" CssClass="copy10grey" runat="server" Width="80%">
	                </asp:DropDownList>  --%>            
        
                </td>   
                
                    
                </tr>
                <tr valign="top">
                <td class="copy10grey" align="right" width="15%">
                    Fulfillment#:
                </td>
                <td width="35%">
                    <asp:TextBox ID="txtPONumber" runat="server" CssClass="copy10grey" MaxLength="12"  TabIndex="2" Width="80%"></asp:TextBox>
                                        
         
                </td>
                <td  width="1%">
                    &nbsp;
                </td>
                <td class="copy10grey" align="right" width="15%">
                    ESN:
                    
                </td>
                <td width="35%">
                   
                        <asp:TextBox ID="txtESN" runat="server" onkeypress="return isNumberKey(event);"  TabIndex="4" CssClass="copy10grey"   Width="80%"></asp:TextBox>
                                                
        
                </td>   
                
                    
                </tr>
          
            
                    <tr valign="top">
                    <td class="copy10grey" align="right" width="15%">
                         SKU#:
                    </td>
                    <td width="35%">
                    
                    <asp:DropDownList ID="ddlSKU" CssClass="copy10grey"  TabIndex="3" runat="server" Width="80%">
	                </asp:DropDownList>           
                
                    </td>
                    <td  width="1%">
                        &nbsp;
                    </td>
                    <td class="copy10grey" align="right" width="15%">
                    </td>
                    <td width="35%">                                                    
                       
                    </td>   
                
                    
                    </tr>
                    
                <tr>
                <td colspan="5">
                <hr />
                </td>
                </tr>
                <tr>
                <td  align="center"  colspan="5">
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
                <td  align="center"  colspan="5">
                    <%--<asp:Panel ID="pnlPO" runat="server">--%>
                        <%--<PO:Status ID="pos1" runat="server" />--%>
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
                            <asp:GridView ID="gvESNs" AutoGenerateColumns="false"  
                            Width="100%" ShowHeader="true"  ShowFooter="false" runat="server" GridLines="Both" 
                            >                        
                            <RowStyle BackColor="Gainsboro" />
                            <AlternatingRowStyle BackColor="white" />
                            <HeaderStyle  CssClass="buttongrid" ForeColor="white"/>
                              <PagerStyle ForeColor="#636363" CssClass="copy10grey" HorizontalAlign="Left" />
                              <Columns>
                                    <asp:TemplateField HeaderText="S.No." ItemStyle-CssClass="copy10grey"  ItemStyle-Width="2%">
                                    <ItemTemplate>
                                          <%# Container.DataItemIndex + 1%>               
                                    </ItemTemplate>
                                    </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Fulfillment#" HeaderStyle-CssClass="buttongrid"   ItemStyle-HorizontalAlign="Left" 
                                        ItemStyle-CssClass="copy10grey" ItemStyle-Width="15%">
                                        <ItemTemplate>
                                            <%# Eval("PONumber")%>
                                            </ItemTemplate>
                                    </asp:TemplateField>  
                                  <asp:TemplateField HeaderText="Fulfillment Date" HeaderStyle-CssClass="buttongrid"   ItemStyle-HorizontalAlign="Left" 
                                        ItemStyle-CssClass="copy10grey" ItemStyle-Width="15%">
                                        <ItemTemplate>
                                            <%# Eval("PODate")%>
                                            </ItemTemplate>
                                    </asp:TemplateField>  
                                      
                                    <asp:TemplateField HeaderText="Category Name" SortExpression="CategoryName" HeaderStyle-CssClass="buttongrid"  ItemStyle-HorizontalAlign="Left" 
                                        ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <%# Eval("CategoryName")%>                                    
                                        </ItemTemplate>
                                    </asp:TemplateField> 
                                    <asp:TemplateField HeaderText="SKU" SortExpression="SKU" HeaderStyle-CssClass="buttongrid"  ItemStyle-HorizontalAlign="Left" 
                                        ItemStyle-CssClass="copy10grey" ItemStyle-Width="15%">
                                        <ItemTemplate>
                                            <%# Eval("SKU")%>
                                            </ItemTemplate>
                                    </asp:TemplateField>  
                                    <asp:TemplateField HeaderText="ESN" HeaderStyle-CssClass="buttongrid"  ItemStyle-HorizontalAlign="Left"
                                      ItemStyle-CssClass="copy10grey" ItemStyle-Width="7%">
                                        <ItemTemplate>                                                
                                             <%# Eval("ESN")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>   
                                    <asp:TemplateField HeaderText="Action" SortExpression="Action" HeaderStyle-CssClass="buttongrid"  ItemStyle-HorizontalAlign="Left" 
                                      ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                                        <ItemTemplate>                                                
                                             <asp:TextBox ID="txtESN" CssClass="copy10grey" runat="server" Width="90%"></asp:TextBox>
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
             <Triggers>
                 <asp:PostBackTrigger ControlID="btnDownload" />
             </Triggers>
           </asp:UpdatePanel>
                
<%--            <asp:UpdatePanel ID="upnlJsRunner" UpdateMode="Always" runat="server">
			<ContentTemplate>
				<asp:PlaceHolder ID="phrJsRunner" runat="server"></asp:PlaceHolder>
			</ContentTemplate>
		</asp:UpdatePanel>--%>

            <asp:UpdateProgress ID="UpdateProgress1" runat="server"
            DynamicLayout="false">
            <ProgressTemplate>
                <img src="/Images/ajax-loaders.gif" />Loading ...
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
