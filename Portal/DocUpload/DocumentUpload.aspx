<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DocumentUpload.aspx.cs" Inherits="avii.DocUpload.DocumentUpload" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>
<%@ Register TagPrefix="menu" TagName="Menu" Src="~/Controls/Header.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Fulfillment Documents</title>
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
                <tr class="buttonlabel" align="left" style="font-size:14px">
                <td>&nbsp;Fulfillment Documents</td></tr>
             </table>
            <%--<asp:UpdatePanel ID="docUpdatePanel1"  UpdateMode="Conditional" runat="server"   >
             <ContentTemplate>--%>

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
            <tr valign="top" runat="server" id="trCustomer">
                <td class="copy10grey" align="right" width="40%">
                    <strong>Customer Name:</strong>
                </td>
                <td width="60%">
                    <asp:DropDownList ID="dpCompany" CssClass="copy10grey" runat="server" Width="40%">
				    </asp:DropDownList>
                
                </td>                
            </tr>
            <tr valign="top">
                <td class="copy10grey" align="right" width="40%">
                    <strong>Fulfillment#:</strong>
                </td>
                <td width="60%">
                                       
                  <asp:TextBox ID="txtFulfillmentNo" runat="server"   CssClass="copy10grey" MaxLength="20"  Width="40%"></asp:TextBox>
                </td>   
                <td>
                     
                </td>
                
                    
                </tr>
            <tr>
                <td align="center" colspan="3">
                    <hr />
                </td>
                
            </tr>
            <tr>
                <td align="center" colspan="3">
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
         <asp:Panel  ID="pnlPO" runat="server" Visible="false"  >
     <table bordercolor="#839abf" border="1" width="100%" align="center" cellpadding="0" cellspacing="0">
      <tr>
        <td>
         
     <table width="100%" border="0" class="box" align="center" cellpadding="2" cellspacing="2">
     
           
                <tr>
                <td class="copy10grey" align="right" width="15%">
                    <span>Fulfillment#:</span>
                </td>
                <td width="35%">
                    <asp:Label ID="lblPONum" runat="server"  CssClass="copy10grey" ></asp:Label>
                </td>
                <td  width="1%">
                    &nbsp;
                </td>
                <td class="copy10grey" align="right" width="15%">
                    <span>Order Date:</span>
                </td>
                <td width="35%">
                    <asp:Label ID="lblOrderDate" runat="server"  CssClass="copy10grey" ></asp:Label>
                    
                </td>   
                </tr>
                <tr >
                <td class="copy10grey" align="right" width="15%">
                    <span>Quantity:</span>
                </td>
                <td width="35%">
                <asp:Label ID="lblOrderQty" runat="server"  CssClass="copy10grey" ></asp:Label>
                </td>
                <td  width="1%">
                    &nbsp;
                </td>
                <td class="copy10grey" align="right" width="15%">
                    <span>Status:</span>
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
            <asp:Panel ID="pnlUpload" runat="server" Visible="false">
            <table bordercolor="#839abf" border="1" width="98%" align="center" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <table border="0" width="100%" class="box" align="center" cellpadding="5" cellspacing="5">
                    <tr class="">                        
                        <td  class="buttongrid" style="font-size:14px" width="33%">
                            Upload
                        </td>
                        <td class="buttongrid" width="65%" style="font-size:14px">
                            Document Description
                        </td>
                        
                        <td class="buttongrid" ></td>
                    </tr>
                    <tr style="height:10px !important; background-color:#87ceeb" >
                        <td colspan="3" >
                            <asp:Label ID="lblContentType" Text="File format: pdf, doc, docx, png, jpeg & txt" Font-Size="12px" CssClass="copy10grey"   runat="server"></asp:Label>
                            
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:FileUpload ID="fupDoc1" runat="server" CssClass="copy10grey" /> 
                            <asp:LinkButton ID="lnkDocfile1" CssClass="copy10grey" ForeColor="#4092D1" Font-Underline="true"  Font-Size="12px" OnClick="lnkDownload1_Click" runat="server"></asp:LinkButton>  
                        </td>
                        <td class="copy10grey" width="120">
                            <asp:TextBox ID="txtFile1Desc" runat="server" CssClass="copy10grey" Width="90%" TextMode="MultiLine" Rows="2"></asp:TextBox>
                        </td>
                        
                        <td>
                            <asp:ImageButton ID="img1" ImageUrl="~/images/delete.png" OnClientClick="return confirm('Are you sure want to delete this file?');"  OnClick="lnkDocfile1_Click" runat="server"></asp:ImageButton> 
                            
                            <asp:HiddenField ID="hdnDocID1" runat="server"></asp:HiddenField>
                            
                        </td>
                    </tr>
                    <tr class="alternaterow">
                        <td>
                            <asp:FileUpload ID="fupDoc2" runat="server" CssClass="copy10grey" /> 
                            <asp:LinkButton ID="lnkDocfile2" CssClass="copy10grey"  ForeColor="#4092D1" Font-Underline="true" Font-Size="12px" OnClick="lnkDownload2_Click" runat="server"></asp:LinkButton>     
                            
                        </td>
                        <td class="copy10grey">
                            <asp:TextBox ID="txtFile2Desc" runat="server" CssClass="copy10grey" Width="90%" TextMode="MultiLine" Rows="2"></asp:TextBox>
                        </td>
                        
                        <td>
                            <asp:ImageButton ID="img2" ImageUrl="~/images/delete.png" OnClientClick="return confirm('Are you sure want to delete this file?');"  OnClick="lnkDocfile2_Click" runat="server"></asp:ImageButton>  
                            
                            <asp:HiddenField ID="hdnDocID2" runat="server"></asp:HiddenField>
                        </td>
                    </tr>                                    
                    <tr>
                        <td>
                            <asp:FileUpload ID="fupDoc3" runat="server" CssClass="copy10grey" /> 
                            <asp:LinkButton ID="lnkDocfile3" CssClass="copy10grey" Font-Size="12px" ForeColor="#4092D1" Font-Underline="true"  OnClick="lnkDownload3_Click" runat="server"></asp:LinkButton>    
                            
                        </td>
                        <td class="copy10grey">
                            <asp:TextBox ID="txtFile3Desc" runat="server" CssClass="copy10grey" Width="90%" TextMode="MultiLine" Rows="2"></asp:TextBox>
                        </td>                                        
                        
                        <td>
                            <asp:ImageButton ID="img3" ImageUrl="~/images/delete.png" OnClientClick="return confirm('Are you sure want to delete this file?');"  OnClick="lnkDocfile3_Click" runat="server"></asp:ImageButton>  
                            <asp:HiddenField ID="hdnDocID3" runat="server"></asp:HiddenField>
                        </td>
                    </tr>                                    
                    <tr  class="alternaterow">
                        
                        <td>
                            <asp:FileUpload ID="fupDoc4" runat="server" CssClass="copy10grey" /> 
                            <asp:LinkButton ID="lnkDocfile4" CssClass="copy10grey" Font-Size="12px"  ForeColor="#4092D1" Font-Underline="true" OnClick="lnkDownload4_Click" runat="server"></asp:LinkButton>
                            
                        </td>
                        <td class="copy10grey">
                            <asp:TextBox ID="txtFile4Desc" runat="server" CssClass="copy10grey" Width="90%" TextMode="MultiLine" Rows="2"></asp:TextBox>
                        </td>                                        
                        
                        <td>
                            <asp:ImageButton ID="img4" ImageUrl="~/images/delete.png" OnClientClick="return confirm('Are you sure want to delete this file?');"  OnClick="lnkDocfile4_Click" runat="server"></asp:ImageButton>  
                             <asp:HiddenField ID="hdnDocID4" runat="server"></asp:HiddenField>
                        </td>
                    </tr>
                    <tr >
                        <td>
                            <asp:FileUpload ID="fupDoc5" runat="server" CssClass="copy10grey" />
                            <asp:LinkButton ID="lnkDocfile5" Font-Size="12px" CssClass="copy10grey"  ForeColor="#4092D1" Font-Underline="true" OnClick="lnkDownload5_Click"  runat="server"></asp:LinkButton>
                            
                            
                        </td>
                        <td class="copy10grey">
                            <asp:TextBox ID="txtFile5Desc" runat="server" CssClass="copy10grey" Width="90%" TextMode="MultiLine" Rows="2"></asp:TextBox>
                        </td>                                        
                        
                        <td>
                            <asp:ImageButton ID="img5" ImageUrl="~/images/delete.png" OnClientClick="return confirm('Are you sure want to delete this file?');"  OnClick="lnkDocfile5_Click" runat="server"></asp:ImageButton>  
                            <asp:HiddenField ID="hdnDocID5" runat="server"></asp:HiddenField>                
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
                        />
                    <asp:Button ID="btnUloadCancel" runat="server" Text="Cancel" CssClass="button" OnClick="btnUloadCancel_Click"/>
                </td>
            </tr>
            </table>
                </asp:Panel>
                 <%--</ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnDocUpload" />
                </Triggers>
            </asp:UpdatePanel>--%>
                <asp:UpdatePanel ID="upnlJsRunner" UpdateMode="Always" runat="server">
			<ContentTemplate>
				<asp:PlaceHolder ID="phrJsRunner" runat="server"></asp:PlaceHolder>
			</ContentTemplate>
		</asp:UpdatePanel>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server"
                     DynamicLayout="false">
            <ProgressTemplate>
                <img src="/Images/ajax-loaders.gif" /> Loading ...
            </ProgressTemplate>
        </asp:UpdateProgress>
        
            </td>
        </tr>
       </table>
         <br /> <br />
            <br /> <br />

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
