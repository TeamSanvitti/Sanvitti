<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SKUDetail.aspx.cs" Inherits="avii.Product.SKUDetail" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>
<%@ Register TagPrefix="menu" TagName="Menu" Src="~/Controls/Header.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Product Detail</title>
</head>
<body bgcolor="#ffffff" leftmargin="0" rightmargin="0" topmargin="0" align="center" >
    <form id="form1" runat="server">
        <asp:ValidationSummary id="ValidationSummary1" runat="server" 
  ShowMessageBox="True" ShowSummary="False"></asp:ValidationSummary>


    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <table  cellspacing="0" cellpadding="0" border="0" align="center" width="100%">
        <tr>
            <td>
                <menu:menu ID="HeadAdmin2" runat="server" ></menu:menu>    
            </td>
        </tr>
        </table>
        <div id="winVP" style="z-index:1">
        
        
        <table  cellspacing="0" cellpadding="0" border="0" align="center" width="95%">
          
            <tr>
                
                <td>
                
                   <table width="100%" cellspacing="0" cellpadding="0" border="0"  >
                       <tr>
                       <br />
                            <td height="16"  class="buttonlabel" align="left"><strong>Manage Products</strong></td>
                       </tr>
                       <tr>
                        <td align="left">
                        <asp:Label ID="lblMsg" runat="server" CssClass="errormessage"></asp:Label>
                        
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                        &nbsp;
                        
                        </td>
                    </tr>
                   </table>
                   <div>
                   <table bordercolor="#839abf" border="1" width="100%" align="center" cellpadding="0" cellspacing="0">
                    <tr><td>

                    <table border="0" width="100%" class="box" align="center" cellpadding="3" cellspacing="3">
                    <tr valign="top">
                        <td class="copy10grey" align="left" width="40%">
                            Category Type: <span class="errormessage">*</span><br />
                            <asp:DropDownList ID="ddlCategoryType" runat="server" CssClass="copy10grey" 
                                    Width="80%" ></asp:DropDownList>
                        </td>    
                        <td class="copy10grey" align="left" width="30%">
                            Brand/Maker: <span class="errormessage">*</span><br />
                            <asp:DropDownList ID="ddlMaker" runat="server" CssClass="copy10grey" 
                                    Width="80%" ></asp:DropDownList> 
                           <%-- Product Condition:<span class="errormessage">*</span><br />
                            <asp:DropDownList ID="ddlitemType" Visible="false" runat="server" CssClass="copy10grey" 
                                    Width="80%">
                                </asp:DropDownList>--%>
                        </td>    
                        <td class="copy10grey" align="left" width="30%">
                             <br />
                            <asp:CheckBox ID="chkActive" Text="Active" Checked="true" runat="server" CssClass="copy10grey" />
                                &nbsp; 
                                <asp:CheckBox ID="chkShowunderCatalog" Checked="true" Text="Show under Catalog" runat="server" CssClass="copy10grey" />
                                &nbsp; 
                                <asp:CheckBox ID="chkAllowRMA" Text="Allow RMA" runat="server" CssClass="copy10grey" />
                        </td>    
                    </tr>
                    <tr valign="top">
                        <td class="copy10grey" align="left" width="40%" >
                                         Model Number: <span class="errormessage">*</span><br />    
                             
                        <asp:TextBox ID="txtModelNumber" CssClass="copy10grey" runat="server" 
                                    MaxLength="20" Width="80%"></asp:TextBox>
                              <asp:RequiredFieldValidator 
                                         id="rfvModel" runat="server" 
                                         ErrorMessage="Model Number is Required!" 
                                         ControlToValidate="txtModelNumber">
                                        </asp:RequiredFieldValidator>
                             <%-- Product Code:<span class="errormessage">*</span><br />--%>
                              <asp:TextBox ID="txtProductCode" Visible="false" CssClass="copy10grey" runat="server" 
                                    MaxLength="20" Width="80%"></asp:TextBox>
                                   <%-- <asp:RequiredFieldValidator 
                                         id="rqfPCode" runat="server" 
                                         ErrorMessage="Product Code is Required!" 
                                         ControlToValidate="txtProductCode">
                                        </asp:RequiredFieldValidator> --%>  
                        </td>
                        <td align="left" width="30%" class="copy10grey" >
                              UPC:<span class="errormessage">*</span><br />
                              <asp:TextBox ID="txtUPC" CssClass="copy10grey" runat="server" MaxLength="30" 
                                    Width="80%"></asp:TextBox>
                                <asp:RequiredFieldValidator 
                                         id="fqfUPC" runat="server" 
                                         ErrorMessage="Product UPC is Required!" 
                                         ControlToValidate="txtUPC">
                                        </asp:RequiredFieldValidator>  
                                 
                        </td>
                        <td class="copy10grey" align="left" width="30%" >
                         
  <br />
                                 <asp:CheckBox ID="chkKitted" Text="Kitted" runat="server" CssClass="copy10grey" />
&nbsp; 
                                <asp:CheckBox ID="chkSim" Text="Allow SIM" runat="server"  Visible="false"  CssClass="copy10grey" />
                            &nbsp; <asp:CheckBox ID="chkESN" Text="Allow ESN" Visible="false"  runat="server" CssClass="copy10grey" />
                               
                        </td>
                    </tr>
                    <tr valign="top">
                        <td class="copy10grey" align="left" width="40%" >
                              Name:<span class="errormessage">*</span><br />
                                <asp:HiddenField ID="hdnitemGUID" runat="server" />
                                <asp:HiddenField ID="hdnImage_Guid" runat="server" />
                                <asp:TextBox ID="txtProductName"  Width="80%" CssClass="copy10grey" 
                                    runat="server" MaxLength="100"></asp:TextBox>
                                     <asp:RequiredFieldValidator 
                                         id="rqfPName" runat="server" 
                                         ErrorMessage="Product Name is Required!" 
                                         ControlToValidate="txtProductName">
                                        </asp:RequiredFieldValidator>
                        </td>
                        <td align="left" width="30%" class="copy10grey" >
                            Weight(in Lbs): <br />
                         <asp:TextBox ID="txtWeight"  CssClass="copy10grey" MaxLength="6" runat="server" Width="80%" onkeypress="return ValidatePrice(event, this);"></asp:TextBox>
                
                        </td>
                        <td class="copy10grey" align="left" width="30%" rowspan="3">
                                    Carriers:<%--<span class="errormessage">*</span>--%><br />
                                    <asp:ListBox ID="lbTechnology" Height="100" SelectionMode="Multiple" runat="server" CssClass="copy10grey" 
                                Width="80%">
                                </asp:ListBox>
                        </td>
                    </tr>
                    <tr valign="top" style="display:none">
                        <td class="copy10grey" align="left" width="40%" >
                              Default Price($): <br />
                              <asp:TextBox ID="txtPrice"  Width="80%"  CssClass="copy10grey" runat="server"   onkeypress="return ValidatePrice(event, this);" 
                              ></asp:TextBox>
                        </td>
                        <td align="left" width="30%" class="copy10grey" >
                            Color:<span class="errormessage">*</span><br />
                                    <asp:TextBox ID="txtColor"  CssClass="copy10grey" runat="server" Width="80%"></asp:TextBox>
                                <%-- <asp:RequiredFieldValidator 
                                         id="RequiredFieldValidator1" runat="server" 
                                         ErrorMessage="Product Color is Required!" 
                                         ControlToValidate="txtColor">
                                        </asp:RequiredFieldValidator> --%>
                            
                        </td>
                        <td class="copy10grey" align="left" width="30%" >
                        
                        </td>
                    </tr>
                    <tr valign="top" style="display:none">
                        <td class="copy10grey" align="left" width="40%" >
                              DisplayPriority: <br />
                              <asp:DropDownList ID="ddlDisplayPriority" runat="server" CssClass="copy10grey" 
                                    Width="80%" >
                              <asp:ListItem Text="--Select DisplayPriority--" Value="0" ></asp:ListItem> 
                              <asp:ListItem Text="1" Value="1" ></asp:ListItem>      
                              <asp:ListItem Text="2" Value="2" ></asp:ListItem>      
                              <asp:ListItem Text="3" Value="3" ></asp:ListItem>      
                              <asp:ListItem Text="4" Value="4" ></asp:ListItem>      
                              <asp:ListItem Text="5" Value="5" ></asp:ListItem>      
                              <asp:ListItem Text="6" Value="6" ></asp:ListItem>      
                              <asp:ListItem Text="7" Value="7" ></asp:ListItem>      
                              <asp:ListItem Text="8" Value="8" ></asp:ListItem>      
                              <asp:ListItem Text="9" Value="9" ></asp:ListItem>      
                              <asp:ListItem Text="10" Value="10" ></asp:ListItem>      
                              <asp:ListItem Text="11" Value="11" ></asp:ListItem>      
                              <asp:ListItem Text="12" Value="12" ></asp:ListItem>      
                              <asp:ListItem Text="13" Value="13" ></asp:ListItem>      
                              <asp:ListItem Text="14" Value="14" ></asp:ListItem>      
                              <asp:ListItem Text="15" Value="15" ></asp:ListItem>      
                              <asp:ListItem Text="16" Value="16" ></asp:ListItem>      
                              <asp:ListItem Text="17" Value="17" ></asp:ListItem>      
                              <asp:ListItem Text="18" Value="18" ></asp:ListItem>      
                              <asp:ListItem Text="19" Value="19" ></asp:ListItem>      
                              <asp:ListItem Text="20" Value="20" ></asp:ListItem>      
                              </asp:DropDownList> 
                        </td>
                        <td align="left" width="30%" class="copy10grey" >
                         
                                        Document: <br /> <asp:FileUpload ID="fuItemDoc" runat="server" />
                        </td>
                        <td class="copy10grey" align="left" width="30%" >
                       
                        </td>
                    </tr>
                    <tr valign="top" style="display:none">
                        <td class="copy10grey" align="left" width="40%" >
                              SimCardType: <br />
                              <asp:DropDownList ID="ddlSimCardType" runat="server" CssClass="copy10grey" 
                                    Width="80%" >  
                              </asp:DropDownList> 
                        </td>
                        <td align="left" width="30%" class="copy10grey" >
                         
                                        SpintorLockType: <br />
                                           <asp:DropDownList ID="ddlSpintorLockType" runat="server" CssClass="copy10grey" 
                                    Width="80%" >  
                              </asp:DropDownList> 
                        </td>
                        <td class="copy10grey" align="left" width="30%" >
                      
                        </td>
                    </tr>
                    <tr valign="top" style="display:none">
                        <td class="copy10grey" align="left" width="40%" >
                              OperationSystem: <br />
                               <asp:DropDownList ID="ddlOS" runat="server" CssClass="copy10grey" 
                                    Width="80%" >  
                                    <%--<asp:ListItem Text="--Select OperationSystem--" Value="0" ></asp:ListItem> 
                              <asp:ListItem Text="1"" Value="1" ></asp:ListItem>      
                              <asp:ListItem Text="2"" Value="2" ></asp:ListItem>      
                              <asp:ListItem Text="3" Value="3" ></asp:ListItem>      
                              <asp:ListItem Text="4" Value="4" ></asp:ListItem>      
                              <asp:ListItem Text="5" Value="5" ></asp:ListItem>      
                              <asp:ListItem Text="6" Value="6" ></asp:ListItem>      
                              <asp:ListItem Text="7" Value="7" ></asp:ListItem>      
                              <asp:ListItem Text="8" Value="8" ></asp:ListItem>      
                              <asp:ListItem Text="9" Value="9" ></asp:ListItem>  --%>
                              </asp:DropDownList> 
                        </td>
                        <td align="left" width="30%" class="copy10grey" >
                         
                                        ScreenSize: <br />
                                    <asp:DropDownList ID="ddlScreenSize" runat="server" CssClass="copy10grey" Width="80%" >  
                                    <asp:ListItem Text="--Select ScreenSize--" Value="" ></asp:ListItem> 
                                    <asp:ListItem Text="1''" Value="1''" ></asp:ListItem>      
                                    <asp:ListItem Text="1.5''" Value="1.5''" ></asp:ListItem>      
                                    <asp:ListItem Text="2''" Value="2''" ></asp:ListItem>      
                                    <asp:ListItem Text="2.5''" Value="2.5''" ></asp:ListItem>    
                                    <asp:ListItem Text="3''" Value="3''" ></asp:ListItem>   
                                    <asp:ListItem Text="3.5''" Value="3.5''" ></asp:ListItem>     
                                  <asp:ListItem Text="4''" Value="4''" ></asp:ListItem>      
                                  <asp:ListItem Text="4.5''" Value="4.5''" ></asp:ListItem>      
                                  <asp:ListItem Text="4.6''" Value="4.6''" ></asp:ListItem>      
                                  <asp:ListItem Text="5''" Value="5''" ></asp:ListItem>      
                                  <asp:ListItem Text="5.2''" Value="5.2''" ></asp:ListItem>      
                                  <asp:ListItem Text="5.5''" Value="5.5''" ></asp:ListItem>      
                                  <asp:ListItem Text="6''" Value="6''" ></asp:ListItem>      
                                  <asp:ListItem Text="6.5''" Value="6.5''" ></asp:ListItem>      
                                  <asp:ListItem Text="7''" Value="7''" ></asp:ListItem>      
                                  <asp:ListItem Text="7.5''" Value="7.5''" ></asp:ListItem>      
                                  <%--<asp:ListItem Text="8" Value="8" ></asp:ListItem>      
                                  <asp:ListItem Text="9" Value="9" ></asp:ListItem>  --%>
                              </asp:DropDownList> 
                        </td>
                        <td class="copy10grey" align="left" width="30%" >
                        <%-- Weight: <br />
                         <asp:TextBox ID="txtWeight"  CssClass="copy10grey" runat="server" Width="80%"></asp:TextBox>--%>
                        </td>
                    </tr>
                     <tr valign="top">
                        <td class="copy10grey" align="left"  colspan="3">
                        Short Description: <br />
                        <asp:TextBox ID="txtDescription" Height="40px" Width="70%" TextMode="MultiLine" 
                                     CssClass="copy10grey" runat="server" onkeypress="return isMaxLength(this,500);"></asp:TextBox>
                        

                        </td>
                    </tr>
                       <%-- <tr valign="top"><td>&nbsp;</td></tr>--%>
                       <tr valign="top">
                        <td class="copy10grey" align="left"  colspan="3">
                        Full Desc.:<br />
                        <asp:TextBox ID="txtFullDesc" Height="48px" Width="94%" TextMode="MultiLine" 
                                    CssClass="copy10grey" runat="server"></asp:TextBox>

                        </td>
                    </tr>
                    <tr valign="top" style="line-height:10px">
                            <td align="left" class="copy10grey" >
                            &nbsp;
                            </td>
                            <td align="left">
                               
                            </td>
                            <td>
                            
                            </td>
                            
                        </tr>
                    </table>
                   <%-- <table border="0" width="100%" class="box" align="center" cellpadding="2" cellspacing="2">
                        <tr valign="top">
                            <td class="copy10grey" align="left" width="15%" >
                                
                            </td>
                            <td align="left" width="35%" >
                                
                            </td>
                            <td class="copy10grey" align="left" width="15%" ></td>
                            <td align="left" width="35%" >
                                                               
                            </td>                            
                        </tr>
                        <tr valign="top">
                            <td align="left" class="copy10grey">
                            </td>
                            <td align="left" >
                                
                            </td>
                            <td align="left" class="copy10grey">
                            </td>
                            <td align="left" rowspan="2">
                                
                                
                            </td>
                            
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>

                        </tr>
                        
                        <tr>
                            <td style="width:85px" class="copy10grey" align="left" >
                                
                            </td>
                            <td align="left" >
                                

                            </td>
                            <td class="copy10grey" align="left" >
                               
                            </td>
                            <td align="left" >
                                
                            </td>
                        </tr>
                        <tr>
                            <td class="copy10grey" align="left" >
                               
                            </td>
                            <td align="left" >
                                
                            </td>
                            <td class="copy10grey" align="left" >
                               
                            </td>
                            <td align="left" >
                                
                            </td>
                        </tr>
                        <tr valign="top">
                            <td class="copy10grey" align="left" >
                               
                            </td>
                            <td align="left" >
                                 
                            </td>
                            <td class="copy10grey" align="left" >
                            
                            </td>
                            <td align="left" >
                            
                                
                            </td>
                        </tr>
                        
                        
                          
                        <tr>
                            <td align="left" class="copy10grey"></td>
                            <td align="left" >
                                 
                            </td>
                            <td></td>
                            <td align="left" >
                               
                            </td>
                        </tr>
                        
                        <tr>
                            <td colspan="4" style="height:18px"></td>
                        </tr>
                    </table>--%>
                    </td>
                    </tr>
                    </table>
                   </div>  
                   
                   
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
           
            <tr>
                <td style="height:24">
                    &nbsp;
                </td>
            </tr>
            <tr>
                
                <td align="center">
                   
                                <asp:button ID="btnCancel" runat="server" Text="Back To Search"  onclick="btnCancel_Click" 
                                     CssClass="buybt"  />
                    
                </td>
            </tr>
            <tr>
                <td>

                </td>
            </tr>
        </table>

                        <br />
        <br />

                    <foot:MenuFooter ID="footer" runat="server"></foot:MenuFooter>
        </div>
    </div>
    <script type="text/javascript">
    //SYNTAX: ddtabmenu.definemenu("tab_menu_id", integer OR "auto")
     function formatParentCatDropDown(objddl) {

            for (i = 0; i < objddl.options.length; i++) {
                objddl.options[i].innerHTML = objddl.options[i].innerHTML.replace(/&amp;/g, '&');
            }
        }
       
    formatParentCatDropDown(document.getElementById("<%=ddlCategoryType.ClientID%>"));
    
    
    </script>
    </form>
</body>
</html>
