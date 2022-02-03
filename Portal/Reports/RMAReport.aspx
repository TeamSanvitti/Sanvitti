<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RMAReport.aspx.cs" Inherits="avii.RMA.RMAReport" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>
<%@ Register TagPrefix="menu" TagName="Menu" Src="~/Controls/Header.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title>.:: Lan Global Inc. -  RMA Report ::.</title>
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
<script type="text/javascript">
    function OpenNewPage(url) {
        window.open(url);
    }

    function divexpandcollapse(divname) {
        // alert(divname);

        var div = document.getElementById(divname);
        var img = document.getElementById('img' + divname);

        if (div.style.display == "none") {

            if ($('#hdnchild').val() == "")
                $('#hdnchild').val(divname);
            else
                $('#hdnchild').val($('#hdnchild').val() + ',' + divname);

            div.style.display = "inline";
            img.src = "../Images/minus.gif";

        } else {

            var id = $('#hdnchild').val().replace(divname + ',', '').replace(divname + ',', '').replace(divname + ',', '').replace(divname, '').replace(divname, '').replace(divname, '');
            //   //   alert(id)
            $('#hdnchild').val(id);

            div.style.display = "none";
            img.src = "../Images/plus.gif";

        }

    } 

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
<body  bgcolor="#ffffff" leftmargin="0" rightmargin="0" topmargin="0">
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
            <asp:HiddenField ID="hdnchild" runat="server" />
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
     
           
            <tr valign="top" runat="server" id="trCustomer" >
                <td class="copy10grey" align="right" width="15%">
                    Customer Name:
                </td>
                <td width="35%">
                    <asp:DropDownList ID="dpCompany" runat="server"  Width="80%" class="copy10grey"  ></asp:DropDownList>  
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
                
                    <asp:DropDownList ID="ddlEsnStatus"  runat="server" Class="copy10grey" Width="80%" ></asp:DropDownList>
                
                </td>
                <td  width="1%">
                    &nbsp;
                </td>
                <td class="copy10grey" align="right" width="10%">
                    RMA Status:
                </td>
                <td width="40%">
                    <asp:DropDownList ID="ddlRmaStaus"  runat="server" Class="copy10grey" Width="46%" ></asp:DropDownList>
                
                </td>
                </tr>
                <tr>
                <td class="copy10grey" align="right" width="15%">
                    Triage Status:
                </td>
                <td width="35%">
                
                    <asp:DropDownList ID="ddlTriage"  runat="server" Class="copy10grey" Width="80%" ></asp:DropDownList>
                
                </td>
                <td  width="1%">
                    &nbsp;
                </td>
                <td class="copy10grey" align="right" width="10%">
                    Receive Status:
                </td>
                <td width="40%">
                    <asp:DropDownList ID="ddlReceive"  runat="server" Class="copy10grey" Width="46%" ></asp:DropDownList>
                
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
                <asp:Label ID="lblCount" CssClass="copy10grey" runat="server" Visible="true" ></asp:Label>
            </td>
        </tr>
        <tr>
            <td >
        <div id="esn">
        <asp:GridView ID="gvRMA" OnPageIndexChanging="gvRMA_PageIndexChanging"    AutoGenerateColumns="false"  
        Width="100%" ShowHeader="true"  ShowFooter="false" runat="server" GridLines="Both" OnRowDataBound="OnRowDataBound"  
        PageSize="50" AllowPaging="true"  AllowSorting="true" OnSorting="gvRMA_Sorting"  
        >
        <RowStyle BackColor="Gainsboro" />
        <AlternatingRowStyle BackColor="white" />
        <HeaderStyle  CssClass="buttonlabel" ForeColor="white"/>
          <PagerStyle ForeColor="#636363" CssClass="copy10grey" HorizontalAlign="Left" />
          <Columns>
                <asp:TemplateField ItemStyle-Width="1%">
                <ItemTemplate>
                    <a href="JavaScript:divexpandcollapse('div<%# Container.DataItemIndex +  1 %>');">
                        <img id="imgdiv<%# Container.DataItemIndex +  1 %>" width="9px" border="0" 
                                                                        src="../Images/plus.png" alt="" /></a>                       
                </ItemTemplate>
                <ItemStyle Width="20px" VerticalAlign="Middle"></ItemStyle>
                </asp:TemplateField>
                                
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
                <asp:TemplateField HeaderText="RMA Date" SortExpression="RmaDate" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%" HeaderStyle-CssClass="buttonundlinelabel" >
                    <ItemTemplate>
                        <%# Eval("RmaDate") %>   
                    </ItemTemplate>
                </asp:TemplateField>    
              <asp:TemplateField HeaderText="Customer RMA#" SortExpression="CustomerRMANumber" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%" HeaderStyle-CssClass="buttonundlinelabel" >
                    <ItemTemplate>
                        <%# Eval("CustomerRMANumber")%>   
                    </ItemTemplate>
                </asp:TemplateField><asp:TemplateField HeaderText="Customer Name" SortExpression="CustomerContactName" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%" HeaderStyle-CssClass="buttonundlinelabel" >
                    <ItemTemplate>
                        <%# Eval("CustomerContactName")%>   
                    </ItemTemplate>
                </asp:TemplateField>
              <asp:TemplateField HeaderText="City" SortExpression="CustomerCity" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%" HeaderStyle-CssClass="buttonundlinelabel" >
                    <ItemTemplate>
                        <%# Eval("CustomerCity")%>   
                    </ItemTemplate>
              </asp:TemplateField>
              <asp:TemplateField HeaderText="State" SortExpression="CustomerState" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%" HeaderStyle-CssClass="buttonundlinelabel" >
                    <ItemTemplate>
                        <%# Eval("CustomerState")%>   
                    </ItemTemplate>
                </asp:TemplateField>
              <asp:TemplateField HeaderText="Zip" SortExpression="CustomerZipCode" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%" HeaderStyle-CssClass="buttonundlinelabel" >
                    <ItemTemplate>
                        <%# Eval("CustomerZipCode")%>   
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="RMA Status" SortExpression="RmaStatus" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%" HeaderStyle-CssClass="buttonundlinelabel" >
                    <ItemTemplate>
                        <%# Eval("RmaStatus")%>   
                    </ItemTemplate>
                </asp:TemplateField>
              <asp:TemplateField HeaderText="Triage Status" SortExpression="TriageStatus" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%" HeaderStyle-CssClass="buttonundlinelabel" >
                    <ItemTemplate>
                        <%# Eval("TriageStatus")%>   
                    </ItemTemplate>
                </asp:TemplateField>
              <asp:TemplateField HeaderText="Receive Status" SortExpression="ReceiveStatus" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%" HeaderStyle-CssClass="buttonundlinelabel" >
                    <ItemTemplate>
                        <%# Eval("ReceiveStatus")%>   
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Fulfillment#" SortExpression="FulfillmentNumber" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%" HeaderStyle-CssClass="buttonundlinelabel" >
                    <ItemTemplate>
                        <%# Eval("FulfillmentNumber")%>   

                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText=""  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="1%" HeaderStyle-CssClass="buttonundlinelabel" >
                    <ItemTemplate>
                        
                        <asp:ImageButton ID="imgViewRMA"  ToolTip="View RMA" OnCommand="imgViewRMA_Command"
                                     CausesValidation="false" CommandArgument='<%# Eval("rmaGUID") %>' ImageUrl="~/Images/view.png"  runat="server" />

                    </ItemTemplate>
                </asp:TemplateField>

              <asp:TemplateField>
                    <ItemTemplate>
                    <tr>
                    <td colspan="100%">
                        <div id="div<%# Container.DataItemIndex +  1 %>"  style="overflow:auto; display:none;
                                        position: relative; left: 15px; overflow: auto">
                        <asp:GridView ID="gvRmaDetail" runat="server" AutoGenerateColumns="false" Width="99%" >
                            <RowStyle BackColor="Gainsboro" />
                            <AlternatingRowStyle BackColor="white" />
                            <HeaderStyle  CssClass="buttonlabel" ForeColor="white"/>
                            <FooterStyle CssClass="white"  />
                            <PagerStyle ForeColor="#636363" CssClass="copy10grey" HorizontalAlign="Left" />
                            <Columns>
                                <asp:TemplateField HeaderText="S.No." ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="1%">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex +  1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>                                 
                                <asp:TemplateField HeaderText="Category Name" SortExpression="CategoryName" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="8%" HeaderStyle-CssClass="buttonundlinelabel" >
                                    <ItemTemplate>
                                        <%# Eval("CategoryName")%>   
                                    </ItemTemplate>
                                </asp:TemplateField>                  
                                <asp:TemplateField HeaderText="SKU" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                    <ItemTemplate><%#Eval("SKU")%></ItemTemplate>
                                </asp:TemplateField>                                                            
                                <asp:TemplateField HeaderText="Product Name" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="18%">
                                    <ItemTemplate><%#Eval("ProductName")%></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ESN" SortExpression="ESN"  ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="buttonundlinelabel" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <%# Eval("ESN")%>   
                                    </ItemTemplate>
                                </asp:TemplateField>   
                                <asp:TemplateField HeaderText="Esn Status" SortExpression="EsnStatus" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%" HeaderStyle-CssClass="buttonundlinelabel" >
                                    <ItemTemplate>
                                        <%# Eval("Status")%>   
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Reason" SortExpression="Reason" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%" HeaderStyle-CssClass="buttonundlinelabel" >
                                    <ItemTemplate>
                                        <%# Eval("Reason")%>   
                                    </ItemTemplate>
                                </asp:TemplateField>              
                                <asp:TemplateField HeaderText="Warrenty" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                    <ItemTemplate><%#Eval("Warranty")%></ItemTemplate>
                                </asp:TemplateField>                                
                                <asp:TemplateField HeaderText="Disposition" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%" HeaderStyle-CssClass="buttonundlinelabel" >
                                    <ItemTemplate>
                                        <%# Eval("Disposition")%>   
                                    </ItemTemplate>
                                </asp:TemplateField>        
                            </Columns>
                        </asp:GridView>

                    </div>
                    </td>
                    </tr>
                    </ItemTemplate>
                </asp:TemplateField>
  
                
            </Columns>
        </asp:GridView>
        </div>
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
