<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProvisioningSearch.aspx.cs" Inherits="avii.Container.ProvisioningSearch" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="~/Controls/Header.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Provisioning</title>
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
	
    <link rel="stylesheet"  type="text/css" href="../fullfillment/calendar/dhtmlgoodies_calendar.css" media="screen" />
	<script type="text/javascript" src="../fullfillment/calendar/dhtmlgoodies_calendar.js"></script>  
    <script type="text/javascript">
       
        $(document).ready(function () {

            

            $('#txtPoNum').keypress(function (e) {
                var regex = new RegExp("^[a-zA-Z0-9-]+$");
                var str = String.fromCharCode(!e.charCode ? e.which : e.charCode);
                if (regex.test(str)) {
                    return true;
                }
                e.preventDefault();
                return false;
            });


        });

        function isQuantity(obj) {
                
                if (obj.value == '0') {
                    alert('Quantity can not be zero');
                    obj.value = '1';
                    return false;
                }
                if (obj.value == '') {
                    alert('Quantity can not be empty');
                    obj.value = '1';
                    return false;
                }
            }
            function isNumberKey(evt) {
                
                var charCodes = evt.keyCode ? evt.keyCode : evt.which ? evt.which : evt.charCode;
                if (charCodes > 31 && (charCodes < 48 || charCodes > 57)) {
                    charCodes = 0;
                    return false;
                }
                return true;
        }

        function set_focus1() {
		        var img = document.getElementById("imgDateFrom");
            var st = document.getElementById("txtPoNum");
		        st.focus();
		        img.click();
		    }
		    function set_focus2() {
		        var img = document.getElementById("imgDateTo");
                var st = document.getElementById("txtPoNum");
		        st.focus();
		        img.click();
		    }

        
            function OpenNewPage(url) {

            var newWin = window.open(url);

            if (!newWin || newWin.closed || typeof newWin.closed == 'undefined') {
                alert('your pop up blocker is enabled');

                //POPUP BLOCKED
            }
        }
    </script>
    
</head>
<body leftmargin="0" topmargin="0" marginwidth="0" marginheight="0">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    
    <table cellSpacing="0" cellPadding="0" align="center" width="100%" border="0">
        <tr>
			<td><head:MenuHeader id="HeadAdmin" runat="server"></head:MenuHeader></td>
		</tr>
     </table>
 <table cellspacing="0" cellpadding="0" border="0" align="center" width="95%">
		<tr>
			<td  bgcolor="#dee7f6" class="buttonlabel">
            &nbsp;&nbsp;Provisioning - Search
			</td>
		</tr>
    
    </table>
    <table cellSpacing="0" cellPadding="0" align="center" width="95%" border="0">
        <tr>
			<td>
    <asp:UpdatePanel ID="upnlCustomers" UpdateMode="Conditional" runat="server">
	<ContentTemplate>
	
    <table cellSpacing="0" cellPadding="0" align="center" width="100%" border="0">
    		<tr>                    
                <td >
                    <asp:Label ID="lblMsg" runat="server" Width="100%" CssClass="errormessage"></asp:Label></td>
            </tr> 
            
     </table> 
        
        
      <table bordercolor="#839abf" border="1" width="100%" align="center" cellpadding="0" cellspacing="0">
      <tr>
        <td>
         <asp:Panel  ID="pnlSearch" runat="server"  DefaultButton="btnSearch" >
     <table width="100%" border="0" class="box" align="center" cellpadding="2" cellspacing="2">
     <%--<tr style="height:1px">
     <td style="height:1px"></td>
     </tr>
     --%>
            <tr>
                <td  class="copy10grey" align="right" width="15%">
                        Customer: &nbsp;</td>
                <td align="left"  width="20%">
                        <asp:DropDownList ID="dpCompany" CssClass="copy10grey" runat="server" Width="80%">
				    </asp:DropDownList>
                </td>
                <td  width="1%">
                    &nbsp;
                </td>
                <td class="copy10grey"  align="right">
                   Fulfillment#:
                </td>
                <td>
                    <asp:TextBox ID="txtPoNum"  CssClass="copy10grey" runat="server" Width="80%" MaxLength="20" ></asp:TextBox>
                
                </td>
            </tr>
             <tr valign="top">
                <td class="copy10grey" align="right" width="15%">
                    Order Date From:
                </td>
                <td width="35%">
                    <asp:TextBox ID="txtDateFrom" runat="server" onfocus="set_focus1();"  CssClass="copy10grey" MaxLength="12"  Width="80%"></asp:TextBox>
                    <img id="imgDateFrom" alt="" onclick="document.getElementById('<%=txtDateFrom.ClientID%>').value = ''; displayCalendar(document.getElementById('<%=txtDateFrom.ClientID%>'),'mm/dd/yyyy',this,true);" src="../fullfillment/calendar/sscalendar.jpg" />
                                        
         
                </td>
                <td  width="1%">
                    &nbsp;
                </td>
                <td class="copy10grey" align="right" width="15%">
                    Date To:
                </td>
                <td width="35%">
                   
                  <asp:TextBox ID="txtDateTo" runat="server" onfocus="set_focus2();"  CssClass="copy10grey" MaxLength="12"  Width="80%"></asp:TextBox>
                    <img id="imgDateTo" alt="" onclick="document.getElementById('<%=txtDateTo.ClientID%>').value = ''; displayCalendar(document.getElementById('<%=txtDateTo.ClientID%>'),'mm/dd/yyyy',this,true);" src="../fullfillment/calendar/sscalendar.jpg" />
                                                
        
                </td>   
                
                    
                </tr>
                
                
                <tr>
                <td colspan="5">
                <hr />
                </td>
                </tr>
                <tr>
                <td  align="center"  colspan="5">
            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="button" OnClick="btnSearch_Click" CausesValidation="false"/>
            
         &nbsp;
           <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="button" OnClick="btnCancel_Click" CausesValidation="false"/>
                   
        </td>
        </tr>
            </table>
            </asp:Panel>
   
     </td>
     </tr>
     </table>  
        <br />
        <table align="center" style="text-align:left" width="100%">
       <tr>
            <td  align="right">
               <strong>  <asp:Label ID="lblPO" runat="server"  CssClass="copy10grey"></asp:Label></strong> 

            </td>
           </tr>
        <tr>
            <td  align="left">
                
            <asp:GridView ID="gvPO" runat="server" AutoGenerateColumns="false" 
                  Width="100%" GridLines="Both" OnPageIndexChanging="gvPO_PageIndexChanging" AllowPaging="true" PageSize="20"
                AllowSorting="true" OnSorting="gvPO_Sorting" >
                <RowStyle BackColor="Gainsboro" />
                <AlternatingRowStyle BackColor="white" />
                <HeaderStyle  CssClass="buttongrid" ForeColor="white"/>
                <FooterStyle CssClass="white"  />
                <PagerStyle ForeColor="#636363" CssClass="copy10grey" HorizontalAlign="Left" />
                <Columns>
                     
                    <asp:TemplateField HeaderText="S.No." ItemStyle-CssClass="copy10grey"  ItemStyle-Width="1%" HeaderStyle-CssClass="buttongrid">
                    <ItemTemplate>

                            <%# Container.DataItemIndex + 1%>
                  
                    </ItemTemplate>
                </asp:TemplateField> 
                    <asp:TemplateField HeaderText="Fulfillment#" HeaderStyle-CssClass="buttonundlinelabel" SortExpression="FulfillmentNumber" 
                        ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                        <ItemTemplate>
                            
                            <asp:LinkButton ID="lnkViewPO" ToolTip="View Fulfillment" runat="server" OnCommand="lnkViewPO_Command" CommandArgument='<%#Eval("POID")%>'>
                            <%#Eval("FulfillmentNumber")%></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                     <asp:TemplateField HeaderText="Fulfillment Date" HeaderStyle-CssClass="buttonundlinelabel" SortExpression="PODate" 
                         ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                        <ItemTemplate><%# Convert.ToDateTime(Eval("PODate")).ToString("MM/dd/yyyy") %></ItemTemplate>
                    </asp:TemplateField>

                     <asp:TemplateField HeaderText="Provisioning Date" HeaderStyle-CssClass="buttonundlinelabel" SortExpression="ProvisioningDate" 
                         ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                        <ItemTemplate><%# Convert.ToDateTime(Eval("ProvisioningDate")).ToString("MM/dd/yyyy") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Requested Ship Date" HeaderStyle-CssClass="buttonundlinelabel" SortExpression="RequestedShipDate"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                        <ItemTemplate><%# Convert.ToDateTime(Eval("RequestedShipDate")).ToString("MM/dd/yyyy") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Ship Date" HeaderStyle-CssClass="buttonundlinelabel" SortExpression="ShipDate"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                        <ItemTemplate><%# Convert.ToDateTime(Eval("ShipDate")).ToString("MM/dd/yyyy") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Quantity" HeaderStyle-CssClass="buttonundlinelabel" SortExpression="Quantity"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                        <ItemTemplate><%#Eval("Quantity")%></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status" HeaderStyle-CssClass="buttonundlinelabel" SortExpression="POStatus"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                        <ItemTemplate><%# Eval("POStatus") %></ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Action" HeaderStyle-CssClass="buttongrid" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
                        <ItemTemplate>

                             <asp:ImageButton ID="imgPO"  ToolTip="Provisioning View" OnCommand="imgViewPO_OnCommand"  CausesValidation="false" 
                            CommandArgument='<%# Eval("POID") %>' ImageUrl="~/Images/view.png"  runat="server" />

<%--                            <asp:LinkButton ID="lnkPO" runat="server" OnCommand="lnkPO_Command" CommandArgument='<%#Eval("POID")%>'>
                            View
                            </asp:LinkButton>--%>

                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    
                </Columns>
            </asp:GridView>
  
                </td>
            </tr>
            
            </table>
        </ContentTemplate>
        <%--<Triggers>
            <asp:PostBackTrigger ControlID="btnSearch" />
            <asp:PostBackTrigger ControlID="btnCancel" />
            
        </Triggers>--%>
        </asp:UpdatePanel>
	
        <asp:UpdatePanel ID="upnlJsRunner" UpdateMode="Always" runat="server">
			<ContentTemplate>
				<asp:PlaceHolder ID="phrJsRunner" runat="server"></asp:PlaceHolder>
			</ContentTemplate>
		</asp:UpdatePanel>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server"
                     DynamicLayout="false">
            <ProgressTemplate>
                <img src="/Images/ajax-loaders.gif" /> Loading ...
            </ProgressTemplate>
        </asp:UpdateProgress>
                </td>
    </tr>

    </table>
        <br /> <br />
            <br /> <br />
        <table width="100%">
        <tr>
		    <td>
			    <foot:MenuFooter id="Menuheader2" runat="server"></foot:MenuFooter>
		    </td>
	    </tr>
        </table>
    </form>
</body>
</html>
