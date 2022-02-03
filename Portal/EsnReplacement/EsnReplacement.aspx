<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EsnReplacement.aspx.cs" Inherits="avii.EsnReplacement.EsnReplacement" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="/Controls/Footer.ascx" %>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="/Controls/Header.ascx" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ESN Relacement</title>
       <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>

    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.4.0/jquery.min.js"></script>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/jquery-ui.min.js"></script>
    <link href="https://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/themes/start/jquery-ui.css"
    rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function isNumberKey(evt) {

            var charCodes = evt.keyCode ? evt.keyCode : evt.which ? evt.which : evt.charCode;
            if (charCodes > 31 && (charCodes < 48 || charCodes > 57)) {
                charCodes = 0;
                return false;
            }
            return true;
        }
        function isNumberHiphen(evt) {

            var charCodes = evt.keyCode ? evt.keyCode : evt.which ? evt.which : evt.charCode;
            if (((((charCodes > 31 && (charCodes < 48 || charCodes > 57) && charCodes != 45) && charCodes != 46) && charCodes != 95) && !(charCodes > 96 && charCodes < 123)) && !(charCodes > 64 && charCodes < 91)) {
                // alert(charCodes);

                charCodes = 0;
                return false;
            }

            return true;
        }
    </script>
  <%--<script type="text/javascript">

var timeoutTime = 100;
var timeoutTimer = setTimeout(ShowTimeOutWarning, timeoutTime);
$(document).ready(function() {
    $('body').bind('mousedown keydown', function(event) {
        clearTimeout(timeoutTimer);
        timeoutTimer = setTimeout(ShowTimeOutWarning, timeoutTime);
    });
});

      function ShowTimeOutWarning() {
          //alert('time out');
      }
  </script>--%>
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
<body bgColor="#ffffff" leftmargin="0" rightmargin="0" topmargin="0">
    <form id="form1" runat="server">
        
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
	<table cellSpacing="0" cellPadding="0"  border="0" align="center" width="100%">
		<tr>
			<td>
			<head:menuheader id="HeadAdmin" runat="server"></head:menuheader>
			</td>
		</tr>
     </table>
    <table  cellSpacing="1" cellPadding="1" width="95%" align="center" >
        <tr>
		    <td colSpan="6" bgcolor="#dee7f6" class="buttonlabel">&nbsp;&nbsp;ESN Relacement
		    </td>
        </tr>
    </table> 
     <asp:UpdatePanel ID="UpdatePanel1"  runat="server" UpdateMode="Conditional" >
        <ContentTemplate>
   
    <table cellSpacing="0" cellPadding="0" align="center" width="95%" border="0">
    	<tr>                    
            <td colspan="2">
                <asp:Label ID="lblMsg" runat="server" Width="100%" CssClass="errormessage"></asp:Label>
            </td>
        </tr>
        </table>
        <table cellSpacing="0" cellPadding="0" align="center" width="95%" border="0">
    
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
                        
                        <tr>
                                <td  class="copy10grey" align="right" width="10%">
                                    <b> Customer:</b> &nbsp;</td>
                                <td align="left"  width="18%">
                                     <asp:DropDownList ID="dpCompany" CssClass="copy10grey" runat="server" Width="90%">
									</asp:DropDownList>
                                    </td>
                            <%--</tr>
                            <tr>--%>
                                <td  class="copy10grey" align="right"  width="10%">
                                    <b> Fulfillment Number: </b>&nbsp;</td>
                                <td align="left"  width="18%">
                                  <asp:TextBox    ID="txtPO" MaxLength="20" onkeypress="return isNumberHiphen(event);" CssClass="copy10grey" runat="server" Width="90%"></asp:TextBox>
									
                                    </td>
                                <td  class="copy10grey" align="right"  width="10%">
                                    <b> ESN:</b> &nbsp;</td>
                                <td align="left"  width="18%">
                                  <asp:TextBox   onkeypress="return isNumberKey(event);"  ID="txtESN" MaxLength="20" CssClass="copy10grey" runat="server" Width="90%"></asp:TextBox>
									
                                    </td>

                            <td  align="center"  width="15%">
                                <div class="loadingcss" align="center" id="modalSending">
                                    <img src="/Images/ajax-loaders.gif" alt=""  /> Loading...
                                </div>
                                    <asp:Button ID="btnSearch" runat="server" CssClass="button"  Text="Search Order" OnClick="btnSearch_Click"  CausesValidation="false" OnClientClick="return ShowSendingProgress();" />
                                 &nbsp;<asp:Button ID="btnCancel1" Visible="true" runat="server"  CssClass="button" Text="Cancel" OnClick="btnCancel_Click" />
                                 
                                </td>
                            </tr>
                            </table>
                                    </asp:Panel>
                            </td>
                        </tr>

                            <%--<tr><td colspan="5" align="center">
                                        <hr style="width:100%" />
                            
                                        </td></tr> --%>
                            </table>
                          </td>
                    </tr>
                           
                 </table>
                  <br />
                            <table bordercolor="#839abf" border="0" cellSpacing="0" cellPadding="0" width="100%" id="tblPO" runat="server" visible="false">
                                <tr>
		                            <td  bgcolor="#dee7f6" class="buttonlabel">&nbsp;&nbsp;Fulfillment Detail
		                            </td>
                                </tr>
                            <tr>
                                 <td >
                                     <table bordercolor="#839abf" border="1" width="100%" cellpadding="1" cellspacing="1">
                                    <tr>
                                     <td>
                                     <table bordercolor="#839abf" border="0" width="100%" cellpadding="1" cellspacing="1">
                                    <tr>
                                        <td class="copy10grey"  with="15%" align="right">
                                            Fulfillment#:
                                        </td>
                                        <td class="copy10grey"  with="35%" align="left">
                                            &nbsp;&nbsp;<asp:Label ID="lblFulfillmentNo" CssClass="copy10grey" runat="server"></asp:Label>
                                        </td>
                                        <td class="copy10grey"  with="15%" align="right">
                                            Status:
                                        </td>
                                        <td class="copy10grey"  with="35%"  align="left">
                                            &nbsp;&nbsp;<asp:Label ID="lblStatus" CssClass="copy10grey" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="copy10grey"  with="15%" align="right">
                                            Fulfillment Date:
                                        </td>
                                        <td class="copy10grey"  with="35%"  align="left">
                                            &nbsp;&nbsp;<asp:Label ID="lblPODate" CssClass="copy10grey" runat="server"></asp:Label>
                                        </td>
                                        <td class="copy10grey"  with="15%" align="right">
                                            Contact Name:
                                        </td>
                                        <td class="copy10grey"  with="35%">
                                            &nbsp;&nbsp;<asp:Label ID="lblContactName" CssClass="copy10grey" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    </table>
                                         </td>
                                        </tr>
                                         </table>

                                 </td>
                             </tr>
                          <tr>
                              <td>   
                    <br />

                            <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="100%" id="Table1" runat="server">
                            <tr>
		                            <td  bgcolor="#dee7f6" class="buttonlabel">&nbsp;&nbsp;Ordered Quantity
		                            </td>
                                </tr>
                            <tr>
                                <td  align="left">
                                    
                                <asp:Repeater ID="rptLineItems" runat="server" Visible="true" >
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
                                    <td class="buttonlabel" width="30%">
                                        SKU
                                    </td>
                                    <td class="buttonlabel" width="10%">
                                       Quantity
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
                                            <%# Eval("Quantity")%>    
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
                            </td>
                          </tr>   
                            <tr>
                                <td>
                                    <br />
                                    <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="100%" id="Table3" runat="server">
                           <%-- <tr>
		                            <td  bgcolor="#dee7f6" class="buttonlabel">&nbsp;&nbsp;ESN
		                            </td>
                                </tr>--%>
                            <tr>
                                <td  align="center">
                                
                                    <table bordercolor="#839abf" border="0" width="100%" cellpadding="1" cellspacing="1">
                                         <%--<tr>
		                            <td colspan="4" bgcolor="#dee7f6" class="buttonlabel">&nbsp;&nbsp;ESN
		                            </td>
                                </tr>--%>
                           
                                    <tr>
                                        <td class="copy10grey"  with="15%"  align="right">
                                            <b>Assigned ESN/ICCID:</b>
                                        </td>
                                        <td class="copy10grey"  with="35%">
                                            &nbsp;&nbsp;<asp:Label ID="lblESN" CssClass="copy10grey" runat="server"></asp:Label>
                                        </td>
                                        <td class="copy10grey"  with="15%"  align="right">
                                            <b>Replaced ESN/ICCID:</b>
                                        </td>
                                        <td class="copy10grey"  with="35%">
                                            &nbsp;&nbsp;<asp:TextBox ID="txtReplacedESN"  onkeypress="return isNumberKey(event);" MaxLength="20" Width="80%" CssClass="copy10grey" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    </table>
                                </td>
                                </tr>
                                        </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <br />
                                    <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="100%" id="Table5" visible="false" runat="server">
                                    <tr>
		                                    <td  bgcolor="#dee7f6" class="buttonlabel">&nbsp;&nbsp;Replaced ESN/ICCID Info
		                                    </td>
                                        </tr>
                                    <tr>
                                        <td  align="left">
                                    
                                            <asp:Repeater ID="rptESN" runat="server" Visible="true" >
                                            <HeaderTemplate>
                                            <table bordercolor="#839abf" border="1" width="100%" cellpadding="1" cellspacing="1">
                                            <tr>
                                                <td class="buttonlabel" width="10%">
                                                    Category Name
                                                </td>                                    
                                                <td class="buttonlabel"  width="20%">
                                                    Product Name
                                                </td>
                                                <td class="buttonlabel" width="15%">
                                                    SKU
                                                </td>
                                                <td class="buttonlabel"  width="10%">
                                                    HEX
                                                </td>
                                                <td class="buttonlabel" width="10%">
                                                   DEC
                                                </td>
                                                <td class="buttonlabel" width="10%">
                                                   Location
                                                </td>
                                                <td class="buttonlabel" width="10%">
                                                   SerialNumber
                                                </td>                                    
                                            </tr>
                                            </HeaderTemplate>
                                            <ItemTemplate>    
                                                <tr valign="bottom"  class="<%# Container.ItemIndex % 2 == 0 ? "alternaterow" : "" %>">                                        
                                                    <td valign="bottom" class="copy10grey"  >
                                                    <span width="100%">
                                                        <%# Eval("CategoryName")%>    
                                                        </span>
                                                    </td>
                                                    <td valign="bottom" class="copy10grey"  >
                                                    <span width="100%">
                                                        <%# Eval("ItemName")%>    
                                                        </span>
                                                    </td>
                                                    <td valign="bottom" class="copy10grey"  >
                                                    <span width="100%">
                                                        <%# Eval("SKU")%>    
                                                        </span>
                                                    </td>
                                                     <td valign="bottom" class="copy10grey"  >
                                                    <span width="100%">
                                                        <%# Eval("MeidHex")%>    
                                                        </span>
                                                    </td>
                                                     <td valign="bottom" class="copy10grey"  >
                                                    <span width="100%">
                                                        <%# Eval("MeidDec")%>    
                                                        </span>
                                                    </td>
                                                     <td valign="bottom" class="copy10grey"  >
                                                    <span width="100%">
                                                        <%# Eval("Location")%>    
                                                        </span>
                                                    </td>
                                                     <td valign="bottom" class="copy10grey"  >
                                                    <span width="100%">
                                                        <%# Eval("SerialNumber")%>    
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
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <br />
                                    <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="100%" id="Table4" runat="server">
                           <%-- <tr>
		                            <td  bgcolor="#dee7f6" class="buttonlabel">&nbsp;&nbsp;ESN
		                            </td>
                                </tr>--%>
                            <tr>
                                <td  align="center">
                                
                                    <table bordercolor="#839abf" border="0" width="100%" cellpadding="1" cellspacing="1">
                                
                                    <tr valign="top">
                                        <td class="copy10grey"  width="10%"  align="right">
                                            <b>Approved By:</b>
                                        </td>
                                        <td class="copy10grey"  width="25%">
                                            &nbsp;&nbsp;
                                         <asp:DropDownList ID="ddlUser" CssClass="copy10grey" runat="server" Width="60%">
								         </asp:DropDownList>
                                        </td>
                                        <td class="copy10grey"  width="10%"  align="right" valign="top">
                                            Comment: 
                                        </td>
                                        <td class="copy10grey"  width="55%">
                                             <asp:TextBox ID="txtComment" CssClass="copy10grey" runat="server" Width="99%" Height="80" TextMode="MultiLine">
								            </asp:TextBox>
                                        
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="copy10grey"  with="15%"  align="right">
                                            
                                        </td>
                                        <td colspan="3">
                                           
                                        </td>
                                    </tr>
                                    </table>
                                </td>
                                </tr>
                                        </table>
                                </td>
                            </tr>

                            <tr>
                                <td colspan="5">
                                    <asp:Panel ID="pnlSubmit" runat="server" Visible="true">
                                    <table cellpadding="0" cellspacing="0" width="100%">
                                        <tr><td>
                                        <hr style="width:100%" />
                            
                                        </td></tr>   
                                         
                                         <tr>
                                            <td  align="center">
                                
                                            <asp:Button ID="btnValidate" Visible="false"  Width="190px" CssClass="button" runat="server" OnClick="btnValidate_Click" Text="Validate" />

                                            &nbsp;<asp:Button ID="btnSubmit" Visible="false" Width="190px"  CssClass="button" runat="server" OnClick="btnSubmit_Click" Text="Submit" />
                                
                                            &nbsp;<asp:Button ID="btnCancel" Visible="true" runat="server"  CssClass="button" Text="Cancel" OnClick="btnCancel_Click" />
                                 
                                            
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
    </ContentTemplate>
         </asp:UpdatePanel>
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
