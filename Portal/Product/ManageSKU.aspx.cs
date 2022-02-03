using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
//using avii.Classes;
using System.Reflection;
using SV.Framework.Models.Catalog;
using SV.Framework.Models.Common;

namespace avii.Product
{
    public partial class ManageSKU : System.Web.UI.Page
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
                if (Session["UserID"] == null)
                {
                    Response.Redirect(url);
                }
            }

            if (!IsPostBack)
            {
                ReadOlnyAccess();
                trSubmit.Visible = false;
                bindCategory(ddlCategoryFilter);
                if (Session["adm"] != null)
                {
                    BindCompany();
                    

                }
                else
                {
                    lblCust.Visible = false;
                    ddlCompany.Visible = false;

                }
                //bindsku();
                
                FillSearchResult();

            }
        }
        private void ReadOlnyAccess()
        {
            //string url = HttpContext.Current.Request.Url.AbsoluteUri;
            // http://localhost:1302/TESTERS/Default6.aspx

            string path = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
            // /TESTERS/Default6.aspx
            if (path != null)
            {
                path = path.ToLower();
                List<avii.Classes.MenuItem> menuItems = Session["MenuItems"] as List<avii.Classes.MenuItem>;
                foreach (avii.Classes.MenuItem item in menuItems)
                {
                    if (item.Url.ToLower().Contains(path) && item.IsReadOnly)
                    {
                        ViewState["IsReadOnly"] = true;
                        btnSubmit.Visible = false;
                    }
                }

            }

        }
        private void FillSearchResult()
        {
            if (Session["skudetail"] != null)
            {
                Session["skudetail"] = null;
                if (Session["skusearch"] != null)
                {
                    string searchCriteria = (string)Session["skusearch"];
                    string[] arr = searchCriteria.Split('~');

                    ddlCategoryFilter.SelectedValue = arr[0];


                   
                    txtModel.Text = arr[1];
                    txtSKU.Text = arr[2];
                    txtUPC.Text = arr[3];
                   
                    ddlCompany.SelectedValue = arr[4];
                    if (arr.Length > 5 && arr[5] != "")
                        chkDisable.Checked = Convert.ToBoolean(arr[5]);
                    

                    bindItems();

                    Session["productsearch"] = null;
                }

            }
        }
        private void BindCompany()
        {
            ddlCompany.DataSource = avii.Classes.RMAUtility.getRMAUserCompany(-1, "", -1, -1);
            ddlCompany.DataValueField = "companyID";
            ddlCompany.DataTextField = "companyName";
            ddlCompany.DataBind();
            ddlCompany.Items.Insert(0, new ListItem("", "0"));

        }

        

        private void bindCategory(DropDownList ddlCategory)
        {

            SV.Framework.Catalog.ProductController productController = SV.Framework.Catalog.ProductController.CreateInstance<SV.Framework.Catalog.ProductController>();

            ddlCategoryFilter.Items.Clear();
            int catid = -1, parentcat = -1;
            int iactive = 1;
            //if (chkactive.Checked)
            //    iactive = -1;

            List<ItemCategory> lstItemCategoryList = productController.GetItemCategoryTree(0, 0, 1, true, iactive, -1, true, false, false, false);
            ddlCategory.DataSource = lstItemCategoryList;
            ddlCategory.DataTextField = "CategoryName";
            ddlCategory.DataValueField = "CategoryGUID";
            ddlCategory.DataBind();

            ListItem li = new ListItem("--Select Category--", "0");
            ddlCategory.Items.Insert(0, li);

            
        }
        private void bindItems()
        {
            string upc, model, sku;
            trSubmit.Visible = false;
            btnDownload.Visible = false;
           // ProductController objProductController = new ProductController();
            SV.Framework.Catalog.ProductController productController = SV.Framework.Catalog.ProductController.CreateInstance<SV.Framework.Catalog.ProductController>();

            lblMsg.Text = string.Empty;
            int categoryGUID = -1;
           // int makerGUID = -1;
            int companyId = -1;

            
            // int technologyGUID = -1;
            // int showunderCatalog = -1;
            bool Isdisable = chkDisable.Checked;

            upc = model = sku =  string.Empty;
            
            
            if (ddlCategoryFilter.SelectedIndex > 0)
            {
                categoryGUID = Convert.ToInt32(ddlCategoryFilter.SelectedValue);
            }
            if (Session["adm"] != null)
            {
                if (ddlCompany.SelectedIndex > 0)
                {
                    companyId = Convert.ToInt32(ddlCompany.SelectedValue.Trim());
                }
            }
            else
            {
               avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
                if (userInfo != null && userInfo.CompanyGUID > 0)
                { companyId = userInfo.CompanyGUID; }
            }
            string sortExpression = "ModelNumber";
            string sortDirection = "ASC";
            ViewState["SortDirection"] = sortDirection;
            ViewState["SortExpression"] = sortExpression;
            upc = txtUPC.Text.Trim();
            sku = txtSKU.Text.Trim();
            model = txtModel.Text.Trim();

            string searchCriteria = ddlCategoryFilter.SelectedValue + "~" + model + "~" + sku + "~" + upc + "~" + ddlCompany.SelectedValue + "~" + Isdisable;
            Session["skusearch"] = searchCriteria;
            
            {
                List<InventoryItems> items = productController.GetCustomerSKUList(categoryGUID, -1, model, sku, upc, companyId, Isdisable);
                if (items != null && items.Count > 0)
                {
                    grvItem.DataSource = items;
                    trSubmit.Visible = true;
                    btnDownload.Visible = true;
                    Session["items"] = items;
                }
                else
                {
                    btnDownload.Visible = false;
                    trSubmit.Visible = false;
                    grvItem.DataSource = null;
                    lblMsg.Text = "No record exists for selected criteria";
                }
            }
            grvItem.DataBind();

        }
        protected void grvItem_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvItem.PageIndex = e.NewPageIndex;
            bindItems();
        }
        protected void btnItemSearch_click(object sender, EventArgs e)
        {
            bindItems(); 
        }

        protected void btnCancel_click(object sender, EventArgs e)
        {
            chkDisable.Checked = false;
            btnDownload.Visible = false;
            //txtItemCode.Text = string.Empty;
            trSubmit.Visible = false;
            txtUPC.Text = string.Empty;
            txtModel.Text = string.Empty;
            txtSKU.Text = string.Empty;
            //txtColor.Text = string.Empty;
            //ddlSku.SelectedIndex = 0;
            ddlCategoryFilter.SelectedIndex = 0;
            if (Session["adm"] != null)
                ddlCompany.SelectedIndex = 0;

            //ddlTechnology.SelectedIndex = 0;
            //ddlMaker.SelectedIndex = 0;
            lblMsg.Text = string.Empty;
            this.grvItem.DataSource = null;
            grvItem.DataBind();
            //ddlWhCode.SelectedIndex = 0;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            SV.Framework.Catalog.ProductController productController = SV.Framework.Catalog.ProductController.CreateInstance<SV.Framework.Catalog.ProductController>();

            lblMsg.Text = string.Empty;
            string ItemCompanyGUIDs = string.Empty;
            bool IsDisable = true, hdnIsDisable = true;
            int userId = 0;
            avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
            if (userInfo != null && userInfo.UserGUID > 0)
            { userId = userInfo.UserGUID; }

            if (chkDisable.Checked)
                IsDisable = false;

            foreach (GridViewRow row in grvItem.Rows)
            {
                HiddenField hdIsDisable = row.FindControl("hdIsDisable") as HiddenField;
                hdnIsDisable = Convert.ToBoolean(hdIsDisable.Value);

                HiddenField hdItemCompanyGUID = row.FindControl("hdItemCompanyGUID") as HiddenField;
                CheckBox chkSKU = row.FindControl("chkSKU") as CheckBox;
                if (IsDisable)
                {
                    if (chkSKU.Checked)
                    {
                        if (string.IsNullOrWhiteSpace(ItemCompanyGUIDs))
                            ItemCompanyGUIDs = hdItemCompanyGUID.Value;
                        else
                            ItemCompanyGUIDs = ItemCompanyGUIDs + "," + hdItemCompanyGUID.Value;
                    }
                }
                else
                {
                    if (!chkSKU.Checked)
                    {
                        if (string.IsNullOrWhiteSpace(ItemCompanyGUIDs))
                            ItemCompanyGUIDs = hdItemCompanyGUID.Value;
                        else
                            ItemCompanyGUIDs = ItemCompanyGUIDs + "," + hdItemCompanyGUID.Value;
                    }
                }
            }
            if(!string.IsNullOrWhiteSpace(ItemCompanyGUIDs))
            {
                productController.SKUEnableDisableUpdate(ItemCompanyGUIDs, IsDisable, userId);
                lblMsg.Text = "Submitted sucessfully";
            }
            else
            {
                if (IsDisable)
                    lblMsg.Text = "Please select SKU!";
                else
                    lblMsg.Text = "Please unselect SKU!";
            }

        }

        protected void lnkItem_Click(object sender, EventArgs e)
        {
            LinkButton lnkItem = (sender as LinkButton);
            int itemGUID = Convert.ToInt32(lnkItem.CommandArgument);
            Session["itemGUID"] = itemGUID;
            Response.Redirect("SKUDetail.aspx");
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
        public List<InventoryItems> Sort<TKey>(List<InventoryItems> list, string sortBy, SortDirection direction)
        {
            PropertyInfo property = list.GetType().GetGenericArguments()[0].GetProperty(sortBy);
            if (direction == SortDirection.Ascending)
            {
                return list.OrderBy(e => property.GetValue(e, null)).ToList<InventoryItems>();
            }
            else
            {
                return list.OrderByDescending(e => property.GetValue(e, null)).ToList<InventoryItems>();
            }
        }
        protected void grvItem_Sorting(object sender, GridViewSortEventArgs e)
        {
            string Sortdir = GetSortDirection(e.SortExpression);
            string SortExp = e.SortExpression;

            if (Session["items"] != null)
            {
                List<InventoryItems> shipments = (List<InventoryItems>)Session["items"];

                if (shipments != null && shipments.Count > 0)
                {
                    //var list = shipments;
                    if (Sortdir == "ASC")
                    {
                        shipments = Sort<InventoryItems>(shipments, SortExp, SortDirection.Ascending);
                    }
                    else
                    {
                        shipments = Sort<InventoryItems>(shipments, SortExp, SortDirection.Descending);
                    }
                    Session["items"] = shipments;
                    grvItem.DataSource = shipments;
                    grvItem.DataBind();
                }
            }
        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            if (Session["items"] != null)
            {
                List<InventoryItemCSV> itemList = new List<InventoryItemCSV>();
                InventoryItemCSV itemInfo = null;
                List<InventoryItems> items = Session["items"] as List<InventoryItems>;
                if(items != null && items.Count > 0)
                {
                    foreach(InventoryItems item in items)
                    {
                        itemInfo = new InventoryItemCSV();
                        itemInfo.CategoryName = item.ItemCategory;
                        itemInfo.CustomerName = item.CompanyName;
                        itemInfo.ModelNumber = item.ModelNumber;
                        itemInfo.ProductName = item.ItemName;
                        itemInfo.SKU = item.SKU;
                        itemInfo.UPC = item.Upc;
                        itemList.Add(itemInfo);
                    }
                    string string2CSV = itemList.ToCSV();

                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment;filename=SKUExport.csv");
                    Response.Charset = "";
                    Response.ContentType = "application/text";
                    Response.Output.Write(string2CSV);
                    Response.Flush();
                    Response.End();
                }
            }
        }     
    }
}