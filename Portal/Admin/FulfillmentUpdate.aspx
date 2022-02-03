<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FulfillmentUpdate.aspx.cs" Inherits="avii.Admin.FulfillmentUpdate" %>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="/Controls/Header.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>.:: Lan Global inc. ::.</title>
    <link href="../aerostyle.css" rel="stylesheet" type="text/css"/>
<script type="text/javascript">
    function Validate(flag) {
        if (flag == '1' || flag == '2') {
            var company = document.getElementById("<% =dpCompany.ClientID %>");
            if (company.selectedIndex == 0) {
                alert('Customer is required!');
                return false;

            }
        }
        if (flag == '2') {
            var status = document.getElementById("<% =ddlStatus.ClientID %>");
            if (status.selectedIndex == 0) {
                alert('Status is required!');
                return false;

            }
        }



    }
    </script>

</head>
<body>
    <form id="form1" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <table cellSpacing="0" cellPadding="0" align="center" width="100%" border="0">
        	<tr>
				<td><head:MenuHeader id="HeadAdmin" runat="server"></head:MenuHeader></td>
			</tr>
			<tr><td>&nbsp;</td></tr>
			<tr>
				<td  bgcolor="#dee7f6" class="button">&nbsp;&nbsp;Fulfillment Orders Upload
				</td>
			</tr>
            <tr><td>&nbsp;</td></tr>
            </table>

    <asp:UpdatePanel ID="UpdatePanel1"  runat="server" UpdateMode="Conditional" >
            <ContentTemplate>
    
            <table cellSpacing="0" cellPadding="0" align="center" width="100%" border="0">
    		<tr>                    
                <td colspan="2">
                    <asp:Label ID="lblMsg" runat="server" Width="100%" CssClass="errormessage"></asp:Label></td>
            </tr>               
            
            <tr>
                <td align="center">

                    <table  cellSpacing="1" cellPadding="1" width="100%">
                        <tr><td class="copy10grey" align="left">
                        
	                    - Upload file should be less than 2 MB. <br />&nbsp;
                        </td></tr>
                    </table>

                 <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="90%">
                    <tr>
                    <td>
			            <table id="Table2" cellSpacing="5" cellPadding="5" width="100%" border="0">
                            <tr>
                                <td  class="copy10grey" align="right" width="42%" >
                                    Customer: &nbsp;</td>
                                <td align="left" >
                                    <asp:DropDownList ID="dpCompany" CssClass="copy10grey" runat="server"  AutoPostBack="false">
									</asp:DropDownList>
                            </tr>
                            <tr>
                                <td class="copy10grey"  width="42%" align="right">
                                    Select Status:
                                </td>
                                
                                <td  align="left">
                                <asp:DropDownList ID="ddlStatus" runat="server" class="copy10grey">
                                <asp:ListItem Text="" Value=""></asp:ListItem>
                                <asp:ListItem Text="Closed" Value="4"></asp:ListItem>
                                <asp:ListItem Text="Cancel" Value="9"></asp:ListItem>
                                </asp:DropDownList>
                                            

                                </td>
                            </tr>
                            
                            <tr>
                                <td  class="copy10grey" align="right" >
                                    Upload Fulfillment Order file: &nbsp;</td>
                                <td align="left" >
                                    <asp:FileUpload ID="flnUpload" runat="server" CssClass="txfield1" Width="50%" /></td>
                            </tr>
                            <tr id="trShipFile" >
                                <td class="copy10grey" align="right">
                                    File format sample: &nbsp;
                                         </td>
                                <td class="copy10grey" align="left">
                                   <b>FulfillmentOrder#</b>
                                </td>
                             </tr>

                             <tr valign="top">
                                <td class="copy10grey" align="right">
                                    Comment: &nbsp;
                                         </td>
                                <td class="copy10grey" align="left">
                                    <asp:TextBox ID="txtComment" runat="server" CssClass="copy10grey" Width="70%" Height="50px" TextMode="MultiLine"></asp:TextBox>

                                </td>
                             </tr>
 
                            <tr>
                                <td colspan="2">
                                    <asp:Panel ID="pnlSubmit" runat="server" Visible="false">
                                    <table cellpadding="0" cellspacing="0" width="100%">
                                        <tr><td>
                                        <hr style="width:100%" />
                            
                                        </td></tr>   
                                         <tr>                    
                                            <td align="left">
                                                <asp:Label ID="lblConfirm" runat="server" Width="100%" CssClass="errorGreenMsg"></asp:Label></td>
                                        </tr>               
                                    
                                         <tr>
                                            <td  align="center">
                                
                                
                                            <asp:Button ID="btnSubmit2" Visible="false" Width="190px"  CssClass="button" runat="server" OnClick="btnSubmit_Click" Text="Submit" OnClientClick="return Validate(2);" />

                                            &nbsp;<asp:Button ID="btnCancel2" runat="server"  CssClass="button" Text="Cancel" OnClick="btnCancel_Click" />
                                                </td>
                                        </tr>
                                        <tr><td>
                                        <hr style="width:100%" />
                            
                                        </td></tr>   
                                    </table>

                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr><td colspan="2">
                            
                            <table cellpadding="0" cellspacing="0" width="100%">
                             <tr>
                                <td colspan="2" align="right" style="height:8px; vertical-align:bottom">
                                <strong>   <asp:Label ID="lblCount" CssClass="copy10grey" runat="server" ></asp:Label> &nbsp;&nbsp;</strong> 
                                </td>
                             </tr>
                             <tr>
                                <td colspan="2" align="center">
                                
                                <asp:Repeater ID="rptPO" runat="server" Visible="true"  >
                                <HeaderTemplate>
                                <table border="0" width="98%" cellpadding="1" cellspacing="1">
                                <tr>
                                    <td class="button" >
                                        S.No.
                                    </td>
                                    <td class="button" >
                                        FulfillmentOrder#
                                    </td>
                                    
                                    <td class="button">
                                        Fulfillment Date
                                    </td>
                                    <td class="button">
                                         AVSO#
                                    </td>
                                    <td class="button">
                                        StoreID
                                    </td>
                                    <td class="button">
                                         Contact Name
                                    </td>
                                    <td class="button">
                                        Address
                                    </td>
                                    <td class="button">
                                         City
                                    </td>
                                    <td class="button">
                                        State
                                    </td>
                                    <td class="button">
                                         Zip
                                    </td>
                                    <td class="button">
                                         Ship Via
                                    </td>
                                    <td class="button">
                                         Status
                                    </td>


                                </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
    
                                    <tr valign="bottom"  class="<%# Container.ItemIndex % 2 == 0 ? "alternaterow" : "" %>">
                                        <td class="copy10grey"  >
                                        <%# Container.ItemIndex +  1 %>
                                        </td>
                                        <td align="left" valign="bottom" class="copy10grey"  style="background-color:<%# Convert.ToString(Eval("FulfillmentOrderNumber")) == "" ? "red" : ""%>">
                                        <span width="100%" >
                                            <%--<asp:LinkButton ID="lnkPoNum"  runat="server"><%# Eval("FulfillmentOrderNumber")%></asp:LinkButton>
                                             --%>   

                                            <%# Eval("FulfillmentOrderNumber") %>  
                                             
                                             <%--
                                             style="background-color:<%# Convert.ToString(Eval("SalesOrderNumber")) == "" ? "red" : ""%>"  --%>
                                            
                                            </span>
                                        </td>
                                        <td valign="bottom" class="copy10grey"  align="left">
                                        <span width="100%">
                                            <%# Convert.ToDateTime(Eval("FulfillmentOrderDate")).ToShortDateString() %>
                                            </span>
                                        </td>
                                        <td valign="bottom" class="copy10grey"  >
                                        <span width="100%">
                                            <%# Eval("AerovoiceSaleOrderNumber")%>
                                            </span>
                                        </td>
                                        <td valign="bottom" class="copy10grey"  align="left">
                                        <span width="100%">
                                            <%# Eval("StoreID")%>
                                            </span>
                                        </td>
                                        <td valign="bottom" class="copy10grey"  align="left">
                                        <span width="100%">
                                            <%# Eval("ContactName")%>
                                            </span>
                                        </td><td valign="bottom" class="copy10grey" align="left" >
                                        <span width="100%">
                                            <%# Eval("ShipAddress")%>
                                            </span>
                                        </td>
                                        <td valign="bottom" class="copy10grey"  align="left">
                                        <span width="100%">
                                            <%# Eval("ShipCity")%>
                                            </span>
                                        </td><td valign="bottom" class="copy10grey"  align="left">
                                        <span width="100%">
                                            <%# Eval("ShipState")%>
                                            </span>
                                        </td>
                                        <td valign="bottom" class="copy10grey"  align="left">
                                        <span width="100%">
                                            <%# Eval("ShipZip")%>
                                            </span>
                                        </td><td valign="bottom" class="copy10grey"  align="left">
                                        <span width="100%">
                                            <%# Eval("ShipVia")%>
                                            </span>
                                        </td>
                                        <td valign="bottom" class="copy10grey"  align="left">
                                        <span width="100%">
                                            <%# Eval("FulfillmentStatus")%>
                                            </span>
                                        </td>
                                        
                                    </tr>
    
    

    
                                </ItemTemplate>
                                <FooterTemplate>
                                </table>
                                </FooterTemplate>
                                </asp:Repeater>

                        </td>
                            </tr>
                            </table>
                            </td></tr>                            
                            
                            <tr><td colspan="2">
                            <hr style="width:100%" />
                            
                            </td></tr>                            
                             <tr>
                                <td colspan="2" align="center">
                                
                                <asp:Button ID="btnUpload"  Width="190px" CssClass="button" runat="server" OnClick="btnValidateUploadedFile_Click" Text="Validate Uploaded file" OnClientClick="return Validate(1);"  />

                                &nbsp;<asp:Button ID="btnSubmit" Visible="false" Width="190px"  CssClass="button" runat="server" OnClick="btnSubmit_Click" Text="Submit" OnClientClick="return Validate(2);"/>
                                &nbsp;<asp:Button ID="btnViewTracking" Visible="false" Width="190px"  CssClass="button" runat="server" OnClick="btnViewAssignedPos_Click" Text="View UPDATED fulfillment" />

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

            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnUpload" />
            </Triggers>
            </asp:UpdatePanel>
	</form>
</body>
</html>
