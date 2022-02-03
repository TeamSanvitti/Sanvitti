<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="manage-product.aspx.cs" Inherits="avii.product.manage_products" ValidateRequest="false" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>
<%@ Register TagPrefix="menu" TagName="Menu" Src="~/Controls/Header.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >

<head runat="server">
    <title>Admin - Products Query</title>
     <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>

    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.4.0/jquery.min.js"></script>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/jquery-ui.min.js"></script>
    <link href="https://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/themes/start/jquery-ui.css"
    rel="stylesheet" type="text/css" />
<%--<script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/jquery-ui.js" type="text/javascript"></script>
<link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/themes/start/jquery-ui.css"
    rel="stylesheet" type="text/css" />--%>

	<%--<script type="text/javascript" src="../JQuery/jquery.min.js"></script>
    <script type="text/javascript" src="../JQuery/jquery-ui.min.js"></script>--%>

	<script type="text/javascript" src="../JQuery/jquery.blockUI.js"></script>
	
     <link href="../../aerostyle.css" type="text/css" rel="stylesheet" />
    <%--<link href="../Styles.css" type="text/css" rel="stylesheet" />--%>

    <script language="javascript" type="text/javascript">
        function formatParentCatDropDown(objddl) {

            for (i = 0; i < objddl.options.length; i++) {
                objddl.options[i].innerHTML = objddl.options[i].innerHTML.replace(/&amp;/g, '&');
            }
        }
        function addNewItem()
        {
            location.href='manageproduct.aspx';
        }
        
        function setconfirmAlert()        {
            var vFlag=confirm('This Item is used in order, want to delete!');
            if(vFlag)
            {
                document.getElementById('hdnFlag').value=1;
                return true;
            }    
            else
               return false;
        }
        
        function deleteItemCheck(obj)
        {
            var vFlag;
            objOrderFlag = document.getElementById(obj.id.replace('lnkItemDelete', 'hdnOrder'));
            document.getElementById('hdnOrderFlag').value = objOrderFlag.value;
            if(objOrderFlag.value==1)
            {
                vFlag=confirm('Product used in some orders, want to delete this Product?');
                if(vFlag)
                    return true;
                else
                    return false;    
            }
            else
            {
                vFlag=confirm('Delete this Product?');
                if(vFlag)
                    return true;
                else
                    return false;    
            }
        }
    </script>
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
<body bgcolor="#ffffff" leftmargin="0" rightmargin="0" topmargin="0">
    <form id="frmProducts" runat="server" defaultbutton="btnSearch">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server" >
                </asp:ScriptManager>

        
        <asp:Label ID="lblmassege" runat="server"   />
        <%--<table cellspacing="0" cellpadding="0" border="0" align="center" width="100%">
        <tr>
            <td>--%>
                <menu:menu ID="HeadAdmin1" runat="server" ></menu:menu>
            <%--</td>
        </tr>
        </table>--%>
        <asp:UpdatePanel ID="UpdatePanel1"  runat="server" ChildrenAsTriggers="true" >
                    <ContentTemplate>
        <table cellspacing="0" cellpadding="0" border="0" align="center" width="95%">
        
        <tr valign="top">
           
            <td  class="copy10grey" align="left">
            <br />
               <div>
               
                <table width="100%">
                <tr>
                    <td class="buttonlabel" >
                    
                        <strong>Products Query</strong>        
                    </td>
                </tr>
                <tr><td>
                <asp:HiddenField ID="hdnFlag" runat="server" />
                <asp:HiddenField ID="hdnOrderFlag" runat="server" />
                <asp:Label ID="lblMsg" runat="server" CssClass="errormessage"></asp:Label></td></tr>
                <tr><td class="copy10grey">
                - Please select your search
                criterial to narrow down the search and record selection.<br />
                - Atleast one search criteria should be selected.
                
                </td></tr>
                <tr>
                    <td class="copy10grey" align="right"></td>
                </tr>
                <tr>
                    <td>
                    <table bordercolor="#839abf" border="1" width="100%" align="center" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <table width="100%" border="0" class="box" align="center">
                                    <tr valign="top">
                                        <td>
                                            &nbsp;
                                        </td>                                            
                                    </tr>
                                    <tr style="display:none">
                                            <td class="copy10grey" width="7%">Product Code:</td>
                                            <td  width="15%">
                                                        <asp:TextBox ID="txtItemCode" Width="90%" MaxLength="30"  runat="server" CssClass="copy10grey"></asp:TextBox>
                                            </td>
                                            <td class="copy10grey"  width="7%">Color:</td>
                                            <td  width="15%">
                                                <asp:TextBox ID="txtColor" Width="90%"  runat="server" CssClass="copy10grey"></asp:TextBox>
                                                </td>
                                            <td class="copy10grey"  width="7%">Maker:</td>
                                            <td  width="15%">
                                                        <asp:DropDownList ID="ddlMaker" runat="server" CssClass="copy10grey"  Width="90%">
                                                        </asp:DropDownList>
                                             </td>
                                    </tr>

                                    <tr style="line-height:18px">
                            
                            <td class="copy10grey">Customer:</td>
                            <td>
                                <asp:DropDownList ID="ddlCompany" runat="server" Width="90%"></asp:DropDownList>
                                
                            </td>
                                        <td class="copy10grey">SKU#:</td>                            
                            <td><asp:TextBox ID="txtSKU" Width="90%"  runat="server" CssClass="copy10grey"></asp:TextBox></td>
                            <td class="copy10grey">UPC:</td>
                            <td>
                                       <asp:TextBox ID="txtUPC"  Width="90%" MaxLength="9"  runat="server" CssClass="copy10grey"></asp:TextBox>     
                            </td>
                        </tr>
<tr>
    <td>&nbsp;</td>
</tr>
                                    <tr style="line-height:18px">
                                        <td class="copy10grey">Category:</td>
                            <td>
                                        <asp:DropDownList ID="ddlCategoryFilter" runat="server" 
                                            CssClass="copy10grey" Width="90%">
                                        </asp:DropDownList>
                            </td>
                            <td class="copy10grey">Model Number:</td>
                            <td>
                                <asp:TextBox ID="txtModel" Width="90%"  runat="server" CssClass="copy10grey"></asp:TextBox>

                            </td>
                            
                            <td class="copy10grey"> Active:  </td>  
                              <td>          
                                  <asp:DropDownList ID="ddlActive" Width="90%" runat="server">
                                <asp:ListItem Value="-1" Text="" ></asp:ListItem>
                                <asp:ListItem Value="1" Text="Active" ></asp:ListItem>
                                <asp:ListItem Value="0" Text="Inactive"></asp:ListItem>
                                </asp:DropDownList>
                                  
                           </td>
                        </tr> 
                                    
<tr>
    <td>&nbsp;</td>
</tr>
                         <tr style="line-height:18px">
                            
                            <td class="copy10grey">Product Type:</td>
                            <td>
                                 <asp:DropDownList ID="ddlProductType"  Width="80%"  CssClass="copy10grey" runat="server" >                                 
                              </asp:DropDownList>
                           
                                
                            </td>
                                        <td class="copy10grey">Product Condition:</td>                            
                            <td>
                                <asp:DropDownList ID="ddlCondition"  Width="90%"  CssClass="copy10grey" runat="server" >                                 
                              </asp:DropDownList>
                            

                            </td>
                            <td class="copy10grey">Re-Stock:</td>
                            <td>
                                       <asp:CheckBox ID="chkStock"   CssClass="copy10grey" runat="server" >                                 
                              </asp:CheckBox>
                           
                            </td>
                        </tr>

 <tr style="display:none">
                             <td class="copy10grey">Warehouse Code:</td>

                            <td>

 <asp:TextBox ID="txtWhCode" Width="90%"  runat="server" CssClass="copy10grey"></asp:TextBox>
                            <asp:DropDownList ID="ddlWhCode" Visible="false" runat="server" Width="90%"></asp:DropDownList>
                           

</td>
                            <td class="copy10grey">
                                
                            Carriers:
                            </td>                            
                            <td>
                             
                            <asp:DropDownList ID="ddlTechnology" runat="server" CssClass="copy10grey" Width="90%"/>
                            
                            </td>
                            <td class="copy10grey">
                            Catalog Product:
                            
                            </td>                            
                            <td>
                            <asp:DropDownList ID="ddlCatalog" Width="90%" runat="server">
                                <asp:ListItem Value="-1" Text="" ></asp:ListItem>
                                <asp:ListItem Value="1" Text="Show" ></asp:ListItem>
                                <asp:ListItem Value="0" Text="Do not show"></asp:ListItem>
                                </asp:DropDownList>
                            
                            </td>
                        </tr>                                     
<tr  >
                            <td colspan="6" align="center">
                                <br />  
                                <hr />
                                        <asp:Button ID="btnSearch" Text="Search" CssClass="button" runat="server"  OnClick="btnItemSearch_click" CausesValidation="False" />  &nbsp;&nbsp;                                          
                                        <asp:Button ID="btnCancel" Text="Cancel" CssClass="button" runat="server"  OnClick="btnCancel_click" CausesValidation="False" />       
                                     &nbsp;&nbsp;<asp:Button ID="btnAddNewitem" Visible="true" Text="Manage Product" CssClass="button" runat="server"  OnClick="btnAddNewitem_click" />
                                     
                                   <br />
                             </td>
                         </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                        
                        
                        <br />
                        
                        <table width="100%">
                            <tr>
                                <td class="copy10grey" colspan="2">
                                    <asp:HiddenField ID="hdnitemGUID" runat="server"  />
                                    <asp:GridView ID="grvItem" runat="server" CssClass="gridGray1" AutoGenerateColumns="false" Width="100%"  AllowPaging="true" PageSize="20"
                                       OnRowEditing="grvItem_RowEditing" OnPageIndexChanging="grvItem_PageIndexChanging" AllowSorting="true" OnSorting="grvItem_Sorting">
                                        <RowStyle BackColor="Gainsboro" />
                                        <AlternatingRowStyle BackColor="white" />
                                        <HeaderStyle  CssClass="buttongrid" ForeColor="white"/>
                                        <FooterStyle CssClass="white"  />
                                        <PagerStyle ForeColor="#636363" CssClass="copy10grey" HorizontalAlign="Left" />
                
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No." ItemStyle-CssClass="copy10grey"  ItemStyle-Width="1%" HeaderStyle-CssClass="buttongrid">
                                            <ItemTemplate>

                                                    <%# Container.DataItemIndex + 1%>
                  
                                            </ItemTemplate>
                                        </asp:TemplateField> 
                                            <asp:TemplateField HeaderStyle-CssClass="buttonundlinelabel" SortExpression="ItemName"
                                                HeaderStyle-HorizontalAlign="left" HeaderText="Product Name" 
                                                ItemStyle-VerticalAlign="top">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hdnitemID" runat="server" Value='<%# DataBinder.Eval( Container.DataItem, "itemGUID") %>' />
                                                    <asp:HiddenField ID="hdnOrder" runat="server"  />
                                                    <a class="linkgrey" href="detail-product.aspx?itemGUID=<%# Eval("itemGUID") %>">
                                                        <asp:Label ID="lblItemName" runat="server"  CssClass="copy10grey" 
                                                            Text='<%# DataBinder.Eval( Container.DataItem, "ItemName") %>' />
                                                    </a>
                                                </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-CssClass="buttonundlinelabel" SortExpression="ItemCategory"
                                                    HeaderStyle-HorizontalAlign="left" 
                                                    HeaderText="Category" ItemStyle-VerticalAlign="top">
                                                    <ItemTemplate>
                                                        
                                                            <asp:Label ID="lblItemCategory" runat="server"  CssClass="copy10grey" 
                                                                Text='<%# DataBinder.Eval( Container.DataItem, "ItemCategory") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-CssClass="buttonundlinelabel"  Visible="false"
                                                    HeaderStyle-HorizontalAlign="left" 
                                                    HeaderText="Brand" ItemStyle-VerticalAlign="top">
                                                    <ItemTemplate>
                                                        
                                                            <asp:Label ID="lblItemMaker" runat="server"  CssClass="copy10grey" 
                                                                Text='<%# DataBinder.Eval( Container.DataItem, "ItemMaker") %>' />
                                                        
                                                    </ItemTemplate>
                                                </asp:TemplateField>                                                    
                                                <asp:TemplateField HeaderStyle-CssClass="buttonundlinelabel"  Visible="false"
                                                    HeaderStyle-HorizontalAlign="left" HeaderText="Product Code" 
                                                    ItemStyle-VerticalAlign="top">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblItemcode" runat="server" CssClass="copy10grey" Text='<%# DataBinder.Eval( Container.DataItem, "ItemCode") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-CssClass="buttonundlinelabel" SortExpression="ModelNumber"
                                                    HeaderStyle-HorizontalAlign="left" HeaderText="Model Number" 
                                                    ItemStyle-VerticalAlign="top">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblItemModelNumber" CssClass="copy10grey" runat="server" Text='<%# DataBinder.Eval( Container.DataItem, "ModelNumber") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-CssClass="buttonundlinelabel" HeaderStyle-HorizontalAlign="left" SortExpression="ItemDesc1"
                                                    HeaderText="Description" ItemStyle-VerticalAlign="top">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblItemDescription" CssClass="copy10grey" runat="server" Text='<%# DataBinder.Eval( Container.DataItem, "itemDesc1") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField  Visible="false" HeaderStyle-CssClass="buttongrid" HeaderStyle-HorizontalAlign="left" HeaderText="Color" ItemStyle-VerticalAlign="top">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblItemColor" runat="server" CssClass="copy10grey"  Text='<%# DataBinder.Eval( Container.DataItem, "ItemColor") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-CssClass="buttonundlinelabel" HeaderStyle-HorizontalAlign="left" SortExpression="Upc"
                                                    HeaderText="UPC" ItemStyle-VerticalAlign="top">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblUPC" runat="server" CssClass="copy10grey" Text='<%# DataBinder.Eval( Container.DataItem, "upc") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField  Visible="false" HeaderStyle-CssClass="buttongrid" HeaderStyle-HorizontalAlign="left" 
                                                    HeaderText="Carriers" ItemStyle-VerticalAlign="top">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTechnology" runat="server" CssClass="copy10grey" Text='<%# DataBinder.Eval( Container.DataItem, "itemTechnology") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-CssClass="buttongrid" HeaderStyle-HorizontalAlign="left"  Visible="false"
                                                    HeaderText="Price($)" ItemStyle-VerticalAlign="top" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey">
                                                    <ItemTemplate>

                                                    <%# Convert.ToDouble(DataBinder.Eval( Container.DataItem, "DefaultPrice")) > 0 ?  Eval("DefaultPrice"): ""  %>
                                                        <%--<asp:Label ID="lblprice" runat="server" CssClass="copy10grey" Text='<%# DataBinder.Eval( Container.DataItem, "DefaultPrice") %>' />--%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField  Visible="false" HeaderStyle-CssClass="buttongrid" HeaderStyle-HorizontalAlign="left" 
                                                    HeaderText="Doc" ItemStyle-VerticalAlign="top">
                                                    <ItemTemplate>
                                                        <%--href="/Documents/Products/<%# Eval("ItemDocument") %>" onclick="Validate('<%# Eval("ItemDocument") %>');"--%>

                                                    <a href="/Documents/Products/<%# Eval("ItemDocument") %>" target="_blank"  style="display:<%# Convert.ToString(Eval("ItemDocument"))=="" ? "none": "block" %>" >
                                                    <img src="../images/view.png" title="Item Document" alt="Item Document" />
                                                    </a>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField  HeaderStyle-CssClass="buttonundlinelabel" HeaderStyle-HorizontalAlign="left" SortExpression="Active"
                                                    HeaderText="Status" ItemStyle-VerticalAlign="top">
                                                    <ItemTemplate>
                                                        <%# Convert.ToBoolean(Eval("Active"))==true ? "Active":"Inactive" %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField HeaderStyle-Width="80px" HeaderStyle-CssClass="buttongrid" HeaderText="Action">
                                                    <ItemTemplate>
                                                        <a href="detail-product.aspx?itemGUID=<%# Eval("itemGUID") %>"><asp:Image runat="server" ID="ImageEdit" src="../images/edit.png" alt="edit" style="border:0"/></a>
                                                        <asp:ImageButton ID="imgView" CommandArgument='<%# Eval("itemGUID") %>' OnCommand="imgView_Command" runat="server"  AlternateText="View Log" 
                                                            ImageUrl="../images/view.png" />                                                        
                                                        
                                                        <asp:ImageButton ID="lnkItemUpdate" Visible="false" runat="server" ImageUrl="../images/edit.png"   AlternateText="Edit Item" />
                                                        <asp:ImageButton ID="lnkItemDelete" CommandArgument='<%# Eval("itemGUID") %>' OnCommand="ItemDelete_click" OnClientClick="return deleteItemCheck(this);" runat="server"  CommandName="wDelete"  AlternateText="Delete Item" ImageUrl="../images/delete.png" />
                                                        
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
                </td>
            </tr>
            <tr><td>&nbsp;</td></tr>
            
        </table>
        </ContentTemplate>
        </asp:UpdatePanel>

<asp:UpdateProgress ID="UpdateProgress1" runat="server"
                     DynamicLayout="false">
            <ProgressTemplate>
                <img src="/Images/ajax-loaders.gif" /> Loading ...
            </ProgressTemplate>
        </asp:UpdateProgress>
        
        <table width="100%">
        <tr>
                <td>
                    <foot:MenuFooter ID="footer" runat="server"></foot:MenuFooter>
                </td>
            </tr>
        </table>
    </div>
    </form>
    <script type="text/javascript">
        formatParentCatDropDown(document.getElementById("<%=ddlCategoryFilter.ClientID %>"));

        var prm = Sys.WebForms.PageRequestManager.getInstance();
    if (prm != null) {
        prm.add_endRequest(function (sender, e) {
            if (sender._postBackSettings.panelsToUpdate != null) {
                formatParentCatDropDown(document.getElementById("<%=ddlCategoryFilter.ClientID %>"));
            }
        });
    };
    </script>
    
</body>
</html>

