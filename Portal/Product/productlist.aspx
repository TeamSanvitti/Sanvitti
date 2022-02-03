<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="productlist.aspx.cs" Inherits="avii.product.productlist"  ValidateRequest="false"  %>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="~/Controls/Header.ascx" %>
<%@ Register TagPrefix="product" TagName="productcontrol" Src="../product/product.ascx" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%--<script runat="server">

    protected void DLitems_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
</script>--%>



<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Product List</title>
    
<link href="../aerostyle.css" rel="stylesheet" type="text/css" />
    <style type="text/css">


.button {   
	border: 1px solid #ffffff;
    FONT-WEIGHT: bold; FONT-SIZE: 10px; TEXT-TRANSFORM: uppercase; COLOR: #ffffff; LINE-HEIGHT: 16px; FONT-FAMILY: Verdana, Arial, Helvetica, sans-serif; BACKGROUND-COLOR: navy;
    margin-bottom: 5px;
    
}
.auto-style1 {
	text-align: center;
	white-space: normal;
}


.auto-style9 {
	text-align: center;
}

.auto-style10 {
	background-image: url('/Images/Header_productlist.png');
}

.auto-style22 {
	background-image: url('Images/footer.png');
	text-align: center;
}

.auto-style18 {
	color: #004071;
	font-family: "Century Gothic";
	font-size: x-large;
}
.auto-style16 {
	font-family: Arial, Helvetica, sans-serif;
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

.auto-style24 {
	text-align: justify;
	margin-top: 0;
	margin-bottom: 0;
}

.auto-style21 {
	text-align: right;
	font-size: small;
	font-family: Arial, Helvetica, sans-serif;
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

.auto-style29 {
	text-decoration: none;
}

.auto-style1 {
	text-align: center;
	white-space: normal;
}


.auto-style9 {
	text-align: center;
}

.auto-style10 {
	background-image: url('/Images/Header_productlist.png');
}

.auto-style22 {
	background-image: url('/Images/footer.png');
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
.auto-style29b {
	text-align: center;
	background-image: url('/Images/P_Name_BG.png');
}
INPUT
{
	font-size: 10pt;
	font-weight: normal;
	text-decoration: none;
}
.auto-style30 {
	font-family: "Century Gothic";
	font-size: small;
	color: #FFFFFF;
}
.auto-style31 {
	border-width: 0;
	text-align: center;
	background-image: url('/Images/P_Pic_BG.png');
}

.auto-style33 {
	text-decoration: none;
}
.copy11grey {
	FONT-SIZE: 11px; COLOR: #000000; FONT-FAMILY: Verdana, Arial, Helvetica, sans-serif;text-decoration: none
}
</style>
<%--
 <link rel="StyleSheet" href="/css/menu_2.css" />--%>

<%--    <LINK href="../../aerostyle.css" type="text/css" rel="stylesheet">
    <link href="../Styles.css" type="text/css" rel="stylesheet" />--%>

    <script language="javascript" type="text/javascript">
        function getproduct_list(obj) {
            var objid = '';
            if (obj.id.indexOf('divItemThumb') > -1)
                objid = 'divItemThumb';
            else
                objid = 'lnkmaker';
            var itemID = document.getElementById(obj.id.replace(objid, 'hdnItemID'));
            var url = "productdetail.aspx?itemid=" + itemID.value;
            window.location.href = url;
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
<body leftmargin="0" rightmargin="0" topmargin="0" >
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
  </table>
--%>
 <table width="1300" border="0" align="center" cellpadding="0" cellspacing="0">
  <tr>
    <td align="left" width="1000">
    <table width="100%" border="0" cellspacing="8" cellpadding="8">
      <tr>
        <td class="bodytext1" >

        <p class="auto-style24"><span class="auto-style18"><%--<strong>Products</strong></span>--%>
        
        <span class="auto-style16"><br />
		
       <asp:Panel ID="PanelMakers" runat="server" Visible="true" Width="100%"   >
                    
                                <asp:DataList ID="dlMaker" runat="server"  RepeatColumns="4"  
                                RepeatDirection="Horizontal" >
                                <ItemTemplate>
                                    <table align="center" cellspacing="0">
                                    <tr>
                                        <td class="auto-style9" style="width: 200px; height: 200px">
                                        
                                      <a href='<%# "productlist.aspx?maker=" + Eval("MakerGUID") %>'  >
                                        <img  height="170" title='<%# Eval("MakerName") %>' alt='<%# Eval("MakerName") %>' src='<%# Convert.ToString(Eval("MakerImage")) =="" ? "/images/makers/ComingSoon.jpg" :  "/images/makers/" + Eval("MakerImage") %>'  width="170" class="auto-style13"  />

                                      </a>
                                      </td>
                                      </tr>
                                      </table>
                                </ItemTemplate>
                               
                                </asp:DataList>
                    
                    </asp:Panel>
        </span></p>

        
        <p class="auto-style28">
        
    <table  cellpadding="0" cellspacing="0" border="0" align="left" width="100%">
            				
			<tr valign="top" >				
				<td  align="left">								
                    
       
				    
                  <asp:Panel ID="PanelProducts" Visible="false" runat="server" Width="100%">

                    &nbsp;&nbsp;
                     <a class="copy11grey" href="productlist.aspx"> >> OEMs</a>
                     <asp:Label id="lblOEM" Visible="false" CssClass="copy11grey" Text=" >> "   runat="server"/>
                    
                    <asp:LinkButton ID="lnkMaker" CssClass="copy11grey" runat="server" onclick="lnkMaker_Click"></asp:LinkButton>
                    <asp:Label id="lblCID" Visible="false" CssClass="copy11grey" Text=" >> "   runat="server"/>
                    
                    <asp:LinkButton ID="lnkCategory" CssClass="copy11grey" runat="server" ></asp:LinkButton>
                     
                    
                      <%--<asp:HiddenField ID="hdnMaker" runat="server"/>
                      <asp:Label id="Label1" CssClass="copy11grey" Text=" >> " Visible="false"  runat="server"/>
                    <asp:LinkButton ID="lnkTechnology" CssClass="copy11grey" runat="server" onclick="lnkTechnology_Click"></asp:LinkButton>
                     <asp:Label id="Label2" CssClass="copy11grey" Text=" >> "  Visible="false" runat="server"/>
                    <asp:LinkButton ID="lnkModel" CssClass="copy11grey" runat="server" ></asp:LinkButton>
                     --%>
                     <table border="0">
			            <tr>
			            <td align="left">
                            <asp:Label ID="lblMsg" runat="server" CssClass="errormessage"></asp:Label></td>
                            <td  align="right"  class="copy10grey" >
                            <asp:LinkButton  id="btnPrev" CausesValidation="false" CssClass="copy11grey" ToolTip="Previous"  Text="Prev" OnClick="Prev_Click" runat="server"/>
                            <asp:Label id="lblCurrentPage"  runat="server"/>
                            <asp:LinkButton id="btnNext" Text="Next" CssClass="copy11grey" CausesValidation="false" ToolTip="Next" OnClick="Next_Click" runat="server"/>
                            &nbsp;&nbsp;&nbsp;
                        </td>
                        </tr>	
                        <tr valign="top">
            
			            
           
                        <td  valign="top" class="auto-style9" colspan="2">
                                <asp:Panel ID="pnlMsg" Visible="false" runat="server" Width="100%">
                                <br />
                                        <br />
                                         <br />
                                        <br />
                                        
                            <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="45%" align="center">
                            <tr >
                            <td>
                                <table align="center" >
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblMessage" CssClass="errormessage"  runat="server" ></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                </table>

                            </td>
                            </tr>
                            </table>
                             <br />
                                        <br />
                                        <br />
                                </asp:Panel>
                                 
                                  <asp:DataList ID="dlCategory" runat="server" Width="100%"
                                    RepeatColumns="5" RepeatDirection="Horizontal"  ItemStyle-HorizontalAlign="Left"  
                                      ItemStyle-VerticalAlign="top"  >
                                    <ItemTemplate>
                                                                                       
                                                <table cellspacing="0" width="100%" align="left">

			                                    <tr >
				                                    <td class="auto-style31" style="width: 149px; height: 149px">
                                                    <div runat="server" id="divItemThumb" style="cursor:pointer; " 
                                                        >
                                                         <a href='<%# "productlist.aspx?maker=" + Eval("MakerGUID") +"&t="+Eval("CarrierID")+"&cid="+Eval("CategoryGUID") %>'  >
                                     
                                                        <img id="imgCat" src='<%# Convert.ToString(Eval("CategoryImage")) == "" ? "/images/category/comingsoon.jpg" : "/images/category/" + Eval("CategoryImage") %>'

                                                        height="100" width="100" class="auto-style13" alt=""  />
                                                      
                                                            </a>
                                                            </div>
                                                            
                                                    </td>
                                                </tr>
                                                <tr>
				                                <td class="auto-style29b" style="height: 40px; width: 149px">
				                                <div runat="server" id="divItemThumb1" style="cursor:pointer;" 
                                                        >

					                                <span class="auto-style30"><strong>
                                                      <%#DataBinder.Eval(Container.DataItem, "CategoryName")%></strong><%--<br>(<%#DataBinder.Eval(Container.DataItem, "ItemMaker")%>)--%>
                                                         
					                                </span><strong>
					                                        
					                                </strong></div>
				                                </td>
                                               </tr>
                                               <tr>
				                                <td class="auto-style9" style="height: 5px; " >
				                                    &nbsp;</td>
			                                    </tr>
                                               </table>
                                        
                                    </ItemTemplate>
                                    <ItemStyle VerticalAlign="Top"></ItemStyle>

                                    </asp:DataList>
                       
                                <asp:DataList ID="DLitems" runat="server" Width="100%"
                                    RepeatColumns="5" RepeatDirection="Horizontal"  ItemStyle-HorizontalAlign="Left"  
                                      ItemStyle-VerticalAlign="top"  OnItemDataBound="dlProducts_ItemDataBound">
                                    <ItemTemplate>
                                                                                       
                                                <table cellspacing="0" width="100%" align="left">

			                                    <tr >
				                                    <td class="auto-style31" style="width: 149px; height: 149px">
                                                    <div runat="server" id="divItemThumb" style="cursor:pointer; " 
                                                        onclick="getproduct_list(this);">
                                                        
                                                        <asp:Image runat="server"  ID="imgItemThumb" AlternateText ='<%#DataBinder.Eval( Container.DataItem, "ItemName") %>' Height="100" Width="100" class="auto-style13"  />
                                                      
                                                            
                                                            </div>
                                                            
                                                    </td>
                                                </tr>
                                                <tr>
				                                <td class="auto-style29b" style="height: 40px; width: 149px">
				                                <div runat="server" id="divItemThumb1" style="cursor:pointer; " 
                                                        onclick="getproduct_list(this);">

					                                <span class="auto-style30"><strong>
                                                      <%#DataBinder.Eval(Container.DataItem, "ItemName")%></strong><br>(<%#DataBinder.Eval(Container.DataItem, "ItemMaker")%>)
                                                         
					                                </span><strong>
					                                <asp:HiddenField ID="hdnItemID" runat="server" Value='<%# DataBinder.Eval( Container.DataItem, "ItemGUID")%>' />
                                                            
					                                </strong></div>
				                                </td>
                                               </tr>
                                               <tr>
				                                <td class="auto-style9" style="height: 5px; " >
				                                    &nbsp;</td>
			                                    </tr>
                                               </table>
                                        
                                    </ItemTemplate>
                                    <ItemStyle VerticalAlign="Top"></ItemStyle>

                                    <FooterTemplate>
                                    
                                    
                                    </FooterTemplate>
                                    
                                </asp:DataList>
                                
               
			
            </td>
        </tr></table>
                     </asp:Panel>
				</td>
				
				</tr>
				
				
       
        </table>
        
        </p>

</td>
      </tr>
    </table>
    </td>
    <td width="300" align="center" valign="top" bgcolor="#E5E5E5">
    <p ><product:ProductControl id="productid" runat="server"/>              
     
          </p>
          
    </td>
  </tr>
</table>
<%--<table align="center" cellspacing="0" style="width: 1300px">
	<tr>
		<td>&nbsp;</td>
		<td>
		&nbsp;</td>
	</tr>
	<tr>
		<td class="auto-style23" style="width: 300px" valign="top">
        
        </td>
        <td style="width: 900px">--%>
		
<%--         <p class="auto-style28">
        
    <table  cellpadding="0" cellspacing="0" border="0" align="left" width="100%">
            				
			<tr valign="top" >				
				<td  align="left">		
                     <asp:Panel ID="Panel1" Visible="false" runat="server" Width="100%">
                        <asp:DataList ID="dlCategory" runat="server" Width="100%"
                                    RepeatColumns="5" RepeatDirection="Horizontal"  ItemStyle-HorizontalAlign="Left"  
                                      ItemStyle-VerticalAlign="top"  OnItemDataBound="dlProducts_ItemDataBound">
                                    <ItemTemplate>
                                                                                       
                                                <table cellspacing="0" width="100%" align="left">

			                                    <tr >
				                                    <td class="auto-style31" style="width: 149px; height: 149px">
                                                    <div runat="server" id="divItemThumb" style="cursor:pointer; " 
                                                        onclick="getproduct_list(this);">
                                                        
                                                        <asp:Image runat="server"  ID="imgItemThumb" AlternateText ='<%#DataBinder.Eval( Container.DataItem, "ItemName") %>' Height="100" Width="100" class="auto-style13"  />
                                                      
                                                            
                                                            </div>
                                                            
                                                    </td>
                                                </tr>
                                                <tr>
				                                <td class="auto-style29b" style="height: 40px; width: 149px">
				                                <div runat="server" id="divItemThumb1" style="cursor:pointer; " 
                                                        onclick="getproduct_list(this);">

					                                <span class="auto-style30"><strong>
                                                      <%#DataBinder.Eval( Container.DataItem, "ItemName") %></strong><br>(<%#DataBinder.Eval( Container.DataItem, "ItemMaker") %>)
                                                         
					                                </span><strong>
					                                <asp:HiddenField ID="hdnItemID" runat="server" Value='<%# DataBinder.Eval( Container.DataItem, "ItemGUID")%>' />
                                                            
					                                </strong></div>
				                                </td>
                                               </tr>
                                               <tr>
				                                <td class="auto-style9" style="height: 5px; " >
				                                    &nbsp;</td>
			                                    </tr>
                                               </table>
                                        
                                    </ItemTemplate>
                                    <ItemStyle VerticalAlign="Top"></ItemStyle>

                                    </asp:DataList>
                                
                     </asp:Panel>
                </td>
                </tr>
                </table>
                </p>
--%>

		<%--</td>

    </tr>
    <tr>
		<td class="auto-style23" style="width: 300px" valign="top">
		&nbsp;</td>
		<td style="width: 900px">
		&nbsp;</td>
	</tr>

    </table>

--%>
<foot:MenuFooter ID="footer" runat="server"></foot:MenuFooter>  
                                
    <script type="text/javascript">

        function test() {
            var arr = document.getElementsByTagName('img');
            var obj;

            for (i = 0; i < arr.length; i++) {
                obj = arr[i];

                if (obj.id.indexOf('imgItemThumb') > -1) {
                    var imgSrc = obj.src;
                    imgSrc = obj.src.replace('\images', '\new\images');
                    obj.src = imgSrc

                }
            }
        }
        //test();
    </script>
    </form>
</body>
</html>
	