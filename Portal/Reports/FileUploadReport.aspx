<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FileUploadReport.aspx.cs" Inherits="avii.Reports.FileUploadReport" %>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="/Controls/Header.ascx" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="/Controls/Footer.ascx" %>

<%@ Register TagPrefix="UC" TagName="Content" Src="~/Controls/Assignment.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>.:: Lan Global Inc. - File Upload ::.</title>

    <link href="../aerostyle.css" rel="stylesheet" type="text/css"/>
    
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
                minHeight: 400,
                height: 540,
                width: 400,
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


        function Validate() {
            
            var company = document.getElementById("<% =dpCompany.ClientID %>");
            //alert(company);
            //if (company != 'null' && company.selectedIndex == 0) {
            //    alert('Customer is required!');
            //    return false;

           // }
            
        }
		    

	</script>
    

    <link rel="stylesheet"  type="text/css" href="../fullfillment/calendar/dhtmlgoodies_calendar.css" media="screen" />
	<script type="text/javascript" src="../fullfillment/calendar/dhtmlgoodies_calendar.js"></script>  
		
    
</head>
<body leftmargin="0" topmargin="0" marginwidth="0" marginheight="0"> 
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    
    <table cellSpacing="0" cellPadding="0" align="center" width="100%" border="0">
        <tr>
			<td><head:MenuHeader id="HeadAdmin" runat="server"></head:MenuHeader></td>
		</tr>
		<tr><td>&nbsp;</td></tr>
		<tr>
			<td  bgcolor="#dee7f6" class="button">
            &nbsp;&nbsp;File Upload
			</td>
		</tr>
    <tr>
        <td>&nbsp;</td>
    </tr>
    </table>
    <div id="divContainer">	
			<div id="divESN" style="display:none">
					
				<asp:UpdatePanel ID="upnlESN" runat="server">
					<ContentTemplate>
                   
						<asp:PlaceHolder ID="phrESN" runat="server">
                        <table width="100%" border="0">
                                <%--<tr>
                                    <td class="copy10grey" width="20%" align="left">
                                        <b> &nbsp;Tracking#:</b>
                                    </td>
                                    <td align="left" >
                                     <asp:Label ID="lblTracking" runat="server" CssClass="copy10grey"></asp:Label>               
                                    </td>
                                </tr>--%>
                                <tr>
                                    <td colspan="2">
                                
                                    <asp:Panel ID="pnlESN" runat="server" Width="100%">
                                        <asp:Label ID="lblEsnMsg" runat="server" Width="100%" CssClass="errormessage"></asp:Label>
                                        <UC:Content ID="esn1" runat="server" ></UC:Content>
                                    
                            </asp:Panel>
                            </td>
                                </tr>
                                </table>
                                
                        </asp:PlaceHolder>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
</div>
	<asp:UpdatePanel ID="upnlCustomers" UpdateMode="Conditional" runat="server">
			<ContentTemplate>
	
    <table cellSpacing="0" cellPadding="0" align="center" width="100%" border="0">
    		<tr>                    
                <td >
                    <asp:Label ID="lblMsg" runat="server" Width="100%" CssClass="errormessage"></asp:Label></td>
            </tr> 
            <%--<tr><td class="copy10grey" align="left">&nbsp;
                        - ESN/SIM of inprocess RMA will be reflected fullfillment shipped count & RMA count.<br />&nbsp;
	                    - Unused ESN/SIM count equal to new plus approved RMA ESN. <br />&nbsp;
                        </td></tr> --%> 
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
                <td width="35%" align="left">
                &nbsp;<asp:DropDownList ID="dpCompany" runat="server" AutoPostBack="false" Width="81%"
                 class="copy10grey"  >
                </asp:DropDownList>  
                <asp:Label ID="lblCompany" runat="server" CssClass="copy10grey" ></asp:Label>
                </td>
                <td  width="1%">
                    &nbsp;
                </td>
                <td class="copy10grey" align="right" width="10%">
                Module Name:
                    
                
                </td>
                <td class="copy10grey" align="left" width="40%">
                    &nbsp;<asp:DropDownList ID="ddlModule" runat="server" class="copy10grey" Width="46%">
                    </asp:DropDownList>
          
                </td>

            </tr>    
            <tr>
                <td class="copy10grey" align="right" width="15%">
                    From Date:
                
                
                </td>
                <td align="left" width="35%">
                    &nbsp;<asp:TextBox ID="txtFromDate" runat="server" onfocus="set_focus1();"  CssClass="copy10grey" MaxLength="15"  Width="80%"></asp:TextBox>
                    <img id="imgFromtDate" alt="" onclick="document.getElementById('<%=txtFromDate.ClientID%>').value = ''; displayCalendar(document.getElementById('<%=txtFromDate.ClientID%>'),'mm/dd/yyyy',this,true);" src="../fullfillment/calendar/sscalendar.jpg" />
                </td>
                <td  width="1%">
                    &nbsp;
                </td>
                    <td class="copy10grey" align="right" width="10%">Date To:</td>
                
                <td align="left" width="40%">
                    &nbsp;<asp:TextBox ID="txtToDate" runat="server" onfocus="set_focus2();"  CssClass="copy10grey" MaxLength="15"  Width="45%"></asp:TextBox>
                    <img id="imgToDate"  alt="" onclick="document.getElementById('<%=txtToDate.ClientID%>').value = ''; displayCalendar(document.getElementById('<%=txtToDate.ClientID%>'),'mm/dd/yyyy',this,true);" src="../fullfillment/calendar/sscalendar.jpg" />
                
                </td>               
                </tr>
                
            <tr>
            <td colspan="5">
                <hr />
                </td>
                </tr>
                <tr>
                <td  align="center"  colspan="5">
                    
                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="button" OnClientClick="return Validate();"   OnClick="btnSearch_Click" CausesValidation="false"/>
                     &nbsp;
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="button" OnClick="btnCancel_Click" CausesValidation="false"/>
                    &nbsp;
                    <%--<asp:Button ID="btnDownload" runat="server" Text=" Download " CssClass="button" OnClientClick="openDownloadDialog('Download ESN','btnDownload')"  CausesValidation="false"/>  &nbsp;
                          &nbsp;--%>
                    <%--<asp:Button ID="btnDwnlUsedESN" runat="server" Text=" Download Used ESN" CssClass="button" OnClick="btnDwnlUsedESN_Click" CausesValidation="false" />  
--%>
            
        
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
                                    
                                    <%--<asp:Label ID="lblDate" CssClass="copy10grey" runat="server" ></asp:Label> --%>
                                    <%--<asp:Label ID="lblRmaDate" CssClass="copy10grey" runat="server" ></asp:Label> &nbsp;&nbsp;    --%>
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
                                    <asp:GridView ID="gvFileUpload" runat="server" Width="100%" GridLines="Both"   AutoGenerateColumns="false"
                                    OnPageIndexChanging="gridView_PageIndexChanging" PageSize="50" AllowPaging="false" 
                                    OnRowDataBound="gridView_RowDataBound" ShowFooter="false" >
                                        <RowStyle BackColor="Gainsboro" />
                                        <AlternatingRowStyle BackColor="white" />
                                        <HeaderStyle  CssClass="button" ForeColor="white"/>
                                        <FooterStyle CssClass="white"  />
                                        <PagerStyle ForeColor="#636363" CssClass="copy10grey" HorizontalAlign="Left" />
                                        <Columns>

                                            <asp:TemplateField HeaderText="S.No." ItemStyle-HorizontalAlign="Left"  ItemStyle-CssClass="copy10grey" ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex +  1 %>
                                                
                                                </ItemTemplate>
                                                
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Upload Date" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                    <%#Eval("UploadDate")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField HeaderText="Customer Name" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                    <%#Eval("CustomerName")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            
                                            
                                            <asp:TemplateField HeaderText="Module Name" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                    <%#Eval("ModuleName")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField HeaderText="Uploaded By" ItemStyle-HorizontalAlign="Left"   ItemStyle-CssClass="copy10grey"  ItemStyle-Width="8%">
                                                <ItemTemplate>
                                                <%#Eval("UploadedBy")%>
                                                </ItemTemplate>
                                                
                                            </asp:TemplateField> 
                                            
                                            <asp:TemplateField HeaderText="FILENAME" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="8%">
                                                <ItemTemplate>
                                                <%#Eval("FileName")%>
                                                </ItemTemplate>
                                                
                                            </asp:TemplateField> 
                                            <asp:TemplateField HeaderText="Comments" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="8%">
                                                <ItemTemplate>
                                                <%#Eval("Comments")%>
                                                </ItemTemplate>
                                                
                                            </asp:TemplateField> 
                                            
                                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center"  ItemStyle-CssClass="copy10grey"  ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                <asp:ImageButton ToolTip="View Mapping" 
                                                        CausesValidation="false" OnCommand="imgViewMapping_Click" CommandArgument='<%# Eval("UploadedFileID") + "," + Eval("ModuleID") %>'  
                                                        ID="imgMapping"  runat="server" ImageUrl="~/Images/View.png"  >
                                                        
                                                        
                                                        </asp:ImageButton>&nbsp;
                                                
                                                </ItemTemplate>
                                                
                                            </asp:TemplateField> 
                                            
                                            

                            
                                        </Columns>
                                    </asp:GridView>
                                    

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
