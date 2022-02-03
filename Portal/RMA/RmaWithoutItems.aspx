<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RmaWithoutItems.aspx.cs" Inherits="avii.RMA.RmaWithoutItems" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>
<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
--%>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="/Controls/Header.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Return Merchandise Authorization (RMA)  without items - Report</title>
     <link href="/aerostyle.css" type="text/css" rel="stylesheet" />
   
</head>
<body bgcolor="#ffffff" leftmargin="0" rightmargin="0" topmargin="0">
    <form id="form1" runat="server">
    <table cellspacing="0" cellpadding="0" border="0" align="center" width="100%">
        <tr>
            <td>
                <head:MenuHeader ID="MenuHeader1" runat="server"/>
            </td>
        </tr>
        </table>
      <table width="95%" align="center" cellspacing="0" cellpadding="0"> 
        <tr>
            <td>
                <table style="text-align: left; width:100%;" align="center" class="copy10grey">
                    
                    <tr>
                        <td class="buttonlabel" align="left">
                            &nbsp; Return Merchandise Authorization (RMA)  without items - Report
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

            <table width="100%">
                        <tr>
                            <td align="right"> 
            
                <asp:Label ID="lblCount" CssClass="copy10grey" runat="server" ></asp:Label>
                </td>
                </tr>


                <tr>
                    <td> 
                         <asp:GridView ID="gvRma"   EnableViewState="true"  
                          AutoGenerateColumns="false"  
                            DataKeyNames="RMAGUID"  Width="100%"  
                        ShowFooter="false" runat="server" GridLines="Both"  
                        PageSize="50" AllowPaging="false" 
                        BorderStyle="Outset"
                        AllowSorting="false" > 
                        <RowStyle BackColor="Gainsboro" />
                        <AlternatingRowStyle BackColor="white" />
                        <HeaderStyle  CssClass="buttongrid" ForeColor="white"/>
                        <FooterStyle CssClass="white"  />
                        <PagerStyle ForeColor="#636363" CssClass="copy10grey" HorizontalAlign="Left" />
                        <Columns>
                            <%--<asp:TemplateField ItemStyle-CssClass="copy10grey"  HeaderText="" ItemStyle-Width="2%" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chk"  runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>                 
                            --%>
                            <asp:TemplateField ItemStyle-CssClass="copy10grey"  HeaderText="S.No." ItemStyle-Width="2%" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <%--<%# Eval("RMAUserCompany.CompanyName")%>--%>
                                    <%# Container.DataItemIndex + 1 %>
                                   
                                </ItemTemplate>
                            </asp:TemplateField>                 
                            
                            <asp:TemplateField ItemStyle-CssClass="copy10grey"  HeaderText="Company Name" ItemStyle-Width="15%">
                                <ItemTemplate>
                                    <%# Eval("RMAUserCompany.CompanyName")%>

                                    <%--<asp:Label ID="lblCompany" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "RMAUserCompany.CompanyName")%>' ></asp:Label>--%>
                                </ItemTemplate>
                            </asp:TemplateField>                 
                            <asp:TemplateField ItemStyle-CssClass="copy10grey"  ItemStyle-Width="10%" HeaderText="RMA #">
                                <ItemTemplate>
                                    <%# Eval("RmaNumber")%>
                                    <%--<asp:Label ID="lblRMANo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "RmaNumber")%>' ></asp:Label>--%>
                                </ItemTemplate>
                            </asp:TemplateField>                 
                            <asp:TemplateField ItemStyle-CssClass="copy10grey"  ItemStyle-Width="10%" HeaderText="RMA Date">
                                <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "RmaDate", "{0:MM/dd/yyyy}")%>
                                    <%--<asp:Label ID="lblRmaDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "RmaDate", "{0:MM/dd/yyyy}")%>' ></asp:Label>--%>
                                    
                                </ItemTemplate>
                            </asp:TemplateField>                 
                            <asp:TemplateField ItemStyle-CssClass="copy10grey"  ItemStyle-Width="10%" HeaderText="Modified Date">
                                <ItemTemplate>
                                    <%# Eval("ModifiedDate")%>
                                </ItemTemplate>
                            </asp:TemplateField>                 
                            <asp:TemplateField ItemStyle-CssClass="copy10grey"  ItemStyle-Width="10%" HeaderText="Status">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "Status") %>
                                </ItemTemplate>
                            </asp:TemplateField>                 
                            <asp:TemplateField ItemStyle-CssClass="copy10grey"  ItemStyle-Width="25%" HeaderText="Customer Comments">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "Comment")%>
                                </ItemTemplate>
                            </asp:TemplateField>                 
                            <asp:TemplateField ItemStyle-CssClass="copy10grey"  ItemStyle-Width="25%" HeaderText="AV Comments">
                                <ItemTemplate>
                                    <%# Eval("AVComments")%>
                                </ItemTemplate>
                            </asp:TemplateField>                 
                            
                                

                      </Columns>
                      </asp:GridView>
                
                    </td>
                </tr>
                </table>
        </td>
        </tr>
        </table>
        
    </form>
</body>
</html>
