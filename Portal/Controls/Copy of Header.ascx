<%@ Control Language="c#" AutoEventWireup="false" Codebehind="Header.ascx.cs" Inherits="avii.Controls.Header" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="~/dhtmlxmenu/menuControl.ascx" %>

<script language="JavaScript" type="text/javascript">
<!--
function mmLoadMenus() {
  if (window.mm_menu_0628005734_0) return;
  window.mm_menu_0628005734_0 = new Menu("root",152,16,"Verdana, Arial, Helvetica, sans-serif",10,"navy","#FFFFFF","#FFFFFF","navy","left","middle",3,0,500,-5,7,true,true,true,0,true,true);
  mm_menu_0628005734_0.addMenuItem("Comapny&nbsp;Profile","location='profile.aspx'");
   mm_menu_0628005734_0.fontWeight="bold";
   mm_menu_0628005734_0.hideOnMouseOut=true;
   mm_menu_0628005734_0.bgColor='#FFFFFF';
   mm_menu_0628005734_0.menuBorder=1;
   mm_menu_0628005734_0.menuLiteBgColor='#FFFFFF';
   mm_menu_0628005734_0.menuBorderBgColor='navy';


  window.mm_menu_0628005736_0 = new Menu("root",152,16,"Verdana, Arial, Helvetica, sans-serif",10,"navy","#FFFFFF","#E2EDFE","navy","left","middle",3,0,500,-5,7,true,false,true,0,true,true);
  mm_menu_0628005736_0.addMenuItem("Credit Application","location='./content/UFA10109267.pdf'");
  mm_menu_0628005736_0.addMenuItem("Return Merchandise Authorization","location='/frmRIMA.aspx'");
    mm_menu_0628005736_0.addMenuItem(" ","");
  mm_menu_0628005736_0.addMenuItem("Purchase&nbsp;Order&nbsp;Query","location='/PurchaseOrderQuery.aspx'");
  mm_menu_0628005736_0.addMenuItem("Inventory&nbsp;Query","location='/ItemSummary.aspx'");
  mm_menu_0628005736_0.addMenuItem("ESN&nbsp;Error&nbsp;Log","location='/frmErrorLog.aspx'");
    mm_menu_0628005736_0.addMenuItem(" ","");
  mm_menu_0628005736_0.addMenuItem("Sprint&nbsp;PLS&nbsp;Forecast&nbsp;OEM","location='/avforecast.aspx?i=1'");
  mm_menu_0628005736_0.addMenuItem("Sprint&nbsp;PLS&nbsp;Forecast&nbsp;Debrand","location='/avforecast.aspx?i=2'");
  mm_menu_0628005736_0.addMenuItem("Sprint&nbsp;PLS&nbsp;Forecast&nbsp;Future","location='/avforecast.aspx?i=3'");
  mm_menu_0628005736_0.addMenuItem(" ","");
  mm_menu_0628005736_0.addMenuItem("Create&nbsp;Purchase&nbsp;Order", "location='/Po.aspx'");
  mm_menu_0628005736_0.addMenuItem(" ", "");
  mm_menu_0628005736_0.addMenuItem("RMA&nbsp;by&nbsp;ESN", "location='/rma/rmaform.aspx?mode=esn'");
  mm_menu_0628005736_0.addMenuItem("RMA&nbsp;by&nbsp;PO", "location='/rma/rmaform.aspx?mode=po'");
  mm_menu_0628005736_0.addMenuItem("RMA&nbsp;Query", "location='/rma/rmaquery.aspx'");

   mm_menu_0628005736_0.hideOnMouseOut=true;
   mm_menu_0628005736_0.bgColor='#555555';
   mm_menu_0628005736_0.menuBorder=1;
   mm_menu_0628005736_0.menuLiteBgColor='#FFFFFF';
   mm_menu_0628005736_0.menuBorderBgColor='#777777';
  
  window.mm_menu_0628005815_0 = new Menu("root",152,16,"Verdana, Arial, Helvetica, sans-serif",10,"navy","#FFFFFF","#E2EDFE","navy","left","middle",3,0,500,-5,7,true,false,true,0,true,true);
  mm_menu_0628005815_0.addMenuItem("Company&nbsp;Profile","location='/profile.aspx'");
  mm_menu_0628005815_0.addMenuItem("Management&nbsp;Team&nbsp;","location='/team.aspx'");
   mm_menu_0628005815_0.hideOnMouseOut=true;
   mm_menu_0628005815_0.bgColor='#555555';
   mm_menu_0628005815_0.menuBorder=1;
   mm_menu_0628005815_0.menuLiteBgColor='#FFFFFF';
   mm_menu_0628005815_0.menuBorderBgColor='#777777';


  window.mm_menu_0713152944_0 = new Menu("root",140,16,"Verdana, Arial, Helvetica, sans-serif",10,"navy","#FFFFFF","#E2EDFE","navy","left","middle",3,0,1000,-5,7,true,false,true,0,true,true);
  mm_menu_0713152944_0.addMenuItem("Devices","location='Products.aspx'");
  mm_menu_0713152944_0.addMenuItem("Accessories","location='Accessories.aspx'");
  mm_menu_0713152944_0.addMenuItem("Accessories Closeouts","location='Accessories.aspx'");
   mm_menu_0713152944_0.hideOnMouseOut=true;
   mm_menu_0713152944_0.bgColor='#555555';
   mm_menu_0713152944_0.menuBorder=1;
   mm_menu_0713152944_0.menuLiteBgColor='#FFFFFF';
   mm_menu_0713152944_0.menuBorderBgColor='#777777';
       
   
  window.mm_menu_0713153653_0 = new Menu("root",250,16,"Verdana, Arial, Helvetica, sans-serif",10,"navy","#FFFFFF","#E2EDFE","navy","left","middle",3,0,1000,-5,7,true,false,true,0,true,true);
  mm_menu_0713153653_0.addMenuItem("Forward&nbsp;Logistics","location='fulfillment.aspx'");
  mm_menu_0713153653_0.addMenuItem("Packaging","location='customPack.aspx'");
  mm_menu_0713153653_0.addMenuItem("Assembly","location='Services.aspx'");
  mm_menu_0713153653_0.addMenuItem("Fulfillment&nbsp;and&nbsp;Supply&nbsp;Chain&nbsp;Management","location='fulfillment.aspx'");
  mm_menu_0713153653_0.addMenuItem("Software&nbsp;Provisioning","location='/programming.aspx'");
  mm_menu_0713153653_0.addMenuItem("Reverse&nbsp;Logistics","location='/servicecentre.aspx'");
   mm_menu_0713153653_0.hideOnMouseOut=true;
   mm_menu_0713153653_0.bgColor='#555555';
   mm_menu_0713153653_0.menuBorder=1;
   mm_menu_0713153653_0.menuLiteBgColor='#FFFFFF';
   mm_menu_0713153653_0.menuBorderBgColor='#777777';


  window.mm_menu_0714104340_0 = new Menu("root",82,16,"Verdana, Arial, Helvetica, sans-serif",10,"navy","#FFFFFF","#E2EDFE","navy","left","middle",3,0,1000,-5,7,true,false,true,0,false,true);
  mm_menu_0714104340_0.addMenuItem("Contact&nbsp;Info","location='/contactus.aspx'"); //navy  navy
  mm_menu_0714104340_0.addMenuItem("Register","location='/register.aspx'");
   mm_menu_0714104340_0.hideOnMouseOut=true;
   mm_menu_0714104340_0.bgColor='#555555';
   mm_menu_0714104340_0.menuBorder=1;
   mm_menu_0714104340_0.menuLiteBgColor='#FFFFFF';
   mm_menu_0714104340_0.menuBorderBgColor='#777777';

mm_menu_0714104340_0.writeMenus();
} // mmLoadMenus()
//-->
function fnValid(verb)
{
	if (verb.value.length > 0){
		fnSearch(verb)
		return true;
		}
	else {
		alert('Enter search criteria to search the site content');
		return false;
		}
}
</script>
<script langanguage="JavaScript" src="/mm_menu.js"></script>
<script langanguage="JavaScript">mmLoadMenus();</script>

<table cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
	<tr>
		<td height="70" width="82%">
			<A href="/index.aspx"><IMG height="67" src="/images/logo.gif" width="210" border="0"/></A>
		</td>
		<td valign="bottom" width="250">

										<TABLE cellSpacing="2" cellPadding="2" width="100%" border="0">
											<TBODY>
												<TR>
													<TD class="copy10white" align="right" colSpan="2" height="28">
														<table height="1" cellSpacing="2" cellPadding="0" border="0">
															<tr align="center">
																<td><input id="imgLogin" tabIndex="1" type="image" src="/images/loginbt.gif" align="absMiddle"
																		name="imgLogin" runat="server" causesvalidation="false"> <input id="imglogout" tabIndex="1" type="image" src="/images/logoutbt.gif" align="absMiddle"
																		name="imglogout" runat="server" causesvalidation="false">
																</td>															
															</tr>
														</table>
													</TD>
												</TR>
												<!--<TR>
													<TD class="copy10white" align="right" height="28"><IMG height="17" src="/images/searchhd.gif" width="46" align="absMiddle">&nbsp;&nbsp; 
														    <input class="txfield1" onkeypress="return fnValueValidate(event,'s');" id="verb" type="text"
															size="15" name="verb" maxLength="100">
													</TD>
													<td class="copy10white" align="right" width="30">
														<IMG onclick="return fnValid(verb);" width="25" height="17" src="/images/gobt.gif">
													</td>
												</TR>-->
											</TBODY>
										</TABLE>
									
		</td>
	</tr>

</table>
		&nbsp;<table cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
	    <tr  width="100%">
          <td bgcolor="navy"  align="right">	
	            <head:menuheader id="MenuHeader1" runat="server"></head:menuheader>
             </td>
	    </tr>
    </table>