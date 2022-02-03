using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing;
//using avii.Classes;
using System.Linq;
using SV.Framework.Models.Catalog;

namespace avii.product
{
    public partial class detail_product : System.Web.UI.Page
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
            int itemGUID = 0;
            int imageGUID = 0;
          
            if (Request["itemGUID"] != "" && Request["itemGUID"] != null)
            {
                hdnTabindex.Value = "0";
                itemGUID = Convert.ToInt32(Request["itemGUID"]);
                hdnitemGUID.Value = Request["itemGUID"].ToString();

            }
            else
                btnUpdate.Text = "Submit Item";

            if (Request["imageGUID"] != "" && Request["imageGUID"] != null)
            {
                imageGUID = Convert.ToInt32(Request["imageGUID"]);
            }

            if (!IsPostBack)
            {
                BindProductType();
                BindProductCondition();
                bindCategory(ddlCategoryType);
                bindMaker(ddlMaker);
                BindCarriers();
               // bindItmeType();
                bindCompany();
                //BindSimCartSpintorLockTypes();
                //BindOperatingSystem();

                btnBackToSearch.Visible = false;
                if (itemGUID > 0)
                {
                    GetMappedSKUs(itemGUID);
                    BindCameraConfig();
                    bindPricetype();
                    bindImageType(ddlImageType1);
                    getItemDetails(itemGUID);
                    bindItmPricing(itemGUID);
                    bindSpecification(itemGUID);
                    bindItmImages(itemGUID);
                    bindAttributeValue(itemGUID);
                    bindItmcompSKU(itemGUID);
                    BindWarehouseCode(0);
                    BindItmCamera(itemGUID);
                    //btnCancel.Text = "Back to Search";
                    btnBackToSearch.Visible = true;
                }
                else
                {
                    chkActive.Checked = true;
                    chkAllowRMA.Checked = true;
                    //chkAllowLTE.Checked = true;
                }
            }

        }
        private void BindCameraConfig()
        {
            SV.Framework.Catalog.ProductController productController = SV.Framework.Catalog.ProductController.CreateInstance<SV.Framework.Catalog.ProductController>();

            List<CameraConfig> CameraConfigList = productController.GetCameraConfigList();
            ddlCameraConfig.DataSource = CameraConfigList;
            ddlCameraConfig.DataValueField = "CameraID";
            ddlCameraConfig.DataTextField = "CameraConfigs";
            ddlCameraConfig.DataBind();
            ListItem item = new ListItem("--Select Camera--", "0");
            ddlCameraConfig.Items.Insert(0, item);
        }
        private void SelectedIndexChanged()
        {
            if (ddlCompany.SelectedIndex > 0)
            {
                BindWarehouseCode(Convert.ToInt32(ddlCompany.SelectedValue));
            }
            else
                BindWarehouseCode(0);
        }
        protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
        { 
            SelectedIndexChanged();
        }
        private void BindWarehouseCode(int compnayID)
        {
            if (compnayID > 0)
            {
                SV.Framework.Catalog.ProductController productController = SV.Framework.Catalog.ProductController.CreateInstance<SV.Framework.Catalog.ProductController>();

                List<CustomerWarehouseCode> warehuseCodeList = productController.GetCompanyWarehouseCode(compnayID, null, true);
                if (warehuseCodeList != null && warehuseCodeList.Count > 0)
                {
                    ddlWhCode.DataSource = warehuseCodeList;
                    ddlWhCode.DataValueField = "WarehouseCode";
                    ddlWhCode.DataTextField = "WarehouseCode";
                    ddlWhCode.DataBind();
                    ListItem item = new ListItem("000", "000");
                    ddlWhCode.Items.Insert(0, item);
                }
                else
                {
                    ddlWhCode.Items.Clear();
                    //ddlWhCode.DataSource = null;
                    //ddlWhCode.DataBind();
                    ListItem item = new ListItem("000", "000");
                    ddlWhCode.Items.Insert(0, item);
                }
            }
            else
            {
                ddlWhCode.Items.Clear();
                //ddlWhCode.DataSource = null;
                //ddlWhCode.DataBind();
                ListItem item = new ListItem("000", "000");
                ddlWhCode.Items.Insert(0, item);
            }
        }
        protected void bindCompany()
        {
            ddlCompany.DataSource = avii.Classes.RMAUtility.getRMAUserCompany(-1, "", 1, -1);
            ddlCompany.DataValueField = "companyID";
            ddlCompany.DataTextField = "companyName";
            ddlCompany.DataBind();
            ListItem item = new ListItem("--All--", "0");
            ddlCompany.Items.Insert(0, item);

        }
        protected void bindAttributeValue(int itemGUID)
        {
            SV.Framework.Catalog.AttributeValueUtility attributeValueUtility = SV.Framework.Catalog.AttributeValueUtility.CreateInstance<SV.Framework.Catalog.AttributeValueUtility>();

            List<attributevalue> listattributevalue = attributeValueUtility.getattributevalueList(-1, -1, itemGUID, "");
            DlAttributeValue.DataSource = listattributevalue;
            DlAttributeValue.DataBind();
        }
        protected void BindSimCartSpintorLockTypes()
        { 
            //List<SimCardType> SimCardTypeList = new List<SimCardType>();
            //List<SpintorLockType> SpintorLockTypeList = new List<SpintorLockType>();
            //SimCardSpintorLockOperations.GetSimSpintorLockTypesList(out SimCardTypeList, out SpintorLockTypeList);
            //if (SimCardTypeList != null && SimCardTypeList.Count > 0)
            //{
            //    //ddlSimCardType.DataSource = SimCardTypeList;
            //    //ddlSimCardType.DataTextField = "SimCardTypeText";
            //    //ddlSimCardType.DataValueField = "SimCardTypeID";
            //    //ddlSimCardType.DataBind();

            //    ListItem item = new ListItem("--Select SimCardType--", "0");
            //    //ddlSimCardType.Items.Insert(0, item);
            //}
            //else
            //{
            //    ListItem item = new ListItem("--Select SimCardType--", "0");
               
            //}
            //if (SpintorLockTypeList != null && SpintorLockTypeList.Count > 0)
            //{
              
            //    ListItem item = new ListItem("--Select SpintorLockType--", "0");
               
            //}
            //else
            //{

            //    ListItem item = new ListItem("--Select SpintorLockType--", "0");
              
            //}
        
        }
        protected void BindCarriers()
        {
            SV.Framework.Catalog.CarriersOperation carriersOperation = SV.Framework.Catalog.CarriersOperation.CreateInstance<SV.Framework.Catalog.CarriersOperation>();

            //avii.Classes.ProductUtility obj = new avii.Classes.ProductUtility();


            //ddlTechnology1.DataSource = CarriersOperation.GetCarriersList(-1,-1);
            //ddlTechnology1.DataTextField = "CarrierName";
            //ddlTechnology1.DataValueField = "CarrierGUID";
            //ddlTechnology1.DataBind();

            //ListItem item = new ListItem("--Select Carriers--", "0");
            //ddlTechnology1.Items.Insert(0, item);

            lbTechnology.DataSource = carriersOperation.GetCarriersList(-1, -1);
            lbTechnology.DataTextField = "CarrierName";
            lbTechnology.DataValueField = "CarrierGUID";
            lbTechnology.DataBind();

            ListItem item = new ListItem("--Select Carriers--", "0");
            lbTechnology.Items.Insert(0, item);
            
        }
        protected void getItemDetails(int itemID)
        {
            SV.Framework.Catalog.ProductController productController = SV.Framework.Catalog.ProductController.CreateInstance<SV.Framework.Catalog.ProductController>();

           // ProductController objProduct = new ProductController();
            hdnitemGUID.Value = itemID.ToString();

            List<InventoryItems> lsitemList = productController.getItemList(-1, itemID, -1, -1, -1, "", -1, "", "", "", -1, "", "", -1, 0, 0, false);
           
            if (lsitemList != null && lsitemList.Count > 0)
            {
                ddlProductType.SelectedValue = lsitemList[0].ProductTypeID.ToString();
                ddlCondition.SelectedValue = lsitemList[0].ConditionID.ToString();
                chkStock.Checked = lsitemList[0].ReStock;

                if (lsitemList[0].EsnLength > 0)
                    txtEsnLength.Text = lsitemList[0].EsnLength.ToString();

                if (lsitemList[0].MeidLength > 0)
                    txtMeidLength.Text = lsitemList[0].MeidLength.ToString();

                if (lsitemList[0].StorageQty > 0)
                    txtStorageQty.Text = lsitemList[0].StorageQty.ToString();

                if (!lsitemList[0].IsESNRequired)
                    ddlMappedSKU.Enabled = false;

                if (lsitemList[0].ItemCategory.ToLower() == "kitted box")
                    ddlMappedSKU.Enabled = false;

                ddlStorage.SelectedValue = lsitemList[0].Storage;
                ddlOSType.SelectedValue = lsitemList[0].OSType;

                //  ddlOS.SelectedValue = lsitemList[0].OperationSystem.ToString();
                //   ddlScreenSize.SelectedValue = lsitemList[0].ScreenSize.ToString();
                txtProductName.Text = lsitemList[0].ItemName.ToString();
               // txtProductCode.Text = lsitemList[0].ItemCode.ToString();
                txtModelNumber.Text = lsitemList[0].ModelNumber.ToString();
                txtDescription.Text = lsitemList[0].ItemDesc1.ToString();
                txtFullDesc.Text = lsitemList[0].ItemDesc2.ToString();

                txtUPC.Text = lsitemList[0].Upc.ToString();
                //  txtColor.Text = lsitemList[0].ItemColor.ToString();
                //  if (lsitemList[0].DefaultPrice > 0)
                //      txtPrice.Text = lsitemList[0].DefaultPrice.ToString();

                ddlCategoryType.SelectedValue = lsitemList[0].CategoryWithProductAllowed;//lsitemList[0].ItemCategoryGUID.ToString();
                ddlMaker.SelectedValue = lsitemList[0].ItemMakerGUID.ToString();
               
                txtSKU.Text = lsitemList[0].SKU;

                ddlCompany.SelectedValue = lsitemList[0].CompanyGUID.ToString();
             //   ddlitemType.SelectedValue = lsitemList[0].Item_Type.ToString();

                //NEW MVNO changes
                if (lsitemList[0].Weight > 0)
                    txtWeight.Text = lsitemList[0].Weight.ToString();
               // ddlDisplayPriority.SelectedValue = lsitemList[0].DisplayPriority.ToString();
               // ddlSimCardType.SelectedValue = lsitemList[0].SimCardTypeID.ToString();
               // ddlSpintorLockType.SelectedValue = lsitemList[0].SpintorLockTypeID.ToString();



                if(!string.IsNullOrEmpty(lsitemList[0].ItemTechnology))
                {
                    string[] str = lsitemList[0].ItemTechnology.Split(',');
                    if (str != null && str.Length > 0)
                    {
                        for (int i = 0; i < str.Length; i++)
                        {
                            for (int j = 0; j < lbTechnology.Items.Count; j++)
                            {
                                if (lbTechnology.Items[j].Text.ToLower() == str[i].ToLower())
                                {
                                    lbTechnology.Items[j].Selected = true;
                                }
                            }
                        }
                    }

                }
                //ddlTechnology1.SelectedValue = lsitemList[0].TechnologyID.ToString();
                
                if (lsitemList[0].Active)
                    chkActive.Checked = true;
                else
                    chkActive.Checked = false;
                
                chkAllowRMA.Checked = lsitemList[0].AllowRMA;
                chkESN.Checked = lsitemList[0].AllowESN;
                chkSim.Checked = lsitemList[0].AllowSIM;
                chkShowunderCatalog.Checked = lsitemList[0].ShowunderCatalog;
                chkKitted.Checked = lsitemList[0].IsKitted;
                chkDisplayName.Checked = lsitemList[0].IsDisplayName;

                //ViewState["doc"] = lsitemList[0].ItemDocument;

            }
        }
        private void GetMappedSKUs(int itemGUID)
        {
            SV.Framework.Catalog.ProductController productController = SV.Framework.Catalog.ProductController.CreateInstance<SV.Framework.Catalog.ProductController>();

            //ProductController objProduct = new ProductController();
            List<MappedSKUModel> skuList = productController.GetMappedSKUs(itemGUID);
            ddlMappedSKU.DataSource = skuList;
            ddlMappedSKU.DataTextField = "SKU";
            ddlMappedSKU.DataValueField = "ItemCompanyGUID";
            ddlMappedSKU.DataBind();
            ListItem li = new ListItem("--Select SKU--", "0");
            ddlMappedSKU.Items.Insert(0, li);

        }
        protected void bindSpecification(int itemGUID)
        {
            SV.Framework.Catalog.ProductController productController = SV.Framework.Catalog.ProductController.CreateInstance<SV.Framework.Catalog.ProductController>();

            //ProductController objProduct = new ProductController();
            gvSpecification.DataSource = productController.getItemSpecifications(itemGUID);
            gvSpecification.DataBind();
        }
        public Hashtable GetEnumForBind(Type enumeration)
        {
            string[] names = Enum.GetNames(enumeration);
            Array values = Enum.GetValues(enumeration);
            Hashtable ht = new Hashtable();
            for (int i = 0; i < names.Length; i++)
            {
                ht.Add(Convert.ToInt32(values.GetValue(i)).ToString(), names[i]);
            }
            return ht;
        }
        protected void BindOperatingSystem()
        {
            //Hashtable ht = GetEnumForBind(typeof(avii.Classes.SKUOperations.OperationalSystem));
            //ddlOS.DataSource = ht;
            //ddlOS.DataTextField = "value";
            //ddlOS.DataValueField = "key";
            //ddlOS.DataBind();
        }
        protected void bindItmeType()
        { 
            //Hashtable ht = GetEnumForBind(typeof(InventoryItemType));
            //ddlitemType.DataSource = ht;
            //ddlitemType.DataTextField = "value";
            //ddlitemType.DataValueField = "key";
            //ddlitemType.DataBind();
        }

        protected void bindPricetype()
        {
            Hashtable ht = GetEnumForBind(typeof(CatalogEnums.ItemPricingType));
            ddlPricetype.DataSource = ht;
            ddlPricetype.DataTextField = "value";
            ddlPricetype.DataValueField = "key";
            ddlPricetype.DataBind();
        }
        protected void bindImageType(DropDownList ddlImageType)
        {
            Hashtable ht = GetEnumForBind(typeof(CatalogEnums.ImageType));
            ddlImageType.DataSource = ht;
            ddlImageType.DataValueField = "key";
            ddlImageType.DataTextField = "value";
            ddlImageType.DataBind();
        }

        protected void bindCategory(DropDownList ddlCategory)
        {
            SV.Framework.Catalog.ProductController productController = SV.Framework.Catalog.ProductController.CreateInstance<SV.Framework.Catalog.ProductController>();

            //ProductController objProductController = new ProductController();
            ddlCategory.DataSource = productController.GetItemCategoryTree(0, 0, 1, true, -1, -1, true, false, false, false);
            ddlCategory.DataTextField = "categoryname";
            ddlCategory.DataValueField = "CategoryWithProductAllowed";
            ddlCategory.DataBind();
            ListItem li = new ListItem("--Select Category--", "0");
            ddlCategory.Items.Insert(0, li);
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
        protected void bindMaker(DropDownList ddlMaker)
        {
            SV.Framework.Catalog.ProductController productController = SV.Framework.Catalog.ProductController.CreateInstance<SV.Framework.Catalog.ProductController>();

            ddlMaker.Items.Clear();

            List<Maker> lstItemCategoryList = productController.getMakerList(-1, "", -1, -1, -1, -1, -1, -1);
            ListItem li = new ListItem("--Select Maker--", "0");
            //ddlMaker.Items.Insert(0, li);
            int catid = -1; 
            for (int ictr = 0; ictr < lstItemCategoryList.Count; ictr++)
            {
                catid = lstItemCategoryList[ictr].MakerGUID;

                li = new ListItem(lstItemCategoryList[ictr].MakerName, catid.ToString());
                ddlMaker.Items.Add(li);
            }
        }
        protected void btnUpdateitemcompsku_Click(object sender, EventArgs e)
        {
            SV.Framework.Catalog.ProductController productController = SV.Framework.Catalog.ProductController.CreateInstance<SV.Framework.Catalog.ProductController>();

            if (hdnPO.Value == "1" && ddlCompany.SelectedIndex == 0)
            {

                lblMsg.Text = "Some of SKU(s) have fulfillment order so cannot update for all customer's";
            }
            else
            {
                int userID = 0;
               avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
                if (userInfo != null)
                {
                    userID = userInfo.UserGUID;
                    //ViewState["userid"] = userID;
                }
                int itemCount = 0;
                int returnValue = 0;
                string skmsg = ConfigurationManager.AppSettings["skumsg"].ToString();
                //ProductController objProduct = new ProductController();

                //ProductUtility objProducts = new ProductUtility();
                //List<ItemCompanyAssign> objItemCompanyAssignList1 = new List<ItemCompanyAssign>();

                //List<RMAUserCompany> lscompanylist = avii.Classes.RMAUtility.getRMAUserCompany(-1, "", 1, -1);
                if (hdnitemGUID.Value != "")
                {
                    //if (hdnItemcompskuGUID.Value == "")
                    //{
                    //    if (ddlCompany.SelectedIndex > 0)
                    //    {
                    //        objItemCompanyAssignList1 = objProducts.getItemCompanyAssignList(Convert.ToInt32(ddlCompany.SelectedValue.Trim()), "", Convert.ToInt32(hdnitemGUID.Value.Trim()));

                    //        itemCount = objItemCompanyAssignList1.Count;
                    //    }
                    //}

                    //if (itemCount > 0)
                    //{
                    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "temp", "<script language='javascript'>alert('" + skmsg + "')</script>", false);
                    //    lblMsg.Text = skmsg;
                    //}
                    //else
                    {
                        CompanySKUno itemSKU = new CompanySKUno();
                        int itemCompanyGUID = 0;
                        
                        int companyID = 0, maxStock = 0, minStock = 0, containerQty = 0, PalletQuantity = 0;
                        string sku = string.Empty, dpciNumber = string.Empty, SWVersion = string.Empty, HWVersion = string.Empty, BoxDesc = "";
                        sku = txtSKU.Text.Trim();
                        SWVersion = txtSWVersion.Text.Trim();
                        HWVersion = txtHWVersion.Text.Trim();
                        BoxDesc = txtBoxDesc.Text.Trim();
                        int.TryParse(txtMaxQty.Text.Trim(), out maxStock);
                        int.TryParse(txtMinQty.Text.Trim(), out minStock);
                        int.TryParse(txtContainerQty.Text.Trim(), out containerQty);
                        int.TryParse(txtPalletQty.Text.Trim(), out PalletQuantity);
                        dpciNumber = txtDPCINumber.Text.Trim();
                        //double skuPrice = 0;

                        //if (!string.IsNullOrEmpty(txtSKUPrice.Text.Trim()))
                        //{
                        //    skuPrice = Convert.ToDouble(txtSKUPrice.Text.Trim());
                        //}
                        if (!string.IsNullOrEmpty(sku))
                        {
                            if (hdnItemcompskuGUID.Value == "")
                                hdnItemcompskuGUID.Value = "0";

                            itemCompanyGUID = Convert.ToInt32(hdnItemcompskuGUID.Value);
                            itemSKU.ContainerQty = containerQty;
                            itemSKU.DPCI = dpciNumber;

                            itemSKU.IsDisable = chkEnable.Checked;
                            itemSKU.MaximumStockLevel = maxStock;
                            itemSKU.MinimumStockLevel = minStock;
                            itemSKU.ItemcompanyGUID = itemCompanyGUID;
                            itemSKU.ItemGUID = Convert.ToInt32(hdnitemGUID.Value.Trim());
                            itemSKU.SKU = sku;
                            itemSKU.SWVersion = SWVersion;
                            itemSKU.HWVersion = HWVersion;
                            itemSKU.BoxDesc = BoxDesc;
                          //  itemSKU.MASSKU = txtMasSKU.Text.Trim();
                            itemSKU.WarehouseCode = ddlWhCode.SelectedValue;
                            //itemSKU.Price = skuPrice;
                            if (ddlCompany.SelectedIndex > 0)
                                companyID = Convert.ToInt32(ddlCompany.SelectedValue);

                            itemSKU.CompanyID = companyID;
                            itemSKU.MappedItemCompanyGUID = Convert.ToInt32(ddlMappedSKU.SelectedValue); 
                            itemSKU.PalletQuantity = PalletQuantity; 

                            //itemSKU.IsFinishedSKU = chkSKU.Checked;
                            string isDuplicate = string.Empty;
                           // returnValue = ProductController.AssignItemCompanySKU(itemSKU, userID, chkSKU.Checked, out isDuplicate);
                            returnValue = productController.AssignItemCompanySKU(itemSKU, userID, out isDuplicate);
                            if (returnValue > 0)
                            {
                                lblMsg.Text = "Item already assigned to customer " + ddlCompany.SelectedItem.Text;
                                skmsg = "Item already assigned to customer " + ddlCompany.SelectedItem.Text;

                                ScriptManager.RegisterStartupScript(this, this.GetType(), "temp", "<script language='javascript'>alert('" + skmsg + "')</script>", false);
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(isDuplicate))
                                {
                                    skmsg = "SKU# (" + sku + ") already assigned to other product "; // +ddlCompany.SelectedItem.Text;
                                    //skmsg = "Item already assigned to customer " + ddlCompany.SelectedItem.Text;
                                    lblMsg.Text = skmsg;
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "temp", "<script language='javascript'>alert('" + skmsg + "')</script>", false);
                                }
                                else
                                {
                                    lblMsg.Text = "Assigned successfully";
                                    skmsg = "Assigned successfully";
                                    txtSKU.Enabled = true;

                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "temp", "<script language='javascript'>alert('" + skmsg + "')</script>", false);
                                }
                            }

                            //if (ddlCompany.SelectedIndex == 0)
                            //{
                            //    for (int i = 1; i < lscompanylist.Count; i++)
                            //    {
                            //        if (hdnItemcompskuGUID.Value == "0")
                            //        {
                            //            objItemCompanyAssignList1 = objProducts.getItemCompanyAssignList(lscompanylist[i].CompanyID, "", Convert.ToInt32(hdnitemGUID.Value.Trim()));
                            //            itemCount = objItemCompanyAssignList1.Count;
                            //        }
                            //        if (itemCount == 0)
                            //            objProduct.createItemcompanySKU(Convert.ToInt32(hdnItemcompskuGUID.Value.Trim()), Convert.ToInt32(hdnitemGUID.Value.Trim()), lscompanylist[i].CompanyID, txtSKU.Text.Trim(), 1);
                            //    }
                            //}
                            //else
                            //    objProduct.createItemcompanySKU(Convert.ToInt32(hdnItemcompskuGUID.Value.Trim()), Convert.ToInt32(hdnitemGUID.Value.Trim()), Convert.ToInt32(ddlCompany.SelectedValue.Trim()), txtSKU.Text.Trim(), 0);

                            hdnItemcompskuGUID.Value = string.Empty;
                            txtSKU.Text = string.Empty;
                            txtDPCINumber.Text = string.Empty;
                            txtContainerQty.Text = string.Empty;
                            txtPalletQty.Text = string.Empty;
                            txtMaxQty.Text = string.Empty;
                            txtHWVersion.Text = string.Empty;
                            txtBoxDesc.Text = string.Empty;
                            txtSWVersion.Text = string.Empty;
                            txtMinQty.Text = string.Empty;
                            chkEnable.Checked = false;
                            //txtMasSKU.Text = string.Empty;
                            ddlWhCode.SelectedIndex = 0;
                            ddlCompany.SelectedIndex = 0;
                            ddlMappedSKU.SelectedIndex = 0;
                            bindItmcompSKU(Convert.ToInt32(hdnitemGUID.Value.Trim()));
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "temp", "<script language='javascript'>alert('SKU required')</script>", false);

                            lblMsg.Text = "SKU required";
                        }
                    }

                }
            }
            
        }

        protected void btnUpdateCamera_Click(object sender, EventArgs e)
        {
            SV.Framework.Catalog.ProductController productController = SV.Framework.Catalog.ProductController.CreateInstance<SV.Framework.Catalog.ProductController>();

            if (ddlCameraConfig.SelectedIndex == 0 || ddlCameraType.SelectedIndex == 0)
            {
                if (ddlCameraConfig.SelectedIndex == 0)
                    lblMsg.Text = "Please select camera configuaration!";
                if (ddlCameraType.SelectedIndex == 0)
                    lblMsg.Text = "Please select camera type!";
            }
            else
            {

                if (hdnitemGUID.Value != "")
                {


                    ItemCameraInfo itemCameraInfo = new ItemCameraInfo();
                    int itemCameraID = 0;
                    int CameraID = Convert.ToInt32(ddlCameraConfig.SelectedValue);
                    string cameraType = ddlCameraType.SelectedValue;

                    if (!string.IsNullOrEmpty(cameraType))
                    {
                        if (hdItemCameraID.Value == "")
                            hdItemCameraID.Value = "0";

                        itemCameraID = Convert.ToInt32(hdItemCameraID.Value);


                        itemCameraInfo.ItemCameraID = itemCameraID;
                        itemCameraInfo.ItemGUID = Convert.ToInt32(hdnitemGUID.Value.Trim());
                        itemCameraInfo.CameraID = CameraID;
                        itemCameraInfo.CameraType = cameraType;
                        bool isDuplicate = false;
                        List<ItemCameraInfo> cameraList = Session["ItemCameraList"] as List<ItemCameraInfo>;
                        if (cameraList != null && cameraList.Count > 0)
                        {
                            ItemCameraInfo result = cameraList.Find(x => x.CameraID == CameraID);
                            if (result != null && result.CameraID > 0)
                            {
                                isDuplicate = true;
                            }
                        }

                        if (isDuplicate)
                        {
                            lblMsg.Text = "Camera configuaration already this product!";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "temp", "<script language='javascript'>alert('Camera configuaration already this product!')</script>", false);
                        }
                        else
                        {
                            productController.ItemCameraInsertUpdate(itemCameraInfo);
                            lblMsg.Text = "Assigned successfully";


                            ScriptManager.RegisterStartupScript(this, this.GetType(), "temp", "<script language='javascript'>alert('Assigned successfully')</script>", false);
                        }




                        hdItemCameraID.Value = string.Empty;
                      
                        ddlCameraType.SelectedIndex = 0;
                        BindItmCamera(Convert.ToInt32(hdnitemGUID.Value.Trim()));
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "temp", "<script language='javascript'>alert('CameraType required')</script>", false);

                        lblMsg.Text = "CameraType required";
                    }
                }
            }
        }
        private void ValidateForm()
        { }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            SV.Framework.Catalog.ProductController productController = SV.Framework.Catalog.ProductController.CreateInstance<SV.Framework.Catalog.ProductController>();

            //ProductController objProduct = new ProductController();
            int userID = 0;
           avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
            if (userInfo != null)
            {
                userID = userInfo.UserGUID;
                //ViewState["userid"] = userID;
            }
            //string screenSize = string.Empty;

           // int SimCardTypeID = 0, SpintorLockTypeID = 0, DisplayPriority = 5, operatingSystem=0;
            double Weight = 0;
            int itemGUID = 0, esnLength = 0, meidLength = 0, productTypeID = 0;
            int returnValue = 0;
            string fileName = string.Empty;
            string filePath = string.Empty;
            bool IsKitted = chkKitted.Checked;
            int storageQty = 0;
            string storage = string.Empty;
            storage = ddlStorage.SelectedValue;
            int conditionID = Convert.ToInt32(ddlCondition.SelectedValue);
            bool restock = chkStock.Checked;

            storage = ddlStorage.SelectedValue;
            int.TryParse(txtStorageQty.Text.Trim(), out storageQty);
            int.TryParse(txtMeidLength.Text.Trim(), out meidLength);
            int.TryParse(txtEsnLength.Text.Trim(), out esnLength);

            if (ddlProductType.SelectedIndex > 0)
                productTypeID = Convert.ToInt32(ddlProductType.SelectedValue);

            //string itemCode = txtProductCode.Text.Trim();
            // double defaultPrice = 0;

                //operatingSystem = Convert.ToInt32(ddlOS.SelectedValue);
                //screenSize = ddlScreenSize.SelectedValue;
                //if (!string.IsNullOrEmpty(txtPrice.Text.Trim()))
                //{
                //    defaultPrice = Convert.ToDouble(txtPrice.Text.Trim());
                //}

            List<Carriers> carrierList = new List<Carriers>();
            if (fuItemDoc.HasFile)
            {
                fileName = Path.GetFileName(fuItemDoc.PostedFile.FileName);
                //string extension = Path.GetExtension(fupItemImage1.PostedFile.FileName);
                string ext = Path.GetExtension(fuItemDoc.PostedFile.FileName);
                fileName = txtModelNumber.Text.Trim() + ext;
                
                string targetFolder = Server.MapPath("~");

                targetFolder = targetFolder + "\\Documents\\Products\\";

                if (!Directory.Exists(Path.GetFullPath(targetFolder)))
                {
                    Directory.CreateDirectory(Path.GetFullPath(targetFolder));
                }
                filePath = targetFolder + fileName;
                
                if (ext == ".pdf" || ext == ".doc" || ext == ".docx" || ext == ".txt")
                    fuItemDoc.PostedFile.SaveAs(filePath);
                else
                {
                    lblMsg.Text = "Not a valid file";
                    return;
                }

            }
            else
                if (ViewState["doc"] != null)
                {
                    fileName = ViewState["doc"].ToString();
                }
            if (hdnitemGUID.Value != "" && hdnitemGUID.Value != null)
            {
                itemGUID = Convert.ToInt32(hdnitemGUID.Value);
            }

            for (int i = 0; i < lbTechnology.Items.Count; i++)
            {
                if (lbTechnology.Items[i].Selected)
                {
                    Carriers carrierObj = new Carriers();
                    carrierObj.CarrierGUID = Convert.ToInt32(lbTechnology.Items[i].Value);
                    carrierList.Add(carrierObj);
                }
            }
            if (txtProductName.Text != "")
            {
                string CategoryWithProductAllowed = ddlCategoryType.SelectedValue;
                string[] array = CategoryWithProductAllowed.Split('|');

                int categoryID = Convert.ToInt32(array[0]);

                int makerGUID = Convert.ToInt32(ddlMaker.SelectedValue);
                string ostype = ddlOSType.SelectedValue;
                //SimCardTypeID = Convert.ToInt32(ddlSimCardType.SelectedValue);
                //SpintorLockTypeID = Convert.ToInt32(ddlSpintorLockType.SelectedValue);
                //DisplayPriority = Convert.ToInt32(ddlDisplayPriority.SelectedValue);
                if(!string.IsNullOrEmpty(txtWeight.Text.Trim()))
                {
                    Weight = Convert.ToDouble(txtWeight.Text);
                }

                returnValue =    productController.createInventoryItem(itemGUID, categoryID, txtModelNumber.Text.Trim(), txtProductName.Text, 
                        txtDescription.Text, txtFullDesc.Text, txtUPC.Text, Convert.ToInt32(chkActive.Checked),  
                        makerGUID, 0, "", chkAllowRMA.Checked, chkShowunderCatalog.Checked, fileName, carrierList,
                        false, chkSim.Checked,userID, Weight, IsKitted, chkESN.Checked, storage, storageQty, chkDisplayName.Checked, 
                        esnLength, meidLength, productTypeID, conditionID, restock, ostype);

                if (returnValue == 0)
                    Response.Redirect("manage-product.aspx");
                else
                    if (returnValue == 1)
                        lblMsg.Text = "ModelNumber already exist";
                    else
                        if (returnValue == 2)
                            lblMsg.Text = "UPC already exist";
                        else
                            if (returnValue == 3)
                                lblMsg.Text = "ModelNumber and UPC already exist";
                    
            }
        }

        protected void btnpriceUpdate_Click(object sender, EventArgs e)
        {
            SV.Framework.Catalog.ProductController productController = SV.Framework.Catalog.ProductController.CreateInstance<SV.Framework.Catalog.ProductController>();

            hdnTabindex.Value = "1";
            if (hdnpricingGUID.Value != "" && hdnpricingGUID.Value != null)
                productController.createItemPricing(Convert.ToInt32(hdnitemGUID.Value), Convert.ToInt32(hdnpricingGUID.Value), txtWebPrice.Text, txtRetailPrice.Text, txtWholesalePrice.Text, Convert.ToInt16(ddlPricetype.SelectedValue));
            else
                productController.createItemPricing(Convert.ToInt32(hdnitemGUID.Value), 0, txtWebPrice.Text, txtRetailPrice.Text, txtWholesalePrice.Text, Convert.ToInt16(ddlPricetype.SelectedValue));
        }

        protected void bindItmImages(int itemGUID)
        {
            SV.Framework.Catalog.ProductController productController = SV.Framework.Catalog.ProductController.CreateInstance<SV.Framework.Catalog.ProductController>();

            gvItemImages.DataSource = productController.getItemImageList(itemGUID, -1, 1, 0);
            gvItemImages.DataBind();
        }
       
        protected void bindItmPricing(int itemGUID)
        {
            ProductController objProduct = new ProductController();
            gvItemPricing.DataSource = objProduct.getItemPriceList(itemGUID, -1);
            gvItemPricing.DataBind();

        }
        protected void bindItmcompSKU(int itemGUID)
        {
            SV.Framework.Catalog.ProductController productController = SV.Framework.Catalog.ProductController.CreateInstance<SV.Framework.Catalog.ProductController>();

            List<CompanySKUno> lstCompanySKUno = productController.getCompanySKUnoList(itemGUID, -1, "");
            gvcompanynsku.DataSource = lstCompanySKUno;
            gvcompanynsku.DataBind();
            if (lstCompanySKUno != null && lstCompanySKUno.Count > 0)
            {
                var poInfoList = (from item in lstCompanySKUno where item.PoExists.Equals(1) select item).ToList();
                if (poInfoList != null && poInfoList.Count > 0)
                {
                    //ViewState["po"] = 1;
                    hdnPO.Value = "1";

                }
            }



        }

        protected void BindItmCamera(int itemGUID)
        {
            SV.Framework.Catalog.ProductController productController = SV.Framework.Catalog.ProductController.CreateInstance<SV.Framework.Catalog.ProductController>();

            List<ItemCameraInfo> lstCameraList = productController.GetItemCameraList(itemGUID);
            gvCamera.DataSource = lstCameraList;
            gvCamera.DataBind();

            Session["ItemCameraList"] = lstCameraList;

        }
        protected void btnBackToSearch_Click(object sender, EventArgs e)
        {
            Response.Redirect("manage-product.aspx?search=1");

        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            if (hdnitemGUID.Value != string.Empty)
                Response.Redirect("detail-product.aspx?itemGUID="+ hdnitemGUID.Value);
            else
                Resetvalues();
        }

        protected void Resetvalues()
        {
            hdnitemGUID.Value = string.Empty;
            hdnImageGUID1.Value = string.Empty;


         //   txtColor.Text = string.Empty;
            txtSKU.Text = string.Empty;
            txtDescription.Text = string.Empty;
            txtFullDesc.Text = string.Empty;
            txtImageDesc1.Text = string.Empty;

            txtModelNumber.Text = string.Empty;
            txtProductCode.Text = string.Empty;
            txtProductName.Text = string.Empty;
            txtUPC.Text = string.Empty;
            
            chkActive.Checked = false;
            ddlCategoryType.SelectedIndex = 0;
            
            ddlMaker.SelectedIndex = 0;
           // ddlitemType.SelectedIndex = 0;
            lbTechnology.SelectedIndex = 0;

        }

        //Function to get random number
        private static readonly Random getrandom = new Random();
        private static readonly object syncLock = new object();
        private static int GetRandomNumber(int min, int max)
        {
            lock (syncLock)
            { // synchronize
                return getrandom.Next(min, max);
            }
        }

        protected void btnUpdateImage_Click(object sender, EventArgs e)
        {
            SV.Framework.Catalog.ProductController productController = SV.Framework.Catalog.ProductController.CreateInstance<SV.Framework.Catalog.ProductController>();

            // DBConnect objDB = new DBConnect();
            //ProductController objProduct = new ProductController();
            hdnTabindex.Value = "0";
            string filename = "";
            string filename4 = "";
            string filename2 = "";
            string filename3 = "";
            string imagePath = "";
            string imagePath2 = "";
            string imagePath3 = "";

            string imagePathT = "";
            string imagePathT2 = "";
            string imagePathT3 = "";


            //string filename = "";
            //string filename2 = "";
            //string filename3 = "";
            //string imagePath = "";
            //string imagePath2 = "";
            //string imagePath3 = "";

            

            Random rnd = new Random();
            int randomNo = GetRandomNumber(1, 9999);
            string modelNumber = txtModelNumber.Text.Trim();
            string[] filename1 = new string[2];

            if (fupItemImage1.HasFile && fupItemImg2.HasFile)
            {
                //filename = System.IO.Path.GetFileName(uploadImage.PostedFile.FileName);
                    
                filename = System.IO.Path.GetFileName(fupItemImage1.PostedFile.FileName);
                string extension = Path.GetExtension(fupItemImage1.PostedFile.FileName);
                
                //filename1 = filename.Split('.');
                if (hdnitemGUID.Value != null && hdnitemGUID.Value != "")
                {
                    filename = modelNumber + "_"+ randomNo + extension;
                        
                    
                    //filename2 = hdnitemGUID.Value + "_serviceTime" + ddlImageType1.SelectedItem.Text + "1_Medium." + filename1[1].ToString();
                    //filename3 = hdnitemGUID.Value + "_serviceTime" + ddlImageType1.SelectedItem.Text + "1_Small." + filename1[1].ToString();
                }
                else
                {
                   // string sql = "select top 1  itemguid + 1 as itemguid from av_item order by itemguid desc";
                   // DataSet ds = objDB.GetDataSet(sql);

                    filename = modelNumber + "_" + randomNo + extension;
                    
                    //filename2 = ds.Tables[0].Rows[0]["itemguid"].ToString() + "_serviceTime" + ddlImageType1.SelectedItem.Text + "1_Medium." + filename1[1].ToString();
                    //filename3 = ds.Tables[0].Rows[0]["itemguid"].ToString() + "_serviceTime" + ddlImageType1.SelectedItem.Text + "1_Small." + filename1[1].ToString();
                }

                string targetFolder = Server.MapPath("~");

                targetFolder = targetFolder + "\\images\\products\\";

                if (!Directory.Exists(Path.GetFullPath(targetFolder)))
                {
                    Directory.CreateDirectory(Path.GetFullPath(targetFolder));
                }


                filename2 = "L_" + filename;

                imagePath2 = targetFolder + filename2;

                fupItemImage1.PostedFile.SaveAs(imagePath2);


                filename3 = "S_" + filename;

                imagePath3 = targetFolder + filename3;

                fupItemImg2.PostedFile.SaveAs(imagePath3);
                

                //imagePath = targetFolder + filename;
                //imagePath2 = targetFolder + filename2;
                //imagePath3 = targetFolder + filename3;

                //Determine image format
                //HttpPostedFile myFile = fupItemImage1.PostedFile;
                //ResizeFromStream(imagePath, myFile.InputStream, 237, 345);
                //ResizeFromStream(imagePath2, myFile.InputStream, 131, 174);
                //ResizeFromStream(imagePath3, myFile.InputStream, 80, 114);

            }
            

            DataTable dtimage = new DataTable();
            ImageType image_type = (ImageType)Enum.Parse(typeof(ImageType), ddlImageType1.SelectedValue);
           
            if (hdnImageGUID.Value != "" && hdnImageGUID.Value != null)
                dtimage = productController.createItemImages(Convert.ToInt32(hdnitemGUID.Value), Convert.ToInt32(hdnImageGUID.Value), filename, Convert.ToInt32(ddlImageType1.SelectedValue), txtImageDesc1.Text);
            else
                dtimage = productController.createItemImages(Convert.ToInt32(hdnitemGUID.Value), 0, filename, Convert.ToInt32(ddlImageType1.SelectedValue), txtImageDesc1.Text);
            if (dtimage.Rows.Count > 0)
            {
                hdnImage_Guid.Value = dtimage.Rows[0]["itemimageGUID"].ToString();
            }
            bindItmImages(Convert.ToInt32(hdnitemGUID.Value));
            hdnImageGUID.Value = "";
            txtImageDesc1.Text = "";
        }

        protected void ResizeFromStream(string ImageSavePath, Stream Buffer, int intNewWidth, int intNewHeight)
        {
            try
            {
                System.Drawing.Image imgInput = System.Drawing.Image.FromStream(Buffer);

                //Determine image format
                ImageFormat fmtImageFormat = imgInput.RawFormat;


                //create new bitmap
                Bitmap bmpResized = new Bitmap(imgInput, intNewWidth, intNewHeight);

                //save bitmap to disk
                bmpResized.Save(ImageSavePath, fmtImageFormat);

                //release used resources
                imgInput.Dispose();
                bmpResized.Dispose();
                lblMsg.Text = string.Empty;
            }
            catch (Exception ex)
            {
                lblMsg.Text = "Browsed file must be an image!";
            }
           
        }
        protected void btnUpdatePricing_Click(object sender, EventArgs e)
        {
            SV.Framework.Catalog.ProductController productController = SV.Framework.Catalog.ProductController.CreateInstance<SV.Framework.Catalog.ProductController>();

            hdnTabindex.Value = "1";
            if (hdnpricingGUID.Value != "" && hdnpricingGUID.Value != null)
                productController.createItemPricing(Convert.ToInt32(hdnitemGUID.Value), Convert.ToInt32(hdnpricingGUID.Value), txtWebPrice.Text, txtRetailPrice.Text, txtWholesalePrice.Text, Convert.ToInt32(ddlPricetype.SelectedValue));
            else
                productController.createItemPricing(Convert.ToInt32(hdnitemGUID.Value), 0, txtWebPrice.Text, txtRetailPrice.Text, txtWholesalePrice.Text, Convert.ToInt32(ddlPricetype.SelectedValue));

            bindItmPricing(Convert.ToInt32(hdnitemGUID.Value));
            hdnpricingGUID.Value = "";
            txtRetailPrice.Text = "";
            txtWebPrice.Text = "";
            txtWholesalePrice.Text = "";
        }
        protected void btnUpdateSpecification_Click(object sender, EventArgs e)
        {
            SV.Framework.Catalog.ProductController productController = SV.Framework.Catalog.ProductController.CreateInstance<SV.Framework.Catalog.ProductController>();

            hdnTabindex.Value = "3";
            if (hdnSpecificationGUID.Value != "" && hdnSpecificationGUID.Value != null)
                productController.createItemSpecifications(Convert.ToInt32(hdnitemGUID.Value), Convert.ToInt32(hdnSpecificationGUID.Value), txtSpecification.Text);
            else
                productController.createItemSpecifications(Convert.ToInt32(hdnitemGUID.Value), 0, txtSpecification.Text);

            bindSpecification(Convert.ToInt32(hdnitemGUID.Value));
            hdnSpecificationGUID.Value = "";
            txtSpecification.Text = "";

        }

        protected void ImageDelete_click(object sender, CommandEventArgs e)
        {
            int itemGUID=0;
            if(hdnitemGUID.Value!="")
                itemGUID = Convert.ToInt32(hdnitemGUID.Value.Trim());
            SV.Framework.Catalog.ProductController productController = SV.Framework.Catalog.ProductController.CreateInstance<SV.Framework.Catalog.ProductController>();

            hdnTabindex.Value = "0";
            int imageGUID = Convert.ToInt32(e.CommandArgument);
            string sql = "delete from av_itemimages where imageGUID=" + imageGUID + "";
            productController.deleteItem(sql);

            bindItmImages(itemGUID);
        }
        protected void priceDelete_click(object sender, CommandEventArgs e)
        {
            int itemGUID = 0;
            if (hdnitemGUID.Value != "")
                itemGUID = Convert.ToInt32(hdnitemGUID.Value.Trim());
            SV.Framework.Catalog.ProductController productController = SV.Framework.Catalog.ProductController.CreateInstance<SV.Framework.Catalog.ProductController>();

            hdnTabindex.Value = "2";
            int priceGUID = Convert.ToInt32(e.CommandArgument);
            string sql = "delete from av_itempricing where pricingGUID=" + priceGUID + "";
            productController.deleteItem(sql);
            bindItmPricing(itemGUID);
        }
        protected void specificationDelete_click(object sender, CommandEventArgs e)
        {
            SV.Framework.Catalog.ProductController productController = SV.Framework.Catalog.ProductController.CreateInstance<SV.Framework.Catalog.ProductController>();

            int itemGUID = 0;
            if (hdnitemGUID.Value != "")
                itemGUID = Convert.ToInt32(hdnitemGUID.Value.Trim());
           // ProductController objProduct = new ProductController();
            hdnTabindex.Value = "3";
            int specificationGUID = Convert.ToInt32(e.CommandArgument);
            string sql = "delete from av_ItemSpecification where SpecificationGUID=" + specificationGUID + "";
            productController.deleteItem(sql);
            bindSpecification(itemGUID);
        }

        protected void btnSubmitval_Click(object sender, EventArgs e)
        {
            SV.Framework.Catalog.AttributeValueUtility attributeValueUtility = SV.Framework.Catalog.AttributeValueUtility.CreateInstance<SV.Framework.Catalog.AttributeValueUtility>();

            foreach (DataListItem item in DlAttributeValue.Items)
            {
                Label lblAttribute = (Label)item.FindControl("lblAttribute");
                TextBox txtAttributeValue = (TextBox)item.FindControl("txtAttributeValue");
                HiddenField hdnAttributeValueGuid = (HiddenField)item.FindControl("hdnAttributeValueGuid");
                HiddenField hdnAttributeGuid = (HiddenField)item.FindControl("hdnAttributeGuid");
                
                if (txtAttributeValue.Text != string.Empty)
                {
                    attributeValueUtility.createAttributesValues(Convert.ToInt32(hdnAttributeValueGuid.Value.Trim()), Convert.ToInt32(hdnAttributeGuid.Value.Trim()), Convert.ToInt32(hdnitemGUID.Value.Trim()), txtAttributeValue.Text);
                    
                }
            }
            bindAttributeValue(Convert.ToInt32(hdnitemGUID.Value.Trim()));
            
        }

        protected void btnCancelVal_Click(object sender, EventArgs e)
        {
            foreach (DataListItem item in DlAttributeValue.Items)
            {
                TextBox txtAttributeValue = (TextBox)item.FindControl("txtAttributeValue");
                txtAttributeValue.Text = string.Empty;
            }
        }
        protected void deleteattribute_click(object sender, CommandEventArgs e)
        {
            SV.Framework.Catalog.AttributeValueUtility attributeValueUtility = SV.Framework.Catalog.AttributeValueUtility.CreateInstance<SV.Framework.Catalog.AttributeValueUtility>();

            int attributecalueGUID = Convert.ToInt32(e.CommandArgument);
            attributeValueUtility.Deleteattributevalue(attributecalueGUID);
            bindAttributeValue(Convert.ToInt32(hdnitemGUID.Value.Trim()));
        }

        protected void CameraEdit_click(object sender, CommandEventArgs e)
        {
            SV.Framework.Catalog.ProductController productController = SV.Framework.Catalog.ProductController.CreateInstance<SV.Framework.Catalog.ProductController>();

            int itemCameraID = Convert.ToInt32(e.CommandArgument);
            ItemCameraInfo itemCameraInfo = productController.GetItemCameraInfo(itemCameraID);
            ddlCameraConfig.SelectedValue = itemCameraInfo.CameraID.ToString();
            ddlCameraType.SelectedValue = itemCameraInfo.CameraType;
            hdItemCameraID.Value = itemCameraID.ToString();

            //txtMasSKU.Text = itemCameraInfo.MASSKU;
            ////if (companySKUInfo.Price > 0)
            ////    txtSKUPrice.Text = companySKUInfo.Price.ToString();
            ////if (ddlCompany.SelectedIndex > 0)
            //ddlCompany.SelectedValue = itemCameraInfo.CompanyID.ToString();
            
            




        }
        protected void ItemCompanySKUEdit_click(object sender, CommandEventArgs e)
        {
            SV.Framework.Catalog.ProductController productController = SV.Framework.Catalog.ProductController.CreateInstance<SV.Framework.Catalog.ProductController>();

            int itemcompanyskuGUID = Convert.ToInt32(e.CommandArgument);
            bool IsPoExists = Convert.ToBoolean(e.CommandName);
            txtSKU.Enabled = true;
            CompanySKUno companySKUInfo = productController.GetCompanyItemSKUInfo(itemcompanyskuGUID);
            txtSKU.Text = companySKUInfo.SKU;
            if (IsPoExists)
                txtSKU.Enabled = false;

            txtMinQty.Text = companySKUInfo.MinimumStockLevel == 0 ? "" : companySKUInfo.MinimumStockLevel.ToString();
            txtMaxQty.Text = companySKUInfo.MaximumStockLevel == 0 ? "" : companySKUInfo.MaximumStockLevel.ToString();
            chkEnable.Checked = companySKUInfo.IsDisable;
            txtContainerQty.Text = companySKUInfo.ContainerQty == 0 ? "" : companySKUInfo.ContainerQty.ToString();
            txtPalletQty.Text = companySKUInfo.PalletQuantity == 0 ? "" : companySKUInfo.PalletQuantity.ToString();
            txtDPCINumber.Text = companySKUInfo.DPCI;
            txtSWVersion.Text = companySKUInfo.SWVersion;
            txtHWVersion.Text = companySKUInfo.HWVersion;
            txtBoxDesc.Text = companySKUInfo.BoxDesc;

            //  txtMasSKU.Text = companySKUInfo.MASSKU;
            // chkSKU.Checked = companySKUInfo.IsFinishedSKU;
            //if (companySKUInfo.Price > 0)
            //    txtSKUPrice.Text = companySKUInfo.Price.ToString();
            //if (ddlCompany.SelectedIndex > 0)

            ddlCompany.SelectedValue = companySKUInfo.CompanyID.ToString();
            ddlMappedSKU.SelectedValue = companySKUInfo.MappedItemCompanyGUID.ToString();
            hdnItemcompskuGUID.Value = itemcompanyskuGUID.ToString();
            BindWarehouseCode(companySKUInfo.CompanyID);
            if (!string.IsNullOrEmpty(companySKUInfo.WarehouseCode))
                ddlWhCode.SelectedValue = companySKUInfo.WarehouseCode;




        }
        protected void ItemcompanyskuDelete_click(object sender, CommandEventArgs e)
        {
            int ItemcompanyskuGUID = Convert.ToInt32(e.CommandArgument);
            SV.Framework.Catalog.ProductController productController = SV.Framework.Catalog.ProductController.CreateInstance<SV.Framework.Catalog.ProductController>();

            int userID = Convert.ToInt32(Session["UserID"]);
            productController.DeleteitemCompanysku(ItemcompanyskuGUID, userID);
            bindItmcompSKU(Convert.ToInt32(hdnitemGUID.Value.Trim()));
        }
        protected void grvItem_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvcompanynsku.EditIndex = e.NewEditIndex;
        }
        protected void grvItem_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvcompanynsku.PageIndex = e.NewPageIndex;
            bindItmcompSKU(Convert.ToInt32(hdnitemGUID.Value.Trim()));
        }
        protected void gvItemImages_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.DataItem == null)
            {
                return;
            }
            string[] pagename = Request.Url.Segments;
            List<avii.Classes.AccessControlMapping> Accesscontrollist = new List<avii.Classes.AccessControlMapping>();
            avii.Classes.AccessControlMappingUtility objAccesscontrol = new avii.Classes.AccessControlMappingUtility();
            ImageButton edit = (ImageButton)e.Row.FindControl("lnkImageUpdate");
            ImageButton delete = (ImageButton)e.Row.FindControl("lnkImageDelete");
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
                    for (int i = 0; i < Accesscontrollist.Count; i++)
                    {
                        string ct = Accesscontrollist[i].Control.ToString();

                        Control linkcontrol = (Control)e.Row.FindControl(ct);
                        if (Accesscontrollist[0].Mode == true)
                            if (ct == "lnkImageUpdate")
                                edit.Visible = true;
                        if (ct == "lnkImageDelete")
                            delete.Visible = true;
                    }
                }
            }

        }
        protected void gvItemPricing_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.DataItem == null)
            {
                return;
            }
            string[] pagename = Request.Url.Segments;
            List<avii.Classes.AccessControlMapping> Accesscontrollist = new List<avii.Classes.AccessControlMapping>();
            avii.Classes.AccessControlMappingUtility objAccesscontrol = new avii.Classes.AccessControlMappingUtility();
            ImageButton edit = (ImageButton)e.Row.FindControl("lnkPricingUpdate");
            ImageButton delete = (ImageButton)e.Row.FindControl("lnkPriceDelete");
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
                    for (int i = 0; i < Accesscontrollist.Count; i++)
                    {
                        string ct = Accesscontrollist[i].Control.ToString();
                        Control linkcontrol = (Control)e.Row.FindControl(ct);
                        if (Accesscontrollist[0].Mode == true)
                            if (ct == "lnkPricingUpdate")
                                edit.Visible = true;
                        if (ct == "lnkPriceDelete")
                            delete.Visible = true;
                    }
                }
            }

        }
        protected void gvSpecification_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.DataItem == null)
            {
                return;
            }
            string[] pagename = Request.Url.Segments;
            List<avii.Classes.AccessControlMapping> Accesscontrollist = new List<avii.Classes.AccessControlMapping>();
            avii.Classes.AccessControlMappingUtility objAccesscontrol = new avii.Classes.AccessControlMappingUtility();
            ImageButton edit = (ImageButton)e.Row.FindControl("lnkSpecificationUpdate");
            ImageButton delete = (ImageButton)e.Row.FindControl("lnkSpecificationDelete");
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
                    for (int i = 0; i < Accesscontrollist.Count; i++)
                    {
                        string ct = Accesscontrollist[i].Control.ToString();
                        Control linkcontrol = (Control)e.Row.FindControl(ct);
                        if (Accesscontrollist[i].Mode == true)
                            if (ct == "lnkSpecificationUpdate")
                                edit.Visible = true;
                        if (ct == "lnkSpecificationDelete")
                            delete.Visible = true;
                    }
                }
            }

        }
        protected void gvcompanynsku_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.DataItem == null)
            {
                return;
            }
            string[] pagename = Request.Url.Segments;
            List<avii.Classes.AccessControlMapping> Accesscontrollist = new List<avii.Classes.AccessControlMapping>();
            avii.Classes.AccessControlMappingUtility objAccesscontrol = new avii.Classes.AccessControlMappingUtility();
            ImageButton edit = (ImageButton)e.Row.FindControl("lnkItemcompskuUpdate");
            ImageButton delete = (ImageButton)e.Row.FindControl("lnkItemcompskuDelete");
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
                    for (int i = 0; i < Accesscontrollist.Count; i++)
                    {
                        string ct = Accesscontrollist[i].Control.ToString();
                        Control linkcontrol = (Control)e.Row.FindControl(ct);
                        if (Accesscontrollist[i].Mode == true)
                            if (ct == "lnkItemcompskuUpdate")
                                edit.Visible = true;
                        if (ct == "lnkItemcompskuDelete")
                            delete.Visible = true;
                    }
                }
            }

        }
        protected void DlAttributeValue_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                string[] pagename = Request.Url.Segments;
                List<avii.Classes.AccessControlMapping> Accesscontrollist = new List<avii.Classes.AccessControlMapping>();
                avii.Classes.AccessControlMappingUtility objAccesscontrol = new avii.Classes.AccessControlMappingUtility();

                LinkButton delete = (LinkButton)e.Item.FindControl("lnkDelete");

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
                        for (int i = 0; i < Accesscontrollist.Count; i++)
                        {
                            string ct = Accesscontrollist[i].Control.ToString();
                            Control linkcontrol = (Control)e.Item.FindControl(ct);
                            if (Accesscontrollist[i].Mode == true)

                                if (ct == "lnkDelete")
                                    delete.Visible = true;
                        }
                    }
                }

            }
        }

        protected void ItemCameraDelete_click(object sender, CommandEventArgs e)
        {
            int ItemCameraID = Convert.ToInt32(e.CommandArgument);
            SV.Framework.Catalog.ProductController productController = SV.Framework.Catalog.ProductController.CreateInstance<SV.Framework.Catalog.ProductController>();


            productController.ItemCameraDelete(ItemCameraID);
            BindItmCamera(Convert.ToInt32(hdnitemGUID.Value.Trim()));
        }
    }
}
