<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InvalidEsnReport.aspx.cs" Inherits="avii.Reports.InvalidEsnReport" %>

<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="~/Controls/Header.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Invalid ESN/MEID</title>

    <script type="text/javascript">
        function formatParentCatDropDown(objddl) {

            for (i = 0; i < objddl.options.length; i++) {
                objddl.options[i].innerHTML = objddl.options[i].innerHTML.replace(/&amp;/g, '&');
            }
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
            &nbsp;&nbsp;Invalid ESN/MEID
			</td>
		</tr>
    
    </table>
      <table cellSpacing="0" cellPadding="0" align="center" width="95%" border="0">
        <tr>
			<td>
    <asp:UpdatePanel ID="upnlESN" UpdateMode="Conditional" runat="server">
	<ContentTemplate>
	
    <table cellSpacing="0" cellPadding="0" align="center" width="100%" border="0">
    		<tr>                    
                <td><asp:Label ID="lblMsg" runat="server"  CssClass="errormessage"></asp:Label></td>
            </tr> 
     </table> 
        
        
      <table bordercolor="#839abf" border="1" width="100%" align="center" cellpadding="0" cellspacing="0">
      <tr>
        <td>
         <asp:Panel  ID="pnlSearch" runat="server"  DefaultButton="btnSearch" >
         <table width="100%" border="0" class="box" align="center" cellpadding="2" cellspacing="2">   
            <tr>
                <td  class="copy10grey" align="right" width="15%">
                        Customer: &nbsp;</td>
                <td align="left"  width="30%">
                      <asp:DropDownList ID="dpCompany" CssClass="copy10grey" runat="server" Width="80%"
                        OnSelectedIndexChanged="dpCompany_SelectedIndexChanged"  
                        AutoPostBack="true">
						</asp:DropDownList>

                </td>
                <td  width="1%">
                    &nbsp;
                </td>
                <td class="copy10grey"  align="right" width="15%">
                   Assigned:
                </td>
                <td>
                    <asp:CheckBox ID ="chkAssigned" runat="server" CssClass="copy10grey" />
                </td>
            </tr>
            <tr>
                <td class="copy10grey"  align="right">
                   Category:
                </td>
                <td>
                    <asp:DropDownList ID="ddlCategory" runat="server" CssClass="copy10grey" AutoPostBack="false"
                    Width="80%" ></asp:DropDownList>

                </td>
                <td  width="1%">
                    &nbsp;
                </td>
                <td class="copy10grey"  align="right" width="15%">
                   SKU:
                </td>
                <td >
                    <asp:DropDownList ID="ddlSKU" runat="server" class="copy10grey" Width="70%">
                    </asp:DropDownList>
                                   
                </td>   
                </tr>
                
                
                <tr>
                <td colspan="5">
                <hr />
                </td>
                </tr>
                <tr>
                <td  align="center"  colspan="5">
            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="button" OnClick="btnSearch_Click" CausesValidation="false"/>
            
         &nbsp;
           <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="button" OnClick="btnCancel_Click" CausesValidation="false"/>
                   
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
                            <td align="left">
                                <asp:Label ID="lblCount" CssClass="copy10grey" runat="server" ></asp:Label>
                            </td>
                            <td align="right">
                                <asp:Button ID="btnDownload" Visible="false" runat="server" Text=" Download " CssClass="button" OnClick="btnDownload_Click" 
                    CausesValidation="false" />  

                            </td>
                        </tr>
                        <tr>
                            <td colspan="2"  align="center">
    
                      <asp:GridView ID="gvESN" AutoGenerateColumns="false"  
                        Width="100%" ShowHeader="true"  ShowFooter="false" runat="server" GridLines="Both" 
                        OnPageIndexChanging="gvESN_PageIndexChanging" PageSize="20" AllowPaging="true" 
                        AllowSorting="true" OnSorting="gvESN_Sorting">
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
                                <asp:TemplateField HeaderText="Category Name" SortExpression="CategoryName" HeaderStyle-CssClass="buttonundlinelabel"  
                                    ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="15%">
                                    <ItemTemplate>
                                        <%--<asp:HiddenField ID="hdnSKUId" Value='<%# Eval("ItemcompanyGUID")%>' runat="server" />--%>
                                        
                                        <%# Eval("CategoryName")%>                                   
                                    </ItemTemplate>
                                </asp:TemplateField> 
                                <asp:TemplateField HeaderText="Product Name" SortExpression="ProductName" HeaderStyle-CssClass="buttonundlinelabel"   
                                    ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="20%">
                                    <ItemTemplate>
                                        <%# Eval("ProductName")%>
                                        </ItemTemplate>
                                </asp:TemplateField>  
                                <asp:TemplateField HeaderText="SKU" SortExpression="SKU" HeaderStyle-CssClass="buttonundlinelabel"  ItemStyle-HorizontalAlign="Left" 
                                    ItemStyle-CssClass="copy10grey" ItemStyle-Width="20%">
                                    <ItemTemplate>
                                        <%# Eval("SKU")%>
                                        </ItemTemplate>
                                </asp:TemplateField>  
                              <asp:TemplateField HeaderText="ESN" SortExpression="ESN" HeaderStyle-CssClass="buttonundlinelabel"  ItemStyle-HorizontalAlign="Left" 
                                  ItemStyle-CssClass="copy10grey" ItemStyle-Width="15%">
                                    <ItemTemplate>  
                                        <table cellpadding="3" cellspacing="3" style="width:100%; background-color:<%#Eval("ESNColor")%>; height:100%">
                                        <tr>
                                        <td>
                                            <%#Eval("ESN")%>
                                        </td>
                                        </tr>
                                        </table>
                   
                                    </ItemTemplate>
                                </asp:TemplateField>   
                              <asp:TemplateField HeaderText="MEIDDEC" SortExpression="MEID"  HeaderStyle-CssClass="buttonundlinelabel"  HeaderStyle-HorizontalAlign="Center" 
                                  ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="15%">
                                    <ItemTemplate>   
                                        <table cellpadding="3" cellspacing="3" style="width:100%; background-color:<%#Eval("MeidColor")%>; height:100%">
                                        <tr>
                                        <td>
                                            <%#Eval("MEID")%>
                                        </td>
                                        </tr>
                                        </table>
                   
                                    </ItemTemplate>
                                </asp:TemplateField> 
                                <asp:TemplateField HeaderText="Fulfillment#" SortExpression="FulfillmentNumber" HeaderStyle-CssClass="buttonundlinelabel"  ItemStyle-HorizontalAlign="Left" 
                                    ItemStyle-CssClass="copy10grey" ItemStyle-Width="14%">
                                    <ItemTemplate>
                                        <%# Eval("FulfillmentNumber")%>
                                        </ItemTemplate>
                                </asp:TemplateField>  
                              
                            </Columns>
                        </asp:GridView>
                            
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>

                </td>
            </tr>            
            </table>
                </td>
            </tr>
            <tr>
                <td>

                </td>
                <td>

                </td>
            </tr>
            </table>
            </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnDownload" />
        </Triggers>
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
                </td>
    </tr>

    </table>
        <br /> <br />
            <br /> <br />
        <table width="100%">
        <tr>
		    <td>
			    <foot:MenuFooter id="Menuheader2" runat="server"></foot:MenuFooter>
		    </td>
	    </tr>
        </table>
  
    </form>
    <script type="text/javascript">
        formatParentCatDropDown(document.getElementById("<%=ddlCategory.ClientID %>"));

        var prm = Sys.WebForms.PageRequestManager.getInstance();
    if (prm != null) {
        prm.add_endRequest(function (sender, e) {
            if (sender._postBackSettings.panelsToUpdate != null) {
                formatParentCatDropDown(document.getElementById("<%=ddlCategory.ClientID %>"));
            }
        });
    };
    </script>
    
</body>
</html>
