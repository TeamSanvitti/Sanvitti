<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="avii.Registration" %>
<%@ Register TagPrefix="head" TagName="MenuHeader" Src="/Controls/Header.ascx" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="/Controls/Footer.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Account Request</title>
    <link href="./aerostyle.css" rel="stylesheet" type="text/css"/>
		<script type="text/javascript" language="javascript" src="./avI.js"></script> 
        <script type="text/javascript" language="javascript" src="/JQuery/jquery-latest.js"></script> 
		
<script type="text/javascript">
    
    function PassRange() {
        var txt_pwd = document.getElementById("<%=txtPassword.ClientID %>").value;
        if (txt_pwd.length < 8 || txt_pwd.length > 16) {
            alert("Password must be of minimum 8 and maximum 16 characters!");
            return false;
        }

    }
    function UserRange() {
        var txt_pwd = document.getElementById("<%=txtUserName.ClientID %>").value;

        if (txt_pwd.length < 5 || txt_pwd.length > 20) {
            alert("UserName must be of minimum 5 and maximum 20 characters!");
            return false;
        }
    }
    //("Password must be of minimum 8 characters!");
    function isAlfaNumberKey(evt) {
        // alert(evt.which);
        var charCodes = evt.keyCode ? evt.keyCode : evt.which ? evt.which : evt.charCode;
        if (charCodes < 48 || charCodes > 57 && charCodes < 65 || charCodes > 90 && charCodes < 97 || charCodes > 122) {
            if (charCodes != 8 && charCodes != 0) {
                evt.keyCode = 0;
                return false;
            }
        }
        return true;
    }
    function IsValid() {
        var username = document.getElementById("<%=txtUserName.ClientID %>").value;
        if (username == '') {
            alert('Username can not be empty');
            return false;
        }
    }
    function validatePhoneNumber(elementValue) {

        var phoneNumberPattern = /^\(?(\d{3})\)?[- ]?(\d{3})[- ]?(\d{4})$/;
        return phoneNumberPattern.test(elementValue);
    }
    function IsValidate() {
        var username = document.getElementById("<%=txtUserName.ClientID %>").value;
        if (username == '') {
            alert('Username can not be empty');
            return false;
        }
        var username = document.getElementById("<%=txtUserName.ClientID %>").value;
        if (username == '') {
            alert('Username can not be empty');
            return false;
        }
        var username = document.getElementById("<%=txtUserName.ClientID %>").value;
        if (username == '') {
            alert('Username can not be empty');
            return false;
        }

        var username = document.getElementById("<%=txtUserName.ClientID %>").value;
        if (username == '') {
            alert('Username can not be empty');
            return false;
        }
        var username = document.getElementById("<%=txtUserName.ClientID %>").value;
        if (username == '') {
            alert('Username can not be empty');
            return false;
        }
        var username = document.getElementById("<%=txtUserName.ClientID %>").value;
        if (username == '') {
            alert('Username can not be empty');
            return false;
        }


        var cellPhoneObj = document.getElementById("<%=txtPhone.ClientID %>").value;
        if (cellPhoneObj == '') {
            alert('Contact Phone can not be empty');
            return false;
        }
        if (!validatePhoneNumber(cellPhoneObj)) {
            alert('Invalid phone number');
            return false;
        }
    }
</script>
<script type="text/javascript">
    $(document).AjaxReady(function () {

        //    $('#btnVarify').click(function(evt) {
        $("#[id$=btnVarify]").click(function (evt) {
            if ($("#[id$=txtUserName]").val() != '') {
                //alert($("#[id$=txtUserName]").val())
                //alert($('#txtUserName').val());
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    data: "{ userName: '" + $("#[id$=txtUserName]").val() + "'}",
                    url: "Registration.aspx/FetchCustomer",
                    dataType: "json",
                    success: function (data) {
                        //alert(data.d);
                        // $("#[id$=lblMessage]").html(data.d);
                        if (data.d != '') {
                            $("#[id$=lblMessage]").html(data.d);

                            $("#[id$=lblMessage]").html('Username already exist');
                            $("#[id$=lblMessage]").removeClass("errorGreenMsg");
                            $("#[id$=lblMessage]").addClass("errormessage");
                        }
                        else {
                            $("#[id$=lblMessage]").html('Username available');
                            $("#[id$=lblMessage]").removeClass("errormessage");
                            $("#[id$=lblMessage]").addClass("errorGreenMsg");
                        }
                    }
                });
            }
            else
                alert('Username can not be empty');

            evt.preventDefault();
            //$.get("FetchCustomer.aspx",
            //{ userName: "" + $("#[id$=txtUserName]").val() + "" },
            // function(data) {
            //alert(data);
            //if(data != 'Username available')
            //{
            //    $("#[id$=lblMessage]").html(data);


            //    $("#[id$=lblMessage]").removeClass();
            //    $("#[id$=lblMessage]").addClass("copy14greentext");
            //}
            //else  
            //{
            //    $("#[id$=lblMessage]").html('Username available');
            ////    $("#[id$=lblMessage]").removeClass("copy14redtext");
            //    $("#[id$=lblMessage]").addClass("copy14greentext");
            //}
            // });
        });



        $("#[id$=btnSubmit]").click(function (event) {

            if ($("#[id$=hdnUserID]").val() == '') {
                if ($("#[id$=txtUserName]").val() != '') {
                    //alert($("#[id$=txtUserName]").val())
                    $.flag = 'test';
                    //alert($('#txtUserName').val());
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        data: "{ userName: '" + $("#[id$=txtUserName]").val() + "'}",
                        url: "Registration.aspx/FetchCustomer",
                        dataType: "json",
                        async: false,
                        success: function (data) {
                            //alert(data.d);
                            // $("#[id$=lblMessage]").html(data.d);
                            if (data.d != '') {
                                //$("#[id$=lblMsgs]").html(data.d);

                                $("#[id$=lblMsg]").html('Username already exist');

                                // alert('Username already exist');
                                //flag = true;
                                //event.preventDefault();

                                $.flag = "Username";
                                if (event.preventDefault) { event.preventDefault() }
                                else { event.stop() };

                                event.returnValue = false;
                                event.stopPropagation();

                            }
                            else
                                return true;

                        }
                    });
                    window.setTimeout(function () { $.flag = "Username"; }, 4000);
                }

                //alert($.flag);

                if ($.flag != 'test')
                    event.preventDefault();
            }

        });


    });

    
</script>
</head>
<body  bgColor="#ffffff" leftmargin="0" rightmargin="0" topmargin="0">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table cellspacing="0" cellpadding="0"  border="0" align="center" width="100%">
	<tr>
	    <td><head:menuheader id="MenuHeader" runat="server"></head:menuheader>
		</td>
	</tr>
	<tr valign="top">
	    <td height="450" align="center" >

        <asp:UpdatePanel ID="UpdatePanel1"  runat="server" >
        <Triggers>
            <asp:PostBackTrigger ControlID="btnCancel" />

        </Triggers>
        <ContentTemplate>
        
            <ajaxToolkit:ModalPopupExtender BackgroundCssClass="modal3Background" 
        CancelControlID="btnCancel" runat="server" PopupControlID="pnlModelPoupp" 
        ID="ModalPopupExtender1" TargetControlID="lnk"   
         />
         <asp:LinkButton ID="lnk" runat="server"></asp:LinkButton>
                
            <table width="100%" align="center">
        <tr>
            <td class="buttonlabel">
                Account Request
            </td>
        </tr>
                </table>
        <table width="95%" align="center">
        <tr>
            <td align="left">
                <asp:Label ID="lblMsgs" runat="server" CssClass="errormessage"></asp:Label>
                <asp:HiddenField ID="hdnUserID" runat="server" />
            </td>
        </tr>
        <tr>
        <td class="copy10grey" align="left">
            - Click on "Create New Account" to send request for new account.<br />
            - Click on "Edit" to change the existing request.<br />
            - Please contact Aervoice Administrator to get more infomration.
        </td>
        </tr>
      <tr>
      <td align="center">
      
      
          <asp:Panel ID="pnlCreateNew" runat="server">
          
            <table align="center" bordercolor="#839abf"   border="1" cellSpacing="0" cellPadding="0" width="100%" >           
                <tr>
                <td align="center"><br /><br />
           <%-- <asp:ImageButton ID="imgCreateNew"  ImageUrl="~/images/createnewaccountbt.png" runat="server"  />
           OnClick="btnCreateNew_Click"
           --%>
           
                <asp:Button ID="btnCreateNew" runat="server" OnClick="btnCreateNew_Click" Text="  New Account  " CssClass="button" Height="28" CausesValidation="false"  />   &nbsp;
<asp:Button ID="btnViewAll" runat="server" Text="View All Users" CssClass="button" OnClick="btnViewAll_Click"  Height="28" CausesValidation="false" />
          <br />    <br />
                </td>
                </tr>
                </table>
            </asp:Panel>                                                                                                                                                                                                                                                                                                   
      </td>
      </tr>
      </table> 
            
            <asp:Panel ID="pnlModelPoupp"  Width="80%" runat="server"  CssClass="modal3Popup"  DefaultButton="btnSubmit"  Style="display: none">
      
            <table align="left" border="0"  width="100%">
             <tr  height="24" class="buttonlabel">
                <td  align="left" valign="middle" class="buttonlabel">
                 <strong>User Details</strong>   
                </td>
            </tr>
            
            </table>
            <table cellSpacing="3" cellPadding="3" width="99%" align="center" >
            
            <tr>
                <td align="left">
                    <asp:Label ID="lblMsg" runat="server" CssClass="errormessage"></asp:Label>
                </td>
            </tr>
            <tr>
            <td class="copy10grey" align="left">
                - Username must be of minimum 5 characters and maximum 20 characters<br />
                - Password must be of minimum 8 characters and maximum 16 characters
            </td>
            </tr>
            </table>
            <br />
            
            <table align="center" bordercolor="#839abf"   border="1" cellSpacing="0" cellPadding="0" width="99%" >
                <tr>
                <td align="center">
                    <table cellSpacing="3" cellPadding="2" width="100%" align="center" >
                        <tr>
                            <td colspan="4">
                            &nbsp;
                            </td>
                        </tr>    
                        <tr>

                            <td align="left" class="copy10grey" width="17%">
                            <strong>Username:</strong>&nbsp;
                        </td>
                            <td align="left">
                            <asp:TextBox ID="txtUserName" Width="180" TabIndex="1" onchange="return UserRange();" onkeypress="return isAlfaNumberKey(event);" MaxLength="20" runat="server" CssClass="copy10grey" autocomplete="off"></asp:TextBox>
                            <asp:RequiredFieldValidator  
                            Text="." ForeColor="white" Display="None" SetFocusOnError="true"
                             ID="RequiredFieldValidator1" ValidationGroup="Retail" ControlToValidate="txtUserName" runat="server"  ErrorMessage="Enter user name"></asp:RequiredFieldValidator>
           
                        </td>
                            <td align="left">
                            <asp:Button ID="btnVarify" CssClass="button" runat="server" Text="Check Availability" />
                        <%--<asp:ImageButton ID="imgVarify" TabIndex="2" ImageUrl="~/images/availabilitybt.png" runat="server"  />
                        --%>
                       
                        </td>
                        <td align="left">
                             <asp:Label ID="lblMessage" runat="server" CssClass="errormessage" ></asp:Label>
                       
                        </td>
                        </tr>
                        <tr>
                            <td align="left" class="copy10grey" width="17%">
                          
                            <strong>   <asp:Label ID="lblPassword" runat="server" Text="Password:"></asp:Label></strong>&nbsp;
                            </td>
                            <td align="left">
                            <asp:TextBox ID="txtPassword" Width="180"  TextMode="Password" TabIndex="3" MaxLength="20" runat="server" autocomplete="off" CssClass="copy10grey"></asp:TextBox>
                            <asp:RequiredFieldValidator  
                            Text="." ForeColor="white" Display="None" SetFocusOnError="true"
                             ID="rfv2" ControlToValidate="txtPassword" ValidationGroup="Retail" runat="server"  ErrorMessage="Enter password"></asp:RequiredFieldValidator>
                             <asp:RegularExpressionValidator ID="RegExp1" runat="server" ValidationGroup="Retail" 
                            ErrorMessage="Password must be between 8 to 16 characters" Text="." ForeColor="white" Display="None" SetFocusOnError="true"
                            ControlToValidate="txtPassword"    
                            ValidationExpression="^[a-zA-Z0-9'@&#.\s]{8,16}$"></asp:RegularExpressionValidator>
                            </td>
                            <td align="left" class="copy10grey" >
                            
                            
                               <strong><asp:Label ID="lblConfirmPwd" runat="server" Text="Confirm Password:"></asp:Label></strong> &nbsp;
                            
                            </td>
                            <td align="left">
                            <asp:TextBox ID="txtConfirmPwd" Width="180" TextMode="Password" TabIndex="4" MaxLength="20" runat="server" CssClass="copy10grey"></asp:TextBox>
                            <asp:RequiredFieldValidator  
                            Text="." ForeColor="white" Display="None" SetFocusOnError="true"
                             ID="RequiredFieldValidator3" ValidationGroup="Retail" ControlToValidate="txtConfirmPwd" runat="server"  ErrorMessage="Enter confirm password"></asp:RequiredFieldValidator>
                            <asp:CompareValidator  ID="CompareValidator2" ControlToValidate = "txtConfirmPwd" ValidationGroup="Retail"
                            ControlToCompare = "txtPassword" Type = "String" Operator="Equal" ErrorMessage="Password must match!"
                            Text="." ForeColor="white" Display="None"  SetFocusOnError="true" Runat = "Server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                            &nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
                </tr>
                
	        </table>
	        <br />
	        <table align="center" bordercolor="#839abf"   border="1" cellSpacing="0" cellPadding="0" width="99%" >
                <tr>
                <td align="center">
                    <table cellSpacing="3"  border="0" cellPadding="3" width="100%" align="center" >
                        <tr>
                        <td>
                            &nbsp;
                        </td>
                        </tr>
                        <tr>
                            <td align="left" class="copy10grey" width="17%">
                             <strong>Customer Name:</strong>&nbsp;
                            </td>
                        <td align="left">
                            <asp:TextBox ID="txtCustomerName"  Width="180" TabIndex="5" MaxLength="50" runat="server" CssClass="copy10grey"></asp:TextBox>
                            <asp:RequiredFieldValidator  
                            Text="." ForeColor="white" Display="None" SetFocusOnError="true"
                             ID="RequiredFieldValidator4" ControlToValidate="txtCustomerName" runat="server" ValidationGroup="Retail"  ErrorMessage="Enter customer name"></asp:RequiredFieldValidator>
           
                        </td>
                        <td align="left" class="copy10grey" rowspan="4" valign="top">
                            
                           
                              <strong> <asp:Label CssClass="copy10grey" ID="lblStoreID" runat="server" Text="StoreID:"></asp:Label></strong>&nbsp;
                            </td>
                            <td rowspan="4" valign="top">
                                <asp:ListBox ID="lbStoreID" Width="180" SelectionMode="Multiple" CssClass="copy10grey" runat="server"></asp:ListBox>
                                <asp:RequiredFieldValidator  
                            Text="." ForeColor="white" Display="None" SetFocusOnError="true"
                             ID="RequiredFieldValidator8" ValidationGroup="Retail" ControlToValidate="lbStoreID" InitialValue="" runat="server"  ErrorMessage="Select a store"></asp:RequiredFieldValidator>
           
                            </td>
                        </tr>
                        <tr>
                        <td align="left" class="copy10grey" width="17%">
                           <strong> Email:</strong>&nbsp;

                        </td>
                        
                        
                        <td align="left">
                            <asp:TextBox ID="txtEmail"  Width="180"  TabIndex="6" MaxLength="50" runat="server" CssClass="copy10grey"></asp:TextBox>
                        <asp:RequiredFieldValidator  ValidationGroup="Retail"
                        Text="." ForeColor="white" Display="None" SetFocusOnError="true"
                             ID="RequiredFieldValidator5" ControlToValidate="txtEmail" runat="server"  ErrorMessage="Enter email address"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                        ErrorMessage="Invalid email ID!" ValidationGroup="Retail"
                         Text="." ForeColor="white" Display="None"  SetFocusOnError="true"
                        ControlToValidate="txtEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"  >
                        </asp:RegularExpressionValidator>
                            </td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                        <td align="left" class="copy10grey" >
                            
                           <strong>Confirm Email:</strong>&nbsp;
                            </td>
                            <td align="left">
                            <asp:TextBox ID="txtConfirmEmail"  Width="180"  TabIndex="7" MaxLength="50" runat="server" CssClass="copy10grey"></asp:TextBox>
                            <asp:RequiredFieldValidator   ValidationGroup="Retail"
                            Text="." ForeColor="white" Display="None" SetFocusOnError="true"
                             ID="RequiredFieldValidator6" ControlToValidate="txtConfirmEmail" runat="server"  ErrorMessage="Enter confirm email address"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                            ErrorMessage="Invalid email ID!" ValidationGroup="Retail"
                             Text="." ForeColor="white" Display="None"  SetFocusOnError="true"
                            ControlToValidate="txtConfirmEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"  >
                            </asp:RegularExpressionValidator>
                            <asp:CompareValidator  ID="CompareValidator1" ControlToValidate = "txtConfirmEmail" ValidationGroup="Retail"
                                    ControlToCompare = "txtEmail" Type = "String" Operator="Equal" ErrorMessage="Email must match!"
                                     Text="." ForeColor="white" Display="None"  SetFocusOnError="true" Runat = "Server" />
                            </td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr valign="top">
                            <td align="left" class="copy10grey">
                            <strong>Contact Phone#:</strong>&nbsp;
                            </td>
                            <td class="copy10grey" align="left">
                                <asp:TextBox ID="txtPhone"  Width="180"  TabIndex="8" MaxLength="12" runat="server" CssClass="copy10grey"></asp:TextBox>
                                <br />(123-456-1234)
                            <asp:RequiredFieldValidator  ValidationGroup="Retail"
                            Text="." ForeColor="white" Display="None" SetFocusOnError="true"
                             ID="RequiredFieldValidator7" ControlToValidate="txtPhone" runat="server"  ErrorMessage="Enter contact phone"></asp:RequiredFieldValidator>
                             <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" 
                            ErrorMessage="Invalid phone number!" ValidationGroup="Retail"
                             Text="." ForeColor="white" Display="None"  SetFocusOnError="true"
                            ControlToValidate="txtPhone" 
                            ValidationExpression="((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}"  ></asp:RegularExpressionValidator>
          
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                        <td>
                        &nbsp;
                        </td>
                        </tr>
                    </table>
                </td>
                </tr>
                
	        </table>
	        <br />
	        <table  border="0" cellSpacing="0" cellPadding="0" width="100%" >
	        <tr>
                <td align="center" valign="bottom">
                        <asp:Button ID="btnSubmit"  runat="server" Text="Submit" onclick="btnSubmit_click" CssClass="button" ValidationGroup="Retail"  />
                        &nbsp;<asp:Button ID="btnCancel" OnClick="btnCancel_click" runat="server" Text="Cancel" CssClass="button" />

                        <%--<asp:ImageButton ID="imgSubmit" onclick="btnSubmit_click" ValidationGroup="Retail"  ImageUrl="~/images/submitbt.png"  runat="server" />
                    &nbsp;
                    <asp:ImageButton ID="btnCancel" runat="server"  TabIndex="18"  
                              CausesValidation="false"  AlternateText="Cancel" 
                           OnClick="btnCancel_click"  ImageUrl="~/images/cancelbt.png"  />--%>
                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="True"
                        ShowSummary="False" ValidationGroup="Retail"></asp:ValidationSummary>
                
                            
                </td>
            </tr>
	        </table>
	        
	        </asp:Panel>
                
	        <br />
	        <table align="center" width="100%">
	        <tr>
	            <td align="center">
                <table width="90%">
                <tr>
                <td align="right">
                
                    
                </td>
                </tr>
                </table>
                    
            <asp:GridView ID="gvUsers" Width="95%" CellPadding="8" CellSpacing="1"
            runat="server" AutoGenerateColumns="false" AllowSorting="true"  OnSorting="GridView_Sorting">
            <RowStyle BackColor="Gainsboro" />
            <AlternatingRowStyle BackColor="white" />
            <HeaderStyle  CssClass="buttongrid"  ForeColor="white"/>
            <FooterStyle CssClass="white"  />
            <RowStyle CssClass="copy10grey" />
            <Columns>
    
                <asp:BoundField DataField="UserName"  SortExpression="UserName" HeaderStyle-CssClass="buttongrid"  HeaderStyle-HorizontalAlign="Left" HeaderText="User Name" ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="Email" SortExpression="Email" HeaderText="Email" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="buttongrid"  />
                <asp:BoundField DataField="CompanyName" SortExpression="CompanyName" HeaderText="Company Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="buttongrid"  />
                <asp:BoundField DataField="AccStatus" SortExpression="AccStatus" HeaderText="Status" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="buttongrid"  />
                
                <asp:TemplateField>
                    <ItemTemplate>
                        <table>
                        <tr>
                            <td>
                                <asp:ImageButton ToolTip="Edit User"  CausesValidation="false" CommandArgument='<%# Eval("userID") %>' ImageUrl="~/Images/edit.png" ID="imgEditUser" OnCommand="imgEdit_Commnad" runat="server" />
                        
                            </td>
                            <td>
                            <asp:ImageButton ToolTip="Delete User" OnClientClick="return confirm('Delete this User?');"  Visible='<%# Convert.ToString(Eval("AccStatus"))=="Pending" ? true : false %>'  CausesValidation="false" CommandArgument='<%# Eval("userID") %>' ImageUrl="~/Images/delete.png" ID="ImageButton1" OnCommand="imgDelete_Commnad" runat="server" />
                            </td>
                        </tr>
                        </table>
                        
                        
                    </ItemTemplate>
                </asp:TemplateField>
                </Columns>
            </asp:GridView>
            
	            </td>
	        </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
	        </table>

            
    <script type='text/javascript'>

        //document.getElementById("<%=txtPassword.ClientID %>").value = '';
        //Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);

        //function EndRequestHandler(sender, args) {
        //    document.getElementById("<%= btnCancel.ClientID %>").click();
            
        //}

        prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(EndRequest);
        function EndRequest(sender, args) {
            //alert("EndRequest");
            $(document).AjaxReady();
        }
    </script>
         </ContentTemplate>
        </asp:UpdatePanel>
        </td>
	</tr>
    <tr>
	    <td>
			<foot:MenuFooter id="Menuheader2" runat="server"></foot:MenuFooter>
        </td>
	</tr>
	</table>
    
    
    </form>
</body>
</html>
