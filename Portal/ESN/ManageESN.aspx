<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageESN.aspx.cs" Inherits="avii.ESN.ManageESN" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>
<%@ Register TagPrefix="menu" TagName="Menu" Src="~/Controls/Header.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>IMEI Reconditioning</title>

    <script type="text/javascript">
        function OpenNewPage(url) {

            var newWin = window.open(url);

            if (!newWin || newWin.closed || typeof newWin.closed == 'undefined') {
                alert('Your browser pop up blocker is enabled');

                //POPUP BLOCKED
            }
        }
        </script>

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
    <table cellspacing="0" cellpadding="0" border="0" align="center" width="100%">
    <tr>
        <td>
    
    <table cellspacing="0" cellpadding="0" border="0" align="center" style="text-align:left" width="95%">
        <tr class="buttonlabel" align="left">
            <td>&nbsp;IMEI Reconditioning</td>
        </tr>
    </table>
    <table cellSpacing="0" cellPadding="0" align="center" width="95%" border="0" id="maintbl">
        <tr>
	        <td>
    <asp:UpdatePanel ID="upnlCustomers" UpdateMode="Conditional" runat="server">
	<ContentTemplate>
	
    <table cellSpacing="0" cellPadding="0" align="center" width="100%" border="0">
    	<tr>                    
            <td><asp:Label ID="lblMsg" runat="server"  CssClass="errormessage"></asp:Label></td>
        </tr> 
     </table>
    
        <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="100%">
            <tr>
            <td>
			    <table  cellSpacing="5" cellPadding="5" width="100%" border="0">
                <tr>
                    <td  class="copy10grey" align="right" >
                        Customer: &nbsp;</td>
                    <td align="left" >

                        <asp:DropDownList ID="dpCompany" CssClass="copy10grey" runat="server" Width="30%">
				        </asp:DropDownList>
                    </td>
                    </tr>
                    <tr>
                    <td  class="copy10grey" align="right" >
                        Upload file: &nbsp;</td>
                    <td align="left" >

                        <asp:FileUpload ID="fu" CssClass="copy10grey" runat="server" onchange="return fileValidation();" />
                    </td>
                    </tr>
                    <tr  >
                    <td width="45%" class="copy10grey" align="right">
                        File format sample: &nbsp;
                                </td>
                    <td class="copy10grey" align="left">
                                                 
                        <b>IMEI</b> <asp:LinkButton ID="lnkDownload" runat="server"   Text="Download file format" OnClick="lnkDownload_Click"></asp:LinkButton>
                                                

                    </td>
                    </tr>                                        
                    <tr><td colspan="2" align="center"><hr style="width:100%" /></td></tr>
                    <tr>

                    <td align="center" colspan="2">
                        <asp:Button ID="btnUpload" CssClass="button"  runat="server" Text="Validate Uploaded file" OnClick="btnUpload_Click" />
                         &nbsp;<asp:Button ID="btnCancel1" runat="server"  CssClass="button" Text="Cancel" OnClick="btnCancel_Click" />
                    </td>
                </tr>
                </table>
                </td>
            </tr>                           
            </table>

            <asp:Panel ID="pnlESN" runat="server" Visible="false">
            <br />
            <table cellspacing="0" cellpadding="0" border="0" align="center" style="text-align:left" width="100%">
                <tr class="buttonlabel" align="left">
                    <td>&nbsp;SKU Summary</td>
                </tr>
            </table>
    
                <asp:Repeater ID="rptSKU" runat="server" >
                    <HeaderTemplate>
                    <table bordercolor="#839abf" border="1" width="100%" cellpadding="1" cellspacing="1">
                    <tr>
                        <td class="buttongrid"  width="1%" >
                            S.No.
                        </td>
                        <td class="buttongrid"  width="15%">
                            Category Name
                        </td>
                        <td class="buttongrid"  width="15">
                            RAW SKU
                        </td>
                        <td class="buttongrid"  width="20%">
                            Product Name
                        </td>
                        <td class="buttongrid"  width="15%" align="center">
                            Current Stock
                        </td>
                        <td class="buttongrid"  width="15%" align="center">
                            Impact on ESN 
                        </td>            
                        <td class="buttongrid"  width="15%" align="center">
                            Proposed Stock
                        </td>
                    </tr>
                    </HeaderTemplate>
                    <ItemTemplate>    
                        <tr valign="bottom"  class="<%# Container.ItemIndex % 2 == 0 ? "alternaterow" : "" %>">
                            <td class="copy10grey"  >
                            <%# Container.ItemIndex +  1 %>
                            </td>
                            <td  class="copy10grey"  >
                                <%# Eval("CategoryName")%>
                            </td>
                            <td  class="copy10grey"  >
                                <%# Eval("RawSKU")%>
                            </td><td  class="copy10grey"  >
                                <%# Eval("ItemName")%>
                            </td>
                            <td  class="copy10grey"   align="right">
                                <%# Eval("CurrentStock")%> &nbsp;
                            </td>
                            <td class="copy10grey" align="right">
                                <%# Eval("ESNCount") %> &nbsp;
                            </td>
                            <td  class="copy10grey"   align="right">
                                <%# Eval("ProposedStock")%> &nbsp;
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                    </table>
                    </FooterTemplate>
                </asp:Repeater>
                <br />
                <asp:Repeater ID="rptESN" runat="server"  >
                    <HeaderTemplate>
                    <table bordercolor="#839abf" border="1" width="100%" cellpadding="1" cellspacing="1">
                    <tr>
                        <td class="buttongrid"  width="1%" >
                            S.No.
                        </td>
                        <td class="buttongrid"  width="20%">
                            IMEI
                        </td>                        
                        <td class="buttongrid"  width="20%">
                            Kitted SKU
                        </td>
                        <td class="buttongrid"  width="20%">
                            Raw SKU
                        </td>
                        <td class="buttongrid"  width="20%">
                            Fulfillment#
                        </td>                        
                        <td class="buttongrid" >
                            
                        </td>                       
                    </tr>
                    </HeaderTemplate>
                    <ItemTemplate>    
                        <tr valign="bottom"  class="<%# Container.ItemIndex % 2 == 0 ? "alternaterow" : "" %>">
                            <td class="copy10grey"  >
                            <%# Container.ItemIndex +  1 %>
                            </td>
                            <td  class="copy10grey"  >
                                <asp:Label ID="lblESN" runat="server" CssClass="copy10grey" Text='<%# Eval("ESN")%>'></asp:Label>
                            </td>       
                            <td class="copy10grey">
                                <%# Eval("KittedSKU") %>
                            </td>                                   
                            <td class="copy10grey">
                                <%# Eval("RawSKU") %>
                            </td>       
                            <td class="copy10grey">
                                <asp:LinkButton ID="lnkPO" runat="server" CommandArgument='<%# Eval("POID") %>' OnCommand="lnkPO_Command" >
                                    <%# Eval("FulfillmentNumber") %>
                                </asp:LinkButton>                                
                            </td>
                            <td  class="errormessage"  >
                                <%# Eval("ErrorMessage")%>                                    
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                    </table>
                    </FooterTemplate>
                    </asp:Repeater>                  
                <br />
             <table cellspacing="0" cellpadding="0" border="0" align="center" style="text-align:left" width="95%">             
             <tr>
                <td align="center">&nbsp;
                       <asp:Button ID="btnSubmit"  CssClass="button" runat="server" Text="Submit"  Visible="false"  OnClientClick="return confirm('This will impact the inventory of listed SKU(s), Are you sure to continue?');"
                        OnClick="btnSubmit_Click" />&nbsp;       
                       <asp:Button ID="btnCancel" CssClass="button" runat="server" Text="Cancel" Visible="false" OnClientClick="return confirm('This will impact the inventory of listed SKU(s), Are you sure to continue?');"
                        CausesValidation="false" OnClick="btnCancel_Click" /> 
                                        

                </td>
             </tr>
             </table>
            </asp:Panel>          
          </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnUpload" />
            <asp:PostBackTrigger ControlID="lnkDownload" />
        </Triggers>
        </asp:UpdatePanel>
		
            </td>
       </tr>
    </table>

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
