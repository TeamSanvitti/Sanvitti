<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddressValidateSearch.aspx.cs" Inherits="avii.AddressValidateSearch" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="~/Controls/Header.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Validate Address</title>
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
        $("#divAddress").dialog({
            autoOpen: false,
            modal: false,
            minHeight: 400,
            height: 550,
            width: 900,
            resizable: false,
            open: function (event, ui) {
                $(this).parent().appendTo("#divContainer");
            }
        });

        

    });


    function closeDialog() {
        //Could cause an infinite loop because of "on close handling"
        $("#divAddress").dialog('close');
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
        $("#divAddress").dialog("option", "title", title);
        $("#divAddress").dialog("option", "position", [left, top]);
        $("#divAddress").dialog('open');

    }


    function openDialogAndBlock(title, linkID) {

        openDialog(title, linkID);
        //alert('2')
        //block it to clean out the data
        $("#divAddress").block({
            message: '<img src="../images/async.gif" />',
            css: { border: '0px' },
            fadeIn: 0,
            //fadeOut: 0,
            overlayCSS: { backgroundColor: '#ffffff', opacity: 1 }
        });
    }

    function unblockDialog() {
        $("#divAddress").unblock();
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
     </table><br />
 <table cellspacing="0" cellpadding="0" border="0" align="center" width="95%">
		<tr>
			<td  bgcolor="#dee7f6" class="buttonlabel">
            &nbsp;&nbsp;Validate Address
			</td>
		</tr>
    
    </table>

     <table cellSpacing="0" cellPadding="0" align="center" width="95%" border="0">
        <tr>
			<td>
           <div id="divContainer">	
              <div id="divAddress"  style="display:none">
                    <asp:UpdatePanel ID="upAdd" runat="server">
				        <ContentTemplate>
                        <table cellSpacing="0" cellPadding="0" align="center" width="95%" border="0">
                        <tr>
			                <td>
     
                        <asp:Repeater ID="rptLog" runat="server" OnItemDataBound="rptLog_ItemDataBound">
                                            <HeaderTemplate>
                                            <table width="100%" align="center">
                                                <tr >
                                                    <td class="buttonlabel" width="1%">
                                                        &nbsp;<%--S.No.--%>
                                                    </td>
                                                    <td class="buttonlabel" width="10%">
                                                        &nbsp;Address
                                                    </td>
                                                    <td class="buttonlabel" width="10%">
                                                        &nbsp;City
                                                    </td>
                                                    <td class="buttonlabel" width="10%">
                                                        &nbsp;State
                                                    </td>
                                                    <td class="buttonlabel" width="20%">
                                                        &nbsp;Zip Code
                                                    </td>
                                                    <td class="buttonlabel" width="20%">
                                                        &nbsp;Zip4
                                                    </td>
                                                    <%--<td class="buttonlabel" width="20%">
                                                        &nbsp;Action
                                                    </td>
                                                    --%>
                                                </tr>
                            
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                            <tr class="<%# Container.ItemIndex % 2 == 0 ? "alternaterow" : "" %>">
                                                
                                                <td class="copy10grey">
                                                        &nbsp;
                                                    <asp:CheckBox ID="chk" runat="server" />
                                                </td>
                                
                                                <td class="copy10grey">
                                                        &nbsp;<%# Eval("Address1")%>
                                                </td>
                                                <td class="copy10grey">
                                                        &nbsp;<%# Eval("City")%>
                                                </td>
                                                <td class="copy10grey">
                                                        &nbsp;<%# Eval("State")%>
                                                </td>
                                                <td class="copy10grey">
                                                        &nbsp;<%# Eval("PostalCode")%>
               
                                                </td>
                                                <td class="copy10grey">
                                                         
                                                     &nbsp;<%# Eval("Zip4")%>
                                                </td>
                                                <%--<td class="copy10grey">
                                                        &nbsp;<%# Eval("ActionName")%>
                                                </td>
                                                --%>
                                            </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                </table>    
                                            </FooterTemplate>
                                            </asp:Repeater<tr>
                            <td>
                                <hr />
                            </td>
                        </tr>

                        </td>
                        </tr>
                        <tr>
                            <td>
                                
                            </td>
                        </tr>

                        </table>
                    
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
             
        </div>
		
    

    <asp:UpdatePanel ID="upnlAdd" UpdateMode="Conditional" runat="server">
	<ContentTemplate>
	
    <table cellSpacing="0" cellPadding="0" align="center" width="100%" border="0">
    		<tr>                    
                <td >
                    <asp:Label ID="lblMsg" runat="server" Width="100%" CssClass="errormessage"></asp:Label></td>
            </tr> 
     </table> 
        
        
      <table bordercolor="#839abf" border="1" width="100%" align="center" cellpadding="0" cellspacing="0">
      <tr>
        <td>
         <asp:Panel  ID="pnlSearch" runat="server"  DefaultButton="btnSearch" >
     <table width="100%" border="0" class="box" align="center" cellpadding="2" cellspacing="2">
     <tr style="height:1px">
     <td style="height:1px"></td>
     </tr>
     
            <tr runat="server" id="trCustomer">
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
                <td  class="copy10grey" align="right" width="15%">
                        Status: &nbsp;</td>
                <td align="left"  width="30%">
                         &nbsp;<asp:DropDownList ID="ddlStatus" runat="server" class="copy10grey"  Width="80%">
                                <asp:ListItem Text="" Value=""></asp:ListItem>
                                <asp:ListItem Text="Pending" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Processed" Value="2"></asp:ListItem>
                                
                                <%--<asp:ListItem Text="In Process" Value="8"></asp:ListItem>
                                <asp:ListItem Text="Partial Processed" Value="10"></asp:ListItem>
                                <asp:ListItem Text="Partial Shipped" Value="11"></asp:ListItem>--%>
                                
                                
                                
                            </asp:DropDownList>

                </td>
                <td  width="1%">
                    &nbsp;
                </td>
                <td class="copy10grey"  align="right">
                   Fulfillment#:
                </td>
                <td>
                    <asp:TextBox ID="txtPoNum"  CssClass="copy10grey" runat="server" Width="80%" MaxLength="30" ></asp:TextBox>
                
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
            <asp:GridView ID="gvPO" runat="server" AutoGenerateColumns="false"  OnRowDataBound="gvPO_RowDataBound"
                  Width="100%" GridLines="Both">
                <RowStyle BackColor="Gainsboro" />
                <AlternatingRowStyle BackColor="white" />
                <HeaderStyle  CssClass="buttonlabel" ForeColor="white"/>
                <FooterStyle CssClass="white"  />
                <PagerStyle ForeColor="#636363" CssClass="copy10grey" HorizontalAlign="Left" />
                <Columns>
                     
                    <asp:TemplateField HeaderText="S.No." ItemStyle-CssClass="copy10grey"  ItemStyle-Width="1%" HeaderStyle-CssClass="buttonlabel">
                    <ItemTemplate>

                            <%# Container.DataItemIndex + 1%>
                  
                    </ItemTemplate>
                </asp:TemplateField> 
                    <asp:TemplateField HeaderText="Fulfillment" HeaderStyle-CssClass="buttonlabel" SortExpression="CategoryName" ItemStyle-HorizontalAlign="Left" 
                        ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                        <ItemTemplate><%#Eval("FulfillmentNumber")%></ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Date" HeaderStyle-CssClass="buttonlabel" SortExpression="SKU" ItemStyle-HorizontalAlign="Left" 
                        ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                        <ItemTemplate><%#Eval("FulfillmentDate")%></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Address1" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="15%">
                        <ItemTemplate><%#Eval("Address1")%></ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Address2" HeaderStyle-CssClass="buttonlabel"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" 
                        ItemStyle-Width="10%">
                        <ItemTemplate><%#Eval("Address2")%></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="City" HeaderStyle-CssClass="buttonlabel"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                        <ItemTemplate><%# Eval("City") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="State" HeaderStyle-CssClass="buttonlabel" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                        <ItemTemplate>
                            <%# Eval("State") %>

                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ValidationStatus" HeaderStyle-CssClass="buttonlabel" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" 
                        ItemStyle-Width="8%">
                        <ItemTemplate>
                            <%# Eval("ValidationStatus") %>

                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass="buttonlabel" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="copy10grey" ItemStyle-Width="3%">
                        <ItemTemplate>
                            
                             &nbsp; <asp:LinkButton ToolTip="Validate" CssClass="button" CausesValidation="false" OnCommand="lnkValidate_Command" CommandArgument='<%# Eval("POID") %>'  
                                                                ID="lnkValidate"  runat="server"  >Validate</asp:LinkButton>
               
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass="buttonlabel" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="copy10grey" ItemStyle-Width="3%">
                        <ItemTemplate>
                            
                             &nbsp; <asp:LinkButton ToolTip="Suggestion" CausesValidation="false" OnCommand="lnkSuggestion_Command" CommandArgument='<%# Eval("POID") %>'  
                                                                ID="lnkSuggestion"  runat="server"  >Suggestion</asp:LinkButton>
               
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
