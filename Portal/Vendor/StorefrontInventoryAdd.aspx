<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StorefrontInventoryAdd.aspx.cs" Inherits="avii.Vendor.StorefrontInventoryAdd" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="/Controls/Footer.ascx" %>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="/Controls/Header.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"> 
    <title>Store Front Products</title>
    <script>
        function formatParentCatDropDown(objddl) {

            for (i = 0; i < objddl.options.length; i++) {
                objddl.options[i].innerHTML = objddl.options[i].innerHTML.replace(/&amp;/g, '&');
            }
        }
    </script>
</head>
<body  bgColor="#ffffff" leftmargin="0" rightmargin="0" topmargin="0">
    <form id="form1" runat="server">
       <%-- <asp:ValidationSummary id="ValidationSummary1" runat="server" 
   ShowMessageBox="True" ShowSummary="False"></asp:ValidationSummary>--%>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
	<table cellSpacing="0" cellPadding="0"  border="0" align="center" width="100%">
		<tr>
			<td>
			<head:menuheader id="HeadAdmin" runat="server"></head:menuheader>
			</td>
		</tr>
     </table>
    <table  cellSpacing="1" cellPadding="1" width="95%" align="center" >
        <tr>
		    <td colSpan="6" bgcolor="#dee7f6" class="buttonlabel">&nbsp;&nbsp;Store Front Products
		    </td>
        </tr>
    </table> 
     <table cellspacing="0" cellpadding="0" border="0" align="center" width="100%">
        <tr>
            <td >
     <asp:UpdatePanel ID="UpdatePanel1"  runat="server" UpdateMode="Conditional" >
        <ContentTemplate>   
        <table cellSpacing="0" cellPadding="0" align="center" width="95%" border="0">
    	<tr>                    
            <td colspan="2">
                <asp:Label ID="lblMsg" runat="server" Width="100%" CssClass="errormessage"></asp:Label>
            </td>
        </tr>
        </table>
        <table cellSpacing="0" cellPadding="0" align="center" width="95%" border="0">    
         <tr>
                <td align="center">
                 <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="100%">
                    <tr>
                    <td>
			            <table id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
                        <tr>
                            <td colspan="5"> 
                                <asp:Panel ID="pnlSearch" runat="server" DefaultButton="btnSubmit">
                        <table cellSpacing="5" cellPadding="5" width="100%" border="0">  
                            <tr>
                                <td  class="copy10grey" align="right" width="15%">
                                        <strong> Store front: </strong> &nbsp;</td>
                                <td align="left"  width="35%">
                                   <asp:DropDownList ID="ddlStorefront" CssClass="copy10grey" runat="server" Width="80%">
				                    </asp:DropDownList>
                                </td>
                                <td  width="1%">
                                    &nbsp;
                                </td>
                                <td class="copy10grey" align="right" width="15%">
                                    <strong> Customer: </strong>
                                </td>
                                <td align="left">
                                     <asp:DropDownList ID="dpCompany" CssClass="copy10grey" runat="server" 
                                      OnSelectedIndexChanged="dpCompany_SelectedIndexChanged" AutoPostBack="true"   Width="80%">
				                    </asp:DropDownList>

                                    
                                </td>
                            </tr>
                            <tr>
                                <td  class="copy10grey" align="right" width="15%">
                                        <strong> Category Type: </strong> &nbsp;</td>
                                <td align="left"  width="35%">
                                   <asp:DropDownList ID="ddlCategoryType" runat="server" CssClass="copy10grey"  
                                    Width="80%" ></asp:DropDownList>
                                </td>
                                <td  width="1%">
                                    &nbsp;
                                </td>
                                <td class="copy10grey" align="right" width="15%">
                                    <strong> Brand/Maker: </strong>
                                </td>
                                <td align="left">
                                     <asp:DropDownList ID="ddlMaker" runat="server" CssClass="copy10grey" 
                                    Width="80%" ></asp:DropDownList> 
                                </td>
                            </tr>
                         <tr valign="top">
                            <td class="copy10grey" align="right" width="10%">
                                <b> SKU:</b>
                            </td>
                            <td width="35%">
                                <asp:TextBox ID="txtSKU" CssClass="copy10grey" runat="server" 
                                    MaxLength="20" Width="80%"></asp:TextBox>
                                <%--<asp:RequiredFieldValidator 
                                         id="rfvModel" runat="server" 
                                         ErrorMessage="SKU is Required!" 
                                         ControlToValidate="txtSKU"></asp:RequiredFieldValidator>--%>
                            </td>
                            <td  width="1%">
                                &nbsp;
                            </td>
                            <td class="copy10grey" align="right" width="10%">
                               <b> Model Number:</b>
                            </td>
                            <td >
                                    <asp:TextBox ID="txtModelNumber" CssClass="copy10grey" runat="server" 
                                    MaxLength="40" Width="80%"></asp:TextBox>                             
        
                            </td>   
                
                    
                            </tr>
                            <tr valign="top">
                            <td class="copy10grey" align="right" width="10%">
                                <b> UPC:</b>
                            </td>
                            <td width="35%">
                                <asp:TextBox ID="txtUPC" CssClass="copy10grey" runat="server" MaxLength="30" 
                                    Width="80%"></asp:TextBox>
                                <%--<asp:RequiredFieldValidator 
                                         id="fqfUPC" runat="server" 
                                         ErrorMessage="Product UPC is Required!" 
                                         ControlToValidate="txtUPC">
                                        </asp:RequiredFieldValidator>  --%>
                            </td>
                            <td  width="1%">
                                &nbsp;
                            </td>
                            <td class="copy10grey" align="right" width="10%">
                                Serial #:
                            </td>
                            <td >
                                    <asp:TextBox ID="txtSerialNo" CssClass="copy10grey" runat="server" 
                                    MaxLength="40" Width="80%"></asp:TextBox>                             
        
                            </td>   
                
                    
                            </tr>
                            <tr valign="top">
                            <td class="copy10grey" align="right" width="10%">
                                <b> Title:</b>
                            </td>
                            <td width="35%" colspan="4">
                                <asp:TextBox ID="txtTitle" CssClass="copy10grey" runat="server" MaxLength="100" 
                                    Width="80%"></asp:TextBox>
                               <%-- <asp:RequiredFieldValidator 
                                         id="rfvTitle" runat="server" 
                                         ErrorMessage="Title is Required!" 
                                         ControlToValidate="txtTitle">
                                        </asp:RequiredFieldValidator>  --%>
                            </td>
                              
                
                    
                            </tr>
                            <tr valign="top">
                            <td class="copy10grey" align="right" width="10%">
                                <b> Description:</b>
                            </td>
                            <td width="35%" colspan="4">
                                <asp:TextBox ID="txtDesc" CssClass="copy10grey" TextMode="MultiLine" Rows="4" runat="server" MaxLength="500" 
                                    Width="80%"></asp:TextBox>                               
                            </td>
                              
                
                    
                            </tr>
                            <tr valign="top">
                            <td class="copy10grey" align="right" width="10%">
                                <b> Opening Stock:</b>
                            </td>
                            <td width="35%">
                                 <asp:TextBox ID="txtOpeningStock" CssClass="copy10grey" runat="server" MaxLength="5" 
                                    Width="80%"></asp:TextBox> 
                                
                            </td>
                            <td  width="1%">
                                &nbsp;
                            </td>
                            <td class="copy10grey" align="right" width="10%">
                               <b> Country/Region of Manufacture:</b>
                            </td>
                            <td>
                                 <asp:TextBox ID="txtRegion" CssClass="copy10grey" runat="server" MaxLength="200" 
                                    Width="80%"></asp:TextBox>                            
        
                            </td>
                            </tr>
                            <tr>
                                <td  class="copy10grey" align="right" width="15%">
                                        <strong> Locale: </strong> &nbsp;</td>
                                <td align="left"  width="35%">
                                   <asp:DropDownList ID="ddlLocale" runat="server" CssClass="copy10grey" 
                                    Width="80%" ></asp:DropDownList>
                                </td>
                                <td  width="1%">
                                    &nbsp;
                                </td>
                                <td class="copy10grey" align="right" width="15%">
                                    <strong> Condition: </strong>
                                </td>
                                <td align="left">
                                     <asp:DropDownList ID="ddlCondition" runat="server" CssClass="copy10grey" 
                                    Width="80%" ></asp:DropDownList> 
                                </td>
                            </tr>
                            <tr valign="top">
                            <td class="copy10grey" align="right" width="10%">
                                <strong> Condition Desc:</strong>
                            </td>
                            <td width="35%" colspan="4">
                                <asp:TextBox ID="txtConditionDesc" CssClass="copy10grey" TextMode="MultiLine" Rows="2" runat="server" MaxLength="500" 
                                    Width="80%"></asp:TextBox>                               
                            </td>
                            </tr>
                            <tr valign="top">
                            <td class="copy10grey" align="right" width="10%">
                                 Package Weight:
                            </td>
                            <td width="35%">
                                 <asp:TextBox ID="txtWeight" CssClass="copy10grey" runat="server" MaxLength="5" 
                                    Width="80%"></asp:TextBox> 
                                
                            </td>
                            <td  width="1%">
                                &nbsp;
                            </td>
                            <td class="copy10grey" align="right" width="10%">
                               Dimension:
                            </td>
                            <td >
                                     <asp:TextBox ID="txtDimension" CssClass="copy10grey" runat="server" MaxLength="100" 
                                    Width="80%"></asp:TextBox>                            
        
                            </td>   
                
                    
                            </tr>
                            <tr valign="top">
                            <td class="copy10grey" align="right" width="10%">
                                 Warehouse code:
                            </td>
                            <td width="35%">
                                  <asp:DropDownList ID="ddlWarehousecode" runat="server" CssClass="copy10grey" 
                                    Width="80%" ></asp:DropDownList>  
                                
                            </td>
                            <td  width="1%">
                                &nbsp;
                            </td>
                            <td class="copy10grey" align="right" width="10%">
                               Location:
                            </td>
                            <td >
                                     <asp:TextBox ID="txtLocation" CssClass="copy10grey" runat="server" MaxLength="100" 
                                    Width="80%"></asp:TextBox>                            
        
                            </td>   
                
                    
                            </tr>
                            <tr valign="top">
                            <td class="copy10grey" align="right" width="10%">
                                 Minimum Stock:
                            </td>
                            <td width="35%">
                                  <asp:TextBox ID="txtMinStock" CssClass="copy10grey" runat="server" MaxLength="4" 
                                    Width="80%"></asp:TextBox>     
                                
                            </td>
                            <td  width="1%">
                                &nbsp;
                            </td>
                            <td class="copy10grey" align="right" width="10%">
                               Maximum Stock:
                            </td>
                            <td >
                                     <asp:TextBox ID="txtMaxStock" CssClass="copy10grey" runat="server" MaxLength="6" 
                                    Width="80%"></asp:TextBox>                            
        
                            </td>   
                
                    
                            </tr>
                        <%--<tr valign="top" >                             
                            <td class="copy10grey" align="right" width="10%">
                                <b> Status: </b>
                            </td>
                            <td width="35%">
                                    <asp:DropDownList ID="ddlStatus" runat="server" CssClass="copy10grey" Width="70%">
                                       
                                    </asp:DropDownList>    
         
                            </td>
                            <td  width="1%">
                                &nbsp;
                            </td>
                                <td  class="copy10grey" align="right" width="10%">
                                   </td>
                                <td align="left"  >                                  
                                 
                                </td>
                                
                            </tr>--%>
                            </table>
                                    </asp:Panel>
                            </td>
                        </tr>
                        
                    </table>
                </td>
                </tr>
                           
                </table>
                  <br />

                    <table cellSpacing="0" cellPadding="0" align="center" width="95%" border="0">    
                        
                        <tr>
                            <td  align="center"  colspan="5">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="button" OnClick="btnSubmit_Click" OnClientClick="return validate();" CausesValidation="false"/>
            
                                &nbsp;
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="button" OnClick="btnCancel_Click" CausesValidation="false"/>
                   
                            </td>
                        </tr>
                    </table>        
                        


                    </td>
                </tr>

            
       
    </table>
    </ContentTemplate>
         </asp:UpdatePanel>
                
    <asp:UpdatePanel ID="upnlJsRunner" UpdateMode="Always" runat="server">
			<ContentTemplate>
				<asp:PlaceHolder ID="phrJsRunner" runat="server">
                
                </asp:PlaceHolder>
			</ContentTemplate>
		</asp:UpdatePanel>
         </td>
         </tr>
         </table>
    <br />
        <table cellspacing="0" cellpadding="0" border="0" align="center" width="100%">
        <tr>
            <td >
                <foot:MenuFooter ID="footer2" runat="server"></foot:MenuFooter>
            </td>
         </tr>
         </table>

        <script type="text/javascript">
  
    formatParentCatDropDown(document.getElementById("<%=ddlCategoryType.ClientID%>"));
    
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            if (prm != null) {
                prm.add_endRequest(function (sender, e) {
                    if (sender._postBackSettings.panelsToUpdate != null) {
                        formatParentCatDropDown(document.getElementById("<%=ddlCategoryType.ClientID %>"));
            }
        });
            };
    
        </script>
    </form>
</body>
</html>
