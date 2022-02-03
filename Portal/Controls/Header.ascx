<%@ Control Language="c#" AutoEventWireup="false" Codebehind="Header.ascx.cs" Inherits="avii.Controls.Header" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="~/dhtmlxmenu/menuControl.ascx" %>

<head:MenuHeader ID="header1" runat="server" />
<table width="100%" border="0" cellspacing="0" cellpadding="0">
  <tr valign="top" >
    <td style="text-align: right !important;float:right;">
    <table border="0" style="text-align: right !important;float:right;">
      <tr valign="top" >
       
        <td  valign="top" height="80">
        <table border="0" cellpadding="0" cellspacing="0"  width="100%">
            <tr>
              <td valign="top" width="100%">
      
                    

              </td>
                    </tr>
                    
    
                </table></td>
              </tr>
                <tr>
              <td valign="top" width="100%">
<table style="width: 100%;" align="right" border="0">
<tr >
            <td style="text-align: right !important; float:right; vertical-align:bottom">

                &nbsp;<asp:Label ID="lblName" CssClass="loginTitle" runat="server" ></asp:Label>&nbsp;&nbsp;
                
            </td>
        </tr>
    </table>
                </td>
              </tr>
            </table>
            
            
            </td>
          </tr>
        </table>

  
<div id="myModal" class="modal fade inectivemod" role="dialog" style="opacity: 1;background:#0000005e;">
    <br />
    <div class="modal-dialog">
        <!-- Modal content-->
        <div class="modal-content">

            <div class="modal-body text-center">
                <h4>Session Expire Confirmation</h4>
                <p>
                   <strong> <span id="spnName" runat="server" ></span></strong> Are you still there?
                    <br />
                    <strong><span id="CountDownHolder" style="color:red;"></span></strong>
                    <br />
                        Click Yes to continue your session.
                </p>

                <div class="text-center button-block" style="text-align:center; margin-top:22px;">
                    <button type="button" class="btn btn-default btn-sm" onclick="SessionTimeout.sendKeepAlive();">Yes</button>
                  <button type="button" class="btn btn-default btn-sm" onclick="SessionTimeout.hidePopup();">No</button>
                </div>
            </div>
            <div class="modal-footer">
                <!--         <button type="button" class="btn btn-default" data-dismiss="modal">Close</button> -->
            </div>
        </div>

    </div>
</div>


<script type="text/javascript">

    var redirectTimeout2 = 1080;// <%= FormsAuthentication.Timeout.TotalSeconds %>;

    //var sessionTimeout = "<%= Session.Timeout %>";
    //alert(sessionTimeout);
    redirectTimeout2 = 1000 * redirectTimeout2 - 130;
    var extendMethodUrl = '';

    $(document).ready(function () {
        debugger;
        SessionTimeout2.schedulePopup2();
    });

    window.SessionTimeout2 = (function () {
        var _timeLeft, _popupTimer, _countDownTimer;
        var stopTimer = function () {
            window.clearTimeout(_popupTimer);
            window.clearTimeout(_countDownTimer);
        };
        var updateCountDown2 = function () {
            var min = Math.floor(_timeLeft / 60);
            var sec = _timeLeft % 60;
            if (sec < 10)
                sec = "0" + sec;

            //document.getElementById("CountDownHolder").innerHTML = min + ":" + sec;

            if (_timeLeft > 0) {
                _timeLeft--;
                _countDownTimer = window.setTimeout(updateCountDown2, 1000);
            } else {
                $("#myModal").hide();
                //alert('Stay here 1');
                $.ajax({
                    type: "post",
                    url: "../logon.aspx/UpdateSession",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function successFunc(response) {
                        //debugger;
                        //alert('success');
                        SessionTimeout2.schedulePopup2();
                    },
                    error: function () {
                        //alert('erro');
                    }
                });
                //sendKeepAlive2: sendKeepAlive2
                
                //alert('Stay here');
                //document.location = logoutUrl;
            }
        };
        var showPopup2 = function () {
            //$("#myModal").show();
            _timeLeft = 60;
            updateCountDown2();
        };
        var schedulePopup2 = function () {
            //$("#myModal").hide();
            stopTimer();
            _popupTimer = window.setTimeout(showPopup2, redirectTimeout2);
        };
        var hidePopup2 = function () {
            //alert('Stay here 2');
           // $("#myModal").hide();
            //alert('Stay here');
            //window.location.href = "../logout.aspx";
        };
        var sendKeepAlive2 = function () {
           // alert('Live here');
            // window.location.href = "../logon.aspx";
            //debugger;
            stopTimer();
           // $("#myModal").hide();
            $.ajax({
                type: "post",
                url: "../logon.aspx/UpdateSession",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function successFunc(response) {
                    //debugger;
                    //alert('success');
                    SessionTimeout2.schedulePopup2();
                },
                error: function () {
                    //alert('erro');
                }
            });
        };
        return {
            schedulePopup2: schedulePopup2,
            sendKeepAlive2: sendKeepAlive2,
            hidePopup2: hidePopup2,
            stopTimer: stopTimer,
        };

    })();

</script>
      
<script type="text/javascript">

    var redirectTimeout = 1080;// <%= FormsAuthentication.Timeout.TotalSeconds %>;

    redirectTimeout = 1000 * redirectTimeout - 130;
    var logoutUrl = "../logout.aspx";

    var loginUrl = '../logon.aspx';
    var extendMethodUrl = '';
   
    $(document).ready(function () {
        debugger;
        SessionTimeout.schedulePopup();
    });

    window.SessionTimeout = (function () {
        var _timeLeft, _popupTimer, _countDownTimer;
        var stopTimers = function () {
            window.clearTimeout(_popupTimer);
            window.clearTimeout(_countDownTimer);
        };
        var updateCountDown = function () {
            var min = Math.floor(_timeLeft / 60);
            var sec = _timeLeft % 60;
            if (sec < 10)
                sec = "0" + sec;

            document.getElementById("CountDownHolder").innerHTML = min + ":" + sec;

            if (_timeLeft > 0) {
                _timeLeft--;
                _countDownTimer = window.setTimeout(updateCountDown, 1000);
            } else {
                $("#myModal").hide();
                $.ajax({
                    type: "post",
                    url: "../logon.aspx/ExtendSession",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function successFunc(response) {
                        //debugger;
                        //alert('success');
                        SessionTimeout.schedulePopup();
                    },
                    error: function () {
                        //alert('erro');
                    }
                });
                //alert('Stay here');
                //document.location = logoutUrl;
            }
        };
        var showPopup = function () {
            $("#myModal").show();
            _timeLeft = 60;
            updateCountDown();
        };
        var schedulePopup = function () {
            $("#myModal").hide();
            stopTimers();
            _popupTimer = window.setTimeout(showPopup, redirectTimeout);
        };
        var hidePopup = function () {
            $("#myModal").hide();
            //alert('Stay here');
            window.location.href = "../logout.aspx";
        };
        var sendKeepAlive = function () {
           // window.location.href = "../logon.aspx";
            //debugger;
            stopTimers();
            $("#myModal").hide();
            $.ajax({
                type: "post",
                url: "../logon.aspx/ExtendSession",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function successFunc(response) {
                    //debugger;
                    //alert('success');
                    SessionTimeout.schedulePopup();
                },
                error: function () {
                    //alert('erro');
                }
            });
        };
        return {
            schedulePopup: schedulePopup,
            sendKeepAlive: sendKeepAlive,
            hidePopup: hidePopup,
            stopTimers: stopTimers,
        };

    })();

</script>


