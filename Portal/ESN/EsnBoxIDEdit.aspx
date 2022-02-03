<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EsnBoxIDEdit.aspx.cs" Inherits="avii.ESN.EsnBoxIDEdit" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>
<%@ Register TagPrefix="menu" TagName="Menu" Src="~/Controls/Header.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>BOX ID Edit</title>
        <script type="text/javascript">

         function close_window() {
             if (confirm("Close Window?")) {
                 window.close();
                 return true;
             }
             else
                 return false
             }
             function OpenNewPage(url) {

                 var newWin = window.open(url);

                 if (!newWin || newWin.closed || typeof newWin.closed == 'undefined') {
                     alert('your pop up blocker is enabled');

                     //POPUP BLOCKED
                 }
             }
        </script>
   

</head>
<body bgcolor="#ffffff" leftmargin="0" rightmargin="0" topmargin="0">
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
            <td   >
                <table align="center" style="text-align:left" width="100%">
                <tr class="buttonlabel" align="left">
                    <td>&nbsp;BOXID Edit</td>

                </tr>
                </table>
            <asp:UpdatePanel ID="UPl1"  UpdateMode="Conditional" ChildrenAsTriggers="true" runat="server"   >
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
             
                 <table width="100%" border="0" class="box" align="center" cellpadding="5" cellspacing="5">
                 <tr style="height:1px">
                 <td style="height:1px"></td>
                 </tr>
                    <tr>
                    <td class="copy10grey" align="right" width="20%">
                      <strong> <span> ESN:</span></strong>
                    </td>
                    <td width="30%">
                           <asp:Label ID="txtESN" runat="server"  CssClass="copy10grey"   ></asp:Label>
                                                
        
                
                    </td>
                    <td  width="1%">
                        &nbsp;
                    </td>
                    <td class="copy10grey" align="right" width="15%">
                   Fulfillment#:
                    </td>
                    <td width="35%">
                             
                      <asp:LinkButton ID="lnkPO" runat="server" CssClass=".copy10underline" OnClick="lnkPO_Click"  ></asp:LinkButton>
                                        
         
                    </td>   
                
                    
                    </tr>
                    <tr >
                    <td class="copy10grey" align="right" width="20%">
                        <strong> SKU#:</strong>
                    </td>
                    <td width="30%">
                        <asp:Label ID="txtSKU" runat="server" CssClass="copy10grey"></asp:Label>
                                        
         
                    </td>
                    <td  width="1%">
                        &nbsp;
                    </td>
                    <td class="copy10grey" align="right" width="15%">
                    <b>    Category:</b>
                    
                    </td>
                    <td width="35%">
                   
                          <asp:Label ID="txtCategory" runat="server" CssClass="copy10grey"></asp:Label>
                                        
         
                    </td>   
                
                    
                    </tr>
                     <tr >
                    <td class="copy10grey" align="right" width="20%">
                     <b>   Hex#:</b>
                    </td>
                    <td width="30%">
                        <asp:Label ID="txtHex" runat="server" CssClass="copy10grey" ></asp:Label>
                                        
         
                    </td>
                    <td  width="1%">
                        &nbsp;
                    </td>
                    <td class="copy10grey" align="right" width="15%">
                      <b>  Dec:</b>
                    
                    </td>
                    <td width="35%">
                   
                          <asp:Label ID="txtDec" runat="server" CssClass="copy10grey" ></asp:Label>
                                        
         
                    </td>   
                
                    
                    </tr>
                     <tr >
                    <td class="copy10grey" align="right" width="20%">
                        <b>Serial#:</b>
                    </td>
                    <td width="30%">
                        <asp:Label ID="txtSerialNo" runat="server" CssClass="copy10grey"></asp:Label>
                                        
         
                    </td>
                    <td  width="1%">
                        &nbsp;
                    </td>
                    <td class="copy10grey" align="right" width="15%">
                      <b>  Receive Date:</b>
                    
                    </td>
                    <td width="35%">
                   
                          <asp:Label ID="txtReceiveDate" runat="server" CssClass="copy10grey" ></asp:Label>
                                        
         
                    </td>
                    </tr>
          
            
                   
               
                </table>
              
             </td>
             </tr>
             </table>

                 <br />
                 <table bordercolor="#839abf" border="1" width="100%" align="center" cellpadding="0" cellspacing="0">
              <tr>
                <td>
             
                 <table width="100%" border="0" class="box" align="center" cellpadding="5" cellspacing="5">
                 <tr style="height:1px">
                 <td style="height:1px"></td>
                 </tr>
                    <tr >
                    <td class="copy10grey" align="right" width="20%">
                        Configured Items/Box:
                    </td>
                    <td width="30%">
                           <asp:Label ID="txtBoxItems" runat="server"  CssClass="copy10grey"></asp:Label>
                                                
        
                
                    </td>
                    <td  width="1%">
                        &nbsp;
                    </td>
                    <td class="copy10grey" align="right" width="15%">
                    Assigned  Items/Box
                    </td>
                    <td width="35%">
                             
          <asp:Label ID="txtAssignedQtyBox" runat="server" CssClass="copy10grey" ></asp:Label>
                                        
         
                    </td>   
                
                    
                    </tr>
                    <tr >
                    <td class="copy10grey" align="right" width="20%">
                      <b>  Box ID:</b>
                    </td>
                    <td width="30%">
                        <asp:TextBox ID="txtBoxID" runat="server" CssClass="copy10grey" MaxLength="10"   Width="40%"></asp:TextBox>
                                        
         
                    </td>
                    <td  width="1%">
                        &nbsp;
                    </td>
                    <td class="copy10grey" align="right" width="15%">
                     
                    </td>
                    <td width="35%">
                   
                        
                    </td>   
                
                    
                    </tr>
                    
          
            
                   
               
                </table>
              
             </td>
             </tr>
             </table>
                 <br />
                 <table width="100%" border="0" class="box" align="center" cellpadding="5" cellspacing="5">
                     <tr>
                         <td align="center">
                            <asp:Button ID="btnRemove" runat="server"  CssClass="button" OnClientClick="return confirm('Are you sure to remove from box?');" Text="Remove from Box" OnClick="btnRemove_Click" ></asp:Button>
                            &nbsp;<asp:Button ID="btnAssign" runat="server" Text="Assign to Box" CssClass="button"  OnClick="btnAssign_Click" CausesValidation="false"/>
                            &nbsp;<asp:Button ID="btnClosew" runat="server" Text="Close" CssClass="button" Visible="true" OnClientClick="return close_window();"  />
                    
                         </td>
                     </tr>
                 </table>
             </ContentTemplate>             
           </asp:UpdatePanel>
                
<%--            <asp:UpdatePanel ID="upnlJsRunner" UpdateMode="Always" runat="server">
			<ContentTemplate>
				<asp:PlaceHolder ID="phrJsRunner" runat="server"></asp:PlaceHolder>
			</ContentTemplate>
		</asp:UpdatePanel>--%>

            <asp:UpdateProgress ID="UpdateProgress1" runat="server"
            DynamicLayout="false">
            <ProgressTemplate>
                <img src="/Images/ajax-loaders.gif" />Loading ...
            </ProgressTemplate>
            </asp:UpdateProgress>   
            </td>
        </tr>
        </table>

           <br /><br /> <br />
            <br /> <br />
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
