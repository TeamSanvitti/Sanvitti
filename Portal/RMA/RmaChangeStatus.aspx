<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RmaChangeStatus.aspx.cs" Inherits="avii.RMA.RmaChangeStatus" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="~/Controls/Header.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>.:: Return Merchandise Authorization (RMA) - Change Status ::.</title>
    <link href="/aerostyle.css" type="text/css" rel="stylesheet"/>
<script type="text/javascript">
        function Validate() {
            var status = document.getElementById("<% =ddlStatus.ClientID %>");
            if (status.selectedIndex == 0) {
                alert('Status is required!');
                return false;

            }
            

        }
    </script>


</head>
<body bgcolor="#ffffff"  leftmargin="0" topmargin="0" marginwidth="0" marginheight="0">>
    <form id="form1" runat="server">
    <table cellspacing="0" cellpadding="0" border="0"  width="100%" align="center">
        <tr>
            <td>
                <head:MenuHeader ID="HeadAdmin1" runat="server" ></head:MenuHeader>
            </td>
        </tr> 
        </table>
        
        <table cellspacing="0" cellpadding="0" border="0"  width="98%" align="center">       
            <tr><td>&nbsp;</td></tr>                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                 
             <tr>
                  <td>
                    <table id="rmaform" style="text-align:left; width:100%;"  align="center" class="copy10grey">
                    
                    <tr>
                        <td class="buttonlabel" align="left">Return Merchandise Authorization(RMA) - Change Status</td>
                    </tr>
                     <tr>
                   <td><br />
                       <asp:Label ID="lblMsg" runat="server" CssClass="errormessage"></asp:Label>
                   <table bordercolor="#839abf" border="1" cellspacing="0" cellpadding="0" width="100%">
                    <tr bordercolor="#839abf">
                        <td>
                        <table  cellSpacing="5" cellPadding="5" width="100%">
                            <tr>
                            <td class="copy10grey" width="42%" align="right">
                                Select a file:                                
                            </td>
                            <td class="copy10grey" width="3%">
                                &nbsp;                              
                            </td>

                            <td  align="left">
                                <asp:FileUpload ID="flnUpload" CssClass="copy10grey"  runat="server" />
                            </td>
                            </tr>
                            <tr>
                                <td class="copy10grey"  width="42%" align="right">
                                    Select Status:
                                </td>
                                <td class="copy10grey" width="3%">
                                &nbsp;                              
                            </td>
                                <td  align="left">
                                <asp:DropDownList ID="ddlStatus" runat="server" class="copy10grey">
                                                
                                                        <asp:ListItem  Value="0" >------</asp:ListItem>
                                                        <asp:ListItem Value="1" >Pending</asp:ListItem>
                                                        <asp:ListItem  Value="2">Received</asp:ListItem>
                                                        <asp:ListItem  Value="3">Pending for Repair</asp:ListItem>
                                                        <asp:ListItem  Value="4">Pending for Credit</asp:ListItem>
                                                        <asp:ListItem  Value="5">Pending for Replacement</asp:ListItem>
                                                        <asp:ListItem  Value="6">Approved</asp:ListItem>
                                                        <asp:ListItem  Value="7">Returned</asp:ListItem>
                                                        <asp:ListItem  Value="8">Credited</asp:ListItem>
                                                        <asp:ListItem  Value="9">Denied</asp:ListItem>
                                                        <asp:ListItem  Value="10">Closed</asp:ListItem>

                                                        <asp:ListItem  Value="11" >Out with OEM for repair</asp:ListItem>
                                                        <asp:ListItem  Value="12">Back to Stock -NDF</asp:ListItem>
                                                        <asp:ListItem  Value="13">Back to Stock- Credited</asp:ListItem>
                                                        <asp:ListItem  Value="14">Back to Stock – Replaced by OEM</asp:ListItem>
                                                        <asp:ListItem  Value="15">Repaired by OEM</asp:ListItem>
                                                        <asp:ListItem  Value="16">Replaced BY OEM</asp:ListItem>
<asp:ListItem  Value="38">Replaced by OEM- New</asp:ListItem>
                                                        <asp:ListItem  Value="39">Replaced by OEM- Preowned</asp:ListItem>
                                                        <asp:ListItem  Value="17">Replaced BY AV</asp:ListItem>
                                                        <asp:ListItem  Value="18">Repaired By AV</asp:ListItem>
                                                        <asp:ListItem  Value="19">NDF (No Defect Found)</asp:ListItem>
                                                        <asp:ListItem  Value="20">PRE-OWNED – A stock</asp:ListItem>
                                                        <asp:ListItem Value="21" >PRE-OWEND - B Stock</asp:ListItem>
                                                        <asp:ListItem Value="22" >PRE-OWEND – C Stock</asp:ListItem>
                                                        <asp:ListItem Value="23" >Rejected</asp:ListItem>
                                                        <asp:ListItem Value="24" >RTS (Return To Stock)</asp:ListItem>
                                                        <asp:ListItem Value="25" >Incomplete</asp:ListItem>
                                                        <asp:ListItem Value="26" >Damaged</asp:ListItem>
                                                        <asp:ListItem Value="27" >Preowned</asp:ListItem>
                                                        <asp:ListItem Value="28" >Return to OEM</asp:ListItem>
                                                        <asp:ListItem Value="29" >Returned to Stock</asp:ListItem>
                                                        <asp:ListItem Value="30" >Cancel</asp:ListItem>
                                                        <asp:ListItem Value="31" >External ESN</asp:ListItem>
                                                        <asp:ListItem Value="32" >Pending ship to OEM</asp:ListItem>
                                                        <asp:ListItem Value="33" >Sent to OEM</asp:ListItem>
                                                        <asp:ListItem Value="34" >Pending ship to Supplier</asp:ListItem>
                                                        <asp:ListItem Value="35" >Sent to Supplier</asp:ListItem>
                                                        <asp:ListItem Value="36" >Returned from OEM</asp:ListItem>

                                                        
                                            </asp:DropDownList>
                                            

                                </td>
                            </tr>
                            <tr>    
                                <td class="copy10grey" width="42%">
                                &nbsp;                              
                            </td><td class="copy10grey" width="3%">
                                &nbsp;                              
                            </td><td>
                                    
                                File format sample:	 RmaNumber (in Excel (.xls or .xlsx) format)
                                </td>
                            </tr>
                            
                            <tr>
                                <td>
                                    
                                &nbsp;

                                </td>
                            </tr>
                            <tr>
                                <td colspan="3"  align="center">
                                    <hr />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3"  align="center">
                                    <asp:Button ID="btnSubmit" CssClass="button" runat="server" Text="Submit" OnClick="btnSubmit_Click"  OnClientClick="return Validate();"  />&nbsp;&nbsp;
                                    <asp:Button ID="btnCancel" CssClass="button" runat="server" Text="Cancel" />
                                </td>
                            </tr>
                            
                        </table>
                        </td>
                        </tr>
                    </table>
                   
                    </td>
                    </tr>
                </table>    
                </td>
            </tr>
        </table>
         <br />
            <br /> <br />
            <br /> <br />
            <br />
        <table width="100%">
        <tr>
            <td>
                <foot:MenuFooter id="Footer" runat="server"></foot:MenuFooter>
            </td>
        </tr>
        </table>
               
    </form>
</body>
</html>
