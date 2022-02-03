<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MenuForm.aspx.cs" Inherits="avii.MenuForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <!-- Required meta tags -->
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <!-- Bootstrap CSS -->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css">
	<link href="https://maxcdn.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" rel="stylesheet">
	<link rel="stylesheet" href="css/stylenewmenu.css">
	<link rel="stylesheet" href="css/animate.min.css">
	<%--<script src="https://www.google.com/recaptcha/api.js" async defer></script>--%>
    
	
    <title>Lan Global</title>
   <style>

       /*body {
  padding-top: 60px;
  padding-bottom: 40px;
}*/

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
   </style>
</head>
<body>
    <form id="form1" runat="server">
        <section class="upper_part">
          <header>
            <div class="head">
                <div class="container-fluid">
                    <div class="row">
                        <div class="col-md-4 col-6 wid_0">
                            <div class="logo">
                                <a href="Index.aspx"><img src="img/logo.png"></a>
                            </div>
                        </div>
                        <div class="col-md-8 col-12 pad_0">
                            <div class="navbar_start">
                                <nav class="navbar navbar-expand-md navbar-light">
                                  <a class="navbar-brand" href="#">&nbsp;</a>
                                  <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
                                    <span class="navbar-toggler-icon"></span>
                                  </button>

                                  <div class="collapse navbar-collapse" id="navbarSupportedContent">
                                    <ul class='navbar-nav mr-auto'><li class='dropdown'><a href='#' class='dropdown-toggle' data-toggle='dropdown'>ACL<b class='caret'></b></a> <ul class='dropdown-menu'><li><a  href='accessmanagement/addmodules.aspx'> Manage Modules</a></li><li><a  href='accessmanagement/managemodule.aspx'> Modules Query</a></li><li><a  href='accessmanagement/permissions.aspx'> Manage Permissions</a></li><li><a  href='accessmanagement/rolemgmt.aspx'> Role Management</a></li><li><a  href='accessmanagement/rolequery.aspx'> Role Query</a></li></ul></li><li class='dropdown'><a href='#' class='dropdown-toggle' data-toggle='dropdown'>Users<b class='caret'></b></a> <ul class='dropdown-menu'><li><a  href='accessmanagement/userquery.aspx'> User Query</a></li><li><a  href='accessmanagement/users.aspx'> Manage Users</a></li></ul></li><li class='dropdown'><a href='#' class='dropdown-toggle' data-toggle='dropdown'>Vendors<b class='caret'></b></a> <ul class='dropdown-menu'><li><a href='#' >OEM<i class='caret'></i></a> <ul class='dropdown-menu sub-menu'><li><a  href='OEM/ManageOEM.aspx'> Manage OEM</a></li><li><a  href='OEM/OEMQuery.aspx'> OEM Query</a></li></ul></li><li><a href='#' >Customer<i class='icon-arrow-right'></i></a> <ul class='dropdown-menu sub-menu'><li><a  href='Company/Customer-form.aspx'> Manage Customer</a></li><li><a  href='Company/CustomerQuery.aspx'> Customer Query</a></li></ul></li></ul></li><li class='dropdown'><a href='#' class='dropdown-toggle' data-toggle='dropdown'>Forecast<b class='caret'></b></a> <ul class='dropdown-menu'><li><a  href='Forecast/CreateForecast.aspx'> Create Forecast</a></li><li><a  href='Forecast/ForecastQuery.aspx'> Forecast Query</a></li></ul></li><li class='dropdown'><a href='#' class='dropdown-toggle' data-toggle='dropdown'>Product<b class='caret'></b></a> <ul class='dropdown-menu'><li><a  href='product/detail-product.aspx'> Manage Products</a></li><li><a  href='product/manage-product.aspx'> Products Query</a></li><li><a  href='product/finalskus.aspx'> Finished SKU</a></li><li><a  href='Product/ManageCarriers.aspx'> Manage Carrier</a></li><li><a  href='product/SKUPricesApprove.aspx'> Approve SKU Pricing</a></li><li><a  href='product/AssignSKUPrice.aspx'> SKU Pricing</a></li></ul></li><li class='dropdown'><a href='#' class='dropdown-toggle' data-toggle='dropdown'>Fulfillment<b class='caret'></b></a> <ul class='dropdown-menu'><li><a  href='../NewPO.aspx'> Create Fulfillment Order</a></li><li><a  href='./POChangeStatus.aspx'> Fulfillment Status Update(Bulk)</a></li><li><a  href='POQuerynew.aspx'> Fulfillment Query</a></li><li><a  href='admin/ManageMSL.aspx'> Re-Assign Provisioning</a></li></ul></li><li class='dropdown'><a href='#' class='dropdown-toggle' data-toggle='dropdown'>Reports<b class='caret'></b></a> <ul class='dropdown-menu'><li><a href='#' >Fulfillment Report<i class='icon-arrow-right'></i></a> <ul class='dropdown-menu sub-menu'><li><a  href='Reports/FulfillmentTrackingReport.aspx'> Shipment (Tracking)</a></li></ul></li><li><a href='#' >Inventory Report<i class='icon-arrow-right'></i></a> <ul class='dropdown-menu sub-menu'><li><a  href='Reports/ReassignSkuReport.aspx'> ESN Reassign</a></li><li><a  href='Reports/CustomerEsnRepositoryDetail.aspx'> ESN Repository Detail</a></li><li><a  href='Reports/CustomerRmaEsnSummary.aspx'> RMA ESN Listing</a></li></ul></li><li><a  href='admin/frmMSL.aspx'> ESN Query</a></li><li><a href='#' >Summary<i class='icon-arrow-right'></i></a> <ul class='dropdown-menu sub-menu'><li><a  href=''> Fulfillments</a></li><li><a  href=''> Products</a></li><li><a  href=''> RMA's</a></li></ul></li></ul></li><li class='dropdown'><a href='#' class='dropdown-toggle' data-toggle='dropdown'>RMA<b class='caret'></b></a> <ul class='dropdown-menu'><li><a  href='RMA/RMAChangeStatus.aspx'> Returns (RMA) Status Update (Bulk)</a></li><li><a  href='rma/NewRMAForm.aspx?mode=esn'> Manage Returns (RMA)</a></li><li><a  href='rma/NewRMAQuery.aspx'> Search Returns(RMA)</a></li></ul></li><li class='dropdown'><a href='#' class='dropdown-toggle' data-toggle='dropdown'>Upload<b class='caret'></b></a> <ul class='dropdown-menu'><li><a  href='RMA/RmaUpload.aspx'> Bulk RMA</a></li><li><a  href='admin/ManageEsn.aspx'> ESN Clean Up</a></li><li><a href='#' >ESN Repository<i class='icon-arrow-right'></i></a> <ul class='dropdown-menu sub-menu'><li><a  href='BadEsnupload.aspx'> Bad ESN</a></li><li><a  href='admin/managemslesn.aspx'> Create/Update ESN(s) Respository</a></li></ul></li><li><a href='#' >SIM Repository<i class='icon-arrow-right'></i></a> <ul class='dropdown-menu sub-menu'><li><a  href='Admin/ManageSim.aspx'> Create/Update SIM(s) Repository</a></li></ul></li><li><a href='#' >Fulfillment Orders<i class='icon-arrow-right'></i></a> <ul class='dropdown-menu sub-menu'><li><a  href='admin/FulfillmentAVSONumber.aspx'> AVSO - Assignment</a></li><li><a  href='Admin/FulfillmentUpdate.aspx'> Cancel & Closed</a></li><li><a  href='Admin/FulfillmentUpload.aspx'> Create</a></li><li><a  href='admin/potrackingupload.aspx'> Multiple Shipment</a></li><li><a  href='admin/AssignTracking.aspx'> Tracking - Assignment</a></li><li><a  href='Admin/AssignESn.aspx'> ESN Assignment</a></li></ul></li></ul></li><li class='dropdown'><a href='#' class='dropdown-toggle' data-toggle='dropdown'>Utility<b class='caret'></b></a> <ul class='dropdown-menu'><li><a  href='Dashboard.aspx'> Dashboard</a></li><li><a  href='frmErrorLog.aspx'> Error Log</a></li><li><a  href='admin/UploadPOData.aspx'> Forms Uploads</a></li><li><a  href='Logout.aspx'> Signout</a></li></ul></li><li class='nav-item user_none'><a href = 'Logon.aspx' ><img src = 'img/user_logo2.png' /></a></li></ul>
                                  </div>
                                </nav>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </header>
        </section>
    </form>
</body>
</html>
