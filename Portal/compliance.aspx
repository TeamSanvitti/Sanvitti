<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="compliance.aspx.cs" Inherits="avii.compliance" %>
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
<body class="about compliance_set">
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
      <%--  <header>
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
            <h4 class="wow fadeInRight">compliance</h4>
        </div>
      </div>
      <section class="wht_we_do_pg txt_set">
        <div class="inner_wht_we_do_pg">
            <div class="cstm_container">
                <div class="wht_txt">
                    <div class="upper_txt">
                        <p>LAN Global is committed to creating and nurturing strategic partnerships with customers through the creation of,
                            and access to, customized business intelligence and reporting systems. We have implemented Six Sigma methodology
                            to calculate  quality of service (QoS) and have multiple processes that allow the collection and documentation 
                            of quality control metrics to calculate  QoS.</p>
                    </div>
                    <div class="btm_txt">
                        <p>
                            All processes are measured for flow, throughput, responsiveness and accuracy. The data is monitored 
                            hourly/daily to ensure output is at a maximum whilst maintaining a zero percentage of errors. 
                            All inbound focus materials have a documented multi-step quality control process prior to entry into 
                            inventory as finished goods. In addition, we conduct customer service surveys on a quarterly basis. </p>
                    </div>
                </div>
                <div class="accordion_set">
                    <div class="panel-group" id="accordion" role="tablist" aria-multiselectable="true">
                      <div class="panel panel-default">
                            <div class="panel-heading" role="tab" id="headingOne">  
                                 <div class="title" data-role="title" data-toggle="collapse" data-parent="#accordion" href="#collapseOne" aria-expanded="false" aria-controls="collapseOne">
                                    <h5>ENVIRONMENT SAFETY</h5>
                                </div>
                            </div>
                         <div id="collapseOne" class="panel-collapse collapse in" role="tabpanel" aria-labelledby="headingOne">
                              <div class="panel-body">
                                  <p>LAN Global is committed to  environmental, quality, health and safety management. 
                                      These values are  key components of our company’s standards, which we are continuously 
                                      improving to meet the changing demands of the industry.</p>
                                  
                                  <%--<p>Furthermore, the Company's Environmental, Quality, Health and Safety Policy calls for continual improvement in its respective management activities. </p>
                                  <p>We Will:</p>
								  <div class="compliance_li_inner chng_list_style">
									  <ul>
										<li>Furthermore, the Company's Environmental, Quality, Health and Safety Policy calls for continual improvement in its respective management activities. </li>
										<li>Follow a concept of continual improvement and make best use of our management resources in all matters of environmental, quality, health and safety matters. Communicate our objectives/targets and our performance against defined objectives/targets throughout the Company and to interested parties.</li>
									  </ul>
								  </div>--%>
                              </div>
                          </div>
                      </div>

                    <div class="panel panel-default">
                         <div class="panel-heading" role="tab" id="headingTwo"> 
                                 <div class="title collapsed" data-role="title" data-toggle="collapse" data-parent="#accordion" href="#collapseTwo" aria-expanded="false" aria-controls="collapseTwo">
                                    <h5>RECYCLING</h5>
                                </div>
                            </div>
                         <div id="collapseTwo" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingTwo">
                              <div class="panel-body">
                                  <p> We are committed to meet customer satisfaction with quality wireless products by maintaining and 
                                      improving the environmental management system (EMS), complying  with all environmental laws, 
                                      regulations, codes of practice, prevention of pollution and prevention of injury and ill health.
                                      By utilizing best practices, we 
                                      able to reduce our carbon footprint on the environment. Our priorities include: 
                                    </p>
                                  <div class="compliance_li_inner chng_list_style">
									  <ul>
										<li> addressing clients’ security and privacy issues through the destruction of e-Waste data contained in e-Waste in accordance with the R2v3 standard throughout the recycling chain.</li>
										<li>ensuring accountability for all focus materials and hazardous e-waste.</li>
										<li>managing used and end-of-life electronics equipment based on a "reuse, refurbish, recover, dispose" hierarchy of responsible management strategies. </li>
									  </ul>
								  </div>
                              </div>
                          </div>
                      </div>
                     
                    <div class="panel panel-default">
                         <div class="panel-heading" role="tab" id="headingThree"> 
                                 <div class="title collapsed" data-role="title" data-toggle="collapse" data-parent="#accordion" href="#collapseThree" aria-expanded="false" aria-controls="collapseThree">
                                    <h5>CERTIFICATION & COMMITMENT</h5>
                                </div>
                            </div>
                          <div id="collapseThree" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingThree">
                              <div class="panel-body">
                                  <p>
                                      LAN Global maintains ISO 14001:2015, ISO 45001:2018 and R2v3 certifications, adhering to a strict zero e-waste policy, which ensures that none of the equipment collected through our reuse and recycling programs, including accessories, enter the waste stream. LAN is an EPA Waste Handler approved agency as well as a Waste Wise Endorser. All of these processes are audited regularly by certified 3rd party registrars to ensure that the company remains in compliance with all industry requirements. As industry leaders, LAN Global is dedicated to maintaining 100 percent green processes throughout our production facility.
                                  </p>
                                  <div class="panel_inner_txt">
                                      <%--<div class="Programming">
                                          <div class="heading">
                                          <h3>ISO 9001:2008 Certification</h3>
                                          </div>
                                          <p>
                                              ISO 9001:2008 is a family of standards and guidelines for quality in the manufacturing and service industries from the International Organization for Standardization (ISO). ISO certification ensures that the processes that develop the product are documented and performed in a quality manner.

                                          </p>
                                      </div>--%>
									  <div class="Programming img_set">
                                          <div class="heading">
                                          <h3>ISO 14001:2015 Certification</h3>
                                          </div>
                                          <p>
                                              ISO 14001:2015 is the international specification for an environmental management system (EMS). It specifies requirements for establishing an environmental policy, determining environmental aspects and impacts of products/activities/services, planning environmental objectives and measurable targets, implementation and operation of programs to meet objectives and targets, checking and corrective action and management review.

                                          </p>
										  <img src="img/R2V3_certified_logo_ccexpress.jpeg">
                                      </div>
                                      <div class="LabTesting">
                                          <div class="heading">
                                          <h3>R2v3 Certification</h3>
                                          </div>
                                          <p>
                                              R2 Certification, also known as Responsible Recycling Certification, provides verification of LAN’s environmentally responsible, safe, and transparent management of cell phone batteries, and other consumer electronics.

                                          </p>
                                      </div>
									   <%-- <div class="LabTesting">
                                          <div class="heading">
                                          <h3>OHSAS 18001:2007 Certification</h3>
                                          </div>
                                          <p>OHSAS 18001:2007 is an international occupational health and safety management system specification. The certification exhibits a company’s efforts to minimize risk to employees and improve an existing OH&S management system.

                                          </p>
                                      </div>--%>
                                      <div class="LabTesting">
                                          <div class="heading">
                                          <h3>ISO 45001:2018 Certification</h3>
                                          </div>
                                          <p>
                                              

                                          </p>
                                      </div>
                                      <div class="LabTesting">
                                          <div class="heading">
                                          <h3>ISO:9001:2015 Certification</h3>
                                          </div>
                                          <p>
                                              

                                          </p>
                                      </div>
                                  </div>
                              </div>
                          </div>
                      </div>
                        <div class="panel panel-default">
                         <div class="panel-heading" role="tab" id="headingfour"> 
                                 <div class="title collapsed" data-role="title" data-toggle="collapse" data-parent="#accordion" href="#collapsefour" aria-expanded="false" aria-controls="collapsefour">
                                    <h5>Quality, Environmental, Health and Safety policies</h5>
                                </div>
                            </div>
                         <div id="collapsefour" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingfour">
                              <div class="panel-body">
                                  <p> 
                                      <img src="documents/5.2.1-P_Quality_Environmental_Health_Safety_Policy-1.4.png" width="100%">

<%--                                        LAN Global enacted  stringent environmental quality rules for the protection of environment and employes. 
                                      Please read here to understand the <a target="_blank" href="/Documents/R2 Safety Policy.pdf">Quality, Environmental, Health and Safety policies</a> of LAN Global.--%>
                                    </p>
                                  
                              </div>
                          </div>
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
