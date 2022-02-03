<%@ Page Language="C#" AutoEventWireup="true" Trace="false" CodeBehind="NewRmaQuery.aspx.cs" Inherits="avii.RMA.NewRmaQuery"  %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>
<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
--%>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="/Controls/Header.ascx" %>
<%@ Register TagPrefix="UC" TagName="Comments" Src="~/Controls/ForecastComment.ascx" %>
<%--
<%@ Register TagPrefix="RMA" TagName="ViewRMA" Src="~/Controls/ViewRMA.ascx" %>--%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
    
<head id="Head1" runat="server">
    <title>Return Merchandise Authorization (RMA) - Search</title>
    <!-- from http://encosia.com/2009/10/11/do-you-know-about-this-undocumented-google-cdn-feature/ -->
     <link href="https://ajax.googleapis.com/ajax/libs/jqueryui/1.8.11/themes/start/jquery-ui.css" type="text/css" rel="Stylesheet" />
	<%--
	<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.4.0/jquery.min.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/jquery-ui.min.js"></script>--%>
    <script type="text/javascript" src="/JQuery/jquery.min.js"></script>
    <script type="text/javascript" src="/JQuery/jquery-ui.min.js"></script>
	
	<script type="text/javascript" src="/JQuery/jquery.blockUI.js"></script>
		
<!-- fix for 1.1em default font size in Jquery UI -->
	<style type="text/css">
		.ui-widget{font-size:11px !important;}
		.ui-state-error-text{margin-left: 10px}
	</style>
    
	<script type="text/javascript">
		$(document).ready(function() {


        $("#divComments").dialog({
	            autoOpen: false,
	            modal: false,
	            minHeight: 20,
	            height: 260,
	            width: 450,
	            resizable: false,

	            open: function (event, ui) {
	                $(this).parent().appendTo("#divEditCustomerDlgContainer");
	            },

	        });

			$("#divEditCustomer").dialog({
				autoOpen: false,
				modal: true,
				minHeight: 20,
				height: 400,
				width: 750,
				resizable: false,
				open: function(event, ui) {
					$(this).parent().appendTo("#divEditCustomerDlgContainer");
				},
			});
            $("#divHistory").dialog({
				autoOpen: false,
				modal: true,
				minHeight: 20,
				height: 350,
				width: 600,
				resizable: false,
				open: function(event, ui) {
					$(this).parent().appendTo("#divEditCustomerDlgContainer");
				},
			});
		});


        function closeCommentsDialog() {
	        //Could cause an infinite loop because of "on close handling"
	        $("#divComments").dialog('close');
	    }


	    function openCommentsDialog(title, linkID) {

	        var pos = $("#" + linkID).position();
	        var top = pos.top;
	        var left = pos.left + $("#" + linkID).width() + 10;
	        //alert(top);
	        if (top > 600)
	            top = 10;
	        else
	            top = 10;
	        //top = top - 600;
	        left = 425;
	        $("#divComments").dialog("option", "title", title);
	        $("#divComments").dialog("option", "position", [left, top]);

	        $("#divComments").dialog('open');

	        unblockCommentsDialog();
	    }
	    function openCommentsDialogAndBlock(title, linkID) {
	        openCommentsDialog(title, linkID);

	        //block it to clean out the data
	        $("#divComments").block({
	            message: '<img src="../images/async.gif" />',
	            css: { border: '0px' },
	            fadeIn: 0,
	            //fadeOut: 0,
	            overlayCSS: { backgroundColor: '#ffffff', opacity: 1 }
	        });
	    }
	    function unblockCommentsDialog() {
	        $("#divComments").unblock();
	    }


		function closeDialog() {
			//Could cause an infinite loop because of "on close handling"
			$("#divEditCustomer").dialog('close');
		}
        function closeHistoryDialog() {
			//Could cause an infinite loop because of "on close handling"
			$("#divHistory").dialog('close');
		}
		
		function openDialog(title, linkID) {
		
			var pos = $("#" + linkID).position();
			var top = 0;

top = pos.top;			var left = pos.left + $("#" + linkID).width() + 10;
			//alert(left);
            //top = top - 400;
if(top > 400)
	top = 10;
			left = 275;
			$("#divEditCustomer").dialog("option", "title", title);
			$("#divEditCustomer").dialog("option", "position", [left, top]);
			
			$("#divEditCustomer").dialog('open');
		}

        function openHistoryDialog(title, linkID) {
		
			var pos = $("#" + linkID).position();
			var top = pos.top;
			var left = pos.left + $("#" + linkID).width() + 10;
			//alert(left);
            //top = top - 300;
if(top > 400)
	top = 10;

			left = 300;
            //$("#divHistory").html("");
            //$('#divHistory').empty();
            //alert($('#phrHistory'));
			$("#divHistory").dialog("option", "title", title);
			$("#divHistory").dialog("option", "position", [left, top]);
			
			$("#divHistory").dialog('open');
		}



		function openDialogAndBlock(title, linkID) {
			openDialog(title, linkID);

			//block it to clean out the data
			$("#divEditCustomer").block({
				message: '<img src="../images/async.gif" />',
				css: { border: '0px' },
				fadeIn: 0,
				//fadeOut: 0,
				overlayCSS: { backgroundColor: '#ffffff', opacity: 1 } 
			});
		}
        function openHistoryDialogAndBlock(title, linkID) {
			openHistoryDialog(title, linkID);

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
			$("#divEditCustomer").unblock();
		}
        function unblockHistoryDialog() {
			$("#divHistory").unblock();
		}

		function onTest() {
			$("#divEditCustomer").block({
				message: '<h1>Processing</h1>',
				css: { border: '3px solid #a00' },
				overlayCSS: { backgroundColor: '#ffffff', opacity: 1 } 
			});
		}
$(function () {
	        $('#txtESNSearch').keyup(function () {
	            if (this.value.match(/[^a-zA-Z0-9 ]/g)) {
	                this.value = this.value.replace(/[^a-zA-Z0-9 ]/g, '');
	            }
	        });
	    });
        $(function () {
	        $('#txtPONum').keyup(function () {
	            if (this.value.match(/[^a-zA-Z0-9 ]/g)) {
	                this.value = this.value.replace(/[^a-zA-Z0-9 ]/g, '');
	            }
	        });
	    });
        $(function () {
	        $('#rmanumber').keyup(function () {
	            if (this.value.match(/[^a-zA-Z0-9 ]/g)) {
	                this.value = this.value.replace(/[^a-zA-Z0-9 ]/g, '');
	            }
	        });
	    });
        $(function () {
	        $('#txtRMADate').keyup(function () {
	            if (this.value.match(/[^a-zA-Z0-9 ]/g)) {
	                this.value = this.value.replace(/[^a-zA-Z0-9 ]/g, '');
	            }
	        });
	    });
        $(function () {
	        $('#txtRMADateTo').keyup(function () {
	            if (this.value.match(/[^a-zA-Z0-9 ]/g)) {
	                this.value = this.value.replace(/[^a-zA-Z0-9 ]/g, '');
	            }
	        });
	    });
        $(function () {
	        $('#txtUPC').keyup(function () {
	            if (this.value.match(/[^a-zA-Z0-9 ]/g)) {
	                this.value = this.value.replace(/[^a-zA-Z0-9 ]/g, '');
	            }
	        });
	    });
        $(function () {
	        $('#txtAVSO').keyup(function () {
	            if (this.value.match(/[^a-zA-Z0-9 ]/g)) {
	                this.value = this.value.replace(/[^a-zA-Z0-9 ]/g, '');
	            }
	        });
	    });
	</script>
	
    <link href="/aerostyle.css" type="text/css" rel="stylesheet" />
    
     <link rel="stylesheet" type="text/css" href="/fullfillment/calendar/dhtmlgoodies_calendar.css" media="screen"></link>
    <script type="text/javascript" src="/fullfillment/calendar/dhtmlgoodies_calendar.js"></script>

    <script type="text/javascript">
        function doReadonly(evt) {

            evt.keyCode = 0;
            return false;
        }
        function set_focus() {
            var img = document.getElementById("img1");
            var st = document.getElementById("ddlStatus");
            st.focus();
            img.click();
        }
        function set_focus1() {
            var img = document.getElementById("img1");
            var st = document.getElementById("ddlStatus");
            st.focus();
            img.click();
        }

        function set_focus2() {
            var img = document.getElementById("img2");
            var st = document.getElementById("ddlStatus");
            st.focus();
            img.click();
        }

        

        function ValidateSearch() {
            var rmaDate = document.getElementById("<%= txtRMADate.ClientID %>").value;
            var rmaDateTo = document.getElementById("<%= txtRMADateTo.ClientID %>").value;
            var rmaESN = document.getElementById("<%= txtESNSearch.ClientID %>").value;
            var rmanumber = document.getElementById("<%= rmanumber.ClientID %>").value;
            var poNum = document.getElementById("<%= txtPONum.ClientID %>").value;
            var upc = document.getElementById("<%= txtUPC.ClientID %>").value;
            var avso = document.getElementById("<%= txtAVSO.ClientID %>").value;

            document.getElementById("<%= txtRMADate.ClientID %>").value = rmaDate.replace("'", "");
            document.getElementById("<%= txtRMADateTo.ClientID %>").value = rmaDateTo.replace("'", "");
            document.getElementById("<%= txtESNSearch.ClientID %>").value = rmaESN.replace("'", "");
            document.getElementById("<%= rmanumber.ClientID %>").value = rmanumber.replace("'", "");
            document.getElementById("<%= txtPONum.ClientID %>").value = poNum.replace("'", "");
            document.getElementById("<%= txtUPC.ClientID %>").value = upc.replace("'", "");
            document.getElementById("<%= txtAVSO.ClientID %>").value = avso.replace("'", "");
            //alert(rmaESN.replace("'", ""))


        }
    </script>

<link rel="stylesheet" type="text/css" href="/dhtmlxwindow/dhtmlxwindows.css"/>
	    <link rel="stylesheet" type="text/css" href="/dhtmlxwindow/skins/dhtmlxwindows_dhx_skyblue.css"/>
	    <script src="/dhtmlxwindow/dhtmlxcommon.js" type="text/javascript"></script>
	    <script src="/dhtmlxwindow/dhtmlxwindows.js" type="text/javascript"></script>
	    <script src="/dhtmlxwindow/dhtmlxcontainer.js" type="text/javascript"></script>

        <script type="text/javascript" language="javascript">
            var dhxWins, w1;
            function RmaDownload(rmaGUID) {

                dhxWins = new dhtmlXWindows();
                dhxWins.enableAutoViewport(false);
                dhxWins.attachViewportTo("winVP");
                dhxWins.setImagePath("../../codebase/imgs/");

                var topPos = 10; // document.documentElement.scrollTop;


                w1 = dhxWins.createWindow("w1", 225, topPos, 625, 350);
                w1.setText("RMA Document");
                w1.attachURL("rmadownload.aspx?rmaguid=" + rmaGUID);

            }
        </script>
		
    
</head>
<body bgcolor="#ffffff" leftmargin="0" rightmargin="0" topmargin="0">
    <form id="form1" runat="server">
      <table cellspacing="0" cellpadding="0" border="0" align="center" width="100%">
        <tr>
            <td>
                <head:MenuHeader ID="MenuHeader1" runat="server"/>
            </td>
        </tr>
        </table>
        <asp:ScriptManager ID="scriptManager" runat="server" EnablePartialRendering="true" />
        
		<div id="divEditCustomerDlgContainer">	
			<div id="divEditCustomer" style="display:none">
					
				<asp:UpdatePanel ID="upnlViewRMA" runat="server">
					<ContentTemplate>
                   
						<asp:PlaceHolder ID="phrViewRMA" runat="server">
		                    
                        <table width="100%">
            <tr>
                 <td>
                
                <div style="overflow:auto; height:350px; width:100%; border: 0px solid #839abf" >
      
<table width="98%" align="center" cellpadding="0" cellspacing="0">
<tr>
    <td>
    <asp:Label ID="lblMsgDetail" runat="server" CssClass="errormessage"></asp:Label>
        <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="100%" align="center" >
           <tr bordercolor="#839abf">
                <td>
                <table width="100%" cellSpacing="3" cellPadding="3">
                <tr>
                    <td class="copy10grey" width="25%" align="right">
                        RMA#:
                    </td>
                    <td width="1%">&nbsp;</td>
                    <td width="24%" align="left">
                        <asp:Label ID="lblRMA" CssClass="copy10grey" runat="server" EnableViewState="false" ></asp:Label>
                    </td>
                    <td class="copy10grey" width="15%" align="right">
                        RMA Date:
                    </td>
                    <td width="1%">&nbsp;</td>
                    <td width="33%" align="left">
                        <asp:Label ID="lblRMADate" CssClass="copy10grey" runat="server" EnableViewState="false"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="copy10grey" width="25%" align="right">
                        Status:
                    </td>
                    <td width="1%">&nbsp;</td>
                    <td width="24%" align="left">
                        <asp:Label ID="lblStatus" CssClass="copy10grey" runat="server" EnableViewState="false" ></asp:Label>
                    </td>
                    <td class="copy10grey" width="15%" align="right">
                        Company Name:
                    </td>
                    <td width="1%">&nbsp;</td>
                    <td width="33%" align="left">
                        <asp:Label ID="lblCompanyName" CssClass="copy10grey" runat="server" EnableViewState="false"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="copy10grey" width="25%" align="right">
                    Customer Comment:
                    </td>
                    <td width="1%">&nbsp;</td>
                    <td  align="left" colspan="5">
                        <asp:Label ID="lblComment" CssClass="copy10grey" runat="server" EnableViewState="false"></asp:Label>
                    </td>
                </tr>   
                <tr>
                    <td class="copy10grey" width="25%" align="right">
                    Admin Comment:
                    </td>
                    <td width="1%">&nbsp;</td>
                    <td  colspan="5" align="left">
                        <asp:Label ID="lblAVComment" CssClass="copy10grey" runat="server" EnableViewState="false"></asp:Label>
                    </td>
                </tr>   
                </table>
                </td>
            </tr>
        </table>
    </td>
</tr>
<tr>
<td>&nbsp;</td>
</tr>
<tr>
    <td>
        <asp:GridView ID="gvRMADetail"  BackColor="White" Width="100%" Visible="true" 
        AutoGenerateColumns="false" Font-Names="Verdana" runat="server" DataKeyNames="rmaguID"
        GridLines="Both"  OnRowDataBound="gvRmaDetail_RowDataBound" 
        BorderStyle="Double" BorderColor="#0083C1">
        <RowStyle BackColor="Gainsboro" />
        <AlternatingRowStyle BackColor="white" />
        <HeaderStyle  CssClass="button" ForeColor="white"/>
        <FooterStyle CssClass="white"  />
        <Columns>
            <asp:TemplateField HeaderText="ESN#" SortExpression="Item_Code" HeaderStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey"  ItemStyle-Width="20%">
                <ItemTemplate><%# Eval("ESN")%></ItemTemplate>
            </asp:TemplateField>
                                                                                                                                    
                                            
           <%-- <asp:TemplateField HeaderText="UPC"   ItemStyle-CssClass="copy10grey" ItemStyle-Width="15%">
                <ItemTemplate><%#Eval("UPC")%></ItemTemplate>
            </asp:TemplateField> --%>
             <asp:TemplateField HeaderText="SKU#"   ItemStyle-CssClass="copy10grey" ItemStyle-Width="15%">
                <ItemTemplate><%#Eval("ItemCode")%></ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="NEW SKU#"   ItemStyle-CssClass="copy10grey" ItemStyle-Width="15%">
                <ItemTemplate><%#Eval("NEWSKU")%></ItemTemplate>
            </asp:TemplateField>
                                          
            <asp:TemplateField HeaderText="CallTime" ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                <ItemTemplate>
                    <%# Eval("CallTime") %>
                </ItemTemplate>
                                                
            </asp:TemplateField>
                                            
            <asp:TemplateField HeaderText="Reason" SortExpression="Reason"  ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                <ItemTemplate><%--
                <asp:HiddenField ID="hdnReason" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "reason")%>' />
                                                <asp:Label ID="lblreason" runat="server" ></asp:Label>--%>
                                                
                                                <%#Eval("ReasonTxt")%>
                </ItemTemplate>
                                                               
            </asp:TemplateField>
                                        
            <asp:TemplateField HeaderText="Status" SortExpression="Status"  ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                <ItemTemplate><%#Eval("Status")%></ItemTemplate>
                                                                                                 
            </asp:TemplateField>
                                            
    <asp:TemplateField ItemStyle-Width="2%">
                <ItemTemplate>
                        <asp:ImageButton ToolTip="Delete RMA Item"   CommandArgument='<%# Eval("rmaDetGUID") %>'  OnClientClick="return confirm('Do you want to delete?');"
                            ImageUrl="~/Images/delete.png" ID="imgDelDetail1" OnCommand="imgDeleteRMADetail_Commnad" runat="server" />

              </ItemTemplate>
              </asp:TemplateField>                                                
			                                
        </Columns>
    </asp:GridView>
    </td>
</tr>
</table>

                    

                    
                </div>
               
                </td>
            </tr>
        </table>
                        </asp:PlaceHolder>
						
						
					</ContentTemplate>
							
				</asp:UpdatePanel>
			
			</div>
            <div id="divHistory" style="display:none">
					
				<asp:UpdatePanel ID="upHistory" runat="server" >
                
					<ContentTemplate>
                    
						<asp:PlaceHolder ID="phrHistory" runat="server">
                            
                            
<table width="100%">
<tr>
    <td >
<table bordercolor="#839abf" border="1" width="98%" align="center" cellpadding="0" cellspacing="0">
            <tr><td>

            <table border="0" width="100%" class="box" align="center" cellpadding="5" cellspacing="5">
            <tr valign="top">
                <td>
                <strong> RMA#: </strong>&nbsp;&nbsp;&nbsp;<asp:Label ID="lblRMANum" runat="server" CssClass="copy10grey"></asp:Label>
                </td>
            </tr>
            </table>
            </td>
            </tr>
    </table>
    <br />
    
    <table bordercolor="#839abf" border="1" width="98%" align="center" cellpadding="0" cellspacing="0">
    <tr><td>

    <table border="0" width="100%" class="box" align="center" cellpadding="0" cellspacing="0">
    <tr valign="top">
        <td>
            <asp:Label ID="lblHistory" runat="server" CssClass="errormessage"></asp:Label>
                            
            <asp:Repeater ID="rptRma" runat="server">
            <HeaderTemplate>
            <table width="100%" align="center">
                <tr>
                    <td class="button">
                    &nbsp;Status
                    </td>
                    <td class="button">
                        &nbsp;Last Modified Date
                    </td>
                    <td class="button">
                        &nbsp;Modified By
                    </td>

<td class="button">
                        &nbsp;Source
                    </td>                                    
                </tr>
                            
            </HeaderTemplate>
            <ItemTemplate>
            <tr>
                <td class="copy10grey">
                    <%-- <span title='<%# Eval("AVComments") %>'> &nbsp;<%# Eval("Status") %></span> --%>

<span > &nbsp;<%# Eval("Status") %></span>               
                </td>
                <td class="copy10grey">
                    &nbsp;<%# Eval("ModifiedDate")%>
                    <%--<%# DataBinder.Eval(Container.DataItem, "ModifiedDate", "{0:MM/dd/yyyy}")%>--%>
                </td>
                <td class="copy10grey">
                        &nbsp;<%# Eval("RmaContactName") %>
                </td>
<td class="copy10grey">
                        &nbsp;<%# Eval("PoNum")%>
                </td>
                                
            </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>    
            </FooterTemplate>
            </asp:Repeater>
                        
        </td>
    </tr>
    </table>
    </td></tr>
    </table>
    </td>
</tr>
</table>
                        </asp:PlaceHolder>
						
						
					</ContentTemplate>
							
				</asp:UpdatePanel>
			
			</div>

             <div id="divComments" style="display:none">
					
				<asp:UpdatePanel ID="upComments" runat="server">
					<ContentTemplate>
                   
						<asp:PlaceHolder ID="phComments" runat="server">
                            
                        <table bordercolor="#839abf" border="1" width="98%" align="center" cellpadding="0" cellspacing="0">
                                <tr><td>

                                <table border="0" width="100%" class="box" align="center" cellpadding="5" cellspacing="5">
                                <tr valign="top">
                                    <td>
                                    <strong> RMA#: </strong>&nbsp;&nbsp;&nbsp;<asp:Label ID="lblRNum" runat="server" CssClass="copy10grey"></asp:Label>
                                    </td>
                                </tr>
                                </table>
                                </td>
                                </tr>
                        </table>
                        <br />
                        <asp:Panel ID="pnlComments" runat="server">
                        
                            <%--<table bordercolor="#839abf" border="1" width="98%" align="center" cellpadding="0" cellspacing="0">
                                    <tr><td>

                                    <table border="0" width="100%" class="box" align="center" cellpadding="5" cellspacing="5">
                                    <tr valign="top">
                                        <td>--%>
                                            <UC:Comments ID="c1" runat="server" />
                                        <%--</td>
                                    </tr>
                                    </table>
                                    </td>
                                    </tr>
                            </table>--%>
                            </asp:Panel>
                    </asp:PlaceHolder>
						
						
					</ContentTemplate>
							
				</asp:UpdatePanel>
        </div>
       	
		    
		</div>	<!-- divEditCustomerDlgContainer -->
<div id="winVP">
        <asp:UpdatePanel ID="upnlCustomers" UpdateMode="Conditional" runat="server">
			<ContentTemplate>
		
        <%--<asp:LinkButton ID="btnRefreshGrid" CausesValidation="false" OnClick="btnRefreshGrid_Click" style="display:none" runat="server"></asp:LinkButton>		--%>
		<table width="95%" align="center" cellspacing="0" cellpadding="0"> 
        <tr>
            <td>
                <table style="text-align: left; width:100%;" align="center" class="copy10grey">
                    <tr>
                        <td>
                            &nbsp;<input type="hidden" ID="hdncount" />
                        </td>
                    </tr>
                    <tr>
                        <td class="buttonlabel" align="left">
                            &nbsp; Return Merchandise Authorization (RMA) - Search
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblMsg" runat="server" CssClass="errormessage"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table bordercolor="#839abf" border="1" cellspacing="0" cellpadding="0" width="100%" align="center">
                    <tr bordercolor="#839abf">
                        <td>
                            <table style="text-align: left; width: 100%;" cellspacing="0" cellpadding="0" align="center" class="copy10grey">
                                <tr>
                                    <td>
                                        <table class="box" width="100%" align="center" cellspacing="2" cellpadding="2">
                                            <tr>
                                                <td >
                                                    <asp:Label ID="lblComapny" CssClass="copy10grey" runat="server" Text="Customer:"></asp:Label>
                                                    </td>
                                                        <asp:HiddenField ID="hdnrmaGUIDs" runat="server" />
                                                        <asp:HiddenField ID="hdncompany" runat="server" />
                                                        <asp:HiddenField ID="hdnCompanyId" runat="server" />
                                                        <asp:HiddenField ID="hdnUserID" runat="server" />
                                                <td>
                                                    <asp:Panel ID="companyPanel" runat="server">
                                                        <asp:DropDownList ID="ddlCompany" CssClass="copy10grey"
                                                            runat="server">
                                                        </asp:DropDownList>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="copy10grey" width="15%">
                                                    RMA#:
                                                </td>
                                                <td width="35%">
                                                    <asp:TextBox runat="server" ID="rmanumber" CssClass="copy10grey" Width="60%" />
                                                </td>
                                                 <td class="copy10grey" width="15%">
                                                    ESN:
                                                </td>
                                                <td  width="35%">
                                                    <asp:TextBox runat="server" ID="txtESNSearch" MaxLength="35" CssClass="copy10grey" Width="60%"  />
                                                </td>
                                               
                                            </tr>
                                            <tr>
                                                <td class="copy10grey">
                                                    RMA From Date:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtRMADate" runat="server" Width="60%" onfocus="set_focus1();" onkeypress="return doReadonly(event);"
                                                        CssClass="copy10grey" MaxLength="15" Text="" />
                                                    <img id="img1" alt="" onclick="document.getElementById('<%=txtRMADate.ClientID%>').value = ''; displayCalendar(document.getElementById('<%=txtRMADate.ClientID%>'),'mm/dd/yyyy',this,true);"
                                                        src="../../fullfillment/calendar/sscalendar.jpg" />
                                                    
                                               </td>
                                                <td class="copy10grey">
                                                    RMA To Date:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtRMADateTo" runat="server" Width="60%" onfocus="set_focus2();" onkeypress="return doReadonly(event);"
                                                        CssClass="copy10grey" MaxLength="15" Text="" />
                                                    <img id="img2" alt="" onclick="document.getElementById('<%=txtRMADateTo.ClientID%>').value = ''; displayCalendar(document.getElementById('<%=txtRMADateTo.ClientID%>'),'mm/dd/yyyy',this,true);"
                                                        src="../../fullfillment/calendar/sscalendar.jpg" />
                                                    
                                               </td>
                                            </tr>
                                            <tr valign="top">
                                                <td class="copy10grey">
                                                    Status:</td>
                                                <td>
                                                    <asp:DropDownList ID="ddlStatus" runat="server" Class="copy10grey" Width="60%" >
                                                        <asp:ListItem  Value="0" >------</asp:ListItem>
                                                        <asp:ListItem  Value="6">Approved</asp:ListItem>
                                                        <asp:ListItem  Value="12">Back to Stock -NDF</asp:ListItem>
                                                        <asp:ListItem  Value="14">Back to Stock – Replaced by OEM</asp:ListItem>
                                                        <asp:ListItem  Value="13">Back to Stock- Credited</asp:ListItem>
                                                        <asp:ListItem Value="30" >Cancel</asp:ListItem>
                                                        <asp:ListItem  Value="10">Closed</asp:ListItem>
                                                        <asp:ListItem  Value="8">Credited</asp:ListItem>
                                                        <asp:ListItem Value="26" >Damaged</asp:ListItem>
                                                        <asp:ListItem  Value="9">Denied</asp:ListItem>
                                                        <asp:ListItem Value="31" >External ESN</asp:ListItem>
                                                        <asp:ListItem Value="25" >Incomplete</asp:ListItem>
                                                        <asp:ListItem  Value="19">NDF (No Defect Found)</asp:ListItem>
                                                        <asp:ListItem  Value="11" >Out with OEM for repair</asp:ListItem>
                                                        <asp:ListItem Value="1" >Pending</asp:ListItem>
                                                        <asp:ListItem  Value="4">Pending for Credit</asp:ListItem>
                                                        <asp:ListItem  Value="3">Pending for Repair</asp:ListItem>
                                                        <asp:ListItem  Value="5">Pending for Replacement</asp:ListItem>
                                                        <asp:ListItem Value="32" >Pending ship to OEM</asp:ListItem>
                                                        <asp:ListItem Value="34" >Pending ship to Supplier</asp:ListItem>
                                                        <asp:ListItem  Value="20">PRE-OWNED – A stock</asp:ListItem>
                                                        <asp:ListItem Value="21" >PRE-OWEND - B Stock</asp:ListItem>
                                                        <asp:ListItem Value="22" >PRE-OWEND – C Stock</asp:ListItem>
                                                        <asp:ListItem Value="27" >Preowned</asp:ListItem>
                                                        <asp:ListItem  Value="2">Received</asp:ListItem>
                                                        <asp:ListItem Value="23" >Rejected</asp:ListItem>
                                                        <asp:ListItem  Value="18">Repaired By AV</asp:ListItem>
                                                        <asp:ListItem  Value="15">Repaired by OEM</asp:ListItem>
                                                        <asp:ListItem  Value="17">Replaced BY AV</asp:ListItem>
                                                        <asp:ListItem  Value="16">Replaced BY OEM</asp:ListItem>
<asp:ListItem  Value="38">Replaced by OEM- New</asp:ListItem>
                                                        <asp:ListItem  Value="39">Replaced by OEM- Preowned</asp:ListItem>
                                                        <asp:ListItem Value="28" >Return to OEM</asp:ListItem>
                                                        <asp:ListItem  Value="7">Returned</asp:ListItem>
                                                        <asp:ListItem Value="36" >Returned from OEM</asp:ListItem>
                                                        <asp:ListItem Value="29" >Returned to Stock</asp:ListItem>
                                                        <asp:ListItem Value="24" >RTS (Return To Stock)</asp:ListItem>
                                                        <asp:ListItem Value="33" >Sent to OEM</asp:ListItem>
                                                        <asp:ListItem Value="35" >Sent to Supplier</asp:ListItem>

                                                        </asp:DropDownList>
                                                </td>
                                                
                                                <td class="copy10grey">
                                                UPC:</td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="txtUPC" CssClass="copy10grey" Width="60%" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="copy10grey">AVSO:</td>
                                                <td><asp:TextBox runat="server" ID="txtAVSO"  MaxLength="30"  CssClass="copy10grey" Width="60%" /></td>
                                                <td class="copy10grey">Purchase Order#:</td>
                                                <td><asp:TextBox runat="server" ID="txtPONum" MaxLength="30" CssClass="copy10grey" Width="60%" /></td>
                                            </tr>
                                             <tr>
                                                <td class="copy10grey">Return Reason:</td>
                                                <td><asp:DropDownList ID="ddReason" CssClass="copy10grey" runat="server" Width="60%" >

<asp:ListItem Value="0" Text=""></asp:ListItem>

                                                        <asp:ListItem Value="13">ActivationIssues</asp:ListItem>
 
                                                        <asp:ListItem Value="2">AudioIssues</asp:ListItem>

                                                        <asp:ListItem Value="8">BuyerRemorse</asp:ListItem>
 
                                                        <asp:ListItem Value="14">CoverageIssues</asp:ListItem>

                                                        <asp:ListItem Value="1" >DOA</asp:ListItem>
 
                                                        <asp:ListItem  Value="11">DropCalls</asp:ListItem>

                                                        <asp:ListItem  Value="17">HardwareIssues</asp:ListItem>
 
                                                        <asp:ListItem  Value="10">LiquidDamage</asp:ListItem>

                                                        <asp:ListItem  Value="15">LoanerProgram</asp:ListItem>
 
                                                        <asp:ListItem  Value="6">MissingParts</asp:ListItem>

                                                        <asp:ListItem  Value="5">Others</asp:ListItem>
 
                                                        <asp:ListItem  Value="9">PhysicalAbuse</asp:ListItem>

                                                        <asp:ListItem  Value="4">PowerIssues</asp:ListItem>
 
                                                        <asp:ListItem  Value="7">ReturnToStock</asp:ListItem>

                                                        <asp:ListItem  Value="3">ScreenIssues</asp:ListItem>
 
                                                        <asp:ListItem  Value="16">ShippingError</asp:ListItem>

                                                        <asp:ListItem  Value="12">Software</asp:ListItem>
                                                              </asp:DropDownList></td>
                                                <td class="copy10grey"></td>
                                                <td></td>
                                            </tr>
                                             
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <hr />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:Button ID="btnSearch" runat="server" Text="Search RMA" OnClick="search_click" OnClientClick="ValidateSearch();"
                                            CssClass="button" Height="24px" Width="130px" />&nbsp;
                                            

                                        
                                        <asp:Button ID="Button2" runat="server" OnClick="Button2_Click1" Text="Cancel RMA"  Height="24px" Width="130px"
                                            CssClass="button" />

                                            
	
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="pnlGrid" runat="server">
                
                <table width="100%" class="copy10grey" style="text-align: left">
                    <tr>
                        <td>
                        <table>
                        <tr>
                            <td>
                                <%--<asp:Button ID="btnExport" runat="server" Text="Export to Excel" OnClick="btnExport_click" CssClass="button" />&nbsp;     
                                --%><asp:HyperLink ID="btnRMAReport" style="display:none" runat="server" Text="RMA Report" Visible="false" 
                                ViewStateMode="Disabled"
                                      NavigateUrl="/rma/rmalist.aspx" Target="_blank" />
                                <asp:HyperLink ID="hlkRMASummary" runat="server" Text="RMA Summary" Visible="false"
                                 ViewStateMode="Disabled"
                                      NavigateUrl="/rma/rmaSummary.aspx" Target="_blank" />



                               
                            </td>
                           
                        </tr>
                        </table>
                        
                        
                    
                   <table width="100%">
                        <tr>
                            <td align="right"> 
            
                <asp:Label ID="lblCount" CssClass="copy10grey" runat="server" ></asp:Label>
                </td>
                </tr>


                <tr>
                    <td> 
                         <asp:GridView ID="gvRma" OnPageIndexChanging="gridView_PageIndexChanging"  EnableViewState="true"  
                          AutoGenerateColumns="false"  OnRowDataBound="gvRma_RowDataBound"
                            DataKeyNames="RMAGUID"  Width="100%"  
                        ShowFooter="false" runat="server" GridLines="Both"  
                        PageSize="20" AllowPaging="true" 
                        BorderStyle="Outset"
                        AllowSorting="false" > 
                        <RowStyle BackColor="Gainsboro" />
                        <AlternatingRowStyle BackColor="white" />
                        <HeaderStyle  CssClass="buttonlabel" ForeColor="white"/>
                        <FooterStyle CssClass="white"  />
                        <PagerStyle ForeColor="#636363" CssClass="copy10grey" HorizontalAlign="Left" />
                        <Columns>
                            <%--<asp:TemplateField ItemStyle-CssClass="copy10grey"  HeaderText="" ItemStyle-Width="2%" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chk"  runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>                 
                            --%>
                            <asp:TemplateField ItemStyle-CssClass="copy10grey"  HeaderText="S.No." ItemStyle-Width="2%" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <%--<%# Eval("RMAUserCompany.CompanyName")%>--%>
                                    <%# Container.DataItemIndex + 1 %>
                                   
                                </ItemTemplate>
                            </asp:TemplateField>                 
                            
                            <asp:TemplateField ItemStyle-CssClass="copy10grey"  HeaderText="Customer Name" ItemStyle-Width="15%">
                                <ItemTemplate>
                                    <%# Eval("RMAUserCompany.CompanyName")%>

                                    <%--<asp:Label ID="lblCompany" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "RMAUserCompany.CompanyName")%>' ></asp:Label>--%>
                                </ItemTemplate>
                            </asp:TemplateField>                 
                            <asp:TemplateField ItemStyle-CssClass="copy10grey"  ItemStyle-Width="10%" HeaderText="RMA #">
                                <ItemTemplate>
                                    <%# Eval("RmaNumber")%>
                                    <%--<asp:Label ID="lblRMANo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "RmaNumber")%>' ></asp:Label>--%>
                                </ItemTemplate>
                            </asp:TemplateField>                 
                            <asp:TemplateField ItemStyle-CssClass="copy10grey"  ItemStyle-Width="10%" HeaderText="RMA Date">
                                <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "RmaDate", "{0:MM/dd/yyyy}")%>
                                    <%--<asp:Label ID="lblRmaDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "RmaDate", "{0:MM/dd/yyyy}")%>' ></asp:Label>--%>
                                    
                                </ItemTemplate>
                            </asp:TemplateField>                 
                            <asp:TemplateField ItemStyle-CssClass="copy10grey"  ItemStyle-Width="10%" HeaderText="Modified Date">
                                <ItemTemplate>
                                    <%# Eval("ModifiedDate")%>
                                </ItemTemplate>
                            </asp:TemplateField>                 
                            <asp:TemplateField ItemStyle-CssClass="copy10grey"  ItemStyle-Width="10%" HeaderText="Status">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "Status") %>
                                </ItemTemplate>
                            </asp:TemplateField>                 
                            <%--<asp:TemplateField ItemStyle-CssClass="copy10grey"  ItemStyle-Width="25%" HeaderText="Customer Comments">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "Comment")%>
                                </ItemTemplate>
                            </asp:TemplateField>                 
                            <asp:TemplateField ItemStyle-CssClass="copy10grey"  ItemStyle-Width="25%" HeaderText="AV Comments">
                                <ItemTemplate>
                                    <%# Eval("AVComments")%>
                                </ItemTemplate>
                            </asp:TemplateField>                 --%>
                            <asp:TemplateField ItemStyle-CssClass="copy10grey"  ItemStyle-Width="5%" HeaderText="">
                                <ItemTemplate>
                                     <table><tr>
                                     
                                <td>
                               <%-- <asp:UpdatePanel ID="uPnl1"   runat="server" >
                                <ContentTemplate> OnCommand="imgViewRMA_Click" 
                               --%>     <asp:ImageButton ID="imgView"  ToolTip="View RMA" OnCommand="imgViewRMA_Click"
                                     CausesValidation="false" CommandArgument='<%# Eval("rmaGUID") %>' ImageUrl="~/Images/view.png"  runat="server" />
                                <%--</ContentTemplate>
                                </asp:UpdatePanel>--%>
                                </td>
                                <td>
                                     <asp:ImageButton ID="imgCComments" AlternateText="View Customer Commnets"  ToolTip="View Customer Commnets" OnCommand="imgCComments_OnCommand"  CausesValidation="false" 
                                        CommandArgument='<%# Eval("rmaguid") + "," + Eval("rmaNumber") %>' ImageUrl="~/Images/ccomments.png"  runat="server" />
                        
                                </td>
                                <td>
                                     <asp:ImageButton ID="imgAComments" AlternateText="View LAN Global Commnets"  ToolTip="View LAN Global Commnets" OnCommand="imgAComments_OnCommand"  CausesValidation="false" 
                                        CommandArgument='<%# Eval("rmaguid") + "," + Eval("rmaNumber") %>' ImageUrl="~/Images/comments.png"  runat="server" />
                        
                                </td>
                                
                                <%--<td>
                                    <asp:ImageButton ID="imgViewDetail"  ToolTip="View RMA Detail" OnCommand="imgViewRmaDetails_Click" 
                                     CausesValidation="false" CommandArgument='<%# Eval("rmaGUID") %>' ImageUrl="~/Images/view.png"  runat="server" />
                       
                                </td>--%>
                                <td>
                                                 <asp:ImageButton ID="imgHistory"  ToolTip="RMA History" OnCommand="imgRMAHistory_Click" 
                                                  CausesValidation="false" CommandArgument='<%# Eval("rmaGUID") %>' ImageUrl="~/Images/history.png"  runat="server" />
                                
                                </td>
                                <td>
                        <asp:ImageButton  ToolTip="Edit RMA" CausesValidation="false" OnCommand="imgEditRMA_OnCommand" Visible="false"
                         CommandArgument='<%# Eval("RMAGUID") %>' ImageUrl="~/Images/edit.png" ID="imgEditRma"  runat="server" />
<a title="Edit RMA" href='NewRMAForm.aspx?rmaGUID=<%# Eval("RMAGUID") %>&companyID='>
                         <img src="/Images/edit.png" alt="" border="0"/>
                         </a>
                        
                        
                        </td>
<td>
                                     <a onclick="RmaDownload('<%# Eval("rmaGUID") %>');"
                                      style="display:<%# Convert.ToString(Eval("RmaDocument"))=="" ? "none": "block" %>"  >
                                                    <img src="../images/doc1.png" title="RMA Document" alt="RMA Document" />
                                                    </a>

                                                     <asp:ImageButton ID="imgUpload" runat="server" ImageUrl="~/images/doc.png" OnCommand="imgRmaDoc_Click" 
                                        CommandArgument='<%# Eval("rmaGUID") %>' OnClientClick="return RmaDownload();" 
                                            CausesValidation="false" Visible="false" />
                            
                                                    
                                     </td>
                        <td>
                        <asp:ImageButton ID="imgDel" runat="server"  CommandName="Delete" AlternateText="Delete RMA" ToolTip="Delete RMA" 
                        ImageUrl="~/images/delete.png" CommandArgument='<%# Eval("RMAGUID") %>' OnCommand="imgDeleteRMA_OnCommand"/>
                        </td>
                        
                                </tr></table>
                                </ItemTemplate>
                            </asp:TemplateField>  
                            

                      </Columns>
                      </asp:GridView>
                
                    </td>
                </tr>
                </table>
                </td>
                    </tr>
                </table>
                </asp:Panel>
            </td>
        </tr>
        
        </table>
				
			</ContentTemplate>
		</asp:UpdatePanel>
		

        <asp:UpdatePanel ID="upnlJsRunner" UpdateMode="Always" runat="server">
			<ContentTemplate>
				<asp:PlaceHolder ID="phrJsRunner" runat="server"></asp:PlaceHolder>
			</ContentTemplate>
		</asp:UpdatePanel>
         <br />
            <br /> <br />
            <br /> <br />
            <br /> <br />
            <br />
    <table cellspacing="0" cellpadding="0" border="0" align="center" width="100%">
        <tr>
            <td>
                <foot:MenuFooter ID="Foter" runat="server"></foot:MenuFooter>
            </td>
        </tr>
    </table>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server"
                     DynamicLayout="false">
            <ProgressTemplate>
                <img src="/Images/ajax-loaders.gif" /> Loading ...
            </ProgressTemplate>
        </asp:UpdateProgress>
			</div>
        <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.4.0/jquery.min.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/jquery-ui.min.js"></script>
        <script type="text/javascript" src="../JQuery/jquery.blockUI.js"></script>
    </form>
</body>
</html>
