<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PoSummary.aspx.cs" Inherits="avii.PoSummary" %>
<%@ Register TagPrefix="po" TagName="poSum" Src="/Controls/PoSum.ascx" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>
        Inventory Summary
    </title>
    <link href="./lanstyle.css" rel="stylesheet" type="text/css"/>
</head>
<body bgColor="#ffffff" leftmargin="0" rightmargin="0" topmargin="0">
    <form id="form1" runat="server">
    <div>
        <po:poSum id="sum1" runat="server"></po:poSum>
    </div>
    </form>
</body>
</html>
