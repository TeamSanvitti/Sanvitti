<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomerRmaEsnSummary.aspx.cs" Inherits="avii.Reports.CustomerRmaEsnSummary" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>
<%@ Register TagPrefix="menu" TagName="Menu" Src="~/Controls/Header.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

<title>.:: Lan Global Inc. -  RMA ESN Listing ::.</title>
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
	

     <link href="../aerostyle.css" type="text/css" rel="stylesheet"/>
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


	        function set_focus1() {
	            var img = document.getElementById("imgFromtDate");
	            var st = document.getElementById("ddlEsnStatus");
	            st.focus();
	            img.click();
	        }
	        function set_focus2() {
	            var img = document.getElementById("imgToDate");
	            var st = document.getElementById("ddlEsnStatus");
	            st.focus();
	            img.click();
	        }

        </script>
        <link rel="stylesheet"  type="text/css" href="../fullfillment/calendar/dhtmlgoodies_calendar.css" media="screen" />
		<script type="text/javascript" src="../fullfillment/calendar/dhtmlgoodies_calendar.js"></script>  
		

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
                <td>&nbsp;RMA  Report</td></tr>
             </table>
     
      <asp:ScriptManager ID="ScriptManager1" runat="server">
      </asp:ScriptManager>
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
     <table width="100%" border="0" class="box" align="center" cellpadding="4" cellspacing="4">
         
           
            <tr valign="top">
                <td class="copy10grey" align="right" width="15%">
                    Customer Name:
                </td>
                <td width="35%">
                <asp:DropDownList ID="dpCompany" runat="server" AutoPostBack="false"  Width="80%"
                 class="copy10grey"  >
                </asp:DropDownList>  
                <asp:Label ID="lblCompany" runat="server" CssClass="copy10grey" ></asp:Label>
                </td>
                <td  width="1%">
                    &nbsp;
                </td>
                <td class="copy10grey" align="right" width="10%">
                    <%--Duration:--%>
                </td>

                <td width="40%">
                    
                   <asp:DropDownList ID="ddlDuration" Visible="false" AutoPostBack="false"  runat="server" Class="copy10grey" Width="166px" >
                                <asp:ListItem  Value="1"  >Today</asp:ListItem>
                                <asp:ListItem  Value="7"  >One week</asp:ListItem>
                                <asp:ListItem  Value="15"  >Two week</asp:ListItem>
                                <asp:ListItem  Value="30" Selected="True" >Last Month</asp:ListItem>
                                <asp:ListItem Value="90" >Quaterly</asp:ListItem>
                                <asp:ListItem  Value="180">6 Months</asp:ListItem>
                                <asp:ListItem  Value="365">One Year</asp:ListItem>
                    </asp:DropDownList>
                    <%--<br />--%>
                    <asp:Label ID="lblDuration" Visible="false" runat="server" CssClass="copy10grey" ></asp:Label>
        
                    <%--<br />
                    <br />--%>
                </td>   
                
                    
                </tr>

                <tr>
                <td class="copy10grey" align="right" width="15%">
                    From Date:
                
                
                </td>
                <td align="left" width="35%">
                    <asp:TextBox ID="txtFromDate" runat="server" onfocus="set_focus1();"  CssClass="copy10grey" MaxLength="15"  Width="80%"></asp:TextBox>
                    <img id="imgFromtDate" alt="" onclick="document.getElementById('<%=txtFromDate.ClientID%>').value = ''; displayCalendar(document.getElementById('<%=txtFromDate.ClientID%>'),'mm/dd/yyyy',this,true);" src="../fullfillment/calendar/sscalendar.jpg" />
                </td>
                <td  width="1%">
                    &nbsp;
                </td>
                    <td class="copy10grey" align="right" width="10%">Date To:</td>
                
                <td align="left" width="40%">
                    <asp:TextBox ID="txtToDate" runat="server" onfocus="set_focus2();"  CssClass="copy10grey" MaxLength="15"  Width="45%"></asp:TextBox>
                    <img id="imgToDate"  alt="" onclick="document.getElementById('<%=txtToDate.ClientID%>').value = ''; displayCalendar(document.getElementById('<%=txtToDate.ClientID%>'),'mm/dd/yyyy',this,true);" src="../fullfillment/calendar/sscalendar.jpg" />
                
                </td>               
                </tr>
                <tr>
                <td class="copy10grey" align="right" width="15%">
                    ESN Status:
                </td>
                <td width="35%">
                
                    <asp:DropDownList ID="ddlEsnStatus"  runat="server" Class="copy10grey" 
                                                Width="80%" >
                                                        
                                                        
                                            </asp:DropDownList>
                
                </td>
                <td  width="1%">
                    &nbsp;
                </td>
                <td class="copy10grey" align="right" width="10%">
                    RMA Status:
                </td>
                <td width="40%">
                    <asp:DropDownList ID="ddlRmaStaus"  runat="server" Class="copy10grey" 
                                                Width="46%" >
                                                        
                                            </asp:DropDownList>
                
                </td>
                </tr>
                <tr>
                <td colspan="5">
                    <hr />
                </td>
                </tr>
                <tr>
                <td  align="center"  colspan="5">
                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="button" OnClientClick="return Validate();" OnClick="btnSearch_Click" CausesValidation="false"/>
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
            <td align="right">
            <%--<input id="btnPrint" type="button" name="btnPrint1" class="button" value=" Print" onclick="javascript:CallPrint('esn');" Runat="Server"  /> &nbsp; &nbsp;--%>
                <asp:Button ID="btnDownload" runat="server" Text=" Download " CssClass="button" OnClick="btnDownload_Click"  />  
                <asp:Label ID="lblCount" CssClass="copy10grey" runat="server" Visible="false" ></asp:Label>
            </td>
        </tr>
        <tr>
            <td >
        <div id="esn">
        <asp:GridView ID="gvRMA" OnPageIndexChanging="gridView_PageIndexChanging"    AutoGenerateColumns="false"  
        Width="100%" ShowHeader="true"  ShowFooter="false" runat="server" GridLines="Both" 
        PageSize="50" AllowPaging="true"  AllowSorting="true" OnSorting="gvRMA_Sorting"  
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
        
                <asp:TemplateField HeaderText="RMA#" SortExpression="RmaNumber" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%" HeaderStyle-CssClass="buttonundlinelabel" >
                    <ItemTemplate>
                        <%# Eval("RmaNumber")%>   
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="RMA Date" SortExpression="RmaDate" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="6%" HeaderStyle-CssClass="buttonundlinelabel" >
                    <ItemTemplate>
                        <%# Eval("RmaDate") %>   
                    </ItemTemplate>
                </asp:TemplateField>
                           
                <asp:TemplateField HeaderText="RMA Status" SortExpression="RmaStatus" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="7%" HeaderStyle-CssClass="buttonundlinelabel" >
                    <ItemTemplate>
                        <%# Eval("RmaStatus")%>   
                    </ItemTemplate>
                </asp:TemplateField>
              
              <asp:TemplateField HeaderText="ESN" SortExpression="ESN"  ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="buttonundlinelabel" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                    <ItemTemplate>
                        <%# Eval("ESN")%>   
                    </ItemTemplate>
                </asp:TemplateField>
              
                <asp:TemplateField HeaderText="SKU#" SortExpression="SKU" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%" HeaderStyle-CssClass="buttonundlinelabel" >
                    <ItemTemplate>
                        <%# Eval("SKU")%>   
                    </ItemTemplate>
                </asp:TemplateField>
                  
                <asp:TemplateField HeaderText="Product Name" SortExpression="ProductName" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%" HeaderStyle-CssClass="buttonundlinelabel" >
                    <ItemTemplate>
                        <%# Eval("ProductName")%>   
                    </ItemTemplate>
                </asp:TemplateField>
                  
                <asp:TemplateField HeaderText="Fulfillment#" SortExpression="FulfillmentNumber" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%" HeaderStyle-CssClass="buttonundlinelabel" >
                    <ItemTemplate>
                        <%# Eval("FulfillmentNumber")%>   
                    </ItemTemplate>
                </asp:TemplateField>
                
                
                <asp:TemplateField HeaderText="Esn Status" SortExpression="EsnStatus" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="7%" HeaderStyle-CssClass="buttonundlinelabel" >
                    <ItemTemplate>
                        <%# Eval("EsnStatus")%>   
                    </ItemTemplate>
                </asp:TemplateField>
              <asp:TemplateField HeaderText="Triage Date" SortExpression="TriageDate" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="6%" HeaderStyle-CssClass="buttonundlinelabel" >
                    <ItemTemplate>
                        <%# Eval("TriageDate")%>   
                    </ItemTemplate>
                </asp:TemplateField>
              <asp:TemplateField HeaderText="Triage Status" SortExpression="TriageStatus" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="8%" HeaderStyle-CssClass="buttonundlinelabel" >
                    <ItemTemplate>
                        <%# Eval("TriageStatus")%>   
                    </ItemTemplate>
                </asp:TemplateField>
              <asp:TemplateField HeaderText="Reason" SortExpression="Reason" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%" HeaderStyle-CssClass="buttonundlinelabel" >
                    <ItemTemplate>
                        <%# Eval("Reason")%>   
                    </ItemTemplate>
                </asp:TemplateField>
              <asp:TemplateField HeaderText="Tracking#" SortExpression="TrackingNumber" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%" HeaderStyle-CssClass="buttonundlinelabel" >
                    <ItemTemplate>
                        <%# Eval("TrackingNumber")%>   
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
        

    </form>
</body>
</html>
