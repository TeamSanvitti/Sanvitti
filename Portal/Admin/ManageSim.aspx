<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageSim.aspx.cs" Inherits="avii.Admin.ManageSim" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="/Controls/Footer.ascx" %>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="/Controls/Header.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Lan Global inc. Inc. - Assign SIM</title>
    <link href="../aerostyle.css" type="text/css" rel="stylesheet">
    <script type="text/javascript">
        function Validate(flag) {
            if (flag == '1' || flag == '2') {
                var company = document.getElementById("<% =dpCompany.ClientID %>");
                if (company.selectedIndex == 0) {
                    alert('Customer is required!');
                    return false;

                }
            }
            if (flag == '2') {
                var status = document.getElementById("<% =ddlSKU.ClientID %>");
                if (status.selectedIndex == 0) {
                    //alert('Status is required!');
                    //return false;

                }
            }



        }
        function IsValidate(obj) {
            if (obj.checked) {
                var isTrue = confirm('This will remove sim from repository. Do you want to continue?');
                obj.checked = isTrue;
            }
        }
		
    </script>


	
</head>
<body bgColor="#ffffff" leftmargin="0" rightmargin="0" topmargin="0">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
	<table cellSpacing="0" cellPadding="0"  border="0" align="center" width="100%">
		<tr>
			<td>
			<head:menuheader id="HeadAdmin" runat="server"></head:menuheader>
			</td>
		</tr>
     </table>
    <br />
    <table  cellSpacing="1" cellPadding="1" width="100%">
        <tr>
		    <td colSpan="6" bgcolor="#dee7f6" class="button">&nbsp;&nbsp;Assign SIM
		    </td>
        </tr>

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
                        - Two types of SIM inventory can be uploaded:<br />&nbsp;
	                        &nbsp;&nbsp;&nbsp;&nbsp;* SIM no ESN association.<br />&nbsp;
	                        &nbsp;&nbsp;&nbsp;&nbsp;* SIM with ESN association.<br />&nbsp;
                        - Products marked with "SIM" only displayed in SKU dropdown.<br />&nbsp;
                        - ESN should exists in "ESN Repository" and should not be in use.<br />&nbsp;
                        - Only unused SIM can be deleted from "SIM Repository".<br />&nbsp;
                        - SIM once assigned to SKU can not be changed once used.&nbsp;
                        <%--- Create SIM(s) to Customer assigned SKU#(s).<br />&nbsp;
	                    - Upload file should be less than 2 MB. <br />&nbsp;--%>
                        
                        
                        </td></tr>
                    </table>

                 <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="90%">
                    <tr>
                    <td>
			            <table  cellSpacing="5" cellPadding="5" width="100%" border="0">
                            <tr>
                                <td  class="copy10grey" align="right" width="42%" >
                                    Customer: &nbsp;</td>
                                <td align="left" >
                                    <asp:DropDownList ID="dpCompany" CssClass="copy10grey" runat="server" Width="55%"
                                    OnSelectedIndexChanged="dpCompany_SelectedIndexChanged"  
                                    AutoPostBack="true">
									</asp:DropDownList>
                            </tr>
                            <tr runat="server" id="trSKU">
                                <td class="copy10grey"  width="42%" align="right">
                                    SKU: &nbsp;
                                </td>
                                
                                <td  align="left">
                                <asp:DropDownList ID="ddlSKU" runat="server" class="copy10grey">
                                </asp:DropDownList>
                                            

                                </td>
                            </tr>
                            
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
                                  <b> SIM</b>,ESN

                                </td>
                             </tr>
<tr  valign="top">
                                <td class="copy10grey" align="right">
                                    Comment: &nbsp;
                                         </td>
                                <td class="copy10grey" align="left">
                                    <asp:TextBox ID="txtComment" runat="server" CssClass="copy10grey" Width="70%" Height="50px" TextMode="MultiLine"></asp:TextBox>

                                </td>
                             </tr>
 
                             <tr  >
                             <td class="copy10grey" align="right">
                            &nbsp;
                            </td>

                            <td class="copy10grey" align="left">
                                <asp:CheckBox ID="chkDelete" CssClass="copy10grey" Text="Remove SIM" runat="server" onclick="return IsValidate(this);" />

                            </td>
                            </tr>
                
                             
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
                                
                                
                                            <asp:Button ID="btnSubmit2" Visible="false" Width="190px"  CssClass="button" runat="server" OnClick="btnSubmit_Click" Text="Submit" OnClientClick="return Validate(2);" />

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
                                <strong>   <asp:Label ID="lblCount" CssClass="copy10grey" runat="server" ></asp:Label> &nbsp;&nbsp;</strong> 
                                </td>
                             </tr>
                             <tr>
                                <td colspan="2" align="center">
                                
                                <asp:GridView ID="gvSIM" runat="server" Width="100%" GridLines="Both"   AutoGenerateColumns="false"
                                    OnPageIndexChanging="gridView_PageIndexChanging" PageSize="100" AllowPaging="true" 
                                    >
                                        <RowStyle BackColor="Gainsboro" />
                                        <AlternatingRowStyle BackColor="white" />
                                        <HeaderStyle  CssClass="button" ForeColor="white"/>
                                        <FooterStyle CssClass="white"  />
                                        <PagerStyle ForeColor="#636363" CssClass="copy10grey" HorizontalAlign="Left" />
                                        <Columns>
                                            
                                            
                                            <asp:TemplateField HeaderText="SIM#"   ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="30%">
                                                <ItemTemplate>
                                                <table  cellpadding="0" cellspacing="0" style="border:0; width:100%; height:100%; background:<%# Convert.ToString(Eval("SIM")) == "" ?  "Red": ""%>">
                                                <tr style="border:0; width:100%; height:100%; background:<%# Convert.ToString(Eval("SIM")) == "" ?  "Red": ""%>">
                                                    <td>
                                                        <%#Eval("SIM")%>&nbsp;
                                                    </td>
                                                </tr>
                                                </table>
                                                </ItemTemplate>
                                            </asp:TemplateField> 
                                            <asp:TemplateField HeaderText="ESN"   ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="30%">
                                                <ItemTemplate>
                                                    <%#Eval("esn")%>
                                                </ItemTemplate>
                                            </asp:TemplateField> 
                                            <asp:TemplateField HeaderText="SKU"   ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="30%">
                                                <ItemTemplate>
                                                    <%#Eval("sku")%>
                                                </ItemTemplate>
                                            </asp:TemplateField> 
                                            
                                        </Columns>
                                    </asp:GridView>
                                    

                                </td>
                            </tr>
                            </table>
                            </td></tr>                            
                            
                            <tr><td colspan="2">
                            <hr style="width:100%" />
                            
                            </td></tr>                            
                             <tr>
                                <td colspan="2" align="center">
                                
                                <asp:Button ID="btnUpload"  Width="190px" CssClass="button" runat="server" OnClick="btnValidateUploadedFile_Click" Text="Validate Uploaded file" OnClientClick="return Validate(1);"  />

                                &nbsp;<asp:Button ID="btnSubmit" Visible="false" Width="190px"  CssClass="button" runat="server" OnClick="btnSubmit_Click" Text="Submit" OnClientClick="return Validate(2);"/>
                               <%-- &nbsp;<asp:Button ID="btnViewTracking" Visible="false" Width="190px"  CssClass="button" runat="server" OnClick="btnViewAssignedPos_Click" Text="View UPDATED fulfillment" />
--%>
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
            <Triggers    >
            <asp:PostBackTrigger ControlID="btnUpload" />
            </Triggers>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server"
                     DynamicLayout="false">
            <ProgressTemplate>
                <img src="/Images/ajax-loaders.gif" /> Loading ...
            </ProgressTemplate>
        </asp:UpdateProgress>
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
