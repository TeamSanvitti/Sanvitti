<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="commitment.aspx.cs" Inherits="avii.commitment" %>
<%@ Register TagPrefix="UC" TagName="Footer" Src="~/Controls/FooterControl.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <!-- Required meta tags -->
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <!-- Bootstrap CSS -->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css">
	<link href="https://maxcdn.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" rel="stylesheet">
	<link rel="stylesheet" href="css/stylenew.css">
	
	<link rel="stylesheet" href="css/animate.min.css">
	<script src="https://www.google.com/recaptcha/api.js" async defer></script>
    <%--<script src="https://www.google.com/recaptcha/api.js?onload=onloadCallback&render=explicit" async defer type="text/javascript"> </script>
<script type="text/javascript">
    var onloadCallback = function () {
        grecaptcha.render('recaptcha', {
            'sitekey': '6LeblVwUAAAAAHlIzyOktPVtXLzBpP09h2159D-T'
        });
    };
</script>--%>
	
    <title>Lan Global</title>
</head>
<body class="about">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <section class="upper_part">
          <div class="user_links res_on">
            <div class="upper_user_logo">
               <a href="../logon.aspx">  <img class="home_pg" src="img/user_logo.png">
				 <img src="img/user_logo2.png"></a>
            </div>
          </div>
            <header>
            <div class="head">
                <div class="container-fluid">
                    <div class="row">
                        <div class="col-md-12 col-12 wid_0">
                            <div class="logo">
                                <a href="index.aspx"><img src="img/logo_2nd.png"></a>
                            </div>
							<div class="other_logos">
								<ul>
									<%--<li><img src="img/logo_1.jpg"></li>--%>
									<li><img width="59" height="86"  src="img/logo_2.jpg"></li>
									<li><a target="_blank" href="http://sustainableelectronics.org/"><img src="img/R2V3_certified_logo_ccexpress.jpeg"></a></li>
								</ul>
							</div>
                        </div>
					</div>
					<div class="row nav_bg">
                        <div class="col-md-12 col-12 pad_0">
                            <div class="navbar_start">
                                <nav class="navbar navbar-expand-md navbar-light">
                                  <a class="navbar-brand" href="#">&nbsp;</a>
                                  <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
                                    <span class="navbar-toggler-icon"></span>
                                  </button>

                                  <div class="collapse navbar-collapse" id="navbarSupportedContent">
                                    <ul class="navbar-nav mr-auto">
                                      <li class="nav-item">
                                        <a class="nav-link" href="about.aspx">who we are </a>
                                      </li>
                                      <li class="nav-item">
                                        <a class="nav-link" href="whatwedo.aspx">what we do</a>
                                      </li>
                                      <li class="nav-item">
                                        <a class="nav-link" href="compliance.aspx">compliance</a>
                                      </li>
                                      <li class="nav-item user_none">
                                         <a href="logon.aspx"><img src="img/user_logo2.png"></a>
                                      </li>
                                    </ul>
                                  </div>
                                </nav>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </header>
        <%--<header>
            <div class="head">
                <div class="container-fluid">
                    <div class="row">
                        <div class="col-md-4 col-6 wid_0">
                            <div class="logo">
                                <a href="index.aspx"><img src="img/logo_2nd.png"></a>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div >
                                <img src="img/compliancelogos.jpg" />
                            </div>
                        </div>
                        <div class="col-md-5 col-12 pad_0">
                            <div class="navbar_start">
                                <nav class="navbar navbar-expand-md navbar-light">
                                  <a class="navbar-brand" href="#">&nbsp;</a>
                                  <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
                                    <span class="navbar-toggler-icon"></span>
                                  </button>

                                  <div class="collapse navbar-collapse" id="navbarSupportedContent">
                                    <ul class="navbar-nav mr-auto">
                                      <li class="nav-item">
                                        <a class="nav-link" href="about.aspx">who we are </a>
                                      </li>
                                      <li class="nav-item">
                                        <a class="nav-link" href="whatwedo.aspx">what we do</a>
                                      </li>
                                      <li class="nav-item">
                                        <a class="nav-link" href="compliance.aspx">compliance</a>
                                      </li>
                                      <li class="nav-item user_none">
                                        <a href="logon.aspx"><img src="img/user_logo2.png"></a>
                                      </li>
                                    </ul>
                                  </div>
                                </nav>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </header>--%>
      </section>
      <div class="clearfix"></div>
      <div class="page_heading">
        <div class="heading_bg about">
            <h4 class="wow fadeInRight">Who we are</h4>
        </div>
      </div>
      <section class="about_pg">
        <div class="about_inner">
            <div class="first_section left_bg">
				<div class="about_sec img_set">
                    <div class="sec_1_inner about_img wow fadeInLeft">
                        <img src="img/about_1p.jpg">
                    </div>
                </div>
                <div class="about_sec">
                    <div class="sec_1_inner about_txt commit">
                        <div class="about_txt_inner pad_1p wow fadeInRight">
                        <h4>COMMITMENT</h4> 
                        <p>LAN Global is committed to creating strategic partnerships with customers through personalized software, 
                            tailored exclusively to each business. We use different methodologies to calculate  quality of service (QoS) 
                            and employ multiple processes that allow us to collect and document the highest quality control metrics to 
                            calculate QoS.
                        </p>
                           
						<p>All processes are evaluated for accuracy, and data is monitored around the clock to ensure a zero margin of error.
                            Through our multi-step quality control process, finished goods are ready for reuse. Monthly customer 
                            surveys ensure that client satisfaction is at a 100 percent rate. Quarterly management review meetings 
                            allow our company to continuously improve and evolve our system to keep up with today's highest industry 
                            standards.

						</p>
						
                        </div>
                    </div>
                </div>
            </div>
            <div class="clearfix"></div>
        </div>
      </section>
      <div class="clearfix"></div>
      <section class="companies_logo">
        <div class="logo_set">  
            <ul>
              <li><img src="img/logo_1.png"></li>
                <li><img src="img/logo_2.png"></li>
                <li><img src="img/logo_3.png"></li>
               <%-- <li><img src="img/logo_4.png"></li>--%>
                <li><img src="img/logo_5.png"></li>
                <li><img src="img/logo_6.png"></li>
               <%-- <li><img src="img/logo_7.png"></li>--%>
                <li><img src="img/logo_8.png"></li>
                <li><img src="img/logo_9.png"></li>
            </ul>
        </div>
      </section>
      <UC:Footer ID="footer1" runat="server" />

    </form>
</body>
</html>
