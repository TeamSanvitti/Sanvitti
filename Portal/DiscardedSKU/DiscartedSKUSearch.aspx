<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DiscartedSKUSearch.aspx.cs" Inherits="avii.DiscartedSKU.DiscartedSKUSearch" %>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="/Controls/Header.ascx" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Discarded SKU Search</title>
    <link rel="stylesheet"  type="text/css" href="../fullfillment/calendar/dhtmlgoodies_calendar.css" media="screen" />
	<script type="text/javascript" src="../fullfillment/calendar/dhtmlgoodies_calendar.js"></script>  
    <script>
        function set_focus1() {
            var img = document.getElementById("img1");
            var st = document.getElementById("btnSearch");
            st.focus();
            img.click();
        }
        function set_focus2() {
            var img = document.getElementById("img2");
            var st = document.getElementById("btnSearch");
            st.focus();
            img.click();
        }
        function ReadOnly1(evt) {
            var img = document.getElementById("img1");
            img.click();
            evt.keyCode = 0;
            return false;

        }
        function ReadOnly2(evt) {
            var img2 = document.getElementById("img2");
            img2.click();
            evt.keyCode = 0;
            return false;

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
                <head:MenuHeader ID="menu1" runat="server" ></head:MenuHeader>
            </td>
         </tr>
         </table>
          <table cellspacing="0" cellpadding="0" border="0" align="center" width="95%">
        <tr valign="top">
            <td   >
                <table align="center" style="text-align:left" width="100%">
                <tr class="buttonlabel" align="left">
                <td>&nbsp;Discarded SKU Search</td></tr>
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
                    <asp:DropDownList ID="dpCompany" CssClass="copy10grey" runat="server" Width="80%">
									</asp:DropDownList>
                
                </td>
                <td  width="1%">
                    &nbsp;
                </td>
                <td class="copy10grey" align="right" width="15%">
                   SKU:
                </td>
                <td width="35%">
                   
                           <asp:TextBox ID="txtSKU" runat="server"  CssClass="copy10grey" MaxLength="50"  Width="80%"></asp:TextBox>
        
                </td>   
                
                    
                </tr>
                <tr valign="top">
                <td class="copy10grey" align="right" width="15%">
                    Date From:
                </td>
                <td width="35%">
                    <asp:TextBox ID="txtDateFrom" runat="server" onkeydown="return ReadOnly1(event);"  onfocus="set_focus1();"  CssClass="copy10grey" MaxLength="12"  Width="80%"></asp:TextBox>
                    <img id="img1" alt="" onclick="document.getElementById('<%=txtDateFrom.ClientID%>').value = ''; displayCalendar(document.getElementById('<%=txtDateFrom.ClientID%>'),'mm/dd/yyyy',this,true);" src="../fullfillment/calendar/sscalendar.jpg" />
                                        
         
                </td>
                <td  width="1%">
                    &nbsp;
                </td>
                <td class="copy10grey" align="right" width="15%">
                    Date To:
                </td>
                <td width="35%">
                   
                  <asp:TextBox ID="txtDateTo" runat="server" onkeydown="return ReadOnly2(event);"  onfocus="set_focus2();"  CssClass="copy10grey" MaxLength="12"  Width="80%"></asp:TextBox>
                    <img id="img2" alt="" onclick="document.getElementById('<%=txtDateTo.ClientID%>').value = ''; displayCalendar(document.getElementById('<%=txtDateTo.ClientID%>'),'mm/dd/yyyy',this,true);" src="../fullfillment/calendar/sscalendar.jpg" />
                                                
        
                </td>   
                
                    
                </tr>
           
            
             <tr>
                <td colspan="5">
                <hr />
                </td>
                </tr>
                <tr>
                <td  align="center"  colspan="5">
                    <asp:Button ID="btnSearch" runat="server"  CssClass="button" Text="Search" OnClick="btnSearch_Click"
                                                ></asp:Button>
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
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                            <asp:GridView ID="gvSKU" AutoGenerateColumns="false"  
                            Width="100%" ShowHeader="true"  ShowFooter="false" runat="server" GridLines="Both" 
                            OnPageIndexChanging="gvSKU_PageIndexChanging" PageSize="20" AllowPaging="true" 
                            AllowSorting="true" OnSorting="gvSKU_Sorting" >                        
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
                                    <asp:TemplateField HeaderText="Customer Name" SortExpression="CustomerName" HeaderStyle-CssClass="buttonundlinelabel"  ItemStyle-HorizontalAlign="Left" 
                                        ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <%# Eval("CustomerName")%>                                    
                                        </ItemTemplate>
                                    </asp:TemplateField> 
                                    <asp:TemplateField HeaderText="SKU" SortExpression="SKU" HeaderStyle-CssClass="buttonundlinelabel"  ItemStyle-HorizontalAlign="Left" 
                                        ItemStyle-CssClass="copy10grey" ItemStyle-Width="15%">
                                        <ItemTemplate>
                                            <%# Eval("SKU")%>
                                            </ItemTemplate>
                                    </asp:TemplateField>  
                                    
                                    <asp:TemplateField HeaderText="Discard Date" SortExpression="DiscardDate" HeaderStyle-CssClass="buttonundlinelabel"  ItemStyle-HorizontalAlign="Left"
                                      ItemStyle-CssClass="copy10grey" ItemStyle-Width="7%">
                                        <ItemTemplate>                                                
                                             <%# Eval("DiscardDate")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>   
                                  <asp:TemplateField HeaderText="Quantity" SortExpression="Qty" HeaderStyle-CssClass="buttonundlinelabel"  ItemStyle-HorizontalAlign="Left" 
                                        ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <%# Eval("Qty")%>
                                            </ItemTemplate>
                                    </asp:TemplateField>  
                                  <asp:TemplateField HeaderText="Module" SortExpression="ModuleName" HeaderStyle-CssClass="buttonundlinelabel"  ItemStyle-HorizontalAlign="Left" 
                                        ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <%# Eval("ModuleName")%>
                                            </ItemTemplate>
                                    </asp:TemplateField>  
                                  
                                    <asp:TemplateField HeaderText="Discarded By" SortExpression="DiscardedBy" HeaderStyle-CssClass="buttonundlinelabel"  ItemStyle-HorizontalAlign="Left" 
                                      ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                                        <ItemTemplate>                                                
                                             <%# Eval("DiscardedBy")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>   
                                    <asp:TemplateField HeaderText="Requested By" SortExpression="RequestedByName" HeaderStyle-CssClass="buttonundlinelabel"  ItemStyle-HorizontalAlign="Left"
                                      ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                                        <ItemTemplate>                                                
                                             <%# Eval("RequestedByName")%>
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
             <%--<Triggers>
                 <asp:PostBackTrigger ControlID="btnDownload" />
             </Triggers>--%>
           </asp:UpdatePanel>
                
            <asp:UpdatePanel ID="upnlJsRunner" UpdateMode="Always" runat="server">
			<ContentTemplate>
				<asp:PlaceHolder ID="phrJsRunner" runat="server"></asp:PlaceHolder>
			</ContentTemplate>
		</asp:UpdatePanel>
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
