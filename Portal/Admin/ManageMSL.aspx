<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageMSL.aspx.cs" Inherits="avii.Admin.ManageMSL" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="/Controls/Footer.ascx" %>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="/Controls/Header.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Lan Global Inc. - Assign MSL</title>
    <LINK href="../aerostyle.css" type="text/css" rel="stylesheet">
	
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
    
    
    <table  cellSpacing="1" cellPadding="1" width="100%">
        <tr>
		    <td colSpan="6" bgcolor="#dee7f6" class="button">&nbsp;&nbsp;Assign MSL
		    </td>
        </tr>

    </table>   

    <asp:UpdatePanel ID="UpdatePanel1"  runat="server" UpdateMode="Conditional" >
    <ContentTemplate>
    
    <table width="100%">
        <tr><td><asp:Label ID="lblMsg" runat="server" CssClass="errormessage"></asp:Label></td></tr>
        <%--<tr>
            <td  class="copy10grey">- Enter partial or complete ESN# to get the result.
            <br />- Upload Excel file with list of ESN values to search multiple records.
            <br />- Check the repository checkbox to get the search result only from repository.
            <br />- Highlighted ESN's are Bad ESN.
            </td>
        </tr>--%>
    </table>
    <asp:Panel ID="pnlMissingMsl" runat="server">
        
    <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="95%" align="center" >
    <tr bordercolor="#839abf">
        <td>
        <table  cellSpacing="5" cellPadding="5" width="100%" border="0">
        <tr><td class="copy10grey"> Customer: &nbsp; 
        <asp:DropDownList ID="dpCompany" CssClass="copy10grey" runat="server"  >
									</asp:DropDownList> </td></tr>

        <tr><td>
        <hr />
        </td></tr>
        <tr>
            <td align="center">
                <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Get Missing MSL" OnClick="btnGetMissingMSL_Click"/>&nbsp;
                &nbsp;&nbsp;&nbsp;
                <%--<asp:Button ID="btnCancel" runat="server" CssClass="button" Text="Cancel" OnClick="btnCancel_Click"/>&nbsp;&nbsp;&nbsp;--%>
            </td>
        </tr>
        <%--<tr><td>&nbsp;</td></tr>--%>
    </table>
    </td>
    </tr>
        
    </table>  
    </asp:Panel>  
    <asp:Panel ID="pnlAssignMSL" runat="server">
        <table cellSpacing="3" cellPadding="3" width="100%" border="0">
        <tr>
            <td align="center">
                <asp:Button ID="btnAssignMSL" runat="server" CssClass="button" Text="Assign MSL Data" OnClick="btnAssignMSL_Click"/>&nbsp;
                &nbsp;&nbsp;
                <asp:Button ID="btnCancel" runat="server" CssClass="button" Text="Cancel" OnClick="btnCancel_Click"/>&nbsp;&nbsp;&nbsp;
            </td>
        </tr>
        <%--<tr>
            <td>
                <hr />
            </td>
        </tr>--%>
        <tr>
            <td>
                
                <asp:GridView ID="gvMSL"   OnPageIndexChanging="gvMSL_PageIndexChanging"  AutoGenerateColumns="false"  
                Width="100%" ShowHeader="true"  ShowFooter="false" runat="server" GridLines="Both" 
                PageSize="50" AllowPaging="true" AllowSorting="false"  
                >
                <RowStyle BackColor="Gainsboro" />
                <AlternatingRowStyle BackColor="white" />
                <HeaderStyle  CssClass="button" ForeColor="white"/>
                  <PagerStyle ForeColor="#636363" CssClass="copy10grey" HorizontalAlign="Left" />
                  <Columns>
                        <asp:TemplateField HeaderText="S.No." ItemStyle-CssClass="copy10grey"  ItemStyle-Width="1%">
                            <ItemTemplate>

                                  <%# Container.DataItemIndex + 1%>
                  
                            </ItemTemplate>
                        </asp:TemplateField>                 
               
                        <%--<asp:TemplateField HeaderText="Fulfillment#" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="12%">
                            <ItemTemplate>
                                <%# Eval("ESN")%>   
                            </ItemTemplate>
                        </asp:TemplateField>
                        --%>
                        
                        <asp:TemplateField HeaderText="Fulfillment#" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="8%">
                            <ItemTemplate>
                                <%# Eval("FulfillmentNumber")%>   
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Fulfillment Date" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                            <ItemTemplate>
                               <%#  Convert.ToDateTime(Eval("FulfillmentDate")).ToShortDateString() %>

                                <%--<%# Convert.ToDateTime(Eval("FulfillmentDate")).ToShortDateString() == "1/1/0001" ? "" : Convert.ToDateTime(Eval("FulfillmentDate")).ToShortDateString()%>    --%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Customer Name" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <%# Eval("CustomerName")%>   
                            </ItemTemplate>
                        </asp:TemplateField>
                
                        <asp:TemplateField HeaderText="ESN" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <%# Eval("ESN")%>   
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="MSL#" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="copy10grey" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <%# Eval("MSLNumber")%>   
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <%--<tr>
            <td>
                <hr />
            </td>
        </tr>
        --%>
        <tr>
            <td align="center">
                <asp:Button ID="btnAssignMSL2" runat="server" CssClass="button" Text="Assign MSL Data" OnClick="btnAssignMSL_Click"/>&nbsp;
                &nbsp;&nbsp;
                <asp:Button ID="btnCancel2" runat="server" CssClass="button" Text="Cancel" OnClick="btnCancel_Click"/>&nbsp;&nbsp;&nbsp;
            </td>
        </tr>
        </table>
    </asp:Panel>
    </ContentTemplate>
            
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server"
                     DynamicLayout="false">
            <ProgressTemplate>
                <img src="/Images/ajax-loaders.gif" /> Loading ...
            </ProgressTemplate>
        </asp:UpdateProgress>
        <br />
        <br />
        <br />
<br />
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
