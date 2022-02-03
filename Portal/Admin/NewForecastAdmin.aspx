<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewForecastAdmin.aspx.cs" Inherits="avii.Admin.NewForecastAdmin" %>
<%@ Register TagPrefix="head" TagName="menuheader" Src="~/Controls/Header.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Forecast Admin</title>
   <link href="../aerostyle.css" rel="stylesheet" type="text/css"/>
   
    <link rel="stylesheet"  type="text/css" href="../fullfillment/calendar/dhtmlgoodies_calendar.css" media="screen"></link>
		
		<script type="text/javascript" src="../fullfillment/calendar/dhtmlgoodies_calendar.js"></script>  
		
    <script type="text/javascript">
        function Validate() {

            var foreCastDate = document.getElementById("<%=hdnForCastDate.ClientID %>").value
            //alert(foreCastDate);
            if (foreCastDate == '') {
                alert('Forecast date required');
                return false;
            }
        }
        function clickHandler() {
            obj = event.srcElement;

            //alert(obj);
            //alert(obj.value);
            if (obj != null) {

                if (obj.name == 'checkDate') {
                    var flag = obj.checked;
                    var arrChecks = document.getElementsByName("checkDate");
                    for (i = 0; i < arrChecks.length; i++) {
                        arrChecks[i].checked = false;
                    }
                    obj.checked = flag;
                    if (obj.checked)
                        document.getElementById("<%=hdnForCastDate.ClientID %>").value = obj.value;
                    else
                        document.getElementById("<%=hdnForCastDate.ClientID %>").value = '';
                }
            }
            // alert(document.getElementById("<%=hdnForCastDate.ClientID %>").value);
        }
        document.onclick = clickHandler;
        function populateForecastHeader() {
            var arrHid = document.getElementsByName("hidForecastDate[]");
            var arrItem = document.getElementsByName("hidItemSKU[]");
            var iColCount = 16;
            //alert(arrItem.length);
            /*
            if(arrItem.length > 0)
            iColCount = eval(arrHid.length / arrItem.length);
            */
            var objDivTR = document.getElementById('trForecastHeader');
            if (!objDivTR)
                return;
            var sDateVal = '';
            //var objTable = cre
            for (i = 0; i < iColCount; i++) {
                //alert(arrHid[i].value);

                var objTD = document.createElement('td');
                var cb;
                try {
                    cb = document.createElement("<input type=\"checkbox\" >");
                }
                catch (e) {
                    cb = document.createElement("input");
                    cb.type = "checkbox";
                    //cb.checked = true;
                }
                objTD.style.width = '100px';
                objTD.style.height = '28px';
                objTD.className = 'button';
                if (arrHid[i])
                    sDateVal = arrHid[i].value.split(' ');
                else
                    sDateVal[0] = '&nbsp;-&nbsp;';

                cb.value = sDateVal[0];
                //cb.id = sDateVal[0];
                cb.name = 'checkDate';

                //objTD.innerHTML = sDateVal[0];
                //objDivTR.appendChild(objTD)
                var label = document.createElement('label')
                //alert(label);
                label.htmlFor = sDateVal[0];
                label.appendChild(document.createTextNode(sDateVal[0]));
                //alert(cb.value);
                objTD.appendChild(cb);
                objTD.appendChild(label);
                //objTD.innerHTML = sDateVal[0];
                objDivTR.appendChild(objTD);

                //alert(objTD.innerHTML);                
            }
            //alert(arrHid.length);
        }
        function checkQty(obj) {
            //alert(obj.id);
            var objtxt = document.getElementById(obj.id.replace('chkLocked', 'txtQty'));
            //alert(objtxt) 
            var isValid = true;
            if (isNaN(objtxt.value))
                isValid = false;
            else if (objtxt.value <= 0)
                isValid = false

            if (!isValid) {
                alert('invalid quantity');
                objtxt.value = '';
                objtxt.focus();
                obj.checked = false;
            }
        }

        function isNumberKey(evt) {
            var e = event || evt; // for trans-browser compatibility
            var charCode = e.which || e.keyCode;

            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                e.keyCode = 0;
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
        <tr><td><asp:Label ID="lblMsg" runat="server" CssClass="errormessage"></asp:Label>
        </td></tr>
	    <tr>
	        <td>
	            <div>
                <asp:Panel ID="pnlSearch" runat="server" DefaultButton="btnSearch">
	        	    <table bordercolor="#839abf" cellspacing="0" cellpadding="0" width="100%" align="center" border="1">
			        <tr bordercolor="#839abf">
				        <td>
	                        <table cellspacing="3" cellpadding="3" width="100%" border="0" align="center">
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
	                                <asp:TextBox runat="server" onkeypress="return false;" MaxLength="10" ID="txtDateFrom" CssClass="txfield1" />
	                                <img id="imgFromtDate" alt="" onclick="document.getElementById('<%=txtDateFrom.ClientID%>').value = ''; displayCalendar(document.getElementById('<%=txtDateFrom.ClientID%>'),'mm/dd/yyyy',this,true);" src="../fullfillment/calendar/sscalendar.jpg" />
                                </td>
	                            <td class="LabelBold" align="right">Forecast Date To:&nbsp;&nbsp;</td>
	                            <td align="left">
                                <asp:TextBox runat="server" onkeypress="return false;" MaxLength="10" ID="txtDateTo" CssClass="txfield1"/>
	                            <img id="img1" alt="" onclick="document.getElementById('<%=txtDateTo.ClientID%>').value = ''; displayCalendar(document.getElementById('<%=txtDateTo.ClientID%>'),'mm/dd/yyyy',this,true);" src="../fullfillment/calendar/sscalendar.jpg" />
                                
                            </td>
	                    </tr>
	                    <tr>
                        	<td class="LabelBold" align="right">Brand:&nbsp;&nbsp;</td>
                            <td align="left">
                                <asp:DropDownList ID="dpBrand" Width="120px"  CssClass="txfield1" runat="server">
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
                        <tr height="8" ><td colspan="4"><hr /></td></tr>
	                    <tr>
	                        <td colspan="4">
	                            <center>
                                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="button" OnClick="btnSearch_Click" /> &nbsp; 
                                <asp:Button ID="btnSearchCancel" runat="server" Text="Cancel" CssClass="button" OnClick="btnSearchCancel_Click" />
                                </center>
	                        </td>
	                    </tr>  
	                    <tr height="8"><td></td></tr>
	                    </table>
	                </td>
	              </tr>
	              
	                </table>
                </asp:Panel>
	        </div>
	        <div>
	        <br />
            <table style="overflow:scroll;width:100%;" cellspacing="0" cellpadding="0" border="0" align="center">
	        <tr><td>&nbsp;</td></tr>
	        <tr><td align="right"><asp:HiddenField ID="hdnForCastDate" runat="server"  />
                    <asp:Button ID="btnCreatePO" runat="server" OnClientClick="return Validate();" Text="     Create Purchase Order    " CssClass="button" OnClick="btnCreatePO_Click" />      
                </td></tr>       
	        <tr>
		        <td>
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
                        <HeaderStyle Width="100%" />
                        <HeaderTemplate>
                            <table style="height:10%;width:100%" cellpadding="0" cellspacing="0" border="0" >
                               <tr id="trForecastHeader">
                               </tr>
                            </table>
                        </HeaderTemplate>
                        <ItemStyle Width="100%" />
                        <ItemTemplate>
                            <asp:DataList ID="dlsItemForecast"  runat="server" CssClass="copy10grey"
                                RepeatColumns="16" RepeatDirection="Horizontal" GridLines="vertical" Width="100%"
                                DataSource='<%# Eval("ItemForecasts") %>' OnItemDataBound="dlsItemForecast_ItemDataBound" >
                                <HeaderStyle CssClass="button" />
                                <HeaderTemplate>
                                    <%--<asp:Label ID="lblWeek" Visible="true" Text='<%# Eval("forecastDate") %>' runat="server" />--%>
                                </HeaderTemplate>
                                <ItemStyle width="100px" Height="100%" />
                                <ItemTemplate  >
                                    <table cellpadding="0" border="0" cellspacing="0" style="width:100%; height:100%">
                                    <tr >
                                        <td style="width:100%;" align="center" class='cell_<%# isEditable %>'>
                                            <asp:HiddenField ID="hidForecastGUID" runat="server" Value='<%# Eval("forecastGUID") %>' />
                                            <input type="hidden" name="hidForecastDate[]" value='<%# Eval("forecastDate") %>' />
                                            <asp:HiddenField ID="hidForecastDate" runat="server" Value='<%# Eval("forecastDate") %>' />
                                            <asp:HiddenField ID="hidForecastStatusID" runat="server" Value='<%# Eval("StatusID") %>' />
                                            <asp:HiddenField ID="hdnPOForecastGUID" runat="server" Value='<%# Eval("POForecastGUID") %>' />
                                            
                                            <%--<asp:CheckBox ID="chkLocked" runat="server" visible='<%# isEditable %>' Checked='<%# Convert.ToBoolean(DataBinder.Eval( Container.DataItem, "StatusID")) %>' />--%>
                                            <asp:CheckBox ID="chkLocked" runat="server" visible="false" />
                                            
                                            <%--<asp:TextBox ID="txtQty" Text='<%# Convert.ToInt32(DataBinder.Eval( Container.DataItem, "qty")) > 0? DataBinder.Eval( Container.DataItem, "qty"): "" %>' runat="server" CssClass="txfield1" MaxLength="6" Width="32px" Visible='<%# isEditable %>'/>
                                            <asp:LinkButton ID="lnkQty" Text='<%# Eval("qty") %>' runat="server" CssClass="copy10grey" Visible='<%# !isEditable %>' />--%>
                                            <asp:TextBox ID="txtQty" ReadOnly='<%# Convert.ToInt32(DataBinder.Eval( Container.DataItem, "POForecastGUID")) > 0? true: false %>' Text='<%# Convert.ToInt32(DataBinder.Eval( Container.DataItem, "qty")) > 0? DataBinder.Eval( Container.DataItem, "qty"): "" %>' runat="server" CssClass="txfield1" MaxLength="5" Width="35px"  />
                                            <%--<%# Convert.ToInt32(Container.ItemIndex) > 3 ? "" : Convert.ToString(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "qty")) > 0 ? DataBinder.Eval(Container.DataItem, "qty") : "")%>
--%>
                                        </td>
                                    </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:DataList>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                </asp:GridView>
                </td>
            </tr>
            </table>
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
    <script type="text/javascript">        populateForecastHeader()</script>
</body>
</html>
