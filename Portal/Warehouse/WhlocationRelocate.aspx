<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WhlocationRelocate.aspx.cs" Inherits="avii.Warehouse.WhlocationRelocate" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="~/Controls/Header.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Warehouse Location </title>
    <script>
        function isNumberKey(evt) {

            var charCodes = evt.keyCode ? evt.keyCode : evt.which ? evt.which : evt.charCode;
            if (charCodes > 31 && (charCodes < 48 || charCodes > 57)) {
                charCodes = 0;
                return false;
            }
            return true;
        }
        function ValidateQuantity(obj) {
            var qty = document.getElementById("<%=hdQty.ClientID%>").value;
            if (parseInt(qty) <= 0) {

                alert('Quantity can not be 0 or less');
                obj.value = qty;
                return false;
            }
            if (parseInt(obj.value) > parseInt(qty)) {
                alert('Quantity can not be greater than ' + qty);
                obj.value = qty;
                return false;
            }
            return true;

        }
    </script>
</head>
<body bgcolor="#ffffff" leftmargin="0" rightmargin="0" topmargin="0">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
      <table cellSpacing="0" cellPadding="0" align="center" width="100%" border="0">
        <tr>
			<td><head:MenuHeader id="HeadAdmin" runat="server"></head:MenuHeader></td>
		</tr>
     </table>

    <table cellspacing="0" cellpadding="0" border="0" align="center" width="95%">
		<tr>
			<td  bgcolor="#dee7f6" class="buttonlabel">
            &nbsp;&nbsp;Warehouse Location Relocate
			</td>
		</tr>    
    </table>
       <asp:UpdatePanel ID="upnlCode" UpdateMode="Conditional" runat="server">
	        <ContentTemplate>	            
            <table cellSpacing="0" cellPadding="0" align="center" width="95%" border="0">
    	        <tr>                    
                    <td><asp:Label ID="lblMsg" runat="server"  CssClass="errormessage"></asp:Label></td>
                </tr> 
            </table>
            
            <table bordercolor="#839abf" border="1" width="95%" align="center" cellpadding="0" cellspacing="0">
            <tr>
            <td>
             <table width="100%" border="0" class="" align="center" cellpadding="5" cellspacing="5">    
                 <tr>
                <td class="copy10grey"  align="right" width="20%" >
                    Customer:

                </td>
                <td width="30%" >
                    <asp:DropDownList ID="dpCompany" TabIndex="2"  runat="server" CssClass="copy10grey" Width="60%" AutoPostBack="false">									
                    </asp:DropDownList>                 

                </td>
                <td  width="1%">
                       &nbsp;
                    
                </td>
                <td class="copy10grey"  align="right" width="19%" >
                      Warehouse City:
                </td>
                <td width="30%" >
                     <asp:label ID="lblWhCity"  CssClass="copy10grey" runat="server"   ></asp:label>

                    </td>   
                </tr>
                 <tr>
                <td class="copy10grey"  align="right" width="20%" >
                                          Warehouse Location:

                </td>
                <td width="30%" >
                     <asp:label ID="lblWhLocation"  CssClass="copy10grey" runat="server"   ></asp:label>

                    
                </td>
                <td  width="1%">
                       &nbsp;
                    
                </td>
                <td class="copy10grey"  align="right" width="19%" >
                      Caregory:
                </td>
                <td width="30%" >
                    
                     <asp:label ID="lblCategoryName"  CssClass="copy10grey" runat="server"   ></asp:label>


                      
                </td>   
                </tr>
                
                 <tr>
                <td class="copy10grey"  align="right" width="20%" >
                    SKU:
                </td>
                <td width="30%" >    
                     <asp:label ID="lblSKU"  CssClass="copy10grey" runat="server"   ></asp:label>

              
                </td>
                <td  width="1%">
                       &nbsp;
                    
                </td>
                <td class="copy10grey"  align="right" width="19%" >
                   Product Name: 
                </td>
                <td width="30%" >
                      <asp:label ID="lblProductName"  CssClass="copy10grey" runat="server"   > </asp:label>

                </td>   
                </tr>
                 <tr>
                <td class="copy10grey"  align="right" width="20%" >
                    <b>New Location:</b>
                </td>
                <td width="30%" >    
                      <asp:DropDownList ID="ddlWhLocation" TabIndex="1" runat="server" Width="60%" CssClass="copy10grey">                         
                    </asp:DropDownList>

                </td>
                <td  width="1%">
                       &nbsp;
                    
                </td>
                <td class="copy10grey"  align="right" width="19%" >
                   Quantity: 
                </td>
                <td width="30%" >
                      <asp:TextBox ID="txtQuantity"  onkeypress="return isNumberKey(event);" onchange="return ValidateQuantity(this);"
                            TabIndex="2" CssClass="copy10grey" runat="server" Width="60%" MaxLength="4" ></asp:TextBox>
                    <asp:HiddenField ID="hdQty" runat="server" />
                </td>   
                </tr>
                  <tr valign="top">
                <td class="copy10grey"  align="right" width="20%" >
                    <b>Requested By:</b>
                </td>
                <td width="30%" >    
                      <asp:DropDownList ID="ddlUsers" TabIndex="11" runat="server" Width="60%" CssClass="copy10grey">                         
                    </asp:DropDownList>

                </td>
                <td  width="1%">
                       &nbsp;
                    
                </td>
                <td class="copy10grey"  align="right" width="19%" >
                    Commnet: 
                </td>
                <td width="30%" >

                     <asp:TextBox ID="txtComment" runat="server" CssClass="copy10grey" TextMode="MultiLine" Width="94%" Rows="3"></asp:TextBox>
                </td>   
                </tr>

                </td>
                </tr>
                    </Table>
                </td>
                </tr>
                 </table>
                     <br />
                     
     <table bordercolor="#839abf" border="1" width="95%" align="center" cellpadding="0" cellspacing="0" id="trUpload" runat="server" visible="true">
      <tr>
        <td align="center">
    
                                <table width="65%" cellpadding="5" cellspacing="5">
                        <tr>
                            <td class="copy10grey" align="right" width="40%">
                
                                    Upload ESN file: &nbsp;</td>
                                <td align="left" width="60%">
                                    <asp:FileUpload ID="flnUpload" runat="server" CssClass="txfield1" Width="50%" />

                                </td>
                                
                                
                                
                        </tr>
                        <tr>
                            <td class="copy10grey" align="right" width="40%">
                                    File format sample: &nbsp;
                                          </td>
                                <td class="copy10grey" align="left" width="60%">
                                   <b>ESN</b> <%--<asp:LinkButton ID="lnkButton" runat="server"   Text="Download file format" OnClick="btnCSV_Click"></asp:LinkButton>--%>
                    
                                </td>
                           
                        </tr>
                        <tr>
                            <td class="copy10grey" align="right" width="40%">
                                     &nbsp;
                                         </td>
                                <td class="copy10grey" align="left" width="60%">                                    
                                    <asp:Button ID="btnUploadValidate"  runat="server" Text="Validate Uploaded File" OnClick="btnUploadValidate_Click" CssClass="button" />       
                                    
                                    
                            </td>
                        </tr>
                    </table>

        </td>
      </tr>
    </table>
                <table width="100%" align="center" >
                <tr>
			        <td align="center" >
                       <%-- <div class="loadingcss" align="center" id="modalSending">
                        <img src="/Images/ajax-loaders.gif" alt=""  /> Loading...
                    </div>--%>
                       <%-- <asp:Button ID="btnValidate" runat="server" TabIndex="18"  CssClass="buybt" Text="   Validate   " onclick="btnValidate_Click"  />&nbsp;&nbsp;
                       --%> 
			            <asp:Button ID="btnSubmit" runat="server" TabIndex="18" Visible="false" CssClass="buybt" Text="   Submit   " onclick="btnSubmit_Click" OnClientClick="return confirm('Are you sure?');" />&nbsp;&nbsp;
                        <asp:Button ID="btnCancel" runat="server" TabIndex="19" CssClass="buybt" CausesValidation="false"  Text="   Cancel   " onclick="btnCancel_Click" />
                        
			        </td>
			    </tr>
			    </table> 
                
                <br />
                 <table cellSpacing="0" cellPadding="0" width="95%" align="center" runat="server" id="tblESN" visible="false" >
                     <tr>
                         <td align="right">
                             <asp:Label ID="lblCounts" runat="server"  CssClass="copy10grey"></asp:Label>
                         </td>
                     </tr>
                     <tr>
                         <td>
                             
                 <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="100%" align="center"  >
                        <tr bordercolor="#839abf">
                            <td>                                   
                                        
                                        <asp:Repeater ID="rptESN" runat="server">
                                            <HeaderTemplate>
                                            <table width="100%" align="center">
                                                <tr >
                                                    <td class="buttongrid" width="2%">
                                                        &nbsp;S.No.
                                                    </td>
                                                    <td class="buttongrid" width="10%">
                                                        &nbsp;ESN
                                                    </td>
                                                    <td class="buttongrid" width="10%">
                                                        &nbsp;Location
                                                    </td>
                                                    
                                                    <td class="buttongrid" width="15%">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                            
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                            <tr class="<%# Container.ItemIndex % 2 == 0 ? "alternaterow" : "" %>">
                                                
                                                <td class="copy10grey">
                                                        &nbsp;<%# Container.ItemIndex + 1 %></td>
                                
                                                <td class="copy10grey">
                                                        &nbsp;<%# Eval("ESN")%></td>
                                                <td class="copy10grey">
                                                        &nbsp;<%# Eval("Location")%></td>
                                                
                                                <td class="copy10grey">
                                                        &nbsp;<%# Eval("ErrorMessage")%></td>
                                
                                            </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                </table>    
                                            </FooterTemplate>
                                            </asp:Repeater>
                        
                            </td>
                                        </tr>
                                        </table>
                         </td>
                     </tr>
                 </table>
                
                   
            </ContentTemplate>
           <Triggers>
               <asp:PostBackTrigger ControlID="btnUploadValidate" />
           </Triggers>
        </asp:UpdatePanel>
    </form>
</body>
</html>
