<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageOEM.aspx.cs" Inherits="avii.ManageOEM" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="/Controls/Header.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <title>.:: Manage OEM ::.</title>
     <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>

    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.4.0/jquery.min.js"></script>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/jquery-ui.min.js"></script>
    <link href="https://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/themes/start/jquery-ui.css"
    rel="stylesheet" type="text/css" />
<%--<script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/jquery-ui.js" type="text/javascript"></script>
<link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/themes/start/jquery-ui.css"
    rel="stylesheet" type="text/css" />--%>
	<%--<script type="text/javascript" src="../JQuery/jquery.min.js"></script>
    <script type="text/javascript" src="../JQuery/jquery-ui.min.js"></script>--%>
	<script type="text/javascript" src="../JQuery/jquery.blockUI.js"></script>
	
    <link href="/aerostyle.css" rel="stylesheet" type="text/css"/>  

    <script type="text/javascript">
        function validatePhoneNumber(elementValue) {

            var phoneNumberPattern = /^\(?(\d{3})\)?[- ]?(\d{3})[- ]?(\d{4})$/;
            return phoneNumberPattern.test(elementValue);
        }  
        function Validate() {

            var MakerName = document.getElementById("<%=txtMakerName.ClientID %>");

            var ShortName = document.getElementById("<%=txtShortName.ClientID %>");


            if (MakerName.value == "") {
                alert("OEM name can not be blank");
                MakerName.focus();
                return false;
            }
            if (ShortName.value == "") {
                alert("Short Name can not be blank");
                ShortName.focus();
                return false;
            }

            var cellPhoneObj = document.getElementById("<%=txtCellPhone.ClientID %>").value;
            if (cellPhoneObj != "") {
                if (!validatePhoneNumber(cellPhoneObj)) {
                    alert('Cell phone not a valid number');
                    return false;
                }
            }
            var homePhoneObj = document.getElementById("<%=txtHomePhone.ClientID %>").value;
            if (homePhoneObj != "") {
                if (!validatePhoneNumber(homePhoneObj)) {
                    alert('Home phone not a valid number');
                    return false;
                }
            }
            var office1PhoneObj = document.getElementById("<%=txtOfficePhone1.ClientID %>").value;
            if (office1PhoneObj != "") {
                if (!validatePhoneNumber(office1PhoneObj)) {
                    alert('Office phone 1 not a valid number');
                    return false;
                }
            }
            var office2PhoneObj = document.getElementById("<%=txtOfficePhone2.ClientID %>").value;
            if (office2PhoneObj != "") {
                if (!validatePhoneNumber(office2PhoneObj)) {
                    alert('Office phone 2 not a valid number');
                    return false;
                }
            }
        }
    </script>

</head>
<body bgColor="#ffffff" leftmargin="0" rightmargin="0" topmargin="0"  >
    <form id="form1" runat="server">
    <table cellSpacing="0" cellPadding="0"  border="0" align="center" width="100%">
		<tr>
			<td>
    			<head:MenuHeader ID="menuheader" runat="server"></head:MenuHeader>
           	</td>
		</tr>
	</table>
    <table   width="95%" align="center">
    <tr>
        <td>
            <table   width="100%" align="center">
                <tr>
			        <td  bgcolor="#dee7f6" class="buttonlabel">&nbsp;&nbsp;
			            MANAGE OEM
			        </td>
                </tr>
                
               <tr><td><asp:Label ID="lblMsg" runat="server" CssClass="errormessage" ></asp:Label>
               <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" CssClass="errormessage"
                ControlToValidate="txtEmail1" ErrorMessage="RegularExpressionValidator" 
                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">Invalid Email!!!</asp:RegularExpressionValidator>
               <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" CssClass="errormessage"
                ControlToValidate="txtEmail2" ErrorMessage="RegularExpressionValidator" 
                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">Invalid Email!!!</asp:RegularExpressionValidator>
               
               
               </td></tr>
            </table>
                <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="100%" align="center" >
                <tr bordercolor="#839abf">
                    <td >
                        
                        <table width="100%">
                        <tr height="6">
                            <td class="copy10grey">
                                &nbsp;</td>
                        </tr>
                        <tr>
                        <td width="3%"></td>
                        <td class="copy10grey" width="10%" >
                            OEM Name: &nbsp;<span class="errormessage">*</span></td>
                        <td class="style1" width="55%">
                            <asp:TextBox ID="txtMakerName"  MaxLength="50" Width="70%" TabIndex="1" 
                                CssClass="copy10grey" runat="server"></asp:TextBox>
                        </td>
                        <td class="copy10grey"  width="10%">
                            Short Name:&nbsp;<span class="errormessage">*</span></td>
                        <td width="37%">
                            <asp:TextBox ID="txtShortName" Width="50%"  MaxLength="5" CssClass="copy10grey" TabIndex="2" runat="server"></asp:TextBox>
                        </td>
                        </tr>
                        
                    <tr height="6">
                            <td class="copy10grey">
                                &nbsp;</td>
                        </tr>
                        </table>    
                    </td>
                </tr>
                
                </table>
                <br />
                 <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="100%" align="center" >
                    <tr bordercolor="#839abf">
                        <td >
                 <table width="100%"  >
                <tr><td width="3%"></td>
                    <td class="copy10grey" >
                        Description:<br />
                        <asp:TextBox CssClass="copy10grey" TabIndex="3" ID="txtDescription" Width="91%" Height="40" TextMode="MultiLine" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="copy10grey">
                        &nbsp;</td>
                </tr>
                </table>
                </td>
                </tr>
                </table>  
                <br />
                <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="100%" align="center" >
                    <tr bordercolor="#839abf">
                        <td >
                 <table width="100%"  >
                 <tr>
                    <td class="copy10grey">
                        &nbsp;</td>
                </tr>
                <tr>
                <td width="3%"></td>
                    <td class="copy10grey"  width="10%" >
                        Contact Name:
                       </td>
                    <td  width="35%" >
                        <asp:TextBox CssClass="copy10grey" MaxLength="50" TabIndex="3" ID="txtContactName" Width="91%" runat="server"></asp:TextBox>
                    
                    </td>
                    <td class="copy10grey"  width="7%" >
                        
                       </td>
                    <td class="copy10grey"  width="10%" >
                        
                       </td>
                    <td  width="35%" >
                        
                    
                    </td>
                </tr>
                <tr>
                    <td width="3%"></td>
                    <td class="copy10grey" width="10%" >
                        City:
                       </td>
                    <td  width="35%" >
                        <asp:TextBox CssClass="copy10grey" MaxLength="20" TabIndex="4" ID="txtCity" Width="91%" runat="server"></asp:TextBox>
                    
                    </td>
                    <td class="copy10grey"  width="7%" >
                        
                       </td>
                    <td class="copy10grey"  width="10%" >
                        Email1:
                       </td>
                    <td  width="35%" >
                        <asp:TextBox CssClass="copy10grey" MaxLength="50" TabIndex="7" ID="txtEmail1" Width="91%" runat="server"></asp:TextBox>
                    
                    </td>
                </tr>
                <tr>
                    <td width="3%"></td>
                    <td class="copy10grey"  width="10%" >
                        State:
                       </td>
                    <td  width="35%" >
                        
                        <asp:DropDownList CssClass="copy10grey" TabIndex="5" ID="ddlState" runat="server" Width="91%">
                        </asp:DropDownList>
                    </td>
                    <td class="copy10grey"  width="7%" >
                        
                       </td>
                    <td class="copy10grey"  width="10%" >
                        Email2:
                       </td>
                    <td  width="35%" > 
                        <asp:TextBox CssClass="copy10grey" TabIndex="8" MaxLength="50" ID="txtEmail2" Width="91%" runat="server"></asp:TextBox>
                    
                    </td>
                </tr>
                <tr>
                    <td width="3%"></td>
                    <td class="copy10grey"  width="10%" >
                        Zip:
                       </td>
                    <td  width="35%" >
                        <asp:TextBox CssClass="copy10grey" TabIndex="6" ID="txtZip" MaxLength="5" Width="91%" runat="server"></asp:TextBox>
                    
                    </td>

                    <td class="copy10grey"  width="7%" >
                        
                       </td>
                    <td class="copy10grey"  width="10%" >   
                            Country:
                       </td>
                    <td  width="35%" >
                        <asp:TextBox CssClass="copy10grey" TabIndex="9" ID="txtCountry" MaxLength="100" Width="91%" runat="server"></asp:TextBox>
                    
                    </td>
                </tr>
                <tr>
                    <td class="copy10grey">
                        &nbsp;</td>
                </tr>
                </table>
                </td>
                </tr>
                </table>  
                <br />
                <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="100%" align="center" >
                    <tr bordercolor="#839abf">
                        <td >
                 <table width="100%"  >
                 <tr>
                    <td class="copy10grey">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td width="3%"></td>
                    <td class="copy10grey"  width="10%" >
                        Address1:
                       </td>
                    <td  width="35%" >
                        <asp:TextBox CssClass="copy10grey" MaxLength="100" TabIndex="10" ID="txtAddress1" Width="91%" runat="server"></asp:TextBox>
                    
                    </td>
                    <td class="copy10grey"  width="7%" >
                        
                       </td>
                    <td class="copy10grey"  width="10%" >
                        Office Phone 1:
                       </td>
                    <td  width="35%" >
                        
                    <asp:TextBox CssClass="copy10grey" TabIndex="13" MaxLength="12" ID="txtOfficePhone1" Width="91%" runat="server"></asp:TextBox>
                    
                    </td>
                </tr>
                <tr>
                    <td width="3%"></td>
                    <td class="copy10grey"  width="10%" >
                        Address2:
                       </td>
                    <td  width="35%" >
                        <asp:TextBox CssClass="copy10grey" TabIndex="11" MaxLength="100" ID="txtAddress2" Width="91%" runat="server"></asp:TextBox>
                    
                    </td>
                    <td class="copy10grey"  width="7%" >
                        
                       </td>
                    <td class="copy10grey"  width="10%" >
                        Office Phone 2:
                       </td>
                    <td  width="35%" > 
                        <asp:TextBox CssClass="copy10grey" TabIndex="14" ID="txtOfficePhone2" MaxLength="12" Width="91%" runat="server"></asp:TextBox>
                    
                    </td>
                </tr>
                <tr>
                    <td width="3%"></td>
                    <td class="copy10grey"  width="10%" >
                        Cell Phone:
                       </td>
                    <td  width="35%" >
                        <asp:TextBox CssClass="copy10grey" TabIndex="12" ID="txtCellPhone" Width="91%" MaxLength="12" runat="server"></asp:TextBox>
                    
                    </td>
                    <td class="copy10grey"  width="7%" >
                        
                       </td>
                    <td class="copy10grey"  width="10%" >
                        Home Phone:
                       </td>
                    <td  width="35%" >
                        <asp:TextBox CssClass="copy10grey" TabIndex="15" ID="txtHomePhone" Width="91%" MaxLength="12" runat="server"></asp:TextBox>
                    
                    </td>
                </tr>
                <tr>
                    <td class="copy10grey">
                        &nbsp;</td>
                </tr>
                </table>
                </td>
                </tr>
                </table> <br />
                <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="100%" align="center" >
                    <tr bordercolor="#839abf">
                        <td >
                <table width="100%"  >
                 <tr>
                    <td class="copy10grey">
                        &nbsp;</td>
                </tr>   
                <tr>
                    <td width="3%"></td>
                    <td width="10%" class="copy10grey">
                    Image:
                    </td>
                    <td width="35%" >
                    <asp:FileUpload ID="uploadMakerImg" TabIndex="16" runat="server" Width="300" CssClass="copy10grey" />    
                    </td>
                    <td  width="5%" >
                    
                    </td>
                    <td  width="45%" colspan="2" align="left" >
                        <asp:CheckBox ID="chkActive" Text="Active" TabIndex="17" CssClass="copy10grey" Checked="true" runat="server" />
                        &nbsp;
                        <asp:CheckBox ID="chkShowCatolog" Text="Show under catolog" TabIndex="18" Checked="true" CssClass="copy10grey" runat="server" />
                    <%--</td>
                    <td class="copy10grey" width="35%" align="right">--%>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:LinkButton ID="lnkView" OnClick="lnkView_Click" runat="server">View Image</asp:LinkButton> &nbsp;&nbsp;
                       </td>
                </tr>
                <tr>
                    <td class="copy10grey">
                        &nbsp;</td>
                </tr>
                </table>        

                    
                
                    
        </td>
    </tr>
    </table>
    <br />
    <table width="100%" align="center" >
                <tr>
			        <td align="center" >
			            <asp:Button ID="btnSubmit" runat="server" TabIndex="18"  CssClass="buybt" OnClientClick="return Validate();" 
                                        Text="   Submit   " onclick="btnSubmit_Click" />&nbsp;&nbsp;
                        <asp:Button ID="btnCancel" runat="server" TabIndex="19" CssClass="buybt" CausesValidation="false"  Text="   Cancel   " 
                            onclick="btnCancel_Click" />
			        </td>
			    </tr>
			    </table> 

                <table width="100%" align="center" >
                <tr>
                    <td>
                        <asp:ScriptManager ID="ScriptManager1" runat="server">
                        </asp:ScriptManager>
                          <asp:UpdatePanel ID="UpdatePanel1"  runat="server" >
                         <ContentTemplate>
                         <table>
                            <tr>
                                <td>
                            <ajaxToolkit:ModalPopupExtender BackgroundCssClass="modal2Background" 
                            CancelControlID="btnClose" runat="server" PopupControlID="pnlModelPopup" 
                            ID="ModalPopupExtender1" TargetControlID="lnk"
                             />
                            <asp:LinkButton ID="lnk" runat="server" ></asp:LinkButton>
                            <asp:Panel  ID="pnlModelPopup" runat="server" CssClass="modal2Popup"   >
      
      
                          <div style="overflow:auto; height:520px; width:100%; border: 0px solid #839abf" >
      
                          <table align="center" border="0"  width="100%">
                          <tr>
                            <td>
        
        
                          <table align="center" border="0" width="100%" >
                          <tr>
                            <td align="right" >
       
        
                                <asp:Button ID="btnClose" CssClass="button" Height="28" runat="server" Text="Close" CausesValidation="false"  />
        
         
                            </td>
                          </tr>
                          </table>
                          </td>
                          </tr>
                          <tr>
                            <td align="center">
        
        
                          <table align="center" border="0" width="80%"> 
                          <tr>
                          <td align="center" >
                              <asp:Image ID="imgMaker" runat="server" />
                          </td>
                          </tr>
                          </table>
                          </td>
                          </tr>
                          </div>
                          </asp:Panel>
                                </td>
                                </tr>
                                </table>
     
     
     </ContentTemplate>
     </asp:UpdatePanel>
                    </td>
                </tr>
                </table>

    <br />
    <br />
                    <foot:MenuFooter id="Footer" runat="server"></foot:MenuFooter>
                
    </form>
</body>
</html>
