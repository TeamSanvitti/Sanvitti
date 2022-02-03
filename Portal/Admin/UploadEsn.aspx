<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UploadEsn.aspx.cs" Inherits="avii.Admin.UploadEsn" %>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="/Controls/Header.ascx" %>


<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
        <title>ESN Management</title>
		<link href="../aerostyle.css" rel="stylesheet" type="text/css"/>
		<script type="text/javascript" language="javascript" src="./avI.js"></script> 
		 <link rel="stylesheet"  type="text/css" href="../fullfillment/calendar/dhtmlgoodies_calendar.css" media="screen"></link>
		<script type="text/javascript" src="../fullfillment/calendar/dhtmlgoodies_calendar.js"></script>  
</head>
<body bgColor="#ffffff" leftmargin="0" rightmargin="0" topmargin="0">
    <form id="form1" runat="server">
            <TABLE cellSpacing="0" cellPadding="0"  border="0" align="center" width="100%">
				<TR>
					<TD>
					<head:menuheader id="HeadAdmin" runat="server"></head1:menuheader>
					</TD>
				</TR>
			</TABLE>
			
			    <br />
            <table  cellSpacing="1" cellPadding="1" width="100%">
                <tr>
			        <td colSpan="6" bgcolor="#dee7f6" class="button">&nbsp;&nbsp;ESN Management
			        </td>
                </tr>
                <tr><td><asp:Label ID="lblMsg" runat="server" CssClass="errormessage"></asp:Label></td></tr>
                <tr><td class="copy10grey">
                    - Please enter multiple Purchase Order#s in Comma (,) seperated format
                </td></tr>
            </table>
            
            
            <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="100%" >
                <tr bordercolor="#839abf">
                    <td>
                        <table cellSpacing="1" cellPadding="1" width="100%"  >
                             <tr>
                                <td align="right" class="copy10grey" width="15%">
                                    Purchase Order#:</td>
                                <td>
                                
                                <td colspan="4">
                                    <asp:TextBox ID="txtPONum" Width="80%" onkeypress="JavaScript:return alphaOnly(event);" MaxLength="2000" runat="server" CssClass="copy10grey" ></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="copy10grey">
                                    PO Date From:</td>
                                <td>
                                </td>
                                <td>
                                    &nbsp;<asp:TextBox ID="txtFromDate" runat="server" CssClass="copy10grey" MaxLength="15"></asp:TextBox>
                                    <img id="imgFromtDate" alt="" onclick="document.getElementById('<%=txtFromDate.ClientID%>').value = ''; displayCalendar(document.getElementById('<%=txtFromDate.ClientID%>'),'mm/dd/yyyy',this,true);" src="../fullfillment/calendar/sscalendar.jpg" />
                                </td>
                                    <td align="right" class="copy10grey">PO Date To:</td>
                                <td></td>
                                <td>
                                    &nbsp;<asp:TextBox ID="txtToDate" runat="server" CssClass="copy10grey" MaxLength="15"></asp:TextBox>
                                    <img id="imgToDate"  alt="" onclick="document.getElementById('<%=txtToDate.ClientID%>').value = ''; displayCalendar(document.getElementById('<%=txtToDate.ClientID%>'),'mm/dd/yyyy',this,true);" src="../fullfillment/calendar/sscalendar.jpg" />
                                
                                </td>               
                            </tr>
                            <tr>
                                <td align="right" class="copy10grey">
                                    PO Status:</td>
                                <td></td>
                                <td>
                                    <asp:DropDownList ID="dpStatusList" runat="server" class="copy10grey" Width="80%">
                                                <asp:ListItem Text="" Value=""></asp:ListItem>
                                                <asp:ListItem Text="Pending" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="Processed" Value="2"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <asp:Panel ID="pnlCompany" runat="server" Visible="false">
                                <td align="right" class="copy10grey">
                                    Company:</td>
                                <td></td>
                                <td>
                                    <asp:DropDownList ID="dpCompany" runat="server" class="copy10grey">
                                                <asp:ListItem Text="" Value=""></asp:ListItem>
                                                <asp:ListItem Text="iWireless" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="telSpace" Value="1"></asp:ListItem>
                                            </asp:DropDownList>
                                </td>
                                </asp:Panel>
                            </tr>
                            
                            <tr>
                                <td align="right" class="copy10grey">
                                    ESN:</td>
                                <td></td>
                                <td>
                                    <asp:TextBox ID="txtEsn" onkeypress="JavaScript:return alphaOnly(event);" 
                                        runat="server"  CssClass="copy10grey" MaxLength="35" Width="60%"></asp:TextBox>
                                </td>
                                <td align="right" class="copy10grey">
                                MSL Number:</td>
                                <td></td>
                                <td>
                                    <asp:TextBox ID="txtMslNumber" runat="server" onkeypress="JavaScript:return alphaOnly(event);" CssClass="copy10grey" MaxLength="32" Width="80%"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="6">
                                
                        
                                </td>
                            </tr>
                            <tr><td colspan="6"><hr /></td></tr>
                            <tr><td colspan="6" align="center">
                                    <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Search ESN " OnClick="btnSearch_Click" />&nbsp;&nbsp;
                                    <asp:Button ID="btnCancel" runat="server" CssClass="button" Text="Cancel Search" OnClick="btnCancel_Click" /><br />      
	                        </td></tr>

            </table>
            	    </td>
            	 </tr>
           </table>
            	 
            <table width="100%">
                <tr>
                    <td align="right">
                                    <asp:Button ID="btnClear" runat="server" CssClass="button"  Visible="false"
                                        Text="Clear ESN Assignment" onclick="btnClear_Click"/>&nbsp;&nbsp;
                                    <asp:Button ID="btnValidateESN" runat="server" CssClass="button" Text="Validate Assigned  ESN" Visible="false" />&nbsp;&nbsp;
                                    <asp:Button ID="btnSubmit" runat="server" Enabled="false" CssClass="button" Text="Submit Assignment" Visible="false" />
                    </td>
                </tr>
            
                <tr>
                    <td colspan="6">                    
                            <asp:GridView ID="grdEsn"   AutoGenerateColumns="false" Width="100%"  
                                DataKeyNames="PO_ID"  ShowFooter="false" runat="server" GridLines="Both"> 
                            <RowStyle BackColor="Gainsboro" />
                            <AlternatingRowStyle BackColor="white" />
                            <HeaderStyle  CssClass="button" ForeColor="white"/>
                            <FooterStyle CssClass="white"  />
                            <Columns>                                
                                <asp:TemplateField HeaderText="PO#" SortExpression="PO_Num" ItemStyle-CssClass="copy10grey" ItemStyle-Width="6%">
                                    <ItemTemplate>
                                        <%# Eval("PO_Num")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="PO Date"  SortExpression="po_date" ItemStyle-CssClass="copy10grey"  ItemStyle-Width="11%">
                                    <ItemTemplate><%# DataBinder.Eval(Container.DataItem, "po_date", "{0:d}") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="SKU#" SortExpression="ITEM_CODE" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <%# Eval("ITEM_CODE")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="UPC" SortExpression="UPC" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <%# Eval("UPC")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ESN" SortExpression="ESN" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtEsn" Width="100%" CssClass="copy10grey" MaxLength="32"  Text='<%# Eval("ESN") %>' runat="server"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>   
                                <asp:TemplateField HeaderText="MslNumber" SortExpression="MSL" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:Label ID="txtMsl" CssClass="copy10grey" MaxLength="32"  Text='<%# Eval("mslNumber") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField> 
                                <asp:TemplateField HeaderText="FM UPC" SortExpression="fmupc" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtfmupc"  Width="100%"  CssClass="copy10grey" MaxLength="32"  Text='<%# Eval("fmupc") %>' runat="server"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField> 
                             </Columns> 
                            </asp:GridView>
                    </td>
                </tr>
            </table>
            
        <table>
				<tr>
					<td>
						&nbsp;
					</td>
				</tr>
				<TR>
					<TD>
				    </TD>
				</TR>        
        </table>            
    </form>
</body>
</html>
