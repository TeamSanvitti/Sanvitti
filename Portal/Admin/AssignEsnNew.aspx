<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AssignEsnNew.aspx.cs" Inherits="avii.Admin.AssignEsnNew" %>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="/Controls/Header.ascx" %>
<%@ Register TagPrefix="PO" TagName="PoDetail" Src="~/Controls/PODetails.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>.:: Lan Global inc. ::.</title>
    <link href="../aerostyle.css" rel="stylesheet" type="text/css"/>
    
    <!-- from http://encosia.com/2009/10/11/do-you-know-about-this-undocumented-google-cdn-feature/ -->
    <link href="https://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/themes/start/jquery-ui.css" type="text/css" rel="Stylesheet" />
	
	<script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.4.0/jquery.min.js"></script>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/jquery-ui.min.js"></script>
    <%--<script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js" type="text/javascript"></script>
<script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js" type="text/javascript"></script>--%>
	<%--<script type="text/javascript" src="JQuery/jquery.min.js"></script>
    <script type="text/javascript" src="JQuery/jquery-ui.min.js"></script>
	--%><script type="text/javascript" src="../JQuery/jquery.blockUI.js"></script>
	
<!-- fix for 1.1em default font size in Jquery UI -->
	<style type="text/css">
		.ui-widget{font-size:11px !important;}
		.ui-state-error-text{margin-left: 10px}
	</style>
    <script type="text/javascript">
        $(document).ready(function () {
            //  alert('1')
            var increementIndex = <%=IncreementIndex%>;
            //alert(increementIndex);
            $('input:text:first').focus();
            //  alert('2')
            $('input:text').bind('keypress', function (e) {
                // alert('3')
                if (e.keyCode == 13) {
                    //  alert('4')

                    e.preventDefault();
                    var nextIndex = $('input:text').index(this) + increementIndex;
                    var maxIndex = $('input:text').length;
                    //  alert(maxIndex);
                    if (nextIndex < maxIndex) {
                        //  alert('5')
                        $('input:text:eq(' + nextIndex + ')').focus();
                        //e.preventDefault();
                    }

                }

            });
        });
    </script>
	<script type="text/javascript">

	    $(document).ready(function () {

	        
	        $("#divFulfillmentView").dialog({
	            autoOpen: false,
	            modal: true,
	            minHeight: 20,
	            height: 640,
	            width: 1124,
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
	            //top = top - 600;
	            left = 150;
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

	    </script>

    <script language="javascript" type="text/javascript">



        function KeyDownHandler(btn) {

            if (event.keyCode == 13) {
                event.returnValue = false;
                event.cancel = true;
                btn.click();
            }
        }
    </script>

     <style>
.progresss {
          position: fixed !important;
          z-index: 9999 !important;
          top: 0px !important;
          left: 0px !important;
          background-color: #EEEEEE !important;
          width: 100% !important;
          height: 100% !important;
          filter: Alpha(Opacity=80) !important;
          opacity: 0.80 !important;
          -moz-cpacity: 0.80 !important;
      }
.modal
{
    position: fixed;
    top: 0;
    left: 0;
    background-color: black;
    z-index: 100000000;
    opacity: 0.8;
    filter: alpha(opacity=80);
    -moz-opacity: 0.8;
    min-height: 100%;
    width: 100%;
}
.loadingcss
{    
    font-size: 18px;
    /*border: 1px solid red;*/
    /*width: 200px;
    height: 100px;*/
    display: none;
    position: fixed;
    /*background-color: White;*/
    z-index: 100000001;
    background-color:#CF4342;
}

  </style>


</head>
<body  leftmargin="0" topmargin="0" marginwidth="0" marginheight="0"> 
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table cellSpacing="0" cellPadding="0" align="center" width="100%" border="0">
        	<tr>
				<td><head:MenuHeader id="HeadAdmin" runat="server"></head:MenuHeader></td>
			</tr>
			</table>
        <table cellSpacing="0" cellPadding="0" align="center" width="95%" border="0">
			<tr>
				<td bgcolor="#dee7f6"  class="buttonlabel">&nbsp;&nbsp;Provisioning - B2C
                    <%--Assign ESN to Fulfillment Orders--%>
				</td>
			</tr>
            </table>
	        
            <asp:UpdatePanel ID="UpdatePanel1"  runat="server" UpdateMode="Conditional" >
            <ContentTemplate>
            <table cellSpacing="0" cellPadding="0" align="center" width="95%" border="0">
    		<tr>                    
                <td colspan="2">
                    <asp:Label ID="lblMsg" runat="server" CssClass="errormessage"></asp:Label>

                </td>
            </tr>               
            
            <tr>
                <td align="center">


                 <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="100%">
                    <tr>
                    <td>
			            <table id="Table2" cellSpacing="5" cellPadding="5" width="100%" border="0">
                        <tr>
                            <td colspan="5"> 
                                <asp:Panel ID="pnlSearch" runat="server" DefaultButton="btnSearch">
                        <table cellSpacing="5" cellPadding="5" width="100%" border="0">
                        <tr>
                                <td  class="copy10grey" align="right" width="15%">
                                     Customer: &nbsp;</td>
                                <td align="left"  width="20%">
                                     <asp:DropDownList ID="dpCompany" CssClass="copy10grey" runat="server" Width="80%">
									</asp:DropDownList>
                                    </td>
                            <%--</tr>
                            <tr>--%>
                                <td  class="copy10grey" align="right"  width="15%">
                                     Fulfillment Number: &nbsp;</td>
                                <td align="left"  width="20%">
                                  <asp:TextBox    ID="txtPO" MaxLength="20" CssClass="copy10grey" runat="server" Width="80%"></asp:TextBox>
									
                                    </td>
                            <td  align="center"  width="30%">
                                <div class="loadingcss" align="center" id="modalSending">
                                    <img src="/Images/ajax-loaders.gif" alt=""  /> Loading...
                                </div>
                                    <asp:Button ID="btnSearch" runat="server" CssClass="button"  Text="Search Order" OnClick="btnSearch_Click" CausesValidation="false" OnClientClick="return ShowSendingProgress();"/>
                                &nbsp; <asp:Button ID="btnShCancel" runat="server" CssClass="button"  Text="Cancel" OnClick="btnCancel_Click" />
                                </td>
                            </tr>
                            </table>
                                    </asp:Panel>
                            </td>
                        </tr>
                            </table>
                        </td>
                        </tr>
                     </table>
                    
                            <%--<tr><td colspan="5" align="center">
                                        <hr style="width:100%" />
                            
                                        </td></tr> 
                            <tr>
                                
                            </tr>--%>
                   <%-- <table bordercolor="#839abf" border="0" cellSpacing="0" cellPadding="0" width="100%">
                    <tr>
                    <td>--%>
                            <table id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
                       
                             <tr>
                                <td colspan="5" align="left">
                                    <table id="tblNonESN" visible="false" runat="server"><tr><td>&nbsp;</td></tr></table>
                                    <asp:Label ID="lblNonEsn" runat="server" Width="100%" CssClass="buttonlabel"></asp:Label>

                                <asp:Repeater ID="rptSKU" runat="server" Visible="true" >
                                <HeaderTemplate>
                                    
                                <table bordercolor="#839abf" border="1" width="100%" cellpadding="1" cellspacing="1">
                                <tr>
                                    <td class="buttonlabel"  width="2%" >
                                        S.No.
                                    </td>
                                    <td class="buttonlabel"  width="10%">
                                        Category Name
                                    </td>
                                    <td class="buttonlabel"  width="25%">
                                        Product Name
                                    </td>
                                    <td class="buttonlabel" width="23%">
                                        SKU
                                    </td>
                                    <td class="buttonlabel" width="15%">
                                        Qty 
                                    </td>
                                    
                                    <td class="buttonlabel" width="10%">
                                        Current Stock 
                                    </td>
                                    
                                    <td class="buttonlabel" width="10%">
                                        Assign
                                    </td>
                                    <td class="buttonlabel" width="10%">
                                        
                                    </td
                                </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
    
                                    <tr valign="bottom"  class="<%# Container.ItemIndex % 2 == 0 ? "alternaterow" : "" %>">
                                        <td class="copy10grey"  >
                                        <%# Container.ItemIndex +  1 %>
                                        </td>
                                        <td valign="bottom" class="copy10grey"  >
                                        <span width="100%">
                                            <%# Eval("CategoryName")%>    
                                            </span>
                                        </td>
                                        <td valign="bottom" class="copy10grey"  >
                                        <span width="100%">
                                            <%# Eval("ProductName")%>    
                                            </span>
                                        </td>
                                        <td valign="bottom" class="copy10grey"  >
                                        <span width="100%">
                                            <%# Eval("SKU")%>    
                                            </span>
                                        </td>
                                         <td valign="bottom" class="copy10grey"  >
                                        <span width="100%">
                                            <%# Eval("Qty")%>    
                                            </span>
                                        </td>
                                        <td valign="bottom" class="copy10grey" >
                                        <span width="100%">
                                            <%# Eval("CurrentStock")%>    
                                            </span>
                                        </td>
                                       
                                        <td valign="bottom" class="copy10grey"  >
                                        <span width="100%">
                                            <asp:CheckBox ID="chkAssign" Enabled='<%# Eval("IsAssign")%>' runat="server" Checked='<%# Eval("IsAssign")%>' />
                                            
                                            </span>
                                        </td>
                                         <td valign="bottom"  class="errormessage" >
                                        <span width="100%">
                                            <%# Eval("ErrorMessage")%>    
                                            </span>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                </table>
                                </FooterTemplate>
                                </asp:Repeater>
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="5" align="left">
                                    <asp:Label ID="lblEsn" runat="server" Width="100%" CssClass="buttonlabel"></asp:Label>

                                <asp:Repeater ID="rptSKUESN" runat="server" Visible="true"  >
                                <HeaderTemplate>
                                <table bordercolor="#839abf" border="1" width="100%" cellpadding="1" cellspacing="1">
                                <tr>
                                    <td class="buttonlabel" width="2%">
                                        S.No.
                                    </td>
                                    <td class="buttonlabel"  width="18%">
                                        Category Name
                                    </td>
                                    <td class="buttonlabel"  width="30%">
                                        Product Name
                                    </td>
                                    <td class="buttonlabel" width="20%">
                                        SKU
                                    </td>
                                    <td class="buttonlabel" width="10%">
                                       Required Qty 
                                    </td>
                                    <td class="buttonlabel" width="10%">
                                        Current Stock 
                                    </td>
                                    <td class="buttonlabel" width="10%">
                                        
                                    </td>
                                </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
    
                                    <tr valign="bottom"  class="<%# Container.ItemIndex % 2 == 0 ? "alternaterow" : "" %>">
                                        <td class="copy10grey"  >
                                        <%# Container.ItemIndex +  1 %>
                                        </td>
                                        <td valign="bottom" class="copy10grey"  >
                                        <span width="100%">
                                            <%# Eval("CategoryName")%>    
                                            </span>
                                        </td>
                                        <td valign="bottom" class="copy10grey"  >
                                        <span width="100%">
                                            <%# Eval("ProductName")%>    
                                            </span>
                                        </td>
                                        <td valign="bottom" class="copy10grey"  >
                                        <span width="100%">
                                            <%# Eval("SKU")%>    
                                            </span>
                                        </td>
                                         <td valign="bottom" class="copy10grey"  >
                                        <span width="100%">
                                            <%# Eval("Qty")%>    
                                            </span>
                                        </td>
                                        <td valign="bottom" class="copy10grey"  >
                                        <span width="100%">
                                            <%# Eval("CurrentStock")%>    
                                            </span>
                                        </td>
                                        
                                         <td valign="bottom"  class="errormessage" >
                                        <span width="100%">
                                            <%# Eval("ErrorMessage")%>    
                                            </span>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                </table>
                                </FooterTemplate>
                                </asp:Repeater>
                                    <br />
                                </td>
                            </tr>
                            <tr runat="server" id="row1" visible="false">
                                <td colspan="5">
                                 <table bordercolor="#839abf" border="1" width="98%" cellpadding="2" cellspacing="2">
                                 <tr>
                                     <td>

                                <asp:Repeater ID="rptUpload" runat="server" Visible="true" OnItemDataBound="rptUpload_OnItemDataBound" >
                                <HeaderTemplate>
                                <table bordercolor="#839abf" border="0" width="100%" cellpadding="5" cellspacing="5">
                                </HeaderTemplate>
                                <ItemTemplate>    
                                    <tr valign="bottom"  >
                                        <td  class="copy10grey" align="right" width="20%">
                                            Tracking #: &nbsp;
                                        </td>
                                        <td align="left"  width="30%">
                                            <asp:DropDownList ID="ddlTrackingNo" CssClass="copy10grey" runat="server" Width="80%">
									        </asp:DropDownList>                                    
                                        </td>
                                        <td  class="copy10grey" align="right" width="20%">
                                            Upload ESN file: &nbsp;
                                        </td>
                                        <td align="left"  width="30%">
                                            <asp:FileUpload ID="fu" runat="server" CssClass="txfield1" Width="80%" />
                                        </td>
                                    </tr>                                    
                                </ItemTemplate>
                                <FooterTemplate>
                                    <tr>
                                        <td></td>
                                        <td></td>
                                        <td class="copy10grey" align="right">
                                    File format sample: &nbsp;
                                         </td>
                                <td class="copy10grey" align="left" colspan="4">
                                   <b>ESN</b><%--,Fmupc,msl,otksl,akey,LteICCID,LteIMSI--%>
                                </td>
                                        
                                    </tr>
                                </table>
                                </FooterTemplate>
                                </asp:Repeater>

                                     </td>
                                 </tr>
                                 </table>
                                </td>
                                
                            </tr>
                            <tr runat="server" id="row11" visible="false">
                                <td  class="copy10grey" align="right" >
                                    Upload ESN file: &nbsp;</td>
                                <td align="left" colspan="4">
                                    <asp:FileUpload ID="flnUpload" runat="server" CssClass="txfield1" Width="50%" /></td>
                            </tr>
                            <tr id="row2" runat="server"  visible="false">
                                <td class="copy10grey" align="right">
                                    <%--File format sample: &nbsp;--%>
                                         </td>
                                <td class="copy10grey" align="left" colspan="4">
                                   <%--<b>ESN</b>--%>
                                </td>
                             </tr>
                            <tr id="row3" runat="server"  visible="false">
                                <td colspan="5" align="left">
                                <asp:Repeater ID="rptScanESN" runat="server" Visible="true" OnItemDataBound="rptScanESN_OnItemDataBound" >
                                <HeaderTemplate>
                                <table bordercolor="#839abf" border="1" width="100%" cellpadding="1" cellspacing="1">
                                <tr>
                                    <td class="buttonlabel" width="1%" >
                                        S.No.
                                    </td>
                                    <td class="buttonlabel"  width="8%">
                                        Category Name
                                    </td>
                                    <td class="buttonlabel"  width="23%">
                                        Product Name
                                    </td>
                                    <td class="buttonlabel"  width="18%">
                                        SKU#
                                    </td>
                                    <td class="buttonlabel"  width="25%">
                                       ESN
                                    </td>
                                    <td class="buttonlabel"  width="8%"  >
                                        <%--<td class="buttonlabel"  width="14%" runat="server" visible='<%# IncreementIndex == 2 ? true:false%>' >--%>
                                        ICCID
                                    </td>
                                    <td class="buttonlabel"  width="3%">
                                       Batch#
                                    </td>
                                    <td class="buttonlabel"  width="12%">
                                        Tracking#
                                    </td>
                                   
                                </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
    
                                    <tr valign="bottom"  class="<%# Container.ItemIndex % 2 == 0 ? "alternaterow" : "" %>">
                                        <td class="copy10grey"  >
                                        <%# Container.ItemIndex +  1 %>
                                        </td>
                                        
                                        <td valign="bottom" class="copy10grey"  >
                                        <span width="100%">
                                            <%# Eval("CategoryName")%>    
                                            </span>
                                        </td>
                                        <td valign="bottom" class="copy10grey"  >
                                        <span width="100%">
                                            <%# Eval("ProductName")%>    
                                            </span>
                                        </td>
                                        <td valign="bottom" class="copy10grey"  >
                                        <span width="100%">
                                            <%# Eval("SKU")%>    
                                            
                                            </span>
                                            
                                        </td>
                                         
                                        <td valign="bottom" class="copy10grey"  >
                                            <asp:TextBox ID="txtESN" Text='<%# Eval("ESN")%>' Width="70%" runat="server"></asp:TextBox>
                                            <asp:HiddenField ID="hdSKUID" Value='<%# Eval("ItemCompanyGUID")%>' runat="server" />
                                            <asp:Label ID="lblErrMsg" CssClass="errormessage" Text='<%# Eval("ErrorMessage")%>' runat="server"></asp:Label>
                                            
                                        </td>
                                        <td valign="bottom" class="copy10grey"  runat="server" >
                                           <%-- <td valign="bottom" class="copy10grey"  runat="server" visible='<%# IncreementIndex == 2 ? true:false%>'>--%>
                                        <span width="100%">
                                            <%--<asp:TextBox ID="txtICCID" Enabled="false" Text='<%# Eval("LteICCID")%>' Width="99%" runat="server"></asp:TextBox>--%>
                                            <asp:Label ID="txtICCID"  Text='<%# Eval("LteICCID")%>' Width="99%" runat="server"></asp:Label>
                                                <%--<%# Eval("LteICCID")%>  --%>
                                            </span>
                                        </td>
                                        <td valign="bottom" class="copy10grey"  >
                                        <span width="100%">
                                            <%# Eval("BatchNumber")%>    
                                            </span>
                                        </td>
                                        <td valign="bottom" class="copy10grey"  >
                                            <asp:HiddenField ID="hdTN" Value='<%# Eval("TrackingNumber")%>' runat="server" />
                                             <asp:DropDownList ID="ddlTrackNo"    CssClass="copy10grey" runat="server" Width="90%">
									        </asp:DropDownList>
                                    
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                </table>
                                </FooterTemplate>
                                </asp:Repeater>
                                </td>
                             </tr>
                             
                            <tr>
                                <td colspan="5">
                                    <asp:Panel ID="pnlSubmit" runat="server" Visible="false">
                                    <table cellpadding="0" cellspacing="0" width="100%">
                                        <tr><td>
                                        <hr style="width:100%" />
                            
                                        </td></tr>   
                                         <tr>                    
                                            <td >
                                                <asp:Label ID="lblConfirm" runat="server" Width="100%" CssClass="errorGreenMsg"></asp:Label></td>
                                        </tr>               
                                    
                                         <tr>
                                            <td  align="center">
                                
                                
                                            <asp:Button ID="btnSubmit2" Visible="false" Width="190px"  CssClass="button" runat="server" OnClick="btnSubmit_Click" Text="Submit" />

                                            &nbsp;<asp:Button ID="btnCancel2" runat="server"  CssClass="button" Text="Cancel" OnClick="btnCancel_Click" />
                                                </td>
                                        </tr>
                                        <tr id="trHR" runat="server"><td>
                                        <hr style="width:100%" />
                            
                                        </td></tr>   
                                    </table>

                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr><td colspan="5">
                            
                            <table cellpadding="0" cellspacing="0" width="100%">
                             <tr>
                                <td colspan="2" align="right" style="height:8px; vertical-align:bottom">
                                <strong>   <asp:Label ID="lblCount" CssClass="copy10grey" runat="server" ></asp:Label></strong> 
                                </td>
                             </tr>
                             <tr>
                                <td colspan="2" align="center">
                                
                                <asp:Repeater ID="rptESN" runat="server" Visible="true" OnItemDataBound="rpt_OnItemDataBound">
                                <HeaderTemplate>
                                <table border="0" width="100%" cellpadding="1" cellspacing="1">
                                <tr>
                                    <td class="button" >
                                        S.No.
                                    </td>
                                    <td class="button" >
                                        Fulfillment Number
                                    </td>
                                    <%--<td class="button" >
                                        CustomerAccountNumber
                                    </td>--%>
                                    
                                    <td class="button">
                                        SKU
                                    </td>
                                    <td class="button">
                                        ESN 
                                    </td>
                                    <%--<td class="button">
                                        FMUPC 
                                    </td>--%>
                                    <td class="button">
                                        Batch Number 
                                    </td>
                                    <%--<td class="button">
                                        OTKSL
                                    </td>
                                    <td class="button">
                                        AKEY
                                    </td>--%>
                                    <td class="button" >
                                        ICCID
                                    </td>
                                    <%--<td class="button" >
                                        LTEIMSI
                                    </td>--%>
                                    
                                    
                                </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
    
                                    <tr valign="bottom"  class="<%# Container.ItemIndex % 2 == 0 ? "alternaterow" : "" %>">
                                        <td class="copy10grey"  >
                                        <%# Container.ItemIndex +  1 %>
                                        </td>
                                        <td valign="bottom" class="copy10grey"  style="background-color:<%# Convert.ToString(Eval("FulfillmentNumber")) == "" ? "red" : ""%>">
                                        <span width="100%" >
                                            <asp:LinkButton ID="lnkPoNum" Visible="false" CommandArgument='<%# Eval("FulfillmentNumber") + "," + Eval("CustomerAccountNumber") %>' OnCommand="lnkPoNum_OnCommand" runat="server"><%# Eval("FulfillmentNumber")%></asp:LinkButton>
                                                
                                            <%# Eval("FulfillmentNumber")%>
                                            <%--<%# Convert.ToString(Eval("FulfillmentNumber")) == "" ? "red" : ""%>    
                                            --%>
                                            </span>
                                        </td>
                                        <%--<td align="left" class="copy10grey" valign="bottom" style="background-color:<%# Convert.ToString(Eval("CustomerAccountNumber")) == "" ? "red" : ""%>">
                                            <%# Eval("CustomerAccountNumber")%>   
                                        </td>--%>
                                        <td valign="bottom" class="copy10grey"  >
                                        <span width="100%">
                                            <%# Eval("SKU")%>    
                                            </span>
                                        </td>
                                        <td valign="bottom" class="copy10grey"  style="background-color:<%# Convert.ToString(Eval("ESN")) == "" ? "red" : ""%>">
                                        <span width="100%">
                                            <%# Eval("ESN")%>    
                                            </span>
                                        </td>
                                        <%--<td valign="bottom" class="copy10grey" >  style="background-color:<%# Convert.ToString(Eval("SKU")) == "" ? "red" : ""%>"
                                        <span width="100%">
                                            <%# Eval("FMUPC")%>    
                                            </span>
                                        </td>--%>
                                        <td valign="bottom" class="copy10grey"  >
                                        <span width="100%">
                                            <%# Eval("BatchNumber")%>    
                                            </span>
                                        </td>
                                        
                                       <%--  <td align="left" class="copy10grey" valign="bottom">
                                            <%# Eval("OTKSL")%>   
            
                                        </td>
                                        <td align="left" class="copy10grey" valign="bottom">
                                            <%# Eval("AKEY")%>   
            
                                       </td>--%>
                                        <td align="left" class="copy10grey" valign="bottom">
                                            <%# Eval("LteICCID")%>   
            
                                        </td>
                                       <%-- <td align="left" class="copy10grey" valign="bottom">
                                            <%# Eval("LTEIMSI")%>   
            
                                        </td>--%>
                                        
                                        
                                        
                                        
                                    </tr>
    
    

    
                                </ItemTemplate>
                                <FooterTemplate>
                                </table>
                                </FooterTemplate>
                                </asp:Repeater>

                                </td>
                            </tr>
                            </table>
                            </td></tr>                            
                            
                            <tr runat="server" id="hrRow" visible="false"><td colspan="5">
                                <hr style="width:100%" />
                            
                            </td></tr>                            
                             <tr>
                                <td colspan="5" align="center" class="copy10grey">
                                <asp:LinkButton ID="lnkDownload" runat="server"  Visible="false"   Text="Download file format" OnClick="lnkDownload_Click"></asp:LinkButton>
                    &nbsp;
                                <asp:Button ID="btnUpload" Visible="false"  Width="190px" CssClass="button" runat="server" OnClick="btnValidateUploadedFile_Click" 
                                    Text="Validate Uploaded file" />
                                 <asp:Button ID="btnValidate" Visible="false"  Width="190px" CssClass="button" runat="server" OnClick="btnValidate_Click" Text="Validate" />

                                &nbsp;<asp:Button ID="btnSubmit" Visible="false" Width="190px"  CssClass="button" runat="server" OnClick="btnSubmit_Click" Text="Submit" />
                                &nbsp;<asp:Button ID="btnViewAssignedESN" Visible="false" Width="190px"  CssClass="button" runat="server" OnClick="btnViewAssignedESN_Click" Text="View Assigned ESN" />

                                &nbsp;<asp:Button ID="btnCancel" Visible="false" runat="server"  CssClass="button" Text="Cancel" OnClick="btnCancel_Click" />
                                    </td>
                            </tr>
                       </table>
                            
                    <%--</td>
                    </tr>
                           
                 </table>
                        --%>


                    </td>
                </tr>
            
        </table>

            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnUpload" />
                <asp:PostBackTrigger ControlID="btnSearch" />
                <asp:PostBackTrigger ControlID="btnSubmit" />
                <asp:PostBackTrigger ControlID="lnkDownload" />
                

                
            </Triggers>
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
           </div>

           <asp:UpdatePanel ID="upnlJsRunner" UpdateMode="Always" runat="server">
			<ContentTemplate>
				<asp:PlaceHolder ID="phrJsRunner" runat="server"></asp:PlaceHolder>
			</ContentTemplate>
		    </asp:UpdatePanel>

     <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.4.0/jquery.min.js"></script>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/jquery-ui.min.js"></script>
        <script type="text/javascript" src="../JQuery/jquery.blockUI.js"></script>
        <script type="text/javascript" src="/JSLibrary/jquery.blockUI.js"></script>

        <script type="text/javascript">
            function ShowSendingProgress() {
                var modal = $('<div  />');
                modal.addClass("modal");
                modal.attr("id", "modalSending");
                $('body').append(modal);
                var loading = $("#modalSending.loadingcss");
                //alert(loading);
                loading.show();
                var top = '300px';
                var left = '820px';
                loading.css({ top: top, left: left, color: '#ffffff' });

                var tb = $("maintbl");
                tb.addClass("progresss");
                // alert(tb);

                return true;
            }
            //background-color:#CF4342;

            function StopProgress() {

                $("div.modal").hide();

                var tb = $("maintbl");
                tb.removeClass("progresss");


                var loading = $(".loadingcss");
                loading.hide();
            }
        </script>
    </form>
</body>
</html>

