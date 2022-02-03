<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FulfillmentsStatusReport.aspx.cs" Inherits="avii.Reports.FulfillmentsStatusReport" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>
<%@ Register TagPrefix="menu" TagName="Menu" Src="~/Controls/Header.ascx" %>
<%@ Register TagPrefix="POS" TagName="Status" Src="~/Controls/StoresFulfillmentStatus.ascx" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title>Customerwise Fulfillment Status Report</title>
     <link href="../aerostyle.css" type="text/css" rel="stylesheet"/>
      <!-- from http://encosia.com/2009/10/11/do-you-know-about-this-undocumented-google-cdn-feature/ -->
    <link href="https://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/themes/start/jquery-ui.css" type="text/css" rel="Stylesheet" />
	<%--
	<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.4.0/jquery.min.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/jquery-ui.min.js"></script>--%>
    
	<script type="text/javascript" src="../JQuery/jquery.min.js"></script>
    <script type="text/javascript" src="../JQuery/jquery-ui.min.js"></script>
	<script type="text/javascript" src="../JQuery/jquery.blockUI.js"></script
	
	
<!-- fix for 1.1em default font size in Jquery UI -->
	<style type="text/css">
		.ui-widget{font-size:11px !important;}
		.ui-state-error-text{margin-left: 10px}
	</style>
    
	<script type="text/javascript">

	    $(document).ready(function () {


	        $("#divFulfillmentView").dialog({
	            autoOpen: false,
	            modal: true,
	            minHeight: 20,
	            height: 500,
	            width: 1124,
	            resizable: false,
	            open: function (event, ui) {
	                $(this).parent().appendTo("#divFulfillmentContainer");
	            }
	        });
	    });

	    function closeDialog() {
	        //Could cause an infinite loop because of "on close handling"
	        $("#divFulfillmentView").dialog('close');
	    }
	    function openDialog(title, linkID) {

	        var pos = $("#" + linkID).position();
	        var top = pos.top;
	        var left = pos.left + $("#" + linkID).width() + 10;
	        //alert(top);
	        if (top > 600)
	            top = 10;
	        top = 100;
	        //top = top - 600;
	        left = 100;
	        $("#divFulfillmentView").dialog("option", "title", title);
	        $("#divFulfillmentView").dialog("option", "position", [left, top]);

	        $("#divFulfillmentView").dialog('open');

	    }
	    function openDialogAndBlock(title, linkID) {
	        openDialog(title, linkID);

	        //block it to clean out the data
	        $("#divFulfillmentView").block({
	            message: '<img src="../images/async.gif" />',
	            css: { border: '0px' },
	            fadeIn: 0,
	            //fadeOut: 0,
	            overlayCSS: { backgroundColor: '#ffffff', opacity: 1 }
	        });
	    }

	    function unblockDialog() {
	        $("#divFulfillmentView").unblock();
	    }

	    </script>
   
   	
</head>
<body bgcolor="#ffffff" leftmargin="0" rightmargin="0" topmargin="0">
    <form id="form1" runat="server">
    <table cellspacing="0" cellpadding="0" border="0" align="center" width="100%">
    <tr>
        <td>
            <menu:Menu ID="menu1" runat="server" ></menu:Menu>
        </td>
     </tr>
     </table><br />
     <table cellspacing="0" cellpadding="0" border="0" align="center" width="95%">
        
        <tr valign="top">
           
            <td   >
    <table align="center" style="text-align:left" width="100%">
                <tr class="button" align="left">
                <td>&nbsp;Customerwise Fulfillment Status Report</td></tr>
             </table><br />
     
     <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <div id="divFulfillmentContainer">
			<div id="divFulfillmentView" style="display:none">
					
				<asp:UpdatePanel ID="upnlView" runat="server">
					<ContentTemplate>
                   
						<asp:PlaceHolder ID="phrView" runat="server">
                            <%--<asp:Label ID="lblViewPO" runat="server"  CssClass="errormessage"></asp:Label>
    --%>
                            <asp:Panel ID="pnlStore" runat="server">
                                <POS:Status ID="posStatus1" runat="server" />

                            </asp:Panel>


                        </asp:PlaceHolder>
						
						
					</ContentTemplate>
							
				</asp:UpdatePanel>
           </div>
           </div>
    
    
      <asp:UpdatePanel ID="UpdatePanel1"  runat="server"  UpdateMode="Conditional" >
     <ContentTemplate>
     <table  align="center" style="text-align:left" width="99%">
     <tr>
        <td>
           <asp:Label ID="lblMsg" runat="server" CssClass="errormessage"></asp:Label>
            
        </td>
     </tr>
     </table>
     <table bordercolor="#839abf" border="1" width="100%" align="center" cellpadding="0" cellspacing="0">
      <tr>
        <td>
         <asp:Panel  ID="pnlSearch" runat="server"  DefaultButton="btnSearch" >
     <table width="100%" border="0" class="box" align="center" cellpadding="2" cellspacing="2">
         <tr style="height:1px">
         <td style="height:1px"></td>
         </tr>
     
           
            <tr valign="top">
                <td class="copy10grey" align="right" width="15%">
                    Customer Name:
                </td>
                <td width="35%">
                <asp:DropDownList ID="dpCompany" runat="server" AutoPostBack="false" 
                 class="copy10grey"  >
                </asp:DropDownList> 
                <asp:Label ID="lblCompany" runat="server" CssClass="copy10grey" ></asp:Label>
           
                </td>
                <td  width="1%">
                    &nbsp;
                </td>
                <td class="copy10grey" align="right" width="10%">
                    Duration:
                </td>
                <td width="40%">
                   <asp:DropDownList ID="ddlDuration" AutoPostBack="false"  runat="server" Class="copy10grey" 
                                                Width="135px" >
                                                        <asp:ListItem  Value="1"  >Today</asp:ListItem>
                                <asp:ListItem  Value="7"  >One week</asp:ListItem>
                                <asp:ListItem  Value="15"  >Two week</asp:ListItem>
                                <asp:ListItem  Value="30" Selected="True" >Last Month</asp:ListItem>
                                <asp:ListItem Value="90" >Quaterly</asp:ListItem>
                                <asp:ListItem  Value="180">6 Months</asp:ListItem>
                                <asp:ListItem  Value="365">One Year</asp:ListItem>
                    </asp:DropDownList>
                    <br /><br />
                    <asp:Label ID="lblDuration" runat="server" CssClass="copy10grey" ></asp:Label>
        
        
                </td>   
                
                    
                </tr>
                <tr>
                <td colspan="5">
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
      
     <br />
      <table align="center" style="text-align:left" width="100%">
      <tr>
     <tr>
                <td  align="center"  colspan="5">
                    <asp:Panel ID="pnlPO" runat="server">
                        <%--<PO:Status ID="pos1" runat="server" />--%>
                        <table width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td align="right">
                                <asp:Label ID="lblCount" CssClass="copy10grey" runat="server" ></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td >
    
                        <asp:GridView ID="gvPO" OnPageIndexChanging="gridView_PageIndexChanging"    AutoGenerateColumns="false"  
                        Width="100%" ShowHeader="true"  ShowFooter="false" runat="server" GridLines="Both" 
                        PageSize="50" AllowPaging="true" AllowSorting="false" OnRowDataBound="gvPO_RowDataBound"
                        >
                        <RowStyle BackColor="Gainsboro" />
                        <AlternatingRowStyle BackColor="white" />
                        <HeaderStyle  CssClass="button" ForeColor="white"/>
                          <PagerStyle ForeColor="#636363" CssClass="copy10grey" HorizontalAlign="Left" />
                          <Columns>
                                <asp:TemplateField HeaderText="S.No." ItemStyle-CssClass="copy10grey"  ItemStyle-Width="2%"  ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>

                                          <%# Convert.ToString(Eval("CustomerName")) == "zzTotal" ? "" : Convert.ToString(Container.DataItemIndex + 1)%> &nbsp;
                                    </ItemTemplate>
                                </asp:TemplateField>                 
        
                                <asp:TemplateField HeaderText="Customer Name" ItemStyle-CssClass="copy10grey"  ItemStyle-Width="25%" ItemStyle-HorizontalAlign="Left" >
                                    <ItemTemplate>
                                     <div style="width:100%;  font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
                                    <asp:LinkButton ID="lnkPos" CommandArgument='<%# Eval("CustomerName") %>' OnCommand="lnkPOStatus_OnCommand" runat="server">
                                    <%# Convert.ToString(Eval("CustomerName")) == "zzTotal" ? "Total" : Eval("CustomerName")%>
                                    </asp:LinkButton>
                                            
            
                                         </div>
           
                                         <%--<%# Eval("CustomerName")%>--%> 
                                    </ItemTemplate>
                                </asp:TemplateField>                 
                
                                <asp:TemplateField HeaderText="Pending" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                     <div style="width:100%;  font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
                                    &nbsp;

                                       <%--<a class="copy10grey" href='FulfillmentsStatusReport.aspx?pos=<%# Eval("Pending")%>&t=<% =TimeInterval%>&cid=<%# Eval("CompanyID")%>'> --%>
                                    <%# Convert.ToString(Eval("Pending")) == "0" ? "" : Convert.ToString(Eval("Pending"))%>   
                                    <%--</a>--%>
            
                                    </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="In Process" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                     <div style="width:100%;  font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
                                        &nbsp;

                                    <%--<%# Eval("InProcess")%>   --%>
                                    <%--<a class="copy10grey" href='FulfillmentsStatusReport.aspx?pos=<%# Eval("InProcess")%>&t=<% =TimeInterval%>&cid=<%# Eval("CompanyID")%>'> --%>
                                    <%# Convert.ToString(Eval("InProcess")) == "0" ? "" : Convert.ToString(Eval("InProcess"))%>
                                    <%--</a>--%>
            
                                    </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Processed" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                     <div style="width:100%;  font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
                                    &nbsp;    
                                    <%--<%# Eval("Processed")%>   --%>
                                    <%--<a class="copy10grey" href='FulfillmentsStatusReport.aspx?pos=<%# Eval("Processed")%>&t=<% =TimeInterval%>&cid=<%# Eval("CompanyID")%>'> --%>
                                    <%# Convert.ToString(Eval("Processed")) == "0" ? "" : Convert.ToString(Eval("Processed"))%>
                                    <%--</a>   --%>
            
                                    </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Shipped" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                     <div style="width:100%;  font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
                                    &nbsp;   
                                    <%--<%# Eval("Shipped")%>   --%>
                                    <%--<a class="copy10grey" href='FulfillmentsStatusReport.aspx?pos=<%# Eval("Shipped")%>&t=<% =TimeInterval%>&cid=<%# Eval("CompanyID")%>'> --%>
                                    <%# Convert.ToString(Eval("Shipped")) == "0" ? "" : Convert.ToString(Eval("Shipped"))%>   
                                    <%--</a>--%>
            
                                    </div>

                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Closed" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <div style="width:100%;  font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
                                    &nbsp;
                                    <%--<%# Eval("Closed")%>   --%>
                                    <%--<a class="copy10grey" href='FulfillmentsStatusReport.aspx?pos=<%# Eval("Closed")%>&t=<% =TimeInterval%>&cid=<%# Eval("CompanyID")%>'> --%>
            
                                    <%# Convert.ToString(Eval("Closed")) == "0" ? "" : Convert.ToString(Eval("Closed"))%>   
                                    <%--</a>--%>
            
                                    </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Return" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <div style="width:100%;  font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
                                    &nbsp;
                                    <%--<%# Eval("Return")%>   --%>
                                    <%--<a class="copy10grey" href='FulfillmentsStatusReport.aspx?pos=<%# Eval("Return")%>&t=<% =TimeInterval%>&cid=<%# Eval("CompanyID")%>'> 
                                    --%>
                                    <%# Convert.ToString(Eval("Return")) == "0" ? "" : Convert.ToString(Eval("Return"))%>   
                                    <%--</a>--%>
            
                                    </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="On Hold" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="8%">
                                    <ItemTemplate>
                                        <div style="width:100%;  font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
                                    &nbsp;
                                    <%--<%# Eval("OnHold")%>   --%>
                                    <%--<a class="copy10grey" href='FulfillmentsStatusReport.aspx?pos=<%# Eval("OnHold")%>&t=<% =TimeInterval%>&cid=<%# Eval("CompanyID")%>'> 
                                    --%>
                                    <%# Convert.ToString(Eval("OnHold")) == "0" ? "" : Convert.ToString(Eval("OnHold"))%>   
                                    <%--</a>--%>
            
                                    </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Out of Stock" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="12%">
                                    <ItemTemplate>
                                        <div style="width:100%;  font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
                                    &nbsp;
                                    <%--<%# Eval("OutofStock")%> --%>  
                                    <%--<a class="copy10grey" href='FulfillmentsStatusReport.aspx?pos=<%# Eval("OutofStock")%>&t=<% =TimeInterval%>&cid=<%# Eval("CompanyID")%>'> --%>
            
                                    <%# Convert.ToString(Eval("OutofStock")) == "0" ? "" : Convert.ToString(Eval("OutofStock"))%>   
                                    <%--</a>--%>
            
                                    </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cancel" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <div style="width:100%;  font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
                                    &nbsp;
                                    <%--<%# Eval("Cancel")%>   --%>
                                   <%-- <a class="copy10grey" href='FulfillmentsStatusReport.aspx?pos=<%# Eval("Cancel")%>&t=<% =TimeInterval%>&cid=<%# Eval("CompanyID")%>'> 
                                   --%> 
                                    <%# Convert.ToString(Eval("Cancel")) == "0" ? "" : Convert.ToString(Eval("Cancel"))%>   
                                    <%--</a>--%>
            
                                    </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Partial Processed" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="13%">
                                    <ItemTemplate>
                                        <div style="width:100%;  font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
                                    &nbsp;
                                    <%--<%# Eval("PartialProcessed")%>   --%>
                                    <%--<a class="copy10grey" href='FulfillmentsStatusReport.aspx?pos=<%# Eval("PartialProcessed")%>&t=<% =TimeInterval%>&cid=<%# Eval("CompanyID")%>'> 
                                    --%>
                                    <%# Convert.ToString(Eval("PartialProcessed")) == "0" ? "" : Convert.ToString(Eval("PartialProcessed"))%>   
                                    <%--</a>--%>
                                    </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ControlStyle-BackColor="Yellow" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <div style="width:100%;  font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:yellow; height:20px">
                                    &nbsp;
                                   <b> <%# Eval("Total")%>   </b>
                                    <%--<a class="copy10grey" href='FulfillmentsStatusReport.aspx?pos=<%# Eval("PartialProcessed")%>&t=<% =TimeInterval%>&cid=<%# Eval("CompanyID")%>'> 
                                    --%>
                                    <%--<%# Convert.ToString(Eval("HardwareIssues")) == "0" ? "" : Convert.ToString(Eval("HardwareIssues"))%>   --%>
                                    <%--</a>--%>
                                    </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <asp:Label ID="lblPO" runat="server" CssClass="errormessage"></asp:Label>

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

        <asp:UpdateProgress ID="UpdateProgress1" runat="server"
                     DynamicLayout="false">
            <ProgressTemplate>
                <img src="/Images/ajax-loaders.gif" /> Loading ...
            </ProgressTemplate>
        </asp:UpdateProgress>
      </td>
      </tr>
      </table>
    </form>
</body>
</html>
