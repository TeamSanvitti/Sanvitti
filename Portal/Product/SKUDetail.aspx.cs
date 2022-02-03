using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using avii.Classes;

namespace avii.Product
{
    public partial class SKUDetail : System.Web.UI.Page
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
            int itemGUID = 0;
            int imageGUID = 0;

            if (Session["itemGUID"] != null)
            {
                //hdnTabindex.Value = "0";
                itemGUID = Convert.ToInt32(Session["itemGUID"]);
                hdnitemGUID.Value = Session["itemGUID"].ToString();

            }
            //else
            //    btnUpdate.Text = "Submit Item";


            if (Request["imageGUID"] != "" && Request["imageGUID"] != null)
            {
                imageGUID = Convert.ToInt32(Request["imageGUID"]);
            }

            if (!IsPostBack)
            {

                bindCategory(ddlCategoryType);
                bindMaker(ddlMaker);
                BindCarriers();
                // bindItmeType();
               // bindCompany();
                //BindSimCartSpintorLockTypes();
                //BindOperatingSystem();


                if (itemGUID > 0)
                {
                  //  BindCameraConfig();
                   // bindPricetype();
                   // bindImageType(ddlImageType1);
                    getItemDetails(itemGUID);
                   // bindItmPricing(itemGUID);
                   // bindSpecification(itemGUID);
                  //  bindItmImages(itemGUID);
                   // bindAttributeValue(itemGUID);
                    //bindItmcompSKU(itemGUID);
                    //BindWarehouseCode(0);
                    //BindItmCamera(itemGUID);
                }
                else
                {
                    chkActive.Checked = true;
                    chkAllowRMA.Checked = true;
                    //chkAllowLTE.Checked = true;
                }
            }

        }
       
       
        protected void BindSimCartSpintorLockTypes()
        {
            List<SimCardType> SimCardTypeList = new List<SimCardType>();
            List<SpintorLockType> SpintorLockTypeList = new List<SpintorLockType>();
            SimCardSpintorLockOperations.GetSimSpintorLockTypesList(out SimCardTypeList, out SpintorLockTypeList);
            if (SimCardTypeList != null && SimCardTypeList.Count > 0)
            {
                //ddlSimCardType.DataSource = SimCardTypeList;
                //ddlSimCardType.DataTextField = "SimCardTypeText";
                //ddlSimCardType.DataValueField = "SimCardTypeID";
                //ddlSimCardType.DataBind();

                ListItem item = new ListItem("--Select SimCardType--", "0");
                //ddlSimCardType.Items.Insert(0, item);
            }
            else
            {
                ListItem item = new ListItem("--Select SimCardType--", "0");
                // ddlSimCardType.Items.Insert(0, item);
            }
            if (SpintorLockTypeList != null && SpintorLockTypeList.Count > 0)
            {
                // ddlSpintorLockType.DataSource = SpintorLockTypeList;
                //  ddlSpintorLockType.DataTextField = "SpintorLockTypeText";
                //  ddlSpintorLockType.DataValueField = "SpintorLockTypeID";
                // ddlSpintorLockType.DataBind();

                ListItem item = new ListItem("--Select SpintorLockType--", "0");
                // ddlSpintorLockType.Items.Insert(0, item);
            }
            else
            {

                ListItem item = new ListItem("--Select SpintorLockType--", "0");
                //  ddlSpintorLockType.Items.Insert(0, item);
            }

        }
        protected void BindCarriers()
        {
            //avii.Classes.ProductUtility obj = new avii.Classes.ProductUtility();


            //ddlTechnology1.DataSource = CarriersOperation.GetCarriersList(-1,-1);
            //ddlTechnology1.DataTextField = "CarrierName";
            //ddlTechnology1.DataValueField = "CarrierGUID";
            //ddlTechnology1.DataBind();

            //ListItem item = new ListItem("--Select Carriers--", "0");
            //ddlTechnology1.Items.Insert(0, item);

            lbTechnology.DataSource = CarriersOperation.GetCarriersList(-1, -1);
            lbTechnology.DataTextField = "CarrierName";
            lbTechnology.DataValueField = "CarrierGUID";
            lbTechnology.DataBind();

            ListItem item = new ListItem("--Select Carriers--", "0");
            lbTechnology.Items.Insert(0, item);

        }
        protected void getItemDetails(int itemID)
        {
            ProductController objProduct = new ProductController();
            hdnitemGUID.Value = itemID.ToString();

            List<InventoryItems> lsitemList = objProduct.getItemList(-1, itemID, -1, -1, -1, "", -1, "", "", "", -1, "", "", -1, 0, 0, false);

            if (lsitemList.Count > 0)
            {
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

                ddlCategoryType.SelectedValue = lsitemList[0].ItemCategoryGUID.ToString();
                ddlMaker.SelectedValue = lsitemList[0].ItemMakerGUID.ToString();

               // txtSKU.Text = lsitemList[0].SKU;

              //  ddlCompany.SelectedValue = lsitemList[0].CompanyGUID.ToString();
                //   ddlitemType.SelectedValue = lsitemList[0].Item_Type.ToString();

                //NEW MVNO changes
                if (lsitemList[0].Weight > 0)
                    txtWeight.Text = lsitemList[0].Weight.ToString();
                // ddlDisplayPriority.SelectedValue = lsitemList[0].DisplayPriority.ToString();
                // ddlSimCardType.SelectedValue = lsitemList[0].SimCardTypeID.ToString();
                // ddlSpintorLockType.SelectedValue = lsitemList[0].SpintorLockTypeID.ToString();



                if (!string.IsNullOrEmpty(lsitemList[0].ItemTechnology))
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

                //ViewState["doc"] = lsitemList[0].ItemDocument;

            }
        }
        

        protected void bindCategory(DropDownList ddlCategory)
        {
            ProductController objProductController = new ProductController();
            ddlCategory.DataSource = objProductController.getItemCategoryTree(0, 0, 1, true, -1, -1, true, false, false, false);
            ddlCategory.DataTextField = "categoryname";
            ddlCategory.DataValueField = "categoryGUID";
            ddlCategory.DataBind();
            ListItem li = new ListItem("--Select Category--", "0");
            ddlCategory.Items.Insert(0, li);
        }

        protected void bindMaker(DropDownList ddlMaker)
        {
            ProductController objProductController = new ProductController();

            ddlMaker.Items.Clear();

            List<avii.Classes.Maker> lstItemCategoryList = objProductController.getMakerList(-1, "", -1, -1, -1, -1, -1, -1);
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
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            //if (hdnitemGUID.Value != string.Empty)
            Session["skudetail"] = "y";
                Response.Redirect("ManageSKU.aspx");
            //else
             //   Resetvalues();
        }
    }
}