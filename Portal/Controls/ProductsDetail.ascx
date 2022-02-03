<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProductsDetail.ascx.cs" Inherits="avii.Controls.ProductsDetail" %>
 <table bordercolor="#839abf" border="1" width="100%" align="center" cellpadding="0" cellspacing="0">
<tr><td>
<table border="0" width="100%" class="box" align="center" cellpadding="3" cellspacing="3">
<tr valign="top">
    <td class="copy10grey" align="left" width="13%">
       <b> Category:</b>
    </td>
    <td class="copy10grey" align="left" width="22%">
        <asp:Label ID="lblCategory" runat="server" CssClass="copy10grey"></asp:Label>
    </td>
    <td class="copy10grey" align="left" width="13%">
        <b>Model Number: </b>
    </td>
    <td class="copy10grey" align="left" width="18%">
        <asp:Label ID="lblModelNumber" runat="server" CssClass="copy10grey"></asp:Label>(<asp:Label ID="lblMaker" runat="server" CssClass="copy10grey"></asp:Label>)
        
    </td>
    <td class="copy10grey" align="left" width="34%" rowspan="7">
         <table bordercolor="#839abf" border="1" width="100%" align="center" cellpadding="0" cellspacing="0">
        <tr><td>
        <table border="0" width="100%" class="box" align="center" cellpadding="5" cellspacing="5">
        <tr valign="top">
            <td class="copy10grey" align="left" width="18%" >
                <b>Product condition:</b>
            </td>
            <td class="copy10grey" align="left" width="10%" >
                <asp:Label ID="lblCondition" runat="server" CssClass="copy10grey"></asp:Label>
            </td>
            </tr>
            <tr valign="top">
                <td class="copy10grey" align="left" width="22%">
                <b>Allow LTE:</b>
                </td>
                <td class="copy10grey" align="left" width="12%">
                    
                                
                                <asp:CheckBox ID="chkAllowLTE" Enabled="false" runat="server" CssClass="copy10grey" />
                    <%--<asp:Label ID="lblAllowLTE" runat="server" CssClass="copy10grey"></asp:Label>--%>
                </td>
            </tr>

            <tr valign="top">
            <td class="copy10grey" align="left" width="22%">
           <b> Active:</b>
            </td>
            <td class="copy10grey" align="left" width="12%">
                <%--<asp:Label ID="lblActive" runat="server" CssClass="copy10grey"></asp:Label>--%>
                <asp:CheckBox ID="chkActive"  Enabled="false" runat="server" CssClass="copy10grey" />
            </td>
            </tr>
            <tr valign="top">
            <td class="copy10grey" align="left" width="22%">
               <b> Allow RMA:</b>
            </td>
            <td class="copy10grey" align="left" width="12%">
                <%--<asp:Label ID="lblAllowRMA" runat="server" CssClass="copy10grey"></asp:Label>--%>
                <asp:CheckBox ID="chkAllowRMA" Enabled="false" runat="server" CssClass="copy10grey" />
            </td>
            </tr>
            <tr valign="top">
            <td class="copy10grey" align="left" width="22%">
               <b> Catalog:</b>
            </td>
            <td class="copy10grey" align="left" width="12%">
                <%--<asp:Label ID="lblShowUnderCatalog" runat="server" CssClass="copy10grey"></asp:Label>--%>
                <asp:CheckBox ID="chkShowunderCatalog" Enabled="false"  runat="server" CssClass="copy10grey" />
                                
            </td>
            </tr>
            
        
        
        </table>
        </td>
        </tr>
        </table>
    </td>
    <%--<td class="copy10grey" align="left" width="15%">
        
    </td>--%>
</tr>
<tr valign="top">
    <td class="copy10grey" align="left" width="13%">
    <b>Product Code:</b>
    </td>
    <td class="copy10grey" align="left" width="22%">
        <asp:Label ID="lblProductCode" runat="server" CssClass="copy10grey"></asp:Label>
    </td>
    <td class="copy10grey" align="left" width="15%">
    <b>UPC:</b>
    </td>
    <td class="copy10grey" align="left" width="22%">
        <asp:Label ID="lblUPC" runat="server" CssClass="copy10grey"></asp:Label>
    </td>
    <%--<td class="copy10grey" align="left" width="13%">
    
    </td>
    <td class="copy10grey" align="left" width="15%">
        
    </td>--%>
</tr>
<tr valign="top">
    <td class="copy10grey" align="left" width="13%">
   <b> Product Name:</b>
    </td>
    <td class="copy10grey" align="left" width="22%" colspan="3">
        <asp:Label ID="lblName" runat="server" CssClass="copy10grey"></asp:Label>
    </td>
    <%--<td class="copy10grey" align="left" width="13%">
    
    </td>
    <td class="copy10grey" align="left" width="15%">
        
    </td>--%>
</tr>

<tr valign="top">
    <td class="copy10grey" align="left" width="13%">
  <b>  Carrier:</b>
    </td>
    <td class="copy10grey" align="left" width="22%">
        <asp:Label ID="lblCarrier" runat="server" CssClass="copy10grey"></asp:Label>
    </td>
    <td class="copy10grey" align="left" width="15%">
   <b> Color: </b>
    </td>
    <td class="copy10grey" align="left" width="22%">
        <asp:Label ID="lblColor" runat="server" CssClass="copy10grey"></asp:Label>
        
    </td>
    <%--<td class="copy10grey" align="left" width="13%">
    
    </td>
    <td class="copy10grey" align="left" width="15%">
        
    </td>--%>
</tr>
<tr valign="top">
    <td class="copy10grey" align="left" width="13%">
    <b>Document:</b>
    </td>
    <td align="left" colspan="3" >
      <a target="_blank">  <asp:Label ID="lblDoc" runat="server" CssClass="copy10grey"></asp:Label></a>
    </td>
    </tr>

<tr valign="top">
    <td class="copy10grey" align="left" width="13%">
    <b>Short Desc:</b>
    </td>
    <td align="left" colspan="3" >
        <asp:Label ID="lblShortDesc" runat="server" CssClass="copy10grey"></asp:Label>
    </td>
    </tr>
    
<tr valign="top">
    <td class="copy10grey" align="left" width="13%">
    <b>Full Desc:</b>
    </td>
    <td align="left" colspan="3" >
    <asp:Label ID="lblFullDesc" runat="server" CssClass="copy10grey"></asp:Label>
    </td>
    </tr>



</table>
</td>
</tr>
</table>