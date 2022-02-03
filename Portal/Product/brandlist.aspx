<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="brandlist.aspx.cs" Inherits="avii.product.brandlist" ValidateRequest="false" %>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="../Controls/Header.ascx" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="../Controls/Footer.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Brand List</title>
     <LINK href="../../aerostyle.css" type="text/css" rel="stylesheet">
    <link href="../Styles.css" type="text/css" rel="stylesheet" />
    
</head>
<body  leftmargin="0" rightmargin="0" topmargin="0" >
    <form id="form1" runat="server">
    <div>
        <table  width="95%" cellpadding="0" cellspacing="0" border="0" align="center">
            <tr>
					<td colspan="2">
						<head:menuheader id="MenuHeader1" runat="server"></head:menuheader>
						
					</td>
				</tr>
				
        <tr valign="top">
            <td width="150" valign="top" class="bgleft"><br />
                <table width="100%" border="0" cellpadding="0" cellspacing="0" class="copy10grey2">
							<tr>
								<td align="center" bgcolor="#dee7f6" class="button">
									Search Products</td>
							</tr>
						</table>
						<div align="center">
							<img src="../images/5_pic_3.jpg" width="178" height="90" hspace="8" vspace="8">
						</div>
                <table width="100%">
                <tr>
                    <td  class="copy10grey">
                        Price Range
                    </td>
                </tr>    
                <tr>
                    <td class="copy10grey">
                       <asp:CheckBox runat="server" ID="chk1" /> $200 to $499 
                    </td>
                </tr>
                <tr>
                    <td class="copy10grey">
                       <asp:CheckBox runat="server" ID="CheckBox1" /> $500 to $899
                    
                    </td>
                </tr>
                <tr>
                    <td class="copy10grey">
                       <asp:CheckBox runat="server" ID="CheckBox2" /> $900 to $1299
                    </td>
                </tr>
                <tr>
                    <td class="copy10grey">
                       <asp:CheckBox runat="server" ID="CheckBox3" /> $900 to $1299
                    </td>
                </tr>
                <tr>
                    <td class="copy10grey">
                       <asp:CheckBox runat="server" ID="CheckBox4" /> $1300 to $2000
                    </td>
                </tr>
                <tr>
                    <td class="copy10grey">
                       <asp:CheckBox runat="server" ID="CheckBox5" /> More than $2000
                    </td>
                </tr>
                <tr>
                    <td  class="copy10grey">
                        Phone Model
                    </td>
                </tr>
                <tr>
                    <td class="copy10grey">
                        <asp:DropDownList ID="DropDownList1" runat="server" Width="90">
                        <asp:ListItem>IFM-PLS125</asp:ListItem>
                        </asp:DropDownList>
                        
                    </td>
                </tr>
                <tr>
                    <td class="copy10grey">
                        Phone Maker
                    </td>
                </tr>
                <tr>
                    <td class="copy10grey">
                    
                        <asp:DropDownList ID="DropDownList2" runat="server" Width="90">
                        <asp:ListItem>LG</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td  class="copy10grey">
                        Color
                    </td>
                </tr>
                <tr>
                    <td class="copy10grey">
                        <asp:DropDownList ID="DropDownList3" runat="server" Width="90">
                        <asp:ListItem>BLACK</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td   class="copy10grey">
                        Technology
                    </td>
                </tr>
                
                <tr>
                    <td class="copy10grey">
                        <asp:DropDownList ID="DropDownList4" runat="server" Width="90">
                        <asp:ListItem>CDMA</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                 <tr >
                    <td align="center">
                        <asp:Button ID="btnsearch" Text="Search" runat="server" CssClass="Button" />
                    </td>
                </tr>
                </table>
            </td>
            <td >
                
                <div >
                
                <table width="75%" border="0" align="left" cellpadding="6" cellspacing="6">

				<tr>
					<td class="copy10grey" colspan="3">Lan Global is authorized to sell and distribute the full road map of the following OEMs and ODM:</td>
				</tr>
				
				<tr>
					<td class="copy10grey" width="50%" align="center">
					    <a href="productlist.aspx" ><img  alt="Alcatel" src="../images/newsite/logo_alctel.jpg"  border="0"/></a><br />
					    
					    
					</td>
					<td class="copy10grey" align="center">
					    <img  alt="Cal-Comp" src="../images/newsite/logo_calcomp.jpg"  border="0"/>
					   
					</td>
					<td>
					    <a href="productlist.aspx" ><img  alt="Kingston" src="../images/newsite/logo_kingston.jpg"  border="0" /></a>
					</td>
				</tr>
                <tr><td><br /></td></tr>
				<tr>
					<td class="copy10grey" align="center">
					    <a href="productlist.aspx" target="_blank"><img  alt="Alcatel" src="../images/newsite/logo_lg.jpg"  border="0"/></a>
					</td>
					<td class="copy10grey" align="center">
					    <a href="productlist.aspx" target="_blank"><img  alt="Pantech" src="../images/newsite/logo_pantech.jpg"  border="0"/></a>
					</td>
					<td>
					    <a href="http://www.samsung.com" target="_blank"><img  alt="Samsung" src="../images/newsite/logo_samsung.jpg"  border="0"/></a>
					</td>
				</tr>
                <tr><td><br /></td></tr>
				<tr>
					<td class="copy10grey" colspan="3" align="center">
					    <a href="http://www.audiovox.com" target="_blank"><img  alt="Audiovox" src="../images/newsite/logo_audiovox.jpg"  border="0"/></a>
					    
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
            <td colspan="2">
                <foot:MenuFooter id="Menuheader2" runat="server" ></foot:MenuFooter>
            </td>
        </tr>
        </table>
    </div>
    </form>
</body>
</html>
