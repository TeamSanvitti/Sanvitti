<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LinkToMobilair.aspx.cs" Inherits="avii.LinkToMobilair" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="../JQuery/jquery-latest.js"></script>
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            $('#btnNewWindow').click();

        });
        function openNewWin() 
        {
            
alert('You will now be re-directed to the Lan Global accessory group, Mobilair. \r\n Thank you')

window.open("http://www.mobilair.com", "_blank", "toolbar=yes, location=yes, directories=no, status=no, menubar=yes, scrollbars=yes, resizable=no, copyhistory=yes, width=1000, height=600,left=0,top=0");
//            window.location.href = "http://www.mobilair.com";

            window.location.href = "https://aerovoice.com";
            return false;
            //window.open("http://mobilairtech.com", "_blank", "toolbar=yes, location=yes, directories=no, status=no, menubar=yes, scrollbars=yes, resizable=no, copyhistory=yes, width=1000, height=600,left=0,top=0");
            
        }
        </script>

</head>
<body>
    <form id="form1" runat="server">
    <div style="display:none">
        <asp:Button ID="btnNewWindow" runat="server" OnClick="Button1_Click" Text="Button" OnClientClick=" return openNewWin()" />
    </div>

    </form>
</body>
</html>
