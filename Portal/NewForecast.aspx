<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewForecast.aspx.cs" Inherits="avii.NewForecast" %>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="./Controls/Header.ascx" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="./Controls/Footer.ascx" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>.:: Lan Global Inc. -  Forecast ::.</title>
    <link href="aerostyle.css" rel="stylesheet" type="text/css" />
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
            var objDivTR = document.getElementById('trForecastHeader');
            trForecastHeader.className
            var sDateVal = '';
            for (i = 0; i < 16; i++) {
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
                //cb.className = 'button';

                objTD.style.width = '72px';
                objTD.style.height = '28px';
                objTD.className = 'button';
                sDateVal = arrHid[i].value.split(' ');
                cb.value = sDateVal[0];
                //cb.id = sDateVal[0];
                cb.name = 'checkDate';
                //var objHdn = document.createElement("<input  type=\"hidden\" >");
                //objHdn.id = 'hdnDate_' + sDateVal[0];
                //objHdn.value = sDateVal[0];

                var label = document.createElement('label')
                //alert(label);
                label.htmlFor = sDateVal[0];
                label.appendChild(document.createTextNode(sDateVal[0]));
                //alert(cb.value);
                objTD.appendChild(cb);
                objTD.appendChild(label);
                //objTD.innerHTML = sDateVal[0];
                objDivTR.appendChild(objTD);
            }
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

        function runpo(fguid) {
            alert('in');
            alert(fguid);
        }

    </script>
</head>
<body>
    <form id="frmItemForecast" runat="server">
	<table cellspacing="0" cellpadding="0" width="100%" border="0" align="center">
	<tr>
		<td>
		    <head:menuheader id="MenuHeader1" runat="server"></head:menuheader>
		</td>
	</tr>
	<tr><td>&nbsp;</td></tr>
    <tr>
		<td colspan="6" bgcolor="#dee7f6" class="buttonlabel">&nbsp;&nbsp;<asp:Label ID="lblHeader" runat="server"></asp:Label>
		</td>
    </tr>
        <tr>
            <td>
                <table  cellspacing="1" cellpadding="1" width="100%">
                                <tr><td class="copy10grey">
                                - Forecast can be entered for 12 weeks by selecting the forecast date checkbox on header of the grid<br />
                                - After entering the forecast, select a week from grid header and click on "Create Purchase Order" button to create the purchase orders for the forecast.</td></tr>
                    </table>
            </td>
        </tr>
        <tr><td><asp:Label ID="lblMsg" runat="server" CssClass="errormessage"></asp:Label></td></tr>

	<tr>
	    <td>
	        <table style="overflow:scroll;width:100%;" cellspacing="0" cellpadding="0" border="0" align="center">
	        <tr><td>&nbsp;</td></tr>
	        <tr><td align="right"><asp:HiddenField ID="hdnForCastDate" runat="server"  />
                    <asp:Button ID="btnCreatePO" runat="server" OnClientClick="return Validate();" Text="     Create Purchase Order    " CssClass="button" OnClick="btnCreatePO_Click" />      
                </td></tr>       
	        <tr>
		        <td>
                <asp:GridView runat="server" CssClass="copy10grey" ID="gvForecast" AutoGenerateColumns="false" Width="100%">
                <AlternatingRowStyle BackColor="Gainsboro" />
                
                <Columns>
                    
                    <asp:TemplateField HeaderText="SKU" HeaderStyle-CssClass="buttongrid" ItemStyle-Width="12%" >
                        <%--<ItemStyle Width="20%" />--%>
                        <ItemTemplate >
                            <asp:HiddenField ID="hdnItemID" runat="server" Value='<%#Eval("itemID") %>' />
                            <asp:Label ID="lblItemSKU" Text='<%#Eval("itemSKU") %>' runat="server" CssClass="copy10grey" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--<asp:TemplateField HeaderText="UPC" HeaderStyle-CssClass="button" Visible="false">
                        <ItemStyle Width="30%"  CssClass="copy10grey" />
                        <ItemTemplate >
                            <%#Eval("UPC") %>
                        </ItemTemplate>
                    </asp:TemplateField>
--%>
                    <asp:TemplateField>
                        <HeaderStyle Width="100%" />
                        <HeaderTemplate>
                            <table style="height:10%;width:100%" cellpadding="0" cellspacing="0" border="0" >
                               <tr id="trForecastHeader">
                               </tr>
                            </table>
                        </HeaderTemplate>
                        <%--<ItemStyle Width="80%" />--%>
                        <ItemTemplate>
                            <asp:DataList ID="dlsItemForecast" runat="server" CssClass="copy10grey"
                                RepeatColumns="16" RepeatDirection="Horizontal" Width="100%" GridLines="vertical"
                                DataSource='<%# Eval("ItemForecasts") %>' OnItemDataBound="dlsItemForecast_ItemDataBound" >
                                <HeaderStyle CssClass="buttongrid" />
                                <HeaderTemplate>
                                    <%--<asp:Label ID="lblWeek" Visible="true" Text='<%# Eval("forecastDate") %>' runat="server" />--%>
                                </HeaderTemplate>
                                <ItemStyle width="62px" Height="100%" />
                                <ItemTemplate  >
                                    <table cellpadding="0" cellspacing="0" border="0" style="width:101%; height:100%">
                                    <tr style="height:26px">
                                        <td style="width:101%;" align="center" class='cell_<%# DataBinder.Eval( Container.DataItem, "StatusID") %>'>
                                        <%--<%# Convert.ToDateTime(Eval("forecastDate")).ToShortDateString() %>--%>
                                            <asp:HiddenField ID="hidForecastGUID" runat="server" Value='<%# Eval("forecastGUID") %>' />
                                            <input type="hidden" name="hidForecastDate[]" value='<%# Eval("forecastDate") %>' />
                                            <asp:HiddenField ID="hidForecastDate" runat="server" Value='<%# Eval("forecastDate") %>' />
                                            <asp:HiddenField ID="hidForecastStatusID" runat="server" Value='<%# Eval("StatusID") %>' />
                                            <asp:HiddenField ID="hdnPOForecastGUID" runat="server" Value='<%# Eval("POForecastGUID") %>' />
                                            <%--<asp:CheckBox ID="chkLocked" runat="server" visible='<%# !Convert.ToBoolean(DataBinder.Eval( Container.DataItem, "StatusID")) %>' />--%>
                                            <asp:CheckBox ID="chkLocked" runat="server" visible="false" />
                                            
                                            <%--<asp:TextBox ID="txtQty" Text='<%# Convert.ToInt32(DataBinder.Eval( Container.DataItem, "qty")) > 0? DataBinder.Eval( Container.DataItem, "qty"): "" %>' runat="server" CssClass="txfield1" MaxLength="5" Width="50px" Visible='<%# !Convert.ToBoolean(DataBinder.Eval( Container.DataItem, "StatusID")) %>'/>
--%>

                                            <asp:TextBox ID="txtQty" ReadOnly='<%# Convert.ToInt32(DataBinder.Eval( Container.DataItem, "POForecastGUID")) > 0? true: false %>' Text='<%# Convert.ToInt32(DataBinder.Eval( Container.DataItem, "qty")) > 0? DataBinder.Eval( Container.DataItem, "qty"): "" %>' runat="server" CssClass="txfield1" MaxLength="5" Width="45px" Visible='<%# Convert.ToInt32(Container.ItemIndex) > 3 ? true : false %>' />
                                            <%# Convert.ToInt32(Container.ItemIndex) > 3 ? "" : Convert.ToString(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "qty")) > 0 ? DataBinder.Eval(Container.DataItem, "qty") : "")%>
                                            <%--<asp:HyperLink ID="lnkQty" Text='<%# Eval("qty") %>' NavigateUrl='<%# "./po.aspx?fty1=" + DataBinder.Eval(Container.DataItem,"ItemType") + "&fid=" + DataBinder.Eval (Container.DataItem,"forecastGUID") %>' runat="server" CssClass="copy10grey" Visible='<%# Convert.ToBoolean(DataBinder.Eval( Container.DataItem, "StatusID")) && Convert.ToInt32(DataBinder.Eval( Container.DataItem, "qty")) > 0 %>' />--%>
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
		<tr><td>&nbsp;</td></tr>
            
            </table>
            <div>
                <center>
                    <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="button"  OnClick="btnUpdate_Click" /> &nbsp; 
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="button" OnClick="btnCancel_Click"/>
                </center>
            </div>
	    </td>
	</tr>
    <tr>
        <td>
            <foot:MenuFooter id="Menuheader2" runat="server"></foot:MenuFooter>
        </td>
    </tr>
    </table>
    </form>
    <script type="text/javascript">
        populateForecastHeader()
    </script>
</body>
</html>