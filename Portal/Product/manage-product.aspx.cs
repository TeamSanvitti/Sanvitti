using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Reflection;
using SV.Framework.Models.Catalog;
using SV.Framework.Catalog;

namespace avii.product
{
    public partial class manage_products : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["adm"] == null)
            {
                string url = "/avii/logon.aspx";
                try
                {
                    url =  ConfigurationSettings.AppSettings["LogonPage"].ToString();

                }
                catch
                {
                    url = "/avii/logon.aspx";
                }
                Response.Redirect(url);
            }

            if (!IsPostBack)
            {
                BindProductType();
                BindProductCondition();
                bindCategory(ddlCategoryFilter);
                bindMaker(ddlMaker);
                bindtechnology();
                BindCompany();
                //bindsku();
                BindWarehouseCode(0);
                FillSearchResult();
                
            }
        }
        private void FillSearchResult()
        {
            
            if (Request["search"] != null)
            {
                if (Session["productsearch"] != null)
                {
                    string searchCriteria = (string)Session["productsearch"];
                    string[] arr = searchCriteria.Split('~');

                    ddlCategoryFilter.SelectedValue = arr[0];


                    ddlMaker.SelectedValue = arr[1];

                    ddlTechnology.SelectedValue = arr[2];
                    txtModel.Text = arr[3];
                    txtSKU.Text = arr[4]; txtUPC.Text = arr[5];
                    txtColor.Text = arr[6];
                    ddlCompany.SelectedValue = arr[7];
                    txtItemCode.Text = arr[8];
                    ddlWhCode.SelectedValue = arr[9];
                    ddlActive.SelectedValue = arr[10];
                    ddlCatalog.SelectedValue = arr[11];
                    ddlProductType.SelectedValue = arr[12];
                    ddlCondition.SelectedValue = arr[13];
                    chkStock.Checked = Convert.ToBoolean(arr[14]);


                    bindItems();

                    Session["productsearch"] = null;
                }

            }
        }
        private void BindProductType()
        {
            SV.Framework.Catalog.ProductController productController = SV.Framework.Catalog.ProductController.CreateInstance<SV.Framework.Catalog.ProductController>();

            List<ProductType> productTypes = productController.GetProductTypes(0);
            ddlProductType.DataSource = productTypes;
            ddlProductType.DataTextField = "Code";
            ddlProductType.DataValueField = "ProductTypeID";

            ddlProductType.DataBind();

            System.Web.UI.WebControls.ListItem newList = new System.Web.UI.WebControls.ListItem("", "0");
            ddlProductType.Items.Insert(0, newList);
        }
        private void BindProductCondition()
        {
            SV.Framework.Catalog.ProductController productController = SV.Framework.Catalog.ProductController.CreateInstance<SV.Framework.Catalog.ProductController>();

            List<ProductCondition> productConditions = productController.GetProductCondition(0);
            ddlCondition.DataSource = productConditions;
            ddlCondition.DataTextField = "Code";
            ddlCondition.DataValueField = "ConditionID";

            ddlCondition.DataBind();

            System.Web.UI.WebControls.ListItem newList = new System.Web.UI.WebControls.ListItem("", "0");
            ddlCondition.Items.Insert(0, newList);
        }

        private void BindCompany()
        {
            ddlCompany.DataSource = avii.Classes.RMAUtility.getRMAUserCompany(-1, "", -1, -1);
            ddlCompany.DataValueField = "companyID";
            ddlCompany.DataTextField = "companyName";
            ddlCompany.DataBind();
            ddlCompany.Items.Insert(0, new ListItem("", "0"));
            
        }

        private void bindtechnology()
        {
            SV.Framework.Catalog.CarriersOperation carriersOperation = SV.Framework.Catalog.CarriersOperation.CreateInstance<SV.Framework.Catalog.CarriersOperation>();

            int active = 0;
            //if (chkTechnology.Checked)
            //    active = -1;

            ddlTechnology.DataSource = carriersOperation.GetCarriersList(-1, active);
            ddlTechnology.DataTextField = "CarrierName";
            ddlTechnology.DataValueField = "CarrierGUID";
            ddlTechnology.DataBind();
            ListItem item = new ListItem("--Select Carriers--", "0");
            ddlTechnology.Items.Insert(0, item);
            ddlTechnology.Enabled = true;
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
            
            //for (int ictr = 0; ictr < lstItemCategoryList.Count; ictr++)
            //{
            //    catid = lstItemCategoryList[ictr].CategoryGUID;
            //    parentcat = lstItemCategoryList[ictr].ParentCategoryGUID;
            //    if (0 == parentcat)
            //    {
            //        parentcat = catid;
            //        catid = -1;
            //    }
            //    li = new ListItem(lstItemCategoryList[ictr].CategoryName, catid.ToString() + ":" + parentcat.ToString());
            //    ddlCategory.Items.Add(li);
            //}
        }

        private void bindMaker(DropDownList ddlMaker)
        {
            SV.Framework.Catalog.ProductController productController = SV.Framework.Catalog.ProductController.CreateInstance<SV.Framework.Catalog.ProductController>();

            int catid = -1, parentcat = -1;
            int iactive = 1;
            ddlMaker.Items.Clear();

            
            //if (chkactive.Checked)
            //    iactive = -1;

            List<Maker> lstItemCategoryList = productController.getMakerList(-1, "", -1, iactive, -1, -1, -1, -1);
            ListItem li = new ListItem("--Select Maker--", "0");
            //ddlMaker.Items.Insert(0, li);
            
            for (int ictr = 0; ictr < lstItemCategoryList.Count; ictr++)
            {
                catid = lstItemCategoryList[ictr].MakerGUID;
               
                li = new ListItem(lstItemCategoryList[ictr].MakerName, catid.ToString() + ":" + parentcat.ToString());
                ddlMaker.Items.Add(li);
            }
        }
        private void BindWarehouseCode(int compnayID)
        {
            SV.Framework.Catalog.ProductController productController = SV.Framework.Catalog.ProductController.CreateInstance<SV.Framework.Catalog.ProductController>();

            List<CustomerWarehouseCode> warehuseCodeList = productController.GetCompanyWarehouseCode(compnayID, null, true);
            if (warehuseCodeList != null && warehuseCodeList.Count > 0)
            {
                ddlWhCode.DataSource = warehuseCodeList;
                ddlWhCode.DataValueField = "CompanyID";
                ddlWhCode.DataTextField = "WHCodecompanyName";
                ddlWhCode.DataBind();
                ListItem item = new ListItem("--Select Warehouse Code--", "0");
                ddlWhCode.Items.Insert(0, item);
            }

        }
        private void bindItems()
        {
            string upc, model, sku, color, itemCode, warehouseCode;
            string sortExpression = "ItemName";
            string sortDirection = "ASC";
            ViewState["SortDirection"] = sortDirection;
            ViewState["SortExpression"] = sortExpression;

            SV.Framework.Catalog.ProductController productController = SV.Framework.Catalog.ProductController.CreateInstance<SV.Framework.Catalog.ProductController>();

            lblMsg.Text = string.Empty;
            int categoryGUID = -1;
            int makerGUID = -1;
            int companyId = -1;
            int technologyGUID = -1;
            int showunderCatalog = -1;
            int active = -1;
            int productTypeID = 0, conditionID=0;
            bool reStock = false;

            upc = model = sku = color = itemCode = warehouseCode = string.Empty;
            if (ddlActive.SelectedIndex > 0)
            {
                active = Convert.ToInt32(ddlActive.SelectedValue);
            }
            if (ddlCatalog.SelectedIndex > 0)
                showunderCatalog = Convert.ToInt32(ddlCatalog.SelectedValue); ;

            if (ddlCategoryFilter.SelectedIndex > 0)
            {
                categoryGUID = Convert.ToInt32(ddlCategoryFilter.SelectedValue);
            }      

            if (ddlMaker.SelectedValue.IndexOf(":") > 0)
            {
                String[] strVal = ddlMaker.SelectedValue.Split(':');
                makerGUID = Convert.ToInt32(strVal[0]);
            }
            if (ddlTechnology.SelectedIndex > 0)
            {
                technologyGUID = Convert.ToInt32(ddlTechnology.SelectedValue.Trim());
            }           

            if (ddlWhCode.SelectedIndex > 0)
            {
                warehouseCode = ddlWhCode.SelectedItem.Text;
                string[] warehouseCodeArr = warehouseCode.Split('-');
                warehouseCode = warehouseCodeArr[0];
                warehouseCode = warehouseCode.Trim();
                companyId = Convert.ToInt32(ddlWhCode.SelectedValue.Trim());
            }

            if (ddlCompany.SelectedIndex > 0)
            {
                companyId = Convert.ToInt32(ddlCompany.SelectedValue.Trim());
            }

            warehouseCode = txtWhCode.Text.Trim();
            upc = txtUPC.Text.Trim();
            color = txtColor.Text.Trim();
            sku = txtSKU.Text.Trim();
            itemCode = txtItemCode.Text.Trim();
            model = txtModel.Text.Trim();
            if(ddlProductType.SelectedIndex > 0)
            {
                productTypeID = Convert.ToInt32(ddlProductType.SelectedValue);
            }
            if (ddlCondition.SelectedIndex > 0)
            {
                conditionID = Convert.ToInt32(ddlCondition.SelectedValue);
            }
            reStock = chkStock.Checked;

            string searchCriteria = ddlCategoryFilter.SelectedValue + "~" + ddlMaker.SelectedValue + "~" + ddlTechnology.SelectedValue + "~" + model + "~" + sku + "~" + upc + "~" + color + "~" + ddlCompany.SelectedValue + "~" + itemCode + "~" + ddlWhCode.SelectedValue + "~" + active + "~" + showunderCatalog + "~" + productTypeID + "~" + conditionID + "~" + reStock;
            Session["productsearch"] = searchCriteria;
            //if (ddlSku.SelectedIndex > 0)
            //{
            //    sku = ddlSku.SelectedValue;
            //}

            //if (string.IsNullOrEmpty(upc) && string.IsNullOrEmpty(color) && string.IsNullOrEmpty(model) && string.IsNullOrEmpty(sku) && string.IsNullOrEmpty(itemCode) &&
            //    categoryGUID == -1 && companyId == -1 && technologyGUID == -1 && makerGUID == -1 && showunderCatalog == -1 && active == -1)
            //{
            //    lblMsg.Text = "Please select the search criteria";
            //    grvItem.DataSource = null;
            //}
            //else
            {
                List<InventoryItems> items = productController.getItemList(categoryGUID, -1, active, makerGUID, technologyGUID, model, 0, sku, upc, color, 
                    companyId, itemCode, warehouseCode, showunderCatalog, productTypeID, conditionID, reStock);
                if (items != null && items.Count > 0)
                {
                    grvItem.DataSource = items;
                    Session["items"] = items;

                }
                else
                {
                    grvItem.DataSource = null;
                    Session["items"] = null;
                    lblMsg.Text = "No record exists for selected criteria";
                }
            }
            grvItem.DataBind();

        }

        //protected void bindsku()
        //{ 
        
        //    ProductController objProductController = new ProductController();
        //    List<avii.Classes.CompanySKUno> lstItemsku = objProductController.getCompanySKUnoList(-1,-1,"");
        //    ddlSku.DataSource = lstItemsku;
        //    ddlSku.DataTextField = "SKU";
        //    ddlSku.DataValueField = "SKU";
        //    ddlSku.DataBind();
        //    ListItem item = new ListItem("--Select Sku--","0");
        //    ddlSku.Items.Insert(0, item);
        //}

        protected void ddlCategoryType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //lblMsg.Text = string.Empty;
            bindItems();
        }

        protected void chkTechnology_CheckedChanged(object sender, EventArgs e)
        {
            bindtechnology();
        }

        protected void ddlTechnology_SelectedIndexChanged(object sender, EventArgs e)
        {
            //lblMsg.Text = string.Empty;
            bindItems();
        }
        //protected void ddlSku_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    lblMsg.Text = string.Empty;
        //    bindItems();
        //}

        protected void grvItem_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                
                string[] pagename = Request.Url.Segments;
                List<avii.Classes.AccessControlMapping> Accesscontrollist = new List<avii.Classes.AccessControlMapping>();
                avii.Classes.AccessControlMappingUtility objAccesscontrol = new avii.Classes.AccessControlMappingUtility();
                Image edit = (Image)e.Row.FindControl("ImageEdit");
                ImageButton delete = (ImageButton)e.Row.FindControl("lnkItemDelete");
                edit.Visible = false;
                delete.Visible = false;
                avii.Classes.user_utility objUserUtility = new avii.Classes.user_utility();
                string loginurl = ConfigurationManager.AppSettings["url"].ToString();
                if (Session["userInfo"] == null)
                    Response.Redirect(loginurl);

                avii.Classes.UserInfo userList = (avii.Classes.UserInfo)Session["userInfo"];
                string usertype = System.Configuration.ConfigurationManager.AppSettings["UserType"];
                string entitytype = string.Empty;
                if (userList.UserType == usertype)
                    entitytype = "adm";
                else
                    entitytype = "usr";

                List<avii.Classes.UserRole> roleList = userList.ActiveRoles;
                List<avii.Classes.UserPermission> permissionlist = objUserUtility.getUserPermissionList(pagename[pagename.Length - 1].ToString(), roleList, entitytype);
                for (int k = 0; k < permissionlist.Count; k++)
                {
                    Accesscontrollist = objAccesscontrol.getmappingControls(pagename[pagename.Length - 1].ToString(), permissionlist[k].PermissionGUID, entitytype);
                    Control controlid = new Control();
                    if (Accesscontrollist.Count > 0)
                    {
                        string ct = Accesscontrollist[0].Control.ToString();
                        Control linkcontrol = (Control)e.Row.FindControl(ct);
                        if (Accesscontrollist[0].Mode == true)
                            if (ct == "ImageEdit")
                                edit.Visible = true;
                        if (ct == "lnkItemDelete")
                            delete.Visible = true;
                    }
                }
            }
        }

        protected void grvItem_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grvItem.EditIndex = e.NewEditIndex;
        }

        protected void btnAddNewitem_click(object sender, EventArgs e)
        {
            Response.Redirect("detail-product.aspx");
        }

        

        protected void grvItem_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvItem.PageIndex = e.NewPageIndex;
            bindItems();
        }
        protected void imgDocument_click(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {
            int itemGUID = Convert.ToInt32(e.CommandArgument);
        }
        
        protected void ItemDelete_click(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {
            int itemGUID = Convert.ToInt32(e.CommandArgument);
            SV.Framework.Catalog.ProductController productController = SV.Framework.Catalog.ProductController.CreateInstance<SV.Framework.Catalog.ProductController>();

            //ProductController objProduct = new ProductController();
            int userID = Convert.ToInt32(Session["UserID"]);
            string sql = "";
            if (hdnOrderFlag.Value == "1")
            {
                //sql = "exec av_item_deactivate " + itemGUID;
                //objProduct.deleteItem(sql);
                productController.DeleteItems(itemGUID, 1, userID);

            }
            else
            {
                //sql = "exec av_item_delete " + itemGUID;
                //objProduct.deleteItem(sql);
                productController.DeleteItems(itemGUID, 0, userID);

            }
            bindItems();
            lblMsg.Text = "Deleted successfully";
        }

        protected void btnItemSearch_click(object sender, EventArgs e)
        {
            bindItems();
        }

        protected void btnCancel_click(object sender, EventArgs e)
        {
            txtItemCode.Text = string.Empty;
            txtUPC.Text = string.Empty;
            txtModel.Text = string.Empty;
            txtSKU.Text = string.Empty;
            txtColor.Text = string.Empty;
            chkStock.Checked = false;
            //ddlSku.SelectedIndex = 0;
            ddlCategoryFilter.SelectedIndex = 0;
            ddlProductType.SelectedIndex = 0;
            ddlCondition.SelectedIndex = 0;
            ddlCompany.SelectedIndex = 0;
            ddlTechnology.SelectedIndex = 0;
            ddlMaker.SelectedIndex = 0;
            lblMsg.Text = string.Empty;
            this.grvItem.DataSource = null;
            grvItem.DataBind();
            ddlWhCode.SelectedIndex = 0;
        }

        protected void chkactive_CheckedChanged(object sender, EventArgs e)
        {
            bindCategory(ddlCategoryFilter);
        }

        protected void chkbrand_CheckedChanged(object sender, EventArgs e)
        {
            bindMaker(ddlMaker);
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
                List<InventoryItems> items = (List<InventoryItems>)Session["items"];

                if (items != null && items.Count > 0)
                {
                    
                    if (Sortdir == "ASC")
                    {
                        items = Sort<InventoryItems>(items, SortExp, SortDirection.Ascending);
                    }
                    else
                    {
                        items = Sort<InventoryItems>(items, SortExp, SortDirection.Descending);
                    }
                    Session["items"] = items;
                    grvItem.DataSource = items;
                    grvItem.DataBind();
                }
            }
        }

        protected void imgView_Command(object sender, CommandEventArgs e)
        {
            int itemGUID = Convert.ToInt32(e.CommandArgument);
            if(itemGUID>0)
            {
                Session["itemguid"] = itemGUID;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "temp", "<script language='javascript'>OpenNewPage('ProductLog.aspx')</script>", false);

               // Response.Redirect("ProductLog.aspx", true);
            }
        }
    }
}
