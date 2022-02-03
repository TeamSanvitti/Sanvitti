<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UploadPOData.aspx.cs" Inherits="avii.Admin.UploadPOData" ValidateRequest="false" %>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="/Controls/Header.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>.:: Lan Global inc. ::.</title>
    <link href="../aerostyle.css" rel="stylesheet" type="text/css"/>
    <script language="javascript" src="../avI.js" type="text/javascript"></script>
	
    <script language="javascript" type="text/javascript">
	function KeyDownHandler(btn)
    {

       if (event.keyCode == 13)
        {
            event.returnValue=false;
            event.cancel = true;
            btn.click();
        }
    }
			</script>
</head>
<body leftmargin="0" topmargin="0" marginwidth="0" marginheight="0" onkeydown="KeyDownHandler(document.all.btnUpload)">
    <form id="form1" runat="server" method="post" >
        <TABLE cellSpacing="0" cellPadding="0" align="center" width="100%" border="0">
        	<tr>
				<td><head:menuheader id="HeadAdmin" runat="server"></head:menuheader></td>
			</tr>
			<tr><td>&nbsp;</td></tr>
			<TR>
				<TD  bgcolor="#dee7f6" class="button">&nbsp;&nbsp;Form Uploads
				</TD>
			</TR>
            <tr><td>&nbsp;</td></tr>
			<tr>                    
                <td colspan="2">
                    <asp:Label ID="lblMsg" runat="server" Width="100%" CssClass="errormessage"></asp:Label></td>
            </tr>               
            <tr>
                <td align="center">
                 <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="70%">
                    <tr>
                    <td>
					<table id="Table2" cellSpacing="5" cellPadding="5" width="100%" border="0">
                 
                            <tr id="trUser" runat="server">
                                <td style="width: 35%" class="copy10grey" align="Right">
                                    Form Data Type:</td>
                                <td style="width:65%" align="left" >
                                     <asp:DropDownList ID="ddlData" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlData_SelectedIndexChanged">
                                        <asp:ListItem></asp:ListItem>
                                        <%--<asp:ListItem Text="Tracking Information" Value="ship"></asp:ListItem>--%>
                                        <asp:ListItem Text="Purchase Order ESN Database Upload" Value="esn"></asp:ListItem>
                                        <%--<asp:ListItem Text="Upload ESNs to Repository" Value="esnmsl"></asp:ListItem>--%>
                                        <asp:ListItem Text="Close PurchaseOrder" Value="delpo"></asp:ListItem>
                                        <asp:ListItem Text="Delete Purchase Order" Value="delePO"></asp:ListItem>
                                        <asp:ListItem Text="Upload PurchaseOrders" Value="po"></asp:ListItem>
                                        <asp:ListItem Text="Upload Company Stores" Value="cs"></asp:ListItem>
                                        <%--<asp:ListItem Text="Upload Company RMA" Value="rma"></asp:ListItem>--%>
                                        <%--<asp:ListItem Text="BETA " Value="BETA"></asp:ListItem>--%>
									 </asp:DropDownList>
								</td>
                             </tr>

                             <tr id="trItems" runat="server" visible="false">
                                <td colspan="2" align="left"> 
                                                                       
                                    <table width="100%" border="0"  cellSpacing="0" cellPadding="0">
                                    <tr>
                                          <td  class="copy10grey" align="right" >Inventory Item:</td>
                                          <td align="left">
                                            <asp:DropDownList ID="dpItems" runat="server"></asp:DropDownList>
                                            &nbsp;<asp:HyperLink ID="hnk" runat="server" Visible="false"></asp:HyperLink>
                                          </td>  
                                    </tr>
                                    </table>
                                </td>  
                             </tr>

                                   
							<tr id="trTrack" runat="server" visible="false">
								<td colspan="2">
									<table width="100%" border="0"  cellSpacing="0" cellPadding="0" >
										<tr >
											  <td  style="width: 35%" class="copy10grey" align="Right" >Customer:&nbsp;</td>
											  <td align="left">&nbsp;
												 <asp:DropDownList ID="dpCompany" CssClass="copy10grey" runat="server"  AutoPostBack="true"
                                                      onselectedindexchanged="dpCompany_SelectedIndexChanged">
												</asp:DropDownList>
											  </td>  
										</tr>
										<tr>
										    <td colspan="2">
                                                &nbsp;</td>
										</tr>
									</table>                      
                        
								</td>
							</tr>
                             
                            <tr>
                                <td  class="copy10grey" align="right" >
                                    Select a filect a file</td>
                                <td align="left" >
                                    <asp:FileUpload ID="flnUpload" runat="server" CssClass="txfield1" Width="60%" /></td>
                            </tr>
                             <tr id="trItemFile" runat="server" visible="false">
                                <td class="copy10grey" align="center" colspan="2">
                           File format sample: <b>PoNum,PODID,SKU,Esn</b>,Fmupc,LteICCID,LteIMSI,Otksl,Akey
                                </td>
                                
                             </tr>
<tr  id="trComment" runat="server" visible="false"  valign="top">
                                <td class="copy10grey" align="right">
                                    Comment: &nbsp;
                                         </td>
                                <td class="copy10grey" align="left">
                                    <asp:TextBox ID="txtComment" runat="server" CssClass="copy10grey" Width="70%" Height="50px" TextMode="MultiLine"></asp:TextBox>

                                </td>
                             </tr>
 
                             <tr id="trShipFile" runat="server" visible="false">
                                <td class="copy10grey" align="right">
                                    File format sample:
                                         </td>
                                <td class="copy10grey" align="left">
                                    poNum,trackingnumber,AvOrderNumber
                                </td>
                             </tr>
                             <tr id="trMsl" runat="server" visible="false">
                                <td class="copy10grey" align="right">
                                    File format sample:
                                </td>
                                <td class="copy10grey" align="left">
                                    ESN, MEID, AKEY, MSL, OTKSL, AVPO, PRO#, PALLET_ID, CARTON_ID, HEX, ICC_id, LTE_IMSI
                                </td>
                             </tr>
                             <tr id="trStore" runat="server" visible="false">
                                <td class="copy10grey" align="right">
                                    File format sample:
                                </td>
                                <td class="copy10grey" align="left">
                                    StoreID,StoreName,Address,City,State,Country,Zip (in Excel (.xls) format)
                                </td>
                             </tr>
                             <tr id="trRMA" runat="server" visible="false">
                                <td class="copy10grey" align="right">
                                    File format sample:
                                </td>
                                <td class="copy10grey" align="left">
                                    StoreID,StoreName,Address,City,State,Country,Zip (in Excel (.xls) format)
                                </td>
                             </tr>
                             <tr id="trDp" runat="server" visible="false">
                                <td class="copy10grey" align="right">
                                    File format sample:
                                </td>
                                <td class="copy10grey" align="left">
                                    <asp:Label ID="lblFileFormat" runat="server" Text="PurchaseOrderNumber"></asp:Label><br />
                                    <asp:HyperLink  ID="lnlURL" runat="server" Target="_blank"  Visible="false">
                                    </asp:HyperLink>                                  
                                </td>
                             </tr>
                            <tr>
                                <td colspan="4">
                                    </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    </td>
                            </tr>
                            <tr id="trStatus" runat="server">
                                <td style="width: 35%" class="copy10grey" align="right"  >
                                    </td>
                                <td style="width:65%" >
                                    </td>
                             </tr>
                             <tr><td colspan="2">&nbsp;</td></tr>                            
                             <tr>
                                <td colspan="2" align=center><asp:Button ID="btnUpload" Width="190px" CssClass="button" runat="server" OnClick="btnUpload_Click" Text="Upload" />
                                &nbsp;&nbsp;&nbsp;<asp:Button ID="btnMSL" runat="server" Visible="false" CssClass="button" Text="Assign MSL to Fulfillment Items" OnClick="btnMSL_Click" />
                                    </td>
                            </tr>
                    </table>
            </td>
            </tr>
            </table>
                    </td>
                </tr>
                <tr>
                    <td ><br /><br />
                    </td>
                </tr>


            </table>

    </form>
</body>
</html>