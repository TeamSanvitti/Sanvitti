
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="/Controls/Footer.ascx" %>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="/Controls/Header.ascx" %>
<%@ Page language="c#" Codebehind="index.aspx.cs" AutoEventWireup="false" Inherits="avii.Admin.index" %>
<html>
	<HEAD>
		<title>.::  Lan Global inc. BackOffice  ::.</title>
		<LINK href="../aerostyle.css" type="text/css" rel="stylesheet">
		</LINK>
	</HEAD>
<body bgColor="#ffffff" leftmargin="0" rightmargin="0" topmargin="0">
	<body >
		<form id="Form1" method="post" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
				<TR>
					<TD>
                    <head:MenuHeader ID="MenuHeader1" runat="server"/>
				</TR>
                <tr>
                    <td height="500px"></td>
                </tr>
                 <tr>
                    <td>
                        <foot:MenuFooter ID="Foter" runat="server"></foot:MenuFooter>
                    </td>
                </tr>
			</TABLE>
			

		</form>
	</body>
</html>
