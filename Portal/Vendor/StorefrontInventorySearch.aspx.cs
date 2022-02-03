using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace avii.Vendor
{
    public partial class StorefrontInventorySearch : System.Web.UI.Page
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
                BindCompany();
                BindStorefront();
                BindCondition();
            }
        }
        private void BindProducts()
        {
            int companyID = 0;
            string productSource = "", SKU = "", UPC = "", Title = "", condition = "", status = "";
            //bool isSync = false;
            lblMsg.Text = "";
            lblNote.Text = "";
            lblCount.Text = "";
            btnDownload.Visible = false;
            btnSync.Visible = false;
            gvProducts.DataSource = null;
            gvProducts.DataBind();
            try
            {
                if (ddlStorefront.SelectedIndex > 0)
                {
                    if (dpCompany.SelectedIndex > 0)
                    {
                        if (chkGet.Checked)
                        {
                            string authCode = txtToken.Text;
                            string url = ConfigurationSettings.AppSettings["ebaymgmtinventory"].ToString();
                            SV.Framework.Storefront.Product.GetProductRequest request = new SV.Framework.Storefront.Product.GetProductRequest();
                            if (!string.IsNullOrEmpty(authCode))
                            {
                                request.authCode = txtToken.Text;
                                request.limit = 25;
                                request.offset = 0;
                                request.requestType = "GET";
                                GetStorefrontInventory(url, request);
                            }
                            else
                                lblMsg.Text = "Token is required!";
                        }
                        else
                        {
                            productSource = ddlStorefront.SelectedValue;
                            companyID = Convert.ToInt32(dpCompany.SelectedValue);
                            SKU = txtSKU.Text.Trim();
                            UPC = txtUPC.Text.Trim();
                            Title = txtTitle.Text.Trim();
                            if (ddlCondition.SelectedIndex > 0)
                                condition = ddlCondition.SelectedValue;

                            status = ddlStatus.SelectedValue;

                            List<SV.Framework.Storefront.Product.ProductRequestModel> productList = SV.Framework.Storefront.Product.ProductOperation.GetInventoty(productSource, companyID, SKU, Title, condition, UPC, status);

                            if (productList != null && productList.Count > 0)
                            {
                                Session["productList"] = productList;
                                gvProducts.DataSource = productList;
                                gvProducts.DataBind();
                                // btnDownload.Visible = true;
                                btnSync.Visible = true;
                                btnSync.Text = "Sync to storefront";

                                lblCount.Text = "<b>Total Count: " + productList.Count + "</b>";
                                lblNote.Text = "(Retrieved record(s) from databse)";
                            }
                            else
                                lblMsg.Text = "No record found";
                        }
                    }
                    else
                        lblMsg.Text = "Customer is required!";
                }
                else
                {
                    lblMsg.Text = "Storefront is required!";
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "";
            BindProducts();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "stop loader", "StopProgress();", true);
        }
        protected void btnGetToken_Click(object sender, EventArgs e)
        {
            //         <!--<add key="ebayordersandbox" value="https://auth.sandbox.ebay.com/oauth2/authorize?client_id=stkmanag-langloba-SBX-77ffb2180-30f97435&response_type=code&redirect_uri=stkmanagement-stkmanag-langlo-akkji&scope=https://api.ebay.com/oauth/api_scope+https://api.ebay.com/oauth/api_scope/sell.fulfillment+https://api.ebay.com/oauth/api_scope/sell.fulfillment.readonly&state=current-page" />
            //<add key="ebayorderprod" value="https://auth.ebay.com/oauth2/authorize?client_id=stkmanag-langloba-PRD-91d9af84c-be224c1b&response_type=code&redirect_uri=stkmanagement-stkmanag-langlo-vmasgpbws&scope=https://api.ebay.com/oauth/api_scope+https://api.ebay.com/oauth/api_scope/sell.fulfillment+https://api.ebay.com/oauth/api_scope/sell.fulfillment.readonly&state=current-page" />
            //<add key="ebayinventorysandbox" value="https://auth.sandbox.ebay.com/oauth2/authorize?client_id=stkmanag-langloba-SBX-77ffb2180-30f97435&response_type=code&redirect_uri=stkmanagement-stkmanag-langlo-akkji&scope=https://api.ebay.com/oauth/api_scope+https://api.ebay.com/oauth/api_scope/sell.fulfillment+https://api.ebay.com/oauth/api_scope/sell.fulfillment.readonly+https://api.ebay.com/oauth/api_scope/sell.inventory+https://api.ebay.com/oauth/api_scope/sell.inventory.readonly&state=current-page" />
            //<add key="ebayinventoryprod" value="https://auth.ebay.com/oauth2/authorize?client_id=stkmanag-langloba-PRD-91d9af84c-be224c1b&response_type=code&redirect_uri=stkmanagement-stkmanag-langlo-vmasgpbws&scope=https://api.ebay.com/oauth/api_scope+https://api.ebay.com/oauth/api_scope/sell.fulfillment+https://api.ebay.com/oauth/api_scope/sell.fulfillment.readonly+https://api.ebay.com/oauth/api_scope/sell.inventory+https://api.ebay.com/oauth/api_scope/sell.inventory.readonly&state=current-page&state=current-page" />-->

            string ebayUrl = "https://auth.ebay.com/oauth2/authorize?client_id=stkmanag-langloba-PRD-91d9af84c-be224c1b&response_type=code&redirect_uri=stkmanagement-stkmanag-langlo-vmasgpbws&scope=https://api.ebay.com/oauth/api_scope+https://api.ebay.com/oauth/api_scope/sell.fulfillment+https://api.ebay.com/oauth/api_scope/sell.fulfillment.readonly+https://api.ebay.com/oauth/api_scope/sell.inventory+https://api.ebay.com/oauth/api_scope/sell.inventory.readonly&state=current-page&state=current-page";//ConfigurationSettings.AppSettings["ebayinventoryprod"].ToString();
            if (Request["op"] != null && Request["op"].ToString().ToLower() == "t")
            {
                ebayUrl = "https://auth.sandbox.ebay.com/oauth2/authorize?client_id=stkmanag-langloba-SBX-77ffb2180-30f97435&response_type=code&redirect_uri=stkmanagement-stkmanag-langlo-akkji&scope=https://api.ebay.com/oauth/api_scope+https://api.ebay.com/oauth/api_scope/sell.fulfillment+https://api.ebay.com/oauth/api_scope/sell.fulfillment.readonly+https://api.ebay.com/oauth/api_scope/sell.inventory+https://api.ebay.com/oauth/api_scope/sell.inventory.readonly&state=current-page"; // ConfigurationSettings.AppSettings["ebayinventorysandbox"].ToString();
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "temp", "<script language='javascript'>OpenNewPage('"+ ebayUrl +"')</script>", false);

        }
        protected void btnGet_Click(object sender, EventArgs e)
        {
           
            string authCode = txtToken.Text;
            string url = ConfigurationSettings.AppSettings["ebaymgmtinventory"].ToString();
            SV.Framework.Storefront.Product.GetProductRequest request = new SV.Framework.Storefront.Product.GetProductRequest();
            request.authCode = txtToken.Text;
            request.limit = 25;
            request.offset = 0;
            request.requestType = "GET";
            GetStorefrontInventory(url, request);

            ScriptManager.RegisterStartupScript(this, this.GetType(), "stop loader", "StopProgress();", true);

        }

        private DataTable LoadProducts(List<SV.Framework.Storefront.Product.ProductRequestModel> productList )
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("SKU", typeof(System.String));
            dt.Columns.Add("UPC", typeof(System.String));
            dt.Columns.Add("ItemName", typeof(System.String));
            dt.Columns.Add("ItemDescription", typeof(System.String));
            dt.Columns.Add("OpeningStock", typeof(System.Int32));
            dt.Columns.Add("CountryOrRegion", typeof(System.String));
            dt.Columns.Add("Condition", typeof(System.String));
            dt.Columns.Add("ConditionDesc", typeof(System.String));
            dt.Columns.Add("Locale", typeof(System.String));
            DataRow row;
            int index = 0;
            bool IsValid = false;
            foreach (GridViewRow row2 in gvProducts.Rows)
            {
                CheckBox chkItem = row2.FindControl("chkItem") as CheckBox;
                if (chkItem.Checked)
                {
                    IsValid = true;
                    break;
                }
            }
            if (IsValid)
            {
                foreach (GridViewRow row1 in gvProducts.Rows)
                {
                    CheckBox chkItem = row1.FindControl("chkItem") as CheckBox;
                    if (chkItem.Checked)
                    {
                        row = dt.NewRow();
                        row["SKU"] = productList[index].SKU;
                        row["UPC"] = productList[index].UPC;
                        row["ItemName"] = productList[index].ItemName;
                        row["ItemDescription"] = productList[index].ItemDescription;
                        row["OpeningStock"] = productList[index].OpeningStock;
                        row["CountryOrRegion"] = productList[index].CountryOrRegion;
                        row["Condition"] = productList[index].Condition;
                        row["ConditionDesc"] = productList[index].ConditionDesc;
                        row["Locale"] = productList[index].Locale;
                        dt.Rows.Add(row);

                    }

                    index = index + 1;
                }
            }
            else if (!IsValid)
            {
                lblMsg.Text = "Please select product!";
            }
            return dt;

        }
        protected void btnSyncFrom_Click(object sender, EventArgs e)
        {
            int userID = Convert.ToInt32(Session["UserID"]);
            int companyID = 0;
            string returnMessage = "";
            if (dpCompany.SelectedIndex > 0)
            {
                companyID = Convert.ToInt32(dpCompany.SelectedValue);
                
                lblMsg.Text = "";
                List<SV.Framework.Storefront.Product.ProductRequestModel> productList = Session["productList"] as List<SV.Framework.Storefront.Product.ProductRequestModel>;
                if (productList != null && productList.Count > 0)
                {
                    DataTable dt = LoadProducts(productList);
                    SV.Framework.Storefront.Product.ProductOperation.InventorySyncFromStorefrontUpdate(dt, userID, companyID, out returnMessage);
                    lblMsg.Text = returnMessage;
                }
            }
            else
                lblMsg.Text = "Customer is required!";

        }
        private void SyncFromStorefront()
        {
            int userID = Convert.ToInt32(Session["UserID"]);
            int companyID = 0;
            string returnMessage = "";
            if (dpCompany.SelectedIndex > 0)
            {
                companyID = Convert.ToInt32(dpCompany.SelectedValue);

                lblMsg.Text = "";
                List<SV.Framework.Storefront.Product.ProductRequestModel> productList = Session["productList"] as List<SV.Framework.Storefront.Product.ProductRequestModel>;
                if (productList != null && productList.Count > 0)
                {
                    DataTable dt = LoadProducts(productList);
                    SV.Framework.Storefront.Product.ProductOperation.InventorySyncFromStorefrontUpdate(dt, userID, companyID, out returnMessage);
                    BindProducts();
                    lblMsg.Text = returnMessage;                    
                }
            }
            else
                lblMsg.Text = "Customer is required!";
        }
        protected void btnSync_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "";
            
            try
            {
                int userID = Convert.ToInt32(Session["UserID"]);
                int CompanID = 0;
                string authCode = txtToken.Text;
                string url = ConfigurationSettings.AppSettings["ebaymgmtinventory"].ToString();
                if (!chkGet.Checked)
                {
                    if (dpCompany.SelectedIndex > 0)
                    {
                        CompanID = Convert.ToInt32(dpCompany.SelectedValue);
                        if (!string.IsNullOrEmpty(authCode))
                        {
                            SV.Framework.Storefront.Product.StorefrontProduct storefrontProduct = new SV.Framework.Storefront.Product.StorefrontProduct();
                            List<SV.Framework.Storefront.Product.ProductRequest> requests = new List<SV.Framework.Storefront.Product.ProductRequest>();
                            SV.Framework.Storefront.Product.ProductRequest productRequest = null;
                            SV.Framework.Storefront.Product.ProductInfo product = new SV.Framework.Storefront.Product.ProductInfo();
                            SV.Framework.Storefront.Product.Availability availability = new SV.Framework.Storefront.Product.Availability();
                            SV.Framework.Storefront.Product.ShipToLocationAvailability shipToLocationAvailability = new SV.Framework.Storefront.Product.ShipToLocationAvailability();
                            storefrontProduct.authCode = authCode;
                            storefrontProduct.requestType = "ADD";
                            int index = 0;
                            bool IsValid = false;
                            lblMsg.Text = "";
                            List<SV.Framework.Storefront.Product.ProductRequestModel> productList = Session["productList"] as List<SV.Framework.Storefront.Product.ProductRequestModel>;
                            if (productList != null && productList.Count > 0)
                            {
                                foreach (GridViewRow row in gvProducts.Rows)
                                {
                                    CheckBox chkItem = row.FindControl("chkItem") as CheckBox;
                                    if (chkItem.Checked)
                                    {
                                        IsValid = true;
                                        break;
                                    }
                                }
                                if (IsValid)
                                {
                                    foreach (GridViewRow row in gvProducts.Rows)
                                    {
                                        CheckBox chkItem = row.FindControl("chkItem") as CheckBox;
                                        if (chkItem.Checked)
                                        {
                                            productRequest = new SV.Framework.Storefront.Product.ProductRequest();
                                            availability = new SV.Framework.Storefront.Product.Availability();
                                            product = new SV.Framework.Storefront.Product.ProductInfo();
                                            Dictionary<string, string[]> aspects = new Dictionary<string, string[]>();
                                            string[] dicKeyvalue1 = new string[1];
                                            dicKeyvalue1[0] = productList[index].CountryOrRegion;
                                            aspects.Add("Country/Region of Manufacture", dicKeyvalue1);
                                            string[] dicKeyvalue2 = new string[1];
                                            dicKeyvalue2[0] = productList[index].UPC;
                                            aspects.Add("UPC", dicKeyvalue2);

                                            string[] dicKeyvalue3 = new string[1];
                                            dicKeyvalue3[0] = "Langlobal"; //productList[index].b;

                                            aspects.Add("Brand", dicKeyvalue3);

                                            shipToLocationAvailability = new SV.Framework.Storefront.Product.ShipToLocationAvailability();
                                            productRequest.condition = productList[index].Condition;
                                            productRequest.conditionDescription = productList[index].ConditionDesc;
                                            productRequest.locale = productList[index].Locale;
                                            productRequest.sku = productList[index].SKU;
                                            shipToLocationAvailability.quantity = productList[index].OpeningStock;
                                            availability.shipToLocationAvailability = shipToLocationAvailability;
                                            productRequest.availability = availability;
                                            product.description = productList[index].ItemDescription;
                                            //product.imageUrls = "";
                                            product.title = productList[index].ItemName;
                                            product.aspects = aspects;
                                            productRequest.product = product;
                                            requests.Add(productRequest);
                                        }

                                        index = index + 1;
                                    }
                                    storefrontProduct.requests = requests;

                                    SyncStorefrontInventory(url, storefrontProduct, userID, CompanID);


                                }
                                else if (!IsValid)
                                {
                                    lblMsg.Text = "Please select check box in the grid to sync the product(s)!";
                                }
                            }
                        }
                        else
                            lblMsg.Text = "Token is required!";
                    }
                    else
                        lblMsg.Text = "Customer is required!";
                }
                else
                {
                    SyncFromStorefront();
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "stop loader", "StopProgress();", true);
        }
        public async void SyncStorefrontInventory(string url, SV.Framework.Storefront.Product.StorefrontProduct request, int userID, int CompanID)
        {
            try
            {
                string errorMessage = "";
                SV.Framework.Storefront.Product.AddProductResponse response = await SV.Framework.Storefront.Product.ProductOperation.AddeBayInventory(url, request);
                if (response != null && response.Responses != null && response.Responses.Count > 0)
                {
                    int returnResult = SV.Framework.Storefront.Product.ProductOperation.InventorySyncUpdate(response, userID, CompanID, out errorMessage);
                    BindProducts();
                    lblMsg.Text = errorMessage;
                    
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Error occoured while adding inventory :Exception occured while fetching token"))
                    lblMsg.Text = "Error occoured while adding inventory :Exception occured while fetching token";
                else
                    lblMsg.Text = ex.Message;
            }
        }
        public async void GetStorefrontInventory(string url, SV.Framework.Storefront.Product.GetProductRequest request)
        {
            try
            {
                lblNote.Text = "";
                
                lblMsg.Text = "";
                lblCount.Text = "";
                gvProducts.DataSource = null;
                gvProducts.DataBind();
                List<SV.Framework.Storefront.Product.ProductRequestModel> productList = new List<SV.Framework.Storefront.Product.ProductRequestModel>();
                SV.Framework.Storefront.Product.GeteBayProductResponse response = await SV.Framework.Storefront.Product.ProductOperation.GeteBayInventory(url, request);
                SV.Framework.Storefront.Product.ProductRequestModel productModel = null;

                if (response != null && response.inventoryItems != null && response.inventoryItems.Count > 0)
                {
                    foreach (SV.Framework.Storefront.Product.ProductRequest item in response.inventoryItems)
                    {
                        productModel = new SV.Framework.Storefront.Product.ProductRequestModel();
                        //productModel.Active = true;
                        productModel.Condition = item.condition;
                        productModel.ConditionDesc = item.conditionDescription;
                        string[] CountryOrRegion = item.product.aspects["Country/Region of Manufacture"];
                        //string[] UPC = item.product.aspects["UPC"];
                        //string[] Brand = item.product.aspects["Brand"];
                        productModel.CountryOrRegion = CountryOrRegion[0];
                        productModel.UPC = "";// UPC[0];
                        //productModel.b = Brand[0];

                        productModel.ItemDescription = item.product.description;
                        productModel.ItemName = item.product.title;
                        productModel.SKU = item.sku;
                        productModel.Locale = item.locale;
                        productModel.OpeningStock = item.availability.shipToLocationAvailability.quantity;
                        productList.Add(productModel);

                        // productModel.Condition = item.condition
                    }
                    Session["productList"] = productList;
                    gvProducts.DataSource = productList;
                    gvProducts.DataBind();
                    //  btnDownload.Visible = true;
                    btnSync.Visible = true;
                    btnSync.Text = "Sync from storefront";
                    gvProducts.Columns[9].Visible = false;
                    //btnDownload.Visible = true;

                    lblCount.Text = "<b>Total Count: " + productList.Count + "</b>";
                    lblNote.Text = "(Retrieved record(s) from Storefront)";
                }
                else
                {
                    lblMsg.Text = "No record found!";
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Error occoured while getting inventory :Exception occured while fetching token"))
                    lblMsg.Text = "Error occoured while getting inventory :Exception occured while fetching token";
                else
                    lblMsg.Text = ex.Message;
            }
            
        }
        protected void btnDownload_Click(object sender, EventArgs e)
        {
            SV.Framework.Storefront.Product.StorefrontProduct storefrontProduct = new SV.Framework.Storefront.Product.StorefrontProduct();
            List<SV.Framework.Storefront.Product.ProductRequest> requests = new List<SV.Framework.Storefront.Product.ProductRequest>();
            SV.Framework.Storefront.Product.ProductRequest productRequest = null;
            SV.Framework.Storefront.Product.ProductInfo product = new SV.Framework.Storefront.Product.ProductInfo();
            SV.Framework.Storefront.Product.Availability availability = new SV.Framework.Storefront.Product.Availability();
            SV.Framework.Storefront.Product.ShipToLocationAvailability shipToLocationAvailability = new SV.Framework.Storefront.Product.ShipToLocationAvailability();
            storefrontProduct.authCode = "v%5E1.1%23i%5E1%23p%5E3%23I%5E3%23f%5E0%23r%5E1%23t%5EUl41Xzk6RkFCMzFCQzNFRTdDOUU1MUIyRTk1MTZDOUI2NzMwMDlfMF8xI0VeMTI4NA%3D%3D";
            storefrontProduct.requestType = "ADD";
            int index = 0;
            bool IsValid = false;
            lblMsg.Text = "";
            List<SV.Framework.Storefront.Product.ProductRequestModel> productList = Session["productList"] as List<SV.Framework.Storefront.Product.ProductRequestModel>;
            if (productList != null && productList.Count > 0)
            {
                foreach (GridViewRow row in gvProducts.Rows)
                {
                    CheckBox chkItem = row.FindControl("chkItem") as CheckBox;
                    if (chkItem.Checked)
                    {
                        IsValid = true;
                        break;
                    }
                }
                if (IsValid)
                {
                    foreach (GridViewRow row in gvProducts.Rows)
                    {
                        CheckBox chkItem = row.FindControl("chkItem") as CheckBox;
                        if (chkItem.Checked)
                        {
                            productRequest = new SV.Framework.Storefront.Product.ProductRequest();
                            availability = new SV.Framework.Storefront.Product.Availability();
                            product = new SV.Framework.Storefront.Product.ProductInfo();
                            Dictionary<string, string[]> aspects = new Dictionary<string, string[]>();
                            string[] dicKeyvalue1 = new string[1];
                            dicKeyvalue1[0] = productList[index].CountryOrRegion;
                            aspects.Add("Country/Region of Manufacture", dicKeyvalue1);
                            string[] dicKeyvalue2 = new string[1];
                            dicKeyvalue2[0] = productList[index].UPC;
                            aspects.Add("UPC", dicKeyvalue2);

                            string[] dicKeyvalue3 = new string[1];
                            dicKeyvalue3[0] = "Langlobal"; //productList[index].b;

                            aspects.Add("Brand", dicKeyvalue3);

                            shipToLocationAvailability = new SV.Framework.Storefront.Product.ShipToLocationAvailability();
                            productRequest.condition = productList[index].Condition;
                            productRequest.conditionDescription = productList[index].ConditionDesc;
                            productRequest.locale = productList[index].Locale;
                            productRequest.sku = productList[index].SKU;
                            shipToLocationAvailability.quantity = productList[index].OpeningStock;
                            availability.shipToLocationAvailability = shipToLocationAvailability;
                            productRequest.availability = availability;
                            product.description = productList[index].ItemDescription;
                            //product.imageUrls = "";
                            product.title = productList[index].ItemName;
                            product.aspects = aspects;
                            productRequest.product = product;
                            requests.Add(productRequest);
                        }

                        index = index + 1;
                    }
                    storefrontProduct.requests = requests;

                    string requestJson = Newtonsoft.Json.JsonConvert.SerializeObject(storefrontProduct);
                    MemoryStream ms = new MemoryStream();
                    TextWriter tw = new StreamWriter(ms);
                    tw.WriteLine(requestJson);
                    tw.Flush();
                    byte[] bytes = ms.ToArray();
                    ms.Close();

                    Response.Clear();
                    Response.ContentType = "application/force-download";
                    Response.AddHeader("content-disposition", "attachment;    filename=file.txt");
                    Response.BinaryWrite(bytes);
                    Response.End();
                }
                else if (!IsValid)
                {
                    lblMsg.Text = "Please select product!";
                }
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            lblNote.Text = "";
            lblCount.Text = "";
            lblMsg.Text = "";
            txtSKU.Text = "";
            txtUPC.Text = "";
            txtTitle.Text = "";
            ddlCondition.SelectedIndex = 0;
            dpCompany.SelectedIndex = 0;
            ddlStorefront.SelectedIndex = 1;
            ddlStatus.SelectedIndex = 0;
            //chkIsSync.Checked = false;
            btnDownload.Visible = false;
            btnSync.Visible = false;
            gvProducts.DataSource = null;
            gvProducts.DataBind();
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

        protected void imgView_Command(object sender, CommandEventArgs e)
        {
            int itemGUID = Convert.ToInt32(e.CommandArgument);

            Response.Redirect("~/product/detail-product.aspx?itemGUID=" + itemGUID);
        }

        protected void gvProducts_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //header select all function
            if (e.Row.RowType == DataControlRowType.Header)
            {
                ((CheckBox)e.Row.FindControl("allchk")).Attributes.Add("onclick",
                    "javascript:SelectAll('" +
                    ((CheckBox)e.Row.FindControl("allchk")).ClientID + "')");
            }
        }

        
    }
}