<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="menuControl.ascx.cs" Inherits="avii.dhtmlxmenu.menuControl"  %>
 	<meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <link  id="custStyle"   rel="stylesheet" type="text/css" runat="server" />

    <!-- Bootstrap CSS -->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css">
	<link href="https://maxcdn.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" rel="stylesheet">

<%--    <link rel="stylesheet" href="../css/bootstrap.min.css">
	<link href="../css/font-awesome.min.css" rel="stylesheet">--%>

	<link rel="stylesheet"  id="custMenu" runat="server" type="text/css"  />
	<%--<link rel="stylesheet" href="../css/animate.min.css">--%>
	<%--<script src="https://www.google.com/recaptcha/api.js" async defer></script> href="../css/stylenewmenu.css"--%>
    
	
   <style>
              /*body {
                padding-top: 60px;
                padding-bottom: 40px;
              }*/
              .alignleft
              {
                  float:left; 
                  width:100%;
              }
              
              .sidebar-nav {
                padding: 9px 0;
              }
              .navbar_start .navbar-nav li {
                  padding-left: 15px;
              }
              .dropdown-menu .sub-menu {
                left: 100%;
                position: absolute;
                top: 10%;
                visibility: hidden;
                margin-top: -1px;
                width:180px !important;
              }
              
              .dropdown-menu li:hover .sub-menu {
                visibility: visible;
              
              }
              
              .dropdown:hover .dropdown-menu {
                display: block;
               
                /*width:200px !important;*/
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

              /* My Css */

              .custom-dropmenu .nav-item.dropdown ul.dropdown-menu li{
                padding-left: 0px;
              }
              .custom-dropmenu .nav-item.dropdown ul.dropdown-menu li a{
                padding: 3px 15px !important;
              }
              .custom-dropmenu .nav-item.dropdown ul.dropdown-menu li.dropdown-submenu {
                  position: relative;
              }
              .custom-dropmenu .nav-item.dropdown ul.dropdown-menu li.dropdown-submenu .dropdown-item:after{
                display: none;
              }
              .custom-dropmenu .nav-item.dropdown ul.dropdown-menu li.dropdown-submenu ul.dropdown-menu {
                  position: absolute;
                  left: 157px !important;
                  top: 0;
                  opacity: 0;
              }
              .custom-dropmenu .nav-item.dropdown ul.dropdown-menu li.dropdown-submenu:hover  ul.dropdown-menu{
                opacity: 1;
              }


            </style> 
<body onload="initMenu();">
<section class="upper_part">
    <div class="user_links res_on">
            <div class="upper_user_logo">
                <a href="../logout.aspx"> <%--<img class="home_pg" src="../img/user_logo.png">--%>
				 
				 <img src="../img/user_logo2.png"></a>
            </div>
          </div>
          <header>
            <div class="head">
                <div class="container-fluid">
                    <div class="row">
                        <div class="col-md-3 col-6 wid_0">
                            <div class="logo">
                                <%--<a href="../Index.aspx"><img src="../img/logo_2nd.png"></a>style="float:left"--%>
                                 <a href="../logon.aspx"><img src="../img/<% =LogoPath %>"></a>
                            </div>
                        </div>
                         <div class="col-md-9 col-12 pad_0">
                            <div class="navbar_start alignleft" >
                                <nav class="navbar navbar-expand-md navbar-light">
                                  <a class="navbar-brand" href="#">&nbsp;</a>
                                  <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
                                    <span class="navbar-toggler-icon"></span>
                                  </button>

                                  <div class="collapse navbar-collapse" id="navbarSupportedContent">
                                    
                                  </div>
                                </nav>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </header>
        </section>
     <div class="clearfix"></div>
    
    <asp:HiddenField ID="hdnurl" runat="server" />
  <asp:HiddenField ID="hdnmenu" runat="server" />
  <div runat="server" id="a1">
    <script type="text/javascript">

        var menu;
        function initMenu() {
            
           
            var div = document.getElementById("navbarSupportedContent");
            var menu_content = document.getElementById("<%=hdnmenu.ClientID %>").value;
           
             div.innerHTML = menu_content;
        
        }
     
    </script>
    </div>
   </body>
   
   
    