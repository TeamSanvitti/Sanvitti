<%@ Page language="c#" Codebehind="frmcart.aspx.cs" AutoEventWireup="false" Inherits="avii.frmcart" %>
<%@ Register TagPrefix="foot" TagName="MenuHeader" Src="./Controls/Footer.ascx" %>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="./Controls/Header.ascx" %>
<%@ Register TagPrefix="cart" TagName="Menu" Src="./Controls/cart.ascx" %>
<%@ Register TagPrefix="serv" TagName="MenuSP" Src="./Controls/provider.ascx" %>
<HTML>
	<HEAD>
		<title>frmcart</title>
			<script language=javascript src="avI.js"></script>
		
		<link href="aerostyle.css" rel="stylesheet" type="text/css">
			<link href="style.css" rel="stylesheet" type="text/css">
		<script language="javascript">
		function fnV()
		{
			alert('Shopping cart is empty');
			window.navigate('list.aspx?p=n');
		}
		</script>
			
				<script language="javascript" src="avI.js"></script>
	</HEAD>
	<body bgColor="#ffffff" leftmargin="0" rightmargin="0" topmargin="0" >
		<form runat="server">
			<table width="780" cellpadding="0" cellspacing="0" align="center" border="0">
				<tr>
					<td><head:menuheader id="MenuHeader" runat="server"></head:menuheader></td>
				</tr>
				<tr>
					<td>
						<cart:Menu id="cart" runat="server"></cart:Menu>
					</td>
				</tr>
				<tr>
					<td>
						<serv:MenuSP id="Menusp1" runat="server"></serv:MenuSP>
					</td>
				</tr>				
				<tr>
					<td><foot:menuheader id="MenuFooter" runat="server"></foot:menuheader></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
