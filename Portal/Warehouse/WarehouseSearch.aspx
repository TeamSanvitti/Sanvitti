<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WarehouseSearch.aspx.cs" Inherits="avii.Warehouse.WarehouseSearch" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="~/Controls/Header.ascx" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Warehouse Search</title>
    <script>
        function OpenNewPage(url) {

            var newWin = window.open(url);

            if (!newWin || newWin.closed || typeof newWin.closed == 'undefined') {
                alert('your pop up blocker is enabled');

                //POPUP BLOCKED
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
            &nbsp;&nbsp;Warehouse Search
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
                    <asp:DropDownList ID="ddlCompany" TabIndex="2" runat="server" CssClass="copy10grey" Width="60%">
                    </asp:DropDownList>

                </td>
                <td  width="1%">
                       &nbsp;
                    
                </td>
                <td class="copy10grey"  align="right" width="19%" >
                </td>
                <td width="30%" >
                    
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
			            <asp:Button ID="btnSearch" runat="server" TabIndex="18"  CssClass="buybt" OnClientClick="return Validate();" 
                                        Text="   Search   " onclick="btnSearch_Click" />&nbsp;&nbsp;
                        <asp:Button ID="btnCancel" runat="server" TabIndex="19" CssClass="buybt" CausesValidation="false"  Text="   Cancel   " onclick="btnCancel_Click" />&nbsp;&nbsp;
                        <asp:Button ID="btnNewLocation" runat="server" TabIndex="20" CssClass="buybt" CausesValidation="false"  Text="New Warehouse Location" onclick="btnNewLocation_Click" />
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
           AllowSorting="true" OnSorting="gvgvWHCode_Sorting" >                        
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
                <asp:TemplateField HeaderText="Warehouse Location" SortExpression="WarehouseLocation"  HeaderStyle-CssClass="buttonundlinelabel"   ItemStyle-HorizontalAlign="Left" 
                    ItemStyle-CssClass="copy10grey" ItemStyle-Width="15%">
                    <ItemTemplate>
                        <%# Eval("WarehouseLocation")%>
                        </ItemTemplate>
                </asp:TemplateField>  
                <asp:TemplateField HeaderText="Warehouse City" SortExpression="WarehouseCity"  HeaderStyle-CssClass="buttonundlinelabel" ItemStyle-HorizontalAlign="Left" 
                    ItemStyle-CssClass="copy10grey" ItemStyle-Width="15%">
                    <ItemTemplate>
                        <%# Eval("WarehouseCity")%>
                        </ItemTemplate>
                </asp:TemplateField>  
                <asp:TemplateField HeaderText="Aisle" SortExpression="Aisle"  HeaderStyle-CssClass="buttonundlinelabel"   ItemStyle-HorizontalAlign="Left" 
                    ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                    <ItemTemplate>
                        <%# Eval("Aisle")%>
                        </ItemTemplate>
                </asp:TemplateField>  
                <asp:TemplateField HeaderText="Bay" SortExpression="Bay"  HeaderStyle-CssClass="buttonundlinelabel"   ItemStyle-HorizontalAlign="Left" 
                    ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                    <ItemTemplate>
                        <%# Eval("Bay")%>
                        </ItemTemplate>
                </asp:TemplateField>  
                <asp:TemplateField HeaderText="Level" SortExpression="RowLevel"  HeaderStyle-CssClass="buttonundlinelabel"   ItemStyle-HorizontalAlign="Left" 
                    ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                    <ItemTemplate>
                        <%# Eval("RowLevel")%>
                        </ItemTemplate>
                </asp:TemplateField>  
                <asp:TemplateField HeaderText="Customer" SortExpression="CompanyName"  HeaderStyle-CssClass="buttonundlinelabel"   ItemStyle-HorizontalAlign="Left" 
                    ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                    <ItemTemplate>
                        <%# Eval("CompanyName")%>
                        </ItemTemplate>
                </asp:TemplateField>  
                <asp:TemplateField HeaderText="Create Date" SortExpression="CreatedDateTime"  HeaderStyle-CssClass="buttonundlinelabel"   ItemStyle-HorizontalAlign="Left" 
                    ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                    <ItemTemplate>
                        <%# Eval("CreateDate")%>
                        </ItemTemplate>
                </asp:TemplateField>  
                <asp:TemplateField HeaderText="CreatedBy" HeaderStyle-CssClass="buttongrid"   ItemStyle-HorizontalAlign="Left" 
                    ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                    <ItemTemplate>
                        <%# Eval("CreatedBy")%>
                        </ItemTemplate>
                </asp:TemplateField>  
                                       
                
                <asp:TemplateField HeaderText="Action" HeaderStyle-CssClass="buttongrid"  ItemStyle-HorizontalAlign="Left" 
                    ItemStyle-CssClass="copy10grey" ItemStyle-Width="6%"> 
                    <ItemTemplate>
                        <asp:ImageButton ID="imgView"  ToolTip="View Warehouse Location" OnCommand="imgView_Command"
                        CausesValidation="false" CommandArgument='<%# Eval("WarehouseLocation") %>' ImageUrl="~/Images/view.png"  runat="server" />
                        
                        <asp:ImageButton ID="imgEdit"  ToolTip="Edit Warehouse Code" OnCommand="imgEdit_Command"
                        CausesValidation="false" CommandArgument='<%# Eval("WarehouseStorageID") %>' ImageUrl="~/Images/edit.png"  runat="server" />
                        <asp:ImageButton ID="imgDelete"  ToolTip="Delete" OnCommand="imgDelete_Command" OnClientClick="return confirm('Are you sure want to delete?')"
                        CausesValidation="false" CommandArgument='<%# Eval("WarehouseStorageID") %>' ImageUrl="~/Images/Delete.png"  runat="server" />
                                                                  
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
