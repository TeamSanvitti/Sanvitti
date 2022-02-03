<%@ Page Language="c#" AutoEventWireup="true" CodeBehind="PurchaseOrderQuery.aspx.cs" Inherits="avii.PurchaseOrderQuery" %>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="/Controls/Header.ascx" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="/Controls/Footer.ascx" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Purchase Order Query</title>
		<link href="./aerostyle.css" rel="stylesheet" type="text/css"/>
		<script type="text/javascript" language="javascript" src="./avI.js"></script> 
		 <link rel="stylesheet"  type="text/css" href="../fullfillment/calendar/dhtmlgoodies_calendar.css" media="screen"></link>
		<script type="text/javascript" src="../fullfillment/calendar/dhtmlgoodies_calendar.js"></script>  
		
		 <script language=javascript type="text/javascript">

		 function ValidateStatus() {
	        var status = document.getElementById("<%=dpStatus.ClientID  %>");
	        if (status.selectedIndex == 0) {
	            alert('Select a status first');
	            return false;
            }
        }
		function alphaOnly(eventRef)
        {
         var returnValue = false;
         var keyStroke = (eventRef.which) ? eventRef.which : (window.event) ? window.event.keyCode : -1;
         if ( ((keyStroke >= 65) && (keyStroke <= 90)) ||
              ((keyStroke >= 97) && (keyStroke <= 122)) ||
              ((keyStroke >= 45 && keyStroke < 58)))
                 returnValue = true;

         if ( navigator.appName.indexOf('Microsoft') != -1 )
          window.event.returnValue = returnValue;

         return returnValue;
         
        }



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
<body bgColor="#ffffff" leftmargin="0" rightmargin="0" topmargin="0">
    <form id="form1" runat="server">
			<TABLE cellSpacing="0" cellPadding="0"  border="0" align="center" width="100%">
				<TR>
					<TD><head:menuheader id="MenuHeader" runat="server"></head:menuheader>
				
					</TD>
				</TR>
				<TR>
					<TD align="center">
    <div>
    <br />
    <table  cellSpacing="1" cellPadding="1" width="100%">
        <tr>
			<td colSpan="6" bgcolor="#dee7f6" class="button">&nbsp;&nbsp;Purchase Order Query
			</td>
        </tr>
        <tr><td><asp:Label ID="lblMsg" runat="server" CssClass="errormessage"></asp:Label></td></tr>
    </table>
    <table  cellSpacing="1" cellPadding="1" width="100%">
                <tr><td class="copy10grey">
                - Please select your search
                criterial to narrow down the search and record selection.<br />
                - Atleast one search criteria should be selected.


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
                </td>
                <td>
                    <asp:TextBox ID="txtPONum" onkeypress="JavaScript:return alphaOnly(event);" MaxLength="20" runat="server" CssClass="copy10grey" ></asp:TextBox></td>
                <td align="right" class="copy10grey" width="15%">
                    Contact Name:</td>
                
                <td></td>
                <td>
                    <asp:TextBox ID="txtCustName" MaxLength="20" runat="server" CssClass="copy10grey" ></asp:TextBox></td>
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
                    Shipping Date From:</td>
                <td>
                </td>
                <td>
                    &nbsp;<asp:TextBox ID="txtShipFrom" runat="server" CssClass="copy10grey" MaxLength="15"></asp:TextBox>
                    <img id="img1" alt="" onclick="document.getElementById('<%=txtShipFrom.ClientID%>').value = ''; displayCalendar(document.getElementById('<%=txtShipFrom.ClientID%>'),'mm/dd/yyyy',this,true);" src="../fullfillment/calendar/sscalendar.jpg" />
                </td>
                    <td align="right" class="copy10grey">Shipping Date To:</td>
                <td></td>
                <td>
                    &nbsp;<asp:TextBox ID="txtShipTo" runat="server" CssClass="copy10grey" MaxLength="15"></asp:TextBox>
                    <img id="img2"  alt="" onclick="document.getElementById('<%=txtShipTo.ClientID%>').value = ''; displayCalendar(document.getElementById('<%=txtShipTo.ClientID%>'),'mm/dd/yyyy',this,true);" src="../fullfillment/calendar/sscalendar.jpg" />
                
                </td>               
            </tr>            
            <tr>
                <td align="right" class="copy10grey">
                    PO Status:</td>
                <td></td>
                <td>
                    <asp:DropDownList ID="dpStatusList" runat="server" class="copy10grey">
                                <asp:ListItem Text="" Value=""></asp:ListItem>
                                <asp:ListItem Text="Pending" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Processed" Value="2"></asp:ListItem>
                                <asp:ListItem Text="Shipped" Value="3"></asp:ListItem>
                                <asp:ListItem Text="Closed" Value="4"></asp:ListItem>
                                <asp:ListItem Text="Return" Value="5"></asp:ListItem>
                                <asp:ListItem Text="Cancel" Value="9"></asp:ListItem>
<asp:ListItem Text="On Hold" Value="6"></asp:ListItem>
                                <asp:ListItem Text="Out of Stock" Value="7"></asp:ListItem>
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
                    <asp:TextBox ID="txtEsn" onkeypress="JavaScript:return alphaOnly(event);" runat="server"  CssClass="copy10grey" MaxLength="20"></asp:TextBox>
                </td>
                  <td align="right" class="copy10grey">
                    AVSO#:</td>
                <td></td>
                <td>
                    <asp:TextBox ID="txtAvNumber" onkeypress="JavaScript:return alphaOnly(event);" runat="server"  CssClass="copy10grey" MaxLength="20"></asp:TextBox>
                </td>
            </tr> 

            <tr>
                <td align="right" class="copy10grey">
                    MSL Number:</td>
                <td></td>
                <td>
                    <asp:TextBox ID="txtMslNumber" runat="server" onkeypress="JavaScript:return alphaOnly(event);" CssClass="copy10grey" MaxLength="20"></asp:TextBox>
                </td>
                  <%--<td align="right" class="copy10grey">
                    Phone Category:</td>
                <td></td>
                <td>
                    <asp:DropDownList ID="dpPhoneCategory" runat="server"  class="copy10grey">
                                <asp:ListItem Text="" Value=""></asp:ListItem>
                                <asp:ListItem Text="Hot" Value="H"></asp:ListItem>
                                <asp:ListItem Text="Cold" Value="C"></asp:ListItem>
                    </asp:DropDownList>
                </td>--%>
<td align="right" class="copy10grey">
                    Store ID:</td>
                <td></td>
                <td>
                    <asp:TextBox ID="txtStoreID" runat="server" onkeypress="JavaScript:return alphaOnly(event);"  CssClass="copy10grey" MaxLength="20"></asp:TextBox>
                </td>
            </tr> 
            
            <tr>
                <td align="right" class="copy10grey">
                    Item Code:</td>
                <td></td>
                <td>
                    <asp:TextBox ID="txtItemCode" runat="server" onkeypress="JavaScript:return alphaOnly(event);" CssClass="copy10grey" MaxLength="20"></asp:TextBox>
                </td>
                 
<td></td>
	<td></td>
	<td></td>                 
            </tr>
            <tr>
               <td align="right" class="copy10grey">
                    FM-UPC:</td>
                <td></td>
                <td>
                    <asp:TextBox ID="txtFmUpc" runat="server" onkeypress="JavaScript:return alphaOnly(event);" CssClass="copy10grey" MaxLength="35"></asp:TextBox>
                </td>
            </tr>
            <tr>
                 <td align="right" class="copy10grey">
                    Zone:</td>
                <td></td>
                <td>
                    <asp:DropDownList ID="dpZone" runat="server"  class="copy10grey">
                        <asp:ListItem Text="" Value=""></asp:ListItem> 
                        <asp:ListItem Text="Zone1" Value="1"></asp:ListItem> 
                        <asp:ListItem Text="Zone2" Value="2"></asp:ListItem> 
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="6"><asp:CheckBox ID="chkDownload"  runat="server" Text="Download selected records only" CssClass="copy10grey" /></td>
            </tr>
            <tr><td colspan="6"><hr /></td></tr>
            <tr><td colspan="6" align="center"><asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Search Purchase Order" OnClick="btnSearch_Click" />&nbsp;
            &nbsp;<asp:Button ID="btnCancel" runat="server" CssClass="button" Text="Cancel Search" OnClick="btnCancel_Click" /><br />
            <asp:Button ID="btnDownPO" runat="server" CssClass="button" OnClick= "btnDownPO_Click" Text="Download Purchase Order" Visible="False"  />&nbsp;
            <asp:Button ID="btnDown" runat="server" CssClass="button" OnClick= "btnDown_Click" Text="Download PO for ESN" Visible="False" />           
            <asp:Button ID="btnUpload" runat="server" CssClass="button"  Text="Upload ESN" OnClick= "btnUpload_Click" Visible="False" />
            <asp:Button ID="btnChangeStatus" runat="server" CssClass="button" Text="Change Status" OnClick="btnChangeStatus_Click" Visible="False" />
            <asp:Button ID="btnSendESN" runat="server" CssClass="button" Text="Send ESN to iWireless" Visible="False" OnClick="btnSendESN_Click" />
            <asp:Button ID="Button1" runat="server" CssClass="button" Text="Send ASN to iWireless" Visible="False" OnClick="btnSendESN_Click" />
       	    <asp:Button ID="btnPoDetail" runat="server" CssClass="button" Text="Download Purchase Order Details" Visible="False" CausesValidation="False" OnClick="btnPoDetail_Click" Width="253px"  />
	        <asp:Button ID="btnPoDetailTrk" runat="server" CssClass="button" Text="Download Purchase Order Detail ||" Visible="False" CausesValidation="False" OnClick="btnPoDetailTrk_Click" />
	        <br />
	        <asp:Button ID="btnEsn_Excel" runat="server" CssClass="button" OnClick= "btnEsn_Excel_Click" Text="Download PO for ESN (Excel)" Visible="False" />&nbsp;
	        <asp:Button ID="btnPoHeader" runat="server" CssClass="button" OnClick= "btnPoHeader_Click" Text="Download PO Header" Visible="False" />&nbsp;
	        
	        </td></tr>
	                
<tr id="trUpload" runat="server" visible="false">
                <td align="center" colspan="6" style="height: 48px">
                    <table width="35%" border="2"  align="center" BackColor="Gainsboro" cellpadding="0" cellspacing="0">
                        <tr>
                            <td width="70%"><asp:FileUpload ID="flnUpload" runat="server" CssClass="txfield1" Width="100%" /></td>
                            <td align="center"><asp:Button ID="btnUpd" runat="server" Text="Upload" CssClass="button" OnClick="btn_UpdClick" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr id="trStatus" runat="server" visible="false">
                <td align="center" colspan="6">
                  <table width="40%" border="1" align="center" BackColor="Gainsboro" cellpadding="0" cellspacing="0">
                        <tr valign="middle">
                        <td valign="middle" align="center">
                    <table width="100%" border="0" align="center" BackColor="Gainsboro" cellpadding="0" cellspacing="0">
                        <tr valign="middle" >
                            <td width="70%" class="copy10grey" align="center">Status: 
                            <asp:DropDownList ID="dpStatus" runat="server">
                                <asp:ListItem Text="" Value=""></asp:ListItem>
                                <asp:ListItem Text="Pending" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Processed" Value="2"></asp:ListItem>
                                <asp:ListItem Text="Shipped" Value="3"></asp:ListItem>
                                <asp:ListItem Text="Closed" Value="4"></asp:ListItem>
                                <asp:ListItem Text="Return" Value="5"></asp:ListItem>
                                <asp:ListItem Text="Cancel" Value="9"></asp:ListItem>
                                <asp:ListItem Text="Deleted" Value="6"></asp:ListItem>
                                <%--<asp:ListItem Text="Out of Stock" Value="7"></asp:ListItem>--%>
                            </asp:DropDownList></td>
                            <td align="center" valign="bottom">
                                <asp:Button ID="btbSubmitStatus" runat="server" Text="Submit" CssClass="button" OnClientClick="return ValidateStatus();" OnClick="btn_UpdStatusClick" />
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
	</table>
        <table cellSpacing="1" cellPadding="1" width="100%" >
            <tr><td style="width: 1032px">&nbsp;
                &nbsp;&nbsp;&nbsp;
             <asp:HyperLink ID="lnkSummary" Visible="false" runat="server" NavigateUrl="./PoSummary.aspx" Target="_blank" Text="PO Summary"></asp:HyperLink>&nbsp;
            
            </td></tr>
            <tr><td style="width: 1032px">
                    <table cellSpacing="1" cellPadding="1" width="80%"> 
                        <tr><td>
			                <asp:Panel ID="pnlSummary" runat="server" Width="100%" Visible="False" BackColor="#dee7f6">
			                    <table width="100%" border="1">
			                        <tr align="left">
			                            <td align="right" class="copy10grey" width="30%" >Total Purchase Orders:</td>
                                        <td width="70%"><asp:Label ID="lblTotalPOs"  CssClass="copy10grey" runat="server"></asp:Label></td>
			                        </tr>
			                        <tr>
			                            <td align="right" class="copy10grey" >Pending Purchase Orders:</td>
                                        <td><asp:Label ID="lblPendingPOs" CssClass="copy10grey"  runat="server"></asp:Label></td>
			                        </tr>
			                        <tr>
			                            <td align="right" class="copy10grey" >Processed Purchase Orders:</td>
                                        <td><asp:Label ID="lblProcessedPOs" CssClass="copy10grey"  runat="server"></asp:Label></td>
			                        </tr>
			                        <tr>
			                            <td align="right" class="copy10grey" style="height: 2px" >Shipped Purchase Orders:</td>
                                        <td style="height: 2px"><asp:Label ID="lblShippedPOs" CssClass="copy10grey"  runat="server"></asp:Label></td>
			                        </tr>			        			        
			                    </table>
			                </asp:Panel>
			            </td></tr>
			            <tr>
			                <td>
			                    <asp:Panel ID="pnlItem" runat="server">
			                    
			                    </asp:Panel>
			                </td>
			            </tr>
			        </table>
            </td></tr>
            <tr><td>
            
           <asp:GridView ID="GridView1" OnPageIndexChanging="gridView_PageIndexChanging"    AutoGenerateColumns="false"  
                DataKeyNames="PurchaseOrderID"  Width="100%"  
           ShowFooter="false" runat="server" GridLines="Both" OnRowDataBound="GridView1_RowDataBound" PageSize="50" AllowPaging="true"
            OnRowCommand = "GridView1_RowCommand" OnRowUpdating = "GridView1_RowUpdating" BorderStyle="Outset"
            OnRowDeleting = "GridView1_RowDeleting" OnRowDeleted = "GridView1_RowDeleted" OnRowEditing="GridView1_RowEditing"
            OnRowUpdated = "GridView1_RowUpdated" AllowSorting="false" OnRowCancelingEdit="GridView1_RowCancelingEdit"> 
            <RowStyle BackColor="Gainsboro" />
            <AlternatingRowStyle BackColor="white" />
            <HeaderStyle  CssClass="button" ForeColor="white"/>
            <FooterStyle CssClass="white"  />
            <Columns>
            <asp:ButtonField   ButtonType="Image"  ImageUrl ="../images/plus.gif"
                CommandName="sel"  />
            <asp:TemplateField ItemStyle-CssClass="copy10grey"  ItemStyle-Width="3%">
                <ItemTemplate>
                    <asp:CheckBox ID="chk"  runat="server" />
                </ItemTemplate>
            </asp:TemplateField>                 
                
                <asp:TemplateField HeaderText="PO#" SortExpression="PurchaseOrderNumber" ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                    <ItemTemplate>
                        <asp:Label ID="lblPoNum" runat="server" Text='<%# Eval("PurchaseOrderNumber") %>'></asp:Label>  
<asp:HiddenField ID="hdnCANo" runat="server" Value='<%# Eval("CustomerAccountNumber") %>' />                                                  
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="PO Date"  SortExpression="PurchaseOrderDate" ItemStyle-CssClass="copy10grey"  ItemStyle-Width="5%">
                    <ItemTemplate><%# DataBinder.Eval(Container.DataItem, "PurchaseOrderDate", "{0:d}") %></ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Customer" SortExpression="CustomerName"  ItemStyle-HorizontalAlign="left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                    <ItemTemplate>
                    <%# Eval("CustomerName")%>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Contact Name" SortExpression="ContactName" ItemStyle-CssClass="copy10grey" ItemStyle-Width="15%">
                    <ItemTemplate><%# Eval("Shipping.ContactName")%></ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtContactName" CssClass="copy10grey"  MaxLength="50"  Text='<%# Eval("Shipping.ContactName") %>' runat="server"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Contact Phone" SortExpression="ContactPhone" ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                    <ItemTemplate><%# Eval("Shipping.ContactPhone") %></ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtShipAttn" CssClass="copy10grey"  MaxLength="50" Text='<%# Eval("Shipping.ShipToAttn") %>' runat="server"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Ship Via" SortExpression="ShipToBy" ItemStyle-CssClass="copy10grey"  ItemStyle-Width="7%">
                    <ItemTemplate><%# Eval("Tracking.ShipToBy")%></ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtVia" CssClass="copy10grey"  MaxLength="200" TextMode="MultiLine" Text='<%# Eval("Tracking.ShipToBy") %>' runat="server"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
               
                 <asp:TemplateField HeaderText="AVSO#" SortExpression="AerovoiceSalesOrderNumber"  ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="copy10grey" ItemStyle-Width="7%">
                    <ItemTemplate>
                    <%# Eval("AerovoiceSalesOrderNumber") %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Tracking#" SortExpression="ShipToTrackingNumber" ItemStyle-CssClass="copy10grey"  ItemStyle-Width="7%">
                    <ItemTemplate><%# Eval("Tracking.ShipToTrackingNumber")%></ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtTrack" CssClass="copy10grey"  MaxLength="200" TextMode="MultiLine" Text='<%# Eval("Tracking.ShipToTrackingNumber") %>' runat="server"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>   
               
               
                <asp:TemplateField HeaderText="Shipping Date" SortExpression="ShipToDate"  ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                    <ItemTemplate>
<asp:Label ID="lblShippDate" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "Tracking.ShipToDate", "{0:d}"))=="1/1/0001"? "": DataBinder.Eval(Container.DataItem, "Tracking.ShipToDate", "{0:d}")%>'></asp:Label>                        
                    
</ItemTemplate>
                </asp:TemplateField>                                  
                
               
                
                <asp:TemplateField HeaderText="Store ID" SortExpression="StoreID"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                    <ItemTemplate><%#Eval("StoreID")%></ItemTemplate>
                </asp:TemplateField> 

                <asp:TemplateField HeaderText="Street Address" SortExpression="ShipTo_Address"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="15%">
                    <ItemTemplate><%#Eval("Shipping.ShipToAddress")%> <%#Eval("Shipping.ShipToAddress2")%></ItemTemplate>
                </asp:TemplateField> 
                <asp:TemplateField HeaderText="State"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                    <ItemTemplate><%#Eval("Shipping.ShipToState")%></ItemTemplate>
                </asp:TemplateField>                                              

                <asp:TemplateField HeaderText="Zip"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                    <ItemTemplate><%#Eval("Shipping.ShipToZip")%></ItemTemplate>
                </asp:TemplateField>  

                <asp:TemplateField HeaderText="Status"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                    <ItemTemplate><%#Eval("PurchaseOrderStatus")%></ItemTemplate>
                </asp:TemplateField>   
                
                <asp:TemplateField HeaderText="Sent ESN" SortExpression="sentesn"  ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                    <ItemTemplate><%#Eval("sentesn")%></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Sent ASN" SortExpression="sentesn"  ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                    <ItemTemplate><%#Eval("sentasn ")%></ItemTemplate>
                </asp:TemplateField>	    
			    <asp:CommandField HeaderText="Edit" ShowEditButton="True" HeaderStyle-CssClass="button" ControlStyle-CssClass="linkgrid"  ItemStyle-HorizontalAlign="Center"/>
			    <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center" >
                    <ItemTemplate>
                        <asp:LinkButton ID="linkDeleteCust" CssClass="linkgrid" CommandName="Delete" runat="server">Delete</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField  >
			        <ItemTemplate>
			            <tr>
                            <td colspan="100%">
                                <div id="div<%# Eval("PurchaseOrderID") %>"  >
                                    <asp:GridView ID="GridView2"  BackColor="White" Width="100%" Visible="False"
                                        AutoGenerateColumns="false" Font-Names="Verdana" runat="server" DataKeyNames="PodID"
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
                                                <ItemTemplate><asp:Label ID="lblItemCode" runat="server" Text='<%# Eval("ItemCode")%>'></asp:Label></ItemTemplate>
                                            </asp:TemplateField>
                                                                                                                                    
                                            
                                            <asp:TemplateField HeaderText="Qty" SortExpression="Qty"  ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                                                <ItemTemplate><%#Eval("Quantity")%></ItemTemplate>
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField HeaderText="ESN" SortExpression="ESN" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEsn" CssClass="copy10grey" Text='<%# Eval("ESN") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtEsn" CssClass="copy10grey" MaxLength="35"  Text='<%# Eval("ESN") %>' runat="server"></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField HeaderText="MDN" SortExpression="MdnNumber"  ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                                <ItemTemplate><%#Eval("MdnNumber")%></ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtMdn" CssClass="copy10grey" MaxLength="30"  Text='<%# Eval("MdnNumber") %>' runat="server"></asp:TextBox>
                                                </EditItemTemplate>                                                
                                            </asp:TemplateField>
                                        
                                            <asp:TemplateField HeaderText="MSID" SortExpression="MsID"  ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                                <ItemTemplate><%#Eval("MsID")%></ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtMsid" CssClass="copy10grey" MaxLength="30"  Text='<%# Eval("MsID") %>' runat="server"></asp:TextBox>
                                                </EditItemTemplate>                                                 
                                            </asp:TemplateField>
                                            

                                            <asp:TemplateField HeaderText="MslNumber" SortExpression="MslNumber" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                    <ItemTemplate><%#Eval("MslNumber")%></ItemTemplate>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtMslNumber" CssClass="copy10grey" MaxLength="30"  Text='<%# Eval("MslNumber") %>' runat="server"></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField HeaderText="Passcode" SortExpression="PassCode"  ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                                <ItemTemplate><%#Eval("PassCode")%></ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="UPC" SortExpression="UPC"  ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                                <ItemTemplate><%#Eval("UPC")%></ItemTemplate>
                                            </asp:TemplateField>
                                            
                                             <asp:TemplateField HeaderText="FM-UPC" SortExpression="FMUPC" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                    <ItemTemplate><%#Eval("FMUPC")%></ItemTemplate>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtFMUPC" CssClass="copy10grey" MaxLength="50"  Text='<%# Eval("FMUPC") %>' runat="server"></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                                                                                                                        
                                            <%--<asp:TemplateField HeaderText="Phone Type" Visible="false" SortExpression="PhoneCategory"  ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                                <ItemTemplate><%#Eval("PhoneCategory")%></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField  ItemStyle-Width="10%">  
                                            </asp:TemplateField>  --%>                                                        
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
             <tr><td style="width: 1032px"><br/></td></tr>
              <tr><td style="width: 1032px"><br/></td></tr>
        </table>
        
    </div>
    </TD>
				</TR>
				<tr>
					<td>
						&nbsp;
					</td>
				</tr>
				<TR>
					<TD>
						<foot:MenuFooter id="Menuheader2" runat="server"></foot:MenuFooter></TD>
				</TR>
			</TABLE>
			

    </form>
</body>
</html>
