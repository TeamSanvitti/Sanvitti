<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="productdetail.aspx.cs" Inherits="avii.product.productdetail" ValidateRequest="false"%>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="~/Controls/Header.ascx" %>
<%@ Register TagPrefix="product" TagName="productcontrol" Src="~/product/product.ascx" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>.:: Lan Global inc. Inc. - Product Detail ::.</title>
    <link rel="Stylesheet" href="../aerostyle.css" type="text/css" />

    <style type="text/css">
        
.copy11grey {
	FONT-SIZE: 11px; COLOR: #000000; FONT-FAMILY: Verdana, Arial, Helvetica, sans-serif;text-decoration: none
}
.auto-style1 {
	text-align: center;
	white-space: normal;
}


.auto-style9 {
	text-align: center;
}

.auto-style10 {
	background-image: url('../Images/Header_productlist.png');
}

.auto-style22 {
	background-image: url('../Images/footer.png');
	text-align: center;
}

.auto-style23 {
	background-color: #E9E9E9;
	text-align: center;
}

.auto-style6 {
	font-family: "Century Gothic";
	font-size: 15pt;
	text-align: center;
}
.auto-style7 {
	font-family: "Century Gothic";
	font-size: 15pt;
}

.auto-style13 {
	border-width: 0px;
}

.auto-style21 {
	text-align: right;
}

.auto-style12 {
	color: #000000;
}

.auto-style25 {
	text-align: center;
	color: #004071;
	font-family: "Century Gothic";
	font-size: large;
}

.auto-style27 {
	font-family: "Century Gothic";
	font-size: large;
	color: #004071;
	text-align: left;
}

.auto-style28 {
	text-align: justify;
	margin-top: 0;
	margin-bottom: 0;
	font-family: Arial, Helvetica, sans-serif;
}
.autostyle11 {
	text-align: left;
	margin-top: 0;
	margin-bottom: 0;
	font-size: 13px;
	font-family: Arial, Helvetica, sans-serif;
	text-decoration: none;
}
INPUT
{
	font-size: 10pt;
	font-weight: normal;
	text-decoration: none;
}
.auto-style29 {
	text-align: left;
}
.auto-style30 {
	text-align: justify;
	margin-top: 0;
	margin-bottom: 0;
	font-family: "Century Gothic";
	font-size: large;
}

.auto-style31 {
	border: 3px solid #0061FF;
	text-align: center;
}
.auto-style33 {
	border: 1px solid #0061FF;
	text-align: center;
}
.auto-style34 {
	border: 1px solid #FFFFFF;
	background-color: #D2E8FF;
}
.auto-style35 {
	border: 1px solid #FFFFFF;
	text-align: left;
	background-color: #E5E5E5;
}
.auto-style36 {
	border: 1px solid #FFFFFF;
	background-color: #E5E5E5;
}

.auto-style38 {
	text-decoration: none;
}
.image {
	border:1px solid #CCC;
	margin:0px;
	
}


</style>


    <script language="javascript" type="text/javascript">
        function OpenDocs() {
            var filename = document.getElementById("<%=hdnDoc.ClientID %>").value;

            var url = 'http://jbhargava-001-site1.htempurl.com/Documents/Products/' + filename
            if (filename != '')
                window.open(url, "_blank", "toolbar=yes, location=yes, directories=no, status=no, menubar=yes, scrollbars=yes, resizable=yes, copyhistory=yes, width=1024, height=650,left=0,top=0");
            else
                alert('No document available');


        }
        function ReplaceImage(fileName) {
            //alert(fileName)
            var imgObj = document.getElementById("<%=imgItem.ClientID %>");
            //fileName = fileName.replace('s_','l_')
            imgObj.src = '/images/products/' + fileName;
            
        }
        function getproduct_list(obj) {
           var objid = '';
            if (obj.id.indexOf('divItemThumb') > -1)
                objid = 'divItemThumb';
            else
                objid = 'divItemName';
           var itemID = document.getElementById(obj.id.replace(objid, 'hdnItemID'));
           var url = "productdetail.aspx?itemid=" + itemID.value;
           window.location = url;
            return false;
            }
            function getproductlist(obj) {
           var objid = 'lnkmaker';
           var itemID = document.getElementById(obj.id.replace(objid, 'hdnItemID'));
           var url = "productdetail.aspx?itemid=" + itemID.value;
           window.location = url;
            return false;
            }
            </script>

<style type="text/css">
   .text-1{
	font-size: 35px;
	color: #939598;
	font-family:Arial;
	text-align:left;
	margin: 0;
}
   
   .text-3{
	font-size: 18px;
	color: #ffffff;
	font-family:Arial;
	text-align:left;
	margin: 30px;
}

.text-4{
	font-size: 12px;
	color:#333;
	font-family:Arial, Helvetica, sans-serif;
	font-weight:bold;
	text-align:center;
	margin: 0;
}
.text-5{
	font-size: 12px;
	color:#FFF;
	font-family:Arial, Helvetica, sans-serif;
	font-weight:bold;
	text-align:right;
	margin: 0;
}
</style>


</head>
<body>
    <form id="form1" runat="server">

    
        <head:menuheader id="MenuHeader2" runat="server"></head:menuheader>  
    


<table width="100%" border="0" cellspacing="0" cellpadding="0">
  <tr>
    <td bgcolor="#CCCCCC"><table width="1300" border="0" align="center" cellpadding="0" cellspacing="0">
      <tr>
        <td rowspan="2" align="left" valign="middle" bgcolor="#000066"><table width="100%" border="0" cellspacing="8" cellpadding="8">
          <tr>
            <td class="whitetx1"><p class="text-3">Our expert knowledge and vast experience in the wireless industry provides everyday solutions for our customers and partners. It is this knowledge that pushes Lan Global inc. into the future of wireless services.
    <br>We believe in customer satisfaction and we are the team to work with you and for you to make your success, our success.</br></p>
		
             
            
              <p></p></td>
          </tr>

        </table></td>
        <td height="55" align="center" valign="middle" bgcolor="#999999"><span class="whitehd">Product List</span></td>
      </tr>
      <tr>
        <td width="300" valign="top"><img src="/images/Aerovoice_product.png" width="300" height="172"></td>
      </tr>
    </table></td>
  </tr>
</table>    
 <%--<table width="1300" border="0" align="center">
  <tr>
    <td colspan="5">&nbsp;</td>
  </tr>
  
  <tr>
    <td colspan="4" bgcolor="#000066"><p class="text-3">Our expert knowledge and vast experience in the wireless industry provides everyday solutions for our customers and partners. It is this knowledge that pushes Lan Global into the future of wireless services.
    <br>We believe in customer satisfaction and we are the team to work with you and for you to make your success, our success.</br></p></td>
    <td><img src="/images/Aerovoice_product.png" alt="Aerovoice_Product" width="300" height="231" align="middle"></td>
  </tr>
  </table>--%>
    <table width="1300" border="0" align="center" cellpadding="0" cellspacing="0">
    <%--<tr>
		<td>&nbsp;</td>
		<td>
		&nbsp;</td>
	</tr>
	--%>
  <tr>
    <td align="left" width="1000">
    <table width="100%" border="0" cellspacing="8" cellpadding="8">
    
      <tr>
        <td class="bodytext1" >

        
        <p class="auto-style28">
        <table width="100%" border="0" >
        <tr style="height:5px; line-height:7px">
            <td height="5" class="copy11grey"> 
            &nbsp; >> <a class="copy11grey" href="productlist.aspx">OEMs</a> >> <asp:LinkButton ID="lnkMaker" CssClass="copy11grey" runat="server" onclick="lnkMaker_Click"></asp:LinkButton>
              <asp:Label id="lblCID"  CssClass="copy11grey" Text=" >> "   runat="server"/>
                    
                    <asp:LinkButton ID="lnkCategory" CssClass="copy11grey" runat="server" OnClick="lnkCategory_Click" ></asp:LinkButton>
              <asp:Label id="lblItem"  CssClass="copy11grey" Text=" >> "   runat="server"/>
               <asp:Label id="lbl_Item"  CssClass="copy11grey"    runat="server"/>
             
                   
            </td>
        </tr>
        </table>
        </p>
        <p class="auto-style28">
		                &nbsp; <img alt="" height="5" src="../Images/bar_bg.png" width="600" /></p>
                            <table cellspacing="0" style="width: 100%">
                            <tr valign="top">
				            <td style="width: 75px"; >&nbsp;</td>
				            <td >
		                         &nbsp;
                            </td>
                            <td style="width: 105px">&nbsp;</td>
				            <td   style="width: 180px;" >&nbsp;
                            </td>
                            <td style="width: 25px">&nbsp;</td>
                            <td>
                                <table cellspacing="0" style="width: 100%" border="0">
			                    <tr> 
                                    <td  style="width: 350px">
                                            <strong><asp:Label  CssClass="auto-style30" ID="lblItemName" runat="server">
                                    </asp:Label></strong> 
				                    </td>
                                    <td align="right">
                                        <asp:HiddenField ID="hdnDoc" runat="server" />
                                        <asp:ImageButton ID="imgDoc" AlternateText="Print Document" ToolTip="Print Document" ImageUrl="~/images/printer.png" Visible="false" OnClientClick="return OpenDocs();" runat="server" />
                                    </td>
                                 </tr>
                                 </table>
                             </td>

                            </tr>
                            </table>

                    <table cellspacing="0" style="width: 100%">
			        <tr valign="top">
				        <td style="width: 15px; height:258px">&nbsp;</td>
				        <td class="auto-style31"  style="width:258px; height: 258px">
		                     <asp:Image ID="imgItem" Visible="false" onmouseover="this.style.cursor='pointer'"  runat="server"   width="250px" Height="345"  />
                        </td>
                        <td style="width: 15px">&nbsp;</td>
				        <td class="auto-style33"  style="width: 80px; height: 80px;">
                            <asp:DataList  Visible="true" ID="dlItemImages" RepeatColumns="1"  
                             RepeatDirection="Vertical"  runat="server" CellPadding="0" CellSpacing="0" Width="100%"> 
                            <ItemStyle HorizontalAlign="Left" />
                            <ItemTemplate>
                            <table border="0" cellspacing="0" cellpadding="0" width="100%">
                            <tr valign="top"> 
                                <td class="auto-style33"  style="width: 80px; height: 80px;">
                                    <img width="80" height="80" onmouseover="ReplaceImage('<%# "L_" + Eval("ImageURL") %>')" class="image" src='<%# Convert.ToString(Eval("ImageURL"))==""? "/images/products/comingsoon.jpg": "/images/products/s_" + Eval("ImageUrl") %>' alt=""  />                    
                                </td>
                            </tr>
                            </table>
                            </ItemTemplate>
                            </asp:DataList>
                        </td>
				        <td style="width: 15px">&nbsp;</td>
                        <td >
                            <table cellspacing="0" style="width: 100%" border="0">
			                <tr> 
                                <td class="auto-style36" style="height: 40px; width: 200px">
				                <strong>&nbsp; Product Code: </strong></td>  
                                <td class="auto-style34" style="height: 40px">
                                <asp:Label CssClass="autostyle11"  ID="lblCode" runat="server"></asp:Label>
                                 </td>
                            </tr>
                            <tr>
				                <td class="auto-style36" style="height: 40px; width: 200px">
				                <strong>&nbsp; Model#:</strong></td>
				                <td class="auto-style34" style="height: 40px">
                                <asp:Label CssClass="autostyle11" ID="lblModel" runat="server"></asp:Label>
                                
                                </td>
			                </tr>
                            <tr>
				                <td class="auto-style36" style="height: 40px; width: 200px">
				                <strong>&nbsp; Product Condition:</strong></td>
				                <td class="auto-style34" style="height: 40px">
                                <asp:Label CssClass="autostyle11" ID="lblProdCond" runat="server"></asp:Label>
                                
                                </td>
			                </tr>
                            <tr>
                                <td class="auto-style36" style="height: 40px; width: 200px">
				                <strong>&nbsp; Carriers:</strong></td>
				                <td class="auto-style34" style="height: 40px">
                                <asp:Label CssClass="autostyle11" ID="lblTechnology" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style36" style="height: 40px; width: 200px">
				                <strong>&nbsp; UPC:</strong></td>
				                <td class="auto-style34" style="height: 40px">
                                <asp:Label CssClass="autostyle11" ID="lblUPC" runat="server"></asp:Label>
                                
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style36" style="height: 40px; width: 200px">
				                <strong>&nbsp; Color:</strong></td>
				                <td class="auto-style34" style="height: 40px">
                                <asp:Label CssClass="autostyle11" ID="lblColor" runat="server"></asp:Label>
                                
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style35" style="height: 120px; width: 200px" valign="top" rowspan="2">
				                <strong>&nbsp; Description: </strong></td>
				                <td class="auto-style34" style="height: 120px" valign="top" rowspan="2">
				                <asp:Label ID="lblItemDesc"  runat="server" CssClass="autostyle11" ></asp:Label><br />
                                <asp:Label ID="lblFullDesc"  runat="server" CssClass="autostyle11" ></asp:Label>
                                </td>
                            </tr>
                            </table>
                        </td>
				        </tr>
                        <tr style="display:none">
                                <td colspan="2" style="display:none">
                                
                                    <asp:DataList ID="dlItemSpecifications"  runat="server">
                                    <HeaderTemplate><span class="copy11">Specifications:</span></HeaderTemplate>
                                        <ItemTemplate>
                                            &nbsp;- <asp:Label ID="lblSpecification" runat="server" CssClass="copy10grey" Text='<%# Eval("Specificaiton") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:DataList>
                                
                                </td>
                            </tr>
                            <tr style="display:none">
                                <td colspan="2" style="display:none">
                                
                                
                                <asp:DataList ID="dlItemAttributes"  runat="server">
                                                 
                                    <HeaderTemplate><span class="copy11"> Detailed Specifications:</span> </HeaderTemplate>
                                    <ItemTemplate>
                                    <table><tr><td>
                                        <asp:Label ID="lblAttributes" runat="server" CssClass="copy10grey" Text='<%# Convert.ToString(DataBinder.Eval( Container.DataItem, "AttributeValue")) != "" ? "-"+Convert.ToString(DataBinder.Eval( Container.DataItem, "AttributeName"))+":"+Convert.ToString(DataBinder.Eval( Container.DataItem, "AttributeValue")):DataBinder.Eval( Container.DataItem, "AttributeValue")%>'></asp:Label>
                                        </td></tr></table>
                                    </ItemTemplate>
                                </asp:DataList>
                                </td>
                            </tr>
                    </table>
</td>
      </tr>
    </table>
    </td>
    <td width="300" align="center" valign="top" bgcolor="#E5E5E5">
    <p ><product:ProductControl id="ProductControl1" runat="server"/>             
     
          </p>
          
    </td>
  </tr>
  <%--<tr>
		<td class="auto-style23" style="width: 300px" valign="top">
		&nbsp;</td>
		<td style="width: 900px">
		&nbsp;</td>
	</tr>--%>
</table>

<foot:MenuFooter ID="footer" runat="server"></foot:MenuFooter>
<%--
<table align="center" cellspacing="0" style="width: 1200px; height: 40px">
	<tr>
		<td class="auto-style22" valign="bottom">&nbsp;</td>
	</tr>
	</table>
<table style="width: 1200px" align="center">
	<tr>
		<td class="auto-style9" style="width: 1200px" valign="bottom">
		<img alt="" height="46" src="../../Images/Samsung.jpg" width="138" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
		<img alt="" height="46" src="../../Images/PCD%20logo.jpg" width="122" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
		<img alt="" height="46" src="../../Images/ZTE.jpg" width="69" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
		<img alt="" height="46" src="../../Images/novatel_wireless.jpg" width="77" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
		<img alt="" height="46" src="../../Images/LG.jpg" width="109" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
		<img alt="" height="46" src="../../Images/Cradlepoint.jpg" width="78" /></td>
	</tr>
</table>

<table align="center" cellspacing="0" style="width: 1200px; height: 40px">
	<tr>
		<td class="auto-style21" style="width: 1200px" valign="bottom">
		<a href="http://www.aerovoice.com/newaerovoice/terms.htm" class="auto-style38">
		<span class="auto-style12"><strong>Terms &amp; Conditions</strong></span></a><span class="auto-style12"><strong> 
		| </strong></span>
		<a href="http://www.aerovoice.com/newaerovoice/policy.htm" class="auto-style38">
		<span class="auto-style12"><strong>Privacy Policy</strong></span></a><span class="auto-style12"><strong> 
		| </strong></span>
		<a href="http://www.aerovoice.com/newaerovoice/index.htm" class="auto-style38">
		<span class="auto-style12"><strong>Home</strong></span></a></td>
	</tr>
</table>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>   --%>                     
                   
                   
    
    <script language="javascript" type="text/javascript">
        var searchHeader = document.getElementById('searchHeader');
        searchHeader.innerHTML = "Products";
    </script>
        
        </form>
</body>
</html>
