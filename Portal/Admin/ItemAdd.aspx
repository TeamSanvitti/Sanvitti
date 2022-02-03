<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ItemAdd.aspx.cs" Inherits="avii.Admin.ItemAdd" %>
<%--<%@ Register TagPrefix="head" TagName="MenuHeader" Src="./admhead.ascx" %>--%>
<%@ Register TagPrefix="head" TagName="menuheader" Src="~/dhtmlxmenu/menuControl.ascx" %>

<%@ Register TagPrefix="Item" TagName="itemlist" Src="../controls/ItemList.ascx" %>
<%@ Register TagPrefix="Itemfrm" TagName="itemform" Src="../controls/ItemForm.ascx" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    	<title>Phone Inventory Management</title>
		<link href="../aerostyle.css" rel="stylesheet" type="text/css">
		<script language="javascript" src="../avI.js"></script> 
</head>
<body leftmargin="0" topmargin="0" marginwidth="0" marginheight="0" onkeydown="KeyDownHandler(document.all.btnUpload)">
    <form id="form1" runat="server" method="post" >
        <TABLE cellSpacing="0" cellPadding="0" align="center" width="100%" border="0">
         <tr>
            <td>
                <head:menuheader id="MenuHeader" runat="server"></head:menuheader>
            </td>
        </tr>
        				<tr>
					<td>
						<table borderColor="gray" cellSpacing="0" cellPadding="0" width="100%" align="center" border="1">
							<tr borderColor="white">
								<td>
									<table width="100%">
										<tr>
											<td class="copyblue11b">&nbsp;Inventory Management:</td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
					</td>
				</tr>
        <tr>
            <td>
                <table width="100%">
                    <tr>
                        <td valign="top"><Item:itemlist id="MenuHeader1" runat="server"></Item:itemlist></td>
                        <td valign="top"><Itemfrm:itemform id="Itemlist1" runat="server"></Itemfrm:itemform></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
