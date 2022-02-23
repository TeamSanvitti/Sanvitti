using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
//using avii.Classes;
using SV.Framework.Models.Inventory;
using SV.Framework.Inventory;

namespace avii.Admin
{
    public partial class NonEsnInventory : System.Web.UI.Page
    {
        private string fileStoreLocation = "~/UploadedData/ESNUpload/";
        private const char DELIMITER = ',';
        private MslOperation mslOperation = MslOperation.CreateInstance<MslOperation>();
        private NonEsnOperation nonEsnOperation = NonEsnOperation.CreateInstance<NonEsnOperation>();

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

                if (Session["esnheaderid"] != null)
                {

                    GetNonEsnDetail();
                }
                else
                {
                    string OerderNo = mslOperation.GenerateOrderNumber();
                    txtOrderNumber.Text = OerderNo;
                    txtOrderNumber.Enabled = false;
                }
            }
        }

        private void GetNonEsnDetail()
        {
            int esnHeaderID = Convert.ToInt32(Session["esnheaderid"]);
            Session["esnheaderid"] = null;
            NonEsnHeader nonEsnHeader = nonEsnOperation.GetNonESNwithHeaderDetails(esnHeaderID);
            if(nonEsnHeader != null && nonEsnHeader.ItemCompanyGUID > 0)
            {
                tblUpload.Visible = false;
                pnlHeader.Visible = true;
                trReceivedate.Visible = true;

                txtBoxQty.Text = nonEsnHeader.PiecesPerBox.ToString();
                txtCarton.Text = nonEsnHeader.CartonCount.ToString();
                txtPallet.Text = nonEsnHeader.PalletCount.ToString();
                txtTotalQty.Text = nonEsnHeader.TotalQty.ToString();
                txtCustOrderNumber.Text = nonEsnHeader.CustomerOrderNumber;
                txtReceiveBy.Text = nonEsnHeader.UserName;
                txtReceiveDate.Text = nonEsnHeader.UploadDate;

                txtOrderNumber.Text = nonEsnHeader.OrderNumber;
                ddlCategory.SelectedValue = nonEsnHeader.CategoryWithProductAllowed;
                dpCompany.SelectedValue = nonEsnHeader.CompanyID.ToString();
                BindCompanySKU(nonEsnHeader.CompanyID, true);
                ddlSKU.SelectedValue = nonEsnHeader.ItemCompanyGUID.ToString();

                ddlReceivedAs.SelectedValue = nonEsnHeader.ReceivedAs;
                if (nonEsnHeader.StorageList.Count > 0)
                {
                    gvMSL.DataSource = nonEsnHeader.StorageList;
                    gvMSL.DataBind();
                    lblCount.Text = "Total count: " + nonEsnHeader.StorageList.Count;

                }
                else
                {

                }



            }

        }
        
        protected void dpCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            int companyID = 0;

            //gvMSL.DataSource = null;
            //gvMSL.DataBind();
            lblMsg.Text = string.Empty;
            //lblConfirm.Text = string.Empty;

            btnSubmit.Visible = false;
            btnUpload.Visible = true;
           // btnSubmit2.Visible = false;
           // pnlSubmit.Visible = false;
            lblCount.Text = string.Empty;
            string CustInfo = string.Empty;
            //  trSKU.Visible = true;
            if (dpCompany.SelectedIndex > 0)
            {
                companyID = Convert.ToInt32(dpCompany.SelectedValue);
                
            }
            if (companyID > 0)
                BindCompanySKU(companyID, false);
            else
            {
                //  trSKU.Visible = true;
                ddlSKU.DataSource = null;
                ddlSKU.DataBind();
            }
        }

        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            int categoryGUID = 0, parentCategoryGUID = 0;
            string CategoryWithProductAllowed = ddlCategory.SelectedValue;
            lblMsg.Text = string.Empty;
            ViewState["IsESNRequired"] = 0;
            //trQty.Visible = true;
           // txtShipQty.Enabled = true;
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
                                            //if (!categoryInfo[0].IsESNRequired)
                                            //{
                                            //    pnlHeader.Visible = true;
                                            //    pnlESN.Visible = false;

                                            //}
                                            //else
                                            //{
                                            //    // trQty.Visible = false;
                                            //    //txtTotalQty.Enabled = false;
                                            //    ViewState["IsESNRequired"] = 1;
                                            //    pnlHeader.Visible = false;
                                            //    pnlESN.Visible = true;
                                            //}


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

        protected void btnBack2Search_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/admin/nonesnsearch.aspx");

        }

        

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            ValidateCSV();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            List<NonEsnStorage> nonesnList = Session["nonesnList"] as  List<NonEsnStorage>;
            NonESNInventory nonESNInventory = new NonESNInventory();
            int cartonCount = 0, palletCount = 0, PiecesPerBox = 0, totalQuantity = 0, CompanyID = 0, itemCompanyGUID = 0, userID=0;
            string orderNumber = "", custOrderNumber = "", ReceivedAs="";
            orderNumber = txtOrderNumber.Text.Trim();
            custOrderNumber = txtCustOrderNumber.Text.Trim();
            ReceivedAs = ddlReceivedAs.SelectedValue;

            int.TryParse(txtCarton.Text.Trim(), out cartonCount);
            int.TryParse(txtPallet.Text.Trim(), out palletCount);
            int.TryParse(txtBoxQty.Text.Trim(), out PiecesPerBox);
            int.TryParse(txtTotalQty.Text.Trim(), out totalQuantity);
            userID = Convert.ToInt32(Session["UserID"]);
            if (dpCompany.SelectedIndex > 0)
            {
                if (ddlSKU.SelectedIndex > 0)
                {
                    if (totalQuantity > 0)
                    {
                        CompanyID = Convert.ToInt32(dpCompany.SelectedValue);
                        itemCompanyGUID = Convert.ToInt32(ddlSKU.SelectedValue);

                        nonESNInventory.CartonCount = cartonCount;
                        nonESNInventory.Comment = txtComment.Text.Trim();
                        nonESNInventory.CompanyID = CompanyID;
                        nonESNInventory.CustomerOrderNumber = custOrderNumber;
                        nonESNInventory.ESNHeaderId = 0;
                        nonESNInventory.ItemCompanyGUID = itemCompanyGUID;
                        nonESNInventory.nonEsnList = nonesnList;
                        nonESNInventory.OrderNumber = orderNumber;
                        nonESNInventory.PalletCount = palletCount;
                        nonESNInventory.PiecesPerBox = PiecesPerBox;
                        nonESNInventory.ReceivedAs = ReceivedAs;
                        nonESNInventory.TotalQuantity = totalQuantity;
                        nonESNInventory.UserID = userID;
                        int index = 0;
                        foreach(GridViewRow row in gvMSL.Rows)
                        {
                            CheckBox chkInspected = row.FindControl("chkInspected") as CheckBox;
                            nonesnList[index].Inspected = chkInspected.Checked;
                            index = index + 1;
                        }

                        if (nonesnList != null && nonesnList.Count > 0)
                        {
                            int insertCount = 0, updateCount = 0;
                            string errorMessage = "";
                            int returnResult = nonEsnOperation.NonESNInventoryInsert(nonESNInventory, out insertCount, out updateCount, out errorMessage);
                            if (!string.IsNullOrEmpty(errorMessage))
                            {
                                lblMsg.Text = errorMessage;
                            }
                            else
                            {
                                if (insertCount > 0 && updateCount > 0)
                                { lblMsg.Text = insertCount + " records  successfully inserted. <br />" + updateCount + " records  successfully updated.";
                                    Session["nonesnList"] = null;
                                }

                                if (insertCount > 0 && updateCount == 0)
                                {
                                    lblMsg.Text = insertCount + " records  successfully inserted.";
                                    Session["nonesnList"] = null;
                                }
                                if (insertCount == 0 && updateCount > 0)
                                {
                                    lblMsg.Text = updateCount + " records  successfully updated.";
                                    Session["nonesnList"] = null;
                                }

                            }
                        }
                    }
                    else
                    { lblMsg.Text = "Total quantity required!"; }
                }
                else
                {
                    lblMsg.Text = "SKU required!";
                }
            }
            else
                lblMsg.Text = "Customer required!";
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }
        protected void lnkDownload_Click(object sender, EventArgs e)
        {
            GenerateCSV();
        }
        private void ValidateCSV()
        {
            gvMSL.DataSource = null;
            gvMSL.DataBind();

            int itemCompanyGUID = 0;
            int returnValue = 0;
            bool IsOrderNumber = false;
            string dateErrorMessage = string.Empty;
            //lblConfirm.Text = string.Empty;

            lblCount.Text = string.Empty;
            lblMsg.Text = string.Empty;
            Hashtable hshESN = new Hashtable();
            int uploadDateRange = 730;
            uploadDateRange = Convert.ToInt32(ConfigurationSettings.AppSettings["UploadAdminDateRange"]);

            string uploaddate = string.Empty, orderNumber = string.Empty, custOrderNumber = string.Empty, orderDate = string.Empty, shipDate = string.Empty, shipVia = string.Empty, trackingNumber = string.Empty;
            int totalQty = 0, shipQty = 0;
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
            //else
            //{ lblMsg.Text = "Customer order number is required"; return; }
            if (!string.IsNullOrEmpty(txtTotalQty.Text))
                totalQty = Convert.ToInt32(txtTotalQty.Text.Trim());
            else
            { lblMsg.Text = "total quantity is required"; return; }

            
            // if (!string.IsNullOrEmpty(txtOrderQty.Text))
            //     orderQty = Convert.ToInt32(txtOrderQty.Text);
            //  else
            // { lblMsg.Text = "Order quantity is required"; return; }

            
            itemCompanyGUID = Convert.ToInt32(ddlSKU.SelectedValue);
            if (itemCompanyGUID > 0)
            {
                //bool uploadEsn = false;
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
                            //System.Text.StringBuilder sbESN = new System.Text.StringBuilder();
                            //System.Text.StringBuilder sbHex = new System.Text.StringBuilder();
                            //System.Text.StringBuilder sbDec = new System.Text.StringBuilder();
                            //System.Text.StringBuilder sbErrors = new System.Text.StringBuilder();
                            NonEsnStorage assignNonESN = null;
                            List<NonEsnStorage> nonesnList = new List<NonEsnStorage>();

                            if (extension == ".csv")
                            {
                                try
                                {
                                    uploadDate = DateTime.Now;

                                    using (StreamReader sr = new StreamReader(fileName))
                                    {
                                        string line;
                                        int SequenceNumber = 0, qty = 0;
                                        string WareHouseLocation, BoxID;//, uploaddate;
                                        int i = 0;
                                        while ((line = sr.ReadLine()) != null)
                                        {
                                            if (!string.IsNullOrEmpty(line) && i == 0)
                                            {
                                                i = 1;
                                                line = line.ToLower();

                                                string[] headerArray = line.Split(',');
                                                if (headerArray.Length == 2 || headerArray.Length <= 9)
                                                {
                                                    if (headerArray[0].Trim() != "seq.no.")
                                                    {
                                                        invalidColumns = headerArray[0];
                                                        columnsIncorrectFormat = true;
                                                    }

                                                    if (headerArray[1].Trim() != "warehouselocation")
                                                    {
                                                        sbInvalidColumns.Append(headerArray[1] + ",");
                                                        columnsIncorrectFormat = true;
                                                    }

                                                    if (headerArray.Length > 2 && headerArray[2].Trim() != "" && headerArray[2].Trim() != "boxid")
                                                    {
                                                        sbInvalidColumns.Append(headerArray[2] + ",");

                                                        columnsIncorrectFormat = true;
                                                    }
                                                    if (headerArray.Length > 3 && headerArray[3].Trim() != "" && headerArray[3].Trim() != "quantity")
                                                    {
                                                        sbInvalidColumns.Append(headerArray[3] + ",");

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
                                                WareHouseLocation = BoxID = string.Empty;
                                                qty = SequenceNumber = 0;
                                                //poNum = sku = customerAccountNumber = fmupc = esn = lteICCID = lteIMSI = otksl = akey = msl = string.Empty;
                                                string[] arr = line.Split(',');
                                                try
                                                {
                                                    assignNonESN = new NonEsnStorage();
                                                    int.TryParse(arr[0].Trim(), out SequenceNumber);
                                                    WareHouseLocation = arr[1].Trim();

                                                    //if (!string.IsNullOrEmpty(esn) && !IsWholeNumber(esn))
                                                    //{
                                                    //    sbESN.Append(esn + ",");
                                                    //}
                                                    if (arr.Length > 2)
                                                    {
                                                        BoxID = arr[2].Trim();

                                                    }

                                                    if (arr.Length > 3)
                                                    {
                                                        int.TryParse(arr[3].Trim(), out qty);

                                                    }


                                                    if (SequenceNumber > 0 && qty > 0)
                                                    {
                                                        // meid = arr[1].Trim();

                                                        assignNonESN.SequenceNumber = SequenceNumber;
                                                        assignNonESN.WareHouseLocation = WareHouseLocation;
                                                        assignNonESN.BoxID = BoxID;
                                                        assignNonESN.Quantity = qty;
                                                        assignNonESN.Inspected = false;

                                                        //if (string.IsNullOrEmpty(esn) || string.IsNullOrEmpty(msl) || string.IsNullOrEmpty(meid) || string.IsNullOrEmpty(akey) || string.IsNullOrEmpty(otksl))
                                                        if (string.IsNullOrEmpty(WareHouseLocation) || string.IsNullOrEmpty(BoxID))
                                                        {
                                                            if (string.IsNullOrEmpty(WareHouseLocation) && string.IsNullOrEmpty(BoxID))
                                                                lblMsg.Text = "Missing BATCH & ESN data";
                                                            else
                                                                if (string.IsNullOrEmpty(WareHouseLocation))
                                                                lblMsg.Text = "Missing ESN data";
                                                            else
                                                                    if (string.IsNullOrEmpty(BoxID))
                                                                lblMsg.Text = "Missing BATCH data";
                                                        }
                                                        if (SequenceNumber == 0 && qty == 0)
                                                            lblMsg.Text = "Sequence number & quantity cannot be 0";
                                                        else if (SequenceNumber == 0 && qty > 0)
                                                            lblMsg.Text = "Sequence number cannot be 0";
                                                        else if (SequenceNumber > 0 && qty == 0)
                                                            lblMsg.Text = "Quantity number cannot be 0";


                                                        nonesnList.Add(assignNonESN);
                                                        WareHouseLocation = string.Empty;
                                                        BoxID = string.Empty;
                                                        SequenceNumber = 0;
                                                        qty = 0;
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
                                catch (Exception ex)
                                {
                                    lblMsg.Text = ex.Message;
                                }
                                if (nonesnList != null && nonesnList.Count > 0 && columnsIncorrectFormat == false)
                                {
                                    gvMSL.DataSource = nonesnList;
                                    gvMSL.DataBind();
                                    lblCount.Text = "Total count: " + nonesnList.Count;
                                    Session["nonesnList"] = nonesnList;
                                    var sumOfQty = nonesnList.Sum(x => x.Quantity);
                                    if (sumOfQty == totalQty)
                                    {
                                        if (lblMsg.Text == string.Empty)
                                        {
                                            //lblMsg.CssClass = "errorGreenMsg";
                                            // lblConfirm.Text = "ESN file is ready to upload";
                                            btnUpload.Visible = false;
                                            btnSubmit.Visible = true;

                                        }
                                        else
                                        {
                                            btnUpload.Visible = true;
                                            btnSubmit.Visible = false;


                                        }
                                    }
                                    else
                                    {
                                        lblMsg.Text = "Quantity in uploaded CSV file and total quantity entered not machting!";
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

                                        
                                    }
                                    else
                                    {

                                        if (nonesnList != null && nonesnList.Count == 0)
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
                                        if (nonesnList != null)
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

        protected void BindCustomer()
        {
            dpCompany.DataSource = avii.Classes.clsCompany.GetCompany(0, 0);
            dpCompany.DataValueField = "CompanyID";
            dpCompany.DataTextField = "CompanyName";
            dpCompany.DataBind();

        }
        private void BindCategory()
        {
            SV.Framework.Inventory.ProductController productController = SV.Framework.Inventory.ProductController.CreateInstance<SV.Framework.Inventory.ProductController>();
            List<ItemCategory> categoryList = productController.GetItemCategoryTree(0, 0, 1, true, -1, -1, true, true, true, false);
            ViewState["categoryList"] = categoryList;
            ddlCategory.DataSource = categoryList;
            ddlCategory.DataTextField = "categoryname";
            ddlCategory.DataValueField = "CategoryWithProductAllowed";
            ddlCategory.DataBind();
            ListItem li = new ListItem("--Select Category--", "0");
            ddlCategory.Items.Insert(0, li);
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
        private void BindCompanySKU(int companyID, bool IsEdit)
        {
            lblMsg.Text = string.Empty;
            List<CompanySKUno> skuList = mslOperation.GetCompanySKUs(companyID, 0);
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



        
        private void GenerateCSV()
        {
            lblMsg.Text = string.Empty;

            string string2CSV = "Seq.No.,WareHouseLocation,BoxID,Quantity" + Environment.NewLine;

            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=NonEsnInventory.csv");
            Response.Charset = "";
            Response.ContentType = "application/text";
            Response.Output.Write(string2CSV);
            Response.Flush();
            Response.End();

        }

    }
}