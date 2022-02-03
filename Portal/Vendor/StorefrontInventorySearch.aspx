<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StorefrontInventorySearch.aspx.cs" Async="true"  Inherits="avii.Vendor.StorefrontInventorySearch" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="/Controls/Footer.ascx" %>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="/Controls/Header.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Store Front Products</title>
    <script src="https://code.jquery.com/jquery-1.11.0.min.js"></script>
     <script>
         function SelectAll(id) {
             // alert(document.getElementById(id).checked);
             var check = document.getElementById(id).checked;
             // alert(check);

             var elements = document.getElementsByTagName('input');
             // iterate and change status
             for (var i = elements.length; i--;) {
                 if (elements[i].type == 'checkbox' && elements[i].id != 'chkGet') {
                     elements[i].checked = check;
                 }
             }
             // $(':checkbox').prop('checked', check);



         }
         function OpenNewPage(url) {

             var newWin = window.open(url);

             if (!newWin || newWin.closed || typeof newWin.closed == 'undefined') {
                 alert('your pop up blocker is enabled');

                 //POPUP BLOCKED
             }
         }

     </script>
     
     <style>
.progresss {
          position: fixed !important;
          z-index: 9999 !important;
          top: 0px !important;
          left: 0px !important;
          background-color: #EEEEEE !important;
          width: 100% !important;
          height: 100% !important;
          filter: Alpha(Opacity=80) !important;
          opacity: 0.80 !important;
          -moz-cpacity: 0.80 !important;
      }
.modal
{
    position: fixed;
    top: 0;
    left: 0;
    background-color: black;
    z-index: 100000000;
    opacity: 0.8;
    filter: alpha(opacity=80);
    -moz-opacity: 0.8;
    min-height: 100%;
    width: 100%;
}
.loadingcss
{    
    font-size: 18px;
    /*border: 1px solid red;*/
    /*width: 200px;
    height: 100px;*/
    display: none;
    position: fixed;
    /*background-color: White;*/
    z-index: 100000001;
    background-color:#CF4342;
}

  </style>
   
</head>
<body bgColor="#ffffff" leftmargin="0" rightmargin="0" topmargin="0">
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
    <table  cellSpacing="1" cellPadding="1" width="95%" align="center" >
        <tr>
		    <td colSpan="6" bgcolor="#dee7f6" class="buttonlabel">&nbsp;&nbsp;Store Front Products
		    </td>
        </tr>
    </table> 
     <table cellspacing="0" cellpadding="0" border="0" align="center" width="100%">
        <tr>
            <td >
     <asp:UpdatePanel ID="UpdatePanel1"  runat="server" UpdateMode="Conditional" >
        <ContentTemplate>   
        <table cellSpacing="0" cellPadding="0" align="center" width="95%" border="0">
    	<tr>                    
            <td colspan="2">
                <asp:Label ID="lblMsg" runat="server" Width="100%" CssClass="errormessage"></asp:Label>
            </td>
            <td width="20%" align="right">
                <%--<asp:LinkButton ID="lnk" runat="server"  PostBackUrl="~/Vendor/test.html" CssClass="button" >Get Token</asp:LinkButton>--%>
                <%--<a href="test.html" class="button">Get Token</a>--%>
            </td>
        </tr>
        </table>
        <table cellSpacing="0" cellPadding="0" align="center" width="95%" border="0">    
         <tr>
                <td align="center">
                 <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="100%">
                    <tr>
                    <td>
			            <table id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
                        <tr>
                            <td colspan="5"> 
                            <asp:Panel ID="pnlSearch" runat="server" DefaultButton="btnSearch">
                            <table cellSpacing="5" cellPadding="5" width="100%" border="0">  
                            <tr>
                                <td  class="copy10grey" align="right" width="15%">
                                        <strong> Storefront: </strong> &nbsp;</td>
                                <td align="left"  width="35%">
                                    <table  cellSpacing="0" cellPadding="0" width="100%" border="0">
                                        <tr>
                                            <td align="left"  width="80%">
                                                <asp:DropDownList ID="ddlStorefront" CssClass="copy10grey" runat="server" Width="100%">
				                                </asp:DropDownList>

                                            </td>
                                            <td align="right"  width="20%"> 
                                                <asp:CheckBox ID="chkGet" Text="Storefront only" CssClass="copy10grey" runat="server" ></asp:CheckBox>

                                            </td>
                                        </tr>
                                    </table>                                   
                                </td>
                                <td  width="1%">
                                    &nbsp;
                                </td>
                                <td class="copy10grey" align="right" width="15%">
                                    <strong> Customer: </strong>
                                </td>
                                <td align="left">
                                     <asp:DropDownList ID="dpCompany" CssClass="copy10grey" runat="server" 
                                         Width="80%">
				                    </asp:DropDownList>                                    
                                </td>
                            </tr>
                            
                         <tr valign="top">
                            <td class="copy10grey" align="right" width="10%">
                                 SKU:
                            </td>
                            <td width="35%">
                                <asp:TextBox ID="txtSKU" CssClass="copy10grey" runat="server" 
                                    MaxLength="20" Width="80%"></asp:TextBox>
                                <%--<asp:RequiredFieldValidator 
                                         id="rfvModel" runat="server" 
                                         ErrorMessage="SKU is Required!" 
                                         ControlToValidate="txtSKU"></asp:RequiredFieldValidator>--%>
                            </td>
                            <td  width="1%">
                                &nbsp;
                            </td>
                            <td class="copy10grey" align="right" width="10%">
                                Title:
                            </td>
                            <td >
                                   <asp:TextBox ID="txtTitle" CssClass="copy10grey" runat="server" MaxLength="100" 
                                    Width="80%"></asp:TextBox>                             
        
                            </td>   
                
                    
                            </tr>
                            <tr valign="top">
                            <td class="copy10grey" align="right" width="10%">
                                 UPC:
                            </td>
                            <td width="35%">
                                <asp:TextBox ID="txtUPC" CssClass="copy10grey" runat="server" MaxLength="30" 
                                    Width="80%"></asp:TextBox>
                                <%--<asp:RequiredFieldValidator 
                                         id="fqfUPC" runat="server" 
                                         ErrorMessage="Product UPC is Required!" 
                                         ControlToValidate="txtUPC">
                                        </asp:RequiredFieldValidator>  --%>
                            </td>
                            <td  width="1%">
                                &nbsp;
                            </td>
                            <td class="copy10grey" align="right" width="15%">
                                    Condition: 
                                </td>
                                <td align="left">
                                     <asp:DropDownList ID="ddlCondition" runat="server" CssClass="copy10grey" 
                                    Width="80%" ></asp:DropDownList> 
                                </td>
                
                    
                            </tr>
                            <tr valign="top">
                            <td class="copy10grey" align="right" width="10%">
                                Status:
                            </td>
                            <td width="35%">
                                 <asp:DropDownList ID="ddlStatus" CssClass="copy10grey" runat="server" Width="80%">
                                     <asp:ListItem Text="" Value=""></asp:ListItem>
                                     <asp:ListItem Text="New" Value="New"></asp:ListItem>
                                     <asp:ListItem Text="Synced" Value="Synced"></asp:ListItem>
				                    </asp:DropDownList>
                               
                            </td>
                            <td  width="1%">
                                &nbsp;
                            </td>
                            <td class="copy10grey" align="right" width="15%">
                                  
                                </td>
                                <td align="left">
                                   
                               
                                </td>
                
                    
                            </tr>
                                <tr valign="top">
                            <td class="copy10grey" align="right" width="10%">
                                 Token:
                            </td>
                            <td width="90%" colspan="4" valign="top">
                                <table with="100%" border="0" cellSpacing="0" cellPadding="0" >
                                    <tr valign="top">
                                        <td align="left"  width="97%">
                                            <asp:TextBox ID="txtToken"  CssClass="copy10grey" runat="server"  TextMode="MultiLine" Rows="2"
                                    Width="99%"></asp:TextBox>  
                                        </td>
                                        <td align="left" width="3%" valign="top">
                                            <asp:Button ID="btnGetToken" runat="server" Text="Get Token" CssClass="button" OnClick="btnGetToken_Click" CausesValidation="false" />
                                        </td>
                                    </tr>
                                </table>
                                
                                
                            </td>
                            <td></td>
                           <%-- <td  width="1%">
                                &nbsp;
                            </td>
                            <td class="copy10grey" align="right" width="15%">
                                   
                                </td>
                                <td align="left">
                                  
                                </td>--%>
                
                    
                            </tr>
                            </table>
                                    </asp:Panel>
                            </td>
                        </tr>
                        <tr style="height:4px">
                <td cellSpacing="1" cellPadding="1" >
                <hr style="height:1px" />
                </td>
                </tr>
                            <tr>
                            <td  align="center"  >
                                <div class="loadingcss" align="center" id="modalSending">
                                    <img src="/Images/ajax-loaders.gif" alt=""  /> Loading...
                                </div> 
                                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="button" OnClick="btnSearch_Click"  CausesValidation="false" OnClientClick="return ShowSendingProgress();"/>
            
                                &nbsp;
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="button" OnClick="btnCancel_Click" CausesValidation="false"/>
                   
                            </td>
                        </tr>
                    </table>
                </td>
                </tr>
                
                 
                </table>

                    </td>
                </tr>
            <tr>
                <td><br />
                    <table cellSpacing="0" cellPadding="0" align="center" width="95%" border="0">    
         <tr>
             <td align="left">
                  <asp:Label ID="lblCount" runat="server"  CssClass="copy10grey"></asp:Label>
                 <asp:Label ID="lblNote" runat="server"  CssClass="errorGreenMsg"></asp:Label>
             </td>
             <td align="right">
                  <asp:Button ID="btnDownload" Visible="false" runat="server" Text="Download Json" CssClass="button" OnClick="btnDownload_Click"  CausesValidation="false"/>
                  &nbsp; <asp:Button ID="btnSync"  Visible="false" runat="server" Text="Sync from storefront" CssClass="button" OnClick="btnSync_Click"  CausesValidation="false" OnClientClick="return ShowSendingProgress();"/>
                  <asp:Button ID="btnGet"  Visible="false" runat="server" Text="Get Inventory" CssClass="button" OnClick="btnGet_Click"  CausesValidation="false"/>
            
             </td>
         </tr>
                        <tr>
             <td colspan="2" align="center">

                  <asp:GridView ID="gvProducts" AutoGenerateColumns="false"   OnRowDataBound="gvProducts_RowDataBound"
        Width="100%" ShowHeader="true"  ShowFooter="false" runat="server" GridLines="Both"
            >                        
        <RowStyle BackColor="Gainsboro" />
        <AlternatingRowStyle BackColor="white" />
        <HeaderStyle  CssClass="buttongrid" ForeColor="white"/>
            <PagerStyle ForeColor="#636363" CssClass="copy10grey" HorizontalAlign="Left" />
            <Columns>
                <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="1%">
                        <HeaderTemplate>
                        <asp:CheckBox ID="allchk"  runat="server"  />
                    </HeaderTemplate>
                    <ItemTemplate>
                        
                        <asp:CheckBox ID="chkItem"  runat="server" CssClass="copy10grey" />
                        </ItemTemplate>
                    </asp:TemplateField>   
                <asp:TemplateField HeaderText="S.No." ItemStyle-CssClass="copy10grey"  ItemStyle-Width="1%">                
                <ItemTemplate>
                        <%#  Container.DataItemIndex + 1%>               
                </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Category Name" HeaderStyle-CssClass="buttongrid"   ItemStyle-HorizontalAlign="Left" 
                    ItemStyle-CssClass="copy10grey" ItemStyle-Width="7%">
                    <ItemTemplate>
                        <%# Eval("CategoryName")%>
                        </ItemTemplate>
                </asp:TemplateField>  
                <asp:TemplateField HeaderText="Brand" HeaderStyle-CssClass="buttongrid"   ItemStyle-HorizontalAlign="Left" 
                    ItemStyle-CssClass="copy10grey" ItemStyle-Width="7%">
                    <ItemTemplate>
                        <%#  Eval("MakerName") %>
                        </ItemTemplate>
                </asp:TemplateField>  
                
                
                
                <asp:TemplateField HeaderText="SKU" HeaderStyle-CssClass="buttongrid"   ItemStyle-HorizontalAlign="Left" 
                    ItemStyle-CssClass="copy10grey" ItemStyle-Width="8%">
                    <ItemTemplate>
                        <%#  Eval("SKU") %>
                        </ItemTemplate>
                </asp:TemplateField>  
                <asp:TemplateField HeaderText="Title" HeaderStyle-CssClass="buttongrid"   ItemStyle-HorizontalAlign="Left" 
                    ItemStyle-CssClass="copy10grey" ItemStyle-Width="15%">
                    <ItemTemplate>
                        <%#  Eval("ItemName") %>
                        </ItemTemplate>
                </asp:TemplateField>  
                <asp:TemplateField HeaderText="Condition" HeaderStyle-CssClass="buttongrid"   ItemStyle-HorizontalAlign="Left" 
                    ItemStyle-CssClass="copy10grey" ItemStyle-Width="8%">
                    <ItemTemplate>
                        <%#  Eval("Condition") %>
                        </ItemTemplate>
                </asp:TemplateField>  
                <asp:TemplateField HeaderText="Locale" HeaderStyle-CssClass="buttongrid"   ItemStyle-HorizontalAlign="Left" 
                    ItemStyle-CssClass="copy10grey" ItemStyle-Width="7%">
                    <ItemTemplate>
                        <%#  Eval("Locale") %>
                        </ItemTemplate>
                </asp:TemplateField>  
                <asp:TemplateField HeaderText="Opening Stock" HeaderStyle-CssClass="buttongrid"   ItemStyle-HorizontalAlign="Left" 
                    ItemStyle-CssClass="copy10grey" ItemStyle-Width="7%">
                    <ItemTemplate>
                        <%#  Eval("OpeningStock") %>
                        </ItemTemplate>
                </asp:TemplateField>  
                <asp:TemplateField HeaderText="Created By" HeaderStyle-CssClass="buttongrid"   ItemStyle-HorizontalAlign="Left" 
                    ItemStyle-CssClass="copy10grey" ItemStyle-Width="8%">
                    <ItemTemplate>
                        <%#  Eval("UserName") %>
                        </ItemTemplate>
                </asp:TemplateField> 
                <asp:TemplateField HeaderText="Create Date" HeaderStyle-CssClass="buttongrid"   ItemStyle-HorizontalAlign="Left" 
                    ItemStyle-CssClass="copy10grey" ItemStyle-Width="8%">
                    <ItemTemplate>
                        <%#  Eval("CreatedDate") %>
                        </ItemTemplate>
                </asp:TemplateField> 
                <asp:TemplateField HeaderText="Last Sync" HeaderStyle-CssClass="buttongrid"   ItemStyle-HorizontalAlign="Left" 
                    ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                    <ItemTemplate>
                        <%#  Eval("ModifiedDate") %>
                        </ItemTemplate>
                </asp:TemplateField>  
                <asp:TemplateField HeaderText="Status" HeaderStyle-CssClass="buttongrid"   ItemStyle-HorizontalAlign="Left" 
                    ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                    <ItemTemplate>
                        <%# Convert.ToBoolean(Eval("IsSync")) == true ? "Synced" : "New" %>
                        </ItemTemplate>
                </asp:TemplateField>  
                    
                <asp:TemplateField HeaderText="View" HeaderStyle-CssClass="buttongrid"  ItemStyle-HorizontalAlign="Center" 
                    ItemStyle-CssClass="copy10grey" ItemStyle-Width="1%"> 
                    <ItemTemplate>

                        <a href='../Product/detail-product.aspx?itemGUID=<%# Eval("ItemGUID") %>' target="_blank">
                            <img id="imgDateFrom" alt="" src="../Images/view.png" />
                        </a>
                        <asp:ImageButton ID="imgView" Visible="false"  ToolTip="Line items" OnCommand="imgView_Command"
                                     CausesValidation="false" CommandArgument='<%# Eval("ItemGUID") %>' ImageUrl="~/Images/view.png"  runat="server" />
                                                                  
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
                
    <asp:UpdatePanel ID="upnlJsRunner" UpdateMode="Always" runat="server">
			<ContentTemplate>
				<asp:PlaceHolder ID="phrJsRunner" runat="server">
                
                </asp:PlaceHolder>
			</ContentTemplate>
		</asp:UpdatePanel>
         </td>
         </tr>
         </table>
    <br />
        <table cellspacing="0" cellpadding="0" border="0" align="center" width="100%">
        <tr>
            <td >
                <foot:MenuFooter ID="footer2" runat="server"></foot:MenuFooter>
            </td>
         </tr>
         </table>

        <script type="text/javascript">
            function ShowSendingProgress() {
                var modal = $('<div  />');
                modal.addClass("modal");
                modal.attr("id", "modalSending");
                $('body').append(modal);
                var loading = $("#modalSending.loadingcss");
                //alert(loading);
                loading.show();
                var top = '300px';
                var left = '820px';
                loading.css({ top: top, left: left, color: '#ffffff' });

                var tb = $("maintbl");
                tb.addClass("progresss");
                // alert(tb);

                return true;
            }
            //background-color:#CF4342;

            function StopProgress() {

                $("div.modal").hide();

                var tb = $("maintbl");
                tb.removeClass("progresss");


                var loading = $(".loadingcss");
                loading.hide();
            }
        </script>
    </form>
</body>
</html>
