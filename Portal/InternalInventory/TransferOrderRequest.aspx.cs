﻿using System;
using System.Data;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SV.Framework.Models.Inventory;
using SV.Framework.Inventory;

namespace avii.InternalInventory
{
    public partial class TransferOrderRequest : System.Web.UI.Page
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
                BindCategory();
            }
        }
        protected void BindCustomer()
        {
            DataTable dt = avii.Classes.clsCompany.GetCompany(0, 0);
            dpSourceCompany.DataSource = dt;
            dpSourceCompany.DataValueField = "CompanyID";
            dpSourceCompany.DataTextField = "CompanyName";
            dpSourceCompany.DataBind();

            ddlDestinationCompany.DataSource = dt;
            ddlDestinationCompany.DataValueField = "CompanyID";
            ddlDestinationCompany.DataTextField = "CompanyName";
            ddlDestinationCompany.DataBind();
        }
        private void BindCategory()
        {
            SV.Framework.Inventory.ProductController productController = SV.Framework.Inventory.ProductController.CreateInstance<SV.Framework.Inventory.ProductController>();

            List<ItemCategory> categoryList = productController.GetItemCategoryTree(0, 0, 1, true, -1, -1, true, true, false, false);
            ViewState["categoryList"] = categoryList;
            ddlDestinationCategory.DataSource = categoryList;
            ddlDestinationCategory.DataTextField = "categoryname";
            ddlDestinationCategory.DataValueField = "CategoryWithProductAllowed";
            ddlDestinationCategory.DataBind();
            ListItem li = new ListItem("", "0");
            ddlDestinationCategory.Items.Insert(0, li);


            ddlSourceCategory.DataSource = categoryList;
            ddlSourceCategory.DataTextField = "categoryname";
            ddlSourceCategory.DataValueField = "CategoryWithProductAllowed";
            ddlSourceCategory.DataBind();
            ListItem li2 = new ListItem("", "0");
            ddlSourceCategory.Items.Insert(0, li2);
        }
        private void BindUsers(int companyID)
        {
            avii.Classes.UserUtility objUser = new avii.Classes.UserUtility();
            List<avii.Classes.clsUserManagement> userList = objUser.getUserList("", companyID, "", -1, -1, -1, true);
            if (userList != null && userList.Count > 0)
            {
                ddlUsers.DataSource = userList;
                ddlUsers.DataValueField = "UserID";
                ddlUsers.DataTextField = "UserName";
                ddlUsers.DataBind();

                ListItem newList = new ListItem("", "0");
                ddlUsers.Items.Insert(0, newList);
            }
            else
            {
                ddlUsers.DataSource = null;
                ddlUsers.DataBind();
                lblMsg.Text = "No users are assigned to selected Customer";
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            TransferOrder request = new TransferOrder();
            TransferOrderOperation serviceRequestOperations = TransferOrderOperation.CreateInstance<TransferOrderOperation>();
            int OrderTransferID, SourceCompanyID, SourceItemCompanyGUID, DestinationCompanyID, DestinationItemCompanyGUID, RequestedQty, RequestedBy, CreatedBy;
            OrderTransferID = SourceCompanyID = SourceItemCompanyGUID = DestinationCompanyID = DestinationItemCompanyGUID = RequestedBy = CreatedBy = 0;
            CreatedBy = Convert.ToInt32(Session["UserID"]);

            string OrderTransferNumber, OrderTransferDate, Comment, errorMessage;
            OrderTransferNumber = txtTranferOrderNo.Text;
            request.OrderTransferNumber = OrderTransferNumber;
            OrderTransferDate = txtOrderDate.Text;
            request.OrderTransferDate = OrderTransferDate;
            Comment = txtComment.Text;
            request.Comment = Comment;
            int.TryParse(txtRequestQty.Text.Trim(), out RequestedQty);
            request.RequestedQty = RequestedQty;

            if (dpSourceCompany.SelectedIndex > 0)
                SourceCompanyID = Convert.ToInt32(dpSourceCompany.SelectedValue);
            if (ddlSourceSKU.SelectedIndex > 0)
                SourceItemCompanyGUID = Convert.ToInt32(ddlSourceSKU.SelectedValue);
            if (ddlDestinationCompany.SelectedIndex > 0)
                DestinationCompanyID = Convert.ToInt32(ddlDestinationCompany.SelectedValue);
            if (ddlDestinationSKU.SelectedIndex > 0)
                DestinationItemCompanyGUID = Convert.ToInt32(ddlDestinationSKU.SelectedValue);
            if (ddlUsers.SelectedIndex > 0)
                RequestedBy = Convert.ToInt32(ddlUsers.SelectedValue);
            request.OrderTransferID = OrderTransferID;
            request.SourceCompanyID = SourceCompanyID;
            request.SourceItemCompanyGUID = SourceItemCompanyGUID;
            request.DestinationCompanyID = DestinationCompanyID;
            request.DestinationItemCompanyGUID = DestinationItemCompanyGUID;
            request.RequestedBy = RequestedBy;
            request.CreatedBy = CreatedBy;

            errorMessage = serviceRequestOperations.CreateInternalTransferOrder(request);

            lblMsg.Text = errorMessage;
            
        }
        private void ClearForm()
        {
            lblMsg.Text = "";
            txtComment.Text = "";
            txtOrderDate.Text = "";
            txtRequestQty.Text = "";
            txtTranferOrderNo.Text = "";
            ddlDestinationCategory.SelectedIndex = 0;
            ddlDestinationSKU.Items.Clear();
            ddlDestinationCompany.SelectedIndex = 0;
            dpSourceCompany.SelectedIndex = 0;
            ddlSourceCategory.SelectedIndex = 0;
            ddlSourceSKU.Items.Clear();
            ddlUsers.Items.Clear(); 

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        protected void dpSourceCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dpSourceCompany.SelectedIndex > 0)
            {
                int companyID = Convert.ToInt32(dpSourceCompany.SelectedValue);
                BindCustomerSKU(companyID, "source");
            }
        }
        //private string ValidateTranferOrder(string transferOrder, int sourceCompanyID, int sourceCategoryID, string sourceSKU, )
        //{
        //    string returnMessage = "";


        //    return returnMessage;
        //}
        protected void ddlSourceCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            int categoryGUID = 0, parentCategoryGUID = 0;
            string CategoryWithProductAllowed = ddlSourceCategory.SelectedValue;
            lblMsg.Text = string.Empty;
            ViewState["IsESNRequired"] = 0;
            if (ddlSourceCategory.SelectedIndex > 0)
            {
                string[] array = CategoryWithProductAllowed.Split('|');
                categoryGUID = Convert.ToInt32(array[0]);
                if (Convert.ToInt32(array[0]) == 0)
                {
                    lblMsg.Text = "Please select leaf category!";
                    ddlSourceCategory.SelectedIndex = 0;
                    return;
                }
            }

            List<CompanySKUno> skusList = new List<CompanySKUno>();
            if (dpSourceCompany.SelectedIndex > 0)
            {
                if (categoryGUID > 0)
                {
                    ddlSourceSKU.Items.Clear();
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

                                if (ViewState["sourceskulist"] != null)
                                {
                                    List<CompanySKUno> skuList = ViewState["sourceskulist"] as List<CompanySKUno>;
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
                                            ddlSourceSKU.DataSource = skusList;
                                            ddlSourceSKU.DataValueField = "ItemcompanyGUID";
                                            ddlSourceSKU.DataTextField = "SKU";


                                            ddlSourceSKU.DataBind();
                                            ListItem item = new ListItem("", "0");
                                            ddlSourceSKU.Items.Insert(0, item);
                                        }
                                        else
                                        {
                                            //ViewState["skulist"] = null;
                                            ddlSourceSKU.DataSource = null;
                                            ddlSourceSKU.DataBind();
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

        protected void ddlSourceSKU_SelectedIndexChanged(object sender, EventArgs e)
        {
            int itemcompanyguid = 0;
            lblDCurrentStock.Text = "";
            if (ddlSourceSKU.SelectedIndex > 0)
            {
                itemcompanyguid = Convert.ToInt32(ddlSourceSKU.SelectedValue);
                if (ViewState["sourceskulist"] != null)
                {
                    List<CompanySKUno> skuList = ViewState["sourceskulist"] as List<CompanySKUno>;
                    var skus = (from item in skuList where item.ItemcompanyGUID.Equals(itemcompanyguid) select item).ToList();
                    if (skus != null && skus.Count > 0)
                    {
                        lblSCurrentStock.Text = skus[0].CurrentStock.ToString();
                    }
                }
            }
        }

        protected void ddlDestinationCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlDestinationCompany.SelectedIndex > 0)
            {
                int companyID = Convert.ToInt32(ddlDestinationCompany.SelectedValue);
                BindCustomerSKU(companyID, "destination");
                BindUsers(companyID);
            }
        }

        protected void ddlDestinationCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            int categoryGUID = 0, parentCategoryGUID = 0;
            string CategoryWithProductAllowed = ddlDestinationCategory.SelectedValue;
            lblMsg.Text = string.Empty;
            ddlDestinationSKU.DataSource = null;
            ddlDestinationSKU.DataBind();

            // ViewState["IsESNRequired"] = 0;
            if (ddlDestinationCategory.SelectedIndex > 0)
            {
                string[] array = CategoryWithProductAllowed.Split('|');
                categoryGUID = Convert.ToInt32(array[0]);
                if (Convert.ToInt32(array[0]) == 0)
                {
                    lblMsg.Text = "Please select leaf category!";
                    ddlSourceCategory.SelectedIndex = 0;
                    return;
                }
            }

            List<CompanySKUno> skusList = new List<CompanySKUno>();
            if (ddlDestinationCompany.SelectedIndex > 0)
            {
                if (categoryGUID > 0)
                {
                    ddlDestinationSKU.Items.Clear();
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

                                if (ViewState["destinationskulist"] != null)
                                {
                                    List<CompanySKUno> skuList = ViewState["destinationskulist"] as List<CompanySKUno>;
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
                                            ddlDestinationSKU.DataSource = skusList;
                                            ddlDestinationSKU.DataValueField = "ItemcompanyGUID";
                                            ddlDestinationSKU.DataTextField = "SKU";


                                            ddlDestinationSKU.DataBind();
                                            ListItem item = new ListItem("", "0");
                                            ddlDestinationSKU.Items.Insert(0, item);
                                        }
                                        else
                                        {
                                            //ViewState["skulist"] = null;
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
                lblMsg.Text = "Please select destination customer!";
            }
        }

        protected void ddlDestinationSKU_SelectedIndexChanged(object sender, EventArgs e)
        {
            int itemcompanyguid = 0;
            lblDCurrentStock.Text = "";
            if (ddlDestinationSKU.SelectedIndex  > 0)
            {
                itemcompanyguid = Convert.ToInt32(ddlDestinationSKU.SelectedValue);
                if (ViewState["destinationskulist"] != null)
                {
                    List<CompanySKUno> skuList = ViewState["destinationskulist"] as List<CompanySKUno>;
                    var skus = (from item in skuList where item.ItemcompanyGUID.Equals(itemcompanyguid) select item).ToList();
                    if (skus != null && skus.Count > 0)
                    {
                        lblDCurrentStock.Text = skus[0].CurrentStock.ToString();
                    }
                }
            }
        }
        private void BindCustomerSKU(int companyID,  string customerType)
        {
            MslOperation mslOperation = MslOperation.CreateInstance<MslOperation>();

            lblMsg.Text = string.Empty;
            List<CompanySKUno> skuList = mslOperation.GetCompanySKUs(companyID, 0);
            if (skuList != null)
            {
                if (customerType == "source")
                    ViewState["sourceskulist"] = skuList;
                else
                    ViewState["destinationskulist"] = skuList;
                //if (IsEdit)
                //{
                //    ddlSourceSKU.DataSource = skuList;
                //    ddlSourceSKU.DataValueField = "itemcompanyguid";
                //    ddlSourceSKU.DataTextField = "sku";
                //    ddlSourceSKU.DataBind();
                //}

                //  ddlSKU.DataBind();
                //  ListItem item = new ListItem("", "0");
                //  ddlSKU.Items.Insert(0, item);
            }
            else
            {
                ViewState["sourceskulist"] = null;
                //ddlSourceSKU.DataSource = null;
                //ddlSourceSKU.DataBind();
                lblMsg.Text = "No SKU are assigned to selected Customer";

            }


        }


    }
}