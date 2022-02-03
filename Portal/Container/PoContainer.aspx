<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PoContainer.aspx.cs" Inherits="avii.Container.PoContainer" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="~/Controls/Header.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    
    <title>Generate Container ID</title>
   
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
            if (obj.value != '' && obj.value != '0') {
                var objCon = document.getElementById('<%= hdContainers.ClientID %>');
                if (objCon != null && objCon.value != "") {
                    if (obj.value < objCon.value) {

                        obj.value = objCon.value;
                        alert('Not allowed!');

                        return false;
                    }
                }
            }
        }
        function isPalletQuantity(obj) {
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
            if (obj.value != '' && obj.value != '0') {
                var objCon = document.getElementById('<%= hdPallets.ClientID %>');
                if (objCon != null && objCon.value != "") {
                    if (obj.value < objCon.value) {

                        obj.value = objCon.value;
                        alert('Not allowed!');

                        return false;
                    }
                }
            }
        }
        function ValidateQuantity() {
            var obj = document.getElementById('<%= txtContainers.ClientID %>');
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
</head>
<body>
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
            &nbsp;&nbsp;Generate Container ID
			</td>
		</tr>
    
    </table>
        <%--<div id="divContainer">	
            
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
                                                <td class="buttonlabel"  width="1%" >
                                                    S.No.
                                                </td>
                                                <td class="buttonlabel"  width="10%">
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
        </div>--%>
    <table cellSpacing="0" cellPadding="0" align="center" width="95%" border="0">
        <tr>
			<td>
    <asp:UpdatePanel ID="upnlCustomers" UpdateMode="Conditional" runat="server">
	<ContentTemplate>
	
    <table cellSpacing="0" cellPadding="0" align="center" width="100%" border="0">
    		<tr>                    
                <td><asp:Label ID="lblMsg" runat="server"  CssClass="errormessage"></asp:Label></td>
            </tr> 
     </table> 
        
        
      <table bordercolor="#839abf" border="1" width="100%" align="center" cellpadding="0" cellspacing="0">
      <tr>
        <td>
         <asp:Panel  ID="pnlSearch" runat="server"  DefaultButton="btnSearch" >
     <table width="100%" border="0" class="box" align="center" cellpadding="2" cellspacing="2">   
            <tr>
                <td  class="copy10grey" align="right" width="15%">
                        Customer: &nbsp;</td>
                <td align="left"  width="30%">
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
                <asp:TextBox ID="txtPoNum"  CssClass="copy10grey" runat="server" Width="80%" MaxLength="30" ></asp:TextBox>
                
                </td>
                <td  width="1%">
                    &nbsp;
                </td>
                <td class="copy10grey"  align="right">
                   Tracking#:
                </td>
                <td>
                <asp:TextBox ID="txtTrackingNo"   CssClass="copy10grey" runat="server" Width="60%" MaxLength="30" ></asp:TextBox>
                
                </td>   
                </tr>
                
                
                <tr>
                <td colspan="5">
                <hr />
                </td>
                </tr>
                <tr>
                <td  align="center"  colspan="5">
            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="button" OnClick="btnSearch_Click" CausesValidation="false"/>
         &nbsp;   <asp:Button ID="btnBoxID" runat="server" Text="BOX Label"   CssClass="button" Visible="false"   OnClick="btnBoxID_Click"    CausesValidation="false"/>
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
        <tr>
            
            <td colspan="3"  align="right" style="height:8px; vertical-align:bottom">
                        
            </td>
        </tr>

        <tr>
            <td colspan="3" align="center">
            <asp:GridView ID="gvPOSKUs" runat="server" AutoGenerateColumns="false"  OnRowDataBound="gvPOSKUs_RowDataBound"
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
                    <asp:TemplateField HeaderText="Pallet Required" HeaderStyle-CssClass="buttongrid" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="8%">
                        <ItemTemplate>
                            <asp:HiddenField ID="hdPallet" Value='<%# Eval("PalletRequired") %>' runat="server" />
                            <%# Eval("PalletRequired") %>

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
                <td colspan="3" align="left" class="copy10grey" >
                   

                    <table bordercolor="#839abf" border="1" width="100%" align="center" cellpadding="0" cellspacing="0" id="tblContainer" runat="server" >
                      <tr>
                        <td>
                        
                        <table width="100%" cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td width="50%">
                        <table width="100%" cellpadding="5" cellspacing="5" border="0">
                        <tr style="vertical-align:top">
                            <td align="left" style="vertical-align:middle" width="40%">
                            <asp:Label ID="lblReqContainer" runat="server" Visible="false" CssClass="copy10grey" Text="Container Required:"></asp:Label>   
                                <asp:TextBox ID="txtContainers"   Visible="false" onkeypress="return isNumberKey(event);" onchange="return isQuantity(this);" CssClass="copy10grey" runat="server" MaxLength="4" ></asp:TextBox>
                                <asp:HiddenField ID="hdContainers" runat="server" />     
                            </td>
                            <td></td>
                            <td align="left" class="copy10grey" style="vertical-align:middle">
                                <asp:Button ID="btnGenContainerID" runat="server" Text="Generate" Visible="false"  CssClass="button"  OnClick="btnGenContainerID_Click"    CausesValidation="false"/>
                                
                                <%--OnClientClick="return ValidateQuantity();" --%>
                            </td>
                        </tr>
                                    <tr>
                                        <td></td>
                                        <td>
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
                                                    <%--<td class="buttongrid"  width="10%">BOX#</td>--%>
                                                    <td class="buttongrid"  ></td>
                                                </tr>
                                                </HeaderTemplate>
                                                <ItemTemplate>
    
                                                    <tr valign="bottom"  class="<%# Container.ItemIndex % 2 == 0 ? "alternaterow" : "" %>">
                                                        <td class="copy10grey"  >
                                                        <%# Container.ItemIndex +  1 %>
                                                        </td>
                                                        <td valign="bottom" class="copy10grey"  >
                                                        <span width="100%">
                                                                
                                                          <%--<asp:CheckBox ID="chkC" runat="server" Enabled='<%# Convert.ToInt32(Eval("POID")) > 0 ? false : true %>' />--%>
                                                            <%# Eval("ContainerID")%>    
                                                            </span>
                                                            <%--<asp:HiddenField ID="hdContainerID" runat="server" Value='<%# Eval("ContainerID")%>  ' />--%>
                                                        </td>
                                                        <%--<td>
                                                            <%# Eval("Code")%>  
                                                        </td>--%>
                                                        <td valign="bottom" class="copy10grey" width="1%"  >
                                                        
                                                                
                                                          <asp:ImageButton ID="imgDel" runat="server" ImageUrl="~/images/delete.png" OnClientClick="return confirm('Are you sure you want to delete?')" 
                                                              CommandArgument='<%# Eval("ContainerID")%>' Visible='<%# Convert.ToInt32(Eval("POID")) == 0 ? true :false  %>' OnCommand="imdDel_Command" 
                                                              AlternateText="Delete Container"/>      
                                                         
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
                            </td>
                            <td style="vertical-align:top">
                                <table width="100%" cellpadding="5" cellspacing="5" border="0" id="tblPallet" runat="server">
                                <tr>
                                    <td align="left" style="vertical-align:top" width="40%">
                            <asp:Label ID="lblReqPallet" runat="server"  CssClass="copy10grey" Text="Pallet Required:"></asp:Label>   
                                <asp:TextBox ID="txtPallets"    onkeypress="return isNumberKey(event);" onchange="return isPalletQuantity(this);" CssClass="copy10grey" runat="server" MaxLength="2" ></asp:TextBox>
                                <asp:HiddenField ID="hdPallets" runat="server" />     
                            
                                    </td>
                                    <td>

                                    </td>
                                </tr>
                                <tr>
                                <td>
                                 <asp:Repeater ID="rptPallets" runat="server" Visible="true" >
                                    <HeaderTemplate>
                                    <table bordercolor="#839abf" border="1" width="100%" cellpadding="1" cellspacing="1">
                                    <tr>
                                        <td class="buttongrid"  width="1%" >
                                            S.No.
                                        </td>
                                        <td class="buttongrid"  width="10%">
                                            Pallet ID
                                        </td>
                                        <td class="buttongrid"  ></td>
                                    </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>    
                                        <tr valign="bottom"  class="<%# Container.ItemIndex % 2 == 0 ? "alternaterow" : "" %>">
                                            <td class="copy10grey"  >
                                            <%# Container.ItemIndex +  1 %>
                                            </td>
                                            <td valign="bottom" class="copy10grey"  >
                                            <span width="100%">
                                                                
                                                <%--<asp:CheckBox ID="chkC" runat="server" Enabled='<%# Convert.ToInt32(Eval("POID")) > 0 ? false : true %>' />--%>
                                                <%# Eval("PalletID")%>    
                                                </span> 
                                            </td>
                                            <td valign="bottom" class="copy10grey" width="1%"  >                                                        
                                                                
                                                <asp:ImageButton ID="imgPalletDel" runat="server" ImageUrl="~/images/delete.png" OnClientClick="return confirm('Are you sure you want to delete?')" 
                                                    CommandArgument='<%# Eval("PalletID")%>' Visible='<%# Convert.ToInt32(Eval("POID")) == 0 ? true :false  %>' OnCommand="imgPalletDel_Command" 
                                                    AlternateText="Delete Pallet"/>      
                                                         
                                            </td>
                                        </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                        </table>
                                    </FooterTemplate>
                                    </asp:Repeater>
                                </td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td class="copy10grey">
                                        Country of Origin:  <asp:TextBox ID="txtComment"   CssClass="copy10grey" runat="server" MaxLength="50" ></asp:TextBox>
                                
                                    </td>
                                    <td>

                                    </td>
                                </tr>
                                </table>
                            </td>
                            
                        </tr>
                            <tr >
                                <td colspan="2">
                                    <hr />
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" Visible="false" CssClass="button"  OnClick="btnSubmit_Click"    CausesValidation="false"/>
                                <asp:Button ID="btnDelete" runat="server" Text="Delete" Visible="false" CssClass="button"  OnClick="btnDelete_Click"    CausesValidation="false"/>
                                 &nbsp;
                                <asp:Button ID="btnCancel1" runat="server" Text="Cancel" Visible="false" CssClass="button" OnClick="btnCancel_Click" CausesValidation="false"/>
                  
                            </td>
                            </tr>
                            </table>
                            
                        </tr>
                        
                    </table>
                            </td>
                          </tr>
                        </table>
                </td>
            </tr>
            <tr>
                <td>

                </td>
                <td>

                </td>
            </tr>
            </table>
            </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnBoxID" />
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
	
    </form>
</body>
</html>
