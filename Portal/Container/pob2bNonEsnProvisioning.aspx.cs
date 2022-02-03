using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SV.Framework.Fulfillment;
using SV.Framework.Models.Fulfillment;

namespace avii.Container
{
    public partial class pob2bNonEsnProvisioning : System.Web.UI.Page
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
                        rptUpload.DataSource = null;
                        rptUpload.DataBind();
                        lblMsg.Text = "Please generate shipping label";
                        return;
                    }
                    Session["poskulist"] = skuList;
                    gvPOSKUs.DataSource = skuList;
                    gvPOSKUs.DataBind();
                    lblEsn.Text = "Line Item(s)";
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
            RegisterStartupScript("jsUnblockDialog", "unblockDialog();");
        }
        private void RegisterStartupScript(string key, string script)
        {
            ScriptManager.RegisterStartupScript(phrJsRunner, phrJsRunner.GetType(), key, script, true);
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int returnRecord = 0;
            int userID = 0, poid = 0, containerQty = 0;
            string fulfillmentNumber = string.Empty, returnMessage = string.Empty;
            int companyID = 0;

            SV.Framework.Fulfillment.ContainerProvisioningOperation containerProvisioningOperation = SV.Framework.Fulfillment.ContainerProvisioningOperation.CreateInstance<SV.Framework.Fulfillment.ContainerProvisioningOperation>();

            if (ViewState["poid"] != null)
            {
                poid = Convert.ToInt32(ViewState["poid"]);
            }
            if (ViewState["casePackQuantity"] != null)
                containerQty = Convert.ToInt32(ViewState["casePackQuantity"]);

            //   
            fulfillmentNumber = txtPoNum.Text.Trim();
            companyID = Convert.ToInt32(dpCompany.SelectedValue);

            avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
            if (userInfo != null)
            {
                userID = userInfo.UserGUID;
            }

            lblMsg.Text = string.Empty;

            List<ContainerNonESN> containersList = default;
            if (Session["nonesncontainers"] != null)
            {
                containersList = new List<ContainerNonESN>();
                containersList = Session["nonesncontainers"] as List<ContainerNonESN>;

                returnRecord = containerProvisioningOperation.PurchaseOrderContainerNonESNAssign(poid,  userID, containersList, containerQty);
                if (returnRecord > 0)
                {
                    lblMsg.Text = returnRecord + " record(s) submitted successfully";
                    btnSubmit.Enabled = false;
                    Session["containerEsnList"] = null;
                    Session["nonesncontainers"] = null;
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
            bool columnsIncorrectFormat = false;
            string poNum = txtPoNum.Text.Trim();

            bool IsValid = true;
            string previoustrackingNumber = string.Empty, trackingNumber = string.Empty;
            int trackingNumberCount = 0;
            int containerQty = 0, POID = 0;
            ContainerNonESN esnAssignment = default;
            int poQuantity = 0;
            List<ContainerNonESN> esnList = default;// new List<ContainerNonESN>();
            //List<ContainerInfo> skuList = default; // new List<ContainerInfo>();
            List<FulfillmentContainer> containers = default;

            if (Session["containerList"] != null)
            {
                containers = new List<FulfillmentContainer>();
                containers = Session["containerList"] as List<FulfillmentContainer>;
            }
            if (ViewState["poid"] != null)
                POID = Convert.ToInt32(ViewState["poid"]);

            //if (Session["poskulist"] != null)
            //    skuList = Session["poskulist"] as List<ContainerInfo>;

            if (ViewState["quantity"] != null)
                poQuantity = Convert.ToInt32(ViewState["quantity"]);
            if (ViewState["casePackQuantity"] != null)
                containerQty = Convert.ToInt32(ViewState["casePackQuantity"]);
            int companyID = 0, fileCount = 0;
            lblMsg.Text = string.Empty;
            Hashtable hshESN = new Hashtable();
            if (ViewState["trackingNumberCount"] != null)
                trackingNumberCount = Convert.ToInt32(ViewState["trackingNumberCount"]);
            bool IsMultiTrackingNumber = false;
            string invalidColumns = string.Empty;
            string duplicateESNs = string.Empty, duplicateICCIDs = string.Empty;

            //bool uploadEsn = false;
            if (dpCompany.SelectedIndex > 0)
            {
                if (!string.IsNullOrEmpty(poNum))
                {
                    esnList = new List<ContainerNonESN>();
                    companyID = Convert.ToInt32(dpCompany.SelectedValue);
                    try
                    {                       
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
                                                string containerID;//, fmupc, msl, lteICCID, lteIMSI, otksl, akey;
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
                                                    }
                                                    else if (!string.IsNullOrEmpty(line) && i > 0)
                                                    {
                                                        containerID = string.Empty;
                                                        string[] arr = line.Split(',');
                                                        try
                                                        {
                                                            if (arr.Length > 0)
                                                            {
                                                                containerID = arr[0].Trim();                                                                
                                                            }
                                                            if (string.IsNullOrEmpty(containerID))
                                                            {
                                                            }
                                                            else
                                                            {
                                                                if (string.IsNullOrEmpty(containerID))
                                                                {
                                                                    lblMsg.Text = "Missing ContainerID";
                                                                }
                                                                if (containers != null)
                                                                {
                                                                    var container = (from row in containers where row.ContainerID.Equals(containerID) select row).ToList();
                                                                    if (container == null)
                                                                    {
                                                                        lblMsg.Text = containerID+ " invalid ContainerID!";// "Invalid file data";
                                                                        return;
                                                                    }
                                                                }
                                                                esnAssignment = new ContainerNonESN();

                                                                if (hshESN.ContainsKey(containerID) && !string.IsNullOrEmpty(containerID))
                                                                {
                                                                    if (string.IsNullOrEmpty(duplicateESNs))
                                                                        duplicateESNs = containerID;
                                                                    else
                                                                        duplicateESNs = duplicateESNs + ", " + containerID;
                                                                    
                                                                }
                                                                else if (!hshESN.ContainsKey(containerID) && !string.IsNullOrEmpty(containerID))
                                                                {
                                                                    hshESN.Add(containerID, containerID);
                                                                }
                                                                esnAssignment.ContainerID = containerID;
                                                                esnAssignment.TrackingNumber = trackingNumber;

                                                                //esnAssignment.SNo = ctr;
                                                                esnList.Add(esnAssignment);
                                                                ctr = ctr + 1;
                                                            }
                                                            containerID = string.Empty;
                                                        }
                                                        catch (ApplicationException ex)
                                                        {
                                                            lblMsg.Text = ex.Message; //throw ex;
                                                        }
                                                        catch (Exception exception)
                                                        {
                                                            lblMsg.Text = exception.Message;
                                                        }
                                                        if (!string.IsNullOrEmpty(duplicateESNs))
                                                        {
                                                            lblMsg.Text = duplicateESNs + " duplicate ContainerID(s) exists in the file";
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
                            }
                        }
                        if (fileCount == 0)
                        {
                            lblMsg.Text = "Invalid file!";
                            return;
                        }
                        if (esnList != null && esnList.Count > 0 && columnsIncorrectFormat == false)
                        {
                            Session["nonesncontainers"] = esnList;
                            string errorMessage = string.Empty;
                            try
                            {                                
                                List<ContainerESNInfo> containerEsnList = containerProvisioningOperation.GetNonESNContainer(POID, esnList, containerQty);
                                if (containerEsnList != null && containerEsnList.Count > 0)
                                {                                    
                                    Session["containerEsnList"] = containerEsnList;
                                    rptESN.DataSource = containerEsnList;
                                    rptESN.DataBind();
                                    btnSubmit.Visible = true;
                                    btnUpload.Visible = false;
                                    lnkDownload.Visible = false;
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
                            if (esnList != null && esnList.Count == 0)
                            {
                                if (columnsIncorrectFormat)
                                {
                                    lblMsg.Text = invalidColumns + " column(s) name is not correct";
                                }
                                else
                                    lblMsg.Text = "There is no ContainerID to upload";
                            }
                            if (esnList != null)
                            {
                                if (columnsIncorrectFormat)
                                {
                                    lblMsg.Text = invalidColumns + " column(s) name is not correct";
                                }
                                else
                                    lblMsg.Text = "There is no ContainerID to upload";
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
            string fileStoreLocation = "~/UploadedData/";
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

            string string2CSV = "ContainerID" + Environment.NewLine;
            List<FulfillmentContainer> containers = default;

            if (Session["containerList"] != null)
            {
                containers = new List<FulfillmentContainer>();
                containers = Session["containerList"] as List<FulfillmentContainer>;
                if(containers != null && containers.Count > 0)
                {
                    foreach(FulfillmentContainer item in containers)
                    {
                        string2CSV = string2CSV + item.ContainerID + Environment.NewLine;
                    }
                }
                Session["containerList"] = null;
            }

            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=NonESNB2BProvisioning.csv");
            Response.Charset = "";
            Response.ContentType = "application/text";
            Response.Output.Write(string2CSV);
            Response.Flush();
            Response.End();

        }
        
    }
}