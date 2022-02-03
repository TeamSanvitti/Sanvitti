<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KittingSummary.aspx.cs" Inherits="avii.Kitting.KittingSummary" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="~/Controls/Header.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Kitting Summary</title>
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
<script type="text/javascript">
    function OpenNewPage(url) {

        var newWin = window.open(url);

        if (!newWin || newWin.closed || typeof newWin.closed == 'undefined') {
            alert('your pop up blocker is enabled');

            //POPUP BLOCKED
        }
    }

    </script>
    <script type="text/javascript">
       function set_focus1() {
           var img = document.getElementById("imgDateFrom");
           var st = document.getElementById("ddlUser");
           st.focus();
           img.click();
       }
       function set_focus2() {
           var img = document.getElementById("imgDateTo");
           var st = document.getElementById("ddlUser");
           st.focus();
           img.click();
       }
</script>
    
    <link rel="stylesheet"  type="text/css" href="../fullfillment/calendar/dhtmlgoodies_calendar.css" media="screen" />
	<script type="text/javascript" src="../fullfillment/calendar/dhtmlgoodies_calendar.js"></script> 

    
    
      <style type="text/css">
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
    
    <table cellSpacing="0" cellPadding="0" align="center" width="100%" border="0">
        <tr>
			<td><head:MenuHeader id="HeadAdmin" runat="server"></head:MenuHeader></td>
		</tr>
     </table>
    <table cellspacing="0" cellpadding="0" border="0" align="center" width="95%">
		<tr>
			<td  bgcolor="#dee7f6" class="buttonlabel">
            &nbsp;&nbsp;Kitting Summary
			</td>
		</tr>
    
    </table>
    
    <table cellSpacing="0" cellPadding="0" align="center" width="95%" border="0"  id="maintbl">
        <tr>
	        <td>
             <asp:UpdatePanel ID="upnlSumary" UpdateMode="Conditional" runat="server">
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
         <table width="100%" border="0" class="" align="center" cellpadding="5" cellspacing="5">  
             <tr>
                <td class="copy10grey"  align="right" width="20%" >
                  Fulfillment#:
                </td>
                <td width="30%" >
                <asp:TextBox ID="txtPONum"  CssClass="copy10grey" runat="server" Width="60%" MaxLength="30" ></asp:TextBox>
                
                </td>
                <%--<td  width="1%">
                    &nbsp;
                </td>--%>
                <td class="copy10grey"  align="right" width="20%" >
                   Employee Name:
                </td>
                <td width="30%" >
                  <asp:DropDownList ID="ddlUser" CssClass="copy10grey" runat="server" Width="60%">
									</asp:DropDownList>
              
                </td>   
                </tr>

            <tr>
                <td class="copy10grey"  align="right" width="20%" >
                   Date From:
                </td>
                <td width="30%" >
                <asp:TextBox ID="txtDateFrom" runat="server" onfocus="set_focus1();"  CssClass="copy10grey" MaxLength="12"  Width="60%"></asp:TextBox>
                    <img id="imgDateFrom" alt="" onclick="document.getElementById('<%=txtDateFrom.ClientID%>').value = ''; displayCalendar(document.getElementById('<%=txtDateFrom.ClientID%>'),'mm/dd/yyyy',this,true);" src="../fullfillment/calendar/sscalendar.jpg" />
                    
                </td>
                <%--<td  width="1%">
                    &nbsp;
                </td>--%>
                <td class="copy10grey"  align="right" width="20%" >
                   To From:
                </td>
                <td width="30%" >
                
                    <asp:TextBox ID="txtDateTo" runat="server" onfocus="set_focus2();"  CssClass="copy10grey" MaxLength="12"  Width="60%"></asp:TextBox>
                    <img id="imgDateTo" alt="" onclick="document.getElementById('<%=txtDateTo.ClientID%>').value = ''; displayCalendar(document.getElementById('<%=txtDateTo.ClientID%>'),'mm/dd/yyyy',this,true);" src="../fullfillment/calendar/sscalendar.jpg" />
                  
                </td>   
                </tr>
            
            <tr>
                <td class="copy10grey"  align="right" width="20%" >
                   IMEI#:
                </td>
                <td width="30%" >
                <asp:TextBox ID="txtESN"  CssClass="copy10grey" runat="server" Width="60%" MaxLength="30" ></asp:TextBox>
                
                </td>
                <%--<td  width="1%">
                    &nbsp;
                </td>--%>
                <td class="copy10grey"  align="right" width="20%" >
                   BOX#:
                </td>
                <td width="30%" >
                <asp:TextBox ID="txtBoxNo"   CssClass="copy10grey" runat="server" Width="60%" MaxLength="30" ></asp:TextBox>
                
                </td>   
                </tr>
                <tr style="height:12px">
                <td colspan="4">
                <hr />
                </td>
                </tr>
                <tr>
                <td  align="center"  colspan="4">
            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="button" OnClick="btnSearch_Click" CausesValidation="false" OnClientClick="return ShowSendingProgress();"/>
         &nbsp;
           <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="button" OnClick="btnCancel_Click"  CausesValidation="false"/>
                   
        </td>
        </tr>
            </table>
            </asp:Panel>
        
                </td>
            </tr>
            </table>

        <asp:Panel ID="pnlPO" runat="server" Visible="true">
              <br />
                
          <table align="center" style="text-align:left" width="100%">
            <tr>
                <td align="right">
                 <%--   <asp:Button ID="btnBox" runat="server" Text="Box Label" CssClass="button"  CausesValidation="false" OnClick="btnBox_Click"/>
                    &nbsp; <asp:Button ID="btnPOSLabel" runat="server" Text="POS Label" CssClass="button"  OnClick="btnPOSLabel_Click" CausesValidation="false"/>
                    &nbsp; <asp:Button ID="btnCarton" runat="server" Text="Master Carton Label" CssClass="button"  OnClick="btnCarton_Click" CausesValidation="false"/>
           --%>
                    <asp:Label ID="lblCount" CssClass="copy10grey" runat="server" ></asp:Label>    
                </td>
            </tr>
          <tr>
              <td align="center">
                  
                      <asp:GridView ID="gvKitting"   AutoGenerateColumns="false"  
                        Width="100%" ShowHeader="true"  ShowFooter="false" runat="server" GridLines="Both" 
                        OnPageIndexChanging="gvKitting_PageIndexChanging" PageSize="50" AllowPaging="true" 
                        AllowSorting="true" OnSorting="gvKitting_Sorting" >
                        <RowStyle BackColor="Gainsboro" />
                        <AlternatingRowStyle BackColor="white" />
                        <HeaderStyle  CssClass="buttongrid" ForeColor="white"/>
                          <PagerStyle ForeColor="#636363" CssClass="copy10grey" HorizontalAlign="Left" />
                          <Columns>
                              <asp:TemplateField HeaderText="S.No." ItemStyle-CssClass="copy10grey"  ItemStyle-Width="2%">
                                <ItemTemplate>
                                      <%# Container.DataItemIndex + 1%>                  
                                </ItemTemplate>
                                </asp:TemplateField> 

                                <asp:TemplateField HeaderText="Employee Name" SortExpression="UserName" HeaderStyle-CssClass="buttonundlinelabel"  
                                    ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                            <%# Eval("UserName") %>                                     
                                        
                                    </ItemTemplate>
                                </asp:TemplateField> 
                                <asp:TemplateField HeaderText="Kitting Date" SortExpression="KittedCSTDate" HeaderStyle-CssClass="buttonundlinelabel"  
                                    ItemStyle-HorizontalAlign="Left" 
                                    ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <%# Convert.ToDateTime(Eval("KittedCSTDate")).ToString("MM/dd/yyyy")%>
                                    </ItemTemplate>
                                </asp:TemplateField> 
                              <asp:TemplateField HeaderText="BOXID" SortExpression="BOXID" HeaderStyle-CssClass="buttonundlinelabel"   ItemStyle-HorizontalAlign="Left" 
                                  ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <%--<%# Eval("BOXID")%>--%>
                                          <asp:LinkButton ID="lnkBOXID" runat="server" ToolTip="View Box detail" OnCommand="lnkBOXID_Command" 
                                              CommandArgument='<%# Eval("FulfillmentNumber")+ ","+ Eval("BOXID") +","+ Eval("PalletID") %>'><b><%# Eval("BOXID")%></b></asp:LinkButton>
                                        
                                        </ItemTemplate>
                                </asp:TemplateField>  
                              
                              <asp:TemplateField HeaderText="SKU#" SortExpression="SKU" HeaderStyle-CssClass="buttonundlinelabel"  ItemStyle-HorizontalAlign="left" 
                                  ItemStyle-CssClass="copy10grey" ItemStyle-Width="20%">                                    
                                    <ItemTemplate>
                                                
                                         <%# Eval("SKU")%>
                                    </ItemTemplate>
                                </asp:TemplateField> 
                              <asp:TemplateField HeaderText="IMEI Count" SortExpression="EsnCount" HeaderStyle-CssClass="buttonundlinelabel"  
                                  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <%# Eval("EsnCount")%>
                                        </ItemTemplate>
                                </asp:TemplateField>  
                                
                            </Columns>
                        </asp:GridView>
                                 
              </td>
          </tr>
                </table>
              
          </asp:Panel>
          
            
       
    
          </ContentTemplate>
        <%--<Triggers>
            <asp:PostBackTrigger ControlID="btnBox" />
            <asp:PostBackTrigger ControlID="btnCarton" />
            <asp:PostBackTrigger ControlID="btnPOSLabel" />
        </Triggers>--%>
        </asp:UpdatePanel>
		
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
        <script type="text/javascript">       
            function ShowSendingProgress() {

                var modal = $('<div  />');
                modal.addClass("modal");
                modal.attr("id", "modalSending");
                $('body').append(modal);
                var loading = $("#modalSending.loadingcss");
                loading.show();
                var top = '300px';
                var left = '820px';
                loading.css({ top: top, left: left, color: '#ffffff' });

                var tb = $("maintbl");
                tb.addClass("progresss");


                return true;
            }
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
