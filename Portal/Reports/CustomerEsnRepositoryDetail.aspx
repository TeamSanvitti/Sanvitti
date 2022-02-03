<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomerEsnRepositoryDetail.aspx.cs" Inherits="avii.Reports.CustomerEsnRepositoryDetail" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>
<%@ Register TagPrefix="menu" TagName="Menu" Src="~/Controls/Header.ascx" %>
<%@ Register TagPrefix="UC" TagName="EsnLog" Src="~/Controls/EsnLogDetails.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<title>.::  ESN Repository Detail ::.</title>
     <link href="../aerostyle.css" type="text/css" rel="stylesheet"/>

      <!-- from http://encosia.com/2009/10/11/do-you-know-about-this-undocumented-google-cdn-feature/ -->
    <link href="https://ajax.googleapis.com/ajax/libs/jqueryui/1.8.11/themes/start/jquery-ui.css" type="text/css" rel="Stylesheet" />
	<%--<link href="../CSS/jquery-ui.css" rel="stylesheet" type="text/css"/>--%>
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
	        $("#divESN").dialog({
	            autoOpen: false,
	            modal: true,
	            minHeight: 00,
	            height: 400,
	            width: 800,
	            resizable: false,
	            open: function (event, ui) {
	                $(this).parent().appendTo("#divContainer");
	            }
	        });



	    });


	    function closeDialog() {
	        //Could cause an infinite loop because of "on close handling"
	        $("#divESN").dialog('close');
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
	        $("#divESN").dialog("option", "title", title);
	        $("#divESN").dialog("option", "position", [left, top]);

	        $("#divESN").dialog('open');

	    }


	    function openDialogAndBlock(title, linkID) {

	        openDialog(title, linkID);
	        //alert('2')
	        //block it to clean out the data
	        $("#divESN").block({
	            message: '<img src="../images/async.gif" />',
	            css: { border: '0px' },
	            fadeIn: 0,
	            //fadeOut: 0,
	            overlayCSS: { backgroundColor: '#ffffff', opacity: 1 }
	        });
	    }


	    function unblockDialog() {
	        $("#divESN").unblock();
	    }


	</script>

	
     	<script type="text/javascript">
     	    function CallPrint(strid) {
     	        var prtContent = document.getElementById(strid);
     	        var WinPrint = window.open('', '', 'letf=0,top=0,width=1024,height=690,toolbar=0,scrollbars=0,status=0');
     	        WinPrint.document.write('<link href="../aerostyle.css" type="text/css" rel="stylesheet" />');
     	        WinPrint.document.write(prtContent.innerHTML);
     	        WinPrint.document.close();
     	        WinPrint.focus();
     	        WinPrint.print();
     	        WinPrint.close();
     	        prtContent.innerHTML = strOldOne;


     	    }
     	    function Validate() {
     	        //if (flag == '1' || flag == '2') {
     	        var company = document.getElementById("<% =dpCompany.ClientID %>");
     	        //alert(company);
     	        if (company != 'null' && company.selectedIndex == 0) {
     	            alert('Customer is required!');
     	            return false;

     	        }
     	        //}
     	    }
        </script>
        <link rel="stylesheet"  type="text/css" href="../fullfillment/calendar/dhtmlgoodies_calendar.css" media="screen" />
		<script type="text/javascript" src="../fullfillment/calendar/dhtmlgoodies_calendar.js"></script>  
	
        <script type="text/javascript">

            function set_focus1() {
                var img = document.getElementById("imgFromtDate");
                var st = document.getElementById("btnSearch");
                st.focus();
                img.click();
            }
            function set_focus2() {
                var img = document.getElementById("imgToDate");
                var st = document.getElementById("btnSearch");
                st.focus();
                img.click();
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
    <table align="center" style="text-align:left" width="95%">
    <tr class="buttonlabel" align="left">
        <td>&nbsp;ESN Repository Detail
        </td>
    </tr>
    </table>
     
      <asp:ScriptManager ID="ScriptManager1" runat="server">
      </asp:ScriptManager>
      <div id="divContainer">	
			<div id="divESN" style="display:none">
					
				<asp:UpdatePanel ID="upnlESN" runat="server">
					<ContentTemplate>
                   
						<asp:PlaceHolder ID="phrESN" runat="server">
                            <asp:Panel ID="pnlESN" runat="server" Width="100%">


                                
                                        <UC:EsnLog ID="esn1" runat="server" />
                                    
                            </asp:Panel>
                            </asp:PlaceHolder>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            
     </div>

    <asp:UpdatePanel ID="UpdatePanel1"  runat="server"  UpdateMode="Conditional" >
    
     <ContentTemplate>
     <table  align="center" style="text-align:left" width="95%" cellSpacing="0" cellPadding="0">
     <tr>
        <td align="left">
           <asp:Label ID="lblMsg" runat="server" CssClass="errormessage"></asp:Label>
            
        </td>
     </tr>
     <tr>
        <td align="left" class="copy10grey">
           
            &nbsp;- Maximum of last one year records will be shown if not From and To dates are given. <br />
            &nbsp;- Unused ESN count equal to new plus approved RMA ESN.
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
                <td width="35%" align="left" >
                &nbsp;<asp:DropDownList ID="dpCompany" runat="server" AutoPostBack="true" OnSelectedIndexChanged="dpCompany_SelectedIndexChanged"  Width="80%"
                 class="copy10grey"  >
                </asp:DropDownList>  
                <asp:Label ID="lblCompany" runat="server" CssClass="copy10grey" ></asp:Label>
                </td>
                <td  width="1%">
                    &nbsp;
                </td>
                <td class="copy10grey" align="right" width="15%">
                <asp:Label ID="lblSKU" runat="server" CssClass="copy10grey" Text="SKU:" ></asp:Label>
                    
                </td>
                <td width="35%"  align="left" >
                 &nbsp;<asp:DropDownList ID="ddlSKU" runat="server" class="copy10grey" Width="80%">
                                </asp:DropDownList>
          
                
          
                </td>   
                
                    
                </tr>
                
                <tr>
                <td class="copy10grey" align="right" width="15%">
                   Receive From Date:
                
                
                </td>
                <td align="left" width="35%">
                    &nbsp;<asp:TextBox ID="txtFromDate" runat="server" onfocus="set_focus1();"  CssClass="copy10grey" MaxLength="15"  Width="80%"></asp:TextBox>
                    <img id="imgFromtDate" alt="" onclick="document.getElementById('<%=txtFromDate.ClientID%>').value = ''; displayCalendar(document.getElementById('<%=txtFromDate.ClientID%>'),'mm/dd/yyyy',this,true);" src="../fullfillment/calendar/sscalendar.jpg" />
                </td>
                <td  width="1%">
                    &nbsp;
                </td>
                    <td class="copy10grey" align="right" width="15%">Receive To:</td>
                
                <td align="left" width="35%">
                    &nbsp;<asp:TextBox ID="txtToDate" runat="server" onfocus="set_focus2();"  CssClass="copy10grey" MaxLength="15"  Width="80%"></asp:TextBox>
                    <img id="imgToDate"  alt="" onclick="document.getElementById('<%=txtToDate.ClientID%>').value = ''; displayCalendar(document.getElementById('<%=txtToDate.ClientID%>'),'mm/dd/yyyy',this,true);" src="../fullfillment/calendar/sscalendar.jpg" />
                
                </td>               
                </tr>
                <tr valign="top">
                <td class="copy10grey" align="right" width="15%">
                    
                    Unused ESN only:
                </td>
                <td width="35%"  align="left" >
                <asp:CheckBox ID="chkUnusedESN" runat="server" />
                </td>
                <td  width="1%">
                    &nbsp;
                </td>
                <td class="copy10grey" align="right" width="15%">
                    <%--Show all unused ESN:--%>
                </td>
                <td width="35%"  align="left" >

                <asp:CheckBox ID="chkAllUnusedESN" Visible="false" runat="server" />
                  
                </td>   
                
                    
                </tr>
            
                <%--<tr valign="top">
                <td class="copy10grey" align="right" width="15%">
                    
                </td>
                <td width="35%">
                    
                </td>
                <td  width="1%">
                    &nbsp;
                </td>
                <td class="copy10grey" align="right" width="10%">
                    
                </td>
                <td width="40%">
                   
                </td> 
                </tr>
--%>
                <tr>
                <td colspan="5">
                    <hr />
                </td>
                </tr>
                <tr>
                <td  align="center"  colspan="5">
                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="button"  OnClientClick="return Validate();" OnClick="btnSearch_Click" CausesValidation="false"/>
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
      <table align="center" style="text-align:left" width="95%">
      <tr>
       <td  align="center"  >
       <asp:Panel ID="pnlRMA" runat="server">

       <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td align="left">
<asp:Label ID="lblCount" CssClass="copy10grey" runat="server" Visible="true" ></asp:Label>
            </td>
            <td align="right">
            <%--<input id="btnPrint" type="button" name="btnPrint1" class="button" value=" Print" onclick="javascript:CallPrint('esn');" Runat="Server"  /> &nbsp; &nbsp;--%>
            <asp:Button ID="btnDownloadESN" runat="server" Text="Download ESN Only" CssClass="button" OnClick="btnDownloadEsn_Click"  />  
                <asp:Button ID="btnDownload" runat="server" Text=" Download " CssClass="button" OnClick="btnDownload_Click"  />  
                
            </td>
        </tr>
        <tr>
            <td colspan="2" >
        <div id="esn">
        <asp:GridView ID="gvESN" OnPageIndexChanging="gridView_PageIndexChanging"    AutoGenerateColumns="false"  
        Width="100%" ShowHeader="true"  ShowFooter="false" runat="server" GridLines="Both" 
        PageSize="40" AllowPaging="true" AllowSorting="true" OnSorting="gvESN_Sorting"  OnRowDataBound="gvESN_RowDataBound"
        >
        <RowStyle BackColor="Gainsboro" />
        <AlternatingRowStyle BackColor="white" />
        <HeaderStyle  CssClass="buttonlabel" ForeColor="white"/>
          <PagerStyle ForeColor="#636363" CssClass="copy10grey" HorizontalAlign="Left" />
          <Columns>
                <asp:TemplateField HeaderText="S.No." ItemStyle-CssClass="copy10grey"  ItemStyle-Width="1%">
                    <ItemTemplate>

                          <%# Container.DataItemIndex + 1%>
                  
                    </ItemTemplate>
                </asp:TemplateField>                 
               
                <asp:TemplateField HeaderText="ESN" SortExpression="ESN" HeaderStyle-CssClass="buttonundlinelabel"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="12%">
                    <ItemTemplate>
                        <%# Eval("ESN")%>   
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Receive Date" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="8%">
                    <ItemTemplate>
                        <%--<%# Eval("UploadDate")%>   
--%>
                        <%# Convert.ToDateTime(Eval("UploadDate")).ToShortDateString()%>    
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="SKU#" SortExpression="SKU" HeaderStyle-CssClass="buttonundlinelabel"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                    <ItemTemplate>
                        <%# Eval("SKU")%>   
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="Fulfillment#" SortExpression="FulfillmentNumber" HeaderStyle-CssClass="buttonundlinelabel"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="8%">
                    <ItemTemplate>
                        <%# Eval("FulfillmentNumber")%>   
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Fulfillment Date" SortExpression="FulfillmentDate" HeaderStyle-CssClass="buttonundlinelabel" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                    <ItemTemplate>
                        <%--<%# Eval("FulfillmentDate")%>  --%>

                        <%# Convert.ToDateTime(Eval("FulfillmentDate")).ToShortDateString() == "1/1/0001" ? "" : Convert.ToDateTime(Eval("FulfillmentDate")).ToShortDateString()%>    
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Fulfillment Status" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                    <ItemTemplate>
                        <%# Eval("FulfillmentStatus")%>   
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="SHIPpED Date"  SortExpression="ShipDate" HeaderStyle-CssClass="buttonundlinelabel"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                    <ItemTemplate>
                        <%--<%# Eval("FulfillmentDate")%>  --%>

                        <%# Convert.ToDateTime(Eval("ShipDate")).ToShortDateString() == "1/1/0001" ? "" : Convert.ToDateTime(Eval("ShipDate")).ToShortDateString()%>    
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="RMA#"  SortExpression="RmaNumber" HeaderStyle-CssClass="buttonundlinelabel"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                    <ItemTemplate>
                        <%# Eval("RmaNumber")%>   
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="RMA Date"  SortExpression="RmaDate" HeaderStyle-CssClass="buttonundlinelabel"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="8%">
                    <ItemTemplate>
                        <%# Convert.ToDateTime(Eval("RmaDate")).ToShortDateString() == "1/1/0001" ? "" : Convert.ToDateTime(Eval("RmaDate")).ToShortDateString()%>    
                        <%--<%# Convert.ToDateTime(Eval("RmaDate")).ToShortDateString() %>   --%>
                    </ItemTemplate>
                </asp:TemplateField>
                           
                <asp:TemplateField HeaderText="RMA Status" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                    <ItemTemplate>
                        <%# Eval("RmaStatus")%>   
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="RMA Esn Status" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                    <ItemTemplate>
                        <%# Eval("RmaEsnStatus")%>   
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="View Log" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                    <ItemTemplate>
                        
                        <asp:ImageButton ToolTip="View ESN Log"  CausesValidation="false" OnCommand="imgESNLog_Click" 
                        CommandArgument='<%# Eval("ESN") %>' ImageUrl="~/Images/view.png" ID="imgESN"  runat="server" />
                        
                    </ItemTemplate>
                </asp:TemplateField>
                
            </Columns>
        </asp:GridView>
        </div>
        <%--<asp:Label ID="lblRMA" runat="server" CssClass="errormessage"></asp:Label>
--%>
            </td>
        </tr>
        </table>
        </asp:Panel>
        </td>
        </tr>
      </table>
    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="btnDownload" />
        <asp:PostBackTrigger ControlID="btnDownloadESN" />
        
    </Triggers>
            
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
        <br />
        <br /> <br />
            <br /> <br />
        <table cellspacing="0" cellpadding="0" border="0" align="center" width="100%">
        <tr>
            <td >
                <foot:MenuFooter ID="footer2" runat="server"></foot:MenuFooter>
            </td>
         </tr>
         </table>
         <script type="text/javascript" src="../JQuery/jquery.min.js"></script>
    <script type="text/javascript" src="../JQuery/jquery-ui.min.js"></script>
	
	<script type="text/javascript" src="../JQuery/jquery.blockUI.js"></script>

    </form>
</body>
</html>
