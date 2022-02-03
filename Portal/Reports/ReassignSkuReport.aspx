<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReassignSkuReport.aspx.cs" Inherits="avii.Reports.ReassignSkuReport" %>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="/Controls/Header.ascx" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="/Controls/Footer.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>.:: Lan Global Inc. - Reassign SKU ::.</title>

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
	
        <link href="../aerostyle.css" rel="stylesheet" type="text/css"/>

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
<body leftmargin="0" topmargin="0" marginwidth="0" marginheight="0">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    
    <table cellSpacing="0" cellPadding="0" align="center" width="100%" border="0">
        <tr>
			<td><head:MenuHeader id="HeadAdmin" runat="server"></head:MenuHeader></td>
		</tr>
		<%--<tr><td>&nbsp;</td></tr>--%>
		<tr>
			<td  bgcolor="#dee7f6" class="buttonlabel">&nbsp;&nbsp;Reassign SKU Report
			</td>
		</tr>
    <%--<tr><td>&nbsp;</td></tr>--%>
    </table>
	<asp:UpdatePanel ID="upnlCustomers" UpdateMode="Conditional" runat="server">
			<ContentTemplate>
	
    <table cellSpacing="0" cellPadding="0" align="center" width="100%" border="0">
    		<tr>                    
                <td >
                    <asp:Label ID="lblMsg" runat="server" Width="100%" CssClass="errormessage"></asp:Label></td>
            </tr> 
            <tr><td class="copy10grey" align="left"><%--&nbsp;
                        - ESN of inprocess RMA will be reflected fullfillment shipped count & RMA count.<br />&nbsp;
	                    - Unused ESN count equal to new plus approved RMA ESN. <br />&nbsp;--%>
                        </td></tr>  
            </table>  
                      
      <table bordercolor="#839abf" border="1" width="100%" align="center" cellpadding="0" cellspacing="0">
      <tr>
        <td>
         <asp:Panel  ID="pnlSearch" runat="server"  DefaultButton="btnSearch" >
      <table width="100%" border="0" class="box" align="center" cellpadding="2" cellspacing="2">
         <tr style="height:1px">
         <td style="height:1px"></td>
         </tr>
            <tr>
                <td class="copy10grey" align="right" width="15%">
                    Customer Name:
                </td>
                <td  align="left"colspan="4" >
                   <asp:DropDownList ID="dpCompany" runat="server" AutoPostBack="false"  Width="33%"
                 class="copy10grey"  >
                </asp:DropDownList>  
                <asp:Label ID="lblCompany" runat="server" CssClass="copy10grey" ></asp:Label>
                
        
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
                    <asp:TextBox ID="txtToDate" runat="server" onfocus="set_focus2();"  CssClass="copy10grey" MaxLength="15"  Width="70%"></asp:TextBox>
                    <img id="imgToDate"  alt="" onclick="document.getElementById('<%=txtToDate.ClientID%>').value = ''; displayCalendar(document.getElementById('<%=txtToDate.ClientID%>'),'mm/dd/yyyy',this,true);" src="../fullfillment/calendar/sscalendar.jpg" />
                
                </td>               
                </tr>
            <tr>
                <td class="copy10grey" align="right" width="15%">
                    ESN:
                
                
                </td>
                <td align="left" width="35%">
                    <asp:TextBox ID="txtESN" runat="server" CssClass="copy10grey" MaxLength="30"  Width="80%"></asp:TextBox>
                    
                </td>
                <td  width="1%">
                    &nbsp;
                </td>
                    <td class="copy10grey" align="right" width="10%">SKU#:</td>
                
                <td align="left" width="40%">
                    <asp:TextBox ID="txtSKU" runat="server" CssClass="copy10grey" MaxLength="20"  Width="70%"></asp:TextBox>
                    
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
                    &nbsp;
                    <asp:Button ID="btnDownload" runat="server" Text=" Download " CssClass="button" OnClick="btnDownload_Click" CausesValidation="false"/>  &nbsp;
                          &nbsp;
                    
            
        
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
                                    
                                    <%--<asp:Label ID="lblDate" CssClass="copy10grey" runat="server" ></asp:Label> 
                                    --%>
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

                                    <asp:GridView ID="gvESN" runat="server" Width="100%" GridLines="Both"   AutoGenerateColumns="false"
                                    OnPageIndexChanging="gridView_PageIndexChanging" PageSize="50" AllowPaging="true">
                                        <RowStyle BackColor="Gainsboro" />
                                        <AlternatingRowStyle BackColor="white" />
                                        <HeaderStyle  CssClass="buttongrid" ForeColor="white"/>
                                        <FooterStyle CssClass="white"  />
                                        <PagerStyle ForeColor="#636363" CssClass="copy10grey" HorizontalAlign="Left" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No." ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex +  1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ESN" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                                <ItemTemplate><%#Eval("ESN")%></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="OLD SKU Category" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                                <ItemTemplate><%#Eval("OLDSKUCategory")%></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="OLD SKU#" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                                <ItemTemplate><%#Eval("OLDSKU")%></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="New SKU Category" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                                <ItemTemplate><%#Eval("NewSKUCategory")%></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="NEW SKU#" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                                <ItemTemplate><%#Eval("NEWSKU")%></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Change Date" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                    <%--<%# Convert.ToDateTime(Eval("ChangeDate")).ToShortDateString() %>--%>
                                                    <%#Eval("ChangeDate")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField HeaderText="Customer Name" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                                <ItemTemplate><%#Eval("CustomerName")%></ItemTemplate>
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField HeaderText="Fulfillment#" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="8%">
                                                <ItemTemplate>
                                                <%#Eval("FulfillmentNumber")%>
                                                </ItemTemplate>
                                            </asp:TemplateField> 
                                            <asp:TemplateField HeaderText="RMA Number" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="8%">
                                                <ItemTemplate>
                                                <%#Eval("RMANumber")%>
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
		
      <%--      <asp:UpdatePanel ID="upnlJsRunner" UpdateMode="Always" runat="server">
			<ContentTemplate>
				<asp:PlaceHolder ID="phrJsRunner" runat="server"></asp:PlaceHolder>
			</ContentTemplate>
		</asp:UpdatePanel>--%>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server"
                     DynamicLayout="false">
            <ProgressTemplate>
                <img src="/Images/ajax-loaders.gif" /> Loading ...
            </ProgressTemplate>
        </asp:UpdateProgress>
        <br /> <br />
            <br /> <br />
        <table width="100%">
        <tr>
		    <td>
			    <foot:MenuFooter id="Menuheader2" runat="server"></foot:MenuFooter>
		    </td>
	    </tr>
        </table>
      

    </form>
</body>
</html>
