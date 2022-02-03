<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="customerForm.aspx.cs" Inherits="avii.Admin.customerForm" ValidateRequest="false" %>
<%@ Register TagPrefix="foot" TagName="MenuFooter" Src="~/Controls/Footer.ascx" %>
<%@ Register TagPrefix="head1" TagName="MenuHeader1" Src="~/Controls/Header.ascx" %>
<%@ Register TagPrefix="menu" TagName="Menu" Src="~/Admin/admHead.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Customer Form</title>
    <link href="/aerostyle.css" rel="stylesheet" type="text/css"/>
    <LINK href="/Styles.css" type="text/css" rel="stylesheet">
    <script type="text/javascript">
        function displayMessage(msg) {
            var lblMsg = document.getElementById('lblMsg');
            lblMsg.innerHTML = msg;
            //var btnSubmit = document.getElementById('btnSubmit');
            //btnSubmit.disabled = false;
        }
        function echeck(obj) {
            //var btnSubmit = document.getElementById('btnSubmit');
            //btnSubmit.disabled = true;
            var str = obj.value;
            var at = "@"
            var dot = "."
            var lat = str.indexOf(at)
            var lstr = str.length
            var ldot = str.indexOf(dot)
            if (str.indexOf(at) == -1) {
                alert("Invalid E-mail ID")
                return false
            }

            if (str.indexOf(at) == -1 || str.indexOf(at) == 0 || str.indexOf(at) == lstr) {
                alert("Invalid E-mail ID")
                return false
            }

            if (str.indexOf(dot) == -1 || str.indexOf(dot) == 0 || str.indexOf(dot) == lstr) {
                alert("Invalid E-mail ID")
                return false
            }

            if (str.indexOf(at, (lat + 1)) != -1) {
                alert("Invalid E-mail ID")
                return false
            }

            if (str.substring(lat - 1, lat) == dot || str.substring(lat + 1, lat + 2) == dot) {
                alert("Invalid E-mail ID")
                return false
            }

            if (str.indexOf(dot, (lat + 2)) == -1) {
                alert("Invalid E-mail ID")
                return false
            }

            if (str.indexOf(" ") != -1) {
                alert("Invalid E-mail ID")
                return false
            }

            return true
        }
        function emailcheck(obj) {
            
            var str = obj.value;
            var at = "@"
            var dot = "."
            var lat = str.indexOf(at)
            var lstr = str.length
            var ldot = str.indexOf(dot)
            if (str.indexOf(at) == -1) {
                alert("Invalid E-mail ID")
                return false
            }

            if (str.indexOf(at) == -1 || str.indexOf(at) == 0 || str.indexOf(at) == lstr) {
                alert("Invalid E-mail ID")
                return false
            }

            if (str.indexOf(dot) == -1 || str.indexOf(dot) == 0 || str.indexOf(dot) == lstr) {
                alert("Invalid E-mail ID")
                return false
            }

            if (str.indexOf(at, (lat + 1)) != -1) {
                alert("Invalid E-mail ID")
                return false
            }

            if (str.substring(lat - 1, lat) == dot || str.substring(lat + 1, lat + 2) == dot) {
                alert("Invalid E-mail ID")
                return false
            }

            if (str.indexOf(dot, (lat + 2)) == -1) {
                alert("Invalid E-mail ID")
                return false
            }

            if (str.indexOf(" ") != -1) {
                alert("Invalid E-mail ID")
                return false
            }

            return true
        }

        function ValidateForm() {
            var emailID = document.getElementById("txtEmail");
            var CompanyName = document.getElementById("txtCmpName");
            var Zip=document.getElementById("txtZip");
            var AccNum = document.getElementById("txtAccNum");
            var Add1 = document.getElementById("txtAdd1");
            var City=document.getElementById("txtCity");
            
            if ((AccNum.value == null) || (AccNum.value == "")) {
                alert("Please Enter Company A/C Number")
                AccNum.focus()
                return false
            }
            if ((CompanyName.value == null) || (CompanyName.value == "")) {
                alert("Please Enter Company Name")
                CompanyName.focus()
                return false
            }
            if ((Add1.value == null) || (Add1.value == "")) {
                alert("Please Enter your Address1")
                Add1.focus()
                return false
            }
            if ((City.value == null) || (City.value == "")) {
                alert("Please Enter your City")
                City.focus()
                return false
            }
            if ((Zip.value == null) || (Zip.value == "")) {
                alert("Please Enter your Zip Code")
                Zip.focus()
                return false
            }
            if ((emailID.value == null) || (emailID.value == "")) {
                alert("Please Enter your Email ID")
                emailID.focus()
                return false
            }
            

            if (!emailcheck(emailID))
                return false;
            if (!validateZIP(Zip.value))
                return false;

            return true;
                
                
           
            
    }
    var digits = "0123456789";
    // non-digit characters which are allowed in phone numbers
    var phoneNumberDelimiters = "()- ";
    // characters which are allowed in international phone numbers
    // (a leading + is OK)
    var validWorldPhoneChars = phoneNumberDelimiters + "+";
    // Minimum no of digits in an international phone no.
    var minDigitsInIPhoneNumber = 10;

    function isInteger(s) {
        var i;
        for (i = 0; i < s.length; i++) {
            // Check that current character is number.
            var c = s.charAt(i);
            if (((c < "0") || (c > "9"))) return false;
        }
        // All characters are numbers.
        return true;
    }
    function trim(s) {
        var i;
        var returnString = "";
        // Search through string's characters one by one.
        // If character is not a whitespace, append to returnString.
        for (i = 0; i < s.length; i++) {
            // Check that current character isn't whitespace.
            var c = s.charAt(i);
            if (c != " ") returnString += c;
        }
        return returnString;
    }
    function stripCharsInBag(s, bag) {
        var i;
        var returnString = "";
        // Search through string's characters one by one.
        // If character is not in bag, append to returnString.
        for (i = 0; i < s.length; i++) {
            // Check that current character isn't whitespace.
            var c = s.charAt(i);
            if (bag.indexOf(c) == -1) returnString += c;
        }
        return returnString;
    }

    function checkInternationalPhone(strPhone) {
        var bracket = 3
        strPhone = trim(strPhone)
        if (strPhone.indexOf("+") > 1) return false
        if (strPhone.indexOf("-") != -1) bracket = bracket + 1
        if (strPhone.indexOf("(") != -1 && strPhone.indexOf("(") > bracket) return false
        var brchr = strPhone.indexOf("(")
        if (strPhone.indexOf("(") != -1 && strPhone.charAt(brchr + 2) != ")") return false
        if (strPhone.indexOf("(") == -1 && strPhone.indexOf(")") != -1) return false
        s = stripCharsInBag(strPhone, validWorldPhoneChars);
        return (isInteger(s) && s.length >= minDigitsInIPhoneNumber);
    }
    function validateZIP(field) {
       
        var valid = "0123456789-";
        var hyphencount = 0;

        if (field.length != 5 && field.length != 10) {
            alert("Please enter your 5 digit or 5 digit+4 zip code.");
            return false;
        }
        for (var i = 0; i < field.length; i++) {
            temp = "" + field.substring(i, i + 1);
            if (temp == "-") hyphencount++;
            if (valid.indexOf(temp) == "-1") {
                alert("Invalid characters in your zip code.  Please try again.");
                return false;
            }
            if ((hyphencount > 1) || ((field.length == 10) && "" + field.charAt(5) != "-")) {
                alert("The hyphen character should be used with a properly formatted 5 digit+four zip code, like '12345-6789'.   Please try again.");
                return false;
            }
        }
        return true;
    }
    
    </script>
</head>
<body bgColor="#ffffff" leftmargin="0" rightmargin="0" topmargin="0"  onkeydown="if (event.keyCode==13) {event.keyCode=9; return event.keyCode }">
    <form id="form1" runat="server">
            <TABLE cellSpacing="0" cellPadding="0"  border="0" align="center" width="100%">
				<TR>
					<TD>
					<%--<head1:MenuHeader1 ID="menuheader" runat="server"></head1:MenuHeader1>--%>
                    <menu:menu ID="menu" runat="server" ></menu:menu>
					</TD>
				</TR>
			</TABLE>
			
			    <br />
            <table   width="80%" align="center">
                <tr>
			        <td colSpan="6" bgcolor="#dee7f6" class="button">&nbsp;&nbsp;
			        <asp:Label ID="lblHeader" runat="server" CssClass="button" BorderWidth="0" Text="Add Customer" ></asp:Label>
			        </td>
                </tr>
                
               <tr><td><asp:Label ID="lblMsg" runat="server" CssClass="errormessage"></asp:Label></td></tr>
            </table>
    
            <table bordercolor="#839abf" border="1" cellSpacing="0" cellPadding="0" width="80%" align="center" >
                <tr bordercolor="#839abf">
                    <td >
                        <asp:ScriptManager ID="ScriptManager1" runat="server">
                        </asp:ScriptManager>
                        <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>--%>
                        <table cellSpacing="1" cellPadding="1" width="100%">
                        
                             <tr valign="top">
                                <td align="right" class="copy10grey" width="25%" >
                                    Company Account #:<span class="errormessage">*</span></td>
                                <td width="25%">
                                     &nbsp;<asp:TextBox ID="txtAccNum"   MaxLength="50" runat="server" 
                                        CssClass="copy10grey" Width="80%" 
                                        ></asp:TextBox>
                               
                                   </td>
                            
                                <td align="right" class="copy10grey" width="25%">
                                    Company Name:<span class="errormessage">*</span></td>
                                <td width="25%">
                                     &nbsp;<asp:TextBox ID="txtCmpName" runat="server" CssClass="copy10grey" 
                                        MaxLength="50" Width="80%" 
                                        ></asp:TextBox>
                                </td></tr>
                            <tr valign="top">
                                    <td align="right" class="copy10grey">Address 1:<span class="errormessage">*</span></td>
                                <td>
                                    &nbsp;<asp:TextBox ID="txtAdd1" runat="server" CssClass="copy10grey" MaxLength="500" Width="80%"></asp:TextBox>
                                                                   
                                </td>               
                           
                                <td align="right" class="copy10grey">
                                    Address 2:</td>
                                <td>
                                 &nbsp;<asp:TextBox ID="txtAdd2" runat="server" CssClass="copy10grey" MaxLength="500" Width="80%"></asp:TextBox>
                                </td> </tr>
                            <tr valign="top">
                               
                                <td align="right" class="copy10grey">
                                    Address 3:</td>
                                <td>
                                    &nbsp;<asp:TextBox ID="txtAdd3" runat="server" CssClass="copy10grey" MaxLength="500" Width="80%"></asp:TextBox>
                                </td>
                                
                           
                                <td align="right" class="copy10grey">
                                    City:<span class="errormessage">*</span></td>
                                <td>
                                     &nbsp;<asp:TextBox ID="txtCity" 
                                        runat="server"  CssClass="copy10grey" MaxLength="100" Width="80%"></asp:TextBox>
                                </td> </tr>
                            <tr valign="top">
                                <td align="right" class="copy10grey">
                                State:</td>
                                <td>
                                     &nbsp;<asp:TextBox ID="txtState" runat="server" CssClass="copy10grey" MaxLength="100" Width="80%"></asp:TextBox>
                                </td>
                           
                                <td align="right" class="copy10grey">
                                    Country:
                                </td> 
                                <td >
                                     &nbsp;<asp:TextBox ID="txtCountry" runat="server" CssClass="copy10grey" MaxLength="50" Width="80%"></asp:TextBox>
                                </td>
                                </tr>
                            <tr valign="top">
                                <td align="right" class="copy10grey">
                                   ZIP:<span class="errormessage">*</span>
                                </td>
                                <td>
                                
                                     &nbsp;<asp:TextBox ID="txtZip" runat="server" CssClass="copy10grey" MaxLength="20" Width="80%" onchange="validateZIP(this.value)"></asp:TextBox>
                                </td>
                             <td align="right" class="copy10grey" rowspan="2" valign="top">
                                    Sales Person:</td>
                                <td rowspan="2">
                                
                                     &nbsp;<asp:ListBox ID="lbSalesPerson" CssClass="copy10grey" runat="server" Width="82%" SelectionMode="Multiple"></asp:ListBox>
                                </td></tr>
                            <tr valign="top">
                                <td align="right" class="copy10grey" >
                                   E-mail:<span class="errormessage">*</span>
                                </td>
                                <td>
                                
                                     &nbsp;<asp:TextBox ID="txtEmail" runat="server" CssClass="copy10grey" MaxLength="50" 
                                        Width="80%" onchange="echeck(this);" 
                                        ></asp:TextBox>
                                </td>
                             </tr>
                            <tr><td colspan="4"><hr /></td></tr>
                            </table>
                        <%-- </ContentTemplate>
                        </asp:UpdatePanel>--%>
                         <table align="center">
                         
                            <tr><td colspan="4" align="center">
                                    <asp:Button ID="btnSubmit" runat="server" CssClass="button" 
                                        Text="Submit Customer " onclick="btnSubmit_Click" onclientclick="return ValidateForm();" />&nbsp;&nbsp;
                                    <asp:Button ID="btnCancel" runat="server" CssClass="button" Text="Cancel" 
                                        onclick="btnCancel_Click"  /><br />      
	                        </td></tr>

            </table>
            	    </td>
            	 </tr>
           </table>
            	 
            
            
        <table width="100%" align="center">
        <tr><td>&nbsp;</td></tr>
				<tr>
            <td><foot:MenuFooter id="Footer" runat="server"></foot:MenuFooter></td>
          </tr>
				        
        </table>
                 
    </form>
</body>
</html>
