<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WhLocationReport.aspx.cs" Inherits="avii.Warehouse.WhLocationReport" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="~/Controls/Header.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Warehouse Location Report</title>
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
    
    <table cellSpacing="0" cellPadding="0" align="center" width="100%" border="0">
        <tr>
			<td><head:MenuHeader id="HeadAdmin" runat="server"></head:MenuHeader></td>
		</tr>
     </table>
    <table cellspacing="0" cellpadding="0" border="0" align="center" width="95%">
		<tr>
			<td  bgcolor="#dee7f6" class="buttonlabel">
            &nbsp;&nbsp;Warehouse Location Report
			</td>
		</tr>
    
    </table>
<table cellSpacing="0" cellPadding="0" align="center" width="95%" border="0" id="maintbl">
        <tr>
	        <td>
            <asp:UpdatePanel ID="upnlCode" UpdateMode="Conditional" runat="server">
	        <ContentTemplate>	            
                <table cellSpacing="0" cellPadding="0" align="center" width="100%" border="0">
    	            <tr>                    
                        <td><asp:Label ID="lblMsg" runat="server"  CssClass="errormessage"></asp:Label></td>
                    </tr> 
                </table>
                <table bordercolor="#839abf" border="1" width="100%" align="center" cellpadding="0" cellspacing="0">
            <tr>
            <td>
             <table width="100%" border="0" class="" align="center" cellpadding="5" cellspacing="5">    
                 <tr>
                <td class="copy10grey"  align="right" width="20%" >
                 Warehouse City:
                </td>
                <td width="30%" >
                     <asp:DropDownList ID="ddlWarehouse" TabIndex="1" runat="server" Width="60%" CssClass="copy10grey">                         
                    </asp:DropDownList>

                <%--       <asp:TextBox ID="txtWarehousecity"  onkeypress="return IsAlphaNumericHiphen(event);"  TabIndex="1" 
                           CssClass="copy10grey" runat="server" Width="60%" MaxLength="10" ></asp:TextBox>
                --%>
                </td>
                <td  width="1%">
                       &nbsp;
                    
                </td>
                <td class="copy10grey"  align="right" width="19%" >
                      Warehouse Location:
                </td>
                <td width="30%" >
                        <asp:TextBox ID="txtWarehouseLocation"  onkeypress="return IsAlphaNumericHiphen(event);"  
                            TabIndex="2" CssClass="copy10grey" runat="server" Width="60%" MaxLength="10" ></asp:TextBox>
                </td>   
                </tr>
                
                 <tr>
                <td class="copy10grey"  align="right" width="20%" >
                      Customer:
                </td>
                <td width="30%" >
                    <asp:DropDownList ID="dpCompany" TabIndex="2" runat="server" CssClass="copy10grey" Width="60%"
                        
                                    AutoPostBack="false">
									
                    </asp:DropDownList>
                    <%--OnSelectedIndexChanged="dpCompany_SelectedIndexChanged"  --%>
                </td>
                <td  width="1%">
                       &nbsp;
                    
                </td>
                <td class="copy10grey"  align="right" width="19%" >
                    SKU:
                </td>
                <td width="30%" >
                          <%--<asp:DropDownList ID="ddlSKU" runat="server" class="copy10grey"  Width="60%"   ></asp:DropDownList>--%>
                    <asp:TextBox ID="txtSKU" runat="server" class="copy10grey"  Width="60%" MaxLength="50"   ></asp:TextBox>
              
                </td>   
                </tr>
                 <tr valign="top">
                <td class="copy10grey" align="right" width="20%">
                    Date From:
                </td>
                <td width="30%">
                    <asp:TextBox ID="txtDateFrom" runat="server" onkeydown="return ReadOnly1(event);"  onfocus="set_focus1();"  CssClass="copy10grey" MaxLength="12"  Width="60%"></asp:TextBox>
                    <img id="img1" alt="" onclick="document.getElementById('<%=txtDateFrom.ClientID%>').value = ''; displayCalendar(document.getElementById('<%=txtDateFrom.ClientID%>'),'mm/dd/yyyy',this,true);" src="../fullfillment/calendar/sscalendar.jpg" />
                                        
         
                </td>
                <td  width="1%">
                    &nbsp;
                </td>
                <td class="copy10grey" align="right" width="19%">
                    Date To:
                </td>
                <td width="30%">
                   
                  <asp:TextBox ID="txtDateTo" runat="server" onkeydown="return ReadOnly2(event);"  onfocus="set_focus2();"  CssClass="copy10grey" MaxLength="12"  Width="60%"></asp:TextBox>
                    <img id="img2" alt="" onclick="document.getElementById('<%=txtDateTo.ClientID%>').value = ''; displayCalendar(document.getElementById('<%=txtDateTo.ClientID%>'),'mm/dd/yyyy',this,true);" src="../fullfillment/calendar/sscalendar.jpg" />
                                                
        
                </td>   
                
                    
                </tr>
           
                <tr style="height:5px">
                     <td colspan="5" style="height:5px">
                         <hr style="height:2px" />
                     </td>
                 </tr>
                <tr>
                <td colspan="5">
                     <table width="100%" align="center" >
                <tr>
			        <td align="center" >
			            <asp:Button ID="btnSearch" runat="server" TabIndex="18"  CssClass="buybt" Text="   Search   " onclick="btnSearch_Click" />&nbsp;&nbsp;
                        <asp:Button ID="btnCancel" runat="server" TabIndex="19" CssClass="buybt" CausesValidation="false"  Text="   Cancel   " onclick="btnCancel_Click" />
                        
			        </td>
			    </tr>
			    </table> 
           
                </td>
                </tr>
                    <//table>
                </td>
                </tr>
                 </table>
            </td>
            </tr>
            </table>   
             <br />
            <table cellSpacing="0" cellPadding="0" align="center" width="100%" border="0">
    <tr>
        <td align="left">
                                
        </td>
        <td align="right">
          <strong> <asp:Label ID="lblCount" CssClass="copy10grey" runat="server" ></asp:Label></strong>
            
            
        </td>
    </tr>
    <tr>
        <td colspan="2" align="center">

        <asp:GridView ID="gvWHCode" AutoGenerateColumns="false"   
        Width="100%" ShowHeader="true"  ShowFooter="false" runat="server" GridLines="Both"
        AllowPaging="true" OnPageIndexChanging="gvWHCode_PageIndexChanging" PageSize="50" 
           AllowSorting="true" OnSorting="gvWHCode_Sorting" >                        
        <RowStyle BackColor="Gainsboro" />
        <AlternatingRowStyle BackColor="white" />
        <HeaderStyle  CssClass="buttongrid" ForeColor="white"/>
            <PagerStyle ForeColor="#636363" CssClass="copy10grey" HorizontalAlign="Left" />
            <Columns>
                <asp:TemplateField HeaderText="S.No." ItemStyle-CssClass="copy10grey"  ItemStyle-Width="1%">                
                <ItemTemplate>
                        <%#  Container.DataItemIndex + 1%>               
                </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Company Name" SortExpression="CompanyName"  HeaderStyle-CssClass="buttonundlinelabel"   ItemStyle-HorizontalAlign="Left" 
                    ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                    <ItemTemplate>
                        <%# Eval("CompanyName")%>
                        </ItemTemplate>
                </asp:TemplateField>  
                 <asp:TemplateField HeaderText="Warehouse City" SortExpression="WarehouseCity"  HeaderStyle-CssClass="buttonundlinelabel" ItemStyle-HorizontalAlign="Left" 
                    ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                    <ItemTemplate>
                        <%# Eval("WarehouseCity")%>
                        </ItemTemplate>
                </asp:TemplateField>  
               
                <asp:TemplateField HeaderText="Warehouse Location" SortExpression="WarehouseLocation"  HeaderStyle-CssClass="buttonundlinelabel"   ItemStyle-HorizontalAlign="Left" 
                    ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                    <ItemTemplate>
                        <%# Eval("WarehouseLocation")%>
                        </ItemTemplate>
                </asp:TemplateField>  
                <asp:TemplateField HeaderText="Aisle" SortExpression="Aisle"  HeaderStyle-CssClass="buttonundlinelabel"   ItemStyle-HorizontalAlign="Left" 
                    ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                    <ItemTemplate>
                        <%# Eval("Aisle")%>
                        </ItemTemplate>
                </asp:TemplateField>  
                <asp:TemplateField HeaderText="Bay" SortExpression="Bay"  HeaderStyle-CssClass="buttonundlinelabel"   ItemStyle-HorizontalAlign="Left" 
                    ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                    <ItemTemplate>
                        <%# Eval("Bay")%>
                        </ItemTemplate>
                </asp:TemplateField>  
                <asp:TemplateField HeaderText="Level" SortExpression="RowLevel"  HeaderStyle-CssClass="buttonundlinelabel"   ItemStyle-HorizontalAlign="Left" 
                    ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                    <ItemTemplate>
                        <%# Eval("RowLevel")%>
                        </ItemTemplate>
                </asp:TemplateField>  
                <asp:TemplateField HeaderText="Category Name" SortExpression="CategoryName"  HeaderStyle-CssClass="buttonundlinelabel"   ItemStyle-HorizontalAlign="Left" 
                    ItemStyle-CssClass="copy10grey" ItemStyle-Width="8%">
                    <ItemTemplate>
                        <%# Eval("CategoryName")%>
                        </ItemTemplate>
                </asp:TemplateField>  
                <asp:TemplateField HeaderText="SKU" SortExpression="CategoryName"  HeaderStyle-CssClass="buttonundlinelabel"   ItemStyle-HorizontalAlign="Left" 
                    ItemStyle-CssClass="copy10grey" ItemStyle-Width="15%">
                    <ItemTemplate>
                        <%# Eval("SKU")%>
                        </ItemTemplate>
                </asp:TemplateField>  
                <asp:TemplateField HeaderText="Product Name" SortExpression="ItemName"  HeaderStyle-CssClass="buttonundlinelabel"   ItemStyle-HorizontalAlign="Left" 
                    ItemStyle-CssClass="copy10grey" ItemStyle-Width="20%">
                    <ItemTemplate>
                        <%# Eval("ItemName")%>
                        </ItemTemplate>
                </asp:TemplateField>  
                  
                <asp:TemplateField HeaderText="Quantity" SortExpression="Quantity"  HeaderStyle-CssClass="buttonundlinelabel"   ItemStyle-HorizontalAlign="Right" 
                    ItemStyle-CssClass="copy10grey" ItemStyle-Width="4%">
                    <ItemTemplate>
                        <%# Eval("Quantity")%>
                        </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Last Received Date" SortExpression="LastReceivedDate"  HeaderStyle-CssClass="buttonundlinelabel"   ItemStyle-HorizontalAlign="Left" 
                    ItemStyle-CssClass="copy10grey" ItemStyle-Width="6%">
                    <ItemTemplate>
                        <%# Convert.ToDateTime( Eval("LastReceivedDate")).ToString("MM-dd-yyyy")%>
                        </ItemTemplate>
                </asp:TemplateField>  
               
                
                     
                                    
            </Columns>
        </asp:GridView>               
        </td>
    </tr> 
        
</table>
    
            </ContentTemplate>
            </asp:UpdatePanel>
		
            </td>
       </tr>
    </table>
        <br /> <br />
            <br /> <br /><br /> <br />
            
        <table width="100%">
        <tr>
		    <td>
			    <foot:MenuFooter id="Menuheader2" runat="server"></foot:MenuFooter>
		    </td>
	    </tr>
        </table>
        
    </form>
</body>
</html>
