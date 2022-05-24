using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;
//using avii.Classes;
using SV.Framework.Models.Inventory;
using SV.Framework.Inventory;

namespace avii.Admin
{
    public partial class ManageMslEsn : System.Web.UI.Page
    {
        // public bool EsnHeaderId { get; set; }
        private string fileStoreLocation = "~/UploadedData/ESNUpload/";
        private const char DELIMITER = ',';
        private MslOperation mslOperation = MslOperation.CreateInstance<MslOperation>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["adm"] == null)
            {
                string url = "/avii/logon.aspx";
                try
                {
                    url = ConfigurationSettings.AppSettings["LogonPage"].ToString();

                }
                catch
                {
                    url = "/avii/logon.aspx";
                }
                //if (Session["UserID"] == null)
                {
                    Response.Redirect(url);
                }
            }
            if (!IsPostBack)
            {
                string uploadAdmin = ConfigurationSettings.AppSettings["UploadAdmin"].ToString();
                avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
                List<avii.Classes.UserRole> userRoles = userInfo.ActiveRoles;
                if (userRoles != null && userRoles.Count > 0)
                {
                    var roles = (from item in userRoles where item.RoleName.Equals(uploadAdmin) select item).ToList();
                    if (roles != null && roles.Count > 0 && !string.IsNullOrEmpty(roles[0].RoleName))
                    {
                        ViewState["adminrole"] = roles[0].RoleName;
                        //pnlDate.Visible = true;
                    }
                    // else
                    //   pnlDate.Visible = false;
                }
                //pnlDate.Visible = false;

                BindCustomer();
                BindCategory();
                if (Session["ESN_HeaderID"] != null)
                {
                    // EsnHeaderId = true;
                    int esnHeaderId = Convert.ToInt32(Session["ESN_HeaderID"]);
                    ViewState["headerid"] = esnHeaderId;
                    GetEsnHeaderDetail(esnHeaderId);

                    Session["ESN_HeaderID"] = null;
                }
                else
                {
                    string OerderNo = mslOperation.GenerateOrderNumber();
                    string currentDate = DateTime.Now.ToShortDateString();
                    txtOrderDate.Text = currentDate;
                    txtShipDate.Text = currentDate;
                    txtOrderNumber.Text = OerderNo;
                    txtOrderNumber.Enabled = false;

                    if(Session["orderTransferID"] != null)
                    {
                        Int64 orderTransferID = Convert.ToInt64(Session["orderTransferID"]);
                        GetTransferOrderDetail(orderTransferID);
                        Session["orderTransferID"] = null;
                    }
                }
                //else
                //EsnHeaderId = false;
                //trSKU.Visible = true;
            }
        }
        private void GetTransferOrderDetail(Int64 orderTransferID)
        {
            SV.Framework.Inventory.TransferOrderOperation orderOperations = SV.Framework.Inventory.TransferOrderOperation.CreateInstance<SV.Framework.Inventory.TransferOrderOperation>();
            TransferOrder transferOrder = orderOperations.GetTransferOrderDetail(orderTransferID);
            int companyID = 0, orderQty = 0;
            ViewState["orderTransferID"] = orderTransferID;
            ViewState["IsESNRequired"] = 1;
            if (transferOrder != null)
            {
                if (Session["orderqty"] != null)
                {
                    orderQty = Convert.ToInt32(Session["orderqty"]);
                    txtOrderQty.Text = orderQty.ToString();
                    txtShipQty.Text = txtOrderQty.Text;
                }

                dpCompany.SelectedValue = transferOrder.CustomerInfo;
                dpCompany.Enabled = false;               

                string[] arr = transferOrder.CustomerInfo.Split(',');
                companyID = Convert.ToInt32(arr[0]);
                ViewState["CompanyAccountNumber"] = arr[1];
            }
            if (companyID > 0)
            {
                List<ItemCategory> categoryList = ViewState["categoryList"] as List<ItemCategory>;
                if (categoryList != null && categoryList.Count > 0)
                {
                    var category = (from item in categoryList where item.CategoryGUID.Equals(transferOrder.CategoryID) select item).ToList();
                    if (category != null && category.Count > 0)
                    {
                        ddlCategory.SelectedValue = category[0].CategoryWithProductAllowed;// transferOrder.CategoryID.ToString();
                        ddlCategory.Enabled = false;
                        //ddlCategory.SelectedIndex = 3;
                    }
                }
                BindCompanySKU(companyID);
                ddlSKU.SelectedValue = transferOrder.DestinationItemCompanyGUID.ToString();
                ddlSKU.Enabled = false;
            }
            else
            {
                //  trSKU.Visible = true;
                ddlSKU.DataSource = null;
                ddlSKU.DataBind();
            }
        
        }
        protected void GetEsnHeaderDetail(int esnHeaderId)
        {
            string sortExpression = "ESNHeaderId";
            string sortDirection = "DESC";
            ViewState["SortDirection"] = sortDirection;
            ViewState["SortExpression"] = sortExpression;

            EsnHeaders esnHeader = mslOperation.GetESNwithHeaderDetail(esnHeaderId);
            if (esnHeader != null)
            {
                btnClosew.Visible = true;
                lblCol.Visible = false;
                lblFilesize.Visible = false;
                dpCompany.SelectedValue = esnHeader.CompanyAccountNumber;

                string CustInfo = esnHeader.CompanyAccountNumber;
                if (CustInfo != null)
                {
                    string[] arr = CustInfo.Split(',');
                    if (arr.Length > 1)
                        ViewState["CompanyAccountNumber"] = arr[1];
                }
                BindCompanySKU(esnHeader.CompanyID);
                BindSKUs(esnHeader.CategoryID);

                ddlSKU.SelectedValue = esnHeader.ItemCompanyGUID.ToString();
               // ddlReceivedAs.SelectedValue = esnHeader.ReceivedAs;
                chkInspection.Checked = esnHeader.IsInspection;

                txtSuppliername.Text = esnHeader.SupplierName;
                txtComment.Text = esnHeader.CustomerOrderNumber;
                txtCustOrderNumber.Text = esnHeader.CustomerOrderNumber;
                txtOrderDate.Text = esnHeader.OrderDate;
                txtOrderNumber.Text = esnHeader.OrderNumber;
                txtReceiveDate.Text = esnHeader.OrderDate;
                txtReceiveBy.Text = esnHeader.UserName;
                txtOrderQty.Text = esnHeader.OrderQty.ToString();
                txtShipDate.Text = esnHeader.ShipDate;
                txtShipQty.Text = esnHeader.ShipQty.ToString();
                txtShipvia.Text = esnHeader.Shipvia;
                txtTrackingNo.Text = esnHeader.TrackingNumber;
                txtUnitPrice.Text = esnHeader.UnitPrice.ToString();
                ddlCategory.SelectedItem.Text = esnHeader.CategoryName;
                if (esnHeader.CategoryID == 16)
                {
                    ddlCategory.Visible = false;
                    ddlSKU.Visible = false;
                    lblCategory.Text = esnHeader.CategoryName;
                    lblSKU.Text = esnHeader.SKU;
                }
                dpCompany.Enabled = false;
                txtOrderNumber.Enabled = false;
                txtCustOrderNumber.Enabled = false;
                ddlCategory.Enabled = false;
                ddlSKU.Enabled = false;
                trReceivedate.Visible = true;

                List<EsnUploadNew> esnList = new List<EsnUploadNew>();
                esnList = esnHeader.EsnList;
                if (esnList != null && esnList.Count > 0)
                {
                    var delArr = (from item in esnList where item.InUse.Equals(false) select item).ToList();
                    if (delArr != null && delArr.Count > 0 && delArr[0].EsnID > 0)
                    {
                        btnDelete.Visible = true;
                    }
                    gvMSL.DataSource = esnList;
                    gvMSL.DataBind();
                    lblCount.Text = "Total count: " + esnList.Count;
                    txtShipQty.Text =  esnList.Count.ToString();

                    Session["mslesn"] = esnList;
                    // trQty.Visible = false;
                    txtShipQty.Enabled = false;
                    btnSubmit.Visible = false;
                    chkInspection.Visible = false;
                    btnSubmit2.Visible = false;
                    btnCancel.Visible = false;
                    btnCancel2.Visible = false;
                    pnlSubmit.Visible = false;
                    uploadDT.Visible = false;
                    btnUpload.Visible = false;
                    trESN1.Visible = false;
                    trESN2.Visible = false;
                    btnClose.Visible = true;

                }
            }
        }
        protected void GetEsnDetail(int esnHeaderId)
        {
            EsnHeaders esnHeader = mslOperation.GetESNwithHeaderDetail(esnHeaderId);
            if (esnHeader != null)
            {
                List<EsnUploadNew> esnList = new List<EsnUploadNew>();
                esnList = esnHeader.EsnList;
                if (esnList != null && esnList.Count > 0)
                {
                    gvMSL.DataSource = esnList;
                    gvMSL.DataBind();
                    lblCount.Text = "Total count: " + esnList.Count;
                    txtShipQty.Text = esnList.Count.ToString();

                    Session["mslesn"] = esnList;
                    //trQty.Visible = false;

                    //btnSubmit.Visible = false;
                    //btnSubmit2.Visible = false;
                    //btnCancel.Visible = false;
                    //btnCancel2.Visible = false;
                    //pnlSubmit.Visible = true;
                    //uploadDT.Visible = false;
                    //btnUpload.Visible = false;

                }
                else
                {
                    gvMSL.DataSource = null;
                    gvMSL.DataBind();
                    lblCount.Text = "";
                    Session["mslesn"] = null;
                    btnDelete.Enabled = false;
                }
            }
        }
        protected void BindCustomer()
        {
            dpCompany.DataSource = avii.Classes.clsCompany.GetCompany(0, 0);
            dpCompany.DataValueField = "CustInfo";
            dpCompany.DataTextField = "CompanyName";
            dpCompany.DataBind();

        }
        private void BindCategory()
        {
            //ProductController objProductController = new ProductController();
            //List<avii.Classes.ItemCategory> categoryList = objProductController.getItemCategoryTree(0, 0, 1, true, -1, -1, true, true, false, true);

            SV.Framework.Inventory.ProductController productController = SV.Framework.Inventory.ProductController.CreateInstance<SV.Framework.Inventory.ProductController>();
            List<ItemCategory> categoryList = productController.GetItemCategoryTree(0, 0, 1, true, -1, -1, true, true, false, true);

            ViewState["categoryList"] = categoryList;
            ddlCategory.DataSource = categoryList;
            ddlCategory.DataTextField = "categoryname";
            ddlCategory.DataValueField = "CategoryWithProductAllowed";
            ddlCategory.DataBind();
            ListItem li = new ListItem("--Select Category--", "0");
            ddlCategory.Items.Insert(0, li);
        }
        protected void dpCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            int companyID = 0;

            gvMSL.DataSource = null;
            gvMSL.DataBind();
            //rptESN.Visible = false;
            lblMsg.Text = string.Empty;
            lblConfirm.Text = string.Empty;

            btnSubmit.Visible = false;
            btnUpload.Visible = true;
            btnSubmit2.Visible = false;
            pnlSubmit.Visible = false;
            lblCount.Text = string.Empty;
            txtShipQty.Text = "";

            string CustInfo = string.Empty;
            //  trSKU.Visible = true;
            if (dpCompany.SelectedIndex > 0)
            {
                CustInfo = dpCompany.SelectedValue;
                string[] arr = CustInfo.Split(',');
                companyID = Convert.ToInt32(arr[0]);
                ViewState["CompanyAccountNumber"] = arr[1];
            }
            if (companyID > 0)
                BindCompanySKU(companyID);
            else
            {
                //  trSKU.Visible = true;
                ddlSKU.DataSource = null;
                ddlSKU.DataBind();
            }
        }
        protected void ESNDelete_click(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {
            string esn = Convert.ToString(e.CommandArgument);

            int returnRsult = mslOperation.ESNDelete(esn);

            if (returnRsult > 0 && ViewState["headerid"] != null)
            {
                GetEsnDetail(Convert.ToInt32(ViewState["headerid"]));
            }

            if (returnRsult > 0)
                lblMsg.Text = "Deleted successfully";
            else
                if (returnRsult == 0)
                lblMsg.Text = "ESN cannot be deleted";

        }
        private void BindSKUs(int categoryGUID)
        {
            int parentCategoryGUID = 0;
            List<CompanySKUno> skusList = new List<CompanySKUno>();

            if (categoryGUID > 0)
            {
                ddlSKU.Items.Clear();
                List<CompanySKUno> skuList = ViewState["skulist"] as List<CompanySKUno>;
                if (skuList != null && skuList.Count > 0)
                {

                    //ViewState["skulist"] = skuList;
                    ddlSKU.DataSource = skuList;
                    ddlSKU.DataValueField = "ItemcompanyGUID";
                    ddlSKU.DataTextField = "SKU";


                    ddlSKU.DataBind();
                    ListItem item = new ListItem("", "0");
                    ddlSKU.Items.Insert(0, item);
                }

            }


        }
        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            int categoryGUID = 0, parentCategoryGUID = 0;
            string CategoryWithProductAllowed = ddlCategory.SelectedValue;
            lblMsg.Text = string.Empty;
            ViewState["IsESNRequired"] = 0;
            trQty.Visible = true;
            txtShipQty.Enabled = true;
            if (ddlCategory.SelectedIndex > 0)
            {
                string[] array = CategoryWithProductAllowed.Split('|');
                categoryGUID = Convert.ToInt32(array[0]);
                if (Convert.ToInt32(array[0]) == 0)
                {
                    lblMsg.Text = "Please select leaf category!";
                    ddlCategory.SelectedIndex = 0;
                    return;
                }
            }

            List<CompanySKUno> skusList = new List<CompanySKUno>();
            //tblheader/.Visible = false;
            //tblesn.Visible = true;
            if (dpCompany.SelectedIndex > 0)
            {
                if (categoryGUID > 0)
                {
                    ddlSKU.Items.Clear();
                    if (ViewState["categoryList"] != null)
                    {
                        List<ItemCategory> categoryList = ViewState["categoryList"] as List<ItemCategory>;
                        if (categoryList != null)
                        {
                            var categoryInfo = (from item in categoryList where item.CategoryGUID.Equals(categoryGUID) select item).ToList();
                            if (categoryInfo != null && categoryInfo.Count > 0)
                            {
                                parentCategoryGUID = categoryInfo[0].ParentCategoryGUID;
                                //var itemInfo = null;

                                if (ViewState["skulist"] != null)
                                {
                                    List<CompanySKUno> skuList = ViewState["skulist"] as List<CompanySKUno>;
                                    if (skuList != null)
                                    {
                                        if (parentCategoryGUID == 0)
                                        {
                                            skusList = (from items in skuList where items.ParentCategoryGUID.Equals(categoryGUID) select items).ToList();
                                            if (skusList != null && skusList.Count > 0)
                                            { }
                                            else
                                                skusList = (from items in skuList where items.CategoryID.Equals(categoryGUID) select items).ToList();
                                        }
                                        else
                                            skusList = (from items in skuList where items.CategoryID.Equals(categoryGUID) select items).ToList();

                                        if (skusList != null && skusList.Count > 0)
                                        {
                                            if (!categoryInfo[0].IsESNRequired)
                                            {
                                                pnlHeader.Visible = true;
                                                pnlESN.Visible = false;

                                            }
                                            else
                                            {
                                                // trQty.Visible = false;
                                                txtShipQty.Enabled = false;
                                                ViewState["IsESNRequired"] = 1;
                                                pnlHeader.Visible = false;
                                                pnlESN.Visible = true;
                                            }


                                            //ViewState["skulist"] = skuList;
                                            ddlSKU.DataSource = skusList;
                                            ddlSKU.DataValueField = "ItemcompanyGUID";
                                            ddlSKU.DataTextField = "SKU";


                                            ddlSKU.DataBind();
                                            ListItem item = new ListItem("", "0");
                                            ddlSKU.Items.Insert(0, item);
                                        }
                                        else
                                        {
                                            //ViewState["skulist"] = null;
                                            ddlSKU.DataSource = null;
                                            ddlSKU.DataBind();
                                            lblMsg.Text = "No SKU(s) are assigned to selected Category";

                                        }
                                    }
                                }

                            }
                        }
                    }
                }

            }
            else
            {
                lblMsg.Text = "Please select customer!";
            }


        }
        protected void ddlSKU_SelectedIndexChanged(object sender, EventArgs e)
        {
            //    int ItemcompanyGUID = 0;
            //    if (ddlSKU.SelectedIndex > 0)
            //        ItemcompanyGUID = Convert.ToInt32(ddlSKU.SelectedValue);

            //    //tblheader/.Visible = false;
            //    //tblesn.Visible = true;

            //    if (ViewState["skulist"] != null)
            //    {
            //        List<CompanySKUno> skuList = ViewState["skulist"] as List<CompanySKUno>;
            //        if (skuList != null)
            //        {
            //            var skuByCategory = (from items in skuList where items.ItemcompanyGUID.Equals(ItemcompanyGUID) select items).ToList();
            //            if (skuByCategory != null && skuByCategory.Count > 0)
            //            {
            //                ddlSKU.DataSource = skuByCategory;
            //                ddlSKU.DataValueField = "ItemcompanyGUID";
            //                ddlSKU.DataTextField = "SKU";


            //                ddlSKU.DataBind();
            //                ListItem item = new ListItem("", "0");
            //                ddlSKU.Items.Insert(0, item);



            //                if (!skuByCategory[0].AllowESN)
            //                {
            //                    pnlHeader.Visible = true;
            //                    pnlESN.Visible = false;
            //                }
            //                else
            //                {
            //                     pnlHeader.Visible = false;
            //                     pnlESN.Visible = true;
            //                }
            //            }
            //            else
            //            {
            //                //ViewState["skulist"] = null;
            //                ddlSKU.DataSource = null;
            //                ddlSKU.DataBind();
            //                lblMsg.Text = "No SKU(s) are assigned to selected Category";

            //            }
            //        }
            //    }

        }
        private void BindCompanySKU(int companyID)
        {
            lblMsg.Text = string.Empty;
            List<CompanySKUno> skuList = mslOperation.GetCompanySKUs(companyID, 0);
            if (skuList != null)
            {
                ViewState["skulist"] = skuList;
                ddlSKU.DataSource = skuList;
                ddlSKU.DataValueField = "ItemcompanyGUID";
                ddlSKU.DataTextField = "SKU";


                ddlSKU.DataBind();
                ListItem item = new ListItem("", "0");
                ddlSKU.Items.Insert(0, item);
            }
            else
            {
                ViewState["skulist"] = null;
                ddlSKU.DataSource = null;
                ddlSKU.DataBind();
                lblMsg.Text = "No SKU are assigned to selected Customer";

            }


        }
        private void BindSKUByCategory(int companyID)
        {
            int ItemcompanyGUID = 0;
            if (ddlSKU.SelectedIndex > 0)
                ItemcompanyGUID = Convert.ToInt32(ddlSKU.SelectedValue);
            lblMsg.Text = string.Empty;
            //tblheader/.Visible = false;
            //tblesn.Visible = true;

            if (ViewState["skulist"] != null)
            {
                List<CompanySKUno> skuList = ViewState["skulist"] as List<CompanySKUno>;
                if (skuList != null)
                {
                    var itemInfo = (from items in skuList where items.ItemcompanyGUID.Equals(ItemcompanyGUID) select items).ToList();
                    if (itemInfo != null && itemInfo.Count > 0)
                    {
                        if (!itemInfo[0].AllowESN)
                        {
                            pnlHeader.Visible = true;
                            pnlESN.Visible = false;
                        }
                        else
                        {
                            pnlHeader.Visible = false;
                            pnlESN.Visible = true;
                        }
                    }
                    //List<CompanySKUno> skuList = MslOperation.GetCompanySKUs(companyID, 0);
                    if (skuList != null)
                    {
                        ViewState["skulist"] = skuList;
                        ddlSKU.DataSource = skuList;
                        ddlSKU.DataValueField = "ItemcompanyGUID";
                        ddlSKU.DataTextField = "SKU";


                        ddlSKU.DataBind();
                        ListItem item = new ListItem("", "0");
                        ddlSKU.Items.Insert(0, item);
                    }
                    else
                    {
                        ViewState["skulist"] = null;
                        ddlSKU.DataSource = null;
                        ddlSKU.DataBind();
                        lblMsg.Text = "No SKU(s) are assigned to selected Customer";

                    }
                }
            }






        }
        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvMSL.PageIndex = e.NewPageIndex;
            if (Session["mslesn"] != null)
            {
                List<EsnUploadNew> esnList = (List<EsnUploadNew>)Session["mslesn"];

                gvMSL.DataSource = esnList;
                gvMSL.DataBind();
            }
            else
                lblMsg.Text = "Session expire!";

        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string receivedAs = "Product without ASN";// ddlReceivedAs.SelectedValue;
            bool IsInspection = chkInspection.Checked;
            int itemCompanyGUID, insertCount, updateCount, esnHeaderId;
            Int64 orderTransferID=0;
            EsnHeaderUpload esnHeaderUpload = new EsnHeaderUpload();
            itemCompanyGUID = insertCount = updateCount = esnHeaderId = 0;
            bool returnValue = false;
            string errorMessage = string.Empty, companyAccountNumber = string.Empty;
            if (ViewState["orderTransferID"] != null)
                orderTransferID = Convert.ToInt64(ViewState["orderTransferID"]);

            if (ViewState["CompanyAccountNumber"] != null)
                companyAccountNumber = Convert.ToString(ViewState["CompanyAccountNumber"]);
            if (ViewState["headerid"] != null)
                esnHeaderId = Convert.ToInt32(ViewState["headerid"]);

            itemCompanyGUID = Convert.ToInt32(ddlSKU.SelectedValue);
            string filename = string.Empty;
            int uploadDateRange = 1095;
            uploadDateRange = Convert.ToInt32(ConfigurationSettings.AppSettings["UploadAdminDateRange"]);
            string uploaddate = string.Empty, orderNumber = string.Empty, custOrderNumber = string.Empty, orderDate = string.Empty, shipDate = string.Empty, shipVia = string.Empty, trackingNumber = string.Empty;
            string selectedSKU = ddlSKU.SelectedItem.Text;
            string[] skuAray = selectedSKU.Split('~');
            string SKU = skuAray[0].Trim();
            int orderQty = 0, shipQty = 0;
            decimal unitPrice = 0;
            DateTime uploadDate = DateTime.Now;
            uploaddate = uploadDate.ToShortDateString();

            string comment = txtComment.Text; if (ViewState["filename"] != null)
                filename = ViewState["filename"] as string;

            if (!string.IsNullOrEmpty(txtOrderNumber.Text))
                orderNumber = txtOrderNumber.Text.Trim();
            else
            { lblMsg.Text = "Order number is required"; return; }
            if (!string.IsNullOrEmpty(txtCustOrderNumber.Text))
                custOrderNumber = txtCustOrderNumber.Text.Trim();
            // else
            //{ lblMsg.Text = "Customer order number is required"; return; }
            if (!string.IsNullOrEmpty(txtOrderDate.Text))
                orderDate = txtOrderDate.Text.Trim();
            else
            { lblMsg.Text = "Order date is required"; return; }

            if (!string.IsNullOrEmpty(txtShipDate.Text))
                shipDate = txtShipDate.Text.Trim();
            else
            { lblMsg.Text = "Ship date is required"; return; }

            shipVia = txtShipvia.Text.Trim();
            trackingNumber = txtTrackingNo.Text.Trim();

            if (!string.IsNullOrEmpty(txtOrderQty.Text))
                orderQty = Convert.ToInt32(txtOrderQty.Text);

            //else
            //{ lblMsg.Text = "Order quantity is required"; return; }

            if (!string.IsNullOrEmpty(txtShipQty.Text))
                shipQty = Convert.ToInt32(txtShipQty.Text);
            else
            {
                if (Convert.ToInt32(ViewState["IsESNRequired"]) == 0)
                {
                    lblMsg.Text = "Received quantity is required";
                    return;
                }
            }  

            if (!string.IsNullOrEmpty(txtUnitPrice.Text))
                unitPrice = Convert.ToDecimal(txtUnitPrice.Text);
            esnHeaderUpload.Comments = comment;
            esnHeaderUpload.CustomerAccountNumber = companyAccountNumber;
            esnHeaderUpload.CustomerOrderNumber = custOrderNumber;
            esnHeaderUpload.ESNHeaderId = esnHeaderId;
            esnHeaderUpload.OrderDate = orderDate;
            esnHeaderUpload.OrderNumber = orderNumber;
            esnHeaderUpload.OrderQty = orderQty;
            esnHeaderUpload.ShipDate = shipDate;
            esnHeaderUpload.ShipQty = shipQty;
            esnHeaderUpload.Shipvia = shipVia;
            esnHeaderUpload.SKU = SKU;
            esnHeaderUpload.TrackingNumber = trackingNumber;
            esnHeaderUpload.UnitPrice = unitPrice;
            esnHeaderUpload.IsESNRequired = true;
            esnHeaderUpload.IsInspection = IsInspection;
            esnHeaderUpload.ReceivedAs = receivedAs;
            esnHeaderUpload.SupplierName = txtSuppliername.Text.Trim();

            int userID = 0;
            avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
            if (userInfo != null)
            {
                userID = userInfo.UserGUID;
            }
            esnHeaderUpload.UserId = userID;

            if (itemCompanyGUID > 0)
            {
                if (Session["mslesn"] != null)
                {

                    //if (ViewState["adminrole"] != null)
                    //{
                    //    DateTime uploadDatedt = DateTime.Now;
                    //    uploaddate = uploadDatedt.ToShortTimeString();
                    //    if (!string.IsNullOrEmpty(uploaddate))
                    //    {
                    //        if (uploaddate.Trim().Length > 0)
                    //        {
                    //            DateTime dt;
                    //            if (DateTime.TryParse(uploaddate.Trim(), out dt))
                    //            {
                    //                double days = (uploadDate - dt).TotalDays;
                    //                if (days > uploadDateRange)
                    //                    errorMessage = "UploadDate(" + dt.ToShortDateString() + ") can not be more than " + uploadDateRange + " days before";
                    //                else
                    //                    if (days < 0)
                    //                        errorMessage = "UploadDate(" + dt.ToShortDateString() + ") can not be more than today date";
                    //                    else
                    //                        if (days == 0)
                    //                            uploadDate = Convert.ToDateTime("01/01/1900");

                    //                uploadDate = dt;

                    //            }
                    //            else
                    //                errorMessage = "UploadDate(" + uploaddate + ") does not have correct format(MM/DD/YYYY)";

                    //            if (string.IsNullOrEmpty(errorMessage))
                    //            {
                    //                comment = comment + " Upload Date: " + uploadDate.ToShortDateString();
                    //            }

                    //        }
                    //        else
                    //            uploadDate = Convert.ToDateTime("01/01/1900");
                    //    }
                    //    else
                    //        uploadDate = Convert.ToDateTime("01/01/1900");
                    //}
                    //else
                    //{
                    //    uploadDate = Convert.ToDateTime("01/01/1900");
                    //}

                    uploadDate = DateTime.SpecifyKind(uploadDate, DateTimeKind.Unspecified);

                    if (string.IsNullOrEmpty(errorMessage))
                    {
                        List<EsnUploadNew> esnList = (Session["mslesn"]) as List<EsnUploadNew>;
                        esnHeaderUpload.ESNs = esnList;


                        returnValue = mslOperation.MslEsnInsertUpdateNew(esnHeaderUpload, filename, orderTransferID, out insertCount, out updateCount, out errorMessage);
                        if (returnValue)
                        {
                            if (esnHeaderId == 0)
                                ClearForm();
                            if (insertCount > 0 && updateCount > 0)
                                lblMsg.Text = insertCount + " records  successfully inserted. <br />" + updateCount + " records  successfully updated.";

                            if (insertCount > 0 && updateCount == 0)
                                lblMsg.Text = insertCount + " records  successfully inserted.";
                            if (insertCount == 0 && updateCount > 0)
                                lblMsg.Text = updateCount + " records  successfully updated.";

                        }
                        else
                            lblMsg.Text = errorMessage;
                    }
                    else
                        lblMsg.Text = errorMessage;
                }
                else
                    lblMsg.Text = "Session expire!";
            }
            else
                lblMsg.Text = "Please select SKU";
        }
        protected void btnHeaderSubmit_Click(object sender, EventArgs e)
        {
            int itemCompanyGUID, insertCount, updateCount, esnHeaderId;
            EsnHeaderUpload esnHeaderUpload = new EsnHeaderUpload();

            itemCompanyGUID = insertCount = updateCount = esnHeaderId = 0;
            bool returnValue = false;
            string errorMessage = string.Empty, companyAccountNumber = string.Empty;
            if (ViewState["CompanyAccountNumber"] != null)
                companyAccountNumber = Convert.ToString(ViewState["CompanyAccountNumber"]);
            if (ViewState["headerid"] != null)
                esnHeaderId = Convert.ToInt32(ViewState["headerid"]);
            if (ddlSKU.SelectedIndex == 0)
            { lblMsg.Text = "SKU is required"; return; }

            itemCompanyGUID = Convert.ToInt32(ddlSKU.SelectedValue);
            string filename = string.Empty;
            int uploadDateRange = 1095;
            uploadDateRange = Convert.ToInt32(ConfigurationSettings.AppSettings["UploadAdminDateRange"]);
            string uploaddate = string.Empty, orderNumber = string.Empty, custOrderNumber = string.Empty, orderDate = string.Empty, shipDate = string.Empty, shipVia = string.Empty, trackingNumber = string.Empty;
            string selectedSKU = ddlSKU.SelectedItem.Text;
            string[] skuAray = selectedSKU.Split('~');
            string SKU = skuAray[0].Trim();
            int orderQty = 0, shipQty = 0;
            decimal unitPrice = 0;
            DateTime uploadDate = DateTime.Now;
            uploaddate = uploadDate.ToShortDateString();

            string comment = txtComment.Text;
            if (!string.IsNullOrEmpty(txtOrderNumber.Text))
                orderNumber = txtOrderNumber.Text.Trim();
            else
            { lblMsg.Text = "Order number is required"; return; }
            if (!string.IsNullOrEmpty(txtCustOrderNumber.Text))
                custOrderNumber = txtCustOrderNumber.Text.Trim();
            //else
            //{ lblMsg.Text = "Customer order number is required"; return; }
            if (!string.IsNullOrEmpty(txtOrderDate.Text))
                orderDate = txtOrderDate.Text.Trim();
            else
            { lblMsg.Text = "Order date is required"; return; }

            if (!string.IsNullOrEmpty(txtShipDate.Text))
                shipDate = txtShipDate.Text.Trim();
            else
            { lblMsg.Text = "Ship date is required"; return; }

            shipVia = txtShipvia.Text.Trim();
            trackingNumber = txtTrackingNo.Text.Trim();

            if (!string.IsNullOrEmpty(txtOrderQty.Text))
                orderQty = Convert.ToInt32(txtOrderQty.Text);
            else
            { lblMsg.Text = "Order quantity is required"; return; }

            if (!string.IsNullOrEmpty(txtShipQty.Text))
                shipQty = Convert.ToInt32(txtShipQty.Text);
            else
            { lblMsg.Text = "Received quantity is required"; return; }

            if (!string.IsNullOrEmpty(txtUnitPrice.Text))
                unitPrice = Convert.ToDecimal(txtUnitPrice.Text);
            esnHeaderUpload.Comments = comment;
            esnHeaderUpload.CustomerAccountNumber = companyAccountNumber;
            esnHeaderUpload.CustomerOrderNumber = custOrderNumber;
            esnHeaderUpload.ESNHeaderId = esnHeaderId;
            esnHeaderUpload.OrderDate = orderDate;
            esnHeaderUpload.OrderNumber = orderNumber;
            esnHeaderUpload.OrderQty = orderQty;
            esnHeaderUpload.ShipDate = shipDate;
            esnHeaderUpload.ShipQty = shipQty;
            esnHeaderUpload.Shipvia = shipVia;
            esnHeaderUpload.SKU = SKU;
            esnHeaderUpload.TrackingNumber = trackingNumber;
            esnHeaderUpload.UnitPrice = unitPrice;
            esnHeaderUpload.IsESNRequired = false;
            int userID = 0;
            avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
            if (userInfo != null)
            {
                userID = userInfo.UserGUID;
            }
            esnHeaderUpload.UserId = userID;

            if (itemCompanyGUID > 0)
            {
                //if (Session["mslesn"] != null)
                {
                    if (ViewState["filename"] != null)
                        filename = ViewState["filename"] as string;


                    uploadDate = DateTime.SpecifyKind(uploadDate, DateTimeKind.Unspecified);

                    if (string.IsNullOrEmpty(errorMessage))
                    {
                        List<EsnUploadNew> esnList = new List<EsnUploadNew>();
                        esnHeaderUpload.ESNs = esnList;

                        returnValue = mslOperation.MslEsnInsertUpdateNew(esnHeaderUpload, filename, 0, out insertCount, out updateCount, out errorMessage);
                        if (returnValue)
                        {
                            if (esnHeaderId == 0)
                                ClearForm();
                            if (insertCount > 0 && updateCount > 0)
                                lblMsg.Text = insertCount + " records  successfully inserted. <br />" + updateCount + " records  successfully updated.";

                            if (insertCount > 0 && updateCount == 0)
                                lblMsg.Text = insertCount + " records  successfully inserted.";
                            if (insertCount == 0 && updateCount > 0)
                                lblMsg.Text = updateCount + " records  successfully updated.";

                        }
                        else
                            lblMsg.Text = errorMessage;
                    }
                    else
                        lblMsg.Text = errorMessage;
                }
                //else
                //    lblMsg.Text = "Session expire!";
            }
            else
                lblMsg.Text = "Please select SKU";
        }
        private void ClearForm()
        {
            txtCustOrderNumber.Text = string.Empty;
            // txtOrderDate.Text = string.Empty;
            txtOrderNumber.Text = mslOperation.GenerateOrderNumber();
            txtOrderQty.Text = string.Empty;
            // txtShipDate.Text = string.Empty;
            txtShipQty.Text = string.Empty;
            //   txtShipvia.Text = string.Empty;
            //  txtTrackingNo.Text = string.Empty;
            txtUnitPrice.Text = string.Empty;
            ddlSKU.Items.Clear();
            txtComment.Text = string.Empty;

            //trSKU.Visible = true;
            dpCompany.SelectedIndex = 0;
            ddlCategory.SelectedIndex = 0;
            gvMSL.DataSource = null;
            gvMSL.DataBind();
            //rptESN.Visible = false;
            lblMsg.Text = string.Empty;
            lblConfirm.Text = string.Empty;

            btnSubmit.Visible = false;
            btnUpload.Visible = true;
            btnSubmit2.Visible = false;
            pnlSubmit.Visible = false;
            lblCount.Text = string.Empty;
            txtShipQty.Text = "";
            pnlHeader.Visible = false;
            pnlESN.Visible = true;

        }
        protected void btnValidateUploadedFile_Click(object sender, EventArgs e)
        {
            ValidateMslESN();
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            if (ViewState["headerid"] != null)
                Response.Redirect("ManageMslEsn.aspx");
            else
                ClearForm();
        }
        // Function to test for Positive Integers with zero inclusive   
        public bool IsWholeNumber(String strNumber)
        {
            Regex objNotWholePattern = new Regex("[^0-9]");
            return !objNotWholePattern.IsMatch(strNumber);
        }
        private void ValidateMslESN()
        {
            gvMSL.DataSource = null;
            gvMSL.DataBind();
            string receivelength = ConfigurationSettings.AppSettings["receivelength"].ToString();
            string[] lenArray = receivelength.Split(',');
            int esnMaxLength = 0, batchMaxLen = 0, hexMaxLen = 0, decMaxLen=0, locationMaxlen = 0, serialNoMaxLen = 0, boxIDMaxLen=0;
            
            int.TryParse(lenArray[0], out batchMaxLen);
            int.TryParse(lenArray[1], out esnMaxLength);
            int.TryParse(lenArray[2], out hexMaxLen);
            int.TryParse(lenArray[3], out decMaxLen);
            int.TryParse(lenArray[4], out locationMaxlen);

            //int.TryParse(lenArray[5], out otkslMaxLen);
            //int.TryParse(lenArray[6], out mslMaxlen);

            int.TryParse(lenArray[5], out serialNoMaxLen);
            int.TryParse(lenArray[6], out boxIDMaxLen);

            int itemCompanyGUID = 0;
            bool isLTE = false;
            bool isSim = false;
            int returnValue = 0;
            bool IsOrderNumber = false;            
            string dateErrorMessage = string.Empty;            
            lblConfirm.Text = string.Empty;
            txtShipQty.Text = "";
            lblCount.Text = string.Empty;
            lblMsg.Text = string.Empty;
            Hashtable hshESN = new Hashtable();
            int uploadDateRange = 730;
            uploadDateRange = Convert.ToInt32(ConfigurationSettings.AppSettings["UploadAdminDateRange"]);

            string uploaddate = string.Empty, orderNumber = string.Empty, custOrderNumber = string.Empty, orderDate = string.Empty, shipDate = string.Empty, shipVia = string.Empty, trackingNumber = string.Empty;
            int orderQty = 0, shipQty = 0;
            decimal unitPrice = 0;
            DateTime uploadDate = DateTime.Now;
            uploaddate = uploadDate.ToShortTimeString();

            bool columnsIncorrectFormat = false;

            if (!string.IsNullOrEmpty(txtOrderNumber.Text))
                orderNumber = txtOrderNumber.Text.Trim();
            else
            { lblMsg.Text = "Order number is required"; return; }
            if (!string.IsNullOrEmpty(txtCustOrderNumber.Text))
                custOrderNumber = txtCustOrderNumber.Text.Trim();
            
            if (!string.IsNullOrEmpty(txtOrderDate.Text))
                orderDate = txtOrderDate.Text.Trim();
            else
            { lblMsg.Text = "Order date is required"; return; }

            if (!string.IsNullOrEmpty(txtShipDate.Text))
                shipDate = txtShipDate.Text.Trim();
            else
            { lblMsg.Text = "Ship date is required"; return; }

            shipVia = txtShipvia.Text.Trim();
            trackingNumber = txtTrackingNo.Text.Trim();

            if (!string.IsNullOrEmpty(txtShipQty.Text))
                shipQty = Convert.ToInt32(txtShipQty.Text);
            else
            {
                if (Convert.ToInt32(ViewState["IsESNRequired"]) == 0)
                {
                    lblMsg.Text = "Received quantity is required";
                    return;
                }
            }

            if (!string.IsNullOrEmpty(txtUnitPrice.Text))
                unitPrice = Convert.ToDecimal(txtUnitPrice.Text);

            itemCompanyGUID = Convert.ToInt32(ddlSKU.SelectedValue);
            if (itemCompanyGUID > 0)
            {                
                try
                {
                    if (flnUpload.PostedFile.FileName.Trim().Length == 0)
                    {
                        lblMsg.Text = "Select file to upload";
                    }
                    else
                    {
                        if (flnUpload.PostedFile.ContentLength > 0)
                        {
                            string fileName = UploadFile();
                            string extension = Path.GetExtension(flnUpload.PostedFile.FileName);
                            extension = extension.ToLower();
                            string invalidColumns = string.Empty;
                            System.Text.StringBuilder sbInvalidColumns = new System.Text.StringBuilder();
                            System.Text.StringBuilder sbESN = new System.Text.StringBuilder();
                            System.Text.StringBuilder sbESN2 = new System.Text.StringBuilder();
                            System.Text.StringBuilder sbHex = new System.Text.StringBuilder();
                            System.Text.StringBuilder sbDec = new System.Text.StringBuilder();
                            System.Text.StringBuilder sbErrors = new System.Text.StringBuilder();
                            EsnUploadNew assignESN = null;
                            List<EsnUploadNew> esnList = new List<EsnUploadNew>();

                             if (extension == ".csv")
                            {
                                try
                                {
                                    if (ViewState["adminrole"] != null)
                                    {
                                        // uploaddate = txtUploadDate.Text.Trim();
                                        if (!string.IsNullOrEmpty(uploaddate))
                                        {
                                            if (uploaddate.Trim().Length > 0)
                                            {
                                                DateTime dt;
                                                if (DateTime.TryParse(uploaddate.Trim(), out dt))
                                                {
                                                    double days = (uploadDate - dt).TotalDays;
                                                    if (days > uploadDateRange)
                                                        dateErrorMessage = "UploadDate(" + dt.ToShortDateString() + ") can not be more than " + uploadDateRange + " days before";
                                                    else
                                                        if (days < 0)
                                                        dateErrorMessage = "UploadDate(" + dt.ToShortDateString() + ") can not be more than today date";
                                                    else
                                                            if (days == 0)
                                                        uploadDate = Convert.ToDateTime("01/01/1900");

                                                    uploadDate = dt;
                                                }
                                                else
                                                    dateErrorMessage = "UploadDate(" + uploaddate + ") does not have correct format(MM/DD/YYYY)";
                                            }
                                            else
                                                uploadDate = Convert.ToDateTime("01/01/1900");
                                        }
                                        else
                                            uploadDate = Convert.ToDateTime("01/01/1900");
                                    }
                                    else
                                    {
                                        uploadDate = Convert.ToDateTime("01/01/1900");
                                    }

                                    uploadDate = DateTime.SpecifyKind(uploadDate, DateTimeKind.Unspecified);
                                    if (string.IsNullOrEmpty(dateErrorMessage))
                                    {
                                        using (StreamReader sr = new StreamReader(fileName))
                                        {
                                            string line;
                                            string esn, batch, lteICCID, MeidHex, MeidDec, Location,  SerialNumber, BoxID, IMEI2;//, uploaddate;
                                            int i = 0;
                                            while ((line = sr.ReadLine()) != null)
                                            {
                                                //uploadDate = DateTime.Now;
                                                if (!string.IsNullOrEmpty(line) && i == 0)
                                                {
                                                    i = 1;
                                                    line = line.ToLower();

                                                    string[] headerArray = line.Split(',');
                                                    if (headerArray.Length == 2 || headerArray.Length <= 8)
                                                    {
                                                        if (headerArray[0].Trim() != "batch")
                                                        {
                                                            invalidColumns = headerArray[0];
                                                            columnsIncorrectFormat = true;
                                                        }

                                                        if (headerArray[1].Trim() != "esn1")
                                                        {
                                                            sbInvalidColumns.Append(headerArray[1] + ",");

                                                            columnsIncorrectFormat = true;
                                                        }

                                                        if (headerArray.Length > 2 && headerArray[2].Trim() != "" && headerArray[2].Trim() != "esn2")
                                                            {
                                                                sbInvalidColumns.Append(headerArray[2] + ",");

                                                                columnsIncorrectFormat = true;
                                                            }
                                                        //if (headerArray.Length > 2 && headerArray[2].Trim() != "" && headerArray[2].Trim() != "meidhex")
                                                            //{
                                                            //    sbInvalidColumns.Append(headerArray[2] + ",");

                                                            //    columnsIncorrectFormat = true;
                                                            //}
                                                            if (headerArray.Length > 3 && headerArray[3].Trim() != "" && headerArray[3].Trim() != "meidhex")
                                                            {
                                                                sbInvalidColumns.Append(headerArray[3] + ",");

                                                                columnsIncorrectFormat = true;
                                                            }
                                                            if (headerArray.Length > 4 && headerArray[4].Trim() != "" && headerArray[4].Trim() != "meiddec")
                                                            {
                                                                sbInvalidColumns.Append(headerArray[4] + ",");

                                                                columnsIncorrectFormat = true;
                                                            }

                                                            //if (headerArray.Length > 5 && headerArray[5].Trim() != "" && headerArray[5].Trim() != "msl")
                                                            //{
                                                            //    sbInvalidColumns.Append(headerArray[5] + ",");

                                                            //    //if (string.IsNullOrEmpty(invalidColumns))
                                                            //    //    invalidColumns = headerArray[5];
                                                            //    //else
                                                            //    //    invalidColumns = invalidColumns + ", " + headerArray[5];
                                                            //    columnsIncorrectFormat = true;
                                                            //}
                                                            //if (headerArray.Length > 6 && headerArray[6].Trim() != "" && headerArray[6].Trim() != "otksl")
                                                            //{
                                                            //    sbInvalidColumns.Append(headerArray[6] + ",");

                                                            //    //if (string.IsNullOrEmpty(invalidColumns))
                                                            //    //    invalidColumns = headerArray[6];
                                                            //    //else
                                                            //    //    invalidColumns = invalidColumns + ", " + headerArray[6];
                                                            //    columnsIncorrectFormat = true;
                                                            //}

                                                            if (headerArray.Length > 5 && headerArray[5].Trim() != "" && headerArray[5].Trim() != "location")
                                                            {
                                                                sbInvalidColumns.Append(headerArray[5] + ",");

                                                                columnsIncorrectFormat = true;
                                                            }
                                                            if (headerArray.Length > 6 && headerArray[6].Trim() != "" && headerArray[6].Trim() != "serialnumber")
                                                            {
                                                                sbInvalidColumns.Append(headerArray[6] + ",");

                                                                columnsIncorrectFormat = true;
                                                            }
                                                            if (headerArray.Length > 7 && headerArray[7].Trim() != "" && headerArray[7].Trim() != "boxid")
                                                            {
                                                                sbInvalidColumns.Append(headerArray[7] + ",");

                                                                columnsIncorrectFormat = true;
                                                            }

                                                            invalidColumns = sbInvalidColumns.ToString();
                                                        }
                                                    else
                                                        {
                                                            columnsIncorrectFormat = true;
                                                            invalidColumns = string.Empty;
                                                        }
                                                    }
                                                    else if (!string.IsNullOrEmpty(line) && i > 0)
                                                    {
                                                        esn = batch = lteICCID = MeidHex = MeidDec = Location = SerialNumber = BoxID = IMEI2 = string.Empty;
                                                        string[] arr = line.Split(',');
                                                    try
                                                    {
                                                        assignESN = new EsnUploadNew();
                                                        batch = arr[0].Trim();
                                                        esn = arr[1].Trim();
                                                        if (!string.IsNullOrEmpty(esn) && !IsWholeNumber(esn))
                                                        {
                                                            sbESN.Append(esn + ",");
                                                        }
                                                        if (arr.Length > 2)
                                                        {
                                                            IMEI2 = arr[2].Trim();
                                                            ViewState["IMEI2"] = 1;
                                                            if (!string.IsNullOrEmpty(IMEI2) && !IsWholeNumber(IMEI2))
                                                            {
                                                                sbESN2.Append(IMEI2 + ",");
                                                            }
                                                        }
                                                        if (arr.Length > 3)
                                                        {
                                                            MeidHex = arr[3].Trim();
                                                            ViewState["HEX"] = 1;
                                                            if (!string.IsNullOrEmpty(MeidHex) && !IsWholeNumber(MeidHex))
                                                            {
                                                                sbHex.Append(MeidHex + ",");
                                                            }
                                                        }
                                                        if (arr.Length > 4)
                                                        {
                                                            MeidDec = arr[4].Trim();
                                                            if (!string.IsNullOrEmpty(MeidDec) && !IsWholeNumber(MeidDec))
                                                            {
                                                                sbDec.Append(MeidDec + ",");
                                                            }
                                                        }
                                                        if (arr.Length > 5)
                                                        {
                                                            Location = arr[5].Trim();
                                                        }
                                                        //if (arr.Length > 5)
                                                        //{
                                                        //    MSL = arr[5].Trim();
                                                        //}
                                                        //if (arr.Length > 6)
                                                        //{
                                                        //    OTKSL = arr[6].Trim();
                                                        //}

                                                        if (arr.Length > 6)
                                                        {
                                                            SerialNumber = arr[6].Trim();
                                                        }
                                                        if (arr.Length > 7)
                                                        {
                                                            BoxID = arr[7].Trim();
                                                        }
                                                        if (string.IsNullOrEmpty(esn))
                                                        {

                                                        }
                                                        else
                                                        {
                                                            assignESN.ESN = esn;
                                                            assignESN.MslNumber = batch;
                                                            assignESN.ICC_ID = lteICCID;
                                                            assignESN.MeidHex = MeidHex;
                                                            assignESN.MeidDec = MeidDec;
                                                            assignESN.Location = Location;
                                                            assignESN.MSL = "";
                                                            assignESN.OTKSL = "";
                                                            assignESN.SerialNumber = SerialNumber;
                                                            assignESN.BoxID = BoxID;
                                                            assignESN.IMEI2 = IMEI2;

                                                            if (string.IsNullOrEmpty(esn) || string.IsNullOrEmpty(batch))
                                                            {
                                                                if (string.IsNullOrEmpty(esn) && string.IsNullOrEmpty(batch))
                                                                    lblMsg.Text = "Missing BATCH & ESN data";
                                                                else
                                                                    if (string.IsNullOrEmpty(esn))
                                                                    lblMsg.Text = "Missing ESN data";
                                                                else
                                                                        if (string.IsNullOrEmpty(batch))
                                                                    lblMsg.Text = "Missing BATCH data";
                                                            }
                                                            if (batch.Length > batchMaxLen)
                                                            {
                                                                lblMsg.Text = "BATCH# length cannot be greater than " + batchMaxLen;
                                                            }
                                                            if (esn.Length > esnMaxLength)
                                                            {
                                                                lblMsg.Text = "ESN1 length cannot be greater than " + esnMaxLength;
                                                            }
                                                            if (IMEI2.Length > esnMaxLength)
                                                            {
                                                                lblMsg.Text = "ESN2 length cannot be greater than " + esnMaxLength;
                                                            }
                                                            if (MeidHex.Length > hexMaxLen)
                                                            {
                                                                lblMsg.Text = "Hex length cannot be greater than " + hexMaxLen;
                                                            }
                                                            if (MeidDec.Length > decMaxLen)
                                                            {
                                                                lblMsg.Text = "Dec length cannot be greater than " + decMaxLen;
                                                            }
                                                            if (Location.Length > locationMaxlen)
                                                            {
                                                                lblMsg.Text = "Location length cannot be greater than " + locationMaxlen;
                                                            }
                                                            if (SerialNumber.Length > serialNoMaxLen)
                                                            {
                                                                lblMsg.Text = "Serial number length cannot be greater than " + serialNoMaxLen;
                                                            }
                                                            if (BoxID.Length > boxIDMaxLen)
                                                            {
                                                                lblMsg.Text = "BoxID length cannot be greater than " + boxIDMaxLen;
                                                            }

                                                            if (hshESN.ContainsKey(esn) && !string.IsNullOrEmpty(esn))
                                                            {
                                                                sbErrors.Append(esn + ",");
                                                                assignESN.ErrorMessage = "Duplicate ESN1";
                                                                lblMsg.Text = sbErrors.ToString() + " duplicate ESN1(s) exists in the file";
                                                            }
                                                            else if (!hshESN.ContainsKey(esn) && !string.IsNullOrEmpty(esn))
                                                            {
                                                                hshESN.Add(esn, esn);
                                                            }

                                                            if (hshESN.ContainsKey(IMEI2) && !string.IsNullOrEmpty(IMEI2))
                                                            {
                                                                sbErrors.Append(IMEI2 + ",");
                                                                assignESN.ErrorMessage = "Duplicate ESN2";
                                                                lblMsg.Text = sbErrors.ToString() + " duplicate ESN2(s) exists in the file";
                                                            }
                                                            else if (!hshESN.ContainsKey(IMEI2) && !string.IsNullOrEmpty(IMEI2))
                                                            {
                                                                hshESN.Add(IMEI2, IMEI2);
                                                            }

                                                            if (!string.IsNullOrEmpty(MeidHex))
                                                            {
                                                                if (hshESN.ContainsKey(MeidHex) && !string.IsNullOrEmpty(MeidHex))
                                                                {
                                                                    assignESN.ErrorMessage = "Duplicate MeidHex";
                                                                    lblMsg.Text = "Duplicate MeidHex(s) exists in the file";
                                                                }
                                                                else if (!hshESN.ContainsKey(MeidHex) && !string.IsNullOrEmpty(MeidHex))
                                                                {
                                                                    hshESN.Add(MeidHex, MeidHex);
                                                                }
                                                            }
                                                            if (!string.IsNullOrEmpty(MeidDec))
                                                            {
                                                                if (hshESN.ContainsKey(MeidDec) && !string.IsNullOrEmpty(MeidDec))
                                                                {
                                                                    assignESN.ErrorMessage = "Duplicate MeidDec";
                                                                    lblMsg.Text = "Duplicate MeidDec(s) exists in the file";
                                                                }
                                                                else if (!hshESN.ContainsKey(MeidDec) && !string.IsNullOrEmpty(MeidDec))
                                                                {
                                                                    hshESN.Add(MeidDec, MeidDec);
                                                                }
                                                            }
                                                            if (!string.IsNullOrEmpty(SerialNumber))
                                                            {
                                                                if (hshESN.ContainsKey(SerialNumber) && !string.IsNullOrEmpty(SerialNumber))
                                                                {
                                                                    assignESN.ErrorMessage = "Duplicate Serial#";
                                                                    lblMsg.Text = SerialNumber + " Duplicate SerialNumber(s) exists in the file";
                                                                }
                                                                else if (!hshESN.ContainsKey(SerialNumber) && !string.IsNullOrEmpty(SerialNumber))
                                                                {
                                                                    hshESN.Add(SerialNumber, SerialNumber);
                                                                }
                                                            }
                                                            esnList.Add(assignESN);
                                                            esn = string.Empty;
                                                            MeidHex = string.Empty;
                                                            MeidDec = string.Empty;
                                                            Location = string.Empty;
                                                            //MSL = string.Empty;
                                                            //OTKSL = string.Empty;
                                                            SerialNumber = string.Empty;
                                                            BoxID = string.Empty;
                                                            IMEI2 = string.Empty;
                                                        }
                                                    }
                                                    catch (ApplicationException ex)
                                                    {
                                                        throw ex;
                                                    }
                                                    catch (Exception exception)
                                                    {
                                                        lblMsg.Text = exception.Message;
                                                    }
                                                    }
                                            }
                                            sr.Close();
                                        }
                                    }
                                    else
                                        lblMsg.Text = dateErrorMessage;
                                }
                                catch (Exception ex)
                                {
                                    lblMsg.Text = ex.Message;
                                }
                                if(esnList != null && esnList.Count > 0)
                                {
                                    var emptySerialNos = (from item in esnList where item.SerialNumber.Equals(string.Empty) select item).ToList();
                                    {
                                        if(emptySerialNos != null && emptySerialNos.Count > 0)
                                        {
                                            lblMsg.Text = "Serial# cannot be empty!";
                                        }
                                    }
                                    var emptyBoxIds = (from item in esnList where item.BoxID.Equals(string.Empty) select item).ToList();
                                    {
                                        if (emptyBoxIds != null && emptyBoxIds.Count > 0)
                                        {
                                            lblMsg.Text = "BoxID(s) cannot be empty!";
                                        }
                                    }
                                    var emptyLocations = (from item in esnList where item.Location.Equals(string.Empty) select item).ToList();
                                    {
                                        if (emptyLocations != null && emptyLocations.Count > 0)
                                        {
                                            lblMsg.Text = "Location(s) cannot be empty!";
                                        }
                                    }
                                }
                                if (lblMsg.Text == "")
                                {
                                    if (esnList != null && esnList.Count > 0 && columnsIncorrectFormat == false)
                                    {
                                        gvMSL.DataSource = esnList;
                                        gvMSL.DataBind();
                                        lblCount.Text = "Total count: " + esnList.Count;
                                        txtShipQty.Text = esnList.Count.ToString();
                                        Session["mslesn"] = esnList;

                                        int n = 0;
                                        //int poRecordCount = 0;
                                        string poESNquarantine = string.Empty,
                                         poErrorMessage = string.Empty,
                                         poSimMessage = string.Empty,
                                         duplicateESN = string.Empty,
                                         errorMessage = string.Empty,
                                         poEsnMessage = string.Empty,
                                         poESNBoxIDs = string.Empty,
                                         poLOcations = string.Empty;
                                        System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
                                        double totalChunk = 0;
                                        try
                                        {
                                            totalChunk = (double)esnList.Count / 5000;
                                            n = Convert.ToInt16(Math.Ceiling(totalChunk));
                                            int esnCount = 5000;
                                            //var esnToUpload;
                                            for (int i = 0; i < n; i++)
                                            {
                                                poESNquarantine = poErrorMessage = duplicateESN = poSimMessage = poEsnMessage = poLOcations = poESNBoxIDs = string.Empty;
                                                isLTE = false;
                                                isSim = false;
                                                returnValue = 0;
                                                //esnToAdd = new List<FulfillmentAssignESN>();
                                                if (esnList.Count < 5000)
                                                    esnCount = esnList.Count;
                                                var esnToUpload = (from item in esnList.Take(esnCount) select item).ToList();

                                                //Upload/Assign ESN to POs
                                                mslOperation.MslESNs_ValidateNew1(esnToUpload, itemCompanyGUID, orderNumber, 
                                                    out poErrorMessage, out duplicateESN, out poSimMessage, out isLTE, out IsOrderNumber, 
                                                    out returnValue, out poEsnMessage, out poESNquarantine, out poESNBoxIDs, out poLOcations);

                                                if (!string.IsNullOrEmpty(poLOcations))
                                                {
                                                    stringBuilder.Append(poLOcations + " <br /> ");
                                                }
                                                if (!string.IsNullOrEmpty(poESNBoxIDs))
                                                {
                                                    stringBuilder.Append(poESNBoxIDs + " <br /> ");
                                                }
                                                if (!string.IsNullOrEmpty(poErrorMessage))
                                                {
                                                    stringBuilder.Append(poErrorMessage + " ESN(s) can not re-assign because already assigned to service order" + " <br /> ");
                                                }

                                                if (!string.IsNullOrEmpty(poEsnMessage))
                                                {
                                                    stringBuilder.Append(poEsnMessage  + " <br /> ");
                                                //    stringBuilder.Append(poEsnMessage + " ESN(s) can not re-assign because already assigned to fulfillment order" + " <br /> ");
                                                }

                                                if (!string.IsNullOrEmpty(poESNquarantine))
                                                {
                                                    stringBuilder.Append(poESNquarantine + " ESN(s) can not re-assign because already quarantine is issued " + " <br /> ");
                                                }
                                                if (!string.IsNullOrEmpty(poSimMessage))
                                                {
                                                    stringBuilder.Append(poSimMessage + " <br /> ");
                                                }
                                                if (!string.IsNullOrEmpty(duplicateESN))
                                                {                                                    
                                                    stringBuilder.Append(duplicateESN + " <br /> ");
                                                }
                                                if (IsOrderNumber)
                                                {
                                                    stringBuilder.Append(orderNumber + " already exists!" + " <br /> ");
                                                }
                                                errorMessage = stringBuilder.ToString();
                                            }

                                        }
                                        catch (Exception ex)
                                        {
                                            lblMsg.Text = ex.Message;
                                        }
                                        if (!string.IsNullOrEmpty(errorMessage))
                                        {
                                            lblMsg.Text = errorMessage;
                                            return;
                                        }
                                        errorMessage = sbESN.ToString();
                                        if (!string.IsNullOrEmpty(errorMessage))
                                        {
                                            lblMsg.Text = errorMessage + " ESN(s) having value other than numbers";
                                            return;
                                        }
                                        errorMessage = sbHex.ToString();
                                        if (!string.IsNullOrEmpty(errorMessage))
                                        {
                                            lblMsg.Text = errorMessage + " Hex(s) having value other than numbers"; ;
                                            return;
                                        }
                                        errorMessage = sbDec.ToString();
                                        if (!string.IsNullOrEmpty(errorMessage))
                                        {
                                            lblMsg.Text = errorMessage + " Dec(s) having value other than numbers"; ;
                                            return;
                                        }
                                        if (lblMsg.Text == string.Empty)
                                        {
                                            //lblMsg.CssClass = "errorGreenMsg";
                                            lblConfirm.Text = "ESN file is ready to upload";
                                            btnUpload.Visible = false;
                                            btnSubmit.Visible = true;
                                            btnSubmit2.Visible = true;
                                            pnlSubmit.Visible = true;
                                        }
                                        else
                                        {
                                            btnUpload.Visible = true;
                                            btnSubmit.Visible = false;
                                            btnSubmit2.Visible = false;
                                            pnlSubmit.Visible = false;
                                        }
                                    }
                                    else
                                    {
                                        gvMSL.DataSource = null;
                                        gvMSL.DataBind();
                                        if (!string.IsNullOrEmpty(dateErrorMessage))
                                        {
                                            lblMsg.Text = dateErrorMessage;
                                            btnUpload.Visible = true;
                                            btnSubmit.Visible = false;
                                            btnSubmit2.Visible = false;
                                            pnlSubmit.Visible = false;
                                        }
                                        else
                                        {
                                            if (esnList != null && esnList.Count == 0)
                                            {
                                                if (columnsIncorrectFormat)
                                                {
                                                    if (!string.IsNullOrEmpty(invalidColumns))
                                                        lblMsg.Text = invalidColumns + " column(s) name is not correct";
                                                    else
                                                        lblMsg.Text = "File format is not correct";
                                                }
                                                else
                                                    lblMsg.Text = "There is no ESN to upload";
                                            }
                                            if (esnList != null)
                                            {
                                                if (columnsIncorrectFormat)
                                                {
                                                    //lblMsg.Text = invalidColumns + " column(s) name is not correct";
                                                    if (!string.IsNullOrEmpty(invalidColumns))
                                                        lblMsg.Text = invalidColumns + " column(s) name is not correct";
                                                    else
                                                        lblMsg.Text = "File format is not correct";
                                                }
                                                else
                                                    lblMsg.Text = "There is no ESN to upload";
                                            }
                                        }
                                    }
                                }
                            }
                            else
                                lblMsg.Text = "Invalid file!";
                        }
                        else
                            lblMsg.Text = "Invalid file!";
                    }
                }
                catch (Exception ex)
                {
                    lblMsg.Text = ex.Message;
                }
            }
            else
                lblMsg.Text = "Please select SKU";
        }
        private string UploadFile()
        {
            string actualFilename = string.Empty;
            Int32 maxFileSize = 3145728;
            actualFilename = System.IO.Path.GetFileName(flnUpload.PostedFile.FileName);
            if (ConfigurationManager.AppSettings["ESNUploadFilesStoreage"] != null)
            {
                fileStoreLocation = ConfigurationManager.AppSettings["ESNUploadFilesStoreage"].ToString();
            }

            fileStoreLocation = Server.MapPath(fileStoreLocation);

            if (File.Exists(fileStoreLocation + actualFilename))
            {
                actualFilename = System.Guid.NewGuid().ToString() + actualFilename;
            }

            flnUpload.PostedFile.SaveAs(fileStoreLocation + actualFilename);


            ViewState["filename"] = actualFilename;

            FileInfo fileInfo = new FileInfo(fileStoreLocation + actualFilename);

            if (ConfigurationManager.AppSettings["maxCSVfilesize"] != null)
            {
                if (Int32.TryParse(ConfigurationManager.AppSettings["maxCSVfilesize"].ToString(), out maxFileSize))
                {
                    if (fileInfo.Length > maxFileSize)
                    {
                        fileInfo.Delete();
                        throw new Exception("File size is greater than 3 MB");// + maxFileSize + " bytes");
                    }
                }
            }



            return fileStoreLocation + actualFilename;
        }
        protected void gvMSL_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (ViewState["headerid"] != null)
                {
                    gvMSL.Columns[0].Visible = true;
                    //gvMSL.Columns[5].Visible = false;
                }
            }
            if (e.Row.RowType == DataControlRowType.Header)
            {
                ((CheckBox)e.Row.FindControl("allchk")).Attributes.Add("onclick",
                    "javascript:SelectAll('" +
                    ((CheckBox)e.Row.FindControl("allchk")).ClientID + "')");
            }
            //else
            //{
            //    gvMSL.Columns[4].Visible = true;
            //    gvMSL.Columns[5].Visible = true;
            //}
        }
        protected void lnkDownload_Click(object sender, EventArgs e)
        {
            GenerateCSV();
        }
        private void GenerateCSV()
        {
            lblMsg.Text = string.Empty;

            string string2CSV = "BATCH,ESN1,ESN2,MeidHex,MeidDec,Location,SerialNumber,BoxID" + Environment.NewLine;

            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=InventoryReceive.csv");
            Response.Charset = "";
            Response.ContentType = "application/text";
            Response.Output.Write(string2CSV);
            Response.Flush();
            Response.End();

        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            List<EsnUploadNew> esnList = new List<EsnUploadNew>();
            EsnUploadNew esnData = null;
            foreach (GridViewRow row in gvMSL.Rows)
            {
                HiddenField hdEsn = row.FindControl("hdEsn") as HiddenField;
                CheckBox chkesn = row.FindControl("chkesn") as CheckBox;
                if(chkesn.Checked)
                {
                    esnData = new EsnUploadNew();
                    esnData.ESN = hdEsn.Value;
                    esnList.Add(esnData);
                }

            }
            if(esnList != null && esnList.Count > 0)
            {
                int returnRsult = mslOperation.ESNDelete(esnList);

                if (returnRsult > 0 && ViewState["headerid"] != null)
                {
                    GetEsnDetail(Convert.ToInt32(ViewState["headerid"]));
                }

                if (returnRsult > 0)
                { lblMsg.Text = "Deleted successfully"; 
                
                }
                else
                    if (returnRsult == 0)
                    lblMsg.Text = "ESN cannot be deleted";
            }
            else
            {
                lblMsg.Text = "Please select at least one ESN";
            }
        }
        private string GetSortDirection(string column)
        {
            string sortDirection = "ASC";
            string sortExpression = ViewState["SortExpression"] as string;
            if (sortExpression != null)
            {
                if (sortExpression == column)
                {
                    string lastDirection = ViewState["SortDirection"] as string;
                    if ((lastDirection != null) && (lastDirection == "ASC"))
                    {
                        sortDirection = "DESC";
                    }
                }
            }
            ViewState["SortDirection"] = sortDirection;
            ViewState["SortExpression"] = column;
            return sortDirection;
        }
        public List<EsnUploadNew> Sort<TKey>(List<EsnUploadNew> list, string sortBy, SortDirection direction)
        {
            PropertyInfo property = list.GetType().GetGenericArguments()[0].GetProperty(sortBy);
            if (direction == SortDirection.Ascending)
            {
                return list.OrderBy(e => property.GetValue(e, null)).ToList<EsnUploadNew>();
            }
            else
            {
                return list.OrderByDescending(e => property.GetValue(e, null)).ToList<EsnUploadNew>();
            }
        }
        protected void gvMSL_Sorting(object sender, GridViewSortEventArgs e)
        {
            string Sortdir = GetSortDirection(e.SortExpression);
            string SortExp = e.SortExpression;

            if (Session["mslesn"] != null)
            {
                List<EsnUploadNew> esnHeadersList = (List<EsnUploadNew>)Session["mslesn"];

                if (esnHeadersList != null && esnHeadersList.Count > 0)
                {
                    //var list = shipments;
                    if (Sortdir == "ASC")
                    {
                        esnHeadersList = Sort<EsnUploadNew>(esnHeadersList, SortExp, SortDirection.Ascending);
                    }
                    else
                    {
                        esnHeadersList = Sort<EsnUploadNew>(esnHeadersList, SortExp, SortDirection.Descending);
                    }
                    Session["mslesn"] = esnHeadersList;

                    gvMSL.DataSource = esnHeadersList;
                    gvMSL.DataBind();
                }
            }
        }
        protected void lnkBox_Command(object sender, CommandEventArgs e)
        {
            string esn = Convert.ToString(e.CommandArgument);
            Session["esn"] = esn;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "temp", "<script language='javascript'>OpenNewPage('../esn/esnboxidedit.aspx')</script>", false);

        }
        protected void btnLocation_Click(object sender, EventArgs e)
        {
            // int companyID = 0;
            if (dpCompany.SelectedIndex > 0)
            {
                Session["whsearch"] = "" + "," + "" + "," + dpCompany.SelectedValue;
                Session["wh"] = 1;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "temp", "<script language='javascript'>OpenNewPage('../warehouse/warehousesearch.aspx')</script>", false);
            }
            else
                lblMsg.Text = "Customer is required!";
        }
    }
}