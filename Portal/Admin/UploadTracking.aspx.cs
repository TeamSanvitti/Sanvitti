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
    public partial class UploadTracking : System.Web.UI.Page
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
                //if (Session["UserID"] == null)
                {
                    Response.Redirect(url);
                }
            }
            if (!this.IsPostBack)
            {
                BindCustomer();
            }
        }
        protected void BindCustomer()
        {
            dpCompany.DataSource = avii.Classes.clsCompany.GetCompany(0, 0);
            dpCompany.DataValueField = "CompanyAccountNumber";
            dpCompany.DataTextField = "CompanyName";
            dpCompany.DataBind();

        }

        private void RegisterStartupScript(string key, string script)
        {
            ScriptManager.RegisterStartupScript(phrJsRunner, phrJsRunner.GetType(), key, script, true);
        }

        protected void rpt_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                LinkButton lnkPoNum = (LinkButton)e.Item.FindControl("lnkPoNum");
                lnkPoNum.OnClientClick = "openDialogAndBlock('Fulfillment Detail', '" + lnkPoNum.ClientID + "')";

            }
        }
        protected void lnkPoNum_OnCommand(object sender, CommandEventArgs e)
        {
            if (dpCompany.SelectedIndex > 0)
            {
                string poNum = e.CommandArgument.ToString();
                string companyAccountNumber = dpCompany.SelectedValue;
                if (!string.IsNullOrEmpty(poNum))
                {

                    Control tmp1 = LoadControl("../controls/PODetails.ascx");
                    avii.Controls.PODetails ctlPODetails = tmp1 as avii.Controls.PODetails;
                    pnlPO.Controls.Clear();
                    ctlPODetails.BindPO(poNum, companyAccountNumber);

                    pnlPO.Controls.Add(ctlPODetails);

                    RegisterStartupScript("jsUnblockDialog", "unblockDialog();");



                }
            }
            else
                lblMsg.Text = "Select a customer!";
        }
        protected void btnViewAssignedPos_Click(object sender, EventArgs e)
        {
            if (Session["posList"] != null)
            {
                List<TrackingInfo> posList = Session["posList"] as List<TrackingInfo>;
                rptTracking.DataSource = posList;
                rptTracking.DataBind();
                lblCount.Text = "Total count: " + posList.Count;

            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Session["posList"] = null;
            //lblMsg.CssClass = "errormessage";
            lblMsg.Text = string.Empty;
            lblConfirm.Text = string.Empty;

            int userID = 0;
            UserInfo userInfo = Session["userInfo"] as UserInfo;
            if (userInfo != null)
            {
                userID = userInfo.UserGUID;

            }
            if (Session["tracking"] != null)
            {
                List<TrackingInfo> posList = new List<TrackingInfo>();
                List<TrackingInfo> poToAdd = null;

                List<TrackingInfo> poList = Session["tracking"] as List<TrackingInfo>;
                int n = 0;
                int poRecordCount = 0;
                string poErrorMessage = string.Empty;
                string companyAccountNumber = string.Empty;
                companyAccountNumber = dpCompany.SelectedValue;
                double totalChunk = 0;
                try
                {

                    totalChunk = (double)poList.Count / 1000;
                    n = Convert.ToInt16(Math.Ceiling(totalChunk));
                    int poCount = 1000;
                    //var esnToUpload;
                    for (int i = 0; i < n; i++)
                    {
                        poToAdd = new List<TrackingInfo>();
                        if (poList.Count < 1000)
                            poCount = poList.Count;
                        var poToUpload = (from item in poList.Take(poCount) select item).ToList();

                        //Upload/Assign ESN to POs
                        poToAdd = avii.Classes.PurchaseOrder.UpdateFulfillmentTracking(poToUpload, companyAccountNumber, "U", PurchaseOrderStatus.Shipped, userID, out poRecordCount, out poErrorMessage);
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
                        rptTracking.DataSource = null;
                        rptTracking.DataBind();
                        Session["tracking"] = null;
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
                            rptTracking.DataSource = null;
                            rptTracking.DataBind();
                            Session["tracking"] = null;
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
                            rptTracking.DataSource = null;
                            rptTracking.DataBind();
                            Session["tracking"] = null;
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
            UploadTrackingInfo();
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            //lblMsg.CssClass = "errormessage";

            rptTracking.DataSource = null;
            rptTracking.DataBind();
            //rptTracking.Visible = false;
            lblMsg.Text = string.Empty;
            lblConfirm.Text = string.Empty;

            btnSubmit.Visible = false;
            btnUpload.Visible = true;
            btnSubmit2.Visible = false;
            pnlSubmit.Visible = false;
            lblCount.Text = string.Empty;

            btnViewTracking.Visible = false;
            dpCompany.SelectedIndex = 0;

        }
        private void UploadTrackingInfo()
        {
            //int poUploadedCount = 0;
            int poTotalCount = 0;
            string returnMessage = string.Empty;

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
                            List<TrackingInfo> trackingInfoList = new List<TrackingInfo>();

                            using (StreamReader sr = new StreamReader(fileName))
                            {
                                string line, poNum, trackingNumber, avOrderNumber;
                                line = poNum = trackingNumber = avOrderNumber = string.Empty;
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

                                            if (headerArray[0].Trim() != "ponum")
                                            {
                                                invalidColumns = headerArray[0];
                                                columnsIncorrectFormat = true;
                                            }
                                            if (headerArray[1].Trim() != "trackingnumber")
                                            {
                                                if (string.IsNullOrEmpty(invalidColumns))
                                                    invalidColumns = headerArray[1];
                                                else
                                                    invalidColumns = invalidColumns + ", " + headerArray[1];
                                                columnsIncorrectFormat = true;
                                            }
                                        }
                                        else if (!string.IsNullOrEmpty(line) && i > 0)
                                        {
                                            //poNum = trackingNumber = avOrderNumber = string.Empty;
                                            TrackingInfo trackingInfo = new TrackingInfo();
                                            poNum = line.Split(DELIMITER)[0];
                                            trackingNumber = line.Split(DELIMITER)[1];
                                            avOrderNumber = line.Split(DELIMITER)[2];
                                            //if (!string.IsNullOrEmpty(poNum) && poNum.ToLower() != "ponum")

                                            poTotalCount++;


                                            trackingInfo.PurchaseOrderNumber = poNum.Trim();
                                            trackingInfo.ShipToTrackingNumber = trackingNumber.Trim();
                                            trackingInfo.SalesOrderNumber = avOrderNumber.Trim();
                                            saved = true;
                                            trackingInfoList.Add(trackingInfo);
                                            if (string.IsNullOrEmpty(poNum) || string.IsNullOrEmpty(trackingNumber)
                                                            || !ValidateTrackingNumber(trackingNumber))
                                            {

                                                if (!ValidateTrackingNumber(trackingNumber))
                                                    lblMsg.Text = trackingNumber + " not a valid trackingNumber";
                                                else
                                                    lblMsg.Text = "Missing required data";
                                            }
                                            //if (saved == false)
                                            //{
                                            //    //stringbuilder.Append(poNum + ", ");

                                            //}

                                        }
                                        line = poNum = trackingNumber = avOrderNumber = string.Empty;
                                    }
                                    catch (Exception ex)
                                    {
                                        stringbuilder.Append(poNum + ", ");
                                        lblMsg.Text = ex.Message;
                                    }
                                }
                                sr.Close();

                                if (trackingInfoList != null && trackingInfoList.Count > 0 && columnsIncorrectFormat == false)
                                {
                                    rptTracking.DataSource = trackingInfoList;
                                    rptTracking.DataBind();
                                    Session["tracking"] = trackingInfoList;
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
            if (ConfigurationManager.AppSettings["PurchaseOrderFilesStoreage"] != null)
            {
                fileStoreLocation = ConfigurationManager.AppSettings["PurchaseOrderFilesStoreage"].ToString();
            }

            fileStoreLocation = Server.MapPath(fileStoreLocation);
            if (File.Exists(fileStoreLocation + actualFilename))
            {
                actualFilename = System.Guid.NewGuid().ToString() + actualFilename;
            }

            flnUpload.PostedFile.SaveAs(fileStoreLocation + actualFilename);

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
