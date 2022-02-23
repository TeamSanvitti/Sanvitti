<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NonEsnSearch.aspx.cs" Inherits="avii.Admin.NonEsnSearch" %>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="/Controls/Header.ascx" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Accessory receive search </title>
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
	
    <link rel="stylesheet"  type="text/css" href="../fullfillment/calendar/dhtmlgoodies_calendar.css" media="screen" />
	<script type="text/javascript" src="../fullfillment/calendar/dhtmlgoodies_calendar.js"></script>  
		
    <script type="text/javascript">
        function OpenNewPage(url) {

            var newWin = window.open(url);

            if (!newWin || newWin.closed || typeof newWin.closed == 'undefined') {
                alert('your pop up blocker is enabled');

                //POPUP BLOCKED
            }
        }

        function formatParentCatDropDown(objddl) {

            for (i = 0; i < objddl.options.length; i++) {
                objddl.options[i].innerHTML = objddl.options[i].innerHTML.replace(/&amp;/g, '&');
            }
        }

        function set_focus1() {
            var img = document.getElementById("imgShipFrom");
            var st = document.getElementById("txtESN");
            st.focus();
            img.click();
        }
         function set_focus2() {
            var img = document.getElementById("imgShipTo");
            var st = document.getElementById("txtESN");
            st.focus();
            img.click();
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
          <table cellSpacing="0" cellPadding="0" align="center" width="100%" border="0">
        <tr>
			<td><head:MenuHeader id="HeadAdmin" runat="server"></head:MenuHeader></td>
		</tr>
            </table>
        <table cellSpacing="0" cellPadding="0" align="center" width="95%" border="0">
		<tr class="buttonlabel">
			<td  class="buttonlabel">&nbsp;&nbsp;Accessory Receive Search
			</td>
		</tr>
        </table>
        <table cellSpacing="0" cellPadding="0" align="center" width="95%" border="0">
    		<tr>                    
                <td colspan="2">
                    <asp:Label ID="lblMsg" runat="server" CssClass="errormessage"></asp:Label></td>
            </tr>               
            
            <tr>
                <td align="center">

                    

                <table cellSpacing="0" cellPadding="0" align="center" width="100%" border="0">
                    <tr>
                        <td align="center">

                        
               
                 <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="100%">
                    <tr>
                    <td>
			            <table  cellSpacing="5" cellPadding="5" width="100%" border="0">
                            <tr>
                                <td  class="copy10grey" align="right" width="22%" >
                                    Customer: &nbsp;</td>
                                <td align="left" width="28%">
                                   <asp:DropDownList ID="dpCompany" CssClass="copy10grey" runat="server" Width="80%">
									</asp:DropDownList>
                                </td>
                                <td class="copy10grey" align="right" width="22%" >
                                    Customer Order#: &nbsp;
                                </td>
                                <td  align="left"  width="28%" >
                                    <asp:TextBox ID="txtCustOrderNumber" runat="server" CssClass="copy10grey" MaxLength="20"  Width="80%"></asp:TextBox>
                                     
                                </td>
                            </tr>
                           
                            <tr>
                                <td class="copy10grey" align="right" width="22%" >
                                    Receive From: &nbsp;

                                </td>

                                
                                <td  align="left" width="28%" >
                                     <asp:TextBox ID="txtShipFrom" runat="server" onfocus="set_focus1();"  CssClass="copy10grey" MaxLength="12"  Width="80%"></asp:TextBox>
                                            <img id="imgShipFrom" alt="" onclick="document.getElementById('<%=txtShipFrom.ClientID%>').value = ''; displayCalendar(document.getElementById('<%=txtShipFrom.ClientID%>'),'mm/dd/yyyy',this,true);" src="../fullfillment/calendar/sscalendar.jpg" />
                                        
                                </td>
                                <td class="copy10grey" align="right" width="22%" >
                                    Receive To: &nbsp;
                                </td>
                                <td  align="left"  width="28%" >
                                     <asp:TextBox ID="txtShipTo" runat="server" onfocus="set_focus2();"  CssClass="copy10grey" MaxLength="12"  Width="80%"></asp:TextBox>
                                            <img id="imgShipTo" alt="" onclick="document.getElementById('<%=txtShipTo.ClientID%>').value = ''; displayCalendar(document.getElementById('<%=txtShipTo.ClientID%>'),'mm/dd/yyyy',this,true);" src="../fullfillment/calendar/sscalendar.jpg" />
                                        
                                </td>
                            </tr>
                            <tr>
                                <td class="copy10grey" align="right" width="22%" >
                                    Category: &nbsp;

                                </td>

                                
                                <td  align="left" width="28%" >
                                    <asp:DropDownList ID="ddlCategoryFilter" runat="server" 
                                            CssClass="copy10grey" Width="80%">
                                        </asp:DropDownList>
                                     
                                </td>
                                <td class="copy10grey" align="right" width="22%" >
                                    SKU: &nbsp;
                                </td>
                                <td  align="left"  width="28%" >
                                    <asp:TextBox ID="txtSKU" runat="server"   CssClass="copy10grey" MaxLength="35"  Width="80%"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="copy10grey" align="right" width="22%" >
                                    Location: &nbsp;

                                </td>                                
                                <td  align="left" width="28%" >
                                    <asp:TextBox ID="txtLocation" runat="server"   CssClass="copy10grey" MaxLength="35"  Width="80%"></asp:TextBox>
                                     
                                </td>
                                <td class="copy10grey" align="right" width="22%" >
                                    
                                </td>
                                <td  align="left"  width="28%" >
                                    
                                </td>
                            </tr>
                           <%-- <tr>
                                <td class="copy10grey" align="right" width="22%" >
                                    ESN: &nbsp;

                                </td>

                                
                                <td  align="left" width="28%" >
                                    <asp:TextBox ID="txtESN" runat="server"   CssClass="copy10grey" MaxLength="30"  Width="80%"></asp:TextBox>
                                     
                                </td>
                                <td class="copy10grey" align="right" width="22%" >
                                     &nbsp;
                                </td>
                                <td  align="left"  width="28%" >
                                    <asp:TextBox ID="txtTrackingNo" Visible="false" runat="server"   CssClass="copy10grey" MaxLength="20"  Width="80%"></asp:TextBox>
                                </td>
                            </tr>
                           --%> 
                            <tr><td colspan="6">
                                        <hr style="width:100%" />
                            
                             </td></tr> 
                            <tr>
                                <td colspan="6">
                                   
                                    <table cellpadding="0" cellspacing="0" width="100%">
                                          
                                                   
                                    
                                         <tr>
                                            <td  align="center">
                                
                                
                                            <asp:Button ID="btnSearch" Width="190px"  CssClass="button" runat="server" OnClick="btnSearch_Click" Text="Search" OnClientClick="return Validate(2);" />

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
                        </td>
                    </tr>
                    <tr>
                        <td>
&nbsp;
                    </td>
                    </tr>
                    
                <tr>
                        <td align="center">
                            <asp:Panel ID="pnlSearch" Visible="false" runat="server">
                 <table bordercolor="#839abf" border="0" cellSpacing="0" cellPadding="0" width="100%">
                    <tr>
                    <td>
			            <table  cellSpacing="0" cellPadding="0" width="100%" border="0">
                            <tr><td>
                            <table cellpadding="0" cellspacing="0" width="100%">
                             <tr>
                                <td align="right" style="height:8px; vertical-align:bottom">
                                <strong>   <asp:Label ID="lblCount" CssClass="copy10grey" runat="server" ></asp:Label> &nbsp;&nbsp;</strong> 
                                    <strong>   <asp:Label ID="lblTotalQty" CssClass="copy10grey" runat="server" ></asp:Label> &nbsp;&nbsp;</strong> 
                                </td>
                             </tr>
                             <tr>
                                <td align="center">
                                
                                <asp:GridView ID="gvMSL" runat="server" Width="100%" GridLines="Both"   AutoGenerateColumns="false"
                                    OnPageIndexChanging="gridView_PageIndexChanging" PageSize="100" AllowPaging="true" 
                                    AllowSorting="true" OnSorting="gvMSL_Sorting" >
                                        <RowStyle BackColor="Gainsboro" />
                                        <AlternatingRowStyle BackColor="white" />
                                        <HeaderStyle  CssClass="buttonlabel" ForeColor="white"/>
                                        <FooterStyle CssClass="white"  />
                                        <PagerStyle ForeColor="#636363" CssClass="copy10grey" HorizontalAlign="Left" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="s.No." ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                    
                                                </ItemTemplate>
                                            </asp:TemplateField> 
                                            <asp:TemplateField HeaderText="Order Number" HeaderStyle-CssClass="buttonundlinelabel" SortExpression="OrderNumber"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="9%">
                                                <ItemTemplate>
                                                            <%#Eval("OrderNumber")%>
                                                    
                                                </ItemTemplate>
                                            </asp:TemplateField> 
                                            <asp:TemplateField HeaderText="Customer Order#" HeaderStyle-CssClass="buttonundlinelabel" SortExpression="CustomerOrderNumber"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="9%">
                                                <ItemTemplate>
                                                            <%#Eval("CustomerOrderNumber")%>
                                                    
                                                </ItemTemplate>
                                            </asp:TemplateField> 
                                            
                                            <asp:TemplateField HeaderText="Category Name" HeaderStyle-CssClass="buttonundlinelabel" HeaderStyle-Width="9%" SortExpression="CategoryName"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="9%">
                                                <ItemTemplate>
                                                            <%#Eval("CategoryName")%>
                                                    
                                                </ItemTemplate>
                                            </asp:TemplateField> 
                                            <asp:TemplateField HeaderText="SKU" HeaderStyle-CssClass="buttonundlinelabel"  HeaderStyle-Width="20%" SortExpression="SKU"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                            <%#Eval("SKU")%>
                                                    
                                                </ItemTemplate>
                                            </asp:TemplateField> 
                                            
                                            <asp:TemplateField HeaderText="Product Name" HeaderStyle-CssClass="buttonundlinelabel" HeaderStyle-Width="25%" SortExpression="ProductName"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                            <%#Eval("ProductName")%>
                                                    
                                                </ItemTemplate>
                                            </asp:TemplateField> 
                                            
                                            <asp:TemplateField HeaderText="Receive Date" HeaderStyle-CssClass="buttonundlinelabel" SortExpression="ReceivedDate"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="8%">
                                                <ItemTemplate>
                                                            <%#Eval("UploadDate")%>
                                                    
                                                </ItemTemplate>
                                            </asp:TemplateField> 
                                            <%--<asp:TemplateField HeaderText="WH LOCATION" HeaderStyle-CssClass="buttonundlinelabel" SortExpression="WareHouseLocation"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="8%">
                                                <ItemTemplate>
                                                            <%#Eval("WareHouseLocation")%>
                                                    
                                                </ItemTemplate>
                                            </asp:TemplateField> 
                                            <asp:TemplateField HeaderText="BOXID" HeaderStyle-CssClass="buttonundlinelabel" SortExpression="BOXID"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="8%">
                                                <ItemTemplate>
                                                            <%#Eval("BOXID")%>
                                                    
                                                </ItemTemplate>
                                            </asp:TemplateField> --%>

                                            <asp:TemplateField HeaderText="Receive Quantity"   ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="9%">
                                                <ItemTemplate>
                                                            <%#Eval("TotalQty")%>
                                                    
                                                </ItemTemplate>
                                            </asp:TemplateField> 
                                            <asp:TemplateField HeaderText="Assigned Quantity"   ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="9%">
                                                <ItemTemplate>
                                                            <%#Eval("AssignedQty")%>
                                                    
                                                </ItemTemplate>
                                            </asp:TemplateField> 
                                            

                                            <asp:TemplateField HeaderText="Received By"   ItemStyle-HorizontalAlign="left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="9%">
                                                <ItemTemplate>
                                                            <%#Eval("UserName")%>
                                                    
                                                </ItemTemplate>
                                            </asp:TemplateField> 
                                            
                                            
                                            <asp:TemplateField HeaderStyle-Width="3%" HeaderStyle-CssClass="buttonlabel" HeaderText="Action">
                                                <ItemTemplate>
                                                    
                                                    <asp:ImageButton ID="edit" runat="server" CommandArgument='<%# Eval("ESNHeaderId") %>' ImageUrl="~/images/edit.png" OnCommand="edit_Command" />
                                                    <asp:ImageButton ID="imgDelete" Visible='<%#  Convert.ToInt32(Eval("AssignedQty")) > 0 ? false : true %>' runat="server" OnClientClick="return confirm('Are you sure want to delete this record?')"
                                                        CommandArgument='<%# Eval("ESNHeaderId") %>' ImageUrl="~/images/delete.png" OnCommand="imgDelete_Command" />
                                                    <%--<div runat="server" visible='<%#Eval("IsESN")%>' width="100%">
                                                    

                                                        
                                                        <a href="ManageMslEsn.aspx?headerid=<%# Eval("ESNHeaderId") %>">
                                                        <asp:Image runat="server" ID="ImageEdit" src="../images/edit.png" alt="edit" style="border:0"/></a>
                                                            
                                                    </div>
                                                    --%>    
                                                </ItemTemplate>
                                            </asp:TemplateField>    
                                        </Columns>
                                    </asp:GridView>
                                    

                                </td>
                            </tr>
                            </table>
                            </td></tr>                            
                                                    
                             
                       </table>
                            
                    </td>
                    </tr>
                           
                 </table>
</asp:Panel>
                    </td>
                    </tr>
                        

 </table>
                    </td>
                </tr>
            
        </table>
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
    </form>
</body>
</html>
