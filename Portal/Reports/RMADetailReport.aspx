<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RMADetailReport.aspx.cs" Inherits="avii.Reports.RMADetailReport" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>
<%@ Register TagPrefix="menu" TagName="Menu" Src="~/Controls/Header.ascx" %>

<!DOCTYPE html>

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
	        WinPrint.document.write('<link href="../lanstyle.css" type="text/css" rel="stylesheet" />');
	        WinPrint.document.write(prtContent.innerHTML);
	        WinPrint.document.close();
	        WinPrint.focus();
	        WinPrint.print();
	        WinPrint.close();
	        prtContent.innerHTML = strOldOne;


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
     </table><br />
    <table align="center" style="text-align:left" width="95%">
                <tr class="button" align="left">
                <td>&nbsp;RMA  ESN Report</td></tr>
             </table><br />
     
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
         <tr style="height:1px">
         <td style="height:1px"></td>
         </tr>
     
           
            <tr valign="top" style="display:none">
                <td class="copy10grey" align="right" width="15%">
                    RMA#:
                </td>
                <td width="35%">
                
                     <asp:TextBox ID="txtRMANumber" CssClass="copy10grey" MaxLength="25"  Width="60%"  runat="server"></asp:TextBox>
                    <%--<asp:DropDownList ID="dpCompany" runat="server" AutoPostBack="false"  Width="80%"
                 class="copy10grey"  >
                </asp:DropDownList>  
                <asp:Label ID="lblCompany" runat="server" CssClass="copy10grey" ></asp:Label>--%>
                </td>
                <td  width="1%">
                    &nbsp;
                </td>
                <td class="copy10grey" align="right" width="10%">
                    ESN:
                </td>

                <td width="40%">
                    
                   <asp:TextBox ID="txtESN" CssClass="copy10grey" MaxLength="30"  Width="80%"  runat="server"></asp:TextBox>
                    
                </td>   
                
                    
                </tr>

                <tr>
                <td class="copy10grey" align="right" width="15%">
                    From Date:
                
                
                </td>
                <td align="left" width="35%">
                    <asp:TextBox ID="txtFromDate" runat="server" onfocus="set_focus1();"  CssClass="copy10grey" MaxLength="15"  Width="60%"></asp:TextBox>
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
                                                Width="60%" >
                                                        
                                                        
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
                <td class="copy10grey" align="right" width="15%">
                    ESN Detail:
                </td>
                <td width="35%">
                    <asp:CheckBox ID="chkESN" runat="server" />
                
                </td>
                <td  width="1%">
                    &nbsp;
                </td>
                <td class="copy10grey" align="right" width="10%">
                   
                </td>
                <td width="40%">
                
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
      <table align="center" style="text-align:left" width="95%">
      <tr>
       <td  align="center"  >
       <asp:Panel ID="pnlRMA" runat="server" Visible="false">

       <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td align="right">
            <%--<input id="btnPrint" type="button" name="btnPrint1" class="button" value=" Print" onclick="javascript: CallPrint('esn');" Runat="Server"  /> &nbsp; &nbsp;--%>
                <asp:Button ID="btnDownload" runat="server" Text=" Download " CssClass="button" OnClick="btnDownload_Click"  />  
                <asp:Label ID="lblCount" CssClass="copy10grey" runat="server" Visible="false" ></asp:Label>
            </td>
        </tr>
        <tr>
            <td >
        <div id="esn">
        <asp:GridView ID="gvRMA" OnPageIndexChanging="gridView_PageIndexChanging"    AutoGenerateColumns="false"  
        Width="100%" ShowHeader="true"  ShowFooter="false" runat="server" GridLines="Both" 
        PageSize="50" AllowPaging="true"   AllowSorting="true" OnSorting="gvRMA_Sorting">
        <RowStyle BackColor="Gainsboro" />
        <AlternatingRowStyle BackColor="white" />
        <HeaderStyle  CssClass="buttonlabel" ForeColor="white"/>
          <PagerStyle ForeColor="#636363" CssClass="copy10grey" HorizontalAlign="Left" />
          <Columns>
                <%--<asp:TemplateField HeaderText="S.No." ItemStyle-CssClass="copy10grey"  ItemStyle-Width="1%">
                    <ItemTemplate>

                          <%# Container.DataItemIndex + 1%>
                  
                    </ItemTemplate>
                </asp:TemplateField>                 
        --%>
                <%--<asp:TemplateField HeaderText="Customer Name" ItemStyle-CssClass="copy10grey"  ItemStyle-Width="10%"  ItemStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                    <div style="width:100%; font-weight:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "bold":"normal" %>; background-color:<%# Convert.ToString(Eval("CustomerName")) == "zzTotal"? "yellow":"" %>; height:20px">
                    &nbsp;
                         <%# Convert.ToString(Eval("CustomerName")) == "zzTotal" ? "Total" : Eval("CustomerName")%>
                 
                 
                    </div>
                    </ItemTemplate>
                </asp:TemplateField>  --%>  
              <asp:TemplateField HeaderText="Category" SortExpression="CategoryName"  ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="buttonundlinelabel" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                    <ItemTemplate>
                        <%# Eval("CategoryName")%>   
                    </ItemTemplate>
                </asp:TemplateField>
              <asp:TemplateField HeaderText="SKU" SortExpression="SKU"  ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="buttonundlinelabel" ItemStyle-CssClass="copy10grey" ItemStyle-Width="8%">
                    <ItemTemplate>
                        <%# Eval("SKU")%>   
                    </ItemTemplate>
                </asp:TemplateField>
              
              <asp:TemplateField HeaderText="ESN" SortExpression="ESN"  ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="buttonundlinelabel" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                    <ItemTemplate>
                        <%# Eval("ESN")%>   
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="Batch Number" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey"  ItemStyle-Width="8%">
                    <ItemTemplate>
                        <%# Eval("BatchNumber")%>   
                    </ItemTemplate>
                </asp:TemplateField>
                  
                <asp:TemplateField HeaderText="ICCID" SortExpression="ICCID" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" HeaderStyle-CssClass="buttonundlinelabel" ItemStyle-Width="10%">
                    <ItemTemplate>
                        <%# Eval("ICCID")%>   
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="RMA#" SortExpression="RmaNumber" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%" HeaderStyle-CssClass="buttonundlinelabel">
                    <ItemTemplate>
                        <%# Eval("RMANumber")%>   
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="RMA Date" SortExpression="RmaDate" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="7%" HeaderStyle-CssClass="buttonundlinelabel">
                    <ItemTemplate>
                        <%# Convert.ToDateTime(Eval("RmaDate")).ToShortDateString() %>   
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="RMA Status" SortExpression="RmaStatus"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%" HeaderStyle-CssClass="buttonundlinelabel">
                    <ItemTemplate>
                        <%# Eval("RmaStatus")%>   
                    </ItemTemplate>
                </asp:TemplateField>
                
               <%-- <asp:TemplateField HeaderText="Esn Status" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                    <ItemTemplate>
                        <%# Eval("EsnStatus")%>   
                    </ItemTemplate>
                </asp:TemplateField>--%>
                <asp:TemplateField HeaderText="Triage Date" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="7%">
                    <ItemTemplate>
                        <%# Eval("TriageDate") %>  
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Triage Status" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="8%">
                    <ItemTemplate>
                        <%# Eval("TriageStatus")%>   
                    </ItemTemplate>
                </asp:TemplateField>           
                <asp:TemplateField HeaderText="Reason" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="8%">
                    <ItemTemplate>
                        <%# Eval("Reason")%>   
                    </ItemTemplate>
                </asp:TemplateField>           
              <asp:TemplateField HeaderText="Tracking#" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="8%">
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
           <asp:Panel ID="pnlESNDetail" runat="server" Visible="false">

       <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td align="right">
            <%--<input id="Button1" type="button" name="btnPrint1" class="button" value=" Print" onclick="javascript: CallPrint('esn');" Runat="Server"  /> &nbsp; &nbsp;--%>
                <asp:Button ID="Button2" runat="server" Text=" Download " CssClass="button" OnClick="btnDownload_Click"  />  
                <asp:Label ID="lblCountdetail" CssClass="copy10grey" runat="server" Visible="false" ></asp:Label>
            </td>
        </tr>
        <tr>
            <td >
        <div id="esndetail">
        <asp:GridView ID="gvEsnDetail" OnPageIndexChanging="gvEsnDetail_PageIndexChanging"    AutoGenerateColumns="false"  
        Width="100%" ShowHeader="true"  ShowFooter="false" runat="server" GridLines="Both" 
        PageSize="50" AllowPaging="true"   AllowSorting="true" OnSorting="gvEsnDetail_Sorting">
        <RowStyle BackColor="Gainsboro" />
        <AlternatingRowStyle BackColor="white" />
        <HeaderStyle  CssClass="buttonlabel" ForeColor="white"/>
          <PagerStyle ForeColor="#636363" CssClass="copy10grey" HorizontalAlign="Left" />
          <Columns>
                <%--<asp:TemplateField HeaderText="S.No." ItemStyle-CssClass="copy10grey"  ItemStyle-Width="1%">
                    <ItemTemplate>

                          <%# Container.DataItemIndex + 1%>
                  
                    </ItemTemplate>
                </asp:TemplateField>                 
        --%>
              <asp:TemplateField HeaderText="Category" SortExpression="CategoryName"  ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="buttonundlinelabel" ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                    <ItemTemplate>
                        <%# Eval("CategoryName")%>   
                    </ItemTemplate>
                </asp:TemplateField>
              <asp:TemplateField HeaderText="SKU" SortExpression="SKU"  ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="buttonundlinelabel" ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                    <ItemTemplate>
                        <%# Eval("SKU")%>   
                    </ItemTemplate>
                </asp:TemplateField>
              
               <asp:TemplateField HeaderText="ESN" SortExpression="ESN" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="8%" HeaderStyle-CssClass="buttonundlinelabel">
                    <ItemTemplate>
                        <%# Eval("ESN")%>   
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="Batch Number" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="7%">
                    <ItemTemplate>
                        <%# Eval("BatchNumber")%>   
                    </ItemTemplate>
                </asp:TemplateField>
                  
                <asp:TemplateField HeaderText="ICCID" SortExpression="ICCID" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="8%" HeaderStyle-CssClass="buttonundlinelabel">
                    <ItemTemplate>
                        <%# Eval("ICCID")%>   
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="RMA#" SortExpression="RmaNumber" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="8%" HeaderStyle-CssClass="buttonundlinelabel">
                    <ItemTemplate>
                        <%# Eval("RMANumber")%>   
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="RMA Date" SortExpression="RmaDate" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%" HeaderStyle-CssClass="buttonundlinelabel">
                    <ItemTemplate>
                        <%# Convert.ToDateTime(Eval("RmaDate")).ToShortDateString() %>   
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="RMA Status" SortExpression="RmaStatus" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%" HeaderStyle-CssClass="buttonundlinelabel">
                    <ItemTemplate>
                        <%# Eval("RmaStatus")%>   
                    </ItemTemplate>
                </asp:TemplateField>
                
               <%-- <asp:TemplateField HeaderText="Esn Status" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                    <ItemTemplate>
                        <%# Eval("EsnStatus")%>   
                    </ItemTemplate>
                </asp:TemplateField>--%>
                <asp:TemplateField HeaderText="Triage Date" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                    <ItemTemplate>
                        <%# Eval("TriageDate") %> 
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Triage Status" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                    <ItemTemplate>
                        <%# Eval("TriageStatus")%>   
                    </ItemTemplate>
                </asp:TemplateField>           
                <asp:TemplateField HeaderText="Reason" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                    <ItemTemplate>
                        <%# Eval("Reason")%>   
                    </ItemTemplate>
                </asp:TemplateField>           
              <asp:TemplateField HeaderText="Tracking#" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                    <ItemTemplate>
                        <%# Eval("TrackingNumber")%>   
                    </ItemTemplate>
                </asp:TemplateField>  
              <asp:TemplateField HeaderText="CustomerName" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="8%">
                    <ItemTemplate>
                        <%# Eval("CustomerName")%>   
                    </ItemTemplate>
                </asp:TemplateField>  
              <asp:TemplateField HeaderText="Address" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                    <ItemTemplate>
                        <%# Eval("Address")%>   
                    </ItemTemplate>
                </asp:TemplateField>  
              <asp:TemplateField HeaderText="City" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="7%">
                    <ItemTemplate>
                        <%# Eval("City")%>   
                    </ItemTemplate>
                </asp:TemplateField>  
              <asp:TemplateField HeaderText="State" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="7%">
                    <ItemTemplate>
                        <%# Eval("State")%>   
                    </ItemTemplate>
                </asp:TemplateField>  
                <asp:TemplateField HeaderText="Zip" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
                    <ItemTemplate>
                        <%# Eval("Zip")%>   
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
         <asp:PostBackTrigger ControlID="Button2" />
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
