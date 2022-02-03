<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="visibilty.aspx.cs" Inherits="avii.visibilty" %>
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
   <%-- <script src="https://www.google.com/recaptcha/api.js?onload=onloadCallback&render=explicit" async defer type="text/javascript"> </script>
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
									<li><img width="59" height="86" src="img/logo_2.jpg"></li>
									<li><a target="_blank" href="http://sustainableelectronics.org/"><img src="img/logo_3.jpg"></a></li>
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
       <%-- <header>
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
                                      <li class="nav-item active">
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
                <div class="about_sec">
                    <div class="sec_1_inner about_img wow fadeInLeft">
                        <img src="img/about_4p.jpg">
                    </div>
                </div>
                <div class="about_sec">
                    <div class="sec_1_inner about_txt visible">
                        <div class="about_txt_inner pad_4p wow fadeInRight">
                            <h4>VISIBILITY & AUDITING</h4> 
                            <p>
                                LAN Global is committed to creating and nurturing strategic partnerships with customers through the 
                                creation of, and access to, customized business intelligence and reporting systems. 
                                We have implemented Six Sigma methodology to calculate quality of service (QoS) and have multiple
                                processes that allow the collection and documentation of quality control metrics to calculate  QoS.
                            </p>
                            <p>
                                All processes are measured for flow, throughput, responsiveness and accuracy. The data is monitored 
                                hourly/daily to ensure output is at a maximum whilst maintaining a zero percentage of errors. 
                                All inbound focus materiais have  a documented multi-step quality control process prior to entry into 
                                inventory as finished goods. In addition, we conduct customer service surveys on a quarterly basis.
                            </p>
                        <%--<p>LAN utilizes a custom designed business intelligence platform that can pull data from virtually any enterprise resource planning (ERP) system and enterprise-level accounting software for inventory management, production control and financial reporting. We can work with virtually any digital format including spreadsheets, databases, and cloud data.</p>
						<p>We can provide up to the minute production and costing and transmit that data via EDI, XML or other suitable format. Furthermore, this technology allows us to provide a visual dashboard of the given metrics in a web display format that is easily accessed via secure permission-based web portal for desktop and mobile viewing. The individual user can tailor the report to their liking by pivot-style categories to give the utmost granular detail, if required.</p> 
                        --%></div>
                    </div>
                </div>
            </div>
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
