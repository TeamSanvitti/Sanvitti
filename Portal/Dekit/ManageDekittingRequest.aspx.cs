//using avii.Classes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SV.Framework.Models.SOR;
using SV.Framework.Models.Common;
using SV.Framework.SOR;

namespace avii
{
    public partial class ManageDekittingRequest : System.Web.UI.Page
    {
        
        private string fileStoreLocation = "~/UploadedData/";
        private const char DELIMITER = ',';

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
                DekitOperations dekitOperations = DekitOperations.CreateInstance<DekitOperations>();

                BindCustomer();
                BindSORStatus();
                btnSubmit.Visible = false;
                trUpload.Visible = true;
                //btnPrint.Visible = false;
                //btnValidate.Visible = false;
                // trUpload.Visible = false;
                btnCancel.Visible = false;
                txtDekitRequestNo.Text = dekitOperations.GenerateServiceOrder();

                txtDekitRequestNo.Enabled = false;
                pnlSKU.Visible = false;
            }

        }
        

        //public int IncreementIndex { get; set; }
        protected void BindCustomer()
        {
            dpCompany.DataSource = avii.Classes.clsCompany.GetCompany(0, 0);
            dpCompany.DataValueField = "CompanyID";
            dpCompany.DataTextField = "CompanyName";
            dpCompany.DataBind();

        }
        private void BindSORStatus()
        {
            SV.Framework.SOR.DekitOperations dekitOperations = SV.Framework.SOR.DekitOperations.CreateInstance<SV.Framework.SOR.DekitOperations>();

            List<ServiceRequestStatus> statusList = dekitOperations.GetDeKitStatusList();
            ddlStatus.DataSource = statusList;
            ddlStatus.DataValueField = "StatusID";
            ddlStatus.DataTextField = "Status";
            ddlStatus.DataBind();
        }
        protected void dpCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            int companyID = 0;

            gvEsn.DataSource = null;
            gvEsn.DataBind();
            //rptESN.Visible = false;
            lblMsg.Text = string.Empty;
            // lblConfirm.Text = string.Empty;

            btnSubmit.Visible = false;
            trUpload.Visible = true;
            lblCount.Text = string.Empty;
            //ClearForm();
            //txtCustOrderNo.Text = string.Empty;
            ddlKitted.Items.Clear();
            string CustInfo = string.Empty;
            //  trSKU.Visible = true;
            if (dpCompany.SelectedIndex > 0)
                companyID = Convert.ToInt32(dpCompany.SelectedValue);
            if (companyID > 0)
            {
                BindCompanySKU(companyID);
                BindUsers(companyID);
            }
            else
            {
                //  trSKU.Visible = true;
                ddlKitted.DataSource = null;
                ddlKitted.DataBind();

            }
        }
        protected void ddlKitted_SelectedIndexChanged(object sender, EventArgs e)
        {
            rptSKUs.DataSource = null;
            rptSKUs.DataBind();

            btnSubmit.Visible = false;
            btnCancel.Visible = false;
            trUpload.Visible = true;
            gvEsn.DataSource = null;
            gvEsn.DataBind();

            int companyID = 0, itemCompanyGUID = 0;
            if (dpCompany.SelectedIndex > 0)
            {
                companyID = Convert.ToInt32(dpCompany.SelectedValue);
                
                if (ddlKitted != null && ddlKitted.Items.Count > 0)
                {
                    if (ddlKitted.SelectedIndex > 0)
                    {
                        itemCompanyGUID = Convert.ToInt32(ddlKitted.SelectedValue);
                        if (ViewState["skulist"] != null)
                        {
                            List<SV.Framework.Models.Catalog.CompanySKUno> skuList = ViewState["skulist"] as List<SV.Framework.Models.Catalog.CompanySKUno>;
                            if (skuList != null)
                            {
                                var skuInfo = (from item in skuList where item.ItemcompanyGUID.Equals(itemCompanyGUID) select item).ToList();
                                if (skuInfo != null && skuInfo.Count > 0 && !string.IsNullOrEmpty(skuInfo[0].SKU))
                                {
                                    lblCurrentStock.Text = skuInfo[0].CurrentStock.ToString();
                                    if(skuInfo[0].IsKittedBox && !skuInfo[0].IsESNRequired)
                                    {
                                        trUpload.Visible = false;

                                        btnSubmit.Visible = true;
                                        btnCancel.Visible = true;

                                    }
                                }
                            }
                        }
                        BindRawSKUs(companyID, itemCompanyGUID);
                    }
                    else
                    {
                        lblMsg.Text = "SKU is required!";
                        ddlKitted.DataSource = null;
                        ddlKitted.DataBind();
                    }

                }
                else
                {
                    lblMsg.Text = "No SKU/Product are assigned to selected Customer";
                    ddlKitted.Items.Clear();
                    ddlKitted.DataSource = null;
                    ddlKitted.DataBind();

                }
            }
            else
            {
                lblMsg.Text = "Customer is required!";
                dpCompany.DataSource = null;
                dpCompany.DataBind();
            }

        }

        private void BindRawSKUs(int companyID, int itemCompanyGUID)
        {
            pnlSKU.Visible = true;
            Session["RawSKUs"] = null;
            //ddlSKU.Items.Clear();
            //plnSKU.Visible = false;
            //btnSubmit.Visible = false;
            //btnCancel.Visible = false;
            //trHr.Visible = false;
            lblMsg.Text = string.Empty;
            SV.Framework.Catalog.FinishSKUOperations FinishSKUOperations = SV.Framework.Catalog.FinishSKUOperations.CreateInstance<SV.Framework.Catalog.FinishSKUOperations>();

            //companyID = itemCompanyGUID = 0;
            if (companyID > 0)
            {
                if (itemCompanyGUID > 0)
                {
                    //companyID = Convert.ToInt32(dpCompany.SelectedValue);
                    //if (ddlKitted != null && ddlKitted.Items.Count > 0)
                    {
                        //if (ddlKitted.SelectedIndex > 0)
                        //   itemCompanyGUID = Convert.ToInt32(ddlKitted.SelectedValue);

                        List<SV.Framework.Models.Catalog.RawSKU> rawSKUList = FinishSKUOperations.GetKittedAssignedRawSKUs(companyID, itemCompanyGUID);
                        if (rawSKUList != null && rawSKUList.Count > 0)
                        {
                            rptSKUs.DataSource = rawSKUList;
                            //ddlSKU.DataValueField = "ItemcompanyGUID";
                            //ddlSKU.DataTextField = "SKU";
                            rptSKUs.DataBind();
                            // ListItem newList = new ListItem("", "");
                            // ddlSKU.Items.Insert(0, newList);
                            Session["RawSKUs"] = rawSKUList;
                            
                            //btnValidate.Visible = false;
                            //btnValidate.Visible = true;
                            //btValidate.Visible = false;
                            //if (lblMsg.Text == string.Empty)
                            //{
                            //    //plnSKU.Visible = true;
                            //    btnSubmit.Visible = true;
                            //    btnCancel.Visible = true;
                            //    //trHr.Visible = true;
                            //    //lblMsg.CssClass = "errorGreenMsg";
                            //    //lblConfirm.Text = "SIM file is ready to upload";
                            //    //btnSearch.Visible = false;
                            //    btnSubmit.Visible = true;
                            //    /// btnSubmit2.Visible = true;
                            //    pnlSearch.Visible = true;
                            //    // trSKU.Visible = false;

                            //}
                            //else
                            //{
                            //    // trSKU.Visible = false;
                            //    //btnSearch.Visible = true;
                            //    btnSubmit.Visible = false;

                            //    //  btnSubmit2.Visible = false;
                            //    //pnlSearch.Visible = false;

                            //}
                        }
                        else
                        {
                            //btnValidate.Visible = false;
                            //btnSubmit.Visible = false;
                            //btnCancel.Visible = false;
                            // ddlSKU.Items.Clear();
                            //trHr.Visible = false;
                            rptSKUs.DataSource = null;
                            rptSKUs.DataBind();
                            lblMsg.Text = "No SKU are assigned!";
                            
                        }
                    }
                    //else
                    //{
                    //    lblMsg.Text = "No SKU/Product are assigned to selected Customer";
                    //    ddlKitted.Items.Clear();
                    //    ddlKitted.DataSource = null;
                    //    ddlKitted.DataBind();

                    //}

                }
                else
                {
                    lblMsg.Text = "SKU is required!";
                    ddlKitted.DataSource = null;
                    ddlKitted.DataBind();
                }
            }
            else
            {
                lblMsg.Text = "Customer is required!";
                dpCompany.DataSource = null;
                dpCompany.DataBind();
            }
        }

        private void BindCompanySKU(int companyID)
        {
            SV.Framework.Catalog.FinishSKUOperations FinishSKUOperations = SV.Framework.Catalog.FinishSKUOperations.CreateInstance<SV.Framework.Catalog.FinishSKUOperations>();

            lblMsg.Text = string.Empty;
            List<SV.Framework.Models.Catalog.CompanySKUno> skuList = FinishSKUOperations.GetCompanyFinalOrRawSKUList(companyID, true);
            if (skuList != null && skuList.Count > 0)
            {
                //Session["kittedskulist"] = skuList;

                ddlKitted.DataSource = skuList;
                ddlKitted.DataValueField = "ItemcompanyGUID";
                ddlKitted.DataTextField = "SKU";

                ddlKitted.DataBind();
                ListItem newList = new ListItem("", "");
                ddlKitted.Items.Insert(0, newList);
            }
            if (skuList != null)
            {
                ViewState["skulist"] = skuList;
                ddlKitted.DataSource = skuList;
                ddlKitted.DataValueField = "ItemcompanyGUID";
                ddlKitted.DataTextField = "SKU";


                ddlKitted.DataBind();
                ListItem item = new ListItem("", "0");
                ddlKitted.Items.Insert(0, item);
            }
            else
            {
                ViewState["skulist"] = null;
                ddlKitted.DataSource = null;
                ddlKitted.DataBind();
                lblMsg.Text = "No SKU are assigned to selected Customer";

            }


        }

        private void BindUsers(int companyID)
        {
            avii.Classes.UserUtility objUser = new avii.Classes.UserUtility();
            List<avii.Classes.clsUserManagement> userList = objUser.getUserList("", companyID, "", -1, -1, -1, true);
            if (userList != null && userList.Count > 0)
            {
                ddlUser.DataSource = userList;
                ddlUser.DataValueField = "UserID";
                ddlUser.DataTextField = "UserName";
                ddlUser.DataBind();

                ListItem newList = new ListItem("", "0");
                ddlUser.Items.Insert(0, newList);
            }
            else
            {
                ddlUser.DataSource = null;
                ddlUser.DataBind();
                lblMsg.Text = "No users are assigned to selected Customer";
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            SV.Framework.SOR.DekitOperations dekitOperations = SV.Framework.SOR.DekitOperations.CreateInstance<SV.Framework.SOR.DekitOperations>();

            DekitServiceOrder serviceOrder = new DekitServiceOrder();
            List<DekitOrderDetail> dekitDetails = new List<DekitOrderDetail>();
            List<DekitESN> esnList = new List<DekitESN>();
            DekitOrderDetail dekitOrderDetail = new DekitOrderDetail();
            DekitESN esnDetail = null;
            string errorMessage = string.Empty;
            int KittedSKUId = 0, qty = 1;

            //bool IsValidate = true, IsMappedSKU = false;
            int numberOfESN = 0, requiredQty = 0, requestedBy = 0, userID = 0;
            if (Session["UserID"] != null)
                userID = Convert.ToInt32(Session["UserID"]);
            if (ddlUser.SelectedIndex > 0)
                requestedBy = Convert.ToInt32(ddlUser.SelectedValue);
            lblCount.Text = string.Empty;
            lblMsg.Text = string.Empty;
            string customerRequestNumber = txtCustomerRequestNo.Text.Trim();
            string dekitRequestNumber = txtDekitRequestNo.Text.Trim();
            List<SV.Framework.Models.Catalog.RawSKU> RawSKUs =  Session["RawSKUs"] as List<SV.Framework.Models.Catalog.RawSKU>;
            if (dpCompany.SelectedIndex > 0)
            {
                if (ddlKitted.SelectedIndex > 0)
                {
                    KittedSKUId = Convert.ToInt32(ddlKitted.SelectedValue);
                    int.TryParse(txtOrderQty.Text.Trim(), out qty);
                    if (qty > 0)
                    {
                        if (RawSKUs != null && RawSKUs.Count > 0)
                        {
                            foreach (SV.Framework.Models.Catalog.RawSKU item in RawSKUs)
                            {
                                dekitOrderDetail = new DekitOrderDetail();
                                dekitOrderDetail.DeKittingDetailID = 0;
                                dekitOrderDetail.DestinationItemCompanyGUID = item.ItemcompanyGUID;
                                dekitOrderDetail.ItemCompanyGUID = KittedSKUId;
                                dekitOrderDetail.Quantity = qty;
                                dekitDetails.Add(dekitOrderDetail);
                                if (item.MappedItemcompanyGUID > 0)
                                {
                                    dekitOrderDetail = new DekitOrderDetail();
                                    dekitOrderDetail.DeKittingDetailID = 0;
                                    dekitOrderDetail.DestinationItemCompanyGUID = item.MappedItemcompanyGUID;
                                    dekitOrderDetail.ItemCompanyGUID = KittedSKUId;
                                    dekitOrderDetail.Quantity = qty;
                                    dekitDetails.Add(dekitOrderDetail);
                                }
                            }
                        }
                        foreach(GridViewRow row in gvEsn.Rows)
                        {
                            Label hdnSKUId = row.FindControl("hdnSKUId") as Label;
                            Label hdnMappedItemCompanyGUID = row.FindControl("hdnMappedItemCompanyGUID") as Label;
                            TextBox txtESN = row.FindControl("txtESN") as TextBox;
                            TextBox txtICCID = row.FindControl("txtICCID") as TextBox;
                            if(hdnSKUId != null && txtESN != null)
                            {
                                esnDetail = new DekitESN();
                                esnDetail.ItemCompanyGUID = Convert.ToInt32(hdnSKUId.Text);
                                esnDetail.MappedItemCompanyGUID = Convert.ToInt32(hdnMappedItemCompanyGUID.Text);
                                esnDetail.ESN = txtESN.Text;
                                esnDetail.ICCID = txtICCID.Text;
                                esnList.Add(esnDetail);
                            }
                        }
                        serviceOrder.CompanyId = Convert.ToInt32(dpCompany.SelectedValue);
                        serviceOrder.CustomerRequestNumber = customerRequestNumber;
                        serviceOrder.DekitRequestNumber = dekitRequestNumber;
                        serviceOrder.CreatedBy = userID;
                        serviceOrder.RequestedBy = requestedBy;
                        serviceOrder.DekitDetails = dekitDetails;
                        serviceOrder.EsnList = esnList;

                        int returnResult = dekitOperations.DeKittingRequestInsertUpdate(serviceOrder, out errorMessage);
                        if(returnResult > 0)
                        {
                            ClearForm();
                            lblMsg.Text = "Submitted successfuly";
                        }
                    }
                }
            }
        }
        protected void btnUploadValidate_Click(object sender, EventArgs e)
        {
            UploadInventoryInfo();
        }
        private void UploadInventoryInfo()
        {
            SV.Framework.SOR.DekitOperations dekitOperations = SV.Framework.SOR.DekitOperations.CreateInstance<SV.Framework.SOR.DekitOperations>();

            DekitServiceOrder serviceOrder = new DekitServiceOrder();
            List<DekitOrderDetail> dekitDetails = new List<DekitOrderDetail>();
            List<DekitESN> esnList = new List<DekitESN>();
            DekitOrderDetail dekitOrderDetail = new DekitOrderDetail();
            DekitESN esnDetail = null;
            string errorMessage = string.Empty;
            int KittedSKUId = 0, qty = 1;

            bool IsValidate = true, IsMappedSKU = false;
            int numberOfESN = 0, requiredQty = 0, mappedItemCompanyGUID = 0;
            lblCount.Text = string.Empty;
            lblMsg.Text = string.Empty;
            Hashtable hshESN = new Hashtable();
            bool IsESNRequired = false;
            bool IsFileSizeValid = true;
            int currentStock = 0;
            bool columnsIncorrectFormat = false;
            string customerRequestNumber = txtCustomerRequestNo.Text.Trim();
            string dekitRequestNumber = txtDekitRequestNo.Text.Trim();
            if (dpCompany.SelectedIndex > 0)
            {
                if (ddlKitted.SelectedIndex > 0)
                {
                    KittedSKUId = Convert.ToInt32(ddlKitted.SelectedValue);
                    int.TryParse(txtOrderQty.Text.Trim(), out qty);
                    int.TryParse(lblCurrentStock.Text.Trim(), out currentStock);

                    if (qty > 0)
                    {
                        if (currentStock >= qty)
                        {
                            foreach (RepeaterItem item in rptSKUs.Items)
                            {
                                HiddenField hdIsESNRequired = item.FindControl("hdIsESNRequired") as HiddenField;
                                IsESNRequired = Convert.ToBoolean(hdIsESNRequired.Value);
                                if (IsESNRequired)
                                {
                                    HiddenField hdMappedItemCompanyGUID = item.FindControl("hdMappedItemCompanyGUID") as HiddenField;

                                    int.TryParse(hdMappedItemCompanyGUID.Value, out mappedItemCompanyGUID);
                                    if (mappedItemCompanyGUID > 0)
                                    {
                                        IsMappedSKU = true;
                                    }
                                    HiddenField hdnQty = item.FindControl("hdnQty") as HiddenField;
                                    int.TryParse(hdnQty.Value, out requiredQty);
                                    if (requiredQty == 0)
                                        requiredQty = 1;
                                    numberOfESN += qty * requiredQty;
                                }
                            }
                            if(numberOfESN == 0)
                            {
                                numberOfESN = qty;
                            }
                            if (!string.IsNullOrEmpty(dekitRequestNumber))
                            {
                                serviceOrder.CompanyId = Convert.ToInt32(dpCompany.SelectedValue);
                                serviceOrder.CustomerRequestNumber = customerRequestNumber;
                                // serviceOrder.ServiceOrderNumber = dekitRequestNumber;
                                dekitOrderDetail.ItemCompanyGUID = KittedSKUId;

                                dekitOrderDetail.Quantity = qty;
                                dekitDetails.Add(dekitOrderDetail);
                                serviceOrder.DekitDetails = dekitDetails;

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
                                            string fileName = UploadFile(out IsFileSizeValid);

                                            string extension = Path.GetExtension(flnUpload.PostedFile.FileName);
                                            extension = extension.ToLower();
                                            string invalidColumns = string.Empty;

                                            if (extension == ".csv")
                                            {
                                                try
                                                {
                                                    using (StreamReader sr = new StreamReader(fileName))
                                                    {
                                                        string line;
                                                        string esn, ICCID;
                                                        int i = 0;
                                                        while ((line = sr.ReadLine()) != null)
                                                        {

                                                            if (!string.IsNullOrEmpty(line) && i == 0)
                                                            {
                                                                i = 1;
                                                                line = line.ToLower();
                                                                string[] headerArray = line.Split(',');

                                                                if (headerArray[0].Trim() != "esn")
                                                                {
                                                                    if (string.IsNullOrEmpty(invalidColumns))
                                                                        invalidColumns = headerArray[0];
                                                                    else
                                                                        invalidColumns = invalidColumns + ", " + headerArray[0];
                                                                    columnsIncorrectFormat = true;
                                                                }
                                                                if (headerArray.Length > 1 && headerArray[1].Trim() != "")
                                                                {
                                                                    if (headerArray[1].Trim() != "iccid")
                                                                    {
                                                                        if (string.IsNullOrEmpty(invalidColumns))
                                                                            invalidColumns = headerArray[1];
                                                                        else
                                                                            invalidColumns = invalidColumns + ", " + headerArray[1];
                                                                        columnsIncorrectFormat = true;
                                                                    }
                                                                }

                                                            }
                                                            else if (!string.IsNullOrEmpty(line) && i > 0)
                                                            {
                                                                esn = ICCID = string.Empty;// fmupc = lteICCID = lteIMSI = otksl = akey = msl = string.Empty;
                                                                string[] arr = line.Split(',');
                                                                try
                                                                {

                                                                    if (arr.Length > 0)
                                                                    {
                                                                        esn = arr[0].Trim();
                                                                    }
                                                                    if (arr.Length > 1)
                                                                    {
                                                                        ICCID = arr[1].Trim();

                                                                    }

                                                                    if (string.IsNullOrEmpty(esn))
                                                                    {
                                                                        lblMsg.Text = "Missing required data";
                                                                    }

                                                                    esnDetail = new DekitESN();

                                                                    if (hshESN.ContainsKey(esn) && !string.IsNullOrEmpty(esn))
                                                                    {
                                                                        //uploadEsn = false;
                                                                        throw new ApplicationException("Duplicate ESN(s) exists in the file");
                                                                    }
                                                                    else if (!hshESN.ContainsKey(esn) && !string.IsNullOrEmpty(esn))
                                                                    {
                                                                        hshESN.Add(esn, esn);
                                                                    }
                                                                    //uploadEsn = true;
                                                                    esnDetail.ESN = esn;
                                                                    esnDetail.ICCID = ICCID;
                                                                    esnList.Add(esnDetail);

                                                                    esn = string.Empty;
                                                                    ICCID = string.Empty;
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


                                                if (esnList != null && esnList.Count > 0 && columnsIncorrectFormat == false)
                                                {
                                                    //rptESN.DataSource = esnList;
                                                    //rptESN.DataBind();
                                                    //lblCount.Text = "Total count: " + esnList.Count;
                                                    Session["esns"] = esnList;

                                                    int n = 0;
                                                    int poRecordCount = 0;
                                                    //string poErrorMessage = string.Empty;
                                                    //string invalidLTEESNMessage = string.Empty;
                                                    //string invalidESNMessage = string.Empty;
                                                    //string invalidSkuESNMessage = string.Empty;
                                                    //string esnExistsMessage = string.Empty;
                                                    //string badESNMessage = string.Empty;
                                                    //string errorMessage = string.Empty;
                                                    List<ServiceOrderDetail> esnList1 = new List<ServiceOrderDetail>();
                                                    double totalChunk = 0;
                                                    try
                                                    {

                                                        totalChunk = (double)esnList.Count / 3000;
                                                        n = Convert.ToInt16(Math.Ceiling(totalChunk));
                                                        int esnCount = 3000;
                                                        //int skip = 1000;
                                                        int listLength = esnList.Count;
                                                        List<DekitESN> esnToUpload = null;
                                                        //var esnToUpload;
                                                        //for (int i = 0; i < n; i++)
                                                        for (int i = 0; i < listLength; i = i + 3000)
                                                        {

                                                            esnToUpload = new List<DekitESN>();
                                                            serviceOrder.EsnList = new List<DekitESN>();
                                                            //invalidLTEESNMessage = invalidESNMessage = invalidSkuESNMessage = esnExistsMessage = badESNMessage = string.Empty;
                                                            //esnToAdd = new List<FulfillmentAssignESN>();
                                                            //if (esnList.Count < 1000)
                                                            //{   esnCount = esnList.Count;
                                                            //    skip = esnList.Count;
                                                            //    if (i > 0)
                                                            //        skip = skip + 1000;
                                                            //}

                                                            esnToUpload = (from item in esnList.Skip(i).Take(esnCount) select item).ToList();

                                                            serviceOrder.EsnList = esnToUpload;
                                                            //Upload/Assign ESN to POs
                                                            int returnValue = 0;
                                                            string AlreadyInUseICCIDMessase = string.Empty, ICCIDNotExistsMessase = string.Empty, InvalidICCIDMessase = string.Empty, AlreadyMappedESNMessase = string.Empty;

                                                            //List<ServiceOrderDetail> esnList2 = ServiceOrderOperation.ValidateServiceOrder(serviceOrder, out errorMessage, out IsValidate);
                                                            esnList = dekitOperations.ValidateDeKittingESN(serviceOrder, out errorMessage, out IsValidate);
                                                            if (esnList != null && esnList.Count > 0)
                                                            {
                                                                foreach (DekitESN item in esnList)
                                                                {
                                                                    if (item.MappedItemCompanyGUID > 0)
                                                                        IsMappedSKU = true;
                                                                }
                                                                gvEsn.DataSource = esnList;
                                                                gvEsn.DataBind();
                                                                foreach (GridViewRow item in gvEsn.Rows)
                                                                {

                                                                    TextBox txtESN = item.FindControl("txtESN") as TextBox;
                                                                    txtESN.Enabled = false;
                                                                    TextBox txtICCID = item.FindControl("txtICCID") as TextBox;
                                                                    txtICCID.Enabled = false;
                                                                }
                                                                //gvEsn.Columns[5].Visible = false;
                                                                if (!IsMappedSKU)
                                                                {
                                                                    gvEsn.Columns[3].Visible = false;
                                                                    gvEsn.Columns[4].Visible = false;
                                                                }
                                                                Session["esnList"] = esnList;
                                                                //pnlSearch.Visible = true;
                                                                lblCounts.Text = "Total count: " + esnList.Count;
                                                                //txtOrderQty.Text = esnList.Count.ToString();

                                                                if (IsValidate && string.IsNullOrEmpty(errorMessage) && numberOfESN == esnList.Count)
                                                                {
                                                                    // btnAdd.Visible = false;
                                                                    //btnValidate.Visible = false;
                                                                    //btValidate.Visible = false;
                                                                    //trUpload.Visible = false;
                                                                    //txtCustOrderNo.Enabled = false;
                                                                    // txtOrderQty.Enabled = false;

                                                                    btnSubmit.Visible = true;
                                                                    btnCancel.Visible = true;
                                                                    trUpload.Visible = false;
                                                                }
                                                                else
                                                                {
                                                                    if (!IsValidate)
                                                                    {
                                                                        //btnValidate.Visible = false;
                                                                        trUpload.Visible = true;
                                                                    }
                                                                    if (numberOfESN != esnList.Count)
                                                                    {
                                                                        lblMsg.Text = "The ESN/ICCID required for number of kits entered are not correct!";
                                                                    }
                                                                    if (!string.IsNullOrEmpty(errorMessage))
                                                                        lblMsg.Text = errorMessage + " Customer order number already exists!";
                                                                }
                                                            }
                                                            else
                                                            {
                                                                gvEsn.DataSource = null;
                                                                gvEsn.DataBind();
                                                            }
                                                            //esnList1.AddRange(esnList2);





                                                        }

                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        lblMsg.Text = ex.Message;
                                                    }
                                                    if (!string.IsNullOrEmpty(errorMessage))
                                                    {
                                                        lblMsg.Text = errorMessage;
                                                        return;
                                                    }
                                                    if (lblMsg.Text == string.Empty)
                                                    {
                                                        //lblMsg.CssClass = "errorGreenMsg";
                                                        //lblConfirm.Text = "ESN file is ready to upload";
                                                        //btnUpload.Visible = false;
                                                        //btnSubmit.Visible = true;
                                                        //btnSubmit2.Visible = true;
                                                        //pnlSubmit.Visible = true;
                                                        //row1.Visible = false;
                                                        //row2.Visible = false;
                                                        ////btnSearch.Visible = false;

                                                    }
                                                    else
                                                    {
                                                        //btnUpload.Visible = true;
                                                        //btnSubmit.Visible = false;
                                                        //row1.Visible = true;
                                                        //row2.Visible = true;
                                                        //btnSubmit2.Visible = false;
                                                        //pnlSubmit.Visible = false;

                                                    }

                                                }
                                                else
                                                {
                                                    gvEsn.DataSource = null;
                                                    gvEsn.DataBind();

                                                    if (esnList != null && esnList.Count == 0)
                                                    {
                                                        if (columnsIncorrectFormat)
                                                        {
                                                            lblMsg.Text = invalidColumns + " column(s) name is not correct";
                                                        }
                                                        else
                                                            lblMsg.Text = "There is no ESN to upload";

                                                    }
                                                    if (esnList != null)
                                                    {
                                                        if (columnsIncorrectFormat)
                                                        {
                                                            lblMsg.Text = invalidColumns + " column(s) name is not correct";
                                                        }
                                                        else
                                                            lblMsg.Text = "There is no ESN to upload";
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
                        }
                        else
                            lblMsg.Text = "Dekitting quantity should not be greater than current stock!";
                    }
                    else
                        lblMsg.Text = "Please enter number of kits!";

                }
                else
                    lblMsg.Text = "Please select Kitted SKU!";

            }
            else
                lblMsg.Text = "Please select customer!";
        }
        protected void btnCSV_Click(object sender, EventArgs e)
        {
            GenerateCSV();
        }
        private void GenerateCSV()
        {
            // IncreementIndex = 1;
            string errorMessage = string.Empty, skus = string.Empty;
            List<DekitCSV> esnList = new List<DekitCSV>();
            DekitCSV esnInfo = new DekitCSV();
            lblMsg.Text = string.Empty;
          //  int qty = 0, itemQty = 0, requiredQty = 0; ;
            //int ItemcompanyGUID = 0, mappedItemCompanyGUID = 0;
            //bool ICCID = false;
            //bool IsMappedSKU = false;
          //  bool IsESNRequired = false;
            string esns = string.Empty;
            //int.TryParse(txtOrderQty.Text.Trim(), out qty);
            //if (qty > 0)
            //{
            //    for (int i = 1; i <= qty; i++)
            //    {

            //        foreach (RepeaterItem item in rptSKUs.Items)
            //        {

            //            itemQty = 0;
            //            HiddenField hdIsESNRequired = item.FindControl("hdIsESNRequired") as HiddenField;
            //            IsESNRequired = Convert.ToBoolean(hdIsESNRequired.Value);
            //            if (IsESNRequired)
            //            {
            //                TextBox txtICCID = item.FindControl("txtICCID") as TextBox;
            //                TextBox txtsku = item.FindControl("txtsku") as TextBox;
            //                HiddenField hdSKUId = item.FindControl("hdSKUId") as HiddenField;
            //                HiddenField hdMappedItemCompanyGUID = item.FindControl("hdMappedItemCompanyGUID") as HiddenField;

            //                HiddenField hdnQty = item.FindControl("hdnQty") as HiddenField;
            //                int.TryParse(hdnQty.Value, out requiredQty);
            //                if (requiredQty == 0)
            //                    requiredQty = 1;
            //                itemQty = qty * requiredQty;


            //                int.TryParse(hdSKUId.Value, out ItemcompanyGUID);
            //                int.TryParse(hdMappedItemCompanyGUID.Value, out mappedItemCompanyGUID);
            //                if (mappedItemCompanyGUID > 0)
            //                {
            //                    // IsMappedSKU = true;
            //                   // IncreementIndex = 2;
            //                }

            //                esnInfo = new DekitCSV();
            //                //esnInfo.ItemCompanyGUID = ItemcompanyGUID;
            //             //   esnInfo.SKU = txtsku.Text;
            //                //esnInfo.MappedItemCompanyGUID = mappedItemCompanyGUID;
            //                esnInfo.ESN = string.Empty;// esn.ToString();
            //                esnInfo.ICCID = string.Empty;//esnInfo.ESN;
            //                                             //esnInfo.BatchNumber = "";
            //                                             // esnInfo.ValidationMsg = "";
            //                                             //esnInfo.IsPrint = true;
            //                esnList.Add(esnInfo);

            //            }
            //        }
            //    }
            //}
            //else
            //{
            //    lblMsg.Text = "Quantity is required!";
            //    return;
            //}
            esnInfo.ESN = string.Empty;
            esnInfo.ICCID = string.Empty;
            esnList.Add(esnInfo);
            //DekitCSV esnInfo1 = new DekitCSV();
            


            if (esnList != null && esnList.Count > 0)
            {
                string string2CSV = esnList.ToCSV();

                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=DekittingESNs.csv");
                Response.Charset = "";
                Response.ContentType = "application/text";
                Response.Output.Write(string2CSV);
                Response.Flush();
                Response.End();

            }
            else
            {

            }
        }
        private void ClearForm()
        {
            lblCounts.Text = string.Empty;
            lblCount.Text = string.Empty;
            lblMsg.Text = string.Empty;

            btnSubmit.Visible = false;
            trUpload.Visible = true;
            //btnPrint.Visible = false;
            //btnValidate.Visible = false;
            //btValidate.Visible = false;
            btnCancel.Visible = false;
           // pnlSearch.Visible = false;
           // btnAdd.Visible = false;
            //BindCustomer();
            //int serviceOerderNo = ServiceOrderOperation.GenerateServiceOrder();
            //txtSONumber.Text = serviceOerderNo.ToString();
           // txtOrderDate.Text = DateTime.Now.ToShortDateString();
            txtDekitRequestNo.Enabled = false;
            //txtOrderDate.Enabled = false;
            txtOrderQty.Text = string.Empty;
            txtCustomerRequestNo.Text = string.Empty;
            txtCustomerRequestNo.Enabled = true;
            txtOrderQty.Enabled = true;
            //ddlKitted.SelectedIndex = 0;
            ddlKitted.Items.Clear();
            ddlUser.Items.Clear();
            dpCompany.SelectedIndex = 0;

            rptSKUs.DataSource = null;
            rptSKUs.DataBind();
            gvEsn.DataSource = null;
            gvEsn.DataBind();
            pnlSKU.Visible = false;



        }
        
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        protected void gvEsn_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //header select all function
            if (e.Row.RowType == DataControlRowType.Header)
            {
                ((CheckBox)e.Row.FindControl("chkAll")).Attributes.Add("onclick",
                    "javascript:SelectAll('" +
                    ((CheckBox)e.Row.FindControl("chkAll")).ClientID + "')");
            }
        }
        private string UploadFile(out bool IsValid)
        {
            IsValid = true;
            string actualFilename = string.Empty;
            Int32 maxFileSize = 1572864;
            actualFilename = System.IO.Path.GetFileName(flnUpload.PostedFile.FileName);
            if (ConfigurationManager.AppSettings["FulfillmentFilesStoreage"] != null)
            {
                fileStoreLocation = ConfigurationManager.AppSettings["FulfillmentFilesStoreage"].ToString();
            }

            fileStoreLocation = Server.MapPath(fileStoreLocation);
            if (File.Exists(fileStoreLocation + actualFilename))
            {
                actualFilename = System.Guid.NewGuid().ToString() + actualFilename;
            }


            flnUpload.PostedFile.SaveAs(fileStoreLocation + actualFilename);
            ViewState["filename"] = fileStoreLocation + actualFilename;

            FileInfo fileInfo = new FileInfo(fileStoreLocation + actualFilename);

            if (ConfigurationManager.AppSettings["maxCSVfilesize"] != null)
            {
                if (Int32.TryParse(ConfigurationManager.AppSettings["maxCSVfilesize"].ToString(), out maxFileSize))
                {
                    if (fileInfo.Length > maxFileSize)
                    {
                        fileInfo.Delete();
                        int fileSize = maxFileSize / (1024 * 1024);
                        lblMsg.Text = "File size is greater than " + fileSize + " MB";
                        IsValid = false;
                        //throw new Exception("File size is greater than " + maxFileSize + " bytes");
                    }
                }
            }



            return fileStoreLocation + actualFilename;
        }

    }
}