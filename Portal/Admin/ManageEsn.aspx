<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageEsn.aspx.cs" Inherits="avii.Admin.ManageEsn" %>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="/Controls/Header.ascx" %>
<%@ Register TagPrefix="PO" TagName="Detail" Src="~/Controls/PODetails.ascx" %>
<%@ Register TagPrefix="RMA" TagName="Detail" Src="~/Controls/RMADetails.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../aerostyle.css" rel="stylesheet" type="text/css"/>
    
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
	        $("#divPO").dialog({
	            autoOpen: false,
	            modal: true,
	            minHeight: 400,
	            height: 400,
	            width: 900,
	            resizable: false,
	            open: function (event, ui) {
	                $(this).parent().appendTo("#divContainer");
	            }
	        });
	        $("#divRMA").dialog({
	            autoOpen: false,
	            modal: true,
	            minHeight: 20,
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
	        $("#divPO").dialog('close');
	    }
	    function closeRMADialog() {
	        //Could cause an infinite loop because of "on close handling"
	        $("#divRMA").dialog('close');
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
	        left = 225;
	        $("#divPO").dialog("option", "title", title);
	        $("#divPO").dialog("option", "position", [left, top]);

	        $("#divPO").dialog('open');
	        
	    }

	    function openRMADialog(title, linkID) {

	        var pos = $("#" + linkID).position();
	        var top = pos.top;
	        var left = pos.left + $("#" + linkID).width() + 10;
	        //alert(left);
	        //top = top - 300;
	        if (top > 600)
	            top = 10;
	        top = 100;
	        left = 250;
	        //$("#divHistory").html("");
	        //$('#divHistory').empty();
	        //alert($('#phrHistory'));
	        $("#divRMA").dialog("option", "title", title);
	        $("#divRMA").dialog("option", "position", [left, top]);

	        $("#divRMA").dialog('open');
	    }

	    function openDialogAndBlock(title, linkID) {
	        
	        openDialog(title, linkID);
	        //alert('2')
	        //block it to clean out the data
	        $("#divPO").block({
	            message: '<img src="../images/async.gif" />',
	            css: { border: '0px' },
	            fadeIn: 0,
	            //fadeOut: 0,
	            overlayCSS: { backgroundColor: '#ffffff', opacity: 1 }
	        });
	    }
	    function openRMADialogAndBlock(title, linkID) {
	        openRMADialog(title, linkID);

	        //block it to clean out the data
	        $("#divRMA").block({
	            message: '<img src="../images/async.gif" />',
	            css: { border: '0px' },
	            fadeIn: 0,
	            //fadeOut: 0,
	            overlayCSS: { backgroundColor: '#ffffff', opacity: 1 }
	        });
	    }


	    function unblockDialog() {
	        $("#divPO").unblock();
	    }
	    function unblockRMADialog() {
	        $("#divRMA").unblock();
	    }
        

		
	</script>
	
    <script type="text/javascript">
        var allCheckBoxSelector = '#<%=gvMSL.ClientID%> input[id*="chkAll"]:checkbox';
        var checkBoxSelector = '#<%=gvMSL.ClientID%> input[id*="chkEsn"]:checkbox';

        function ToggleCheckUncheckAllOptionAsNeeded() {
            var totalCheckboxes = $(checkBoxSelector),
         checkedCheckboxes = totalCheckboxes.filter(":checked"),
         noCheckboxesAreChecked = (checkedCheckboxes.length === 0),
         allCheckboxesAreChecked = (totalCheckboxes.length === checkedCheckboxes.length);

            $(allCheckBoxSelector).attr('checked', allCheckboxesAreChecked);
        }

        $(document).ready(function () {
            $(allCheckBoxSelector).live('click', function () {
                $(checkBoxSelector).attr('checked', $(this).is(':checked'));

                ToggleCheckUncheckAllOptionAsNeeded();
            });

            $(checkBoxSelector).live('click', ToggleCheckUncheckAllOptionAsNeeded);

            ToggleCheckUncheckAllOptionAsNeeded();
        });
</script>
    <script type="text/javascript" language="javascript">
        function SelectAll(chkbox) {
            var chk = document.getElementById(chkbox);
            var grid = document.getElementById("<%= gvMSL.ClientID %>");
            var cell;
            if (chk.checked == true) {
                if (grid.rows.length > 0) {
                    for (i = 1; i < grid.rows.length; i++) {
                        for (var k = 0; k < grid.rows[i].cells.length; k++) {
                            cell = grid.rows[i].cells[k];
                            for (j = 0; j < cell.childNodes.length; j++) {
                                if (cell.childNodes[j].type == "checkbox") {
                                    cell.childNodes[j].checked = true;
                                }
                            }
                        }
                    }
                }
            }
            else {
                if (grid.rows.length > 0) {
                    for (i = 1; i < grid.rows.length; i++) {
                        for (var k = 0; k < grid.rows[i].cells.length; k++) {
                            cell = grid.rows[i].cells[k];
                            for (j = 0; j < cell.childNodes.length; j++) {
                                if (cell.childNodes[j].type == "checkbox") {
                                    cell.childNodes[j].checked = false;
                                }
                            }
                        }
                    }
                }
            }
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
		<tr><td>&nbsp;</td></tr>
		<tr>
			<td  bgcolor="#dee7f6" class="button">&nbsp;&nbsp;ESN CLEAN UP
			</td>
		</tr>
    <tr><td>&nbsp;</td></tr>
    </table>
	
    

     <div id="divContainer">	
			<div id="divPO" style="display:none">
					
				<asp:UpdatePanel ID="upnlPO" runat="server">
					<ContentTemplate>
                   
						<asp:PlaceHolder ID="phrPO" runat="server">
                            <asp:Panel ID="pnlPO" runat="server">
                            <PO:Detail id="podetail" runat="server" ></PO:Detail>     
                            </asp:Panel>
                        </asp:PlaceHolder>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div id="divRMA" style="display:none">
					
				<asp:UpdatePanel ID="upRMA" runat="server" >
                
					<ContentTemplate>
                    
						<asp:PlaceHolder ID="phrRMA" runat="server">
                             <asp:Panel ID="pnlRMA" runat="server">
                                <RMA:Detail id="rma1" runat="server" ></RMA:Detail>
                            </asp:Panel>
                        </asp:PlaceHolder>
						
						
					</ContentTemplate>
							
				</asp:UpdatePanel>
			
			</div>
            </div>
            <asp:UpdatePanel ID="upnlCustomers" UpdateMode="Conditional" runat="server">
			<ContentTemplate>
	
    <table cellSpacing="0" cellPadding="0" align="center" width="100%" border="0">
    		<tr>                    
                <td colspan="2">
                    <asp:Label ID="lblMsg" runat="server" Width="100%" CssClass="errormessage"></asp:Label></td>
            </tr>               
            
            <tr>
                <td align="center">

                    <table  cellSpacing="1" cellPadding="1" width="100%">
                        <tr><td class="copy10grey" align="left">
                        
	                    - Upload file should be less than 2 MB. <br />&nbsp;
                        </td></tr>
                    </table>

                 <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="95%">
                    <tr>
                    <td>
			            <table id="Table2" cellSpacing="5" cellPadding="5" width="100%" border="0">
                        
                            <tr>
                                <td  class="copy10grey" align="right" >
                                    Upload ESN file: &nbsp;</td>
                                <td align="left" >
                                    <asp:FileUpload ID="flnUpload" runat="server" CssClass="txfield1" Width="50%" /></td>
                            </tr>
                            <tr id="trShipFile" >
                                <td class="copy10grey" align="right">
                                    File format sample: &nbsp;
                                         </td>
                                <td class="copy10grey" align="left">
                                   <b>ESN</b> (in csv (.csv) format)
                                </td>
                             </tr>
<tr  valign="top">
                                <td class="copy10grey" align="right">
                                    Comment: &nbsp;
                                         </td>
                                <td class="copy10grey" align="left">
                                    <asp:TextBox ID="txtComment" runat="server" CssClass="copy10grey" Width="70%" Height="50px" TextMode="MultiLine"></asp:TextBox>

                                </td>
                             </tr>
 

                             
                            <tr>
                                <td colspan="2">
                                    <asp:Panel ID="pnlSubmit" runat="server" Visible="false">
                                    <table cellpadding="0" cellspacing="0" width="100%">
                                        <tr><td>
                                        <hr style="width:100%" />
                            
                                        </td></tr>   
                                         <tr>                    
                                            <td align="left" >
                                                <asp:Label ID="lblConfirm" runat="server" Width="100%" CssClass="errorGreenMsg"></asp:Label></td>
                                        </tr>               
                                    
                                         <tr>
                                            <td  align="center">
                                
                                
                                            <asp:Button ID="btnSubmit2" Visible="false" Width="190px"  CssClass="button" runat="server" OnClick="btnSubmit_Click" Text="Submit" />

                                            &nbsp;<asp:Button ID="btnCancel2" runat="server"  CssClass="button" Text="Cancel" OnClick="btnCancel_Click" />
                                                </td>
                                        </tr>
                                        <tr><td>
                                        <hr style="width:100%" />
                            
                                        </td></tr>   
                                    </table>

                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr><td colspan="2">
                            
                            <table cellpadding="0" cellspacing="0" width="100%">
                             <tr>
                                <td colspan="2" align="right" style="height:8px; vertical-align:bottom">
                                <strong>   
                                    <asp:Label ID="lblCount" CssClass="copy10grey" runat="server" ></asp:Label> &nbsp;&nbsp;
                                </strong> 
                                </td>
                             </tr>
                             <tr>
                                <td colspan="2" align="center">
                                    <asp:GridView ID="gvMSL" runat="server" Width="100%" GridLines="Both"   AutoGenerateColumns="false"
                                    OnPageIndexChanging="gridView_PageIndexChanging" PageSize="100" AllowPaging="true" 
                                    OnRowDataBound="gvMSL_RowDataBound" >
                                        <RowStyle BackColor="Gainsboro" />
                                        <AlternatingRowStyle BackColor="white" />
                                        <HeaderStyle  CssClass="button" ForeColor="white"/>
                                        <FooterStyle CssClass="white"  />
                                        <PagerStyle ForeColor="#636363" CssClass="copy10grey" HorizontalAlign="Left" />
                                        <Columns>
                                            
                                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
                                                <HeaderTemplate>
                                                    <asp:CheckBox runat="server" ID="chkAll" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                
                                                    <asp:CheckBox runat="server" ID="chkEsn" Visible='<%# Convert.ToString(Eval("PurchaseOrderNumber")) != "" ? false : Convert.ToString(Eval("RmaNumber")) != "" ? false : Convert.ToString(Eval("MSLNumber")) == ""? false: true %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField> 
                                            
                                            <asp:TemplateField HeaderText="ESN#" SortExpression="ESN"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                
                                                    <asp:Label ID="lblESN" CssClass='<%# Convert.ToString(Eval("Item_code")) == "" && Convert.ToString(Eval("MSLNumber")) == "" && Convert.ToString(Eval("RmaNumber")) == "" ? "copy10greyStrik" : "copy10grey"%>' runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ESN")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField> 
                                            <asp:TemplateField HeaderText="MSL#" SortExpression="MSLNumber"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                                                <ItemTemplate><%#Eval("MSLNumber")%></ItemTemplate>
                                            </asp:TemplateField> 
                                            <%--<asp:TemplateField HeaderText="SKU#" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                                <ItemTemplate><%#Eval("SKU")%></ItemTemplate>
                                            </asp:TemplateField>
                            --%>
                                            <asp:TemplateField HeaderText="Product Code" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                                <ItemTemplate><%#Eval("Item_code")%></ItemTemplate>
                                            </asp:TemplateField>
                            
                                             <asp:TemplateField HeaderText="AVSO" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="7%">
                                                <ItemTemplate><%#Eval("AerovoiceSalesOrderNumber")%></ItemTemplate>
                                            </asp:TemplateField> 
                                            <asp:TemplateField HeaderText="MEID" SortExpression="Meid"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="8%">
                                                <ItemTemplate><%# DataBinder.Eval(Container.DataItem, "Meid")%></ItemTemplate>
                                            </asp:TemplateField> 
                                            <asp:TemplateField HeaderText="HEX" SortExpression="HEX"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="8%">
                                                <ItemTemplate><%# DataBinder.Eval(Container.DataItem, "HEX")%></ItemTemplate>
                                            </asp:TemplateField> 
                                            <asp:TemplateField HeaderText="AKEY" SortExpression="AKEY"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="8%">
                                                <ItemTemplate><%# DataBinder.Eval(Container.DataItem, "AKEY")%></ItemTemplate>
                                            </asp:TemplateField> 
                                            <asp:TemplateField HeaderText="AVPO" SortExpression="AVPO"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="7%">
                                                <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "AVPO")%>
                                                </ItemTemplate>
                                            </asp:TemplateField> 
                            
                                            <asp:TemplateField HeaderText="Otksl" SortExpression="Otksl"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="8%">
                                                <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "Otksl")%>
                                                </ItemTemplate>
                                            </asp:TemplateField> 
                                            <asp:TemplateField HeaderText="ICC ID"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="8%">
                                                <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "icc_id")%>
                                                </ItemTemplate>
                                            </asp:TemplateField> 
                                            <asp:TemplateField HeaderText="LTE IMSI" SortExpression="lte_imsi"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="8%">
                                                <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "lte_imsi")%>
                                                </ItemTemplate>
                                            </asp:TemplateField> 

                                            <asp:TemplateField HeaderText="PO#" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="7%">
                                                <ItemTemplate>
                                
                                                <%--<%#Eval("PurchaseOrderNumber")%>--%>
                                                <asp:LinkButton Visible='<%# Convert.ToInt32(Eval("po_id")) ==0 ? false : true %>' ToolTip="View PO" CausesValidation="false" OnCommand="imgViewPO_Click" CommandArgument='<%# Eval("po_id") %>'  ID="lnkPO"  runat="server" Text='<%#Eval("PurchaseOrderNumber")%>' />
                 
                                                </ItemTemplate>
                                            </asp:TemplateField> 
                                            <asp:TemplateField HeaderText="Rma#" SortExpression="RmaNumber"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="7%">
                                                <ItemTemplate>
                               
                                               <%--<%#Eval("RmaNumber")%> --%>
                                                <asp:LinkButton ToolTip="View RMA" Visible='<%# Convert.ToInt32(Eval("rmaGuid")) ==0 ? false : true %>' CausesValidation="false" OnCommand="imgViewRMA_Click" CommandArgument='<%# Eval("rmaGuid") %>'  ID="lnkRMA"  runat="server" Text='<%#Eval("RmaNumber")%>' ></asp:LinkButton>
                
                                                </ItemTemplate>
                                            </asp:TemplateField> 
                            

                                            <%--<asp:TemplateField  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderText="View Log">
                                                <ItemTemplate>
                                                <img src="/Images/view.png" onclick="doOnLoad('<%# DataBinder.Eval(Container.DataItem, "ESN")%>')" id="view" alt="View ESN Log" />
                                
                                                </ItemTemplate>
                                            </asp:TemplateField>                        --%>
                            
                                        </Columns>
                                    </asp:GridView>
                                    

                                </td>
                            </tr>
                            </table>
                            </td></tr>                            
                            
                            <tr><td colspan="2">
                            <hr style="width:100%" />
                            
                            </td></tr>                            
                             <tr>
                                <td colspan="2" align="center">
                                
                                <asp:Button ID="btnUpload"  Width="190px" CssClass="button" runat="server" OnClick="btnValidateUploadedFile_Click" Text="Validate Uploaded file" />

                                &nbsp;<asp:Button ID="btnSubmit" Visible="false" Width="190px"  CssClass="button" runat="server" OnClick="btnSubmit_Click" Text="Submit" />
                               

                                &nbsp;<asp:Button ID="btnCancel" runat="server"  CssClass="button" Text="Cancel" OnClick="btnCancel_Click" />
                                    </td>
                            </tr>
                       </table>
                            
                    </td>
                    </tr>
                           
                 </table>
                        


                    </td>
                </tr>
            
        </table>

        </ContentTemplate>
        <Triggers>
                <asp:PostBackTrigger ControlID="btnUpload" />
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
	

    </form>
</body>
</html>
