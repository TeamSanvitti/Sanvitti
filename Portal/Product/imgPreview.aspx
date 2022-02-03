<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="imgPreview.aspx.cs" Inherits="avii.product.imgPreview" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Image Preview</title>
</head>
<body bgcolor="#ffffff" leftmargin="0" rightmargin="0" topmargin="0">
    <form id="form1" runat="server">
    <table width="100%" align="center">
    <tr>
        <td align="center">
            <asp:Image ID="imagePreview" runat="server" /> 
<asp:Label ID="lblMsg" runat="server" CssClass="errormessage" Text="No image uploaded yet"></asp:Label>   
        </td>
    </tr>
    </table>
    </form>
</body>
</html>
