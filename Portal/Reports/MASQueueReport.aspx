<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MASQueueReport.aspx.cs" Inherits="avii.Reports.MASQueueReport" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>
<%@ Register TagPrefix="menu" TagName="Menu" Src="~/Controls/Header.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Portal MAS Queue</title>
     <link href="../aerostyle.css" type="text/css" rel="stylesheet"/>
   	 <link rel="stylesheet"  type="text/css" href="../fullfillment/calendar/dhtmlgoodies_calendar.css" media="screen"></link>
		<script type="text/javascript" src="../fullfillment/calendar/dhtmlgoodies_calendar.js"></script>  
        
        <script type="text/javascript" src="../JQuery/jquery-latest.js"></script>
        <script type="text/javascript" >

            $(document).ready(function () {
                $('#txtFromDate').focusin(function (event) {
                    $('#img1').click();
                    event.preventDefault();

                });
                $('#txtFromDate').keypress(function (event) {
                    $('#img1').click();
                    event.preventDefault();

                });
                $('#txtEndDate').focusin(function (event) {
                    $('#img2').click();
                    event.preventDefault();

                });
                $('#txtEndDate').keypress(function (event) {
                    $('#img2').click();
                    event.preventDefault();

                });
            });
            $(document).AjaxReady(function () {
                $('#txtFromDate').focusin(function (event) {
                    $('#img1').click();
                    event.preventDefault();

                });
                $('#txtFromDate').keypress(function (event) {
                    $('#img1').click();
                    event.preventDefault();

                });
                $('#txtEndDate').focusin(function (event) {
                    $('#img2').click();
                    event.preventDefault();

                });
                $('#txtEndDate').keypress(function (event) {
                    $('#img2').click();
                    event.preventDefault();

                });
            });

            
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
           
            <td   ><br />
    <table align="center" style="text-align:left" width="100%">
                <tr class="button" align="left">
                <td>&nbsp;Portal MAS Queue</td></tr>
             </table>
     
     
     
      <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
      <asp:UpdatePanel ID="UpdatePanel1"  runat="server" >
     <ContentTemplate>
     
     <table  align="center" style="text-align:left" width="99%">
     <tr>
        <td>
           <asp:Label ID="lblMsg" runat="server" CssClass="errormessage"></asp:Label>
           
            
        </td>
     </tr>
     <tr><td class="copy10grey">
                - Please select your search
                criterial to narrow down the search and record selection.<br />
                - Atleast one check box "To MAS or From MAS" should be selected.<br />
                - TO MAS: Fulfillment records queued up for MAS 90 <br />
                - TO FROM: Fulfillment records recieved from  MAS 90 
                
                </td></tr>
     </table>
     
     
           
      <table bordercolor="#839abf" border="1" width="100%" align="center" cellpadding="0" cellspacing="0">
      <tr>
        <td>
         <asp:Panel  ID="pnlSearch" runat="server"  DefaultButton="btnSearch" >
            <table width="100%" border="0" class="box" align="center" cellpadding="2" cellspacing="2">
     <tr style="height:1px">
     <td style="height:1px"></td>
     </tr>
     
           
            <tr>
                <td class="copy10grey" align="right">
                    From Date:
                </td>
                <td>
                &nbsp;<asp:TextBox ID="txtFromDate" CssClass="copy10grey" runat="server" Width="80%" MaxLength="15" ></asp:TextBox>
                <img id="img1" alt=""  onclick="document.getElementById('<%=txtFromDate.ClientID%>').value = ''; displayCalendar(document.getElementById('<%=txtFromDate.ClientID%>'),'mm/dd/yyyy',this,true);" src="../fullfillment/calendar/sscalendar.jpg" />
                
                </td>
                <td  width="10%">
                    &nbsp;
                </td>
                <td class="copy10grey" align="right">
                    End Date:
                </td>
                <td>
                &nbsp;<asp:TextBox ID="txtEndDate"   CssClass="copy10grey" runat="server" Width="75%" MaxLength="12" ></asp:TextBox>
                <img id="img2" alt=""  onclick="document.getElementById('<%=txtEndDate.ClientID%>').value = ''; displayCalendar(document.getElementById('<%=txtEndDate.ClientID%>'),'mm/dd/yyyy',this,true);" src="../fullfillment/calendar/sscalendar.jpg" />
                
                </td>   
                </tr>
                <tr>
                <td class="copy10grey" align="right">
                    Fulfillment#:
                </td>
                <td>
                &nbsp;<asp:TextBox ID="txtPO" CssClass="copy10grey" runat="server" Width="80%" MaxLength="15" ></asp:TextBox>
                
                </td>
                <td  width="10%">
                    &nbsp;
                </td>
                <td class="copy10grey" align="right">
                    Sales Order#:
                </td>
                <td>
                &nbsp;<asp:TextBox ID="txtSO"   CssClass="copy10grey" runat="server" Width="75%" MaxLength="15" ></asp:TextBox>
                
                </td>   
                </tr>
                <tr>
            <td align="right">
                <asp:CheckBox ID="chkToMas" Checked="true" CssClass="copy10grey" Text="To MAS" runat="server" />
            </td>
            <td  align="right">
                <asp:CheckBox ID="chkFromMas"  Checked="true" CssClass="copy10grey" Text="From MAS" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
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
         <asp:Panel ID="toMasPnl" Visible="false" runat="server">
         
      <table align="center" width="100%" border="0" cellpadding="0" cellspacing="0">
    <tr>
        <td >
        <table width="100%" cellpadding="0" cellspacing="0">
        <tr class="button">
            <td align="left" >
             TO MAS search result
            </td>
            <td align="right">
            <asp:Label ID="lblCount" runat="server"  ></asp:Label>   
        &nbsp;
            </td>
        </tr>
        </table>
        </td>
    </tr>
    <tr>
        <td><asp:Label ID="lblToMasMsg" runat="server" CssClass="errormessage" ></asp:Label>   
            <asp:GridView ID="gvPOQuery" OnPageIndexChanging="gridView_PageIndexChanging"    AutoGenerateColumns="false"  
                DataKeyNames="SalesOrderNo, OrderDate"  Width="100%"  
            ShowFooter="false" runat="server" GridLines="Both" OnRowDataBound="GridView1_RowDataBound" 
            PageSize="20" AllowPaging="true" OnRowCommand = "GridView1_RowCommand"
            BorderStyle="Outset" OnRowDeleting = "GridView1_RowDeleting" OnRowDeleted = "GridView1_RowDeleted" 
            AllowSorting="false" > 
            <RowStyle BackColor="Gainsboro" />
            <AlternatingRowStyle BackColor="white" />
            <HeaderStyle  CssClass="button" ForeColor="white"/>
            <FooterStyle CssClass="white"  />
            <PagerStyle ForeColor="#636363" CssClass="copy10grey" HorizontalAlign="Left" />
            <Columns>
                <asp:ButtonField   ButtonType="Image"  ImageUrl ="../images/plus.gif"
                    CommandName="sel"  />
               <%-- <asp:TemplateField ItemStyle-CssClass="copy10grey"  ItemStyle-Width="3%">
                    <ItemTemplate>
                        <asp:CheckBox ID="chk"  runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>   --%>              
                
                <asp:TemplateField HeaderText="Sales Order#" SortExpression="SalesOrderNo" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                    <ItemTemplate>
                        <%# Eval("SalesOrderNo") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Fulfillment Date"  SortExpression="OrderDate" ItemStyle-CssClass="copy10grey"  ItemStyle-Width="5%">
                    <ItemTemplate><%# DataBinder.Eval(Container.DataItem, "OrderDate", "{0:d}")%></ItemTemplate>
                </asp:TemplateField>

              

                <asp:TemplateField HeaderText="Contact Name" SortExpression="BillToName" ItemStyle-CssClass="copy10grey" ItemStyle-Width="15%">
                    <ItemTemplate><%# Eval("BillToName")%></ItemTemplate>
                    
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="Ship Via" SortExpression="ShipVia" ItemStyle-CssClass="copy10grey"  ItemStyle-Width="7%">
                    <ItemTemplate><%# Eval("ShipVia")%></ItemTemplate>
                    <%--<EditItemTemplate>
                        <asp:TextBox ID="txtVia" CssClass="copy10grey"  MaxLength="200" TextMode="MultiLine" Text='<%# Eval("Tracking.ShipToBy") %>' runat="server"></asp:TextBox>
                    </EditItemTemplate>--%>
                </asp:TemplateField>
               
                

                <asp:TemplateField HeaderText="Street Address" SortExpression="ShipToAddress1"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="25%">
                    <ItemTemplate><%#Eval("ShipToAddress1")%></ItemTemplate>
                </asp:TemplateField> 
                
                <asp:TemplateField HeaderText="City"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                    <ItemTemplate><%#Eval("ShipToCity")%></ItemTemplate>
                </asp:TemplateField>                                              
                <asp:TemplateField HeaderText="State"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                    <ItemTemplate><%#Eval("ShipToState")%></ItemTemplate>
                </asp:TemplateField>                                              

                <asp:TemplateField HeaderText="Zip"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="7%">
                    <ItemTemplate><%#Eval("ShipToZipCode")%></ItemTemplate>
                </asp:TemplateField>  

                <asp:TemplateField HeaderText="Status"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="7%">
                    <ItemTemplate><%#Eval("POSTATUS")%></ItemTemplate>
                </asp:TemplateField>   
                
                	    
			   <asp:templatefield headertext=""  itemstyle-horizontalalign="center"  >
                    <itemtemplate>
                    
                        <asp:imagebutton id="imgdelpo" runat="server"  commandname="delete" alternatetext="delete po" imageurl="~/images/delete.png" />
                        
                        
                    </itemtemplate>
                </asp:templatefield>
                
                <asp:TemplateField  >
			        <ItemTemplate>
			            <tr>
                            <td colspan="100%">
                                <div id="div<%# Eval("SalesOrderno") %>"  >
                                    <asp:GridView ID="GridView2"  BackColor="White" Width="100%" Visible="False"
                                        AutoGenerateColumns="false" Font-Names="Verdana" runat="server" 
                                        
                                        BorderStyle="Double" BorderColor="#0083C1">
                                        <RowStyle BackColor="Gainsboro" />
                                        <AlternatingRowStyle BackColor="white" />
                                        <HeaderStyle  CssClass="button" ForeColor="white"/>
                                        <FooterStyle CssClass="white"  />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Product Code" SortExpression="ItemCode" ItemStyle-CssClass="copy10grey" HeaderStyle-HorizontalAlign="Left"  ItemStyle-Width="20%">
                                                <ItemTemplate><%# Eval("ItemCode")%></ItemTemplate>
                                            </asp:TemplateField>
                                                                                                                                    
                                            
                                            <asp:TemplateField HeaderText="Qty" ItemStyle-HorizontalAlign="Right" SortExpression="QuantityOrderedOriginal"  ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                <%# Convert.ToInt32(Eval("QuantityOrderedOriginal")) %></ItemTemplate>
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField HeaderText="ESN" SortExpression="AliasItemNo" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                    <%# Eval("AliasItemNo") %>
                                                </ItemTemplate>
                                                
                                            </asp:TemplateField>
                                            
                                            <%--<asp:TemplateField HeaderText="MDN" SortExpression="MdnNumber"  ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                                <ItemTemplate><%#Eval("MdnNumber")%></ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtMdn" CssClass="copy10grey" MaxLength="30"  Text='<%# Eval("MdnNumber") %>' runat="server"></asp:TextBox>
                                                </EditItemTemplate>                                                
                                            </asp:TemplateField>
                                        
                                            <asp:TemplateField HeaderText="MSID" SortExpression="MsID"  ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                                <ItemTemplate><%#Eval("MsID")%></ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtMsid" CssClass="copy10grey" MaxLength="30"  Text='<%# Eval("MsID") %>' runat="server"></asp:TextBox>
                                                </EditItemTemplate>                                                 
                                            </asp:TemplateField>
                                            
--%>
                                            <asp:TemplateField HeaderText="MslNumber" SortExpression="CommentText" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                    <%#Eval("CommentText")%>
                                                </ItemTemplate>
                                                
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
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;    
        </td>
    </tr>
    </table>
    

    </asp:Panel>  
    <asp:Panel ID="frmMasPnl" Visible="false"  runat="server">
         
    <table align="center" width="100%" border="0" cellpadding="0" cellspacing="0">
    <tr>
        <td >
        <table width="100%" cellpadding="0" cellspacing="0">
        <tr class="button">
            <td align="left" >
            From MAS search result
            </td>
            <td align="right">
            <asp:Label ID="lblFromMas" runat="server"  ></asp:Label>   
        &nbsp;
            </td>
        </tr>
        </table>
        </td>
    </tr>
    <tr>
        <td><asp:Label ID="lblFrmMasMsg" runat="server" CssClass="errormessage"></asp:Label>
            <asp:GridView ID="gvFromMas" OnPageIndexChanging="gvFromMas_PageIndexChanging"    AutoGenerateColumns="false"  
                DataKeyNames="SalesOrderNo1"  Width="100%"  
            ShowFooter="false" runat="server" GridLines="Both" 
            PageSize="25" AllowPaging="true" OnRowCommand = "gvFromMas_RowCommand"
            BorderStyle="Outset" 
            AllowSorting="false" > 
            <RowStyle BackColor="Gainsboro" />
            <AlternatingRowStyle BackColor="white" />
            <HeaderStyle  CssClass="button" ForeColor="white"/>
            <FooterStyle CssClass="white"  />
            <PagerStyle ForeColor="#636363" CssClass="copy10grey" HorizontalAlign="Left" />
            <Columns>
                <asp:ButtonField   ButtonType="Image"  ImageUrl ="../images/plus.gif"
                    CommandName="sel"  />
               <%-- <asp:TemplateField ItemStyle-CssClass="copy10grey"  ItemStyle-Width="3%">
                    <ItemTemplate>
                        <asp:CheckBox ID="chk"  runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>   --%>              
                
                <asp:TemplateField HeaderText="MAS Order#" SortExpression="SalesOrderNo" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                    <ItemTemplate>
                        <%# Eval("SalesOrderNo1") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Fulfillment#" SortExpression="SalesOrderNo" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                    <ItemTemplate>
                        <%# Eval("SalesOrderNo") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Fulfillment Date"  SortExpression="OrderDate" ItemStyle-CssClass="copy10grey"  ItemStyle-Width="5%">
                    <ItemTemplate><%# DataBinder.Eval(Container.DataItem, "OrderDate", "{0:d}")%></ItemTemplate>
                </asp:TemplateField>

              

                <asp:TemplateField HeaderText="Contact Name" SortExpression="BillToName" ItemStyle-CssClass="copy10grey" ItemStyle-Width="15%">
                    <ItemTemplate><%# Eval("BillToName")%></ItemTemplate>
                    
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="Ship Via" SortExpression="ShipVia" ItemStyle-CssClass="copy10grey"  ItemStyle-Width="7%">
                    <ItemTemplate><%# Eval("ShipVia")%></ItemTemplate>
                    <%--<EditItemTemplate>
                        <asp:TextBox ID="txtVia" CssClass="copy10grey"  MaxLength="200" TextMode="MultiLine" Text='<%# Eval("Tracking.ShipToBy") %>' runat="server"></asp:TextBox>
                    </EditItemTemplate>--%>
                </asp:TemplateField>
               
                

                <asp:TemplateField HeaderText="Street Address" SortExpression="ShipToAddress1"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="25%">
                    <ItemTemplate><%#Eval("ShipToAddress1")%></ItemTemplate>
                </asp:TemplateField> 
                
                <asp:TemplateField HeaderText="City"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                    <ItemTemplate><%#Eval("ShipToCity")%></ItemTemplate>
                </asp:TemplateField>                                              
                <asp:TemplateField HeaderText="State"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                    <ItemTemplate><%#Eval("ShipToState")%></ItemTemplate>
                </asp:TemplateField>                                              

                <asp:TemplateField HeaderText="Zip"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="7%">
                    <ItemTemplate><%#Eval("ShipToZipCode")%></ItemTemplate>
                </asp:TemplateField>  

                <asp:TemplateField HeaderText="Status"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="7%">
                    <ItemTemplate><%#Eval("POSTATUS")%></ItemTemplate>
                </asp:TemplateField>   
                
                	    
			   <%--<asp:templatefield headertext=""  itemstyle-horizontalalign="center"  >
                    <itemtemplate>
                    
                        <asp:imagebutton id="imgdelpo" runat="server"  commandname="delete" alternatetext="delete po" imageurl="~/images/delete.png" />
                        
                        
                    </itemtemplate>
                </asp:templatefield>--%>
                
                <asp:TemplateField  >
			        <ItemTemplate>
			            <tr>
                            <td colspan="100%">
                                <div id="div<%# Eval("SalesOrderno") %>"  >
                                    <asp:GridView ID="GridView2"  BackColor="White" Width="100%" Visible="False"
                                        AutoGenerateColumns="false" Font-Names="Verdana" runat="server" 
                                        
                                        BorderStyle="Double" BorderColor="#0083C1">
                                        <RowStyle BackColor="Gainsboro" />
                                        <AlternatingRowStyle BackColor="white" />
                                        <HeaderStyle  CssClass="button" ForeColor="white"/>
                                        <FooterStyle CssClass="white"  />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Product Code" SortExpression="ItemCode" ItemStyle-CssClass="copy10grey" HeaderStyle-HorizontalAlign="Left"  ItemStyle-Width="20%">
                                                <ItemTemplate><%# Eval("ItemCode")%></ItemTemplate>
                                            </asp:TemplateField>
                                                                                                                                    
                                            
                                            <asp:TemplateField HeaderText="Qty" ItemStyle-HorizontalAlign="Right" SortExpression="QuantityOrderedOriginal"  ItemStyle-CssClass="copy10grey" ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                <%# Convert.ToInt32(Eval("QuantityOrderedOriginal")) %></ItemTemplate>
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField HeaderText="ESN" SortExpression="AliasItemNo" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                    <%# Eval("AliasItemNo") %>
                                                </ItemTemplate>
                                                
                                            </asp:TemplateField>
                                            
                                            <%--<asp:TemplateField HeaderText="MDN" SortExpression="MdnNumber"  ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                                <ItemTemplate><%#Eval("MdnNumber")%></ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtMdn" CssClass="copy10grey" MaxLength="30"  Text='<%# Eval("MdnNumber") %>' runat="server"></asp:TextBox>
                                                </EditItemTemplate>                                                
                                            </asp:TemplateField>
                                        
                                            <asp:TemplateField HeaderText="MSID" SortExpression="MsID"  ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                                <ItemTemplate><%#Eval("MsID")%></ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtMsid" CssClass="copy10grey" MaxLength="30"  Text='<%# Eval("MsID") %>' runat="server"></asp:TextBox>
                                                </EditItemTemplate>                                                 
                                            </asp:TemplateField>
                                            
--%>
                                            <asp:TemplateField HeaderText="MslNumber" SortExpression="CommentText" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                    <%#Eval("CommentText")%>
                                                </ItemTemplate>
                                                
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
        </td>
    </tr>

       
    </table>   
    </asp:Panel>        
                
    <script type='text/javascript'>


        prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(EndRequest);
        function EndRequest(sender, args) {
            //alert("EndRequest");
            $(document).AjaxReady();
        }
        </script>
    
    
    
     </ContentTemplate>
     </asp:UpdatePanel>
     
    </td>
        </tr>
        <tr>
        <td>
            <asp:UpdateProgress ID="UpdateProgress1" runat="server"
                     DynamicLayout="false">
            <ProgressTemplate>
                <img src="/Images/ajax-loaders.gif" /> Loading ...
            </ProgressTemplate>
        </asp:UpdateProgress>
        </td>
    </tr>
    </table>
     <br />
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
