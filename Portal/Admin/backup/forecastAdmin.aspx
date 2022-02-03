<%@ Page Language="C#" AutoEventWireup="true" Codebehind="forecastAdmin.aspx.cs" Inherits="avii.forecastAdmin" %>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="./admhead.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Forecast Admin</title>
   <link href="../aerostyle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function populateForecastHeader()
        {
            var arrHid = document.getElementsByName("hidForecastDate[]");
            var objDivTR = document.getElementById('trForecastHeader');
            trForecastHeader.className
            var sDateVal='';
            //var objTable = cre
            for(i=0; i<16; i++)
            {
//alert(arrHid[i].value);
                var objTD = document.createElement('td');
                objTD.style.width = '72px';
                objTD.className = 'button';
                sDateVal = arrHid[i].value.split(' ');
                objTD.innerHTML = sDateVal[0];
                objDivTR.appendChild(objTD)
//alert(objTD.innerHTML);                
            }
//alert(arrHid.length);
        }
        function checkQty(obj)
        {
//alert(obj.id);
            var objtxt = document.getElementById(obj.id.replace('chkLocked', 'txtQty'));
//alert(objtxt) 
            var isValid = true;           
            if(isNaN(objtxt.value))
                isValid = false;
            else if(objtxt.value<=0)
                isValid = false
                
            if(!isValid)
            {
                alert('invalid quantity');
                objtxt.value = '';
                objtxt.focus();
                obj.checked = false;
            }
        }
        
        function isNumberKey(evt)
        {
            var e = event || evt; // for trans-browser compatibility
            var charCode = e.which || e.keyCode;

            if (charCode > 31 && (charCode < 48 || charCode > 57))
            {
                e.keyCode=0;
                return false;
            }
            return true;
        }

    </script>
</head>
	<body bgColor="#ffffff" leftmargin="0" rightmargin="0" topmargin="0">
		<form id="Form1" method="post" encType="multipart/form-data" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
	<tr>
		<td>
		    <head:menuheader id="MenuHeader1" runat="server"></head:menuheader>
		</td>
	</tr>
	<tr>
	<td>
		<table borderColor="gray" cellSpacing="0" cellPadding="0" width="100%" align="center" border="1">
			<tr borderColor="white">
				<td>
					<table width="100%">
						<tr>
							<td class="copyblue11b">&nbsp;Forecasts</td>
						</tr>
					</table>
				</td>
			</tr>
		</table>
	</td>
</tr>
<tr><td><asp:Label ID="lblMsg" runat="server" CssClass="errormessage"></asp:Label></td></tr>
	<tr>
	    <td>
	        <div>
	        
	        <table borderColor="gray" cellSpacing="0" cellPadding="0" width="100%" align="center" border="1">
			    <tr borderColor="white">
				    <td>
	                     <table cellspacing="0" cellpadding="0" width="100%" border="0" align="center">
	                    <tr><td>&nbsp;</td></tr>	                   
	                    <tr>
	                        <td class="LabelBold" align="right">Customer:&nbsp;&nbsp;</td>
	                        <td align="left">
	                            <asp:DropDownList ID="ddlCustomer" runat="server" CssClass="txfield1" OnSelectedIndexChanged="ddlCustomer_SelectedIndexChanged" AutoPostBack="true" ></asp:DropDownList>
	                        </td>
	                    </tr>
	                    <tr>
	                        <td class="LabelBold" align="right">Forecast Date From:&nbsp;&nbsp;</td>
	                        <td align="left">
	                            <asp:TextBox runat="server" ID="txtDateFrom" CssClass="txfield1" />
                            </td>
	                        <td class="LabelBold" align="right">Forecast Date To:&nbsp;&nbsp;</td>
	                        <td align="left">
                                <asp:TextBox runat="server" ID="txtDateTo" CssClass="txfield1" />
                            </td>
	                    </tr>
	                    <tr><td>&nbsp;</td></tr>	                   
	                </table>
	                </td>
	              </tr>
	            </table>
	        </div>
	        <div>
            
             <table cellspacing="0" cellpadding="0" width="100%" border="0" align="center">
	                    <tr><td>&nbsp;</td></tr>	                   

                <tr>
                    <td>
                        <center>
                            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="button" OnClick="btnUpdate_Click" /> &nbsp; 
                            <asp:Button ID="btnSearchCancel" runat="server" Text="Cancel" CssClass="button" OnClick="btnSearchCancel_Click" />
                        </center>
                    </td>
               </tr>
	                    <tr><td>&nbsp;</td></tr>	                   
               
           </table>
                        
            
            <asp:GridView runat="server" CssClass="copy10grey" ID="gvForecast" AutoGenerateColumns="false">
                <Columns>
                    <asp:TemplateField HeaderText="SKU" HeaderStyle-CssClass="button" >
                        <ItemStyle Width="80px" />
                        <ItemTemplate >
                            <asp:HiddenField ID="hdnItemID" runat="server" Value='<%#Eval("itemID") %>' />
                            <asp:Label ID="lblItemSKU" Text='<%#Eval("itemSKU") %>' runat="server" CssClass="copy10grey" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Description" HeaderStyle-CssClass="button">
                        <ItemStyle Width="130px" />
                        <ItemTemplate >
                            <asp:Label ID="lblItemDesc" Text='<%#Eval("itemDesc") %>' runat="server" CssClass="copy10grey" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField>
                        <HeaderStyle Width="760px" />
                        <HeaderTemplate>
                            <table style="height:10%;width:1048px" cellpadding="0" cellspacing="0" border="0" >
                               <tr id="trForecastHeader">
                               </tr>
                            </table>
                        </HeaderTemplate>
                        <ItemStyle Width="760px" />
                        <ItemTemplate>
                            <asp:DataList ID="dlsItemForecast" runat="server" CssClass="copy10grey"
                                RepeatColumns="16" RepeatDirection="Horizontal" Width="1048px" GridLines="vertical"
                                DataSource='<%# Eval("ItemForecasts") %>' OnItemDataBound="dlsItemForecast_ItemDataBound" >
                                <HeaderStyle CssClass="button" />
                                <HeaderTemplate>
                                    <%--<asp:Label ID="lblWeek" Visible="true" Text='<%# Eval("forecastDate") %>' runat="server" />--%>
                                </HeaderTemplate>
                                <ItemStyle width="60px" Height="100%" />
                                <ItemTemplate  >
                                    <table cellpadding="0" cellspacing="0" style="width:60px; height:100%">
                                    <tr style="height:16px">
                                        <td style="width:60px;" align="center" class='cell_<%# DataBinder.Eval( Container.DataItem, "StatusID") %>'>
                                            <asp:HiddenField ID="hidForecastGUID" runat="server" Value='<%# Eval("forecastGUID") %>' />
                                            <input type="hidden" name="hidForecastDate[]" value='<%# Eval("forecastDate") %>' />
                                            <asp:HiddenField ID="hidForecastDate" runat="server" Value='<%# Eval("forecastDate") %>' />
                                            <asp:HiddenField ID="hidForecastStatusID" runat="server" Value='<%# Eval("StatusID") %>' />
                                            <asp:CheckBox ID="chkLocked" runat="server" visible='<%# !Convert.ToBoolean(DataBinder.Eval( Container.DataItem, "StatusID")) %>' />
                                            <asp:TextBox ID="txtQty" Text='<%# Convert.ToInt32(DataBinder.Eval( Container.DataItem, "qty")) > 0? DataBinder.Eval( Container.DataItem, "qty"): "" %>' runat="server" CssClass="txfield1" MaxLength="6" Width="32px" Visible='<%# !Convert.ToBoolean(DataBinder.Eval( Container.DataItem, "StatusID")) %>'/>
                                            <asp:LinkButton ID="lnkQty" Text='<%# Eval("qty") %>' runat="server" CssClass="copy10grey" Visible='<%# Convert.ToBoolean(DataBinder.Eval( Container.DataItem, "StatusID")) && Convert.ToInt32(DataBinder.Eval( Container.DataItem, "qty")) > 0 %>' />
                                        </td>
                                    </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:DataList>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                </asp:GridView>
            </div>
            <div>
                <center>
                    <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="button" Visible="false"  OnClick="btnUpdate_Click" /> &nbsp; 
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="button"  Visible="false" OnClick="btnCancel_Click"/>
                </center>
            </div>
	    </td>
	</tr>
    <tr>
        <td>
        </td>
    </tr>
    </table>
    </form>
    <script type="text/javascript">
        populateForecastHeader()
    </script>
</body>
</html>
