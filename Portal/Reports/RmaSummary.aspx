<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RmaSummary.aspx.cs" Inherits="avii.Reports.RmaSummary" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>
<%@ Register TagPrefix="menu" TagName="Menu" Src="~/Controls/Header.ascx" %>
<%@ Register TagPrefix="RMA" TagName="Statuses" Src="~/Controls/ctlRmaStatusesSummary.ascx" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<title>RMA Summary of Product & Reasons</title>
     <link href="../aerostyle.css" rel="stylesheet" type="text/css"/>

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


	        $("#divRMAView").dialog({
	            autoOpen: false,
	            modal: true,
	            minHeight: 20,
	            height: 400,
	            width: 450,
	            resizable: false,
	            open: function (event, ui) {
	                $(this).parent().appendTo("#divRMAContainer");
	            }
	        });
	    });

	    function closeDialog() {
	        //Could cause an infinite loop because of "on close handling"
	        $("#divRMAView").dialog('close');
	    }
	    function openDialog(title, linkID) {

	        var pos = $("#" + linkID).position();
	        var top = pos.top;
	        var left = pos.left + $("#" + linkID).width() + 10;
	        //alert(top);
	        if (top > 500)
	            top = 10;
	        //top = top - 600;
	        left = 350;
	        $("#divRMAView").dialog("option", "title", title);
	        $("#divRMAView").dialog("option", "position", [left, top]);

	        $("#divRMAView").dialog('open');

	    }
	    function openDialogAndBlock(title, linkID) {
	        openDialog(title, linkID);

	        //block it to clean out the data
	        $("#divRMAView").block({
	            message: '<img src="../images/async.gif" />',
	            css: { border: '0px' },
	            fadeIn: 0,
	            //fadeOut: 0,
	            overlayCSS: { backgroundColor: '#ffffff', opacity: 1 }
	        });
	    }

	    function unblockDialog() {
	        $("#divRMAView").unblock();
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
     </table>
     <%--<table cellspacing="0" cellpadding="0" border="0" align="center" width="95%">
        
        <tr valign="top">
           
            <td   >--%>
    <table align="center" style="text-align:left" width="95%">
                <tr class="buttonlabel" align="left">
                <td>&nbsp;RMA Summary of Product & Reasons</td></tr>
             </table>
     
     <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
      <asp:UpdatePanel ID="UpdatePanel1"  runat="server" >
     <ContentTemplate>
     <table  align="center" style="text-align:left" width="99%">
     <tr>
        <td>
           <asp:Label ID="lblMsg" runat="server" CssClass="errormessage"></asp:Label>
            
        </td>
     </tr>
     </table>
     <table bordercolor="#839abf" border="1" width="95%" align="center" cellpadding="0" cellspacing="0">
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
                    Duratioin:
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
      <%--</td>
      </tr>
      </table>--%>
      <br />
      <table align="center" style="text-align:left" width="100%">
      <tr>
                <td  align="center"  >
                    <asp:Panel ID="pnlRMA" runat="server">
                        <table width="115%" cellpadding="0" cellspacing="0">
        <tr>
            <td align="left">
                <asp:Label ID="lblCount" CssClass="copy10grey" runat="server" ></asp:Label>
            </td>
        </tr>
        <tr>
            <td >
    
         <asp:GridView ID="gvRMA" OnPageIndexChanging="gridView_PageIndexChanging"    AutoGenerateColumns="false"  
        Width="100%" ShowHeader="true"  ShowFooter="false" runat="server" GridLines="Both" 
        PageSize="50" AllowPaging="true" AllowSorting="false"    OnRowDataBound="GridView1_RowDataBound"
        >
        <RowStyle BackColor="Gainsboro" />
        <AlternatingRowStyle BackColor="white" />
        <HeaderStyle  CssClass="buttongrid" ForeColor="white"/>
          <PagerStyle ForeColor="#636363" CssClass="copy10grey" HorizontalAlign="Left" />
          <Columns>
                <asp:TemplateField HeaderText="S.No." ItemStyle-CssClass="copy10grey"    ItemStyle-Width="2%">
                    <ItemTemplate>
                    <%--<div style="width:100%;  font-weight:<%# Convert.ToString(Eval("ProductName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("ProductName")) == "zzTotal"? "yellow":"" %>; height:20px">--%>
                         <%--<%# Container.DataItemIndex + 1 %> 
        --%>
                         <%# Convert.ToString(Eval("ProductName")) == "zzTotal" ? "" : Convert.ToString(Container.DataItemIndex + 1)%>
                         <%--</div>--%>
            
                         <%--<%# Container.DataItemIndex + 1 %> --%>
                    </ItemTemplate>
                </asp:TemplateField>                 
        
                <asp:TemplateField HeaderText="Product Name" ItemStyle-CssClass="copy10grey"  ItemStyle-Width="18%" ItemStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                    <div style="width:100%;  font-weight:<%# Convert.ToString(Eval("ProductName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("ProductName")) == "zzTotal"? "yellow":"" %>; height:20px">
            
                    <%# Convert.ToString(Eval("ProductName")) == "zzTotal" ? "Total" : Eval("ProductName")%>
                    
                         </div>
                    </ItemTemplate>
                </asp:TemplateField>                 
                
                <asp:TemplateField HeaderText="DOA" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                    <ItemTemplate>
                    <div style="width:100%;  font-weight:<%# Convert.ToString(Eval("ProductName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("ProductName")) == "zzTotal"? "yellow":"" %>; height:20px">
                    &nbsp;
                       <%--<a class="copy10grey" href='FulfillmentsStatusReport.aspx?pos=<%# Eval("Pending")%>&t=<% =TimeInterval%>&cid=<%# Eval("CompanyID")%>'> --%>
                        <asp:LinkButton ID="lnkDOA" runat="server" CssClass='<%# Convert.ToString(Eval("ProductName")) == "zzTotal" ? "copy10link" : "copy10grey" %>' CommandArgument='<%# "1," + Eval("ProductName")  %>' OnCommand="lnkDOA_OnCommand">
                    
                    <%# Convert.ToString(Eval("DOA")) == "0" ? "" : Convert.ToString(Eval("DOA"))%>
                    </asp:LinkButton>   
                    <%--</a>--%>
                    </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Audio Issues" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                    <ItemTemplate>
                        <div style="width:100%;  font-weight:<%# Convert.ToString(Eval("ProductName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("ProductName")) == "zzTotal"? "yellow":"" %>; height:20px">
                    &nbsp;
                    <%--<%# Eval("InProcess")%>   --%>
                    <%--<a class="copy10grey" href='FulfillmentsStatusReport.aspx?pos=<%# Eval("InProcess")%>&t=<% =TimeInterval%>&cid=<%# Eval("CompanyID")%>'> --%>
                    <asp:LinkButton ID="lnkAI" runat="server" CssClass='<%# Convert.ToString(Eval("ProductName")) == "zzTotal" ? "copy10link" : "copy10grey" %>'  CommandArgument='<%# "2," + Eval("ProductName")%>' OnCommand="lnkDOA_OnCommand">
                    <%# Convert.ToString(Eval("AudioIssues")) == "0" ? "" : Convert.ToString(Eval("AudioIssues"))%>
                    </asp:LinkButton>
                    <%--</a>--%>
                    </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Screen Issues" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                    <ItemTemplate>
                        <div style="width:100%;  font-weight:<%# Convert.ToString(Eval("ProductName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("ProductName")) == "zzTotal"? "yellow":"" %>; height:20px">
                    &nbsp;
                    <%--<%# Eval("Processed")%>   --%>
                    <%--<a class="copy10grey" href='FulfillmentsStatusReport.aspx?pos=<%# Eval("Processed")%>&t=<% =TimeInterval%>&cid=<%# Eval("CompanyID")%>'> --%>
                    <asp:LinkButton ID="lnkSI" runat="server"  CssClass='<%# Convert.ToString(Eval("ProductName")) == "zzTotal" ? "copy10link" : "copy10grey" %>' CommandArgument='<%# "3," + Eval("ProductName")%>' OnCommand="lnkDOA_OnCommand">
                    <%# Convert.ToString(Eval("ScreenIssues")) == "0" ? "" : Convert.ToString(Eval("ScreenIssues"))%></asp:LinkButton>
                    <%--</a>   --%>
                    </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Power Issues" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                    <ItemTemplate>
                        <div style="width:100%;  font-weight:<%# Convert.ToString(Eval("ProductName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("ProductName")) == "zzTotal"? "yellow":"" %>; height:20px">
                    &nbsp;
                    <%--<%# Eval("Shipped")%>   --%>
                    <%--<a class="copy10grey" href='FulfillmentsStatusReport.aspx?pos=<%# Eval("Shipped")%>&t=<% =TimeInterval%>&cid=<%# Eval("CompanyID")%>'> --%>
                    <asp:LinkButton ID="lnkPI" runat="server"  CssClass='<%# Convert.ToString(Eval("ProductName")) == "zzTotal" ? "copy10link" : "copy10grey" %>' CommandArgument='<%# "4," + Eval("ProductName")%>' OnCommand="lnkDOA_OnCommand">
                    <%# Convert.ToString(Eval("PowerIssues")) == "0" ? "" : Convert.ToString(Eval("PowerIssues"))%>  </asp:LinkButton> 
                    <%--</a>--%>
                    </div>

                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Others" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                    <ItemTemplate>
                        <div style="width:100%;  font-weight:<%# Convert.ToString(Eval("ProductName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("ProductName")) == "zzTotal"? "yellow":"" %>; height:20px">
            
                    <%--<%# Eval("Closed")%>   --%>
                    <%--<a class="copy10grey" href='FulfillmentsStatusReport.aspx?pos=<%# Eval("Closed")%>&t=<% =TimeInterval%>&cid=<%# Eval("CompanyID")%>'> --%>

                    &nbsp;
                    <asp:LinkButton ID="lnkOthers" runat="server"  CssClass='<%# Convert.ToString(Eval("ProductName")) == "zzTotal" ? "copy10link" : "copy10grey" %>' CommandArgument='<%# "5," + Eval("ProductName")%>' OnCommand="lnkDOA_OnCommand">
                    <%# Convert.ToString(Eval("Others")) == "0" ? "" : Convert.ToString(Eval("Others"))%>  </asp:LinkButton>  
                    <%--</a>--%>
                    </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Missing Parts" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                    <ItemTemplate>
                        <div style="width:100%;  font-weight:<%# Convert.ToString(Eval("ProductName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("ProductName")) == "zzTotal"? "yellow":"" %>; height:20px">
            
                    <%--<%# Eval("Closed")%>   --%>
                    <%--<a class="copy10grey" href='FulfillmentsStatusReport.aspx?pos=<%# Eval("Closed")%>&t=<% =TimeInterval%>&cid=<%# Eval("CompanyID")%>'> --%>

                    &nbsp;
                    <asp:LinkButton ID="lnkMP" runat="server"  CssClass='<%# Convert.ToString(Eval("ProductName")) == "zzTotal" ? "copy10link" : "copy10grey" %>' CommandArgument='<%# "6," + Eval("ProductName")%>' OnCommand="lnkDOA_OnCommand">
                    <%# Convert.ToString(Eval("MissingParts")) == "0" ? "" : Convert.ToString(Eval("MissingParts"))%>  </asp:LinkButton>  
                    <%--</a>--%>
                    </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Return To Stock" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                    <ItemTemplate>
                        <div style="width:100%;  font-weight:<%# Convert.ToString(Eval("ProductName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("ProductName")) == "zzTotal"? "yellow":"" %>; height:20px">
            
                    <%--<%# Eval("Return")%>   --%>
                    <%--<a class="copy10grey" href='FulfillmentsStatusReport.aspx?pos=<%# Eval("Return")%>&t=<% =TimeInterval%>&cid=<%# Eval("CompanyID")%>'> 
                    --%>&nbsp;
                    <asp:LinkButton ID="lnkRTS" runat="server"  CssClass='<%# Convert.ToString(Eval("ProductName")) == "zzTotal" ? "copy10link" : "copy10grey" %>' CommandArgument='<%# "7," +  Eval("ProductName")%>' OnCommand="lnkDOA_OnCommand">
                    <%# Convert.ToString(Eval("ReturnToStock")) == "0" ? "" : Convert.ToString(Eval("ReturnToStock"))%>   </asp:LinkButton>
                    <%--</a>--%>
                    </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Buyer Remorse" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                    <ItemTemplate>
                        <div style="width:100%;  font-weight:<%# Convert.ToString(Eval("ProductName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("ProductName")) == "zzTotal"? "yellow":"" %>; height:20px">
            
                    <%--<%# Eval("OnHold")%>   --%>
                    <%--<a class="copy10grey" href='FulfillmentsStatusReport.aspx?pos=<%# Eval("OnHold")%>&t=<% =TimeInterval%>&cid=<%# Eval("CompanyID")%>'> 
                    --%>&nbsp;
                    <asp:LinkButton ID="lnkBR" runat="server"  CssClass='<%# Convert.ToString(Eval("ProductName")) == "zzTotal" ? "copy10link" : "copy10grey" %>' CommandArgument='<%# "8," + Eval("ProductName")%>' OnCommand="lnkDOA_OnCommand">
                    <%# Convert.ToString(Eval("BuyerRemorse")) == "0" ? "" : Convert.ToString(Eval("BuyerRemorse"))%>  </asp:LinkButton> 
                    <%--</a>--%>
                    </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Physical Abuse" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                    <ItemTemplate>
                        <div style="width:100%;  font-weight:<%# Convert.ToString(Eval("ProductName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("ProductName")) == "zzTotal"? "yellow":"" %>; height:20px">
            
                    <%--<%# Eval("OutofStock")%> --%>  
                    <%--<a class="copy10grey" href='FulfillmentsStatusReport.aspx?pos=<%# Eval("OutofStock")%>&t=<% =TimeInterval%>&cid=<%# Eval("CompanyID")%>'> --%>
                    &nbsp;
                    <asp:LinkButton ID="lnkPA" runat="server"  CssClass='<%# Convert.ToString(Eval("ProductName")) == "zzTotal" ? "copy10link" : "copy10grey" %>' CommandArgument='<%# "9," + Eval("ProductName")%>' OnCommand="lnkDOA_OnCommand">
                    <%# Convert.ToString(Eval("PhysicalAbuse")) == "0" ? "" : Convert.ToString(Eval("PhysicalAbuse"))%>   </asp:LinkButton>
                    <%--</a>--%>
                    </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Liquid Damage" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                    <ItemTemplate>
                        <div style="width:100%;  font-weight:<%# Convert.ToString(Eval("ProductName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("ProductName")) == "zzTotal"? "yellow":"" %>; height:20px">
            
                    <%--<%# Eval("Cancel")%>   --%>
                   <%-- <a class="copy10grey" href='FulfillmentsStatusReport.aspx?pos=<%# Eval("Cancel")%>&t=<% =TimeInterval%>&cid=<%# Eval("CompanyID")%>'> 
                   --%> &nbsp;
                   <asp:LinkButton ID="lnkLD" runat="server"  CssClass='<%# Convert.ToString(Eval("ProductName")) == "zzTotal" ? "copy10link" : "copy10grey" %>' CommandArgument='<%# "10," + Eval("ProductName")%>' OnCommand="lnkDOA_OnCommand">
                    <%# Convert.ToString(Eval("LiquidDamage")) == "0" ? "" : Convert.ToString(Eval("LiquidDamage"))%>   </asp:LinkButton>
                    <%--</a>--%>
                    </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Drop Calls" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                    <ItemTemplate>
                        <div style="width:100%;  font-weight:<%# Convert.ToString(Eval("ProductName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("ProductName")) == "zzTotal"? "yellow":"" %>; height:20px">
            
                    <%--<%# Eval("PartialProcessed")%>   --%>
                    <%--<a class="copy10grey" href='FulfillmentsStatusReport.aspx?pos=<%# Eval("PartialProcessed")%>&t=<% =TimeInterval%>&cid=<%# Eval("CompanyID")%>'> 
                    --%>&nbsp;
                    <asp:LinkButton ID="lnkDC" runat="server" CssClass='<%# Convert.ToString(Eval("ProductName")) == "zzTotal" ? "copy10link" : "copy10grey" %>'  CommandArgument='<%# "11," + Eval("ProductName")%>' OnCommand="lnkDOA_OnCommand">
                    <%# Convert.ToString(Eval("DropCalls")) == "0" ? "" : Convert.ToString(Eval("DropCalls"))%>   </asp:LinkButton>
                    <%--</a>--%>
                    </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Software" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                    <ItemTemplate>
                        <div style="width:100%;  font-weight:<%# Convert.ToString(Eval("ProductName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("ProductName")) == "zzTotal"? "yellow":"" %>; height:20px">
            
                    <%--<%# Eval("Cancel")%>   --%>
                   <%-- <a class="copy10grey" href='FulfillmentsStatusReport.aspx?pos=<%# Eval("Cancel")%>&t=<% =TimeInterval%>&cid=<%# Eval("CompanyID")%>'> 
                   --%> &nbsp;
                   <asp:LinkButton ID="lnkSoftware" runat="server"  CssClass='<%# Convert.ToString(Eval("ProductName")) == "zzTotal" ? "copy10link" : "copy10grey" %>' CommandArgument='<%# "12," + Eval("ProductName")%>' OnCommand="lnkDOA_OnCommand">
                    <%# Convert.ToString(Eval("Software")) == "0" ? "" : Convert.ToString(Eval("Software"))%>   </asp:LinkButton>
                    <%--</a>--%>
                    </div>
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="Activation Issues" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                    <ItemTemplate>
                        <div style="width:100%;  font-weight:<%# Convert.ToString(Eval("ProductName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("ProductName")) == "zzTotal"? "yellow":"" %>; height:20px">
            
                    <%--<%# Eval("Cancel")%>   --%>
                   <%-- <a class="copy10grey" href='FulfillmentsStatusReport.aspx?pos=<%# Eval("Cancel")%>&t=<% =TimeInterval%>&cid=<%# Eval("CompanyID")%>'> 
                   --%> &nbsp;
                   <asp:LinkButton ID="lnkACI" runat="server"  CssClass='<%# Convert.ToString(Eval("ProductName")) == "zzTotal" ? "copy10link" : "copy10grey" %>' CommandArgument='<%# "13," + Eval("ProductName")%>' OnCommand="lnkDOA_OnCommand">
                    <%# Convert.ToString(Eval("ActivationIssues")) == "0" ? "" : Convert.ToString(Eval("ActivationIssues"))%>   </asp:LinkButton>
                    <%--</a>--%>
                    </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Coverage Issues" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                    <ItemTemplate>
                        <div style="width:100%;  font-weight:<%# Convert.ToString(Eval("ProductName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("ProductName")) == "zzTotal"? "yellow":"" %>; height:20px">
            
                    <%--<%# Eval("PartialProcessed")%>   --%>
                    <%--<a class="copy10grey" href='FulfillmentsStatusReport.aspx?pos=<%# Eval("PartialProcessed")%>&t=<% =TimeInterval%>&cid=<%# Eval("CompanyID")%>'> 
                    --%>&nbsp;
                    <asp:LinkButton ID="lnkCVI" runat="server"  CssClass='<%# Convert.ToString(Eval("ProductName")) == "zzTotal" ? "copy10link" : "copy10grey" %>' CommandArgument='<%# "14," + Eval("ProductName")%>' OnCommand="lnkDOA_OnCommand">
                    <%# Convert.ToString(Eval("CoverageIssues")) == "0" ? "" : Convert.ToString(Eval("CoverageIssues"))%>   </asp:LinkButton>
                    <%--</a>--%>
                    </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Loaner Program" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                    <ItemTemplate>
                        <div style="width:100%;  font-weight:<%# Convert.ToString(Eval("ProductName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("ProductName")) == "zzTotal"? "yellow":"" %>; height:20px">
            
                    <%--<%# Eval("PartialProcessed")%>   --%>
                    <%--<a class="copy10grey" href='FulfillmentsStatusReport.aspx?pos=<%# Eval("PartialProcessed")%>&t=<% =TimeInterval%>&cid=<%# Eval("CompanyID")%>'> 
                    --%>&nbsp;
                    <asp:LinkButton ID="lnkLP" runat="server"  CssClass='<%# Convert.ToString(Eval("ProductName")) == "zzTotal" ? "copy10link" : "copy10grey" %>' CommandArgument='<%# "15," + Eval("ProductName")%>' OnCommand="lnkDOA_OnCommand">
                    <%# Convert.ToString(Eval("LoanerProgram")) == "0" ? "" : Convert.ToString(Eval("LoanerProgram"))%>   </asp:LinkButton>
                    <%--</a>--%>
                    </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Shipping Error" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                    <ItemTemplate>
                        <div style="width:100%;  font-weight:<%# Convert.ToString(Eval("ProductName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("ProductName")) == "zzTotal"? "yellow":"" %>; height:20px">
            
                    <%--<%# Eval("Cancel")%>   --%>
                   <%-- <a class="copy10grey" href='FulfillmentsStatusReport.aspx?pos=<%# Eval("Cancel")%>&t=<% =TimeInterval%>&cid=<%# Eval("CompanyID")%>'> 
                   --%> &nbsp;
                   <asp:LinkButton ID="lnkSE" runat="server"  CssClass='<%# Convert.ToString(Eval("ProductName")) == "zzTotal" ? "copy10link" : "copy10grey" %>' CommandArgument='<%# "16," + Eval("ProductName")%>' OnCommand="lnkDOA_OnCommand">
                    <%# Convert.ToString(Eval("ShippingError")) == "0" ? "" : Convert.ToString(Eval("ShippingError"))%>   </asp:LinkButton>
                    <%--</a>--%>
                    </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Hardware Issues" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                    <ItemTemplate>
                        <div style="width:100%;  font-weight:<%# Convert.ToString(Eval("ProductName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("ProductName")) == "zzTotal"? "yellow":"" %>; height:20px">
            
                    <%--<%# Eval("PartialProcessed")%>   --%>
                    <%--<a class="copy10grey" href='FulfillmentsStatusReport.aspx?pos=<%# Eval("PartialProcessed")%>&t=<% =TimeInterval%>&cid=<%# Eval("CompanyID")%>'> 
                    --%>&nbsp;
                    <asp:LinkButton ID="lnkHI" runat="server"  CssClass='<%# Convert.ToString(Eval("ProductName")) == "zzTotal" ? "copy10link" : "copy10grey" %>' CommandArgument='<%# "17," + Eval("ProductName")%>' OnCommand="lnkDOA_OnCommand">
                    <%# Convert.ToString(Eval("HardwareIssues")) == "0" ? "" : Convert.ToString(Eval("HardwareIssues"))%> </asp:LinkButton>  
                    <%--</a>--%>
                    </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Total" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ControlStyle-BackColor="Yellow" ItemStyle-Width="5%">
                    <ItemTemplate>
                        <div style="width:100%;  font-weight:<%# Convert.ToString(Eval("ProductName")) == "zzTotal"? "bold":"normal" %>; background-color:yellow; height:20px">
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

                        <%--<RMA:Status ID="rma1" runat="server" />--%>
                    </asp:Panel>
                </td>
                </tr>
      </table>
    </ContentTemplate>
        </asp:UpdatePanel>

        <div id="divRMAContainer">
			<div id="divRMAView" style="display:none">
					
				<asp:UpdatePanel ID="upnlView" runat="server">
					<ContentTemplate>
                   
						<asp:PlaceHolder ID="phrView" runat="server">
                            
                            <asp:Panel ID="pnlRMAView" runat="server">
                                <RMA:Statuses ID="rmaStatus1" runat="server" />
                            </asp:Panel>


                        </asp:PlaceHolder>
						
						
					</ContentTemplate>
							
				</asp:UpdatePanel>
           </div>
           </div>
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
<br /><br /> <br />
            <br /> <br />
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
