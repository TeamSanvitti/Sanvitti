<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="POChangeStatus.aspx.cs" Inherits="avii.POChangeStatus" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="~/Controls/Header.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>.:: Lan Global inc. Inc. - Change Status ::.</title>
    <link href="/aerostyle.css" type="text/css" rel="stylesheet"/>
    <script type="text/javascript">
        function Validate() {
            var company = document.getElementById("<% =ddlCompany.ClientID %>");
            if (company.selectedIndex == 0) {
                alert('Customer is required!');
                return false;

            }
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
                        <td class="button" align="left">Fulfillment Order  - Change Status</td>
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
                                    Select Customer:
                                </td>
                                <td class="copy10grey" width="3%">
                                &nbsp;                              
                            </td>
                                <td  align="left">
                                <asp:DropDownList ID="ddlCompany" runat="server" class="copy10grey">
                                </asp:DropDownList>
                                            

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
                                <asp:ListItem Text="" Value=""></asp:ListItem>
                                <asp:ListItem Text="Pending" Value="1"></asp:ListItem>
                                <asp:ListItem Text="In-Process" Value="8"></asp:ListItem>
                                <asp:ListItem Text="Processed" Value="2"></asp:ListItem>
                                <asp:ListItem Text="Shipped" Value="3"></asp:ListItem>
                                <asp:ListItem Text="Closed" Value="4"></asp:ListItem>
                                <asp:ListItem Text="Return" Value="5"></asp:ListItem>

                                <asp:ListItem Text="Cancel" Value="9"></asp:ListItem>
                                <asp:ListItem Text="On Hold" Value="6"></asp:ListItem>
                                <asp:ListItem Text="Out of Stock" Value="7"></asp:ListItem>
                                
                                </asp:DropDownList>
                                            

                                </td>
                            </tr>

                            <tr>    
                                <td class="copy10grey" width="42%">
                                &nbsp;                              
                            </td><td class="copy10grey" width="3%">
                                &nbsp;                              
                            </td><td>
                                    
                                File format sample:	 PoNum (in Excel (.xlsx, .xls) format)
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
                                    <asp:Button ID="btnSubmit" CssClass="button" runat="server" Text="Submit" OnClientClick="return Validate();" OnClick="btnSubmit_Click" />&nbsp;&nbsp;
                                    <asp:Button ID="btnCancel" CssClass="button" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
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
        <br />
<br />
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
