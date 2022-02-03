using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web.UI.WebControls;
using avii.Classes;
using System.Web.UI;

namespace avii.Admin
{
    public partial class AssignTracking : System.Web.UI.Page
    {
        private string fileStoreLocation = "~/UploadedData/ShipmentAssn/";
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
                //if (Session["UserID"] == null)
                {
                    Response.Redirect(url);
                }
            }
            if (!IsPostBack)
            {
                string uploadAdmin = ConfigurationSettings.AppSettings["UploadAdmin"].ToString();
                UserInfo userInfo = Session["userInfo"] as UserInfo;
                List<UserRole> userRoles = userInfo.ActiveRoles;
                if (userRoles != null && userRoles.Count > 0)
                {
                    var roles = (from item in userRoles where item.RoleName.Equals(uploadAdmin) select item).ToList();
                    if (roles != null && roles.Count > 0 && !string.IsNullOrEmpty(roles[0].RoleName))
                    {
                        ViewState["adminrole"] = roles[0].RoleName;
                        //lblUploadDate.Visible = true;
                    }
                    //else
                    //    lblUploadDate.Visible = false;

                }
                //lblUploadDate.Visible = false;
                BindCustomer();
                BindShipBy();
            }
        }
        private void BindShipBy()
        {

            dpShipBy.DataSource = avii.Classes.PurchaseOrder.GetShipByList();
            dpShipBy.DataTextField = "ShipByText";
            dpShipBy.DataValueField = "ShipByCode";
            dpShipBy.DataBind();

        }
        protected void BindCustomer()
        {
            dpCompany.DataSource = avii.Classes.clsCompany.GetCompany(0, 0);
            dpCompany.DataValueField = "CompanyAccountNumber";
            dpCompany.DataTextField = "CompanyName";
            dpCompany.DataBind();

        }

        protected void btnShipVia_Click(object sender, EventArgs e)
        {

            RegisterStartupScript("jsUnblockDialog", "unblockShipViaDialog();");
        }
        private void RegisterStartupScript(string key, string script)
        {
            ScriptManager.RegisterStartupScript(phrJsRunner, phrJsRunner.GetType(), key, script, true);
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        { 
            string errorMessage = string.Empty;
            int companyID = 0;
            int userID = 0;
            int recordCount = 0;
            bool isDelete = false;
            isDelete = chkDelete.Checked;
            string comment = txtComment.Text;
            string filename = string.Empty;
            FulfillmentMultiTracking trackingInfo = new FulfillmentMultiTracking();
            UserInfo userInfo = Session["userInfo"] as UserInfo;
            if (userInfo != null)
            {
                userID = userInfo.UserGUID;

            }
            trackingInfo.CustomerAccountNumber = dpCompany.SelectedValue;
            trackingInfo.ShipViaCode = dpShipBy.SelectedValue;
            trackingInfo.ShipDate = Convert.ToDateTime(txtShipDate.Text);
            trackingInfo.Comments = txtComment.Text.Trim();
            if (dpCompany.SelectedIndex > 0)
            {
                companyID = Convert.ToInt32(dpCompany.SelectedValue);

                if (Session["newtracking"] != null)
                {
                    if (ViewState["filename"] != null)
                        filename = ViewState["filename"] as string;

                    List<MultiTracking> trackingInfoList = (Session["newtracking"]) as List<MultiTracking>;
                    int n = 0;
                    double totalChunk = 0;
                    try
                    {

                        totalChunk = (double)trackingInfoList.Count / 1000;
                        n = Convert.ToInt16(Math.Ceiling(totalChunk));
                        int esnCount = 1000;
                        //var esnToUpload;
                        for (int j = 0; j < n; j++)
                        {

                            if (trackingInfoList.Count < 1000)
                                esnCount = trackingInfoList.Count;
                            var esnToUpload = (from item in trackingInfoList.Take(esnCount) select item).ToList();

                            trackingInfo.Trackings = esnToUpload;
                            //Upload/Assign Tracking to esn
                            avii.Classes.TrackingOperations.FulfillmentMultiTrackingInsert(trackingInfo, "U", userID, isDelete, filename, comment, out recordCount, out errorMessage);
                            if (recordCount > 0)
                            {
                                if (!isDelete)
                                    lblMsg.Text = "Successfully assigned" + "<br />" + "Record count: " + recordCount;
                                else
                                    lblMsg.Text = "Deleted successfully" + "<br />" + "Record count: " + recordCount;
                                Session["newtracking"] = null;
                                lblConfirm.Text = string.Empty;
                                txtComment.Text = string.Empty;
                                txtShipDate.Text = string.Empty;
                                //pnlSubmit.Visible = false;
                                btnSubmit.Visible = false;
                                btnUpload.Visible = true;
                                btnSubmit2.Visible = false;
                                pnlSubmit.Visible = false;
                                lblCount.Text = string.Empty;
                                chkDelete.Checked = false;
                                //btnViewTracking.Visible = false;
                                dpCompany.SelectedIndex = 0;
                                rptTracking.DataSource = null;
                                rptTracking.DataBind();
                            }
                            else
                            {
                                lblMsg.Text = "No record updated";
                            }



                        }

                    }
                    catch (Exception ex)
                    {
                        lblMsg.Text = ex.Message;
                    }
                }
            }
            else
                lblMsg.Text = "Customer is required!";
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearForm();
        }
        private void ClearForm()
        {
            txtShipDate.Text = string.Empty;
            dpCompany.SelectedIndex = 0;
            rptTracking.DataSource = null;
            rptTracking.DataBind();
            //rptESN.Visible = false;
            lblMsg.Text = string.Empty;
            lblConfirm.Text = string.Empty;
            chkDelete.Checked = false;
            btnSubmit.Visible = false;
            btnUpload.Visible = true;
            btnSubmit2.Visible = false;
            pnlSubmit.Visible = false;
            lblCount.Text = string.Empty;

        }
        protected void btnValidateUploadedFile_Click(object sender, EventArgs e)
        {
            UploadTrackingInfo();
        }
        
        private void UploadTrackingInfo()
        {
            bool isDelete = false;
            isDelete = chkDelete.Checked;
            FulfillmentMultiTracking trackingsInfo = new FulfillmentMultiTracking();
            lblConfirm.Text = string.Empty;
            lblMsg.Text = string.Empty;
            Session["newtracking"] = null;
            string shipdate, shipViaCode;
            shipdate =  shipViaCode = string.Empty;
            shipViaCode = dpShipBy.SelectedValue;
            shipdate = txtShipDate.Text;

            List<ShipBy> shipViaList = avii.Classes.PurchaseOrder.GetShipByList();
            //int poUploadedCount = 0;
            int poTotalCount = 0;
            int companyID = 0;
            int dateRange = Convert.ToInt32(ConfigurationSettings.AppSettings["UploadAdminDateRange"]);
            string returnMessage = string.Empty;
            Hashtable hshCheckESN = new Hashtable();
            Hashtable hsDuplicateTracking = new Hashtable();
            if (dpCompany.SelectedIndex <= 0)
            {
                //throw new Exception("Please select Company");
                returnMessage = "Please select Customer ";
                lblMsg.Text = "Please select Customer ";
            }
            else
            {
                if (!string.IsNullOrEmpty(txtShipDate.Text))
                {
                    try
                    {
                        companyID = Convert.ToInt32(dpCompany.SelectedValue);
                        if (flnUpload.PostedFile.FileName.Trim().Length == 0)
                        {
                            lblMsg.Text = "Select file to upload";
                        }
                        else
                        {
                            string extension = Path.GetExtension(flnUpload.PostedFile.FileName);
                            extension = extension.ToLower();
                            string fileName = UploadFile();
                            string invalidColumns = string.Empty;
                            bool columnsIncorrectFormat = false;
                            if (extension == ".csv")
                            {
                                System.Text.StringBuilder stringbuilder = new System.Text.StringBuilder();
                                System.Text.StringBuilder shipViaStringbuilder = new System.Text.StringBuilder();
                                bool saved = false;
                                List<MultiTracking> trackingInfoList = new List<MultiTracking>();
                                DateTime shippingDate = DateTime.Now;

                                using (StreamReader sr = new StreamReader(fileName))
                                {
                                    string line, fulfillmentNumber, trackingNumber, duplicatePO, shipmentType;//, avsoNumber, duplicatePO, ;
                                                                                                              //int lineNumber = 0;
                                    line = fulfillmentNumber = trackingNumber = shipmentType = shipViaCode = duplicatePO = shipdate = string.Empty;
                                    shipmentType = "S";
                                    //int compID = Convert.ToInt32(dpCompany.SelectedValue);
                                    int i = 0;
                                    while ((line = sr.ReadLine()) != null)
                                    {
                                        shippingDate = DateTime.Now;
                                        try
                                        {

                                            if (!string.IsNullOrEmpty(line) && i == 0)
                                            {
                                                i = 1;
                                                line = line.ToLower();
                                                string[] headerArray = line.Split(',');

                                                if (headerArray[0].Trim().ToLower() != "fulfillmentnumber")
                                                {
                                                    invalidColumns = headerArray[0];
                                                    columnsIncorrectFormat = true;
                                                }
                                                //if (headerArray[1].Trim() != "esn")
                                                //{
                                                //    if (string.IsNullOrEmpty(invalidColumns))
                                                //        invalidColumns = headerArray[1];
                                                //    else
                                                //        invalidColumns = invalidColumns + ", " + headerArray[1];
                                                //    columnsIncorrectFormat = true;
                                                //}
                                                if (headerArray[1].Trim().ToLower() != "trackingnumber")
                                                {
                                                    if (string.IsNullOrEmpty(invalidColumns))
                                                        invalidColumns = headerArray[1];
                                                    else
                                                        invalidColumns = invalidColumns + ", " + headerArray[1];
                                                    columnsIncorrectFormat = true;
                                                }

                                                //if (headerArray[3].Trim() != "shipviacode")
                                                //{
                                                //    if (string.IsNullOrEmpty(invalidColumns))
                                                //        invalidColumns = headerArray[3];
                                                //    else
                                                //        invalidColumns = invalidColumns + ", " + headerArray[3];
                                                //    columnsIncorrectFormat = true;
                                                //}
                                                //if (headerArray[4].Trim() != "avsonumber")
                                                //{
                                                //    if (string.IsNullOrEmpty(invalidColumns))
                                                //        invalidColumns = headerArray[4];
                                                //    else
                                                //        invalidColumns = invalidColumns + ", " + headerArray[4];
                                                //    columnsIncorrectFormat = true;
                                                //}

                                                //if (headerArray.Length > 5 && headerArray[5].Trim() != "shipdate")
                                                //{
                                                //    if (string.IsNullOrEmpty(invalidColumns))
                                                //        invalidColumns = headerArray[5];
                                                //    else
                                                //        invalidColumns = invalidColumns + ", " + headerArray[5];
                                                //    columnsIncorrectFormat = true;
                                                //}



                                            }
                                            else if (!string.IsNullOrEmpty(line) && i > 0)
                                            {
                                                //poNum = trackingNumber = avOrderNumber = string.Empty;
                                                MultiTracking trackingInfo = new MultiTracking();
                                                fulfillmentNumber = line.Split(DELIMITER)[0].Trim();
                                                //esn = line.Split(DELIMITER)[1].Trim();
                                                //if (!string.IsNullOrEmpty(line.Split(DELIMITER)[2]))
                                                //    lineNumber = Convert.ToInt32(line.Split(DELIMITER)[2]);
                                                trackingNumber = line.Split(DELIMITER)[1].Trim();
                                                //shipViaCode = line.Split(DELIMITER)[3].Trim();
                                                //shipViaCode = shipViaCode.ToUpper();
                                                //avsoNumber = line.Split(DELIMITER)[4].Trim();
                                                //if (line.Split(DELIMITER).Length > 5)
                                                //    shipdate = line.Split(DELIMITER)[5].Trim();
                                                //if (!string.IsNullOrEmpty(poNum) && poNum.ToLower() != "ponum")
                                                //if (poTotalCount > 0)
                                                //{
                                                //if (trackingInfoList[poTotalCount - 1].FulfillmentNumber == fulfillmentNumber && trackingInfoList[poTotalCount - 1].AVSONumber != avsoNumber)
                                                //{
                                                //    lblMsg.Text = fulfillmentNumber + " fulfillment number have more than one avsonumber.";
                                                //    //return;
                                                //}
                                                //}

                                                poTotalCount++;


                                                if (string.IsNullOrEmpty(shipmentType))
                                                    shipmentType = "S";

                                                if (!string.IsNullOrEmpty(shipdate) && ViewState["adminrole"] != null)
                                                {
                                                    if (shipdate.Trim().Length > 0)
                                                    {
                                                        DateTime dt;
                                                        if (DateTime.TryParse(shipdate.Trim(), out dt))
                                                        {
                                                            double days = (shippingDate - dt).TotalDays;
                                                            if (days > dateRange)
                                                                lblMsg.Text = "ShipDate can not be more than " + dateRange + " days before";
                                                            if (days < 0)
                                                                lblMsg.Text = "ShipDate can not be more than today date";

                                                            shippingDate = dt;
                                                        }
                                                        else
                                                            lblMsg.Text = "ShipDate does not have correct format(MM/DD/YYYY)";


                                                    }
                                                }
                                                shippingDate = DateTime.SpecifyKind(shippingDate, DateTimeKind.Unspecified);
                                                trackingInfo.FulfillmentNumber = fulfillmentNumber.Trim();
                                                trackingInfo.Tracking = trackingNumber.Trim();
                                                //trackingInfo.ESN = esn;
                                                //trackingInfo.ShipViaCode = shipViaCode;
                                                //trackingInfo.AVSONumber = avsoNumber;
                                                //trackingInfo.ShipDate = shippingDate;
                                                //trackingInfo.ShipmentType = shipmentType;

                                                saved = true;
                                                trackingInfoList.Add(trackingInfo);
                                                if (string.IsNullOrEmpty(fulfillmentNumber) || string.IsNullOrEmpty(trackingNumber) || !ValidateTrackingNumber(trackingNumber))
                                                {

                                                    if (!ValidateTrackingNumber(trackingNumber))
                                                        lblMsg.Text = trackingNumber + " not a valid trackingNumber";
                                                    else
                                                    {
                                                        if (string.IsNullOrEmpty(fulfillmentNumber) && string.IsNullOrEmpty(trackingNumber))
                                                            lblMsg.Text = "Missing Fulfillment# & Tracking# data";
                                                        else
                                                        {
                                                            if (!string.IsNullOrEmpty(fulfillmentNumber) && string.IsNullOrEmpty(trackingNumber))
                                                                lblMsg.Text = "Missing Tracking# data";
                                                            else
                                                                if (string.IsNullOrEmpty(fulfillmentNumber) && !string.IsNullOrEmpty(trackingNumber))
                                                                lblMsg.Text = "Missing Fulfillment# data";
                                                        }
                                                    }

                                                }
                                                //if (!string.IsNullOrEmpty(shipViaCode))
                                                //{
                                                //    var poShipViaList = (from item in shipViaList where item.ShipByCode.Equals(shipViaCode) select item).ToList();
                                                //    if (poShipViaList != null && poShipViaList.Count > 0)
                                                //    {

                                                //    }
                                                //    else
                                                //    {
                                                //        shipViaStringbuilder.Append(shipViaCode + ", "); // +" is not valid";
                                                //        lblMsg.Text = shipViaStringbuilder.ToString() + " invalid ShipViaCode";
                                                //    }
                                                //}

                                                if (!string.IsNullOrEmpty(fulfillmentNumber) && !string.IsNullOrEmpty(trackingNumber))
                                                {
                                                    if (duplicatePO != fulfillmentNumber)
                                                    {
                                                        if (hsDuplicateTracking.ContainsKey(trackingNumber) && !string.IsNullOrEmpty(trackingNumber))
                                                        {
                                                            //uploadEsn = false;
                                                            lblMsg.Text = "Duplicate " + trackingNumber + " TrackingNumber exists in the file";

                                                            //throw new ApplicationException("Duplicate " + lineNumber + " line number exists for FulfillmetOrder(" + poNum + ") in the file");
                                                        }
                                                        else if (!hsDuplicateTracking.ContainsKey(trackingNumber) && !string.IsNullOrEmpty(trackingNumber))
                                                        {
                                                            hsDuplicateTracking.Add(trackingNumber, trackingNumber);
                                                        }
                                                    }
                                                }

                                                duplicatePO = fulfillmentNumber;
                                                //if (!string.IsNullOrEmpty(esn) && !string.IsNullOrEmpty(trackingNumber))
                                                //{
                                                //    if (hshCheckESN.ContainsKey(esn) && !string.IsNullOrEmpty(esn))
                                                //    {
                                                //        //uploadEsn = false;
                                                //        lblMsg.Text = "Duplicate " + esn + " ESN exists in the file";

                                                //        //throw new ApplicationException("Duplicate " + lineNumber + " line number exists for FulfillmetOrder(" + poNum + ") in the file");
                                                //    }
                                                //    else if (!hshCheckESN.ContainsKey(esn) && !string.IsNullOrEmpty(esn))
                                                //    {
                                                //        hshCheckESN.Add(esn, esn);
                                                //    }
                                                //}
                                                ////if (saved == false)
                                                ////{
                                                ////    //stringbuilder.Append(poNum + ", ");

                                                ////}

                                            }
                                            line = fulfillmentNumber = trackingNumber = shipViaCode = shipmentType = string.Empty;
                                            //lineNumber = 0;

                                        }
                                        catch (Exception ex)
                                        {
                                            stringbuilder.Append(fulfillmentNumber + ", ");
                                            lblMsg.Text = ex.Message;
                                        }
                                    }
                                    sr.Close();

                                    int n = 0;
                                    string poFulfillmentMessage = string.Empty;
                                    string poTrackingExistsMessage = string.Empty;
                                    string poEsnMessage = string.Empty;
                                    string poEsnExistsMessage = string.Empty;
                                    string errorMessage = string.Empty;

                                    if (trackingInfoList != null && trackingInfoList.Count > 0 && columnsIncorrectFormat == false)
                                    {

                                        double totalChunk = 0;
                                        try
                                        {

                                            totalChunk = (double)trackingInfoList.Count / 1000;
                                            n = Convert.ToInt16(Math.Ceiling(totalChunk));
                                            int esnCount = 1000;
                                            //var esnToUpload;
                                            for (int j = 0; j < n; j++)
                                            {
                                                poFulfillmentMessage = poTrackingExistsMessage = poEsnExistsMessage = poEsnMessage = string.Empty;
                                                if (trackingInfoList.Count < 1000)
                                                    esnCount = trackingInfoList.Count;
                                                var esnToUpload = (from item in trackingInfoList.Take(esnCount) select item).ToList();
                                                trackingsInfo.CustomerAccountNumber = dpCompany.SelectedValue;
                                                trackingsInfo.ShipDate = shippingDate;
                                                trackingsInfo.ShipViaCode = shipViaCode;
                                                trackingsInfo.Trackings = esnToUpload;
                                                //Upload/Assign ESN to POs
                                                avii.Classes.TrackingOperations.ValidateFulfillmentMultiTracking(trackingsInfo, isDelete, out poFulfillmentMessage, out poTrackingExistsMessage, out poEsnMessage, out poEsnExistsMessage);

                                                if (!string.IsNullOrEmpty(poFulfillmentMessage))
                                                {
                                                    if (string.IsNullOrEmpty(errorMessage))
                                                        errorMessage = poFulfillmentMessage + " Fulfillment Order does not exists";
                                                    else
                                                        errorMessage = errorMessage + " <br /> " + poFulfillmentMessage + " Fulfillment Order(s) does not exists";
                                                }
                                                if (!string.IsNullOrEmpty(poTrackingExistsMessage))
                                                {
                                                    if (string.IsNullOrEmpty(errorMessage))
                                                        errorMessage = poTrackingExistsMessage + " trackingnumber(s) already exists";
                                                    else
                                                        errorMessage = errorMessage + " <br /> " + poTrackingExistsMessage + " trackingnumber(s) already exists";
                                                }

                                                //if (!string.IsNullOrEmpty(poEsnMessage))
                                                //{
                                                //    if (string.IsNullOrEmpty(errorMessage))
                                                //        errorMessage = poEsnMessage + " ESN(s) does not exists";
                                                //    else
                                                //        errorMessage = errorMessage + " <br /> " + poEsnMessage + " ESN(s) does not exists";
                                                //}
                                                //if (!string.IsNullOrEmpty(poEsnExistsMessage))
                                                //{
                                                //    if (string.IsNullOrEmpty(errorMessage))
                                                //        errorMessage = poEsnExistsMessage + " ESN(s) already exists";
                                                //    else
                                                //        errorMessage = errorMessage + " <br /> " + poEsnExistsMessage + " ESN(s) already exists";
                                                //}

                                            }

                                        }
                                        catch (Exception ex)
                                        {
                                            lblMsg.Text = ex.Message;
                                        }
                                        if (!string.IsNullOrEmpty(errorMessage))
                                        {
                                            lblMsg.Text = errorMessage;
                                            //return;
                                        }


                                        rptTracking.DataSource = trackingInfoList;
                                        rptTracking.DataBind();
                                        Session["newtracking"] = trackingInfoList;
                                        lblCount.Text = "Record count: " + trackingInfoList.Count;
                                        if (lblMsg.Text == string.Empty)
                                        {
                                            lblConfirm.Text = "Tracking file is ready to upload";
                                            btnUpload.Visible = false;
                                            btnSubmit.Visible = true;
                                            btnSubmit2.Visible = true;
                                            pnlSubmit.Visible = true;

                                        }
                                        else
                                        {
                                            btnUpload.Visible = true;
                                            btnSubmit.Visible = false;

                                            btnSubmit2.Visible = false;
                                            pnlSubmit.Visible = false;
                                        }
                                    }
                                    else
                                    {
                                        rptTracking.DataSource = null;
                                        rptTracking.DataBind();

                                        if (trackingInfoList != null && trackingInfoList.Count == 0)
                                        {
                                            if (columnsIncorrectFormat)
                                            {
                                                lblMsg.Text = invalidColumns + " column(s) name is not correct";
                                            }
                                            else
                                                lblMsg.Text = "There is no fulfillment order  to upload";

                                        }
                                        if (trackingInfoList != null)
                                        {
                                            if (columnsIncorrectFormat)
                                            {
                                                lblMsg.Text = invalidColumns + " column(s) name is not correct";
                                            }
                                            else
                                                lblMsg.Text = "There is no fulfillment order  to upload";
                                        }

                                    }
                                    //poUploadedCount = Classes.PurchaseOrder.UpdateTrackingInfo(trackingInfoList, userID);

                                }
                            }
                            else
                                lblMsg.Text = "Invalid file!";
                        }
                        //if (poTotalCount == 0 && poUploadedCount == 0)
                        //    returnMessage = "There is no valid Purchase Orders in the file";
                        //if (poUploadedCount > 0 && poUploadedCount == poTotalCount)
                        //    returnMessage = "Successfully uploaded";
                        //else
                        //    returnMessage = poUploadedCount + " out of " + poTotalCount + " Purchase Orders uploaded Successfully";

                    }
                    catch (Exception ex)
                    {
                        lblMsg.Text = ex.Message;
                    }
                }
                else
                {
                    lblMsg.Text = "Please select ship date";
                    returnMessage = "Please select ship date";
                }

            }


            //return returnMessage;

        }
        private bool ValidateTrackingNumber(string TrackingNumber)
        {
            bool notFound = true;
            string target = ".+";
            char[] anyOf = target.ToCharArray();
            if (TrackingNumber.IndexOfAny(anyOf) > -1)
            {
                notFound = false;
            }

            return notFound;
        }
        private string UploadFile()
        {
            string actualFilename = string.Empty;
            Int32 maxFileSize = 1572864;
            actualFilename = System.IO.Path.GetFileName(flnUpload.PostedFile.FileName);
            if (ConfigurationManager.AppSettings["ShipmentAssnFilesStoreage"] != null)
            {
                fileStoreLocation = ConfigurationManager.AppSettings["ShipmentAssnFilesStoreage"].ToString();
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
                        throw new Exception("File size is greater than " + maxFileSize + " bytes");
                    }
                }
            }



            return fileStoreLocation + actualFilename;
        }
        
    }
}