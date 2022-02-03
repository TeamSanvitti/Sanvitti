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
    public partial class PoTrackingUpload : System.Web.UI.Page
    {
        private string fileStoreLocation = "~/UploadedData/Shipment/";
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
                string uploadAdmin = ConfigurationSettings.AppSettings["UploadAdmin"].ToString();
                UserInfo userInfo = Session["userInfo"] as UserInfo;
                List<UserRole> userRoles = userInfo.ActiveRoles;
                if (userRoles != null && userRoles.Count > 0)
                {
                    var roles = (from item in userRoles where item.RoleName.Equals(uploadAdmin) select item).ToList();
                    if (roles != null && roles.Count > 0 && !string.IsNullOrEmpty(roles[0].RoleName))
                    {
                        ViewState["adminrole"] = roles[0].RoleName;
                        lblUploadDate.Visible = true;
                    }
                    else
                        lblUploadDate.Visible = false;


                }
                //lblUploadDate.Visible = false;
                BindCustomer();
                BindShipBy();
            }
        }
        protected void BindCustomer()
        {
            dpCompany.DataSource = avii.Classes.clsCompany.GetCompany(0, 0);
            dpCompany.DataValueField = "CompanyAccountNumber";
            dpCompany.DataTextField = "CompanyName";
            dpCompany.DataBind();

        }
        private void BindShipBy()
        {
            //List<ShipBy> shipVia = avii.Classes.PurchaseOrder.GetShipByList();

            //ddlShipby.DataSource = avii.Classes.PurchaseOrder.GetShipByList();
            //ddlShipby.DataTextField = "ShipByText";
            //ddlShipby.DataValueField = "ShipByID";
            //ddlShipby.DataBind();
        }

        protected void btnShipVia_Click(object sender, EventArgs e)
        {

            RegisterStartupScript("jsUnblockDialog", "unblockShipViaDialog();");
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
                List<Trackings> posList = Session["posList"] as List<Trackings>;
                rptTracking.DataSource = posList;
                rptTracking.DataBind();
                lblCount.Text = "Total count: " + posList.Count;

            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            bool isDelete = chkDelete.Checked;
            string comment = txtComment.Text;
            string filename = string.Empty;

            //string avSaleOrderNumber;
            //int shipByID = 0;

            //avSaleOrderNumber = null;
            //if (ddlShipby.SelectedIndex > 0)
            //{
            //    shipByID = Convert.ToInt32(ddlShipby.SelectedValue);
            //}

            //if (txtAvOrderNumber.Text.Trim().Length > 0)
            //    avSaleOrderNumber = txtAvOrderNumber.Text.Trim();
            //comments = txtComments.Text;
            Session["posList"] = null;
            //lblMsg.CssClass = "errormessage";
            lblMsg.Text = string.Empty;
            lblConfirm.Text = string.Empty;
            int n = 0;
            int poRecordCount = 0;
            string poErrorMessage = string.Empty;
            string companyAccountNumber = string.Empty;
            companyAccountNumber = dpCompany.SelectedValue;
                
            int userID = 0;
            UserInfo userInfo = Session["userInfo"] as UserInfo;
            if (userInfo != null)
            {
                userID = userInfo.UserGUID;

            }
            if (!string.IsNullOrEmpty(companyAccountNumber))
            {
                if (Session["trackings"] != null)
                {
                    if (ViewState["filename"] != null)
                        filename = ViewState["filename"] as string;

                    List<Trackings> posList = new List<Trackings>();
                    List<Trackings> poToAdd = null;

                    List<Trackings> poList = Session["trackings"] as List<Trackings>;
                    double totalChunk = 0;
                    try
                    {
                        totalChunk = (double)poList.Count / 1000;
                        n = Convert.ToInt16(Math.Ceiling(totalChunk));
                        int poCount = 1000;
                        //var esnToUpload;
                        for (int i = 0; i < n; i++)
                        {
                            poToAdd = new List<Trackings>();
                            if (poList.Count < 1000)
                                poCount = poList.Count;
                            var poToUpload = (from item in poList.Take(poCount) select item).ToList();

                            //Upload/Assign ESN to POs
                            poToAdd = avii.Classes.TrackingOperations.UpdateFulfillmentTracking(poToUpload, companyAccountNumber, "U", PurchaseOrderStatus.Shipped, userID, isDelete, filename, comment, out poRecordCount, out poErrorMessage);
                            //string poXML = clsGeneral.SerializeObject(esnToUpload);
                            if (i != 0)
                                poRecordCount += poRecordCount;

                            posList.AddRange(poToAdd);

                            poList.RemoveRange(0, poCount);
                        }
                        if (poRecordCount > 0 && string.IsNullOrEmpty(poErrorMessage))
                        {
                            if (!isDelete)
                                lblMsg.Text = "Updated successfully <br /> Record count: " + poRecordCount;
                            else
                                lblMsg.Text = "Deleted successfully <br /> Record count: " + poRecordCount;

                            pnlSubmit.Visible = false;
                            Session["posList"] = posList;
                            rptTracking.DataSource = null;
                            rptTracking.DataBind();
                            Session["trackings"] = null;
                            //lblCount.Text = string.Empty;
                            lblConfirm.Text = string.Empty;

                            btnSubmit.Visible = false;
                            btnUpload.Visible = true;
                            btnSubmit2.Visible = false;
                            pnlSubmit.Visible = false;
                            lblCount.Text = string.Empty;
                            chkDelete.Checked = false;
                            //btnViewTracking.Visible = false;
                            dpCompany.SelectedIndex = 0;
                            //ddlShipby.SelectedIndex = 0;
                            //txtAvOrderNumber.Text = string.Empty;

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
                                Session["trackings"] = null;
                                lblConfirm.Text = string.Empty;

                                btnSubmit.Visible = false;
                                btnUpload.Visible = true;
                                btnSubmit2.Visible = false;
                                pnlSubmit.Visible = false;
                                lblCount.Text = string.Empty;

                                //btnViewTracking.Visible = false;
                                dpCompany.SelectedIndex = 0;
                                //ddlShipby.SelectedIndex = 0;
                                //txtAvOrderNumber.Text = string.Empty;


                            }
                            else if (poRecordCount == 0 && !string.IsNullOrEmpty(poErrorMessage))
                            {
                                lblMsg.Text = "Fulfillment not found " + poErrorMessage;

                                pnlSubmit.Visible = false;
                                Session["posList"] = posList;
                                rptTracking.DataSource = null;
                                rptTracking.DataBind();
                                Session["trackings"] = null;
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
            else
            {

                
                lblMsg.Text = "Customer is required!";
            }
        }
        protected void btnValidateUploadedFile_Click(object sender, EventArgs e)
        {
            UploadTrackingInfo();
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            //lblMsg.CssClass = "errormessage";
            chkDelete.Checked = false;
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

            //btnViewTracking.Visible = false;
            dpCompany.SelectedIndex = 0;

        }
        private void UploadTrackingInfo()
        {
            lblConfirm.Text = string.Empty;
            lblMsg.Text = string.Empty;
            Session["trackings"] = null;
            List<ShipBy> shipViaList = avii.Classes.PurchaseOrder.GetShipByList();
            int dateRange = Convert.ToInt32(ConfigurationSettings.AppSettings["UploadAdminDateRange"]);
            //int poUploadedCount = 0;
            int poTotalCount = 0;
            string returnMessage = string.Empty;
            Hashtable hshCheckDuplicateLineNo = new Hashtable();
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
                            System.Text.StringBuilder shipViaStringbuilder = new System.Text.StringBuilder();
                            bool saved = false;
                            List<Trackings> trackingInfoList = new List<Trackings>();
                            DateTime shippingDate = DateTime.Now;
                            using (StreamReader sr = new StreamReader(fileName))
                            {
                                string line, poNum, avOrderNumber, trackingNumber, comments, shippingVia, shipmentType, shipDate;
                                int lineNumber = 0;
                                line = poNum = trackingNumber = comments = shippingVia = shipmentType = avOrderNumber = shipDate = string.Empty;
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
                                            if (headerArray[2].Trim() != "avordernumber")
                                            {
                                                if (string.IsNullOrEmpty(invalidColumns))
                                                    invalidColumns = headerArray[2];
                                                else
                                                    invalidColumns = invalidColumns + ", " + headerArray[2];
                                                columnsIncorrectFormat = true;
                                            }
                                            if (headerArray[3].Trim() != "shipmenttype")
                                            {
                                                if (string.IsNullOrEmpty(invalidColumns))
                                                    invalidColumns = headerArray[3];
                                                else
                                                    invalidColumns = invalidColumns + ", " + headerArray[3];
                                                columnsIncorrectFormat = true;
                                            }

                                            if (headerArray[4].Trim() != "shippingvia")
                                            {
                                                if (string.IsNullOrEmpty(invalidColumns))
                                                    invalidColumns = headerArray[4];
                                                else
                                                    invalidColumns = invalidColumns + ", " + headerArray[4];
                                                columnsIncorrectFormat = true;
                                            }
                                            
                                            if (headerArray[5].Trim() != "comments")
                                            {
                                                if (string.IsNullOrEmpty(invalidColumns))
                                                    invalidColumns = headerArray[5];
                                                else
                                                    invalidColumns = invalidColumns + ", " + headerArray[5];
                                                columnsIncorrectFormat = true;
                                            }
                                            if (headerArray.Length > 6 && headerArray[6].Trim() != "shipdate")
                                            {
                                                if (string.IsNullOrEmpty(invalidColumns))
                                                    invalidColumns = headerArray[6];
                                                else
                                                    invalidColumns = invalidColumns + ", " + headerArray[6];
                                                columnsIncorrectFormat = true;
                                            }

                                            
                                        }
                                        else if (!string.IsNullOrEmpty(line) && i > 0)
                                        {
                                            //poNum = trackingNumber = avOrderNumber = string.Empty;
                                            Trackings trackingInfo = new Trackings();
                                            poNum = line.Split(DELIMITER)[0];
                                            trackingNumber = line.Split(DELIMITER)[1];
                                            //if (!string.IsNullOrEmpty(line.Split(DELIMITER)[2]))
                                            //    lineNumber = Convert.ToInt32(line.Split(DELIMITER)[2]);
                                            avOrderNumber = line.Split(DELIMITER)[2];
                                            shipmentType = line.Split(DELIMITER)[3];
                                            shippingVia = line.Split(DELIMITER)[4];
                                            comments = line.Split(DELIMITER)[5];
                                            if (line.Split(DELIMITER).Length > 6)
                                                shipDate = line.Split(DELIMITER)[6];
                                            //if (!string.IsNullOrEmpty(poNum) && poNum.ToLower() != "ponum")

                                            poTotalCount++;

                                            if (string.IsNullOrEmpty(shipmentType))
                                                shipmentType = "S";
                                            if (!string.IsNullOrEmpty(shipDate) && ViewState["adminrole"] != null)
                                            {
                                                if (shipDate.Trim().Length > 0)
                                                {
                                                    DateTime dt;
                                                    if (DateTime.TryParse(shipDate.Trim(), out dt))
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
                                            trackingInfo.FulfillmentNumber = poNum.Trim();
                                            trackingInfo.Tracking = trackingNumber.Trim();
                                            trackingInfo.AvOrderNumber = avOrderNumber;
                                            trackingInfo.ShippingVia = shippingVia;
                                            trackingInfo.ShipmentType = shipmentType;
                                            trackingInfo.Comments = comments;
                                            trackingInfo.ShipDate = shippingDate;
                                            
                                            saved = true;
                                            trackingInfoList.Add(trackingInfo);
                                            if (string.IsNullOrEmpty(poNum) || string.IsNullOrEmpty(trackingNumber)
                                                            || !ValidateTrackingNumber(trackingNumber) )
                                            {

                                                if (!ValidateTrackingNumber(trackingNumber))
                                                    lblMsg.Text = trackingNumber + " not a valid trackingNumber";
                                                else
                                                {
                                                    if (string.IsNullOrEmpty(poNum) && string.IsNullOrEmpty(trackingNumber))
                                                        lblMsg.Text = "Missing Fulfillment# & Tracking# data";
                                                    else
                                                    {
                                                        if (!string.IsNullOrEmpty(poNum) && string.IsNullOrEmpty(trackingNumber))
                                                            lblMsg.Text = "Missing Tracking# data";
                                                        else
                                                            if (string.IsNullOrEmpty(poNum) && !string.IsNullOrEmpty(trackingNumber))
                                                                lblMsg.Text = "Missing Fulfillment# data";
                                                    }
                                                }
                                                   
                                            }
                                            if (!string.IsNullOrEmpty(shippingVia))
                                            {
                                                var poShipViaList = (from item in shipViaList where item.ShipByCode.Equals(shippingVia) select item).ToList();
                                                if (poShipViaList != null && poShipViaList.Count > 0)
                                                {

                                                }
                                                else
                                                {
                                                    shipViaStringbuilder.Append(shippingVia + ", "); // +" is not valid";
                                                    lblMsg.Text = shipViaStringbuilder.ToString() + " invalid shipvia";
                                                }
                                            }
                                            //compositKey = poNum + lineNumber.ToString();

                                            //if (!string.IsNullOrEmpty(poNum) && lineNumber > 0)
                                            //{
                                            //    if (hshCheckDuplicateLineNo.ContainsKey(compositKey) && !string.IsNullOrEmpty(compositKey))
                                            //    {
                                            //        //uploadEsn = false;
                                            //        lblMsg.Text = "Duplicate " + lineNumber + " line number exists for FulfillmetOrder(" + poNum + ") in the file";

                                            //        //throw new ApplicationException("Duplicate " + lineNumber + " line number exists for FulfillmetOrder(" + poNum + ") in the file");
                                            //    }
                                            //    else if (!hshCheckDuplicateLineNo.ContainsKey(compositKey) && !string.IsNullOrEmpty(compositKey))
                                            //    {
                                            //        hshCheckDuplicateLineNo.Add(compositKey, compositKey);
                                            //    }
                                            //}
                                            ////if (saved == false)
                                            ////{
                                            ////    //stringbuilder.Append(poNum + ", ");

                                            ////}

                                        }
                                        line = poNum = trackingNumber = comments = shippingVia = shipmentType = string.Empty;
                                        //lineNumber = 0;

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
                                    Session["trackings"] = trackingInfoList;
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
            if (ConfigurationManager.AppSettings["ShipmentFilesStoreage"] != null)
            {
                fileStoreLocation = ConfigurationManager.AppSettings["ShipmentFilesStoreage"].ToString();
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
