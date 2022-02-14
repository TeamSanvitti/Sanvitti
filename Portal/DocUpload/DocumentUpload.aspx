<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DocumentUpload.aspx.cs" Inherits="avii.DocUpload.DocumentUpload" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>
<%@ Register TagPrefix="menu" TagName="Menu" Src="~/Controls/Header.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Manage Documents</title>
      <style type="text/css">
		.ui-widget{font-size:11px !important;}
		.ui-state-error-text{margin-left: 10px}
		.ui-state-error-text{margin-left: 10px}
	</style>
  
</head>
<body  bgcolor="#ffffff" leftmargin="0" rightmargin="0" topmargin="0">
    <form id="form1" runat="server">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table cellspacing="0" cellpadding="0" border="0" align="center" width="100%">
    <tr>
        <td>
            <menu:Menu ID="menu1" runat="server" ></menu:Menu>
        </td>
     </tr>
     </table>
    
     <table cellspacing="0" cellpadding="0" border="0" align="center" width="95%">
        
        <tr valign="top">
           
            <td>
                <table align="center" style="text-align:left" width="100%">
                <tr class="buttonlabel" align="left">
                <td>&nbsp;Manage Documents</td></tr>
             </table>
            <asp:UpdatePanel ID="UpdatePanel1"  UpdateMode="Conditional" ChildrenAsTriggers="true" runat="server"   >
             <ContentTemplate>
             <table  align="center" style="text-align:left" width="100%">
             <tr>
                <td>
                   <asp:Label ID="lblMsg" runat="server" CssClass="errormessage"></asp:Label>
            
                </td>
             </tr>
             </table>
             <table bordercolor="#839abf" border="1" width="100%" align="center" cellpadding="0" cellspacing="0">
      <tr>
        <td>           
            <asp:Panel  ID="Panel1" runat="server"  >
        <table width="100%" border="0" class="box" align="center" cellpadding="2" cellspacing="2">
         
            <tr valign="top">
                <td class="copy10grey" align="right" width="15%">
                    <strong>Customer Name:</strong>
                </td>
                <td width="35%">
                    <asp:DropDownList ID="dpCompany" CssClass="copy10grey" runat="server" Width="80%"
                    >
				    </asp:DropDownList>
                
                </td>
                <td  width="1%">
                    &nbsp;
                </td>
                <td class="copy10grey" align="right" width="15%">
                    Fulfillment#:
                </td>
                <td width="35%">
                                       
                  <asp:TextBox ID="txtFulfillmentNo" runat="server"   CssClass="copy10grey" MaxLength="20"  Width="80%"></asp:TextBox>
                </td>   
                <td>
                     
                </td>
                
                    
                </tr>
            <tr>
                <td align="center" colspan="5">
                    <hr />
                </td>
                
            </tr>
            <tr>
                <td align="center" colspan="5">
                    <asp:Button ID="btnSearch" runat="server" TabIndex="18"  CssClass="buybt" OnClientClick="return Validate();" 
                                        Text="   Search   " onclick="btnSearch_Click" />&nbsp;&nbsp;
                        <asp:Button ID="btnCancel" runat="server" TabIndex="19" CssClass="buybt" CausesValidation="false"  Text="   Cancel   " onclick="btnCancel_Click" />&nbsp;&nbsp;
                        <asp:Button ID="btnClose" runat="server" TabIndex="20" Visible="false" CssClass="buybt" CausesValidation="false"  Text="Close" onclick="btnClose_Click" />
                </td>
                
            </tr>

         </table>
        </asp:Panel>
            
        </td>
     </tr>
     </table> 
         <br />
         <asp:Panel  ID="pnlPO" runat="server" Visible="true"  >
     <table bordercolor="#839abf" border="1" width="100%" align="center" cellpadding="0" cellspacing="0">
      <tr>
        <td>
         
     <table width="100%" border="0" class="box" align="center" cellpadding="2" cellspacing="2">
     
           
                <tr valign="top">
                <td class="copy10grey" align="right" width="15%">
                    <strong>Fulfillment#:</strong>
                </td>
                <td width="35%">
                    <asp:Label ID="lblPONum" runat="server"  CssClass="copy10grey" ></asp:Label>
                </td>
                <td  width="1%">
                    &nbsp;
                </td>
                <td class="copy10grey" align="right" width="15%">
                    <strong>Order Date:</strong>
                </td>
                <td width="35%">
                    <asp:Label ID="lblOrderDate" runat="server"  CssClass="copy10grey" ></asp:Label>
                    
                </td>   
                
                    
                </tr>
                <tr valign="top">
                <td class="copy10grey" align="right" width="15%">
                    <strong>Quantity:</strong>
                </td>
                <td width="35%">
                <asp:Label ID="lblOrderQty" runat="server"  CssClass="copy10grey" ></asp:Label>
                </td>
                <td  width="1%">
                    &nbsp;
                </td>
                <td class="copy10grey" align="right" width="15%">
                    <strong>Status:</strong>
                </td>
                <td width="35%">
                   
                    <asp:Label ID="lblStatus" runat="server"  CssClass="copy10grey" ></asp:Label>                          
        
                </td>   
                
                    
                </tr>
                  
                
                
        </table>
            
   
     </td>
     </tr>
     </table>      
         </asp:Panel>
         <br />
         <table bordercolor="#839abf" border="1" width="98%" align="center" cellpadding="0" cellspacing="0">
                            <tr><td>
                                    <table border="0" width="100%" class="box" align="center" cellpadding="5" cellspacing="5">
                                    <tr class="">
                                        <td class="buttongrid" width="30%">
                                            Document Description
                                        </td>
                                        <td  class="buttongrid" width="30%">
                                            Upload
                                        </td>
                                        <td  class="buttongrid" width="30%">
                                            Uploaded file
                                        </td>
                                    </tr>
                                    <tr >
                                        <td class="copy10grey" width="120">
                                            <asp:TextBox ID="txtFile1Desc" runat="server" CssClass="copy10grey" Width="80%" TextMode="MultiLine" Rows="2"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:FileUpload ID="fupDocA1" runat="server" />  
                                        </td>
                                        <td>
                                            
                                        </td>
                                    </tr>
                                    <tr >
                                        <td class="copy10grey">
                                            <asp:TextBox ID="txtFile2Desc" runat="server" CssClass="copy10grey" Width="80%" TextMode="MultiLine" Rows="2"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:FileUpload ID="fupDocA2" runat="server" />       
                                        </td>
                                        <td>
                                            
                                        </td>
                                    </tr>                                    
                                    <tr>
                                        <td class="copy10grey">
                                            <asp:TextBox ID="txtFile3Desc" runat="server" CssClass="copy10grey" Width="80%" TextMode="MultiLine" Rows="2"></asp:TextBox>
                                        </td>                                        
                                        <td>
                                            <asp:FileUpload ID="fupDocA3" runat="server" />  
                                        </td>
                                        <td>
                                            
                                        </td>
                                    </tr>                                    
                                    <tr >
                                        <td class="copy10grey">
                                            <asp:TextBox ID="txtFile4Desc" runat="server" CssClass="copy10grey" Width="80%" TextMode="MultiLine" Rows="2"></asp:TextBox>
                                        </td>                                        
                                        <td>
                                            <asp:FileUpload ID="fupDocA4" runat="server" /> 
                                        </td>
                                        <td>
                                            
                                        </td>
                                    </tr>
                                    <tr >
                                        <td class="copy10grey">
                                            <asp:TextBox ID="txtFile5Desc" runat="server" CssClass="copy10grey" Width="80%" TextMode="MultiLine" Rows="2"></asp:TextBox>
                                        </td>                                        
                                        <td>
                                            <asp:FileUpload ID="fupDocA5" runat="server" /> 
                                        </td>
                                        <td>
                                            
                                        </td>
                                    </tr>
                                    </table>
                                </td>
                            </tr>
                            </table>
                            <table border="0" width="100%"  align="center" cellpadding="5" cellspacing="5">
                            <tr>
                                <td align="center">
                                        
                                    <asp:Button ID="btnDocUpload" runat="server" Text="Upload" CssClass="button" OnClick="btnDocUpload_Click"   
                                        OnClientClick="return ValidateFile();"  />
                                    <asp:Button ID="btnUloadCancel" runat="server" Text="Cancel" CssClass="button" OnClick="btnUloadCancel_Click"/>
                                </td>
                            </tr>
                            </table>


                 </ContentTemplate>
            </asp:UpdatePanel>

            </td>
        </tr>
       </table>
    </form>
</body>
</html>
