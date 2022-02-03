<%@ Page Language="C#" AutoEventWireup="true" CodeFile="forecastAdmin.aspx.cs" Inherits="avii.forecastAdmin" %>
<%--<%@ Register TagPrefix="head" TagName="MenuHeader" Src="./admhead.ascx" %>--%>
<%@ Register TagPrefix="head" TagName="menuheader" Src="~/dhtmlxmenu/menuControl.ascx" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Forecast Admin</title>
   <link href="../aerostyle.css" rel="stylesheet" type="text/css"/>
   <link href="css/aerostyle.css" rel="stylesheet" type="text/css"/>
    <link rel="stylesheet"  type="text/css" href="../fullfillment/calendar/dhtmlgoodies_calendar.css" media="screen"></link>
		
		<script type="text/javascript" src="../fullfillment/calendar/dhtmlgoodies_calendar.js"></script>  
		
    <script type="text/javascript">
        function populateForecastHeader()
        {
            var arrHid = document.getElementsByName("hidForecastDate[]");
            var arrItem = document.getElementsByName("hidItemSKU[]");
            var iColCount = 16;
//alert(arrItem.length);
            /*
            if(arrItem.length > 0)
                iColCount = eval(arrHid.length / arrItem.length);
            */
            var objDivTR = document.getElementById('trForecastHeader');
            if(!objDivTR)
                return;
            var sDateVal='';
            //var objTable = cre
            for(i=0; i<iColCount; i++)
            {
//alert(arrHid[i].value);

                var objTD = document.createElement('td');
                objTD.style.width = '74px';
                objTD.className = 'button';
                if(arrHid[i])
                    sDateVal = arrHid[i].value.split(' ');
                else 
                    sDateVal[0] = '&nbsp;-&nbsp;';
                    
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
	<body bgcolor="#ffffff" style="margin: 0 0 0 0;">
		<form id="Form1" method="post" enctype="multipart/form-data" runat="server">
		<table cellspacing="0" cellpadding="0" width="100%" align="center" border="0">
	    <tr>
		    <td>
		        <head:menuheader id="MenuHeader1" runat="server"></head:menuheader>
		    </td>
	    </tr>
	    <tr>
	        <td>
		        <table style="border-color:gray;" cellspacing="0" cellpadding="0" width="100%" align="center" border="0">
			    <tr style="border-color:white;" >
				    <td>
					    <table width="100%">
						<tr>
							<td class="button">&nbsp;Forecasts Administration</td>
						</tr>
					    </table>
				    </td>
			    </tr>
		        </table>
            </td>
        </tr>
        <tr><td>&nbsp;</td></tr>
        <tr><td><asp:Label ID="lblMsg" runat="server" CssClass="errormessage"></asp:Label></td></tr>
	    <tr>
	        <td>
	            <div>
	        	    <table bordercolor="gray" cellspacing="0" cellpadding="0" width="100%" align="center" border="1">
			        <tr bordercolor="white">
				        <td>
	                        <table cellspacing="0" cellpadding="0" width="100%" border="0" align="center">
	                        <tr><td colspan="4">&nbsp;</td></tr>	                   
	                        <tr>
	                            <td class="LabelBold" align="right">Customer:&nbsp;&nbsp;</td>
	                            <td align="left" colspan="3">
	                                <asp:DropDownList ID="ddlCustomer" runat="server" CssClass="txfield1"  ></asp:DropDownList>
	                            </td>
	                        </tr>
	                        <tr>
	                            <td class="LabelBold" align="right">Forecast Date From:&nbsp;&nbsp;</td>
	                            <td align="left">
	                                <asp:TextBox runat="server" MaxLength="10" ID="txtDateFrom" CssClass="txfield1" />
	                                <img id="imgFromtDate" alt="" onclick="document.getElementById('<%=txtDateFrom.ClientID%>').value = ''; displayCalendar(document.getElementById('<%=txtDateFrom.ClientID%>'),'mm/dd/yyyy',this,true);" src="../fullfillment/calendar/sscalendar.jpg" />
                                </td>
	                            <td class="LabelBold" align="right">Forecast Date To:&nbsp;&nbsp;</td>
	                            <td align="left">
                                <asp:TextBox runat="server" MaxLength="10" ID="txtDateTo" CssClass="txfield1"/>
	                            <img id="img1" alt="" onclick="document.getElementById('<%=txtDateTo.ClientID%>').value = ''; displayCalendar(document.getElementById('<%=txtDateTo.ClientID%>'),'mm/dd/yyyy',this,true);" src="../fullfillment/calendar/sscalendar.jpg" />
                                
                            </td>
	                    </tr>
	                    <tr>
                        	<td class="LabelBold" align="right">Brand:&nbsp;&nbsp;</td>
                            <td align="left">
                                <asp:DropDownList ID="dpBrand"  CssClass="txfield1" runat="server">
                                    <asp:ListItem Text=""></asp:ListItem>
                                    <asp:ListItem Text="LG">LG</asp:ListItem>
                                    <asp:ListItem Text="SAMSUNG">Samsung</asp:ListItem>
                                    <asp:ListItem Text="PCD">PCD</asp:ListItem>
                                    <asp:ListItem Text="ACER">Acer</asp:ListItem>
                                    <asp:ListItem Text="SANYO">Sanyo</asp:ListItem>
                                    <asp:ListItem Text="NOKIA">Nokia</asp:ListItem>
                                    <asp:ListItem Text="MOTOROLA">Motorola</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td class="LabelBold" align="right">SKU:&nbsp;&nbsp;</td>
                            <td align="left">
                            <asp:TextBox MaxLength="30" runat="server" ID="txtSKU" CssClass="txfield1"/>
                            </td>
	                    </tr>
	                    <tr>
	                        <td colspan="4">
	                            <center>
                                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="button" OnClick="btnSearch_Click" /> &nbsp; 
                                <asp:Button ID="btnSearchCancel" runat="server" Text="Cancel" CssClass="button" OnClick="btnSearchCancel_Click" />
                                </center>
	                        </td>
	                    </tr>  
	                    <tr><td>&nbsp;</td></tr>
	                    </table>
	                </td>
	              </tr>
	              
	            </table>
	        </div>
	        <div>
	        <br />
            <asp:GridView runat="server" CssClass="copy10grey" ID="gvForecast" AutoGenerateColumns="false" OnRowDataBound="gvForecast_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="SKU" HeaderStyle-CssClass="button" >
                        <ItemStyle Width="80px" />
                        <ItemTemplate >
                            <asp:HiddenField ID="hdnItemID" runat="server" value='<%#Eval("itemID") %>' />
                            <input type="hidden" name="hidItemSKU[]" value='<%#Eval("itemID") %>' />
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
                        <HeaderStyle Width="1184px" />
                        <HeaderTemplate>
                            <table style="height:10%;width:1184px" cellpadding="0" cellspacing="0" border="0" >
                               <tr id="trForecastHeader">
                               </tr>
                            </table>
                        </HeaderTemplate>
                        <ItemStyle Width="1184px" />
                        <ItemTemplate>
                            <asp:DataList ID="dlsItemForecast" runat="server" CssClass="copy10grey"
                                RepeatColumns="16" RepeatDirection="Horizontal" GridLines="vertical" Width="1048px"
                                DataSource='<%# Eval("ItemForecasts") %>' OnItemDataBound="dlsItemForecast_ItemDataBound" >
                                <HeaderStyle CssClass="button" />
                                <HeaderTemplate>
                                    <%--<asp:Label ID="lblWeek" Visible="true" Text='<%# Eval("forecastDate") %>' runat="server" />--%>
                                </HeaderTemplate>
                                <ItemStyle width="74px" Height="100%" />
                                <ItemTemplate  >
                                    <table cellpadding="0" cellspacing="0" style="width:100%; height:100%">
                                    <tr >
                                        <td style="width:100%;" align="center" class='cell_<%# isEditable %>'>
                                            <asp:HiddenField ID="hidForecastGUID" runat="server" Value='<%# Eval("forecastGUID") %>' />
                                            <input type="hidden" name="hidForecastDate[]" value='<%# Eval("forecastDate") %>' />
                                            <asp:HiddenField ID="hidForecastDate" runat="server" Value='<%# Eval("forecastDate") %>' />
                                            <asp:HiddenField ID="hidForecastStatusID" runat="server" Value='<%# Eval("StatusID") %>' />
                                            <asp:CheckBox ID="chkLocked" runat="server" visible='<%# isEditable %>' Checked='<%# Convert.ToBoolean(DataBinder.Eval( Container.DataItem, "StatusID")) %>' />
                                            <asp:TextBox ID="txtQty" Text='<%# Convert.ToInt32(DataBinder.Eval( Container.DataItem, "qty")) > 0? DataBinder.Eval( Container.DataItem, "qty"): "" %>' runat="server" CssClass="txfield1" MaxLength="6" Width="32px" Visible='<%# isEditable %>'/>
                                            <asp:LinkButton ID="lnkQty" Text='<%# Eval("qty") %>' runat="server" CssClass="copy10grey" Visible='<%# !isEditable %>' />
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
    <script type="text/javascript">populateForecastHeader()</script>
</body>
</html>
