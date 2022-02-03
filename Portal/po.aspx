<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="po.aspx.cs" Inherits="avii.po" %>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="/Controls/Header.ascx" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="/Controls/Footer.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
        <title>Fulfillment</title>
		<link href="./aerostyle.css" rel="stylesheet" type="text/css"/>
		<script language="javascript" type="text/javascript" src="./avI.js"></script> 
        <style type="text/css">
            .style1
            {
                width: 235px;
            }
            .style2
            {
                FONT-SIZE: 10px;
                COLOR: #000000;
                FONT-FAMILY: Verdana, Arial, Helvetica, sans-serif;
                width: 235px;
            }
        </style>
        <script type="text/javascript">
            function DisplayStoreName(obj) {
                //document   ("storeName").style.display = "block";    
                //var value = obj.value;
                //var arr = value.split('!');
                //alert(arr.length);
                //if (arr.length > 1) {
                //    var storeObj = document   (" =lblStoreName.ClientID %>");
                //    storeObj.innerHTML = arr[1];

               // }
                //alert(value);
            }        
        </script>
</head>
<body bgColor="#ffffff" leftmargin="0" rightmargin="0" topmargin="0">
    <form id="form1" runat="server">
        <div align="center">
			<table cellSpacing="0" cellPadding="0"  border="0" align="center" width="95%">
				<tr>
					<td>
					<head:menuheader id="HeadAdmin" runat="server"></head:menuheader>
				    
					</td>
				</tr>
            <tr>
                <td>
                <asp:ScriptManager ID="ScriptManager1" runat="server" >
                </asp:ScriptManager>
                    
                    <br />
                    <asp:UpdatePanel ID="UpdatePanel1"  runat="server" ChildrenAsTriggers="true" >
        
                    <ContentTemplate>
                    
                    <asp:ValidationSummary id="ValSummary" CssClass="copy10grey" HeaderText="The following errors were found:" ShowSummary="True" EnableClientScript="true" Enabled="true" ShowMessageBox="true" DisplayMode="List" Runat="server"/>
                    <table width="100%">
                        <tr>
			                <td class="button">&nbsp;&nbsp;Fulfillment</td>
                        </tr>
                        <tr><td>
                            <asp:HyperLink ID="lnkf" CssClass="copy10grey"  runat="server" Visible="false"  Text="Return to Forecast"></asp:HyperLink>
                        <asp:Label ID="lblMsg" runat="server" CssClass="errormessage"></asp:Label></td></tr>
                    </table>
                    <table cellSpacing="0" cellPadding="0" width="100%" >
                    <tr valign="top">
                        <td width="50%">
                            <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="100%" >
                   <tr bordercolor="#839abf">
                    <td>
                        <table width="100%" bgColor="#ffffff" leftmargin="0" rightmargin="0" topmargin="0">
                            <tr height="8">
                                <td>
                               
                                </td>
                            </tr>
                            <tr>
                                <td align="left" class="copy10grey">Order#:</td>
                                <td><FONT color="#ff0000">*</FONT></td>
                                <td >
                                    <asp:TextBox ID="txtPoNum" MaxLength="15" runat="server" CssClass="copy10grey"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="reqPoNum" CssClass="copy10grey"
                                          ErrorMessage="Order Number is required"
                                          ControlToValidate="txtPoNum"
                                          EnableClientScript="false" 
                                          Display="None"
                                          runat="server">*</asp:RequiredFieldValidator>
                                    </td>
                                <td align="right" class="copy10grey">Order Date:</td>
                                <td><FONT color="#ff0000">*</FONT></td>
                                <td>
                                    <asp:TextBox ID="txtPoDate" runat="server" MaxLength="10" CssClass="copy10grey" Enabled="False"></asp:TextBox>
                                     <asp:RequiredFieldValidator ID="reqPoDate" CssClass="copy10grey"
                                          ErrorMessage="Ordre Date is required"
                                           ControlToValidate="txtPoDate"
                                          EnableClientScript="false"
                                          Display="None"
                                          runat="server">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr  height="8">
                                <td  >
                                
                                </td>
                            </tr>
                            </table>
                            </td>
                            </tr>
                            </table>
                            <br />
                            <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="100%" >
                           
                            <tr bordercolor="#839abf">
                            <td>
                            <table cellSpacing="0" cellPadding="0" width="100%">
                             <tr  height="8">
                             <td>
                                
                             </td>
                             </tr>
                             <tr>
                                <td>
                                    <asp:Panel ID="pnlCustomer" runat="server">
                                    <table width="100%" bgColor="#ffffff" leftmargin="0" rightmargin="0" topmargin="0">
                            
                                    
                                            <tr>
                                                <td class="copy10grey" width="19%">

                                                Company Name:
                                                </td>
                                                <td width="0"><FONT color="#ff0000">*</FONT>&nbsp;</td>
                                        
                                        
                                                <td width="81%" align="left">
                                                <asp:DropDownList ID="ddlCustomer" AutoPostBack="true"  CssClass="copy10grey" 
                                                    runat="server" onselectedindexchanged="ddlCustomer_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                </td>
                                            </tr>
                                    
                                    </table>
                                    </asp:Panel>
                                    <asp:Panel ID="pnlStore" runat="server">
                                    <table width="100%" border="0" bgColor="#ffffff" leftmargin="0" rightmargin="0" topmargin="0">
                                    <tr>
                                    <td class="copy10grey" width="19%">
                                    Store ID:
                                    </td>
                                    <td width="0"><FONT color="#ff0000">*</FONT>&nbsp;</td>
                                    <td width="81%" align="left">
                                        <asp:DropDownList CssClass="copy10grey"  AutoPostBack="true" 
                                         ID="ddlStoreID" runat="server" OnSelectedIndexChanged="ddlStoreID_SelectedIndexChanged"  >
                                        </asp:DropDownList>
                                 
                                         <asp:RequiredFieldValidator  
                                    EnableClientScript="false" Display="None" 
                                     ID="reqStore" ControlToValidate="ddlStoreID" 
                                     InitialValue="" runat="server"  ErrorMessage="Store is required"></asp:RequiredFieldValidator>
           
                                    </td>
                                    <%--<td class="copy10grey" width="15%">
                                     <span id="storeName">
                                     Store Name:</span>
                                    </td>
                                    <td width="29%">
                                        <asp:Label ID="lblStoreName" CssClass="copy10grey" runat="server" ></asp:Label>
                                    </td>--%>
                                    </tr>
                            
                                </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr  height="8">
                                <td>
                                
                                </td>
                            </tr>
                            </table>
                            
                        


                            </td>
                            </tr>
                            
                            </table>
                            <br />
                            <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="100%" >
                                <tr bordercolor="#839abf">
                    <td>
                        <table border="0" width="100%" bgColor="#ffffff" leftmargin="0" rightmargin="0" topmargin="0">
                            <tr  height="8">
                                <td  >
                                
                                </td>
                            </tr>
                            <tr>
                                <td align="left" class="copy10grey" width="19%">Ship Via:</td>
                                <td width="0"><FONT color="#ff0000">*</FONT>&nbsp;</td>
                                <td align="left" width="81%">
                                    <asp:DropDownList ID="dpShipBy" runat="server" CssClass="copy10grey">
                                        <asp:ListItem Value="FDGE">FedEx General</asp:ListItem>
                                        <asp:ListItem Value="FED 1D PM">FedEx One day</asp:ListItem>
                                        <asp:ListItem Value="FED 2D PM">FedEx 2 days</asp:ListItem>
<asp:ListItem Value="FedEx Saturday Deliver">FedEx Saturday Deliver</asp:ListItem>
                                        <asp:ListItem Value="FedEx Saver 3 day">FedEx Saver 3 day</asp:ListItem>
                                        <asp:ListItem Value="UPS Ground">UPS Ground</asp:ListItem>
                                         <asp:ListItem Value="UPS Blue">UPS Blue</asp:ListItem>
                                        <asp:ListItem Value="UPS Red">UPS Red</asp:ListItem>
                                        
                                    </asp:DropDownList>
                                    
                                </td>
                                
                            </tr>
                            <tr  height="8">
                                <td>
                                
                                </td>
                            </tr>
                        </table>   
                         
                     </td>
                 </tr>
                            </table>  
                        </td>
                        <td width="3">
                        &nbsp;
                        </td>
                        <td width="49%">
                            <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="100%" >
                           <tr bordercolor="#839abf">
                            <td>
                                <table width="100%" border="0" bgColor="#ffffff" leftmargin="0" rightmargin="0" topmargin="0" cellpadding="3" cellspacing="3">
                            <tr  height="10">
                                <td>
                                
                                </td>
                            </tr>
                            <tr>
                                <td class="copy10grey" align="left">Contact Name:</td>
                                <td><FONT color="#ff0000">*</FONT></td>
                                <td colspan="4">
                                    <asp:TextBox ID="txtContactName" runat="server" MaxLength="50" Width="80%" CssClass="copy10grey"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="reqName" CssClass="copy10grey"
                                          ErrorMessage="Contact Name is required"
                                          ControlToValidate="txtContactName"
                                          EnableClientScript="false"
                                          Display="None"
                                          runat="server">*</asp:RequiredFieldValidator>
                                </td>
                                <%--<td class="copy10grey" align="right">Customer Number:</td>
                                <td><FONT color="#ff0000"></FONT></td>
                                <td class="copy10grey">
                                    <asp:TextBox ID="txtCustNumber" runat="server" MaxLength="30" 
                                        CssClass="copy10grey"></asp:TextBox>                                
                                </td>--%> 

                            </tr>
                            
                            <tr valign="top">
                                <td align="left" class="copy10grey">Address:</td>
                                <td><FONT color="#ff0000">*</FONT></td>
                                <td class="copy10grey" colspan="4">
                                    <asp:TextBox ID="txtAddress1" runat="server" MaxLength="50" CssClass="copy10grey" Width="80%"></asp:TextBox>
                                    
                                     <asp:RequiredFieldValidator ID="reqAddress" CssClass="copy10grey"
                                          ErrorMessage="Address is required"
                                          ControlToValidate="txtAddress1"
                                          EnableClientScript="false"
                                          Display="None"
                                          runat="server">*</asp:RequiredFieldValidator>
                                </td>                        
                            </tr>
                            <tr valign="top">
                                <td align="left" class="copy10grey"></td>
                                <td></td>
                                <td class="copy10grey" colspan="4">
                                    <asp:TextBox ID="txtAddress2" runat="server" MaxLength="50"  CssClass="copy10grey" Width="80%"></asp:TextBox>
                                    
                                </td>                        
                            </tr>
                            <tr>
                             <td class="copy10grey" align="left">Customer Phone:</td>
                                <td><FONT color="#ff0000"></FONT></td>
                                <td  >
                                    <asp:TextBox ID="txtPhone" runat="server" MaxLength="12" Width="99%" CssClass="copy10grey"></asp:TextBox>                                
                                </td> 
                                     <td align="left" class="copy10grey">City:</td>
                                <td><FONT color="#ff0000">*</FONT></td>
                                <td class="style2">
                                    <asp:TextBox ID="txtCity" runat="server" MaxLength="30" CssClass="copy10grey"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="reqCity" CssClass="copy10grey"
                                          ErrorMessage="City is required"
                                          ControlToValidate="txtCity"
                                          EnableClientScript="false"
                                          Display="None"
                                          runat="server">*</asp:RequiredFieldValidator>
                                    </td>                        
                            </tr>
                            <tr>
                                
                                <td align="left" class="copy10grey">State:</td> 
                                <td><FONT color="#ff0000">*</FONT></td>                       
                                <td class="copy10grey">
                                    <asp:dropdownlist id="dpState" tabIndex="6" runat="server" cssclass="txfield1">
																	<%--<asp:ListItem></asp:ListItem>
																	<asp:ListItem Value="AL">AL</asp:ListItem>
																	<asp:ListItem Value="AK">AK</asp:ListItem>
																	<asp:ListItem Value="AZ">AZ</asp:ListItem>
																	<asp:ListItem Value="AR">AR</asp:ListItem>
																	<asp:ListItem Value="CA" Selected="True">CA</asp:ListItem>
																	<asp:ListItem Value="CO">CO</asp:ListItem>
																	<asp:ListItem Value="CT">CT</asp:ListItem>
																	<asp:ListItem Value="DC">DC</asp:ListItem>
																	<asp:ListItem Value="DE">DE</asp:ListItem>
																	<asp:ListItem Value="FL">FL</asp:ListItem>
																	<asp:ListItem Value="GA">GA</asp:ListItem>
																	<asp:ListItem Value="HI">HI</asp:ListItem>
																	<asp:ListItem Value="ID">ID</asp:ListItem>
																	<asp:ListItem Value="IL">IL</asp:ListItem>
																	<asp:ListItem Value="IN">IN</asp:ListItem>
																	<asp:ListItem Value="IA">IA</asp:ListItem>
																	<asp:ListItem Value="KS">KS</asp:ListItem>
																	<asp:ListItem Value="KY">KY</asp:ListItem>
																	<asp:ListItem Value="LA">LA</asp:ListItem>
																	<asp:ListItem Value="ME">ME</asp:ListItem>
																	<asp:ListItem Value="MD">MD</asp:ListItem>
																	<asp:ListItem Value="MA">MA</asp:ListItem>
																	<asp:ListItem Value="MI">MI</asp:ListItem>
																	<asp:ListItem Value="MN">MN</asp:ListItem>
																	<asp:ListItem Value="MS">MS</asp:ListItem>
																	<asp:ListItem Value="MO">MO</asp:ListItem>
																	<asp:ListItem Value="MT">MT</asp:ListItem>
																	<asp:ListItem Value="NE">NE</asp:ListItem>
																	<asp:ListItem Value="NV">NV</asp:ListItem>
																	<asp:ListItem Value="NH">NH</asp:ListItem>
																	<asp:ListItem Value="NJ">NJ</asp:ListItem>
																	<asp:ListItem Value="NM">NM</asp:ListItem>
																	<asp:ListItem Value="NY">NY</asp:ListItem>
																	<asp:ListItem Value="NC">NC</asp:ListItem>
																	<asp:ListItem Value="ND">ND</asp:ListItem>
																	<asp:ListItem Value="OH">OH</asp:ListItem>
																	<asp:ListItem Value="OK">OK</asp:ListItem>
																	<asp:ListItem Value="OR">OR</asp:ListItem>
																	<asp:ListItem Value="PA">PA</asp:ListItem>
																	<asp:ListItem Value="RI">RI</asp:ListItem>
																	<asp:ListItem Value="SC">SC</asp:ListItem>
																	<asp:ListItem Value="SD">SD</asp:ListItem>
																	<asp:ListItem Value="TN">TN</asp:ListItem>
																	<asp:ListItem Value="TX">TX</asp:ListItem>
																	<asp:ListItem Value="UT">UT</asp:ListItem>
																	<asp:ListItem Value="VT">VT</asp:ListItem>
																	<asp:ListItem Value="VA">VA</asp:ListItem>
																	<asp:ListItem Value="WA">WA</asp:ListItem>
																	<asp:ListItem Value="WV">WV</asp:ListItem>
																	<asp:ListItem Value="WI">WI</asp:ListItem>
																	<asp:ListItem Value="WY">WY</asp:ListItem>--%>
																</asp:dropdownlist>
                                    </td>
                                <td align="left" class="copy10grey">Zip:</td>                        
                                <td><FONT color="#ff0000">*</FONT></td>
                                <td class="copy10grey">
                                    <asp:TextBox ID="txtZip" MaxLength="6" runat="server" CssClass="copy10grey"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="copy10grey"
                                          ErrorMessage="Zip is required"
                                          ControlToValidate="txtZip"
                                          EnableClientScript="false"
                                          Display="None"
                                          runat="server">*</asp:RequiredFieldValidator>
                                </td>
                                    
                            </tr>
                            <tr  height="10">
                                <td>
                                
                                </td>
                            </tr>
                            </table>
                            </td>
                            </tr>
                            </table>
                        </td>
                    </tr>
                    </table>
                    <br />   
                   <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="100%" >
                   <tr bordercolor="#839abf">
                    <td>
                        <table width="100%" bgColor="#ffffff" leftmargin="0" rightmargin="0" topmargin="0">
                            <tr  height="5">
                                <td  >
                                
                                </td>
                            </tr>
                            <tr >
                                <td class="copy10grey">
                                    Comments:
                                    <br />
                                    <asp:TextBox ID="txtCommments" CssClass="copy10grey" Width="95%" TextMode="MultiLine" Height="40"  runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr  height="8">
                                <td  >
                                
                                </td>
                            </tr>
                        </table>
                        </td>
                    </tr>
                    </table>
                            
                            
                        
                            
                            


                   
                 <br />   
             <%--   </td>
            </tr>
            <tr>
                <td>--%>
                    <table width="100%">
                    
                    <tr>
                            <td>

                    
                                <asp:datagrid id="dgPoItem" Width="100%" AutoGenerateColumns="False" AllowPaging="false" Runat="server"
										ShowFooter="false" OnItemCreated="dg_ItemCreated" OnCancelCommand="dg_Cancel" OnUpdateCommand="dg_Update"
										OnEditCommand="dg_Edit" OnItemCommand="dg_ItemCommand" OnItemDataBound="dg_ItemBound">
										<HeaderStyle CssClass="button"></HeaderStyle>
										<Columns>
											<asp:EditCommandColumn ItemStyle-Width="10%" ButtonType="LinkButton" UpdateText="Update" HeaderText="Select" CancelText="Cancel"
												EditText="Edit" ItemStyle-CssClass="copy11" CausesValidation="false"></asp:EditCommandColumn>
											<asp:ButtonColumn  ItemStyle-Width="10%"  CausesValidation="false" CommandName="delete" Text="Delete" ItemStyle-CssClass="copy11" HeaderText="Delete"></asp:ButtonColumn>
											<asp:TemplateColumn  ItemStyle-Width="40%" HeaderText="Product code"  ItemStyle-CssClass="copy10grey">
											    <ItemTemplate><%#Eval("ItemCode")%></ItemTemplate>
												<EditItemTemplate>
												    <asp:DropDownList ID="dpItem" CssClass="copy10grey" runat="server"></asp:DropDownList>
													
												</EditItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Quantity" ItemStyle-Width="40%"   ItemStyle-CssClass="copy10grey">
												<ItemTemplate><%#Eval("Quantity")%></ItemTemplate>
												
												<EditItemTemplate>
													<asp:TextBox ID="txtQty" runat="server" CssClass="copy10grey" Text="1"></asp:TextBox>
												</EditItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Category" ItemStyle-Width="40%"   ItemStyle-CssClass="copy10grey">
												<ItemTemplate><%#Eval("PhoneCategory")%></ItemTemplate>
												<EditItemTemplate>
													 <asp:DropDownList ID="dpPhoneCategory" runat="server"  class="copy10grey" >
                                                                <asp:ListItem Text="" Value=""></asp:ListItem>
                                                                <asp:ListItem Text="Hot" Value="H"></asp:ListItem>
                                                                <asp:ListItem Text="Cold" Value="C" Selected="True"></asp:ListItem>
                                                    </asp:DropDownList>
												</EditItemTemplate>
											</asp:TemplateColumn>			
										</Columns>
										<PagerStyle NextPageText="Next Page" PrevPageText="Previous Page&amp;nbsp;&amp;nbsp;" CssClass="text7"></PagerStyle>
									</asp:datagrid>
                            
                            </td>
                        </tr>
                    </table>
                    <br />
                    <table width="100%">
                    <tr>
                        <td align="center">
                            <asp:Button ID="btnSave" runat="server" Text="Submit" CssClass="button" OnClick="btnSave_Click" />&nbsp;&nbsp;
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="button" CausesValidation="false" OnClick="btnCancel_Click" />
                        </td>
                    </tr>
                    </table>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnSave" />
                        <asp:PostBackTrigger ControlID="btnCancel" />
                        
                    </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
            
            <tr><td><asp:UpdateProgress ID="UpdateProgress1" runat="server" 
                     DynamicLayout="true">
                        <ProgressTemplate>
                            <img src="/Images/ajax-loaders.gif" /> Loading ...
                        </ProgressTemplate>
                    </asp:UpdateProgress></td></tr>
        	<tr>
				<td>
					<foot:MenuFooter id="Menuheader2" runat="server"></foot:MenuFooter>
				</td>
			</tr>
        </table>
        </div>
        <script type="text/javascript">
            //var objStore = document.getElementById("storeName");
            //if (objStore != null)
            //    objStore.style.display = "none";

        </script>
    </form>
</body>
</html>
