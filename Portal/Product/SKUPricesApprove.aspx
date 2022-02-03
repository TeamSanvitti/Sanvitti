<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SKUPricesApprove.aspx.cs" Inherits="avii.Product.SKUPricesApprove" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="/Controls/Footer.ascx" %>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="/Controls/Header.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Lan Global inc. Inc. - Approve SKU/Product Prices</title>
    <link href="../aerostyle.css" type="text/css" rel="stylesheet" />
    <script type="text/javascript" src="../JQuery/jquery-latest.js"></script>
    
    <script type="text/javascript">

        function Validate() {
            var options = document.getElementById("<% =ddlOption.ClientID %>");
            if (options.selectedIndex > 0) {
                var company = document.getElementById("<% =dpCompany.ClientID %>");
                if (company.selectedIndex == 0) {
                    alert('Customer is required!');
                    return false;
                }
            }
        }
    </script>
    <script type="text/javascript">
        var allCheckBoxSelector = '#<%=gvSKUPrice.ClientID%> input[id*="chkAll"]:checkbox';
        var checkBoxSelector = '#<%=gvSKUPrice.ClientID%> input[id*="chkSKU"]:checkbox';

        function ToggleCheckUncheckAllOptionAsNeeded() {
            var totalCheckboxes = $(checkBoxSelector),
         checkedCheckboxes = totalCheckboxes.filter(":checked"),
         noCheckboxesAreChecked = (checkedCheckboxes.length === 0),
         allCheckboxesAreChecked = (totalCheckboxes.length === checkedCheckboxes.length);

            $(allCheckBoxSelector).attr('checked', allCheckboxesAreChecked);
        }

        $(document).ready(function () {
            $(allCheckBoxSelector).live('click', function () {
                $(checkBoxSelector).attr('checked', $(this).is(':checked'));

                ToggleCheckUncheckAllOptionAsNeeded();
            });

            $(checkBoxSelector).live('click', ToggleCheckUncheckAllOptionAsNeeded);

            ToggleCheckUncheckAllOptionAsNeeded();
        });
</script>


</head>
<body  bgColor="#ffffff" leftmargin="0" rightmargin="0" topmargin="0">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
	<table cellSpacing="0" cellPadding="0"  border="0" align="center" width="100%">
		<tr>
			<td>
			<head:menuheader id="HeadAdmin" runat="server"></head:menuheader>
			</td>
		</tr>
     </table>
    <br />
    <table  cellSpacing="1" cellPadding="1" width="100%">
        <tr>
		    <td colSpan="6" bgcolor="#dee7f6" class="button">&nbsp;&nbsp;Approve SKU/Product Prices
		    </td>
        </tr>

    </table>   
    <br />
    <asp:UpdatePanel ID="UpdatePanel1"  runat="server" UpdateMode="Conditional" >
    <ContentTemplate>
    
        <table cellSpacing="0" cellPadding="0" align="center" width="100%" border="0">
    	<tr>                    
            <td colspan="2">
                <asp:Label ID="lblMsg" runat="server" Width="100%" CssClass="errormessage"></asp:Label>
            </td>
        </tr>               
        <tr>
            <td align="center">
                <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="90%">
                    <tr>
                    <td>
			            <table  cellSpacing="5" cellPadding="5" width="100%" border="0">
                            <tr>
                                <td  class="copy10grey" align="right" width="42%" >
                                    Product/SKU: &nbsp;</td>
                                <td align="left" >
                                    <asp:DropDownList ID="ddlOption" CssClass="copy10grey" runat="server" Width="55%" >
                                    <%--<asp:ListItem Text="" Value="0"></asp:ListItem>--%>
                                    <asp:ListItem Text="Product" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="SKU" Value="2"></asp:ListItem>
									</asp:DropDownList>
                            </tr>
                            
                            <tr>
                                <td  class="copy10grey" align="right" width="42%" >
                                    Customer: &nbsp;</td>
                                <td align="left" >
                                    <asp:DropDownList ID="dpCompany" CssClass="copy10grey" runat="server" Width="55%" >
									</asp:DropDownList>
                            </tr>
                            
                
                             
                            <tr>
                                <td colspan="2">
                                    <asp:Panel ID="pnlSubmit" runat="server" Visible="false">
                                    <table cellpadding="0" cellspacing="0" width="100%">
                                        <tr><td>
                                        <hr style="width:100%" />
                            
                                        </td></tr>   
                                         <%--<tr>                    
                                            <td align="left">
                                                <asp:Label ID="lblConfirm" runat="server" Width="100%" CssClass="errorGreenMsg"></asp:Label></td>
                                        </tr>               
                                    --%>
                                         <tr>
                                            <td  align="center">
                                
                                
                                            <asp:Button ID="btnSubmit2" Visible="false" Width="190px"  CssClass="button" runat="server" OnClick="btnSubmit_Click" Text="Approve" OnClientClick="return Validate(2);" />

                                            &nbsp;
                                            <asp:Button ID="btnReject" Visible="false" Width="190px"  CssClass="button" runat="server" OnClick="btnReject_Click" Text="Reject" OnClientClick="return Validate(2);" />

                                            &nbsp;
                                            <asp:Button ID="btnCancel2" runat="server"  CssClass="button" Text="Cancel" OnClick="btnCancel_Click" />
                                                </td>
                                        </tr>
                                        <%--<tr><td>
                                        <hr style="width:100%" />
                            
                                        </td></tr>   --%>
                                    </table>

                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr><td colspan="2">
                            
                            <table cellpadding="0" cellspacing="0" width="100%">
                             <%--<tr>
                                <td colspan="2" align="right" style="height:8px; vertical-align:bottom">
                                <strong>   <asp:Label ID="lblCount" CssClass="copy10grey" runat="server" ></asp:Label> &nbsp;&nbsp;</strong> 
                                </td>
                             </tr>--%>
                             <tr>
                                <td colspan="2" align="center">
                                
                                <asp:GridView ID="gvSKUPrice" runat="server" Width="90%" GridLines="Both"   AutoGenerateColumns="false"
                                    OnPageIndexChanging="gridView_PageIndexChanging" PageSize="100" AllowPaging="false" 
                                    >
                                        <RowStyle BackColor="Gainsboro" />
                                        <AlternatingRowStyle BackColor="white" />
                                        <HeaderStyle  CssClass="button" ForeColor="white"/>
                                        <FooterStyle CssClass="white"  />
                                        <PagerStyle ForeColor="#636363" CssClass="copy10grey" HorizontalAlign="Left" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
                                                <HeaderTemplate>
                                                    <asp:CheckBox runat="server" Checked="true" ID="chkAll" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                
                                                    <asp:CheckBox runat="server"  Checked="true"  ID="chkSKU"  />
                                                </ItemTemplate>
                                            </asp:TemplateField> 
                                            <asp:TemplateField HeaderText="S.No." ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex +  1 %> 
                                                </ItemTemplate>
                                            </asp:TemplateField> 
                                            <asp:TemplateField HeaderText="ProductCode" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="15%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPCode" runat="server" CssClass="copy10grey" Text='<%#Eval("ProductCode")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField> 
                                            
                                            <asp:TemplateField HeaderText="SKU#" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="13%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSKU" runat="server" CssClass="copy10grey" Text='<%#Eval("SKU")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField> 
                                            <asp:TemplateField HeaderText="SKU Last Price($)"   ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="13%">
                                                <ItemTemplate>
<%--<%# Convert.ToInt32(Eval("SKULastPrice")) > 0 ?  Eval("SKULastPrice", "{0:n}") : "" %>--%>
                                                    <asp:Label ID="lblLastPrice" CssClass="copy10grey" Width="90%"  Text='<%# Convert.ToInt32(Eval("SKULastPrice")) > 0 ? Eval("SKULastPrice", "{0:n}") : "" %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField> 
                                            
                                            <asp:TemplateField HeaderText="Current Price($)"   ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="12%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSKUPrice" CssClass="copy10grey" Width="90%"  Text='<%# Convert.ToInt32(Eval("skuPrice")) > 0 ? Eval("skuPrice", "{0:n}") : "" %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField> 
                                            
                                            <asp:TemplateField HeaderText="Propose Price($)"   ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="12%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPrice" CssClass="copy10grey" Width="90%"  Text='<%# Convert.ToInt32(Eval("ProposePrice")) > 0 ? Eval("ProposePrice", "{0:n}") : "" %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField> 
                                            
                                            <asp:TemplateField HeaderText="Updated By" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="15%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblUser" runat="server" CssClass="copy10grey" Text='<%#Eval("username")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField> 
                                            <asp:TemplateField HeaderText="Change Date" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="20%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblChangeDate" runat="server" CssClass="copy10grey" Text='<%#Eval("ChangeDate")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField> 
                                            
                                        </Columns>
                                    </asp:GridView>
                                    

                                </td>
                            </tr>
                            </table>
                            </td></tr>                            
                            
                            <tr id="trHr" runat="server"><td colspan="2">
                            <hr style="width:100%" />
                            
                            </td></tr>                            
                             <tr>
                                <td colspan="2" align="center">
                                
                                <asp:Button ID="btnSearch"  Width="190px" CssClass="button" runat="server" OnClick="btnSearch_Click" Text="Search" OnClientClick="return Validate();"  />

                                &nbsp;<asp:Button ID="btnSubmit" Visible="false" Width="190px"  CssClass="button" runat="server" OnClick="btnSubmit_Click" Text="Approve" OnClientClick="return Validate();"/>
                                &nbsp;
                                            <asp:Button ID="btnReject2" Visible="false" Width="190px"  CssClass="button" runat="server" OnClick="btnReject_Click" Text="Reject" OnClientClick="return Validate(2);" />

                               <%-- &nbsp;<asp:Button ID="btnViewTracking" Visible="false" Width="190px"  CssClass="button" runat="server" OnClick="btnViewAssignedPos_Click" Text="View UPDATED fulfillment" />
--%>
                                &nbsp;<asp:Button ID="btnCancel" runat="server"  CssClass="button" Text="Cancel" OnClick="btnCancel_Click" />
                                    </td>
                            </tr>
                       </table>
                            
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
        <br />
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
