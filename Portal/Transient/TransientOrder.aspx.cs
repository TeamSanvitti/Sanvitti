using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SV.Framework.Models.Inventory;
using SV.Framework.Inventory;
using System.Configuration;

namespace avii.Transient
{
    public partial class TransientOrder : System.Web.UI.Page
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
                int internalordercustomer = Convert.ToInt32(ConfigurationSettings.AppSettings["internalordercustomer"]);
                string orderDate = DateTime.Now.ToShortDateString();
                lblOrderDate.Text = orderDate;
                txtProposedReceiveDate.Text = orderDate;

                GetOrderDetail();
            }
        }

        protected void BindCustomer()
        {
            DataTable dt = avii.Classes.clsCompany.GetCompany(0, 0);
            dpCompany.DataSource = dt;
            dpCompany.DataValueField = "CompanyID";
            dpCompany.DataTextField = "CompanyName";
            dpCompany.DataBind();

        }
        private void GetOrderDetail()
        {
            Int64 transientOrderID = 0;
            if (Session["TransientOrderID"] != null)
            {
                transientOrderID = Convert.ToInt64(Session["TransientOrderID"]);
                Session["TransientOrderID"] = null;
                ViewState["TransientOrderID"] = transientOrderID;
                ViewState["EDIT"] = 1;
                TransientOrderOperation serviceRequestOperations = TransientOrderOperation.CreateInstance<TransientOrderOperation>();
                TransientReceiveOrder transientOrder = serviceRequestOperations.GetTransientOrderDetail(transientOrderID);
                if(transientOrder != null)
                {
                    BindUsers(transientOrder.CompanyID);
                    BindCustomerSKU(transientOrder.CompanyID, true);
                    trMemo.Visible = true;
                    lblMemoNo.Text = transientOrder.MemoNumber.ToString();
                    txtComment.Text = transientOrder.Comment;
                    txtOrderedQty.Text = transientOrder.OrderedQty.ToString();
                    txtProposedReceiveDate.Text = transientOrder.ProposedReceiveDate;
                    txtSupplier.Text = transientOrder.SupplierName;
                    ddlSKU.SelectedValue = transientOrder.ItemCompanyGUID.ToString();
                    dpCompany.SelectedValue = transientOrder.CompanyID.ToString();
                    ddlCategory.SelectedValue = transientOrder.CategoryWithProductAllowed;
                    ddlUsers.SelectedValue = transientOrder.RequestedBy.ToString();
                    lblSCurrentStock.Text = transientOrder.Stock_in_Hand.ToString();
                    lblProductname.Text = transientOrder.ProductName;
                    lblOrderDate.Text = transientOrder.TransientOrderDateTime.ToString("MM/dd/yyyy");
                    txtProposedReceiveDate.Text = transientOrder.ProposedReceiveDateTime.ToString("MM/dd/yyyy");
                }

            }
        }
        private void BindCategory()
        {
            SV.Framework.Inventory.ProductController productController = SV.Framework.Inventory.ProductController.CreateInstance<SV.Framework.Inventory.ProductController>();

            List<ItemCategory> categoryList = productController.GetItemCategoryTree(0, 0, 1, true, -1, -1, true, true, false, false);
            ViewState["categoryList"] = categoryList;
            

            ddlCategory.DataSource = categoryList;
            ddlCategory.DataTextField = "categoryname";
            ddlCategory.DataValueField = "CategoryWithProductAllowed";
            ddlCategory.DataBind();
            ListItem li2 = new ListItem("", "0");
            ddlCategory.Items.Insert(0, li2);
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


        protected void dpCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlSKU.Items.Clear();
            ddlCategory.SelectedIndex = 0;
            ddlUsers.Items.Clear();
            lblProductname.Text = "";
            lblSCurrentStock.Text = "";
            txtOrderedQty.Text = "";
            txtSupplier.Text = "";
            lblMsg.Text = "";
            txtComment.Text = "";
            ViewState["sourceskulist"] = null;
            
            if (dpCompany.SelectedIndex > 0)
            {
                int companyID = Convert.ToInt32(dpCompany.SelectedValue);
                BindCustomerSKU(companyID, false);
                BindUsers(companyID);
            }
        }

        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            int categoryGUID = 0, parentCategoryGUID = 0;
            string CategoryWithProductAllowed = ddlCategory.SelectedValue;
            lblMsg.Text = string.Empty;
            ViewState["IsESNRequired"] = 0;
            ddlSKU.Items.Clear();
            lblProductname.Text = "";
            lblSCurrentStock.Text = "";
            txtOrderedQty.Text = "";
            lblMsg.Text = "";
            txtComment.Text = "";
            

            if (dpCompany.SelectedIndex > 0)
            {
                string[] array = CategoryWithProductAllowed.Split('|');
                categoryGUID = Convert.ToInt32(array[0]);
                if (Convert.ToInt32(array[0]) == 0)
                {
                    lblMsg.Text = "Please select leaf category!";
                    dpCompany.SelectedIndex = 0;
                    return;
                }
            }

            List<CompanySKUno> skusList = new List<CompanySKUno>();
            if (dpCompany.SelectedIndex > 0)
            {
                if (categoryGUID > 0)
                {
                    //ddlSourceSKU.Items.Clear();
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
                                            //ddlSourceSKU.DataSource = null;
                                            //ddlSourceSKU.DataBind();
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
            int itemcompanyguid = 0;
            lblMsg.Text = "";
            btnSubmit.Visible = true;

           // ddlUsers.Items.Clear();
            lblProductname.Text = "";
            lblSCurrentStock.Text = "";
            //txtOrderedQty.Text = "";
           // txtComment.Text = "";

            if (ddlSKU.SelectedIndex > 0)
            {
                itemcompanyguid = Convert.ToInt32(ddlSKU.SelectedValue);
                if (ViewState["sourceskulist"] != null)
                {
                    List<CompanySKUno> skuList = ViewState["sourceskulist"] as List<CompanySKUno>;
                    var skus = (from item in skuList where item.ItemcompanyGUID.Equals(itemcompanyguid) select item).ToList();
                    if (skus != null && skus.Count > 0)
                    {
                        lblSCurrentStock.Text = skus[0].CurrentStock.ToString();
                        lblProductname.Text = skus[0].ProductName;
                        //if (skus[0].CurrentStock == 0)
                        //{
                        //    lblMsg.Text = "Connot create order request with current stock 0!";
                        //    btnSubmit.Visible = false;
                        //}
                    }
                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Int64 transientOrderID = 0;
            TransientOrderOperation serviceRequestOperations = TransientOrderOperation.CreateInstance<TransientOrderOperation>();

            if (ViewState["TransientOrderID"] != null)
                transientOrderID = Convert.ToInt64(ViewState["TransientOrderID"]);
            //DateTime  = DateTime.Now;
            int companyID = 0, itemCompanyGUID = 0, userID = 0, requestedBy = 0, orderedQty=0;
            string supplierName = "", comments = "", response = "", transientOrderDate="", proposedReceiveDate = "";
            //DateTime  = DateTime.Now;
            TransientReceiveOrder transientOrder = new TransientReceiveOrder();
            response = ValidateTranferOrder();

            if (string.IsNullOrEmpty(response))
            {
                companyID = Convert.ToInt32(dpCompany.SelectedValue);
                itemCompanyGUID = Convert.ToInt32(ddlSKU.SelectedValue);
                requestedBy = Convert.ToInt32(ddlUsers.SelectedValue);
                int.TryParse(txtOrderedQty.Text, out orderedQty);
                supplierName = txtSupplier.Text.Trim();
                transientOrderDate = lblOrderDate.Text;
                proposedReceiveDate = txtProposedReceiveDate.Text.Trim();

                comments = txtComment.Text.Trim();
                userID = Convert.ToInt32(Session["UserID"]);
                transientOrder.CompanyID = companyID;
                transientOrder.TransientOrderID = transientOrderID;
                transientOrder.ItemCompanyGUID = itemCompanyGUID;
                transientOrder.OrderedQty = orderedQty;
                transientOrder.TransientOrderDate = transientOrderDate;
                transientOrder.SupplierName = supplierName;
                transientOrder.ProposedReceiveDate = proposedReceiveDate;
                transientOrder.RequestedBy = requestedBy;
                transientOrder.Comment = comments;
                transientOrder.UserID = userID;

                response = serviceRequestOperations.CreateTransientOrderInsertUpdate(transientOrder, out transientOrderID);

                if (transientOrderID > 0)
                {
                    ViewState["TransientOrderID"] = transientOrderID;
                    btnAdd.Visible= true; 
                }

                lblMsg.Text = response;
            }
            else
                lblMsg.Text = response;
        }
        private string ValidateTranferOrder()
        {
            string returnMessage = "";
            if(dpCompany.SelectedIndex==0)
            {
                returnMessage = "Customer required!";
                return returnMessage;
            }
            if (ddlCategory.SelectedIndex == 0)
            {
                returnMessage = "Category required!";
                return returnMessage;
            }
            if (ddlSKU.SelectedIndex == 0)
            {
                returnMessage = "SKU required!";
                return returnMessage;
            }
            if(string.IsNullOrEmpty(txtOrderedQty.Text.Trim()))
            {
                returnMessage = "Ordered quatity required!";
                return returnMessage;
            }
            if (string.IsNullOrEmpty(txtSupplier.Text.Trim()))
            {
                returnMessage = "Supplier name required!";
                return returnMessage;
            }
            if (string.IsNullOrEmpty(txtProposedReceiveDate.Text.Trim()))
            {
                returnMessage = "Proposed receive date required!";
                return returnMessage;
            }
            if (ddlUsers.SelectedIndex == 0)
            {
                returnMessage = "Requested by required!";
                return returnMessage;
            }


            return returnMessage;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            if (ViewState["EDIT"] != null)
            {
                Session["trsearchback"] = 1;
                Response.Redirect("TransientOrderSearch.aspx", true);
            }
            else
                ClearForm();
        }
        private void ClearForm()
        {
            string orderDate = DateTime.Now.ToShortDateString();
            lblOrderDate.Text = orderDate;
            txtProposedReceiveDate.Text = orderDate;
            lblMsg.Text = "";
            txtComment.Text = "";
            txtOrderedQty.Text = "";
            txtSupplier.Text = "";
            dpCompany.SelectedIndex = 0;
            ddlCategory.SelectedIndex = 0;
            ddlSKU.Items.Clear();
            ddlUsers.Items.Clear();
            lblProductname.Text = "";
            lblSCurrentStock.Text = "";
            
        }

        private void BindCustomerSKU(int companyID, bool IsEdit)
        {
            MslOperation mslOperation = MslOperation.CreateInstance<MslOperation>();

            lblMsg.Text = string.Empty;
            List<CompanySKUno> skuList = mslOperation.GetCompanySKUsNew(companyID, 0, "");
            if (skuList != null)
            {
                ViewState["sourceskulist"] = skuList;
                if(IsEdit)
                {
                    ddlSKU.DataSource = skuList;
                    ddlSKU.DataValueField = "ItemcompanyGUID";
                    ddlSKU.DataTextField = "SKU";
                    ddlSKU.DataBind();
                    ListItem item = new ListItem("", "0");
                    ddlSKU.Items.Insert(0, item);
                }
            }
            else
            {
                ViewState["sourceskulist"] = null;
                lblMsg.Text = "No SKU are assigned to selected Customer";

            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("TransientOrder.aspx", true);
        }
    }
} 