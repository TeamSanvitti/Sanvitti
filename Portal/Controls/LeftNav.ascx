<%@ Control Language="c#" AutoEventWireup="false" Codebehind="LeftNav.ascx.cs" Inherits="avii.Controls.LeftNav" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<table height="100%" cellSpacing="0" cellPadding="0" width="160" border="0">
	<tr>
		<td class="bgsub" vAlign="top" width="160" bgColor="#093173" height="100%">
			<table cellSpacing="0" cellPadding="0" width="160" border="0">
				<tr>
					<td>
						<!--  +++++++++++++++++++++++++++++ SERACH TABLE BEGINS  ++++++++++++++++++++++++++++++++ -->
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td bgColor="#527ab5"><IMG height="3" src="/images/dot1.gif" width="100%"></td>
							</tr>
							<tr>
								<td bgColor="#4a72ab">&nbsp;<IMG height="17" src="/images/searchhd.gif" width="49"></td>
							</tr>
							<tr>
								<td bgColor="#4a72ab">
									<form name="form2" action="" method="post">
										<table cellSpacing="2" cellPadding="2" width="100%" border="0">
											<tr>
												<td class="copy10white"><input name="verb" type="text" class="txfield" size="14"></td>
												<td class="copy10white"><img src="/images/gobt.gif" width="25" onclick="fnSearch(verb)" height="17" name="btnsrch" align="absmiddle"></td>
                    </tr>
										</table>
									</form>
								</td>
							</tr>
							<TR>
								<TD bgColor="#ffffff"><IMG height="1" src="/images/dotwhite.gif" width="1"></TD>
							</TR>
						</table> <!-- END SEARCH TABLE --></td>
				</tr>
			</table>
			<TABLE cellSpacing="0" cellPadding="0" width="160" border="0">
				<TR>
					<TD width="7" bgColor="#93aed9">&nbsp;</TD>
					<TD width="2" bgColor="#ffffff"><IMG src="/images/dotwhite.gif" width="2"></TD>
					<TD bgColor="#5681c3">
						<table cellSpacing="2" cellPadding="2" width="100%" border="0">
							<tr>
								<td><A onmouseover="MM_swapImage('Image28','','images/aboutbt2.gif',1)" onmouseout="MM_swapImgRestore()"
										href="aboutus.htm"><IMG onmouseover="MM_showHideLayers('aboutus','','show')" onmouseout="MM_showHideLayers('aboutus','','hide')"
											height="23" src="/images/aboutbt1.gif" width="111" border="0" name="Image28"></A></td>
							</tr>
							<tr>
								<td><A onmouseover="MM_swapImage('Image291','','images/productsbt2.gif',1)" onmouseout="MM_swapImgRestore()"
										href="products.htm"><IMG id="Image291" onmouseover="MM_showHideLayers('aboutus','','hide','products','','show')"
											onmouseout="MM_showHideLayers('aboutus','','hide','products','','hide')" height="21" src="/images/productsbt1.gif"
											width="111" border="0" name="Image291"></A></td>
							</tr>
							<tr>
								<td><A onmouseover="MM_swapImage('Image301','','images/servicesbt2.gif',1)" onmouseout="MM_swapImgRestore()"
										href="services.htm"><IMG id="Image301" onmouseover="MM_showHideLayers('aboutus','','hide','products','','hide','services','','show')"
											onmouseout="MM_showHideLayers('aboutus','','hide','products','','hide','services','','hide')" height="21"
											src="/images/servicesbt1.gif" width="111" border="0" name="Image301"></A></td>
							</tr>
							<tr>
								<td><A onmouseover="MM_swapImage('Image42','','images/formsbt2.gif',1)" onmouseout="MM_swapImgRestore()"
										href="#"><IMG onmouseover="MM_showHideLayers('form','','show')" onmouseout="MM_showHideLayers('aboutus','','hide','products','','hide','services','','hide','contact','','hide','form','','hide')"
											height="21" src="/images/formsbt1.gif" width="111" border="0" name="Image42"></A></td>
							</tr>
							<tr>
								<td><A onmouseover="MM_swapImage('Image311','','images/contactusbt2.gif',1)" onmouseout="MM_swapImgRestore()"
										href="contactus.htm"><IMG id="Image311" onmouseover="MM_showHideLayers('aboutus','','hide','products','','hide','services','','hide','contact','','show')"
											onmouseout="MM_showHideLayers('aboutus','','hide','products','','hide','services','','hide','contact','','hide')"
											height="21" src="/images/contactusbt1.gif" width="111" border="0" name="Image311"></A></td>
							</tr>
							<tr>
								<td><A onmouseover="MM_swapImage('Image321','','images/homebt2.gif',1)" onmouseout="MM_swapImgRestore()"
										href="index.htm"><IMG id="Image321" height="21" src="/images/homebt1.gif" width="111" border="0" name="Image321"></A></td>
							</tr>
							<tr>
								<td>
									<div id="aboutus" onmouseover="MM_showHideLayers('aboutus','','show')" style="BORDER-RIGHT: #000000 1px; BORDER-TOP: #000000 1px; Z-INDEX: 1; LEFT: 365px; VISIBILITY: hidden; BORDER-LEFT: #000000 1px; WIDTH: 140px; BORDER-BOTTOM: #000000 1px; POSITION: absolute; TOP: 190px; HEIGHT: 54px; BACKGROUND-COLOR: #ffffff; layer-background-color: #FFFFFF"
										onmouseout="MM_showHideLayers('aboutus','','hide')">
										<table cellSpacing="1" cellPadding="0" width="100%" border="0">
											<tr>
												<td bgColor="#7492ca">
													<table cellSpacing="1" cellPadding="2" width="100%" border="0">
														<tr>
															<td width="4"><IMG height="5" hspace="2" src="/images/bullet.gif" width="5" vspace="2"></td>
															<td><A class="copy11whb" href="/profile.htm">COMPANY PROFILE</A></td>
														</tr>
														<tr>
															<td width="4"><IMG height="5" hspace="2" src="/images/bullet.gif" width="5" vspace="2"></td>
															<td><A class="copy11whb" href="/management.htm">MANAGEMENT</A></td>
														</tr>
														<tr>
															<td width="4"><IMG height="5" hspace="2" src="/images/bullet.gif" width="5" vspace="2"></td>
															<td><A class="copy11whb" href="/achievements.htm">ACHIEVEMENTS</A></td>
														</tr>
													</table>
												</td>
											</tr>
										</table>
									</div>
									<div id="products" onmouseover="MM_showHideLayers('aboutus','','hide','products','','show')"
										style="BORDER-RIGHT: #000000 1px; BORDER-TOP: #000000 1px; Z-INDEX: 1; LEFT: 365px; VISIBILITY: hidden; BORDER-LEFT: #000000 1px; WIDTH: 140px; BORDER-BOTTOM: #000000 1px; POSITION: absolute; TOP: 218px; HEIGHT: 54px; BACKGROUND-COLOR: #ffffff; layer-background-color: #FFFFFF"
										onmouseout="MM_showHideLayers('aboutus','','hide','products','','hide')">
										<table cellSpacing="1" cellPadding="0" width="100%" border="0">
											<tr>
												<td bgColor="#7492ca">
													<table cellSpacing="1" cellPadding="2" width="100%" border="0">
														<tr>
															<td width="4"><IMG height="5" hspace="2" src="/images/bullet.gif" width="5" vspace="2"></td>
															<td><A class="copy11whb" href="#">NEW PHONES</A></td>
														</tr>
														<tr>
															<td><IMG height="5" hspace="2" src="/images/bullet.gif" width="5" align="absMiddle" vspace="2"></td>
															<td><A class="copy11whb" href="#">CLOSEOUT PHONES</A></td>
														</tr>
														<tr>
															<td><IMG height="5" hspace="2" src="/images/bullet.gif" width="5" vspace="2"></td>
															<td><A class="copy11whb" href="#">REFURBISHED PHONES</A></td>
														</tr>
													</table>
												</td>
											</tr>
										</table>
									</div>
									<div id="services" onmouseover="MM_showHideLayers('aboutus','','hide','products','','hide','services','','show')"
										style="BORDER-RIGHT: #000000 1px; BORDER-TOP: #000000 1px; Z-INDEX: 1; LEFT: 365px; VISIBILITY: hidden; BORDER-LEFT: #000000 1px; WIDTH: 140px; BORDER-BOTTOM: #000000 1px; POSITION: absolute; TOP: 245px; HEIGHT: 54px; BACKGROUND-COLOR: #ffffff; layer-background-color: #FFFFFF"
										onmouseout="MM_showHideLayers('aboutus','','hide','products','','hide','services','','hide')">
										<table cellSpacing="1" cellPadding="0" width="100%" border="0">
											<tr>
												<td bgColor="#7492ca">
													<table cellSpacing="1" cellPadding="2" width="100%" border="0">
														<tr>
															<td width="4"><IMG height="5" hspace="2" src="/images/bullet.gif" width="5" vspace="2"></td>
															<td><A class="copy11whb" href="services.htm">PROGRAMMING</A></td>
														</tr>
														<tr>
															<td><IMG height="5" hspace="2" src="/images/bullet.gif" width="5" align="absMiddle" vspace="2"></td>
															<td><A class="copy11whb" href="servicecentre.htm">SERVICE CENTER</A></td>
														</tr>
														<tr>
															<td><IMG height="5" hspace="2" src="/images/bullet.gif" width="5" vspace="2"></td>
															<td><A class="copy11whb" href="packaging.htm">PACKAGING</A></td>
														</tr>
														<tr>
															<td><IMG height="5" hspace="2" src="/images/bullet.gif" width="5" vspace="2"></td>
															<td><A class="copy11whb" href="fulfillment.htm">FULFILLMENT</A></td>
														</tr>
														<tr>
															<td><IMG height="5" hspace="2" src="/images/bullet.gif" width="5" vspace="2"></td>
															<td><A class="copy11whb" href="refurbishing.htm">REFURBISHING</A></td>
														</tr>
													</table>
												</td>
											</tr>
										</table>
									</div>
									<div id="contact" onmouseover="MM_showHideLayers('aboutus','','hide','products','','hide','services','','hide','contact','','show')"
										style="BORDER-RIGHT: #000000 1px; BORDER-TOP: #000000 1px; Z-INDEX: 1; LEFT: 365px; VISIBILITY: hidden; BORDER-LEFT: #000000 1px; WIDTH: 139px; BORDER-BOTTOM: #000000 1px; POSITION: absolute; TOP: 299px; HEIGHT: 34px; BACKGROUND-COLOR: #ffffff; layer-background-color: #FFFFFF"
										onmouseout="MM_showHideLayers('aboutus','','hide','products','','hide','services','','hide','contact','','hide')">
										<table cellSpacing="1" cellPadding="0" width="100%" border="0">
											<tr>
												<td bgColor="#7492ca">
													<table cellSpacing="2" cellPadding="0" width="100%" border="0">
														<tr>
															<td><IMG height="5" hspace="2" src="/images/bullet.gif" width="5" vspace="2"></td>
															<td><A class="copy11whb" href="contactus.htm">CONTACT INFO</A></td>
														</tr>
														<tr>
															<td width="4"><IMG height="5" hspace="2" src="/images/bullet.gif" width="5" vspace="2"></td>
															<td><A class="copy11whb" href="register.htm">REGISTER</A></td>
														</tr>
													</table>
												</td>
											</tr>
										</table>
									</div>
									<div id="form" onmouseover="MM_showHideLayers('aboutus','','hide','form','','show')"
										style="BORDER-RIGHT: #000000 1px; BORDER-TOP: #000000 1px; Z-INDEX: 1; LEFT: 365px; VISIBILITY: hidden; BORDER-LEFT: #000000 1px; WIDTH: 139px; BORDER-BOTTOM: #000000 1px; POSITION: absolute; TOP: 272px; HEIGHT: 24px; BACKGROUND-COLOR: #ffffff; layer-background-color: #FFFFFF"
										onmouseout="MM_showHideLayers('aboutus','','hide','form','','hide')">
										<table cellSpacing="1" cellPadding="0" width="100%" border="0">
											<tr>
												<td bgColor="#7492ca">
													<table cellSpacing="1" cellPadding="2" width="100%" border="0">
														<tr>
															<td width="4"><IMG height="5" hspace="2" src="/images/bullet.gif" width="5" vspace="2"></td>
															<td><A class="copy11whb" href="profile.htm">link 1</A></td>
														</tr>
														<tr>
															<td width="4"><IMG height="5" hspace="2" src="/images/bullet.gif" width="5" vspace="2"></td>
															<td><A class="copy11whb" href="management.htm">RA FORMS</A></td>
														</tr>
													</table>
												</td>
											</tr>
										</table>
									</div>
								</td>
							</tr>
						</table>
					</TD>
					<td width="1" bgColor="#ffffff"><IMG src="/images/dotwhite.gif" width="1"></td>
					<td width="50" bgColor="#7497cd">&nbsp;</td>
				</TR>
			</TABLE>
		</td>
	</tr>
</table>
<script language="javascript">
		function fnSearch(txt)
		{
			alert('in');
			var str = 'list.aspx?verb=' + txt.value;
			window.navigate(str);
		}

		
function MM_preloadImages() { //v3.0
  var d=document; if(d.images){ if(!d.MM_p) d.MM_p=new Array();
    var i,j=d.MM_p.length,a=MM_preloadImages.arguments; for(i=0; i<a.length; i++)
    if (a[i].indexOf("#")!=0){ d.MM_p[j]=new Image; d.MM_p[j++].src=a[i];}}
}

function MM_findObj(n, d) { //v4.01
  var p,i,x;  if(!d) d=document; if((p=n.indexOf("?"))>0&&parent.frames.length) {
    d=parent.frames[n.substring(p+1)].document; n=n.substring(0,p);}
  if(!(x=d[n])&&d.all) x=d.all[n]; for (i=0;!x&&i<d.forms.length;i++) x=d.forms[i][n];
  for(i=0;!x&&d.layers&&i<d.layers.length;i++) x=MM_findObj(n,d.layers[i].document);
  if(!x && d.getElementById) x=d.getElementById(n); return x;
}

function MM_showHideLayers() { //v6.0
  var i,p,v,obj,args=MM_showHideLayers.arguments;
  for (i=0; i<(args.length-2); i+=3) if ((obj=MM_findObj(args[i]))!=null) { v=args[i+2];
    if (obj.style) { obj=obj.style; v=(v=='show')?'visible':(v=='hide')?'hidden':v; }
    obj.visibility=v; }
}

function MM_swapImgRestore() { //v3.0
  var i,x,a=document.MM_sr; for(i=0;a&&i<a.length&&(x=a[i])&&x.oSrc;i++) x.src=x.oSrc;
}

function MM_swapImage() { //v3.0
  var i,j=0,x,a=MM_swapImage.arguments; document.MM_sr=new Array; for(i=0;i<(a.length-2);i+=3)
   if ((x=MM_findObj(a[i]))!=null){document.MM_sr[j++]=x; if(!x.oSrc) x.oSrc=x.src; x.src=a[i+2];}
}		
				</script>
