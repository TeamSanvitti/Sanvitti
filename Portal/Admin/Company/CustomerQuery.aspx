<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomerQuery.aspx.cs" 
Inherits="avii.Admin.CustomerQuery" ValidateRequest="false"%>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>
<%@ Register TagPrefix="head1" TagName="MenuHeader1" Src="~/Controls/Header.ascx" %>
<%--<%@ Register TagPrefix="menu" TagName="Menu" Src="~/Admin/admHead.ascx" %>--%>
<%@ Register TagPrefix="menu" TagName="Menu" Src="~/dhtmlxmenu/menuControl.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Company Query</title>
    <link href="../../aerostyle.css" rel="stylesheet" type="text/css"/>
     <link href="../../product/ddcolortabs.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
     <asp:ScriptManager ID="ScriptManager1" runat="server">
     </asp:ScriptManager>
     <table cellSpacing="0" cellPadding="0"  border="0" align="center" width="100%">
				<tr>
					<td>
					<%--<head1:MenuHeader1 ID="menuheader" runat="server"></head1:MenuHeader1>--%>
                    <menu:Menu ID="menu1" runat="server" ></menu:menu>
					</td>
				</tr>
			</table><br />
			<table   width="80%" align="center">
                <tr>
			        <td  bgcolor="#dee7f6" class="button">&nbsp;&nbsp;
			        <asp:Label ID="lblHeader" runat="server" CssClass="button" BorderWidth="0" Text="Customer Query" ></asp:Label>
			        </td>
                </tr>
                
               <tr><td><asp:Label ID="lblMsg" runat="server" CssClass="errormessage"></asp:Label></td></tr>
            </table>
            
                        
            <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="80%" align="center" >
                <tr bordercolor="#839abf">
                                    <td>
                                        <table class="box" width="100%" align="center">
                                            
                                            <tr>
                                                <td class="copy10grey">
                                                    Company Name
                                                </td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="txtCompanyName" CssClass="copy10grey" Width="160px" />
                                                </td>
                                                <td class="copy10grey">
                                                    Company A/c #
                                                </td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="txtCompanyAC" CssClass="copy10grey" Width="160px" />
                                                    
                                                </td>
                                            </tr>
                                            <tr valign="top">
                                                <td class="copy10grey">
                                                    Contact Name
                                                </td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="txtContactName" CssClass="copy10grey" Width="160px" />
                                                </td>
                                                
                                                <td class="copy10grey">
                                                StoreID
                                                </td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="txtStoreID" CssClass="copy10grey" Width="160px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <%--<tr>
                                    <td>
                                        <hr />
                                     </td>
                                </tr>--%>
                                <tr>
                                    <td align="center"><br />
                                        <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_click"
                                            CssClass="buybt" />&nbsp;
                                        <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click1" Text="Cancel"
                                            CssClass="buybt" />
                                            <br />
                                    </td>
                                </tr>
                            </table>
			
            
             <asp:Panel ID="searchPanel" runat="server">
            <table bordercolor="#839abf" border="0" cellSpacing="0" cellPadding="0" width="80%" align="center" >
            <tr bordercolor="#839abf">
                <td >
                <table width="100%">
                <tr>
                    <td>
                        
                        <telerik:RadGrid ID="gvCompany" Skin="WebBlue" runat="server" Width="100%" GridLines="None" 
                            AutoGenerateColumns="False" PageSize="10" AllowSorting="True" AllowPaging="True"
                               OnDetailTableDataBind="RadGrid1_DetailTableDataBind"
                            OnNeedDataSource="RadGrid1_NeedDataSource" AllowMultiRowSelection="true" 
                            ShowStatusBar="false" OnDeleteCommand="RadGrid1_DeleteCommand"  OnItemDataBound="RadGrid1_ItemDataBound">
                            <PagerStyle Mode="NumericPages"></PagerStyle>
                            <ClientSettings>
                                 <Selecting AllowRowSelect="true" />
                                 <ClientEvents OnRowSelected="RowSelected" />
                              </ClientSettings>
                            <MasterTableView DataKeyNames="CompanyID" ClientDataKeyNames="CompanyID" EditMode="InPlace" 
                            AllowMultiColumnSorting="True" Width="100%" 
                                CommandItemDisplay="None" HierarchyLoadMode="ServerOnDemand" Name="Company">
                                <%--<CommandItemTemplate>
                                <div style="padding:0px 0px;">
                                    
                                    <asp:LinkButton ID="lnk_batchUpdate" runat="server"  CommandName="Selectedqq" OnClientClick="return displayStatus(1);" ><img style="border:0px;vertical-align:middle;" alt="" src="../images/edit.png" />Status Batch Update</asp:LinkButton>
                                    
                                </div>
                            </CommandItemTemplate>--%>
                                <DetailTables>
                                <telerik:GridTableView ShowHeader="true" CommandItemDisplay="None" EditMode="InPlace" 
                                BorderWidth="0"  HierarchyLoadMode="ServerOnDemand"
                                    DataKeyNames="CompanyAddressID"  ClientDataKeyNames="CompanyAddressID" 
                                    Width="100%" runat="server" Name="StoreLocation">
                                       <NoRecordsTemplate></NoRecordsTemplate>
                                          <ParentTableRelation>
                                            <telerik:GridRelationFields DetailKeyField="CompanyID" MasterKeyField="CompanyID" />    
                                           </ParentTableRelation>
                                           <Columns>
                                                <%--<telerik:GridBoundColumn UniqueName="CompanyName" Visible="false" ReadOnly="true"  HeaderText="CompanyName"/>
                                                <telerik:GridTemplateColumn UniqueName="RMAnum" Visible="false" HeaderText="RMA #">
                                                <ItemTemplate>
                                                
                                                </ItemTemplate>
                                                
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn UniqueName="RmaDate" Visible="false" HeaderText="RmaDate">
                                                <ItemTemplate>
                                                
                                                </ItemTemplate>
                                                
                                            </telerik:GridTemplateColumn>
                                            
                                            <telerik:GridTemplateColumn UniqueName="InvoiceNumber" Visible="false" HeaderText="InvoiceNumber">
                                                <ItemTemplate>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn UniqueName="InvoiceDate" Visible="false" HeaderText="InvoiceDate">
                                                <ItemTemplate>
                                                    
                                                </ItemTemplate>
                                                
                                            </telerik:GridTemplateColumn>--%>
                                            
                                                    <telerik:GridTemplateColumn UniqueName="StoreID" HeaderText="StoreID">
                                                    <ItemTemplate>
                                                    <%# DataBinder.Eval(Container.DataItem, "StoreID")%>
                                                        <%--<asp:Label ID="lblESN"  runat="server" Text='<%# "#" + DataBinder.Eval(Container.DataItem, "ESN")%>' ></asp:Label>--%>
                                                    </ItemTemplate>
                                                    
                                                    
                                                </telerik:GridTemplateColumn>
                                                
                                                    <telerik:GridTemplateColumn UniqueName="Addreess" HeaderText="Addreess">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAddreess" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "StoreAddress.Address1")%>' ></asp:Label>
                                                    </ItemTemplate>
                                                    
                                                </telerik:GridTemplateColumn>
                                                
                                                
                                                    <%--<telerik:GridTemplateColumn UniqueName="Maker" HeaderText="Brand">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMaker" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Maker")%>' ></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtMaker" ReadOnly="true" Text='<%# DataBinder.Eval(Container.DataItem, "Maker")%>' CssClass="copy10grey" runat="server"></asp:TextBox>
                                                    </EditItemTemplate>
                                                </telerik:GridTemplateColumn>--%>
                                                    <telerik:GridTemplateColumn UniqueName="city" HeaderText="City">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCity" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "StoreAddress.City")%>' ></asp:Label>
                                                    </ItemTemplate>
                                                    
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn UniqueName="State"  HeaderText="State">
                                                    <ItemTemplate>
                                                    
                                                        <asp:Label ID="lblState" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "StoreAddress.State")%>' ></asp:Label>
                                                    </ItemTemplate>
                                                    
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn UniqueName="zip" HeaderText="Zip">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblZip" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "StoreAddress.Zip") %>' ></asp:Label>
                                                    </ItemTemplate>
                                                    
                                                </telerik:GridTemplateColumn>
                                               
                                                <telerik:GridEditCommandColumn UpdateText="Edit" Visible="false" UniqueName="EditCommandColumn" CancelText="Cancel"
                                                    EditText="Edit">
                                                    <HeaderStyle Width="55px"></HeaderStyle>
                                                </telerik:GridEditCommandColumn>
                                                <telerik:GridButtonColumn UniqueName="EsnDeleteColumn" ConfirmText="Delete this Store?"  Text="Delete" CommandName="Delete" />
                                           </Columns>
                                       </telerik:GridTableView>
                                </DetailTables>
                                <Columns>
                                    
                                    <telerik:GridClientSelectColumn UniqueName="colSelect" >
                                            <HeaderStyle Width="10" />
                                        </telerik:GridClientSelectColumn>
                                   
                                        <telerik:GridBoundColumn UniqueName="CompanyName" ReadOnly="true" SortExpression="CompanyName" HeaderText="CompanyName"
                                                DataField="CompanyName" />
                                    <telerik:GridTemplateColumn UniqueName="BussinessType" HeaderText="BussinessType" SortExpression="BussinessType" >
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hdnrmaGUID" Value='<%# DataBinder.Eval(Container.DataItem, "companyID")%>' runat="server" />
                                            
                                            <asp:Label ID="lblBussinessType" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "BussinessType")%>' ></asp:Label>
                                        </ItemTemplate>
                                        
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn UniqueName="CompanyAccountNumber" HeaderText="CompanyAccountNumber" SortExpression="CompanyAccountNumber">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCompanyAcno" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CompanyAccountNumber")%>' ></asp:Label>
                                        </ItemTemplate>
                                        
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn UniqueName="CompanySType" HeaderText="CompanyType" SortExpression="CompanySType">
                                        <ItemTemplate>
                                            <asp:Label ID="lblInvoiceNumber"  runat="server" Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "CompanySType")) ==1 ? "Aerovoice" : "Company" %>' ></asp:Label>
                                        </ItemTemplate>
                                        
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn UniqueName="CompanyAccountStatus" HeaderText="Status" SortExpression="CompanyAccountStatus">
                                        <ItemTemplate>
                                            <asp:Label ID="lblInvoiceDate" runat="server" Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "CompanyAccountStatus")) == 2 ? "Approved" : "Pending"%>'></asp:Label>
                                        </ItemTemplate>
                                        
                                    </telerik:GridTemplateColumn>
                                                
                                    <telerik:GridTemplateColumn UniqueName="Email" HeaderText="Email" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblstatus" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Email") %>' ></asp:Label>
                                        </ItemTemplate>
                                        
                                    </telerik:GridTemplateColumn>
                                    
                                    <telerik:GridTemplateColumn UniqueName="Website" HeaderText="Website" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblWebsite" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Website") %>' ></asp:Label>
                                        </ItemTemplate>
                                        
                                    </telerik:GridTemplateColumn>
                                    
                                     <telerik:GridTemplateColumn UniqueName="edit"  >
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkedit" CausesValidation="false" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "companyID")%>' OnClientClick="selectcompany(this);" CommandName="ss" OnCommand="Edit_click" runat="server">Edit</asp:LinkButton>
                                        </ItemTemplate>
                                        
                                    </telerik:GridTemplateColumn>
                                    
                                    <telerik:GridEditCommandColumn UpdateText="Edit" Visible="false" UniqueName="EditCommandColumn" CancelText="Cancel"
                                        EditText="Edit">
                                        <HeaderStyle Width="55px"></HeaderStyle>
                                    </telerik:GridEditCommandColumn>
                                    <telerik:GridButtonColumn UniqueName="RmaDeleteColumn" Text="Delete" ConfirmText="Delete this company?" CommandName="Delete" />
                                </Columns>
                                <EditFormSettings CaptionFormatString="Edit details for Company with ID {0}" CaptionDataField="companyID">
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
            <tr><td>&nbsp;</td></tr>
            <tr><td>&nbsp;</td></tr>
             <tr>
            <td>
                <foot:MenuFooter ID="Footer" runat="server"></foot:MenuFooter>
            </td>
        </tr>
            </table>
            </asp:Panel>           
    </form>
</body>
</html>
             </MasterTableView>
                        </telerik:RadGrid>
                    </td>
                </tr>
                </table>
                </td>
            </tr>
            <tr><td>&nbsp;</td></tr>
            <tr><td>&nbsp;</td></tr>
             <tr>
            <td>
                <foot:MenuFooter ID="Footer" runat="server"></foot:MenuF