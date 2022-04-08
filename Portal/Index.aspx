<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="avii.Index" %>

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
	<%--<script src="https://www.google.com/recaptcha/api.js" async defer></script>--%>
    <script src="https://www.google.com/recaptcha/api.js?onload=onloadCallback&render=explicit" async defer type="text/javascript"> </script>
<script type="text/javascript">
    var onloadCallback = function () {
        grecaptcha.render('recaptcha', {
            'sitekey': '6LeblVwUAAAAAHlIzyOktPVtXLzBpP09h2159D-T'
        });
    };
</script>
	
   <title>Lan Global</title>
    
  <%-- <style>

      

.sidebar-nav {
  padding: 9px 0;
}

.dropdown-menu .sub-menu {
  left: 100%;
  position: absolute;
  top: 0;
  visibility: hidden;
  margin-top: -1px;
}

.dropdown-menu li:hover .sub-menu {
  visibility: visible;
}

.dropdown:hover .dropdown-menu {
  display: block;
}

.nav-tabs .dropdown-menu,
.nav-pills .dropdown-menu,
.navbar .dropdown-menu {
  margin-top: 0;
}

.navbar .sub-menu:before {
  border-bottom: 7px solid transparent;
  border-left: none;
  border-right: 7px solid rgba(0, 0, 0, 0.2);
  border-top: 7px solid transparent;
  left: -7px;
  top: 10px;
}

.navbar .sub-menu:after {
  border-top: 6px solid transparent;
  border-left: none;
  border-right: 6px solid #fff;
  border-bottom: 6px solid transparent;
  left: 10px;
  top: 11px;
  left: -6px;
}

       .navbarli {
           color:#808080 !important;font-size:14px;font-weight: 400;text-transform: uppercase;padding: 0 !important;transition: all ease 0.6s;outline: none !important;line-height: 26px;
       }
   </style>--%>
</head>
<body class="about edit_who_we_are">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <section class="upper_part">
          <div class="user_links res_on">
            <div class="upper_user_logo">
                <a href="Logon.aspx">
                 <img src="img/user_logo.png" />

                </a>
            </div>
          </div>
            <header>
                <div class="head">
                    <div class="container-fluid">
                        <div class="row">
                            <div class="col-md-12 col-12 wid_0">
                                <div class="logo">
                                    <a href="index.aspx">
                                        <img src="img/logo_2nd.png"></a>
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

                                        <div class="collapse navbar-collapse" id="navbarSupportedContent" style="z-index: 3;">
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
                                                <li class="nav-item">
                                                    <a class="nav-link" target="_blank" href="../documents/A1776 generic User Manual 20201218.docx">A1776</a>
                                                 </li>
                                                <li class="nav-item user_none">
                                                    <a href="logon.aspx">
                                                        <img src="img/user_logo2.png"></a>
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
      

        <div id="carouselExampleControls" class="carousel slide" data-ride="carousel">
		  <div class="carousel-inner"> 
			<%--<div class="carousel-item active upper_part" style="background-image:url('img/compliance.jpg')">
				<div class="cstm_container">
					<div class="banner_txt_inner wow fadeIn">
					  <h1></h1>
					</div>
				</div>
			</div>--%>
              <div class="carousel-item active upper_part" style="background-image:url('img/bg_1.jpg')">
				<div class="cstm_container">
					<div class="banner_txt_inner wow fadeIn">
					  <h1>solutions to empower<br> your growth</h1>
					</div>
				</div>
			</div>
			<div class="carousel-item upper_part" style="background-image:url('img/bg_2.jpg')">
				<div class="cstm_container">
					<div class="banner_txt_inner wow fadeIn">
					  <h1>solutions to empower<br> your growth</h1>
					</div>
				</div>
			</div>
            
            			<div class="carousel-item upper_part" style="background-image:url('img/bg3.jpg')">
				<div class="cstm_container">
					<div class="banner_txt_inner wow fadeIn">
					  <h1>solutions to empower<br> your growth</h1>
					</div>
				</div>
			</div>
            
            			<div class="carousel-item upper_part" style="background-image:url('img/bg4.jpg')">
				<div class="cstm_container">
					<div class="banner_txt_inner wow fadeIn">
					  <h1>solutions to empower<br> your growth</h1>
					</div>
				</div>
			</div>
            
            			<div class="carousel-item upper_part" style="background-image:url('img/bg5.jpg')">
				<div class="cstm_container">
					<div class="banner_txt_inner wow fadeIn">
					  <h1>solutions to empower<br> your growth</h1>
					</div>
				</div>
			</div>
            
            			<div class="carousel-item upper_part" style="background-image:url('img/bg6.jpg')">
				<div class="cstm_container">
					<div class="banner_txt_inner wow fadeIn">
					  <h1>solutions to empower<br> your growth</h1>
					</div>
				</div>
			</div>
		  </div>
		   <div class="slider_arrow">
			<a class="carousel-control-prev" href="#carouselExampleControls" role="button" data-slide="prev">
				<span class="carousel-control" aria-hidden="true"><img class="left_side" src="img/arrow-left.png"></span>
				<span class="sr-only">Previous</span>
			  </a>
			  <a class="carousel-control-next" href="#carouselExampleControls" role="button" data-slide="next">
				<span class="carousel-control" aria-hidden="true"><img src="img/arrow-right.png"></span>
				<span class="sr-only">Next</span>
			  </a></div>
		</div>
      </section>
      <div class="clearfix"></div>
      <section class="overview">
        <div class="cstm_container">
            <div class="overview_inner wow fadeInLeft">
                <h4>AN OVERVIEW</h4>
                <p><%--LANGLOBAL provides infrastructure asset recovery and data destruction services that go above and beyond what other companies in the industry offer. Over the past decade we have become one of the largest and most proficient companies providing these services in the nation. Companies large and small have come to rely on our professional teams, advanced resources, and flexible services to handle the recovery or disposal of their cell phone batteries, mobile accessories, and repurpose these back into the market for reuse.--%>
                    <%--Over the past two decades we have grown to become one of the largest and most proficient companies to provide these services in the U.S. and abroad. Private business and cell phone carriers rely on Lan Global to handle the recovery or disposal of their cell phones, batteries and mobile accessories. We, in turn, repurpose these back into the market for reuse.--%>
                    LAN Global provides infrastructure asset recovery and data destruction services for cell phones, cell phone batteries 
                    and mobile accessories which are then repurposed back into the market for reuse.
                    

                <%--</p>
                <p>--%>
                    Over the past two decades we have become one of the industry’s largest and proficient 
                    companies to provide these services in the U.S. and abroad due to our 
                    <%--<a href="compliance.aspx#headingThree">ISO 9001:2008</a>, --%>
                    <a href="compliance.aspx#headingThree">ISO 14001:2015</a>, 
                    <a href="compliance.aspx#headingThree">ISO 45001:2018 </a>, and 
                    <a href="compliance.aspx#headingThree">R2v3</a> certifications. These adhere us to a strict zero e-waste policy.
                </p>
                <p>
                    As industry leaders, LAN Global is dedicated to maintaining 100 percent green processes throughout our production facility. We are an EPA Waste Handler approved agency as well as a Waste Wise Endorser. 
                <%--</p>
                <p>--%>
                    Businesses large and small have come to rely on our professional teams, advanced resources, and flexible services to handle the recovery or disposal of their mobile products.

                </p>
                <p>
                    LAN Global  is audited regularly by certified 3rd party registrars to ensure compliance with all industry requirements.  
                </p>
                <a style="float:left;" href="about.aspx">Read more <span><i class="fa fa-chevron-right" aria-hidden="true"></i></span></a>            </div>
        </div>
      </section>
      <div class="clearfix"></div>
      <section class="works_block">
          <div class="works_block_inner">
              <ul>
                  <li class="bg_1 wow fadeInLeft">
				    <div class="product-blog">
				        <div class="product-circle">
				            <div class="inner-product">
				                <div class="middle-product">
								    <a href="compliance.aspx"><img src="img/compliance.png" alt="icon"></a>
								 </div>
				            </div>
				        </div>
				        <h3><a href="compliance.aspx">COMPLIANCE</a></h3>
				    </div>
				  </li>
                  <li class="bg_2 wow fadeInLeft">
				    <div class="product-blog">
				        <div class="product-circle">
				            <div class="inner-product">
				                <div class="middle-product">
								    <a href="whatwedo.aspx"><img src="img/logistics.png" alt="icon"></a>
								 </div>
				            </div>
				        </div>
				        <h3><a href="whatwedo.aspx">REVERSE LOGISTICS</a></h3>
				    </div>
				  </li>
                  <li class="bg_3 wow fadeInRight">
				    <div class="product-blog">
				        <div class="product-circle">
				            <div class="inner-product">
				                <div class="middle-product">
								     <a href="partnership.aspx"><img src="img/partnerships.png" alt="icon"></a>
								 </div>
				            </div>
				        </div>
				        <h3><a href="partnership.aspx">PARTNERSHIPS</a></h3>
				    </div>
				  </li>
                  <li class="bg_4 wow fadeInRight">
				    <div class="product-blog">
				        <div class="product-circle">
                            <div class="inner-product">
				                <div class="middle-product">
				                <a href="whatwedo.aspx"><img src="img/security.png" alt="icon"></a>
								 </div>
				            </div>
				        </div>
				        <h3><a href="whatwedo.aspx">DATA SECURITY</a></h3>
				    </div>
				  </li>
                   <li class="bg_5 wow fadeInLeft">
				    <div class="product-blog">
				        <div class="product-circle">
                            <div class="inner-product">
				                <div class="middle-product">
								    <a href="whatwedo.aspx"><img src="img/chain.png" alt="icon"></a>
								 </div>
				            </div>
				        </div>
				        <h3><a href="whatwedo.aspx">SUPPLY CHAIN</a></h3>
				    </div>
				  </li>
                   <li class="bg_6 wow fadeInLeft">
				    <div class="product-blog">
				        <div class="product-circle">
                            <div class="inner-product">
				                <div class="middle-product">
								    <a href="integration.aspx"><img src="img/inegrations.png" alt="icon"></a>
								 </div>
				            </div>
				        </div>
				        <h3><a href="integration.aspx">INTEGRATIONS</a></h3>
				    </div>
				  </li> 
                   <li class="bg_7 wow fadeInRight">
				    <div class="product-blog">
				        <div class="product-circle">
                            <div class="inner-product">
				                <div class="middle-product">
								    <a href="whatwedo.aspx"><img src="img/management.png" alt="icon"></a>
								 </div>
				            </div>
				        </div>
				        <h3><a href="whatwedo.aspx">ASSET MANAGEMENT</a></h3>
				    </div>
				  </li>
                   <li class="bg_8 wow fadeInRight">
				    <div class="product-blog">
				        <div class="product-circle">
                            <div class="inner-product">
				                <div class="middle-product">
                                     <a href="compliance.aspx"><img src="img/recycling.png" alt="icon"></a>
								 </div>
				            </div>
				        </div>
				        <h3><a href="compliance.aspx">RECYCLING</a></h3>
				    </div>
				  </li>
              </ul>
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
