<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ForecastQuery.aspx.cs" Inherits="avii.ForecastQuery" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="/Controls/Footer.ascx" %>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="/Controls/Header.ascx" %>
<%@ Register TagPrefix="UC" TagName="History" Src="/Controls/ForecastHistory.ascx" %>
<%@ Register TagPrefix="UC" TagName="ForecastDetail" Src="/Controls/ucForecastDetail.ascx" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>LAN Global Inc. - Forecast Query</title>
    <link href="../aerostyle.css" type="text/css" rel="stylesheet" />
    <link rel="stylesheet"  type="text/css" href="../fullfillment/calendar/dhtmlgoodies_calendar.css" media="screen" />
    <script type="text/javascript" src="../fullfillment/calendar/dhtmlgoodies_calendar.js"></script>
    
    <!-- from http://encosia.com/2009/10/11/do-you-know-about-this-undocumented-google-cdn-feature/ -->
    <link href="https://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/themes/start/jquery-ui.css" type="text/css" rel="Stylesheet" />
	
	<script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.4.0/jquery.min.js"></script>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/jquery-ui.min.js"></script>
<%--
    <script type="text/javascript" src="/JQuery/jquery.min.js"></script>
    <script type="text/javascript" src="/JQuery/jquery-ui.min.js"></script>
	--%>
	<script type="text/javascript" src="/JQuery/jquery.blockUI.js"></script>


<!-- fix for 1.1em default font size in Jquery UI -->
	<style type="text/css">
		.ui-widget{font-size:11px !important;}
		.ui-state-error-text{margin-left: 10px}
	</style>
    <script type="text/javascript">
        $(document).ready(function () {
            
            $("#divForecast").dialog({
                autoOpen: false,
                modal: true,
                minHeight: 300,
                height: 350,
                width: 650,
                resizable: false,
                open: function (event, ui) {
                    $(this).parent().appendTo("#divContainer");
                },
            });

            $("#divHistory").dialog({
                autoOpen: false,
                modal: true,
                minHeight: 300,
                height: 400,
                width: 650,
                resizable: false,
                open: function (event, ui) {
                    $(this).parent().appendTo("#divContainer");
                },
            });

        });


        function closeDialog() {
            //Could cause an infinite loop because of "on close handling"
            $("#divHistory").dialog('close');
        }

        function openDialog(title, linkID) {

            var pos = $("#" + linkID).position();
            var top = pos.top;
            var left = pos.left + $("#" + linkID).width() + 10;
            //alert(top);
            //top = top - 300;
            if (top > 600)
                top = 10;

            top = 100;
            //alert(top);
            left = 400;
            $("#divHistory").dialog("option", "title", title);
            $("#divHistory").dialog("option", "position", [left, top]);

            $("#divHistory").dialog('open');

        }


        function openDialogAndBlock(title, linkID) {

            openDialog(title, linkID);
            //alert('2')
            //block it to clean out the data
            $("#divHistory").block({
                message: '<img src="../images/async.gif" />',
                css: { border: '0px' },
                fadeIn: 0,
                //fadeOut: 0,
                overlayCSS: { backgroundColor: '#ffffff', opacity: 1 }
            });
        }

        function unblockDialog() {
            $("#divHistory").unblock();
        }

        
        function closeDialog() {
            //Could cause an infinite loop because of "on close handling"
            $("#divForecast").dialog('close');
        }

        function openForecastDialog(title, linkID) {

            var pos = $("#" + linkID).position();
            var top = pos.top;
            var left = pos.left + $("#" + linkID).width() + 10;
            //alert(top);
            //top = top - 300;
            if (top > 600)
                top = 10;

            top = 100;
            //alert(top);
            left = 400;
            $("#divForecast").dialog("option", "title", title);
            $("#divForecast").dialog("option", "position", [left, top]);

            $("#divForecast").dialog('open');

        }


        function openForecastDialogAndBlock(title, linkID) {

            openForecastDialog(title, linkID);
            //alert('2')
            //block it to clean out the data
            $("#divForecast").block({
                message: '<img src="../images/async.gif" />',
                css: { border: '0px' },
                fadeIn: 0,
                //fadeOut: 0,
                overlayCSS: { backgroundColor: '#ffffff', opacity: 1 }
            });
        }

        function unblockForecastDialog() {
            $("#divForecast").unblock();
        }

        

	</script>
    



    <script type="text/javascript">
        function set_focus1() {
            var img = document.getElementById("img1");
            var st = document.getElementById("btnSearch");
            st.focus();
            img.click();
        }
        function set_focus2() {
            var img = document.getElementById("img2");
            var st = document.getElementById("btnSearch");
            st.focus();
            img.click();
        }

    </script>
</head>
<body bgColor="#ffffff" leftmargin="0" rightmargin="0" topmargin="0">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

        
	<table cellspacing="0" cellpadding="0"  border="0" align="center" width="100%">
	<tr>
		<td>
			<head:menuheader id="HeadAdmin" runat="server"></head:menuheader>
		</td>
	 </tr>
     </table>
    <br />
    
    <table cellspacing="0" cellpadding="0"  border="0" align="center" width="95%">
    <tr>
    <td>
    <table  cellspacing="1" cellpadding="1" width="100%">
    <tr>
		<td colspan="6" bgcolor="#dee7f6" class="buttonlabel">&nbsp;&nbsp; Forecast Query
		</td>
    </tr>
    </table>   
    <br />
    <div id="divContainer">	
    <div id="divForecast" style="display:none">
					
			<asp:UpdatePanel ID="upForecast" runat="server">
				<ContentTemplate>
                   
					<asp:PlaceHolder ID="phForecast" runat="server">

                        <table bordercolor="#839abf" border="1" width="98%" align="center" cellpadding="0" cellspacing="0">
                                <tr><td>

                                <table border="0" width="100%" class="box" align="center" cellpadding="5" cellspacing="5">
                                <tr valign="top">
                                    <td>
                                    <strong> Forecast#: </strong>&nbsp;&nbsp;&nbsp;<asp:Label ID="lblFcNum" runat="server" CssClass="copy10grey"></asp:Label>
                                    </td>
                                </tr>
                                </table>
                                </td>
                                </tr>
                        </table>
                        <br />
                        <asp:Panel ID="plnForecast" runat="server">
                            <UC:ForecastDetail ID="forecastDetail1" runat="server" />
                        </asp:Panel>

                        </asp:PlaceHolder>
						
						
					</ContentTemplate>
							
				</asp:UpdatePanel>



            
        </div>
        
        <div id="divHistory" style="display:none">
					
			<asp:UpdatePanel ID="upnlHistory" runat="server">
				<ContentTemplate>
                   
					<asp:PlaceHolder ID="phrHistory" runat="server">

                        <table bordercolor="#839abf" border="1" width="98%" align="center" cellpadding="0" cellspacing="0">
                                <tr><td>

                                <table border="0" width="100%" class="box" align="center" cellpadding="5" cellspacing="5">
                                <tr valign="top">
                                    <td>
                                    <strong> Forecast#: </strong>&nbsp;&nbsp;&nbsp;<asp:Label ID="lblForecastNum" runat="server" CssClass="copy10grey"></asp:Label>
                                    </td>
                                </tr>
                                </table>
                                </td>
                                </tr>
                        </table>
                        <br />
                        <asp:Panel ID="pnlHistory" runat="server">
                            <UC:History ID="h1" runat="server" />
                        </asp:Panel>

                        </asp:PlaceHolder>
						
						
					</ContentTemplate>
							
				</asp:UpdatePanel>



            
        </div>
            
	</div>
    <asp:UpdatePanel ID="upnlForecast" UpdateMode="Conditional" runat="server">
    <ContentTemplate>

     <table width="100%">
                         
        <tr><td align="left">
        <asp:Label ID="lblMsg" runat="server" CssClass="errormessage"></asp:Label></td></tr>
     </table>
       <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="100%" >
                    <tr bordercolor="#839abf">
                        <td>
                         <asp:Panel  ID="pnlSearch" runat="server"  DefaultButton="btnSearch" >
                            <table width="100%" bgColor="#ffffff" leftmargin="0" rightmargin="0" topmargin="0" border="0">
                            <tr height="8">
                                <td>
                               
                                </td>
                            </tr>
                            <tr>
                                <td align="left" class="copy10grey" width="15%">Forecast#:</td>
                                
                                <td align="left" width="35%">
                                    <asp:TextBox ID="txtForecastNumber" MaxLength="20" Width="70%"  runat="server" CssClass="copy10grey"></asp:TextBox>
                                    
                                    </td>
                                <td align="left" class="copy10grey" width="15%">Status:</td>
                                
                                <td width="35%">
                                    <asp:DropDownList ID="ddlStatus" runat="server" Width="70%" class="copy10grey" >
                                    <asp:ListItem Text="" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Pending" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Received" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Cancelled" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="Fulfilled" Value="4"></asp:ListItem>
                                    <asp:ListItem Text="OutOfOrder" Value="5"></asp:ListItem>
                                    <asp:ListItem Text="Partial Fulfilled" Value="6"></asp:ListItem>
                                    <asp:ListItem Text="Deleted" Value="7"></asp:ListItem>
                                    </asp:DropDownList>
                                        
                                </td>
                            </tr>
                            
                            <tr>
                                <td align="left" class="copy10grey" width="15%">Forecast Date from:</td>
                                
                                <td align="left" width="35%">
                                    <asp:TextBox ID="txtFcDateFrom" runat="server" onfocus="set_focus1();"  CssClass="copy10grey" MaxLength="15"  Width="70%"></asp:TextBox>
                                    <img id="img1" alt="" onclick="document.getElementById('<%=txtFcDateFrom.ClientID%>').value = ''; displayCalendar(document.getElementById('<%=txtFcDateFrom.ClientID%>'),'mm/dd/yyyy',this,true);" src="../fullfillment/calendar/sscalendar.jpg" />
                                    
                                    </td>
                                <td align="left" class="copy10grey" width="15%">Forecast Date to:</td>
                                
                                <td width="35%">
                                    <asp:TextBox ID="txtFcDateTo" runat="server" onfocus="set_focus2();"  CssClass="copy10grey" MaxLength="15"  Width="70%"></asp:TextBox>
                                    <img id="img2" alt="" onclick="document.getElementById('<%=txtFcDateTo.ClientID%>').value = ''; displayCalendar(document.getElementById('<%=txtFcDateTo.ClientID%>'),'mm/dd/yyyy',this,true);" src="../fullfillment/calendar/sscalendar.jpg" />
                                    
                                </td>
                            </tr>
                            <tr>
                                <td align="left" class="copy10grey" width="15%">SKU#:</td>
                                
                                <td align="left" width="35%">
                                    <asp:TextBox ID="txtSKU" MaxLength="20" runat="server" CssClass="copy10grey" Width="70%" ></asp:TextBox>
                                    
                                    </td>
                                <td align="left" class="copy10grey" width="15%">
                                    <asp:Label ID="lblCname" runat="server" Text="Customer Name:" CssClass="copy10grey"  ></asp:Label>
                                
                                </td>
                                
                                <td width="35%">
                                    <asp:DropDownList ID="dpCompany" runat="server" Width="70%" CssClass="copy10grey"  >
                                    </asp:DropDownList>
                                    
                                </td>
                            </tr>
                            
                            <tr>
                                <td colspan="4">
                                <hr />
                                </td>
                                </tr>
                                <tr>
                                <td  align="center"  colspan="5">
                                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="button"  OnClick="btnSearch_Click" CausesValidation="false"/>
                                     &nbsp;
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="button" OnClick="btnCancel_Click" CausesValidation="false"/>
            
        
                                </td>
                                </tr>
                            </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    </table>

                    
            <table align="center" style="text-align:left" width="100%">
                             <tr>
                                <td  align="left" style="height:8px; vertical-align:bottom">
                                    
                                    
                                </td>
                                <b></b>

                                <td  align="right" style="height:8px; vertical-align:bottom">
                                
                                <strong>   
                                    <asp:Label ID="lblCount" CssClass="copy10grey" runat="server" ></asp:Label> &nbsp;&nbsp;
                                </strong> 
                                </td>
                             </tr>

                             <tr>
                                <td colspan="2" align="center">
                                    <asp:GridView ID="gvForecast" runat="server" Width="100%" GridLines="Both"   AutoGenerateColumns="false"
                                    OnPageIndexChanging="gridView_PageIndexChanging" PageSize="50" AllowPaging="true" 
                                    OnRowDataBound="gvForecast_RowDataBound" ShowFooter="false" DataKeyNames="ForecastID" >
                                        <RowStyle BackColor="Gainsboro" />
                                        <AlternatingRowStyle BackColor="white" />
                                        <HeaderStyle  CssClass="buttonlabel" ForeColor="white"/>
                                        <FooterStyle CssClass="white"  />
                                        <PagerStyle ForeColor="#636363" CssClass="copy10grey" HorizontalAlign="Left" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No." ItemStyle-HorizontalAlign="Right"  ItemStyle-CssClass="copy10grey" 
                                            ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                <%# Container.DataItemIndex +  1 %>
                                                
                                                </ItemTemplate>
                                                
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Forecast date" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="15%">
                                                <ItemTemplate>
                                                
                                                <%--<%#Eval("SHIPDATE")%>--%>
                                                <%# Convert.ToDateTime(Eval("ForecastDATE")).ToShortDateString()%>
                                                
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField HeaderText="Forecast#" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="15%">
                                                <ItemTemplate><%#Eval("ForecastNUMBER")%></ItemTemplate>
                                            </asp:TemplateField>
                                            
                                            
                                            <asp:TemplateField HeaderText="CustomerName" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="20%">
                                                <ItemTemplate><%#Eval("CustomerName")%></ItemTemplate>
                                            </asp:TemplateField>
                                            
                                             
                                            
                                            <asp:TemplateField HeaderText="Status" ItemStyle-HorizontalAlign="Left" FooterStyle-HorizontalAlign="Right"  ItemStyle-CssClass="copy10grey" FooterStyle-CssClass="copy10grey" ItemStyle-Width="8%">
                                                <ItemTemplate>
                                                <%#Eval("Status")%>
                                                </ItemTemplate>
                                                
                                            </asp:TemplateField> 
                                                        
                                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center"   ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
                                                <ItemTemplate>
                                                <table>
                                                <tr>
                                                    <td>
                                                        <asp:ImageButton ID="imgView"  ToolTip="View Forecast line items" OnCommand="imgViewFC_OnCommand"  CausesValidation="false" 
                                                CommandArgument='<%# Eval("ForecastID") + "," + Eval("ForecastNUMBER") %>' ImageUrl="~/Images/view.png"  runat="server" />
                            
                                                    </td>
                                                    <%--<td>
                                                        <asp:ImageButton ID="imgHistory"  ToolTip="Forecast History" OnCommand="imgFCHistory_OnCommand"  CausesValidation="false" 
                                                CommandArgument='<%# Eval("ForecastID") + "," + Eval("ForecastNUMBER") %>' ImageUrl="~/Images/history.png"  runat="server" />
                        
                                                    </td>--%>
                                                    <td>
                                                        <a   href="CreateForecast.aspx?fid=<%# Eval("ForecastID") %>">
                                                            <img id="imgEdit" src="../images/edit.png" alt="Edit forecast" style="border:0"/>
                                                        </a>
                                                
                                                    </td>
                                                    <td>
                                                        <asp:ImageButton ID="imgDelete"  ToolTip="Forecast Delete" OnCommand="imgDelete_OnCommand"  CausesValidation="false" 
                                                CommandArgument='<%# Eval("ForecastID")%>' ImageUrl="~/Images/delete.png"  runat="server" OnClientClick="return confirm('Do you want to delete?');" />
                        
                                                    </td>
                                                </tr>
                                                </table>
                                                
                                                
                                                </ItemTemplate>
                                                
                                            </asp:TemplateField> 
                                        </Columns>
                                    </asp:GridView>
                                    

                                </td>
                            </tr>
                            </table>
   
                  

   
    </ContentTemplate>
    </asp:UpdatePanel>

    </td>
    </tr>
    </table>
    <asp:UpdatePanel ID="upnlJsRunner" UpdateMode="Always" runat="server">
<ContentTemplate>
	<asp:PlaceHolder ID="phrJsRunner" runat="server"></asp:PlaceHolder>
</ContentTemplate>
</asp:UpdatePanel>
<asp:UpdateProgress ID="UpdateProgress1" runat="server" DynamicLayout="false">
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
    

<script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.4.0/jquery.min.js"></script>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/jquery-ui.min.js"></script>
        <script type="text/javascript" src="../JQuery/jquery.blockUI.js"></script>
    </form>
</body>
</html>
