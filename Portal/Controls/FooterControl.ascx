<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FooterControl.ascx.cs" Inherits="avii.Controls.FooterControl" %>
<%--Site key
6LeblVwUAAAAAHlIzyOktPVtXLzBpP09h2159D-T
Secret key
6LeblVwUAAAAAKytE2q3Wl10cTSrGVjygtyxg_-9--%>

<%--<div class="g-recaptcha" data-sitekey="6LeblVwUAAAAAHlIzyOktPVtXLzBpP09h2159D-T"></div>--%>
<asp:UpdatePanel ID="UpdatePanel1"  runat="server" ChildrenAsTriggers="true"  >
     <Triggers>
     <asp:AsyncPostBackTrigger ControlID="btnSubmit" EventName="Click" />
     </Triggers>
     <ContentTemplate>
     
<script lang="javascript" type="text/javascript">
   
    function addDashes(f) {
       // var number = f.value;
       // var new_number = '';
       var f_val = f.value.replace(/\D[^\.]/g, "");
       // //alert(number)
       // if (number.length > 2)
       // {
       //     // matches: 123 || 123-4 || 123-45

       //     new_number = number.substring(0, 3) + '-';
       //     //alert(number.length)
       //     // matches: 123-4 || 123-45
       //    // if (number.length == 4)
       //    //     new_number = new_number + number.substring(3, 1);
       //     if (number.length == 5)
       //         new_number = new_number + number.substring(4, 1);
       //     if (number.length == 6)
       //         new_number = new_number + number.substring(4, 2);
       //     if (number.length > 6) {
       //         // matches: 123-456 || 123-456-7 || 123-456-789
       //         new_number = new_number + number.substring(4, 3) + '-';
       //     }
       //     if (number.length == 9)
       //         new_number = new_number + number.substring(8, 1);
       //     if (number.length == 10)
       //         new_number = new_number + number.substring(8, 2);
       //     if (number.length == 11)
       //         new_number = new_number + number.substring(8, 3);
       //     if (number.length = 12) {
       //         // matches: 123-456-7 || 123-456-789 || 123-456-7890
       //         new_number = new_number + number.substring(8, 4);
       //     }
       // }
       // else
       // {
       //     new_number = number;
       // }
       // f.value = new_number

        //if (f_val.length == 3)
        //    f.value = f_val.slice(0, 3) + "-";

        
        //if (f_val.length == 4)
        //    f.value = f_val.slice(0, 3) + "-"+ f_val.slice(3, 1);
        //if (f_val.length == 6)
        //    f.value = f_val.slice(0, 3) + "-" + f_val.slice(3, 6) + "-";

        if (f_val.length == 10)
            f.value = f_val.slice(0, 3) + "-" + f_val.slice(3, 6) + "-" + f_val.slice(6);
    }
    function ValidateEmail(obj) {
        var EmaiAddress = obj.value;

        var RegExEmail = /^(?:\w+\.?)*\w+@(?:\w+\.)+\w+$/;
        if (obj.value != '') {
            if (!RegExEmail.test(EmaiAddress)) {
                obj.focus();
                alert("Invalid E-mail");
                return false;
            }
        }

    }
    function isNumberKey(evt) {

        var charCodes = evt.keyCode ? evt.keyCode : evt.which ? evt.which : evt.charCode;
        if (charCodes > 31 && (charCodes < 48 || charCodes > 57)) {
            charCodes = 0;
            return false;
        }
        return true;
    }
    function IsValidate() {

        var Name = document.getElementById("<%=txtName.ClientID %>");
        var email = document.getElementById("<%=txtEmail.ClientID %>");
        var Mobile = document.getElementById("<%=txtMobile.ClientID %>");
        var comment = document.getElementById("<%=txtcomments.ClientID %>");
        var flag = true;
        if (Name.value == "") {
            alert("Name required!");
            Name.focus();
            flag = false;
            return false;
        }
        if (email.value == "") {
            alert("Email required!");
            email.focus();
            flag = false;
            return false;
        }
        var EmaiAddress = email.value;
        var RegExEmail = /^(?:\w+\.?)*\w+@(?:\w+\.)+\w+$/;
        if (obj.value != '') {
            if (!RegExEmail.test(EmaiAddress)) {
                email.focus();
                alert("Invalid E-mail");
                flag = false;
                return false;
            }
        }
        if (Mobile.value == "") {
            alert("Mobile required!");
            Mobile.focus();
            flag = false;
            return false;
        }
        if (comment.value == "") {

            alert("Query/Suggestion required!");
            comment.focus();
            flag = false;
            return false;
        }
        return flag;
    }
</script>
 <footer>
         <div class="foot">
            <div class="container-fluid">
                <div class="row">
                <div class="col-lg-3 col-md-4">
					<div class="foot_left">
						<div class="foot_logo">
							<a href="index.aspx"><img src="img/foot_logo.png"></a>
						</div>
					<div class="con_details">
							<div class="con_numbers">
								<p><a href="https://goo.gl/maps/DzMt3LfSQpm" target="_blank">4080 North Pecos Road, Suite# 1010, <br /> Las Vegas, Nevada 89115</a></p>
								<p>E-mail: <a href="mail-to:Contact@langlobal.com">Contact@langlobal.com</a></p>
								<p class="con_pad">Office Phone: <a href="tel:(818) 933-2222"> (818) 933-2222</a></p>
								<p>Fax number: <a href="tel:(818) 933-2225"> (818) 933-2225</a></p>
							</div>
							<div class="foot_icons">
								<ul>
									<li><a href="#"><i class="fa fa-facebook" aria-hidden="true"></i></a></li>
									<li><a href="#"><i class="fa fa-instagram" aria-hidden="true"></i></a></li> 
									<li><a href="#"><i class="fa fa-linkedin-square" aria-hidden="true"></i></a></li> 
								</ul>
							</div>
						</div>
					</div>
                </div>
                <div class="col-lg-4">
				
				<%--<?php 
				if($succMsg){?>
				<div class="" style="color:green;">   <?php echo $succMsg;?> </div>
				<?php } else{?>
					<div class="" style="color:red;"><?php echo $errMsg; ?></div>
					
				<?php }?>--%>
					<div class="foot_form">
						<h2>Get In Touch</h2>
						<%--<form method="POST" action="" id="request-call" name="request-call">--%>
						<div class="row">
							<div class="col-lg-4 col-md-6 form-group">
                               <asp:HiddenField ID="hdnMsg" runat="server" />
                                <asp:ValidationSummary ID="vlsManageEmployees" ForeColor="Red" ValidationGroup="onSave"
                                    ShowSummary="false" EnableClientScript="true" ShowMessageBox="true"  CssClass="errSummary" runat="server" HeaderText="Please correct following errors." />

                                <asp:TextBox ID="txtName" CssClass="form-control required" MaxLength="50" runat="server" placeholder="Name" ></asp:TextBox>
                                <asp:RequiredFieldValidator ID="vldrName" ControlToValidate="txtName"
                                                            ForeColor="Red" Text="*" ValidationGroup="onSave" runat="server" ErrorMessage="Name required...!!"></asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtName"
                                                                ValidationExpression="[a-zA-Z ]*$" ValidationGroup="onSave" ForeColor="Red" Text="*" ErrorMessage="Valid characters for Name: Alphabets and space.">
                                                                </asp:RegularExpressionValidator>
								<%--<input type="text" class="form-control" id="name" placeholder="Name" name="Name" required>onchange="return ValidateEmail(this);"--%>
							</div>
							<div class="col-lg-4 col-md-6 form-group">
                                <asp:TextBox ID="txtEmail" CssClass="form-control required"  MaxLength="100" runat="server" placeholder="Email" ></asp:TextBox>
                               <asp:RequiredFieldValidator ID="vldrEmailId" runat="server" ControlToValidate="txtEmail"
                                                            ErrorMessage="Email required...!!" ForeColor="Red" Text="*" ValidationGroup="onSave"></asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator ID="vlxEmailId" runat="server" ControlToValidate="txtEmail"
                                                                ErrorMessage="Email Id is not in correct format...!!" ForeColor="Red" Text="*"
                                                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="onSave"></asp:RegularExpressionValidator>
                                                        
								<%--<input type="email" class="form-control" id="email" placeholder="Email" name="email" required>-- onkeypress="return isNumberKey(event);"--%>
							</div>
							<div class="col-lg-4 col-md-12 form-group">
                                <asp:TextBox ID="txtMobile" MaxLength="12" CssClass="form-control required" onkeyup="formatPhone(this);" runat="server" placeholder="Mobile" ></asp:TextBox>
                                <asp:RequiredFieldValidator ID="vldrMobileNo" ControlToValidate="txtMobile"
                                                            ForeColor="Red" Text="*" ValidationGroup="onSave" runat="server" ErrorMessage="Mobile required..!!"></asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator6" 
                                                            runat="server" ErrorMessage="Enter valid Phone number" 
                                                            ControlToValidate="txtMobile"  Text="*" ValidationGroup="onSave" 
                                                            ValidationExpression= "^([\(]{1}[0-9]{3}[\)]{1}[\.| |\-]{0,1}|^[0-9]{3}[\.|\-| ]?)?[0-9]{3}(\.|\-| )?[0-9]{4}$"></asp:RegularExpressionValidator>
                                                            
								<%--<input type="text" class="form-control" id="phone" placeholder="Mobile" name="phone" required>--%>
							</div>
							<div class="col-md-12">
								<div class="form-group">
                                    <asp:TextBox ID="txtcomments" CssClass="form-control required"  TextMode="MultiLine" Rows="3" runat="server" placeholder="Query/Suggestion" ></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtcomments"
                                                            ForeColor="Red" Text="*" ValidationGroup="onSave" runat="server" ErrorMessage="Query/Suggestion required..!!"></asp:RequiredFieldValidator>
                                                            
									<%--<textarea class="form-control" id="comments" rows="3" placeholder="Query/Suggestion" name="Subject"></textarea>--%>
								</div>
							</div>
                            <div class="col-md-8">
                                <div class="captcha_col">
                                    <%--<div class="g-recaptcha" data-sitekey="6LeblVwUAAAAAHlIzyOktPVtXLzBpP09h2159D-T"></div>--%>
                                    <div id="recaptcha" class="recaptcha"></div>
                                    <asp:Label ID="lblMsg" ForeColor="Red" runat="server"></asp:Label>
                                </div>
                            </div>
							<div class="col-md-4">
								<div class="submit_btn">
                                <asp:Button ID="btnSubmit" ValidationGroup="onSave" CssClass="btn btn-warning" runat="server" Text="Send" OnClick="btnSubmit_Click" />
								<%--<input name="submit" value="Send" type="submit" class="btn btn-warning">--%>
								</div>
							</div>
						</div>
						
					</div>
				</div>
                <div class="col-lg-5">
                    <div class="foot_links">
                        <div class="about_links">
                            <h4>About Us</h4>
                        <ul>
                            <li><a href="about.aspx">Overview</a></li>
                            <li><a href="#">Team</a></li>
                            <li><a href="partnership.aspx">Partnership</a></li>
                        </ul>
                        </div>
                        <div class="about_links li_chng">
                            <h4>What we do</h4>
						<%--<?php if(basename($_SERVER['SCRIPT_FILENAME'])=='whatwedo.php'){ ?>
						<ul>
                            <li><a href="#headingfive" data-tab="#headingfive" class="openTab" data-open="collapsefive">Provisioning</a></li>
                            <li><a href="#headingfour" data-tab="#headingfour" class="openTab" data-open="collapsefour">Supply Chain</a></li>
                            <li><a href="#headingsix" data-tab="#headingsix" class="openTab" data-open="collapsesix">Reverse logistics</a></li>
                            <li><a href="#headingTwo" data-tab="#headingTwo" class="openTab" data-open="collapseTwo">Asset Management</a></li>
                            <li><a href="integration.aspx">Integrations</a></li>
                            <li><a href="#headingThree" data-tab="#headingThree" class="openTab" data-open="collapseThree">Data Security</a></li>
                            <li><a href="visibilty.aspx">Visibility & Auditing</a></li>
                        </ul>
						<?php }else{ ?>--%>
                        <ul>
                            <li><a href="whatwedo.aspx#headingfive">Provisioning</a></li>
                            <li><a href="whatwedo.aspx#headingfour">Supply Chain</a></li>
                            <li><a href="whatwedo.aspx#headingsix">Reverse logistics</a></li>
                            <li><a href="whatwedo.aspx#headingTwo">Asset Management</a></li>
                            <li><a href="integration.aspx">Integrations</a></li>
                            <li><a href="whatwedo.aspx#headingThree">Data Security</a></li>
                            <li><a href="visibilty.aspx">Visibility & Auditing</a></li>
                        </ul>
						<%--<?php } ?>--%>
                        </div>
                        <div class="about_links">
                            <h4>Compliance</h4>
                        <ul>
                            <li><a href="compliance.aspx">Compliance</a></li>
                            <li><a href="compliance.aspx#headingOne">Environment</a></li>
                            <li><a href="compliance.aspx#headingTwo">Recycling</a></li>
                        </ul>
                        </div>
                    </div>
                </div>
             </div>
             </div>
         </div>
      </footer>
          
        </ContentTemplate>
    </asp:UpdatePanel>
 
	<!--last line foot start-->
    <!-- Optional JavaScript -->
    <!-- jQuery first, then Popper.js, then Bootstrap JS -->
    
	
	
	<script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
	<script src="https://code.jquery.com/jquery-3.3.1.slim.min.js"></script>
	<script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.3/umd/popper.min.js" ></script>
	<script src="https://stackpath.bootstrapcdn.com/bootstrap/4.1.1/js/bootstrap.min.js" ></script>
	
	<script src="js/wow.min.js"></script>
    <script src="js/main-min.js"></script>
	<script src="js/jquery.validate.min.js"></script> 
        <script>
            function formatPhone(obj) {
                var numbers = obj.value.replace(/\D/g, ''),
                    char = { 3: '-', 6: '-' };
                obj.value = '';
                for (var i = 0; i < numbers.length; i++) {
                    obj.value += (char[i] || '') + numbers[i];
                }
            }
            
            $(document).ready(function () {

                <%--var msg = document.getElementById("<%=hdnMsg.ClientID %>");
                var txt = document.getElementById("<%=txtName.ClientID %>");
                if (msg.value == 'sent') {
                    alert('Thank you for contacting LAN Global. \n\n Your message will be sent to our customer support team.');
                    txt.focus();
                }
                msg.value = '';--%>
                

            $('a.openTab').click(function(){
                $('html, body').animate({
                    scrollTop: $( $(this).attr('data-tab') ).offset().top
                }, 500);
                var openingTabVal = $(this).attr('data-open');
                $("#"+openingTabVal).addClass('show');
                return false;
            });
            
            var tabOpen = window.location.hash.substr(1);
            if(tabOpen != '' && tabOpen != undefined){
                $("#"+tabOpen).next().addClass('show');
            }
        });
    </script>
