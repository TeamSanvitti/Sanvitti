<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SessionTimeout.aspx.cs" Inherits="avii.SessionTimeout" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
       
<div id="ExpireConfirm_Submit">
<table>
    <tr>
        <td style="width: 230px;">
            Your Session will expire in <span id="seconds"></span>&nbsp;seconds.<br />Do you
            want to logout?
        </td>
    </tr>
</table>
</div>
    <script src="https://code.jquery.com/jquery-1.11.1.min.js"></script>
 
<script src="https://code.jquery.com/ui/1.11.1/jquery-ui.min.js"></script>

<link rel="stylesheet" href="https://code.jquery.com/ui/1.11.1/themes/smoothness/jquery-ui.css" />

    <script type="text/javascript">
        //var sessionTimeout = "<%= Session.Timeout %>";
        var sessionTimeout = 10;
        function DisplaySessionTimeout(timeout) {
            alert(timeout);
            var seconds = timeout / 1000;
            document.getElementsByName("seconds").innerHTML = seconds;
            setInterval(function () {
                seconds--;
                document.getElementById("seconds").innerHTML = seconds;
            }, 1000);
            setTimeout(function () {
                //Show Popup before 20 seconds of timeout.
                $("#ExpireConfirm_Submit").dialog({
                    height: 300,
                    width: 600,
                    resizable: false,
                    modal: true,
                    title: "Session Expire Confirmation",
                    open: function () {
                        $('p#id1').html(sessionTimeout);
                    },
                    buttons: {
                        "Yes": function () {
                            $(location).attr("href", "logon.aspx").submit();
                            $(this).dialog("close");
                        },
                        "No": function () {
                            ResetSession();
                            $(this).dialog("close");
                        }
                    }
                }).prev(".ui-dialog-titlebar").css("background", "red");
            }, timeout - 55 * 1000);
            setTimeout(function () {
                $(location).attr("href", "../logout.aspx").submit();
            }, timeout);
        };
        function ResetSession() {
            window.location = window.location.href;
        }    
</script>
        

    </form>
</body>
</html>
