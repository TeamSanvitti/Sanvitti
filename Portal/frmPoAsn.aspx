<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmPoAsn.aspx.cs" Inherits="avii.frmPoAsn" %>
<%@ Register TagPrefix="head1" TagName="MenuHeader1" Src="/admin/admhead.ascx" %>


<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
	<link href="./aerostyle.css" rel="stylesheet" type="text/css"/>
		<script type="text/javascript" language="javascript" src="./avI.js"></script> 
    <title>Aerovoice Fulfillment Portal</title>
</head>
<body bgColor="#ffffff" leftmargin="0" rightmargin="0" topmargin="0">
    <form id="form1" runat="server">
    <TABLE cellSpacing="0" cellPadding="0"  border="0" align="center" width="100%">
				<TR>
					<TD>
					    <head1:MenuHeader1 id="HeadAdmin" runat="server"></head1:MenuHeader1>
					</TD>
				</TR>
    </TABLE>				
    <div>
        <table style="border-color:gray;" cellspacing="0" cellpadding="0" width="100%" align="center" border="0">
	    <tr style="border-color:white;" >
		    <td>
			    <table width="100%">
				<tr>
					<td class="button">&nbsp;Fulfillment Ad-hoc Service</td>
				</tr>
			    </table>
		    </td>
	    </tr>
        </table>    
		<table>
			 <tr><td><asp:Label ID="lblMsg" runat="server" CssClass="errormessage"></asp:Label></td></tr>
		</table>
		
		<table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="100%" >
           <tr bordercolor="#839abf">
                <td>
		<table  cellSpacing="0" cellPadding="0" width="100%" border="0">
			<tr>
				<td  widht="25%" align="Right" class="copy10grey">Purchase Order#:</td>
				<td  widht="25%"><asp:TextBox id="txtPO" runat="server" CssClass="copy10grey"></asp:TextBox></td>
				<td  widht="25%"align="Right" class="copy10grey">Customer:</td>
				<td  widht="25%">  
				    <asp:DropDownList ID="dpCompany" runat="server" class="copy10grey">
                    </asp:DropDownList>
                </td>	
			</tr>
			<tr><td colspan="4"><hr /></td></tr>
			<tr>
                <td  widht="25%"align="Right" class="copy10grey">Status:</td>
				<td  widht="25%">&nbsp;<b><asp:Label ID="lblStatus" runat="server" CssClass="copy10grey"></asp:Label></b></td>				
			</tr>
			 <tr>
				<td align="Right" class="copy10grey">ESN Status:</td>
				<td colspan="3">&nbsp;
					<b><asp:Label ID="lblEsnStatus" runat="server" CssClass="copy10grey"></asp:Label></b>
				</td>
			</tr>
			 <tr>
				<td align="Right" class="copy10grey">ASN Status:</td>
				<td colspan="3">&nbsp;<b>
					<asp:Label ID="lblAsnStatus" runat="server" CssClass="copy10grey"></asp:Label></b>
				</td>
			</tr>
			<tr><td colspan="4"><hr /></td></tr>
			 <tr>
				<td align="center" class="copy10grey" colspan="4">
					 <asp:Button ID="btnSearch" runat="server" CssClass="button" OnClick= "btnSearch_Click" Text="Search Purchase Order" />
						&nbsp;&nbsp;
					 <asp:Button ID="btnCancel" runat="server" CssClass="button" OnClick= "btnCancel_Click" Text="Cancel Search" />
						&nbsp;&nbsp;	
					 <asp:Button ID="btnSendESN" Visible="false" runat="server" CssClass="button" OnClick= "btnSendEsn_Click" Text="Send ESN" />
						&nbsp;&nbsp;
					 <asp:Button ID="btnSendASN"  Visible="false"  runat="server" CssClass="button" OnClick= "btnSendAsn_Click" Text="Send ASN" />
				</td>
			</tr>

		</table>
		</td>
		</tr>
		</table>
		
		<br />
		
		<asp:Panel ID="pnlPO" runat="server" Visible="false">
		<table width="100%" border="0" cellpadding="0" cellspacing="0">
		<tr>
		    <td>
		  <asp:GridView ID="grdPO"   AutoGenerateColumns="false"  
                DataKeyNames="PurchaseOrderID"  Width="100%" ShowFooter="false" runat="server" GridLines="Both"  PageSize="50" AllowPaging="false"
            AllowSorting="false" > 
            <RowStyle BackColor="Gainsboro" />
            <AlternatingRowStyle BackColor="white" />
            <HeaderStyle  CssClass="button" ForeColor="white"/>
            <FooterStyle CssClass="white"  />
            <Columns>
             <asp:ButtonField   ButtonType="Image"  ImageUrl ="../images/plus.gif"
                    CommandName="sel"  />              

                <asp:TemplateField HeaderText="Aerovoice Order#" SortExpression="AerovoiceSalesOrderNumber"  ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                    <ItemTemplate><%#Eval("AerovoiceSalesOrderNumber")%></ItemTemplate>
                </asp:TemplateField>   
                                
                <asp:TemplateField HeaderText="PO#" SortExpression="PurchaseOrderNumber" ItemStyle-CssClass="copy10grey" ItemStyle-Width="6%">
                    <ItemTemplate>
                        <asp:Label ID="lblPoNum" runat="server" Text='<%# Eval("PurchaseOrderNumber") %>'></asp:Label>                        
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="PO Date"  SortExpression="PurchaseOrderDate" ItemStyle-CssClass="copy10grey"  ItemStyle-Width="11%">
                    <ItemTemplate><%# DataBinder.Eval(Container.DataItem, "PurchaseOrderDate", "{0:d}") %></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Contact Name" SortExpression="ContactName" ItemStyle-CssClass="copy10grey">
                    <ItemTemplate><%# Eval("Shipping.ContactName")%></ItemTemplate>

                </asp:TemplateField>
                <asp:TemplateField HeaderText="Ship Attn" SortExpression="ShipToAttn" ItemStyle-CssClass="copy10grey">
                    <ItemTemplate><%# Eval("Shipping.ShipToAttn") %></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Shippping" SortExpression="ShipToBy" ItemStyle-CssClass="copy10grey">
                    <ItemTemplate><%# Eval("Tracking.ShipToBy")%></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Tracking#" SortExpression="ShipToTrackingNumber" ItemStyle-CssClass="copy10grey">
                    <ItemTemplate><%# Eval("Tracking.ShipToTrackingNumber")%></ItemTemplate>
                </asp:TemplateField>   
                                
                <asp:TemplateField HeaderText="Store ID" SortExpression="StoreID"  ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                    <ItemTemplate><%#Eval("StoreID")%></ItemTemplate>
                </asp:TemplateField> 

                <asp:TemplateField HeaderText="Street Address" SortExpression="ShipTo_Address"  ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                    <ItemTemplate><%#Eval("Shipping.ShipToAddress")%></ItemTemplate>
                </asp:TemplateField> 
                <asp:TemplateField HeaderText="State"  ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                    <ItemTemplate><%#Eval("Shipping.ShipToState")%></ItemTemplate>
                </asp:TemplateField>                                              
                <asp:TemplateField HeaderText="Zip Code"  ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                    <ItemTemplate><%#Eval("Shipping.ShipToZip")%></ItemTemplate>
                </asp:TemplateField>                                              


                <asp:TemplateField HeaderText="Status"  ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                    <ItemTemplate><%#Eval("PurchaseOrderStatus")%></ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="Sent ESN" SortExpression="sentesn"  ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                    <ItemTemplate><%#Eval("sentesn")%></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Sent ASN" SortExpression="sentesn"  ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                    <ItemTemplate><%#Eval("sentasn ")%></ItemTemplate>
                </asp:TemplateField>	                    
                <asp:TemplateField  >
			        <ItemTemplate>
			            <tr>
                            <td colspan="100%">
                                <div id="div<%# Eval("PurchaseOrderID") %>"  >
                                    <asp:GridView ID="GridView2"  BackColor="White" Width="100%" Visible="False"
                                        AutoGenerateColumns="false" Font-Names="Verdana" runat="server" DataKeyNames="PodID"
                                         GridLines="Both"
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
                                                    <asp:TextBox ID="txtEsn" CssClass="copy10grey" MaxLength="50"  Text='<%# Eval("ESN") %>' runat="server"></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField HeaderText="MDN" SortExpression="MdnNumber"  ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                                <ItemTemplate><%#Eval("MdnNumber")%></ItemTemplate>
                                            </asp:TemplateField>
                                        
                                            <asp:TemplateField HeaderText="MSID" SortExpression="MsID"  ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                                <ItemTemplate><%#Eval("MsID")%></ItemTemplate>
                                            </asp:TemplateField>
                                            

                                            <asp:TemplateField HeaderText="MslNumber" SortExpression="MslNumber" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMslNumber" CssClass="copy10grey" Text='<%# Eval("MslNumber") %>' runat="server"></asp:Label>
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
                                                    <asp:Label ID="lblFMUPC" CssClass="copy10grey" Text='<%# Eval("FMUPC") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtFMUPC" CssClass="copy10grey" MaxLength="50"  Text='<%# Eval("FMUPC") %>' runat="server"></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                                                                                                                        
                                            <asp:TemplateField HeaderText="Phone Type" SortExpression="PhoneCategory"  ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                                <ItemTemplate><%#Eval("PhoneCategory")%></ItemTemplate>
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
            
		    </td>
		</tr>
		<tr><td>&nbsp;</td></tr>
		<tr>
		    <td>
		        <table width="100%" border="0">
		            <tr>
		                <td class="Button" colspan="2">ESN & ASN Service Status:</td>
		            </tr>
		            <tr>
		                <td valign="top">
		                    <table runat="server" id="tblESN" border="0" width="100%">
		                    <tr>
        		                <td class="Button">ESN Sent:</td>                    
		                    </tr>
		                    <tr>
		                        <td>
		                         <asp:GridView ID="grdESN" runat="server" Width="100%" GridLines="Both" AllowPaging="false"  AutoGenerateColumns="false">
                                    <RowStyle BackColor="Gainsboro" />
                                    <AlternatingRowStyle BackColor="white" />
                                    <HeaderStyle  CssClass="button" ForeColor="white"/>
                                    <FooterStyle CssClass="white"  />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Send Date" SortExpression="SendDate"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                            <ItemTemplate><%#Eval("SendDate")%></ItemTemplate>
                                        </asp:TemplateField> 
                                        <asp:TemplateField HeaderText="ESN" SortExpression="ESN"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                            <ItemTemplate><%#Eval("ESN")%></ItemTemplate>
                                        </asp:TemplateField> 
                                        <asp:TemplateField HeaderText="MSLNumber" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                            <ItemTemplate><%#Eval("MSLNumber")%></ItemTemplate>
                                        </asp:TemplateField> 
                                        <asp:TemplateField HeaderText="ReturnCode" SortExpression="ReturnCode" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                            <ItemTemplate><%#Eval("ReturnCode")%></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ReturnMessage" SortExpression="ReturnMessage" ItemStyle-Width="60%"   ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey">
                                            <ItemTemplate><%#Eval("ReturnMessage")%></ItemTemplate>
                                        </asp:TemplateField> 
                                    </Columns>
                                </asp:GridView>		
		                        </td>
		                    </tr>
		                    </table>                        
		                </td>
		                <td  valign="top">
		                	<table runat="server" id="tblAsn" border="0"  width="100%">
		                    <tr>
        		                <td class="Button">ASN Sent:</td>                    
		                    </tr>
		                    <tr>
		                        <td>
		                            <asp:GridView ID="grdASN" runat="server" Width="100%" GridLines="Both" AllowPaging="false"  AutoGenerateColumns="false">
                                    <RowStyle BackColor="Gainsboro" />
                                    <AlternatingRowStyle BackColor="white" />
                                    <HeaderStyle  CssClass="button" ForeColor="white"/>
                                    <FooterStyle CssClass="white"  />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Send Date" SortExpression="SendDate"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                            <ItemTemplate><%#Eval("SendDate")%></ItemTemplate>
                                        </asp:TemplateField> 
                                        <asp:TemplateField HeaderText="ESN" SortExpression="ESN"   ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" >
                                            <ItemTemplate><%#Eval("TrackingNumber")%></ItemTemplate>
                                        </asp:TemplateField> 
                                        <asp:TemplateField HeaderText="ReturnCode" SortExpression="ReturnCode"  ItemStyle-Width="10%"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey">
                                            <ItemTemplate><%#Eval("ReturnCode")%></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ReturnMessage" SortExpression="ReturnMessage" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="70%">
                                            <ItemTemplate><%#Eval("ReturnMessage")%></ItemTemplate>
                                        </asp:TemplateField> 
                                    </Columns>
                                </asp:GridView>	
                            </td>
                            </tr>
                            </table>
		                </td>
		            </tr>
		        </table>
		    
		    </td>
		</tr>
		<tr><td>&nbsp;</td></tr>
		<tr><td>&nbsp;</td></tr>
		<tr>
		    <td>
		        <table width="100%" border="0" runat="server" id="tblErrorLog">
		            <tr>
		                <td class="Button">Error Log:</td>
		            </tr>
		            <tr>
		                <td  valign="top">
		                    <asp:GridView ID="grdErrorLog" runat="server" Width="100%" GridLines="Both" AllowPaging="false"  AutoGenerateColumns="false">
                            <RowStyle BackColor="Gainsboro" />
                            <AlternatingRowStyle BackColor="white" />
                            <HeaderStyle  CssClass="button" ForeColor="white"/>
                            <FooterStyle CssClass="white"  />
                            <Columns>
                                <asp:TemplateField HeaderText="Module Name" SortExpression="ModuleName"  ItemStyle-Width="20%"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey">
                                    <ItemTemplate><%#Eval("ModuleName")%></ItemTemplate>
                                </asp:TemplateField> 
                                <asp:TemplateField HeaderText="Log Date" SortExpression="LogDate"  ItemStyle-Width="10%"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" >
                                    <ItemTemplate><%# DataBinder.Eval(Container.DataItem, "LogDate", "{0:d}")%></ItemTemplate>
                                </asp:TemplateField> 
                                <asp:TemplateField HeaderText="Description" SortExpression="Description"  ItemStyle-Width="70%"   ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey">
                                    <ItemTemplate><%#Eval("Description")%></ItemTemplate>
                                </asp:TemplateField> 
                            </Columns>
                            </asp:GridView>
		                </td>
		            </tr>
		        </table>
		    
		    </td>
		</tr>
		</table>
		</asp:Panel>
    </div>
    </form>
</body>
</html>
