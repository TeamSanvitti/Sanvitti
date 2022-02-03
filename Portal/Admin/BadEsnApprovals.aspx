<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BadEsnApprovals.aspx.cs" Inherits="avii.Admin.BadEsnApprovals" %>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="/Controls/Header.ascx" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>.:: Lan Global inc. ::.</title>
    <link href="../aerostyle.css" rel="stylesheet" type="text/css"/>

</head>
<body leftmargin="0" topmargin="0" marginwidth="0" marginheight="0">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table cellSpacing="0" cellPadding="0" align="center" width="100%" border="0">
        	<tr>
				<td><head:MenuHeader id="HeadAdmin" runat="server"></head:MenuHeader></td>
			</tr>
			<tr><td>&nbsp;</td></tr>
			<tr>
				<td  bgcolor="#dee7f6" class="button">&nbsp;&nbsp;Bad ESN Approval
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
                        <tr><td class="copy10grey" align="left">&nbsp;
                        - ESN should exists in repository. Bad ESNs will be removed from fulfillment orders in Processed state.<br />&nbsp;
- Bad ESN can not be used again.<br />&nbsp;
	                    - Upload file should be less than 2 MB. <br />&nbsp;
                        </td></tr>
                    </table>

                 <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="90%">
                    <tr>
                    <td>
			            <table id="Table2" cellSpacing="5" cellPadding="5" width="100%" border="0">
                            
                            <tr>
                                <td  class="copy10grey" align="right" >
                                    <asp:Label ID="lblCust" runat="server" CssClass="copy10grey" Text="Customer Name:"></asp:Label></td>
                                <td align="left" >
                                                
                                    <asp:DropDownList ID="dpCompany" runat="server" AutoPostBack="false" Width="40%" 
                                        class="copy10grey"  >
                                    </asp:DropDownList> 
                                    
           
                                    </td>
                            </tr>
                            <tr>
                                <td  class="copy10grey" align="right" width="35%" >
                                    Upload ESN file: &nbsp;</td>
                                <td align="left" >
                                    <asp:FileUpload ID="flnUpload" runat="server" CssClass="txfield1" Width="40%" /></td>
                            </tr>
                            <tr id="trShipFile" >
                                <td class="copy10grey" align="right">
                                    File format sample: &nbsp;
                                         </td>
                                <td class="copy10grey" align="left">
                                   <b>ESN</b>(.csv)
                                </td>
                             </tr>


                             <%--<tr  >
                                <td class="copy10grey" align="right">
                                    Reason: &nbsp;
                                         </td>
                                <td class="copy10grey" align="left">
                                    <asp:TextBox ID="txtReason" CssClass="copy10grey" runat="server" Width="40%" ></asp:TextBox>
                                </td>
                             </tr>
--%>
                             
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
                                
                                
                                            <asp:Button ID="btnSubmit2" Visible="false" Width="190px"  CssClass="button" runat="server" OnClick="btnSubmit_Click" Text="Submit" />

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
                                <strong>   
                                <asp:Label ID="lblCount" CssClass="copy10grey" runat="server" ></asp:Label> &nbsp;&nbsp;</strong> 

                                </td>
                             </tr>
                             <tr>
                                <td colspan="2" align="center">
                                
                                <asp:Repeater ID="rptESN" runat="server" Visible="true" OnItemDataBound="rpt_OnItemDataBound"  >
                                <HeaderTemplate>
                                <table border="0" width="98%" cellpadding="1" cellspacing="1">
                                <tr>
                                    <td class="button" >
                                        S.No.
                                    </td>
                                    <td class="button">
                                        ESN 
                                    </td>
                                    
                                    <td class="button">
                                        MSL 
                                    </td>
                                    
                                    <td class="button">
                                        Product Code 
                                    </td>
                                    
                                    
                                    <td class="button">
                                        MEID 
                                    </td>
                                    
                                    <td class="button">
                                        HEX 
                                    </td>
                                    <td class="button">
                                        AKEY
                                    </td>
                                    <td class="button">
                                        AVPO 
                                    </td>
                                    <td class="button">
                                        OTKSL
                                    </td>
                                    
                                    
                                    <td class="button">
                                        ICC ID 
                                    </td>
                                    
                                    <td class="button">
                                        LTE IMSI
                                    </td>
                                    
                                </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
    
                                    <tr valign="bottom"  class="<%# Container.ItemIndex % 2 == 0 ? "alternaterow" : "" %>">
                                        <td class="copy10grey"  >
                                        <%# Container.ItemIndex +  1 %>

                                       <%-- <%# Convert.ToBoolean(Eval("ISESN")) == false ? "red" : ""%>--%>
                                        </td>
                                        <td valign="bottom" class="copy10grey"  style="background-color:<%# Convert.ToBoolean(Eval("ISESN")) == false ? "red" : ""%>">
                                        <span width="100%">
                                            <%# Eval("ESN")%>   
                                            <asp:HiddenField ID="hdnIsESN" Value='<%# Eval("ISESN")%>' runat="server" /> 
                                            </span>
                                        </td>
                                        <%--<td valign="bottom" class="copy10grey"  >
                                        <span width="100%">
                                            <%# Eval("FMUPC")%>    
                                            </span>
                                        </td>--%>
                                        <td valign="bottom" class="copy10grey"  >
                                        <span width="100%">
                                            <%# Eval("MslNumber")%>    
                                            </span>
                                        </td>
                                        <td valign="bottom" class="copy10grey"  >
                                        <span width="100%">
                                            <%# Eval("Item_code")%>    
                                            </span>
                                        </td>
                                        <td valign="bottom" class="copy10grey"  >
                                        <span width="100%">
                                            <%# Eval("MEID")%>    
                                            </span>
                                        </td>
                                        <td valign="bottom" class="copy10grey"  >
                                        <span width="100%">
                                            <%# Eval("HEX")%>    
                                            </span>
                                        </td>
                                        <td align="left" class="copy10grey" valign="bottom">
                                            <%# Eval("AKEY")%>   
            
                                        </td>
                                        <td align="left" class="copy10grey" valign="bottom">
                                            <%# Eval("AVPO")%>   
            
                                        </td>
                                        <td align="left" class="copy10grey" valign="bottom">
                                            <%# Eval("OTKSL")%>   
            
                                        </td>
                                        <td align="left" class="copy10grey" valign="bottom">
                                            <%# Eval("icc_id")%>   
            
                                        </td>
                                        <td align="left" class="copy10grey" valign="bottom">
                                            <%# Eval("lte_imsi")%>   
            
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
                                
                                <asp:Button ID="btnUpload"  Width="190px" CssClass="button" runat="server" OnClick="btnValidateUploadedFile_Click" Text="Validate Uploaded file" />

                                &nbsp;<asp:Button ID="btnSubmit" Visible="false" Width="190px"  CssClass="button" runat="server" OnClick="btnSubmit_Click" Text="Submit" />
                                &nbsp;<asp:Button ID="btnViewAssignedESN" Visible="false" Width="190px"  CssClass="button" runat="server" OnClick="btnViewAssignedESN_Click" Text="View Assigned ESN" />

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
<br />
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
