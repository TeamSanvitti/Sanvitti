using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using avii.Classes;

namespace avii.Vendor
{
    public partial class StorefrontInventoryAdd : System.Web.UI.Page
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
                Response.Redirect(url);
            }
            if (!IsPostBack)
            {
                BindStorefront();
                BindCategory(ddlCategoryType);
                BindMaker(ddlMaker);
                BindCompany();
                BindCondition();
                BindLocale();
                BindStatus();

            }
        }         
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int userID = Convert.ToInt32(Session["UserID"]);
            SV.Framework.Storefront.Product.ProductRequestModel request = new SV.Framework.Storefront.Product.ProductRequestModel();
            bool active = true;
            int MaximumStock = 0, MinimumStock = 0, OpeningStock = 0;
            string SKU = txtSKU.Text.Trim();
            string UPC = txtUPC.Text.Trim();
            string modelNumber = txtModelNumber.Text.Trim();
            string title = txtTitle.Text.Trim();
            string titleDesc = txtDesc.Text.Trim();
            string countryRegion = txtRegion.Text.Trim();
            string conditionDesc = txtConditionDesc.Text.Trim();
            decimal Weight = 0;
            int.TryParse(txtMaxStock.Text.Trim(), out MaximumStock);
            int.TryParse(txtMinStock.Text.Trim(), out MinimumStock);
            int.TryParse(txtOpeningStock.Text.Trim(), out OpeningStock);
            decimal.TryParse(txtWeight.Text.Trim(), out Weight);
            
                //if (ddlStatus.SelectedValue == "Enable")
                //active = true;

                request.Active = active;
            if (ddlStorefront.SelectedIndex > 0)
            {
                if (dpCompany.SelectedIndex > 0)
                {
                    if (ddlCategoryType.SelectedIndex > 0)
                    {
                        if (ddlMaker.SelectedIndex > 0)
                        {
                            if (!string.IsNullOrEmpty(SKU))
                            {
                                if (!string.IsNullOrEmpty(modelNumber))
                                {
                                    if (!string.IsNullOrEmpty(UPC))
                                    {
                                        if (!string.IsNullOrEmpty(title))
                                        {
                                            if (!string.IsNullOrEmpty(titleDesc))
                                            {
                                                if (!string.IsNullOrEmpty(countryRegion))
                                                {
                                                    if (OpeningStock > 0)
                                                    {
                                                        if (ddlLocale.SelectedIndex > 0)
                                                        {
                                                            if (ddlCondition.SelectedIndex > 0)
                                                            {
                                                                //if (ddlStatus.SelectedIndex > 0)
                                                                //{
                                                                    request.ItemGUID = 0;
                                                                    string[] array = ddlCategoryType.SelectedValue.Split('|');
                                                                    request.CategoryID = Convert.ToInt32(array[0]);
                                                                    // request.CategoryID = Convert.ToInt32(ddlCategoryType.SelectedValue);
                                                                    request.CompanyID = Convert.ToInt32(dpCompany.SelectedValue);
                                                                    request.Condition = ddlCondition.SelectedValue;
                                                                    request.ConditionDesc = conditionDesc;
                                                                    request.CountryOrRegion = countryRegion;
                                                                    request.ItemDescription = titleDesc;
                                                                    request.ItemName = title;
                                                                    request.Locale = ddlLocale.SelectedValue;// txtLocation.Text.Trim();
                                                                    request.Location = txtLocation.Text.Trim();
                                                                    request.MakerGUID = Convert.ToInt32(ddlMaker.SelectedValue);
                                                                    request.ModelNumber = modelNumber;
                                                                    request.MaximumStock = MaximumStock;
                                                                    request.MinimumStock = MinimumStock;
                                                                    request.OpeningStock = OpeningStock;
                                                                    request.ProductSource = ddlStorefront.SelectedValue;
                                                                    request.SerialNumber = txtSerialNo.Text.Trim();
                                                                    request.SKU = SKU;
                                                                    request.UPC = UPC;
                                                                    request.UserID = userID;
                                                                    request.WeightDimension = txtDimension.Text.Trim();
                                                                    request.Weight = Weight; // Convert.ToDecimal(txtWeight.Text.Trim());
                                                                    request.WarehouseCode = ddlWarehousecode.SelectedValue;

                                                                    lblMsg.Text = SV.Framework.Storefront.Product.ProductOperation.InventoryInsertUpdate(request);
                                                                //}
                                                                //else
                                                                //    lblMsg.Text = "Status is required!";
                                                            }
                                                            else
                                                                lblMsg.Text = "Condition is required!";
                                                        }
                                                        else
                                                            lblMsg.Text = "Locale is required!";
                                                    }
                                                    else
                                                        lblMsg.Text = "Country/Region of Manufacture is required!";
                                                }
                                                else
                                                    lblMsg.Text = "Description is required!";
                                            }
                                            else
                                                lblMsg.Text = "Description is required!";
                                        }
                                        else
                                            lblMsg.Text = "Title is required!";
                                    }
                                    else
                                        lblMsg.Text = "UPC is required!";
                                }
                                else
                                    lblMsg.Text = "Model number is required!";
                            }
                            else
                                lblMsg.Text = "SKU is required!";
                        }
                        else
                            lblMsg.Text = "Brand/Maker is required!";
                    }
                    else
                        lblMsg.Text = "Category is required!";
                }
                else
                    lblMsg.Text = "Customer is required!";
            }
            else
                lblMsg.Text = "Storefront is required!";
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearForm();
        }
        private void ClearForm()
        {
            txtConditionDesc.Text = "";
            txtDesc.Text = "";
            txtDimension.Text = "";
            txtLocation.Text = "";
            txtMaxStock.Text = "";
            txtMinStock.Text = "";
            txtModelNumber.Text = "";
            txtOpeningStock.Text = "";
            txtRegion.Text = "";
            txtSerialNo.Text = "";
            txtSKU.Text = "";
            txtTitle.Text = "";
            txtUPC.Text = "";
            txtWeight.Text = "";
            lblMsg.Text = "";
            dpCompany.SelectedIndex = 0;
            ddlCategoryType.SelectedIndex = 0;
            ddlCondition.SelectedIndex = 0;
            ddlLocale.SelectedIndex = 0;
            ddlMaker.SelectedIndex = 0;
            //ddlStatus.SelectedIndex = 0;
            ddlStorefront.SelectedIndex = 1;
            ddlWarehousecode.SelectedIndex = 0;
        }
        protected void dpCompany_SelectedIndexChanged(object sender, EventArgs e)
        {

            BindWarehouseCode(Convert.ToInt32(dpCompany.SelectedValue));

        }
        private void BindStorefront()
        {
            ddlStorefront.DataSource = Enum.GetNames(typeof(SV.Framework.Storefront.Product.ProductOperation.StorefrontEnum));
            ddlStorefront.DataBind();
            ListItem li = new ListItem("", "0");
            ddlStorefront.Items.Insert(0, li);

            ddlStorefront.SelectedIndex = 1;
        }
        private void BindCondition()
        {
            ddlCondition.DataSource = Enum.GetNames(typeof(SV.Framework.Storefront.Product.ProductOperation.ConditionEnum));
            ddlCondition.DataBind();
            ListItem li = new ListItem("", "0");
            ddlCondition.Items.Insert(0, li);
        }
        private void BindLocale()
        {
            ddlLocale.DataSource = Enum.GetNames(typeof(SV.Framework.Storefront.Product.ProductOperation.LocaleEnum));
            ddlLocale.DataBind();
            ListItem li = new ListItem("", "0");
            ddlLocale.Items.Insert(0, li);
        }
        private void BindStatus()
        {
            //ddlStatus.DataSource = Enum.GetNames(typeof(SV.Framework.Storefront.Product.ProductOperation.ProductStatusEnum));
            //ddlStatus.DataBind();
            //ListItem li = new ListItem("", "0");
            //ddlStatus.Items.Insert(0, li);
        }
        protected void BindCategory(DropDownList ddlCategory)
        {
            ProductController objProductController = new ProductController();
            ddlCategory.DataSource = objProductController.getItemCategoryTree(0, 0, 1, true, -1, -1, true, false, false, false);
            ddlCategory.DataTextField = "categoryname";
            ddlCategory.DataValueField = "CategoryWithProductAllowed";
            ddlCategory.DataBind();
            ListItem li = new ListItem("", "0");
            ddlCategory.Items.Insert(0, li);
        }
        protected void BindMaker(DropDownList ddlMaker)
        {
            ProductController objProductController = new ProductController();

            ddlMaker.Items.Clear();

            List<avii.Classes.Maker> lstItemCategoryList = objProductController.getMakerList(-1, "", -1, -1, -1, -1, -1, -1);
            ListItem li = new ListItem("", "0");
            //ddlMaker.Items.Insert(0, li);
            int catid = -1;
            for (int ictr = 0; ictr < lstItemCategoryList.Count; ictr++)
            {
                catid = lstItemCategoryList[ictr].MakerGUID;

                li = new ListItem(lstItemCategoryList[ictr].MakerName, catid.ToString());
                ddlMaker.Items.Add(li);
            }
        }

        private void BindWarehouseCode(int compnayID)
        {
            if (compnayID > 0)
            {
                List<CustomerWarehouseCode> warehuseCodeList = WarehouseCodeOperations.GetCompanyWarehouseCode(compnayID, null, true);
                if (warehuseCodeList != null && warehuseCodeList.Count > 0)
                {
                    ddlWarehousecode.DataSource = warehuseCodeList;
                    ddlWarehousecode.DataValueField = "WarehouseCode";
                    ddlWarehousecode.DataTextField = "WarehouseCode";
                    ddlWarehousecode.DataBind();
                    ListItem item = new ListItem("000", "000");
                    ddlWarehousecode.Items.Insert(0, item);
                }
                else
                {
                    ddlWarehousecode.Items.Clear();
                    //ddlWhCode.DataSource = null;
                    //ddlWhCode.DataBind();
                   // ListItem item = new ListItem("000", "000");
                   // ddlWarehousecode.Items.Insert(0, item);
                }
            }
            else
            {
                ddlWarehousecode.Items.Clear();
                //ddlWhCode.DataSource = null;
                //ddlWhCode.DataBind();
               // ListItem item = new ListItem("000", "000");
               // ddlWarehousecode.Items.Insert(0, item);
            }
        }
        protected void BindCompany()
        {
            dpCompany.DataSource = avii.Classes.RMAUtility.getRMAUserCompany(-1, "", 1, -1);
            dpCompany.DataValueField = "companyID";
            dpCompany.DataTextField = "companyName";
            dpCompany.DataBind();
            ListItem item = new ListItem("", "0");
            dpCompany.Items.Insert(0, item);

        }

        
    }
}