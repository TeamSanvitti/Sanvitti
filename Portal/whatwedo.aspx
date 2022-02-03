<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="whatwedo.aspx.cs" Inherits="avii.whatwedo" %>

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
                                      <li class="nav-item active">
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
            <h4 class="wow fadeInRight">What we do</h4>
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
                        <p>All processes are measured for flow, throughput, responsiveness and accuracy. The data is monitored 
                            hourly/daily to ensure output is at a maximum whilst maintaining a zero percentage of errors. 
                            All inbound focus materials have a documented multi-step quality control process prior to entry into 
                            inventory as finished goods. In addition, we conduct customer service surveys on a quarterly basis. </p>
                    </div>
                </div>
                <div class="accordion_set">
                    <div class="panel-group" id="accordion" role="tablist" aria-multiselectable="true">
                      <div class="panel panel-default">
                            <div class="panel-heading" role="tab" id="headingOne">  
                                 <div class="title collapsed open_tab_we" data-role="title" data-toggle="collapse" data-parent="#accordion" href="#collapseOne" aria-expanded="false" aria-controls="collapseOne">
                                    <h5>asset recovery</h5>
                                </div>
                            </div>
                         <div id="collapseOne" class="panel-collapse collapse in" role="tabpanel" aria-labelledby="headingOne">
                              <div class="panel-body">
                                  <p>Lan Global will assess the value of your mobile products through stringent and 
                                      certified testing  processes and reinstate maximum value for the product.</p>
                                  <p>For product that cannot be reused or resold, LAN recovers the items’ valuable resources and 
                                      recycles them into saleable components and materials.
                                      This is done through a comprehensive triage process in which we sort through procured equipment 
                                      to identify salvageable parts  for resale and useable elements  for reuse. 
                                      Through this process, LAN is able to maximize the useful lives of many re-usable electronic assets and further separate those assets containing valuable natural elements. </p>
                              </div>
                          </div>
                      </div>

                    <div class="panel panel-default">
                         <div class="panel-heading" role="tab" id="headingTwo"> 
                                 <div class="title collapsed open_tab_we" data-role="title" data-toggle="collapse" data-parent="#accordion" href="#collapseTwo" aria-expanded="false" aria-controls="collapseTwo">
                                    <h5>asset management</h5>
                                </div>
                            </div>
                         <div id="collapseTwo" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingTwo">
                              <div class="panel-body">
                                  <p>LAN has developed and maintains proprietary internal systems that track all products through 
                                      the supply chain in real-time, as well as measure up-to-the-hour productivity and cost. 
                                      This system allows LAN to provide a true cost analysis, as well as provide customers with an 
                                      accurate market overview and valuation on all of the assets. 
                                      LAN employs a team of programmers that maintain and make continual improvements to this system.</p>
                                  <p>Our customer’s data is our top priority security of. 
                                      We certify 100 percent sanitization and/or destruction of data on any assets processed. 
                                      Furthermore, we can provide certificates of destruction on any product,
                                       initiating its record into our resource management system. 
                                      This network  gives clients access to customized reports  and information around the clock.</p>
                              </div>
                          </div>
                      </div>

                    <div class="panel panel-default">
                         <div class="panel-heading" role="tab" id="headingThree"> 
                                 <div class="title collapsed open_tab_we" data-role="title" data-toggle="collapse" data-parent="#accordion" href="#collapseThree" aria-expanded="false" aria-controls="collapseThree">
                                    <h5>data security</h5>
                                </div>
                            </div>
                          <div id="collapseThree" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingThree">
                              <div class="panel-body">
                                  <p>Data security is an integral part of recycling electronics. LAN Global strictly adheres to the 
                                      National Institute of Standards and Technology and the U.S. Department of Commerce for the handling 
                                      of Information Security. </p>
                                  <p>These regulations have increased governments’ and businesses’ need to properly dispose of confidential 
                                      data when retiring end-of-life equipment. After a device is collected by LAN Global  it is assessed to 
                                      determine if it can be refurbished, recycled for parts or if it needs to be destroyed. 
                                      For items that can be refurbished or used for parts, LAN Global wipes these devices in accordance 
                                      with Department of Defense standards, ensuring that 100 percent of data is destroyed. The item is then either refurbished or broken down for parts, which are then used to repair other electronic devices.</p>
                                  <p>All working data is stored on a secure internal network that is only accessible via multi-tiered 
                                      permissions that are password protected, and reside behind firewall systems 
                                      (hardware/software). Sensitive data such as password information is stored on a secure server 
                                      that is only accessible via global administrator privilege. Sensitive information is only passed on to relevant concerned parties. Data security can be further tailored to meet the specifications of individual client’s requirements.</p>
                              </div>
                          </div>
                      </div>

                    <div class="panel panel-default">
                         <div class="panel-heading" role="tab" id="headingfour"> 
                                 <div class="title collapsed open_tab_we" data-role="title" data-toggle="collapse" data-parent="#accordion" href="#collapsefour" aria-expanded="false" aria-controls="collapsefour">
                                    <h5>supply chain</h5>
                                </div>
                            </div>
                              <div id="collapsefour" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingfour">
                              <div class="panel-body chng_list_style">
                                  <p>LAN Global offers a full variety of fulfillment models that include:
                                  </p>
                                  <ul>
                                      <li>Ordering and buying product for the customer</li>
                                    <li>Warehousing</li>   
                                    <li>Kitting</li>   
                                    <li>Customized packaging</li>   
                                    <li>Shipping</li>   
                                    <li>Reverse logistics</li>
                                    <li>Sell to their distributor</li>   
                                    <li>DOA and Warranty</li>   
                                  </ul>
                                  <p>Our company has established strong OEM partner relationships to resource the best in today's 
                                      handset and accessory offerings. Our customers have the option to buy products for themselves with 
                                      LAN Global's support or we can purchase on the customer's behalf directly from manufacturers. 
                                       Additionally, our buying team offers purchase options for both large and small bulk and gift box quantities.</p>
                              </div>
                          </div>
                      </div>
                      <div class="panel panel-default">
                         <div class="panel-heading" role="tab" id="headingfive"> 
                                 <div class="title collapsed open_tab_we" data-role="title" data-toggle="collapse" data-parent="#accordion" href="#collapsefive" aria-expanded="false" aria-controls="collapsefive">
                                    <h5>provisioning</h5>
                                </div>
                            </div>
                              <div id="collapsefive" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingfive">
                              <div class="panel-body">
                                  <p>We offer the support of a dedicated engineering team for all areas of product development and testing. LAN Global works closely with 
                                      our OEM partners on co-developing customized software and script file settings, reflash/ rework projects and software resolutions. 
                                      We are able to provide the PRI, IRDB forms for your technical department to set your parameters.
                                      This allows LAN Global to customize 
                                      files to meet any  needs.</p>
                                  <div class="panel_inner_txt">
                                      <div class="Programming">
                                          <div class="heading">
                                          <h3>Programming</h3>
                                          </div>
                                          <p>LAN Global has the capability to program and  re-manufacture all brands of  phones including Apple, Samsung, LG, HTC, ZTE, and more. 
                                              Our team can support multiple projects and software loads simultaneously to minimize flash time and maximize output of customer orders.

                                          </p>
                                      </div>
                                      <div class="LabTesting">
                                          <div class="heading">
                                          <h3>LabTesting</h3>
                                          </div>
                                          <p>LAN Global tests handsets before submitting them to the carriers lab for both GSM & CDMA.
                                              We  work with the manufacturers, carriers and MVNOs in customize the handset S/W and hardware.</p>
                                      </div>
                                  </div>
                              </div>
                          </div>
                      </div>
                      <div class="panel panel-default">
                         <div class="panel-heading" role="tab" id="headingsix"> 
                                 <div class="title collapsed open_tab_we" data-role="title" data-toggle="collapse" data-parent="#accordion" href="#collapsesix" aria-expanded="false" aria-controls="collapsesix">
                                    <h5>reverse logistics</h5>
                                </div>
                            </div>
                              <div id="collapsesix" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingsix">
                              <div class="panel-body">
                                  <p>LAN Global can buy any excess inventory from Tier 1 U.S. carriers (used or new) and refurbish
                                      it as white label product for  customers and their agents.</p>
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
