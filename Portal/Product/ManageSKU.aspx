<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageSKU.aspx.cs" Inherits="avii.Product.ManageSKU" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>
<%@ Register TagPrefix="menu" TagName="Menu" Src="~/Controls/Header.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Manage SKU</title>
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
       
        
       
    </script>
</head>
<body bgcolor="#ffffff" leftmargin="0" rightmargin="0" topmargin="0">
    <form id="form1" runat="server">
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
                    
                        <strong>Manage SKU</strong>        
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
                        <asp:Panel  ID="pnlSearch" runat="server"  DefaultButton="btnSearch" >
                    <table bordercolor="#839abf" border="1" width="100%" align="center" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>

                                <table width="100%" border="0" class="box" align="center">
                                    <tr valign="top">
                                        <td>
                                            &nbsp;
                                        </td>                                            
                                    </tr>
                                   

                                    <tr style="line-height:18px">
                            
                            <td class="copy10grey"><asp:Label ID="lblCust" runat="server" Text="Customer:"></asp:Label></td>
                            <td>
                                <asp:DropDownList ID="ddlCompany" runat="server" Width="90%"></asp:DropDownList></td>
                            
                                </td>
                                        <td class="copy10grey">Model Number:</td>                            
                            <td><asp:TextBox ID="txtModel" Width="90%"  runat="server" CssClass="copy10grey"></asp:TextBox></td>
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
                            <td class="copy10grey">SKU#: </td>
                            <td>
                                <asp:TextBox ID="txtSKU" Width="90%"  runat="server" CssClass="copy10grey"></asp:TextBox>
                                
                                
                            <td class="copy10grey"> Disabled SKU:  </td>  
                              <td>          
                                  <asp:CheckBox ID="chkDisable" runat="server" CssClass="copy10grey" /> 
                                  
                           </td>
                        </tr>                    

                                     
<tr  >
                            <td colspan="6" align="center">
                                <br />  
                                <hr />
                                        <asp:Button ID="btnSearch" Text="Search" CssClass="button" runat="server"  OnClick="btnItemSearch_click" CausesValidation="False" />  &nbsp;&nbsp;                                          
                                        <asp:Button ID="btnCancel" Text="Cancel" CssClass="button" runat="server"  OnClick="btnCancel_click" CausesValidation="False" />       
                                        <asp:Button ID="btnDownload" Text="Download" Visible="false" CssClass="button" runat="server"  OnClick="btnDownload_Click" CausesValidation="False" />       
                                     
                                   <br />
                             </td>
                         </tr>
                                </table>
                            
                        </asp:Panel>
                            </td>
                        </tr>
                    </table>
                        
                        <br />
                        
                        <table width="100%">
                            <tr>
                                <td class="copy10grey" >
                                    <asp:HiddenField ID="hdnitemGUID" runat="server"  />
                                    <asp:GridView ID="grvItem" runat="server"     AutoGenerateColumns="false" Width="100%"  AllowPaging="true" PageSize="20"
                                        OnPageIndexChanging="grvItem_PageIndexChanging" AllowSorting="true" OnSorting="grvItem_Sorting">
                                        <RowStyle BackColor="Gainsboro" />
                                        <AlternatingRowStyle BackColor="white" />
                                        <HeaderStyle  CssClass="buttongrid" ForeColor="white"/>
                                        <FooterStyle CssClass="white"  />
                                           <SortedAscendingHeaderStyle  Font-Underline="true" />
                                           <SortedDescendingHeaderStyle   Font-Underline="true" />
                                        <PagerStyle ForeColor="#636363" CssClass="copy10grey" HorizontalAlign="Left" />
                
                                        <Columns>
                                             <asp:TemplateField HeaderStyle-CssClass="buttongrid" 
                                                    HeaderStyle-HorizontalAlign="left" 
                                                    HeaderText="Customer Name" ItemStyle-VerticalAlign="top">
                                                    <ItemTemplate>
                                                        
                                                            <%# DataBinder.Eval( Container.DataItem, "CompanyName") %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-CssClass="buttonundlinelabel"  SortExpression="ItemCategory"
                                                    HeaderStyle-HorizontalAlign="left" 
                                                    HeaderText="Category" ItemStyle-VerticalAlign="top">
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval( Container.DataItem, "ItemCategory") %>
                                                            <%--<asp:Label ID="lblItemCategory" runat="server"  CssClass="copy10grey" 
                                                                Text='<%# DataBinder.Eval( Container.DataItem, "ItemCategory") %>' />--%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-CssClass="buttonundlinelabel"  SortExpression="SKU"
                                                    HeaderStyle-HorizontalAlign="left" HeaderText="SKU#" 
                                                    ItemStyle-VerticalAlign="top">
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval( Container.DataItem, "SKU") %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            
                                             
                                            <asp:TemplateField HeaderStyle-CssClass="buttonundlinelabel"   SortExpression="ItemName"
                                                HeaderStyle-HorizontalAlign="left" HeaderText="Product Name" 
                                                ItemStyle-VerticalAlign="top">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hdItemCompanyGUID" runat="server" Value='<%# DataBinder.Eval( Container.DataItem, "ItemCompanyGUID") %>' />
                                                    <asp:HiddenField ID="hdIsDisable" runat="server" Value='<%# DataBinder.Eval( Container.DataItem, "IsDisable") %>' />
                                                    
                                                    <asp:HiddenField ID="hdnitemID" runat="server" Value='<%# DataBinder.Eval( Container.DataItem, "itemGUID") %>' />
                                                    <asp:LinkButton ID="lnkItem" CssClass="linkgrey" runat="server" CommandArgument='<%# DataBinder.Eval( Container.DataItem, "itemGUID") %>' 
                                                        OnCommand="lnkItem_Click">
                                                        <b><%# DataBinder.Eval( Container.DataItem, "ItemName") %></b>
                                                    </asp:LinkButton>
                                                   <%-- <a class="linkgrey" href="SKUDetail.aspx?itemGUID=<%# Eval("itemGUID") %>">
                                                      <b><%# DataBinder.Eval( Container.DataItem, "SKU") %></b>
                                                    </a>--%>
                                                </ItemTemplate>
                                                </asp:TemplateField>
                                                                                                 
                                                
                                                <asp:TemplateField HeaderStyle-CssClass="buttonundlinelabel"  SortExpression="ModelNumber"
                                                    HeaderStyle-HorizontalAlign="left" HeaderText="Model Number" 
                                                    ItemStyle-VerticalAlign="top">
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval( Container.DataItem, "ModelNumber") %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField HeaderStyle-CssClass="buttonundlinelabel" HeaderStyle-HorizontalAlign="left" 
                                                    HeaderText="UPC" ItemStyle-VerticalAlign="top"  SortExpression="upc">
                                                    <ItemTemplate>
                                                      <%# DataBinder.Eval( Container.DataItem, "upc") %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                
                                                <asp:TemplateField   HeaderStyle-CssClass="buttongrid" HeaderStyle-HorizontalAlign="left" 
                                                    HeaderText="Disable" ItemStyle-VerticalAlign="top">
                                                    <ItemTemplate>
                                                       <asp:CheckBox ID="chkSKU" Checked='<%# Eval("IsDisable") %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                                         
                                            </Columns>
                                            </asp:GridView>
                                        </td>            
                                    </tr>

                                                              
                         <tr id="trSubmit" runat="server"  >
                            <td  align="center">
                                <br />  
                                <hr />
                                        <asp:Button ID="btnSubmit" Text="Submit" CssClass="button" runat="server"  OnClick="btnSubmit_Click" CausesValidation="False" />  &nbsp;&nbsp;                                          
                                        <asp:Button ID="btnCancel2" Text="Cancel" CssClass="button" runat="server"  OnClick="btnCancel_click" CausesValidation="False" />       
                                     
                                   <br />
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
            <Triggers>
                <asp:PostBackTrigger  ControlID="btnDownload"  />
            </Triggers>
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
