<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Managewarehouse.aspx.cs" Inherits="avii.Warehouse.Managewarehouse" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="~/Controls/Header.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Manage Warehouse</title>
    <script>
        function Validate() {
            
            var whouse = document.getElementById("<%= ddlWarehouse.ClientID %>");
            if (whouse.selectedIndex == 0) {
                alert('Warehouse is required!')
                return false;
            }
            var whCompany = document.getElementById("<%= ddlCompany.ClientID %>");
            if (whCompany.selectedIndex == 0) {
                alert('Customer is required!')
                return false;
            }
            var whLocation = document.getElementById("<%= txtWarehouseCode.ClientID %>");
            if (whLocation.value == '') {
                alert('Warehouse location is required!')
                return false;
            }
            var whAisle = document.getElementById("<%= txtAisle.ClientID %>");
            if (whLocation.value == '') {
                alert('Aisle is required!')
                return false;
            }
            var whBay = document.getElementById("<%= txtBay.ClientID %>");
            if (whLocation.value == '') {
                alert('Bay is required!')
                return false;
            }
            var whLevel = document.getElementById("<%= txtLevel.ClientID %>");
            if (whLocation.value == '') {
                alert('Row level is required!')
                return false;
            }
            return true;

        }
        function IsAlphaNumericHiphen(evt) {

            var charCodes = evt.keyCode ? evt.keyCode : evt.which ? evt.which : evt.charCode;
            if (((((charCodes > 31 && (charCodes < 48 || charCodes > 57) && charCodes != 45) && charCodes != 46) && charCodes != 95) && !(charCodes > 96 && charCodes < 123)) && !(charCodes > 64 && charCodes < 91)) {
                // alert(charCodes);

                charCodes = 0;
                return false;
            }

            return true;
        }

    </script>
</head>
<body>
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
            &nbsp;&nbsp;Manage Warehouse
			</td>
		</tr>
    
    </table>
    
    <table cellSpacing="0" cellPadding="0" align="center" width="95%" border="0" id="maintbl">
        <tr>
	        <td>
            <asp:UpdatePanel ID="upnlCode" UpdateMode="Conditional" runat="server">
	        <ContentTemplate>	            
                <table cellSpacing="0" cellPadding="0" align="center" width="100%" border="0">
    	            <tr>                    
                        <td><asp:Label ID="lblMsg" runat="server"  CssClass="errormessage"></asp:Label></td>
                    </tr> 
                </table>
                <table bordercolor="#839abf" border="1" width="100%" align="center" cellpadding="0" cellspacing="0">
            <tr>
            <td>
             <table width="100%" border="0" class="" align="center" cellpadding="5" cellspacing="5">    
                 
                 <tr>
                <td class="copy10grey"  align="right" width="20%" >
                 <b>Warehouse</b>:
                </td>
                <td width="30%" >
                    <asp:DropDownList ID="ddlWarehouse" TabIndex="1" runat="server" Width="60%" CssClass="copy10grey">                         
                    </asp:DropDownList>

                </td>
                <td  width="1%">
                       &nbsp;
                    
                </td>
                <td class="copy10grey"  align="right" width="19%" >
                      <b>Customer</b>:
                </td>
                <td width="30%" >
                    <asp:DropDownList ID="ddlCompany" TabIndex="2" runat="server" CssClass="copy10grey" Width="60%">
                                </asp:DropDownList>

                </td>   
                </tr>
                <tr>
                     <td colspan="5" >
                         <hr />
                     </td>
                 </tr>
                <tr>
                <td colspan="5">
                    <table width="100%" border="0" class="" align="center" cellpadding="5" cellspacing="5">               
                    <tr>
                        <td class="copy10grey"  align="right" width="15%" >
                        <b>Warehouse Location</b>:
                    </td>
                    <td width="15%" >                   
                    <asp:TextBox ID="txtWarehouseCode"  onkeypress="return IsAlphaNumericHiphen(event);"  TabIndex="3" CssClass="copy10grey" runat="server" Width="80%" MaxLength="10" ></asp:TextBox>
                
                    </td>
                    <td class="copy10grey"  align="right" width="8%" >
                   <b>Aisle</b>:
                </td>
                <td width="15%" >
                    
                    <asp:TextBox ID="txtAisle"  onkeypress="return IsAlphaNumericHiphen(event);" CssClass="copy10grey" TabIndex="4" runat="server" Width="80%" MaxLength="5" ></asp:TextBox>
                
                </td>
                <td class="copy10grey"  align="right" width="8%" >
                    <b>Bay</b>:
                </td>
                <td width="15%" >
                     <asp:TextBox ID="txtBay"   onkeypress="return IsAlphaNumericHiphen(event);" CssClass="copy10grey" TabIndex="5" runat="server" Width="80%" MaxLength="5" ></asp:TextBox>
                
                </td>   
                    <td class="copy10grey"  align="right" width="8%" >
                       <b>Level</b>: 
                    </td>
                    <td width="15%" >
                             <asp:TextBox ID="txtLevel"  onkeypress="return IsAlphaNumericHiphen(event);" TabIndex="6" CssClass="copy10grey" runat="server" Width="80%" MaxLength="5" ></asp:TextBox>
                
                    </td>   
                
                
                    </tr>
                    <//table>
                </td>
                </tr>
                 <tr valign="top">
                        <td class="copy10grey"  align="right" width="15%" >
                        
                            Special Instructions:
                    </td>
                    <td width="15%" colspan="7" >                   
                        <asp:TextBox ID="txtSpecialInstructions"  Height="60" TabIndex="7" TextMode="MultiLine" Rows="3" CssClass="copy10grey" runat="server" Width="97%" MaxLength="500" ></asp:TextBox>
                
                    </td>
                   
                    </tr>
                    <//table>
                </td>
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
                           OnClick="btnCancel_Click" /> 
                        &nbsp;
                        <asp:Button ID="btBacktoSearch" Visible="false" runat="server" TabIndex="19" CssClass="buybt" CausesValidation="false"  Text="Back to Search" 
                            onclick="btBacktoSearch_Click" />
                        &nbsp;
                        <asp:Button ID="btnNewLocation" Visible="true" runat="server" TabIndex="20" CssClass="buybt" CausesValidation="false"  
                            Text="New Warehouse Location" onclick="btnNewLocation_Click" />
			        </td>
			    </tr>
			    </table> 
            </ContentTemplate>
            </asp:UpdatePanel>
		
            </td>
       </tr>
    </table>
        <br /> <br />
            <br /> <br /><br /> <br />
            
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
