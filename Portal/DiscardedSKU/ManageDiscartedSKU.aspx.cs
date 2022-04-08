using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SV.Framework.Models.Inventory;
using SV.Framework.Inventory;
using System.Configuration;

namespace avii.DiscardedSKU
{
    public partial class ManageDiscartedSKU : System.Web.UI.Page
    {
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
                BindCustomer();
               // BindCategory();
            }

        }
        protected void dpCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            int companyID = 0;

            //gvMSL.DataSource = null;
            //gvMSL.DataBind();
            lblMsg.Text = string.Empty;
            //lblConfirm.Text = string.Empty;

           // btnSubmit.Visible = false;
            //btnUpload.Visible = true;
            // btnSubmit2.Visible = false;
            // pnlSubmit.Visible = false;
            //lblCount.Text = string.Empty;
            string CustInfo = string.Empty;
            //  trSKU.Visible = true;
            if (dpCompany.SelectedIndex > 0)
            {
                companyID = Convert.ToInt32(dpCompany.SelectedValue);

            }
            if (companyID > 0)
            {
                BindCompanySKU(companyID, true);
                BindUser();
            }
            else
            {
                //  trSKU.Visible = true;
                ddlSKU.DataSource = null;
                ddlSKU.DataBind();
            }
        }

        //protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    int categoryGUID = 0, parentCategoryGUID = 0;
        //    string CategoryWithProductAllowed = ddlCategory.SelectedValue;
        //    lblMsg.Text = string.Empty;
        //    ViewState["IsESNRequired"] = 0;
        //    //trQty.Visible = true;
        //    // txtShipQty.Enabled = true;
        //    if (ddlCategory.SelectedIndex > 0)
        //    {
        //        string[] array = CategoryWithProductAllowed.Split('|');
        //        categoryGUID = Convert.ToInt32(array[0]);
        //        if (Convert.ToInt32(array[0]) == 0)
        //        {
        //            lblMsg.Text = "Please select leaf category!";
        //            ddlCategory.SelectedIndex = 0;
        //            return;
        //        }
        //    }

        //    List<CompanySKUno> skusList = new List<CompanySKUno>();
        //    //tblheader/.Visible = false;
        //    //tblesn.Visible = true;
        //    if (dpCompany.SelectedIndex > 0)
        //    {
        //        if (categoryGUID > 0)
        //        {
        //            ddlSKU.Items.Clear();
        //            if (ViewState["categoryList"] != null)
        //            {
        //                List<ItemCategory> categoryList = ViewState["categoryList"] as List<ItemCategory>;
        //                if (categoryList != null)
        //                {
        //                    var categoryInfo = (from item in categoryList where item.CategoryGUID.Equals(categoryGUID) select item).ToList();
        //                    if (categoryInfo != null && categoryInfo.Count > 0)
        //                    {
        //                        parentCategoryGUID = categoryInfo[0].ParentCategoryGUID;
        //                        //var itemInfo = null;

        //                        if (ViewState["skulist"] != null)
        //                        {
        //                            List<CompanySKUno> skuList = ViewState["skulist"] as List<CompanySKUno>;
        //                            if (skuList != null)
        //                            {
        //                                if (parentCategoryGUID == 0)
        //                                {
        //                                    skusList = (from items in skuList where items.ParentCategoryGUID.Equals(categoryGUID) select items).ToList();
        //                                    if (skusList != null && skusList.Count > 0)
        //                                    { }
        //                                    else
        //                                        skusList = (from items in skuList where items.CategoryID.Equals(categoryGUID) select items).ToList();
        //                                }
        //                                else
        //                                    skusList = (from items in skuList where items.CategoryID.Equals(categoryGUID) select items).ToList();

        //                                if (skusList != null && skusList.Count > 0)
        //                                {                                            

        //                                    //ViewState["skulist"] = skuList;
        //                                    ddlSKU.DataSource = skusList;
        //                                    ddlSKU.DataValueField = "ItemcompanyGUID";
        //                                    ddlSKU.DataTextField = "SKU";


        //                                    ddlSKU.DataBind();
        //                                    ListItem item = new ListItem("", "0");
        //                                    ddlSKU.Items.Insert(0, item);
        //                                }
        //                                else
        //                                {
        //                                    //ViewState["skulist"] = null;
        //                                    ddlSKU.DataSource = null;
        //                                    ddlSKU.DataBind();
        //                                    lblMsg.Text = "No SKU(s) are assigned to selected Category";

        //                                }
        //                            }
        //                        }

        //                    }
        //                }
        //            }
        //        }

        //    }
        //    else
        //    {
        //        lblMsg.Text = "Please select customer!";
        //    }

        //}

        protected void BindCustomer()
        {
            dpCompany.DataSource = avii.Classes.clsCompany.GetCompany(0, 0);
            dpCompany.DataValueField = "CompanyID";
            dpCompany.DataTextField = "CompanyName";
            dpCompany.DataBind();

        }
        //private void BindCategory()
        //{
        //    SV.Framework.Inventory.ProductController productController = SV.Framework.Inventory.ProductController.CreateInstance<SV.Framework.Inventory.ProductController>();
        //    List<ItemCategory> categoryList = productController.GetItemCategoryTree(0, 0, 1, true, -1, -1, true, true, true, false);
        //    ViewState["categoryList"] = categoryList;
        //    ddlCategory.DataSource = categoryList;
        //    ddlCategory.DataTextField = "categoryname";
        //    ddlCategory.DataValueField = "CategoryWithProductAllowed";
        //    ddlCategory.DataBind();
        //    ListItem li = new ListItem("--Select Category--", "0");
        //    ddlCategory.Items.Insert(0, li);
        //}
        //private void BindSKUs(int categoryGUID)
        //{
        //    int parentCategoryGUID = 0;
        //    List<CompanySKUno> skusList = new List<CompanySKUno>();

        //    if (categoryGUID > 0)
        //    {
        //        ddlSKU.Items.Clear();
        //        List<CompanySKUno> skuList = ViewState["skulist"] as List<CompanySKUno>;
        //        if (skuList != null && skuList.Count > 0)
        //        {

        //            //ViewState["skulist"] = skuList;
        //            ddlSKU.DataSource = skuList;
        //            ddlSKU.DataValueField = "ItemcompanyGUID";
        //            ddlSKU.DataTextField = "SKU";


        //            ddlSKU.DataBind();
        //            ListItem item = new ListItem("", "0");
        //            ddlSKU.Items.Insert(0, item);
        //        }

        //    }


        //}
        private void BindCompanySKU(int companyID, bool IsEdit)
        {
            MslOperation mslOperation = MslOperation.CreateInstance<MslOperation>();
            lblMsg.Text = string.Empty;
            List<CompanySKUno> skuList = mslOperation.GetCompanySKUs(companyID, 4);
            if (skuList != null)
            {
                ViewState["skulist"] = skuList;
                if (IsEdit)
                {
                    ddlSKU.DataSource = skuList;
                    ddlSKU.DataValueField = "itemcompanyguid";
                    ddlSKU.DataTextField = "sku";
                    ddlSKU.DataBind();
                }

                //  ddlSKU.DataBind();
                //  ListItem item = new ListItem("", "0");
                //  ddlSKU.Items.Insert(0, item);
            }
            else
            {
                ViewState["skulist"] = null;
                ddlSKU.DataSource = null;
                ddlSKU.DataBind();
                lblMsg.Text = "No SKU are assigned to selected Customer";

            }


        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int qty = 0, userID = 0, currentStock = 0;
            DiscardSKUOperation discardSKUOperation = DiscardSKUOperation.CreateInstance<DiscardSKUOperation>();
            avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
            if (userInfo != null)
            {
                userID = userInfo.UserGUID;
            }
            int.TryParse(txtQty.Text, out qty);
            int.TryParse(lblCurrentStock.Text, out currentStock);

            if (dpCompany.SelectedIndex > 0)
            {
                if (ddlSKU.SelectedIndex > 0)
                {
                    if (ddlModule.SelectedIndex > 0)
                    {
                        if (ddlUser.SelectedIndex > 0)
                        {
                            if (qty > 0)
                            {
                                if (qty <= currentStock)
                                {
                                    if (txtComments.Text.Trim().Length > 1000)
                                    {
                                        lblMsg.Text = "Comment cannot greater than 1000 characters!";
                                    }
                                    else
                                    {
                                        SV.Framework.Models.Inventory.DiscartedSKU request = new SV.Framework.Models.Inventory.DiscartedSKU();
                                        request.ItemCompanyGUID = Convert.ToInt32(ddlSKU.SelectedValue);
                                        request.DiscardedSKUID = 0;
                                        request.UserID = userID;
                                        request.RequestedBy = Convert.ToInt32(ddlUser.SelectedValue);
                                        request.ModuleName = ddlModule.SelectedValue;
                                        request.Qty = qty;
                                        request.ApproveLan = chkAproveLan.Checked;
                                        request.Comments = txtComments.Text.Trim();


                                        int returnResult = discardSKUOperation.DiscartedSKUInsert(request);
                                        if (returnResult > 0)
                                        {
                                            lblMsg.Text = "Submitted successfully";
                                            btnSubmit.Enabled = false;
                                        }
                                        else
                                        {
                                            lblMsg.Text = "Submitted successfully";
                                        }
                                    }
                                }
                                else
                                {
                                    lblMsg.Text = "Discarded quantity cannot be greater than current stock!";
                                }
                            }
                            else
                            {
                                lblMsg.Text = "Please enter quantity!";
                            }
                        }
                        else
                        {
                            lblMsg.Text = "Please select requested By!";
                        }
                    }
                    else
                    {
                        lblMsg.Text = "Please select Module!";
                    }
                }
                else
                {
                    lblMsg.Text = "Please select SKU!";
                }
            }
            else
            {
                lblMsg.Text = "Please select customer!";
            }
        }
        private void BindUser()
        {
            int companyID = 0;
            string sortExpression = "UserName";
            string sortDirection = "ASC";
            string sortBy = sortExpression + " " + sortDirection;
            int statusID = 2;

            if (dpCompany.SelectedIndex > 0)
                companyID = Convert.ToInt32(dpCompany.SelectedValue);
            List<avii.Classes.UserRegistration> userList = avii.Classes.RegistrationOperation.GetUserInfoList(companyID, string.Empty, sortBy, statusID);
            if (userList != null && userList.Count > 0)
            {
                ddlUser.DataSource = userList;
                ddlUser.DataTextField = "UserName";
                ddlUser.DataValueField = "UserID";
                ddlUser.DataBind();
                ddlUser.Items.Insert(0, new System.Web.UI.WebControls.ListItem("", "0"));

            }
            else
            {
                lblMsg.Text = "No record found";
            }

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            btnSubmit.Enabled = true;
            dpCompany.SelectedIndex = 0;
            ddlUser.SelectedIndex = 0;
            ddlModule.SelectedIndex = 0;
            ddlSKU.SelectedIndex = 0;
            ddlUser.Items.Clear();
            ddlSKU.Items.Clear();
            txtQty.Text = "";
            lblMsg.Text = "";
            lblCategoryName.Text = "";
            lblItemName.Text = "";
            lblSKU.Text = "";
            lblCurrentStock.Text = "";
            trSKU.Visible = false;
            //ddlSKU.SelectedIndex = 0;
        }

        protected void ddlSKU_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblCategoryName.Text = "";
            lblItemName.Text = "";
            lblSKU.Text = "";
            lblCurrentStock.Text = "";
            trSKU.Visible = false;
            DiscardSKUOperation discardSKUOperation = DiscardSKUOperation.CreateInstance<DiscardSKUOperation>();
            if (ddlSKU.SelectedIndex > 0)
            {
                SkuInfo skuInfo = discardSKUOperation.SKUInfoBySKUId(Convert.ToInt32(ddlSKU.SelectedValue));
                if(skuInfo != null && skuInfo.ItemCompanyGUID > 0)
                {
                    trSKU.Visible = true;
                    lblCategoryName.Text= skuInfo.CategoryName;
                    lblItemName.Text= skuInfo.ItemName;
                    lblSKU.Text= skuInfo.SKU;
                    lblCurrentStock.Text= skuInfo.CurrentStock.ToString();
                }
            }
        }
    }
}