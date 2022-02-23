﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PalletContainerMapping.aspx.cs" Inherits="avii.Container.PalletContainerMapping" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="~/Controls/Header.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Container - Pallet Mapping</title>
   
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
            &nbsp;&nbsp;Container - Pallet Mapping
			</td>
		</tr>
    
    </table>
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
                  <asp:Button ID="btnBoxID" runat="server" Text="BOX Label"   CssClass="button" Visible="false"   OnClick="btnBoxID_Click"    CausesValidation="false"/>
                &nbsp; 
                <asp:Button ID="btnMapping" Visible="false" runat="server" CssClass="button" Text="Download Pallets Mapping" OnClick="btnMapping_Click" />       
            </td>
        </tr>
        <tr>
            <td colspan="3" align="center">
                <asp:Repeater ID="rptMapping" runat="server" Visible="true" OnItemDataBound="rptMapping_ItemDataBound" >
                        <HeaderTemplate>
                        <table bordercolor="#839abf" border="1" width="100%" cellpadding="1" cellspacing="1">
                        <tr>
                            <td class="buttongrid"  width="1%" >
                                S.No.
                            </td>
                            <td class="buttongrid"  width="40%">
                                Container ID
                            </td>
                            <td class="buttongrid"  width="10%">
                                BOX#
                            </td>
                            <td class="buttongrid"  width="40%">Pallet ID</td>
                        </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
    
                            <tr valign="bottom"  class="<%# Container.ItemIndex % 2 == 0 ? "alternaterow" : "" %>">
                                <td class="copy10grey"  >
                                <%# Container.ItemIndex +  1 %>
                                </td>
                                <td valign="bottom" class="copy10grey"  >
                                    <asp:Label ID="lblContainer" runat="server" Text='<%# Eval("ContainerID")%>' CssClass="copy10grey"></asp:Label> 
                                    <asp:HiddenField ID="hdPalletID" runat="server" Value='<%# Eval("PalletID")%>' />
                                <%--<span width="100%">
                                     
                                        
                                    </span> --%>
                                </td>
                                 <td valign="bottom" class="copy10grey"  >
                                    <%# Eval("Code")%>
                                </td>
                               
                                <td valign="bottom" class="copy10grey" width="1%"  >
                                                        
                                                                
                                    <asp:DropDownList ID="ddlPallet" CssClass="copy10grey" runat="server" Width="40%">
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
                <td>
                    &nbsp;
                </td>
                <td>

                </td>
            </tr>
            <tr>
                <td colspan="3" align="center" class="copy10grey" >
                   

                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" Visible="false" CssClass="button"  OnClick="btnSubmit_Click"    CausesValidation="false"/>
                                <asp:Button ID="btnDelete" runat="server" Text="Delete" Visible="false" CssClass="button"  OnClick="btnDelete_Click"    CausesValidation="false"/>
                                 &nbsp;
                                <asp:Button ID="btnCancel1" runat="server" Text="Cancel" Visible="false" CssClass="button" OnClick="btnCancel_Click" CausesValidation="false"/>
                  
                            
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
            <asp:PostBackTrigger ControlID="btnMapping" />
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
