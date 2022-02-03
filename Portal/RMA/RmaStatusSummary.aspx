<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RmaStatusSummary.aspx.cs" Inherits="avii.RMA.RmaStatusSummary" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>
<%@ Register TagPrefix="menu" TagName="Menu" Src="~/Controls/Header.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <link rel="stylesheet"  type="text/css" href="../fullfillment/calendar/dhtmlgoodies_calendar.css" media="screen" />
	<script type="text/javascript" src="../fullfillment/calendar/dhtmlgoodies_calendar.js"></script>  
    
    <script type="text/javascript">
        function set_focus1() {
            var img = document.getElementById("imgDateFrom");
            var st = document.getElementById("ddlShipVia");
            st.focus();
            img.click();
        }
        function set_focus2() {
            var img = document.getElementById("imgDateTo");
            var st = document.getElementById("ddlShipVia");
            st.focus();
            img.click();
        }
</script>
</head>
<body bgcolor="#ffffff" leftmargin="0" rightmargin="0" topmargin="0">
    <form id="form1" runat="server">
    <table cellspacing="0" cellpadding="0" border="0" align="center" width="100%">
    <tr>
        <td>
            <menu:Menu ID="menu1" runat="server" ></menu:Menu>
        </td>
    </tr>
    </table>    
    
        <table cellspacing="0" cellpadding="0" border="0" align="center" width="95%">      
        <tr valign="top">           
        <td>
            <table align="center" style="text-align:left" width="100%">
                <tr class="buttonlabel" align="left">
                    <td>&nbsp;Customerwise RMA Status Report</td>
                </tr>
            </table>
     
     <asp:ScriptManager ID="ScriptManager1" runat="server">
     </asp:ScriptManager>

     <asp:UpdatePanel ID="UpdatePanel1"  runat="server"  UpdateMode="Conditional" >
     <ContentTemplate>
     <table  align="center" style="text-align:left" width="99%">
     <tr>
        <td>
           <asp:Label ID="lblMsg" runat="server" CssClass="errormessage"></asp:Label>            
        </td>
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
     
           
            <tr valign="top">
                <td class="copy10grey" align="right" width="15%">
                    Customer Name:
                </td>
                <td width="35%">
                <asp:DropDownList ID="dpCompany" runat="server" AutoPostBack="false" Width="65%"
                 class="copy10grey">
                </asp:DropDownList> 
                <asp:Label ID="lblCompany" runat="server" CssClass="copy10grey" ></asp:Label>
           
                </td>
                <td  width="1%">
                    &nbsp;
                </td>
                <td class="copy10grey" align="right" width="10%">
                    Summary By:
                </td>
                <td width="40%">
                   <asp:DropDownList ID="ddlSummaryBy" AutoPostBack="false"  runat="server" Class="copy10grey" Width="60%" >
                        <asp:ListItem  Value="RMAStatus"  >RMA Status</asp:ListItem>
                        <asp:ListItem  Value="LineItemStatus"  >Line Item Status</asp:ListItem>
                        <asp:ListItem  Value="Disposition"  >Disposition</asp:ListItem>
                        <asp:ListItem  Value="Reason"  >Reason</asp:ListItem>
                        <asp:ListItem Value="Triage" >Triage Status</asp:ListItem>
                        <asp:ListItem  Value="ShipmentPaidBy">Shipment Paid By</asp:ListItem>                                
                    </asp:DropDownList>
                    
        
        
                </td>   
                
                    
                </tr>
                
            <tr valign="top">
                <td class="copy10grey" align="right" width="15%">
                    From Date:
                </td>
                <td width="35%">
                    <asp:TextBox ID="txtDateFrom" runat="server" onfocus="set_focus1();"  CssClass="copy10grey" MaxLength="12"  Width="65%"></asp:TextBox>
                    <img id="imgDateFrom" alt="" onclick="document.getElementById('<%=txtDateFrom.ClientID%>').value = ''; displayCalendar(document.getElementById('<%=txtDateFrom.ClientID%>'),'mm/dd/yyyy',this,true);" src="../fullfillment/calendar/sscalendar.jpg" />
                            
                </td>
                <td  width="1%">
                    &nbsp;
                </td>
                <td class="copy10grey" align="right" width="10%">
                    To Date:
                </td>
                <td width="40%">
                   
                <asp:TextBox ID="txtDateTo" runat="server" onfocus="set_focus2();"  CssClass="copy10grey" MaxLength="12"  Width="60%"></asp:TextBox>
                            <img id="imgDateTo" alt="" onclick="document.getElementById('<%=txtDateTo.ClientID%>').value = ''; displayCalendar(document.getElementById('<%=txtDateTo.ClientID%>'),'mm/dd/yyyy',this,true);" src="../fullfillment/calendar/sscalendar.jpg" />
                          
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
        <td  align="center"  colspan="5">
            <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td align="right">
                    <asp:Label ID="lblCount" CssClass="copy10grey" runat="server" ></asp:Label>
                </td>
            </tr>
            <tr>
                <td >
    
                <div id="divRMA" runat="server"  style="width:100%;border:none">
            
                </div>
                </td>
            </tr>
            </table>
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
<br /><br /> <br />
            <br /> <br />
        <table cellspacing="0" cellpadding="0" border="0" align="center" width="100%">
        <tr>
            <td >
                <foot:MenuFooter ID="footer2" runat="server"></foot:MenuFooter>
            </td>
         </tr>
         </table>
        
    </form>
</body>
</html>
