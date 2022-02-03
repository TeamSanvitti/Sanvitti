<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UnusedShipmentLabel.aspx.cs" Inherits="avii.Reports.UnusedShipmentLabel" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>
<%@ Register TagPrefix="menu" TagName="Menu" Src="~/Controls/Header.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title>Unused Label Search</title>
     <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>

    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.4.0/jquery.min.js"></script>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/jquery-ui.min.js"></script>
    <link href="https://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/themes/start/jquery-ui.css"
    rel="stylesheet" type="text/css" />

     <link rel="stylesheet"  type="text/css" href="../fullfillment/calendar/dhtmlgoodies_calendar.css" media="screen" />
	<script type="text/javascript" src="../fullfillment/calendar/dhtmlgoodies_calendar.js"></script>  
    <script>
        function SelectAll(id) {
            // alert(document.getElementById(id).checked);
            var check = document.getElementById(id).checked;
            // alert(check);

            var elements = document.getElementsByTagName('input');
            // iterate and change status
            for (var i = elements.length; i--;) {
                if (elements[i].type == 'checkbox') {
                    elements[i].checked = check;
                }
            }
            // $(':checkbox').prop('checked', check);



        }
    </script>
    <script>       
            function OpenNewPage(url) {

            var newWin = window.open(url);

            if (!newWin || newWin.closed || typeof newWin.closed == 'undefined') {
                alert('your pop up blocker is enabled');

                //POPUP BLOCKED
            }
        }
        function alphanumeric(e) {

            var regex = new RegExp("^[a-zA-Z0-9]+$");
            var str = String.fromCharCode(!e.charCode ? e.which : e.charCode);


            //alert(regex.test(str));
            if (regex.test(str)) {
                return true;
            }
            else {
                e.preventDefault();
                return false;
            }
        }

        $(document).ready(function () {

            $(".alphanumericonly").keypress(function (e) {
                var regex = new RegExp("^[a-zA-Z0-9]+$");
                var str = String.fromCharCode(!e.charCode ? e.which : e.charCode);


                //alert(regex.test(str));
                if (regex.test(str)) {
                    return true;
                }
                else {
                    e.preventDefault();
                    return false;
                }
            });
        });

    </script>
    <script type="text/javascript">
       function set_focus1() {
           var img = document.getElementById("imgFromtDate");
           //var st = document.getElementById("txtTrackingNo");
           //st.focus();
           img.click();
           return false;
       }
       function set_focus2() {
           var img = document.getElementById("imgToDate");
           //var st = document.getElementById("txtTrackingNo");
           //st.focus();
           img.click();
           return false;
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
<body bgcolor="#ffffff" leftmargin="0" rightmargin="0" topmargin="0">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table cellspacing="0" cellpadding="0" border="0" align="center" width="100%">
    <tr>
        <td>
            <menu:Menu ID="menu1" runat="server" ></menu:Menu>
            
        </td>
     </tr>
     </table>
         <table cellspacing="0" cellpadding="0" border="0" align="center" style="text-align:left" width="100%">
            <tr class="buttonlabel" align="left">
                <td>&nbsp;Unused Shipment Label Search</td>
            </tr>
        </table>
    
<table cellspacing="0" cellpadding="0" border="0" align="center" width="95%">
        
        <tr valign="top">           
            <td>
          
    <asp:UpdatePanel ID="UpdatePanel1"  runat="server"   >
    <ContentTemplate>
    <table  align="center" style="text-align:left" width="100%" cellSpacing="0" cellPadding="0">
     <tr>
        <td align="left">
           <asp:Label ID="lblMsg" runat="server" CssClass="errormessage"></asp:Label>&nbsp;
            
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
     
           
            <tr  >
                <td class="copy10grey" align="right" width="15%">
                    Customer:
                </td>
                <td width="35%">
                
                            <asp:DropDownList ID="ddlCustomer" CssClass="copy10grey" runat="server" Width="70%">
	                        </asp:DropDownList>      
                
                </td>                
                <td  width="1%">
                    &nbsp;
                </td>
                <td class="copy10grey" align="right" width="15%">
                    Tracking#:
                </td>
                <td width="35%">
                   
                  <asp:TextBox ID="txtTrackingNo" runat="server" onkeypress="return alphanumeric(event);"  CssClass="copy10grey" MaxLength="25"  Width="70%"></asp:TextBox>
                   
                </td>   
                
                    
                </tr>
       
                <tr >
                
                <td class="copy10grey" align="right" width="15%">
                    From Date:
                </td>
                <td width="35%">
                 <asp:TextBox ID="txtFromDate" runat="server" onfocus="set_focus1();" onkeypress="return false;"   CssClass="copy10grey" MaxLength="15"  Width="70%"></asp:TextBox>
                    <img id="imgFromtDate" alt="" onclick="document.getElementById('<%=txtFromDate.ClientID%>').value = ''; displayCalendar(document.getElementById('<%=txtFromDate.ClientID%>'),'mm/dd/yyyy',this,true);" src="../fullfillment/calendar/sscalendar.jpg" />
                  
                </td>

                <td  width="1%">
                    &nbsp;
                </td>
                <td class="copy10grey" align="right" width="15%">
                    To Date:
                </td>
                <td width="35%">
                   
                     <asp:TextBox ID="txtToDate" runat="server" onfocus="set_focus2();" onkeypress="return false;"  CssClass="copy10grey" MaxLength="15"  Width="70%"></asp:TextBox>
                    <img id="imgToDate"  alt="" onclick="document.getElementById('<%=txtToDate.ClientID%>').value = ''; displayCalendar(document.getElementById('<%=txtToDate.ClientID%>'),'mm/dd/yyyy',this,true);" src="../fullfillment/calendar/sscalendar.jpg" />
                
                </td>   
                
                    
                </tr>
                 <td class="copy10grey" align="right" width="15%">
                    Status:
                </td>
                <td width="35%">
                 <asp:DropDownList ID="ddlStatus" runat="server" CssClass="copy10grey" Width="70%">
                     <asp:ListItem Text="" Value=""></asp:ListItem>
                     <asp:ListItem Text="Pending" Value="false"></asp:ListItem>
                     <asp:ListItem Text="Cancelled" Value="true"></asp:ListItem>
                 </asp:DropDownList>
                    
                </td>

                <td  width="1%">
                    &nbsp;
                </td>
                <td class="copy10grey" align="right" width="15%">
                    
                </td>
                <td width="35%">
                   
                     
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
                    <asp:Button ID="btnSearch" runat="server"  CssClass="button" Text="Search" OnClick="btnSearch_Click" CausesValidation="false" OnClientClick="return ShowSendingProgress();"></asp:Button>
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="button"  OnClick="btnCancel_Click" CausesValidation="false"/>
                  
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
          <td align="center" colspan="5">
              <table cellpadding="0" cellspacing="0" width="100%">
                  <tr>
                      <td align="left">
                          <asp:Label ID="lblCount" runat="server" CssClass="copy10grey"></asp:Label>
                      </td>
                      <td align="right">
                          
                          <asp:Button ID="btnCancelled" runat="server" Text="Cancel Label"  Visible="false"  CssClass="button"  OnClick="btnCancelled_Click" CausesValidation="false"/>
                  
                          &nbsp;<asp:Button ID="btnDownload" runat="server" Text="Download"  Visible="false"  CssClass="button"  OnClick="btnDownload_Click" CausesValidation="false"/>
                  
                          
                      </td>
                  </tr>
                  <tr>
                      <td colspan="2">
                          <asp:GridView ID="gvLabel" runat="server" AllowPaging="true" AllowSorting="true" AutoGenerateColumns="false"
                              GridLines="Both" OnPageIndexChanging="gvLabel_PageIndexChanging" OnSorting="gvLabel_Sorting" PageSize="100" 
                              ShowFooter="false" ShowHeader="true" Width="100%" OnRowDataBound="gvLabel_RowDataBound">
                              <RowStyle BackColor="Gainsboro" />
                              <AlternatingRowStyle BackColor="white" />
                              <HeaderStyle CssClass="buttonlabel" ForeColor="white" />
                              <PagerStyle CssClass="copy10grey" ForeColor="#636363" HorizontalAlign="Left" />
                              <Columns>
                                  <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="1%">
                                   <HeaderTemplate>
                                        <asp:CheckBox ID="allchk"  runat="server"  />
                                    </HeaderTemplate>
                                    <ItemTemplate>                        
                                        <asp:CheckBox ID="chkItem"  runat="server" CssClass="copy10grey" />
                                        </ItemTemplate>
                                  </asp:TemplateField>   
                                  <asp:TemplateField HeaderText="S.No." ItemStyle-CssClass="copy10grey" ItemStyle-Width="2%">
                                      <ItemTemplate>
                                          <%# Container.DataItemIndex + 1%>
                                          <asp:HiddenField ID="hnID" Value='<%# Eval("ID")%>' runat="server" />
                                          <asp:HiddenField ID="hnSource" Value='<%# Eval("LabelSource")%>' runat="server" />
                                      </ItemTemplate>
                                  </asp:TemplateField>
                                  <asp:TemplateField HeaderStyle-CssClass="buttonundlinelabel" HeaderText="Label Generation Date" ItemStyle-CssClass="copy10grey" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%" SortExpression="CategoryName">
                                      <ItemTemplate>
                                          <%# Eval("LabelGenerationDate")%>
                                      </ItemTemplate>
                                  </asp:TemplateField>
                                  <asp:TemplateField HeaderStyle-CssClass="buttonlabel" HeaderText="Shipping Method" ItemStyle-CssClass="copy10grey" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%" SortExpression="ShippingMethod">
                                      <ItemTemplate>
                                          <%# Eval("ShipmentMethod")%>
                                      </ItemTemplate>
                                  </asp:TemplateField>
                                  <asp:TemplateField HeaderStyle-CssClass="buttonlabel" HeaderStyle-HorizontalAlign="Center" HeaderText="Package" ItemStyle-CssClass="copy10grey" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%" SortExpression="Package">
                                      <ItemTemplate>
                                          <%# Eval("ShipPackage")%>
                                      </ItemTemplate>
                                  </asp:TemplateField>
                                  <asp:TemplateField HeaderStyle-CssClass="buttonlabel" HeaderStyle-HorizontalAlign="Center" HeaderText="Tracking Number" ItemStyle-CssClass="copy10grey" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="15%" SortExpression="TrackingNumber">
                                      <ItemTemplate>
                                          <%# Eval("TrackingNumber")%>
                                      </ItemTemplate>
                                  </asp:TemplateField>
                                  <asp:TemplateField HeaderStyle-CssClass="buttonlabel" HeaderStyle-HorizontalAlign="Center" HeaderText="Weight" ItemStyle-CssClass="copy10grey" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="7%" SortExpression="Weight">
                                      <ItemTemplate>
                                          <%# Eval("ShippingWeight")%>
                                      </ItemTemplate>
                                  </asp:TemplateField>
                                  <asp:TemplateField HeaderStyle-CssClass="buttonlabel" HeaderStyle-HorizontalAlign="Center" HeaderText="Cost($)" ItemStyle-CssClass="copy10grey" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="7%" SortExpression="Cost">
                                      <ItemTemplate>
                                          $<%# Eval("FinalPostage", "{0:n}")%>
                                      </ItemTemplate>
                                  </asp:TemplateField>
                                  <asp:TemplateField HeaderStyle-CssClass="buttonlabel" HeaderStyle-HorizontalAlign="Left" HeaderText="Label Type" ItemStyle-CssClass="copy10grey" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%" SortExpression="LabelType">
                                      <ItemTemplate>
                                          <%#  Eval("LabelType")%>
                                      </ItemTemplate>
                                  </asp:TemplateField>
                                  <asp:TemplateField HeaderStyle-CssClass="buttonlabel" HeaderStyle-HorizontalAlign="Left" HeaderText="Assigned To" ItemStyle-CssClass="copy10grey" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%" SortExpression="AssignedTo">
                                      <ItemTemplate>
                                          <%# Eval("AssignedTo")%>
                                      </ItemTemplate>
                                  </asp:TemplateField>
                                  <asp:TemplateField HeaderStyle-CssClass="buttonlabel" HeaderStyle-HorizontalAlign="Left" HeaderText="Assign To Number" ItemStyle-CssClass="copy10grey" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%" SortExpression="AssignToNumber">
                                      <ItemTemplate>
                                          <asp:LinkButton ID="lnkPO" CommandArgument='<%# Eval("POID")%>' runat="server" OnCommand="lnkPO_Command"
                                              CausesValidation="false" ToolTip="View PO">
                                              <%# Eval("AssignToNumber")%>
                                          </asp:LinkButton>
                                          
                                      </ItemTemplate>
                                  </asp:TemplateField>
                              </Columns>
                          </asp:GridView>
                      </td>
                  </tr>
                  <tr>
                      <td colspan="2">
                          
                          
                      </td>
                  </tr>
              </table>
          </td>
            </table>
            
    </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnDownload" />
        </Triggers>
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
