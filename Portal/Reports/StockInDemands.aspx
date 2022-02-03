<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StockInDemands.aspx.cs" Inherits="avii.Reports.StockInDemands" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>
<%@ Register TagPrefix="menu" TagName="Menu" Src="~/Controls/Header.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Stock In Demand</title>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>

    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.4.0/jquery.min.js"></script>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/jquery-ui.min.js"></script>
    <link href="https://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/themes/start/jquery-ui.css"
    rel="stylesheet" type="text/css" />

     <link rel="stylesheet"  type="text/css" href="../fullfillment/calendar/dhtmlgoodies_calendar.css" media="screen" />
	<script type="text/javascript" src="../fullfillment/calendar/dhtmlgoodies_calendar.js"></script> 
    <script>
        function isAlphaNumberHiphen(e) {

            var regex = new RegExp("^[a-zA-Z0-9-]+$");
            var str = String.fromCharCode(!e.charCode ? e.which : e.charCode);
            //alert(regex.test(str));
            if (regex.test(str)) {
                return true;
            }
            else {
                e.preventDefault();
                return false;
            }

        }
        function OpenNewPage(url) {
            window.open(url);
        }

        function set_focus1() {
            var img = document.getElementById("img1");
            var st = document.getElementById("txtSKU");
            st.focus();
            img.click();
        }
        function set_focus2() {
            var img = document.getElementById("img2");
            var st = document.getElementById("txtSKU");
            st.focus();
            img.click();
        }
    </script>
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
    <table cellspacing="0" cellpadding="0" border="0" align="center" style="text-align:left" width="100%">
        <tr class="buttonlabel" align="left">
            <td>&nbsp;Stock In Demand</td>
        </tr>
    </table>
    <asp:UpdatePanel ID="UpdatePanel1"  runat="server" UpdateMode="Conditional" >
        <ContentTemplate>
        <table cellSpacing="0" cellPadding="0" align="center" width="95%" border="0">
    		<tr>                    
                <td colspan="2">
                    <asp:Label ID="lblMsg" runat="server" Width="100%" CssClass="errormessage"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="center">
                <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="100%">
                    <tr>
                    <td>
			            <table id="Table2" cellSpacing="5" cellPadding="5" width="100%" border="0">
                        <tr>
                            <td colspan="5"> 
                                <asp:Panel ID="pnlSearch" runat="server" DefaultButton="btnSearch">
                                <table cellSpacing="5" cellPadding="5" width="100%" border="0">
                                <tr>
                                    <td  class="copy10grey" align="right" width="22%">
                                            Customer: &nbsp;</td>
                                    <td align="left"  width="28%">
                                        <asp:DropDownList ID="dpCompany" CssClass="copy10grey" runat="server" Width="70%">
								        </asp:DropDownList>
                                    </td>
                                    <td  class="copy10grey" align="right"  width="22%">
                                            SKU: &nbsp;</td>
                                    <td align="left"  width="28%" align="left">
                                        <asp:TextBox    ID="txtSKU" MaxLength="20" onkeypress="return isAlphaNumberHiphen(event);"  CssClass="copy10grey" runat="server" Width="70%"></asp:TextBox>
									
                                    </td>
                                    </tr>
                                    <tr>     
                                    <td  class="copy10grey" align="right"  >
                                             From Date: &nbsp;</td>
                                    <td align="left" width="28%">
                                          <asp:TextBox ID="txtFromDate" runat="server"  onfocus="set_focus1();" onkeypress="return doReadonly(event);"  
                                                CssClass="copy10grey" MaxLength="15"  Width="70%"/>
                                             <img id="img1" alt="" onclick="document.getElementById('<%=txtFromDate.ClientID%>').value = ''; displayCalendar(document.getElementById('<%=txtFromDate.ClientID%>'),'mm/dd/yyyy',this,true);"
                                                        src="../../fullfillment/calendar/sscalendar.jpg" />	
                                    </td>
                                    <td  class="copy10grey" align="right"  width="22%">
                                            To Date: &nbsp;</td>
                                    
                                    <td  width="28%" align="left">
                                        <asp:TextBox ID="txtToDate" runat="server"  onfocus="set_focus2();" onkeypress="return doReadonly(event);"  
                                                CssClass="copy10grey" MaxLength="15"  Width="70%"/>
                                             <img id="img2" alt="" onclick="document.getElementById('<%=txtToDate.ClientID%>').value = ''; displayCalendar(document.getElementById('<%=txtToDate.ClientID%>'),'mm/dd/yyyy',this,true);"
                                                        src="../../fullfillment/calendar/sscalendar.jpg" />
                                            
                                    </td>
                               </tr>
                                <tr>
                                    <td colspan="4" align="center">
                                        <hr style="width:100%" />                            
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" align="center">
                                    <asp:Button ID="btnSearch" runat="server" CssClass="button"  Text="Search" OnClick="btnSearch_Click" /> &nbsp;
                                        <asp:Button ID="btnCancel" runat="server" CssClass="button"  Text="Cancel" OnClick="btnCancel_Click" />
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
                </td>
            </tr>

            <tr>
                <td align="center">
                   <table align="center" width="100%" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td align="left"><strong>   
                                <asp:Label ID="lblCount" CssClass="copy10grey" runat="server" ></asp:Label> &nbsp;&nbsp;
                            </strong>               
                         </td>
                        <td align="right">
                          <asp:Button ID="btnDownload" runat="server" Text="Download"  Visible="false"  CssClass="button"  OnClick="btnDownload_Click" CausesValidation="false"/>
                  
                          
                      </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                     <asp:GridView runat="server" ID="gvStock" AutoGenerateColumns="False" 
                     PageSize="50" AllowPaging="true" Width="100%" OnPageIndexChanging="gvStock_PageIndexChanging"   
                     CellPadding="3"  AllowSorting="true" OnSorting="gvStock_Sorting"
                    GridLines="Vertical" >
                    <RowStyle  CssClass="copy10grey" BackColor="Gainsboro" />
                    <AlternatingRowStyle BackColor="white" />
                    <HeaderStyle  CssClass="buttonlabel" ForeColor="white"/>
                    <PagerStyle ForeColor="#636363" CssClass="copy10grey" />
                    <Columns>
                        <asp:TemplateField HeaderText="S.No." ItemStyle-CssClass="copy10grey"  ItemStyle-Width="2%" HeaderStyle-CssClass="buttonlabel">
                            <ItemTemplate>
                                    <%# Container.DataItemIndex + 1%>                  
                            </ItemTemplate>
                        </asp:TemplateField> 
                        <asp:TemplateField HeaderText="Category Name" ItemStyle-Width="10%" SortExpression="CategoryName"  HeaderStyle-CssClass="buttonundlinelabel">
                            <ItemTemplate>
                                <%# Eval("CategoryName") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="SKU" ItemStyle-Width="28%" SortExpression="SKU"  HeaderStyle-CssClass="buttonundlinelabel">
                            <ItemTemplate>
                                <%# Eval("SKU") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Product Name" ItemStyle-Width="30%" SortExpression="ProductName"  HeaderStyle-CssClass="buttonundlinelabel">
                            <ItemTemplate>
                                <%# Eval("ProductName") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Order Quantity" ItemStyle-Width="10%" SortExpression="RequiredQunatity"  ItemStyle-HorizontalAlign="Right"  HeaderStyle-CssClass="buttonundlinelabel">
                            <ItemTemplate>
                                <%# Eval("RequiredQunatity") %>&nbsp;
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Current Stock" ItemStyle-Width="9%" SortExpression="CurrentStock"  ItemStyle-HorizontalAlign="Right" HeaderStyle-CssClass="buttonundlinelabel">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkESN" CssClass="copyblue11b" runat="server" CausesValidation="true" CommandArgument='<%# Eval("ItemCompanyGUID") + "," + Eval("CompanyID") %>' CommandName="esn" OnCommand="lnkESN_Command">
                                 
                                <%# Eval("CurrentStock") %>&nbsp;
                                    </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Order Count" ItemStyle-Width="7%" SortExpression="OrderCount"  HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right"  HeaderStyle-CssClass="buttonundlinelabel">
                            <ItemTemplate>
                                 <asp:LinkButton ID="lnkPO" CssClass="copyblue11b" runat="server" CausesValidation="true" CommandArgument='<%# Eval("SKU") %>' CommandName="test" OnCommand="lnkPO_Command">
                                   &nbsp;<%# Eval("OrderCount") %>&nbsp;

                                    </asp:LinkButton>

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
