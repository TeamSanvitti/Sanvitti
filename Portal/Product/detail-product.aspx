<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="detail-product.aspx.cs" Inherits="avii.product.detail_product" ValidateRequest="false"  %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>
<%@ Register TagPrefix="menu" TagName="Menu" Src="~/Controls/Header.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Admin - Manage Products</title>
     <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>

    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.4.0/jquery.min.js"></script>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/jquery-ui.min.js"></script>
    <link href="https://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/themes/start/jquery-ui.css"
    rel="stylesheet" type="text/css" />
<%--<script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/jquery-ui.js" type="text/javascript"></script>
<link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/themes/start/jquery-ui.css"
    rel="stylesheet" type="text/css" />--%>
	<%--<script type="text/javascript" src="../JQuery/jquery.min.js"></script>
    <script type="text/javascript" src="../JQuery/jquery-ui.min.js"></script>--%>
	<script type="text/javascript" src="../JQuery/jquery.blockUI.js"></script>
	
    <link href="../../aerostyle.css" type="text/css" rel="stylesheet" />
    
    <link href="ddcolortabs.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript" src="ddtabmenu.js"></script>
    
    <link href="../dhtmlwindow.css" type="text/css" rel="stylesheet" />
	<link rel="stylesheet" href="../dhtmlxwindow/style.css" type="text/css" media="screen" />
    <link rel="stylesheet" type="text/css" href="../dhtmlxwindow/dhtmlxwindows.css"/>
	<link rel="stylesheet" type="text/css" href="../dhtmlxwindow/skins/dhtmlxwindows_dhx_skyblue.css"/>
	
	<script src="../dhtmlxwindow/dhtmlxcommon.js"></script>
	<script src="../dhtmlxwindow/dhtmlxwindows.js"></script>
	<script src="../dhtmlxwindow/dhtmlxcontainer.js"></script>
    <script type="text/javascript" language="javascript">
    var dhxWins, w1;
    function doOnLoad() {
		dhxWins = new dhtmlXWindows();
		dhxWins.enableAutoViewport(false);
		dhxWins.attachViewportTo("winVP");
		dhxWins.setImagePath("../../codebase/imgs/");
		w1 = dhxWins.createWindow("w1", 320, 100, 465,350);
		w1.setText("Add/Edit Attribute");
		w1.attachURL("Attribute.aspx?flag=1");

    }
    function displayImage(imgurl) {
        dhxWins = new dhtmlXWindows();
        dhxWins.enableAutoViewport(false);
        dhxWins.attachViewportTo("winVP");
        dhxWins.setImagePath("../../codebase/imgs/");
        w1 = dhxWins.createWindow("w1", 320, 100, 450, 430);
        w1.setText("View Image");
        w1.attachURL("imgpreview.aspx?url=../images/products/"+imgurl);

    }
	
    </script>
    
    <script language="javascript" type="text/javascript">
        function ValidateCamera() {
            var objCameraType = document.getElementById('ddlCameraType');
            var CameraType = objCameraType.options[objCameraType.selectedIndex].value;

            if (CameraType == 0) {
                alert('Please select a camera type!');
                return false;
            }
            var objCameraConfig = document.getElementById('ddlCameraConfig');
            var CameraConfig = objCameraConfig.options[objCameraConfig.selectedIndex].value;

            if (CameraConfig == 0) {
                alert('Please select a camera config!');
                return false;
            } 
        }
function ValidatePrice(evt, obj) {
            var charCodes = evt.keyCode ? evt.keyCode : evt.which ? evt.which : evt.charCode;
            //alert(charCodes);
            var priceValue = obj.value;
            //alert(priceValue);
            //alert(priceValue.indexOf('.'))
            //if ((charCodes == 46 && priceValue.indexOf('.') > -1) || (charCodes == 190 && priceValue.indexOf('.') > -1) || (charCodes < 48 && charCodes == 46 && priceValue.indexOf('.') > -1) || (charCodes > 57 && charCodes != 190)) {
            if ((charCodes == 46 && priceValue.indexOf('.') > -1) || (charCodes < 48 && charCodes != 46) || charCodes > 57) {
                //charCodes = 0;
                ///priceValue = priceValue.replace('..', '.');
                //obj.value = priceValue;

                evt.preventDefault();
                //alert('in');
                return false;
            }
            //else

            return true;


        }

        function formatParentCatDropDown(objddl) {

            for (i = 0; i < objddl.options.length; i++) {
                objddl.options[i].innerHTML = objddl.options[i].innerHTML.replace(/&amp;/g, '&');
            }
        }
       
    function checkForEmptyPrice()
    {
        var WebPrice=document.getElementById('txtWebPrice').value;
        if(WebPrice=='')
        {
            alert('WebPrice can not be empty!');
            return false;
        }
    }
    function checkForEmptyImage() {

        var ImageGUID = document.getElementById('hdnImageGUID').value;
        if (ImageGUID == '') {
            var img2 = document.getElementById('<%= fupItemImg2.ClientID %>').value;
            var itemImage = document.getElementById('<%= fupItemImage1.ClientID %>').value;
            if (itemImage == '') {
                alert('Image 250*345 can not be empty!');
                return false;
            }
            if (img2 == '') {
                alert('Image 100*100 can not be empty!');
                return false;
            }
        }
    }
    function checkForEmptyspecification() {
        var itemspec = document.getElementById('txtSpecification').value;
        if (itemspec == '') {
            alert('Specification can not be empty!');
            return false;
        }
    }
    function checkForEmptyAttribute() {
        var attributeList = document.getElementById('DlAttributeValue');
        var listrows = attributeList.getElementsByTagName('input');
        var attrflag = false;
        

        for (var i = 0; i < listrows.length; i++) {
            if (listrows[i].id.indexOf('txtAttributeValue') > -1) {
                if (listrows[i].value != '') {
                    attrflag = true;    
                }
            }

        }


        if (attrflag == false) {
            alert('There is no Attribute to insert!');
            return false;
        }
        return true;
    }

    function callItemcompanyskuUpdate(obj) {
        
        objitemSKU = document.getElementById(obj.id.replace('lnkItemcompskuUpdate', 'hdnItemcompanyGUID'));
        document.getElementById('hdnItemcompskuGUID').value = objitemSKU.value;
        objcompanyID = document.getElementById(obj.id.replace('lnkItemcompskuUpdate', 'hdnCompanyID'));
        objddl = document.getElementById("<%=ddlCompany.ClientID %>");

        for (i = 0; i < objddl.options.length; i++) {
            if (objcompanyID.value == objddl.options[i].value)
                objddl.options[i].selected = true;
        }
        objSKU = document.getElementById(obj.id.replace('lnkItemcompskuUpdate', 'lblSKU'));

        objMASSKU = document.getElementById(obj.id.replace('lnkItemcompskuUpdate', 'lblMASSKU'));
        objWarehouseCode = document.getElementById(obj.id.replace('lnkItemcompskuUpdate', 'lblWhCode'));

       // objIsFinishedSKU = document.getElementById(obj.id.replace('lnkItemcompskuUpdate', 'hdIsFinishedSKU'));

        document.getElementById('txtSKU').value = objSKU.innerHTML;
        document.getElementById('txtMasSKU').value = objMASSKU.innerHTML;
        //document.getElementById('chkSKU').checked = objIsFinishedSKU.innerHTML;

        objddlWhCode = document.getElementById("<%=ddlWhCode.ClientID %>");

        for (i = 0; i < objddlWhCode.options.length; i++) {
            if (objWarehouseCode.innerHTML == objddlWhCode.options[i].value)
                objddlWhCode.options[i].selected = true;
        }
        //objWarehouseCode.innerHTML;
        
    
    }
    
    function callPricingUpdate(obj)
    {
        objWebPriceGUID = document.getElementById(obj.id.replace('lnkPricingUpdate', 'hdnpricingID'));
        document.getElementById('hdnpricingGUID').value = objWebPriceGUID.value;
        
        

        objWebPrice = document.getElementById(obj.id.replace('lnkPricingUpdate', 'lblWebPrice'));
        
        document.getElementById('txtWebPrice').value = objWebPrice.innerHTML;
        objRetialPrice = document.getElementById(obj.id.replace('lnkPricingUpdate', 'lblRetialPrice'));
        document.getElementById('txtRetailPrice').value = objRetialPrice.innerHTML;
        
        objWholesalePrice = document.getElementById(obj.id.replace('lnkPricingUpdate', 'lblWholesalePrice'));
        document.getElementById('txtWholesalePrice').value = objWholesalePrice.innerHTML;
        
        objPriceType = document.getElementById(obj.id.replace('lnkPricingUpdate', 'lblPriceType'));
        var priceType;
        if('Regular'==objPriceType.innerHTML)
            priceType=1;
        else
            priceType=2;    
            
        document.getElementById('ddlPricetype').value = priceType;
        
        
        
        return false;
    }
    function callSpecificationUpdate(obj)
    {
        objSpecificationGUID = document.getElementById(obj.id.replace('lnkSpecificationUpdate', 'hdnSpecificationID'));
        document.getElementById('hdnSpecificationGUID').value = objSpecificationGUID.value;
        objspecifications = document.getElementById(obj.id.replace('lnkSpecificationUpdate', 'lblSpecification'));
        document.getElementById('txtSpecification').innerHTML = objspecifications.innerHTML;
        return false;
    
    }
    
    
    
    
    function callImageUpdate(obj) {
            objImageGUID = document.getElementById(obj.id.replace('lnkImageUpdate', 'hdnImageID'));
            document.getElementById('hdnImageGUID').value = objImageGUID.value;
            
            objImageDescription = document.getElementById(obj.id.replace('lnkImageUpdate', 'hdnImagetxt'));
            
            document.getElementById('txtImageDesc1').value = objImageDescription.value;
            
            objImageType = document.getElementById(obj.id.replace('lnkImageUpdate', 'lblImagetype'));
            
            var imageType;
            if('Main'==objImageType.innerHTML)
                imageType=0;
            if('front'==objImageType.innerHTML)
                imageType=1;
            
            if('Accessories'==objImageType.innerHTML)
                imageType=2;
            if('Other'==objImageType.innerHTML)
                imageType=3;
            
            document.getElementById('ddlImageType1').value = imageType;
            return false;
        }
    
    function DisplayTab()
    {
        document.getElementById('tabTD').style.display="block"; 
    }
    function openpopUp(url) {
        alert(url);
	}
   
    function isNumberKey(evt)
        {
            
            var charCodes = evt.keyCode ? evt.keyCode : evt.which ? evt.which : evt.charCode;
            
           if (charCodes > 31 && (charCodes < 48 || charCodes > 57)&& charCodes!=46)
           {
           
               charCodes = 0;
               return false;
           }
           
           return true;
        }
        function isNumberOnly(evt) {

            var charCodes = evt.keyCode ? evt.keyCode : evt.which ? evt.which : evt.charCode;

            if (charCodes > 31 && (charCodes < 48 || charCodes > 57)) {

                charCodes = 0;
                return false;
            }

            return true;
        }

        function isNumberHiphen(evt) {

            var charCodes = evt.keyCode ? evt.keyCode : evt.which ? evt.which : evt.charCode;
            if (charCodes > 31 && (charCodes < 48 || charCodes > 57) && charCodes != 45) {
                charCodes = 0;
                return false;
            }

            return true;
        }
        function checkForEmptySKU() {
            var objCompany = document.getElementById('ddlCompany');
            var Companyvalue = objCompany.options[objCompany.selectedIndex].value;

            if (Companyvalue == 0) {
                alert('Please select a company!');
                return false;
            }
            var SKU = document.getElementById('txtSKU').value;
            if (SKU == '') {
                alert('SKU can not be empty!');
                return false;
            }
        }
        function CategoryOnchange(obj) {
            var categoryType = document.getElementById('ddlCategoryType');
            var categoryvalue = categoryType.options[categoryType.selectedIndex].value;
            //alert(categoryvalue)
            //if (categoryvalue > 0) 
            {

                //alert(categoryType.value)
                var categorywithproduct = categoryType.value
                var arr = categorywithproduct.split('|');
                if (arr.length > 1) {  //alert(arr)
                    //alert(arr[1])
                    if (arr[1] == 0) {
                        alert('Please select leaf category!')
                       // alert('Product not allowed for this category!')
                        // categoryType.options[categoryType.selectedIndex].value = 0;
                        return false;
                    }
                }

            }
        }
        function validate() {

            var categoryType = document.getElementById('ddlCategoryType');
            var categoryvalue = categoryType.options[categoryType.selectedIndex].value;

            if (categoryvalue == 0) {
                alert('Please select a category!');
                return false;
            }
            else {
                //alert(categoryType.value)
                var categorywithproduct = categoryType.value
                var arr = categorywithproduct.split('|');
                //alert(arr)
                //alert(arr[1])
                if (arr[1] == 0)
                {
                    alert('Please select leaf category!')
                   // alert('Product not allowed for this category!')
                   // categoryType.options[categoryType.selectedIndex].value = 0;
                    return false;
                }

            }

            var vddlMaker = document.getElementById('ddlMaker');
            var makerValue = vddlMaker.options[vddlMaker.selectedIndex].value;


            if (makerValue == 0) {
                alert('Please select a Maker!');
                return false;
            }

           // var objTechnology = document.getElementById('lbTechnology');
            //var objTechnologyvalue = objTechnology.options[objTechnology.selectedIndex].value;

           // if (objTechnologyvalue == 0) {
           //     alert('Please select a technology!');
           //     return false;
           // }

            var itemname = document.getElementById('txtProductName').value;
            if (itemname == '') {
                alert('Product Name can not be empty!');
                return false;
            }
           // var ProductCode = document.getElementById('txtProductCode').value;
           // if (ProductCode == '') {
           //     alert('Product code can not be empty!');
           //     return false;
           // }


            //var color = document.getElementById('txtColor').value;
            //if (color == '') {
            //    alert('Product color can not be empty!');
            //    return false;
            //}

            var upc = document.getElementById('txtUPC').value;
            if (upc == '') {
                alert('Product UPC can not be empty!');
                return false;
            }
           // return checkCategory(document.getElementById('ddlCategoryType'));

        }
        function isMaxLength(obj,length) {
            var maxlength = obj.value;
            if (maxlength.length > length) {
                obj.value = obj.value.substring(0, length-1)
            }
            return true;
        }


        function ValidateSKU() {

var sku = document.getElementById("<%=txtSKU.ClientID %>");
            
            var customer = document.getElementById("<%=ddlCompany.ClientID %>");
            var PO = document.getElementById("<%=hdnPO.ClientID %>");
            if (sku.value == '') {
                alert('SKU can not be empty');
                return false;
            }
           
            if (customer.selectedIndex == 0) {

                if (PO.value == '1') {
                    alert('Some of customers SKU have fulfillment order so cannot update for all customers');
                    return false;
                }
                return confirm('Warehouse Code "000" will be assign to all Customers if you select "All" option from Company dropdown. Do you want to continue?');                

            }
        }
    </script>

</head>
<body  bgcolor="#ffffff" leftmargin="0" rightmargin="0" topmargin="0" align="center" >
    <form id="frmProduct" runat="server" align="center">
    <asp:ValidationSummary id="ValidationSummary1" runat="server" 
   ShowMessageBox="True" ShowSummary="False"></asp:ValidationSummary>


    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <table  cellspacing="0" cellpadding="0" border="0" align="center" width="100%">
        <tr>
            <td>
                <menu:menu ID="HeadAdmin2" runat="server" ></menu:menu>    
            </td>
        </tr>
        </table>
        <div id="winVP" style="z-index:1">
        
        
        <table  cellspacing="0" cellpadding="0" border="0" align="center" width="95%">
          
            <tr>
                
                <td>
                
                   <table width="100%" cellspacing="0" cellpadding="0" border="0"  >
                       <tr>
                       <br />
                            <td height="16"  class="buybt" align="left"><strong>Manage Products</strong></td>
                       </tr>
                       <tr>
                        <td align="left">
                        <asp:Label ID="lblMsg" runat="server" CssClass="errormessage"></asp:Label>
                        
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                        &nbsp;
                        
                        </td>
                    </tr>
                   </table>
                   <div>
                   <table bordercolor="#839abf" border="1" width="100%" align="center" cellpadding="0" cellspacing="0">
                    <tr><td>

                    <table border="0" width="100%" class="box" align="center" cellpadding="3" cellspacing="3">
                    <tr valign="top">
                        <td class="copy10grey" align="left" width="40%">
                           <strong> Category Type: <span class="errormessage">*</span></strong><br />
                            <asp:DropDownList ID="ddlCategoryType" runat="server" CssClass="copy10grey" onchange="CategoryOnchange(this);" 
                                    Width="80%" ></asp:DropDownList>
                        </td>    
                        <td class="copy10grey" align="left" width="30%">
                           <strong> Brand/Maker: <span class="errormessage">*</span></strong><br />
                            <asp:DropDownList ID="ddlMaker" runat="server" CssClass="copy10grey" 
                                    Width="80%" ></asp:DropDownList> 
                           <%-- Product Condition:<span class="errormessage">*</span><br />
                            <asp:DropDownList ID="ddlitemType" Visible="false" runat="server" CssClass="copy10grey" 
                                    Width="80%">
                                </asp:DropDownList>--%>
                        </td>    
                        <td class="copy10grey" align="left" width="30%">
                             <br />
                            <asp:CheckBox ID="chkActive" Text="Active" Checked="true" runat="server" CssClass="copy10grey" />
                                &nbsp; 
                                <asp:CheckBox ID="chkShowunderCatalog" Checked="true" Text="Show under Catalog" runat="server" CssClass="copy10grey" />
                                &nbsp; 
                                <asp:CheckBox ID="chkAllowRMA" Text="Allow RMA" runat="server" CssClass="copy10grey" />
                        </td>    
                    </tr>
                    <tr valign="top">
                        <td class="copy10grey" align="left" width="40%" >
                                      <strong>   Model Number: <span class="errormessage">*</span></strong><br />    
                             
                        <asp:TextBox ID="txtModelNumber" CssClass="copy10grey" runat="server" 
                                    MaxLength="40" Width="80%"></asp:TextBox>
                              <asp:RequiredFieldValidator 
                                         id="rfvModel" runat="server" 
                                         ErrorMessage="Model Number is Required!" 
                                         ControlToValidate="txtModelNumber">
                                        </asp:RequiredFieldValidator>
                             <%-- Product Code:<span class="errormessage">*</span><br />--%>
                              <asp:TextBox ID="txtProductCode" Visible="false" CssClass="copy10grey" runat="server" 
                                    MaxLength="20" Width="80%"></asp:TextBox>
                                   <%-- <asp:RequiredFieldValidator 
                                         id="rqfPCode" runat="server" 
                                         ErrorMessage="Product Code is Required!" 
                                         ControlToValidate="txtProductCode">
                                        </asp:RequiredFieldValidator> --%>  
                        </td>
                        <td align="left" width="30%" class="copy10grey" >
                             <strong> UPC:<span class="errormessage">*</span></strong><br />
                              <asp:TextBox ID="txtUPC" CssClass="copy10grey" runat="server" MaxLength="30" 
                                    Width="80%"></asp:TextBox>
                                <asp:RequiredFieldValidator 
                                         id="fqfUPC" runat="server" 
                                         ErrorMessage="Product UPC is Required!" 
                                         ControlToValidate="txtUPC">
                                        </asp:RequiredFieldValidator>  
                                 
                        </td>
                        <td class="copy10grey" align="left" width="30%" >
                         
  <br />
                                 <asp:CheckBox ID="chkKitted" Text="Kitted" runat="server" CssClass="copy10grey" />
&nbsp; 
                            <asp:CheckBox ID="chkDisplayName" Text="Display Name" runat="server" CssClass="copy10grey" />
&nbsp; 
                            
                            <asp:CheckBox ID="chkStock"   CssClass="copy10grey" runat="server" >                                 
                              </asp:CheckBox>
                                <asp:CheckBox ID="chkSim" Text="Allow SIM" runat="server"  Visible="false"  CssClass="copy10grey" />
                            &nbsp; <asp:CheckBox ID="chkESN" Text="Allow ESN" Visible="false"  runat="server" CssClass="copy10grey" />
                               
                        </td>
                    </tr>
                    <tr valign="top">
                        <td class="copy10grey" align="left" width="40%" >
                              <strong>Name:<span class="errormessage">*</span></strong><br />
                                <asp:HiddenField ID="hdnitemGUID" runat="server" />
                                <asp:HiddenField ID="hdnImage_Guid" runat="server" />
                                <asp:TextBox ID="txtProductName"  Width="80%" CssClass="copy10grey" 
                                    runat="server" MaxLength="100"></asp:TextBox>
                                     <asp:RequiredFieldValidator 
                                         id="rqfPName" runat="server" 
                                         ErrorMessage="Product Name is Required!" 
                                         ControlToValidate="txtProductName">
                                        </asp:RequiredFieldValidator>
                        </td>
                        <td align="left" width="30%" class="copy10grey" >
                            Weight(in Lbs): <br />
                         <asp:TextBox ID="txtWeight"  CssClass="copy10grey" MaxLength="6" runat="server" Width="80%" onkeypress="return ValidatePrice(event, this);"></asp:TextBox>
                
                        </td>
                        <td class="copy10grey" align="left" width="30%" rowspan="3">
                                    Carriers:<%--<span class="errormessage">*</span>--%><br />
                                    <asp:ListBox ID="lbTechnology" Height="100" SelectionMode="Multiple" runat="server" CssClass="copy10grey" 
                                    Width="80%">
                                    </asp:ListBox>
                        </td>
                    </tr>
                    
                    <tr valign="top" >
                        <td class="copy10grey" align="left" width="40%" >
                              Storage: <br />

                              <asp:DropDownList ID="ddlStorage"  Width="80%"  CssClass="copy10grey" runat="server" >
                                  <asp:ListItem Text="" Value=""></asp:ListItem>
                                  <asp:ListItem Text="Pallets" Value="Pallets"></asp:ListItem>
                                  <asp:ListItem Text="Loose Box" Value="Loose Box"></asp:ListItem>
                                  <asp:ListItem Text="Carton" Value="Carton"></asp:ListItem>
                              </asp:DropDownList>
                        </td>
                        <td align="left" width="30%" class="copy10grey" >
                            Qty per Storage: <br />
                                    <asp:TextBox ID="txtStorageQty" MaxLength="8"  CssClass="copy10grey" runat="server" Width="80%" onkeypress="return isNumberKey(event);" ></asp:TextBox>
                                 
                            
                        </td>
                        <td class="copy10grey" align="left" width="30%" >
                        
                        </td>
                    </tr>
                    <tr valign="top" >
                        <td class="copy10grey" align="left" width="40%" >
                              ESN Length: <br />
                             <asp:TextBox ID="txtEsnLength" MaxLength="8"  CssClass="copy10grey" runat="server" Width="80%" onkeypress="return isNumberKey(event);" ></asp:TextBox>
                                 
                        </td>
                        <td align="left" width="30%" class="copy10grey" >
                            MEID Length: <br />
                                    <asp:TextBox ID="txtMeidLength" MaxLength="8"  CssClass="copy10grey" runat="server" Width="80%" onkeypress="return isNumberKey(event);" ></asp:TextBox>
                                 
                            
                        </td>
                        <td class="copy10grey" align="left" width="30%" >
                        
                        </td>
                    </tr>
                    <tr valign="top" >
                        <td class="copy10grey" align="left" width="40%" >
                              Product Type: <br />
                            <asp:DropDownList ID="ddlProductType"  Width="80%"  CssClass="copy10grey" runat="server" >                                 
                              </asp:DropDownList>
                                 
                        </td>
                        <td align="left" width="30%" class="copy10grey" >
                              Product Condition: <br />
                            <asp:DropDownList ID="ddlCondition"  Width="80%"  CssClass="copy10grey" runat="server" >                                 
                              </asp:DropDownList>
                                 
                              
                        </td>
                        <td class="copy10grey" align="left" width="30%" >
                           OS Type: <br />
                             <asp:DropDownList ID="ddlOSType"  Width="80%"  CssClass="copy10grey" runat="server" >
                                 <asp:ListItem Text="" Value=""></asp:ListItem>
                                 <asp:ListItem Text="Android" Value="Android"></asp:ListItem>
                                 <asp:ListItem Text="iOS" Value="iOS"></asp:ListItem>
                                 
                              </asp:DropDownList>
                           
                        </td>
                    </tr>

                    <tr valign="top" style="display:none">
                        <td class="copy10grey" align="left" width="40%" >
                              Default Price($): <br />
                              <asp:TextBox ID="txtPrice"  Width="80%"  CssClass="copy10grey" runat="server"   onkeypress="return ValidatePrice(event, this);" 
                              ></asp:TextBox>
                        </td>
                        <td align="left" width="30%" class="copy10grey" >
                            Color:<span class="errormessage">*</span><br />
                                    <asp:TextBox ID="txtColor"  CssClass="copy10grey" runat="server" Width="80%"></asp:TextBox>
                                <%-- <asp:RequiredFieldValidator 
                                         id="RequiredFieldValidator1" runat="server" 
                                         ErrorMessage="Product Color is Required!" 
                                         ControlToValidate="txtColor">
                                        </asp:RequiredFieldValidator> --%>
                            
                        </td>
                        <td class="copy10grey" align="left" width="30%" >
                        
                        </td>
                    </tr>
                    <tr valign="top" style="display:none">
                        <td class="copy10grey" align="left" width="40%" >
                              DisplayPriority: <br />
                              <asp:DropDownList ID="ddlDisplayPriority" runat="server" CssClass="copy10grey" 
                                    Width="80%" >
                              <asp:ListItem Text="--Select DisplayPriority--" Value="0" ></asp:ListItem> 
                              <asp:ListItem Text="1" Value="1" ></asp:ListItem>      
                              <asp:ListItem Text="2" Value="2" ></asp:ListItem>      
                              <asp:ListItem Text="3" Value="3" ></asp:ListItem>      
                              <asp:ListItem Text="4" Value="4" ></asp:ListItem>      
                              <asp:ListItem Text="5" Value="5" ></asp:ListItem>      
                              <asp:ListItem Text="6" Value="6" ></asp:ListItem>      
                              <asp:ListItem Text="7" Value="7" ></asp:ListItem>      
                              <asp:ListItem Text="8" Value="8" ></asp:ListItem>      
                              <asp:ListItem Text="9" Value="9" ></asp:ListItem>      
                              <asp:ListItem Text="10" Value="10" ></asp:ListItem>      
                              <asp:ListItem Text="11" Value="11" ></asp:ListItem>      
                              <asp:ListItem Text="12" Value="12" ></asp:ListItem>      
                              <asp:ListItem Text="13" Value="13" ></asp:ListItem>      
                              <asp:ListItem Text="14" Value="14" ></asp:ListItem>      
                              <asp:ListItem Text="15" Value="15" ></asp:ListItem>      
                              <asp:ListItem Text="16" Value="16" ></asp:ListItem>      
                              <asp:ListItem Text="17" Value="17" ></asp:ListItem>      
                              <asp:ListItem Text="18" Value="18" ></asp:ListItem>      
                              <asp:ListItem Text="19" Value="19" ></asp:ListItem>      
                              <asp:ListItem Text="20" Value="20" ></asp:ListItem>      
                              </asp:DropDownList> 
                        </td>
                        <td align="left" width="30%" class="copy10grey" >
                         
                                        Document: <br /> <asp:FileUpload ID="fuItemDoc" runat="server" />
                        </td>
                        <td class="copy10grey" align="left" width="30%" >
                       
                        </td>
                    </tr>
                    <tr valign="top" style="display:none">
                        <td class="copy10grey" align="left" width="40%" >
                              SimCardType: <br />
                              <asp:DropDownList ID="ddlSimCardType" runat="server" CssClass="copy10grey" 
                                    Width="80%" >  
                              </asp:DropDownList> 
                        </td>
                        <td align="left" width="30%" class="copy10grey" >
                         
                                        SpintorLockType: <br />
                                           <asp:DropDownList ID="ddlSpintorLockType" runat="server" CssClass="copy10grey" 
                                    Width="80%" >  
                              </asp:DropDownList> 
                        </td>
                        <td class="copy10grey" align="left" width="30%" >
                      
                        </td>
                    </tr>
                    <tr valign="top" style="display:none">
                        <td class="copy10grey" align="left" width="40%" >
                              OperationSystem: <br />
                               <asp:DropDownList ID="ddlOS" runat="server" CssClass="copy10grey" 
                                    Width="80%" >  
                                    <%--<asp:ListItem Text="--Select OperationSystem--" Value="0" ></asp:ListItem> 
                              <asp:ListItem Text="1"" Value="1" ></asp:ListItem>      
                              <asp:ListItem Text="2"" Value="2" ></asp:ListItem>      
                              <asp:ListItem Text="3" Value="3" ></asp:ListItem>      
                              <asp:ListItem Text="4" Value="4" ></asp:ListItem>      
                              <asp:ListItem Text="5" Value="5" ></asp:ListItem>      
                              <asp:ListItem Text="6" Value="6" ></asp:ListItem>      
                              <asp:ListItem Text="7" Value="7" ></asp:ListItem>      
                              <asp:ListItem Text="8" Value="8" ></asp:ListItem>      
                              <asp:ListItem Text="9" Value="9" ></asp:ListItem>  --%>
                              </asp:DropDownList> 
                        </td>
                        <td align="left" width="30%" class="copy10grey" >
                         
                                        ScreenSize: <br />
                                    <asp:DropDownList ID="ddlScreenSize" runat="server" CssClass="copy10grey" Width="80%" >  
                                    <asp:ListItem Text="--Select ScreenSize--" Value="" ></asp:ListItem> 
                                    <asp:ListItem Text="1''" Value="1''" ></asp:ListItem>      
                                    <asp:ListItem Text="1.5''" Value="1.5''" ></asp:ListItem>      
                                    <asp:ListItem Text="2''" Value="2''" ></asp:ListItem>      
                                    <asp:ListItem Text="2.5''" Value="2.5''" ></asp:ListItem>    
                                    <asp:ListItem Text="3''" Value="3''" ></asp:ListItem>   
                                    <asp:ListItem Text="3.5''" Value="3.5''" ></asp:ListItem>     
                                  <asp:ListItem Text="4''" Value="4''" ></asp:ListItem>      
                                  <asp:ListItem Text="4.5''" Value="4.5''" ></asp:ListItem>      
                                  <asp:ListItem Text="4.6''" Value="4.6''" ></asp:ListItem>      
                                  <asp:ListItem Text="5''" Value="5''" ></asp:ListItem>      
                                  <asp:ListItem Text="5.2''" Value="5.2''" ></asp:ListItem>      
                                  <asp:ListItem Text="5.5''" Value="5.5''" ></asp:ListItem>      
                                  <asp:ListItem Text="6''" Value="6''" ></asp:ListItem>      
                                  <asp:ListItem Text="6.5''" Value="6.5''" ></asp:ListItem>      
                                  <asp:ListItem Text="7''" Value="7''" ></asp:ListItem>      
                                  <asp:ListItem Text="7.5''" Value="7.5''" ></asp:ListItem>      
                                  <%--<asp:ListItem Text="8" Value="8" ></asp:ListItem>      
                                  <asp:ListItem Text="9" Value="9" ></asp:ListItem>  --%>
                              </asp:DropDownList> 
                        </td>
                        <td class="copy10grey" align="left" width="30%" >
                        <%-- Weight: <br />
                         <asp:TextBox ID="txtWeight"  CssClass="copy10grey" runat="server" Width="80%"></asp:TextBox>--%>
                        </td>
                    </tr>
                     <tr valign="top">
                        <td class="copy10grey" align="left"  colspan="3">
                        Short Description: <br />
                        <asp:TextBox ID="txtDescription" Height="40px" Width="70%" TextMode="MultiLine" 
                                     CssClass="copy10grey" runat="server" onkeypress="return isMaxLength(this,500);"></asp:TextBox>
                        

                        </td>
                    </tr>
                       <%-- <tr valign="top"><td>&nbsp;</td></tr>--%>
                       <tr valign="top">
                        <td class="copy10grey" align="left"  colspan="3">
                        Full Desc.:<br />
                        <asp:TextBox ID="txtFullDesc" Height="48px" Width="94%" TextMode="MultiLine" 
                                    CssClass="copy10grey" runat="server"></asp:TextBox>

                        </td>
                    </tr>
                    <tr valign="top" style="line-height:10px">
                            <td align="left" class="copy10grey" >
                            &nbsp;
                            </td>
                            <td align="left">
                               
                            </td>
                            <td>
                            
                            </td>
                            
                        </tr>
                    </table>
                   <%-- <table border="0" width="100%" class="box" align="center" cellpadding="2" cellspacing="2">
                        <tr valign="top">
                            <td class="copy10grey" align="left" width="15%" >
                                
                            </td>
                            <td align="left" width="35%" >
                                
                            </td>
                            <td class="copy10grey" align="left" width="15%" ></td>
                            <td align="left" width="35%" >
                                                               
                            </td>                            
                        </tr>
                        <tr valign="top">
                            <td align="left" class="copy10grey">
                            </td>
                            <td align="left" >
                                
                            </td>
                            <td align="left" class="copy10grey">
                            </td>
                            <td align="left" rowspan="2">
                                
                                
                            </td>
                            
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>

                        </tr>
                        
                        <tr>
                            <td style="width:85px" class="copy10grey" align="left" >
                                
                            </td>
                            <td align="left" >
                                

                            </td>
                            <td class="copy10grey" align="left" >
                               
                            </td>
                            <td align="left" >
                                
                            </td>
                        </tr>
                        <tr>
                            <td class="copy10grey" align="left" >
                               
                            </td>
                            <td align="left" >
                                
                            </td>
                            <td class="copy10grey" align="left" >
                               
                            </td>
                            <td align="left" >
                                
                            </td>
                        </tr>
                        <tr valign="top">
                            <td class="copy10grey" align="left" >
                               
                            </td>
                            <td align="left" >
                                 
                            </td>
                            <td class="copy10grey" align="left" >
                            
                            </td>
                            <td align="left" >
                            
                                
                            </td>
                        </tr>
                        
                        
                          
                        <tr>
                            <td align="left" class="copy10grey"></td>
                            <td align="left" >
                                 
                            </td>
                            <td></td>
                            <td align="left" >
                               
                            </td>
                        </tr>
                        
                        <tr>
                            <td colspan="4" style="height:18px"></td>
                        </tr>
                    </table>--%>
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
            <tr >
                
                <td id="tabTD" width="100%" >
                <asp:Panel ID="tabPanel" runat="server">
                    <asp:HiddenField ID="hdnTabindex" runat="server" />
                    <div id="ddtabs4" class="ddcolortabs" align="center" width="100%" >
                    <ul>
                        <li><a href="#" rel="ct1"><span>SKUs</span></a></li>
                    
                    <%--<li><a href="#" rel="ct3"><span>Pricing</span></a></li>--%>
                    <li><a href="#" rel="ct5"><span>Attribute</span></a></li>
                    <li><a href="#" rel="ct4"><span>Specifications</span></a></li>
                    <li><a href="#" rel="ct6" id="imagetab"><span>Images</span></a></li>
                    <%--<li><a href="#" rel="ct7"><span>Camera</span></a></li>--%>
                    </ul>
                    </div>
                    <div class="ddcolortabsline" width="100%">&nbsp;</div>
                    <div id="allTabContnt" width="100%">
                    <div id="ct6" class="tabcontent" style="border:1px solid #666666" align="center">
                            <%--<ContentTemplate>--%>
                        <br />
                        <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="99%" align="center">

                       <tr bordercolor="#839abf">
                            <td>
                        <table align="center" width="100%" >
                            
                            <tr valign="top" >
                            <td class="copy10grey" align="left" width="270" >
                            
                         <strong>   Image 250*345</strong>
                            </td>
                            <td  class="copy10grey" align="left" width="120">
                           
                            <strong > Image 100*100</strong>
                           
                         
                            </td>
                            <td  class="copy10grey" align="left" width="200">
                           <strong>  Image Description</strong>
                            </td>
                            <td>
                            
                            </td>
                            
                            
                        </tr>
                        <tr valign="top" >
                            <td class="copy10grey" align="left" width="200">
                                <asp:FileUpload ID="fupItemImage1"  runat="server" width="200" CssClass="copy10grey" />
                            </td>
                            <td class="copy10grey" align="left" width="200">  
<asp:FileUpload ID="fupItemImg2"  runat="server" width="200" CssClass="copy10grey" />

                                <asp:DropDownList ID="ddlImageType1" Visible="false"  runat="server" width="100" CssClass="copy10grey" >
                                    </asp:DropDownList>
                            </td>
                            <td class="copy10grey" align="left" width="200">
                                <asp:TextBox ID="txtImageDesc1" CssClass="copy10grey" width="95%" runat="server"></asp:TextBox>
                            </td>
                            <td class="copy10grey" align="left">
                                <asp:DropDownList Visible="false" ID="ddlColor" runat="server" CssClass="copy10grey">
                                    </asp:DropDownList>
                            </td>  
                            
                            
                        </tr>
                        <tr>
                            <td colspan="4">
                            <hr />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" align="center">
                            <asp:Button ID="btnUpdateImage" CssClass="buybt" runat="server"  OnClientClick="return checkForEmptyImage();" 
                                        Text="Update Image" OnClick="btnUpdateImage_Click" />&nbsp;&nbsp;
                                    
                                    <asp:HiddenField ID="hdnImageGUID1" runat="server"  />
                            </td>
                        </tr>
                        
                        </table>
                        </td>
                        </tr>
                        </table>
                        <br />

                        <table width="99%">
                        
                        <tr>
                            <td >
                                <div>
                                
                                <asp:HiddenField ID="hdnImageGUID" runat="server"  />
                                <asp:HiddenField  ID="hdnImageURL" runat="server" /><br />
                                <asp:GridView Width="100%"  ID="gvItemImages" CssClass="gridGray1" runat="server" AutoGenerateColumns="false">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Image Type" Visible="false" HeaderStyle-CssClass="buybt" HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:HiddenField Value='<%# Eval("imageGUID") %>' ID="hdnImageID" runat="server" />
                                                <asp:Label ID="lblImagetype" runat="server" CssClass="copy10grey" Text='<%# Eval("imagetype") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Image Description" HeaderStyle-CssClass="buybt" HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                            <asp:HiddenField Value='<%# Eval("imagename") %>' ID="hdnImagetxt" runat="server" />
                                            <asp:HiddenField Value='<%# Eval("ImageURL") %>' ID="hdnImagePath" runat="server" />
                                                <asp:Label ID="lblImagetxt" runat="server" CssClass="copy10grey" Text='<%# Eval("imagename") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Color" Visible="false" HeaderStyle-CssClass="buybt" HeaderStyle-HorizontalAlign="left">
                                            <ItemTemplate>
                                                <asp:HiddenField Value='<%# Eval("ColorGUID") %>' ID="hdnColorGUID" runat="server" />
                                                <asp:Label ID="lblcolor" runat="server" CssClass="copy10grey" Text='<%# Eval("color") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Image 250*345" HeaderStyle-CssClass="buybt" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                        <img id="displayimg" src="../images/view1.png" onclick="displayImage('<%# "L_" + Eval("ImageURL")%>');" />
                                        </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Image 100*100" HeaderStyle-CssClass="buybt" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                        <img id="Img2" src="../images/image.png" onclick="displayImage('<%# "S_" + Eval("ImageURL")%>');" />
                                        </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="buybt" HeaderText="Action" ItemStyle-HorizontalAlign="center">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="lnkImageUpdate"  runat="server" OnClientClick="return callImageUpdate(this)" ImageUrl="~/images/edit.png"  AlternateText="Edit Item" />
                                                <asp:ImageButton ID="lnkimageDelete" CommandArgument='<%# Eval("imageGUID") %>' OnCommand="ImageDelete_click" OnClientClick="return confirm('Delete this Image?');" runat="server"  CommandName="wDelete"  AlternateText="Delete Image" ImageUrl="../images/delete.png" />
                                                
                                            </ItemTemplate>
                                        </asp:TemplateField>   
                                    </Columns>
                                </asp:GridView>
                                </div>
                            </td>
                        </tr>
                        </table>
                        
                    </div>
                    <div id="ct2" class="tabcontent" style="border:1px solid #666666">
                      
                    </div>
                    <div id="ct3" class="tabcontent" style="border:1px solid #666666">
                      <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                        
                             <br />
                        <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="99%" align="center">

                       <tr bordercolor="#839abf">
                            <td>
                        <table width="100%">
                            
                            <tr>
                                    <td class="copy10grey" width="150" align="left"> 
                                   <strong>    Web Price</strong>
                                    </td>
                                    <td class="copy10grey" width="150" align="left"><strong> Retail Price</strong></td>
                                    <td class="copy10grey" width="150" align="left"><strong> Wholesale Price</strong></td>
                                    <td class="copy10grey" width="150" align="left"><strong> Price Type</strong></td>
                                <tr>
                                    <td class="copy10grey" align="left"><asp:TextBox ID="txtWebPrice"  Width="75px" CssClass="copy10grey" runat="server" onkeypress="return isNumberKey(event);" ></asp:TextBox></td>
                                    <td class="copy10grey" align="left"><asp:TextBox ID="txtRetailPrice"  Width="75px"  CssClass="copy10grey" runat="server" onkeypress="return isNumberKey(event);" ></asp:TextBox></td>
                                    <td class="copy10grey" align="left"><asp:TextBox ID="txtWholesalePrice" Width="75px"  CssClass="copy10grey" runat="server" onkeypress="return isNumberKey(event);" ></asp:TextBox></td>
                                    <td class="copy10grey" align="left"> <asp:DropDownList ID="ddlPricetype" Width="75px" runat="server" CssClass="copy10grey" >
                                        </asp:DropDownList></td>

                                
                                
                            </tr>
                            <tr>
                                <td colspan ="4" >
                                    <hr />
                                </td>
                            </tr>
                            <tr>
                                <td colspan ="4" align="center" >
                                    <asp:Button ID="Button2" runat="server" CssClass="buybt" Text="Update Price" 
                                        OnClick="btnUpdatePricing_Click" OnClientClick="return checkForEmptyPrice();" />
                                </td>
                            </tr>
                            </table>
                            </td>
                            </tr>
                            </table>
                            <br />

                            <table width="99%">
                            
                            
                            <tr >
                                
                                <td colspan="8">
                                <asp:HiddenField ID="hdnpricingGUID" runat="server"  />
                                <asp:HiddenField ID="hdnSize_GUID" runat="server" Value='<%# Eval("SizeGUID") %>'  /><br />
                                    <asp:GridView Width="100%" ID="gvItemPricing" CssClass="gridGray1" 
                                        runat="server" AutoGenerateColumns="false" >
                                    <Columns>
                                      
                                        <asp:TemplateField HeaderText="Web Price" HeaderStyle-CssClass="buybt" HeaderStyle-HorizontalAlign="left"> 
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hdnpricingID" runat="server" Value='<%# Eval("pricingGUID") %>'  />
                                                <asp:Label ID="lblWebPrice" runat="server" CssClass="copy10grey" Text='<%# Eval("webprice") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Retail Price" HeaderStyle-CssClass="buybt" HeaderStyle-HorizontalAlign="left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRetialPrice" runat="server" CssClass="copy10grey" Text='<%# Eval("retailprice") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Wholesale Price" HeaderStyle-CssClass="buybt" HeaderStyle-HorizontalAlign="left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblWholesalePrice" runat="server" CssClass="copy10grey" Text='<%# Eval("wholesaleprice") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Price Type" HeaderStyle-CssClass="buybt" HeaderStyle-HorizontalAlign="left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPriceType" runat="server" CssClass="copy10grey" Text='<%# Eval("pricetype") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="buybt" HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="lnkPricingUpdate" runat="server" ImageUrl="~/images/edit.png" OnClientClick="return callPricingUpdate(this)" AlternateText="Edit Pricing" />
                                                <asp:ImageButton ID="lnkPriceDelete" CommandArgument='<%# Eval("pricingGUID") %>' OnCommand="priceDelete_click" OnClientClick="return confirm('Delete this price?');" runat="server"  CommandName="wDelete"  AlternateText="Delete Price" ImageUrl="../images/delete.png" />
                                                
                                            </ItemTemplate>
                                        </asp:TemplateField>   
                                    </Columns>
                                </asp:GridView>
                                </td>
                            </tr>
                        </table> 
                        </ContentTemplate>
                        </asp:UpdatePanel>   
                    </div>
                    
                    <div id="ct5" class="tabcontent" style="border:1px solid #666666">
                    <table width="100%">
                    <tr>
                        <td width="95%">
                            <%--Attribute Value--%>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                            <br />
                        <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="99%" align="center">

                       <tr bordercolor="#839abf">
                            <td>
                            <table width="100%">
                            <tr>
                                <td>
                                    
                                
                            <asp:DataList ID="DlAttributeValue" runat="server" >
                                <HeaderTemplate>
                                <table cellspacing="2" >
                                    <tr class="buybt">
                                        <td class="buybt">
                                            Attribute
                                        </td>
                                        <td class="buybt">
                                            Attribute Value
                                        </td>
                                        <td>
                                        </td>
                                        
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                <tr>
                                    <td class="copy10grey">
                                        <asp:HiddenField ID="hdnAttributeValueGuid" Value='<%#Eval("AttributeValueGuid")%>' runat="server" />
                                        <asp:HiddenField ID="hdnAttributeGuid" Value='<%#Eval("AttributeGuid")%>' runat="server" />
                                        <asp:Label ID="lblAttribute" runat="server" Text='<%#Eval("AttributeName")%>' Width="90"></asp:Label>
                                    </td>
                                    <td class="copy10grey">
                                        <asp:TextBox ID="txtAttributeValue" runat="server" CssClass="copy10grey" Text='<%#Eval("AttributeValue")%>' Width="400"></asp:TextBox>
                                    </td>
                                          
                                    <td>
                                        <asp:LinkButton ID="lnkEdit" CssClass="copy10grey" Visible="false" CommandArgument='<%# Eval("AttributeValueGuid") %>'   runat="server">Edit</asp:LinkButton>
                                        <asp:LinkButton ID="lnkDelete" CssClass="copy10grey"  OnCommand="deleteattribute_click" CommandArgument='<%# Eval("AttributeValueGuid") %>' OnClientClick="return confirm('Delete this Attribute?');"  runat="server" >Clear</asp:LinkButton>
                                                            
                                    </td>
                                </tr>
                                                        
                                </ItemTemplate>
                                <FooterTemplate>
                                </table>
                                </FooterTemplate>
                                </asp:DataList>
                                </td>
                                <%--<td valign="baseline">&nbsp;&nbsp;&nbsp;
                                    
                                </td>--%>
                            </tr>
                            <tr>
                            <td>
                                <hr />
                            </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:Button ID="btnSubmitval" runat="server" OnClientClick="return checkForEmptyAttribute();" Text="Update Attribute" class="buybt" 
                                    onclick="btnSubmitval_Click"/>
                                    <asp:Button ID="btnCancelVal" Visible="false" runat="server" Text="Cancel" class="buybt" 
                                    onclick="btnCancelVal_Click"/>
                                </td>
                            </tr>
                            </table>
                            </td>
                            </tr>
                            </table>    
                                    <asp:HiddenField ID="hdnAttributevalGUID" runat="server" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td valign="top">
                            <img id="img1" onclick="doOnLoad();" alt="Add Attribute" src="../images/a-plus.gif" />
                        </td>
                    </tr>
                    </table>
                    
                    </div>
                    <div id="ct4" class="tabcontent" style="border:1px solid #666666">
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                            <ContentTemplate>
                            <br />
                        <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="99%" align="center">

                   <tr bordercolor="#839abf">
                        <td>
                        <table width="100%">
                            
                            <tr valign="top">
                                <td class="copy10grey" width="150" >  
                               <strong>  Specification:</strong>  
                                </td>
                                <td class="copy10grey" align="left" >
                                    <asp:TextBox ID="txtSpecification"  TextMode="MultiLine" Height="70" Width="80%" onkeypress="return isMaxLength(this,300);"
 CssClass="copy10grey" runat="server"></asp:TextBox><br />
                                    <label class="errormessage">Max 300 char!</label>
                                                                   
                                         </td>
                                
                            </tr>
                            <tr>
                            <td colspan="2">
                            <hr />
                            </td>
                        </tr>
                            <tr>
                                <td colspan="2" ><asp:Button ID="btnUpdateSpecification" runat="server" OnClientClick="return checkForEmptyspecification();" CssClass="buybt" Text="Update Specification" OnClick="btnUpdateSpecification_Click" />
                               </td>
                            </tr>
                            
                        </table>
                        </td>
                        </tr>
                        </table>
                        <br />
                        <table  width="100%">                            
                            <tr>
                               
                                <td colspan="4" align="center"><asp:HiddenField ID="hdnSpecificationGUID" runat="server"  /><br />
                                <asp:GridView ID="gvSpecification"  Width="98%" CssClass="gridGray1" runat="server" AutoGenerateColumns="false">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Specification" HeaderStyle-CssClass="buybt" HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                <asp:HiddenField ID="hdnSpecificationID" Value='<%# Eval("SpecificationGUID") %>' runat="server"  />
                                                    <asp:Label ID="lblSpecification" CssClass="copy10grey" runat="server"  Text='<%# Eval("Specificaiton") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-CssClass="buybt" HeaderText="Action" HeaderStyle-Width="50px">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="lnkSpecificationUpdate" runat="server" ImageUrl="~/images/edit.png" OnClientClick="return callSpecificationUpdate(this)" AlternateText="Edit Specification" />
                                                    <asp:ImageButton ID="lnkSpecificationDelete" CommandArgument='<%# Eval("SpecificationGUID") %>' OnCommand="specificationDelete_click" OnClientClick="return confirm('Delete this specification?');" runat="server"  CommandName="wDelete"  AlternateText="Delete specification" ImageUrl="../images/delete.png" />
                                                    
                                                </ItemTemplate>
                                            </asp:TemplateField>   
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                        </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div id="ct1" class="tabcontent" style="border:1px solid #666666">
                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                            <ContentTemplate>
                            <table>
                            <tr>
                                <td align="left">
                                    <span class="copy10grey"> 
                                    Warehouse Code '000' will be assign to all Customers if you select 'All' option from Company dropdown
                                    </span>     
                                </td>
                            </tr>
                            </table>
                           
                   <br />
                   <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="99%" align="center">
                   <tr bordercolor="#839abf">
                        <td>
                          <table width="100%" align="center" border="1" cellSpacing="3" cellPadding="3">
                        <tr valign="top">
                            <td class="copy10grey" align="left" width="7%">
                                <strong>Company</strong>
                            </td>
                            <td class="copy10grey" align="left" width="12%">
                               <strong>SKU# </strong>
                            </td>
                            <td class="copy10grey" align="left" width="7%">
                                <strong>Warehouse Code</strong>
                            </td>
                            <td class="copy10grey" align="left" width="7%">
                                <strong>Minimum Stock</strong>
                            </td>
                            <td class="copy10grey" align="left" width="7%">
                                <strong>Maximum Stock</strong>
                            </td>
                            <td class="copy10grey" align="left" width="3%">
                                <strong>Disable</strong>
                            </td>
                            <td class="copy10grey" align="left"  width="7%">
                                <strong>Container Quantity</strong>
                            </td>
                            <td class="copy10grey" align="left"  width="5%">
                                <strong>DPCI</strong>
                            </td>
                            <td class="copy10grey" align="left"  width="5%">
                                <strong>Pallet Quantity</strong>
                            </td>
                            <td class="copy10grey" align="left"  width="5%">
                                <strong>SW Version</strong>
                            </td><td class="copy10grey" align="left"  width="5%">
                                <strong>HW Version</strong>
                            </td>
                            <td class="copy10grey" align="left"  width="15%">
                                <strong>Box Description </strong>
                            </td>
                            <%--<td class="copy10grey" align="left"  width="9%">
                                <strong>Mapped SKU</strong>
                            </td>--%>
                            <td>&nbsp;</td>
                        </tr>                        
                        <tr>
                            <td class="copy10grey" align="left">
                                <asp:DropDownList ID="ddlCompany" runat="server" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged"  CssClass="copy10grey" AutoPostBack="true">
                                </asp:DropDownList>
                            </td>
                            <td class="copy10grey" align="left">
                                <asp:TextBox ID="txtSKU"  CssClass="copy10grey" MaxLength="50" Width="100%" runat="server"></asp:TextBox>
                            </td>
                            <td class="copy10grey" align="left">
                                <asp:DropDownList ID="ddlWhCode" runat="server" CssClass="copy10grey" Width="100">
                                </asp:DropDownList>
                            </td>
                             <td class="copy10grey" align="left" >
                                <asp:TextBox ID="txtMinQty" onkeypress="return isNumberOnly(event);" MaxLength="9"  Width="90"  CssClass="copy10grey" runat="server"></asp:TextBox>
                            </td>
                             <td class="copy10grey" align="left">
                                <asp:TextBox ID="txtMaxQty" onkeypress="return isNumberOnly(event);"  MaxLength="9" Width="90" CssClass="copy10grey" runat="server"></asp:TextBox>
                            </td>
                             <td class="copy10grey" align="left">
                                <asp:CheckBox ID="chkEnable"  CssClass="copy10grey" runat="server"></asp:CheckBox>
                            </td>
                            <td class="copy10grey" align="left">
                                <asp:TextBox ID="txtContainerQty" onkeypress="return isNumberOnly(event);" MaxLength="9" Width="110" CssClass="copy10grey" runat="server"></asp:TextBox>
                            </td>
                            <td class="copy10grey" align="left">
                                <asp:TextBox ID="txtDPCINumber"  CssClass="copy10grey" onkeypress="return isNumberHiphen(event);" Width="50" MaxLength="20" runat="server"></asp:TextBox>
                            </td>
                            
                            <td class="copy10grey" align="left">
                                <asp:TextBox ID="txtPalletQty" onkeypress="return isNumberOnly(event);" MaxLength="9" CssClass="copy10grey" Width="90" runat="server"></asp:TextBox>
                            </td>
                            <td class="copy10grey" align="left" >
                                <asp:TextBox ID="txtSWVersion"  MaxLength="20" CssClass="copy10grey" Width="90" runat="server"></asp:TextBox>
                            </td>
                            <td class="copy10grey" align="left">
                                <asp:TextBox ID="txtHWVersion"  MaxLength="20" CssClass="copy10grey" Width="90" runat="server"></asp:TextBox>
                            </td>
                            
                            <td class="copy10grey" align="left">
                                <asp:TextBox ID="txtBoxDesc"  MaxLength="100" TextMode="MultiLine" Rows="2" CssClass="copy10grey" Width="99%" runat="server"></asp:TextBox>
                                <asp:DropDownList ID="ddlMappedSKU" Visible="false" runat="server"  CssClass="copy10grey" Width="99%" >
                                </asp:DropDownList>
                            </td>
                            <td class="copy10grey" align="right">
                                <asp:HiddenField ID="hdnPO" runat="server" />
                                <asp:Button ID="btnUpdateitemcompsku" runat="server" OnClientClick="return ValidateSKU();" CssClass="buybt" Text="Update SKU#" OnClick="btnUpdateitemcompsku_Click" />
                            &nbsp;&nbsp;
                               
                           </td>
                            
                        </tr>
                        <%--<tr>
                            <td colspan="4">
                            <hr />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" align="right">

                            </td>
                        </tr>--%>
                        
                        <%--<tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>--%>
                        </table>
                        </td>
                        </tr>
                        </table>
                        <br />
                        <table width="99%" align="center" cellSpacing="0" cellPadding="0">
                        <tr>
                            <td >
                                <asp:HiddenField ID="hdnItemcompskuGUID"  runat="server"  />
                                <asp:GridView ID="gvcompanynsku" Width="100%" AllowPaging="true" PageSize="30" CssClass="gridGray1" 
                                    OnRowEditing="grvItem_RowEditing" OnPageIndexChanging="grvItem_PageIndexChanging" 
                                    runat="server" AutoGenerateColumns="false">
                                    <PagerStyle ForeColor="black" Font-Size="XX-Small"/>
                                        <Columns>
                                            <asp:TemplateField HeaderText="Company Name" HeaderStyle-CssClass="buybt" HeaderStyle-HorizontalAlign="left" 
                                                ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="7%">
                                                <ItemTemplate>
                                                <asp:HiddenField ID="hdnItemcompanyGUID" Value='<%# Eval("ItemcompanyGUID") %>' runat="server"  />
                                                    <asp:Label ID="lblcompanyName" CssClass="copy10grey" runat="server"  Text='<%# Eval("CompanyName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="SKU#" HeaderStyle-Width="12%" HeaderStyle-CssClass="buybt" HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                <asp:HiddenField ID="hdnSKU" Value='<%# Eval("itemGUID") %>' runat="server"  />
                                                <asp:HiddenField ID="hdnCompanyID" Value='<%# Eval("companyID") %>' runat="server"  />
                                                   <%-- <asp:HiddenField ID="hdIsFinishedSKU" Value='<%# Eval("IsFinishedSKU") %>' runat="server"  />--%>
                                                    <asp:Label ID="lblSKU" CssClass="copy10grey" runat="server"  Text='<%# Eval("sku") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--<asp:TemplateField HeaderText="MAS SKU#" HeaderStyle-CssClass="buybt" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                 <asp:Label ID="lblMASSKU" CssClass="copy10grey" runat="server"  Text='<%# Eval("MASSKU") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="Warehouse Code" HeaderStyle-Width="9%" HeaderStyle-CssClass="buybt" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                 <asp:Label ID="lblWhCode" CssClass="copy10grey" runat="server"  Text='<%# Eval("WarehouseCode") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Minimum Stock" HeaderStyle-Width="9%" ItemStyle-CssClass="copy10grey" HeaderStyle-CssClass="buybt" HeaderStyle-HorizontalAlign="Center" 
                                                ItemStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <%# Convert.ToInt32(Eval("MinimumStockLevel"))==0?"": Eval("MinimumStockLevel")%>
                                                 <%--<asp:Label ID="lblMinQty" CssClass="copy10grey" runat="server"  Text='<%# Eval("MinimumStockLevel") %>'></asp:Label>--%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Maximum Stock" HeaderStyle-Width="9%" ItemStyle-CssClass="copy10grey"  HeaderStyle-CssClass="buybt" HeaderStyle-HorizontalAlign="Center" 
                                                ItemStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <%# Convert.ToInt32(Eval("MaximumStockLevel"))==0?"": Eval("MaximumStockLevel")%>
                                                 <%--<asp:Label ID="lblMaxQty" CssClass="copy10grey" runat="server"  Text='<%# Eval("MaximumStockLevel") %>'></asp:Label>--%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Disable" HeaderStyle-CssClass="buybt" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                 <asp:Label ID="lblEnable" CssClass="copy10grey" runat="server"  Text='<%# Convert.ToBoolean(Eval("IsDisable"))==true?"Disable":"Enable" %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Container Qty" HeaderStyle-Width="9%" ItemStyle-CssClass="copy10grey"  HeaderStyle-CssClass="buybt" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <%# Convert.ToInt32(Eval("ContainerQty"))==0?"": Eval("ContainerQty")%>
                                                 <%--<asp:Label ID="lblContainerQty" CssClass="copy10grey" runat="server"  Text='<%# Eval("ContainerQty") %>'></asp:Label>--%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DPCI" HeaderStyle-Width="8%" HeaderStyle-CssClass="buybt" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                 <asp:Label ID="lblDPCI" CssClass="copy10grey" runat="server"  Text='<%# Eval("DPCI") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField HeaderText="Pallet Qty" HeaderStyle-Width="7%" ItemStyle-CssClass="copy10grey"  HeaderStyle-CssClass="buybt" HeaderStyle-HorizontalAlign="Center" 
                                                ItemStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <%# Convert.ToInt32(Eval("PalletQuantity"))==0?"": Eval("PalletQuantity")%>
                                                 <%--<asp:Label ID="lblContainerQty" CssClass="copy10grey" runat="server"  Text='<%# Eval("ContainerQty") %>'></asp:Label>--%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="SW Version" HeaderStyle-Width="7%" HeaderStyle-CssClass="buybt" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                 <asp:Label ID="lblSWVersion" CssClass="copy10grey" runat="server"  Text='<%# Eval("SWVersion") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="HW Version" HeaderStyle-Width="10%" HeaderStyle-CssClass="buybt" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                 <asp:Label ID="lblHWVersion" CssClass="copy10grey" runat="server"  Text='<%# Eval("HWVersion") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField HeaderText="Box Desc." HeaderStyle-Width="10%" HeaderStyle-CssClass="buybt" ItemStyle-CssClass="copy10grey" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <%# Eval("BoxDesc") %>

                                                 </ItemTemplate>
                                            </asp:TemplateField> 
                                            <asp:TemplateField HeaderText="Mapped SKU" Visible="false" HeaderStyle-Width="10%" HeaderStyle-CssClass="buybt" ItemStyle-CssClass="copy10grey" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <%# Eval("MappedSKU") %>

                                                 <%--<asp:Label ID="lblMappedSKU" CssClass="copy10grey" runat="server"  Text='<%# Eval("MappedSKU") %>'></asp:Label>--%>
                                                </ItemTemplate>
                                            </asp:TemplateField>                                            
                                            <asp:TemplateField HeaderStyle-CssClass="buybt" HeaderText="Action" HeaderStyle-Width="50px">
                                                <ItemTemplate>
                                                    <%--<asp:ImageButton ID="lnkItemcompskuUpdate" Enabled='<%# Convert.ToInt32(Eval("PoExists")) == 1 ? false : true %>'  runat="server" ImageUrl="~/images/edit.png" OnClientClick="return callItemcompanyskuUpdate(this)" AlternateText="Edit SKU#" />--%>

                                                    <asp:ImageButton ID="lnkItemcompskuUpdate"   CommandArgument='<%# Eval("ItemcompanyGUID") %>' OnCommand="ItemCompanySKUEdit_click" runat="server" ImageUrl="~/images/edit.png" AlternateText="Edit SKU#"  CommandName='<%# Convert.ToInt32(Eval("PoExists")) == 1 ? true : false %>' />
                                                    <asp:ImageButton ID="lnkItemcompskuDelete" Enabled='<%# Convert.ToInt32(Eval("PoExists")) == 1 ? false : true %>'  
                                                    CommandArgument='<%# Eval("ItemcompanyGUID") %>' OnCommand="ItemcompanyskuDelete_click" OnClientClick="return confirm('Delete this SKU#?');" runat="server"  CommandName="wDelete"  AlternateText="Delete SKU#" ImageUrl="../images/delete.png" />
                                                    
                                                </ItemTemplate>
                                            </asp:TemplateField>   
                                        </Columns>
                                    </asp:GridView>
                            </td>
                        </tr>
                        </table>
                         </ContentTemplate>
                     </asp:UpdatePanel>
                    </div>
                    <div id="ct7" class="tabcontent" style="border:1px solid #666666">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                            <table>
                            <tr>
                                <td align="left">
                                    <span class="copy10grey"> 
                                    Camera Info
                                    </span>     
                                </td>
                            </tr>
                            </table>
                           
                            <br />
                    <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="99%" align="center">

                   <tr bordercolor="#839abf">
                        <td>
                          <table width="100%" align="center" cellSpacing="3" cellPadding="3">
                        <tr valign="top">
                            <td class="copy10grey" align="left">
                             <strong>   Camera Type</strong>
                            </td>
                            <td class="copy10grey" align="left">
                               <strong>Camera Config </strong>
                            </td>
                            <%--<td class="copy10grey" align="left">
                                <strong>Zoom</strong>
                            </td>--%>
                            
                        </tr>
                        
                        <tr>
                            <td class="copy10grey" align="left">
                                <asp:DropDownList ID="ddlCameraType" runat="server" CssClass="copy10grey" >
                                <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                <asp:ListItem Value="FF" Text="Front Facing"></asp:ListItem>
                                <asp:ListItem Value="BF" Text="Back Facing"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td class="copy10grey" align="left">
                                <asp:DropDownList ID="ddlCameraConfig" runat="server" CssClass="copy10grey" AutoPostBack="false">
                                </asp:DropDownList>
                            </td>
                            <%--<td class="copy10grey" align="left">
                            <asp:TextBox ID="txtZoom"  CssClass="copy10grey" runat="server"></asp:TextBox>
                               
                            </td>--%>
                           
                        </tr>
                        <tr>
                            <td colspan="4">
                            <hr />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" align="right">
                                <asp:HiddenField ID="hdItemCameraID" runat="server" />
                                <asp:Button ID="btnCamera" runat="server" OnClientClick="return ValidateCamera();" CssClass="buybt" Text="Update Camera Info" OnClick="btnUpdateCamera_Click" />
                            &nbsp;&nbsp;
                            </td>
                        </tr>
                        
                        <%--<tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>--%>
                        </table>
                        </td>
                        </tr>
                        </table>
                        <br />
                        <table width="99%" align="center" cellSpacing="0" cellPadding="0">
                        <tr>
                            <td >
                                <asp:HiddenField ID="HiddenField2"  runat="server"  />
                                <asp:GridView ID="gvCamera" Width="100%" AllowPaging="true" PageSize="30" CssClass="gridGray1" 
                                     
                                    runat="server" AutoGenerateColumns="false">
                                    <PagerStyle ForeColor="black" Font-Size="XX-Small"/>
                                        <Columns>
                                            <asp:TemplateField HeaderText="Camera Type" HeaderStyle-CssClass="buybt" HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                <asp:HiddenField ID="hdnCameraID" Value='<%# Eval("CameraID") %>' runat="server"  />
                                                    <asp:Label ID="lblCameraType" CssClass="copy10grey" runat="server"  Text='<%# Eval("CameraType") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Pixel" HeaderStyle-CssClass="buybt" HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                
                                                    <asp:Label ID="lblPixel" CssClass="copy10grey" runat="server"  Text='<%# Eval("Pixel") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Zoom" HeaderStyle-CssClass="buybt" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                 <asp:Label ID="lblZoom" CssClass="copy10grey" runat="server"  Text='<%# Eval("Zoom") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                           
                                            <asp:TemplateField HeaderStyle-CssClass="buybt" HeaderText="Action" HeaderStyle-Width="50px">
                                                <ItemTemplate>
                                                    <%--<asp:ImageButton ID="lnkItemcompskuUpdate" Enabled='<%# Convert.ToInt32(Eval("PoExists")) == 1 ? false : true %>'  runat="server" ImageUrl="~/images/edit.png" OnClientClick="return callItemcompanyskuUpdate(this)" AlternateText="Edit SKU#" />--%>

                                                    <asp:ImageButton ID="lnkItemCameraUpdate"   CommandArgument='<%# Eval("itemCameraID") %>' OnCommand="CameraEdit_click" runat="server" ImageUrl="~/images/edit.png" AlternateText="Edit Camera"  CommandName="wEdit" />
                                                    <asp:ImageButton ID="lnkItemCameraDelete" 
                                                    CommandArgument='<%# Eval("itemCameraID") %>' OnCommand="ItemCameraDelete_click" OnClientClick="return confirm('Delete this camera configuaration?');" runat="server"  CommandName="wDelete"  AlternateText="Delete Camera" ImageUrl="../images/delete.png" />
                                                    
                                                </ItemTemplate>
                                            </asp:TemplateField>   
                                        </Columns>
                                    </asp:GridView>
                            </td>
                        </tr>
                        </table>
                         </ContentTemplate>
                     </asp:UpdatePanel>
                    </div>
                 </div>
                 </asp:Panel>
                </td>
            </tr>
            <tr>
                <td style="height:24">
                    &nbsp;
                </td>
            </tr>
            <tr>
                
                <td align="center">
                    <asp:button ID="btnUpdate" runat="server" Text=" Update Item" 
                                    onclick="btnUpdate_Click" CssClass="buybt" OnClientClick="return validate();"  />&nbsp;
                                <asp:button ID="btnCancel" runat="server" Text="Cancel"  onclick="btnCancel_Click" 
                                     CssClass="buybt"  />
                    <asp:button ID="btnBackToSearch" runat="server" Text="Back to Search"  onclick="btnBackToSearch_Click" 
                                     CssClass="buybt"  />
                    
                </td>
            </tr>
            <tr>
                <td>

                </td>
            </tr>
        </table>

                        <br />
        <br />

                    <foot:MenuFooter ID="footer" runat="server"></foot:MenuFooter>
        </div>
    </div>
    <script type="text/javascript">
    //SYNTAX: ddtabmenu.definemenu("tab_menu_id", integer OR "auto")
    var tabIndex=document.getElementById("<%= hdnTabindex.ClientID %>").value;
    if(tabIndex=='')
        tabIndex = 0;
        
    ddtabmenu.definemenu("ddtabs4", tabIndex) //initialize Tab Menu #4 with 3rd tab selected

    formatParentCatDropDown(document.getElementById("<%=ddlCategoryType.ClientID%>"));
    var itemGUID = document.getElementById("<%= hdnitemGUID.ClientID %>").value;
    var tabpanel = document.getElementById('tabTD');
    if (itemGUID != '')
        tabpanel.style.display = 'block';
    else
        tabpanel.style.display = 'none';

    if (itemGUID != '') {
        var itemCode = document.getElementById("<%= txtProductCode.ClientID %>").value;
        document.getElementById("<%= txtSKU.ClientID %>").value = itemCode;
        
        
    }
    
    
    </script>
    </form>
</body>
</html>

