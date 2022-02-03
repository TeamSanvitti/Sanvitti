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
    public partial class FulfillmentUpdate : System.Web.UI.Page
    {
        private string fileStoreLocation = "~/UploadedData/FulfillmentCloseDelete/";
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
                BindCustomer();
        }

        protected void BindCustomer()
        {
            dpCompany.DataSource = avii.Classes.clsCompany.GetCompany(0, 0);
            dpCompany.DataValueField = "CompanyID";
            dpCompany.DataTextField = "CompanyName";
            dpCompany.DataBind();

        }
        protected void lnkPoNum_OnCommand(object sender, CommandEventArgs e)
        {
            
        }
        protected void btnViewAssignedPos_Click(object sender, EventArgs e)
        {
            if (Session["posList"] != null)
            {
                List<FulfillmentOrderHeader> posList = Session["posList"] as List<FulfillmentOrderHeader>;
                rptPO.DataSource = posList;
                rptPO.DataBind();
                lblCount.Text = "Total count: " + posList.Count;

            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Session["posList"] = null;
            //lblMsg.CssClass = "errormessage";
            lblMsg.Text = string.Empty;
            lblConfirm.Text = string.Empty;
            int companyID = 0;
            int userID = 0;
            int statusID = 0;
            string comment = txtComment.Text;
            string filename = string.Empty;

            if (ViewState["filename"] != null)
                filename = ViewState["filename"] as string;

            UserInfo userInfo = Session["userInfo"] as UserInfo;
            if (userInfo != null)
            {
                userID = userInfo.UserGUID;

            }
            if (Session["poupload"] != null)
            {
                List<FulfillmentOrderHeader> posList = new List<FulfillmentOrderHeader>();
                List<FulfillmentOrderHeader> poToAdd = null;

                List<FulfillmentChangeStatus> poList = Session["poupload"] as List<FulfillmentChangeStatus>;
                int n = 0;
                int poRecordCount = 0;
                string poErrorMessage = string.Empty;
                //string companyAccountNumber = string.Empty;
                
                double totalChunk = 0;
                try
                {
                    if (dpCompany.SelectedIndex > 0)
                        companyID = Convert.ToInt32(dpCompany.SelectedValue);
                    if (ddlStatus.SelectedIndex > 0)
                        statusID = Convert.ToInt32(ddlStatus.SelectedValue);

                    totalChunk = (double)poList.Count / 1000;
                    n = Convert.ToInt16(Math.Ceiling(totalChunk));
                    int poCount = 1000;
                    //var esnToUpload;
                    for (int i = 0; i < n; i++)
                    {
                        poToAdd = new List<FulfillmentOrderHeader>();
                        if (poList.Count < 1000)
                            poCount = poList.Count;
                        var poToUpload = (from item in poList.Take(poCount) select item).ToList();

                        //Upload/Assign ESN to POs
                        //poToAdd = avii.Classes.PurchaseOrder.UpdateFulfillmentTracking(poToUpload, companyAccountNumber, "U", PurchaseOrderStatus.Shipped, userID, out poRecordCount, out poErrorMessage);
                        poToAdd = avii.Classes.PurchaseOrder.UploadFulfillmentStatuses(poToUpload, companyID, "U", statusID, userID, filename, comment, out poRecordCount, out poErrorMessage);
                                    
                        //string poXML = clsGeneral.SerializeObject(esnToUpload);
                        if (i != 0)
                            poRecordCount += poRecordCount;

                        posList.AddRange(poToAdd);

                        poList.RemoveRange(0, poCount);
                    }
                    if (poRecordCount > 0 && string.IsNullOrEmpty(poErrorMessage))
                    {
                        lblMsg.Text = "Updated successfully <br /> Record count: " + poRecordCount;
                        pnlSubmit.Visible = false;
                        Session["posList"] = posList;
                        rptPO.DataSource = null;
                        rptPO.DataBind();
                        Session["poupload"] = null;
                        btnSubmit.Visible = false;
                        //pnlPO.Visible = false;
                        btnUpload.Visible = false;
                        btnViewTracking.Visible = true;
                        lblCount.Text = string.Empty;
                    }
                    else
                    {
                        if (poRecordCount > 0 && !string.IsNullOrEmpty(poErrorMessage))
                        {
                            lblMsg.Text = "Partially Updated. <br /> Fulfillment not found " + poErrorMessage + "<br /> Record count: " + poRecordCount;
                            pnlSubmit.Visible = false;
                            Session["posList"] = posList;
                            rptPO.DataSource = null;
                            rptPO.DataBind();
                            Session["poupload"] = null;
                            btnSubmit.Visible = false;
                            //pnlPO.Visible = false;
                            btnUpload.Visible = false;
                            btnViewTracking.Visible = true;
                            lblCount.Text = string.Empty;
                        }
                        else if (poRecordCount == 0 && !string.IsNullOrEmpty(poErrorMessage))
                        {
                            lblMsg.Text = "Fulfillment not found " + poErrorMessage;

                            pnlSubmit.Visible = false;
                            Session["posList"] = posList;
                            rptPO.DataSource = null;
                            rptPO.DataBind();
                            Session["poupload"] = null;
                            btnSubmit.Visible = false;
                            //pnlPO.Visible = false;
                            btnUpload.Visible = true;
                            //btnViewTracking.Visible = true;
                            lblCount.Text = string.Empty;
                        }
                    }


                }
                catch (Exception ex)
                {
                    lblMsg.Text = ex.Message;
                }


            }
            else
            {
                lblMsg.Text = "session expire";
            }
        }
        protected void btnValidateUploadedFile_Click(object sender, EventArgs e)
        {
            UploadFulfillmentOrders();
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            //lblMsg.CssClass = "errormessage";
            Session["poupload"] = null;
            Session["posList"] = null;
            rptPO.DataSource = null;
            rptPO.DataBind();
            //rptPO.Visible = false;
            lblMsg.Text = string.Empty;
            lblConfirm.Text = string.Empty;

            btnSubmit.Visible = false;
            btnUpload.Visible = true;
            btnSubmit2.Visible = false;
            pnlSubmit.Visible = false;
            lblCount.Text = string.Empty;

            btnViewTracking.Visible = false;
            dpCompany.SelectedIndex = 0;
            ddlStatus.SelectedIndex = 0;

        }
        private void UploadFulfillmentOrders()
        {
            int poReturnCount = 0;
            string returnMessege = string.Empty;
            int poTotalCount = 0;
            int companyID = 0;
            string returnMessage = string.Empty;
            lblMsg.Text = string.Empty;
            lblCount.Text = string.Empty;

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
                            bool saved = false;
                            List<FulfillmentChangeStatus> poList = new List<FulfillmentChangeStatus>();

                            using (StreamReader sr = new StreamReader(fileName))
                            {
                                string line, poNum;
                                line = poNum =  string.Empty;
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

                                            if (headerArray[0].Trim() != "fulfillmentorder#")
                                            {
                                                invalidColumns = headerArray[0];
                                                columnsIncorrectFormat = true;
                                            }
                                            
                                        }
                                        else if (!string.IsNullOrEmpty(line) && i > 0)
                                        {
                                            //poNum = trackingNumber = avOrderNumber = string.Empty;
                                            FulfillmentChangeStatus poInfo = new FulfillmentChangeStatus();
                                            poNum = line.Split(DELIMITER)[0];
                                            //if (!string.IsNullOrEmpty(poNum) && poNum.ToLower() != "ponum")
                                            if (!string.IsNullOrEmpty(poNum))
                                            {
                                                poTotalCount++;


                                                poInfo.FulfillmentOrder = poNum.Trim();
                                                saved = true;
                                                poList.Add(poInfo);
                                            }
                                            //else if (string.IsNullOrEmpty(poNum))
                                            //{

                                                
                                            //    lblMsg.Text = "Missing required data";
                                            //}
                                            //if (saved == false)
                                            //{
                                            //    //stringbuilder.Append(poNum + ", ");

                                            //}

                                        }
                                        line = poNum = string.Empty;
                                    }
                                    catch (Exception ex)
                                    {
                                        stringbuilder.Append(poNum + ", ");
                                        lblMsg.Text = ex.Message;
                                    }
                                }
                                sr.Close();

                                if (poList != null && poList.Count > 0 && columnsIncorrectFormat == false)
                                {
                                    try
                                    {
                                        List<FulfillmentOrderHeader> posList = avii.Classes.PurchaseOrder.ValidateFulfillmentOrders(poList, companyID, out poReturnCount, out returnMessage);

                                        rptPO.DataSource = posList;
                                        rptPO.DataBind();
                                        poList.Clear();

                                        var fulfillmentList = posList.Select(m => new FulfillmentChangeStatus { FulfillmentOrder = m.FulfillmentOrderNumber }).ToList();

                                        Session["poupload"] = fulfillmentList;

                                        lblCount.Text = "Record count: " + poReturnCount;
                                    }
                                    catch (Exception ex)
                                    {
                                        stringbuilder.Append(poNum + ", ");
                                        lblMsg.Text = ex.Message;
                                    }



                                    if (poReturnCount > 0 && lblMsg.Text == string.Empty && string.IsNullOrEmpty(returnMessage))
                                    {
                                        lblConfirm.Text = "FulfillmentOrder file is ready to upload";
                                        btnUpload.Visible = false;
                                        btnSubmit.Visible = true;
                                        btnSubmit2.Visible = true;
                                        pnlSubmit.Visible = true;

                                    }
                                    else
                                    {
                                        if (poReturnCount > 0 && !string.IsNullOrEmpty(returnMessage))
                                        {
                                            lblConfirm.Text = "FulfillmentOrder file is ready to upload";
                                            lblMsg.Text = lblMsg.Text + " <br /> " + returnMessage + " Fulfillment order number(s) not found.";
                                            btnUpload.Visible = false;
                                            btnSubmit.Visible = true;
                                            btnSubmit2.Visible = true;
                                            pnlSubmit.Visible = true;


                                        }
                                        else
                                        {
                                            if (!string.IsNullOrEmpty(returnMessage))
                                                lblMsg.Text = lblMsg.Text + " <br /> " + returnMessage + " Fulfillment order number(s) not found.";
                                        
                                            btnUpload.Visible = true;
                                            btnSubmit.Visible = false;

                                            btnSubmit2.Visible = false;
                                            pnlSubmit.Visible = false;

                                            rptPO.DataSource = null;
                                            rptPO.DataBind();
                                        }
                                    }
                                }
                                else
                                {
                                    rptPO.DataSource = null;
                                    rptPO.DataBind();

                                    if (poList != null && poList.Count == 0)
                                    {
                                        if (columnsIncorrectFormat)
                                        {
                                            lblMsg.Text = invalidColumns + " column(s) name is not correct";
                                        }
                                        else
                                            lblMsg.Text = "There is no fulfillment order  to upload";

                                    }
                                    if (poList != null)
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
            if (ConfigurationManager.AppSettings["FulfillmentCloseDeleteFilesStoreage"] != null)
            {
                fileStoreLocation = ConfigurationManager.AppSettings["FulfillmentCloseDeleteFilesStoreage"].ToString();
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