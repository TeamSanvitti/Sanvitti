<%@ Page Language="c#" AutoEventWireup="true" CodeBehind="CustomerQuery.aspx.cs" Inherits="avii.Admin.CustomerQuery" %>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="/Controls/Header.ascx" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="/Controls/Footer.ascx" %>
<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>--%>
<%@ Register TagPrefix="UC" TagName="Stores" Src="~/Controls/CustomerStores.ascx" %>
<%@ Register TagPrefix="UC" TagName="WHCode" Src="~/Controls/WarehouseCode.ascx" %>



<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>.:: Company Query ::.</title>
    <!-- from http://encosia.com/2009/10/11/do-you-know-about-this-undocumented-google-cdn-feature/ -->
    <link href="https://ajax.googleapis.com/ajax/libs/jqueryui/1.8.11/themes/start/jquery-ui.css" type="text/css" rel="Stylesheet" />
	
	<%--<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.4.0/jquery.min.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/jquery-ui.min.js"></script>
--%>

    <script type="text/javascript" src="../JQuery/jquery.min.js"></script>
    <script type="text/javascript" src="../JQuery/jquery-ui.min.js"></script>
	
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.4.0/jquery.min.js"></script>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/jquery-ui.min.js"></script>
	<script type="text/javascript" src="../JQuery/jquery.blockUI.js"></script>
	
	
<!-- fix for 1.1em default font size in Jquery UI -->
	<style type="text/css">
		.ui-widget{font-size:11px !important;}
		.ui-state-error-text{margin-left: 10px}
	</style>
    
	<script type="text/javascript">
		$(document).ready(function() {
			$("#divStores").dialog({
				autoOpen: false,
				modal: true,
				minHeight: 400,
				height: 400,
				width: 900,
				resizable: false,
				open: function(event, ui) {
					$(this).parent().appendTo("#divEditCustomerDlgContainer");
				}
			});
            $("#divWarehouse").dialog({
				autoOpen: false,
				modal: true,
				minHeight: 20,
				height: 250,
				width: 400,
				resizable: false,
				open: function(event, ui) {
					$(this).parent().appendTo("#divEditCustomerDlgContainer");
				}
			});
            
		});


		function closeDialog() {
			//Could cause an infinite loop because of "on close handling"
			$("#divStores").dialog('close');
		}
        function closeWarehouseDialog() {
			//Could cause an infinite loop because of "on close handling"
			$("#divWarehouse").dialog('close');
		}
        
		function openDialog(title, linkID) {
		//alert('1');
			var pos = $("#" + linkID).position();
			var top = pos.top;
			var left = pos.left + $("#" + linkID).width() + 10;
			//alert(top);
            //top = top - 300;
            if (top > 600)
	                top = 10;
	        top = 100;
            //alert(top);
			left = 225;
			$("#divStores").dialog("option", "title", title);
			$("#divStores").dialog("option", "position", [left, top]);
			
			$("#divStores").dialog('open');
            //alert('3')
		}

        function openWarehouseDialog(title, linkID) {
		
			var pos = $("#" + linkID).position();
			var top = pos.top;
			var left = pos.left + $("#" + linkID).width() + 10;
			//alert(left);
            //top = top - 300;
            if (top > 600)
	                top = 10;
	        top = 100;   
			left = 450;
            //$("#divHistory").html("");
            //$('#divHistory').empty();
            //alert($('#phrHistory'));
			$("#divWarehouse").dialog("option", "title", title);
			$("#divWarehouse").dialog("option", "position", [left, top]);
			
			$("#divWarehouse").dialog('open');
		}

		function openDialogAndBlock(title, linkID) {
        //alert('0')
			openDialog(title, linkID);
            //alert('2')
			//block it to clean out the data
			$("#divStores").block({
				message: '<img src="../images/async.gif" />',
				css: { border: '0px' },
				fadeIn: 0,
				//fadeOut: 0,
				overlayCSS: { backgroundColor: '#ffffff', opacity: 1 } 
			});
		}
        function openWarehouseDialogAndBlock(title, linkID) {
			openWarehouseDialog(title, linkID);

			//block it to clean out the data
			$("#divWarehouse").block({
				message: '<img src="../images/async.gif" />',
				css: { border: '0px' },
				fadeIn: 0,
				//fadeOut: 0,
				overlayCSS: { backgroundColor: '#ffffff', opacity: 1 } 
			});
		}
        
		
		function unblockDialog() {
			$("#divStores").unblock();
		}
        function unblockWarehouseDialog() {
			$("#divWarehouse").unblock();
		}
        

		
	</script>
	
	<link href="/aerostyle.css" rel="stylesheet" type="text/css"/>
        
        <script type="text/javascript" language="javascript">
            
            function ConfirmDelete(obj) {
                
                var vFlag;
                objStoreFlag = document.getElementById(obj.id.replace('lnkDelete', 'hdnStoreFlag'));

                if (objStoreFlag.value != "") {
                    vFlag = alert('StoreID is already in use, you cannot delete?');
                    return false;
                }
                else {
                    vFlag = confirm('Delete this StoreID?');
                    if (vFlag)
                        return true;
                    else
                        return false;
                }
            }
        
        </script>
        
</head>

<body bgColor="#ffffff" leftmargin="0" rightmargin="0" topmargin="0">
    <form id="form1" runat="server">
			<table cellSpacing="0" cellPadding="0"  border="0" align="center" width="100%">
				<tr>
					<td><head:MenuHeader id="MenuHeader11" runat="server"></head:MenuHeader>				
					</td>
				</tr> 
               </table>         
<asp:ScriptManager ID="ScriptManager1" runat="server">
     </asp:ScriptManager>

     <div id="divEditCustomerDlgContainer">	
			<div id="divStores" style="display:none">
					
				<asp:UpdatePanel ID="upnlStores" runat="server">
					<ContentTemplate>
                   
						<asp:PlaceHolder ID="phrStores" runat="server">
                        <asp:Panel ID="pnlStores" runat="server">
                            <UC:Stores ID="store1" runat="server" />
                        </asp:Panel>
                        </asp:PlaceHolder>
						
						
					</ContentTemplate>
							
				</asp:UpdatePanel>    
                            
            </div>
            <div id="divWarehouse" style="display:none">
					
				<asp:UpdatePanel ID="upWarehouse" runat="server" >
                
					<ContentTemplate>
                    
						<asp:PlaceHolder ID="phrWarehouse" runat="server">
                            <asp:Panel ID="pnlWhCode" runat="server">
                                <UC:WHCode ID="whCode1" runat="server" />
                            </asp:Panel>
                        </asp:PlaceHolder>
						
						
					</ContentTemplate>
							
				</asp:UpdatePanel>
			
			</div>
            </div>

     <asp:UpdatePanel ID="upnlCustomers" UpdateMode="Conditional" runat="server">
			<ContentTemplate>
		
			<table   width="95%" align="center">
                <tr>
			        <td  bgcolor="#dee7f6" class="buttonlabel">&nbsp;&nbsp;
			        <asp:Label ID="lblHeader" runat="server" CssClass="buttonlabel" BorderWidth="0" Text="Customer Query" ></asp:Label>
                        <asp:HiddenField ID="hdnEdit" runat="server" />
                        <asp:HiddenField ID="hdnDel" runat="server" />
			        </td>
                </tr>
                
               <tr><td><asp:Label ID="lblMsg" runat="server" CssClass="errormessage"></asp:Label></td></tr>
            </table>
            
            <asp:Panel ID="pnlSearch" runat="server" DefaultButton="btnSearch">
    
            <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="95%" align="center" >
                <tr bordercolor="#839abf">
                                    <td>
                                        <table class="box" width="100%" align="center" cellpadding="3" cellspacing="3">
                                            
                                            <tr>
                                                <td class="copy10grey"  width="15%">
                                                    Company Name:
                                                </td>
                                                <td  width="35%">
                                                    <asp:TextBox runat="server" ID="txtCompanyName" CssClass="copy10grey"  Width="80%" />
                                                </td>
                                                <td class="copy10grey"  width="15%">
                                                    Company A/c #:
                                                </td>
                                                <td  width="35%">
                                                    <asp:TextBox runat="server" ID="txtCompanyAC" CssClass="copy10grey" Width="80%" />
                                                    
                                                </td>
                                            </tr>
                                            <tr valign="top">
                                                <td class="copy10grey" width="15%">
                                                    Contact Name:
                                                </td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="txtContactName" CssClass="copy10grey" Width="80%" />
                                                </td>
                                                
                                                <td class="copy10grey"  width="15%">
                                                StoreID:
                                                </td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="txtStoreID" CssClass="copy10grey" Width="80%" />
                                                </td>
                                            </tr>
                                            <tr valign="top">
                                                <td class="copy10grey" width="15%">
                                                Status:
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlStatus" TabIndex="4"  CssClass="copy10grey" runat="server">
                                                    <asp:ListItem Text="" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="Pending" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Approved" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="Cancelled" Value="3"></asp:ListItem>
                                                    <asp:ListItem Text="AllowChanges" Value="4"></asp:ListItem>
                                                    <asp:ListItem Text="Inactive" Value="5"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                
                                                </td>
                                                <td>
                                                
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <%--<tr>
                                    <td>
                                        <hr />
                                     </td>
                                </tr>--%>
                                <tr>
                                    <td align="center"><br />
                                        <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_click"
                                            CssClass="button" />&nbsp;
                                        <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click1" Text="Cancel"
                                            CssClass="button" />
                                            <br />
                                    </td>
                                </tr>
                            </table>
			</asp:Panel>   
            <br />         
            <asp:Panel ID="searchPanel" runat="server">
            <table bordercolor="#839abf" border="0" cellSpacing="0" cellPadding="0" width="95%" align="center" >
            <tr bordercolor="#839abf">
                <td >
                <table width="100%">
                <tr>
                    <td>
                        <asp:GridView ID="gvCustomer"  BackColor="White" Width="100%" Visible="true" 
                        AutoGenerateColumns="false" Font-Names="Verdana" runat="server" DataKeyNames="CompanyID"
                        GridLines="Both"  OnRowDataBound="gvCustomer_RowDataBound" 
                        OnPageIndexChanging="gridView_PageIndexChanging" PageSize="50" AllowPaging="true"
                        BorderStyle="Double" BorderColor="#0083C1">
                        <RowStyle BackColor="Gainsboro" />
                        <AlternatingRowStyle BackColor="white" />
                        <HeaderStyle  CssClass="buttongrid" ForeColor="white"/>
                        <FooterStyle CssClass="white"  />
                        <PagerStyle ForeColor="#636363" CssClass="copy10grey" HorizontalAlign="Left" />
                        <Columns>
                            <asp:TemplateField HeaderText="Company Name"  HeaderStyle-HorizontalAlign="Left" 
                            ItemStyle-CssClass="copy10grey"  ItemStyle-Width="25%"  ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate><%# Eval("CompanyName")%></ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Company Code"  HeaderStyle-HorizontalAlign="Left" 
                            ItemStyle-CssClass="copy10grey"  ItemStyle-Width="10%"  ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate><%# Eval("CompanyShortName")%></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Bussiness Type" ItemStyle-CssClass="copy10grey"  ItemStyle-Width="8%"  ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate> 
                                    <asp:HiddenField ID="hdnrmaGUID" Value='<%# Eval("companyID")%>' runat="server" />
                                            
                                    <asp:Label ID="lblBussinessType" runat="server" Text='<%# Eval("BussinessType")%>' ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Company A/C#" ItemStyle-CssClass="copy10grey" ItemStyle-Width="9%"  ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <%# Eval("CompanyAccountNumber")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Status" ItemStyle-CssClass="copy10grey" ItemStyle-Width="6%"  ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <%# Convert.ToInt32(Eval("CompanyAccountStatus")) == 2 ? "Approved" : Convert.ToInt32(Eval("CompanyAccountStatus"))==1 ? "Pending": Convert.ToInt32(Eval("CompanyAccountStatus"))==3 ?"Cancelled":Convert.ToInt32(Eval("CompanyAccountStatus"))==4?"AllowChanges":"Inactive"%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Email" ItemStyle-CssClass="copy10grey" ItemStyle-Width="11%"  ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <%# Eval("Email")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Website" ItemStyle-CssClass="copy10grey" ItemStyle-Width="9%"  ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <%# Eval("website")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                                        
                            <asp:TemplateField HeaderText="Warehouse Code" ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%"  ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:ImageButton ToolTip="Warehouse code" AlternateText="Warehouse code"  CommandArgument='<%# Eval("companyID") %>' ImageUrl="~/Images/view.png" ID="imgView" OnCommand="imgViewWarehouse_Commnad" runat="server" />
                                    
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Stores" ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:ImageButton ToolTip="Stores"  AlternateText="Stores"  CommandArgument='<%# Eval("companyID") %>' ImageUrl="~/Images/Store.png" ID="imgStore" OnCommand="imgViewStores_Commnad" runat="server" />
                                    
                                </ItemTemplate>
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="" ItemStyle-CssClass="copy10grey" ItemStyle-Width="6%"  ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                <table>
                                <tr>
                                    <td>
                                        <div id="editDiv" name="editDiv" >
                                        
                                            <asp:ImageButton ID="lnkedit" ToolTip="Edit" AlternateText="Edit"  CausesValidation="false" CommandArgument='<%# Eval("companyID")%>'
                                             CommandName="ss" OnCommand="Edit_click" runat="server" ImageUrl="~/Images/edit.png" />
                                            </div>
                                    </td>
                                    <td>
                                    
                                <div id="delDiv" name="delDiv">
                                <asp:ImageButton ID="lnkDelete" runat="server" ImageUrl="~/images/delete.png" OnCommand="lnkDelete_click" 
                                        CommandArgument='<%# Eval("companyID") %>' OnClientClick="return confirm('Do you want to delete?');" 
                                            CausesValidation="false" AlternateText="Delete" ToolTip="Delete"/>
<%--
                                            <asp:LinkButton ID="lnkDelete"  CausesValidation="false" 
                                            CommandArgument='<%# Eval("companyID")%>' CommandName="ss" 
                                            OnCommand="lnkDelete_click" runat="server">Delete</asp:LinkButton>--%>
                                            </div>
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
            <tr><td>&nbsp;</td></tr>
            <tr><td>&nbsp;</td></tr>
            </table>
            </asp:Panel>  
            
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
            <table width="100%" align="center">
            	<tr>
					<td>
						<foot:MenuFooter id="Menuheader2" runat="server"></foot:MenuFooter></td>
				</tr>
			</table>      
             <script type="text/javascript" src="../JQuery/jquery.min.js"></script>
    <script type="text/javascript" src="../JQuery/jquery-ui.min.js"></script>
	
	<script type="text/javascript" src="../JQuery/jquery.blockUI.js"></script>
    </form>
</body>
</html>
