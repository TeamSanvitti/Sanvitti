<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ForecastReport.aspx.cs" Inherits="avii.Reports.ForecastReport" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="/Controls/Footer.ascx" %>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="/Controls/Header.ascx" %>
<%@ Register TagPrefix="UC" TagName="Comments" Src="~/Controls/ForecastComment.ascx" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Forecast Report</title>
    <link href="../aerostyle.css" type="text/css" rel="stylesheet" />
    
    <!-- from http://encosia.com/2009/10/11/do-you-know-about-this-undocumented-google-cdn-feature/ -->
    <link href="https://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/themes/start/jquery-ui.css" type="text/css" rel="Stylesheet" />
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
        $(document).ready(function () {
            

            $("#divComment").dialog({
                autoOpen: false,
                modal: true,
                minHeight: 300,
                height: 400,
                width: 650,
                resizable: false,
                open: function (event, ui) {
                    $(this).parent().appendTo("#divContainer");
                }
            });

        });


        function closeDialog() {
            //Could cause an infinite loop because of "on close handling"
            $("#divComment").dialog('close');
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
            $("#divComment").dialog("option", "title", title);
            $("#divComment").dialog("option", "position", [left, top]);

            $("#divComment").dialog('open');

        }


        function openDialogAndBlock(title, linkID) {

            openDialog(title, linkID);
            //alert('2')
            //block it to clean out the data
            $("#divComment").block({
                message: '<img src="../images/async.gif" />',
                css: { border: '0px' },
                fadeIn: 0,
                //fadeOut: 0,
                overlayCSS: { backgroundColor: '#ffffff', opacity: 1 }
            });
        }

        function unblockDialog() {
            $("#divComment").unblock();
        }


        function IsValidate() {
            var company = document.getElementById("<% =dpCompany.ClientID %>");
            //alert(company);
            if (company != 'null' && company.selectedIndex == 0) {
                alert('Customer is required!');
                return false;

            }
        }

	</script>
    
</head>
<body>
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
		<td colspan="6" bgcolor="#dee7f6" class="button">&nbsp;&nbsp; Forecast Report
		</td>
    </tr>
    </table>   
    <br />
    
    <div id="divContainer">	
			<div id="divComment" style="display:none">
					
				<asp:UpdatePanel ID="upnlComment" runat="server">
					<ContentTemplate>
                   
						<asp:PlaceHolder ID="phrComment" runat="server">
                            <table width="100%" border="0">
                                
                                <tr>
                                    <td colspan="2">
                                
                                    <asp:Panel ID="pnlComment" runat="server" Width="100%">
                                        <%--<asp:Label ID="lblCMsg" runat="server" Width="100%" CssClass="errormessage"></asp:Label>--%>
                                        <UC:Comments ID="c1" runat="server" ></UC:Comments>
                                    
                                    </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                                
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
                            <table width="100%" bgColor="#ffffff" leftmargin="0" rightmargin="0" topmargin="0" border="0" cellSpacing="3" cellPadding="3">
                            <tr height="8">
                                <td>
                               
                                </td>
                            </tr>
                            <tr valign="top">
                            <td class="copy10grey" align="right" width="15%">
                                Customer Name:
                            </td>
                            <td width="35%" align="left">
                            &nbsp;<asp:DropDownList ID="dpCompany" runat="server" AutoPostBack="true" OnSelectedIndexChanged="dpCompany_SelectedIndexChanged"  Width="80%"
                             class="copy10grey"  >
                            </asp:DropDownList>  
                            <asp:Label ID="lblCompany" runat="server" CssClass="copy10grey" ></asp:Label>
                            </td>
                            <td  width="1%">
                                &nbsp;
                            </td>
                            <td class="copy10grey" align="right" width="10%">
                                <asp:Label ID="lblSKU" runat="server" CssClass="copy10grey" Text="SKU:" ></asp:Label>
                
                            </td>
                            <td class="copy10grey" align="left" width="40%">
                                &nbsp;<asp:DropDownList ID="ddlSKU" runat="server" Width="70%" class="copy10grey">
                                </asp:DropDownList>
          
                            </td>

                        </tr>    
                        <tr valign="top">
                            <td class="copy10grey" align="right" width="15%">
                                Duration:
                            </td>
                            <td width="35%" align="left">
                            &nbsp;<asp:DropDownList ID="ddlMonth" runat="server" Width="25%"
                             class="copy10grey"  >
                                <asp:ListItem Text="Select Month" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="January" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="February" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="March" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="April" Value="4"></asp:ListItem>
                                    <asp:ListItem Text="May" Value="5"></asp:ListItem>
                                    <asp:ListItem Text="June" Value="6"></asp:ListItem>
                                    <asp:ListItem Text="July" Value="7"></asp:ListItem>
                                    <asp:ListItem Text="August" Value="8"></asp:ListItem>
                                    <asp:ListItem Text="September" Value="9"></asp:ListItem>
                                    <asp:ListItem Text="October" Value="10"></asp:ListItem>
                                    <asp:ListItem Text="November" Value="11"></asp:ListItem>
                                    <asp:ListItem Text="December" Value="12"></asp:ListItem>
                            </asp:DropDownList>  
                            &nbsp;<asp:DropDownList ID="ddlYears" runat="server" Width="20%"
                             class="copy10grey"  >
                             <%--<asp:ListItem Text="2014" Value="2014"></asp:ListItem>
                                    <asp:ListItem Text="2015" Value="2015"></asp:ListItem>
                                    <asp:ListItem Text="2016" Value="2016"></asp:ListItem>
                                    <asp:ListItem Text="2017" Value="2017"></asp:ListItem>
                                    <asp:ListItem Text="2018" Value="2018"></asp:ListItem>
                             --%>   
                            </asp:DropDownList>  
                            
                            </td>
                            <td  width="1%">
                                &nbsp;
                            </td>
                            <td class="copy10grey" align="right" width="10%">
                                
                
                            </td>
                            <td class="copy10grey" align="left" width="40%">
                                
          
                            </td>

                        </tr>    
            
                            
                            <tr>
                                <td colspan="5">
                                <hr />
                                </td>
                                </tr>
                                <tr>
                                <td  align="center"  colspan="5">

                                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="button"  OnClick="btnSearch_Click" CausesValidation="false" OnClientClick="return IsValidate();" />
                                    &nbsp;<asp:Button ID="btnDownload" runat="server" Text=" Download " CssClass="button"  OnClick="btnDownload_Click" CausesValidation="false"/>
                                    &nbsp;<asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="button" OnClick="btnCancel_Click" CausesValidation="false"/>
                                    
        
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
                                    OnPageIndexChanging="gridView_PageIndexChanging" PageSize="25" AllowPaging="true" 
                                    OnRowDataBound="gvForecast_RowDataBound" ShowFooter="false" DataKeyNames="ForecastID" >
                                        <RowStyle BackColor="Gainsboro" />
                                        <AlternatingRowStyle BackColor="white" />
                                        <HeaderStyle  CssClass="button" ForeColor="white"/>
                                        <FooterStyle CssClass="white"  />
                                        <PagerStyle ForeColor="#636363" CssClass="copy10grey" HorizontalAlign="Left" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No." ItemStyle-HorizontalAlign="Right"  ItemStyle-CssClass="copy10grey" 
                                            ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                <%# Container.DataItemIndex +  1 %>
                                                
                                                </ItemTemplate>
                                                
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="CustomerName" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="20%">
                                                <ItemTemplate><%#Eval("CustomerName")%></ItemTemplate>
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField HeaderText="Forecast#" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="15%">
                                                <ItemTemplate><%#Eval("ForecastNUMBER")%></ItemTemplate>
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField HeaderText="Forecast date" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="15%">
                                                <ItemTemplate>
                                                
                                                <%--<%#Eval("SHIPDATE")%>--%>
                                                <%# Convert.ToDateTime(Eval("ForecastDATE")).ToShortDateString()%>
                                                
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            
                                            
                                            
                                            
                                             
                                            
                                            <asp:TemplateField HeaderText="Status" ItemStyle-HorizontalAlign="Left" FooterStyle-HorizontalAlign="Right"  ItemStyle-CssClass="copy10grey" FooterStyle-CssClass="copy10grey" ItemStyle-Width="8%">
                                                <ItemTemplate>
                                                <%#Eval("Status")%>
                                                </ItemTemplate>
                                                
                                            </asp:TemplateField> 
                                            
                                            <asp:TemplateField HeaderText="SKU" ItemStyle-HorizontalAlign="Left" FooterStyle-HorizontalAlign="Right"  ItemStyle-CssClass="copy10grey" FooterStyle-CssClass="copy10grey" ItemStyle-Width="8%">
                                                <ItemTemplate>
                                                <%#Eval("SKU")%>
                                                </ItemTemplate>
                                                
                                            </asp:TemplateField> 
                                            
                                            <asp:TemplateField HeaderText="Quantity" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right"  ItemStyle-CssClass="copy10grey" FooterStyle-CssClass="copy10grey" ItemStyle-Width="8%">
                                                <ItemTemplate>
                                                <%#Eval("Quantity")%>
                                                </ItemTemplate>
                                                
                                            </asp:TemplateField> 
                                            
                                            <asp:TemplateField HeaderText="Price($)" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right"  ItemStyle-CssClass="copy10grey" FooterStyle-CssClass="copy10grey" ItemStyle-Width="8%">
                                                <ItemTemplate>
                                                $<%#Eval("Price", "{0:n}")%>
                                                </ItemTemplate>
                                                
                                            </asp:TemplateField> 
                                            
                                            <asp:TemplateField HeaderText="Total Price($)" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right"  ItemStyle-CssClass="copy10grey" FooterStyle-CssClass="copy10grey" ItemStyle-Width="8%">
                                                <ItemTemplate>
                                                $<%#Eval("totalprice", "{0:n}")%>
                                                </ItemTemplate>
                                                
                                            </asp:TemplateField> 

                                                        
                                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center"   ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
                                                <ItemTemplate>
                                                <table>
                                                <tr>
                                                    <td>
                                                        <asp:ImageButton ID="imgComment"  ToolTip="View Forecast Comments" OnCommand="imgComment_OnCommand"  CausesValidation="false" 
                                                        CommandArgument='<%# Eval("ForecastID") %>' ImageUrl="~/Images/view.png"  runat="server" />
                            
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


    <Triggers>
        <asp:PostBackTrigger ControlID="btnDownload" />
    </Triggers>
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
    


    </form>
</body>
</html>
