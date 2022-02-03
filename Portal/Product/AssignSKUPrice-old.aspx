<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AssignSKUPrice.aspx.cs" Inherits="avii.Product.AssignSKUPrice" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="/Controls/Footer.ascx" %>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="/Controls/Header.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Aerovoice Inc. - Assign SKU Prices</title>
    <link href="../aerostyle.css" type="text/css" rel="stylesheet" />

    <script type="text/javascript">
        function Validate() {

            var company = document.getElementById("<% =dpCompany.ClientID %>");
            if (company.selectedIndex == 0) {
                alert('Customer is required!');
                return false;

            }

        }

        function CheckDecimal(inputtxt) {
            var decimal = /^[-+]?[0-9]+\.[0-9]+$/;
            if (inputtxt.value.match(decimal)) {
                //alert('Correct, try another...')
                return true;
            }
            else {
                alert('Invalid value!');
                return false;
            }
        }
        //var priceValue='';
        function ValidatePrice(evt, obj) {
            var charCodes = evt.keyCode ? evt.keyCode : evt.which ? evt.which : evt.charCode;
            //alert(charCodes);
            var priceValue = obj.value;
            //alert(priceValue);
            //alert(priceValue.indexOf('.'))
            //if ((charCodes == 46 && priceValue.indexOf('.') > -1) || (charCodes == 190 && priceValue.indexOf('.') > -1) || (charCodes < 48 && charCodes == 46 && priceValue.indexOf('.') > -1) || (charCodes > 57 && charCodes != 190)) {
            if ((charCodes == 46 && priceValue.indexOf('.') > -1) || (charCodes < 48 && charCodes != 46)  || charCodes > 57 ) {
                //charCodes = 0;
                ///priceValue = priceValue.replace('..', '.');
                //obj.value = priceValue;

                evt.preventDefault();
                //alert('in');
                return false;
            }
            //else
                
            //priceValue = priceValue.replace('..', '.');
            //obj.value = priceValue;

            return true;
            
            //alert(priceValue);

        }

//        $("#Foo").keydown(function (e) {
//            var c = e.keyCode
//      , value = $(this).val();

//            // Prevent insertion if the inserting character is
//            // 1. a 'dot' but there is already one in the text box, or
//            // 2. not numerics.
//            if ((c == 190 && value.indexOf('.') > -1) || c < 48 || c > 57) {
//                e.preventDefault();
//                return;
//            }
//        });
		
    </script>


</head>
<body  bgColor="#ffffff" leftmargin="0" rightmargin="0" topmargin="0">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
	<table cellSpacing="0" cellPadding="0"  border="0" align="center" width="100%">
		<tr>
			<td>
			<head:menuheader id="HeadAdmin" runat="server"></head:menuheader>
			</td>
		</tr>
     </table>
    <br />
    <table  cellSpacing="1" cellPadding="1" width="100%">
        <tr>
		    <td colSpan="6" bgcolor="#dee7f6" class="button">&nbsp;&nbsp;Assign SKU PriceS
		    </td>
        </tr>

    </table>   
    <br />
    <asp:UpdatePanel ID="UpdatePanel1"  runat="server" UpdateMode="Conditional" >
    <ContentTemplate>
    
        <table cellSpacing="0" cellPadding="0" align="center" width="100%" border="0">
    	<tr>                    
            <td colspan="2">
                <asp:Label ID="lblMsg" runat="server" Width="100%" CssClass="errormessage"></asp:Label>
            </td>
        </tr>               
        <tr>
            <td align="center">
                <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="90%">
                    <tr>
                    <td>
			            <table  cellSpacing="5" cellPadding="5" width="100%" border="0">
                            <tr>
                                <td  class="copy10grey" align="right" width="42%" >
                                    Customer: &nbsp;</td>
                                <td align="left" >
                                    <asp:DropDownList ID="dpCompany" CssClass="copy10grey" runat="server" Width="55%"
                                    OnSelectedIndexChanged="dpCompany_SelectedIndexChanged"  
                                    AutoPostBack="true">
									</asp:DropDownList>
                            </tr>
                            <tr runat="server" id="trSKU">
                                <td class="copy10grey"  width="42%" align="right">
                                    SKU: &nbsp;
                                </td>
                                
                                <td  align="left">
                                <asp:DropDownList ID="ddlSKU" runat="server" class="copy10grey">
                                </asp:DropDownList>
                                            

                                </td>
                            </tr>
                            
 
                
                             
                            <tr>
                                <td colspan="2">
                                    <asp:Panel ID="pnlSubmit" runat="server" Visible="false">
                                    <table cellpadding="0" cellspacing="0" width="100%">
                                        <tr><td>
                                        <hr style="width:100%" />
                            
                                        </td></tr>   
                                         <%--<tr>                    
                                            <td align="left">
                                                <asp:Label ID="lblConfirm" runat="server" Width="100%" CssClass="errorGreenMsg"></asp:Label></td>
                                        </tr>
                                        --%>
                                    
                                         <tr>
                                            <td  align="center">
                                
                                
                                            <asp:Button ID="btnSubmit2" Visible="false" Width="190px"  CssClass="button" runat="server" OnClick="btnSubmit_Click" Text="Submit" OnClientClick="return Validate(2);" />

                                            &nbsp;<asp:Button ID="btnCancel2" runat="server"  CssClass="button" Text="Cancel" OnClick="btnCancel_Click" />
                                                </td>
                                        </tr>
                                        <%--<tr><td>
                                        <hr style="width:100%" />
                            
                                        </td></tr>   --%>
                                    </table>

                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr><td colspan="2">
                            
                            <table cellpadding="0" cellspacing="0" width="100%">
                             <%--<tr>
                                <td colspan="2" align="right" style="height:8px; vertical-align:bottom">
                                <strong>   <asp:Label ID="lblCount" CssClass="copy10grey" runat="server" ></asp:Label> &nbsp;&nbsp;</strong> 
                                </td>
                             </tr>--%>
                             <tr>
                                <td colspan="2" align="center">
                                
                                <asp:GridView ID="gvSKUPrice" runat="server" Width="60%" GridLines="Both"   AutoGenerateColumns="false"
                                    OnPageIndexChanging="gridView_PageIndexChanging" PageSize="100" AllowPaging="false" 
                                    >
                                        <RowStyle BackColor="Gainsboro" />
                                        <AlternatingRowStyle BackColor="white" />
                                        <HeaderStyle  CssClass="button" ForeColor="white"/>
                                        <FooterStyle CssClass="white"  />
                                        <PagerStyle ForeColor="#636363" CssClass="copy10grey" HorizontalAlign="Left" />
                                        <Columns>
<asp:TemplateField HeaderText="S.No." ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex +  1 %> 
                                                </ItemTemplate>
                                            </asp:TemplateField> 
                                            
                                            <asp:TemplateField HeaderText="SKU#" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="30%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSKU" runat="server" CssClass="copy10grey" Text='<%#Eval("SKU")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField> 
                                            <asp:TemplateField HeaderText="Existing Price($)"   ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="25%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPrice" runat="server" CssClass="copy10grey" Text='<%# Convert.ToInt32(Eval("SKUPrice")) > 0 ? "$ " + Eval("skuPrice", "{0:n}") : "" %>'></asp:Label>
                                                    <%--<asp:TextBox ID="txtPrice" CssClass="copy10grey" Width="90%"  Text='' runat="server"></asp:TextBox>--%>
                                                </ItemTemplate>
                                            </asp:TemplateField> 
                                            <asp:TemplateField HeaderText="Propose Price($)"   ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="copy10grey" ItemStyle-Width="25%">
                                                <ItemTemplate>
                                                <asp:TextBox ID="txtPrice" style="text-align:right" CssClass="copy10grey" Width="98%"  runat="server" onkeypress="return ValidatePrice(event, this);" ></asp:TextBox>
                                                    <%--<asp:TextBox ID="txtPrice" CssClass="copy10grey" Width="90%"  Text='<%# Convert.ToInt32(Eval("SKUPrice")) > 0 ? Eval("skuPrice") : "" %>' runat="server"></asp:TextBox>--%>
                                                </ItemTemplate>
                                            </asp:TemplateField> 
                                            <%--<asp:TemplateField HeaderText="AV Supply Chain Prices"   ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="25%">
                                                <ItemTemplate>
                                                
                                                    <asp:TextBox ID="txtAvSCP" CssClass="copy10grey" Width="90%" Text='<%# Convert.ToInt32(Eval("AVSupplyChainPrices")) > 0 ? Eval("AVSupplyChainPrices") : ""%>' runat="server"></asp:TextBox>
                                                    
                                                </ItemTemplate>
                                            </asp:TemplateField> 
                                            <asp:TemplateField HeaderText="AV Spot Buy"   ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="25%">
                                                <ItemTemplate>
                                                
                                                    <asp:TextBox ID="txtAvSB" CssClass="copy10grey" Width="90%" Text='<%# Convert.ToInt32(Eval("AVSpotBuy")) > 0 ? Eval("AVSpotBuy") : ""%>' runat="server"></asp:TextBox>
                                                    
                                                </ItemTemplate>
                                            </asp:TemplateField> 
                                            --%>
                                        </Columns>
                                    </asp:GridView>
                                    

                                </td>
                            </tr>
                            </table>
                            </td></tr>                            
                            
                            <%--<tr><td colspan="2">
                            <hr style="width:100%" />
                            
                            </td></tr>  
                                                      --%>
                            <tr id="trHr" runat="server"><td colspan="2">
                            <hr style="width:100%" />
                            
                            </td></tr>                            
                             <tr>
                                <td colspan="2" align="center">
                                
                                <asp:Button ID="btnSearch"  Width="190px" CssClass="button" runat="server" OnClick="btnSearch_Click" Text="Search" OnClientClick="return Validate();"  />

                                &nbsp;<asp:Button ID="btnSubmit" Visible="false" Width="190px"  CssClass="button" runat="server" OnClick="btnSubmit_Click" Text="Submit" OnClientClick="return Validate();"/>
                               <%-- &nbsp;<asp:Button ID="btnViewTracking" Visible="false" Width="190px"  CssClass="button" runat="server" OnClick="btnViewAssignedPos_Click" Text="View UPDATED fulfillment" />
--%>
                                &nbsp;<asp:Button ID="btnCancel" runat="server"  CssClass="button" Text="Cancel" OnClick="btnCancel_Click" />
                                    </td>
                            </tr>
                       </table>
                            
                    </td>
                    </tr>
                           
                 </table>
                 
            </td>
        </tr>
            
        </table>






    </ContentTemplate>
            
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server"
                     DynamicLayout="false">
            <ProgressTemplate>
                <img src="/Images/ajax-loaders.gif" /> Loading ...
            </ProgressTemplate>
        </asp:UpdateProgress>
        <br />
        <table cellspacing="0" cellpadding="0" border="0" align="center" width="100%">
        <tr>
            <td >
                <foot:MenuFooter ID="footer2" runat="server"></foot:MenuFooter>
            </td>
         </tr>
         </table>
    


    </form>
</body>
</html>
