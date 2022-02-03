<%@ Register TagPrefix="foot" TagName="MenuHeader" Src="Controls/Footer.ascx" %>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="Controls/Header.ascx" %>
<%@ Page language="c#"  %>

<script runat="server">

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] != null || Session["adm"] != null)
            {
                Control tmp = LoadControl("./controls/ctlItemSummary.ascx");
                avii.Controls.ctlItemSummary ctlItem = tmp as avii.Controls.ctlItemSummary;

                if (tmp != null)
                {
                    ctlItem.UserID = Convert.ToInt32(Session["UserID"]);
                }
                
                pnlItems.Controls.Add(ctlItem);
            }
       
    }
</script>

<HTML>
	<HEAD>
		<title>Products</title>
			<script language=javascript src="avI.js"></script>
		
		<link href="aerostyle.css" rel="stylesheet" type="text/css">
	</HEAD>
	<BODY bgColor="#ffffff" leftmargin="0" rightmargin="0" topmargin="0">
		<form id="Form1" method="post" runat="server">
			<table align="center" width="95%" border="0" cellpadding="0" cellspacing="0">
				<tr>
					<td valign="top" align="left"><head:menuheader id="MenuHeader1" runat="server"></head:menuheader></td>
				</tr>
			</table>
			<table width="95%" border="0" align="center" cellpadding="0" cellspacing="0">
				<tr>
					<td width="200" valign="top" class="bgleft"><br>
						<table width="100%" border="0" cellpadding="0" cellspacing="0" class="copy10grey2">
							<tr>
								<td align="center" bgcolor="#dee7f6" class="button">
									Products</td>
							</tr>
						</table>
						<div align="center"><br>
							<img src="images/5_pic_3.jpg" width="178" height="90" hspace="8" vspace="8">
						</div>
					</td>
					<td width="1" bgcolor="#a6bce0"><img src="images/dotblue.gif" width="1" height="1"></td>
					<td>
					    <table width="95%" border="0" align="center" cellpadding="6" cellspacing="6">

							<tr>
								<td class="copy10grey" colspan="3">Lan Global inc. is authorized to sell and distribute the full road map of the following OEMs and ODM:</td>
							</tr>
							

                            <tr><td><br /></td></tr>
							<tr>
								<td class="copy10grey" align="center">
								    <a href="http://www.lge.com" target="_blank"><img  alt="Alcatel" src="./images/newsite/logo_lg.jpg"  border="0"/></a>
								</td>
								<td class="copy10grey" align="center">
								    <a href="http://www.pantechusa.com" target="_blank"><img  alt="Pantech" src="./images/newsite/logo_pantech.jpg"  border="0"/></a>
								    
								</td>
								<td align="center">
								    <a href="http://www.samsung.com" target="_blank"><img  alt="Samsung" src="./images/newsite/logo_samsung.jpg"  border="0"/></a>
								</td>
							</tr>
                            <tr><td><br /></td></tr>
							<tr>
								<td class="copy10grey" align="center">
								    <a href="http://www.Sanyo.com" target="_blank"><img  alt="Sanyo" src="./images/newsite/sanyologo.gif"  border="0"/></a>
								</td>
								<td class="copy10grey" align="center">
								    <a href="http://www.ZTE.com" target="_blank"><img  alt="ZTE" src="./images/newsite/ztelogo.gif"  border="0"/></a>
								</td>								
								<td class="copy10grey" align="center">
								    <a href="http://www.sprintcom" target="_blank"><img  alt="sprint" src="./images/newsite/sprintlogo.gif"  border="0"/></a>
								</td>
							</tr>

                            <tr><td><br /></td></tr>
							<tr>
								<td class="copy10grey" align="center">
								    <a href="http://www.Acer.com" target="_blank"><img  alt="Acer" src="./images/newsite/acerlogo.gif"  border="0"/></a>
								</td>								
								<td class="copy10grey" align="center">
								    <a href="http://www.PCD.com" target="_blank"><img  alt="PCD" src="./images/newsite/pcdlogo.gif"  border="0"/></a>
								</td>
							</tr>
							<tr><td>&nbsp;</td></tr>
                            <tr><td><br /></td></tr>
                            <tr><td colspan="3"><asp:Panel id="pnlItems" Width="100%" runat="server"></asp:Panel></td></tr>
                            <tr><td><br /></td></tr>
																																																	
						</table>
					</td>
				</tr>
			</table>
			<table align="center" width="95%" border="0" cellpadding="0" cellspacing="0">
				<tr>
					<td colspan="2"><foot:menuheader id="footer" runat="server"></foot:menuheader></td>
				</tr>
			</table>
		</form>
	</BODY>
</HTML>
