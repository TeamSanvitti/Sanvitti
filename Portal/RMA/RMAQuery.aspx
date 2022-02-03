<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="RMAQuery.aspx.cs" Inherits="avii.Admin.RMA.RMAQuery" ValidateRequest="false"%>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="/Controls/Header.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Return Merchandise Authorization (RMA) - Search</title>
    <link href="/aerostyle.css" type="text/css" rel="stylesheet" />
    <link href="/Styles.css" type="text/css" rel="stylesheet" />

    <script language="javaScript" src="../mm_menu.js" type="text/javascript"></script>


    <link rel="stylesheet" type="text/css" href="/fullfillment/calendar/dhtmlgoodies_calendar.css" media="screen"></link>
    <script type="text/javascript" src="/fullfillment/calendar/dhtmlgoodies_calendar.js"></script>
<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        
    <script type="text/javascript">
        
        function  doReadonly(evt)
        {
            
           evt.keyCode = 0;
           return false;
        }
        function set_focus() {
            var img = document.getElementById("img1");
            var st = document.getElementById("ddlStatus");
            st.focus();
            img.click();
        }
        function set_focus1() {
            var img = document.getElementById("img1");
            var st = document.getElementById("ddlStatus");
            st.focus();
            img.click();
        }

        function set_focus2() {
            var img = document.getElementById("img2");
            var st = document.getElementById("ddlStatus");
            st.focus();
            img.click();
        } 

        function selectcompany(obj) {

            var objid = "";
            if (obj.id.indexOf('lnkedit') > -1)
                objid = 'lnkedit';
            objhdCompanyID = document.getElementById(obj.id.replace(objid, 'hdnCompanyid'));

            document.getElementById("hdnCompanyId").value = objhdCompanyID.value
        }

        function displayStatus(obj)
        {
            var count = document.getElementById('hdncount').value;
            var statuspanel = document.getElementById('statuspanel');
            if(obj=='0')
            {
                statuspanel.style.display = 'none';
                return false;
            }
            else
            {
                if(count=='1')
                {
                    statuspanel.style.display = 'block';
                }
                else
                    alert('RMA not selected!');
                return false;
            }
        }
        function validateUser()
        {
            var status = document.getElementById('ddlchangestatus');
            if(status.selectedIndex < 1)
            {
               alert('Please select a status');
               return false;
            }   
            var confirmflag = confirm('Do you want to change the status of selected RMA?');
            if(confirmflag)
                return true;
            else
            {
                displayStatus(0);
                return false; 
            } 
               
        }
        
        function RowSelected(sender, args)
        {
            document.getElementById('hdncount').value="1";
        }
    </script>
    
    </telerik:RadCodeBlock>


</head>
<body bgcolor="#ffffff" leftmargin="0" rightmargin="0" topmargin="0">
    <form id="frmRMAItemLookup" runat="server">
    <table cellspacing="0" cellpadding="0" border="0" align="center" width="100%">
        <tr>
            <td>
                <head:MenuHeader ID="MenuHeader1" runat="server"/>
            </td>
        </tr>
    
        <tr><td>
        
         <table width="95%" align="center" cellspacing="0" cellpadding="0"> 
        <tr>
            <td>
                <table style="text-align: left; width:100%;" align="center" class="copy10grey">
                    <tr>
                        <td>
                            &nbsp;<input type="hidden" ID="hdncount" />
                        </td>
                    </tr>
                    <tr>
                        <td class="button" align="left">
                            &nbsp; Return Merchandise Authorization (RMA) - Search
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblMsg" runat="server" CssClass="errormessage"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table bordercolor="#839abf" border="1" cellspacing="0" cellpadding="0" width="100%" align="center">
                    <tr bordercolor="#839abf">
                        <td>
                            <table style="text-align: left; width: 100%;" cellspacing="0" cellpadding="0" align="center" class="copy10grey">
                                <tr>
                                    <td>
                                        <table class="box" width="100%" align="center" cellspacing="2" cellpadding="2">
                                            <tr>
                                                <td >
                                                    <asp:Label ID="lblComapny" CssClass="copy10grey" runat="server" Text="Company:"></asp:Label>
                                                    </td>
                                                        <asp:HiddenField ID="hdnrmaGUIDs" runat="server" />
                                                        <asp:HiddenField ID="hdncompany" runat="server" />
                                                        <asp:HiddenField ID="hdnCompanyId" runat="server" />
                                                        <asp:HiddenField ID="hdnUserID" runat="server" />
                                                <td>
                                                    <asp:Panel ID="companyPanel" runat="server">
                                                        <asp:DropDownList ID="ddlCompany" CssClass="copy10grey"
                                                            runat="server">
                                                        </asp:DropDownList>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="copy10grey" width="15%">
                                                    RMA#:
                                                </td>
                                                <td width="35%">
                                                    <asp:TextBox runat="server" ID="rmanumber" 
CssClass="copy10grey" Width="60%" />
                                                </td>
                                                 <td class="copy10grey" width="15%">
                                                    ESN:
                                                </td>
                                                <td  width="35%">
                                                    <asp:TextBox runat="server" ID="txtESNSearch" 
MaxLength="35" CssClass="copy10grey" Width="60%"  />
                                                </td>
                                               
                                            </tr>
                                            <tr>
                                                <td class="copy10grey">
                                                    RMA From Date:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtRMADate" runat="server" Width="60%" onfocus="set_focus1();" onkeypress="return doReadonly(event);"
                                                        CssClass="copy10grey" MaxLength="15" Text="" />
                                                    <img id="img1" alt="" onclick="document.getElementById('<%=txtRMADate.ClientID%>').value = ''; displayCalendar(document.getElementById('<%=txtRMADate.ClientID%>'),'mm/dd/yyyy',this,true);"
                                                        src="../../fullfillment/calendar/sscalendar.jpg" />
                                                    
                                               </td>
                                                <td class="copy10grey">
                                                    RMA To Date:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtRMADateTo" runat="server" Width="60%" onfocus="set_focus2();" onkeypress="return doReadonly(event);"
                                                        CssClass="copy10grey" MaxLength="15" Text="" />
                                                    <img id="img2" alt="" onclick="document.getElementById('<%=txtRMADateTo.ClientID%>').value = ''; displayCalendar(document.getElementById('<%=txtRMADateTo.ClientID%>'),'mm/dd/yyyy',this,true);"
                                                        src="../../fullfillment/calendar/sscalendar.jpg" />
                                                    
                                               </td>
                                            </tr>
                                            <tr valign="top">
                                                <td class="copy10grey">
                                                    Status:</td>
                                                <td>
                                                    <asp:DropDownList ID="ddlStatus" runat="server" 
Class="copy10grey" Width="60%" >
                                                         <asp:ListItem  Value="0" >------</asp:ListItem>
                                                        <asp:ListItem Value="1" >Pending</asp:ListItem>
                                                        <asp:ListItem  Value="2">Received</asp:ListItem>
                                                        <asp:ListItem  Value="3">Pending for Repair</asp:ListItem>
                                                        <asp:ListItem  Value="4">Pending for Credit</asp:ListItem>
                                                        <asp:ListItem  Value="5">Pending for Replacement</asp:ListItem>
                                                        <asp:ListItem  Value="6">Approved</asp:ListItem>
                                                        <asp:ListItem  Value="7">Returned</asp:ListItem>
                                                        <asp:ListItem  Value="8">Credited</asp:ListItem>
                                                        <asp:ListItem  Value="9">Denied</asp:ListItem>
                                                        <asp:ListItem  Value="10">Closed</asp:ListItem>

                                                        <asp:ListItem  Value="11" >Out with OEM for repair</asp:ListItem>
                                                        <asp:ListItem  Value="12">Back to Stock -NDF</asp:ListItem>
                                                        <asp:ListItem  Value="13">Back to Stock- Credited</asp:ListItem>
                                                        <asp:ListItem  Value="14">Back to Stock – Replaced by OEM</asp:ListItem>
                                                        <asp:ListItem  Value="15">Repaired by OEM</asp:ListItem>
                                                        <asp:ListItem  Value="16">Replaced BY OEM</asp:ListItem>
                                                        <asp:ListItem  Value="17">Replaced BY AV</asp:ListItem>
                                                        <asp:ListItem  Value="18">Repaired By AV</asp:ListItem>
                                                        <asp:ListItem  Value="19">NDF (No Defect Found)</asp:ListItem>
                                                        <asp:ListItem  Value="20">PRE-OWNED – A stock</asp:ListItem>
                                                        <asp:ListItem Value="21" >PRE-OWEND - B Stock</asp:ListItem>
                                                        <asp:ListItem Value="22" >PRE-OWEND – C Stock</asp:ListItem>
                                                        <asp:ListItem Value="23" >Rejected</asp:ListItem>
<asp:ListItem Value="24" >RTS (Return To Stock)</asp:ListItem>
<asp:ListItem Value="25" >Incomplete</asp:ListItem>
<asp:ListItem Value="26" >Damaged</asp:ListItem>
<asp:ListItem Value="27" >Preowned</asp:ListItem>
<asp:ListItem Value="28" >Return to OEM</asp:ListItem>
<asp:ListItem Value="29" >Returned to Stock</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                
                                                <td class="copy10grey">
                                                UPC:</td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="txtUPC" CssClass="copy10grey" Width="60%" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="copy10grey">AVSO:</td>
                                                <td><asp:TextBox runat="server" ID="txtAVSO"  MaxLength="30" 
 CssClass="copy10grey" Width="60%" /></td>
                                                <td class="copy10grey">Purchase Order#:</td>
                                                <td><asp:TextBox runat="server" ID="txtPONum" MaxLength="30"
 CssClass="copy10grey" Width="60%" /></td>
                                            </tr>
                                             <tr>
                                                <td class="copy10grey">Return Reason:</td>
                                                <td><asp:DropDownList ID="ddReason" CssClass="copy10grey" 
runat="server" Width="60%" >
                                                              </asp:DropDownList></td>
                                                <td class="copy10grey"></td>
                                                <td></td>
                                            </tr>
                                             
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <hr />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:Button ID="Button1" runat="server" Text="Search RMA" OnClick="search_click"
                                            CssClass="button" Height="24px" Width="130px" />&nbsp;
                                            

                                        
                                        <asp:Button ID="Button2" runat="server" OnClick="Button2_Click1" Text="Cancel RMA"  Height="24px" Width="130px"
                                            CssClass="button" />
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
                <asp:Panel ID="pnlGrid" runat="server">
                
                <table width="100%" class="copy10grey" style="text-align: left">
                    <tr>
                        <td>
                        <table>
                        <tr>
                            <td>
                                <asp:Button ID="btnExport" runat="server" Text="Export to Excel" OnClick="btnExport_click" CssClass="button" />&nbsp;     
                                <asp:HyperLink ID="btnRMAReport" runat="server" Text="RMA Report" Visible="false" ViewStateMode="Disabled"
                                      NavigateUrl="/rma/rmalist.aspx" Target="_blank" />
                                <asp:HyperLink ID="hlkRMASummary" runat="server" Text="RMA Summary" Visible="false" ViewStateMode="Disabled"
                                      NavigateUrl="/rma/rmaSummary.aspx" Target="_blank" />
                               
                            </td>
                            <td>
                                <asp:Panel runat="server" ID="statuspanel" >
                                <table style="background:#969696">
                                <tr>
                                    
                                    <td class="copy10grey">
                                        Status
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlchangestatus" runat="server" Class="copy10grey" Width="165px">
                                            <asp:ListItem Value="0">------</asp:ListItem>
                                            <asp:ListItem Value="1">Pending</asp:ListItem>
                                            <asp:ListItem Value="2">Received</asp:ListItem>
                                            <asp:ListItem Value="3">Pending for Repair</asp:ListItem>
                                            <asp:ListItem Value="4">Pending for Credit</asp:ListItem>
                                            <asp:ListItem Value="5">Pending for Replacement</asp:ListItem>
                                            <asp:ListItem Value="6">Approved</asp:ListItem>
                                            <asp:ListItem Value="7">Cancelled</asp:ListItem>
                                            <asp:ListItem Value="8">Returned</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_click" OnClientClick="return validateUser();"
                                            CssClass="buybt" />&nbsp;
                                        <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Cancel" OnClientClick="return displayStatus(0);"
                                            CssClass="buybt" />
                                    </td>
                                </tr>
                                </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        </table>
                        
                        
                    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager> 
               
                    <table width="100%">
                        <tr>
                            <td align="right"> 
            
                <asp:Label ID="lblCount" CssClass="copy10grey" runat="server" ></asp:Label>
            </td>
                </tr>


                    <tr>
                            <td> 
                    <telerik:RadGrid ID="radGridRmaDetails" Skin="WebBlue" runat="server" Width="100%" GridLines="None" 
                    AutoGenerateColumns="False" PageSize="25" AllowSorting="True" AllowPaging="True"
                      OnPreRender="RadGrid1_PreRender" OnDetailTableDataBind="RadGrid1_DetailTableDataBind" OnPageIndexChanged="RadGrid1_PageIndexChanged"
                    OnNeedDataSource="RadGrid1_NeedDataSource" AllowMultiRowSelection="true" 
                    ShowStatusBar="false" OnDeleteCommand="RadGrid1_DeleteCommand" OnItemCommand="RadGrid1_ItemCommand" OnItemDataBound="RadGrid1_ItemDataBound">
                    <PagerStyle Mode="NumericPages"></PagerStyle>
                    <ClientSettings>
                         <Selecting AllowRowSelect="true" />
                         <ClientEvents OnRowSelected="RowSelected" />
                      </ClientSettings>
                    <MasterTableView DataKeyNames="RmaGUID" ClientDataKeyNames="RmaGUID" EditMode="InPlace" 
                    AllowMultiColumnSorting="True" Width="100%" 
                        CommandItemDisplay="Top" HierarchyLoadMode="Client" Name="RMAMaster">
                        <CommandItemTemplate>
                        <div style="padding:0px 0px;">
                            
                            <asp:LinkButton ID="LinkButton2" runat="server"  CommandName="Selectedqq" OnClientClick="return displayStatus(1);" ><img style="border:0px;vertical-align:middle;" alt="" src="../images/edit.png" />Status Batch Update</asp:LinkButton>
                            
                        </div>
                    </CommandItemTemplate>
                        <DetailTables>
                        <telerik:GridTableView ShowHeader="true" CommandItemDisplay="None" EditMode="InPlace" 
                        BorderWidth="0"  HierarchyLoadMode="Client"
                            Width="100%" runat="server" Name="rmaDetails">
                               <NoRecordsTemplate></NoRecordsTemplate>
                                  <ParentTableRelation>
                                    <telerik:GridRelationFields DetailKeyField="rmaGUID" MasterKeyField="RmaGUID" />    
                                   </ParentTableRelation>
                                   <Columns>
                                        <telerik:GridBoundColumn UniqueName="CompanyName" Visible="false" ReadOnly="true"  HeaderText="CompanyName"/>
                                        <telerik:GridTemplateColumn UniqueName="RMAnum" Visible="false" HeaderText="RMA #"></telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn UniqueName="RmaDate" Visible="false" HeaderText="RmaDate"></telerik:GridTemplateColumn>
                            
                            <telerik:GridButtonColumn CommandName="MyCommandName" HeaderText="esn"  Visible="false" 
                                UniqueName= "esnssss"  DataTextField="esn"></telerik:GridButtonColumn>
                                            <telerik:GridTemplateColumn DataType="System.UInt32"  ItemStyle-Wrap="false"  UniqueName="ESN" HeaderText="ESN">
                                            <ItemTemplate>
                                                <asp:Label ID="lblESN"  runat="server" Text='<%# "#" + DataBinder.Eval(Container.DataItem, "ESN")%>' ></asp:Label>
                                            </ItemTemplate>
                                            
                                        </telerik:GridTemplateColumn>
                                        
                                            <telerik:GridTemplateColumn UniqueName="ItemCode" HeaderText="UPC">
                                            <ItemTemplate>
						<%# DataBinder.Eval(Container.DataItem, "UPC")%>
                                            </ItemTemplate>
                                            
                                        </telerik:GridTemplateColumn>
                                       
                                            <telerik:GridTemplateColumn UniqueName="CallTime" HeaderText="CallTime">
                                            <ItemTemplate>
<%# DataBinder.Eval(Container.DataItem, "CallTime")%>
                                            </ItemTemplate>
                                            
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn UniqueName="Reason"  HeaderText="Reason">
                                            <ItemTemplate>
                                            <asp:HiddenField ID="hdnReason" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "reason")%>' />
                                                <asp:Label ID="lblreason" runat="server" ></asp:Label>
                                            </ItemTemplate>
                                            
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn UniqueName="Status" HeaderText="Status">
                                            <ItemTemplate>
                                                <asp:Label ID="lblstatus" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Status") %>' ></asp:Label>
                                            </ItemTemplate>
                                            
                                        </telerik:GridTemplateColumn>
                                        
                                        <telerik:GridEditCommandColumn UpdateText="Edit" Visible="false" UniqueName="EditCommandColumn" CancelText="Cancel"
                                            EditText="Edit">
                                            <HeaderStyle Width="55px"></HeaderStyle>
                                        </telerik:GridEditCommandColumn>
                                        <telerik:GridButtonColumn UniqueName="EsnDeleteColumn" Visible="false" ConfirmText="Delete this ESN?"  Text="Delete" CommandName="Delete" />
                                   </Columns>
                               </telerik:GridTableView>
                        </DetailTables>
                        <Columns>
                            
                            <telerik:GridClientSelectColumn   UniqueName="colSelect" >
                                    <HeaderStyle Width="10" />
                                </telerik:GridClientSelectColumn>
                           
                                <telerik:GridBoundColumn UniqueName="CompanyName" ReadOnly="true" HeaderStyle-Width ="15%"  SortExpression="RMAUserCompany.CompanyName" HeaderText="CompanyName"
                                        DataField="RMAUserCompany.CompanyName" />
                            <telerik:GridTemplateColumn UniqueName="RMAnum" HeaderText="RMA #" SortExpression="RmaNumber" HeaderStyle-Width ="10%"  >
                                <ItemTemplate>
                               
                                    <asp:HiddenField ID="hdnrmaGUID" Value='<%# DataBinder.Eval(Container.DataItem, "rmaGUID")%>' runat="server" />                 
                                    <asp:HiddenField ID="hdnCompanyid" Value='<%# DataBinder.Eval(Container.DataItem, "RMAUserCompany.companyid")%>' runat="server" />                 
                                    <asp:Label ID="lblRMANo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "RmaNumber")%>' ></asp:Label>
                                </ItemTemplate>
                                
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn UniqueName="RmaDate" HeaderText="RmaDate" SortExpression="RmaDate" HeaderStyle-Width ="10%" >
                                <ItemTemplate>
<%# DataBinder.Eval(Container.DataItem, "RmaDate", "{0:MM/dd/yyyy}")%>
                                </ItemTemplate>
                                
                            </telerik:GridTemplateColumn>
<telerik:GridTemplateColumn UniqueName="ModifiedDate" HeaderText="Modified Date" SortExpression="ModifiedDate" HeaderStyle-Width ="10%" >
                                <ItemTemplate>
                                  <%--  <%# DataBinder.Eval(Container.DataItem, "ModifiedDate", "{0:MM/dd/yyyy}")%> --%>
                                   <%# DataBinder.Eval(Container.DataItem, "ModifiedDate") %>
                                </ItemTemplate>
                                
                            </telerik:GridTemplateColumn>
                            
                                           
                            <telerik:GridTemplateColumn UniqueName="Status" HeaderText="Status" HeaderStyle-Width ="10%" ItemStyle-Width="10%"  >
                                <ItemTemplate>
                                   <%# DataBinder.Eval(Container.DataItem, "Status") %>
                                </ItemTemplate>
                                
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn UniqueName="Comment" HeaderText="Customer Comments" HeaderStyle-Width ="25%" ItemStyle-Width="25%" >
                                <ItemTemplate>
                                   <%# DataBinder.Eval(Container.DataItem, "Comment")%>
                                </ItemTemplate>                                
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn UniqueName="AVComments" HeaderText="AV Comments" HeaderStyle-Width ="25%"  ItemStyle-Width="25%">
                                <ItemTemplate>
                                   <%# DataBinder.Eval(Container.DataItem, "AVComments")%>
                                </ItemTemplate>                                
                            </telerik:GridTemplateColumn>
				<telerik:GridTemplateColumn UniqueName="InvoiceDate2"  >
                                <ItemTemplate>
                                
                                <%--<td>
                                    <asp:ImageButton ID="ImageButton1"  ToolTip="View RMA" OnCommand="imgViewRMA_Click"  CausesValidation="false" CommandArgument='<%# Eval("rmaGUID") %>' ImageUrl="~/Images/view.png"  runat="server" />
                       
                                </td>--%>
                                
                                                 <asp:ImageButton ID="imgPO"  ToolTip="RMA History" OnCommand="imgRMAHistory_Click"  CausesValidation="false" CommandArgument='<%# Eval("rmaGUID") %>' ImageUrl="~/Images/history.png"  runat="server" />
                                
                                
                       
                        </ItemTemplate>	
</telerik:GridTemplateColumn>
                              <telerik:GridTemplateColumn UniqueName="InvoiceDate"  >
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkedit" CausesValidation="false" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "rmaGUID")%>' OnClientClick="selectcompany(this);" CommandName="ss" OnCommand="Edit_click" runat="server">Edit</asp:LinkButton>
                                </ItemTemplate>
                                
                            </telerik:GridTemplateColumn>
                            <telerik:GridEditCommandColumn UpdateText="Edit" Visible="false" UniqueName="EditCommandColumn" CancelText="Cancel"
                                EditText="Edit">
                                <HeaderStyle Width="55px"></HeaderStyle>
                            </telerik:GridEditCommandColumn>

                            <telerik:GridButtonColumn UniqueName="RmaDeleteColumn" Text="Delete" ConfirmText="Delete this RMA?" CommandName="Delete" />
                        </Columns>
                        <EditFormSettings CaptionFormatString="Edit details for RMA with ID {0}" CaptionDataField="rmaGUID">
                            <FormTableItemStyle Width="100%" Height="29px"></FormTableItemStyle>
                            <FormTableStyle GridLines="None" CellSpacing="0" CellPadding="2"></FormTableStyle>
                            <FormStyle Width="100%" BackColor="#eef2ea"></FormStyle>
                            <EditColumn ButtonType="ImageButton" />
                        </EditFormSettings>
                    </MasterTableView>
                </telerik:RadGrid>
                </td>
                </tr>
                </table>
                </td>
                    </tr>
                </table>
                </asp:Panel>
            </td>
        </tr></table>


<table>
        <tr>
            <td>
                    <cc1:ModalPopupExtender BackgroundCssClass="modal5Background"
            CancelControlID="btnClose2"  runat="server" PopupControlID="pnlRmap" 
            ID="mdlPopup2" TargetControlID="lnk2"
             />
            <asp:LinkButton ID="lnk2" runat="server" ></asp:LinkButton>
            <asp:Panel ID="pnlRmap" runat="server" CssClass="modal5Popup" >
                <div style="overflow:auto; height:450px; width:100%; border: 0px solid #839abf" >
      
                <table width="100%">
                <tr>
                    <td class="button">
                        RMA Report
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Button ID="btnClose2" runat="server" Text="Close" CssClass="button" />
                    </td>
                </tr>
                <tr>
                    <td >
                    <table bordercolor="#839abf" border="1" width="98%" align="center" cellpadding="0" cellspacing="0">
                    <tr><td>

                    <table border="0" width="100%" class="box" align="center" cellpadding="0" cellspacing="0">
                    <tr valign="top">
                        <td>
                        <asp:Panel ID="pnlRma2" runat="server">
                         
                            <asp:Repeater ID="rptRma" runat="server">
                            <HeaderTemplate>
                            <table width="100%" align="center">
                                <tr>
                                    <td class="button">
                                    &nbsp; Status
                                    </td>
                                    <td class="button">
                                        &nbsp;Last Modified Date
                                    </td>
                                    <td class="button">
                                        &nbsp;Modified By
                                    </td>
                                </tr>
                            
                            </HeaderTemplate>
                            <ItemTemplate>
                            <tr>
                                <td class="copy10grey">
                                    &nbsp;<%# Eval("Status") %>
                                </td>
                                <td class="copy10grey">
                                    &nbsp;<%# Eval("ModifiedDate")%>
                                </td>
                                <td class="copy10grey">
                                     &nbsp;<%# Eval("RmaContactName") %>
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
                    </td></tr>
                    </table>
                    </td>
                </tr>
                </table>
            
                </div>
            </asp:Panel>
            </td>
        </tr>
        </table>
        </td></tr>
        <tr><td>&nbsp;</td></tr>
        <tr>
            <td>
                <foot:MenuFooter ID="Foter" runat="server"></foot:MenuFooter>
            </td>
        </tr>
    </table>
    <script type="text/javascript">
        generateRMADetail();
        
        
        displayStatus(0);
        
        
    </script>

    </form>
</body>
</html>
