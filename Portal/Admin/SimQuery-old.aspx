<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SimQuery.aspx.cs" Inherits="avii.Admin.SimQuery" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="/Controls/Footer.ascx" %>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="/Controls/Header.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Aerovoice Inc. - SIM Query</title>
    <link href="../aerostyle.css" type="text/css" rel="stylesheet" />
</head>
<body bgColor="#ffffff" leftmargin="0" rightmargin="0" topmargin="0">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
	<table cellspacing="0" cellpadding="0"  border="0" align="center" width="100%">
		<tr>
			<td>
			<head:menuheader id="HeadAdmin" runat="server"></head:menuheader>
			</td>
		</tr>
     </table>
    <br />
    <table  cellspacing="1" cellpadding="1" width="100%">
        <tr>
		    <td colspan="6" bgcolor="#dee7f6" class="button">&nbsp;&nbsp;SIM Query
		    </td>
        </tr>

    </table>  
    <asp:UpdatePanel ID="UpdatePanel1"  runat="server" UpdateMode="Conditional" >
    <ContentTemplate>
    
    <table cellSpacing="0" cellPadding="0" align="center" width="100%" border="0">
    		<tr>                    
                <td colspan="2">
                    <asp:Label ID="lblMsg" runat="server" Width="100%" CssClass="errormessage"></asp:Label></td>
            </tr>               
            
            <tr>
                <td align="center">

                    <table  cellSpacing="1" cellPadding="1" width="100%">
                        <tr><td class="copy10grey" align="left">&nbsp;
                        
                        
                        </td></tr>
                    </table>

            
                 <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="90%">
                    <tr>
                    <td>
                        <table  cellSpacing="5" cellPadding="5" width="100%" border="0">
                            <tr>
                                <td  class="copy10grey" align="right" width="15%" >
                                    Customer: &nbsp;</td>
                                <td align="left" width="35%" >
                                    <asp:DropDownList ID="dpCompany" CssClass="copy10grey" runat="server" Width="71%"
                                    OnSelectedIndexChanged="dpCompany_SelectedIndexChanged"  
                                    AutoPostBack="true">
									</asp:DropDownList>
                                <td class="copy10grey"  width="15%" align="right">
                                    <asp:Label ID="lblSKU" runat="server" Text="SKU:" CssClass="copy10grey"></asp:Label>
                                     &nbsp;
                                </td>
                                
                                <td  align="left" width="35%" >
                                <asp:DropDownList ID="ddlSKU" runat="server" class="copy10grey" Width="71%">
                                </asp:DropDownList>
                                            

                                </td>
                            </tr>
                            
                            <tr>
                                <td  class="copy10grey" align="right" >
                                    SIM: &nbsp;</td>
                                <td align="left" >
                                    
                                    <asp:TextBox ID="txtSIM" runat="server" MaxLength="30" CssClass="copy10grey" Width="70%"></asp:TextBox>
                                    </td>
                                <td class="copy10grey" align="right">
                                    ESN: &nbsp;
                                         </td>
                                <td class="copy10grey" align="left">
                                  <asp:TextBox ID="txtESN" runat="server" MaxLength="30" CssClass="copy10grey" Width="70%"></asp:TextBox>

                                </td>
                             </tr>
                             
                            <tr><td colspan="4">
                                        <hr style="width:100%" />
                            
                                        </td></tr>
                            <tr>
                                    <td align="center"  colspan="4">
                                        <asp:Button ID="btnSearch" runat="server" Text="Search " OnClick="search_click"  CausesValidation="false"
                                            CssClass="button" Height="24px" Width="130px" />&nbsp;
                                            

                                        
                                        <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Cancel"  Height="24px" Width="130px" 
                                        CausesValidation="false" CssClass="button" />

                                            
	
                                    </td>
                                </tr>   
                        </table>
                    </td>
                    </tr>
                           
                 </table>
                

                 
                   <table width="100%">
                        <tr>
                            <td align="right"> 
                                
                <asp:Label ID="lblCount" CssClass="copy10grey" runat="server" ></asp:Label>
                </td>
                </tr>


                <tr>
                    <td> 
                         <asp:GridView ID="gvSIM" OnPageIndexChanging="gridView_PageIndexChanging"  
                          AutoGenerateColumns="false"  
                            DataKeyNames="simID"  Width="100%"  
                        ShowFooter="false" runat="server" GridLines="Both"  
                        PageSize="50" AllowPaging="true" 
                        BorderStyle="Outset"
                        AllowSorting="false" > 
                        <RowStyle BackColor="Gainsboro" />
                        <AlternatingRowStyle BackColor="white" />
                        <HeaderStyle  CssClass="button" ForeColor="white"/>
                        <FooterStyle CssClass="white"  />
                        <PagerStyle ForeColor="#636363" CssClass="copy10grey" HorizontalAlign="Left" />
                        <Columns>
                            <%--<asp:TemplateField ItemStyle-CssClass="copy10grey"  HeaderText="" ItemStyle-Width="2%" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chk"  runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>                 
                            --%>
                            <asp:TemplateField ItemStyle-CssClass="copy10grey"  HeaderText="S.No." ItemStyle-Width="2%" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    
                                    <%# Container.DataItemIndex + 1 %>
                                   
                                </ItemTemplate>
                            </asp:TemplateField>                 
                            
                            <asp:TemplateField ItemStyle-CssClass="copy10grey"  HeaderText="Company Name" ItemStyle-Width="15%">
                                <ItemTemplate>
                                    <%# Eval("CompanyName")%>

                                    
                                </ItemTemplate>
                            </asp:TemplateField>                 
                            <asp:TemplateField ItemStyle-CssClass="copy10grey"  ItemStyle-Width="10%" HeaderText="SIM #">
                                <ItemTemplate>
                                    <%# Eval("sim")%>
                                    
                                </ItemTemplate>
                            </asp:TemplateField> 
                            <asp:TemplateField ItemStyle-CssClass="copy10grey"  ItemStyle-Width="10%" HeaderText="ESN">
                                <ItemTemplate>
                                    <%# Eval("esn")%>
                                    
                                </ItemTemplate>
                            </asp:TemplateField>  
                            <asp:TemplateField ItemStyle-CssClass="copy10grey"  ItemStyle-Width="10%" HeaderText="SKU">
                                <ItemTemplate>
                                    <%# Eval("sku")%>
                                    
                                </ItemTemplate>
                            </asp:TemplateField>                
                            <asp:TemplateField ItemStyle-CssClass="copy10grey"  ItemStyle-Width="10%" HeaderText="Upload Date">
                                <ItemTemplate>
                                <%--<%# DataBinder.Eval(Container.DataItem, "UploadDate", "{0:MM/dd/yyyy}")%>--%>
                                    <%# Convert.ToDateTime(Eval("UploadDate")).ToShortDateString() == "1/1/0001" ? "" : Eval("UploadDate")%>
                                </ItemTemplate>
                            </asp:TemplateField>                 
                            <asp:TemplateField ItemStyle-CssClass="copy10grey"  ItemStyle-Width="10%" HeaderText="Modified Date">
                                <ItemTemplate>
                                 <%# Convert.ToDateTime(Eval("ModifiedDate")).ToShortDateString() == "1/1/0001" ? "" : Eval("ModifiedDate")%>
                                    <%--<%# Eval("ModifiedDate")%>--%>
                                </ItemTemplate>
                            </asp:TemplateField>                 
                            <asp:TemplateField ItemStyle-CssClass="copy10grey"  ItemStyle-Width="10%" HeaderText="FulfillmentNumber">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "Fulfillment#")%>
                                </ItemTemplate>
                            </asp:TemplateField>                 
                            <asp:TemplateField ItemStyle-CssClass="copy10grey"  ItemStyle-Width="25%" HeaderText="RMA#">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "RmaNumber")%>
                                </ItemTemplate>
                            </asp:TemplateField>                 
                            <asp:TemplateField ItemStyle-CssClass="copy10grey"  ItemStyle-Width="5%" HeaderText="">
                                <ItemTemplate>
                                     <table><tr>
                                     
                                        <td>
                                            <%--<asp:ImageButton ID="imgView"  ToolTip="View RMA" OnCommand="imgViewRMA_Click"
                                             CausesValidation="false" CommandArgument='<%# Eval("rmaGUID") %>' ImageUrl="~/Images/view.png"  runat="server" />--%>
                                        </td>
                                
                                        
                                </tr></table>
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
    
 <asp:UpdatePanel ID="upnlJsRunner" UpdateMode="Always" runat="server">
			<ContentTemplate>
				<asp:PlaceHolder ID="phrJsRunner" runat="server"></asp:PlaceHolder>
			</ContentTemplate>
		</asp:UpdatePanel>


        <asp:UpdateProgress ID="UpdateProgress1" runat="server"
                     DynamicLayout="false">
            <ProgressTemplate>
                <img src="/Images/ajax-loaders.gif" /> Loading ...
            </ProgressTemplate>
        </asp:UpdateProgress>
        <br />
 
    <table cellspacing="0" cellpadding="0" border="0" align="center" width="100%">
        <tr>
            <td>
                <foot:MenuFooter ID="Foter" runat="server"></foot:MenuFooter>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
