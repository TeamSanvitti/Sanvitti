<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frm.aspx.cs" Inherits="avii.Admin.frm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>.:: Lan Global ::.</title>
    <link href="../aerostyle.css" rel="stylesheet" type="text/css"/>
    
</head>
<body>
    <form id="form1" runat="server">
    <br />
        <br />
        <br />

        <table cellSpacing="0" cellPadding="0" align="center" width="100%" border="0">
    		<tr>                    
                <td colspan="2">
                    <asp:Label ID="lblMsg" runat="server" Width="100%" CssClass="errormessage"></asp:Label></td>
            </tr>               
            
            <tr>
                <td align="center">
                    <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="70%">
                    <tr>
                    <td>
			            <table id="Table2" cellSpacing="5" cellPadding="5" width="100%" border="0">
                        
                            <tr>
                                <td  align="center">
                                <asp:TextBox ID="txtSQL" CssClass="copy10grey" Width="80%" runat="server"></asp:TextBox>
                                </td>
                                </tr>
                                <tr>
                                <td  align="center">
                                
                                
                                <asp:Button ID="btnSubmit"  CssClass="button" runat="server" OnClick="btnSubmit_Click" Text="Submit" />

                                &nbsp;<asp:Button ID="btnCancel" runat="server"  CssClass="button" Text="Cancel" OnClick="btnCancel_Click" />
                                    </td>
                            </tr>
                        </table>
                   </td>
                   </tr>
                   </table>
                   <br />

                   <table cellpadding="0" cellspacing="0" width="100%">
                   <tr>
                        <td colspan="2" align="right" style="height:8px; vertical-align:bottom">
                            <strong>   <asp:Label ID="lblCount" CssClass="copy10grey" runat="server" ></asp:Label> &nbsp;&nbsp;</strong> 
                        </td>
                   </tr>
                   <tr>
                        <td colspan="2" align="center">


                        <asp:GridView runat="server" ID="gvUsr" AutoGenerateColumns="true" 
                         PageSize="50" AllowPaging="false" Width="100%"  
                         CellPadding="1" 
                        GridLines="Vertical"   >
                        <RowStyle  CssClass="copy10grey" BackColor="Gainsboro" />
                        <AlternatingRowStyle BackColor="white" />
                        <HeaderStyle  CssClass="button"   />
                         <PagerStyle ForeColor="#636363" CssClass="copy10grey" />
        
                        </asp:GridView>
   
                                
                       <%-- <asp:Repeater ID="rpUsr" runat="server" >
                                <HeaderTemplate>
                                <table border="0" width="98%" cellpadding="1" cellspacing="1">
                                <tr>
                                    <td class="button" >
                                        S.No.
                                    </td>
                                    <td class="button" >
                                        UserName
                                    </td>
                                    <td class="button" >
                                        Password
                                    </td>
                                    
                                    
                                    
                                </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
    
                                    <tr valign="bottom"  class="<%# Container.ItemIndex % 2 == 0 ? "alternaterow" : "" %>">
                                        <td class="copy10grey"  >
                                        <%# Container.ItemIndex +  1 %>
                                        </td>
                                        
                                        <td align="left" class="copy10grey" valign="bottom">
                                            <%# Eval("UserName")%>   
            
                                        </td>
                                        <td align="left" class="copy10grey" valign="bottom">
                                            <%# Eval("pwd")%>   
            
                                        </td>
                                        
                                        
                                        
                                        
                                    </tr>
    
    

    
                                </ItemTemplate>
                                <FooterTemplate>
                                </table>
                                </FooterTemplate>
                                </asp:Repeater>
--%>
                        </td>
                   </tr>
                   </table>
                            
              </tr>
              </table>             
        

    
    </form>
</body>
</html>
