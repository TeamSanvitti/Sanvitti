<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewESNLog.aspx.cs" Inherits="Avii.Admin.ViewESNLog" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../lanstyle.css" rel="Stylesheet" type="text/css" />
</head>
<body bgcolor="#ffffff" >
    <form id="form1" runat="server">
        <table width="100%">
        <tr>
            <td>
                <asp:Label ID="lblMsg" runat="server" CssClass="errormessage" ></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                
                <asp:GridView ID="gvEsn" runat="server" AutoGenerateColumns="false" Width="100%"  
                ShowFooter="false"  GridLines="Both" 
                AllowPaging="true" PageSize="50" 
                OnPageIndexChanging="gvEsn_PageIndexChanging" > 
                <PagerStyle ForeColor="black" Font-Size="XX-Small"/>
                <RowStyle BackColor="Gainsboro" />
                <AlternatingRowStyle BackColor="white" />
                <HeaderStyle  CssClass="buttonlabel" ForeColor="white"/>
                <FooterStyle CssClass="white"  />
                <Columns>  
                                                      
                    <asp:TemplateField HeaderText="ESN" SortExpression="esn" HeaderStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="25%">
                        <ItemTemplate>
                            <%# Eval("ESN")%>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Update Date" SortExpression="UpdateDate" ItemStyle-CssClass="copy10grey" ItemStyle-Width="25%">
                        <ItemTemplate>
                            <%# Eval("UpdateDate")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                                 
                    <asp:TemplateField HeaderText="Status" SortExpression="status" ItemStyle-CssClass="copy10grey" ItemStyle-Width="25%">
                        <ItemTemplate>
                            <%# Eval("Status")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Module" SortExpression="Module" ItemStyle-CssClass="copy10grey" ItemStyle-Width="25%">
                        <ItemTemplate>
                            <%# Eval("Module")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                </asp:GridView>
            
            </td>
        </tr>
        </table>
    </form>
</body>
</html>
