<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RmaCustomerReport.aspx.cs" Inherits="avii.Reports.RmaCustomerReport" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>
<%@ Register TagPrefix="menu" TagName="Menu" Src="~/Controls/Header.ascx" %>
<%@ Register TagPrefix="RMA" TagName="Esn" Src="~/Controls/RmaESNs.ascx" %>
<%@ Register TagPrefix="RMA" TagName="EsnDetail" Src="~/Controls/RmaEsnDetail.ascx" %>
<%--
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
--%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<title>Customer wise RMA Statuses Summary</title>
     <link href="../aerostyle.css" type="text/css" rel="stylesheet"/>
      <!-- from http://encosia.com/2009/10/11/do-you-know-about-this-undocumented-google-cdn-feature/ -->
    <link href="https://ajax.googleapis.com/ajax/libs/jqueryui/1.8.11/themes/start/jquery-ui.css" type="text/css" rel="Stylesheet" />
	
	<%--<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.4.0/jquery.min.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/jquery-ui.min.js"></script>
--%>

    <script type="text/javascript" src="../JQuery/jquery.min.js"></script>
    <script type="text/javascript" src="../JQuery/jquery-ui.min.js"></script>
	
	<script type="text/javascript" src="../JQuery/jquery.blockUI.js"></script>
	
	
<!-- fix for 1.1em default font size in Jquery UI -->
	<style type="text/css">
		.ui-widget{font-size:11px !important;}
		.ui-state-error-text{margin-left: 10px}
	</style>
    
	<script type="text/javascript">

	    $(document).ready(function () {
	        $("#divRmaEsn").dialog({
	            autoOpen: false,
	            modal: true,
	            minHeight: 20,
	            height: 600,
	            width: 500,
	            resizable: false,
	            open: function (event, ui) {
	                $(this).parent().appendTo("#divRMA");
	            }
	        });

	        $("#divRmaEsnDetail").dialog({
	            autoOpen: false,
	            modal: true,
	            minHeight: 20,
	            height: 600,
	            width: 900,
	            resizable: false,
	            open: function (event, ui) {
	                $(this).parent().appendTo("#divRMA");
	            }
	        });
	    });

	    function closeDialog() {
	        //Could cause an infinite loop because of "on close handling"
	        $("#divRmaEsn").dialog('close');
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
	        left = 300;
	        $("#divRmaEsn").dialog("option", "title", title);
	        $("#divRmaEsn").dialog("option", "position", [left, top]);

	        $("#divRmaEsn").dialog('open');

	    }
	    function openDialogAndBlock(title, linkID) {
	        openDialog(title, linkID);

	        //block it to clean out the data
	        $("#divRmaEsn").block({
	            message: '<img src="../images/async.gif" />',
	            css: { border: '0px' },
	            fadeIn: 0,
	            //fadeOut: 0,
	            overlayCSS: { backgroundColor: '#ffffff', opacity: 1 }
	        });
	    }

	    function unblockDialog() {
	        $("#divRmaEsn").unblock();
	    }

	    function closedEsnDetailDialog() {
	        //Could cause an infinite loop because of "on close handling"
	        $("#divRmaEsnDetail").dialog('close');
	    }
	    function openEsnDetailDialog(title, linkID) {

	        var pos = $("#" + linkID).position();
	        var top = pos.top;
	        var left = pos.left + $("#" + linkID).width() + 10;
	        //alert(top);
	        if (top > 600)
	            top = 10;
	        top = 100;
	        //top = top - 600;
	        left = 150;
	        $("#divRmaEsnDetail").dialog("option", "title", title);
	        $("#divRmaEsnDetail").dialog("option", "position", [left, top]);

	        $("#divRmaEsnDetail").dialog('open');

	    }
	    function openEsnDetailDialogAndBlock(title, linkID) {
	        openEsnDetailDialog(title, linkID);

	        //block it to clean out the data
	        $("#divRmaEsnDetail").block({
	            message: '<img src="../images/async.gif" />',
	            css: { border: '0px' },
	            fadeIn: 0,
	            //fadeOut: 0,
	            overlayCSS: { backgroundColor: '#ffffff', opacity: 1 }
	        });
	    }

	    function unblockEsnDetailDialog() {
	        $("#divRmaEsnDetail").unblock();
	    }

	    function CallPrint(strid) {
	        var prtContent = document.getElementById(strid);
	        var WinPrint = window.open('', '', 'letf=0,top=0,width=900,height=690,toolbar=0,scrollbars=0,status=0');
	        WinPrint.document.write('<link href="../aerostyle.css" type="text/css" rel="stylesheet" />');
	        WinPrint.document.write(prtContent.innerHTML);
	        WinPrint.document.close();
	        WinPrint.focus();
	        WinPrint.print();
	        WinPrint.close();
	        prtContent.innerHTML = strOldOne;
	        
        
	    }

	    function printDiv(divName) {
	        
	        var printContents = document.getElementById(divName).innerHTML;
	        //alert(printContents);
	        var originalContents = document.body.innerHTML;
	        //alert(originalContents);
	        document.body.innerHTML = printContents;

	        window.print();
	        //window.close();
	        document.body.innerHTML = originalContents;
	        //closeDialog();
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
                <td>&nbsp;Customers wise RMA Statuses</td></tr>
             </table>
     
      <asp:ScriptManager ID="ScriptManager1" runat="server">
      </asp:ScriptManager>
    
    <div id="divRMA">
			<div id="divRmaEsn" style="display:none">
            <input id="Button1" type="button" name="btnPrint1" class="button" value=" Print" onclick="javascript:CallPrint('esn');" Runat="Server"  />
					<%--<input type="button" class="button" onclick="printDiv('esn')" value="print 2" />

    <input type="button" class="button" onclick="closeDialog()" value="close" />
--%>
    
                            <div id="esn" class="copy10grey">
    
				<asp:UpdatePanel ID="upnlView" runat="server">
					<ContentTemplate>
                   
						<asp:PlaceHolder ID="phrView" runat="server">
                            
                            <asp:Panel ID="pnlESN" runat="server">
                                <RMA:Esn ID="esn1" runat="server" />

                            </asp:Panel>
                        </asp:PlaceHolder>
					</ContentTemplate>
				</asp:UpdatePanel>
           </div>
           </div>
            <div id="divRmaEsnDetail" style="display:none">
                <asp:UpdatePanel ID="upEsnDetail" runat="server">
					<ContentTemplate>
                   
						<asp:PlaceHolder ID="phESNDetail" runat="server">
                            
                            <asp:Panel ID="pnlEsnDetail" runat="server">
                                <RMA:EsnDetail ID="esnd2" runat="server" />

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
                    Duration:
                </td>
                <td width="40%">
                   <asp:DropDownList ID="ddlDuration" AutoPostBack="false"  runat="server" Class="copy10grey" Width="135px" >
                                <asp:ListItem  Value="1"  >Today</asp:ListItem>
                                <asp:ListItem  Value="7"  >One week</asp:ListItem>
                                <asp:ListItem  Value="15"  >Two week</asp:ListItem>
                                <asp:ListItem  Value="30" Selected="True" >Last Month</asp:ListItem>
                                <asp:ListItem Value="90" >Quaterly</asp:ListItem>
                                <asp:ListItem  Value="180">6 Months</asp:ListItem>
                                <asp:ListItem  Value="365">One Year</asp:ListItem>
                    </asp:DropDownList>
                    <br />
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

                        

       <table width="125%" cellpadding="0" cellspacing="0">
        <tr>
            <td align="left">
                <asp:Label ID="lblCount" CssClass="copy10grey" runat="server" ></asp:Label>
            </td>
        </tr>
        <tr>
            <td >
    
        <asp:GridView ID="gvRMA" OnPageIndexChanging="gridView_PageIndexChanging"    AutoGenerateColumns="false"  
        Width="100%" ShowHeader="true"  ShowFooter="false" runat="server" GridLines="Both" 
        PageSize="50" AllowPaging="true" AllowSorting="false"  OnRowDataBound="gvRMA_RowDataBound"
        >
        <RowStyle BackColor="Gainsboro" />
        <AlternatingRowStyle BackColor="white" />
        <HeaderStyle  CssClass="buttongrid" ForeColor="white"/>
          <PagerStyle ForeColor="#636363" CssClass="copy10grey" HorizontalAlign="Left" />
          <Columns>
                <asp:TemplateField HeaderText="S.No." ItemStyle-CssClass="copy10grey"  ItemStyle-Width="1%">
                    <ItemTemplate>

                          <%# Convert.ToString(Eval("CustomerName")) == "zzTotal" ? "" : Convert.ToString(Container.DataItemIndex + 1)%>
                  
                    </ItemTemplate>
                </asp:TemplateField>                 
        
                <asp:TemplateField HeaderText="Customer Name" ItemStyle-CssClass="copy10grey"  ItemStyle-Width="10%"  ItemStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                    <div style="width:100%; font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
                    &nbsp;
                         
                 
                        <asp:LinkButton ID="lnkCustomer" CommandArgument='<%# Eval("CustomerName") %>' OnCommand="lnkCustomer_OnCommand" runat="server">
                                    <%# Convert.ToString(Eval("CustomerName")) == "zzTotal" ? "Total" : Eval("CustomerName")%>
                                    </asp:LinkButton>
                    </div>
                    </ItemTemplate>
                </asp:TemplateField>                 
                
                <asp:TemplateField HeaderText="Pending" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
                    <ItemTemplate>
                    <div style="width:100%; font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
                    &nbsp;
                       <%--<a class="copy10grey" href='FulfillmentsStatusReport.aspx?pos=<%# Eval("Pending")%>&t=<% =TimeInterval%>&cid=<%# Eval("CompanyID")%>'> --%>
                    
                    <asp:LinkButton ID="lnkRma1" CommandArgument='<%# Eval("CustomerName") + "|1"%>' OnCommand="lnkRMA_OnCommand" runat="server">
                                    <%# Convert.ToString(Eval("Pending")) == "0" ? "" : Convert.ToString(Eval("Pending"))%>   
                                    </asp:LinkButton>
                                    
                    <%--</a>--%>
            
                    </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Received" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
                    <ItemTemplate>
                        <div style="width:100%;  font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
                    &nbsp;
                    <%--<%# Eval("InProcess")%>   --%>
                    <%--<a class="copy10grey" href='FulfillmentsStatusReport.aspx?pos=<%# Eval("InProcess")%>&t=<% =TimeInterval%>&cid=<%# Eval("CompanyID")%>'> --%>
                    <asp:LinkButton ID="lnkRma2" CommandArgument='<%# Eval("CustomerName") + "|2"%>' OnCommand="lnkRMA_OnCommand" runat="server">
                    
                    <%# Convert.ToString(Eval("Received")) == "0" ? "" : Convert.ToString(Eval("Received"))%>
                    </asp:LinkButton>
                    <%--</a>--%>
            
                    </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Pending for Repair" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
                    <ItemTemplate>
                        <div style="width:100%;  font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
                    &nbsp;
                    <%--<%# Eval("Processed")%>   --%>
                    <%--<a class="copy10grey" href='FulfillmentsStatusReport.aspx?pos=<%# Eval("Processed")%>&t=<% =TimeInterval%>&cid=<%# Eval("CompanyID")%>'> --%>
                    <asp:LinkButton ID="lnkRma3" CommandArgument='<%# Eval("CustomerName") + "|3"%>' OnCommand="lnkRMA_OnCommand" runat="server">
                    
                    <%# Convert.ToString(Eval("PendingforRepair")) == "0" ? "" : Convert.ToString(Eval("PendingforRepair"))%>
                    </asp:LinkButton>
                    <%--</a>   --%>
            
                    </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Pending for Credit" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
                    <ItemTemplate>
                        <div style="width:100%;  font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
                    &nbsp;
                    <%--<%# Eval("Shipped")%>   --%>
                    <%--<a class="copy10grey" href='FulfillmentsStatusReport.aspx?pos=<%# Eval("Shipped")%>&t=<% =TimeInterval%>&cid=<%# Eval("CompanyID")%>'> --%>
                    <asp:LinkButton ID="lnkRma4" CommandArgument='<%# Eval("CustomerName") + "|4"%>' OnCommand="lnkRMA_OnCommand" runat="server">
                    
                    <%# Convert.ToString(Eval("PendingforCredit")) == "0" ? "" : Convert.ToString(Eval("PendingforCredit"))%>   
                    </asp:LinkButton>
                    <%--</a>--%>

            
                    </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Pending for Replacement" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
                    <ItemTemplate>
                        <div style="width:100%;  font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
                    &nbsp;
                    <%--<%# Eval("Closed")%>   --%>
                    <%--<a class="copy10grey" href='FulfillmentsStatusReport.aspx?pos=<%# Eval("Closed")%>&t=<% =TimeInterval%>&cid=<%# Eval("CompanyID")%>'> --%>
            <asp:LinkButton ID="lnkRma5" CommandArgument='<%# Eval("CustomerName") + "|5"%>' OnCommand="lnkRMA_OnCommand" runat="server">
                    
                    <%# Convert.ToString(Eval("PendingforReplacement")) == "0" ? "" : Convert.ToString(Eval("PendingforReplacement"))%>   
                    </asp:LinkButton>
                    <%--</a>--%>
            
                    </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Approved" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
                    <ItemTemplate>
                        <div style="width:100%;  font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
                    &nbsp;
                    <%--<%# Eval("Return")%>   --%>
                    <%--<a class="copy10grey" href='FulfillmentsStatusReport.aspx?pos=<%# Eval("Return")%>&t=<% =TimeInterval%>&cid=<%# Eval("CompanyID")%>'> 
                    --%>
                    <asp:LinkButton ID="lnkRma6" CommandArgument='<%# Eval("CustomerName") + "|6"%>' OnCommand="lnkRMA_OnCommand" runat="server">
                    
                    <%# Convert.ToString(Eval("Approved")) == "0" ? "" : Convert.ToString(Eval("Approved"))%>  
                    </asp:LinkButton> 
                    <%--</a>--%>
            
                    </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Returned" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
                    <ItemTemplate>
                        <div style="width:100%;  font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
                    &nbsp;
                    <%--<%# Eval("OnHold")%>   --%>
                    <%--<a class="copy10grey" href='FulfillmentsStatusReport.aspx?pos=<%# Eval("OnHold")%>&t=<% =TimeInterval%>&cid=<%# Eval("CompanyID")%>'> 
                    --%>
                    <asp:LinkButton ID="lnkRma7" CommandArgument='<%# Eval("CustomerName") + "|7"%>' OnCommand="lnkRMA_OnCommand" runat="server">
                    
                    <%# Convert.ToString(Eval("Returned")) == "0" ? "" : Convert.ToString(Eval("Returned"))%>   
                    </asp:LinkButton>
                    <%--</a>--%>
            
                    </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Credited" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
                    <ItemTemplate>
                        <div style="width:100%; font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>;  background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
                    &nbsp;
                    <%--<%# Eval("OutofStock")%> --%>  
                    <%--<a class="copy10grey" href='FulfillmentsStatusReport.aspx?pos=<%# Eval("OutofStock")%>&t=<% =TimeInterval%>&cid=<%# Eval("CompanyID")%>'> --%>
                    <asp:LinkButton ID="lnkRma8" CommandArgument='<%# Eval("CustomerName") + "|8"%>' OnCommand="lnkRMA_OnCommand" runat="server">
                    
                    <%# Convert.ToString(Eval("Credited")) == "0" ? "" : Convert.ToString(Eval("Credited"))%>  
                    </asp:LinkButton> 
                    <%--</a>--%>
            
                    </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Denied" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
                    <ItemTemplate>
                        <div style="width:100%; font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>;  background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
                        &nbsp;
                        <%--<%# Eval("Cancel")%>   --%>
                        <%-- <a class="copy10grey" href='FulfillmentsStatusReport.aspx?pos=<%# Eval("Cancel")%>&t=<% =TimeInterval%>&cid=<%# Eval("CompanyID")%>'> 
                        --%> 
                       <asp:LinkButton ID="lnkRma9" CommandArgument='<%# Eval("CustomerName") + "|9"%>' OnCommand="lnkRMA_OnCommand" runat="server">
                    
                        <%# Convert.ToString(Eval("Denied")) == "0" ? "" : Convert.ToString(Eval("Denied"))%>  
                        </asp:LinkButton> 
                        <%--</a>--%>
            
                    </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Closed" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
                    <ItemTemplate>
                        <div style="width:100%; font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>;  background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
                    &nbsp;
                    <%--<%# Eval("PartialProcessed")%>   --%>
                    <%--<a class="copy10grey" href='FulfillmentsStatusReport.aspx?pos=<%# Eval("PartialProcessed")%>&t=<% =TimeInterval%>&cid=<%# Eval("CompanyID")%>'> 
                    --%>
                    <asp:LinkButton ID="lnkRma10" CommandArgument='<%# Eval("CustomerName") + "|10"%>' OnCommand="lnkRMA_OnCommand" runat="server">
                    
                    <%# Convert.ToString(Eval("Closed")) == "0" ? "" : Convert.ToString(Eval("Closed"))%> 
                    </asp:LinkButton>  
                    <%--</a>--%>
            
                    </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Out with OEM for repair" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
                    <ItemTemplate>
                        <div style="width:100%; font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>;  background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
                        &nbsp;
                        <asp:LinkButton ID="lnkRma11" CommandArgument='<%# Eval("CustomerName") + "|11"%>' OnCommand="lnkRMA_OnCommand" runat="server">
                    
                        <%# Convert.ToString(Eval("OutwithOEMforrepair")) == "0" ? "" : Convert.ToString(Eval("Out with OEM for repair"))%>   
            
                        </asp:LinkButton>
                    </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Back to Stock -NDF" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
                    <ItemTemplate>
                        <div style="width:100%;  font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
                    &nbsp;
                    <asp:LinkButton ID="lnkRma12" CommandArgument='<%# Eval("CustomerName") + "|12"%>' OnCommand="lnkRMA_OnCommand" runat="server">
                    
                    <%# Convert.ToString(Eval("BacktoStockNDF")) == "0" ? "" : Convert.ToString(Eval("BacktoStockNDF"))%>   
                    </asp:LinkButton>
                    </div>

                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Back to Stock- Credited" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
                    <ItemTemplate>
                        <div style="width:100%;  font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
                    &nbsp;
                    <asp:LinkButton ID="lnkRma13" CommandArgument='<%# Eval("CustomerName") + "|13"%>' OnCommand="lnkRMA_OnCommand" runat="server">
                    
                    <%# Convert.ToString(Eval("BacktoStockCredited")) == "0" ? "" : Convert.ToString(Eval("BacktoStockCredited"))%>   
                    </asp:LinkButton>
                    </div>

                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Back to Stock – Replaced by OEM" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
                    <ItemTemplate>
                        <div style="width:100%;  font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
                    &nbsp;
                    <asp:LinkButton ID="lnkRma14" CommandArgument='<%# Eval("CustomerName") + "|14"%>' OnCommand="lnkRMA_OnCommand" runat="server">
                    
                    <%# Convert.ToString(Eval("BacktoStockReplacedbyOEM")) == "0" ? "" : Convert.ToString(Eval("BacktoStockReplacedbyOEM"))%>   
                    </asp:LinkButton>
                    </div>

                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Repaired by OEM" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
                    <ItemTemplate>
                        <div style="width:100%;  font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
                    &nbsp;
                    <asp:LinkButton ID="lnkRma15" CommandArgument='<%# Eval("CustomerName") + "|15"%>' OnCommand="lnkRMA_OnCommand" runat="server">
                    
                    <%# Convert.ToString(Eval("RepairedbyOEM")) == "0" ? "" : Convert.ToString(Eval("RepairedbyOEM"))%>   
                    </asp:LinkButton>
            
                    </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Replaced BY OEM" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
                    <ItemTemplate>
                        <div style="width:100%;  font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
                    &nbsp;
                    <asp:LinkButton ID="lnkRma16" CommandArgument='<%# Eval("CustomerName") + "|16"%>' OnCommand="lnkRMA_OnCommand" runat="server">
                    
                    <%# Convert.ToString(Eval("ReplacedBYOEM")) == "0" ? "" : Convert.ToString(Eval("ReplacedBYOEM"))%>   
                    </asp:LinkButton>
                    </div>

                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Replaced BY AV" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
                    <ItemTemplate>
                        <div style="width:100%;  font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
                    &nbsp;
                    <asp:LinkButton ID="lnkRma17" CommandArgument='<%# Eval("CustomerName") + "|17"%>' OnCommand="lnkRMA_OnCommand" runat="server">
                    
                    <%# Convert.ToString(Eval("ReplacedBYAV")) == "0" ? "" : Convert.ToString(Eval("ReplacedBYAV"))%>   
                    </asp:LinkButton>
            
                    </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Repaired By AV" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
                    <ItemTemplate>
                        <div style="width:100%;  font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
                    &nbsp;
                    <asp:LinkButton ID="lnkRma18" CommandArgument='<%# Eval("CustomerName") + "|18"%>' OnCommand="lnkRMA_OnCommand" runat="server">
                    
                    <%# Convert.ToString(Eval("RepairedByAV")) == "0" ? "" : Convert.ToString(Eval("RepairedByAV"))%>   
                    </asp:LinkButton>
            
                    </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="NDF (No Defect Found)" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
                    <ItemTemplate>
                        <div style="width:100%; font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>;  background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
                    &nbsp;
                    <asp:LinkButton ID="lnkRma19" CommandArgument='<%# Eval("CustomerName") + "|19"%>' OnCommand="lnkRMA_OnCommand" runat="server">
                    
                    <%# Convert.ToString(Eval("NDFNoDefectFound")) == "0" ? "" : Convert.ToString(Eval("NDFNoDefectFound"))%>   
                    </asp:LinkButton>
                    </div>

                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="PRE-OWNED – A stock" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
                    <ItemTemplate>
                        <div style="width:100%;  font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
                    &nbsp;
                    <asp:LinkButton ID="lnkRma20" CommandArgument='<%# Eval("CustomerName") + "|20"%>' OnCommand="lnkRMA_OnCommand" runat="server">
                    
                    <%# Convert.ToString(Eval("PREOWNEDAstock")) == "0" ? "" : Convert.ToString(Eval("PREOWNEDAstock"))%>   
                    </asp:LinkButton>
                    </div>

                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="PRE-OWEND - B Stock" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
                    <ItemTemplate>
                        <div style="width:100%;  font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
                    &nbsp;
                    <asp:LinkButton ID="lnkRma21" CommandArgument='<%# Eval("CustomerName") + "|21"%>' OnCommand="lnkRMA_OnCommand" runat="server">
                    
                    <%# Convert.ToString(Eval("PREOWENDBStock")) == "0" ? "" : Convert.ToString(Eval("PREOWENDBStock"))%>   
                    </asp:LinkButton>
                    </div>

                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="PRE-OWEND – C Stock" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
                    <ItemTemplate>
                        <div style="width:100%;  font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
                        &nbsp;
                        <asp:LinkButton ID="lnkRma22" CommandArgument='<%# Eval("CustomerName") + "|22"%>' OnCommand="lnkRMA_OnCommand" runat="server">
                    
                    <%# Convert.ToString(Eval("PREOWENDCStock")) == "0" ? "" : Convert.ToString(Eval("PREOWENDCStock"))%>   
                    </asp:LinkButton>
            
                    </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Rejected" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
                    <ItemTemplate>
                        <div style="width:100%; font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>;  background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
                    &nbsp;
                    <asp:LinkButton ID="lnkRma23" CommandArgument='<%# Eval("CustomerName") + "|23"%>' OnCommand="lnkRMA_OnCommand" runat="server">
                    
                    <%# Convert.ToString(Eval("Rejected")) == "0" ? "" : Convert.ToString(Eval("Rejected"))%>   
                    </asp:LinkButton>
            
                    </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="RTS (Return To Stock)" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
                    <ItemTemplate>
                        <div style="width:100%; font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>;  background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
                    &nbsp;
                    <asp:LinkButton ID="lnkRma24" CommandArgument='<%# Eval("CustomerName") + "|24"%>' OnCommand="lnkRMA_OnCommand" runat="server">
                    
                    <%# Convert.ToString(Eval("RTSReturnToStock")) == "0" ? "" : Convert.ToString(Eval("RTSReturnToStock"))%>   
                        </asp:LinkButton>
                    </div>

                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Incomplete" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
                    <ItemTemplate>
                        <div style="width:100%; font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>;  background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
                    &nbsp;
                    <asp:LinkButton ID="lnkRma25" CommandArgument='<%# Eval("CustomerName") + "|25"%>' OnCommand="lnkRMA_OnCommand" runat="server">
                    
                    <%# Convert.ToString(Eval("Incomplete")) == "0" ? "" : Convert.ToString(Eval("Incomplete"))%>   
                    </asp:LinkButton>
                    </div>

                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Damaged" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
                    <ItemTemplate>
                        <div style="width:100%; font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>;  background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
                    &nbsp;
                    <asp:LinkButton ID="lnkRma26" CommandArgument='<%# Eval("CustomerName") + "|26"%>' OnCommand="lnkRMA_OnCommand" runat="server">
                    
                    <%# Convert.ToString(Eval("Damaged")) == "0" ? "" : Convert.ToString(Eval("Damaged"))%>   
                    </asp:LinkButton>
                    </div>

                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Preowned" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
                    <ItemTemplate>
                        <div style="width:100%;  font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
                    &nbsp;
                    <asp:LinkButton ID="lnkRma27" CommandArgument='<%# Eval("CustomerName") + "|27"%>' OnCommand="lnkRMA_OnCommand" runat="server">
                    
                    <%# Convert.ToString(Eval("Preowned")) == "0" ? "" : Convert.ToString(Eval("Preowned"))%>   
                    </asp:LinkButton>
                    </div>

                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Return to OEM" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
                    <ItemTemplate>
                        <div style="width:100%;  font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
                    &nbsp;
                    <asp:LinkButton ID="lnkRma28" CommandArgument='<%# Eval("CustomerName") + "|28"%>' OnCommand="lnkRMA_OnCommand" runat="server">
                    
                    <%# Convert.ToString(Eval("ReturntoOEM")) == "0" ? "" : Convert.ToString(Eval("ReturntoOEM"))%>   
                    </asp:LinkButton>
                    </div>

                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Returned to Stock" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
                    <ItemTemplate>
                        <div style="width:100%; font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>;  background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
                    &nbsp;
                    <asp:LinkButton ID="lnkRma29" CommandArgument='<%# Eval("CustomerName") + "|29"%>' OnCommand="lnkRMA_OnCommand" runat="server">
                    
                    <%# Convert.ToString(Eval("ReturnedtoStock")) == "0" ? "" : Convert.ToString(Eval("ReturnedtoStock"))%>   
                    </asp:LinkButton>
            
                    </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Cancel" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
                    <ItemTemplate>
                        <div style="width:100%; font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>;  background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
                    &nbsp;
                    <asp:LinkButton ID="lnkRma30" CommandArgument='<%# Eval("CustomerName") + "|30"%>' OnCommand="lnkRMA_OnCommand" runat="server">
                    
                    <%# Convert.ToString(Eval("Cancel")) == "0" ? "" : Convert.ToString(Eval("Cancel"))%>   
                    </asp:LinkButton>
                    </div>

                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="External ESN" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
                    <ItemTemplate>
                        <div style="width:100%; font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>;  background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
                    &nbsp;
                    <asp:LinkButton ID="lnkRma31" CommandArgument='<%# Eval("CustomerName") + "|31"%>' OnCommand="lnkRMA_OnCommand" runat="server">
                    
                    <%# Convert.ToString(Eval("ExternalESN")) == "0" ? "" : Convert.ToString(Eval("ExternalESN"))%>   
                    </asp:LinkButton>
            
                    </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Pending ship to OEM" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
                    <ItemTemplate>
                        <div style="width:100%;  font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
                    &nbsp;
                    <asp:LinkButton ID="lnkRma32" CommandArgument='<%# Eval("CustomerName") + "|32"%>' OnCommand="lnkRMA_OnCommand" runat="server">
                    
                    <%# Convert.ToString(Eval("PendingshiptoOEM")) == "0" ? "" : Convert.ToString(Eval("PendingshiptoOEM"))%>   
                    </asp:LinkButton>
            
                    </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Sent to OEM" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
                    <ItemTemplate>
                        <div style="width:100%; font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>;  background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
                    &nbsp;
                    <asp:LinkButton ID="lnkRma33" CommandArgument='<%# Eval("CustomerName") + "|33"%>' OnCommand="lnkRMA_OnCommand" runat="server">
                    
                    <%# Convert.ToString(Eval("SenttoOEM")) == "0" ? "" : Convert.ToString(Eval("SenttoOEM"))%>   
                    </asp:LinkButton>
            
                    </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Pending ship to Supplier" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
                    <ItemTemplate>
                        <div style="width:100%;  font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
                    &nbsp;
                    <asp:LinkButton ID="lnkRma34" CommandArgument='<%# Eval("CustomerName") + "|34"%>' OnCommand="lnkRMA_OnCommand" runat="server">
                    
                    <%# Convert.ToString(Eval("PendingshiptoSupplier")) == "0" ? "" : Convert.ToString(Eval("PendingshiptoSupplier"))%>   
                    </asp:LinkButton>
            
                    </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Sent to Supplier" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
                    <ItemTemplate>
                        <div style="width:100%; font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>;  background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
                    &nbsp;
                    <asp:LinkButton ID="lnkRma35" CommandArgument='<%# Eval("CustomerName") + "|35"%>' OnCommand="lnkRMA_OnCommand" runat="server">
                    
                    <%# Convert.ToString(Eval("SenttoSupplier")) == "0" ? "" : Convert.ToString(Eval("SenttoSupplier"))%>   
                    </asp:LinkButton>
            
                    </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Returned from OEM" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
                    <ItemTemplate>
                        <div style="width:100%;  font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
                    &nbsp;
                    <asp:LinkButton ID="lnkRma36" CommandArgument='<%# Eval("CustomerName") + "|36"%>' OnCommand="lnkRMA_OnCommand" runat="server">
                    
                    <%# Convert.ToString(Eval("ReturnedfromOEM")) == "0" ? "" : Convert.ToString(Eval("ReturnedfromOEM"))%>   
                    </asp:LinkButton>
                    </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Total" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="copy10grey" ControlStyle-BackColor="Yellow" ItemStyle-Width="2%">
                    <ItemTemplate>
                        <div style="width:100%;  font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>;  background-color:yellow; height:20px">
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
        <asp:Label ID="lblRMA" runat="server" CssClass="errormessage"></asp:Label>

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
