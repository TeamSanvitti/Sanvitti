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
using SV.Framework.Models.Inventory;

namespace avii.ESNBlock
{
    public partial class BlockESNs : System.Web.UI.Page
    {
        private SV.Framework.Inventory.BlockEsnOperation blockEsnOperation = SV.Framework.Inventory.BlockEsnOperation.CreateInstance<SV.Framework.Inventory.BlockEsnOperation>();

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
                int companyID = 0;
                btnSubmit.Visible = true;
                if (Session["adm"] != null)
                {
                    BindCustomer();
                   // hdnCustomer.Value = "1";

                    //companyID = 464;
                }
                else
                {
                   // hdnCustomer.Value = "";
                    trCustomer.Visible = false;
                    if (Session["userInfo"] != null)
                    {
                        avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
                        if (userInfo != null)
                        {
                            companyID = userInfo.CompanyGUID;
                            ViewState["companyID"] = companyID;
                        }
                    }
                    BindCompanySKU(companyID);
                    BindUsers(companyID);
                }
                
            }
        }

        protected void BindCustomer()
        {
            dpCompany.DataSource = avii.Classes.clsCompany.GetCompany(0, 0);
            dpCompany.DataValueField = "CompanyID";
            dpCompany.DataTextField = "CompanyName";
            dpCompany.DataBind();
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

        private void BindCompanySKU(int companyID)
        {
            lblMsg.Text = string.Empty;
            SV.Framework.Inventory.MslOperation mslOperation = SV.Framework.Inventory.MslOperation.CreateInstance<SV.Framework.Inventory.MslOperation>();
            List<CompanySKUno> skuList = mslOperation.GetCompanySKUs(companyID, 0);
            if (skuList != null)
            {
                ViewState["skulist"] = skuList;
                ddlSKU.DataSource = skuList;
                ddlSKU.DataValueField = "ItemcompanyGUID";
                ddlSKU.DataTextField = "SKU";


                ddlSKU.DataBind();
                ListItem item = new ListItem("", "");
                ddlSKU.Items.Insert(0, item);
            }
            else
            {
                ViewState["skulist"] = null;
                ddlSKU.DataSource = null;
                ddlSKU.DataBind();
                lblMsg.Text = "No SKU are assigned to selected Customer";

            }


        }
        private void Clear()
        {
            lblMsg.Text = string.Empty;
            txtComments.Text = string.Empty;
            ddlSKU.DataSource = null;
            ddlSKU.DataBind();
            ddlUser.DataSource = null;
            ddlUser.DataBind();        



        }
        protected void dpCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            int companyID = 0;

            lblMsg.Text = string.Empty;
            pnlESN.Visible = false;
            pnlUpload.Visible = true;
            rptESN.DataSource = null;
            rptESN.DataBind();

            //btnSubmit.Visible = false;
            Clear();

            if (dpCompany.SelectedIndex > 0)
                companyID = Convert.ToInt32(dpCompany.SelectedValue);
            if (companyID > 0)
            {
                BindCompanySKU(companyID);
                BindUsers(companyID);
            }
            else
            {
                ddlSKU.DataSource = null;
                ddlSKU.DataBind();
                ddlUser.DataSource = null;
                ddlUser.DataBind();

            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Session["esnData"] != null)
            {
                string esnData = Session["esnData"] as string;
                if (!string.IsNullOrEmpty(esnData))
                {
                    BlockEsnInfo request = new BlockEsnInfo();
                    request.Comment = txtComments.Text;
                    request.ESNData = esnData;
                    request.ItemCompanyGUID =  Convert.ToInt32(ddlSKU.SelectedValue);
                    request.ReceivedBy = Convert.ToInt32(ddlUser.SelectedValue);
                    request.UserID = Convert.ToInt32(Session["UserID"]);
                    string errorMesage = blockEsnOperation.ValidateRequiredFields(request);
                    if (string.IsNullOrEmpty(errorMesage))
                    {
                        int returnResult = blockEsnOperation.StockBlockInsert(request);
                        if (returnResult > 0)
                        {
                            lblMsg.Text = returnResult + " records  successfully inserted.";
                        }
                        else
                            lblMsg.Text = "Data not saved!";
                    }
                    else
                    {
                        lblMsg.Text = errorMesage;
                    }
                }
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            if(Session["adm"]!=null)
            {
                dpCompany.SelectedIndex = 0;
            }
            lblMsg.Text = string.Empty;
            rptESN.DataSource = null;
            rptESN.DataBind();
            ddlSKU.Items.Clear();
            ddlUser.Items.Clear();
            ddlSKU.DataSource = null;
            ddlSKU.DataBind();
            ddlUser.DataSource = null;
            ddlUser.DataBind();
            txtComments.Text = "";
            pnlESN.Visible = false;
            pnlUpload.Visible = true;

        }
        //private void UploadInventoryInfo()
        //{
        //    ESNAssignment esnAssignment = new ESNAssignment();
        //    //lblMsg.CssClass = "errormessage";
        //    lblConfirm.Text = string.Empty;
        //    string customerAccountNumber = string.Empty, trackingNumber = string.Empty;
        //    lblCount.Text = string.Empty;
        //    lblMsg.Text = string.Empty;
        //    Hashtable hshESN = new Hashtable();
        //    //bool esnExists = false;
        //    //bool esnIncorrectFormat = false;
        //    bool columnsIncorrectFormat = false;
        //    string poNum = txtPO.Text.Trim();
        //    string invalidColumns = string.Empty;
        //    bool IsValid = true;
        //    //bool uploadEsn = false;
        //    if (dpCompany.SelectedIndex > 0)
        //    {
        //        if (!string.IsNullOrEmpty(poNum))
        //        {
        //            customerAccountNumber = dpCompany.SelectedValue;
        //            esnAssignment.CustomerAccountNumber = customerAccountNumber;
        //            esnAssignment.FulfillmentNumber = poNum;
        //            try
        //            {
        //                //foreach (RepeaterItem item in rptUpload.Items)
        //                //{
        //                //    if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
        //                //    {
        //                //        FileUpload fu = (FileUpload)item.FindControl("fu");
        //                //        if (fu.HasFile)
        //                //        {
        //                //            string path = Server.MapPath("~/images/");
        //                //            string fileName = Path.GetFileName(fu.FileName);
        //                //            string fileExt = Path.GetExtension(fu.FileName).ToLower();
        //                //            fu.SaveAs(path + fileName + fileExt);
        //                //        }
        //                //    }
        //                //}
        //                //if (flnUpload.PostedFile.FileName.Trim().Length == 0)
        //                //{
        //                //    lblMsg.Text = "Select file to upload";
        //                //}
        //                //else
        //                //{

        //                //if (flnUpload.PostedFile.ContentLength > 0)
        //                //{
        //                FulfillmentAssignESN assignESN = null;
        //                List<FulfillmentAssignESN> esnList = new List<FulfillmentAssignESN>();
        //                string previoustrackingNumber = string.Empty;
        //                int trackingNumberCount = 0, fileCount = 0;
        //                if (ViewState["trackingNumberCount"] != null)
        //                    trackingNumberCount = Convert.ToInt32(ViewState["trackingNumberCount"]);
        //                bool IsMultiTrackingNumber = false;
        //                foreach (RepeaterItem item in rptUpload.Items)
        //                {
        //                    if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
        //                    {
        //                        FileUpload fu = (FileUpload)item.FindControl("fu");
        //                        DropDownList ddlTrackingNo = (DropDownList)item.FindControl("ddlTrackingNo");
        //                        if (ddlTrackingNo.SelectedIndex == 0)
        //                            ddlTrackingNo.Focus();
        //                        //{
        //                        //    lblMsg.Text = "Please select tracking number";
        //                        //    ddlTrackingNo.Focus();
        //                        //    return;
        //                        //}
        //                        if (ddlTrackingNo != null)
        //                            trackingNumber = ddlTrackingNo.SelectedValue;


        //                        if (trackingNumberCount > 1)
        //                        {
        //                            IsMultiTrackingNumber = true;
        //                            if (!string.IsNullOrWhiteSpace(previoustrackingNumber))
        //                                if (previoustrackingNumber != trackingNumber)
        //                                    IsMultiTrackingNumber = false;
        //                        }
        //                        previoustrackingNumber = trackingNumber;
        //                        if (fu.HasFile)
        //                        {
        //                            fileCount = 1;
        //                            string fileName = UploadFile(fu);
        //                            string extension = Path.GetExtension(fu.PostedFile.FileName);
        //                            extension = extension.ToLower();
        //                            if (string.IsNullOrWhiteSpace(trackingNumber))
        //                            {
        //                                lblMsg.Text = "please select tracking number";
        //                                // ddltrackingno.focus();
        //                                return;
        //                            }
        //                            if (extension == ".csv")
        //                            {
        //                                try
        //                                {
        //                                    using (StreamReader sr = new StreamReader(fileName))
        //                                    {
        //                                        string line;
        //                                        string esn, ICCID;//, fmupc, msl, lteICCID, lteIMSI, otksl, akey;
        //                                        int i = 0;
        //                                        while ((line = sr.ReadLine()) != null)
        //                                        {

        //                                            if (!string.IsNullOrEmpty(line) && i == 0)
        //                                            {
        //                                                i = 1;
        //                                                line = line.ToLower();
        //                                                string[] headerArray = line.Split(',');


        //                                                if (headerArray[0].Trim() != "esn")
        //                                                {
        //                                                    if (string.IsNullOrEmpty(invalidColumns))
        //                                                        invalidColumns = headerArray[0];
        //                                                    else
        //                                                        invalidColumns = invalidColumns + ", " + headerArray[0];
        //                                                    columnsIncorrectFormat = true;
        //                                                }
        //                                                if (headerArray.Length > 1)
        //                                                {
        //                                                    if (headerArray[1].Trim() != "iccid")
        //                                                    {
        //                                                        if (string.IsNullOrEmpty(invalidColumns))
        //                                                            invalidColumns = headerArray[1];
        //                                                        else
        //                                                            invalidColumns = invalidColumns + ", " + headerArray[1];
        //                                                        columnsIncorrectFormat = true;
        //                                                    }
        //                                                }

        //                                            }
        //                                            else if (!string.IsNullOrEmpty(line) && i > 0)
        //                                            {
        //                                                esn = ICCID = string.Empty;// fmupc = lteICCID = lteIMSI = otksl = akey = msl = string.Empty;
        //                                                string[] arr = line.Split(',');
        //                                                try
        //                                                {

        //                                                    if (arr.Length > 0)
        //                                                    {
        //                                                        esn = arr[0].Trim();
        //                                                        //customerAccountNumber = arr[1].Trim().Trim();
        //                                                        //sku = arr[2].Trim();
        //                                                        //esn = arr[1].Trim();
        //                                                    }
        //                                                    if (arr.Length > 1)
        //                                                    {
        //                                                        ICCID = arr[1].Trim();

        //                                                    }
        //                                                    //if(esn.Length < 8 || esn.Length < 18)
        //                                                    //{

        //                                                    //        lblMsg.Text = "ESN length should be between 8 and 18 required data";

        //                                                    //}
        //                                                    if (string.IsNullOrEmpty(esn))
        //                                                    {
        //                                                        lblMsg.Text = "Missing required data";
        //                                                        IsValid = false;
        //                                                    }

        //                                                    assignESN = new FulfillmentAssignESN();

        //                                                    if (hshESN.ContainsKey(esn) && !string.IsNullOrEmpty(esn))
        //                                                    {
        //                                                        //uploadEsn = false;
        //                                                        throw new ApplicationException("Duplicate ESN(s) exists in the file");
        //                                                    }
        //                                                    else if (!hshESN.ContainsKey(esn) && !string.IsNullOrEmpty(esn))
        //                                                    {
        //                                                        hshESN.Add(esn, esn);
        //                                                    }



        //                                                    //uploadEsn = true;
        //                                                    assignESN.FulfillmentNumber = poNum;
        //                                                    assignESN.CustomerAccountNumber = customerAccountNumber;
        //                                                    assignESN.SKU = string.Empty;
        //                                                    assignESN.ESN = esn;
        //                                                    assignESN.LteICCID = ICCID;
        //                                                    assignESN.TrackingNumber = trackingNumber;

        //                                                    esnList.Add(assignESN);
        //                                                    esn = string.Empty;
        //                                                    ICCID = string.Empty;
        //                                                }
        //                                                catch (ApplicationException ex)
        //                                                {
        //                                                    IsValid = false;
        //                                                    throw ex;
        //                                                }
        //                                                catch (Exception exception)
        //                                                {
        //                                                    IsValid = false;
        //                                                    lblMsg.Text = exception.Message;
        //                                                }
        //                                            }
        //                                        }

        //                                        sr.Close();
        //                                    }
        //                                }
        //                                catch (Exception ex)
        //                                {
        //                                    lblMsg.Text = ex.Message;
        //                                    IsValid = false;
        //                                }
        //                            }
        //                            else
        //                            {
        //                                lblMsg.Text = "Invalid file!";
        //                                IsValid = false;
        //                            }
        //                        }
        //                        //else

        //                    }
        //                }
        //                if (fileCount == 0)
        //                {
        //                    //if (esnList == null || (esnList != null && esnList.Count == 0))
        //                    {
        //                        lblMsg.Text = "Invalid file!";
        //                        IsValid = false;
        //                    }
        //                }
        //                if (IsMultiTrackingNumber)
        //                {
        //                    lblMsg.Text = "Multi Tracking numbers found but assigned only one!";
        //                    return;
        //                }
        //                if (esnList != null && esnList.Count > 0 && columnsIncorrectFormat == false && IsValid)
        //                {

        //                    rptESN.DataSource = esnList;
        //                    rptESN.DataBind();
        //                    lblCount.Text = "Total count: " + esnList.Count;
        //                    Session["esns"] = esnList;

        //                    int n = 0;
        //                    int poRecordCount = 0, Maxqty = 0, statusID = 0;
        //                    string poErrorMessage = string.Empty;
        //                    string invalidLTEESNMessage = string.Empty;
        //                    string invalidESNMessage = string.Empty;
        //                    string invalidSkuESNMessage = string.Empty;
        //                    string esnExistsMessage = string.Empty;
        //                    string kitESNMessage = string.Empty;
        //                    string errorMessage = string.Empty;
        //                    string poStatus = string.Empty;
        //                    List<FulfillmentAssignESN> esnList1 = new List<FulfillmentAssignESN>();
        //                    double totalChunk = 0;
        //                    try
        //                    {

        //                        totalChunk = (double)esnList.Count / 1000;
        //                        n = Convert.ToInt16(Math.Ceiling(totalChunk));
        //                        int esnCount = 1000;
        //                        //int skip = 1000;
        //                        int listLength = esnList.Count;
        //                        List<FulfillmentAssignESN> esnToUpload = null;
        //                        //var esnToUpload;
        //                        //for (int i = 0; i < n; i++)
        //                        for (int i = 0; i < listLength; i = i + 1000)
        //                        {

        //                            esnToUpload = new List<FulfillmentAssignESN>();
        //                            esnAssignment.EsnList = new List<FulfillmentAssignESN>();
        //                            invalidLTEESNMessage = invalidESNMessage = invalidSkuESNMessage = esnExistsMessage = kitESNMessage = string.Empty;
        //                            //esnToAdd = new List<FulfillmentAssignESN>();
        //                            //if (esnList.Count < 1000)
        //                            //{   esnCount = esnList.Count;
        //                            //    skip = esnList.Count;
        //                            //    if (i > 0)
        //                            //        skip = skip + 1000;
        //                            //}
        //                            esnToUpload = (from item in esnList.Skip(i).Take(esnCount) select item).ToList();

        //                            esnAssignment.EsnList = esnToUpload;
        //                            //Upload/Assign ESN to POs
        //                            int returnValue = 0;
        //                            string AlreadyInUseICCIDMessase = string.Empty, ICCIDNotExistsMessase = string.Empty, InvalidICCIDMessase = string.Empty, AlreadyMappedESNMessase = string.Empty;

        //                            List<FulfillmentAssignESN> esnList2 = avii.Classes.ESNAssignOperation.ValidateAssignESN_New(esnAssignment, out poRecordCount, out invalidLTEESNMessage, out invalidESNMessage, out invalidSkuESNMessage, out esnExistsMessage, out kitESNMessage, out returnValue, out poStatus, out statusID, out Maxqty);
        //                            esnList1.AddRange(esnList2);

        //                            if (returnValue == -5)
        //                            { errorMessage = "Fulfillment number does not exists!"; }

        //                            //if (!string.IsNullOrEmpty(invalidLTEESNMessage))
        //                            //{
        //                            //    if (string.IsNullOrEmpty(errorMessage))
        //                            //        errorMessage = invalidLTEESNMessage + " ESN(s) LTE data missing!";
        //                            //    else
        //                            //        errorMessage = errorMessage + " <br /> " + invalidLTEESNMessage + " ESN(s) LTE data missing!";
        //                            //}

        //                            if (statusID == 1 || statusID == 10 || statusID == 11)
        //                            {
        //                                if (Maxqty == 0)
        //                                {
        //                                    if (string.IsNullOrEmpty(errorMessage))
        //                                    {

        //                                        errorMessage = "Provisioning is already done for this Fulfillment number";
        //                                    }
        //                                    else
        //                                        errorMessage = errorMessage + " <br /> " + " Provisioning is already done for this Fulfillment number";
        //                                }
        //                            }
        //                            else
        //                            {
        //                                //if (poStatus.ToLower() == "processed" || poStatus.ToLower() == "shipped")
        //                                {
        //                                    if (string.IsNullOrEmpty(errorMessage))
        //                                    {

        //                                        errorMessage = "Fulfillment number already " + poStatus;

        //                                    }
        //                                    else
        //                                        errorMessage = errorMessage + " <br /> " + " Fulfillment number already " + poStatus;
        //                                }

        //                            }
        //                            if (!string.IsNullOrEmpty(invalidESNMessage))
        //                            {
        //                                //errorMessage = invalidESNMessage + " ESN(s) does not exists!";
        //                                if (string.IsNullOrEmpty(errorMessage))
        //                                    errorMessage = invalidESNMessage + " ESN(s) does not exists!";
        //                                else
        //                                    errorMessage = errorMessage + " <br /> " + invalidESNMessage + " ESN(s) does not exists!";

        //                            }
        //                            if (!string.IsNullOrEmpty(invalidSkuESNMessage))
        //                            {
        //                                if (string.IsNullOrEmpty(errorMessage))
        //                                    errorMessage = invalidSkuESNMessage + " ESN(s) are not assigned to right Product/SKU!";
        //                                else
        //                                    errorMessage = errorMessage + " <br /> " + invalidSkuESNMessage + " ESN(s) are not assigned to right Product/SKU!";

        //                            }
        //                            if (!string.IsNullOrEmpty(esnExistsMessage))
        //                            {
        //                                if (string.IsNullOrEmpty(errorMessage))
        //                                    errorMessage = esnExistsMessage + " ESN(s) are already assigned! <br /> Please unassign first and try again!";
        //                                else
        //                                    errorMessage = errorMessage + " <br /> " + esnExistsMessage + " ESN(s) are already assigned! <br /> Please unassign first and try again!";

        //                            }
        //                            if (!string.IsNullOrEmpty(kitESNMessage))
        //                            {
        //                                if (string.IsNullOrEmpty(errorMessage))
        //                                    errorMessage = kitESNMessage + " ESN(s) are not valit KIT!";
        //                                else
        //                                    errorMessage = errorMessage + " <br /> " + kitESNMessage + " ESN(s) are not valit KIT!";

        //                            }
        //                        }
        //                        if (esnList1 != null && esnList1.Count > 0)
        //                        {
        //                            rptESN.DataSource = esnList1;
        //                            rptESN.DataBind();
        //                        }
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        lblMsg.Text = ex.Message;
        //                    }
        //                    if (!string.IsNullOrEmpty(errorMessage))
        //                    {
        //                        lblMsg.Text = errorMessage;
        //                        return;
        //                    }
        //                    if (lblMsg.Text == string.Empty)
        //                    {
        //                        //lblMsg.CssClass = "errorGreenMsg";
        //                        lblConfirm.Text = "ESN file is ready to upload";
        //                        btnUpload.Visible = false;
        //                        lnkDownload.Visible = false;
        //                        btnSubmit.Visible = true;
        //                        btnSubmit2.Visible = true;
        //                        pnlSubmit.Visible = true;
        //                        row1.Visible = false;
        //                        row2.Visible = false;
        //                        //btnSearch.Visible = false;

        //                    }
        //                    else
        //                    {
        //                        btnUpload.Visible = true;
        //                        lnkDownload.Visible = true;
        //                        btnSubmit.Visible = false;
        //                        row1.Visible = true;
        //                        row2.Visible = true;
        //                        btnSubmit2.Visible = false;
        //                        pnlSubmit.Visible = false;

        //                    }

        //                }
        //                else
        //                {
        //                    rptESN.DataSource = null;
        //                    rptESN.DataBind();

        //                    if (esnList != null && esnList.Count == 0)
        //                    {
        //                        if (columnsIncorrectFormat)
        //                        {
        //                            lblMsg.Text = invalidColumns + " column(s) name is not correct";
        //                        }
        //                        else
        //                            lblMsg.Text = "There is no ESN to upload";

        //                    }
        //                    if (esnList != null)
        //                    {
        //                        if (columnsIncorrectFormat)
        //                        {
        //                            lblMsg.Text = invalidColumns + " column(s) name is not correct";
        //                        }
        //                        else
        //                            lblMsg.Text = "There is no ESN to upload";
        //                    }
        //                }
        //                //    }
        //                //    else
        //                //        lblMsg.Text = "Invalid file!";
        //                //}
        //                //else
        //                //    lblMsg.Text = "Invalid file!";
        //                //}
        //                //}
        //            }
        //            catch (Exception ex)
        //            {
        //                lblMsg.Text = ex.Message;
        //            }
        //        }
        //        else
        //            lblMsg.Text = "Please enter Fulfillment number!";

        //    }
        //    else
        //        lblMsg.Text = "Please select customer!";
        //}


        protected void btnValidate_Click(object sender, EventArgs e)
        {
            ValidateESNs();
        }
        private void ValidateESNs()
        {
            lblMsg.Text = string.Empty;

            rptESN.DataSource = null;
            rptESN.DataBind();
            pnlUpload.Visible = true;
            pnlESN.Visible = false;
            btnSubmit.Visible = false;
            btnCancel.Visible = false;


            Hashtable hshESN = new Hashtable();
            int companyID = 0, itemCompanyGUID = 0;
            System.Text.StringBuilder sbESNs = new System.Text.StringBuilder();
            System.Text.StringBuilder sbErrors = new System.Text.StringBuilder();

            try
            {
                if (Session["adm"] != null)
                {
                    if (dpCompany.SelectedIndex == 0)
                    {
                        lblMsg.Text = "Customer is required!";
                        return;
                    }
                    else
                        companyID = Convert.ToInt32(dpCompany.SelectedValue);
                }
                else
                    companyID = Convert.ToInt32(ViewState["companyID"]);

                if (ddlSKU.SelectedIndex > 0)
                {
                    itemCompanyGUID = Convert.ToInt32(ddlSKU.SelectedValue);

                    if (flnUpload.PostedFile.FileName.Trim().Length == 0)
                    {
                        lblMsg.Text = "Select file to upload";
                    }
                    else
                    {
                        if (flnUpload.PostedFile.ContentLength > 0)
                        {
                            string fileName = UploadFile(flnUpload);
                            string extension = Path.GetExtension(flnUpload.PostedFile.FileName);
                            extension = extension.ToLower();
                            if (extension == ".csv")
                            {
                                using (StreamReader sr = new StreamReader(fileName))
                                {
                                    string line;
                                    string esn;
                                    int i = 0;
                                    while ((line = sr.ReadLine()) != null)
                                    {
                                        if (!string.IsNullOrEmpty(line) && i == 0)
                                        {
                                            i = 1;
                                            line = line.ToLower();

                                            string[] headerArray = line.Split(',');
                                            if (headerArray.Length == 1 || headerArray.Length <= 1)
                                            {
                                                if (headerArray[0].Trim() != "esn")
                                                {
                                                    lblMsg.Text = "Column name is not correct";
                                                    return;
                                                }
                                            }
                                        }
                                        else if (!string.IsNullOrEmpty(line) && i > 0)
                                        {
                                            esn = string.Empty;
                                            string[] arr = line.Split(',');
                                            esn = arr[0].Trim();

                                            if (string.IsNullOrEmpty(esn))
                                            {
                                            }
                                            else
                                            {
                                                sbESNs.Append(esn + ",");
                                            }
                                            if (hshESN.ContainsKey(esn) && !string.IsNullOrEmpty(esn))
                                            {
                                                sbErrors.Append(esn + ",");
                                                lblMsg.Text = sbErrors.ToString() + " duplicate ESN(s) exists in the file";
                                            }
                                            else if (!hshESN.ContainsKey(esn) && !string.IsNullOrEmpty(esn))
                                            {
                                                hshESN.Add(esn, esn);
                                            }
                                            esn = string.Empty;
                                        }
                                    }
                                    sr.Close();
                                    if (lblMsg.Text == "")
                                    {
                                        string esnData = sbESNs.ToString();
                                        esnData = esnData.Substring(0, esnData.Length - 1);
                                        if (!string.IsNullOrEmpty(esnData) && esnData.Length > 0)
                                        {
                                            //if (esnData.Length <= 50)
                                            {
                                                List<BlockEsn> esnList = blockEsnOperation.BlockEsnValidate(itemCompanyGUID, esnData);
                                                if (esnList != null && esnList.Count > 0)
                                                {
                                                    rptESN.DataSource = esnList;
                                                    rptESN.DataBind();
                                                    //foreach()
                                                    var esnError = (from item in esnList where (item.ErrorMessage != "") select item).ToList();
                                                    if (esnError != null && esnError.Count > 0)
                                                    {
                                                        lblMsg.Text = "Invalid file data";
                                                        pnlESN.Visible = true;
                                                        btnSubmit.Visible = false;
                                                        btnCancel.Visible = false;
                                                        // return;
                                                    }
                                                    else
                                                    {
                                                        pnlUpload.Visible = false;
                                                        pnlESN.Visible = true;
                                                        btnSubmit.Visible = true;
                                                        btnCancel.Visible = true;

                                                        Session["esnData"] = esnData;

                                                    }

                                                }
                                                else
                                                {
                                                    pnlUpload.Visible = true;
                                                    pnlESN.Visible = false;

                                                    rptESN.DataSource = null;
                                                    rptESN.DataBind();
                                                    lblMsg.Text = "No data found!";
                                                }
                                            }
                                            //else
                                            {
                                              //  rptESN.DataSource = null;
                                                //rptESN.DataBind();
                                                //lblMsg.Text = "Only 50 ESNs are allowed at one time!";
                                            }
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
                else
                {
                    lblMsg.Text = "SKU is required!";
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        private string UploadFile(FileUpload fu)
        {
            string fileStoreLocation;
            string actualFilename = string.Empty;
            Int32 maxFileSize = 1572864;
            actualFilename = System.IO.Path.GetFileName(fu.PostedFile.FileName);
            if (ConfigurationManager.AppSettings["PurchaseOrderFilesStoreage"] != null)
            {
                fileStoreLocation = ConfigurationManager.AppSettings["PurchaseOrderFilesStoreage"].ToString();
            }

            fileStoreLocation = Server.MapPath("~/UploadedData/");
            if (File.Exists(fileStoreLocation + actualFilename))
            {
                actualFilename = System.Guid.NewGuid().ToString() + actualFilename;
            }

            fu.PostedFile.SaveAs(fileStoreLocation + actualFilename);

            FileInfo fileInfo = new FileInfo(fileStoreLocation + actualFilename);

            if (ConfigurationManager.AppSettings["maxCSVfilesize"] != null)
            {
                if (Int32.TryParse(ConfigurationManager.AppSettings["maxCSVfilesize"].ToString(), out maxFileSize))
                {
                    if (fileInfo.Length > maxFileSize)
                    {
                        fileInfo.Delete();
                        throw new Exception("File size is greater than " + maxFileSize + " bytes");
                    }
                }
            }



            return fileStoreLocation + actualFilename;
        }

    }
}