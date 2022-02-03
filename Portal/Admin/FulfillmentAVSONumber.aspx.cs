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
    public partial class FulfillmentAVSONumber : System.Web.UI.Page
    {
        private string fileStoreLocation = "~/UploadedData/AVSO/";
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
                BindCustomer();
            }
        }

        protected void BindCustomer()
        {
            dpCompany.DataSource = avii.Classes.clsCompany.GetCompany(0, 0);
            dpCompany.DataValueField = "CompanyID";
            dpCompany.DataTextField = "CompanyName";
            dpCompany.DataBind();

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
            //bool isDelete = false;
            //isDelete = chkDelete.Checked;
            string comment = txtComment.Text;
            string filename = string.Empty;
            string avsoNumber = txtAVSO.Text.Trim();
            UserInfo userInfo = Session["userInfo"] as UserInfo;
            if (userInfo != null)
            {
                userID = userInfo.UserGUID;

            }
            if (dpCompany.SelectedIndex > 0)
            {
                companyID = Convert.ToInt32(dpCompany.SelectedValue);

                if (Session["avso"] != null)
                {
                    if (ViewState["filename"] != null)
                        filename = ViewState["filename"] as string;

                    List<FulfillmentNumber> poList = (Session["avso"]) as List<FulfillmentNumber>;
                    int n = 0;
                    double totalChunk = 0;
                    try
                    {

                        totalChunk = (double)poList.Count / 1000;
                        n = Convert.ToInt16(Math.Ceiling(totalChunk));
                        int poCount = 1000;
                        //var esnToUpload;
                        for (int j = 0; j < n; j++)
                        {

                            if (poList.Count < 1000)
                                poCount = poList.Count;
                            var poToUpload = (from item in poList.Take(poCount) select item).ToList();

                            //Upload/Assign Tracking to esn
                            avii.Classes.FulfillmentOperations.FulfillmentAVSONumberUpdate(poToUpload, companyID, avsoNumber, "U", userID, filename, comment, out recordCount, out errorMessage);
                            if (recordCount > 0)
                            {
                                lblMsg.Text = "Successfully assigned" + "<br />" + "Record count: " + recordCount;
                                Session["avso"] = null;
                                lblConfirm.Text = string.Empty;
                                //pnlSubmit.Visible = false;
                                btnSubmit.Visible = false;
                                btnUpload.Visible = true;
                                btnSubmit2.Visible = false;
                                pnlSubmit.Visible = false;
                                lblCount.Text = string.Empty;
                                //chkDelete.Checked = false;
                                //btnViewTracking.Visible = false;
                                dpCompany.SelectedIndex = 0;
                                txtAVSO.Text = string.Empty;
                                txtComment.Text = string.Empty;
                                gvPO.DataSource = null;
                                gvPO.DataBind();
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
            dpCompany.SelectedIndex = 0;
            gvPO.DataSource = null;
            gvPO.DataBind();
            //rptESN.Visible = false;
            lblMsg.Text = string.Empty;
            lblConfirm.Text = string.Empty;
            //chkDelete.Checked = false;
            btnSubmit.Visible = false;
            btnUpload.Visible = true;
            btnSubmit2.Visible = false;
            pnlSubmit.Visible = false;
            lblCount.Text = string.Empty;

        }
        protected void btnValidateUploadedFile_Click(object sender, EventArgs e)
        {
            ValidateFulfillmentNumber();
        }

        private void ValidateFulfillmentNumber()
        {
            
            lblConfirm.Text = string.Empty;
            lblMsg.Text = string.Empty;
            Session["avso"] = null;
            //int poUploadedCount = 0;
            int poTotalCount = 0;
            int companyID = 0;
            string returnMessage = string.Empty;
            Hashtable hsDuplicatePO = new Hashtable();
            if (dpCompany.SelectedIndex <= 0)
            {
                //throw new Exception("Please select Company");
                returnMessage = "Please select Company";
                lblMsg.Text = "Please select Company";
            }
            else
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
                            List<FulfillmentNumber> fulfillmentOrder = new List<FulfillmentNumber>();
                            
                            using (StreamReader sr = new StreamReader(fileName))
                            {
                                string line, fulfillmentNumber, avsoNumber;
                                //int lineNumber = 0;
                                line = fulfillmentNumber = avsoNumber = string.Empty;
                                avsoNumber = txtAVSO.Text.Trim();
                                if (string.IsNullOrEmpty(avsoNumber))
                                {
                                    lblMsg.Text = "AVSO# can not be empty";
                                }
                                //int compID = Convert.ToInt32(dpCompany.SelectedValue);
                                int i = 0;
                                while ((line = sr.ReadLine()) != null)
                                {

                                    try
                                    {

                                        if (!string.IsNullOrEmpty(line) && i == 0)
                                        {
                                            i = 1;
                                            line = line.ToLower();
                                            string[] headerArray = line.Split(',');

                                            if (headerArray[0].Trim() != "fulfillmentnumber")
                                            {
                                                invalidColumns = headerArray[0];
                                                columnsIncorrectFormat = true;
                                            }
                                            


                                        }
                                        else if (!string.IsNullOrEmpty(line) && i > 0)
                                        {
                                            //poNum = trackingNumber = avOrderNumber = string.Empty;
                                            FulfillmentNumber fulfillmentInfo = new FulfillmentNumber();
                                            fulfillmentNumber = line.Split(DELIMITER)[0].Trim();
                                            
                                            poTotalCount++;



                                            fulfillmentInfo.FulfillmentOrder = fulfillmentNumber.Trim();
                                            
                                            saved = true;
                                            fulfillmentOrder.Add(fulfillmentInfo);
                                            if (string.IsNullOrEmpty(fulfillmentNumber))
                                            {
                                                lblMsg.Text = "Fulfillment# can not be empty";

                                            }
                                            
                                            if (!string.IsNullOrEmpty(fulfillmentNumber))
                                            {

                                                if (hsDuplicatePO.ContainsKey(fulfillmentNumber) && !string.IsNullOrEmpty(fulfillmentNumber))
                                                    {
                                                        //uploadEsn = false;
                                                        lblMsg.Text = "Duplicate " + fulfillmentNumber + " FulfillmentNumber exists in the file";

                                                        //throw new ApplicationException("Duplicate " + lineNumber + " line number exists for FulfillmetOrder(" + poNum + ") in the file");
                                                    }
                                                else if (!hsDuplicatePO.ContainsKey(fulfillmentNumber) && !string.IsNullOrEmpty(fulfillmentNumber))
                                                    {
                                                        hsDuplicatePO.Add(fulfillmentNumber, fulfillmentNumber);
                                                    }
                                                
                                            }

                                            

                                        }
                                        line = fulfillmentNumber = string.Empty;
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
                                string poErrorMessage = string.Empty;
                                string errorMessage = string.Empty;

                                if (fulfillmentOrder != null && fulfillmentOrder.Count > 0 && columnsIncorrectFormat == false)
                                {

                                    double totalChunk = 0;
                                    try
                                    {

                                        totalChunk = (double)fulfillmentOrder.Count / 1000;
                                        n = Convert.ToInt16(Math.Ceiling(totalChunk));
                                        int fnCount = 1000;
                                        //var esnToUpload;
                                        for (int j = 0; j < n; j++)
                                        {
                                            poFulfillmentMessage = poErrorMessage = string.Empty;
                                            if (fulfillmentOrder.Count < 1000)
                                                fnCount = fulfillmentOrder.Count;
                                            var poToUpload = (from item in fulfillmentOrder.Take(fnCount) select item).ToList();

                                            //validate POs
                                            avii.Classes.FulfillmentOperations.ValidateAVSOFulfillmentList(poToUpload, companyID, avsoNumber, out poFulfillmentMessage, out poErrorMessage);

                                            if (!string.IsNullOrEmpty(poFulfillmentMessage))
                                            {
                                                if (string.IsNullOrEmpty(errorMessage))
                                                    errorMessage = poFulfillmentMessage + " Fulfillment Order does not exists";
                                                else
                                                    errorMessage = errorMessage + " <br /> " + poFulfillmentMessage + " FulfillmentOrder(s) does not exists";
                                            }
                                            if(!string.IsNullOrEmpty(poErrorMessage))
                                            {
                                                if (string.IsNullOrEmpty(errorMessage))
                                                    errorMessage = poErrorMessage;
                                                else
                                                    errorMessage = errorMessage + " <br /> " + poErrorMessage;
                                                
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
                                        //return;
                                    }


                                    gvPO.DataSource = fulfillmentOrder;
                                    gvPO.DataBind();
                                    Session["avso"] = fulfillmentOrder;
                                    lblCount.Text = "Record count: " + fulfillmentOrder.Count;
                                    if (lblMsg.Text == string.Empty)
                                    {
                                        lblConfirm.Text = "FulfillmentOrder file is ready to upload";
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
                                    gvPO.DataSource = null;
                                    gvPO.DataBind();

                                    if (fulfillmentOrder != null && fulfillmentOrder.Count == 0)
                                    {
                                        if (columnsIncorrectFormat)
                                        {
                                            lblMsg.Text = invalidColumns + " column(s) name is not correct";
                                        }
                                        else
                                            lblMsg.Text = "There is no fulfillment order  to upload";

                                    }
                                    if (fulfillmentOrder != null)
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


            //return returnMessage;

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