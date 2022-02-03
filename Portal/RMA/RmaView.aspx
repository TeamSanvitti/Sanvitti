<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RmaView.aspx.cs" Inherits="avii.RMA.RmaView" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>
<%@ Register TagPrefix="menu" TagName="Menu" Src="~/Controls/Header.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Return Merchandise Authorization</title>
     <!-- from http://encosia.com/2009/10/11/do-you-know-about-this-undocumented-google-cdn-feature/ -->
    <link href="https://ajax.googleapis.com/ajax/libs/jqueryui/1.8.11/themes/start/jquery-ui.css" type="text/css" rel="Stylesheet" />
	<%--<link href="../CSS/jquery-ui.css" rel="stylesheet" type="text/css"/>--%>
	<%--<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.4.0/jquery.min.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/jquery-ui.min.js"></script>
--%>

    <script type="text/javascript" src="../JQuery/jquery.min.js"></script>
    <script type="text/javascript" src="../JQuery/jquery-ui.min.js"></script>
	
	<script type="text/javascript" src="../JQuery/jquery.blockUI.js"></script>

    <script>
        function Refresh() {
            //alert('refreshing..');
            var btnhdPrintlabel = document.getElementById("<%= btnhdPrintlabel.ClientID %>");
            btnhdPrintlabel.click();
        }

        function closeWindow() {

            alert('No valid data!')
            window.close();

        }
        function close_window() {
            if (confirm("Close Window?")) {
                window.close();
                return true;
            }
            else
                return false
        }

        function divexpandcollapse(divname) {
            // alert(divname);

            var div = document.getElementById(divname);
            var img = document.getElementById('img' + divname);

            if (div.style.display == "none") {

                if ($('#hdnchild').val() == "")
                    $('#hdnchild').val(divname);
                else
                    $('#hdnchild').val($('#hdnchild').val() + ',' + divname);

                div.style.display = "inline";
                img.src = "../Images/minus.gif";

            } else {

                var id = $('#hdnchild').val().replace(divname + ',', '').replace(divname + ',', '').replace(divname + ',', '').replace(divname, '').replace(divname, '').replace(divname, '');
                //   //   alert(id)
                $('#hdnchild').val(id);

                div.style.display = "none";
                img.src = "../Images/plus.gif";

            }

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
    <table cellspacing="0" cellpadding="0" border="0" align="center" style="text-align:left" width="95%">
        <tr class="buttonlabel" align="left">
        <td>&nbsp;Return Merchandise Authorization (RMA) View</td></tr>
    </table>
    <table cellSpacing="0" cellPadding="0" align="center" width="100%" border="0">
    <tr>                    
        <td colspan="2">
            <asp:HiddenField ID="hdnchild" runat="server" />
            <asp:Label ID="lblMsg" runat="server" Width="100%" CssClass="errormessage"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
                <%--<table cellspacing="0" cellpadding="0" border="0" align="center" style="text-align:left" width="95%">
                    <tr class="buttonlabel" align="left">
                    <td>&nbsp;RMA Header</td></tr>
                </table>
                --%>
                <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="95%" align="center">
                    <tr>
                    <td>
			            <table  cellSpacing="5" cellPadding="5" width="100%" border="0">
                        <tr>
                            <td colspan="5"> 
                                <table cellSpacing="5" cellPadding="5" width="100%" border="0">
                                <tr>
                                    <td class="copy10grey" width="15%"><strong>RMA#:</strong></td>
                                    <td class="copy10grey" width="35%">
                                        <asp:Label ID="lblRmaNumber" runat="server"  CssClass="copyblue11b"  />

                                    </td>
                                    <td class="copy10grey" width="20%">RMA Date:</td>
                                    <td class="copy10grey" width="30%">
                                         <asp:Label ID="lblRmaDate" runat="server"  CssClass="copy10grey"  />
 
                                    </td>
                                </tr>
                                <tr>
                                    <td class="copy10grey">Customer RMA#:</td>
                                    <td class="copy10grey">
                                         <asp:Label ID="lblCustomerRmaNo" runat="server"  CssClass="copy10grey"  />
 
                                    </td>
                                    <td class="copy10grey">Company Name:</td>
                                    <td class="copy10grey">
                                         <asp:Label ID="lblCompanyName" runat="server"  CssClass="copy10grey"  />
 
                                        
                                    </td>
                                </tr>
                                <tr>
                                    <td class="copy10grey">Triage Status:</td>
                                    <td class="copy10grey">
                                         <asp:Label ID="lblTriageStatus" runat="server"  CssClass="copy10grey"  />
 
                                    </td>
                                    <td class="copy10grey">Receive Status:</td>
                                    <td class="copy10grey">
                                         <asp:Label ID="lblReceiveStatus" runat="server"  CssClass="copy10grey"  />
                                        
                                    </td>
                                </tr>
                                <tr>
                                    <td class="copy10grey">RMA Status:</td>
                                    <td class="copy10grey">
                                        <asp:Label ID="lblRmaStatus" runat="server"  CssClass="copy10grey"  />
 
                                    </td>
                                    <td class="copy10grey">
                                       RMA Documents:
                                    </td>
                                    <td class="copy10grey">
                                         <asp:Label ID="lblDocs" runat="server"  CssClass="copy10grey"  />
                                    </td>
                                </tr>
                                    
                                </table>

                                
                            </td>
                        </tr>
                        </table>                            
                    </td>
                    </tr>                           
                 </table>
            <br />
            
             <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="95%" align="center">
                    <tr>
                    <td>
			            <table  cellSpacing="5" cellPadding="5" width="100%" border="0">
                        <tr>
                            <td class="copy10grey" width="15%">Customer Name:</td>
                            <td class="copy10grey" width="35%">
                                <asp:Label ID="lblContactName" runat="server"  CssClass="copy10grey"  />
 
                            </td>
                            <td class="copy10grey" width="20%">Store ID:</td>
                            <td class="copy10grey" width="30%">
                                <asp:Label ID="lblStoreID" runat="server"  CssClass="copy10grey"  />
 
                                        
                            </td>
                        </tr>
                        <tr>
                            <td class="copy10grey">Address:</td>
                            <td class="copy10grey">
                                <asp:Label ID="lblAddress" runat="server"  CssClass="copy10grey"  />
 
                            </td>
                            <td class="copy10grey">City:</td>
                            <td class="copy10grey">
                                <asp:Label ID="lblCity" runat="server"  CssClass="copy10grey"  />
                            </td>
                        </tr>
                        <tr>
                            <td class="copy10grey">State:</td>
                            <td class="copy10grey">
                                <asp:Label ID="lblState" runat="server"  CssClass="copy10grey"  />
                                        
                            </td>
                            <td class="copy10grey">Zip:</td>
                            <td class="copy10grey">
                                <asp:Label ID="lblZip" runat="server"  CssClass="copy10grey"  />
                                
                            </td>
                        </tr>
                        <tr>
                            <td class="copy10grey">Contact Number:</td>
                            <td class="copy10grey">
                                <asp:Label ID="lblContactNumber" runat="server"  CssClass="copy10grey"  />
                            </td>
                            <td class="copy10grey"></td>
                            <td class="copy10grey">
                                        
                            </td>
                        </tr>
                            
                                
                        </table>
                               
                </td>
                </tr>                           
            </table>

        </td>
    </tr>

    <tr>
        <td align="center" class="copy10grey" >
            <table cellspacing="0" cellpadding="0" border="0" align="center" style="text-align:right" width="95%">
        <tr>
            <td>

            <asp:Button ID="btnPackingSlip" runat="server"  Text="Packing Slip" 
            CssClass="button" Height="24px" 
            OnClick="btnPackingSlip_Click" />
            &nbsp;
    
            <asp:Button ID="btnPrintLabel" runat="server" Visible="false" Text="Print Label" 
            CssClass="button" Height="24px" Width="130px"
            OnClick="btnPrintLabel_Click" />
            </td>
        </tr>
    </table>
    </td>
    </tr>
    <tr>
    <td  align="center">
        <table cellspacing="0" cellpadding="0" border="0" align="center" style="text-align:left" width="95%">
        <tr class="buttonlabel" align="left">
        <td>Shipment</td></tr>
    </table>
    
        
    </td>
    </tr>
    <tr>
        <td>
            
            <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="95%" align="center">
            <tr>
            <td>
			    <table  cellSpacing="0" cellPadding="0" width="100%" border="0">
                <tr>
                    <td>
                         <div style="display:none">
                        <asp:Button ID="btnhdPrintlabel" runat="server"   Text="Printlabel" OnClick="btnhdnPrintlabel_Click" /> 
                        </div>
               
                        <asp:Repeater ID="rptTracking" runat="server" >
                        <HeaderTemplate>
                            <table border="0" width="100%" class="box" align="center" cellpadding="5" cellspacing="5">
                            <tr valign="top">
                                <td class="buttongrid" align="left" width="1%">
                                    Line#
                                </td>
                                <td class="buttongrid" align="left" width="15%">
                                    Ship Via
                                        </td>
                                <td class="buttongrid" align="left" width="20%">
                                    Ship Date
                                        </td>
                                <td class="buttongrid" align="left" width="14%">
                                    Ship Package
                                        </td>
                                <td class="buttongrid" align="left" width="10%">
                                    Weight(Oz)
                                        </td>
                                <td class="buttongrid" align="left" width="10%">
                                    Price
                                        </td>
                                <td class="buttongrid" align="left" width="22%">
                                    Tracking Number
                                        </td>
                                <td class="buttongrid" align="left" width="8%">
                                    Prepaid
                                        </td>
                                <td class="buttongrid" align="left" width="1%">
                                    
                                        </td>
                            </tr>        
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr class="<%# Container.ItemIndex % 2 == 0 ? "alternaterow" : "" %>">
                                    
                                    <td class="copy10grey" align="right" width="1%">
                                        <%# Container.ItemIndex + 1 %>
                                         
                                    </td>
                                    
                                    <td  class="copy10grey" >
                                        <%# Eval("ShipVia") %>
                                       
                                     </td>
                                    <td  class="copy10grey" >
                                        <%# Eval("ShipDate") %>
                                       
                                        </td>
                                    <td  class="copy10grey" >
                                        <%# Eval("Package") %>
                                        </td>
                                    <td class="copy10grey" >
                                            <%# Eval("Weight") %>
                                             
                                        </td>
                                    <td class="copy10grey" >
                                        $<%# Convert.ToDecimal(Eval("FinalPostage")).ToString("0.00") %>
                                        
                                                 
                                        </td>
                                        <td class="copy10grey" >
                                            <%# Eval("TrackingNumber") %>
                                                 
                                        </td>
                                <td class="copy10grey" >
                                            <%# Eval("Prepaid") %>
                                                 
                                        </td>
                                <td>
                                     <asp:ImageButton ID="imgPrint" runat="server" CausesValidation="false" 
                        CommandArgument='<%# Eval("TrackingId") %>'  OnCommand="imgPrint_Command" ToolTip="Print Label" AlternateText="View Label" 
                        ImageUrl="~/images/printer.png" />
                                </td>
                                
                                </tr>
                                  
                        
                            </ItemTemplate>
                            <FooterTemplate>
                                </table>    
                            </FooterTemplate>
                        </asp:Repeater>  
                        <asp:Label ID="lblLabel" runat="server"  CssClass="errormessage"></asp:Label>
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
            &nbsp;
        </td>
    </tr>
    <tr>
        <td align="center">
            <table cellspacing="0" cellpadding="0" border="0" align="center" style="text-align:left" width="95%">
        <tr class="buttonlabel" align="left">
        <td>Line Item(s)</td></tr>
    </table>
    
        
        </td>
    </tr>
    <tr>
        <td>
            <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="95%" align="center">
            <tr>
            <td>
			    <table  cellSpacing="0" cellPadding="0" width="100%" border="0">
                <tr>
                    <td>
                        <asp:Repeater ID="rptRmaDetail" runat="server" >
                        <HeaderTemplate>
                            <table border="0" width="100%" class="box" align="center" cellpadding="5" cellspacing="5">
                            <tr valign="top">
                                <td class="buttongrid" align="left" width="1%">
                                    Line#
                                </td>
                                <td class="buttongrid" align="left" width="12%">
                                    SKU
                                        </td>
                                <td class="buttongrid" align="left" width="15%">
                                    Product Name
                                        </td>
                                <td class="buttongrid" align="left" width="5%">
                                    Quantity
                                        </td>
                                <td class="buttongrid" align="left" width="10%">
                                    ESN/ICCID
                                        </td>
                                <td class="buttongrid" align="left" width="8%">
                                    Reason
                                        </td>
                                <td class="buttongrid" align="left" width="5%">
                                    Warranty
                                </td>
                                <td class="buttongrid" align="left" width="5%">
                                    Disposition
                                </td>
                                
                                <td class="buttongrid" align="left" width="12%">
                                    Notes
                                </td>
                                <td class="buttongrid" align="left" width="13%">
                                    Triage Notes
                                </td>
                                <td class="buttongrid" align="left" width="8%">
                                    Triage Status
                                </td>
                                <td class="buttongrid" align="left" width="5%">
                                    Status
                                </td>
                                <td class="buttongrid" align="left" width="1%">
                                    
                                </td>
                            </tr>        
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr class="<%# Container.ItemIndex % 2 == 0 ? "alternaterow" : "" %>">
                                    
                                    <td class="copy10grey" align="right" width="1%">
                                        <%# Container.ItemIndex + 1 %>                                         
                                    </td>                                    
                                    <td  class="copy10grey" >
                                        <%# Eval("SKU") %>                                       
                                     </td>
                                    <td  class="copy10grey" >
                                        <%# Eval("ProductName") %>                                       
                                        </td>
                                    <td  class="copy10grey" >
                                        <%# Eval("Quantity") %>
                                        </td>
                                    <td class="copy10grey" >
                                            <%# Eval("ESN") %>                                             
                                        </td>
                                    <td class="copy10grey" >
                                        <%# Eval("Reason") %>                                                 
                                        </td>
                                        
                                    <td class="copy10grey" >
                                            <%# Eval("Warranty") %>                                                 
                                        </td>
                                    <td class="copy10grey" >
                                            <%# Eval("Disposition") %>                                                 
                                        </td>
                                    <td class="copy10grey" >
                                            <%# Eval("Notes") %>                                                 
                                    </td>
                                    <td class="copy10grey" >
                                        <%# Eval("TriageNotes") %>   
                                    </td>
                                <td class="copy10grey" >
                                        <%# Eval("TriageStatus") %>   
                                    </td>
                                <td class="copy10grey" >
                                        <%# Eval("Status") %>   
                                    </td>
                                
                                <td class="copy10grey" >
                                          <asp:ImageButton ID="imgRmaDet" runat="server" Visible='<%# Convert.ToString(Eval("Status")).ToLower() == "pending" ? true : false %>'  CommandName="Delete" AlternateText="Delete RMA Item" ToolTip="Delete RMA Item" 
                                    ImageUrl="~/images/delete.png" CommandArgument='<%# Eval("RmaDetGUID") %>' OnCommand="imgRmaDet_Command" 
                                              OnClientClick="return confirm('Do you want to delete this RMA line item?')" />
                        
                                    </td>
                                </tr>
                                
                            </ItemTemplate>
                            <FooterTemplate>
                                </table>    
                            </FooterTemplate>
                        </asp:Repeater>  
                        <asp:Label ID="lblDetail" runat="server"  CssClass="errormessage"></asp:Label>
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
            &nbsp;
        </td>
    </tr>
    <tr>
        <td align="center">
       
            <table cellspacing="0" cellpadding="0" border="0" align="center" style="text-align:left" width="95%">
        <tr class="buttonlabel" align="left">
        <td>RMA Receive</td></tr>
    </table>
    
        </td>
    </tr>
    <tr>
        <td >
             <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="95%" align="center">
            <tr>
            <td>
			    <table  cellSpacing="0" cellPadding="0" width="100%" border="0">
                <tr>
                    <td>
            
                        <asp:GridView ID="gvReceive" runat="server" Width="100%" GridLines="Both"   AutoGenerateColumns="false"
                        OnRowDataBound="OnRowDataBound" >
                        <RowStyle BackColor="Gainsboro" />
                        <AlternatingRowStyle BackColor="white" />
                        <HeaderStyle  CssClass="buttongrid" ForeColor="white"/>
                        <FooterStyle CssClass="white"  />
                        <PagerStyle ForeColor="#636363" CssClass="copy10grey" HorizontalAlign="Left" />
                            <Columns>
                                <asp:TemplateField ItemStyle-Width="1%">
                                <ItemTemplate>
                                    <a href="JavaScript:divexpandcollapse('div<%# Container.DataItemIndex +  1 %>');">
                                        <img id="imgdiv<%# Container.DataItemIndex +  1 %>" width="9px" border="0" 
                                                                                        src="../Images/plus.png" alt="" /></a>                       
                                </ItemTemplate>
                                <ItemStyle Width="20px" VerticalAlign="Middle"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="S.No." ItemStyle-CssClass="copy10grey"  ItemStyle-Width="1%" HeaderStyle-CssClass="buttongrid">
                                    <ItemTemplate>
                                            <%# Container.DataItemIndex + 1%>                  
                                    </ItemTemplate>
                                </asp:TemplateField> 
                                <asp:TemplateField HeaderText="Receive Date" SortExpression="ReceiveDate" HeaderStyle-CssClass="buttongrid" ItemStyle-HorizontalAlign="Left"  ItemStyle-CssClass="copy10grey" ItemStyle-Width="15%">
                                    <ItemTemplate><%#Eval("ReceiveDate")%></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Approved By"  SortExpression="ApprovedBy" ItemStyle-CssClass="copy10grey"  ItemStyle-Width="15%"
                                    HeaderStyle-CssClass="buttongrid">
                                    <ItemTemplate>
                                            <%# Eval("ApprovedBy") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Comment" HeaderStyle-CssClass="buttongrid" ItemStyle-HorizontalAlign="Left"  ItemStyle-CssClass="copy10grey" ItemStyle-Width="68%">
                                    <ItemTemplate><%#Eval("Comments")%></ItemTemplate>
                                </asp:TemplateField>                                             
                                <asp:TemplateField>
                                    <ItemTemplate>
                                    <tr>
                                    <td colspan="100%">
                                        <div id="div<%# Container.DataItemIndex +  1 %>"  style="overflow:auto; display:none;
                                                        position: relative; left: 15px; overflow: auto">
                                        <asp:GridView ID="gvReceiveDetail" runat="server" AutoGenerateColumns="false" 
                                                Width="99%" >
                                            <RowStyle BackColor="Gainsboro" />
                                            <AlternatingRowStyle BackColor="white" />
                                            <HeaderStyle  CssClass="buttongrid" ForeColor="white"/>
                                            <FooterStyle CssClass="white"  />
                                            <PagerStyle ForeColor="#636363" CssClass="copy10grey" HorizontalAlign="Left" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No." ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="1%">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex +  1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                            
                                                <asp:TemplateField HeaderText="SKU" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="15%">
                                                    <ItemTemplate><%#Eval("SKU")%></ItemTemplate>
                                                </asp:TemplateField>
                                                            
                                                <asp:TemplateField HeaderText="Product Name" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="20%">
                                                    <ItemTemplate><%#Eval("ProductName")%></ItemTemplate>
                                                </asp:TemplateField>
                                                            
                                                <asp:TemplateField HeaderText="Qty Received" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                                    <ItemTemplate><%#Eval("QtyReceived")%></ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ESN Received" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="15%">
                                                    <ItemTemplate><%#Eval("ESNReceived")%></ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Shipping Tracking#" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="15%">
                                                    <ItemTemplate><%#Eval("ShippingTrackingNumber")%></ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Status" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                                    <ItemTemplate><%#Eval("ReceiveStatus")%></ItemTemplate>
                                                </asp:TemplateField>                                                           
                                                            
                                            </Columns>
                                        </asp:GridView>

                                    </div>
                                    </td>
                                    </tr>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
            <asp:Label ID="lblReceive" runat="server"  CssClass="errormessage"></asp:Label>                        
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
            &nbsp;
        </td>
    </tr>
    <tr>
        <td align="center">
       
            <table cellspacing="0" cellpadding="0" border="0" align="center" style="text-align:left" width="95%">
        <tr class="buttonlabel" align="left">
        <td>Customer Comments</td></tr>
    </table>
        
        </td>
    </tr>
    <tr>
        <td >
             <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="95%" align="center">
            <tr>
            <td>
			    <table  cellSpacing="0" cellPadding="0" width="100%" border="0">
                <tr>
                    <td>
                        <asp:Repeater ID="rptCustComments" runat="server" >
                        <HeaderTemplate>
                            <table border="0" width="100%" class="box" align="center" cellpadding="5" cellspacing="5">
                            <tr valign="top">
                                <td class="buttongrid" align="left" width="1%">
                                    Line#
                                </td>
                                <td class="buttongrid" align="left" width="10%">
                                    Comment Date
                                        </td>
                                <td class="buttongrid" align="left" width="10%">
                                    Commented By
                                        </td>
                                <td class="buttongrid" align="left" width="79%">
                                    Comment
                                        </td>
                                
                            </tr>        
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr class="<%# Container.ItemIndex % 2 == 0 ? "alternaterow" : "" %>">
                                    
                                    <td class="copy10grey" align="right" width="1%">
                                        <%# Container.ItemIndex + 1 %>                                         
                                    </td>                                    
                                   <td  class="copy10grey" >
                                        <%# Eval("CreateDate") %>                                       
                                        </td>
                                    <td  class="copy10grey" >
                                        <%# Eval("CommentBy") %>
                                        </td> 
                                <td  class="copy10grey" >
                                        <%# Eval("Comments") %>                                       
                                     </td>
                                 
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </table>    
                            </FooterTemplate>
                        </asp:Repeater>   
                        <asp:Label ID="lblCComments" runat="server"  CssClass="errormessage"></asp:Label>
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
            &nbsp;
        </td>
    </tr>
    <tr>
        <td align="center">
       
            <table cellspacing="0" cellpadding="0" border="0" align="center" style="text-align:left" width="95%">
        <tr class="buttonlabel" align="left">
        <td>Internal Comments</td></tr>
    </table>
        
        </td>
    </tr>
    <tr>
        <td >
            <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="95%" align="center">
            <tr>
            <td>
			    <table  cellSpacing="0" cellPadding="0" width="100%" border="0">
                <tr>
                    <td>
                        <asp:Repeater ID="rptInternalComments" runat="server" >
                        <HeaderTemplate>
                            <table border="0" width="100%" class="box" align="center" cellpadding="5" cellspacing="5">
                            <tr valign="top">
                                <td class="buttongrid" align="left" width="1%">
                                    Line#
                                </td>
                                <td class="buttongrid" align="left" width="10%">
                                    Comment Date
                                        </td>
                                <td class="buttongrid" align="left" width="10%">
                                    Commented By
                                        </td>
                                <td class="buttongrid" align="left" width="79%">
                                    Comment
                                        </td>
                                
                            </tr>        
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr class="<%# Container.ItemIndex % 2 == 0 ? "alternaterow" : "" %>">
                                    
                                    <td class="copy10grey" align="right" width="1%">
                                        <%# Container.ItemIndex + 1 %>                                         
                                    </td>
                                    <td  class="copy10grey" >
                                        <%# Eval("CreateDate") %>                                       
                                        </td>
                                    <td  class="copy10grey" >
                                        <%# Eval("CommentBy") %>
                                        </td>
                                    <td  class="copy10grey" >
                                        <%# Eval("Comments") %>                                       
                                     </td>
                                    
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </table>    
                            </FooterTemplate>
                        </asp:Repeater>   
                        <asp:Label ID="lblIComments" runat="server"  CssClass="errormessage"></asp:Label>
                    </td>
                </tr>
                </table>
                
                </td>
            </tr>

            </table>    
        </td>
    </tr>
        <tr>
            <td align="center">
                <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="button"  OnClientClick="return close_window();"  />
            </td>
        </tr>
    </table>  
        <br />
        <br />
        
        <table cellspacing="0" cellpadding="0" border="0" align="center" width="100%">
        <tr>
            <td>
                <foot:MenuFooter ID="Foter" runat="server"></foot:MenuFooter>
            </td>
        </tr>
        </table>
    </form>
</body>
</html>
