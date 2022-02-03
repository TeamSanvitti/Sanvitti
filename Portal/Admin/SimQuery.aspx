<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SimQuery.aspx.cs" Inherits="avii.Admin.SimQuery" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="/Controls/Footer.ascx" %>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="/Controls/Header.ascx" %>
<%@ Register TagPrefix="PO" TagName="PoDetail" Src="~/Controls/PODetails.ascx" %>
<%@ Register TagPrefix="CT" TagName="SimLog" Src="~/Controls/SimLogControl.ascx" %>
<%@ Register TagPrefix="CT" TagName="RMA" Src="~/Controls/RMADetails.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Lan Global inc. Inc. - SIM Query</title>
    <link href="../aerostyle.css" type="text/css" rel="stylesheet" />
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
	            height: 400,
	            width: 1124,
	            resizable: false,
	            open: function (event, ui) {
	                $(this).parent().appendTo("#divFulfillmentContainer");
	            }
	        });

	        $("#divRMA").dialog({
	            autoOpen: false,
	            modal: true,
	            minHeight: 20,
	            height: 300,
	            width: 800,
	            resizable: false,
	            open: function (event, ui) {
	                $(this).parent().appendTo("#divFulfillmentContainer");
	            }
	        });
	        $("#divSimLog").dialog({
	            autoOpen: false,
	            modal: true,
	            minHeight: 20,
	            height: 300,
	            width: 800,
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
	        else
	            top = 10;
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


	    function closedivRMADialog() {
	        //Could cause an infinite loop because of "on close handling"
	        $("#divRMA").dialog('close');
	    }

	    function opendivRMADialog(title, linkID) {

	        var pos = $("#" + linkID).position();
	        var top = pos.top;
	        var left = pos.left + $("#" + linkID).width() + 10;
	        //alert(top);
	        if (top > 600)
	            top = 10;
	        else
	            top = 10;
	        
	        //top = top - 600;
	        left = 250;
	        $("#divRMA").dialog("option", "title", title);
	        $("#divRMA").dialog("option", "position", [left, top]);

	        $("#divRMA").dialog('open');

	    }
	    function opendivRMADialogAndBlock(title, linkID) {
	        opendivRMADialog(title, linkID);

	        //block it to clean out the data
	        $("#divRMA").block({
	            message: '<img src="../images/async.gif" />',
	            css: { border: '0px' },
	            fadeIn: 0,
	            //fadeOut: 0,
	            overlayCSS: { backgroundColor: '#ffffff', opacity: 1 }
	        });
	    }

	    function unblockdivRMADialog() {
	        $("#divRMA").unblock();
	    }

	    function closedivSimLogDialog() {
	        //Could cause an infinite loop because of "on close handling"
	        $("#divSimLog").dialog('close');
	    }
	    function opendivSimLogDialog(title, linkID) {

	        var pos = $("#" + linkID).position();
	        var top = pos.top;
	        var left = pos.left + $("#" + linkID).width() + 10;
	        //alert(left);
	        top = top - 300;
	        left = 300;
	        $("#divSimLog").dialog("option", "title", title);
	        $("#divSimLog").dialog("option", "position", [left, top]);

	        $("#divSimLog").dialog('open');
	    }
	    function opendivSimLogDialogAndBlock(title, linkID) {
	        opendivSimLogDialog(title, linkID);

	        //block it to clean out the data
	        $("#divSimLog").block({
	            message: '<img src="../images/async.gif" />',
	            css: { border: '0px' },
	            fadeIn: 0,
	            //fadeOut: 0,
	            overlayCSS: { backgroundColor: '#ffffff', opacity: 1 }
	        });
	    }
	    function unblockdivSimLogDialog() {
	        $("#divSimLog").unblock();
	    }
	    
	    </script>
</head>
<body bgColor="#ffffff" leftmargin="0" rightmargin="0" topmargin="0">
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
    <table  cellspacing="1" cellpadding="1" width="100%">
        <tr>
		    <td colspan="6" bgcolor="#dee7f6" class="button">&nbsp;&nbsp;SIM Query
		    </td>
        </tr>

    </table>  
    <asp:UpdatePanel ID="UpdatePanel1"  runat="server" UpdateMode="Conditional" >
    <ContentTemplate>
    
    <table cellSpacing="0" cellPadding="0" align="center" width="100%" border="0">
    		<tr>                    
                <td colspan="2">
                    <asp:Label ID="lblMsg" runat="server" Width="100%" CssClass="errormessage"></asp:Label></td>
            </tr>               
            
            <tr>
                <td align="center">

                    <table  cellSpacing="1" cellPadding="1" width="100%">
                        <tr><td class="copy10grey" align="left">&nbsp;
                        
                        
                        </td></tr>
                    </table>

            
                 <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="90%">
                    <tr>
                    <td>
                        <table  cellSpacing="5" cellPadding="5" width="100%" border="0">
                            <tr>
                                <td  class="copy10grey" align="right" width="15%" >
                                    Customer: &nbsp;</td>
                                <td align="left" width="35%" >
                                    <asp:DropDownList ID="dpCompany" CssClass="copy10grey" runat="server" Width="71%"
                                    OnSelectedIndexChanged="dpCompany_SelectedIndexChanged"  
                                    AutoPostBack="true">
									</asp:DropDownList>
                                <td class="copy10grey"  width="15%" align="right">
                                    <asp:Label ID="lblSKU" runat="server" Text="SKU:" CssClass="copy10grey"></asp:Label>
                                     &nbsp;
                                </td>
                                
                                <td  align="left" width="35%" >
                                <asp:DropDownList ID="ddlSKU" runat="server" class="copy10grey" Width="71%">
                                </asp:DropDownList>
                                            

                                </td>
                            </tr>
                            
                            <tr>
                                <td  class="copy10grey" align="right" >
                                    SIM: &nbsp;</td>
                                <td align="left" >
                                    
                                    <asp:TextBox ID="txtSIM" runat="server" MaxLength="30" CssClass="copy10grey" Width="70%"></asp:TextBox>
                                    </td>
                                <td class="copy10grey" align="right">
                                    ESN: &nbsp;
                                         </td>
                                <td class="copy10grey" align="left">
                                  <asp:TextBox ID="txtESN" runat="server" MaxLength="30" CssClass="copy10grey" Width="70%"></asp:TextBox>

                                </td>
                             </tr>
                             
                            <tr><td colspan="4">
                                        <hr style="width:100%" />
                            
                                        </td></tr>
                            <tr>
                                    <td align="center"  colspan="4">
                                        <asp:Button ID="btnSearch" runat="server" Text="Search " OnClick="search_click"  CausesValidation="false"
                                            CssClass="button" Height="24px" Width="130px" />&nbsp;
                                            

                                        
                                        <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Cancel"  Height="24px" Width="130px" 
                                        CausesValidation="false" CssClass="button" />

                                            
	
                                    </td>
                                </tr>   
                        </table>
                    </td>
                    </tr>
                           
                 </table>
                

                 
                   <table width="100%">
                        <tr>
                            <td align="right"> 
                                
                <asp:Label ID="lblCount" CssClass="copy10grey" runat="server" ></asp:Label>
                </td>
                </tr>


                <tr>
                    <td> 
                         <asp:GridView ID="gvSIM" OnPageIndexChanging="gridView_PageIndexChanging"  
                          AutoGenerateColumns="false"  OnRowDataBound="gvSIM_RowDataBound"
                            DataKeyNames="simID"  Width="100%"  
                        ShowFooter="false" runat="server" GridLines="Both"  
                        PageSize="50" AllowPaging="true" 
                        BorderStyle="Outset"
                        AllowSorting="false" > 
                        <RowStyle BackColor="Gainsboro" />
                        <AlternatingRowStyle BackColor="white" />
                        <HeaderStyle  CssClass="button" ForeColor="white"/>
                        <FooterStyle CssClass="white"  />
                        <PagerStyle ForeColor="#636363" CssClass="copy10grey" HorizontalAlign="Left" />
                        <Columns>
                            <%--<asp:TemplateField ItemStyle-CssClass="copy10grey"  HeaderText="" ItemStyle-Width="2%" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chk"  runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>                 
                            --%>
                            <asp:TemplateField ItemStyle-CssClass="copy10grey"  HeaderText="S.No." ItemStyle-Width="2%" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    
                                    <%# Container.DataItemIndex + 1 %>
                                   
                                </ItemTemplate>
                            </asp:TemplateField>                 
                            
                            <asp:TemplateField ItemStyle-CssClass="copy10grey"  HeaderText="Company Name" ItemStyle-Width="15%">
                                <ItemTemplate>
                                    <%# Eval("CompanyName")%>

                                    
                                </ItemTemplate>
                            </asp:TemplateField>                 
                            <asp:TemplateField ItemStyle-CssClass="copy10grey"  ItemStyle-Width="10%" HeaderText="SIM #">
                                <ItemTemplate>
                                    <%# Eval("sim")%>
                                    
                                </ItemTemplate>
                            </asp:TemplateField> 
                            <asp:TemplateField ItemStyle-CssClass="copy10grey"  ItemStyle-Width="10%" HeaderText="ESN">
                                <ItemTemplate>
                                    <%# Eval("esn")%>
                                    
                                </ItemTemplate>
                            </asp:TemplateField>  
                            <asp:TemplateField ItemStyle-CssClass="copy10grey"  ItemStyle-Width="10%" HeaderText="SKU">
                                <ItemTemplate>
                                    <%# Eval("sku")%>
                                </ItemTemplate>
                            </asp:TemplateField>                
                            <asp:TemplateField ItemStyle-CssClass="copy10grey"  ItemStyle-Width="10%" HeaderText="Upload Date">
                                <ItemTemplate>
                                <%--<%# DataBinder.Eval(Container.DataItem, "UploadDate", "{0:MM/dd/yyyy}")%>--%>
                                    <%# Convert.ToDateTime(Eval("UploadDate")).ToShortDateString() == "1/1/0001" ? "" : Eval("UploadDate")%>
                                </ItemTemplate>
                            </asp:TemplateField>                 
                            <asp:TemplateField ItemStyle-CssClass="copy10grey"  ItemStyle-Width="10%" HeaderText="Modified Date">
                                <ItemTemplate>
                                 <%# Convert.ToDateTime(Eval("ModifiedDate")).ToShortDateString() == "1/1/0001" ? "" : Eval("ModifiedDate")%>
                                    <%--<%# Eval("ModifiedDate")%>--%>
                                </ItemTemplate>
                            </asp:TemplateField>                 
                            <asp:TemplateField ItemStyle-CssClass="copy10grey"  ItemStyle-Width="10%" HeaderText="Fulfillment#">
                                <ItemTemplate>
                                <asp:LinkButton ID="lnkPoNum" CommandArgument='<%# Eval("FulfillmentNumber")%>' OnCommand="lnkPoNum_OnCommand" 
                                runat="server"><%# Eval("FulfillmentNumber")%></asp:LinkButton>
                                
                                            
                                    <%--<%# DataBinder.Eval(Container.DataItem, "FulfillmentNumber")%>
                                    <asp:LinkButton ID="lnkPoNum" CommandArgument='mtracking05' OnCommand="lnkPoNum_OnCommand" 
                                runat="server"><%# Eval("FulfillmentNumber")%> test</asp:LinkButton>
                                
                                <asp:LinkButton ID="lnkRMA" CommandArgument='<%# Eval("RmaGUID")%>' OnCommand="lnkRMA_OnCommand" 
                                runat="server"><%# Eval("RmaNumber")%></asp:LinkButton>
                                
                                    --%>
                                </ItemTemplate>
                            </asp:TemplateField>                 
                            <asp:TemplateField ItemStyle-CssClass="copy10grey"  ItemStyle-Width="25%" HeaderText="RMA#">
                                <ItemTemplate>
                                
                                <asp:LinkButton ID="lnkRMA" CommandArgument='<%# Eval("RmaGUID")%>' OnCommand="lnkRMA_OnCommand" 
                                runat="server"><%# Eval("RmaNumber")%></asp:LinkButton>
                                
                                    
                                </ItemTemplate>
                            </asp:TemplateField>                 
                            <asp:TemplateField ItemStyle-CssClass="copy10grey"  ItemStyle-Width="5%" HeaderText="">
                                <ItemTemplate>
                                     <table><tr>
                                     
                                        <td>
                                            <asp:ImageButton ID="imgSim"  ToolTip="View SIM log" OnCommand="imgViewSimLog_Click"
                                             CausesValidation="false" CommandArgument='<%# Eval("SIM") %>' ImageUrl="~/Images/view.png"  runat="server" />
                                        </td>
                                
                                        
                                </tr></table>
                                </ItemTemplate>
                            </asp:TemplateField>  
                            

                      </Columns>
                      </asp:GridView>
                
                    </td>
                </tr>
                </table>
                        


                    </td>
                </tr>
            
        </table>






    </ContentTemplate>
            
    </asp:UpdatePanel>

    <div id="divFulfillmentContainer">
			<div id="divFulfillmentView" style="display:none">
					
				<asp:UpdatePanel ID="upnlView" runat="server">
					<ContentTemplate>
                   
						<asp:PlaceHolder ID="phrView" runat="server">
                            <%--<asp:Label ID="lblViewPO" runat="server"  CssClass="errormessage"></asp:Label>
    --%>
                            <asp:Panel ID="pnlPO" runat="server">
                            <PO:PoDetail ID="poDetail1" runat="server" />

                            </asp:Panel>


                        </asp:PlaceHolder>
						
						
					</ContentTemplate>
							
				</asp:UpdatePanel>
           </div>
           <div id="divRMA" style="display:none">
					
				<asp:UpdatePanel ID="upRMA" runat="server">
					<ContentTemplate>
                   
						<asp:PlaceHolder ID="phRMA" runat="server">
                            <%--<asp:Label ID="lblViewPO" runat="server"  CssClass="errormessage"></asp:Label>
    --%>
                            <asp:Panel ID="pnlRMA" runat="server">
                            <CT:RMA ID="ctRMA1" runat="server" />

                            </asp:Panel>


                        </asp:PlaceHolder>
						
						
					</ContentTemplate>
							
				</asp:UpdatePanel>
           </div>

            <div id="divSimLog" style="display:none">
					
				<asp:UpdatePanel ID="upSimLog" runat="server">
					<ContentTemplate>
                   
						<asp:PlaceHolder ID="phSimLog" runat="server">
        
                            <asp:Panel ID="pnlSim" runat="server">
                            <CT:SimLog ID="ctSimLog1" runat="server" />

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
        <br />
 
    <table cellspacing="0" cellpadding="0" border="0" align="center" width="100%">
        <tr>
            <td>
                <foot:MenuFooter ID="Foter" runat="server"></foot:MenuFooter>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
