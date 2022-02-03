<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BlockESNs.aspx.cs" Inherits="avii.ESNBlock.BlockESNs" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>
<%@ Register TagPrefix="menu" TagName="Menu" Src="~/Controls/Header.ascx" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Quarantine</title>
    <script type="text/javascript">
        function Validate(flag) {
            if (flag == '1') {
                var company = document.getElementById("<% =dpCompany.ClientID %>");
                if (company.selectedIndex == 0) {
                    alert('Customer is required!');
                    return false;
                }
                var category = document.getElementById("<% =ddlSKU.ClientID %>");
                if (category.selectedIndex == 0) {
                    alert('SKU is required!');
                    return false;
                }
                var oFile = document.getElementById("flnUpload").files[0]; // <input type="file" id="fileUpload" accept=".jpg,.png,.gif,.jpeg"/>

                if (oFile.size > 3145728) // 3 mb for bytes.
                {
                    alert("File size must under 3 MB!");
                    return false;
                }
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
                <td>&nbsp;Quarantine</td></tr>
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
                <tr valign="top" id="trCustomer" runat="server">
                <td class="copy10grey" align="right" width="15%">
                    <strong>Customer Name:</strong>
                </td>
                <td width="35%">
                    <asp:DropDownList ID="dpCompany" CssClass="copy10grey" runat="server" Width="80%"
                                    OnSelectedIndexChanged="dpCompany_SelectedIndexChanged"  
                                    AutoPostBack="true">
									</asp:DropDownList>
                
                </td>
                <td  width="1%">
                    &nbsp;
                </td>
                <td class="copy10grey" align="right" width="15%">
                   <%--<asp:Label ID="lblStatus" runat="server" CssClass="copy10grey"></asp:Label>--%>
                </td>
                <td width="35%">
                   
                          
<%--                    <asp:DropDownList ID="ddlStatus" CssClass="copy10grey" runat="server" Width="80%">
	                </asp:DropDownList>  --%>            
        
                </td>   
                
                    
                </tr>
            
                    <tr valign="top">
                    <td class="copy10grey" align="right" width="15%">
                         <strong>SKU#:</strong>
                    </td>
                    <td width="35%">
                    
                    <asp:DropDownList ID="ddlSKU" CssClass="copy10grey" runat="server" Width="80%">
	                </asp:DropDownList>           
                
                    </td>
                    <td  width="1%">
                        &nbsp;
                    </td>
                    <td class="copy10grey" align="right" width="15%">
                   <strong>Requested By:</strong>
                    </td>
                    <td width="35%">                                                    
                        
                    <asp:DropDownList ID="ddlUser" CssClass="copy10grey" runat="server" Width="80%">
	                </asp:DropDownList>
                    </td>   
                
                    
                    </tr>
                    
                    <tr valign="top">
                        <td class="copy10grey" align="right" width="15%">
                            Comments:
                        </td>
                        <td colspan="4">
                             <asp:TextBox ID="txtComments" runat="server" TextMode="MultiLine"  Height="70px" CssClass="copy10grey"   Width="92%"></asp:TextBox>
                       
                        </td>
                    </tr>
               
            </table>
              
   
         </td>
         </tr>
         </table>       
         <br />
             <asp:Panel ID="pnlUpload" runat="server">
        <table bordercolor="#839abf" border="1" width="100%" align="center" cellpadding="0" cellspacing="0">
          <tr>
            <td>
             
         <table width="100%" border="0" class="box" align="center" cellpadding="5" cellspacing="5">
         <tr>
              <tr>
                <td  class="copy10grey" align="right" >
                    Upload ESN file: &nbsp;</td>
                <td align="left" >
                    <asp:FileUpload ID="flnUpload" runat="server" CssClass="txfield1" Width="55%" /></td>
            </tr>
            <tr  >
                <td class="copy10grey" align="right">
                    File format sample: &nbsp;
                            </td>
                <td class="copy10grey" align="left">
                    <b>ESN</b>

                </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <hr />
                    </td>
                </tr>         
             <tr>
                 <td colspan="2" align="center">
                       <asp:Button ID="btnValidate"  Width="190px" CssClass="button" runat="server" OnClick="btnValidate_Click" Text="Validate Uploaded file" OnClientClick="return Validate(1);"  />
                       <asp:Button ID="btnCancel2"  Width="190px" CssClass="button" runat="server" OnClick="btnCancel_Click" Text="Cancel"   />

                 </td>
             </tr>
         </tr>
        </table>
        </td>
        </tr>
        </table>
        </asp:Panel>
          <asp:Panel ID="pnlESN" runat="server" Visible="false">
              <br />
          <table align="center" style="text-align:left" width="80%">
          <tr>
              <td align="center">
                  <asp:Repeater ID="rptESN" runat="server"  >
                    <HeaderTemplate>
                    <table bordercolor="#839abf" border="1" width="100%" cellpadding="1" cellspacing="1">
                    <tr>
                        <td class="buttongrid"  width="1%" >
                            S.No.
                        </td>
                        <td class="buttongrid"  width="50%">
                            ESN
                        </td>
                        <td class="buttongrid"  >
                            &nbsp;
                        </td>
                    </tr>
                    </HeaderTemplate>
                    <ItemTemplate>    
                        <tr valign="bottom"  class="<%# Container.ItemIndex % 2 == 0 ? "alternaterow" : "" %>">
                            <td class="copy10grey"  >
                            <%# Container.ItemIndex +  1 %>
                            </td>
                            <td  class="copy10grey"  >
                                <%# Eval("ESN")%>    
                                
                            </td>
                            <td class="errormessage">
                                <%# Eval("ErrorMessage")%>    
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                    </table>
                    </FooterTemplate>
                    </asp:Repeater>
                               
              </td>
          </tr>
          <tr>

                    <td  align="center">
                    
                            <table width="100%" cellpadding="0" cellspacing="0">
                                 <tr>
                                   
                                    <td  align="center">
                                
                                        <asp:Button ID="btnSubmit" CssClass="button" runat="server" OnClick="btnSubmit_Click" Text="Submit" />
                               
                                        &nbsp;<asp:Button ID="btnCancel" Visible="true" runat="server"  CssClass="button" Text="Cancel" OnClick="btnCancel_Click" />

                                        </td>
                                </tr>
                        
                            </table>
                        
                    </td>
                    </tr>
                </table>
              
          </asp:Panel>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnValidate" />
            </Triggers>
            
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
