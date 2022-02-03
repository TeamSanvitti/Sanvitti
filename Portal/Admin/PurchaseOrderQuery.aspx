<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PurchaseOrderQuery.aspx.cs" Inherits="avii.PurchaseOrderQuery" %>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="./admhead.ascx" %>


<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Purchase Order Query</title>
		<link href="../aerostyle.css" rel="stylesheet" type="text/css">
		<script language="javascript" src="../avI.js"></script> 
		 <link rel="stylesheet" href="../fullfillment/calendar/dhtmlgoodies_calendar.css" media="screen"></link>
		<script type="text/javascript" src="../fullfillment/calendar/dhtmlgoodies_calendar.js"></script>  
		
		 <script language=javascript type="text/javascript">
    function expandcollapse(obj,row)
    {
        var div = document.getElementById(obj);
        var img = document.getElementById('img' + obj);
        
        if (div.style.display == "none")
        {
            div.style.display = "block";
            if (row == 'alt')
            {
                img.src = "../images/minus.gif";
            }
            else
            {
                img.src = "../images/minus.gif";
            }
            img.alt = "Close to view Purchase Order";
        }
        else
        {
            div.style.display = "none";
            if (row == 'alt')
            {
                img.src = "../images/plus.gif";
            }
            else
            {
                img.src = "../images/plus.gif";
            }
            img.alt = "Expand to show Orders";
        }
    } 
    </script>
     
</head>
<BODY bgColor="#ffffff" leftmargin="0" rightmargin="0" topmargin="0">
    <form id="form1" runat="server">
    			<table align="center"  border="0" cellpadding="0" cellspacing="0" width="100%">
				<tr>
					<td valign="top" align="left" align="center">
					<head:menuheader id="MenuHeader" runat="server"></head:menuheader>
					</td>
				</tr>
				 <tr><td><br/></td></tr>
			</table> 
    <div>
    <table  cellSpacing="1" cellPadding="1" width="100%" >
        <tr>
			<td colSpan="6" bgcolor="#dee7f6" class="button">&nbsp;&nbsp;Purchase Order Query
			</td>
        </tr>
        <tr><td>&nbsp;</td></tr>
        <tr><td><asp:Label ID="lblMsg" runat="server" CssClass="errormessage"></asp:Label></td></tr>
    </table>
    <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="100%" >
                <tr bordercolor="#839abf">
                <td>
        <table cellSpacing="1" cellPadding="1" width="100%"  >
            <tr><td>&nbsp;</td></tr>
            <tr>
                <td align="right" class="copy10grey" width="15%">
                    Purchase Order#:</td>
                <td>
                </td>
                <td>
                    <asp:TextBox ID="txtPONum" MaxLength="20" runat="server" CssClass="copy10grey" ></asp:TextBox></td>
                <td align="right" class="copy10grey" width="15%">Store ID:</td>
                
                <td></td>
                <td>
                    <asp:TextBox ID="txtStoreID" MaxLength="20" runat="server" CssClass="copy10grey" ></asp:TextBox></td>
            </tr>
            <tr>
                <td align="right" class="copy10grey">
                    PO Date From:</td>
                <td>
                </td>
                <td>
                    <asp:TextBox MaxLength="12" ID="txtFromDate" runat="server" CssClass="copy10grey" ReadOnly="true"></asp:TextBox>
                    <img id="imgFromtDate" alt="" onclick="document.getElementById('<%=txtFromDate.ClientID%>').value = ''; displayCalendar(document.getElementById('<%=txtFromDate.ClientID%>'),'mm/dd/yyyy',this,true);" src="../fullfillment/calendar/sscalendar.jpg" />
                </td>
                <td align="right" class="copy10grey">PO Date To:</td>
                <td></td>
                <td><asp:TextBox ID="txtToDate" MaxLength="12" runat="server" CssClass="copy10grey" ReadOnly="true" ></asp:TextBox>
                    <img id="imgToDate"  alt="" onclick="document.getElementById('<%=txtToDate.ClientID%>').value = ''; displayCalendar(document.getElementById('<%=txtToDate.ClientID%>'),'mm/dd/yyyy',this,true);" src="../fullfillment/calendar/sscalendar.jpg" />
                
                </td>               
            </tr>
            <tr><td colspan="6">&nbsp;</td></tr>
            <tr><td colspan="6" align="center"><asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Search Purchase Order" OnClick="btnSearch_Click" />&nbsp;
            <asp:Button ID="btnDown" runat="server" CssClass="button" Visible="false" OnClick= "btnDown_Click" Text="Download Purchase Order" />&nbsp;
            <asp:Button ID="btnUpload" runat="server" CssClass="button" Visible="false"  Text="Upload Purchase Order" OnClick= "btnUpload_Click" />&nbsp;<asp:Button ID="btnCancel" runat="server" CssClass="button" Text="Cancel" OnClick="btnCancel_Click" />
            
            </td></tr>
            <tr id="trUpload" runat="server" visible="false"><td align="center" colspan="6">
                <table width="35%" border="2"  align="center" BackColor="Gainsboro" cellpadding="0" cellspacing="0">
                    <tr>
                        <td width="70%"><asp:FileUpload ID="flnUpload" runat="server" CssClass="txfield1" Width="100%" /></td>
                        <td align="center"><asp:Button ID="btnUpd" runat="server" Text="Upload" CssClass="button" OnClick="btn_UpdClick" /></td>
                    </tr>
                </table>
            </td></tr>
            <tr><td colspan="6" align="left"><asp:Label ID="lblTime" CssClass="copy10" runat="server"></asp:Label></td></tr>
            </table>
            			</td></tr>
			</table>
            <table cellSpacing="1" cellPadding="1" width="100%" >
            <tr><td>&nbsp;</td></tr>
            <tr><td>
            
           <asp:GridView ID="GridView1"     AutoGenerateColumns="false"  DataKeyNames="PO_ID"  Width="100%"  
           ShowFooter="false" runat="server" GridLines="Both" OnRowDataBound="GridView1_RowDataBound"
            OnRowCommand = "GridView1_RowCommand" OnRowUpdating = "GridView1_RowUpdating" BorderStyle="Outset"
            OnRowDeleting = "GridView1_RowDeleting" OnRowDeleted = "GridView1_RowDeleted" OnRowEditing="GridView1_RowEditing"
            OnRowUpdated = "GridView1_RowUpdated" AllowSorting="false" OnRowCancelingEdit="GridView1_RowCancelingEdit"> 
            <RowStyle BackColor="Gainsboro" />
            <AlternatingRowStyle BackColor="white" />
            <HeaderStyle  CssClass="button" ForeColor="white"/>
            <FooterStyle CssClass="white"  />
            <Columns>
                <asp:TemplateField ItemStyle-CssClass="copy10grey"  ItemStyle-Width="3%">
                    <ItemTemplate>
                        <asp:CheckBox ID="chk"  runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>  
                <asp:TemplateField ItemStyle-CssClass="copy10grey"  ItemStyle-Width="3%">
                    <ItemTemplate>
                        <asp:Button ID="btnSel" CommandName="sel" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>  
                 
                <asp:TemplateField>
                    <ItemTemplate>
                        <a href="javascript:expandcollapse('div<%# Eval("PO_ID") %>', 'one');">
                            <img id="imgdiv<%# Eval("PO_ID") %>" alt="Click to show/hide Items for Purchase Order <%# Eval("PO_ID") %>"  width="9px" border="0" src="../images/plus.gif"/>
                        </a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="PO#" SortExpression="PO_Num" ItemStyle-CssClass="copy10grey" ItemStyle-Width="6%">
                    <ItemTemplate>
                        <asp:Label ID="lblPoNum" runat="server" Text='<%# Eval("PO_Num") %>'></asp:Label>
                        
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="PO Date" SortExpression="PO_DATE" ItemStyle-CssClass="copy10grey"  ItemStyle-Width="11%">
                    <ItemTemplate><%# Eval("PO_DATE")%></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Contact Name" SortExpression="ContactName" ItemStyle-CssClass="copy10grey">
                    <ItemTemplate><%# Eval("Contact_Name")%></ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtContactName" CssClass="copy10grey"  MaxLength="50"  Text='<%# Eval("Contact_Name") %>' runat="server"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Ship Attn" SortExpression="ShipTo_Attn" ItemStyle-CssClass="copy10grey">
                    <ItemTemplate><%# Eval("ShipTo_Attn")%></ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtShipAttn" CssClass="copy10grey"  MaxLength="50" Text='<%# Eval("ShipTo_Attn") %>' runat="server"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Shippping" SortExpression="Ship_Via" ItemStyle-CssClass="copy10grey">
                    <ItemTemplate><%# Eval("Ship_Via")%></ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtVia" CssClass="copy10grey"  MaxLength="200" TextMode="MultiLine" Text='<%# Eval("Ship_Via") %>' runat="server"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Tracking#" SortExpression="TrackingNumber" ItemStyle-CssClass="copy10grey">
                    <ItemTemplate><%# Eval("TrackingNumber")%></ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtTrack" CssClass="copy10grey"  MaxLength="200" TextMode="MultiLine" Text='<%# Eval("TrackingNumber") %>' runat="server"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>                
			    
			    <asp:CommandField HeaderText="Edit" ShowEditButton="True" HeaderStyle-CssClass="button" ControlStyle-CssClass="linkgrid"  ItemStyle-HorizontalAlign="Center"/>
			    <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center" >
                    <ItemTemplate>
                        <asp:LinkButton ID="linkDeleteCust" CssClass="linkgrid" CommandName="Delete" runat="server">Delete</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                
			    <asp:TemplateField HeaderText="Send PO" ItemStyle-HorizontalAlign="Center" >
                    <ItemTemplate>
                        <asp:LinkButton ID="sendPO" CssClass="linkgrid" CommandArgument='<%# Eval("PO_ID") %>' CommandName="SendPO" runat="server">Send PO</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField>
			        <ItemTemplate>
			            <tr>
                            <td colspan="100%">
                                
                                <div id="div<%# Eval("PO_ID") %>"  style="display:none;position:relative;left:15px;OVERFLOW: auto;WIDTH:97%;">
                                    <asp:GridView ID="GridView2"  BackColor="White" Width="100%"
                                        AutoGenerateColumns="false" Font-Names="Verdana" runat="server" DataKeyNames="POD_ID"
                                         OnRowUpdating = "GridView2_RowUpdating"
                                        OnRowCommand = "GridView2_RowCommand" OnRowEditing = "GridView2_RowEditing" GridLines="Both"
                                        OnRowUpdated = "GridView2_RowUpdated" OnRowCancelingEdit = "GridView2_CancelingEdit" OnRowDataBound = "GridView2_RowDataBound"
                                        OnRowDeleting = "GridView2_RowDeleting" OnRowDeleted = "GridView2_RowDeleted"
                                        BorderStyle="Double" BorderColor="#0083C1">
                                        <RowStyle BackColor="Gainsboro" />
                                        <AlternatingRowStyle BackColor="white" />
                                        <HeaderStyle  CssClass="button" ForeColor="white"/>
                                        <FooterStyle CssClass="white"  />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Item Code" SortExpression="Item_Code" ItemStyle-CssClass="copy10grey"  ItemStyle-Width="20%">
                                                <ItemTemplate><asp:Label ID="lblItemCode" runat="server" Text='<%# Eval("Item_Code")%>'></asp:Label></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Qty" SortExpression="Qty"  ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                                                <ItemTemplate><%# Eval("Qty")%></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ESN" SortExpression="ESN" ItemStyle-CssClass="copy10grey" ItemStyle-Width="20%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEsn" CssClass="copy10grey" Text='<%# Eval("ESN") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtEsn" CssClass="copy10grey" MaxLength="50"  Text='<%# Eval("ESN") %>' runat="server"></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField HeaderText="Pass Code" SortExpression="Pass_Code" ItemStyle-CssClass="copy10grey" ItemStyle-Width="15%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPass"  CssClass="copy10grey" Text='<%# Eval("Pass_Code") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtPass" CssClass="copy10grey"  MaxLength="50" Text='<%# Eval("Pass_Code") %>' runat="server"></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateField>                                                                                            
                                            <asp:TemplateField HeaderText="Box ID" SortExpression="Box_id" ItemStyle-CssClass="copy10grey" ItemStyle-Width="20%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBox" CssClass="copy10grey" Text='<%# Eval("Box_id") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtBox" CssClass="copy10grey" MaxLength="50"  Text='<%# Eval("Box_id") %>' runat="server"></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateField>  
                                            <asp:TemplateField HeaderText="Cost" SortExpression="Cost" ItemStyle-CssClass="copy10grey" ItemStyle-Width="20%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCost" CssClass="copy10grey" Text='<%# Eval("Cost") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtCost" CssClass="copy10grey" MaxLength="50"  Text='<%# Eval("Cost") %>' runat="server"></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateField>                                                                                                                         
                                                                                        
			                                <asp:CommandField HeaderText="Edit" ShowEditButton="True"  HeaderStyle-CssClass="button" ControlStyle-CssClass="linkgrid"  ItemStyle-HorizontalAlign="Center" />
			                                <asp:TemplateField HeaderText="Delete" HeaderStyle-CssClass="button"  ItemStyle-HorizontalAlign="Center">
                                                 <ItemTemplate>
                                                    <asp:LinkButton ID="linkDeleteCust" CssClass="linkgrid"  CommandName="Delete" runat="server">Delete</asp:LinkButton>
                                                 </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                   </asp:GridView>
                                </div>
                             </td>
                        </tr>
			        </ItemTemplate>			       
			    </asp:TemplateField>			    
			</Columns>
        </asp:GridView>          
            
            </td></tr>
             <tr><td><br/></td></tr>
              <tr><td><br/></td></tr>
        </table>
        
    </div>
    </form>
</body>
</html>
