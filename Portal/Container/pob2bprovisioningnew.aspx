<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="pob2bprovisioningnew.aspx.cs" Inherits="avii.Container.pob2bprovisioningnew" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="~/Controls/Header.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    
    <title>Provisioning</title>
   
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
	
	

	
	
<!-- fix for 1.1em default font size in Jquery UI -->
	<style type="text/css">
		.ui-widget{font-size:11px !important;}
		.ui-state-error-text{margin-left: 10px}
	</style>
<script type="text/javascript">
    $(document).ready(function () {
        $("#divESN").dialog({
            autoOpen: false,
            modal: false,
            minHeight: 400,
            height: 450,
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


        </script>
    <script type="text/javascript">
        function isQuantity(obj) {

            if (obj.value == '0') {
                alert('Quantity can not be zero');
               // obj.value = '1';
                return false;
            }
            if (obj.value == '') {
                alert('Quantity can not be empty');
              //  obj.value = '1';
                return false;
            }
        }
        function ValidateQuantity() {
            if (obj.value == '0') {
                alert('Quantity can not be zero');
                //obj.value = '1';
                return false;
            }
            if (obj.value == '') {
                alert('Quantity can not be empty');
//obj.value = '1';
                return false;
            }
        }
        function isNumberKey(evt) {

            var charCodes = evt.keyCode ? evt.keyCode : evt.which ? evt.which : evt.charCode;
            if (charCodes > 31 && (charCodes < 48 || charCodes > 57)) {
                charCodes = 0;
                return false;
            }
            return true;
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
<body leftmargin="0" topmargin="0" marginwidth="0" marginheight="0">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    
    <table cellSpacing="0" cellPadding="0" align="center" width="100%" border="0">
        <tr>
			<td><head:MenuHeader id="HeadAdmin" runat="server"></head:MenuHeader></td>
		</tr>
     </table>
 <table cellspacing="0" cellpadding="0" border="0" align="center" width="95%">
		<tr>
			<td  bgcolor="#dee7f6" class="buttonlabel">
            &nbsp;&nbsp;Provisioning - B2B
			</td>
		</tr>
    <%--<tr>
        <td>&nbsp;</td>
    </tr>--%>
    </table>
        <div id="divContainer">	
            
			<div id="divESN" style="display:none">
					
				<asp:UpdatePanel ID="upnlESN" runat="server">
					<ContentTemplate>
                   
						<asp:PlaceHolder ID="phrESN" runat="server">
                        <table width="100%" border="0">
                                
                                <tr>
                                    <td colspan="2">
                                
                                    <asp:Panel ID="pnlESN" runat="server" Width="100%">
                                        <asp:Label ID="lblEsnMsg" runat="server" Width="100%" CssClass="errormessage"></asp:Label>
                                          <asp:Repeater ID="rptContainers" runat="server" Visible="true" >
                                            <HeaderTemplate>
                                            <table bordercolor="#839abf" border="1" width="100%" cellpadding="1" cellspacing="1">
                                            <tr>
                                                <td class="buttongrid"  width="1%" >
                                                    S.No.
                                                </td>
                                                <td class="buttongrid"  width="10%">
                                                    Container ID
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
                                                        <%# Eval("ContainerID")%>    
                                                        </span>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                            </table>
                                            </FooterTemplate>
                                            </asp:Repeater>
                               
                                    
                            </asp:Panel>
                            </td>
                                </tr>
                                </table>
                                
                        </asp:PlaceHolder>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <table cellSpacing="0" cellPadding="0" align="center" width="95%" border="0">
        <tr>
			<td>
    <asp:UpdatePanel ID="upnlCustomers" UpdateMode="Conditional" runat="server">
	<ContentTemplate>
	
    <table cellSpacing="0" cellPadding="0" align="center" width="100%" border="0">
    		<tr>                    
                <td >
                    <asp:Label ID="lblMsg" runat="server" CssClass="errormessage"></asp:Label></td>
            </tr>             
     </table> 
        
        
      <table bordercolor="#839abf" border="1" width="100%" align="center" cellpadding="0" cellspacing="0">
      <tr>
        <td>
         <asp:Panel  ID="pnlSearch" runat="server"  DefaultButton="btnSearch" >
     <table width="100%" border="0" class="box" align="center" cellpadding="2" cellspacing="2">
     <%--<tr style="height:1px">
     <td style="height:1px"></td>
     </tr>
     --%>
            <tr>
                <td  class="copy10grey" align="right" width="15%">
                        Customer: &nbsp;</td>
                <td align="left"  width="20%">
                        <asp:DropDownList ID="dpCompany" CssClass="copy10grey" runat="server" Width="80%">
				    </asp:DropDownList>
                </td>
                <td  width="1%">
                    &nbsp;
                </td>
                <td class="copy10grey"  align="right">
                   
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td class="copy10grey"  align="right">
                   Fulfillment#:
                </td>
                <td>
                <asp:TextBox ID="txtPoNum"  CssClass="copy10grey" runat="server" Width="80%" MaxLength="20" ></asp:TextBox>
                
                </td>
                <td  width="1%">
                    &nbsp;
                </td>
                <td class="copy10grey"  align="right">
                   Tracking#:
                </td>
                <td>
                <asp:TextBox ID="txtTrackingNo"   CssClass="copy10grey" runat="server" Width="80%" MaxLength="20" ></asp:TextBox>
                
                </td>   
                </tr>
                
                
                <tr>
                <td colspan="5">
                <hr />
                </td>
                </tr>
                <tr>
                <td  align="center"  colspan="5">
                    <div class="loadingcss" align="center" id="modalSending">
                                    <img src="/Images/ajax-loaders.gif" alt=""  /> Loading...
                                </div>
            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="button" OnClick="btnSearch_Click" CausesValidation="false"  OnClientClick="return ShowSendingProgress();"/>
            
         &nbsp;
           <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="button" OnClick="btnCancel_Click" CausesValidation="false"/>
                   
        </td>
        </tr>
            </table>
            </asp:Panel>
   
     </td>
     </tr>
     </table>  
        <br />
        <table align="center" style="text-align:left" width="100%">
        <%--<tr>
            
            <td colspan="3"  align="right" style="height:8px; vertical-align:bottom">
                        
            </td>
        </tr>--%>
        <tr>
            <td colspan="3" align="left">

                <asp:Label ID="lblNonEsn" runat="server" Width="100%" CssClass="buttonlabel"></asp:Label>

                                <asp:Repeater ID="rptSKU" runat="server" Visible="true" >
                                <HeaderTemplate>
                                <table bordercolor="#839abf" border="1" width="100%" cellpadding="1" cellspacing="1">
                                <tr>
                                    <td class="buttongrid"  width="2%" >
                                        S.No.
                                    </td>
                                    <td class="buttongrid"  width="10%">
                                        Category Name
                                    </td>
                                    <td class="buttongrid"  width="25%">
                                        Product Name
                                    </td>
                                    <td class="buttongrid" width="23%">
                                        SKU
                                    </td>
                                    <td class="buttongrid" width="15%">
                                        Qty 
                                    </td>
                                    
                                    <td class="buttongrid" width="15%">
                                        Current Stock 
                                    </td>
                                    
                                    <td class="buttongrid" width="5%">
                                        Assign
                                    </td>
                                    <td class="buttongrid" width="5%">
                                        
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
                                        <td>
                                        <span width="100%" class="errormessage">
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
            <td colspan="3" align="left">
                  <asp:Label ID="lblEsn" runat="server" Width="100%" CssClass="buttonlabel"></asp:Label>

            <asp:GridView ID="gvPOSKUs" runat="server" AutoGenerateColumns="false" 
                  Width="100%" GridLines="Both">
                <RowStyle BackColor="Gainsboro" />
                <AlternatingRowStyle BackColor="white" />
                <HeaderStyle  CssClass="buttongrid" ForeColor="white"/>
                <FooterStyle CssClass="white"  />
                <PagerStyle ForeColor="#636363" CssClass="copy10grey" HorizontalAlign="Left" />
                <Columns>
                     
                    <asp:TemplateField HeaderText="S.No." ItemStyle-CssClass="copy10grey"  ItemStyle-Width="1%" HeaderStyle-CssClass="buttongrid">
                    <ItemTemplate>

                            <%# Container.DataItemIndex + 1%>
                  
                    </ItemTemplate>
                </asp:TemplateField> 
                    <asp:TemplateField HeaderText="Category Name" HeaderStyle-CssClass="buttongrid" SortExpression="CategoryName" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                        <ItemTemplate><%#Eval("CategoryName")%></ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="SKU" HeaderStyle-CssClass="buttongrid" SortExpression="SKU" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                        <ItemTemplate><%#Eval("SKU")%></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Product Name" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="20%">
                        <ItemTemplate><%#Eval("ProductName")%></ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Quantity" HeaderStyle-CssClass="buttongrid"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                        <ItemTemplate><%#Eval("Quantity")%></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Quantity/Container" HeaderStyle-CssClass="buttongrid"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                        <ItemTemplate><%# Eval("ContainerQuantity") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Container Required" HeaderStyle-CssClass="buttongrid" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="8%">
                        <ItemTemplate>
                            <%# Eval("ContainerRequired") %>

                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Current Stock" HeaderStyle-CssClass="buttongrid" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="8%">
                        <ItemTemplate>
                            <%# Eval("CurrentStock") %>

                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="" HeaderStyle-CssClass="buttongrid" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="8%">
                        <ItemTemplate>
                           <span width="100%" class="errormessage">
                                            <%# Eval("ErrorMessage")%>    
                                            </span>

                        </ItemTemplate>
                    </asp:TemplateField>
                    
                   
                    
                </Columns>
            </asp:GridView>
  
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>

                </td>
            </tr>
            <tr>
                <td align="left" class="copy10grey" width="35%">
                   

                    
                    <table width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td align="left" style="vertical-align:middle">
                            
                            </td>
                            <td align="left" class="copy10grey" style="vertical-align:middle">
                                <asp:Button ID="btnGenContainerID" runat="server" Text="View Container ID(s)" Visible="false" OnClientClick="openDialogAndBlock('Container IDs', 'btnGenContainerID')"
                                    CssClass="button"  OnClick="btnGenContainerID_Click"    CausesValidation="false"/>
                                <%--OnClientClick="return ValidateQuantity();" --%>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" >
                                
                            </td>
                        </tr>
                    </table>
                </td>
                <td width="1%">
                    &nbsp;
                </td>
                <td width="53%" align="right">
                    
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <table bordercolor="#839abf" border="1" width="100%" align="center" cellpadding="0" cellspacing="0" id="trUpload" runat="server" visible="false" >
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
                                            <asp:FileUpload ID="fu" runat="server" CssClass="txfield1" Width="80%" /></td>
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
                                            <b>ContainerID, ESN</b>, Location
                                        </td>
                                    </tr>
                                </table>
                                </FooterTemplate>
                                </asp:Repeater>


<%--            <table width="100%" align="center" cellpadding="5" cellspacing="5">
            <tr id="trUpload" runat="server" visible="false">
                <td class="copy10grey" align="right" >
                         Upload ESN file: &nbsp;
                </td>
                <td></td>
                <td>
                    <asp:FileUpload ID="flnUpload" runat="server" CssClass="txfield1" Width="50%" />

                </td>
                
            </tr>
            <tr id="trFormat" runat="server" visible="false">
                <td class="copy10grey" align="right">
                    File format sample: &nbsp;
                </td>
                <td></td>
                <td class="copy10grey"   >
                    <b>ContainerID, ESN</b>
                </td>
            </tr>
                </table>--%>
                                
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="5" align="right" class="copy10grey">
                    <asp:LinkButton ID="lnkDownload" runat="server"  Visible="false"   Text="Download file format" OnClick="lnkDownload_Click"></asp:LinkButton>
                    &nbsp;
                     <asp:Button ID="btnUpload" Visible="false"  Width="190px" CssClass="button" runat="server" OnClick="btnValidateUploadedFile_Click" Text="Validate Uploaded file" />
                                &nbsp;
                    
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" Visible="false" CssClass="button"  OnClick="btnSubmit_Click"   CausesValidation="false"  OnClientClick="return ShowSendingProgress();"/>
                     &nbsp;
                    <asp:Button ID="btnCancel1" runat="server" Text="Cancel" Visible="false" CssClass="button" OnClick="btnCancel1_Click" CausesValidation="false"/>
                  
                </td>

            </tr>
            <tr>
                <td colspan="5" align="center">
                    <asp:Repeater ID="rptESN" runat="server" Visible="true" >
                                <HeaderTemplate>
                                <table bordercolor="#839abf" border="1" width="98%" cellpadding="1" cellspacing="1">
                                <tr>
                                    <td class="buttongrid" width="1%" >
                                        S.No.
                                    </td>
                                    <td class="buttongrid"  width="10%">
                                        Container ID
                                    </td>
                                    
                                    <td class="buttongrid"  width="8%">
                                        Category Name
                                    </td>
                                    <td class="buttongrid"  width="21%">
                                        Product Name
                                    </td>
                                    <td class="buttongrid"  width="18%">
                                        SKU#
                                    </td>
                                    <td class="buttongrid"  width="10%">
                                       ESN
                                    </td>
                                   <td class="buttongrid"  width="6%">
                                       ICCID
                                    </td>
                                    
                                    <td class="buttongrid"  width="6%">
                                       Batch#
                                    </td>
                                    
                                    <td class="buttongrid"  width="6%">
                                    Location   
                                    </td>
                                   <td class="buttongrid"  width="14%">
                                        Result
                                    </td>
                                </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
    
                                    <tr valign="bottom"  class="<%# Container.ItemIndex % 2 == 0 ? "alternaterow" : "" %>">
                                        <td class="copy10grey"  >
                                        <%# Container.ItemIndex +  1 %>
                                        </td>
                                        <td valign="bottom" class="copy10grey"  >
                                        <%-- <asp:TextBox ID="txtContainerID" Text='<%# Eval("ContainerID")%>' Width="90%" runat="server"></asp:TextBox>--%>
                                            <%# Eval("ContainerID")%>
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
                                            <%# Eval("ESN")%>

<%--                                        <%--    <asp:TextBox ID="txtESN" Text='<%# Eval("ESN")%>' Width="70%" runat="server"></asp:TextBox>
                                            <asp:HiddenField ID="hdSKUID" Value='<%# Eval("ItemCompanyGUID")%>' runat="server" />
                                            --%><%--<asp:Label ID="lblErrMsg" CssClass="errormessage" Text='<%# Eval("ErrorMessage")%>' runat="server"></asp:Label>
                                            --%>
                                        </td>
                                        <td valign="bottom" class="copy10grey"  >
                                        <span width="100%">
                                            <%# Eval("ICCID")%>    
                                            </span>
                                        </td>
                                        
                                        <td valign="bottom" class="copy10grey"  >
                                        <span width="100%">
                                            <%# Eval("BatchNumber")%>    
                                            </span>
                                        </td>
                                        <td valign="bottom" class="copy10grey"  >
                                        <span width="100%">
                                            <%# Eval("Location")%>    
                                            </span>
                                        </td>
                                        <td valign="bottom" class="copy10grey"  >
                                        <span width="100%">
                                            <asp:Label ID="lblErrMsg" CssClass="errormessage" Text='<%# Eval("ErrorMessage") + " " + Eval("LocationMessage") %>' runat="server"></asp:Label>
                                                
                                            </span>
                                        </td>
                                        
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                </table>
                                </FooterTemplate>
                                </asp:Repeater>
                                
                </td>
            </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnUpload" />
            <asp:PostBackTrigger ControlID="btnSearch" />
            <asp:PostBackTrigger ControlID="btnSubmit" />
            <asp:PostBackTrigger ControlID="btnCancel1" />
            <asp:PostBackTrigger ControlID="btnCancel" />
            <asp:PostBackTrigger ControlID="lnkDownload" />
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
                </td>
    </tr>

    </table>
        <br /> <br />
            <br /> <br />
        <table width="100%">
        <tr>
		    <td>
			    <foot:MenuFooter id="Menuheader2" runat="server"></foot:MenuFooter>
		    </td>
	    </tr>
        </table>
        
   <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>

    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.4.0/jquery.min.js"></script>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/jquery-ui.min.js"></script>
    <link href="https://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/themes/start/jquery-ui.css"
    rel="stylesheet" type="text/css" />
	<script type="text/javascript" src="../JQuery/jquery.blockUI.js"></script>
	

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
