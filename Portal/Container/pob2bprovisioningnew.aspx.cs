using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using avii.Classes;
using SV.Framework.Fulfillment;
using SV.Framework.Models.Fulfillment;

namespace avii.Container
{
    public partial class pob2bprovisioningnew : System.Web.UI.Page
    {
        private string fileStoreLocation = "~/UploadedData/";

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
                BindCustomers();
        }

        protected void BindCustomers()
        {
            dpCompany.DataSource = avii.Classes.clsCompany.GetCompany(0, 1);
            dpCompany.DataValueField = "CompanyID";
            dpCompany.DataTextField = "CompanyName";
            dpCompany.DataBind();

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindPO();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "stop loader", "StopProgress();", true);
        }
        private void BindPO()
        {
            SV.Framework.Fulfillment.ContainerOperation containerOperation = SV.Framework.Fulfillment.ContainerOperation.CreateInstance<SV.Framework.Fulfillment.ContainerOperation>();

            lblMsg.Text = string.Empty;
            lblNonEsn.Text = string.Empty;
            lblEsn.Text = string.Empty;
            rptSKU.Visible = false;
            trUpload.Visible = false;
            int companyID = 0;
            if (dpCompany.SelectedIndex > 0)
            {
                int.TryParse(dpCompany.SelectedValue, out companyID);
                int numberOfContainers = 0, poid = 0, casePackQuantity = 0, quantity = 0;
                string fulfillmentNumber = txtPoNum.Text.Trim();
                string trackingNumber;
                trackingNumber = txtTrackingNo.Text.Trim();

                if (string.IsNullOrEmpty(fulfillmentNumber) && string.IsNullOrEmpty(trackingNumber))
                {
                    lblMsg.Text = "Please select fulfillment number or tracking number";
                    return;
                }

                List<POTracking> trackingList = default;

                List<FulfillmentContainer> containers = default;
                List<FulfillmentAssignNonESN> nonESNList = default;
                List<ContainerInfo> skuList = containerOperation.GetContainerInfo(companyID, fulfillmentNumber, trackingNumber, out containers, out nonESNList, out trackingList);
                if (skuList != null && skuList.Count > 0)
                {
                    if (trackingList != null && trackingList.Count > 0)
                    {
                        ViewState["trackingNumberCount"] = trackingList.Count;
                        Session["trackingList"] = trackingList;
                        rptUpload.DataSource = trackingList;
                        rptUpload.DataBind();
                        trUpload.Visible = true;
                    }
                    else
                    {
                        //ViewState["trackingNumberCount"] = trackingList.Count;
                        //Session["trackingList"] = trackingList;
                        //POTracking tracking = new POTracking();
                        //tracking.TrackingNumber = string.Empty;
                        //trackingList.Add(tracking);
                        rptUpload.DataSource = null;
                        rptUpload.DataBind();
                        lblMsg.Text = "Please generate shipping label";
                        return;
                    }
                    Session["poskulist"] = skuList;
                    gvPOSKUs.DataSource = skuList;
                    gvPOSKUs.DataBind();
                    lblEsn.Text = "ESN Items";
                    //btnSubmit.Visible = true;
                    btnCancel1.Visible = true;
                    btnGenContainerID.Visible = true;


                    foreach (ContainerInfo item in skuList)
                    {
                        quantity += item.PoQuantity;
                        numberOfContainers += item.ContainerRequired;
                        poid = item.POID;
                        casePackQuantity = item.ContainerQuantity;

                    }
                    ViewState["casePackQuantity"] = casePackQuantity;
                    if (containers != null && containers.Count > 0)
                    {
                        rptContainers.DataSource = containers;
                        rptContainers.DataBind();
                        Session["containerList"] = containers;
                        // btnSubmit.Visible = false;
                        // btnGenContainerID.Visible = false;

                    }
                    if (nonESNList != null && nonESNList.Count > 0)
                    {
                        lblNonEsn.Text = "Non ESN Items";
                        rptSKU.DataSource = nonESNList;
                        rptSKU.DataBind();
                        rptSKU.Visible = true;
                        //Session["containerList"] = containers;
                        // btnSubmit.Visible = false;
                        // btnGenContainerID.Visible = false;

                    }
                    else
                    {
                        rptSKU.DataSource = null;
                        rptSKU.DataBind();

                    }
                    // txtContainers.Text = numberOfContainers.ToString();
                    ViewState["quantity"] = quantity;
                    ViewState["poid"] = poid;

                    rptUpload.Visible = true;
                    trUpload.Visible = true;
                    //trFormat.Visible = true;
                    //trUpload.Visible = true;

                    btnUpload.Visible = true;
                    lnkDownload.Visible = true;

                    var Errors = (from item in skuList where (item.ErrorMessage != "") select item).ToList();
                    if (Errors != null && Errors.Count > 0)
                    {
                        btnUpload.Visible = false;
                        lnkDownload.Visible = false;
                    }
                    if (nonESNList != null && nonESNList.Count > 0)
                    {
                        var nonEsnErrors = (from item in nonESNList where (item.ErrorMessage != "") select item).ToList();
                        if (nonEsnErrors != null && nonEsnErrors.Count > 0)
                        {
                            btnUpload.Visible = false;
                            lnkDownload.Visible = false;
                        }
                    }
                    //}

                }
                else
                {
                    gvPOSKUs.DataSource = null;
                    gvPOSKUs.DataBind();
                    lblMsg.Text = "No record found";
                }
            }
            else
            {
                gvPOSKUs.DataSource = null;
                gvPOSKUs.DataBind();
                lblMsg.Text = "Please select customer";
            }

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            lblNonEsn.Text = string.Empty;
            lblEsn.Text = string.Empty;
            lblMsg.Text = string.Empty;
            txtPoNum.Text = string.Empty;
            txtTrackingNo.Text = string.Empty;
            gvPOSKUs.DataSource = null;
            gvPOSKUs.DataBind();
            rptESN.DataSource = null;
            rptESN.DataBind();
            rptSKU.DataSource = null;
            rptSKU.DataBind();
            btnSubmit.Enabled = true;

            btnSubmit.Visible = false;
            btnGenContainerID.Visible = false;
            btnCancel1.Visible = false;
            dpCompany.SelectedIndex = 0;
            rptUpload.DataSource = null;
            rptUpload.DataBind();
            trUpload.Visible = false;
            rptUpload.Visible = false;
            // trFormat.Visible = false;
            //trUpload.Visible = false;

            btnUpload.Visible = false;
            lnkDownload.Visible = false;
            // txtContainers.Visible = false;

        }
        protected void btnCancel1_Click(object sender, EventArgs e)
        {
            //lblNonEsn.Text = string.Empty;
            lblMsg.Text = string.Empty;
            // txtPoNum.Text = string.Empty;
            // txtTrackingNo.Text = string.Empty;
            //  gvPOSKUs.DataSource = null;
            // gvPOSKUs.DataBind();
            rptESN.DataSource = null;
            rptESN.DataBind();
            btnSubmit.Enabled = true;
            //rptSKU.DataSource = null;
            //rptSKU.DataBind();

            btnSubmit.Visible = false;
            //  btnGenContainerID.Visible = false;
            // btnCancel1.Visible = false;
            // dpCompany.SelectedIndex = 0;
            // trFormat.Visible = false;
            // trUpload.Visible = false;

            // lblReqContainer.Visible = false;
            // txtContainers.Visible = false;

        }

        protected void btnGenContainerID_Click(object sender, EventArgs e)
        {
            int numberOfContainers = 0, poid = 0;
            lblMsg.Text = string.Empty;

            //if (ViewState["numberofcontainers"] != null)
            //    numberOfContainers = Convert.ToInt32(ViewState["numberofcontainers"]);
            //if (txtContainers.Text != string.Empty)
            //    numberOfContainers = Convert.ToInt32(txtContainers.Text);

            //if (numberOfContainers > 0)
            //{
            //    List<FulfillmentContainer> containerList = ContainerOperation.GenerateContainerIDs(numberOfContainers, poid);
            //    if (containerList != null)
            //    {
            //        rptContainers.DataSource = containerList;
            //        rptContainers.DataBind();
            //        Session["containerList"] = containerList;

            //    }
            //    else
            //    {
            //        rptContainers.DataSource = null;
            //        rptContainers.DataBind();
            //        Session["containerList"] = null;
            //    }
            //}
            //else
            //{
            //    lblMsg.Text = "";
            //    rptContainers.DataSource = null;
            //    rptContainers.DataBind();
            //    Session["containerList"] = null;
            //}
            RegisterStartupScript("jsUnblockDialog", "unblockDialog();");
        }
        private void RegisterStartupScript(string key, string script)
        {
            ScriptManager.RegisterStartupScript(phrJsRunner, phrJsRunner.GetType(), key, script, true);
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            SV.Framework.Fulfillment.ContainerProvisioningOperation containerProvisioningOperation = SV.Framework.Fulfillment.ContainerProvisioningOperation.CreateInstance<SV.Framework.Fulfillment.ContainerProvisioningOperation>();

            int returnRecord = 0;
            int userID = 0, poid=0;
            string fulfillmentNumber = string.Empty, returnMessage = string.Empty;
            int companyID = 0;
            if (ViewState["poid"] != null)
            {
                poid = Convert.ToInt32(ViewState["poid"]);
            }
            //   
            fulfillmentNumber = txtPoNum.Text.Trim();
            companyID = Convert.ToInt32(dpCompany.SelectedValue);

           avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
            if (userInfo != null)
            {
                userID = userInfo.UserGUID;
            }

            lblMsg.Text = string.Empty;

            List<ContainerESNInfo> containerEsnList = null;
            if (Session["containerEsnList"] != null)
            {
                containerEsnList = Session["containerEsnList"] as List<ContainerESNInfo>;

                returnRecord = containerProvisioningOperation.PurchaseOrderContainerESNAssign2(companyID, fulfillmentNumber, userID, containerEsnList, poid);
                if (returnRecord > 0)
                {
                    lblMsg.Text = returnRecord + " record(s) submitted successfully";
                    btnSubmit.Enabled = false;
                    Session["containerEsnList"] = null;
                }
                else
                    lblMsg.Text = "Data not saved";
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "stop loader", "StopProgress();", true);

        }
        protected void rptUpload_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                List<POTracking> trackingList = new List<POTracking>();
                trackingList = Session["trackingList"] as List<POTracking>;
                DropDownList ddlTrackingNo = (DropDownList)e.Item.FindControl("ddlTrackingNo");
                if (ddlTrackingNo != null)
                {
                    ddlTrackingNo.DataSource = trackingList;
                    ddlTrackingNo.DataTextField = "TrackingNumber";
                    ddlTrackingNo.DataValueField = "TrackingNumber";
                    ddlTrackingNo.DataBind();
                    System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem("", "");
                    ddlTrackingNo.Items.Insert(0, item);
                }
            }
        }
        protected void btnValidateUploadedFile_Click(object sender, EventArgs e)
        {
            UploadESNInfo();
        }
        private void UploadESNInfo()
        {
            rptESN.DataSource = null;
            rptESN.DataBind();
            SV.Framework.Fulfillment.ContainerProvisioningOperation containerProvisioningOperation = SV.Framework.Fulfillment.ContainerProvisioningOperation.CreateInstance<SV.Framework.Fulfillment.ContainerProvisioningOperation>();

            ContainerESN esnAssignment = new ContainerESN();
            int poQuantity = 0;
            List<ContainerESN> esnList = new List<ContainerESN>();
            List<ContainerInfo> skuList = new List<ContainerInfo>();
            if (Session["poskulist"] != null)
                skuList = Session["poskulist"] as List<ContainerInfo>;
            if (ViewState["quantity"] != null)
                poQuantity = Convert.ToInt32(ViewState["quantity"]);

            //lblMsg.CssClass = "errormessage";
            int companyID = 0, fileCount = 0;
            //     lblCount.Text = string.Empty;
            lblMsg.Text = string.Empty;
            Hashtable hshESN = new Hashtable();
            //bool esnExists = false;
            //bool esnIncorrectFormat = false;
            bool columnsIncorrectFormat = false;
            string poNum = txtPoNum.Text.Trim();
            bool IsValid = true;
            string previoustrackingNumber = string.Empty, trackingNumber = string.Empty;
            int trackingNumberCount = 0;
            if (ViewState["trackingNumberCount"] != null)
                trackingNumberCount = Convert.ToInt32(ViewState["trackingNumberCount"]);
            bool IsMultiTrackingNumber = false;
            string invalidColumns = string.Empty;
            //FulfillmentAssignESN assignESN = null;
            //List<FulfillmentAssignESN> esnList = new List<FulfillmentAssignESN>();
            string duplicateESNs = string.Empty, duplicateICCIDs = string.Empty;

            //bool uploadEsn = false;
            if (dpCompany.SelectedIndex > 0)
            {
                if (!string.IsNullOrEmpty(poNum))
                {
                    companyID = Convert.ToInt32(dpCompany.SelectedValue);
                    //esnAssignment.CustomerAccountNumber = customerAccountNumber;
                    //esnAssignment.FulfillmentNumber = poNum;
                    try
                    {
                        //if (flnUpload.PostedFile.FileName.Trim().Length == 0)
                        //{
                        //    lblMsg.Text = "Select file to upload";
                        //}

                        foreach (RepeaterItem item in rptUpload.Items)
                        {
                            if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                            {
                                FileUpload fu = (FileUpload)item.FindControl("fu");
                                DropDownList ddlTrackingNo = (DropDownList)item.FindControl("ddlTrackingNo");
                                if (ddlTrackingNo != null)
                                    trackingNumber = ddlTrackingNo.SelectedValue;

                                if (fu.HasFile)
                                {
                                    fileCount = 1;
                                    string fileName = UploadFile(fu);
                                    string extension = Path.GetExtension(fu.PostedFile.FileName);
                                    extension = extension.ToLower();
                                    if (extension == ".csv")
                                    {
                                        try
                                        {

                                            if (trackingNumberCount > 1)
                                            {
                                                IsMultiTrackingNumber = true;
                                                if (!string.IsNullOrWhiteSpace(previoustrackingNumber))
                                                    if (previoustrackingNumber != trackingNumber)
                                                        IsMultiTrackingNumber = false;
                                            }
                                            previoustrackingNumber = trackingNumber;
                                            using (StreamReader sr = new StreamReader(fileName))
                                            {
                                                string line;
                                                string esn, containerID, ICCID, Location;//, fmupc, msl, lteICCID, lteIMSI, otksl, akey;
                                                int i = 0;
                                                int ctr = 1;
                                                while ((line = sr.ReadLine()) != null)
                                                {

                                                    if (!string.IsNullOrEmpty(line) && i == 0)
                                                    {
                                                        i = 1;
                                                        line = line.ToLower();
                                                        string[] headerArray = line.Split(',');


                                                        if (headerArray[0].Trim() != "containerid")
                                                        {
                                                            if (string.IsNullOrEmpty(invalidColumns))
                                                                invalidColumns = headerArray[0];
                                                            else
                                                                invalidColumns = invalidColumns + ", " + headerArray[0];
                                                            columnsIncorrectFormat = true;
                                                        }
                                                        if (headerArray[1].Trim() != "esn")
                                                        {
                                                            if (string.IsNullOrEmpty(invalidColumns))
                                                                invalidColumns = headerArray[1];
                                                            else
                                                                invalidColumns = invalidColumns + ", " + headerArray[1];
                                                            columnsIncorrectFormat = true;
                                                        }
                                                        if (headerArray.Length > 2 && !string.IsNullOrEmpty(headerArray[2].Trim()))
                                                        {
                                                            if (headerArray[2].Trim() != "location")
                                                            {
                                                                if (string.IsNullOrEmpty(invalidColumns))
                                                                    invalidColumns = headerArray[2];
                                                                else
                                                                    invalidColumns = invalidColumns + ", " + headerArray[2];
                                                                columnsIncorrectFormat = true;
                                                            }
                                                        }
                                                        //if (headerArray.Length > 2 && !string.IsNullOrEmpty(headerArray[2].Trim()))
                                                        //{
                                                        //    if (headerArray[2].Trim() != "iccid")
                                                        //    {
                                                        //        if (string.IsNullOrEmpty(invalidColumns))
                                                        //            invalidColumns = headerArray[2];
                                                        //        else
                                                        //            invalidColumns = invalidColumns + ", " + headerArray[2];
                                                        //        columnsIncorrectFormat = true;
                                                        //    }
                                                        //}
                                                    }
                                                    else if (!string.IsNullOrEmpty(line) && i > 0)
                                                    {

                                                        esn = containerID = Location = string.Empty;// fmupc = lteICCID = lteIMSI = otksl = akey = msl = string.Empty;
                                                        string[] arr = line.Split(',');
                                                        try
                                                        {

                                                            if (arr.Length > 0)
                                                            {
                                                                containerID = arr[0].Trim();
                                                                esn = arr[1].Trim();

                                                                if (arr.Length > 2)
                                                                    Location = arr[2].Trim();
                                                                //customerAccountNumber = arr[1].Trim().Trim();
                                                                //sku = arr[2].Trim();
                                                                //esn = arr[1].Trim();
                                                            }
                                                            if (string.IsNullOrEmpty(esn) && string.IsNullOrEmpty(containerID))
                                                            {

                                                            }
                                                            else
                                                            {
                                                                //if(esn.Length < 8 || esn.Length < 18)
                                                                //{

                                                                //        lblMsg.Text = "ESN length should be between 8 and 18 required data";

                                                                //}
                                                                if (string.IsNullOrEmpty(containerID) || string.IsNullOrEmpty(esn))
                                                                {
                                                                    lblMsg.Text = "Missing required data";
                                                                }
                                                                esnAssignment = new ContainerESN();

                                                                if (hshESN.ContainsKey(esn) && !string.IsNullOrEmpty(esn))
                                                                {
                                                                    if (string.IsNullOrEmpty(duplicateESNs))
                                                                        duplicateESNs = esn;
                                                                    else
                                                                        duplicateESNs = duplicateESNs + ", " + esn;
                                                                    //uploadEsn = false;
                                                                    // lblMsg.Text = duplicateESNs + " Duplicate ESN(s) exists in the file";
                                                                    // throw new ApplicationException("Duplicate ESN(s) exists in the file");
                                                                }
                                                                else if (!hshESN.ContainsKey(esn) && !string.IsNullOrEmpty(esn))
                                                                {
                                                                    hshESN.Add(esn, esn);
                                                                }

                                                                //if (hshESN.ContainsKey(Location) && !string.IsNullOrEmpty(Location))
                                                                //{
                                                                //    if (string.IsNullOrEmpty(duplicateICCIDs))
                                                                //        duplicateICCIDs = Location;
                                                                //    else
                                                                //        duplicateICCIDs = duplicateICCIDs + ", " + Location;
                                                                //}

                                                                //if (hshESN.ContainsKey(Location) && !string.IsNullOrEmpty(Location))
                                                                //{
                                                                //    if (string.IsNullOrEmpty(duplicateICCIDs))
                                                                //        duplicateICCIDs = Location;
                                                                //    else
                                                                //        duplicateICCIDs = duplicateICCIDs + ", " + Location;

                                                                //}
                                                                //else if (!hshESN.ContainsKey(Location) && !string.IsNullOrEmpty(ICCID))
                                                                //{
                                                                //    hshESN.Add(ICCID, ICCID);
                                                                //}
                                                                //uploadEsn = true;
                                                                esnAssignment.ContainerID = containerID;
                                                                esnAssignment.ESN = esn;
                                                                esnAssignment.ICCID = "";
                                                                esnAssignment.Location = Location;
                                                                esnAssignment.TrackingNumber = trackingNumber;
                                                                esnAssignment.SNo = ctr;
                                                                esnList.Add(esnAssignment);
                                                                ctr = ctr + 1;
                                                            }
                                                            esn = containerID = Location = string.Empty;
                                                        }
                                                        catch (ApplicationException ex)
                                                        {
                                                            throw ex;
                                                        }
                                                        catch (Exception exception)
                                                        {
                                                            lblMsg.Text = exception.Message;
                                                        }
                                                        if (!string.IsNullOrEmpty(duplicateESNs))
                                                        {
                                                            lblMsg.Text = duplicateESNs + " duplicate ESN(s) exists in the file";
                                                            return;
                                                        }
                                                        if (!string.IsNullOrEmpty(duplicateICCIDs))
                                                        {
                                                            lblMsg.Text = duplicateICCIDs + " duplicate ICCID(s) exists in the file";
                                                            return;
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
                                    }
                                    else
                                        lblMsg.Text = "Invalid file!";
                                }
                                //else

                            }
                        }
                        if (fileCount == 0)
                        {

                            lblMsg.Text = "Invalid file!";
                            return;
                        }
                        if (esnList != null && esnList.Count > 0 && columnsIncorrectFormat == false)
                        {
                            // Session["esns"] = esnList;

                            // int n = 0;
                            // int poRecordCount = 0;
                            string poErrorMessage = string.Empty;
                            string invalidLTEESNMessage = string.Empty;
                            string invalidESNMessage = string.Empty;
                            string invalidSkuESNMessage = string.Empty;
                            string esnExistsMessage = string.Empty;
                            string containerMessage = string.Empty;
                            string errorMessage = string.Empty;
                            System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
                            int skuQty = 0;
                            //int ItemCompanyGUID = 0, qty = 0, containerQuantity=0;
                            List<FulfillmentContainer> containers = new List<FulfillmentContainer>();
                            //List<FulfillmentAssignESN> esnList1 = new List<FulfillmentAssignESN>();
                            //double totalChunk = 0;
                            try
                            {
                                List<ContainerESNInfo> containerEsnList = containerProvisioningOperation.GetContainerESNsNew2(companyID, poNum, esnList);
                                if (containerEsnList != null && containerEsnList.Count > 0)
                                {
                                    //string esnXML = clsGeneral.SerializeObject(containerEsnList);

                                    Session["containerEsnList"] = containerEsnList;
                                    rptESN.DataSource = containerEsnList;
                                    rptESN.DataBind();
                                    //        
                                    //var esnss = containerEsnList.GroupBy(e => new { e.ItemCompanyGUID, e.ContainerID });


                                    var esnError = (from item in containerEsnList where (item.ErrorMessage != "") select item).ToList();
                                    if (esnError != null && esnError.Count > 0)
                                    {
                                        lblMsg.Text = esnError[0].ErrorMessage;// "Invalid file data";
                                        rptESN.DataSource = containerEsnList;
                                        rptESN.DataBind();
                                        return;
                                    }
                                    var locationError = (from item in containerEsnList where (item.LocationMessage != "") select item).ToList();
                                    if(locationError != null && locationError.Count > 0)
                                    {
                                        lblMsg.Text = "Location(s) not matched, Do you want to overwrite? Please continue";
                                        //return;
                                    }
                                    if (Session["containerList"] != null)
                                    {
                                        containers = Session["containerList"] as List<FulfillmentContainer>;
                                        foreach (FulfillmentContainer poitem in containers)
                                        {

                                            var containerList = (from item in containerEsnList where (item.ContainerID == poitem.ContainerID) select item).ToList();
                                            if (containerList != null && containerList.Count > 0)
                                            {
                                                //var esnCount = (from item in containerEsnList.Where(x => x.ContainerID == poitem.ContainerID) select item).ToList().Count;
                                                //containerList[0].
                                            }
                                            else
                                            {

                                                stringBuilder.Append(poitem.ContainerID + ",");
                                                //if (string.IsNullOrEmpty(containerMessage))
                                                  //  containerMessage = poitem.ContainerID;
                                                //else
                                                //    containerMessage = containerMessage + ", " + containerMessage;
                                            }

                                        }
                                        containerMessage = stringBuilder.ToString();
                                        if (!string.IsNullOrEmpty(containerMessage))
                                        {
                                            lblMsg.Text = containerMessage + " containerID(s) missing";
                                            return;
                                        }
                                    }
                                    if (containerEsnList.Count <= poQuantity)
                                    {
                                        // var SKUs = skuList.GroupBy(e => new { e.ItemCompanyGUID }).ToList();
                                        List<ContainerInfo> SKUs = skuList
                                        .GroupBy(l => l.ItemCompanyGUID)
                                        .Select(cl => new ContainerInfo
                                        {
                                            ItemCompanyGUID = cl.First().ItemCompanyGUID,

                                            PoQuantity = cl.Sum(c => c.PoQuantity),
                                        }).ToList();
                                        foreach (ContainerInfo po in SKUs)
                                        {
                                            var esnCount = (from item in containerEsnList.Where(x => x.ItemCompanyGUID == po.ItemCompanyGUID) select item).ToList();

                                            //}
                                            //foreach (ContainerInfo po in skuList)
                                            //{


                                            // containerQuantity = po.ContainerQuantity;
                                            // ItemCompanyGUID = po.ItemCompanyGUID;
                                            // qty = po.Quantity;
                                            //var esns = containerEsnList.GroupBy(e => new {  e.ContainerID });

                                            //where item.ItemCompanyGUID.Equals(ItemCompanyGUID) select item).ToList();
                                            //var esnCount = (from item in containerEsnList.Where(x => x.ItemCompanyGUID == po.ItemCompanyGUID) select item).ToList();

                                            if (esnCount != null && esnCount.Count > 0)
                                            {
                                                if (esnCount.Count <= po.PoQuantity)
                                                {
                                                    btnSubmit.Visible = true;
                                                    btnUpload.Visible = false;
                                                    lnkDownload.Visible = false;
                                                }
                                                //else
                                                //{
                                                //    if (string.IsNullOrEmpty(errorMessage))
                                                //        errorMessage = esnCount[0].ContainerID;
                                                //    else
                                                //        errorMessage = errorMessage + ", " + esnCount[0].ContainerID;
                                                //}
                                            }
                                            else
                                            {
                                                if (string.IsNullOrEmpty(errorMessage))
                                                    errorMessage = "Invalid ESN";
                                                else
                                                    errorMessage = errorMessage + ", " + "Invalid ESN";
                                            }
                                            if (!string.IsNullOrEmpty(errorMessage))
                                            {
                                                btnSubmit.Visible = false;
                                                btnUpload.Visible = true;
                                                lnkDownload.Visible = true;

                                            }
                                        }
                                    }
                                    else
                                    {
                                        //if (poQuantity > containerEsnList.Count)
                                        //    lblMsg.Text = poQuantity - containerEsnList.Count + " ESN(s) missing";
                                        //else
                                        if (poQuantity < containerEsnList.Count)
                                            lblMsg.Text = containerEsnList.Count - poQuantity + " extra ESN(s) are in file";
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                lblMsg.Text = ex.Message;
                            }
                            if (!string.IsNullOrEmpty(errorMessage))
                            {
                                lblMsg.Text = errorMessage;
                                btnSubmit.Visible = false;
                                btnUpload.Visible = true;
                                lnkDownload.Visible = true;
                                return;
                            }
                        }
                        else
                        {
                            //rptESN.DataSource = null;
                            //rptESN.DataBind();

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
                    catch (Exception ex)
                    {
                        lblMsg.Text = ex.Message;
                    }
                }
                else
                    lblMsg.Text = "Please enter Fulfillment number!";
            }
            else
                lblMsg.Text = "Please select customer!";
        }
        private string UploadFile(FileUpload fu)
        {
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


        protected void lnkDownload_Click(object sender, EventArgs e)
        {
            GenerateCSV();
        }
        private void GenerateCSV()
        {
            lblMsg.Text = string.Empty;

            string string2CSV = "ContainerID,ESN,Location" + Environment.NewLine;

            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=B2BProvisioning.csv");
            Response.Charset = "";
            Response.ContentType = "application/text";
            Response.Output.Write(string2CSV);
            Response.Flush();
            Response.End();

        }
        //private string UploadFile()
        //{
        //    string actualFilename = string.Empty;
        //    Int32 maxFileSize = 1572864;
        //    actualFilename = System.IO.Path.GetFileName(flnUpload.PostedFile.FileName);
        //    if (ConfigurationManager.AppSettings["PurchaseOrderFilesStoreage"] != null)
        //    {
        //        fileStoreLocation = ConfigurationManager.AppSettings["PurchaseOrderFilesStoreage"].ToString();
        //    }

        //    fileStoreLocation = Server.MapPath(fileStoreLocation);
        //    if (File.Exists(fileStoreLocation + actualFilename))
        //    {
        //        actualFilename = System.Guid.NewGuid().ToString() + actualFilename;
        //    }

        //    flnUpload.PostedFile.SaveAs(fileStoreLocation + actualFilename);

        //    FileInfo fileInfo = new FileInfo(fileStoreLocation + actualFilename);

        //    if (ConfigurationManager.AppSettings["maxCSVfilesize"] != null)
        //    {
        //        if (Int32.TryParse(ConfigurationManager.AppSettings["maxCSVfilesize"].ToString(), out maxFileSize))
        //        {
        //            if (fileInfo.Length > maxFileSize)
        //            {
        //                fileInfo.Delete();
        //                throw new Exception("File size is greater than " + maxFileSize + " bytes");
        //            }
        //        }
        //    }



        //    return fileStoreLocation + actualFilename;
        //}

    }
}