<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WarehouseCode.aspx.cs" Inherits="avii.WarehouseCode" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="/Controls/Header.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>.:: Manage Warehouse Code ::.</title>
    <link href="/aerostyle.css" rel="stylesheet" type="text/css"/>  
    <script type="text/javascript">

    
    function Validate() {

            var warehouseCode = document.getElementById("<%=txtWarehouseCode.ClientID %>");

            var customer = document.getElementById("<%=ddlCompany.ClientID %>");

            if (customer.selectedIndex == 0) {
                alert("Please select a customer");
                //customer.focus();
                return false;
            }
            if (warehouseCode.value == "") {
                alert("Warehouse code can not be blank");
                warehouseCode.focus();
                return false;
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
    <br />
    <table   width="95%" align="center">
    <tr>
        <td>
            <table   width="100%" align="center">
                <tr>
			        <td  bgcolor="#dee7f6" class="buttonlabel">&nbsp;&nbsp;
			            MANAGE Warehouse Code
			        </td>
                </tr>
                
               <tr>
                    <td>
                        <asp:Label ID="lblMsg" runat="server" CssClass="errormessage" ></asp:Label>
                    </td>
               </tr>
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
                        <td class="copy10grey" width="15%" >
                            Customer Name: &nbsp;<span class="errormessage">*</span></td>
                        <td class="style1" width="50%">
                            <asp:DropDownList ID="ddlCompany" TabIndex="1" runat="server" CssClass="copy10grey">
                                </asp:DropDownList>
                        </td>
                        <td class="copy10grey"  width="15%">
                            Warehouse Code:&nbsp;<span class="errormessage">*</span></td>
                        <td width="32%">
                            <asp:TextBox ID="txtWarehouseCode"  Width="50%"  MaxLength="5" CssClass="copy10grey" TabIndex="2" runat="server"></asp:TextBox>
                        </td>
                        </tr>
                        <tr>
                            <td width="3%"></td>
                            <td>
                            
                            </td>
                            <td>
                                <asp:CheckBox ID="chkActive" TabIndex="3" CssClass="copy10grey" Text="Active" runat="server" />
                            </td>
                            <td>
                            
                            </td>
                            <td>
                            
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
        </td>
    </tr>
    </table>
       <br /><br />
<br />
     <br />
     <br />
     <br /><br />
<br />

     <br />
     <br />
     <br /><br />
<br />
     <br /
    <table width="100%" align="center">
     <tr>
        <td>
            <foot:MenuFooter ID="footer" runat="server"></foot:MenuFooter>
        </td>
     </tr>
     </table>
    </form>
</body>
</html>
